Imports System.Runtime.CompilerServices


''' <summary>
''' Contains extensions to JsonDynamic structure.
''' </summary>
Public Module JsonDynamicExtensions

	''' <summary>
	''' Returns the value of the dynamic wrapper.
	''' </summary>
	''' <param name="value">Dynamic wrapper whose value to get.</param>
	''' <returns>Value of the dynamic wrapper.</returns>
	<Extension()>
	Public Function ToStatic(value As JsonDynamic) As JsonValue
		Return value.Value
	End Function

End Module
