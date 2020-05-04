
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Runtime.InteropServices

Namespace izzix

    Friend NotInheritable Class izzixSCARDFIM

        Public Sub New()
        End Sub



        Friend Const MAX_FEATUREVECT_LEN_384 As UInteger = 384
        Friend Const MAX_FEATUREVECT_LEN_417 As UInteger = 417
        Friend Const MAX_FEATUREVECT_LEN_545 As UInteger = 545

        Friend Const FMT_SC37 As UInteger = &H101
        Friend Const FMT_DIGENT As UInteger = &H48
        Friend Const FMT_M1 As UInteger = &H1B
        Friend Const FMT_DIN As UInteger = &H1A

        '// Error code.
        Friend Const FPAPIERR_NO As UInteger = 0
        Friend Const FPAPIERR_OK As UInteger = 1
        Friend Const FPAPIERR_GENERAL_ERROR As Int32 = -1
        Friend Const FPAPIERR_CAN_NOT_OPEN_DEVICE As Int32 = -2
        Friend Const FPAPIERR_MATCH_FAILED As Int32 = -101
        Friend Const FPAPIERR_CAN_NOT_ALLOC_MEMORY As Int32 = -201
        Friend Const FPAPIERR_VECT_FAILED As Int32 = -301

        Friend Const FPAPIERR_FAKER_FINGERPRINT As Int32 = -501

        Friend Const FPAPIERR_LEFT_FINGERPRINT As Int32 = -601
        Friend Const FPAPIERR_RIGHT_FINGERPRINT As Int32 = -602
        Friend Const FPAPIERR_UP_FINGERPRINT As Int32 = -603
        Friend Const FPAPIERR_DOWN_FINGERPRINT As Int32 = -604
        Friend Const FPAPIERR_SMALL_FINGERPRINT As Int32 = -605

        Friend Const FPAPIERR_TOO_WET As Int32 = -701
        Friend Const FPAPIERR_TOO_DRY As Int32 = -702



        <DllImport("izxSCardFIM32.dll")> _
        Friend Shared Function izxSCFIM_CapChkImage(ByVal nDeviceNumner As Integer, ByVal ucLoop As Integer) As Int32
        End Function

        <DllImport("izxSCardFIM32.dll")> _
        Friend Shared Function izxSCFIM_GetImageEx(ByVal nDeviceNumner As Integer, ByVal ucOffset As Integer, <[Out]()> ByVal pRawImage As Byte()) As Int32
        End Function

        <DllImport("izxSCardFIM32.dll")> _
        Friend Shared Function izxSCFIM_DisplayImage(ByVal hDC As IntPtr, ByVal x1 As Int32, ByVal y1 As Int32, ByVal x2 As Int32, ByVal y2 As Int32, <[In]()> ByVal pImage As Byte(), ByVal nWidth As Int32, ByVal nHeight As Int32) As Int32
        End Function

        <DllImport("izxSCardFIM32.dll")> _
        Friend Shared Function izxSCFIM_XtractFeature(ByVal nDeviceNumner As Integer, ByVal ucExtractStep As Integer, ByVal ubFlag As Integer) As Int32
        End Function

        <DllImport("izxSCardFIM32.dll")> _
        Friend Shared Function izxSCFIM_FeatureTx(ByVal nDeviceNumner As Integer, ByVal ucClaTx As Integer, ByVal ucInsTx As Integer, ByVal ucFinger As Integer, ByVal ucType As Integer) As Int32
        End Function

        <DllImport("izxSCardFIM32.dll")> _
        Friend Shared Function izxSCFIM_SetStartButton(ByVal nDeviceNumner As Integer, ByVal hwnd As IntPtr) As Int32
        End Function

        <DllImport("izxSCardFIM32.dll")> _
        Friend Shared Function izxSCFIM_SetStopButton(ByVal nDeviceNumner As Integer) As Int32
        End Function

        <DllImport("izxSCardFIM32.dll")> _
        Friend Shared Function izxSCFIM_OkLedOn(ByVal nDeviceNumner As Integer) As Int32
        End Function

        <DllImport("izxSCardFIM32.dll")> _
        Friend Shared Function izxSCFIM_OkLedOff(ByVal nDeviceNumner As Integer) As Int32
        End Function

        <DllImport("izxSCardFIM32.dll")> _
        Friend Shared Function izxSCFIM_IsAvailableDevice(ByVal nDeviceNumner As Integer) As Int32
        End Function

        <DllImport("izxSCardFIM32.dll")> _
        Friend Shared Function izxSCFIM_GetApiVersion(<[Out]()> ByVal ucVersuion As Integer) As Int32
        End Function


    End Class

End Namespace

