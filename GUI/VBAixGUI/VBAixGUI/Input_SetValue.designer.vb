<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Input_SetValue
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
        Me.components = New System.ComponentModel.Container()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.lblUnit = New System.Windows.Forms.Label()
        Me.TimerPoll = New System.Windows.Forms.Timer(Me.components)
        CType(Me.NumericUpDown1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.BackColor = System.Drawing.SystemColors.Window
        Me.NumericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.NumericUpDown1.DecimalPlaces = 2
        Me.NumericUpDown1.Increment = 1
        Me.NumericUpDown1.Dock = System.Windows.Forms.DockStyle.Left
        Me.NumericUpDown1.Location = New System.Drawing.Point(0, 0)
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(105, 16)
        Me.NumericUpDown1.TabIndex = 0
        Me.NumericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumericUpDown1.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left
        '
        'lblUnit
        '
        Me.lblUnit.AutoSize = True
        Me.lblUnit.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblUnit.Location = New System.Drawing.Point(120, 0)
        Me.lblUnit.Name = "lblUnit"
        Me.lblUnit.Size = New System.Drawing.Size(14, 13)
        Me.lblUnit.TabIndex = 1
        Me.lblUnit.Text = "X"
        '
        'TimerPoll
        '
        '
        'Input_SetValue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblUnit)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Name = "Input_SetValue"
        Me.Size = New System.Drawing.Size(134, 16)
        CType(Me.NumericUpDown1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblUnit As System.Windows.Forms.Label
    Friend WithEvents TimerPoll As System.Windows.Forms.Timer

End Class
