Imports System.IO

Public Class Reprint
    Dim trk1, trk2, trk3, cvv, exp, CrdStatus, tmpSheba As String
    Private Sub Reprint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblCardStatus.Text = ""
        If Not GetData() Then
            btnReprint.Enabled = False
            btnSendLog.Enabled = False
            If lblCardStatus.Text.Length = 0 Then lblCardStatus.Text = "لطفاً مجدداً وارد این فرم شوید"
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnSendLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendLog.Click
        Try
            If Not SetPrintedCardFlag(txtPAN.Text.Trim & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "3" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            InsertLocalLog("SetPrintedCardFlag- " & ex.Message)
            MessageBox.Show("خطا هنگام تغییر وضعیت کارت" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Inform Card Status" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Card NO " & txtPAN.Text & " status was informed" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        If MessageBox.Show("آیا این کارت بدرستی چاپ شده است؟", "اعلان", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Try
                If Not SetPrintedCardFlag(txtPAN.Text.Trim & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "1" & "|" & "1" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Inform Card Status" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Card NO " & txtPAN.Text & " status was set to PRINTED" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        GetData()
    End Sub

    Function MakeCard(ByVal PAN As String, ByVal Name As String, ByVal ExpDate As String, ByVal CVV2 As String, ByVal Branch As String, ByVal SpecialText As String, ByVal AccNo As String, ByVal Sheba As String) As Boolean
        If File.Exists("tmp.bmp") Then
            File.Delete("tmp.bmp")
        End If
        MakeCard = False
        Try
            Dim tmp As String() = PrintTemplate.Split("!")
            Dim tpan, tcvv2, tname, texp, tbranch, txt, tSpecialText, tLOGO, tAccNo, tSheba As String()
            Dim f1, f2, f3, f4, f5, f, f6, f7, f8 As Font
            tpan = tmp(0).Split(";")
            tcvv2 = tmp(1).Split(";")
            tname = tmp(2).Split(";")
            texp = tmp(3).Split(";")
            tbranch = tmp(4).Split(";")
            tSpecialText = tmp(5).Split(";")
            tLOGO = tmp(6).Split(";")
            tAccNo = tmp(7).Split(";")
            tSheba = tmp(8).Split(";")
            Dim t As String = ""
            For ii As Integer = 9 To tmp.Length - 1
                t = t + tmp(ii).ToString + ";"
            Next
            txt = t.Split(";")   'logo setting

            Dim x As New Bitmap(1016, 648, Imaging.PixelFormat.Format24bppRgb)
            Dim z As Graphics
            Dim Nformat As New System.Drawing.StringFormat
            z = Graphics.FromImage(x)
            z.Clear(Color.White)
            If tpan(7) = "True" Then
                f1 = New Font(tpan(1), tpan(2), FontStyle.Bold, GraphicsUnit.Millimeter)
            Else
                f1 = New Font(tpan(1), tpan(2), FontStyle.Regular, GraphicsUnit.Millimeter)
            End If

            If tcvv2(7) = "True" Then
                f2 = New Font(tcvv2(1), tcvv2(2), FontStyle.Bold, GraphicsUnit.Millimeter)
            Else
                f2 = New Font(tcvv2(1), tcvv2(2), FontStyle.Regular, GraphicsUnit.Millimeter)
            End If

            If tname(7) = "True" Then
                f3 = New Font(tname(1), tname(2), FontStyle.Bold, GraphicsUnit.Millimeter)
            Else
                f3 = New Font(tname(1), tname(2), FontStyle.Regular, GraphicsUnit.Millimeter)
            End If

            If texp(7) = "True" Then
                f4 = New Font(texp(1), texp(2), FontStyle.Bold, GraphicsUnit.Millimeter)
            Else
                f4 = New Font(texp(1), texp(2), FontStyle.Regular, GraphicsUnit.Millimeter)
            End If

            If tbranch(7) = "True" Then
                f5 = New Font(tbranch(1), tbranch(2), FontStyle.Bold, GraphicsUnit.Millimeter)
            Else
                f5 = New Font(tbranch(1), tbranch(2), FontStyle.Regular, GraphicsUnit.Millimeter)
            End If

            If tSpecialText(7) = "True" Then
                f6 = New Font(tSpecialText(1), tSpecialText(2), FontStyle.Bold, GraphicsUnit.Millimeter)
            Else
                f6 = New Font(tSpecialText(1), tSpecialText(2), FontStyle.Regular, GraphicsUnit.Millimeter)
            End If

            If tAccNo(7) = "True" Then
                f7 = New Font(tAccNo(1), tAccNo(2), FontStyle.Bold, GraphicsUnit.Millimeter)
            Else
                f7 = New Font(tAccNo(1), tAccNo(2), FontStyle.Regular, GraphicsUnit.Millimeter)
            End If

            If tSheba(7) = "True" Then
                f8 = New Font(tSheba(1), tSheba(2), FontStyle.Bold, GraphicsUnit.Millimeter)
            Else
                f8 = New Font(tSheba(1), tSheba(2), FontStyle.Regular, GraphicsUnit.Millimeter)
            End If

            If tpan(5) = "True" Then
                Nformat.Alignment = StringAlignment.Far
            Else
                Nformat.Alignment = StringAlignment.Near
            End If
            If tpan(8) = "True" Then
                Nformat.Alignment = StringAlignment.Center
            End If

            PAN = PAN.PadRight(16, "0")
            Dim tmpPAN As String = PAN.Substring(0, 4) & " " & PAN.Substring(4, 4) & " " & PAN.Substring(8, 4) & " " & PAN.Substring(12, 4)
            If tpan(6) = "True" Then z.DrawString(tmpPAN, f1, Brushes.Black, tpan(3), tpan(4), Nformat)

            If tcvv2(5) = "True" Then
                Nformat.Alignment = StringAlignment.Far
            Else
                Nformat.Alignment = StringAlignment.Near
            End If
            If tcvv2(8) = "True" Then
                Nformat.Alignment = StringAlignment.Center
            End If
            If tcvv2(6) = "True" Then z.DrawString(CVV2, f2, Brushes.Black, tcvv2(3), tcvv2(4), Nformat)

            If tname(5) = "True" Then
                Nformat.Alignment = StringAlignment.Far
            Else
                Nformat.Alignment = StringAlignment.Near
            End If
            If tname(8) = "True" Then
                Nformat.Alignment = StringAlignment.Center
            End If
            If tname(6) = "True" Then z.DrawString(Name, f3, Brushes.Black, tname(3), tname(4), Nformat)

            If texp(5) = "True" Then
                Nformat.Alignment = StringAlignment.Far
            Else
                Nformat.Alignment = StringAlignment.Near
            End If
            If texp(8) = "True" Then
                Nformat.Alignment = StringAlignment.Center
            End If
            If texp(6) = "True" Then z.DrawString(ExpDate, f4, Brushes.Black, texp(3), texp(4), Nformat)

            If tbranch(5) = "True" Then
                Nformat.Alignment = StringAlignment.Far
            Else
                Nformat.Alignment = StringAlignment.Near
            End If
            If tbranch(8) = "True" Then
                Nformat.Alignment = StringAlignment.Center
            End If
            If tbranch(6) = "True" Then z.DrawString(Branch, f5, Brushes.Black, tbranch(3), tbranch(4), Nformat)


            ''If tSpecialText(5) = "True" Then
            ''    Nformat.Alignment = StringAlignment.Far
            ''Else
            ''    Nformat.Alignment = StringAlignment.Near
            ''End If
            ''If tSpecialText(8) = "True" Then
            ''    Nformat.Alignment = StringAlignment.Center
            ''End If
            ''If tSpecialText(6) = "True" Then z.DrawString(SpecialText, f6, Brushes.Black, tSpecialText(3), tSpecialText(4), Nformat)


            If tAccNo(5) = "True" Then
                Nformat.Alignment = StringAlignment.Far
            Else
                Nformat.Alignment = StringAlignment.Near
            End If
            If tAccNo(8) = "True" Then
                Nformat.Alignment = StringAlignment.Center
            End If
            If tAccNo(6) = "True" Then z.DrawString(AccNo, f7, Brushes.Black, tAccNo(3), tAccNo(4), Nformat)


            If tSheba(5) = "True" Then
                Nformat.Alignment = StringAlignment.Far
            Else
                Nformat.Alignment = StringAlignment.Near
            End If
            If tSheba(8) = "True" Then
                Nformat.Alignment = StringAlignment.Center
            End If
            If tSheba(6) = "True" Then z.DrawString(Sheba, f8, Brushes.Black, tSheba(3), tSheba(4), Nformat)


            Dim i As Integer = txt.Length
            For j As Integer = 0 To i \ 7 - 1
                If txt(j * 7 + 6) = "True" Then
                    f = New Font(txt(j * 7 + 1), txt(j * 7 + 2), FontStyle.Bold, GraphicsUnit.Millimeter)
                Else
                    f = New Font(txt(j * 7 + 1), txt(j * 7 + 2), FontStyle.Regular, GraphicsUnit.Millimeter)
                End If
                z.DrawString(txt(j * 7 + 3), f, Brushes.Black, txt(j * 7 + 4), txt(j * 7 + 5))
            Next

            If tSpecialText(6) = "True" Then
                If tSpecialText(5) = "True" Then
                    Nformat.Alignment = StringAlignment.Far
                Else
                    Nformat.Alignment = StringAlignment.Near
                End If
                If tSpecialText(8) = "True" Then
                    Nformat.Alignment = StringAlignment.Center
                End If
                If tSpecialText(6) = "True" Then z.DrawString(SpecialText, f6, Brushes.Black, tSpecialText(3), tSpecialText(4), Nformat)
            End If


            'If IsLogoEnalbled And tLOGO(6) = "True" Then
            '    Dim newImage As Image = Image.FromFile(OpenFileDialog1.FileName.ToString)
            '    z.DrawImage(newImage, 50, 450)
            '    newImage.Dispose()
            'End If
            x = CNVRTClass.CopyToBpp(x, 1)
            x.Save("tmp.bmp", Imaging.ImageFormat.Bmp)
            MakeCard = True
            x.Dispose()
            z.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function


    Private Sub btnReprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReprint.Click
        Dim tmpAns2 As String
        Dim tmpWSResult2 As String
        If txtAccNo.Text.Trim.Length = 13 Then
            Try
                tmpWSResult2 = CiBS_WS.ShebaGenerator(Encrypt(txtAccNo.Text.Trim))
            Catch ex As Exception
                InsertLocalLog("Error Getting Status Card Data" & ex.Message)
                'ClearFieldsFlag = True
                MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            tmpAns2 = Decrypt(tmpWSResult2)
            Windows.Forms.Application.DoEvents()
            If tmpAns2.Contains("Error") Then
                MessageBox.Show("محاسبه شبا با مشکل مواجه گردید قبل از چاپ از صحت شماره حساب اطمینان حاصل فرمایید")
                Exit Sub
            End If

            If String.Compare(tmpAns2, "Timeout") = 0 Then
                MessageBox.Show("Timeout", "خطا")
                Exit Sub
            End If
            'tmpSheba = tmpAns2
            tmpSheba = tmpAns2.Substring(0, 4) & "-" & tmpAns2.Substring(4, 3) & "-" & tmpAns2.Substring(7, 6) & "-" & tmpAns2.Substring(13, 13)
        End If

        If CiBS_Module.CardPrintRee = True Then
            If CardPrint.btnPrint.Enabled = False Then
                MessageBox.Show("خطا هنگام ارتباط با چاپگر" & vbCrLf & "لطفاً در فرم چاپ کارت، چاپگر را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If
        If CiBS_Module.IrCardPrintRee = True Then
            If CardPrintRe.btnPrint.Enabled = False Then
                MessageBox.Show("خطا هنگام ارتباط با چاپگر" & vbCrLf & "لطفاً در فرم چاپ کارت، چاپگر را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        If (txtCardType.Text.ToString.Contains("هد") Or txtCardType.Text.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
            Try
                If txtAccNo.Text.Trim.Length > 6 Then
                    If Not MakeCard(txtPAN.Text.Trim, "کارت هدیه" & " " & txtAccNo.Text & " ریالی ", exp, cvv, CurrentBranchCode, txtSpecialText.Text.Trim, txtAccNo.Text.Trim, tmpSheba) Then
                        MessageBox.Show("خطا هنگام ساختن تصویر کارت", "خطا")
                        Try
                            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & txtPAN.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Error making Card Image" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            If Not SetPrintedCardFlag(txtPAN.Text.Trim & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Catch ex As Exception
                            InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                            MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                        Exit Try
                    End If
                Else
                    If Not MakeCard(txtPAN.Text.Trim, "", exp, cvv, CurrentBranchCode, txtSpecialText.Text.Trim, txtAccNo.Text.Trim, tmpSheba) Then
                        MessageBox.Show("خطا هنگام ساختن تصویر کارت", "خطا")
                        Try
                            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & txtPAN.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Error making Card Image" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            If Not SetPrintedCardFlag(txtPAN.Text.Trim & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Catch ex As Exception
                            InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                            MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                        Exit Try
                    End If
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, "خطا")
            End Try
        Else
            Try
                If Not MakeCard(txtPAN.Text.Trim, txtCustName.Text.Trim & " " & txtCustFamilyName.Text, exp, cvv, CurrentBranchCode, txtSpecialText.Text, txtAccNo.Text.Trim, tmpSheba) Then
                    MessageBox.Show("خطا هنگام ساختن تصویر کارت", "خطا")
                    Try
                        If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & txtPAN.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Error making Card Image" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        If Not SetPrintedCardFlag(txtPAN.Text.Trim & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                        MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    Exit Try
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "خطا")
            End Try
        End If
        Try
            tmpAns = CardPrint.MangePrinterPass(1, PrinterSerialNo)
            If Not tmpAns = "OK" Then
                MessageBox.Show("خطا هنگام فعال سازی چاپگر " & vbCrLf & tmpAns, "خطا")
                Exit Try
            End If


            If CardPrint.MagEncode(trk1, trk2, trk3) = "OK" Then
                Try
                    If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & txtPAN.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Mag Encoded Successfully" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    InsertLocalLog("InsertLog- " & ex.Message)
                    MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Try
                End Try

                If PrintName(txtPAN.Text.Trim) Then

                    Try
                        If Not SetPrintedCardFlag(txtPAN.Text.Trim & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "2" & "|" & "2" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then
                            MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Else
                            MessageBox.Show("کارت با موفقیت صادر گردید")
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try

                Else
                    Exit Try
                End If
            Else
                MessageBox.Show("خطا هنگام نوشتن در نوار مغناطیسی " & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Try
                    If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & txtPAN.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Error Encoding Mag-" & tmpAns & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    If Not SetPrintedCardFlag(txtPAN.Text.Trim & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                    MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                Exit Try
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطا")
        End Try
        GetData()

        '' tmpSheba = "IR08062000000" & txtAccNo.Text.Trim

        'Dim tmpAns2 As String
        'Dim tmpWSResult2 As String
        'Try
        '    tmpWSResult2 = CiBS_WS.ShebaGenerator(Encrypt(txtAccNo.Text.Trim))
        'Catch ex As Exception
        '    InsertLocalLog("Error Getting Status Card Data" & ex.Message)
        '    'ClearFieldsFlag = True
        '    MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try
        'tmpAns2 = Decrypt(tmpWSResult2)
        'Windows.Forms.Application.DoEvents()
        'If String.Compare(tmpAns, "Timeout") = 0 Then
        '    MessageBox.Show("Timeout", "خطا")
        '    'ClearFieldsFlag = True
        '    Exit Sub
        'End If
        '' tmpSheba = tmpAns2
        'tmpSheba = tmpAns2.Substring(0, 4) & "-" & tmpAns2.Substring(4, 3) & "-" & tmpAns2.Substring(7, 6) & "-" & tmpAns2.Substring(13, 12)

        'If CiBS_Module.CardPrintRee = True Then
        '    If CardPrint.btnPrint.Enabled = False Then
        '        MessageBox.Show("خطا هنگام ارتباط با چاپگر" & vbCrLf & "لطفاً در فرم چاپ کارت، چاپگر را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Exit Sub
        '    End If
        'End If
        'If CiBS_Module.IrCardPrintRee = True Then
        '    If CardPrintRe.btnPrint.Enabled = False Then
        '        MessageBox.Show("خطا هنگام ارتباط با چاپگر" & vbCrLf & "لطفاً در فرم چاپ کارت، چاپگر را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Exit Sub
        '    End If
        'End If

        'If (txtCardType.Text.ToString.Contains("هد") Or txtCardType.Text.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
        '    Try
        '        If txtAccNo.Text.Trim.Length > 6 Then
        '            If Not MakeCard(txtPAN.Text.Trim, "کارت هدیه" & " " & txtAccNo.Text & " ریالی ", exp, cvv, CurrentBranchCode, txtSpecialText.Text.Trim, txtAccNo.Text.Trim, tmpSheba) Then
        '                MessageBox.Show("خطا هنگام ساختن تصویر کارت", "خطا")
        '                Try
        '                    If Not InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & txtPAN.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Error making Card Image" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                    If Not SetPrintedCardFlag(txtPAN.Text.Trim  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                Catch ex As Exception
        '                    InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
        '                    MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                End Try
        '                Exit Try
        '            End If
        '        Else
        '            If Not MakeCard(txtPAN.Text.Trim, "", exp, cvv, CurrentBranchCode, txtSpecialText.Text.Trim, txtAccNo.Text.Trim, tmpSheba) Then
        '                MessageBox.Show("خطا هنگام ساختن تصویر کارت", "خطا")
        '                Try
        '                    If Not InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & txtPAN.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Error making Card Image" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                    If Not SetPrintedCardFlag(txtPAN.Text.Trim  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                Catch ex As Exception
        '                    InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
        '                    MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                End Try
        '                Exit Try
        '            End If
        '        End If

        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message, "خطا")
        '    End Try
        'Else
        '    Try
        '        If Not MakeCard(txtPAN.Text.Trim, txtCustName.Text.Trim & " " & txtCustFamilyName.Text, exp, cvv, CurrentBranchCode, txtSpecialText.Text, txtAccNo.Text.Trim, tmpSheba) Then
        '            MessageBox.Show("خطا هنگام ساختن تصویر کارت", "خطا")
        '            Try
        '                If Not InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & txtPAN.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Error making Card Image" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                If Not SetPrintedCardFlag(txtPAN.Text.Trim  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Catch ex As Exception
        '                InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
        '                MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            End Try
        '            Exit Try
        '        End If
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message, "خطا")
        '    End Try
        'End If
        'Try
        '    tmpAns = CardPrint.MangePrinterPass(1, PrinterSerialNo)
        '    If Not tmpAns = "OK" Then
        '        MessageBox.Show("خطا هنگام فعال سازی چاپگر " & vbCrLf & tmpAns, "خطا")
        '        Exit Try
        '    End If


        '    If CardPrint.MagEncode(trk1, trk2, trk3) = "OK" Then
        '        Try
        '            If Not InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & txtPAN.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Mag Encoded Successfully" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Catch ex As Exception
        '            InsertLocalLog("InsertLog- " & ex.Message)
        '            MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Exit Try
        '        End Try

        '        If PrintName(txtPAN.Text.Trim) Then

        '            Try
        '                If Not SetPrintedCardFlag(txtPAN.Text.Trim  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "2" & "|" & "2" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then
        '                    MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                Else
        '                    MessageBox.Show("کارت با موفقیت صادر گردید")
        '                End If
        '            Catch ex As Exception
        '                MessageBox.Show(ex.Message)
        '            End Try

        '        Else
        '            Exit Try
        '        End If
        '    Else
        '        MessageBox.Show("خطا هنگام نوشتن در نوار مغناطیسی " & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Try
        '            If Not InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & txtPAN.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Error Encoding Mag-" & tmpAns & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            If Not SetPrintedCardFlag(txtPAN.Text.Trim  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Catch ex As Exception
        '            InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
        '            MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        End Try
        '        Exit Try
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "خطا")
        'End Try
        'GetData()
    End Sub

    Function GetData() As Boolean
        Dim tat_tmp As String = ""
        Try
            tat_tmp = CiBS_WS.GetLastPrintedCardData(Encrypt(CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99"))
        Catch ex As Exception
            InsertLocalLog("GetLastPrintedCardData- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت وضعیت آخرین کارت صادره" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
        tmpWSResult = Nothing
        Try
            tmpWSResult = Decrypt(tat_tmp)
            If Not String.IsNullOrEmpty(tmpWSResult) Then ws_fb = tmpWSResult.Split("|")
            If ws_fb.Length = 2 And ws_fb(0).Contains("Error") Then
                InsertLocalLog("GetLastPrintedCardData- " & ws_fb(1))
                lblCardStatus.Text = "خطا - لطفاً مجدداً تلاش نمایید"
                Return False
            End If
            If ws_fb.Length = 16 Then
                txtPAN.Text = ws_fb(1).Trim
                txtAccNo.Text = ws_fb(8).Trim
                txtCustName.Text = ws_fb(10).Trim
                txtCustNo.Text = ws_fb(9).Trim
                txtCardType.Text = ws_fb(7).Trim
                trk1 = ws_fb(4).Trim
                trk2 = ws_fb(5).Trim
                trk3 = ws_fb(6).Trim
                cvv = ws_fb(3).Trim
                '/////////// NewRequest 
                If ws_fb(13) = 20 Then '.Contains("Latin") Or ws_fb(13).Contains("لاتین") Then
                    Dim tmpExpDate As String

                    Dim ss As String() = ws_fb(2).Trim.Split("/")
                    Dim ss1 As String
                    If ss(0) = 0 Then
                        ss1 = 1400
                    ElseIf ss(0) >= 97 And ss(0) <= 99 Then
                        ss1 = 13 & ss(0)
                    End If
                    Dim tt1 As String = ss1 & "/" & ss(1)
                    Dim tt2 As String() = tt1.Split("/")
                    Dim Day As String
                    If tt2(1) >= 1 And tt2(1) <= 6 Then
                        Day = 31
                    ElseIf tt2(1) >= 7 And tt2(1) <= 11 Then
                        Day = 30
                    Else
                        Day = 29
                    End If

                    tmpExpDate = Day & "/" & tt2(1) & "/" & tt2(0)
                    Dim georgianDateTime As DateTime = CnvShamciToMiladi.ToDateTime(tmpExpDate)
                    Dim strDateTimeLatin As String = georgianDateTime.ToString    '"7/23/2018 12:00:00 AM"
                    Dim tt3 As String = strDateTimeLatin.Substring(0, 9)
                    Dim tt4 As String() = tt3.Split("/")
                    Dim ExpDateLatin As String = tt4(2) & "/" & tt4(0)
                    exp = ExpDateLatin
                Else
                    Dim ss00 As String() = ws_fb(2).Trim.Split("/")
                    Dim ss11 As String
                    If ss00(0) = 0 Or ss00(0) = "00" Or ss00(0) = "0" Then
                        ss11 = 1400
                    ElseIf ss00(0) >= 97 And ss00(0) <= 99 Then
                        ss11 = 13 & ss00(0)
                    Else
                        ss11 = ss00(0)
                    End If
                    Dim tt11 As String = ss11 & "/" & ss00(1)
                    exp = tt11
                End If
                '//////////////////////
                'exp = ws_fb(2).Trim
                txtCustFamilyName.Text = ws_fb(12).Trim
                lblCardStatus.Text = ws_fb(11)
                txtSpecialText.Text = ws_fb(14)
                CrdStatus = ws_fb(15).Trim
            Else
                If ws_fb.Length = 1 And ws_fb(0) = "No Data" Then lblCardStatus.Text = "اطلاعاتی جهت نمایش وجود ندارد"
                Return False
            End If
            btnReprint.Enabled = False
        Catch ex As Exception
            InsertLocalLog("GetLastPrintedCardData-Analyze Message- " & ex.Message)
            MessageBox.Show("خطا هنگام بررسی پیام" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

        Try
            '/////// Masi gheire Faal Kard
            'Dim tt As String() = lblCardStatus.Text.Split("-")
            'Dim tt1 As String() = tt(1).Split(" ")

            If (UserGrantLevel = 1 And CrdStatus = "4") Or (UserGrantLevel = 1 And CrdStatus = "3") Then  '(UserGrantLevel = 1 And CrdStatus = "4" And tt1(1) = 0)
                btnReprint.Enabled = True
            Else
                btnReprint.Enabled = False
            End If

        Catch ex As Exception
            MessageBox.Show("Error Get Data: " & ex.Message)
        End Try

        Try
            If txtCardType.Text.ToString.Contains("هد") Or txtCardType.Text.ToString.ToLower.Contains("gift") Or txtCardType.Text.ToString.ToLower.Contains("ران کا") Then
                lblAccountNo.Text = "مبلغ کارت"
                lblCustName.Text = "نام خریدار"
                lblCustFamilyName.Text = "نام خانوادگی خریدار"
                lblCuno.Text = "تعداد درخواست"
            Else
                lblAccountNo.Text = "شماره حساب"
                lblCustName.Text = "نام مشتری"
                lblCustFamilyName.Text = "نام خانوادگی مشتری"
                lblCuno.Text = "شماره مشتری"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
        If UserGrantLevel = 1 And CrdStatus = "3" Then
            btnReprint.Enabled = True
        End If

        Try
            tmpWSResult = CiBS_WS.GetPrintTempalte(Encrypt(ws_fb(13).Trim & "|" & Now.TimeOfDay.TotalSeconds))
        Catch ex As Exception
            MessageBox.Show("خطا هنگام دریافت الگوی چاپ", "خطا")
            btnReprint.Enabled = False
            Return False
        End Try

        PrintTemplate = Decrypt(tmpWSResult)
        Dim tmpPrintTemplate As String() = PrintTemplate.Split("|")
        PrintTemplate = tmpPrintTemplate(0)

        If UserGrantLevel = 2 Then
            btnReprint.Enabled = True
        End If
        If CrdStatus <> "1" And CrdStatus <> "2" And CrdStatus <> "3" And CrdStatus <> "4" Then     ' no log sent to Center- reprint is disable
            btnReprint.Enabled = False
        End If
        If CrdStatus = "1" Or CrdStatus = "2" Or CrdStatus = "3" Or CrdStatus = "4" Then
            btnSendLog.Enabled = False
        End If
        If (CrdStatus = "3" Or CrdStatus = "4") And UserGrantLevel = 2 Then
            btnChangeStatus.Visible = True
        Else
            btnChangeStatus.Visible = False
        End If
        Return True
    End Function

    Function PrintName(ByVal CardNumber As String) As Boolean
        Dim tmp As String = ""
        If CardPrint.CheckClening(CurrentPrinter) > 1 Then
            If OpenPrinter(CurrentPrinter) > 0 Then
                tmp = KOprinting(hPrinter, "tmp.bmp", "varnish.bmp")
                If String.Compare(tmp, "Well Done") = 0 Then
                    If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNumber & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Card Printed Successfully" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClosePrinter(hPrinter)
                    Return True
                Else
                    MessageBox.Show("خطا هنگام چاپ کارت " & vbCrLf & tmp, "خطا")
                    If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNumber & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Error on Printing Card - " & tmp.Replace("Error-", "") & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    If Not SetPrintedCardFlag(CardNumber & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            Else
                MessageBox.Show("خطا هنگام برقراری ارتباط با چاپگر" & vbCrLf & tmp, "خطا")
                If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNumber & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Error on Printing Card - " & tmp.Replace("Error-", "") & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                If Not SetPrintedCardFlag(CardNumber & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Else
            If Not String.IsNullOrEmpty(tmp) Then
                MessageBox.Show("چاپگر شما نیاز به تمیزکاری دارد ", "خطا")
                If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNumber & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Error on Printing Card - Printer Needs Cleaning" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                If Not SetPrintedCardFlag(CardNumber & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            Else
                MessageBox.Show("خطا هنگام برقراری ارتباط با چاپگر", "خطا")
                Return False
            End If

        End If
    End Function

    Private Sub btnChangeStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeStatus.Click
        If MessageBox.Show("آیا این کارت بدرستی چاپ شده است؟", "اعلان", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            If SetPrintedCardFlag(txtPAN.Text.Trim & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "2" & "|" & "" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then
                If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Inform Card Status" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Card NO " & txtPAN.Text & " status was set to PRINTED" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                MessageBox.Show("وضعیت کارت با موفقیت تغییر یافت", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("خطا هنگام تغییر وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
        GetData()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'tmpSheba = "IR08062000000" & txtAccNo.Text.Trim
        Dim tmpAns2 As String
        Dim tmpWSResult2 As String
        Try
            tmpWSResult2 = CiBS_WS.ShebaGenerator(Encrypt(txtAccNo.Text.Trim))
        Catch ex As Exception
            InsertLocalLog("Error Getting Status Card Data" & ex.Message)
            'ClearFieldsFlag = True
            MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        tmpAns2 = Decrypt(tmpWSResult2)
        Windows.Forms.Application.DoEvents()
        If String.Compare(tmpAns, "Timeout") = 0 Then
            MessageBox.Show("Timeout", "خطا")
            'ClearFieldsFlag = True
            Exit Sub
        End If
        'tmpSheba = tmpAns2
        tmpSheba = tmpAns2.Substring(0, 4) & "-" & tmpAns2.Substring(4, 3) & "-" & tmpAns2.Substring(7, 6) & "-" & tmpAns2.Substring(13, 13)

        If CiBS_Module.CardPrintRee = True Then
            If CardPrint.btnPrint.Enabled = False Then
                MessageBox.Show("خطا هنگام ارتباط با چاپگر" & vbCrLf & "لطفاً در فرم چاپ کارت، چاپگر را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If
        If CiBS_Module.IrCardPrintRee = True Then
            If CardPrintRe.btnPrint.Enabled = False Then
                MessageBox.Show("خطا هنگام ارتباط با چاپگر" & vbCrLf & "لطفاً در فرم چاپ کارت، چاپگر را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        If (txtCardType.Text.ToString.Contains("هد") Or txtCardType.Text.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
            Try
                If txtAccNo.Text.Trim.Length > 6 Then
                    If Not MakeCard(txtPAN.Text.Trim, "کارت هدیه" & " " & txtAccNo.Text & " ریالی ", exp, cvv, CurrentBranchCode, txtSpecialText.Text.Trim, txtAccNo.Text.Trim, tmpSheba) Then
                        MessageBox.Show("خطا هنگام ساختن تصویر کارت", "خطا")
                        Try
                            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & txtPAN.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Error making Card Image" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            If Not SetPrintedCardFlag(txtPAN.Text.Trim & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Catch ex As Exception
                            InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                            MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                        Exit Try
                    End If
                Else
                    If Not MakeCard(txtPAN.Text.Trim, "", exp, cvv, CurrentBranchCode, txtSpecialText.Text.Trim, txtAccNo.Text.Trim, tmpSheba) Then
                        MessageBox.Show("خطا هنگام ساختن تصویر کارت", "خطا")
                        Try
                            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & txtPAN.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Error making Card Image" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            If Not SetPrintedCardFlag(txtPAN.Text.Trim & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Catch ex As Exception
                            InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                            MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                        Exit Try
                    End If
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, "خطا")
            End Try
        Else
            Try
                If Not MakeCard(txtPAN.Text.Trim, txtCustName.Text.Trim & " " & txtCustFamilyName.Text, exp, cvv, CurrentBranchCode, txtSpecialText.Text, txtAccNo.Text.Trim, tmpSheba) Then
                    MessageBox.Show("خطا هنگام ساختن تصویر کارت", "خطا")
                    Try
                        If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & txtPAN.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Error making Card Image" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        If Not SetPrintedCardFlag(txtPAN.Text.Trim & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                        MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    Exit Try
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "خطا")
            End Try
        End If
        Try
            tmpAns = CardPrint.MangePrinterPass(1, PrinterSerialNo)
            If Not tmpAns = "OK" Then
                MessageBox.Show("خطا هنگام فعال سازی چاپگر " & vbCrLf & tmpAns, "خطا")
                Exit Try
            End If


            If CardPrint.MagEncode(trk1, trk2, trk3) = "OK" Then
                Try
                    If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & txtPAN.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Mag Encoded Successfully" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    InsertLocalLog("InsertLog- " & ex.Message)
                    MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Try
                End Try

                If PrintName(txtPAN.Text.Trim) Then

                    Try
                        If Not SetPrintedCardFlag(txtPAN.Text.Trim & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "2" & "|" & "2" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then
                            MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Else
                            MessageBox.Show("کارت با موفقیت صادر گردید")
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try

                Else
                    Exit Try
                End If
            Else
                MessageBox.Show("خطا هنگام نوشتن در نوار مغناطیسی " & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Try
                    If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & txtPAN.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "REPRINT-Error Encoding Mag-" & tmpAns & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    If Not SetPrintedCardFlag(txtPAN.Text.Trim & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                    MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                Exit Try
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطا")
        End Try
        GetData()
    End Sub
End Class