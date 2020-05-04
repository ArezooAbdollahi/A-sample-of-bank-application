
Imports CIBS.SCLib
Imports System.Text

Public Class SCAPI

    Private Shared AIDISO As String = "00A404000AA00000006203010C0101"
    Private Shared AIDDIGENT As String = "00A404000AA00000006203010C0201"

    Public Shared Function LoadReader() As IList
        Dim mgr1 As New SCResMgr()
        mgr1.EstablishContext(SCardContextScope.System)
        Dim list1 As New ArrayList()
        mgr1.ListReaders(list1)
        mgr1.ReleaseContext()
        Return list1
    End Function

    Public Shared Function getReaderProperty(ByVal strReaderName As String) As String
        Dim result As String
        Try
            Dim ResMgr As New SCResMgr
            ResMgr.EstablishContext(SCardContextScope.System)
            Dim Reader As New SCReader(ResMgr)
            Dim ReaderList As New ArrayList
            ResMgr.ListReaders(ReaderList)
            If (strReaderName = Nothing) Then
                result = "Please select a reader to continue..."
            Else
                Reader.Connect(strReaderName, SCardAccessMode.Shared, SCardProtocolIdentifiers.T0 Or SCardProtocolIdentifiers.T1)
                result = "You selected: " + strReaderName + vbCrLf + "For selected reader" + vbCrLf + vbTab + "Vendor: " + Reader.VendorName + vbCrLf + vbTab + "Vendor IFD Type: " + Reader.VendorIFDType + vbCrLf + vbTab + "IFD Channel Number: " + Reader.ChannelNumber.ToString
                Reader.Disconnect(SCardDisposition.UnpowerCard)
            End If
        Catch sclibex As SCLibException
            Return "Error in sclib."
        Catch sex As SCardException
            Return "Error in SCReader."
        Catch ex As Exception
            Return "Some general Error Occured."
        End Try
        Return result
    End Function

    Public Shared Function test(ByVal strReaderName As String, ByVal s As String) As String
        Return test(strReaderName, s, True)
    End Function

    Public Shared Function beginTransaction(ByVal strReaderName As String) As SCReader
        Dim ResMgr As New SCResMgr
        ResMgr.EstablishContext(SCardContextScope.System)

        Dim Reader As New SCReader(ResMgr)
        Dim ReaderList As New ArrayList
        ResMgr.ListReaders(ReaderList)
        Reader.Connect(strReaderName, SCardAccessMode.Shared, SCardProtocolIdentifiers.T0 Or SCardProtocolIdentifiers.T1)
        Reader.BeginTransaction()
        Return Reader
    End Function

    Public Shared Sub EndTransaction(ByVal reader As SCReader)
        reader.EndTransaction(SCardDisposition.UnpowerCard)
        reader.Disconnect(SCardDisposition.UnpowerCard)
    End Sub

    Public Shared Function getData(ByVal strReaderName As String) As Byte()
        Dim resByte() As Byte
        Dim res As String
        Try
            Dim Reader As SCReader = beginTransaction(strReaderName)
            Reader.Transmit(SCUtil.ToByteArray(AIDISO), resByte)
            Reader.Transmit(SCUtil.ToByteArray("B0310000C8"), resByte)
            EndTransaction(Reader)

        Catch sclibex As SCLibException
            res = "Error in sclib."
        Catch sex As SCardException
            res = "Error in SCReader."
        Catch ex As Exception
            res = "Some general Error Occured."
        End Try
        Return resByte
    End Function

    Public Shared Function enroll(ByVal strReaderName As String, ByVal m_future As Byte(), ByVal finger As Int16) As String

        Dim res As String = ""

        Try
            Dim Reader As SCReader = beginTransaction(strReaderName)
            Dim resByte() As Byte = sendData(strReaderName, m_future, Reader)
            Dim b As New StringBuilder(10)
            b.Append("B020")
            b.Append(finger.ToString("X2"))
            b.Append("00")
            Reader.Transmit(SCUtil.ToByteArray(b.ToString), resByte)
            res = IIf(SCUtil.FromByteArray(resByte) = "9000", "Enroll Ok", SCUtil.FromByteArray(resByte))
            EndTransaction(Reader)

        Catch sclibex As SCLibException
            res = "Error in sclib."
        Catch sex As SCardException
            res = "Error in SCReader."
        Catch ex As Exception
            res = "Some general Error Occured."
        End Try
        Return res



    End Function


    Public Shared Function Enroll_CB1000f(ByVal strReaderName As String, ByVal finger As Int16) As String

        Dim res As String = ""

        Try
            'Dim Reader As SCReader = beginTransaction(strReaderName)

            'Dim resByte As Byte()
            'Reader.Transmit(SCUtil.ToByteArray(AIDISO), resByte)
            'Dim b As StringBuilder = New StringBuilder(600)
            'b.Append("B0100000FA")

            'For index = 0 To 249
            '    b.Append(m_future(index).ToString("X2"))
            'Next
            'Reader.Transmit(SCUtil.ToByteArray(b.ToString), resByte)
            'b.Clear()
            'b.Append("B0100100")
            'b.Append((500 - 250).ToString("X2"))
            'For index = 250 To 499
            '    b.Append(m_future(index).ToString("X2"))
            'Next

            'Reader.Transmit(SCUtil.ToByteArray(b.ToString), resByte)

            'b.Clear()
            'b.Append("B0100100")
            'b.Append((m_future.Length - 500 - 1).ToString("X2"))

            'For index = 500 To m_future.Length - 2
            '    b.Append(m_future(index).ToString("X2"))
            'Next
            'Reader.Transmit(SCUtil.ToByteArray(b.ToString), resByte)
            'Dim b As New StringBuilder(10)
            'b.Append("B020")
            'b.Append(finger.ToString("X2"))
            'b.Append("00")
            'Reader.Transmit(SCUtil.ToByteArray(b.ToString), resByte)
            'res = IIf(SCUtil.FromByteArray(resByte) = "9000", "Enroll Ok", SCUtil.FromByteArray(resByte))
            'EndTransaction(Reader)

        Catch sclibex As SCLibException
            res = "Error in sclib."
        Catch sex As SCardException
            res = "Error in SCReader."
        Catch ex As Exception
            res = "Some general Error Occured."
        End Try
        Return res



    End Function

    Public Shared Function test(ByVal strReaderName As String, ByVal s As String, ByVal b As Boolean) As String
        Dim res As String = ""

        Try
            Dim Reader As SCReader = beginTransaction(strReaderName)
            Dim resByte() As Byte
            Reader.Transmit(SCUtil.ToByteArray(AIDISO), resByte)
            Reader.Transmit(SCUtil.ToByteArray(s), resByte)
            res = SCUtil.FromByteArray(resByte)
            EndTransaction(Reader)

        Catch sclibex As SCLibException
            res = "Error in sclib."
        Catch sex As SCardException
            res = "Error in SCReader."
        Catch ex As Exception
            res = "Some general Error Occured."
        End Try
        Return res


    End Function





    Public Shared Function getFormat(ByVal strReaderName As String) As String
        Dim resiso As String = ""
        Dim resdigent As String = ""
        Dim res As String = ""

        Try
            Dim Reader As SCReader = beginTransaction(strReaderName)
            Dim resByte() As Byte
            Reader.Transmit(SCUtil.ToByteArray(AIDISO), resByte)
            resiso = SCUtil.FromByteArray(resByte)
            EndTransaction(Reader)

        Catch sclibex As SCLibException
            resiso = "Error in sclib."
        Catch sex As SCardException
            resiso = "Error in SCReader."
        Catch ex As Exception
            resiso = "Some general Error Occured."
        End Try

        Try
            Dim Reader As SCReader = beginTransaction(strReaderName)
            Dim resByte() As Byte
            Reader.Transmit(SCUtil.ToByteArray(AIDDIGENT), resByte)
            resdigent = SCUtil.FromByteArray(resByte)
            EndTransaction(Reader)

        Catch sclibex As SCLibException
            resdigent = "Error in sclib."
        Catch sex As SCardException
            resdigent = "Error in SCReader."
        Catch ex As Exception
            resdigent = "Some general Error Occured."
        End Try


        If (resiso = "9000" And resdigent <> "9000") Then
            res = "ISO"
        ElseIf (resiso <> "9000" And resdigent = "9000") Then
            res = "DIGENT"
        ElseIf (resiso = "9000" And resdigent = "9000") Then
            res = "ISO & DIGENT"
        Else
            res = "Unknown"
        End If

        Return res

    End Function

    Private Shared Function sendData(ByVal strReaderName As String, ByVal m_future As Byte(), ByVal Reader As SCReader) As Byte()
        Dim resByte As Byte()
        Reader.Transmit(SCUtil.ToByteArray(AIDISO), resByte)
        Dim b As StringBuilder = New StringBuilder(600)
        b.Append("B0100000FA")
        For index As Integer = 0 To 249
            b.Append(m_future(index).ToString("X2"))
        Next
        Reader.Transmit(SCUtil.ToByteArray(b.ToString), resByte)
        b.Remove(0, b.Length)
        b.Append("B0100100")
        b.Append((500 - 250).ToString("X2"))
        For index As Integer = 250 To 499
            b.Append(m_future(index).ToString("X2"))
        Next

        Reader.Transmit(SCUtil.ToByteArray(b.ToString), resByte)

        b.Remove(0, b.Length)
        b.Append("B0100100")
        b.Append((m_future.Length - 500 - 1).ToString("X2"))

        For index As Integer = 500 To m_future.Length - 2
            b.Append(m_future(index).ToString("X2"))
        Next
        Reader.Transmit(SCUtil.ToByteArray(b.ToString), resByte)


        Return resByte
    End Function

    Shared Function match(ByVal strReaderName As String, ByVal m_future As Byte(), ByVal finger As Int16, ByVal securityLevel As Integer) As String

        Dim res As String = ""

        Try
            Dim Reader As SCReader = beginTransaction(strReaderName)
            Dim resByte() As Byte = sendData(strReaderName, m_future, Reader)
            Dim b As New StringBuilder(10)
            b.Append("B021")
            b.Append(finger.ToString("X2"))
            b.Append(securityLevel.ToString("X2"))
            Reader.Transmit(SCUtil.ToByteArray(b.ToString), resByte)
            res = IIf(SCUtil.FromByteArray(resByte) = "9000", "Correct", "Wrong")
            EndTransaction(Reader)

        Catch sclibex As SCLibException
            res = "Error in sclib."
        Catch sex As SCardException
            res = "Error in SCReader."
        Catch ex As Exception
            res = "Some general Error Occured."
        End Try
        Return res


    End Function


End Class
