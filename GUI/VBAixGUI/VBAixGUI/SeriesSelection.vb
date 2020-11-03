Public Class SeriesSelection
    Public Property Name As String
    Public Property Symbol As String
    Public Property Unit As String

    Sub New()

    End Sub

    Sub New(ByVal _name As String, ByVal _symbol As String)
        Name = _name
        Symbol = _symbol
    End Sub

    Sub New(ByVal _name As String, ByVal _symbol As String, _unit As String)
        Name = _name
        Symbol = _symbol
        Unit = _unit
    End Sub
End Class