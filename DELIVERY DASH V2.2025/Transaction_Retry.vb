Public Class Transaction_Retry
    Private Sub Transaction_Retry_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DTDELIVER.Value = Form1.DTCALENDAR.Value

    End Sub

    Private Sub GunaLabel2_Click(sender As Object, e As EventArgs) Handles GunaLabel2.Click

        Close()

    End Sub

    Private Sub DTDELIVER_ValueChanged(sender As Object, e As EventArgs) Handles DTDELIVER.ValueChanged

        INVNUM.Text = ""
        DTGBATCH.DataSource = Nothing
        BATCHNUMBER.Text = ""

        If DTDELIVER.Value > Form1.DTCALENDAR.Value Then

            MsgBox("ONLY SELECT PREVIOUS DAY", MsgBoxStyle.Exclamation, "SORRY")

        Else

            Try

                views("SELECT LINEID
                   ,[BATCH]
              	  ,[CUSTOMER_ID]
                   ,[CUSTOMER_NAME]
                   ,[INVOICE_NUMBER]
                   ,[TOTAL_AMOUNT]
                   ,[INVOICE_VOLUME]
                   ,[DISTANCE]
                   ,[DATE_TO_DELIVER]
                   ,[AGENT_ID]
                   ,[ORDER_DATE]

               FROM [dbo].[Dash_Plan_Batch_Details] WHERE DATE_TO_DELIVER = '" & DTDELIVER.Value & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "'", "BSPIDB", DTGDATA)

                DTGDATA.Columns(0).Visible = False
                DTGDATA.Columns(7).Visible = False
                DTGDATA.Columns(9).Visible = False
                DTGDATA.Columns(10).Visible = False

            Catch ex As Exception

                '  MsgBox("3" & ex.ToString)

            End Try

            RETRYDELIVERYDATE.MinDate = DTDELIVER.Value

        End If

    End Sub

    Dim LINEID As String

    Private Sub DTGDATA_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGDATA.CellClick

        INVNUM.Text = ""
        DTGBATCH.DataSource = Nothing
        BATCHNUMBER.Text = ""

        LINEID = ""

        Try

            LINEID = DTGDATA.CurrentRow.Cells(0).Value
            INVNUM.Text = DTGDATA.CurrentRow.Cells(4).Value

        Catch ex As Exception

        End Try



    End Sub

    Public Sub REFRESHBTN()

        Try

            views("SELECT LINEID
                   ,[BATCH]
              	  ,[CUSTOMER_ID]
                   ,[CUSTOMER_NAME]
                   ,[INVOICE_NUMBER]
                   ,[TOTAL_AMOUNT]
                   ,[INVOICE_VOLUME]
                   ,[DISTANCE]
                   ,[DATE_TO_DELIVER]
                   ,[AGENT_ID]
                   ,[ORDER_DATE]
                   

               FROM [dbo].[Dash_Plan_Batch_Details] WHERE DATE_TO_DELIVER = '" & DTDELIVER.Value & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'FOR DELIVERY'", "BSPIDB", DTGDATA)

        Catch ex As Exception

            '  MsgBox("3" & ex.ToString)

        End Try


    End Sub

    Private Sub GunaAdvenceButton3_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton3.Click

        If LINEID = "" Then

            MsgBox("PLEASE SELECT FROM LIST", MsgBoxStyle.Exclamation, "SORRY")

        Else

            Try

                Dim O As String = "UPDATE Dash_Plan_Batch_Details SET STATUS = 'DELIVERED' WHERE LINEID = '" & LINEID & "'"

                Data(O)

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try

            MsgBox("INVOICE UPDATED TO DELIVERED", MsgBoxStyle.Information, "COMPLETED")

            LINEID = ""

            REFRESHBTN()

        End If

    End Sub

    Private Sub GunaAdvenceButton2_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton2.Click

        If LINEID = "" Then

            MsgBox("PLEASE SELECT FROM LIST", MsgBoxStyle.Exclamation, "SORRY")

        Else

            Try

                Dim O As String = "UPDATE Dash_Plan_Batch_Details SET STATUS = 'REJECED' WHERE LINEID = '" & LINEID & "'"

                Data(O)

            Catch ex As Exception

            End Try

            MsgBox("INVOICE UPDATED TO REJECTED", MsgBoxStyle.Information, "COMPLETED")

            REFRESHBTN()

        End If

    End Sub

    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click

        Dim P As MsgBoxResult

        P = MsgBox("ARE YOU SURE TO SET ALL UNDELIVERED INTO REJECTED", MsgBoxStyle.YesNo, "CONFIRM")

        If P = MsgBoxResult.Yes Then

            Try

                Dim O As String = "UPDATE Dash_Plan_Batch_Details SET STATUS = 'REJECED' WHERE  DATE_TO_DELIVER = '" & DTDELIVER.Value & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'FOR DELIVERY'"

                Data(O)

            Catch ex As Exception

            End Try

            MsgBox("INVOICE UPDATED TO REJECTED", MsgBoxStyle.Information, "COMPLETED")

            REFRESHBTN()

        End If

    End Sub

    Private Sub GunaAdvenceButton4_Click(sender As Object, e As EventArgs)

        If LINEID = "" Then

            MsgBox("PLEASE SELECT FROM LIST", MsgBoxStyle.Exclamation, "SORRY")

        Else

            '  Try
            '
            '      Transaction_Retry_Delivery_Set_Date_Retry.INVOICENUMBER.Text = DTGDATA.CurrentRow.Cells(4).Value
            '      Transaction_Retry_Delivery_Set_Date_Retry.ShowDialog()
            '
            '  Catch ex As Exception
            '
            '  End Try



            '
        End If



    End Sub

    Private Sub GunaAdvenceButton4_Click_1(sender As Object, e As EventArgs) Handles GunaAdvenceButton4.Click

        Dim P As MsgBoxResult

        P = MsgBox("ARE YOU SURE TO SET ALL ALL PENDING AS DELIVERED", MsgBoxStyle.YesNo, "CONFIRM")

        If P = MsgBoxResult.Yes Then

            Try

                Dim O As String = "UPDATE Dash_Plan_Batch_Details SET STATUS = 'DELIVERED' WHERE  DATE_TO_DELIVER = '" & DTDELIVER.Value & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'FOR DELIVERY'"

                Data(O)

            Catch ex As Exception

            End Try

            MsgBox("INVOICE(S) UPDATED TO DELIVERED", MsgBoxStyle.Information, "COMPLETED")

            REFRESHBTN()

        End If

    End Sub

    Private Sub RETRYDELIVERYDATE_ValueChanged(sender As Object, e As EventArgs) Handles RETRYDELIVERYDATE.ValueChanged


        Try

            views1("SELECT * FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'PROCESSED' AND  DATE_TO_DELIVER = '" & RETRYDELIVERYDATE.Value & "' ", "BSPIDB", DTGBATCH)
            DTGBATCH.Columns(0).Visible = False
            DTGBATCH.Columns(1).Visible = False
            DTGBATCH.Columns(2).Visible = False
            DTGBATCH.Columns(9).Visible = False
            DTGBATCH.Columns(12).Visible = False

            For Each row As DataGridViewRow In DTGBATCH.Rows

                row.Height = 30 ' Set the desired height for all rows

            Next

        Catch ex As Exception

            ' MsgBox(ex.ToString)

        End Try

    End Sub


    Private Sub DTGBATCH_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGBATCH.CellClick

        Try

            BATCHNUMBER.Text = DTGBATCH.CurrentRow.Cells("BATCH_ID").Value
            AGENTID.Text = DTGBATCH.CurrentRow.Cells("AGENT").Value
            VEHICLEID.Text = DTGBATCH.CurrentRow.Cells("VEHICLE_ID").Value

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GunaAdvenceButton6_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton6.Click

        If INVNUM.Text = "" Then

            MsgBox("NO INVOICE TO PROCESS", MsgBoxStyle.Exclamation, "SORRY")

        ElseIf BATCHNUMBER.Text = "" Then

            MsgBox("NO BATCH SELECTED", MsgBoxStyle.Exclamation, "SORRY")

        Else

            Try

                Dim L As String = "UPDATE Dash_Plan_Batch_Details SET AGENT_ID = '" & AGENTID.Text & "' , VEHICLE_IDS = '" & VEHICLEID.Text & "' , DATE_TO_DELIVER = '" & RETRYDELIVERYDATE.Value & "' WHERE INVOICE_NUMBER = '" & INVNUM.Text & "'"

                Data(L)

            Catch ex As Exception

            End Try

            Try

                Dim L As String = "UPDATE Dash_Plan_Batch_Transaction SET AGENT = '" & AGENTID.Text & "' , VEHICLE_ID = '" & VEHICLEID.Text & "' ,DATE_TO_DELIVER = '" & RETRYDELIVERYDATE.Value & "', NUM_OF_INVOICES +=1  WHERE BATCH_ID = '" & BATCHNUMBER.Text & "'"

                Data(L)

            Catch ex As Exception

            End Try

            MsgBox("INVOICE PROCESSED FOR RETRY", MsgBoxStyle.Information, "COMPLETE")

            DTGBATCH.DataSource = Nothing

            INVNUM.Text = ""
            BATCHNUMBER.Text = ""
            AGENTID.Text = ""
            VEHICLEID.Text = ""

            REFRESHBTN()

        End If

    End Sub

End Class