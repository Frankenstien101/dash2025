
Imports System.Data.SqlClient
Imports System.Data
Module Module4

    Public da111 As New SqlDataAdapter
    Public con111 As New SqlConnection
    Public cmd111 As New SqlCommand
    Public ds111 As New DataSet
    Public sql111 As String
    Public dr111 As SqlDataReader

    Public Sub Connections111()

        Try

            con111 = New SqlConnection("Data Source=bspidbservernew.database.windows.net;Network Library=DBMSSOCN;Initial Catalog=BSPIDBNEW;User ID=sqladmin;Password=b$p1.@dm1n;")
            cmd111.Connection = con111

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

    End Sub

    Public Sub Data111(ByVal sql111 As String)

        Connections111()

        Try
            If con111.State = ConnectionState.Open Then
                con111.Close()
            End If
            con111.Open()
            cmd111 = New SqlCommand
            cmd111.CommandText = sql111
            cmd111.Connection = con111
            cmd111.ExecuteNonQuery()
            con111.Close()

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Public Sub views3(ByVal sql111 As String, ByVal col111 As String, ByVal dg111 As DataGridView)

        Connections111()

        da111 = New SqlClient.SqlDataAdapter(sql111, con111)
        ds111.Reset()
        da111.Fill(ds111, col111)
        dg111.DataSource = ds111.Tables(col111)
        con111.Close()

    End Sub

End Module


