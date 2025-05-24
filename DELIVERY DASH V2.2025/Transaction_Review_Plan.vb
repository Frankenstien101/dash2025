
Imports System.Data.SqlClient

Public Class Transaction_Review_Plan

    Public Function GETBATCHNUMBER() As String

        Return MERGEFROM.Text

    End Function

    Public Sub TriggerButtonREF()


        DTGMERGEFROM.DataSource = Nothing

        DISTANCETXT.Text = ""
        WEIGHTTXT.Text = ""

        con.Open()

        dt.Clear()

        Dim query As String = "SELECT STORE_LAT,STORE_LONG,BATCH FROM Dash_Plan_Batch_Details WHERE DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "'AND SITE_ID = '" & Form1.SITEID.Text & "'"

        Using command As New SqlCommand(query, con)

            Dim adapter As New SqlDataAdapter(command)

            adapter.Fill(dt)

        End Using

        con.Close()

        ' Bind the DataTable to the DataGridView
        DTGBATCH1.DataSource = dt

        Try

            views("SELECT BATCH_ID,NUM_OF_INVOICES,TOTAL_VALUE,TOTAL_VOLUME,WEIGHT,AGENT,VEHICLE_ID FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND STATUS != 'MERGED'", "BSPIDB", DTGMERGEFROM)
            DTGMERGEFROM.Columns(2).DefaultCellStyle.Format = "N2"

        Catch ex As Exception

            ' MsgBox(ex.ToString)

        End Try

        ' This will trigger Button1's click event
    End Sub

    Private Sub Transaction_Review_Plan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        AVAILABILITY.Text = "-"
        Control.CheckForIllegalCrossThreadCalls = False
        DTDELIVERY.Value = Form1.GETDATE

        COMPANYID.Text = Form1.GETCOMPANYID
        SITEID.Text = Form1.GETSITEID

        GunaAdvenceButton2.Enabled = False

    End Sub

    Private Async Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click


        TRIGGERMAP.Text = "ALL"

        Try
            Await WebView21.EnsureCoreWebView2Async(Nothing)
        Catch ex As Exception
            MsgBox("Error initializing WebView2: " & ex.Message)
        End Try
        ' DTDELIVERY.Value = Form1.DTCALENDAR.Value

        ' Add handler for WebView2 messages
        '  AddHandler WebView21.CoreWebView2.WebMessageReceived, AddressOf WebView21_WebMessageReceived

        DTGROUTES.DataSource = Nothing

        Try

            views3("SELECT STORE_LAT,STORE_LONG,BATCH,CUSTOMER_ID,CUSTOMER_NAME
                    FROM Dash_Plan_Batch_Details 
                    WHERE DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "'", "BSPIDB", DTGROUTES)

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

            For Each row As DataGridViewRow In DTGROUTES.Rows
                If row.Cells("STORE_LAT").Value IsNot DBNull.Value AndAlso row.Cells("STORE_LONG").Value IsNot DBNull.Value Then
                    Dim lat As String = row.Cells("STORE_LAT").Value.ToString()
                    Dim lng As String = row.Cells("STORE_LONG").Value.ToString()
                    Dim soPlanNumber As String = row.Cells("BATCH").Value.ToString()
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

    End Sub

    Private dt As New DataTable()

    Private Sub DTDELIVERY_ValueChanged(sender As Object, e As EventArgs) Handles DTDELIVERY.ValueChanged

        DTGMERGEFROM.DataSource = Nothing

        DISTANCETXT.Text = ""
        WEIGHTTXT.Text = ""

        con.Open()

        dt.Clear()

        Dim query As String = "SELECT STORE_LAT,STORE_LONG,BATCH FROM Dash_Plan_Batch_Details WHERE DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "'AND SITE_ID = '" & Form1.SITEID.Text & "'"

        Using command As New SqlCommand(query, con)

            Dim adapter As New SqlDataAdapter(command)

            adapter.Fill(dt)

        End Using

        con.Close()

        ' Bind the DataTable to the DataGridView
        DTGBATCH1.DataSource = dt

        Try

            views("SELECT BATCH_ID,NUM_OF_INVOICES,TOTAL_VALUE,TOTAL_VOLUME,WEIGHT,AGENT,VEHICLE_ID FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND STATUS != 'MERGED'", "BSPIDB", DTGMERGEFROM)
            DTGMERGEFROM.Columns(2).DefaultCellStyle.Format = "N2"

        Catch ex As Exception

            ' MsgBox(ex.ToString)

        End Try

    End Sub

    Private Async Sub DTGMERGEFROM_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGMERGEFROM.CellClick

        TRIGGERMAP.Text = "BATCH"

        AVAILABILITY.Text = "-"

        MERGEFROM.Text = ""

        TOTALINVOICE.Text = ""
        TOTALVALUE.Text = ""
        TOTALVOLUME.Text = ""
        TOTALWEIGHT.Text = ""
        AGENT.Text = ""
        WEIGHTTXT.Text = "0"

        Try

            MERGEFROM.Text = DTGMERGEFROM.CurrentRow.Cells(0).Value
            TOTALINVOICE.Text = DTGMERGEFROM.CurrentRow.Cells(1).Value
            TOTALVALUE.Text = DTGMERGEFROM.CurrentRow.Cells(2).Value
            TOTALVOLUME.Text = DTGMERGEFROM.CurrentRow.Cells(3).Value
            TOTALWEIGHT.Text = DTGMERGEFROM.CurrentRow.Cells(4).Value
            AGENT.Text = DTGMERGEFROM.CurrentRow.Cells(5).Value

            WEIGHTTXT.Text = TOTALWEIGHT.Text

        Catch ex As Exception

        End Try

        'BTNCLEARMAP.PerformClick()

        Try

            con.Open()

            dt.Clear()

            Dim query As String = "SELECT STORE_LAT,STORE_LONG,CUSTOMER_NAME FROM Dash_Plan_Batch_Details WHERE DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "'AND SITE_ID = '" & Form1.SITEID.Text & "' AND BATCH = '" & MERGEFROM.Text & "'"

            Using command As New SqlCommand(query, con)

                Dim adapter As New SqlDataAdapter(command)

                adapter.Fill(dt)

            End Using

            con.Close()

            ' Bind the DataTable to the DataGridView
            DTGBATCH1.DataSource = dt

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        Try

            views1("SELECT BATCH_ID,NUM_OF_INVOICES,TOTAL_VALUE,TOTAL_VOLUME FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND BATCH_ID != '" & MERGEFROM.Text & "' AND STATUS != 'MERGED'", "BSPIDB", DTGMERGETO)

            DTGMERGETO.Columns(2).DefaultCellStyle.Format = "N2"

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        Try

            views2("SELECT INVOICE_NUMBER FROM Dash_Plan_Batch_Details WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND STATUS = 'DELIVERED' AND BATCH = '" & MERGEFROM.Text & "'", "BSPIDB", DTGCHECK)

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        If DTGCHECK.Rows.Count > 0 Then

            AVAILABILITY.Text = "UNAVAILABLE"
            AVAILABILITY.ForeColor = Color.Maroon
            GunaAdvenceButton2.Enabled = False

        Else

            AVAILABILITY.Text = "AVAILABLE"
            AVAILABILITY.ForeColor = Color.Green
            GunaAdvenceButton2.Enabled = True

        End If

        Try
            Await WebView21.EnsureCoreWebView2Async(Nothing)
        Catch ex As Exception
            MsgBox("Error initializing WebView2: " & ex.Message)
        End Try
        ' DTDELIVERY.Value = Form1.DTCALENDAR.Value

        ' Add handler for WebView2 messages
        '  AddHandler WebView21.CoreWebView2.WebMessageReceived, AddressOf WebView21_WebMessageReceived

        DTGROUTES.DataSource = Nothing

        Try

            views3("SELECT STORE_LAT,STORE_LONG,BATCH,CUSTOMER_ID,CUSTOMER_NAME
                    FROM Dash_Plan_Batch_Details 
                    WHERE DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND BATCH = '" & MERGEFROM.Text & "'", "BSPIDB", DTGROUTES)

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

            For Each row As DataGridViewRow In DTGROUTES.Rows
                If row.Cells("STORE_LAT").Value IsNot DBNull.Value AndAlso row.Cells("STORE_LONG").Value IsNot DBNull.Value Then
                    Dim lat As String = row.Cells("STORE_LAT").Value.ToString()
                    Dim lng As String = row.Cells("STORE_LONG").Value.ToString()
                    Dim soPlanNumber As String = row.Cells("BATCH").Value.ToString()
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

    End Sub

    Private Sub GunaAdvenceButton2_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton2.Click


        If MERGEFROM.Text = "" Or MERGETO.Text = "" Then

            MsgBox("PLEASE SELECT FROM LIST", MsgBoxStyle.Exclamation, "SORRY")

        Else

            Dim D As MsgBoxResult

            D = MsgBox("ARE YOU SURE TO MERGE THIS BATCH?", MsgBoxStyle.YesNo, "CONFIRM")

            If D = MsgBoxResult.Yes Then

                Try

                    views2("SELECT AGENT,VEHICLE_ID FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND BATCH_ID = '" & MERGETO.Text & "'", "BSPIDB", DTGCHECK)
                    AGENT.Text = DTGCHECK.CurrentRow.Cells(0).Value
                    VEHICLENEW.Text = DTGCHECK.CurrentRow.Cells(1).Value

                Catch ex As Exception

                    MsgBox(ex.ToString)

                End Try

                Try

                    Dim L As String = "UPDATE Dash_Plan_Batch_Details SET BATCH = '" & MERGETO.Text & "' , AGENT_ID = '" & AGENT.Text & "'  WHERE BATCH = '" & MERGEFROM.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "'"

                    Data(L)

                Catch ex As Exception

                    MsgBox("1" & ex.ToString)
                End Try

                Try

                    Dim LD As String = "UPDATE Dash_Plan_Batch_Transaction SET STATUS = 'MERGED' WHERE BATCH_ID = '" & MERGEFROM.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "'"

                    Data(LD)

                Catch ex As Exception
                    MsgBox("2" & ex.ToString)
                End Try

                Try

                    Dim LD As String = "UPDATE Dash_Plan_Batch_Transaction SET NUM_OF_INVOICES += '" & TOTALINVOICE.Text & "', TOTAL_VALUE += '" & TOTALVALUE.Text & "', TOTAL_VOLUME += '" & TOTALVOLUME.Text & "',WEIGHT += '" & TOTALWEIGHT.Text & "' WHERE BATCH_ID = '" & MERGETO.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "'"

                    Data(LD)

                Catch ex As Exception
                    MsgBox("3" & ex.ToString)
                End Try

                MsgBox("BATCH SUCCESFULLY MERGED!", MsgBoxStyle.Information, "COMPLETED")

                GunaAdvenceButton1.PerformClick()

                Try

                    views("SELECT BATCH_ID,NUM_OF_INVOICES,TOTAL_VALUE,TOTAL_VOLUME,WEIGHT,AGENT FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND STATUS = 'READY'", "BSPIDB", DTGMERGEFROM)
                    DTGMERGEFROM.Columns(2).DefaultCellStyle.Format = "N2"

                Catch ex As Exception

                    MsgBox(ex.ToString)

                End Try

                DTGMERGETO.DataSource = Nothing

            End If

        End If

    End Sub

    Private Sub GunaLinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles GunaLinkLabel1.LinkClicked

        If MERGEFROM.Text = "" Then

            MsgBox("PLEASE SELECT FROM LIST", MsgBoxStyle.Exclamation, "SORRY")

        Else

            Transaction_Review_Plan_Invoice_Transfer.ShowDialog()

        End If

    End Sub

    Private Async Sub GunaAdvenceButton3_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton3.Click

        If MERGEFROM.Text = "" Or MERGETO.Text = "" Then

            MsgBox("PLEASE SELECT BATCH ON BOTH DATA TO COMPUTE", MsgBoxStyle.Exclamation, "SORRY")

        Else

            SEARCH2MERGE.Text = "'" & MERGEFROM.Text & "'" & "," & "'" & MERGETO.Text & "'"

            Try

                con.Open()

                dt.Clear()

                Dim query As String = "SELECT STORE_LAT,STORE_LONG,BATCH FROM Dash_Plan_Batch_Details WHERE DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "'AND SITE_ID = '" & Form1.SITEID.Text & "' AND BATCH IN (" & SEARCH2MERGE.Text & ")"

                Using command As New SqlCommand(query, con)

                    Dim adapter As New SqlDataAdapter(command)

                    adapter.Fill(dt)

                End Using

                con.Close()

                ' Bind the DataTable to the DataGridView
                DTGBATCH1.DataSource = dt

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try

            '' ADD MAP

            TRIGGERMAP.Text = "BATCH"

            AVAILABILITY.Text = "-"

            MERGEFROM.Text = ""

            TOTALINVOICE.Text = ""
            TOTALVALUE.Text = ""
            TOTALVOLUME.Text = ""
            TOTALWEIGHT.Text = ""
            AGENT.Text = ""
            WEIGHTTXT.Text = "0"

            Try

                MERGEFROM.Text = DTGMERGEFROM.CurrentRow.Cells(0).Value
                TOTALINVOICE.Text = DTGMERGEFROM.CurrentRow.Cells(1).Value
                TOTALVALUE.Text = DTGMERGEFROM.CurrentRow.Cells(2).Value
                TOTALVOLUME.Text = DTGMERGEFROM.CurrentRow.Cells(3).Value
                TOTALWEIGHT.Text = DTGMERGEFROM.CurrentRow.Cells(4).Value
                AGENT.Text = DTGMERGEFROM.CurrentRow.Cells(5).Value

                WEIGHTTXT.Text = TOTALWEIGHT.Text

            Catch ex As Exception

            End Try

            'BTNCLEARMAP.PerformClick()

            Try

                con.Open()

                dt.Clear()

                Dim query As String = "SELECT STORE_LAT,STORE_LONG,CUSTOMER_NAME FROM Dash_Plan_Batch_Details WHERE DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "'AND SITE_ID = '" & Form1.SITEID.Text & "' AND BATCH = '" & MERGEFROM.Text & "'"

                Using command As New SqlCommand(query, con)

                    Dim adapter As New SqlDataAdapter(command)

                    adapter.Fill(dt)

                End Using

                con.Close()

                ' Bind the DataTable to the DataGridView
                DTGBATCH1.DataSource = dt

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try

            Try

                views1("SELECT BATCH_ID,NUM_OF_INVOICES,TOTAL_VALUE,TOTAL_VOLUME FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND BATCH_ID != '" & MERGEFROM.Text & "' AND STATUS != 'MERGED'", "BSPIDB", DTGMERGETO)

                DTGMERGETO.Columns(2).DefaultCellStyle.Format = "N2"

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try

            Try

                views2("SELECT INVOICE_NUMBER FROM Dash_Plan_Batch_Details WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND STATUS = 'DELIVERED' AND BATCH = '" & MERGEFROM.Text & "'", "BSPIDB", DTGCHECK)

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try

            If DTGCHECK.Rows.Count > 0 Then

                AVAILABILITY.Text = "UNAVAILABLE"
                AVAILABILITY.ForeColor = Color.Maroon
                GunaAdvenceButton2.Enabled = False

            Else

                AVAILABILITY.Text = "AVAILABLE"
                AVAILABILITY.ForeColor = Color.Green
                GunaAdvenceButton2.Enabled = True

            End If

            Try
                Await WebView21.EnsureCoreWebView2Async(Nothing)
            Catch ex As Exception
                MsgBox("Error initializing WebView2: " & ex.Message)
            End Try
            ' DTDELIVERY.Value = Form1.DTCALENDAR.Value

            ' Add handler for WebView2 messages
            '  AddHandler WebView21.CoreWebView2.WebMessageReceived, AddressOf WebView21_WebMessageReceived

            DTGROUTES.DataSource = Nothing

            Try

                views3("SELECT STORE_LAT,STORE_LONG,BATCH,CUSTOMER_ID,CUSTOMER_NAME
                    FROM Dash_Plan_Batch_Details 
                    WHERE DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND BATCH IN ('" & MERGEFROM.Text & "','" & MERGETO.Text & "' )", "BSPIDB", DTGROUTES)

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

                For Each row As DataGridViewRow In DTGROUTES.Rows
                    If row.Cells("STORE_LAT").Value IsNot DBNull.Value AndAlso row.Cells("STORE_LONG").Value IsNot DBNull.Value Then
                        Dim lat As String = row.Cells("STORE_LAT").Value.ToString()
                        Dim lng As String = row.Cells("STORE_LONG").Value.ToString()
                        Dim soPlanNumber As String = row.Cells("BATCH").Value.ToString()
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


        End If

    End Sub

    Private Sub DTGMERGEFROM_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGMERGEFROM.CellContentClick

        TRIGGERMAP.Text = "ALL"

        MERGETO.Text = ""

        Try

            MERGETO.Text = DTGMERGETO.CurrentRow.Cells(0).Value

        Catch ex As Exception

        End Try


        GunaAdvenceButton3.PerformClick()

    End Sub

End Class