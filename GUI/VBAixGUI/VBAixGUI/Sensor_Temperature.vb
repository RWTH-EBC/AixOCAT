Option Strict On

Imports System.ComponentModel

Public Class Sensor_Temperature
    Inherits Sensor_Base_Orientation

#Region "Events"
    Public Event TemperatureChanged(ByVal NewStatus As Decimal)
#End Region

#Region "Properties"
    <Browsable(True), DefaultValue(100.0), Description("Temperatur as Dezimal")> Private _temperature As Decimal = 0
#End Region

#Region "Property Functions"
    Public Property Temperature() As Decimal
        Get
            Temperature = _temperature
        End Get
        Set(ByVal Value As Decimal)
            If _temperature <> Value Then
                _temperature = Value
                IconChange()
                RaiseEvent TemperatureChanged(_temperature)
                Me.Invalidate()
            End If
        End Set
    End Property
#End Region

#Region "Subs"
    Private Sub IconChange()
        Me.lblValue.Text = Temperature.ToString("F") & " " & Unit
        If _temperature > 0 Then
            PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.sensor_temperature_hot
        Else
            PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.sensor_temperature_cold
        End If
    End Sub
#End Region

#Region "Form Events"
    Private Sub Sensor_Base_UnitChanged(NewStatus As String) Handles Me.UnitChanged
        IconChange()
    End Sub

    Private Sub Sensor_Temperature_Load(sender As Object, e As EventArgs) Handles Me.Load
        If PollRate > 0 And DesignMode = False Then
            TimerPoll.Interval = PollRate
            TimerPoll.Start()
        End If
        IconChange()
    End Sub

    Private Sub TimerPoll_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        If DesignMode = False And ADS.Connected Then
            Temperature = CDec(ADS.getSymbolValueCached(Symbol, PollRate))
        End If
    End Sub
#End Region

End Class