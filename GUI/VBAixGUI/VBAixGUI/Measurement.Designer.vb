<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Measurement
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.MeasurementName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnAssignSymbols = New System.Windows.Forms.Button()
        Me.btnStartMeasurement = New System.Windows.Forms.Button()
        Me.TimerPoll = New System.Windows.Forms.Timer(Me.components)
        Me.txtBoxMeasurementName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.numUpDownDuration = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.numUpDownResolution = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtBoxSaveLocation = New System.Windows.Forms.TextBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.btnSelectFolder = New System.Windows.Forms.Button()
        Me.btnStopMeasurement = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numUpDownDuration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numUpDownResolution, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.MeasurementName})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Top
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 20
        Me.DataGridView1.Size = New System.Drawing.Size(587, 300)
        Me.DataGridView1.TabIndex = 0
        '
        'MeasurementName
        '
        Me.MeasurementName.Frozen = True
        Me.MeasurementName.HeaderText = "MeasurementName"
        Me.MeasurementName.Name = "MeasurementName"
        Me.MeasurementName.Width = 124
        '
        'btnAssignSymbols
        '
        Me.btnAssignSymbols.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAssignSymbols.Location = New System.Drawing.Point(3, 310)
        Me.btnAssignSymbols.Name = "btnAssignSymbols"
        Me.btnAssignSymbols.Size = New System.Drawing.Size(114, 23)
        Me.btnAssignSymbols.TabIndex = 1
        Me.btnAssignSymbols.Text = "Symbole zuweisen"
        Me.btnAssignSymbols.UseVisualStyleBackColor = True
        '
        'btnStartMeasurement
        '
        Me.btnStartMeasurement.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnStartMeasurement.BackColor = System.Drawing.Color.FromArgb(CType(CType(87, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.btnStartMeasurement.Location = New System.Drawing.Point(3, 338)
        Me.btnStartMeasurement.Name = "btnStartMeasurement"
        Me.btnStartMeasurement.Size = New System.Drawing.Size(114, 35)
        Me.btnStartMeasurement.TabIndex = 2
        Me.btnStartMeasurement.Text = "Messung starten"
        Me.btnStartMeasurement.UseVisualStyleBackColor = False
        '
        'TimerPoll
        '
        '
        'txtBoxMeasurementName
        '
        Me.txtBoxMeasurementName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtBoxMeasurementName.Location = New System.Drawing.Point(244, 338)
        Me.txtBoxMeasurementName.Name = "txtBoxMeasurementName"
        Me.txtBoxMeasurementName.Size = New System.Drawing.Size(207, 20)
        Me.txtBoxMeasurementName.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(139, 341)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Name der Messung"
        '
        'lblStatus
        '
        Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblStatus.Location = New System.Drawing.Point(0, 417)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(587, 13)
        Me.lblStatus.TabIndex = 5
        Me.lblStatus.Text = "Status"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(139, 366)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Dauer der Messung"
        '
        'numUpDownDuration
        '
        Me.numUpDownDuration.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.numUpDownDuration.Location = New System.Drawing.Point(244, 364)
        Me.numUpDownDuration.Maximum = New Decimal(New Integer() {600, 0, 0, 0})
        Me.numUpDownDuration.Name = "numUpDownDuration"
        Me.numUpDownDuration.Size = New System.Drawing.Size(108, 20)
        Me.numUpDownDuration.TabIndex = 8
        Me.numUpDownDuration.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(358, 366)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(12, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "s"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(358, 392)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(20, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "ms"
        '
        'numUpDownResolution
        '
        Me.numUpDownResolution.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.numUpDownResolution.Location = New System.Drawing.Point(244, 390)
        Me.numUpDownResolution.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.numUpDownResolution.Name = "numUpDownResolution"
        Me.numUpDownResolution.Size = New System.Drawing.Size(108, 20)
        Me.numUpDownResolution.TabIndex = 11
        Me.numUpDownResolution.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(184, 392)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Auflösung"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(177, 315)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Speicherort"
        '
        'txtBoxSaveLocation
        '
        Me.txtBoxSaveLocation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtBoxSaveLocation.Location = New System.Drawing.Point(244, 312)
        Me.txtBoxSaveLocation.Name = "txtBoxSaveLocation"
        Me.txtBoxSaveLocation.ReadOnly = True
        Me.txtBoxSaveLocation.Size = New System.Drawing.Size(207, 20)
        Me.txtBoxSaveLocation.TabIndex = 14
        Me.txtBoxSaveLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.Description = "Speicherort für die Messdaten"
        Me.FolderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer
        '
        'btnSelectFolder
        '
        Me.btnSelectFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelectFolder.Location = New System.Drawing.Point(457, 311)
        Me.btnSelectFolder.Name = "btnSelectFolder"
        Me.btnSelectFolder.Size = New System.Drawing.Size(114, 23)
        Me.btnSelectFolder.TabIndex = 15
        Me.btnSelectFolder.Text = "Auswählen"
        Me.btnSelectFolder.UseVisualStyleBackColor = True
        '
        'btnStopMeasurement
        '
        Me.btnStopMeasurement.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnStopMeasurement.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(7, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.btnStopMeasurement.Enabled = False
        Me.btnStopMeasurement.Location = New System.Drawing.Point(3, 378)
        Me.btnStopMeasurement.Name = "btnStopMeasurement"
        Me.btnStopMeasurement.Size = New System.Drawing.Size(114, 35)
        Me.btnStopMeasurement.TabIndex = 16
        Me.btnStopMeasurement.Text = "Messung stoppen"
        Me.btnStopMeasurement.UseVisualStyleBackColor = False
        '
        'Measurement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.btnStopMeasurement)
        Me.Controls.Add(Me.btnSelectFolder)
        Me.Controls.Add(Me.txtBoxSaveLocation)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.numUpDownResolution)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.numUpDownDuration)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtBoxMeasurementName)
        Me.Controls.Add(Me.btnStartMeasurement)
        Me.Controls.Add(Me.btnAssignSymbols)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Measurement"
        Me.Size = New System.Drawing.Size(587, 430)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numUpDownDuration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numUpDownResolution, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnAssignSymbols As Button
    Friend WithEvents btnStartMeasurement As Button
    Friend WithEvents TimerPoll As Timer
    Friend WithEvents MeasurementName As DataGridViewTextBoxColumn
    Friend WithEvents txtBoxMeasurementName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents numUpDownDuration As NumericUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents numUpDownResolution As NumericUpDown
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtBoxSaveLocation As TextBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents btnSelectFolder As Button
    Friend WithEvents btnStopMeasurement As Button
End Class
