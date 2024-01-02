Option Strict Off

Imports System.ComponentModel
Public Class Histogram

#Region "Events"

#End Region

#Region "Properties"
    Private _lookBack As Integer = 60
    Private _seriesCount As Integer = 0

    <Browsable(True), Description("Poll Rate")> Dim _pollRate As Integer = 1000
    <Browsable(True), Description("Include Filter")> Dim _includeFilter As String
    <Browsable(True), Description("Exclude Filter")> Dim _excludeFilter As String
    Private _symbolList As New List(Of SeriesSelection)
#End Region

#Region "Property Functions"
    Public Property LookBack As Integer
        Get
            LookBack = _lookBack
        End Get
        Set(value As Integer)
            _lookBack = value
        End Set
    End Property

    Public Property PollRate() As Integer
        Get
            PollRate = _pollRate
        End Get
        Set(ByVal Value As Integer)
            If _pollRate <> Value Then
                If Value <= 0 Then
                    TimerPoll.Stop()
                    _pollRate = 0
                ElseIf Value > 0 Then
                    TimerPoll.Start()
                    TimerPoll.Interval = Value
                    _pollRate = Value
                Else
                End If
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property ExcludeFilter As String
        Get
            ExcludeFilter = _excludeFilter
        End Get
        Set(Value As String)
            _excludeFilter = Value
        End Set
    End Property

    Public Property IncludeFilter As String
        Get
            IncludeFilter = _includeFilter
        End Get
        Set(Value As String)
            _includeFilter = Value
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property SymbolList As List(Of SeriesSelection)
        Set(ByVal value As List(Of SeriesSelection))
            _symbolList = value
        End Set
        Get
            Return _symbolList
        End Get
    End Property
#End Region

#Region "Subs"
    Public Sub ResetChart()
        Chart1.Series.Clear()
        _seriesCount = 0
        If SymbolList.Count > 0 Then
            For Each series As SeriesSelection In SymbolList
                Dim seriesName As String
                If series.Unit <> "" Then
                    seriesName = series.Name & " / " & series.Unit
                Else
                    seriesName = series.Name
                End If
                Chart1.Series.Add(seriesName)
                Chart1.Series(seriesName).ChartType = DataVisualization.Charting.SeriesChartType.Line
                Chart1.Series(seriesName).XValueType = DataVisualization.Charting.ChartValueType.Date
            Next
        End If
    End Sub

    Public Sub ApplyLookBack()
        Chart1.ChartAreas(0).AxisX.Minimum = Now.AddSeconds(LookBack * -1).ToOADate
        Chart1.ChartAreas(0).AxisX.Maximum = Now.ToOADate
    End Sub

    Private Sub Zoom(ByVal factor As Integer)
        Select Case factor
            Case -2
                Me.Chart1.ChartAreas(0).AxisY.Minimum = Me.NumericUpDown1.Value
                Me.Chart1.ChartAreas(0).AxisY.Maximum = Me.NumericUpDown2.Value
            Case -1
                Me.Chart1.ChartAreas(0).AxisY.Minimum = 0
                Me.Chart1.ChartAreas(0).AxisY.Maximum = 100
            Case Else
                Dim average As Double
                For i = Math.Max(0, Chart1.Series(0).Points.Count - 50) To Chart1.Series(0).Points.Count - 1
                    average = average + Chart1.Series(0).Points(i).YValues(0)
                Next
                If Chart1.Series(0).Points.Count = 0 Then
                    average = 0
                Else
                    average = average / Chart1.Series(0).Points.Count
                End If

                Me.Chart1.ChartAreas(0).AxisY.Minimum = Math.Floor(average - factor)
                Me.Chart1.ChartAreas(0).AxisY.Maximum = Math.Ceiling(average + factor)
        End Select

        Me.NumericUpDown1.Value = CDec(Me.Chart1.ChartAreas(0).AxisY.Minimum)
        Me.NumericUpDown2.Value = CDec(Me.Chart1.ChartAreas(0).AxisY.Maximum)
    End Sub
#End Region

#Region "Form Events"
    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        LookBack = 10
        ApplyLookBack()
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        LookBack = 60
        ApplyLookBack()
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        LookBack = 180
        ApplyLookBack()
    End Sub

    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        LookBack = 900
        ApplyLookBack()
    End Sub

    Private Sub UmMittelwertToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UmMittelwertToolStripMenuItem.Click
        Zoom(1)
    End Sub

    Private Sub UmMittelwertToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles UmMittelwertToolStripMenuItem1.Click
        Zoom(5)
    End Sub

    Private Sub ToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem6.Click
        Zoom(-1)
    End Sub

    Private Sub AutoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AutoToolStripMenuItem.Click
        Zoom(-2)
    End Sub

    Private Sub AnzeigenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnzeigenToolStripMenuItem.Click
        Chart1.Legends(0).Enabled = True
    End Sub

    Private Sub AusblendenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AusblendenToolStripMenuItem.Click
        Chart1.Legends(0).Enabled = False
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        Try
            If Me.NumericUpDown1.Value < Me.NumericUpDown2.Value Then
                Me.Chart1.ChartAreas(0).AxisY.Minimum = Me.NumericUpDown1.Value
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub NumericUpDown2_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown2.ValueChanged
        Try
            If Me.NumericUpDown2.Value > Me.NumericUpDown1.Value Then
                Me.Chart1.ChartAreas(0).AxisY.Maximum = Me.NumericUpDown2.Value
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TimerPoll_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        If SymbolList.Count > 0 Then
            For Each series As SeriesSelection In SymbolList
                Dim seriesName As String
                If series.Unit <> "" Then
                    seriesName = series.Name & " / " & series.Unit
                Else
                    seriesName = series.Name
                End If
                If Chart1.Series.IndexOf(seriesName) <> -1 Then
                    Chart1.Series(seriesName).Points.AddXY(Now.ToOADate, ADS.getSymbolValueCached(series.Symbol, PollRate))

                End If
            Next
            ApplyLookBack()
        End If
    End Sub

    Private Sub Histogramm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.SplitContainer1.SplitterDistance = 50
    End Sub

    Private Sub Histogramm_Load(sender As Object, e As EventArgs) Handles Me.Load
        If PollRate > 0 And DesignMode = False Then
            TimerPoll.Interval = PollRate
            TimerPoll.Start()
        End If
        If SymbolList.Count > 0 Then
            ResetChart()
        End If
    End Sub

    Private Sub SymboleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SymboleToolStripMenuItem.Click
        SymbolSelect.Include = IncludeFilter
        SymbolSelect.Exclude = ExcludeFilter
        SymbolSelect.SelectedSymbols = New List(Of SeriesSelection)
        For Each series As SeriesSelection In SymbolList
            SymbolSelect.SelectedSymbols.Add(New SeriesSelection(series.Name, series.Symbol, series.Unit))
        Next
        SymbolSelect.ShowDialog()

        SymbolList.Clear()
        For Each symbolSelected In SymbolSelect.SelectedSymbols
            Dim series As SeriesSelection = New SeriesSelection(symbolSelected.Name, symbolSelected.Symbol, symbolSelected.Unit)
            SymbolList.Add(series)
        Next

        ResetChart()
    End Sub
#End Region

End Class
