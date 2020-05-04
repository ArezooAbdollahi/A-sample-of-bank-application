Imports System.Windows.Forms
Imports Microsoft.Office.Interop.Excel
Imports System.Net.Mime.MediaTypeNames
Imports System.Runtime.InteropServices
Imports Microsoft.Win32
Imports System.Drawing.Text

Public Class CiBS_Parent

    Private Const HWND_BROADCAST = &HFFFF&
    Private Const WM_FONTCHANGE As Long = &H1D

    <DllImport("gdi32.dll")> _
    Public Shared Function AddFontResource(ByVal lpszFilename As String) As Integer
    End Function
    <DllImport("user32.dll")> _
    Public Shared Function SendMessage(ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub CiBS_Parent_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'If Not String.IsNullOrEmpty(CurrentUser) Then
        If (lblUserName.Text.Trim = "" Or lblUserName.Text.Trim = "UserName" Or lblUserNameWindows.Text.Trim = "" Or lblUserNameWindows.Text.Trim = "UserNameWindows") Then
        Else
            ' If Not InsertLog(CurrentBranchCode & "|" & lblUserName.Text.Trim & "|" & "LogOut" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "User Logged Out Successfully" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز FormClosing", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Try
                CiBS_WS.SetIsLogedIn(Encrypt(lblUserName.Text.Trim & "|" & lblUserNameWindows.Text.Trim & "|" & Now.TimeOfDay.TotalSeconds))
            Catch
                MessageBox.Show("خروج کاربر به درستی ثبت نگردیده است")
            End Try
        End If

        'Else
        'MessageBox.Show("خروج کاربر به درستی ثبت نگردیده است")
        'End If
    End Sub

    Private Sub CiBS_Parent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InstallFont("AdobeArabic-Bold.otf")
        InstallFont("DINPro-Medium.otf")

        Me.Text = Me.Text & " " & My.Application.Info.Version.ToString
        LogIn.MdiParent = Me
        LogIn.Show()
    End Sub

    Private Function GetFileName(ByRef fullPath As String) As String
        Return fullPath.Substring(fullPath.LastIndexOf("\") + 1)
    End Function
    Private Function GetFontName(ByRef fileName As String)
        Return fileName.Substring(0, fileName.IndexOf("."))
    End Function

    Sub InstallFont(ByVal Font_File As String)
        Try
            Dim FONTS_FOLDER = System.Environment.GetFolderPath(Environment.SpecialFolder.System).Replace("system32", "fonts") & "\"
            Dim regKey As RegistryKey = Registry.LocalMachine.OpenSubKey("Software", True).OpenSubKey("Microsoft", True).OpenSubKey("Windows NT", True).OpenSubKey("CurrentVersion", True).OpenSubKey("Fonts", True)
            Dim fileName As String
            Dim tempFonts As New PrivateFontCollection
            Dim fontName As String
            fileName = GetFileName(Font_File)
            tempFonts.AddFontFile(Font_File)
            fontName = tempFonts.Families(0).Name
            If Not System.IO.File.Exists(FONTS_FOLDER & fileName) Then        'copy the font into the windows font directory      
                System.IO.File.Copy(Font_File, FONTS_FOLDER & fileName)        'add it to the system font table  
                AddFontResource(FONTS_FOLDER & fileName) 'write the change in the registry
                regKey.SetValue(fontName, fileName)        'alert all top-level programs about the change 
                SendMessage(HWND_BROADCAST, WM_FONTCHANGE, 0, 0)
            End If
            tempFonts.Dispose()
        Catch ex As Exception
            InsertLocalLog("Error in installing font " & Font_File)
        End Try
    End Sub


    ' ''Private Sub MDIControlPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
    ' ''    'GET THE IMAGE (IT IS COMPILED WITHIN THE EXE AS AN EMBEDDED RESOURCE)
    ' ''    'Dim MyImage As Image = Image.FromStream(Me.GetType().Assembly.GetManifestResourceStream("Resources.bg.png"))
    ' ''    Dim MyImage As Image = Image.FromFile("bg.png")
    ' ''    'CALCULATE THE TOP AND LEFT POSITION (CENTER) TO DRAW THE IMAGE
    ' ''    Dim MyTop As Integer = 0
    ' ''    Dim MyLeft As Integer = 0
    ' ''    If MyImage.Height < Me.Height Then
    ' ''        MyTop = Convert.ToInt32((Me.ClientSize.Height - MyImage.Height) / 2)
    ' ''    End If
    ' ''    If MyImage.Width < Me.Width Then
    ' ''        MyLeft = Convert.ToInt32((Me.ClientSize.Width - MyImage.Width) / 2)
    ' ''    End If
    ' ''    'MyLeft = 355
    ' ''    'MyTop = 188
    ' ''    'DRAW THE IMAGE ONTO THE MDI BACKGROUND
    ' ''    ' e.Graphics.DrawImage(Image.FromStream(Me.GetType().Assembly.GetManifestResourceStream("Resources.bg.png")), MyLeft, MyTop)
    ' ''    e.Graphics.DrawImage(Image.FromFile("bg.png"), 2, 2)
    ' ''    e.Graphics.DrawImage(Image.FromFile("bg.png"), MyLeft, 2)
    ' ''    e.Graphics.DrawImage(Image.FromFile("bg.png"), 706, 2)
    ' ''    e.Graphics.DrawImage(Image.FromFile("bg.png"), 1056, 2)

    ' ''    e.Graphics.DrawImage(Image.FromFile("bg.png"), 2, MyTop)
    ' ''    e.Graphics.DrawImage(Image.FromFile("bg.png"), MyLeft, MyTop)
    ' ''    e.Graphics.DrawImage(Image.FromFile("bg.png"), 706, MyTop)
    ' ''    e.Graphics.DrawImage(Image.FromFile("bg.png"), 1056, MyTop)

    ' ''    e.Graphics.DrawImage(Image.FromFile("bg.png"), 2, 370)
    ' ''    e.Graphics.DrawImage(Image.FromFile("bg.png"), MyLeft, 370)
    ' ''    e.Graphics.DrawImage(Image.FromFile("bg.png"), 706, 370)
    ' ''    e.Graphics.DrawImage(Image.FromFile("bg.png"), 1056, 370)

    ' ''    e.Graphics.DrawImage(Image.FromFile("bg.png"), 2, 560)
    ' ''    e.Graphics.DrawImage(Image.FromFile("bg.png"), MyLeft, 560)
    ' ''    e.Graphics.DrawImage(Image.FromFile("bg.png"), 706, 560)
    ' ''    e.Graphics.DrawImage(Image.FromFile("bg.png"), 1056, 560)

    ' ''End Sub

    'WHEN THE FORM IS RESIZED, INVALIDATE IT, SO IT CAN BE REDRAWN

    Private Sub MDIControlResize(ByVal sender As Object, ByVal e As System.EventArgs)
        CType(sender, MdiClient).Invalidate()
    End Sub


    Private Sub mnuChangePassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuChangePassword.Click
        ChangePassword.MdiParent = Me
        ChangePassword.Show()
    End Sub

    Private Sub mnuCardPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCardPrint.Click
        CardPrint.MdiParent = Me
        CardPrint.Show()
    End Sub

    Private Sub mnuUserManagement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUserManagement.Click
        UserManagement.MdiParent = Me
        UserManagement.Show()
    End Sub

    Private Sub RequestConsumablesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestConsumablesToolStripMenuItem.Click
        RequestConsumables.MdiParent = Me
        RequestConsumables.Show()
    End Sub

    Private Sub AcceptConsumablesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AcceptConsumablesToolStripMenuItem.Click
        AcceptConsumablesRequest.MdiParent = Me
        AcceptConsumablesRequest.Show()
    End Sub

    Private Sub ReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportToolStripMenuItem.Click
        Reports.MdiParent = Me
        Reports.Show()
    End Sub

    Private Sub mnuConsumablesStack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuConsumablesStack.Click
        ConsumablesStack.MdiParent = Me
        ConsumablesStack.Show()
    End Sub

    Private Sub InformUnusableCardsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InformUnusableCardsToolStripMenuItem.Click
        InformUnUsableCard.MdiParent = Me
        InformUnUsableCard.Show()
    End Sub

    Private Sub PrinterManagementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrinterManagementToolStripMenuItem.Click
        PrinterMaintenance.MdiParent = Me
        PrinterMaintenance.Show()
    End Sub

    Private Sub timerLogIn_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerLogIn.Tick
        lblTime.Text = Now.TimeOfDay.ToString.Substring(0, 8)
        lblDate.Text = DT.DateToPersian(Now.Date).ShortDate.Trim
        If GetLastUserInput.IdleTimeInTicks() > 1800000 Then
            logout()
        End If
    End Sub

    Private Sub PrintTemplateSettingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintTemplateSettingToolStripMenuItem.Click
        PrintSetting.MdiParent = Me
        PrintSetting.Show()
    End Sub

    Private Sub AdminMessageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdminMessageToolStripMenuItem.Click
        AdminMessages.MdiParent = Me
        AdminMessages.Show()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        'About.MdiParent = Me
        'About.Show()
    End Sub

    Private Sub UserManualToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserManualToolStripMenuItem.Click
        System.Diagnostics.Process.Start("http://cib.ab.net/UserManual.pdf")
    End Sub

    Private Sub LoadCardRecordsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadCardRecordsToolStripMenuItem.Click
        Loader.MdiParent = Me
        Loader.Show()
    End Sub

    Private Sub LogOutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOutToolStripMenuItem.Click
        logout()
    End Sub

    Sub logout()
        timerLogIn.Enabled = False
        Dim frm As Form
        For Each frm In MdiChildren
            frm.Dispose()
        Next
        mnuPrinter.Visible = False
        mnuCard.Visible = False
        InformUnusableCardsToolStripMenuItem.Visible = False
        mnuAdmin.Visible = False
        mnuUserManagement.Visible = False
        AcceptConsumablesToolStripMenuItem.Visible = False
        mnuConsumablesStack.Visible = False
        If Not String.IsNullOrEmpty(lblLogedInUser.Text.Trim) Then
            If Not InsertLog(CurrentBranchCode & "|" & lblLogedInUser.Text.Trim & "|" & "LogOUt" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "User Logged Out Successfully" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            For i As Integer = 1 To 20
                ' If Decrypt(CiBS_WS.SetIsLogedIn(Encrypt(CurrentUser & "|" & Now.TimeOfDay.TotalSeconds))) = 1 Then
                If Decrypt(CiBS_WS.SetIsLogedIn(Encrypt(lblUserName.Text.Trim & "|" & lblUserNameWindows.Text.Trim & "|" & Now.TimeOfDay.TotalSeconds))) = 1 Then
                    Exit For
                End If
            Next

        End If
        ssUserInfo.Visible = False
        ssAdminMessage.Visible = False
        MenuStrip.Visible = False
        timerLogIn.Enabled = False
        Dim tmpLogin As New LogIn
        tmpLogin.ShowDialog()
    End Sub

    Private Sub LastBankInfoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        System.Diagnostics.Process.Start("http://cib.tb.net/BankInfo.pdf")
    End Sub

    Private Sub timerSLS_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerSLS.Tick
        Dim strReaderName As String = "DIGENT CB1000 USB Reader 0"
        Dim s As String = SCAPI.test(strReaderName, "B040000104")
        If s.Contains("Some general Error Occured") Then
            timerSLS.Enabled = False
            If Not String.IsNullOrEmpty(lblLogedInUser.Text.Trim) Then
                CiBS_WS.InsertLog(Encrypt(CurrentBranchCode & "|" & lblLogedInUser.Text.Trim & "|" & "SLS" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "User has taken out the SLS card" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds))
                ' CiBS_WS.SetIsLogedIn(Encrypt(CurrentUser & "|" & Now.TimeOfDay.TotalSeconds))
                CiBS_WS.SetIsLogedIn(Encrypt(lblUserName.Text.Trim & "|" & lblUserNameWindows.Text.Trim & "|" & Now.TimeOfDay.TotalSeconds))
            End If
            Dim frm As Form
            For Each frm In MdiChildren
                frm.Dispose()
            Next
            InformUnusableCardsToolStripMenuItem.Visible = False
            mnuAdmin.Visible = False
            mnuUserManagement.Visible = False
            AcceptConsumablesToolStripMenuItem.Visible = False
            mnuConsumablesStack.Visible = False
            Dim tmpLogin As New LogIn
            tmpLogin.ShowDialog()
        End If
    End Sub

    Private Sub ErrorSearchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ErrorSearchToolStripMenuItem.Click
        Guidance.MdiParent = Me
        Guidance.Show()
    End Sub

    Private Sub تستیToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles تستیToolStripMenuItem.Click
        InsertCustData.MdiParent = Me
        InsertCustData.Show()
    End Sub

    Private Sub ReportCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportCustomer.Click
        ReportsCustomer.MdiParent = Me
        ReportsCustomer.Show()
    End Sub

    Private Sub mnuReprintIranCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReprintIranCard.Click
        CardPrintRe.MdiParent = Me
        CardPrintRe.Show()
    End Sub

    Private Sub lblDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblDate.Click

    End Sub

    Private Sub ReportUsersToolStrip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportUsersToolStrip.Click

    End Sub
End Class
