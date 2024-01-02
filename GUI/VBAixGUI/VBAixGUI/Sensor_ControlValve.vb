Option Strict On

Imports System.ComponentModel

Public Class Sensor_ControlValve
    Inherits Sensor_Base_Component

#Region "Events"
    Public Event PositionChanged(ByVal NewStatus As Decimal)
#End Region

#Region "Properties"
    <Browsable(True), DefaultValue(0.0), Description("Position")> Private _position As Decimal = 0
    <Browsable(True), DefaultValue(Rotation.horizontal), Description("Rotation")> Private _rotation As Rotation = Rotation.horizontal
#End Region

#Region "Property Functions"

    Public Property Position() As Decimal
        Get
            Position = _position
        End Get
        Set(ByVal Value As Decimal)
            If _position <> Value Then
                _position = Value
                IconChange()
                RaiseEvent PositionChanged(_position)
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property Rotation() As Rotation
        Get
            Rotation = _rotation
        End Get
        Set(ByVal Value As Rotation)
            If _rotation <> Value Then
                _rotation = Value
                IconChange()
                Me.Invalidate()
            End If
        End Set
    End Property

#End Region

#Region "Subs"
    Private Sub IconChange()
        Me.lblValue.Text = Position.ToString("F") & " " & Unit
        If Rotation = Rotation.vertical Then
            Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.sensor_control_valve_ver
        Else
            Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.sensor_control_valve_hor
        End If
    End Sub
#End Region

#Region "Form Events"
    Private Sub Sensor_Base_UnitChanged(NewStatus As String) Handles Me.UnitChanged
        IconChange()
    End Sub

    Private Sub TimerPoll_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        If DesignMode = False And ADS.Connected Then
            Position = CDec(ADS.getSymbolValueCached(Symbol, PollRate))
        End If
    End Sub

    Private Sub Sensor_Load(sender As Object, e As EventArgs) Handles Me.Load
        If PollRate > 0 And DesignMode = False Then
            TimerPoll.Interval = PollRate
            TimerPoll.Start()
        End If
        IconChange()
    End Sub

    Private Sub Sensor_Flap_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.PictureBoxIcon.Size = Me.Size
        Me.PictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage
        Me.lblValue.Width = Me.Size.Width - 8
        Me.lblValue.Top = CInt((Me.Size.Height - 12) / 2)
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
#End Region

End Class

