<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConsumablesStack
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbConsumables = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dgvStack = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnInsertRecord = New System.Windows.Forms.Button()
        Me.txtCosumableCount = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnRemainAll = New System.Windows.Forms.Button()
        Me.btnRemainBranch = New System.Windows.Forms.Button()
        Me.txtBranchCode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.dgvStack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.DarkRed
        Me.Label1.Location = New System.Drawing.Point(13, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(126, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "موجودی انبار مواد مصرفی"
        '
        'cbConsumables
        '
        Me.cbConsumables.FormattingEnabled = True
        Me.cbConsumables.Location = New System.Drawing.Point(97, 27)
        Me.cbConsumables.Name = "cbConsumables"
        Me.cbConsumables.Size = New System.Drawing.Size(183, 21)
        Me.cbConsumables.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(286, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "نوع مواد مصرفی"
        '
        'dgvStack
        '
        Me.dgvStack.AllowUserToAddRows = False
        Me.dgvStack.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dgvStack.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvStack.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvStack.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvStack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvStack.Location = New System.Drawing.Point(16, 104)
        Me.dgvStack.MultiSelect = False
        Me.dgvStack.Name = "dgvStack"
        Me.dgvStack.ReadOnly = True
        Me.dgvStack.RowHeadersWidth = 21
        Me.dgvStack.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvStack.Size = New System.Drawing.Size(563, 233)
        Me.dgvStack.TabIndex = 6
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnInsertRecord)
        Me.GroupBox1.Controls.Add(Me.txtCosumableCount)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cbConsumables)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 350)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(384, 100)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "ثبت رسید انبار"
        '
        'btnInsertRecord
        '
        Me.btnInsertRecord.Location = New System.Drawing.Point(97, 60)
        Me.btnInsertRecord.Name = "btnInsertRecord"
        Me.btnInsertRecord.Size = New System.Drawing.Size(76, 23)
        Me.btnInsertRecord.TabIndex = 6
        Me.btnInsertRecord.Text = "ثبت"
        Me.btnInsertRecord.UseVisualStyleBackColor = True
        '
        'txtCosumableCount
        '
        Me.txtCosumableCount.Location = New System.Drawing.Point(178, 60)
        Me.txtCosumableCount.Name = "txtCosumableCount"
        Me.txtCosumableCount.Size = New System.Drawing.Size(100, 21)
        Me.txtCosumableCount.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(286, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "تعداد دریافتی"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(503, 350)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(76, 23)
        Me.btnRefresh.TabIndex = 24
        Me.btnRefresh.Text = "بروزرسانی"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnRemainAll)
        Me.GroupBox2.Controls.Add(Me.btnRemainBranch)
        Me.GroupBox2.Controls.Add(Me.txtBranchCode)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 36)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(563, 53)
        Me.GroupBox2.TabIndex = 25
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "GroupBox2"
        '
        'btnRemainAll
        '
        Me.btnRemainAll.Location = New System.Drawing.Point(21, 20)
        Me.btnRemainAll.Name = "btnRemainAll"
        Me.btnRemainAll.Size = New System.Drawing.Size(124, 23)
        Me.btnRemainAll.TabIndex = 10
        Me.btnRemainAll.Text = "باقیمانده تمامی شعب"
        Me.btnRemainAll.UseVisualStyleBackColor = True
        '
        'btnRemainBranch
        '
        Me.btnRemainBranch.Location = New System.Drawing.Point(221, 18)
        Me.btnRemainBranch.Name = "btnRemainBranch"
        Me.btnRemainBranch.Size = New System.Drawing.Size(101, 23)
        Me.btnRemainBranch.TabIndex = 9
        Me.btnRemainBranch.Text = "باقیمانده شعبه"
        Me.btnRemainBranch.UseVisualStyleBackColor = True
        '
        'txtBranchCode
        '
        Me.txtBranchCode.AcceptsReturn = True
        Me.txtBranchCode.Location = New System.Drawing.Point(361, 20)
        Me.txtBranchCode.Name = "txtBranchCode"
        Me.txtBranchCode.Size = New System.Drawing.Size(113, 21)
        Me.txtBranchCode.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(482, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "شعبه"
        '
        'ConsumablesStack
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 460)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvStack)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ConsumablesStack"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "انبار مواد مصرفی"
        CType(Me.dgvStack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbConsumables As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dgvStack As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnInsertRecord As System.Windows.Forms.Button
    Friend WithEvents txtCosumableCount As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRemainAll As System.Windows.Forms.Button
    Friend WithEvents btnRemainBranch As System.Windows.Forms.Button
    Friend WithEvents txtBranchCode As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
