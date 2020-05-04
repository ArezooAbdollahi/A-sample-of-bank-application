Imports System.Net.Mime.MediaTypeNames
Imports System.Runtime.InteropServices

Public Class Reports
    Public dset As New DataSet
    Dim TmpbtnReportDateTimeClick As Boolean = False
    Dim TmpbtnReportDate As Boolean = False
    Dim jender As String

#Region "GetDefaultPrinter"
    <DllImport("winspool.drv", EntryPoint:="GetDefaultPrinter", _
         SetLastError:=True, CharSet:=CharSet.Auto, _
         ExactSpelling:=False, _
         CallingConvention:=CallingConvention.StdCall)> _
    Private Shared Function GetDefaultPrinter(ByVal pszBuffer As System.Text.StringBuilder, _
                                              ByRef BufferSize As Int32) As Boolean

    End Function
#End Region

#Region "SetDefaultPrinter"
    <DllImport("winspool.drv", EntryPoint:="SetDefaultPrinter", _
         SetLastError:=True, CharSet:=CharSet.Auto, _
         ExactSpelling:=False, _
         CallingConvention:=CallingConvention.StdCall)> _
    Private Shared Function SetDefaultPrinter(ByVal PrinterName As String) As Boolean

    End Function
#End Region

    Public Shared Property DefaultPrinterName() As String
        Get
            '\\ Go through the list of printers and return the default one
            Dim lpsRet As New System.Text.StringBuilder(256), chars As Integer = 256
            If GetDefaultPrinter(lpsRet, chars) Then

            End If
            Return lpsRet.ToString
        End Get
        Set(ByVal value As String)
            '\\ Go through the list of printers and if you find the one named as above make it the default
            If Not SetDefaultPrinter(value) Then
                Trace.WriteLine("Failed to set printer to : " & value)
            End If
        End Set
    End Property

    Dim MyReportSelectCommand As String
    Dim tmpCradType As String()
    Dim tmpConsumables As String()

    Private Sub Reports_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        CiBS_WS.Timeout = CiBS_WS.Timeout / 10
    End Sub

    Private Sub Reports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CiBS_WS.Timeout = CiBS_WS.Timeout * 10
        txtBrCode.Text = CurrentBranchCode
        txtFromDate.Text = DT.DateToPersian(Now.Date).ShortDate
        txtToDate.Text = DT.DateToPersian(Now.Date).ShortDate
        If UserGrantLevel = 3 Then   'admin
            txtBrCode.Enabled = True
            rbEvents.Enabled = True
            cbReposotoryStatus.Enabled = True
            rbConflictConsumables.Enabled = True
            rbCardDataRemain.Enabled = True
            rbImportedDataReport.Enabled = True

            GroupBox3.Enabled = True
            GroupBox3.Visible = True
            btnReportDateTime.Enabled = True
            btnReportDateTime.Visible = True
        Else
            GroupBox3.Enabled = False
            GroupBox3.Visible = False
            btnReportDateTime.Enabled = False
            btnReportDateTime.Visible = False
        End If
        If UserGrantLevel = 4 Then  'support
            'txtBrCode.Enabled = True
            'rbIssuedCards.Enabled = False
            'rbConsumables.Checked = True
            'cbReposotoryStatus.Enabled = True
            'rbConflictConsumables.Enabled = True
            txtBrCode.Enabled = True
            rbEvents.Enabled = True
            cbReposotoryStatus.Enabled = True
            rbConflictConsumables.Enabled = True
            rbCardDataRemain.Enabled = True
            rbImportedDataReport.Enabled = True
        End If
        'get card types
        Try
            tmpWSResult = CiBS_WS.GetCardTypes(Encrypt("ALL" & "|" & "99"))
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


        ReDim tmpCradType(tmpAns.Length - 1)

        For i As Integer = 0 To tmpAns.Length - 2
            Dim tmp As String() = Decrypt(tmpAns(i)).Split("|")
            tmpCradType(i) = tmp(0)
            cbCardType.Items.Add(tmp(1).Trim)
        Next

        tmpWSResult = ""
        tmpAns = {}
        'get Consumables
        Try
            tmpWSResult = CiBS_WS.GetConsumables(Encrypt("1|" & Now.TimeOfDay.TotalSeconds))
        Catch ex As Exception
            InsertLocalLog("GetConsumables- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت انواع مواد مصرفی" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Exit Sub
        End Try

        tmpAns = tmpWSResult.Split("!")

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

    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        ExportToExcel()
    End Sub

    Private Sub ExportToExcel()
        Try
            'MessageBox.Show("1")
            Dim oldCI As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")

            If ((dgvCards.Columns.Count = 0) Or (dgvCards.Rows.Count = 0)) Then
                Exit Sub
            End If
            'MessageBox.Show("2")
            Dim excel As New Microsoft.Office.Interop.Excel.ApplicationClass
            Dim wBook As Microsoft.Office.Interop.Excel.Workbook
            Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet
            '' excel.Visible = True
            wBook = excel.Workbooks.Add()
            wSheet = wBook.ActiveSheet()

            Dim dt As System.Data.DataTable = dset.Tables(0)
            Dim dc As System.Data.DataColumn
            Dim dr As System.Data.DataRow
            Dim colIndex As Integer = 0
            Dim rowIndex As Integer = 0
            wSheet.Range("D:D").NumberFormat = "@"
            wSheet.Range("P:P").NumberFormat = "@"

            'MessageBox.Show("3")
            wSheet.Columns.HorizontalAlignment = 3
            wSheet.Rows.VerticalAlignment = 2
            wSheet.DisplayRightToLeft = True

            For Each dc In dt.Columns
                ' MessageBox.Show("4")
                colIndex = colIndex + 1
                excel.Cells(1, colIndex) = dc.ColumnName
            Next

            For Each dr In dt.Rows
                'MessageBox.Show("5")
                rowIndex = rowIndex + 1
                colIndex = 0
                For Each dc In dt.Columns
                    colIndex = colIndex + 1
                    excel.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
                Next
            Next

            '//////////////////
            '  MessageBox.Show("6")
            wSheet.UsedRange.NumberFormat = "####0"
            wSheet.Range("H:H").NumberFormat = "00000####0"
            wSheet.Range("N:N").NumberFormat = "00########0"
            wSheet.Columns.AutoFit()
            wSheet.UsedRange.Cells.Borders.Weight = 2
            wSheet.PageSetup.CenterHorizontally = True
            wSheet.PageSetup.PrintTitleRows = "$1:$1"
            wSheet.PageSetup.FitToPagesTall = dt.Rows.Count \ 20 + 1
            wSheet.PageSetup.FitToPagesWide = 1
            wSheet.PageSetup.Zoom = False
            wSheet.PageSetup.LeftMargin = 0
            wSheet.PageSetup.RightMargin = 0
            'wSheet.PageSetup.PaperSize = 9
            wSheet.PageSetup.Orientation = 2

            ' MessageBox.Show("7")
            'Try
            '    Dim strFileName As String = "C:\GSS.xls"
            '    If System.IO.File.Exists(strFileName) Then
            '        '    MessageBox.Show("8")
            '        System.IO.File.Delete(strFileName)
            '    End If

            '    wBook.SaveCopyAs(strFileName)
            'Catch ex As Exception
            '    MessageBox.Show("ExportToExcel ex.Message:" & ex.Message)
            'End Try


            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI

            '''''''' 970703
            ''If System.IO.File.Exists(strFileName) Then
            ''    System.IO.File.Delete(strFileName)
            ''End If
            '''''''' 970703
            excel.Visible = True

        Catch ex As Exception
            'MessageBox.Show("10 ex.Message :" & ex.Message)

        End Try

    End Sub

    Private Sub ExportToExcel0()

        Dim oldCI As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")

        If ((dgvCards.Columns.Count = 0) Or (dgvCards.Rows.Count = 0)) Then
            Exit Sub
        End If

        'Creating dataset to export
        Dim dset As New DataSet
        'add table to dataset
        dset.Tables.Add()
        'add column to that table
        For i As Integer = 0 To dgvCards.ColumnCount - 1
            dset.Tables(0).Columns.Add(dgvCards.Columns(i).HeaderText)
        Next
        'add rows to the table
        Dim dr1 As DataRow
        ' Dim f As String
        For i As Integer = 0 To dgvCards.RowCount - 1
            dr1 = dset.Tables(0).NewRow
            For j As Integer = 0 To dgvCards.Columns.Count - 1

                dr1(j) = dgvCards.Rows(i).Cells(j).Value
            Next
            dset.Tables(0).Rows.Add(dr1)
        Next

        Dim excel As New Microsoft.Office.Interop.Excel.ApplicationClass
        Dim wBook As Microsoft.Office.Interop.Excel.Workbook
        Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet
        excel.Visible = True
        wBook = excel.Workbooks.Add()
        wSheet = wBook.ActiveSheet()

        Dim dt As System.Data.DataTable = dset.Tables(0)
        Dim dc As System.Data.DataColumn
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0
        wSheet.Range("D:D").NumberFormat = "@"

        wSheet.Columns.HorizontalAlignment = 3
        wSheet.Rows.VerticalAlignment = 2
        wSheet.DisplayRightToLeft = True

        For Each dc In dt.Columns
            colIndex = colIndex + 1
            excel.Cells(1, colIndex) = dc.ColumnName
        Next

        For Each dr In dt.Rows
            rowIndex = rowIndex + 1
            colIndex = 0
            For Each dc In dt.Columns
                colIndex = colIndex + 1
                excel.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
            Next
        Next

        '//////////////////
        wSheet.UsedRange.NumberFormat = "####0"
        wSheet.Range("H:H").NumberFormat = "00000####0"
        wSheet.Range("N:N").NumberFormat = "00########0"

        wSheet.Columns.AutoFit()
        wSheet.UsedRange.Cells.Borders.Weight = 2
        wSheet.PageSetup.CenterHorizontally = True
        wSheet.PageSetup.PrintTitleRows = "$1:$1"
        wSheet.PageSetup.FitToPagesTall = dt.Rows.Count \ 20 + 1
        wSheet.PageSetup.FitToPagesWide = 1
        wSheet.PageSetup.Zoom = False
        wSheet.PageSetup.LeftMargin = 0
        wSheet.PageSetup.RightMargin = 0
        wSheet.PageSetup.PaperSize = 9
        wSheet.PageSetup.Orientation = 2

        Dim strFileName As String = "c:\GSS.xls"
        If System.IO.File.Exists(strFileName) Then
            System.IO.File.Delete(strFileName)
        End If

        wBook.SaveCopyAs(strFileName)

        'Dim dp As String = DefaultPrinterName()

        'If Not dp.Contains("Adobe PDF") Then
        '    DefaultPrinterName = "Adobe PDF"
        'End If

        'Dim psi As New ProcessStartInfo
        'psi.UseShellExecute = True
        'psi.Verb = "print"
        'psi.CreateNoWindow = True
        'psi.WindowStyle = ProcessWindowStyle.Hidden
        'PrintDialog1.PrinterSettings.PrinterName = "Adobe PDF"
        'psi.Arguments = PrintDialog1.PrinterSettings.PrinterName.ToString() ' Here specify printer name
        'psi.FileName = "c:\GSS.xls" ' Here specify a document to be printed
        'Process.Start(psi)

        'wBook.Close(False, strFileName, )
        'excel.DisplayAlerts = False

        'excel.Quit()
        'excel = Nothing

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        If System.IO.File.Exists(strFileName) Then
            System.IO.File.Delete(strFileName)
        End If
        '   DefaultPrinterName = dp
    End Sub
    Private Sub btnRunReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunReport.Click
        TmpbtnReportDateTimeClick = False
        TmpbtnReportDate = True
        GetReport()
    End Sub

    Private Sub GetReport()
        dgvCards.DataSource = ""
        If rbIssuedCards.Checked = True Then
            If cbReportType.SelectedIndex = 0 Then
                repIssuedCards()
            End If
            If cbReportType.SelectedIndex = 1 Then
                repIssuedCardsCounts()
            End If
            If cbReportType.SelectedIndex = 2 Then
                repIssuedCardsCountsBranch()
            End If
        End If
        If rbConsumables.Checked = True Then
            repConsumables()
        End If
        If rbEvents.Checked = True Then
            repEvents()
        End If

        If rbAccount.Checked = True Then
            repIssuedCardsAccount()
        End If

        If rbPan.Checked = True Then
            repIssuedCardsPAN()
        End If

        If rbUnusableCards.Checked Then
            repUnusableCards()
        End If
        If rbConflictConsumables.Checked Then
            repConflictConsumables()
        End If
        If rbCardDataRemain.Checked Then
            repCardDataRemain()
        End If
        If rbImportedDataReport.Checked Then
            repImportedData()
        End If
        If rbBranchStackStatus.Checked Then
            GetBranchStackStatusByDAte()
        End If
    End Sub

    Sub GetBranchStackStatusByDAte()
        Try
            tmpWSResult = CiBS_WS.GetBranchRepositoryStatusByDate(Encrypt(txtBrCode.Text & "|" & txtFromDate.Text & "|" & txtToDate.Text & "|" & Now.TimeOfDay.TotalSeconds))
        Catch ex As Exception
            InsertLocalLog("GetBranchRepositoryStatusByDate- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت گزارش" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim dtt As New DataTable("Cards")
        dtt.Columns.Add("کد شعبه")
        dtt.Columns.Add("نوع کارت")
        dtt.Columns.Add("مانده")

        dset.Clear()
        dset.Tables.Clear()
        dset.Tables.Add()

        dset.Tables(0).Columns.Add("کد شعبه")
        dset.Tables(0).Columns.Add("نوع کارت")
        dset.Tables(0).Columns.Add("مانده")

        Dim dr1 As DataRow

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
            dr1 = dset.Tables(0).NewRow
            Dim tmp As String()
            ReDim tmp(CardsRecords.Length - 1)
            tmp = CardsRecords(i).Split("|")
            dtt.Rows.Add(New Object() {tmp(0).Trim, tmp(1).Trim, tmp(2).Trim})

            dr1(0) = tmp(0).Trim
            dr1(1) = tmp(1).Trim
            dr1(2) = tmp(2).Trim

            dset.Tables(0).Rows.Add(dr1)

        Next
        dgvCards.DataSource = dtt
    End Sub
    Sub repImportedData()
        Try
            tmpWSResult = CiBS_WS.GetImportedDataStatus(Encrypt(txtFromDate.Text & "|" & txtToDate.Text & "|" & Now.TimeOfDay.TotalSeconds))
        Catch ex As Exception
            InsertLocalLog("GetImportedDataStatus- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت گزارش کارتهای وارد شده به سیستم" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Exit Sub
        End Try

        Dim dtt As New DataTable("Cards")
        dtt.Columns.Add("ردیف")
        dtt.Columns.Add("نوع کارت")
        dtt.Columns.Add("تاریخ اضافه شدن")
        dtt.Columns.Add("تعداد اضافه شده")
        dtt.Columns.Add("تعداد مصرف شده")
        dtt.Columns.Add("تعداد مانده")

        dset.Clear()
        dset.Tables.Clear()
        dset.Tables.Add()

        dset.Tables(0).Columns.Add("ردیف")
        dset.Tables(0).Columns.Add("نوع کارت")
        dset.Tables(0).Columns.Add("تاریخ اضافه شدن")
        dset.Tables(0).Columns.Add("تعداد اضافه شده")
        dset.Tables(0).Columns.Add("تعداد مصرف شده")
        dset.Tables(0).Columns.Add("تعداد مانده")

        Dim dr1 As DataRow


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
            dr1 = dset.Tables(0).NewRow
            Dim tmp As String()
            ReDim tmp(CardsRecords.Length - 1)
            tmp = CardsRecords(i).Split("|")
            dtt.Rows.Add(New Object() {i + 1, tmp(0).Trim, tmp(1).Trim, tmp(2).Trim, tmp(3).Trim, tmp(4).Trim})

            dr1(0) = i + 1
            dr1(1) = tmp(0).Trim
            dr1(2) = tmp(1).Trim
            dr1(3) = tmp(2).Trim
            dr1(4) = tmp(3).Trim
            dr1(5) = tmp(4).Trim

            dset.Tables(0).Rows.Add(dr1)

        Next
        dgvCards.DataSource = dtt
    End Sub

    Sub repUnusableCards()
        Try
            tmpWSResult = CiBS_WS.GetgetUnusableCards(Encrypt(txtFromDate.Text & "|" & txtToDate.Text & "|" & txtBrCode.Text & "|" & Now.TimeOfDay.TotalSeconds))
        Catch ex As Exception
            InsertLocalLog("GetgetUnusableCards- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت گزارش کارتهای معیوب" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Exit Sub
        End Try

        Dim dtt As New DataTable("Cards")
        dtt.Columns.Add("ردیف")
        dtt.Columns.Add("کد شعبه")
        dtt.Columns.Add("نوع کارت")
        dtt.Columns.Add("نوع ایراد")
        dtt.Columns.Add("شرح")
        dtt.Columns.Add("تاریخ")

        dset.Clear()
        dset.Tables.Clear()
        dset.Tables.Add()

        dset.Tables(0).Columns.Add("ردیف")
        dset.Tables(0).Columns.Add("کد شعبه")
        dset.Tables(0).Columns.Add("نوع کارت")
        dset.Tables(0).Columns.Add("نوع ایراد")
        dset.Tables(0).Columns.Add("شرح")
        dset.Tables(0).Columns.Add("تاریخ")

        Dim dr1 As DataRow

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
            dr1 = dset.Tables(0).NewRow
            Dim tmp As String()
            ReDim tmp(CardsRecords.Length - 1)
            tmp = CardsRecords(i).Split("|")
            dtt.Rows.Add(New Object() {i + 1, tmp(0).Trim, tmp(1).Trim, tmp(2).Trim, tmp(3).Trim, tmp(4).Trim})

            dr1(0) = i + 1
            dr1(1) = tmp(0).Trim
            dr1(2) = tmp(1).Trim
            dr1(3) = tmp(2).Trim
            dr1(4) = tmp(3).Trim
            dr1(5) = tmp(4).Trim

            dset.Tables(0).Rows.Add(dr1)
        Next
        dgvCards.DataSource = dtt
    End Sub


    Sub repIssuedCards()
        'Dim FrDateTime As String = txtFromDate.Text.Trim & "-00:00:00"
        'Dim ToDateTime As String = txtToDate.Text.Trim & "-23:59:59"
        'Dim str As String = FrDateTime.Trim & "|" & ToDateTime.Trim

        Dim str As String = txtFromDate.Text & "|" & txtToDate.Text
        If txtBrCode.Text = "0" Then
            str = str & "|" & "NULL"
        Else
            str = str & "|" & txtBrCode.Text
        End If

        If cbCardType.SelectedIndex = -1 Or cbCardType.SelectedIndex = 0 Then
            str = str & "|" & "NULL"
        Else
            str = str & "|" & tmpCradType(cbCardType.SelectedIndex - 1)
        End If


        Try
            If TmpbtnReportDateTimeClick = True Then
                tmpWSResult = CiBS_WS.GetReportData(Encrypt(TmpbtnReportDateTimeClick & "|" & str & "|NULL|1|" & Now.TimeOfDay.TotalSeconds & "|" & "99"))
            Else
                tmpWSResult = CiBS_WS.GetReportData(Encrypt(TmpbtnReportDateTimeClick & "|" & str & "|NULL|1|" & Now.TimeOfDay.TotalSeconds & "|" & "99"))
            End If
        Catch ex As Exception
            InsertLocalLog("GetReportData- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت گزارش" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim dtt As New DataTable("Cards")
        dtt.Columns.Add("ردیف")
        dtt.Columns.Add("کد شعبه")
        dtt.Columns.Add("نوع کارت")
        dtt.Columns.Add("شماره کارت")
        dtt.Columns.Add("شماره حساب / مبلغ کارت")
        dtt.Columns.Add("نام دارنده / خریدار کارت")
        dtt.Columns.Add("فامیلی دارنده / خریدار کارت")
        dtt.Columns.Add("کد ملی دارنده / خریدار کارت")
        dtt.Columns.Add("تاریخ وساعت صدور")
        dtt.Columns.Add("کاربر صادر کننده")
        dtt.Columns.Add("وضعیت صدور")
        dtt.Columns.Add("متن خاص")
        dtt.Columns.Add("شماره فیش واریزی")
        dtt.Columns.Add("شماره موبایل")
        dtt.Columns.Add("شماره حساب هدیه ")
        If TmpbtnReportDateTimeClick = True Then
            dtt.Columns.Add("شماره پیگیری")
            dtt.Columns.Add("شماره مرجع")
            dtt.Columns.Add("تاریخ پوز")
            dtt.Columns.Add("ساعت پوز ")
            dtt.Columns.Add("RespCodePOS")
            dtt.Columns.Add("RespMessagePOS")
            dtt.Columns.Add("CardNoPOS")
            dtt.Columns.Add("CardTypePOS ")
            dtt.Columns.Add("TerminalPOS")
            dtt.Columns.Add("MerchantPOS")
            dtt.Columns.Add("TrAmountPOS")
        Else
            dtt.Columns.Add("شماره کارت قبلی")
            dtt.Columns.Add("شماره حساب قبلی")
            dtt.Columns.Add("رنگ کارت قبلی")
        End If
        dtt.Columns.Add("کد جنسیت")


        ' ds
        dset.Clear()
        dset.Tables.Clear()
        dset.Tables.Add()

        dset.Tables(0).Columns.Add("ردیف")
        dset.Tables(0).Columns.Add("کد شعبه")
        dset.Tables(0).Columns.Add("نوع کارت")
        dset.Tables(0).Columns.Add("شماره کارت")
        dset.Tables(0).Columns.Add("شماره حساب / مبلغ کارت")
        dset.Tables(0).Columns.Add("نام دارنده / خریدار کارت")
        dset.Tables(0).Columns.Add("فامیلی دارنده / خریدار کارت")
        dset.Tables(0).Columns.Add("کد ملی دارنده / خریدار کارت")
        dset.Tables(0).Columns.Add("تاریخ وساعت صدور")
        dset.Tables(0).Columns.Add("کاربر صادر کننده")
        dset.Tables(0).Columns.Add("وضعیت صدور")
        dset.Tables(0).Columns.Add("متن خاص")
        dset.Tables(0).Columns.Add("شماره فیش واریزی")
        dset.Tables(0).Columns.Add("شماره موبایل")
        dset.Tables(0).Columns.Add("شماره حساب هدیه ")
        If TmpbtnReportDateTimeClick = True Then
            dset.Tables(0).Columns.Add("شماره پیگیری")
            dset.Tables(0).Columns.Add("شماره مرجع")
            dset.Tables(0).Columns.Add("تاریخ پوز")
            dset.Tables(0).Columns.Add("ساعت پوز ")
            dset.Tables(0).Columns.Add("RespCodePOS")
            dset.Tables(0).Columns.Add("RespMessagePOS")
            dset.Tables(0).Columns.Add("CardNoPOS")
            dset.Tables(0).Columns.Add("CardTypePOS ")
            dset.Tables(0).Columns.Add("TerminalPOS")
            dset.Tables(0).Columns.Add("MerchantPOS")
            dset.Tables(0).Columns.Add("TrAmountPOS")
        Else
            dset.Tables(0).Columns.Add("شماره کارت قبلی")
            dset.Tables(0).Columns.Add("شماره حساب قبلی")
            dset.Tables(0).Columns.Add("رنگ کارت قبلی")
        End If
        dset.Tables(0).Columns.Add("زن1/کد جنسیت")

        ''''
        Dim dr1 As DataRow
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
            dr1 = dset.Tables(0).NewRow
            Dim tmp As String()
            ReDim tmp(CardsRecords.Length - 1)
            tmp = CardsRecords(i).Split("|")
            If TmpbtnReportDateTimeClick = True Then

                If tmp(26).Trim = "" Or tmp(26).Trim = " " Then
                    jender = ""
                Else
                    If tmp(26).Trim = 1 Then
                        jender = "1"
                    Else
                        jender = "0"
                    End If
                End If
                dtt.Rows.Add(New Object() {i + 1, tmp(0).Trim, tmp(1).Trim, tmp(2).Trim, tmp(3).Trim, tmp(4).Trim, tmp(13).Trim, tmp(10).Trim, tmp(5).Trim, tmp(6).Trim, tmp(7).Trim, tmp(9).Trim, tmp(11).Trim, tmp(12).Trim, tmp(14).Trim, tmp(15).Trim, tmp(16).Trim, tmp(17).Trim, tmp(18).Trim, tmp(19).Trim, tmp(20).Trim, tmp(21).Trim, tmp(22).Trim, tmp(23).Trim, tmp(24).Trim, tmp(25).Trim, tmp(26).Trim})
                dr1(0) = i + 1
                dr1(1) = tmp(0).Trim
                dr1(2) = tmp(1).Trim
                dr1(3) = tmp(2).Trim
                dr1(4) = tmp(3).Trim
                dr1(5) = tmp(4).Trim
                dr1(6) = tmp(13).Trim
                dr1(7) = tmp(10).Trim
                dr1(8) = tmp(5).Trim
                dr1(9) = tmp(6).Trim
                dr1(10) = tmp(7).Trim
                dr1(11) = tmp(9).Trim
                dr1(12) = tmp(11).Trim
                dr1(13) = tmp(12).Trim
                dr1(14) = tmp(14).Trim
                dr1(15) = tmp(15).Trim
                dr1(16) = tmp(16).Trim
                dr1(17) = tmp(17).Trim
                dr1(18) = tmp(18).Trim
                dr1(19) = tmp(19).Trim
                dr1(20) = tmp(20).Trim
                dr1(21) = tmp(21).Trim
                dr1(22) = tmp(22).Trim
                dr1(23) = tmp(23).Trim
                dr1(24) = tmp(24).Trim
                dr1(25) = tmp(25).Trim
                dr1(26) = jender 'tmp(26).Trim
            Else
                If tmp(18).Trim = "" Or tmp(18).Trim = " " Then
                    jender = ""
                Else
                    If tmp(18).Trim = 1 Then
                        jender = "1"
                    Else
                        jender = "0"
                    End If
                End If


                dtt.Rows.Add(New Object() {i + 1, tmp(0).Trim, tmp(1).Trim, tmp(2).Trim, tmp(3).Trim, tmp(4).Trim, tmp(13).Trim, tmp(10).Trim, tmp(5).Trim, tmp(6).Trim, tmp(7).Trim, tmp(9).Trim, tmp(11).Trim, tmp(12).Trim, tmp(14).Trim, tmp(15).Trim, tmp(16).Trim, tmp(17).Trim, tmp(18).Trim})
                dr1(0) = i + 1
                dr1(1) = tmp(0).Trim
                dr1(2) = tmp(1).Trim
                dr1(3) = tmp(2).Trim
                dr1(4) = tmp(3).Trim
                dr1(5) = tmp(4).Trim
                dr1(6) = tmp(13).Trim
                dr1(7) = tmp(10).Trim
                dr1(8) = tmp(5).Trim
                dr1(9) = tmp(6).Trim
                dr1(10) = tmp(7).Trim
                dr1(11) = tmp(9).Trim
                dr1(12) = tmp(11).Trim
                dr1(13) = tmp(12).Trim
                dr1(14) = tmp(14).Trim
                dr1(15) = tmp(15).Trim
                dr1(16) = tmp(16).Trim
                dr1(17) = tmp(17).Trim
                dr1(18) = jender 'tmp(18).Trim
            End If
            dset.Tables(0).Rows.Add(dr1)
        Next

        dgvCards.DataSource = dtt

    End Sub

    Sub repIssuedCardsAccount()
        Dim str As String = txtFromDate.Text & "|" & txtToDate.Text

        If txtBrCode.Text = "0" Then
            str = str & "|" & "NULL"
        Else
            str = str & "|" & txtBrCode.Text
        End If

        If cbCardType.SelectedIndex = -1 Or cbCardType.SelectedIndex = 0 Then
            str = str & "|" & "NULL"
        Else
            str = str & "|" & tmpCradType(cbCardType.SelectedIndex - 1)
        End If

        Try
            tmpWSResult = CiBS_WS.GetReportData(Encrypt(TmpbtnReportDateTimeClick & "|" & str & "|" & txtAccount.Text.Trim & "|2|" & Now.TimeOfDay.TotalSeconds & "|" & "99"))
        Catch ex As Exception
            InsertLocalLog("GetReportData- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت گزارش" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim dtt As New DataTable("Cards")
        dtt.Columns.Add("ردیف")
        dtt.Columns.Add("کد شعبه")
        dtt.Columns.Add("نوع کارت")
        dtt.Columns.Add("شماره کارت")
        dtt.Columns.Add("شماره حساب")
        dtt.Columns.Add("نام دارنده کارت")
        dtt.Columns.Add("فامیلی دارنده کارت")
        dtt.Columns.Add("تاریخ وساعت صدور")
        dtt.Columns.Add("کاربر صادر کننده")
        dtt.Columns.Add("وضعیت صدور")
        dtt.Columns.Add("متن خاص")
        dtt.Columns.Add("شماره فیش واریزی")
        dtt.Columns.Add("شماره موبایل")
        dtt.Columns.Add("شماره حساب هدیه")
        dtt.Columns.Add("شماره کارت قبلی")
        dtt.Columns.Add("شماره حساب قبلی")
        dtt.Columns.Add("رنگ کارت قبلی")

        dset.Clear()
        dset.Tables.Clear()
        dset.Tables.Add()

        dset.Tables(0).Columns.Add("ردیف")
        dset.Tables(0).Columns.Add("کد شعبه")
        dset.Tables(0).Columns.Add("نوع کارت")
        dset.Tables(0).Columns.Add("شماره کارت")
        dset.Tables(0).Columns.Add("شماره حساب")
        dset.Tables(0).Columns.Add("نام دارنده کارت")
        dset.Tables(0).Columns.Add("فامیلی دارنده کارت")
        dset.Tables(0).Columns.Add("تاریخ وساعت صدور")
        dset.Tables(0).Columns.Add("کاربر صادر کننده")
        dset.Tables(0).Columns.Add("وضعیت صدور")
        dset.Tables(0).Columns.Add("متن خاص")
        dset.Tables(0).Columns.Add("شماره فیش واریزی")
        dset.Tables(0).Columns.Add("شماره موبایل")
        dset.Tables(0).Columns.Add("شماره حساب هدیه")
        dset.Tables(0).Columns.Add("شماره کارت قبلی")
        dset.Tables(0).Columns.Add("شماره حساب قبلی")
        dset.Tables(0).Columns.Add("رنگ کارت قبلی")
        Dim dr1 As DataRow

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
            dr1 = dset.Tables(0).NewRow
            Dim tmp As String()
            ReDim tmp(CardsRecords.Length - 1)
            tmp = CardsRecords(i).Split("|")
            dtt.Rows.Add(New Object() {i + 1, tmp(0).Trim, tmp(1).Trim, tmp(2).Trim, tmp(3).Trim, tmp(4).Trim, tmp(13), tmp(5).Trim, tmp(6).Trim, tmp(7).Trim, tmp(9).Trim, tmp(11).Trim, tmp(12).Trim, tmp(14).Trim})

            dr1(0) = i + 1
            dr1(1) = tmp(0).Trim
            dr1(2) = tmp(1).Trim
            dr1(3) = tmp(2).Trim
            dr1(4) = tmp(3).Trim
            dr1(5) = tmp(4).Trim
            dr1(6) = tmp(13).Trim
            dr1(7) = tmp(5).Trim
            dr1(8) = tmp(6).Trim
            dr1(9) = tmp(7).Trim
            dr1(10) = tmp(9).Trim
            dr1(11) = tmp(11).Trim
            dr1(12) = tmp(12).Trim
            dr1(13) = tmp(14).Trim
            
            dset.Tables(0).Rows.Add(dr1)

        Next
        dgvCards.DataSource = dtt
    End Sub

    Sub repIssuedCardsPAN()
        Dim str As String = txtFromDate.Text & "|" & txtToDate.Text

        If txtBrCode.Text = "0" Then
            str = str & "|" & "NULL"
        Else
            str = str & "|" & txtBrCode.Text
        End If

        If cbCardType.SelectedIndex = -1 Or cbCardType.SelectedIndex = 0 Then
            str = str & "|" & "NULL"
        Else
            str = str & "|" & tmpCradType(cbCardType.SelectedIndex - 1)
        End If

        Try
            tmpWSResult = CiBS_WS.GetReportData(Encrypt(TmpbtnReportDateTimeClick & "|" & str & "|" & txtPAN.Text.Trim & "|3|" & Now.TimeOfDay.TotalSeconds & "|" & "99"))
        Catch ex As Exception
            InsertLocalLog("GetReportData- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت گزارش" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim dtt As New DataTable("Cards")
        dtt.Columns.Add("ردیف")
        dtt.Columns.Add("کد شعبه")
        dtt.Columns.Add("نوع کارت")
        dtt.Columns.Add("شماره کارت")
        dtt.Columns.Add("شماره حساب")
        dtt.Columns.Add("نام دارنده کارت")
        dtt.Columns.Add("فامیلی دارنده کارت")
        dtt.Columns.Add("تاریخ وساعت صدور")
        dtt.Columns.Add("کاربر صادر کننده")
        dtt.Columns.Add("وضعیت صدور")
        dtt.Columns.Add("متن خاص")
        dtt.Columns.Add("شماره فیش واریزی")
        dtt.Columns.Add("شماره موبایل")
        dtt.Columns.Add("شماره حساب هدیه")
        If TmpbtnReportDateTimeClick = True Then
            dtt.Columns.Add("شماره پیگیری")
            dtt.Columns.Add("شماره مرجع")
            dtt.Columns.Add("تاریخ پوز")
            dtt.Columns.Add("ساعت پوز ")
            dtt.Columns.Add("RespCodePOS")
            dtt.Columns.Add("RespMessagePOS")
            dtt.Columns.Add("CardNoPOS")
            dtt.Columns.Add("CardTypePOS ")
            dtt.Columns.Add("TerminalPOS")
            dtt.Columns.Add("MerchantPOS")
            dtt.Columns.Add("TrAmountPOS")
        Else
            dtt.Columns.Add("شماره کارت قبلی")
            dtt.Columns.Add("شماره حساب قبلی")
            dtt.Columns.Add("رنگ کارت قبلی")
        End If
        dset.Clear()
        dset.Tables.Clear()
        dset.Tables.Add()

        dset.Tables(0).Columns.Add("ردیف")
        dset.Tables(0).Columns.Add("کد شعبه")
        dset.Tables(0).Columns.Add("نوع کارت")
        dset.Tables(0).Columns.Add("شماره کارت")
        dset.Tables(0).Columns.Add("شماره حساب")
        dset.Tables(0).Columns.Add("نام دارنده کارت")
        dset.Tables(0).Columns.Add("فامیلی دارنده کارت")
        dset.Tables(0).Columns.Add("تاریخ وساعت صدور")
        dset.Tables(0).Columns.Add("کاربر صادر کننده")
        dset.Tables(0).Columns.Add("وضعیت صدور")
        dset.Tables(0).Columns.Add("متن خاص")
        dset.Tables(0).Columns.Add("شماره فیش واریزی")
        dset.Tables(0).Columns.Add("شماره موبایل")
        dset.Tables(0).Columns.Add("شماره حساب هدیه")
        If TmpbtnReportDateTimeClick = True Then
            dset.Tables(0).Columns.Add("شماره پیگیری")
            dset.Tables(0).Columns.Add("شماره مرجع")
            dset.Tables(0).Columns.Add("تاریخ پوز")
            dset.Tables(0).Columns.Add("ساعت پوز ")
            dset.Tables(0).Columns.Add("RespCodePOS")
            dset.Tables(0).Columns.Add("RespMessagePOS")
            dset.Tables(0).Columns.Add("CardNoPOS")
            dset.Tables(0).Columns.Add("CardTypePOS ")
            dset.Tables(0).Columns.Add("TerminalPOS")
            dset.Tables(0).Columns.Add("MerchantPOS")
            dset.Tables(0).Columns.Add("TrAmountPOS")
        Else
            dset.Tables(0).Columns.Add("شماره کارت قبلی")
            dset.Tables(0).Columns.Add("شماره حساب قبلی")
            dset.Tables(0).Columns.Add("رنگ کارت قبلی")
        End If


        Dim dr1 As DataRow

        Dim tmpAns As String() = tmpWSResult.Split("!")
        If String.Compare(Decrypt(tmpAns(0)), "Timeout") = 0 Then
            MessageBox.Show("Timeout", "خطا")
            Exit Sub
        End If
        Dim CardsRecords(tmpAns.Length - 2) As String
        For i As Integer = 0 To tmpAns.Length - 2
            CardsRecords(i) = Decrypt(tmpAns(i))
        Next
        If TmpbtnReportDateTimeClick = True Then
            For i As Integer = 0 To CardsRecords.Length - 1
                dr1 = dset.Tables(0).NewRow
                Dim tmp As String()
                ReDim tmp(CardsRecords.Length - 1)
                tmp = CardsRecords(i).Split("|")
                dtt.Rows.Add(New Object() {i + 1, tmp(0).Trim, tmp(1).Trim, tmp(2).Trim, tmp(3).Trim, tmp(4).Trim, tmp(13).Trim, tmp(5).Trim, tmp(6).Trim, tmp(7).Trim, tmp(9).Trim, tmp(11).Trim, tmp(12).Trim, tmp(14).Trim, tmp(15).Trim, tmp(16).Trim, tmp(17).Trim, tmp(18).Trim, tmp(19).Trim, tmp(20).Trim, tmp(21).Trim, tmp(22).Trim, tmp(23).Trim, tmp(24).Trim, tmp(25).Trim})

                dr1(0) = i + 1
                dr1(1) = tmp(0).Trim
                dr1(2) = tmp(1).Trim
                dr1(3) = tmp(2).Trim
                dr1(4) = tmp(3).Trim
                dr1(5) = tmp(4).Trim
                dr1(6) = tmp(13).Trim
                dr1(7) = tmp(5).Trim
                dr1(8) = tmp(6).Trim
                dr1(9) = tmp(7).Trim
                dr1(10) = tmp(9).Trim
                dr1(11) = tmp(11).Trim
                dr1(12) = tmp(12).Trim
                dr1(13) = tmp(14).Trim
                dr1(14) = tmp(15).Trim
                dr1(15) = tmp(16).Trim
                dr1(16) = tmp(17).Trim
                dr1(17) = tmp(18).Trim
                dr1(18) = tmp(19).Trim
                dr1(19) = tmp(20).Trim
                dr1(20) = tmp(21).Trim
                dr1(21) = tmp(22).Trim
                dr1(22) = tmp(23).Trim
                dr1(23) = tmp(24).Trim
                dr1(24) = tmp(25).Trim


                dset.Tables(0).Rows.Add(dr1)

            Next
        Else
            For i As Integer = 0 To CardsRecords.Length - 1
                dr1 = dset.Tables(0).NewRow
                Dim tmp As String()
                ReDim tmp(CardsRecords.Length - 1)
                tmp = CardsRecords(i).Split("|")
                dtt.Rows.Add(New Object() {i + 1, tmp(0).Trim, tmp(1).Trim, tmp(2).Trim, tmp(3).Trim, tmp(4).Trim, tmp(13).Trim, tmp(5).Trim, tmp(6).Trim, tmp(7).Trim, tmp(9).Trim, tmp(11).Trim, tmp(12).Trim, tmp(14).Trim, tmp(15).Trim, tmp(16).Trim, tmp(17).Trim})

                dr1(0) = i + 1
                dr1(1) = tmp(0).Trim
                dr1(2) = tmp(1).Trim
                dr1(3) = tmp(2).Trim
                dr1(4) = tmp(3).Trim
                dr1(5) = tmp(4).Trim
                dr1(6) = tmp(13).Trim
                dr1(7) = tmp(5).Trim
                dr1(8) = tmp(6).Trim
                dr1(9) = tmp(7).Trim
                dr1(10) = tmp(9).Trim
                dr1(11) = tmp(11).Trim
                dr1(12) = tmp(12).Trim
                dr1(13) = tmp(14).Trim
                dr1(14) = tmp(15).Trim
                dr1(15) = tmp(16).Trim
                dr1(16) = tmp(17).Trim
                'dr1(17) = tmp(18).Trim
                'dr1(18) = tmp(19).Trim
                'dr1(19) = tmp(20).Trim
                'dr1(20) = tmp(21).Trim
                'dr1(21) = tmp(22).Trim
                'dr1(22) = tmp(23).Trim
                'dr1(23) = tmp(24).Trim
                'dr1(24) = tmp(25).Trim


                dset.Tables(0).Rows.Add(dr1)

            Next
        End If
        dgvCards.DataSource = dtt
    End Sub

    Sub repIssuedCardsCounts()
        Dim str As String = txtFromDate.Text & "|" & txtToDate.Text

        If txtBrCode.Text = "0" Then
            str = str & "|" & "NULL"
        Else
            str = str & "|" & txtBrCode.Text
        End If

        If cbCardType.SelectedIndex = -1 Or cbCardType.SelectedIndex = 0 Then
            str = str & "|" & "NULL"
        Else
            str = str & "|" & tmpCradType(cbCardType.SelectedIndex - 1)
        End If


        Try
            tmpWSResult = CiBS_WS.GetReportDataCounts(Encrypt(str & "|" & Now.TimeOfDay.TotalSeconds))
        Catch ex As Exception
            InsertLocalLog("GetReportDataCounts- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت گزارش" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim dtt As New DataTable("Cards")
        dtt.Columns.Add("نوع کارت")
        dtt.Columns.Add("وضعیت صدور")
        dtt.Columns.Add("تعداد صادره")

        dset.Clear()
        dset.Tables.Clear()
        dset.Tables.Add()

        dset.Tables(0).Columns.Add("نوع کارت")
        dset.Tables(0).Columns.Add("وضعیت صدور")
        dset.Tables(0).Columns.Add("تعداد صادره")
        Dim dr1 As DataRow

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
            dr1 = dset.Tables(0).NewRow
            Dim tmp As String()
            ReDim tmp(CardsRecords.Length - 1)
            tmp = CardsRecords(i).Split("|")
            dtt.Rows.Add(New Object() {tmp(1).Trim, tmp(2).Trim, tmp(0).Trim})

            dr1(0) = tmp(1).Trim
            dr1(1) = tmp(2).Trim
            dr1(2) = tmp(0).Trim
           
            dset.Tables(0).Rows.Add(dr1)
        Next
        dgvCards.DataSource = dtt
    End Sub

    Sub repIssuedCardsCountsBranch()
        Dim str As String = txtFromDate.Text & "|" & txtToDate.Text

        If txtBrCode.Text = "0" Then
            str = str & "|" & "NULL"
        Else
            str = str & "|" & txtBrCode.Text
        End If

        If cbCardType.SelectedIndex = -1 Or cbCardType.SelectedIndex = 0 Then
            str = str & "|" & "NULL"
        Else
            str = str & "|" & tmpCradType(cbCardType.SelectedIndex - 1)
        End If


        Try
            tmpWSResult = CiBS_WS.GetReportDataCountsBranch(Encrypt(str & "|" & Now.TimeOfDay.TotalSeconds))
        Catch ex As Exception
            InsertLocalLog("GetReportDataCountsBranch- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت گزارش" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim dtt As New DataTable("Cards")
        dtt.Columns.Add("کد شعبه")
        dtt.Columns.Add("نوع کارت")
        dtt.Columns.Add("وضعیت صدور")
        dtt.Columns.Add("تعداد صادره")

        dset.Clear()
        dset.Tables.Clear()
        dset.Tables.Add()

        dset.Tables(0).Columns.Add("کد شعبه")
        dset.Tables(0).Columns.Add("نوع کارت")
        dset.Tables(0).Columns.Add("وضعیت صدور")
        dset.Tables(0).Columns.Add("تعداد صادره")
        Dim dr1 As DataRow

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
            dr1 = dset.Tables(0).NewRow
            Dim tmp As String()
            ReDim tmp(CardsRecords.Length - 1)
            tmp = CardsRecords(i).Split("|")
            dtt.Rows.Add(New Object() {tmp(0).Trim, tmp(2).Trim, tmp(3).Trim, tmp(1).Trim})

            dr1(0) = tmp(0).Trim
            dr1(1) = tmp(2).Trim
            dr1(2) = tmp(3).Trim
            dr1(3) = tmp(1).Trim

            dset.Tables(0).Rows.Add(dr1)

        Next
        dgvCards.DataSource = dtt
    End Sub

    Sub repConsumables()
        Dim str As String = txtFromDate.Text & "|" & txtToDate.Text
        If cbConsumables.SelectedIndex = -1 Or cbConsumables.SelectedIndex = 0 Then
            str = "NULL"
        Else
            str = tmpConsumables(cbConsumables.SelectedIndex - 1)
        End If

        If txtBrCode.Text = 0 Then
            str = str & "|" & "NULL"
        Else
            str = str & "|" & txtBrCode.Text.Trim
        End If

        If cbReposotoryStatus.SelectedIndex = -1 Then
            Try
                tmpWSResult = CiBS_WS.GetBranchRepositoryData(Encrypt(str & "|" & txtFromDate.Text & "|" & txtToDate.Text & "|" & "1" & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99"))
            Catch ex As Exception
                InsertLocalLog("GetBranchRepositoryData- " & ex.Message)
                MessageBox.Show("خطا هنگام دریافت گزارش" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Else
            Try
                tmpWSResult = CiBS_WS.GetBranchRepositoryData(Encrypt(str & "|" & txtFromDate.Text & "|" & txtToDate.Text & "|" & cbReposotoryStatus.SelectedIndex & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99"))
            Catch ex As Exception
                InsertLocalLog("GetBranchRepositoryData- " & ex.Message)
                MessageBox.Show("خطا هنگام دریافت گزارش" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)

                Exit Sub
            End Try

        End If

        Dim dtt As New DataTable("Cards")
        dtt.Columns.Add("ردیف")
        dtt.Columns.Add("کد شعبه")
        dtt.Columns.Add("کاربر درخواست کننده")
        dtt.Columns.Add("نوع مواد مصرفی")
        dtt.Columns.Add("تعداد درخواستی")
        dtt.Columns.Add("تاریخ درخواست")
        dtt.Columns.Add("تعداد تایید شده")
        dtt.Columns.Add("تاریخ تایید")
        dtt.Columns.Add("کاربر تایید کننده")
        dtt.Columns.Add("تعداد دریافت شده")
        dtt.Columns.Add("تاریخ دریافت")
        dtt.Columns.Add("کاربر دریافت کننده")
        ' dtt.Columns.Add("شماره حساب هدیه ")

        dset.Clear()
        dset.Tables.Clear()
        dset.Tables.Add()

        dset.Tables(0).Columns.Add("ردیف")
        dset.Tables(0).Columns.Add("کد شعبه")
        dset.Tables(0).Columns.Add("کاربر درخواست کننده")
        dset.Tables(0).Columns.Add("نوع مواد مصرفی")
        dset.Tables(0).Columns.Add("تعداد درخواستی")
        dset.Tables(0).Columns.Add("تاریخ درخواست")
        dset.Tables(0).Columns.Add("تعداد تایید شده")
        dset.Tables(0).Columns.Add("تاریخ تایید")
        dset.Tables(0).Columns.Add("کاربر تایید کننده")
        dset.Tables(0).Columns.Add("تعداد دریافت شده")
        dset.Tables(0).Columns.Add("تاریخ دریافت")
        dset.Tables(0).Columns.Add("کاربر دریافت کننده")
        Dim dr1 As DataRow


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
            dr1 = dset.Tables(0).NewRow
            Dim tmp As String()
            ReDim tmp(CardsRecords.Length - 1)
            tmp = CardsRecords(i).Split("|")
            dtt.Rows.Add(New Object() {i + 1, tmp(0), tmp(1).Trim, tmp(2).Trim, tmp(3).Trim, tmp(4).Trim, tmp(5).Trim, tmp(6).Trim, tmp(7).Trim, tmp(8).Trim, tmp(9).Trim, tmp(10).Trim}) ', tmp(11).Trim

            dr1(0) = i + 1
            dr1(1) = tmp(0).Trim
            dr1(2) = tmp(1).Trim
            dr1(3) = tmp(2).Trim
            dr1(4) = tmp(3).Trim
            dr1(5) = tmp(4).Trim
            dr1(6) = tmp(5).Trim
            dr1(7) = tmp(6).Trim
            dr1(8) = tmp(7).Trim
            dr1(9) = tmp(8).Trim
            dr1(10) = tmp(9).Trim
            dr1(11) = tmp(10).Trim
           
            dset.Tables(0).Rows.Add(dr1)
        Next

        dgvCards.DataSource = dtt

    End Sub

    Sub repCardDataRemain()
        Try
            tmpWSResult = CiBS_WS.GetRemainCardData(Encrypt(Now.TimeOfDay.TotalSeconds))
        Catch ex As Exception
            InsertLocalLog("GetRemainCardData- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت گزارش" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try


        Dim dtt As New DataTable("Cards")
        dtt.Columns.Add("نوع کارت")
        dtt.Columns.Add("مانده")

        dset.Clear()
        dset.Tables.Clear()
        dset.Tables.Add()

        dset.Tables(0).Columns.Add("نوع کارت")
        dset.Tables(0).Columns.Add("مانده")

        Dim dr1 As DataRow

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
            dr1 = dset.Tables(0).NewRow
            Dim tmp As String()
            ReDim tmp(CardsRecords.Length - 1)
            tmp = CardsRecords(i).Split("|")
            dtt.Rows.Add(New Object() {tmp(0).Trim, tmp(1).Trim})

            dr1(0) = tmp(0).Trim
            dr1(1) = tmp(1).Trim

            dset.Tables(0).Rows.Add(dr1)
        Next

        dgvCards.DataSource = dtt
    End Sub


    Sub repConflictConsumables()
        Dim str As String = txtFromDate.Text & "|" & txtToDate.Text

        If txtBrCode.Text = "0" Then
            str = str & "|" & "NULL"
        Else
            str = str & "|" & txtBrCode.Text
        End If

        Try
            tmpWSResult = CiBS_WS.GetBranchRepositoryConflictData(Encrypt(str & "|" & Now.TimeOfDay.TotalSeconds))
        Catch ex As Exception
            InsertLocalLog("GetBranchRepositoryConflictData- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت گزارش" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try



        Dim dtt As New DataTable("Cards")
        dtt.Columns.Add("ردیف")
        dtt.Columns.Add("کد شعبه")
        dtt.Columns.Add("کاربر درخواست کننده")
        dtt.Columns.Add("نوع مواد مصرفی")
        dtt.Columns.Add("تعداد درخواستی")
        dtt.Columns.Add("تاریخ درخواست")
        dtt.Columns.Add("تعداد تایید شده")
        dtt.Columns.Add("تاریخ تایید")
        dtt.Columns.Add("کاربر تایید کننده")
        dtt.Columns.Add("تعداد دریافت شده")
        dtt.Columns.Add("تاریخ دریافت")
        dtt.Columns.Add("کاربر دریافت کننده")

        dset.Clear()
        dset.Tables.Clear()
        dset.Tables.Add()

        dset.Tables(0).Columns.Add("ردیف")
        dset.Tables(0).Columns.Add("کد شعبه")
        dset.Tables(0).Columns.Add("کاربر درخواست کننده")
        dset.Tables(0).Columns.Add("نوع مواد مصرفی")
        dset.Tables(0).Columns.Add("تعداد درخواستی")
        dset.Tables(0).Columns.Add("تاریخ درخواست")
        dset.Tables(0).Columns.Add("تعداد تایید شده")
        dset.Tables(0).Columns.Add("تاریخ تایید")
        dset.Tables(0).Columns.Add("کاربر تایید کننده")
        dset.Tables(0).Columns.Add("تعداد دریافت شده")
        dset.Tables(0).Columns.Add("تاریخ دریافت")
        dset.Tables(0).Columns.Add("کاربر دریافت کننده")

        Dim dr1 As DataRow

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
            dr1 = dset.Tables(0).NewRow
            Dim tmp As String()
            ReDim tmp(CardsRecords.Length - 1)
            tmp = CardsRecords(i).Split("|")
            dtt.Rows.Add(New Object() {i + 1, tmp(0), tmp(1).Trim, tmp(2).Trim, tmp(3).Trim, tmp(4).Trim, tmp(5).Trim, tmp(6).Trim, tmp(7).Trim, tmp(8).Trim, tmp(9).Trim, tmp(10).Trim})

            dr1(0) = i + 1
            dr1(1) = tmp(0).Trim
            dr1(2) = tmp(1).Trim
            dr1(3) = tmp(2).Trim
            dr1(4) = tmp(3).Trim
            dr1(5) = tmp(4).Trim
            dr1(6) = tmp(5).Trim
            dr1(7) = tmp(6).Trim
            dr1(8) = tmp(7).Trim
            dr1(9) = tmp(8).Trim
            dr1(10) = tmp(9).Trim
            dr1(11) = tmp(10).Trim

            dset.Tables(0).Rows.Add(dr1)

        Next

        dgvCards.DataSource = dtt

    End Sub

    Sub repEvents()

        Try
            tmpWSResult = CiBS_WS.GetLogData(Encrypt(txtFromDate.Text & "|" & txtToDate.Text & "|" & txtBrCode.Text & "|" & Now.TimeOfDay.TotalSeconds))
        Catch ex As Exception
            InsertLocalLog("GetLogData- " & ex.Message)
            MessageBox.Show("خطا هنگام دریافت گزارش" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim dtt As New DataTable("Cards")
        dtt.Columns.Add("ردیف")
        dtt.Columns.Add("کد شعبه")
        dtt.Columns.Add("کاربر")
        dtt.Columns.Add("آدرس شبکه")
        dtt.Columns.Add("تاریخ")
        dtt.Columns.Add("شماره کارت / عملیات")
        dtt.Columns.Add("شرح")

        dset.Clear()
        dset.Tables.Clear()
        dset.Tables.Add()

        dset.Tables(0).Columns.Add("ردیف")
        dset.Tables(0).Columns.Add("کد شعبه")
        dset.Tables(0).Columns.Add("کاربر")
        dset.Tables(0).Columns.Add("آدرس شبکه")
        dset.Tables(0).Columns.Add("تاریخ")
        dset.Tables(0).Columns.Add("شماره کارت / عملیات")
        dset.Tables(0).Columns.Add("شرح")
        Dim dr1 As DataRow

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
            dr1 = dset.Tables(0).NewRow
            Dim tmp As String()
            ReDim tmp(CardsRecords.Length - 1)
            tmp = CardsRecords(i).Split("|")
            dtt.Rows.Add(New Object() {i + 1, tmp(0).Trim, tmp(1).Trim, tmp(5).Trim, tmp(2).Trim, tmp(3).Trim, tmp(4).Trim})
            
            dr1(0) = i + 1
            dr1(1) = tmp(0).Trim
            dr1(2) = tmp(1).Trim
            dr1(3) = tmp(5).Trim
            dr1(4) = tmp(2).Trim
            dr1(5) = tmp(3).Trim
            dr1(6) = tmp(4).Trim

            dset.Tables(0).Rows.Add(dr1)

        Next
        dgvCards.DataSource = dtt
    End Sub

    Private Sub rbIssuedCards_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbIssuedCards.CheckedChanged
        If rbIssuedCards.Checked = True Then
            cbCardType.Enabled = True
            cbReportType.Enabled = True
            cbReportType.SelectedIndex = 0
            cbCardType.SelectedIndex = 0
        Else
            cbCardType.Enabled = False
            cbReportType.Enabled = False
            cbReportType.SelectedIndex = -1
            cbCardType.SelectedIndex = -1
        End If
    End Sub

    Private Sub rbConsumables_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbConsumables.CheckedChanged
        If rbConsumables.Checked = True Then
            cbConsumables.Enabled = True
            cbConsumables.SelectedIndex = 0
        Else
            cbConsumables.Enabled = False
            cbConsumables.SelectedIndex = 0
        End If
    End Sub

    Private Sub rbAccount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAccount.CheckedChanged
        If rbAccount.Checked = True Then
            txtAccount.Enabled = True
        Else
            txtAccount.Enabled = False
            txtAccount.Text = ""
        End If
    End Sub

    Private Sub dgvCards_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvCards.Sorted
        For i As Integer = 0 To dgvCards.RowCount - 1
            dgvCards.Rows(i).Cells(0).Value = i + 1
        Next
    End Sub

    Private Sub btnExportPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportPDF.Click

        Dim oldCI As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")

        If ((dgvCards.Columns.Count = 0) Or (dgvCards.Rows.Count = 0)) Then
            Exit Sub
        End If

        'Creating dataset to export
        Dim dset As New DataSet
        'add table to dataset
        dset.Tables.Add()
        'add column to that table
        For i As Integer = 0 To dgvCards.ColumnCount - 1
            dset.Tables(0).Columns.Add(dgvCards.Columns(i).HeaderText)
        Next
        'add rows to the table
        Dim dr1 As DataRow
        For i As Integer = 0 To dgvCards.RowCount - 1
            dr1 = dset.Tables(0).NewRow
            For j As Integer = 0 To dgvCards.Columns.Count - 1
                dr1(j) = dgvCards.Rows(i).Cells(j).Value
            Next
            dset.Tables(0).Rows.Add(dr1)
        Next

        Dim excel As New Microsoft.Office.Interop.Excel.ApplicationClass
        Dim wBook As Microsoft.Office.Interop.Excel.Workbook
        Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet
        excel.Visible = False
        wBook = excel.Workbooks.Add()
        wSheet = wBook.ActiveSheet()

        Dim dt As System.Data.DataTable = dset.Tables(0)
        Dim dc As System.Data.DataColumn
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        wSheet.Columns.HorizontalAlignment = 3
        wSheet.Rows.VerticalAlignment = 2
        wSheet.DisplayRightToLeft = True

        For Each dc In dt.Columns
            colIndex = colIndex + 1
            excel.Cells(1, colIndex) = dc.ColumnName
        Next

        For Each dr In dt.Rows
            rowIndex = rowIndex + 1
            colIndex = 0
            For Each dc In dt.Columns
                colIndex = colIndex + 1
                excel.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)

            Next
        Next
        ''''formating
        wSheet.UsedRange.NumberFormat = "####0"
        wSheet.Columns.AutoFit()
        wSheet.UsedRange.Cells.Borders.Weight = 2
        wSheet.PageSetup.CenterHorizontally = True
        wSheet.PageSetup.PrintTitleRows = "$1:$1"
        wSheet.PageSetup.FitToPagesTall = dt.Rows.Count \ 20 + 1
        wSheet.PageSetup.FitToPagesWide = 1
        wSheet.PageSetup.Zoom = False
        wSheet.PageSetup.LeftMargin = 0
        wSheet.PageSetup.RightMargin = 0
        wSheet.PageSetup.PaperSize = 9
        wSheet.PageSetup.Orientation = 2



        Dim strFileName As String = "c:\GSS.xls"
        If System.IO.File.Exists(strFileName) Then
            System.IO.File.Delete(strFileName)
        End If

        wBook.SaveCopyAs(strFileName)

        Dim dp As String = DefaultPrinterName()

        If Not dp.Contains("Adobe PDF") Then
            DefaultPrinterName = "Adobe PDF"
        End If

        Dim psi As New ProcessStartInfo
        psi.UseShellExecute = True
        psi.Verb = "print"
        psi.CreateNoWindow = True
        psi.WindowStyle = ProcessWindowStyle.Hidden
        PrintDialog1.PrinterSettings.PrinterName = "Adobe PDF"
        psi.Arguments = PrintDialog1.PrinterSettings.PrinterName.ToString() ' Here specify printer name
        psi.FileName = "c:\GSS.xls" ' Here specify a document to be printed
        Process.Start(psi)

        wBook.Close(False, strFileName, )
        excel.DisplayAlerts = False

        excel.Quit()
        excel = Nothing

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        If System.IO.File.Exists(strFileName) Then
            System.IO.File.Delete(strFileName)
        End If
        DefaultPrinterName = dp
    End Sub

    Private Sub rbPan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbPan.CheckedChanged
        If rbPan.Checked = True Then '
            txtPAN.Enabled = True
        Else
            txtPAN.Enabled = False
        End If
        txtPAN.Text = ""
    End Sub


    Private Sub btnAllUsers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllUsers.Click

        Try
            tmpWSResult = Decrypt(CiBS_WS.FindUsers(Encrypt(1 & "|" & Now.TimeOfDay.TotalSeconds & "|" & "99")))
        Catch ex As Exception
            MessageBox.Show("خطا هنگام دریافت لیست کاربران ", "خطا")
            Exit Sub
        End Try

        Dim datatable As New DataTable("Users")

        datatable.Columns.Add("ردیف")
        datatable.Columns.Add("کد پرسنلی")
        datatable.Columns.Add("نام")
        datatable.Columns.Add("نام خانوادگی")
        datatable.Columns.Add("کد شعبه")
        datatable.Columns.Add("وضعیت")
        datatable.Columns.Add("سطح دسترسی")
        datatable.Columns.Add("مسدودی")

        Dim tmpAns As String() = tmpWSResult.Split("!")
        For i As Integer = 0 To tmpAns.Length - 2
            Dim tmp As String() = tmpAns(i).Split("|")

            datatable.Rows.Add(New Object() {i + 1, tmp(0).Trim, tmp(1).Trim, tmp(2).Trim, tmp(3).Trim, tmp(4).Trim, tmp(5).Trim, tmp(6).Trim})
        Next
        dgvCards.DataSource = datatable
        ' txtUsersCount.Text = dgvCards.RowCount
    End Sub


    Private Sub btnBranchUsers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBranchUsers.Click
        Try
            tmpWSResult = Decrypt(CiBS_WS.FindBranchUsers(Encrypt(txtBranchCode.Text & "|" & Now.TimeOfDay.TotalSeconds)))
        Catch ex As Exception
            MessageBox.Show("خطا هنگام دریافت لیست کاربران ", "خطا")
            Exit Sub
        End Try

        Dim datatable As New DataTable("Users")

        datatable.Columns.Add("ردیف")
        datatable.Columns.Add("کد پرسنلی")
        datatable.Columns.Add("نام")
        datatable.Columns.Add("نام خانوادگی")
        datatable.Columns.Add("کد شعبه")
        datatable.Columns.Add("وضعیت")
        datatable.Columns.Add("سطح دسترسی")
        datatable.Columns.Add("مسدودی")

        Dim tmpAns As String() = tmpWSResult.Split("!")
        For i As Integer = 0 To tmpAns.Length - 2
            Dim tmp As String() = tmpAns(i).Split("|")

            datatable.Rows.Add(New Object() {i + 1, tmp(0).Trim, tmp(1).Trim, tmp(2).Trim, tmp(3).Trim, tmp(4).Trim, tmp(5).Trim, tmp(6).Trim})
        Next
        dgvCards.DataSource = datatable
    End Sub

    Private Sub btnReportDateTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReportDateTime.Click
        TmpbtnReportDateTimeClick = True
        TmpbtnReportDate = False
        GetReport()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oldCI As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")

        If ((dgvCards.Columns.Count = 0) Or (dgvCards.Rows.Count = 0)) Then
            Exit Sub
        End If

        'Creating dataset to export
        Dim dset As New DataSet
        'add table to dataset
        dset.Tables.Add()
        'add column to that table
        For i As Integer = 0 To dgvCards.ColumnCount - 1
            dset.Tables(0).Columns.Add(dgvCards.Columns(i).HeaderText)
        Next
        'add rows to the table
        Dim dr1 As DataRow
        ' Dim f As String
        For i As Integer = 0 To dgvCards.RowCount - 1
            dr1 = dset.Tables(0).NewRow
            For j As Integer = 0 To dgvCards.Columns.Count - 1
                'Dim m As String = dgvCards.Rows(i).Cells(j).Value
                'If m.Contains("-") Then
                '    Dim n As String() = m.Split("-")
                '    f = 0
                '    For z As Integer = 0 To n.Length - 1
                '        f = f & n(z)
                '    Next
                '    dr1(j) = f
                'Else
                dr1(j) = dgvCards.Rows(i).Cells(j).Value
                'End If
            Next
            dset.Tables(0).Rows.Add(dr1)
        Next

        Dim excel As New Microsoft.Office.Interop.Excel.ApplicationClass
        Dim wBook As Microsoft.Office.Interop.Excel.Workbook
        Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet
        excel.Visible = True
        wBook = excel.Workbooks.Add()
        wSheet = wBook.ActiveSheet()

        Dim dt As System.Data.DataTable = dset.Tables(0)
        Dim dc As System.Data.DataColumn
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0
        wSheet.Range("D:D").NumberFormat = "@"

        wSheet.Columns.HorizontalAlignment = 3
        wSheet.Rows.VerticalAlignment = 2
        wSheet.DisplayRightToLeft = True

        For Each dc In dt.Columns
            colIndex = colIndex + 1
            excel.Cells(1, colIndex) = dc.ColumnName
        Next

        For Each dr In dt.Rows
            rowIndex = rowIndex + 1
            colIndex = 0
            For Each dc In dt.Columns
                colIndex = colIndex + 1
                excel.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
            Next
        Next

        '//////////////////
        wSheet.UsedRange.NumberFormat = "####0"
        wSheet.Range("H:H").NumberFormat = "00000####0"
        wSheet.Range("N:N").NumberFormat = "00########0"

        wSheet.Columns.AutoFit()
        wSheet.UsedRange.Cells.Borders.Weight = 2
        wSheet.PageSetup.CenterHorizontally = True
        wSheet.PageSetup.PrintTitleRows = "$1:$1"
        wSheet.PageSetup.FitToPagesTall = dt.Rows.Count \ 20 + 1
        wSheet.PageSetup.FitToPagesWide = 1
        wSheet.PageSetup.Zoom = False
        wSheet.PageSetup.LeftMargin = 0
        wSheet.PageSetup.RightMargin = 0
        wSheet.PageSetup.PaperSize = 9
        wSheet.PageSetup.Orientation = 2

        Dim strFileName As String = "c:\GSS.xls"
        If System.IO.File.Exists(strFileName) Then
            System.IO.File.Delete(strFileName)
        End If

        wBook.SaveCopyAs(strFileName)

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        If System.IO.File.Exists(strFileName) Then
            System.IO.File.Delete(strFileName)
        End If
    End Sub

End Class