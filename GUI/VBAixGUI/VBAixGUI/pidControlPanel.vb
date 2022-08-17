Option Strict Off

Imports System.ComponentModel
Public Class pidControlPanel
#Region "Events"
    Public Event ModeChanged()
    Public Event SelectedIndexChanged()
#End Region

#Region "Properties"
    <Browsable(True), Description("Active")> Dim _active As Boolean = True
    <Browsable(True), Description("Hint")> Dim _hint As String
    <Browsable(True), Description("Regler")> Dim _regler As String
    <Browsable(True), Description("Poll Rate")> Dim _pollRate As Integer
    <Browsable(True), Description("new set value")> Dim _setValueNew As Boolean
    Dim _ClassList As New List(Of TcEnumPid)
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

    Public Property Regler() As String
        Get
            Regler = _regler
        End Get
        Set(ByVal Value As String)
            If _regler <> Value Then
                _regler = Value
                RaiseEvent ModeChanged()
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

    Public Property PollRate() As Integer
        Get
            PollRate = _pollRate
        End Get
        Set(ByVal Value As Integer)
            If _pollRate <> Value Then
                _pollRate = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property TcEnumList As List(Of TcEnumPid)
        Set(ByVal value As List(Of TcEnumPid))
            _ClassList = value
        End Set
        Get
            Return _ClassList
        End Get
    End Property
#End Region

#Region "Form Events"
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        SetValueNew = True
        Dim m As TcEnumPid
        For Each m In Me.TcEnumList
            If m.Anzeigename = Me.ComboBox1.SelectedItem() Then
                Regler = m.TcEnumName
                Me.Input_SetValue1.Symbol = m.TcEnumName + ".Kp"
                Me.Input_SetValue2.Symbol = m.TcEnumName + ".Ti"
                Me.Input_SetValue3.Symbol = m.TcEnumName + ".Td"
                Me.CheckboxEingabe1.Symbolname = m.TcEnumName + ".RST"
                Exit For
            End If
        Next
    End Sub

    Private Sub ReglerSelect_Load(sender As Object, e As EventArgs) Handles Me.Load, ComboBox1.SelectedIndexChanged
        If Me.ComboBox1.Items.Count = 0 Then
            Dim m As TcEnumPid
            ComboBox1.Items.Clear()
            For Each m In Me.TcEnumList
                Me.ComboBox1.Items.Add(m.Anzeigename)
            Next
            If Not Me.ComboBox1.Items.Count = 0 And Me.DesignMode Then
                Me.ComboBox1.SelectedIndex = 0
            End If
        End If
    End Sub

#End Region

End Class

<TypeConverter(GetType(ExpandableObjectConverter))>
Public Class TcEnumPid
    Public Property Anzeigename As String
    Public Property TcEnumName As String

    Public Event TcEnumPIDChanged As EventHandler

    Sub New()
        RaiseEvent TcEnumPIDChanged(Me, New EventArgs)
    End Sub

    Sub New(ByVal Anzeigename As String, ByVal TcEnumName As String)
        Me.Anzeigename = Anzeigename
        Me.TcEnumName = TcEnumName
    End Sub
End Class
