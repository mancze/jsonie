Imports NUnit.Framework
Imports Dextronet.Jsonie
Imports System.Text.RegularExpressions
Imports System.Globalization


'''<summary>
'''This is a test class for JsonEncoder and is intended
'''to contain all JsonBoolTest Unit Tests
'''</summary>
<TestFixture()>
Public Class JsonEncoderTest

#Region "Encode Null Tests"

	<Test()>
	Public Sub Encode_Null_ReturnsCorrectJson()
		Dim nullValue As JsonValue = Nothing

		Dim jsonString = JsonParser.Encode(nullValue)
		Assert.AreEqual("null", jsonString)
	End Sub


	<Test()>
	Public Sub Encode_BoolNull_ReturnsCorrectJson()
		Dim nullValue As JsonBool = Nothing

		Dim jsonString = JsonParser.Encode(nullValue)
		Assert.AreEqual("null", jsonString)
	End Sub

#End Region

#Region "Encode Bool Tests"

	<Test()>
	Public Sub Encode_True_ReturnsCorrectJson()
		Dim value = JsonBool.True

		Dim jsonString = JsonParser.Encode(value)
		Assert.AreEqual("true", jsonString)
	End Sub


	<Test()>
	Public Sub Encode_False_ReturnsCorrectJson()
		Dim value = JsonBool.False

		Dim jsonString = JsonParser.Encode(value)
		Assert.AreEqual("false", jsonString)
	End Sub

#End Region

#Region "Encode Number Tests"

	<Test()>
	Public Sub Encode_PositiveInteger_ReturnsCorrectJson()
		Dim intValue = 42
		Dim value = New JsonNumber(42)

		Dim jsonString = JsonParser.Encode(value)
		Assert.AreEqual(intValue.ToString(CultureInfo.InvariantCulture), jsonString)
	End Sub


	<Test()>
	Public Sub Encode_NegativeInteger_ReturnsCorrectJson()
		Dim commonValue = -42
		Dim value = New JsonNumber(commonValue)

		Dim jsonString = JsonParser.Encode(value)
		Assert.AreEqual("-42", jsonString)
	End Sub


	<Test()>
	Public Sub Encode_Double_ReturnsCorrectJson()
		Dim commonValue = -123456987.987R
		Dim value = New JsonNumber(commonValue)

		Dim jsonString = JsonParser.Encode(value)
		Assert.AreEqual("-123456987.987", jsonString)
	End Sub


	<Test()>
	Public Sub Encode_Decimal_ReturnsCorrectJson()
		Dim commonValue = 123456789.987654321D
		Dim value = New JsonNumber(commonValue)

		Dim jsonString = JsonParser.Encode(value)
		Assert.AreEqual(commonValue.ToString(CultureInfo.InvariantCulture), jsonString)
	End Sub

#End Region

#Region "Encode String Tests"

	<Test()>
	Public Sub Encode_String_ReturnsCorrectJson()
		Dim commonValue = "Hello World!"
		Dim value = New JsonString(commonValue)

		Dim jsonString = JsonParser.Encode(value)
		Assert.AreEqual(Me.Quote(commonValue), jsonString)
	End Sub


	<Test()>
	Public Sub Encode_EmptyString_ReturnsCorrectJson()
		Dim commonValue = String.Empty
		Dim value = New JsonString(commonValue)

		Dim jsonString = JsonParser.Encode(value)
		Assert.AreEqual(Me.Quote(commonValue), jsonString)
	End Sub


	<Test()>
	Public Sub Encode_EscapableString_ReturnsCorrectJson()
		Dim commonValue = "Hello ""World""! Two revers solidus go here \\"
		Dim expectedValue = "Hello \""World\""! Two revers solidus go here \\\\"
		Dim value = New JsonString(commonValue)

		Dim jsonString = JsonParser.Encode(value)
		Assert.AreEqual(Me.Quote(expectedValue), jsonString)
	End Sub


	<Test()>
	Public Sub Encode_EscapeCharacters_ReturnsCorrectJson()
		Dim escapeSeqs = {"\""", "\", "\/", "\b", "\f", "\n", "\r", "\t", "\u1234", "\u0000"}

		For Each escapeSeq In escapeSeqs
			Dim commonValue = escapeSeq
			Dim expectedValue = commonValue.Replace("\", "\\").Replace("""", "\""").Replace("/", "\/")
			Dim value = New JsonString(commonValue)

			Dim jsonString = JsonParser.Encode(value)
			Assert.AreEqual(Me.Quote(expectedValue), jsonString)
		Next
	End Sub


	<Test()>
	Public Sub Encode_ControlCharacters()
		' json -> control char
		Dim controlSeqs = New Dictionary(Of String, String)
		controlSeqs("\""") = """"
		controlSeqs("\\") = "\"
		controlSeqs("\b") = vbBack
		controlSeqs("\f") = vbFormFeed
		controlSeqs("\n") = vbLf
		controlSeqs("\r") = vbCr
		controlSeqs("\t") = vbTab
		controlSeqs("\u0000") = vbNullChar

		For Each controlChar In controlSeqs
			Dim value = New JsonString(controlChar.Value)

			Dim jsonString = JsonParser.Encode(value)
			Assert.AreEqual(Me.Quote(controlChar.Key), jsonString)
		Next

		' c0 and c1 sets
		Dim offsets = {0, 128}

		For Each offset In offsets
			For charIndex = offset To offset + 31
				Dim curChar As Char = ChrW(charIndex)

				If curChar = """" OrElse curChar = vbBack OrElse curChar = vbFormFeed OrElse curChar = vbLf OrElse curChar = vbCr OrElse curChar = vbTab OrElse curChar = " " Then
					' encoder may use shorter representation (e.g. "\b" for backspace)
					Continue For
				End If

				Dim expectedValue = "\u" & charIndex.ToString("X4")
				Dim value = New JsonString(curChar)

				Dim jsonString = JsonParser.Encode(value)
				Assert.AreEqual(Me.Quote(expectedValue), jsonString)
			Next
		Next
	End Sub


	<Test()>
	Public Sub Encode_MultilineStringCrLf_ReturnsCorrectJson()
		Dim words = "Hello World Each Word Is On Separate Line"
		Dim multilineText = words.Replace(" ", vbCrLf)
		Dim expectedValue = words.Replace(" ", "\r\n")

		Dim value = New JsonString(multilineText)

		Dim jsonString = JsonParser.Encode(value)
		Assert.AreEqual(Me.Quote(expectedValue), jsonString)
	End Sub


	<Test()>
	Public Sub Encode_MultilineStringCr_ReturnsCorrectJson()
		Dim words = "Hello World Each Word Is On Separate Line"
		Dim multilineText = words.Replace(" ", vbCr)
		Dim expectedValue = words.Replace(" ", "\r")

		Dim value = New JsonString(multilineText)

		Dim jsonString = JsonParser.Encode(value)
		Assert.AreEqual(Me.Quote(expectedValue), jsonString)
	End Sub


	<Test()>
	Public Sub Encode_MultilineStringLf_ReturnsCorrectJson()
		Dim words = "Hello World Each Word Is On Separate Line"
		Dim multilineText = words.Replace(" ", vbLf)
		Dim expectedValue = words.Replace(" ", "\n")

		Dim value = New JsonString(multilineText)

		Dim jsonString = JsonParser.Encode(value)
		Assert.AreEqual(Me.Quote(expectedValue), jsonString)
	End Sub

#End Region

#Region "Encode Array Tests"

	<Test()>
	Public Sub Encode_EmptyArray_ReturnsCorrectJson()
		Dim value = New JsonArray()

		Dim jsonString = JsonParser.Encode(value)
		Assert.AreEqual("[]", jsonString)
	End Sub


	<Test()>
	Public Sub Encode_SimpleArray_ReturnsCorrectJson()
		Dim value = New JsonArray()
		value.Add("Hello World!")
		value.Add(123.321D)
		value.Add(True)
		value.Add(Nothing)
		value.Add(New JsonArray())
		value.Add(New JsonObject())

		Dim expectedValue = "[ ""Hello World!"", 123.321, true, null, [], {} ]"

		Dim jsonString = JsonParser.Encode(value)
		Assert.IsTrue(Me.CompareJsonStrings(expectedValue, jsonString))
	End Sub

#End Region

#Region "Encode Object Tests"

	<Test()>
	Public Sub Encode_EmptyObject_ReturnsCorrectJson()
		Dim value = New JsonObject()

		Dim jsonString = JsonParser.Encode(value)
		Assert.AreEqual("{}", jsonString)
	End Sub


	<Test()>
	Public Sub Encode_SimpleObject_ReturnsCorrectJson()
		Dim value = New JsonObject()
		value("string") = "Hello World!"
		value("number") = 123.321
		value("bool") = True
		value("null") = Nothing
		value("array") = New JsonArray()
		value("object") = New JsonObject()

		Dim expectedValue = "{ ""string"": ""Hello World!"", ""number"": 123.321, ""bool"": true, ""null"": null, ""array"": [], ""object"": { } }"

		Dim jsonString = JsonParser.Encode(value)
		Assert.IsTrue(Me.CompareJsonStrings(expectedValue, jsonString))
	End Sub


#End Region

	''' <summary>
	''' Returns text quoted by " characters both at start and end. No escapes.
	''' </summary>
	''' <param name="text">Text to quote.</param>
	''' <returns>Quoted text.</returns>
	Private Function Quote(text As String) As String
		If text Is Nothing Then
			Return Nothing
		End If

		Return """" & text & """"
	End Function


	Private Function CompareJsonStrings(formerText As String, latterText As String) As Boolean
		' for simple implementation of comparison just ignore any whitespace difference
		' error in encoding of strings should be covered by some other tests
		Dim whitespacePattern = "\s+"

		Dim formerTextNoWhitespace = Regex.Replace(formerText, whitespacePattern, "")
		Dim latterTextNoWhitespace = Regex.Replace(latterText, whitespacePattern, "")

		Return formerTextNoWhitespace = latterTextNoWhitespace
	End Function

End Class

