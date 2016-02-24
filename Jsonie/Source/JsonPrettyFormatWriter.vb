Imports System.IO


''' <summary>
''' Pretty format JSON writer. Makes JSON human-readable. Decorates underlying TextWriter.
''' </summary>
Public Class JsonPrettyFormatWriter
	Inherits TextWriter


	''' <summary>
	''' Gets/sets string used for indentation.
	''' </summary>
	Public Property Indent As String = vbTab


	''' <summary>
	''' Gets encoding of the underlying writer.
	''' </summary>
	Public Overrides ReadOnly Property Encoding As System.Text.Encoding
		Get
			Return Me._writer.Encoding
		End Get
	End Property


	''' <summary>
	''' Gets/sets current indent depth. Must be non-negative.
	''' </summary>
	Private Property Depth As Integer
		Get
			Return _depth
		End Get
		Set(value As Integer)
			If value < 0 Then
				Trace.Fail("Indent level negative.")
				_depth = 0
			Else
				_depth = value
			End If
		End Set
	End Property
	Private _depth As Integer = 0


	Private _writer As TextWriter = Nothing
	Private _hasPendingIndent As Boolean = False


	''' <summary>
	''' Decorates underlying writer with pretty JSON format.
	''' </summary>
	''' <param name="writer">Writer to decorate.</param>
	Public Sub New(writer As TextWriter)
		If writer Is Nothing Then
			Throw New ArgumentNullException("writer")
		End If

		Me._writer = writer
	End Sub


	''' <summary>
	''' Writes a character.
	''' </summary>
	''' <param name="value">Character to write.</param>
	Public Overrides Sub Write(value As Char)
		Select Case value
			Case "{"c, "["c
				Me._writer.Write(value)
				Me.Depth += 1
				Me._hasPendingIndent = True

			Case ","c
				Me._writer.Write(value)
				Me.WriteNewLine()
				Me.WriteIndent()

			Case ":"c
				Me._writer.Write(": ")

			Case "}"c, "]"c
				Me.Depth -= 1

				If _hasPendingIndent Then
					' empty array/object
					Me._hasPendingIndent = False
					Me._writer.Write(" ")
				Else
					' non empty array/object
					Me.WriteNewLine()
					Me.WriteIndent()
				End If

				Me._writer.Write(value)

			Case Else
				Me._writer.Write(value)

		End Select
	End Sub


	''' <summary>
	''' For faster writing. Indent changes comes only when writing single char (relies on 
	''' implementation details of JsonEncoder).
	''' </summary>
	''' <param name="value">The string to write.</param>
	Public Overrides Sub Write(value As String)
		If Me._hasPendingIndent Then
			Me._hasPendingIndent = False
			Me.WriteNewLine()
			Me.WriteIndent()
		End If

		Me._writer.Write(value)
	End Sub


	''' <summary>
	''' Writes new line.
	''' </summary>
	Private Sub WriteNewLine()
		Me._writer.Write(Me.NewLine)
	End Sub


	''' <summary>
	''' Writes current indentations.
	''' </summary>
	Private Sub WriteIndent()
		For i = 0 To Me.Depth - 1
			Me._writer.Write(Me.Indent)
		Next
	End Sub

End Class
