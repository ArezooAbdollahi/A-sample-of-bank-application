Public Class AdminMessages

    Private Sub AdminMessages_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtFromDate.Text = DT.DateToPersian(Now.Date.AddMonths(-1)).ShortDate
        txtToDate.Text = DT.DateToPersian(Now.Date).ShortDate
        UpdateGrid()

    End Sub

    Sub UpdateGrid()
        Try
            tmpWSResult = CiBS_WS.GetAdminMessages(Encrypt(txtFromDate.Text.Trim & "|" & txtToDate.Text.Trim & "|" & Now.TimeOfDay.TotalSeconds))
        Catch ex As Exception
            InsertLocalLog("GetAdminMessages- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت پیامها" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim dtt As New DataTable("Cards")
        dtt.Columns.Add("ردیف")
        dtt.Columns.Add("از تاریخ")
        dtt.Columns.Add("تا تاریخ")
        dtt.Columns.Add("پیام مدیر سیستم")
        dtt.Columns.Add("وضعیت")
        dtt.Columns.Add("تاریخ درج پیام")
        dtt.Columns.Add("ID")

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
            dtt.Rows.Add(New Object() {i + 1, tmp(1), tmp(2), tmp(3), tmp(4), tmp(5), tmp(0)})
        Next

        dgvAdminMesages.DataSource = dtt
        dgvAdminMesages.Columns(6).Visible = False

    End Sub


    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        UpdateGrid()
    End Sub

    Private Sub btnAddMessage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddMessage.Click
        If txtMsg.Text.Length < 5 Then
            MessageBox.Show("متن پیام خالی یا کمتر از 5 کارکتر میباشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Try
            tmpWSResult = Decrypt(CiBS_WS.InsertAdminMessage(Encrypt(txtNewFromdate.Text & "|" & txtNewToDate.Text & "|" & txtMsg.Text & "|" & DT.DateToPersian(Now.Date).ShortDate & "|" & cbStatus.SelectedIndex & "|" & Now.TimeOfDay.TotalSeconds)))
        Catch ex As Exception
            InsertLocalLog("InsertAdminMessage- " & ex.Message)
            MessageBox.Show("خطا هنگام اضافه نمودن پیام" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        If tmpWSResult = "1" Then
            MessageBox.Show("پیام جدید با موفقیت اضافه گردید", "اعلان")
            txtNewFromdate.Text = ""
            txtNewToDate.Text = ""
            txtMsg.Text = ""
            cbStatus.SelectedIndex = -1
        Else
            MessageBox.Show("خطا هنگام اضافه نمودن پیام جدید", "خطا")
        End If
        UpdateGrid()
    End Sub

    Private Sub btnDeleteMessage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteMessage.Click
        If dgvAdminMesages.Rows.Count > 0 Then
            Try
                tmpWSResult = Decrypt(CiBS_WS.DeleteAdminMessage(Encrypt(dgvAdminMesages.Rows(dgvAdminMesages.CurrentRow.Index).Cells(6).Value & "|" & Now.TimeOfDay.TotalSeconds)))
            Catch ex As Exception
                InsertLocalLog("DeleteAdminMessage- " & ex.Message)
                MessageBox.Show("خطا هنگام حذف پیام" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If tmpWSResult = "1" Then
                MessageBox.Show("پیام حدف گردید", "اعلان")
            Else
                MessageBox.Show("خطا هنگام حذف پیام", "خطا")
            End If
            UpdateGrid()
        End If

    End Sub

    Private Sub btnEnableMessage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnableMessage.Click
        Try
            tmpWSResult = Decrypt(CiBS_WS.ChangeAdminMessageStatus(Encrypt(dgvAdminMesages.Rows(dgvAdminMesages.CurrentRow.Index).Cells(6).Value & "|" & Now.TimeOfDay.TotalSeconds)))
        Catch ex As Exception
            InsertLocalLog("ChangeAdminMessageStatus- " & ex.Message)
            MessageBox.Show("خطا هنگام فعال سازی پبام" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        If tmpWSResult = "1" Then
            MessageBox.Show("وضعیت پیام تغییر یافت", "اعلان")
        Else
            MessageBox.Show("خطا هنگام تغییر وضعیت پیام", "خطا")
        End If
        UpdateGrid()
    End Sub


    Private Sub dgvAdminMesages_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvAdminMesages.Sorted
        For i As Integer = 0 To dgvAdminMesages.RowCount - 1
            dgvAdminMesages.Rows(i).Cells(0).Value = i + 1
        Next
    End Sub
End Class