
Option Strict On

Imports System.ComponentModel


Public Class DreiWegVentil
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
    Dim mWert As Decimal
    <Browsable(True), DefaultValue(0), _
  Description("Signal")> _
    Public Property Wert() As Decimal

        Get
            Wert = mWert
        End Get
        Set(ByVal Value As Decimal)
            mWert = Value
            mWert = Decimal.Round(mWert, 2)
            lbl_Wert.Text = mWert & " " & Einheit


            'If mWert = minWert Then
            '    Me.PictureBox1.Image = ImageList1.Images(0)
            'ElseIf mWert > 0 And mWert < maxWert Then
            '    Me.PictureBox1.Image = ImageList1.Images(1)
            'ElseIf mWert = maxWert Then
            '    Me.PictureBox1.Image = ImageList1.Images(2)
            'End If


            Select Case Ausrichtung
                Case Orientation.Left
                    If mWert = 0 Then
                        Me.PictureBox1.Image = ImageList1.Images(3)
                    ElseIf mWert > 0 And mWert < maxWert Then
                        Me.PictureBox1.Image = ImageList1.Images(1)
                    ElseIf mWert = maxWert Then
                        Me.PictureBox1.Image = ImageList1.Images(2)
                    End If
                    Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)
                Case Orientation.Right
                    If mWert = 0 Then
                        Me.PictureBox1.Image = ImageList1.Images(3)
                    ElseIf mWert > 0 And mWert < maxWert Then
                        Me.PictureBox1.Image = ImageList1.Images(1)
                    ElseIf mWert = maxWert Then
                        Me.PictureBox1.Image = ImageList1.Images(2)
                    End If
                    Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
                Case Orientation.Top
                    'noch keinen Anwendungsfall
                    Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate180FlipNone)
                Case Orientation.Bottom
                    If mWert = 0 Then
                        Me.PictureBox1.Image = ImageList1.Images(0)
                    ElseIf mWert > 0 And mWert < maxWert Then
                        Me.PictureBox1.Image = ImageList1.Images(1)
                    ElseIf mWert = maxWert Then
                        Me.PictureBox1.Image = ImageList1.Images(2)
                    End If
            End Select





            Me.Invalidate()
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



    Private Sub DreiWegVentil_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.lbl_Wert.TextAlign = ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.PictureBox1, "Symbolname: " & Symbolname & ControlChars.NewLine &
               "Hersteller:" & Hersteller & ControlChars.NewLine &
               "Model: " & Model & ControlChars.NewLine &
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
                Wert = CDec(ADS.getSymbolValue(Symbolname))
            End If
        End If

    End Sub
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
                Me.lbl_Wert.TextAlign = ContentAlignment.MiddleRight
                Select Case Ausrichtung
                    Case Orientation.Left
                        Me.PictureBox1.Image = ImageList1.Images(0)
                        Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)

                        Me.Size = New Size(93, 40)
                        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
                        Me.lbl_Wert.Location = New System.Drawing.Point(46, 10)


                    Case Orientation.Right
                        Me.PictureBox1.Image = ImageList1.Images(0)
                        Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)

                        Me.Size = New Size(40, 60)
                        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
                        Me.lbl_Wert.Location = New System.Drawing.Point(0, 40)
                        Me.lbl_Wert.TextAlign = ContentAlignment.MiddleLeft

                    Case Orientation.Top
                        Me.PictureBox1.Image = ImageList1.Images(0)
                        Me.PictureBox1.Image.RotateFlip(RotateFlipType.Rotate180FlipNone)

                        Me.Size = New Size(50, 55)
                        Me.PictureBox1.Location = New System.Drawing.Point(5, 0)
                        Me.lbl_Wert.Location = New System.Drawing.Point(0, 32)

                    Case Orientation.Bottom
                        Me.PictureBox1.Image = ImageList1.Images(0)

                        Me.Size = New Size(50, 55)
                        Me.PictureBox1.Location = New System.Drawing.Point(5, 15)
                        Me.lbl_Wert.Location = New System.Drawing.Point(0, 0)



                End Select

                RaiseEvent AusrichtungChanged(mAusrichtung)
                Me.Invalidate()
            End If
        End Set
    End Property

End Class
