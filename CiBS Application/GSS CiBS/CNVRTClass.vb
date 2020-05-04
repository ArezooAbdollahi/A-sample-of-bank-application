Imports System
Public Class CNVRTClass



    Private Shared Sub Main(ByVal args As String())
        Dim b As New System.Drawing.Bitmap("test.jpg")
        SplashImage(b, 0, 0)
        '
        Dim dtFaq As DateTime = DateTime.Now
        Dim b0 As System.Drawing.Bitmap = CopyToBpp(b, 1)
        Dim tsFaq As TimeSpan = DateTime.Now - dtFaq
        Console.WriteLine("GDI conversion time: " & tsFaq.ToString())
        SplashImage(b0, 200, 100)
        '
        Dim dtLu As DateTime = DateTime.Now
        Dim b1 As System.Drawing.Bitmap = FaqCopyTo1bpp(b)
        Dim tsLu As TimeSpan = DateTime.Now - dtLu
        Console.WriteLine("FAQ conversion time: " & tsLu.ToString())
        SplashImage(b1, 400, 200)
        '
        System.Threading.Thread.Sleep(1000)
        InvalidateRect(IntPtr.Zero, IntPtr.Zero, 1)
    End Sub


    ''' <summary>
    ''' Copies a bitmap into a 1bpp/8bpp bitmap of the same dimensions, fast
    ''' </summary>
    ''' <param name="b">original bitmap</param>
    ''' <param name="bpp">1 or 8, target bpp</param>
    ''' <returns>a 1bpp copy of the bitmap</returns>
    Public Shared Function CopyToBpp(ByVal b As System.Drawing.Bitmap, ByVal bpp As Integer) As System.Drawing.Bitmap
        If bpp <> 1 AndAlso bpp <> 8 Then
            Throw New System.ArgumentException("1 or 8", "bpp")
        End If

        ' Plan: built into Windows GDI is the ability to convert
        ' bitmaps from one format to another. Most of the time, this
        ' job is actually done by the graphics hardware accelerator card
        ' and so is extremely fast. The rest of the time, the job is done by
        ' very fast native code.
        ' We will call into this GDI functionality from C#. Our plan:
        ' (1) Convert our Bitmap into a GDI hbitmap (ie. copy unmanaged->managed)
        ' (2) Create a GDI monochrome hbitmap
        ' (3) Use GDI "BitBlt" function to copy from hbitmap into monochrome (as above)
        ' (4) Convert the monochrone hbitmap into a Bitmap (ie. copy unmanaged->managed)

        Dim w As Integer = b.Width, h As Integer = b.Height
        Dim hbm As IntPtr = b.GetHbitmap()
        ' this is step (1)
        '
        ' Step (2): create the monochrome bitmap.
        ' "BITMAPINFO" is an interop-struct which we define below.
        ' In GDI terms, it's a BITMAPHEADERINFO followed by an array of two RGBQUADs
        Dim bmi As New BITMAPINFO()
        bmi.biSize = 40
        ' the size of the BITMAPHEADERINFO struct
        bmi.biWidth = w
        bmi.biHeight = h
        bmi.biPlanes = 1
        ' "planes" are confusing. We always use just 1. Read MSDN for more info.
        bmi.biBitCount = CShort(bpp)
        ' ie. 1bpp or 8bpp
        bmi.biCompression = BI_RGB
        ' ie. the pixels in our RGBQUAD table are stored as RGBs, not palette indexes
        bmi.biSizeImage = CUInt((((w + 7) And &HFFFFFFF8) * h / 8))
        bmi.biXPelsPerMeter = 1000000
        ' not really important
        bmi.biYPelsPerMeter = 1000000
        ' not really important
        ' Now for the colour table.
        Dim ncols As UInteger = CUInt(1) << bpp
        ' 2 colours for 1bpp; 256 colours for 8bpp
        bmi.biClrUsed = ncols
        bmi.biClrImportant = ncols
        bmi.cols = New UInteger(255) {}
        ' The structure always has fixed size 256, even if we end up using fewer colours
        If bpp = 1 Then
            bmi.cols(1) = MAKERGB(0, 0, 0)
            bmi.cols(0) = MAKERGB(255, 255, 255)
        Else
            For i As Integer = 0 To ncols - 1
                bmi.cols(i) = MAKERGB(i, i, i)
            Next
        End If
        ' For 8bpp we've created an palette with just greyscale colours.
        ' You can set up any palette you want here. Here are some possibilities:
        ' greyscale: for (int i=0; i<256; i++) bmi.cols[i]=MAKERGB(i,i,i);
        ' rainbow: bmi.biClrUsed=216; bmi.biClrImportant=216; int[] colv=new int[6]{0,51,102,153,204,255};
        ' for (int i=0; i<216; i++) bmi.cols[i]=MAKERGB(colv[i/36],colv[(i/6)%6],colv[i%6]);
        ' optimal: a difficult topic: http://en.wikipedia.org/wiki/Color_quantization
        ' 
        ' Now create the indexed bitmap "hbm0"
        Dim bits0 As IntPtr
        ' not used for our purposes. It returns a pointer to the raw bits that make up the bitmap.
        Dim hbm0 As IntPtr = CreateDIBSection(IntPtr.Zero, bmi, DIB_RGB_COLORS, bits0, IntPtr.Zero, 0)
        '
        ' Step (3): use GDI's BitBlt function to copy from original hbitmap into monocrhome bitmap
        ' GDI programming is kind of confusing... nb. The GDI equivalent of "Graphics" is called a "DC".
        Dim sdc As IntPtr = GetDC(IntPtr.Zero)
        ' First we obtain the DC for the screen
        ' Next, create a DC for the original hbitmap
        Dim hdc As IntPtr = CreateCompatibleDC(sdc)
        SelectObject(hdc, hbm)
        ' and create a DC for the monochrome hbitmap
        Dim hdc0 As IntPtr = CreateCompatibleDC(sdc)
        SelectObject(hdc0, hbm0)
        ' Now we can do the BitBlt:
        BitBlt(hdc0, 0, 0, w, h, hdc, _
        0, 0, SRCCOPY)
        ' Step (4): convert this monochrome hbitmap back into a Bitmap:
        Dim b0 As System.Drawing.Bitmap = System.Drawing.Bitmap.FromHbitmap(hbm0)
        '
        ' Finally some cleanup.
        DeleteDC(hdc)
        DeleteDC(hdc0)
        ReleaseDC(IntPtr.Zero, sdc)
        DeleteObject(hbm)
        DeleteObject(hbm0)
        '
        Return b0
    End Function

    ''' <summary>
    ''' Draws a bitmap onto the screen. Note: this will be overpainted
    ''' by other windows when they come to draw themselves. Only use it
    ''' if you want to draw something quickly and can't be bothered with forms.
    ''' </summary>
    ''' <param name="b">the bitmap to draw on the screen</param>
    ''' <param name="x">x screen coordinate</param>
    ''' <param name="y">y screen coordinate</param>
    Private Shared Sub SplashImage(ByVal b As System.Drawing.Bitmap, ByVal x As Integer, ByVal y As Integer)
        ' Drawing onto the screen is supported by GDI, but not by the Bitmap/Graphics class.
        ' So we use interop:
        ' (1) Copy the Bitmap into a GDI hbitmap
        Dim hbm As IntPtr = b.GetHbitmap()
        ' (2) obtain the GDI equivalent of a "Graphics" for the screen
        Dim sdc As IntPtr = GetDC(IntPtr.Zero)
        ' (3) obtain the GDI equivalent of a "Graphics" for the hbitmap
        Dim hdc As IntPtr = CreateCompatibleDC(sdc)
        SelectObject(hdc, hbm)
        ' (4) Draw from the hbitmap's "Graphics" onto the screen's "Graphics"
        BitBlt(sdc, x, y, b.Width, b.Height, hdc, _
        0, 0, SRCCOPY)
        ' and do boring GDI cleanup:
        DeleteDC(hdc)
        ReleaseDC(IntPtr.Zero, sdc)
        DeleteObject(hbm)
    End Sub


    <System.Runtime.InteropServices.DllImport("gdi32.dll")> _
    Public Shared Function DeleteObject(ByVal hObject As IntPtr) As Boolean
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Public Shared Function InvalidateRect(ByVal hwnd As IntPtr, ByVal rect As IntPtr, ByVal bErase As Integer) As Integer
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Public Shared Function GetDC(ByVal hwnd As IntPtr) As IntPtr
    End Function

    <System.Runtime.InteropServices.DllImport("gdi32.dll")> _
    Public Shared Function CreateCompatibleDC(ByVal hdc As IntPtr) As IntPtr
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Public Shared Function ReleaseDC(ByVal hwnd As IntPtr, ByVal hdc As IntPtr) As Integer
    End Function

    <System.Runtime.InteropServices.DllImport("gdi32.dll")> _
    Public Shared Function DeleteDC(ByVal hdc As IntPtr) As Integer
    End Function

    <System.Runtime.InteropServices.DllImport("gdi32.dll")> _
    Public Shared Function SelectObject(ByVal hdc As IntPtr, ByVal hgdiobj As IntPtr) As IntPtr
    End Function

    <System.Runtime.InteropServices.DllImport("gdi32.dll")> _
    Public Shared Function BitBlt(ByVal hdcDst As IntPtr, ByVal xDst As Integer, ByVal yDst As Integer, ByVal w As Integer, ByVal h As Integer, ByVal hdcSrc As IntPtr, _
    ByVal xSrc As Integer, ByVal ySrc As Integer, ByVal rop As Integer) As Integer
    End Function
    Shared SRCCOPY As Integer = &HCC0020

    <System.Runtime.InteropServices.DllImport("gdi32.dll")> _
    Private Shared Function CreateDIBSection(ByVal hdc As IntPtr, ByRef bmi As BITMAPINFO, ByVal Usage As UInteger, ByRef bits As IntPtr, ByVal hSection As IntPtr, ByVal dwOffset As UInteger) As IntPtr
    End Function
    Shared BI_RGB As UInteger = 0
    Shared DIB_RGB_COLORS As UInteger = 0
    <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)> _
    Public Structure BITMAPINFO
        Public biSize As UInteger
        Public biWidth As Integer, biHeight As Integer
        Public biPlanes As Short, biBitCount As Short
        Public biCompression As UInteger, biSizeImage As UInteger
        Public biXPelsPerMeter As Integer, biYPelsPerMeter As Integer
        Public biClrUsed As UInteger, biClrImportant As UInteger
        <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst:=256)> _
        Public cols As UInteger()
    End Structure

    Private Shared Function MAKERGB(ByVal r As Integer, ByVal g As Integer, ByVal b As Integer) As UInteger
        Return CUInt((b And 255)) Or CUInt(((r And 255) << 8)) Or CUInt(((g And 255) << 16))
    End Function


    ''' <summary>
    ''' Copies a bitmap into a 1bpp bitmap of the same dimensions, slowly, using code from Bob Powell's GDI+ faq http://www.bobpowell.net/onebit.htm
    ''' </summary>
    ''' <param name="b">original bitmap</param>
    ''' <returns>a 1bpp copy of the bitmap</returns>
    Private Shared Function FaqCopyTo1bpp(ByVal b As System.Drawing.Bitmap) As System.Drawing.Bitmap
        Dim w As Integer = b.Width, h As Integer = b.Height
        Dim r As New System.Drawing.Rectangle(0, 0, w, h)
        If b.PixelFormat <> System.Drawing.Imaging.PixelFormat.Format32bppPArgb Then
            Dim temp As New System.Drawing.Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
            Dim g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(temp)
            g.DrawImage(b, r, 0, 0, w, h, _
            System.Drawing.GraphicsUnit.Pixel)
            g.Dispose()
            b = temp
        End If
        Dim bdat As System.Drawing.Imaging.BitmapData = b.LockBits(r, System.Drawing.Imaging.ImageLockMode.[ReadOnly], b.PixelFormat)
        Dim b0 As New System.Drawing.Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format1bppIndexed)
        Dim b0dat As System.Drawing.Imaging.BitmapData = b0.LockBits(r, System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format1bppIndexed)
        For y As Integer = 0 To h - 1
            For x As Integer = 0 To w - 1
                Dim index As Integer = y * bdat.Stride + (x * 4)
                If System.Drawing.Color.FromArgb(System.Runtime.InteropServices.Marshal.ReadByte(bdat.Scan0, index + 2), System.Runtime.InteropServices.Marshal.ReadByte(bdat.Scan0, index + 1), System.Runtime.InteropServices.Marshal.ReadByte(bdat.Scan0, index)).GetBrightness() > 0.5F Then
                    Dim index0 As Integer = y * b0dat.Stride + (x >> 3)
                    Dim p As Byte = System.Runtime.InteropServices.Marshal.ReadByte(b0dat.Scan0, index0)
                    Dim mask As Byte = CByte((&H80 >> (x And &H7)))
                    System.Runtime.InteropServices.Marshal.WriteByte(b0dat.Scan0, index0, CByte((p Or mask)))
                End If
            Next
        Next
        b0.UnlockBits(b0dat)
        b.UnlockBits(bdat)
        Return b0
    End Function


End Class
