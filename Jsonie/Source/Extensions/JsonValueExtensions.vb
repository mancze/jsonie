Imports System.Runtime.CompilerServices


Namespace Extensions

	''' <summary>
	''' Contains extensions to JsonValue object. Extensions makes Json library to easier work with.
	''' </summary>
	Public Module JsonValueExtensions

		''' <summary>
		''' Casts (CType) value to JsonObject.
		''' </summary>
		''' <param name="value">Value which will be casted.</param>
		''' <returns>Casted value.</returns>
		''' <exception cref="InvalidCastException">If value is not type of JsonObject.</exception>
		<Extension()>
		Public Function AsObject(value As JsonValue) As JsonObject
			Return CType(value, JsonObject)
		End Function


		''' <summary>
		''' Casts (CType) value to JsonObject.
		''' </summary>
		''' <param name="value">Value which will be casted.</param>
		''' <returns>Casted value.</returns>
		''' <exception cref="InvalidCastException">If value is not type of JsonArray.</exception>
		<Extension()>
		Public Function AsArray(value As JsonValue) As JsonArray
			Return CType(value, JsonArray)
		End Function


		''' <summary>
		''' Casts (CType) value to JsonString.
		''' </summary>
		''' <param name="value">Value which will be casted.</param>
		''' <returns>Casted value.</returns>
		''' <exception cref="InvalidCastException">If value is not type of JsonString.</exception>
		<Extension()>
		Public Function AsString(value As JsonValue) As JsonString
			Return CType(value, JsonString)
		End Function


		''' <summary>
		''' Casts (CType) value to JsonNumber.
		''' </summary>
		''' <param name="value">Value which will be casted.</param>
		''' <returns>Casted value.</returns>
		''' <exception cref="InvalidCastException">If value is not type of JsonNumber.</exception>
		<Extension()>
		Public Function AsNumber(value As JsonValue) As JsonNumber
			Return CType(value, JsonNumber)
		End Function


		''' <summary>
		''' Casts (CType) value to JsonBool.
		''' </summary>
		''' <param name="value">Value which will be casted.</param>
		''' <returns>Casted value.</returns>
		''' <exception cref="InvalidCastException">If value is not type of JsonBool.</exception>
		<Extension()>
		Public Function AsBoolean(value As JsonValue) As JsonBool
			Return CType(value, JsonBool)
		End Function


		''' <summary>
		''' Wraps given json value to dynamic type.
		''' </summary>
		''' <param name="value">Value to be wrapped as dynamic.</param>
		''' <returns>Dynamic wrappper around given value.</returns>
		<Extension()>
		Public Function ToDynamic(value As JsonValue) As JsonDynamic
			Return New JsonDynamic(value)
		End Function

	End Module

End Namespace
