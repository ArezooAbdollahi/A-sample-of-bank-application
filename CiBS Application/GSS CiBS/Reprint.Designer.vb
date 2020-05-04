<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reprint
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPAN = New System.Windows.Forms.TextBox()
        Me.txtCustName = New System.Windows.Forms.TextBox()
        Me.lblCustName = New System.Windows.Forms.Label()
        Me.txtAccNo = New System.Windows.Forms.TextBox()
        Me.lblAccountNo = New System.Windows.Forms.Label()
        Me.txtCustNo = New System.Windows.Forms.TextBox()
        Me.lblCuno = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCardType = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSendLog = New System.Windows.Forms.Button()
        Me.btnReprint = New System.Windows.Forms.Button()
        Me.txtCustFamilyName = New System.Windows.Forms.TextBox()
        Me.lblCustFamilyName = New System.Windows.Forms.Label()
        Me.txtSpecialText = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnChangeStatus = New System.Windows.Forms.Button()
        Me.lblCardStatus = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(533, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "شماره کارت"
        '
        'txtPAN
        '
        Me.txtPAN.Enabled = False
        Me.txtPAN.Location = New System.Drawing.Point(366, 57)
        Me.txtPAN.Name = "txtPAN"
        Me.txtPAN.ReadOnly = True
        Me.txtPAN.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtPAN.Size = New System.Drawing.Size(161, 21)
        Me.txtPAN.TabIndex = 1
        '
        'txtCustName
        '
        Me.txtCustName.Enabled = False
        Me.txtCustName.Location = New System.Drawing.Point(367, 84)
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.ReadOnly = True
        Me.txtCustName.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtCustName.Size = New System.Drawing.Size(160, 21)
        Me.txtCustName.TabIndex = 3
        '
        'lblCustName
        '
        Me.lblCustName.AutoSize = True
        Me.lblCustName.Location = New System.Drawing.Point(534, 87)
        Me.lblCustName.Name = "lblCustName"
        Me.lblCustName.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblCustName.Size = New System.Drawing.Size(60, 13)
        Me.lblCustName.TabIndex = 2
        Me.lblCustName.Text = "نام مشتری"
        '
        'txtAccNo
        '
        Me.txtAccNo.Enabled = False
        Me.txtAccNo.Location = New System.Drawing.Point(37, 111)
        Me.txtAccNo.Name = "txtAccNo"
        Me.txtAccNo.ReadOnly = True
        Me.txtAccNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtAccNo.Size = New System.Drawing.Size(160, 21)
        Me.txtAccNo.TabIndex = 9
        '
        'lblAccountNo
        '
        Me.lblAccountNo.AutoSize = True
        Me.lblAccountNo.Location = New System.Drawing.Point(203, 114)
        Me.lblAccountNo.Name = "lblAccountNo"
        Me.lblAccountNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblAccountNo.Size = New System.Drawing.Size(71, 13)
        Me.lblAccountNo.TabIndex = 8
        Me.lblAccountNo.Text = "شماره حساب"
        '
        'txtCustNo
        '
        Me.txtCustNo.Enabled = False
        Me.txtCustNo.Location = New System.Drawing.Point(367, 111)
        Me.txtCustNo.Name = "txtCustNo"
        Me.txtCustNo.ReadOnly = True
        Me.txtCustNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtCustNo.Size = New System.Drawing.Size(160, 21)
        Me.txtCustNo.TabIndex = 7
        '
        'lblCuno
        '
        Me.lblCuno.AutoSize = True
        Me.lblCuno.Location = New System.Drawing.Point(533, 114)
        Me.lblCuno.Name = "lblCuno"
        Me.lblCuno.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblCuno.Size = New System.Drawing.Size(77, 13)
        Me.lblCuno.TabIndex = 6
        Me.lblCuno.Text = "شماره مشتری"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(385, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label6.Size = New System.Drawing.Size(65, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "وضعیت کارت"
        '
        'txtCardType
        '
        Me.txtCardType.Enabled = False
        Me.txtCardType.Location = New System.Drawing.Point(37, 57)
        Me.txtCardType.Name = "txtCardType"
        Me.txtCardType.ReadOnly = True
        Me.txtCardType.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtCardType.Size = New System.Drawing.Size(160, 21)
        Me.txtCardType.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(203, 60)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label7.Size = New System.Drawing.Size(46, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "نوع کارت"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(325, 174)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnClose.Size = New System.Drawing.Size(90, 23)
        Me.btnClose.TabIndex = 14
        Me.btnClose.Text = "بازگشت"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSendLog
        '
        Me.btnSendLog.Location = New System.Drawing.Point(517, 174)
        Me.btnSendLog.Name = "btnSendLog"
        Me.btnSendLog.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnSendLog.Size = New System.Drawing.Size(90, 23)
        Me.btnSendLog.TabIndex = 15
        Me.btnSendLog.Text = "اعلان وضعیت"
        Me.btnSendLog.UseVisualStyleBackColor = True
        '
        'btnReprint
        '
        Me.btnReprint.Enabled = False
        Me.btnReprint.Location = New System.Drawing.Point(421, 174)
        Me.btnReprint.Name = "btnReprint"
        Me.btnReprint.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnReprint.Size = New System.Drawing.Size(90, 23)
        Me.btnReprint.TabIndex = 16
        Me.btnReprint.Text = "چاپ مجدد"
        Me.btnReprint.UseVisualStyleBackColor = True
        '
        'txtCustFamilyName
        '
        Me.txtCustFamilyName.Enabled = False
        Me.txtCustFamilyName.Location = New System.Drawing.Point(37, 84)
        Me.txtCustFamilyName.Name = "txtCustFamilyName"
        Me.txtCustFamilyName.ReadOnly = True
        Me.txtCustFamilyName.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtCustFamilyName.Size = New System.Drawing.Size(160, 21)
        Me.txtCustFamilyName.TabIndex = 18
        '
        'lblCustFamilyName
        '
        Me.lblCustFamilyName.AutoSize = True
        Me.lblCustFamilyName.Location = New System.Drawing.Point(203, 87)
        Me.lblCustFamilyName.Name = "lblCustFamilyName"
        Me.lblCustFamilyName.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblCustFamilyName.Size = New System.Drawing.Size(105, 13)
        Me.lblCustFamilyName.TabIndex = 17
        Me.lblCustFamilyName.Text = "نام خانوادگی مشتری"
        '
        'txtSpecialText
        '
        Me.txtSpecialText.Enabled = False
        Me.txtSpecialText.Location = New System.Drawing.Point(37, 138)
        Me.txtSpecialText.Name = "txtSpecialText"
        Me.txtSpecialText.ReadOnly = True
        Me.txtSpecialText.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSpecialText.Size = New System.Drawing.Size(490, 21)
        Me.txtSpecialText.TabIndex = 20
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(534, 141)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label8.Size = New System.Drawing.Size(51, 13)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "متن خاص"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.btnChangeStatus)
        Me.GroupBox1.Controls.Add(Me.lblCardStatus)
        Me.GroupBox1.Controls.Add(Me.txtAccNo)
        Me.GroupBox1.Controls.Add(Me.btnSendLog)
        Me.GroupBox1.Controls.Add(Me.lblAccountNo)
        Me.GroupBox1.Controls.Add(Me.txtCardType)
        Me.GroupBox1.Controls.Add(Me.txtCustFamilyName)
        Me.GroupBox1.Controls.Add(Me.lblCustFamilyName)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtSpecialText)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtPAN)
        Me.GroupBox1.Controls.Add(Me.lblCustName)
        Me.GroupBox1.Controls.Add(Me.txtCustName)
        Me.GroupBox1.Controls.Add(Me.btnReprint)
        Me.GroupBox1.Controls.Add(Me.lblCuno)
        Me.GroupBox1.Controls.Add(Me.txtCustNo)
        Me.GroupBox1.Controls.Add(Me.btnClose)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(633, 211)
        Me.GroupBox1.TabIndex = 21
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "آخرین کارت صادره"
        '
        'Button1
        '
        Me.Button1.Enabled = False
        Me.Button1.Location = New System.Drawing.Point(214, 174)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 23
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'btnChangeStatus
        '
        Me.btnChangeStatus.Location = New System.Drawing.Point(37, 174)
        Me.btnChangeStatus.Name = "btnChangeStatus"
        Me.btnChangeStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnChangeStatus.Size = New System.Drawing.Size(171, 23)
        Me.btnChangeStatus.TabIndex = 22
        Me.btnChangeStatus.Text = "تغییر وضعیت به چاپ شده"
        Me.btnChangeStatus.UseVisualStyleBackColor = True
        Me.btnChangeStatus.Visible = False
        '
        'lblCardStatus
        '
        Me.lblCardStatus.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCardStatus.ForeColor = System.Drawing.Color.Maroon
        Me.lblCardStatus.Location = New System.Drawing.Point(37, 16)
        Me.lblCardStatus.Name = "lblCardStatus"
        Me.lblCardStatus.ReadOnly = True
        Me.lblCardStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblCardStatus.Size = New System.Drawing.Size(342, 27)
        Me.lblCardStatus.TabIndex = 21
        Me.lblCardStatus.TabStop = False
        '
        'Reprint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(654, 230)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Reprint"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "بررسی وضعیت  / چاپ مجدد آخرین کارت صادره"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPAN As System.Windows.Forms.TextBox
    Friend WithEvents txtCustName As System.Windows.Forms.TextBox
    Friend WithEvents lblCustName As System.Windows.Forms.Label
    Friend WithEvents txtAccNo As System.Windows.Forms.TextBox
    Friend WithEvents lblAccountNo As System.Windows.Forms.Label
    Friend WithEvents txtCustNo As System.Windows.Forms.TextBox
    Friend WithEvents lblCuno As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCardType As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSendLog As System.Windows.Forms.Button
    Friend WithEvents btnReprint As System.Windows.Forms.Button
    Friend WithEvents txtCustFamilyName As System.Windows.Forms.TextBox
    Friend WithEvents lblCustFamilyName As System.Windows.Forms.Label
    Friend WithEvents txtSpecialText As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblCardStatus As System.Windows.Forms.TextBox
    Friend WithEvents btnChangeStatus As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
