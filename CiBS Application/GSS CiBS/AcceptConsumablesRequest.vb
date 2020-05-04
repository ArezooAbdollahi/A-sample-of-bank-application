Public Class AcceptConsumablesRequest
    Dim tmpCradType As String()

    Private Sub AcceptRequest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If UserGrantLevel = 3 Then

        Else
            txtBranches.Text = CurrentBranchCode
        End If

        UpdateGrid()

        ' GetCunsomableType()
    End Sub

    Sub GetCunsomableType()
        Try
            tmpWSResult = CiBS_WS.GetCardTypes(Encrypt("ACTIVE" & "|" & "99"))
            Dim tmpAns As String() = tmpWSResult.Split("!")
            ReDim tmpCradType(tmpAns.Length - 1)
            For i As Integer = 0 To tmpAns.Length - 2
                Dim tmp() As String = Decrypt(tmpAns(i)).Split("|")
                tmpCradType(i) = tmp(0)
                cmbCunsomable.Items.Add(tmp(1).Trim)
            Next
        Catch ex As Exception
            'InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error while getting card types-" & ex.Message & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds)
            'InsertLocalLog("GetCardTypes-" & ex.Message)
            MessageBox.Show("خطا هنگام دریافت انواع کارت" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Sub UpdateGrid()
        Dim BrCode As String
        If txtBranches.Text.Trim = "" Then
            BrCode = "NULL"
        Else
            BrCode = txtBranches.Text.Trim
        End If
        Try
            tmpWSResult = CiBS_WS.GetRepositoryRequests(Encrypt(BrCode & "|" & "1" & "|" & "1" & "|" & Now.TimeOfDay.TotalSeconds))
        Catch ex As Exception
            InsertLocalLog("GetRepositoryRequests- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت اطلاعات درخواستها" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim dtt As New DataTable("Cards")
        dtt.Columns.Add("کد شعبه")
        dtt.Columns.Add("کاربر درخواست کننده")
        dtt.Columns.Add("نوع مواد مصرفی")
        dtt.Columns.Add("تعداد درخواستی")
        dtt.Columns.Add("تاریخ درخواست")
        dtt.Columns.Add("RowID")
        dtt.Columns.Add("ConsumableCode")

        Dim tmpAns As String() = tmpWSResult.Split("!")
        If String.Compare(Decrypt(tmpAns(0)), "Timeout") = 0 Then
            MessageBox.Show("Timeout", "خطا")
            Exit Sub
        End If
        Dim CardsRecords(tmpAns.Length - 2) As String
        For i As Integer = 0 To tmpAns.Length - 2
            CardsRecords(i) = Decrypt(tmpAns(i))
        Next
        For i As Integer = 0 To CardsRecords.Length - 1
            Dim tmp As String()
            ReDim tmp(CardsRecords.Length - 1)
            tmp = CardsRecords(i).Split("|")
            dtt.Rows.Add(New Object() {tmp(0), tmp(1), tmp(2), tmp(3), tmp(4), tmp(13), tmp(14)})
        Next
        dgvRequests.DataSource = dtt
        dgvRequests.Columns(5).Visible = False
        dgvRequests.Columns(6).Visible = False
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        UpdateGrid()
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        If txtAcceptedCount.Text.Length <= 0 Then
            MessageBox.Show("لطفاً تعداد تاییدی را وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If
        If MessageBox.Show(" آیا تایید تعداد " & txtAcceptedCount.Text & " عدد " & lblConsumableType.Text & " از تعداد درخواست شده " & lblRequestCount.Text & " عدد را تایید می نمایید؟ ", "تاییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Try
                tmpWSResult = Decrypt(CiBS_WS.UpdateRepositoryAccepting(Encrypt(DT.DateToPersian(Now.Date).ShortDate.ToString & "|" & txtAcceptedCount.Text.Trim & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & dgvRequests.Rows(dgvRequests.CurrentRow.Index).Cells(5).Value & "|" & Now.TimeOfDay.TotalSeconds)))
            Catch ex As Exception
                InsertLocalLog("UpdateRepositoryAccepting- " & ex.Message)
                MessageBox.Show("خطا هنگام بروزرسانی درخواست مواد مصرفی" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            ws_fb = tmpWSResult.Split("|")
            If String.Compare(ws_fb(0), "Timeout") = 0 Then
                MessageBox.Show("Timeout", "خطا")
                Exit Sub
            End If
            If String.Compare(ws_fb(0), "9999") = 0 Then
                MessageBox.Show("موجودی انبار این کالا از میزان تاییدی کمتر است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            If String.Compare(ws_fb(0), "1") = 0 Then
                If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Accept Consumables" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Consumable Request NO " & dgvRequests.Rows(dgvRequests.CurrentRow.Index).Cells(5).Value & " related to branch code " & dgvRequests.Rows(dgvRequests.CurrentRow.Index).Cells(0).Value & " with count " & dgvRequests.Rows(dgvRequests.CurrentRow.Index).Cells(3).Value & " was accepted for count " & txtAcceptedCount.Text & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                If dgvRequests.RowCount = 1 Then
                    UpdateGrid()
                    lblBrCode.Text = ""
                    lblConsumableType.Text = ""
                    lblRequestCount.Text = ""
                    txtAcceptedCount.Text = ""
                    txtBranchRemain.Text = ""
                    btnAccept.Enabled = False
                Else
                    lblBrCode.Text = ""
                    lblConsumableType.Text = ""
                    lblRequestCount.Text = ""
                    txtAcceptedCount.Text = ""
                    txtBranchRemain.Text = ""
                    btnAccept.Enabled = False
                    UpdateGrid()
                End If
            Else
                MessageBox.Show(ws_fb(1), "خطا")
                UpdateGrid()
                Exit Sub
            End If

        End If
    End Sub

    Private Sub dgvRequests_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvRequests.SelectionChanged

        Try
            lblBrCode.Text = dgvRequests.Rows(dgvRequests.CurrentRow.Index).Cells(0).Value
            lblConsumableType.Text = dgvRequests.Rows(dgvRequests.CurrentRow.Index).Cells(2).Value
            lblRequestCount.Text = dgvRequests.Rows(dgvRequests.CurrentRow.Index).Cells(3).Value
            GetCardRemain(lblBrCode.Text, dgvRequests.Rows(dgvRequests.CurrentRow.Index).Cells(6).Value)
            btnAccept.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
       
    End Sub

    Sub GetCardRemain(ByVal BrCod As String, ByVal ConsumableCode As Integer)
        Try
            tmpWSResult = CiBS_WS.GetCardRemain(Encrypt(ConsumableCode & "|" & BrCod & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99"))
        Catch ex As Exception
            MessageBox.Show("خطا هنگام دریافت مانده کارت", "خطا")
            Exit Sub
        End Try
        txtBranchRemain.Text = Decrypt(tmpWSResult).Trim
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        UpdateGrid()
    End Sub

End Class