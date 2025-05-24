<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reports_Order_Plan_Re_Assign_Per_Seller
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Reports_Order_Plan_Re_Assign_Per_Seller))
        Me.GunaElipse1 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.GunaGroupBox2 = New Guna.UI.WinForms.GunaGroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GunaAdvenceButton2 = New Guna.UI.WinForms.GunaAdvenceButton()
        Me.DTGPLANS = New Guna.UI.WinForms.GunaDataGridView()
        Me.GunaGroupBox1 = New Guna.UI.WinForms.GunaGroupBox()
        Me.CKLIST = New System.Windows.Forms.CheckedListBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BATCHNUMBER = New Guna.UI.WinForms.GunaTextBox()
        Me.GunaTextBox1 = New Guna.UI.WinForms.GunaTextBox()
        Me.SITEID = New Guna.UI.WinForms.GunaTextBox()
        Me.CHECKID = New Guna.UI.WinForms.GunaTextBox()
        Me.DTGCHECK = New Guna.UI.WinForms.GunaDataGridView()
        Me.SEARCH = New Guna.UI.WinForms.GunaTextBox()
        Me.SOPLANNUMBER = New Guna.UI.WinForms.GunaTextBox()
        Me.GunaLabel2 = New Guna.UI.WinForms.GunaLabel()
        Me.GunaProgressBar1 = New Guna.UI.WinForms.GunaProgressBar()
        Me.GunaAdvenceButton1 = New Guna.UI.WinForms.GunaAdvenceButton()
        Me.DTDELIVERY = New Guna.UI.WinForms.GunaDateTimePicker()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.GunaGroupBox2.SuspendLayout()
        CType(Me.DTGPLANS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GunaGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DTGCHECK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GunaElipse1
        '
        Me.GunaElipse1.Radius = 8
        Me.GunaElipse1.TargetControl = Me
        '
        'GunaGroupBox2
        '
        Me.GunaGroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GunaGroupBox2.BaseColor = System.Drawing.Color.White
        Me.GunaGroupBox2.BorderColor = System.Drawing.Color.Gainsboro
        Me.GunaGroupBox2.BorderSize = 2
        Me.GunaGroupBox2.Controls.Add(Me.Label1)
        Me.GunaGroupBox2.Controls.Add(Me.GunaAdvenceButton2)
        Me.GunaGroupBox2.Controls.Add(Me.DTGPLANS)
        Me.GunaGroupBox2.LineColor = System.Drawing.Color.Gainsboro
        Me.GunaGroupBox2.Location = New System.Drawing.Point(286, 29)
        Me.GunaGroupBox2.Name = "GunaGroupBox2"
        Me.GunaGroupBox2.Radius = 9
        Me.GunaGroupBox2.Size = New System.Drawing.Size(525, 293)
        Me.GunaGroupBox2.TabIndex = 10
        Me.GunaGroupBox2.Text = "PLAN NUMBER"
        Me.GunaGroupBox2.TextLocation = New System.Drawing.Point(10, 8)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.DarkRed
        Me.Label1.Location = New System.Drawing.Point(309, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(209, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "DOUBLE CLICK TO ASSIGN SUB-BATCH"
        '
        'GunaAdvenceButton2
        '
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
        Me.GunaAdvenceButton2.Location = New System.Drawing.Point(7, 34)
        Me.GunaAdvenceButton2.Name = "GunaAdvenceButton2"
        Me.GunaAdvenceButton2.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GunaAdvenceButton2.OnHoverBorderColor = System.Drawing.Color.Black
        Me.GunaAdvenceButton2.OnHoverForeColor = System.Drawing.Color.White
        Me.GunaAdvenceButton2.OnHoverImage = Nothing
        Me.GunaAdvenceButton2.OnHoverLineColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GunaAdvenceButton2.OnPressedColor = System.Drawing.Color.Black
        Me.GunaAdvenceButton2.Radius = 9
        Me.GunaAdvenceButton2.Size = New System.Drawing.Size(118, 30)
        Me.GunaAdvenceButton2.TabIndex = 4
        Me.GunaAdvenceButton2.Text = "NEW"
        Me.GunaAdvenceButton2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DTGPLANS
        '
        Me.DTGPLANS.AllowUserToAddRows = False
        Me.DTGPLANS.AllowUserToDeleteRows = False
        Me.DTGPLANS.AllowUserToResizeColumns = False
        Me.DTGPLANS.AllowUserToResizeRows = False
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.DTGPLANS.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        Me.DTGPLANS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DTGPLANS.BackgroundColor = System.Drawing.Color.White
        Me.DTGPLANS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DTGPLANS.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DTGPLANS.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(34, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DTGPLANS.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.DTGPLANS.ColumnHeadersHeight = 25
        Me.DTGPLANS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(211, Byte), Integer))
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(107, Byte), Integer))
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DTGPLANS.DefaultCellStyle = DataGridViewCellStyle9
        Me.DTGPLANS.EnableHeadersVisualStyles = False
        Me.DTGPLANS.GridColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(203, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.DTGPLANS.Location = New System.Drawing.Point(3, 68)
        Me.DTGPLANS.Name = "DTGPLANS"
        Me.DTGPLANS.ReadOnly = True
        Me.DTGPLANS.RowHeadersVisible = False
        Me.DTGPLANS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DTGPLANS.Size = New System.Drawing.Size(519, 222)
        Me.DTGPLANS.TabIndex = 1
        Me.DTGPLANS.Theme = Guna.UI.WinForms.GunaDataGridViewPresetThemes.DeepOrange
        Me.DTGPLANS.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.DTGPLANS.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.DTGPLANS.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.DTGPLANS.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.DTGPLANS.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.DTGPLANS.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.DTGPLANS.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(203, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.DTGPLANS.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.DTGPLANS.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DTGPLANS.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.DTGPLANS.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.DTGPLANS.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DTGPLANS.ThemeStyle.HeaderStyle.Height = 25
        Me.DTGPLANS.ThemeStyle.ReadOnly = True
        Me.DTGPLANS.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.DTGPLANS.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DTGPLANS.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.DTGPLANS.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black
        Me.DTGPLANS.ThemeStyle.RowsStyle.Height = 22
        Me.DTGPLANS.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(107, Byte), Integer))
        Me.DTGPLANS.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black
        '
        'GunaGroupBox1
        '
        Me.GunaGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GunaGroupBox1.BaseColor = System.Drawing.Color.White
        Me.GunaGroupBox1.BorderColor = System.Drawing.Color.Gainsboro
        Me.GunaGroupBox1.BorderSize = 2
        Me.GunaGroupBox1.Controls.Add(Me.CKLIST)
        Me.GunaGroupBox1.LineColor = System.Drawing.Color.Gainsboro
        Me.GunaGroupBox1.Location = New System.Drawing.Point(6, 28)
        Me.GunaGroupBox1.Name = "GunaGroupBox1"
        Me.GunaGroupBox1.Radius = 9
        Me.GunaGroupBox1.Size = New System.Drawing.Size(275, 355)
        Me.GunaGroupBox1.TabIndex = 9
        Me.GunaGroupBox1.Text = "SELLERS"
        Me.GunaGroupBox1.TextLocation = New System.Drawing.Point(10, 8)
        '
        'CKLIST
        '
        Me.CKLIST.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CKLIST.FormattingEnabled = True
        Me.CKLIST.Location = New System.Drawing.Point(6, 39)
        Me.CKLIST.Name = "CKLIST"
        Me.CKLIST.Size = New System.Drawing.Size(266, 308)
        Me.CKLIST.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.DTDELIVERY)
        Me.Panel1.Controls.Add(Me.BATCHNUMBER)
        Me.Panel1.Controls.Add(Me.GunaTextBox1)
        Me.Panel1.Controls.Add(Me.SITEID)
        Me.Panel1.Controls.Add(Me.CHECKID)
        Me.Panel1.Controls.Add(Me.DTGCHECK)
        Me.Panel1.Controls.Add(Me.SEARCH)
        Me.Panel1.Controls.Add(Me.SOPLANNUMBER)
        Me.Panel1.Location = New System.Drawing.Point(471, 13)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(10, 10)
        Me.Panel1.TabIndex = 12
        '
        'BATCHNUMBER
        '
        Me.BATCHNUMBER.BaseColor = System.Drawing.Color.White
        Me.BATCHNUMBER.BorderColor = System.Drawing.Color.Silver
        Me.BATCHNUMBER.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.BATCHNUMBER.FocusedBaseColor = System.Drawing.Color.White
        Me.BATCHNUMBER.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BATCHNUMBER.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.BATCHNUMBER.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.BATCHNUMBER.Location = New System.Drawing.Point(58, 28)
        Me.BATCHNUMBER.Name = "BATCHNUMBER"
        Me.BATCHNUMBER.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.BATCHNUMBER.SelectedText = ""
        Me.BATCHNUMBER.Size = New System.Drawing.Size(18, 31)
        Me.BATCHNUMBER.TabIndex = 8
        '
        'GunaTextBox1
        '
        Me.GunaTextBox1.BaseColor = System.Drawing.Color.White
        Me.GunaTextBox1.BorderColor = System.Drawing.Color.Silver
        Me.GunaTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.GunaTextBox1.FocusedBaseColor = System.Drawing.Color.White
        Me.GunaTextBox1.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GunaTextBox1.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.GunaTextBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.GunaTextBox1.Location = New System.Drawing.Point(41, 40)
        Me.GunaTextBox1.Name = "GunaTextBox1"
        Me.GunaTextBox1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.GunaTextBox1.SelectedText = ""
        Me.GunaTextBox1.Size = New System.Drawing.Size(18, 31)
        Me.GunaTextBox1.TabIndex = 7
        '
        'SITEID
        '
        Me.SITEID.BaseColor = System.Drawing.Color.White
        Me.SITEID.BorderColor = System.Drawing.Color.Silver
        Me.SITEID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.SITEID.FocusedBaseColor = System.Drawing.Color.White
        Me.SITEID.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SITEID.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.SITEID.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.SITEID.Location = New System.Drawing.Point(17, 55)
        Me.SITEID.Name = "SITEID"
        Me.SITEID.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.SITEID.SelectedText = ""
        Me.SITEID.Size = New System.Drawing.Size(18, 31)
        Me.SITEID.TabIndex = 6
        '
        'CHECKID
        '
        Me.CHECKID.BaseColor = System.Drawing.Color.White
        Me.CHECKID.BorderColor = System.Drawing.Color.Silver
        Me.CHECKID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.CHECKID.FocusedBaseColor = System.Drawing.Color.White
        Me.CHECKID.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CHECKID.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.CHECKID.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.CHECKID.Location = New System.Drawing.Point(2, 28)
        Me.CHECKID.Name = "CHECKID"
        Me.CHECKID.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.CHECKID.SelectedText = ""
        Me.CHECKID.Size = New System.Drawing.Size(18, 31)
        Me.CHECKID.TabIndex = 5
        '
        'DTGCHECK
        '
        Me.DTGCHECK.AllowUserToAddRows = False
        Me.DTGCHECK.AllowUserToDeleteRows = False
        Me.DTGCHECK.AllowUserToResizeColumns = False
        Me.DTGCHECK.AllowUserToResizeRows = False
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(189, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.DTGCHECK.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle10
        Me.DTGCHECK.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DTGCHECK.BackgroundColor = System.Drawing.Color.White
        Me.DTGCHECK.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DTGCHECK.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DTGCHECK.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(242, Byte), Integer))
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DTGCHECK.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.DTGCHECK.ColumnHeadersHeight = 25
        Me.DTGCHECK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(252, Byte), Integer))
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        DataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DTGCHECK.DefaultCellStyle = DataGridViewCellStyle12
        Me.DTGCHECK.EnableHeadersVisualStyles = False
        Me.DTGCHECK.GridColor = System.Drawing.Color.FromArgb(CType(CType(187, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.DTGCHECK.Location = New System.Drawing.Point(50, 19)
        Me.DTGCHECK.Name = "DTGCHECK"
        Me.DTGCHECK.ReadOnly = True
        Me.DTGCHECK.RowHeadersVisible = False
        Me.DTGCHECK.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DTGCHECK.Size = New System.Drawing.Size(34, 15)
        Me.DTGCHECK.TabIndex = 2
        Me.DTGCHECK.Theme = Guna.UI.WinForms.GunaDataGridViewPresetThemes.Blue
        Me.DTGCHECK.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(189, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.DTGCHECK.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.DTGCHECK.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.DTGCHECK.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.DTGCHECK.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.DTGCHECK.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.DTGCHECK.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(187, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.DTGCHECK.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.DTGCHECK.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DTGCHECK.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.DTGCHECK.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.DTGCHECK.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DTGCHECK.ThemeStyle.HeaderStyle.Height = 25
        Me.DTGCHECK.ThemeStyle.ReadOnly = True
        Me.DTGCHECK.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.DTGCHECK.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DTGCHECK.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.DTGCHECK.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black
        Me.DTGCHECK.ThemeStyle.RowsStyle.Height = 22
        Me.DTGCHECK.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.DTGCHECK.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black
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
        Me.SEARCH.Location = New System.Drawing.Point(17, 93)
        Me.SEARCH.Name = "SEARCH"
        Me.SEARCH.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.SEARCH.SelectedText = ""
        Me.SEARCH.Size = New System.Drawing.Size(81, 31)
        Me.SEARCH.TabIndex = 4
        '
        'SOPLANNUMBER
        '
        Me.SOPLANNUMBER.BaseColor = System.Drawing.Color.White
        Me.SOPLANNUMBER.BorderColor = System.Drawing.Color.Silver
        Me.SOPLANNUMBER.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.SOPLANNUMBER.FocusedBaseColor = System.Drawing.Color.White
        Me.SOPLANNUMBER.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SOPLANNUMBER.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.SOPLANNUMBER.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.SOPLANNUMBER.Location = New System.Drawing.Point(26, 25)
        Me.SOPLANNUMBER.Name = "SOPLANNUMBER"
        Me.SOPLANNUMBER.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.SOPLANNUMBER.SelectedText = ""
        Me.SOPLANNUMBER.Size = New System.Drawing.Size(81, 31)
        Me.SOPLANNUMBER.TabIndex = 3
        '
        'GunaLabel2
        '
        Me.GunaLabel2.AutoSize = True
        Me.GunaLabel2.BackColor = System.Drawing.Color.Maroon
        Me.GunaLabel2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GunaLabel2.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.GunaLabel2.Location = New System.Drawing.Point(798, 6)
        Me.GunaLabel2.Name = "GunaLabel2"
        Me.GunaLabel2.Size = New System.Drawing.Size(14, 15)
        Me.GunaLabel2.TabIndex = 11
        Me.GunaLabel2.Text = "X"
        '
        'GunaProgressBar1
        '
        Me.GunaProgressBar1.BackColor = System.Drawing.Color.Transparent
        Me.GunaProgressBar1.BorderColor = System.Drawing.Color.Black
        Me.GunaProgressBar1.ColorStyle = Guna.UI.WinForms.ColorStyle.[Default]
        Me.GunaProgressBar1.IdleColor = System.Drawing.Color.Gainsboro
        Me.GunaProgressBar1.Location = New System.Drawing.Point(290, 372)
        Me.GunaProgressBar1.Name = "GunaProgressBar1"
        Me.GunaProgressBar1.ProgressMaxColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GunaProgressBar1.ProgressMinColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GunaProgressBar1.Radius = 4
        Me.GunaProgressBar1.Size = New System.Drawing.Size(521, 10)
        Me.GunaProgressBar1.TabIndex = 14
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
        Me.GunaAdvenceButton1.Location = New System.Drawing.Point(290, 325)
        Me.GunaAdvenceButton1.Name = "GunaAdvenceButton1"
        Me.GunaAdvenceButton1.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GunaAdvenceButton1.OnHoverBorderColor = System.Drawing.Color.Black
        Me.GunaAdvenceButton1.OnHoverForeColor = System.Drawing.Color.White
        Me.GunaAdvenceButton1.OnHoverImage = Nothing
        Me.GunaAdvenceButton1.OnHoverLineColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.GunaAdvenceButton1.OnPressedColor = System.Drawing.Color.Black
        Me.GunaAdvenceButton1.Radius = 8
        Me.GunaAdvenceButton1.Size = New System.Drawing.Size(522, 43)
        Me.GunaAdvenceButton1.TabIndex = 13
        Me.GunaAdvenceButton1.Text = "ASSIGN"
        Me.GunaAdvenceButton1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DTDELIVERY
        '
        Me.DTDELIVERY.BackColor = System.Drawing.Color.Transparent
        Me.DTDELIVERY.BaseColor = System.Drawing.Color.White
        Me.DTDELIVERY.BorderColor = System.Drawing.Color.Silver
        Me.DTDELIVERY.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DTDELIVERY.CustomFormat = Nothing
        Me.DTDELIVERY.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right
        Me.DTDELIVERY.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DTDELIVERY.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.DTDELIVERY.ForeColor = System.Drawing.Color.Black
        Me.DTDELIVERY.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTDELIVERY.Location = New System.Drawing.Point(-27, 10)
        Me.DTDELIVERY.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DTDELIVERY.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DTDELIVERY.Name = "DTDELIVERY"
        Me.DTDELIVERY.OnHoverBaseColor = System.Drawing.Color.White
        Me.DTDELIVERY.OnHoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DTDELIVERY.OnHoverForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DTDELIVERY.OnPressedColor = System.Drawing.Color.Black
        Me.DTDELIVERY.Radius = 9
        Me.DTDELIVERY.Size = New System.Drawing.Size(123, 30)
        Me.DTDELIVERY.TabIndex = 19
        Me.DTDELIVERY.Text = "11/15/2023"
        Me.DTDELIVERY.Value = New Date(2023, 11, 15, 11, 27, 1, 325)
        '
        'Reports_Order_Plan_Re_Assign_Per_Seller
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(819, 388)
        Me.Controls.Add(Me.GunaProgressBar1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GunaAdvenceButton1)
        Me.Controls.Add(Me.GunaGroupBox2)
        Me.Controls.Add(Me.GunaGroupBox1)
        Me.Controls.Add(Me.GunaLabel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Reports_Order_Plan_Re_Assign_Per_Seller"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reports_Order_Plan_Re_Assign_Per_Seller"
        Me.GunaGroupBox2.ResumeLayout(False)
        Me.GunaGroupBox2.PerformLayout()
        CType(Me.DTGPLANS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GunaGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.DTGCHECK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GunaElipse1 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents GunaProgressBar1 As Guna.UI.WinForms.GunaProgressBar
    Friend WithEvents GunaAdvenceButton1 As Guna.UI.WinForms.GunaAdvenceButton
    Friend WithEvents GunaGroupBox2 As Guna.UI.WinForms.GunaGroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GunaAdvenceButton2 As Guna.UI.WinForms.GunaAdvenceButton
    Friend WithEvents DTGPLANS As Guna.UI.WinForms.GunaDataGridView
    Friend WithEvents GunaGroupBox1 As Guna.UI.WinForms.GunaGroupBox
    Friend WithEvents CKLIST As CheckedListBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents BATCHNUMBER As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents GunaTextBox1 As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents SITEID As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents CHECKID As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents DTGCHECK As Guna.UI.WinForms.GunaDataGridView
    Friend WithEvents SEARCH As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents SOPLANNUMBER As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents GunaLabel2 As Guna.UI.WinForms.GunaLabel
    Friend WithEvents DTDELIVERY As Guna.UI.WinForms.GunaDateTimePicker
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
End Class
