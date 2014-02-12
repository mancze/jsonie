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
			Return Me.writer.Encoding
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


	Private writer As TextWriter = Nothing

	Private pendingIndent As Boolean = False


	''' <summary>
	''' Decorates underlying writer with pretty JSON format.
	''' </summary>
	''' <param name="writer">Writer to decorate.</param>
	Public Sub New(writer As TextWriter)
		If writer Is Nothing Then
			Throw New ArgumentNullException("writer")
		End If

		Me.writer = writer
	End Sub


	''' <summary>
	''' Writes a character.
	''' </summary>
	''' <param name="value">Character to write.</param>
	Public Overrides Sub Write(value As Char)
		Select Case value
			Case "{"c, "["c
				Me.writer.Write(value)
				Me.Depth += 1
				Me.pendingIndent = True

			Case ","c
				Me.writer.Write(value)
				Me.WriteNewLine()
				Me.WriteIndent()

			Case ":"c
				Me.writer.Write(": ")

			Case "}"c, "]"c
				Me.Depth -= 1

				If pendingIndent Then
					' empty array/object
					Me.pendingIndent = False
					Me.writer.Write(" ")
				Else
					' non empty array/object
					Me.WriteNewLine()
					Me.WriteIndent()
				End If

				Me.writer.Write(value)

			Case Else
				Me.writer.Write(value)

		End Select
	End Sub


	''' <summary>
	''' For faster writing. Indent changes comes only when writing single char (relies on 
	''' implementation details of JsonEncoder).
	''' </summary>
	Public Overrides Sub Write(value As String)
		If Me.pendingIndent Then
			Me.pendingIndent = False
			Me.WriteNewLine()
			Me.WriteIndent()
		End If

		Me.writer.Write(value)
	End Sub


	''' <summary>
	''' Writes new line.
	''' </summary>
	Private Sub WriteNewLine()
		Me.writer.Write(Me.NewLine)
	End Sub


	''' <summary>
	''' Writes current indentations.
	''' </summary>
	Private Sub WriteIndent()
		For i = 0 To Me.Depth - 1
			Me.writer.Write(Me.Indent)
		Next
	End Sub

End Class
