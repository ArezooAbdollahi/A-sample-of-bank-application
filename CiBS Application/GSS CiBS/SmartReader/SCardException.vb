Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices

Namespace SCLib
	Public NotInheritable Class SCardException
		Inherits ApplicationException



		Private m_nResponseCode As SCardResponseCode


		Public Sub New(nResponseCode As SCardResponseCode)
			Me.m_nResponseCode = nResponseCode
		End Sub

		Friend Shared Sub RaiseWin32ResponseCode(rc As UInteger)
			If (rc And 4294901760UI) = &H80100000UI Then
                Dim code1 As SCardResponseCode = DirectCast(CInt(rc), SCardResponseCode) And DirectCast(&HFFFF, SCardResponseCode)
				Throw New SCardException(code1)
			End If
			If rc > &Hffff Then
				Marshal.ThrowExceptionForHR(CInt(rc))
			Else
				Dim num1 As UInteger = rc
				If num1 = 6 Then
					Dim code2 As SCardResponseCode = SCardResponseCode.InvalidHandle
					Throw New SCardException(code2)
				End If
				Marshal.ThrowExceptionForHR(CInt(rc) Or -2147024896)
				Throw New InvalidProgramException()
			End If
		End Sub

		Public ReadOnly Property ResponseCode() As SCardResponseCode
			Get
				Return Me.m_nResponseCode
			End Get
		End Property


	End Class
End Namespace
