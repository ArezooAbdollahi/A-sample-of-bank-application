<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserManagement
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtWinUserCode = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.BtnClear = New System.Windows.Forms.Button()
        Me.txtMainPosSerial = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.gbSLS = New System.Windows.Forms.GroupBox()
        Me.btnSavePersonalInfo = New System.Windows.Forms.Button()
        Me.txtMsg = New System.Windows.Forms.TextBox()
        Me.btnScanFinger = New System.Windows.Forms.Button()
        Me.pbScannedFinger = New System.Windows.Forms.PictureBox()
        Me.btnEnroll = New System.Windows.Forms.Button()
        Me.rbL1 = New System.Windows.Forms.RadioButton()
        Me.rbL2 = New System.Windows.Forms.RadioButton()
        Me.rbL3 = New System.Windows.Forms.RadioButton()
        Me.rbL4 = New System.Windows.Forms.RadioButton()
        Me.rbL5 = New System.Windows.Forms.RadioButton()
        Me.rbR5 = New System.Windows.Forms.RadioButton()
        Me.rbR4 = New System.Windows.Forms.RadioButton()
        Me.rbR3 = New System.Windows.Forms.RadioButton()
        Me.rbR2 = New System.Windows.Forms.RadioButton()
        Me.rbR1 = New System.Windows.Forms.RadioButton()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnUsersList = New System.Windows.Forms.Button()
        Me.btnSolveBlocked = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.cbStatus = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.cbGrantLevel = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBrCode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnInsert = New System.Windows.Forms.Button()
        Me.btnResetPassword = New System.Windows.Forms.Button()
        Me.txtPersFamily = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPersName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dgvUsers = New System.Windows.Forms.DataGridView()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtUsersCount = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnUpdatePos = New System.Windows.Forms.Button()
        Me.btnAddPos = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.btnSearchPos = New System.Windows.Forms.Button()
        Me.txtPosSerial = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtPosIP = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dgvPos = New System.Windows.Forms.DataGridView()
        Me.GroupBox1.SuspendLayout()
        Me.gbSLS.SuspendLayout()
        CType(Me.pbScannedFinger, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.dgvPos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(441, 16)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(82, 21)
        Me.txtUserName.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(537, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "کد پرسنلی"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtWinUserCode)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.BtnClear)
        Me.GroupBox1.Controls.Add(Me.txtMainPosSerial)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.btnDelete)
        Me.GroupBox1.Controls.Add(Me.gbSLS)
        Me.GroupBox1.Controls.Add(Me.btnUsersList)
        Me.GroupBox1.Controls.Add(Me.btnSolveBlocked)
        Me.GroupBox1.Controls.Add(Me.btnUpdate)
        Me.GroupBox1.Controls.Add(Me.cbStatus)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.cbGrantLevel)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtBrCode)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btnInsert)
        Me.GroupBox1.Controls.Add(Me.btnResetPassword)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtPersFamily)
        Me.GroupBox1.Controls.Add(Me.txtUserName)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtPersName)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(629, 280)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "اضافه / ویرایش کاربر"
        '
        'txtWinUserCode
        '
        Me.txtWinUserCode.Location = New System.Drawing.Point(402, 170)
        Me.txtWinUserCode.Name = "txtWinUserCode"
        Me.txtWinUserCode.Size = New System.Drawing.Size(121, 21)
        Me.txtWinUserCode.TabIndex = 7
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(538, 176)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(58, 13)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "کد ویندوزی"
        '
        'BtnClear
        '
        Me.BtnClear.Location = New System.Drawing.Point(321, 253)
        Me.BtnClear.Name = "BtnClear"
        Me.BtnClear.Size = New System.Drawing.Size(202, 27)
        Me.BtnClear.TabIndex = 15
        Me.BtnClear.Text = "پاک کردن فیلدها"
        Me.BtnClear.UseVisualStyleBackColor = True
        '
        'txtMainPosSerial
        '
        Me.txtMainPosSerial.Location = New System.Drawing.Point(402, 196)
        Me.txtMainPosSerial.Name = "txtMainPosSerial"
        Me.txtMainPosSerial.Size = New System.Drawing.Size(121, 21)
        Me.txtMainPosSerial.TabIndex = 8
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(546, 199)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(50, 13)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "PosSerial"
        '
        'btnDelete
        '
        Me.btnDelete.Enabled = False
        Me.btnDelete.Location = New System.Drawing.Point(4, 224)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(99, 23)
        Me.btnDelete.TabIndex = 14
        Me.btnDelete.Text = "حذف کردن کاربر"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'gbSLS
        '
        Me.gbSLS.Controls.Add(Me.btnSavePersonalInfo)
        Me.gbSLS.Controls.Add(Me.txtMsg)
        Me.gbSLS.Controls.Add(Me.btnScanFinger)
        Me.gbSLS.Controls.Add(Me.pbScannedFinger)
        Me.gbSLS.Controls.Add(Me.btnEnroll)
        Me.gbSLS.Controls.Add(Me.rbL1)
        Me.gbSLS.Controls.Add(Me.rbL2)
        Me.gbSLS.Controls.Add(Me.rbL3)
        Me.gbSLS.Controls.Add(Me.rbL4)
        Me.gbSLS.Controls.Add(Me.rbL5)
        Me.gbSLS.Controls.Add(Me.rbR5)
        Me.gbSLS.Controls.Add(Me.rbR4)
        Me.gbSLS.Controls.Add(Me.rbR3)
        Me.gbSLS.Controls.Add(Me.rbR2)
        Me.gbSLS.Controls.Add(Me.rbR1)
        Me.gbSLS.Controls.Add(Me.PictureBox1)
        Me.gbSLS.Enabled = False
        Me.gbSLS.Location = New System.Drawing.Point(4, 4)
        Me.gbSLS.Name = "gbSLS"
        Me.gbSLS.Size = New System.Drawing.Size(328, 208)
        Me.gbSLS.TabIndex = 14
        Me.gbSLS.TabStop = False
        Me.gbSLS.Text = "اطلاعات اثر انگشت"
        '
        'btnSavePersonalInfo
        '
        Me.btnSavePersonalInfo.Location = New System.Drawing.Point(3, 173)
        Me.btnSavePersonalInfo.Name = "btnSavePersonalInfo"
        Me.btnSavePersonalInfo.Size = New System.Drawing.Size(70, 23)
        Me.btnSavePersonalInfo.TabIndex = 50
        Me.btnSavePersonalInfo.Text = "ذخیره اطلاعات فردی"
        Me.btnSavePersonalInfo.UseVisualStyleBackColor = True
        Me.btnSavePersonalInfo.Visible = False
        '
        'txtMsg
        '
        Me.txtMsg.Location = New System.Drawing.Point(79, 175)
        Me.txtMsg.Name = "txtMsg"
        Me.txtMsg.ReadOnly = True
        Me.txtMsg.Size = New System.Drawing.Size(246, 21)
        Me.txtMsg.TabIndex = 35
        '
        'btnScanFinger
        '
        Me.btnScanFinger.Location = New System.Drawing.Point(210, 146)
        Me.btnScanFinger.Name = "btnScanFinger"
        Me.btnScanFinger.Size = New System.Drawing.Size(104, 23)
        Me.btnScanFinger.TabIndex = 49
        Me.btnScanFinger.Text = "اسکن اثر انگشت"
        Me.btnScanFinger.UseVisualStyleBackColor = True
        '
        'pbScannedFinger
        '
        Me.pbScannedFinger.Location = New System.Drawing.Point(3, 12)
        Me.pbScannedFinger.Name = "pbScannedFinger"
        Me.pbScannedFinger.Size = New System.Drawing.Size(70, 157)
        Me.pbScannedFinger.TabIndex = 48
        Me.pbScannedFinger.TabStop = False
        Me.pbScannedFinger.Visible = False
        '
        'btnEnroll
        '
        Me.btnEnroll.Enabled = False
        Me.btnEnroll.Location = New System.Drawing.Point(91, 146)
        Me.btnEnroll.Name = "btnEnroll"
        Me.btnEnroll.Size = New System.Drawing.Size(104, 23)
        Me.btnEnroll.TabIndex = 47
        Me.btnEnroll.Text = "ذخیره اثر انگشت"
        Me.btnEnroll.UseVisualStyleBackColor = True
        '
        'rbL1
        '
        Me.rbL1.AutoSize = True
        Me.rbL1.Location = New System.Drawing.Point(91, 46)
        Me.rbL1.Name = "rbL1"
        Me.rbL1.Size = New System.Drawing.Size(14, 13)
        Me.rbL1.TabIndex = 46
        Me.rbL1.TabStop = True
        Me.rbL1.UseVisualStyleBackColor = True
        '
        'rbL2
        '
        Me.rbL2.AutoSize = True
        Me.rbL2.Location = New System.Drawing.Point(111, 35)
        Me.rbL2.Name = "rbL2"
        Me.rbL2.Size = New System.Drawing.Size(14, 13)
        Me.rbL2.TabIndex = 45
        Me.rbL2.TabStop = True
        Me.rbL2.UseVisualStyleBackColor = True
        '
        'rbL3
        '
        Me.rbL3.AutoSize = True
        Me.rbL3.Location = New System.Drawing.Point(132, 30)
        Me.rbL3.Name = "rbL3"
        Me.rbL3.Size = New System.Drawing.Size(14, 13)
        Me.rbL3.TabIndex = 44
        Me.rbL3.TabStop = True
        Me.rbL3.UseVisualStyleBackColor = True
        '
        'rbL4
        '
        Me.rbL4.AutoSize = True
        Me.rbL4.Location = New System.Drawing.Point(152, 36)
        Me.rbL4.Name = "rbL4"
        Me.rbL4.Size = New System.Drawing.Size(14, 13)
        Me.rbL4.TabIndex = 43
        Me.rbL4.TabStop = True
        Me.rbL4.UseVisualStyleBackColor = True
        '
        'rbL5
        '
        Me.rbL5.AutoSize = True
        Me.rbL5.Location = New System.Drawing.Point(175, 63)
        Me.rbL5.Name = "rbL5"
        Me.rbL5.Size = New System.Drawing.Size(14, 13)
        Me.rbL5.TabIndex = 42
        Me.rbL5.TabStop = True
        Me.rbL5.UseVisualStyleBackColor = True
        '
        'rbR5
        '
        Me.rbR5.AutoSize = True
        Me.rbR5.Location = New System.Drawing.Point(300, 47)
        Me.rbR5.Name = "rbR5"
        Me.rbR5.Size = New System.Drawing.Size(14, 13)
        Me.rbR5.TabIndex = 41
        Me.rbR5.TabStop = True
        Me.rbR5.UseVisualStyleBackColor = True
        '
        'rbR4
        '
        Me.rbR4.AutoSize = True
        Me.rbR4.Location = New System.Drawing.Point(279, 36)
        Me.rbR4.Name = "rbR4"
        Me.rbR4.Size = New System.Drawing.Size(14, 13)
        Me.rbR4.TabIndex = 40
        Me.rbR4.TabStop = True
        Me.rbR4.UseVisualStyleBackColor = True
        '
        'rbR3
        '
        Me.rbR3.AutoSize = True
        Me.rbR3.Location = New System.Drawing.Point(258, 30)
        Me.rbR3.Name = "rbR3"
        Me.rbR3.Size = New System.Drawing.Size(14, 13)
        Me.rbR3.TabIndex = 39
        Me.rbR3.TabStop = True
        Me.rbR3.UseVisualStyleBackColor = True
        '
        'rbR2
        '
        Me.rbR2.AutoSize = True
        Me.rbR2.Location = New System.Drawing.Point(237, 36)
        Me.rbR2.Name = "rbR2"
        Me.rbR2.Size = New System.Drawing.Size(14, 13)
        Me.rbR2.TabIndex = 38
        Me.rbR2.TabStop = True
        Me.rbR2.UseVisualStyleBackColor = True
        '
        'rbR1
        '
        Me.rbR1.AutoSize = True
        Me.rbR1.Location = New System.Drawing.Point(215, 63)
        Me.rbR1.Name = "rbR1"
        Me.rbR1.Size = New System.Drawing.Size(14, 13)
        Me.rbR1.TabIndex = 37
        Me.rbR1.TabStop = True
        Me.rbR1.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.CIBS.My.Resources.Resources.HANDs
        Me.PictureBox1.Location = New System.Drawing.Point(79, 15)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(246, 125)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 36
        Me.PictureBox1.TabStop = False
        '
        'btnUsersList
        '
        Me.btnUsersList.Location = New System.Drawing.Point(109, 224)
        Me.btnUsersList.Name = "btnUsersList"
        Me.btnUsersList.Size = New System.Drawing.Size(104, 23)
        Me.btnUsersList.TabIndex = 13
        Me.btnUsersList.Text = "لیست کاربران"
        Me.btnUsersList.UseVisualStyleBackColor = True
        '
        'btnSolveBlocked
        '
        Me.btnSolveBlocked.Enabled = False
        Me.btnSolveBlocked.Location = New System.Drawing.Point(321, 224)
        Me.btnSolveBlocked.Name = "btnSolveBlocked"
        Me.btnSolveBlocked.Size = New System.Drawing.Size(99, 23)
        Me.btnSolveBlocked.TabIndex = 11
        Me.btnSolveBlocked.Text = "رفع مسدودی"
        Me.btnSolveBlocked.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Enabled = False
        Me.btnUpdate.Location = New System.Drawing.Point(219, 224)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(99, 23)
        Me.btnUpdate.TabIndex = 12
        Me.btnUpdate.Text = "ذخیره تغییرات"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'cbStatus
        '
        Me.cbStatus.FormattingEnabled = True
        Me.cbStatus.Items.AddRange(New Object() {"غیرفعال", "فعال"})
        Me.cbStatus.Location = New System.Drawing.Point(402, 144)
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.Size = New System.Drawing.Size(121, 21)
        Me.cbStatus.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(537, 147)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "وضعیت"
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(360, 13)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 1
        Me.btnSearch.Text = "یافتن کاربر"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'cbGrantLevel
        '
        Me.cbGrantLevel.Enabled = False
        Me.cbGrantLevel.FormattingEnabled = True
        Me.cbGrantLevel.ItemHeight = 13
        Me.cbGrantLevel.Items.AddRange(New Object() {"کاربر شعبه", "کاربر ارشد شعبه", "مدیر سیستم", "کاربر پشتیبانی"})
        Me.cbGrantLevel.Location = New System.Drawing.Point(402, 118)
        Me.cbGrantLevel.Name = "cbGrantLevel"
        Me.cbGrantLevel.Size = New System.Drawing.Size(121, 21)
        Me.cbGrantLevel.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(537, 118)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "سطح دسترسی"
        '
        'txtBrCode
        '
        Me.txtBrCode.Enabled = False
        Me.txtBrCode.Location = New System.Drawing.Point(441, 91)
        Me.txtBrCode.Name = "txtBrCode"
        Me.txtBrCode.Size = New System.Drawing.Size(82, 21)
        Me.txtBrCode.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(537, 94)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "کد شعبه"
        '
        'btnInsert
        '
        Me.btnInsert.Enabled = False
        Me.btnInsert.Location = New System.Drawing.Point(424, 224)
        Me.btnInsert.Name = "btnInsert"
        Me.btnInsert.Size = New System.Drawing.Size(99, 23)
        Me.btnInsert.TabIndex = 9
        Me.btnInsert.Text = "افزودن کاربر"
        Me.btnInsert.UseVisualStyleBackColor = True
        '
        'btnResetPassword
        '
        Me.btnResetPassword.Enabled = False
        Me.btnResetPassword.Location = New System.Drawing.Point(114, 257)
        Me.btnResetPassword.Name = "btnResetPassword"
        Me.btnResetPassword.Size = New System.Drawing.Size(99, 23)
        Me.btnResetPassword.TabIndex = 10
        Me.btnResetPassword.Text = "به روز رسانی رمز"
        Me.btnResetPassword.UseVisualStyleBackColor = True
        Me.btnResetPassword.Visible = False
        '
        'txtPersFamily
        '
        Me.txtPersFamily.Location = New System.Drawing.Point(360, 66)
        Me.txtPersFamily.Name = "txtPersFamily"
        Me.txtPersFamily.Size = New System.Drawing.Size(163, 21)
        Me.txtPersFamily.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(537, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "نام خانوادگی کاربر"
        '
        'txtPersName
        '
        Me.txtPersName.Location = New System.Drawing.Point(360, 41)
        Me.txtPersName.Name = "txtPersName"
        Me.txtPersName.Size = New System.Drawing.Size(163, 21)
        Me.txtPersName.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(538, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "نام کاربر"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgvUsers)
        Me.GroupBox2.Location = New System.Drawing.Point(14, 289)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(629, 225)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "لیست کاربران "
        '
        'dgvUsers
        '
        Me.dgvUsers.AllowUserToAddRows = False
        Me.dgvUsers.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dgvUsers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvUsers.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUsers.Location = New System.Drawing.Point(4, 20)
        Me.dgvUsers.MultiSelect = False
        Me.dgvUsers.Name = "dgvUsers"
        Me.dgvUsers.ReadOnly = True
        Me.dgvUsers.RowHeadersWidth = 21
        Me.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvUsers.Size = New System.Drawing.Size(619, 191)
        Me.dgvUsers.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 517)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 13)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "تعداد کاربران :"
        '
        'txtUsersCount
        '
        Me.txtUsersCount.Location = New System.Drawing.Point(93, 516)
        Me.txtUsersCount.Name = "txtUsersCount"
        Me.txtUsersCount.ReadOnly = True
        Me.txtUsersCount.Size = New System.Drawing.Size(64, 21)
        Me.txtUsersCount.TabIndex = 13
        Me.txtUsersCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Button1)
        Me.GroupBox3.Controls.Add(Me.btnUpdatePos)
        Me.GroupBox3.Controls.Add(Me.btnAddPos)
        Me.GroupBox3.Controls.Add(Me.Button2)
        Me.GroupBox3.Controls.Add(Me.btnSearchPos)
        Me.GroupBox3.Controls.Add(Me.txtPosSerial)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.txtPosIP)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Enabled = False
        Me.GroupBox3.Location = New System.Drawing.Point(649, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(421, 271)
        Me.GroupBox3.TabIndex = 14
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "پوز ها"
        Me.GroupBox3.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(75, 215)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(114, 35)
        Me.Button1.TabIndex = 24
        Me.Button1.Text = "پاک کردن فیلدها"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnUpdatePos
        '
        Me.btnUpdatePos.Location = New System.Drawing.Point(75, 167)
        Me.btnUpdatePos.Name = "btnUpdatePos"
        Me.btnUpdatePos.Size = New System.Drawing.Size(114, 35)
        Me.btnUpdatePos.TabIndex = 19
        Me.btnUpdatePos.Text = "ذخیره تغییرات"
        Me.btnUpdatePos.UseVisualStyleBackColor = True
        '
        'btnAddPos
        '
        Me.btnAddPos.Location = New System.Drawing.Point(240, 215)
        Me.btnAddPos.Name = "btnAddPos"
        Me.btnAddPos.Size = New System.Drawing.Size(119, 35)
        Me.btnAddPos.TabIndex = 18
        Me.btnAddPos.Text = "افزودن پوز"
        Me.btnAddPos.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(240, 167)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(119, 35)
        Me.Button2.TabIndex = 18
        Me.Button2.Text = "لیست پوزها"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'btnSearchPos
        '
        Me.btnSearchPos.Location = New System.Drawing.Point(90, 66)
        Me.btnSearchPos.Name = "btnSearchPos"
        Me.btnSearchPos.Size = New System.Drawing.Size(64, 24)
        Me.btnSearchPos.TabIndex = 20
        Me.btnSearchPos.Text = "یافتن Pos"
        Me.btnSearchPos.UseVisualStyleBackColor = True
        '
        'txtPosSerial
        '
        Me.txtPosSerial.Location = New System.Drawing.Point(160, 69)
        Me.txtPosSerial.Name = "txtPosSerial"
        Me.txtPosSerial.Size = New System.Drawing.Size(165, 21)
        Me.txtPosSerial.TabIndex = 18
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(332, 111)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(34, 13)
        Me.Label12.TabIndex = 19
        Me.Label12.Text = "PosIP"
        '
        'txtPosIP
        '
        Me.txtPosIP.Location = New System.Drawing.Point(160, 108)
        Me.txtPosIP.Name = "txtPosIP"
        Me.txtPosIP.Size = New System.Drawing.Size(165, 21)
        Me.txtPosIP.TabIndex = 23
        '
        'Label10
        '
        Me.Label10.AccessibleDescription = ""
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(331, 72)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(50, 13)
        Me.Label10.TabIndex = 21
        Me.Label10.Text = "PosSerial"
        '
        'dgvPos
        '
        Me.dgvPos.AllowUserToAddRows = False
        Me.dgvPos.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dgvPos.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvPos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvPos.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvPos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPos.Location = New System.Drawing.Point(660, 311)
        Me.dgvPos.MultiSelect = False
        Me.dgvPos.Name = "dgvPos"
        Me.dgvPos.ReadOnly = True
        Me.dgvPos.RowHeadersWidth = 21
        Me.dgvPos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPos.Size = New System.Drawing.Size(402, 191)
        Me.dgvPos.TabIndex = 7
        Me.dgvPos.Visible = False
        '
        'UserManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(643, 543)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.txtUsersCount)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvPos)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UserManagement"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gbSLS.ResumeLayout(False)
        Me.gbSLS.PerformLayout()
        CType(Me.pbScannedFinger, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.dgvPos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPersFamily As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPersName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnResetPassword As System.Windows.Forms.Button
    Friend WithEvents btnInsert As System.Windows.Forms.Button


    Private Sub UserManagement_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Friend WithEvents txtBrCode As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbGrantLevel As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents cbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnSolveBlocked As System.Windows.Forms.Button
    Friend WithEvents btnUsersList As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvUsers As System.Windows.Forms.DataGridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtUsersCount As System.Windows.Forms.TextBox
    Friend WithEvents gbSLS As System.Windows.Forms.GroupBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents txtMainPosSerial As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents dgvPos As System.Windows.Forms.DataGridView
    Friend WithEvents btnSearchPos As System.Windows.Forms.Button
    Friend WithEvents btnSavePersonalInfo As System.Windows.Forms.Button
    Friend WithEvents txtMsg As System.Windows.Forms.TextBox
    Friend WithEvents btnScanFinger As System.Windows.Forms.Button
    Friend WithEvents pbScannedFinger As System.Windows.Forms.PictureBox
    Friend WithEvents btnEnroll As System.Windows.Forms.Button
    Friend WithEvents rbL1 As System.Windows.Forms.RadioButton
    Friend WithEvents rbL2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbL3 As System.Windows.Forms.RadioButton
    Friend WithEvents rbL4 As System.Windows.Forms.RadioButton
    Friend WithEvents rbL5 As System.Windows.Forms.RadioButton
    Friend WithEvents rbR5 As System.Windows.Forms.RadioButton
    Friend WithEvents rbR4 As System.Windows.Forms.RadioButton
    Friend WithEvents rbR3 As System.Windows.Forms.RadioButton
    Friend WithEvents rbR2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbR1 As System.Windows.Forms.RadioButton
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnUpdatePos As System.Windows.Forms.Button
    Friend WithEvents btnAddPos As System.Windows.Forms.Button
    Friend WithEvents txtPosSerial As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtPosIP As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents BtnClear As System.Windows.Forms.Button
    Friend WithEvents txtWinUserCode As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
End Class
