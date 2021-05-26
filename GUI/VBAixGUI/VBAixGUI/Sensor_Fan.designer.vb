<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Sensor_Fan
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
        Me.lbl_Pressure = New System.Windows.Forms.Label()
        Me.lbl_Volumeflow = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lbl_Pressure
        '
        Me.lbl_Pressure.BackColor = System.Drawing.Color.White
        Me.lbl_Pressure.Location = New System.Drawing.Point(4, 4)
        Me.lbl_Pressure.Name = "lbl_Pressure"
        Me.lbl_Pressure.Size = New System.Drawing.Size(70, 12)
        Me.lbl_Pressure.TabIndex = 2
        Me.lbl_Pressure.Text = "00,00 Pa"
        Me.lbl_Pressure.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'lbl_Volumeflow
        '
        Me.lbl_Volumeflow.BackColor = System.Drawing.Color.White
        Me.lbl_Volumeflow.Location = New System.Drawing.Point(4, 16)
        Me.lbl_Volumeflow.Name = "lbl_Volumeflow"
        Me.lbl_Volumeflow.Size = New System.Drawing.Size(70, 12)
        Me.lbl_Volumeflow.TabIndex = 3
        Me.lbl_Volumeflow.Text = "00,00 m³/h"
        Me.lbl_Volumeflow.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'lblValue
        '
        Me.lblValue.BackColor = System.Drawing.Color.White
        Me.lblValue.Location = New System.Drawing.Point(4, 64)
        Me.lblValue.Size = New System.Drawing.Size(70, 12)
        '
        'Sensor_Fan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lbl_Volumeflow)
        Me.Controls.Add(Me.lbl_Pressure)
        Me.Name = "Sensor_Fan"
        Me.Controls.SetChildIndex(Me.lbl_Pressure, 0)
        Me.Controls.SetChildIndex(Me.lbl_Volumeflow, 0)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lbl_Pressure As Label
    Friend WithEvents lbl_Volumeflow As Label
End Class
