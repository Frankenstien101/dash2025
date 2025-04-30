Imports Microsoft.Web.WebView2.WinForms
Imports System.Data.SqlClient
Imports System.Text.Json

Public Class Reports_Order_Plan

    Private Async Sub Reports_Order_Plan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Await WebView21.EnsureCoreWebView2Async(Nothing)
        Catch ex As Exception
            MsgBox("Error initializing WebView2: " & ex.Message)
        End Try
        DTDELIVERY.Value = Form1.DTCALENDAR.Value

        ' Add handler for WebView2 messages
        AddHandler WebView21.CoreWebView2.WebMessageReceived, AddressOf WebView21_WebMessageReceived
    End Sub

    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click
        Try
            ' Fetch data into DataGridView
            views("SELECT Dash_SO_Plan_Batch_Details.COMPANY_ID, 
                          Dash_SO_Plan_Batch_Details.SITE_ID,
                          Dash_SO_Plan_Batch_Details.SO_PLAN_NUMBER,
                          [SO_NUMBER],
                          VEHICLE_ID,
                          SO_PICK_BATCH,
                          [CUSTOMER_ID],
                          [CUSTOMER_NAME],
                          [TOTAL_AMOUNT],
                          [STORE_LAT],
                          [STORE_LONG],
                          [ORDER_DATE],
                          Dash_SO_Plan_Batch_Details.STATUS,
                          [SUB_BATCH],
                          [SUB_DA],
                          [VEHICLE_IDS]
                   FROM [dbo].[Dash_SO_Plan_Batch_Details] 
                   LEFT JOIN Dash_SO_Plan_Transaction 
                   ON Dash_SO_Plan_Transaction.SO_PLAN_NUMBER = Dash_SO_Plan_Batch_Details.SO_PLAN_NUMBER 
                   WHERE Dash_SO_Plan_Batch_Details.COMPANY_ID = '" & Form1.COMPANYID.Text & "' 
                   AND Dash_SO_Plan_Batch_Details.SITE_ID = '" & Form1.SITEID.Text & "' 
                   AND ORDER_DATE = '" & DTDELIVERY.Value & "' 
                   AND Dash_SO_Plan_Batch_Details.STATUS != 'NEW'",
                   "BSPIDB", DTGDATA)

            ' Prepare HTML
            Dim htmlContent As String = "
            <html>
            <head>
                <title>Bing Maps</title>
                <script type='text/javascript' src='https://www.bing.com/api/maps/mapcontrol?key=u8bVA0NAuVbJ5r3MZgUR~fk8RCjst0j6nqTGJqGeK6w~Aj-CdHthPN5zeNnT6yh7YYRy1RGcZ3--xAUdQNkjP7_5t7ysVrqpNUvA6QYV-fYC'></script>
                <script type='text/javascript'>
                    var map;
                    var locations = [];
                    var uniqueSOPlans = {};

                    function getPushpinColor(soPlanNumber) {
                        var hash = 0;
                        for (var i = 0; i < soPlanNumber.length; i++) {
                            hash = (hash << 5) - hash + soPlanNumber.charCodeAt(i);
                            hash = hash & hash;
                        }
                        var colorIndex = Math.abs(hash % 12);
                        var colors = ['#FF0000', '#00FF00', '#0000FF', '#FFFF00', '#FF00FF', '#00FFFF', '#800000', '#008000', '#000080', '#808000', '#800080', '#008080'];
                        return colors[colorIndex];
                    }

                    function initialize() {
                        map = new Microsoft.Maps.Map('#myMap', {
                            center: new Microsoft.Maps.Location(13.7563, 100.5018),
                            zoom: 8
                        });
            "

            ' Calculate map bounds
            Dim minLat As Double = Double.MaxValue
            Dim maxLat As Double = Double.MinValue
            Dim minLng As Double = Double.MaxValue
            Dim maxLng As Double = Double.MinValue

            For Each row As DataGridViewRow In DTGDATA.Rows
                If row.Cells("STORE_LAT").Value IsNot DBNull.Value AndAlso row.Cells("STORE_LONG").Value IsNot DBNull.Value Then
                    Dim lat As String = row.Cells("STORE_LAT").Value.ToString()
                    Dim lng As String = row.Cells("STORE_LONG").Value.ToString()
                    Dim soPlanNumber As String = row.Cells("SO_PLAN_NUMBER").Value.ToString()
                    Dim customerId As String = row.Cells("CUSTOMER_ID").Value.ToString().Replace("'", "\'")
                    Dim customerName As String = row.Cells("CUSTOMER_NAME").Value.ToString().Replace("'", "\'")

                    Dim latitude As Double = Convert.ToDouble(lat)
                    Dim longitude As Double = Convert.ToDouble(lng)
                    minLat = Math.Min(minLat, latitude)
                    maxLat = Math.Max(maxLat, latitude)
                    minLng = Math.Min(minLng, longitude)
                    maxLng = Math.Max(maxLng, longitude)

                    ' Add pushpins
                    htmlContent &= "locations.push({latitude: " & lat & ", longitude: " & lng & ", soPlanNumber: '" & soPlanNumber & "', customerId: '" & customerId & "', customerName: '" & customerName & "'});"
                    htmlContent &= "uniqueSOPlans['" & soPlanNumber & "'] = true;"
                End If
            Next

            htmlContent &= "
                        for (var i = 0; i < locations.length; i++) {
                            var location = new Microsoft.Maps.Location(locations[i].latitude, locations[i].longitude);
                            var color = getPushpinColor(locations[i].soPlanNumber);
                            var pushpin = new Microsoft.Maps.Pushpin(location, { color: color });

                            Microsoft.Maps.Events.addHandler(pushpin, 'click', (function(loc) {
                                return function() {
                                    window.chrome.webview.postMessage({
                                        customerId: loc.customerId,
                                        customerName: loc.customerName,
                                        soPlanNumber: loc.soPlanNumber
                                    });
                                };
                            })(locations[i]));

                            map.entities.push(pushpin);
                        }

                        // Fit bounds
                        var bounds = Microsoft.Maps.LocationRect.fromCorners(
                            new Microsoft.Maps.Location(" & minLat.ToString() & ", " & minLng.ToString() & "),
                            new Microsoft.Maps.Location(" & maxLat.ToString() & ", " & maxLng.ToString() & ")
                        );
                        map.setView({ bounds: bounds });

                        // Create Legend
                        var legendHtml = '<div style=""position:absolute;top:10px;left:10px;background:white;padding:10px;border-radius:5px;box-shadow:0 0 5px rgba(0,0,0,0.5);z-index:999;"">';
                        legendHtml += '<b>Legend:</b><br>';
                        for (var soPlan in uniqueSOPlans) {
                            var color = getPushpinColor(soPlan);
                            legendHtml += '<div style=""display:flex;align-items:center;margin-bottom:5px;"">';
                            legendHtml += '<div style=""background-color:' + color + ';width:20px;height:20px;margin-right:5px;""></div>' + soPlan;
                            legendHtml += '</div>';
                        }
                        legendHtml += '</div>';
                        document.body.insertAdjacentHTML('beforeend', legendHtml);
                    }

                    window.onload = initialize;
                </script>
            </head>
            <body style='margin:0;padding:0;'>
                <div id='myMap' style='position:relative;width:100%;height:100%;'></div>
            </body>
            </html>"

            ' Load to WebView
            WebView21.NavigateToString(htmlContent)

        Catch ex As Exception
            MsgBox("Error: " & ex.ToString())
        End Try

        ' Fetch SO_PLAN_NUMBER to ComboBox

    End Sub

    Private Sub WebView21_WebMessageReceived(sender As Object, e As Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs)
        Dim json As String = e.WebMessageAsJson
        Dim data As Dictionary(Of String, String) = JsonSerializer.Deserialize(Of Dictionary(Of String, String))(json)

        If data IsNot Nothing Then
            STOREID.Text = data("customerId")
            STORENAME.Text = data("customerName")
            SOPLANNUMBER.Text = data("soPlanNumber")


            Try
                con.Open()
                Dim daA As New SqlDataAdapter("
                SELECT SO_PLAN_NUMBER 
                FROM Dash_SO_Plan_Batch_Details 
                WHERE ORDER_DATE = '" & DTDELIVERY.Value & "' 
                  AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' 
                  AND SITE_ID = '" & Form1.SITEID.Text & "' AND SO_PLAN_NUMBER != '" & SONUMBER.Text & "'
                GROUP BY SO_PLAN_NUMBER", con)
                Dim dtA As New DataTable
                daA.Fill(dtA)
                SONUMBER.DisplayMember = "SO_PLAN_NUMBER"
                SONUMBER.DataSource = dtA
                con.Close()
            Catch ex As Exception
                ' Ignore if error

            End Try

        End If
    End Sub

    Private Sub GunaAdvenceButton4_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton4.Click

        If STOREID.Text = "-" Then

            MsgBox("NO STORE TO TRANSFER", MsgBoxStyle.Exclamation, "SORRY")

        Else

            Try

                Dim A As String = "UPDATE Dash_SO_Plan_Batch_Details SET SO_PLAN_NUMBER = '" & SONUMBER.Text & "' WHERE SO_PLAN_NUMBER = '" & SOPLANNUMBER.Text & "' AND CUSTOMER_ID = '" & STOREID.Text & "'"

                Data(A)

            Catch ex As Exception

            End Try

            STOREID.Text = "-"
            STORENAME.Text = "-"
            SOPLANNUMBER.Text = "-"

            GunaAdvenceButton1.PerformClick()

        End If

    End Sub

End Class
