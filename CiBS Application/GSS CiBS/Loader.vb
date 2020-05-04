Imports System.Data.OleDb

Public Class Loader
    Dim tmpCradType As String()
    Private Sub btnSelectInputFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectInputFile.Click
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.Filter = "Card Data (*.mdb)|*.mdb"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Title = "Please select input file"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.RestoreDirectory = True
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtFilePath.Text = OpenFileDialog1.FileName
            CheckInputFile()
        End If
    End Sub

    Public Structure ComboItem
        Public ProductCode As String
        Public TypeDesc As String
        Public TypeCode As String

        Public Overrides Function ToString() As String
            Return TypeDesc
        End Function
    End Structure

    Sub CheckInputFile()
        Dim accessConnection As New OleDbConnection()
        Try
            accessConnection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + OpenFileDialog1.FileName
            accessConnection.Open()
            Dim accessSelectCommand As New OleDbCommand()
            accessSelectCommand.CommandText = "select count(*) from main"
            accessSelectCommand.Connection = accessConnection
            txtRecordCount.Text = accessSelectCommand.ExecuteScalar
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطا")
        End Try
        accessConnection.Close()

    End Sub


    Private Sub btnInsertRecords_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsertRecords.Click
        If txtFilePath.Text.Length < 8 Then
            MessageBox.Show("Please select card type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If
        'If cbCardType.SelectedIndex = -1 And rbDebit.Checked = False Then
        '    MessageBox.Show("Please select input file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        '    Exit Sub
        'End If

        bgwLoad.RunWorkerAsync()

    End Sub

    Private Sub Loader_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' get card types
        ''Try
        ''    tmpWSResult = CiBS_WS.GetCardTypes(Encrypt("ACTIVE"))
        ''Catch ex As Exception
        ''    InsertLocalLog("GetCardTypes- " & ex.Message)
        ''    MessageBox.Show("خطا هنگام دریافت انواع کارت" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''    Exit Sub
        ''End Try
        ''Dim tmpAns As String() = tmpWSResult.Split("!")
        ''ReDim tmpCradType(tmpAns.Length - 1)

        ''For i As Integer = 0 To tmpAns.Length - 2
        ''    Dim tmp() As String = Decrypt(tmpAns(i)).Split("|")
        ''    tmpCradType(i) = tmp(0)
        ''    cbCardType.Items.Add(tmp(1).Trim)
        ''Next
        '********************************************************************
        Dim tmpSave As String()
        tmpWSResult = CiBS_WS.GetCardTypes(Encrypt("ACTIVE" & "|" & "99"))
        ' tmpWSResult = Decrypt(tmpWSResult)
        Dim tmpAnsPic As String() = tmpWSResult.Split("!")
        If String.Compare(tmpAnsPic(0), "No Data") = 0 Then
            MessageBox.Show("نوع کارتی یافت نشد", "خطا")
            Exit Sub
        End If
        Dim CardsRecordsPic(tmpAnsPic.Length - 2) As String
        For i As Integer = 0 To tmpAnsPic.Length - 2
            CardsRecordsPic(i) = Decrypt(tmpAnsPic(i))
        Next
        For i As Integer = 0 To CardsRecordsPic.Length - 1
            ReDim tmpSave(CardsRecordsPic.Length - 1)
            tmpSave = CardsRecordsPic(i).Split("|")

            Dim C As ComboItem
            C.TypeCode = LTrim(RTrim(tmpSave(0)))
            C.TypeDesc = tmpSave(1)
            C.ProductCode = tmpSave(2)
            cbCardType.Items.Add(C)
        Next


    End Sub

    Private Sub bgwLoad_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwLoad.DoWork
        Control.CheckForIllegalCrossThreadCalls = False
        PBload.Visible = True
        PBload.Maximum = CInt(txtRecordCount.Text)
        Dim i As Integer = 0
        Dim ErrCounter As Integer = 0

        If rbDebit.Checked = False Then
            Dim PAN, ExpDate, CVV2, TRACK1, TRACK2, TRACK3, Account As String
            Dim accessConnection As New OleDbConnection()
            Try
                accessConnection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + OpenFileDialog1.FileName
                accessConnection.Open()
                Dim accessSelectCommand As New OleDbCommand()
                accessSelectCommand.CommandText = "select * from main"
                accessSelectCommand.Connection = accessConnection
                Dim dr As OleDbDataReader
                dr = accessSelectCommand.ExecuteReader
                While dr.Read

                    Dim it As ComboItem = cbCardType.SelectedItem
                    If Not CInt(dr("ProductCode").trim) = it.ProductCode Then
                        MessageBox.Show("نوع کارت انتخابی با فرمت فایل ورودی تطابق ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If

                    'If (cbCardType.SelectedItem.ToString.Contains("نقد") Or cbCardType.SelectedItem.ToString.Contains("خانواده") Or cbCardType.SelectedItem.ToString.Contains("مشترک")) And Not CInt(dr("ProductCode").trim) = 10 Then
                    '    MessageBox.Show("نوع کارت انتخابی با فرمت فایل ورودی تطابق ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    Exit Sub
                    'End If
                    'If (cbCardType.SelectedItem.ToString.Contains("مجاز")) And Not CInt(dr("ProductCode").trim) = 12 Then
                    '    MessageBox.Show("نوع کارت انتخابی با فرمت فایل ورودی تطابق ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    Exit Sub
                    'End If
                    'If (cbCardType.SelectedItem.ToString.Contains("نما")) And Not CInt(dr("ProductCode").trim) = 28 Then
                    '    MessageBox.Show("نوع کارت انتخابی با فرمت فایل ورودی تطابق ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    Exit Sub
                    'End If
                    'If cbCardType.SelectedItem.ToString.Contains("هد") And Not CInt(dr("ProductCode").trim) = 30 Then
                    '    MessageBox.Show("نوع کارت انتخابی با فرمت فایل ورودی تطابق ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    Exit Sub
                    'End If

                    PBload.Value = i
                    lblInsertSequence.Text = "Inserting " & i & " of " & txtRecordCount.Text
                    i += 1
                    Me.Refresh()
                    PAN = dr("cardnoemb").ToString.Trim.Replace(" ", "")
                    CVV2 = dr("CVV2").ToString
                    ExpDate = dr("ExpDate").ToString.Trim
                    TRACK1 = "" 'dr("track1")
                    TRACK2 = dr("track2").ToString.Trim
                    TRACK3 = "" 'dr("track3")
                    Account = dr("Account").ToString.Trim

                    Dim tempNULL As String = "NULL"
                    Try
                        'tmpWSResult = Decrypt(CiBS_WS.InsertCardData(Encrypt("NULL" & "|" & tmpCradType(cbCardType.SelectedIndex) & "|" & PAN & "|" & ExpDate & "|" & CVV2 & "|" & TRACK1 & "|" & TRACK2 & "|" & TRACK3 & "|" & "NULL" & "|" & "NULL" & "|" & "NULL" & "|" & "NULL" & "|" & "NULL" & "|" & DT.DateToPersian(Now.Date).ShortDate & "|" & Now.TimeOfDay.TotalSeconds)))
                        'tmpWSResult = Decrypt(CiBS_WS.InsertCardData(Encrypt(Account & "|" & "NULL" & "|" & tmpCradType(cbCardType.SelectedIndex) & "|" & PAN & "|" & ExpDate & "|" & CVV2 & "|" & TRACK1 & "|" & TRACK2 & "|" & TRACK3 & "|" & "NULL" & "|" & "NULL" & "|" & "NULL" & "|" & "NULL" & "|" & "NULL" & "|" & DT.DateToPersian(Now.Date).ShortDate & "|" & Now.TimeOfDay.TotalSeconds)))

                        Dim it2 As ComboItem = cbCardType.SelectedItem
                        Dim TypeCode2 As String = it2.TypeCode
                        tmpWSResult = Decrypt(CiBS_WS.InsertCardData(Encrypt(Account & "|" & "NULL" & "|" & TypeCode2 & "|" & PAN & "|" & ExpDate & "|" & CVV2 & "|" & TRACK1 & "|" & TRACK2 & "|" & TRACK3 & "|" & "NULL" & "|" & "NULL" & "|" & "NULL" & "|" & "NULL" & "|" & "NULL" & "|" & DT.DateToPersian(Now.Date).ShortDate & "|" & Now.TimeOfDay.TotalSeconds)))

                        'tmpWSResult = Decrypt(CiBS_WS.InsertCardData(Encrypt(Account & "|" & tempNULL & "|" & tmpCradType(cbCardType.SelectedIndex) & "|" & PAN & "|" & ExpDate & "|" & CVV2 & "|" & TRACK1 & "|" & TRACK2 & "|" & TRACK3 & "|" & tempNULL & "|" & tempNULL & "|" & tempNULL & "|" & tempNULL & "|" & tempNULL & "|" & DT.DateToPersian(Now.Date).ShortDate & "|" & Now.TimeOfDay.TotalSeconds)))
                    Catch ex As Exception
                        InsertLocalLog("InsertCardData- " & ex.Message)
                        MessageBox.Show("خطا هنگام دریافت ورود اطلاعات کارت به بانک اطلاعاتی" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try

                    ws_fb = tmpWSResult.Split("|")
                    If Not String.Compare(ws_fb(0), "1") = 0 Then
                        If ws_fb(1).ToString.Contains("Violation of PRIMARY KEY constraint") Then
                            ErrCounter += 1
                        Else
                            MessageBox.Show(ws_fb(1), "خطا")
                            Exit Try
                        End If
                    End If
                End While
                MessageBox.Show("تعداد رکوردهای اضافه شده:" & CInt(txtRecordCount.Text) - ErrCounter & vbCrLf & "تعداد رکوردهای تکراری:" & ErrCounter)
                Me.Dispose()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "خطا")
            End Try
            accessConnection.Close()
            PBload.Visible = False

        End If

        'If rbDebit.Checked = True Then
        '    Dim PAN, ExpDate, CVV2, CustName, CustNumber, AccNo, BrCode, TRACK1, TRACK2, TRACK3 As String
        '    Dim accessConnection As New OleDbConnection()
        '    Try

        '        accessConnection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + OpenFileDialog1.FileName
        '        accessConnection.Open()
        '        Dim accessSelectCommand As New OleDbCommand()
        '        accessSelectCommand.CommandText = "select * from main"
        '        accessSelectCommand.Connection = accessConnection
        '        Dim dr As OleDbDataReader
        '        dr = accessSelectCommand.ExecuteReader
        '        While dr.Read
        '            If Not CInt(dr("ProductCode").trim) = 10 Then
        '                MessageBox.Show("نوع کارت انتخابی با فرمت فایل ورودی تطابق ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                Exit Sub
        '            End If
        '            PBload.Value = i
        '            lblInsertSequence.Text = "Inserting " & i & " of " & txtRecordCount.Text
        '            i += 1
        '            Me.Refresh()
        '            PAN = dr("cardnoemb").ToString.Trim.Replace(" ", "")
        '            CVV2 = dr("CVV2").ToString
        '            CustName = dr("NameEmb").ToString
        '            ExpDate = dr("ExpDate").ToString.Trim
        '            TRACK1 = ""
        '            TRACK2 = dr("track2").ToString.Trim
        '            TRACK3 = ""
        '            BrCode = dr("Cshobe")
        '            AccNo = dr("AccNumber")
        '            CustNumber = dr("CustomerNo")
        '            Try
        '                tmpWSResult = Decrypt(CiBS_WS.InsertCardData(Encrypt(BrCode & "|" & "1" & "|" & PAN & "|" & ExpDate & "|" & CVV2 & "|" & TRACK1 & "|" & TRACK2 & "|" & TRACK3 & "|" & AccNo & "|" & CustName & "|" & "NULL" & "|" & CustNumber & "|" & "NULL" & "|" & Now.TimeOfDay.TotalSeconds)))
        '            Catch ex As Exception
        '               MessageBox.Show("خطا هنگام ارتباط با مرکز" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                Exit Try
        '            End Try

        '            ws_fb = tmpWSResult.Split("|")
        '            If Not String.Compare(ws_fb(0), "1") = 0 Then
        '                If ws_fb(1).ToString.Contains("Violation of PRIMARY KEY constraint") Then
        '                    ErrCounter += 1
        '                Else
        '                    MessageBox.Show(ws_fb(1), "خطا")
        '                    Exit Try
        '                End If
        '            End If
        '        End While
        '        MessageBox.Show("تعداد رکوردهای اضافه شده:" & CInt(txtRecordCount.Text) - ErrCounter & vbCrLf & "تعداد رکوردهای تکراری:" & ErrCounter)
        '        Me.Dispose()
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message, "خطا")
        '    End Try
        '    accessConnection.Close()
        '    PBload.Visible = False
        'End If
    End Sub

End Class
