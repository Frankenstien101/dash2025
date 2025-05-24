
Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class Reports_Order_Plan_Re_Assign_Per_Seller
    Private Sub GunaLabel2_Click(sender As Object, e As EventArgs) Handles GunaLabel2.Click

        Close()

    End Sub

    Private Sub Reports_Order_Plan_Re_Assign_Per_Seller_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim salesTab As TabPage = Nothing
        Dim salesForm As Reports_Order_Plan = Nothing

        ' Find the "SalesOrder" tab inside TabControl1
        For Each tab As TabPage In Form1.PANELMAIN.TabPages ' Assuming TabControl1 is on Form1
            If tab.Text = "ORDERREPORT" Then
                salesTab = tab
                salesForm = TryCast(tab.Controls(0), Reports_Order_Plan)
                Exit For
            End If
        Next

        ' If SalesOrder exists, retrieve the TextBox1 value
        If salesForm IsNot Nothing Then

            Dim valueFromSalesOrder As String = salesForm.GETDELIVERYDATE()


            DTDELIVERY.Value = valueFromSalesOrder


            ' MessageBox.Show("Value from SalesOrder: " & valueFromSalesOrder & "--" & valueFromSalesOrder1, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("SalesOrder form is not open!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End If

        COMPANYID = Form1.COMPANYID.Text
        SITEID.Text = Form1.SITEID.Text

        CKLIST.Items.Clear()

        Try

            con.Open()

            Dim cmd As SqlCommand = New SqlCommand("select SELLER_NAME from PRFR_SO_UPLOAD WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND ORDER_DATE = '" & DTDELIVERY.Value & "' GROUP BY SELLER_NAME ", con)
            Dim rd As SqlDataReader = cmd.ExecuteReader()

            While rd.Read()

                CKLIST.Items.Add(rd("SELLER_NAME").ToString(), CheckState.Unchecked)

            End While

            con.Close()

        Catch ex As Exception

        End Try

        Try

            views1("SELECT SO_PLAN_NUMBER ,VEHICLE_IDS FROM Dash_SO_Plan_Batch_Details WHERE ORDER_DATE ='" & DTDELIVERY.Value & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' GROUP BY SO_PLAN_NUMBER,VEHICLE_IDS", "BSPIDB", DTGPLANS)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click

        SEARCH.Text = ""

        For Each value As String In CKLIST.CheckedItems

            SEARCH.Text = SEARCH.Text & "'" & value & "',"

        Next

        SEARCH.Text = SEARCH.Text.Remove(SEARCH.Text.Length - 1)

        If SEARCH.Text.Trim = "" Then

            MsgBox("PLEASE SELECT FROM SELLER", MsgBoxStyle.Exclamation, "SORRY")

        ElseIf SOPLANNUMBER.Text = "" Then

            MsgBox("PLEASE SELECT PLAN NUMBER", MsgBoxStyle.Exclamation, "SORRY")

        Else

            Dim K As MsgBoxResult

            K = MsgBox("ARE YOU SURE TO ASSIGN SELLER TO THIS BATCH NUMBER?", MsgBoxStyle.YesNo, "CONFIRM")

            If K = MsgBoxResult.Yes Then

                Try

                    views2("SELECT STORE_CODE FROM PRFR_SO_UPLOAD WHERE ORDER_DATE ='" & DTDELIVERY.Value & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND SELLER_NAME IN (" & SEARCH.Text & ") GROUP BY STORE_CODE  ", "BSPIDB", DTGCHECK)

                Catch ex As Exception

                End Try

                COMPANYID = Form1.COMPANYID.Text
                SITE_ID = Form1.SITEID.Text
                DTDEL = DTDELIVERY.Value

                Me.Enabled = False
                BackgroundWorker1.RunWorkerAsync()

            End If

        End If

    End Sub

    Private Sub DTGPLANS_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGPLANS.CellContentClick

        Try

            SOPLANNUMBER.Text = DTGPLANS.CurrentRow.Cells(0).Value

        Catch ex As Exception

        End Try

    End Sub

    Dim COMPANYID As String

    Dim SITE_ID As String

    Dim DTDEL As Date

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        For i = 0 To DTGCHECK.Rows.Count - 1 Step +1

            If DTGCHECK.Rows.Count > 1 Then

                Dim x As Decimal
                'Dim y As Decimal
                Dim z As Decimal
                Dim a As Decimal

                x = DTGCHECK.Rows.Count - 1

                z = i / x

                a = z * 100

                GunaProgressBar1.Value = a
                '  percentlbl.Text = a

            Else

                GunaProgressBar1.Value = 100
                ' percentlbl.Text = "100%"

            End If

            Try

                Dim A As String = "UPDATE Dash_SO_Plan_Batch_Details SET SO_PLAN_NUMBER = '" & SOPLANNUMBER.Text & "' WHERE ORDER_DATE ='" & DTDEL & "' AND COMPANY_ID = '" & COMPANYID & "' AND SITE_ID = '" & SITE_ID & "' AND CUSTOMER_ID = '" & DTGCHECK.Rows(i).Cells(0).Value & "'"

                Data(A)

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try

        Next

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        Me.Enabled = True

        MsgBox("SELLER AND BATCH ASSIGNED!", MsgBoxStyle.Information, "COMPLETE")

        SOPLANNUMBER.Text = ""

    End Sub

    Private Sub DTGPLANS_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGPLANS.CellDoubleClick

        Try

            Transaction_Order_Plan_Re_Assign_Per_Seller_Set_Sub_Batch.BATCHNUMBER.Text = DTGPLANS.CurrentRow.Cells(0).Value
            Transaction_Order_Plan_Re_Assign_Per_Seller_Set_Sub_Batch.ShowDialog()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GunaAdvenceButton2_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton2.Click

        Dim K As MsgBoxResult

        K = MsgBox("ARE YOU SURE TO CREATE NEW BATCH?", MsgBoxStyle.YesNo, "CONFIRM")

        If K = MsgBoxResult.Yes Then

            Dim timenosep As String

            Try

                Dim l As String
                l = Form1.TIMETXT.Text

                l = l.Replace(":", "")
                l = l.Replace("AM", "")
                l = l.Replace("PM", "")
                l = l.Replace("am", "")
                l = l.Replace("pm", "")
                l = l.Replace(" ", "")

                timenosep = l

                ' MsgBox(timenosep.ToString)

            Catch ex As Exception

            End Try


            Try

                Dim BATCHNUM As Integer

                views2("SELECT COUNT(COMPANY_ID) FROM Dash_SO_Plan_Transaction WHERE COMPANY_ID = '" & COMPANYID & "' AND SITE_ID = '" & SITEID.Text & "'", "BSPIDB", DTGCHECK)

                CHECKID.Text = DTGCHECK.CurrentRow.Cells(0).Value

                BATCHNUM = CHECKID.Text

                BATCHNUMBER.Text = "SOPLN" & COMPANYID & SITEID.Text & "00" & timenosep & BATCHNUM + 1

            Catch ex As Exception

                MsgBox("1" & ex.ToString)

            End Try

            Try

                Dim TOTVAL As Decimal
                Dim TOTDEC As Decimal

                TOTVAL = 0
                TOTDEC = 0

                Dim kf1 As String = "INSERT INTO Dash_SO_Plan_Transaction(COMPANY_ID,SITE_ID,SO_PLAN_NUMBER,DATE_SO,VEHICLE_ID,SO_PICK_BATCH,STATUS)" _
                     & "VALUES('" & COMPANYID & "'," _
                    & "'" & SITEID.Text & "'," _
                      & "'" & BATCHNUMBER.Text & "'," _
                          & "'" & DTDELIVERY.Value & "'," _
                             & "''," _
                        & "'0'," _
                                 & "'PLANNED')"

                Data(kf1)

                '  MsgBox("NEW VEH")

                '   MsgBox("SAVE TRANSACTION")

            Catch ex As Exception

                MsgBox("4" & ex.ToString)

            End Try

            Try

                Dim kf As String = "INSERT INTO Dash_SO_Plan_Batch_Details(COMPANY_ID,SITE_ID,SO_PLAN_NUMBER,SO_NUMBER,CUSTOMER_ID,CUSTOMER_NAME,TOTAL_AMOUNT,STORE_LAT,STORE_LONG,ORDER_DATE,STATUS,DISTANCE)" _
                    & "VALUES('" & COMPANYID & "'," _
                   & "'" & SITEID.Text & "'," _
                     & "'" & BATCHNUMBER.Text & "'," _
                         & "''," _
                           & "''," _
                         & " '', " _
                         & "'0'," _
                                   & "'0'," _
                                    & "'0'," _
                                     & "'" & DTDELIVERY.Value & "'," _
                                       & "'NEW'," _
                                & "'0')"

                Data(kf)

            Catch ex As Exception

            End Try

            Try

                views1("SELECT SO_PLAN_NUMBER FROM Dash_SO_Plan_Batch_Details WHERE ORDER_DATE ='" & DTDELIVERY.Value & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' GROUP BY SO_PLAN_NUMBER", "BSPIDB", DTGPLANS)

            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub GunaLinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)

        Transaction_Order_Plan_Re_Assign_Per_Seller_Set_Sub_Batch.ShowDialog()

    End Sub


End Class