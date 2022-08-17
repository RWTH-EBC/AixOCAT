Option Strict On

Imports System.ComponentModel

Public Class Sensor_Volumeflow
    Inherits Sensor_Base_Component

#Region "Events"
    Public Event ShapeChanged(ByVal NewStatus As Shape)
    Public Event VolumeflowChanged(ByVal NewStatus As Decimal)
#End Region

#Region "Properties"
    <Browsable(True), DefaultValue(Shape.Round), Description("Shape")> Private _shape As Shape = Shape.Round
    <Browsable(True), DefaultValue(100.0), Description("Volumeflow")> Private _volumeflow As Decimal
#End Region

#Region "Property Functions"
    Public Property Shape() As Shape
        Get
            Shape = _shape
        End Get
        Set(ByVal Value As Shape)
            If _shape <> Value Then
                _shape = Value
                Me.lblValue.Visible = True
                If _shape = Shape.Round Then
                    Me.lblValue.Top = 42
                    Me.Height = 80
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.sensor_volumeflow_round
                ElseIf _shape = Shape.Rectangle Then
                    Me.lblValue.Top = 10
                    Me.Height = 40
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.sensor_volumeflow_rectangle
                End If
                RaiseEvent ShapeChanged(_shape)
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property Volumeflow() As Decimal
        Get
            Volumeflow = _volumeflow
        End Get
        Set(ByVal Value As Decimal)
            If _volumeflow <> Value Then
                _volumeflow = Value
                IconChange()
                RaiseEvent VolumeflowChanged(_volumeflow)
                Me.Invalidate()
            End If
        End Set
    End Property
#End Region

#Region "Subs"
    Private Sub IconChange()
        If Volumeflow >= 1000 Then
            Me.lblValue.Text = Volumeflow.ToString("F0") & " " & Unit
        ElseIf Volumeflow >= 100 Then
            Me.lblValue.Text = Volumeflow.ToString("F1") & " " & Unit
        Else
            Me.lblValue.Text = Volumeflow.ToString("F") & " " & Unit
        End If
    End Sub
#End Region

#Region "Form Events"
    Private Sub Sensor_Base_UnitChanged(NewStatus As String) Handles Me.UnitChanged
        IconChange()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        If DesignMode = False And ADS.Connected Then
            Volumeflow = CDec(ADS.getSymbolValueCached(Symbol, PollRate))
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