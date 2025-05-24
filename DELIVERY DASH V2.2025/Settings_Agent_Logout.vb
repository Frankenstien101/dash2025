
Imports System.Data.SqlClient

Public Class Settings_Agent_Logout
    Private Sub GunaLabel2_Click(sender As Object, e As EventArgs) Handles GunaLabel2.Click

        Close()

    End Sub

    Dim SELECTEDA As String



    Private Sub CMBSITE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMBSITE.SelectedIndexChanged

        Try

            views("SELECT SUB_DA FROM Dash_Agents WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & CMBSITE.SelectedValue & "'", "BSPIDB", DTGDATA)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub SEARCH_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SEARCH.KeyPress

        Try

            views("SELECT SUB_DA FROM Dash_Agents WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & CMBSITE.SelectedValue & "' AND SUB_DA LIKE '%" & SEARCH.Text & "%'", "BSPIDB", DTGDATA)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub DTGDATA_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGDATA.CellClick

        SELECTEDA = ""

        Try

            SELECTEDA = DTGDATA.CurrentRow.Cells(0).Value

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GunaAdvenceButton2_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton2.Click

        If SELECTEDA = "" Then

            MsgBox("PLEASE SELECT AGENT TO LOGOUT", MsgBoxStyle.Exclamation, "SORRY")

        Else

            Try

                Dim K As String = "UPDATE Dash_Agents SET IS_LOGIN = '0' WHERE SUB_DA = '" & SELECTEDA & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & CMBSITE.SelectedValue & "'"

                Data(K)

            Catch ex As Exception

            End Try

            MsgBox("AGENT LOGOUT COMPLETE", MsgBoxStyle.Information, "COMPLETED")

            Try

                views("SELECT SUB_DA FROM Dash_Agents WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & CMBSITE.SelectedValue & "'", "BSPIDB", DTGDATA)

            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub Settings_Agent_Logout_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            con.Open()
            Dim da As New SqlDataAdapter("SELECT SITE_NAME, SITE_ID FROM Dash_User_Site WHERE USER_ID = '" & Form1.USERID.Text & "' GROUP BY SITE_NAME, SITE_ID", con)
            Dim dt As New DataTable
            da.Fill(dt)
            CMBSITE.DisplayMember = "SITE_NAME"
            CMBSITE.ValueMember = "SITE_ID" ' Set the ValueMember property
            CMBSITE.DataSource = dt
            con.Close()

        Catch ex As Exception
            ' Handle exceptions here, e.g., display an error message or log the error
        End Try

    End Sub
End Class