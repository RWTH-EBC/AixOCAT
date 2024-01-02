<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Sensor_Base_Component
    Inherits System.Windows.Forms.UserControl

    'UserControl1 überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
        Me.ToolTipInfo = New System.Windows.Forms.ToolTip(Me.components)
        Me.TimerPoll = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBoxIcon = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBoxIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblValue
        '
        Me.lblValue.BackColor = System.Drawing.Color.White
        Me.lblValue.Location = New System.Drawing.Point(4, 34)
        Me.lblValue.Name = "lblValue"
        Me.lblValue.Size = New System.Drawing.Size(72, 12)
        Me.lblValue.TabIndex = 0
        Me.lblValue.Text = "0000,00"
        Me.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ToolTipInfo
        '
        Me.ToolTipInfo.AutoPopDelay = 10000
        Me.ToolTipInfo.InitialDelay = 300
        Me.ToolTipInfo.ReshowDelay = 100
        '
        'PictureBoxIcon
        '
        Me.PictureBoxIcon.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBoxIcon.Location = New System.Drawing.Point(0, 0)
        Me.PictureBoxIcon.Name = "PictureBoxIcon"
        Me.PictureBoxIcon.Size = New System.Drawing.Size(80, 80)
        Me.PictureBoxIcon.TabIndex = 1
        Me.PictureBoxIcon.TabStop = False
        '
        'Sensor_Base_Component
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.lblValue)
        Me.Controls.Add(Me.PictureBoxIcon)
        Me.MaximumSize = New System.Drawing.Size(80, 80)
        Me.MinimumSize = New System.Drawing.Size(40, 40)
        Me.Name = "Sensor_Base_Component"
        Me.Size = New System.Drawing.Size(80, 80)
        CType(Me.PictureBoxIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblValue As System.Windows.Forms.Label
    Friend WithEvents PictureBoxIcon As System.Windows.Forms.PictureBox
    Friend WithEvents ToolTipInfo As System.Windows.Forms.ToolTip
    Friend WithEvents TimerPoll As Timer
End Class
