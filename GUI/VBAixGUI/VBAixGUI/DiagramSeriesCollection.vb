Public Class DiagramSeriesCollection
    Public Property Name As String
    Public Property xValue_Symbol As String
    Public Property yValue_Symbol As String

    Sub New()

    End Sub

    Sub New(ByVal _name As String, ByVal _xValue_symbol As String, ByVal _yValue_symbol As String)
        Name = _name
        xValue_Symbol = _xValue_symbol
        yValue_Symbol = _yValue_symbol
    End Sub
End Class
