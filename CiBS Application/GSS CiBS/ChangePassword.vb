
Public Class ChangePassword



    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If txtNewPass1.Text <> txtNewPass2.Text Then
            MessageBox.Show("رمزهای وارد شده یکسان نیستند ", "خطا")
            Exit Sub
        End If

        Try
            tmpWSResult = Decrypt(CiBS_WS.UserChangePassword(Encrypt(CurrentUser & "|" & txtOldPass.Text & "|" & txtNewPass1.Text & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99")))
        Catch ex As Exception
            InsertLocalLog("UserChangePassword- " & ex.Message)
            MessageBox.Show("خطا هنگام تغییر رمز کاربر" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show("رمز با موفقیت تغییر یافت", "اعلان")
            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Change Password" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "User has changed password successfully" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Dispose()
        End If
        If String.Compare(ws_fb(0), "0") = 0 Then
            MessageBox.Show("خطا هنگام تغییر رمز", "اعلان")
            Me.Dispose()
        End If
    End Sub

    Private Sub btnCancell_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancell.Click
        Me.Dispose()
    End Sub

    Private Sub ChangePassword_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class