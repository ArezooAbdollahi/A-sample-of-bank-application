Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Collections
Imports System.Windows.Forms

Namespace SCLib
	Public NotInheritable Class SCReader
		Implements IDisposable

		Private m_aResourceManager As SCResMgr
		Private m_aVendorIfdVersion As Version
		Private m_dwActiveProtocol As UInteger
		Private m_dwChannelID As UInteger
		Private m_dwMechanicalCharacteristics As UInteger
		Private m_fDisposed As Boolean
		Private m_fInTransaction As Boolean
		Private m_fIsConnected As Boolean
		Private m_hCard As IntPtr
		Private m_sVendorIfdSerialNo As String
		Private m_sVendorIfdType As String
		Private m_sVendorName As String




		Public Sub New(aResourceManager As SCResMgr)
			Me.m_fDisposed = False
			Me.m_hCard = IntPtr.Zero
			Me.m_dwActiveProtocol = 0
			Me.m_fIsConnected = False
			Me.m_fInTransaction = False
			Me.m_sVendorName = Nothing
			Me.m_sVendorIfdType = Nothing
			Me.m_aVendorIfdVersion = Nothing
			Me.m_sVendorIfdSerialNo = Nothing
			Me.m_dwChannelID = 0
			Me.m_dwMechanicalCharacteristics = 0
			If aResourceManager Is Nothing Then
				Throw New ArgumentNullException()
			End If
			Me.m_aResourceManager = aResourceManager
		End Sub

		Private Sub _Dispose(fDisposing As Boolean)
			If Not Me.m_fDisposed Then
				If Me.m_hCard <> IntPtr.Zero Then
					If Me.m_fInTransaction Then
						WinSCard.SCardEndTransaction(Me.m_hCard, 0)
						Me.m_fInTransaction = False
					End If
					If Me.m_fIsConnected Then
						WinSCard.SCardDisconnect(Me.m_hCard, 3)
						Me.m_fIsConnected = False
					End If
					Me.m_hCard = IntPtr.Zero
				End If
				Me.m_aResourceManager = Nothing
				Me.m_dwActiveProtocol = 0
				Me.m_sVendorName = Nothing
				Me.m_sVendorIfdType = Nothing
				Me.m_aVendorIfdVersion = Nothing
				Me.m_sVendorIfdSerialNo = Nothing
				Me.m_dwChannelID = 0
				Me.m_dwMechanicalCharacteristics = 0
				Me.m_fDisposed = True
			End If
		End Sub

		Public Sub BeginTransaction()
			If Me.m_fDisposed Then
				Throw New ObjectDisposedException(MyBase.[GetType]().FullName)
			End If
			If (Me.m_hCard = IntPtr.Zero) OrElse Not Me.m_fIsConnected Then
				Throw New InvalidOperationException()
			End If
			Dim num1 As UInteger = WinSCard.SCardBeginTransaction(Me.m_hCard)
			If num1 <> 0 Then
				SCardException.RaiseWin32ResponseCode(num1)
			End If
			Me.m_fInTransaction = True
		End Sub

		Public Function Connect(sReader As String, nAccessMode As SCardAccessMode, nPreferredProtocols As SCardProtocolIdentifiers) As Boolean
			If Me.m_fDisposed Then
				Throw New ObjectDisposedException(MyBase.[GetType]().FullName)
			End If
			If ((Me.m_hCard <> IntPtr.Zero) OrElse (Me.m_aResourceManager.Context = IntPtr.Zero)) OrElse Me.m_fIsConnected Then
				Throw New InvalidOperationException()
			End If
			Dim num1 As UInteger = CUInt(nAccessMode)
			Dim num2 As UInteger = CUInt(nPreferredProtocols)
			Dim num3 As UInteger = WinSCard.SCardConnect(Me.m_aResourceManager.Context, sReader, num1, num2, Me.m_hCard, Me.m_dwActiveProtocol)
			If num3 = 0 Then
				Me.m_fIsConnected = True
				Return True
			End If
			If num3 = &H80100069UI Then
				Return False
			End If
			SCardException.RaiseWin32ResponseCode(num3)
			Throw New InvalidProgramException()
		End Function

		Public Sub Control(nControlCode As Integer, vbInBuffer As Byte(), ByRef vbOutBuffer As Byte())
			If Me.m_fDisposed Then
				Throw New ObjectDisposedException(MyBase.[GetType]().FullName)
			End If
			If (Me.m_hCard = IntPtr.Zero) OrElse Not Me.m_fIsConnected Then
				Throw New InvalidOperationException()
			End If
			Dim buffer1 As Byte() = New Byte(65537) {}
			Dim num3 As Integer = buffer1.Length
			Dim num1 As UInteger = 0
			Dim num2 As UInteger = WinSCard.SCardControl(Me.m_hCard, CUInt(nControlCode), vbInBuffer, CUInt(vbInBuffer.Length), buffer1, CUInt(buffer1.Length), _
				num1)
			If num2 = 0 Then
				If num1 > 0 Then
					vbOutBuffer = New Byte(num1 - 1) {}
					Buffer.BlockCopy(buffer1, 0, vbOutBuffer, 0, CInt(num1))
				Else
					vbOutBuffer = Nothing
				End If
			Else
				SCardException.RaiseWin32ResponseCode(num2)
				Throw New InvalidProgramException()
			End If
		End Sub

		Public Sub Disconnect(nDisposition As SCardDisposition)
			If Me.m_fDisposed Then
				Throw New ObjectDisposedException(MyBase.[GetType]().FullName)
			End If
			If (Me.m_hCard = IntPtr.Zero) OrElse Not Me.m_fIsConnected Then
				Throw New InvalidOperationException()
			End If
			Dim num1 As UInteger = WinSCard.SCardDisconnect(Me.m_hCard, CUInt(nDisposition))
			Me.m_hCard = IntPtr.Zero
			Me.m_dwActiveProtocol = 0
			Me.m_fIsConnected = False
			Me.m_fInTransaction = False
			If num1 <> 0 Then
				SCardException.RaiseWin32ResponseCode(num1)
			End If
		End Sub

		Public Sub Dispose() Implements IDisposable.Dispose
			Me._Dispose(True)
			GC.SuppressFinalize(Me)
		End Sub

		Public Sub EndTransaction(nDisposition As SCardDisposition)
			If Me.m_fDisposed Then
				Throw New ObjectDisposedException(MyBase.[GetType]().FullName)
			End If
			If (Me.m_hCard = IntPtr.Zero) OrElse Not Me.m_fIsConnected Then
				Throw New InvalidOperationException()
			End If
			If Not Me.m_fInTransaction Then
				Throw New InvalidOperationException()
			End If
			Dim num1 As UInteger = WinSCard.SCardEndTransaction(Me.m_hCard, CUInt(nDisposition))
			If num1 <> 0 Then
				SCardException.RaiseWin32ResponseCode(num1)
			End If
			Me.m_fInTransaction = False
		End Sub

		Protected Overrides Sub Finalize()
			Try
				Me._Dispose(False)
			Finally
				MyBase.Finalize()
			End Try
		End Sub

		Public Function GetReaderCapabilities(nTag As SCardReaderCapability, ByRef vbValue As Byte()) As Boolean
			If Me.m_fDisposed Then
				Throw New ObjectDisposedException(MyBase.[GetType]().FullName)
			End If
			If (Me.m_hCard = IntPtr.Zero) OrElse Not Me.m_fIsConnected Then
				Throw New InvalidOperationException()
			End If
			Dim ptr1 As IntPtr = IntPtr.Zero
			Dim num1 As UInteger = UInteger.MaxValue
			Dim num2 As UInteger = WinSCard.SCardGetAttrib(Me.m_hCard, CUInt(nTag), ptr1, num1)
			If num2 = 0 Then
				vbValue = New Byte(num1 - 1) {}
				Marshal.Copy(ptr1, vbValue, 0, CInt(num1))
				num2 = WinSCard.SCardFreeMemory(Me.m_aResourceManager.Context, ptr1)
				Return True
			End If
			If (num2 = 50) OrElse (num2 = &H16) Then
				vbValue = Nothing
				Return False
			End If
			SCardException.RaiseWin32ResponseCode(num2)
			Throw New InvalidProgramException()
		End Function

		Public Function GetReaderCapabilitiesByte(nTag As SCardReaderCapability) As Integer
			Dim buffer1 As Byte()
			If Not Me.GetReaderCapabilities(nTag, buffer1) Then
				Return -1
			End If
			If buffer1.Length < 1 Then
				Throw New SCardException(DirectCast(0, SCardResponseCode))
			End If
			Return buffer1(0)
		End Function

		Public Function GetReaderCapabilitiesInteger(nTag As SCardReaderCapability, nDefaultValue As Integer) As Integer
			Dim buffer1 As Byte()
			If Not Me.GetReaderCapabilities(nTag, buffer1) Then
				Return nDefaultValue
			End If
			If buffer1.Length < 4 Then
				Throw New SCardException(DirectCast(0, SCardResponseCode))
			End If
			Return BitConverter.ToInt32(buffer1, 0)
		End Function

		Public Function GetReaderCapabilitiesString(nTag As SCardReaderCapability) As String
			Dim buffer1 As Byte()
			If Not Me.GetReaderCapabilities(nTag, buffer1) Then
				Return Nothing
			End If
			Dim num1 As Integer = buffer1.Length
			If (num1 > 0) AndAlso (buffer1(num1 - 1) = 0) Then
				num1 -= 1
			End If
			Return Encoding.GetEncoding(&H4e4).GetString(buffer1, 0, num1)
		End Function

		Public Function Reconnect(nAccessMode As SCardAccessMode, nPreferredProtocols As SCardProtocolIdentifiers, nInitialization As SCardDisposition) As Boolean
			If Me.m_fDisposed Then
				Throw New ObjectDisposedException(MyBase.[GetType]().FullName)
			End If
			If (Me.m_hCard = IntPtr.Zero) OrElse Not Me.m_fIsConnected Then
				Throw New InvalidOperationException()
			End If
			If nInitialization = SCardDisposition.EjectCard Then
				Throw New ArgumentException()
			End If
			Me.m_dwActiveProtocol = 0
			Dim num1 As UInteger = WinSCard.SCardReconnect(Me.m_hCard, CUInt(nAccessMode), CUInt(nPreferredProtocols), CUInt(nInitialization), Me.m_dwActiveProtocol)
			If num1 = 0 Then
				Return True
			End If
			If num1 = &H80100069UI Then
				Me.m_fIsConnected = False
				Return False
			End If
			SCardException.RaiseWin32ResponseCode(num1)
			Throw New InvalidProgramException()
		End Function

		Public Shared Function SCardCtlCode(nFunctionCode As Integer) As Integer
			Return (&H310000 Or (nFunctionCode << 2))
		End Function

		

		Public Function SetReaderCapabilities(nTag As SCardReaderCapability, vbValue As Byte()) As Boolean
			If Me.m_fDisposed Then
				Throw New ObjectDisposedException(MyBase.[GetType]().FullName)
			End If
			If (Me.m_hCard = IntPtr.Zero) OrElse Not Me.m_fIsConnected Then
				Throw New InvalidOperationException()
			End If
			Dim num1 As UInteger = WinSCard.SCardSetAttrib(Me.m_hCard, CUInt(nTag), vbValue, CUInt(vbValue.Length))
			If num1 = 0 Then
				Return True
			End If
			If num1 = 50 Then
				Return False
			End If
			SCardException.RaiseWin32ResponseCode(num1)
			Throw New InvalidProgramException()
		End Function

		Public Function Status() As SCardReaderState
			Dim num1 As UInteger
			Dim num2 As UInteger
			If Me.m_fDisposed Then
				Throw New ObjectDisposedException(MyBase.[GetType]().FullName)
			End If
			If Me.m_hCard = IntPtr.Zero Then
				Throw New InvalidOperationException()
			End If
			Dim builder1 As New StringBuilder(&H100)
			Dim num3 As UInteger = CUInt(builder1.Capacity)
			Dim buffer1 As Byte() = New Byte(31) {}
			Dim num4 As UInteger = CUInt(buffer1.Length)
			Dim num5 As UInteger = WinSCard.SCardStatus(Me.m_hCard, builder1, num3, num1, num2, buffer1, _
				num4)
			If num5 <> 0 Then
				SCardException.RaiseWin32ResponseCode(num5)
			End If
            Return DirectCast(CInt(num1), SCardReaderState)
		End Function

		Public Sub Transmit(vbSendBuffer As Byte(), ByRef vbRecvBuffer As Byte())
			If Me.m_fDisposed Then
				Throw New ObjectDisposedException(MyBase.[GetType]().FullName)
			End If
			If ((Me.m_hCard = IntPtr.Zero) OrElse Not Me.m_fIsConnected) OrElse (Me.m_dwActiveProtocol = 0) Then
				Throw New InvalidOperationException()
			End If
			Dim scard_io_request1 As New WinSCard.SCARD_IO_REQUEST()
			scard_io_request1.dwProtocol = Me.m_dwActiveProtocol
			scard_io_request1.cbPciLength = CUInt(Marshal.SizeOf(scard_io_request1))
			Dim buffer1 As Byte() = New Byte(65537) {}
			Dim num1 As UInteger = CUInt(buffer1.Length)
			Dim num2 As UInteger = WinSCard.SCardTransmit(Me.m_hCard, scard_io_request1, vbSendBuffer, CUInt(vbSendBuffer.Length), Nothing, buffer1, _
				num1)
			If num2 = 0 Then
				vbRecvBuffer = New Byte(num1 - 1) {}
				Buffer.BlockCopy(buffer1, 0, vbRecvBuffer, 0, CInt(num1))
			Else
				SCardException.RaiseWin32ResponseCode(num2)
				Throw New InvalidProgramException()
			End If
		End Sub

		Public ReadOnly Property ActiveProtocol() As SCardProtocolIdentifiers
			Get
                Return DirectCast(CInt(Me.m_dwActiveProtocol), SCardProtocolIdentifiers)
			End Get
		End Property

		Public ReadOnly Property ChannelNumber() As Integer
			Get
				If Me.m_dwChannelID = 0 Then
					Me.m_dwChannelID = CUInt(Me.GetReaderCapabilitiesInteger(SCardReaderCapability.ChannelID, 0))
				End If
				Return (CInt(Me.m_dwChannelID) And &Hffff)
			End Get
		End Property

		Public ReadOnly Property ChannelType() As SCardChannelType
			Get
				If Me.m_dwChannelID = 0 Then
					Me.m_dwChannelID = CUInt(Me.GetReaderCapabilitiesInteger(SCardReaderCapability.ChannelID, 0))
				End If
                Return DirectCast(CInt(Me.m_dwChannelID) >> &H10, SCardChannelType)
			End Get
		End Property

		Public ReadOnly Property InTransaction() As Boolean
			Get
				Return Me.m_fInTransaction
			End Get
		End Property

		Public ReadOnly Property IsConnected() As Boolean
			Get
				Return Me.m_fIsConnected
			End Get
		End Property

		Public ReadOnly Property MechanicalCharacteristics() As SCardMechanicalCharacteristics
			Get
				If Me.m_dwMechanicalCharacteristics = 0 Then
					Me.m_dwMechanicalCharacteristics = CUInt(Me.GetReaderCapabilitiesInteger(SCardReaderCapability.Characteristics, 0))
				End If
                Return DirectCast(CInt(Me.m_dwMechanicalCharacteristics), SCardMechanicalCharacteristics)
			End Get
		End Property

		Public ReadOnly Property VendorIFDSerialNo() As String
			Get
				If Me.m_sVendorIfdSerialNo Is Nothing Then
					Me.m_sVendorIfdSerialNo = Me.GetReaderCapabilitiesString(SCardReaderCapability.VendorIFDSerialNo)
				End If
				Return Me.m_sVendorIfdSerialNo
			End Get
		End Property

		Public ReadOnly Property VendorIFDType() As String
			Get
				If Me.m_sVendorIfdType Is Nothing Then
					Me.m_sVendorIfdType = Me.GetReaderCapabilitiesString(SCardReaderCapability.VendorIFDType)
				End If
				Return Me.m_sVendorIfdType
			End Get
		End Property

		Public ReadOnly Property VendorIFDVersion() As Version
			Get
				If Me.m_aVendorIfdVersion Is Nothing Then
					Dim num1 As Integer = Me.GetReaderCapabilitiesInteger(SCardReaderCapability.VendorIFDVersion, -1)
					If num1 > 0 Then
						Dim num2 As Integer = (num1 >> &H18) And &Hff
						Dim num3 As Integer = (num1 >> &H10) And &Hff
						Dim num4 As Integer = num1 And &Hffff
						Me.m_aVendorIfdVersion = New Version(num2, num3, num4)
					End If
				End If
				Return Me.m_aVendorIfdVersion
			End Get
		End Property

		Public ReadOnly Property VendorName() As String
			Get
				If Me.m_sVendorName Is Nothing Then
					Me.m_sVendorName = Me.GetReaderCapabilitiesString(SCardReaderCapability.VendorName)
				End If
				Return Me.m_sVendorName
			End Get
		End Property














	End Class


End Namespace