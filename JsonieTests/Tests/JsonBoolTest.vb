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

#End Region

End Class
