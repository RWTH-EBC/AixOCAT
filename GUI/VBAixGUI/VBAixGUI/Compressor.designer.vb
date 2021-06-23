<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Compressor
    Inherits Sensor_Base_Component

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
        Me.SuspendLayout()
        '
        'Sensor_Volumeflow
        '

        Me.lblValue.Top = 30
        Me.lblValue.Left = 20
        Me.lblValue.BackColor = Color.Transparent
        Me.lblValue.AutoSize = True
        Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.Verdichter_aus
        Me.PictureBoxIcon.Image.RotateFlip(RotateFlipType.RotateNoneFlipX)
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Name = "Sensor_Coriolis"
        Me.ResumeLayout(False)


    End Sub

End Class
