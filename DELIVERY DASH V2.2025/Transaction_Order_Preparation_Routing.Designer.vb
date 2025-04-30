<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Transaction_Order_Preparation_Routing
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DTGROUTES = New Guna.UI.WinForms.GunaDataGridView()
        Me.NUMOFCLUSTER = New Guna.UI2.WinForms.Guna2TextBox()
        Me.WebView21 = New Microsoft.Web.WebView2.WinForms.WebView2()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.DTGROUTES, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WebView21, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DTGROUTES
        '
        Me.DTGROUTES.AllowUserToAddRows = False
        Me.DTGROUTES.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.DTGROUTES.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DTGROUTES.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DTGROUTES.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DTGROUTES.BackgroundColor = System.Drawing.Color.White
        Me.DTGROUTES.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DTGROUTES.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DTGROUTES.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(34, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DTGROUTES.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DTGROUTES.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(211, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(107, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DTGROUTES.DefaultCellStyle = DataGridViewCellStyle6
        Me.DTGROUTES.EnableHeadersVisualStyles = False
        Me.DTGROUTES.GridColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(203, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.DTGROUTES.Location = New System.Drawing.Point(31, 90)
        Me.DTGROUTES.Name = "DTGROUTES"
        Me.DTGROUTES.ReadOnly = True
        Me.DTGROUTES.RowHeadersVisible = False
        Me.DTGROUTES.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DTGROUTES.Size = New System.Drawing.Size(370, 447)
        Me.DTGROUTES.TabIndex = 21
        Me.DTGROUTES.Theme = Guna.UI.WinForms.GunaDataGridViewPresetThemes.DeepOrange
        Me.DTGROUTES.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.DTGROUTES.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.DTGROUTES.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.DTGROUTES.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.DTGROUTES.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.DTGROUTES.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.DTGROUTES.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(203, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.DTGROUTES.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.DTGROUTES.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DTGROUTES.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.DTGROUTES.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White
        Me.DTGROUTES.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DTGROUTES.ThemeStyle.HeaderStyle.Height = 23
        Me.DTGROUTES.ThemeStyle.ReadOnly = True
        Me.DTGROUTES.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.DTGROUTES.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DTGROUTES.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.DTGROUTES.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black
        Me.DTGROUTES.ThemeStyle.RowsStyle.Height = 22
        Me.DTGROUTES.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(107, Byte), Integer))
        Me.DTGROUTES.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black
        '
        'NUMOFCLUSTER
        '
        Me.NUMOFCLUSTER.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.NUMOFCLUSTER.DefaultText = ""
        Me.NUMOFCLUSTER.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.NUMOFCLUSTER.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.NUMOFCLUSTER.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.NUMOFCLUSTER.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.NUMOFCLUSTER.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.NUMOFCLUSTER.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NUMOFCLUSTER.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.NUMOFCLUSTER.Location = New System.Drawing.Point(31, 33)
        Me.NUMOFCLUSTER.Name = "NUMOFCLUSTER"
        Me.NUMOFCLUSTER.PlaceholderText = ""
        Me.NUMOFCLUSTER.SelectedText = ""
        Me.NUMOFCLUSTER.Size = New System.Drawing.Size(159, 40)
        Me.NUMOFCLUSTER.TabIndex = 22
        '
        'WebView21
        '
        Me.WebView21.AllowExternalDrop = True
        Me.WebView21.CreationProperties = Nothing
        Me.WebView21.DefaultBackgroundColor = System.Drawing.Color.White
        Me.WebView21.Location = New System.Drawing.Point(529, 90)
        Me.WebView21.Name = "WebView21"
        Me.WebView21.Size = New System.Drawing.Size(616, 519)
        Me.WebView21.TabIndex = 23
        Me.WebView21.ZoomFactor = 1.0R
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(240, 41)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 24
        Me.Button1.Text = "GO"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Transaction_Order_Preparation_Routing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1192, 704)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.WebView21)
        Me.Controls.Add(Me.NUMOFCLUSTER)
        Me.Controls.Add(Me.DTGROUTES)
        Me.Name = "Transaction_Order_Preparation_Routing"
        Me.Text = "Transaction_Order_Preparation_Routing"
        CType(Me.DTGROUTES, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WebView21, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DTGROUTES As Guna.UI.WinForms.GunaDataGridView
    Friend WithEvents NUMOFCLUSTER As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents WebView21 As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents Button1 As Button
End Class
