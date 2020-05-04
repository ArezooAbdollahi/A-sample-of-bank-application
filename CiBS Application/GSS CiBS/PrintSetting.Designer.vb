<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrintSetting
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
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtProductCode = New System.Windows.Forms.TextBox()
        Me.ProductCode = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.cbEnabled = New System.Windows.Forms.ComboBox()
        Me.btnCopyTemplate = New System.Windows.Forms.Button()
        Me.btnSavePrintTemplate = New System.Windows.Forms.Button()
        Me.btnShowCardPrintTemplate = New System.Windows.Forms.Button()
        Me.cbCardType = New System.Windows.Forms.ComboBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnChange = New System.Windows.Forms.Button()
        Me.cbShoarBold = New System.Windows.Forms.CheckBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.lvCardText = New System.Windows.Forms.ListView()
        Me.chText = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chFont = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chBold = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chX = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chY = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.txtFontSize = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.cbFonts = New System.Windows.Forms.ComboBox()
        Me.txtCardlogo = New System.Windows.Forms.TextBox()
        Me.txtX = New System.Windows.Forms.TextBox()
        Me.txtY = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.pbResult = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkCenterSheba = New System.Windows.Forms.CheckBox()
        Me.cbShebaBold = New System.Windows.Forms.CheckBox()
        Me.chkRTLSheba = New System.Windows.Forms.CheckBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.txtFontSizeSheba = New System.Windows.Forms.TextBox()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.cbFontSheba = New System.Windows.Forms.ComboBox()
        Me.txtXSheba = New System.Windows.Forms.TextBox()
        Me.txtYSheba = New System.Windows.Forms.TextBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.cbSheba = New System.Windows.Forms.CheckBox()
        Me.chkCenterAccNo = New System.Windows.Forms.CheckBox()
        Me.cbAccNoBold = New System.Windows.Forms.CheckBox()
        Me.chkRTLAccNo = New System.Windows.Forms.CheckBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtFontSizeAccNo = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.cbFontAccNo = New System.Windows.Forms.ComboBox()
        Me.txtXAccNo = New System.Windows.Forms.TextBox()
        Me.txtYAccNo = New System.Windows.Forms.TextBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.cbAccNo = New System.Windows.Forms.CheckBox()
        Me.txtXlogo = New System.Windows.Forms.TextBox()
        Me.txtYlogo = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.cbLogo = New System.Windows.Forms.CheckBox()
        Me.chkCenterSpecialText = New System.Windows.Forms.CheckBox()
        Me.cbSpecialTextBold = New System.Windows.Forms.CheckBox()
        Me.chkRTLspecialText = New System.Windows.Forms.CheckBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtFontSizeSpecialtext = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.cbFontSpecialText = New System.Windows.Forms.ComboBox()
        Me.txtXSpecialText = New System.Windows.Forms.TextBox()
        Me.txtYSpecialText = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.chkCenterName = New System.Windows.Forms.CheckBox()
        Me.chkCenterCVV = New System.Windows.Forms.CheckBox()
        Me.chkCenterEXP = New System.Windows.Forms.CheckBox()
        Me.chkCenterBrCode = New System.Windows.Forms.CheckBox()
        Me.chkCenterPAN = New System.Windows.Forms.CheckBox()
        Me.cbSpecialText = New System.Windows.Forms.CheckBox()
        Me.cbBrCodeBold = New System.Windows.Forms.CheckBox()
        Me.cbExpDateBold = New System.Windows.Forms.CheckBox()
        Me.cbCvvBold = New System.Windows.Forms.CheckBox()
        Me.cbNameBold = New System.Windows.Forms.CheckBox()
        Me.cbPanBold = New System.Windows.Forms.CheckBox()
        Me.cbBRANCH = New System.Windows.Forms.CheckBox()
        Me.cbEXP = New System.Windows.Forms.CheckBox()
        Me.cbCVV2 = New System.Windows.Forms.CheckBox()
        Me.cbNAME = New System.Windows.Forms.CheckBox()
        Me.cbPAN = New System.Windows.Forms.CheckBox()
        Me.chkRTLname = New System.Windows.Forms.CheckBox()
        Me.chkRTLcvv2 = New System.Windows.Forms.CheckBox()
        Me.chkRTLexp = New System.Windows.Forms.CheckBox()
        Me.chkRTLbranch = New System.Windows.Forms.CheckBox()
        Me.chkRTLpan = New System.Windows.Forms.CheckBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtFontSizeBrCode = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cbFontBrCode = New System.Windows.Forms.ComboBox()
        Me.txtXbrcode = New System.Windows.Forms.TextBox()
        Me.txtYbrcode = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtFontSizeExpDate = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cbFontExpDate = New System.Windows.Forms.ComboBox()
        Me.txtXexpdate = New System.Windows.Forms.TextBox()
        Me.txtYexpdate = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtFontSizeCVV2 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cbFontCVV2 = New System.Windows.Forms.ComboBox()
        Me.txtXcvv2 = New System.Windows.Forms.TextBox()
        Me.txtYcvv2 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtFontSizeName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbFontName = New System.Windows.Forms.ComboBox()
        Me.txtXname = New System.Windows.Forms.TextBox()
        Me.txtYname = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtFontSizePAN = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbFontPAN = New System.Windows.Forms.ComboBox()
        Me.txtXpan = New System.Windows.Forms.TextBox()
        Me.txtYpan = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.pbResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btnPreview
        '
        Me.btnPreview.Location = New System.Drawing.Point(175, 548)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(75, 23)
        Me.btnPreview.TabIndex = 87
        Me.btnPreview.Text = "پیش نمایش"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(13, 548)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 89
        Me.btnExit.Text = "خروج"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtProductCode)
        Me.GroupBox2.Controls.Add(Me.ProductCode)
        Me.GroupBox2.Controls.Add(Me.Label40)
        Me.GroupBox2.Controls.Add(Me.cbEnabled)
        Me.GroupBox2.Controls.Add(Me.btnCopyTemplate)
        Me.GroupBox2.Controls.Add(Me.btnSavePrintTemplate)
        Me.GroupBox2.Controls.Add(Me.btnShowCardPrintTemplate)
        Me.GroupBox2.Controls.Add(Me.cbCardType)
        Me.GroupBox2.Controls.Add(Me.Label31)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 1)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox2.Size = New System.Drawing.Size(886, 60)
        Me.GroupBox2.TabIndex = 95
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "انتخاب تنظیمات چاپ کارت"
        '
        'txtProductCode
        '
        Me.txtProductCode.Location = New System.Drawing.Point(370, 22)
        Me.txtProductCode.Name = "txtProductCode"
        Me.txtProductCode.Size = New System.Drawing.Size(84, 21)
        Me.txtProductCode.TabIndex = 153
        '
        'ProductCode
        '
        Me.ProductCode.AutoSize = True
        Me.ProductCode.Location = New System.Drawing.Point(460, 25)
        Me.ProductCode.Name = "ProductCode"
        Me.ProductCode.Size = New System.Drawing.Size(55, 13)
        Me.ProductCode.TabIndex = 152
        Me.ProductCode.Text = "کد محصول"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(600, 25)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(41, 13)
        Me.Label40.TabIndex = 151
        Me.Label40.Text = "وضعیت"
        '
        'cbEnabled
        '
        Me.cbEnabled.FormattingEnabled = True
        Me.cbEnabled.Items.AddRange(New Object() {"غیرفعال", "فعال"})
        Me.cbEnabled.Location = New System.Drawing.Point(516, 22)
        Me.cbEnabled.Name = "cbEnabled"
        Me.cbEnabled.Size = New System.Drawing.Size(78, 21)
        Me.cbEnabled.TabIndex = 94
        '
        'btnCopyTemplate
        '
        Me.btnCopyTemplate.Enabled = False
        Me.btnCopyTemplate.Location = New System.Drawing.Point(6, 20)
        Me.btnCopyTemplate.Name = "btnCopyTemplate"
        Me.btnCopyTemplate.Size = New System.Drawing.Size(118, 23)
        Me.btnCopyTemplate.TabIndex = 93
        Me.btnCopyTemplate.Text = "ذخیره  الگو با نام جدید"
        Me.btnCopyTemplate.UseVisualStyleBackColor = True
        '
        'btnSavePrintTemplate
        '
        Me.btnSavePrintTemplate.Enabled = False
        Me.btnSavePrintTemplate.Location = New System.Drawing.Point(131, 20)
        Me.btnSavePrintTemplate.Name = "btnSavePrintTemplate"
        Me.btnSavePrintTemplate.Size = New System.Drawing.Size(110, 23)
        Me.btnSavePrintTemplate.TabIndex = 92
        Me.btnSavePrintTemplate.Text = "ذخیره الگوی چاپ"
        Me.btnSavePrintTemplate.UseVisualStyleBackColor = True
        '
        'btnShowCardPrintTemplate
        '
        Me.btnShowCardPrintTemplate.Location = New System.Drawing.Point(248, 20)
        Me.btnShowCardPrintTemplate.Name = "btnShowCardPrintTemplate"
        Me.btnShowCardPrintTemplate.Size = New System.Drawing.Size(110, 23)
        Me.btnShowCardPrintTemplate.TabIndex = 91
        Me.btnShowCardPrintTemplate.Text = "نمایش الگوی چاپ"
        Me.btnShowCardPrintTemplate.UseVisualStyleBackColor = True
        '
        'cbCardType
        '
        Me.cbCardType.FormattingEnabled = True
        Me.cbCardType.Location = New System.Drawing.Point(647, 22)
        Me.cbCardType.Name = "cbCardType"
        Me.cbCardType.Size = New System.Drawing.Size(179, 21)
        Me.cbCardType.TabIndex = 90
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(832, 25)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(46, 13)
        Me.Label31.TabIndex = 89
        Me.Label31.Text = "نوع کارت"
        '
        'btnPrint
        '
        Me.btnPrint.Enabled = False
        Me.btnPrint.Location = New System.Drawing.Point(94, 548)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 23)
        Me.btnPrint.TabIndex = 97
        Me.btnPrint.Text = "چاپ"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnChange)
        Me.GroupBox3.Controls.Add(Me.cbShoarBold)
        Me.GroupBox3.Controls.Add(Me.btnDelete)
        Me.GroupBox3.Controls.Add(Me.lvCardText)
        Me.GroupBox3.Controls.Add(Me.btnAdd)
        Me.GroupBox3.Controls.Add(Me.txtFontSize)
        Me.GroupBox3.Controls.Add(Me.Label27)
        Me.GroupBox3.Controls.Add(Me.Label26)
        Me.GroupBox3.Controls.Add(Me.cbFonts)
        Me.GroupBox3.Controls.Add(Me.txtCardlogo)
        Me.GroupBox3.Controls.Add(Me.txtX)
        Me.GroupBox3.Controls.Add(Me.txtY)
        Me.GroupBox3.Controls.Add(Me.Label28)
        Me.GroupBox3.Controls.Add(Me.Label29)
        Me.GroupBox3.Controls.Add(Me.Label30)
        Me.GroupBox3.Location = New System.Drawing.Point(317, 322)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox3.Size = New System.Drawing.Size(543, 223)
        Me.GroupBox3.TabIndex = 103
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = " شعار / متن "
        '
        'btnChange
        '
        Me.btnChange.Location = New System.Drawing.Point(9, 180)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(44, 23)
        Me.btnChange.TabIndex = 55
        Me.btnChange.Text = "تغییر"
        Me.btnChange.UseVisualStyleBackColor = True
        '
        'cbShoarBold
        '
        Me.cbShoarBold.AutoSize = True
        Me.cbShoarBold.Location = New System.Drawing.Point(33, 20)
        Me.cbShoarBold.Name = "cbShoarBold"
        Me.cbShoarBold.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cbShoarBold.Size = New System.Drawing.Size(46, 17)
        Me.cbShoarBold.TabIndex = 44
        Me.cbShoarBold.Text = "Bold"
        Me.cbShoarBold.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(10, 151)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(44, 23)
        Me.btnDelete.TabIndex = 54
        Me.btnDelete.Text = "حذف"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'lvCardText
        '
        Me.lvCardText.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvCardText.CausesValidation = False
        Me.lvCardText.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chText, Me.chFont, Me.chBold, Me.chSize, Me.chX, Me.chY})
        Me.lvCardText.FullRowSelect = True
        Me.lvCardText.GridLines = True
        Me.lvCardText.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvCardText.HideSelection = False
        Me.lvCardText.Location = New System.Drawing.Point(115, 45)
        Me.lvCardText.MultiSelect = False
        Me.lvCardText.Name = "lvCardText"
        Me.lvCardText.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lvCardText.RightToLeftLayout = True
        Me.lvCardText.Size = New System.Drawing.Size(422, 169)
        Me.lvCardText.TabIndex = 53
        Me.lvCardText.UseCompatibleStateImageBehavior = False
        Me.lvCardText.View = System.Windows.Forms.View.Details
        '
        'chText
        '
        Me.chText.Text = "متن"
        Me.chText.Width = 120
        '
        'chFont
        '
        Me.chFont.Text = "قلم"
        Me.chFont.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.chFont.Width = 100
        '
        'chBold
        '
        Me.chBold.Text = "Bold"
        Me.chBold.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.chBold.Width = 40
        '
        'chSize
        '
        Me.chSize.Text = "اندازه"
        Me.chSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.chSize.Width = 50
        '
        'chX
        '
        Me.chX.Text = "افقی"
        Me.chX.Width = 42
        '
        'chY
        '
        Me.chY.Text = "عمودی"
        Me.chY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.chY.Width = 53
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(10, 122)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(44, 23)
        Me.btnAdd.TabIndex = 50
        Me.btnAdd.Text = "اضافه"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtFontSize
        '
        Me.txtFontSize.Location = New System.Drawing.Point(7, 44)
        Me.txtFontSize.Name = "txtFontSize"
        Me.txtFontSize.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtFontSize.Size = New System.Drawing.Size(43, 21)
        Me.txtFontSize.TabIndex = 45
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(51, 48)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(31, 13)
        Me.Label27.TabIndex = 52
        Me.Label27.Text = "اندازه"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(267, 21)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(24, 13)
        Me.Label26.TabIndex = 51
        Me.Label26.Text = "قلم"
        '
        'cbFonts
        '
        Me.cbFonts.FormattingEnabled = True
        Me.cbFonts.Location = New System.Drawing.Point(115, 18)
        Me.cbFonts.Name = "cbFonts"
        Me.cbFonts.Size = New System.Drawing.Size(146, 21)
        Me.cbFonts.TabIndex = 43
        '
        'txtCardlogo
        '
        Me.txtCardlogo.Location = New System.Drawing.Point(297, 18)
        Me.txtCardlogo.Name = "txtCardlogo"
        Me.txtCardlogo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtCardlogo.Size = New System.Drawing.Size(187, 21)
        Me.txtCardlogo.TabIndex = 42
        '
        'txtX
        '
        Me.txtX.Location = New System.Drawing.Point(6, 71)
        Me.txtX.Name = "txtX"
        Me.txtX.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtX.Size = New System.Drawing.Size(43, 21)
        Me.txtX.TabIndex = 46
        '
        'txtY
        '
        Me.txtY.Location = New System.Drawing.Point(6, 95)
        Me.txtY.Name = "txtY"
        Me.txtY.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtY.Size = New System.Drawing.Size(43, 21)
        Me.txtY.TabIndex = 47
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(51, 75)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(56, 13)
        Me.Label28.TabIndex = 49
        Me.Label28.Text = "محل افقی"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(51, 99)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(65, 13)
        Me.Label29.TabIndex = 47
        Me.Label29.Text = "محل عمودی"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(489, 21)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(25, 13)
        Me.Label30.TabIndex = 46
        Me.Label30.Text = "متن"
        '
        'pbResult
        '
        Me.pbResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbResult.Location = New System.Drawing.Point(13, 329)
        Me.pbResult.Name = "pbResult"
        Me.pbResult.Size = New System.Drawing.Size(294, 214)
        Me.pbResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbResult.TabIndex = 101
        Me.pbResult.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkCenterSheba)
        Me.GroupBox1.Controls.Add(Me.cbShebaBold)
        Me.GroupBox1.Controls.Add(Me.chkRTLSheba)
        Me.GroupBox1.Controls.Add(Me.Label46)
        Me.GroupBox1.Controls.Add(Me.txtFontSizeSheba)
        Me.GroupBox1.Controls.Add(Me.Label47)
        Me.GroupBox1.Controls.Add(Me.Label48)
        Me.GroupBox1.Controls.Add(Me.cbFontSheba)
        Me.GroupBox1.Controls.Add(Me.txtXSheba)
        Me.GroupBox1.Controls.Add(Me.txtYSheba)
        Me.GroupBox1.Controls.Add(Me.Label49)
        Me.GroupBox1.Controls.Add(Me.Label50)
        Me.GroupBox1.Controls.Add(Me.cbSheba)
        Me.GroupBox1.Controls.Add(Me.chkCenterAccNo)
        Me.GroupBox1.Controls.Add(Me.cbAccNoBold)
        Me.GroupBox1.Controls.Add(Me.chkRTLAccNo)
        Me.GroupBox1.Controls.Add(Me.Label41)
        Me.GroupBox1.Controls.Add(Me.txtFontSizeAccNo)
        Me.GroupBox1.Controls.Add(Me.Label42)
        Me.GroupBox1.Controls.Add(Me.Label43)
        Me.GroupBox1.Controls.Add(Me.cbFontAccNo)
        Me.GroupBox1.Controls.Add(Me.txtXAccNo)
        Me.GroupBox1.Controls.Add(Me.txtYAccNo)
        Me.GroupBox1.Controls.Add(Me.Label44)
        Me.GroupBox1.Controls.Add(Me.Label45)
        Me.GroupBox1.Controls.Add(Me.cbAccNo)
        Me.GroupBox1.Controls.Add(Me.txtXlogo)
        Me.GroupBox1.Controls.Add(Me.txtYlogo)
        Me.GroupBox1.Controls.Add(Me.Label38)
        Me.GroupBox1.Controls.Add(Me.Label39)
        Me.GroupBox1.Controls.Add(Me.Label37)
        Me.GroupBox1.Controls.Add(Me.cbLogo)
        Me.GroupBox1.Controls.Add(Me.chkCenterSpecialText)
        Me.GroupBox1.Controls.Add(Me.cbSpecialTextBold)
        Me.GroupBox1.Controls.Add(Me.chkRTLspecialText)
        Me.GroupBox1.Controls.Add(Me.Label32)
        Me.GroupBox1.Controls.Add(Me.txtFontSizeSpecialtext)
        Me.GroupBox1.Controls.Add(Me.Label33)
        Me.GroupBox1.Controls.Add(Me.Label34)
        Me.GroupBox1.Controls.Add(Me.cbFontSpecialText)
        Me.GroupBox1.Controls.Add(Me.txtXSpecialText)
        Me.GroupBox1.Controls.Add(Me.txtYSpecialText)
        Me.GroupBox1.Controls.Add(Me.Label35)
        Me.GroupBox1.Controls.Add(Me.Label36)
        Me.GroupBox1.Controls.Add(Me.chkCenterName)
        Me.GroupBox1.Controls.Add(Me.chkCenterCVV)
        Me.GroupBox1.Controls.Add(Me.chkCenterEXP)
        Me.GroupBox1.Controls.Add(Me.chkCenterBrCode)
        Me.GroupBox1.Controls.Add(Me.chkCenterPAN)
        Me.GroupBox1.Controls.Add(Me.cbSpecialText)
        Me.GroupBox1.Controls.Add(Me.cbBrCodeBold)
        Me.GroupBox1.Controls.Add(Me.cbExpDateBold)
        Me.GroupBox1.Controls.Add(Me.cbCvvBold)
        Me.GroupBox1.Controls.Add(Me.cbNameBold)
        Me.GroupBox1.Controls.Add(Me.cbPanBold)
        Me.GroupBox1.Controls.Add(Me.cbBRANCH)
        Me.GroupBox1.Controls.Add(Me.cbEXP)
        Me.GroupBox1.Controls.Add(Me.cbCVV2)
        Me.GroupBox1.Controls.Add(Me.cbNAME)
        Me.GroupBox1.Controls.Add(Me.cbPAN)
        Me.GroupBox1.Controls.Add(Me.chkRTLname)
        Me.GroupBox1.Controls.Add(Me.chkRTLcvv2)
        Me.GroupBox1.Controls.Add(Me.chkRTLexp)
        Me.GroupBox1.Controls.Add(Me.chkRTLbranch)
        Me.GroupBox1.Controls.Add(Me.chkRTLpan)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.txtFontSizeBrCode)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.cbFontBrCode)
        Me.GroupBox1.Controls.Add(Me.txtXbrcode)
        Me.GroupBox1.Controls.Add(Me.txtYbrcode)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtFontSizeExpDate)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.cbFontExpDate)
        Me.GroupBox1.Controls.Add(Me.txtXexpdate)
        Me.GroupBox1.Controls.Add(Me.txtYexpdate)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtFontSizeCVV2)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.cbFontCVV2)
        Me.GroupBox1.Controls.Add(Me.txtXcvv2)
        Me.GroupBox1.Controls.Add(Me.txtYcvv2)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtFontSizeName)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cbFontName)
        Me.GroupBox1.Controls.Add(Me.txtXname)
        Me.GroupBox1.Controls.Add(Me.txtYname)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtFontSizePAN)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cbFontPAN)
        Me.GroupBox1.Controls.Add(Me.txtXpan)
        Me.GroupBox1.Controls.Add(Me.txtYpan)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 60)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox1.Size = New System.Drawing.Size(851, 263)
        Me.GroupBox1.TabIndex = 102
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "تنظیمات الگوی کارت"
        '
        'chkCenterSheba
        '
        Me.chkCenterSheba.AutoSize = True
        Me.chkCenterSheba.Location = New System.Drawing.Point(6, 237)
        Me.chkCenterSheba.Name = "chkCenterSheba"
        Me.chkCenterSheba.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkCenterSheba.Size = New System.Drawing.Size(72, 17)
        Me.chkCenterSheba.TabIndex = 176
        Me.chkCenterSheba.Text = "وسط چین"
        Me.chkCenterSheba.UseVisualStyleBackColor = True
        '
        'cbShebaBold
        '
        Me.cbShebaBold.AutoSize = True
        Me.cbShebaBold.Location = New System.Drawing.Point(482, 234)
        Me.cbShebaBold.Name = "cbShebaBold"
        Me.cbShebaBold.Size = New System.Drawing.Size(46, 17)
        Me.cbShebaBold.TabIndex = 175
        Me.cbShebaBold.Text = "Bold"
        Me.cbShebaBold.UseVisualStyleBackColor = True
        '
        'chkRTLSheba
        '
        Me.chkRTLSheba.AutoSize = True
        Me.chkRTLSheba.Location = New System.Drawing.Point(82, 237)
        Me.chkRTLSheba.Name = "chkRTLSheba"
        Me.chkRTLSheba.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkRTLSheba.Size = New System.Drawing.Size(86, 17)
        Me.chkRTLSheba.TabIndex = 174
        Me.chkRTLSheba.Text = "راست به چپ"
        Me.chkRTLSheba.UseVisualStyleBackColor = True
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.ForeColor = System.Drawing.Color.Maroon
        Me.Label46.Location = New System.Drawing.Point(747, 237)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(62, 13)
        Me.Label46.TabIndex = 173
        Me.Label46.Text = "شماره شبا:"
        '
        'txtFontSizeSheba
        '
        Me.txtFontSizeSheba.Location = New System.Drawing.Point(397, 232)
        Me.txtFontSizeSheba.Name = "txtFontSizeSheba"
        Me.txtFontSizeSheba.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtFontSizeSheba.Size = New System.Drawing.Size(43, 21)
        Me.txtFontSizeSheba.TabIndex = 166
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(446, 235)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(31, 13)
        Me.Label47.TabIndex = 172
        Me.Label47.Text = "اندازه"
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(717, 237)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(24, 13)
        Me.Label48.TabIndex = 171
        Me.Label48.Text = "قلم"
        '
        'cbFontSheba
        '
        Me.cbFontSheba.FormattingEnabled = True
        Me.cbFontSheba.Location = New System.Drawing.Point(533, 234)
        Me.cbFontSheba.Name = "cbFontSheba"
        Me.cbFontSheba.Size = New System.Drawing.Size(178, 21)
        Me.cbFontSheba.TabIndex = 165
        '
        'txtXSheba
        '
        Me.txtXSheba.Location = New System.Drawing.Point(290, 232)
        Me.txtXSheba.Name = "txtXSheba"
        Me.txtXSheba.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtXSheba.Size = New System.Drawing.Size(41, 21)
        Me.txtXSheba.TabIndex = 167
        '
        'txtYSheba
        '
        Me.txtYSheba.Location = New System.Drawing.Point(176, 232)
        Me.txtYSheba.Name = "txtYSheba"
        Me.txtYSheba.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtYSheba.Size = New System.Drawing.Size(41, 21)
        Me.txtYSheba.TabIndex = 168
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(334, 235)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(56, 13)
        Me.Label49.TabIndex = 170
        Me.Label49.Text = "محل افقی"
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Location = New System.Drawing.Point(221, 235)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(65, 13)
        Me.Label50.TabIndex = 169
        Me.Label50.Text = "محل عمودی"
        '
        'cbSheba
        '
        Me.cbSheba.AutoSize = True
        Me.cbSheba.Location = New System.Drawing.Point(824, 238)
        Me.cbSheba.Name = "cbSheba"
        Me.cbSheba.Size = New System.Drawing.Size(15, 14)
        Me.cbSheba.TabIndex = 164
        Me.cbSheba.UseVisualStyleBackColor = True
        '
        'chkCenterAccNo
        '
        Me.chkCenterAccNo.AutoSize = True
        Me.chkCenterAccNo.Location = New System.Drawing.Point(6, 212)
        Me.chkCenterAccNo.Name = "chkCenterAccNo"
        Me.chkCenterAccNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkCenterAccNo.Size = New System.Drawing.Size(72, 17)
        Me.chkCenterAccNo.TabIndex = 163
        Me.chkCenterAccNo.Text = "وسط چین"
        Me.chkCenterAccNo.UseVisualStyleBackColor = True
        '
        'cbAccNoBold
        '
        Me.cbAccNoBold.AutoSize = True
        Me.cbAccNoBold.Location = New System.Drawing.Point(482, 209)
        Me.cbAccNoBold.Name = "cbAccNoBold"
        Me.cbAccNoBold.Size = New System.Drawing.Size(46, 17)
        Me.cbAccNoBold.TabIndex = 162
        Me.cbAccNoBold.Text = "Bold"
        Me.cbAccNoBold.UseVisualStyleBackColor = True
        '
        'chkRTLAccNo
        '
        Me.chkRTLAccNo.AutoSize = True
        Me.chkRTLAccNo.Location = New System.Drawing.Point(82, 212)
        Me.chkRTLAccNo.Name = "chkRTLAccNo"
        Me.chkRTLAccNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkRTLAccNo.Size = New System.Drawing.Size(86, 17)
        Me.chkRTLAccNo.TabIndex = 161
        Me.chkRTLAccNo.Text = "راست به چپ"
        Me.chkRTLAccNo.UseVisualStyleBackColor = True
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.ForeColor = System.Drawing.Color.Maroon
        Me.Label41.Location = New System.Drawing.Point(747, 212)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(75, 13)
        Me.Label41.TabIndex = 160
        Me.Label41.Text = "شماره حساب:"
        '
        'txtFontSizeAccNo
        '
        Me.txtFontSizeAccNo.Location = New System.Drawing.Point(397, 207)
        Me.txtFontSizeAccNo.Name = "txtFontSizeAccNo"
        Me.txtFontSizeAccNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtFontSizeAccNo.Size = New System.Drawing.Size(43, 21)
        Me.txtFontSizeAccNo.TabIndex = 153
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(446, 210)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(31, 13)
        Me.Label42.TabIndex = 159
        Me.Label42.Text = "اندازه"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(717, 212)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(24, 13)
        Me.Label43.TabIndex = 158
        Me.Label43.Text = "قلم"
        '
        'cbFontAccNo
        '
        Me.cbFontAccNo.FormattingEnabled = True
        Me.cbFontAccNo.Location = New System.Drawing.Point(533, 209)
        Me.cbFontAccNo.Name = "cbFontAccNo"
        Me.cbFontAccNo.Size = New System.Drawing.Size(178, 21)
        Me.cbFontAccNo.TabIndex = 152
        '
        'txtXAccNo
        '
        Me.txtXAccNo.Location = New System.Drawing.Point(290, 207)
        Me.txtXAccNo.Name = "txtXAccNo"
        Me.txtXAccNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtXAccNo.Size = New System.Drawing.Size(41, 21)
        Me.txtXAccNo.TabIndex = 154
        '
        'txtYAccNo
        '
        Me.txtYAccNo.Location = New System.Drawing.Point(176, 207)
        Me.txtYAccNo.Name = "txtYAccNo"
        Me.txtYAccNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtYAccNo.Size = New System.Drawing.Size(41, 21)
        Me.txtYAccNo.TabIndex = 155
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(334, 210)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(56, 13)
        Me.Label44.TabIndex = 157
        Me.Label44.Text = "محل افقی"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(221, 210)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(65, 13)
        Me.Label45.TabIndex = 156
        Me.Label45.Text = "محل عمودی"
        '
        'cbAccNo
        '
        Me.cbAccNo.AutoSize = True
        Me.cbAccNo.Location = New System.Drawing.Point(824, 213)
        Me.cbAccNo.Name = "cbAccNo"
        Me.cbAccNo.Size = New System.Drawing.Size(15, 14)
        Me.cbAccNo.TabIndex = 151
        Me.cbAccNo.UseVisualStyleBackColor = True
        '
        'txtXlogo
        '
        Me.txtXlogo.Location = New System.Drawing.Point(290, 181)
        Me.txtXlogo.Name = "txtXlogo"
        Me.txtXlogo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtXlogo.Size = New System.Drawing.Size(41, 21)
        Me.txtXlogo.TabIndex = 147
        '
        'txtYlogo
        '
        Me.txtYlogo.Location = New System.Drawing.Point(176, 181)
        Me.txtYlogo.Name = "txtYlogo"
        Me.txtYlogo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtYlogo.Size = New System.Drawing.Size(41, 21)
        Me.txtYlogo.TabIndex = 148
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(334, 184)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(56, 13)
        Me.Label38.TabIndex = 150
        Me.Label38.Text = "محل افقی"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(221, 184)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(65, 13)
        Me.Label39.TabIndex = 149
        Me.Label39.Text = "محل عمودی"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.ForeColor = System.Drawing.Color.Maroon
        Me.Label37.Location = New System.Drawing.Point(747, 183)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(29, 13)
        Me.Label37.TabIndex = 146
        Me.Label37.Text = "لوگو:"
        '
        'cbLogo
        '
        Me.cbLogo.AutoSize = True
        Me.cbLogo.Location = New System.Drawing.Point(824, 184)
        Me.cbLogo.Name = "cbLogo"
        Me.cbLogo.Size = New System.Drawing.Size(15, 14)
        Me.cbLogo.TabIndex = 145
        Me.cbLogo.UseVisualStyleBackColor = True
        '
        'chkCenterSpecialText
        '
        Me.chkCenterSpecialText.AutoSize = True
        Me.chkCenterSpecialText.Location = New System.Drawing.Point(6, 158)
        Me.chkCenterSpecialText.Name = "chkCenterSpecialText"
        Me.chkCenterSpecialText.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkCenterSpecialText.Size = New System.Drawing.Size(72, 17)
        Me.chkCenterSpecialText.TabIndex = 144
        Me.chkCenterSpecialText.Text = "وسط چین"
        Me.chkCenterSpecialText.UseVisualStyleBackColor = True
        '
        'cbSpecialTextBold
        '
        Me.cbSpecialTextBold.AutoSize = True
        Me.cbSpecialTextBold.Location = New System.Drawing.Point(482, 155)
        Me.cbSpecialTextBold.Name = "cbSpecialTextBold"
        Me.cbSpecialTextBold.Size = New System.Drawing.Size(46, 17)
        Me.cbSpecialTextBold.TabIndex = 143
        Me.cbSpecialTextBold.Text = "Bold"
        Me.cbSpecialTextBold.UseVisualStyleBackColor = True
        '
        'chkRTLspecialText
        '
        Me.chkRTLspecialText.AutoSize = True
        Me.chkRTLspecialText.Location = New System.Drawing.Point(82, 158)
        Me.chkRTLspecialText.Name = "chkRTLspecialText"
        Me.chkRTLspecialText.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkRTLspecialText.Size = New System.Drawing.Size(86, 17)
        Me.chkRTLspecialText.TabIndex = 142
        Me.chkRTLspecialText.Text = "راست به چپ"
        Me.chkRTLspecialText.UseVisualStyleBackColor = True
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.ForeColor = System.Drawing.Color.Maroon
        Me.Label32.Location = New System.Drawing.Point(747, 158)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(55, 13)
        Me.Label32.TabIndex = 141
        Me.Label32.Text = "متن خاص:"
        '
        'txtFontSizeSpecialtext
        '
        Me.txtFontSizeSpecialtext.Location = New System.Drawing.Point(397, 153)
        Me.txtFontSizeSpecialtext.Name = "txtFontSizeSpecialtext"
        Me.txtFontSizeSpecialtext.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtFontSizeSpecialtext.Size = New System.Drawing.Size(43, 21)
        Me.txtFontSizeSpecialtext.TabIndex = 134
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(446, 156)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(31, 13)
        Me.Label33.TabIndex = 140
        Me.Label33.Text = "اندازه"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(717, 158)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(24, 13)
        Me.Label34.TabIndex = 139
        Me.Label34.Text = "قلم"
        '
        'cbFontSpecialText
        '
        Me.cbFontSpecialText.FormattingEnabled = True
        Me.cbFontSpecialText.Location = New System.Drawing.Point(533, 155)
        Me.cbFontSpecialText.Name = "cbFontSpecialText"
        Me.cbFontSpecialText.Size = New System.Drawing.Size(178, 21)
        Me.cbFontSpecialText.TabIndex = 133
        '
        'txtXSpecialText
        '
        Me.txtXSpecialText.Location = New System.Drawing.Point(290, 153)
        Me.txtXSpecialText.Name = "txtXSpecialText"
        Me.txtXSpecialText.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtXSpecialText.Size = New System.Drawing.Size(41, 21)
        Me.txtXSpecialText.TabIndex = 135
        '
        'txtYSpecialText
        '
        Me.txtYSpecialText.Location = New System.Drawing.Point(176, 153)
        Me.txtYSpecialText.Name = "txtYSpecialText"
        Me.txtYSpecialText.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtYSpecialText.Size = New System.Drawing.Size(41, 21)
        Me.txtYSpecialText.TabIndex = 136
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(334, 156)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(56, 13)
        Me.Label35.TabIndex = 138
        Me.Label35.Text = "محل افقی"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(221, 156)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(65, 13)
        Me.Label36.TabIndex = 137
        Me.Label36.Text = "محل عمودی"
        '
        'chkCenterName
        '
        Me.chkCenterName.AutoSize = True
        Me.chkCenterName.Location = New System.Drawing.Point(6, 48)
        Me.chkCenterName.Name = "chkCenterName"
        Me.chkCenterName.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkCenterName.Size = New System.Drawing.Size(72, 17)
        Me.chkCenterName.TabIndex = 132
        Me.chkCenterName.Text = "وسط چین"
        Me.chkCenterName.UseVisualStyleBackColor = True
        '
        'chkCenterCVV
        '
        Me.chkCenterCVV.AutoSize = True
        Me.chkCenterCVV.Location = New System.Drawing.Point(6, 75)
        Me.chkCenterCVV.Name = "chkCenterCVV"
        Me.chkCenterCVV.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkCenterCVV.Size = New System.Drawing.Size(72, 17)
        Me.chkCenterCVV.TabIndex = 131
        Me.chkCenterCVV.Text = "وسط چین"
        Me.chkCenterCVV.UseVisualStyleBackColor = True
        '
        'chkCenterEXP
        '
        Me.chkCenterEXP.AutoSize = True
        Me.chkCenterEXP.Location = New System.Drawing.Point(6, 104)
        Me.chkCenterEXP.Name = "chkCenterEXP"
        Me.chkCenterEXP.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkCenterEXP.Size = New System.Drawing.Size(72, 17)
        Me.chkCenterEXP.TabIndex = 130
        Me.chkCenterEXP.Text = "وسط چین"
        Me.chkCenterEXP.UseVisualStyleBackColor = True
        '
        'chkCenterBrCode
        '
        Me.chkCenterBrCode.AutoSize = True
        Me.chkCenterBrCode.Location = New System.Drawing.Point(6, 131)
        Me.chkCenterBrCode.Name = "chkCenterBrCode"
        Me.chkCenterBrCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkCenterBrCode.Size = New System.Drawing.Size(72, 17)
        Me.chkCenterBrCode.TabIndex = 129
        Me.chkCenterBrCode.Text = "وسط چین"
        Me.chkCenterBrCode.UseVisualStyleBackColor = True
        '
        'chkCenterPAN
        '
        Me.chkCenterPAN.AutoSize = True
        Me.chkCenterPAN.Location = New System.Drawing.Point(6, 16)
        Me.chkCenterPAN.Name = "chkCenterPAN"
        Me.chkCenterPAN.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkCenterPAN.Size = New System.Drawing.Size(72, 17)
        Me.chkCenterPAN.TabIndex = 128
        Me.chkCenterPAN.Text = "وسط چین"
        Me.chkCenterPAN.UseVisualStyleBackColor = True
        '
        'cbSpecialText
        '
        Me.cbSpecialText.AutoSize = True
        Me.cbSpecialText.Location = New System.Drawing.Point(824, 159)
        Me.cbSpecialText.Name = "cbSpecialText"
        Me.cbSpecialText.Size = New System.Drawing.Size(15, 14)
        Me.cbSpecialText.TabIndex = 127
        Me.cbSpecialText.UseVisualStyleBackColor = True
        '
        'cbBrCodeBold
        '
        Me.cbBrCodeBold.AutoSize = True
        Me.cbBrCodeBold.Location = New System.Drawing.Point(482, 125)
        Me.cbBrCodeBold.Name = "cbBrCodeBold"
        Me.cbBrCodeBold.Size = New System.Drawing.Size(46, 17)
        Me.cbBrCodeBold.TabIndex = 126
        Me.cbBrCodeBold.Text = "Bold"
        Me.cbBrCodeBold.UseVisualStyleBackColor = True
        '
        'cbExpDateBold
        '
        Me.cbExpDateBold.AutoSize = True
        Me.cbExpDateBold.Location = New System.Drawing.Point(482, 98)
        Me.cbExpDateBold.Name = "cbExpDateBold"
        Me.cbExpDateBold.Size = New System.Drawing.Size(46, 17)
        Me.cbExpDateBold.TabIndex = 125
        Me.cbExpDateBold.Text = "Bold"
        Me.cbExpDateBold.UseVisualStyleBackColor = True
        '
        'cbCvvBold
        '
        Me.cbCvvBold.AutoSize = True
        Me.cbCvvBold.Location = New System.Drawing.Point(482, 71)
        Me.cbCvvBold.Name = "cbCvvBold"
        Me.cbCvvBold.Size = New System.Drawing.Size(46, 17)
        Me.cbCvvBold.TabIndex = 124
        Me.cbCvvBold.Text = "Bold"
        Me.cbCvvBold.UseVisualStyleBackColor = True
        '
        'cbNameBold
        '
        Me.cbNameBold.AutoSize = True
        Me.cbNameBold.Location = New System.Drawing.Point(482, 44)
        Me.cbNameBold.Name = "cbNameBold"
        Me.cbNameBold.Size = New System.Drawing.Size(46, 17)
        Me.cbNameBold.TabIndex = 123
        Me.cbNameBold.Text = "Bold"
        Me.cbNameBold.UseVisualStyleBackColor = True
        '
        'cbPanBold
        '
        Me.cbPanBold.AutoSize = True
        Me.cbPanBold.Location = New System.Drawing.Point(482, 17)
        Me.cbPanBold.Name = "cbPanBold"
        Me.cbPanBold.Size = New System.Drawing.Size(46, 17)
        Me.cbPanBold.TabIndex = 122
        Me.cbPanBold.Text = "Bold"
        Me.cbPanBold.UseVisualStyleBackColor = True
        '
        'cbBRANCH
        '
        Me.cbBRANCH.AutoSize = True
        Me.cbBRANCH.Location = New System.Drawing.Point(824, 130)
        Me.cbBRANCH.Name = "cbBRANCH"
        Me.cbBRANCH.Size = New System.Drawing.Size(15, 14)
        Me.cbBRANCH.TabIndex = 121
        Me.cbBRANCH.UseVisualStyleBackColor = True
        '
        'cbEXP
        '
        Me.cbEXP.AutoSize = True
        Me.cbEXP.Location = New System.Drawing.Point(824, 103)
        Me.cbEXP.Name = "cbEXP"
        Me.cbEXP.Size = New System.Drawing.Size(15, 14)
        Me.cbEXP.TabIndex = 120
        Me.cbEXP.UseVisualStyleBackColor = True
        '
        'cbCVV2
        '
        Me.cbCVV2.AutoSize = True
        Me.cbCVV2.Location = New System.Drawing.Point(824, 76)
        Me.cbCVV2.Name = "cbCVV2"
        Me.cbCVV2.Size = New System.Drawing.Size(15, 14)
        Me.cbCVV2.TabIndex = 119
        Me.cbCVV2.UseVisualStyleBackColor = True
        '
        'cbNAME
        '
        Me.cbNAME.AutoSize = True
        Me.cbNAME.Location = New System.Drawing.Point(824, 49)
        Me.cbNAME.Name = "cbNAME"
        Me.cbNAME.Size = New System.Drawing.Size(15, 14)
        Me.cbNAME.TabIndex = 118
        Me.cbNAME.UseVisualStyleBackColor = True
        '
        'cbPAN
        '
        Me.cbPAN.AutoSize = True
        Me.cbPAN.Location = New System.Drawing.Point(824, 22)
        Me.cbPAN.Name = "cbPAN"
        Me.cbPAN.Size = New System.Drawing.Size(15, 14)
        Me.cbPAN.TabIndex = 55
        Me.cbPAN.UseVisualStyleBackColor = True
        '
        'chkRTLname
        '
        Me.chkRTLname.AutoSize = True
        Me.chkRTLname.Location = New System.Drawing.Point(82, 45)
        Me.chkRTLname.Name = "chkRTLname"
        Me.chkRTLname.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkRTLname.Size = New System.Drawing.Size(86, 17)
        Me.chkRTLname.TabIndex = 117
        Me.chkRTLname.Text = "راست به چپ"
        Me.chkRTLname.UseVisualStyleBackColor = True
        '
        'chkRTLcvv2
        '
        Me.chkRTLcvv2.AutoSize = True
        Me.chkRTLcvv2.Location = New System.Drawing.Point(82, 72)
        Me.chkRTLcvv2.Name = "chkRTLcvv2"
        Me.chkRTLcvv2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkRTLcvv2.Size = New System.Drawing.Size(86, 17)
        Me.chkRTLcvv2.TabIndex = 116
        Me.chkRTLcvv2.Text = "راست به چپ"
        Me.chkRTLcvv2.UseVisualStyleBackColor = True
        '
        'chkRTLexp
        '
        Me.chkRTLexp.AutoSize = True
        Me.chkRTLexp.Location = New System.Drawing.Point(82, 101)
        Me.chkRTLexp.Name = "chkRTLexp"
        Me.chkRTLexp.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkRTLexp.Size = New System.Drawing.Size(86, 17)
        Me.chkRTLexp.TabIndex = 115
        Me.chkRTLexp.Text = "راست به چپ"
        Me.chkRTLexp.UseVisualStyleBackColor = True
        '
        'chkRTLbranch
        '
        Me.chkRTLbranch.AutoSize = True
        Me.chkRTLbranch.Location = New System.Drawing.Point(82, 128)
        Me.chkRTLbranch.Name = "chkRTLbranch"
        Me.chkRTLbranch.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkRTLbranch.Size = New System.Drawing.Size(86, 17)
        Me.chkRTLbranch.TabIndex = 114
        Me.chkRTLbranch.Text = "راست به چپ"
        Me.chkRTLbranch.UseVisualStyleBackColor = True
        '
        'chkRTLpan
        '
        Me.chkRTLpan.AutoSize = True
        Me.chkRTLpan.Location = New System.Drawing.Point(82, 16)
        Me.chkRTLpan.Name = "chkRTLpan"
        Me.chkRTLpan.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkRTLpan.Size = New System.Drawing.Size(86, 17)
        Me.chkRTLpan.TabIndex = 113
        Me.chkRTLpan.Text = "راست به چپ"
        Me.chkRTLpan.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.ForeColor = System.Drawing.Color.Maroon
        Me.Label25.Location = New System.Drawing.Point(747, 128)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(52, 13)
        Me.Label25.TabIndex = 112
        Me.Label25.Text = "کد شعبه:"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.ForeColor = System.Drawing.Color.Maroon
        Me.Label24.Location = New System.Drawing.Point(747, 101)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(63, 13)
        Me.Label24.TabIndex = 111
        Me.Label24.Text = "تاریخ انقضاء:"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.ForeColor = System.Drawing.Color.Maroon
        Me.Label23.Location = New System.Drawing.Point(747, 74)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(36, 13)
        Me.Label23.TabIndex = 110
        Me.Label23.Text = "CVV2:"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.ForeColor = System.Drawing.Color.Maroon
        Me.Label22.Location = New System.Drawing.Point(747, 47)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(64, 13)
        Me.Label22.TabIndex = 109
        Me.Label22.Text = "نام مشتری:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.ForeColor = System.Drawing.Color.Maroon
        Me.Label21.Location = New System.Drawing.Point(747, 20)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(65, 13)
        Me.Label21.TabIndex = 108
        Me.Label21.Text = "شماره کارت:"
        '
        'txtFontSizeBrCode
        '
        Me.txtFontSizeBrCode.Location = New System.Drawing.Point(397, 123)
        Me.txtFontSizeBrCode.Name = "txtFontSizeBrCode"
        Me.txtFontSizeBrCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtFontSizeBrCode.Size = New System.Drawing.Size(43, 21)
        Me.txtFontSizeBrCode.TabIndex = 101
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(446, 126)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(31, 13)
        Me.Label17.TabIndex = 107
        Me.Label17.Text = "اندازه"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(717, 128)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(24, 13)
        Me.Label18.TabIndex = 106
        Me.Label18.Text = "قلم"
        '
        'cbFontBrCode
        '
        Me.cbFontBrCode.FormattingEnabled = True
        Me.cbFontBrCode.Location = New System.Drawing.Point(533, 125)
        Me.cbFontBrCode.Name = "cbFontBrCode"
        Me.cbFontBrCode.Size = New System.Drawing.Size(178, 21)
        Me.cbFontBrCode.TabIndex = 100
        '
        'txtXbrcode
        '
        Me.txtXbrcode.Location = New System.Drawing.Point(290, 123)
        Me.txtXbrcode.Name = "txtXbrcode"
        Me.txtXbrcode.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtXbrcode.Size = New System.Drawing.Size(41, 21)
        Me.txtXbrcode.TabIndex = 102
        '
        'txtYbrcode
        '
        Me.txtYbrcode.Location = New System.Drawing.Point(176, 123)
        Me.txtYbrcode.Name = "txtYbrcode"
        Me.txtYbrcode.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtYbrcode.Size = New System.Drawing.Size(41, 21)
        Me.txtYbrcode.TabIndex = 103
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(334, 126)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(56, 13)
        Me.Label19.TabIndex = 105
        Me.Label19.Text = "محل افقی"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(221, 126)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(65, 13)
        Me.Label20.TabIndex = 104
        Me.Label20.Text = "محل عمودی"
        '
        'txtFontSizeExpDate
        '
        Me.txtFontSizeExpDate.Location = New System.Drawing.Point(397, 96)
        Me.txtFontSizeExpDate.Name = "txtFontSizeExpDate"
        Me.txtFontSizeExpDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtFontSizeExpDate.Size = New System.Drawing.Size(43, 21)
        Me.txtFontSizeExpDate.TabIndex = 93
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(446, 99)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(31, 13)
        Me.Label13.TabIndex = 99
        Me.Label13.Text = "اندازه"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(717, 101)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(24, 13)
        Me.Label14.TabIndex = 98
        Me.Label14.Text = "قلم"
        '
        'cbFontExpDate
        '
        Me.cbFontExpDate.FormattingEnabled = True
        Me.cbFontExpDate.Location = New System.Drawing.Point(533, 98)
        Me.cbFontExpDate.Name = "cbFontExpDate"
        Me.cbFontExpDate.Size = New System.Drawing.Size(178, 21)
        Me.cbFontExpDate.TabIndex = 92
        '
        'txtXexpdate
        '
        Me.txtXexpdate.Location = New System.Drawing.Point(290, 96)
        Me.txtXexpdate.Name = "txtXexpdate"
        Me.txtXexpdate.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtXexpdate.Size = New System.Drawing.Size(41, 21)
        Me.txtXexpdate.TabIndex = 94
        '
        'txtYexpdate
        '
        Me.txtYexpdate.Location = New System.Drawing.Point(176, 96)
        Me.txtYexpdate.Name = "txtYexpdate"
        Me.txtYexpdate.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtYexpdate.Size = New System.Drawing.Size(41, 21)
        Me.txtYexpdate.TabIndex = 95
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(334, 99)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(56, 13)
        Me.Label15.TabIndex = 97
        Me.Label15.Text = "محل افقی"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(221, 99)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(65, 13)
        Me.Label16.TabIndex = 96
        Me.Label16.Text = "محل عمودی"
        '
        'txtFontSizeCVV2
        '
        Me.txtFontSizeCVV2.Location = New System.Drawing.Point(397, 69)
        Me.txtFontSizeCVV2.Name = "txtFontSizeCVV2"
        Me.txtFontSizeCVV2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtFontSizeCVV2.Size = New System.Drawing.Size(43, 21)
        Me.txtFontSizeCVV2.TabIndex = 85
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(446, 72)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(31, 13)
        Me.Label9.TabIndex = 91
        Me.Label9.Text = "اندازه"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(717, 74)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(24, 13)
        Me.Label10.TabIndex = 90
        Me.Label10.Text = "قلم"
        '
        'cbFontCVV2
        '
        Me.cbFontCVV2.FormattingEnabled = True
        Me.cbFontCVV2.Location = New System.Drawing.Point(533, 71)
        Me.cbFontCVV2.Name = "cbFontCVV2"
        Me.cbFontCVV2.Size = New System.Drawing.Size(178, 21)
        Me.cbFontCVV2.TabIndex = 84
        '
        'txtXcvv2
        '
        Me.txtXcvv2.Location = New System.Drawing.Point(290, 69)
        Me.txtXcvv2.Name = "txtXcvv2"
        Me.txtXcvv2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtXcvv2.Size = New System.Drawing.Size(41, 21)
        Me.txtXcvv2.TabIndex = 86
        '
        'txtYcvv2
        '
        Me.txtYcvv2.Location = New System.Drawing.Point(176, 69)
        Me.txtYcvv2.Name = "txtYcvv2"
        Me.txtYcvv2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtYcvv2.Size = New System.Drawing.Size(41, 21)
        Me.txtYcvv2.TabIndex = 87
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(334, 72)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(56, 13)
        Me.Label11.TabIndex = 89
        Me.Label11.Text = "محل افقی"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(221, 72)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(65, 13)
        Me.Label12.TabIndex = 88
        Me.Label12.Text = "محل عمودی"
        '
        'txtFontSizeName
        '
        Me.txtFontSizeName.Location = New System.Drawing.Point(397, 42)
        Me.txtFontSizeName.Name = "txtFontSizeName"
        Me.txtFontSizeName.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtFontSizeName.Size = New System.Drawing.Size(43, 21)
        Me.txtFontSizeName.TabIndex = 77
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(446, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 83
        Me.Label1.Text = "اندازه"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(717, 47)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(24, 13)
        Me.Label6.TabIndex = 82
        Me.Label6.Text = "قلم"
        '
        'cbFontName
        '
        Me.cbFontName.FormattingEnabled = True
        Me.cbFontName.Location = New System.Drawing.Point(533, 44)
        Me.cbFontName.Name = "cbFontName"
        Me.cbFontName.Size = New System.Drawing.Size(178, 21)
        Me.cbFontName.TabIndex = 76
        '
        'txtXname
        '
        Me.txtXname.Location = New System.Drawing.Point(290, 42)
        Me.txtXname.Name = "txtXname"
        Me.txtXname.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtXname.Size = New System.Drawing.Size(41, 21)
        Me.txtXname.TabIndex = 78
        '
        'txtYname
        '
        Me.txtYname.Location = New System.Drawing.Point(176, 42)
        Me.txtYname.Name = "txtYname"
        Me.txtYname.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtYname.Size = New System.Drawing.Size(41, 21)
        Me.txtYname.TabIndex = 79
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(334, 45)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 81
        Me.Label7.Text = "محل افقی"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(221, 45)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 13)
        Me.Label8.TabIndex = 80
        Me.Label8.Text = "محل عمودی"
        '
        'txtFontSizePAN
        '
        Me.txtFontSizePAN.Location = New System.Drawing.Point(397, 15)
        Me.txtFontSizePAN.Name = "txtFontSizePAN"
        Me.txtFontSizePAN.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtFontSizePAN.Size = New System.Drawing.Size(43, 21)
        Me.txtFontSizePAN.TabIndex = 69
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(446, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(31, 13)
        Me.Label5.TabIndex = 75
        Me.Label5.Text = "اندازه"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(717, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(24, 13)
        Me.Label4.TabIndex = 74
        Me.Label4.Text = "قلم"
        '
        'cbFontPAN
        '
        Me.cbFontPAN.FormattingEnabled = True
        Me.cbFontPAN.Location = New System.Drawing.Point(533, 17)
        Me.cbFontPAN.Name = "cbFontPAN"
        Me.cbFontPAN.Size = New System.Drawing.Size(178, 21)
        Me.cbFontPAN.TabIndex = 68
        '
        'txtXpan
        '
        Me.txtXpan.Location = New System.Drawing.Point(290, 15)
        Me.txtXpan.Name = "txtXpan"
        Me.txtXpan.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtXpan.Size = New System.Drawing.Size(41, 21)
        Me.txtXpan.TabIndex = 70
        '
        'txtYpan
        '
        Me.txtYpan.Location = New System.Drawing.Point(176, 15)
        Me.txtYpan.Name = "txtYpan"
        Me.txtYpan.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtYpan.Size = New System.Drawing.Size(41, 21)
        Me.txtYpan.TabIndex = 71
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(334, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 73
        Me.Label3.Text = "محل افقی"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(221, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 72
        Me.Label2.Text = "محل عمودی"
        '
        'PrintSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(903, 574)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.pbResult)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.btnExit)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PrintSetting"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تنظیمات الگوی چاپ"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.pbResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents lvCardText As System.Windows.Forms.ListView
    Friend WithEvents chText As System.Windows.Forms.ColumnHeader
    Friend WithEvents chFont As System.Windows.Forms.ColumnHeader
    Friend WithEvents chSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents chX As System.Windows.Forms.ColumnHeader
    Friend WithEvents chY As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtFontSize As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents cbFonts As System.Windows.Forms.ComboBox
    Friend WithEvents txtCardlogo As System.Windows.Forms.TextBox
    Friend WithEvents txtX As System.Windows.Forms.TextBox
    Friend WithEvents txtY As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents pbResult As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkRTLname As System.Windows.Forms.CheckBox
    Friend WithEvents chkRTLcvv2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkRTLexp As System.Windows.Forms.CheckBox
    Friend WithEvents chkRTLbranch As System.Windows.Forms.CheckBox
    Friend WithEvents chkRTLpan As System.Windows.Forms.CheckBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtFontSizeBrCode As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cbFontBrCode As System.Windows.Forms.ComboBox
    Friend WithEvents txtXbrcode As System.Windows.Forms.TextBox
    Friend WithEvents txtYbrcode As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtFontSizeExpDate As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cbFontExpDate As System.Windows.Forms.ComboBox
    Friend WithEvents txtXexpdate As System.Windows.Forms.TextBox
    Friend WithEvents txtYexpdate As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtFontSizeCVV2 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cbFontCVV2 As System.Windows.Forms.ComboBox
    Friend WithEvents txtXcvv2 As System.Windows.Forms.TextBox
    Friend WithEvents txtYcvv2 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtFontSizeName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbFontName As System.Windows.Forms.ComboBox
    Friend WithEvents txtXname As System.Windows.Forms.TextBox
    Friend WithEvents txtYname As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtFontSizePAN As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbFontPAN As System.Windows.Forms.ComboBox
    Friend WithEvents txtXpan As System.Windows.Forms.TextBox
    Friend WithEvents txtYpan As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbBRANCH As System.Windows.Forms.CheckBox
    Friend WithEvents cbEXP As System.Windows.Forms.CheckBox
    Friend WithEvents cbCVV2 As System.Windows.Forms.CheckBox
    Friend WithEvents cbNAME As System.Windows.Forms.CheckBox
    Friend WithEvents cbPAN As System.Windows.Forms.CheckBox
    Friend WithEvents cbBrCodeBold As System.Windows.Forms.CheckBox
    Friend WithEvents cbExpDateBold As System.Windows.Forms.CheckBox
    Friend WithEvents cbCvvBold As System.Windows.Forms.CheckBox
    Friend WithEvents cbNameBold As System.Windows.Forms.CheckBox
    Friend WithEvents cbPanBold As System.Windows.Forms.CheckBox
    Friend WithEvents cbShoarBold As System.Windows.Forms.CheckBox
    Friend WithEvents chBold As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnChange As System.Windows.Forms.Button
    Friend WithEvents cbCardType As System.Windows.Forms.ComboBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents btnShowCardPrintTemplate As System.Windows.Forms.Button
    Friend WithEvents btnSavePrintTemplate As System.Windows.Forms.Button
    Friend WithEvents btnCopyTemplate As System.Windows.Forms.Button
    Friend WithEvents chkCenterName As System.Windows.Forms.CheckBox
    Friend WithEvents chkCenterCVV As System.Windows.Forms.CheckBox
    Friend WithEvents chkCenterEXP As System.Windows.Forms.CheckBox
    Friend WithEvents chkCenterBrCode As System.Windows.Forms.CheckBox
    Friend WithEvents chkCenterPAN As System.Windows.Forms.CheckBox
    Friend WithEvents cbSpecialText As System.Windows.Forms.CheckBox
    Friend WithEvents chkCenterSpecialText As System.Windows.Forms.CheckBox
    Friend WithEvents cbSpecialTextBold As System.Windows.Forms.CheckBox
    Friend WithEvents chkRTLspecialText As System.Windows.Forms.CheckBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtFontSizeSpecialtext As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents cbFontSpecialText As System.Windows.Forms.ComboBox
    Friend WithEvents txtXSpecialText As System.Windows.Forms.TextBox
    Friend WithEvents txtYSpecialText As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtXlogo As System.Windows.Forms.TextBox
    Friend WithEvents txtYlogo As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents cbLogo As System.Windows.Forms.CheckBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents cbEnabled As System.Windows.Forms.ComboBox
    Friend WithEvents txtProductCode As System.Windows.Forms.TextBox
    Friend WithEvents ProductCode As System.Windows.Forms.Label
    Friend WithEvents chkCenterSheba As System.Windows.Forms.CheckBox
    Friend WithEvents cbShebaBold As System.Windows.Forms.CheckBox
    Friend WithEvents chkRTLSheba As System.Windows.Forms.CheckBox
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents txtFontSizeSheba As System.Windows.Forms.TextBox
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents cbFontSheba As System.Windows.Forms.ComboBox
    Friend WithEvents txtXSheba As System.Windows.Forms.TextBox
    Friend WithEvents txtYSheba As System.Windows.Forms.TextBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents cbSheba As System.Windows.Forms.CheckBox
    Friend WithEvents chkCenterAccNo As System.Windows.Forms.CheckBox
    Friend WithEvents cbAccNoBold As System.Windows.Forms.CheckBox
    Friend WithEvents chkRTLAccNo As System.Windows.Forms.CheckBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtFontSizeAccNo As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents cbFontAccNo As System.Windows.Forms.ComboBox
    Friend WithEvents txtXAccNo As System.Windows.Forms.TextBox
    Friend WithEvents txtYAccNo As System.Windows.Forms.TextBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents cbAccNo As System.Windows.Forms.CheckBox
End Class
