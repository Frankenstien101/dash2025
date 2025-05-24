

Imports System.Data.SqlClient
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Reports_Delivery_Performance
    Private Sub Reports_Delivery_Performance_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DTFROM.Value = Form1.DTCALENDAR.Value
        DTTO.Value = Form1.DTCALENDAR.Value

    End Sub

    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click

        STATUS.Text = "GENERATING YOUR REPORT, PLEASE WAIT. . ."

        Try

            views("SELECT 
    COMPANY_ID, 
    SITE_ID,         -- Now from Dash_Customer_Master (c)
    SITE_NAME, 
    DATE_TO_DELIVER, 
    AGENT, 
    STORE_ENTRY, 
    STORE_EXIT, 
    STORE_TIME_SPENT, 
    CUSTOMER_ID, 
    CUSTOMER_NAME, 
    PHONE, 
    ADDRESS, 
    IMAGE1, 
    LATITUDE, 
    LONGITUDE, 
    STATUS,
    SUB_BATCH,
    SUB_DA,
    IS_RECEIVED,
    VEHICLE_IDS,
    IS_DROP_STATUS
FROM ( 
    SELECT 
        b.COMPANY_ID, 
        a.SITE_ID,                              -- Get SITE_ID from Dash_Customer_Master
        Dash_Sites.SITE_NAME, 
        b.DATE_TO_DELIVER, 
        a.AGENT, 
        d.STORE_ENTRY, 
        d.STORE_EXIT, 
        d.STORE_TIME_SPENT, 
        b.CUSTOMER_ID, 
        b.CUSTOMER_NAME, 
        c.PHONE, 
        c.ADDRESS, 
        c.IMAGE1, 
        c.LATITUDE, 
        c.LONGITUDE, 
        b.STATUS, 
        b.SUB_BATCH,
        b.SUB_DA,
        b.IS_RECEIVED,
        b.VEHICLE_IDS,
        b.IS_DROP_STATUS,
        ROW_NUMBER() OVER (
            PARTITION BY b.CUSTOMER_ID, b.COMPANY_ID 
            ORDER BY d.STORE_EXIT DESC
        ) AS rn 
    FROM 
        Dash_Plan_Batch_Transaction a 
    JOIN 
        Dash_Plan_Batch_Details b 
        ON a.BATCH_ID = b.BATCH AND a.COMPANY_ID = b.COMPANY_ID
    LEFT JOIN 
        Dash_Customer_Master c 
        ON c.CODE = b.CUSTOMER_ID AND c.COMPANY_ID = b.COMPANY_ID 
    LEFT JOIN 
        Dash_Agent_Performance_Detailed d 
        ON b.CUSTOMER_ID = d.STORE_CODE 
        AND b.DATE_TO_DELIVER = d.DELIVERY_DATE 
        AND b.COMPANY_ID = d.COMPANY_ID
    LEFT JOIN 
        Dash_Sites 
        ON Dash_Sites.SITE_ID = a.SITE_ID     -- Join based on c.SITE_ID
    WHERE 
        b.DATE_TO_DELIVER BETWEEN '" & DTFROM.Value & "' AND '" & DTTO.Value & "' 
        AND a.STATUS = 'PROCESSED' 
        AND b.COMPANY_ID = '" & Form1.COMPANYID.Text & "' 
) AS subquery 
WHERE rn = 1;
", "BSPIDB", DTGDETAILS)

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try


        Try

            views1("SELECT COMPANY_ID
                           ,SITE_ID
                           ,AGENT_ID
                           ,USERNAME
                           ,DELIVERY_DATE
                           ,ENTRY_BAT_PERCENTAGE
                           ,EXIT_BAT_PERCENTAGE
                           ,TIME_ENTRY
                           ,TIME_EXIT
                           ,STATUS
                           ,LOGIN_ID
                           ,TIME_SPENT FROM Dash_Agent_Performance_Summary WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND DELIVERY_DATE BETWEEN '" & DTFROM.Value & "' AND '" & DTTO.Value & "' AND STATUS = 'COMPLETE'
                           
                           GROUP BY
                           COMPANY_ID
                           ,SITE_ID
                           ,AGENT_ID
                           ,USERNAME
                           ,DELIVERY_DATE
                           ,ENTRY_BAT_PERCENTAGE
                           ,EXIT_BAT_PERCENTAGE
                           ,TIME_ENTRY
                           ,TIME_EXIT
                           ,STATUS
                           ,LOGIN_ID
                           ,TIME_SPENT", "BSPIDB", DTGTRANS)




        Catch ex As Exception

        End Try

        Try

            views2("SELECT 
                         [DISTRIBUTOR_CODE]
                         ,[BRANCH_CODE]
                         ,[BRANCH]
                    	  ,ORDER_DATE
                         ,[DATE] AS INVOICE_DATE
                    	  ,DATE_TO_DELIVER AS DATE_DELIVERED
                         ,[SALES_REP]
                         ,[SELLER_NAME]
                    	  ,AGENT_ID
                    	  ,BATCH
                    	  ,Dash_Plan_Batch_Details.STATUS
						  , CASE WHEN ISNULL([RETURN_AMOUNT], 0) = 0 THEN 'NO' ELSE 'YES' END AS HAS_RETURN
                         ,PRFR_Invoice_Detailed.CUSTOMER_ID
                         ,PRFR_Invoice_Detailed.CUSTOMER_NAME
                         ,[NAME] AS ITEM_ID
                         ,[SCHEME_CODE]
                         ,[SCHEME_SLAB_DESCRIPTION]
                         ,[SCHEME_GROUP_NAME]
                         ,PRFR_Invoice_Detailed.IT_BARCODE
                         ,[SW_BARCODE]
                         ,[DESCRIPTION]
                         ,[BRAND]
                         ,[ITEM_CATEGORY]
                         ,[BRANDFORM]
                         ,[TRADE_CHANNEL]
                         ,[DOCUMENT_NUMBER]
                         ,[CS]
                         ,[AMOUNT]
                         ,[DISCOUNT_VALUE]
                         ,[SCHEME_VALUE]
                         ,[SALES_EX_VAT]
                         ,[VAT_AMOUNT]
                         ,[SALES_AMOUNT],
                    	  ISNULL([QTY_RETURN], 0) AS QTY_RETURN,
                          ISNULL([RETURN_AMOUNT], 0) AS RETURN_AMOUNT
                        ,[MONTHLY_TRANSACTION]
                         ,[PG_LOCAL_SUBSEGMENT]
                         ,[SALES_SUPERVISOR]
                         ,[ITEM_QTY]
                         ,[GIV]
                         ,[NIV]
                         ,[ITEM_QTY_CS]
                         ,[ITEM_QTY_SW]
                         ,[ITEM_QTY_IT]
                    	  
                     FROM [dbo].[PRFR_Invoice_Detailed]

                     LEFT JOIN Dash_Plan_Batch_Details ON Dash_Plan_Batch_Details.INVOICE_NUMBER = PRFR_Invoice_Detailed.DOCUMENT_NUMBER AND PRFR_Invoice_Detailed.DISTRIBUTOR_CODE = Dash_Plan_Batch_Details.COMPANY_ID

                     LEFT JOIN Dash_Returns ON Dash_Returns.INVOICE_NUMBER = PRFR_Invoice_Detailed.DOCUMENT_NUMBER AND PRFR_Invoice_Detailed.DISTRIBUTOR_CODE = Dash_Returns.COMPANY_ID AND Dash_Returns.IT_BARCODE = PRFR_Invoice_Detailed.IT_BARCODE

                     WHERE  Dash_Plan_Batch_Details.COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND DATE_TO_DELIVER BETWEEN '" & DTFROM.Value & "' AND '" & DTTO.Value & "' ", "BSPIDB", DTGDATA)

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try


        Try

            views3("SELECT
      [COMPANY_ID]
      ,[SITE_ID]
      ,[UPLOAD_BY_USER_ID]
      ,[DIST_NAME]
      ,[BRANCH_NAME]
      ,[SELLER_TYPE]
      ,[SELLER_NAME]
      ,[CUSTOMER_NAME]
      ,[STORE_CODE]
      ,[CHANNEL_NAME]
      ,[SUB_CHANNEL_NAME]
      ,[ORDER_DATE]
      ,[ORDER_ID]
      ,[PRD_SKU_CODE]
      ,[PRD_SKU_NAME]
      ,[BARCODE]
      ,[CS_QTY]
      ,[QTY_PIECE]
      ,[PRICE_PIECE]
      ,[SCHEME_CODE]
      ,[SCHEME_DESC]
      ,[ORDER_VALUE_WITHOUTSCHEME]
      ,[SCHEME_VALUE]
      ,[ORDER_VALUE]
      ,[ORDER_SOURCE]
      ,[IS_PLAN]
  FROM [dbo].[PRFR_SO_UPLOAD] WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND ORDER_DATE BETWEEN '" & DTFROM.Value & "' AND '" & DTTO.Value & "'", "BSPIDB", DTGSO)

        Catch ex As Exception

        End Try


        ' Try
        '
        '     views3("SELECT 
        '                  [DISTRIBUTOR_CODE]
        '                  ,[BRANCH_CODE]
        '                  ,[BRANCH]
        '             	  ,ORDER_DATE
        '                  ,[DATE] AS INVOICE_DATE
        '             	  ,DATE_TO_DELIVER AS DATE_DELIVERED
        '                  ,[SALES_REP]
        '                  ,[SELLER_NAME]
        '             	  ,AGENT_ID
        '             	  ,BATCH
        '             	  ,Dash_Plan_Batch_Details.STATUS
        '				  , CASE WHEN ISNULL([RETURN_AMOUNT], 0) = 0 THEN 'NO' ELSE 'YES' END AS HAS_RETURN
        '                  ,PRFR_Invoice_Detailed.CUSTOMER_ID
        '                  ,PRFR_Invoice_Detailed.CUSTOMER_NAME
        '                  ,[DOCUMENT_NUMBER]
        '                  ,SUM(SALES_AMOUNT) AS SALES_AMOUNT
        '         
        '             	  
        '              FROM [dbo].[PRFR_Invoice_Detailed]
        '
        '              LEFT JOIN Dash_Plan_Batch_Details ON Dash_Plan_Batch_Details.INVOICE_NUMBER = PRFR_Invoice_Detailed.DOCUMENT_NUMBER AND PRFR_Invoice_Detailed.DISTRIBUTOR_CODE = Dash_Plan_Batch_Details.COMPANY_ID
        '
        '              LEFT JOIN Dash_Returns ON Dash_Returns.INVOICE_NUMBER = PRFR_Invoice_Detailed.DOCUMENT_NUMBER AND PRFR_Invoice_Detailed.DISTRIBUTOR_CODE = Dash_Returns.COMPANY_ID AND Dash_Returns.IT_BARCODE = PRFR_Invoice_Detailed.IT_BARCODE
        '
        '              WHERE  Dash_Plan_Batch_Details.COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND DATE_TO_DELIVER BETWEEN '" & DTFROM.Value & "' AND '" & DTTO.Value & "'
        '              
        '              GROUP BY
        '              [DISTRIBUTOR_CODE]
        '                  ,[BRANCH_CODE]
        '                  ,[BRANCH]
        '             	  ,ORDER_DATE
        '                  ,[DATE] AS INVOICE_DATE
        '             	  ,DATE_TO_DELIVER AS DATE_DELIVERED
        '                  ,[SALES_REP]
        '                  ,[SELLER_NAME]
        '             	  ,AGENT_ID
        '             	  ,BATCH
        '             	  ,Dash_Plan_Batch_Details.STATUS
        '				  , CASE WHEN ISNULL([RETURN_AMOUNT], 0) = 0 THEN 'NO' ELSE 'YES' END AS HAS_RETURN
        '                  ,PRFR_Invoice_Detailed.CUSTOMER_ID
        '                  ,PRFR_Invoice_Detailed.CUSTOMER_NAME
        '                  ,[DOCUMENT_NUMBER]
        '         
        '              ", "BSPIDB", DTGSUMMARY)
        '
        ' Catch ex As Exception
        '
        '     MsgBox(ex.ToString)
        '

        ' End Try

        Try

            views4("SELECT 
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
                    
                    ", "BSPIDB", DTGRESULTSUMMARY)

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

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

        Dim dt1 As DataGridView = DTGTRANS
        Dim dt2 As DataGridView = DTGDETAILS
        Dim dt3 As DataGridView = DTGDATA
        Dim dt4 As DataGridView = DTGSO
        Dim dt5 As DataGridView = DTGRESULTSUMMARY

        Dim l As String = DTFROM.Value.ToString("yyyyMMdd")
        Dim lA As String = DTTO.Value.ToString("yyyyMMdd")
        Dim d As String = Form1.TIMETXT.Text.Replace(":", "").Replace("am", "").Replace("pm", "").Replace("AM", "").Replace("PM", "").Replace(" ", "")

        Dim sfd As New SaveFileDialog()
        sfd.Filter = "Excel Documents (.xlsx)|*.xlsx"
        sfd.FileName = "DeliveryResult_" & l & "_" & lA & "_" & d & ".xlsx"

        If sfd.ShowDialog() = DialogResult.OK Then
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim xlexcel As New Excel.Application()

            xlexcel.DisplayAlerts = False
            Dim xlWorkBook As Excel.Workbook = xlexcel.Workbooks.Add(misValue)

            ' Export first DataGridView to the first worksheet
            Dim xlWorkSheet1 As Excel.Worksheet = xlWorkBook.Worksheets(1)
            xlWorkSheet1.Name = "AGENT PERFORMANCE SUMMARY"
            copyAlltoClipboard(dt1)
            xlWorkSheet1.Cells(1, 1).PasteSpecial(Excel.XlPasteType.xlPasteAll, Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, Type.Missing, Type.Missing)
            Clipboard.Clear()
            dt1.ClearSelection()

            ' Add and export second DataGridView to the second worksheet
            Dim xlWorkSheet2 As Excel.Worksheet = xlWorkBook.Worksheets.Add(misValue, misValue, misValue, misValue)
            xlWorkSheet2.Name = "AGENT PERFORMANCE DETAILED"
            copyAlltoClipboard(dt2)
            xlWorkSheet2.Cells(1, 1).PasteSpecial(Excel.XlPasteType.xlPasteAll, Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, Type.Missing, Type.Missing)
            Clipboard.Clear()
            dt2.ClearSelection()

            Dim xlWorkSheet3 As Excel.Worksheet = xlWorkBook.Worksheets.Add(misValue, misValue, misValue, misValue)
            xlWorkSheet3.Name = "DELIVERY RESULT SUMMARY"
            copyAlltoClipboard(dt5)
            xlWorkSheet3.Cells(1, 1).PasteSpecial(Excel.XlPasteType.xlPasteAll, Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, Type.Missing, Type.Missing)
            Clipboard.Clear()
            dt5.ClearSelection()

            ' Add and export third DataGridView to the third worksheet
            Dim xlWorkSheet4 As Excel.Worksheet = xlWorkBook.Worksheets.Add(misValue, misValue, misValue, misValue)
            xlWorkSheet4.Name = "DELIVERY RESULT DETAILED"
            copyAlltoClipboard(dt3)
            xlWorkSheet4.Cells(1, 1).PasteSpecial(Excel.XlPasteType.xlPasteAll, Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, Type.Missing, Type.Missing)
            Clipboard.Clear()
            dt3.ClearSelection()

            Dim xlWorkSheet5 As Excel.Worksheet = xlWorkBook.Worksheets.Add(misValue, misValue, misValue, misValue)
            xlWorkSheet5.Name = "SO REPORT"
            copyAlltoClipboard(dt4)
            xlWorkSheet5.Cells(1, 1).PasteSpecial(Excel.XlPasteType.xlPasteAll, Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, Type.Missing, Type.Missing)
            Clipboard.Clear()
            dt4.ClearSelection()


            ' Save the workbook as .xlsx
            xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, False, False, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
            xlexcel.DisplayAlerts = True
            xlWorkBook.Close(True, misValue, misValue)
            xlexcel.Quit()

            releaseObject(xlWorkSheet1)
            releaseObject(xlWorkSheet2)
            releaseObject(xlWorkSheet3)
            releaseObject(xlWorkSheet4)
            releaseObject(xlWorkSheet5)
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

End Class