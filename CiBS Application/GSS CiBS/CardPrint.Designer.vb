<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CardPrint
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
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.gbGender = New System.Windows.Forms.GroupBox()
        Me.rbFemale = New System.Windows.Forms.RadioButton()
        Me.rbMale = New System.Windows.Forms.RadioButton()
        Me.lblMobileExp = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.chkNewCustDataGift = New System.Windows.Forms.CheckBox()
        Me.txtCustNoGift = New System.Windows.Forms.TextBox()
        Me.lblCustNoGift = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.RdbWithoutAmount = New System.Windows.Forms.RadioButton()
        Me.RdbAmount = New System.Windows.Forms.RadioButton()
        Me.txtMobile = New System.Windows.Forms.TextBox()
        Me.lblMobile = New System.Windows.Forms.Label()
        Me.txtFishVarizi = New System.Windows.Forms.TextBox()
        Me.lblFisheVarizi = New System.Windows.Forms.Label()
        Me.chkPrintGiftCardAmount = New System.Windows.Forms.CheckBox()
        Me.lblPrintStatus = New System.Windows.Forms.Label()
        Me.rbOthers = New System.Windows.Forms.RadioButton()
        Me.rbIranian = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.txtCardRemain = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtCustFamilyName = New System.Windows.Forms.TextBox()
        Me.lblCustFamilyName = New System.Windows.Forms.Label()
        Me.cbCardType = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.pbPreview = New System.Windows.Forms.PictureBox()
        Me.txtIntCode = New System.Windows.Forms.TextBox()
        Me.txtCustNo = New System.Windows.Forms.TextBox()
        Me.txtAccountNo = New System.Windows.Forms.TextBox()
        Me.txtCustName = New System.Windows.Forms.TextBox()
        Me.lblIntCode = New System.Windows.Forms.Label()
        Me.lblCustNo = New System.Windows.Forms.Label()
        Me.lblAccNo = New System.Windows.Forms.Label()
        Me.lblCustName = New System.Windows.Forms.Label()
        Me.btnlastPrintedCard = New System.Windows.Forms.Button()
        Me.ShapeContainer2 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.RectangleShape5 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.RectangleShape1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.gbLogo = New System.Windows.Forms.GroupBox()
        Me.btnSelectLogo = New System.Windows.Forms.Button()
        Me.btnSelectFont = New System.Windows.Forms.Button()
        Me.pbLogo = New System.Windows.Forms.PictureBox()
        Me.txtSpecialText = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.chkSpecialText = New System.Windows.Forms.CheckBox()
        Me.RectangleShape2 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.RectangleShape4 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.RectangleShape3 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.lblCleaning = New System.Windows.Forms.TextBox()
        Me.lblPrintingStatus = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbPrinters = New System.Windows.Forms.ComboBox()
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.bgwCard = New System.ComponentModel.BackgroundWorker()
        Me.btnOldFields = New System.Windows.Forms.Button()
        Me.GroupBox3.SuspendLayout()
        Me.gbGender.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.pbPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbLogo.SuspendLayout()
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(335, 450)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(117, 23)
        Me.btnRefresh.TabIndex = 70
        Me.btnRefresh.Text = "به روز رسانی چاپگر"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Enabled = False
        Me.btnPrint.Location = New System.Drawing.Point(17, 450)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(117, 23)
        Me.btnPrint.TabIndex = 72
        Me.btnPrint.Text = "صدور کارت"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.BurlyWood
        Me.GroupBox3.Controls.Add(Me.Button3)
        Me.GroupBox3.Controls.Add(Me.gbGender)
        Me.GroupBox3.Controls.Add(Me.lblMobileExp)
        Me.GroupBox3.Controls.Add(Me.Button2)
        Me.GroupBox3.Controls.Add(Me.chkNewCustDataGift)
        Me.GroupBox3.Controls.Add(Me.txtCustNoGift)
        Me.GroupBox3.Controls.Add(Me.lblCustNoGift)
        Me.GroupBox3.Controls.Add(Me.Button1)
        Me.GroupBox3.Controls.Add(Me.GroupBox2)
        Me.GroupBox3.Controls.Add(Me.txtMobile)
        Me.GroupBox3.Controls.Add(Me.lblMobile)
        Me.GroupBox3.Controls.Add(Me.txtFishVarizi)
        Me.GroupBox3.Controls.Add(Me.lblFisheVarizi)
        Me.GroupBox3.Controls.Add(Me.chkPrintGiftCardAmount)
        Me.GroupBox3.Controls.Add(Me.lblPrintStatus)
        Me.GroupBox3.Controls.Add(Me.rbOthers)
        Me.GroupBox3.Controls.Add(Me.rbIranian)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.btnPreview)
        Me.GroupBox3.Controls.Add(Me.txtCardRemain)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.txtCustFamilyName)
        Me.GroupBox3.Controls.Add(Me.lblCustFamilyName)
        Me.GroupBox3.Controls.Add(Me.cbCardType)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.GroupBox1)
        Me.GroupBox3.Controls.Add(Me.txtIntCode)
        Me.GroupBox3.Controls.Add(Me.txtCustNo)
        Me.GroupBox3.Controls.Add(Me.txtAccountNo)
        Me.GroupBox3.Controls.Add(Me.txtCustName)
        Me.GroupBox3.Controls.Add(Me.lblIntCode)
        Me.GroupBox3.Controls.Add(Me.lblCustNo)
        Me.GroupBox3.Controls.Add(Me.lblAccNo)
        Me.GroupBox3.Controls.Add(Me.lblCustName)
        Me.GroupBox3.Controls.Add(Me.btnPrint)
        Me.GroupBox3.Controls.Add(Me.btnlastPrintedCard)
        Me.GroupBox3.Controls.Add(Me.btnRefresh)
        Me.GroupBox3.Controls.Add(Me.ShapeContainer2)
        Me.GroupBox3.Location = New System.Drawing.Point(367, 11)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox3.Size = New System.Drawing.Size(668, 479)
        Me.GroupBox3.TabIndex = 13
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "دریافت اطلاعات مشتری"
        '
        'Button3
        '
        Me.Button3.Enabled = False
        Me.Button3.Location = New System.Drawing.Point(22, 390)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(221, 54)
        Me.Button3.TabIndex = 74
        Me.Button3.Text = "Button3"
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'gbGender
        '
        Me.gbGender.Controls.Add(Me.rbFemale)
        Me.gbGender.Controls.Add(Me.rbMale)
        Me.gbGender.Location = New System.Drawing.Point(376, 300)
        Me.gbGender.Name = "gbGender"
        Me.gbGender.Size = New System.Drawing.Size(248, 38)
        Me.gbGender.TabIndex = 61
        Me.gbGender.TabStop = False
        Me.gbGender.Text = "جنسیت"
        '
        'rbFemale
        '
        Me.rbFemale.AutoSize = True
        Me.rbFemale.Location = New System.Drawing.Point(84, 15)
        Me.rbFemale.Name = "rbFemale"
        Me.rbFemale.Size = New System.Drawing.Size(36, 17)
        Me.rbFemale.TabIndex = 62
        Me.rbFemale.TabStop = True
        Me.rbFemale.Text = "زن"
        Me.rbFemale.UseVisualStyleBackColor = True
        '
        'rbMale
        '
        Me.rbMale.AutoSize = True
        Me.rbMale.Location = New System.Drawing.Point(32, 15)
        Me.rbMale.Name = "rbMale"
        Me.rbMale.Size = New System.Drawing.Size(41, 17)
        Me.rbMale.TabIndex = 63
        Me.rbMale.TabStop = True
        Me.rbMale.Text = "مرد"
        Me.rbMale.UseVisualStyleBackColor = True
        '
        'lblMobileExp
        '
        Me.lblMobileExp.AutoSize = True
        Me.lblMobileExp.Location = New System.Drawing.Point(384, 236)
        Me.lblMobileExp.Name = "lblMobileExp"
        Me.lblMobileExp.Size = New System.Drawing.Size(130, 13)
        Me.lblMobileExp.TabIndex = 73
        Me.lblMobileExp.Text = "مثال:         *******98912"
        Me.lblMobileExp.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Button2
        '
        Me.Button2.Enabled = False
        Me.Button2.Location = New System.Drawing.Point(56, 354)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 69
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'chkNewCustDataGift
        '
        Me.chkNewCustDataGift.AutoSize = True
        Me.chkNewCustDataGift.Enabled = False
        Me.chkNewCustDataGift.Location = New System.Drawing.Point(371, 367)
        Me.chkNewCustDataGift.Name = "chkNewCustDataGift"
        Me.chkNewCustDataGift.Size = New System.Drawing.Size(148, 17)
        Me.chkNewCustDataGift.TabIndex = 65
        Me.chkNewCustDataGift.Text = "ثبت اطلاعات مشتری جدید"
        Me.chkNewCustDataGift.UseVisualStyleBackColor = True
        Me.chkNewCustDataGift.Visible = False
        '
        'txtCustNoGift
        '
        Me.txtCustNoGift.Enabled = False
        Me.txtCustNoGift.Location = New System.Drawing.Point(376, 188)
        Me.txtCustNoGift.MaxLength = 10
        Me.txtCustNoGift.Name = "txtCustNoGift"
        Me.txtCustNoGift.Size = New System.Drawing.Size(143, 21)
        Me.txtCustNoGift.TabIndex = 56
        Me.txtCustNoGift.Tag = "ENG"
        Me.txtCustNoGift.Visible = False
        '
        'lblCustNoGift
        '
        Me.lblCustNoGift.AutoSize = True
        Me.lblCustNoGift.Enabled = False
        Me.lblCustNoGift.Location = New System.Drawing.Point(531, 190)
        Me.lblCustNoGift.Name = "lblCustNoGift"
        Me.lblCustNoGift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCustNoGift.Size = New System.Drawing.Size(77, 13)
        Me.lblCustNoGift.TabIndex = 60
        Me.lblCustNoGift.Text = "شماره مشتری"
        Me.lblCustNoGift.Visible = False
        '
        'Button1
        '
        Me.Button1.Enabled = False
        Me.Button1.Location = New System.Drawing.Point(45, 319)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 59
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RdbWithoutAmount)
        Me.GroupBox2.Controls.Add(Me.RdbAmount)
        Me.GroupBox2.Location = New System.Drawing.Point(377, 383)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(245, 61)
        Me.GroupBox2.TabIndex = 66
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "مبلغ"
        '
        'RdbWithoutAmount
        '
        Me.RdbWithoutAmount.AutoSize = True
        Me.RdbWithoutAmount.Location = New System.Drawing.Point(43, 37)
        Me.RdbWithoutAmount.Name = "RdbWithoutAmount"
        Me.RdbWithoutAmount.Size = New System.Drawing.Size(98, 17)
        Me.RdbWithoutAmount.TabIndex = 68
        Me.RdbWithoutAmount.TabStop = True
        Me.RdbWithoutAmount.Text = "بدون دستگاه پوز"
        Me.RdbWithoutAmount.UseVisualStyleBackColor = True
        '
        'RdbAmount
        '
        Me.RdbAmount.AutoSize = True
        Me.RdbAmount.Location = New System.Drawing.Point(58, 17)
        Me.RdbAmount.Name = "RdbAmount"
        Me.RdbAmount.Size = New System.Drawing.Size(83, 17)
        Me.RdbAmount.TabIndex = 67
        Me.RdbAmount.TabStop = True
        Me.RdbAmount.Text = "با دستگاه پوز"
        Me.RdbAmount.UseVisualStyleBackColor = True
        '
        'txtMobile
        '
        Me.txtMobile.Location = New System.Drawing.Point(377, 213)
        Me.txtMobile.MaxLength = 12
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.Size = New System.Drawing.Size(143, 21)
        Me.txtMobile.TabIndex = 57
        Me.txtMobile.Tag = "ENG"
        '
        'lblMobile
        '
        Me.lblMobile.AutoSize = True
        Me.lblMobile.Location = New System.Drawing.Point(530, 215)
        Me.lblMobile.Name = "lblMobile"
        Me.lblMobile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMobile.Size = New System.Drawing.Size(68, 13)
        Me.lblMobile.TabIndex = 56
        Me.lblMobile.Text = "شماره موبایل"
        '
        'txtFishVarizi
        '
        Me.txtFishVarizi.Location = New System.Drawing.Point(377, 187)
        Me.txtFishVarizi.MaxLength = 6
        Me.txtFishVarizi.Name = "txtFishVarizi"
        Me.txtFishVarizi.Size = New System.Drawing.Size(143, 21)
        Me.txtFishVarizi.TabIndex = 55
        Me.txtFishVarizi.Tag = "ENG"
        '
        'lblFisheVarizi
        '
        Me.lblFisheVarizi.AutoSize = True
        Me.lblFisheVarizi.Location = New System.Drawing.Point(528, 188)
        Me.lblFisheVarizi.Name = "lblFisheVarizi"
        Me.lblFisheVarizi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFisheVarizi.Size = New System.Drawing.Size(96, 13)
        Me.lblFisheVarizi.TabIndex = 54
        Me.lblFisheVarizi.Text = "شماره فیش واریزی"
        '
        'chkPrintGiftCardAmount
        '
        Me.chkPrintGiftCardAmount.AutoSize = True
        Me.chkPrintGiftCardAmount.Checked = True
        Me.chkPrintGiftCardAmount.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPrintGiftCardAmount.Location = New System.Drawing.Point(383, 344)
        Me.chkPrintGiftCardAmount.Name = "chkPrintGiftCardAmount"
        Me.chkPrintGiftCardAmount.Size = New System.Drawing.Size(136, 17)
        Me.chkPrintGiftCardAmount.TabIndex = 64
        Me.chkPrintGiftCardAmount.Text = "چاپ رقم کارت هدیه       "
        Me.chkPrintGiftCardAmount.UseVisualStyleBackColor = True
        '
        'lblPrintStatus
        '
        Me.lblPrintStatus.AutoSize = True
        Me.lblPrintStatus.BackColor = System.Drawing.Color.RosyBrown
        Me.lblPrintStatus.Location = New System.Drawing.Point(133, 262)
        Me.lblPrintStatus.Name = "lblPrintStatus"
        Me.lblPrintStatus.Size = New System.Drawing.Size(0, 13)
        Me.lblPrintStatus.TabIndex = 52
        '
        'rbOthers
        '
        Me.rbOthers.AutoSize = True
        Me.rbOthers.Location = New System.Drawing.Point(377, 254)
        Me.rbOthers.Name = "rbOthers"
        Me.rbOthers.Size = New System.Drawing.Size(72, 17)
        Me.rbOthers.TabIndex = 59
        Me.rbOthers.Text = "غیر ایرانی"
        Me.rbOthers.UseVisualStyleBackColor = True
        '
        'rbIranian
        '
        Me.rbIranian.AutoSize = True
        Me.rbIranian.Checked = True
        Me.rbIranian.Location = New System.Drawing.Point(467, 254)
        Me.rbIranian.Name = "rbIranian"
        Me.rbIranian.Size = New System.Drawing.Size(53, 17)
        Me.rbIranian.TabIndex = 58
        Me.rbIranian.TabStop = True
        Me.rbIranian.Text = "ایرانی"
        Me.rbIranian.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(531, 259)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "وضعیت اقامت"
        '
        'btnPreview
        '
        Me.btnPreview.Location = New System.Drawing.Point(494, 450)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(117, 23)
        Me.btnPreview.TabIndex = 69
        Me.btnPreview.Text = "پیش نمایش کارت"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'txtCardRemain
        '
        Me.txtCardRemain.Enabled = False
        Me.txtCardRemain.ForeColor = System.Drawing.Color.DarkRed
        Me.txtCardRemain.Location = New System.Drawing.Point(377, 47)
        Me.txtCardRemain.MaxLength = 50
        Me.txtCardRemain.Name = "txtCardRemain"
        Me.txtCardRemain.ReadOnly = True
        Me.txtCardRemain.Size = New System.Drawing.Size(142, 21)
        Me.txtCardRemain.TabIndex = 46
        Me.txtCardRemain.TabStop = False
        Me.txtCardRemain.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Enabled = False
        Me.Label11.Location = New System.Drawing.Point(528, 48)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(121, 13)
        Me.Label11.TabIndex = 45
        Me.Label11.Text = "باقی مانده کارت در شعبه"
        Me.Label11.Visible = False
        '
        'txtCustFamilyName
        '
        Me.txtCustFamilyName.Location = New System.Drawing.Point(376, 102)
        Me.txtCustFamilyName.MaxLength = 50
        Me.txtCustFamilyName.Name = "txtCustFamilyName"
        Me.txtCustFamilyName.Size = New System.Drawing.Size(143, 21)
        Me.txtCustFamilyName.TabIndex = 37
        Me.txtCustFamilyName.Tag = "PERS"
        '
        'lblCustFamilyName
        '
        Me.lblCustFamilyName.AutoSize = True
        Me.lblCustFamilyName.Location = New System.Drawing.Point(528, 103)
        Me.lblCustFamilyName.Name = "lblCustFamilyName"
        Me.lblCustFamilyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCustFamilyName.Size = New System.Drawing.Size(105, 13)
        Me.lblCustFamilyName.TabIndex = 37
        Me.lblCustFamilyName.Text = "نام خانوادگی مشتری"
        '
        'cbCardType
        '
        Me.cbCardType.FormattingEnabled = True
        Me.cbCardType.Location = New System.Drawing.Point(376, 20)
        Me.cbCardType.Name = "cbCardType"
        Me.cbCardType.Size = New System.Drawing.Size(143, 21)
        Me.cbCardType.TabIndex = 35
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(528, 21)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(46, 13)
        Me.Label8.TabIndex = 34
        Me.Label8.Text = "نوع کارت"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.GroupBox1.Controls.Add(Me.pbPreview)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 25)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(329, 216)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " پیش نمایش کارت "
        '
        'pbPreview
        '
        Me.pbPreview.Location = New System.Drawing.Point(5, 19)
        Me.pbPreview.Name = "pbPreview"
        Me.pbPreview.Size = New System.Drawing.Size(318, 191)
        Me.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbPreview.TabIndex = 0
        Me.pbPreview.TabStop = False
        '
        'txtIntCode
        '
        Me.txtIntCode.Location = New System.Drawing.Point(376, 277)
        Me.txtIntCode.MaxLength = 10
        Me.txtIntCode.Name = "txtIntCode"
        Me.txtIntCode.Size = New System.Drawing.Size(143, 21)
        Me.txtIntCode.TabIndex = 60
        Me.txtIntCode.Tag = "ENG"
        '
        'txtCustNo
        '
        Me.txtCustNo.Location = New System.Drawing.Point(377, 158)
        Me.txtCustNo.MaxLength = 10
        Me.txtCustNo.Name = "txtCustNo"
        Me.txtCustNo.Size = New System.Drawing.Size(143, 21)
        Me.txtCustNo.TabIndex = 39
        Me.txtCustNo.Tag = "ENG"
        '
        'txtAccountNo
        '
        Me.txtAccountNo.Location = New System.Drawing.Point(376, 131)
        Me.txtAccountNo.MaxLength = 13
        Me.txtAccountNo.Name = "txtAccountNo"
        Me.txtAccountNo.Size = New System.Drawing.Size(143, 21)
        Me.txtAccountNo.TabIndex = 38
        Me.txtAccountNo.Tag = "ENG"
        '
        'txtCustName
        '
        Me.txtCustName.Location = New System.Drawing.Point(376, 75)
        Me.txtCustName.MaxLength = 50
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.Size = New System.Drawing.Size(143, 21)
        Me.txtCustName.TabIndex = 36
        Me.txtCustName.Tag = "PERS"
        '
        'lblIntCode
        '
        Me.lblIntCode.AutoSize = True
        Me.lblIntCode.Location = New System.Drawing.Point(528, 281)
        Me.lblIntCode.Name = "lblIntCode"
        Me.lblIntCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblIntCode.Size = New System.Drawing.Size(93, 13)
        Me.lblIntCode.TabIndex = 29
        Me.lblIntCode.Text = "کد ملی / کد اقامت"
        '
        'lblCustNo
        '
        Me.lblCustNo.AutoSize = True
        Me.lblCustNo.Location = New System.Drawing.Point(528, 159)
        Me.lblCustNo.Name = "lblCustNo"
        Me.lblCustNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCustNo.Size = New System.Drawing.Size(77, 13)
        Me.lblCustNo.TabIndex = 28
        Me.lblCustNo.Text = "شماره مشتری"
        '
        'lblAccNo
        '
        Me.lblAccNo.AutoSize = True
        Me.lblAccNo.Location = New System.Drawing.Point(528, 131)
        Me.lblAccNo.Name = "lblAccNo"
        Me.lblAccNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAccNo.Size = New System.Drawing.Size(71, 13)
        Me.lblAccNo.TabIndex = 27
        Me.lblAccNo.Text = "شماره حساب"
        '
        'lblCustName
        '
        Me.lblCustName.AutoSize = True
        Me.lblCustName.Location = New System.Drawing.Point(528, 75)
        Me.lblCustName.Name = "lblCustName"
        Me.lblCustName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCustName.Size = New System.Drawing.Size(60, 13)
        Me.lblCustName.TabIndex = 26
        Me.lblCustName.Text = "نام مشتری"
        '
        'btnlastPrintedCard
        '
        Me.btnlastPrintedCard.Location = New System.Drawing.Point(176, 450)
        Me.btnlastPrintedCard.Name = "btnlastPrintedCard"
        Me.btnlastPrintedCard.Size = New System.Drawing.Size(117, 23)
        Me.btnlastPrintedCard.TabIndex = 71
        Me.btnlastPrintedCard.Text = "آخرین کارت صادره"
        Me.btnlastPrintedCard.UseVisualStyleBackColor = True
        '
        'ShapeContainer2
        '
        Me.ShapeContainer2.Location = New System.Drawing.Point(3, 17)
        Me.ShapeContainer2.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer2.Name = "ShapeContainer2"
        Me.ShapeContainer2.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape5, Me.RectangleShape1})
        Me.ShapeContainer2.Size = New System.Drawing.Size(662, 459)
        Me.ShapeContainer2.TabIndex = 51
        Me.ShapeContainer2.TabStop = False
        '
        'RectangleShape5
        '
        Me.RectangleShape5.BackColor = System.Drawing.Color.RosyBrown
        Me.RectangleShape5.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.RectangleShape5.BorderColor = System.Drawing.Color.Transparent
        Me.RectangleShape5.CornerRadius = 10
        Me.RectangleShape5.FillColor = System.Drawing.Color.LightSlateGray
        Me.RectangleShape5.Location = New System.Drawing.Point(7, 238)
        Me.RectangleShape5.Name = "RectangleShape5"
        Me.RectangleShape5.Size = New System.Drawing.Size(345, 25)
        '
        'RectangleShape1
        '
        Me.RectangleShape1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.RectangleShape1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.RectangleShape1.BorderColor = System.Drawing.Color.Transparent
        Me.RectangleShape1.CornerRadius = 10
        Me.RectangleShape1.FillColor = System.Drawing.SystemColors.ActiveCaption
        Me.RectangleShape1.Location = New System.Drawing.Point(6, 4)
        Me.RectangleShape1.Name = "RectangleShape1"
        Me.RectangleShape1.Size = New System.Drawing.Size(344, 225)
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'gbLogo
        '
        Me.gbLogo.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.gbLogo.Controls.Add(Me.btnSelectLogo)
        Me.gbLogo.Controls.Add(Me.btnSelectFont)
        Me.gbLogo.Controls.Add(Me.pbLogo)
        Me.gbLogo.Controls.Add(Me.txtSpecialText)
        Me.gbLogo.Controls.Add(Me.Label10)
        Me.gbLogo.Location = New System.Drawing.Point(29, 201)
        Me.gbLogo.Name = "gbLogo"
        Me.gbLogo.Size = New System.Drawing.Size(307, 198)
        Me.gbLogo.TabIndex = 23
        Me.gbLogo.TabStop = False
        Me.gbLogo.Text = "      متن خاص / لوگو"
        '
        'btnSelectLogo
        '
        Me.btnSelectLogo.Location = New System.Drawing.Point(213, 144)
        Me.btnSelectLogo.Name = "btnSelectLogo"
        Me.btnSelectLogo.Size = New System.Drawing.Size(75, 23)
        Me.btnSelectLogo.TabIndex = 58
        Me.btnSelectLogo.Text = "انتخاب عکس"
        Me.btnSelectLogo.UseVisualStyleBackColor = True
        '
        'btnSelectFont
        '
        Me.btnSelectFont.Location = New System.Drawing.Point(213, 60)
        Me.btnSelectFont.Name = "btnSelectFont"
        Me.btnSelectFont.Size = New System.Drawing.Size(75, 23)
        Me.btnSelectFont.TabIndex = 57
        Me.btnSelectFont.Text = "انتخاب قلم"
        Me.btnSelectFont.UseVisualStyleBackColor = True
        '
        'pbLogo
        '
        Me.pbLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbLogo.Location = New System.Drawing.Point(15, 96)
        Me.pbLogo.Name = "pbLogo"
        Me.pbLogo.Size = New System.Drawing.Size(77, 75)
        Me.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbLogo.TabIndex = 55
        Me.pbLogo.TabStop = False
        '
        'txtSpecialText
        '
        Me.txtSpecialText.Location = New System.Drawing.Point(15, 25)
        Me.txtSpecialText.Multiline = True
        Me.txtSpecialText.Name = "txtSpecialText"
        Me.txtSpecialText.Size = New System.Drawing.Size(192, 58)
        Me.txtSpecialText.TabIndex = 51
        Me.txtSpecialText.Tag = "PERS"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(213, 25)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(51, 13)
        Me.Label10.TabIndex = 50
        Me.Label10.Text = "متن خاص"
        '
        'chkSpecialText
        '
        Me.chkSpecialText.AutoSize = True
        Me.chkSpecialText.Enabled = False
        Me.chkSpecialText.Location = New System.Drawing.Point(40, 193)
        Me.chkSpecialText.Name = "chkSpecialText"
        Me.chkSpecialText.Size = New System.Drawing.Size(15, 14)
        Me.chkSpecialText.TabIndex = 50
        Me.chkSpecialText.UseVisualStyleBackColor = True
        Me.chkSpecialText.Visible = False
        '
        'RectangleShape2
        '
        Me.RectangleShape2.BackColor = System.Drawing.Color.BurlyWood
        Me.RectangleShape2.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.RectangleShape2.BorderColor = System.Drawing.Color.Transparent
        Me.RectangleShape2.CornerRadius = 10
        Me.RectangleShape2.FillColor = System.Drawing.SystemColors.ActiveCaption
        Me.RectangleShape2.Location = New System.Drawing.Point(10, 5)
        Me.RectangleShape2.Name = "RectangleShape2"
        Me.RectangleShape2.Size = New System.Drawing.Size(683, 489)
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape4, Me.RectangleShape3, Me.RectangleShape2})
        Me.ShapeContainer1.Size = New System.Drawing.Size(1053, 502)
        Me.ShapeContainer1.TabIndex = 51
        Me.ShapeContainer1.TabStop = False
        '
        'RectangleShape4
        '
        Me.RectangleShape4.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.RectangleShape4.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.RectangleShape4.BorderColor = System.Drawing.Color.Transparent
        Me.RectangleShape4.CornerRadius = 10
        Me.RectangleShape4.FillColor = System.Drawing.SystemColors.ActiveCaption
        Me.RectangleShape4.Location = New System.Drawing.Point(704, 161)
        Me.RectangleShape4.Name = "RectangleShape4"
        Me.RectangleShape4.Size = New System.Drawing.Size(335, 327)
        '
        'RectangleShape3
        '
        Me.RectangleShape3.BackColor = System.Drawing.Color.Khaki
        Me.RectangleShape3.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.RectangleShape3.BorderColor = System.Drawing.Color.Transparent
        Me.RectangleShape3.CornerRadius = 10
        Me.RectangleShape3.FillColor = System.Drawing.SystemColors.ActiveCaption
        Me.RectangleShape3.Location = New System.Drawing.Point(704, 6)
        Me.RectangleShape3.Name = "RectangleShape3"
        Me.RectangleShape3.Size = New System.Drawing.Size(336, 148)
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Khaki
        Me.GroupBox4.Controls.Add(Me.lblCleaning)
        Me.GroupBox4.Controls.Add(Me.lblPrintingStatus)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.cbPrinters)
        Me.GroupBox4.Location = New System.Drawing.Point(29, 12)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(307, 124)
        Me.GroupBox4.TabIndex = 58
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "تنظیمات چاپگر"
        '
        'lblCleaning
        '
        Me.lblCleaning.ForeColor = System.Drawing.Color.DarkRed
        Me.lblCleaning.Location = New System.Drawing.Point(18, 79)
        Me.lblCleaning.MaxLength = 50
        Me.lblCleaning.Name = "lblCleaning"
        Me.lblCleaning.ReadOnly = True
        Me.lblCleaning.Size = New System.Drawing.Size(188, 21)
        Me.lblCleaning.TabIndex = 63
        Me.lblCleaning.TabStop = False
        Me.lblCleaning.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblPrintingStatus
        '
        Me.lblPrintingStatus.ForeColor = System.Drawing.Color.DarkRed
        Me.lblPrintingStatus.Location = New System.Drawing.Point(18, 52)
        Me.lblPrintingStatus.MaxLength = 50
        Me.lblPrintingStatus.Name = "lblPrintingStatus"
        Me.lblPrintingStatus.ReadOnly = True
        Me.lblPrintingStatus.Size = New System.Drawing.Size(188, 21)
        Me.lblPrintingStatus.TabIndex = 62
        Me.lblPrintingStatus.TabStop = False
        Me.lblPrintingStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(212, 81)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 13)
        Me.Label4.TabIndex = 61
        Me.Label4.Text = "نیاز به تمیزکاری"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(212, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 60
        Me.Label3.Text = "وضعیت چاپگر"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(212, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 59
        Me.Label1.Text = "انتخاب چاپگر"
        '
        'cbPrinters
        '
        Me.cbPrinters.FormattingEnabled = True
        Me.cbPrinters.Location = New System.Drawing.Point(18, 24)
        Me.cbPrinters.Name = "cbPrinters"
        Me.cbPrinters.Size = New System.Drawing.Size(188, 21)
        Me.cbPrinters.TabIndex = 58
        '
        'FontDialog1
        '
        Me.FontDialog1.Color = System.Drawing.SystemColors.ControlText
        '
        'btnOldFields
        '
        Me.btnOldFields.Location = New System.Drawing.Point(62, 415)
        Me.btnOldFields.Name = "btnOldFields"
        Me.btnOldFields.Size = New System.Drawing.Size(232, 38)
        Me.btnOldFields.TabIndex = 59
        Me.btnOldFields.Text = "اطلاعات فیلدهای کارت هدیه قبلی"
        Me.btnOldFields.UseVisualStyleBackColor = True
        '
        'CardPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(1053, 502)
        Me.Controls.Add(Me.btnOldFields)
        Me.Controls.Add(Me.chkSpecialText)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.gbLogo)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CardPrint"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "                                                                        "
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.gbGender.ResumeLayout(False)
        Me.gbGender.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.pbPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbLogo.ResumeLayout(False)
        Me.gbLogo.PerformLayout()
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents pbPreview As System.Windows.Forms.PictureBox
    Friend WithEvents btnlastPrintedCard As System.Windows.Forms.Button
    Friend WithEvents txtIntCode As System.Windows.Forms.TextBox
    Friend WithEvents txtCustNo As System.Windows.Forms.TextBox
    Friend WithEvents txtAccountNo As System.Windows.Forms.TextBox
    Friend WithEvents txtCustName As System.Windows.Forms.TextBox
    Friend WithEvents lblIntCode As System.Windows.Forms.Label
    Friend WithEvents lblCustNo As System.Windows.Forms.Label
    Friend WithEvents lblAccNo As System.Windows.Forms.Label
    Friend WithEvents lblCustName As System.Windows.Forms.Label
    Friend WithEvents cbCardType As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCustFamilyName As System.Windows.Forms.TextBox
    Friend WithEvents lblCustFamilyName As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents gbLogo As System.Windows.Forms.GroupBox
    Friend WithEvents txtSpecialText As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents chkSpecialText As System.Windows.Forms.CheckBox
    Friend WithEvents pbLogo As System.Windows.Forms.PictureBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCardRemain As System.Windows.Forms.TextBox
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents rbOthers As System.Windows.Forms.RadioButton
    Friend WithEvents rbIranian As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ShapeContainer2 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents RectangleShape1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents RectangleShape2 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents RectangleShape3 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents lblCleaning As System.Windows.Forms.TextBox
    Friend WithEvents lblPrintingStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents RectangleShape4 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents RectangleShape5 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents lblPrintStatus As System.Windows.Forms.Label
    Friend WithEvents btnSelectFont As System.Windows.Forms.Button
    Friend WithEvents btnSelectLogo As System.Windows.Forms.Button
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents chkPrintGiftCardAmount As System.Windows.Forms.CheckBox
    Friend WithEvents txtMobile As System.Windows.Forms.TextBox
    Friend WithEvents lblMobile As System.Windows.Forms.Label
    Friend WithEvents txtFishVarizi As System.Windows.Forms.TextBox
    Friend WithEvents lblFisheVarizi As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents RdbWithoutAmount As System.Windows.Forms.RadioButton
    Friend WithEvents RdbAmount As System.Windows.Forms.RadioButton
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents chkNewCustDataGift As System.Windows.Forms.CheckBox
    Friend WithEvents txtCustNoGift As System.Windows.Forms.TextBox
    Friend WithEvents lblCustNoGift As System.Windows.Forms.Label
    Friend WithEvents bgwCard As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnOldFields As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents rbMale As System.Windows.Forms.RadioButton
    Friend WithEvents rbFemale As System.Windows.Forms.RadioButton
    Friend WithEvents lblMobileExp As System.Windows.Forms.Label
    Friend WithEvents gbGender As System.Windows.Forms.GroupBox
    Friend WithEvents Button3 As System.Windows.Forms.Button

End Class
