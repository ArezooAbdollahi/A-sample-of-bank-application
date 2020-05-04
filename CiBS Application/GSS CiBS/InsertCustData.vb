'Imports System.Net.Mime.MediaTypeNames
'Imports System.Runtime.InteropServices
Imports CheckDigit.CheckDigitAlgorithm

Public Class InsertCustData

    Private Sub InsertCustData_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnInsertCustData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsertCustData.Click
        
        'If txtBirthDate.Text.Length <> 10 Then
        '    MessageBox.Show("تاریخ تولد را به درستی وارد نمایید", "خطا")
        '    Exit Sub
        'End If
        Dim tmpBirthDate As String() = txtBirthDate.Text.Split("/")
        If tmpBirthDate.Length <> 3 Then
            MessageBox.Show("روز و ماه و سال را با ' / ' از هم جدا نمایید ", "خطا")
            Exit Sub
        End If
        If tmpBirthDate(0).Length <> 4 Then
            MessageBox.Show("سال تولد می بایست 4 رقمی باشد", "خطا")
            Exit Sub
        End If
        If tmpBirthDate(1).Length <> 2 Then
            MessageBox.Show("ماه تولد می بایست 2 رقمی باشد", "خطا")
            Exit Sub
        End If
        If tmpBirthDate(2).Length <> 2 Then
            MessageBox.Show("روز تولد می بایست 2 رقمی باشد", "خطا")
            Exit Sub
        End If
       

        If txtAddress.Text.Trim = "" Or txtBirthDate.Text.Trim = "" Or txtFatherName.Text.Trim = "" Or txtTell.Text.Trim = "" Then
            MessageBox.Show("اطلاعات مربوطه را به درستی وارد نمایید", "خطا")
            Exit Sub
        End If

        'If txtBirthDate.Text.Length <> 10 Then
        '    MessageBox.Show("تاریخ تولد را به درستی وارد نمایید", "خطا")
        '    Exit Sub
        'End If

        Try
            ' MessageBox.Show("1")
            tmpWSResult = CiBS_WS.InsertCustomerData(Encrypt(CardPrint.txtIntCode.Text.Trim & "|" & txtBirthDate.Text.Trim & "|" & txtFatherName.Text.Trim & "|" & txtTell.Text.Trim & "|" & txtAddress.Text.Trim & "|" & "99"))
        Catch ex As Exception
            MessageBox.Show("خطا هنگام ذخیره سازی, لطفا دوباره سعی نمایید", "خطا")
            Exit Sub
        End Try
        tmpAns = Decrypt(tmpWSResult)
        '  MessageBox.Show("2 -" & tmpAns)
        If tmpAns.Contains("Cannot insert duplicate key") Then
            CardPrint.CusNumber = True
            MessageBox.Show("اطلاعات این مشتری قبلا ثبت شده است")
        Else
            If tmpAns.Contains("Error") Then
                CardPrint.CusNumber = False
            ElseIf tmpAns = "1" Then
                CardPrint.CusNumber = True
            End If
            MessageBox.Show("اطلاعات مشتری با موفقیت اضافه شد", "اعلان")
        End If
        Me.Close()
    End Sub

    Private Sub txtBirthDate_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtBirthDate.MouseClick
        'Dim tmpDate As New DatePicker

        'Dim MPx As Point = MousePosition()
        ''If CInt(Me.Width) - CInt(MPx.X) < 359 Then
        ''    MPx = New Point(CInt(Me.Width) - 326, MPx.Y)
        ''End If

        'MPx = New Point(MPx.X, MPx.Y)
        'tmpDate.Location = MPx
        'tmpDate.ShowDialog()
        'txtBirthDate.Text = tmpDate.Data
    End Sub

    Private Sub txtFatherName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFatherName.Leave
        Dim Numeric As Boolean = False
        If txtFatherName.Text.Length = 0 Then
            Exit Sub
        End If
        Dim tmpCheck As Char() = txtFatherName.Text.ToCharArray
        For i As Integer = 0 To tmpCheck.Length - 1
            If IsNumeric(tmpCheck(i)) Then
                'MessageBox.Show("نام وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
                Numeric = True
                txtFatherName.Focus()
                txtFatherName.SelectAll()
            End If
        Next
        If Numeric = True Then
            MessageBox.Show("نام وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
        End If
        If txtFatherName.Text.Length < 2 Then
            'MessageBox.Show("نام وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            txtFatherName.Focus()
            txtFatherName.SelectAll()
        End If
    End Sub

    Private Sub txtTell_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTell.Leave
        If txtTell.Text.Length > 0 Then
            'If Not CheckDigit.CheckDigitAlgorithm.ValidateCheckDigit(txtTell.Text) Then
            '    MessageBox.Show("شماره تلفن وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            '    txtTell.Focus()
            '    txtTell.SelectAll()
            '    Exit Sub
            'End If
            If Not IsNumeric(txtTell.Text) Then
                MessageBox.Show("شماره تلفن فقط میتواند مقادیر عددی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
                txtTell.Focus()
                txtTell.SelectAll()
                Exit Sub
            End If
            If CLng(txtTell.Text) = 0 Then
                MessageBox.Show("شماره تلفن وارد شده معتبر نمی باشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
                txtTell.Focus()
                txtTell.SelectAll()
                Exit Sub
            End If
        End If
    End Sub

    Private Sub txtAddress_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAddress.Leave
        If txtAddress.Text.Length < 2 Then
            MessageBox.Show("آدرس وارد شده را بدرستی وارد نمایید ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            txtAddress.Focus()
            txtAddress.SelectAll()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpDate As New DatePicker

        Dim MPx As Point = MousePosition()
        'If CInt(Me.Width) - CInt(MPx.X) < 359 Then
        '    MPx = New Point(CInt(Me.Width) - 326, MPx.Y)
        'End If

        MPx = New Point(MPx.X, MPx.Y)
        tmpDate.Location = MPx
        tmpDate.ShowDialog()
        txtBirthDate.Text = tmpDate.Data
    End Sub

    Private Sub btnDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDate.Click
        Dim tmpDate As New DatePicker

        Dim MPx As Point = MousePosition()
        'If CInt(Me.Width) - CInt(MPx.X) < 359 Then
        '    MPx = New Point(CInt(Me.Width) - 326, MPx.Y)
        'End If

        MPx = New Point(MPx.X, MPx.Y)
        tmpDate.Location = MPx
        tmpDate.ShowDialog()
        txtBirthDate.Text = tmpDate.Data
    End Sub

    Private Sub txtBirthDate_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBirthDate.Leave
        Dim NotNumeric As Boolean = False
        If txtBirthDate.Text.Length = 0 Then
            Exit Sub
        End If
        Dim tmpCheck As Char() = txtBirthDate.Text.ToCharArray
        For i As Integer = 0 To tmpCheck.Length - 1
            If (IsNumeric(tmpCheck(i)) Or tmpCheck(i) = "/") Then

            Else
                NotNumeric = True
                txtBirthDate.Focus()
                txtBirthDate.SelectAll()
            End If
        Next
        If NotNumeric = True Then
            MessageBox.Show("تاریخ تولد وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
        End If
        If txtBirthDate.Text.Length < 2 Then
            'MessageBox.Show("نام وارد شده معتبر نمیباشد ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            txtBirthDate.Focus()
            txtBirthDate.SelectAll()
        End If
    End Sub
End Class