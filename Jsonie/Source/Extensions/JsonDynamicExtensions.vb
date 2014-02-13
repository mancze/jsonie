Imports System.Runtime.CompilerServices


Namespace Extensions

	''' <summary>
	''' Contains extensions to JsonDynamic structure.
	''' </summary>
	Public Module JsonDynamicExtensions

		''' <summary>
		''' Wraps given json value to dynamic type.
		''' </summary>
		''' <param name="value">Value to be wrapped as dynamic.</param>
		''' <returns>Dynamic wrappper around given value.</returns>
		<Extension()>
		Public Function ToStatic(value As JsonDynamic) As JsonValue
			Return value.Value
		End Function

	End Module

End Namespace
