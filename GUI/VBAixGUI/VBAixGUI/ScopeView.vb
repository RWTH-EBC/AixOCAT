Imports TwinCAT.Scope2.View.ScopeViewControlLib
Imports TwinCAT.Scope2.Communications
Imports TwinCAT.Ads
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.IO

Public Class ScopeView
    <Browsable(True), Description("Konfigurationsdatei *.tcsope")> _
    Dim _configfile As String
    Public Property Konfigurationsdatei() As String

        Get
            Konfigurationsdatei = _configfile
        End Get
        Set(ByVal Value As String)
            If _configfile <> Value Then
                _configfile = Value
                Me.Invalidate()
            End If
        End Set
    End Property


    <Browsable(True), Description("Autostart")> _
    Dim mAutostart As Boolean
    Public Property Autostart() As Boolean

        Get
            Autostart = mAutostart
        End Get
        Set(ByVal Value As Boolean)
            If mAutostart <> Value Then
                mAutostart = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    Private Sub ToolStripMenuItemKonfigAuswahlen_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemKonfigAuswahlen.Click
        Dim res As DialogResult

        res = OpenFileDialog1.ShowDialog
        If res = DialogResult.OK Then
            Konfigurationsdatei = OpenFileDialog1.FileName
            KonfigLaden(Konfigurationsdatei)

        End If
    End Sub


    Private Sub KonfigLaden(ByVal Dateiname As String)
        Try
            ' alte Konfiguration löschen
            While ScopeViewControl1.Charts.Count > 0

                ScopeViewControl1.DeleteChart(Me.ScopeViewControl1.Charts(0))

            End While

            ' Konfiguration laden
            ScopeViewControl1.LoadScopeConfig(Dateiname)
            Dim channel As ScopeViewControlChannel
            For Each channel In ScopeViewControl1.ConnectedChannels
                'channel.Acquisition.AmsNetId = TwinCAT.Ads.AmsNetId.Parse("5.25.57.202.1.1")
            Next
            Me.ScopeViewControl1.BackColor = System.Drawing.SystemColors.ControlDark
        Catch ex As Exception
            Me.ScopeViewControl1.BackColor = System.Drawing.Color.Firebrick
        End Try
    End Sub

    Private Sub ToolStripMenuItemKanfigNeuLaden_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemKanfigNeuLaden.Click
        KonfigLaden(Konfigurationsdatei)
    End Sub
    Private Sub Starten()
        Try

            ' alte Daten verwerfen
            If (ScopeViewControl1.State = ScopeViewControlStates.REPLY) Then
                ScopeViewControl1.Operating.DisConnect(False)
            End If

            'Aufnahme starten
            If (ScopeViewControl1.State = ScopeViewControlStates.CONFIG) Then
                ScopeViewControl1.Operating.StartRecord()
            End If

            'alle Charts starten
            If (ScopeViewControl1.State = ScopeViewControlStates.CONNECTED) Then
                ScopeViewControl1.Operating.StartAllDisplays()
            End If

        Catch ex As Exception
            MsgBox(ex)
            ' Throw ex
        End Try
    End Sub
    Private Sub StartenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartenToolStripMenuItem.Click
        Starten()
    End Sub

    Private Sub StopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StopToolStripMenuItem.Click
        Try

            If ScopeViewControl1.State = ScopeViewControlStates.RECORD Then
                ScopeViewControl1.Operating.StopRecord()
            End If
        Catch ex As Exception


        End Try
    End Sub

    Private Sub SpeichernUnterToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SpeichernUnterToolStripMenuItem1.Click
        Dim res As DialogResult
        res = SaveFileDialog1.ShowDialog
        If res = vbOK Then
            RecordSpeichern(SaveFileDialog1.FileName)
        End If
    End Sub

    Private Sub RecordSpeichern(ByVal Dateiname)
        Try

            ' wenn Daten da sind speichern
            ' If ScopeViewControl1.State = ScopeViewControlStates.REPLY Then

            File.Create(Dateiname).Close()
            ScopeViewControl1.Operating.SaveData(Dateiname)

            '' sonst nur die Konfiguration speichern
            'Else

            'File.Create(Dateiname).Close()
            'ScopeViewControl1.SaveScopeConfig(Dateiname)
            'End If

        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Sub

    Private Sub ScopeView_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Konfigurationsdatei = "" Then
            'Keine Konfig, kein Autostart
        Else
            If Autostart = True Then
                Starten()
            End If
        End If
    End Sub
End Class
