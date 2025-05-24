Imports System.Data.SqlClient
Imports Microsoft.Office.Interop.Excel
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Reports_Cross_Dock
    Private Sub Reports_Cross_Dock_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Form1.ROLETXT2.Text = "ADMIN" Then

            ALLSITECK.Visible = True

        Else

            ALLSITECK.Visible = False

        End If

        DTFROM.Value = Form1.DTCALENDAR.Value
        DTTO.Value = Form1.DTCALENDAR.Value

        cklist.Items.Clear()

        con.Open()

        Dim cmd As SqlCommand = New SqlCommand("select PLATE_NUM from Dash_Vehicles WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'ACTIVE' GROUP BY PLATE_NUM ", con)
        Dim rd As SqlDataReader = cmd.ExecuteReader()

        While rd.Read()

            cklist.Items.Add(rd("PLATE_NUM").ToString(), CheckState.Unchecked)

        End While

        con.Close()

    End Sub

    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click

        STATUS.Text = "GENARATING YOUR DATA, PLEASE WAIT. . ."

        If ALLSITECK.Checked = True Then

            Try

                views("WITH OrderedPoints AS (
                                SELECT
                                    AGENT_ID,
                                    DELIVERY_DATE,
                                    LAT_CAPTURED,
                                    LONG_CAPTURED,
                                    TIME_STAMP,
                                    ROW_NUMBER() OVER (PARTITION BY AGENT_ID, DELIVERY_DATE ORDER BY TIME_STAMP) AS rn
                                FROM [dbo].[Dash_Agent_Time_Stamp]
                            ),
                            
                            DistancePairs AS (
                                SELECT
                                    a.AGENT_ID,
                                    a.DELIVERY_DATE,
                                    geography::Point(a.LAT_CAPTURED, a.LONG_CAPTURED, 4326) AS PointA,
                                    geography::Point(b.LAT_CAPTURED, b.LONG_CAPTURED, 4326) AS PointB
                                FROM OrderedPoints a
                                INNER JOIN OrderedPoints b 
                                    ON a.AGENT_ID = b.AGENT_ID
                                    AND a.DELIVERY_DATE = b.DELIVERY_DATE
                                    AND a.rn = b.rn - 1
                            ),
                            
                            TotalDistances AS (
                                SELECT
                                    AGENT_ID,
                                    DELIVERY_DATE,
                                    ROUND(SUM(PointA.STDistance(PointB)) / 1000.0, 2) AS TotalDistanceKm  -- total distance in kilometers
                                FROM DistancePairs
                                GROUP BY AGENT_ID, DELIVERY_DATE
                            )
                            
                            SELECT 
                                xd.[COMPANY_ID],
                                xd.[SITE_ID],
                                xd.[BATCH],
                                xd.[DELIVERY_AMOUNT],
                                xd.[AGENT],
                                xd.[VEHICLE],
                                xd.[TRANSACTION_DATE],
                            
                                CONVERT(varchar(8), xd.[WH_ENTRY], 108) AS WAREHOUSE_ENTRY,
                                CONVERT(varchar(8), xd.[DEPARTURE_TIME], 108) AS DEPARTURE_TIME,
                                CONVERT(varchar(8), xd.[ARRIVAL_TIME], 108) AS CROSS_DOCK_ARRIVAL_TIME,
                                CONVERT(varchar(8), xd.[XD_EXIT], 108) AS CROSS_DOCK_EXIT,
                                CONVERT(varchar(8), xd.[WH_RETURN], 108) AS WAREHOUSE_RETURN_TIME,
                            
                                td.TotalDistanceKm AS DISTANCE_TRAVELLED_IN_KM,
                            
                                ROUND(
                                    td.TotalDistanceKm * ISNULL(v.CONSUMPTION_PER_100_METERS, 0) / 100, 
                                    2
                                ) AS FUEL_CONSUMED_IN_LITER
                            
                            FROM [dbo].[Dash_XDock_Status] xd
                            LEFT JOIN TotalDistances td 
                                ON xd.AGENT = td.AGENT_ID 
                                AND xd.TRANSACTION_DATE = td.DELIVERY_DATE
                            LEFT JOIN [dbo].[Dash_Vehicles] v
                                ON xd.VEHICLE = v.PLATE_NUM
                            
                            WHERE xd.TRANSACTION_DATE BETWEEN '" & DTFROM.Value & "' AND '" & DTTO.Value & "' AND  xd.[COMPANY_ID] = '" & Form1.COMPANYID.Text & "'
                            
                            ORDER BY xd.AGENT, xd.TRANSACTION_DATE;
                                            ", "BSPIDB", DTGDATA)

            Catch EX As Exception

                MsgBox(EX.ToString)

            End Try

        Else

            If ALLVEHICLECK.Checked = True Then

                Try

                    views("WITH OrderedPoints AS (
                                SELECT
                                    AGENT_ID,
                                    DELIVERY_DATE,
                                    LAT_CAPTURED,
                                    LONG_CAPTURED,
                                    TIME_STAMP,
                                    ROW_NUMBER() OVER (PARTITION BY AGENT_ID, DELIVERY_DATE ORDER BY TIME_STAMP) AS rn
                                FROM [dbo].[Dash_Agent_Time_Stamp]
                            ),
                            
                            DistancePairs AS (
                                SELECT
                                    a.AGENT_ID,
                                    a.DELIVERY_DATE,
                                    geography::Point(a.LAT_CAPTURED, a.LONG_CAPTURED, 4326) AS PointA,
                                    geography::Point(b.LAT_CAPTURED, b.LONG_CAPTURED, 4326) AS PointB
                                FROM OrderedPoints a
                                INNER JOIN OrderedPoints b 
                                    ON a.AGENT_ID = b.AGENT_ID
                                    AND a.DELIVERY_DATE = b.DELIVERY_DATE
                                    AND a.rn = b.rn - 1
                            ),
                            
                            TotalDistances AS (
                                SELECT
                                    AGENT_ID,
                                    DELIVERY_DATE,
                                    ROUND(SUM(PointA.STDistance(PointB)) / 1000.0, 2) AS TotalDistanceKm  -- total distance in kilometers
                                FROM DistancePairs
                                GROUP BY AGENT_ID, DELIVERY_DATE
                            )
                            
                            SELECT 
                                xd.[COMPANY_ID],
                                xd.[SITE_ID],
                                xd.[BATCH],
                                xd.[DELIVERY_AMOUNT],
                                xd.[AGENT],
                                xd.[VEHICLE],
                                xd.[TRANSACTION_DATE],
                            
                                CONVERT(varchar(8), xd.[WH_ENTRY], 108) AS WAREHOUSE_ENTRY,
                                CONVERT(varchar(8), xd.[DEPARTURE_TIME], 108) AS DEPARTURE_TIME,
                                CONVERT(varchar(8), xd.[ARRIVAL_TIME], 108) AS CROSS_DOCK_ARRIVAL_TIME,
                                CONVERT(varchar(8), xd.[XD_EXIT], 108) AS CROSS_DOCK_EXIT,
                                CONVERT(varchar(8), xd.[WH_RETURN], 108) AS WAREHOUSE_RETURN_TIME,
                            
                                td.TotalDistanceKm AS DISTANCE_TRAVELLED_IN_KM,
                            
                                ROUND(
                                    td.TotalDistanceKm * ISNULL(v.CONSUMPTION_PER_100_METERS, 0) / 100, 
                                    2
                                ) AS FUEL_CONSUMED_IN_LITER
                            
                            FROM [dbo].[Dash_XDock_Status] xd
                            LEFT JOIN TotalDistances td 
                                ON xd.AGENT = td.AGENT_ID 
                                AND xd.TRANSACTION_DATE = td.DELIVERY_DATE
                            LEFT JOIN [dbo].[Dash_Vehicles] v
                                ON xd.VEHICLE = v.PLATE_NUM
                            
                            WHERE xd.TRANSACTION_DATE BETWEEN '" & DTFROM.Value & "' AND '" & DTTO.Value & "' AND  xd.[COMPANY_ID] = '" & Form1.COMPANYID.Text & "' AND xd.[SITE_ID] = '" & Form1.SITEID.Text & "'
                            
                            ORDER BY xd.AGENT, xd.TRANSACTION_DATE;
                                            ", "BSPIDB", DTGDATA)

                Catch EX As Exception
                    MsgBox(EX.ToString)
                End Try

            Else

                '' PER SITE BY VEHICLE
                Try


                    SEARCH.Text = ""

                    For Each value As String In cklist.CheckedItems
                        SEARCH.Text = SEARCH.Text & "'" & value & "',"
                    Next

                    SEARCH.Text = SEARCH.Text.Remove(SEARCH.Text.Length - 1)

                Catch ex As Exception

                End Try


                Try

                    views("WITH OrderedPoints AS (
                                SELECT
                                    AGENT_ID,
                                    DELIVERY_DATE,
                                    LAT_CAPTURED,
                                    LONG_CAPTURED,
                                    TIME_STAMP,
                                    ROW_NUMBER() OVER (PARTITION BY AGENT_ID, DELIVERY_DATE ORDER BY TIME_STAMP) AS rn
                                FROM [dbo].[Dash_Agent_Time_Stamp]
                            ),
                            
                            DistancePairs AS (
                                SELECT
                                    a.AGENT_ID,
                                    a.DELIVERY_DATE,
                                    geography::Point(a.LAT_CAPTURED, a.LONG_CAPTURED, 4326) AS PointA,
                                    geography::Point(b.LAT_CAPTURED, b.LONG_CAPTURED, 4326) AS PointB
                                FROM OrderedPoints a
                                INNER JOIN OrderedPoints b 
                                    ON a.AGENT_ID = b.AGENT_ID
                                    AND a.DELIVERY_DATE = b.DELIVERY_DATE
                                    AND a.rn = b.rn - 1
                            ),
                            
                            TotalDistances AS (
                                SELECT
                                    AGENT_ID,
                                    DELIVERY_DATE,
                                    ROUND(SUM(PointA.STDistance(PointB)) / 1000.0, 2) AS TotalDistanceKm  -- total distance in kilometers
                                FROM DistancePairs
                                GROUP BY AGENT_ID, DELIVERY_DATE
                            )
                            
                            SELECT 
                                xd.[COMPANY_ID],
                                xd.[SITE_ID],
                                xd.[BATCH],
                                xd.[DELIVERY_AMOUNT],
                                xd.[AGENT],
                                xd.[VEHICLE],
                                xd.[TRANSACTION_DATE],
                            
                                CONVERT(varchar(8), xd.[WH_ENTRY], 108) AS WAREHOUSE_ENTRY,
                                CONVERT(varchar(8), xd.[DEPARTURE_TIME], 108) AS DEPARTURE_TIME,
                                CONVERT(varchar(8), xd.[ARRIVAL_TIME], 108) AS CROSS_DOCK_ARRIVAL_TIME,
                                CONVERT(varchar(8), xd.[XD_EXIT], 108) AS CROSS_DOCK_EXIT,
                                CONVERT(varchar(8), xd.[WH_RETURN], 108) AS WAREHOUSE_RETURN_TIME,
                            
                                td.TotalDistanceKm AS DISTANCE_TRAVELLED_IN_KM,
                            
                                ROUND(
                                    td.TotalDistanceKm * ISNULL(v.CONSUMPTION_PER_100_METERS, 0) / 100, 
                                    2
                                ) AS FUEL_CONSUMED_IN_LITER
                            
                            FROM [dbo].[Dash_XDock_Status] xd
                            LEFT JOIN TotalDistances td 
                                ON xd.AGENT = td.AGENT_ID 
                                AND xd.TRANSACTION_DATE = td.DELIVERY_DATE
                            LEFT JOIN [dbo].[Dash_Vehicles] v
                                ON xd.VEHICLE = v.PLATE_NUM
                            
                            WHERE xd.TRANSACTION_DATE BETWEEN '" & DTFROM.Value & "' AND '" & DTTO.Value & "' AND  xd.[COMPANY_ID] = '" & Form1.COMPANYID.Text & "' AND xd.[SITE_ID] = '" & Form1.SITEID.Text & "' AND xd.VEHICLE IN (" & SEARCH.Text & ")
                            
                            ORDER BY xd.AGENT, xd.TRANSACTION_DATE;
                                            ", "BSPIDB", DTGDATA)

                Catch EX As Exception
                    MsgBox(EX.ToString)
                End Try

            End If

        End If

        STATUS.Text = ""

    End Sub

    Private Sub GunaAdvenceButton2_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton2.Click


        If DTGDATA.Rows.Count > 0 Then

            STATUS.Text = "EXPORTING YOUR REPORT, PLEASE WAIT. . ."

            EXPORT()

            STATUS.Text = ""
        Else

            MsgBox("NO DATA TO EXPORT", MsgBoxStyle.Exclamation, "SORRY")

        End If

    End Sub

    Private Sub EXPORT()

        Dim dt1 As DataGridView = DTGDATA


        Dim l As String = DTFROM.Value.ToString("yyyyMMdd")
        Dim lA As String = DTTO.Value.ToString("yyyyMMdd")
        Dim d As String = Form1.TIMETXT.Text.Replace(":", "").Replace("am", "").Replace("pm", "").Replace("AM", "").Replace("PM", "").Replace(" ", "")

        Dim sfd As New SaveFileDialog()
        sfd.Filter = "Excel Documents (.xlsx)|*.xlsx"
        sfd.FileName = "CrossDockReport_" & l & "_" & lA & "_" & d & ".xlsx"

        If sfd.ShowDialog() = DialogResult.OK Then
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim xlexcel As New Excel.Application()

            xlexcel.DisplayAlerts = False
            Dim xlWorkBook As Excel.Workbook = xlexcel.Workbooks.Add(misValue)

            ' Export first DataGridView to the first worksheet
            Dim xlWorkSheet1 As Excel.Worksheet = xlWorkBook.Worksheets(1)
            xlWorkSheet1.Name = "RESULT"
            copyAlltoClipboard(dt1)
            xlWorkSheet1.Cells(1, 1).PasteSpecial(Excel.XlPasteType.xlPasteAll, Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, Type.Missing, Type.Missing)
            Clipboard.Clear()
            dt1.ClearSelection()



            ' Save the workbook as .xlsx
            xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, False, False, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
            xlexcel.DisplayAlerts = True
            xlWorkBook.Close(True, misValue, misValue)
            xlexcel.Quit()

            releaseObject(xlWorkSheet1)

            releaseObject(xlexcel)

            If MessageBox.Show("Download Complete! Would you like to open the file?", "Successfully Saved!", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                If File.Exists(sfd.FileName) Then
                    System.Diagnostics.Process.Start(sfd.FileName)
                End If
            End If
        End If
    End Sub

    Private Sub copyAlltoClipboard(dgv As DataGridView)
        dgv.SelectAll()
        Dim dataObj As DataObject = dgv.GetClipboardContent()
        If dataObj IsNot Nothing Then
            Clipboard.SetDataObject(dataObj)
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

End Class