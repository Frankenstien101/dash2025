
Imports System.Net
Imports System.Data.SqlClient

Public Class Transaction_cashier

    Public Function GETCMBAGENT() As String

        Return CMBAGENT.Text

    End Function

    Public Function GETBATCH() As String

        Return BATCH.Text


    End Function
    Private Sub GunaLabel2_Click(sender As Object, e As EventArgs) Handles GunaLabel2.Click

        ARAMOUNT.Visible = False
        Close()

    End Sub

    Private Sub Transaction_Cashier_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Form1.CMBCOLORTHEME.Text = "DEFAULT" Then

            '   Guna.FillColor = Color.FromArgb(192, 64, 0)
            '   Guna2GradientPanel1.FillColor2 = Color.FromArgb(255, 128, 0)

            Guna2Panel1.BackColor = Color.FromArgb(255, 128, 0)
            GunaAdvenceButton1.BaseColor = Color.FromArgb(192, 64, 0)
            GunaAdvenceButton2.BaseColor = Color.FromArgb(192, 64, 0)

            GunaGroupBox1.BorderColor = Color.FromArgb(192, 64, 0)
            GunaGroupBox1.LineColor = Color.FromArgb(192, 64, 0)

            GunaGroupBox2.BorderColor = Color.FromArgb(192, 64, 0)
            GunaGroupBox2.LineColor = Color.FromArgb(192, 64, 0)

            GunaGroupBox3.BorderColor = Color.FromArgb(192, 64, 0)
            GunaGroupBox3.LineColor = Color.FromArgb(192, 64, 0)

            GunaGroupBox4.BorderColor = Color.FromArgb(192, 64, 0)
            GunaGroupBox4.LineColor = Color.FromArgb(192, 64, 0)

            DTGBATCH.Theme = Guna.UI.WinForms.GunaDataGridViewPresetThemes.DeepOrange
            DTGINVOICES.Theme = Guna.UI.WinForms.GunaDataGridViewPresetThemes.DeepOrange

        ElseIf Form1.CMBCOLORTHEME.Text = "BLUE" Then

            Guna2Panel1.BackColor = Color.DarkBlue
            GunaAdvenceButton1.BaseColor = Color.DarkBlue
            GunaAdvenceButton2.BaseColor = Color.DarkBlue

            GunaGroupBox1.BorderColor = Color.DarkBlue
            GunaGroupBox1.LineColor = Color.DarkBlue

            GunaGroupBox2.BorderColor = Color.DarkBlue
            GunaGroupBox2.LineColor = Color.DarkBlue

            GunaGroupBox3.BorderColor = Color.DarkBlue
            GunaGroupBox3.LineColor = Color.DarkBlue

            GunaGroupBox4.BorderColor = Color.DarkBlue
            GunaGroupBox4.LineColor = Color.DarkBlue

            '  Guna2GradientPanel1.FillColor = Color.Navy
            '  Guna2GradientPanel1.FillColor2 = Color.FromArgb(192, 64, 0)

            DTGBATCH.Theme = Guna.UI.WinForms.GunaDataGridViewPresetThemes.Blue
            DTGINVOICES.Theme = Guna.UI.WinForms.GunaDataGridViewPresetThemes.Blue

        ElseIf Form1.CMBCOLORTHEME.Text = "GREEN" Then



        ElseIf Form1.CMBCOLORTHEME.Text = "DARK" Then



        ElseIf form1.CMBCOLORTHEME.Text = "LIGHT" Then



        End If

        DTDELIVERY.Value = Form1.DTCALENDAR.Value

        ARAMOUNT.Text = "0.00"
        ARAMOUNT.Visible = False

    End Sub

    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click

        Try

            views("SELECT BATCH_ID,NUM_OF_INVOICES FROM Dash_Plan_Batch_Transaction WHERE AGENT ='" & CMBAGENT.Text & "' AND VEHICLE_ID = '" & CMBVEHICLE.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND DATE_TO_DELIVER = '" & DTDELIVERY.Value & "'", "BSPIDB", DTGBATCH)

        Catch ex As Exception

            ' MsgBox(ex.ToString)

        End Try

    End Sub

    Private Sub CMBAGENT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMBAGENT.SelectedIndexChanged

        Try

            con.Open()

            Dim daA As New SqlDataAdapter("select VEHICLE_ID from Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND AGENT = '" & CMBAGENT.Text & "'  GROUP BY VEHICLE_ID", con)

            Dim dtA As New DataTable

            daA.Fill(dtA)

            CMBVEHICLE.DisplayMember = "VEHICLE_ID"

            CMBVEHICLE.DataSource = dtA

            con.Close()

        Catch ex As Exception

            '   MsgBox(ex.ToString)

        End Try

    End Sub

    Private Sub DTDELIVERY_ValueChanged(sender As Object, e As EventArgs) Handles DTDELIVERY.ValueChanged

        Try

            con.Open()

            Dim da As New SqlDataAdapter("select AGENT from Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND AGENT != '' GROUP BY AGENT", con)

            Dim dt As New DataTable

            da.Fill(dt)

            CMBAGENT.DisplayMember = "AGENT"

            CMBAGENT.DataSource = dt

            con.Close()

        Catch ex As Exception

            '  MsgBox(ex.ToString)

        End Try

    End Sub

    Private Sub DTGBATCH_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGBATCH.CellClick

        Try

            BATCH.Text = DTGBATCH.CurrentRow.Cells(0).Value

        Catch ex As Exception

        End Try

        Try

            views1("SELECT INVOICE_NUMBER,CUSTOMER_ID,CUSTOMER_NAME,TOTAL_AMOUNT FROM Dash_Plan_Batch_Details WHERE AGENT_ID ='" & CMBAGENT.Text & "' AND BATCH = '" & BATCH.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'DELIVERED'", "BSPIDB", DTGINVOICES)

        Catch ex As Exception

            '  MsgBox(ex.ToString)

        End Try

    End Sub

    Private Sub TXTSEARCH_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTSEARCH.KeyPress

        Try

            views1("SELECT INVOICE_NUMBER, CUSTOMER_ID, CUSTOMER_NAME, TOTAL_AMOUNT " &
       "FROM Dash_Plan_Batch_Details " &
       "WHERE AGENT_ID = '" & CMBAGENT.Text & "' " &
       "  AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' " &
       "  AND SITE_ID = '" & Form1.SITEID.Text & "' " &
       "  AND STATUS = 'DELIVERED' " &
       "  AND BATCH = '" & BATCH.Text & "' " &
       "  AND (INVOICE_NUMBER LIKE '%" & TXTSEARCH.Text & "%' " &
       "       OR CUSTOMER_ID LIKE '%" & TXTSEARCH.Text & "%' " &
       "       OR CUSTOMER_NAME LIKE '%" & TXTSEARCH.Text & "%')", "BSPIDB", DTGINVOICES)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub DTGINVOICES_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGINVOICES.CellClick

        STOREIMAGE.Image = Nothing
        PROOFIMG.Image = Nothing
        INVOICENUMBER.Text = ""
        STORECODE.Text = ""
        STORENAME.Text = ""
        AMOUNT.Text = ""
        REMARKS.Text = ""

        CHECKNUMBER.Text = ""
        COLLECTEDAMOUNT.Text = ""
        ' PROOFTXT.Text = DTGCHECK.CurrentRow.Cells(3).Value

        CHECKNUMTOUPDATE.Text = ""
        AMOUNTTOSAVE.Text = ""

        Try

            INVOICENUMBER.Text = DTGINVOICES.CurrentRow.Cells(0).Value
            STORECODE.Text = DTGINVOICES.CurrentRow.Cells(1).Value
            STORENAME.Text = DTGINVOICES.CurrentRow.Cells(2).Value
            AMOUNT.Text = DTGINVOICES.CurrentRow.Cells(3).Value

        Catch ex As Exception

        End Try

        Try

            views2("SELECT PAYMENT_TYPE,CHECK_NUMBER,COLLECTED_AMOUNT,PROOF_OF_DELIVERY FROM Dash_Payments WHERE INVOICE_NUMBER = '" & INVOICENUMBER.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "'  AND INVOICE_NUMBER = '" & INVOICENUMBER.Text & "'", "BSPIDB", DTGCHECK)

            PAYMENTTYPE.Text = DTGCHECK.CurrentRow.Cells(0).Value
            CHECKNUMBER.Text = DTGCHECK.CurrentRow.Cells(1).Value
            COLLECTEDAMOUNT.Text = DTGCHECK.CurrentRow.Cells(2).Value
            ' PROOFTXT.Text = DTGCHECK.CurrentRow.Cells(3).Value

            CHECKNUMTOUPDATE.Text = DTGCHECK.CurrentRow.Cells(1).Value
            AMOUNTTOSAVE.Text = DTGCHECK.CurrentRow.Cells(2).Value

        Catch ex As Exception

            '  MsgBox(ex.ToString)

        End Try

        Try

            views2("SELECT IMAGE1 FROM Dash_Customer_Master WHERE CODE = '" & STORECODE.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "'", "BSPIDB", DTGCHECK)
            PROOFTXT.Text = DTGCHECK.CurrentRow.Cells(0).Value

        Catch ex As Exception

        End Try

        Try

            views2("SELECT PROOF_OF_DELIVERY FROM Dash_Payments WHERE INVOICE_NUMBER = '" & INVOICENUMBER.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "'", "BSPIDB", DTGCHECK)
            PROOFTXT2.Text = DTGCHECK.CurrentRow.Cells(0).Value

        Catch ex As Exception

        End Try

        Try

            ''' PROOF 1

            Dim imageUrl1 As String = PROOFTXT.Text

            ' Create a WebClient to download the image
            Using webClient As New WebClient()
                ' Download the image bytes
                Dim imageBytes As Byte() = webClient.DownloadData(imageUrl1)

                Using ms As New IO.MemoryStream(imageBytes)

                    Dim img As Image = Image.FromStream(ms)

                    STOREIMAGE.Image = img
                End Using

            End Using

        Catch ex As Exception

        End Try

        ''' PROOF 2 

        Try
            PROOFTXT2.MaxLength = 10000

            Dim imageUrl2 As String = PROOFTXT2.Text

            ' Create a WebClient to download the image
            Using webClient As New WebClient()
                ' Download the image bytes
                Dim imageBytes2 As Byte() = webClient.DownloadData(imageUrl2)

                ' Create a MemoryStream from the downloaded bytes
                Using ms2 As New IO.MemoryStream(imageBytes2)
                    ' Create an Image object from the MemoryStream
                    Dim img2 As Image = Image.FromStream(ms2)

                    PROOFIMG.Image = img2

                End Using

            End Using

        Catch ex As Exception


        End Try

        CMBMETHOD.SelectedIndex = 0

    End Sub

    Private Sub CMBMETHOD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMBMETHOD.SelectedIndexChanged

        If CMBMETHOD.SelectedIndex = 1 Then

            CHECKNUMTOUPDATE.Enabled = True
            CHECKNUMTOUPDATE.Enabled = True
            AMOUNTTOSAVE.Enabled = True
            REMARKS.Text = ""
            REMARKS.Enabled = True

        ElseIf CMBMETHOD.SelectedIndex = 2 Then

            CHECKNUMTOUPDATE.Enabled = False
            AMOUNTTOSAVE.Enabled = False
            REMARKS.Text = "AR"
            REMARKS.Enabled = False

            ARAMOUNT.Visible = True
            ARAMOUNT.Text = "0.00"

        Else

            CHECKNUMTOUPDATE.Enabled = False
            CHECKNUMTOUPDATE.Enabled = True
            AMOUNTTOSAVE.Enabled = True
            REMARKS.Text = ""
            REMARKS.Enabled = True

        End If

    End Sub

    Private Sub GunaAdvenceButton2_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton2.Click

        If INVOICENUMBER.Text = "" Then

            MsgBox("PLEASE SELECT INVOICE TO VERIFY", MsgBoxStyle.Exclamation, "SORRY")

        Else

            Dim L As MsgBoxResult

            L = MsgBox("ARE YOU SURE TO VERIFY THIS PAYMENT?", MsgBoxStyle.YesNo, "CONFIRM")

            If L = MsgBoxResult.Yes Then

                Dim ISHAS As String

                Try

                    views3("SELECT INVOICE_NUMBER FROM Dash_Payments WHERE INVOICE_NUMBER = '" & INVOICENUMBER.Text & "'", "BSPIDB", DTGCHECK)
                    ISHAS = DTGCHECK.CurrentRow.Cells(0).Value

                Catch ex As Exception

                End Try

                If ISHAS = "" Then

                    Try

                        Dim kf As String = "INSERT INTO Dash_Payments(COMPANY_ID,SITE_ID,PAYMENT_ID,PAYMENT_DATE,CUSTOMER_ID,CUSTOMER_NAME,INVOICE_NUMBER,AMOUNT,PAYMENT_TYPE,STATUS,COLLECTED_AMOUNT,CREDIT_AMOUNT,AR_AMOUNT)" _
                             & "VALUES('" & Form1.COMPANYID.Text & "'," _
                            & "'" & Form1.SITEID.Text & "'," _
                              & "'-'," _
                                   & "'" & STORECODE.Text & "'," _
                              & "'" & STORENAME.Text & "'," _
                                   & "'" & INVOICENUMBER.Text & "'," _
                              & "'" & AMOUNTTOSAVE.Text & "'," _
                                   & "'CASH'," _
                              & "'DELIVERED'," _
                                   & "'" & AMOUNTTOSAVE.Text & "'," _
                              & "'0'," _
                                         & "'" & AMOUNTTOSAVE.Text & "')"

                        Data(kf)

                    Catch EX As Exception

                    End Try

                Else

                    Try

                        Dim K As String = "UPDATE Dash_Payments SET COLLECTED_AMOUNT = '" & AMOUNTTOSAVE.Text & "', AR_AMOUNT = '" & ARAMOUNT.Text & "' , PAYMENT_TYPE = '" & CMBMETHOD.Text & "' , CHECK_NUMBER = '" & CHECKNUMTOUPDATE.Text & "', STATUS = 'VERIFIED' , REMARKS = '" & REMARKS.Text & "' WHERE INVOICE_NUMBER = '" & INVOICENUMBER.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' "

                        Data(K)

                    Catch ex As Exception

                    End Try

                End If

                Try

                    Dim K As String = "UPDATE Dash_Plan_Batch_Details SET STATUS = 'VERIFIED' WHERE INVOICE_NUMBER = '" & INVOICENUMBER.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' "

                    Data(K)

                Catch ex As Exception

                End Try

                MsgBox("PAYMENT VERIFIED", MsgBoxStyle.Information, "COMPLETED")

                Try

                    views1("SELECT INVOICE_NUMBER,CUSTOMER_ID,CUSTOMER_NAME,TOTAL_AMOUNT FROM Dash_Plan_Batch_Details WHERE AGENT_ID ='" & CMBAGENT.Text & "' AND BATCH = '" & BATCH.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'DELIVERED'", "BSPIDB", DTGINVOICES)

                Catch ex As Exception

                    '  MsgBox(ex.ToString)

                End Try

                STOREIMAGE.Image = Nothing
                PROOFIMG.Image = Nothing

                INVOICENUMBER.Text = ""
                STORECODE.Text = ""
                STORENAME.Text = ""
                AMOUNT.Text = "0"
                AMOUNTTOSAVE.Text = "0"
                COLLECTEDAMOUNT.Text = "0"
                CHECKNUMBER.Text = ""
                CHECKNUMTOUPDATE.Text = ""
                REMARKS.Text = ""
                ARAMOUNT.Text = "0.00"
                ARAMOUNT.Visible = False

            End If

        End If

    End Sub

    Private Sub COLLECTEDAMOUNT_TextChanged(sender As Object, e As EventArgs) Handles COLLECTEDAMOUNT.TextChanged

        Try

            COLLECTEDAMOUNT.Text = Decimal.Parse(COLLECTEDAMOUNT.Text).ToString("n2")

        Catch ex As Exception

        End Try

    End Sub

    Private Sub SHOWVER_CheckedChanged(sender As Object, e As EventArgs) Handles SHOWVER.CheckedChanged

        If SHOWVER.Checked = True Then

            Try

                views1("SELECT INVOICE_NUMBER,CUSTOMER_ID,CUSTOMER_NAME,TOTAL_AMOUNT FROM Dash_Plan_Batch_Details WHERE AGENT_ID ='" & CMBAGENT.Text & "' AND BATCH = '" & BATCH.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'VERIFIED'", "BSPIDB", DTGINVOICES)

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try

        Else

            Try

                views1("SELECT INVOICE_NUMBER,CUSTOMER_ID,CUSTOMER_NAME,TOTAL_AMOUNT FROM Dash_Plan_Batch_Details WHERE AGENT_ID ='" & CMBAGENT.Text & "' AND BATCH = '" & BATCH.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'DELIVERED'", "BSPIDB", DTGINVOICES)

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try

        End If

    End Sub

    Private Sub GunaLinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles GunaLinkLabel1.LinkClicked

        If INVOICENUMBER.Text = "" Then

            MsgBox("PLEASE SELECT INVOICE TO VERIFY", MsgBoxStyle.Exclamation, "SORRY")

        Else

            Dim L As MsgBoxResult

            L = MsgBox("ARE YOU SURE TO SET AS FAILED THIS DELIVERY?", MsgBoxStyle.YesNo, "CONFIRM")

            If L = MsgBoxResult.Yes Then

                Try

                    Dim K As String = "UPDATE Dash_Payments SET COLLECTED_AMOUNT = '0.00' , PAYMENT_TYPE = '" & CMBMETHOD.Text & "' , CHECK_NUMBER = '" & CHECKNUMTOUPDATE.Text & "', STATUS = 'FAILED' WHERE INVOICE_NUMBER = '" & INVOICENUMBER.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' "

                    Data(K)

                Catch ex As Exception

                End Try

                Try

                    Dim K As String = "UPDATE Dash_Plan_Batch_Details SET STATUS = 'FAILED' WHERE INVOICE_NUMBER = '" & INVOICENUMBER.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' "

                    Data(K)

                Catch ex As Exception

                End Try

                MsgBox("PAYMENT SET AS FAILED", MsgBoxStyle.Information, "COMPLETED")

                Try

                    views1("SELECT INVOICE_NUMBER,CUSTOMER_ID,CUSTOMER_NAME,TOTAL_AMOUNT FROM Dash_Plan_Batch_Details WHERE AGENT_ID ='" & CMBAGENT.Text & "' AND BATCH = '" & BATCH.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'DELIVERED'", "BSPIDB", DTGINVOICES)

                Catch ex As Exception

                    '  MsgBox(ex.ToString)

                End Try

                STOREIMAGE.Image = Nothing
                PROOFIMG.Image = Nothing

                INVOICENUMBER.Text = ""
                STORECODE.Text = ""
                STORENAME.Text = ""
                AMOUNT.Text = "0"
                AMOUNTTOSAVE.Text = "0"
                COLLECTEDAMOUNT.Text = "0"
                CHECKNUMBER.Text = ""
                CHECKNUMTOUPDATE.Text = ""
                REMARKS.Text = ""

            End If

        End If

    End Sub


    Public Sub REF1()

        Try

            views1("SELECT INVOICE_NUMBER,CUSTOMER_ID,CUSTOMER_NAME,TOTAL_AMOUNT FROM Dash_Plan_Batch_Details WHERE AGENT_ID ='" & CMBAGENT.Text & "' AND BATCH = '" & BATCH.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'DELIVERED'", "BSPIDB", DTGINVOICES)

        Catch ex As Exception

            '  MsgBox(ex.ToString)

        End Try

    End Sub

    Private Sub GunaLinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles GunaLinkLabel2.LinkClicked

        Transaction_Cashier_Show_Failed.ShowDialog()

    End Sub

    Private Sub STORENAME_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Transaction_cashier_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.F1 Then
            GunaAdvenceButton2.PerformClick()
        End If

    End Sub
End Class