Public Class Buffer
    Dim _average As Decimal
    Dim _buffer(99) As Decimal
    Dim _bufferIndex As Integer = 0
    Dim _init As Boolean = False
    Dim _absDeviation As Decimal
    Dim _relDeviation As Decimal
    Dim _max As Decimal
    Dim _min As Decimal
    Dim _statisticsCounter As Integer = 0
    Dim _statisticsTrigger As Integer = 10

#Region "Constructors"
    Public Sub New()

    End Sub

    Public Sub New(_size As Integer)
        Me._statisticsTrigger = CInt(_size / 10)
        ReDim _buffer(_size - 1)
    End Sub

    Public Sub New(_size As Integer, _statisticsTrigger As Integer)
        Me._statisticsTrigger = _statisticsTrigger
        ReDim _buffer(_size - 1)
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property Average() As Decimal
        Get
            Average = _average
        End Get
    End Property

    Public ReadOnly Property AbsDeviation() As Decimal
        Get
            AbsDeviation = _absDeviation
        End Get
    End Property

    Public ReadOnly Property Maximum() As Decimal
        Get
            Maximum = _max
        End Get
    End Property

    Public ReadOnly Property Minimum() As Decimal
        Get
            Minimum = _min
        End Get
    End Property

    Public ReadOnly Property RelDeviation() As Decimal
        Get
            RelDeviation = _relDeviation
        End Get
    End Property

    Public ReadOnly Property Size() As Decimal
        Get
            Size = UBound(_buffer) + 1
        End Get
    End Property

    Public ReadOnly Property Value() As Decimal
        Get
            Value = _buffer(_bufferIndex)
        End Get
    End Property
#End Region

#Region "Private Subs"
    Private Sub EvaluateStatistics()
        _absDeviation = 0
        _relDeviation = 0
        _max = _buffer(0)
        _min = _buffer(0)
        For i = 0 To UBound(_buffer)
            _absDeviation = Math.Max(Math.Abs(_absDeviation), Math.Abs(_buffer(i) - Average))
            _max = Math.Max(_max, _buffer(i))
            _min = Math.Max(_min, _buffer(i))
            If Average <> 0 Then
                _relDeviation = Math.Max(Math.Abs(_relDeviation), Math.Abs((_buffer(i) - Average) / Average))
            Else
                _relDeviation = Math.Max(Math.Abs(_relDeviation), 0)
            End If
        Next
    End Sub
#End Region

#Region "Public Subs"
    Public Sub AddValue(_value As Decimal)
        If _init Then
            If _bufferIndex > UBound(_buffer) Then
                _bufferIndex = 0
            End If
            _average = _average + (_value - Value) / Size
            _buffer(_bufferIndex) = _value
            _bufferIndex = _bufferIndex + 1
            _statisticsCounter = _statisticsCounter + 1
            If _statisticsCounter > _statisticsTrigger And _statisticsTrigger > 0 Then
                EvaluateStatistics()
                _statisticsCounter = 0
            End If
        ElseIf UBound(_buffer) > 0 Then
            Initialize(_value)
        End If
    End Sub

    Public Sub Initialize(ByVal _initialValue As Decimal)
        For i = 0 To UBound(_buffer)
            _buffer(i) = _initialValue
        Next
        _average = _initialValue
        _init = True
    End Sub

    Public Function GetValue(ByVal index As Integer) As Decimal
        If index <= UBound(_buffer) Then
            Return _buffer(index)
        Else
            Return (-1)
        End If
    End Function

    Public Function GetValues() As Decimal()
        Return _buffer
    End Function
#End Region

End Class
