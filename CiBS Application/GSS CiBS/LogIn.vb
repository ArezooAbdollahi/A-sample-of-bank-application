Imports System.Data.OleDb
Imports CIBS.SCLib
Imports System.Text
Imports System.Data
Imports System.DirectoryServices
Imports System.Globalization


Public Class LogIn
    Public PosIdLogin As String = 0
    Public usernameMain As String
    Dim newDateTime As Date
    Dim PersianCalendar As PersianCalendar
    Dim username As String

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        End
    End Sub

    Private Function ValidateActiveDirectoryLogin(ByVal Domain As String, ByVal Username As String, ByVal Password As String) As Boolean

        Dim Success As Boolean = False

        Dim Entry As New System.DirectoryServices.DirectoryEntry("LDAP://" & Domain, Username, Password)

        Dim Searcher As New System.DirectoryServices.DirectorySearcher(Entry)

        Searcher.SearchScope = DirectoryServices.SearchScope.OneLevel

        Try

            Dim Results As System.DirectoryServices.SearchResult = Searcher.FindOne
            Success = Not (Results Is Nothing)

        Catch

            Success = False

        End Try

        Return Success

    End Function

    'Private Function GetActiveDirUserDetails(ByVal userName As String) As String
    '    Dim dirEntry As System.DirectoryServices.DirectoryEntry
    '    Dim dirSearcher As System.DirectoryServices.DirectorySearcher
    '    Dim domainName As String = "GSSintdc" 'System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName
    '    Try
    '        dirEntry = New System.DirectoryServices.DirectoryEntry("LDAP://" & domainName)
    '        dirSearcher = New System.DirectoryServices.DirectorySearcher(dirEntry)
    '        dirSearcher.Filter = "(userName=" & userName & ")"

    '        dirSearcher.PropertiesToLoad.Add("GivenName")
    '        'Users e-mail address
    '        dirSearcher.PropertiesToLoad.Add("sn")
    '        'Users last name
    '        Dim sr As SearchResult = dirSearcher.FindOne()
    '        If sr Is Nothing Then 'return false if user isn't found 
    '            Return False
    '        End If
    '        Dim de As System.DirectoryServices.DirectoryEntry = sr.GetDirectoryEntry()
    '        Dim userFirstLastName = de.Properties("sn").Value.ToString() + ", " + de.Properties("GivenName").Value.ToString()
    '        Return userFirstLastName
    '    Catch ex As Exception ' return false if exception occurs 
    '        Return ex.Message
    '    End Try
    'End Function


    Public Shared Function Shamsi2Miladi(ByVal Date1 As DateTime) As String
        Dim gc As GregorianCalendar = New GregorianCalendar(GregorianCalendarTypes.USEnglish)
        Return (gc.GetDayOfMonth(Date1).ToString + ("/" _
                    + (gc.GetMonth(Date1).ToString + ("/" + gc.GetYear(Date1).ToString))))
    End Function

    Public Function GetAllUsers(ByVal ldapServerName As String) As Hashtable
        'To retrieve list of all  LDAP users

        'This function returns HashTable





        Dim Domain As String = "GSSintdc.local"
        Dim Username As String = "m.sarmadi"
        Dim Password As String = "12345"


        Dim sServerName As String = "mail"
        Dim oRoot As DirectoryEntry = New System.DirectoryServices.DirectoryEntry("LDAP://" & Domain, Username, Password)   '= New DirectoryEntry("LDAP://" & ldapServerName & "/ou=People,dc=m.sarmadi,dc=12345")

        Dim oSearcher As DirectorySearcher = New DirectorySearcher(oRoot)
        Dim oResults As SearchResultCollection
        Dim oResult As SearchResult
        Dim RetArray As New Hashtable()

        Try

            oSearcher.PropertiesToLoad.Add("uid")
            oSearcher.PropertiesToLoad.Add("givenname")
            oSearcher.PropertiesToLoad.Add("cn")
            oResults = oSearcher.FindAll

            For Each oResult In oResults

                If Not oResult.GetDirectoryEntry().Properties("cn").Value = "" Then
                    RetArray.Add(oResult.GetDirectoryEntry().Properties("uid").Value, _
                      oResult.GetDirectoryEntry().Properties("cn").Value)
                End If

            Next

        Catch e As Exception

            'MsgBox("Error is " & e.Message)
            Return RetArray

        End Try

        Return RetArray

    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click


        'MessageBox.Show("0")
        'Dim ADEntry As New DirectoryServices.DirectoryEntry("LDAP://" & "GSSintdc.local")
        'Dim domain As String
        'MessageBox.Show("1")
        'username = txtUserName.Text.Trim ' "m.sarmadi"   ' 
        'MessageBox.Show("2 username:" & username)
        'Dim password As String = txtPassword.Text '"123456"  '


        ' ''////// Faal Kon Masiii
        'domain = "ab"  '"GSSintdc.local"
        'Try
        '    If Not ValidateActiveDirectoryLogin(domain, username, password) Then ' Me.txtUserName.Text, Me.txtPassword.Text) Then
        '        MessageBox.Show("نام کاربری ویندوز و یا پسورد اشتباه می باشد")
        '        Exit Sub
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show("خطاهنگام دریافت اطلاعات از اکتیودایرکتوری" & "ex.Message:" & ex.Message)
        '    Exit Sub
        'End Try
        ' ''////////////////////
        'MessageBox.Show("3")
        'CiBS_WS.Url = GetSetting("GSSCiBS", "CardPrint", "WebServiceURL")
        'MessageBox.Show("4 CiBS_WS.Url:" & CiBS_WS.Url)

        'Dim tmpusernameMain As String
        'Try
        '    MessageBox.Show("5")
        '    tmpusernameMain = CiBS_WS.GetUserNameFrWinUser(Encrypt(username))
        '    MessageBox.Show("6 tmpusernameMain:" & tmpusernameMain)
        'Catch ex As Exception
        '    InsertLocalLog("GetusernameMain- " & ex.Message)
        '    'MessageBox.Show("2")
        '    MessageBox.Show("خطا هنگام دریافت نام کاربری سیستم" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try
        'usernameMain = Decrypt(tmpusernameMain)
        'MessageBox.Show(" کد پرسنلی شما   " & usernameMain & " می باشد. ")
        'usernameMain = usernameMain.Trim

        'If usernameMain = "" Or usernameMain = " " Or usernameMain.Contains("error") Then
        '    MessageBox.Show("نام کاربری درست یافت نشد لطفا با مرکز تماس حاصل فرمایید.")
        '    Exit Sub
        'Else
        '    CurrentUser = usernameMain ' txtUserName.Text
        '    CurrentUserNEW = usernameMain
        '    MessageBox.Show(" usernameMain:" & usernameMain)
        '    CiBS_Parent.lblLogedInUser.Text = usernameMain
        'End If
        'MessageBox.Show("CiBS_Parent.lblLogedInUser.Text:" & CiBS_Parent.lblLogedInUser.Text)
        'CiBS_Parent.lblLogedInUser.Text = usernameMain
        'MessageBox.Show("CiBS_Parent.lblLogedInUser.Text" & CiBS_Parent.lblLogedInUser.Text)
        UserLogin()

    End Sub

    Sub UserLogin()

        Dim ADEntry As New DirectoryServices.DirectoryEntry("LDAP://" & "ab")
        Dim domain As String = "ab"   ' "GSSintdc.local"
        username = txtUserName.Text.Trim ' "m.sarmadi"   ' 
        Dim password As String = txtPassword.Text '"123456"  '

        CiBS_WS.Url = GetSetting("GSSCiBS", "CardPrint", "WebServiceURL")

        ''////// ValidActiveDirectory To WebService 
        Dim DecrypttmpResultVAD As String
        Dim tmpResultVAD As String
        Try
            tmpResultVAD = CiBS_WS.ValidateActiveDirectoryLoginWeb(domain & "|" & username & "|" & password)
            DecrypttmpResultVAD = tmpResultVAD
            ' DecrypttmpResultVAD = Decrypt(tmpResultVAD)
        Catch ex As Exception
            InsertLocalLog("ValidateActiveDirectoryLoginWeb- " & ex.Message)
            MessageBox.Show("مشکلی درشبکه و یابرقراری ارتباط سرور و دیتابیس رخ داده است-(ValidateActiveDirectory) " & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'خطاهنگام دریافت اطلاعات از اکتیودایرکتوری
            Exit Sub
        End Try

        If DecrypttmpResultVAD = "True" Then

        ElseIf DecrypttmpResultVAD.Contains("Invalid") Then
            If DecrypttmpResultVAD = "InvalidStart" Then
                MessageBox.Show("بروز مشکل در صحت سنجی نام کاربری اکتیودایرکتوری InvalidStart")
                Exit Sub
            Else
                MessageBox.Show("1- بروز مشکل در صحت سنجی نام کاربری اکتیودایرکتوری")
                Exit Sub
            End If
        ElseIf DecrypttmpResultVAD.Contains("Error") Then
            MessageBox.Show("2-بروز مشکل در صحت سنجی نام کاربری اکتیودایرکتوری" & "-ex.Message:" & DecrypttmpResultVAD)
                Exit Sub
        Else
            MessageBox.Show("3-بروز مشکل در صحت سنجی نام کاربری اکتیودایرکتوری:" & DecrypttmpResultVAD)
                Exit Sub
        End If

        '/////////////////////////////////////////////

        'domain = "GSSintdc.local"  '"ab"
        'Try
        '    If Not ValidateActiveDirectoryLogin(domain, username, password) Then ' Me.txtUserName.Text, Me.txtPassword.Text) Then
        '        MessageBox.Show("نام کاربری ویندوز و یا پسورد اشتباه می باشد")
        '        Exit Sub
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show("خطاهنگام دریافت اطلاعات از اکتیودایرکتوری" & "ex.Message:" & ex.Message)
        '    Exit Sub
        'End Try

        '////////////////////

        Dim tmpusernameMain As String
        Try
            '   MessageBox.Show("5")
            tmpusernameMain = CiBS_WS.GetUserNameFrWinUser(Encrypt(username))
            '  MessageBox.Show("6 tmpusernameMain:" & tmpusernameMain)
        Catch ex As Exception
            InsertLocalLog("GetusernameMain- " & ex.Message)
            MessageBox.Show(" (GetusernameMain) مشکلی درشبکه و یابرقراری ارتباط سرور و دیتابیس رخ داده است " & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' هنگام دریافت نام کاربری سیستم
            Exit Sub
        End Try
        usernameMain = Decrypt(tmpusernameMain)
        'MessageBox.Show(" کد پرسنلی شما   " & usernameMain & " می باشد. ")
        usernameMain = usernameMain.Trim

        If usernameMain = "" Or usernameMain = " " Or usernameMain.Contains("error") Or usernameMain.Contains("Invalid") Then
            MessageBox.Show("نام کاربری شما در سیستم تعریف نشده است")
            Exit Sub
        Else
            CurrentUser = usernameMain ' txtUserName.Text
            CurrentUserNEW = usernameMain
            '   MessageBox.Show(" usernameMain:" & usernameMain)
            CiBS_Parent.lblLogedInUser.Text = usernameMain
        End If

        ' CiBS_Parent.lblLogedInUser.Text = usernameMain

        '/////////////////////////////////////////////////////////

        CiBS_Parent.lblUserNameWindows.Text = txtUserName.Text.Trim
        CiBS_WS.Url = GetSetting("GSSCiBS", "CardPrint", "WebServiceURL")
        If txtUserName.Text.Length < 3 And txtPassword.Text.Length = 0 And txtBrCode.Text.Length <= 3 Then
            MessageBox.Show("لطفاً مقادیر ورودی را بررسی نمایید", "خطا")
            Exit Sub
        End If

        Try
            ' MessageBox.Show("11")
            tmpWSResult = (Decrypt(CiBS_WS.CheckUser(Encrypt(txtUserName.Text.Trim & "|" & GenerateHash(usernameMain & txtPassword.Text) & "|" & txtBrCode.Text & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99"))))
            'MessageBox.Show("12")
        Catch ex As Exception
            InsertLocalLog("CheckUser- " & ex.Message)
            MessageBox.Show("خطا هنگام بررسی هویت کاربر" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        'MessageBox.Show("13 tmpWSResult:" & tmpWSResult)
        ws_fb = tmpWSResult.Split("|")
        If String.Compare(ws_fb(0), "Invalid") = 0 Then

            ' update failed login count
            tmpWSResult = (Decrypt(CiBS_WS.UpdateFailedLogin(Encrypt(usernameMain & "|False" & "|" & Now.TimeOfDay.TotalSeconds))))

            MessageBox.Show("شناسه و رمز عبور اشتباه می باشد", "خطا")
            txtUserName.Text = ""
            txtPassword.Text = ""
            txtBrCode.Text = ""
            txtUserName.Enabled = True
            txtBrCode.Enabled = True
            txtUserName.Focus()
            Exit Sub
        End If
        If ws_fb(0) = "Timeout" Then
            MessageBox.Show("Timeout", "خطا")
            Exit Sub
        End If

        If String.Compare(ws_fb(0), "9999") = 0 Then
            MessageBox.Show("شناسه شما مسدود می باشد", "خطا")
            Exit Sub
        End If
        If String.Compare(ws_fb(0), "8888") = 0 Then
            MessageBox.Show("این کاربر قبلاً وارد سیستم شده است", "خطا")
            Exit Sub
        End If
        If String.Compare(ws_fb(0), "0") = 0 Then
            MessageBox.Show("شناسه شما غیر فعال می باشد", "خطا")
            Exit Sub
        End If
        If String.Compare(ws_fb(0), "1") = 0 Then   ' valid Login

            If String.Compare(ws_fb(5), "Timeout-Diffrent") = 0 Then
                MessageBox.Show("،تاخیر در ارسال و پردازش درخواست" & vbCrLf & "ساعت دستگاه شما با ساعت سرور تطابق ندارد TimeOut" & vbCrLf & " ساعت سرور " & ws_fb(6), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
                ' Exit Sub
            End If

            ' insert LOG
            If Not InsertLog(txtBrCode.Text & "|" & usernameMain & "|" & "LogIn" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "User Logged in Successfully" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)

            ' update failed login count
            tmpWSResult = (Decrypt(CiBS_WS.UpdateFailedLogin(Encrypt(usernameMain & "|True" & "|" & Now.TimeOfDay.TotalSeconds))))

            Me.Visible = False
            CiBS_Parent.ssUserInfo.Visible = True
            CiBS_Parent.ssAdminMessage.Visible = True
            CiBS_Parent.MenuStrip.Visible = True
            CiBS_Parent.timerLogIn.Enabled = True
            If txtUserName.Enabled = False Then CiBS_Parent.timerSLS.Enabled = True ' Login with SLS

            PosIdLogin = ws_fb(4)
            If String.Compare(ws_fb(1), "2") = 0 Then  'Branch Head
                CiBS_Parent.AcceptConsumablesToolStripMenuItem.Visible = True
                CiBS_Parent.mnuUserManagement.Visible = True
                CiBS_Parent.InformUnusableCardsToolStripMenuItem.Visible = True
                CiBS_Parent.mnuCard.Visible = True
                CiBS_Parent.mnuPrinter.Visible = True
                CiBS_Parent.RequestConsumablesToolStripMenuItem.Visible = True
            End If
            If String.Compare(ws_fb(1), "3") = 0 Then  ' Admin
                CiBS_Parent.InformUnusableCardsToolStripMenuItem.Visible = True
                CiBS_Parent.mnuAdmin.Visible = True
                CiBS_Parent.mnuUserManagement.Visible = True
                CiBS_Parent.AcceptConsumablesToolStripMenuItem.Visible = True
                CiBS_Parent.mnuConsumablesStack.Visible = True
                CiBS_Parent.mnuCard.Visible = True
                CiBS_Parent.mnuPrinter.Visible = True
                CiBS_Parent.RequestConsumablesToolStripMenuItem.Visible = True
            End If
            If String.Compare(ws_fb(1), "4") = 0 Then  'Support User
                CiBS_Parent.AcceptConsumablesToolStripMenuItem.Visible = True
                CiBS_Parent.mnuConsumablesStack.Visible = True
                CiBS_Parent.mnuUserManagement.Visible = True
                CiBS_Parent.RequestConsumablesToolStripMenuItem.Visible = True
                CiBS_Parent.mnuCard.Visible = False
                CiBS_Parent.mnuPrinter.Visible = False
            End If
            If String.Compare(ws_fb(1), "1") = 0 Then  'Branch User
                CiBS_Parent.AcceptConsumablesToolStripMenuItem.Visible = True
                CiBS_Parent.mnuCard.Visible = True
                CiBS_Parent.mnuPrinter.Visible = True
                CiBS_Parent.RequestConsumablesToolStripMenuItem.Visible = True
            End If
            CurrentBranchCode = txtBrCode.Text

            UserGrantLevel = ws_fb(1)


            ' CiBS_Parent.lblLogedInUser.Text = usernameMain 'txtUserName.Text
            'CiBS_Parent.lblLogedInUser.Text = usernameMain
            CiBS_Parent.lblLogedInBranch.Text = txtBrCode.Text
            CiBS_Parent.lblSoftwareVervion.Text = My.Application.Info.Version.ToString
            If CiBS_WS.Url.ToString.ToLower.Contains("test") Then
                CiBS_Parent.lblServer.Text = "تستی"
            Else
                CiBS_Parent.lblServer.Text = "عملیاتی"
            End If

            PosSerial = ws_fb(4)
            CiBS_Parent.lblUserName.Text = ws_fb(3).Trim
            CiBS_Parent.lblAdminMessage.Text = ws_fb(2).Trim
            Select Case ws_fb(1)
                Case 1 : CiBS_Parent.lblGrantLevel.Text = "کاربر شعبه"
                Case 2 : CiBS_Parent.lblGrantLevel.Text = "کاربر ارشد شعبه"
                Case 3 : CiBS_Parent.lblGrantLevel.Text = "مدیر سیستم"
                Case 4 : CiBS_Parent.lblGrantLevel.Text = "کاربر پشتیبانی"
            End Select
            CiBS_Parent.lblDate.Text = DT.DateToPersian(Now.Date).ShortDate.Trim
            CiBS_Parent.lblTime.Text = Now.TimeOfDay.ToString.Substring(0, 8)
        End If
    End Sub

    Private Sub txtBrCode_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBrCode.KeyUp
        If e.KeyCode = Keys.Enter And txtBrCode.Text.Length > 0 Then
            UserLogin()
        End If
    End Sub
    ':8200

    Private Sub LogIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''////////// ActiveDirectory
        Dim CurrentUserName As String = Environment.UserName
        Dim DomainName = Environment.UserDomainName
        '/////////////// 
        Dim myHost As String = System.Net.Dns.GetHostName()
        ClientIP = System.Net.Dns.GetHostByName(myHost).AddressList(0).ToString()
        Me.Height = 255 ' 204 ' 180
        '   SaveSetting("GSSCiBS", "CardPrint", "WebServiceURL", "http://cib_ws.ab.net:8989/cibs.asmx")
        If String.IsNullOrEmpty(GetSetting("GSSCiBS", "CardPrint", "WebServiceURL")) Then
            SaveSetting("GSSCiBS", "CardPrint", "WebServiceURL", "http://cib_ws.ab.net:8989/cibs.asmx")
        End If
    End Sub

    'Private Function ValidateActiveDirectoryLogin(ByVal Domain As String, ByVal Username As String, ByVal Password As String) As Boolean

    '    Dim Success As Boolean = False
    '    Dim Entry As New System.DirectoryServices.DirectoryEntry("LDAP://" & Domain, Username, Password)
    '    Dim Searcher As New System.DirectoryServices.DirectorySearcher(Entry)

    '    Searcher.SearchScope = DirectoryServices.SearchScope.OneLevel

    '    Try
    '        Dim Results As System.DirectoryServices.SearchResult = Searcher.FindOne
    '        Success = Not (Results Is Nothing)
    '    Catch
    '        Success = False
    '    End Try

    '    Return (Success)

    'End Function

    'Public Function ValidateActiveDirectoryLogin(ByVal domainName As String, ByVal userName As String, ByVal userPassword As String) As Boolean
    '    Dim groupName As String
    '    Dim isValidated As Boolean = False

    '    Try

    '        Dim ldapPath As String = "LDAP://patten.local"
    '        Dim dirEntry As New DirectoryServices.DirectoryEntry(ldapPath, userName, userPassword, DirectoryServices.AuthenticationTypes.Secure)
    '        Dim dirSearcher As New DirectoryServices.DirectorySearcher(dirEntry)

    '        dirSearcher.Filter = "(userPrincipalName=" & userName & ")"
    '        dirSearcher.PropertiesToLoad.Add("memberOf")
    '        Dim result As DirectoryServices.SearchResult = dirSearcher.FindOne()

    '        If Not result Is Nothing Then

    '            If groupName.Length = 0 Then
    '                isValidated = True
    '            Else
    '                Dim groupCount As Integer = result.Properties("Fiserv Processing - MIS").Count
    '                Dim isInGroup As Boolean = False

    '                For index As Integer = 0 To groupCount - 1
    '                    Dim groupDN As String = result.Properties("Fiserv Processing - MIS").Item(index)

    '                    Dim equalsIndex As Integer = groupDN.IndexOf("=")
    '                    Dim commaIndex As Integer = groupDN.IndexOf(",")

    '                    Dim group As String = groupDN.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1).ToLower
    '                    If group.Equals(groupName.ToLower) Then
    '                        isInGroup = True
    '                        Exit For
    '                    End If
    '                Next index

    '                isValidated = isInGroup
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try

    '    Return isValidated

    'End Function


    Private Sub ckChangeWebService_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkChangeWebService.CheckedChanged
        If chkChangeWebService.Checked Then
            Me.Height = 353 ' 309 ' 290
            Try
                txtCurrentwebAddr.Text = GetSetting("GSSCiBS", "CardPrint", "WebServiceURL")
            Catch ex As Exception
                txtCurrentwebAddr.Text = ""
            End Try
        Else
            Me.Height = 255 ' 203 ' 180
        End If

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            SaveSetting("GSSCiBS", "CardPrint", "WebServiceURL", txtCurrentwebAddr.Text)
            MessageBox.Show("تغییرات با موفقیت ذخیره گردید")
        Catch ex As Exception
            MessageBox.Show("خطا هنگام ذخیره سازی")
        End Try

    End Sub

    Function Scan_CB1000F() As String

        Dim m_Image(70 * 80) As Byte
        Dim res As Int16 = izzix.izzixSCARDFIM.izxSCFIM_IsAvailableDevice(0)
        If res <> izzix.izzixSCARDFIM.FPAPIERR_OK Then
            Return ReturnErrDesc(res)
        End If

        res = izzix.izzixSCARDFIM.izxSCFIM_CapChkImage(0, 20)
        If res <> izzix.izzixSCARDFIM.FPAPIERR_OK Then
            Return ReturnErrDesc(res)
        End If


        res = izzix.izzixSCARDFIM.izxSCFIM_GetImageEx(0, 3, m_Image)
        If res <> izzix.izzixSCARDFIM.FPAPIERR_OK Then
            Return ReturnErrDesc(res)
        End If
        res = izzix.izzixSCARDFIM.izxSCFIM_XtractFeature(0, 3, 1)
        If res <> izzix.izzixSCARDFIM.FPAPIERR_OK Then
            Return ReturnErrDesc(res)
        End If
        Return "OK"
    End Function


    Private Sub btnSLSlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSLSlogin.Click
        txtUserName.Text = ""
        txtBrCode.Text = ""
        Dim str As String = Scan_CB1000F()
        If Not str = "OK" Then
            MessageBox.Show("خطا هنگام اسکن اثر انگشت" & vbCrLf & str, "اسکن اثر انگشت", MessageBoxButtons.OK, MessageBoxIcon.Error)
            str = ""
        Else
            str = Match_SLS()

            If str = "True" Then
                GetPersonalData()
            Else
                If str = "False" Then
                    MessageBox.Show("هویت شما احراز نگردید" & vbCrLf & "لطفاً مجدداً تلاش نمایید", "احراز هویت", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
                MessageBox.Show("خطا هنگام مقایسه اثر انگشت" & vbCrLf & "لطفاً مجدداً تلاش نمایید", "مقایسه اثر انگشت", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtUserName.Enabled = True
                txtBrCode.Enabled = True
            End If

        End If
    End Sub

    Private Shared AIDISO As String = "00A404000AA00000006203010C0101"
    Function Match_SLS() As String
        Dim res As String = ""
        Try
            Dim Reader As SCReader = SCAPI.beginTransaction(strReaderName)
            Dim resByte() As Byte
            Reader.Transmit(SCUtil.ToByteArray(AIDISO), resByte)

            Dim res1 As Int16 = izzix.izzixSCARDFIM.izxSCFIM_FeatureTx(0, &HB0, &H10, 0, &H2)
            If res1 <> izzix.izzixSCARDFIM.FPAPIERR_OK Then
                Return ReturnErrDesc(res1)
            End If

            Dim securityLevel As Integer = 2

            Dim b As New StringBuilder(10)
            b.Append("B021")
            Dim finger As Integer = 0
            b.Append(finger.ToString("X2"))
            b.Append(securityLevel.ToString("X2"))
            Reader.Transmit(SCUtil.ToByteArray(b.ToString), resByte)
            res = IIf(SCUtil.FromByteArray(resByte) = "9000", "True", "False")
            SCAPI.EndTransaction(Reader)
        Catch sclibex As SCLibException
            res = "Error in sclib."
        Catch sex As SCardException
            res = "Error in SCReader."
        Catch ex As Exception
            res = "Some general Error Occured."
        End Try
        Return res
    End Function
    Sub GetPersonalData()
        Dim res1 As Byte() = SCAPI.getData(strReaderName)
        Dim tmp As String() = System.Text.Encoding.UTF8.GetString(res1).Trim().Split("|")
        If tmp.Length = 3 Then
            txtUserName.Text = tmp(0)
            txtUserName.Enabled = False
            txtBrCode.Text = tmp(1)
            txtBrCode.Enabled = False
        End If
    End Sub

    'Private Sub txtUserName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserName.KeyPress
    '    '  If e.KeyChar >= 58 Or (e.KeyChar.Trim <= 47 And e.KeyChar <> 45 And e.KeyChar <> 8 And e.KeyChar <> 13) Then

    '    If ((e.KeyChar >= "a" And e.KeyChar <= "z") Or (e.KeyChar >= "A" And e.KeyChar <= "Z") Or (e.KeyChar >= "آ" And e.KeyChar <= "ی")) Then

    '        e.Handled = True

    '    End If

    'End Sub

    Private Sub txtPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassword.TextChanged

    End Sub
End Class