Imports System.Runtime.CompilerServices


''' <summary>
''' Contains extensions to JsonValue object. Extensions makes Json library to easier work with.
''' </summary>
Public Module JsonValueExtensions

	''' <summary>
	''' Casts (CType) value to JsonObject.
	''' </summary>
	''' <param name="value">Value which will be casted.</param>
	''' <returns>Casted value.</returns>
	<Extension()>
	Public Function AsObject(value As JsonValue) As JsonObject
		Return CType(value, JsonObject)
	End Function


	''' <summary>
	''' Casts (CType) value to JsonObject.
	''' </summary>
	''' <param name="value">Value which will be casted.</param>
	''' <returns>Casted value.</returns>
	<Extension()>
	Public Function AsArray(value As JsonValue) As JsonArray
		Return CType(value, JsonArray)
	End Function


	''' <summary>
	''' Casts (CType) value to JsonString.
	''' </summary>
	''' <param name="value">Value which will be casted.</param>
	''' <returns>Casted value.</returns>
	<Extension()>
	Public Function AsString(value As JsonValue) As JsonString
		Return CType(value, JsonString)
	End Function


	''' <summary>
	''' Casts (CType) value to JsonNumber.
	''' </summary>
	''' <param name="value">Value which will be casted.</param>
	''' <returns>Casted value.</returns>
	<Extension()>
	Public Function AsNumber(value As JsonValue) As JsonNumber
		Return CType(value, JsonNumber)
	End Function


	''' <summary>
	''' Casts (CType) value to JsonBool.
	''' </summary>
	''' <param name="value">Value which will be casted.</param>
	''' <returns>Casted value.</returns>
	<Extension()>
	Public Function AsBoolean(value As JsonValue) As JsonBool
		Return CType(value, JsonBool)
	End Function

End Module
