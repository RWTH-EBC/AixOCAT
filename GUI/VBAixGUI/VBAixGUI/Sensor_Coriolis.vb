Option Strict On

Imports System.ComponentModel

Public Class Sensor_Coriolis
    Inherits Sensor_Base_Component

#Region "Events"
    Public Event FlussrichtungChanged(ByVal NewStatus As Orientation)
    Public Event MassflowChanged(ByVal NewStatus As Decimal)
#End Region

#Region "Properties"
    <Browsable(True), DefaultValue(100.0), Description("Massflow")> Private _massflow As Decimal
    <Browsable(True), DefaultValue(Orientation.Right), Description("Flussrichtung des Coriolis-Sensors")> Private _ausrichtung As Orientation = Orientation.Left
#End Region

#Region "Property Functions"

    Public Property DirectionOfFlow() As Orientation
        Get
            DirectionOfFlow = _ausrichtung
        End Get
        Set(value As Orientation)
            If _ausrichtung <> value Then
                _ausrichtung = value

                Select Case DirectionOfFlow
                    Case Orientation.Left
                        Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Coriolis_aus_Horiz
                        Me.PictureBoxIcon.Image.RotateFlip(RotateFlipType.RotateNoneFlipX)
                        Me.Height = 68
                        Me.Width = 79
                        Me.lblValue.Top = 2
                        Me.lblValue.Left = 3
                    Case Orientation.Right
                        Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Coriolis_aus_Horiz
                        Me.Height = 68
                        Me.Width = 79
                        Me.lblValue.Top = 2
                        Me.lblValue.Left = 3
                    Case Orientation.Top
                        Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Coriolis_aus_vert
                        Me.PictureBoxIcon.Image.RotateFlip(RotateFlipType.RotateNoneFlipY)
                        Me.Height = 80
                        Me.Width = 80
                        Me.lblValue.Top = 58
                        Me.lblValue.Left = 4
                    Case Orientation.Bottom
                        Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Coriolis_aus_vert
                        Me.Height = 80
                        Me.Width = 80
                        Me.lblValue.Top = 11
                        Me.lblValue.Left = 4
                End Select

                RaiseEvent FlussrichtungChanged(_ausrichtung)
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property Massflow() As Decimal
        Get
            Massflow = _massflow
        End Get
        Set(ByVal Value As Decimal)
            If _massflow <> Value Then
                _massflow = Value
                IconChange()
                RaiseEvent MassflowChanged(_massflow)
                Me.Invalidate()
            End If
        End Set
    End Property
#End Region

#Region "Subs"
    Private Sub IconChange()
        If Massflow >= 1000 Then
            Me.lblValue.Text = Massflow.ToString("F0") & " " & Unit
        ElseIf Massflow >= 100 Then
            Me.lblValue.Text = Massflow.ToString("F1") & " " & Unit
        Else
            Me.lblValue.Text = Massflow.ToString("F") & " " & Unit
        End If
        If Massflow <> 0 Then
            Select Case DirectionOfFlow
                Case Orientation.Left
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Coriolis_ein_horiz
                    Me.PictureBoxIcon.Image.RotateFlip(RotateFlipType.RotateNoneFlipX)
                Case Orientation.Right
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Coriolis_ein_horiz
                Case Orientation.Top
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Coriolis_ein_vert
                    Me.PictureBoxIcon.Image.RotateFlip(RotateFlipType.RotateNoneFlipY)
                Case Orientation.Bottom
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Coriolis_ein_vert
            End Select
        Else
            Select Case DirectionOfFlow
                Case Orientation.Left
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Coriolis_aus_Horiz
                    Me.PictureBoxIcon.Image.RotateFlip(RotateFlipType.RotateNoneFlipX)
                Case Orientation.Right
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Coriolis_aus_Horiz
                Case Orientation.Top
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Coriolis_aus_vert
                    Me.PictureBoxIcon.Image.RotateFlip(RotateFlipType.RotateNoneFlipY)
                Case Orientation.Bottom
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Coriolis_aus_vert
            End Select
        End If
    End Sub
#End Region

#Region "Form Events"
    Private Sub Sensor_Base_UnitChanged(NewStatus As String) Handles Me.UnitChanged
        IconChange()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        If DesignMode = False And ADS.Connected Then
            Massflow = CDec(ADS.getSymbolValueCached(Symbol, PollRate))
        End If
    End Sub

    Private Sub Volumenstromsensor_Load(sender As Object, e As EventArgs) Handles Me.Load
        If PollRate > 0 And DesignMode = False Then
            TimerPoll.Interval = PollRate
            TimerPoll.Start()
        End If
        IconChange()
    End Sub
#End Region

End Class