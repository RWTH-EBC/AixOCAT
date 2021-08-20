Option Strict Off

Imports System.ComponentModel
Public Class Diagram

#Region "Events"

#End Region

#Region "Properties"

    <Browsable(True), Description("Poll Rate")> Dim _pollRate As Integer = 1000
    <Browsable(True), Description("X-Axis min")> Dim _xAxis_min As Decimal = 0
    <Browsable(True), Description("X-Axis max")> Dim _xAxis_max As Decimal = 100
    <Browsable(True), Description("X-Axis logaritmic")> Dim _xAxis_log As Boolean = 0
    <Browsable(True), Description("Y-Axis min")> Dim _yAxis_min As Decimal = 0
    <Browsable(True), Description("Y-Axis max")> Dim _yAxis_max As Decimal = 100
    <Browsable(True), Description("X-Axis Interval")> Dim _xAxis_Interval As Decimal = 10
    <Browsable(True), Description("X-Axis Interval Offset")> Dim _xAxis_Offset As Decimal = 0
    <Browsable(True), Description("Y-Axis Interval")> Dim _yAxis_Interval As Decimal = 10
    <Browsable(True), Description("Y-Axis Interval Offset")> Dim _yAxis_Offset As Decimal = 0
    <Browsable(True), Description("Y-Axis logaritmic")> Dim _yAxis_log As Boolean = 0
    <Browsable(True), Description("X-Axis Name")> Dim _xAxis_Name As String = "X-Axis"
    <Browsable(True), Description("Y-Axis Name")> Dim _yAxis_Name As String = "Y-Axis"
    <Browsable(True), Description("GridColor")> Dim _gridColor As System.Drawing.Color = System.Drawing.Color.LightGray
    <Browsable(True), Description("BackImage")> Dim _backImage As System.Drawing.Image

    Private _symbolList As New List(Of DiagramSeriesCollection)
#End Region

#Region "Property Functions"
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

    Public Property XAxis_Name() As String
        Get
            XAxis_Name = _xAxis_Name
        End Get
        Set(ByVal value As String)
            If _xAxis_Name <> value Then
                _xAxis_Name = value
                AxisAdjust()
            End If
        End Set
    End Property
    Public Property XAxis_min() As Decimal
        Get
            XAxis_min = _xAxis_min
        End Get
        Set(ByVal value As Decimal)
            If _xAxis_min <> value Then
                _xAxis_min = value
                AxisAdjust()
            End If
        End Set
    End Property

    Public Property XAxis_max() As Decimal
        Get
            XAxis_max = _xAxis_max
        End Get
        Set(ByVal value As Decimal)
            If _xAxis_max <> value Then
                _xAxis_max = value
                AxisAdjust()
            End If
        End Set
    End Property
    Public Property XAxis_log() As Boolean
        Get
            XAxis_log = _xAxis_log
        End Get
        Set(ByVal value As Boolean)
            If _xAxis_log <> value Then
                _xAxis_log = value
                If _xAxis_log = True And _xAxis_min = 0 Then
                    _xAxis_min = 0.01
                End If
                AxisAdjust()
            End If
        End Set
    End Property

    Public Property XAxis_Interval() As Decimal
        Get
            XAxis_Interval = _xAxis_Interval
        End Get
        Set(ByVal value As Decimal)
            If _xAxis_Interval <> value Then
                _xAxis_Interval = value
                AxisAdjust()
            End If
        End Set
    End Property

    Public Property XAxis_Offset() As Decimal
        Get
            XAxis_Offset = _xAxis_Offset
        End Get
        Set(ByVal value As Decimal)
            If _xAxis_Offset <> value Then
                _xAxis_Offset = value
                AxisAdjust()
            End If
        End Set
    End Property

    Public Property YAxis_min() As Decimal
        Get
            YAxis_min = _yAxis_min
        End Get
        Set(ByVal value As Decimal)
            If _yAxis_min <> value Then
                _yAxis_min = value
                AxisAdjust()
            End If
        End Set
    End Property

    Public Property YAxis_max() As Decimal
        Get
            YAxis_max = _yAxis_max
        End Get
        Set(ByVal value As Decimal)
            If _yAxis_max <> value Then
                _yAxis_max = value
                AxisAdjust()
            End If
        End Set
    End Property
    Public Property YAxis_log() As Boolean
        Get
            YAxis_log = _yAxis_log
        End Get
        Set(ByVal value As Boolean)
            If _yAxis_log <> value Then
                _yAxis_log = value
                If _yAxis_log = True And _yAxis_min = 0 Then
                    _yAxis_min = 0.01
                End If
                AxisAdjust()
            End If
        End Set
    End Property
    Public Property YAxis_Interval() As Decimal
        Get
            YAxis_Interval = _yAxis_Interval
        End Get
        Set(ByVal value As Decimal)
            If _yAxis_Interval <> value Then
                _yAxis_Interval = value
                AxisAdjust()
            End If
        End Set
    End Property
    Public Property YAxis_Offset() As Decimal
        Get
            YAxis_Offset = _yAxis_Offset
        End Get
        Set(ByVal value As Decimal)
            If _yAxis_Offset <> value Then
                _yAxis_Offset = value
                AxisAdjust()
            End If
        End Set
    End Property
    Public Property YAxis_Name() As String
        Get
            YAxis_Name = _yAxis_Name
        End Get
        Set(ByVal value As String)
            If _yAxis_Name <> value Then
                _yAxis_Name = value
                AxisAdjust()
            End If
        End Set
    End Property

    Public Property GridColor() As System.Drawing.Color
        Get
            GridColor = _gridColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            If _gridColor <> value Then
                _gridColor = value
                AxisAdjust()
            End If
        End Set
    End Property

    Public Property BackImage() As System.Drawing.Image
        Get
            BackImage = _backImage
        End Get
        Set(ByVal value As System.Drawing.Image)
            Dim x As Integer
            If IsNothing(_backImage) Then
                x = 0
            Else x = _backImage.Width
            End If

            If x <> value.Width Then
                _backImage = value
                BackgroundPic()
            End If

        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property SymbolList As List(Of DiagramSeriesCollection)
        Set(ByVal value As List(Of DiagramSeriesCollection))
            _symbolList = value
        End Set
        Get
            Return _symbolList
        End Get
    End Property
#End Region

#Region "Subs"
    Private Sub AxisAdjust()
        Chart1.ChartAreas(0).AxisX.IntervalAutoMode = DataVisualization.Charting.IntervalAutoMode.VariableCount
        Chart1.ChartAreas(0).AxisY.IntervalAutoMode = DataVisualization.Charting.IntervalAutoMode.VariableCount
        Chart1.ChartAreas(0).AxisX.Minimum = _xAxis_min
        Chart1.ChartAreas(0).AxisX.Maximum = _xAxis_max
        Chart1.ChartAreas(0).AxisY.Minimum = _yAxis_min
        Chart1.ChartAreas(0).AxisY.Maximum = _yAxis_max
        Chart1.ChartAreas(0).AxisX.Title = _xAxis_Name
        Chart1.ChartAreas(0).AxisY.Title = _yAxis_Name
        Chart1.ChartAreas(0).AxisX.MajorGrid.LineColor = _gridColor
        Chart1.ChartAreas(0).AxisY.MajorGrid.LineColor = _gridColor
        Chart1.ChartAreas(0).AxisY.Interval = _yAxis_Interval
        Chart1.ChartAreas(0).AxisX.Interval = _xAxis_Interval
        Chart1.ChartAreas(0).AxisY.IntervalOffset = _yAxis_Offset
        Chart1.ChartAreas(0).AxisX.IntervalOffset = _xAxis_Offset
        If _xAxis_log = True Then
            Chart1.ChartAreas(0).AxisX.IsLogarithmic = True
            Chart1.ChartAreas(0).AxisX.Interval = Math.Log10(_xAxis_Interval)
        Else Chart1.ChartAreas(0).AxisX.IsLogarithmic = False
        End If
        If _yAxis_log = True Then
            Chart1.ChartAreas(0).AxisY.IsLogarithmic = True
            Chart1.ChartAreas(0).AxisY.Interval = Math.Log10(_yAxis_Interval)
        Else Chart1.ChartAreas(0).AxisY.IsLogarithmic = False
        End If
        Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "0.00"
        Chart1.ChartAreas(0).AxisY.LabelStyle.Format = "0.00"


    End Sub

    Private Sub BackgroundPic()
        Chart1.Images.Clear()
        Chart1.Images.Add(New DataVisualization.Charting.NamedImage("Background", _backImage))
        Chart1.ChartAreas(0).BackImage = Chart1.Images(0).Name
    End Sub

    Private Sub TimerPoll_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        Dim len As Integer
        len = Chart1.Series.Count()
        If len > 1 Then             'Delete Series
            Dim i As Integer
            i = len - 1
            While len > 1
                Chart1.Series.RemoveAt(i)
                i = i - 1
                len = Chart1.Series.Count()
            End While
        End If
        If SymbolList.Count > 0 Then
            For Each dPoint As DiagramSeriesCollection In SymbolList 'delete DataPoints from Series
                Dim seriesName As String
                seriesName = dPoint.Name
                If seriesName <> "" Then
                    If Chart1.Series.IsUniqueName(seriesName) = False Then
                        Chart1.Series(seriesName).Points.Clear()
                    End If
                End If
            Next

            For Each dataPoint As DiagramSeriesCollection In SymbolList
                Dim seriesName As String
                seriesName = dataPoint.Name
                If dataPoint.Name <> "" Then
                    If Chart1.Series.IsUniqueName(seriesName) = True Then
                        Chart1.Series.Add(seriesName)
                    End If
                    Chart1.Series(seriesName).ChartType = DataVisualization.Charting.SeriesChartType.Point
                    Chart1.Series(seriesName).MarkerStyle = dataPoint.MarkerStyle
                    Chart1.Series(seriesName).MarkerColor = dataPoint.MarkerColor
                    Chart1.Series(seriesName).MarkerSize = dataPoint.MarkerSize
                    'Chart1.Series(seriesName).ChartType = dataPoint.ConnectionType
                    'Chart1.Series(seriesName).Points.AddXY(dataPoint.xValue_Symbol, dataPoint.yValue_Symbol)
                    Chart1.Series(seriesName).Points.AddXY(ADS.getSymbolValueCached(dataPoint.xValue_Symbol, PollRate), ADS.getSymbolValueCached(dataPoint.yValue_Symbol, PollRate))
                End If
            Next
        End If

    End Sub


#End Region


#Region "Form Events"

#End Region

End Class
