<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DatePicker
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DatePicker))
        Me.PersianMonthCalendar1 = New FreeControls.PersianMonthCalendar()
        Me.SuspendLayout()
        '
        'PersianMonthCalendar1
        '
        Me.PersianMonthCalendar1.BackColor = System.Drawing.Color.White
        Me.PersianMonthCalendar1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PersianMonthCalendar1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.PersianMonthCalendar1.Location = New System.Drawing.Point(0, 0)
        Me.PersianMonthCalendar1.MarkColor = System.Drawing.Color.Green
        Me.PersianMonthCalendar1.MaximumSize = New System.Drawing.Size(325, 172)
        Me.PersianMonthCalendar1.MinimumSize = New System.Drawing.Size(325, 172)
        Me.PersianMonthCalendar1.Name = "PersianMonthCalendar1"
        Me.PersianMonthCalendar1.ShowToday = True
        Me.PersianMonthCalendar1.Size = New System.Drawing.Size(325, 172)
        Me.PersianMonthCalendar1.TabIndex = 0
        Me.PersianMonthCalendar1.Text = "PersianMonthCalendar1"
        Me.PersianMonthCalendar1.TitleBackColor = System.Drawing.SystemColors.ActiveCaption
        Me.PersianMonthCalendar1.TitleForeColor = System.Drawing.Color.White
        Me.PersianMonthCalendar1.Value = CType(resources.GetObject("PersianMonthCalendar1.Value"), FreeControls.PersianDate)
        '
        'DatePicker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(327, 173)
        Me.ControlBox = False
        Me.Controls.Add(Me.PersianMonthCalendar1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "DatePicker"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PersianMonthCalendar1 As FreeControls.PersianMonthCalendar
End Class
