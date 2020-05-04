Imports System.Text
Namespace SCLib
	Public Class CardCommand
		Implements ICloneable

		Private m_nCLA As Byte
		Private m_nINS As Byte
		Private m_nLe As Integer
		Private m_nP1 As Byte
		Private m_nP2 As Byte
		Private m_vbData As Byte()
		Public Const MAXEXTENDEDLC As Integer = &Hffff
		Public Const MAXEXTENDEDLE As Integer = &H10000
		Public Const MAXEXTENDEDLENGTH As Integer = &H10008
		Public Const MAXSHORTLC As Integer = &Hff
		Public Const MAXSHORTLE As Integer = &H100
		Public Const MAXSHORTLENGTH As Integer = &H105
		Public Const MINLENGTH As Integer = 4


		Public Sub New(nCLA As Byte, nINS As Byte, nP1 As Byte, nP2 As Byte)
			Me.m_nCLA = nCLA
			Me.m_nINS = nINS
			Me.m_nP1 = nP1
			Me.m_nP2 = nP2
			Me.m_vbData = Nothing
			Me.m_nLe = 0
		End Sub

		Public Sub New(nCLA As Byte, nINS As Byte, nP1 As Byte, nP2 As Byte, nLe As Integer)
			If (nLe < 0) OrElse (nLe > &H10000) Then
				Throw New ArgumentOutOfRangeException()
			End If
			Me.m_nCLA = nCLA
			Me.m_nINS = nINS
			Me.m_nP1 = nP1
			Me.m_nP2 = nP2
			Me.m_vbData = Nothing
			Me.m_nLe = If((nLe = 0), &H100, nLe)
		End Sub

		Public Sub New(nCLA As Byte, nINS As Byte, nP1 As Byte, nP2 As Byte, vbData As Byte())
			If vbData Is Nothing Then
				Throw New ArgumentNullException()
			End If
			If (vbData.Length <= 0) OrElse (vbData.Length > &Hffff) Then
				Throw New ArgumentOutOfRangeException()
			End If
			Me.m_nCLA = nCLA
			Me.m_nINS = nINS
			Me.m_nP1 = nP1
			Me.m_nP2 = nP2
			Me.m_vbData = vbData
			Me.m_nLe = 0
		End Sub

		Public Sub New(nCLA As Byte, nINS As Byte, nP1 As Byte, nP2 As Byte, vbData As Byte(), nLe As Integer)
			If vbData Is Nothing Then
				Throw New ArgumentNullException()
			End If
			If (vbData.Length <= 0) OrElse (vbData.Length > &Hffff) Then
				Throw New ArgumentOutOfRangeException()
			End If
			If (nLe < 0) OrElse (nLe > &H10000) Then
				Throw New ArgumentOutOfRangeException()
			End If
			Me.m_nCLA = nCLA
			Me.m_nINS = nINS
			Me.m_nP1 = nP1
			Me.m_nP2 = nP2
			Me.m_vbData = vbData
			Me.m_nLe = If((nLe = 0), &H100, nLe)
		End Sub

		Public Sub AppendData(vbData As Byte())
			If vbData Is Nothing Then
				Throw New ArgumentNullException()
			End If
			If (Me.Lc + vbData.Length) > &Hffff Then
				Throw New ArgumentOutOfRangeException()
			End If
			If Me.m_vbData Is Nothing Then
				Me.SetData(vbData)
			Else
				Me.AppendData(vbData, 0, vbData.Length)
			End If
		End Sub

		Public Sub AppendData(vbData As Byte(), nOffset As Integer, nLength As Integer)
			If vbData Is Nothing Then
				Throw New ArgumentNullException()
			End If
			If ((nOffset < 0) OrElse (nLength <= 0)) OrElse ((nLength > &Hffff) OrElse ((nOffset + nLength) > vbData.Length)) Then
				Throw New ArgumentOutOfRangeException()
			End If
			If Me.m_vbData Is Nothing Then
				Me.SetData(vbData, nOffset, nLength)
			Else
				Dim buffer1 As Byte() = New Byte(Me.m_vbData.Length + (nLength - 1)) {}
				Buffer.BlockCopy(Me.m_vbData, 0, buffer1, 0, Me.m_vbData.Length)
				Buffer.BlockCopy(vbData, nOffset, buffer1, Me.m_vbData.Length, nLength)
				Me.m_vbData = buffer1
			End If
		End Sub

		Public Function Clone() As Object Implements ICloneable.Clone
			Dim command1 As New CardCommand(Me.m_nCLA, Me.m_nINS, Me.m_nP1, Me.m_nP2)
			If Me.m_vbData IsNot Nothing Then
				command1.SetData(DirectCast(Me.m_vbData.Clone(), Byte()))
			End If
			command1.Le = Me.m_nLe
			Return command1
		End Function

		Private Function DumpBytes(vb As Byte(), nOffset As Integer, nLength As Integer) As String
			Dim chArray1 As Char() = New Char(nLength - 1) {}
			For num1 As Integer = 0 To nLength - 1
				Dim num2 As Byte = vb(nOffset + num1)
				If (num2 < &H20) OrElse ((num2 >= &H7f) AndAlso (num2 < 160)) Then
					chArray1(num1) = "·"C
				Else
					chArray1(num1) = ChrW(num2)
				End If
			Next
			Return New String(chArray1)
		End Function

		Public Function GenerateBytes() As Byte()
			Dim num1 As Integer = 4
			Dim num2 As Integer = 0
			If Me.m_vbData IsNot Nothing Then
				num2 = Me.m_vbData.Length
				num1 += (num2 + 1)
			End If
			If Me.m_nLe <> 0 Then
				num1 += 1
			End If
			Dim flag1 As Boolean = (num2 > &Hff) OrElse (Me.m_nLe > &H100)
			If flag1 Then
				num1 += 1
				If num2 > 0 Then
					num1 += 1
				End If
				If Me.m_nLe > 0 Then
					num1 += 1
				End If
			End If
			Dim buffer1 As Byte() = New Byte(num1 - 1) {}
			Dim num3 As Integer = 0
			buffer1(System.Math.Max(System.Threading.Interlocked.Increment(num3),num3 - 1)) = Me.m_nCLA
			buffer1(System.Math.Max(System.Threading.Interlocked.Increment(num3),num3 - 1)) = Me.m_nINS
			buffer1(System.Math.Max(System.Threading.Interlocked.Increment(num3),num3 - 1)) = Me.m_nP1
			buffer1(System.Math.Max(System.Threading.Interlocked.Increment(num3),num3 - 1)) = Me.m_nP2
			If flag1 Then
				buffer1(System.Math.Max(System.Threading.Interlocked.Increment(num3),num3 - 1)) = 0
			End If
			If num2 > 0 Then
				If flag1 Then
					buffer1(System.Math.Max(System.Threading.Interlocked.Increment(num3),num3 - 1)) = CByte((num2 >> 8) And &Hff)
				End If
				buffer1(System.Math.Max(System.Threading.Interlocked.Increment(num3),num3 - 1)) = CByte(num2 And &Hff)
				Buffer.BlockCopy(Me.m_vbData, 0, buffer1, num3, num2)
				num3 += num2
			End If
			If Me.m_nLe > 0 Then
				If flag1 Then
					buffer1(System.Math.Max(System.Threading.Interlocked.Increment(num3),num3 - 1)) = CByte((Me.m_nLe >> 8) And &Hff)
				End If
				buffer1(System.Math.Max(System.Threading.Interlocked.Increment(num3),num3 - 1)) = CByte(Me.m_nLe And &Hff)
			End If
			Return buffer1
		End Function

		Public Function GetData() As Byte()
			Return Me.m_vbData
		End Function

		Public Function GetInstructionName() As String
			Return CardCommand.GetInstructionName(Me.m_nINS)
		End Function

		Public Shared Function GetInstructionName(nINS As Byte) As String
			Dim num2 As Byte
			Dim num1 As Byte = nINS
			If num1 <= &Ha4 Then
				If num1 <= &H70 Then
					If num1 = 14 Then
						Return "ERASE BINARY"
					End If
					If num1 = &H20 Then
						Return "VERIFY"
					End If
					If num1 = &H70 Then
						Return "MANAGE CHANNEL"
					End If
				Else
					num2 = num1
					Select Case num2
						Case 130
							If True Then
								Return "EXTERNAL AUTHENTICATE"
							End If
						Case &H83, &H85, &H86, &H87
							If True Then
								GoTo Label_0163
							End If
						Case &H84
							If True Then
								Return "GET CHALLENGE"
							End If
						Case &H88
							If True Then
								Return "INTERNAL AUTHENTICATE"
							End If
						Case &Ha4
							If True Then
								Return "SELECT FILE"
							End If
					End Select
				End If
			ElseIf num1 <= &Hca Then
				num2 = num1
				Select Case num2
					Case &Hb0
						If True Then
							Return "READ BINARY"
						End If
					Case &Hb1, &Hc1
						If True Then
							GoTo Label_0163
						End If
					Case &Hb2
						If True Then
							Return "READ RECORD"
						End If
					Case &Hc0
						If True Then
							Return "GET RESPONSE"
						End If
					Case &Hc2
						If True Then
							Return "ENVELOPE"
						End If
					Case &Hca
						If True Then
							Return "GET DATA"
						End If
				End Select
			ElseIf num1 <= &Hd6 Then
				num2 = num1
				Select Case num2
					Case &Hd0
						If True Then
							Return "WRITE BINARY"
						End If
					Case &Hd1, &Hd3, &Hd4, &Hd5
						If True Then
							GoTo Label_0163
						End If
					Case 210
						If True Then
							Return "WRITE RECORD"
						End If
					Case &Hd6
						If True Then
							Return "UPDATE BINARY"
						End If
				End Select
			Else
				num2 = num1
				Select Case num2
					Case &Hda
						If True Then
							Return "PUT DATA"
						End If
					Case &Hdb
						If True Then
							GoTo Label_0163
						End If
					Case 220
						If True Then
							Return "UPDATE RECORD"
						End If
					Case &He2
						If True Then
							Return "APPEND RECORD"
						End If
				End Select
			End If
			Label_0163:
			Return Nothing
		End Function

		Public Shared Function ParseBytes(vbCommandAPDU As Byte()) As CardCommand
			If vbCommandAPDU Is Nothing Then
				Throw New ArgumentNullException()
			End If
			Return CardCommand.ParseBytes(vbCommandAPDU, 0, vbCommandAPDU.Length)
		End Function

		Public Shared Function ParseBytes(vbCommandAPDU As Byte(), nOffset As Integer, nLength As Integer) As CardCommand
			Dim num1 As Integer
			Dim num2 As Integer
			Dim buffer1 As Byte()
			If vbCommandAPDU Is Nothing Then
				Throw New ArgumentNullException()
			End If
			If ((nOffset < 0) OrElse (nLength < 0)) OrElse ((nOffset + nLength) > vbCommandAPDU.Length) Then
				Throw New ArgumentOutOfRangeException()
			End If
			If (nLength < 4) OrElse (nLength > &H10008) Then
				Throw New SCLibException(SCLibExceptionCode.InvalidAPDU)
			End If
			Dim num3 As Byte = vbCommandAPDU(System.Math.Max(System.Threading.Interlocked.Increment(nOffset),nOffset - 1))
			Dim num4 As Byte = vbCommandAPDU(System.Math.Max(System.Threading.Interlocked.Increment(nOffset),nOffset - 1))
			Dim num5 As Byte = vbCommandAPDU(System.Math.Max(System.Threading.Interlocked.Increment(nOffset),nOffset - 1))
			Dim num6 As Byte = vbCommandAPDU(System.Math.Max(System.Threading.Interlocked.Increment(nOffset),nOffset - 1))
			If nLength = 4 Then
				Return New CardCommand(num3, num4, num5, num6)
			End If
			Dim num7 As Integer = vbCommandAPDU(System.Math.Max(System.Threading.Interlocked.Increment(nOffset),nOffset - 1))
			If nLength = 5 Then
				num2 = If((num7 = 0), &H100, num7)
				Return New CardCommand(num3, num4, num5, num6, num2)
			End If
			If num7 = 0 Then
				If nLength < 7 Then
					Throw New SCLibException(SCLibExceptionCode.InvalidAPDU)
				End If
				num7 = (vbCommandAPDU(nOffset) << 8) + vbCommandAPDU(nOffset + 1)
				nOffset += 2
				If nLength = 7 Then
					num2 = If((num7 = 0), &H10000, num7)
					Return New CardCommand(num3, num4, num5, num6, num2)
				End If
				If (nLength < (7 + num7)) OrElse (num7 = 0) Then
					Throw New SCLibException(SCLibExceptionCode.InvalidAPDU)
				End If
				buffer1 = New Byte(num7 - 1) {}
				Buffer.BlockCopy(vbCommandAPDU, nOffset, buffer1, 0, num7)
				nOffset += num7
				If nLength = (7 + num7) Then
					Return New CardCommand(num3, num4, num5, num6, buffer1)
				End If
				If nLength <> ((7 + num7) + 2) Then
					GoTo Label_01AF
				End If
				num1 = (vbCommandAPDU(nOffset) << 8) + vbCommandAPDU(nOffset + 1)
				num2 = If((num1 = 0), &H10000, num1)
				Return New CardCommand(num3, num4, num5, num6, buffer1, num2)
			End If
			If nLength < (5 + num7) Then
				Throw New SCLibException(SCLibExceptionCode.InvalidAPDU)
			End If
			buffer1 = New Byte(num7 - 1) {}
			Buffer.BlockCopy(vbCommandAPDU, nOffset, buffer1, 0, num7)
			nOffset += num7
			If nLength = (5 + num7) Then
				Return New CardCommand(num3, num4, num5, num6, buffer1)
			End If
			If nLength = ((5 + num7) + 1) Then
				num1 = vbCommandAPDU(nOffset)
				num2 = If((num1 = 0), &H100, num1)
				Return New CardCommand(num3, num4, num5, num6, buffer1, num2)
			End If
			Label_01AF:
			Throw New SCLibException(SCLibExceptionCode.InvalidAPDU)
		End Function

		Public Sub SetData(vbData As Byte())
			If (vbData IsNot Nothing) AndAlso ((vbData.Length = 0) OrElse (vbData.Length > &Hffff)) Then
				Throw New ArgumentOutOfRangeException()
			End If
			Me.m_vbData = vbData
		End Sub

		Public Sub SetData(vbData As Byte(), nOffset As Integer, nLength As Integer)
			If vbData Is Nothing Then
				Throw New ArgumentNullException()
			End If
			If ((nOffset < 0) OrElse (nLength <= 0)) OrElse ((nLength > &Hffff) OrElse ((nOffset + nLength) > vbData.Length)) Then
				Throw New ArgumentOutOfRangeException()
			End If
			Me.m_vbData = New Byte(nLength - 1) {}
			Buffer.BlockCopy(vbData, nOffset, Me.m_vbData, 0, nLength)
		End Sub

		Public Overrides Function ToString() As String
			Dim builder1 As New StringBuilder(80)
			builder1.Append("CMD ")
			builder1.Append(SCUtil.FromByte(Me.m_nCLA))
			builder1.Append(SCUtil.FromByte(Me.m_nINS))
			builder1.Append(SCUtil.FromByte(Me.m_nP1))
			builder1.Append(SCUtil.FromByte(Me.m_nP2))
			Dim text1 As String = Me.GetInstructionName()
			If text1 IsNot Nothing Then
				builder1.Append(" (")
				builder1.Append(text1)
				builder1.Append(")"C)
			End If
			If Me.IsExtended Then
				builder1.Append(" 00")
			End If
			If Me.Lc > 0 Then
				If Me.IsExtended Then
					builder1.AppendFormat(" {0:X4} ", Me.Lc)
				Else
					builder1.AppendFormat(" {0:X2} ", Me.Lc And &Hff)
				End If
				If Me.m_nINS <> &H20 Then
					builder1.Append(SCUtil.FromByteArray(Me.m_vbData))
					builder1.Append(" ('")
					builder1.Append(Me.DumpBytes(Me.m_vbData, 0, Me.m_vbData.Length))
					builder1.Append("')")
				Else
					builder1.Append(SCUtil.ToDigit(Me.m_vbData(0) >> 4))
					builder1.Append("X"C, CInt((Me.Lc * 2) - 1))
				End If
			End If
			If Me.m_nLe > 0 Then
				If Me.IsExtended Then
					builder1.AppendFormat(" {0:X4} ", Me.m_nLe)
				Else
					builder1.AppendFormat(" {0:X2} ", Me.m_nLe And &Hff)
				End If
			End If
			Return builder1.ToString()
		End Function

		Public ReadOnly Property [Case]() As Integer
			Get
				Dim num1 As Integer = Me.Lc
				If (num1 = 0) AndAlso (Me.m_nLe = 0) Then
					Return 1
				End If
				If (num1 = 0) AndAlso (Me.m_nLe > 0) Then
					Return 2
				End If
				If (num1 > 0) AndAlso (Me.m_nLe = 0) Then
					Return 3
				End If
				Return 4
			End Get
		End Property

		Public Property CLA() As Byte
			Get
				Return Me.m_nCLA
			End Get
			Set
				Me.m_nCLA = value
			End Set
		End Property

		Public Property INS() As Byte
			Get
				Return Me.m_nINS
			End Get
			Set
				Me.m_nINS = value
			End Set
		End Property

		Public ReadOnly Property IsExtended() As Boolean
			Get
				If Me.Lc <= &Hff Then
					Return (Me.m_nLe > &H100)
				End If
				Return True
			End Get
		End Property

		Public ReadOnly Property Lc() As Integer
			Get
				If Me.m_vbData Is Nothing Then
					Return 0
				End If
				Return Me.m_vbData.Length
			End Get
		End Property

		Public Property Le() As Integer
			Get
				Return Me.m_nLe
			End Get
			Set
				Me.m_nLe = value
			End Set
		End Property

		Public Property P1() As Byte
			Get
				Return Me.m_nP1
			End Get
			Set
				Me.m_nP1 = value
			End Set
		End Property

		Public Property P2() As Byte
			Get
				Return Me.m_nP2
			End Get
			Set
				Me.m_nP2 = value
			End Set
		End Property




	End Class

End Namespace