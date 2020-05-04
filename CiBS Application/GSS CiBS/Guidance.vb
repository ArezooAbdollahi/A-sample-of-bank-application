Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic
Imports System
Imports System.Text
Imports System.Net.Sockets
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.Data.OleDb
Imports System.Data
Imports System.Data.SqlClient

Public Class Guidance
    Dim tmpWSResult As String
    Dim ResultError As String = ""
    Dim BankClick, DeviceClick As Boolean
    Dim idBank As Integer
    'Dim conn As String = "Data Source=localhost;Initial Catalog=TAT_BANK_CiBS1;Persist Security Info=True;User ID=sa;Password=sasqlsa"
    'Dim conn As String = My.Settings.DB_ConnectionString

    Dim tmpSave As String()
    Dim it As ComboItem

    Public Structure ComboItem
        Public ErrorID As Integer
        Public ErrorMess As String

        Public Overrides Function ToString() As String
            Return ErrorMess
        End Function
    End Structure

    'Function SearchError(ByVal IDError As String) As String
    '    Dim sqlCnt As SqlConnection = New SqlConnection(Conn)
    '    Dim sqlCmd As New SqlCommand
    '    Dim sqlDR As SqlDataReader
    '    Dim IDError1, Bank1, Device1 As New SqlParameter

    '    sqlCmd.Parameters.Clear()
    '    sqlCmd.Connection = sqlCnt
    '    sqlCmd.CommandType = Data.CommandType.StoredProcedure
    '    sqlCmd.CommandText = "spSearchError"

    '    IDError1.Value = IDError
    '    IDError1.ParameterName = "@ErrorID"
    '    sqlCmd.Parameters.Add(IDError1)

    '    sqlCmd.CommandText = "spSearchError"
    '    sqlCmd.Connection = sqlCnt
    '    Try
    '        sqlCnt.Open()
    '        sqlDR = sqlCmd.ExecuteReader
    '        If sqlDR.HasRows = False Then
    '            ResultError = "Invalid"
    '        End If
    '        While sqlDR.Read
    '            ResultError = ResultError & "|" & sqlDR("Probability") & "|" & sqlDR("Solution")
    '            ResultError = ResultError & "!"
    '        End While

    '    Catch ex As SqlException
    '        ResultError = "Data Base Error" & "|" & ex.Message
    '    Catch ex As Exception
    '        ResultError = "Unexpected Error" & "|" & ex.Message
    '    End Try
    '    sqlCnt.Close()
    '    Return ResultError
    'End Function

    'Function SearchErrorMsg(ByVal ErrorMsg As String) As String
    '    Dim sqlCnt As SqlConnection = New SqlConnection(Conn)
    '    Dim sqlCmd As New SqlCommand
    '    Dim sqlDR As SqlDataReader
    '    Dim ErrorMsg1 As New SqlParameter
    '    ResultError = ""
    '    sqlCmd.Parameters.Clear()
    '    sqlCmd.Connection = sqlCnt
    '    sqlCmd.CommandType = Data.CommandType.StoredProcedure
    '    ' sqlCmd.CommandText = "spSearchError"

    '    ErrorMsg1.Value = ErrorMsg
    '    ErrorMsg1.ParameterName = "@ErrorMsg"
    '    sqlCmd.Parameters.Add(ErrorMsg1)

    '    sqlCmd.CommandText = "spSearchErrorMsg"
    '    sqlCmd.Connection = sqlCnt
    '    Try
    '        sqlCnt.Open()
    '        sqlDR = sqlCmd.ExecuteReader
    '        If sqlDR.HasRows = False Then
    '            ResultError = "Invalid"
    '        End If
    '        While sqlDR.Read
    '            ResultError = ResultError & sqlDR("ErrorMess") & "|" & sqlDR("Probability") & "|" & sqlDR("Solution") & "|" & sqlDR("Type") & "|" & sqlDR("ErrorID")
    '            ResultError = ResultError & "!"
    '        End While

    '    Catch ex As SqlException
    '        ResultError = "Data Base Error" & "|" & ex.Message
    '    Catch ex As Exception
    '        ResultError = "Unexpected Error" & "|" & ex.Message
    '    End Try
    '    sqlCnt.Close()
    '    Return ResultError
    'End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim txtErrorMsg As String = cbError.Text
        Dim cmbErrorMsg As String
        Dim itError As ComboItem = cbError.SelectedItem
        Dim idError As Integer = itError.ErrorID

        If cbError.SelectedIndex > -1 Then
            dgvError.Enabled = False
            dgvError.Visible = False
            lbSolution.Enabled = True
            lbSolution.Visible = True
            lbProbability.Enabled = True
            lbProbability.Visible = True
            txProbability.Enabled = True
            txProbability.Visible = True
            txtSolution.Enabled = True
            txtSolution.Visible = True
            Me.Height = 330
            clearFields()
        ElseIf cbError.Text <> "" Then
            dgvError.Enabled = True
            dgvError.Visible = True
            Me.Height = 485
            clearFields()
        End If

        If cbError.SelectedIndex > -1 Then
            Try
                ResultError = (Decrypt(CiBS_WS.SearchError(Encrypt(idError))))
            Catch ex As Exception
                InsertLocalLog("CheckUser- " & ex.Message)
                MessageBox.Show("خطا هنگام بررسی هویت کاربر" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            ' Dim tmpAns As String() = tmpWSResult.Split("!")

            FillTextError()
        ElseIf cbError.Text <> "" Then
            txtErrorMsg = Replace(txtErrorMsg, " ", "")

            Try
                ResultError = (Decrypt(CiBS_WS.SearchErrorMsg(Encrypt(txtErrorMsg))))
            Catch ex As Exception
                InsertLocalLog("CheckUser- " & ex.Message)
                MessageBox.Show("خطا هنگام بررسی هویت کاربر" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            FillDgvError()
        Else
            MessageBox.Show("لطفا نوع خطا را انتخاب نمایید", "خطا")
            Exit Sub
        End If 
    End Sub
    Sub clearFields()
        txProbability.Text = " "
        txtSolution.Text = " "
        ' dgvError.Rows.Clear()

        If dgvError.DataSource IsNot Nothing Then
            dgvError.DataSource = Nothing
        Else
            dgvError.Rows.Clear()
        End If
    End Sub
    Sub FillTextError()
        Dim tmp As String()

        Dim tmpAns As String() = ResultError.Split("!")
        Dim CardsRecords(tmpAns.Length - 1) As String
        For i As Integer = 0 To tmpAns.Length - 2
            tmp = tmpAns(i).Split("|")
        Next

        txProbability.Text = tmp(1)
        txtSolution.Text = tmp(2)

    End Sub

    Sub FillDgvError()
        Dim datatable As New DataTable("Error")
        datatable.Columns.Add("ردیف")
        datatable.Columns.Add("خطا")
        datatable.Columns.Add("احتمالات")
        datatable.Columns.Add("راه حل")

        Dim tmpAns As String() = ResultError.Split("!")
        Dim CardsRecords(tmpAns.Length - 1) As String
        For i As Integer = 0 To tmpAns.Length - 2
            Dim tmp As String() = tmpAns(i).Split("|")
            CardsRecords(i) = tmp(0) & "|" & tmp(1) & "|" & tmp(2)
        Next
        For i As Integer = 0 To CardsRecords.Length - 2
            Dim tmp As String()
            Dim tmpReq As String = ""
            Dim strA, strB As String
            Dim tmpFinishSerial As String = Nothing
            Dim tmpMyPrintCount As Integer = Nothing
            ReDim tmp(CardsRecords.Length - 1)
            tmp = CardsRecords(i).Split("|")
            datatable.Rows.Add(New Object() {i + 1, tmp(0).Trim, tmp(1).Trim, tmp(2).Trim})
        Next

        dgvError.DataSource = datatable
        dgvError.Columns(0).Width = 40
        dgvError.Columns(1).Width = 300
        dgvError.Columns(2).Width = 150
        dgvError.Columns(3).Width = 400
    End Sub

    Sub FillcbError()
        '**************
        'Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        'Dim sqlCmd As New SqlCommand
        'Dim sqlDR As SqlDataReader
        'Dim ResultErrormsg As String = ""

        'sqlCmd.Parameters.Clear()
        'sqlCmd.Connection = sqlCnt
        'sqlCmd.CommandType = Data.CommandType.StoredProcedure
        'sqlCmd.CommandText = "SpGetErrorMsg"
        'sqlCmd.Connection = sqlCnt
        'Try
        '    sqlCnt.Open()
        '    sqlDR = sqlCmd.ExecuteReader
        '    If sqlDR.HasRows = False Then
        '        ResultErrormsg = "Invalid"
        '    End If
        '    While sqlDR.Read
        '        ResultErrormsg = ResultErrormsg & sqlDR("ErrorMess") & "|" & sqlDR("ErrorID")
        '        ResultErrormsg = ResultErrormsg & "!"
        '    End While

        'Catch ex As SqlException
        '    ResultErrormsg = "Data Base Error" & "|" & ex.Message
        'Catch ex As Exception
        '    ResultErrormsg = "Unexpected Error" & "|" & ex.Message
        'End Try
        'sqlCnt.Close()
        '*********

        Try
            tmpWSResult = (Decrypt(CiBS_WS.GetAllErrorMsg()))
            'tmpWSResult = CiBS_WS.GetAllErrorMsg()
        Catch ex As Exception
            InsertLocalLog("CheckUser- " & ex.Message)
            MessageBox.Show("خطا هنگام بررسی هویت کاربر" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        ' Dim tmp As String = Decrypt(tmpWSResult)
        Dim tmpAns As String() = tmpWSResult.Split("!")
       
        For i As Integer = 0 To tmpAns.Length - 2
            ReDim tmpSave(tmpAns.Length - 1)
            tmpSave = tmpAns(i).Split("|")
            Dim C As ComboItem
            C.ErrorID = tmpSave(1)
            C.ErrorMess = LTrim(RTrim(tmpSave(0)))
            cbError.Items.Add(C)
        Next

    End Sub

    Private Sub cbError_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbError.SelectedIndexChanged
        If cbError.SelectedIndex > -1 Then
            clearFields()
            
            it = cbError.SelectedItem
            Dim x As Integer = it.ErrorID
            '************************************************************
            Dim cmbErrorMsg, txtErrorMsg As String
            Dim itError As ComboItem = cbError.SelectedItem
            Dim idError As Integer = itError.ErrorID

            If cbError.SelectedIndex > -1 Then
                'SearchError(idError)
                Try
                    ResultError = (Decrypt(CiBS_WS.SearchError(Encrypt(idError))))
                Catch ex As Exception
                    InsertLocalLog("CheckUser- " & ex.Message)
                    MessageBox.Show("خطا هنگام بررسی هویت کاربر" & vbCrLf & ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                FillTextError()
            End If
            '************************************************************
            dgvError.Enabled = False
            dgvError.Visible = False
            lbSolution.Enabled = True
            lbSolution.Visible = True
            lbProbability.Enabled = True
            lbProbability.Visible = True
            txProbability.Enabled = True
            txProbability.Visible = True
            txtSolution.Enabled = True
            txtSolution.Visible = True
            Me.Height = 330
        End If
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        FillcbError()
    End Sub

    Public Function AddItems0(ByRef cmb) As String
        Dim tmpAns As String() = tmpWSResult.Split("!")
        If String.Compare(tmpAns(0), "No Data") = 0 Then
            MessageBox.Show("متنی یافت نشد", "خطا")
            Exit Function
        End If
        Dim CardsRecords(tmpAns.Length - 2) As String
        For i As Integer = 0 To tmpAns.Length - 2
            CardsRecords(i) = tmpAns(i)
        Next

        For i As Integer = 0 To CardsRecords.Length - 1
            ReDim tmpSave(CardsRecords.Length - 1)
            tmpSave = CardsRecords(i).Split("|")

            Dim C As ComboItem
            C.ErrorID = tmpSave(1)
            C.ErrorMess = LTrim(RTrim(tmpSave(0)))
            cmb.Items.Add(C)
        Next
    End Function

    Private Sub cbBank_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Dim s As String = Encoding.GetEncoding(1256).GetBytes((Encoding.Convert(Encoding.Default, Encoding.GetEncoding(1256), Encoding.Default.GetString("علی"))))
        'If cbBank.SelectedIndex > -1 Then
        '    cbDevice.Items.Clear()
        '    Dim it As ComboItem = cbBank.SelectedItem
        '    idBank = it.ErrorID
        '    BankClick = True
        '    DeviceClick = False
        '    cbDevice.Enabled = True
        '    cbDevice.Items.Clear()
        '    ' tmpWSResult = InsertData.FindItems(idBank, "spFindDeviceFrBank")
        '    ' ///// Just Now
        '    AddItems(cbDevice)
        'End If
    End Sub

    Public Function AddItems(ByRef cmb) As String
        Dim tmpAns As String() = tmpWSResult.Split("!")
        Dim tmpAns1 As String
        If String.Compare(tmpAns(0), "No Data") = 0 Then
            MessageBox.Show("متنی یافت نشد", "خطا")
            Exit Function
        End If
        ' Dim CardsRecords(tmpAns.Length - 2) As String.
        Dim CardsRecord As String()

        For i As Integer = 0 To tmpAns.Length - 2
            CardsRecord = tmpAns(i).Split("|")
            If BankClick = True Then
                ' tmpAns1 = InsertData.FindDeviceName(CardsRecord(1))
                ' ///// Just Now
            End If

            Dim C As ComboItem
            C.ErrorID = CardsRecord(1)
            C.ErrorMess = LTrim(RTrim(CardsRecord(0)))
            cmb.Items.Add(C)
        Next


        ''For i As Integer = 0 To tmpAns.Length - 2
        ''    CardsRecords(i) = tmpAns(i)
        ''Next

        ''For i As Integer = 0 To CardsRecords.Length - 1
        ''    ReDim tmpSave(CardsRecords.Length - 1)
        ''    tmpSave = CardsRecords(i).Split("|")



        ''    Dim C As ComboItem
        ''    C.id = tmpSave(1)
        ''    C.title = LTrim(RTrim(tmpSave(0)))
        ''    cmb.Items.Add(C)
        ''Next
    End Function

    Private Sub cbDevice_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If cbDevice.SelectedIndex > -1 Then
        '    cbForm.Items.Clear()
        '    Dim it As ComboItem = cbDevice.SelectedItem
        '    Dim idDevice As Integer = it.ErrorID
        '    DeviceClick = True
        '    BankClick = False
        '    cbForm.Enabled = True
        '    cbForm.Items.Clear()
        '    ' tmpWSResult = InsertData.FindFormFrDevice2(idBank, idDevice)
        '    ' ///// Just Now
        '    AddItems(cbForm)
        'End If
    End Sub

    Private Sub cbForm_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub btnFilteringError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'cbDevice.Items.Clear()
        'cbError.Text = ""
        'clearFields()

        'txProbability.Text = " "
        'txtSolution.Text = " "
        'Dim idForm As Integer
        'Dim it As ComboItem = cbBank.SelectedItem
        'Dim idBank As Integer = it.ErrorID

        'Dim itDevice As ComboItem = cbDevice.SelectedItem
        'Dim idDevice As Integer = itDevice.ErrorID

        'If cbForm.SelectedIndex > -1 Then
        '    Dim itForm As ComboItem = cbForm.SelectedItem
        '    idForm = itForm.ErrorID
        'Else
        '    idForm = 0
        'End If
        'cbError.SelectedIndex = -1
        'cbError.Items.Clear()
        'FillcbErrorFiltering(idDevice, idForm)
    End Sub

    Sub FillcbErrorFiltering(ByVal idDevice As Integer, ByVal idForm As Integer)
        'Dim sqlCnt As SqlConnection = New SqlConnection(Conn)
        'Dim sqlCmd As New SqlCommand
        'Dim sqlDR As SqlDataReader
        'Dim ResultErrormsg As String = ""
        'Dim idBank0, idDevice0, idForm0 As New SqlParameter

        'sqlCmd.Parameters.Clear()
        'sqlCmd.Connection = sqlCnt
        'sqlCmd.CommandType = Data.CommandType.StoredProcedure

        'idDevice0.Value = idDevice
        'idDevice0.ParameterName = "@idDevice"
        'sqlCmd.Parameters.Add(idDevice0)

        'If idForm > 0 Then
        '    idForm0.Value = idForm
        '    idForm0.ParameterName = "@idForm"
        '    sqlCmd.Parameters.Add(idForm0)
        '    sqlCmd.CommandText = "spGetErrorMsgFiltering"
        'Else
        '    sqlCmd.CommandText = "spGetErrorMsgFilteringHardware"
        'End If
        'sqlCmd.Connection = sqlCnt
        'Try
        '    sqlCnt.Open()
        '    sqlDR = sqlCmd.ExecuteReader
        '    If sqlDR.HasRows = False Then
        '        ResultErrormsg = "Invalid"
        '    End If

        '    While sqlDR.Read
        '        ResultErrormsg = ResultErrormsg & sqlDR("ErrorMess") & "|" & sqlDR("ErrorID")
        '        ResultErrormsg = ResultErrormsg & "!"
        '    End While

        'Catch ex As SqlException
        '    ResultErrormsg = "Data Base Error" & "|" & ex.Message
        'Catch ex As Exception
        '    ResultErrormsg = "Unexpected Error" & "|" & ex.Message
        'End Try
        'sqlCnt.Close()



        'Dim tmpAns As String() = ResultErrormsg.Split("!")
        ''Dim CardsRecords(tmpAns.Length - 1) As String
        ''For i As Integer = 0 To tmpAns.Length - 2
        ''    Dim tmp As String() = tmpAns(i).Split("|")
        ''    CardsRecords(i) = tmp(0) & "|" & tmp(1)
        ''    ' cbError.Items.Add(tmp(i))
        ''Next

        'For i As Integer = 0 To tmpAns.Length - 2
        '    ReDim tmpSave(tmpAns.Length - 1)
        '    tmpSave = tmpAns(i).Split("|")
        '    Dim C As ComboItem
        '    C.ErrorID = tmpSave(1)
        '    C.ErrorMess = LTrim(RTrim(tmpSave(0)))
        '    cbError.Items.Add(C)
        'Next


        ''For i As Integer = 0 To CardsRecords.Length - 2
        ''    Dim tmp As String()
        ''    Dim tmpReq As String = ""
        ''    Dim strA, strB As String
        ''    Dim tmpFinishSerial As String = Nothing
        ''    Dim tmpMyPrintCount As Integer = Nothing
        ''    ReDim tmp(CardsRecords.Length - 1)
        ''    tmp = CardsRecords(i).Split("|")
        ''    DataTable.Rows.Add(New Object() {tmp(0).Trim, tmp(1).Trim, tmp(2).Trim})
        ''Next


    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'cbBank.SelectedIndex = -1
        'cbDevice.SelectedIndex = -1
        'cbForm.SelectedIndex = -1
        'cbError.SelectedIndex = -1

        'cbBank.Text = ""
        'cbDevice.Text = ""
        'cbForm.Text = ""
        'cbError.Text = ""

        'cbDevice.Items.Clear()
        'cbForm.Items.Clear()
        'cbError.Items.Clear()

        'clearFields()

        'txProbability.Text = " "
        'txtSolution.Text = " "
        'FillcbError()
    End Sub

    Private Sub cbForm_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim idForm As Integer
        'Dim it As ComboItem = cbBank.SelectedItem
        'Dim idBank As Integer = it.ErrorID

        'Dim itDevice As ComboItem = cbDevice.SelectedItem
        'Dim idDevice As Integer = itDevice.ErrorID

        'If cbForm.SelectedIndex > -1 Then
        '    Dim itForm As ComboItem = cbForm.SelectedItem
        '    idForm = itForm.ErrorID
        'Else
        '    idForm = 0
        'End If
        'cbError.SelectedIndex = -1
        'cbError.Items.Clear()
        'FillcbErrorFiltering(idDevice, idForm)
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub dgvError_Click(sender As System.Object, e As System.EventArgs) Handles dgvError.Click
        FindItems()

    End Sub
    Sub FindItems()
        'Error11 = dgvError.CurrentRow.Cells(1).Value.ToString.Trim
        txProbability.Text = dgvError.CurrentRow.Cells(2).Value.ToString.Trim
        txtSolution.Text = dgvError.CurrentRow.Cells(3).Value.ToString.Trim
    End Sub

End Class
