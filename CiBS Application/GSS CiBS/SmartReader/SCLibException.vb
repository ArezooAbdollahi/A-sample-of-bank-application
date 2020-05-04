Imports System.Collections.Generic
Imports System.Text

Namespace SCLib
	Public NotInheritable Class SCLibException
        Inherits ApplicationException
        Private m_nCode As SCLibExceptionCode

		Public Sub New(nCode As SCLibExceptionCode)
			Me.m_nCode = nCode
		End Sub

		Public Sub New(nCode As SCLibExceptionCode, aInnerException As Exception)
			MyBase.New("", aInnerException)
			Me.m_nCode = nCode
		End Sub

		Public Sub New(nCode As SCLibExceptionCode, sMessage As String)
			MyBase.New(sMessage)
			Me.m_nCode = nCode
		End Sub
		Public ReadOnly Property Code() As SCLibExceptionCode
			Get
				Return Me.m_nCode
			End Get
		End Property



	End Class


End Namespace
