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
    <Browsable(True), Description("Absolute Convergence Threshold")> Dim _absConvergenceThreshold As Decimal = 5
    <Browsable(True), Description("Relative Convergence Threshold")> Dim _relConvergenceThreshold As Decimal = 0.05
    <Browsable(True), Description("AbsDeviation")> Dim _absDeviation As Decimal
    <Browsable(True), Description("RelDeviation")> Dim _relDeviation As Decimal
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

    Public Property AbsConvergenceThreshold() As Decimal
        Get
            AbsConvergenceThreshold = _absConvergenceThreshold
        End Get
        Set(ByVal Value As Decimal)
            If _absConvergenceThreshold <> Value Then
                _absConvergenceThreshold = Value
                IconChange()
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property RelConvergenceThreshold() As Decimal
        Get
            RelConvergenceThreshold = _relConvergenceThreshold
        End Get
        Set(ByVal Value As Decimal)
            If _relConvergenceThreshold <> Value Then
                _relConvergenceThreshold = Value
                IconChange()
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property AbsDeviation() As Decimal
        Get
            AbsDeviation = _absDeviation
        End Get
        Set(ByVal Value As Decimal)
            If _absDeviation <> Value Then
                _absDeviation = Value
                EvaluateConvergence()
                IconChange()
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property RelDeviation() As Decimal
        Get
            RelDeviation = _relDeviation
        End Get
        Set(ByVal Value As Decimal)
            If _relDeviation <> Value Then
                _relDeviation = Value
                EvaluateConvergence()
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
            deviation = Me.RelDeviation * 100
            unit = "%"
        Else
            deviation = Me.AbsDeviation
            unit = Me.Unit
        End If
        If Average >= 1000 Then
            Me.lblValue.Text = Average.ToString("F0") & " " & Me.Unit
        ElseIf Average >= 100 Then
            Me.lblValue.Text = Average.ToString("F1") & " " & Me.Unit
        ElseIf Average >= 1 Then
            Me.lblValue.Text = Average.ToString("F2") & " " & Me.Unit
        Else
            Me.lblValue.Text = Average.ToString("F3") & " " & Me.Unit
        End If
        If deviation >= 100 Then
            Me.lblDeviation.Text = "+/- " & deviation.ToString("F0") & " " & unit
        ElseIf deviation >= 10 Then
            Me.lblDeviation.Text = "+/- " & deviation.ToString("F1") & " " & unit
        ElseIf deviation >= 1 Then
            Me.lblDeviation.Text = "+/- " & deviation.ToString("F2") & " " & unit
        Else
            Me.lblDeviation.Text = "+/- " & deviation.ToString("F3") & " " & unit
        End If
        If Convergence Then
            Me.lblDeviation.BackColor = System.Drawing.Color.FromArgb(87, 171, 39)
            Me.ToolTipInfo.SetToolTip(Me.lblDeviation, "stationary")
        Else
            Me.lblDeviation.BackColor = System.Drawing.Color.FromArgb(204, 7, 30)
            Me.ToolTipInfo.SetToolTip(Me.lblDeviation, "not stationary")
        End If
    End Sub

    Private Sub EvaluateConvergence()
        Dim refValue As Decimal
        Dim relAbsDeviation As Decimal
        If SymbolTarget = "" Then
            refValue = Average
        Else
            refValue = CDec(ADS.getSymbolValueCached(SymbolTarget, PollRate))
        End If
        relAbsDeviation = refValue * RelDeviation
        If AbsDeviation > relAbsDeviation Or (RelDeviation > RelConvergenceThreshold) Then
            Relative = False
        Else
            Relative = True
        End If
        Convergence = (AbsDeviation <= AbsConvergenceThreshold) Or (RelDeviation <= RelConvergenceThreshold)
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
                RelDeviation = _buffer.RelDeviation
                AbsDeviation = _buffer.AbsDeviation
            Else
                _bufferDelta.AddValue(Math.Abs(CDec(ADS.getSymbolValueCached(Symbol, PollRate)) - CDec(ADS.getSymbolValueCached(SymbolTarget, PollRate))))
                If CDec(ADS.getSymbolValueCached(SymbolTarget, PollRate)) <> 0 Then
                    RelDeviation = _bufferDelta.Maximum / CDec(ADS.getSymbolValueCached(SymbolTarget, PollRate))
                Else
                    RelDeviation = 0
                End If
                AbsDeviation = _bufferDelta.Maximum
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
