Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices

Namespace SCLib
	Friend NotInheritable Class WinSCard
		<StructLayout(LayoutKind.Sequential)> _
		Friend Class SCARD_IO_REQUEST
			Friend dwProtocol As UInteger
			Friend cbPciLength As UInteger
			Public Sub New()
				Me.dwProtocol = 0
			End Sub


		End Class

		<StructLayout(LayoutKind.Sequential, CharSet := CharSet.Auto)> _
		Friend Structure SCARD_READERSTATE
			Friend szReader As String
			Friend pvUserData As IntPtr
			Friend dwCurrentState As UInteger
			Friend dwEventState As UInteger
			Friend cbAtr As UInteger
			<MarshalAs(UnmanagedType.ByValArray, SizeConst := &H24, ArraySubType := UnmanagedType.U1)> _
			Friend rgbAtr As Byte()
		End Structure



		Public Sub New()
		End Sub


		<DllImport("WINSCARD.DLL")> _
		Friend Shared Function SCardBeginTransaction(hCard As IntPtr) As UInteger
		End Function
		<DllImport("WINSCARD.DLL")> _
		Friend Shared Function SCardCancel(hContext As IntPtr) As UInteger
		End Function
		<DllImport("WINSCARD.DLL", CharSet := CharSet.Auto)> _
		Friend Shared Function SCardConnect(hContext As IntPtr, szReader As String, dwShareMode As UInteger, dwPreferredProtocols As UInteger, ByRef phCard As IntPtr, ByRef pdwActiveProtocol As UInteger) As UInteger
		End Function
		<DllImport("WINSCARD.DLL")> _
		Friend Shared Function SCardControl(hCard As IntPtr, dwControlCode As UInteger, <[In]> lpInBuffer As Byte(), nInBufferSize As UInteger, <[In], Out> lpOutBuffer As Byte(), nOutBufferSize As UInteger, _
			ByRef lpBytesReturned As UInteger) As UInteger
		End Function
		<DllImport("WINSCARD.DLL")> _
		Friend Shared Function SCardDisconnect(hCard As IntPtr, dwDisposition As UInteger) As UInteger
		End Function
		<DllImport("WINSCARD.DLL")> _
		Friend Shared Function SCardEndTransaction(hCard As IntPtr, dwDisposition As UInteger) As UInteger
		End Function
		<DllImport("WINSCARD.DLL")> _
		Friend Shared Function SCardEstablishContext(dwScope As UInteger, pvReserved1 As IntPtr, pvReserved2 As IntPtr, ByRef phContext As IntPtr) As UInteger
		End Function
		<DllImport("WINSCARD.DLL")> _
		Friend Shared Function SCardFreeMemory(hContext As IntPtr, pvMem As IntPtr) As UInteger
		End Function
		<DllImport("WINSCARD.DLL")> _
		Friend Shared Function SCardGetAttrib(hCard As IntPtr, dwAttrId As UInteger, ByRef pbAttr As IntPtr, ByRef pcbAttrLen As UInteger) As UInteger
		End Function
		<DllImport("WINSCARD.DLL", CharSet := CharSet.Auto)> _
		Friend Shared Function SCardGetStatusChange(hContext As IntPtr, dwTimeout As UInteger, <[In], Out> rgReaderStates As SCARD_READERSTATE(), cReaders As UInteger) As UInteger
		End Function
		<DllImport("WINSCARD.DLL")> _
		Friend Shared Function SCardIsValidContext(hContext As IntPtr) As UInteger
		End Function
		<DllImport("WINSCARD.DLL", CharSet := CharSet.Auto)> _
		Friend Shared Function SCardListReaderGroups(hContext As IntPtr, ByRef pmszGroups As IntPtr, ByRef pcchGroups As UInteger) As UInteger
		End Function
		<DllImport("WINSCARD.DLL", CharSet := CharSet.Auto)> _
		Friend Shared Function SCardListReaders(hContext As IntPtr, mszGroups As String, ByRef pmszReaders As IntPtr, ByRef pcchReaders As UInteger) As UInteger
		End Function
		<DllImport("WINSCARD.DLL")> _
		Friend Shared Function SCardReconnect(hCard As IntPtr, dwShareMode As UInteger, dwPreferredProtocols As UInteger, dwInitialization As UInteger, ByRef pdwActiveProtocol As UInteger) As UInteger
		End Function
		<DllImport("WINSCARD.DLL")> _
		Friend Shared Function SCardReleaseContext(hContext As IntPtr) As UInteger
		End Function
		<DllImport("WINSCARD.DLL")> _
		Friend Shared Function SCardSetAttrib(hCard As IntPtr, dwAttrId As UInteger, <[In]> pbAttr As Byte(), cbAttrLen As UInteger) As UInteger
		End Function
		<DllImport("WINSCARD.DLL", CharSet := CharSet.Auto)> _
		Friend Shared Function SCardStatus(hCard As IntPtr, szReaderName As StringBuilder, ByRef pcchReaderLen As UInteger, ByRef pdwState As UInteger, ByRef pdwProtocol As UInteger, <Out> pbAtr As Byte(), _
			ByRef pcbAtrLen As UInteger) As UInteger
		End Function
		<DllImport("WINSCARD.DLL")> _
		Friend Shared Function SCardTransmit(hCard As IntPtr, pioSendPci As SCARD_IO_REQUEST, <[In]> pbSendBuffer As Byte(), cbSendLength As UInteger, pioRecvPci As SCARD_IO_REQUEST, <[In], Out> pbRecvBuffer As Byte(), _
			ByRef pcbRecvLength As UInteger) As UInteger
		End Function

		' Fields
		Friend Const IOCTL_SMARTCARD_CONFISCATE As UInteger = 3211280
		Friend Const IOCTL_SMARTCARD_EJECT As UInteger = &H310018
		Friend Const IOCTL_SMARTCARD_GET_ATTRIBUTE As UInteger = &H310008
		Friend Const IOCTL_SMARTCARD_GET_LAST_ERROR As UInteger = &H31003c
		Friend Const IOCTL_SMARTCARD_GET_PERF_CNTR As UInteger = &H310040
		Friend Const IOCTL_SMARTCARD_GET_STATE As UInteger = 3211320
		Friend Const IOCTL_SMARTCARD_IS_ABSENT As UInteger = &H31002c
		Friend Const IOCTL_SMARTCARD_IS_PRESENT As UInteger = &H310028
		Friend Const IOCTL_SMARTCARD_POWER As UInteger = &H310004
		Friend Const IOCTL_SMARTCARD_READ As UInteger = &H310020
		Friend Const IOCTL_SMARTCARD_SET_ATTRIBUTE As UInteger = &H31000c
		Friend Const IOCTL_SMARTCARD_SET_PROTOCOL As UInteger = &H310030
		Friend Const IOCTL_SMARTCARD_SWALLOW As UInteger = &H31001c
		Friend Const IOCTL_SMARTCARD_TRANSMIT As UInteger = &H310014
		Friend Const IOCTL_SMARTCARD_WRITE As UInteger = 3211300
		Friend Const MAXIMUM_ATTR_STRING_LENGTH As UInteger = &H20
		Friend Const SCARD_ABSENT As UInteger = 1
		Friend Const SCARD_ALL_READERS As String = "SCard$AllReaders"
		Friend Const SCARD_ATTR_ATR_STRING As UInteger = &H90303
		Friend Const SCARD_ATTR_CHANNEL_ID As UInteger = &H20110
		Friend Const SCARD_ATTR_CHARACTERISTICS As UInteger = &H60150
		Friend Const SCARD_ATTR_CURRENT_BWT As UInteger = &H80209
		Friend Const SCARD_ATTR_CURRENT_CLK As UInteger = &H80202
		Friend Const SCARD_ATTR_CURRENT_CWT As UInteger = 524810
		Friend Const SCARD_ATTR_CURRENT_D As UInteger = &H80204
		Friend Const SCARD_ATTR_CURRENT_EBC_ENCODING As UInteger = &H8020b
		Friend Const SCARD_ATTR_CURRENT_F As UInteger = &H80203
		Friend Const SCARD_ATTR_CURRENT_IFSC As UInteger = &H80207
		Friend Const SCARD_ATTR_CURRENT_IFSD As UInteger = &H80208
		Friend Const SCARD_ATTR_CURRENT_IO_STATE As UInteger = &H90302
		Friend Const SCARD_ATTR_CURRENT_N As UInteger = &H80205
		Friend Const SCARD_ATTR_CURRENT_PROTOCOL_TYPE As UInteger = &H80201
		Friend Const SCARD_ATTR_CURRENT_W As UInteger = &H80206
		Friend Const SCARD_ATTR_DEFAULT_CLK As UInteger = &H30121
		Friend Const SCARD_ATTR_DEFAULT_DATA_RATE As UInteger = &H30123
		Friend Const SCARD_ATTR_DEVICE_FRIENDLY_NAME As UInteger = &H7fff0003
		Friend Const SCARD_ATTR_DEVICE_FRIENDLY_NAME_A As UInteger = &H7fff0003
		Friend Const SCARD_ATTR_DEVICE_FRIENDLY_NAME_W As UInteger = &H7fff0005
		Friend Const SCARD_ATTR_DEVICE_IN_USE As UInteger = &H7fff0002
		Friend Const SCARD_ATTR_DEVICE_SYSTEM_NAME As UInteger = &H7fff0004
		Friend Const SCARD_ATTR_DEVICE_SYSTEM_NAME_A As UInteger = &H7fff0004
		Friend Const SCARD_ATTR_DEVICE_SYSTEM_NAME_W As UInteger = &H7fff0006
		Friend Const SCARD_ATTR_DEVICE_UNIT As UInteger = &H7fff0001
		Friend Const SCARD_ATTR_ESC_AUTHREQUEST As UInteger = &H7a005
		Friend Const SCARD_ATTR_ESC_CANCEL As UInteger = &H7a003
		Friend Const SCARD_ATTR_ESC_RESET As UInteger = &H7a000
		Friend Const SCARD_ATTR_EXTENDED_BWT As UInteger = &H8020c
		Friend Const SCARD_ATTR_ICC_INTERFACE_STATUS As UInteger = &H90301
		Friend Const SCARD_ATTR_ICC_PRESENCE As UInteger = &H90300
		Friend Const SCARD_ATTR_ICC_TYPE_PER_ATR As UInteger = &H90304
		Friend Const SCARD_ATTR_MAX_CLK As UInteger = &H30122
		Friend Const SCARD_ATTR_MAX_DATA_RATE As UInteger = 196900
		Friend Const SCARD_ATTR_MAX_IFSD As UInteger = &H30125
		Friend Const SCARD_ATTR_MAXINPUT As UInteger = &H7a007
		Friend Const SCARD_ATTR_POWER_MGMT_SUPPORT As UInteger = &H40131
		Friend Const SCARD_ATTR_PROTOCOL_TYPES As UInteger = &H30120
		Friend Const SCARD_ATTR_SUPRESS_T1_IFS_REQUEST As UInteger = &H7fff0007
		Friend Const SCARD_ATTR_USER_AUTH_INPUT_DEVICE As UInteger = &H50142
		Friend Const SCARD_ATTR_USER_TO_CARD_AUTH_DEVICE As UInteger = 328000
		Friend Const SCARD_ATTR_VENDOR_IFD_SERIAL_NO As UInteger = &H10103
		Friend Const SCARD_ATTR_VENDOR_IFD_TYPE As UInteger = &H10101
		Friend Const SCARD_ATTR_VENDOR_IFD_VERSION As UInteger = &H10102
		Friend Const SCARD_ATTR_VENDOR_NAME As UInteger = &H10100
		Friend Const SCARD_AUTOALLOCATE As UInteger = UInteger.MaxValue
		Friend Const SCARD_CONFISCATE_CARD As UInteger = 4
		Friend Const SCARD_DEFAULT_READERS As String = "SCard$DefaultReaders"
		Friend Const SCARD_E_BAD_SEEK As UInteger = &H80100029UI
		Friend Const SCARD_E_CANCELLED As UInteger = &H80100002UI
		Friend Const SCARD_E_CANT_DISPOSE As UInteger = &H8010000eUI
		Friend Const SCARD_E_CARD_UNSUPPORTED As UInteger = &H8010001cUI
		Friend Const SCARD_E_CERTIFICATE_UNAVAILABLE As UInteger = &H8010002dUI
		Friend Const SCARD_E_COMM_DATA_LOST As UInteger = &H8010002fUI
		Friend Const SCARD_E_DIR_NOT_FOUND As UInteger = &H80100023UI
		Friend Const SCARD_E_DUPLICATE_READER As UInteger = &H8010001bUI
		Friend Const SCARD_E_FILE_NOT_FOUND As UInteger = 2148532260UI
		Friend Const SCARD_E_ICC_CREATEORDER As UInteger = &H80100021UI
		Friend Const SCARD_E_ICC_INSTALLATION As UInteger = &H80100020UI
		Friend Const SCARD_E_INSUFFICIENT_BUFFER As UInteger = &H80100008UI
		Friend Const SCARD_E_INVALID_ATR As UInteger = &H80100015UI
		Friend Const SCARD_E_INVALID_CHV As UInteger = &H8010002aUI
		Friend Const SCARD_E_INVALID_HANDLE As UInteger = &H80100003UI
		Friend Const SCARD_E_INVALID_PARAMETER As UInteger = &H80100004UI
		Friend Const SCARD_E_INVALID_TARGET As UInteger = &H80100005UI
		Friend Const SCARD_E_INVALID_VALUE As UInteger = &H80100011UI
		Friend Const SCARD_E_NO_ACCESS As UInteger = &H80100027UI
		Friend Const SCARD_E_NO_DIR As UInteger = &H80100025UI
		Friend Const SCARD_E_NO_FILE As UInteger = &H80100026UI
		Friend Const SCARD_E_NO_KEY_CONTAINER As UInteger = &H80100030UI
		Friend Const SCARD_E_NO_MEMORY As UInteger = 2148532230UI
		Friend Const SCARD_E_NO_READERS_AVAILABLE As UInteger = 2148532270UI
		Friend Const SCARD_E_NO_SERVICE As UInteger = &H8010001dUI
		Friend Const SCARD_E_NO_SMARTCARD As UInteger = &H8010000cUI
		Friend Const SCARD_E_NO_SUCH_CERTIFICATE As UInteger = &H8010002cUI
		Friend Const SCARD_E_NOT_READY As UInteger = 2148532240UI
		Friend Const SCARD_E_NOT_TRANSACTED As UInteger = &H80100016UI
		Friend Const SCARD_E_PCI_TOO_SMALL As UInteger = &H80100019UI
		Friend Const SCARD_E_PROTO_MISMATCH As UInteger = &H8010000fUI
		Friend Const SCARD_E_READER_UNAVAILABLE As UInteger = &H80100017UI
		Friend Const SCARD_E_READER_UNSUPPORTED As UInteger = 2148532250UI
		Friend Const SCARD_E_SERVER_TOO_BUSY As UInteger = &H80100031UI
		Friend Const SCARD_E_SERVICE_STOPPED As UInteger = &H8010001eUI
		Friend Const SCARD_E_SHARING_VIOLATION As UInteger = &H8010000bUI
		Friend Const SCARD_E_SYSTEM_CANCELLED As UInteger = &H80100012UI
		Friend Const SCARD_E_TIMEOUT As UInteger = &H8010000aUI
		Friend Const SCARD_E_UNEXPECTED As UInteger = &H8010001fUI
		Friend Const SCARD_E_UNKNOWN_CARD As UInteger = &H8010000dUI
		Friend Const SCARD_E_UNKNOWN_READER As UInteger = &H80100009UI
		Friend Const SCARD_E_UNKNOWN_RES_MNG As UInteger = &H8010002bUI
		Friend Const SCARD_E_UNSUPPORTED_FEATURE As UInteger = &H80100022UI
		Friend Const SCARD_E_WRITE_TOO_MANY As UInteger = &H80100028UI
		Friend Const SCARD_EJECT_CARD As UInteger = 3
		Friend Const SCARD_F_COMM_ERROR As UInteger = &H80100013UI
		Friend Const SCARD_F_INTERNAL_ERROR As UInteger = &H80100001UI
		Friend Const SCARD_F_UNKNOWN_ERROR As UInteger = &H80100014UI
		Friend Const SCARD_F_WAITED_TOO_LONG As UInteger = &H80100007UI
		Friend Const SCARD_LEAVE_CARD As UInteger = 0
		Friend Const SCARD_LOCAL_READERS As String = "SCard$LocalReaders"
		Friend Const SCARD_NEGOTIABLE As UInteger = 5
		Friend Const SCARD_P_SHUTDOWN As UInteger = &H80100018UI
		Friend Const SCARD_PERF_BYTES_TRANSMITTED As UInteger = &H7ffe0002
		Friend Const SCARD_PERF_NUM_TRANSMISSIONS As UInteger = &H7ffe0001
		Friend Const SCARD_PERF_TRANSMISSION_TIME As UInteger = &H7ffe0003
		Friend Const SCARD_POWERED As UInteger = 4
		Friend Const SCARD_PRESENT As UInteger = 2
		Friend Const SCARD_PROTOCOL_DEFAULT As UInteger = &H80000000UI
		Friend Const SCARD_PROTOCOL_OPTIMAL As UInteger = 0
		Friend Const SCARD_PROTOCOL_RAW As UInteger = &H10000
		Friend Const SCARD_PROTOCOL_T0 As UInteger = 1
		Friend Const SCARD_PROTOCOL_T1 As UInteger = 2
		Friend Const SCARD_PROTOCOL_Tx As UInteger = 3
		Friend Const SCARD_PROTOCOL_UNDEFINED As UInteger = 0
		Friend Const SCARD_READER_CONFISCATES As UInteger = 4
		Friend Const SCARD_READER_EJECTS As UInteger = 2
		Friend Const SCARD_READER_SWALLOWS As UInteger = 1
		Friend Const SCARD_READER_TYPE_IDE As UInteger = &H10
		Friend Const SCARD_READER_TYPE_KEYBOARD As UInteger = 4
		Friend Const SCARD_READER_TYPE_PARALELL As UInteger = 2
		Friend Const SCARD_READER_TYPE_PCMCIA As UInteger = &H40
		Friend Const SCARD_READER_TYPE_SCSI As UInteger = 8
		Friend Const SCARD_READER_TYPE_SERIAL As UInteger = 1
		Friend Const SCARD_READER_TYPE_USB As UInteger = &H20
		Friend Const SCARD_READER_TYPE_VENDOR As UInteger = 240
		Friend Const SCARD_RESET_CARD As UInteger = 1
		Friend Const SCARD_S_SUCCESS As UInteger = 0
		Friend Const SCARD_SCOPE_SYSTEM As UInteger = 2
		Friend Const SCARD_SCOPE_TERMINAL As UInteger = 1
		Friend Const SCARD_SCOPE_USER As UInteger = 0
		Friend Const SCARD_SHARE_DIRECT As UInteger = 3
		Friend Const SCARD_SHARE_EXCLUSIVE As UInteger = 1
		Friend Const SCARD_SHARE_SHARED As UInteger = 2
		Friend Const SCARD_SPECIFIC As UInteger = 6
		Friend Const SCARD_STATE_ATRMATCH As UInteger = &H40
		Friend Const SCARD_STATE_CHANGED As UInteger = 2
		Friend Const SCARD_STATE_EMPTY As UInteger = &H10
		Friend Const SCARD_STATE_EXCLUSIVE As UInteger = &H80
		Friend Const SCARD_STATE_IGNORE As UInteger = 1
		Friend Const SCARD_STATE_INUSE As UInteger = &H100
		Friend Const SCARD_STATE_MUTE As UInteger = &H200
		Friend Const SCARD_STATE_PRESENT As UInteger = &H20
		Friend Const SCARD_STATE_UNAVAILABLE As UInteger = 8
		Friend Const SCARD_STATE_UNAWARE As UInteger = 0
		Friend Const SCARD_STATE_UNKNOWN As UInteger = 4
		Friend Const SCARD_STATE_UNPOWERED As UInteger = &H400
		Friend Const SCARD_SWALLOWED As UInteger = 3
		Friend Const SCARD_SYSTEM_READERS As String = "SCard$SystemReaders"
		Friend Const SCARD_UNKNOWN As UInteger = 0
		Friend Const SCARD_UNPOWER_CARD As UInteger = 2
		Friend Const SCARD_W_CANCELLED_BY_USER As UInteger = &H8010006eUI
		Friend Const SCARD_W_CARD_NOT_AUTHENTICATED As UInteger = &H8010006fUI
		Friend Const SCARD_W_CHV_BLOCKED As UInteger = &H8010006cUI
		Friend Const SCARD_W_EOF As UInteger = &H8010006dUI
		Friend Const SCARD_W_REMOVED_CARD As UInteger = &H80100069UI
		Friend Const SCARD_W_RESET_CARD As UInteger = &H80100068UI
		Friend Const SCARD_W_SECURITY_VIOLATION As UInteger = 2148532330UI
		Friend Const SCARD_W_UNPOWERED_CARD As UInteger = &H80100067UI
		Friend Const SCARD_W_UNRESPONSIVE_CARD As UInteger = &H80100066UI
		Friend Const SCARD_W_UNSUPPORTED_CARD As UInteger = &H80100065UI
		Friend Const SCARD_W_WRONG_CHV As UInteger = &H8010006bUI



	End Class


End Namespace