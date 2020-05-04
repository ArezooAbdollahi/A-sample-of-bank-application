Imports System.Runtime.InteropServices

Public Class GetLastUserInput



    Private Declare Function GetLastInputInfo Lib "User32.dll" (ByRef lii As LASTINPUTINFO) As Boolean

    <StructLayout(LayoutKind.Sequential)> Public Structure LASTINPUTINFO

        Public cbSize As Int32

        Public dwTime As Int32

    End Structure

    Public Shared ReadOnly Property IdleTimeInTicks() As Int32

        Get

            Dim lii As New LASTINPUTINFO

            lii.cbSize = Marshal.SizeOf(lii)

            If GetLastInputInfo(lii) Then

                Return Environment.TickCount - lii.dwTime

            End If

        End Get

    End Property

End Class
