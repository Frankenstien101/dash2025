
Imports System.Data.SqlClient

Public Class Transaction_Order_Plan_Re_Assign_Per_Seller_Set_Sub_Batch_Set_Agent
    Private Sub Transaction_Order_Plan_Re_Assign_Per_Seller_Set_Sub_Batch_Set_Agent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            con.Open()
            Dim da As New SqlDataAdapter("SELECT SUB_DA FROM Dash_Agents WHERE AGENT_TYPE = 'SUB' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND STATUS = 'ACTIVE' GROUP BY SUB_DA", con)
            Dim dt As New DataTable
            da.Fill(dt)
            CMBSELLER.DisplayMember = "SUB_DA"
            CMBSELLER.ValueMember = "SUB_DA" ' Set the ValueMember property
            CMBSELLER.DataSource = dt
            con.Close()

        Catch ex As Exception
            ' Handle exceptions here, e.g., display an error message or log the error
        End Try

    End Sub

    Private Sub GunaLabel2_Click(sender As Object, e As EventArgs) Handles GunaLabel2.Click

        Close()

    End Sub

    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click

        Try

            Dim kf1 As String = "INSERT INTO Dash_Vehicle_Agent(COMPANY_ID,SITE_ID,VEHICLE,AGENT)" _
                       & "VALUES('" & Form1.COMPANYID.Text & "'," _
                      & "'" & Form1.SITEID.Text & "'," _
                        & "'" & Transaction_Order_Plan_Re_Assign_Per_Seller_Set_Sub_Batch.VEHICLE.Text & "'," _
                                   & "'" & CMBSELLER.Text & "')"

            Data(kf1)

            MsgBox("AGENT ASSIGNED TO VEHICLE", MsgBoxStyle.Information, "COMPLETE")

            Transaction_Order_Plan_Re_Assign_Per_Seller_Set_Sub_Batch.DA.Text = CMBSELLER.Text

            Close()

        Catch ex As Exception

        End Try

    End Sub
End Class