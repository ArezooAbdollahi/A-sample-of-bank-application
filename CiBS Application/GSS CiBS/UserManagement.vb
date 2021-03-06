Imports System.Text
Imports CIBS.SCLib

Public Class UserManagement
    Dim finger As Integer = 0

    Private Sub btnResetPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetPassword.Click
        Try
            tmpWSResult = Decrypt(CiBS_WS.ResetPassword(Encrypt(txtUserName.Text.Trim & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99")))
        Catch ex As Exception
            InsertLocalLog("ResetPassword- " & ex.Message)
            MessageBox.Show("خطا هنگام بروزرسانی رمز کاربر" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        ws_fb = tmpWSResult.Split("|")
        If String.Compare(ws_fb(0), "Timeout") = 0 Then
            MessageBox.Show("Timeout", "خطا")
            Exit Sub
        End If
        If ws_fb(0).Contains("Error") Then
            MessageBox.Show(ws_fb(1), "خطا")
            Exit Sub
        End If
        If String.Compare(ws_fb(0), "1") = 0 Then
            MessageBox.Show("رمز کاربر همان شناسه عبور وی گردید", "اعلان")
            txtUserName.Enabled = True
            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Reset Password" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Password of UserName " & txtUserName.Text.Trim & " was reseted" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearTexts()
            txtUserName.Text = ""
        End If
    End Sub


    Private Sub btnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        If txtBrCode.Text = "" Or txtPersFamily.Text.Trim.Length < 2 Or txtPersName.Text.Trim.Length < 2 Or txtUserName.Text = "" Or cbStatus.SelectedIndex = -1 Or cbStatus.SelectedIndex = -1 Then
            MessageBox.Show("لطفاً مقادیر ورودی را بررسی نمایید", "خطا")
            Exit Sub
        End If

        Dim MainPosSerial As String = txtMainPosSerial.Text.Trim
        If MainPosSerial = "" Or MainPosSerial = " " Then
            MainPosSerial = 0
        End If

        Try
            tmpWSResult = Decrypt(CiBS_WS.InsertUser(Encrypt(txtPersName.Text & "|" & txtPersFamily.Text & "|" & txtUserName.Text & "|" & txtBrCode.Text & "|" & cbStatus.SelectedIndex & "|" & cbGrantLevel.SelectedIndex + 1 & "|" & MainPosSerial & "|" & Now.TimeOfDay.TotalSeconds & "|" & txtWinUserCode.Text & "|" & "99")))
        Catch ex As Exception
            InsertLocalLog("InsertUser- " & ex.Message)
            MessageBox.Show("خطا هنگام اضافه نمودن کاربر" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        ws_fb = tmpWSResult.Split("|")
        If String.Compare(ws_fb(0), "Timeout") = 0 Then
            MessageBox.Show("Timeout", "خطا")
            Exit Sub
        End If
        If CInt(ws_fb(0)) > 1 Then
            MessageBox.Show("فقط امکان تعریف یک کاربر ارشد در هر شعبه وجود دارد" & vbCrLf & " کد کاربر ارشد این شعبه " & ws_fb(0), "خطا")
            Exit Sub
        End If
        ' '
        If CInt(ws_fb(0)) = "-999" Then
            MessageBox.Show(" امکان تعریف مدیر سیستم فقط در شعبه مرکز وجود دارد", "خطا")
            Exit Sub
        End If
        If CInt(ws_fb(0)) = "-888" Then
            MessageBox.Show(" امکان تعریف کاربر پشتیبانی فقط در شعبه مرکز وجود دارد", "خطا")
            Exit Sub
        End If
        If CInt(ws_fb(0)) = "-777" Then
            MessageBox.Show("ابتدا کددستگاه پوز را تعریف نمایید ", "خطا")
            Exit Sub
        End If
        If String.Compare(ws_fb(0), "1") = 0 Then
            MessageBox.Show("کاربر با موفقیت اضافه گردید", "اعلان")
            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Add User" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "UserName " & txtUserName.Text.Trim & " was inserted" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearTexts()
            txtUserName.Text = ""
        Else
            If ws_fb(1).StartsWith("Violation of PRIMARY KEY constraint 'PK_Users'") Then
                MessageBox.Show("این شناسه عبور در سیستم وجود دارد", "خطا")
                Exit Sub
            Else
                MessageBox.Show(ws_fb(1), "خطا")
                Exit Sub
            End If
        End If
    End Sub

    Private Sub UserManagement_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If UserGrantLevel = 3 Then
            cbGrantLevel.Enabled = True
            txtBrCode.Enabled = True
            GroupBox3.Enabled = True
            dgvPos.Enabled = True
        End If
        If UserGrantLevel = 2 Then
            cbGrantLevel.SelectedIndex = 0
            txtBrCode.Text = CurrentBranchCode
            GroupBox3.Enabled = True
            dgvPos.Enabled = True
        End If
        If UserGrantLevel = 4 Then
            GroupBox3.Enabled = False
            dgvPos.Enabled = False
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ClearTexts()
        If txtUserName.Text.Length < 3 Then
            MessageBox.Show("لطفاً مقدار کد پرسنلی را بررسی نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Try
            tmpWSResult = Decrypt(CiBS_WS.SearchUser(Encrypt(txtUserName.Text & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99")))
        Catch ex As Exception
            InsertLocalLog("SearchUser- " & ex.Message)
            MessageBox.Show("خطا هنگام جستجوی کاربر" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        ws_fb = tmpWSResult.Split("|")
        If String.Compare(ws_fb(0), "Timeout") = 0 Then
            MessageBox.Show("Timeout", "خطا")
            Exit Sub
        End If
        If String.Compare(ws_fb(0), "Invalid") = 0 Then
            MessageBox.Show("این شناسه عبور در سیستم وجود ندارد", "اعلان")
            cbGrantLevel.SelectedIndex = 0
            txtBrCode.Text = CurrentBranchCode
            If UserGrantLevel <> 4 Then
                btnInsert.Enabled = True
                btnDelete.Enabled = False
            End If

            Exit Sub
        Else
            If ws_fb(0).Contains("Error") Then
                MessageBox.Show(ws_fb(1), "خطا")
                Exit Sub
            End If

            txtPersFamily.Text = ws_fb(1)
            txtPersName.Text = ws_fb(0)
            txtUserName.Text = ws_fb(2)
            cbStatus.SelectedIndex = ws_fb(4)
            txtWinUserCode.Text = ws_fb(7)

            If UserGrantLevel = 3 Or UserGrantLevel = 4 Or UserGrantLevel = 2 Then
                cbGrantLevel.SelectedIndex = ws_fb(5) - 1
                txtBrCode.Text = ws_fb(3)
                'ElseIf UserGrantLevel = 2 Then
                '    cbGrantLevel.SelectedIndex = 1
                '    txtBrCode.Text = CurrentBranchCode
            End If

            If ws_fb(6) = 0 Then
                txtMainPosSerial.Text = ""
            Else
                txtMainPosSerial.Text = ws_fb(6)
            End If

            If UserGrantLevel = 2 And ws_fb(5) = 1 Then
                If txtBrCode.Text.Trim <> ws_fb(3).Trim Then
                    MessageBox.Show("این کاربر به این شعبه تعلق ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearTexts()
                    Exit Sub
                End If
            End If

            If UserGrantLevel = 2 And (ws_fb(5) = 2 Or ws_fb(5) = 3 Or ws_fb(5) = 4) Then
                MessageBox.Show("شما مجاز به تغییر اطلاعات  این سطح کاربری نمی باشید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearTexts()
                Exit Sub
            End If
        End If
        'If Not UserGrantLevel >= 3 Then

        If UserGrantLevel = 4 Then
            txtUserName.Enabled = False
            btnSolveBlocked.Enabled = True
        ElseIf UserGrantLevel = 3 Or UserGrantLevel = 2 Then
            txtUserName.Enabled = False
            btnUpdate.Enabled = True
            btnResetPassword.Enabled = True
            btnDelete.Enabled = True
            btnInsert.Enabled = False
            btnSolveBlocked.Enabled = True
            gbSLS.Enabled = True
        End If
        'End If

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If txtBrCode.Text = "" Or txtPersFamily.Text = "" Or txtPersName.Text = "" Or txtUserName.Text = "" Or cbStatus.SelectedIndex = -1 Or cbStatus.SelectedIndex = -1 Then
            MessageBox.Show("لطفاً مقادیر ورودی را بررسی نمایید", "خطا")
            Exit Sub
        End If

        Dim MainPosSerial As String = txtMainPosSerial.Text.Trim
        If MainPosSerial = "" Or MainPosSerial = " " Then
            MainPosSerial = 0
        End If

        Try
            tmpWSResult = Decrypt(CiBS_WS.UpdateUser(Encrypt(txtUserName.Text.Trim & "|" & txtPersName.Text.Trim & "|" & txtPersFamily.Text.Trim & "|" & txtBrCode.Text.Trim & "|" & cbGrantLevel.SelectedIndex + 1 & "|" & cbStatus.SelectedIndex & "|" & MainPosSerial.Trim & "|" & Now.TimeOfDay.TotalSeconds & "|" & txtWinUserCode.Text.Trim & "|" & "99")))
        Catch ex As Exception
            InsertLocalLog("UpdateUser- " & ex.Message)
            MessageBox.Show("خطا هنگام بروزرسانی کاربر" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        ws_fb = tmpWSResult.Split("|")
        If String.Compare(ws_fb(0), "Timeout") = 0 Then
            MessageBox.Show("Timeout", "خطا")
            Exit Sub
        End If
        If ws_fb(0).Contains("Error") Then
            MessageBox.Show(ws_fb(1), "خطا")
            Exit Sub
        End If

        If CInt(ws_fb(0)) = "0" Then
            MessageBox.Show(" بررسی نمایید که کددستگاه پوز تعریف شده باشد", "خطا")
            Exit Sub
        End If
        If String.Compare(ws_fb(0), "1") = 0 Then
            MessageBox.Show("اطلاعات کاربر ذخیره گردید", "اعلان")
            txtUserName.Enabled = True
            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Change User Information" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Information of UserName " & txtUserName.Text.Trim & " was changed" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearTexts()
            txtUserName.Text = ""
        End If
    End Sub

    Sub ClearPosTexts()

        txtPosIP.Text = ""
        txtPosSerial.Text = ""
        'txtIsSerial.Text = ""
        'txtTerminalOrder.Text = ""
        'txtTimeOut.Text = ""
        'txtPosPort.Text = ""
    End Sub

    Sub ClearTexts()
        txtPersFamily.Text = ""
        txtPersName.Text = ""
        txtMainPosSerial.Text = ""
        cbStatus.SelectedIndex = -1
        cbGrantLevel.SelectedIndex = -1
        txtBrCode.Text = ""
        txtWinUserCode.Text = ""
        btnInsert.Enabled = False
        btnUpdate.Enabled = False
        btnResetPassword.Enabled = False
        btnSolveBlocked.Enabled = False
        gbSLS.Enabled = False
    End Sub


    Private Sub btnSolveBlocked_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSolveBlocked.Click
        tmpWSResult = (Decrypt(CiBS_WS.UpdateFailedLogin(Encrypt(txtUserName.Text & "|True" & "|" & Now.TimeOfDay.TotalSeconds))))

        tmpWSResult = Decrypt(CiBS_WS.SetIsLogedIn(Encrypt(txtUserName.Text & "|" & txtUserName.Text & "|" & Now.TimeOfDay.TotalSeconds)))
        If tmpWSResult = 1 Then
            MessageBox.Show("رفع مسدودی کاربر با موفقیت انجام گردید", "اعلان")
        Else
            MessageBox.Show("خطا هنگام رفع مسدودی کاربر ", "خطا")
        End If
    End Sub

    Private Sub btnUsersList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUsersList.Click

        Try
            tmpWSResult = Decrypt(CiBS_WS.FindUsers(Encrypt(CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99")))
        Catch ex As Exception
            MessageBox.Show("خطا هنگام دریافت لیست کاربران ", "خطا")
            Exit Sub
        End Try

        Dim datatable As New DataTable("Users")

        datatable.Columns.Add("ردیف")
        datatable.Columns.Add("کد پرسنلی")
        datatable.Columns.Add("نام")
        datatable.Columns.Add("نام خانوادگی")
        datatable.Columns.Add("کد شعبه")
        datatable.Columns.Add("وضعیت")
        datatable.Columns.Add("سطح دسترسی")
        datatable.Columns.Add("مسدودی")
        datatable.Columns.Add("PosID")
        datatable.Columns.Add("کد ویندوزی")

        Dim tmpAns As String() = tmpWSResult.Split("!")
        For i As Integer = 0 To tmpAns.Length - 2
            Dim tmp As String() = tmpAns(i).Split("|")

            datatable.Rows.Add(New Object() {i + 1, tmp(0).Trim, tmp(1).Trim, tmp(2).Trim, tmp(3).Trim, tmp(4).Trim, tmp(5).Trim, tmp(6).Trim, tmp(7).Trim, tmp(8).Trim})
        Next
        dgvUsers.DataSource = datatable
        txtUsersCount.Text = dgvUsers.RowCount
    End Sub

    Private Sub dgvUsers_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvUsers.Sorted
        For i As Integer = 0 To dgvUsers.RowCount - 1
            dgvUsers.Rows(i).Cells(0).Value = i + 1
        Next
    End Sub

    Declare Function GetWindowDC Lib "user32" (ByVal hwnd As IntPtr) As IntPtr

    Function Scan_CB1000F() As Boolean
        txtMsg.Text = ""
        btnEnroll.Enabled = False
        If finger = 0 Then
            txtMsg.Text = "لطفاً انگشت را انتخاب نمایید"
            Return False
        End If

        Dim m_Image(70 * 80) As Byte

        Dim res As Int16 = izzix.izzixSCARDFIM.izxSCFIM_IsAvailableDevice(0)
        If res <> izzix.izzixSCARDFIM.FPAPIERR_OK Then
            txtMsg.Text = ReturnErrDesc(res)
            Return False
        End If

        res = izzix.izzixSCARDFIM.izxSCFIM_CapChkImage(0, 20)
        If res <> izzix.izzixSCARDFIM.FPAPIERR_OK Then
            txtMsg.Text = ReturnErrDesc(res)
            Return False
        End If


        res = izzix.izzixSCARDFIM.izxSCFIM_GetImageEx(0, 3, m_Image)
        If res <> izzix.izzixSCARDFIM.FPAPIERR_OK Then
            txtMsg.Text = ReturnErrDesc(res)
            Return False
        End If
        Dim HDC As IntPtr = GetWindowDC(pbScannedFinger.Handle)
        btnEnroll.Enabled = True
        res = izzix.izzixSCARDFIM.izxSCFIM_DisplayImage(HDC, 0, 0, pbScannedFinger.Width, pbScannedFinger.Height, m_Image, 70, 80)
        If res <> izzix.izzixSCARDFIM.FPAPIERR_OK Then
            txtMsg.Text = ReturnErrDesc(res)
            Return False
        End If
        res = izzix.izzixSCARDFIM.izxSCFIM_XtractFeature(0, 3, 1)
        If res <> izzix.izzixSCARDFIM.FPAPIERR_OK Then
            txtMsg.Text = ReturnErrDesc(res)
            Return False
        End If
        txtMsg.Text = "اسکن اثر انگشت انجام گردید"
        Return True
    End Function

    Private Sub c_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbR1.CheckedChanged, rbR4.CheckedChanged, rbR3.CheckedChanged, rbR2.CheckedChanged, rbL1.CheckedChanged, rbL2.CheckedChanged, rbL3.CheckedChanged, rbL4.CheckedChanged, rbR5.CheckedChanged, rbL5.CheckedChanged
        If rbR1.Checked Then
            finger = 1
            Return
        End If
        If rbR2.Checked Then
            finger = 2
            Return
        End If
        If rbR3.Checked Then
            finger = 3
            Return
        End If
        If rbR4.Checked Then
            finger = 4
            Return
        End If
        If rbR5.Checked Then
            finger = 5
            Return
        End If


        If rbL5.Checked Then
            finger = 6
            Return
        End If
        If rbL4.Checked Then
            finger = 7
            Return
        End If
        If rbL3.Checked Then
            finger = 8
            Return
        End If
        If rbL2.Checked Then
            finger = 9
            Return
        End If
        If rbL1.Checked Then
            finger = 10
            Return
        End If
    End Sub

    Private Shared AIDISO As String = "00A404000AA00000006203010C0101"

    Private Sub btnEnroll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CB1000F_Enrol(strReaderName)
    End Sub

    Function CB1000F_Enrol(ByVal strReaderName As String) As String
        txtMsg.Text = ""
        Dim res As String = ""

        Try
            Dim Reader As SCReader = SCAPI.beginTransaction(strReaderName)
            Dim resByte() As Byte
            Reader.Transmit(SCUtil.ToByteArray(AIDISO), resByte)

            Dim res1 As Int16 = izzix.izzixSCARDFIM.izxSCFIM_FeatureTx(0, &HB0, &H10, finger, &H2)
            If res1 <> izzix.izzixSCARDFIM.FPAPIERR_OK Then
                txtMsg.Text = ReturnErrDesc(res1)
            End If

            Dim b As New StringBuilder(10)
            b.Append("B020")
            b.Append(finger.ToString("X2"))
            b.Append("00")
            Reader.Transmit(SCUtil.ToByteArray(b.ToString), resByte)
            res = IIf(SCUtil.FromByteArray(resByte) = "9000", "اثر انگشت روی کارت ذخیره گردید", SCUtil.FromByteArray(resByte))
            SCAPI.EndTransaction(Reader)
            txtMsg.Text = res

        Catch sclibex As SCLibException
            res = "Error in sclib."
        Catch sex As SCardException
            res = "Error in SCReader."
        Catch ex As Exception
            res = "Some general Error Occured."
        End Try
        Return res

    End Function

    Private Sub btnScanFinger_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Scan_CB1000F()
    End Sub

    Private Sub btnSavePersonalInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim b As Byte() = System.Text.Encoding.UTF8.GetBytes((txtUserName.Text.Trim & "|" & txtBrCode.Text.Trim & "|" & txtPersName.Text.Trim & " " & txtPersFamily.Text.Trim).PadRight(200, " "))
        Dim sb As New StringBuilder
        sb.Append("B0300000")
        sb.Append(b.Length.ToString("X2"))
        For index As Integer = 0 To b.Length - 1
            sb.Append(b(index).ToString("X2"))
        Next
        txtMsg.Text = IIf(SCAPI.test(strReaderName, sb.ToString()) = "9000", "اطلاعات فردی در کارت ذخیره گردید", SCAPI.test(strReaderName, sb.ToString()))
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            tmpWSResult = Decrypt(CiBS_WS.DeleteUser(Encrypt(txtUserName.Text.Trim & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99")))
        Catch ex As Exception
            InsertLocalLog("ResetPassword- " & ex.Message)
            MessageBox.Show("خطا هنگام حذف کاربر" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        ws_fb = tmpWSResult.Split("|")
        If String.Compare(ws_fb(0), "Timeout") = 0 Then
            MessageBox.Show("Timeout", "خطا")
            Exit Sub
        End If
        If ws_fb(0).Contains("Error") Then
            MessageBox.Show(ws_fb(1), "خطا")
            Exit Sub
        End If
        If String.Compare(ws_fb(0), "1") = 0 Then
            MessageBox.Show(" کاربر مربوطه حذف گردید", "اعلان")
            txtUserName.Enabled = True
            'If Not InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "Reset Password" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Password of UserName " & txtUserName.Text.Trim & " was reseted" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearTexts()
            txtUserName.Text = ""
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            tmpWSResult = Decrypt(CiBS_WS.FindPos(Encrypt(Now.TimeOfDay.TotalSeconds)))
        Catch ex As Exception
            MessageBox.Show("خطا هنگام دریافت لیست کاربران ", "خطا")
            Exit Sub
        End Try

        Dim datatable As New DataTable("Users")

        ' datatable.Columns.Add("ردیف")
        'datatable.Columns.Add("PosID")
        datatable.Columns.Add("PosSeril")
        datatable.Columns.Add("PosIP")
        'datatable.Columns.Add("PosPort")
        'datatable.Columns.Add("IsSerial")
        'datatable.Columns.Add("TimeOut")
        'datatable.Columns.Add("TerminalOrder")

        Dim tmpAns As String() = tmpWSResult.Split("!")
        For i As Integer = 0 To tmpAns.Length - 2
            Dim tmp As String() = tmpAns(i).Split("|")

            'datatable.Rows.Add(New Object() {i + 1, tmp(0).Trim, tmp(1).Trim, tmp(2).Trim, tmp(3).Trim, tmp(4).Trim, tmp(5).Trim, tmp(6).Trim})
            datatable.Rows.Add(New Object() {tmp(0).Trim, tmp(1).Trim}) ', tmp(2).Trim, tmp(3).Trim, tmp(4).Trim, tmp(5).Trim, tmp(6).Trim})
        Next
        dgvPos.DataSource = datatable

    End Sub

    Private Sub btnSearchPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPos.Click

        If txtPosSerial.Text.Length < 1 Then
            MessageBox.Show("لطفاً مقدار سریال پوز را بررسی نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Try
            tmpWSResult = Decrypt(CiBS_WS.SearchPos(Encrypt(txtPosSerial.Text & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99")))
        Catch ex As Exception
            InsertLocalLog("SearchUser- " & ex.Message)
            MessageBox.Show("خطا هنگام جستجوی Pos" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        ws_fb = tmpWSResult.Split("|")
        If String.Compare(ws_fb(0), "Timeout") = 0 Then
            MessageBox.Show("Timeout", "خطا")
            Exit Sub
        End If
        If String.Compare(ws_fb(0), "Invalid") = 0 Then
            MessageBox.Show("این کد سریال پوز در سیستم وجود ندارد", "اعلان")
            If UserGrantLevel <> 4 Then
                btnAddPos.Enabled = True
                btnUpdatePos.Enabled = False
            End If

            Exit Sub
        Else
            If ws_fb(0).Contains("Error") Then
                MessageBox.Show(ws_fb(1), "خطا")
                Exit Sub
            End If
            'If Not UserGrantLevel >= 3 Then
            '    If txtBrCode.Text.Trim <> ws_fb(3).Trim Then
            '        MessageBox.Show("این کاربر به این شعبه تعلق ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '        Exit Sub
            '    End If
            'End If


            txtPosIP.Text = ws_fb(1)
            'txtPosPort.Text = ws_fb(2)
            ' txtPosSerial.Text = ws_fb(3)
            'txtIsSerial.Text = ws_fb(4)
            'txtTerminalOrder.Text = ws_fb(6)
            'txtTimeOut.Text = ws_fb(5)

            txtPosSerial.Enabled = False
            btnUpdatePos.Enabled = True
            btnAddPos.Enabled = False
        End If
        ' ClearPosTexts()
    End Sub

    Private Sub btnAddPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPos.Click
        If txtPosSerial.Text = "" Or txtPosIP.Text = "" Or txtPosSerial.Text = "" Then 'Or txtIsSerial.Text = "" Or txtTerminalOrder.Text = "" Or txtTimeOut.Text = "" Or txtPosPort.Text = "" Then
            MessageBox.Show("لطفاً مقادیر ورودی را بررسی نمایید", "خطا")
            Exit Sub
        End If
        Try
            tmpWSResult = Decrypt(CiBS_WS.InsertPos(Encrypt(txtPosSerial.Text & "|" & txtPosIP.Text & "|" & "99"))) ' & "|" & txtIsSerial.Text & "|" & txtTerminalOrder.Text & "|" & txtTimeOut.Text & "|" & txtPosPort.Text)))
        Catch ex As Exception
            InsertLocalLog("InsertUser- " & ex.Message)
            MessageBox.Show("خطا هنگام اضافه نمودن کاربر" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        ws_fb = tmpWSResult.Split("|")
        If String.Compare(ws_fb(0), "Timeout") = 0 Then
            MessageBox.Show("Timeout", "خطا")
            Exit Sub
        End If
        'If CInt(ws_fb(0)) > 1 Then
        '    MessageBox.Show("فقط امکان تعریف یک کاربر ارشد در هر شعبه وجود دارد" & vbCrLf & " کد کاربر ارشد این شعبه " & ws_fb(0), "خطا")
        '    Exit Sub
        'End If
        ' '
        'If CInt(ws_fb(0)) = "-999" Then
        '    MessageBox.Show(" امکان تعریف مدیر سیستم فقط در شعبه مرکز وجود دارد", "خطا")
        '    Exit Sub
        'End If
        'If CInt(ws_fb(0)) = "-888" Then
        '    MessageBox.Show(" امکان تعریف کاربر پشتیبانی فقط در شعبه مرکز وجود دارد", "خطا")
        '    Exit Sub
        'End If
        If String.Compare(ws_fb(0), "1") = 0 Then
            MessageBox.Show("دستگاه مربوطه با موفقیت اضافه گردید", "اعلان")
            If Not InsertLog(CurrentBranchCode & "|" & txtMainPosSerial.Text.Trim & "|" & "Add User" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "UserName " & txtUserName.Text.Trim & " was inserted" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'ClearTexts()
            txtUserName.Text = ""
        Else
            If ws_fb(1).StartsWith("Violation of PRIMARY KEY constraint 'PK_Users'") Then
                MessageBox.Show("این شناسه دستگاه در سیستم وجود دارد", "خطا")
                Exit Sub
            Else
                MessageBox.Show(ws_fb(1), "خطا")
                Exit Sub
            End If
        End If
        txtPosIP.Text = ""
        txtPosSerial.Text = ""

    End Sub

    Private Sub btnSavePos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdatePos.Click
        If txtPosSerial.Text = "" Or txtPosIP.Text = "" Or txtPosSerial.Text = "" Then ' Or txtIsSerial.Text = "" Or txtTerminalOrder.Text = "" Or txtTimeOut.Text = "" Or txtPosPort.Text = "" Then
            MessageBox.Show("لطفاً مقادیر ورودی را بررسی نمایید", "خطا")
            Exit Sub
        End If
        Try
            tmpWSResult = Decrypt(CiBS_WS.UpdatePos(Encrypt(txtPosSerial.Text & "|" & txtPosIP.Text & "|" & "99"))) ' & "|" & txtIsSerial.Text & "|" & txtTerminalOrder.Text & "|" & txtTimeOut.Text & "|" & txtPosPort.Text & "|" & Now.TimeOfDay.TotalSeconds)))
        Catch ex As Exception
            InsertLocalLog("UpdateUser- " & ex.Message)
            MessageBox.Show("خطا هنگام بروزرسانی کاربر" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        ws_fb = tmpWSResult.Split("|")
        If String.Compare(ws_fb(0), "Timeout") = 0 Then
            MessageBox.Show("Timeout", "خطا")
            Exit Sub
        End If
        If ws_fb(0).Contains("Error") Then
            MessageBox.Show(ws_fb(1), "خطا")
            Exit Sub
        End If
        If String.Compare(ws_fb(0), "1") = 0 Then
            MessageBox.Show("اطلاعات پوز ذخیره گردید", "اعلان")
            txtUserName.Enabled = True
            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Change User Information" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Information of UserName " & txtUserName.Text.Trim & " was changed" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearPosTexts()
            txtPosSerial.Text = ""
            txtPosSerial.Enabled = True
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ClearPosTexts()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        txtUserName.Text = ""
        txtUserName.Enabled = True
        txtBrCode.Text = ""
        cbGrantLevel.SelectedItem = -1
        ClearTexts()
    End Sub
End Class