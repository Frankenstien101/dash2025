
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Text

Public Class Reports_Order_Plan_Recalibrate
    Private Sub Reports_Order_Plan_Recalibrate_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Control.CheckForIllegalCrossThreadCalls = False

        '        Try
        '            ' Load data into DTGROUTES
        '            views1("Select Case SO_PLAN_NUMBER from Dash_SO_Plan_Batch_Details
        '
        'WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND ORDER_DATE = '" & DTTODAY.Text & "'
        '
        'GROUP BY SO_PLAN_NUMBER ",
        '        "BSPIDB", DTGROUTES)
        '
        '        End Try


    End Sub



    Private Async Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click
        SEARCH.Text = ""

        Dim numofcheked As Integer

        '   For Each value As String In cklist.CheckedItems
        '     SEARCH.Text = SEARCH.Text & "'" & value & "',"
        '   Next

        '  numofcheked = cklist.CheckedItems.Count()

        SEARCH.Text = SEARCH.Text.Remove(SEARCH.Text.Length - 1)

        Try
            ' Load data into DTGROUTES
            views1("SELECT 
                  Dash_SO_Plan_Batch_Details.SO_PLAN_NUMBER,
                  [CUSTOMER_ID],
                  [CUSTOMER_NAME],
                  [STORE_LAT],
                  [STORE_LONG]
        
           FROM [dbo].[Dash_SO_Plan_Batch_Details] 
           LEFT JOIN Dash_SO_Plan_Transaction 
           ON Dash_SO_Plan_Transaction.SO_PLAN_NUMBER = Dash_SO_Plan_Batch_Details.SO_PLAN_NUMBER 
           WHERE Dash_SO_Plan_Batch_Details.COMPANY_ID = '" & Form1.COMPANYID.Text & "' 
           AND Dash_SO_Plan_Batch_Details.SITE_ID = '" & Form1.SITEID.Text & "' 
              AND Dash_SO_Plan_Batch_Details.SO_PLAN_NUMBER IN (" & SEARCH.Text & ")
           AND ORDER_DATE = '" & DTTODAY.Text & "' 
           AND Dash_SO_Plan_Batch_Details.STATUS != 'NEW'",
        "BSPIDB", DTGDATA)

        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
            Return
        End Try

        If DTGDATA.Columns.Contains("Cluster Group") Then
            DTGDATA.Columns.Remove("Cluster Group")
        End If

        GunaAdvenceButton2.Enabled = False
        Await WebView21.EnsureCoreWebView2Async()

        If DTGDATA.Rows.Count = 0 Then
            MsgBox("NO ORDERS TO PROCESS", MsgBoxStyle.Exclamation, "SORRY")
            Return
        End If

        Try
            Dim K As Integer
            If Not Integer.TryParse(numofcheked, K) OrElse K < 1 Then
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

            ' K-means clustering
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

                ' Assign to nearest centroid
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

                ' Update centroids
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

            ' === BALANCE CLUSTERS BY MOVING NEARBY STORES FROM BIGGER TO SMALLER CLUSTER ===
            If K = 2 Then ' Only balance if 2 clusters, adjust for more if needed

                Dim clusterSizes = New Dictionary(Of Integer, Integer) From {
            {0, assignments.Count(Function(a) a = 0)},
            {1, assignments.Count(Function(a) a = 1)}
        }

                Dim bigClusterId As Integer = If(clusterSizes(0) > clusterSizes(1), 0, 1)
                Dim smallClusterId As Integer = If(bigClusterId = 0, 1, 0)

                Dim bigCount = clusterSizes(bigClusterId)
                Dim smallCount = clusterSizes(smallClusterId)

                Dim toMoveCount As Integer = (bigCount - smallCount) \ 2

                If toMoveCount > 0 Then
                    Dim smallCentroid = centroids(smallClusterId)
                    Dim maxDistanceThreshold As Double = 0.5 ' Adjust distance threshold here (degrees)

                    Dim candidatePoints = points.
                Select(Function(p, i) New With {
                    .Index = i,
                    .Cluster = assignments(i),
                    .Distance = Distance(p.Item1, p.Item2, smallCentroid.Item1, smallCentroid.Item2)
                }).
                Where(Function(x) x.Cluster = bigClusterId AndAlso x.Distance <= maxDistanceThreshold).
                OrderBy(Function(x) x.Distance).
                Take(toMoveCount).
                ToList()

                    For Each cp In candidatePoints
                        assignments(cp.Index) = smallClusterId
                    Next

                    ' Recalculate centroids for balanced clusters
                    For Each cid In {bigClusterId, smallClusterId}
                        Dim assignedPoints = points.
                    Select(Function(pt, i) New With {.Point = pt, .Index = i}).
                    Where(Function(x) assignments(x.Index) = cid).
                    Select(Function(x) x.Point).
                    ToList()
                        If assignedPoints.Count > 0 Then
                            Dim avgLat = assignedPoints.Average(Function(pt) pt.Item1)
                            Dim avgLon = assignedPoints.Average(Function(pt) pt.Item2)
                            centroids(cid) = Tuple.Create(avgLat, avgLon)
                        End If
                    Next
                End If
            End If
            ' === END OF BALANCING LOGIC ===

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

            ' Add cluster markers
            For i = 0 To points.Count - 1
                Dim lat = points(i).Item1
                Dim lon = points(i).Item2
                Dim cid = assignments(i)
                Dim storename = DTGDATA.Rows(i).Cells("CUSTOMER_NAME").Value.ToString()
                html.AppendLine($"L.circleMarker([{lat}, {lon}], {{ color: colors[{cid}], fillColor: colors[{cid}], fillOpacity: 0.6, radius: 8 }}).addTo(map).bindPopup('{storename.Replace("'", "\'")} (Cluster {cid + 1})');")
            Next

            ' Add legend with store counts
            Dim clusterCounts = assignments.
                        GroupBy(Function(a) a).
                        ToDictionary(Function(g) g.Key, Function(g) g.Count())

            html.AppendLine("var legend = L.control({position: 'topleft'});")
            html.AppendLine("legend.onAdd = function (map) {")
            html.AppendLine("  var div = L.DomUtil.create('div', 'info legend');")
            html.AppendLine("  div.style.backgroundColor = 'white';")
            html.AppendLine("  div.style.padding = '10px';")
            html.AppendLine("  div.style.border = '1px solid gray';")
            html.AppendLine("  div.style.borderRadius = '5px';")
            html.AppendLine("  div.style.fontSize = '13px';")
            html.AppendLine("  div.innerHTML += '<b>Clusters</b><br/>';")

            For Each kvp In clusterCounts
                Dim color = GetColorName(kvp.Key)
                html.AppendLine($"  div.innerHTML += '<i style=""background:{color};width:12px;height:12px;display:inline-block;margin-right:5px;""></i> Cluster {kvp.Key + 1} ({kvp.Value} stores)<br/>';")
            Next

            html.AppendLine("  return div;")
            html.AppendLine("};")
            html.AppendLine("legend.addTo(map);")

            html.AppendLine($"<!-- Timestamp: {DateTime.Now.Ticks} -->") ' Force reload
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

        GunaAdvenceButton2.Enabled = True

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

    Private Function GetColorName(cid As Integer) As String
        Dim colors() As String = {"red", "blue", "green", "orange", "purple", "cyan", "lime", "brown", "black", "magenta"}
        Return colors(cid Mod colors.Length)
    End Function

    Private Sub GunaAdvenceButton2_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton2.Click

    End Sub

    Private Sub GunaLabel2_Click(sender As Object, e As EventArgs) Handles GunaLabel2.Click

        Close()

    End Sub
End Class