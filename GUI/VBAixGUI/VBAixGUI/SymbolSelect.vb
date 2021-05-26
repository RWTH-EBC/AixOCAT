Imports TwinCAT.Ads
Imports TwinCAT.Ads.TypeSystem
Imports TwinCAT.TypeSystem
Imports System.Text.RegularExpressions

Public Class SymbolSelect

#Region "Properties"
    Public Include As String
    Public Exclude As String
    Public SelectedSymbols As List(Of SeriesSelection)
    Dim SupportedDataType As List(Of String) = New List(Of String)(New String() {"REAL"})
#End Region

#Region "Subs"
    Public Sub FillSelectedSymbols()
        Dim addedSymbols As List(Of String) = New List(Of String)
        For Each row As DataGridViewRow In DataGridView1.Rows
            addedSymbols.Add(row.Cells("ColSymbol").Value)
        Next
        For Each selectedSymbol As SeriesSelection In SelectedSymbols
            If Not addedSymbols.Contains(selectedSymbol.Symbol) Then
                Dim newRow As String() = New String() {selectedSymbol.Symbol, selectedSymbol.Name, selectedSymbol.Unit}
                DataGridView1.Rows.Add(newRow)
            End If
        Next
        DataGridView1.Sort(DataGridView1.Columns("ColSymbol"), System.ComponentModel.ListSortDirection.Ascending)
    End Sub

    Private Sub UpdateListBox()
        Include = txtBoxInclude.Text
        Exclude = txtBoxExclude.Text
        ListBox1.Items.Clear()
        If ADS.Connected Then
            For Each symbol As Symbol In ADS.getSymbols()
                If SupportedDataType.Contains(symbol.DataType.Name) Then
                    If ((Include <> "" And Regex.IsMatch(symbol.InstancePath, Include, RegexOptions.IgnoreCase)) Or Include = "") And ((Exclude <> "" And Not Regex.IsMatch(symbol.InstancePath, Exclude, RegexOptions.IgnoreCase)) Or Exclude = "") Then
                        ListBox1.Items.Add(symbol.InstancePath)
                    End If
                End If
            Next
            ListBox1.Enabled = True
            If Me.ListBox1.Items.Count > 0 Then
                Me.ListBox1.SelectedIndex = 0
            End If
        Else
            ListBox1.Items.Add("PLC not connected!")
            ListBox1.Enabled = False
        End If
    End Sub
#End Region

#Region "Form Events"
    Private Sub Symbole_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Include <> "" Then
            txtBoxInclude.Text = Include
        End If
        If Exclude <> "" Then
            txtBoxExclude.Text = Exclude
        End If
        If SelectedSymbols Is Nothing Then
            SelectedSymbols = New List(Of SeriesSelection)
        End If
        UpdateListBox()
        FillSelectedSymbols()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnAssignSymbols.Click
        DataGridView1.Sort(DataGridView1.Columns("ColSymbol"), System.ComponentModel.ListSortDirection.Ascending)
        SelectedSymbols.Clear()
        For Each row As DataGridViewRow In DataGridView1.Rows
            SelectedSymbols.Add(New SeriesSelection(row.Cells("ColDisplayname").Value, row.Cells("ColSymbol").Value, row.Cells("ColUnit").Value))
        Next
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        UpdateListBox()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim addedSymbols As List(Of String) = New List(Of String)
        For Each row As DataGridViewRow In DataGridView1.Rows
            addedSymbols.Add(row.Cells("ColSymbol").Value)
        Next
        For Each selectedSymbol In ListBox1.SelectedItems
            If Not addedSymbols.Contains(selectedSymbol.ToString()) Then
                Dim newRow As String() = New String() {selectedSymbol.ToString(), selectedSymbol.ToString()}
                DataGridView1.Rows.Add(newRow)
            End If
        Next
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Dim removeIndices As List(Of Integer) = New List(Of Integer)
        For Each row As DataGridViewRow In DataGridView1.SelectedRows
            DataGridView1.Rows.Remove(row)
        Next
        SelectedSymbols.Clear()
        For Each row As DataGridViewRow In DataGridView1.Rows
            SelectedSymbols.Add(New SeriesSelection(row.Cells("ColDisplayname").Value, row.Cells("ColSymbol").Value, row.Cells("ColUnit").Value))
        Next
    End Sub
#End Region
End Class