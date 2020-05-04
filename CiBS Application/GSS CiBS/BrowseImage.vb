Public Class BrowseImage

    Private Sub btnSearchImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchImage.Click
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.Filter = "عکس مشتری (*.jpg)|*.jpg"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Title = "لطفاً عکس را انتخاب نمایید"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.RestoreDirectory = True
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtImagePath.Text = OpenFileDialog1.FileName
            ScaleImage()
        End If
    End Sub

    Sub ScaleImage()
        Dim PicBoxHeight As Integer
        Dim PicBoxWidth As Integer
        Dim ImageHeight As Integer
        Dim ImageWidth As Integer
        Dim TempImage As Image
        Dim scale_factor As Single

        pbImage.SizeMode = PictureBoxSizeMode.AutoSize
        PicBoxHeight = pbImage.Height
        PicBoxWidth = pbImage.Width
        TempImage = Image.FromFile(OpenFileDialog1.FileName)
        ImageHeight = TempImage.Height
        ImageWidth = TempImage.Width
        scale_factor = 1.0
        If ImageHeight > PicBoxHeight Then
            scale_factor = CSng(PicBoxHeight / ImageHeight)
        End If
        If (ImageWidth * scale_factor) > PicBoxWidth Then
            scale_factor = CSng(PicBoxWidth / ImageWidth)
        End If
        pbImage.Image = TempImage
        Dim bm_source As New Bitmap(pbImage.Image)
        Dim bm_dest As New Bitmap( _
            CInt(bm_source.Width * scale_factor), _
            CInt(bm_source.Height * scale_factor))
        Dim gr_dest As Graphics = Graphics.FromImage(bm_dest)
        gr_dest.DrawImage(bm_source, 0, 0, _
            bm_dest.Width + 1, _
            bm_dest.Height + 1)
        pbImage.Image = bm_dest
    End Sub

    Dim cropX As Integer
    Dim cropY As Integer
    Dim cropWidth As Integer
    Dim cropHeight As Integer
    Dim cropBitmap As Bitmap
    Dim flgMouseDown As Boolean = False

    Private Sub btnCropImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCropImage.Click
        Try
            If cropWidth < 1 Then
                MessageBox.Show("ابتدا باید قسمتی از عکس را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Dim rect As Rectangle = New Rectangle(cropX, cropY, cropWidth, cropHeight)
            Dim bit As Bitmap = New Bitmap(pbImage.Image, pbImage.Width, pbImage.Height)
            cropBitmap = New Bitmap(cropWidth, cropHeight)
            Dim g As Graphics = Graphics.FromImage(cropBitmap)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
            g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
            g.DrawImage(bit, 0, 0, rect, GraphicsUnit.Pixel)
            imgCapture.Image = cropBitmap
            imgCapture.BorderStyle = BorderStyle.FixedSingle
            imgCapture.Image.Save("img.jpg")
            ' MessageBox.Show("عملیات انتخاب با موفقیت انجام شد", "انتخاب محدوده ای از عکس", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Dispose()
        Catch exc As Exception
        End Try
    End Sub

    Public cropPen As Pen
    Public cropPenSize As Integer = 1 '2
    Public cropPenColor As Color = Color.Yellow

    Private Sub crobPictureBox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbImage.MouseDown
        Try
            If pbImage.Image Is Nothing Then Exit Sub
            If e.Button = Windows.Forms.MouseButtons.Left Then
                flgMouseDown = True
                cropX = e.X
                cropY = e.Y
                cropPen = New Pen(cropPenColor, cropPenSize)
            End If
            pbImage.Refresh()
        Catch exc As Exception
        End Try
    End Sub


    Private Sub crobPictureBox_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbImage.MouseMove
        Try

            If pbImage.Image Is Nothing Then Exit Sub

            If e.Button = Windows.Forms.MouseButtons.Left And flgMouseDown Then

                pbImage.Refresh()
                cropWidth = e.X - cropX
                cropHeight = e.Y - cropY
                pbImage.CreateGraphics.DrawRectangle(cropPen, cropX, cropY, cropWidth, cropHeight)


            End If
            GC.Collect()
        Catch exc As Exception

            If Err.Number = 5 Then Exit Sub
        End Try

    End Sub

    Private Sub btrReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btrReturn.Click
        Me.Dispose()
    End Sub

    Private Sub pbImage_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbImage.MouseUp
        flgMouseDown = False
    End Sub

  
    Private Sub btnSelectCompleteImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectCompleteImage.Click
        pbImage.Image.Save("img.jpg")
        Me.Dispose()
    End Sub

  
End Class