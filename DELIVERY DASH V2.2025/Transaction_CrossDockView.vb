
Imports System.Data.SqlClient

Public Class Transaction_CrossDockView
    Private Sub Transaction_CrossDockView_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        COMPANYID.Text = Form1.GETCOMPANYID
        SITEID.Text = Form1.GETSITEID
        DTDELIVERY.Value = Form1.GETDATE

        VEHICLE.Text = ""
        DEPARTURETIME.Text = ""
        ARRIVALTIME.Text = ""
        STATUS.Text = ""

    End Sub

    Private Sub DTDELIVERY_ValueChanged(sender As Object, e As EventArgs) Handles DTDELIVERY.ValueChanged

        VEHICLE.Text = ""
        DEPARTURETIME.Text = ""
        ARRIVALTIME.Text = ""
        STATUS.Text = ""


        Try

            views("SELECT BATCH,AGENT_ID 
                FROM Dash_Plan_Batch_Details
            WHERE COMPANY_ID = '" & Form1.GETCOMPANYID & "' AND SITE_ID = '" & Form1.GETSITEID & "' AND DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND SUB_BATCH != '' GROUP BY BATCH,AGENT_ID ", "BSPIDB", DTGDATA)

        Catch ex As Exception

            '   MsgBox(ex.ToString)

        End Try


    End Sub

    Private Sub DTGDATA_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGDATA.CellClick

        VEHICLE.Text = ""
        DEPARTURETIME.Text = ""
        ARRIVALTIME.Text = ""
        STATUS.Text = ""


        BATCH.Text = DTGDATA.CurrentRow.Cells(0).Value
        CMBAGENT.Text = DTGDATA.CurrentRow.Cells(1).Value

        Try

            views1("SELECT SUB_BATCH,SUB_DA AS SUB_AGENT,VEHICLE_IDS AS SUB_VEHICLE
                FROM Dash_Plan_Batch_Details
            WHERE COMPANY_ID = '" & Form1.GETCOMPANYID & "' AND SITE_ID = '" & Form1.GETSITEID & "' AND DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND BATCH = '" & BATCH.Text & "' GROUP BY SUB_BATCH,SUB_DA,VEHICLE_IDS ", "BSPIDB", DTGCROSSDOCK)

        Catch ex As Exception

            '  MsgBox(ex.ToString)

        End Try

        Try

            views3("SELECT VEHICLE,DEPARTURE_TIME,ARRIVAL_TIME,STATUS
                FROM Dash_XDock_Status
            WHERE COMPANY_ID = '" & Form1.GETCOMPANYID & "' AND SITE_ID = '" & Form1.GETSITEID & "'AND BATCH = '" & BATCH.Text & "'", "BSPIDB", DTGROUTES)

            VEHICLE.Text = DTGROUTES.CurrentRow.Cells(0).Value
            DEPARTURETIME.Text = DTGROUTES.CurrentRow.Cells(1).Value
            ARRIVALTIME.Text = DTGROUTES.CurrentRow.Cells(2).Value
            STATUS.Text = DTGROUTES.CurrentRow.Cells(3).Value

        Catch ex As Exception

            '  MsgBox(ex.ToString)

        End Try

        Guna2Button1.PerformClick()

    End Sub

    Private Sub DTGCROSSDOCK_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGCROSSDOCK.CellClick

        SUBBATCH.Text = DTGCROSSDOCK.CurrentRow.Cells(0).Value

        Try

            views2("SELECT CUSTOMER_ID,CUSTOMER_NAME,INVOICE_NUMBER,TOTAL_AMOUNT,INVOICE_VOLUME 
                FROM Dash_Plan_Batch_Details
            WHERE COMPANY_ID = '" & Form1.GETCOMPANYID & "' AND SITE_ID = '" & Form1.GETSITEID & "' AND DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND SUB_BATCH = '" & SUBBATCH.Text & "' ", "BSPIDB", DTGINVOICES)

        Catch ex As Exception

            ' MsgBox(ex.ToString)

        End Try

    End Sub

    Private Async Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click

        Try
            Await WebView21.EnsureCoreWebView2Async(Nothing)
        Catch ex As Exception
            MsgBox("Error initializing WebView2: " & ex.Message)
            Return
        End Try

        DTGROUTES.DataSource = Nothing

        Try
            ' Load route data to DTGROUTES
            views3("SELECT LATITUDE, LONGITUDE, ADDRESS, TYPE " &
           "FROM Dash_Coordinates_XD_MW " &
           "WHERE TYPE != ''  " &
           "AND COMPANY_ID = '" & COMPANYID.Text & "' AND STATUS = '1' " &
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
                    Dim lat = Convert.ToDouble(row.Cells("LATITUDE").Value)
                    Dim lng = Convert.ToDouble(row.Cells("LONGITUDE").Value)
                    Dim batch = row.Cells("TYPE").Value.ToString().Replace("'", "\'")
                    Dim cust = row.Cells("ADDRESS").Value.ToString().Replace("'", "\'").Replace(",", "").Replace(" ", "_")

                    htmlContent.AppendLine($"routePins.push({{ lat: {lat}, lng: {lng}, label: '{batch}' }});")

                    minLat = Math.Min(minLat, lat)
                    maxLat = Math.Max(maxLat, lat)
                    minLng = Math.Min(minLng, lng)
                    maxLng = Math.Max(maxLng, lng)
                End If

            Next

            ' Get AGENT trace from DB
            Dim con As New SqlConnection("Data Source=bspidbservernew.database.windows.net;Network Library=DBMSSOCN;Initial Catalog=BSPIDBNEW;User ID=sqladmin;Password=b$p1.@dm1n;")
            Dim cmd As New SqlCommand("SELECT LAT_CAPTURED, LONG_CAPTURED, TIME_STAMP FROM Dash_Agent_Time_Stamp " &
                                  "WHERE DELIVERY_DATE = @Date AND COMPANY_ID = @Comp AND SITE_ID = @Site AND AGENT_ID = @Vehicle AND CAST([GPS_ACCURACY] AS DECIMAL(10, 2)) !> 5.0 ORDER BY TIME_STAMP", con)
            cmd.Parameters.AddWithValue("@Date", DTDELIVERY.Value)
            cmd.Parameters.AddWithValue("@Comp", COMPANYID.Text)
            cmd.Parameters.AddWithValue("@Site", SITEID.Text)
            cmd.Parameters.AddWithValue("@Vehicle", CMBAGENT.Text)
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

    Private Sub DTGDATA_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGDATA.CellContentClick

    End Sub
End Class