Imports NUnit.Framework
Imports Dextronet.Jsonie


'''<summary>
'''This is a test class for JsonDecoder and is intended
'''to contain all JsonDecoder Unit Tests
'''</summary>
<TestFixture()>
Public Class JsonDecoderTest

#Region "Decode Null Tests"

	<Test()>
	Public Sub Decode_Null()
		Dim nullDecoded = JsonParser.Decode("null")
		Assert.AreEqual(Nothing, nullDecoded)
	End Sub


	<Test()>
	Public Sub Decode_EmptyInput()
		Dim nullDecoded = JsonParser.Decode("")
		Assert.AreEqual(Nothing, nullDecoded)
	End Sub

#End Region

#Region "Decode Boolean Tests"

	<Test()>
	Public Sub Decode_True()
		Dim trueDecoded = JsonParser.Decode("true")

		Assert.AreEqual(JsonBool.True, trueDecoded)
	End Sub


	<Test()>
	Public Sub Decode_False()
		Dim falseDecoded = JsonParser.Decode("false")

		Assert.AreEqual(JsonBool.False, falseDecoded)
	End Sub

#End Region

#Region "Decode Number Tests"

	<Test()>
	Public Sub Decode_PositiveInteger()
		Dim valueExpected = New JsonNumber(42)
		Dim valueDecoded = JsonParser.Decode("42")

		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub


	<Test()>
	Public Sub Decode_NegativeInteger()
		Dim valueExpected = New JsonNumber(-42)
		Dim valueDecoded = JsonParser.Decode("-42")

		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub


	<Test()>
	Public Sub Decode_Double()
		Dim valueExpected = New JsonNumber(-123456987.987R)
		Dim valueDecoded = JsonParser.Decode("-123456987.987")

		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub


	<Test()>
	Public Sub Decode_Decimal()
		Dim valueExpected = New JsonNumber(-123456789.987654321D)
		Dim valueDecoded = JsonParser.Decode("-123456789.987654321")

		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub


	<TestCase(100, "10E1")>
	<TestCase(100, "10E+1")>
	<TestCase(100, "1000E-1")>
	<TestCase(100, "10e1")>
	<TestCase(100, "10e+1")>
	<TestCase(100, "1e+2")>
	<TestCase(100, "1000e-1")>
	<TestCase(0.321, "321E-3")>
	<TestCase(0.321, "32.1E-2")>
	<TestCase(0.321, "3.21E-1")>
	<TestCase(0.321, "0.0321E1")>
	<TestCase(0.321, "0.00321E2")>
	<TestCase(0.321, "0.0321E+1")>
	Public Sub Decode_DecimalWithExponentialNotation(value As Decimal, encodedValue As String)
		Dim valueExpected = New JsonNumber(value)
		Dim valueDecoded = JsonParser.Decode(encodedValue)

		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub

#End Region

#Region "Decode String Tests"

	<Test()>
	Public Sub Decode_String()
		Dim valueExpected = New JsonString("Hello World!")
		Dim valueDecoded = JsonParser.Decode(Me.Quote("Hello World!"))

		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub


	<Test()>
	Public Sub Decode_EmptyString()
		Dim valueExpected = New JsonString(String.Empty)
		Dim valueDecoded = JsonParser.Decode(Me.Quote(String.Empty))

		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub


	<Test()>
	Public Sub Decode_EscapableString()
		Dim valueExpected = New JsonString("Hello ""World""! Two revers solidus go here \\")
		Dim valueDecoded = JsonParser.Decode("""Hello \""World\""! Two revers solidus go here \\\\""")

		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub


	<Test()>
	Public Sub Decode_EscapeCharacters()
		Dim escapeSeqs = {"\""", "\\", "\/", "\b", "\f", "\n", "\r", "\t", "\u0000", "\u1234"}

		For Each escapeSeq In escapeSeqs
			Dim valueExpected = New JsonString(escapeSeq)

			Dim escapedSeq = Me.Quote(escapeSeq.Replace("\", "\\").Replace("""", "\"""))
			Dim valueDecoded = JsonParser.Decode(escapedSeq)

			Assert.AreEqual(valueExpected, valueDecoded)
		Next
	End Sub


	<Test()>
	Public Sub Decode_ControlCharacters()
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
		controlSeqs("\u1234") = ChrW(4660)

		For Each controlSeq In controlSeqs
			Dim valueExpected = New JsonString(controlSeq.Value)

			Dim escapedSeq = Me.Quote(controlSeq.Key)
			Dim valueDecoded = JsonParser.Decode(escapedSeq)

			Assert.AreEqual(valueExpected, valueDecoded)
		Next
	End Sub


	<Test()>
	Public Sub Decode_First2049Chars_Hexescaped()
		For controlChar = 0 To 2048
			Dim valueExpected = New JsonString(ChrW(controlChar))

			Dim escapedSeq = Me.Quote("\u" & controlChar.ToString("X4"))
			Dim valueDecoded = JsonParser.Decode(escapedSeq)

			Assert.AreEqual(valueExpected, valueDecoded)
		Next
	End Sub


	<Test()>
	Public Sub Decode_MultilineStringCrLf()
		Dim words = "Hello World Each Word Is On Separate Line"

		Dim jsonValue = """" & words.Replace(" ", "\r\n") & """"
		Dim valueExpected = New JsonString(words.Replace(" ", vbCrLf))

		Dim valueDecoded = JsonParser.Decode(jsonValue)
		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub


	<Test()>
	Public Sub Decode_MultilineStringCr()
		Dim words = "Hello World Each Word Is On Separate Line"

		Dim jsonValue = """" & words.Replace(" ", "\r") & """"
		Dim valueExpected = New JsonString(words.Replace(" ", vbCr))

		Dim valueDecoded = JsonParser.Decode(jsonValue)
		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub


	<Test()>
	Public Sub Decode_MultilineStringLf()
		Dim words = "Hello World Each Word Is On Separate Line"

		Dim jsonValue = """" & words.Replace(" ", "\n") & """"
		Dim valueExpected = New JsonString(words.Replace(" ", vbLf))

		Dim valueDecoded = JsonParser.Decode(jsonValue)
		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub

#End Region

#Region "Decode Array Tests"

	<Test()>
	Public Sub Decode_EmptyArray()
		Dim valueDecoded = JsonParser.Decode("[]")

		Assert.IsTrue(TypeOf (valueDecoded) Is JsonArray, "Expected type JsonArray.")

		Dim arrayDecoded = DirectCast(valueDecoded, JsonArray)
		Assert.IsTrue(arrayDecoded.Count = 0, "Expected empty array.")
	End Sub


	<Test()>
	Public Sub Decode_SimpleArray()
		Dim jsonValue = "[ ""Hello World!"", 123.321, true, null, [], {} ]"
		Dim valueDecoded = JsonParser.Decode(jsonValue)

		Assert.IsTrue(TypeOf (valueDecoded) Is JsonArray, "Expected type JsonArray.")

		Dim arrayDecoded = DirectCast(valueDecoded, JsonArray)
		Assert.IsTrue(arrayDecoded.Count = 6, "Expected array length 6.")

		Assert.AreEqual(New JsonString("Hello World!"), arrayDecoded(0))
		Assert.AreEqual(New JsonNumber(123.321D), arrayDecoded(1))
		Assert.AreEqual(JsonBool.True, arrayDecoded(2))
		Assert.AreEqual(Nothing, arrayDecoded(3))
	End Sub

#End Region

#Region "Decode Object Tests"

	<Test()>
	Public Sub Decode_EmptyObject()
		Dim valueDecoded = JsonParser.Decode("{}")

		Assert.IsTrue(TypeOf (valueDecoded) Is JsonObject, "Expected type JsonObject.")

		Dim objectDecoded = DirectCast(valueDecoded, JsonObject)
		Assert.IsTrue(objectDecoded.Count = 0, "Expected empty object.")
	End Sub


	<Test()>
	Public Sub Decode_SimpleObject()
		Dim jsonValue = "{ ""string"": ""Hello World!"", ""number"": 123.321, ""bool"": true, ""null"": null, ""array"": [], ""object"": { } }"
		Dim valueDecoded = JsonParser.Decode(jsonValue)

		Assert.IsTrue(TypeOf (valueDecoded) Is JsonObject, "Expected type JsonObject.")

		Dim objectDecoded = DirectCast(valueDecoded, JsonObject)
		Assert.IsTrue(objectDecoded.Count = 6, "Expected 6 memebers in object.")

		Assert.IsTrue(objectDecoded.ContainsKey("string"), "Missing `string` member.")
		Assert.IsTrue(objectDecoded.ContainsKey("number"), "Missing `string` member.")
		Assert.IsTrue(objectDecoded.ContainsKey("bool"), "Missing `string` member.")
		Assert.IsTrue(objectDecoded.ContainsKey("null"), "Missing `string` member.")
		Assert.IsTrue(objectDecoded.ContainsKey("array"), "Missing `string` member.")
		Assert.IsTrue(objectDecoded.ContainsKey("object"), "Missing `string` member.")
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

End Class

