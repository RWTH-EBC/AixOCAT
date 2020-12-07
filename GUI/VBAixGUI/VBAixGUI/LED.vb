Option Strict On

Imports System.ComponentModel
Public Class LED

#Region "Events"
    Public Event StateChanged(ByVal NewStatus As Boolean)
#End Region

#Region "Properties"
    <Browsable(True), Description("Color False")> Dim _colorFalse As Color = System.Drawing.Color.FromArgb(204, 7, 30)
    <Browsable(True), Description("Color True")> Dim _colorTrue As Color = System.Drawing.Color.FromArgb(87, 171, 39)
    <Browsable(True), Description("Poll rate in ms")> Dim _pollRate As Integer = 1000
    <Browsable(True), Description("new set value")> Dim _setValueNew As Boolean
    <Browsable(True), DefaultValue(0), Description("State")> Private _state As Boolean
    <Browsable(True), Description("Symbolname")> Dim _symbol As String
    <Browsable(True), Description("Text False")> Dim _textFalse As String
    <Browsable(True), Description("Text True")> Dim _textTrue As String
#End Region

#Region "Property Functions"
    Public Property ColorFalse() As Color
        Get
            ColorFalse = _colorFalse
        End Get
        Set(ByVal Value As Color)
            If _colorFalse <> Value Then
                _colorFalse = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property ColorTrue() As Color
        Get
            ColorTrue = _colorTrue
        End Get
        Set(ByVal Value As Color)
            If _colorTrue <> Value Then
                _colorTrue = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property TextFalse() As String
        Get
            TextFalse = _textFalse
        End Get
        Set(ByVal Value As String)
            If _textFalse <> Value Then
                _textFalse = Value
                IconChange()
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property TextTrue() As String
        Get
            TextTrue = _textTrue
        End Get
        Set(ByVal Value As String)
            If _textTrue <> Value Then
                _textTrue = Value
                IconChange()
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

    Public Property State() As Boolean
        Get
            State = _state
        End Get
        Set(ByVal Value As Boolean)
            If _state <> Value Then
                _state = Value
                RaiseEvent StateChanged(_state)
                IconChange()
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
#End Region

#Region "Subs"
    Private Sub IconChange()
        Select Case State
            Case True
                Me.Label1.Text = TextTrue
                Me.Label1.BackColor = ColorTrue
            Case False
                Me.Label1.Text = TextFalse
                Me.Label1.BackColor = ColorFalse
        End Select
    End Sub

    Public Sub SubmitSetValue()
        ADS.setSymboleValue(Symbol, Convert.ToBoolean(State))
        SetValueNew = False
    End Sub
#End Region

#Region "Form Events"
    Private Sub TimerPoll_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        If DesignMode = False Then
            If SetValueNew = True Then
                ADS.setSymboleValue(Symbol, Convert.ToBoolean(State))
                SetValueNew = False
            Else
                State = System.Convert.ToBoolean(ADS.getSymbolValueCached(Symbol, PollRate))
            End If
        End If
    End Sub

    Private Sub LED_Load(sender As Object, e As EventArgs) Handles Me.Load
        If PollRate > 0 Then
            TimerPoll.Interval = PollRate
            TimerPoll.Start()
        End If
        IconChange()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        SetValueNew = True
        State = Not State
    End Sub
#End Region

End Class
