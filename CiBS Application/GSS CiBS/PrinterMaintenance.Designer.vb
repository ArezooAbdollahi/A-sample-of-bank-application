<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrinterMaintenance
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblCleaning = New System.Windows.Forms.TextBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblPrintingStatus = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbPrinters = New System.Windows.Forms.ComboBox()
        Me.gbFunctions = New System.Windows.Forms.GroupBox()
        Me.btnGeneralTest = New System.Windows.Forms.Button()
        Me.btnTestCard = New System.Windows.Forms.Button()
        Me.btnEject = New System.Windows.Forms.Button()
        Me.btnCleaning = New System.Windows.Forms.Button()
        Me.GroupBox2.SuspendLayout()
        Me.gbFunctions.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblCleaning)
        Me.GroupBox2.Controls.Add(Me.btnRefresh)
        Me.GroupBox2.Controls.Add(Me.lblPrintingStatus)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.cbPrinters)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 9)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(281, 142)
        Me.GroupBox2.TabIndex = 23
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "چاپگر"
        '
        'lblCleaning
        '
        Me.lblCleaning.ForeColor = System.Drawing.Color.DarkRed
        Me.lblCleaning.Location = New System.Drawing.Point(9, 79)
        Me.lblCleaning.MaxLength = 50
        Me.lblCleaning.Name = "lblCleaning"
        Me.lblCleaning.ReadOnly = True
        Me.lblCleaning.Size = New System.Drawing.Size(188, 21)
        Me.lblCleaning.TabIndex = 48
        Me.lblCleaning.TabStop = False
        Me.lblCleaning.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(9, 108)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(188, 23)
        Me.btnRefresh.TabIndex = 50
        Me.btnRefresh.Text = "به روز رسانی وضعیت چاپگر"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'lblPrintingStatus
        '
        Me.lblPrintingStatus.ForeColor = System.Drawing.Color.DarkRed
        Me.lblPrintingStatus.Location = New System.Drawing.Point(9, 52)
        Me.lblPrintingStatus.MaxLength = 50
        Me.lblPrintingStatus.Name = "lblPrintingStatus"
        Me.lblPrintingStatus.ReadOnly = True
        Me.lblPrintingStatus.Size = New System.Drawing.Size(188, 21)
        Me.lblPrintingStatus.TabIndex = 47
        Me.lblPrintingStatus.TabStop = False
        Me.lblPrintingStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(203, 81)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 13)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "نیاز به تمیزکاری"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(203, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "وضعیت چاپگر"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(203, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "انتخاب چاپگر"
        '
        'cbPrinters
        '
        Me.cbPrinters.FormattingEnabled = True
        Me.cbPrinters.Location = New System.Drawing.Point(9, 24)
        Me.cbPrinters.Name = "cbPrinters"
        Me.cbPrinters.Size = New System.Drawing.Size(188, 21)
        Me.cbPrinters.TabIndex = 9
        '
        'gbFunctions
        '
        Me.gbFunctions.Controls.Add(Me.btnGeneralTest)
        Me.gbFunctions.Controls.Add(Me.btnTestCard)
        Me.gbFunctions.Controls.Add(Me.btnEject)
        Me.gbFunctions.Controls.Add(Me.btnCleaning)
        Me.gbFunctions.Enabled = False
        Me.gbFunctions.Location = New System.Drawing.Point(299, 9)
        Me.gbFunctions.Name = "gbFunctions"
        Me.gbFunctions.Size = New System.Drawing.Size(281, 142)
        Me.gbFunctions.TabIndex = 55
        Me.gbFunctions.TabStop = False
        Me.gbFunctions.Text = "عملیات"
        '
        'btnGeneralTest
        '
        Me.btnGeneralTest.Location = New System.Drawing.Point(45, 108)
        Me.btnGeneralTest.Name = "btnGeneralTest"
        Me.btnGeneralTest.Size = New System.Drawing.Size(188, 23)
        Me.btnGeneralTest.TabIndex = 58
        Me.btnGeneralTest.Text = "تست کلی نرم افزار و سخت افزار"
        Me.btnGeneralTest.UseVisualStyleBackColor = True
        '
        'btnTestCard
        '
        Me.btnTestCard.Location = New System.Drawing.Point(46, 79)
        Me.btnTestCard.Name = "btnTestCard"
        Me.btnTestCard.Size = New System.Drawing.Size(188, 23)
        Me.btnTestCard.TabIndex = 57
        Me.btnTestCard.Text = "چاپ کارت تست"
        Me.btnTestCard.UseVisualStyleBackColor = True
        '
        'btnEject
        '
        Me.btnEject.Location = New System.Drawing.Point(46, 48)
        Me.btnEject.Name = "btnEject"
        Me.btnEject.Size = New System.Drawing.Size(188, 23)
        Me.btnEject.TabIndex = 56
        Me.btnEject.Text = "خارج نمودن کارت"
        Me.btnEject.UseVisualStyleBackColor = True
        '
        'btnCleaning
        '
        Me.btnCleaning.Location = New System.Drawing.Point(46, 17)
        Me.btnCleaning.Name = "btnCleaning"
        Me.btnCleaning.Size = New System.Drawing.Size(188, 23)
        Me.btnCleaning.TabIndex = 55
        Me.btnCleaning.Text = "تمیزکاری چاپگر"
        Me.btnCleaning.UseVisualStyleBackColor = True
        '
        'PrinterMaintenance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(588, 163)
        Me.Controls.Add(Me.gbFunctions)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PrinterMaintenance"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "نگهداری چاپگر"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.gbFunctions.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblCleaning As System.Windows.Forms.TextBox
    Friend WithEvents lblPrintingStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents gbFunctions As System.Windows.Forms.GroupBox
    Friend WithEvents btnGeneralTest As System.Windows.Forms.Button
    Friend WithEvents btnTestCard As System.Windows.Forms.Button
    Friend WithEvents btnEject As System.Windows.Forms.Button
    Friend WithEvents btnCleaning As System.Windows.Forms.Button
End Class
