Imports System.Net.Sockets
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Security.Cryptography

Module CiBS_Module
    
    Public IrCardPrintRee As Boolean = False
    Public CardPrintRee As Boolean = False
    Public CurrentPrinter, CurrentBranchCode, CurrentUser, ClientIP, CurrentUserNEW As String
    Public hPrinter As Int32
    Public tmpAns As String
    Public PrintTemplate, RetEncryptionKey, PrinterSerialNo As String
    Public CiBS_WS As New CiBS_WebService.CiBS_WebService
    Public tmpWSResult As String
    Public ws_fb() As String
    Public DT As New PersianToolS.PersinToolsClass
    Public UserGrantLevel As Integer
    Public PosSerial As String
    Public MannualRange As Boolean
    Dim tmpByte As Byte() = Encoding.Unicode.GetBytes(200)
    Public GSSdp As New GSS_TDES.TDES

    Public Function KOprinting(ByVal _hPrinter As String, ByVal kfile As String, ByVal Ofile As String) As String
        Dim br As BinaryReader
        Dim fsK As FileStream = Nothing
        Dim fsO As FileStream = Nothing
        Dim szK As Integer
        Dim szO As Integer
        Dim cmdKpanel, cmdOpanel, cmdEnter As String
        Dim pos As Integer
        Dim data() As Byte
        Dim tmpAns As String = Nothing
        cmdEnter = Chr(13)
        KOprinting = Nothing
        tmpAns = RunMyCommand(_hPrinter, "Pem;2")
        If String.Compare(tmpAns, "OK") = 0 Then
            tmpAns = RunMyCommand(_hPrinter, "Si")
            If String.Compare(tmpAns, "OK") = 0 Then
                tmpAns = RunMyCommand(_hPrinter, "Pr;ko")
                If String.Compare(tmpAns, "OK") = 0 Then
                    tmpAns = RunMyCommand(_hPrinter, "Ss")
                    If String.Compare(tmpAns, "OK") = 0 Then
                        tmpAns = RunMyCommand(_hPrinter, "Wcb;k")
                        If String.Compare(tmpAns, "OK") = 0 Then
                            tmpAns = RunMyCommand(_hPrinter, "Wcb;o")
                            'tmpAns = RunMyCommand(_hPrinter, "Pwb;k")
                            If String.Compare(tmpAns, "OK") = 0 Then
                                tmpAns = RunMyCommand(_hPrinter, "Pc;k;=;15")
                                If String.Compare(tmpAns, "OK") = 0 Then
                                    ' reading of the bmp file sizes
                                    fsK = File.Open(kfile, FileMode.Open)
                                    szK = CInt(fsK.Length)
                                    'Downloading K Panel
                                    cmdKpanel = Chr(27) & "Dbmp;k;0;648;0;"
                                    ReDim data(cmdKpanel.Length + szK + cmdEnter.Length)
                                    System.Text.Encoding.ASCII.GetBytes(cmdKpanel).CopyTo(data, 0)
                                    pos = pos + cmdKpanel.Length
                                    'Converting Kfile to binary
                                    br = New BinaryReader(fsK)
                                    br.ReadBytes(szK).CopyTo(data, pos)
                                    pos = pos + szK
                                    br.Close()
                                    fsK.Dispose()
                                    System.Text.Encoding.ASCII.GetBytes(cmdEnter).CopyTo(data, pos)
                                    pos = pos + cmdEnter.Length
                                    tmpAns = RunMyCommandBYTE(_hPrinter, data)
                                    Application.DoEvents()
                                    If String.Compare(tmpAns, "True") = 0 Then
                                        fsO = File.Open(Ofile, FileMode.Open)
                                        szO = CInt(fsO.Length)
                                        'K panel Dwonload OK
                                        pos = 0
                                        data = Nothing
                                        'Downloading O Panel
                                        cmdOpanel = Chr(27) & "Dbmp;o;0;648;0;"
                                        ReDim data(cmdOpanel.Length + szO + cmdEnter.Length)
                                        System.Text.Encoding.ASCII.GetBytes(cmdOpanel).CopyTo(data, 0)
                                        pos = pos + cmdOpanel.Length
                                        'Converting Ofile to binary
                                        br = New BinaryReader(fsO)
                                        br.ReadBytes(szO).CopyTo(data, pos)
                                        pos = pos + szO
                                        br.Close()
                                        fsO.Dispose()
                                        System.Text.Encoding.ASCII.GetBytes(cmdEnter).CopyTo(data, pos)
                                        pos = pos + cmdEnter.Length
                                        tmpAns = RunMyCommandBYTE(_hPrinter, data)
                                        Application.DoEvents()
                                        If String.Compare(tmpAns, "True") = 0 Then
                                            'O panel Dwonload OK
                                            tmpAns = RunMyCommand(_hPrinter, "Se")
                                            Application.DoEvents()
                                            If String.Compare(tmpAns, "OK") = 0 Then
                                                tmpAns = RunMyCommand(_hPrinter, "Pem;0")
                                                If String.Compare(tmpAns, "OK") = 0 Then
                                                    KOprinting = "Well Done"
                                                Else
                                                    KOprinting = tmpAns
                                                    Exit Function
                                                End If
                                            Else
                                                KOprinting = tmpAns
                                                Exit Function
                                            End If
                                        Else
                                            KOprinting = tmpAns
                                            Exit Function
                                        End If
                                    Else
                                        KOprinting = tmpAns
                                        Exit Function
                                    End If
                                Else
                                    KOprinting = tmpAns
                                    Exit Function
                                End If
                            Else
                                KOprinting = tmpAns
                                Exit Function
                            End If
                        Else
                            KOprinting = tmpAns
                            Exit Function
                        End If
                    Else
                        KOprinting = tmpAns
                        Exit Function
                    End If
                Else
                    KOprinting = tmpAns
                    Exit Function
                End If
            Else
                KOprinting = tmpAns
                Exit Function
            End If
            Else
                KOprinting = tmpAns
                Exit Function
            End If
    End Function

    Public Function RunMyCommand(ByVal _hPrinter As Int32, ByVal MyCommand As String) As String
        MyCommand = Chr(27) & MyCommand & Chr(13)
        RunMyCommand = Nothing
        Dim cmdByte(MyCommand.Length) As Byte
        System.Text.Encoding.ASCII.GetBytes(MyCommand).CopyTo(cmdByte, 0)
        Try
            WritePebble(_hPrinter, cmdByte, cmdByte.Length)
            RunMyCommand = ReadPrinterAnswer(_hPrinter)
        Catch ex As Exception
            RunMyCommand = "Error Running Command"
        End Try
    End Function

    Private Function RunMyCommandBYTE(ByVal _hPrinter As Int32, ByVal cmdByte() As Byte) As String
        RunMyCommandBYTE = Nothing
        Try
            RunMyCommandBYTE = WritePebble(_hPrinter, cmdByte, cmdByte.Length)
        Catch ex As Exception
            RunMyCommandBYTE = "Error Running Command"
        End Try
    End Function

    Public Function ReadPrinterAnswer(ByVal _hPrinter As Int32) As String
        ReadPrinterAnswer = Nothing
        Dim bRead As Boolean = False
        Dim _nbBytesRead As Integer
        Dim lpBuffer As IntPtr = Marshal.AllocHGlobal(1024)
        If Not lpBuffer.Equals(IntPtr.Zero) Then
            bRead = ReadPebble(_hPrinter, lpBuffer, 1024, _nbBytesRead)
            ReadPrinterAnswer = bRead
            If bRead Then
                ReadPrinterAnswer = Marshal.PtrToStringAnsi(lpBuffer, _nbBytesRead)
            End If
        End If
        Marshal.FreeHGlobal(lpBuffer)
    End Function

    Public Function OpenPrinter(ByVal PrinterName As String) As Int32
        OpenPrinter = OpenPebble(PrinterName)
    End Function

    Public Function ClosePrinter(ByVal _hPrinter As Int32) As Boolean
        ClosePrinter = ClosePebble(_hPrinter)
    End Function

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

    Public Function InsertLog(ByVal LogDesc As String) As Boolean
        For i As Integer = 1 To 100
            Dim tmp As String = Decrypt(CiBS_WS.InsertLog(Encrypt(LogDesc)))
            If tmp = 1 Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function SetPrintedCardFlag(ByVal LogDesc As String) As Boolean
        For i As Integer = 1 To 100
          Dim tmp As String = Decrypt(CiBS_WS.SetPrintedCardFlag(Encrypt(LogDesc)))
            If tmp = 1 Then
                Return True
            End If
        Next
        Return False
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

    Public strReaderName As String = "DIGENT CB1000 USB Reader 0"

    Function ReturnErrDesc(ByVal ErrNumber As Integer) As String
        Select Case ErrNumber
            Case 0 : Return "NO SCANNING DONE"
            Case 1 : Return "OK"
            Case -1 : Return "GENERAL ERROR"
            Case -2 : Return "CAN NOT OPEN DEVICE"
            Case -101 : Return "MATCH FAILED"
            Case -201 : Return "CAN NOT ALLOC MEMORY"
            Case -301 : Return "VECT FAILED"
            Case -501 : Return "FAKER FINGERPRINT"
            Case -601 : Return "LEFT FINGERPRINT"
            Case -602 : Return "RIGHT FINGERPRINT"
            Case -603 : Return "UP FINGERPRINT"
            Case -604 : Return "DOWN FINGERPRINT"
            Case -605 : Return "SMALL FINGERPRINT"
            Case -701 : Return "TOO WET"
            Case -702 : Return "TOO DRY"
        End Select
        Return Nothing
    End Function
End Module


