Imports System
Imports System.Globalization
Imports System.Text.RegularExpressions

Public Class CnvShamciToMiladi

    ' Public Class Extentions

    Public Shared Function ToPersianDate(ByVal dateTime As DateTime) As String
        Dim persianCalendar As PersianCalendar = New PersianCalendar
        Return String.Format("{0}/{1:00}/{2:00}", persianCalendar.GetYear(DateTime), persianCalendar.GetMonth(DateTime), persianCalendar.GetDayOfMonth(DateTime))
    End Function

    Public Shared Function ToPersianDateTime(ByVal dateTime As DateTime) As String
        Dim persianCalendar As PersianCalendar = New PersianCalendar
        Return String.Format("{0}/{1:00}/{2:00} 3'9* {3:00}:{4:00}", persianCalendar.GetYear(DateTime), persianCalendar.GetMonth(DateTime), persianCalendar.GetDayOfMonth(DateTime), persianCalendar.GetHour(DateTime), persianCalendar.GetMinute(DateTime))
    End Function

    Public Shared Function ToPersianTime(ByVal dateTime As DateTime) As String
        Dim persianCalendar As PersianCalendar = New PersianCalendar
        Return String.Format("3'9* {0:00}:{1:00}", persianCalendar.GetHour(DateTime), persianCalendar.GetMinute(DateTime))
    End Function

    Public Shared Function ToDateTime(ByVal objDate As String) As DateTime
        Dim year As Integer = 0
        Dim month As Integer = 0
        Dim day As Integer = 0
        Dim persianCalendar As PersianCalendar = New PersianCalendar
        Dim date0 As String = CType(objDate, String)
        Dim match As Match
        If Regex.IsMatch(date0, "^((0?[1-9]|[12][0-9]|3[01])[- /.](0?[1-9]|1[012])[- /.](13|14)?\d{2})|((13|14)\d{2}[- /.](0?[1-9]|1[0" & _
            "12])[- /.](0?[1-9]|[12][0-9]|3[01]))$") Then
            match = Regex.Match(date0, "^((0?[1-9]|[12][0-9]|3[01])[- /.](0?[1-9]|1[012])[- /.]((13|14)?\d{2}))|(((13|14)\d{2})[- /.](0?[1-9]" & _
                "|1[012])[- /.](0?[1-9]|[12][0-9]|3[01]))$")
            If match.Groups(1).Success Then
                day = Convert.ToInt32(match.Groups(2).Value)
                month = Convert.ToInt32(match.Groups(3).Value)
                If match.Groups(5).Success Then
                    year = Convert.ToInt32(match.Groups(4).Value)
                Else
                    year = Convert.ToInt32(String.Format("{0:00}{1:00}", (persianCalendar.GetYear(DateTime.Now) / 100), match.Groups(4).Value))
                End If

            Else
                day = Convert.ToInt32(match.Groups(10).Value)
                month = Convert.ToInt32(match.Groups(9).Value)
                year = Convert.ToInt32(match.Groups(7).Value)
            End If

        Else
            Throw New Exception("Invalid Date Expression")
        End If

        Return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0)
    End Function
End Class
