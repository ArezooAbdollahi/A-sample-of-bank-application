Public Class RequestConsumables

    Dim tmpConsumables As String()
    Dim ThisBranchCode As String

    Private Sub RequestConsumables_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If UserGrantLevel = 3 Or UserGrantLevel = 4 Then
            txtBranchCode.Text = ""
            txtBranchCode.Enabled = True
            txtBranchCode.Visible = True
        Else
            ThisBranchCode = CurrentBranchCode
            txtBranchCode.Text = CurrentBranchCode
            txtBranchCode.Enabled = False
            txtBranchCode.Visible = True
        End If

        'get Consumables
        Try
            tmpWSResult = CiBS_WS.GetConsumables(Encrypt("1|" & Now.TimeOfDay.TotalSeconds))
        Catch ex As Exception
            InsertLocalLog("GetConsumables- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت لیست مواد مصرفی" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim tmpAns As String() = tmpWSResult.Split("!")

        If String.Compare(Decrypt(tmpAns(0)), "Timeout") = 0 Then
            MessageBox.Show("Timeout", "خطا")
            Exit Sub
        End If

        ReDim tmpConsumables(tmpAns.Length - 1)

        For i As Integer = 0 To tmpAns.Length - 2
            Dim tmp As String() = Decrypt(tmpAns(i)).Split("|")
            tmpConsumables(i) = tmp(0)
            cbConsumables.Items.Add(tmp(1).Trim)
        Next

        UpdateGrid()

    End Sub

    Private Sub btnAddRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRequest.Click
        ThisBranchCode = txtBranchCode.Text.Trim
        If cbConsumables.SelectedIndex = -1 Then
            MessageBox.Show("ابتدا مواد مصرفی مورد نظر را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If txtConsumableRequestCount.Text.Length = 0 Then
            MessageBox.Show("مقدار مواد مصرفی مورد نظر را وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Try
            tmpWSResult = Decrypt(CiBS_WS.InsertConsumableRequest(Encrypt(ThisBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & txtConsumableRequestCount.Text.Trim & "|" & tmpConsumables(cbConsumables.SelectedIndex) & "|" & DT.DateToPersian(Now.Date).ShortDate.ToString & "|1|" & Now.TimeOfDay.TotalSeconds)))
        Catch ex As Exception
            InsertLocalLog("InsertConsumableRequest- " & ex.Message)
            MessageBox.Show("خطا هنگام ورود درخواست مواد مصرفی" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        ws_fb = tmpWSResult.Split("|")
        If String.Compare(ws_fb(0), "Timeout") = 0 Then
            MessageBox.Show("Timeout", "خطا")
            Exit Sub
        End If
        If String.Compare(ws_fb(0), "1") = 0 Then
            If Not InsertLog(ThisBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Request Consumables" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Consumable NO " & tmpConsumables(cbConsumables.SelectedIndex) & " was requested for count " & txtConsumableRequestCount.Text & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' MessageBox.Show("درخواست شما با موفقیت ارسال گردید", "اعلان")
            UpdateGrid()
            txtConsumableRequestCount.Text = ""
            cbConsumables.SelectedIndex = -1
        Else
            MessageBox.Show(ws_fb(1), "خطا")
            Exit Sub
        End If

    End Sub

    Sub UpdateGrid()

        Try
            tmpWSResult = CiBS_WS.GetRepositoryRequests(Encrypt(ThisBranchCode & "|" & "1" & "|" & "0" & "|" & Now.TimeOfDay.TotalSeconds))
        Catch ex As Exception
            InsertLocalLog("GetRepositoryRequests- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت لیست درخواست مواد مصرفی" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim dtt As New DataTable("Cards")
        dtt.Columns.Add("کد شعبه")
        dtt.Columns.Add("کاربر درخواست کننده")
        dtt.Columns.Add("نوع مواد مصرفی")
        dtt.Columns.Add("تعداد درخواستی")
        dtt.Columns.Add("تاریخ درخواست")
        dtt.Columns.Add("تعداد تایید شده")
        dtt.Columns.Add("تاریخ تایید")
        dtt.Columns.Add("کاربر تایید کننده")
        dtt.Columns.Add("RowID")

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
            dtt.Rows.Add(New Object() {tmp(0), tmp(1), tmp(2), tmp(3), tmp(4), tmp(5), tmp(6), tmp(7), tmp(13)})
        Next
        lblConsumableType.Text = ""
        lblAcceptedCount.Text = ""
        txtRecievedCount.Text = ""
        dgvRequests.DataSource = dtt
        dgvRequests.Columns(8).Visible = False

    End Sub

    Private Sub btnDeletRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeletRequest.Click
        If dgvRequests.SelectedRows.Count = 0 Then
            MessageBox.Show("ابتدا یک سطر را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If MessageBox.Show("آیا مایل به حذف درخواست انتخاب شده هستید؟", "تاییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If dgvRequests.Rows(dgvRequests.CurrentRow.Index).Cells(5).Value.ToString.Length > 0 Then
                MessageBox.Show("امکان حذف این درخواست بدلیل داشتن سابقه مقدور نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Try
                tmpWSResult = Decrypt(CiBS_WS.DeleteReposotoryRequest(Encrypt(dgvRequests.Rows(dgvRequests.CurrentRow.Index).Cells(8).Value & "|" & Now.TimeOfDay.TotalSeconds)))
            Catch ex As Exception
                InsertLocalLog("DeleteReposotoryRequest- " & ex.Message)
                MessageBox.Show("خطا هنگام حذف درخواست مواد مصرفی" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            ws_fb = tmpWSResult.Split("|")
            If String.Compare(ws_fb(0), "Timeout") = 0 Then
                MessageBox.Show("Timeout", "خطا")
                Exit Sub
            End If
            If String.Compare(ws_fb(0), "1") = 0 Then
                ' MessageBox.Show("درخواست شما حذف گردید", "اعلان")
                If Not InsertLog(ThisBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Delete Consumables" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Consumable Request NO " & dgvRequests.Rows(dgvRequests.CurrentRow.Index).Cells(8).Value & " was deleted" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                UpdateGrid()
                txtConsumableRequestCount.Text = ""
                cbConsumables.SelectedIndex = -1
            Else
                MessageBox.Show(ws_fb(1), "خطا")
                Exit Sub
            End If

        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        UpdateGrid()
    End Sub

    Private Sub dgvRequests_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvRequests.SelectionChanged
        Try
            lblAcceptedCount.Text = dgvRequests.Rows(dgvRequests.CurrentRow.Index).Cells(5).Value
            lblConsumableType.Text = dgvRequests.Rows(dgvRequests.CurrentRow.Index).Cells(2).Value
            If lblAcceptedCount.Text.Length > 0 Then
                btnAccept.Enabled = True
            Else
                btnAccept.Enabled = False
            End If
        Catch ex As Exception
        End Try

    End Sub


    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        If txtRecievedCount.Text.Length <= 0 Then
            MessageBox.Show("لطفاً تعداد دریافتی را وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If
        If MessageBox.Show(" آیا دریافت تعداد " & txtRecievedCount.Text & " عدد " & lblConsumableType.Text & " از تعداد ارسال شده " & lblAcceptedCount.Text & " عدد را تایید می نمایید؟ ", "تاییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Try
                tmpWSResult = Decrypt(CiBS_WS.UpdateRepositoryReceiving(Encrypt(DT.DateToPersian(Now.Date).ShortDate.ToString & "|" & txtRecievedCount.Text.Trim & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & dgvRequests.Rows(dgvRequests.CurrentRow.Index).Cells(8).Value & "|" & Now.TimeOfDay.TotalSeconds)))
            Catch ex As Exception
                InsertLocalLog("UpdateRepositoryReceiving- " & ex.Message)
                MessageBox.Show("خطا هنگام بروزرسانی درخواست مواد مصرفی" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            ws_fb = tmpWSResult.Split("|")
            If String.Compare(ws_fb(0), "Timeout") = 0 Then
                MessageBox.Show("Timeout", "خطا")
                Exit Sub
            End If
            If String.Compare(ws_fb(0), "1") = 0 Then
                ' MessageBox.Show("درخواست تایید گردید", "اعلان")
                If Not InsertLog(ThisBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Receive Consumables" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Consumable Request NO " & dgvRequests.Rows(dgvRequests.CurrentRow.Index).Cells(8).Value & " with accepted count " & dgvRequests.Rows(dgvRequests.CurrentRow.Index).Cells(5).Value & " was received with count " & txtRecievedCount.Text & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)

                If dgvRequests.RowCount = 1 Then
                    UpdateGrid()
                    lblConsumableType.Text = ""
                    lblAcceptedCount.Text = ""
                    txtRecievedCount.Text = ""
                Else
                    lblConsumableType.Text = ""
                    lblAcceptedCount.Text = ""
                    txtRecievedCount.Text = ""
                    UpdateGrid()
                End If
            Else
                MessageBox.Show(ws_fb(1), "خطا")
                Exit Sub
            End If

        End If
    End Sub


End Class