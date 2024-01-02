<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Histogram
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim DataPoint1 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(44106.55972222222R, 2.0R)
        Dim DataPoint2 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(44106.559895833336R, 5.0R)
        Dim DataPoint3 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(44106.560069444444R, 0R)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RückblickToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZoomToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UmMittelwertToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UmMittelwertToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AutoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LegendeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AnzeigenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AusblendenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SymboleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TimerPoll = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RückblickToolStripMenuItem, Me.ZoomToolStripMenuItem, Me.LegendeToolStripMenuItem, Me.SymboleToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(172, 92)
        '
        'RückblickToolStripMenuItem
        '
        Me.RückblickToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem2, Me.ToolStripMenuItem3, Me.ToolStripMenuItem4, Me.ToolStripMenuItem5})
        Me.RückblickToolStripMenuItem.Name = "RückblickToolStripMenuItem"
        Me.RückblickToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.RückblickToolStripMenuItem.Text = "Rückblick"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(92, 22)
        Me.ToolStripMenuItem2.Text = "10"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(92, 22)
        Me.ToolStripMenuItem3.Text = "60"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(92, 22)
        Me.ToolStripMenuItem4.Text = "180"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(92, 22)
        Me.ToolStripMenuItem5.Text = "900"
        '
        'ZoomToolStripMenuItem
        '
        Me.ZoomToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UmMittelwertToolStripMenuItem, Me.UmMittelwertToolStripMenuItem1, Me.ToolStripMenuItem6, Me.AutoToolStripMenuItem})
        Me.ZoomToolStripMenuItem.Name = "ZoomToolStripMenuItem"
        Me.ZoomToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.ZoomToolStripMenuItem.Text = "Zoom"
        '
        'UmMittelwertToolStripMenuItem
        '
        Me.UmMittelwertToolStripMenuItem.Name = "UmMittelwertToolStripMenuItem"
        Me.UmMittelwertToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.UmMittelwertToolStripMenuItem.Text = "+/- 1 um Mittelwert"
        '
        'UmMittelwertToolStripMenuItem1
        '
        Me.UmMittelwertToolStripMenuItem1.Name = "UmMittelwertToolStripMenuItem1"
        Me.UmMittelwertToolStripMenuItem1.Size = New System.Drawing.Size(179, 22)
        Me.UmMittelwertToolStripMenuItem1.Text = "+/- 5 um Mittelwert"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(179, 22)
        Me.ToolStripMenuItem6.Text = "0 - 100"
        '
        'AutoToolStripMenuItem
        '
        Me.AutoToolStripMenuItem.Name = "AutoToolStripMenuItem"
        Me.AutoToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.AutoToolStripMenuItem.Text = "manuell"
        Me.AutoToolStripMenuItem.Visible = False
        '
        'LegendeToolStripMenuItem
        '
        Me.LegendeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AnzeigenToolStripMenuItem, Me.AusblendenToolStripMenuItem})
        Me.LegendeToolStripMenuItem.Name = "LegendeToolStripMenuItem"
        Me.LegendeToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.LegendeToolStripMenuItem.Text = "Legende"
        '
        'AnzeigenToolStripMenuItem
        '
        Me.AnzeigenToolStripMenuItem.Name = "AnzeigenToolStripMenuItem"
        Me.AnzeigenToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.AnzeigenToolStripMenuItem.Text = "Anzeigen"
        '
        'AusblendenToolStripMenuItem
        '
        Me.AusblendenToolStripMenuItem.Name = "AusblendenToolStripMenuItem"
        Me.AusblendenToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.AusblendenToolStripMenuItem.Text = "Ausblenden"
        '
        'SymboleToolStripMenuItem
        '
        Me.SymboleToolStripMenuItem.Name = "SymboleToolStripMenuItem"
        Me.SymboleToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.SymboleToolStripMenuItem.Text = "Symbole zuweisen"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.BackColor = System.Drawing.SystemColors.Control
        Me.NumericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.NumericUpDown1.DecimalPlaces = 1
        Me.NumericUpDown1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.NumericUpDown1.ForeColor = System.Drawing.Color.Black
        Me.NumericUpDown1.Location = New System.Drawing.Point(0, 284)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {9999, 0, 0, -2147483648})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(71, 16)
        Me.NumericUpDown1.TabIndex = 2
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.SplitContainer1.Panel1.Controls.Add(Me.NumericUpDown2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.NumericUpDown1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Chart1)
        Me.SplitContainer1.Panel2MinSize = 550
        Me.SplitContainer1.Size = New System.Drawing.Size(660, 300)
        Me.SplitContainer1.SplitterDistance = 71
        Me.SplitContainer1.TabIndex = 3
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.BackColor = System.Drawing.SystemColors.Control
        Me.NumericUpDown2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.NumericUpDown2.DecimalPlaces = 1
        Me.NumericUpDown2.Dock = System.Windows.Forms.DockStyle.Top
        Me.NumericUpDown2.ForeColor = System.Drawing.Color.Black
        Me.NumericUpDown2.Location = New System.Drawing.Point(0, 0)
        Me.NumericUpDown2.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.NumericUpDown2.Minimum = New Decimal(New Integer() {9999, 0, 0, -2147483648})
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(71, 16)
        Me.NumericUpDown2.TabIndex = 2
        Me.NumericUpDown2.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Chart1
        '
        Me.Chart1.BackColor = System.Drawing.Color.Transparent
        Me.Chart1.BorderSkin.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Chart1.BorderSkin.BorderColor = System.Drawing.Color.Silver
        ChartArea1.AxisX.Crossing = -1.7976931348623157E+308R
        ChartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.[True]
        ChartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount
        ChartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Minutes
        ChartArea1.AxisX.LabelStyle.Format = "HH:mm:ss"
        ChartArea1.AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.[Auto]
        ChartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.WhiteSmoke
        ChartArea1.AxisX.ScaleBreakStyle.BreakLineStyle = System.Windows.Forms.DataVisualization.Charting.BreakLineStyle.None
        ChartArea1.AxisX.Title = "Zeit"
        ChartArea1.AxisX2.LabelStyle.ForeColor = System.Drawing.Color.WhiteSmoke
        ChartArea1.AxisX2.LineColor = System.Drawing.Color.WhiteSmoke
        ChartArea1.AxisX2.TitleForeColor = System.Drawing.Color.WhiteSmoke
        ChartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.WhiteSmoke
        ChartArea1.AxisY2.LabelStyle.ForeColor = System.Drawing.Color.WhiteSmoke
        ChartArea1.AxisY2.LineColor = System.Drawing.Color.WhiteSmoke
        ChartArea1.AxisY2.TitleForeColor = System.Drawing.Color.WhiteSmoke
        ChartArea1.BackColor = System.Drawing.Color.White
        ChartArea1.Name = "ChartArea1"
        ChartArea1.ShadowColor = System.Drawing.Color.Black
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Me.Chart1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Chart1.Dock = System.Windows.Forms.DockStyle.Fill
        Legend1.BackColor = System.Drawing.Color.Transparent
        Legend1.MaximumAutoSize = 100.0!
        Legend1.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(0, 0)
        Me.Chart1.Name = "Chart1"
        Me.Chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None
        Me.Chart1.PaletteCustomColors = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(7, Byte), Integer), CType(CType(30, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(84, Byte), Integer), CType(CType(159, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(87, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(39, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(161, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(0, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(172, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(189, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(0, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(101, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(161, Byte), Integer), CType(CType(16, Byte), Integer), CType(CType(53, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(88, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(229, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(79, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(80, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(160, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(198, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(0, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(102, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(39, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(176, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(70, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(40, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(152, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(155, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(30, Byte), Integer))}
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Series1.Points.Add(DataPoint1)
        Series1.Points.Add(DataPoint2)
        Series1.Points.Add(DataPoint3)
        Series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Size = New System.Drawing.Size(585, 300)
        Me.Chart1.SuppressExceptions = True
        Me.Chart1.TabIndex = 1
        Me.Chart1.Text = "Chart1"
        '
        'TimerPoll
        '
        '
        'Histogram
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.SplitContainer1)
        Me.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.Name = "Histogram"
        Me.Size = New System.Drawing.Size(660, 300)
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RückblickToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ZoomToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UmMittelwertToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UmMittelwertToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LegendeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AnzeigenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AusblendenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents NumericUpDown2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents SymboleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Chart1 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents TimerPoll As Timer
End Class
