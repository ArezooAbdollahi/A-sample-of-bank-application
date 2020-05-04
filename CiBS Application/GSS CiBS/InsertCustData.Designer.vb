<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InsertCustData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InsertCustData))
        Me.btnInsertCustData = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBirthDate = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtTell = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtFatherName = New System.Windows.Forms.TextBox()
        Me.btnDate = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnInsertCustData
        '
        Me.btnInsertCustData.Location = New System.Drawing.Point(145, 308)
        Me.btnInsertCustData.Name = "btnInsertCustData"
        Me.btnInsertCustData.Size = New System.Drawing.Size(187, 52)
        Me.btnInsertCustData.TabIndex = 15
        Me.btnInsertCustData.Text = "ثبت اطلاعات مشتری جدید"
        Me.btnInsertCustData.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(48, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "تاریخ تولد"
        '
        'txtBirthDate
        '
        Me.txtBirthDate.Location = New System.Drawing.Point(145, 61)
        Me.txtBirthDate.MaxLength = 10
        Me.txtBirthDate.Name = "txtBirthDate"
        Me.txtBirthDate.Size = New System.Drawing.Size(199, 21)
        Me.txtBirthDate.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(48, 131)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(28, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "تلفن"
        '
        'txtTell
        '
        Me.txtTell.Location = New System.Drawing.Point(145, 128)
        Me.txtTell.MaxLength = 11
        Me.txtTell.Name = "txtTell"
        Me.txtTell.Size = New System.Drawing.Size(199, 21)
        Me.txtTell.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(48, 170)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(91, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "نشانی و کدپستی"
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(145, 165)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(328, 100)
        Me.txtAddress.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(48, 99)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(36, 13)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "نام پدر"
        '
        'txtFatherName
        '
        Me.txtFatherName.Location = New System.Drawing.Point(145, 96)
        Me.txtFatherName.MaxLength = 100
        Me.txtFatherName.Name = "txtFatherName"
        Me.txtFatherName.Size = New System.Drawing.Size(199, 21)
        Me.txtFatherName.TabIndex = 9
        '
        'btnDate
        '
        Me.btnDate.BackgroundImage = CType(resources.GetObject("btnDate.BackgroundImage"), System.Drawing.Image)
        Me.btnDate.Location = New System.Drawing.Point(352, 55)
        Me.btnDate.Name = "btnDate"
        Me.btnDate.Size = New System.Drawing.Size(27, 28)
        Me.btnDate.TabIndex = 16
        Me.btnDate.UseVisualStyleBackColor = True
        '
        'InsertCustData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(522, 452)
        Me.Controls.Add(Me.btnDate)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtTell)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtFatherName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtBirthDate)
        Me.Controls.Add(Me.btnInsertCustData)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "InsertCustData"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ثبت اطلاعات مشتری"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnInsertCustData As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBirthDate As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTell As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtFatherName As System.Windows.Forms.TextBox
    Friend WithEvents btnDate As System.Windows.Forms.Button
End Class
