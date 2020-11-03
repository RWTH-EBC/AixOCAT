Imports EBC_ADS_Bib.ADS
Imports TwinCAT.Ads
Imports System.ComponentModel

Public Class ADS_Connection

#Region "Events"

#End Region

#Region "Properties"
    <Browsable(True), Description("NetID")> Dim _netid As String = "x.xx.xxx.xxx.x.x"
    <Browsable(True), Description("Port"), DefaultValue(851)> Dim _port As Integer = 851
#End Region

#Region "Property Functions"
    Public Property NetID() As String
        Get
            NetID = _netid
        End Get
        Set(ByVal Value As String)
            If _netid <> Value Then
                _netid = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property Port() As Integer
        Get
            Port = _port
        End Get
        Set(ByVal Value As Integer)
            If _port <> Value Then
                _port = Value
                Me.Invalidate()
            End If
        End Set
    End Property
#End Region

#Region "Subs"
    Private Sub TestConnection()
        If NetID <> "x.xx.xxx.xxx.x.x" Then
            ADS.connect(NetID, Port)
        End If
        ChangeIcon()
    End Sub

    Public Sub ChangeIcon()
        If ADS.Connected Then
            ToolStripMenuItemNetId.Text = "NetId: " & NetID
            ToolStripMenuItemPort.Text = "Port: " & Port
            lblStatus.BackColor = System.Drawing.Color.FromArgb(87, 171, 39)
        Else
            ToolStripMenuItemNetId.Text = "NetId: ?"
            ToolStripMenuItemPort.Text = "Port: ?"
            lblStatus.BackColor = System.Drawing.Color.FromArgb(204, 7, 30)
        End If
    End Sub

#End Region

#Region "Form Events"
    Private Sub TestConnectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestConnectionToolStripMenuItem.Click
        TestConnection()
    End Sub

    Private Sub ADS_Connection_Load(sender As Object, e As EventArgs) Handles Me.Load
        TestConnection()
        TimerPoll.Interval = 1000
        TimerPoll.Start()
    End Sub

    Private Sub TimerPoll_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        If ADS.CheckConnection() Then
            ChangeIcon()
        Else
            TestConnection()
        End If
    End Sub
#End Region

End Class
