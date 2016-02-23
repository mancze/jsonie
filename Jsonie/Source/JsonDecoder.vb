Imports System.IO
Imports System.Text
Imports System.Globalization


''' <summary>
''' Classes responsible for JSON decoding.
''' </summary>
Public Class JsonDecoder

	Private Shared ReadOnly SpaceCharCode As Integer = AscW(" ")
	Private Shared ReadOnly TabCharCode As Integer = AscW(vbTab)
	Private Shared ReadOnly LineFeedCharCode As Integer = AscW(vbLf)
	Private Shared ReadOnly CarriageReturnCharCode As Integer = AscW(vbCr)
	Private Shared ReadOnly MaxWhiteSpaceCharCode As Integer = Math.Max(Math.Max(SpaceCharCode, TabCharCode), Math.Max(LineFeedCharCode, CarriageReturnCharCode))


	Private _options As JsonDecoderOptions = Nothing
	Private _reader As TextReader = Nothing

	Private _position As Integer = 1
	Private _line As Integer = 1
	Private _column As Integer = 1

	Private _bufferedCharValid As Boolean = False
	Private _bufferedChar As Char = Nothing


	''' <summary>
	''' Creates new decoder with given options.
	''' </summary>
	''' <param name="options">Options of the decoder.</param>
	Public Sub New(Optional options As JsonDecoderOptions = Nothing)
		If options Is Nothing Then
			options = JsonDecoderOptions.Default
		End If

		Me._options = options
	End Sub


	''' <summary>
	''' Deserializes JSON encoded string into object representation.
	''' </summary>
	''' <param name="reader">Reader of the json string.</param>
	''' <returns>Decoded JSON value.</returns>
	''' <exception cref="JsonFormatException">When json string is invalid.</exception>
	''' <exception cref="IOException">An I/O error occurs.</exception>
	''' <exception cref="ObjectDisposedException">The reader is closed.</exception>
	Public Function Decode(reader As TextReader) As JsonValue
		Try
			Me._reader = reader

			' is reader input empty?
			Dim isEos = False
			Me.PeekChar(isEos)

			If isEos Then
				Return Nothing
			End If

			Return Me.ReadValue()
		Finally
			Me._reader = Nothing
			Me._position = 1
			Me._line = 1
			Me._column = 1
		End Try
	End Function


	Private Function ReadValue() As JsonValue
		Dim result As JsonValue = Nothing
		Dim peek = Me.PeekToken()

		Select Case peek
			Case Token.ObjectBegin
				Return Me.ReadObject()

			Case Token.ArrayBegin
				Return Me.ReadArray()

			Case Token.Null
				Return Me.ReadNull()

			Case Token.False
				Return Me.ReadFalse()

			Case Token.True
				Return Me.ReadTrue()

			Case Token.String
				Return Me.ReadString()

			Case Token.Number
				Return Me.ReadNumber()

			Case Else
				Dim peekChar = Me.PeekChar()
				Throw CreateFormatException("Unexpected token " & peek.ToString() & " (char `" & peekChar & "`, int " & AscW(peekChar) & ").")

		End Select
	End Function


	Private Function ReadNull() As JsonValue
		Dim chars(3) As Char
		chars(0) = Me.ReadChar()
		chars(1) = Me.ReadChar()
		chars(2) = Me.ReadChar()
		chars(3) = Me.ReadChar()

		' we don't need to test first char - its token
		If chars(1) = "u"c AndAlso chars(2) = "l"c AndAlso chars(3) = "l"c Then
			' null
			Return Nothing
		Else
			' something else
			Throw CreateFormatException("Unexpected data. Chars `null` expected but `" & New String(chars) & "` found.")
		End If
	End Function


	Private Function ReadFalse() As JsonBool
		Dim chars(4) As Char
		chars(0) = Me.ReadChar()
		chars(1) = Me.ReadChar()
		chars(2) = Me.ReadChar()
		chars(3) = Me.ReadChar()
		chars(4) = Me.ReadChar()

		' we don't need to test first char - its token
		If chars(1) = "a"c AndAlso chars(2) = "l"c AndAlso chars(3) = "s"c AndAlso chars(4) = "e"c Then
			' false
			Return JsonBool.False
		Else
			' something else
			Throw CreateFormatException("Unexpected data. Chars `false` expected but `" & New String(chars) & "` found.")
		End If
	End Function


	Private Function ReadTrue() As JsonBool
		Dim chars(3) As Char
		chars(0) = Me.ReadChar()
		chars(1) = Me.ReadChar()
		chars(2) = Me.ReadChar()
		chars(3) = Me.ReadChar()

		' we don't need to test first char - its token
		If chars(1) = "r"c AndAlso chars(2) = "u"c AndAlso chars(3) = "e"c Then
			' null
			Return JsonBool.True
		Else
			' something else
			Throw CreateFormatException("Unexpected data. Chars `true` expected but `" & New String(chars) & "` found.")
		End If
	End Function


	Private Function ReadString() As JsonString
		' consume starting "
		Me.ReadChar()

		Dim value = Me.ReadStringValue()

		' consume ending "
		Me.ReadChar()

		Return New JsonString(value)
	End Function


	Private Function ReadNumber() As JsonNumber
		Dim digits = New StringBuilder()
		Dim peek As Char = Nothing
		Dim isEndOfStream = False

		Do
			digits.Append(Me.ReadChar())
			peek = Me.PeekChar(isEndOfStream)
		Loop While Not isEndOfStream AndAlso ((peek >= "0"c AndAlso peek <= "9"c) OrElse peek = "." OrElse peek = "+"c OrElse peek = "-"c OrElse peek = "e"c OrElse peek = "E"c)

		Dim decimalValue = Decimal.Parse(digits.ToString(), NumberStyles.AllowExponent Or NumberStyles.AllowDecimalPoint Or NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture)
		Return New JsonNumber(decimalValue)
	End Function


	Private Function ReadObject() As JsonObject
		' consume {
		Me.ReadChar()

		Dim result = New JsonObject()

		Do
			' key
			Dim peek = Me.PeekToken()

			If peek = Token.ObjectEnd Then
				Exit Do
			ElseIf peek = Token.Comma Then
				' consume , (memeber separator)
				Me.ReadChar()
				Continue Do
			ElseIf Not peek = Token.String Then
				Throw CreateFormatException("Double quotes were expected.")
			End If

			' consume "
			Me.ReadChar()

			Dim key = Me.ReadStringValue()

			' consume "
			Me.ReadChar()

			' colon
			peek = Me.PeekToken

			If Not peek = Token.Colon Then
				Throw CreateFormatException("Colon was expected.")
			End If

			' consume colon
			Me.ReadChar()

			' value
			Dim value = Me.ReadValue()

			' set member
			result(key) = value
		Loop

		' consume }
		Me.ReadChar()

		Return result
	End Function


	Private Function ReadArray() As JsonArray
		' consume [
		Me.ReadChar()

		Dim result = New JsonArray()

		Do
			Dim peek = Me.PeekToken()

			If peek = Token.ArrayEnd Then
				Exit Do
			ElseIf peek = Token.Comma Then
				' consume ,
				Me.ReadChar()
			End If

			' value
			Dim value = Me.ReadValue()
			result.Add(value)
		Loop

		' consume ]
		Me.ReadChar()

		Return result
	End Function


	''' <summary>
	''' Reads string from reader.
	''' </summary>
	''' <returns>String until next unescaped double quotes.</returns>
	Private Function ReadStringValue() As String
		Dim builder = New StringBuilder()

		Do
			Dim peek As Char = Me.PeekChar()

			If peek = """"c Then
				Exit Do
			ElseIf peek = "\"c Then
				' escape seq
				Me.ReadChar()

				Select Case Me.ReadChar()
					Case """"c
						builder.Append("""")

					Case "\"c
						builder.Append("\")

					Case "/"c
						builder.Append("/")

					Case "b"c
						builder.Append(vbBack)

					Case "f"c
						builder.Append(vbFormFeed)

					Case "n"c
						builder.Append(vbLf)

					Case "r"c
						builder.Append(vbCr)

					Case "t"c
						builder.Append(vbTab)

					Case "u"c
						' unicode escape seq
						Dim chars(3) As Char
						chars(0) = Me.ReadChar()
						chars(1) = Me.ReadChar()
						chars(2) = Me.ReadChar()
						chars(3) = Me.ReadChar()
						builder.Append(Me.ParseUnicode(chars))

				End Select
			Else
				builder.Append(Me.ReadChar())
			End If
		Loop

		Return builder.ToString()
	End Function


	Private Function ParseUnicode(chars As Char()) As Char
		Dim p1 = ParseSingleChar(chars(0), &H1000)
		Dim p2 = ParseSingleChar(chars(1), &H100)
		Dim p3 = ParseSingleChar(chars(2), &H10)
		Dim p4 = ParseSingleChar(chars(3), 1)

		Return ChrW(p1 + p2 + p3 + p4)
	End Function


	Private Function ParseSingleChar(ch As Char, multipliyer As Integer) As Integer
		Dim p1 As Integer = 0

		If ch >= "0"c AndAlso ch <= "9"c Then
			p1 = CInt(AscW(ch) - AscW("0"c)) * multipliyer

		ElseIf ch >= "A"c AndAlso ch <= "F"c Then
			p1 = CInt(AscW(ch) - AscW("A"c) + 10) * multipliyer

		ElseIf ch >= "a"c AndAlso ch <= "f"c Then
			p1 = CInt(AscW(ch) - AscW("a"c) + 10) * multipliyer

		End If

		Return p1
	End Function

#Region "Helpers"

	Private Function IsWhiteSpace(charCode As Integer) As Boolean
		If charCode > MaxWhiteSpaceCharCode Then
			Return False
		ElseIf charCode = SpaceCharCode OrElse charCode = TabCharCode OrElse charCode = LineFeedCharCode OrElse charCode = CarriageReturnCharCode Then
			Return True
		Else
			Return False
		End If
	End Function


	Private Function PeekToken() As Token
		Dim peek As Char = Me.PeekChar()

		Do While Me.IsWhiteSpace(AscW(peek))
			Me._bufferedCharValid = False ' this will cause to read next char
			peek = Me.PeekChar()
		Loop

		Select Case peek
			Case "{"c
				Return Token.ObjectBegin

			Case "}"c
				Return Token.ObjectEnd

			Case "["c
				Return Token.ArrayBegin

			Case "]"c
				Return Token.ArrayEnd

			Case ","c
				Return Token.Comma

			Case ":"c
				Return Token.Colon

			Case """"c
				Return Token.String

			Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "+"c, "-"c, "."c
				Return Token.Number

			Case "f"c
				Return Token.False

			Case "t"c
				Return Token.True

			Case "n"c
				Return Token.Null
		End Select

		Return Token.Invalid
	End Function


	Private Function PeekChar() As Char
		If Not Me._bufferedCharValid Then
			Me._bufferedChar = Me.ReadChar()
			Me._bufferedCharValid = True
		End If

		Return Me._bufferedChar
	End Function


	Private Function PeekChar(ByRef isEndOfStream As Boolean) As Char
		isEndOfStream = False
		If Not Me._bufferedCharValid Then
			Me._bufferedChar = Me.ReadChar(isEndOfStream)

			If isEndOfStream Then
				Return Nothing
			End If

			Me._bufferedCharValid = True
		End If

		Return Me._bufferedChar
	End Function


	Private Function ReadChar() As Char
		Dim isEndOfStream = False
		Dim result = Me.ReadChar(isEndOfStream)

		If isEndOfStream Then
			Throw CreateFormatException("Unexpected end of input.")
		Else
			Return result
		End If
	End Function


	Private Function ReadChar(ByRef isEndOfStream As Boolean) As Char
		isEndOfStream = False

		If Me._bufferedCharValid Then
			' take char from buffered
			Me._bufferedCharValid = False
			Return Me._bufferedChar
		End If

		Dim charCode = Me._reader.Read()

		If charCode < 0 Then
			isEndOfStream = True
			Return Nothing
		ElseIf charCode = LineFeedCharCode Then
			Me._line += 1
			Me._column = 0
		ElseIf charCode = CarriageReturnCharCode Then
			Me._column -= 1
		End If

		Me._position += 1
		Me._column += 1

		Return ChrW(charCode)
	End Function


	Private Function CreateFormatException(message As String) As JsonFormatException
		Return New JsonFormatException(message, Me._line, Me._column - 1, Me._position - 1)
	End Function

#End Region

End Class
