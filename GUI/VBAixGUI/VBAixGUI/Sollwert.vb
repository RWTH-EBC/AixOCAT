Public Class Sollwert
    Public maxWert As Decimal
    Public minWert As Decimal
    Public istWert As Decimal
    Public sollWert As Decimal
    Public Schrittweite As Decimal


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        sollWert = Me.NumericUpDown1.Value
        Me.Close()
    End Sub

    Private Sub Sollwert_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.NumericUpDown1.Maximum = maxWert
        Me.NumericUpDown1.Minimum = minWert
        Me.NumericUpDown1.Value = istWert
        Me.NumericUpDown1.Increment = Schrittweite
        sollWert = istWert
    End Sub

   
End Class