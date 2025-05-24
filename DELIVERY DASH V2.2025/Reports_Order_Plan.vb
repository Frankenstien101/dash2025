Imports Microsoft.Web.WebView2.WinForms
Imports System.Data.SqlClient
Imports System.Text.Json

Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Reports_Order_Plan

    Public Function GETDELIVERYDATE() As String

        Return DTDELIVERY.Value

    End Function

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
                var vehicleStoreCounts = {};

                function getPushpinColor(value) {
                    var hash = 0;
                    for (var i = 0; i < value.length; i++) {
                        hash = (hash << 5) - hash + value.charCodeAt(i);
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
                    Dim vehicleId As String = row.Cells("VEHICLE_ID").Value.ToString().Replace("'", "\'")

                    Dim latitude As Double = Convert.ToDouble(lat)
                    Dim longitude As Double = Convert.ToDouble(lng)
                    minLat = Math.Min(minLat, latitude)
                    maxLat = Math.Max(maxLat, latitude)
                    minLng = Math.Min(minLng, longitude)
                    maxLng = Math.Max(maxLng, longitude)

                    ' Add location data
                    htmlContent &= "locations.push({latitude: " & lat & ", longitude: " & lng & ", soPlanNumber: '" & soPlanNumber & "', customerId: '" & customerId & "', customerName: '" & customerName & "', vehicleId: '" & vehicleId & "'});"
                End If
            Next

            ' Continue HTML with JavaScript
            htmlContent &= "
                    for (var i = 0; i < locations.length; i++) {
                        var loc = locations[i];
                        var location = new Microsoft.Maps.Location(loc.latitude, loc.longitude);
                        var color = getPushpinColor(loc.vehicleId);

                        // Count stores per vehicle
                        if (!vehicleStoreCounts[loc.vehicleId]) {
                            vehicleStoreCounts[loc.vehicleId] = 0;
                        }
                        vehicleStoreCounts[loc.vehicleId]++;

                        var pushpin = new Microsoft.Maps.Pushpin(location, { color: color });

                        Microsoft.Maps.Events.addHandler(pushpin, 'click', (function(l) {
                            return function() {
                                window.chrome.webview.postMessage({
                                    customerId: l.customerId,
                                    customerName: l.customerName,
                                    soPlanNumber: l.soPlanNumber
                                });
                            };
                        })(loc));

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
                    legendHtml += '<b>Legend (Vehicle ID - Store Count):</b><br>';
                    for (var vehicle in vehicleStoreCounts) {
                        var color = getPushpinColor(vehicle);
                        var count = vehicleStoreCounts[vehicle];
                        legendHtml += '<div style=""display:flex;align-items:center;margin-bottom:5px;"">';
                        legendHtml += '<div style=""background-color:' + color + ';width:20px;height:20px;margin-right:5px;""></div>' + vehicle + ' - ' + count + ' store(s)';
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

    End Sub

    ' Fetch SO_PLAN_NUMBER to ComboBox

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
                  AND SITE_ID = '" & Form1.SITEID.Text & "' AND SO_PLAN_NUMBER != '" & SOPLANNUMBER.Text & "'
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

    Private Sub GunaAdvenceButton3_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton3.Click

        Reports_Order_Plan_Re_Assign_Per_Seller.ShowDialog()

    End Sub

    Private Sub GunaAdvenceButton2_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton2.Click

        Dim l As MsgBoxResult

        l = MsgBox("GENERATE AND DOWNLOAD REPORT?", MsgBoxStyle.YesNo, "CONFIRM")

        If l = MsgBoxResult.Yes Then

            If DTGDATA.Rows.Count = 0 Then

                MsgBox("NO DATA TO EXPORT", MsgBoxStyle.Exclamation, "SORRY")

            Else

                GunaAdvenceButton2.Enabled = False
                EXPORT()

            End If

        End If

    End Sub

    Private Sub releaseObject(ByVal obj As Object)

        Try

            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)

            obj = Nothing

        Catch ex As Exception

            obj = Nothing

        Finally

            GC.Collect()

        End Try

    End Sub

    Private Sub copyAlltoClipboard(dt As DataGridView)

        dt.SelectAll()
        Clipboard.SetDataObject(dt.GetClipboardContent())
        dt.ClearSelection()

    End Sub

    Private Sub EXPORT()

        Dim dt As DataGridView
        Dim filenames As String
        dt = DTGDATA

        Dim l As String
        l = Form1.DTCALENDAR.Value

        l = l.Replace("/", "")

        Dim sfd As New SaveFileDialog()
        sfd.Filter = "Excel Documents (.xls)|.xls"
        sfd.FileName = filenames & "SALESORDERPLAN" & Form1.COMPANYID.Text & Form1.SITEID.Text & l & ".xls"

        If sfd.ShowDialog() = DialogResult.OK Then

            ' Copy DataGridView results to clipboard

            copyAlltoClipboard(dt)

            Dim misValue As Object = System.Reflection.Missing.Value
            Dim xlexcel As New Excel.Application()

            xlexcel.DisplayAlerts = False ' Without this you will get two confirm overwrite prompts
            Dim xlWorkBook As Excel.Workbook = xlexcel.Workbooks.Add(misValue)
            Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets(1)

            Dim rng As Excel.Range = xlWorkSheet.Range("D:D")
            rng.NumberFormat = "@"

            ' Format column D as text before pasting results, this was required for my data

            ' Paste clipboard results to worksheet range
            ' Dim CR As Excel.Range = xlWorkSheet.Cells(1, 1)
            xlWorkSheet.Cells(1, 1).PasteSpecial(Excel.XlPasteType.xlPasteAll, Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, Type.Missing, Type.Missing)

            ' For some reason column A is always blank in the worksheet. ¯\(ツ)/¯
            ' Delete blank column A and select cell A1
            ' Dim delRng As Excel.Range = xlWorkSheet.Range("A:A")
            ' delRng.Delete(Excel.XlDeleteShiftDirection.xlShiftToLeft)
            ' xlWorkSheet.Range("A1").Select()

            ' Save the excel file under the captured location from the SaveFileDialog
            xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
            xlexcel.DisplayAlerts = True
            xlWorkBook.Close(True, misValue, misValue)
            xlexcel.Quit()

            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlexcel)

            ' Clear Clipboard and DataGridView selection
            Clipboard.Clear()
            dt.ClearSelection()

            If MessageBox.Show("Download Complete! Would you like to open the file?", "Successfully Saved!", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                If File.Exists(sfd.FileName) Then
                    System.Diagnostics.Process.Start(sfd.FileName)
                End If

            Else
                ' Do nothing
            End If
            ' Open the newly saved excel file
        End If

        GunaAdvenceButton2.Enabled = True

    End Sub

    Private Sub GunaAdvenceButton5_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton5.Click

        Reports_Order_Plan_Recalibrate.DTTODAY.Text = DTDELIVERY.Value
        Reports_Order_Plan_Recalibrate.ShowDialog()

    End Sub

End Class
