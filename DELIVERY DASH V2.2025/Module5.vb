
Imports System.Data.SqlClient
Imports System.Data
Module Module5

    Public da1111 As New SqlDataAdapter
    Public con1111 As New SqlConnection
    Public cmd1111 As New SqlCommand
    Public ds1111 As New DataSet
    Public sql1111 As String
    Public dr1111 As SqlDataReader

    Public Sub Connections1111()

        Try

            con1111 = New SqlConnection("Data Source=bspidbservernew.database.windows.net;Network Library=DBMSSOCN;Initial Catalog=BSPIDBNEW;User ID=sqladmin;Password=b$p1.@dm1n;")
            cmd1111.Connection = con1111

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

    End Sub

    Public Sub Data1111(ByVal sql1111 As String)

        Connections1111()

        Try
            If con1111.State = ConnectionState.Open Then
                con1111.Close()
            End If
            con1111.Open()
            cmd1111 = New SqlCommand
            cmd1111.CommandText = sql1111
            cmd1111.Connection = con1111
            cmd1111.ExecuteNonQuery()
            con1111.Close()

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Public Sub views4(ByVal sql1111 As String, ByVal col1111 As String, ByVal dg1111 As DataGridView)

        Connections1111()

        da1111 = New SqlClient.SqlDataAdapter(sql1111, con1111)
        ds1111.Reset()
        da1111.Fill(ds1111, col1111)
        dg1111.DataSource = ds1111.Tables(col1111)
        con1111.Close()

    End Sub

End Module


