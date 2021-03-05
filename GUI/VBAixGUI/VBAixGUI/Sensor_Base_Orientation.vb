Option Strict On

Imports System.ComponentModel

Public Class Sensor_Base_Orientation

#Region "Events"
    Public Event UnitChanged(ByVal NewStatus As String)
    Public Event OrientationChanged(ByVal NewStatus As Orientation)
#End Region

#Region "Properties"
    <Browsable(True), Description("Accuracy class")> Dim _accuracyClass As String
    <Browsable(True), Description("Average Time")> Dim _averageTime As Integer = 0
    <Browsable(True), Description("Component variant")> Dim _componentVariant As String
    <Browsable(True), Description("Hint")> Dim _hint As String
    <Browsable(True), Description("Manufacturer")> Dim _manufacturer As String
    <Browsable(True), Description("Model, Version, Type")> Dim _model As String
    <Browsable(True), DefaultValue(Orientation.Bottom), Description("Orientation of the label")> Private _orientation As Orientation = Orientation.Bottom
    <Browsable(True), Description("Poll rate")> Dim _pollRate As Integer = 1000
    <Browsable(True), Description("Symbolname")> Dim _symbol As String
    <Browsable(True), Description("Display unit")> Dim _unit As String

    Protected _buffer As Buffer = New Buffer()
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

    Public Property AverageTime() As Integer
        Get
            AverageTime = _averageTime
        End Get
        Set(ByVal Value As Integer)
            If _averageTime <> Value Then
                _averageTime = Value
                If _averageTime > 0 Then
                    _buffer = New Buffer(CInt(Math.Floor(_averageTime / PollRate)))
                End If
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

    Public Property Orientation() As Orientation
        Get
            Orientation = _orientation
        End Get
        Set(ByVal Value As Orientation)
            If _orientation <> Value Then
                _orientation = Value

                Me.lblValue.Visible = True

                If _orientation = Orientation.Right Then
                    Me.Size = New Drawing.Size(100, 38)
                    Me.PictureBoxIcon.Location = New Drawing.Point(4, 4)
                    Me.lblValue.Size = New Drawing.Size(70, 16)
                    Me.lblValue.Dock = DockStyle.Right
                    Me.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
                ElseIf _orientation = Orientation.Left Then
                    Me.Size = New Drawing.Size(100, 38)
                    Me.PictureBoxIcon.Location = New Drawing.Point(70, 4)
                    Me.lblValue.Size = New Drawing.Size(70, 16)
                    Me.lblValue.Dock = DockStyle.Left
                    Me.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                ElseIf _orientation = Orientation.Top Then
                    Me.Size = New Drawing.Size(70, 50)
                    Me.PictureBoxIcon.Location = New Drawing.Point(28, 20)
                    Me.lblValue.Size = New Drawing.Size(70, 16)
                    Me.lblValue.Dock = DockStyle.Top
                    Me.lblValue.TextAlign = System.Drawing.ContentAlignment.BottomCenter
                ElseIf _orientation = Orientation.Bottom Then
                    Me.Size = New Drawing.Size(70, 50)
                    Me.PictureBoxIcon.Location = New Drawing.Point(28, 4)
                    Me.lblValue.Size = New Drawing.Size(70, 16)
                    Me.lblValue.Dock = DockStyle.Bottom
                    Me.lblValue.TextAlign = System.Drawing.ContentAlignment.BottomCenter
                ElseIf _orientation = Orientation.Off Then
                    Me.PictureBoxIcon.Location = New Drawing.Point(4, 4)
                    Me.Size = New Drawing.Size(38, 38)
                    Me.lblValue.Visible = False
                End If

                RaiseEvent OrientationChanged(_orientation)
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
        Me.ToolTipInfo.Show("Symbolname: " & Symbol & ControlChars.NewLine &
                   "Hersteller:" & Manufacturer & ControlChars.NewLine &
                   "Model: " & Model & ControlChars.NewLine &
                    "Hinweis: " & Hint, Me.PictureBoxIcon)
    End Sub
#End Region

End Class

