Imports System.ComponentModel

Public Class ConvergenceWatchdog

#Region "Events"
    Public Event UnitChanged(ByVal NewStatus As String)
    Public Event ConvergenceChanged(ByVal NewStatus As Boolean)
#End Region

#Region "Properties"
    <Browsable(True), Description("Average")> Dim _average As Decimal
    <Browsable(True), Description("Averaging time")> Dim _averageTime As Integer = 10000
    <Browsable(True), Description("Convergence")> Dim _convergence As Boolean
    <Browsable(True), Description("Convergence Threshold")> Dim _convergenceThreshold As Decimal = 0.05
    <Browsable(True), Description("Deviation")> Dim _deviation As Decimal
    <Browsable(True), Description("Hint")> Dim _hint As String
    <Browsable(True), Description("Poll rate")> Dim _pollRate As Integer = 100
    <Browsable(True), Description("Relative mode")> Dim _relative As Boolean = True
    <Browsable(True), Description("Statistic time")> Dim _statisticTime As Integer = 1000
    <Browsable(True), Description("Symbolname")> Dim _symbol As String
    <Browsable(True), Description("Symbolname of Target")> Dim _symbolTarget As String
    <Browsable(True), Description("Display unit")> Dim _unit As String

    Dim _buffer As Buffer
    Dim _bufferDelta As Buffer
    Dim _lastTime As DateTime
    Dim _adjustedInterval As Integer
    Dim _elapsed As Integer
#End Region

#Region "Property Functions"
    Public Property Average() As Decimal
        Get
            Average = _average
        End Get
        Set(ByVal Value As Decimal)
            If _average <> Value Then
                _average = Value
                IconChange()
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property AverageTime() As Integer
        Get
            AverageTime = _averageTime
        End Get
        Set(ByVal Value As Integer)
            If _averageTime <> Value Then
                _averageTime = Value
                InitializeBuffer()
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property Convergence() As Boolean
        Get
            Convergence = _convergence
        End Get
        Set(ByVal Value As Boolean)
            If _convergence <> Value Then
                _convergence = Value
                IconChange()
                RaiseEvent ConvergenceChanged(_convergence)
                If _convergence Then
                    Log.AddMessage(MessageType.Information, Symbol & " is stationary")
                Else
                    Log.AddMessage(MessageType.Information, Symbol & " is not stable")
                End If
                Me.Invalidate()
                End If
        End Set
    End Property

    Public Property ConvergenceThreshold() As Decimal
        Get
            ConvergenceThreshold = _convergenceThreshold
        End Get
        Set(ByVal Value As Decimal)
            If _convergenceThreshold <> Value Then
                _convergenceThreshold = Value
                IconChange()
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property Deviation() As Decimal
        Get
            Deviation = _deviation
        End Get
        Set(ByVal Value As Decimal)
            If _deviation <> Value Then
                _deviation = Value
                Convergence = (_deviation < ConvergenceThreshold)
                IconChange()
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property Hint() As String
        Get
            Hint = _hint
        End Get
        Set(ByVal Value As String)
            If _hint <> Value Then
                _hint = Value
                Me.Invalidate()
            End If
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
                Else
                    TimerPoll.Start()
                    TimerPoll.Interval = Value
                    _pollRate = Value
                End If
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property Relative() As Boolean
        Get
            Relative = _relative
        End Get
        Set(ByVal Value As Boolean)
            If _relative <> Value Then
                _relative = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property StatisticTime() As Integer
        Get
            StatisticTime = _statisticTime
        End Get
        Set(ByVal Value As Integer)
            If _statisticTime <> Value Then
                _statisticTime = Value
                InitializeBuffer()
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property Symbol() As String
        Get
            Symbol = _symbol
        End Get
        Set(ByVal Value As String)
            If _symbol <> Value Then
                _symbol = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property SymbolTarget() As String
        Get
            SymbolTarget = _symbolTarget
        End Get
        Set(ByVal Value As String)
            If _symbolTarget <> Value Then
                _symbolTarget = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property Unit() As String
        Get
            Unit = _unit
        End Get
        Set(ByVal Value As String)
            If _unit <> Value Then
                _unit = Value
                RaiseEvent UnitChanged(_unit)
                Me.Invalidate()
            End If
        End Set
    End Property
#End Region

#Region "Subs"
    Private Sub IconChange()
        Dim deviation As Decimal
        Dim unit As String
        If Relative Then
            deviation = Me.Deviation * 100
            unit = "%"
        Else
            deviation = Me.Deviation
            unit = Me.Unit
        End If
        If Average >= 1000 Then
            Me.lblValue.Text = Average.ToString("F0") & " " & Me.Unit
        ElseIf Average >= 100 Then
            Me.lblValue.Text = Average.ToString("F1") & " " & Me.Unit
        Else
            Me.lblValue.Text = Average.ToString("F") & " " & Me.Unit
        End If
        If deviation >= 100 Then
            Me.lblDeviation.Text = "+/- " & deviation.ToString("F0") & " " & unit
        ElseIf deviation >= 10 Then
            Me.lblDeviation.Text = "+/- " & deviation.ToString("F1") & " " & unit
        Else
            Me.lblDeviation.Text = "+/- " & deviation.ToString("F2") & " " & unit
        End If
        If Convergence Then
            Me.lblDeviation.BackColor = System.Drawing.Color.FromArgb(87, 171, 39)
            Me.ToolTipInfo.SetToolTip(Me.lblDeviation, "stationary")
        Else
            Me.lblDeviation.BackColor = System.Drawing.Color.FromArgb(204, 7, 30)
            Me.ToolTipInfo.SetToolTip(Me.lblDeviation, "not stationary")
        End If
    End Sub

    Private Sub InitializeBuffer()
        If _averageTime > 0 And _averageTime >= PollRate Then
            If _statisticTime > 0 And _statisticTime >= PollRate Then
                _buffer = New Buffer(CInt(Math.Ceiling(_averageTime / PollRate)), CInt(Math.Ceiling(_statisticTime / PollRate)))
                _bufferDelta = New Buffer(CInt(Math.Ceiling(_averageTime / PollRate)), CInt(Math.Ceiling(_statisticTime / PollRate)))
            Else
                _buffer = New Buffer(CInt(Math.Ceiling(_averageTime / PollRate)))
                _bufferDelta = New Buffer(CInt(Math.Ceiling(_averageTime / PollRate)))
            End If
        Else
            _buffer = New Buffer(10, 5)
            _bufferDelta = New Buffer(10, 5)
        End If
    End Sub
#End Region

#Region "Form Events"
    Private Sub ConvergenceWatchdog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If PollRate > 0 And DesignMode = False Then
            TimerPoll.Start()
        End If
        Me.ToolTipInfo.SetToolTip(Me.lblValue, "Symbol: " & Symbol & ControlChars.NewLine & "Hint: " & Hint)
        InitializeBuffer()
        IconChange()
        _lastTime = DateTime.UtcNow
    End Sub

    Private Sub TimerPoll_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        If DesignMode = False And ADS.Connected And Not _buffer Is Nothing And Not ADS.CachedOnly Then
            _buffer.AddValue(CDec(ADS.getSymbolValueCached(Symbol, PollRate)))
            Average = _buffer.Average
            If SymbolTarget = "" Then
                If Relative Then
                    Deviation = _buffer.RelDeviation
                Else
                    Deviation = _buffer.AbsDeviation
                End If
            Else
                _bufferDelta.AddValue(Math.Abs(CDec(ADS.getSymbolValueCached(Symbol, PollRate)) - CDec(ADS.getSymbolValueCached(SymbolTarget, PollRate))))
                If Relative Then
                    If CDec(ADS.getSymbolValueCached(SymbolTarget, PollRate)) <> 0 Then
                        Deviation = _bufferDelta.Maximum / CDec(ADS.getSymbolValueCached(SymbolTarget, PollRate))
                    Else
                        Deviation = 0
                    End If
                Else
                    Deviation = _bufferDelta.Maximum
                End If
            End If
            IconChange()
        End If
        _elapsed = CInt((DateTime.UtcNow - _lastTime).TotalMilliseconds)
        _lastTime = DateTime.UtcNow
        _adjustedInterval = _adjustedInterval + CInt((PollRate - _elapsed) * 0.3)
        If _adjustedInterval > PollRate Then
            _adjustedInterval = PollRate
        ElseIf _adjustedInterval < 10 Then
            _adjustedInterval = 10
        End If
        TimerPoll.Interval = _adjustedInterval
    End Sub

#End Region

End Class
