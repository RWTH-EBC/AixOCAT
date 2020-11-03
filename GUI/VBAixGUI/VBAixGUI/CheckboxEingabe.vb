Option Strict Off

Imports System.ComponentModel
Public Class CheckboxEingabe
    <Browsable(True), Description("Symbolname")> _
    Dim Symbolname_text As String
    Public Property Symbolname() As String

        Get
            Symbolname = Symbolname_text
        End Get
        Set(ByVal Value As String)
            If Symbolname_text <> Value Then
                Symbolname_text = Value
                Me.Invalidate()
            End If
        End Set
    End Property


    <Browsable(True), Description("Dispalytext")> _
    Dim display_text As String
    Public Property Displaytext() As String

        Get
            Displaytext = display_text
        End Get
        Set(ByVal Value As String)
            If display_text <> Value Then
                display_text = Value
                Me.CheckBox1.Text = display_text
                Me.Invalidate()
            End If
        End Set
    End Property


    <Browsable(True), Description("Hinweis")> _
    Dim Hinweis_text As String
    Public Property Hinweis() As String

        Get
            Hinweis = Hinweis_text
        End Get
        Set(ByVal Value As String)
            If Hinweis_text <> Value Then
                Hinweis_text = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True), Description("SollWertNeu")> _
    Dim mSollWertNeu As Boolean
    Public Property SollWertNeu() As Boolean

        Get
            SollWertNeu = mSollWertNeu
        End Get
        Set(ByVal Value As Boolean)
            If mSollWertNeu <> Value Then
                mSollWertNeu = Value
                Me.Invalidate()
            End If
        End Set
    End Property
    <Browsable(True), Description("manueller Modus aktiv")> _
    Dim mmanuModus As Boolean = True
    Public Property manuellerModus() As Boolean

        Get
            manuellerModus = mmanuModus
        End Get
        Set(ByVal Value As Boolean)
            If mmanuModus <> Value Then
                mmanuModus = Value

                CheckBox1.Enabled = mmanuModus

                Me.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True), Description("Wert")> _
    Dim mWert As Boolean
    Public Property Wert() As Boolean

        Get
            Wert = mWert
        End Get
        Set(ByVal Value As Boolean)
            If mWert <> Value Then
                mWert = Value
                CheckBox1.Checked = Wert
                SollWertNeu = True
                Me.Invalidate()
            End If
        End Set
    End Property


    Private Sub Checkbox_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Wert = CheckBox1.Checked
        SollWertNeu = True
        RaiseEvent Wert_Changed(sender, e)
    End Sub


    Public Event Wert_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)


    <Browsable(True), Description("Aktuallisierungsrate in ms")> _
    Dim aktRate As Integer
    Public Property Aktuallisierungsrate() As Integer

        Get
            Aktuallisierungsrate = aktRate
        End Get
        Set(ByVal Value As Integer)
            If aktRate <> Value Then
                If Value <= 0 Then
                    Timer1.Stop()
                    aktRate = 0
                ElseIf Value > 0 Then
                    Timer1.Start()
                    Timer1.Interval = Value
                    aktRate = Value
                Else
                End If


                Me.Invalidate()

            End If
        End Set
    End Property

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If DesignMode = False Then
            If SollWertNeu = True Then
                ADS.setSymboleValue(Symbolname, CBool(Wert))
                SollWertNeu = False
            Else
                Wert = CDec(ADS.getSymbolValue(Symbolname))
            End If
        End If


    End Sub


End Class
