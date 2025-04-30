
Imports System.Data.SqlClient
Imports System.Data

Module Module2

    Public da1 As New SqlDataAdapter
    Public con1 As New SqlConnection
    Public cmd1 As New SqlCommand
    Public ds1 As New DataSet
    Public sql1 As String
    Public dr1 As SqlDataReader

    Public Sub Connections1()

        Try

            con1 = New SqlConnection("Data Source=bspidbservernew.database.windows.net;Network Library=DBMSSOCN;Initial Catalog=BSPIDBNEW;User ID=sqladmin;Password=b$p1.@dm1n;")
            cmd1.Connection = con1

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

    End Sub

    Public Sub Data1(ByVal sql1 As String)

        Connections1()

        Try
            If con1.State = ConnectionState.Open Then
                con1.Close()
            End If
            con1.Open()
            cmd1 = New SqlCommand
            cmd1.CommandText = sql1
            cmd1.Connection = con1
            cmd1.ExecuteNonQuery()
            con1.Close()

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Public Sub views1(ByVal sql1 As String, ByVal col1 As String, ByVal dg1 As DataGridView)

        Connections1()

        da1 = New SqlClient.SqlDataAdapter(sql1, con1)
        ds1.Reset()
        ds1.Clear()
        da1.Fill(ds1, col1)
        dg1.DataSource = ds1.Tables(col1)
        con1.Close()

    End Sub

End Module






