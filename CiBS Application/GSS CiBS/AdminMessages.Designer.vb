<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminMessages
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
        Me.dgvAdminMesages = New System.Windows.Forms.DataGridView()
        Me.txtFromDate = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtToDate = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cbStatus = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnAddMessage = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtMsg = New System.Windows.Forms.TextBox()
        Me.txtNewToDate = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNewFromdate = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnDeleteMessage = New System.Windows.Forms.Button()
        Me.btnEnableMessage = New System.Windows.Forms.Button()
        CType(Me.dgvAdminMesages, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvAdminMesages
        '
        Me.dgvAdminMesages.AllowUserToAddRows = False
        Me.dgvAdminMesages.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dgvAdminMesages.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvAdminMesages.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvAdminMesages.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvAdminMesages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAdminMesages.Location = New System.Drawing.Point(12, 48)
        Me.dgvAdminMesages.MultiSelect = False
        Me.dgvAdminMesages.Name = "dgvAdminMesages"
        Me.dgvAdminMesages.ReadOnly = True
        Me.dgvAdminMesages.RowHeadersWidth = 21
        Me.dgvAdminMesages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvAdminMesages.Size = New System.Drawing.Size(916, 156)
        Me.dgvAdminMesages.TabIndex = 8
        '
        'txtFromDate
        '
        Me.txtFromDate.Location = New System.Drawing.Point(57, 12)
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.Size = New System.Drawing.Size(87, 21)
        Me.txtFromDate.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "از تاریخ"
        '
        'txtToDate
        '
        Me.txtToDate.Location = New System.Drawing.Point(202, 12)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.Size = New System.Drawing.Size(87, 21)
        Me.txtToDate.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(159, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "تا تاریخ"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(306, 12)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(125, 23)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "به روز رسانی جدول"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbStatus)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.btnAddMessage)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtMsg)
        Me.GroupBox1.Controls.Add(Me.txtNewToDate)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtNewFromdate)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 210)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(916, 101)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "اضافه نمودن پیام"
        '
        'cbStatus
        '
        Me.cbStatus.FormattingEnabled = True
        Me.cbStatus.Items.AddRange(New Object() {"غیر فعال", "فعال"})
        Me.cbStatus.Location = New System.Drawing.Point(497, 19)
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.Size = New System.Drawing.Size(88, 21)
        Me.cbStatus.TabIndex = 15
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(590, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "وضعیت"
        '
        'btnAddMessage
        '
        Me.btnAddMessage.Location = New System.Drawing.Point(6, 71)
        Me.btnAddMessage.Name = "btnAddMessage"
        Me.btnAddMessage.Size = New System.Drawing.Size(125, 23)
        Me.btnAddMessage.TabIndex = 17
        Me.btnAddMessage.Text = "اضافه نمودن پیام"
        Me.btnAddMessage.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(458, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(24, 13)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "پیام"
        '
        'txtMsg
        '
        Me.txtMsg.Location = New System.Drawing.Point(6, 19)
        Me.txtMsg.Multiline = True
        Me.txtMsg.Name = "txtMsg"
        Me.txtMsg.Size = New System.Drawing.Size(446, 48)
        Me.txtMsg.TabIndex = 16
        '
        'txtNewToDate
        '
        Me.txtNewToDate.Location = New System.Drawing.Point(637, 19)
        Me.txtNewToDate.Name = "txtNewToDate"
        Me.txtNewToDate.Size = New System.Drawing.Size(87, 21)
        Me.txtNewToDate.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(730, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "تا تاریخ"
        '
        'txtNewFromdate
        '
        Me.txtNewFromdate.Location = New System.Drawing.Point(774, 19)
        Me.txtNewFromdate.Name = "txtNewFromdate"
        Me.txtNewFromdate.Size = New System.Drawing.Size(87, 21)
        Me.txtNewFromdate.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(867, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "از تاریخ"
        '
        'btnDeleteMessage
        '
        Me.btnDeleteMessage.Location = New System.Drawing.Point(568, 12)
        Me.btnDeleteMessage.Name = "btnDeleteMessage"
        Me.btnDeleteMessage.Size = New System.Drawing.Size(125, 23)
        Me.btnDeleteMessage.TabIndex = 5
        Me.btnDeleteMessage.Text = "حذف نمودن پیام"
        Me.btnDeleteMessage.UseVisualStyleBackColor = True
        '
        'btnEnableMessage
        '
        Me.btnEnableMessage.Location = New System.Drawing.Point(437, 12)
        Me.btnEnableMessage.Name = "btnEnableMessage"
        Me.btnEnableMessage.Size = New System.Drawing.Size(125, 23)
        Me.btnEnableMessage.TabIndex = 4
        Me.btnEnableMessage.Text = "تغییر وضعیت پیام"
        Me.btnEnableMessage.UseVisualStyleBackColor = True
        '
        'AdminMessages
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(940, 318)
        Me.Controls.Add(Me.btnEnableMessage)
        Me.Controls.Add(Me.btnDeleteMessage)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.txtToDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtFromDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dgvAdminMesages)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AdminMessages"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "مدیریت پیامهای مدیر سیستم"
        CType(Me.dgvAdminMesages, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvAdminMesages As System.Windows.Forms.DataGridView
    Friend WithEvents txtFromDate As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtToDate As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNewToDate As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNewFromdate As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnAddMessage As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtMsg As System.Windows.Forms.TextBox
    Friend WithEvents btnDeleteMessage As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents btnEnableMessage As System.Windows.Forms.Button
End Class
