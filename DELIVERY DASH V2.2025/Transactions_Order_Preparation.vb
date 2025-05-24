
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.ComponentModel

Public Class Transactions_Order_Preparation

    Dim timenosep As String
    Private Sub DTORDER_ValueChanged(sender As Object, e As EventArgs) Handles DTORDER.ValueChanged

        COMPANYID.Text = Form1.GETCOMPANYID()
        SITEID.Text = Form1.GETSITEID()

        TOTALPLANTODAY.Text = "0"

        Try

            views2("SELECT BATCH_COUNT FROM Dash_SO_Count WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND DATE_ORDER = '" & DTORDER.Value & "'", "BSPIDB", DTGCHECK)
            TOTALPLANTODAY.Text = DTGCHECK.CurrentRow.Cells(0).Value

        Catch ex As Exception

            ' MsgBox("2" & ex.ToString)

        End Try

        Try

            Dim l As String
            l = DTTIMENOSEAPARATOR.Text

            l = l.Replace(":", "")
            l = l.Replace("AM", "")
            l = l.Replace("PM", "")
            l = l.Replace("am", "")
            l = l.Replace("pm", "")
            l = l.Replace(" ", "")

            timenosep = l

            ' MsgBox(timenosep.ToString)

        Catch ex As Exception

        End Try

        ISNOSTORE.Text = "NO"

        Dim OO As String

        Try

            views("SELECT CUSTOMER_ID,CUSTOMER_NAME,DASH_ORDER_ID,TOTAL_VALUE FROM PRFR_SO_UPLOAD_TRANSACTION WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND ORDER_DATE = '" & DTORDER.Value & "' AND IS_PLAN = '0' ", "BSPIDB", DTGINVOICES)
            TOTALINVOICE.Text = DTGINVOICES.Rows.Count()

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        Try

            views2("SELECT WAREHOUSE_LAT,WAREHOUSE_LONG,VEHICLE_LIMIT FROM Dash_Sites WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "'", "BSPIDB", DTGCHECK)
            LATFROM.Text = DTGCHECK.CurrentRow.Cells(0).Value
            LONGFROM.Text = DTGCHECK.CurrentRow.Cells(1).Value
            OO = DTGCHECK.CurrentRow.Cells(2).Value

            '  whlat.Text = LATFROM.Text
            '  whlong.Text = LONGFROM.Text

        Catch ex As Exception

        End Try

        Try

            views1("SELECT PLATE_NUM,BODY_TYPE,VOLUME_CAPACITY,AMOUNT_CAPACITY FROM Dash_Vehicles WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'ACTIVE' ORDER BY PRIORITY_COUNT ASC", "BSPIDB", DTGVEHICLES)

            TOTALVEHICLES.Text = DTGVEHICLES.Rows.Count()
            VEHICLEID.Text = DTGVEHICLES.CurrentRow.Cells(0).Value
            STORELIMIT.Text = DTGVEHICLES.CurrentRow.Cells(1).Value

            '  MsgBox(STORELIMIT.Text)

        Catch ex As Exception

        End Try

        Try

            Dim BATCHNUM As Integer

            views2("SELECT COUNT(COMPANY_ID) FROM Dash_SO_Plan_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "'", "BSPIDB", DTGCHECK)

            CHECKID.Text = DTGCHECK.CurrentRow.Cells(0).Value

            BATCHNUM = CHECKID.Text

            TIMENOSEPARATOR.Text = DTTIMENOSEAPARATOR.Text
            TIMENOSEPARATOR.Text.Replace(":", "")
            TIMENOSEPARATOR.Text.Replace(" ", "")
            TIMENOSEPARATOR.Text.Replace("PM", "")
            TIMENOSEPARATOR.Text.Replace("AM", "")
            TIMENOSEPARATOR.Text.Replace("pm", "")
            TIMENOSEPARATOR.Text.Replace("am", "")


            BATCHNUMBER.Text = "SOPLN" & COMPANYID.Text & SITEID.Text & timenosep & BATCHNUM + 1

        Catch ex As Exception

            '  MsgBox("1" & ex.ToString)

        End Try

    End Sub

    Private Sub Transactions_Order_Preparation_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Control.CheckForIllegalCrossThreadCalls = False

        COMPANYID.Text = Form1.GETCOMPANYID()
        SITEID.Text = Form1.GETSITEID()
        DTORDER.Value = Form1.DTCALENDAR.Value

        ISASYNCDONE.Text = "YES"

    End Sub

    Private Sub Transactions_Order_Preparation_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        COMPANYID.Text = Form1.GETCOMPANYID()
        SITEID.Text = Form1.GETSITEID()

        DTTIMENOSEAPARATOR.Text = Form1.GETTIME()

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

    Private Async Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click

        If DTGROUTES.Columns.Contains("Cluster Group") Then
            DTGROUTES.Columns.Remove("Cluster Group")
        End If

        ISASYNCDONE.Text = "NO"
        GunaAdvenceButton5.Enabled = False

        Await WebView21.EnsureCoreWebView2Async()

        If DTGINVOICES.Rows.Count = 0 Then
            MsgBox("NO ORDERS TO PROCESS", MsgBoxStyle.Exclamation, "SORRY")
            Return
        End If

        Try
            views2("
        SELECT CUSTOMER_ID,CUSTOMER_NAME,DASH_ORDER_ID,TOTAL_VALUE,LATITUDE,LONGITUDE
        FROM PRFR_SO_UPLOAD_TRANSACTION
        LEFT JOIN Dash_Customer_Master 
            ON Dash_Customer_Master.CODE = PRFR_SO_UPLOAD_TRANSACTION.CUSTOMER_ID 
            AND Dash_Customer_Master.COMPANY_ID = PRFR_SO_UPLOAD_TRANSACTION.COMPANY_ID
        WHERE PRFR_SO_UPLOAD_TRANSACTION.COMPANY_ID = '" & COMPANYID.Text & "' 
            AND SITE_ID = '" & SITEID.Text & "'  
            AND IS_PLAN = '0' 
            AND ORDER_DATE = '" & DTORDER.Value & "' AND LATITUDE IS NOT NULL AND LONGITUDE IS NOT NULL
        GROUP BY CUSTOMER_ID,CUSTOMER_NAME,DASH_ORDER_ID,TOTAL_VALUE,LATITUDE,LONGITUDE",
            "BSPIDB", DTGROUTES)
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
            Return
        End Try

        Try
            Dim K As Integer
            If Not Integer.TryParse(NUMOFCLUSTER.Text, K) OrElse K < 1 Then
                MessageBox.Show("Please enter a valid number of clusters.")
                Return
            End If

            Dim points As New List(Of Tuple(Of Double, Double))()
            For Each row As DataGridViewRow In DTGROUTES.Rows
                If Not row.IsNewRow Then
                    Dim latVal = row.Cells("LATITUDE").Value
                    Dim lonVal = row.Cells("LONGITUDE").Value
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
                If Not centroids.Contains(p) Then
                    centroids.Add(p)
                End If
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
                    newCentroids(cid) = Tuple.Create(
                    newCentroids(cid).Item1 + points(i).Item1,
                    newCentroids(cid).Item2 + points(i).Item2)
                    counts(cid) += 1
                Next

                For j = 0 To K - 1
                    If counts(j) > 0 Then
                        centroids(j) = Tuple.Create(
                        newCentroids(j).Item1 / counts(j),
                        newCentroids(j).Item2 / counts(j))
                    Else
                        centroids(j) = points(rand.Next(points.Count))
                    End If
                Next
            End While

            ' Count stores per cluster
            Dim clusterCounts As New Dictionary(Of Integer, Integer)()
            For Each cid In assignments
                If Not clusterCounts.ContainsKey(cid) Then
                    clusterCounts(cid) = 0
                End If
                clusterCounts(cid) += 1
            Next

            ' Build HTML map
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
                Dim storename = DTGROUTES.Rows(i).Cells("CUSTOMER_NAME").Value.ToString()
                html.AppendLine($"L.circleMarker([{lat}, {lon}], {{ color: colors[{cid}], fillColor: colors[{cid}], fillOpacity: 0.6, radius: 8 }}).addTo(map).bindPopup('{storename.Replace("'", "\'")} (Cluster {cid + 1})');")
            Next

            html.AppendLine("
var legend = L.control({position: 'topleft'});
legend.onAdd = function (map) {
    var div = L.DomUtil.create('div', 'info legend');
    div.style.backgroundColor = 'white';
    div.style.padding = '10px';
    div.style.border = '1px solid gray';
    div.style.borderRadius = '5px';
    div.style.fontSize = '13px';
    div.innerHTML += '<b>Clusters</b><br/>';
")

            Dim distinctClusters = assignments.Distinct().ToList()
            For Each cid In distinctClusters
                Dim color = GetColorName(cid)
                Dim count = clusterCounts(cid)
                html.AppendLine($"div.innerHTML += '<i style=""background:{color};width:12px;height:12px;display:inline-block;margin-right:5px;""></i> Cluster {cid + 1} ({count} stores)<br/>';")
            Next

            html.AppendLine("    return div;")
            html.AppendLine("};")
            html.AppendLine("legend.addTo(map);")
            html.AppendLine($"<!-- Timestamp: {DateTime.Now.Ticks} -->")
            html.AppendLine("</script></body></html>")

            WebView21.NavigateToString(html.ToString())

            If Not DTGROUTES.Columns.Contains("Cluster Group") Then
                DTGROUTES.Columns.Add("Cluster Group", "Cluster Group")
                DTGROUTES.Columns("Cluster Group").DisplayIndex = DTGROUTES.Columns.Count - 1
            End If

            For i = 0 To points.Count - 1
                DTGROUTES.Rows(i).Cells("Cluster Group").Value = assignments(i) + 1
            Next

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        ISASYNCDONE.Text = "YES"
        GunaAdvenceButton5.Enabled = True

    End Sub


    Private Function GetColorName(index As Integer) As String
        Dim colors = {"red", "blue", "green", "orange", "purple", "cyan", "lime", "brown", "black", "magenta"}
        If index >= 0 AndAlso index < colors.Length Then
            Return colors(index)
        End If
        Return "gray"
    End Function


    Private Sub GunaAdvenceButton5_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton5.Click

        If DTGROUTES.Rows.Count = 0 Then

            MsgBox("NO ROUTES TO PROCESS", MsgBoxStyle.Exclamation, "SORRY")

        Else

            If ISASYNCDONE.Text = "YES" Then

                Me.Enabled = False

                GunaAdvenceButton5.Enabled = False

                BackgroundWorker1.RunWorkerAsync()

            Else

                MsgBox("PLEASE RETRY FOR A FEW MOMENT", MsgBoxStyle.Exclamation, "SORRY")

            End If

        End If

    End Sub

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

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        For i = 0 To DTGROUTES.Rows.Count - 1 Step +1

            If DTGROUTES.Rows.Count > 1 Then

                ' MsgBox("more")

                Dim x As Double
                '  Dim y As Double
                Dim z As Double
                Dim a As Double

                x = DTGROUTES.Rows.Count - 1

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

                views3("SELECT TOTAL_CS,TOTAL_VALUE FROM PRFR_SO_UPLOAD_TRANSACTION WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND ORDER_ID = '" & DTGROUTES.Rows(i).Cells(2).Value & "' ", "BSPIDB", DTGCHECK)
                TOTALVOLUME.Text = DTGCHECK.CurrentRow.Cells(0).Value
                TOTALVALUE.Text = DTGCHECK.CurrentRow.Cells(1).Value

            Catch ex As Exception

                '   MsgBox(ex.ToString)
                TOTALVOLUME.Text = "0"
                TOTALVALUE.Text = "0"

            End Try

            Dim WHLAT As Double = Convert.ToDouble(LATFROM.Text)
            Dim WHLONG As Double = Convert.ToDouble(LONGFROM.Text)

            Dim CUSLAT As Double = Convert.ToDouble(DTGROUTES.Rows(i).Cells(4).Value)
            Dim CUSLONG As Double = Convert.ToDouble(DTGROUTES.Rows(i).Cells(5).Value)

            Dim DISTANCE2 As Decimal = CDec(HaversineDistance(WHLAT, WHLONG, CUSLAT, CUSLONG))

            Try

                Dim TOTVAL As Decimal

                TOTVAL = TOTALVALUE.Text

                Dim kf As String = "INSERT INTO Dash_SO_Plan_Batch_Details(COMPANY_ID,SITE_ID,SO_PLAN_NUMBER,SO_NUMBER,CUSTOMER_ID,CUSTOMER_NAME,TOTAL_AMOUNT,STORE_LAT,STORE_LONG,ORDER_DATE,STATUS,DISTANCE,CLUSTER)" _
                   & "VALUES('" & COMPANYID.Text & "'," _
                  & "'" & SITEID.Text & "'," _
                    & "'" & BATCHNUMBER.Text & "0" & DTGROUTES.Rows(i).Cells("Cluster Group").Value & "'," _
                        & "'" & DTGROUTES.Rows(i).Cells("DASH_ORDER_ID").Value & "'," _
                          & "'" & DTGROUTES.Rows(i).Cells("CUSTOMER_ID").Value & "'," _
                        & " '" & DTGROUTES.Rows(i).Cells("CUSTOMER_NAME").Value & "', " _
                        & "'" & TOTALVALUE.Text & "'," _
                                  & "'" & CUSLAT & "'," _
                                   & "'" & CUSLONG & "'," _
                                    & "'" & DTORDER.Value & "'," _
                                      & "'PLANNED'," _
                                       & "'" & DISTANCE2 & "'," _
                               & "'" & DTGROUTES.Rows(i).Cells(6).Value & "')"

                Data(kf)

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try

            Try

                Dim L As String = "UPDATE PRFR_SO_UPLOAD_TRANSACTION SET IS_PLAN = '1' WHERE DASH_ORDER_ID = '" & DTGROUTES.Rows(i).Cells(2).Value & "' AND COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "'"

                Data(L)

            Catch ex As Exception

                MsgBox("UPDATE FAILED : " & ex.ToString)

            End Try

        Next



        ' Me.Enabled = True

        Try

            views3("SELECT SO_PLAN_NUMBER,CLUSTER FROM Dash_SO_Plan_Batch_Details WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND ORDER_DATE = '" & DTORDER.Text & "' GROUP BY SO_PLAN_NUMBER,CLUSTER", "BSPIDB", DTGBATCH)

        Catch ex As Exception

            MsgBox("2" & ex.ToString)

        End Try
        '

        For i = 0 To DTGBATCH.Rows.Count - 1 Step +1


            ' VEHICLEID.Text = DTGVEHICLES.CurrentRow.Cells(0).Value

            Try

                Dim TOTVAL As Decimal
                Dim TOTDEC As Decimal

                TOTVAL = 0
                TOTDEC = 0

                Dim kf1 As String = "INSERT INTO Dash_SO_Plan_Transaction(COMPANY_ID,SITE_ID,SO_PLAN_NUMBER,DATE_SO,VEHICLE_ID,SO_PICK_BATCH,STATUS,CLUSTER)" _
                        & "VALUES('" & COMPANYID.Text & "'," _
                       & "'" & SITEID.Text & "'," _
                         & "'" & DTGBATCH.Rows(i).Cells("SO_PLAN_NUMBER").Value & "'," _
                             & "'" & DTORDER.Value & "'," _
                                & "''," _
                           & "'" & TOTALPLANTODAY.Text & "'," _
                             & "'PLANNED'," _
                                    & "'" & DTGBATCH.Rows(i).Cells("CLUSTER").Value & "')"

                Data(kf1)

                '  MsgBox("NEW VEH")

                '   MsgBox("SAVE TRANSACTION")

            Catch ex As Exception

                MsgBox("4" & ex.ToString)

            End Try

            '            DTGVEHICLES.SelectedRows +1

        Next

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        Me.Enabled = True
        lblprocess.Text = "Routing Complete"
        MsgBox("ROUTING COMPLETE, PLEASE ASSIGN VEHICLE PER SO PLAN NUMBER", MsgBoxStyle.Information, "COMPLETE")

        Try

            views3("SELECT CLUSTER,SO_PLAN_NUMBER FROM Dash_SO_Plan_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND DATE_SO = '" & DTORDER.Text & "'", "BSPIDB", DTGBATCH)

        Catch ex As Exception

            MsgBox("2" & ex.ToString)

        End Try

        Try
            ' Step 1: Load vehicle IDs into a DataTable
            Dim dtVehicles As New DataTable()
            Using con As New SqlClient.SqlConnection("Data Source=bspidbservernew.database.windows.net;Network Library=DBMSSOCN;Initial Catalog=BSPIDBNEW;User ID=sqladmin;Password=b$p1.@dm1n;")
                con.Open()
                Using cmd As New SqlClient.SqlCommand("SELECT PLATE_NUM FROM Dash_Vehicles WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'ACTIVE'", con)
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

        DTGBATCH.Columns(0).ReadOnly = True
        DTGBATCH.Columns(1).ReadOnly = True

    End Sub

    Private Sub GunaAdvenceButton4_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton4.Click

        Dim K As MsgBoxResult '

        K = MsgBox("ARE YOU SURE TO RE-PROCESS ORDERS?", MsgBoxStyle.YesNo, "CONFIRM")

        If K = MsgBoxResult.Yes Then

            Try

                Dim L As String = "  DELETE[Dash_SO_Plan_Batch_Details] WHERE SITE_ID = '" & SITEID.Text & "' AND ORDER_DATE = '" & DTORDER.Value & "'
                                     UPDATE[PRFR_SO_UPLOAD_TRANSACTION] SET IS_PLAN = '0' WHERE SITE_ID = '" & SITEID.Text & "' AND ORDER_DATE = '" & DTORDER.Value & "'
                                     DELETE[Dash_SO_Plan_Transaction] WHERE SITE_ID = '" & SITEID.Text & "' AND DATE_SO = '" & DTORDER.Value & "'"

                Data(L)

            Catch ex As Exception

            End Try

            COMPANYID.Text = Form1.GETCOMPANYID()
            SITEID.Text = Form1.GETSITEID()

            TOTALPLANTODAY.Text = "0"

            Try

                views2("SELECT BATCH_COUNT FROM Dash_SO_Count WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND DATE_ORDER = '" & DTORDER.Value & "'", "BSPIDB", DTGCHECK)
                TOTALPLANTODAY.Text = DTGCHECK.CurrentRow.Cells(0).Value

            Catch ex As Exception

                ' MsgBox("2" & ex.ToString)

            End Try

            Try

                Dim l As String
                l = DTTIMENOSEAPARATOR.Text

                l = l.Replace(":", "")
                l = l.Replace("AM", "")
                l = l.Replace("PM", "")
                l = l.Replace("am", "")
                l = l.Replace("pm", "")
                l = l.Replace(" ", "")

                timenosep = l

                ' MsgBox(timenosep.ToString)

            Catch ex As Exception

            End Try

            ISNOSTORE.Text = "NO"

            Dim OO As String

            Try

                views("SELECT CUSTOMER_ID,CUSTOMER_NAME,DASH_ORDER_ID,TOTAL_VALUE FROM PRFR_SO_UPLOAD_TRANSACTION WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND ORDER_DATE = '" & DTORDER.Value & "' AND IS_PLAN = '0' ", "BSPIDB", DTGINVOICES)
                TOTALINVOICE.Text = DTGINVOICES.Rows.Count()

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try

            Try

                views2("SELECT WAREHOUSE_LAT,WAREHOUSE_LONG,VEHICLE_LIMIT FROM Dash_Sites WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "'", "BSPIDB", DTGCHECK)
                LATFROM.Text = DTGCHECK.CurrentRow.Cells(0).Value
                LONGFROM.Text = DTGCHECK.CurrentRow.Cells(1).Value
                OO = DTGCHECK.CurrentRow.Cells(2).Value

                '  whlat.Text = LATFROM.Text
                '  whlong.Text = LONGFROM.Text

            Catch ex As Exception

            End Try

            Try

                views1("SELECT PLATE_NUM,BODY_TYPE,VOLUME_CAPACITY,AMOUNT_CAPACITY FROM Dash_Vehicles WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'ACTIVE' ORDER BY PRIORITY_COUNT ASC", "BSPIDB", DTGVEHICLES)

                TOTALVEHICLES.Text = DTGVEHICLES.Rows.Count()
                VEHICLEID.Text = DTGVEHICLES.CurrentRow.Cells(0).Value
                STORELIMIT.Text = DTGVEHICLES.CurrentRow.Cells(1).Value

                '  MsgBox(STORELIMIT.Text)

            Catch ex As Exception

            End Try

            Try

                Dim BATCHNUM As Integer

                views2("SELECT COUNT(COMPANY_ID) FROM Dash_SO_Plan_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "'", "BSPIDB", DTGCHECK)

                CHECKID.Text = DTGCHECK.CurrentRow.Cells(0).Value

                BATCHNUM = CHECKID.Text

                TIMENOSEPARATOR.Text = DTTIMENOSEAPARATOR.Text
                TIMENOSEPARATOR.Text.Replace(":", "")
                TIMENOSEPARATOR.Text.Replace(" ", "")
                TIMENOSEPARATOR.Text.Replace("PM", "")
                TIMENOSEPARATOR.Text.Replace("AM", "")
                TIMENOSEPARATOR.Text.Replace("pm", "")
                TIMENOSEPARATOR.Text.Replace("am", "")


                BATCHNUMBER.Text = "SOPLN" & COMPANYID.Text & SITEID.Text & timenosep & BATCHNUM + 1

            Catch ex As Exception

                '  MsgBox("1" & ex.ToString)

            End Try

            DTGBATCH.DataSource = Nothing

        End If

    End Sub

    Private Sub DTGBATCH_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DTGBATCH.CellValueChanged

        If e.ColumnIndex >= 0 AndAlso DTGBATCH.Columns(e.ColumnIndex).Name = "VEHICLE_ID" Then
            Dim row = DTGBATCH.Rows(e.RowIndex)
            Dim selectedVehicle As String = row.Cells("VEHICLE_ID").Value.ToString()
            Dim soPlanNumber As String = row.Cells("SO_PLAN_NUMBER").Value.ToString()

            ' Update DB
            Try
                Dim updateSql As String = "UPDATE Dash_SO_Plan_Transaction SET VEHICLE_ID = '" & selectedVehicle & "' " &
                                      "WHERE SO_PLAN_NUMBER = '" & soPlanNumber & "' AND COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "'"
                Data(updateSql)
                ' Optional success feedback:
                ' MessageBox.Show("Vehicle updated for plan: " & soPlanNumber)
            Catch ex As Exception
                MessageBox.Show("Error saving VEHICLE_ID: " & ex.Message)
            End Try

            Try
                Dim updateSql As String = "UPDATE Dash_SO_Plan_Transaction SET VEHICLE_ID = '" & selectedVehicle & "' " &
                                      "WHERE SO_PLAN_NUMBER = '" & soPlanNumber & "' AND COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "'"
                Data(updateSql)
                ' Optional success feedback:
                ' MessageBox.Show("Vehicle updated for plan: " & soPlanNumber)
            Catch ex As Exception
                MessageBox.Show("Error saving VEHICLE_ID: " & ex.Message)
            End Try

        End If
    End Sub

    Private Sub GunaLinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles GunaLinkLabel1.LinkClicked

        If BATCHNUMBER.Text = "" Then

            MsgBox("NO SELECTED BATCH", MsgBoxStyle.Exclamation, "SORRY")

        ElseIf DTGBATCH.Rows.Count = 0 Then

            MsgBox("NO BATCH SELECTED", MsgBoxStyle.Exclamation, "SORRY")

        Else

            Transactions_Order_Preparation_XDock.BATCHNUMBER.Text = BATCHNUMBER.Text
            '   Transactions_Order_Preparation_XDock.VEHICLE.Text = DTGBATCH.CurrentRow.Cells("").Value
            Transactions_Order_Preparation_XDock.ShowDialog()

        End If

    End Sub

    Private Sub DTGBATCH_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGBATCH.CellClick

        BATCHNUMBER.Text = ""

        Try

            BATCHNUMBER.Text = DTGBATCH.CurrentRow.Cells("SO_PLAN_NUMBER").Value

        Catch ex As Exception

        End Try

    End Sub

End Class