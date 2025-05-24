<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reports_Cross_Dock
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Reports_Cross_Dock))
        Me.GunaElipse1 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.GunaPictureBox1 = New Guna.UI.WinForms.GunaPictureBox()
        Me.GunaLabel2 = New Guna.UI.WinForms.GunaLabel()
        Me.GunaLabel1 = New Guna.UI.WinForms.GunaLabel()
        Me.GunaGroupBox1 = New Guna.UI.WinForms.GunaGroupBox()
        Me.ALLVEHICLECK = New Guna.UI.WinForms.GunaCheckBox()
        Me.cklist = New System.Windows.Forms.CheckedListBox()
        Me.GunaLabel3 = New Guna.UI.WinForms.GunaLabel()
        Me.DTFROM = New Guna.UI.WinForms.GunaDateTimePicker()
        Me.DTTO = New Guna.UI.WinForms.GunaDateTimePicker()
        Me.GunaLabel4 = New Guna.UI.WinForms.GunaLabel()
        Me.GunaAdvenceButton1 = New Guna.UI.WinForms.GunaAdvenceButton()
        Me.DTGDATA = New Guna.UI.WinForms.GunaDataGridView()
        Me.GunaAdvenceButton2 = New Guna.UI.WinForms.GunaAdvenceButton()
        Me.ALLSITECK = New Guna.UI.WinForms.GunaCheckBox()
        Me.SEARCH = New Guna.UI.WinForms.GunaTextBox()
        Me.GunaPanel1 = New Guna.UI.WinForms.GunaPanel()
        Me.STATUS = New Guna.UI.WinForms.GunaLabel()
        Me.Guna2Panel1.SuspendLayout()
        CType(Me.GunaPictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GunaGroupBox1.SuspendLayout()
        CType(Me.DTGDATA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GunaPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GunaElipse1
        '
        Me.GunaElipse1.TargetControl = Me
        '
        'Guna2Panel1
        '
        Me.Guna2Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Guna2Panel1.Controls.Add(Me.GunaPictureBox1)
        Me.Guna2Panel1.Controls.Add(Me.GunaLabel2)
        Me.Guna2Panel1.Controls.Add(Me.GunaLabel1)
        Me.Guna2Panel1.Location = New System.Drawing.Point(-1, 24)
        Me.Guna2Panel1.Name = "Guna2Panel1"
        Me.Guna2Panel1.ShadowDecoration.Enabled = True
        Me.Guna2Panel1.Size = New System.Drawing.Size(1205, 73)
        Me.Guna2Panel1.TabIndex = 24
        '
        'GunaPictureBox1
        '
        Me.GunaPictureBox1.BaseColor = System.Drawing.Color.White
        Me.GunaPictureBox1.Image = CType(resources.GetObject("GunaPictureBox1.Image"), System.Drawing.Image)
        Me.GunaPictureBox1.Location = New System.Drawing.Point(5, -1)
        Me.GunaPictureBox1.Name = "GunaPictureBox1"
        Me.GunaPictureBox1.Size = New System.Drawing.Size(79, 73)
        Me.GunaPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.GunaPictureBox1.TabIndex = 2
        Me.GunaPictureBox1.TabStop = False
        '
        'GunaLabel2
        '
        Me.GunaLabel2.AutoSize = True
        Me.GunaLabel2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.GunaLabel2.Location = New System.Drawing.Point(87, 41)
        Me.GunaLabel2.Name = "GunaLabel2"
        Me.GunaLabel2.Size = New System.Drawing.Size(132, 15)
        Me.GunaLabel2.TabIndex = 1
        Me.GunaLabel2.Text = "Show cross dock details"
        '
        'GunaLabel1
        '
        Me.GunaLabel1.AutoSize = True
        Me.GunaLabel1.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.GunaLabel1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.GunaLabel1.Location = New System.Drawing.Point(84, 15)
        Me.GunaLabel1.Name = "GunaLabel1"
        Me.GunaLabel1.Size = New System.Drawing.Size(205, 25)
        Me.GunaLabel1.TabIndex = 0
        Me.GunaLabel1.Text = "CROSS DOCK REPORT"
        '
        'GunaGroupBox1
        '
        Me.GunaGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GunaGroupBox1.BaseColor = System.Drawing.Color.White
        Me.GunaGroupBox1.BorderColor = System.Drawing.Color.Gainsboro
        Me.GunaGroupBox1.BorderSize = 1
        Me.GunaGroupBox1.Controls.Add(Me.cklist)
        Me.GunaGroupBox1.Controls.Add(Me.ALLVEHICLECK)
        Me.GunaGroupBox1.LineColor = System.Drawing.Color.Gainsboro
        Me.GunaGroupBox1.Location = New System.Drawing.Point(5, 103)
        Me.GunaGroupBox1.Name = "GunaGroupBox1"
        Me.GunaGroupBox1.Radius = 10
        Me.GunaGroupBox1.Size = New System.Drawing.Size(283, 152)
        Me.GunaGroupBox1.TabIndex = 25
        Me.GunaGroupBox1.Text = "SELECT VEHICLE"
        Me.GunaGroupBox1.TextLocation = New System.Drawing.Point(10, 8)
        '
        'ALLVEHICLECK
        '
        Me.ALLVEHICLECK.BaseColor = System.Drawing.Color.White
        Me.ALLVEHICLECK.CheckedOffColor = System.Drawing.Color.Gray
        Me.ALLVEHICLECK.CheckedOnColor = System.Drawing.Color.Navy
        Me.ALLVEHICLECK.FillColor = System.Drawing.Color.White
        Me.ALLVEHICLECK.Location = New System.Drawing.Point(235, 6)
        Me.ALLVEHICLECK.Name = "ALLVEHICLECK"
        Me.ALLVEHICLECK.Size = New System.Drawing.Size(50, 20)
        Me.ALLVEHICLECK.TabIndex = 0
        Me.ALLVEHICLECK.Text = "ALL"
        '
        'cklist
        '
        Me.cklist.AllowDrop = True
        Me.cklist.FormattingEnabled = True
        Me.cklist.Location = New System.Drawing.Point(5, 36)
        Me.cklist.Name = "cklist"
        Me.cklist.Size = New System.Drawing.Size(273, 109)
        Me.cklist.TabIndex = 12
        '
        'GunaLabel3
        '
        Me.GunaLabel3.AutoSize = True
        Me.GunaLabel3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.GunaLabel3.Location = New System.Drawing.Point(296, 119)
        Me.GunaLabel3.Name = "GunaLabel3"
        Me.GunaLabel3.Size = New System.Drawing.Size(43, 15)
        Me.GunaLabel3.TabIndex = 26
        Me.GunaLabel3.Text = "FROM:"
        '
        'DTFROM
        '
        Me.DTFROM.BackColor = System.Drawing.Color.Transparent
        Me.DTFROM.BaseColor = System.Drawing.Color.White
        Me.DTFROM.BorderColor = System.Drawing.Color.Silver
        Me.DTFROM.CustomFormat = Nothing
        Me.DTFROM.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right
        Me.DTFROM.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DTFROM.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.DTFROM.ForeColor = System.Drawing.Color.Black
        Me.DTFROM.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTFROM.Location = New System.Drawing.Point(341, 112)
        Me.DTFROM.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DTFROM.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DTFROM.Name = "DTFROM"
        Me.DTFROM.OnHoverBaseColor = System.Drawing.Color.White
        Me.DTFROM.OnHoverBorderColor = System.Drawing.Color.Navy
        Me.DTFROM.OnHoverForeColor = System.Drawing.Color.Navy
        Me.DTFROM.OnPressedColor = System.Drawing.Color.Black
        Me.DTFROM.Radius = 8
        Me.DTFROM.Size = New System.Drawing.Size(118, 30)
        Me.DTFROM.TabIndex = 27
        Me.DTFROM.Text = "5/18/2025"
        Me.DTFROM.Value = New Date(2025, 5, 18, 10, 35, 0, 403)
        '
        'DTTO
        '
        Me.DTTO.BackColor = System.Drawing.Color.Transparent
        Me.DTTO.BaseColor = System.Drawing.Color.White
        Me.DTTO.BorderColor = System.Drawing.Color.Silver
        Me.DTTO.CustomFormat = Nothing
        Me.DTTO.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right
        Me.DTTO.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DTTO.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.DTTO.ForeColor = System.Drawing.Color.Black
        Me.DTTO.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTTO.Location = New System.Drawing.Point(489, 112)
        Me.DTTO.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DTTO.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DTTO.Name = "DTTO"
        Me.DTTO.OnHoverBaseColor = System.Drawing.Color.White
        Me.DTTO.OnHoverBorderColor = System.Drawing.Color.Navy
        Me.DTTO.OnHoverForeColor = System.Drawing.Color.Navy
        Me.DTTO.OnPressedColor = System.Drawing.Color.Black
        Me.DTTO.Radius = 8
        Me.DTTO.Size = New System.Drawing.Size(118, 30)
        Me.DTTO.TabIndex = 29
        Me.DTTO.Text = "5/18/2025"
        Me.DTTO.Value = New Date(2025, 5, 18, 10, 35, 0, 403)
        '
        'GunaLabel4
        '
        Me.GunaLabel4.AutoSize = True
        Me.GunaLabel4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.GunaLabel4.Location = New System.Drawing.Point(462, 119)
        Me.GunaLabel4.Name = "GunaLabel4"
        Me.GunaLabel4.Size = New System.Drawing.Size(25, 15)
        Me.GunaLabel4.TabIndex = 28
        Me.GunaLabel4.Text = "TO:"
        '
        'GunaAdvenceButton1
        '
        Me.GunaAdvenceButton1.AnimationHoverSpeed = 0.07!
        Me.GunaAdvenceButton1.AnimationSpeed = 0.03!
        Me.GunaAdvenceButton1.BackColor = System.Drawing.Color.Transparent
        Me.GunaAdvenceButton1.BaseColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GunaAdvenceButton1.BorderColor = System.Drawing.Color.Black
        Me.GunaAdvenceButton1.CheckedBaseColor = System.Drawing.Color.Gray
        Me.GunaAdvenceButton1.CheckedBorderColor = System.Drawing.Color.Black
        Me.GunaAdvenceButton1.CheckedForeColor = System.Drawing.Color.White
        Me.GunaAdvenceButton1.CheckedImage = CType(resources.GetObject("GunaAdvenceButton1.CheckedImage"), System.Drawing.Image)
        Me.GunaAdvenceButton1.CheckedLineColor = System.Drawing.Color.DimGray
        Me.GunaAdvenceButton1.DialogResult = System.Windows.Forms.DialogResult.None
        Me.GunaAdvenceButton1.FocusedColor = System.Drawing.Color.Empty
        Me.GunaAdvenceButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.GunaAdvenceButton1.ForeColor = System.Drawing.Color.White
        Me.GunaAdvenceButton1.Image = CType(resources.GetObject("GunaAdvenceButton1.Image"), System.Drawing.Image)
        Me.GunaAdvenceButton1.ImageSize = New System.Drawing.Size(20, 20)
        Me.GunaAdvenceButton1.LineColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.GunaAdvenceButton1.Location = New System.Drawing.Point(613, 107)
        Me.GunaAdvenceButton1.Name = "GunaAdvenceButton1"
        Me.GunaAdvenceButton1.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GunaAdvenceButton1.OnHoverBorderColor = System.Drawing.Color.Black
        Me.GunaAdvenceButton1.OnHoverForeColor = System.Drawing.Color.White
        Me.GunaAdvenceButton1.OnHoverImage = Nothing
        Me.GunaAdvenceButton1.OnHoverLineColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.GunaAdvenceButton1.OnPressedColor = System.Drawing.Color.Black
        Me.GunaAdvenceButton1.Radius = 8
        Me.GunaAdvenceButton1.Size = New System.Drawing.Size(126, 42)
        Me.GunaAdvenceButton1.TabIndex = 30
        Me.GunaAdvenceButton1.Text = "LOAD DATA"
        '
        'DTGDATA
        '
        Me.DTGDATA.AllowUserToAddRows = False
        Me.DTGDATA.AllowUserToDeleteRows = False
        Me.DTGDATA.AllowUserToResizeColumns = False
        Me.DTGDATA.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.DTGDATA.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DTGDATA.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DTGDATA.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DTGDATA.BackgroundColor = System.Drawing.Color.White
        Me.DTGDATA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DTGDATA.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DTGDATA.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(34, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DTGDATA.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DTGDATA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(211, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(107, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DTGDATA.DefaultCellStyle = DataGridViewCellStyle3
        Me.DTGDATA.EnableHeadersVisualStyles = False
        Me.DTGDATA.GridColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(203, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.DTGDATA.Location = New System.Drawing.Point(5, 261)
        Me.DTGDATA.Name = "DTGDATA"
        Me.DTGDATA.ReadOnly = True
        Me.DTGDATA.RowHeadersVisible = False
        Me.DTGDATA.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DTGDATA.Size = New System.Drawing.Size(1195, 383)
        Me.DTGDATA.TabIndex = 31
        Me.DTGDATA.Theme = Guna.UI.WinForms.GunaDataGridViewPresetThemes.DeepOrange
        Me.DTGDATA.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.DTGDATA.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.DTGDATA.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.DTGDATA.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.DTGDATA.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.DTGDATA.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.DTGDATA.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(203, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.DTGDATA.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.DTGDATA.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DTGDATA.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.DTGDATA.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.DTGDATA.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DTGDATA.ThemeStyle.HeaderStyle.Height = 23
        Me.DTGDATA.ThemeStyle.ReadOnly = True
        Me.DTGDATA.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.DTGDATA.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DTGDATA.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.DTGDATA.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black
        Me.DTGDATA.ThemeStyle.RowsStyle.Height = 22
        Me.DTGDATA.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(107, Byte), Integer))
        Me.DTGDATA.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black
        '
        'GunaAdvenceButton2
        '
        Me.GunaAdvenceButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GunaAdvenceButton2.AnimationHoverSpeed = 0.07!
        Me.GunaAdvenceButton2.AnimationSpeed = 0.03!
        Me.GunaAdvenceButton2.BackColor = System.Drawing.Color.Transparent
        Me.GunaAdvenceButton2.BaseColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GunaAdvenceButton2.BorderColor = System.Drawing.Color.Black
        Me.GunaAdvenceButton2.CheckedBaseColor = System.Drawing.Color.Gray
        Me.GunaAdvenceButton2.CheckedBorderColor = System.Drawing.Color.Black
        Me.GunaAdvenceButton2.CheckedForeColor = System.Drawing.Color.White
        Me.GunaAdvenceButton2.CheckedImage = CType(resources.GetObject("GunaAdvenceButton2.CheckedImage"), System.Drawing.Image)
        Me.GunaAdvenceButton2.CheckedLineColor = System.Drawing.Color.DimGray
        Me.GunaAdvenceButton2.DialogResult = System.Windows.Forms.DialogResult.None
        Me.GunaAdvenceButton2.FocusedColor = System.Drawing.Color.Empty
        Me.GunaAdvenceButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.GunaAdvenceButton2.ForeColor = System.Drawing.Color.White
        Me.GunaAdvenceButton2.Image = CType(resources.GetObject("GunaAdvenceButton2.Image"), System.Drawing.Image)
        Me.GunaAdvenceButton2.ImageSize = New System.Drawing.Size(20, 20)
        Me.GunaAdvenceButton2.LineColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.GunaAdvenceButton2.Location = New System.Drawing.Point(997, 650)
        Me.GunaAdvenceButton2.Name = "GunaAdvenceButton2"
        Me.GunaAdvenceButton2.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GunaAdvenceButton2.OnHoverBorderColor = System.Drawing.Color.Black
        Me.GunaAdvenceButton2.OnHoverForeColor = System.Drawing.Color.White
        Me.GunaAdvenceButton2.OnHoverImage = Nothing
        Me.GunaAdvenceButton2.OnHoverLineColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.GunaAdvenceButton2.OnPressedColor = System.Drawing.Color.Black
        Me.GunaAdvenceButton2.Radius = 8
        Me.GunaAdvenceButton2.Size = New System.Drawing.Size(202, 42)
        Me.GunaAdvenceButton2.TabIndex = 32
        Me.GunaAdvenceButton2.Text = "EXPORT REPORT"
        Me.GunaAdvenceButton2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ALLSITECK
        '
        Me.ALLSITECK.BaseColor = System.Drawing.Color.White
        Me.ALLSITECK.CheckedOffColor = System.Drawing.Color.Gray
        Me.ALLSITECK.CheckedOnColor = System.Drawing.Color.Navy
        Me.ALLSITECK.FillColor = System.Drawing.Color.White
        Me.ALLSITECK.Location = New System.Drawing.Point(745, 119)
        Me.ALLSITECK.Name = "ALLSITECK"
        Me.ALLSITECK.Size = New System.Drawing.Size(78, 20)
        Me.ALLSITECK.TabIndex = 13
        Me.ALLSITECK.Text = "ALL SITE"
        '
        'SEARCH
        '
        Me.SEARCH.BaseColor = System.Drawing.Color.White
        Me.SEARCH.BorderColor = System.Drawing.Color.Silver
        Me.SEARCH.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.SEARCH.FocusedBaseColor = System.Drawing.Color.White
        Me.SEARCH.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SEARCH.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.SEARCH.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.SEARCH.Location = New System.Drawing.Point(30, 37)
        Me.SEARCH.Name = "SEARCH"
        Me.SEARCH.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.SEARCH.SelectedText = ""
        Me.SEARCH.Size = New System.Drawing.Size(23, 26)
        Me.SEARCH.TabIndex = 33
        Me.SEARCH.Text = "GunaTextBox1"
        '
        'GunaPanel1
        '
        Me.GunaPanel1.Controls.Add(Me.SEARCH)
        Me.GunaPanel1.Location = New System.Drawing.Point(936, 112)
        Me.GunaPanel1.Name = "GunaPanel1"
        Me.GunaPanel1.Size = New System.Drawing.Size(10, 10)
        Me.GunaPanel1.TabIndex = 34
        '
        'STATUS
        '
        Me.STATUS.AutoSize = True
        Me.STATUS.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic)
        Me.STATUS.Location = New System.Drawing.Point(618, 156)
        Me.STATUS.Name = "STATUS"
        Me.STATUS.Size = New System.Drawing.Size(12, 15)
        Me.STATUS.TabIndex = 35
        Me.STATUS.Text = "-"
        '
        'Reports_Cross_Dock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1204, 700)
        Me.Controls.Add(Me.STATUS)
        Me.Controls.Add(Me.GunaPanel1)
        Me.Controls.Add(Me.ALLSITECK)
        Me.Controls.Add(Me.GunaAdvenceButton2)
        Me.Controls.Add(Me.DTGDATA)
        Me.Controls.Add(Me.GunaAdvenceButton1)
        Me.Controls.Add(Me.DTTO)
        Me.Controls.Add(Me.GunaLabel4)
        Me.Controls.Add(Me.DTFROM)
        Me.Controls.Add(Me.GunaLabel3)
        Me.Controls.Add(Me.GunaGroupBox1)
        Me.Controls.Add(Me.Guna2Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Reports_Cross_Dock"
        Me.Text = "Reports_Cross_Dock"
        Me.Guna2Panel1.ResumeLayout(False)
        Me.Guna2Panel1.PerformLayout()
        CType(Me.GunaPictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GunaGroupBox1.ResumeLayout(False)
        Me.GunaGroupBox1.PerformLayout()
        CType(Me.DTGDATA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GunaPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GunaElipse1 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents DTFROM As Guna.UI.WinForms.GunaDateTimePicker
    Friend WithEvents GunaLabel3 As Guna.UI.WinForms.GunaLabel
    Friend WithEvents GunaGroupBox1 As Guna.UI.WinForms.GunaGroupBox
    Friend WithEvents cklist As CheckedListBox
    Friend WithEvents ALLVEHICLECK As Guna.UI.WinForms.GunaCheckBox
    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents GunaPictureBox1 As Guna.UI.WinForms.GunaPictureBox
    Friend WithEvents GunaLabel2 As Guna.UI.WinForms.GunaLabel
    Friend WithEvents GunaLabel1 As Guna.UI.WinForms.GunaLabel
    Friend WithEvents DTTO As Guna.UI.WinForms.GunaDateTimePicker
    Friend WithEvents GunaLabel4 As Guna.UI.WinForms.GunaLabel
    Friend WithEvents GunaAdvenceButton1 As Guna.UI.WinForms.GunaAdvenceButton
    Friend WithEvents GunaAdvenceButton2 As Guna.UI.WinForms.GunaAdvenceButton
    Friend WithEvents DTGDATA As Guna.UI.WinForms.GunaDataGridView
    Friend WithEvents ALLSITECK As Guna.UI.WinForms.GunaCheckBox
    Friend WithEvents SEARCH As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents GunaPanel1 As Guna.UI.WinForms.GunaPanel
    Friend WithEvents STATUS As Guna.UI.WinForms.GunaLabel
End Class
