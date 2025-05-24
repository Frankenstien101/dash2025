Public Class Transaction_cashier_Show_Failed

    Private Sub GunaLabel2_Click(sender As Object, e As EventArgs) Handles GunaLabel2.Click

        Transaction_cashier.REF1()
        Close()

    End Sub

    Dim LINEID As String

    Private Sub Transaction_cashier_Show_Failed_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Dim salesTab As TabPage = Nothing
        Dim salesForm As Transaction_cashier = Nothing

        ' Find the "SalesOrder" tab inside TabControl1
        For Each tab As TabPage In Form1.PANELMAIN.TabPages ' Assuming TabControl1 is on Form1
            If tab.Text = "CASHIER" Then
                salesTab = tab
                salesForm = TryCast(tab.Controls(0), Transaction_cashier)
                Exit For
            End If
        Next

        ' If SalesOrder exists, retrieve the TextBox1 value
        If salesForm IsNot Nothing Then

            Dim valueFromSalesOrder As String = salesForm.GETBATCH()
            Dim valueFromSalesOrder1 As String = salesForm.GETCMBAGENT()


            BATCH.Text = valueFromSalesOrder
            CMBAGENT.Text = valueFromSalesOrder1


            ' MessageBox.Show("Value from SalesOrder: " & valueFromSalesOrder & "--" & valueFromSalesOrder1, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("SalesOrder form is not open!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End If


        Try

            views2("SELECT LINEID,INVOICE_NUMBER,CUSTOMER_ID,CUSTOMER_NAME,TOTAL_AMOUNT,DATE_TO_DELIVER FROM Dash_Plan_Batch_Details WHERE AGENT_ID ='" & CMBAGENT.Text & "' AND BATCH = '" & BATCH.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'FAILED'", "BSPIDB", DTGDATA)
            DTGDATA.Columns(0).Visible = False
            DTGDATA.Columns(5).Visible = False

        Catch ex As Exception

            '  MsgBox(ex.ToString)

        End Try

    End Sub

    Private Sub Transaction_cashier_Show_Failed_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            views2("SELECT LINEID,INVOICE_NUMBER,CUSTOMER_ID,CUSTOMER_NAME,TOTAL_AMOUNT,DATE_TO_DELIVER FROM Dash_Plan_Batch_Details WHERE AGENT_ID ='" & CMBAGENT.Text & "' AND BATCH = '" & BATCH.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'FAILED'", "BSPIDB", DTGDATA)
            DTGDATA.Columns(0).Visible = False
            DTGDATA.Columns(5).Visible = False

        Catch ex As Exception

            '  MsgBox(ex.ToString)

        End Try

    End Sub

    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click

        If LINEID = "" Then

            MsgBox("SELECT FROM LINE", MsgBoxStyle.Exclamation, "SORRY")

        Else

            Dim K As MsgBoxResult

            K = MsgBox("ARE YOU SURE TO SET THIS INVOICE AS DELIVERED?", MsgBoxStyle.YesNo, "CONFIRM")

            If K = MsgBoxResult.Yes Then

                Try

                    Dim O As String = "UPDATE Dash_Batch_Details SET STATUS = 'DELIVERED' WHERE LINEID = '" & LINEID & "'"

                    Data(O)

                Catch ex As Exception

                End Try

                Dim ISHAS As String

                Try

                    views3("SELECT INVOICE_NUMBER FROM Dash_Payments WHERE INVOICE_NUMBER = '" & INV & "'", "BSPIDB", DTGCHECK)
                    ISHAS = DTGCHECK.CurrentRow.Cells(0).Value

                Catch ex As Exception

                End Try

                If ISHAS = "" Then

                    Try

                        Dim kf As String = "INSERT INTO Dash_Payments(COMPANY_ID,SITE_ID,PAYMENT_ID,PAYMENT_DATE,CUSTOMER_ID,CUSTOMER_NAME,INVOICE_NUMBER,AMOUNT,PAYMENT_TYPE,STATUS,COLLECTED_AMOUNT,CREDIT_AMOUNT,AR_AMOUNT)" _
                               & "VALUES('" & Form1.COMPANYID.Text & "'," _
                              & "'" & Form1.SITEID.Text & "'," _
                               & "'" & DTTODAY & "'," _
                                & "'-'," _
                                     & "'" & CUSID & "'," _
                                & "'" & CUSNAME & "'," _
                                     & "'" & INV & "'," _
                                & "'" & TOT & "'," _
                                     & "'CASH'," _
                                & "'DELIVERED'," _
                                     & "'" & TOT & "'," _
                                & "'0'," _
                                           & "'" & TOT & "')"

                        Data(kf)

                    Catch EX As Exception

                    End Try

                Else

                    Try

                        Dim o As String = "UPDATE Dash_Payments SET PAYMENT_TYPE = 'CASH',STATUS = 'DELIVERED' WHERE INVOICE_NUMBER = '" & INV & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "'"

                        Data(o)

                    Catch ex As Exception

                        MsgBox(ex.ToString)

                    End Try

                End If

                Try

                    views2("SELECT LINEID,INVOICE_NUMBER,CUSTOMER_ID,CUSTOMER_NAME,TOTAL_AMOUNT,DATE_TO_DELIVER FROM Dash_Plan_Batch_Details WHERE AGENT_ID ='" & CMBAGENT.Text & "' AND BATCH = '" & BATCH.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'FAILED'", "BSPIDB", DTGDATA)
                    DTGDATA.Columns(0).Visible = False
                    DTGDATA.Columns(5).Visible = False

                Catch ex As Exception

                    '  MsgBox(ex.ToString)

                End Try

                MsgBox("INVOICE SET TO DELIVERED", MsgBoxStyle.Information, "COMPLETE")

                Try

                    views2("SELECT LINEID,INVOICE_NUMBER,CUSTOMER_ID,CUSTOMER_NAME,TOTAL_AMOUNT,DATE_TO_DELIVER FROM Dash_Plan_Batch_Details WHERE AGENT_ID ='" & CMBAGENT.Text & "' AND BATCH = '" & BATCH.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'FAILED'", "BSPIDB", DTGDATA)
                    DTGDATA.Columns(0).Visible = False
                    DTGDATA.Columns(5).Visible = False

                Catch ex As Exception

                    '  MsgBox(ex.ToString)

                End Try

            End If

        End If

    End Sub

    Dim CUSNAME As String
    Dim CUSID As String
    Dim TOT As String
    Dim INV As String
    Dim DTTODAY As String

    Private Sub DTGDATA_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGDATA.CellClick

        LINEID = ""

        Try

            LINEID = DTGDATA.CurrentRow.Cells(0).Value
            INV = DTGDATA.CurrentRow.Cells(1).Value
            CUSID = DTGDATA.CurrentRow.Cells(2).Value
            CUSNAME = DTGDATA.CurrentRow.Cells(3).Value
            TOT = DTGDATA.CurrentRow.Cells(4).Value
            DTTODAY = DTGDATA.CurrentRow.Cells(5).Value

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

    End Sub

End Class