Option Strict On

Imports System.ComponentModel
Public Class Input_SetValue

#Region "Events"
    Public Event ValueChanged(ByVal NewStatus As Decimal)
#End Region

#Region "Properties"
    <Browsable(True), Description("Active")> Dim _active As Boolean
    <Browsable(True), Description("Hint")> Dim _hint As String
    <Browsable(True), Description("Anzahl Nachkommastellen")> Dim _decimalplaces As Integer = 2
    <Browsable(True), Description("maximaler Wert der Variablen")> Dim _maxValue As Decimal = 100
    <Browsable(True), Description("minimaler Wert der Variablen")> Dim _minValue As Decimal = 0
    <Browsable(True), Description("Aktuallisierungsrate in ms")> Dim _pollRate As Integer = 1000
    <Browsable(True), DefaultValue(0), Description("SetValue")> Dim _setValue As Decimal = 0
    <Browsable(True), Description("SollWertNeu")> Dim _setValueNew As Boolean
    Dim _readValue As Decimal = 0
    Dim _readValueNew As Boolean
    <Browsable(True), Description("Symbolname")> Dim _symbol As String
    <Browsable(True), Description("Anzeige Einheit")> Dim _unit As String
#End Region

#Region "Property Functions"
    Public Property Active() As Boolean
        Get
            Active = _active
        End Get
        Set(ByVal Value As Boolean)
            If _active <> Value Then
                _active = Value
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

    Public Property DecimalPlaces() As Integer
        Get
            DecimalPlaces = _decimalplaces
        End Get
        Set(ByVal Value As Integer)
            If _decimalplaces <> Value Then
                _decimalplaces = Value
                applyDecimalPlaces()
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property MaxValue() As Decimal
        Get
            MaxValue = _maxValue
        End Get
        Set(ByVal Value As Decimal)
            If _maxValue <> Value Then
                _maxValue = Value
                applyMinMax()
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property MinValue() As Decimal
        Get
            MinValue = _minValue
        End Get
        Set(ByVal Value As Decimal)
            If _minValue <> Value Then
                _minValue = Value
                applyMinMax()
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

    Public Property SetValue() As Decimal
        Get
            SetValue = _setValue
        End Get
        Set(ByVal Value As Decimal)
            If _setValue <> Value Then
                _setValue = Value
                _setValue = Decimal.Round(_setValue, _decimalplaces)
                lblUnit.Text = Unit
                If SetValueNew = False Then
                    If _setValue > MaxValue Then
                        _setValue = MaxValue
                    End If
                    If _setValue < MinValue Then
                        _setValue = MinValue
                    End If
                    NumericUpDown1.Value = _setValue
                End If
                RaiseEvent ValueChanged(_setValue)
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property SetValueNew() As Boolean
        Get
            SetValueNew = _setValueNew
        End Get
        Set(ByVal Value As Boolean)
            If _setValueNew <> Value Then
                _setValueNew = Value
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

    Public Property Unit() As String
        Get
            Unit = _unit
        End Get
        Set(ByVal Value As String)
            If _unit <> Value Then
                _unit = Value
                lblUnit.Text = _unit
                Me.Invalidate()
                Me.NumericUpDown1.Width = Me.Width - lblUnit.Width - 2
            End If
        End Set
    End Property
#End Region

#Region "Subs"
    Private Sub applyMinMax()
        Me.NumericUpDown1.Minimum = MinValue
        Me.NumericUpDown1.Maximum = MaxValue
    End Sub

    Private Sub applyDecimalPlaces()
        Me.NumericUpDown1.DecimalPlaces = _decimalplaces
    End Sub

    Public Sub SubmitSetValue()
        ADS.setSymboleValue(Symbol, CSng(SetValue))
        SetValueNew = False
    End Sub
#End Region

#Region "Form Events"
    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If Not _readValueNew Then
            SetValueNew = True
            SetValue = NumericUpDown1.Value
        End If
        _readValueNew = False
    End Sub

    Private Sub Input_SetValue_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        NumericUpDown1.Size = New Drawing.Size(Me.Size.Width - Me.lblUnit.Size.Width - 5, NumericUpDown1.Size.Height)
    End Sub

    Private Sub TimerPoll_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        If DesignMode = False Then
            If SetValueNew = True Then
                ADS.setSymboleValue(Symbol, CSng(SetValue))
                SetValueNew = False
            Else
                _readValue = CDec(ADS.getSymbolValueCached(Symbol, PollRate))
                If _readValue <> SetValue Then
                    _readValueNew = True
                End If
                SetValue = CDec(ADS.getSymbolValueCached(Symbol, PollRate))
            End If
        End If
    End Sub

    Private Sub Input_SetValue_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetValueNew = False
        If PollRate > 0 Then
            TimerPoll.Interval = PollRate
            TimerPoll.Start()
        End If
    End Sub
#End Region


End Class
