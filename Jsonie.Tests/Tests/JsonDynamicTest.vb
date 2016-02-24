Imports NUnit.Framework
Imports Dextronet.Jsonie


''' <summary>
''' Tests for <see cref="JsonDynamic" />.
''' </summary>
<TestFixture()>
Public Class JsonDynamicTest

	<Test()>
	Public Sub Null_ThrowsExceptionOnUsage()
		Dim value As JsonDynamic = Nothing

		Assert.Throws(Of InvalidCastException)(Function() value.Count)
		Assert.Throws(Of InvalidCastException)(Function() value.Item(0))
		Assert.Throws(Of InvalidCastException)(Function() value.Item("foobar"))
		Assert.Throws(Of InvalidCastException)(Function() value.AddArray("heyo"))
		Assert.Throws(Of InvalidCastException)(Sub() value.InsertRange(0, {}))
	End Sub

End Class
