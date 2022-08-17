<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class pidControlPanel
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
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Input_SetValue3 = New VBAixGUI.Input_SetValue()
        Me.Input_SetValue2 = New VBAixGUI.Input_SetValue()
        Me.Input_SetValue1 = New VBAixGUI.Input_SetValue()
        Me.CheckboxEingabe1 = New VBAixGUI.CheckboxEingabe()
        Me.SuspendLayout()
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(16, 14)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(134, 21)
        Me.ComboBox1.TabIndex = 4
        '
        'Input_SetValue3
        '
        Me.Input_SetValue3.Active = True
        Me.Input_SetValue3.DecimalPlaces = 5
        Me.Input_SetValue3.Hint = Nothing
        Me.Input_SetValue3.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.Input_SetValue3.Location = New System.Drawing.Point(16, 126)
        Me.Input_SetValue3.MaxValue = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.Input_SetValue3.MinValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Input_SetValue3.Name = "Input_SetValue3"
        Me.Input_SetValue3.PollRate = 1000
        Me.Input_SetValue3.SetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Input_SetValue3.SetValueNew = False
        Me.Input_SetValue3.Size = New System.Drawing.Size(134, 16)
        Me.Input_SetValue3.Symbol = Nothing
        Me.Input_SetValue3.TabIndex = 3
        Me.Input_SetValue3.Unit = "Td"
        '
        'Input_SetValue2
        '
        Me.Input_SetValue2.Active = True
        Me.Input_SetValue2.DecimalPlaces = 5
        Me.Input_SetValue2.Hint = Nothing
        Me.Input_SetValue2.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.Input_SetValue2.Location = New System.Drawing.Point(16, 93)
        Me.Input_SetValue2.MaxValue = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.Input_SetValue2.MinValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Input_SetValue2.Name = "Input_SetValue2"
        Me.Input_SetValue2.PollRate = 1000
        Me.Input_SetValue2.SetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Input_SetValue2.SetValueNew = False
        Me.Input_SetValue2.Size = New System.Drawing.Size(134, 16)
        Me.Input_SetValue2.Symbol = Nothing
        Me.Input_SetValue2.TabIndex = 2
        Me.Input_SetValue2.Unit = "Ti"
        '
        'Input_SetValue1
        '
        Me.Input_SetValue1.Active = True
        Me.Input_SetValue1.DecimalPlaces = 5
        Me.Input_SetValue1.Hint = Nothing
        Me.Input_SetValue1.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.Input_SetValue1.Location = New System.Drawing.Point(16, 57)
        Me.Input_SetValue1.MaxValue = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.Input_SetValue1.MinValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Input_SetValue1.Name = "Input_SetValue1"
        Me.Input_SetValue1.PollRate = 1000
        Me.Input_SetValue1.SetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Input_SetValue1.SetValueNew = False
        Me.Input_SetValue1.Size = New System.Drawing.Size(134, 16)
        Me.Input_SetValue1.Symbol = Nothing
        Me.Input_SetValue1.TabIndex = 0
        Me.Input_SetValue1.Unit = "Kp"
        '
        'CheckboxEingabe1
        '
        Me.CheckboxEingabe1.Aktuallisierungsrate = 500
        Me.CheckboxEingabe1.Displaytext = "Reset PID"
        Me.CheckboxEingabe1.Hinweis = Nothing
        Me.CheckboxEingabe1.Location = New System.Drawing.Point(16, 161)
        Me.CheckboxEingabe1.manuellerModus = True
        Me.CheckboxEingabe1.Name = "CheckboxEingabe1"
        Me.CheckboxEingabe1.Size = New System.Drawing.Size(134, 27)
        Me.CheckboxEingabe1.SollWertNeu = False
        Me.CheckboxEingabe1.Symbolname = Nothing
        Me.CheckboxEingabe1.TabIndex = 5
        Me.CheckboxEingabe1.Wert = False
        '
        'pidControlPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CheckboxEingabe1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Input_SetValue3)
        Me.Controls.Add(Me.Input_SetValue2)
        Me.Controls.Add(Me.Input_SetValue1)
        Me.Name = "pidControlPanel"
        Me.Size = New System.Drawing.Size(155, 187)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Input_SetValue1 As Input_SetValue
    Friend WithEvents Input_SetValue2 As Input_SetValue
    Friend WithEvents Input_SetValue3 As Input_SetValue
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents CheckboxEingabe1 As CheckboxEingabe
End Class
