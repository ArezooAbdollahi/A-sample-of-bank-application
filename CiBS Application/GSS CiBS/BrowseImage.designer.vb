<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BrowseImage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BrowseImage))
        Me.btnSearchImage = New System.Windows.Forms.Button()
        Me.txtImagePath = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.pbImage = New System.Windows.Forms.PictureBox()
        Me.btnCropImage = New System.Windows.Forms.Button()
        Me.btrReturn = New System.Windows.Forms.Button()
        Me.imgCapture = New System.Windows.Forms.PictureBox()
        Me.btnSelectCompleteImage = New System.Windows.Forms.Button()
        CType(Me.pbImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgCapture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSearchImage
        '
        Me.btnSearchImage.Location = New System.Drawing.Point(15, 19)
        Me.btnSearchImage.Name = "btnSearchImage"
        Me.btnSearchImage.Size = New System.Drawing.Size(127, 23)
        Me.btnSearchImage.TabIndex = 0
        Me.btnSearchImage.Text = "جستجوی عکس"
        Me.btnSearchImage.UseVisualStyleBackColor = True
        '
        'txtImagePath
        '
        Me.txtImagePath.Location = New System.Drawing.Point(163, 19)
        Me.txtImagePath.Name = "txtImagePath"
        Me.txtImagePath.ReadOnly = True
        Me.txtImagePath.Size = New System.Drawing.Size(354, 21)
        Me.txtImagePath.TabIndex = 1
        Me.txtImagePath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'pbImage
        '
        Me.pbImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbImage.Location = New System.Drawing.Point(15, 48)
        Me.pbImage.Name = "pbImage"
        Me.pbImage.Size = New System.Drawing.Size(521, 285)
        Me.pbImage.TabIndex = 2
        Me.pbImage.TabStop = False
        '
        'btnCropImage
        '
        Me.btnCropImage.Location = New System.Drawing.Point(377, 350)
        Me.btnCropImage.Name = "btnCropImage"
        Me.btnCropImage.Size = New System.Drawing.Size(127, 23)
        Me.btnCropImage.TabIndex = 3
        Me.btnCropImage.Text = "انتخاب قسمتی از عکس"
        Me.btnCropImage.UseVisualStyleBackColor = True
        '
        'btrReturn
        '
        Me.btrReturn.Location = New System.Drawing.Point(46, 350)
        Me.btrReturn.Name = "btrReturn"
        Me.btrReturn.Size = New System.Drawing.Size(127, 23)
        Me.btrReturn.TabIndex = 6
        Me.btrReturn.Text = "بازگشت"
        Me.btrReturn.UseVisualStyleBackColor = True
        '
        'imgCapture
        '
        Me.imgCapture.Location = New System.Drawing.Point(218, 339)
        Me.imgCapture.Name = "imgCapture"
        Me.imgCapture.Size = New System.Drawing.Size(100, 50)
        Me.imgCapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.imgCapture.TabIndex = 7
        Me.imgCapture.TabStop = False
        Me.imgCapture.Visible = False
        '
        'btnSelectCompleteImage
        '
        Me.btnSelectCompleteImage.Location = New System.Drawing.Point(218, 350)
        Me.btnSelectCompleteImage.Name = "btnSelectCompleteImage"
        Me.btnSelectCompleteImage.Size = New System.Drawing.Size(127, 23)
        Me.btnSelectCompleteImage.TabIndex = 8
        Me.btnSelectCompleteImage.Text = "انتخاب کل عکس"
        Me.btnSelectCompleteImage.UseVisualStyleBackColor = True
        '
        'BrowseImage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(548, 387)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnSelectCompleteImage)
        Me.Controls.Add(Me.imgCapture)
        Me.Controls.Add(Me.btrReturn)
        Me.Controls.Add(Me.btnCropImage)
        Me.Controls.Add(Me.pbImage)
        Me.Controls.Add(Me.txtImagePath)
        Me.Controls.Add(Me.btnSearchImage)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "BrowseImage"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "پردازش عکس مشتری"
        CType(Me.pbImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgCapture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSearchImage As System.Windows.Forms.Button
    Friend WithEvents txtImagePath As System.Windows.Forms.TextBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents pbImage As System.Windows.Forms.PictureBox
    Friend WithEvents btnCropImage As System.Windows.Forms.Button
    Friend WithEvents btrReturn As System.Windows.Forms.Button
    Friend WithEvents imgCapture As System.Windows.Forms.PictureBox
    Friend WithEvents btnSelectCompleteImage As System.Windows.Forms.Button
End Class
