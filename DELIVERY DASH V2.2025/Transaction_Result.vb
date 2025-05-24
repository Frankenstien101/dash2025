
Imports System.Data.SqlClient
Public Class Transaction_Result
    Private Sub Transaction_Result_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        COMPANYID.Text = Form1.GETCOMPANYID
        SITEID.Text = Form1.GETSITEID

        DTDELIVER.Value = Form1.GETDATE

        Try

            con.Open()

            Dim da As New SqlDataAdapter("SELECT USERNAME FROM Dash_Agents WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' ", con)

            Dim dt As New DataTable

            da.Fill(dt)

            CMBAGENT.DisplayMember = "USERNAME"

            CMBAGENT.DataSource = dt

            con.Close()

        Catch ex As Exception

        End Try

        Try

            views("SELECT BATCH_ID,VEHICLE_ID,AGENT,DROP_COUNT,NUM_OF_INVOICES FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND  STATUS = 'PROCESSED' AND DATE_TO_DELIVER = '" & DTDELIVER.Value & "'", "BSPIDB", DTGAGENT)

        Catch ex As Exception

        End Try

        For Each row As DataGridViewRow In DTGAGENT.Rows

            row.Height = 25 ' Set the desired height for all rows

        Next

    End Sub

    Private Sub CMBAGENT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMBAGENT.SelectedIndexChanged

        Try

            views("SELECT BATCH_ID,VEHICLE_ID,AGENT,DROP_COUNT,NUM_OF_INVOICES FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND  STATUS = 'PROCESSED' AND AGENT = '" & CMBAGENT.Text & "' AND DATE_TO_DELIVER = '" & DTDELIVER.Value & "'", "BSPIDB", DTGAGENT)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub DTDELIVER_ValueChanged(sender As Object, e As EventArgs) Handles DTDELIVER.ValueChanged


        DTGINVOICESSTATUS.DataSource = Nothing

        TOTALINVOICES.Text = "0"
        TOTALDROP.Text = "0"
        COLLECTIONAMOUNT.Text = "0"
        CHECKID.Text = ""
        STATUSPERCENTAGE.Text = "0%"

        Try

            views("SELECT BATCH_ID,VEHICLE_ID,AGENT,DROP_COUNT,NUM_OF_INVOICES FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND  STATUS = 'PROCESSED' AND DATE_TO_DELIVER = '" & DTDELIVER.Value & "'", "BSPIDB", DTGAGENT)

            DTGAGENT.Columns(3).Visible = False

        Catch ex As Exception

        End Try

        For Each row As DataGridViewRow In DTGAGENT.Rows

            row.Height = 25 ' Set the desired height for all rows

        Next


    End Sub

    Dim dropcount As Integer

    Private Sub DTGAGENT_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGAGENT.CellClick

        TOTALINVOICES.Text = "0"
        TOTALDROP.Text = "0"
        COLLECTIONAMOUNT.Text = "0"
        CHECKID.Text = ""
        STATUSPERCENTAGE.Text = "0%"

        Try

            BATCH.Text = DTGAGENT.CurrentRow.Cells(0).Value
            TOTALDROP.Text = "0"
            TOTALINVOICES.Text = DTGAGENT.CurrentRow.Cells(4).Value
            VEHICLEID.Text = DTGAGENT.CurrentRow.Cells(2).Value

        Catch ex As Exception

        End Try

        Try

            views1("SELECT CUSTOMER_ID,BATCH
   
                    FROM Dash_Plan_Batch_Details

              WHERE BATCH = '" & BATCH.Text & "' AND STATUS != 'FOR DELIVERY'

              GROUP BY CUSTOMER_ID,BATCH", "BSPIDB", DTGCHECK)

            dropcount = DTGCHECK.Rows.Count()

        Catch ex As Exception

        End Try

        Try

            views1("SELECT CUSTOMER_ID,BATCH
   
                    FROM Dash_Plan_Batch_Details

              WHERE BATCH = '" & BATCH.Text & "'

              GROUP BY CUSTOMER_ID,BATCH", "BSPIDB", DTGCHECK)

            TOTALDROP.Text = dropcount & "/" & DTGCHECK.Rows.Count()

        Catch ex As Exception

        End Try

        Try

            views1("SELECT SUM(COLLECTED_AMOUNT)
   
                  FROM [dbo].[Dash_Payments]
                  
                  LEFT JOIN Dash_Plan_Batch_Details ON Dash_Plan_Batch_Details.INVOICE_NUMBER = Dash_Payments.INVOICE_NUMBER
                  
                  WHERE BATCH = '" & BATCH.Text & "'", "BSPIDB", DTGCHECK)

            COLLECTIONAMOUNT.Text = DTGCHECK.CurrentRow.Cells(0).Value

        Catch ex As Exception

        End Try

        Try

            views1("SELECT COUNT(BATCH)
   
                FROM [dbo].[Dash_Payments]
              
                LEFT JOIN Dash_Plan_Batch_Details ON Dash_Plan_Batch_Details.INVOICE_NUMBER = Dash_Payments.INVOICE_NUMBER
              
                WHERE Dash_Payments.STATUS IN (" & "'DELIVERED','VERIFIED'" & ") AND BATCH ='" & BATCH.Text & "'", "BSPIDB", DTGCHECK)

            CHECKID.Text = DTGCHECK.CurrentRow.Cells(0).Value

        Catch ex As Exception

        End Try

        Try

            views1("SELECT INVOICE_NUMBER AS INV_NUMBER,CUSTOMER_ID AS CU_ID,CUSTOMER_NAME AS CUSTOMER,TOTAL_AMOUNT AS TOTAL,DISTANCE,STATUS FROM Dash_Plan_Batch_Details WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND  BATCH = '" & BATCH.Text & "'", "BSPIDB", DTGINVOICESSTATUS)

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try


        For Each row As DataGridViewRow In DTGINVOICESSTATUS.Rows

            row.Height = 40

        Next

        Try

            TOTALINVOICED.Text = DTGINVOICESSTATUS.Rows.Count()

            Dim TOTDEL As Integer
            Dim TOTINV As Integer

            TOTDEL = CHECKID.Text
            TOTINV = TOTALINVOICED.Text

            STATUSPERCENTAGE.Text = (TOTDEL / TOTINV) * 100

        Catch ex As Exception

        End Try

        Try

            STATUSPERCENTAGE.Text = Decimal.Parse(STATUSPERCENTAGE.Text).ToString("n2") & "%"

        Catch ex As Exception

        End Try

        GunaAdvenceButton1.PerformClick()

    End Sub

    Private Sub DTGINVOICESSTATUS_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGINVOICESSTATUS.CellClick

        Try

            '   Transaction_Results_Show_Payments.INVOICENUMBER.Text = DTGINVOICESSTATUS.CurrentRow.Cells(0).Value

            '  Transaction_Results_Show_Payments.ShowDialog()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GunaLabel2_Click(sender As Object, e As EventArgs) Handles GunaLabel2.Click

        Close()

    End Sub

    Private Sub COLLECTIONAMOUNT_TextChanged(sender As Object, e As EventArgs) Handles COLLECTIONAMOUNT.TextChanged

        Try

            COLLECTIONAMOUNT.Text = Decimal.Parse(COLLECTIONAMOUNT.Text).ToString("n2")

        Catch ex As Exception

        End Try

    End Sub

    Private Sub CMBSTATUS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMBSTATUS.SelectedIndexChanged

        If CMBSTATUS.Text = "FOR DELIVERY" Then

            Try

                views1("SELECT INVOICE_NUMBER AS INV_NUMBER,CUSTOMER_ID AS CU_ID,CUSTOMER_NAME AS CUSTOMER,TOTAL_AMOUNT AS TOTAL,DISTANCE,STATUS FROM Dash_Plan_Batch_Details WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND  BATCH = '" & BATCH.Text & "' AND STATUS = 'FOR DELIVERY'", "BSPIDB", DTGINVOICESSTATUS)

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try


            For Each row As DataGridViewRow In DTGINVOICESSTATUS.Rows

                row.Height = 40 ' Set the desired height for all rows

            Next

            TOTALLINES.Text = DTGINVOICESSTATUS.Rows.Count()

        ElseIf CMBSTATUS.Text = "DELIVERED" Then

            Try

                views1("SELECT INVOICE_NUMBER AS INV_NUMBER,CUSTOMER_ID AS CU_ID,CUSTOMER_NAME AS CUSTOMER,TOTAL_AMOUNT AS TOTAL,DISTANCE,STATUS FROM Dash_Plan_Batch_Details WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND  BATCH = '" & BATCH.Text & "' AND STATUS = 'DELIVERED'", "BSPIDB", DTGINVOICESSTATUS)

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try


            For Each row As DataGridViewRow In DTGINVOICESSTATUS.Rows

                row.Height = 40 ' Set the desired height for all rows

            Next

            TOTALLINES.Text = DTGINVOICESSTATUS.Rows.Count()

        ElseIf CMBSTATUS.Text = "FAILED" Then

            Try

                views1("SELECT INVOICE_NUMBER AS INV_NUMBER,CUSTOMER_ID AS CU_ID,CUSTOMER_NAME AS CUSTOMER,TOTAL_AMOUNT AS TOTAL,DISTANCE,STATUS FROM Dash_Plan_Batch_Details WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND  BATCH = '" & BATCH.Text & "' AND STATUS = 'FAILED'", "BSPIDB", DTGINVOICESSTATUS)

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try


            For Each row As DataGridViewRow In DTGINVOICESSTATUS.Rows

                row.Height = 40 ' Set the desired height for all rows

            Next


            TOTALLINES.Text = DTGINVOICESSTATUS.Rows.Count()


        ElseIf CMBSTATUS.Text = "VERIFIED" Then


            Try

                views1("SELECT INVOICE_NUMBER AS INV_NUMBER,CUSTOMER_ID AS CU_ID,CUSTOMER_NAME AS CUSTOMER,TOTAL_AMOUNT AS TOTAL,DISTANCE,STATUS FROM Dash_Plan_Batch_Details WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND  BATCH = '" & BATCH.Text & "' AND STATUS = 'VERIFIED'", "BSPIDB", DTGINVOICESSTATUS)

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try


            For Each row As DataGridViewRow In DTGINVOICESSTATUS.Rows

                row.Height = 40 ' Set the desired height for all rows

            Next


            TOTALLINES.Text = DTGINVOICESSTATUS.Rows.Count()

        Else

            Try

                views1("SELECT INVOICE_NUMBER AS INV_NUMBER,CUSTOMER_ID AS CU_ID,CUSTOMER_NAME AS CUSTOMER,TOTAL_AMOUNT AS TOTAL,DISTANCE,STATUS FROM Dash_Plan_Batch_Details WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND  BATCH = '" & BATCH.Text & "'", "BSPIDB", DTGINVOICESSTATUS)

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try


            For Each row As DataGridViewRow In DTGINVOICESSTATUS.Rows

                row.Height = 40 ' Set the desired height for all rows

            Next

            TOTALLINES.Text = DTGINVOICESSTATUS.Rows.Count()

        End If

    End Sub

    Private Async Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click
        Try
            Await WebView21.EnsureCoreWebView2Async(Nothing)
        Catch ex As Exception
            MsgBox("Error initializing WebView2: " & ex.Message)
            Return
        End Try

        DTGROUTES.DataSource = Nothing

        Try
            ' Load route data to DTGROUTES
            views3("SELECT BATCH, DATE_TO_DELIVER, STORE_LAT, STORE_LONG, CUSTOMER_ID, CUSTOMER_NAME " &
           "FROM Dash_Plan_Batch_Details " &
           "WHERE DATE_TO_DELIVER = '" & DTDELIVER.Value & "'  " &
           "AND COMPANY_ID = '" & COMPANYID.Text & "' AND BATCH = '" & BATCH.Text & "'" &
           "AND SITE_ID = '" & SITEID.Text & "'", "BSPIDB", DTGROUTES)

            ' Build pushpin JS from route data
            Dim htmlContent As New System.Text.StringBuilder()
            htmlContent.AppendLine("<html><head><title>Bing Map</title>")
            htmlContent.AppendLine("<script src='https://www.bing.com/api/maps/mapcontrol?key=u8bVA0NAuVbJ5r3MZgUR~fk8RCjst0j6nqTGJqGeK6w~Aj-CdHthPN5zeNnT6yh7YYRy1RGcZ3--xAUdQNkjP7_5t7ysVrqpNUvA6QYV-fYC'></script>")
            htmlContent.AppendLine("<script>")
            htmlContent.AppendLine("var map, agentPin, agentPolyline;")
            htmlContent.AppendLine("var routePins = [];")
            htmlContent.AppendLine("var agentPath = [];")
            htmlContent.AppendLine("var storePushpins = [];")

            ' Bounds
            Dim minLat As Double = Double.MaxValue
            Dim maxLat As Double = Double.MinValue
            Dim minLng As Double = Double.MaxValue
            Dim maxLng As Double = Double.MinValue

            For Each row As DataGridViewRow In DTGROUTES.Rows
                If Not row.IsNewRow Then
                    Dim lat = Convert.ToDouble(row.Cells("STORE_LAT").Value)
                    Dim lng = Convert.ToDouble(row.Cells("STORE_LONG").Value)
                    Dim batch = row.Cells("BATCH").Value.ToString().Replace("'", "\'")
                    Dim cust = row.Cells("CUSTOMER_NAME").Value.ToString().Replace("'", "\'")

                    htmlContent.AppendLine($"routePins.push({{ lat: {lat}, lng: {lng}, label: '{cust}' }});")

                    minLat = Math.Min(minLat, lat)
                    maxLat = Math.Max(maxLat, lat)
                    minLng = Math.Min(minLng, lng)
                    maxLng = Math.Max(maxLng, lng)
                End If
            Next

            ' Get AGENT trace from DB
            Dim con As New SqlConnection("Data Source=bspidbservernew.database.windows.net;Network Library=DBMSSOCN;Initial Catalog=BSPIDBNEW;User ID=sqladmin;Password=b$p1.@dm1n;")
            Dim cmd As New SqlCommand("SELECT LAT_CAPTURED, LONG_CAPTURED, TIME_STAMP FROM Dash_Agent_Time_Stamp " &
                                  "WHERE DELIVERY_DATE = @Date AND COMPANY_ID = @Comp AND SITE_ID = @Site AND AGENT_ID = @Vehicle ORDER BY TIME_STAMP", con)
            cmd.Parameters.AddWithValue("@Date", DTDELIVER.Value)
            cmd.Parameters.AddWithValue("@Comp", COMPANYID.Text)
            cmd.Parameters.AddWithValue("@Site", SITEID.Text)
            cmd.Parameters.AddWithValue("@Vehicle", VEHICLEID.Text)
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)

            For Each row As DataRow In dt.Rows
                htmlContent.AppendLine($"agentPath.push({{ lat: {row("LAT_CAPTURED")}, lng: {row("LONG_CAPTURED")}, time: '{row("TIME_STAMP")}' }});")
            Next

            ' JavaScript functions
            htmlContent.AppendLine("
    function getDistanceMeters(loc1, loc2) {
        var R = 6371000;
        var lat1 = loc1.latitude * Math.PI / 180;
        var lat2 = loc2.latitude * Math.PI / 180;
        var deltaLat = (loc2.latitude - loc1.latitude) * Math.PI / 180;
        var deltaLng = (loc2.longitude - loc1.longitude) * Math.PI / 180;

        var a = Math.sin(deltaLat / 2) * Math.sin(deltaLat / 2) +
                Math.cos(lat1) * Math.cos(lat2) *
                Math.sin(deltaLng / 2) * Math.sin(deltaLng / 2);
        var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
        return R * c;
    }

    function initMap() {
        map = new Microsoft.Maps.Map('#myMap', {
            center: new Microsoft.Maps.Location(13.7563, 100.5018),
            zoom: 10
        });

        var locations = [];

        for (var i = 0; i < routePins.length; i++) {
            var loc = new Microsoft.Maps.Location(routePins[i].lat, routePins[i].lng);
            var pin = new Microsoft.Maps.Pushpin(loc, { color: 'gray', title: routePins[i].label });
            map.entities.push(pin);
            storePushpins.push({ pin: pin, location: loc, visited: false });
            locations.push(loc);
        }

        if (agentPath.length > 0) {
            var firstLoc = new Microsoft.Maps.Location(agentPath[0].lat, agentPath[0].lng);
            agentPin = new Microsoft.Maps.Pushpin(firstLoc, { color: 'black', title: agentPath[0].time });
            map.entities.push(agentPin);
            document.getElementById('timeSlider').max = agentPath.length - 1;
            locations.push(firstLoc);
        }

        if (locations.length > 0) {
            var bounds = Microsoft.Maps.LocationRect.fromLocations(locations);
            map.setView({ bounds: bounds, padding: 80 });
        }
    }

    function updateAgentMarker(i) {
        if (!agentPath[i]) return;

        var loc = new Microsoft.Maps.Location(agentPath[i].lat, agentPath[i].lng);
        agentPin.setLocation(loc);
        agentPin.setOptions({ title: agentPath[i].time });

        if (agentPolyline) {
            map.entities.remove(agentPolyline);
            agentPolyline = null;
        }

        if (i > 0) {
            var partialPath = [];
            for (var j = 0; j <= i; j++) {
                partialPath.push(new Microsoft.Maps.Location(agentPath[j].lat, agentPath[j].lng));
            }
            agentPolyline = new Microsoft.Maps.Polyline(partialPath, { strokeColor: 'black', strokeThickness: 3 });
            map.entities.push(agentPolyline);
        }

        var agentLoc = new Microsoft.Maps.Location(agentPath[i].lat, agentPath[i].lng);
        storePushpins.forEach(function (store) {
            var distance = getDistanceMeters(agentLoc, store.location);
            if (!store.visited && distance <= 50) {
                store.pin.setOptions({ color: 'red' });
                store.visited = true;
            }
        });
    }

    window.onload = function () {
        initMap();
        var slider = document.getElementById('timeSlider');
        slider.addEventListener('input', function () {
            updateAgentMarker(parseInt(this.value));
        });
    }
    </script>
    </head>
    <body style='margin:0'>
        <div id='myMap' style='width:100%;height:90%;'></div>
        <input type='range' id='timeSlider' min='0' max='0' value='0' style='width:100%;position:absolute;bottom:10px;' />
    </body>
    </html>")

            WebView21.NavigateToString(htmlContent.ToString())
        Catch ex As Exception
            MsgBox("Error: " & ex.ToString())
        End Try
    End Sub


    Private Sub DTGAGENT_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGAGENT.CellContentClick

    End Sub
End Class