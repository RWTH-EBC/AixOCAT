Option Strict On

Imports System.ComponentModel

Public Class Sensor_Pressure
    Inherits Sensor_Base_Component

#Region "Events"
    Public Event ShapeChanged(ByVal NewStatus As PressureSensorType)
    Public Event PressureChanged(ByVal NewStatus As Decimal)
#End Region

#Region "Properties"
    <Browsable(True), DefaultValue(10.0), Description("Pressure")> Private _pressure As Decimal
    <Browsable(True), DefaultValue(PressureSensorType.Differential), Description("Type")> Private _type As PressureSensorType = PressureSensorType.Differential
#End Region

#Region "Property Functions"
    Public Property Pressure() As Decimal
        Get
            Pressure = _pressure
        End Get
        Set(ByVal Value As Decimal)
            If _pressure <> Value Then
                _pressure = Value
                IconChange()
                RaiseEvent PressureChanged(_pressure)
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property Type() As PressureSensorType
        Get
            Type = _type
        End Get
        Set(ByVal Value As PressureSensorType)
            If _type <> Value Then
                _type = Value
                Me.lblValue.Visible = True
                If _type = PressureSensorType.Differential Then
                    Me.lblValue.Left = 5
                    Me.Height = 40
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.sensor_pressure_diff
                ElseIf _type = PressureSensorType.Absolute Then
                    Me.lblValue.Left = 10
                    Me.MaximumSize = New System.Drawing.Size(97, 40)
                    Me.MinimumSize = New System.Drawing.Size(97, 34)
                    Me.Height = 34
                    Me.Width = 97
                    Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Resources.sensor_pressure_abs
                End If
                RaiseEvent ShapeChanged(_type)
                Me.Invalidate()
            End If
        End Set
    End Property
#End Region

#Region "Subs"
    Private Sub IconChange()
        If Pressure >= 1000 Then
            Me.lblValue.Text = Pressure.ToString("F0") & " " & Unit
        ElseIf Pressure >= 100 Then
            Me.lblValue.Text = Pressure.ToString("F1") & " " & Unit
        Else
            Me.lblValue.Text = Pressure.ToString("F") & " " & Unit
        End If
    End Sub
#End Region

#Region "Form Events"
    Private Sub Sensor_Base_UnitChanged(NewStatus As String) Handles Me.UnitChanged
        IconChange()
    End Sub

    Private Sub TimerPoll_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        If DesignMode = False And ADS.Connected Then
            Pressure = CDec(ADS.getSymbolValueCached(Symbol, PollRate))
        End If
    End Sub

    Private Sub Pressure_Load(sender As Object, e As EventArgs) Handles Me.Load
        If PollRate > 0 And DesignMode = False Then
            TimerPoll.Interval = PollRate
            TimerPoll.Start()
        End If
        IconChange()
    End Sub
#End Region

End Class