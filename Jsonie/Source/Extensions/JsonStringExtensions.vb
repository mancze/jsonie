Imports System.Runtime.CompilerServices


Namespace Extensions

	''' <summary>
	''' Contains extensions to JsonString object. Extensions makes Json library to easier work with.
	''' </summary>
	Public Module JsonStringExtensions

		''' <summary>
		''' Indicates whether a specified JsonString is Nothing or empty.
		''' </summary>
		''' <param name="value">The object to test.</param>
		''' <returns>True if the value parameter is Nothing or value.Value is String.Empty.</returns>
		<Extension()>
		Public Function IsNullOrEmpty(value As JsonString) As Boolean
			Return JsonString.IsNullOrEmpty(value)
		End Function


		''' <summary>
		''' Indicates whether a specified JsonString is Nothing, empty, or consists only of white-space characters.
		''' </summary>
		''' <param name="value">The object to test.</param>
		''' <returns>
		''' True if the value parameter is Nothing or value.Value is String.Empty, or if value.Value consists exclusively 
		''' of white-space characters.
		''' </returns>
		<Extension()>
		Public Function IsNullOrWhitespace(value As JsonString) As Boolean
			Return JsonString.IsNullOrWhitespace(value)
		End Function

	End Module

End Namespace
