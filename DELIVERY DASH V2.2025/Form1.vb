
Imports System.Data.SqlClient

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim screenBounds As Rectangle = Screen.PrimaryScreen.WorkingArea ' Get the screen's working area (excluding the taskbar)

        ' Calculate the size of the form to fit within the screen's working area
        Dim formWidth As Integer = screenBounds.Width
        Dim formHeight As Integer = screenBounds.Height - 5 ' Assuming the taskbar height is 40 pixels

        ' Set the form's size and position
        Me.Size = New Size(formWidth, formHeight)
        Me.Location = New Point(screenBounds.Left, screenBounds.Top)

        MAINPANEL.Visible = False

        settingssubmenu.Height = 0
        REPORTSSUBMENU.Height = 0

        PANELMAIN.ItemSize = New Size(0, 1)
        PANELMAIN.SizeMode = TabSizeMode.Fixed

        Timer1.Start()

    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click

        Close()

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles BTNLOGIN.Click

        Try

            views("SELECT USERNAME,PASSWORD,STATUS,COMPANY,LINEID,ROLE,NAME_OF_USER FROM Dash_Users WHERE USERNAME = '" & USERNAMETXT.Text & "' AND PASSWORD = '" & PASSWORDTXT.Text & "'", "BSPIDB", DTGCHECK)
            DTUSERNAME.Text = DTGCHECK.CurrentRow.Cells(0).Value
            DTPASSWORD.Text = DTGCHECK.CurrentRow.Cells(1).Value
            STAT.Text = DTGCHECK.CurrentRow.Cells(2).Value
            COMPANYID.Text = DTGCHECK.CurrentRow.Cells(3).Value
            USERID.Text = DTGCHECK.CurrentRow.Cells(4).Value
            ROLETXT2.Text = DTGCHECK.CurrentRow.Cells(5).Value
            NAMEOFUSERTXT.Text = DTGCHECK.CurrentRow.Cells(6).Value
            ROLETXT2.Text = DTGCHECK.CurrentRow.Cells(5).Value

        Catch ex As Exception

            '   MsgBox(ex.ToString)

        End Try

        If STAT.Text = "INACTIVE" Then

            MSGERROR.Show("User is inactive")

        ElseIf USERNAMETXT.Text = DTUSERNAME.Text And PASSWORDTXT.Text = DTPASSWORD.Text Then

            Try

                views("SELECT COMPANY_NAME FROM Dash_Company WHERE COMPANY_ID = '" & COMPANYID.Text & "' ", "BSPIDB", DTGCHECK)
                COMPANYNAME.Text = DTGCHECK.CurrentRow.Cells(0).Value

            Catch ex As Exception

            End Try

            Try

                con.Open()
                Dim da As New SqlDataAdapter("SELECT SITE_NAME, SITE_ID FROM Dash_User_Site WHERE USER_ID = '" & USERID.Text & "' GROUP BY SITE_NAME, SITE_ID", con)
                Dim dt As New DataTable
                da.Fill(dt)
                CMBSITE.DisplayMember = "SITE_NAME"
                CMBSITE.ValueMember = "SITE_ID" ' Set the ValueMember property
                CMBSITE.DataSource = dt
                con.Close()

            Catch ex As Exception
                ' Handle exceptions here, e.g., display an error message or log the error
            End Try

            AcceptButton = Nothing
            MAINPANEL.Visible = True
            SITEID.Text = CMBSITE.SelectedValue

        Else

            MSGERROR.Show()
            NAMEOFUSER.Text = "-"
            ROLETXT.Text = "-"

        End If

    End Sub

    Private Sub BTNLOGOUT_Click(sender As Object, e As EventArgs) Handles BTNLOGOUT.Click

        MAINPANEL.Visible = False

        AcceptButton = BTNLOGIN
        USERNAMETXT.Text = ""
        PASSWORDTXT.Text = ""

    End Sub


    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click

        If settingssubmenu.Height = 380 Then

            settingssubmenu.Height = 0

        Else

            settingssubmenu.Height = 380

        End If

    End Sub

    ' Private Sub ShowFormInPanel(form As Form)
    '     ' Clear any previous form that was in the panel (optional)
    '     For Each ctrl As Control In PANELMAIN.Controls
    '         ctrl.Dispose() ' Clean up any existing form controls
    '     Next
    '
    '     ' Set the parent of Form2 to be the PanelMenu (acts like a container)
    '     form.TopLevel = False
    '     form.FormBorderStyle = FormBorderStyle.None
    '     form.Dock = DockStyle.Fill ' Make it fill the panel
    '     PANELMAIN.Controls.Add(form) ' Add form to the panel
    '     form.Show() ' Show the form inside the panel
    '
    ' End Sub
    Private Sub ShowFormInTab(ByVal frm As Form, ByVal tabPage As TabPage)

        frm.TopLevel = False
        frm.FormBorderStyle = FormBorderStyle.None
        frm.Dock = DockStyle.Fill
        tabPage.Controls.Clear()
        tabPage.Controls.Add(frm)
        frm.Show()

    End Sub

    Public Sub SHOWORDERPREPARATION()


        For Each tab As TabPage In PANELMAIN.TabPages
            If tab.Text = "ORDERPREPARATION" Then
                PANELMAIN.SelectedTab = tab ' Select the existing tab
                Exit Sub
            End If
        Next

        ' Create a new TabPage named "SalesOrder"
        Dim newTab As New TabPage("ORDERPREPARATION")

        ' Add the new TabPage to TabControl1
        PANELMAIN.TabPages.Add(newTab)

        ' Show the SalesOrder form inside the newly created TabPage
        ShowFormInTab(New Transactions_Order_Preparation(), newTab)

        ' Set focus to the newly added tab
        PANELMAIN.SelectedTab = newTab

    End Sub


    Public Sub SHOWORDERREPORT()


        For Each tab As TabPage In PANELMAIN.TabPages
            If tab.Text = "ORDERREPORT" Then
                PANELMAIN.SelectedTab = tab ' Select the existing tab
                Exit Sub
            End If
        Next

        ' Create a new TabPage named "SalesOrder"
        Dim newTab As New TabPage("ORDERREPORT")

        ' Add the new TabPage to TabControl1
        PANELMAIN.TabPages.Add(newTab)

        ' Show the SalesOrder form inside the newly created TabPage
        ShowFormInTab(New Reports_Order_Plan(), newTab)

        ' Set focus to the newly added tab
        PANELMAIN.SelectedTab = newTab

    End Sub
    Public Function GETCOMPANYID() As String
        Return COMPANYID.Text
    End Function

    Public Function GETSITEID() As String
        Return SITEID.Text
    End Function

    Public Function GETDATE() As String
        Return DATETXT.Text
    End Function

    Public Function GETTIME() As String
        Return TIMETXT.Text
    End Function




    Private Sub Guna2Button7_Click(sender As Object, e As EventArgs) Handles Guna2Button7.Click

        '  Dim frm2 As New Transactions_Order_Preparation()
        '  ShowFormInPanel(frm2) ' Show Form2 inside the Panel


        SHOWORDERPREPARATION()

    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click


        If REPORTSSUBMENU.Height = 267 Then

            REPORTSSUBMENU.Height = 0

        Else

            REPORTSSUBMENU.Height = 267

        End If

    End Sub

    Private Sub MAINPANEL_Paint(sender As Object, e As PaintEventArgs) Handles MAINPANEL.Paint

    End Sub

    Private Sub Guna2Button17_Click(sender As Object, e As EventArgs) Handles Guna2Button17.Click

        SHOWORDERREPORT()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        DTCALENDAR.Value = Date.Today

        DATETXT.Text = DTCALENDAR.Value
        TIMETXT.Text = DateAndTime.TimeOfDay

    End Sub

    Private Sub CMBSITE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMBSITE.SelectedIndexChanged


        ' TODAYSORDER.Text = "0"
        ' PENDINGORDER.Text = "0"
        ' FORDELIVERY.Text = "0"
        ' TOCOLLECT.Text = "0"

        Try

            SITEID.Text = CMBSITE.SelectedValue
            '   MsgBox(SITEID.Text)
        Catch ex As Exception

        End Try

        PANELMAIN.SelectedTab = MAINPAGE

        ' Try
        '
        '     views("SELECT COUNT(*) FROM PRFR_SO_UPLOAD_TRANSACTION WHERE ORDER_DATE = DATEADD(day, -1, '" & DTCALENDAR.Value & "') AND COMPANY_ID = '" & COMPANY_ID & "' AND SITE_ID = '" & SITEID.Text & "'", "BSPIDB", DTGCHECK)
        '     TODAYSORDER.Text = DTGCHECK.CurrentRow.Cells(0).Value
        '
        ' Catch ex As Exception
        '
        '     ' MsgBox(ex.ToString)
        '
        ' End Try
        '
        ' Try
        '
        '     TODAYSORDER.Text = Decimal.Parse(TODAYSORDER.Text).ToString("N0")
        '
        ' Catch ex As Exception
        '
        ' End Try
        '
        ' ' MsgBox("0")
        ' Try
        '
        '     views("SELECT COUNT(LINE_ID) FROM PRFR_Invoice_Transaction WHERE COMPANY_ID = '" & COMPANY_ID & "' AND SITE_ID = '" & SITEID.Text & "' AND DATE_INVOICE = '" & DTCALENDAR.Value & "' AND STATUS = 'INVOICED' AND IS_PLAN = '0'", "BSPIDB", DTGCHECK)
        '     PENDINGORDER.Text = DTGCHECK.CurrentRow.Cells(0).Value
        '
        ' Catch ex As Exception
        '
        '     '   MsgBox("1" & ex.ToString)
        '
        ' End Try
        ' '  MsgBox("1")
        '
        ' Try
        '
        '     PENDINGORDER.Text = Decimal.Parse(PENDINGORDER.Text).ToString("n0")
        '
        ' Catch ex As Exception
        '
        ' End Try
        '
        ' Try
        '
        '     views("SELECT COUNT(INVOICE_NUMBER) FROM Dash_Plan_Batch_Details WHERE COMPANY_ID = '" & COMPANY_ID & "' AND SITE_ID = '" & SITEID.Text & "' AND DATE_TO_DELIVER = '" & DTCALENDAR.Value & "'", "BSPIDB", DTGCHECK)
        '     FORDELIVERY.Text = DTGCHECK.CurrentRow.Cells(0).Value
        '
        ' Catch ex As Exception
        '     '   MsgBox("2" & ex.ToString)
        ' End Try
        '
        ' Try
        '
        '     FORDELIVERY.Text = Decimal.Parse(FORDELIVERY.Text).ToString("n0")
        '
        ' Catch ex As Exception
        '
        ' End Try
        '
        ' 'MsgBox("2")
        '
        ' Try
        '
        '     views("SELECT SUM(TOTAL_VALUE) FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANY_ID & "' AND SITE_ID = '" & SITEID.Text & "' AND DATE_TO_DELIVER = '" & DTCALENDAR.Value & "'", "BSPIDB", DTGCHECK)
        '     TOCOLLECT.Text = DTGCHECK.CurrentRow.Cells(0).Value
        '
        ' Catch ex As Exception
        '
        '     '  MsgBox("3" & ex.ToString)
        '     '
        ' End Try
        '
        ' Try
        '
        '     TOCOLLECT.Text = Decimal.Parse(TOCOLLECT.Text).ToString("n2")
        '
        ' Catch ex As Exception
        '
        ' End Try

    End Sub

    Private Sub Guna2GradientPanel1_Paint(sender As Object, e As PaintEventArgs) Handles Guna2GradientPanel1.Paint

    End Sub
End Class
