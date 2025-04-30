
Imports System.Data.SqlClient
Imports System.Data

Module Module3

    Public da11 As New SqlDataAdapter
    Public con11 As New SqlConnection
    Public cmd11 As New SqlCommand
    Public ds11 As New DataSet
    Public sql11 As String
    Public dr11 As SqlDataReader

    Public Sub Connections11()

        Try

            con11 = New SqlConnection("Data Source=bspidbservernew.database.windows.net;Network Library=DBMSSOCN;Initial Catalog=BSPIDBNEW;User ID=sqladmin;Password=b$p1.@dm1n;")
            cmd11.Connection = con11

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

    End Sub

    Public Sub Data11(ByVal sql11 As String)

        Connections11()

        Try
            If con11.State = ConnectionState.Open Then
                con11.Close()
            End If
            con11.Open()
            cmd11 = New SqlCommand
            cmd11.CommandText = sql11
            cmd11.Connection = con11
            cmd11.ExecuteNonQuery()
            con11.Close()

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Public Sub views2(ByVal sql11 As String, ByVal col11 As String, ByVal dg11 As DataGridView)

        Connections11()

        da11 = New SqlClient.SqlDataAdapter(sql11, con11)
        ds11.Reset()
        ds11.Clear()
        da11.Fill(ds11, col11)
        dg11.DataSource = ds11.Tables(col11)
        con11.Close()

    End Sub

End Module







