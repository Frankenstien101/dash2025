
Imports System.Data.SqlClient
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Reports_Result

    Dim connStr As String = "Data Source=bspidbservernew.database.windows.net;Network Library=DBMSSOCN;Initial Catalog=BSPIDBNEW;User ID=sqladmin;Password=b$p1.@dm1n;" ' Edit this

    Dim connStr1 As String = "Data Source=bspidbservernew.database.windows.net;Network Library=DBMSSOCN;Initial Catalog=BSPIDBNEW;User ID=sqladmin;Password=b$p1.@dm1n;" ' Edit this


    Private Sub Reports_Result_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DTFROM.Value = Form1.DTCALENDAR.Value
        DTTO.Value = Form1.DTCALENDAR.Value

        If Form1.ROLETXT2.Text = "ADMIN" Then

            ALLSITECK.Visible = True

        Else

            ALLSITECK.Visible = False

        End If

    End Sub

    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click

        DTGDATA.DataSource = Nothing
        DTGDETAILS.DataSource = Nothing

        If ALLSITECK.Checked = True Then

            Try

                LoadBranchSellerSummary()
                LoadBranchSellerSummaryDetails()
                SHOWDETAILS()

            Catch ex As Exception

            End Try

        Else


            Try

                LoadBranchSellerSummary2()
                LoadBranchSellerSummaryDetails2()
                SHOWDETAILS2()

            Catch ex As Exception

                '   MsgBox(ex.ToString)

            End Try


        End If

    End Sub

    Private Sub SHOWDETAILS()

        Try

            views2("SELECT 
                        [DISTRIBUTOR_CODE],
                        [BRANCH_CODE],
                        [BRANCH],
                        ORDER_DATE,
                        [DATE] AS INVOICE_DATE,
                        Dash_Plan_Batch_Details.DATE_TO_DELIVER AS DATE_DELIVERED,
                        [SALES_REP],
                        [SELLER_NAME],
                        AGENT_ID,
                        BATCH,
                        Dash_Plan_Batch_Details.STATUS,
                        CASE 
                            WHEN SUM(ISNULL([RETURN_AMOUNT], 0)) = 0 THEN 'NO' 
                            ELSE 'YES' 
                        END AS HAS_RETURN,
                        PRFR_Invoice_Detailed.CUSTOMER_ID,
                        PRFR_Invoice_Detailed.CUSTOMER_NAME,
                        [DOCUMENT_NUMBER],
                        SUM([SALES_AMOUNT]) AS TOTAL,
                        PG_LOCAL_SUBSEGMENT,
                        MAX(Dash_Agent_Performance_Detailed.STORE_ENTRY) AS STORE_ENTRY,
                        MAX(Dash_Agent_Performance_Detailed.STORE_EXIT) AS STORE_EXIT,
                        MAX(Dash_Agent_Performance_Detailed.STORE_TIME_SPENT) AS STORE_TIME_SPENT
                    FROM [dbo].[PRFR_Invoice_Detailed]
                    LEFT JOIN Dash_Plan_Batch_Details 
                        ON Dash_Plan_Batch_Details.INVOICE_NUMBER = PRFR_Invoice_Detailed.DOCUMENT_NUMBER 
                        AND PRFR_Invoice_Detailed.DISTRIBUTOR_CODE = Dash_Plan_Batch_Details.COMPANY_ID
                    LEFT JOIN Dash_Returns 
                        ON Dash_Returns.INVOICE_NUMBER = PRFR_Invoice_Detailed.DOCUMENT_NUMBER 
                        AND PRFR_Invoice_Detailed.DISTRIBUTOR_CODE = Dash_Returns.COMPANY_ID 
                        AND Dash_Returns.IT_BARCODE = PRFR_Invoice_Detailed.IT_BARCODE
                    LEFT JOIN Dash_Agent_Performance_Detailed
                        ON Dash_Agent_Performance_Detailed.DELIVERY_DATE = Dash_Plan_Batch_Details.DATE_TO_DELIVER
                        AND Dash_Agent_Performance_Detailed.STORE_CODE = PRFR_Invoice_Detailed.CUSTOMER_ID
                        AND Dash_Agent_Performance_Detailed.COMPANY_ID = PRFR_Invoice_Detailed.DISTRIBUTOR_CODE
                    WHERE Dash_Plan_Batch_Details.COMPANY_ID = '" & Form1.COMPANYID.Text & "'
                        AND Dash_Plan_Batch_Details.DATE_TO_DELIVER BETWEEN '" & DTFROM.Value & "' AND '" & DTTO.Value & "'
                    GROUP BY 
                        [DISTRIBUTOR_CODE],
                        [BRANCH_CODE],
                        [BRANCH],
                        ORDER_DATE,
                        [DATE],
                        Dash_Plan_Batch_Details.DATE_TO_DELIVER,
                        [SALES_REP],
                        [SELLER_NAME],
                        AGENT_ID,
                        BATCH,
                        Dash_Plan_Batch_Details.STATUS,
                        PRFR_Invoice_Detailed.CUSTOMER_ID,
                        PRFR_Invoice_Detailed.CUSTOMER_NAME,
                        [DOCUMENT_NUMBER],
                        PG_LOCAL_SUBSEGMENT
                    
                    ", "BSPIDB", DTGDELIVERYDETAILS)

        Catch ex As Exception

            '  MsgBox(ex.ToString)

        End Try

    End Sub

    Private Sub SHOWDETAILS2()

        Try

            views2("SELECT 
                        [DISTRIBUTOR_CODE],
                        [BRANCH_CODE],
                        [BRANCH],
                        ORDER_DATE,
                        [DATE] AS INVOICE_DATE,
                        Dash_Plan_Batch_Details.DATE_TO_DELIVER AS DATE_DELIVERED,
                        [SALES_REP],
                        [SELLER_NAME],
                        AGENT_ID,
                        BATCH,
                        Dash_Plan_Batch_Details.STATUS,
                        CASE 
                            WHEN SUM(ISNULL([RETURN_AMOUNT], 0)) = 0 THEN 'NO' 
                            ELSE 'YES' 
                        END AS HAS_RETURN,
                        PRFR_Invoice_Detailed.CUSTOMER_ID,
                        PRFR_Invoice_Detailed.CUSTOMER_NAME,
                        [DOCUMENT_NUMBER],
                        SUM([SALES_AMOUNT]) AS TOTAL,
                        PG_LOCAL_SUBSEGMENT,
                        MAX(Dash_Agent_Performance_Detailed.STORE_ENTRY) AS STORE_ENTRY,
                        MAX(Dash_Agent_Performance_Detailed.STORE_EXIT) AS STORE_EXIT,
                        MAX(Dash_Agent_Performance_Detailed.STORE_TIME_SPENT) AS STORE_TIME_SPENT
                    FROM [dbo].[PRFR_Invoice_Detailed]
                    LEFT JOIN Dash_Plan_Batch_Details 
                        ON Dash_Plan_Batch_Details.INVOICE_NUMBER = PRFR_Invoice_Detailed.DOCUMENT_NUMBER 
                        AND PRFR_Invoice_Detailed.DISTRIBUTOR_CODE = Dash_Plan_Batch_Details.COMPANY_ID
                    LEFT JOIN Dash_Returns 
                        ON Dash_Returns.INVOICE_NUMBER = PRFR_Invoice_Detailed.DOCUMENT_NUMBER 
                        AND PRFR_Invoice_Detailed.DISTRIBUTOR_CODE = Dash_Returns.COMPANY_ID 
                        AND Dash_Returns.IT_BARCODE = PRFR_Invoice_Detailed.IT_BARCODE
                    LEFT JOIN Dash_Agent_Performance_Detailed
                        ON Dash_Agent_Performance_Detailed.DELIVERY_DATE = Dash_Plan_Batch_Details.DATE_TO_DELIVER
                        AND Dash_Agent_Performance_Detailed.STORE_CODE = PRFR_Invoice_Detailed.CUSTOMER_ID
                        AND Dash_Agent_Performance_Detailed.COMPANY_ID = PRFR_Invoice_Detailed.DISTRIBUTOR_CODE
                    WHERE Dash_Plan_Batch_Details.COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND Dash_Plan_Batch_Details.SITE_ID = '" & Form1.SITEID.Text & "'
                        AND Dash_Plan_Batch_Details.DATE_TO_DELIVER BETWEEN '" & DTFROM.Value & "' AND '" & DTTO.Value & "'
                    GROUP BY 
                        [DISTRIBUTOR_CODE],
                        [BRANCH_CODE],
                        [BRANCH],
                        ORDER_DATE,
                        [DATE],
                        Dash_Plan_Batch_Details.DATE_TO_DELIVER,
                        [SALES_REP],
                        [SELLER_NAME],
                        AGENT_ID,
                        BATCH,
                        Dash_Plan_Batch_Details.STATUS,
                        PRFR_Invoice_Detailed.CUSTOMER_ID,
                        PRFR_Invoice_Detailed.CUSTOMER_NAME,
                        [DOCUMENT_NUMBER],
                        PG_LOCAL_SUBSEGMENT
                    
                    ", "BSPIDB", DTGDELIVERYDETAILS)

        Catch ex As Exception

            '  MsgBox(ex.ToString)

        End Try

    End Sub

    Private Sub LoadBranchSellerSummary()
        Dim dt As New DataTable()

        Dim query As String = "
    SELECT 
        PRFR_Invoice_Detailed.BRANCH,
        PRFR_Invoice_Detailed.SELLER_NAME,
        SUM(CASE WHEN Dash_Plan_Batch_Details.STATUS = 'DELIVERED' THEN 1 ELSE 0 END) AS DELIVERED,
        SUM(CASE WHEN Dash_Plan_Batch_Details.STATUS = 'FAILED' THEN 1 ELSE 0 END) AS FAILED,
        SUM(CASE WHEN Dash_Plan_Batch_Details.STATUS = 'FOR DELIVERY' THEN 1 ELSE 0 END) AS FOR_DELIVERY,
        SUM(CASE WHEN Dash_Plan_Batch_Details.STATUS = 'VERIFIED' THEN 1 ELSE 0 END) AS VERIFIED
    FROM [dbo].[PRFR_Invoice_Detailed]
    LEFT JOIN Dash_Plan_Batch_Details 
        ON Dash_Plan_Batch_Details.INVOICE_NUMBER = PRFR_Invoice_Detailed.DOCUMENT_NUMBER 
        AND PRFR_Invoice_Detailed.DISTRIBUTOR_CODE = Dash_Plan_Batch_Details.COMPANY_ID
    WHERE Dash_Plan_Batch_Details.COMPANY_ID = @CompanyId
      AND Dash_Plan_Batch_Details.DATE_TO_DELIVER BETWEEN @DateFrom AND @DateTo
    GROUP BY PRFR_Invoice_Detailed.BRANCH, PRFR_Invoice_Detailed.SELLER_NAME
    ORDER BY PRFR_Invoice_Detailed.BRANCH, PRFR_Invoice_Detailed.SELLER_NAME"

        Using conn As New SqlConnection(connStr)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@CompanyId", Form1.COMPANYID.Text)
                cmd.Parameters.AddWithValue("@DateFrom", DTFROM.Value.Date)
                cmd.Parameters.AddWithValue("@DateTo", DTTO.Value.Date)

                conn.Open()
                dt.Load(cmd.ExecuteReader())
            End Using
        End Using

        ' Debug: Show how many rows returned
        If dt.Rows.Count = 0 Then
            MessageBox.Show("No data returned for the selected filters.")
            Return
        End If

        ' Setup columns in DataGridView
        DTGDATA.Columns.Clear()
        DTGDATA.Rows.Clear()
        DTGDATA.Columns.Add("Label", "DETAILS")
        DTGDATA.Columns.Add("DELIVERED", "DELIVERED")
        DTGDATA.Columns.Add("DELIVERED_PCT", "DELIVERED %")
        DTGDATA.Columns.Add("FAILED", "FAILED")
        DTGDATA.Columns.Add("FAILED_PCT", "FAILED %")
        DTGDATA.Columns.Add("FOR_DELIVERY", "FOR DELIVERY")
        DTGDATA.Columns.Add("FOR_DELIVERY_PCT", "FOR DELIVERY %")
        DTGDATA.Columns.Add("VERIFIED", "VERIFIED")
        DTGDATA.Columns.Add("VERIFIED_PCT", "VERIFIED %")
        DTGDATA.Columns.Add("TOTAL", "Grand Total")

        ' Group by branch
        Dim branchGroups = dt.AsEnumerable().GroupBy(Function(r) r.Field(Of String)("BRANCH"))

        For Each branchGroup In branchGroups
            Dim branchName = branchGroup.Key

            Dim delivered = branchGroup.Sum(Function(r) Convert.ToInt32(r("DELIVERED")))
            Dim failed = branchGroup.Sum(Function(r) Convert.ToInt32(r("FAILED")))
            Dim forDelivery = branchGroup.Sum(Function(r) Convert.ToInt32(r("FOR_DELIVERY")))
            Dim verified = branchGroup.Sum(Function(r) Convert.ToInt32(r("VERIFIED")))
            Dim total = delivered + failed + forDelivery + verified

            Dim pct = Function(value As Integer) As String
                          If total = 0 Then Return "0%"
                          Return (value / total * 100).ToString("0.0") & "%"
                      End Function

            Dim headerRow = DTGDATA.Rows.Add(branchName,
                                             delivered, pct(delivered),
                                             failed, pct(failed),
                                             forDelivery, pct(forDelivery),
                                             verified, pct(verified),
                                             total)
            With DTGDATA.Rows(headerRow).DefaultCellStyle
                .Font = New Font(DTGDATA.Font, FontStyle.Bold)
                .BackColor = Color.LightGray
            End With

            For Each sellerRow In branchGroup
                Dim sellerName = sellerRow.Field(Of String)("SELLER_NAME")
                Dim d = Convert.ToInt32(sellerRow("DELIVERED"))
                Dim f = Convert.ToInt32(sellerRow("FAILED"))
                Dim fd = Convert.ToInt32(sellerRow("FOR_DELIVERY"))
                Dim v = Convert.ToInt32(sellerRow("VERIFIED"))
                Dim t = d + f + fd + v

                Dim sellerPct = Function(value As Integer) As String
                                    If t = 0 Then Return "0%"
                                    Return (value / t * 100).ToString("0.0") & "%"
                                End Function

                DTGDATA.Rows.Add("    " & sellerName,
                                d, sellerPct(d),
                                f, sellerPct(f),
                                fd, sellerPct(fd),
                                v, sellerPct(v),
                                t)
            Next
        Next

    End Sub



    Private Sub LoadBranchSellerSummaryDetails()
        Dim dt1 As New DataTable()

        Dim query1 As String = "
SELECT 
    PRFR_Invoice_Detailed.BRANCH,
    Dash_Plan_Batch_Details.AGENT_ID,
    SUM(CASE WHEN Dash_Plan_Batch_Details.STATUS = 'DELIVERED' THEN 1 ELSE 0 END) AS DELIVERED,
    SUM(CASE WHEN Dash_Plan_Batch_Details.STATUS = 'FAILED' THEN 1 ELSE 0 END) AS FAILED,
    SUM(CASE WHEN Dash_Plan_Batch_Details.STATUS = 'FOR DELIVERY' THEN 1 ELSE 0 END) AS FOR_DELIVERY,
    SUM(CASE WHEN Dash_Plan_Batch_Details.STATUS = 'VERIFIED' THEN 1 ELSE 0 END) AS VERIFIED
FROM [dbo].[PRFR_Invoice_Detailed]
LEFT JOIN Dash_Plan_Batch_Details 
    ON Dash_Plan_Batch_Details.INVOICE_NUMBER = PRFR_Invoice_Detailed.DOCUMENT_NUMBER 
    AND PRFR_Invoice_Detailed.DISTRIBUTOR_CODE = Dash_Plan_Batch_Details.COMPANY_ID
WHERE Dash_Plan_Batch_Details.COMPANY_ID = @CompanyId
  AND Dash_Plan_Batch_Details.DATE_TO_DELIVER BETWEEN @DateFrom AND @DateTo
GROUP BY PRFR_Invoice_Detailed.BRANCH, Dash_Plan_Batch_Details.AGENT_ID
ORDER BY PRFR_Invoice_Detailed.BRANCH, Dash_Plan_Batch_Details.AGENT_ID"

        Using conn1 As New SqlConnection(connStr1)
            Using cmd1 As New SqlCommand(query1, conn1)
                cmd1.Parameters.AddWithValue("@CompanyId", Form1.COMPANYID.Text)
                cmd1.Parameters.AddWithValue("@DateFrom", DTFROM.Value.Date)
                cmd1.Parameters.AddWithValue("@DateTo", DTTO.Value.Date)

                conn1.Open()
                dt1.Load(cmd1.ExecuteReader())
            End Using
        End Using

        If dt1.Rows.Count = 0 Then
            MessageBox.Show("No data returned for the selected filters.")
            Return
        End If

        DTGDETAILS.Columns.Clear()
        DTGDETAILS.Rows.Clear()
        DTGDETAILS.Columns.Add("Label", "DETAILS")
        DTGDETAILS.Columns.Add("DELIVERED", "DELIVERED")
        DTGDETAILS.Columns.Add("DELIVERED_PCT", "DELIVERED %")
        DTGDETAILS.Columns.Add("FAILED", "FAILED")
        DTGDETAILS.Columns.Add("FAILED_PCT", "FAILED %")
        DTGDETAILS.Columns.Add("FOR_DELIVERY", "FOR DELIVERY")
        DTGDETAILS.Columns.Add("FOR_DELIVERY_PCT", "FOR DELIVERY %")
        DTGDETAILS.Columns.Add("VERIFIED", "VERIFIED")
        DTGDETAILS.Columns.Add("VERIFIED_PCT", "VERIFIED %")
        DTGDETAILS.Columns.Add("TOTAL", "Grand Total")

        Dim branchGroups = dt1.AsEnumerable().GroupBy(Function(r) r.Field(Of String)("BRANCH"))

        For Each branchGroup In branchGroups
            Dim branchName = branchGroup.Key

            Dim delivered = branchGroup.Sum(Function(r) Convert.ToInt32(r("DELIVERED")))
            Dim failed = branchGroup.Sum(Function(r) Convert.ToInt32(r("FAILED")))
            Dim forDelivery = branchGroup.Sum(Function(r) Convert.ToInt32(r("FOR_DELIVERY")))
            Dim verified = branchGroup.Sum(Function(r) Convert.ToInt32(r("VERIFIED")))
            Dim total = delivered + failed + forDelivery + verified

            Dim pct = Function(value As Integer) As String
                          If total = 0 Then Return "0%"
                          Return (value / total * 100).ToString("0.0") & "%"
                      End Function

            Dim headerRow = DTGDETAILS.Rows.Add(branchName,
                                             delivered, pct(delivered),
                                             failed, pct(failed),
                                             forDelivery, pct(forDelivery),
                                             verified, pct(verified),
                                             total)
            With DTGDETAILS.Rows(headerRow).DefaultCellStyle
                .Font = New Font(DTGDETAILS.Font, FontStyle.Bold)
                .BackColor = Color.LightGray
            End With

            For Each agentRow In branchGroup
                Dim agentId As String = agentRow.Field(Of Object)("AGENT_ID").ToString()
                Dim d = Convert.ToInt32(agentRow("DELIVERED"))
                Dim f = Convert.ToInt32(agentRow("FAILED"))
                Dim fd = Convert.ToInt32(agentRow("FOR_DELIVERY"))
                Dim v = Convert.ToInt32(agentRow("VERIFIED"))
                Dim t = d + f + fd + v

                Dim agentPct = Function(value As Integer) As String
                                   If t = 0 Then Return "0%"
                                   Return (value / t * 100).ToString("0.0") & "%"
                               End Function

                DTGDETAILS.Rows.Add("    " & agentId,
                                d, agentPct(d),
                                f, agentPct(f),
                                fd, agentPct(fd),
                                v, agentPct(v),
                                t)
            Next

        Next

    End Sub


    Private Sub LoadBranchSellerSummary2()

        Dim dt As New DataTable()

        Dim query As String = "
    SELECT 
        PRFR_Invoice_Detailed.BRANCH,
        PRFR_Invoice_Detailed.SELLER_NAME,
        SUM(CASE WHEN Dash_Plan_Batch_Details.STATUS = 'DELIVERED' THEN 1 ELSE 0 END) AS DELIVERED,
        SUM(CASE WHEN Dash_Plan_Batch_Details.STATUS = 'FAILED' THEN 1 ELSE 0 END) AS FAILED,
        SUM(CASE WHEN Dash_Plan_Batch_Details.STATUS = 'FOR DELIVERY' THEN 1 ELSE 0 END) AS FOR_DELIVERY,
        SUM(CASE WHEN Dash_Plan_Batch_Details.STATUS = 'VERIFIED' THEN 1 ELSE 0 END) AS VERIFIED
    FROM [dbo].[PRFR_Invoice_Detailed]
    LEFT JOIN Dash_Plan_Batch_Details 
        ON Dash_Plan_Batch_Details.INVOICE_NUMBER = PRFR_Invoice_Detailed.DOCUMENT_NUMBER 
        AND PRFR_Invoice_Detailed.DISTRIBUTOR_CODE = Dash_Plan_Batch_Details.COMPANY_ID
    WHERE Dash_Plan_Batch_Details.COMPANY_ID = @CompanyId AND Dash_Plan_Batch_Details.SITE_ID = @Siteid
      AND Dash_Plan_Batch_Details.DATE_TO_DELIVER BETWEEN @DateFrom AND @DateTo
    GROUP BY PRFR_Invoice_Detailed.BRANCH, PRFR_Invoice_Detailed.SELLER_NAME
    ORDER BY PRFR_Invoice_Detailed.BRANCH, PRFR_Invoice_Detailed.SELLER_NAME"

        Using conn As New SqlConnection(connStr)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@CompanyId", Form1.COMPANYID.Text)
                cmd.Parameters.AddWithValue("@Siteid", Form1.SITEID.Text)
                cmd.Parameters.AddWithValue("@DateFrom", DTFROM.Value.Date)
                cmd.Parameters.AddWithValue("@DateTo", DTTO.Value.Date)

                conn.Open()
                dt.Load(cmd.ExecuteReader())
            End Using
        End Using

        ' Debug: Show how many rows returned
        If dt.Rows.Count = 0 Then
            MessageBox.Show("No data returned for the selected filters.")
            Return
        End If

        ' Setup columns in DataGridView
        DTGDATA.Columns.Clear()
        DTGDATA.Rows.Clear()
        DTGDATA.Columns.Add("Label", "DETAILS")
        DTGDATA.Columns.Add("DELIVERED", "DELIVERED")
        DTGDATA.Columns.Add("DELIVERED_PCT", "DELIVERED %")
        DTGDATA.Columns.Add("FAILED", "FAILED")
        DTGDATA.Columns.Add("FAILED_PCT", "FAILED %")
        DTGDATA.Columns.Add("FOR_DELIVERY", "FOR DELIVERY")
        DTGDATA.Columns.Add("FOR_DELIVERY_PCT", "FOR DELIVERY %")
        DTGDATA.Columns.Add("VERIFIED", "VERIFIED")
        DTGDATA.Columns.Add("VERIFIED_PCT", "VERIFIED %")
        DTGDATA.Columns.Add("TOTAL", "Grand Total")

        ' Group by branch
        Dim branchGroups = dt.AsEnumerable().GroupBy(Function(r) r.Field(Of String)("BRANCH"))

        For Each branchGroup In branchGroups
            Dim branchName = branchGroup.Key

            Dim delivered = branchGroup.Sum(Function(r) Convert.ToInt32(r("DELIVERED")))
            Dim failed = branchGroup.Sum(Function(r) Convert.ToInt32(r("FAILED")))
            Dim forDelivery = branchGroup.Sum(Function(r) Convert.ToInt32(r("FOR_DELIVERY")))
            Dim verified = branchGroup.Sum(Function(r) Convert.ToInt32(r("VERIFIED")))
            Dim total = delivered + failed + forDelivery + verified

            Dim pct = Function(value As Integer) As String
                          If total = 0 Then Return "0%"
                          Return (value / total * 100).ToString("0.0") & "%"
                      End Function

            Dim headerRow = DTGDATA.Rows.Add(branchName,
                                             delivered, pct(delivered),
                                             failed, pct(failed),
                                             forDelivery, pct(forDelivery),
                                             verified, pct(verified),
                                             total)
            With DTGDATA.Rows(headerRow).DefaultCellStyle
                .Font = New Font(DTGDATA.Font, FontStyle.Bold)
                .BackColor = Color.LightGray
            End With

            For Each sellerRow In branchGroup
                Dim sellerName = sellerRow.Field(Of String)("SELLER_NAME")
                Dim d = Convert.ToInt32(sellerRow("DELIVERED"))
                Dim f = Convert.ToInt32(sellerRow("FAILED"))
                Dim fd = Convert.ToInt32(sellerRow("FOR_DELIVERY"))
                Dim v = Convert.ToInt32(sellerRow("VERIFIED"))
                Dim t = d + f + fd + v

                Dim sellerPct = Function(value As Integer) As String
                                    If t = 0 Then Return "0%"
                                    Return (value / t * 100).ToString("0.0") & "%"
                                End Function

                DTGDATA.Rows.Add("    " & sellerName,
                                d, sellerPct(d),
                                f, sellerPct(f),
                                fd, sellerPct(fd),
                                v, sellerPct(v),
                                t)
            Next
        Next
    End Sub

    Private Sub LoadBranchSellerSummaryDetails2()
        Dim dt1 As New DataTable()

        Dim query1 As String = "
SELECT 
    PRFR_Invoice_Detailed.BRANCH,
    Dash_Plan_Batch_Details.AGENT_ID,
    SUM(CASE WHEN Dash_Plan_Batch_Details.STATUS = 'DELIVERED' THEN 1 ELSE 0 END) AS DELIVERED,
    SUM(CASE WHEN Dash_Plan_Batch_Details.STATUS = 'FAILED' THEN 1 ELSE 0 END) AS FAILED,
    SUM(CASE WHEN Dash_Plan_Batch_Details.STATUS = 'FOR DELIVERY' THEN 1 ELSE 0 END) AS FOR_DELIVERY,
    SUM(CASE WHEN Dash_Plan_Batch_Details.STATUS = 'VERIFIED' THEN 1 ELSE 0 END) AS VERIFIED
FROM [dbo].[PRFR_Invoice_Detailed]
LEFT JOIN Dash_Plan_Batch_Details 
    ON Dash_Plan_Batch_Details.INVOICE_NUMBER = PRFR_Invoice_Detailed.DOCUMENT_NUMBER 
    AND PRFR_Invoice_Detailed.DISTRIBUTOR_CODE = Dash_Plan_Batch_Details.COMPANY_ID
WHERE Dash_Plan_Batch_Details.COMPANY_ID = @CompanyId AND Dash_Plan_Batch_Details.SITE_ID = @Siteid
  AND Dash_Plan_Batch_Details.DATE_TO_DELIVER BETWEEN @DateFrom AND @DateTo
GROUP BY PRFR_Invoice_Detailed.BRANCH, Dash_Plan_Batch_Details.AGENT_ID
ORDER BY PRFR_Invoice_Detailed.BRANCH, Dash_Plan_Batch_Details.AGENT_ID"

        Using conn1 As New SqlConnection(connStr1)
            Using cmd1 As New SqlCommand(query1, conn1)
                cmd1.Parameters.AddWithValue("@CompanyId", Form1.COMPANYID.Text)
                cmd1.Parameters.AddWithValue("@Siteid", Form1.SITEID.Text)
                cmd1.Parameters.AddWithValue("@DateFrom", DTFROM.Value.Date)
                cmd1.Parameters.AddWithValue("@DateTo", DTTO.Value.Date)

                conn1.Open()
                dt1.Load(cmd1.ExecuteReader())
            End Using
        End Using

        If dt1.Rows.Count = 0 Then
            MessageBox.Show("No data returned for the selected filters.")
            Return
        End If

        DTGDETAILS.Columns.Clear()
        DTGDETAILS.Rows.Clear()
        DTGDETAILS.Columns.Add("Label", "DETAILS")
        DTGDETAILS.Columns.Add("DELIVERED", "DELIVERED")
        DTGDETAILS.Columns.Add("DELIVERED_PCT", "DELIVERED %")
        DTGDETAILS.Columns.Add("FAILED", "FAILED")
        DTGDETAILS.Columns.Add("FAILED_PCT", "FAILED %")
        DTGDETAILS.Columns.Add("FOR_DELIVERY", "FOR DELIVERY")
        DTGDETAILS.Columns.Add("FOR_DELIVERY_PCT", "FOR DELIVERY %")
        DTGDETAILS.Columns.Add("VERIFIED", "VERIFIED")
        DTGDETAILS.Columns.Add("VERIFIED_PCT", "VERIFIED %")
        DTGDETAILS.Columns.Add("TOTAL", "Grand Total")

        Dim branchGroups = dt1.AsEnumerable().GroupBy(Function(r) r.Field(Of String)("BRANCH"))

        For Each branchGroup In branchGroups
            Dim branchName = branchGroup.Key

            Dim delivered = branchGroup.Sum(Function(r) Convert.ToInt32(r("DELIVERED")))
            Dim failed = branchGroup.Sum(Function(r) Convert.ToInt32(r("FAILED")))
            Dim forDelivery = branchGroup.Sum(Function(r) Convert.ToInt32(r("FOR_DELIVERY")))
            Dim verified = branchGroup.Sum(Function(r) Convert.ToInt32(r("VERIFIED")))
            Dim total = delivered + failed + forDelivery + verified

            Dim pct = Function(value As Integer) As String
                          If total = 0 Then Return "0%"
                          Return (value / total * 100).ToString("0.0") & "%"
                      End Function

            Dim headerRow = DTGDETAILS.Rows.Add(branchName,
                                             delivered, pct(delivered),
                                             failed, pct(failed),
                                             forDelivery, pct(forDelivery),
                                             verified, pct(verified),
                                             total)
            With DTGDETAILS.Rows(headerRow).DefaultCellStyle
                .Font = New Font(DTGDETAILS.Font, FontStyle.Bold)
                .BackColor = Color.LightGray
            End With

            For Each agentRow In branchGroup
                Dim agentId As String = agentRow.Field(Of Object)("AGENT_ID").ToString()
                Dim d = Convert.ToInt32(agentRow("DELIVERED"))
                Dim f = Convert.ToInt32(agentRow("FAILED"))
                Dim fd = Convert.ToInt32(agentRow("FOR_DELIVERY"))
                Dim v = Convert.ToInt32(agentRow("VERIFIED"))
                Dim t = d + f + fd + v

                Dim agentPct = Function(value As Integer) As String
                                   If t = 0 Then Return "0%"
                                   Return (value / t * 100).ToString("0.0") & "%"
                               End Function

                DTGDETAILS.Rows.Add("    " & agentId,
                                d, agentPct(d),
                                f, agentPct(f),
                                fd, agentPct(fd),
                                v, agentPct(v),
                                t)
            Next

        Next

    End Sub

    Private Sub EXPORT()

        Dim dt1 As DataGridView = DTGDATA
        Dim dt2 As DataGridView = DTGDETAILS
        Dim dt3 As DataGridView = DTGDELIVERYDETAILS

        Dim l As String = DTFROM.Value.ToString("yyyyMMdd")
        Dim lA As String = DTTO.Value.ToString("yyyyMMdd")
        Dim d As String = Form1.TIMETXT.Text.Replace(":", "").Replace("am", "").Replace("pm", "").Replace("AM", "").Replace("PM", "").Replace(" ", "")

        Dim sfd As New SaveFileDialog()
        sfd.Filter = "Excel Documents (.xlsx)|*.xlsx"
        sfd.FileName = "DeliveryResult_Status_" & l & "_" & lA & "_" & d & ".xlsx"

        If sfd.ShowDialog() = DialogResult.OK Then
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim xlexcel As New Excel.Application()

            xlexcel.DisplayAlerts = False
            Dim xlWorkBook As Excel.Workbook = xlexcel.Workbooks.Add(misValue)

            ' Export first DataGridView to the first worksheet
            Dim xlWorkSheet1 As Excel.Worksheet = xlWorkBook.Worksheets(1)
            xlWorkSheet1.Name = "DELIVERY RESULT PER PRESELLER"
            copyAlltoClipboard(dt1)
            xlWorkSheet1.Cells(1, 1).PasteSpecial(Excel.XlPasteType.xlPasteAll, Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, Type.Missing, Type.Missing)
            Clipboard.Clear()
            dt1.ClearSelection()

            ' Add and export second DataGridView to the second worksheet
            Dim xlWorkSheet2 As Excel.Worksheet = xlWorkBook.Worksheets.Add(misValue, misValue, misValue, misValue)
            xlWorkSheet2.Name = "DELIVERY RESULT PER AGENT"
            copyAlltoClipboard(dt2)
            xlWorkSheet2.Cells(1, 1).PasteSpecial(Excel.XlPasteType.xlPasteAll, Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, Type.Missing, Type.Missing)
            Clipboard.Clear()
            dt2.ClearSelection()

            ' Add and export second DataGridView to the second worksheet
            Dim xlWorkSheet3 As Excel.Worksheet = xlWorkBook.Worksheets.Add(misValue, misValue, misValue, misValue)
            xlWorkSheet3.Name = "DELIVERY RESULT SUMMARY"
            copyAlltoClipboard(dt3)
            xlWorkSheet3.Cells(1, 1).PasteSpecial(Excel.XlPasteType.xlPasteAll, Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, Type.Missing, Type.Missing)
            Clipboard.Clear()
            dt3.ClearSelection()


            ' Save the workbook as .xlsx
            xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, False, False, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
            xlexcel.DisplayAlerts = True
            xlWorkBook.Close(True, misValue, misValue)
            xlexcel.Quit()

            releaseObject(xlWorkSheet1)
            releaseObject(xlWorkSheet2)
            releaseObject(xlWorkSheet3)
            releaseObject(xlWorkBook)
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

    Private Sub GunaAdvenceButton2_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton2.Click

        If DTGDATA.Rows.Count > 0 Then

            EXPORT()

        Else

            MsgBox("NO DATA TO EXPORT", MsgBoxStyle.Exclamation, "SORRY")

        End If

    End Sub

End Class