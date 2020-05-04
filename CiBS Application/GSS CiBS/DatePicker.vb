
Public Class DatePicker
    Private vData As String = ""

    Public Property Data As String
        Get
            Return vData
        End Get
        Set(ByVal value As String)
            vData = value
        End Set
    End Property
    Private vX As String = ""

    Public Property x As String
        Get
            Return vX
        End Get
        Set(ByVal value As String)
            vX = value
        End Set
    End Property

    Private vy As String = ""
    Public Property y As String
        Get
            Return vy
        End Get
        Set(ByVal value As String)
            vy = value
        End Set
    End Property

    Private Sub PersianMonthCalendar1_ValueChanged(ByVal sender As Object, ByVal e As FreeControls.PersianMonthCalendarEventArgs) Handles PersianMonthCalendar1.ValueChanged
        Data = e.CurrentValue.ToString.Substring(0, 10)
        If e.CurrentValue.Month = e.OldValue.Month Then Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

  
End Class