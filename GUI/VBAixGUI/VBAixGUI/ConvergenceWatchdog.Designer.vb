<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ConvergenceWatchdog
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.lblValue = New System.Windows.Forms.Label()
        Me.TimerPoll = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTipInfo = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblDeviation = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblValue
        '
        Me.lblValue.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblValue.Location = New System.Drawing.Point(0, 0)
        Me.lblValue.Name = "lblValue"
        Me.lblValue.Size = New System.Drawing.Size(65, 30)
        Me.lblValue.TabIndex = 2
        Me.lblValue.Text = "xxx.xx"
        Me.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TimerPoll
        '
        '
        'ToolTipInfo
        '
        Me.ToolTipInfo.AutoPopDelay = 10000
        Me.ToolTipInfo.InitialDelay = 300
        Me.ToolTipInfo.ReshowDelay = 100
        '
        'lblDeviation
        '
        Me.lblDeviation.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(7, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.lblDeviation.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblDeviation.Location = New System.Drawing.Point(65, 0)
        Me.lblDeviation.Name = "lblDeviation"
        Me.lblDeviation.Size = New System.Drawing.Size(65, 30)
        Me.lblDeviation.TabIndex = 3
        Me.lblDeviation.Text = "xxx.xx"
        Me.lblDeviation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ConvergenceWatchdog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.lblDeviation)
        Me.Controls.Add(Me.lblValue)
        Me.Name = "ConvergenceWatchdog"
        Me.Size = New System.Drawing.Size(130, 30)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblValue As Label
    Friend WithEvents TimerPoll As Timer
    Friend WithEvents ToolTipInfo As ToolTip
    Friend WithEvents lblDeviation As Label
End Class
