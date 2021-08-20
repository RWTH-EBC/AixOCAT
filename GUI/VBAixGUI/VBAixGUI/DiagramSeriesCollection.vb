Public Class DiagramSeriesCollection
    Public Property Name As String
    Public Property xValue_Symbol As String
    Public Property yValue_Symbol As String
    Public Property MarkerSize As Integer = 10
    Public Property MarkerColor As System.Drawing.Color = System.Drawing.Color.Red
    Public Property MarkerStyle As DataVisualization.Charting.MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
    'Public Property ConnectionType As System.Windows.Forms.DataVisualization.Charting.SeriesChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point

    Sub New()

    End Sub

    Sub New(ByVal _name As String, ByVal _xValue_symbol As String, ByVal _yValue_symbol As String, ByVal _markerSize As Integer, ByVal _markerStyle As DataVisualization.Charting.MarkerStyle, ByVal _markerColor As System.Drawing.Color, ByVal _connectionType As System.Windows.Forms.DataVisualization.Charting.SeriesChartType)
        Name = _name
        xValue_Symbol = _xValue_symbol
        yValue_Symbol = _yValue_symbol
        MarkerSize = _markerSize
        MarkerStyle = _markerStyle
        MarkerColor = _markerColor
        'ConnectionType = _connectionType
    End Sub
End Class
