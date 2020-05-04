<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Guidance
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
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.lbError2 = New System.Windows.Forms.Label()
        Me.cbError = New System.Windows.Forms.ComboBox()
        Me.lbProbability = New System.Windows.Forms.Label()
        Me.lbSolution = New System.Windows.Forms.Label()
        Me.txProbability = New System.Windows.Forms.TextBox()
        Me.txtSolution = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.dgvError = New System.Windows.Forms.DataGridView()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvError, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSearch
        '
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearch.Location = New System.Drawing.Point(285, 58)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(89, 38)
        Me.btnSearch.TabIndex = 8
        Me.btnSearch.Text = "جستجو"
        Me.btnSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'lbError2
        '
        Me.lbError2.AutoSize = True
        Me.lbError2.BackColor = System.Drawing.SystemColors.Control
        Me.lbError2.Location = New System.Drawing.Point(539, 24)
        Me.lbError2.Name = "lbError2"
        Me.lbError2.Size = New System.Drawing.Size(25, 13)
        Me.lbError2.TabIndex = 10
        Me.lbError2.Text = "خطا"
        '
        'cbError
        '
        Me.cbError.FormattingEnabled = True
        Me.cbError.Location = New System.Drawing.Point(157, 21)
        Me.cbError.Name = "cbError"
        Me.cbError.Size = New System.Drawing.Size(357, 21)
        Me.cbError.TabIndex = 7
        '
        'lbProbability
        '
        Me.lbProbability.AutoSize = True
        Me.lbProbability.Location = New System.Drawing.Point(664, 142)
        Me.lbProbability.Name = "lbProbability"
        Me.lbProbability.Size = New System.Drawing.Size(47, 13)
        Me.lbProbability.TabIndex = 11
        Me.lbProbability.Text = "احتمالات"
        '
        'lbSolution
        '
        Me.lbSolution.AutoSize = True
        Me.lbSolution.Location = New System.Drawing.Point(674, 229)
        Me.lbSolution.Name = "lbSolution"
        Me.lbSolution.Size = New System.Drawing.Size(37, 13)
        Me.lbSolution.TabIndex = 12
        Me.lbSolution.Text = "راه حل"
        '
        'txProbability
        '
        Me.txProbability.Location = New System.Drawing.Point(16, 119)
        Me.txProbability.Multiline = True
        Me.txProbability.Name = "txProbability"
        Me.txProbability.Size = New System.Drawing.Size(638, 68)
        Me.txProbability.TabIndex = 13
        '
        'txtSolution
        '
        Me.txtSolution.Location = New System.Drawing.Point(16, 195)
        Me.txtSolution.Multiline = True
        Me.txtSolution.Name = "txtSolution"
        Me.txtSolution.Size = New System.Drawing.Size(638, 100)
        Me.txtSolution.TabIndex = 14
        '
        'Panel1
        '
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.lbSolution)
        Me.Panel1.Controls.Add(Me.txtSolution)
        Me.Panel1.Controls.Add(Me.dgvError)
        Me.Panel1.Controls.Add(Me.txProbability)
        Me.Panel1.Controls.Add(Me.lbProbability)
        Me.Panel1.Controls.Add(Me.lbError2)
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Controls.Add(Me.cbError)
        Me.Panel1.Location = New System.Drawing.Point(-1, -2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(729, 458)
        Me.Panel1.TabIndex = 12
        '
        'dgvError
        '
        Me.dgvError.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvError.Enabled = False
        Me.dgvError.Location = New System.Drawing.Point(15, 301)
        Me.dgvError.Name = "dgvError"
        Me.dgvError.Size = New System.Drawing.Size(692, 150)
        Me.dgvError.TabIndex = 14
        Me.dgvError.Visible = False
        '
        'Guidance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(724, 297)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "Guidance"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "راهنمای سیستم"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgvError, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lbError2 As System.Windows.Forms.Label
    Friend WithEvents cbError As System.Windows.Forms.ComboBox
    Friend WithEvents lbProbability As System.Windows.Forms.Label
    Friend WithEvents lbSolution As System.Windows.Forms.Label
    Friend WithEvents txProbability As System.Windows.Forms.TextBox
    Friend WithEvents txtSolution As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents dgvError As System.Windows.Forms.DataGridView

End Class
