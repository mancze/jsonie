''' <summary>
''' Represents JSON string. Internal value is never null.
''' </summary>
''' <remarks>Immutable</remarks>
<DebuggerDisplay("JsonString: Value = {Value}")>
Public Class JsonString
	Inherits JsonValue


	''' <summary>
	''' Gets the Char object at a specified position in the current JsonString object.
	''' </summary>
	''' <param name="index">A position in the current string.</param>
	''' <value>The char at position index.</value>
	''' <remarks>The index parameter is zero-based.</remarks>
	''' <exception cref="IndexOutOfRangeException">
	''' index is greater than or equal to the length of this object or less than zero.
	''' </exception>
	Default Public ReadOnly Property Chars(index As Integer) As Char
		Get
			Return _value.Chars(index)
		End Get
	End Property


	''' <summary>
	''' Gets the number of characters in the current JsonString object.
	''' </summary>
	''' <remarks>
	''' The Length property returns the number of Char objects in this instance, not the number of Unicode characters.
	''' </remarks>
	Public ReadOnly Property Length As Integer
		Get
			Return _value.Length
		End Get
	End Property


	''' <summary>
	''' Actual string value encoded by this JSON object.
	''' </summary>
	Public ReadOnly Property Value As String
		Get
			Return _value
		End Get
	End Property
	Private ReadOnly _value As String


	''' <summary>
	''' Creates new string object.
	''' </summary>
	''' <exception cref="ArgumentNullException">If provided value is null.</exception>
	Public Sub New(value As String)
		If value Is Nothing Then
			Throw New ArgumentNullException("value")
		End If

		Me._value = value
	End Sub

#Region "GetHashCode(), Equals()"

	Public Overrides Function GetHashCode() As Integer
		If Me._value Is Nothing Then
			Return 0
		End If

		Return Me._value.GetHashCode()
	End Function


	Public Overrides Function Equals(obj As Object) As Boolean
		If obj Is Nothing Then
			Return False

		ElseIf obj Is Me Then
			Return True

		ElseIf TypeOf obj Is JsonString Then
			Dim other = DirectCast(obj, JsonString)
			Return Me._value = other._value

		ElseIf TypeOf obj Is JsonDynamic Then
			Dim other = CType(obj, JsonDynamic)
			Return Me.Equals(other.Value)

		End If

		Return False
	End Function


	Public Overloads Function Equals(other As JsonString) As Boolean
		If other Is Nothing Then
			Return False

		ElseIf other Is Me Then
			Return True

		End If

		Return Me._value = other._value
	End Function


	Public Overloads Function Equals(other As JsonDynamic) As Boolean
		Return Me.Equals(other.Value)
	End Function

#End Region

#Region "Operators"

	Public Shared Operator =(former As JsonString, latter As JsonString) As Boolean
		If former Is Nothing AndAlso latter Is Nothing Then
			Return True
		ElseIf former IsNot Nothing Then
			Return former.Equals(latter)
		Else
			Return False
		End If
	End Operator


	Public Shared Operator <>(former As JsonString, latter As JsonString) As Boolean
		Return (Not former = latter)
	End Operator


	Public Overloads Shared Widening Operator CType(text As JsonString) As String
		If text Is Nothing Then
			Return Nothing
		End If

		Return text.Value
	End Operator


	Public Overloads Shared Widening Operator CType(text As String) As JsonString
		If text Is Nothing Then
			Return Nothing
		End If

		Return New JsonString(text)
	End Operator

#End Region

#Region "Shared Methods"

	''' <summary>
	''' Indicates whether a specified JsonString is Nothing or empty.
	''' </summary>
	''' <param name="value">The object to test.</param>
	''' <returns>True if the value parameter is Nothing or value.Value is String.Empty.</returns>
	Public Shared Function IsNullOrEmpty(value As JsonString) As Boolean
		If value Is Nothing Then
			Return True
		Else
			Return String.IsNullOrEmpty(value.Value)
		End If
	End Function


	''' <summary>
	''' Indicates whether a specified JsonString is Nothing, empty, or consists only of white-space characters.
	''' </summary>
	''' <param name="value">The object to test.</param>
	''' <returns>
	''' True if the value parameter is Nothing or value.Value is String.Empty, or if value.Value consists exclusively 
	''' of white-space characters.
	''' </returns>
	Public Shared Function IsNullOrWhitespace(value As JsonString) As Boolean
		If value Is Nothing Then
			Return True
		Else
			Return String.IsNullOrWhiteSpace(value.Value)
		End If
	End Function

#End Region

End Class
