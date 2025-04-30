Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Public Class Transaction_Order_Preparation_Routing

    ' Distance function using Haversine formula
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

    ' Initialize WebView2 and Load Data
    Private Async Sub Transaction_Order_Preparation_Routing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Required to use WebView2
            Await WebView21.EnsureCoreWebView2Async()

            ' Load data to DTGROUTES
            views2("
                SELECT CUSTOMER_ID, CUSTOMER_NAME, DASH_ORDER_ID, TOTAL_VALUE, LATITUDE, LONGITUDE
                FROM PRFR_SO_UPLOAD_TRANSACTION
                LEFT JOIN Dash_Customer_Master 
                ON Dash_Customer_Master.CODE = PRFR_SO_UPLOAD_TRANSACTION.CUSTOMER_ID 
                AND Dash_Customer_Master.COMPANY_ID = PRFR_SO_UPLOAD_TRANSACTION.COMPANY_ID
                WHERE PRFR_SO_UPLOAD_TRANSACTION.COMPANY_ID = '9' 
                AND SITE_ID = '485'  
                AND IS_PLAN = '0' 
                AND ORDER_DATE = '2025/04/01'
                GROUP BY CUSTOMER_ID, CUSTOMER_NAME, DASH_ORDER_ID, TOTAL_VALUE, LATITUDE, LONGITUDE", "BSPIDB", DTGROUTES)

        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        End Try
    End Sub

    ' Button to cluster and display points on map
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ' Get number of clusters
            Dim K As Integer
            If Not Integer.TryParse(NUMOFCLUSTER.Text, K) OrElse K < 1 Then
                MessageBox.Show("Please enter a valid number of clusters.")
                Return
            End If

            ' Get coordinates from DataGridView
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

            ' Initialize centroids randomly
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

                ' Assignment step
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

                ' Update step
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
                        ' Reassign empty cluster
                        centroids(j) = points(rand.Next(points.Count))
                    End If
                Next
            End While

            ' Generate HTML map
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
            html.AppendLine("var map = L.map('map').setView([" & centerLat & ", " & centerLon & "], 11);")
            html.AppendLine("L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { attribution: '© OpenStreetMap', maxZoom: 18 }).addTo(map);")
            html.AppendLine("var colors = ['red','blue','green','orange','purple','cyan','lime','brown','black','magenta'];")

            For i = 0 To points.Count - 1
                Dim lat = points(i).Item1
                Dim lon = points(i).Item2
                Dim cid = assignments(i)
                html.AppendLine($"L.circleMarker([{lat}, {lon}], {{ color: colors[{cid}], fillColor: colors[{cid}], fillOpacity: 0.6, radius: 8 }}).addTo(map).bindPopup('Cluster {cid + 1}');")
            Next

            html.AppendLine($"<!-- Timestamp: {DateTime.Now.Ticks} -->") ' Force update
            html.AppendLine("</script></body></html>")

            ' Show in WebView2
            WebView21.NavigateToString(html.ToString())

            ' Update DataGridView with Cluster Groups
            If Not DTGROUTES.Columns.Contains("Cluster Group") Then
                DTGROUTES.Columns.Add("Cluster Group", "Cluster Group")
            End If

            ' Update the "Cluster Group" column with the cluster assignments
            For i = 0 To points.Count - 1
                DTGROUTES.Rows(i).Cells("Cluster Group").Value = assignments(i) + 1 ' Cluster index starts from 1
            Next

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

End Class
