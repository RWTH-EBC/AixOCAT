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
    <Browsable(True), Description("Y-Axis max")> Dim _yAxis_Interval As Decimal = 10
    <Browsable(True), Description("Y-Axis logaritmic")> Dim _yAxis_log As Boolean = 0
    <Browsable(True), Description("X-Axis Name")> Dim _xAxis_Name As String = "X-Axis"
    <Browsable(True), Description("Y-Axis Name")> Dim _yAxis_Name As String = "Y-Axis"
    <Browsable(True), Description("GridColor")> Dim _gridColor As System.Drawing.Color = System.Drawing.Color.LightGray
    <Browsable(True), Description("BackImage")> Dim _backImage As String

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

    Public Property BackImage() As String
        Get
            BackImage = _backImage
        End Get
        Set(ByVal value As String)
            If _backImage <> value Then
                _backImage = value
                AxisAdjust()
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
        If _xAxis_log = True Then
            Chart1.ChartAreas(0).AxisX.IsLogarithmic = True
        Else Chart1.ChartAreas(0).AxisX.IsLogarithmic = False
        End If
        If _yAxis_log = True Then
            Chart1.ChartAreas(0).AxisY.IsLogarithmic = True
        Else Chart1.ChartAreas(0).AxisY.IsLogarithmic = False
        End If
        Chart1.ChartAreas(0).AxisX.Minimum = _xAxis_min
        Chart1.ChartAreas(0).AxisX.Maximum = _xAxis_max
        Chart1.ChartAreas(0).AxisY.Minimum = _yAxis_min
        Chart1.ChartAreas(0).AxisY.Maximum = _yAxis_max
        'Chart1.ChartAreas(0).AxisX.Interval = _xAxis_Interval
        'Chart1.ChartAreas(0).AxisY.Interval = _yAxis_Interval
        Chart1.ChartAreas(0).AxisX.Title = _xAxis_Name
        Chart1.ChartAreas(0).AxisY.Title = _yAxis_Name
        Chart1.ChartAreas(0).AxisX.MajorGrid.LineColor = _gridColor
        Chart1.ChartAreas(0).AxisY.MajorGrid.LineColor = _gridColor
        If Not _backImage = "" Then
            Chart1.ChartAreas(0).BackImage = _backImage
        End If
    End Sub


#End Region


#Region "Form Events"

#End Region

End Class
