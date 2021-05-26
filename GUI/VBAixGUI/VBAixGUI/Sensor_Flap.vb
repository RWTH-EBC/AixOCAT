Option Strict On

Imports System.ComponentModel

Public Class Sensor_Flap
    Inherits Sensor_Base_Component

#Region "Events"
    Public Event FlapChanged(ByVal NewStatus As Decimal)
#End Region

#Region "Properties"
    <Browsable(True), DefaultValue(2.0), Description("Close threshold")> Private _closeThreshold As Decimal = 2
    <Browsable(True), DefaultValue(0.0), Description("Flap")> Private _flap As Decimal = 0
    <Browsable(True), DefaultValue(95.0), Description("Open threshold")> Private _openThreshold As Decimal = 95
#End Region

#Region "Property Functions"

    Public Property CloseThreshold() As Decimal
        Get
            CloseThreshold = _closeThreshold
        End Get
        Set(ByVal Value As Decimal)
            If _closeThreshold <> Value Then
                _closeThreshold = Value
                IconChange()
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property Flap() As Decimal
        Get
            Flap = _flap
        End Get
        Set(ByVal Value As Decimal)
            If _flap <> Value Then
                _flap = Value
                IconChange()
                RaiseEvent FlapChanged(_flap)
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property OpenThreshold() As Decimal
        Get
            OpenThreshold = _openThreshold
        End Get
        Set(ByVal Value As Decimal)
            If _openThreshold <> Value Then
                _openThreshold = Value
                IconChange()
                Me.Invalidate()
            End If
        End Set
    End Property

#End Region

#Region "Subs"
    Private Sub IconChange()
        Me.lblValue.Text = Flap.ToString("F") & " " & Unit
        If Flap >= OpenThreshold Then
            Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.sensor_flap_open
        ElseIf Flap <= CloseThreshold Then
            Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.sensor_flap_closed
        Else
            Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.sensor_flap_half
        End If
    End Sub
#End Region

#Region "Form Events"
    Private Sub Sensor_Base_UnitChanged(NewStatus As String) Handles Me.UnitChanged
        IconChange()
    End Sub

    Private Sub TimerPoll_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        If DesignMode = False And ADS.Connected Then
            Flap = CDec(ADS.getSymbolValueCached(Symbol, PollRate))
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
#End Region

End Class

