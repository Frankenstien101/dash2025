Imports System.ComponentModel

Public Class Transactions_Order_Preparation_XDock
    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click

        Close()

    End Sub

    Private Async Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click
        If DTGDATA.Columns.Contains("Cluster Group") Then
            DTGDATA.Columns.Remove("Cluster Group")
        End If

        BTNCONFIRM.Enabled = False
        Await WebView21.EnsureCoreWebView2Async()

        If DTGDATA.Rows.Count = 0 Then
            MsgBox("NO ORDERS TO PROCESS", MsgBoxStyle.Exclamation, "SORRY")
            Return
        End If

        Try
            views2("SELECT CUSTOMER_ID,CUSTOMER_NAME,STORE_LAT,STORE_LONG FROM Dash_SO_Plan_Batch_Details " &
               "WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text &
               "' AND SO_PLAN_NUMBER = '" & BATCHNUMBER.Text & "' GROUP BY CUSTOMER_ID,CUSTOMER_NAME,STORE_LAT,STORE_LONG", "BSPIDB", DTGDATA)
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
            Return
        End Try

        Try
            views4("SELECT LATITUDE, LONGITUDE, TYPE FROM Dash_Coordinates_XD_MW " &
               "WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = '1'", "BSPIDB", DTGWAREHOUSE)
        Catch ex As Exception
            MessageBox.Show("Error loading warehouse data: " & ex.Message)
            Return
        End Try

        Try
            Dim K As Integer
            If Not Integer.TryParse(NUMOFCLUSTER.Text, K) OrElse K < 1 Then
                MessageBox.Show("Please enter a valid number of clusters.")
                Return
            End If

            Dim points As New List(Of Tuple(Of Double, Double))()
            For Each row As DataGridViewRow In DTGDATA.Rows
                If Not row.IsNewRow Then
                    Dim latVal = row.Cells("STORE_LAT").Value
                    Dim lonVal = row.Cells("STORE_LONG").Value
                    If latVal IsNot Nothing AndAlso lonVal IsNot Nothing Then
                        points.Add(Tuple.Create(Convert.ToDouble(latVal), Convert.ToDouble(lonVal)))
                    End If
                End If
            Next

            If points.Count < K Then
                MessageBox.Show("Number of clusters exceeds number of points.")
                Return
            End If

            Dim rand As New Random()
            Dim centroids As New List(Of Tuple(Of Double, Double))()
            While centroids.Count < K
                Dim p = points(rand.Next(points.Count))
                If Not centroids.Contains(p) Then centroids.Add(p)
            End While

            Dim assignments(points.Count - 1) As Integer
            Dim changed = True
            Dim maxIter = 100
            Dim iter = 0

            While changed AndAlso iter < maxIter
                iter += 1
                changed = False

                For i = 0 To points.Count - 1
                    Dim minDist = Double.MaxValue
                    Dim bestCluster = 0
                    For j = 0 To K - 1
                        Dim d = Distance(points(i).Item1, points(i).Item2, centroids(j).Item1, centroids(j).Item2)
                        If d < minDist Then
                            minDist = d
                            bestCluster = j
                        End If
                    Next
                    If assignments(i) <> bestCluster Then
                        assignments(i) = bestCluster
                        changed = True
                    End If
                Next

                Dim newCentroids As New List(Of Tuple(Of Double, Double))(New Tuple(Of Double, Double)(K - 1) {})
                Dim counts(K - 1) As Integer

                For i = 0 To points.Count - 1
                    Dim cid = assignments(i)
                    If newCentroids(cid) Is Nothing Then
                        newCentroids(cid) = Tuple.Create(0.0, 0.0)
                    End If
                    newCentroids(cid) = Tuple.Create(newCentroids(cid).Item1 + points(i).Item1, newCentroids(cid).Item2 + points(i).Item2)
                    counts(cid) += 1
                Next

                For j = 0 To K - 1
                    If counts(j) > 0 Then
                        centroids(j) = Tuple.Create(newCentroids(j).Item1 / counts(j), newCentroids(j).Item2 / counts(j))
                    Else
                        centroids(j) = points(rand.Next(points.Count))
                    End If
                Next
            End While

            Dim centerLat = points(0).Item1
            Dim centerLon = points(0).Item2
            Dim html As New System.Text.StringBuilder()

            html.AppendLine("<!DOCTYPE html><html><head>")
            html.AppendLine("<meta charset='utf-8'/>")
            html.AppendLine("<title>Map</title>")
            html.AppendLine("<link rel='stylesheet' href='https://unpkg.com/leaflet/dist/leaflet.css'/>")
            html.AppendLine("<script src='https://unpkg.com/leaflet/dist/leaflet.js'></script>")
            html.AppendLine("</head><body><div id='map' style='width:100%; height:600px;'></div>")
            html.AppendLine("<script>")
            html.AppendLine("var map = L.map('map').setView([" & centerLat & ", " & centerLon & "], 9);")
            html.AppendLine("L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { attribution: '© OpenStreetMap', maxZoom: 20 }).addTo(map);")
            html.AppendLine("var colors = ['red','blue','green','orange','purple','cyan','lime','brown','black','magenta'];")

            For i = 0 To points.Count - 1
                Dim lat = points(i).Item1
                Dim lon = points(i).Item2
                Dim cid = assignments(i)
                Dim storename = DTGDATA.Rows(i).Cells("CUSTOMER_NAME").Value.ToString()
                html.AppendLine($"L.circleMarker([{lat}, {lon}], {{ color: colors[{cid}], fillColor: colors[{cid}], fillOpacity: 0.6, radius: 8 }}).addTo(map).bindPopup('{storename.Replace("'", "\'")} (Cluster {cid + 1})');")
            Next

            html.AppendLine("var warehouseIcon = L.icon({ iconUrl: 'https://cdn-icons-png.flaticon.com/512/149/149060.png', iconSize: [30, 30], iconAnchor: [15, 30], popupAnchor: [0, -30] });")

            For Each row As DataGridViewRow In DTGWAREHOUSE.Rows
                Dim latW = Convert.ToDouble(row.Cells("LATITUDE").Value)
                Dim lonW = Convert.ToDouble(row.Cells("LONGITUDE").Value)
                Dim typeW = row.Cells("TYPE").Value.ToString()
                html.AppendLine($"L.marker([{latW}, {lonW}], {{ icon: warehouseIcon }}).addTo(map).bindPopup('Warehouse ({typeW})');")
            Next

            html.AppendLine("var legend = L.control({position: 'topleft'});")
            html.AppendLine("legend.onAdd = function (map) {")
            html.AppendLine("var div = L.DomUtil.create('div', 'info legend');")
            html.AppendLine("div.style.backgroundColor = 'white'; div.style.padding = '10px'; div.style.border = '1px solid gray'; div.style.borderRadius = '5px'; div.style.fontSize = '13px';")
            html.AppendLine("div.innerHTML += '<b>Clusters</b><br/>';")

            ' === Begin: Add cluster store counts ===
            Dim clusterCounts As New Dictionary(Of Integer, Integer)
            For Each cid In assignments
                If clusterCounts.ContainsKey(cid) Then
                    clusterCounts(cid) += 1
                Else
                    clusterCounts(cid) = 1
                End If
            Next

            For Each cid In clusterCounts.Keys
                Dim color = GetColorName(cid)
                Dim count = clusterCounts(cid)
                html.AppendLine($"div.innerHTML += '<i style=""background:{color};width:12px;height:12px;display:inline-block;margin-right:5px;""></i> Cluster {cid + 1} ({count} stores)<br/>';")
            Next
            ' === End: Add cluster store counts ===

            html.AppendLine("return div;")
            html.AppendLine("};")
            html.AppendLine("legend.addTo(map);")
            html.AppendLine("</script></body></html>")

            WebView21.NavigateToString(html.ToString())

            If Not DTGDATA.Columns.Contains("Cluster Group") Then
                DTGDATA.Columns.Add("Cluster Group", "Cluster Group")
                DTGDATA.Columns("Cluster Group").DisplayIndex = DTGDATA.Columns.Count - 1
            End If

            For i = 0 To points.Count - 1
                DTGDATA.Rows(i).Cells("Cluster Group").Value = assignments(i) + 1
            Next

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        BTNCONFIRM.Enabled = True
    End Sub



    Function Distance(lat1 As Double, lon1 As Double, lat2 As Double, lon2 As Double) As Double
        Dim R As Double = 6371 ' Earth radius in km
        Dim dLat = (lat2 - lat1) * Math.PI / 180
        Dim dLon = (lon2 - lon1) * Math.PI / 180
        Dim a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
        Dim c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a))
        Return R * c
    End Function



    Private Function HaversineDistance(lat1 As Double, lon1 As Double, lat2 As Double, lon2 As Double) As Double
        Dim R As Double = 6371 ' Earth radius in kilometers
        Dim dLat = DegreesToRadians(lat2 - lat1)
        Dim dLon = DegreesToRadians(lon2 - lon1)
        Dim a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
        Dim c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a))
        Return R * c
    End Function

    Private Function DegreesToRadians(deg As Double) As Double
        Return deg * (Math.PI / 180)
    End Function

    Private Function GetColorName(index As Integer) As String
        Dim colors = {"red", "blue", "green", "orange", "purple", "cyan", "lime", "brown", "black", "magenta"}
        If index >= 0 AndAlso index < colors.Length Then
            Return colors(index)
        End If
        Return "gray"
    End Function

    Private Sub Transactions_Order_Preparation_XDock_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        BTNCONFIRM.Enabled = False

        Try

            views2("SELECT CUSTOMER_ID,CUSTOMER_NAME,SO_NUMBER,TOTAL_AMOUNT,STORE_LAT,STORE_LONG FROM Dash_SO_Plan_Batch_Details WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND SO_PLAN_NUMBER = '" & BATCHNUMBER.Text & "' ", "BSPIDB", DTGDATA)

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        For i = 0 To DTGDATA.Rows.Count - 1 Step +1

            If DTGDATA.Rows.Count > 1 Then

                ' MsgBox("more")

                Dim x As Double
                '  Dim y As Double
                Dim z As Double
                Dim a As Double

                x = DTGDATA.Rows.Count - 1

                z = i / x

                a = z * 100

                Dim V1 As Double

                V1 = a

                GunaProgressBar1.Value = a
                lblprocess.Text = " Routing is processing. . . "

            Else

                ''  MsgBox("one only")

                lblprocess.Text = "Saving details....."
                GunaProgressBar1.Value = 100

            End If

            Try

                CLUSTER.Text = DTGDATA.Rows(i).Cells("Cluster Group").Value
                STORECODE.Text = DTGDATA.Rows(i).Cells("CUSTOMER_ID").Value

            Catch ex As Exception

            End Try

            Try


                Dim L As String = "UPDATE Dash_SO_Plan_Batch_Details SET SUB_BATCH = '" & BATCHNUMBER.Text & "-0" & CLUSTER.Text & "' WHERE SO_PLAN_NUMBER = '" & BATCHNUMBER.Text & "' AND CUSTOMER_ID = '" & STORECODE.Text & "'"

                Data(L)


            Catch ex As Exception

            End Try

        Next

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        Me.Enabled = True

        BTNCONFIRM.Enabled = False
        ' lblprocess.Text = "Routing Complete"
        MsgBox("ROUTING COMPLETE, PLEASE ASSIGN VEHICLE AND SUB AGENT PER SO PLAN NUMBER", MsgBoxStyle.Information, "COMPLETE")

        Try

            views2("SELECT SUB_BATCH, COUNT(CUSTOMER_ID) AS NUM_OF_INV FROM Dash_SO_Plan_Batch_Details WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND SO_PLAN_NUMBER = '" & BATCHNUMBER.Text & "' GROUP BY CLUSTER,SUB_BATCH", "BSPIDB", DTGBATCH)

        Catch ex As Exception

            MsgBox("2" & ex.ToString)

        End Try

        Try
            ' Step 1: Load vehicle IDs into a DataTable
            Dim dtVehicles As New DataTable()
            Using con As New SqlClient.SqlConnection("Data Source=bspidbservernew.database.windows.net;Network Library=DBMSSOCN;Initial Catalog=BSPIDBNEW;User ID=sqladmin;Password=b$p1.@dm1n;")
                con.Open()
                Using cmd As New SqlClient.SqlCommand("SELECT PLATE_NUM FROM Dash_Vehicles WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'ACTIVE'", con)
                    Dim adapter As New SqlClient.SqlDataAdapter(cmd)
                    adapter.Fill(dtVehicles)
                End Using
            End Using

            ' Step 2: Ensure VEHICLE_ID column is removed if it exists (to replace with combo)
            If DTGBATCH.Columns.Contains("VEHICLE_ID") Then
                DTGBATCH.Columns.Remove("VEHICLE_ID")
            End If

            ' Step 3: Add the VEHICLE_ID combo box column
            Dim cmbColumn As New DataGridViewComboBoxColumn()
            cmbColumn.Name = "VEHICLE_ID"
            cmbColumn.HeaderText = "VEHICLE_ID"
            cmbColumn.DataSource = dtVehicles
            cmbColumn.DisplayMember = "PLATE_NUM"
            cmbColumn.ValueMember = "PLATE_NUM"
            cmbColumn.DataPropertyName = "PLATE_NUM" ' Bind to the data

            DTGBATCH.Columns.Add(cmbColumn)

        Catch ex As Exception
            MsgBox("Error loading VEHICLE_ID dropdown: " & ex.ToString())
        End Try


        Try
            ' Step 1: Load vehicle IDs into a DataTable
            Dim DTAGENTS As New DataTable()

            Using con As New SqlClient.SqlConnection("Data Source=bspidbservernew.database.windows.net;Network Library=DBMSSOCN;Initial Catalog=BSPIDBNEW;User ID=sqladmin;Password=b$p1.@dm1n;")

                con.Open()

                Using cmd As New SqlClient.SqlCommand("SELECT SUB_DA FROM Dash_Agents WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'ACTIVE' AND AGENT_TYPE = 'SUB'", con)

                    Dim adapter As New SqlClient.SqlDataAdapter(cmd)
                    adapter.Fill(DTAGENTS)

                End Using

            End Using

            ' Step 2: Ensure VEHICLE_ID column is removed if it exists (to replace with combo)
            If DTGBATCH.Columns.Contains("AGENT") Then

                DTGBATCH.Columns.Remove("AGENT")

            End If

            ' Step 3: Add the VEHICLE_ID combo box column
            Dim cmbColumn As New DataGridViewComboBoxColumn()
            cmbColumn.Name = "AGENT"
            cmbColumn.HeaderText = "AGENT"
            cmbColumn.DataSource = DTAGENTS
            cmbColumn.DisplayMember = "SUB_DA"
            cmbColumn.ValueMember = "SUB_DA"
            cmbColumn.DataPropertyName = "SUB_DA" ' Bind to the data

            DTGBATCH.Columns.Add(cmbColumn)

            DTGBATCH.Columns(0).ReadOnly = True
            DTGBATCH.Columns(1).ReadOnly = True


        Catch ex As Exception

            MsgBox("Error loading AGENT dropdown: " & ex.ToString())

        End Try

    End Sub

    Private Sub BTNCONFIRM_Click(sender As Object, e As EventArgs) Handles BTNCONFIRM.Click

        Me.Enabled = False

        BackgroundWorker1.RunWorkerAsync()

    End Sub

    Private Sub DTGBATCH_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DTGBATCH.CellValueChanged

        If e.ColumnIndex >= 0 AndAlso DTGBATCH.Columns(e.ColumnIndex).Name = "VEHICLE_ID" Then

            Dim row = DTGBATCH.Rows(e.RowIndex)
            Dim selectedVehicle As String = row.Cells("VEHICLE_ID").Value.ToString()
            Dim soPlanNumber As String = row.Cells("SUB_BATCH").Value.ToString()

            ' Update DB
            Try
                Dim updateSql As String = "UPDATE Dash_SO_Plan_Batch_Details SET VEHICLE_IDS = '" & selectedVehicle & "' " &
                                      "WHERE SUB_BATCH = '" & soPlanNumber & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "'"
                Data(updateSql)
                ' Optional success feedback:
                ' MessageBox.Show("Vehicle updated for plan: " & soPlanNumber)
            Catch ex As Exception
                MessageBox.Show("Error saving VEHICLE_ID: " & ex.Message)
            End Try

        End If

        If e.ColumnIndex >= 0 AndAlso DTGBATCH.Columns(e.ColumnIndex).Name = "AGENT" Then
            Dim row = DTGBATCH.Rows(e.RowIndex)
            Dim selectedVehicle As String = row.Cells("AGENT").Value.ToString()
            Dim soPlanNumber As String = row.Cells("SUB_BATCH").Value.ToString()

            ' Update DB
            Try
                Dim updateSql As String = "UPDATE Dash_SO_Plan_Batch_Details SET SUB_DA = '" & selectedVehicle & "' " &
                                      "WHERE SUB_BATCH = '" & soPlanNumber & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "'"
                Data(updateSql)
                ' Optional success feedback:
                ' MessageBox.Show("Vehicle updated for plan: " & soPlanNumber)
            Catch ex As Exception
                MessageBox.Show("Error saving VEHICLE_ID: " & ex.Message)
            End Try

        End If


    End Sub
End Class