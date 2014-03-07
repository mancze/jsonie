''' <summary>
''' Thrown when JSON format is invalid.
''' </summary>
<Serializable>
Public Class JsonFormatException
	Inherits JsonException


	''' <summary>
	''' Line where error occured.
	''' </summary>
	Public ReadOnly Property Line As Integer
		Get
			Return _line
		End Get
	End Property
	Private _line As Integer = 0


	''' <summary>
	''' Column offset where error occured (tab counts as 1 character, carriage-return ignored).
	''' </summary>
	Public ReadOnly Property Column As Integer
		Get
			Return _column
		End Get
	End Property
	Private _column As Integer = 0


	''' <summary>
	''' Total offset from the beginning of the parsing.
	''' </summary>
	Public ReadOnly Property Position As Integer
		Get
			Return _position
		End Get
	End Property
	Private _position As Integer = 0


	Public Sub New(line As Integer, column As Integer, position As Integer)
		MyBase.New()
		Me._line = line
		Me._column = column
		Me._position = position
	End Sub


	Public Sub New(message As String, line As Integer, column As Integer, position As Integer)
		MyBase.New(message)
		Me._line = line
		Me._column = column
		Me._position = position
	End Sub

End Class
