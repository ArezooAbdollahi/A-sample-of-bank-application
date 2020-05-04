<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RequestConsumables
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
        Me.txtConsumableRequestCount = New System.Windows.Forms.TextBox()
        Me.btnAddRequest = New System.Windows.Forms.Button()
        Me.dgvRequests = New System.Windows.Forms.DataGridView()
        Me.btnDeletRequest = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.gbReceive = New System.Windows.Forms.GroupBox()
        Me.lblAcceptedCount = New System.Windows.Forms.TextBox()
        Me.lblConsumableType = New System.Windows.Forms.TextBox()
        Me.btnAccept = New System.Windows.Forms.Button()
        Me.txtRecievedCount = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBranchCode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.dgvRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbReceive.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(184, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "مواد مصرفی مورد نیاز"
        '
        'cbConsumables
        '
        Me.cbConsumables.FormattingEnabled = True
        Me.cbConsumables.Location = New System.Drawing.Point(288, 19)
        Me.cbConsumables.Name = "cbConsumables"
        Me.cbConsumables.Size = New System.Drawing.Size(156, 21)
        Me.cbConsumables.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(457, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "مقدار مورد نیاز"
        '
        'txtConsumableRequestCount
        '
        Me.txtConsumableRequestCount.Location = New System.Drawing.Point(527, 19)
        Me.txtConsumableRequestCount.Name = "txtConsumableRequestCount"
        Me.txtConsumableRequestCount.Size = New System.Drawing.Size(64, 21)
        Me.txtConsumableRequestCount.TabIndex = 3
        '
        'btnAddRequest
        '
        Me.btnAddRequest.Location = New System.Drawing.Point(599, 17)
        Me.btnAddRequest.Name = "btnAddRequest"
        Me.btnAddRequest.Size = New System.Drawing.Size(97, 23)
        Me.btnAddRequest.TabIndex = 4
        Me.btnAddRequest.Text = "ارسال درخواست"
        Me.btnAddRequest.UseVisualStyleBackColor = True
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
        Me.dgvRequests.Location = New System.Drawing.Point(17, 59)
        Me.dgvRequests.MultiSelect = False
        Me.dgvRequests.Name = "dgvRequests"
        Me.dgvRequests.ReadOnly = True
        Me.dgvRequests.RowHeadersWidth = 21
        Me.dgvRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvRequests.Size = New System.Drawing.Size(888, 156)
        Me.dgvRequests.TabIndex = 5
        '
        'btnDeletRequest
        '
        Me.btnDeletRequest.Location = New System.Drawing.Point(701, 17)
        Me.btnDeletRequest.Name = "btnDeletRequest"
        Me.btnDeletRequest.Size = New System.Drawing.Size(97, 23)
        Me.btnDeletRequest.TabIndex = 6
        Me.btnDeletRequest.Text = "رد درخواست"
        Me.btnDeletRequest.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(804, 17)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(101, 23)
        Me.btnRefresh.TabIndex = 23
        Me.btnRefresh.Text = "بروزرسانی"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'gbReceive
        '
        Me.gbReceive.Controls.Add(Me.lblAcceptedCount)
        Me.gbReceive.Controls.Add(Me.lblConsumableType)
        Me.gbReceive.Controls.Add(Me.btnAccept)
        Me.gbReceive.Controls.Add(Me.txtRecievedCount)
        Me.gbReceive.Controls.Add(Me.Label6)
        Me.gbReceive.Controls.Add(Me.Label4)
        Me.gbReceive.Controls.Add(Me.Label3)
        Me.gbReceive.Location = New System.Drawing.Point(22, 227)
        Me.gbReceive.Name = "gbReceive"
        Me.gbReceive.Size = New System.Drawing.Size(336, 100)
        Me.gbReceive.TabIndex = 26
        Me.gbReceive.TabStop = False
        Me.gbReceive.Text = "تایید دریافت مواد مصرفی در شعبه"
        '
        'lblAcceptedCount
        '
        Me.lblAcceptedCount.ForeColor = System.Drawing.Color.Black
        Me.lblAcceptedCount.Location = New System.Drawing.Point(34, 44)
        Me.lblAcceptedCount.MaxLength = 50
        Me.lblAcceptedCount.Name = "lblAcceptedCount"
        Me.lblAcceptedCount.ReadOnly = True
        Me.lblAcceptedCount.Size = New System.Drawing.Size(188, 21)
        Me.lblAcceptedCount.TabIndex = 49
        Me.lblAcceptedCount.TabStop = False
        '
        'lblConsumableType
        '
        Me.lblConsumableType.ForeColor = System.Drawing.Color.Black
        Me.lblConsumableType.Location = New System.Drawing.Point(34, 17)
        Me.lblConsumableType.MaxLength = 50
        Me.lblConsumableType.Name = "lblConsumableType"
        Me.lblConsumableType.ReadOnly = True
        Me.lblConsumableType.Size = New System.Drawing.Size(188, 21)
        Me.lblConsumableType.TabIndex = 48
        Me.lblConsumableType.TabStop = False
        '
        'btnAccept
        '
        Me.btnAccept.Enabled = False
        Me.btnAccept.Location = New System.Drawing.Point(34, 69)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(101, 23)
        Me.btnAccept.TabIndex = 34
        Me.btnAccept.Text = "تایید دریافت"
        Me.btnAccept.UseVisualStyleBackColor = True
        '
        'txtRecievedCount
        '
        Me.txtRecievedCount.ForeColor = System.Drawing.Color.Navy
        Me.txtRecievedCount.Location = New System.Drawing.Point(153, 71)
        Me.txtRecievedCount.Name = "txtRecievedCount"
        Me.txtRecievedCount.Size = New System.Drawing.Size(69, 21)
        Me.txtRecievedCount.TabIndex = 33
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(225, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 13)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "تعداد تایید شده"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(225, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 13)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "تعداد دریافت شده"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(225, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 13)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "نوع مواد مصرفی"
        '
        'txtBranchCode
        '
        Me.txtBranchCode.Location = New System.Drawing.Point(66, 19)
        Me.txtBranchCode.Name = "txtBranchCode"
        Me.txtBranchCode.Size = New System.Drawing.Size(100, 21)
        Me.txtBranchCode.TabIndex = 27
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 13)
        Me.Label5.TabIndex = 28
        Me.Label5.Text = "کد شعبه"
        '
        'RequestConsumables
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(916, 335)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtBranchCode)
        Me.Controls.Add(Me.gbReceive)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnDeletRequest)
        Me.Controls.Add(Me.dgvRequests)
        Me.Controls.Add(Me.btnAddRequest)
        Me.Controls.Add(Me.txtConsumableRequestCount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cbConsumables)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RequestConsumables"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "درخواست مواد مصرفی"
        CType(Me.dgvRequests, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbReceive.ResumeLayout(False)
        Me.gbReceive.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbConsumables As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtConsumableRequestCount As System.Windows.Forms.TextBox
    Friend WithEvents btnAddRequest As System.Windows.Forms.Button
    Friend WithEvents dgvRequests As System.Windows.Forms.DataGridView
    Friend WithEvents btnDeletRequest As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents gbReceive As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnAccept As System.Windows.Forms.Button
    Friend WithEvents txtRecievedCount As System.Windows.Forms.TextBox
    Friend WithEvents lblAcceptedCount As System.Windows.Forms.TextBox
    Friend WithEvents lblConsumableType As System.Windows.Forms.TextBox
    Friend WithEvents txtBranchCode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
