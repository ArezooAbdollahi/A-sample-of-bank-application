Imports System.Collections.Generic
Imports System.Text

Namespace SCLib
	Public NotInheritable Class SCUtil

		Friend Shared ReadOnly g_vchHexDigits As Char()


		Shared Sub New()
			SCUtil.g_vchHexDigits = New Char(15) {"0"C, "1"C, "2"C, "3"C, "4"C, "5"C, _
				"6"C, "7"C, "8"C, "9"C, "A"C, "B"C, _
				"C"C, "D"C, "E"C, "F"C}
		End Sub

		Public Sub New()
		End Sub

		Public Shared Function FromByte(b As Byte) As String
			Dim chArray1 As Char() = New Char(1) {SCUtil.g_vchHexDigits(b >> 4), SCUtil.g_vchHexDigits(b And 15)}
			Return New String(chArray1)
		End Function

		Public Shared Function FromByteArray(byArr As Byte()) As String
			Return SCUtil.FromByteArray(byArr, 0, byArr.Length)
		End Function

		Public Shared Function FromByteArray(byArr As Byte(), nOffset As Integer, nLength As Integer) As String
			If byArr Is Nothing Then
				Throw New ArgumentNullException()
			End If
			If ((nOffset < 0) OrElse (nLength < 0)) OrElse ((nOffset + nLength) > byArr.Length) Then
				Throw New ArgumentOutOfRangeException()
			End If
			Dim chArray1 As Char() = New Char(nLength * 2 - 1) {}
			For num1 As Integer = 0 To nLength - 1
				chArray1(num1 * 2) = SCUtil.g_vchHexDigits(byArr(nOffset + num1) >> 4)
				chArray1((num1 * 2) + 1) = SCUtil.g_vchHexDigits(byArr(nOffset + num1) And 15)
			Next
			Return New String(chArray1)
		End Function

		Public Shared Function FromDigit(ch As Char) As Integer
            If (ch >= "0"c) AndAlso (ch <= "9"c) Then
                Return (CInt(AscW(ch)) - CInt(AscW("0"c)))
            End If
            If (ch >= "a"c) AndAlso (ch <= "f"c) Then
                Return ((CInt(AscW(ch) - CInt(AscW("a"c)) + CInt(AscW(ControlChars.Lf)))))
            End If
            If (ch < "A"c) OrElse (ch > "F"c) Then
                Throw New ArgumentOutOfRangeException()
            End If
            Return ((CInt(AscW(ch)) - CInt(AscW("A"c))) + CInt(AscW(ControlChars.Lf)))
		End Function

		Public Shared Function FromWord(n As Integer) As String
			If (n < -32768) OrElse (n > &Hffff) Then
				Throw New ArgumentOutOfRangeException()
			End If
			Dim chArray1 As Char() = New Char(3) {SCUtil.g_vchHexDigits((n >> 12) And 15), SCUtil.g_vchHexDigits((n >> 8) And 15), SCUtil.g_vchHexDigits((n >> 4) And 15), SCUtil.g_vchHexDigits(n And 15)}
			Return New String(chArray1)
		End Function

		Public Shared Function ToByteArray(sHex As String) As Byte()
			Dim buffer1 As Byte() = New Byte(sHex.Length \ 2 - 1) {}
			SCUtil.ToByteArray(sHex, buffer1, 0)
			Return buffer1
		End Function

		Public Shared Sub ToByteArray(sHex As String, byArr As Byte(), nOffset As Integer)
			If (sHex Is Nothing) OrElse (byArr Is Nothing) Then
				Throw New ArgumentNullException()
			End If
			If (sHex.Length And 1) <> 0 Then
				Throw New ArgumentException()
			End If
			Dim num2 As Integer = sHex.Length \ 2
			For num1 As Integer = 0 To num2 - 1
				byArr(nOffset + num1) = CByte((SCUtil.FromDigit(sHex(num1 * 2)) << 4) + SCUtil.FromDigit(sHex((num1 * 2) + 1)))
			Next
		End Sub

		Public Shared Function ToDigit(nDigit As Integer) As Char
			If (nDigit < 0) OrElse (nDigit > 15) Then
				Throw New ArgumentOutOfRangeException()
			End If
			Return SCUtil.g_vchHexDigits(nDigit)
		End Function


	End Class


End Namespace