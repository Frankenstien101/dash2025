
Imports System.Data.SqlClient

Public Class Transaction_Agent_Monitoring

    Private Sub Transaction_Agent_Monitoring_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DTDELIVER.Value = Form1.DTCALENDAR.Value
        COMPANYID.Text = Form1.COMPANYID.Text
        SITEID.Text = Form1.SITEID.Text

        Try

            con.Open()

            Dim da As New SqlDataAdapter("select SUB_DA from Dash_Agents WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'ACTIVE' GROUP BY SUB_DA", con)

            Dim dt As New DataTable

            da.Fill(dt)

            CMBAGENT.DisplayMember = "SUB_DA"

            CMBAGENT.DataSource = dt

            con.Close()

        Catch ex As Exception

            '  MsgBox(ex.ToString)

        End Try

    End Sub

    Private Async Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click

        VEHICLEID.Text = "-"
        TOTALSTORES.Text = ""
        DISTANCETRAVELED.Text = ""
        FUELCINSUMED.Text = ""
        MARKETENTRY.Text = ""
        MARKETEXIT.Text = ""

        Dim DISTANCEORIG As Decimal
        Dim NEWDIS As Decimal


        Try
            Await WebView21.EnsureCoreWebView2Async(Nothing)
        Catch ex As Exception
            MsgBox("Error initializing WebView2: " & ex.Message)
            Return
        End Try

        DTGROUTES.DataSource = Nothing

        Try

            views2("WITH OrderedPoints AS (
                                SELECT 
                                    LAT_CAPTURED,
                                    LONG_CAPTURED,
                                    TIME_STAMP,
                                    geography::Point(LAT_CAPTURED, LONG_CAPTURED, 4326) AS GeoPoint,
                                    ROW_NUMBER() OVER (ORDER BY TIME_STAMP) AS RowNum
                                FROM Dash_Agent_Time_Stamp
                                WHERE DELIVERY_DATE = '" & DTDELIVER.Value & "' 
                                  AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' 
                                  AND SITE_ID = '" & Form1.SITEID.Text & "'
                                  AND AGENT_ID = '" & CMBAGENT.Text & "'
                            ),
                            PairedPoints AS (
                                SELECT 
                                    a.GeoPoint AS Point1,
                                    b.GeoPoint AS Point2
                                FROM OrderedPoints a
                                JOIN OrderedPoints b ON a.RowNum = b.RowNum - 1
                            )
                            SELECT 
                                SUM(Point1.STDistance(Point2)) AS TotalDistanceInMeters
                            FROM PairedPoints;

                            ", "BSPIDB", DTGROUTES)

            DISTANCETRAVELED.Text = DTGROUTES.CurrentRow.Cells(0).Value

            DISTANCEORIG = DISTANCETRAVELED.Text

            NEWDIS = DISTANCEORIG / 1000

            ' MsgBox(NEWDIS.ToString)

            DISTANCETRAVELED.Text = NEWDIS.ToString("F2") & "km"

        Catch ex As Exception

            '  MsgBox(ex.ToString)

        End Try

        '  MsgBox(NEWDIS.ToString)

        ' views3("", "BSPIDB", DTGROUTES)

        Try

            views3("SELECT TOP 1
                     [VEHICLE_ID]
            
                 FROM [dbo].[Dash_Plan_Batch_Transaction]
                 
                 WHERE AGENT = '" & CMBAGENT.Text & "' AND DATE_TO_DELIVER = '" & DTDELIVER.Value & "'", "BSPIDB", DTGROUTES)

            VEHICLEID.Text = DTGROUTES.CurrentRow.Cells(0).Value

        Catch ex As Exception

            '  MsgBox(ex.ToString)

        End Try


        Try

            views3("SELECT TOP 1
                     [CONSUMPTION_PER_100_METERS]
            
                 FROM [dbo].[Dash_Vehicles]
                 
                 WHERE PLATE_NUM = '" & VEHICLEID.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "'", "BSPIDB", DTGROUTES)

            LITERPER100METER.Text = DTGROUTES.CurrentRow.Cells(0).Value

        Catch ex As Exception

            '   MsgBox(ex.ToString)]]

            LITERPER100METER.Text = "0"

        End Try

        Dim CONSUMEDFUEL As Decimal
        Dim LITPER100METERS As Decimal
        Dim FINALCONSUME As Decimal

        LITPER100METERS = LITERPER100METER.Text

        NEWDIS = (NEWDIS * 1000) / 100

        ' MsgBox(NEWDIS.ToString)

        CONSUMEDFUEL = NEWDIS * LITPER100METERS

        '  MsgBox("CONSUMED FUEL:" & CONSUMEDFUEL.ToString)

        FINALCONSUME = CONSUMEDFUEL / 1000

        FUELCINSUMED.Text = FINALCONSUME.ToString("F2") & "L"

        Try

            views3("SELECT TOP 1
                     [DELIVERY_DATE]
                     ,[STORE_CODE]
                     ,[STORE_NAME]
                     ,[STORE_EXIT]

                 FROM [dbo].[Dash_Agent_Performance_Detailed]

                 LEFT JOIN Dash_Plan_Batch_Transaction ON Dash_Plan_Batch_Transaction.BATCH_ID = Dash_Agent_Performance_Detailed.BATCH_ID
                 
                 WHERE STORE_EXIT != '00:00:00'  AND AGENT = '" & CMBAGENT.Text & "' AND DELIVERY_DATE = '" & DTDELIVER.Value & "'

                 ORDER BY STORE_EXIT DESC", "BSPIDB", DTGROUTES)

            MARKETEXIT.Text = DTGROUTES.CurrentRow.Cells(3).Value

        Catch ex As Exception


            MARKETEXIT.Text = "0"
            ' MsgBox(ex.ToString)

        End Try


        Try
            ' Load route data to DTGROUTES
            views3("SELECT BATCH, DATE_TO_DELIVER, STORE_LAT, STORE_LONG, CUSTOMER_ID, CUSTOMER_NAME " &
           "FROM Dash_Plan_Batch_Details " &
           "WHERE DATE_TO_DELIVER = '" & DTDELIVER.Value & "'  " &
           "AND COMPANY_ID = '" & COMPANYID.Text & "' AND AGENT_ID = '" & CMBAGENT.Text & "'" &
           "AND SITE_ID = '" & SITEID.Text & "'", "BSPIDB", DTGROUTES)

            TOTALSTORES.Text = DTGROUTES.Rows.Count()

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
                                  "WHERE DELIVERY_DATE = @Date AND COMPANY_ID = @Comp AND SITE_ID = @Site AND AGENT_ID = @Vehicle AND CAST([GPS_ACCURACY] AS DECIMAL(10, 2)) !> 5.0 ORDER BY TIME_STAMP", con)
            cmd.Parameters.AddWithValue("@Date", DTDELIVER.Value)
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

    Private Sub DTDELIVER_ValueChanged(sender As Object, e As EventArgs) Handles DTDELIVER.ValueChanged

        VEHICLEID.Text = "-"

    End Sub

    Private Sub CMBAGENT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMBAGENT.SelectedIndexChanged

        VEHICLEID.Text = "-"

    End Sub

End Class