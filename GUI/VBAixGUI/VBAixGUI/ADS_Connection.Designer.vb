<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ADS_Connection
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
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.TestConnectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItemNetId = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemPort = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TimerPoll = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TestConnectionToolStripMenuItem, Me.ToolStripSeparator1, Me.ToolStripMenuItemNetId, Me.ToolStripMenuItemPort})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(171, 76)
        '
        'TestConnectionToolStripMenuItem
        '
        Me.TestConnectionToolStripMenuItem.Name = "TestConnectionToolStripMenuItem"
        Me.TestConnectionToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.TestConnectionToolStripMenuItem.Text = "Verbindung testen"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(167, 6)
        '
        'ToolStripMenuItemNetId
        '
        Me.ToolStripMenuItemNetId.Name = "ToolStripMenuItemNetId"
        Me.ToolStripMenuItemNetId.Size = New System.Drawing.Size(170, 22)
        Me.ToolStripMenuItemNetId.Text = "NetId: ?"
        '
        'ToolStripMenuItemPort
        '
        Me.ToolStripMenuItemPort.Name = "ToolStripMenuItemPort"
        Me.ToolStripMenuItemPort.Size = New System.Drawing.Size(170, 22)
        Me.ToolStripMenuItemPort.Text = "Port:?"
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lblStatus.ContextMenuStrip = Me.ContextMenuStrip1
        Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblStatus.Location = New System.Drawing.Point(0, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(30, 35)
        Me.lblStatus.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Silver
        Me.Label1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 35)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "SPS"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblStatus)
        Me.SplitContainer1.Panel1MinSize = 10
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel2MinSize = 40
        Me.SplitContainer1.Size = New System.Drawing.Size(80, 35)
        Me.SplitContainer1.SplitterDistance = 30
        Me.SplitContainer1.TabIndex = 3
        '
        'TimerPoll
        '
        Me.TimerPoll.Enabled = True
        Me.TimerPoll.Interval = 1000
        '
        'ADS_Connection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "ADS_Connection"
        Me.Size = New System.Drawing.Size(80, 35)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents TestConnectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolStripMenuItemNetId As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemPort As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TimerPoll As Timer
End Class
