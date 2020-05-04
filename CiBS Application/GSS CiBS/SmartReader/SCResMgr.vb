Imports System.Collections.Generic
Imports System.Text
Imports System.Collections
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Namespace SCLib
	Public NotInheritable Class SCResMgr
		Implements IDisposable

		Private m_fDisposed As Boolean
		Private m_hContext As IntPtr

		Public Sub New()
			Me.m_fDisposed = False
			Me.m_hContext = IntPtr.Zero
		End Sub

		Private Sub _Dispose(fDisposing As Boolean)
			If Not Me.m_fDisposed Then
				If Me.m_hContext <> IntPtr.Zero Then
					WinSCard.SCardCancel(Me.m_hContext)
					WinSCard.SCardReleaseContext(Me.m_hContext)
					Me.m_hContext = IntPtr.Zero
				End If
				Me.m_fDisposed = True
			End If
		End Sub
		Private Function _SplitMultiString(msz As String, aList As IList) As Integer
			Dim num1 As Integer
			Dim num2 As Integer = 0
			Dim num3 As Integer = 0
			While msz(num3) <> ControlChars.NullChar
				num1 = num3
				While msz(num1) <> ControlChars.NullChar
					num1 += 1
				End While
				aList.Add(msz.Substring(num3, num1 - num3))
				num2 += 1
				num3 = num1 + 1
			End While
			Return num2
		End Function

		Public Sub Cancel()
			If Me.m_hContext = IntPtr.Zero Then
				Throw New InvalidOperationException()
			End If
			Dim num1 As UInteger = WinSCard.SCardCancel(Me.m_hContext)
			If num1 <> 0 Then
				SCardException.RaiseWin32ResponseCode(num1)
			End If
		End Sub

		Public Sub Dispose() Implements IDisposable.Dispose
			Me._Dispose(True)
			GC.SuppressFinalize(Me)
		End Sub

		Public Function EstablishContext(nScope As SCardContextScope) As Boolean
			If Me.m_fDisposed Then
				Throw New ObjectDisposedException(MyBase.[GetType]().FullName)
			End If
			If Me.m_hContext <> IntPtr.Zero Then
				Throw New InvalidOperationException()
			End If
			Dim num1 As UInteger = WinSCard.SCardEstablishContext(CUInt(nScope), IntPtr.Zero, IntPtr.Zero, Me.m_hContext)
			If num1 = &H8010001dUI Then
				MessageBox.Show("The ensure that SCardSvr Service is running. PC/SC routines won't be available.")
				Return False
			End If
			If num1 <> 0 Then
				MessageBox.Show(String.Format("SCardEstablishContext failed: 0x{0:X8}", num1))
				SCardException.RaiseWin32ResponseCode(num1)
			End If
			Return True
		End Function

		Protected Overrides Sub Finalize()
			Try
				Me._Dispose(False)
			Finally
				MyBase.Finalize()
			End Try
		End Sub

		Public Function GetStatusChange(nTimeout As Integer, ByRef aSCInfo As SCStateInfo) As Boolean
			Dim infoArray2 As SCStateInfo() = New SCStateInfo(0) {aSCInfo}
			Dim infoArray1 As SCStateInfo() = infoArray2
			Dim flag1 As Boolean = Me.GetStatusChange(nTimeout, infoArray1)
			aSCInfo = infoArray1(0)
			Return flag1
		End Function

		Public Function GetStatusChange(nTimeout As Integer, vaSCStates As SCStateInfo()) As Boolean
			Dim num1 As Integer = vaSCStates.Length
			Dim scard_readerstateArray1 As WinSCard.SCARD_READERSTATE() = New WinSCard.SCARD_READERSTATE(num1 - 1) {}
			For num2 As Integer = 0 To num1 - 1
				scard_readerstateArray1(num2).szReader = vaSCStates(num2).sReaderName
				scard_readerstateArray1(num2).pvUserData = IntPtr.Zero
				scard_readerstateArray1(num2).dwCurrentState = CUInt(vaSCStates(num2).nCurrentState)
				scard_readerstateArray1(num2).dwEventState = 0
				scard_readerstateArray1(num2).cbAtr = 0
				scard_readerstateArray1(num2).rgbAtr = Nothing
			Next
			Dim num3 As UInteger = WinSCard.SCardGetStatusChange(Me.m_hContext, CUInt(nTimeout), scard_readerstateArray1, CUInt(scard_readerstateArray1.Length))
			If num3 = 0 Then
				For num4 As Integer = 0 To num1 - 1
                    vaSCStates(num4).nEventState = DirectCast(CInt(scard_readerstateArray1(num4).dwEventState), SCStates)
					If (scard_readerstateArray1(num4).cbAtr > 0) AndAlso (scard_readerstateArray1(num4).rgbAtr IsNot Nothing) Then
						vaSCStates(num4).vbATR = New Byte(scard_readerstateArray1(num4).cbAtr - 1) {}
						Buffer.BlockCopy(scard_readerstateArray1(num4).rgbAtr, 0, vaSCStates(num4).vbATR, 0, CInt(scard_readerstateArray1(num4).cbAtr))
					Else
						vaSCStates(num4).vbATR = Nothing
					End If
				Next
				Return True
			End If
			If (num3 = &H8010000aUI) OrElse (num3 = &H80100002UI) Then
				Return False
			End If
			SCardException.RaiseWin32ResponseCode(num3)
			Throw New InvalidProgramException()
		End Function

		Public Function GetStatusChange(nTimeout As Integer, sReaderName As String, nCurrentState As SCStates, ByRef nEventState As SCStates) As Boolean
			Dim info1 As SCStateInfo
			info1.sReaderName = sReaderName
			info1.nCurrentState = nCurrentState
			info1.nEventState = SCStates.Unaware
			info1.vbATR = Nothing
			Dim flag1 As Boolean = Me.GetStatusChange(nTimeout, info1)
			nEventState = info1.nEventState
			Return flag1
		End Function

		Public Function IsValidContext() As Boolean
			If Me.m_fDisposed Then
				Throw New ObjectDisposedException(MyBase.[GetType]().FullName)
			End If
			If Me.m_hContext = IntPtr.Zero Then
				Throw New InvalidOperationException()
			End If
			Dim num1 As UInteger = WinSCard.SCardIsValidContext(Me.m_hContext)
			If num1 = 0 Then
				Return True
			End If
			Return False
		End Function

		Public Function ListReaderGroups(aReaderGroupsList As IList) As Integer
			If Me.m_fDisposed Then
				Throw New ObjectDisposedException(MyBase.[GetType]().FullName)
			End If
			If Me.m_hContext = IntPtr.Zero Then
				Throw New InvalidOperationException()
			End If
			Dim num1 As Integer = 0
			Dim ptr1 As IntPtr = IntPtr.Zero
			Dim num2 As UInteger = UInteger.MaxValue
			Dim num3 As UInteger = WinSCard.SCardListReaderGroups(Me.m_hContext, ptr1, num2)
			If num3 = 0 Then
				Dim text1 As String = Marshal.PtrToStringAuto(ptr1, CInt(num2))
				num1 = Me._SplitMultiString(text1, aReaderGroupsList)
				num3 = WinSCard.SCardFreeMemory(Me.m_hContext, ptr1)
				Return num1
			End If
			If num3 = 2148532270UI Then
				Return 0
			End If
			SCardException.RaiseWin32ResponseCode(num3)
			Return num1
		End Function

		Public Function ListReaders(aReadersList As IList) As Integer
			Return Me.ListReaders(DirectCast(Nothing, String()), aReadersList)
		End Function

		Public Function ListReaders(vsGroups As String(), aReadersList As IList) As Integer
			If Me.m_fDisposed Then
				Throw New ObjectDisposedException(MyBase.[GetType]().FullName)
			End If
			If Me.m_hContext = IntPtr.Zero Then
				Throw New InvalidOperationException()
			End If
			Dim num1 As Integer = 0
			Dim text1 As String = Nothing
			If (vsGroups IsNot Nothing) AndAlso (vsGroups.Length > 0) Then
				Dim textArray1 As String() = vsGroups
				For num2 As Integer = 0 To textArray1.Length - 1
					Dim text2 As String = textArray1(num2)
					text1 = text1 & text2
					text1 = text1 & ControlChars.NullChar
				Next
				text1 = text1 & ControlChars.NullChar
			End If
			Dim ptr1 As IntPtr = IntPtr.Zero
			Dim num3 As UInteger = UInteger.MaxValue
			Dim num4 As UInteger = WinSCard.SCardListReaders(Me.m_hContext, text1, ptr1, num3)
			If num4 = 0 Then
				Dim text3 As String = Marshal.PtrToStringAuto(ptr1, CInt(num3))
				num1 = Me._SplitMultiString(text3, aReadersList)
				num4 = WinSCard.SCardFreeMemory(Me.m_hContext, ptr1)
				Return num1
			End If
			If num4 = 2148532270UI Then
				Return 0
			End If
			SCardException.RaiseWin32ResponseCode(num4)
			Return num1
		End Function

		Public Function ListReaders(sGroup As String, aReadersList As IList) As Integer
			If (sGroup Is Nothing) OrElse (sGroup.Length = 0) Then
				Throw New ArgumentNullException()
			End If
			Dim textArray2 As String() = New String(0) {sGroup}
			Dim textArray1 As String() = textArray2
			Return Me.ListReaders(textArray1, aReadersList)
		End Function

		Public Sub ReleaseContext()
			If Me.m_fDisposed Then
				Throw New ObjectDisposedException(MyBase.[GetType]().FullName)
			End If
			If Me.m_hContext = IntPtr.Zero Then
				Throw New InvalidOperationException()
			End If
			Dim num1 As UInteger = WinSCard.SCardReleaseContext(Me.m_hContext)
			Me.m_hContext = IntPtr.Zero
			If num1 <> 0 Then
				SCardException.RaiseWin32ResponseCode(num1)
			End If
		End Sub

		Public ReadOnly Property Context() As IntPtr
			Get
				Return Me.m_hContext
			End Get
		End Property






	End Class






End Namespace