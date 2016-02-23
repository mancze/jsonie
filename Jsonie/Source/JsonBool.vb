''' <summary>
''' Represents a JSON boolean.
''' </summary>
''' <remarks>Immutable.</remarks>
<DebuggerDisplay("JsonBool: Value = {Value}")>
Public Class JsonBool
	Inherits JsonValue

	''' <summary>
	''' JSON representation of the boolean True.
	''' </summary>
	Public Shared ReadOnly [True] As New JsonBool(True)


	''' <summary>
	''' JSON representation of the boolean False.
	''' </summary>
	Public Shared ReadOnly [False] As New JsonBool(False)


	''' <summary>
	''' Gets the actual boolean value.
	''' </summary>
	Public ReadOnly Property Value As Boolean
		Get
			Return _value
		End Get
	End Property
	Private ReadOnly _value As Boolean


	''' <summary>
	''' Creates new JSON boolean of the specified value.
	''' </summary>
	''' <param name="value">The value of the boolean.</param>
	Public Sub New(value As Boolean)
		Me._value = value
	End Sub

#Region "GetHashCode(), Equals()"

	''' <summary>
	''' Serves as a hash function for a particular type.
	''' </summary>
	''' <returns>A hash code for the current <see cref="T:System.Object"/>.</returns>
	Public Overrides Function GetHashCode() As Integer
		Return Me._value.GetHashCode()
	End Function


	''' <summary>
	''' Determines whether current object is equal to another object.
	''' </summary>
	''' <param name="obj">The object to compare with the current object.</param>
	''' <returns>True if the current object is equal to this, false otherwise.</returns>
	Public Overrides Function Equals(obj As Object) As Boolean
		If obj Is Nothing Then
			Return False

		ElseIf obj Is Me Then
			Return True

		ElseIf TypeOf obj Is JsonBool Then
			Dim other = DirectCast(obj, JsonBool)
			Return Me._value = other._value

		ElseIf TypeOf obj Is JsonDynamic Then
			Dim other = CType(obj, JsonDynamic)
			Return Me.Equals(other.Value)

		End If

		Return False
	End Function


	''' <summary>
	''' Determines whether current object is equal to another object.
	''' </summary>
	''' <param name="other">The object to compare with the current object.</param>
	''' <returns>True if the current object is equal to this, false otherwise.</returns>
	Public Overloads Function Equals(other As JsonBool) As Boolean
		If other Is Nothing Then
			Return False
		ElseIf other Is Me Then
			Return True
		End If

		Return Me._value = other._value
	End Function


	''' <summary>
	''' Determines whether current object is equal to another object.
	''' </summary>
	''' <param name="other">The object to compare with the current object.</param>
	''' <returns>True if the current object is equal to this, false otherwise.</returns>
	Public Overloads Function Equals(other As JsonDynamic) As Boolean
		Return Me.Equals(other.Value)
	End Function

#End Region

#Region "Operators"

	''' <summary>
	''' Test whether values of its operands are equal or both null.
	''' </summary>
	''' <param name="former">First value to compare.</param>
	''' <param name="latter">Second value to compare.</param>
	''' <returns>True if <paramref name="former" /> is equal to <paramref name="latter" />.</returns>
	Public Shared Operator =(former As JsonBool, latter As JsonBool) As Boolean
		If former Is Nothing AndAlso latter Is Nothing Then
			Return True
		ElseIf former IsNot Nothing Then
			Return former.Equals(latter)
		Else
			Return False
		End If
	End Operator


	''' <summary>
	''' Test whether values of its operands are inequal.
	''' </summary>
	''' <param name="former">First value to compare.</param>
	''' <param name="latter">Second value to compare.</param>
	''' <returns>True if <paramref name="former" /> is inequal to <paramref name="latter" />.</returns>
	''' <remarks>Two nulls are considered as equal.</remarks>
	Public Shared Operator <>(former As JsonBool, latter As JsonBool) As Boolean
		Return (Not former = latter)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from <see cref="JsonBool" /> to <see cref="Boolean" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="Boolean" /> casted from the specified value.</returns>
	Public Overloads Shared Widening Operator CType(value As JsonBool) As Boolean
		If value Is Nothing Then
			Return False
		End If

		Return value.Value
	End Operator


	''' <summary>
	''' Performs an implicit conversion from <see cref="Boolean" /> to <see cref="JsonBool" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonBool" /> casted from the specified value.</returns>
	Public Overloads Shared Widening Operator CType(value As Boolean) As JsonBool
		Return If(value, JsonBool.True, JsonBool.False)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from nullable <see cref="Boolean" /> to <see cref="JsonBool" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonBool" /> casted from the specified value.</returns>
	Public Overloads Shared Widening Operator CType(value As Boolean?) As JsonBool
		If Not value.HasValue Then
			Return Nothing
		End If

		Return If(value, JsonBool.True, JsonBool.False)
	End Operator

#End Region

End Class

