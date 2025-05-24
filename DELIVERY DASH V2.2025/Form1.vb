
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
        SYSTEMSETTINGS.Height = 0

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

        ElseIf USERNAMETXT.Text = "" Or PASSWORDTXT.Text = "" Then

            MSGERROR.Show("Please all fields")

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

            DTFROM.Value = DTCALENDAR.Value
            DTTO.Value = DTCALENDAR.Value

            AcceptButton = Nothing
            MAINPANEL.Visible = True
            SITEID.Text = CMBSITE.SelectedValue

        Else

            MSGERROR.Show("Incorrect username or password")
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

        If settingssubmenu.Height = 418 Then

            settingssubmenu.Height = 0

        Else

            settingssubmenu.Height = 418

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
                ' Remove the old tab
                PANELMAIN.TabPages.Remove(tab)
                Exit For
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

        If ROLETXT2.Text = "TRUCK-SIZER" Then

            SHOWORDERPREPARATION()

        ElseIf ROLETXT2.Text = "ADMIN" Then

            SHOWORDERPREPARATION()

        Else

            NOACCESSMSG.Show()

        End If

    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click

        If REPORTSSUBMENU.Height = 342 Then

            REPORTSSUBMENU.Height = 0

        Else

            REPORTSSUBMENU.Height = 342

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

    Private Async Sub CMBSITE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMBSITE.SelectedIndexChanged


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

        Try

            views("SELECT COUNT(*) FROM PRFR_SO_UPLOAD_TRANSACTION WHERE ORDER_DATE = DATEADD(day, -1, '" & DTCALENDAR.Value & "') AND COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "'", "BSPIDB", DTGCHECK)
            TODAYSORDER.Text = DTGCHECK.CurrentRow.Cells(0).Value

        Catch ex As Exception

            ' MsgBox(ex.ToString)

        End Try

        Try

            TODAYSORDER.Text = Decimal.Parse(TODAYSORDER.Text).ToString("N0")

        Catch ex As Exception

        End Try

        ' MsgBox("0")
        Try

            views("SELECT COUNT(LINE_ID) FROM PRFR_Invoice_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND DATE_INVOICE = '" & DTCALENDAR.Value & "' AND STATUS = 'INVOICED' AND IS_PLAN = '0'", "BSPIDB", DTGCHECK)
            PENDINGORDER.Text = DTGCHECK.CurrentRow.Cells(0).Value

        Catch ex As Exception

            '   MsgBox("1" & ex.ToString)

        End Try
        '  MsgBox("1")

        Try

            PENDINGORDER.Text = Decimal.Parse(PENDINGORDER.Text).ToString("n0")

        Catch ex As Exception

        End Try

        Try

            views("SELECT COUNT(INVOICE_NUMBER) FROM Dash_Plan_Batch_Details WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND DATE_TO_DELIVER = '" & DTCALENDAR.Value & "'", "BSPIDB", DTGCHECK)
            FORDELIVERY.Text = DTGCHECK.CurrentRow.Cells(0).Value

        Catch ex As Exception
            '   MsgBox("2" & ex.ToString)
        End Try

        Try

            FORDELIVERY.Text = Decimal.Parse(FORDELIVERY.Text).ToString("n0")

        Catch ex As Exception

        End Try

        'MsgBox("2")

        Try

            views("SELECT SUM(TOTAL_VALUE) FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND DATE_TO_DELIVER = '" & DTCALENDAR.Value & "'", "BSPIDB", DTGCHECK)
            TOCOLLECT.Text = DTGCHECK.CurrentRow.Cells(0).Value

        Catch ex As Exception

            '  MsgBox("3" & ex.ToString)
            '
        End Try

        Try

            TOCOLLECT.Text = Decimal.Parse(TOCOLLECT.Text).ToString("n2")

        Catch ex As Exception

        End Try

        Try
            Await WebView21.EnsureCoreWebView2Async(Nothing)
        Catch ex As Exception
            MsgBox("Error initializing WebView2: " & ex.Message)
        End Try

        Try
            ' Fetch latest row per agent
            views("WITH LatestPerAgent AS (
                    SELECT
                        [COMPANY_ID],
                        [SITE_ID],
                        [AGENT_ID],
                        [VEHICLE_ID],
                        [DELIVERY_DATE],
                        [LAT_CAPTURED],
                        [LONG_CAPTURED],
                        [TIME_STAMP],
                        CAST([DELIVERY_DATE] AS DATETIME) + CAST([TIME_STAMP] AS DATETIME) AS FullDateTime,
                        [BATTERY_PERCENTAGE],
                        [GPS_ACCURACY],
                        [TIME_MINUTES],
                        ROW_NUMBER() OVER (
                            PARTITION BY [AGENT_ID] 
                            ORDER BY CAST([DELIVERY_DATE] AS DATETIME) + CAST([TIME_STAMP] AS DATETIME) DESC
                        ) AS rn
                    FROM [dbo].[Dash_Agent_Time_Stamp]
                
                	WHERE COMPANY_ID = '" & COMPANYID.Text & "' AND SITE_ID = '" & SITEID.Text & "' AND DELIVERY_DATE = '" & DTCALENDAR.Value & "'
                )
                SELECT *
                FROM LatestPerAgent
                WHERE rn = 1;", "BSPIDB", DTGCHECK)

            ' Prepare HTML
            Dim htmlContent As String = "
    <html>
    <head>
        <title>Bing Maps</title>
        <script type='text/javascript' src='https://www.bing.com/api/maps/mapcontrol?key=u8bVA0NAuVbJ5r3MZgUR~fk8RCjst0j6nqTGJqGeK6w~Aj-CdHthPN5zeNnT6yh7YYRy1RGcZ3--xAUdQNkjP7_5t7ysVrqpNUvA6QYV-fYC'></script>
        <script type='text/javascript'>
            var map;
            var locations = [];
            var vehicleStoreCounts = {};

            function getPushpinColor(value) {
                var hash = 0;
                for (var i = 0; i < value.length; i++) {
                    hash = (hash << 5) - hash + value.charCodeAt(i);
                    hash = hash & hash;
                }
                var colorIndex = Math.abs(hash % 12);
                var colors = ['#FF0000', '#00FF00', '#0000FF', '#FFFF00', '#FF00FF', '#00FFFF', '#800000', '#008000', '#000080', '#808000', '#800080', '#008080'];
                return colors[colorIndex];
            }

            function initialize() {
                map = new Microsoft.Maps.Map('#myMap', {
                    center: new Microsoft.Maps.Location(13.7563, 100.5018),
                    zoom: 8
                });
    "

            ' Calculate bounds
            Dim minLat As Double = Double.MaxValue
            Dim maxLat As Double = Double.MinValue
            Dim minLng As Double = Double.MaxValue
            Dim maxLng As Double = Double.MinValue

            For Each row As DataGridViewRow In DTGCHECK.Rows
                If row.IsNewRow Then Continue For
                If Not IsDBNull(row.Cells("LAT_CAPTURED").Value) AndAlso Not IsDBNull(row.Cells("LONG_CAPTURED").Value) Then
                    Dim lat As Double = Convert.ToDouble(row.Cells("LAT_CAPTURED").Value)
                    Dim lng As Double = Convert.ToDouble(row.Cells("LONG_CAPTURED").Value)
                    Dim agentId As String = row.Cells("AGENT_ID").Value.ToString()
                    Dim vehicleId As String = row.Cells("VEHICLE_ID").Value.ToString()

                    minLat = Math.Min(minLat, lat)
                    maxLat = Math.Max(maxLat, lat)
                    minLng = Math.Min(minLng, lng)
                    maxLng = Math.Max(maxLng, lng)

                    ' Add location to JS
                    htmlContent &= $"locations.push({{ latitude: {lat}, longitude: {lng}, agentId: '{agentId}', vehicleId: '{vehicleId}' }});" & vbCrLf
                End If
            Next

            ' Continue JS script
            htmlContent &= "
                for (var i = 0; i < locations.length; i++) {
                    var loc = locations[i];
                    var location = new Microsoft.Maps.Location(loc.latitude, loc.longitude);
                    var color = getPushpinColor(loc.vehicleId);

                    if (!vehicleStoreCounts[loc.vehicleId]) {
                        vehicleStoreCounts[loc.vehicleId] = 0;
                    }
                    vehicleStoreCounts[loc.vehicleId]++;

                    var pushpin = new Microsoft.Maps.Pushpin(location, {
                        color: color,
                        title: loc.agentId 
                    });

                    Microsoft.Maps.Events.addHandler(pushpin, 'click', (function(l) {
                        return function() {
                            alert('Agent: ' + l.agentId + '\nVehicle: ' + l.vehicleId);
                        };
                    })(loc));

                    map.entities.push(pushpin);
                }

                var bounds = Microsoft.Maps.LocationRect.fromCorners(
                    new Microsoft.Maps.Location(" & minLat.ToString() & ", " & minLng.ToString() & "),
                    new Microsoft.Maps.Location(" & maxLat.ToString() & ", " & maxLng.ToString() & ")
                );
                map.setView({ bounds: bounds });
            }

            window.onload = initialize;
        </script>
    </head>
    <body style='margin:0;padding:0;'>
        <div id='myMap' style='position:relative;width:100%;height:100%;'></div>
    </body>
    </html>"

            ' Load to WebView
            WebView21.NavigateToString(htmlContent)

        Catch ex As Exception
            MsgBox("Error: " & ex.ToString())
        End Try

        GunaAdvenceButton1.PerformClick()

    End Sub


    Private Sub Guna2GradientPanel1_Paint(sender As Object, e As PaintEventArgs) Handles Guna2GradientPanel1.Paint

    End Sub

    Public Sub SHOWDELIVERYPLAN()


        For Each tab As TabPage In PANELMAIN.TabPages
            If tab.Text = "DELIVERYPLAN" Then
                PANELMAIN.TabPages.Remove(tab)
                Exit Sub
            End If
        Next

        ' Create a new TabPage named "SalesOrder"
        Dim newTab As New TabPage("DELIVERYPLAN")

        ' Add the new TabPage to TabControl1
        PANELMAIN.TabPages.Add(newTab)

        ' Show the SalesOrder form inside the newly created TabPage
        ShowFormInTab(New Transaction_Delivery_Plan(), newTab)

        ' Set focus to the newly added tab
        PANELMAIN.SelectedTab = newTab

    End Sub

    Private Sub Guna2Button9_Click(sender As Object, e As EventArgs) Handles Guna2Button9.Click

        If ROLETXT2.Text = "TRUCK-SIZER" Then

            SHOWDELIVERYPLAN()

        ElseIf ROLETXT2.Text = "ADMIN" Then

            SHOWDELIVERYPLAN()

        Else

            NOACCESSMSG.Show()

        End If

    End Sub

    Public Sub SHOWREVIEWPLAN()


        For Each tab As TabPage In PANELMAIN.TabPages
            If tab.Text = "REVIEWPLAN" Then
                PANELMAIN.TabPages.Remove(tab)
                Exit Sub
            End If
        Next

        ' Create a new TabPage named "SalesOrder"
        Dim newTab As New TabPage("REVIEWPLAN")

        ' Add the new TabPage to TabControl1
        PANELMAIN.TabPages.Add(newTab)

        ' Show the SalesOrder form inside the newly created TabPage
        ShowFormInTab(New Transaction_Review_Plan(), newTab)

        ' Set focus to the newly added tab
        PANELMAIN.SelectedTab = newTab

    End Sub

    Private Sub Guna2Button10_Click(sender As Object, e As EventArgs) Handles Guna2Button10.Click

        If ROLETXT2.Text = "TRUCK-SIZER" Then

            SHOWREVIEWPLAN()

        ElseIf ROLETXT2.Text = "ADMIN" Then

            SHOWREVIEWPLAN()

        Else

            NOACCESSMSG.Show()

        End If

    End Sub

    Public Sub SHOWEXECUTION()

        For Each tab As TabPage In PANELMAIN.TabPages

            If tab.Text = "EXECUTION" Then
                PANELMAIN.TabPages.Remove(tab)
                Exit Sub
            End If

        Next

        ' Create a new TabPage named "SalesOrder"
        Dim newTab As New TabPage("EXECUTION")

        ' Add the new TabPage to TabControl1
        PANELMAIN.TabPages.Add(newTab)

        ' Show the SalesOrder form inside the newly created TabPage
        ShowFormInTab(New Transaction_Execution(), newTab)

        ' Set focus to the newly added tab
        PANELMAIN.SelectedTab = newTab

    End Sub

    Private Sub Guna2Button11_Click(sender As Object, e As EventArgs) Handles Guna2Button11.Click

        If ROLETXT2.Text = "TRUCK-SIZER" Then

            SHOWEXECUTION()

        ElseIf ROLETXT2.Text = "ADMIN" Then

            SHOWEXECUTION()

        Else

            NOACCESSMSG.Show()

        End If

    End Sub

    Private Sub Guna2Button23_Click(sender As Object, e As EventArgs) Handles Guna2Button23.Click

        Setup_Van_Master_Manual.ShowDialog()

    End Sub

    Private Sub Guna2Button15_Click(sender As Object, e As EventArgs) Handles Guna2Button15.Click

        If SYSTEMSETTINGS.Height = 137 Then

            SYSTEMSETTINGS.Height = 0

        Else

            SYSTEMSETTINGS.Height = 137

        End If

    End Sub

    Public Sub SHOWRESULT()

        For Each tab As TabPage In PANELMAIN.TabPages
            If tab.Text = "RESULT" Then
                ' Remove the old tab
                PANELMAIN.TabPages.Remove(tab)
                Exit For
            End If
        Next

        ' Create a new TabPage named "RESULT"
        Dim newTab As New TabPage("RESULT")

        ' Add the new TabPage to TabControl1
        PANELMAIN.TabPages.Add(newTab)

        ' Show the SalesOrder form inside the newly created TabPage
        ShowFormInTab(New Transaction_Result(), newTab)

        ' Set focus to the newly added tab
        PANELMAIN.SelectedTab = newTab

    End Sub

    Private Sub Guna2Button12_Click(sender As Object, e As EventArgs) Handles Guna2Button12.Click

        SHOWRESULT()

    End Sub

    Public Sub SHOWCASHIER()

        For Each tab As TabPage In PANELMAIN.TabPages
            If tab.Text = "CASHIER" Then
                ' Remove the old tab
                PANELMAIN.TabPages.Remove(tab)
                Exit For
            End If
        Next

        ' Create a new TabPage named "RESULT"
        Dim newTab As New TabPage("CASHIER")

        ' Add the new TabPage to TabControl1
        PANELMAIN.TabPages.Add(newTab)

        ' Show the SalesOrder form inside the newly created TabPage
        ShowFormInTab(New Transaction_cashier(), newTab)

        ' Set focus to the newly added tab
        PANELMAIN.SelectedTab = newTab

    End Sub

    Private Sub Guna2Button13_Click(sender As Object, e As EventArgs) Handles Guna2Button13.Click

        If ROLETXT2.Text = "CASHIER" Then

            SHOWCASHIER()

        ElseIf ROLETXT2.Text = "ADMIN" Then

            SHOWCASHIER()

        Else

            NOACCESSMSG.Show()

        End If

    End Sub

    Public Sub SHOWRETRY()

        For Each tab As TabPage In PANELMAIN.TabPages
            If tab.Text = "RETRY" Then
                ' Remove the old tab
                PANELMAIN.TabPages.Remove(tab)
                Exit For
            End If
        Next

        ' Create a new TabPage named "RESULT"
        Dim newTab As New TabPage("RETRY")

        ' Add the new TabPage to TabControl1
        PANELMAIN.TabPages.Add(newTab)

        ' Show the SalesOrder form inside the newly created TabPage
        ShowFormInTab(New Transaction_Retry(), newTab)

        ' Set focus to the newly added tab
        PANELMAIN.SelectedTab = newTab

    End Sub
    Private Sub Guna2Button8_Click(sender As Object, e As EventArgs) Handles Guna2Button8.Click

        If ROLETXT2.Text = "TRUCK-SIZER" Then

            SHOWRETRY()

        ElseIf ROLETXT2.Text = "ADMIN" Then

            SHOWRETRY()

        Else

            NOACCESSMSG.Show()

        End If

    End Sub

    Public Sub SHOWAGENTMONITORING()

        For Each tab As TabPage In PANELMAIN.TabPages
            If tab.Text = "AGENTMONITORING" Then
                ' Remove the old tab
                PANELMAIN.TabPages.Remove(tab)
                Exit For
            End If
        Next

        ' Create a new TabPage named "RESULT"
        Dim newTab As New TabPage("AGENTMONITORING")

        ' Add the new TabPage to TabControl1
        PANELMAIN.TabPages.Add(newTab)

        ' Show the SalesOrder form inside the newly created TabPage
        ShowFormInTab(New Transaction_Agent_Monitoring(), newTab)

        ' Set focus to the newly added tab
        PANELMAIN.SelectedTab = newTab

    End Sub

    Private Sub Guna2Button14_Click(sender As Object, e As EventArgs) Handles Guna2Button14.Click

        SHOWAGENTMONITORING()

    End Sub

    Public Sub SHOWDELIVERYROUTE()

        For Each tab As TabPage In PANELMAIN.TabPages
            If tab.Text = "DELIVERYROUTE" Then
                ' Remove the old tab
                PANELMAIN.TabPages.Remove(tab)
                Exit For
            End If
        Next

        ' Create a new TabPage named "RESULT"
        Dim newTab As New TabPage("DELIVERYROUTE")

        ' Add the new TabPage to TabControl1
        PANELMAIN.TabPages.Add(newTab)

        ' Show the SalesOrder form inside the newly created TabPage
        ShowFormInTab(New Reports_Delivery_Route(), newTab)

        ' Set focus to the newly added tab
        PANELMAIN.SelectedTab = newTab

    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click

        SHOWDELIVERYROUTE()

    End Sub

    Public Sub SHOWDELIVERYPERFORMANCE()

        For Each tab As TabPage In PANELMAIN.TabPages
            If tab.Text = "DELIVERYPERFORMANCE" Then
                ' Remove the old tab
                PANELMAIN.TabPages.Remove(tab)
                Exit For
            End If
        Next

        ' Create a new TabPage named "RESULT"
        Dim newTab As New TabPage("DELIVERYPERFORMANCE")

        ' Add the new TabPage to TabControl1
        PANELMAIN.TabPages.Add(newTab)

        ' Show the SalesOrder form inside the newly created TabPage
        ShowFormInTab(New Reports_Delivery_Performance(), newTab)

        ' Set focus to the newly added tab
        PANELMAIN.SelectedTab = newTab

    End Sub

    Private Sub Guna2Button20_Click(sender As Object, e As EventArgs) Handles Guna2Button20.Click

        SHOWDELIVERYPERFORMANCE()

    End Sub

    Public Sub SHOWCROSSDOCK()

        For Each tab As TabPage In PANELMAIN.TabPages
            If tab.Text = "CROSSDOCK" Then
                ' Remove the old tab
                PANELMAIN.TabPages.Remove(tab)
                Exit For
            End If
        Next

        ' Create a new TabPage named "RESULT"
        Dim newTab As New TabPage("CROSSDOCK")

        ' Add the new TabPage to TabControl1
        PANELMAIN.TabPages.Add(newTab)

        ' Show the SalesOrder form inside the newly created TabPage
        ShowFormInTab(New Transaction_CrossDockView(), newTab)

        ' Set focus to the newly added tab
        PANELMAIN.SelectedTab = newTab

    End Sub

    Private Sub Guna2Button25_Click(sender As Object, e As EventArgs) Handles Guna2Button25.Click

        SHOWCROSSDOCK()

    End Sub

    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click

        Try

            views("SELECT 
                    AGENT_ID, 
                    SUM(CASE WHEN STATUS = 'FOR DELIVERY' THEN 1 ELSE 0 END) AS FOR_DELIVERY,
                    SUM(CASE WHEN STATUS = 'DELIVERED' THEN 1 ELSE 0 END) AS DELIVERED,
                    SUM(CASE WHEN STATUS = 'VERIFIED' THEN 1 ELSE 0 END) AS VERIFIED,
                    SUM(CASE WHEN STATUS = 'FAILED' THEN 1 ELSE 0 END) AS FAILED
                FROM Dash_Plan_Batch_Details
                WHERE DATE_TO_DELIVER BETWEEN '" & DTFROM.Value & "' AND '" & DTTO.Value & "'
                  AND COMPANY_ID = '" & COMPANYID.Text & "'
                  AND SITE_ID = '" & SITEID.Text & "'
                GROUP BY AGENT_ID
                ", "BSPIDB", DTGCOUNTINV)

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        Try

            views2("SELECT 
                    AGENT_ID, 
                    SUM(CASE WHEN STATUS = 'FOR DELIVERY' THEN TOTAL_AMOUNT ELSE 0 END) AS FOR_DELIVERY,
                    SUM(CASE WHEN STATUS = 'DELIVERED' THEN TOTAL_AMOUNT ELSE 0 END) AS DELIVERED,
                    SUM(CASE WHEN STATUS = 'VERIFIED' THEN TOTAL_AMOUNT ELSE 0 END) AS VERIFIED,
                    SUM(CASE WHEN STATUS = 'FAILED' THEN TOTAL_AMOUNT ELSE 0 END) AS FAILED
                FROM Dash_Plan_Batch_Details
                WHERE DATE_TO_DELIVER BETWEEN '" & DTFROM.Value & "' AND '" & DTTO.Value & "'
                  AND COMPANY_ID = '" & COMPANYID.Text & "'
                  AND SITE_ID = '" & SITEID.Text & "'
                GROUP BY AGENT_ID
                ", "BSPIDB", DTGAMOUNT)

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        DTGAMOUNT.Columns(1).DefaultCellStyle.Format = “N2”
        DTGAMOUNT.Columns(2).DefaultCellStyle.Format = “N2”
        DTGAMOUNT.Columns(3).DefaultCellStyle.Format = “N2”
        DTGAMOUNT.Columns(4).DefaultCellStyle.Format = “N2”

    End Sub

    Private Sub Guna2Button1_Click_1(sender As Object, e As EventArgs) Handles Guna2Button1.Click

        PANELMAIN.SelectedTab = MAINPAGE

    End Sub

    Private Sub Guna2Button24_Click(sender As Object, e As EventArgs) Handles Guna2Button24.Click

        If ROLETXT2.Text = "ADMIN" Then

            Settings_Agent_Logout.ShowDialog()

        Else

            NOACCESSMSG.Show()

        End If

    End Sub

    Private Sub Guna2ControlBox3_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox3.Click

        ' Optionally, set other properties (optional)

        ' Show the message dialog and get the result
        Dim result As DialogResult
        result = CLOSEMSG.Show()

        If result = DialogResult.Yes Then
            Application.Exit()
        End If

    End Sub

    Private Sub Guna2Button22_Click(sender As Object, e As EventArgs) Handles Guna2Button22.Click


        If ROLETXT2.Text = "ADMIN" Then

            '  Settings_Agent_Logout.ShowDialog()

        Else

            NOACCESSMSG.Show()

        End If

    End Sub

    Private Sub CMBCOLORTHEME_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMBCOLORTHEME.SelectedIndexChanged

        If CMBCOLORTHEME.Text = "DEFAULT" Then

            Guna2GradientPanel1.FillColor = Color.FromArgb(192, 64, 0)
            Guna2GradientPanel1.FillColor2 = Color.FromArgb(255, 128, 0)

        ElseIf CMBCOLORTHEME.Text = "BLUE" Then

            Guna2GradientPanel1.FillColor = Color.Navy
            Guna2GradientPanel1.FillColor2 = Color.FromArgb(0, 0, 64)

        ElseIf CMBCOLORTHEME.Text = "GREEN" Then


            Guna2GradientPanel1.FillColor = Color.Green
            Guna2GradientPanel1.FillColor2 = Color.FromArgb(0, 64, 0)

        ElseIf CMBCOLORTHEME.Text = "DARK" Then

            Guna2GradientPanel1.FillColor = Color.Black
            Guna2GradientPanel1.FillColor2 = Color.Black

        ElseIf CMBCOLORTHEME.Text = "LIGHT" Then

            Guna2GradientPanel1.FillColor = Color.DimGray
            Guna2GradientPanel1.FillColor2 = Color.WhiteSmoke

        End If

    End Sub

    Private Sub Guna2Button21_Click(sender As Object, e As EventArgs) Handles Guna2Button21.Click

        SHOWREPORTCROSSDOCK()

    End Sub

    Public Sub SHOWREPORTCROSSDOCK()

        For Each tab As TabPage In PANELMAIN.TabPages
            If tab.Text = "CROSSDOCKREPORT" Then
                ' Remove the old tab
                PANELMAIN.TabPages.Remove(tab)
                Exit For
            End If
        Next

        ' Create a new TabPage named "RESULT"
        Dim newTab As New TabPage("CROSSDOCKREPORT")

        ' Add the new TabPage to TabControl1
        PANELMAIN.TabPages.Add(newTab)

        ' Show the SalesOrder form inside the newly created TabPage
        ShowFormInTab(New Reports_Cross_Dock(), newTab)

        ' Set focus to the newly added tab
        PANELMAIN.SelectedTab = newTab

    End Sub

    Public Sub SHOWRESULTREPORT()

        For Each tab As TabPage In PANELMAIN.TabPages
            If tab.Text = "RESULTREPORT" Then
                ' Remove the old tab
                PANELMAIN.TabPages.Remove(tab)
                Exit For
            End If
        Next

        ' Create a new TabPage named "RESULT"
        Dim newTab As New TabPage("RESULTREPORT")

        ' Add the new TabPage to TabControl1
        PANELMAIN.TabPages.Add(newTab)

        ' Show the SalesOrder form inside the newly created TabPage
        ShowFormInTab(New Reports_Result(), newTab)

        ' Set focus to the newly added tab
        PANELMAIN.SelectedTab = newTab

    End Sub

    Private Sub Guna2Button19_Click(sender As Object, e As EventArgs) Handles Guna2Button19.Click

        SHOWRESULTREPORT()

    End Sub

    Public Sub SHOWPAYMENTSREPORT()

        For Each tab As TabPage In PANELMAIN.TabPages
            If tab.Text = "PAYMENTREPORT" Then
                ' Remove the old tab
                PANELMAIN.TabPages.Remove(tab)
                Exit For
            End If
        Next

        ' Create a new TabPage named "RESULT"
        Dim newTab As New TabPage("PAYMENTREPORT")

        ' Add the new TabPage to TabControl1
        PANELMAIN.TabPages.Add(newTab)

        ' Show the SalesOrder form inside the newly created TabPage
        ShowFormInTab(New Reports_Payments(), newTab)

        ' Set focus to the newly added tab
        PANELMAIN.SelectedTab = newTab

    End Sub

    Private Sub Guna2Button18_Click(sender As Object, e As EventArgs) Handles Guna2Button18.Click

        SHOWPAYMENTSREPORT()

    End Sub
End Class
