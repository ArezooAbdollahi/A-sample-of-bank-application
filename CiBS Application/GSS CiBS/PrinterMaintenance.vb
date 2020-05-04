Imports System.Drawing.Printing
Imports System.IO

Public Class PrinterMaintenance

    Private Sub PrinterManagement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim pkInstalledPrinters As String
        ' Find all printers installed
        For Each pkInstalledPrinters In _
            PrinterSettings.InstalledPrinters
            If pkInstalledPrinters.StartsWith("Evolis") Then cbPrinters.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters
        If cbPrinters.Items.Count > 0 Then
            cbPrinters.SelectedIndex = 0
        Else
            MessageBox.Show("هیچ چاپگر کارتی یافت نشد", "خطا")
        End If
    End Sub

    Private Sub cbPrinters_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrinters.SelectedIndexChanged
        CurrentPrinter = cbPrinters.SelectedItem.ToString
        CheckPrinterStatus()
        GetEncryptionKey()
    End Sub

    Sub CheckPrinterStatus()
        hPrinter = OpenPrinter(CurrentPrinter)
        If hPrinter > 0 Then
            gbFunctions.Enabled = True
            lblPrintingStatus.Text = "Ready"
            PrinterSerialNo = RunMyCommand(hPrinter, "Rsn")
            Dim RemainTillCleanig As Integer = 0
            Integer.TryParse(RunMyCommand(hPrinter, "Rco;rc"), RemainTillCleanig)
            If RemainTillCleanig < 1 Then
                lblCleaning.ForeColor = Color.Red
                lblCleaning.Text = "دارد"
                gbFunctions.Enabled = True
                btnCleaning.Enabled = True
                btnEject.Enabled = False
                btnTestCard.Enabled = False
                btnGeneralTest.Enabled = False
            Else
                lblCleaning.ForeColor = Color.Green
                lblCleaning.Text = "ندارد"
            End If

            If RemainTillCleanig = 1 Then
                lblCleaning.ForeColor = Color.Red
                lblCleaning.Text = "باید انجام شود"
                gbFunctions.Enabled = True
                btnCleaning.Enabled = True
                btnEject.Enabled = False
                btnTestCard.Enabled = False
                btnGeneralTest.Enabled = False

            End If

        Else
            gbFunctions.Enabled = False
            
            lblPrintingStatus.Text = "Not Ready"
            lblCleaning.Text = ""
        End If
        ClosePrinter(hPrinter)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        CheckPrinterStatus()
        GetEncryptionKey()
    End Sub

    Private Sub btnCleaning_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCleaning.Click
        hPrinter = OpenPrinter(CurrentPrinter)
        If hPrinter > 0 Then

            tmpAns = RunMyCommand(hPrinter, "Pem;2")
            If String.Compare(tmpAns, "OK") = 0 Then
                tmpAns = CardPrint.MangePrinterPass(1, PrinterSerialNo)
                If Not tmpAns = "OK" Then
                    MessageBox.Show("خطا هنگام فعال سازی چاپگر " & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                tmpAns = RunMyCommand(hPrinter, "Scp")
                If Not String.Compare(tmpAns, "OK") = 0 Then
                    MessageBox.Show(tmpAns, "خطا")
                End If
                If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Clean Printer" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Cleaning of Printer " & PrinterSerialNo & " done" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            RunMyCommand(hPrinter, "Pem;0")
            ClosePrinter(hPrinter)
        Else
            MessageBox.Show("خطا هنگام ارتباط با چاپگر", "خطا")
        End If
    End Sub

    Private Sub btnEject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEject.Click
        hPrinter = OpenPrinter(CurrentPrinter)
        If hPrinter > 0 Then

            tmpAns = RunMyCommand(hPrinter, "Pem;2")
            If String.Compare(tmpAns, "OK") = 0 Then
                tmpAns = CardPrint.MangePrinterPass(1, PrinterSerialNo)
                If Not tmpAns = "OK" Then
                    MessageBox.Show("خطا هنگام فعال سازی چاپگر " & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                tmpAns = RunMyCommand(hPrinter, "Ser")
                If Not String.Compare(tmpAns, "OK") = 0 Then
                    MessageBox.Show(tmpAns, "خطا")
                End If
            End If
            RunMyCommand(hPrinter, "Pem;0")
            ClosePrinter(hPrinter)
        Else
            MessageBox.Show("خطا هنگام ارتباط با چاپگر", "خطا")
        End If
    End Sub

    Private Sub btnTestCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestCard.Click
        hPrinter = OpenPrinter(CurrentPrinter)
        If hPrinter > 0 Then
            tmpAns = RunMyCommand(hPrinter, "Pem;2")
            If String.Compare(tmpAns, "OK") = 0 Then
                tmpAns = CardPrint.MangePrinterPass(1, PrinterSerialNo)
                If Not tmpAns = "OK" Then
                    MessageBox.Show("خطا هنگام فعال سازی چاپگر " & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                tmpAns = RunMyCommand(hPrinter, "St")
                If Not String.Compare(tmpAns, "OK") = 0 Then
                    MessageBox.Show(tmpAns, "خطا")
                End If
            End If
            RunMyCommand(hPrinter, "Pem;0")
            ClosePrinter(hPrinter)
        Else
            MessageBox.Show("خطا هنگام ارتباط با چاپگر", "خطا")
        End If
    End Sub
    'Function MakeCard(ByVal PAN As String, ByVal Name As String, ByVal ExpDate As String, ByVal CVV2 As String, ByVal Branch As String, ByVal SpecialText As String) As Boolean
    '    If File.Exists("tmp.bmp") Then
    '        File.Delete("tmp.bmp")
    '    End If
    '    MakeCard = False
    '    Try
    '        Dim tmp As String() = PrintTemplate.Split("!")
    '        Dim tpan, tcvv2, tname, texp, tbranch, txt As String()
    '        Dim f1, f2, f3, f4, f5, f As Font
    '        tpan = tmp(0).Split(";")
    '        tcvv2 = tmp(1).Split(";")
    '        tname = tmp(2).Split(";")
    '        texp = tmp(3).Split(";")
    '        tbranch = tmp(4).Split(";")
    '        Dim t As String = ""
    '        For ii As Integer = 5 To tmp.Length - 1
    '            t = t + tmp(ii).ToString + ";"
    '        Next
    '        txt = t.Split(";")   'logo setting

    '        Dim x As New Bitmap(1016, 648, Imaging.PixelFormat.Format24bppRgb)
    '        Dim z As Graphics
    '        Dim Nformat As New System.Drawing.StringFormat
    '        z = Graphics.FromImage(x)
    '        z.Clear(Color.White)
    '        If tpan(7) = "True" Then
    '            f1 = New Font(tpan(1), tpan(2), FontStyle.Bold, GraphicsUnit.Millimeter)
    '        Else
    '            f1 = New Font(tpan(1), tpan(2), FontStyle.Regular, GraphicsUnit.Millimeter)
    '        End If

    '        If tcvv2(7) = "True" Then
    '            f2 = New Font(tcvv2(1), tcvv2(2), FontStyle.Bold, GraphicsUnit.Millimeter)
    '        Else
    '            f2 = New Font(tcvv2(1), tcvv2(2), FontStyle.Regular, GraphicsUnit.Millimeter)
    '        End If

    '        If tname(7) = "True" Then
    '            f3 = New Font(tname(1), tname(2), FontStyle.Bold, GraphicsUnit.Millimeter)
    '        Else
    '            f3 = New Font(tname(1), tname(2), FontStyle.Regular, GraphicsUnit.Millimeter)
    '        End If

    '        If texp(7) = "True" Then
    '            f4 = New Font(texp(1), texp(2), FontStyle.Bold, GraphicsUnit.Millimeter)
    '        Else
    '            f4 = New Font(texp(1), texp(2), FontStyle.Regular, GraphicsUnit.Millimeter)
    '        End If

    '        If tbranch(7) = "True" Then
    '            f5 = New Font(tbranch(1), tbranch(2), FontStyle.Bold, GraphicsUnit.Millimeter)
    '        Else
    '            f5 = New Font(tbranch(1), tbranch(2), FontStyle.Regular, GraphicsUnit.Millimeter)
    '        End If

    '        If tpan(5) = "True" Then
    '            Nformat.Alignment = StringAlignment.Far
    '        Else
    '            Nformat.Alignment = StringAlignment.Near
    '        End If
    '        Dim tmpPAN As String = PAN.Substring(0, 4) & " " & PAN.Substring(4, 4) & " " & PAN.Substring(8, 4) & " " & PAN.Substring(12, 4)
    '        If tpan(6) = "True" Then z.DrawString(tmpPAN, f1, Brushes.Black, tpan(3), tpan(4), Nformat)

    '        If tcvv2(5) = "True" Then
    '            Nformat.Alignment = StringAlignment.Far
    '        Else
    '            Nformat.Alignment = StringAlignment.Near
    '        End If
    '        If tcvv2(6) = "True" Then z.DrawString(CVV2, f2, Brushes.Black, tcvv2(3), tcvv2(4), Nformat)

    '        If tname(5) = "True" Then
    '            Nformat.Alignment = StringAlignment.Far
    '        Else
    '            Nformat.Alignment = StringAlignment.Near
    '        End If
    '        If tname(6) = "True" Then z.DrawString(Name, f3, Brushes.Black, tname(3), tname(4), Nformat)

    '        If texp(5) = "True" Then
    '            Nformat.Alignment = StringAlignment.Far
    '        Else
    '            Nformat.Alignment = StringAlignment.Near
    '        End If
    '        If texp(6) = "True" Then z.DrawString(ExpDate, f4, Brushes.Black, texp(3), texp(4), Nformat)

    '        If tbranch(5) = "True" Then
    '            Nformat.Alignment = StringAlignment.Far
    '        Else
    '            Nformat.Alignment = StringAlignment.Near
    '        End If
    '        If tbranch(6) = "True" Then z.DrawString(Branch, f5, Brushes.Black, tbranch(3), tbranch(4), Nformat)

    '        Dim i As Integer = txt.Length
    '        For j As Integer = 0 To i \ 6 - 1
    '            If txt(j * 7 + 6) = "True" Then
    '                f = New Font(txt(j * 7 + 1), txt(j * 7 + 2), FontStyle.Bold, GraphicsUnit.Millimeter)

    '            Else
    '                f = New Font(txt(j * 7 + 1), txt(j * 7 + 2), FontStyle.Regular, GraphicsUnit.Millimeter)
    '            End If
    '            z.DrawString(txt(j * 7 + 3), f, Brushes.Black, txt(j * 7 + 4), txt(j * 7 + 5))
    '        Next
    '        If SpecialText.Length > 1 Then
    '            Dim f6 As New Font("IranNastaliq", 16, FontStyle.Bold, GraphicsUnit.Millimeter)
    '            Nformat.Alignment = StringAlignment.Center
    '            z.DrawString(SpecialText, f6, Brushes.Black, 508, 50, Nformat)
    '        End If

    '        x = CNVRTClass.CopyToBpp(x, 1)
    '        x.Save("tmp.bmp", Imaging.ImageFormat.Bmp)
    '        MakeCard = True
    '        x.Dispose()
    '        z.Dispose()
    '    Catch ex As Exception
    '    End Try
    'End Function

    Function MakeCard(ByVal PAN As String, ByVal Name As String, ByVal ExpDate As String, ByVal CVV2 As String, ByVal Branch As String, ByVal SpecialText As String) As Boolean
        If File.Exists("tmp.bmp") Then
            File.Delete("tmp.bmp")
        End If
        MakeCard = False
        Try
            Dim tmp As String() = PrintTemplate.Split("!")
            Dim tpan, tcvv2, tname, texp, tbranch, txt, tSpecialText, tLOGO As String()
            Dim f1, f2, f3, f4, f5, f, f6 As Font
            tpan = tmp(0).Split(";")
            tcvv2 = tmp(1).Split(";")
            tname = tmp(2).Split(";")
            texp = tmp(3).Split(";")
            tbranch = tmp(4).Split(";")
            tSpecialText = tmp(5).Split(";")
            tLOGO = tmp(6).Split(";")
            Dim t As String = ""
            For ii As Integer = 7 To tmp.Length - 1
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

            If tpan(5) = "True" Then
                Nformat.Alignment = StringAlignment.Far
            Else
                Nformat.Alignment = StringAlignment.Near
            End If
            If tpan(8) = "True" Then
                Nformat.Alignment = StringAlignment.Center
            End If
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

            Dim i As Integer = txt.Length
            For j As Integer = 0 To i \ 6 - 1
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
            x = CNVRTClass.CopyToBpp(x, 1)
            x.Save("tmp.bmp", Imaging.ImageFormat.Bmp)
            MakeCard = True
            x.Dispose()
            z.Dispose()
        Catch ex As Exception
        End Try
    End Function

    Private Sub btnGeneralTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGeneralTest.Click
        Try
            tmpWSResult = CiBS_WS.GetPrintTempalte(Encrypt("1" & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99"))
        Catch ex As Exception
            MessageBox.Show("خطا هنگام دریافت الگوی چاپ", "خطا")
            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Software & Hardware test" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Software and hardware test with printer  " & PrinterSerialNo & " done with this error while getting print template" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        PrintTemplate = Decrypt(tmpWSResult)
        If Not MakeCard("1234567890123456", CiBS_Parent.lblUserName.Text, CurrentUser, CurrentBranchCode, "1234", "") Then
            MessageBox.Show("خطا هنگام ساختن تصویر کارت", "خطا")
            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Software & Hardware test" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Software and hardware test with printer  " & PrinterSerialNo & " done with this error while making card image " & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        tmpAns = CardPrint.MangePrinterPass(1, PrinterSerialNo)
        If Not tmpAns = "OK" Then
            MessageBox.Show("خطا هنگام فعال سازی چاپگر " & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Software & Hardware test" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Software and hardware test with printer  " & PrinterSerialNo & " done with this error while enabling printer " & tmpAns & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        tmpAns = CardPrint.MagEncode("111111111", "222222222", "333333333")
        If tmpAns = "OK" Then
            Dim _hprn As Int32 = OpenPrinter(CurrentPrinter)
            If _hprn > 0 Then
                tmpAns = KOprinting(_hprn, "tmp.bmp", "varnish.bmp")
                If Not String.Compare(tmpAns, "Well Done") = 0 Then
                    MessageBox.Show("خطا هنگام چاپ کارت " & vbCrLf & tmpAns, "خطا")
                    If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Software & Hardware test" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Software and hardware test with printer  " & PrinterSerialNo & " done with this error " & tmpAns & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Software & Hardware test" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Software and hardware test with printer  " & PrinterSerialNo & " done successfully" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Else
            MessageBox.Show("خطا هنگام نوشتن در نوار مغناطیسی " & vbCrLf & tmpAns, "خطا")
            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Software & Hardware test" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Software and hardware test with printer  " & PrinterSerialNo & " done with this error while encoding mag with this error " & tmpAns & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Sub GetEncryptionKey()
        Try
            If lblPrintingStatus.Text = "Ready" Then
                RetEncryptionKey = GetEncryptionKeyFromPrinter().Substring(0, 16)
                If RetEncryptionKey.StartsWith("Error") Then
                    gbFunctions.Enabled = False
                    lblPrintingStatus.Text = "Security Problem"
                End If
            End If
        Catch ex As Exception
            lblPrintingStatus.Text = "Security Problem"
        End Try

    End Sub
    Function GetEncryptionKeyFromPrinter() As String
        Dim _hprn As Int32 = OpenPrinter(CurrentPrinter)
        Dim tmpRandomKey As String = ""
        Try
            tmpAns = RunMyCommand(_hprn, "Rand")
        Catch ex As Exception
            Return "Error-" & ex.Message.Trim
        End Try
        Dim t As String() = tmpAns.Split("")
        For i As Integer = 1 To 16 - t(0).Trim.Length
            tmpRandomKey = "0" & tmpRandomKey
        Next
        tmpRandomKey = tmpRandomKey & t(0).Trim

        tmpAns = RunMyCommand(_hprn, "RkeyTDES")
        Dim tmpKey As String() = tmpAns.Split("")
        If tmpKey(0).Length < 16 Then
            ClosePrinter(_hprn)
            Return "Error-Security Error"
        End If
        tmpKey = GSSdp.DecryptString(tmpKey(0), tmpRandomKey).Split("")
        ClosePrinter(_hprn)
        Return tmpKey(0)
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        hPrinter = OpenPrinter(CurrentPrinter)
        If hPrinter > 0 Then

            tmpAns = RunMyCommand(hPrinter, "Pem;2")
            If String.Compare(tmpAns, "OK") = 0 Then
                tmpAns = CardPrint.MangePrinterPass(1, PrinterSerialNo)
                If Not tmpAns = "OK" Then
                    MessageBox.Show("خطا هنگام فعال سازی چاپگر " & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                tmpAns = RunMyCommand(hPrinter, "Scp")
                If Not String.Compare(tmpAns, "OK") = 0 Then
                    MessageBox.Show(tmpAns, "خطا")
                End If
                If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Clean Printer" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Cleaning of Printer " & PrinterSerialNo & " done" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            RunMyCommand(hPrinter, "Pem;0")
            ClosePrinter(hPrinter)
        Else
            MessageBox.Show("خطا هنگام ارتباط با چاپگر", "خطا")
        End If

    End Sub
End Class