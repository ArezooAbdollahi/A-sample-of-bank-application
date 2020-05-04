Public Class ConsumablesStack
    Dim tmpConsumables As String()
    Dim tmpCards As String()

    Private Sub ConsumablesStack_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''get Consumables
        'Try
        '    tmpWSResult = CiBS_WS.GetConsumables(Encrypt("1|" & Now.TimeOfDay.TotalSeconds))
        'Catch ex As Exception
        '    InsertLocalLog("GetConsumables- " & ex.Message)
        '    MessageBox.Show("خطا هنگام دریافت لیست مواد مصرفی" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        'Dim tmpAns As String() = tmpWSResult.Split("!")

        'If String.Compare(Decrypt(tmpAns(0)), "Timeout") = 0 Then
        '    MessageBox.Show("Timeout", "خطا")
        '    Exit Sub
        'End If

        'ReDim tmpConsumables(tmpAns.Length - 1)

        'For i As Integer = 0 To tmpAns.Length - 2
        '    Dim tmp As String() = Decrypt(tmpAns(i)).Split("|")
        '    tmpConsumables(i) = tmp(0)
        '    cbConsumables.Items.Add(tmp(1).Trim)
        'Next
        'UpdateGrid()
    End Sub

    Sub UpdateGrid()

        Try
            tmpWSResult = CiBS_WS.GetStackStatus(Encrypt(Now.TimeOfDay.TotalSeconds))
        Catch ex As Exception
            InsertLocalLog("GetStackStatus- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت وضعیت انبار" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim dtt As New DataTable("Cards")
        dtt.Columns.Add("نوع مواد مصرفی")
        dtt.Columns.Add("تعداد وارده به انبار")
        dtt.Columns.Add("تعداد صادره از انبار")
        dtt.Columns.Add("موجودی")

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
            dtt.Rows.Add(New Object() {tmp(0), tmp(1), tmp(2), tmp(3)})
        Next

        dgvStack.DataSource = dtt


    End Sub



    Private Sub btnInsertRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsertRecord.Click
        If cbConsumables.SelectedIndex = -1 Then
            MessageBox.Show("ابتدا مواد مصرفی مورد نظر را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If txtCosumableCount.Text.Length = 0 Then
            MessageBox.Show("مقدار مواد مصرفی مورد نظر را وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Try
            tmpWSResult = Decrypt(CiBS_WS.InsertConsumableRequest(Encrypt(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & txtCosumableCount.Text.Trim & "|" & tmpConsumables(cbConsumables.SelectedIndex) & "|" & DT.DateToPersian(Now.Date).ShortDate.ToString & "|0|" & Now.TimeOfDay.TotalSeconds)))
        Catch ex As Exception
            InsertLocalLog("InsertConsumableRequest- " & ex.Message)
            MessageBox.Show("خطا هنگام اضافه نمودن مواد مصرفی" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        ws_fb = tmpWSResult.Split("|")
        If String.Compare(ws_fb(0), "Timeout") = 0 Then
            MessageBox.Show("Timeout", "خطا")
            Exit Sub
        End If
        If String.Compare(ws_fb(0), "1") = 0 Then
            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Add Consumables to Stack" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Consumable NO " & tmpConsumables(cbConsumables.SelectedIndex) & " was Added for count " & txtCosumableCount.Text & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' MessageBox.Show("درخواست شما با موفقیت ارسال گردید", "اعلان")
            UpdateGrid()
            txtCosumableCount.Text = ""
            cbConsumables.SelectedIndex = -1
        Else
            MessageBox.Show(ws_fb(1), "خطا")
            Exit Sub
        End If
        UpdateGrid()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        UpdateGrid()
    End Sub

    Private Sub btnRemainAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemainAll.Click
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

    Private Sub btnRemainBranch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemainBranch.Click
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

        ''For i As Integer = 0 To tmpAns.Length - 2
        ''    Dim tmp As String() = Decrypt(tmpAns(i)).Split("|")
        ''    tmpCards(i) = tmp(0)
        ''    cbCardTypes.Items.Add(tmp(1).Trim)
        ''Next

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

        ReDim tmpConsumables(tmpAns1.Length - 1)

        For i As Integer = 0 To tmpAns1.Length - 2
            Dim tmp1 As String() = Decrypt(tmpAns1(i)).Split("|")
            tmpConsumables(i) = tmp1(0)
            cbConsumables.Items.Add(tmp1(1).Trim)
        Next
        UpdateGrid()
    End Sub
End Class