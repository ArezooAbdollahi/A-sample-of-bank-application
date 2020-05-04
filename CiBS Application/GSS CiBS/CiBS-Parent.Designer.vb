<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CiBS_Parent
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
        Me.components = New System.ComponentModel.Container()
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.ReportUsers = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuChangePassword = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUserManagement = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogOutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.تستیToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportCustomer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCard = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCardPrint = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReprintIranCard = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPrintSetting = New System.Windows.Forms.ToolStripMenuItem()
        Me.InformUnusableCardsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPrinter = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrinterManagementToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConsumablesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RequestConsumablesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AcceptConsumablesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuConsumablesStack = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAdmin = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintTemplateSettingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdminMessageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadCardRecordsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportUsersToolStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.UserManualToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ErrorSearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ssUserInfo = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblLogedInUser = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel6 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblUserName = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel4 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblGrantLevel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblLogedInBranch = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblSoftwareVervion = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel9 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblServer = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel7 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblDate = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel8 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel10 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblUserNameWindows = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ssAdminMessage = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel5 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblAdminMessage = New System.Windows.Forms.ToolStripStatusLabel()
        Me.timerLogIn = New System.Windows.Forms.Timer(Me.components)
        Me.timerSLS = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip.SuspendLayout()
        Me.ssUserInfo.SuspendLayout()
        Me.ssAdminMessage.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReportUsers, Me.mnuCard, Me.mnuPrinter, Me.ConsumablesToolStripMenuItem, Me.mnuAdmin, Me.HelpMenu})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(1129, 24)
        Me.MenuStrip.TabIndex = 5
        Me.MenuStrip.Text = "MenuStrip"
        Me.MenuStrip.Visible = False
        '
        'ReportUsers
        '
        Me.ReportUsers.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuChangePassword, Me.mnuUserManagement, Me.ReportToolStripMenuItem, Me.LogOutToolStripMenuItem, Me.ExitToolStripMenuItem, Me.تستیToolStripMenuItem, Me.ReportCustomer})
        Me.ReportUsers.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder
        Me.ReportUsers.Name = "ReportUsers"
        Me.ReportUsers.Size = New System.Drawing.Size(48, 20)
        Me.ReportUsers.Text = "&پرونده"
        '
        'mnuChangePassword
        '
        Me.mnuChangePassword.Name = "mnuChangePassword"
        Me.mnuChangePassword.Size = New System.Drawing.Size(149, 22)
        Me.mnuChangePassword.Text = "&تغییر رمز"
        '
        'mnuUserManagement
        '
        Me.mnuUserManagement.Name = "mnuUserManagement"
        Me.mnuUserManagement.Size = New System.Drawing.Size(149, 22)
        Me.mnuUserManagement.Text = "مدیریت کاربران"
        Me.mnuUserManagement.Visible = False
        '
        'ReportToolStripMenuItem
        '
        Me.ReportToolStripMenuItem.Name = "ReportToolStripMenuItem"
        Me.ReportToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.ReportToolStripMenuItem.Text = "گزارشات"
        '
        'LogOutToolStripMenuItem
        '
        Me.LogOutToolStripMenuItem.Name = "LogOutToolStripMenuItem"
        Me.LogOutToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.LogOutToolStripMenuItem.Text = "تغییر کاربر"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.ExitToolStripMenuItem.Text = "&خروج"
        '
        'تستیToolStripMenuItem
        '
        Me.تستیToolStripMenuItem.Enabled = False
        Me.تستیToolStripMenuItem.Name = "تستیToolStripMenuItem"
        Me.تستیToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.تستیToolStripMenuItem.Text = "تستی"
        Me.تستیToolStripMenuItem.Visible = False
        '
        'ReportCustomer
        '
        Me.ReportCustomer.Enabled = False
        Me.ReportCustomer.Name = "ReportCustomer"
        Me.ReportCustomer.Size = New System.Drawing.Size(149, 22)
        Me.ReportCustomer.Text = "گزارش مشتری"
        Me.ReportCustomer.Visible = False
        '
        'mnuCard
        '
        Me.mnuCard.CheckOnClick = True
        Me.mnuCard.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCardPrint, Me.mnuReprintIranCard, Me.mnuPrintSetting, Me.InformUnusableCardsToolStripMenuItem})
        Me.mnuCard.Name = "mnuCard"
        Me.mnuCard.Size = New System.Drawing.Size(43, 20)
        Me.mnuCard.Text = "کارت"
        '
        'mnuCardPrint
        '
        Me.mnuCardPrint.Name = "mnuCardPrint"
        Me.mnuCardPrint.Size = New System.Drawing.Size(181, 22)
        Me.mnuCardPrint.Text = "چاپ کارت"
        '
        'mnuReprintIranCard
        '
        Me.mnuReprintIranCard.Name = "mnuReprintIranCard"
        Me.mnuReprintIranCard.Size = New System.Drawing.Size(181, 22)
        Me.mnuReprintIranCard.Text = "چاپ ایران کارت مجدد"
        '
        'mnuPrintSetting
        '
        Me.mnuPrintSetting.Name = "mnuPrintSetting"
        Me.mnuPrintSetting.Size = New System.Drawing.Size(181, 22)
        Me.mnuPrintSetting.Text = "تنظیم الگوی چاپ"
        Me.mnuPrintSetting.Visible = False
        '
        'InformUnusableCardsToolStripMenuItem
        '
        Me.InformUnusableCardsToolStripMenuItem.Name = "InformUnusableCardsToolStripMenuItem"
        Me.InformUnusableCardsToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.InformUnusableCardsToolStripMenuItem.Text = "اعلام معیوبی کارت"
        Me.InformUnusableCardsToolStripMenuItem.Visible = False
        '
        'mnuPrinter
        '
        Me.mnuPrinter.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrinterManagementToolStripMenuItem})
        Me.mnuPrinter.Name = "mnuPrinter"
        Me.mnuPrinter.Size = New System.Drawing.Size(45, 20)
        Me.mnuPrinter.Text = "چاپگر"
        '
        'PrinterManagementToolStripMenuItem
        '
        Me.PrinterManagementToolStripMenuItem.Name = "PrinterManagementToolStripMenuItem"
        Me.PrinterManagementToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.PrinterManagementToolStripMenuItem.Text = " نگهداری چاپگر"
        '
        'ConsumablesToolStripMenuItem
        '
        Me.ConsumablesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RequestConsumablesToolStripMenuItem, Me.AcceptConsumablesToolStripMenuItem, Me.mnuConsumablesStack})
        Me.ConsumablesToolStripMenuItem.Name = "ConsumablesToolStripMenuItem"
        Me.ConsumablesToolStripMenuItem.Size = New System.Drawing.Size(80, 20)
        Me.ConsumablesToolStripMenuItem.Text = "مواد مصرفی"
        '
        'RequestConsumablesToolStripMenuItem
        '
        Me.RequestConsumablesToolStripMenuItem.Name = "RequestConsumablesToolStripMenuItem"
        Me.RequestConsumablesToolStripMenuItem.Size = New System.Drawing.Size(246, 22)
        Me.RequestConsumablesToolStripMenuItem.Text = "درخواست مواد مصرفی"
        '
        'AcceptConsumablesToolStripMenuItem
        '
        Me.AcceptConsumablesToolStripMenuItem.Name = "AcceptConsumablesToolStripMenuItem"
        Me.AcceptConsumablesToolStripMenuItem.Size = New System.Drawing.Size(246, 22)
        Me.AcceptConsumablesToolStripMenuItem.Text = "تایید درخواست مواد مصرفی شعب"
        Me.AcceptConsumablesToolStripMenuItem.Visible = False
        '
        'mnuConsumablesStack
        '
        Me.mnuConsumablesStack.Name = "mnuConsumablesStack"
        Me.mnuConsumablesStack.Size = New System.Drawing.Size(246, 22)
        Me.mnuConsumablesStack.Text = "انبار مواد مصرفی"
        Me.mnuConsumablesStack.Visible = False
        '
        'mnuAdmin
        '
        Me.mnuAdmin.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintTemplateSettingToolStripMenuItem, Me.AdminMessageToolStripMenuItem, Me.LoadCardRecordsToolStripMenuItem, Me.ReportUsersToolStrip})
        Me.mnuAdmin.Name = "mnuAdmin"
        Me.mnuAdmin.Size = New System.Drawing.Size(85, 20)
        Me.mnuAdmin.Text = "مدیر سیستم"
        Me.mnuAdmin.Visible = False
        '
        'PrintTemplateSettingToolStripMenuItem
        '
        Me.PrintTemplateSettingToolStripMenuItem.Name = "PrintTemplateSettingToolStripMenuItem"
        Me.PrintTemplateSettingToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.PrintTemplateSettingToolStripMenuItem.Text = "تنظیم الگوی چاپ"
        '
        'AdminMessageToolStripMenuItem
        '
        Me.AdminMessageToolStripMenuItem.Name = "AdminMessageToolStripMenuItem"
        Me.AdminMessageToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.AdminMessageToolStripMenuItem.Text = "پیام مدیر سیستم"
        '
        'LoadCardRecordsToolStripMenuItem
        '
        Me.LoadCardRecordsToolStripMenuItem.Name = "LoadCardRecordsToolStripMenuItem"
        Me.LoadCardRecordsToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.LoadCardRecordsToolStripMenuItem.Text = "بارگذاری فایل کارتها"
        '
        'ReportUsersToolStrip
        '
        Me.ReportUsersToolStrip.Name = "ReportUsersToolStrip"
        Me.ReportUsersToolStrip.Size = New System.Drawing.Size(172, 22)
        Me.ReportUsersToolStrip.Text = "گزارش کاربران"
        '
        'HelpMenu
        '
        Me.HelpMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UserManualToolStripMenuItem, Me.AboutToolStripMenuItem, Me.ErrorSearchToolStripMenuItem})
        Me.HelpMenu.Name = "HelpMenu"
        Me.HelpMenu.Size = New System.Drawing.Size(44, 20)
        Me.HelpMenu.Text = "کمک"
        '
        'UserManualToolStripMenuItem
        '
        Me.UserManualToolStripMenuItem.Name = "UserManualToolStripMenuItem"
        Me.UserManualToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.UserManualToolStripMenuItem.Text = "راهنمای استفاده از برنامه"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.AboutToolStripMenuItem.Text = "درباره CiBS"
        '
        'ErrorSearchToolStripMenuItem
        '
        Me.ErrorSearchToolStripMenuItem.Name = "ErrorSearchToolStripMenuItem"
        Me.ErrorSearchToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.ErrorSearchToolStripMenuItem.Text = "جستجوی خطا"
        '
        'ssUserInfo
        '
        Me.ssUserInfo.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ssUserInfo.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.lblLogedInUser, Me.ToolStripStatusLabel6, Me.lblUserName, Me.ToolStripStatusLabel4, Me.lblGrantLevel, Me.ToolStripStatusLabel2, Me.lblLogedInBranch, Me.ToolStripStatusLabel3, Me.lblSoftwareVervion, Me.ToolStripStatusLabel9, Me.lblServer, Me.ToolStripStatusLabel7, Me.lblDate, Me.ToolStripStatusLabel8, Me.lblTime, Me.ToolStripStatusLabel10, Me.lblUserNameWindows})
        Me.ssUserInfo.Location = New System.Drawing.Point(0, 535)
        Me.ssUserInfo.Name = "ssUserInfo"
        Me.ssUserInfo.Padding = New System.Windows.Forms.Padding(21, 0, 1, 0)
        Me.ssUserInfo.Size = New System.Drawing.Size(1129, 22)
        Me.ssUserInfo.TabIndex = 17
        Me.ssUserInfo.Text = "اطلاعات کاربر و نرم افزار"
        Me.ssUserInfo.Visible = False
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(54, 17)
        Me.ToolStripStatusLabel1.Text = "کد کاربر:"
        '
        'lblLogedInUser
        '
        Me.lblLogedInUser.ForeColor = System.Drawing.Color.Maroon
        Me.lblLogedInUser.Name = "lblLogedInUser"
        Me.lblLogedInUser.Size = New System.Drawing.Size(18, 17)
        Me.lblLogedInUser.Text = "id"
        '
        'ToolStripStatusLabel6
        '
        Me.ToolStripStatusLabel6.Name = "ToolStripStatusLabel6"
        Me.ToolStripStatusLabel6.Size = New System.Drawing.Size(57, 17)
        Me.ToolStripStatusLabel6.Text = "نام کاربر:"
        '
        'lblUserName
        '
        Me.lblUserName.ForeColor = System.Drawing.Color.Maroon
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(67, 17)
        Me.lblUserName.Text = "UserName"
        '
        'ToolStripStatusLabel4
        '
        Me.ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
        Me.ToolStripStatusLabel4.Size = New System.Drawing.Size(103, 17)
        Me.ToolStripStatusLabel4.Text = "سطح دسترسی:"
        '
        'lblGrantLevel
        '
        Me.lblGrantLevel.ForeColor = System.Drawing.Color.Maroon
        Me.lblGrantLevel.Name = "lblGrantLevel"
        Me.lblGrantLevel.Size = New System.Drawing.Size(34, 17)
        Me.lblGrantLevel.Text = "level"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(61, 17)
        Me.ToolStripStatusLabel2.Text = "کد شعبه:"
        '
        'lblLogedInBranch
        '
        Me.lblLogedInBranch.ForeColor = System.Drawing.Color.Maroon
        Me.lblLogedInBranch.Name = "lblLogedInBranch"
        Me.lblLogedInBranch.Size = New System.Drawing.Size(47, 17)
        Me.lblLogedInBranch.Text = "branch"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(93, 17)
        Me.ToolStripStatusLabel3.Text = "نسخه نرم افزار:"
        '
        'lblSoftwareVervion
        '
        Me.lblSoftwareVervion.ForeColor = System.Drawing.Color.Maroon
        Me.lblSoftwareVervion.Name = "lblSoftwareVervion"
        Me.lblSoftwareVervion.Size = New System.Drawing.Size(49, 17)
        Me.lblSoftwareVervion.Text = "version"
        '
        'ToolStripStatusLabel9
        '
        Me.ToolStripStatusLabel9.Name = "ToolStripStatusLabel9"
        Me.ToolStripStatusLabel9.Size = New System.Drawing.Size(41, 17)
        Me.ToolStripStatusLabel9.Text = "سرور:"
        '
        'lblServer
        '
        Me.lblServer.ForeColor = System.Drawing.Color.Maroon
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(44, 17)
        Me.lblServer.Text = "server"
        '
        'ToolStripStatusLabel7
        '
        Me.ToolStripStatusLabel7.Name = "ToolStripStatusLabel7"
        Me.ToolStripStatusLabel7.Size = New System.Drawing.Size(39, 17)
        Me.ToolStripStatusLabel7.Text = "تاریخ:"
        '
        'lblDate
        '
        Me.lblDate.ForeColor = System.Drawing.Color.Maroon
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(33, 17)
        Me.lblDate.Text = "date"
        '
        'ToolStripStatusLabel8
        '
        Me.ToolStripStatusLabel8.Name = "ToolStripStatusLabel8"
        Me.ToolStripStatusLabel8.Size = New System.Drawing.Size(50, 17)
        Me.ToolStripStatusLabel8.Text = "ساعت:"
        '
        'lblTime
        '
        Me.lblTime.ForeColor = System.Drawing.Color.Maroon
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(33, 17)
        Me.lblTime.Text = "time"
        '
        'ToolStripStatusLabel10
        '
        Me.ToolStripStatusLabel10.Name = "ToolStripStatusLabel10"
        Me.ToolStripStatusLabel10.Size = New System.Drawing.Size(103, 17)
        Me.ToolStripStatusLabel10.Text = "نام کاربری ویندوز:"
        '
        'lblUserNameWindows
        '
        Me.lblUserNameWindows.ForeColor = System.Drawing.Color.Maroon
        Me.lblUserNameWindows.Name = "lblUserNameWindows"
        Me.lblUserNameWindows.Size = New System.Drawing.Size(119, 17)
        Me.lblUserNameWindows.Text = "UserNameWindows"
        '
        'ssAdminMessage
        '
        Me.ssAdminMessage.Dock = System.Windows.Forms.DockStyle.Top
        Me.ssAdminMessage.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ssAdminMessage.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel5, Me.lblAdminMessage})
        Me.ssAdminMessage.Location = New System.Drawing.Point(0, 0)
        Me.ssAdminMessage.Name = "ssAdminMessage"
        Me.ssAdminMessage.Padding = New System.Windows.Forms.Padding(21, 0, 1, 0)
        Me.ssAdminMessage.Size = New System.Drawing.Size(1129, 22)
        Me.ssAdminMessage.TabIndex = 19
        Me.ssAdminMessage.Text = "پیام مدیر سیستم"
        Me.ssAdminMessage.Visible = False
        '
        'ToolStripStatusLabel5
        '
        Me.ToolStripStatusLabel5.Name = "ToolStripStatusLabel5"
        Me.ToolStripStatusLabel5.Size = New System.Drawing.Size(111, 17)
        Me.ToolStripStatusLabel5.Text = "پیام مدیر سیستم:"
        '
        'lblAdminMessage
        '
        Me.lblAdminMessage.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdminMessage.ForeColor = System.Drawing.Color.Navy
        Me.lblAdminMessage.Name = "lblAdminMessage"
        Me.lblAdminMessage.Size = New System.Drawing.Size(93, 17)
        Me.lblAdminMessage.Text = "AdminMessage"
        '
        'timerLogIn
        '
        Me.timerLogIn.Interval = 1000
        '
        'timerSLS
        '
        Me.timerSLS.Interval = 1000
        '
        'CiBS_Parent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1129, 557)
        Me.Controls.Add(Me.MenuStrip)
        Me.Controls.Add(Me.ssAdminMessage)
        Me.Controls.Add(Me.ssUserInfo)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip
        Me.Name = "CiBS_Parent"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "برنامه صدور کارت آنی در شعبه - نسخه"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.ssUserInfo.ResumeLayout(False)
        Me.ssUserInfo.PerformLayout()
        Me.ssAdminMessage.ResumeLayout(False)
        Me.ssAdminMessage.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents HelpMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportUsers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuChangePassword As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCard As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCardPrint As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPrinter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUserManagement As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPrintSetting As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConsumablesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RequestConsumablesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AcceptConsumablesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuConsumablesStack As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InformUnusableCardsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ssUserInfo As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblLogedInUser As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblLogedInBranch As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel4 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblGrantLevel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblSoftwareVervion As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PrinterManagementToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ssAdminMessage As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel5 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblAdminMessage As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents timerLogIn As System.Windows.Forms.Timer
    Friend WithEvents ToolStripStatusLabel6 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblUserName As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel7 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblDate As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel8 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblTime As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mnuAdmin As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintTemplateSettingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AdminMessageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserManualToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripStatusLabel9 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblServer As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents LoadCardRecordsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LogOutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents timerSLS As System.Windows.Forms.Timer
    Friend WithEvents ErrorSearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportUsersToolStrip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents تستیToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportCustomer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReprintIranCard As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripStatusLabel10 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblUserNameWindows As System.Windows.Forms.ToolStripStatusLabel

End Class
