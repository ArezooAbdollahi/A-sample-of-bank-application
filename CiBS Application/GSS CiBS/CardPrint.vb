Imports AyandehPCPosDLL_Async
Imports System.Drawing.Printing
Imports System.Threading
Imports System.IO
Imports System.Text
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel


Public Class CardPrint
    Public btnPreviewClick As Boolean = False
    Public btnPrintClick As Boolean = False
    Public NameCustomer, FamilyCustomer, AccountNumber, CustomerNumber, CustomerNoGift, FisheVarizi, MobileNumber, IntCode As String
    Public CounterPrint As Integer = 0
    Dim CustNumber As String
    Dim Mablagh As String
    Dim GiftCard As Boolean = False
    Public EndThread As Boolean
    Dim PrintCount As Integer
    Dim tmpCmd As String
    Dim sn As String
    Dim Track1, Track2, Track3, CustName, CustFamilyName, CardNo, CVV, ExpDate, BrCode, PrintTemplate, sheba, AccNo As String
    Dim IsLogoEnalbled As Boolean = False
    Dim IsPrintAmountEnabled As Boolean = False
    Public ClearFieldsFlag As Boolean = False
    Dim tmpCradType As String()
    Public HasError, RespCode, RespMessage, Trace, Refrence, TrDate, TrTime, CardType, Terminal, Merchant, TrAmount, FollowUp As String
    Public CusNumber As Boolean
    Private Const _ENGLISH As String = "ENG"
    Private Const _PERS As String = "PERS"
    Dim fnt As Font = Nothing
    Dim isfntselected As Boolean = False

    Public Structure CardData
        Public CustName As String
        Public CustFamilyName As String
        Public AccountNo As String
        Public CustNo As String
        Public Iranian As Boolean
        Public OtherIranian As Boolean
        Public IntCode As String
        Public WithAmount As Boolean
        Public WithoutAmount As Boolean
        Public PrintGiftCardAmount As Boolean
        Public CardTypeItem As String
        Public CardTypeIndex As Integer
        Public SpecialText As String
        Public FishVarizi As String
        Public Mobile As String
        Public Gender As String
        

    End Structure

    Public cc As New CardData

    Private Sub TextBoxes_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustName.GotFocus, txtCustFamilyName.GotFocus, txtSpecialText.GotFocus, txtAccountNo.GotFocus, txtCustNo.GotFocus, txtIntCode.GotFocus
        Select Case sender.Tag
            Case _PERS
                For Each Lng As InputLanguage In InputLanguage.InstalledInputLanguages
                    If Lng.Culture.DisplayName.ToUpper.StartsWith(_PERS) Then
                        InputLanguage.CurrentInputLanguage = Lng
                        Exit For
                    End If
                Next
            Case _ENGLISH
                For Each Lng As InputLanguage In InputLanguage.InstalledInputLanguages
                    If Lng.Culture.DisplayName.ToUpper.StartsWith(_ENGLISH) Then
                        InputLanguage.CurrentInputLanguage = Lng
                        Exit For
                    End If
                Next
        End Select
    End Sub

    Private Sub CardPrint_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        If cbCardType.SelectedIndex <> -1 Then
            GetCardRemain()
        End If
    End Sub

    Sub GetPrinters()
        Try
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
        Catch ex As Exception
            InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error while getting printers list-" & ex.Message & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds)
            InsertLocalLog("GetPrintersList-" & ex.Message)
            MessageBox.Show("خطا هنگام دریافت لیست چاپگرها" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

    End Sub

    Sub GetCardTypes()
        ' MessageBox.Show("1")
        Try
            ' MessageBox.Show("2")
            tmpWSResult = CiBS_WS.GetCardTypes(Encrypt("ACTIVE" & "|" & "99"))
            Dim tmpAns As String() = tmpWSResult.Split("!")
            ReDim tmpCradType(tmpAns.Length - 1)
            Dim tt As String = Decrypt(tmpAns(0))
            ' MessageBox.Show("3" & tt)
            For i As Integer = 0 To tmpAns.Length - 2
                Dim tmp() As String = Decrypt(tmpAns(i)).Split("|")
                ' MessageBox.Show("5")
                If tmp(1).Contains("مجدد") Then
                Else
                    tmpCradType(i) = tmp(0)
                    cbCardType.Items.Add(tmp(1).Trim)
                End If
            Next
        Catch ex As Exception
            'MessageBox.Show("4")
            InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error while getting card types-" & ex.Message & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds)
            InsertLocalLog("GetCardTypes-" & ex.Message)
            MessageBox.Show("خطا هنگام دریافت انواع کارت" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If PosSerial = 0 Then
            RdbWithoutAmount.Checked = True
            RdbWithoutAmount.Enabled = True
            RdbWithoutAmount.Visible = True
            RdbAmount.Checked = False
            RdbAmount.Enabled = False
            RdbAmount.Visible = False

        Else
            RdbAmount.Visible = True
            RdbAmount.Enabled = True
            RdbAmount.Checked = True
            RdbWithoutAmount.Checked = False
            RdbWithoutAmount.Enabled = False
            RdbWithoutAmount.Visible = False
        End If
        GetPrinters()
        GetCardTypes()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        CheckPrinterStatus()
        ' GetEncryptionKey()
    End Sub

    Function MakeCard(ByVal PAN As String, ByVal Name As String, ByVal ExpDate As String, ByVal CVV2 As String, ByVal Branch As String, ByVal SpecialText As String, ByVal AccNo As String, ByVal Sheba As String) As Boolean
        If File.Exists("tmp.bmp") Then
            File.Delete("tmp.bmp")
        End If
        If File.Exists("tmpPreview.bmp") Then
            File.Delete("tmpPreview.bmp")
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

            If Not isfntselected Then
                If tSpecialText(7) = "True" Then
                    f6 = New Font(tSpecialText(1), tSpecialText(2), FontStyle.Bold, GraphicsUnit.Millimeter)
                Else
                    f6 = New Font(tSpecialText(1), tSpecialText(2), FontStyle.Regular, GraphicsUnit.Millimeter)
                End If
            Else
                f6 = fnt
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

            If IsLogoEnalbled And tLOGO(6) = "True" Then
                Dim newImage As Image = Image.FromFile("img.jpg")
                z.DrawImage(newImage, 50, 450)
                newImage.Dispose()
            End If
            x = CNVRTClass.CopyToBpp(x, 1)
            If btnPreviewClick = True Then
                x.Save("tmpPreview.bmp", Imaging.ImageFormat.Bmp)
            ElseIf btnPrintClick = True Then
                x.Save("tmp.bmp", Imaging.ImageFormat.Bmp)
            End If
            MakeCard = True
            x.Dispose()
            z.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub cbPrinters_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrinters.SelectedIndexChanged
        CurrentPrinter = cbPrinters.SelectedItem.ToString
        CheckPrinterStatus()
        'GetEncryptionKey()
    End Sub

    'Function GetEncryptionKeyFromPrinter() As String
    '    Dim _hprn As Int32 = OpenPrinter(CurrentPrinter)
    '    Dim tmpRandomKey As String = ""
    '    Try
    '        tmpAns = RunMyCommand(_hprn, "Rand")
    '    Catch ex As Exception
    '        Return "Error-" & ex.Message.Trim
    '    End Try
    '    Dim t As String() = tmpAns.Split("")
    '    For i As Integer = 1 To 16 - t(0).Trim.Length
    '        tmpRandomKey = "0" & tmpRandomKey
    '    Next
    '    tmpRandomKey = tmpRandomKey & t(0).Trim

    '    tmpAns = RunMyCommand(_hprn, "RkeyTDES")
    '    Dim tmpKey As String() = tmpAns.Split("")
    '    If tmpKey(0).Length < 16 Then
    '        ClosePrinter(_hprn)
    '        Return "Error-Security Error"
    '    End If
    '    tmpKey = GSSdp.DecryptString(tmpKey(0), tmpRandomKey).Split("")
    '    ClosePrinter(_hprn)
    '    Return tmpKey(0)
    'End Function

    Sub CheckPrinterStatus()
        Try
            hPrinter = OpenPrinter(CurrentPrinter)
            If hPrinter > 0 Then
                btnPrint.Enabled = True
                lblPrintingStatus.Text = "Ready"
                PrinterSerialNo = RunMyCommand(hPrinter, "Rsn")
                Dim RemainTillCleanig As Integer = 0
                Integer.TryParse(RunMyCommand(hPrinter, "Rco;rc"), RemainTillCleanig)
                If RemainTillCleanig < 1 Then
                    lblCleaning.ForeColor = Color.Red
                    lblCleaning.Text = "دارد"
                Else
                    lblCleaning.ForeColor = Color.Green
                    lblCleaning.Text = "ندارد"
                End If

                If RemainTillCleanig = 1 Then
                    lblCleaning.ForeColor = Color.Red
                    lblCleaning.Text = "باید انجام شود"
                    btnPrint.Enabled = False
                End If
            Else
                btnPrint.Enabled = False
                lblPrintingStatus.Text = "Not Ready"
                lblCleaning.Text = ""
            End If
            ClosePrinter(hPrinter)
        Catch ex As Exception
            InsertLocalLog("GetPrinterStatus-" & ex.Message)
            MessageBox.Show("خطا هنگام بررسی وضعیت چاپگر" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'Sub GetEncryptionKey()
    '    Try
    '        If lblPrintingStatus.Text = "Ready" Then
    '            RetEncryptionKey = GetEncryptionKeyFromPrinter().Substring(0, 16)
    '            If RetEncryptionKey.StartsWith("Error") Then
    '                btnPrint.Enabled = False
    '                lblPrintingStatus.Text = "Security Problem"
    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblPrintingStatus.Text = "Security Problem"
    '    End Try

    'End Sub



    'OLd ////////
    'Public Sub GetAmountFromPos()
    '    Dim pos_port, pos_ip, is_serial, pos_serial, time_out, terminal_order, Amount As String
    '    Dim pos_port1, is_serial1, time_out1, terminal_order1 As Integer
    '    Dim CardNo As String
    '    Dim tmpPosResult As String
    '    Dim tmp As String()
    '    Try
    '        tmpPosResult = (Decrypt(CiBS_WS.GetPosData(Encrypt(PosSerial))))
    '    Catch ex As Exception
    '        InsertLocalLog("CheckUser- " & ex.Message)
    '        MessageBox.Show("خطا هنگام گرفتن اطلاعات پوز" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End Try
    '    tmp = tmpPosResult.Split("|")
    '    pos_serial = tmp(0)
    '    pos_ip = tmp(1)
    '    pos_port = "8020"
    '    is_serial = "1"
    '    time_out = "1200"
    '    terminal_order = "1"

    '    pos_port = Convert.ToInt32(pos_port)
    '    is_serial = Convert.ToInt32(is_serial)
    '    time_out = Convert.ToInt32(time_out)
    '    terminal_order = Convert.ToInt32(terminal_order)

    '    Dim AyandehPcPos1 = New AyandehPcPos(pos_ip, pos_port, is_serial, pos_serial, time_out, terminal_order)

    '    'send data
    '    Dim m As String() = Mablagh.Split(",")
    '    For i As Integer = 0 To m.Length - 1
    '        Amount = Amount & m(i)
    '    Next
    '    If IsNumeric(Amount) = False Then
    '        MessageBox.Show("مبلغ را بصورت عددی وارد نمایید")
    '        RespMessage = "Enter Correct Amount"
    '        Exit Sub
    '    End If

    '    RespMessage = ""
    '    AyandehPcPos1.Sale(Amount)

    '    ' //receive data
    '    HasError = Convert.ToString(AyandehPcPos1.HasError)
    '    Trace = AyandehPcPos1.Trace
    '    Refrence = AyandehPcPos1.Refrence
    '    TrDate = AyandehPcPos1.TrDate
    '    TrTime = AyandehPcPos1.TrTime
    '    RespCode = AyandehPcPos1.RespCode
    '    RespMessage = AyandehPcPos1.RespMessage
    '    CardNo = AyandehPcPos1.CardNo
    '    CardType = AyandehPcPos1.CardType
    '    Terminal = AyandehPcPos1.Terminal
    '    Merchant = AyandehPcPos1.Merchant
    '    TrAmount = AyandehPcPos1.TrAmount
    '    FollowUp = Trace

    '    ' FollowUp & "|" & Refrence & "|" & TrDate & "|" & TrTime

    'End Sub


    'New ////
    Public Sub GetAmountFromPos()
        Dim pos_port, pos_ip, is_serial, pos_serial, time_out, terminal_order, Amount, paymentid, orgname, saledesc, PrintMessage As String
        'Dim pos_port1, is_serial1, time_out1, terminal_order1 As Integer
        Dim CardNo As String
        Dim tmpPosResult As String
        Dim tmp As String()
        Try
            tmpPosResult = (Decrypt(CiBS_WS.GetPosData(Encrypt(PosSerial & "|" & "99"))))
        Catch ex As Exception
            InsertLocalLog("CheckUser- " & ex.Message)
            MessageBox.Show("خطا هنگام گرفتن اطلاعات پوز" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        tmp = tmpPosResult.Split("|")
        pos_serial = tmp(0)        'OK
        pos_ip = tmp(1)         'OK
        pos_port = "8020"        'OK
        is_serial = "1"        'OK
        time_out = "1200"
        terminal_order = "10"

        pos_port = Convert.ToInt32(pos_port)
        is_serial = 0 ' Convert.ToInt32(is_serial)
        time_out = Convert.ToInt32(time_out)
        terminal_order = Convert.ToInt32(terminal_order)

        Dim AyandehPcPos1 = New AyandehPcPos(pos_ip, pos_port, is_serial, pos_serial)
        'Dim AyandehPcPos1 = New AyandehPcPos(pos_ip, pos_port, is_serial, pos_serial, time_out, terminal_order)


        'send data
        Dim m As String() = Mablagh.Split(",")
        For i As Integer = 0 To m.Length - 1
            Amount = Amount & m(i)
        Next
        If IsNumeric(Amount) = False Then
            MessageBox.Show("مبلغ را بصورت عددی وارد نمایید")
            RespMessage = "Enter Correct Amount"
            Exit Sub
        End If


        RespMessage = ""
        '' Dim ResultGetCounter As String = GetCounter()
        Dim IntCodeCustNo As String
        If txtCustNoGift.Text.Trim <> "" Or txtCustNoGift.Text.Trim <> " " Then
            IntCodeCustNo = txtCustNoGift.Text.Trim
        ElseIf txtIntCode.Text.Trim <> "" Or txtIntCode.Text.Trim <> " " Then
            IntCodeCustNo = txtIntCode.Text.Trim
        End If
        'paymentid = "K" & "0" & CurrentBranchCode.PadLeft(4, "0") & CurrentUser.PadLeft(5, "0") & ResultGetCounter & IntCodeCustNo
        paymentid = "K" & "0" & CurrentBranchCode.PadLeft(4, "0") & CurrentUser.PadLeft(5, "0") & IntCodeCustNo

        orgname = "GSS"
        saledesc = "لطفا منتظر بمانید"
        PrintMessage = "باموفقیت انتقال یافت"

        ' AyandehPcPos1.Sale(Amount)
        Dim ResultPcPos As SaleResult
        ResultPcPos = AyandehPcPos1.SaleNew(Amount, paymentid, orgname, saledesc, terminal_order, PrintMessage)


        ' //receive data
        HasError = Convert.ToString(ResultPcPos.HasError)          'OK
        Trace = ResultPcPos.Trace          'OK
        Refrence = ResultPcPos.Refrence          'OK
        TrDate = ResultPcPos.TrDate          'OK
        TrTime = ResultPcPos.TrTime          'OK
        RespCode = ResultPcPos.RespCode           'OK
        RespMessage = ResultPcPos.RespMessage          'OK
        CardNo = ResultPcPos.CardNo          'OK
        ' CardType = AyandehPcPos1.CardType
        Terminal = ResultPcPos.Terminal          'OK
        Merchant = ResultPcPos.Merchant          'OK
        TrAmount = ResultPcPos.TrAmount          'OK
        FollowUp = Trace

        ' FollowUp & "|" & Refrence & "|" & TrDate & "|" & TrTime

    End Sub

    Public Function GetCounter() As String
        Try
            tmpWSResult = CiBS_WS.GetCounter()
        Catch ex As Exception
            InsertLocalLog("Error Getting Counter Card Data" & ex.Message)
            ClearFieldsFlag = True
            MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        tmpAns = Decrypt(tmpWSResult)
        Windows.Forms.Application.DoEvents()
        If String.Compare(tmpAns, "Timeout") = 0 Then
            MessageBox.Show("Timeout", "خطا")
            ClearFieldsFlag = True
        End If

        If String.Compare(tmpAns, "ERROR") = 0 Then
            MessageBox.Show("ERROR" & " خطای دیتابیس", "خطا")
            ClearFieldsFlag = True
        End If

        Return (tmpAns)
    End Function


    Function IssueCard(ByVal crd As CardData) As Boolean
        ''   Try

        ''If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
        ''    If crd.WithAmount = True Or crd.WithoutAmount = True Then
        ''    Else
        ''        MessageBox.Show("لطفاً نوع مبلغ را مشخص نمایید(بامبلغ یا بدون مبلغ)", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''        ClearFieldsFlag = False
        ''        Exit Function
        ''    End If
        ''End If


        tmpAns = MangePrinterPass(1, PrinterSerialNo)
        If Not tmpAns = "OK" Then  'retry one more time to enable the printer
            tmpAns = MangePrinterPass(1, PrinterSerialNo)
        End If
        If Not tmpAns = "OK" Then
            InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error while enabling printer" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds)
            MessageBox.Show("خطا هنگام فعال سازی چاپگر " & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید" & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = True
            Exit Function
        End If

        For j As Integer = 1 To PrintCount
            '''''''''''' Masoumeh Check Last Card
            Try
                tmpWSResult = CiBS_WS.GetStatusLastCard(Encrypt(CurrentBranchCode))
            Catch ex As Exception
                InsertLocalLog("Error Getting Status Card Data" & ex.Message)
                ClearFieldsFlag = True
                MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit For
            End Try
            tmpAns = Decrypt(tmpWSResult)
            Windows.Forms.Application.DoEvents()
            If String.Compare(tmpAns, "Timeout") = 0 Then
                MessageBox.Show("Timeout", "خطا")
                ClearFieldsFlag = True
                Exit For
            End If

            If tmpAns.Length = 4 And tmpAns = "-999" Then
                MessageBox.Show("وضعیت آخرین کارت مشخص نمی باشد" & vbCrLf & "لطفاً به قسمت آخرین کارت صادره مراجعه نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ClearFieldsFlag = True
                Exit For
            End If

            If tmpAns.StartsWith("Unexpected Error") Then
                MessageBox.Show("بروز خطای ناشناخته" & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit For
            End If
            If tmpAns.Length = 4 And tmpAns = "-111" Then
                If GiftCard = True Then   'نوع کارت هدیه
                    If RdbAmount.Checked = True Then
                        FollowUp = ""
                        Refrence = ""
                        TrDate = ""
                        TrTime = ""
                        GetAmountFromPos()  ''Masoumeh
                        If RespMessage = "OK" Then

                            '/////////////
                            'Try
                            '    tmpWSResult = Decrypt(CiBS_WS.InsertPosUsedData(Encrypt(FollowUp & "|" & Refrence & "|" & TrDate & "|" & TrTime)))
                            'Catch ex As Exception
                            '    MessageBox.Show("خطا جهت کار پوز -" & RespMessage)
                            '    Exit For
                            'End Try
                            'ws_fb = tmpWSResult.Split("|")
                            'If String.Compare(ws_fb(0), "Timeout") = 0 Then
                            '    MessageBox.Show("Time Out خطا جهت کار پوز -" & RespMessage)
                            '    Exit For
                            'End If

                            'If String.Compare(ws_fb(0), "1") = 0 Then

                            'Else
                            '    MessageBox.Show(ws_fb(0) & " - InsertPosUsedData خطا جهت کار پوز -" & RespMessage)
                            '    Exit For
                            'End If
                            '/////////////
                        Else
                            MessageBox.Show("خطا جهت کار پوز -" & RespMessage)
                            Exit For
                        End If
                    End If
                End If
            End If

            '''''''''''''''''''''''''''''''''''''
            Try
                If crd.CardTypeItem.Contains("ران کا") Then
                    tmpWSResult = CiBS_WS.GetCardDataIranCard(Encrypt(CurrentBranchCode & "|" & crd.AccountNo & "|" & crd.CustName & "|" & crd.CustNo & "|" & crd.IntCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & tmpCradType(crd.CardTypeIndex) & "|" & crd.CustFamilyName & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & crd.SpecialText & "|" & Now.TimeOfDay.TotalSeconds & "|" & crd.FishVarizi & "|" & crd.Mobile))
                ElseIf (crd.CardTypeItem.Contains("gift") Or crd.CardTypeItem.Contains("هد")) Then
                    tmpWSResult = CiBS_WS.GetCardDataGift(Encrypt(CurrentBranchCode & "|" & crd.AccountNo & "|" & crd.CustName & "|" & crd.CustNo & "|" & crd.IntCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & tmpCradType(crd.CardTypeIndex) & "|" & crd.CustFamilyName & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & crd.SpecialText & "|" & Now.TimeOfDay.TotalSeconds & "|" & FollowUp & "|" & Refrence & "|" & TrDate & "|" & TrTime & "|" & "99"))
                Else

                End If
            Catch ex As Exception
                InsertLocalLog("Error Getting Card Data" & ex.Message)
                ClearFieldsFlag = True
                MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit For
            End Try
            tmpAns = Decrypt(tmpWSResult)
            Windows.Forms.Application.DoEvents()
            If String.Compare(tmpAns, "Timeout") = 0 Then
                MessageBox.Show("Timeout", "خطا")
                ClearFieldsFlag = True
                Exit For
            End If

            If tmpAns.Length = 4 And tmpAns = "-999" Then
                MessageBox.Show("وضعیت آخرین کارت مشخصی نمی باشد" & vbCrLf & "لطفاً به قسمت آخرین کارت صادره مراجعه نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ClearFieldsFlag = True
                Exit For
            End If

            If tmpAns = "No Data" Then
                MessageBox.Show("از این نوع کارت اطلاعاتی جهت دریافت از مرکز موجود نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                ClearFieldsFlag = False
                Exit For
            End If

            If tmpAns.StartsWith("Unexpected Error") Then
                MessageBox.Show("بروز خطای ناشناخته" & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit For
            End If

            Dim tmp As String()
            ReDim tmp(tmpAns.Length - 1)
            tmp = tmpAns.Split("|")
            If tmp(8).Trim <> "5" Then
                MessageBox.Show("خطای دیتابیس ، لطفا جهت چاپ کارت، دوباره تلاش نمایید (درصورت نیاز آخرین کارت صادره را نیز بررسی نمایید)", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ClearFieldsFlag = False
                Exit For
            End If
            If tmp.Length <> 9 Then
                InsertLocalLog("Error Geting Data")
                MessageBox.Show("خطا هنگام دریافت دیتا از دیتابیس " & vbCrLf & "مجددا تلاش نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit For
            End If

            Track1 = tmp(4)
            Track2 = tmp(5)
            Track3 = tmp(6)
            CardNo = tmp(1)
            CustName = crd.CustName
            ExpDate = tmp(2)
            CVV = tmp(3)
            BrCode = CurrentBranchCode
            CustFamilyName = crd.CustFamilyName

            If Track2.Trim = "" Then
                MessageBox.Show("از این نوع کارت اطلاعاتی جهت دریافت از مرکز موجود نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                ClearFieldsFlag = False
                Exit For
            End If

            '  AccNo =

            Dim tmpAns2 As String
            Dim tmpWSResult2 As String
            Try
                tmpWSResult2 = CiBS_WS.ShebaGenerator(Encrypt(AccNo))
            Catch ex As Exception
                InsertLocalLog("Error Getting Status Card Data" & ex.Message)
                ClearFieldsFlag = True
                MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit For
            End Try
            tmpAns2 = Decrypt(tmpWSResult2)
            Windows.Forms.Application.DoEvents()
            If String.Compare(tmpAns, "Timeout") = 0 Then
                MessageBox.Show("Timeout", "خطا")
                ClearFieldsFlag = True
                Exit For
            End If

            sheba = tmpAns2.Substring(0, 4) & "-" & tmpAns2.Substring(4, 3) & "-" & tmpAns2.Substring(7, 6) & "-" & tmpAns2.Substring(13, 13)

            If (crd.CardTypeItem.Contains("هد") Or crd.CardTypeItem.ToLower.Contains("gift")) Then   'نوع کارت هدیه

                'If crd.WithAmount = True Then
                '    GetAmountFromPos()  ''Masoumeh
                '    If RespMessage = "OK" Then

                '    Else
                '        MessageBox.Show("خطا جهت کار پوز -" & RespMessage)
                '        Exit Function
                '    End If
                'End If



                If crd.PrintGiftCardAmount = True Then
                    If Not MakeCard(CardNo, "کارت هدیه" & " " & crd.AccountNo & " ریالی ", ExpDate, CVV, BrCode, crd.SpecialText, AccNo, sheba) Then
                        MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
                        Try
                            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            If Not SetPrintedCardFlag(CardNo & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Catch ex As Exception
                            InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                            MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                        ClearFieldsFlag = True
                        Exit For
                    End If
                Else
                    If Not MakeCard(CardNo, "", ExpDate, CVV, BrCode, crd.SpecialText, AccNo, sheba) Then
                        MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
                        Try
                            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            If Not SetPrintedCardFlag(CardNo & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Catch ex As Exception
                            InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                            MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                        ClearFieldsFlag = True
                        Exit For
                    End If
                End If

            ElseIf crd.CardTypeItem.Contains("ران کا") Then
                ' If Not MakeCard(CardNo, "کارت هدیه" & " " & txtAccountNo.Text & " ریالی ", ExpDate, CVV, BrCode, txtSpecialText.Text.Trim) Then
                If Not MakeCard(CardNo, CustName & " " & CustFamilyName, ExpDate, CVV, BrCode, crd.SpecialText, AccNo, sheba) Then
                    MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
                    Try
                        If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        If Not SetPrintedCardFlag(CardNo & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                        MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    ClearFieldsFlag = True
                    Exit For
                End If
            Else
                If Not MakeCard(CardNo, CustName & " " & CustFamilyName, ExpDate, CVV, BrCode, crd.SpecialText, AccNo, sheba) Then
                    MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
                    Try
                        If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        If Not SetPrintedCardFlag(CardNo & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                        MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    ClearFieldsFlag = True
                    Exit For
                End If
            End If
            Windows.Forms.Application.DoEvents()
            ''  lblPrintStatus.Text = "Printing " & j & " of " & PrintCount
            pbPreview.ImageLocation = "tmp.bmp"
            pbPreview.Load()
            '' Me.Refresh()

            tmpAns = MagEncode(Track1, Track2, Track3)
            Windows.Forms.Application.DoEvents()
            If tmpAns = "OK" Then
                Try
                    If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Mag Encoded Successfully" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    InsertLocalLog("InsertLog- " & ex.Message)
                    MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = True
                    Exit For
                End Try
                Windows.Forms.Application.DoEvents()
                If PrintName(CardNo) Then
                    Windows.Forms.Application.DoEvents()
                    Try
                        If Not SetPrintedCardFlag(CardNo & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "1" & "|" & "1" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then
                            MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Else
                            If PrintCount = 1 Then MessageBox.Show("کارت با موفقیت صادر گردید", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Catch ex As Exception
                        InsertLocalLog("SetPrintedCardFlag- " & ex.Message)
                        MessageBox.Show("خطا هنگام تغییر وضعیت کارت" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = True
                        Exit For
                    End Try

                    ClearFieldsFlag = True
                    '   ClearFeilds()

                Else
                    ClearFieldsFlag = True
                    Exit For
                End If
            Else
                If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error Encoding Mag-" & tmpAns.Replace("Error-", "") & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                If Not SetPrintedCardFlag(CardNo & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                MessageBox.Show("خطا هنگام نوشتن اطلاعات در نوار مغناطیسی " & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید" & vbCrLf & tmpAns.Replace("Error-", ""), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = True
            End If
            pbPreview.ImageLocation = ""
            lblPrintStatus.Text = ""
            ClearFieldsFlag = True

            pbPreview.ImageLocation = ""
        Next

        EndThread = True

        ''Catch ex As Exception
        ''    InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error while issuing card -" & ex.Message & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds)
        ''    InsertLocalLog("Error in issuing Card-" & ex.Message)
        ''End Try
    End Function

    Sub GetExistCustomerNumber()
        Try
            tmpWSResult = CiBS_WS.GetExistCustomer(Encrypt(txtIntCode.Text.Trim))
            Dim tmp As String = Decrypt(tmpWSResult)

            If tmp.Contains("No Data") Then
                CusNumber = False
            ElseIf tmp.Contains("Error") Then
                CusNumber = False
            Else
                CusNumber = True
            End If

            'MessageBox.Show("12-" & tmp & CusNumber)
            ''Dim tmpAns As String() = tmpWSResult.Split("!")
            ''ReDim tmpCradType(tmpAns.Length - 1)
            ''For i As Integer = 0 To tmpAns.Length - 2
            ''    Dim tmp() As String = Decrypt(tmpAns(i)).Split("|")
            ''    tmpCradType(i) = tmp(0)
            ''    cbCardType.Items.Add(tmp(1).Trim)
            ''Next
        Catch ex As Exception
            InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error while getting card types-" & ex.Message & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds)
            InsertLocalLog("GetExistCustomerNumber-" & ex.Message)
            MessageBox.Show("خطا هنگام چک کردن شماره مشتری داخلی" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub


    Private Sub PrintedAllCards()
        Try
            EndThread = False
            Dim crd As CardData = cc
            tmpAns = MangePrinterPass(1, PrinterSerialNo)
            If Not tmpAns = "OK" Then  'retry one more time to enable the printer
                tmpAns = MangePrinterPass(1, PrinterSerialNo)
            End If
            If Not tmpAns = "OK" Then
                InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error while enabling printer" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds)
                MessageBox.Show("خطا هنگام فعال سازی چاپگر " & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید" & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = True
                'EndThread = True
                Exit Sub
            End If

            For j As Integer = 1 To PrintCount

                ''''''''''''' Faal kon
                ''''''''' چک کردن شماره مشتری
                If (crd.CardTypeItem.Contains("هد") Or crd.CardTypeItem.Contains("gift")) Then
                    If j = 1 Then
                        If CusNumber = False Then
                            tmpWSResult = ""
                            'Try
                            '    tmpWSResult = CiBS_WS.GetCustomerInfo(Encrypt(CustNumber.Trim & "|" & Now.TimeOfDay.TotalSeconds))
                            'Catch ex As Exception
                            '    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            '    ClearFieldsFlag = False
                            '    Exit Sub
                            'End Try
                            'Dim tmpAns As String = ""
                            'tmpAns = Decrypt(tmpWSResult)
                            'Dim ResultGetCustom As String = Decrypt(tmpWSResult)
                            'Dim ResultGetCustom1 As String() = ResultGetCustom.Split("|")
                            ''///////
                            'If String.Compare(ResultGetCustom1(0), "Timeout") = 0 Then
                            '    MessageBox.Show("هنگام بررسی شماره مشتری خطای Timeout ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            '    ClearFieldsFlag = False
                            '    Exit Sub
                            'End If

                            'If tmpAns.Contains("No Data") Then
                            '    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد. خطای NO DATA ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            '    ClearFieldsFlag = False
                            '    Exit Sub
                            'End If
                            'If tmpAns.Contains("Unexpected Error") Then
                            '    MessageBox.Show("بررسی شماره مشتری وارد شده خطای Unexpected Error ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            '    ClearFieldsFlag = False
                            '    Exit Sub
                            'End If

                            ''//////
                            'If tmpAns.Contains("0") Then
                            '    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            '    ClearFieldsFlag = False
                            '    Exit Sub
                            'End If
                            'CusNumber = True
                        End If
                    End If
                    ''''''''
                    If (j = 1 And PrintCount > 1) Then
                        txtCustName.Enabled = False
                        txtCustFamilyName.Enabled = False
                        txtAccountNo.Enabled = False
                        txtCustNo.Enabled = False
                        txtCustNoGift.Enabled = False
                        txtIntCode.Enabled = False
                    ElseIf (j = PrintCount Or PrintCount = 1) Then
                        txtCustName.Enabled = True
                        txtCustFamilyName.Enabled = True
                        txtAccountNo.Enabled = True
                        txtCustNo.Enabled = True
                        txtCustNoGift.Enabled = True
                        txtIntCode.Enabled = True
                    End If
                End If


                '''''''''''' Masoumeh Check Last Card
                Try
                    tmpWSResult = CiBS_WS.GetStatusLastCard(Encrypt(CurrentBranchCode))
                Catch ex As Exception
                    InsertLocalLog("Error Getting Status Card Data" & ex.Message)
                    ClearFieldsFlag = True
                    MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit For
                End Try
                tmpAns = Decrypt(tmpWSResult)
                Windows.Forms.Application.DoEvents()
                If String.Compare(tmpAns, "Timeout") = 0 Then
                    MessageBox.Show("Timeout", "خطا")
                    ClearFieldsFlag = True
                    Exit For
                End If

                If tmpAns = "-999" Then 'tmpAns.Length = 4 And
                    MessageBox.Show("وضعیت آخرین کارت مشخصی نمی باشد" & vbCrLf & "لطفاً به قسمت آخرین کارت صادره مراجعه نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    ClearFieldsFlag = True
                    Exit For
                End If

                If tmpAns.StartsWith("Unexpected Error") Then
                    MessageBox.Show("بروز خطای ناشناخته" & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit For
                End If
                If tmpAns = "-111" Then 'tmpAns.Length = 4 And
                    If GiftCard = True Then   'نوع کارت هدیه
                        If RdbAmount.Checked = True Then
                            FollowUp = ""
                            Refrence = ""
                            TrDate = ""
                            TrTime = ""
                            RespCode = ""
                            RespMessage = ""
                            CardNo = ""
                            CardType = ""
                            Terminal = ""
                            Merchant = ""
                            TrAmount = ""
                            RespMessage = ""
                            GetAmountFromPos()  ''Masoumeh
                            If RespMessage = "OK" Then

                            Else
                                ''''''''''''' Faal kon
                                MessageBox.Show("خطا جهت کار پوز -" & RespMessage)
                                Exit For
                            End If
                        End If
                    End If
                End If

                Try
                    ''''@arezoo: make change
                    If crd.CardTypeItem.Contains("ران کا") Then
                        tmpWSResult = CiBS_WS.GetCardDataIranCard(Encrypt(CurrentBranchCode & "|" & crd.AccountNo & "|" & crd.CustName & "|" & crd.CustNo & "|" & crd.IntCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & tmpCradType(crd.CardTypeIndex) & "|" & crd.CustFamilyName & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & crd.SpecialText & "|" & Now.TimeOfDay.TotalSeconds & "|" & crd.FishVarizi & "|" & crd.Mobile & "|" & crd.Gender & "|" & "99"))
                    Else
                        tmpWSResult = CiBS_WS.GetCardDataGift(Encrypt(CurrentBranchCode & "|" & crd.AccountNo & "|" & crd.CustName & "|" & crd.CustNo & "|" & crd.IntCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & tmpCradType(crd.CardTypeIndex) & "|" & crd.CustFamilyName & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & crd.SpecialText & "|" & Now.TimeOfDay.TotalSeconds & "|" & FollowUp & "|" & Refrence & "|" & TrDate & "|" & TrTime & "|" & RespCode & "|" & RespMessage & "|" & CardNo & "|" & CardType & "|" & Terminal & "|" & Merchant & "|" & TrAmount & "|" & "99"))
                    End If
                Catch ex As Exception
                    InsertLocalLog("Error Getting Card Data" & ex.Message)
                    ClearFieldsFlag = True
                    MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit For
                End Try

                tmpAns = Decrypt(tmpWSResult)
                Windows.Forms.Application.DoEvents()
                If String.Compare(tmpAns, "Timeout") = 0 Then
                    MessageBox.Show("Timeout", "خطا")
                    ClearFieldsFlag = True
                    Exit For
                End If

                If tmpAns = "-999" Then ' and tmpAns.Length = 4
                    MessageBox.Show("وضعیت آخرین کارت مشخص نمی باشد" & vbCrLf & "لطفاً به قسمت آخرین کارت صادره مراجعه نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    ClearFieldsFlag = True
                    Exit For
                End If

                If tmpAns = "No Data" Then
                    MessageBox.Show("از این نوع کارت اطلاعاتی جهت دریافت از مرکز موجود نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    ClearFieldsFlag = False
                    Exit For
                End If

                If tmpAns.StartsWith("Unexpected Error") Then
                    MessageBox.Show("بروز خطای ناشناخته" & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit For
                End If

                Dim tmp As String()
                ReDim tmp(tmpAns.Length - 1)
                tmp = tmpAns.Split("|")
                If tmp(0) = "ERROR" Then 'tmpAns.Length = 4 And
                    'If tmp(2) = 5 Then
                    MessageBox.Show("وضعیت آخرین کارت مشخصی نمی باشد" & vbCrLf & "لطفاً به قسمت آخرین کارت صادره مراجعه نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    ClearFieldsFlag = True
                    Exit For
                    'End If
                End If

                'If tmp(0).Trim <> "5" Then
                '    MessageBox.Show("خطای دیتابیس ، لطفا جهت چاپ کارت، دوباره تلاش نمایید (درصورت نیاز آخرین کارت صادره را نیز بررسی نمایید)", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '    ClearFieldsFlag = False
                '    Exit For
                'End If

                'If tmp.Length <> 8 Then
                '    InsertLocalLog("Error Geting Data")
                '    MessageBox.Show("خطا هنگام دریافت دیتا از دیتابیس " & vbCrLf & "مجددا تلاش نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    Exit For
                'End If

                Track1 = tmp(4)
                Track2 = tmp(5)
                Track3 = tmp(6)
                CardNo = tmp(1)
                CustName = crd.CustName

                If Track2.Trim = "" Then
                    MessageBox.Show("از این نوع کارت اطلاعاتی جهت دریافت از مرکز موجود نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    ClearFieldsFlag = False
                    Exit For
                End If
                '/////////// NewRequest 
                If cbCardType.SelectedItem.ToString.Contains("Latin") Or cbCardType.SelectedItem.ToString.Contains("لاتین") Then
                    Dim tmpExpDate As String

                    Dim ss As String() = tmp(2).Split("/")
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
                    ExpDate = ExpDateLatin
                Else
                    Dim ss00 As String() = tmp(2).Split("/")
                    Dim ss11 As String
                    If ss00(0) = 0 Or ss00(0) = "00" Or ss00(0) = "0" Then
                        ss11 = 1400
                    ElseIf ss00(0) >= 97 And ss00(0) <= 99 Then
                        ss11 = 13 & ss00(0)
                    Else
                        ss11 = ss00(0)
                    End If
                    Dim tt11 As String = ss11 & "/" & ss00(1)
                    ExpDate = tt11
                End If
                '//////////////////////
                CVV = tmp(3)
                BrCode = CurrentBranchCode
                CustFamilyName = crd.CustFamilyName
                AccNo = crd.AccountNo
                '//////////////////////////// Sheba
                If AccNo > 12 Then
                    Dim tmpAns2 As String
                    Dim tmpWSResult2 As String
                    Try
                        tmpWSResult2 = CiBS_WS.ShebaGenerator(Encrypt(AccNo))
                    Catch ex As Exception
                        InsertLocalLog("Error Getting Status Card Data" & ex.Message)
                        ClearFieldsFlag = True
                        MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit For
                    End Try
                    tmpAns2 = Decrypt(tmpWSResult2)
                    Windows.Forms.Application.DoEvents()
                    If String.Compare(tmpAns, "Timeout") = 0 Then
                        MessageBox.Show("Timeout", "خطا")
                        ClearFieldsFlag = True
                        Exit For
                    End If
                    ' sheba = tmpAns2
                    sheba = tmpAns2.Substring(0, 4) & "-" & tmpAns2.Substring(4, 3) & "-" & tmpAns2.Substring(7, 6) & "-" & tmpAns2.Substring(13, 13)
                Else
                    sheba = ""
                End If

                '//////////////////////////////////
                If (crd.CardTypeItem.Contains("هد") Or crd.CardTypeItem.ToLower.Contains("gift")) Then   'نوع کارت هدیه

                    'If crd.WithAmount = True Then
                    '    GetAmountFromPos()  ''Masoumeh
                    '    If RespMessage = "OK" Then

                    '    Else



                    '        MessageBox.Show("خطا جهت کار پوز -" & RespMessage)
                    '        Exit Function
                    '    End If
                    'End If

                    If crd.PrintGiftCardAmount = True Then
                        If Not MakeCard(CardNo, "کارت هدیه" & " " & crd.AccountNo & " ریالی ", ExpDate, CVV, BrCode, crd.SpecialText, AccNo, sheba) Then
                            MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
                            Try
                                If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                If Not SetPrintedCardFlag(CardNo & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Catch ex As Exception
                                InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                                MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End Try
                            ClearFieldsFlag = True



                            Exit For
                        End If
                    Else
                        If Not MakeCard(CardNo, "", ExpDate, CVV, BrCode, crd.SpecialText, AccNo, sheba) Then
                            MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
                            Try
                                If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                If Not SetPrintedCardFlag(CardNo & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Catch ex As Exception
                                InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                                MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End Try
                            ClearFieldsFlag = True
                            Exit For
                        End If
                    End If

                ElseIf crd.CardTypeItem.Contains("ران کا") Then
                    ' If Not MakeCard(CardNo, "کارت هدیه" & " " & txtAccountNo.Text & " ریالی ", ExpDate, CVV, BrCode, txtSpecialText.Text.Trim) Then
                    If Not MakeCard(CardNo, CustName & " " & CustFamilyName, ExpDate, CVV, BrCode, crd.SpecialText, AccNo, sheba) Then
                        MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
                        Try
                            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            If Not SetPrintedCardFlag(CardNo & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Catch ex As Exception
                            InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                            MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                        ClearFieldsFlag = True
                        Exit For
                    End If
                Else
                    If Not MakeCard(CardNo, CustName & " " & CustFamilyName, ExpDate, CVV, BrCode, crd.SpecialText, AccNo, sheba) Then
                        MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
                        Try
                            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            If Not SetPrintedCardFlag(CardNo & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Catch ex As Exception
                            InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                            MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                        ClearFieldsFlag = True
                        Exit For
                    End If
                End If
                Windows.Forms.Application.DoEvents()
                ''  lblPrintStatus.Text = "Printing " & j & " of " & PrintCount
                pbPreview.ImageLocation = "tmp.bmp"
                pbPreview.Load()
                '' Me.Refresh()

                tmpAns = MagEncode(Track1, Track2, Track3)
                Windows.Forms.Application.DoEvents()
                If tmpAns = "OK" Then
                    Try
                        If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Mag Encoded Successfully" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        InsertLocalLog("InsertLog- " & ex.Message)
                        MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = True
                        Exit For
                    End Try
                    Windows.Forms.Application.DoEvents()
                    If PrintName(CardNo) Then
                        Windows.Forms.Application.DoEvents()
                        Try
                            '////970711
                            If Not SetPrintedCardFlag(CardNo & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "1" & "|" & "1" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then
                                MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                '**************************************** NEW
                                ClearFieldsFlag = True
                                Exit For
                                '****************************************
                            Else
                                If (PrintCount = 1 Or PrintCount = CounterPrint) Then
                                    MessageBox.Show("کارت با موفقیت صادر گردید", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information)

                                    '**************************************** NEW
                                    ClearFieldsFlag = True
                                    Exit For
                                    '****************************************
                                End If

                                ''ClearFeilds()
                                ''CheckPrinterStatus()
                                ''GetCardRemain()
                                ''PrintCount = 1

                                'EndThread = True
                            End If
                        Catch ex As Exception
                            InsertLocalLog("SetPrintedCardFlag- " & ex.Message)
                            MessageBox.Show("خطا هنگام تغییر وضعیت کارت" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            ClearFieldsFlag = True
                            Exit For
                        End Try

                        ClearFieldsFlag = True
                        ' ClearFeilds()

                    Else
                        ClearFieldsFlag = True
                        Exit For
                    End If
                Else
                    If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error Encoding Mag-" & tmpAns.Replace("Error-", "") & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    If Not SetPrintedCardFlag(CardNo & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    MessageBox.Show("خطا هنگام نوشتن اطلاعات در نوار مغناطیسی " & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید" & vbCrLf & tmpAns.Replace("Error-", ""), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = True
                End If
                pbPreview.ImageLocation = ""
                lblPrintStatus.Text = ""
                ClearFieldsFlag = True

                pbPreview.ImageLocation = ""
            Next

            EndThread = True

            ''  If EndThread = True Then
            ClearFeilds()
            CheckPrinterStatus()
            GetCardRemain()
            PrintCount = 1
            '' End If

        Catch ex As Exception

            cbCardType.Enabled = True
            txtCustName.Enabled = True
            txtCustFamilyName.Enabled = True
            txtAccountNo.Enabled = True
            txtCustNo.Enabled = True
            txtCustNoGift.Enabled = True
            txtIntCode.Enabled = True
            rbFemale.Checked = False
            rbMale.Checked = False
            rbFemale.Visible = False
            rbMale.Visible = False

            ClearFeilds()
            CheckPrinterStatus()
            GetCardRemain()
            InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error while issuing card -" & ex.Message & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds)
            InsertLocalLog("Error in issuing Card-" & ex.Message)
        End Try
    End Sub


    'Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
    '    btnPreviewClick = False
    '    btnPrintClick = True


    '    If cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then

    '        If txtAccountNo.Text.Trim = "" Or txtAccountNo.Text.Trim = " " Then
    '        Else
    '            If txtAccountNo.Text.Trim = "0" Then
    '                FisheVarizi = " "
    '            Else
    '                If txtFishVarizi.Text.Trim = "" Then
    '                    MessageBox.Show("با ثبت مبلغ اولیه نیاز می باشد که حتما شماره فیش واریزی را وارد نمایید")
    '                    Exit Sub
    '                End If
    '            End If
    '        End If
    '    End If

    '    If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
    '        NameCustomer = txtCustName.Text.Trim
    '        FamilyCustomer = txtCustFamilyName.Text.Trim
    '        AccountNumber = txtAccountNo.Text.Trim
    '        CustomerNumber = txtCustNo.Text.Trim
    '        CustomerNoGift = txtCustNoGift.Text.Trim
    '        FisheVarizi = txtFishVarizi.Text.Trim
    '        MobileNumber = txtMobile.Text.Trim
    '        IntCode = txtIntCode.Text.Trim
    '    End If
    '    CounterPrint = 0
    '    'Try

    '    '    tmpWSResult = CiBS_WS.GetCustomerInfo(Encrypt(txtCustNo.Text.Trim & "|" & Now.TimeOfDay.TotalSeconds))
    '    'Catch ex As Exception
    '    '    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '    ClearFieldsFlag = False
    '    '    Exit Sub

    '    'End Try
    '    'If Decrypt(tmpWSResult) = 0 Then
    '    '    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '    ClearFieldsFlag = False
    '    '    Exit Sub
    '    'End If


    '    '////////////////// بالا باید حذف گردد
    '    Mablagh = txtAccountNo.Text.Trim
    '    CustNumber = txtCustNoGift.Text
    '    If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
    '        GiftCard = True
    '    End If
    '    PrintCount = 1
    '    If cbCardType.SelectedIndex = -1 Then
    '        MessageBox.Show("لطفاً نوع کارت را مشخص نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        ClearFieldsFlag = False
    '        Exit Sub
    '    End If

    '    ''''''''''''' Faal kon
    '    If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then
    '        ' MessageBox.Show("11-" & CusNumber)
    '        If CusNumber = False Then
    '            ' MessageBox.Show("11")
    '            GetExistCustomerNumber()
    '            If CusNumber = False And txtCustNoGift.Text.Trim = "" Then
    '                MessageBox.Show("شماره مشتری را وارد نمایید یا اطلاعات مشتری جدید را وارد نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                ClearFieldsFlag = False
    '                Exit Sub
    '            End If
    '        End If
    '    End If

    '    ''''''''''''' Faal kon
    '    If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
    '        If RdbAmount.Checked = True Or RdbWithoutAmount.Checked = True Then
    '        Else
    '            MessageBox.Show("لطفاً نوع مبلغ را مشخص نمایید(بامبلغ یا بدون مبلغ)", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            ClearFieldsFlag = False
    '            Exit Sub
    '        End If
    '    End If


    '    If txtIntCode.Text.Length <> 10 And rbIranian.Checked Then
    '        MessageBox.Show("کد ملی وارد شده کمتر از 10 رقم می باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        ClearFieldsFlag = False
    '        Exit Sub
    '    End If

    '    If txtIntCode.Text.Length > 0 And rbIranian.Checked Then
    '        Dim ResultCheckIntCode As Boolean = CheckIntCode(txtIntCode.Text)
    '        If ResultCheckIntCode = False Then
    '            MessageBox.Show("کد ملی وارد شده اشتباه است ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            ClearFieldsFlag = False
    '            Exit Sub
    '        End If
    '    End If

    '    If txtCustName.Text.Length < 2 Then
    '        MessageBox.Show("نام وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        ClearFieldsFlag = False
    '        Exit Sub
    '    End If
    '    If txtCustFamilyName.Text.Length < 2 Then
    '        MessageBox.Show("نام خانوادگی وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        ClearFieldsFlag = False
    '        Exit Sub
    '    End If


    '    If Not SabteAhvalLibrary.SabteAhval.ValidateNIN(txtIntCode.Text) And rbIranian.Checked Then
    '        MessageBox.Show("لطفاً صحت کد ملی را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        ClearFieldsFlag = False
    '        Exit Sub
    '    End If
    '    If rbOthers.Checked Then
    '        If txtIntCode.Text.Length < 5 Then
    '            MessageBox.Show("لطفاً صحت کد اقامت را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            ClearFieldsFlag = False
    '        End If
    '    End If


    '    If cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then
    '        Dim mobile As String = txtMobile.Text.Trim
    '        '''@arezoo
    '        If mobile = "" Then
    '            MessageBox.Show("شماره موبایل را وارد نمایید")
    '            Exit Sub
    '        Else

    '            If mobile(0) <> "9" Or mobile(1) <> "8" Then
    '                MessageBox.Show("شماره موبایل مربوطه باید با 98 شروع شود، مثل 989121111111", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                ClearFieldsFlag = False
    '                Exit Sub
    '            End If

    '            If mobile(2) <> "9" Then
    '                MessageBox.Show("دریافت اطلاعات", "شماره موبایل مربوطه باید با 9 شروع شود، مثل 989121111111", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                ClearFieldsFlag = False
    '                Exit Sub
    '            End If

    '            If mobile.Length < 12 Or mobile.Length > 12 Then
    '                MessageBox.Show("لطفاً صحت شماره موبایل را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                ClearFieldsFlag = False
    '                Exit Sub
    '            End If
    '            ''''@arezoo
    '        End If
    '        If rbFemale.Checked = False And rbMale.Checked = False Then
    '            MessageBox.Show("لطفا جنسیت را انتخاب کنید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            ClearFieldsFlag = False
    '            Exit Sub
    '        End If

    '        If txtAccountNo.Text.Trim <> "0" And txtAccountNo.Text.Trim <> "" Then

    '            If txtFishVarizi.Text = "" Then
    '                MessageBox.Show("شماره فیش واریزی را وارد نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                ClearFieldsFlag = False
    '                Exit Sub
    '            End If
    '        End If
    '    End If

    '    If Not (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then   'نوع کارت هدیه

    '        If txtAccountNo.Text.Length <> 13 Then
    '            MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            ClearFieldsFlag = False
    '            Exit Sub
    '        End If

    '        If txtCustNo.Text.Length < 2 Then
    '            MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            ClearFieldsFlag = False
    '            Exit Sub
    '        End If

    '        If IsValidSIBACheckDigit(txtAccountNo.Text) = False Then
    '            MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            ClearFieldsFlag = False
    '            Exit Sub
    '        End If
    '    End If

    '    If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then   'نوع کارت هدیه
    '        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then
    '            ' MessageBox.Show("FirstGetCustomerInfo")

    '            If CusNumber = False Then
    '                If CustNumber.Length < 2 Then
    '                    MessageBox.Show("شماره مشتری را کامل وارد نمایید ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    ClearFieldsFlag = False
    '                    Exit Sub
    '                End If
    '                tmpWSResult = ""

    '                ''''''' inja ro faal kon
    '                Try
    '                    tmpWSResult = CiBS_WS.GetCustomerInfo(Encrypt(CustNumber.Trim & "|" & Now.TimeOfDay.TotalSeconds))
    '                Catch ex As Exception
    '                    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    ClearFieldsFlag = False
    '                    Exit Sub
    '                End Try

    '                Dim tmpAns As String = ""
    '                tmpAns = Decrypt(tmpWSResult)


    '                'MessageBox.Show("GetCustomerInfo Result:" & tmpAns)

    '                Dim ResultGetCustom As String = Decrypt(tmpWSResult)
    '                Dim ResultGetCustom1 As String() = ResultGetCustom.Split("|")
    '                '///////
    '                If String.Compare(ResultGetCustom1(0), "Timeout") = 0 Then
    '                    MessageBox.Show("هنگام بررسی شماره مشتری خطای Timeout ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    ClearFieldsFlag = False
    '                    Exit Sub
    '                End If

    '                If tmpAns.Contains("No Data") Then
    '                    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد. خطای NO DATA ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    ClearFieldsFlag = False
    '                    Exit Sub
    '                End If
    '                If tmpAns.Contains("Unexpected Error") Then
    '                    MessageBox.Show("بررسی شماره مشتری وارد شده خطای Unexpected Error ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    ClearFieldsFlag = False
    '                    Exit Sub
    '                End If

    '                '//////
    '                If tmpAns.Contains("0") Then
    '                    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    ClearFieldsFlag = False
    '                    Exit Sub
    '                End If
    '            End If
    '            'MessageBox.Show("AfterGetCustomerInfo")
    '            If RdbAmount.Checked = True Then
    '                '///// 941107
    '                'If txtAccountNo.Text.Length < 5 Then
    '                '    MessageBox.Show("رقم کارت هدیه وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                '    ClearFieldsFlag = False
    '                '    Exit Sub
    '                'End If

    '                'If Not txtAccountNo.Text.ToString.Replace(",", "").EndsWith("0000") Or txtAccountNo.Text.Replace(",", "") > 5000000 Or txtAccountNo.Text.Replace(",", "") < 50000 Then
    '                '    MessageBox.Show("رقم کارت هدیه وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                '    ClearFieldsFlag = False
    '                '    Exit Sub
    '                'End If
    '            End If

    '        End If
    '        PrintCount = txtCustNo.Text

    '        ''''@arezoo: set 100000 for irancard  

    '        If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
    '            If txtAccountNo.Text.Trim = 0 Then

    '            ElseIf Not txtAccountNo.Text.ToString.Replace(",", "").EndsWith("0000") Then
    '                MessageBox.Show("رقم ایران کارت باید مضربی از 10000 وارد شود ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                ClearFieldsFlag = False
    '                Exit Sub
    '            End If
    '        End If


    '    End If

    '    If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
    '        If rbFemale.Checked = False And rbMale.Checked = False Then
    '            MessageBox.Show("لطفا جنسیت را انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Exit Sub
    '        End If
    '    End If

    '    'txtCardRemain
    '    'If CInt(txtCardRemain.Text) < 1 Then
    '    '    MessageBox.Show(" این نوع کارت جهت صدور در شعبه وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '    ClearFieldsFlag = False
    '    '    Exit Sub
    '    'End If



    '    ''''''''''''''''''''''''''''''''@arezoo
    '    '''''check whether the customer has a irancard or not 


    '    ''''''@arezoo: فقط شماره شناسنامه رو چک کنه که اگر در دیتابیس بود این پیغام رو بده، درسته؟
    '    ''''جون وقتی که چاپ میکنیم میره شماره شناسنامه میزاره یا نه بره 
    '    ''''isprinted & who printed ina ro ham checkkone?
    '    If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
    '        Try
    '            Dim ResultCheckExistBlueCard As String = CiBS_WS.CheckExistBlueCard(txtIntCode.Text & "|" & "99")
    '            If ResultCheckExistBlueCard = 1 Then
    '                MessageBox.Show("همکار گرامی، مشتری قبلا ایران کارت آبی داشته اند. لطفا از گزینه صدور مجدد استفاده کنید ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                Exit Sub
    '            End If
    '        Catch ex As Exception
    '            InsertLocalLog("Error Getting Card Data" & ex.Message)
    '            ClearFieldsFlag = True
    '            MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Exit Sub
    '        End Try

    '    End If


    '    ''GetAmountFromPos()  ''Masoumeh

    '    '////////////////////////////////

    '    cc.CustName = txtCustName.Text
    '    cc.CustFamilyName = txtCustFamilyName.Text
    '    cc.AccountNo = txtAccountNo.Text
    '    cc.CustNo = txtCustNo.Text
    '    ''cc.FishVarizi = txtFishVarizi.Text
    '    ''cc.Mobile = txtMobile.Text
    '    ''cc.IntCode = txtIntCode.Text
    '    cc.Iranian = rbIranian.Checked
    '    cc.OtherIranian = rbOthers.Checked
    '    cc.IntCode = txtIntCode.Text
    '    cc.WithAmount = RdbAmount.Checked
    '    cc.WithoutAmount = RdbWithoutAmount.Checked
    '    cc.CardTypeItem = cbCardType.SelectedItem.ToString
    '    cc.CardTypeIndex = cbCardType.SelectedIndex
    '    cc.SpecialText = txtSpecialText.Text
    '    cc.PrintGiftCardAmount = chkPrintGiftCardAmount.Checked
    '    cc.FishVarizi = txtFishVarizi.Text.Trim
    '    cc.Mobile = txtMobile.Text.Trim
    '    If rbFemale.Checked Then
    '        cc.Gender = "1"
    '    Else
    '        cc.Gender = "0"
    '    End If


    '    'Dim thrd As New Thread(AddressOf IssueCard)
    '    'thrd.Start(cc)
    '    'bgwCard.RunWorkerAsync()

    '    PrintedAllCards()

    '    txtCustName.Enabled = True
    '    txtCustFamilyName.Enabled = True
    '    txtAccountNo.Enabled = True
    '    txtCustNo.Enabled = True
    '    txtCustNoGift.Enabled = True
    '    txtIntCode.Enabled = True
    '    ''''@arezoo: اینجا غیرفعال کنم؟- سوال
    '    lblMobileExp.Visible = False
    '    rbFemale.Checked = False
    '    rbMale.Checked = False






    '    '/////////////////////////////////////////////////////////////////
    '    'btnPreviewClick = False
    '    ' btnPrintClick = True


    '    ' If cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then

    '    '     If txtAccountNo.Text.Trim = "" Or txtAccountNo.Text.Trim = " " Then
    '    '     Else
    '    '         If txtAccountNo.Text.Trim = "0" Then
    '    '             FisheVarizi = " "
    '    '         Else
    '    '             If txtFishVarizi.Text.Trim = "" Then
    '    '                 MessageBox.Show("با ثبت مبلغ اولیه نیاز می باشد که حتما شماره فیش واریزی را وارد نمایید")
    '    '                 Exit Sub
    '    '             End If
    '    '         End If
    '    '     End If
    '    ' End If

    '    ' If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
    '    '     NameCustomer = txtCustName.Text.Trim
    '    '     FamilyCustomer = txtCustFamilyName.Text.Trim
    '    '     AccountNumber = txtAccountNo.Text.Trim
    '    '     CustomerNumber = txtCustNo.Text.Trim
    '    '     CustomerNoGift = txtCustNoGift.Text.Trim
    '    '     FisheVarizi = txtFishVarizi.Text.Trim
    '    '     MobileNumber = txtMobile.Text.Trim
    '    '     IntCode = txtIntCode.Text.Trim
    '    ' End If
    '    ' CounterPrint = 0
    '    ' 'Try

    '    ' '    tmpWSResult = CiBS_WS.GetCustomerInfo(Encrypt(txtCustNo.Text.Trim & "|" & Now.TimeOfDay.TotalSeconds))
    '    ' 'Catch ex As Exception
    '    ' '    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    ' '    ClearFieldsFlag = False
    '    ' '    Exit Sub

    '    ' 'End Try
    '    ' 'If Decrypt(tmpWSResult) = 0 Then
    '    ' '    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    ' '    ClearFieldsFlag = False
    '    ' '    Exit Sub
    '    ' 'End If


    '    ' '////////////////// بالا باید حذف گردد
    '    ' Mablagh = txtAccountNo.Text.Trim
    '    ' CustNumber = txtCustNoGift.Text
    '    ' If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
    '    '     GiftCard = True
    '    ' End If
    '    ' PrintCount = 1
    '    ' If cbCardType.SelectedIndex = -1 Then
    '    '     MessageBox.Show("لطفاً نوع کارت را مشخص نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '     ClearFieldsFlag = False
    '    '     Exit Sub
    '    ' End If

    '    ' ''''''''''''' Faal kon
    '    ' If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then
    '    '     ' MessageBox.Show("11-" & CusNumber)
    '    '     If CusNumber = False Then
    '    '         ' MessageBox.Show("11")
    '    '         GetExistCustomerNumber()
    '    '         If CusNumber = False And txtCustNoGift.Text.Trim = "" Then
    '    '             MessageBox.Show("شماره مشتری را وارد نمایید یا اطلاعات مشتری جدید را وارد نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '             ClearFieldsFlag = False
    '    '             Exit Sub
    '    '         End If
    '    '     End If
    '    ' End If

    '    ' ''''''''''''' Faal kon
    '    ' If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
    '    '     If RdbAmount.Checked = True Or RdbWithoutAmount.Checked = True Then
    '    '     Else
    '    '         MessageBox.Show("لطفاً نوع مبلغ را مشخص نمایید(بامبلغ یا بدون مبلغ)", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '         ClearFieldsFlag = False
    '    '         Exit Sub
    '    '     End If
    '    ' End If


    '    ' If txtIntCode.Text.Length <> 10 And rbIranian.Checked Then
    '    '     MessageBox.Show("کد ملی وارد شده کمتر از 10 رقم می باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '     ClearFieldsFlag = False
    '    '     Exit Sub
    '    ' End If

    '    ' If txtIntCode.Text.Length > 0 And rbIranian.Checked Then
    '    '     Dim ResultCheckIntCode As Boolean = CheckIntCode(txtIntCode.Text)
    '    '     If ResultCheckIntCode = False Then
    '    '         MessageBox.Show("کد ملی وارد شده اشتباه است ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '         ClearFieldsFlag = False
    '    '         Exit Sub
    '    '     End If
    '    ' End If

    '    ' If txtCustName.Text.Length < 2 Then
    '    '     MessageBox.Show("نام وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '     ClearFieldsFlag = False
    '    '     Exit Sub
    '    ' End If
    '    ' If txtCustFamilyName.Text.Length < 2 Then
    '    '     MessageBox.Show("نام خانوادگی وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '     ClearFieldsFlag = False
    '    '     Exit Sub
    '    ' End If


    '    ' If Not SabteAhvalLibrary.SabteAhval.ValidateNIN(txtIntCode.Text) And rbIranian.Checked Then
    '    '     MessageBox.Show("لطفاً صحت کد ملی را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '     ClearFieldsFlag = False
    '    '     Exit Sub
    '    ' End If
    '    ' If rbOthers.Checked Then
    '    '     If txtIntCode.Text.Length < 5 Then
    '    '         MessageBox.Show("لطفاً صحت کد اقامت را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '         ClearFieldsFlag = False
    '    '     End If
    '    ' End If


    '    ' If cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then
    '    '     Dim mobile As String = txtMobile.Text.Trim
    '    '     '''@arezoo
    '    '     If mobile = "" Then
    '    '         MessageBox.Show("شماره موبایل را وارد نمایید")
    '    '         Exit Sub
    '    '     Else
    '    '         If mobile(0) <> "9" Or mobile(1) <> "8" Then
    '    '             MessageBox.Show("شماره موبایل مربوطه باید با 98 شروع شود، مثل 989121111111", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '             ClearFieldsFlag = False
    '    '             Exit Sub
    '    '         End If

    '    '         If mobile(2) <> "9" Then
    '    '             MessageBox.Show("دریافت اطلاعات", "شماره موبایل مربوطه باید با 9 شروع شود، مثل 989121111111", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '             ClearFieldsFlag = False
    '    '             Exit Sub
    '    '         End If

    '    '         If mobile.Length < 12 Or mobile.Length > 12 Then
    '    '             MessageBox.Show("لطفاً صحت شماره موبایل را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '             ClearFieldsFlag = False
    '    '             Exit Sub
    '    '         End If
    '    '         ''''@arezoo
    '    '     End If

    '    '     If rbFemale.Checked = False And rbMale.Checked = False Then

    '    '         MessageBox.Show("لطفا جنسیت را انتخاب کنید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '         ClearFieldsFlag = False
    '    '         Exit Sub
    '    '     End If

    '    '     If txtAccountNo.Text.Trim <> "0" And txtAccountNo.Text.Trim <> "" Then
    '    '         If txtFishVarizi.Text = "" Then
    '    '             MessageBox.Show("شماره فیش واریزی را وارد نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '             ClearFieldsFlag = False
    '    '             Exit Sub
    '    '         End If
    '    '     End If
    '    ' End If

    '    ' If Not (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then   'نوع کارت هدیه

    '    '     If txtAccountNo.Text.Length <> 13 Then
    '    '         MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '         ClearFieldsFlag = False
    '    '         Exit Sub
    '    '     End If

    '    '     If txtCustNo.Text.Length < 2 Then
    '    '         MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '         ClearFieldsFlag = False
    '    '         Exit Sub
    '    '     End If

    '    '     If IsValidSIBACheckDigit(txtAccountNo.Text) = False Then
    '    '         MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '         ClearFieldsFlag = False
    '    '         Exit Sub
    '    '     End If
    '    ' End If

    '    ' If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then   'نوع کارت هدیه
    '    '     If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then
    '    '         ' MessageBox.Show("FirstGetCustomerInfo")

    '    '         If CusNumber = False Then
    '    '             If CustNumber.Length < 2 Then
    '    '                 MessageBox.Show("شماره مشتری را کامل وارد نمایید ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '                 ClearFieldsFlag = False
    '    '                 Exit Sub
    '    '             End If
    '    '             tmpWSResult = ""

    '    '             ''''''' inja ro faal kon
    '    '             Try
    '    '                 tmpWSResult = CiBS_WS.GetCustomerInfo(Encrypt(CustNumber.Trim & "|" & Now.TimeOfDay.TotalSeconds))
    '    '             Catch ex As Exception
    '    '                 MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '                 ClearFieldsFlag = False
    '    '                 Exit Sub
    '    '             End Try

    '    '             Dim tmpAns As String = ""
    '    '             tmpAns = Decrypt(tmpWSResult)


    '    '             'MessageBox.Show("GetCustomerInfo Result:" & tmpAns)

    '    '             Dim ResultGetCustom As String = Decrypt(tmpWSResult)
    '    '             Dim ResultGetCustom1 As String() = ResultGetCustom.Split("|")
    '    '             '///////
    '    '             If String.Compare(ResultGetCustom1(0), "Timeout") = 0 Then
    '    '                 MessageBox.Show("هنگام بررسی شماره مشتری خطای Timeout ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '                 ClearFieldsFlag = False
    '    '                 Exit Sub
    '    '             End If

    '    '             If tmpAns.Contains("No Data") Then
    '    '                 MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد. خطای NO DATA ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '                 ClearFieldsFlag = False
    '    '                 Exit Sub
    '    '             End If
    '    '             If tmpAns.Contains("Unexpected Error") Then
    '    '                 MessageBox.Show("بررسی شماره مشتری وارد شده خطای Unexpected Error ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '                 ClearFieldsFlag = False
    '    '                 Exit Sub
    '    '             End If

    '    '             '//////
    '    '             If tmpAns.Contains("0") Then
    '    '                 MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '                 ClearFieldsFlag = False
    '    '                 Exit Sub
    '    '             End If
    '    '         End If
    '    '         'MessageBox.Show("AfterGetCustomerInfo")
    '    '         If RdbAmount.Checked = True Then
    '    '             '///// 941107
    '    '             'If txtAccountNo.Text.Length < 5 Then
    '    '             '    MessageBox.Show("رقم کارت هدیه وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '             '    ClearFieldsFlag = False
    '    '             '    Exit Sub
    '    '             'End If

    '    '             'If Not txtAccountNo.Text.ToString.Replace(",", "").EndsWith("0000") Or txtAccountNo.Text.Replace(",", "") > 5000000 Or txtAccountNo.Text.Replace(",", "") < 50000 Then
    '    '             '    MessageBox.Show("رقم کارت هدیه وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '             '    ClearFieldsFlag = False
    '    '             '    Exit Sub
    '    '             'End If
    '    '         End If

    '    '     End If
    '    '     PrintCount = txtCustNo.Text

    '    '     ''''@arezoo: set 100000 for irancard  

    '    '     If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
    '    '         If txtAccountNo.Text.Trim = 0 Then

    '    '         ElseIf Not txtAccountNo.Text.ToString.Replace(",", "").EndsWith("0000") Then
    '    '             MessageBox.Show("رقم ایران کارت باید مضربی از 10000 وارد شود ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '             ClearFieldsFlag = False
    '    '             Exit Sub
    '    '         End If
    '    '     End If


    '    ' End If

    '    ' If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
    '    '     If rbFemale.Checked = False And rbMale.Checked = False Then
    '    '         MessageBox.Show("لطفا جنسیت را انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '         Exit Sub
    '    '     End If
    '    ' End If

    '    ' 'txtCardRemain
    '    ' 'If CInt(txtCardRemain.Text) < 1 Then
    '    ' '    MessageBox.Show(" این نوع کارت جهت صدور در شعبه وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    ' '    ClearFieldsFlag = False
    '    ' '    Exit Sub
    '    ' 'End If



    '    ' ''''''''''''''''''''''''''''''''@arezoo
    '    ' '''''check whether the customer has a irancard or not 


    '    ' ''''''@arezoo: فقط شماره شناسنامه رو چک کنه که اگر در دیتابیس بود این پیغام رو بده، درسته؟
    '    ' ''''جون وقتی که چاپ میکنیم میره شماره شناسنامه میزاره یا نه بره 
    '    ' ''''isprinted & who printed ina ro ham checkkone?
    '    ' If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
    '    '     Try
    '    '         Dim ResultCheckExistBlueCard As String = CiBS_WS.CheckExistBlueCard(txtIntCode.Text & "|" & "99")
    '    '         If ResultCheckExistBlueCard = 1 Then
    '    '             MessageBox.Show("همکار گرامی، مشتری قبلا ایران کارت آبی داشته اند. لطفا از گزینه صدور مجدد استفاده کنید ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '             Exit Sub
    '    '         End If
    '    '     Catch ex As Exception
    '    '         InsertLocalLog("Error Getting Card Data" & ex.Message)
    '    '         ClearFieldsFlag = True
    '    '         MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '         Exit Sub
    '    '     End Try

    '    ' End If


    '    ' ''GetAmountFromPos()  ''Masoumeh

    '    ' '////////////////////////////////

    '    ' cc.CustName = txtCustName.Text
    '    ' cc.CustFamilyName = txtCustFamilyName.Text
    '    ' cc.AccountNo = txtAccountNo.Text
    '    ' cc.CustNo = txtCustNo.Text
    '    ' ''cc.FishVarizi = txtFishVarizi.Text
    '    ' ''cc.Mobile = txtMobile.Text
    '    ' ''cc.IntCode = txtIntCode.Text
    '    ' cc.Iranian = rbIranian.Checked
    '    ' cc.OtherIranian = rbOthers.Checked
    '    ' cc.IntCode = txtIntCode.Text
    '    ' cc.WithAmount = RdbAmount.Checked
    '    ' cc.WithoutAmount = RdbWithoutAmount.Checked
    '    ' cc.CardTypeItem = cbCardType.SelectedItem.ToString
    '    ' cc.CardTypeIndex = cbCardType.SelectedIndex
    '    ' cc.SpecialText = txtSpecialText.Text
    '    ' cc.PrintGiftCardAmount = chkPrintGiftCardAmount.Checked
    '    ' cc.FishVarizi = txtFishVarizi.Text.Trim
    '    ' cc.Mobile = txtMobile.Text.Trim
    '    ' If rbFemale.Checked Then
    '    '     cc.Gender = "1"
    '    ' Else
    '    '     cc.Gender = "0"
    '    ' End If



    '    ' 'Dim thrd As New Thread(AddressOf IssueCard)
    '    ' 'thrd.Start(cc)
    '    ' 'bgwCard.RunWorkerAsync()

    '    ' PrintedAllCards()

    '    ' txtCustName.Enabled = True
    '    ' txtCustFamilyName.Enabled = True
    '    ' txtAccountNo.Enabled = True
    '    ' txtCustNo.Enabled = True
    '    ' txtCustNoGift.Enabled = True
    '    ' txtIntCode.Enabled = True
    '    ' ''''@arezoo: اینجا غیرفعال کنم؟- سوال
    '    ' lblMobileExp.Visible = False
    '    ' rbFemale.Checked = False
    '    ' rbMale.Checked = False



    'End Sub

    Function PrintName(ByVal CardNumber As String) As Boolean
        Try
            Windows.Forms.Application.DoEvents()
            Dim tmp As String = ""
            If CheckClening(CurrentPrinter) > 1 Then
                If OpenPrinter(CurrentPrinter) > 0 Then
                    Windows.Forms.Application.DoEvents()
                    tmp = KOprinting(hPrinter, "tmp.bmp", "varnish.bmp")
                    Windows.Forms.Application.DoEvents()
                    If String.Compare(tmp, "Well Done") = 0 Then
                        CounterPrint = CounterPrint + 1
                        If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNumber & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Card Printed Successfully" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClosePrinter(hPrinter)
                        Return True
                    Else
                        MessageBox.Show("خطا هنگام چاپ کارت " & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید" & vbCrLf & tmp, "خطا")
                        If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNumber & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error on Printing Card - " & tmp.Replace("Error-", "") & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        If Not SetPrintedCardFlag(CardNumber & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End If
                Else
                    MessageBox.Show("خطا هنگام برقراری ارتباط با چاپگر" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید" & vbCrLf & tmp, "خطا")
                    If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNumber & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error on Printing Card - " & tmp.Replace("Error-", "") & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    If Not SetPrintedCardFlag(CardNumber & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            Else
                If Not String.IsNullOrEmpty(tmp) Then
                    MessageBox.Show("چاپگر شما نیاز به تمیزکاری دارد " & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
                    If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNumber & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error on Printing Card - Printer Needs Cleaning" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    If Not SetPrintedCardFlag(CardNumber & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                Else
                    MessageBox.Show("خطا هنگام برقراری ارتباط با چاپگر" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
                    Return False
                End If

            End If
        Catch ex As Exception
            InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error while printing card -" & ex.Message & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds)
            InsertLocalLog("Error in Printing Card-" & ex.Message)
        End Try

    End Function

    Function CheckClening(ByVal PName As String) As Integer
        CheckClening = 0
        hPrinter = OpenPrinter(PName)
        If hPrinter > 0 Then
            tmpAns = RunMyCommand(hPrinter, "Pem;2")
            If String.Compare(tmpAns, "OK") = 0 Then
                Integer.TryParse(RunMyCommand(hPrinter, "Rco;rc"), CheckClening)
            End If
            RunMyCommand(hPrinter, "Pem;0")
            ClosePrinter(hPrinter)
        Else
            CheckClening = -1
        End If
    End Function

    Function MagEncode(ByVal trk1 As String, ByVal trk2 As String, ByVal trk3 As String) As String
        Dim _hprn As Int32 = 0
        Try
            _hprn = OpenPrinter(CurrentPrinter)
            tmpAns = RunMyCommand(_hprn, "Pem;2")
            If String.Compare(tmpAns, "OK") = 0 Then
                tmpAns = RunMyCommand(_hprn, "Dm;1;" & trk1)
                If String.Compare(tmpAns, "OK") = 0 Then
                    tmpAns = RunMyCommand(_hprn, "Dm;2;" & trk2)
                    If String.Compare(tmpAns, "OK") = 0 Then
                        tmpAns = RunMyCommand(_hprn, "Dm;3;" & trk3)
                        If String.Compare(tmpAns, "OK") = 0 Then
                            tmpAns = RunMyCommand(_hprn, "Smw")
                            If String.Compare(tmpAns, "OK") = 0 Then
                                ClosePrinter(_hprn)
                                Return tmpAns
                            Else
                                ClosePrinter(_hprn)
                                Return "Error-" & tmpAns.Trim
                            End If
                        Else
                            ClosePrinter(_hprn)
                            Return "Error-" & tmpAns.Trim
                        End If
                    Else
                        ClosePrinter(_hprn)
                        Return "Error-" & tmpAns.Trim
                    End If
                Else
                    ClosePrinter(_hprn)
                    Return "Error-" & tmpAns.Trim
                End If
            Else
                ClosePrinter(_hprn)
                Return "Error-" & tmpAns.Trim
            End If

        Catch ex As Exception
            ClosePrinter(_hprn)
            Return "Error-" & ex.Message.Trim
        End Try

    End Function

    ' ''Private Sub bgwPrint_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwPrint.DoWork
    ' ''    Control.CheckForIllegalCrossThreadCalls = False

    ' ''    If cbCardType.SelectedIndex = -1 Then
    ' ''        MessageBox.Show("لطفاً نوع کارت را مشخص نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' ''        ClearFieldsFlag = False
    ' ''        e.Cancel = True
    ' ''        Exit Sub
    ' ''    End If

    ' ''    If txtAccountNo.Text.Length <> 13 Then
    ' ''        MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' ''        ClearFieldsFlag = False
    ' ''        e.Cancel = True
    ' ''        Exit Sub
    ' ''    End If


    ' ''    If txtCustNo.Text.Length < 2 Then
    ' ''        MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' ''        ClearFieldsFlag = False
    ' ''        e.Cancel = True
    ' ''        Exit Sub
    ' ''    End If
    ' ''    If txtIntCode.Text.Length <> 10 Then
    ' ''        MessageBox.Show("کد ملی وارد شده کمتر از 10 رقم می باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' ''        ClearFieldsFlag = False
    ' ''        e.Cancel = True
    ' ''        Exit Sub
    ' ''    End If
    ' ''    If txtCustName.Text.Length < 2 Then
    ' ''        MessageBox.Show("نام وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' ''        ClearFieldsFlag = False
    ' ''        e.Cancel = True
    ' ''        Exit Sub
    ' ''    End If
    ' ''    If txtCustFamilyName.Text.Length < 2 Then
    ' ''        MessageBox.Show("نام خانوادگی وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' ''        ClearFieldsFlag = False
    ' ''        e.Cancel = True
    ' ''        Exit Sub
    ' ''    End If
    ' ''    If IsValidSIBACheckDigit(txtAccountNo.Text) = False Then
    ' ''        MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' ''        ClearFieldsFlag = False
    ' ''        e.Cancel = True
    ' ''        Exit Sub
    ' ''    End If

    ' ''    If Not SabteAhvalLibrary.SabteAhval.ValidateNIN(txtIntCode.Text) Then
    ' ''        MessageBox.Show("لطفاً صحت کد ملی را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' ''        ClearFieldsFlag = False
    ' ''        e.Cancel = True
    ' ''        Exit Sub
    ' ''    End If

    ' ''    'نمایشی
    ' ''    ' '' ''If CInt(txtCardRemain.Text) < 1 Then
    ' ''    ' '' ''    MessageBox.Show(" این نوع کارت جهت صدور در شعبه وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' ''    ' '' ''    ClearFieldsFlag = False
    ' ''    ' '' ''    e.Cancel = True
    ' ''    ' '' ''    Exit Sub
    ' ''    ' '' ''End If
    ' ''    Try
    ' ''        If Not bgwPrint.CancellationPending And e.Cancel <> True Then
    ' ''            tmpWSResult = CiBS_WS.GetCardData(Encrypt(CurrentBranchCode & "|" & txtAccountNo.Text.Trim & "|" & txtCustName.Text.Trim & "|" & txtCustNo.Text.Trim & "|" & txtIntCode.Text.Trim  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & tmpCradType(cbCardType.SelectedIndex) & "|" & txtCustFamilyName.Text & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & txtSpecialText.Text & "|" & Now.TimeOfDay.TotalSeconds))
    ' ''            tmpAns = Decrypt(tmpWSResult)

    ' ''            If String.Compare(tmpAns, "Timeout") = 0 Then
    ' ''                MessageBox.Show("Timeout", "خطا")
    ' ''                ClearFieldsFlag = True
    ' ''                Exit Try
    ' ''            End If

    ' ''            If tmpAns.Length = 1 And tmpAns = "5" Then
    ' ''                MessageBox.Show("وضعیت آخرین کارت مشخصی نمی باشد" & vbCrLf & "لطفاً به قسمت آخرین کارت صادره مراجعه نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    ' ''                ClearFieldsFlag = True
    ' ''                Exit Try
    ' ''            End If

    ' ''            If tmpAns = "No Data" Then
    ' ''                MessageBox.Show("از این نوع کارت اطلاعاتی جهت دریافت از مرکز موجود نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop)
    ' ''                ClearFieldsFlag = False
    ' ''                Exit Try
    ' ''            End If

    ' ''            If tmpAns.StartsWith("Unexpected Error") Then
    ' ''                MessageBox.Show("بروز خطای ناشناخته" & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' ''                ClearFieldsFlag = False
    ' ''                Exit Try
    ' ''            End If

    ' ''            Dim tmp As String()
    ' ''            ReDim tmp(tmpAns.Length - 1)
    ' ''            tmp = tmpAns.Split("|")


    ' ''            Track1 = tmp(4)
    ' ''            Track2 = tmp(5)
    ' ''            Track3 = tmp(6)
    ' ''            CardNo = tmp(1)
    ' ''            CustName = txtCustName.Text
    ' ''            ExpDate = tmp(2)
    ' ''            CVV = tmp(3)
    ' ''            BrCode = CurrentBranchCode
    ' ''            CustFamilyName = txtCustFamilyName.Text




    ' ''            If Not MakeCard(CardNo, CustName & " " & CustFamilyName, ExpDate, CVV, BrCode, txtSpecialText.Text.Trim) Then
    ' ''                MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
    ' ''                Try
    ' ''                  if not insertuserlog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & Now.TimeOfDay.TotalSeconds))
    ' ''                    if not SetPrintedCardFlag(CardNo  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "4" & "|" & "NULL" & "|" & Now.TimeOfDay.TotalSeconds))
    ' ''                Catch ex As Exception
    ' ''                    MessageBox.Show("عدم امکان اتصال به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' ''                End Try
    ' ''                ClearFieldsFlag = True
    ' ''                e.Cancel = True
    ' ''                Exit Try
    ' ''            End If
    ' ''            pbPreview.ImageLocation = "tmp.bmp"
    ' ''            pbPreview.Load()

    ' ''            tmpAns = MangePrinterPass(1, PrinterSerialNo)
    ' ''            If Not tmpAns = "OK" Then
    ' ''                MessageBox.Show("خطا هنگام فعال سازی چاپگر " & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید" & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' ''                ClearFieldsFlag = True
    ' ''                bgwPrint.CancelAsync()
    ' ''            End If

    ' ''            If Not bgwPrint.CancellationPending Then
    ' ''                tmpAns = MagEncodeTDES(Track2, Track2, Track2)
    ' ''                If tmpAns = "OK" Then
    ' ''                    Try
    ' ''                      if not insertuserlog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Mag Encoded Successfully" & "|" & Now.TimeOfDay.TotalSeconds))
    ' ''                    Catch ex As Exception
    ' ''                        MessageBox.Show("عدم امکان اتصال به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' ''                        ClearFieldsFlag = True
    ' ''                        Exit Try
    ' ''                    End Try
    ' ''                    If Not bgwPrint.CancellationPending Then
    ' ''                        If PrintName(CardNo) Then
    ' ''                            Try
    ' ''                                if not SetPrintedCardFlag(CardNo  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "1" & "|" & "1" & "|" & Now.TimeOfDay.TotalSeconds))
    ' ''                            Catch ex As Exception
    ' ''                                MessageBox.Show("عدم امکان اتصال به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' ''                                ClearFieldsFlag = True
    ' ''                                Exit Try
    ' ''                            End Try
    ' ''                            MessageBox.Show("کارت با موفقیت صادر گردید", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information)
    ' ''                            ClearFieldsFlag = True
    ' ''                            ClearFeilds()
    ' ''                        Else
    ' ''                            e.Cancel = True
    ' ''                            ClearFieldsFlag = True
    ' ''                            Exit Try
    ' ''                        End If
    ' ''                    Else
    ' ''                        e.Cancel = True
    ' ''                        ClearFieldsFlag = True
    ' ''                        Exit Try
    ' ''                    End If
    ' ''                Else
    ' ''                    MessageBox.Show("خطا هنگام نوشتن اطلاعات در نوار مغناطیسی " & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید" & vbCrLf & tmpAns.Replace("Error-", ""), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)

    ' ''                    Try
    ' ''                      if not insertuserlog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error Encoding Mag-" & tmpAns.Replace("Error-", "") & "|" & Now.TimeOfDay.TotalSeconds))
    ' ''                        if not SetPrintedCardFlag(CardNo  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "4" & "|" & "NULL" & "|" & Now.TimeOfDay.TotalSeconds))
    ' ''                    Catch ex As Exception
    ' ''                        MessageBox.Show("عدم امکان اتصال به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' ''                    End Try
    ' ''                    ClearFieldsFlag = True
    ' ''                    bgwPrint.CancelAsync()
    ' ''                End If
    ' ''            Else

    ' ''                pbPreview.ImageLocation = ""
    ' ''                e.Cancel = True
    ' ''                ClearFieldsFlag = True
    ' ''                Exit Try
    ' ''            End If
    ' ''        Else
    ' ''            ClearFieldsFlag = False
    ' ''            e.Cancel = True
    ' ''        End If
    ' ''        pbPreview.ImageLocation = ""
    ' ''        ClearFieldsFlag = True
    ' ''        e.Cancel = True
    ' ''    Catch ex As SoapException
    ' ''        e.Cancel = True
    ' ''        ClearFieldsFlag = False
    ' ''        MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' ''    Catch ex As Exception
    ' ''        e.Cancel = True
    ' ''        ClearFieldsFlag = True
    ' ''        MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' ''    End Try

    ' ''End Sub

    Public Function IsValidSIBACheckDigit(ByVal accountNo As String) As Boolean
        IsValidSIBACheckDigit = False
        Dim sibaNo As String = String.Format("{0,13:0000000000000}", accountNo)
        Dim sumNo As Integer = Convert.ToInt32(sibaNo(11).ToString()) * 5
        sumNo = sumNo + Convert.ToInt32(sibaNo(10).ToString()) * 7
        sumNo = sumNo + Convert.ToInt32(sibaNo(9).ToString()) * 13
        sumNo = sumNo + Convert.ToInt32(sibaNo(8).ToString()) * 17
        sumNo = sumNo + Convert.ToInt32(sibaNo(7).ToString()) * 19
        sumNo = sumNo + Convert.ToInt32(sibaNo(6).ToString()) * 23
        sumNo = sumNo + Convert.ToInt32(sibaNo(5).ToString()) * 29
        sumNo = sumNo + Convert.ToInt32(sibaNo(4).ToString()) * 31
        sumNo = sumNo + Convert.ToInt32(sibaNo(3).ToString()) * 37
        sumNo = sumNo + Convert.ToInt32(sibaNo(2).ToString()) * 41
        sumNo = sumNo + Convert.ToInt32(sibaNo(1).ToString()) * 43
        sumNo = sumNo + Convert.ToInt32(sibaNo(0).ToString()) * 47
        Dim res As Integer = (sumNo Mod 11)
        If (res = 1) Then
            Return False
        Else
            Dim p As Integer = 11 - res
            If (p = 11 And Convert.ToInt32(sibaNo(12).ToString()) <> 0) Then ' if p=11 then checkDigit is 0 
                Return False
            ElseIf (p <> 11 And p <> Convert.ToInt32(sibaNo(12).ToString())) Then ' p is CheckDigit
                Return False
            End If
        End If
        Return True
    End Function

    Private Sub btnlastPrintedCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlastPrintedCard.Click
        CardPrintRee = True
        IrCardPrintRee = False
        Dim _reprint As New Reprint
        _reprint.MdiParent = CiBS_Parent
        _reprint.Show()
    End Sub

    ' ''Private Sub bgwPrint_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwPrint.RunWorkerCompleted
    ' ''    pbPreview.ImageLocation = ""
    ' ''    ClearFeilds()
    ' ''    CheckPrinterStatus()
    ' ''    'Try
    ' ''    '    tmpWSResult = CiBS_WS.GetCardRemain(Encrypt(tmpCradType(cbCardType.SelectedIndex) & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds))
    ' ''    'Catch ex As Exception
    ' ''    '    MessageBox.Show("خطا هنگام دریافت مانده کارت" & vbCrLf & "لطفاً مجدداً نوع کارت را انتخاب نمایید", "خطا")
    ' ''    '    Exit Sub
    ' ''    'End Try
    ' ''    'txtCardRemain.Text = Decrypt(tmpWSResult).Trim
    ' ''    txtCardRemain.Text = "غیر فعال در نسخه نمایشی"
    ' ''End Sub

    Sub ClearFeilds()
        If ClearFieldsFlag = True Then
            txtAccountNo.Text = ""
            txtCustName.Text = ""
            txtCustNo.Text = ""
            txtCustNoGift.Text = ""
            txtFishVarizi.Text = ""
            txtMobile.Text = ""
            chkNewCustDataGift.Checked = False
            chkPrintGiftCardAmount.Checked = True
            txtIntCode.Text = ""
            txtCustFamilyName.Text = ""
            chkSpecialText.Checked = False
            txtSpecialText.Text = ""
            IsLogoEnalbled = False
            lblPrintStatus.Text = ""
            pbPreview.ImageLocation = ""
            fnt = Nothing
            isfntselected = False
            ''''''''''''''''''''@arezoo''''''''''''
            lblMobileExp.Visible = False
            ' lblGender.Visible = False
            'rbFemale.Visible = False
            'rbFemale.Enabled = False
            'rbMale.Visible = False
            'rbMale.Enabled = False
            '''''''''''''''''''''''''''''''''''''''
        End If

    End Sub

    Function MangePrinterPass(ByVal Operation As Integer, ByVal PrinterPin As String) As String
        ' Operation: 1 --> Enable     2 --> Disable     3 --> Remove     4 --> Save
        Dim _hprn As Int32 = OpenPrinter(CurrentPrinter)
        tmpAns = ""
        Dim tmpKey As String = ""
        Try
            tmpAns = RunMyCommand(_hprn, "Rand")
        Catch ex As Exception
            Return tmpAns
        End Try
        Dim tt As String() = tmpAns.Split("")
        If Not tmpAns.Length < 1 Then
            For i As Integer = 1 To 16 - tt(0).Trim.Length
                tmpKey = "0" & tmpKey
            Next
            tmpKey = tmpKey & tt(0).Trim
            tt = GSSdp.EncryptString(PrinterPin, tmpKey).Split("")
            If Operation = 1 Then
                tmpAns = RunMyCommand(_hprn, "Pkey;E;" & tt(0))
                Return tmpAns
            End If
            If Operation = 2 Then
                tmpAns = RunMyCommand(_hprn, "Pkey;D;" & tt(0))
                Return tmpAns
            End If
            If Operation = 3 Then
                tmpAns = RunMyCommand(_hprn, "Pkey;R;" & tt(0))
                Return tmpAns
            End If
            If Operation = 4 Then
                tmpAns = RunMyCommand(_hprn, "Pkey;SE;" & tt(0))
                Return tmpAns
            End If
        Else
            Return "Error on Enabling Printer"
        End If

    End Function

    Sub GetCardRemain()
        Try
            tmpWSResult = CiBS_WS.GetCardRemain(Encrypt(tmpCradType(cbCardType.SelectedIndex) & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99"))
        Catch ex As Exception
            MessageBox.Show("خطا هنگام دریافت مانده کارت" & vbCrLf & "لطفاً مجدداً نوع کارت را انتخاب نمایید", "خطا")
            btnPrint.Enabled = False
            Exit Sub
        End Try


        txtCardRemain.Text = Decrypt(tmpWSResult).Trim
        '  به درخواست بانک کامنت شد
        ''Try
        ''    If CInt(Decrypt(tmpWSResult).Trim) = 0 Then
        ''        MessageBox.Show(" این نوع کارت در شعبه وجود ندارد، لطفاً نسبت به درخواست کارت خام اقدام نمایید ", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        ''        Exit Sub
        ''    End If
        ''    If CInt(Decrypt(tmpWSResult).Trim) < 50 Then
        ''        MessageBox.Show(" این نوع کارت در حال اتمام میباشد، لطفاً نسبت به درخواست کارت خام اقدام نمایید ", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        ''    End If
        ''Catch ex As Exception
        ''    MessageBox.Show(ex.Message)
        ''End Try
        '  به درخواست بانک کامنت شد


        'txtCardRemain.Text = "غیر فعال در نسخه نمایشی"
    End Sub

    Public Sub UpdateCardType_SelectedIndexChanged()
        rbOthers.Enabled = True

        gbLogo.Visible = False
        gbLogo.Enabled = False
        btnOldFields.Visible = False
        btnOldFields.Enabled = False
        chkSpecialText.Visible = False
        chkSpecialText.Enabled = False

        ''''''''''''@arezoo''''''''''''''''''''''''''''''''''''''''
        lblMobileExp.Visible = False
        'lblGender.Visible = False
        'lblGender.Enabled = False
        rbFemale.Visible = False
        rbFemale.Enabled = False
        rbMale.Visible = False
        rbMale.Enabled = False
        rbMale.Checked = False
        rbFemale.Checked = False
        gbGender.Visible = False

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ClearFieldsFlag = True
        ''''''''''''' Faal kon
        ClearFeilds()
        ClearFieldsFlag = False

        CusNumber = False
        Try
            tmpWSResult = CiBS_WS.GetPrintTempalte(Encrypt(tmpCradType(cbCardType.SelectedIndex) & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99"))
        Catch ex As Exception
            MessageBox.Show("خطا هنگام دریافت الگوی چاپ" & vbCrLf & "لطفاً مجدداً نوع کارت را انتخاب نمایید", "خطا")
            btnPrint.Enabled = False
            Exit Sub
        End Try
        PrintTemplate = Decrypt(tmpWSResult)
        '  GetCardRemain()
        If lblPrintingStatus.Text = "Ready" Then btnPrint.Enabled = True

        If cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Then
            ''''''''''''@arezoo''''''''''''''''''''''''''''''''''''''''
            lblMobileExp.Visible = False
            ' lblGender.Visible = False
            ' lblGender.Enabled = False
            rbFemale.Visible = False
            rbFemale.Enabled = False
            rbMale.Visible = False
            rbMale.Enabled = False
            rbMale.Checked = False
            rbFemale.Checked = False
            gbGender.Visible = False
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            txtCustNoGift.Enabled = True
            txtCustNoGift.Visible = True
            lblCustNoGift.Enabled = True
            lblCustNoGift.Visible = True
            chkNewCustDataGift.Enabled = True
            chkNewCustDataGift.Visible = True
        Else
            rbFemale.Visible = True
            rbFemale.Enabled = True
            rbMale.Visible = True
            rbMale.Enabled = True
            rbMale.Checked = False
            rbFemale.Checked = False
            gbGender.Visible = True


            txtCustNoGift.Enabled = False
            txtCustNoGift.Visible = False
            lblCustNoGift.Enabled = False
            lblCustNoGift.Visible = False
            chkNewCustDataGift.Enabled = False
            chkNewCustDataGift.Visible = False
        End If



        If cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then
            ''''@arezoo
            ''''''''''''@arezoo''''''''''''''''''''''''''''''''''''''''
            lblMobileExp.Visible = False
            'lblGender.Visible = False
            ' lblGender.Enabled = False
            rbFemale.Visible = False
            rbFemale.Enabled = False
            rbMale.Visible = False
            rbMale.Enabled = False
            rbMale.Checked = False
            rbFemale.Checked = False
            gbGender.Visible = False

            gbLogo.Visible = True
            gbLogo.Enabled = True
            btnOldFields.Visible = True
            btnOldFields.Enabled = True
            chkSpecialText.Visible = True
            chkSpecialText.Enabled = True
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            lblCustName.Text = "نام خریدار"
            lblCustFamilyName.Text = "نام خانوادگی خریدار"
            lblIntCode.Text = "کد ملی خریدار"
            lblAccNo.Text = "مبلغ کارت"
            lblMobileExp.Visible = False
            If cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then

                '''@arezoo
                ''' if we want to delete this field, so we should do not shouw custname instead
                'lblCustNo.Text = "تعداد ایران کارت"
                txtCustNo.Text = 1
                ''''''''''''@arezoo''''''''''''''''''''''''''''''''''''''''
                lblMobileExp.Visible = True
                'lblGender.Visible = True
                ' lblGender.Enabled = True
                rbFemale.Visible = True
                rbFemale.Enabled = True
                rbMale.Visible = True
                rbMale.Enabled = True
                rbMale.Checked = False
                rbFemale.Checked = False
                gbGender.Visible = True

                gbLogo.Visible = False
                gbLogo.Enabled = False
                btnOldFields.Visible = False
                btnOldFields.Enabled = False
                chkSpecialText.Visible = False
                chkSpecialText.Enabled = False
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                lblCustName.Text = "نام متقاضی"
                lblCustFamilyName.Text = "نام خانوادگی متقاضی"
                lblAccNo.Text = "مبلغ اولیه کارت"
                txtCustNo.Visible = False
                lblCustNo.Visible = False
                txtCustNo.Enabled = False
                txtAccountNo.Text = 0
                ''''''

                chkPrintGiftCardAmount.Visible = False
                chkPrintGiftCardAmount.Checked = False
                lblFisheVarizi.Visible = True
                lblMobile.Visible = True
                txtMobile.Enabled = True
                txtMobile.Visible = True
                lblMobileExp.Visible = True

                txtFishVarizi.Enabled = True
                txtFishVarizi.Visible = True


                txtFishVarizi.Enabled = False
            Else

                ''''''''''''@arezoo''''''''''''''''''''''''''''''''''''''''
                lblMobileExp.Visible = False
                'lblGender.Visible = False
                'lblGender.Enabled = False
                rbFemale.Visible = False
                rbFemale.Enabled = False
                rbMale.Visible = False
                rbMale.Enabled = False
                rbMale.Checked = False
                rbFemale.Checked = False
                gbGender.Visible = False
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                txtCustNo.Visible = True
                lblCustNo.Visible = True
                txtCustNo.Enabled = True

                lblCustNo.Text = "تعداد کارت هدیه"
                chkPrintGiftCardAmount.Visible = True
                chkPrintGiftCardAmount.Checked = True
                lblFisheVarizi.Visible = False
                lblMobile.Visible = False
                txtMobile.Enabled = False
                txtMobile.Visible = False
                lblMobileExp.Visible = False
                txtFishVarizi.Enabled = False
                txtFishVarizi.Visible = False
            End If
        Else
            ''''''''''''@arezoo''''''''''''''''''''''''''''''''''''''''
            lblMobileExp.Visible = False
            'lblGender.Visible = False
            'lblGender.Enabled = False
            rbFemale.Visible = False
            rbFemale.Enabled = False
            rbMale.Visible = False
            rbMale.Enabled = False
            rbMale.Checked = False
            rbFemale.Checked = False
            gbGender.Visible = False
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            txtCustNo.Visible = True
            lblCustNo.Visible = True
            txtCustNo.Enabled = True
            lblCustName.Text = "نام مشتری"
            lblCustFamilyName.Text = "نام خانوادگی مشتری"
            lblIntCode.Text = "کد ملی"
            lblAccNo.Text = "شماره حساب"
            txtCustNo.Text = ""
            lblCustNo.Text = "شماره مشتری"
            chkPrintGiftCardAmount.Visible = False
            lblFisheVarizi.Visible = False
            lblMobile.Visible = False
            txtMobile.Enabled = False
            txtMobile.Visible = False
            lblMobileExp.Visible = False
            txtFishVarizi.Enabled = False
            txtFishVarizi.Visible = False
        End If

        If cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then
            rbOthers.Enabled = False
        End If

    End Sub

    Private Sub cbCardType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCardType.SelectedIndexChanged
        rbOthers.Enabled = True

        gbLogo.Visible = False
        gbLogo.Enabled = False
        btnOldFields.Visible = False
        btnOldFields.Enabled = False
        chkSpecialText.Visible = False
        chkSpecialText.Enabled = False

        ''''''''''''@arezoo''''''''''''''''''''''''''''''''''''''''
        lblMobileExp.Visible = False
        'lblGender.Visible = False
        'lblGender.Enabled = False
        rbFemale.Visible = False
        rbFemale.Enabled = False
        rbMale.Visible = False
        rbMale.Enabled = False
        rbMale.Checked = False
        rbFemale.Checked = False
        gbGender.Visible = False

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ClearFieldsFlag = True
        ''''''''''''' Faal kon
        ClearFeilds()
        ClearFieldsFlag = False

        CusNumber = False
        Try
            tmpWSResult = CiBS_WS.GetPrintTempalte(Encrypt(tmpCradType(cbCardType.SelectedIndex) & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99"))
        Catch ex As Exception
            MessageBox.Show("خطا هنگام دریافت الگوی چاپ" & vbCrLf & "لطفاً مجدداً نوع کارت را انتخاب نمایید", "خطا")
            btnPrint.Enabled = False
            Exit Sub
        End Try
        PrintTemplate = Decrypt(tmpWSResult)
        '  GetCardRemain()
        If lblPrintingStatus.Text = "Ready" Then btnPrint.Enabled = True

        If cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Then
            ''''''''''''@arezoo''''''''''''''''''''''''''''''''''''''''
            lblMobileExp.Visible = False
            ' lblGender.Visible = False
            ' lblGender.Enabled = False
            rbFemale.Visible = False
            rbFemale.Enabled = False
            rbMale.Visible = False
            rbMale.Enabled = False
            rbMale.Checked = False
            rbFemale.Checked = False
            gbGender.Visible = False
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            txtCustNoGift.Enabled = True
            txtCustNoGift.Visible = True
            lblCustNoGift.Enabled = True
            lblCustNoGift.Visible = True
            chkNewCustDataGift.Enabled = True
            chkNewCustDataGift.Visible = True
        Else
            rbFemale.Visible = True
            rbFemale.Enabled = True
            rbMale.Visible = True
            rbMale.Enabled = True
            rbMale.Checked = False
            rbFemale.Checked = False
            gbGender.Visible = True


            txtCustNoGift.Enabled = False
            txtCustNoGift.Visible = False
            lblCustNoGift.Enabled = False
            lblCustNoGift.Visible = False
            chkNewCustDataGift.Enabled = False
            chkNewCustDataGift.Visible = False
        End If



        If cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then
            ''''@arezoo
            ''''''''''''@arezoo''''''''''''''''''''''''''''''''''''''''
            lblMobileExp.Visible = False
            'lblGender.Visible = False
            ' lblGender.Enabled = False
            rbFemale.Visible = False
            rbFemale.Enabled = False
            rbMale.Visible = False
            rbMale.Enabled = False
            rbMale.Checked = False
            rbFemale.Checked = False
            gbGender.Visible = False

            gbLogo.Visible = True
            gbLogo.Enabled = True
            btnOldFields.Visible = True
            btnOldFields.Enabled = True
            chkSpecialText.Visible = True
            chkSpecialText.Enabled = True
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            lblCustName.Text = "نام خریدار"
            lblCustFamilyName.Text = "نام خانوادگی خریدار"
            lblIntCode.Text = "کد ملی خریدار"
            lblAccNo.Text = "مبلغ کارت"
            lblMobileExp.Visible = False
            If cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then

                '''@arezoo
                ''' if we want to delete this field, so we should do not shouw custname instead
                'lblCustNo.Text = "تعداد ایران کارت"
                txtCustNo.Text = 1
                ''''''''''''@arezoo''''''''''''''''''''''''''''''''''''''''
                lblMobileExp.Visible = True
                'lblGender.Visible = True
                ' lblGender.Enabled = True
                rbFemale.Visible = True
                rbFemale.Enabled = True
                rbMale.Visible = True
                rbMale.Enabled = True
                rbMale.Checked = False
                rbFemale.Checked = False
                gbGender.Visible = True

                gbLogo.Visible = False
                gbLogo.Enabled = False
                btnOldFields.Visible = False
                btnOldFields.Enabled = False
                chkSpecialText.Visible = False
                chkSpecialText.Enabled = False
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                lblCustName.Text = "نام متقاضی"
                lblCustFamilyName.Text = "نام خانوادگی متقاضی"
                lblAccNo.Text = "مبلغ اولیه کارت"
                txtCustNo.Visible = False
                lblCustNo.Visible = False
                txtCustNo.Enabled = False
                txtAccountNo.Text = 0
                ''''''

                chkPrintGiftCardAmount.Visible = False
                chkPrintGiftCardAmount.Checked = False
                lblFisheVarizi.Visible = True
                lblMobile.Visible = True
                txtMobile.Enabled = True
                txtMobile.Visible = True
                lblMobileExp.Visible = True

                txtFishVarizi.Enabled = True
                txtFishVarizi.Visible = True


                txtFishVarizi.Enabled = False
            Else

                ''''''''''''@arezoo''''''''''''''''''''''''''''''''''''''''
                lblMobileExp.Visible = False
                'lblGender.Visible = False
                'lblGender.Enabled = False
                rbFemale.Visible = False
                rbFemale.Enabled = False
                rbMale.Visible = False
                rbMale.Enabled = False
                rbMale.Checked = False
                rbFemale.Checked = False
                gbGender.Visible = False
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                txtCustNo.Visible = True
                lblCustNo.Visible = True
                txtCustNo.Enabled = True

                lblCustNo.Text = "تعداد کارت هدیه"
                chkPrintGiftCardAmount.Visible = True
                chkPrintGiftCardAmount.Checked = True
                lblFisheVarizi.Visible = False
                lblMobile.Visible = False
                txtMobile.Enabled = False
                txtMobile.Visible = False
                lblMobileExp.Visible = False
                txtFishVarizi.Enabled = False
                txtFishVarizi.Visible = False
            End If
        Else
            ''''''''''''@arezoo''''''''''''''''''''''''''''''''''''''''
            lblMobileExp.Visible = False
            'lblGender.Visible = False
            'lblGender.Enabled = False
            rbFemale.Visible = False
            rbFemale.Enabled = False
            rbMale.Visible = False
            rbMale.Enabled = False
            rbMale.Checked = False
            rbFemale.Checked = False
            gbGender.Visible = False
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            txtCustNo.Visible = True
            lblCustNo.Visible = True
            txtCustNo.Enabled = True
            lblCustName.Text = "نام مشتری"
            lblCustFamilyName.Text = "نام خانوادگی مشتری"
            lblIntCode.Text = "کد ملی"
            lblAccNo.Text = "شماره حساب"
            txtCustNo.Text = ""
            lblCustNo.Text = "شماره مشتری"
            chkPrintGiftCardAmount.Visible = False
            lblFisheVarizi.Visible = False
            lblMobile.Visible = False
            txtMobile.Enabled = False
            txtMobile.Visible = False
            lblMobileExp.Visible = False
            txtFishVarizi.Enabled = False
            txtFishVarizi.Visible = False
        End If

        If cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then
            rbOthers.Enabled = False
        End If

    End Sub

    Private Sub chkSpecialText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSpecialText.CheckedChanged
        ''''@arezooh: اینهاقبلا فعال بود- هم دکمه های نمایشی غیرفعال شدند و هم اینجا
        'If chkSpecialText.Checked = True Then
        '    gbLogo.Enabled = True
        '    txtSpecialText.Text = ""
        '    IsLogoEnalbled = False
        '    pbLogo.ImageLocation = ""
        'Else
        '    gbLogo.Enabled = False
        '    txtSpecialText.Text = ""
        '    IsLogoEnalbled = False
        '    pbLogo.ImageLocation = ""
        'End If

    End Sub

    Sub ScaleImage(ByVal img As String)
        Dim PicBoxHeight As Integer
        Dim PicBoxWidth As Integer
        Dim ImageHeight As Integer
        Dim ImageWidth As Integer
        Dim TempImage As Image
        Dim scale_factor As Single

        pbLogo.SizeMode = PictureBoxSizeMode.AutoSize
        PicBoxHeight = pbLogo.Height
        PicBoxWidth = pbLogo.Width
        TempImage = Image.FromFile(img)
        ImageHeight = TempImage.Height
        ImageWidth = TempImage.Width
        scale_factor = 1.0
        If ImageHeight > PicBoxHeight Then
            scale_factor = CSng(PicBoxHeight / ImageHeight)
        End If
        If (ImageWidth * scale_factor) > PicBoxWidth Then
            scale_factor = CSng(PicBoxWidth / ImageWidth)
        End If
        pbLogo.Image = TempImage
        Dim bm_source As New Bitmap(pbLogo.Image)
        Dim bm_dest As New Bitmap( _
            CInt(bm_source.Width * scale_factor), _
            CInt(bm_source.Height * scale_factor))
        Dim gr_dest As Graphics = Graphics.FromImage(bm_dest)
        gr_dest.DrawImage(bm_source, 0, 0, _
            bm_dest.Width + 1, _
            bm_dest.Height + 1)
        pbLogo.Image = bm_dest
    End Sub

    Private Sub txtAccountNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAccountNo.Leave
        If cbCardType.SelectedItem.ToString.Contains("ران کا") And txtAccountNo.Text.Trim = "0" Then

        Else
            If txtAccountNo.Text.Length > 0 Then
                If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then
                    If txtAccountNo.Text.Length < 5 Then
                        If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
                            MessageBox.Show("مبلغ ایران کارت وارد شده می بایست مضرب 10000 باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Else
                            MessageBox.Show("مبلغ وارد شده می بایست مضرب 10000 باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                        txtAccountNo.Focus()
                        txtAccountNo.SelectAll()
                        Exit Sub
                    End If

                    If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
                        Dim a As Integer = txtAccountNo.Text.Trim
                        Dim b As Integer = 10000
                        Dim n As Integer = a Mod b
                        If n > 0 Then
                            MessageBox.Show("مبلغ ایران کارت وارد شده می بایست مضرب 10000 باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            txtAccountNo.Focus()
                            txtAccountNo.SelectAll()
                            Exit Sub
                        End If
                    End If


                    If Not IsNumeric(txtAccountNo.Text) Then
                        If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
                            MessageBox.Show("مبلغ ایران کارت فقط میتواند مقادیر عددی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Else
                            MessageBox.Show("مبلغ کارت هدیه فقط میتواند مقادیر عددی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                        txtAccountNo.Focus()
                        txtAccountNo.SelectAll()
                        Exit Sub
                    End If
                Else
                    If txtAccountNo.Text.Length <> 13 Then
                        MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        txtAccountNo.Focus()
                        txtAccountNo.SelectAll()
                        Exit Sub
                    End If
                    If Not IsNumeric(txtAccountNo.Text) Then
                        MessageBox.Show("شماره حساب فقط میتواند مقادیر عددی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        txtAccountNo.Focus()
                        txtAccountNo.SelectAll()
                        Exit Sub
                    End If
                End If

                txtFishVarizi.Enabled = True
            End If
            If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) And txtAccountNo.Text.Length > 0 Then   'نوع کارت هدیه
                Dim t As Double = txtAccountNo.Text
                txtAccountNo.Text = Format(t, "#,###")
            End If

        End If
    End Sub

    Private Sub txtCustNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCustNo.Leave
        If txtCustNo.Text.Length > 0 And lblCustNo.Text = "شماره مشتری" Then
            If txtCustNo.Text.Length < 10 Then
                MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtCustNo.Focus()
                txtCustNo.SelectAll()
                Exit Sub
            End If
            If Not IsNumeric(txtCustNo.Text) Then
                MessageBox.Show("شماره مشتری فقط میتواند مقادیر عددی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtCustNo.Focus()
                txtCustNo.SelectAll()
                Exit Sub
            End If
        End If

    End Sub


    Private Function CheckIntCode(ByVal IntCode As String) As Boolean
        Dim temp As String = "a|s|d|f|g|h|j|k|l|z"
        Dim m As String() = temp.Split("|")
        m(0) = 0
        m(1) = 0
        m(2) = 0
        m(3) = 0
        m(4) = 0
        m(5) = 0
        m(6) = 0
        m(7) = 0
        m(8) = 0
        m(9) = 0

        m(0) = IntCode.Substring(0, 1)
        m(1) = IntCode.Substring(1, 1)
        m(2) = IntCode.Substring(2, 1)
        m(3) = IntCode.Substring(3, 1)
        m(4) = IntCode.Substring(4, 1)
        m(5) = IntCode.Substring(5, 1)
        m(6) = IntCode.Substring(6, 1)
        m(7) = IntCode.Substring(7, 1)
        m(8) = IntCode.Substring(8, 1)
        m(9) = IntCode.Substring(9, 1)

        If (m(0) = m(1) And m(0) = m(2) And m(0) = m(3) And m(0) = m(4) And m(0) = m(5) And m(0) = m(6) And m(0) = m(7) And m(0) = m(8) And m(0) = m(9)) Then
            Return (False)
        End If

        Dim sum As Integer = (CInt(m(0)) * 10) + (CInt(m(1)) * 9) + (CInt(m(2)) * 8) + (CInt(m(3)) * 7) + (CInt(m(4)) * 6) + (CInt(m(5)) * 5) + (CInt(m(6)) * 4) + (CInt(m(7)) * 3) + (CInt(m(8)) * 2)

        Dim dive As Integer = sum Mod 11
        Dim Result As Integer = 11 - dive

        If dive = CInt(m(9)) Then
            Return (True)
        End If
        If Result = CInt(m(9)) Then
            Return (True)
        Else
            Return (False)
        End If

    End Function

    Private Sub txtIntCode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIntCode.Leave
        If txtIntCode.Text.Length > 0 Then
            If txtIntCode.Text.Length <> 10 And rbIranian.Checked Then
                MessageBox.Show("کد ملی وارد شده کمتر از 10 رقم می باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtIntCode.Focus()
                txtIntCode.SelectAll()
                Exit Sub
            End If

            If txtIntCode.Text.Length > 0 And rbIranian.Checked Then
                Dim ResultCheckIntCode As Boolean = CheckIntCode(txtIntCode.Text)
                If ResultCheckIntCode = False Then
                    MessageBox.Show("کد ملی وارد شده اشتباه است ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtIntCode.Focus()
                    txtIntCode.SelectAll()
                    ClearFieldsFlag = False
                    Exit Sub
                End If
            End If

            If Not IsNumeric(txtIntCode.Text) Then
                MessageBox.Show("کد ملی / کد اقامت فقط میتواند مقادیر عددی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtIntCode.Focus()
                txtIntCode.SelectAll()
                Exit Sub
            End If
        End If

    End Sub

    Private Sub txtCustName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCustName.Leave
        If txtCustName.Text.Length = 0 Then
            Exit Sub
        End If
        Dim tmpCheck As Char() = txtCustName.Text.ToCharArray
        For i As Integer = 0 To tmpCheck.Length - 1
            If IsNumeric(tmpCheck(i)) Then
                MessageBox.Show("نام وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtCustName.Focus()
                txtCustName.SelectAll()
                Exit Sub
            End If
        Next
        If txtCustName.Text.Length < 2 Then
            MessageBox.Show("نام وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtCustName.Focus()
            txtCustName.SelectAll()
            Exit Sub
        End If
    End Sub

    Private Sub txtCustFamilyName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCustFamilyName.Leave
        If txtCustFamilyName.Text.Length = 0 Then
            Exit Sub
        End If
        Dim tmpCheck As Char() = txtCustFamilyName.Text.ToCharArray
        For i As Integer = 0 To tmpCheck.Length - 1
            If IsNumeric(tmpCheck(i)) Then
                MessageBox.Show("نام خانوادگی وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtCustFamilyName.Focus()
                txtCustFamilyName.SelectAll()
            End If
        Next
        If txtCustFamilyName.Text.Length < 2 Then
            MessageBox.Show("نام خانوادگی وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtCustFamilyName.Focus()
            txtCustFamilyName.SelectAll()
        End If
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        btnPreviewClick = True
        btnPrintClick = False
        If cbCardType.SelectedIndex = -1 Then
            MessageBox.Show("لطفاً نوع کارت را مشخص نمایید", "دریافت اطلاعات")
            Exit Sub
        End If

        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
            If chkPrintGiftCardAmount.Checked And txtAccountNo.Text.Length > 4 Then
                If MakeCard("1234567890123456", "کارت هدیه" & " " & txtAccountNo.Text & " ریالی ", "1400/01", "1234", "1000", txtSpecialText.Text.Trim, "1234567891", "IR08-062-000000-020001234002") Then
                    pbPreview.ImageLocation = "tmpPreview.bmp"
                Else
                    MessageBox.Show("خطا هنگام ساخت تصویر", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                If MakeCard("1234567890123456", "", "1400/01", "1234", "1000", txtSpecialText.Text.Trim, "1234567891", "IR08-062-000000-020001234002") Then
                    pbPreview.ImageLocation = "tmpPreview.bmp"
                Else
                    MessageBox.Show("خطا هنگام ساخت تصویر", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            End If

        ElseIf (cbCardType.SelectedItem.ToString.Contains("ران کارت")) Then
            If MakeCard("1234567890123456", txtCustName.Text.Trim + " " + txtCustFamilyName.Text.Trim, "1400/01", "1234", "1000", txtSpecialText.Text.Trim, "1234567891", "IR08-062-000000-020001234002") Then
                pbPreview.ImageLocation = "tmpPreview.bmp"
            Else
                MessageBox.Show("خطا هنگام ساخت تصویر", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            'If txtAccountNo.Text.Trim = "" Then
            '    If MakeCard("1234567890123456", "", "1400/01", "1234", "1000", txtSpecialText.Text.Trim) Then
            '        pbPreview.ImageLocation = "tmp.bmp"
            '    Else
            '        MessageBox.Show("خطا هنگام ساخت تصویر", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    End If
            'Else
            '    If MakeCard("1234567890123456", "ایران کارت " & " " & txtAccountNo.Text & " ریالی ", "1400/01", "1234", "1000", txtSpecialText.Text.Trim) Then
            '        pbPreview.ImageLocation = "tmp.bmp"
            '    Else
            '        MessageBox.Show("خطا هنگام ساخت تصویر", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    End If
            'End If
        Else
            If MakeCard("1234567890123456", txtCustName.Text.Trim + " " + txtCustFamilyName.Text.Trim, "1400/01", "1234", "1000", txtSpecialText.Text.Trim, "1234567891", "IR08-062-000000-020001234002") Then
                pbPreview.ImageLocation = "tmpPreview.bmp"
            Else
                MessageBox.Show("خطا هنگام ساخت تصویر", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

    End Sub


    Private Sub btnSelectLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectLogo.Click
        IsLogoEnalbled = False
        If File.Exists("img.jpg") Then
            File.Delete("img.jpg")
        End If
        Dim tmpBrowseImage As New BrowseImage
        tmpBrowseImage.ShowDialog()
        If File.Exists("img.jpg") Then
            pbLogo.ImageLocation = "img.jpg"
            ' ScaleImage("img.jpg")
            pbLogo.Load()
            IsLogoEnalbled = True
        End If
    End Sub


    Private Sub btnSelectFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectFont.Click
        If FontDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            fnt = FontDialog1.Font
            isfntselected = True
        End If
    End Sub

    Private Sub chkPrintGiftCardAmount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPrintGiftCardAmount.CheckedChanged
        If chkPrintGiftCardAmount.Checked Then
            IsPrintAmountEnabled = True
        Else
            IsPrintAmountEnabled = False
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Dim crd As String
        Dim crd As New CardData
        crd.CustName = txtCustName.Text
        crd.CustFamilyName = txtCustFamilyName.Text
        crd.AccountNo = txtAccountNo.Text
        crd.CustNo = txtCustNo.Text
        ''cc.FishVarizi = txtFishVarizi.Text
        ''cc.Mobile = txtMobile.Text
        ''cc.IntCode = txtIntCode.Text
        crd.Iranian = rbIranian.Checked
        crd.OtherIranian = rbOthers.Checked
        crd.IntCode = txtIntCode.Text
        crd.WithAmount = RdbAmount.Checked
        crd.WithoutAmount = RdbWithoutAmount.Checked
        crd.CardTypeItem = cbCardType.SelectedItem.ToString
        crd.CardTypeIndex = cbCardType.SelectedIndex
        crd.SpecialText = txtSpecialText.Text
        crd.PrintGiftCardAmount = chkPrintGiftCardAmount.Checked
        crd.FishVarizi = txtFishVarizi.Text.Trim
        crd.Mobile = txtMobile.Text.Trim


        ''tmpAns = MangePrinterPass(1, PrinterSerialNo)
        ''If Not tmpAns = "OK" Then  'retry one more time to enable the printer
        ''    tmpAns = MangePrinterPass(1, PrinterSerialNo)
        ''End If
        ''If Not tmpAns = "OK" Then
        ''    InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error while enabling printer" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds)
        ''    MessageBox.Show("خطا هنگام فعال سازی چاپگر " & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید" & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''    ClearFieldsFlag = True
        ''    Exit Sub
        ''End If

        For j As Integer = 1 To 1 'PrintCount
            '''''''''''' Masoumeh Check Last Card
            Try
                tmpWSResult = CiBS_WS.GetStatusLastCard(Encrypt(CurrentBranchCode & "|" & "99"))
            Catch ex As Exception
                InsertLocalLog("Error Getting Status Card Data" & ex.Message)
                ClearFieldsFlag = True
                MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit For
            End Try
            tmpAns = Decrypt(tmpWSResult)
            Windows.Forms.Application.DoEvents()
            If String.Compare(tmpAns, "Timeout") = 0 Then
                MessageBox.Show("Timeout", "خطا")
                ClearFieldsFlag = True
                Exit For
            End If

            'If tmpAns.Length = 4 And tmpAns = "-999" Then
            If tmpAns = "-999" Then
                MessageBox.Show("وضعیت آخرین کارت مشخصی نمی باشد" & vbCrLf & "لطفاً به قسمت آخرین کارت صادره مراجعه نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ClearFieldsFlag = True
                Exit For
            End If

            If tmpAns.StartsWith("Unexpected Error") Then
                MessageBox.Show("بروز خطای ناشناخته" & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit For
            End If
            If tmpAns = "-111" Then 'tmpAns.Length = 4 And
                If GiftCard = True Then   'نوع کارت هدیه
                    If RdbAmount.Checked = True Then
                        RespMessage = ""
                        GetAmountFromPos()  ''Masoumeh
                        If RespMessage = "OK" Then

                        Else
                            MessageBox.Show("خطا جهت کار پوز -" & RespMessage)
                            Exit For
                        End If
                    End If
                End If
            End If

            '''''''''''''''''''''''''''''''''''''
            Try
                If crd.CardTypeItem.Contains("ران کا") Then
                    tmpWSResult = CiBS_WS.GetCardDataIranCard(Encrypt(CurrentBranchCode & "|" & crd.AccountNo & "|" & crd.CustName & "|" & crd.CustNo & "|" & crd.IntCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & tmpCradType(crd.CardTypeIndex) & "|" & crd.CustFamilyName & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & crd.SpecialText & "|" & Now.TimeOfDay.TotalSeconds & "|" & crd.FishVarizi & "|" & crd.Mobile))
                Else
                    ' tmpWSResult = CiBS_WS.GetCardData(Encrypt(CurrentBranchCode & "|" & crd.AccountNo & "|" & crd.CustName & "|" & crd.CustNo & "|" & crd.IntCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & tmpCradType(crd.CardTypeIndex) & "|" & crd.CustFamilyName & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & crd.SpecialText & "|" & Now.TimeOfDay.TotalSeconds))
                    tmpWSResult = CiBS_WS.GetCardDataGift(Encrypt(CurrentBranchCode & "|" & crd.AccountNo & "|" & crd.CustName & "|" & crd.CustNo & "|" & crd.IntCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & tmpCradType(crd.CardTypeIndex) & "|" & crd.CustFamilyName & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & crd.SpecialText & "|" & Now.TimeOfDay.TotalSeconds & "|" & FollowUp & "|" & Refrence & "|" & TrDate & "|" & TrTime & "|" & "99"))
                End If
            Catch ex As Exception
                InsertLocalLog("Error Getting Card Data" & ex.Message)
                ClearFieldsFlag = True
                MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit For
            End Try
            tmpAns = Decrypt(tmpWSResult)
            Windows.Forms.Application.DoEvents()
            If String.Compare(tmpAns, "Timeout") = 0 Then
                MessageBox.Show("Timeout", "خطا")
                ClearFieldsFlag = True
                Exit For
            End If

            If tmpAns.Length = 4 And tmpAns = "-999" Then
                MessageBox.Show("وضعیت آخرین کارت مشخصی نمی باشد" & vbCrLf & "لطفاً به قسمت آخرین کارت صادره مراجعه نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ClearFieldsFlag = True
                Exit For
            End If

            If tmpAns = "No Data" Then
                MessageBox.Show("از این نوع کارت اطلاعاتی جهت دریافت از مرکز موجود نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                ClearFieldsFlag = False
                Exit For
            End If

            If tmpAns.StartsWith("Unexpected Error") Then
                MessageBox.Show("بروز خطای ناشناخته" & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit For
            End If

            Dim tmp As String()
            ReDim tmp(tmpAns.Length - 1)
            tmp = tmpAns.Split("|")
            If tmp(8).Trim <> "5" Then
                MessageBox.Show("خطای دیتابیس ، لطفا جهت چاپ کارت، دوباره تلاش نمایید (درصورت نیاز آخرین کارت صادره را نیز بررسی نمایید)", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ClearFieldsFlag = False
                Exit For
            End If
            If tmp.Length <> 9 Then
                InsertLocalLog("Error Geting Data")
                MessageBox.Show("خطا هنگام دریافت دیتا از دیتابیس " & vbCrLf & "مجددا تلاش نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit For
            End If

            Track1 = tmp(4)
            Track2 = tmp(5)
            Track3 = tmp(6)
            CardNo = tmp(1)
            CustName = crd.CustName
            ExpDate = tmp(2)
            CVV = tmp(3)
            BrCode = CurrentBranchCode
            CustFamilyName = crd.CustFamilyName

            If Track2.Trim = "" Then
                MessageBox.Show("از این نوع کارت اطلاعاتی جهت دریافت از مرکز موجود نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                ClearFieldsFlag = False
                Exit For
            End If

            If (crd.CardTypeItem.Contains("هد") Or crd.CardTypeItem.ToLower.Contains("gift")) Then   'نوع کارت هدیه

                'If crd.WithAmount = True Then
                '    GetAmountFromPos()  ''Masoumeh
                '    If RespMessage = "OK" Then

                '    Else
                '        MessageBox.Show("خطا جهت کار پوز -" & RespMessage)
                '        Exit Function
                '    End If
                'End If

                If crd.PrintGiftCardAmount = True Then
                    If Not MakeCard(CardNo, "کارت هدیه" & " " & crd.AccountNo & " ریالی ", ExpDate, CVV, BrCode, crd.SpecialText, AccNo, sheba) Then
                        MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
                        Try
                            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            If Not SetPrintedCardFlag(CardNo & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Catch ex As Exception
                            InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                            MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                        ClearFieldsFlag = True
                        Exit For
                    End If
                Else
                    If Not MakeCard(CardNo, "", ExpDate, CVV, BrCode, crd.SpecialText, AccNo, sheba) Then
                        MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
                        Try
                            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            If Not SetPrintedCardFlag(CardNo & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Catch ex As Exception
                            InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                            MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                        ClearFieldsFlag = True
                        Exit For
                    End If
                End If

            ElseIf crd.CardTypeItem.Contains("ران کا") Then
                ' If Not MakeCard(CardNo, "کارت هدیه" & " " & txtAccountNo.Text & " ریالی ", ExpDate, CVV, BrCode, txtSpecialText.Text.Trim) Then
                If Not MakeCard(CardNo, CustName & " " & CustFamilyName, ExpDate, CVV, BrCode, crd.SpecialText, AccNo, sheba) Then
                    MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
                    Try
                        If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        If Not SetPrintedCardFlag(CardNo & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                        MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    ClearFieldsFlag = True
                    Exit For
                End If
            Else
                If Not MakeCard(CardNo, CustName & " " & CustFamilyName, ExpDate, CVV, BrCode, crd.SpecialText, AccNo, sheba) Then
                    MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
                    Try
                        If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        If Not SetPrintedCardFlag(CardNo & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
                        MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    ClearFieldsFlag = True
                    Exit For
                End If
            End If
            Windows.Forms.Application.DoEvents()
            ''  lblPrintStatus.Text = "Printing " & j & " of " & PrintCount
            pbPreview.ImageLocation = "tmp.bmp"
            pbPreview.Load()
            '' Me.Refresh()

            tmpAns = MagEncode(Track1, Track2, Track3)
            Windows.Forms.Application.DoEvents()
            If tmpAns = "OK" Then
                Try
                    If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Mag Encoded Successfully" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    InsertLocalLog("InsertLog- " & ex.Message)
                    MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = True
                    Exit For
                End Try
                Windows.Forms.Application.DoEvents()
                If PrintName(CardNo) Then
                    Windows.Forms.Application.DoEvents()
                    Try
                        If Not SetPrintedCardFlag(CardNo & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "1" & "|" & "1" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then
                            MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Else
                            If (PrintCount = 1 Or PrintCount = CounterPrint) Then MessageBox.Show("کارت با موفقیت صادر گردید", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Catch ex As Exception
                        InsertLocalLog("SetPrintedCardFlag- " & ex.Message)
                        MessageBox.Show("خطا هنگام تغییر وضعیت کارت" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = True
                        Exit For
                    End Try

                    ClearFieldsFlag = True
                    '''''@arezoo
                    rbMale.Checked = False
                    rbFemale.Checked = False

                    '   ClearFeilds()

                Else
                    rbMale.Checked = False
                    rbFemale.Checked = False

                    ClearFieldsFlag = True
                    Exit For
                End If
            Else
                If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error Encoding Mag-" & tmpAns.Replace("Error-", "") & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                If Not SetPrintedCardFlag(CardNo & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "4" & "|" & "NULL" & "|" & CurrentBranchCode & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                MessageBox.Show("خطا هنگام نوشتن اطلاعات در نوار مغناطیسی " & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید" & vbCrLf & tmpAns.Replace("Error-", ""), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = True
            End If
            pbPreview.ImageLocation = ""
            lblPrintStatus.Text = ""
            ClearFieldsFlag = True

            pbPreview.ImageLocation = ""
        Next

        EndThread = True
        '''''@arezoo: اینجا هم بزارم واسه جنیست که خالی کنه فیلد رو؟ اگه از بالاییها پرید؟  جنسیت چک شود

    End Sub

    Private Sub chkNewCustDataGift_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNewCustDataGift.CheckedChanged
        If chkNewCustDataGift.Checked Then
            Dim tmpCheckExistIntCode As String
            If txtIntCode.Text = "" Or txtIntCode.Text = " " Then
                chkNewCustDataGift.Checked = False
                MessageBox.Show("کد ملی را وارد نمایید")
                chkNewCustDataGift.Checked = False
                Exit Sub
            Else
                If chkNewCustDataGift.Checked = True Then
                    Dim tmpInsertCustData As New InsertCustData
                    tmpInsertCustData.ShowDialog()
                Else

                End If
            End If
        End If
    End Sub

    Private Sub txtCustNoGift_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCustNoGift.Leave
        If txtCustNoGift.Text.Length > 0 Then
            If txtCustNoGift.Text.Length < 10 Then
                MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtCustNoGift.Focus()
                txtCustNoGift.SelectAll()
                Exit Sub
            End If
            If Not IsNumeric(txtCustNoGift.Text) Then
                MessageBox.Show("شماره مشتری فقط میتواند مقادیر عددی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtCustNoGift.Focus()
                txtCustNoGift.SelectAll()
                Exit Sub
            End If
            '  CusNumber = True
        End If
    End Sub

    'Private Sub bgwCard_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwCard.DoWork
    '    EndThread = False
    '    Dim crd As CardData = cc
    '    tmpAns = MangePrinterPass(1, PrinterSerialNo)
    '    If Not tmpAns = "OK" Then  'retry one more time to enable the printer
    '        tmpAns = MangePrinterPass(1, PrinterSerialNo)
    '    End If
    '    If Not tmpAns = "OK" Then
    '        InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error while enabling printer" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds)
    '        MessageBox.Show("خطا هنگام فعال سازی چاپگر " & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید" & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        ClearFieldsFlag = True
    '        Exit Sub
    '    End If

    '    For j As Integer = 1 To PrintCount
    '        '''''''''''' Masoumeh Check Last Card
    '        Try
    '            tmpWSResult = CiBS_WS.GetStatusLastCard(Encrypt(CurrentBranchCode))
    '        Catch ex As Exception
    '            InsertLocalLog("Error Getting Status Card Data" & ex.Message)
    '            ClearFieldsFlag = True
    '            MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Exit For
    '        End Try
    '        tmpAns = Decrypt(tmpWSResult)
    '        Windows.Forms.Application.DoEvents()
    '        If String.Compare(tmpAns, "Timeout") = 0 Then
    '            MessageBox.Show("Timeout", "خطا")
    '            ClearFieldsFlag = True
    '            Exit For
    '        End If

    '        If tmpAns = "-999" Then 'tmpAns.Length = 4 And
    '            MessageBox.Show("وضعیت آخرین کارت مشخصی نمی باشد" & vbCrLf & "لطفاً به قسمت آخرین کارت صادره مراجعه نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            ClearFieldsFlag = True
    '            Exit For
    '        End If

    '        If tmpAns.StartsWith("Unexpected Error") Then
    '            MessageBox.Show("بروز خطای ناشناخته" & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            ClearFieldsFlag = False
    '            Exit For
    '        End If
    '        If tmpAns = "-111" Then 'tmpAns.Length = 4 And
    '            If GiftCard = True Then   'نوع کارت هدیه
    '                If RdbAmount.Checked = True Then
    '                    GetAmountFromPos()  ''Masoumeh
    '                    If RespMessage = "OK" Then

    '                    Else
    '                        MessageBox.Show("خطا جهت کار پوز -" & RespMessage)
    '                        Exit For
    '                    End If
    '                End If
    '            End If
    '        End If

    '        '''''''''''''''''''''''''''''''''''''
    '        Try
    '            If crd.CardTypeItem.Contains("ران کا") Then
    '                tmpWSResult = CiBS_WS.GetCardDataIranCard(Encrypt(CurrentBranchCode & "|" & crd.AccountNo & "|" & crd.CustName & "|" & crd.CustNo & "|" & crd.IntCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & tmpCradType(crd.CardTypeIndex) & "|" & crd.CustFamilyName & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & crd.SpecialText & "|" & Now.TimeOfDay.TotalSeconds & "|" & crd.FishVarizi & "|" & crd.Mobile))
    '            Else
    '                tmpWSResult = CiBS_WS.GetCardData(Encrypt(CurrentBranchCode & "|" & crd.AccountNo & "|" & crd.CustName & "|" & crd.CustNo & "|" & crd.IntCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & tmpCradType(crd.CardTypeIndex) & "|" & crd.CustFamilyName & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & crd.SpecialText & "|" & Now.TimeOfDay.TotalSeconds))
    '            End If
    '        Catch ex As Exception
    '            InsertLocalLog("Error Getting Card Data" & ex.Message)
    '            ClearFieldsFlag = True
    '            MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Exit For
    '        End Try
    '        tmpAns = Decrypt(tmpWSResult)
    '        Windows.Forms.Application.DoEvents()
    '        If String.Compare(tmpAns, "Timeout") = 0 Then
    '            MessageBox.Show("Timeout", "خطا")
    '            ClearFieldsFlag = True
    '            Exit For
    '        End If

    '        If tmpAns.Length = 4 And tmpAns = "-999" Then
    '            MessageBox.Show("وضعیت آخرین کارت مشخصی نمی باشد" & vbCrLf & "لطفاً به قسمت آخرین کارت صادره مراجعه نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            ClearFieldsFlag = True
    '            Exit For
    '        End If

    '        If tmpAns = "No Data" Then
    '            MessageBox.Show("از این نوع کارت اطلاعاتی جهت دریافت از مرکز موجود نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop)
    '            ClearFieldsFlag = False
    '            Exit For
    '        End If

    '        If tmpAns.StartsWith("Unexpected Error") Then
    '            MessageBox.Show("بروز خطای ناشناخته" & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            ClearFieldsFlag = False
    '            Exit For
    '        End If

    '        Dim tmp As String()
    '        ReDim tmp(tmpAns.Length - 1)
    '        tmp = tmpAns.Split("|")
    '        If tmp(8).Trim <> "5" Then
    '            MessageBox.Show("خطای دیتابیس ، لطفا جهت چاپ کارت، دوباره تلاش نمایید (درصورت نیاز آخرین کارت صادره را نیز بررسی نمایید)", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            ClearFieldsFlag = False
    '            Exit For
    '        End If
    '        If tmp.Length <> 9 Then
    '            InsertLocalLog("Error Geting Data")
    '            MessageBox.Show("خطا هنگام دریافت دیتا از دیتابیس " & vbCrLf & "مجددا تلاش نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Exit For
    '        End If

    '        Track1 = tmp(4)
    '        Track2 = tmp(5)
    '        Track3 = tmp(6)
    '        CardNo = tmp(1)
    '        CustName = crd.CustName
    '        ExpDate = tmp(2)
    '        CVV = tmp(3)
    '        BrCode = CurrentBranchCode
    '        CustFamilyName = crd.CustFamilyName

    '        If (crd.CardTypeItem.Contains("هد") Or crd.CardTypeItem.ToLower.Contains("gift")) Then   'نوع کارت هدیه

    '            'If crd.WithAmount = True Then
    '            '    GetAmountFromPos()  ''Masoumeh
    '            '    If RespMessage = "OK" Then

    '            '    Else
    '            '        MessageBox.Show("خطا جهت کار پوز -" & RespMessage)
    '            '        Exit Function
    '            '    End If
    '            'End If

    '            If crd.PrintGiftCardAmount = True Then
    '                If Not MakeCard(CardNo, "کارت هدیه" & " " & crd.AccountNo & " ریالی ", ExpDate, CVV, BrCode, crd.SpecialText) Then
    '                    MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
    '                    Try
    '                        If Not InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                        If Not SetPrintedCardFlag(CardNo  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "4" & "|" & "NULL" & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    Catch ex As Exception
    '                        InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
    '                        MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    End Try
    '                    ClearFieldsFlag = True
    '                    Exit For
    '                End If
    '            Else
    '                If Not MakeCard(CardNo, "", ExpDate, CVV, BrCode, crd.SpecialText) Then
    '                    MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
    '                    Try
    '                        If Not InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                        If Not SetPrintedCardFlag(CardNo  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "4" & "|" & "NULL" & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    Catch ex As Exception
    '                        InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
    '                        MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    End Try
    '                    ClearFieldsFlag = True
    '                    Exit For
    '                End If
    '            End If

    '        ElseIf crd.CardTypeItem.Contains("ران کا") Then
    '            ' If Not MakeCard(CardNo, "کارت هدیه" & " " & txtAccountNo.Text & " ریالی ", ExpDate, CVV, BrCode, txtSpecialText.Text.Trim) Then
    '            If Not MakeCard(CardNo, CustName & " " & CustFamilyName, ExpDate, CVV, BrCode, crd.SpecialText) Then
    '                MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
    '                Try
    '                    If Not InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    If Not SetPrintedCardFlag(CardNo  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "4" & "|" & "NULL" & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                Catch ex As Exception
    '                    InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
    '                    MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                End Try
    '                ClearFieldsFlag = True
    '                Exit For
    '            End If
    '        Else
    '            If Not MakeCard(CardNo, CustName & " " & CustFamilyName, ExpDate, CVV, BrCode, crd.SpecialText) Then
    '                MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
    '                Try
    '                    If Not InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    If Not SetPrintedCardFlag(CardNo  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "4" & "|" & "NULL" & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                Catch ex As Exception
    '                    InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
    '                    MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                End Try
    '                ClearFieldsFlag = True
    '                Exit For
    '            End If
    '        End If
    '        Windows.Forms.Application.DoEvents()
    '        ''  lblPrintStatus.Text = "Printing " & j & " of " & PrintCount
    '        pbPreview.ImageLocation = "tmp.bmp"
    '        pbPreview.Load()
    '        '' Me.Refresh()

    '        tmpAns = MagEncode(Track1, Track2, Track3)
    '        Windows.Forms.Application.DoEvents()
    '        If tmpAns = "OK" Then
    '            Try
    '                If Not InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Mag Encoded Successfully" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Catch ex As Exception
    '                InsertLocalLog("InsertLog- " & ex.Message)
    '                MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                ClearFieldsFlag = True
    '                Exit For
    '            End Try
    '            Windows.Forms.Application.DoEvents()
    '            If PrintName(CardNo) Then
    '                Windows.Forms.Application.DoEvents()
    '                Try
    '                    If Not SetPrintedCardFlag(CardNo  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "1" & "|" & "1" & "|" & Now.TimeOfDay.TotalSeconds) Then
    '                        MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    Else
    '                        If PrintCount = 1 Then MessageBox.Show("کارت با موفقیت صادر گردید", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        'EndThread = True
    '                    End If
    '                Catch ex As Exception
    '                    InsertLocalLog("SetPrintedCardFlag- " & ex.Message)
    '                    MessageBox.Show("خطا هنگام تغییر وضعیت کارت" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    ClearFieldsFlag = True
    '                    Exit For
    '                End Try

    '                ClearFieldsFlag = True
    '                '   ClearFeilds()

    '            Else
    '                ClearFieldsFlag = True
    '                Exit For
    '            End If
    '        Else
    '            If Not InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error Encoding Mag-" & tmpAns.Replace("Error-", "") & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            If Not SetPrintedCardFlag(CardNo  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "4" & "|" & "NULL" & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            MessageBox.Show("خطا هنگام نوشتن اطلاعات در نوار مغناطیسی " & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید" & vbCrLf & tmpAns.Replace("Error-", ""), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            ClearFieldsFlag = True
    '        End If
    '        pbPreview.ImageLocation = ""
    '        lblPrintStatus.Text = ""
    '        ClearFieldsFlag = True

    '        pbPreview.ImageLocation = ""
    '    Next

    '    EndThread = True

    '    ''Catch ex As Exception
    '    ''    InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error while issuing card -" & ex.Message & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds)
    '    ''    InsertLocalLog("Error in issuing Card-" & ex.Message)
    '    ''End Try
    'End Sub

    Private Sub GroupBox3_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox3.Enter

    End Sub

    Private Sub btnOldFields_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOldFields.Click
        If cbCardType.SelectedIndex > -1 Then
            If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
                txtCustName.Text = NameCustomer
                txtCustFamilyName.Text = FamilyCustomer
                txtAccountNo.Text = AccountNumber
                txtCustNo.Text = CustomerNumber
                txtCustNoGift.Text = CustomerNoGift
                txtFishVarizi.Text = FisheVarizi
                txtMobile.Text = MobileNumber
                txtIntCode.Text = IntCode
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
            NameCustomer = txtCustName.Text.Trim
            FamilyCustomer = txtCustFamilyName.Text.Trim
            AccountNumber = txtAccountNo.Text.Trim
            CustomerNumber = txtCustNo.Text.Trim
            CustomerNoGift = txtCustNoGift.Text.Trim
            FisheVarizi = txtFishVarizi.Text.Trim
            MobileNumber = txtMobile.Text.Trim
            IntCode = txtIntCode.Text.Trim
        End If
        CounterPrint = 0
        'Try

        '    tmpWSResult = CiBS_WS.GetCustomerInfo(Encrypt(txtCustNo.Text.Trim & "|" & Now.TimeOfDay.TotalSeconds))
        'Catch ex As Exception
        '    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub
        'End Try
        'If Decrypt(tmpWSResult) = 0 Then
        '    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub
        'End If


        '////////////////// بالا باید حذف گردد
        Mablagh = txtAccountNo.Text.Trim
        CustNumber = txtCustNoGift.Text
        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
            GiftCard = True
        End If
        PrintCount = 1
        If cbCardType.SelectedIndex = -1 Then
            MessageBox.Show("لطفاً نوع کارت را مشخص نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If

        ''''''''''''' Faal kon
        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then
            ' MessageBox.Show("11-" & CusNumber)
            If CusNumber = False Then
                ' MessageBox.Show("11")
                GetExistCustomerNumber()
                If CusNumber = False And txtCustNoGift.Text.Trim = "" Then
                    MessageBox.Show("شماره مشتری را وارد نمایید یا اطلاعات مشتری جدید را وارد نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If
            End If
        End If

        ''''''''''''' Faal kon
        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
            If RdbAmount.Checked = True Or RdbWithoutAmount.Checked = True Then
            Else
                MessageBox.Show("لطفاً نوع مبلغ را مشخص نمایید(بامبلغ یا بدون مبلغ)", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
        End If


        If txtIntCode.Text.Length <> 10 And rbIranian.Checked Then
            MessageBox.Show("کد ملی وارد شده کمتر از 10 رقم می باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If

        If txtIntCode.Text.Length > 0 And rbIranian.Checked Then
            Dim ResultCheckIntCode As Boolean = CheckIntCode(txtIntCode.Text)
            If ResultCheckIntCode = False Then
                MessageBox.Show("کد ملی وارد شده اشتباه است ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
        End If

        If txtCustName.Text.Length < 2 Then
            MessageBox.Show("نام وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If
        If txtCustFamilyName.Text.Length < 2 Then
            MessageBox.Show("نام خانوادگی وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If


        If Not SabteAhvalLibrary.SabteAhval.ValidateNIN(txtIntCode.Text) And rbIranian.Checked Then
            MessageBox.Show("لطفاً صحت کد ملی را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If
        If rbOthers.Checked Then
            If txtIntCode.Text.Length < 5 Then
                MessageBox.Show("لطفاً صحت کد اقامت را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
            End If
        End If


        If cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then

            If txtMobile.Text.Trim = "" Or txtMobile.Text.Trim = " " Then
                MessageBox.Show("شماره موبایل را وارد نمایید ", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            Dim mobile As String = txtMobile.Text.Trim
            ''''@arezoo: add this part of code:
            If mobile(0) <> "9" Or mobile(1) <> "8" Then
                MessageBox.Show("شماره موبایل مربوطه باید با 98 شروع شود، مثل 989121111111", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If mobile(2) <> "9" Then
                MessageBox.Show("شماره موبایل مربوطه باید با 9 شروع شود، مثل 989121111111", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
            If mobile.Length < 12 Or mobile.Length > 12 Then
                MessageBox.Show("لطفاً صحت شماره موبایل را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
            ''''@arezoo: دو تا شرط پایینی را حذف کنم؟
            'If mobile(0) <> "0" Then
            '    MessageBox.Show("شماره موبایل مربوطه میبایست با صفر شروع گردد", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    ClearFieldsFlag = False
            '    Exit Sub
            'End If
            'If mobile(1) <> "9" Then
            '    MessageBox.Show("شماره موبایل مربوطه میبایست با صفر شروع گردد", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    ClearFieldsFlag = False
            '    Exit Sub
            'End If
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            'If txtFishVarizi.Text = "" Then
            '    MessageBox.Show("شماره فیش واریزی را وارد نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    ClearFieldsFlag = False
            '    Exit Sub
            'End If
        End If

        If Not (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then   'نوع کارت هدیه

            If txtAccountNo.Text.Length <> 13 Then
                MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If txtCustNo.Text.Length < 2 Then
                MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If IsValidSIBACheckDigit(txtAccountNo.Text) = False Then
                MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
        End If

        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then   'نوع کارت هدیه
            If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then
                ' MessageBox.Show("FirstGetCustomerInfo")
                If CusNumber = False Then
                    If CustNumber.Length < 2 Then
                        MessageBox.Show("شماره مشتری را کامل وارد نمایید ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If
                    tmpWSResult = ""

                    ''''''' inja ro faal kon
                    Try
                        tmpWSResult = CiBS_WS.GetCustomerInfo(Encrypt(CustNumber.Trim & "|" & Now.TimeOfDay.TotalSeconds))
                    Catch ex As Exception
                        MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End Try
                    Dim tmpAns As String = ""
                    tmpAns = Decrypt(tmpWSResult)
                    Dim ResultGetCustom As String = Decrypt(tmpWSResult)
                    Dim ResultGetCustom1 As String() = ResultGetCustom.Split("|")
                    '///////
                    If String.Compare(ResultGetCustom1(0), "Timeout") = 0 Then
                        MessageBox.Show("هنگام بررسی شماره مشتری خطای Timeout ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If

                    If tmpAns.Contains("No Data") Then
                        MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد. خطای NO DATA ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If
                    If tmpAns.Contains("Unexpected Error") Then
                        MessageBox.Show("بررسی شماره مشتری وارد شده خطای Unexpected Error ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If

                    '//////
                    If tmpAns.Contains("0") Then
                        MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If
                End If
                'MessageBox.Show("AfterGetCustomerInfo")
                If RdbAmount.Checked = True Then
                    '///// 941107
                    'If txtAccountNo.Text.Length < 5 Then
                    '    MessageBox.Show("رقم کارت هدیه وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    ClearFieldsFlag = False
                    '    Exit Sub
                    'End If

                    'If Not txtAccountNo.Text.ToString.Replace(",", "").EndsWith("0000") Or txtAccountNo.Text.Replace(",", "") > 5000000 Or txtAccountNo.Text.Replace(",", "") < 50000 Then
                    '    MessageBox.Show("رقم کارت هدیه وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    ClearFieldsFlag = False
                    '    Exit Sub
                    'End If
                End If

            End If
            PrintCount = txtCustNo.Text
        End If

        'txtCardRemain
        ''If CInt(txtCardRemain.Text) < 1 Then
        ''    MessageBox.Show(" این نوع کارت جهت صدور در شعبه وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''    ClearFieldsFlag = False
        ''    Exit Sub
        ''End If

        ''GetAmountFromPos()  ''Masoumeh

        '////////////////////////////////

        cc.CustName = txtCustName.Text
        cc.CustFamilyName = txtCustFamilyName.Text
        cc.AccountNo = txtAccountNo.Text
        cc.CustNo = txtCustNo.Text
        ''cc.FishVarizi = txtFishVarizi.Text
        ''cc.Mobile = txtMobile.Text
        ''cc.IntCode = txtIntCode.Text
        cc.Iranian = rbIranian.Checked
        cc.OtherIranian = rbOthers.Checked
        cc.IntCode = txtIntCode.Text
        cc.WithAmount = RdbAmount.Checked
        cc.WithoutAmount = RdbWithoutAmount.Checked
        cc.CardTypeItem = cbCardType.SelectedItem.ToString
        cc.CardTypeIndex = cbCardType.SelectedIndex
        cc.SpecialText = txtSpecialText.Text
        cc.PrintGiftCardAmount = chkPrintGiftCardAmount.Checked
        cc.FishVarizi = txtFishVarizi.Text.Trim
        cc.Mobile = txtMobile.Text.Trim

        'Dim thrd As New Thread(AddressOf IssueCard)
        'thrd.Start(cc)

        'bgwCard.RunWorkerAsync()

        PrintedAllCards()

        txtCustName.Enabled = True
        txtCustFamilyName.Enabled = True
        txtAccountNo.Enabled = True
        txtCustNo.Enabled = True
        txtCustNoGift.Enabled = True
        txtIntCode.Enabled = True
        '/////////////////////////////////
        ''If EndThread = True Then
        ''    ClearFeilds()
        ''    CheckPrinterStatus()
        ''    GetCardRemain()
        ''    PrintCount = 1
        ''End If

        ''CusNumber = False

        '\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        'Try
        '    If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
        '        If RdbAmount.Checked = True Or RdbWithoutAmount.Checked = True Then
        '        Else
        '            MessageBox.Show("لطفاً نوع مبلغ را مشخص نمایید(بامبلغ یا بدون مبلغ)", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            ClearFieldsFlag = False
        '            Exit Sub
        '        End If
        '    End If

        '    Dim PrintCount As Integer = 1
        '    If cbCardType.SelectedIndex = -1 Then
        '        MessageBox.Show("لطفاً نوع کارت را مشخص نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ClearFieldsFlag = False
        '        Exit Sub
        '    End If

        '    If txtIntCode.Text.Length <> 10 And rbIranian.Checked Then
        '        MessageBox.Show("کد ملی وارد شده کمتر از 10 رقم می باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ClearFieldsFlag = False
        '        Exit Sub
        '    End If
        '    If txtCustName.Text.Length < 2 Then
        '        MessageBox.Show("نام وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ClearFieldsFlag = False
        '        Exit Sub
        '    End If
        '    If txtCustFamilyName.Text.Length < 2 Then
        '        MessageBox.Show("نام خانوادگی وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ClearFieldsFlag = False
        '        Exit Sub
        '    End If

        '    If Not SabteAhvalLibrary.SabteAhval.ValidateNIN(txtIntCode.Text) And rbIranian.Checked Then
        '        MessageBox.Show("لطفاً صحت کد ملی را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ClearFieldsFlag = False
        '        Exit Sub
        '    End If
        '    If rbOthers.Checked Then
        '        If txtIntCode.Text.Length < 5 Then
        '            MessageBox.Show("لطفاً صحت کد اقامت را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            ClearFieldsFlag = False
        '        End If
        '    End If

        '    If cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then
        '        Dim mobile As String = txtMobile.Text.Trim
        '        If mobile(0) <> "0" Then
        '            MessageBox.Show("شماره موبایل مربوطه میبایست با صفر شروع گردد", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            ClearFieldsFlag = False
        '            Exit Sub
        '        End If
        '        If mobile(1) <> "9" Then
        '            MessageBox.Show("شماره موبایل مربوطه میبایست با صفر شروع گردد", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            ClearFieldsFlag = False
        '            Exit Sub
        '        End If
        '        If mobile.Length < 11 Or mobile.Length > 11 Then
        '            MessageBox.Show("لطفاً صحت شماره موبایل را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            ClearFieldsFlag = False
        '            Exit Sub
        '        End If
        '        'If txtFishVarizi.Text = "" Then
        '        '    MessageBox.Show("شماره فیش واریزی را وارد نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        '    ClearFieldsFlag = False
        '        '    Exit Sub
        '        'End If
        '    End If

        '    If Not (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then   'نوع کارت هدیه

        '        If txtAccountNo.Text.Length <> 13 Then
        '            MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            ClearFieldsFlag = False
        '            Exit Sub
        '        End If

        '        If txtCustNo.Text.Length < 2 Then
        '            MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            ClearFieldsFlag = False
        '            Exit Sub
        '        End If

        '        If IsValidSIBACheckDigit(txtAccountNo.Text) = False Then
        '            MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            ClearFieldsFlag = False
        '            Exit Sub
        '        End If
        '    End If


        '    If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then   'نوع کارت هدیه
        '        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then

        '            If RdbAmount.Checked = True Then
        '                If txtAccountNo.Text.Length < 5 Then
        '                    MessageBox.Show("رقم کارت هدیه وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                    ClearFieldsFlag = False
        '                    Exit Sub
        '                End If

        '                If Not txtAccountNo.Text.ToString.Replace(",", "").EndsWith("0000") Or txtAccountNo.Text.Replace(",", "") > 5000000 Or txtAccountNo.Text.Replace(",", "") < 50000 Then
        '                    MessageBox.Show("رقم کارت هدیه وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                    ClearFieldsFlag = False
        '                    Exit Sub
        '                End If
        '            End If

        '        End If
        '        PrintCount = txtCustNo.Text
        '    End If

        '    If CInt(txtCardRemain.Text) < 1 Then
        '        MessageBox.Show(" این نوع کارت جهت صدور در شعبه وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ClearFieldsFlag = False
        '        Exit Sub
        '    End If

        '    tmpAns = MangePrinterPass(1, PrinterSerialNo)
        '    If Not tmpAns = "OK" Then  'retry one more time to enable the printer
        '        tmpAns = MangePrinterPass(1, PrinterSerialNo)
        '    End If
        '    If Not tmpAns = "OK" Then
        '        InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error while enabling printer" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds)
        '        MessageBox.Show("خطا هنگام فعال سازی چاپگر " & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید" & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ClearFieldsFlag = True
        '        Exit Sub
        '    End If

        '    For j As Integer = 1 To PrintCount
        '        Try
        '            If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
        '                tmpWSResult = CiBS_WS.GetCardDataIranCard(Encrypt(CurrentBranchCode & "|" & txtAccountNo.Text.Trim & "|" & txtCustName.Text.Trim & "|" & txtCustNo.Text.Trim & "|" & txtIntCode.Text.Trim  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & tmpCradType(cbCardType.SelectedIndex) & "|" & txtCustFamilyName.Text & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & txtSpecialText.Text & "|" & Now.TimeOfDay.TotalSeconds & "|" & txtFishVarizi.Text.Trim & "|" & txtMobile.Text.Trim))
        '            Else
        '                tmpWSResult = CiBS_WS.GetCardData(Encrypt(CurrentBranchCode & "|" & txtAccountNo.Text.Trim & "|" & txtCustName.Text.Trim & "|" & txtCustNo.Text.Trim & "|" & txtIntCode.Text.Trim  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & tmpCradType(cbCardType.SelectedIndex) & "|" & txtCustFamilyName.Text & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & txtSpecialText.Text & "|" & Now.TimeOfDay.TotalSeconds))
        '            End If
        '        Catch ex As Exception
        '            InsertLocalLog("Error Getting Card Data" & ex.Message)
        '            ClearFieldsFlag = True
        '            MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Exit For
        '        End Try
        '        tmpAns = Decrypt(tmpWSResult)
        '        Windows.Forms.Application.DoEvents()
        '        If String.Compare(tmpAns, "Timeout") = 0 Then
        '            MessageBox.Show("Timeout", "خطا")
        '            ClearFieldsFlag = True
        '            Exit For
        '        End If

        '        If tmpAns.Length = 4 And tmpAns = "-999" Then
        '            MessageBox.Show("وضعیت آخرین کارت مشخصی نمی باشد" & vbCrLf & "لطفاً به قسمت آخرین کارت صادره مراجعه نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '            ClearFieldsFlag = True
        '            Exit For
        '        End If


        '        If tmpAns = "No Data" Then
        '            MessageBox.Show("از این نوع کارت اطلاعاتی جهت دریافت از مرکز موجود نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        '            ClearFieldsFlag = False
        '            Exit For
        '        End If


        '        If tmpAns.StartsWith("Unexpected Error") Then
        '            MessageBox.Show("بروز خطای ناشناخته" & vbCrLf & tmpAns, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            ClearFieldsFlag = False
        '            Exit For
        '        End If

        '        Dim tmp As String()
        '        ReDim tmp(tmpAns.Length - 1)
        '        tmp = tmpAns.Split("|")
        '        If tmp(8).Trim <> "5" Then
        '            MessageBox.Show("خطای دیتابیس ، لطفا جهت چاپ کارت، دوباره تلاش نمایید (درصورت نیاز آخرین کارت صادره را نیز بررسی نمایید)", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '            ClearFieldsFlag = False
        '            Exit For
        '        End If
        '        If tmp.Length <> 9 Then
        '            InsertLocalLog("Error Geting Data")
        '            MessageBox.Show("خطا هنگام دریافت دیتا از دیتابیس " & vbCrLf & "مجددا تلاش نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Exit For
        '        End If

        '        Track1 = tmp(4)
        '        Track2 = tmp(5)
        '        Track3 = tmp(6)
        '        CardNo = tmp(1)
        '        CustName = txtCustName.Text
        '        ExpDate = tmp(2)
        '        CVV = tmp(3)
        '        BrCode = CurrentBranchCode
        '        CustFamilyName = txtCustFamilyName.Text

        '        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه

        '            If RdbAmount.Checked = True Then
        '                ' Windows.Forms.Application.DoEvents()
        '                ' lblPrintStatus.Text = "لطفا منتظر بمانید"
        '                GetAmountFromPos()  ''Masoumeh
        '                '  Windows.Forms.Application.DoEvents()
        '                If RespMessage = "OK" Then

        '                Else
        '                    MessageBox.Show("خطا جهت ارتباط پوز -" & RespMessage)
        '                    Exit Sub
        '                End If
        '            End If

        '            If chkPrintGiftCardAmount.Checked = True Then
        '                If Not MakeCard(CardNo, "کارت هدیه" & " " & txtAccountNo.Text & " ریالی ", ExpDate, CVV, BrCode, txtSpecialText.Text.Trim) Then
        '                    MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
        '                    Try
        '                        If Not InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                        If Not SetPrintedCardFlag(CardNo  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "4" & "|" & "NULL" & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                    Catch ex As Exception
        '                        InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
        '                        MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                    End Try
        '                    ClearFieldsFlag = True
        '                    Exit For
        '                End If
        '            Else
        '                If Not MakeCard(CardNo, "", ExpDate, CVV, BrCode, txtSpecialText.Text.Trim) Then
        '                    MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
        '                    Try
        '                        If Not InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                        If Not SetPrintedCardFlag(CardNo  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "4" & "|" & "NULL" & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                    Catch ex As Exception
        '                        InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
        '                        MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                    End Try
        '                    ClearFieldsFlag = True
        '                    Exit For
        '                End If
        '            End If

        '        ElseIf cbCardType.SelectedItem.ToString.Contains("ران کا") Then
        '            ' If Not MakeCard(CardNo, "کارت هدیه" & " " & txtAccountNo.Text & " ریالی ", ExpDate, CVV, BrCode, txtSpecialText.Text.Trim) Then
        '            If Not MakeCard(CardNo, CustName & " " & CustFamilyName, ExpDate, CVV, BrCode, txtSpecialText.Text.Trim) Then
        '                MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
        '                Try
        '                    If Not InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                    If Not SetPrintedCardFlag(CardNo  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "4" & "|" & "NULL" & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                Catch ex As Exception
        '                    InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
        '                    MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                End Try
        '                ClearFieldsFlag = True
        '                Exit For
        '            End If
        '        Else
        '            If Not MakeCard(CardNo, CustName & " " & CustFamilyName, ExpDate, CVV, BrCode, txtSpecialText.Text.Trim) Then
        '                MessageBox.Show("خطا هنگام ساختن تصویر کارت" & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید", "خطا")
        '                Try
        '                    If Not InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error making Card Image" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                    If Not SetPrintedCardFlag(CardNo  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "4" & "|" & "NULL" & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                Catch ex As Exception
        '                    InsertLocalLog("InsertLog-SetPrintedCardFlag- " & ex.Message)
        '                    MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                End Try
        '                ClearFieldsFlag = True
        '                Exit For
        '            End If
        '        End If
        '        Windows.Forms.Application.DoEvents()
        '        lblPrintStatus.Text = "Printing " & j & " of " & PrintCount
        '        pbPreview.ImageLocation = "tmp.bmp"
        '        pbPreview.Load()
        '        Me.Refresh()

        '        tmpAns = MagEncode(Track1, Track2, Track3)
        '        Windows.Forms.Application.DoEvents()
        '        If tmpAns = "OK" Then
        '            Try
        '                If Not InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Mag Encoded Successfully" & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Catch ex As Exception
        '                InsertLocalLog("InsertLog- " & ex.Message)
        '                MessageBox.Show("خطا هنگام ارسال لاگ" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                ClearFieldsFlag = True
        '                Exit For
        '            End Try
        '            Windows.Forms.Application.DoEvents()
        '            If PrintName(CardNo) Then
        '                Windows.Forms.Application.DoEvents()
        '                Try
        '                    If Not SetPrintedCardFlag(CardNo  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "1" & "|" & "1" & "|" & Now.TimeOfDay.TotalSeconds) Then
        '                        MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                    Else
        '                        If PrintCount = 1 Then MessageBox.Show("کارت با موفقیت صادر گردید", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                    End If
        '                Catch ex As Exception
        '                    InsertLocalLog("SetPrintedCardFlag- " & ex.Message)
        '                    MessageBox.Show("خطا هنگام تغییر وضعیت کارت" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                    ClearFieldsFlag = True
        '                    Exit For
        '                End Try

        '                ClearFieldsFlag = True
        '                '   ClearFeilds()

        '            Else
        '                ClearFieldsFlag = True
        '                Exit For
        '            End If
        '        Else
        '            If Not InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error Encoding Mag-" & tmpAns.Replace("Error-", "") & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            If Not SetPrintedCardFlag(CardNo  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & "4" & "|" & "NULL" & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام بروزرسانی وضعیت کارت", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            MessageBox.Show("خطا هنگام نوشتن اطلاعات در نوار مغناطیسی " & vbCrLf & "جهت صدور این کارت به قسمت ""آخرین کارت صادره"" مراجعه نمایید" & vbCrLf & tmpAns.Replace("Error-", ""), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            ClearFieldsFlag = True
        '        End If
        '        pbPreview.ImageLocation = ""
        '        lblPrintStatus.Text = ""
        '        ClearFieldsFlag = True

        '        pbPreview.ImageLocation = ""
        '    Next
        '    ClearFeilds()
        '    CheckPrinterStatus()
        '    GetCardRemain()
        '    PrintCount = 1

        'Catch ex As Exception
        '    InsertLog(CurrentBranchCode  & "|" & CiBS_Parent.lblLogedInUser.Text.Trim  & "|" & CardNo & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Error while issuing card -" & ex.Message & "|" & ClientIP & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds)
        '    InsertLocalLog("Error in issuing Card-" & ex.Message)

        'End Try
    End Sub



    Private Sub rbIranian_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbIranian.CheckedChanged

    End Sub

    Private Sub rbFemale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFemale.CheckedChanged

    End Sub

    'Private Sub txtMobile_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMobile.KeyPress
    '    Dim tmpMobile As String = txtMobile.Text.ToCharArray
    '    'For i As Integer = 0 To tmpCheck.Length - 1
    '    If IsNumeric(tmpMobile) Then

    '    Else

    '    End If
    '    'Next
    'End Sub

    Private Sub txtMobile_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMobile.KeyPress
        'If ((e.KeyChar >= "a" And e.KeyChar <= "z") Or (e.KeyChar >= "A" And e.KeyChar <= "Z") Or (e.KeyChar >= "آ" And e.KeyChar <= "ی")) Then
        '    e.Handled = True
        'End If

        If ((e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or Asc(e.KeyChar) = 8 Or e.KeyChar = Chr(Keys.Delete) Or e.KeyChar = Chr(Keys.Back) Or e.KeyChar = Chr(Keys.Space))) Then

        Else
            e.Handled = True
        End If

    End Sub

    Private Sub txtFishVarizi_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFishVarizi.KeyPress

        If ((e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or Asc(e.KeyChar) = 8 Or e.KeyChar = Chr(Keys.Delete) Or e.KeyChar = Chr(Keys.Back) Or e.KeyChar = Chr(Keys.Space))) Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnPreviewClick = False
        btnPrintClick = True

        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
            NameCustomer = txtCustName.Text.Trim
            FamilyCustomer = txtCustFamilyName.Text.Trim
            AccountNumber = txtAccountNo.Text.Trim
            CustomerNumber = txtCustNo.Text.Trim
            CustomerNoGift = txtCustNoGift.Text.Trim
            FisheVarizi = txtFishVarizi.Text.Trim
            MobileNumber = txtMobile.Text.Trim
            IntCode = txtIntCode.Text.Trim
        End If
        CounterPrint = 0
        'Try

        '    tmpWSResult = CiBS_WS.GetCustomerInfo(Encrypt(txtCustNo.Text.Trim & "|" & Now.TimeOfDay.TotalSeconds))
        'Catch ex As Exception
        '    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub
        'End Try
        'If Decrypt(tmpWSResult) = 0 Then
        '    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub
        'End If


        '////////////////// بالا باید حذف گردد
        Mablagh = txtAccountNo.Text.Trim
        CustNumber = txtCustNoGift.Text
        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
            GiftCard = True
        End If
        PrintCount = 1
        If cbCardType.SelectedIndex = -1 Then
            MessageBox.Show("لطفاً نوع کارت را مشخص نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If

        ''''''''''''' Faal kon
        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then
            ' MessageBox.Show("11-" & CusNumber)
            If CusNumber = False Then
                ' MessageBox.Show("11")
                GetExistCustomerNumber()
                If CusNumber = False And txtCustNoGift.Text.Trim = "" Then
                    MessageBox.Show("شماره مشتری را وارد نمایید یا اطلاعات مشتری جدید را وارد نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If
            End If
        End If

        ''''''''''''' Faal kon
        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
            If RdbAmount.Checked = True Or RdbWithoutAmount.Checked = True Then
            Else
                MessageBox.Show("لطفاً نوع مبلغ را مشخص نمایید(بامبلغ یا بدون مبلغ)", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
        End If


        If txtIntCode.Text.Length <> 10 And rbIranian.Checked Then
            MessageBox.Show("کد ملی وارد شده کمتر از 10 رقم می باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If

        If txtIntCode.Text.Length > 0 And rbIranian.Checked Then
            Dim ResultCheckIntCode As Boolean = CheckIntCode(txtIntCode.Text)
            If ResultCheckIntCode = False Then
                MessageBox.Show("کد ملی وارد شده اشتباه است ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
        End If

        If txtCustName.Text.Length < 2 Then
            MessageBox.Show("نام وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If
        If txtCustFamilyName.Text.Length < 2 Then
            MessageBox.Show("نام خانوادگی وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If


        If Not SabteAhvalLibrary.SabteAhval.ValidateNIN(txtIntCode.Text) And rbIranian.Checked Then
            MessageBox.Show("لطفاً صحت کد ملی را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If
        If rbOthers.Checked Then
            If txtIntCode.Text.Length < 5 Then
                MessageBox.Show("لطفاً صحت کد اقامت را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
            End If
        End If


        If cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then
            Dim mobile As String = txtMobile.Text.Trim
            '''@arezoo
            If mobile(0) <> "9" Or mobile(1) <> "8" Then
                MessageBox.Show("شماره موبایل مربوطه باید با 98 شروع شود، مثل 989121111111", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If mobile(2) <> "9" Then
                MessageBox.Show("دریافت اطلاعات", "شماره موبایل مربوطه باید با 9 شروع شود، مثل 989121111111", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If mobile.Length < 12 Or mobile.Length > 12 Then
                MessageBox.Show("لطفاً صحت شماره موبایل را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
            ''''@arezoo
            If rbFemale.Checked = False And rbMale.Checked = False Then
                MessageBox.Show("لطفا جنسیت را انتخاب کنید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If txtAccountNo.Text.Trim <> "0" And txtAccountNo.Text.Trim <> "" Then

                If txtFishVarizi.Text = "" Then
                    MessageBox.Show("شماره فیش واریزی را وارد نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If
            End If
        End If

        If Not (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then   'نوع کارت هدیه

            If txtAccountNo.Text.Length <> 13 Then
                MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If txtCustNo.Text.Length < 2 Then
                MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If IsValidSIBACheckDigit(txtAccountNo.Text) = False Then
                MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
        End If

        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then   'نوع کارت هدیه
            If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then
                ' MessageBox.Show("FirstGetCustomerInfo")

                If CusNumber = False Then
                    If CustNumber.Length < 2 Then
                        MessageBox.Show("شماره مشتری را کامل وارد نمایید ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If
                    tmpWSResult = ""

                    ''''''' inja ro faal kon
                    Try
                        tmpWSResult = CiBS_WS.GetCustomerInfo(Encrypt(CustNumber.Trim & "|" & Now.TimeOfDay.TotalSeconds))
                    Catch ex As Exception
                        MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End Try

                    Dim tmpAns As String = ""
                    tmpAns = Decrypt(tmpWSResult)


                    'MessageBox.Show("GetCustomerInfo Result:" & tmpAns)

                    Dim ResultGetCustom As String = Decrypt(tmpWSResult)
                    Dim ResultGetCustom1 As String() = ResultGetCustom.Split("|")
                    '///////
                    If String.Compare(ResultGetCustom1(0), "Timeout") = 0 Then
                        MessageBox.Show("هنگام بررسی شماره مشتری خطای Timeout ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If

                    If tmpAns.Contains("No Data") Then
                        MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد. خطای NO DATA ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If
                    If tmpAns.Contains("Unexpected Error") Then
                        MessageBox.Show("بررسی شماره مشتری وارد شده خطای Unexpected Error ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If

                    '//////
                    If tmpAns.Contains("0") Then
                        MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If
                End If
                'MessageBox.Show("AfterGetCustomerInfo")
                If RdbAmount.Checked = True Then
                    '///// 941107
                    'If txtAccountNo.Text.Length < 5 Then
                    '    MessageBox.Show("رقم کارت هدیه وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    ClearFieldsFlag = False
                    '    Exit Sub
                    'End If

                    'If Not txtAccountNo.Text.ToString.Replace(",", "").EndsWith("0000") Or txtAccountNo.Text.Replace(",", "") > 5000000 Or txtAccountNo.Text.Replace(",", "") < 50000 Then
                    '    MessageBox.Show("رقم کارت هدیه وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    ClearFieldsFlag = False
                    '    Exit Sub
                    'End If
                End If

            End If
            PrintCount = txtCustNo.Text

            ''''@arezoo: set 100000 for irancard  

            If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
                If txtAccountNo.Text.Trim = 0 Then

                ElseIf Not txtAccountNo.Text.ToString.Replace(",", "").EndsWith("0000") Then
                    MessageBox.Show("رقم ایران کارت باید مضربی از 10000 وارد شود ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If
            End If


        End If

        If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
            If rbFemale.Checked = False And rbMale.Checked = False Then
                MessageBox.Show("لطفا جنسیت را انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        'txtCardRemain
        'If CInt(txtCardRemain.Text) < 1 Then
        '    MessageBox.Show(" این نوع کارت جهت صدور در شعبه وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub
        'End If



        ''''''''''''''''''''''''''''''''@arezoo
        '''''check whether the customer has a irancard or not 


        ''''''@arezoo: فقط شماره شناسنامه رو چک کنه که اگر در دیتابیس بود این پیغام رو بده، درسته؟
        ''''جون وقتی که چاپ میکنیم میره شماره شناسنامه میزاره یا نه بره 
        ''''isprinted & who printed ina ro ham checkkone?
        If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
            Try
                Dim ResultCheckExistBlueCard As String = CiBS_WS.CheckExistBlueCard(txtIntCode.Text)
                If ResultCheckExistBlueCard = 1 Then
                    MessageBox.Show("همکار گرامی، مشتری قبلا ایران کارت آبی داشته اند. لطفا از گزینه صدور مجدد استفاده کنید ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Catch ex As Exception
                InsertLocalLog("Error Getting Card Data" & ex.Message)
                ClearFieldsFlag = True
                MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

        End If


        ''GetAmountFromPos()  ''Masoumeh

        '////////////////////////////////

        cc.CustName = txtCustName.Text
        cc.CustFamilyName = txtCustFamilyName.Text
        cc.AccountNo = txtAccountNo.Text
        cc.CustNo = txtCustNo.Text
        ''cc.FishVarizi = txtFishVarizi.Text
        ''cc.Mobile = txtMobile.Text
        ''cc.IntCode = txtIntCode.Text
        cc.Iranian = rbIranian.Checked
        cc.OtherIranian = rbOthers.Checked
        cc.IntCode = txtIntCode.Text
        cc.WithAmount = RdbAmount.Checked
        cc.WithoutAmount = RdbWithoutAmount.Checked
        cc.CardTypeItem = cbCardType.SelectedItem.ToString
        cc.CardTypeIndex = cbCardType.SelectedIndex
        cc.SpecialText = txtSpecialText.Text
        cc.PrintGiftCardAmount = chkPrintGiftCardAmount.Checked
        cc.FishVarizi = txtFishVarizi.Text.Trim
        cc.Mobile = txtMobile.Text.Trim
        If rbFemale.Checked Then
            cc.Gender = "1"
        Else
            cc.Gender = "0"
        End If



        'Dim thrd As New Thread(AddressOf IssueCard)
        'thrd.Start(cc)
        'bgwCard.RunWorkerAsync()

        PrintedAllCards()

        txtCustName.Enabled = True
        txtCustFamilyName.Enabled = True
        txtAccountNo.Enabled = True
        txtCustNo.Enabled = True
        txtCustNoGift.Enabled = True
        txtIntCode.Enabled = True
        ''''@arezoo: اینجا غیرفعال کنم؟- سوال
        lblMobileExp.Visible = False
        rbFemale.Checked = False
        rbMale.Checked = False

    End Sub

    Private Sub txtAccountNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAccountNo.KeyPress
        If ((e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or Asc(e.KeyChar) = 8) Or e.KeyChar = Chr(Keys.Delete) Or e.KeyChar = Chr(Keys.Back) Or e.KeyChar = Chr(Keys.Space)) Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnPreviewClick = False
        btnPrintClick = True


        If cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then
            If txtAccountNo.Text.Trim = "" Or txtAccountNo.Text.Trim = " " Then
            Else
                If txtAccountNo.Text.Trim = "0" Then
                    FisheVarizi = " "
                Else
                    If txtFishVarizi.Text.Trim = "" Then
                        MessageBox.Show("با ثبت مبلغ اولیه نیاز می باشد که حتما شماره فیش واریزی را وارد نمایید")
                        Exit Sub
                    End If
                End If
            End If
        End If

        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
            NameCustomer = txtCustName.Text.Trim
            FamilyCustomer = txtCustFamilyName.Text.Trim
            AccountNumber = txtAccountNo.Text.Trim
            CustomerNumber = txtCustNo.Text.Trim
            CustomerNoGift = txtCustNoGift.Text.Trim
            FisheVarizi = txtFishVarizi.Text.Trim
            MobileNumber = txtMobile.Text.Trim
            IntCode = txtIntCode.Text.Trim
        End If
        CounterPrint = 0
        'Try

        '    tmpWSResult = CiBS_WS.GetCustomerInfo(Encrypt(txtCustNo.Text.Trim & "|" & Now.TimeOfDay.TotalSeconds))
        'Catch ex As Exception
        '    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub
        'End Try
        'If Decrypt(tmpWSResult) = 0 Then
        '    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub
        'End If


        '////////////////// بالا باید حذف گردد
        Mablagh = txtAccountNo.Text.Trim
        CustNumber = txtCustNoGift.Text
        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
            GiftCard = True
        End If
        PrintCount = 1
        If cbCardType.SelectedIndex = -1 Then
            MessageBox.Show("لطفاً نوع کارت را مشخص نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If

        ''''''''''''' Faal kon
        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then
            ' MessageBox.Show("11-" & CusNumber)
            If CusNumber = False Then
                ' MessageBox.Show("11")
                GetExistCustomerNumber()
                If CusNumber = False And txtCustNoGift.Text.Trim = "" Then
                    MessageBox.Show("شماره مشتری را وارد نمایید یا اطلاعات مشتری جدید را وارد نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If
            End If
        End If

        ''''''''''''' Faal kon
        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
            If RdbAmount.Checked = True Or RdbWithoutAmount.Checked = True Then
            Else
                MessageBox.Show("لطفاً نوع مبلغ را مشخص نمایید(بامبلغ یا بدون مبلغ)", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
        End If


        If txtIntCode.Text.Length <> 10 And rbIranian.Checked Then
            MessageBox.Show("کد ملی وارد شده کمتر از 10 رقم می باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If

        If txtIntCode.Text.Length > 0 And rbIranian.Checked Then
            Dim ResultCheckIntCode As Boolean = CheckIntCode(txtIntCode.Text)
            If ResultCheckIntCode = False Then
                MessageBox.Show("کد ملی وارد شده اشتباه است ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
        End If

        If txtCustName.Text.Length < 2 Then
            MessageBox.Show("نام وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If
        If txtCustFamilyName.Text.Length < 2 Then
            MessageBox.Show("نام خانوادگی وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If


        If Not SabteAhvalLibrary.SabteAhval.ValidateNIN(txtIntCode.Text) And rbIranian.Checked Then
            MessageBox.Show("لطفاً صحت کد ملی را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If
        If rbOthers.Checked Then
            If txtIntCode.Text.Length < 5 Then
                MessageBox.Show("لطفاً صحت کد اقامت را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
            End If
        End If


        If cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then
            Dim mobile As String = txtMobile.Text.Trim
            '''@arezoo
            If mobile = "" Then
                MessageBox.Show("شماره موبایل را وارد نمایید")
                Exit Sub
            Else

                If mobile(0) <> "9" Or mobile(1) <> "8" Then
                    MessageBox.Show("شماره موبایل مربوطه باید با 98 شروع شود، مثل 989121111111", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If

                If mobile(2) <> "9" Then
                    MessageBox.Show("دریافت اطلاعات", "شماره موبایل مربوطه باید با 9 شروع شود، مثل 989121111111", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If

                If mobile.Length < 12 Or mobile.Length > 12 Then
                    MessageBox.Show("لطفاً صحت شماره موبایل را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If
                ''''@arezoo
            End If
            If rbFemale.Checked = False And rbMale.Checked = False Then
                MessageBox.Show("لطفا جنسیت را انتخاب کنید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If txtAccountNo.Text.Trim <> "0" And txtAccountNo.Text.Trim <> "" Then

                If txtFishVarizi.Text = "" Then
                    MessageBox.Show("شماره فیش واریزی را وارد نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If
            End If
        End If

        If Not (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then   'نوع کارت هدیه

            If txtAccountNo.Text.Length <> 13 Then
                MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If txtCustNo.Text.Length < 2 Then
                MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If IsValidSIBACheckDigit(txtAccountNo.Text) = False Then
                MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
        End If

        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then   'نوع کارت هدیه
            If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then
                ' MessageBox.Show("FirstGetCustomerInfo")

                If CusNumber = False Then
                    If CustNumber.Length < 2 Then
                        MessageBox.Show("شماره مشتری را کامل وارد نمایید ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If
                    tmpWSResult = ""

                    ''''''' inja ro faal kon
                    Try
                        tmpWSResult = CiBS_WS.GetCustomerInfo(Encrypt(CustNumber.Trim & "|" & Now.TimeOfDay.TotalSeconds))
                    Catch ex As Exception
                        MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End Try

                    Dim tmpAns As String = ""
                    tmpAns = Decrypt(tmpWSResult)


                    'MessageBox.Show("GetCustomerInfo Result:" & tmpAns)

                    Dim ResultGetCustom As String = Decrypt(tmpWSResult)
                    Dim ResultGetCustom1 As String() = ResultGetCustom.Split("|")
                    '///////
                    If String.Compare(ResultGetCustom1(0), "Timeout") = 0 Then
                        MessageBox.Show("هنگام بررسی شماره مشتری خطای Timeout ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If

                    If tmpAns.Contains("No Data") Then
                        MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد. خطای NO DATA ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If
                    If tmpAns.Contains("Unexpected Error") Then
                        MessageBox.Show("بررسی شماره مشتری وارد شده خطای Unexpected Error ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If

                    '//////
                    If tmpAns.Contains("0") Then
                        MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If
                End If
                'MessageBox.Show("AfterGetCustomerInfo")
                If RdbAmount.Checked = True Then
                    '///// 941107
                    'If txtAccountNo.Text.Length < 5 Then
                    '    MessageBox.Show("رقم کارت هدیه وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    ClearFieldsFlag = False
                    '    Exit Sub
                    'End If

                    'If Not txtAccountNo.Text.ToString.Replace(",", "").EndsWith("0000") Or txtAccountNo.Text.Replace(",", "") > 5000000 Or txtAccountNo.Text.Replace(",", "") < 50000 Then
                    '    MessageBox.Show("رقم کارت هدیه وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    ClearFieldsFlag = False
                    '    Exit Sub
                    'End If
                End If

            End If
            PrintCount = txtCustNo.Text

            ''''@arezoo: set 100000 for irancard  

            If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
                If txtAccountNo.Text.Trim = 0 Then

                ElseIf Not txtAccountNo.Text.ToString.Replace(",", "").EndsWith("0000") Then
                    MessageBox.Show("رقم ایران کارت باید مضربی از 10000 وارد شود ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If
            End If


        End If

        If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
            If rbFemale.Checked = False And rbMale.Checked = False Then
                MessageBox.Show("لطفا جنسیت را انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        'txtCardRemain
        'If CInt(txtCardRemain.Text) < 1 Then
        '    MessageBox.Show(" این نوع کارت جهت صدور در شعبه وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub
        'End If



        ''''''''''''''''''''''''''''''''@arezoo
        '''''check whether the customer has a irancard or not 


        ''''''@arezoo: فقط شماره شناسنامه رو چک کنه که اگر در دیتابیس بود این پیغام رو بده، درسته؟
        ''''جون وقتی که چاپ میکنیم میره شماره شناسنامه میزاره یا نه بره 
        ''''isprinted & who printed ina ro ham checkkone?
        If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
            Try
                Dim ResultCheckExistBlueCard As String = CiBS_WS.CheckExistBlueCard(txtIntCode.Text)
                If ResultCheckExistBlueCard = 1 Then
                    MessageBox.Show("همکار گرامی، مشتری قبلا ایران کارت آبی داشته اند. لطفا از گزینه صدور مجدد استفاده کنید ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Catch ex As Exception
                InsertLocalLog("Error Getting Card Data" & ex.Message)
                ClearFieldsFlag = True
                MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

        End If


        ''GetAmountFromPos()  ''Masoumeh

        '////////////////////////////////

        cc.CustName = txtCustName.Text
        cc.CustFamilyName = txtCustFamilyName.Text
        cc.AccountNo = txtAccountNo.Text
        cc.CustNo = txtCustNo.Text
        ''cc.FishVarizi = txtFishVarizi.Text
        ''cc.Mobile = txtMobile.Text
        ''cc.IntCode = txtIntCode.Text
        cc.Iranian = rbIranian.Checked
        cc.OtherIranian = rbOthers.Checked
        cc.IntCode = txtIntCode.Text
        cc.WithAmount = RdbAmount.Checked
        cc.WithoutAmount = RdbWithoutAmount.Checked
        cc.CardTypeItem = cbCardType.SelectedItem.ToString
        cc.CardTypeIndex = cbCardType.SelectedIndex
        cc.SpecialText = txtSpecialText.Text
        cc.PrintGiftCardAmount = chkPrintGiftCardAmount.Checked
        cc.FishVarizi = txtFishVarizi.Text.Trim
        cc.Mobile = txtMobile.Text.Trim
        If rbFemale.Checked Then
            cc.Gender = "1"
        Else
            cc.Gender = "0"
        End If



        'Dim thrd As New Thread(AddressOf IssueCard)
        'thrd.Start(cc)
        'bgwCard.RunWorkerAsync()

        PrintedAllCards()

        txtCustName.Enabled = True
        txtCustFamilyName.Enabled = True
        txtAccountNo.Enabled = True
        txtCustNo.Enabled = True
        txtCustNoGift.Enabled = True
        txtIntCode.Enabled = True
        ''''@arezoo: اینجا غیرفعال کنم؟- سوال
        lblMobileExp.Visible = False
        rbFemale.Checked = False
        rbMale.Checked = False

        'btnPreviewClick = False
        'btnPrintClick = True

        'If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
        '    NameCustomer = txtCustName.Text.Trim
        '    FamilyCustomer = txtCustFamilyName.Text.Trim
        '    AccountNumber = txtAccountNo.Text.Trim
        '    CustomerNumber = txtCustNo.Text.Trim
        '    CustomerNoGift = txtCustNoGift.Text.Trim
        '    FisheVarizi = txtFishVarizi.Text.Trim
        '    MobileNumber = txtMobile.Text.Trim
        '    IntCode = txtIntCode.Text.Trim
        'End If
        'CounterPrint = 0
        ''Try

        ''    tmpWSResult = CiBS_WS.GetCustomerInfo(Encrypt(txtCustNo.Text.Trim & "|" & Now.TimeOfDay.TotalSeconds))
        ''Catch ex As Exception
        ''    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''    ClearFieldsFlag = False
        ''    Exit Sub
        ''End Try
        ''If Decrypt(tmpWSResult) = 0 Then
        ''    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''    ClearFieldsFlag = False
        ''    Exit Sub
        ''End If


        ''////////////////// بالا باید حذف گردد
        'Mablagh = txtAccountNo.Text.Trim
        'CustNumber = txtCustNoGift.Text
        'If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
        '    GiftCard = True
        'End If
        'PrintCount = 1
        'If cbCardType.SelectedIndex = -1 Then
        '    MessageBox.Show("لطفاً نوع کارت را مشخص نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub
        'End If

        ' ''''''''''''' Faal kon
        'If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then
        '    ' MessageBox.Show("11-" & CusNumber)
        '    If CusNumber = False Then
        '        ' MessageBox.Show("11")
        '        GetExistCustomerNumber()
        '        If CusNumber = False And txtCustNoGift.Text.Trim = "" Then
        '            MessageBox.Show("شماره مشتری را وارد نمایید یا اطلاعات مشتری جدید را وارد نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            ClearFieldsFlag = False
        '            Exit Sub
        '        End If
        '    End If
        'End If

        ' ''''''''''''' Faal kon
        'If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
        '    If RdbAmount.Checked = True Or RdbWithoutAmount.Checked = True Then
        '    Else
        '        MessageBox.Show("لطفاً نوع مبلغ را مشخص نمایید(بامبلغ یا بدون مبلغ)", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ClearFieldsFlag = False
        '        Exit Sub
        '    End If
        'End If


        'If txtIntCode.Text.Length <> 10 And rbIranian.Checked Then
        '    MessageBox.Show("کد ملی وارد شده کمتر از 10 رقم می باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub
        'End If

        'If txtIntCode.Text.Length > 0 And rbIranian.Checked Then
        '    Dim ResultCheckIntCode As Boolean = CheckIntCode(txtIntCode.Text)
        '    If ResultCheckIntCode = False Then
        '        MessageBox.Show("کد ملی وارد شده اشتباه است ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ClearFieldsFlag = False
        '        Exit Sub
        '    End If
        'End If

        'If txtCustName.Text.Length < 2 Then
        '    MessageBox.Show("نام وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub
        'End If
        'If txtCustFamilyName.Text.Length < 2 Then
        '    MessageBox.Show("نام خانوادگی وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub
        'End If


        'If Not SabteAhvalLibrary.SabteAhval.ValidateNIN(txtIntCode.Text) And rbIranian.Checked Then
        '    MessageBox.Show("لطفاً صحت کد ملی را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub
        'End If
        'If rbOthers.Checked Then
        '    If txtIntCode.Text.Length < 5 Then
        '        MessageBox.Show("لطفاً صحت کد اقامت را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ClearFieldsFlag = False
        '    End If
        'End If


        'If cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then
        '    Dim mobile As String = txtMobile.Text.Trim
        '    '''@arezoo
        '    If mobile(0) <> "9" Or mobile(1) <> "8" Then
        '        MessageBox.Show("دریافت اطلاعات", "شماره موبایل مربوطه باید با 98 شروع شود، مثل 989121111111", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ClearFieldsFlag = False
        '        Exit Sub
        '    End If

        '    If mobile(2) <> "9" Then
        '        MessageBox.Show("دریافت اطلاعات", "شماره موبایل مربوطه باید با 9 شروع شود، مثل 989121111111", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ClearFieldsFlag = False
        '        Exit Sub
        '    End If

        '    If mobile.Length < 12 Or mobile.Length > 12 Then
        '        MessageBox.Show("لطفاً صحت شماره موبایل را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ClearFieldsFlag = False
        '        Exit Sub
        '    End If
        '    ''''@arezoo
        '    If rbFemale.Checked = False And rbMale.Checked = False Then
        '        MessageBox.Show("لطفا جنسیت را انتخاب کنید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ClearFieldsFlag = False
        '        Exit Sub
        '    End If

        '    If txtAccountNo.Text.Trim <> "0" And txtAccountNo.Text.Trim <> "" Then

        '        If txtFishVarizi.Text = "" Then
        '            MessageBox.Show("شماره فیش واریزی را وارد نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            ClearFieldsFlag = False
        '            Exit Sub
        '        End If
        '    End If
        'End If

        'If Not (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then   'نوع کارت هدیه

        '    If txtAccountNo.Text.Length <> 13 Then
        '        MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ClearFieldsFlag = False
        '        Exit Sub
        '    End If

        '    If txtCustNo.Text.Length < 2 Then
        '        MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ClearFieldsFlag = False
        '        Exit Sub
        '    End If

        '    If IsValidSIBACheckDigit(txtAccountNo.Text) = False Then
        '        MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ClearFieldsFlag = False
        '        Exit Sub
        '    End If
        'End If

        'If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then   'نوع کارت هدیه
        '    If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then
        '        ' MessageBox.Show("FirstGetCustomerInfo")

        '        If CusNumber = False Then
        '            If CustNumber.Length < 2 Then
        '                MessageBox.Show("شماره مشتری را کامل وارد نمایید ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                ClearFieldsFlag = False
        '                Exit Sub
        '            End If
        '            tmpWSResult = ""

        '            ''''''' inja ro faal kon
        '            Try
        '                tmpWSResult = CiBS_WS.GetCustomerInfo(Encrypt(CustNumber.Trim & "|" & Now.TimeOfDay.TotalSeconds))
        '            Catch ex As Exception
        '                MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                ClearFieldsFlag = False
        '                Exit Sub
        '            End Try

        '            Dim tmpAns As String = ""
        '            tmpAns = Decrypt(tmpWSResult)


        '            'MessageBox.Show("GetCustomerInfo Result:" & tmpAns)

        '            Dim ResultGetCustom As String = Decrypt(tmpWSResult)
        '            Dim ResultGetCustom1 As String() = ResultGetCustom.Split("|")
        '            '///////
        '            If String.Compare(ResultGetCustom1(0), "Timeout") = 0 Then
        '                MessageBox.Show("هنگام بررسی شماره مشتری خطای Timeout ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                ClearFieldsFlag = False
        '                Exit Sub
        '            End If

        '            If tmpAns.Contains("No Data") Then
        '                MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد. خطای NO DATA ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                ClearFieldsFlag = False
        '                Exit Sub
        '            End If
        '            If tmpAns.Contains("Unexpected Error") Then
        '                MessageBox.Show("بررسی شماره مشتری وارد شده خطای Unexpected Error ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                ClearFieldsFlag = False
        '                Exit Sub
        '            End If

        '            '//////
        '            If tmpAns.Contains("0") Then
        '                MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                ClearFieldsFlag = False
        '                Exit Sub
        '            End If
        '        End If
        '        'MessageBox.Show("AfterGetCustomerInfo")
        '        If RdbAmount.Checked = True Then
        '            '///// 941107
        '            'If txtAccountNo.Text.Length < 5 Then
        '            '    MessageBox.Show("رقم کارت هدیه وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            '    ClearFieldsFlag = False
        '            '    Exit Sub
        '            'End If

        '            'If Not txtAccountNo.Text.ToString.Replace(",", "").EndsWith("0000") Or txtAccountNo.Text.Replace(",", "") > 5000000 Or txtAccountNo.Text.Replace(",", "") < 50000 Then
        '            '    MessageBox.Show("رقم کارت هدیه وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            '    ClearFieldsFlag = False
        '            '    Exit Sub
        '            'End If
        '        End If

        '    End If
        '    PrintCount = txtCustNo.Text

        '    ''''@arezoo: set 100000 for irancard  

        '    If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
        '        If txtAccountNo.Text.Trim = 0 Then

        '        ElseIf Not txtAccountNo.Text.ToString.Replace(",", "").EndsWith("0000") Then
        '            MessageBox.Show("رقم ایران کارت باید مضربی از 10000 وارد شود ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            ClearFieldsFlag = False
        '            Exit Sub
        '        End If
        '    End If


        'End If

        'If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
        '    If rbFemale.Checked = False And rbMale.Checked = False Then
        '        MessageBox.Show("لطفا جنسیت را انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Exit Sub
        '    End If
        'End If

        'If CInt(txtCardRemain.Text) < 1 Then
        '    MessageBox.Show(" این نوع کارت جهت صدور در شعبه وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub
        'End If



        ' ''''''''''''''''''''''''''''''''@arezoo
        ' '''''check whether the customer has a irancard or not 


        ' ''''''@arezoo: فقط شماره شناسنامه رو چک کنه که اگر در دیتابیس بود این پیغام رو بده، درسته؟
        ' ''''جون وقتی که چاپ میکنیم میره شماره شناسنامه میزاره یا نه بره 
        ' ''''isprinted & who printed ina ro ham checkkone?
        'If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
        '    Try
        '        Dim ResultCheckExistBlueCard As String = CiBS_WS.CheckExistBlueCard(txtIntCode.Text)
        '        If ResultCheckExistBlueCard = 1 Then
        '            MessageBox.Show("همکار گرامی، مشتری قبلا ایران کارت آبی داشته اند. لطفا از گزینه صدور مجدد استفاده کنید ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Exit Sub
        '        End If
        '    Catch ex As Exception
        '        InsertLocalLog("Error Getting Card Data" & ex.Message)
        '        ClearFieldsFlag = True
        '        MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Exit Sub
        '    End Try

        'End If


        ' ''GetAmountFromPos()  ''Masoumeh

        ''////////////////////////////////

        'cc.CustName = txtCustName.Text
        'cc.CustFamilyName = txtCustFamilyName.Text
        'cc.AccountNo = txtAccountNo.Text
        'cc.CustNo = txtCustNo.Text
        ' ''cc.FishVarizi = txtFishVarizi.Text
        ' ''cc.Mobile = txtMobile.Text
        ' ''cc.IntCode = txtIntCode.Text
        'cc.Iranian = rbIranian.Checked
        'cc.OtherIranian = rbOthers.Checked
        'cc.IntCode = txtIntCode.Text
        'cc.WithAmount = RdbAmount.Checked
        'cc.WithoutAmount = RdbWithoutAmount.Checked
        'cc.CardTypeItem = cbCardType.SelectedItem.ToString
        'cc.CardTypeIndex = cbCardType.SelectedIndex
        'cc.SpecialText = txtSpecialText.Text
        'cc.PrintGiftCardAmount = chkPrintGiftCardAmount.Checked
        'cc.FishVarizi = txtFishVarizi.Text.Trim
        'cc.Mobile = txtMobile.Text.Trim
        'If rbFemale.Checked Then
        '    cc.Gender = "1"
        'Else
        '    cc.Gender = "0"
        'End If



        ''Dim thrd As New Thread(AddressOf IssueCard)
        ''thrd.Start(cc)
        ''bgwCard.RunWorkerAsync()

        'PrintedAllCards()

        'txtCustName.Enabled = True
        'txtCustFamilyName.Enabled = True
        'txtAccountNo.Enabled = True
        'txtCustNo.Enabled = True
        'txtCustNoGift.Enabled = True
        'txtIntCode.Enabled = True
        ' ''''@arezoo: اینجا غیرفعال کنم؟- سوال
        'lblMobileExp.Visible = False
        'rbFemale.Checked = False
        'rbMale.Checked = False

        ''lblGender.Visible = False
        ''rbFemale.Visible = False
        ''rbMale.Visible = False
        ''rbFemale.Enabled = False
        ''rbMale.Enabled = False
        '' gbGender.Visible = False


    End Sub

    Private Sub btnPrint_ClientSizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.ClientSizeChanged

    End Sub

  
    Private Sub txtIntCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIntCode.KeyPress
        If ((e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or Asc(e.KeyChar) = 8 Or e.KeyChar = Chr(Keys.Delete) Or e.KeyChar = Chr(Keys.Back) Or e.KeyChar = Chr(Keys.Space))) Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtCustNoGift_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCustNoGift.KeyPress
        If ((e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or Asc(e.KeyChar) = 8 Or e.KeyChar = Chr(Keys.Delete) Or e.KeyChar = Chr(Keys.Back) Or e.KeyChar = Chr(Keys.Space))) Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtCustNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCustNo.KeyPress
        If ((e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or Asc(e.KeyChar) = 8 Or e.KeyChar = Chr(Keys.Delete) Or e.KeyChar = Chr(Keys.Back) Or e.KeyChar = Chr(Keys.Space))) Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtCustName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCustName.KeyPress
        If Not Char.IsLetter(e.KeyChar) And Not e.KeyChar = Chr(Keys.Delete) And Not e.KeyChar = Chr(Keys.Back) And Not e.KeyChar = Chr(Keys.Space) Then

            e.Handled = True

        End If



    End Sub

    Private Sub txtCustFamilyName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCustFamilyName.KeyPress
        If Not Char.IsLetter(e.KeyChar) And Not e.KeyChar = Chr(Keys.Delete) And Not e.KeyChar = Chr(Keys.Back) And Not e.KeyChar = Chr(Keys.Space) Then

            e.Handled = True

        End If
    End Sub

    Private Sub txtMobile_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMobile.Leave
        Dim mobile As String = txtMobile.Text.Trim
        '''@arezoo
        If mobile = "" Then
            MessageBox.Show("شماره موبایل را وارد نمایید")
            Exit Sub
        Else

            If mobile(0) <> "9" Or mobile(1) <> "8" Then
                MessageBox.Show("شماره موبایل مربوطه باید با 98 شروع شود، مثل 989121111111", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If mobile(2) <> "9" Then
                MessageBox.Show("دریافت اطلاعات", "شماره موبایل مربوطه باید با 9 شروع شود، مثل 989121111111", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If mobile.Length < 12 Or mobile.Length > 12 Then
                MessageBox.Show("لطفاً صحت شماره موبایل را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
            ''''@arezoo
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        btnPreviewClick = False
        btnPrintClick = True

        If cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then

            If txtAccountNo.Text.Trim = "" Or txtAccountNo.Text.Trim = " " Then
            Else
                If txtAccountNo.Text.Trim = "0" Then
                    FisheVarizi = " "
                Else
                    If txtFishVarizi.Text.Trim = "" Then
                        MessageBox.Show("با ثبت مبلغ اولیه نیاز می باشد که حتما شماره فیش واریزی را وارد نمایید")
                        Exit Sub
                    End If
                End If
            End If
        End If

        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
            NameCustomer = txtCustName.Text.Trim
            FamilyCustomer = txtCustFamilyName.Text.Trim
            AccountNumber = txtAccountNo.Text.Trim
            CustomerNumber = txtCustNo.Text.Trim
            CustomerNoGift = txtCustNoGift.Text.Trim
            FisheVarizi = txtFishVarizi.Text.Trim
            MobileNumber = txtMobile.Text.Trim
            IntCode = txtIntCode.Text.Trim
        End If
        CounterPrint = 0
        'Try

        '    tmpWSResult = CiBS_WS.GetCustomerInfo(Encrypt(txtCustNo.Text.Trim & "|" & Now.TimeOfDay.TotalSeconds))
        'Catch ex As Exception
        '    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub

        'End Try
        'If Decrypt(tmpWSResult) = 0 Then
        '    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub
        'End If


        '////////////////// بالا باید حذف گردد
        Mablagh = txtAccountNo.Text.Trim
        CustNumber = txtCustNoGift.Text
        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
            GiftCard = True
        End If
        PrintCount = 1
        If cbCardType.SelectedIndex = -1 Then
            MessageBox.Show("لطفاً نوع کارت را مشخص نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If

        ''''''''''''' Faal kon
        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then
            ' MessageBox.Show("11-" & CusNumber)
            If CusNumber = False Then
                ' MessageBox.Show("11")
                GetExistCustomerNumber()
                If CusNumber = False And txtCustNoGift.Text.Trim = "" Then
                    MessageBox.Show("شماره مشتری را وارد نمایید یا اطلاعات مشتری جدید را وارد نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If
            End If
        End If

        ''''''''''''' Faal kon
        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
            If RdbAmount.Checked = True Or RdbWithoutAmount.Checked = True Then
            Else
                MessageBox.Show("لطفاً نوع مبلغ را مشخص نمایید(بامبلغ یا بدون مبلغ)", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
        End If


        If txtIntCode.Text.Length <> 10 And rbIranian.Checked Then
            MessageBox.Show("کد ملی وارد شده کمتر از 10 رقم می باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If

        If txtIntCode.Text.Length > 0 And rbIranian.Checked Then
            Dim ResultCheckIntCode As Boolean = CheckIntCode(txtIntCode.Text)
            If ResultCheckIntCode = False Then
                MessageBox.Show("کد ملی وارد شده اشتباه است ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
        End If

        If txtCustName.Text.Length < 2 Then
            MessageBox.Show("نام وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If
        If txtCustFamilyName.Text.Length < 2 Then
            MessageBox.Show("نام خانوادگی وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If


        If Not SabteAhvalLibrary.SabteAhval.ValidateNIN(txtIntCode.Text) And rbIranian.Checked Then
            MessageBox.Show("لطفاً صحت کد ملی را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If
        If rbOthers.Checked Then
            If txtIntCode.Text.Length < 5 Then
                MessageBox.Show("لطفاً صحت کد اقامت را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
            End If
        End If


        If cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then
            Dim mobile As String = txtMobile.Text.Trim
            '''@arezoo
            If mobile = "" Then
                MessageBox.Show("شماره موبایل را وارد نمایید")
                Exit Sub
            Else

                If mobile(0) <> "9" Or mobile(1) <> "8" Then
                    MessageBox.Show("شماره موبایل مربوطه باید با 98 شروع شود، مثل 989121111111", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If

                If mobile(2) <> "9" Then
                    MessageBox.Show("دریافت اطلاعات", "شماره موبایل مربوطه باید با 9 شروع شود، مثل 989121111111", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If

                If mobile.Length < 12 Or mobile.Length > 12 Then
                    MessageBox.Show("لطفاً صحت شماره موبایل را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If
                ''''@arezoo
            End If
            If rbFemale.Checked = False And rbMale.Checked = False Then
                MessageBox.Show("لطفا جنسیت را انتخاب کنید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If txtAccountNo.Text.Trim <> "0" And txtAccountNo.Text.Trim <> "" Then

                If txtFishVarizi.Text = "" Then
                    MessageBox.Show("شماره فیش واریزی را وارد نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If
            End If
        End If

        If Not (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then   'نوع کارت هدیه

            If txtAccountNo.Text.Length <> 13 Then
                MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If txtCustNo.Text.Length < 2 Then
                MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If IsValidSIBACheckDigit(txtAccountNo.Text) = False Then
                MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
        End If

        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then   'نوع کارت هدیه
            If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then
                ' MessageBox.Show("FirstGetCustomerInfo")

                If CusNumber = False Then
                    If CustNumber.Length < 2 Then
                        MessageBox.Show("شماره مشتری را کامل وارد نمایید ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If
                    tmpWSResult = ""

                    ''''''' inja ro faal kon
                    Try
                        tmpWSResult = CiBS_WS.GetCustomerInfo(Encrypt(CustNumber.Trim & "|" & Now.TimeOfDay.TotalSeconds))
                    Catch ex As Exception
                        MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End Try

                    Dim tmpAns As String = ""
                    tmpAns = Decrypt(tmpWSResult)


                    'MessageBox.Show("GetCustomerInfo Result:" & tmpAns)

                    Dim ResultGetCustom As String = Decrypt(tmpWSResult)
                    Dim ResultGetCustom1 As String() = ResultGetCustom.Split("|")
                    '///////
                    If String.Compare(ResultGetCustom1(0), "Timeout") = 0 Then
                        MessageBox.Show("هنگام بررسی شماره مشتری خطای Timeout ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If

                    If tmpAns.Contains("No Data") Then
                        MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد. خطای NO DATA ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If
                    If tmpAns.Contains("Unexpected Error") Then
                        MessageBox.Show("بررسی شماره مشتری وارد شده خطای Unexpected Error ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If

                    '//////
                    If tmpAns.Contains("0") Then
                        MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If
                End If
                'MessageBox.Show("AfterGetCustomerInfo")
                If RdbAmount.Checked = True Then
                    '///// 941107
                    'If txtAccountNo.Text.Length < 5 Then
                    '    MessageBox.Show("رقم کارت هدیه وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    ClearFieldsFlag = False
                    '    Exit Sub
                    'End If

                    'If Not txtAccountNo.Text.ToString.Replace(",", "").EndsWith("0000") Or txtAccountNo.Text.Replace(",", "") > 5000000 Or txtAccountNo.Text.Replace(",", "") < 50000 Then
                    '    MessageBox.Show("رقم کارت هدیه وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    ClearFieldsFlag = False
                    '    Exit Sub
                    'End If
                End If

            End If
            PrintCount = txtCustNo.Text

            ''''@arezoo: set 100000 for irancard  

            If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
                If txtAccountNo.Text.Trim = 0 Then

                ElseIf Not txtAccountNo.Text.ToString.Replace(",", "").EndsWith("0000") Then
                    MessageBox.Show("رقم ایران کارت باید مضربی از 10000 وارد شود ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If
            End If
        End If

        If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
            If rbFemale.Checked = False And rbMale.Checked = False Then
                MessageBox.Show("لطفا جنسیت را انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        'txtCardRemain
        'If CInt(txtCardRemain.Text) < 1 Then
        '    MessageBox.Show(" این نوع کارت جهت صدور در شعبه وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub
        'End If



        ''''''''''''''''''''''''''''''''@arezoo
        '''''check whether the customer has a irancard or not 


        ''''''@arezoo: فقط شماره شناسنامه رو چک کنه که اگر در دیتابیس بود این پیغام رو بده، درسته؟
        ''''جون وقتی که چاپ میکنیم میره شماره شناسنامه میزاره یا نه بره 
        ''''isprinted & who printed ina ro ham checkkone?
        If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
            Try
                Dim ResultCheckExistBlueCard As String = CiBS_WS.CheckExistBlueCard(txtIntCode.Text & "|" & "99")
                If ResultCheckExistBlueCard = 1 Then
                    MessageBox.Show("همکار گرامی، مشتری قبلا ایران کارت آبی داشته اند. لطفا از گزینه صدور مجدد استفاده کنید ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Catch ex As Exception
                InsertLocalLog("Error Getting Card Data" & ex.Message)
                ClearFieldsFlag = True
                MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

        End If


        ''GetAmountFromPos()  ''Masoumeh

        '////////////////////////////////

        cc.CustName = txtCustName.Text
        cc.CustFamilyName = txtCustFamilyName.Text
        cc.AccountNo = txtAccountNo.Text
        cc.CustNo = txtCustNo.Text
        ''cc.FishVarizi = txtFishVarizi.Text
        ''cc.Mobile = txtMobile.Text
        ''cc.IntCode = txtIntCode.Text
        cc.Iranian = rbIranian.Checked
        cc.OtherIranian = rbOthers.Checked
        cc.IntCode = txtIntCode.Text
        cc.WithAmount = RdbAmount.Checked
        cc.WithoutAmount = RdbWithoutAmount.Checked
        cc.CardTypeItem = cbCardType.SelectedItem.ToString
        cc.CardTypeIndex = cbCardType.SelectedIndex
        cc.SpecialText = txtSpecialText.Text
        cc.PrintGiftCardAmount = chkPrintGiftCardAmount.Checked
        cc.FishVarizi = txtFishVarizi.Text.Trim
        cc.Mobile = txtMobile.Text.Trim
        If rbFemale.Checked Then
            cc.Gender = "1"
        Else
            cc.Gender = "0"
        End If


        'Dim thrd As New Thread(AddressOf IssueCard)
        'thrd.Start(cc)
        'bgwCard.RunWorkerAsync()

        PrintedAllCards()

        txtCustName.Enabled = True
        txtCustFamilyName.Enabled = True
        txtAccountNo.Enabled = True
        txtCustNo.Enabled = True
        txtCustNoGift.Enabled = True
        txtIntCode.Enabled = True
        lblMobileExp.Visible = False
        rbFemale.Checked = False
        rbMale.Checked = False
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''
        UpdateCardType_SelectedIndexChanged()

    End Sub


    Private Sub Button3_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        btnPreviewClick = False
        btnPrintClick = True

        If cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then
            If txtAccountNo.Text.Trim = "" Or txtAccountNo.Text.Trim = " " Then
            Else
                If txtAccountNo.Text.Trim = "0" Then
                    FisheVarizi = " "
                Else
                    If txtFishVarizi.Text.Trim = "" Then
                        MessageBox.Show("با ثبت مبلغ اولیه نیاز می باشد که حتما شماره فیش واریزی را وارد نمایید")
                        Exit Sub
                    End If
                End If
            End If
        End If

        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
            NameCustomer = txtCustName.Text.Trim
            FamilyCustomer = txtCustFamilyName.Text.Trim
            AccountNumber = txtAccountNo.Text.Trim
            CustomerNumber = txtCustNo.Text.Trim
            CustomerNoGift = txtCustNoGift.Text.Trim
            FisheVarizi = txtFishVarizi.Text.Trim
            MobileNumber = txtMobile.Text.Trim
            IntCode = txtIntCode.Text.Trim
        End If
        CounterPrint = 0
        'Try

        '    tmpWSResult = CiBS_WS.GetCustomerInfo(Encrypt(txtCustNo.Text.Trim & "|" & Now.TimeOfDay.TotalSeconds))
        'Catch ex As Exception
        '    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub

        'End Try
        'If Decrypt(tmpWSResult) = 0 Then
        '    MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub
        'End If


        '////////////////// بالا باید حذف گردد
        Mablagh = txtAccountNo.Text.Trim
        CustNumber = txtCustNoGift.Text
        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
            GiftCard = True
        End If
        PrintCount = 1
        If cbCardType.SelectedIndex = -1 Then
            MessageBox.Show("لطفاً نوع کارت را مشخص نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If

        ''''''''''''' Faal kon
        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then
            ' MessageBox.Show("11-" & CusNumber)
            If CusNumber = False Then
                ' MessageBox.Show("11")
                GetExistCustomerNumber()
                If CusNumber = False And txtCustNoGift.Text.Trim = "" Then
                    MessageBox.Show("شماره مشتری را وارد نمایید یا اطلاعات مشتری جدید را وارد نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If
            End If
        End If

        ''''''''''''' Faal kon
        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then   'نوع کارت هدیه
            If RdbAmount.Checked = True Or RdbWithoutAmount.Checked = True Then
            Else
                MessageBox.Show("لطفاً نوع مبلغ را مشخص نمایید(بامبلغ یا بدون مبلغ)", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
        End If


        If txtIntCode.Text.Length <> 10 And rbIranian.Checked Then
            MessageBox.Show("کد ملی وارد شده کمتر از 10 رقم می باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If

        If txtIntCode.Text.Length > 0 And rbIranian.Checked Then
            Dim ResultCheckIntCode As Boolean = CheckIntCode(txtIntCode.Text)
            If ResultCheckIntCode = False Then
                MessageBox.Show("کد ملی وارد شده اشتباه است ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
        End If

        If txtCustName.Text.Length < 2 Then
            MessageBox.Show("نام وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If
        If txtCustFamilyName.Text.Length < 2 Then
            MessageBox.Show("نام خانوادگی وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If


        If Not SabteAhvalLibrary.SabteAhval.ValidateNIN(txtIntCode.Text) And rbIranian.Checked Then
            MessageBox.Show("لطفاً صحت کد ملی را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearFieldsFlag = False
            Exit Sub
        End If
        If rbOthers.Checked Then
            If txtIntCode.Text.Length < 5 Then
                MessageBox.Show("لطفاً صحت کد اقامت را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
            End If
        End If


        If cbCardType.SelectedItem.ToString.ToLower.Contains("ران کا") Then
            Dim mobile As String = txtMobile.Text.Trim
            '''@arezoo
            If mobile = "" Then
                MessageBox.Show("شماره موبایل را وارد نمایید")
                Exit Sub
            Else

                If mobile(0) <> "9" Or mobile(1) <> "8" Then
                    MessageBox.Show("شماره موبایل مربوطه باید با 98 شروع شود، مثل 989121111111", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If

                If mobile(2) <> "9" Then
                    MessageBox.Show("دریافت اطلاعات", "شماره موبایل مربوطه باید با 9 شروع شود، مثل 989121111111", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If

                If mobile.Length < 12 Or mobile.Length > 12 Then
                    MessageBox.Show("لطفاً صحت شماره موبایل را بررسی نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If
                ''''@arezoo
            End If
            If rbFemale.Checked = False And rbMale.Checked = False Then
                MessageBox.Show("لطفا جنسیت را انتخاب کنید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If txtAccountNo.Text.Trim <> "0" And txtAccountNo.Text.Trim <> "" Then

                If txtFishVarizi.Text = "" Then
                    MessageBox.Show("شماره فیش واریزی را وارد نمایید", "دریافت اطلاعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If
            End If
        End If

        If Not (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then   'نوع کارت هدیه

            If txtAccountNo.Text.Length <> 13 Then
                MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If txtCustNo.Text.Length < 2 Then
                MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If

            If IsValidSIBACheckDigit(txtAccountNo.Text) = False Then
                MessageBox.Show("شماره حساب وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ClearFieldsFlag = False
                Exit Sub
            End If
        End If

        If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift") Or cbCardType.SelectedItem.ToString.Contains("ران کا")) Then   'نوع کارت هدیه
            If (cbCardType.SelectedItem.ToString.Contains("هد") Or cbCardType.SelectedItem.ToString.ToLower.Contains("gift")) Then
                ' MessageBox.Show("FirstGetCustomerInfo")

                If CusNumber = False Then
                    If CustNumber.Length < 2 Then
                        MessageBox.Show("شماره مشتری را کامل وارد نمایید ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If
                    tmpWSResult = ""

                    ''''''' inja ro faal kon
                    Try
                        tmpWSResult = CiBS_WS.GetCustomerInfo(Encrypt(CustNumber.Trim & "|" & Now.TimeOfDay.TotalSeconds))
                    Catch ex As Exception
                        MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End Try

                    Dim tmpAns As String = ""
                    tmpAns = Decrypt(tmpWSResult)


                    'MessageBox.Show("GetCustomerInfo Result:" & tmpAns)

                    Dim ResultGetCustom As String = Decrypt(tmpWSResult)
                    Dim ResultGetCustom1 As String() = ResultGetCustom.Split("|")
                    '///////
                    If String.Compare(ResultGetCustom1(0), "Timeout") = 0 Then
                        MessageBox.Show("هنگام بررسی شماره مشتری خطای Timeout ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If

                    If tmpAns.Contains("No Data") Then
                        MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد. خطای NO DATA ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If
                    If tmpAns.Contains("Unexpected Error") Then
                        MessageBox.Show("بررسی شماره مشتری وارد شده خطای Unexpected Error ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If

                    '//////
                    If tmpAns.Contains("0") Then
                        MessageBox.Show("شماره مشتری وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearFieldsFlag = False
                        Exit Sub
                    End If
                End If
                'MessageBox.Show("AfterGetCustomerInfo")
                If RdbAmount.Checked = True Then
                    '///// 941107
                    'If txtAccountNo.Text.Length < 5 Then
                    '    MessageBox.Show("رقم کارت هدیه وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    ClearFieldsFlag = False
                    '    Exit Sub
                    'End If

                    'If Not txtAccountNo.Text.ToString.Replace(",", "").EndsWith("0000") Or txtAccountNo.Text.Replace(",", "") > 5000000 Or txtAccountNo.Text.Replace(",", "") < 50000 Then
                    '    MessageBox.Show("رقم کارت هدیه وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    ClearFieldsFlag = False
                    '    Exit Sub
                    'End If
                End If

            End If
            PrintCount = txtCustNo.Text

            ''''@arezoo: set 100000 for irancard  

            If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
                If txtAccountNo.Text.Trim = 0 Then

                ElseIf Not txtAccountNo.Text.ToString.Replace(",", "").EndsWith("0000") Then
                    MessageBox.Show("رقم ایران کارت باید مضربی از 10000 وارد شود ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFieldsFlag = False
                    Exit Sub
                End If
            End If
        End If

        If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
            If rbFemale.Checked = False And rbMale.Checked = False Then
                MessageBox.Show("لطفا جنسیت را انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        'txtCardRemain
        'If CInt(txtCardRemain.Text) < 1 Then
        '    MessageBox.Show(" این نوع کارت جهت صدور در شعبه وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ClearFieldsFlag = False
        '    Exit Sub
        'End If



        ''''''''''''''''''''''''''''''''@arezoo
        '''''check whether the customer has a irancard or not 


        ''''''@arezoo: فقط شماره شناسنامه رو چک کنه که اگر در دیتابیس بود این پیغام رو بده، درسته؟
        ''''جون وقتی که چاپ میکنیم میره شماره شناسنامه میزاره یا نه بره 
        ''''isprinted & who printed ina ro ham checkkone?
        If cbCardType.SelectedItem.ToString.Contains("ران کا") Then
            Try
                Dim ResultCheckExistBlueCard As String = CiBS_WS.CheckExistBlueCard(txtIntCode.Text & "|" & "99")
                If ResultCheckExistBlueCard = 1 Then
                    MessageBox.Show("همکار گرامی، مشتری قبلا ایران کارت آبی داشته اند. لطفا از گزینه صدور مجدد استفاده کنید ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Catch ex As Exception
                InsertLocalLog("Error Getting Card Data" & ex.Message)
                ClearFieldsFlag = True
                MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

        End If


        ''GetAmountFromPos()  ''Masoumeh

        '////////////////////////////////

        cc.CustName = txtCustName.Text
        cc.CustFamilyName = txtCustFamilyName.Text
        cc.AccountNo = txtAccountNo.Text
        cc.CustNo = txtCustNo.Text
        ''cc.FishVarizi = txtFishVarizi.Text
        ''cc.Mobile = txtMobile.Text
        ''cc.IntCode = txtIntCode.Text
        cc.Iranian = rbIranian.Checked
        cc.OtherIranian = rbOthers.Checked
        cc.IntCode = txtIntCode.Text
        cc.WithAmount = RdbAmount.Checked
        cc.WithoutAmount = RdbWithoutAmount.Checked
        cc.CardTypeItem = cbCardType.SelectedItem.ToString
        cc.CardTypeIndex = cbCardType.SelectedIndex
        cc.SpecialText = txtSpecialText.Text
        cc.PrintGiftCardAmount = chkPrintGiftCardAmount.Checked
        cc.FishVarizi = txtFishVarizi.Text.Trim
        cc.Mobile = txtMobile.Text.Trim
        If rbFemale.Checked Then
            cc.Gender = "1"
        Else
            cc.Gender = "0"
        End If


        'Dim thrd As New Thread(AddressOf IssueCard)
        'thrd.Start(cc)
        'bgwCard.RunWorkerAsync()

        PrintedAllCards()

        txtCustName.Enabled = True
        txtCustFamilyName.Enabled = True
        txtAccountNo.Enabled = True
        txtCustNo.Enabled = True
        txtCustNoGift.Enabled = True
        txtIntCode.Enabled = True
        ''''@arezoo: اینجا غیرفعال کنم؟- سوال
        lblMobileExp.Visible = False
        rbFemale.Checked = False
        rbMale.Checked = False

        UpdateCardType_SelectedIndexChanged()
    End Sub
End Class

 