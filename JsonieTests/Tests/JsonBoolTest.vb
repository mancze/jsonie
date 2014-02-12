Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Dextronet.Jsonie


'''<summary>
'''This is a test class for JsonBool and is intended
'''to contain all JsonBool Unit Tests
'''</summary>
<TestClass()>
Public Class JsonBoolTest

#Region "Operators"

	<TestMethod()>
	Public Sub Cast_NullToBool_ReturnsNull()
		Dim value As JsonBool = Nothing
		Dim castedValue As Boolean = value

		Assert.AreEqual(False, castedValue)
	End Sub

#End Region

End Class
