Imports System.Collections
Imports System.Collections.Generic
Imports NUnit.Framework
Imports Dextronet.Jsonie


'''<summary>
'''This is a test class for JsonObjectTest and is intended
'''to contain all JsonObjectTest Unit Tests
'''</summary>
<TestFixture()>
Public Class JsonObjectTest

	<Test()>
	Public Sub GetOrAdd_DoNotOverwriteNull()
		Dim value = New JsonObject()
		value("null") = Nothing

		Assert.AreEqual(Nothing, value.GetOrAdd(Of JsonString)("null", "Hello World!"))
		Assert.AreEqual(Nothing, value.GetOrAddObject("null"))
		Assert.AreEqual(Nothing, value.GetOrAddArray("null"))
	End Sub

End Class
