Imports System.Globalization

''' <summary>
''' Represents JSON numeric value.
''' </summary>
''' <remarks>Immutable</remarks>
Public Class JsonNumber
	Inherits JsonValue


	''' <summary>
	''' Gets or sets decimal value.
	''' </summary>
	Public ReadOnly Property DecimalValue As Decimal
		Get
			Return _decimalValue
		End Get
	End Property
	Private ReadOnly _decimalValue As Decimal


	''' <summary>
	''' Gets or sets integer value.
	''' </summary>
	''' <exception cref="OverflowException">When accessing value which is out of Integer bounds.</exception>
	Public ReadOnly Property IntegerValue As Integer
		Get
			Return Convert.ToInt32(_decimalValue, CultureInfo.InvariantCulture)
		End Get
	End Property


	''' <summary>
	''' Gets or sets integer value.
	''' </summary>
	''' <exception cref="OverflowException">When accessing value which is out of Long bounds.</exception>
	Public ReadOnly Property LongValue As Long
		Get
			Return Convert.ToInt64(_decimalValue, CultureInfo.InvariantCulture)
		End Get
	End Property


	''' <summary>
	''' Gets or sets integer value.
	''' </summary>
	''' <exception cref="OverflowException">When accessing value which is out of Single bounds.</exception>
	Public ReadOnly Property SingleValue As Single
		Get
			Return Convert.ToSingle(_decimalValue, CultureInfo.InvariantCulture)
		End Get
	End Property


	''' <summary>
	''' Gets or sets integer value.
	''' </summary>
	''' <exception cref="OverflowException">When accessing value which is out of Double bounds.</exception>
	Public ReadOnly Property DoubleValue As Double
		Get
			Return Convert.ToDouble(_decimalValue, CultureInfo.InvariantCulture)
		End Get
	End Property


	Public Sub New(value As Decimal)
		Me._decimalValue = value
	End Sub


	Public Sub New(value As Double)
		Me._decimalValue = Convert.ToDecimal(value, CultureInfo.InvariantCulture)
	End Sub

#Region "GetHashCode(), Equals()"

	Public Overrides Function GetHashCode() As Integer
		Return Me._decimalValue.GetHashCode()
	End Function


	Public Overrides Function Equals(obj As Object) As Boolean
		If obj Is Nothing Then
			Return False

		ElseIf obj Is Me Then
			Return True

		ElseIf TypeOf obj Is JsonNumber Then
			Dim other = CType(obj, JsonNumber)
			Return Me._decimalValue.Equals(other._decimalValue)

		ElseIf TypeOf obj Is JsonDynamic Then
			Dim other = CType(obj, JsonDynamic)
			Return Me.Equals(other.Value)

		End If

		Return False
	End Function


	Public Overloads Function Equals(other As JsonNumber) As Boolean
		If other Is Nothing Then
			Return False

		ElseIf other Is Me Then
			Return True

		End If

		Return Me._decimalValue.Equals(other._decimalValue)
	End Function


	Public Overloads Function Equals(other As JsonDynamic) As Boolean
		Return Me.Equals(other.Value)
	End Function

#End Region

#Region "Operators"

	Public Shared Operator =(former As JsonNumber, latter As JsonNumber) As Boolean
		If former Is Nothing AndAlso latter Is Nothing Then
			Return True
		ElseIf former IsNot Nothing Then
			Return former.Equals(latter)
		Else
			Return False
		End If
	End Operator


	Public Shared Operator <>(former As JsonNumber, latter As JsonNumber) As Boolean
		Return (Not former = latter)
	End Operator


	Public Overloads Shared Widening Operator CType(value As JsonNumber) As Decimal
		If value Is Nothing Then
			Return Nothing
		End If

		Return value.DecimalValue
	End Operator


	Public Overloads Shared Widening Operator CType(number As Integer) As JsonNumber
		Return New JsonNumber(number)
	End Operator


	Public Overloads Shared Widening Operator CType(number As Integer?) As JsonNumber
		If Not number.HasValue Then
			Return Nothing
		End If

		Return New JsonNumber(number.Value)
	End Operator


	Public Overloads Shared Widening Operator CType(number As Long) As JsonNumber
		Return New JsonNumber(number)
	End Operator


	Public Overloads Shared Widening Operator CType(number As Long?) As JsonNumber
		If Not number.HasValue Then
			Return Nothing
		End If

		Return New JsonNumber(number.Value)
	End Operator


	Public Overloads Shared Widening Operator CType(number As Single) As JsonNumber
		Return New JsonNumber(number)
	End Operator


	Public Overloads Shared Widening Operator CType(number As Single?) As JsonNumber
		If Not number.HasValue Then
			Return Nothing
		End If

		Return New JsonNumber(number.Value)
	End Operator


	Public Overloads Shared Widening Operator CType(number As Double) As JsonNumber
		Return New JsonNumber(number)
	End Operator


	Public Overloads Shared Widening Operator CType(number As Double?) As JsonNumber
		If Not number.HasValue Then
			Return Nothing
		End If

		Return New JsonNumber(number.Value)
	End Operator


	Public Overloads Shared Widening Operator CType(number As Decimal) As JsonNumber
		Return New JsonNumber(number)
	End Operator


	Public Overloads Shared Widening Operator CType(number As Decimal?) As JsonNumber
		If Not number.HasValue Then
			Return Nothing
		End If

		Return New JsonNumber(number.Value)
	End Operator

#End Region

End Class
