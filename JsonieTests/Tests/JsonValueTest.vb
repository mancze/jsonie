Imports NUnit.Framework
Imports Dextronet.Jsonie
Imports Dextronet.Jsonie.Extensions


'''<summary>
'''This is a test class for JsonValue and is intended
'''to contain all JsonValue Unit Tests
'''</summary>
<TestFixture()>
Public Class JsonValueTest

#Region "Operators"

	<Test()>
	Public Sub Cast_NullToJsonString_ReturnsNull()
		Dim value As JsonValue = Nothing
		Dim stringValue = value.AsString()

		Assert.AreEqual(Nothing, stringValue)
	End Sub

#End Region

End Class
