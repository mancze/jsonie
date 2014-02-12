Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Dextronet.Jsonie


'''<summary>
'''This is a test class for JsonStringTest and is intended
'''to contain all JsonStringTest Unit Tests
'''</summary>
<TestClass()>
Public Class JsonStringTest

#Region "Operators"

	<TestMethod()>
	Public Sub Cast_NullToString_ReturnsNull()
		Dim value As JsonString = Nothing
		Dim castedValue As String = value

		Assert.AreEqual(Nothing, castedValue)
	End Sub

#End Region

End Class
