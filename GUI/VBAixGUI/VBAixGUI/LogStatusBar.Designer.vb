<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LogStatusBar
    Inherits System.Windows.Forms.ToolStripStatusLabel

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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TimerPoll = New System.Windows.Forms.Timer(Me.components)
        '
        'TimerPoll
        '
        '
        'LogStatusBar
        '
        Me.AutoSize = False
        Me.Size = New System.Drawing.Size(500, 20)
        Me.Text = "LogStatusBar"
        Me.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

    End Sub

    Friend WithEvents TimerPoll As Timer
End Class
