<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reports
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Reports))
        Me.dgvCards = New System.Windows.Forms.DataGridView()
        Me.txtBrCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbCardType = New System.Windows.Forms.ComboBox()
        Me.btnRunReport = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnExportToExcel = New System.Windows.Forms.Button()
        Me.txtFromDate = New System.Windows.Forms.TextBox()
        Me.txtToDate = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnReportDateTime = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtBranchCode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnAllUsers = New System.Windows.Forms.Button()
        Me.btnBranchUsers = New System.Windows.Forms.Button()
        Me.btnExportPDF = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbBranchStackStatus = New System.Windows.Forms.RadioButton()
        Me.rbPan = New System.Windows.Forms.RadioButton()
        Me.txtPAN = New System.Windows.Forms.TextBox()
        Me.rbImportedDataReport = New System.Windows.Forms.RadioButton()
        Me.rbCardDataRemain = New System.Windows.Forms.RadioButton()
        Me.rbConflictConsumables = New System.Windows.Forms.RadioButton()
        Me.rbUnusableCards = New System.Windows.Forms.RadioButton()
        Me.rbAccount = New System.Windows.Forms.RadioButton()
        Me.txtAccount = New System.Windows.Forms.TextBox()
        Me.cbReposotoryStatus = New System.Windows.Forms.ComboBox()
        Me.cbReportType = New System.Windows.Forms.ComboBox()
        Me.rbConsumables = New System.Windows.Forms.RadioButton()
        Me.cbConsumables = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.rbEvents = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.rbIssuedCards = New System.Windows.Forms.RadioButton()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        CType(Me.dgvCards, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvCards
        '
        Me.dgvCards.AllowUserToAddRows = False
        Me.dgvCards.AllowUserToDeleteRows = False
        Me.dgvCards.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dgvCards.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvCards.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvCards.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvCards.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCards.Location = New System.Drawing.Point(12, 275)
        Me.dgvCards.MultiSelect = False
        Me.dgvCards.Name = "dgvCards"
        Me.dgvCards.ReadOnly = True
        Me.dgvCards.RowHeadersWidth = 21
        Me.dgvCards.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCards.Size = New System.Drawing.Size(959, 251)
        Me.dgvCards.TabIndex = 1
        '
        'txtBrCode
        '
        Me.txtBrCode.Enabled = False
        Me.txtBrCode.Location = New System.Drawing.Point(821, 22)
        Me.txtBrCode.Name = "txtBrCode"
        Me.txtBrCode.Size = New System.Drawing.Size(75, 21)
        Me.txtBrCode.TabIndex = 19
        Me.txtBrCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(899, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "کد شعبه"
        '
        'cbCardType
        '
        Me.cbCardType.Enabled = False
        Me.cbCardType.FormattingEnabled = True
        Me.cbCardType.Items.AddRange(New Object() {"همه کارتها"})
        Me.cbCardType.Location = New System.Drawing.Point(685, 18)
        Me.cbCardType.Name = "cbCardType"
        Me.cbCardType.Size = New System.Drawing.Size(102, 21)
        Me.cbCardType.TabIndex = 9
        '
        'btnRunReport
        '
        Me.btnRunReport.Location = New System.Drawing.Point(250, 21)
        Me.btnRunReport.Name = "btnRunReport"
        Me.btnRunReport.Size = New System.Drawing.Size(110, 23)
        Me.btnRunReport.TabIndex = 10
        Me.btnRunReport.Text = "مشاهده گزارش"
        Me.btnRunReport.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Navy
        Me.Label2.Location = New System.Drawing.Point(768, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "از تاریخ"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Navy
        Me.Label3.Location = New System.Drawing.Point(538, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "تا تاریخ"
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.Location = New System.Drawing.Point(131, 21)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(110, 23)
        Me.btnExportToExcel.TabIndex = 17
        Me.btnExportToExcel.TabStop = False
        Me.btnExportToExcel.Text = "Export To EXCEL"
        Me.btnExportToExcel.UseVisualStyleBackColor = True
        '
        'txtFromDate
        '
        Me.txtFromDate.Location = New System.Drawing.Point(595, 22)
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.Size = New System.Drawing.Size(171, 21)
        Me.txtFromDate.TabIndex = 20
        Me.txtFromDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtToDate
        '
        Me.txtToDate.Location = New System.Drawing.Point(366, 23)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.Size = New System.Drawing.Size(171, 21)
        Me.txtToDate.TabIndex = 21
        Me.txtToDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnReportDateTime)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.btnExportPDF)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.btnRunReport)
        Me.GroupBox1.Controls.Add(Me.btnExportToExcel)
        Me.GroupBox1.Controls.Add(Me.txtToDate)
        Me.GroupBox1.Controls.Add(Me.txtBrCode)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtFromDate)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(960, 266)
        Me.GroupBox1.TabIndex = 22
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "تنظیمات گزارش"
        '
        'btnReportDateTime
        '
        Me.btnReportDateTime.Location = New System.Drawing.Point(196, 56)
        Me.btnReportDateTime.Name = "btnReportDateTime"
        Me.btnReportDateTime.Size = New System.Drawing.Size(165, 23)
        Me.btnReportDateTime.TabIndex = 28
        Me.btnReportDateTime.Text = "مشاهده گزارش eFarda"
        Me.btnReportDateTime.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtBranchCode)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.btnAllUsers)
        Me.GroupBox3.Controls.Add(Me.btnBranchUsers)
        Me.GroupBox3.Location = New System.Drawing.Point(207, 209)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(532, 51)
        Me.GroupBox3.TabIndex = 27
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "تنظیمات گزارش کاربران"
        '
        'txtBranchCode
        '
        Me.txtBranchCode.Location = New System.Drawing.Point(388, 20)
        Me.txtBranchCode.Name = "txtBranchCode"
        Me.txtBranchCode.Size = New System.Drawing.Size(75, 21)
        Me.txtBranchCode.TabIndex = 29
        Me.txtBranchCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Navy
        Me.Label6.Location = New System.Drawing.Point(466, 24)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 13)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "کد شعبه"
        '
        'btnAllUsers
        '
        Me.btnAllUsers.Image = CType(resources.GetObject("btnAllUsers.Image"), System.Drawing.Image)
        Me.btnAllUsers.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAllUsers.Location = New System.Drawing.Point(13, 18)
        Me.btnAllUsers.Name = "btnAllUsers"
        Me.btnAllUsers.Size = New System.Drawing.Size(112, 23)
        Me.btnAllUsers.TabIndex = 17
        Me.btnAllUsers.Text = "کلیه کاربران"
        Me.btnAllUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAllUsers.UseVisualStyleBackColor = True
        '
        'btnBranchUsers
        '
        Me.btnBranchUsers.Image = CType(resources.GetObject("btnBranchUsers.Image"), System.Drawing.Image)
        Me.btnBranchUsers.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnBranchUsers.Location = New System.Drawing.Point(179, 18)
        Me.btnBranchUsers.Name = "btnBranchUsers"
        Me.btnBranchUsers.Size = New System.Drawing.Size(112, 23)
        Me.btnBranchUsers.TabIndex = 16
        Me.btnBranchUsers.Text = "کاربران شعبه"
        Me.btnBranchUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBranchUsers.UseVisualStyleBackColor = True
        '
        'btnExportPDF
        '
        Me.btnExportPDF.Location = New System.Drawing.Point(9, 21)
        Me.btnExportPDF.Name = "btnExportPDF"
        Me.btnExportPDF.Size = New System.Drawing.Size(110, 23)
        Me.btnExportPDF.TabIndex = 26
        Me.btnExportPDF.TabStop = False
        Me.btnExportPDF.Text = "Export To PDF"
        Me.btnExportPDF.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbBranchStackStatus)
        Me.GroupBox2.Controls.Add(Me.rbPan)
        Me.GroupBox2.Controls.Add(Me.txtPAN)
        Me.GroupBox2.Controls.Add(Me.rbImportedDataReport)
        Me.GroupBox2.Controls.Add(Me.rbCardDataRemain)
        Me.GroupBox2.Controls.Add(Me.rbConflictConsumables)
        Me.GroupBox2.Controls.Add(Me.rbUnusableCards)
        Me.GroupBox2.Controls.Add(Me.rbAccount)
        Me.GroupBox2.Controls.Add(Me.txtAccount)
        Me.GroupBox2.Controls.Add(Me.cbReposotoryStatus)
        Me.GroupBox2.Controls.Add(Me.cbReportType)
        Me.GroupBox2.Controls.Add(Me.rbConsumables)
        Me.GroupBox2.Controls.Add(Me.cbConsumables)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.rbEvents)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.rbIssuedCards)
        Me.GroupBox2.Controls.Add(Me.cbCardType)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 87)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(948, 117)
        Me.GroupBox2.TabIndex = 25
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "نوع گزارش"
        '
        'rbBranchStackStatus
        '
        Me.rbBranchStackStatus.AutoSize = True
        Me.rbBranchStackStatus.Location = New System.Drawing.Point(154, 48)
        Me.rbBranchStackStatus.Name = "rbBranchStackStatus"
        Me.rbBranchStackStatus.Size = New System.Drawing.Size(109, 17)
        Me.rbBranchStackStatus.TabIndex = 35
        Me.rbBranchStackStatus.Text = "وضعیت انبار شعبه"
        Me.rbBranchStackStatus.UseVisualStyleBackColor = True
        '
        'rbPan
        '
        Me.rbPan.AutoSize = True
        Me.rbPan.Location = New System.Drawing.Point(167, 17)
        Me.rbPan.Name = "rbPan"
        Me.rbPan.Size = New System.Drawing.Size(79, 17)
        Me.rbPan.TabIndex = 34
        Me.rbPan.Text = "شماره کارت"
        Me.rbPan.UseVisualStyleBackColor = True
        '
        'txtPAN
        '
        Me.txtPAN.Enabled = False
        Me.txtPAN.Location = New System.Drawing.Point(8, 14)
        Me.txtPAN.MaxLength = 16
        Me.txtPAN.Name = "txtPAN"
        Me.txtPAN.Size = New System.Drawing.Size(152, 21)
        Me.txtPAN.TabIndex = 33
        Me.txtPAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'rbImportedDataReport
        '
        Me.rbImportedDataReport.AutoSize = True
        Me.rbImportedDataReport.Enabled = False
        Me.rbImportedDataReport.Location = New System.Drawing.Point(530, 73)
        Me.rbImportedDataReport.Name = "rbImportedDataReport"
        Me.rbImportedDataReport.Size = New System.Drawing.Size(157, 17)
        Me.rbImportedDataReport.TabIndex = 32
        Me.rbImportedDataReport.Text = "آمار کارتهای وارده به سیستم"
        Me.rbImportedDataReport.UseVisualStyleBackColor = True
        '
        'rbCardDataRemain
        '
        Me.rbCardDataRemain.AutoSize = True
        Me.rbCardDataRemain.Enabled = False
        Me.rbCardDataRemain.Location = New System.Drawing.Point(725, 73)
        Me.rbCardDataRemain.Name = "rbCardDataRemain"
        Me.rbCardDataRemain.Size = New System.Drawing.Size(207, 17)
        Me.rbCardDataRemain.TabIndex = 31
        Me.rbCardDataRemain.Text = "وضعیت کارتهای موجود در بانک اطلاعاتی"
        Me.rbCardDataRemain.UseVisualStyleBackColor = True
        '
        'rbConflictConsumables
        '
        Me.rbConflictConsumables.AutoSize = True
        Me.rbConflictConsumables.Enabled = False
        Me.rbConflictConsumables.Location = New System.Drawing.Point(384, 46)
        Me.rbConflictConsumables.Name = "rbConflictConsumables"
        Me.rbConflictConsumables.Size = New System.Drawing.Size(118, 17)
        Me.rbConflictConsumables.TabIndex = 30
        Me.rbConflictConsumables.Text = "مغایرت مواد مصرفی"
        Me.rbConflictConsumables.UseVisualStyleBackColor = True
        '
        'rbUnusableCards
        '
        Me.rbUnusableCards.AutoSize = True
        Me.rbUnusableCards.Location = New System.Drawing.Point(273, 48)
        Me.rbUnusableCards.Name = "rbUnusableCards"
        Me.rbUnusableCards.Size = New System.Drawing.Size(92, 17)
        Me.rbUnusableCards.TabIndex = 29
        Me.rbUnusableCards.Text = "کارتهای معیوب"
        Me.rbUnusableCards.UseVisualStyleBackColor = True
        '
        'rbAccount
        '
        Me.rbAccount.AutoSize = True
        Me.rbAccount.Location = New System.Drawing.Point(413, 18)
        Me.rbAccount.Name = "rbAccount"
        Me.rbAccount.Size = New System.Drawing.Size(89, 17)
        Me.rbAccount.TabIndex = 28
        Me.rbAccount.Text = "شماره حساب"
        Me.rbAccount.UseVisualStyleBackColor = True
        '
        'txtAccount
        '
        Me.txtAccount.Enabled = False
        Me.txtAccount.Location = New System.Drawing.Point(259, 17)
        Me.txtAccount.MaxLength = 13
        Me.txtAccount.Name = "txtAccount"
        Me.txtAccount.Size = New System.Drawing.Size(152, 21)
        Me.txtAccount.TabIndex = 27
        Me.txtAccount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cbReposotoryStatus
        '
        Me.cbReposotoryStatus.Enabled = False
        Me.cbReposotoryStatus.FormattingEnabled = True
        Me.cbReposotoryStatus.Items.AddRange(New Object() {"وارده به انبار", "صادره از انبار"})
        Me.cbReposotoryStatus.Location = New System.Drawing.Point(530, 46)
        Me.cbReposotoryStatus.Name = "cbReposotoryStatus"
        Me.cbReposotoryStatus.Size = New System.Drawing.Size(148, 21)
        Me.cbReposotoryStatus.TabIndex = 23
        '
        'cbReportType
        '
        Me.cbReportType.Enabled = False
        Me.cbReportType.FormattingEnabled = True
        Me.cbReportType.Items.AddRange(New Object() {"گزارش تفضیلی", "گزارش تجمعی کلیه شعب", "گزارش تجمعی به تفکیک شعب"})
        Me.cbReportType.Location = New System.Drawing.Point(530, 17)
        Me.cbReportType.Name = "cbReportType"
        Me.cbReportType.Size = New System.Drawing.Size(149, 21)
        Me.cbReportType.TabIndex = 24
        '
        'rbConsumables
        '
        Me.rbConsumables.AutoSize = True
        Me.rbConsumables.Location = New System.Drawing.Point(851, 49)
        Me.rbConsumables.Name = "rbConsumables"
        Me.rbConsumables.Size = New System.Drawing.Size(83, 17)
        Me.rbConsumables.TabIndex = 1
        Me.rbConsumables.Text = "مواد مصرفی"
        Me.rbConsumables.UseVisualStyleBackColor = True
        '
        'cbConsumables
        '
        Me.cbConsumables.Enabled = False
        Me.cbConsumables.FormattingEnabled = True
        Me.cbConsumables.Items.AddRange(New Object() {"همه مواد"})
        Me.cbConsumables.Location = New System.Drawing.Point(685, 46)
        Me.cbConsumables.Name = "cbConsumables"
        Me.cbConsumables.Size = New System.Drawing.Size(102, 21)
        Me.cbConsumables.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Navy
        Me.Label4.Location = New System.Drawing.Point(790, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "نوع کارت"
        '
        'rbEvents
        '
        Me.rbEvents.AutoSize = True
        Me.rbEvents.Enabled = False
        Me.rbEvents.Location = New System.Drawing.Point(393, 73)
        Me.rbEvents.Name = "rbEvents"
        Me.rbEvents.Size = New System.Drawing.Size(109, 17)
        Me.rbEvents.TabIndex = 2
        Me.rbEvents.Text = "رخداد نگاری شعبه"
        Me.rbEvents.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Navy
        Me.Label5.Location = New System.Drawing.Point(790, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "نوع مواد"
        '
        'rbIssuedCards
        '
        Me.rbIssuedCards.AutoSize = True
        Me.rbIssuedCards.Checked = True
        Me.rbIssuedCards.Location = New System.Drawing.Point(844, 20)
        Me.rbIssuedCards.Name = "rbIssuedCards"
        Me.rbIssuedCards.Size = New System.Drawing.Size(88, 17)
        Me.rbIssuedCards.TabIndex = 0
        Me.rbIssuedCards.TabStop = True
        Me.rbIssuedCards.Text = "کارتهای صادره"
        Me.rbIssuedCards.UseVisualStyleBackColor = True
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'Reports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(983, 533)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvCards)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Reports"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "فرم گزارش ساز سیتم صدور آنی کارت در شعب"
        CType(Me.dgvCards, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvCards As System.Windows.Forms.DataGridView
    Friend WithEvents txtBrCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbCardType As System.Windows.Forms.ComboBox
    Friend WithEvents btnRunReport As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnExportToExcel As System.Windows.Forms.Button
    Friend WithEvents txtFromDate As System.Windows.Forms.TextBox
    Friend WithEvents txtToDate As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbEvents As System.Windows.Forms.RadioButton
    Friend WithEvents rbConsumables As System.Windows.Forms.RadioButton
    Friend WithEvents rbIssuedCards As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbConsumables As System.Windows.Forms.ComboBox
    Friend WithEvents cbReposotoryStatus As System.Windows.Forms.ComboBox
    Friend WithEvents cbReportType As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rbAccount As System.Windows.Forms.RadioButton
    Friend WithEvents txtAccount As System.Windows.Forms.TextBox
    Friend WithEvents rbUnusableCards As System.Windows.Forms.RadioButton
    Friend WithEvents rbConflictConsumables As System.Windows.Forms.RadioButton
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents rbCardDataRemain As System.Windows.Forms.RadioButton
    Friend WithEvents rbImportedDataReport As System.Windows.Forms.RadioButton
    Friend WithEvents btnExportPDF As System.Windows.Forms.Button
    Friend WithEvents rbPan As System.Windows.Forms.RadioButton
    Friend WithEvents txtPAN As System.Windows.Forms.TextBox
    Friend WithEvents rbBranchStackStatus As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtBranchCode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnAllUsers As System.Windows.Forms.Button
    Friend WithEvents btnBranchUsers As System.Windows.Forms.Button
    Friend WithEvents btnReportDateTime As System.Windows.Forms.Button
End Class
