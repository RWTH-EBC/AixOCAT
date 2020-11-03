Option Strict On

Imports System.ComponentModel

Public Class Sensor_Fan
    Inherits Sensor_Base_Component

#Region "Events"
    Public Event ControlChanged(ByVal NewStatus As Decimal)
    Public Event PressureChanged(ByVal NewStatus As Decimal)
    Public Event VolumeflowChanged(ByVal NewStatus As Decimal)
#End Region

#Region "Properties"
    <Browsable(True), DefaultValue(0.0), Description("Pressure")> Private _control As Decimal = 0
    <Browsable(True), DefaultValue(FanType.Radial), Description("Fan type")> Dim _fanType As FanType = FanType.Radial
    <Browsable(True), DefaultValue(0.0), Description("Pressure")> Private _pressure As Decimal = 0
    <Browsable(True), Description("Symbolname pressure")> Dim _pressureSymbol As String
    <Browsable(True), DefaultValue("Pa"), Description("Pressure Unit")> Dim _pressureUnit As String
    <Browsable(True), DefaultValue(0.0), Description("Volumeflow")> Private _volumeflow As Decimal = 0
    <Browsable(True), Description("Symbolname volumeflow")> Dim _volumeflowSymbol As String
    <Browsable(True), DefaultValue("m³/h"), Description("Volumeflow unit")> Dim _volumeflowUnit As String
#End Region

#Region "Property Functions"
    Public Property Control() As Decimal
        Get
            Control = _control
        End Get
        Set(ByVal Value As Decimal)
            If _control <> Value Then
                _control = Value
                IconChange()
                RaiseEvent PressureChanged(_pressure)
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property FanType() As FanType
        Get
            FanType = _fanType
        End Get
        Set(ByVal Value As FanType)
            If _fanType <> Value Then
                _fanType = Value
                IconChange()
                Me.Invalidate()
            End If
        End Set
    End Property

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

    Public Property PressureSymbol() As String
        Get
            PressureSymbol = _pressureSymbol
        End Get
        Set(ByVal Value As String)
            If _pressureSymbol <> Value Then
                _pressureSymbol = Value
                VisibilityLabels()
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property PressureUnit() As String
        Get
            PressureUnit = _pressureUnit
        End Get
        Set(ByVal Value As String)
            If _pressureUnit <> Value Then
                _pressureUnit = Value
                IconChange()
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

    Public Property VolumeflowSymbol() As String
        Get
            VolumeflowSymbol = _volumeflowSymbol
        End Get
        Set(ByVal Value As String)
            If _volumeflowSymbol <> Value Then
                _volumeflowSymbol = Value
                VisibilityLabels()
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property VolumeflowUnit() As String
        Get
            VolumeflowUnit = _volumeflowUnit
        End Get
        Set(ByVal Value As String)
            If _volumeflowUnit <> Value Then
                _volumeflowUnit = Value
                IconChange()
                Me.Invalidate()
            End If
        End Set
    End Property
#End Region

#Region "Subs"
    Private Sub VisibilityLabels()
        If _pressureSymbol = "" Then
            lbl_Pressure.Visible = False
        Else
            lbl_Pressure.Visible = True
        End If
        If _volumeflowSymbol = "" Then
            lbl_Volumeflow.Visible = False
        Else
            lbl_Volumeflow.Visible = True
            If _pressureSymbol = "" Then
                lbl_Volumeflow.Top = 4
            Else
                lbl_Volumeflow.Top = 16
            End If
        End If
    End Sub

    Private Sub IconChange()
        Me.lblValue.Text = Control.ToString("F") & " " & Unit
        Me.lbl_Pressure.Text = Pressure.ToString("F") & " " & PressureUnit
        If Volumeflow >= 1000 Then
            Me.lbl_Volumeflow.Text = Volumeflow.ToString("F0") & " " & VolumeflowUnit
        ElseIf Volumeflow >= 100 Then
            Me.lbl_Volumeflow.Text = Volumeflow.ToString("F1") & " " & VolumeflowUnit
        Else
            Me.lbl_Volumeflow.Text = Volumeflow.ToString("F") & " " & VolumeflowUnit
        End If
        If Control > 0 Then
            If FanType = FanType.Axial Then
                PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.sensor_fan_axial_on
            Else
                PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.sensor_fan_radial_on
            End If
        Else
            If FanType = FanType.Axial Then
                PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.sensor_fan_axial
            Else
                PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.sensor_fan_radial
            End If
        End If
    End Sub
#End Region

#Region "Form Events"
    Private Sub Sensor_Base_UnitChanged(NewStatus As String) Handles Me.UnitChanged
        IconChange()
    End Sub

    Private Sub TimerPoll_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        If DesignMode = False And ADS.Connected Then
            Control = CDec(ADS.getSymbolValueCached(Symbol, PollRate))
            If PressureSymbol <> "" Then
                Pressure = CDec(ADS.getSymbolValueCached(PressureSymbol, PollRate))
            End If
            If VolumeflowSymbol <> "" Then
                Volumeflow = CDec(ADS.getSymbolValueCached(VolumeflowSymbol, PollRate))
            End If
        End If
    End Sub

    Private Sub Sensor_Fan_Load(sender As Object, e As EventArgs) Handles Me.Load
        If PollRate > 0 And DesignMode = False Then
            TimerPoll.Interval = PollRate
            TimerPoll.Start()
        End If
        IconChange()
        VisibilityLabels()
    End Sub
#End Region

End Class