Public Class InformUnUsableCard

    Dim tmpCards, tmpConSumables As String()


    Sub UpdateGrid()

        Try
            tmpWSResult = CiBS_WS.GetBranchStatckStatus(Encrypt(CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99"))
        Catch ex As Exception
            InsertLocalLog("GetBranchStatckStatus- " & ex.Message)
            MessageBox.Show("خطا هنگام بررسی وضعیت انبار شعبه" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim dtt As New DataTable("Cards")
        dtt.Columns.Add("نوع مواد مصرفی")
        dtt.Columns.Add("موجودی شعبه")

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
            dtt.Rows.Add(New Object() {tmp(0), tmp(1)})
        Next

        dgvStack.DataSource = dtt


    End Sub


    Private Sub InformNonUsableCard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'get cardTypes
        Try
            tmpWSResult = CiBS_WS.GetCardTypes(Encrypt("ACTIVE" & "|" & "99"))
        Catch ex As Exception
            InsertLocalLog("GetCardTypes- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت انواع کارت" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim tmpAns As String() = tmpWSResult.Split("!")

        If String.Compare(Decrypt(tmpAns(0)), "Timeout") = 0 Then
            MessageBox.Show("Timeout", "خطا")
            Exit Sub
        End If

        ReDim tmpCards(tmpAns.Length - 1)

        For i As Integer = 0 To tmpAns.Length - 2
            Dim tmp As String() = Decrypt(tmpAns(i)).Split("|")
            tmpCards(i) = tmp(0)
            cbCardTypes.Items.Add(tmp(1).Trim)
        Next

        tmpWSResult = ""
        'get Consumeables

        Try
            tmpWSResult = CiBS_WS.GetConsumables(Encrypt("2|" & Now.TimeOfDay.TotalSeconds))
        Catch ex As Exception
            InsertLocalLog("GetConsumables- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت لیست مواد مصرفی" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)


            Exit Sub
        End Try

        Dim tmpAns1 As String() = tmpWSResult.Split("!")

        ReDim tmpConSumables(tmpAns1.Length - 1)

        For i As Integer = 0 To tmpAns1.Length - 2
            Dim tmp1 As String() = Decrypt(tmpAns1(i)).Split("|")
            tmpConSumables(i) = tmp1(0)
            cbConsumables.Items.Add(tmp1(1).Trim)
        Next
        UpdateGrid()
    End Sub

    Private Sub btnInsertRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsertRecord.Click
        If cbCardTypes.SelectedIndex = -1 Then
            MessageBox.Show("ابتدا نوع کارت را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If cbErrorTypes.SelectedIndex = -1 Then
            MessageBox.Show("ابتدا نوع معیوبی را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If txtDesc.Text.Length < 4 Then
            MessageBox.Show(" شرح عیب را وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Try
            tmpWSResult = Decrypt(CiBS_WS.InsertUnusableCard(Encrypt(CurrentBranchCode & "|" & tmpCards(cbCardTypes.SelectedIndex) & "|" & cbErrorTypes.SelectedIndex & "|" & txtDesc.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate.ToString & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & Now.TimeOfDay.TotalSeconds)))
        Catch ex As Exception
            InsertLocalLog("InsertUnusableCard- " & ex.Message)
            MessageBox.Show("خطا هنگام اضافه نمودن کارت معیوب" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        ws_fb = tmpWSResult.Split("|")
        If String.Compare(ws_fb(0), "Timeout") = 0 Then
            MessageBox.Show("Timeout", "خطا")
            Exit Sub
        End If
        If String.Compare(ws_fb(0), "1") = 0 Then
            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Insert UnUsable Card" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Card Type NO " & tmpCards(cbCardTypes.SelectedIndex) & " with Error Type " & cbErrorTypes.SelectedIndex & " was inserted with description " & txtDesc.Text.Trim & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            MessageBox.Show("اعلام معیوبی کارت شما با موفقیت ارسال گردید", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information)
            UpdateGrid()
            txtDesc.Text = ""
            cbCardTypes.SelectedIndex = -1
            cbErrorTypes.SelectedIndex = -1
        Else
            MessageBox.Show(ws_fb(1), "خطا")
            Exit Sub
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        UpdateGrid()
    End Sub

    Private Sub btnInsertUsedConsumables_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsertUsedConsumables.Click
        If cbConsumables.SelectedIndex = -1 Then
            MessageBox.Show("ابتدا نوع مواد مصرفی را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If


        Try
            tmpWSResult = Decrypt(CiBS_WS.InsertUnusableCard(Encrypt(CurrentBranchCode & "|" & tmpConSumables(cbConsumables.SelectedIndex) & "|" & "0" & "|" & txtDescription.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate.ToString & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & Now.TimeOfDay.TotalSeconds)))
        Catch ex As Exception
            InsertLocalLog("InsertUnusableCard- " & ex.Message)
            MessageBox.Show("خطا هنگام اعلام مواد مصرفی مصرف شده" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)


            Exit Sub
        End Try

        ws_fb = tmpWSResult.Split("|")
        If String.Compare(ws_fb(0), "Timeout") = 0 Then
            MessageBox.Show("Timeout", "خطا")
            Exit Sub
        End If
        If String.Compare(ws_fb(0), "1") = 0 Then
            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Insert usage of consumables" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Consumable NO " & tmpConSumables(cbConsumables.SelectedIndex) & " was inserted with description " & txtDescription.Text.Trim & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            MessageBox.Show("اعلام استفاده مواد مصرفی شما با موفقیت ارسال گردید", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information)
            UpdateGrid()
            txtDescription.Text = ""
            cbConsumables.SelectedIndex = -1
        Else
            MessageBox.Show(ws_fb(1), "خطا")
            Exit Sub
        End If
    End Sub
End Class