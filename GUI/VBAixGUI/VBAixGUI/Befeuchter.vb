Option Strict On

Imports System.ComponentModel


Public Class Befeuchter


    <Browsable(True), Description("Anzeige Einheit")> _
    Dim Einheit_text As String
    Public Property Einheit() As String

        Get
            Einheit = Einheit_text
        End Get
        Set(ByVal Value As String)
            If Einheit_text <> Value Then
                Einheit_text = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True), Description("minimaler Wert der Variabel")> _
    Dim aminWert As Decimal
    Public Property minWert() As Decimal

        Get
            minWert = aminWert
        End Get
        Set(ByVal Value As Decimal)
            If aminWert <> Value Then
                aminWert = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True), Description("maximaler Wert der Variabel")> _
    Dim amaxWert As Decimal
    Public Property maxWert() As Decimal

        Get
            maxWert = amaxWert
        End Get
        Set(ByVal Value As Decimal)
            If amaxWert <> Value Then
                amaxWert = Value
                Me.Invalidate()
            End If
        End Set
    End Property
    <Browsable(True), Description("Herstellername")> _
    Dim Hersteller_text As String
    Public Property Hersteller() As String

        Get
            Hersteller = Hersteller_text
        End Get
        Set(ByVal Value As String)
            If Hersteller_text <> Value Then
                Hersteller_text = Value
                Me.Invalidate()
            End If
        End Set
    End Property
    <Browsable(True), Description("Symbolname")> _
    Dim Symbolname_text As String
    Public Property SymbolnameWert() As String

        Get
            SymbolnameWert = Symbolname_text
        End Get
        Set(ByVal Value As String)
            If Symbolname_text <> Value Then
                Symbolname_text = Value
                Me.Invalidate()
            End If
        End Set
    End Property
    <Browsable(True), Description("Symbolname Bereit")> _
    Dim BereitSymbolname_text As String
    Public Property SymbolnameBereit() As String

        Get
            SymbolnameBereit = BereitSymbolname_text
        End Get
        Set(ByVal Value As String)
            If BereitSymbolname_text <> Value Then
                BereitSymbolname_text = Value
                Me.Invalidate()
            End If
        End Set
    End Property


    <Browsable(True), Description("Model, Version, Typ")> _
    Dim Model_text As String
    Public Property Model() As String

        Get
            Model = Model_text
        End Get
        Set(ByVal Value As String)
            If Model_text <> Value Then
                Model_text = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    Dim mWert As Decimal = 0
    <Browsable(True), DefaultValue(0), _
  Description("Signal")> _
    Public Property Wert() As Decimal

        Get
            Wert = mWert
        End Get
        Set(ByVal Value As Decimal)
            If mWert <> Value Then
                mWert = Value
                mWert = Decimal.Round(mWert, 2)

                IconChange()


                Me.Invalidate()
            End If
        End Set
    End Property
    Private Sub IconChange()
        lbl_Wert.Text = Wert & " " & Einheit
        If Wert = 0 Then
            'Befeuchter aus
            Me.PictureBox1.Image = ImageList1.Images(0)
        Else
            'Befeuchter an
            If Bereit = True Then
                'und Bereit
                Me.PictureBox1.Image = ImageList1.Images(1)
            Else
                'noch nicht bereit
                Me.PictureBox1.Image = ImageList1.Images(2)
            End If

        End If
    End Sub


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
    Dim mmanuModus As Boolean
    Public Property manuellerModus() As Boolean

        Get
            manuellerModus = mmanuModus
        End Get
        Set(ByVal Value As Boolean)
            If mmanuModus <> Value Then
                mmanuModus = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True), Description("Bereit")> _
    Dim aBereit As Boolean
    Public Property Bereit() As Boolean

        Get
            Bereit = aBereit
        End Get
        Set(ByVal Value As Boolean)
            If aBereit <> Value Then
                aBereit = Value
                Me.Invalidate()
            End If
        End Set
    End Property



    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click, lbl_Wert.Click
        If manuellerModus = True Then
            Sollwert.minWert = minWert
            Sollwert.maxWert = maxWert
            Dim alterWert As Decimal
            Dim neuerWert As Decimal
            alterWert = Wert

            Sollwert.istWert = Wert
            Sollwert.ShowDialog()
            Wert = Sollwert.sollWert
            neuerWert = Wert

            If alterWert = neuerWert Then
                SollWertNeu = False
            Else
                SollWertNeu = True
            End If
        End If

    End Sub
    <Browsable(True), Description("Hinweis")> _
    Dim Hinweis_text As String
    Public Property Hinweis() As String
        Get
            Hinweis = Hinweis_text
        End Get
        Set(ByVal Value As String)
            If Hinweis_text <> Value Then
                Hinweis_text = Value
            End If
        End Set
    End Property


    Private Sub Befeuchter_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ToolTip1.SetToolTip(Me.PictureBox1, "Symbolname: " & SymbolnameWert & ControlChars.NewLine & _
                         "Hersteller:" & Hersteller & ControlChars.NewLine & _
                         "Model: " & Model & ControlChars.NewLine & _
                          "Hinweis: " & Hinweis)
        IconChange()
    End Sub
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
                ADS.setSymboleValue(SymbolnameWert, Wert)
                SollWertNeu = False
            Else
                Wert = CDec(ADS.getSymbolValue(SymbolnameWert))
            End If

            If SymbolnameBereit = "" Then
                'keine Bereit Variable gesetzt
                Bereit = True
            Else
                Bereit = CBool(ADS.getSymbolValue(SymbolnameBereit))
            End If

        End If

    End Sub
End Class
