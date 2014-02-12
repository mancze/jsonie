''' <summary>
''' Base class for JSON related exceptions.
''' </summary>
<Serializable>
Public Class JsonException
	Inherits Exception

	Public Sub New()
		MyBase.New()
	End Sub

	Public Sub New(message As String)
		MyBase.New(message)
	End Sub

	Public Sub New(message As String, innerException As Exception)
		MyBase.New(message, innerException)
	End Sub

End Class
