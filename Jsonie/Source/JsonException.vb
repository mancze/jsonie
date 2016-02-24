''' <summary>
''' Thrown when error occurrs during JSON related processing.
''' </summary>
<Serializable>
Public Class JsonException
	Inherits Exception

	''' <summary>
	''' Creates new instance.
	''' </summary>
	Public Sub New()
		MyBase.New()
	End Sub


	''' <summary>
	''' Creates new instance with a specified message.
	''' </summary>
	''' <param name="message">The message that describes the error. Value can be null.</param>
	Public Sub New(message As String)
		MyBase.New(message)
	End Sub


	''' <summary>
	''' Creates new instance with a specified messageand a reference to the inner exception that is the cause of this 
	''' exception.
	''' </summary>
	''' <param name="message">The message that describes the error. Value can be null.</param>
	''' <param name="innerException">The exception that is the cause of the current exception. Value can be null.</param>
	Public Sub New(message As String, innerException As Exception)
		MyBase.New(message, innerException)
	End Sub

End Class
