
Imports System.Net
Imports System.Data.SqlClient

Public Class Transaction_Execution

    Private Sub Transaction_Execution_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        USESUBDA.Visible = False
        SELECTEDVAL.Text = ""
        BTNSTATUS.Text = ""
        BTNSTATUS.BaseColor = Color.White

        COMPANYID.Text = Form1.GETCOMPANYID
        SITEID.Text = Form1.GETSITEID


        CUID.Text = ""
        CUSTOMERNAME.Text = ""
        ADDR.Text = ""
        STOREIMAGE.Image = Nothing


        DTDELIVER.Value = Form1.DTCALENDAR.Value

        DTGINVOICES.DataSource = Nothing

        BATCHNUMBER.Text = ""
        VEHICLE.Text = ""
        AGENT.Text = ""
        BTNSTATUS.Text = ""
        BTNSTATUS.Visible = False

        CMBAGENTUPDATE.Visible = False
        AGENT.Visible = True
        UPDATEAGENTLINK.Text = "UPDATE"

        CMBVEHICLEUPDATE.Visible = False
        VEHICLE.Visible = True
        UPDATEVEHICLELINK.Text = "UPDATE"

        If SWSTATUS.Checked = True Then

            Try

                views("SELECT * FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'READY' AND  DATE_TO_DELIVER = '" & DTDELIVER.Value & "'", "BSPIDB", DTGDATA)
                DTGDATA.Columns(0).Visible = False
                DTGDATA.Columns(1).Visible = False
                DTGDATA.Columns(2).Visible = False
                DTGDATA.Columns(9).Visible = False
                DTGDATA.Columns(12).Visible = False

                For Each row As DataGridViewRow In DTGDATA.Rows

                    row.Height = 50 ' Set the desired height for all rows

                Next

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try

        Else

            Try

                views("SELECT * FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'PROCESSED' AND  DATE_TO_DELIVER = '" & DTDELIVER.Value & "' ", "BSPIDB", DTGDATA)
                DTGDATA.Columns(0).Visible = False
                DTGDATA.Columns(1).Visible = False
                DTGDATA.Columns(2).Visible = False
                DTGDATA.Columns(9).Visible = False
                DTGDATA.Columns(12).Visible = False

                For Each row As DataGridViewRow In DTGDATA.Rows

                    row.Height = 50 ' Set the desired height for all rows

                Next

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try

        End If

        Try

            con.Open()

            Dim da As New SqlDataAdapter("select PLATE_NUM from Dash_Vehicles WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'ACTIVE' GROUP BY PLATE_NUM", con)

            Dim dt As New DataTable

            da.Fill(dt)

            CMBVEHICLEUPDATE.DisplayMember = "PLATE_NUM"

            CMBVEHICLEUPDATE.DataSource = dt

            con.Close()

        Catch ex As Exception

            '  MsgBox(ex.ToString)

        End Try

        Try

            con.Open()

            Dim da As New SqlDataAdapter("select USERNAME from Dash_Agents WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'ACTIVE' GROUP BY USERNAME", con)

            Dim dt As New DataTable

            da.Fill(dt)

            CMBAGENTUPDATE.DisplayMember = "USERNAME"

            CMBAGENTUPDATE.DataSource = dt

            con.Close()

        Catch ex As Exception

            '  MsgBox(ex.ToString)

        End Try

    End Sub

    Private Sub DTDELIVER_ValueChanged(sender As Object, e As EventArgs) Handles DTDELIVER.ValueChanged

        CUID.Text = ""
        CUSTOMERNAME.Text = ""
        ADDR.Text = ""

        STOREIMAGE.Image = Nothing
        DTGINVOICES.DataSource = Nothing

        BATCHNUMBER.Text = ""
        VEHICLE.Text = ""
        AGENT.Text = ""
        BTNSTATUS.Text = ""
        BTNSTATUS.Visible = False

        CMBAGENTUPDATE.Visible = False
        AGENT.Visible = True
        UPDATEAGENTLINK.Text = "UPDATE"

        CMBVEHICLEUPDATE.Visible = False
        VEHICLE.Visible = True
        UPDATEVEHICLELINK.Text = "UPDATE"

        If SWSTATUS.Checked = True Then

            Try

                views("SELECT * FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'READY' AND  DATE_TO_DELIVER = '" & DTDELIVER.Value & "' ", "BSPIDB", DTGDATA)
                DTGDATA.Columns(0).Visible = False
                DTGDATA.Columns(1).Visible = False
                DTGDATA.Columns(2).Visible = False
                DTGDATA.Columns(9).Visible = False
                DTGDATA.Columns(12).Visible = False

                For Each row As DataGridViewRow In DTGDATA.Rows

                    row.Height = 50 ' Set the desired height for all rows

                Next

            Catch ex As Exception

                ' MsgBox(ex.ToString)

            End Try

        Else

            Try

                views("SELECT * FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'PROCESSED' AND  DATE_TO_DELIVER = '" & DTDELIVER.Value & "' ", "BSPIDB", DTGDATA)
                DTGDATA.Columns(0).Visible = False
                DTGDATA.Columns(1).Visible = False
                DTGDATA.Columns(2).Visible = False
                DTGDATA.Columns(9).Visible = False
                DTGDATA.Columns(12).Visible = False

                For Each row As DataGridViewRow In DTGDATA.Rows

                    row.Height = 50 ' Set the desired height for all rows

                Next
            Catch ex As Exception

                ' MsgBox(ex.ToString)

            End Try

        End If

    End Sub

    Private Sub SWSTATUS_CheckedChanged(sender As Object, e As EventArgs) Handles SWSTATUS.CheckedChanged

        DTGINVOICES.DataSource = Nothing

        BATCHNUMBER.Text = ""
        VEHICLE.Text = ""
        AGENT.Text = ""
        BTNSTATUS.Text = ""
        BTNSTATUS.Visible = False


        CMBAGENTUPDATE.Visible = False
        AGENT.Visible = True
        UPDATEAGENTLINK.Text = "UPDATE"

        CMBVEHICLEUPDATE.Visible = False
        VEHICLE.Visible = True
        UPDATEVEHICLELINK.Text = "UPDATE"

        CUID.Text = ""
        CUSTOMERNAME.Text = ""
        ADDR.Text = ""
        STOREIMAGE.Image = Nothing

        If SWSTATUS.Checked = True Then

            Try

                views("SELECT * FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'READY' AND  DATE_TO_DELIVER = '" & DTDELIVER.Value & "'", "BSPIDB", DTGDATA)
                DTGDATA.Columns(0).Visible = False
                DTGDATA.Columns(1).Visible = False
                DTGDATA.Columns(2).Visible = False
                DTGDATA.Columns(9).Visible = False
                DTGDATA.Columns(12).Visible = False

                For Each row As DataGridViewRow In DTGDATA.Rows

                    row.Height = 50 ' Set the desired height for all rows

                Next

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try

        Else

            Try

                views("SELECT * FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'PROCESSED' AND  DATE_TO_DELIVER = '" & DTDELIVER.Value & "' ", "BSPIDB", DTGDATA)
                DTGDATA.Columns(0).Visible = False
                DTGDATA.Columns(1).Visible = False
                DTGDATA.Columns(2).Visible = False
                DTGDATA.Columns(9).Visible = False
                DTGDATA.Columns(12).Visible = False

                For Each row As DataGridViewRow In DTGDATA.Rows

                    row.Height = 50 ' Set the desired height for all rows

                Next

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try

        End If

        TOTALINES.Text = DTGDATA.Rows.Count()

    End Sub

    Private Sub DTGDATA_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGDATA.CellClick

        BATCHNUMBER.Text = ""
        VEHICLE.Text = ""
        AGENT.Text = ""
        BTNSTATUS.Text = ""
        BTNSTATUS.Visible = True

        CMBAGENTUPDATE.Visible = False
        AGENT.Visible = True
        UPDATEAGENTLINK.Text = "UPDATE"

        CMBVEHICLEUPDATE.Visible = False
        VEHICLE.Visible = True
        UPDATEVEHICLELINK.Text = "UPDATE"

        DTDELIVERYUPDATE.Value = DTDELIVER.Value

        CUID.Text = ""
        CUSTOMERNAME.Text = ""
        ADDR.Text = ""
        STOREIMAGE.Image = Nothing

        Try

            BATCHNUMBER.Text = DTGDATA.CurrentRow.Cells(3).Value
            VEHICLE.Text = DTGDATA.CurrentRow.Cells(10).Value
            AGENT.Text = DTGDATA.CurrentRow.Cells(11).Value
            BTNSTATUS.Text = DTGDATA.CurrentRow.Cells(9).Value

        Catch ex As Exception

        End Try

        If BTNSTATUS.Text = "PROCESSED" Then

            BTNSTATUS.BaseColor = Color.Green

        Else

            BTNSTATUS.BaseColor = Color.Gold

        End If



        Try

            views1("SELECT INVOICE_NUMBER,CUSTOMER_ID,CUSTOMER_NAME,DISTANCE FROM Dash_Plan_Batch_Details WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND  BATCH = '" & BATCHNUMBER.Text & "' ", "BSPIDB", DTGINVOICES)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub UPDATEVEHICLELINK_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles UPDATEVEHICLELINK.LinkClicked

        If BTNSTATUS.Text = "PROCESSED" Then

            MsgBox("CANNOT SET PROCESSED DELIVERY", MsgBoxStyle.Exclamation, "SORRY")

        Else

            If UPDATEVEHICLELINK.Text = "CANCEL" Then

                CMBVEHICLEUPDATE.Visible = False
                VEHICLE.Visible = True
                UPDATEVEHICLELINK.Text = "UPDATE"

            Else

                CMBVEHICLEUPDATE.Visible = True
                VEHICLE.Visible = False
                UPDATEVEHICLELINK.Text = "CANCEL"

            End If

        End If


    End Sub

    Private Sub UPDATEAGENTLINK_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles UPDATEAGENTLINK.LinkClicked

        If BTNSTATUS.Text = "PROCESSED" Then

            MsgBox("CANNOT SET PROCESSED DELIVERY", MsgBoxStyle.Exclamation, "SORRY")


        Else
            If UPDATEAGENTLINK.Text = "CANCEL" Then

                CMBAGENTUPDATE.Visible = False
                AGENT.Visible = True
                UPDATEAGENTLINK.Text = "UPDATE"
                USESUBDA.Visible = False

            Else

                CMBAGENTUPDATE.Visible = True
                AGENT.Visible = False
                UPDATEAGENTLINK.Text = "CANCEL"
                USESUBDA.Visible = True

            End If

        End If

    End Sub

    Private Sub CMBAGENTUPDATE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMBAGENTUPDATE.SelectedIndexChanged

        SELECTEDVAL.Text = CMBAGENTUPDATE.Text

        If SELECTEDVAL.Text = "" And BATCHNUMBER.Text = "" Then


        ElseIf BATCHNUMBER.Text = "" Then


        Else

            Try

                Try

                    Dim O As String = "UPDATE Dash_Plan_Batch_Transaction SET AGENT = '" & SELECTEDVAL.Text & "' WHERE BATCH_ID = '" & BATCHNUMBER.Text & "'"

                    Data(O)

                Catch ex As Exception

                    MsgBox(ex.ToString)

                End Try

                Try

                    Dim G As String = "UPDATE Dash_Plan_Batch_Details SET AGENT_ID = '" & SELECTEDVAL.Text & "', STATUS = 'FOR DELIVERY'  WHERE BATCH = '" & BATCHNUMBER.Text & "'"

                    Data(G)

                Catch ex As Exception

                    MsgBox(ex.ToString)

                End Try

                CMBAGENTUPDATE.Visible = False
                AGENT.Visible = True
                UPDATEAGENTLINK.Text = "UPDATE"

            Catch ex As Exception

            End Try

            SELECTEDVAL.Text = ""

            If SWSTATUS.Checked = True Then

                Try

                    views("SELECT * FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'READY' AND  DATE_TO_DELIVER = '" & DTDELIVER.Value & "'", "BSPIDB", DTGDATA)
                    DTGDATA.Columns(0).Visible = False
                    DTGDATA.Columns(1).Visible = False
                    DTGDATA.Columns(2).Visible = False
                    DTGDATA.Columns(9).Visible = False
                    DTGDATA.Columns(12).Visible = False

                    For Each row As DataGridViewRow In DTGDATA.Rows

                        row.Height = 50 ' Set the desired height for all rows

                    Next

                Catch ex As Exception

                    MsgBox(ex.ToString)

                End Try

            Else

                Try

                    views("SELECT * FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'PROCESSED' AND  DATE_TO_DELIVER = '" & DTDELIVER.Value & "' ", "BSPIDB", DTGDATA)
                    DTGDATA.Columns(0).Visible = False
                    DTGDATA.Columns(1).Visible = False
                    DTGDATA.Columns(2).Visible = False
                    DTGDATA.Columns(9).Visible = False
                    DTGDATA.Columns(12).Visible = False

                    For Each row As DataGridViewRow In DTGDATA.Rows

                        row.Height = 50 ' Set the desired height for all rows

                    Next

                Catch ex As Exception

                    MsgBox(ex.ToString)

                End Try

            End If


        End If

    End Sub

    Private Sub GunaLinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles GunaLinkLabel1.LinkClicked



        If BTNSTATUS.Text = "PROCESSED" Then

            MsgBox("CANNOT SET PROCESSED DELIVERY", MsgBoxStyle.Exclamation, "SORRY")

        Else

            Dim L As MsgBoxResult

            L = MsgBox("ARE YOU SURE TO UPDATE DELIVERY DATE?", MsgBoxStyle.YesNo, "CONFIRM")


            If L = MsgBoxResult.Yes Then

                Try

                    Try

                        Dim O As String = "UPDATE Dash_Plan_Batch_Transaction SET DATE_TO_DELIVER = '" & DTDELIVERYUPDATE.Value & "' WHERE BATCH_ID = '" & BATCHNUMBER.Text & "'"

                        Data(O)

                    Catch ex As Exception

                        MsgBox(ex.ToString)

                    End Try

                    Try

                        Dim G As String = "UPDATE Dash_Plan_Batch_Details SET DATE_TO_DELIVER = '" & DTDELIVERYUPDATE.Value & "', STATUS = 'FOR DELIVERY'  WHERE BATCH = '" & BATCHNUMBER.Text & "'"

                        Data(G)

                    Catch ex As Exception

                        MsgBox(ex.ToString)

                    End Try

                Catch ex As Exception

                End Try


                If SWSTATUS.Checked = True Then

                    Try

                        views("SELECT * FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'READY' AND  DATE_TO_DELIVER = '" & DTDELIVER.Value & "'", "BSPIDB", DTGDATA)
                        DTGDATA.Columns(0).Visible = False
                        DTGDATA.Columns(1).Visible = False
                        DTGDATA.Columns(2).Visible = False
                        DTGDATA.Columns(9).Visible = False
                        DTGDATA.Columns(12).Visible = False

                        For Each row As DataGridViewRow In DTGDATA.Rows

                            row.Height = 50 ' Set the desired height for all rows

                        Next

                    Catch ex As Exception

                        MsgBox(ex.ToString)

                    End Try

                Else

                    Try

                        views("SELECT * FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'PROCESSED' AND  DATE_TO_DELIVER = '" & DTDELIVER.Value & "' ", "BSPIDB", DTGDATA)
                        DTGDATA.Columns(0).Visible = False
                        DTGDATA.Columns(1).Visible = False
                        DTGDATA.Columns(2).Visible = False
                        DTGDATA.Columns(9).Visible = False
                        DTGDATA.Columns(12).Visible = False

                        For Each row As DataGridViewRow In DTGDATA.Rows

                            row.Height = 50 ' Set the desired height for all rows

                        Next

                    Catch ex As Exception

                        MsgBox(ex.ToString)

                    End Try

                End If


            End If


        End If

    End Sub

    Private Sub CMBVEHICLEUPDATE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMBVEHICLEUPDATE.SelectedIndexChanged

        SELECTEDVAL.Text = CMBVEHICLEUPDATE.Text

        If SELECTEDVAL.Text = "" And BATCHNUMBER.Text = "" Then


        ElseIf BATCHNUMBER.Text = "" Then


        Else

            Try

                Try

                    Dim O As String = "UPDATE Dash_Plan_Batch_Transaction SET VEHICLE_ID = '" & SELECTEDVAL.Text & "' WHERE BATCH_ID = '" & BATCHNUMBER.Text & "'"

                    Data(O)

                Catch ex As Exception

                    MsgBox(ex.ToString)

                End Try

                ' Try
                '
                '     Dim G As String = "UPDATE Dash_Plan_Batch_Details SET VEHICLE_IDS = '" & SELECTEDVAL.Text & "', STATUS = 'FOR DELIVERY'  WHERE BATCH = '" & BATCHNUMBER.Text & "'"
                '
                '     Data(G)
                '
                ' Catch ex As Exception
                '
                '     MsgBox(ex.ToString)
                '
                ' End Try

                CMBVEHICLEUPDATE.Visible = False
                VEHICLE.Visible = True
                UPDATEVEHICLELINK.Text = "UPDATE"

            Catch ex As Exception

            End Try

            SELECTEDVAL.Text = ""

            If SWSTATUS.Checked = True Then

                Try

                    views("SELECT * FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'READY' AND  DATE_TO_DELIVER = '" & DTDELIVER.Value & "'", "BSPIDB", DTGDATA)
                    DTGDATA.Columns(0).Visible = False
                    DTGDATA.Columns(1).Visible = False
                    DTGDATA.Columns(2).Visible = False
                    DTGDATA.Columns(9).Visible = False
                    DTGDATA.Columns(12).Visible = False

                    For Each row As DataGridViewRow In DTGDATA.Rows

                        row.Height = 50 ' Set the desired height for all rows

                    Next

                Catch ex As Exception

                    MsgBox(ex.ToString)

                End Try

            Else

                Try

                    views("SELECT * FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'PROCESSED' AND  DATE_TO_DELIVER = '" & DTDELIVER.Value & "' ", "BSPIDB", DTGDATA)
                    DTGDATA.Columns(0).Visible = False
                    DTGDATA.Columns(1).Visible = False
                    DTGDATA.Columns(2).Visible = False
                    DTGDATA.Columns(9).Visible = False
                    DTGDATA.Columns(12).Visible = False

                    For Each row As DataGridViewRow In DTGDATA.Rows

                        row.Height = 50 ' Set the desired height for all rows

                    Next

                Catch ex As Exception

                    MsgBox(ex.ToString)

                End Try

            End If

        End If

    End Sub

    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click


        If BATCHNUMBER.Text = "" Then

            MsgBox("NO TRANSACTION FOUND", MsgBoxStyle.Exclamation, "SORRY")

        ElseIf AGENT.Text = "" Then

            MsgBox("PLEASE SELECT AGENT", MsgBoxStyle.Exclamation, "SORRY")

        ElseIf BTNSTATUS.Text = "PROCESSED" Then

            MsgBox("TRANSACTION ALREADY PROCESSED", MsgBoxStyle.Exclamation, "SORRY")

        Else

            Dim K As MsgBoxResult

            K = MsgBox("ARE YOU SURE TO PROCESS DELIVERY?", MsgBoxStyle.YesNo, "CONFIRM")

            If K = MsgBoxResult.Yes Then

                Try

                    Dim U As String = "UPDATE Dash_Plan_Batch_Transaction SET VEHICLE_ID= '" & VEHICLE.Text & "', AGENT = '" & AGENT.Text & "' ,STATUS = 'PROCESSED' WHERE BATCH_ID = '" & BATCHNUMBER.Text & "'"

                    Data(U)

                Catch ex As Exception

                    ' MsgBox(ex.ToString)

                End Try

                Try

                    Dim U As String = "UPDATE Dash_Plan_Batch_Details SET STATUS = 'FOR DELIVERY', AGENT_ID = '" & AGENT.Text & "' WHERE BATCH = '" & BATCHNUMBER.Text & "'"

                    Data(U)

                Catch ex As Exception

                    MsgBox(ex.ToString)

                End Try


                MsgBox("DELIVERY PROCESSED", MsgBoxStyle.Information, "COMPLETED")

                '   Print_Route_Plan.BATCHNUMBER.Text = BATCHNUMBER.Text
                ' Print_Route_Plan.BTNREFRESH.PerformClick()
                ' Print_Route_Plan.ShowDialog()

                DTGINVOICES.DataSource = Nothing

                BATCHNUMBER.Text = ""
                VEHICLE.Text = ""
                AGENT.Text = ""
                BTNSTATUS.Text = ""
                BTNSTATUS.Visible = False

                CMBAGENTUPDATE.Visible = False
                AGENT.Visible = True
                UPDATEAGENTLINK.Text = "UPDATE"

                CMBVEHICLEUPDATE.Visible = False
                VEHICLE.Visible = True
                UPDATEVEHICLELINK.Text = "UPDATE"

                CUID.Text = ""
                CUSTOMERNAME.Text = ""
                ADDR.Text = ""
                STOREIMAGE.Image = Nothing

                If SWSTATUS.Checked = True Then

                    Try

                        views("SELECT * FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'READY' AND  DATE_TO_DELIVER = '" & DTDELIVER.Value & "'", "BSPIDB", DTGDATA)
                        DTGDATA.Columns(0).Visible = False
                        DTGDATA.Columns(1).Visible = False
                        DTGDATA.Columns(2).Visible = False
                        DTGDATA.Columns(9).Visible = False
                        DTGDATA.Columns(12).Visible = False

                        For Each row As DataGridViewRow In DTGDATA.Rows

                            row.Height = 50 ' Set the desired height for all rows

                        Next

                    Catch ex As Exception

                        MsgBox(ex.ToString)

                    End Try

                Else

                    Try

                        views("SELECT * FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'PROCESSED' AND  DATE_TO_DELIVER = '" & DTDELIVER.Value & "' ", "BSPIDB", DTGDATA)
                        DTGDATA.Columns(0).Visible = False
                        DTGDATA.Columns(1).Visible = False
                        DTGDATA.Columns(2).Visible = False
                        DTGDATA.Columns(9).Visible = False
                        DTGDATA.Columns(12).Visible = False

                        For Each row As DataGridViewRow In DTGDATA.Rows

                            row.Height = 50 ' Set the desired height for all rows

                        Next

                    Catch ex As Exception

                        MsgBox(ex.ToString)

                    End Try

                End If

            End If

        End If

    End Sub

    Private Sub DTGINVOICES_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGINVOICES.CellClick

        CUID.Text = ""
        CUSTOMERNAME.Text = ""
        ADDR.Text = ""
        STOREIMAGE.Image = Nothing

        CUID.Text = DTGINVOICES.CurrentRow.Cells(1).Value
        CUSTOMERNAME.Text = DTGINVOICES.CurrentRow.Cells(2).Value

        Try

            views2("SELECT ADDRESS,IMAGE1 FROM Dash_Customer_Master WHERE CODE = '" & CUID.Text & "' ", "BSPIDB", DTGCHECK)

            ADDR.Text = DTGCHECK.CurrentRow.Cells(0).Value
            STORETXT.Text = DTGCHECK.CurrentRow.Cells(1).Value

        Catch ex As Exception

        End Try


        Try

            Dim imageUrl1 As String = STORETXT.Text

            ' Create a WebClient to download the image
            Using webClient As New WebClient()
                ' Download the image bytes
                Dim imageBytes As Byte() = webClient.DownloadData(imageUrl1)

                ' Create a MemoryStream from the downloaded bytes
                Using ms As New IO.MemoryStream(imageBytes)
                    ' Create an Image object from the MemoryStream
                    Dim img As Image = Image.FromStream(ms)

                    ' Set the PictureBox's image to the downloaded image
                    STOREIMAGE.Image = img

                End Using

            End Using

        Catch ex As Exception
            ' Handle any errors, e.g., image not found, network issue, etc.
            ' MessageBox.Show("An error occurred: " & ex.Message)

        End Try

    End Sub

    Private Sub USESUBDA_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles USESUBDA.LinkClicked



        Try

            con.Open()

            Dim da As New SqlDataAdapter("select SUB_DA from Dash_Agents WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND STATUS = 'ACTIVE' GROUP BY SUB_DA", con)

            Dim dt As New DataTable

            da.Fill(dt)

            CMBAGENTUPDATE.DisplayMember = "SUB_DA"

            CMBAGENTUPDATE.DataSource = dt

            con.Close()

        Catch ex As Exception

            '  MsgBox(ex.ToString)

        End Try


    End Sub
End Class