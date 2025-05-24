Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.Office.Interop
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Public Class Reports_Payments
    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click

        DTGDATA.DataSource = Nothing

        If ALLSITECK.Checked = True Then


            Try

                views("SELECT
      [COMPANY_ID]
      ,[SITE_ID]
      ,[PAYMENT_ID]
      ,[PAYMENT_DATE]
      ,[CUSTOMER_ID]
      ,[CUSTOMER_NAME]
      ,[INVOICE_NUMBER]
      ,[AMOUNT]
      ,[PAYMENT_TYPE]
      ,[CHECK_NUMBER]
      ,[STATUS]
      ,[PROOF_OF_DELIVERY]
      ,[PROOF_OF_DELIVERY2]
      ,[COLLECTED_AMOUNT]
      ,[CREDIT_AMOUNT]
      ,[REMARKS]
      ,[AR_AMOUNT]
  FROM [dbo].[Dash_Payments] WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND PAYMENT_DATE BETWEEN '" & DTFROM.Value & "' AND '" & DTTO.Value & "'", "BSPIDB", DTGDATA)

            Catch EX As Exception

                MsgBox(EX.ToString)

            End Try

        Else

            Try

                views("SELECT
      [COMPANY_ID]
      ,[SITE_ID]
      ,[PAYMENT_ID]
      ,[PAYMENT_DATE]
      ,[CUSTOMER_ID]
      ,[CUSTOMER_NAME]
      ,[INVOICE_NUMBER]
      ,[AMOUNT]
      ,[PAYMENT_TYPE]
      ,[CHECK_NUMBER]
      ,[STATUS]
      ,[PROOF_OF_DELIVERY]
      ,[PROOF_OF_DELIVERY2]
      ,[COLLECTED_AMOUNT]
      ,[CREDIT_AMOUNT]
      ,[REMARKS]
      ,[AR_AMOUNT]
  FROM [dbo].[Dash_Payments] WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND PAYMENT_DATE BETWEEN '" & DTFROM.Value & "' AND '" & DTTO.Value & "'", "BSPIDB", DTGDATA)

            Catch EX As Exception

                MsgBox(EX.ToString)

            End Try


        End If

    End Sub

    Private Sub GunaAdvenceButton2_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton2.Click
        If DTGDATA.Rows.Count > 0 Then

            EXPORT()

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
        sfd.FileName = "PaymentReport" & l & "_" & lA & "_" & d & ".xlsx"

        If sfd.ShowDialog() = DialogResult.OK Then
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim xlexcel As New Excel.Application()

            xlexcel.DisplayAlerts = False
            Dim xlWorkBook As Excel.Workbook = xlexcel.Workbooks.Add(misValue)

            ' Export first DataGridView to the first worksheet
            Dim xlWorkSheet1 As Excel.Worksheet = xlWorkBook.Worksheets(1)
            xlWorkSheet1.Name = "Payment_Details"
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

    Private Sub Reports_Payments_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Form1.ROLETXT2.Text = "ADMIN" Then

            ALLSITECK.Visible = True

        Else

            ALLSITECK.Visible = False

        End If

    End Sub

End Class