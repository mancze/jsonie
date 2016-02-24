Imports System
Imports NUnit.Framework
Imports Dextronet.Jsonie


'''<summary>
'''This is a test class for JsonNumberTest and is intended
'''to contain all JsonNumberTest Unit Tests
'''</summary>
<TestFixture()>
Public Class JsonNumberTest

	<Test()>
	Public Sub Cast_NullToBool_ReturnsNull()
		Dim value As JsonNumber = Nothing
		Dim castedValue As Decimal? = value

		Assert.AreEqual(Nothing, castedValue)
	End Sub

End Class
