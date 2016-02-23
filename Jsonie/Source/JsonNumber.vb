Imports System.Globalization


''' <summary>
''' Represents a JSON number.
''' </summary>
''' <remarks>Immutable.</remarks>
<DebuggerDisplay("JsonNumber: Value = {DecimalValue}")>
Public Class JsonNumber
	Inherits JsonValue


	''' <summary>
	''' Gets the decimal value.
	''' </summary>
	Public ReadOnly Property Value As Decimal
		Get
			Return _value
		End Get
	End Property
	Private ReadOnly _value As Decimal


	''' <summary>
	''' Gets the integer value.
	''' </summary>
	''' <exception cref="OverflowException">When accessing value which is out of Integer bounds.</exception>
	Public ReadOnly Property IntegerValue As Integer
		Get
			Return Convert.ToInt32(_value, CultureInfo.InvariantCulture)
		End Get
	End Property


	''' <summary>
	''' Gets the long value.
	''' </summary>
	''' <exception cref="OverflowException">When accessing value which is out of Long bounds.</exception>
	Public ReadOnly Property LongValue As Long
		Get
			Return Convert.ToInt64(_value, CultureInfo.InvariantCulture)
		End Get
	End Property


	''' <summary>
	''' Gets the single value.
	''' </summary>
	''' <exception cref="OverflowException">When accessing value which is out of Single bounds.</exception>
	Public ReadOnly Property SingleValue As Single
		Get
			Return Convert.ToSingle(_value, CultureInfo.InvariantCulture)
		End Get
	End Property


	''' <summary>
	''' Gets the double value.
	''' </summary>
	''' <exception cref="OverflowException">When accessing value which is out of Double bounds.</exception>
	Public ReadOnly Property DoubleValue As Double
		Get
			Return Convert.ToDouble(_value, CultureInfo.InvariantCulture)
		End Get
	End Property


	''' <summary>
	''' Creates new JSON number of specified value.
	''' </summary>
	''' <param name="value">The value of the number.</param>
	Public Sub New(value As Decimal)
		Me._value = value
	End Sub


	''' <summary>
	''' Creates new JSON number of specified value.
	''' </summary>
	''' <param name="value">The value of the number.</param>
	Public Sub New(value As Integer)
		Me._value = value
	End Sub


	''' <summary>
	''' Creates new JSON number of specified value.
	''' </summary>
	''' <param name="value">The value of the number.</param>
	Public Sub New(value As Long)
		Me._value = value
	End Sub


	''' <summary>
	''' Creates new JSON number of specified value.
	''' </summary>
	''' <param name="value">The value of the number.</param>
	Public Sub New(value As Double)
		Me._value = Convert.ToDecimal(value, CultureInfo.InvariantCulture)
	End Sub


	''' <summary>
	''' Creates new JSON number of specified value.
	''' </summary>
	''' <param name="value">The value of the number.</param>
	Public Sub New(value As Single)
		Me._value = Convert.ToDecimal(value, CultureInfo.InvariantCulture)
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

		ElseIf TypeOf obj Is JsonNumber Then
			Dim other = CType(obj, JsonNumber)
			Return Me._value.Equals(other._value)

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
	Public Overloads Function Equals(other As JsonNumber) As Boolean
		If other Is Nothing Then
			Return False

		ElseIf other Is Me Then
			Return True

		End If

		Return Me._value.Equals(other._value)
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
	Public Shared Operator =(former As JsonNumber, latter As JsonNumber) As Boolean
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
	Public Shared Operator <>(former As JsonNumber, latter As JsonNumber) As Boolean
		Return (Not former = latter)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from <see cref="JsonNumber" /> to <see cref="Decimal" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="Decimal" /> casted from the specified value.</returns>
	Public Overloads Shared Widening Operator CType(value As JsonNumber) As Decimal?
		If value Is Nothing Then
			Return Nothing
		End If

		Return value.Value
	End Operator


	''' <summary>
	''' Performs an implicit conversion from <see cref="Integer" /> to <see cref="JsonNumber" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonNumber" /> casted from the specified value.</returns>
	Public Overloads Shared Widening Operator CType(value As Integer) As JsonNumber
		Return New JsonNumber(value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from nullable <see cref="Integer" /> to <see cref="JsonNumber" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonNumber" /> casted from the specified value.</returns>
	Public Overloads Shared Widening Operator CType(value As Integer?) As JsonNumber
		If Not value.HasValue Then
			Return Nothing
		End If

		Return New JsonNumber(value.Value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from <see cref="Long" /> to <see cref="JsonNumber" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonNumber" /> casted from the specified value.</returns>
	Public Overloads Shared Widening Operator CType(value As Long) As JsonNumber
		Return New JsonNumber(value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from nullable <see cref="Long" /> to <see cref="JsonNumber" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonNumber" /> casted from the specified value.</returns>
	Public Overloads Shared Widening Operator CType(value As Long?) As JsonNumber
		If Not value.HasValue Then
			Return Nothing
		End If

		Return New JsonNumber(value.Value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from <see cref="Single" /> to <see cref="JsonNumber" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonNumber" /> casted from the specified value.</returns>
	Public Overloads Shared Widening Operator CType(value As Single) As JsonNumber
		Return New JsonNumber(value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from nullable <see cref="Single" /> to <see cref="JsonNumber" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonNumber" /> casted from the specified value.</returns>
	Public Overloads Shared Widening Operator CType(value As Single?) As JsonNumber
		If Not value.HasValue Then
			Return Nothing
		End If

		Return New JsonNumber(value.Value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from <see cref="Double" /> to <see cref="JsonNumber" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonNumber" /> casted from the specified value.</returns>
	Public Overloads Shared Widening Operator CType(value As Double) As JsonNumber
		Return New JsonNumber(value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from nullable <see cref="Double" /> to <see cref="JsonNumber" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonNumber" /> casted from the specified value.</returns>
	Public Overloads Shared Widening Operator CType(value As Double?) As JsonNumber
		If Not value.HasValue Then
			Return Nothing
		End If

		Return New JsonNumber(value.Value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from <see cref="Decimal" /> to <see cref="JsonNumber" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonNumber" /> casted from the specified value.</returns>
	Public Overloads Shared Widening Operator CType(value As Decimal) As JsonNumber
		Return New JsonNumber(value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from nullable <see cref="Decimal" /> to <see cref="JsonNumber" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonNumber" /> casted from the specified value.</returns>
	Public Overloads Shared Widening Operator CType(value As Decimal?) As JsonNumber
		If Not value.HasValue Then
			Return Nothing
		End If

		Return New JsonNumber(value.Value)
	End Operator

#End Region

End Class
