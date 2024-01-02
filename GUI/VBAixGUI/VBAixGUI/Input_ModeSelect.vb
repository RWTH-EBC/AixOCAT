Option Strict Off

Imports System.ComponentModel

Public Class Input_ModeSelect

#Region "Events"
    Public Event ModeChanged()
    Public Event SelectedIndexChanged()
#End Region

#Region "Properties"
    <Browsable(True), Description("Active")> Dim _active As Boolean = True
    <Browsable(True), Description("Hint")> Dim _hint As String
    <Browsable(True), Description("Mode")> Dim _mode As Integer
    <Browsable(True), Description("Poll Rate")> Dim _pollRate As Integer = 1000
    <Browsable(True), Description("new set value")> Dim _setValueNew As Boolean
    <Browsable(True), Description("Symbol")> Dim _symbol As String
    Dim _ClassList As New List(Of TcEnum)
#End Region

#Region "Property Functions"

    Public Property Active() As Boolean
        Get
            Active = _active
        End Get
        Set(ByVal Value As Boolean)
            If _active <> Value Then
                _active = Value
                Me.ComboBox1.Enabled = _active
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property Hint() As String
        Get
            Hint = _hint
        End Get
        Set(ByVal Value As String)
            If _hint <> Value Then
                _hint = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property Mode() As Integer
        Get
            Mode = _mode
        End Get
        Set(ByVal Value As Integer)
            If _mode <> Value Then
                _mode = Value
                For m = 0 To Me.ComboBox1.Items.Count - 1
                    If getInt(ComboBox1.Items(m)) = Mode Then
                        Me.ComboBox1.SelectedIndex = m
                        Exit For
                    End If
                Next
                RaiseEvent ModeChanged()
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property PollRate() As Integer
        Get
            PollRate = _pollRate
        End Get
        Set(ByVal Value As Integer)
            If _pollRate <> Value Then
                If Value <= 0 Then
                    TimerPoll.Stop()
                    _pollRate = 0
                ElseIf Value > 0 Then
                    TimerPoll.Start()
                    TimerPoll.Interval = Value
                    _pollRate = Value
                Else
                End If
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property SetValueNew() As Boolean
        Get
            SetValueNew = _setValueNew
        End Get
        Set(ByVal Value As Boolean)
            If _setValueNew <> Value Then
                _setValueNew = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property Symbol() As String
        Get
            Symbol = _symbol
        End Get
        Set(ByVal Value As String)
            If _symbol <> Value Then
                _symbol = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property TcEnumList As List(Of TcEnum)
        Set(ByVal value As List(Of TcEnum))
            _ClassList = value
        End Set
        Get
            Return _ClassList
        End Get
    End Property
#End Region

#Region "Subs"
    Private Function getInt(ByVal ComboboxText As String)
        Dim n As Integer
        Dim a As String
        a = ComboboxText
        a = a.Remove(a.IndexOf("]"), (a.Length - a.IndexOf("]")))
        a = a.Replace("[", "")
        n = CInt(a)
        Return n
    End Function
#End Region

#Region "Form Events"
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Focused = True Then
            SetValueNew = True
            Mode = getInt(Me.ComboBox1.SelectedItem)
        End If
        RaiseEvent SelectedIndexChanged()
    End Sub

    Private Sub ModeSelect_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim m As TcEnum
        ComboBox1.Items.Clear()
        For Each m In Me.TcEnumList
            Me.ComboBox1.Items.Add("[" & m.IntWert & "] " & m.Anzeigename)
        Next
        Mode = -1
        If Me.TcEnumList.Count > 0 Then
            Mode = TcEnumList.Item(0).IntWert
            If PollRate > 0 Then
                TimerPoll.Interval = PollRate
                TimerPoll.Start()
            End If
        End If
    End Sub

    Private Sub TimerPoll_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        If DesignMode = False Then
            If SetValueNew = True Then
                Try
                    Dim m As TcEnum
                    For Each m In Me.TcEnumList
                        If m.IntWert = Mode Then
                            ADS.setSymboleValue(Symbol, Convert.ToInt16(m.IntWert))
                            Exit For
                        End If
                    Next
                    SetValueNew = False
                Catch ex As Exception
                Finally
                    SetValueNew = False
                End Try
            Else
                Mode = CInt(ADS.getSymbolValueCached(Symbol, PollRate))
            End If
        End If
    End Sub
#End Region

End Class

<TypeConverter(GetType(ExpandableObjectConverter))> _
Public Class TcEnum
    Public Property Anzeigename As String
    Public Property TcEnumName As String
    Public Property IntWert As Integer
    Public Event TcEnumChanged As EventHandler

    Sub New()
        RaiseEvent TcEnumChanged(Me, New EventArgs)
    End Sub

    Sub New(ByVal Anzeigename As String, ByVal TcEnumName As String, ByVal IntWert As Integer)
        Me.Anzeigename = Anzeigename
        Me.TcEnumName = TcEnumName
        Me.IntWert = IntWert
    End Sub
End Class
