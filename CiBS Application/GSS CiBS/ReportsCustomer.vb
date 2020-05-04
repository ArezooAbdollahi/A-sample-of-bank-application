Public Class ReportsCustomer


    Private Sub ReportDataGiftCard()
        Dim MelliCode As String = txtMelliCode.Text

        If txtMelliCode.Text.Trim = "" Then
            MelliCode = 0
        Else
            MelliCode = txtMelliCode.Text.Trim
        End If

        Try
            tmpWSResult = CiBS_WS.GetReportDataGiftCard(Encrypt(MelliCode & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99"))
        Catch ex As Exception
            InsertLocalLog("GetReportData- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت گزارش" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Exit Sub
        End Try

        Dim dtt As New DataTable("Cards")
        dtt.Columns.Add("ردیف")
        dtt.Columns.Add("کد ملی")
        dtt.Columns.Add("تاریخ تولد")
        dtt.Columns.Add("نام پدر")
        dtt.Columns.Add("آدرس")
        dtt.Columns.Add("تلفن")

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
            dtt.Rows.Add(New Object() {i + 1, tmp(1).Trim, tmp(2).Trim, tmp(3).Trim, tmp(4).Trim, tmp(5).Trim})
        Next
        dgvCards.DataSource = dtt
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ReportDataGiftCard()
    End Sub
End Class