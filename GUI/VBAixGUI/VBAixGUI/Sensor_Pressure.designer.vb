<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Sensor_Pressure
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
        'lblValue
        '
        Me.lblValue.BackColor = System.Drawing.Color.White
        Me.lblValue.Location = New System.Drawing.Point(4, 10)
        Me.lblValue.Size = New System.Drawing.Size(70, 12)
        '
        'Sensor_Pressure
        '
        Me.PictureBoxIcon.Image = Global.VBAixGUI.My.Resources.sensor_pressure_diff
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Name = "Sensor_Pressure"
        Me.Size = New System.Drawing.Size(80, 40)
        Me.ResumeLayout(False)

    End Sub

End Class
