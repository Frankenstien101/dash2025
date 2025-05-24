
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Reports_Delivery_Route
    Private Sub GunaAdvenceButton2_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton2.Click

        Dim l As MsgBoxResult

        l = MsgBox("GENERATE AND DOWNLOAD REPORT?", MsgBoxStyle.YesNo, "CONFIRM")

        If l = MsgBoxResult.Yes Then

            If DTGDATA.Rows.Count = 0 Then

                MsgBox("NO DATA TO EXPORT", MsgBoxStyle.Exclamation, "SORRY")

            Else

                GunaAdvenceButton2.Enabled = False
                EXPORT()

            End If

        End If

    End Sub

    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click

        Try

            views("SELECT 
                    Dash_Plan_Batch_Details.COMPANY_ID
                    ,Dash_Plan_Batch_Details.SITE_ID
                    ,[BATCH]
                    ,[INVOICE_NUMBER]
                    ,[TOTAL_AMOUNT]
                    ,[INVOICE_VOLUME]
                    ,[DISTANCE]
                    ,[DISTANCE_IN_DECIMAL]
                    ,Dash_Plan_Batch_Details.STATUS
                    ,Dash_Plan_Batch_Details.DATE_TO_DELIVER
                    ,[STORE_LAT]
                    ,[STORE_LONG]
                    ,[CUSTOMER_ID]
                    ,[CUSTOMER_NAME]
                    ,[AGENT_ID]
                    ,VEHICLE_ID
                FROM [dbo].[Dash_Plan_Batch_Details]

            LEFT JOIN Dash_Plan_Batch_Transaction ON Dash_Plan_Batch_Transaction.BATCH_ID = Dash_Plan_Batch_Details.BATCH


            WHERE Dash_Plan_Batch_Details.COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND Dash_Plan_Batch_Details.SITE_ID = '" & Form1.SITEID.Text & "' AND Dash_Plan_Batch_Details.DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' ", "BSPIDB", DTGDATA)

        Catch ex As Exception

            MsgBox(ex.ToString)


        End Try
    End Sub

    Private Sub Reports_Delivery_Route_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DTDELIVERY.Value = Form1.DTCALENDAR.Value

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

    Private Sub copyAlltoClipboard(dt As DataGridView)

        dt.SelectAll()
        Clipboard.SetDataObject(dt.GetClipboardContent())
        dt.ClearSelection()

    End Sub

    Private Sub EXPORT()

        Dim dt As DataGridView
        Dim filenames As String
        dt = DTGDATA

        Dim l As String
        l = Form1.DTCALENDAR.Value

        l = l.Replace("/", "")

        Dim sfd As New SaveFileDialog()
        sfd.Filter = "Excel Documents (.xls)|.xls"
        sfd.FileName = filenames & "DELIVERYROUTE" & Form1.COMPANYID.Text & Form1.SITEID.Text & l & ".xls"

        If sfd.ShowDialog() = DialogResult.OK Then
            ' Copy DataGridView results to clipboard
            copyAlltoClipboard(dt)

            Dim misValue As Object = System.Reflection.Missing.Value
            Dim xlexcel As New Excel.Application()

            xlexcel.DisplayAlerts = False ' Without this you will get two confirm overwrite prompts
            Dim xlWorkBook As Excel.Workbook = xlexcel.Workbooks.Add(misValue)
            Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets(1)

            ' Format column D as text before pasting results, this was required for my data

            ' Paste clipboard results to worksheet range
            ' Dim CR As Excel.Range = xlWorkSheet.Cells(1, 1)
            xlWorkSheet.Cells(1, 1).PasteSpecial(Excel.XlPasteType.xlPasteAll, Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, Type.Missing, Type.Missing)

            ' For some reason column A is always blank in the worksheet. ¯\(ツ)/¯
            ' Delete blank column A and select cell A1
            ' Dim delRng As Excel.Range = xlWorkSheet.Range("A:A")
            ' delRng.Delete(Excel.XlDeleteShiftDirection.xlShiftToLeft)
            ' xlWorkSheet.Range("A1").Select()

            ' Save the excel file under the captured location from the SaveFileDialog
            xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
            xlexcel.DisplayAlerts = True
            xlWorkBook.Close(True, misValue, misValue)
            xlexcel.Quit()

            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlexcel)

            ' Clear Clipboard and DataGridView selection
            Clipboard.Clear()
            dt.ClearSelection()

            If MessageBox.Show("Download Complete! Would you like to open the file?", "Successfully Saved!", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                If File.Exists(sfd.FileName) Then
                    System.Diagnostics.Process.Start(sfd.FileName)
                End If

            Else
                ' Do nothing
            End If
            ' Open the newly saved excel file
        End If

        GunaAdvenceButton2.Enabled = True

    End Sub


End Class