
Imports System.Data.SqlClient
Imports System.Data
Imports Microsoft.Reporting.WinForms

Module Module1

    Public da As New SqlDataAdapter
    Public con As New SqlConnection
    Public cmd As New SqlCommand
    Public ds As New DataSet
    Public sql As String
    Public dr As SqlDataReader

    Public Sub Connections()

        Try

            con = New SqlConnection("Data Source=bspidbservernew.database.windows.net;Network Library=DBMSSOCN;Initial Catalog=BSPIDBNEW;User ID=sqladmin;Password=b$p1.@dm1n;")
            cmd.Connection = con

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

    End Sub

    Public Sub Data(ByVal sql As String)

        Connections()

        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            cmd = New SqlCommand
            cmd.CommandText = sql
            cmd.Connection = con
            cmd.ExecuteNonQuery()
            con.Close()

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Public Sub views(ByVal sql As String, ByVal col As String, ByVal dg As DataGridView)

        Connections()

        da = New SqlClient.SqlDataAdapter(sql, con)
        ds.Reset()
        ds.Clear()
        da.Fill(ds, col)
        dg.DataSource = ds.Tables(col)
        con.Close()

    End Sub

End Module





