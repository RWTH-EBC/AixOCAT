Option Strict On

Imports System.ComponentModel

Public Class Compressor

    Inherits Sensor_Base_Component

#Region "Events"
    Public Event FlussrichtungChanged(ByVal NewStatus As Orientation)
    Public Event DrehzahlChanged(ByVal NewStatus As Decimal)
#End Region

#Region "Properties"
    <Browsable(True), DefaultValue(100.0), Description("Drehzahl")> Private _drehzahl As Decimal
    <Browsable(True), DefaultValue(Orientation.Right), Description("Flussrichtung durch Verdichter")> Private _ausrichtung As Orientation = Orientation.Right
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
                        Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Verdichter_aus
                        Me.PictureBoxIcon.Image.RotateFlip(RotateFlipType.Rotate180FlipNone)
                        Me.Height = 80
                        Me.Width = 80
                        Me.lblValue.Top = 32
                        Me.lblValue.Left = 20
                    Case Orientation.Right
                        Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Verdichter_aus
                        Me.Height = 80
                        Me.Width = 80
                        Me.lblValue.Top = 30
                        Me.lblValue.Left = 20
                    Case Orientation.Top
                        Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Verdichter_aus
                        Me.PictureBoxIcon.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
                        Me.Height = 80
                        Me.Width = 80
                        Me.lblValue.Top = 30
                        Me.lblValue.Left = 18
                    Case Orientation.Bottom
                        Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Verdichter_aus
                        Me.PictureBoxIcon.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)
                        Me.Height = 80
                        Me.Width = 80
                        Me.lblValue.Top = 30
                        Me.lblValue.Left = 20
                End Select

                RaiseEvent FlussrichtungChanged(_ausrichtung)
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property RotationSpeed() As Decimal
        Get
            RotationSpeed = _drehzahl
        End Get
        Set(ByVal Value As Decimal)
            If _drehzahl <> Value Then
                _drehzahl = Value
                IconChange()
                RaiseEvent DrehzahlChanged(_drehzahl)
                Me.Invalidate()
            End If
        End Set
    End Property
#End Region

#Region "Subs"
    Private Sub IconChange()
        If RotationSpeed >= 1000 Then
            Me.lblValue.Text = RotationSpeed.ToString("F0") & " " & Unit
        ElseIf RotationSpeed >= 100 Then
            Me.lblValue.Text = RotationSpeed.ToString("F1") & " " & Unit
        Else
            Me.lblValue.Text = RotationSpeed.ToString("F") & " " & Unit
        End If
        If RotationSpeed <> 0 Then
            Select Case DirectionOfFlow
                Case Orientation.Left
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Verdichter_links
                Case Orientation.Right
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Verdichter_rechts
                Case Orientation.Top
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Verdichter_oben
                Case Orientation.Bottom
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Verdichter_unten
            End Select
        Else
            Select Case DirectionOfFlow
                Case Orientation.Left
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Verdichter_aus
                    Me.PictureBoxIcon.Image.RotateFlip(RotateFlipType.Rotate180FlipNone)
                Case Orientation.Right
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Verdichter_aus
                Case Orientation.Top
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Verdichter_aus
                    Me.PictureBoxIcon.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
                Case Orientation.Bottom
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Verdichter_aus
                    Me.PictureBoxIcon.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)

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
            RotationSpeed = CDec(ADS.getSymbolValueCached(Symbol, PollRate))
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