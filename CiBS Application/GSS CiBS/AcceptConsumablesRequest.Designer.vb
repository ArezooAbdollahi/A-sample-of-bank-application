<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AcceptConsumablesRequest
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
        Me.btnAccept = New System.Windows.Forms.Button()
        Me.txtAcceptedCount = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvRequests = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblBrCode = New System.Windows.Forms.TextBox()
        Me.lblConsumableType = New System.Windows.Forms.TextBox()
        Me.lblRequestCount = New System.Windows.Forms.TextBox()
        Me.txtBranchRemain = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cmbCunsomable = New System.Windows.Forms.ComboBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.txtBranches = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        CType(Me.dgvRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnAccept
        '
        Me.btnAccept.Enabled = False
        Me.btnAccept.Location = New System.Drawing.Point(496, 435)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(101, 23)
        Me.btnAccept.TabIndex = 25
        Me.btnAccept.Text = "تایید درخواست"
        Me.btnAccept.UseVisualStyleBackColor = True
        '
        'txtAcceptedCount
        '
        Me.txtAcceptedCount.ForeColor = System.Drawing.Color.Navy
        Me.txtAcceptedCount.Location = New System.Drawing.Point(411, 437)
        Me.txtAcceptedCount.Name = "txtAcceptedCount"
        Me.txtAcceptedCount.Size = New System.Drawing.Size(69, 21)
        Me.txtAcceptedCount.TabIndex = 24
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(328, 440)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "تعداد مورد تایید"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 389)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "نوع مواد مصرفی"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(592, 348)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(101, 23)
        Me.btnRefresh.TabIndex = 18
        Me.btnRefresh.Text = "بروزرسانی"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.DarkRed
        Me.Label1.Location = New System.Drawing.Point(15, 161)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(161, 13)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "درخواستهای ارسال شده از شعب"
        '
        'dgvRequests
        '
        Me.dgvRequests.AllowUserToAddRows = False
        Me.dgvRequests.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dgvRequests.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvRequests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvRequests.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRequests.Location = New System.Drawing.Point(12, 186)
        Me.dgvRequests.MultiSelect = False
        Me.dgvRequests.Name = "dgvRequests"
        Me.dgvRequests.ReadOnly = True
        Me.dgvRequests.RowHeadersWidth = 21
        Me.dgvRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvRequests.Size = New System.Drawing.Size(681, 156)
        Me.dgvRequests.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(60, 363)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "کد شعبه"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(25, 418)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 13)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "تعداد درخواستی"
        '
        'lblBrCode
        '
        Me.lblBrCode.ForeColor = System.Drawing.Color.Black
        Me.lblBrCode.Location = New System.Drawing.Point(114, 360)
        Me.lblBrCode.MaxLength = 50
        Me.lblBrCode.Name = "lblBrCode"
        Me.lblBrCode.ReadOnly = True
        Me.lblBrCode.Size = New System.Drawing.Size(188, 21)
        Me.lblBrCode.TabIndex = 48
        Me.lblBrCode.TabStop = False
        '
        'lblConsumableType
        '
        Me.lblConsumableType.ForeColor = System.Drawing.Color.Black
        Me.lblConsumableType.Location = New System.Drawing.Point(114, 389)
        Me.lblConsumableType.MaxLength = 50
        Me.lblConsumableType.Name = "lblConsumableType"
        Me.lblConsumableType.ReadOnly = True
        Me.lblConsumableType.Size = New System.Drawing.Size(188, 21)
        Me.lblConsumableType.TabIndex = 49
        Me.lblConsumableType.TabStop = False
        '
        'lblRequestCount
        '
        Me.lblRequestCount.ForeColor = System.Drawing.Color.Black
        Me.lblRequestCount.Location = New System.Drawing.Point(114, 415)
        Me.lblRequestCount.MaxLength = 50
        Me.lblRequestCount.Name = "lblRequestCount"
        Me.lblRequestCount.ReadOnly = True
        Me.lblRequestCount.Size = New System.Drawing.Size(188, 21)
        Me.lblRequestCount.TabIndex = 50
        Me.lblRequestCount.TabStop = False
        '
        'txtBranchRemain
        '
        Me.txtBranchRemain.ForeColor = System.Drawing.Color.Black
        Me.txtBranchRemain.Location = New System.Drawing.Point(114, 442)
        Me.txtBranchRemain.MaxLength = 50
        Me.txtBranchRemain.Name = "txtBranchRemain"
        Me.txtBranchRemain.ReadOnly = True
        Me.txtBranchRemain.Size = New System.Drawing.Size(188, 21)
        Me.txtBranchRemain.TabIndex = 52
        Me.txtBranchRemain.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 445)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 13)
        Me.Label5.TabIndex = 51
        Me.Label5.Text = "مانده فعلی شعبه"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.cmbCunsomable)
        Me.GroupBox1.Controls.Add(Me.TextBox4)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.txtBranches)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(681, 132)
        Me.GroupBox1.TabIndex = 53
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "جستجو بر اساس"
        '
        'Button1
        '
        Me.Button1.Enabled = False
        Me.Button1.Location = New System.Drawing.Point(29, 83)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(134, 39)
        Me.Button1.TabIndex = 54
        Me.Button1.Text = "نمایش درخواستها"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'cmbCunsomable
        '
        Me.cmbCunsomable.FormattingEnabled = True
        Me.cmbCunsomable.Location = New System.Drawing.Point(374, 49)
        Me.cmbCunsomable.Name = "cmbCunsomable"
        Me.cmbCunsomable.Size = New System.Drawing.Size(188, 21)
        Me.cmbCunsomable.TabIndex = 62
        '
        'TextBox4
        '
        Me.TextBox4.ForeColor = System.Drawing.Color.Black
        Me.TextBox4.Location = New System.Drawing.Point(6, 51)
        Me.TextBox4.MaxLength = 50
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ReadOnly = True
        Me.TextBox4.Size = New System.Drawing.Size(188, 21)
        Me.TextBox4.TabIndex = 61
        Me.TextBox4.TabStop = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(220, 53)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(113, 13)
        Me.Label10.TabIndex = 60
        Me.Label10.Text = "کدکاربر درخواست کننده"
        '
        'TextBox1
        '
        Me.TextBox1.ForeColor = System.Drawing.Color.Black
        Me.TextBox1.Location = New System.Drawing.Point(6, 22)
        Me.TextBox1.MaxLength = 50
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(188, 21)
        Me.TextBox1.TabIndex = 59
        Me.TextBox1.TabStop = False
        '
        'txtBranches
        '
        Me.txtBranches.ForeColor = System.Drawing.Color.Black
        Me.txtBranches.Location = New System.Drawing.Point(374, 22)
        Me.txtBranches.MaxLength = 50
        Me.txtBranches.Name = "txtBranches"
        Me.txtBranches.ReadOnly = True
        Me.txtBranches.Size = New System.Drawing.Size(188, 21)
        Me.txtBranches.TabIndex = 57
        Me.txtBranches.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(623, 24)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 13)
        Me.Label9.TabIndex = 54
        Me.Label9.Text = "کد شعبه"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(588, 50)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(83, 13)
        Me.Label8.TabIndex = 55
        Me.Label8.Text = "نوع مواد مصرفی"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(246, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 13)
        Me.Label7.TabIndex = 56
        Me.Label7.Text = "تاریخ درخواست"
        '
        'AcceptConsumablesRequest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(705, 483)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtBranchRemain)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblRequestCount)
        Me.Controls.Add(Me.lblConsumableType)
        Me.Controls.Add(Me.lblBrCode)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnAccept)
        Me.Controls.Add(Me.txtAcceptedCount)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvRequests)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AcceptConsumablesRequest"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "تایید درخواستهای شعب"
        CType(Me.dgvRequests, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnAccept As System.Windows.Forms.Button
    Friend WithEvents txtAcceptedCount As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvRequests As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblBrCode As System.Windows.Forms.TextBox
    Friend WithEvents lblConsumableType As System.Windows.Forms.TextBox
    Friend WithEvents lblRequestCount As System.Windows.Forms.TextBox
    Friend WithEvents txtBranchRemain As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cmbCunsomable As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents txtBranches As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
