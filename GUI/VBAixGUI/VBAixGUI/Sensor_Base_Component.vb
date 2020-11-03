Option Strict On

Imports System.ComponentModel

Public Class Sensor_Base_Component

#Region "Events"
    Public Event UnitChanged(ByVal NewStatus As String)
#End Region

#Region "Properties"
    <Browsable(True), Description("Accuracy class")> Dim _accuracyClass As String
    <Browsable(True), Description("Component variant")> Dim _componentVariant As String
    <Browsable(True), Description("Hint")> Dim _hint As String
    <Browsable(True), Description("Manufacturer")> Dim _manufacturer As String
    <Browsable(True), Description("Model, Version, Type")> Dim _model As String
    <Browsable(True), Description("Poll rate")> Dim _pollRate As Integer = 1000
    <Browsable(True), Description("Symbolname")> Dim _symbol As String
    <Browsable(True), Description("Display unit")> Dim _unit As String
#End Region

#Region "Property Functions"
    Public Property AccuracyClass() As String
        Get
            AccuracyClass = _accuracyClass
        End Get
        Set(ByVal Value As String)
            If _accuracyClass <> Value Then
                _accuracyClass = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property ComponentVariant() As String
        Get
            ComponentVariant = _componentVariant
        End Get
        Set(ByVal Value As String)
            If _componentVariant <> Value Then
                _componentVariant = Value
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

    Public Property Manufacturer() As String
        Get
            Manufacturer = _manufacturer
        End Get
        Set(ByVal Value As String)
            If _manufacturer <> Value Then
                _manufacturer = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property Model() As String
        Get
            Model = _model
        End Get
        Set(ByVal Value As String)
            If _model <> Value Then
                _model = Value
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
                RaiseEvent UnitChanged(_unit)
                Me.Invalidate()
            End If
        End Set
    End Property
#End Region

#Region "Form Events"
    Protected Sub PictureBoxIcon_Click(sender As Object, e As EventArgs) Handles PictureBoxIcon.Click
        Me.ToolTipInfo.Show("Symbol: " & Symbol & ControlChars.NewLine &
                   "Manufacturer: " & Manufacturer & ControlChars.NewLine &
                   "Model: " & Model & ControlChars.NewLine &
                   "Hint: " & Hint, Me.PictureBoxIcon)
    End Sub
#End Region

End Class

