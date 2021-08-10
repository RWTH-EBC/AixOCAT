Public Class DiagramSeriesCollection
    Public Property Name As String
    Public Property xValue_Symbol As Integer
    Public Property yValue_Symbol As Integer

    Sub New()

    End Sub

    Sub New(ByVal _name As String, ByVal _xValue_symbol As String, ByVal _yValue_symbol As String)
        Name = _name
        xValue_Symbol = _xValue_symbol
        yValue_Symbol = _yValue_symbol
    End Sub
End Class
