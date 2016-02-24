Imports NUnit.Framework
Imports Dextronet.Jsonie


'''<summary>
'''This is a test class for JsonBool and is intended
'''to contain all JsonBool Unit Tests
'''</summary>
<TestFixture()>
Public Class JsonBoolTest

#Region "Operators"

	<Test()>
	Public Sub Cast_NullToBool_ReturnsNull()
		Dim value As JsonBool = Nothing
		Dim castedValue As Boolean? = value

		Assert.AreEqual(Nothing, castedValue)
	End Sub


	<Test()>
	Public Sub Cast_True()
		Dim value = True
		Dim json As JsonBool = value
		Dim castedValue = json.Value

		Assert.AreEqual(value, castedValue)
	End Sub


	<Test()>
	Public Sub Cast_False()
		Dim value = False
		Dim json As JsonBool = value
		Dim castedValue = json.Value

		Assert.AreEqual(value, castedValue)
	End Sub


	<Test()>
	Public Sub Equality()
		Dim theTrue = JsonBool.True
		Dim theFalse = JsonBool.False
		Dim tru = New JsonBool(True)
		Dim fls = New JsonBool(False)

		Assert.IsTrue(theTrue.Equals(tru))
		Assert.IsTrue(theFalse.Equals(fls))

		Assert.IsFalse(theTrue.Equals(fls))
		Assert.IsFalse(theFalse.Equals(tru))
	End Sub

#End Region

End Class
