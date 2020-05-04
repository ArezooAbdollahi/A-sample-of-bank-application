<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Loader
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblInsertSequence = New System.Windows.Forms.Label()
        Me.rbDebit = New System.Windows.Forms.CheckBox()
        Me.cbCardType = New System.Windows.Forms.ComboBox()
        Me.PBload = New System.Windows.Forms.ProgressBar()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnInsertRecords = New System.Windows.Forms.Button()
        Me.txtRecordCount = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.btnSelectInputFile = New System.Windows.Forms.Button()
        Me.bgwLoad = New System.ComponentModel.BackgroundWorker()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblInsertSequence)
        Me.GroupBox1.Controls.Add(Me.rbDebit)
        Me.GroupBox1.Controls.Add(Me.cbCardType)
        Me.GroupBox1.Controls.Add(Me.PBload)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.btnInsertRecords)
        Me.GroupBox1.Controls.Add(Me.txtRecordCount)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtFilePath)
        Me.GroupBox1.Controls.Add(Me.btnSelectInputFile)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(427, 150)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Input File"
        '
        'lblInsertSequence
        '
        Me.lblInsertSequence.AutoSize = True
        Me.lblInsertSequence.Location = New System.Drawing.Point(159, 131)
        Me.lblInsertSequence.Name = "lblInsertSequence"
        Me.lblInsertSequence.Size = New System.Drawing.Size(0, 13)
        Me.lblInsertSequence.TabIndex = 38
        '
        'rbDebit
        '
        Me.rbDebit.AutoSize = True
        Me.rbDebit.Location = New System.Drawing.Point(299, 78)
        Me.rbDebit.Name = "rbDebit"
        Me.rbDebit.Size = New System.Drawing.Size(76, 17)
        Me.rbDebit.TabIndex = 37
        Me.rbDebit.Text = "Debit Card"
        Me.rbDebit.UseVisualStyleBackColor = True
        Me.rbDebit.Visible = False
        '
        'cbCardType
        '
        Me.cbCardType.FormattingEnabled = True
        Me.cbCardType.Location = New System.Drawing.Point(110, 74)
        Me.cbCardType.Name = "cbCardType"
        Me.cbCardType.Size = New System.Drawing.Size(177, 21)
        Me.cbCardType.TabIndex = 36
        '
        'PBload
        '
        Me.PBload.Location = New System.Drawing.Point(13, 104)
        Me.PBload.Name = "PBload"
        Me.PBload.Size = New System.Drawing.Size(390, 23)
        Me.PBload.TabIndex = 10
        Me.PBload.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Select Card Type"
        '
        'btnInsertRecords
        '
        Me.btnInsertRecords.Location = New System.Drawing.Point(299, 46)
        Me.btnInsertRecords.Name = "btnInsertRecords"
        Me.btnInsertRecords.Size = New System.Drawing.Size(104, 23)
        Me.btnInsertRecords.TabIndex = 5
        Me.btnInsertRecords.Text = "Insert Records"
        Me.btnInsertRecords.UseVisualStyleBackColor = True
        '
        'txtRecordCount
        '
        Me.txtRecordCount.Location = New System.Drawing.Point(110, 48)
        Me.txtRecordCount.Name = "txtRecordCount"
        Me.txtRecordCount.ReadOnly = True
        Me.txtRecordCount.Size = New System.Drawing.Size(177, 20)
        Me.txtRecordCount.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Record Count"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(49, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "File Path"
        '
        'txtFilePath
        '
        Me.txtFilePath.Location = New System.Drawing.Point(110, 22)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(177, 20)
        Me.txtFilePath.TabIndex = 1
        '
        'btnSelectInputFile
        '
        Me.btnSelectInputFile.Location = New System.Drawing.Point(299, 19)
        Me.btnSelectInputFile.Name = "btnSelectInputFile"
        Me.btnSelectInputFile.Size = New System.Drawing.Size(104, 23)
        Me.btnSelectInputFile.TabIndex = 0
        Me.btnSelectInputFile.Text = "Select Input File"
        Me.btnSelectInputFile.UseVisualStyleBackColor = True
        '
        'bgwLoad
        '
        Me.bgwLoad.WorkerReportsProgress = True
        Me.bgwLoad.WorkerSupportsCancellation = True
        '
        'Loader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(436, 159)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Loader"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Card Data Loader"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSelectInputFile As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents btnInsertRecords As System.Windows.Forms.Button
    Friend WithEvents txtRecordCount As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PBload As System.Windows.Forms.ProgressBar
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbCardType As System.Windows.Forms.ComboBox
    Friend WithEvents bgwLoad As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblInsertSequence As System.Windows.Forms.Label
    Friend WithEvents rbDebit As System.Windows.Forms.CheckBox

End Class
