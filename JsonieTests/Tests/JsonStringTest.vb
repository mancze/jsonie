Imports NUnit.Framework
Imports Dextronet.Jsonie


'''<summary>
'''This is a test class for JsonStringTest and is intended
'''to contain all JsonStringTest Unit Tests
'''</summary>
<TestFixture()>
Public Class JsonStringTest

#Region "Operators"

	<Test()>
	Public Sub Cast_NullToString_ReturnsNull()
		Dim value As JsonString = Nothing
		Dim castedValue As String = value

		Assert.AreEqual(Nothing, castedValue)
	End Sub

#End Region

End Class
