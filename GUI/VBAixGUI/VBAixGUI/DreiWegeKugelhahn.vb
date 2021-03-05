
Option Strict Off

Imports System.ComponentModel


Public Class DreiWegeKugelhahn
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

    Public Event BohrungChanged(ByVal NewStatus As Bohrung)

    Private mBohrung As Bohrung
    Public Property Bohrung() As Bohrung

        Get
            Bohrung = mBohrung
        End Get
        Set(ByVal Value As Bohrung)
            If mBohrung <> Value Then
                mBohrung = Value

                Select Case Bohrung
                    Case Bohrung.L
                        Select Case Wert
                            Case True
                                Me.PictureBox1.Image = ImageList1.Images(0)
                            Case False
                                Me.PictureBox1.Image = ImageList1.Images(0)
                        End Select

                    Case Bohrung.T
                        Select Case Wert
                            Case True
                                Me.PictureBox1.Image = ImageList1.Images(0)
                            Case False
                                Me.PictureBox1.Image = ImageList1.Images(0)
                        End Select
                    
                End Select

                RaiseEvent BohrungChanged(mBohrung)
                Me.Invalidate()
            End If
        End Set
    End Property


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

    <Browsable(True), Description("Symbolname True")> _
    Dim Symbolname_true_text As String
    Public Property Symbolname_true() As String

        Get
            Symbolname_true = Symbolname_true_text
        End Get
        Set(ByVal Value As String)
            If Symbolname_true_text <> Value Then
                Symbolname_true_text = Value
                Me.Invalidate()
            End If
        End Set
    End Property
    <Browsable(True), Description("Symbolname False")> _
    Dim Symbolname_false_text As String
    Public Property Symbolname_false() As String

        Get
            Symbolname_false = Symbolname_false_text
        End Get
        Set(ByVal Value As String)
            If Symbolname_false_text <> Value Then
                Symbolname_false_text = Value
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
    '<Browsable(True), Description("Anzeige Einheit")> _
    'Dim Einheit_text As String
    'Public Property Einheit() As String

    '    Get
    '        Einheit = Einheit_text
    '    End Get
    '    Set(ByVal Value As String)
    '        If Einheit_text <> Value Then
    '            Einheit_text = Value
    '            Me.Invalidate()
    '        End If
    '    End Set
    'End Property

    '<Browsable(True), Description("minimaler Wert der Variabel")> _
    'Dim aminWert As Decimal
    'Public Property minWert() As Decimal

    '    Get
    '        minWert = aminWert
    '    End Get
    '    Set(ByVal Value As Decimal)
    '        If aminWert <> Value Then
    '            aminWert = Value
    '            Me.Invalidate()
    '        End If
    '    End Set
    'End Property

    '<Browsable(True), Description("maximaler Wert der Variabel")> _
    'Dim amaxWert As Decimal
    'Public Property maxWert() As Decimal

    '    Get
    '        maxWert = amaxWert
    '    End Get
    '    Set(ByVal Value As Decimal)
    '        If amaxWert <> Value Then
    '            amaxWert = Value
    '            Me.Invalidate()
    '        End If
    '    End Set
    'End Property


    Dim mWert As Boolean
    <Browsable(True), DefaultValue(0), _
  Description("Signal")> _
    Public Property Wert() As Boolean

        Get
            Wert = mWert
        End Get
        Set(ByVal Value As Boolean)
            If mWert <> Value Then
                mWert = Value




                'Select Case Bohrung
                '    Case Bohrung.L
                '        Select Case mWert
                '            Case True
                '                Me.PictureBox1.Image = ImageList1.Images(0)
                '            Case False
                '                Me.PictureBox1.Image = ImageList1.Images(0)
                '        End Select

                '    Case Bohrung.T
                '        Select Case mWert
                '            Case True
                '                Me.PictureBox1.Image = ImageList1.Images(0)
                '            Case False
                '                Me.PictureBox1.Image = ImageList1.Images(0)
                '        End Select

                'End Select

                Me.Invalidate()
            End If
        End Set
    End Property
    Public Event AusrichtungChanged(ByVal NewStatus As Orientation)

    <Browsable(True), DefaultValue(Orientation.Left), Description("Ausrichtung des KH")>
    Private mAusrichtung As Orientation = Orientation.Bottom
    Public Property Ausrichtung() As Orientation

        Get
            Ausrichtung = mAusrichtung
        End Get
        Set(ByVal Value As Orientation)
            If mAusrichtung <> Value Then
                mAusrichtung = Value

                Select Case Ausrichtung
                    Case Orientation.Left
                        Me.PictureBox1.Image = ImageList1.Images(0)
                        Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)
                    Case Orientation.Right
                        Me.PictureBox1.Image = ImageList1.Images(0)
                        Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
                    Case Orientation.Top
                        Me.PictureBox1.Image = ImageList1.Images(0)
                        Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate180FlipNone)
                    Case Orientation.Bottom
                        Me.PictureBox1.Image = ImageList1.Images(0)
                End Select

                RaiseEvent AusrichtungChanged(mAusrichtung)
                Me.Invalidate()
            End If
        End Set
    End Property

    Dim mWert_true As Boolean
    <Browsable(True), DefaultValue(0), _
  Description("Stellung True")> _
    Public Property Stellung_true() As Boolean

        Get
            Stellung_true = mWert_true
        End Get
        Set(ByVal Value As Boolean)
            If mWert_true <> Value Then
                mWert_true = Value


                If mWert_true = True And Wert = True Then

                    Select Case Bohrung
                        Case Bohrung.L
                            Select Case Ausrichtung
                                Case Orientation.Left
                                    Me.PictureBox1.Image = ImageList1.Images(3)
                                    Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)
                                Case Orientation.Right
                                    Me.PictureBox1.Image = ImageList1.Images(3)
                                    Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
                                Case Orientation.Top
                                    Me.PictureBox1.Image = ImageList1.Images(3)
                                    Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate180FlipNone)
                                Case Orientation.Bottom
                                    Me.PictureBox1.Image = ImageList1.Images(3)
                            End Select
                        Case Bohrung.T
                            Select Case Ausrichtung
                                Case Orientation.Left
                                    Me.PictureBox1.Image = ImageList1.Images(2)
                                    Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)
                                Case Orientation.Right
                                    'Me.PictureBox1.Image = ImageList1.Images(2) Kugel Wird gedreht
                                    Me.PictureBox1.Image = ImageList1.Images(1)

                                    Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
                                Case Orientation.Top
                                    Me.PictureBox1.Image = ImageList1.Images(2)
                                    Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate180FlipNone)
                                Case Orientation.Bottom
                                    Me.PictureBox1.Image = ImageList1.Images(2)
                            End Select
                    End Select
                ElseIf mWert_false = Stellung_true Then
                    Select Case Ausrichtung
                        Case Orientation.Left
                            Me.PictureBox1.Image = ImageList1.Images(0)
                            Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)
                        Case Orientation.Right
                            Me.PictureBox1.Image = ImageList1.Images(0)
                            Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
                        Case Orientation.Top
                            Me.PictureBox1.Image = ImageList1.Images(0)
                            Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate180FlipNone)
                        Case Orientation.Bottom
                            Me.PictureBox1.Image = ImageList1.Images(0)
                    End Select
                End If


                Me.Invalidate()
            End If
        End Set
    End Property


    Dim mWert_false As Boolean
    <Browsable(True), DefaultValue(0), _
  Description("Stellung False")> _
    Public Property Stellung_false() As Boolean

        Get
            Stellung_false = mWert_false
        End Get
        Set(ByVal Value As Boolean)
            If mWert_false <> Value Then
                mWert_false = Value


                If mWert_false = True And Wert = False Then

                    Select Case Bohrung
                        Case Bohrung.L
                            Select Case Ausrichtung
                                Case Orientation.Left
                                    Me.PictureBox1.Image = ImageList1.Images(2)
                                    Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)
                                Case Orientation.Right
                                    Me.PictureBox1.Image = ImageList1.Images(2)
                                    Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
                                Case Orientation.Top
                                    Me.PictureBox1.Image = ImageList1.Images(2)
                                    Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate180FlipNone)
                                Case Orientation.Bottom
                                    Me.PictureBox1.Image = ImageList1.Images(2)
                            End Select
                        Case Bohrung.T
                            Select Case Ausrichtung
                                Case Orientation.Left
                                    Me.PictureBox1.Image = ImageList1.Images(1)
                                    Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)
                                Case Orientation.Right
                                    'Me.PictureBox1.Image = ImageList1.Images(1) Kugel wird gedreht
                                    Me.PictureBox1.Image = ImageList1.Images(3)
                                    Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
                                Case Orientation.Top
                                    Me.PictureBox1.Image = ImageList1.Images(1)
                                    Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate180FlipNone)
                                Case Orientation.Bottom
                                    Me.PictureBox1.Image = ImageList1.Images(1)
                            End Select
                    End Select
                ElseIf mWert_false = Stellung_true Then
                    Select Case Ausrichtung
                        Case Orientation.Left
                            Me.PictureBox1.Image = ImageList1.Images(0)
                            Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)
                        Case Orientation.Right
                            Me.PictureBox1.Image = ImageList1.Images(0)
                            Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
                        Case Orientation.Top
                            Me.PictureBox1.Image = ImageList1.Images(0)
                            Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate180FlipNone)
                        Case Orientation.Bottom
                            Me.PictureBox1.Image = ImageList1.Images(0)
                    End Select
                End If
                



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

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If manuellerModus = True Then


            Sollwert.minWert = 0
            Sollwert.maxWert = 1
            Sollwert.Schrittweite = 1

            Dim alterWert As Decimal
            Dim neuerWert As Decimal
            alterWert = Wert

            If Wert = True Then
                Sollwert.istWert = 1
            Else : Sollwert.istWert = 0
            End If



            Sollwert.ShowDialog()

            If Sollwert.sollWert > 0 Then
                Sollwert.sollWert = 1
            Else : Sollwert.sollWert = 0

            End If

            Wert = CDec(Sollwert.sollWert)
            neuerWert = Wert


            If alterWert = neuerWert Then
                SollWertNeu = False
            Else
                SollWertNeu = True
            End If
        End If
    End Sub



    Private Sub DreiWegVentil_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ToolTip1.SetToolTip(Me.PictureBox1, "Symbolname: " & Symbolname & ControlChars.NewLine & _
               "Hersteller:" & Hersteller & ControlChars.NewLine & _
               "Model: " & Model & ControlChars.NewLine & _
                   "Hinweis: " & Hinweis)
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
                ADS.setSymboleValue(Symbolname, Wert)
                SollWertNeu = False
            Else
                Wert = CBool(ADS.getSymbolValue(Symbolname))
                Stellung_true = CDec(ADS.getSymbolValue(Symbolname_true))
                Stellung_false = CDec(ADS.getSymbolValue(Symbolname_false))
            End If
        End If

    End Sub


End Class
