<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Diagram
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim DataPoint1 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(30, 30)
        Dim DataPoint2 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(100, 50)
        Dim DataPoint3 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(50, 70)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TimerPoll = New System.Windows.Forms.Timer(Me.components)
        Me.TimerPoll.Interval = 1000
        Me.TimerPoll.Start()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

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
        Me.SplitContainer1.Panel2.Controls.Add(Me.Chart1)
        Me.SplitContainer1.Panel2MinSize = 100
        Me.SplitContainer1.Size = New System.Drawing.Size(1000, 600)
        Me.SplitContainer1.SplitterDistance = 0
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer1.Panel2
        'stillEmpty
        '

        '
        'Chart1
        '
        Me.Chart1.BackColor = System.Drawing.Color.Transparent
        Me.Chart1.BorderSkin.BorderColor = System.Drawing.Color.Silver
        ChartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.[True]
        ChartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount
        ChartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.WhiteSmoke
        ChartArea1.AxisX.ScaleBreakStyle.BreakLineStyle = System.Windows.Forms.DataVisualization.Charting.BreakLineStyle.None
        ChartArea1.AxisX.Title = "XAchse"
        ChartArea1.AxisX.Minimum = 0
        ChartArea1.AxisX.Maximum = 100
        'ChartArea1.AxisX.Interval = 10
        ChartArea1.AxisY.Minimum = 0
        ChartArea1.AxisY.Maximum = 100
        ChartArea1.AxisY.IntervalAutoMode = 1
        'ChartArea1.AxisY.Interval = 10
        ChartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray
        ChartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray
        'ChartArea1.AxisX2.LabelStyle.ForeColor = System.Drawing.Color.WhiteSmoke
        'ChartArea1.AxisX2.LineColor = System.Drawing.Color.WhiteSmoke
        'ChartArea1.AxisX2.TitleForeColor = System.Drawing.Color.WhiteSmoke
        ChartArea1.AxisY.Title = "YAchse"
        ChartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.WhiteSmoke
        ChartArea1.BackColor = System.Drawing.Color.White
        ChartArea1.Name = "ChartArea1"
        ChartArea1.ShadowColor = System.Drawing.Color.Black
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Me.Chart1.Dock = System.Windows.Forms.DockStyle.Fill
        Legend1.BackColor = System.Drawing.Color.White
        Legend1.MaximumAutoSize = 100.0!
        Legend1.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(0, 0)
        Me.Chart1.Name = "Chart1"
        Me.Chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None
        Me.Chart1.PaletteCustomColors = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(7, Byte), Integer), CType(CType(30, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(84, Byte), Integer), CType(CType(159, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(87, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(39, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(161, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(0, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(172, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(189, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(0, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(101, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(161, Byte), Integer), CType(CType(16, Byte), Integer), CType(CType(53, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(88, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(229, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(79, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(80, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(160, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(198, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(0, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(102, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(39, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(176, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(70, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(40, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(152, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(155, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(30, Byte), Integer))}
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point
        Series1.IsVisibleInLegend = False
        Series1.MarkerColor = Color.Transparent
        Series1.MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Series1.Points.Add(DataPoint1)
        Series1.MarkerSize = 10
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Size = New System.Drawing.Size(1000, 600)
        Me.Chart1.SuppressExceptions = True
        Me.Chart1.TabIndex = 1
        Me.Chart1.Text = "Chart1"
        Me.Chart1.Margin = New Padding(0)
        Me.Chart1.ChartAreas(0).BackImageWrapMode = 4

        'Me.Chart1.ChartAreas(0).InnerPlotPosition.Width = 600
        'Me.Chart1.ChartAreas(0).InnerPlotPosition.Height = 250


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
        Me.Name = "Diagram"
        Me.Size = New System.Drawing.Size(1000, 600)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents NumericUpDown2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents SymboleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Chart1 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents TimerPoll As Timer


End Class
