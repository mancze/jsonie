Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Globalization


''' <summary>
''' Classes responsible for JSON encoding.
''' </summary>
Public Class JsonEncoder

	''' <summary>
	''' Text writer used for output.
	''' </summary>
	Private writer As TextWriter = Nothing


	''' <summary>
	''' Options for encoder.
	''' </summary>
	Private options As JsonEncoderOptions = Nothing


	''' <summary>
	''' Creates new encoder with given options.
	''' </summary>
	''' <param name="options">Options of the encoder.</param>
	Public Sub New(Optional options As JsonEncoderOptions = Nothing)
		If options Is Nothing Then
			options = JsonEncoderOptions.Default
		End If

		Me.options = options
	End Sub


	''' <summary>
	''' Serializes JSON value into JSON string.
	''' </summary>
	''' <param name="value">Value which should be serialized.</param>
	''' <param name="writer">Text writer to which JSON will be written.</param>
	''' <exception cref="IOException">An I/O error occurs.</exception>
	''' <exception cref="ObjectDisposedException">The writer is closed.</exception>
	Public Sub Encode(value As JsonValue, writer As TextWriter)
		Try
			If Me.options.UsePrettyFormat Then
				writer = New JsonPrettyFormatWriter(writer)
			End If

			Me.writer = writer
			Me.Write(value)
		Finally
			Me.writer = Nothing
		End Try
	End Sub


	''' <summary>
	''' Writes JSON value.
	''' </summary>
	''' <param name="value">Value to write.</param>
	Private Sub Write(value As JsonValue)
		If value Is Nothing Then
			Me.writer.Write("null")

		ElseIf TypeOf value Is JsonBool Then
			Me.WriteBool(DirectCast(value, JsonBool))

		ElseIf TypeOf value Is JsonNumber Then
			Me.WriteNumber(DirectCast(value, JsonNumber))

		ElseIf TypeOf value Is JsonString Then
			Me.WriteString(DirectCast(value, JsonString))

		ElseIf TypeOf value Is JsonArray Then
			Me.WriteArray(DirectCast(value, JsonArray))

		ElseIf TypeOf value Is JsonObject Then
			Me.WriteObject(DirectCast(value, JsonObject))

		End If
	End Sub


	''' <summary>
	''' Writes JSON boolean.
	''' </summary>
	''' <param name="value">JSON boolean to write.</param>
	Private Sub WriteBool(value As JsonBool)
		Me.writer.Write(If(value.Value, "true", "false"))
	End Sub


	''' <summary>
	''' Writes JSON string.
	''' </summary>
	''' <param name="value">JSON string to write.</param>
	Private Sub WriteString(value As JsonString)
		Me.WriteStringWithEscaping(value.Value)
	End Sub


	''' <summary>
	''' Writes JSON number
	''' </summary>
	''' <param name="number">JSON number to write.</param>
	Private Sub WriteNumber(number As JsonNumber)
		' FIXME: access proper number type
		Me.writer.Write(number.DecimalValue.ToString(CultureInfo.InvariantCulture))
	End Sub


	''' <summary>
	''' Writes JSON array.
	''' </summary>
	''' <param name="value">JSON array to write.</param>
	Private Sub WriteArray(value As JsonArray)
		Dim isFirst = True

		Me.writer.Write("["c)

		For Each item In value
			If isFirst Then
				isFirst = False
			Else
				' separator
				Me.writer.Write(","c)
			End If

			Me.Write(item)
		Next

		Me.writer.Write("]"c)
	End Sub


	''' <summary>
	''' Writes JSON object.
	''' </summary>
	''' <param name="value">JSON object to write.</param>
	Private Sub WriteObject(value As JsonObject)
		Dim isFirst = True

		Me.writer.Write("{"c)

		For Each pair In value
			If isFirst Then
				isFirst = False
			Else
				' separator
				Me.writer.Write(","c)
			End If

			Me.WriteStringWithEscaping(pair.Key)
			Me.writer.Write(":"c)
			Me.Write(pair.Value)
		Next

		Me.writer.Write("}"c)
	End Sub


	''' <summary>
	''' Writes string with escaping.
	''' </summary>
	''' <param name="text">Text to escape &amp; write.</param>
	Private Sub WriteStringWithEscaping(text As String)
		Me.writer.Write("""")

		Static charLf As Char = CChar(vbLf)
		Static charCr As Char = CChar(vbCr)
		Static charTab As Char = CChar(vbTab)
		Static charBackspace As Char = CChar(vbBack)
		Static charFormFeed As Char = CChar(vbFormFeed)
		Static charSolidus As Char = "/"c

		Dim thisCharAsString As String = Nothing
		Dim curChar As Char = Nothing

		For index = 0 To text.Length - 1
			curChar = text(index)
			Select Case curChar
				Case "\"c
					Me.writer.Write("\\")

				Case """"c
					Me.writer.Write("\""")

				Case "/"c
					Me.writer.Write("\/")

				Case charLf
					Me.writer.Write("\n")

				Case charCr
					Me.writer.Write("\r")

				Case charTab
					Me.writer.Write("\t")

				Case charFormFeed
					Me.writer.Write("\f")

				Case charBackspace
					Me.writer.Write("\b")

				Case Else
					' escape control characters
					If Char.IsControl(curChar) Then
						Me.writer.Write("\u" & AscW(curChar).ToString("X4"))
					Else
						thisCharAsString = text(index)
						Me.writer.Write(thisCharAsString)
					End If
			End Select
		Next

		Me.writer.Write("""")
	End Sub

End Class
