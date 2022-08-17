Option Strict On

Imports System.ComponentModel
Public Class waermeuebertrager
    Inherits Sensor_Base_Component

#Region "Events"
    Public Event EnergyflowChanged(ByVal NewStatus As Decimal)
#End Region

#Region "Properties"
    <Browsable(True), DefaultValue(100.0), Description("Energy Flow")> Private _energyflow As Decimal
#End Region

#Region "Property Functions"

    Public Event AusrichtungChanged(ByVal NewStatus As Orientation)
    <Browsable(True), DefaultValue(Orientation.Top), Description("Ausrichtung des Sensors")>
    Private mAusrichtung As Orientation = Orientation.Left
    Public Property Ausrichtung() As Orientation

        Get
            Ausrichtung = mAusrichtung
        End Get
        Set(ByVal Value As Orientation)

            mAusrichtung = Value
            Select Case Ausrichtung
                Case Orientation.Left
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Wärmeübertrager_1
                    Me.lblValue.Location = New System.Drawing.Point(15, 60)
                    Me.lblValue.Size = New Size(60, 12)
                    Me.lblValue.TextAlign = ContentAlignment.MiddleCenter
                Case Orientation.Top
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Wärmeübertrager_1
                    Me.PictureBoxIcon.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)
                    Me.lblValue.Location = New System.Drawing.Point(15, 60)
                    Me.lblValue.Size = New Size(60, 12)
                    Me.lblValue.TextAlign = ContentAlignment.MiddleCenter
                Case Orientation.Right
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Wärmeübertrager_1
                    Me.PictureBoxIcon.Image.RotateFlip(RotateFlipType.Rotate180FlipNone)
                    Me.lblValue.Location = New System.Drawing.Point(15, 60)
                    Me.lblValue.Size = New Size(60, 12)
                    Me.lblValue.TextAlign = ContentAlignment.MiddleCenter
                Case Orientation.Bottom
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Wärmeübertrager_1
                    Me.PictureBoxIcon.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
                    Me.lblValue.Location = New System.Drawing.Point(15, 10)
                    Me.lblValue.Size = New Size(60, 12)
                    Me.lblValue.TextAlign = ContentAlignment.MiddleCenter
            End Select

            RaiseEvent AusrichtungChanged(mAusrichtung)
            Me.Invalidate()
        End Set
    End Property

    Public Property EnergyFlow() As Decimal
        Get
            EnergyFlow = _energyflow
        End Get
        Set(ByVal Value As Decimal)
            If _energyflow <> Value Then
                _energyflow = Value
                IconChange()
                RaiseEvent EnergyflowChanged(_energyflow)
                Me.Invalidate()
            End If
        End Set
    End Property
#End Region

#Region "Subs"
    Private Sub IconChange()
        If EnergyFlow >= 1000 Then
            Me.lblValue.Text = EnergyFlow.ToString("F0") & " " & Unit
        ElseIf EnergyFlow >= 100 Then
            Me.lblValue.Text = EnergyFlow.ToString("F1") & " " & Unit
        Else
            Me.lblValue.Text = EnergyFlow.ToString("F1") & " " & Unit
        End If
    End Sub
#End Region

#Region "Form Events"
    Private Sub Sensor_Base_UnitChanged(NewStatus As String) Handles Me.UnitChanged
        IconChange()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        If DesignMode = False And ADS.Connected Then
            EnergyFlow = CDec(ADS.getSymbolValueCached(Symbol, PollRate))
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Waermeuebertrager_Load(sender As Object, e As EventArgs) Handles Me.Load
        If PollRate > 0 And DesignMode = False Then
            TimerPoll.Interval = PollRate
            TimerPoll.Start()
        End If
        Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.Wärmeübertrager_1
        Me.lblValue.Parent = Me.PictureBoxIcon
        'Me.lblValue.BackColor = Color.Transparent
        Me.lblValue.Location = New System.Drawing.Point(15, 60)
        Me.lblValue.Size = New Size(60, 12)
        Me.lblValue.TextAlign = ContentAlignment.MiddleCenter
        IconChange()
        Me.Ausrichtung = mAusrichtung
    End Sub
#End Region

End Class
