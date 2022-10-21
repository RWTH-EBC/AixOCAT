Option Strict On

Imports System.ComponentModel
Imports System.IO

Public Class Measurement

#Region "Events"

#End Region

#Region "Properties"
    <Browsable(True), Description("Measurement duration / s")> Dim _duration As Integer = 10
    <Browsable(True), Description("Exclude Filter")> Dim _excludeFilter As String
    <Browsable(True), Description("Include Filter")> Dim _includeFilter As String
    <Browsable(True), Description("Measurement identifier")> Dim _measurementIdentifier As String = "HighlyScientifcMeasurement"
    <Browsable(True), Description("Measurement resolution / ms")> Dim _resolution As Integer = 100
    <Browsable(True), Description("Save folder")> Dim _saveFolder As String

    Private _symbolList As New List(Of SeriesSelection)

    Private _startTime As DateTime
    Private _symbolBuffers As Dictionary(Of String, Buffer)
    Private _oldInterval As Integer
    Private _elapsed As Integer
    Private _adjustedInterval As Integer
    Private _numMeasurements As Integer
    Private _loggedPoints As Integer
    Private _running As Boolean
    Private _saveName As String
    Private _saveLocation As String
#End Region

#Region "Property Functions"

    Public Property Duration() As Integer
        Get
            Duration = _duration
        End Get
        Set(ByVal Value As Integer)
            If _duration <> Value Then
                _duration = Value
                numUpDownDuration.Value = _duration
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property ExcludeFilter As String
        Get
            ExcludeFilter = _excludeFilter
        End Get
        Set(Value As String)
            _excludeFilter = Value
        End Set
    End Property

    Public Property IncludeFilter As String
        Get
            IncludeFilter = _includeFilter
        End Get
        Set(Value As String)
            _includeFilter = Value
        End Set
    End Property

    Public Property MeasurementIdentifier As String
        Get
            MeasurementIdentifier = _measurementIdentifier
        End Get
        Set(Value As String)
            _measurementIdentifier = Value
            txtBoxMeasurementName.Text = _measurementIdentifier
        End Set
    End Property

    Public Property Resolution() As Integer
        Get
            Resolution = _resolution
        End Get
        Set(ByVal Value As Integer)
            If _resolution <> Value Then
                If Value <= 0 Then
                    _resolution = 0
                ElseIf Value > 0 Then
                    _resolution = Value
                End If
                numUpDownResolution.Value = _resolution
                Me.Invalidate()
            End If
        End Set
    End Property

    Public ReadOnly Property Running() As Boolean
        Get
            Running = _running
        End Get
    End Property

    Public ReadOnly Property SymbolBuffers As Dictionary(Of String, Buffer)
        Get
            SymbolBuffers = _symbolBuffers
        End Get
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property SymbolList As List(Of SeriesSelection)
        Set(ByVal Value As List(Of SeriesSelection))
            _symbolList = Value
            UpdateDataGrid()
        End Set
        Get
            Return _symbolList
        End Get
    End Property
#End Region

#Region "Subs"
    Public Sub UpdateDataGrid()
        If SymbolList.Count > 0 Then
            'Clear series which are no longer selected
            Dim selectedSeries As List(Of String) = New List(Of String)
            Dim removeSeries As List(Of String) = New List(Of String)
            For Each series As SeriesSelection In SymbolList
                selectedSeries.Add(series.Symbol)
            Next
            For Each column As DataGridViewColumn In DataGridView1.Columns
                If Not selectedSeries.Contains(column.Name) And column.Name <> "MeasurementName" Then
                    removeSeries.Add(column.Name)
                End If
            Next
            For Each column In removeSeries
                DataGridView1.Columns.Remove(column)
            Next
            'Add new series
            Dim seriesExists As Boolean
            Dim index As Integer : index = 1
            For Each series As SeriesSelection In SymbolList
                seriesExists = False
                Dim seriesName As String
                If series.Unit <> "" Then
                    seriesName = series.Name & " / " & series.Unit
                Else
                    seriesName = series.Name
                End If
                For Each column As DataGridViewColumn In DataGridView1.Columns
                    If column.Name = series.Symbol Then
                        column.HeaderText = seriesName
                        seriesExists = True
                        column.DisplayIndex = index
                        Exit For
                    End If
                Next
                If Not seriesExists Then
                    DataGridView1.Columns.Add(series.Symbol, seriesName)
                    DataGridView1.Columns(series.Symbol).DisplayIndex = index
                End If
                index = index + 1
            Next
            DataGridView1.Columns("MeasurementName").DisplayIndex = 0
        End If
    End Sub

    Public Function GetAveragedValue(ByVal _symbol As String) As Double
        If _symbolBuffers.ContainsKey(_symbol) Then
            Return (_symbolBuffers(_symbol).Average * _symbolBuffers(_symbol).Size / _loggedPoints)
        Else
            Return (0)
        End If
    End Function

    Private Function SetupSaveFile() As Boolean
        If _saveFolder <> "" Then
            Dim dateTimeString As String = Now.Year.ToString("D4") & Now.Month.ToString("D2") & Now.Day.ToString("D2") & "_" & Now.Hour.ToString("D2") & Now.Minute.ToString("D2") & Now.Second.ToString("D2")
            MeasurementIdentifier = txtBoxMeasurementName.Text
            _saveName = MeasurementIdentifier & "_" & dateTimeString
            _saveLocation = _saveFolder & "\" & _saveName & ".csv"
            txtBoxSaveLocation.Text = _saveLocation

            Try
                File.Create(_saveLocation).Dispose()
                lblStatus.Text = "Prepared result file."
                Return True
            Catch ex As Exception
                lblStatus.Text = "Error when creating result file: " & ex.Message
                Return False
            End Try
        Else
            Log.AddMessage(MessageType.Warning, "Measurement has no valid save location set up!")
            Return False
        End If

    End Function

    Private Sub PrepareBuffers()
        lblStatus.Text = "Preparing buffers ..."
        Dim datapointCount As Integer = CInt(Math.Ceiling(Duration * 1000.0 / Resolution))
        _symbolBuffers = New Dictionary(Of String, Buffer)
        _symbolBuffers.Add("Time", New Buffer(datapointCount, 0))
        _symbolBuffers("Time").Initialize(0)
        For Each symbol As SeriesSelection In SymbolList
            _symbolBuffers.Add(symbol.Symbol, New Buffer(datapointCount, 0))
            'initialize buffers
            _symbolBuffers(symbol.Symbol).Initialize(0)
        Next
        _loggedPoints = 0
        lblStatus.Text = "Prepared buffers with a size of " & datapointCount.ToString() & " points."
    End Sub

    Public Sub StartMeasurement()
        lblStatus.BackColor = System.Drawing.Color.FromArgb(87, 171, 39)
        If SetupSaveFile() Then
            PrepareBuffers()
        End If

        If File.Exists(_saveLocation) Then
            btnStartMeasurement.Enabled = False
            btnStopMeasurement.Enabled = True
            lblStatus.Text = "Starting measurement ..."
            _running = True
            ADS.CachedOnly = True
            TimerPoll.Interval = Resolution
            _startTime = DateTime.UtcNow
            _adjustedInterval = Resolution
            _oldInterval = Resolution
            TimerPoll.Start()
        End If
    End Sub

    Private Sub FinishMeasurement()
        lblStatus.Text = "Finishing measurement ..."
        TimerPoll.Stop()
        SaveMeasurement()
        _running = False
        ADS.CachedOnly = False
        btnStartMeasurement.Enabled = True
        btnStopMeasurement.Enabled = False
        lblStatus.Text = "Ready"
        lblStatus.BackColor = Color.Transparent
    End Sub

    Private Sub SaveMeasurement()
        lblStatus.Text = "Saving measured data ..."
        Dim _line As String
        Dim sw As StreamWriter = New StreamWriter(_saveLocation)
        _line = "Time [ms]"
        For Each symbol As SeriesSelection In SymbolList
            _line &= "," & symbol.Name & " [" & symbol.Unit & "]"
        Next
        sw.WriteLine(_line)
        For i = 0 To _loggedPoints - 1
            _line = _symbolBuffers("Time").GetValue(i).ToString()
            For Each symbol As SeriesSelection In SymbolList
                _line &= "," & CDec(_symbolBuffers(symbol.Symbol).GetValue(i)).ToString("F5", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
            Next
            sw.WriteLine(_line)
        Next
        sw.Close()
        lblStatus.Text = "Saved measurement to " & _saveLocation
        Log.AddMessage(MessageType.Information, "Saved measurement " & _saveName & " with " & _loggedPoints.ToString & " points")
        _numMeasurements = _numMeasurements + 1
        DataGridView1.Rows.Add({_saveName})
        For Each symbol As SeriesSelection In SymbolList
            DataGridView1.Rows(_numMeasurements - 1).Cells(symbol.Symbol).Value = Math.Round(GetAveragedValue(symbol.Symbol), 4)
        Next
    End Sub
#End Region

#Region "Form Events"
    Private Sub btnAssignSymbols_Click(sender As Object, e As EventArgs) Handles btnAssignSymbols.Click
        SymbolSelect.Include = IncludeFilter
        SymbolSelect.Exclude = ExcludeFilter
        SymbolSelect.SelectedSymbols = New List(Of SeriesSelection)
        For Each series As SeriesSelection In SymbolList
            SymbolSelect.SelectedSymbols.Add(New SeriesSelection(series.Name, series.Symbol, series.Unit))
        Next
        SymbolSelect.ShowDialog()

        SymbolList.Clear()
        For Each symbolSelected In SymbolSelect.SelectedSymbols
            Dim series As SeriesSelection = New SeriesSelection(symbolSelected.Name, symbolSelected.Symbol, symbolSelected.Unit)
            SymbolList.Add(series)
        Next
        UpdateDataGrid()
    End Sub

    Private Sub TimerPoll_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        _elapsed = CInt((DateTime.UtcNow - _startTime).TotalMilliseconds)
        If _elapsed >= (Duration * 1000.0) And (_symbolBuffers("Time").Size - _loggedPoints) <> 1 Then
            FinishMeasurement()
        Else
            _loggedPoints = _loggedPoints + 1
            _adjustedInterval = _adjustedInterval + CInt(((Resolution * _loggedPoints) - _elapsed) * 0.3)
            If _adjustedInterval > Resolution Then
                _adjustedInterval = Resolution
            ElseIf _adjustedInterval < 5 Then
                _adjustedInterval = 5
            End If
            If Math.Abs(_oldInterval - _adjustedInterval) / Resolution > 0.25 Then
                Log.AddMessage(MessageType.Warning, "Measurement adjusted polling rate by over 25%, current polling rate is " & _adjustedInterval & "ms")
                _oldInterval = _adjustedInterval
            End If
            TimerPoll.Interval = _adjustedInterval
            lblStatus.Text = "Measurement is running ... " & CInt((DateTime.UtcNow - _startTime).TotalSeconds).ToString & "/" & Duration & "s | " & _loggedPoints & " points logged"
            _symbolBuffers("Time").AddValue(CInt((DateTime.UtcNow - _startTime).TotalMilliseconds))
            For Each symbol As SeriesSelection In SymbolList
                _symbolBuffers(symbol.Symbol).AddValue(CDec(ADS.getSymbolValue(symbol.Symbol)))
            Next
        End If
    End Sub

    Private Sub btnSelectFolder_Click(sender As Object, e As EventArgs) Handles btnSelectFolder.Click
        FolderBrowserDialog1.ShowDialog()
        If Directory.Exists(FolderBrowserDialog1.SelectedPath) Then
            _saveFolder = FolderBrowserDialog1.SelectedPath
            txtBoxSaveLocation.Text = _saveFolder
        End If
    End Sub

    Private Sub Measurement_Load(sender As Object, e As EventArgs) Handles Me.Load
        numUpDownDuration.Value = _duration
        numUpDownResolution.Value = _resolution
        txtBoxMeasurementName.Text = _measurementIdentifier
        UpdateDataGrid()
    End Sub

    Private Sub Measurement_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        DataGridView1.Height = Me.Height - 130
    End Sub

    Private Sub btnStartMeasurement_Click(sender As Object, e As EventArgs) Handles btnStartMeasurement.Click
        StartMeasurement()
    End Sub

    Private Sub btnStopMeasurement_Click(sender As Object, e As EventArgs) Handles btnStopMeasurement.Click
        FinishMeasurement()
    End Sub

    Private Sub numUpDownDuration_ValueChanged(sender As Object, e As EventArgs) Handles numUpDownDuration.ValueChanged
        Duration = CInt(numUpDownDuration.Value)
    End Sub

    Private Sub numUpDownResolution_ValueChanged(sender As Object, e As EventArgs) Handles numUpDownResolution.ValueChanged
        Resolution = CInt(numUpDownResolution.Value)
    End Sub

#End Region
End Class
