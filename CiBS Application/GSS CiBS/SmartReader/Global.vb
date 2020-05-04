Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices

Namespace SCLib
	<StructLayout(LayoutKind.Sequential)> _
	Public Structure SCStateInfo
		Public sReaderName As String
		Public nCurrentState As SCStates
		Public nEventState As SCStates
		Public vbATR As Byte()
	End Structure
	Public Enum SCardAccessMode
		' Fields
		Direct = 3
		Exclusive = 1
		[Shared] = 2
	End Enum

	<Flags> _
	Public Enum SCardAuthDevices
		' Fields
		Display = &H80
		EncryptedInput = &H8000
		Fingerprint = 8
		Image = &H20
		Keyboard = 4
		NoDevices = 0
		Numeric = 2
		ReservedFU = 1
		Retinal = &H10
		Voice = &H40
	End Enum

	Public Enum SCardChannelType
		' Fields
		IDE = &H10
		Keyboard = 4
		Parallel = 2
		PCMCIA = &H40
		SCSI = 8
		Serial = 1
		Unknown = 0
		USB = &H20
		Vendor = 240
	End Enum

	Public Enum SCardContextScope
		' Fields
		System = 2
		Terminal = 1
		User = 0
	End Enum

	Public Enum SCardControlCodes
		' Fields
		Absent = &H31002c
		Confiscate = 3211280
		Eject = &H310018
		GetAttribute = &H310008
		GetLastError = &H31003c
		GetPerfCntr = &H310040
		Power = &H310004
		Present = &H310028
		Protocol = &H310030
		Read = &H310020
		SetAttribute = &H31000c
		State = 3211320
		Swallow = &H31001c
		Transmit = &H310014
		Write = 3211300
	End Enum

	Public Enum SCardDisposition
		' Fields
		Confiscate = 4
		EjectCard = 3
		LeaveCard = 0
		ResetCard = 1
		UnpowerCard = 2
	End Enum

	<Flags> _
	Public Enum SCardMechanicalCharacteristics
		' Fields
		Captures = 4
		Ejects = 2
		None = 0
		Swallows = 1
	End Enum

	<Flags> _
	Public Enum SCardProtocolIdentifiers
		' Fields
		[Default] = -2147483648
		Optimal = 0
		Raw = &H10000
		T0 = 1
		T1 = 2
		Undefined = 0
	End Enum

	Public Enum SCardReaderCapability
		' Fields
		ATRString = &H90303
		ChannelID = &H20110
		Characteristics = &H60150
		CurrentBWT = &H80209
		CurrentCLK = &H80202
		CurrentCWT = 524810
		CurrentD = &H80204
		CurrentEBCEncoding = &H8020b
		CurrentF = &H80203
		CurrentIFSC = &H80207
		CurrentIFSD = &H80208
		CurrentIOState = &H90302
		CurrentN = &H80205
		CurrentProtocolType = &H80201
		CurrentW = &H80206
		DeviceFriendlyName = &H7fff0003
		DeviceInUse = &H7fff0002
		DeviceSystemName = &H7fff0004
		DeviceUnit = &H7fff0001
		EscAuthRequest = &H7a005
		EscCancel = &H7a003
		EscReset = &H7a000
		ExtendedBWT = &H8020c
		ICCInterfaceStatus = &H90301
		ICCPresence = &H90300
		ICCTypePerATR = &H90304
		MaxInput = &H7a007
		PerfBytesTransmitted = &H7ffe0002
		PerfNumTransmissions = &H7ffe0001
		PerfTransmissionTime = &H7ffe0003
		PowerMgmtSupport = &H40131
		ProtocolDefaultCLK = &H30121
		ProtocolDefaultDataRate = &H30123
		ProtocolMaxCLK = &H30122
		ProtocolMaxDataRate = 196900
		ProtocolMaxIFSD = &H30125
		ProtocolTypes = &H30120
		SuppressT1IFSRequest = &H7fff0007
		UserAuthInputDevice = &H50142
		UserToCardAuthDevice = 328000
		VendorIFDSerialNo = &H10103
		VendorIFDType = &H10101
		VendorIFDVersion = &H10102
		VendorName = &H10100
	End Enum

	Public Enum SCardReaderState
		' Fields
		Absent = 1
		Negotiable = 5
		Powered = 4
		Present = 2
		Specific = 6
		Swallowed = 3
		Unknown = 0
	End Enum

	Public Enum SCardResponseCode
		' Fields
		Cancelled = 2
		InsufficientBuffer = 8
		InvalidHandle = 3
		InvalidParameter = 4
		InvalidValue = &H11
		NoMemory = 6
		NoService = &H1d
		NoSmartCard = 12
		NotReady = &H10
		NotTransacted = &H16
		ProtoMismatch = 15
		ReaderUnavailable = &H17
		RemovedCard = &H69
		ResetCard = &H68
		ServiceStopped = 30
		SharingViolation = 11
		SystemCancelled = &H12
		Timeout = 10
		UnknownCard = 13
		UnknownReader = 9
		UnpoweredCard = &H67
		UnresponsiveCard = &H66
		UnsupportedCard = &H65
	End Enum

	Public Enum SCLibExceptionCode
		' Fields
		CardCommunicationError = 2
		CardWithdrawn = 1
		DriverError = 3
		DriverException = 4
		DriverIncompatible = 5
		InvalidAPDU = 0
	End Enum

	<Flags> _
	Public Enum SCStates
		' Fields
		AtrMatch = &H40
		Changed = 2
		Empty = &H10
		Exclusive = &H80
		Ignore = 1
		InUse = &H100
		Mute = &H200
		Present = &H20
		Unavailable = 8
		Unaware = 0
		Unknown = 4
		Unpowered = &H400
	End Enum


End Namespace