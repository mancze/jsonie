Imports System.Globalization


''' <summary>
''' Base class for all JSON types except null which is represented also as null in .NET.
''' </summary>
Public MustInherit Class JsonValue

	''' <summary>
	''' Tests if this is JSON object.
	''' </summary>
	Public ReadOnly Property IsObject As Boolean
		Get
			Return TypeOf Me Is JsonObject
		End Get
	End Property


	''' <summary>
	''' Tests if this is JSON array.
	''' </summary>
	Public ReadOnly Property IsArray As Boolean
		Get
			Return TypeOf Me Is JsonArray
		End Get
	End Property


	''' <summary>
	''' Tests if this is not JSON object nor array.
	''' </summary>
	Public ReadOnly Property IsScalar As Boolean
		Get
			Return Not Me.IsObject AndAlso Not Me.IsArray
		End Get
	End Property


	''' <summary>
	''' Tests if this is JSON string.
	''' </summary>
	Public ReadOnly Property IsString As Boolean
		Get
			Return TypeOf Me Is JsonString
		End Get
	End Property


	''' <summary>
	''' Test if this is JSON number.
	''' </summary>
	Public ReadOnly Property IsNumber As Boolean
		Get
			Return TypeOf Me Is JsonNumber
		End Get
	End Property


	''' <summary>
	''' Tests if this is boolean.
	''' </summary>
	Public ReadOnly Property IsBoolean As Boolean
		Get
			Return TypeOf Me Is JsonBool
		End Get
	End Property

#Region "Operators"

	Public Shared Widening Operator CType(text As String) As JsonValue
		If text Is Nothing Then
			Return Nothing
		End If

		Return New JsonString(text)
	End Operator


	Public Shared Widening Operator CType(value As Boolean) As JsonValue
		Return If(value, JsonBool.True, JsonBool.False)
	End Operator


	Public Shared Widening Operator CType(value As Boolean?) As JsonValue
		If Not value.HasValue Then
			Return Nothing
		End If

		Return If(value, JsonBool.True, JsonBool.False)
	End Operator


	Public Shared Widening Operator CType(number As Integer) As JsonValue
		Return New JsonNumber(number)
	End Operator


	Public Shared Widening Operator CType(number As Integer?) As JsonValue
		If Not number.HasValue Then
			Return Nothing
		End If

		Return New JsonNumber(number.Value)
	End Operator


	Public Shared Widening Operator CType(number As Long) As JsonValue
		Return New JsonNumber(number)
	End Operator


	Public Shared Widening Operator CType(number As Long?) As JsonValue
		If Not number.HasValue Then
			Return Nothing
		End If

		Return New JsonNumber(number.Value)
	End Operator


	Public Shared Widening Operator CType(number As Single) As JsonValue
		Return New JsonNumber(number)
	End Operator


	Public Shared Widening Operator CType(number As Single?) As JsonValue
		If Not number.HasValue Then
			Return Nothing
		End If

		Return New JsonNumber(number.Value)
	End Operator


	Public Shared Widening Operator CType(number As Double) As JsonValue
		Return New JsonNumber(number)
	End Operator


	Public Shared Widening Operator CType(number As Double?) As JsonValue
		If Not number.HasValue Then
			Return Nothing
		End If

		Return New JsonNumber(number.Value)
	End Operator


	Public Shared Widening Operator CType(number As Decimal) As JsonValue
		Return New JsonNumber(number)
	End Operator


	Public Shared Widening Operator CType(number As Decimal?) As JsonValue
		If Not number.HasValue Then
			Return Nothing
		End If

		Return New JsonNumber(number.Value)
	End Operator

#End Region

End Class
