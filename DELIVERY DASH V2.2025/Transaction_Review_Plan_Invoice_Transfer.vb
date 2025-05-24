

Imports System.Data.SqlClient

Public Class Transaction_Review_Plan_Invoice_Transfer
    Private Sub GunaLabel2_Click(sender As Object, e As EventArgs) Handles GunaLabel2.Click

        Try

            Dim U As String = "UPDATE Dash_Plan_Batch_Details SET IS_MOVED = '0' WHERE BATCH = '" & BATCHID.Text & "'"

            Data(U)

        Catch ex As Exception

        End Try

        Close()

    End Sub

    Private Sub Transaction_Merge_With_Show_On_Mapped_Invoice_UnMerged_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim salesTab As TabPage = Nothing
        Dim salesForm As Transaction_Review_Plan = Nothing

        ' Find the "SalesOrder" tab inside TabControl1
        For Each tab As TabPage In Form1.PANELMAIN.TabPages ' Assuming TabControl1 is on Form1
            If tab.Text = "REVIEWPLAN" Then
                salesTab = tab
                salesForm = TryCast(tab.Controls(0), Transaction_Review_Plan)
                Exit For
            End If
        Next

        ' If SalesOrder exists, retrieve the TextBox1 value
        If salesForm IsNot Nothing Then

            Dim valueFromSalesOrder As String = salesForm.GETBATCHNUMBER()


            BATCHID.Text = valueFromSalesOrder


            ' MessageBox.Show("Value from SalesOrder: " & valueFromSalesOrder & "--" & valueFromSalesOrder1, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("SalesOrder form is not open!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        '  BATCHID.Text = Transaction_Review_Plan.GETBATCHNUMBER

        BindDataToGridView()

        DTGDATA.Columns(1).ReadOnly = True
        DTGDATA.Columns(2).ReadOnly = True
        DTGDATA.Columns(3).ReadOnly = True

    End Sub

    Private Function GetDataFromDatabase() As DataTable

        Dim dataTable11 As New DataTable()
        Dim query11 As String = "SELECT INVOICE_NUMBER,CUSTOMER_ID,CUSTOMER_NAME,LINEID  FROM Dash_Plan_Batch_Details WHERE BATCH = '" & BATCHID.Text & "'"

        Using command As New SqlCommand(query11, con111)
            con111.Open()
            Dim adapter11 As New SqlDataAdapter(command)
            adapter11.Fill(dataTable11)
            con111.Close()

        End Using

        Return dataTable11

    End Function

    Private Sub BindDataToGridView()

        DTGDATA.DataSource = GetDataFromDatabase()
        DTGDATA.Columns(1).ReadOnly = True
        DTGDATA.Columns(2).ReadOnly = True

        DTGDATA.Columns(3).ReadOnly = True

        DTGDATA.Columns(4).Visible = False

    End Sub

    Private Sub DTGDATA_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DTGDATA.CellValueChanged

        If e.ColumnIndex = 0 AndAlso e.RowIndex <> -1 Then ' Assuming the checkbox column is at index 0

            Dim checkboxCell As DataGridViewCheckBoxCell = CType(DTGDATA.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCheckBoxCell)
            Dim isChecked As Boolean = CBool(checkboxCell.Value)
            ' Perform actions based on checkbox state

            '  MsgBox(isChecked.ToString)

            If isChecked.ToString = "True" Then

                Try

                    Dim U As String = "UPDATE Dash_Plan_Batch_Details SET IS_MOVED = '1' WHERE LINEID = '" & DTGDATA.CurrentRow.Cells(4).Value & "'"

                    Data(U)

                Catch ex As Exception

                End Try

            Else

                Try

                    Dim U As String = "UPDATE Dash_Plan_Batch_Details SET IS_MOVED = '0' WHERE LINEID = '" & DTGDATA.CurrentRow.Cells(4).Value & "'"

                    Data(U)

                Catch ex As Exception

                End Try

            End If

        End If

    End Sub

    Dim NUMOFINVOICE As Integer

    Dim SUMOFINVOICE As Decimal

    Dim COUNTLINE As Decimal

    Dim DTORDER As Date

    Dim DTFELIVER As Date

    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click

        Dim O As MsgBoxResult

        O = MsgBox("ARE YOU SURE TO TRANSFER IN NEW BATCH?", MsgBoxStyle.YesNo, "CONFIRM")

        If O = MsgBoxResult.Yes Then

            Try

                Dim BATCHNUM As Integer

                views3("SELECT COUNT(COMPANY_ID) FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "'", "BSPIDB", DTGLIST)

                CHECKID.Text = DTGLIST.CurrentRow.Cells(0).Value

                BATCHNUM = CHECKID.Text

                NEWBATCH.Text = "DLV" & Form1.COMPANYID.Text & Form1.SITEID.Text & "00" & BATCHNUM + 1

            Catch ex As Exception

                MsgBox("1" & ex.ToString)

            End Try


            '' COUNT INV
            Try

                views3("SELECT COUNT(INVOICE_NUMBER) FROM Dash_Plan_Batch_Details WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND BATCH = '" & BATCHID.Text & "' AND IS_MOVED = '1'", "BSPIDB", DTGLIST)
                NUMOFINVOICE = DTGLIST.CurrentRow.Cells(0).Value

            Catch ex As Exception

                MsgBox("3" & ex.ToString)

            End Try

            '' SUM TOTAL
            Try

                views3("SELECT SUM(TOTAL_AMOUNT) FROM Dash_Plan_Batch_Details WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND BATCH = '" & BATCHID.Text & "' AND IS_MOVED = '1'", "BSPIDB", DTGLIST)
                SUMOFINVOICE = DTGLIST.CurrentRow.Cells(0).Value

            Catch ex As Exception

                '   MsgBox("4" & ex.ToString)

            End Try

            ' GET DATE ORDER DELIVER
            Try

                views3("SELECT ORDER_DATE,DATE_TO_DELIVER FROM Dash_Plan_Batch_Details WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND BATCH = '" & BATCHID.Text & "' AND IS_MOVED = '1'", "BSPIDB", DTGLIST)
                DTORDER = DTGLIST.CurrentRow.Cells(0).Value
                DTFELIVER = DTGLIST.CurrentRow.Cells(1).Value

            Catch ex As Exception

                ' MsgBox("5" & ex.ToString)

            End Try


            ' INSERT NEW BATCH
            Try

                Dim kf1 As String = "INSERT INTO Dash_Plan_Batch_Transaction(COMPANY_ID,SITE_ID,BATCH_ID,DROP_COUNT,NUM_OF_INVOICES,TOTAL_VALUE,TOTAL_VOLUME,WEIGHT,STATUS,VEHICLE_ID,AGENT,DATE_TO_DELIVER,ORDER_DATE)" _
               & "VALUES('" & Form1.COMPANYID.Text & "'," _
              & "'" & Form1.SITEID.Text & "'," _
                & "'" & NEWBATCH.Text & "'," _
                    & "'" & NUMOFINVOICE & "'," _
                      & "'" & NUMOFINVOICE & "'," _
                    & " '" & SUMOFINVOICE & "', " _
                    & "'0.00'," _
                        & "'0.00'," _
                             & "'READY'," _
                                 & "'-'," _
                                   & "''," _
                                        & "'" & DTFELIVER & "'," _
                           & "'" & DTORDER & "')"

                Data(kf1)

                '  MsgBox("SAVE TRANSACTION")

            Catch ex As Exception

                MsgBox("6" & ex.ToString)

            End Try

            ' UPDATE BATCH NUMBER FOR NEW

            Try

                Dim UPNEW As String = "UPDATE Dash_Plan_Batch_Details SET BATCH = '" & NEWBATCH.Text & "' WHERE BATCH = '" & BATCHID.Text & "' AND IS_MOVED = '1'"

                Data(UPNEW)

            Catch ex As Exception

                MsgBox("2" & ex.ToString)

            End Try

            '' UPDATE TRANSFERED LINES
            Try

                Dim UPNEW As String = "UPDATE Dash_Plan_Batch_Details SET IS_MOVED = '0' WHERE BATCH = '" & NEWBATCH.Text & "'"

                Data(UPNEW)

            Catch ex As Exception

                MsgBox("2" & ex.ToString)

            End Try

            COUNTLINE = DTGLIST.Rows.Count()
            '' LESS ON ORIGINAL BATCH
            Try

                Dim UPNEW As String = "UPDATE Dash_Plan_Batch_Transaction SET NUM_OF_INVOICES -= '" & COUNTLINE & "'   WHERE BATCH_ID = '" & BATCHID.Text & "'"

                Data(UPNEW)

            Catch ex As Exception

                MsgBox("7" & ex.ToString)

            End Try

            MsgBox("INVOICE SEPARATED", MsgBoxStyle.Information, "COMPLETE")

            Dim salesTab As TabPage = Nothing

            ' Find the "SalesOrder" tab inside TabControl1
            For Each tab As TabPage In Form1.PANELMAIN.TabPages ' Assuming TabControl1 is on Form1
                If tab.Text = "REVIEWPLAN" Then
                    salesTab = tab
                    Exit For
                End If
            Next

            ' If the SalesOrder tab exists, update TextBox1
            If salesTab IsNot Nothing Then
                Dim salesForm As Transaction_Review_Plan = TryCast(salesTab.Controls(0), Transaction_Review_Plan)
                If salesForm IsNot Nothing Then

                    salesForm.TriggerButtonREF() ' Pass the new value
                    Form1.PANELMAIN.SelectedTab = salesTab ' Switch to SalesOrder tab

                End If
            Else
                MessageBox.Show("SalesOrder form is not open!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            Close()

        End If

    End Sub


End Class