Option Strict On

Imports System.ComponentModel
Public Class Sensor_Decimal
    Inherits Sensor_Base_Orientation

#Region "Events"
    Public Event DecimalValueChanged(ByVal NewStatus As Decimal)
#End Region

#Region "Properties"
    <Browsable(True), DefaultValue(0.0), Description("Decimal value")> Private _decimalValue As Decimal
#End Region

#Region "Property Functions"
    Public Property DecimalValue() As Decimal
        Get
            DecimalValue = _decimalValue
        End Get
        Set(ByVal Value As Decimal)
            If _decimalValue <> Value Then
                _decimalValue = Value
                _decimalValue = Decimal.Round(_decimalValue, 2)
                IconChange()
                RaiseEvent DecimalValueChanged(_decimalValue)
                Me.Invalidate()
            End If
        End Set
    End Property
#End Region

#Region "Subs"
    Private Sub IconChange()
        If DecimalValue >= 1000 Then
            Me.lblValue.Text = DecimalValue.ToString("F0") & " " & Unit
        ElseIf DecimalValue >= 100 Then
            Me.lblValue.Text = DecimalValue.ToString("F1") & " " & Unit
        Else
            Me.lblValue.Text = DecimalValue.ToString("F") & " " & Unit
        End If
    End Sub
#End Region

#Region "Form Events"
    Private Sub Sensor_Base_UnitChanged(NewStatus As String) Handles Me.UnitChanged
        IconChange()
    End Sub

    Private Sub TimerPoll_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        If DesignMode = False And ADS.Connected Then
            DecimalValue = CDec(ADS.getSymbolValueCached(Symbol, PollRate))
        End If
    End Sub

    Private Sub DezimalAnzeige_Load(sender As Object, e As EventArgs) Handles Me.Load
        If PollRate > 0 And DesignMode = False Then
            TimerPoll.Interval = PollRate
            TimerPoll.Start()
        End If
        IconChange()
    End Sub
#End Region

End Class
