<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SymbolSelect
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.btnAssignSymbols = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.txtBoxInclude = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtBoxExclude = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ColSymbol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDisplayname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColUnit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.HorizontalScrollbar = True
        Me.ListBox1.Location = New System.Drawing.Point(9, 4)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox1.Size = New System.Drawing.Size(200, 303)
        Me.ListBox1.Sorted = True
        Me.ListBox1.TabIndex = 0
        '
        'btnAssignSymbols
        '
        Me.btnAssignSymbols.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAssignSymbols.Location = New System.Drawing.Point(326, 380)
        Me.btnAssignSymbols.Name = "btnAssignSymbols"
        Me.btnAssignSymbols.Size = New System.Drawing.Size(200, 23)
        Me.btnAssignSymbols.TabIndex = 1
        Me.btnAssignSymbols.Text = "Symbole zuweisen"
        Me.btnAssignSymbols.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.Location = New System.Drawing.Point(9, 380)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(200, 23)
        Me.btnUpdate.TabIndex = 3
        Me.btnUpdate.Text = "Aktualisieren"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'txtBoxInclude
        '
        Me.txtBoxInclude.Location = New System.Drawing.Point(69, 316)
        Me.txtBoxInclude.Margin = New System.Windows.Forms.Padding(2)
        Me.txtBoxInclude.Name = "txtBoxInclude"
        Me.txtBoxInclude.Size = New System.Drawing.Size(140, 20)
        Me.txtBoxInclude.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 318)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Include"
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Location = New System.Drawing.Point(215, 155)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(105, 23)
        Me.btnAdd.TabIndex = 6
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemove.Location = New System.Drawing.Point(215, 185)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(105, 23)
        Me.btnRemove.TabIndex = 7
        Me.btnRemove.Text = "Remove"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 342)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Exclude"
        '
        'txtBoxExclude
        '
        Me.txtBoxExclude.Location = New System.Drawing.Point(69, 340)
        Me.txtBoxExclude.Margin = New System.Windows.Forms.Padding(2)
        Me.txtBoxExclude.Name = "txtBoxExclude"
        Me.txtBoxExclude.Size = New System.Drawing.Size(140, 20)
        Me.txtBoxExclude.TabIndex = 8
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColSymbol, Me.ColDisplayname, Me.ColUnit})
        Me.DataGridView1.Location = New System.Drawing.Point(326, 4)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 20
        Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DataGridView1.Size = New System.Drawing.Size(446, 303)
        Me.DataGridView1.TabIndex = 10
        '
        'ColSymbol
        '
        Me.ColSymbol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ColSymbol.HeaderText = "Symbol"
        Me.ColSymbol.Name = "ColSymbol"
        '
        'ColDisplayname
        '
        Me.ColDisplayname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ColDisplayname.HeaderText = "Displayname"
        Me.ColDisplayname.Name = "ColDisplayname"
        '
        'ColUnit
        '
        Me.ColUnit.HeaderText = "Unit"
        Me.ColUnit.Name = "ColUnit"
        '
        'SymbolSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 415)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtBoxExclude)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtBoxInclude)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnAssignSymbols)
        Me.Controls.Add(Me.ListBox1)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(900, 454)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(323, 414)
        Me.Name = "SymbolSelect"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Symbole auswählen"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents btnAssignSymbols As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents txtBoxInclude As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnRemove As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents txtBoxExclude As TextBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents ColSymbol As DataGridViewTextBoxColumn
    Friend WithEvents ColDisplayname As DataGridViewTextBoxColumn
    Friend WithEvents ColUnit As DataGridViewTextBoxColumn
End Class
