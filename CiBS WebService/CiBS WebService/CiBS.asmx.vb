Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.IO
Imports System.Data.OleDb
Imports System.Text
Imports System.Data
Imports System.DirectoryServices
Imports System.Globalization


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://evolis.me/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class CiBS_WebService

    Inherits System.Web.Services.WebService

    'Dim conn As String = "Data Source=172.30.1.90;Initial Catalog=TAT_BANK_CiBS;Persist Security Info=True;User ID=TestApp;Password=P@ssw0rd2018!"

    Dim cc As String = "Data Source=Biha.ab.net;Initial Catalog=TAT_DWBI_ODS;Persist Security Info=True;User ID=gss;Password=qwert12345678"
    'Dim conn As String = "Data Source=localhost;Initial Catalog=TAT_BANK_CiBSNEW;Persist Security Info=True;User ID=sa;Password=sasqlsa"
    Dim conn As String = My.Settings.DB_ConnectionString

    Dim tmp As String()
    Dim tmpByte As Byte() = Encoding.Unicode.GetBytes(200)
    Public DTP As New PersianToolS.PersinToolsClass

    Public Function GenerateHash(ByVal SourceText As String) As String
        Dim Ue As New UnicodeEncoding()
        Dim ByteSourceText() As Byte = Ue.GetBytes(SourceText)
        Dim Md5 As New MD5CryptoServiceProvider()
        Dim ByteHash() As Byte = Md5.ComputeHash(ByteSourceText)
        Return Convert.ToBase64String(ByteHash)
    End Function
  Public Function MakeEncKey()
        Dim tmp = Now.TimeOfDay.ToString.Substring(0, 8).Replace(":", "").ToCharArray
        Array.Reverse(tmp)
        Return tmp
    End Function

    Public Function Encrypt(ByVal source As String) As String
        Dim tmpKey As String = MakeEncKey()
        Dim tmp As String = Cryptography.CryptographyClass.Encrypt(source, tmpByte, tmpKey)
        Dim tmpArray As Char() = tmpKey.ToCharArray
        Return tmp.Substring(0, 7) & tmpArray(0) & tmpArray(1) & tmp.Substring(7, 10) & tmpArray(2) & tmpArray(3) & tmp.Substring(17, 2) & tmpArray(4) & tmpArray(5) & tmp.Substring(19, tmp.Length - 19)
    End Function

    Public Function Decrypt(ByVal source As String) As String
        Dim tmpkey As String = source.Substring(7, 2) & source.Substring(19, 2) & source.Substring(23, 2)
        source = source.Substring(0, 7) & source.Substring(9, 10) & source.Substring(21, 2) & source.Substring(25, source.Length - 25)
        Dim tmp As String = Cryptography.CryptographyClass.Decrypt(source, tmpByte, tmpkey)
        Return tmp
    End Function


    ''<WebMethod()> _
    ''Public Function test() As String

    ''    Dim sqlCnt As SqlConnection = New SqlConnection(conn)
    ''    Dim sqlCmd As New SqlCommand
    ''    Dim sqldr As SqlDataReader
    ''    sqlCmd.Parameters.Clear()
    ''    sqlCmd.Connection = sqlCnt
    ''    sqlCmd.CommandText = "select top 1 * from users"


    ''    Dim Result As String = ""
    ''    sqlCmd.Connection = sqlCnt
    ''    Try
    ''        sqlCnt.Open()
    ''        sqlDR = sqlCmd.ExecuteReader
    ''        If sqlDR.HasRows = False Then
    ''            Result = "Invalid"
    ''        End If
    ''        While sqlDR.Read
    ''            If CStr(sqlDR("IsValid")) = "Blocked User" Then
    ''                Result = "9999"
    ''            ElseIf CStr(sqlDR("IsValid")) = "Loged In User" Then
    ''                Result = "8888"
    ''            Else
    ''                Result = sqldr("IsValid") & "|" & sqldr("GrantLevel") & "|" & sqldr("name").ToString.Trim & " " & sqldr("family").ToString.Trim
    ''            End If

    ''        End While

    ''    Catch ex As SqlException
    ''        Result = "Data Base Error" & "|" & ex.Message
    ''        InsertLocalLog("CheckUser - " & "BranchCode:" & tmp(2) & " - UserName:" & tmp(0) & " - Error:" & ex.Message)
    ''    Catch ex As Exception
    ''        Result = "Unexpected Error" & "|" & ex.Message
    ''        InsertLocalLog("CheckUser - " & "BranchCode:" & tmp(2) & " - UserName:" & tmp(0) & " - Error:" & ex.Message)
    ''    End Try
    ''    sqlCnt.Close()
    ''    sqlDR.Close()
    ''    Return Result
    ''End Function


    <WebMethod()> _
    Public Function CheckUser(ByVal param As String) As String
        Dim DiffrentTime As String
        Dim TempDifTime As String = ""
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        If Now.TimeOfDay.TotalSeconds - tmp(3) >= 3 Then
            DiffrentTime = "Timeout-Diffrent"
        End If

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader
        Dim UserName, BrCode, Password As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        sqlCmd.CommandText = "spLoginNEW2"
        UserName.Value = tmp(0)
        UserName.ParameterName = "@UserName"
        sqlCmd.Parameters.Add(UserName)
        BrCode.Value = tmp(2)
        BrCode.ParameterName = "@brcode"
        sqlCmd.Parameters.Add(BrCode)
        Password.Value = tmp(1)
        Password.ParameterName = "@password"
        sqlCmd.Parameters.Add(Password)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 5 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spLoginNEW2Ayandeh"
        Else
            sqlCmd.CommandText = "spLoginNEW2"
        End If

        Dim Result As String = ""

        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = "Invalid"
            End If
            While sqlDR.Read
                If CStr(sqlDR("IsValid")) = "Blocked User" Then
                    Result = "9999"
                ElseIf CStr(sqlDR("IsValid")) = "Loged In User" Then
                    Result = "8888"
                Else
                    If DiffrentTime = "Timeout-Diffrent" Then
                        Result = sqlDR("IsValid") & "|" & sqlDR("GrantLevel") & "|" & sqlDR("AdminMessage").ToString.Trim & "|" & sqlDR("name").ToString.Trim & " " & sqlDR("family").ToString.Trim & "|" & sqlDR("PosID").ToString.Trim & "|" & DiffrentTime & "|" & Now.TimeOfDay.ToString.Substring(0, 8)
                    Else
                        Result = sqlDR("IsValid") & "|" & sqlDR("GrantLevel") & "|" & sqlDR("AdminMessage").ToString.Trim & "|" & sqlDR("name").ToString.Trim & " " & sqlDR("family").ToString.Trim & "|" & sqlDR("PosID").ToString.Trim & "|" & TempDifTime & "|" & TempDifTime
                    End If
                End If

            End While

        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("CheckUser - " & "BranchCode:" & tmp(2) & " - UserName:" & tmp(0) & " - Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("CheckUser - " & "BranchCode:" & tmp(2) & " - UserName:" & tmp(0) & " - Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        sqlDR.Close()
        Return Encrypt(Result)
    End Function


    <WebMethod()> _
    Public Function ValidateActiveDirectoryLoginWeb(ByVal param As String) As String
        'InsertLocalLog("1ValidateActiveDirectoryLoginWeb ")
        Try
            tmp = param.Split("|")
        Catch ex As Exception
            Return ("InvalidStart")
        End Try

        Dim Result As String
        Dim domain As String
        Dim username, password As String

        domain = tmp(0)
        username = tmp(1)
        password = tmp(2)


        'username = "m.jalali"
        'password = "9125736722*majid"
        'domain = "ab"

        Result = "True"

        Try
            'InsertLocalLog("2ValidateActiveDirectoryLoginWeb")
            If Not ValidateActiveDirectoryLogin(domain, username, password) Then ' Me.txtUserName.Text, Me.txtPassword.Text) Then
                Result = "Invalid"
                InsertLocalLog("ValidateActiveDirectoryLoginWeb - " & " - Error:Invalid")
            End If
        Catch ex As Exception
            Result = "Error:" & ex.Message
            InsertLocalLog("ValidateActiveDirectoryLoginWeb - " & " - Error:" & ex.Message)
        End Try

        'InsertLocalLog("4ValidateActiveDirectoryLoginWeb")
        Return (Result)

    End Function

    Public Function ValidateActiveDirectoryLogin(ByVal Domain As String, ByVal Username As String, ByVal Password As String) As Boolean

        Dim Success As Boolean = False


        Dim Entry As New System.DirectoryServices.DirectoryEntry("LDAP://" & Domain, Username, Password)

        Dim Searcher As New System.DirectoryServices.DirectorySearcher(Entry)

        Searcher.SearchScope = DirectoryServices.SearchScope.OneLevel

        Try

            Dim Results As System.DirectoryServices.SearchResult = Searcher.FindOne
            Success = Not (Results Is Nothing)

        Catch

            Success = False

        End Try

        ' InsertLocalLog("3ValidateActiveDirectoryLoginWeb -Success " & Success)
        Return Success

    End Function


    <WebMethod()> _
    Public Function UserChangePassword(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(3) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If
        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand

        Dim UserName, OldPass, NewPass, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        sqlCmd.CommandText = "spChangePassword"
        UserName.Value = tmp(0)
        UserName.ParameterName = "@UserName"
        sqlCmd.Parameters.Add(UserName)
        OldPass.Value = GenerateHash(tmp(0) & tmp(1))
        OldPass.ParameterName = "@oldPass"
        sqlCmd.Parameters.Add(OldPass)
        NewPass.Value = GenerateHash(tmp(0) & tmp(2))
        NewPass.ParameterName = "@NewPass"
        sqlCmd.Parameters.Add(NewPass)
        sqlCmd.Connection = sqlCnt

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 5 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spChangePasswordAyandeh"
        End If

        Dim Result As String = ""

        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("UserChangePassword - " & " UserName:" & tmp(0) & " - Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("UserChangePassword - " & " UserName:" & tmp(0) & " - Error:" & ex.Message)
        End Try
        sqlCnt.Close()

        Return Encrypt(Result)
    End Function

    <WebMethod()> _
    Public Function ResetPassword(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(1) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If
        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand

        Dim UserName, NewPassword, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        sqlCmd.CommandText = "spResetPassword"
        UserName.Value = tmp(0)
        UserName.ParameterName = "@UserName"
        sqlCmd.Parameters.Add(UserName)
        NewPassword.Value = GenerateHash(tmp(0) & tmp(0))
        NewPassword.ParameterName = "@newpass"
        sqlCmd.Parameters.Add(NewPassword)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 3 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spResetPasswordAyandeh"
        Else
            sqlCmd.CommandText = "spResetPassword"
        End If


        Dim Result As String = ""
        'sqlCmd.CommandText = "spResetPassword"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("ResetPassword - " & " UserName:" & tmp(0) & " - Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("ResetPassword - " & " UserName:" & tmp(0) & " - Error:" & ex.Message)
        End Try
        sqlCnt.Close()

        Return Encrypt(Result)

    End Function

    <WebMethod()> _
    Public Function DeleteUser(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(1) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If
        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand

        Dim UserName, NewPassword, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        'sqlCmd.CommandText = "spDeleteUser"
        UserName.Value = tmp(0)
        UserName.ParameterName = "@UserName"
        sqlCmd.Parameters.Add(UserName)

        'NewPassword.Value = GenerateHash(tmp(0) & tmp(0))
        'NewPassword.ParameterName = "@newpass"
        'sqlCmd.Parameters.Add(NewPassword)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 3 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spDeleteUserAyandeh"
        Else
            sqlCmd.CommandText = "spDeleteUser"
        End If


        Dim Result As String = ""
        'sqlCmd.CommandText = "spDeleteUser"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("DeleteUser - " & " UserName:" & tmp(0) & " - Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("DeleteUser - " & " UserName:" & tmp(0) & " - Error:" & ex.Message)
        End Try
        sqlCnt.Close()

        Return Encrypt(Result)

    End Function

    <WebMethod()> _
    Public Function UpdatePos(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try


        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim UserName, Name, Family, BrCode, GrantLevel, IsValid, PosID As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        sqlCmd.CommandText = "spUpdatePos"

        Dim PosIP, PosSerial, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure


        PosSerial.Value = tmp(0)
        PosSerial.ParameterName = "@PosSerial"
        sqlCmd.Parameters.Add(PosSerial)

        PosIP.Value = tmp(1)
        PosIP.ParameterName = "@PosIP"
        sqlCmd.Parameters.Add(PosIP)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 3 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spUpdatePosAyandeh"
        Else
            sqlCmd.CommandText = "spUpdatePos"
        End If

        Dim Result As String = ""
        ' sqlCmd.CommandText = "spUpdatePos"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("UpdatePos - " & " UserName:" & tmp(0) & " - Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("UpdatePos - " & " UserName:" & tmp(0) & " - Error:" & ex.Message)
        End Try
        sqlCnt.Close()

        Return Encrypt(Result)
        'Return Result
    End Function

    <WebMethod()> _
    Public Function UpdateUser(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(6) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand


        Dim UserName, Name, Family, BrCode, GrantLevel, IsValid, PosID, WinUserCode, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        sqlCmd.CommandText = "spUpdateUserNEW"

        UserName.Value = tmp(0)
        UserName.ParameterName = "@UserName"
        sqlCmd.Parameters.Add(UserName)

        Name.Value = tmp(1)
        Name.ParameterName = "@name"
        sqlCmd.Parameters.Add(Name)

        Family.Value = tmp(2)
        Family.ParameterName = "@family"
        sqlCmd.Parameters.Add(Family)

        BrCode.Value = tmp(3)
        BrCode.ParameterName = "@brcode"
        sqlCmd.Parameters.Add(BrCode)

        GrantLevel.Value = tmp(4)
        GrantLevel.ParameterName = "@grantLevel"
        sqlCmd.Parameters.Add(GrantLevel)

        IsValid.Value = tmp(5)
        IsValid.ParameterName = "@Isvalid"
        sqlCmd.Parameters.Add(IsValid)

        PosID.Value = tmp(6)
        PosID.ParameterName = "@PosID"
        sqlCmd.Parameters.Add(PosID)

        WinUserCode.Value = tmp(8)
        WinUserCode.ParameterName = "@WinUserCode"
        sqlCmd.Parameters.Add(WinUserCode)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 10 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spUpdateUserNEWAyandeh"
        Else
            sqlCmd.CommandText = "spUpdateUserNEW"
        End If


        Dim Result As String = ""
        ' sqlCmd.CommandText = "spUpdateUserNEW"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("UpdateUser - " & " UserName:" & tmp(0) & " - Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("UpdateUser - " & " UserName:" & tmp(0) & " - Error:" & ex.Message)
        End Try
        sqlCnt.Close()

        Return Encrypt(Result)
        'Return Result
    End Function

    <WebMethod()> _
    Public Function InsertPos(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(6) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand

        Dim PosSerial, PosIP, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure


        PosSerial.Value = tmp(0)
        PosSerial.ParameterName = "@PosSerial"
        sqlCmd.Parameters.Add(PosSerial)

        PosIP.Value = tmp(1)
        PosIP.ParameterName = "@PosIP"
        sqlCmd.Parameters.Add(PosIP)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 3 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spInsertPosNEWAyandeh"
        Else
            sqlCmd.CommandText = "spInsertPosNEW"
        End If


        Dim Result As String = ""
        'sqlCmd.CommandText = "spInsertPosNEW"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteScalar
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("InsertPos - " & " UserName:" & tmp(2) & " - Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("InsertPos - " & " UserName:" & tmp(2) & " - Error:" & ex.Message)
        End Try
        sqlCnt.Close()

        Return Encrypt(Result)

    End Function

    <WebMethod()> _
    Public Function InsertUser(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(6) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand

        Dim password, UserName, Name, Family, BrCode, GrantLevel, IsValid, PosID, WinUserCode1, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        UserName.Value = tmp(2)
        UserName.ParameterName = "@persCode"
        sqlCmd.Parameters.Add(UserName)

        Name.Value = tmp(0)
        Name.ParameterName = "@name"
        sqlCmd.Parameters.Add(Name)

        Family.Value = tmp(1)
        Family.ParameterName = "@family"
        sqlCmd.Parameters.Add(Family)

        BrCode.Value = tmp(3)
        BrCode.ParameterName = "@brcode"
        sqlCmd.Parameters.Add(BrCode)

        GrantLevel.Value = tmp(5)
        GrantLevel.ParameterName = "@grantLevel"
        sqlCmd.Parameters.Add(GrantLevel)

        PosID.Value = tmp(6)
        PosID.ParameterName = "@PosID"
        sqlCmd.Parameters.Add(PosID)

        IsValid.Value = tmp(4)
        IsValid.ParameterName = "@Isvalid"
        sqlCmd.Parameters.Add(IsValid)

        password.Value = GenerateHash(tmp(2) & tmp(2))
        password.ParameterName = "@password"
        sqlCmd.Parameters.Add(password)

        WinUserCode1.Value = tmp(8)
        WinUserCode1.ParameterName = "@WinUserCode"
        sqlCmd.Parameters.Add(WinUserCode1)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 10 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spInsertUserNEWAyandeh"
        Else
            sqlCmd.CommandText = "spInsertUserNEW"
        End If


        Dim Result As String = ""
        'sqlCmd.CommandText = "spInsertUserNEW"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteScalar
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("InsertUser - " & " UserName:" & tmp(2) & " - Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("InsertUser - " & " UserName:" & tmp(2) & " - Error:" & ex.Message)
        End Try
        sqlCnt.Close()

        Return Encrypt(Result)

    End Function


    <WebMethod()> _
    Public Function SearchPos(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(1) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If
        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim PosId, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        PosId.Value = tmp(0)
        PosId.ParameterName = "@PosSerial"
        sqlCmd.Parameters.Add(PosId)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 3 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "SpSearchPosNEWAyandeh"
        Else
            sqlCmd.CommandText = "SpSearchPosNEW"
        End If


        Dim Result As String = ""
        ' sqlCmd.CommandText = "SpSearchPosNEW"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = "Invalid"
            End If
            While sqlDR.Read
                Result = sqlDR(0) & "|" & sqlDR(1)
            End While

        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("SearchPos - " & " PosSerial:" & tmp(0) & " - Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("SearchPos - " & " PosSerial:" & tmp(0) & " - Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        sqlDR.Close()
        Return Encrypt(Result)

    End Function

    <WebMethod()> _
    Public Function SearchUser(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(1) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If
        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim UserName, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        UserName.Value = tmp(0)
        UserName.ParameterName = "@PersCode"
        sqlCmd.Parameters.Add(UserName)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 3 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "SpSearchUserNEWAyandeh"
        Else
            sqlCmd.CommandText = "SpSearchUserNEW"
        End If


        Dim Result As String = ""
        'sqlCmd.CommandText = "SpSearchUserNEW"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = "Invalid"
            End If
            While sqlDR.Read
                Result = sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(4) & "|" & sqlDR(5) & "|" & sqlDR(6) & "|" & sqlDR(9) & "|" & sqlDR(11)
            End While

        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("SearchUser - " & " UserName:" & tmp(0) & " - Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("SearchUser - " & " UserName:" & tmp(0) & " - Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        sqlDR.Close()
        Return Encrypt(Result)

    End Function


    <WebMethod()> _
    Public Function ShebaGenerator(ByVal param As String) As String
        Dim tmp As String
        Try
            tmp = Decrypt(param)
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader
        Dim AccountNumber As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        Dim Result As String = Nothing

        AccountNumber.Value = tmp
        AccountNumber.ParameterName = "@AccountNumber"
        sqlCmd.Parameters.Add(AccountNumber)

        sqlCmd.CommandText = "spShebaGenerator"
        sqlCmd.Connection = sqlCnt

        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                If sqlDR(0).ToString.Contains("ERROR") Then
                    Result = Encrypt("ERROR")
                Else
                    Result = Encrypt(sqlDR(0))
                End If
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("ShebaGenerator - " & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("ShebaGenerator - " & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function GetCounter() As String

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim BrCode, AccountNo, CustName, CustNo, IntCode, BranchUser, CardType, CustFamilyName, PrintTime, SpecialText, FollowUp, Refrence, TrDate, TrTime, RespCodePOS, RespMessagePOS, CardNoPOS, CardTypePOS, TerminalPOS, MerchantPOS, TrAmountPOS As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        Dim Result As String = Nothing

        sqlCmd.CommandText = "spGetCounter" ' "spGetCardDataGiftNew2" 
        sqlCmd.Connection = sqlCnt

        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                If sqlDR(0).ToString.Contains("ERROR") Then
                    Result = Encrypt("ERROR")
                Else
                    Result = Encrypt(sqlDR(0))
                End If
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetCardData - " & " BrCode:" & tmp(0) & " - AccountNo: " & tmp(1) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetCardData - " & " BrCode:" & tmp(0) & " - AccountNo: " & tmp(1) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function GetCardDataGift(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try


        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim BrCode, AccountNo, CustName, CustNo, IntCode, BranchUser, CardType, CustFamilyName, PrintTime, SpecialText, FollowUp, Refrence, TrDate, TrTime, RespCodePOS, RespMessagePOS, CardNoPOS, CardTypePOS, TerminalPOS, MerchantPOS, TrAmountPOS, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        BrCode.Value = tmp(0)
        BrCode.ParameterName = "@brcode"
        sqlCmd.Parameters.Add(BrCode)

        AccountNo.Value = tmp(1)
        AccountNo.ParameterName = "@AccountNo"
        sqlCmd.Parameters.Add(AccountNo)

        CustName.Value = tmp(2)
        CustName.ParameterName = "@CustName"
        sqlCmd.Parameters.Add(CustName)

        CustNo.Value = tmp(3)
        CustNo.ParameterName = "@CustNo"
        sqlCmd.Parameters.Add(CustNo)

        IntCode.Value = tmp(4)
        IntCode.ParameterName = "@IntCode"
        sqlCmd.Parameters.Add(IntCode)

        BranchUser.Value = tmp(5)
        BranchUser.ParameterName = "@BranchUser"
        sqlCmd.Parameters.Add(BranchUser)

        CardType.Value = tmp(6)
        CardType.ParameterName = "@CardType"
        sqlCmd.Parameters.Add(CardType)

        CustFamilyName.Value = tmp(7)
        CustFamilyName.ParameterName = "@CustFamilyName"
        sqlCmd.Parameters.Add(CustFamilyName)

        PrintTime.Value = DTP.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8)  ' tmp(2) 'tmp(9)
        PrintTime.ParameterName = "@printtime"
        sqlCmd.Parameters.Add(PrintTime)

        SpecialText.Value = tmp(9)
        SpecialText.ParameterName = "@SpecialText"
        sqlCmd.Parameters.Add(SpecialText)

        FollowUp.Value = tmp(11)
        FollowUp.ParameterName = "@FollowUp"
        sqlCmd.Parameters.Add(FollowUp)

        Refrence.Value = tmp(12)
        Refrence.ParameterName = "@Refrence"
        sqlCmd.Parameters.Add(Refrence)

        TrDate.Value = tmp(13)
        TrDate.ParameterName = "@TrDate"
        sqlCmd.Parameters.Add(TrDate)

        TrTime.Value = tmp(14)
        TrTime.ParameterName = "@TrTime"
        sqlCmd.Parameters.Add(TrTime)


        RespCodePOS.Value = tmp(15)
        RespCodePOS.ParameterName = "@RespCodePOS"
        sqlCmd.Parameters.Add(RespCodePOS)

        RespMessagePOS.Value = tmp(16)
        RespMessagePOS.ParameterName = "@RespMessagePOS"
        sqlCmd.Parameters.Add(RespMessagePOS)

        CardNoPOS.Value = tmp(17)
        CardNoPOS.ParameterName = "@CardNoPOS"
        sqlCmd.Parameters.Add(CardNoPOS)

        CardTypePOS.Value = tmp(18)
        CardTypePOS.ParameterName = "@CardTypePOS"
        sqlCmd.Parameters.Add(CardTypePOS)

        TerminalPOS.Value = tmp(19)
        TerminalPOS.ParameterName = "@TerminalPOS"
        sqlCmd.Parameters.Add(TerminalPOS)

        MerchantPOS.Value = tmp(20)
        MerchantPOS.ParameterName = "@MerchantPOS"
        sqlCmd.Parameters.Add(MerchantPOS)

        TrAmountPOS.Value = tmp(21)
        TrAmountPOS.ParameterName = "@TrAmountPOS"
        sqlCmd.Parameters.Add(TrAmountPOS)

        Dim tmpFortoBranches As Integer = 0
        '  If tmp.Length = 23 Then
        tmpFortoBranches = 1
        sqlCmd.CommandText = "spGetCardDataGiftNew2Ayandeh"
        'Else
        'sqlCmd.CommandText = "spGetCardDataGiftNew2"
        'End If

        Dim Result As String = Nothing

        'sqlCmd.CommandText = "spGetCardDataGiftNew2"   ' "spGetCardDataNEW2"
        sqlCmd.Connection = sqlCnt

        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader

            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                If sqlDR(0).ToString.Contains("ERROR") Then
                    Result = Encrypt(sqlDR(0))
                Else
                    Result = Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4) & "|" & sqlDR(5) & "|" & sqlDR(6) & "|" & sqlDR(7) & "|" & sqlDR(8))
                End If
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetCardData - " & " BrCode:" & tmp(0) & " - AccountNo: " & tmp(1) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetCardData - " & " BrCode:" & tmp(0) & " - AccountNo: " & tmp(1) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function GetCardData(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try


        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim BrCode, AccountNo, CustName, CustNo, IntCode, BranchUser, CardType, CustFamilyName, PrintTime, SpecialText, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        BrCode.Value = tmp(0)
        BrCode.ParameterName = "@brcode"
        sqlCmd.Parameters.Add(BrCode)

        AccountNo.Value = tmp(1)
        AccountNo.ParameterName = "@AccountNo"
        sqlCmd.Parameters.Add(AccountNo)

        CustName.Value = tmp(2)
        CustName.ParameterName = "@CustName"
        sqlCmd.Parameters.Add(CustName)

        CustNo.Value = tmp(3)
        CustNo.ParameterName = "@CustNo"
        sqlCmd.Parameters.Add(CustNo)

        IntCode.Value = tmp(4)
        IntCode.ParameterName = "@IntCode"
        sqlCmd.Parameters.Add(IntCode)

        BranchUser.Value = tmp(5)
        BranchUser.ParameterName = "@BranchUser"
        sqlCmd.Parameters.Add(BranchUser)

        CardType.Value = tmp(7)
        CardType.ParameterName = "@CardType"
        sqlCmd.Parameters.Add(CardType)

        CustFamilyName.Value = tmp(8)
        CustFamilyName.ParameterName = "@CustFamilyName"
        sqlCmd.Parameters.Add(CustFamilyName)


        PrintTime.Value = DTP.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8)  ' tmp(2) 'tmp(9)
        PrintTime.ParameterName = "@printtime"
        sqlCmd.Parameters.Add(PrintTime)

        SpecialText.Value = tmp(10)
        SpecialText.ParameterName = "@SpecialText"
        sqlCmd.Parameters.Add(SpecialText)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 4 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spGetCardDataNew2Ayandeh"
        Else
            sqlCmd.CommandText = "spGetCardDataNew2"
        End If


        Dim Result As String = Nothing

        'sqlCmd.CommandText = "spGetCardDataNew2"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                'If sqlDR(0) = "" Then
                If sqlDR(0) <> "-999" Then
                    If sqlDR(0) <> "Error" Then
                        Result = Encrypt(sqlDR(0))
                    Else
                        Result = Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4) & "|" & sqlDR(5) & "|" & sqlDR(6) & "|" & sqlDR(7))
                    End If
                Else
                    Result = Encrypt(sqlDR(0))
                End If
                'End If
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetCardData - " & " BrCode:" & tmp(0) & " - AccountNo: " & tmp(1) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetCardData - " & " BrCode:" & tmp(0) & " - AccountNo: " & tmp(1) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function GetStatusLastCard(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try


        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim BrCode, FortoBranches, AccountNo, CustName, CustNo, IntCode, BranchUser, CardType, CustFamilyName, PrintTime, SpecialText As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure


        BrCode.Value = tmp(0)
        BrCode.ParameterName = "@brcode"
        sqlCmd.Parameters.Add(BrCode)

        Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 2 Then
        '    tmpFortoBranches = 1
        sqlCmd.CommandText = "spGetStatusLastCardAyandeh"
        'Else
        'sqlCmd.CommandText = "spGetStatusLastCard"
        ' End If


        Dim Result As String = Nothing

        ' sqlCmd.CommandText = "spGetStatusLastCard"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Encrypt(sqlDR(0))
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetStatusLastCard - " & " BrCode:" & tmp(0) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetStatusLastCard - " & " BrCode:" & tmp(0) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function GetPosData(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim PosSerial, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        PosSerial.Value = tmp(0)
        PosSerial.ParameterName = "@PosSerial"
        sqlCmd.Parameters.Add(PosSerial)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 2 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spGetPosDataAyandeh"
        Else
            sqlCmd.CommandText = "spGetPosData"
        End If


        Dim Result As String = Nothing

        'sqlCmd.CommandText = "spGetPosData"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                If sqlDR(0) <> "-999" Then
                    Result = Encrypt(sqlDR(0) & "|" & sqlDR(1))
                Else
                    Result = Encrypt(sqlDR(0))
                End If
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetPosData - " & " BrCode:" & tmp(0) & " - AccountNo: " & tmp(1) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetPosData - " & " BrCode:" & tmp(0) & " - AccountNo: " & tmp(1) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function GetCardDataIranCard(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try


        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim BrCode, AccountNo, CustName, CustNo, IntCode, BranchUser, CardType, CustFamilyName, PrintTime, SpecialText, MobileNumber, FisheVarizi, Gender, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        BrCode.Value = tmp(0)
        BrCode.ParameterName = "@brcode"
        sqlCmd.Parameters.Add(BrCode)

        AccountNo.Value = tmp(1)
        AccountNo.ParameterName = "@AccountNo"
        sqlCmd.Parameters.Add(AccountNo)

        CustName.Value = tmp(2)
        CustName.ParameterName = "@CustName"
        sqlCmd.Parameters.Add(CustName)

        CustNo.Value = tmp(3)
        CustNo.ParameterName = "@CustNo"
        sqlCmd.Parameters.Add(CustNo)

        IntCode.Value = tmp(4)
        IntCode.ParameterName = "@IntCode"
        sqlCmd.Parameters.Add(IntCode)

        BranchUser.Value = tmp(5)
        BranchUser.ParameterName = "@BranchUser"
        sqlCmd.Parameters.Add(BranchUser)

        CardType.Value = tmp(7)
        CardType.ParameterName = "@CardType"
        sqlCmd.Parameters.Add(CardType)

        CustFamilyName.Value = tmp(8)
        CustFamilyName.ParameterName = "@CustFamilyName"
        sqlCmd.Parameters.Add(CustFamilyName)

        PrintTime.Value = DTP.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8)  'tmp(9)
        PrintTime.ParameterName = "@printtime"
        sqlCmd.Parameters.Add(PrintTime)

        SpecialText.Value = tmp(10)
        SpecialText.ParameterName = "@SpecialText"
        sqlCmd.Parameters.Add(SpecialText)

        FisheVarizi.Value = tmp(12)
        FisheVarizi.ParameterName = "@FisheVarizi"
        sqlCmd.Parameters.Add(FisheVarizi)

        MobileNumber.Value = tmp(13)
        MobileNumber.ParameterName = "@MobileNumber"
        sqlCmd.Parameters.Add(MobileNumber)

        Gender.Value = tmp(14)
        Gender.ParameterName = "@Gender"
        sqlCmd.Parameters.Add(Gender)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 16 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spGetCardDataIranCardNew2Ayandeh"
        Else
            sqlCmd.CommandText = "spGetCardDataIranCardNew2"
        End If

        Dim Result As String = Nothing

        'sqlCmd.CommandText = "spGetCardDataIranCardNew2"  '"spGetCardDataIranCard"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                If sqlDR(0).ToString.Contains("ERROR") Then
                    Result = Encrypt(sqlDR(0))
                Else
                    Result = Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4) & "|" & sqlDR(5) & "|" & sqlDR(6) & "|" & sqlDR(7) & "|" & sqlDR(8))
                End If
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetCardData - " & " BrCode:" & tmp(0) & " - AccountNo: " & tmp(1) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetCardData - " & " BrCode:" & tmp(0) & " - AccountNo: " & tmp(1) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function GetReCardDataIranCard(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try


        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim BrCode, AccountNo, CustName, CustNo, IntCode, BranchUser, CardType, CustFamilyName, PrintTime, SpecialText, MobileNumber, FisheVarizi, OldCardNO, OldAccountNo, CardColor, FortoBranches, Gender As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        BrCode.Value = tmp(0)
        BrCode.ParameterName = "@brcode"
        sqlCmd.Parameters.Add(BrCode)

        AccountNo.Value = tmp(1)
        AccountNo.ParameterName = "@AccountNo"
        sqlCmd.Parameters.Add(AccountNo)

        CustName.Value = tmp(2)
        CustName.ParameterName = "@CustName"
        sqlCmd.Parameters.Add(CustName)

        CustNo.Value = tmp(3)
        CustNo.ParameterName = "@CustNo"
        sqlCmd.Parameters.Add(CustNo)

        IntCode.Value = tmp(4)
        IntCode.ParameterName = "@IntCode"
        sqlCmd.Parameters.Add(IntCode)

        BranchUser.Value = tmp(5)
        BranchUser.ParameterName = "@BranchUser"
        sqlCmd.Parameters.Add(BranchUser)

        CardType.Value = tmp(7)
        CardType.ParameterName = "@CardType"
        sqlCmd.Parameters.Add(CardType)

        CustFamilyName.Value = tmp(8)
        CustFamilyName.ParameterName = "@CustFamilyName"
        sqlCmd.Parameters.Add(CustFamilyName)

        PrintTime.Value = DTP.DateToPersian(Now.Date).ShortDate & "-" & Now.TimeOfDay.ToString.Substring(0, 8)  'tmp(9)
        PrintTime.ParameterName = "@printtime"
        sqlCmd.Parameters.Add(PrintTime)

        SpecialText.Value = tmp(10)
        SpecialText.ParameterName = "@SpecialText"
        sqlCmd.Parameters.Add(SpecialText)

        FisheVarizi.Value = tmp(12)
        FisheVarizi.ParameterName = "@FisheVarizi"
        sqlCmd.Parameters.Add(FisheVarizi)

        MobileNumber.Value = tmp(13)
        MobileNumber.ParameterName = "@MobileNumber"
        sqlCmd.Parameters.Add(MobileNumber)

        OldCardNO.Value = tmp(14)
        OldCardNO.ParameterName = "@OldCardNO"
        sqlCmd.Parameters.Add(OldCardNO)

        OldAccountNo.Value = tmp(15)
        OldAccountNo.ParameterName = "@OldAccountNo"
        sqlCmd.Parameters.Add(OldAccountNo)

        CardColor.Value = tmp(16)
        CardColor.ParameterName = "@CardColor"
        sqlCmd.Parameters.Add(CardColor)

        Gender.Value = tmp(17)
        Gender.ParameterName = "@Gender"
        sqlCmd.Parameters.Add(Gender)


        Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 19 Then
        '    tmpFortoBranches = 1
        sqlCmd.CommandText = "spGetReCardDataIranCardNew2Ayandeh"
        'Else
        'sqlCmd.CommandText = "spGetReCardDataIranCardNew2"
        'End If


        Dim Result As String = Nothing

        ' sqlCmd.CommandText = "spGetReCardDataIranCardNew2" ' "spGetReCardDataIranCard"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                If sqlDR(0).ToString.Contains("ERROR") Then
                    Result = Encrypt(sqlDR(0))
                Else
                    Result = Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4) & "|" & sqlDR(5) & "|" & sqlDR(6) & "|" & sqlDR(7) & "|" & sqlDR(8))
                End If
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetReCardDataIranCard - " & " BrCode:" & tmp(0) & " - AccountNo: " & tmp(1) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetReCardDataIranCard - " & " BrCode:" & tmp(0) & " - AccountNo: " & tmp(1) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function


    <WebMethod()> _
    Public Function GetAllErrorMsg() As String
        'Try
        '    tmp = Decrypt(param).Split("|")
        'Catch ex As Exception
        '    Return (Encrypt("Invalid"))
        'End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        'Dim BrCode, AccountNo, CustName, CustNo, IntCode, BranchUser, CardType, CustFamilyName, PrintTime, SpecialText As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        Dim ResultErrormsg As String = Nothing

        sqlCmd.CommandText = "spGetAllErrorMsg"
        sqlCmd.Connection = sqlCnt

        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                ResultErrormsg = "Invalid"
            End If
            While sqlDR.Read
                ResultErrormsg = ResultErrormsg & sqlDR("ErrorMess") & "|" & sqlDR("ErrorID")
                ResultErrormsg = ResultErrormsg & "!"
            End While

        Catch ex As SqlException
            ResultErrormsg = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("GetAllErrorMsg - " & "- Error:" & ex.Message)
        Catch ex As Exception
            ResultErrormsg = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("GetAllErrorMsg - " & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Encrypt(ResultErrormsg)

    End Function

    <WebMethod()> _
    Public Function SearchError(ByVal param As String) As String
        Dim tmp1 As String
        Try
            tmp1 = Decrypt(param) '.Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim IDError1, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        Dim ResultError As String = Nothing

        IDError1.Value = tmp1
        IDError1.ParameterName = "@ErrorID"
        sqlCmd.Parameters.Add(IDError1)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spSearchErrorAyandeh"
        'Else
        '    sqlCmd.CommandText = "spSearchError"
        'End If


        sqlCmd.CommandText = "spSearchError"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                ResultError = "Invalid"
            End If
            While sqlDR.Read
                ResultError = ResultError & "|" & sqlDR("Probability") & "|" & sqlDR("Solution")
                ResultError = ResultError & "!"
            End While

        Catch ex As SqlException
            ResultError = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("SearchError - " & " BrCode:" & tmp(0) & " - AccountNo: " & tmp(1) & "- Error:" & ex.Message)
        Catch ex As Exception
            ResultError = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("SearchError - " & " BrCode:" & tmp(0) & " - AccountNo: " & tmp(1) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Encrypt(ResultError)
    End Function


    <WebMethod()> _
    Public Function SearchErrorMsg(ByVal param As String) As String
        Dim tmp1 As String
        Try
            tmp1 = Decrypt(param) '.Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim ErrorMsg1, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        Dim ResultError As String = Nothing

        ErrorMsg1.Value = tmp1
        ErrorMsg1.ParameterName = "@ErrorMsg"
        sqlCmd.Parameters.Add(ErrorMsg1)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spSearchErrorMsgAyandeh"
        'Else
        '    sqlCmd.CommandText = "spSearchErrorMsg"
        'End If


        sqlCmd.CommandText = "spSearchErrorMsg"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                ResultError = "Invalid"
            End If
            While sqlDR.Read
                ResultError = ResultError & sqlDR("ErrorMess") & "|" & sqlDR("Probability") & "|" & sqlDR("Solution") & "|" & sqlDR("Type") & "|" & sqlDR("ErrorID")
                ResultError = ResultError & "!"
            End While

        Catch ex As SqlException
            ResultError = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("SearchErrorMsg - " & " BrCode:" & tmp(0) & " - AccountNo: " & tmp(1) & "- Error:" & ex.Message)
        Catch ex As Exception
            ResultError = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("SearchErrorMsg - " & " BrCode:" & tmp(0) & " - AccountNo: " & tmp(1) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Encrypt(ResultError)

    End Function

    <WebMethod()> _
    Public Function SetPrintedCardFlag(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand


        Dim PAN, WhoPrinted, ActionType, PrintCount, BranchCode, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        PAN.Value = tmp(0)
        PAN.ParameterName = "@pan"
        sqlCmd.Parameters.Add(PAN)
        WhoPrinted.Value = tmp(1)
        WhoPrinted.ParameterName = "@whoprinted"
        sqlCmd.Parameters.Add(WhoPrinted)
        ActionType.Value = tmp(2)
        ActionType.ParameterName = "@ActionType"
        sqlCmd.Parameters.Add(ActionType)

        BranchCode.Value = tmp(4)
        BranchCode.ParameterName = "@BranchCode"
        sqlCmd.Parameters.Add(BranchCode)

        If Not String.Compare(tmp(3), "NULL") = 0 Then
            PrintCount.Value = tmp(3)
            PrintCount.ParameterName = "@PrintCount"
            sqlCmd.Parameters.Add(PrintCount)
        End If

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spSetIsPrintedFlagNew2Ayandeh"
        'Else
        '    sqlCmd.CommandText = "spSetIsPrintedFlagNew2"
        'End If


        Dim Result As String = "0"

        sqlCmd.CommandText = "spSetIsPrintedFlagNew2Ayandeh" '"spSetIsPrintedFlag"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            ' Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("SetPrintedCardFlag - " & " PAN:" & tmp(0) & " - WhoPrinted: " & tmp(1) & "- Error:" & ex.Message)
        Catch ex As Exception
            ' Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("SetPrintedCardFlag - " & " PAN:" & tmp(0) & " - WhoPrinted: " & tmp(1) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Encrypt(Result)
    End Function

    <WebMethod()> _
    Public Function InsertLog(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(5) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand

        Dim brcode, UserName, pan, logdate, logdata, IP, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        sqlCmd.CommandText = "spUpdateUser"

        brcode.Value = tmp(0)
        brcode.ParameterName = "@branchcode"
        sqlCmd.Parameters.Add(brcode)

        UserName.Value = tmp(1)
        UserName.ParameterName = "@username"
        sqlCmd.Parameters.Add(UserName)

        pan.Value = tmp(2)
        pan.ParameterName = "@pan"
        sqlCmd.Parameters.Add(pan)

        logdate.Value = tmp(3)
        logdate.ParameterName = "@logdate"
        sqlCmd.Parameters.Add(logdate)

        logdata.Value = tmp(4)
        logdata.ParameterName = "@logdata"
        sqlCmd.Parameters.Add(logdata)

        IP.Value = tmp(5)
        IP.ParameterName = "@ip"
        sqlCmd.Parameters.Add(IP)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spInsertLogAyandeh"
        'Else
        '    sqlCmd.CommandText = "spInsertLog"
        'End If



        Dim Result As String = "0"
        sqlCmd.CommandText = "spInsertLog"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            ' Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("InsertLog - " & " BrCode:" & tmp(0) & " - UserName: " & tmp(1) & " - PAN: " & tmp(2) & " - LogData: " & tmp(4) & "- Error:" & ex.Message)
        Catch ex As Exception
            ' Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("InsertLog - " & " BrCode:" & tmp(0) & " - UserName: " & tmp(1) & " - PAN: " & tmp(2) & " - LogData: " & tmp(4) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()

        Return Encrypt(Result)
        'Return Result
    End Function

    <WebMethod()> _
    Public Function UpdateFailedLogin(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(2) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand

        Dim UserName, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        UserName.Value = tmp(0)
        UserName.ParameterName = "@UserName"
        sqlCmd.Parameters.Add(UserName)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    tmpFortoBranches = 1
        '    If tmp(1) = "False" Then sqlCmd.CommandText = "spUpdateFailedLoginAyandeh"
        '    If tmp(1) = "True" Then sqlCmd.CommandText = "spResetFailedLoginAyandeh"
        'Else
        '    If tmp(1) = "False" Then sqlCmd.CommandText = "spUpdateFailedLogin"
        '    If tmp(1) = "True" Then sqlCmd.CommandText = "spResetFailedLogin"
        'End If


        Dim Result As String = ""
        If tmp(1) = "False" Then sqlCmd.CommandText = "spUpdateFailedLogin"
        If tmp(1) = "True" Then sqlCmd.CommandText = "spResetFailedLogin"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            If tmp(1) = "False" Then
                InsertLocalLog("UpdateFailedLogIn - " & " UserName:" & tmp(0) & "- Error:" & ex.Message)
            Else
                InsertLocalLog("ResetFailedLogin - " & " UserName:" & tmp(0) & "- Error:" & ex.Message)
            End If
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            If tmp(1) = "False" Then
                InsertLocalLog("UpdateFailedLogIn - " & " UserName:" & tmp(0) & "- Error:" & ex.Message)
            Else
                InsertLocalLog("ResetFailedLogin - " & " UserName:" & tmp(0) & "- Error:" & ex.Message)
            End If
        End Try
        sqlCnt.Close()
        Return Encrypt(Result)

    End Function

    <WebMethod()> _
    Public Function SetIsLogedIn(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(2) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand

        Dim UserName, FortoBranches, UserNameWindows As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        UserName.Value = tmp(0)
        UserName.ParameterName = "@UserName"
        sqlCmd.Parameters.Add(UserName)


        UserNameWindows.Value = tmp(1)
        UserNameWindows.ParameterName = "@UserNameWindows"
        sqlCmd.Parameters.Add(UserNameWindows)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spSetIsLogedInAyandeh"
        'Else
        '    sqlCmd.CommandText = "spSetIsLogedIn"
        'End If


        Dim Result As String = ""
        sqlCmd.CommandText = "spSetIsLogedInAyandeh"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("SetIsLogedIn - " & " UserName:" & tmp(0) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("SetIsLogedIn - " & " UserName:" & tmp(0) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Encrypt(Result)

    End Function



    <WebMethod()> _
    Public Function GetConsumableCards(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(0) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader
        Dim IsEnabled As New SqlParameter
        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        sqlCmd.CommandText = "spGetConsumableCards"

        If tmp.Length = 0 Then IsEnabled.Value = "0" Else IsEnabled.Value = tmp(0)

        IsEnabled.ParameterName = "@Type"
        sqlCmd.Parameters.Add(IsEnabled)

        Dim Result As String = ""
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetConsumableCards - " & " Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetConsumableCards - " & " Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function GetCardTypes(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(0) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader
        Dim IsEnabled As New SqlParameter
        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 2 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spGetCardTypesNEWAyandeh"
        Else
            sqlCmd.CommandText = "spGetCardTypesNEW"
        End If



        If tmp.Length = 0 Then IsEnabled.Value = "0" Else IsEnabled.Value = tmp(0)

        IsEnabled.ParameterName = "@Type"
        sqlCmd.Parameters.Add(IsEnabled)

        Dim Result As String = ""
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetCardType - " & " Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetCardType - " & " Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function CheckExistBlueCard(ByVal param As String) As String
        Dim tmpNew As String()

        tmpNew = param.Split("|")

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader
        Dim IntCode, FortoBranches As New SqlParameter
        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        ' sqlCmd.CommandText = "spCheckExistBlueCard"

        IntCode.Value = tmpNew(0)
        IntCode.ParameterName = "@IntCode"
        sqlCmd.Parameters.Add(IntCode)

        Dim tmpFortoBranches As Integer = 0
        If tmpNew.Length = 2 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spCheckExistBlueCardAyandeh"
        Else
            sqlCmd.CommandText = "spCheckExistBlueCard"
        End If


        Dim Result As String = ""
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = 0 ' Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = sqlDR(0)
                'Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("CheckExistBlueCard - " & " Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("CheckExistBlueCard - " & " Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function


    <WebMethod()> _
    Public Function CheckOldCardNo(ByVal param As String) As String
        Dim tmpNew As String()
        Try
            tmpNew = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader
        Dim CardNo, FortoBranches As New SqlParameter
        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        'sqlCmd.CommandText = "spCheckOldCardNoNew2" '"spCheckOldCardNo"

        CardNo.Value = tmpNew(0)
        CardNo.ParameterName = "@PAN"
        sqlCmd.Parameters.Add(CardNo)

        Dim tmpFortoBranches As Integer = 0
        If tmpNew.Length = 2 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spCheckOldCardNoNew2Ayandeh"
        Else
            sqlCmd.CommandText = "spCheckOldCardNoNew2"
        End If


        Dim Result As String = ""
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4) & "|" & sqlDR(5))
                'Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("CheckOldCardNo - " & " Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("CheckOldCardNo - " & " Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function


    <WebMethod()> _
    Public Function GetExistCustomer(ByVal param As String) As String
        Dim tmpNew As String
        Try
            tmpNew = Decrypt(param)
            'tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader
        Dim MelliCode, FortoBranches As New SqlParameter
        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        'sqlCmd.CommandText = "spGetExistCustomerNEW"

        MelliCode.Value = tmpNew
        MelliCode.ParameterName = "@IntCode"
        sqlCmd.Parameters.Add(MelliCode)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spGetExistCustomerNEWAyandeh"
        'Else
        '    sqlCmd.CommandText = "spGetExistCustomerNEW"
        'End If

        sqlCmd.CommandText = "spGetExistCustomerNEW"
        Dim Result As String = ""
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0)) '& "|" & sqlDR(1) & "|" & sqlDR(2))
                'Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetExistCustomerNumber - " & " Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetExistCustomerNumber - " & " Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function GetStackStatus(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(0) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If
        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader
        Dim UserName, NewPassword As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        sqlCmd.CommandText = "spGetStackStatus"
        Dim Result As String = ""
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & CLng(sqlDR(1)) - CLng(sqlDR(2)))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetStackStatus - " & " Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetStackStatus - " & " Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function GetStackStatusFromBranch(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(0) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If
        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader
        Dim BranchCode0, FortoBranches As New SqlParameter
        sqlCmd.Parameters.Clear()

        BranchCode0.Value = tmp(0)
        BranchCode0.ParameterName = "@BranchCode"
        sqlCmd.Parameters.Add(BranchCode0)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spGetStackStatusFromBranchAyandeh"
        'Else
        '    sqlCmd.CommandText = "spGetStackStatusFromBranch"
        'End If


        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        sqlCmd.CommandText = "spGetStackStatusFromBranch"

        Dim Result As String = ""
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & CLng(sqlDR(1)) - CLng(sqlDR(2)))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetStackStatusFromBranch - " & " Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetStackStatusFromBranch - " & " Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function GetConsumables(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader
        Dim ActionType, FortoBranches As New SqlParameter
        sqlCmd.Parameters.Clear()

        ActionType.Value = tmp(0)
        ActionType.ParameterName = "@ActionType"
        sqlCmd.Parameters.Add(ActionType)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spGetConsumablesAyandeh"
        'Else
        '    sqlCmd.CommandText = "spGetConsumables"
        'End If


        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        sqlCmd.CommandText = "spGetConsumables"
        Dim Result As String = ""
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetConsumables - " & " Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetConsumables - " & " Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function


    <WebMethod()> _
    Public Function GetPrintTempalte(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(1) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If
        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader
        Dim CardTypeCode, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        ' sqlCmd.CommandText = "spGetCardPrintTemplate"

        CardTypeCode.Value = tmp(0)
        CardTypeCode.ParameterName = "@CardTypeCode"
        sqlCmd.Parameters.Add(CardTypeCode)

        Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 3 Then
        tmpFortoBranches = 1
        sqlCmd.CommandText = "spGetCardPrintTemplateAyandeh"
        'Else
        '    sqlCmd.CommandText = "spGetCardPrintTemplate"
        'End If


        Dim Result As String = ""
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Encrypt(sqlDR(0) & "|" & sqlDR(1))
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetPrintTemplate - " & " CardType:" & tmp(0) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetPrintTemplate - " & " CardType:" & tmp(0) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result
    End Function

    <WebMethod()> _
    Public Function GetCardRemain(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader
        Dim CardTypeCode, BrCode, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        'sqlCmd.CommandText = "spGetCardRemainNEW2"

        CardTypeCode.Value = tmp(0)
        CardTypeCode.ParameterName = "@CardType"
        sqlCmd.Parameters.Add(CardTypeCode)

        BrCode.Value = tmp(1)
        BrCode.ParameterName = "@BrCode"
        sqlCmd.Parameters.Add(BrCode)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 4 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spGetCardRemainNEW2Ayandeh"
        Else
            sqlCmd.CommandText = "spGetCardRemainNEW2"
        End If


        Dim Result As String = ""
        sqlCmd.Connection = sqlCnt

        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Encrypt(sqlDR(0))
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetCardRemain - " & " CardType:" & tmp(0) & " - BrCode:" & tmp(1) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetCardRemain - " & " CardType:" & tmp(0) & " - BrCode:" & tmp(1) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function GetBranchStatckStatus(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader
        Dim BrCode, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        'sqlCmd.CommandText = "spGetBranchStackStatus"

        BrCode.Value = tmp(0)
        BrCode.ParameterName = "@BrCode"
        sqlCmd.Parameters.Add(BrCode)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 3 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spGetBranchStackStatusAyandeh"
        Else
            sqlCmd.CommandText = "spGetBranchStackStatus"
        End If


        Dim Result As String = ""
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetBranchStackStatus - " & " BrCode:" & tmp(0) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetBranchStackStatus - " & " BrCode:" & tmp(0) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function UpdateCardPrintTemplate(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(2) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand

        Dim CardPrintTemplate, CardTypeCode, ProductCode, IsEnabled, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        ProductCode.Value = tmp(0)
        ProductCode.ParameterName = "@ProductCode"
        sqlCmd.Parameters.Add(ProductCode)

        CardTypeCode.Value = tmp(1)
        CardTypeCode.ParameterName = "@CardTypeCode"
        sqlCmd.Parameters.Add(CardTypeCode)

        CardPrintTemplate.Value = tmp(2)
        CardPrintTemplate.ParameterName = "@CardPrintTemplate"
        sqlCmd.Parameters.Add(CardPrintTemplate)

        IsEnabled.Value = tmp(3)
        IsEnabled.ParameterName = "@IsEnabled"
        sqlCmd.Parameters.Add(IsEnabled)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 6 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spUpdateCardPrintTemplateNEW2Ayandeh"
        Else
            sqlCmd.CommandText = "spUpdateCardPrintTemplateNEW2"
        End If


        Dim Result As String = ""
        ' sqlCmd.CommandText = "spUpdateCardPrintTemplateNEW2"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("UpdateCardPrintTemplate - " & " CardType:" & tmp(0) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("UpdateCardPrintTemplate - " & " CardType:" & tmp(0) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Encrypt(Result)

    End Function

    <WebMethod()> _
    Public Function InsertCustomerData(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim IntCode, BirthDate, FatherName, Tell, Address, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        'Name.Value = tmp(0)
        'Name.ParameterName = "@Name"
        'sqlCmd.Parameters.Add(Name)

        'Family.Value = tmp(1)
        'Family.ParameterName = "@Family"
        'sqlCmd.Parameters.Add(Family)

        IntCode.Value = tmp(0)
        IntCode.ParameterName = "@IntCode"
        sqlCmd.Parameters.Add(IntCode)

        BirthDate.Value = tmp(1)
        BirthDate.ParameterName = "@BirthDate"
        sqlCmd.Parameters.Add(BirthDate)

        FatherName.Value = tmp(2)
        FatherName.ParameterName = "@FatherName"
        sqlCmd.Parameters.Add(FatherName)

        Tell.Value = tmp(3)
        Tell.ParameterName = "@Tell"
        sqlCmd.Parameters.Add(Tell)

        Address.Value = tmp(4)
        Address.ParameterName = "@Address"
        sqlCmd.Parameters.Add(Address)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 6 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spInsertCustomerDataNEWAyandeh"
        Else
            sqlCmd.CommandText = "spInsertCustomerDataNEW"
        End If


        Dim Result As String = ""
        ' sqlCmd.CommandText = "spInsertCustomerDataNEW"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("InsertCustomerData - " & " CardType:" & tmp(0) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("InsertCustomerData - " & " CardType:" & tmp(0) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Encrypt(Result)

    End Function

    <WebMethod()> _
    Public Function InsertCardPrintTemplate(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(2) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand

        Dim CardPrintTemplate, CardTypeCode, ProductCode, IsEnabled, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        ProductCode.Value = tmp(0)
        ProductCode.ParameterName = "@ProductCode"
        sqlCmd.Parameters.Add(ProductCode)

        CardTypeCode.Value = tmp(1)
        CardTypeCode.ParameterName = "@CardTypeDesc"
        sqlCmd.Parameters.Add(CardTypeCode)

        CardPrintTemplate.Value = tmp(2)
        CardPrintTemplate.ParameterName = "@CardPrintTemplate"
        sqlCmd.Parameters.Add(CardPrintTemplate)

        IsEnabled.Value = tmp(3)
        IsEnabled.ParameterName = "@IsEnabled"
        sqlCmd.Parameters.Add(IsEnabled)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 6 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spInsertCardPrintTemplateNEWAyandeh"
        Else
            sqlCmd.CommandText = "spInsertCardPrintTemplateNEW"
        End If


        Dim Result As String = ""
        ' sqlCmd.CommandText = "spInsertCardPrintTemplateNEW"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("InsertCardPrintTemplate - " & " CardType:" & tmp(0) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("InsertCardPrintTemplate - " & " CardType:" & tmp(0) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Encrypt(Result)

    End Function


    <WebMethod()> _
    Public Function GetReportDataGiftCard(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader
        Dim IntCode, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure


        IntCode.Value = tmp(0)
        IntCode.ParameterName = "@IntCode"
        sqlCmd.Parameters.Add(IntCode)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 3 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spGetReportDataGiftCardNEWAyandeh"
        Else
            sqlCmd.CommandText = "spGetReportDataGiftCardNEW"
        End If


        Dim Result As String = Nothing
        sqlCmd.CommandTimeout = 0
        ' sqlCmd.CommandText = "spGetReportDataGiftCardNEW"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4) & "|" & sqlDR(5))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetReportDataGiftCard - " & " Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetReportDataGiftCard - " & " Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function


    <WebMethod()> _
    Public Function GetReportData(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader
        Dim BrCode, ToDate, FromDate, cardtype, AccNo, actionType, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        Dim TmpbtnReportDateTimeClick As String = tmp(0)

        FromDate.Value = tmp(1)
        FromDate.ParameterName = "@FromDate"
        sqlCmd.Parameters.Add(FromDate)

        ToDate.Value = tmp(2)
        ToDate.ParameterName = "@ToDate"
        sqlCmd.Parameters.Add(ToDate)

        If Not String.Compare(tmp(3), "NULL") = 0 Then
            BrCode.Value = tmp(3)
            BrCode.ParameterName = "@brcode"
            sqlCmd.Parameters.Add(BrCode)
        End If

        If Not String.Compare(tmp(4), "NULL") = 0 Then
            cardtype.Value = tmp(4)
            cardtype.ParameterName = "@cardtype"
            sqlCmd.Parameters.Add(cardtype)
        End If

        If Not String.Compare(tmp(5), "NULL") = 0 Then
            AccNo.Value = tmp(5)
            AccNo.ParameterName = "@accno"
            sqlCmd.Parameters.Add(AccNo)

        End If

        actionType.Value = tmp(6)
        actionType.ParameterName = "@actionType"
        sqlCmd.Parameters.Add(actionType)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 9 Then
            tmpFortoBranches = 1
            If TmpbtnReportDateTimeClick = "True" Then
                sqlCmd.CommandText = "spGetReportDataDateTimeNEWAyandeh" '"spGetReportDataDateTimeNEW"
            Else
                sqlCmd.CommandText = "spGetReportDataNEWAyandeh"
            End If
        Else
            If TmpbtnReportDateTimeClick = "True" Then
                sqlCmd.CommandText = "spGetReportDataDateTimeNEW" '"spGetReportDataDateTimeNEW"
            Else
                sqlCmd.CommandText = "spGetReportDataNEW"
            End If
        End If



        Dim Result As String = Nothing
        sqlCmd.CommandTimeout = 0
        'If TmpbtnReportDateTimeClick = "True" Then
        '    sqlCmd.CommandText = "spGetReportDataDateTimeNEW" '"spGetReportDataDateTimeNEW"
        'Else
        '    sqlCmd.CommandText = "spGetReportDataNEW"
        'End If
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            If TmpbtnReportDateTimeClick = "True" Then
                While sqlDR.Read
                    Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4) & "|" & sqlDR(5) & "|" & sqlDR(6) & "|" & sqlDR(7) & "|" & sqlDR(8) & "|" & sqlDR(9) & "|" & sqlDR(10) & "|" & sqlDR(11) & "|" & sqlDR(12) & "|" & sqlDR(13) & "|" & sqlDR("Account") & "|" & sqlDR("FollowUpPOS") & "|" & sqlDR("RefrencePOS") & "|" & sqlDR("TrDatePOS") & "|" & sqlDR("TrTimePOS") & "|" & sqlDR("RespCodePOS") & "|" & sqlDR("RespMessagePOS") & "|" & sqlDR("CardNoPOS") & "|" & sqlDR("CardTypePOS") & "|" & sqlDR("TerminalPOS") & "|" & sqlDR("MerchantPOS") & "|" & sqlDR("TrAmountPOS") & "|" & sqlDR("Gender"))
                    Result = Result & "!"
                End While
            Else
                While sqlDR.Read
                    Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4) & "|" & sqlDR(5) & "|" & sqlDR(6) & "|" & sqlDR(7) & "|" & sqlDR(8) & "|" & sqlDR(9) & "|" & sqlDR(10) & "|" & sqlDR(11) & "|" & sqlDR(12) & "|" & sqlDR(13) & "|" & sqlDR("Account") & "|" & sqlDR("OldCardNO") & "|" & sqlDR("OldAccountNO") & "|" & sqlDR("CardColor") & "|" & sqlDR("Gender"))
                    Result = Result & "!"
                End While
            End If
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetReportData - " & " Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetReportData - " & " Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function


    <WebMethod()> _
    Public Function GetUserNameFrWinUser(ByVal UserName As String) As String
        Try
            UserName = Decrypt(UserName)
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim UserName0, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure


        UserName0.Value = UserName
        UserName0.ParameterName = "@UserName"
        sqlCmd.Parameters.Add(UserName0)

        Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    FortoBranches.Value = tmp(3)
        '    FortoBranches.ParameterName = "@FortoBranches"
        '    sqlCmd.Parameters.Add(FortoBranches)
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spGetUserNameFrWinUserAyandeh"
        'Else
        '    sqlCmd.CommandText = "spGetUserNameFrWinUser"
        'End If


        Dim Result As String = Nothing

        sqlCmd.CommandText = "spGetUserNameFrWinUser"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Encrypt(sqlDR(0))
                'Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("spGetUserNameFrWinUser - " & " Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("spGetUserNameFrWinUser - " & " Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function


    <WebMethod()> _
    Public Function GetImportedDataStatus(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim ToDate, FromDate, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure


        FromDate.Value = tmp(0)
        FromDate.ParameterName = "@FromDate"
        sqlCmd.Parameters.Add(FromDate)

        ToDate.Value = tmp(1)
        ToDate.ParameterName = "@ToDate"
        sqlCmd.Parameters.Add(ToDate)

        Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    FortoBranches.Value = tmp(3)
        '    FortoBranches.ParameterName = "@FortoBranches"
        '    sqlCmd.Parameters.Add(FortoBranches)
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spGetImportedDataStatusAyandeh"
        'Else
        '    sqlCmd.CommandText = "spGetImportedDataStatus"
        'End If


        Dim Result As String = Nothing

        sqlCmd.CommandText = "spGetImportedDataStatus"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetImportedDataStatus - " & " Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetImportedDataStatus - " & " Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function


    <WebMethod()> _
    Public Function GetRemainCardData(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        Dim Result As String = Nothing

        sqlCmd.CommandText = "spGetRemainCardData"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetRemainCardData - " & " Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetRemainCardData - " & " Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function


    <WebMethod()> _
    Public Function GetReportDataCounts(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim BrCode, ToDate, FromDate, cardtype, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure


        FromDate.Value = tmp(0)
        FromDate.ParameterName = "@FromDate"
        sqlCmd.Parameters.Add(FromDate)

        ToDate.Value = tmp(1)
        ToDate.ParameterName = "@ToDate"
        sqlCmd.Parameters.Add(ToDate)

        If Not String.Compare(tmp(2), "NULL") = 0 Then
            BrCode.Value = tmp(2)
            BrCode.ParameterName = "@brcode"
            sqlCmd.Parameters.Add(BrCode)

        End If

        If Not String.Compare(tmp(3), "NULL") = 0 Then
            cardtype.Value = tmp(3)
            cardtype.ParameterName = "@cardtype"
            sqlCmd.Parameters.Add(cardtype)
        End If

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    FortoBranches.Value = tmp(3)
        '    FortoBranches.ParameterName = "@FortoBranches"
        '    sqlCmd.Parameters.Add(FortoBranches)
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spGetReportDataCountsAyandeh"
        'Else
        '    sqlCmd.CommandText = "spGetReportDataCounts"
        'End If

        Dim Result As String = Nothing
        sqlCmd.CommandText = "spGetReportDataCounts"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetReportDataCounts - " & " Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetReportDataCounts - " & " Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function GetReportDataCountsBranch(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim BrCode, ToDate, FromDate, cardtype, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure


        FromDate.Value = tmp(0)
        FromDate.ParameterName = "@FromDate"
        sqlCmd.Parameters.Add(FromDate)

        ToDate.Value = tmp(1)
        ToDate.ParameterName = "@ToDate"
        sqlCmd.Parameters.Add(ToDate)

        If Not String.Compare(tmp(2), "NULL") = 0 Then
            BrCode.Value = tmp(2)
            BrCode.ParameterName = "@brcode"
            sqlCmd.Parameters.Add(BrCode)

        End If

        If Not String.Compare(tmp(3), "NULL") = 0 Then
            cardtype.Value = tmp(3)
            cardtype.ParameterName = "@cardtype"
            sqlCmd.Parameters.Add(cardtype)
        End If

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    FortoBranches.Value = tmp(3)
        '    FortoBranches.ParameterName = "@FortoBranches"
        '    sqlCmd.Parameters.Add(FortoBranches)
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spGetReportDataCountsBranchAyandeh"
        'Else
        '    sqlCmd.CommandText = "spGetReportDataCountsBranch"
        'End If

        Dim Result As String = Nothing

        sqlCmd.CommandText = "spGetReportDataCountsBranch"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetReportDataCountsBranch - " & " Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetReportDataCountsBranch - " & " Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function


    <WebMethod()> _
    Public Function GetgetUnusableCards(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim BrCode, ToDate, FromDate, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure


        FromDate.Value = tmp(0)
        FromDate.ParameterName = "@FromDate"
        sqlCmd.Parameters.Add(FromDate)

        ToDate.Value = tmp(1)
        ToDate.ParameterName = "@ToDate"
        sqlCmd.Parameters.Add(ToDate)


        BrCode.Value = tmp(2)
        BrCode.ParameterName = "@brcode"
        sqlCmd.Parameters.Add(BrCode)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    FortoBranches.Value = tmp(3)
        '    FortoBranches.ParameterName = "@FortoBranches"
        '    sqlCmd.Parameters.Add(FortoBranches)
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spGetunusableCardsAyandeh"
        'Else
        '    sqlCmd.CommandText = "spGetunusableCards"
        'End If


        Dim Result As String = Nothing

        sqlCmd.CommandText = "spGetunusableCards"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetunusableCards - " & " Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetunusableCards - " & " Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function


    <WebMethod()> _
    Public Function GetAdminMessages(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim BrCode, ToDate, FromDate, cardtype, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure


        FromDate.Value = tmp(0)
        FromDate.ParameterName = "@FromDate"
        sqlCmd.Parameters.Add(FromDate)

        ToDate.Value = tmp(1)
        ToDate.ParameterName = "@ToDate"
        sqlCmd.Parameters.Add(ToDate)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    FortoBranches.Value = tmp(3)
        '    FortoBranches.ParameterName = "@FortoBranches"
        '    sqlCmd.Parameters.Add(FortoBranches)
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spGetAdminMessagesAyandeh"
        'Else
        '    sqlCmd.CommandText = "spGetAdminMessages"
        'End If

        Dim Result As String = Nothing

        sqlCmd.CommandText = "spGetAdminMessages"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4) & "|" & sqlDR(5))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetAdminMessages - " & " Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetAdminMessages - " & " Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function GetLogData(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim BrCode, ToDate, FromDate, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure


        FromDate.Value = tmp(0)
        FromDate.ParameterName = "@FDate"
        sqlCmd.Parameters.Add(FromDate)

        ToDate.Value = tmp(1)
        ToDate.ParameterName = "@TDate"
        sqlCmd.Parameters.Add(ToDate)

        BrCode.Value = tmp(2)
        BrCode.ParameterName = "@brcode"
        sqlCmd.Parameters.Add(BrCode)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    FortoBranches.Value = tmp(3)
        '    FortoBranches.ParameterName = "@FortoBranches"
        '    sqlCmd.Parameters.Add(FortoBranches)
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spGetLogDataAyandeh"
        'Else
        '    sqlCmd.CommandText = "spGetLogData"
        'End If

        Dim Result As String = Nothing

        sqlCmd.CommandText = "spGetLogData"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4) & "|" & sqlDR(5))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetLogData - " & " BrCode:" & tmp(2) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetLogData - " & " BrCode:" & tmp(2) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function GetBranchRepositoryData(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim BrCode, ToDate, FromDate, Direction, ConsumableCode, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure


        If Not String.Compare(tmp(0), "NULL") = 0 Then
            ConsumableCode.Value = tmp(0)
            ConsumableCode.ParameterName = "@ConsumableCode"
            sqlCmd.Parameters.Add(ConsumableCode)
        End If

        If Not String.Compare(tmp(1), "NULL") = 0 Then
            BrCode.Value = tmp(1)
            BrCode.ParameterName = "@brcode"
            sqlCmd.Parameters.Add(BrCode)
        End If

        FromDate.Value = tmp(2)
        FromDate.ParameterName = "@FDate"
        sqlCmd.Parameters.Add(FromDate)

        ToDate.Value = tmp(3)
        ToDate.ParameterName = "@TDate"
        sqlCmd.Parameters.Add(ToDate)



        Direction.Value = tmp(4)
        Direction.ParameterName = "@Direction"
        sqlCmd.Parameters.Add(Direction)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 6 Then
            'FortoBranches.Value = tmp(3)
            'FortoBranches.ParameterName = "@FortoBranches"
            'sqlCmd.Parameters.Add(FortoBranches)
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spGetBranchRepositoryReportAyandeh"
        Else
            sqlCmd.CommandText = "spGetBranchRepositoryReport"
        End If

        Dim Result As String = Nothing

        'sqlCmd.CommandText = "spGetBranchRepositoryReport"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4) & "|" & sqlDR(5) & "|" & sqlDR(6) & "|" & sqlDR(7) & "|" & sqlDR(8) & "|" & sqlDR(9) & "|" & sqlDR(10) & "|" & sqlDR(11) & "|" & sqlDR(12))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetBranchRepositoryData - " & " BrCode:" & tmp(3) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetBranchRepositoryData - " & " BrCode:" & tmp(3) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function


    <WebMethod()> _
    Public Function GetBranchRepositoryStatusByDate(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim BrCode, ToDate, FromDate, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        BrCode.Value = tmp(0)
        BrCode.ParameterName = "@brcode"
        sqlCmd.Parameters.Add(BrCode)

        FromDate.Value = tmp(1)
        FromDate.ParameterName = "@fdt"
        sqlCmd.Parameters.Add(FromDate)

        ToDate.Value = tmp(2)
        ToDate.ParameterName = "@tdt"
        sqlCmd.Parameters.Add(ToDate)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    FortoBranches.Value = tmp(3)
        '    FortoBranches.ParameterName = "@FortoBranches"
        '    sqlCmd.Parameters.Add(FortoBranches)
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spGetBranchesStackStatusByDateAyandeh"
        'Else
        '    sqlCmd.CommandText = "spGetBranchesStackStatusByDate"
        'End If

        Dim Result As String = Nothing
        sqlCmd.CommandTimeout = 0
        sqlCmd.CommandText = "spGetBranchesStackStatusByDate"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetBranchRepositoryStatusByDate - " & " BrCode:" & tmp(0) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetBranchRepositoryStatusByDate - " & " BrCode:" & tmp(0) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function GetBranchRepositoryConflictData(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim ToDate, FromDate, brcode, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure


        FromDate.Value = tmp(0)
        FromDate.ParameterName = "@FDate"
        sqlCmd.Parameters.Add(FromDate)

        ToDate.Value = tmp(1)
        ToDate.ParameterName = "@TDate"
        sqlCmd.Parameters.Add(ToDate)

        If Not String.Compare(tmp(2), "NULL") = 0 Then
            brcode.Value = tmp(2)
            brcode.ParameterName = "@brcode"
            sqlCmd.Parameters.Add(brcode)
        End If

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    FortoBranches.Value = tmp(3)
        '    FortoBranches.ParameterName = "@FortoBranches"
        '    sqlCmd.Parameters.Add(FortoBranches)
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spGetBranchRepositoryConflictReportAyandeh"
        'Else
        '    sqlCmd.CommandText = "spGetBranchRepositoryConflictReport"
        'End If

        Dim Result As String = Nothing

        sqlCmd.CommandText = "spGetBranchRepositoryConflictReport"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4) & "|" & sqlDR(5) & "|" & sqlDR(6) & "|" & sqlDR(7) & "|" & sqlDR(8) & "|" & sqlDR(9) & "|" & sqlDR(10) & "|" & sqlDR(10) & "|" & sqlDR(11) & "|" & sqlDR(12))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetBranchRepositoryConflictReport - " & " BrCode:" & tmp(3) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetBranchRepositoryConflictReport - " & " BrCode:" & tmp(3) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function GetLastPrintedCardData(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim BrCode, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        BrCode.Value = tmp(0)
        BrCode.ParameterName = "@brcode"
        sqlCmd.Parameters.Add(BrCode)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 3 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "spGetLastPrintedCardAyandeh"
        Else
            sqlCmd.CommandText = "spGetLastPrintedCard"
        End If

        Dim Result As String = Nothing
        ' sqlCmd.CommandText = "spGetLastPrintedCard"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4) & "|" & sqlDR(5) & "|" & sqlDR(6) & "|" & sqlDR(7) & "|" & sqlDR(8) & "|" & sqlDR(9) & "|" & sqlDR(10) & "|" & sqlDR(11) & "|" & sqlDR(12) & "|" & sqlDR(13) & "|" & sqlDR(14) & "|" & sqlDR(15))
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetLastPrintedCard - " & " BrCode:" & tmp(0) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetLastPrintedCard - " & " BrCode:" & tmp(0) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function InsertConsumableRequest(ByVal param As String) As String

        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim brcode, UserName, consumableCode, RequestConsumableCount, RequestDate, Direction, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        '  sqlCmd.CommandText = "spInsertRepository"

        brcode.Value = tmp(0)
        brcode.ParameterName = "@brcode"
        sqlCmd.Parameters.Add(brcode)

        UserName.Value = tmp(1)
        UserName.ParameterName = "@username"
        sqlCmd.Parameters.Add(UserName)

        RequestConsumableCount.Value = tmp(2)
        RequestConsumableCount.ParameterName = "@RequestConsumableCount"
        sqlCmd.Parameters.Add(RequestConsumableCount)

        consumableCode.Value = tmp(3)
        consumableCode.ParameterName = "@consumableCode"
        sqlCmd.Parameters.Add(consumableCode)

        RequestDate.Value = tmp(4)
        RequestDate.ParameterName = "@RequestDate"
        sqlCmd.Parameters.Add(RequestDate)

        Direction.Value = tmp(5)
        Direction.ParameterName = "@Direction"
        sqlCmd.Parameters.Add(Direction)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    FortoBranches.Value = tmp(3)
        '    FortoBranches.ParameterName = "@FortoBranches"
        '    sqlCmd.Parameters.Add(FortoBranches)
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spInsertRepositoryAyandeh"
        'Else
        '    sqlCmd.CommandText = "spInsertRepository"
        'End If

        Dim Result As String = ""
        sqlCmd.CommandText = "spInsertRepository"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("InsertRepository - " & " BrCode:" & tmp(0) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("InsertRepository - " & " BrCode:" & tmp(0) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()

        Return Encrypt(Result)
        'Return Result
    End Function

    <WebMethod()> _
    Public Function InsertAdminMessage(ByVal param As String) As String

        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand

        Dim FromDate, Todate, msg, insertdate, IsValid, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        sqlCmd.CommandText = "spInsertAdminMessage"

        FromDate.Value = tmp(0)
        FromDate.ParameterName = "@FromDate"
        sqlCmd.Parameters.Add(FromDate)

        Todate.Value = tmp(1)
        Todate.ParameterName = "@Todate"
        sqlCmd.Parameters.Add(Todate)

        msg.Value = tmp(2)
        msg.ParameterName = "@msg"
        sqlCmd.Parameters.Add(msg)

        insertdate.Value = tmp(3)
        insertdate.ParameterName = "@insertdate"
        sqlCmd.Parameters.Add(insertdate)

        IsValid.Value = tmp(4)
        IsValid.ParameterName = "@IsValid"
        sqlCmd.Parameters.Add(IsValid)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    FortoBranches.Value = tmp(3)
        '    FortoBranches.ParameterName = "@FortoBranches"
        '    sqlCmd.Parameters.Add(FortoBranches)
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spInsertAdminMessageAyandeh"
        'Else
        '    sqlCmd.CommandText = "spInsertAdminMessage"
        'End If

        Dim Result As String = ""
        sqlCmd.CommandText = "spInsertAdminMessage"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("InsertAdminMessage - " & " Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("InsertAdminMessage - " & " Error:" & ex.Message)
        End Try
        sqlCnt.Close()

        Return Encrypt(Result)
        'Return Result
    End Function


    <WebMethod()> _
    Public Function GetRepositoryRequestsSelected(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim BrCode, Direction, Filter, ReqBrCode0, ReqDate0, ReqUser0, Consumables0, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        Direction.Value = tmp(1)
        Direction.ParameterName = "@Direction"
        sqlCmd.Parameters.Add(Direction)

        If Not String.Compare(tmp(0), "NULL") = 0 Then
            BrCode.Value = tmp(0)
            BrCode.ParameterName = "@brcode"
            sqlCmd.Parameters.Add(BrCode)
        End If


        ReqBrCode0.Value = tmp(3)
        ReqBrCode0.ParameterName = "@ReqBrCode"
        sqlCmd.Parameters.Add(ReqBrCode0)

        ReqDate0.Value = tmp(4)
        ReqDate0.ParameterName = "@ReqDate"
        sqlCmd.Parameters.Add(ReqDate0)

        ReqUser0.Value = tmp(5)
        ReqUser0.ParameterName = "@ReqUser"
        sqlCmd.Parameters.Add(ReqUser0)

        Consumables0.Value = tmp(6)
        Consumables0.ParameterName = "@ConsumableCode"
        sqlCmd.Parameters.Add(Consumables0)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    FortoBranches.Value = tmp(3)
        '    FortoBranches.ParameterName = "@FortoBranches"
        '    sqlCmd.Parameters.Add(FortoBranches)
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spGetRepositoryRequestsSelectedAyandeh"
        'Else
        '    sqlCmd.CommandText = "spGetRepositoryRequestsSelected"
        'End If

        Dim Result As String = Nothing

        sqlCmd.CommandText = "spGetRepositoryRequestsSelected"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4) & "|" & sqlDR(5) & "|" & sqlDR(6) & "|" & sqlDR(7) & "|" & sqlDR(8) & "|" & sqlDR(9) & "|" & sqlDR(10) & "|" & sqlDR(10) & "|" & sqlDR(11) & "|" & sqlDR(12) & "|" & sqlDR(13))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetRepositoryRequestsSelected - " & " BrCode:" & tmp(0) & "- Filter:" & tmp(2) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetRepositoryRequestsSelected - " & " BrCode:" & tmp(0) & "- Filter:" & tmp(2) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function GetRepositoryRequests(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        Dim BrCode, Direction, Filter, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        Direction.Value = tmp(1)
        Direction.ParameterName = "@Direction"
        sqlCmd.Parameters.Add(Direction)

        If Not String.Compare(tmp(0), "NULL") = 0 Then
            BrCode.Value = tmp(0)
            BrCode.ParameterName = "@brcode"
            sqlCmd.Parameters.Add(BrCode)
        End If

        Filter.Value = tmp(2)
        Filter.ParameterName = "@filter"
        sqlCmd.Parameters.Add(Filter)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    FortoBranches.Value = tmp(3)
        '    FortoBranches.ParameterName = "@FortoBranches"
        '    sqlCmd.Parameters.Add(FortoBranches)
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spGetRepositoryRequestsAyandeh"
        'Else
        '    sqlCmd.CommandText = "spGetRepositoryRequests"
        'End If


        Dim Result As String = Nothing

        sqlCmd.CommandText = "spGetRepositoryRequests"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = Encrypt("No Data")
            End If
            While sqlDR.Read
                Result = Result & Encrypt(sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4) & "|" & sqlDR(5) & "|" & sqlDR(6) & "|" & sqlDR(7) & "|" & sqlDR(8) & "|" & sqlDR(9) & "|" & sqlDR(10) & "|" & sqlDR(10) & "|" & sqlDR(11) & "|" & sqlDR(12) & "|" & sqlDR(13))
                Result = Result & "!"
            End While
            sqlDR.Close()
        Catch ex As SqlException
            Result = Encrypt("Data Base Error" & "|" & ex.Message)
            InsertLocalLog("GetRepositoryRequests - " & " BrCode:" & tmp(0) & "- Filter:" & tmp(2) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = Encrypt("Unexpected Error" & "|" & ex.Message)
            InsertLocalLog("GetRepositoryRequests - " & " BrCode:" & tmp(0) & "- Filter:" & tmp(2) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Result

    End Function

    <WebMethod()> _
    Public Function UpdateRepositoryAccepting(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand

        Dim AcceptDate, AcceptCount, UserName, RowId, ConsumableCode, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        AcceptDate.Value = tmp(0)
        AcceptDate.ParameterName = "@AcceptDate"
        sqlCmd.Parameters.Add(AcceptDate)

        AcceptCount.Value = tmp(1)
        AcceptCount.ParameterName = "@AcceptCount"
        sqlCmd.Parameters.Add(AcceptCount)

        UserName.Value = tmp(2)
        UserName.ParameterName = "@UserName"
        sqlCmd.Parameters.Add(UserName)

        RowId.Value = tmp(3)
        RowId.ParameterName = "@RowId"
        sqlCmd.Parameters.Add(RowId)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    FortoBranches.Value = tmp(3)
        '    FortoBranches.ParameterName = "@FortoBranches"
        '    sqlCmd.Parameters.Add(FortoBranches)
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spUpdateRepositoryAcceptingAyandeh"
        'Else
        '    sqlCmd.CommandText = "spUpdateRepositoryAccepting"
        'End If

        Dim Result As String = ""
        sqlCmd.CommandText = "spUpdateRepositoryAccepting"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteScalar
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("UpdateRepositoryAccepting - " & " RowID:" & tmp(3) & "- UserName:" & tmp(2) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("UpdateRepositoryAccepting - " & " RowID:" & tmp(3) & "- UserName:" & tmp(2) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Encrypt(Result)

    End Function

    <WebMethod()> _
    Public Function UpdateRepositoryReceiving(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand

        Dim ReceiveDate, ReceiveCount, UserName, RowId, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        ReceiveDate.Value = tmp(0)
        ReceiveDate.ParameterName = "@ReceiveDate"
        sqlCmd.Parameters.Add(ReceiveDate)

        ReceiveCount.Value = tmp(1)
        ReceiveCount.ParameterName = "@ReceiveCount"
        sqlCmd.Parameters.Add(ReceiveCount)

        UserName.Value = tmp(2)
        UserName.ParameterName = "@UserName"
        sqlCmd.Parameters.Add(UserName)

        RowId.Value = tmp(3)
        RowId.ParameterName = "@RowId"
        sqlCmd.Parameters.Add(RowId)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    FortoBranches.Value = tmp(3)
        '    FortoBranches.ParameterName = "@FortoBranches"
        '    sqlCmd.Parameters.Add(FortoBranches)
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spUpdateRepositoryReceivingAyandeh"
        'Else
        '    sqlCmd.CommandText = "spUpdateRepositoryReceiving"
        'End If


        Dim Result As String = ""
        sqlCmd.CommandText = "spUpdateRepositoryReceiving"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("UpdateRepositoryReceiving - " & " RowID:" & tmp(3) & "- UserName:" & tmp(2) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("UpdateRepositoryReceiving - " & " RowID:" & tmp(3) & "- UserName:" & tmp(2) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Encrypt(Result)

    End Function

    <WebMethod()> _
    Public Function DeleteReposotoryRequest(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand

        Dim RowId, FortoBranches As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        RowId.Value = tmp(0)
        RowId.ParameterName = "@RowId"
        sqlCmd.Parameters.Add(RowId)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    FortoBranches.Value = tmp(3)
        '    FortoBranches.ParameterName = "@FortoBranches"
        '    sqlCmd.Parameters.Add(FortoBranches)
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spDeleteRepositoryRequestAyandeh"
        'Else
        '    sqlCmd.CommandText = "spDeleteRepositoryRequest"
        'End If

        Dim Result As String = ""
        sqlCmd.CommandText = "spDeleteRepositoryRequest"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("DeleteRepositoryRequest - " & " RowID:" & tmp(0) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("DeleteRepositoryRequest - " & " RowID:" & tmp(0) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Encrypt(Result)

    End Function

    <WebMethod()> _
    Public Function DeleteAdminMessage(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand

        Dim RowId As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        RowId.Value = tmp(0)
        RowId.ParameterName = "@Id"
        sqlCmd.Parameters.Add(RowId)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spDeleteAdminMessageAyandeh"
        'Else
        '    sqlCmd.CommandText = "spDeleteAdminMessage"
        'End If

        Dim Result As String = ""
        sqlCmd.CommandText = "spDeleteAdminMessage"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("DeleteAdminMessage - " & " ID:" & tmp(0) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("DeleteAdminMessage - " & " ID:" & tmp(0) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Encrypt(Result)

    End Function

    <WebMethod()> _
    Public Function ChangeAdminMessageStatus(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand

        Dim RowId As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        RowId.Value = tmp(0)
        RowId.ParameterName = "@Id"
        sqlCmd.Parameters.Add(RowId)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spChangeAdminMessageStatusAyandeh"
        'Else
        '    sqlCmd.CommandText = "spChangeAdminMessageStatus"
        'End If

        Dim Result As String = ""
        sqlCmd.CommandText = "spChangeAdminMessageStatus"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("ChangeAdminMessageStatus - " & " ID:" & tmp(0) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("ChangeAdminMessageStatus - " & " ID:" & tmp(0) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Encrypt(Result)

    End Function

    <WebMethod()> _
    Public Function InsertUnusableCard(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(5) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand

        Dim branchcode, CardTypeCode, ErrorType, Desc, InsertDate As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        ' sqlCmd.CommandText = "spUpdateUser"

        branchcode.Value = tmp(0)
        branchcode.ParameterName = "@branchcode"
        sqlCmd.Parameters.Add(branchcode)

        CardTypeCode.Value = tmp(1)
        CardTypeCode.ParameterName = "@CardTypeCode"
        sqlCmd.Parameters.Add(CardTypeCode)

        ErrorType.Value = tmp(2)
        ErrorType.ParameterName = "@ErrorType"
        sqlCmd.Parameters.Add(ErrorType)

        Desc.Value = tmp(3)
        Desc.ParameterName = "@Desc"
        sqlCmd.Parameters.Add(Desc)

        InsertDate.Value = tmp(4)
        InsertDate.ParameterName = "@Date"
        sqlCmd.Parameters.Add(InsertDate)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spInsertUnusableCardAyandeh"
        'Else
        '    sqlCmd.CommandText = "spInsertUnusableCard"
        'End If

        Dim Result As String = ""
        sqlCmd.CommandText = "spInsertUnusableCard"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("InsertUnusableCard - " & " BrCode:" & tmp(0) & "- CardTypeCode:" & tmp(1) & "- ErrorType:" & tmp(2) & "- Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("InsertUnusableCard - " & " BrCode:" & tmp(0) & "- CardTypeCode:" & tmp(1) & "- ErrorType:" & tmp(2) & "- Error:" & ex.Message)
        End Try
        sqlCnt.Close()

        Return Encrypt(Result)
        'Return Result
    End Function


    <WebMethod()> _
    Public Function FindPos(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try

        Dim branchcode As New SqlParameter
        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        'branchcode.Value = tmp(0)
        'branchcode.ParameterName = "@brcode"
        'sqlCmd.Parameters.Add(branchcode)

        Dim Result As String = ""
        sqlCmd.CommandText = "SpFindPosNEW"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = "Invalid"
            End If
            While sqlDR.Read
                Result = Result & sqlDR(0) & "|" & sqlDR(1)
                Result = Result & "!"
            End While

        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("FindPos - Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("FindPos - Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Encrypt(Result)

    End Function

    <WebMethod()> _
    Public Function FindUsers(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(1) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If
        Dim branchcode As New SqlParameter


        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        branchcode.Value = tmp(0)
        branchcode.ParameterName = "@brcode"
        sqlCmd.Parameters.Add(branchcode)

        Dim tmpFortoBranches As Integer = 0
        If tmp.Length = 3 Then
            tmpFortoBranches = 1
            sqlCmd.CommandText = "SpGetUsersNEWAyandeh"
        Else
            sqlCmd.CommandText = "SpGetUsersNEW"
        End If

        Dim Result As String = ""
        'sqlCmd.CommandText = "SpGetUsersNEW"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = "Invalid"
            End If
            While sqlDR.Read
                Result = Result & sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4) & "|" & sqlDR(5) & "|" & sqlDR(6) & "|" & sqlDR(7) & "|" & sqlDR(8)
                Result = Result & "!"
            End While

        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("GetUsers - Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("GetUsers - Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Encrypt(Result)

    End Function

    <WebMethod()> _
    Public Function FindBranchUsers(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(1) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If
        Dim branchcode As New SqlParameter


        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        branchcode.Value = tmp(0)
        branchcode.ParameterName = "@brcode"
        sqlCmd.Parameters.Add(branchcode)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "SpFindBranchUsersAyandeh"
        'Else
        '    sqlCmd.CommandText = "SpFindBranchUsers"
        'End If

        Dim Result As String = ""
        sqlCmd.CommandText = "SpFindBranchUsers"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = "Invalid"
            End If
            While sqlDR.Read
                Result = Result & sqlDR(0) & "|" & sqlDR(1) & "|" & sqlDR(2) & "|" & sqlDR(3) & "|" & sqlDR(4) & "|" & sqlDR(5) & "|" & sqlDR(6)
                Result = Result & "!"
            End While

        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("FindBranchUsers - Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("FindBranchUsers - Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        Return Encrypt(Result)

    End Function


    <WebMethod()> _
    Public Function GetCustomerInfo(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        ''If Now.TimeOfDay.TotalSeconds - tmp(1) >= 30 Then
        ''    Return (Encrypt("Timeout|" & Now.TimeOfDay.ToString.Substring(0, 8)))
        ''End If
        ''Dim cc As String = My.Settings.Customers_DB_Setting
        Dim sqlCnt As SqlConnection = New SqlConnection(cc)
        Dim sqlCmd As New SqlCommand
        Dim sqlDR As SqlDataReader
        Dim Cust As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure
        sqlCmd.CommandText = "Check_Cust_Existance" '"ags_gss"

        Cust.Value = tmp(0)
        Cust.ParameterName = "@Cust"
        sqlCmd.Parameters.Add(Cust)


        Dim Result As String = ""
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            sqlDR = sqlCmd.ExecuteReader
            If sqlDR.HasRows = False Then
                Result = "New Customer"
            End If
            While sqlDR.Read
                Result = sqlDR(0)
            End While

        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("getCustomerInfo - " & "CustNo:" & tmp(0) & " - Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("getCustomerInfo - " & "CustNo:" & tmp(0) & " - Error:" & ex.Message)
        End Try
        sqlCnt.Close()
        ' sqlDR.Close()
        Return Encrypt(Result)

    End Function

    <WebMethod()> _
    Public Function InsertCardData22(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(6) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim BranchCode, CardType, PAN, EXPdate, CVV2, Track1, Track2, Track3, AccountNo, CustName, CustFamilyName, CustNo, IntCode, InsertDate, Account As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        Account.Value = tmp(0)
        Account.ParameterName = "@Account"
        sqlCmd.Parameters.Add(Account)


        If Not String.Compare(tmp(1), "NULL") = 0 Then
            BranchCode.Value = tmp(1)
            BranchCode.ParameterName = "@BranchCode"
            sqlCmd.Parameters.Add(BranchCode)
        End If


        CardType.Value = tmp(2)
        CardType.ParameterName = "@CardType"
        sqlCmd.Parameters.Add(CardType)

        PAN.Value = tmp(3)
        PAN.ParameterName = "@PAN"
        sqlCmd.Parameters.Add(PAN)

        EXPdate.Value = tmp(4)
        EXPdate.ParameterName = "@EXPdate"
        sqlCmd.Parameters.Add(EXPdate)

        CVV2.Value = tmp(5)
        CVV2.ParameterName = "@CVV2"
        sqlCmd.Parameters.Add(CVV2)

        Track1.Value = tmp(6)
        Track1.ParameterName = "@Track1"
        sqlCmd.Parameters.Add(Track1)

        Track2.Value = tmp(7)
        Track2.ParameterName = "@Track2"
        sqlCmd.Parameters.Add(Track2)

        Track3.Value = tmp(8)
        Track3.ParameterName = "@Track3"
        sqlCmd.Parameters.Add(Track3)

        If Not String.Compare(tmp(9), "NULL") = 0 Then
            AccountNo.Value = tmp(9)
            AccountNo.ParameterName = "@AccountNo"
            sqlCmd.Parameters.Add(AccountNo)
        End If
        If Not String.Compare(tmp(10), "NULL") = 0 Then
            CustName.Value = tmp(10)
            CustName.ParameterName = "@CustName"
            sqlCmd.Parameters.Add(CustName)
        End If
        If Not String.Compare(tmp(11), "NULL") = 0 Then
            CustFamilyName.Value = tmp(11)
            CustFamilyName.ParameterName = "@CustFamilyName"
            sqlCmd.Parameters.Add(CustFamilyName)
        End If
        If Not String.Compare(tmp(12), "NULL") = 0 Then
            CustNo.Value = tmp(12)
            CustNo.ParameterName = "@CustNo"
            sqlCmd.Parameters.Add(CustNo)
        End If
        If Not String.Compare(tmp(13), "NULL") = 0 Then
            IntCode.Value = tmp(13)
            IntCode.ParameterName = "@IntCode"
            sqlCmd.Parameters.Add(IntCode)
        End If
        InsertDate.Value = tmp(14)
        InsertDate.ParameterName = "@InsertDate"
        sqlCmd.Parameters.Add(InsertDate)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spInsertCardRecordsNEWAyandeh"
        'Else
        '    sqlCmd.CommandText = "spInsertCardRecordsNEW"
        'End If

        Dim Result As String = ""
        sqlCmd.CommandText = "spInsertCardRecordsNEW"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("InsertCardRecords - " & " CardNo:" & tmp(2) & " - Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("InsertCardRecords - " & " CardNo:" & tmp(2) & " - Error:" & ex.Message)
        End Try
        sqlCnt.Close()

        Return Encrypt(Result)

    End Function


    <WebMethod()> _
    Public Function InsertCardData(ByVal param As String) As String
        Try
            tmp = Decrypt(param).Split("|")
        Catch ex As Exception
            Return (Encrypt("Invalid"))
        End Try
        'If Now.TimeOfDay.TotalSeconds - tmp(6) >= 3 Then
        '    Return (Encrypt("Timeout"))
        'End If

        Dim sqlCnt As SqlConnection = New SqlConnection(conn)
        Dim sqlCmd As New SqlCommand
        Dim BranchCode, CardType, PAN, EXPdate, CVV2, Track1, Track2, Track3, AccountNo, CustName, CustFamilyName, CustNo, IntCode, InsertDate, Account As New SqlParameter

        sqlCmd.Parameters.Clear()
        sqlCmd.Connection = sqlCnt
        sqlCmd.CommandType = Data.CommandType.StoredProcedure

        Account.Value = tmp(0)
        Account.ParameterName = "@Account"
        sqlCmd.Parameters.Add(Account)


        If Not String.Compare(tmp(1), "NULL") = 0 Then
            BranchCode.Value = tmp(1)
            BranchCode.ParameterName = "@BranchCode"
            sqlCmd.Parameters.Add(BranchCode)
        End If


        CardType.Value = tmp(2)
        CardType.ParameterName = "@CardType"
        sqlCmd.Parameters.Add(CardType)

        PAN.Value = tmp(3)
        PAN.ParameterName = "@PAN"
        sqlCmd.Parameters.Add(PAN)

        EXPdate.Value = tmp(4)
        EXPdate.ParameterName = "@EXPdate"
        sqlCmd.Parameters.Add(EXPdate)

        CVV2.Value = tmp(5)
        CVV2.ParameterName = "@CVV2"
        sqlCmd.Parameters.Add(CVV2)

        Track1.Value = tmp(6)
        Track1.ParameterName = "@Track1"
        sqlCmd.Parameters.Add(Track1)

        Track2.Value = tmp(7)
        Track2.ParameterName = "@Track2"
        sqlCmd.Parameters.Add(Track2)

        Track3.Value = tmp(8)
        Track3.ParameterName = "@Track3"
        sqlCmd.Parameters.Add(Track3)

        If Not String.Compare(tmp(9), "NULL") = 0 Then
            AccountNo.Value = tmp(9)
            AccountNo.ParameterName = "@AccountNo"
            sqlCmd.Parameters.Add(AccountNo)
        End If
        If Not String.Compare(tmp(10), "NULL") = 0 Then
            CustName.Value = tmp(10)
            CustName.ParameterName = "@CustName"
            sqlCmd.Parameters.Add(CustName)
        End If
        If Not String.Compare(tmp(11), "NULL") = 0 Then
            CustFamilyName.Value = tmp(11)
            CustFamilyName.ParameterName = "@CustFamilyName"
            sqlCmd.Parameters.Add(CustFamilyName)
        End If
        If Not String.Compare(tmp(12), "NULL") = 0 Then
            CustNo.Value = tmp(12)
            CustNo.ParameterName = "@CustNo"
            sqlCmd.Parameters.Add(CustNo)
        End If
        If Not String.Compare(tmp(13), "NULL") = 0 Then
            IntCode.Value = tmp(13)
            IntCode.ParameterName = "@IntCode"
            sqlCmd.Parameters.Add(IntCode)
        End If
        InsertDate.Value = tmp(14)
        InsertDate.ParameterName = "@InsertDate"
        sqlCmd.Parameters.Add(InsertDate)

        'Dim tmpFortoBranches As Integer = 0
        'If tmp.Length = 4 Then
        '    tmpFortoBranches = 1
        '    sqlCmd.CommandText = "spInsertCardRecordsNEWAyandeh"
        'Else
        '    sqlCmd.CommandText = "spInsertCardRecordsNEW"
        'End If

        Dim Result As String = ""
        sqlCmd.CommandText = "spInsertCardRecordsNEW"
        sqlCmd.Connection = sqlCnt
        Try
            sqlCnt.Open()
            Result = sqlCmd.ExecuteNonQuery
        Catch ex As SqlException
            Result = "Data Base Error" & "|" & ex.Message
            InsertLocalLog("InsertCardRecords - " & " CardNo:" & tmp(2) & " - Error:" & ex.Message)
        Catch ex As Exception
            Result = "Unexpected Error" & "|" & ex.Message
            InsertLocalLog("InsertCardRecords - " & " CardNo:" & tmp(2) & " - Error:" & ex.Message)
        End Try
        sqlCnt.Close()

        Return Encrypt(Result)

    End Function


    Sub InsertLocalLog(ByVal LogDesc As String)
        Try
            Dim x As String = ""
            If CheckLogFile() Then
                Dim r As New System.IO.StreamReader("c:\GSSLOG\GSSlog" & GetPersianDate(Now.Date).Replace("/", "") & ".txt")
                x = r.ReadToEnd
                r.Close()
            End If

            Dim w As New System.IO.StreamWriter("c:\GSSLOG\GSSlog" & GetPersianDate(Now.Date).Replace("/", "") & ".txt")
            w.Write(x)
            w.WriteLine(Now.TimeOfDay.ToString.Substring(0, 8) & " - " & LogDesc)
            w.Close()
        Catch ex As Exception

        End Try

    End Sub

    Function CheckLogFile() As Boolean
        Try
            If Not Directory.Exists("C:\GSSLOG") Then
                Directory.CreateDirectory("C:\GSSLOG")
                Return False
            End If
            If Not File.Exists("c:\GSSLOG\GSSlog" & GetPersianDate(Now.Date).Replace("/", "") & ".txt") Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception

        End Try

    End Function

    Function GetPersianDate(ByVal dateMiladi As String) As String
        Dim tmpDate As New System.Globalization.PersianCalendar
        Dim d, m, y As String
        d = tmpDate.GetDayOfMonth(Now.Date)
        m = tmpDate.GetMonth(Now.Date)
        y = tmpDate.GetYear(Now.Date)
        If CInt(d < 10) Then
            d = "0" & d
        End If
        If CInt(m) < 10 Then
            m = "0" & m
        End If
        Return y & "/" & m & "/" & d
    End Function
End Class