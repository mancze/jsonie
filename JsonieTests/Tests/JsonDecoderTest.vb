Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Dextronet.Jsonie


'''<summary>
'''This is a test class for JsonDecoder and is intended
'''to contain all JsonDecoder Unit Tests
'''</summary>
<TestClass()>
Public Class JsonDecoderTest

#Region "Decode Null Tests"

	<TestMethod()>
	Public Sub Decode_Null_ReturnsCorrectObject()
		Dim nullDecoded = JsonParser.Decode("null")
		Assert.AreEqual(Nothing, nullDecoded)
	End Sub


	<TestMethod()>
	Public Sub Decode_EmptyInput_ReturnsCorrectObject()
		Dim nullDecoded = JsonParser.Decode("")
		Assert.AreEqual(Nothing, nullDecoded)
	End Sub

#End Region

#Region "Decode Boolean Tests"

	<TestMethod()>
	Public Sub Decode_True_ReturnsCorrectObject()
		Dim trueDecoded = JsonParser.Decode("true")

		Assert.AreEqual(JsonBool.True, trueDecoded)
	End Sub


	<TestMethod()>
	Public Sub Decode_False_ReturnsCorrectObject()
		Dim falseDecoded = JsonParser.Decode("false")

		Assert.AreEqual(JsonBool.False, falseDecoded)
	End Sub

#End Region

#Region "Decode Number Tests"

	<TestMethod()>
	Public Sub Decode_PositiveInteger_ReturnsCorrectObject()
		Dim valueExpected = New JsonNumber(42)
		Dim valueDecoded = JsonParser.Decode("42")

		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub


	<TestMethod()>
	Public Sub Decode_NegativeInteger_ReturnsCorrectObject()
		Dim valueExpected = New JsonNumber(-42)
		Dim valueDecoded = JsonParser.Decode("-42")

		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub


	<TestMethod()>
	Public Sub Decode_Double_ReturnsCorrectObject()
		Dim valueExpected = New JsonNumber(-123456987.987R)
		Dim valueDecoded = JsonParser.Decode("-123456987.987")

		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub


	<TestMethod()>
	Public Sub Decode_Decimal_ReturnsCorrectObject()
		Dim valueExpected = New JsonNumber(-123456789.987654321D)
		Dim valueDecoded = JsonParser.Decode("-123456789.987654321")

		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub

#End Region

#Region "Decode String Tests"

	<TestMethod()>
	Public Sub Decode_String_ReturnsCorrectObject()
		Dim valueExpected = New JsonString("Hello World!")
		Dim valueDecoded = JsonParser.Decode("""Hello World!""")

		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub


	<TestMethod()>
	Public Sub Decode_EmptyString_ReturnsCorrectObject()
		Dim valueExpected = New JsonString(String.Empty)
		Dim valueDecoded = JsonParser.Decode("""""")

		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub


	<TestMethod()>
	Public Sub Decode_EscapableString_ReturnsCorrectObject()
		Dim valueExpected = New JsonString("Hello ""World""! Two revers solidus go here \\")
		Dim valueDecoded = JsonParser.Decode("""Hello \""World\""! Two revers solidus go here \\\\""")

		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub


	<TestMethod()>
	Public Sub Decode_EscapeCharacters_ReturnsCorrectObject()
		Dim escapeSeqs = {"\""", "\\", "\/", "\b", "\f", "\n", "\r", "\t", "\u1234"}

		For Each escapeSeq In escapeSeqs
			Dim valueExpected = New JsonString(escapeSeq)

			Dim escapedSeq = """" & escapeSeq.Replace("\", "\\").Replace("""", "\""") & """"
			Dim valueDecoded = JsonParser.Decode(escapedSeq)

			Assert.AreEqual(valueExpected, valueDecoded)
		Next
	End Sub


	<TestMethod()>
	Public Sub Decode_MultilineStringCrLf_ReturnsCorrectObject()
		Dim words = "Hello World Each Word Is On Separate Line"

		Dim jsonValue = """" & words.Replace(" ", "\r\n") & """"
		Dim valueExpected = New JsonString(words.Replace(" ", vbCrLf))

		Dim valueDecoded = JsonParser.Decode(jsonValue)
		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub


	<TestMethod()>
	Public Sub Decode_MultilineStringCr_ReturnsCorrectObject()
		Dim words = "Hello World Each Word Is On Separate Line"

		Dim jsonValue = """" & words.Replace(" ", "\r") & """"
		Dim valueExpected = New JsonString(words.Replace(" ", vbCr))

		Dim valueDecoded = JsonParser.Decode(jsonValue)
		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub


	<TestMethod()>
	Public Sub Decode_MultilineStringLf_ReturnsCorrectObject()
		Dim words = "Hello World Each Word Is On Separate Line"

		Dim jsonValue = """" & words.Replace(" ", "\n") & """"
		Dim valueExpected = New JsonString(words.Replace(" ", vbLf))

		Dim valueDecoded = JsonParser.Decode(jsonValue)
		Assert.AreEqual(valueExpected, valueDecoded)
	End Sub

#End Region

#Region "Decode Array Tests"

	<TestMethod()>
	Public Sub Decode_EmptyArray_ReturnsCorrectObject()
		Dim valueDecoded = JsonParser.Decode("[]")

		Assert.IsTrue(TypeOf (valueDecoded) Is JsonArray, "Expected type JsonArray.")

		Dim arrayDecoded = DirectCast(valueDecoded, JsonArray)
		Assert.IsTrue(arrayDecoded.Count = 0, "Expected empty array.")
	End Sub


	<TestMethod()>
	Public Sub Decode_SimpleArray_ReturnsCorrectObject()
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

	<TestMethod()>
	Public Sub Decode_EmptyObject_ReturnsCorrectObject()
		Dim valueDecoded = JsonParser.Decode("{}")

		Assert.IsTrue(TypeOf (valueDecoded) Is JsonObject, "Expected type JsonObject.")

		Dim objectDecoded = DirectCast(valueDecoded, JsonObject)
		Assert.IsTrue(objectDecoded.Count = 0, "Expected empty object.")
	End Sub


	<TestMethod()>
	Public Sub Decode_SimpleObject_ReturnsCorrectObject()
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

End Class

