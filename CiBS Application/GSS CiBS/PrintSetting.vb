Imports System.IO
Public Class PrintSetting
    Dim tmpCradType As String()
    Dim ProductCodetmp As String()
    Dim currentLVrow As Integer
    Dim prnTemplate As String = ""

    Private Sub PrintSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ff As FontFamily
        For Each ff In FontFamily.Families
            cbFontPAN.Items.Add(ff.Name)
            cbFontName.Items.Add(ff.Name)
            cbFontCVV2.Items.Add(ff.Name)
            cbFontBrCode.Items.Add(ff.Name)
            cbFontExpDate.Items.Add(ff.Name)
            cbFonts.Items.Add(ff.Name)
            cbFontSpecialText.Items.Add(ff.Name)
            cbFontAccNo.Items.Add(ff.Name)
            cbFontSheba.Items.Add(ff.Name)
        Next

        cbFontPAN.Items.Add("AdobeArabic-Bold.otf")
        cbFontName.Items.Add("AdobeArabic-Bold.otf")
        cbFontCVV2.Items.Add("AdobeArabic-Bold.otf")
        cbFontBrCode.Items.Add("AdobeArabic-Bold.otf")
        cbFontExpDate.Items.Add("AdobeArabic-Bold.otf")
        cbFonts.Items.Add("AdobeArabic-Bold.otf")
        cbFontSpecialText.Items.Add("AdobeArabic-Bold.otf")
        cbFontAccNo.Items.Add("AdobeArabic-Bold.otf")
        cbFontSheba.Items.Add("AdobeArabic-Bold.otf")

        cbFontPAN.Items.Add("DINPro-Medium.otf")
        cbFontName.Items.Add("DINPro-Medium.otf")
        cbFontCVV2.Items.Add("DINPro-Medium.otf")
        cbFontBrCode.Items.Add("DINPro-Medium.otf")
        cbFontExpDate.Items.Add("DINPro-Medium.otf")
        cbFonts.Items.Add("DINPro-Medium.otf")
        cbFontSpecialText.Items.Add("DINPro-Medium.otf")
        cbFontAccNo.Items.Add("DINPro-Medium.otf")
        cbFontSheba.Items.Add("DINPro-Medium.otf")

        cbFontPAN.Items.Add("Adobe Arabic.otf")
        cbFontName.Items.Add("Adobe Arabic.otf")
        cbFontCVV2.Items.Add("Adobe Arabic.otf")
        cbFontBrCode.Items.Add("Adobe Arabic.otf")
        cbFontExpDate.Items.Add("Adobe Arabic.otf")
        cbFonts.Items.Add("Adobe Arabic.otf")
        cbFontSpecialText.Items.Add("Adobe Arabic.otf")
        cbFontAccNo.Items.Add("Adobe Arabic.otf")
        cbFontSheba.Items.Add("Adobe Arabic.otf")

        getCardTypes()
    End Sub

    Sub getCardTypes()
        cbCardType.Items.Clear()
        ' get card types
        Try
            tmpWSResult = CiBS_WS.GetCardTypes(Encrypt("ALL" & "|" & "99"))
        Catch ex As Exception
            InsertLocalLog("GetCardTypes- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت انواع کارت" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        Dim tmpAns As String() = tmpWSResult.Split("!")

        ReDim tmpCradType(tmpAns.Length - 1)
        ReDim ProductCodetmp(tmpAns.Length - 1)
        For i As Integer = 0 To tmpAns.Length - 2
            Dim tmp() As String = Decrypt(tmpAns(i)).Split("|")
            tmpCradType(i) = tmp(0)
            cbCardType.Items.Add(tmp(1).Trim)
            ProductCodetmp(i) = tmp(2)
        Next
    End Sub
    Function ShowCard() As Boolean
        ShowCard = False
        If File.Exists("tmp.bmp") Then
            File.Delete("tmp.bmp")
        End If

        Try
            Dim x As New Bitmap(1016, 648, Imaging.PixelFormat.Format24bppRgb)
            Dim z As Graphics
            Dim Nformat As New System.Drawing.StringFormat
            z = Graphics.FromImage(x)
            z.Clear(Color.White)
            '    If Not String.IsNullOrEmpty(txtTemplatePath.Text) Then
            Dim f1, f2, f3, f4, f5, f6, f7, f8 As Font
            If cbPanBold.Checked Then
                f1 = New Font(cbFontPAN.SelectedItem.ToString, txtFontSizePAN.Text, FontStyle.Bold, GraphicsUnit.Millimeter)
            Else
                f1 = New Font(cbFontPAN.SelectedItem.ToString, txtFontSizePAN.Text, FontStyle.Regular, GraphicsUnit.Millimeter)
            End If
            If cbCvvBold.Checked Then
                f2 = New Font(cbFontCVV2.SelectedItem.ToString, txtFontSizeCVV2.Text, FontStyle.Bold, GraphicsUnit.Millimeter)
            Else
                f2 = New Font(cbFontCVV2.SelectedItem.ToString, txtFontSizeCVV2.Text, FontStyle.Regular, GraphicsUnit.Millimeter)
            End If
            If cbNameBold.Checked Then
                f3 = New Font(cbFontName.SelectedItem.ToString, txtFontSizeName.Text, FontStyle.Bold, GraphicsUnit.Millimeter)
            Else
                f3 = New Font(cbFontName.SelectedItem.ToString, txtFontSizeName.Text, FontStyle.Regular, GraphicsUnit.Millimeter)
            End If
            If cbExpDateBold.Checked Then
                f4 = New Font(cbFontExpDate.SelectedItem.ToString, txtFontSizeExpDate.Text, FontStyle.Bold, GraphicsUnit.Millimeter)
            Else
                f4 = New Font(cbFontExpDate.SelectedItem.ToString, txtFontSizeExpDate.Text, FontStyle.Regular, GraphicsUnit.Millimeter)
            End If
            If cbBrCodeBold.Checked Then
                f5 = New Font(cbFontBrCode.SelectedItem.ToString, txtFontSizeBrCode.Text, FontStyle.Bold, GraphicsUnit.Millimeter)
            Else
                f5 = New Font(cbFontBrCode.SelectedItem.ToString, txtFontSizeBrCode.Text, FontStyle.Regular, GraphicsUnit.Millimeter)
            End If
            If cbSpecialTextBold.Checked Then
                f6 = New Font(cbFontSpecialText.SelectedItem.ToString, txtFontSizeSpecialtext.Text, FontStyle.Bold, GraphicsUnit.Millimeter)
            Else
                f6 = New Font(cbFontSpecialText.SelectedItem.ToString, txtFontSizeSpecialtext.Text, FontStyle.Regular, GraphicsUnit.Millimeter)
            End If

            If cbSpecialTextBold.Checked Then
                f7 = New Font(cbFontAccNo.SelectedItem.ToString, txtFontSizeAccNo.Text, FontStyle.Bold, GraphicsUnit.Millimeter)
            Else
                f7 = New Font(cbFontAccNo.SelectedItem.ToString, txtFontSizeAccNo.Text, FontStyle.Regular, GraphicsUnit.Millimeter)
            End If

            If cbSpecialTextBold.Checked Then
                f8 = New Font(cbFontSheba.SelectedItem.ToString, txtFontSizeSheba.Text, FontStyle.Bold, GraphicsUnit.Millimeter)
            Else
                f8 = New Font(cbFontSheba.SelectedItem.ToString, txtFontSizeSheba.Text, FontStyle.Regular, GraphicsUnit.Millimeter)
            End If

            If chkRTLpan.Checked = True Then
                Nformat.Alignment = StringAlignment.Far
            Else
                Nformat.Alignment = StringAlignment.Near
            End If
            If chkCenterPAN.Checked = True Then
                Nformat.Alignment = StringAlignment.Center
            End If
            If cbPAN.Checked = True Then z.DrawString("1234 4567 8901 2345", f1, Brushes.Black, txtXpan.Text, txtYpan.Text, Nformat)

            If chkRTLcvv2.Checked = True Then
                Nformat.Alignment = StringAlignment.Far
            Else
                Nformat.Alignment = StringAlignment.Near
            End If
            If chkCenterCVV.Checked = True Then
                Nformat.Alignment = StringAlignment.Center
            End If
            If cbCVV2.Checked = True Then z.DrawString("1234", f2, Brushes.Black, txtXcvv2.Text, txtYcvv2.Text, Nformat)

            If chkRTLname.Checked = True Then
                Nformat.Alignment = StringAlignment.Far
            Else
                Nformat.Alignment = StringAlignment.Near
            End If
            If chkCenterName.Checked = True Then
                Nformat.Alignment = StringAlignment.Center
            End If
            If cbNAME.Checked = True Then z.DrawString("تست چاپ کارت", f3, Brushes.Black, txtXname.Text, txtYname.Text, Nformat)

            If chkRTLexp.Checked = True Then
                Nformat.Alignment = StringAlignment.Far
            Else
                Nformat.Alignment = StringAlignment.Near
            End If
            If chkCenterEXP.Checked = True Then
                Nformat.Alignment = StringAlignment.Center
            End If
            If cbEXP.Checked = True Then z.DrawString("1400/12", f4, Brushes.Black, txtXexpdate.Text, txtYexpdate.Text, Nformat)

            If chkRTLbranch.Checked = True Then
                Nformat.Alignment = StringAlignment.Far
            Else
                Nformat.Alignment = StringAlignment.Near
            End If
            If chkCenterBrCode.Checked = True Then
                Nformat.Alignment = StringAlignment.Center
            End If
            If cbBRANCH.Checked = True Then z.DrawString("1234", f5, Brushes.Black, txtXbrcode.Text, txtYbrcode.Text, Nformat)

            If chkRTLspecialText.Checked = True Then
                Nformat.Alignment = StringAlignment.Far
            Else
                Nformat.Alignment = StringAlignment.Near
            End If
            If chkCenterSpecialText.Checked = True Then
                Nformat.Alignment = StringAlignment.Center
            End If
            If cbSpecialText.Checked = True Then z.DrawString("تست متن خاص", f6, Brushes.Black, txtXSpecialText.Text, txtYSpecialText.Text, Nformat)

            '/////////
            If chkRTLAccNo.Checked = True Then
                Nformat.Alignment = StringAlignment.Far
            Else
                Nformat.Alignment = StringAlignment.Near
            End If
            If chkCenterAccNo.Checked = True Then
                Nformat.Alignment = StringAlignment.Center
            End If
            If cbAccNo.Checked = True Then z.DrawString("1243568795423", f7, Brushes.Black, txtXAccNo.Text, txtYAccNo.Text, Nformat)

            If chkRTLSheba.Checked = True Then
                Nformat.Alignment = StringAlignment.Far
            Else
                Nformat.Alignment = StringAlignment.Near
            End If
            If chkCenterSheba.Checked = True Then
                Nformat.Alignment = StringAlignment.Center
            End If

            If cbSheba.Checked = True Then z.DrawString("IR08-062-000000-020001234002", f8, Brushes.Black, txtXSheba.Text, txtYSheba.Text, Nformat)

            '/////////////////
            If cbLogo.Checked = True Then
                Dim newImage As Image = Image.FromFile("GSS-logo.jpg")
                z.DrawImage(newImage, CInt(txtXlogo.Text), CInt(txtYlogo.Text))
                newImage.Dispose()
            End If

            For i As Integer = 0 To lvCardText.Items.Count - 1
                Dim f As Font
                If lvCardText.Items(i).SubItems(2).Text = "True" Then
                    f = New Font(lvCardText.Items(i).SubItems(1).Text, lvCardText.Items(i).SubItems(3).Text, FontStyle.Bold, GraphicsUnit.Millimeter)
                Else
                    f = New Font(lvCardText.Items(i).SubItems(1).Text, lvCardText.Items(i).SubItems(3).Text, FontStyle.Regular, GraphicsUnit.Millimeter)
                End If
                z.DrawString(lvCardText.Items(i).Text, f, Brushes.Black, lvCardText.Items(i).SubItems(4).Text, lvCardText.Items(i).SubItems(5).Text)
            Next

            x = CNVRTClass.CopyToBpp(x, 1)
            x.Save("tmp.bmp", Imaging.ImageFormat.Bmp)
            pbResult.ImageLocation = "tmp.bmp"
            ShowCard = True
        Catch ex As Exception
            pbResult.ImageLocation = ""
            If ex.Message.Contains("Object reference not set to an instance of an object") Then MessageBox.Show("ابتدا تمامی فیلدها را تکمیل نمایید", "خطا")
            If ex.Message.Contains("Font") Then MessageBox.Show("عدم امکان کار با یکی از قلمهای انتخاب شده", "خطا")
        End Try
        Me.Refresh()
    End Function

    Private Sub txtFontSizePAN_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNumeric(txtFontSizePAN.Text) And txtFontSizePAN.Text <> "" Then
            MessageBox.Show("مقدار ورودی نامعتبر", "خطا")
            txtFontSizePAN.Select()
        End If

    End Sub

    Private Sub txtFontSizeName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNumeric(txtFontSizeName.Text) And txtFontSizeName.Text <> "" Then
            MessageBox.Show("مقدار ورودی نامعتبر", "خطا")
            txtFontSizeName.Select()
        End If

    End Sub

    Private Sub txtFontSizeExpDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNumeric(txtFontSizeExpDate.Text) And txtFontSizeExpDate.Text <> "" Then
            MessageBox.Show("مقدار ورودی نامعتبر", "خطا")
            txtFontSizeExpDate.Select()
        End If

    End Sub

    Private Sub txtFontSizeCVV2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNumeric(txtFontSizeCVV2.Text) And txtFontSizeCVV2.Text <> "" Then
            MessageBox.Show("مقدار ورودی نامعتبر", "خطا")
            txtFontSizeCVV2.Select()
        End If

    End Sub

    Private Sub txtFontSizeBrCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNumeric(txtFontSizeBrCode.Text) And txtFontSizeBrCode.Text <> "" Then
            MessageBox.Show("مقدار ورودی نامعتبر", "خطا")
            txtFontSizeBrCode.Select()
        End If

    End Sub

    Private Sub txtXbrcode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNumeric(txtXbrcode.Text) And txtXbrcode.Text <> "" Then
            MessageBox.Show("مقدار ورودی نامعتبر", "خطا")
            txtXbrcode.Select()
        End If

    End Sub

    Private Sub txtXcvv2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNumeric(txtXcvv2.Text) And txtXcvv2.Text <> "" Then
            MessageBox.Show("مقدار ورودی نامعتبر", "خطا")
            txtXcvv2.Select()
        End If

    End Sub

    Private Sub txtXexpdate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNumeric(txtXexpdate.Text) And txtXexpdate.Text <> "" Then
            MessageBox.Show("مقدار ورودی نامعتبر", "خطا")
            txtXexpdate.Select()
        End If

    End Sub

    Private Sub txtXname_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNumeric(txtXname.Text) And txtXname.Text <> "" Then
            MessageBox.Show("مقدار ورودی نامعتبر", "خطا")
            txtXname.Select()
        End If

    End Sub

    Private Sub txtXpan_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNumeric(txtXpan.Text) And txtXpan.Text <> "" Then
            MessageBox.Show("مقدار ورودی نامعتبر", "خطا")
            txtXpan.Select()
        End If

    End Sub

    Private Sub txtYbrcode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNumeric(txtYbrcode.Text) And txtYbrcode.Text <> "" Then
            MessageBox.Show("مقدار ورودی نامعتبر", "خطا")
            txtYbrcode.Select()
        End If

    End Sub

    Private Sub txtYcvv2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNumeric(txtYcvv2.Text) And txtYcvv2.Text <> "" Then
            MessageBox.Show("مقدار ورودی نامعتبر", "خطا")
            txtYcvv2.Select()
        End If

    End Sub

    Private Sub txtYexpdate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNumeric(txtYexpdate.Text) And txtYexpdate.Text <> "" Then
            MessageBox.Show("مقدار ورودی نامعتبر", "خطا")
            txtYexpdate.Select()
        End If

    End Sub

    Private Sub txtYname_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNumeric(txtYname.Text) And txtYname.Text <> "" Then
            MessageBox.Show("مقدار ورودی نامعتبر", "خطا")
            txtYname.Select()
        End If

    End Sub

    Private Sub txtYpan_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNumeric(txtYpan.Text) And txtYpan.Text <> "" Then
            MessageBox.Show("مقدار ورودی نامعتبر", "خطا")
            txtYpan.Select()
        End If

    End Sub

    'Private Sub cbFontPAN_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    btnPrint.Enabled = False
    '    btnSaveTemplate.Enabled = False
    'End Sub

    'Private Sub cbFontName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    btnPrint.Enabled = False
    '    btnSaveTemplate.Enabled = False
    'End Sub

    'Private Sub cbFontCVV2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    btnPrint.Enabled = False
    '    btnSaveTemplate.Enabled = False
    'End Sub

    'Private Sub cbFontExpDate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    btnPrint.Enabled = False
    '    btnSaveTemplate.Enabled = False
    'End Sub

    'Private Sub cbFontBrCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    btnPrint.Enabled = False
    '    btnSaveTemplate.Enabled = False
    'End Sub

    'Sub ShowTemplate()

    '    Dim str As String
    '    ' Dim sr As New StreamReader(txtTemplatePath.Text, System.Text.Encoding.UTF8)
    '    'str = sr.ReadLine()
    '    'While sr.EndOfStream = False
    '    '    str = str & "#" & sr.ReadLine()
    '    'End While
    '    'sr.Close()

    '    Dim tmp As String() = str.Split("#")
    '    Dim pan, cvv2, Cname, exp, branch, txt As String()

    '    pan = tmp(0).Split(";")
    '    cvv2 = tmp(1).Split(";")
    '    Cname = tmp(2).Split(";")
    '    exp = tmp(3).Split(";")
    '    branch = tmp(4).Split(";")
    '    For i As Integer = 5 To tmp.Length - 1
    '        txt = tmp(i).Split(";")
    '        Dim lvi As New ListViewItem
    '        lvi.Text = txt(3)
    '        lvi.SubItems.Add(txt(1))
    '        lvi.SubItems.Add(txt(6))
    '        lvi.SubItems.Add(txt(2))
    '        lvi.SubItems.Add(txt(4))
    '        lvi.SubItems.Add(txt(5))
    '        lvCardText.Items.Add(lvi)
    '    Next

    '    cbFontBrCode.SelectedItem = branch(1)
    '    cbFontCVV2.SelectedItem = cvv2(1)
    '    cbFontExpDate.SelectedItem = exp(1)
    '    cbFontName.SelectedItem = Cname(1)
    '    cbFontPAN.SelectedItem = pan(1)

    '    txtFontSizeBrCode.Text = branch(2)
    '    txtFontSizeCVV2.Text = cvv2(2)
    '    txtFontSizeExpDate.Text = exp(2)
    '    txtFontSizeName.Text = Cname(2)
    '    txtFontSizePAN.Text = pan(2)

    '    txtXbrcode.Text = branch(3)
    '    txtXcvv2.Text = cvv2(3)
    '    txtXexpdate.Text = exp(3)
    '    txtXname.Text = Cname(3)
    '    txtXpan.Text = pan(3)

    '    txtYbrcode.Text = branch(4)
    '    txtYcvv2.Text = cvv2(4)
    '    txtYexpdate.Text = exp(4)
    '    txtYname.Text = Cname(4)
    '    txtYpan.Text = pan(4)

    '    If branch(5) = 1 Then
    '        chkRTLbranch.Checked = True
    '    Else
    '        chkRTLbranch.Checked = False
    '    End If

    '    If cvv2(5) = 1 Then
    '        chkRTLcvv2.Checked = True
    '    Else
    '        chkRTLcvv2.Checked = False
    '    End If

    '    If exp(5) = 1 Then
    '        chkRTLexp.Checked = True
    '    Else
    '        chkRTLexp.Checked = False
    '    End If

    '    If Cname(5) = 1 Then
    '        chkRTLname.Checked = True
    '    Else
    '        chkRTLname.Checked = False
    '    End If

    '    If pan(5) = 1 Then
    '        chkRTLpan.Checked = True
    '    Else
    '        chkRTLpan.Checked = False
    '    End If

    '    If branch(6) = 1 Then
    '        cbBRANCH.Checked = True
    '    Else
    '        cbBRANCH.Checked = False
    '    End If

    '    If cvv2(6) = 1 Then
    '        cbCVV2.Checked = True
    '    Else
    '        cbCVV2.Checked = False
    '    End If

    '    If exp(6) = 1 Then
    '        cbEXP.Checked = True
    '    Else
    '        cbEXP.Checked = False
    '    End If

    '    If Cname(6) = 1 Then
    '        cbNAME.Checked = True
    '    Else
    '        cbNAME.Checked = False
    '    End If

    '    If pan(6) = 1 Then
    '        cbPAN.Checked = True
    '    Else
    '        cbPAN.Checked = False
    '    End If



    '    If branch(7) = "True" Then
    '        cbBrCodeBold.Checked = True
    '    Else
    '        cbBrCodeBold.Checked = False
    '    End If

    '    If cvv2(7) = "True" Then
    '        cbCvvBold.Checked = True
    '    Else
    '        cbCvvBold.Checked = False
    '    End If

    '    If exp(7) = "True" Then
    '        cbExpDateBold.Checked = True
    '    Else
    '        cbExpDateBold.Checked = False
    '    End If

    '    If Cname(7) = "True" Then
    '        cbNameBold.Checked = True
    '    Else
    '        cbNameBold.Checked = False
    '    End If

    '    If pan(7) = "True" Then
    '        cbPanBold.Checked = True
    '    Else
    '        cbPanBold.Checked = False
    '    End If






    '    If ShowCard() Then
    '        btnPrint.Enabled = True
    '        'btnSaveTemplate.Enabled = True
    '    End If
    'End Sub

    Private Sub txtFontSize_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNumeric(txtFontSize.Text) And txtFontSize.Text <> "" Then
            MessageBox.Show("مقدار ورودی نامعتبر", "خطا")
            txtFontSize.Select()
        End If
    End Sub

    Private Sub txtX_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNumeric(txtX.Text) And txtX.Text <> "" Then
            MessageBox.Show("مقدار ورودی نامعتبر", "خطا")
            txtX.Select()
        End If
    End Sub

    Private Sub txtY_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsNumeric(txtY.Text) And txtY.Text <> "" Then
            MessageBox.Show("مقدار ورودی نامعتبر", "خطا")
            txtY.Select()
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim tmp As String = Nothing
        hPrinter = OpenPrinter(CurrentPrinter)
        If hPrinter > 0 Then
            tmpAns = CardPrint.MangePrinterPass(1, PrinterSerialNo)
            If Not tmpAns = "OK" Then
                MessageBox.Show("خطا هنگام فعال سازی چاپگر " & vbCrLf & tmpAns, "خطا")
                Exit Sub
            End If
            tmp = KOprinting(hPrinter, "tmp.bmp", "varnish.bmp")
            If String.Compare(tmp, "Well Done") = 0 Then
                ClosePrinter(hPrinter)
            Else
                MessageBox.Show("خطا هنگام چاپ کارت " & vbCrLf & tmp, "خطا")
            End If
        Else
            MessageBox.Show("خطا هنگام برقراری ارتباط با چاپگر", "خطا")
        End If
        tmpAns = CardPrint.MangePrinterPass(2, PrinterSerialNo)
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        btnPrint.Enabled = False
        btnSavePrintTemplate.Enabled = False
        btnCopyTemplate.Enabled = True
        If ShowCard() Then
            btnPrint.Enabled = True
            btnSavePrintTemplate.Enabled = True
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim lvi As New ListViewItem
        lvi.Text = txtCardlogo.Text
        lvi.SubItems.Add(cbFonts.SelectedItem)
        lvi.SubItems.Add(cbShoarBold.Checked)
        lvi.SubItems.Add(txtFontSize.Text)
        lvi.SubItems.Add(txtX.Text)
        lvi.SubItems.Add(txtY.Text)
        lvCardText.Items.Add(lvi)
        txtCardlogo.Text = ""
        cbFonts.SelectedIndex = -1
        txtFontSize.Text = ""
        txtX.Text = ""
        txtY.Text = ""
        cbShoarBold.Checked = False
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If lvCardText.SelectedItems.Count > 0 Then lvCardText.Items.Remove(lvCardText.SelectedItems(0))
        txtCardlogo.Text = ""
        cbFonts.SelectedIndex = -1
        txtFontSize.Text = ""
        txtX.Text = ""
        txtY.Text = ""
        cbShoarBold.Checked = False
    End Sub

    Sub ClearFields()
        cbEnabled.SelectedIndex = -1

        cbFontBrCode.SelectedIndex = -1
        cbFontCVV2.SelectedIndex = -1
        cbFontExpDate.SelectedIndex = -1
        cbFontName.SelectedIndex = -1
        cbFontPAN.SelectedIndex = -1
        cbFontSpecialText.SelectedIndex = -1


        txtFontSizeBrCode.Text = ""
        txtFontSizeCVV2.Text = ""
        txtFontSizeExpDate.Text = ""
        txtFontSizeName.Text = ""
        txtFontSizePAN.Text = ""
        txtFontSizeSpecialtext.Text = ""

        txtXbrcode.Text = ""
        txtXcvv2.Text = ""
        txtXexpdate.Text = ""
        txtXname.Text = ""
        txtXpan.Text = ""
        txtXSpecialText.Text = ""
        txtXlogo.Text = ""


        txtYbrcode.Text = ""
        txtYcvv2.Text = ""
        txtYexpdate.Text = ""
        txtYname.Text = ""
        txtYpan.Text = ""
        txtYSpecialText.Text = ""
        txtYlogo.Text = ""

        pbResult.ImageLocation = ""

        cbBRANCH.Checked = False
        cbCVV2.Checked = False
        cbEXP.Checked = False
        cbNAME.Checked = False
        cbPAN.Checked = False
        cbSpecialText.Checked = False
        cbLogo.Checked = False


        chkRTLbranch.Checked = False
        chkRTLcvv2.Checked = False
        chkRTLexp.Checked = False
        chkRTLname.Checked = False
        chkRTLpan.Checked = False

        cbNameBold.Checked = False
        cbPanBold.Checked = False
        cbExpDateBold.Checked = False
        cbCvvBold.Checked = False
        cbBrCodeBold.Checked = False
        cbSpecialTextBold.Checked = False

        chkCenterName.Checked = False
        chkCenterPAN.Checked = False
        chkCenterEXP.Checked = False
        chkCenterCVV.Checked = False
        chkCenterBrCode.Checked = False
        chkCenterSpecialText.Checked = False


        lvCardText.Items.Clear()
        Me.Refresh()
    End Sub

    Private Sub lvCardText_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvCardText.ItemSelectionChanged
        currentLVrow = e.ItemIndex
        txtCardlogo.Text = lvCardText.Items(e.ItemIndex).Text
        cbFonts.SelectedItem = lvCardText.Items(e.ItemIndex).SubItems(1).Text
        cbShoarBold.Checked = lvCardText.Items(e.ItemIndex).SubItems(2).Text.Trim
        txtFontSize.Text = lvCardText.Items(e.ItemIndex).SubItems(3).Text
        txtX.Text = lvCardText.Items(e.ItemIndex).SubItems(4).Text
        txtY.Text = lvCardText.Items(e.ItemIndex).SubItems(5).Text
    End Sub

    Private Sub btnChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChange.Click
        lvCardText.Items(currentLVrow).Text = txtCardlogo.Text
        lvCardText.Items(currentLVrow).SubItems(1).Text = cbFonts.SelectedItem
        lvCardText.Items(currentLVrow).SubItems(2).Text = cbShoarBold.Checked
        lvCardText.Items(currentLVrow).SubItems(3).Text = txtFontSize.Text
        lvCardText.Items(currentLVrow).SubItems(4).Text = txtX.Text
        lvCardText.Items(currentLVrow).SubItems(5).Text = txtY.Text
        txtCardlogo.Text = ""
        cbFonts.SelectedIndex = -1
        txtFontSize.Text = ""
        txtX.Text = ""
        txtY.Text = ""
        cbShoarBold.Checked = False
    End Sub

    Private Sub btnShowCardPrintTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowCardPrintTemplate.Click
        If cbCardType.SelectedIndex = -1 Then
            MessageBox.Show("لطفاً یک نوع کارت را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        LoadAndShowPrintTemplate()
    End Sub

    Sub LoadAndShowPrintTemplate()
        btnPrint.Enabled = False
        btnSavePrintTemplate.Enabled = False
        btnCopyTemplate.Enabled = False
        ClearFields()
        Try
            tmpWSResult = CiBS_WS.GetPrintTempalte(Encrypt(tmpCradType(cbCardType.SelectedIndex) & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99"))
        Catch ex As Exception
            InsertLocalLog("GetPrintTempalte- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت الگوی چاپ کارت" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        Dim testtemp As String = Decrypt(tmpWSResult)
        Dim testtemp2 As String() = testtemp.Split("|")
        tmpAns = testtemp2(0) ' Decrypt(tmpWSResult).Split("|")(0)    'returned card print template
        cbEnabled.SelectedIndex = testtemp2(1) ' Decrypt(tmpWSResult).Split("|")(1)
        txtProductCode.Text = ProductCodetmp(cbCardType.SelectedIndex)
        Dim tmp As String() = tmpAns.Split("!")
        Dim pan, cvv2, Cname, exp, branch, txt, SpecialText, Logo, AccNo, Sheba As String()

        pan = tmp(0).Split(";")
        cvv2 = tmp(1).Split(";")
        Cname = tmp(2).Split(";")
        exp = tmp(3).Split(";")
        branch = tmp(4).Split(";")
        SpecialText = tmp(5).Split(";")
        Logo = tmp(6).Split(";")
        AccNo = tmp(7).Split(";")
        Sheba = tmp(8).Split(";")

        For i As Integer = 9 To tmp.Length - 1
            txt = tmp(i).Split(";")
            Dim lvi As New ListViewItem
            lvi.Text = txt(3)
            lvi.SubItems.Add(txt(1))
            lvi.SubItems.Add(txt(6))
            lvi.SubItems.Add(txt(2))
            lvi.SubItems.Add(txt(4))
            lvi.SubItems.Add(txt(5))
            lvCardText.Items.Add(lvi)
        Next

        cbFontBrCode.SelectedItem = branch(1)
        cbFontCVV2.SelectedItem = cvv2(1)
        cbFontExpDate.SelectedItem = exp(1)
        cbFontName.SelectedItem = Cname(1)
        cbFontPAN.SelectedItem = pan(1)
        cbFontSpecialText.SelectedItem = SpecialText(1)
        cbFontAccNo.SelectedItem = AccNo(1)
        cbFontSheba.SelectedItem = Sheba(1)

        txtFontSizeBrCode.Text = branch(2)
        txtFontSizeCVV2.Text = cvv2(2)
        txtFontSizeExpDate.Text = exp(2)
        txtFontSizeName.Text = Cname(2)
        txtFontSizePAN.Text = pan(2)
        txtFontSizeSpecialtext.Text = SpecialText(2)
        txtFontSizeAccNo.Text = AccNo(2)
        txtFontSizeSheba.Text = Sheba(2)

        txtXbrcode.Text = branch(3)
        txtXcvv2.Text = cvv2(3)
        txtXexpdate.Text = exp(3)
        txtXname.Text = Cname(3)
        txtXpan.Text = pan(3)
        txtXSpecialText.Text = SpecialText(3)
        txtXAccNo.Text = AccNo(3)
        txtXSheba.Text = Sheba(3)
        txtXlogo.Text = Logo(3)

        txtYbrcode.Text = branch(4)
        txtYcvv2.Text = cvv2(4)
        txtYexpdate.Text = exp(4)
        txtYname.Text = Cname(4)
        txtYpan.Text = pan(4)
        txtYSpecialText.Text = SpecialText(4)
        txtYAccNo.Text = AccNo(4)
        txtYSheba.Text = Sheba(4)
        txtYlogo.Text = Logo(4)

        If branch(5) = "True" Then
            chkRTLbranch.Checked = True
        Else
            chkRTLbranch.Checked = False
        End If

        If cvv2(5) = "True" Then
            chkRTLcvv2.Checked = True
        Else
            chkRTLcvv2.Checked = False
        End If

        If exp(5) = "True" Then
            chkRTLexp.Checked = True
        Else
            chkRTLexp.Checked = False
        End If

        If Cname(5) = "True" Then
            chkRTLname.Checked = True
        Else
            chkRTLname.Checked = False
        End If

        If pan(5) = "True" Then
            chkRTLpan.Checked = True
        Else
            chkRTLpan.Checked = False
        End If

        If SpecialText(5) = "True" Then
            chkRTLspecialText.Checked = True
        Else
            chkRTLspecialText.Checked = False
        End If

        If AccNo(5) = "True" Then
            chkRTLAccNo.Checked = True
        Else
            chkRTLAccNo.Checked = False
        End If

        If Sheba(5) = "True" Then
            chkRTLSheba.Checked = True
        Else
            chkRTLSheba.Checked = False
        End If


        If branch(6) = "True" Then
            cbBRANCH.Checked = True
        Else
            cbBRANCH.Checked = False
        End If

        If cvv2(6) = "True" Then
            cbCVV2.Checked = True
        Else
            cbCVV2.Checked = False
        End If

        If exp(6) = "True" Then
            cbEXP.Checked = True
        Else
            cbEXP.Checked = False
        End If

        If Cname(6) = "True" Then
            cbNAME.Checked = True
        Else
            cbNAME.Checked = False
        End If

        If pan(6) = "True" Then
            cbPAN.Checked = True
        Else
            cbPAN.Checked = False
        End If

        If SpecialText(6) = "True" Then
            cbSpecialText.Checked = True
        Else
            cbSpecialText.Checked = False
        End If

        If AccNo(6) = "True" Then
            cbAccNo.Checked = True
        Else
            cbAccNo.Checked = False
        End If

        If Sheba(6) = "True" Then
            cbSheba.Checked = True
        Else
            cbSheba.Checked = False
        End If

        If Logo(6) = "True" Then
            cbLogo.Checked = True
        Else
            cbLogo.Checked = False
        End If

        If branch(7) = "True" Then
            cbBrCodeBold.Checked = True
        Else
            cbBrCodeBold.Checked = False
        End If

        If cvv2(7) = "True" Then
            cbCvvBold.Checked = True
        Else
            cbCvvBold.Checked = False
        End If

        If exp(7) = "True" Then
            cbExpDateBold.Checked = True
        Else
            cbExpDateBold.Checked = False
        End If

        If Cname(7) = "True" Then
            cbNameBold.Checked = True
        Else
            cbNameBold.Checked = False
        End If

        If pan(7) = "True" Then
            cbPanBold.Checked = True
        Else
            cbPanBold.Checked = False
        End If

        If SpecialText(7) = "True" Then
            cbSpecialTextBold.Checked = True
        Else
            cbSpecialTextBold.Checked = False
        End If

        If AccNo(7) = "True" Then
            cbAccNoBold.Checked = True
        Else
            cbAccNoBold.Checked = False
        End If

        If Sheba(7) = "True" Then
            cbShebaBold.Checked = True
        Else
            cbShebaBold.Checked = False
        End If


        If branch(8) = "True" Then
            chkCenterBrCode.Checked = True
        Else
            chkCenterBrCode.Checked = False
        End If

        If cvv2(8) = "True" Then
            chkCenterCVV.Checked = True
        Else
            chkCenterCVV.Checked = False
        End If

        If exp(8) = "True" Then
            chkCenterEXP.Checked = True
        Else
            chkCenterEXP.Checked = False
        End If

        If Cname(8) = "True" Then
            chkCenterName.Checked = True
        Else
            chkCenterName.Checked = False
        End If

        If pan(8) = "True" Then
            chkCenterPAN.Checked = True
        Else
            chkCenterPAN.Checked = False
        End If

        If SpecialText(8) = "True" Then
            chkCenterSpecialText.Checked = True
        Else
            chkCenterSpecialText.Checked = False
        End If

        If AccNo(8) = "True" Then
            chkCenterAccNo.Checked = True
        Else
            chkCenterAccNo.Checked = False
        End If

        If Sheba(8) = "True" Then
            chkCenterSheba.Checked = True
        Else
            chkCenterSheba.Checked = False
        End If

        If ShowCard() Then
            btnPrint.Enabled = True
            btnSavePrintTemplate.Enabled = True
            btnCopyTemplate.Enabled = True
        End If

    End Sub

    Sub MakePrintTemplate()
        prnTemplate = "pan;" & cbFontPAN.Text & ";" & txtFontSizePAN.Text & ";" & txtXpan.Text & ";" & txtYpan.Text & ";" & chkRTLpan.Checked & ";" & cbPAN.Checked & ";" & cbPanBold.Checked & ";" & chkCenterPAN.Checked
        prnTemplate = prnTemplate & "!" & "cvv2;" & cbFontCVV2.Text & ";" & txtFontSizeCVV2.Text & ";" & txtXcvv2.Text & ";" & txtYcvv2.Text & ";" & chkRTLcvv2.Checked & ";" & cbCVV2.Checked & ";" & cbCvvBold.Checked & ";" & chkCenterCVV.Checked
        prnTemplate = prnTemplate & "!" & "name;" & cbFontName.Text & ";" & txtFontSizeName.Text & ";" & txtXname.Text & ";" & txtYname.Text & ";" & chkRTLname.Checked & ";" & cbNAME.Checked & ";" & cbNameBold.Checked & ";" & chkCenterName.Checked
        prnTemplate = prnTemplate & "!" & "exp;" & cbFontExpDate.Text & ";" & txtFontSizeExpDate.Text & ";" & txtXexpdate.Text & ";" & txtYexpdate.Text & ";" & chkRTLexp.Checked & ";" & cbEXP.Checked & ";" & cbExpDateBold.Checked & ";" & chkCenterEXP.Checked
        prnTemplate = prnTemplate & "!" & "branch;" & cbFontBrCode.Text & ";" & txtFontSizeBrCode.Text & ";" & txtXbrcode.Text & ";" & txtYbrcode.Text & ";" & chkRTLbranch.Checked & ";" & cbBRANCH.Checked & ";" & cbBrCodeBold.Checked & ";" & chkCenterBrCode.Checked
        prnTemplate = prnTemplate & "!" & "special text;" & cbFontSpecialText.Text & ";" & txtFontSizeSpecialtext.Text & ";" & txtXSpecialText.Text & ";" & txtYSpecialText.Text & ";" & chkRTLspecialText.Checked & ";" & cbSpecialText.Checked & ";" & cbSpecialTextBold.Checked & ";" & chkCenterSpecialText.Checked
        prnTemplate = prnTemplate & "!" & "logo;" & "" & ";" & "" & ";" & txtXlogo.Text & ";" & txtYlogo.Text & ";" & "" & ";" & cbLogo.Checked & ";" & ""
        prnTemplate = prnTemplate & "!" & "AccNo;" & cbFontAccNo.Text & ";" & txtFontSizeAccNo.Text & ";" & txtXAccNo.Text & ";" & txtYAccNo.Text & ";" & chkRTLAccNo.Checked & ";" & cbAccNo.Checked & ";" & cbAccNoBold.Checked & ";" & chkCenterAccNo.Checked
        prnTemplate = prnTemplate & "!" & "Sheba;" & cbFontSheba.Text & ";" & txtFontSizeSheba.Text & ";" & txtXSheba.Text & ";" & txtYSheba.Text & ";" & chkRTLSheba.Checked & ";" & cbSheba.Checked & ";" & cbShebaBold.Checked & ";" & chkCenterSheba.Checked
        If lvCardText.Items.Count > 0 Then
            For i As Integer = 0 To lvCardText.Items.Count - 1
                Dim f As New Font(lvCardText.Items(i).SubItems(1).Text, lvCardText.Items(i).SubItems(3).Text)
                prnTemplate = prnTemplate & "!" & "text;" & lvCardText.Items(i).SubItems(1).Text & ";" & lvCardText.Items(i).SubItems(3).Text & ";" & lvCardText.Items(i).Text & ";" & lvCardText.Items(i).SubItems(4).Text & ";" & lvCardText.Items(i).SubItems(5).Text & ";" & lvCardText.Items(i).SubItems(2).Text
            Next
        End If
    End Sub

    Private Sub btnSavePrintTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSavePrintTemplate.Click
        If cbEnabled.SelectedIndex < 0 Then
            MessageBox.Show("لطفا وضعیت الگو را انتخاب نمایید", "خطا")
            Exit Sub
        End If
        MakePrintTemplate()
        Try
            tmpWSResult = CiBS_WS.UpdateCardPrintTemplate(Encrypt(txtProductCode.Text.Trim & "|" & tmpCradType(cbCardType.SelectedIndex) & "|" & prnTemplate & "|" & cbEnabled.SelectedIndex & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99"))
            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Print Template" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "Print template for card NO " & tmpCradType(cbCardType.SelectedIndex) & " was changed" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("خطا هنگام ذخیره سازی, لطفا دوباره سعی نمایید", "خطا")
            Exit Sub
        End Try
        tmpAns = Decrypt(tmpWSResult)
        MessageBox.Show("الگوی چاپ با موفقیت ذخیره شد", "اعلان")


    End Sub

    Private Sub cbCardType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCardType.SelectedIndexChanged
        LoadAndShowPrintTemplate()
    End Sub

    Private Sub btnCopyTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyTemplate.Click
        If cbEnabled.SelectedIndex < 0 Then
            MessageBox.Show("لطفا وضعیت الگو را انتخاب نمایید", "خطا")
            Exit Sub
        End If
        Dim tmpPrnTemplate As String = InputBox("نام الگوی چاپ را وارد نمایید", "دریافت نام الگوی چاپ / نوع کارت")
        If tmpPrnTemplate.Length < 3 Then
            MessageBox.Show("نام الگوی چاپ / نوع کارت را وارد نمایید")
            Exit Sub
        End If
        MakePrintTemplate()
        Try
            tmpWSResult = CiBS_WS.InsertCardPrintTemplate(Encrypt(txtProductCode.Text.Trim & "|" & tmpPrnTemplate & "|" & prnTemplate & "|" & cbEnabled.SelectedIndex & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99"))
            If Not InsertLog(CurrentBranchCode & "|" & CiBS_Parent.lblLogedInUser.Text.Trim & "|" & "Insert New Print Template" & "|" & DT.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8) & "|" & "New Print template with name  " & tmpPrnTemplate & " was inserted" & "|" & ClientIP & "|" & Now.TimeOfDay.TotalSeconds) Then MessageBox.Show("خطا هنگام ارسال لاگ به مرکز", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("خطا هنگام ذخیره سازی, لطفا دوباره سعی نمایید", "خطا")
            Exit Sub
        End Try
        tmpAns = Decrypt(tmpWSResult)
        MessageBox.Show("الگوی چاپ با موفقیت اضافه شد", "اعلان")
        getCardTypes()
    End Sub
End Class