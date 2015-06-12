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


	''' <summary>
	''' Converts object to matching JsonValue. 
	''' 
	''' Supports conversion of numbers, strings and dates including nullable versions.
	''' </summary>
	''' <exception cref="InvalidCastException">Thrown when object being converted is not supported.</exception>
	''' <param name="obj">Object to convert.</param>
	''' <returns>Object wrapped as JSON value.</returns>
	Public Shared Function [From](obj As Object) As JsonValue
		If obj Is Nothing Then
			Return Nothing

		ElseIf TypeOf obj Is JsonValue Then
			Return CType(obj, JsonValue)

		ElseIf TypeOf obj Is Boolean OrElse TypeOf obj Is Boolean? Then
			Return If(CType(obj, Boolean), JsonBool.True, JsonBool.False)

		ElseIf TypeOf obj Is Integer OrElse TypeOf obj Is Integer? Then
			Return New JsonNumber(CType(obj, Integer))

		ElseIf TypeOf obj Is Decimal OrElse TypeOf obj Is Decimal? Then
			Return New JsonNumber(CType(obj, Decimal))

		ElseIf TypeOf obj Is Long OrElse TypeOf obj Is Long? Then
			Return New JsonNumber(CType(obj, Long))

		ElseIf TypeOf obj Is Single OrElse TypeOf obj Is Single? Then
			Return New JsonNumber(CType(obj, Single))

		ElseIf TypeOf obj Is Double OrElse TypeOf obj Is Double? Then
			Return New JsonNumber(CType(obj, Double))

		ElseIf TypeOf obj Is String Then
			Return New JsonString(CType(obj, String))

		Else
			Throw New InvalidCastException(String.Format("Cannot convert object of type {0} to JsonValue.", obj.GetType()))

		End If
	End Function


	''' <summary>
	''' Gets to JSON encoded string representing this object.
	''' </summary>
	''' <returns>JSON string</returns>
	Public Function ToJson() As String
		Return JsonParser.Encode(Me, JsonEncoderOptions.Default)
	End Function

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
