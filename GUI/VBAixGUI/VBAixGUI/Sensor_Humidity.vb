Option Strict On

Imports System.ComponentModel

Public Class Sensor_Humidity
    Inherits Sensor_Base_Orientation

#Region "Events"
    Public Event HumidityChanged(ByVal NewStatus As Decimal)
#End Region

#Region "Properties"
    <Browsable(True), DefaultValue(100.0), Description("Temperatur as Dezimal")> Private _humidity As Decimal = 0
#End Region

#Region "Property Functions"
    Public Property Humidity() As Decimal
        Get
            Humidity = _humidity
        End Get
        Set(ByVal Value As Decimal)
            If _humidity <> Value Then
                _humidity = Value
                IconChange()
                RaiseEvent HumidityChanged(_humidity)
                Me.Invalidate()
            End If
        End Set
    End Property
#End Region

#Region "Subs"
    Private Sub IconChange()
        Me.lblValue.Text = Humidity.ToString("F") & " %rF"
    End Sub
#End Region

#Region "Form Events"
    Private Sub Sensor_Humidity_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Size = New Size(60, 50)
        Me.lblValue.Size = New Size(60, 12)
        If PollRate > 0 And DesignMode = False Then
            TimerPoll.Interval = PollRate
            TimerPoll.Start()
        End If
        IconChange()
    End Sub

    Private Sub TimerPoll_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        If DesignMode = False And ADS.Connected Then
            Humidity = CDec(ADS.getSymbolValueCached(Symbol, PollRate))
        End If
    End Sub
#End Region

End Class