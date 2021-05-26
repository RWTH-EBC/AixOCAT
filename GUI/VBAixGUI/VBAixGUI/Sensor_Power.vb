Option Strict On

Imports System.ComponentModel

Public Class Sensor_Power
    Inherits Sensor_Base_Orientation

#Region "Events"
    Public Event PowerChanged(ByVal NewStatus As Decimal)
#End Region

#Region "Properties"
    <Browsable(True), DefaultValue(0.0), Description("Power")> Private _power As Decimal = 0
#End Region

#Region "Property Functions"

    Public Property Power() As Decimal
        Get
            Power = _power
        End Get
        Set(ByVal Value As Decimal)
            If _power <> Value Then
                _power = Value
                IconChange()
                RaiseEvent PowerChanged(_power)
                Me.Invalidate()
            End If
        End Set
    End Property

#End Region

#Region "Subs"
    Private Sub IconChange()
        If Power >= 1000 Then
            Me.lblValue.Text = Power.ToString("F0") & " " & Unit
        ElseIf Power >= 100 Then
            Me.lblValue.Text = Power.ToString("F1") & " " & Unit
        Else
            Me.lblValue.Text = Power.ToString("F") & " " & Unit
        End If
    End Sub
#End Region

#Region "Form Events"
    Private Sub Sensor_Base_UnitChanged(NewStatus As String) Handles Me.UnitChanged
        IconChange()
    End Sub

    Private Sub TimerPoll_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        If DesignMode = False And ADS.Connected Then
            Power = CDec(ADS.getSymbolValueCached(Symbol, PollRate))
        End If
    End Sub

    Private Sub Sensor_Power_Load(sender As Object, e As EventArgs) Handles Me.Load
        If PollRate > 0 And DesignMode = False Then
            TimerPoll.Interval = PollRate
            TimerPoll.Start()
        End If
        IconChange()
    End Sub
#End Region

End Class

