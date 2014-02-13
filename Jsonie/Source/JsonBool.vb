''' <summary>
''' Represents JSON boolean values.
''' </summary>
''' <remarks>Immutable</remarks>
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
	''' Gets the actual boolean value
	''' </summary>
	Public ReadOnly Property Value As Boolean
		Get
			Return _value
		End Get
	End Property
	Private ReadOnly _value As Boolean


	''' <summary>
	''' Creates new JSON representation of the boolean.
	''' </summary>
	Friend Sub New(value As Boolean)
		Me._value = value
	End Sub

#Region "ToString(), GetHashCode(), Equals()"

	Public Overrides Function ToString() As String
		Return JsonParser.Encode(Me)
	End Function


	Public Overrides Function GetHashCode() As Integer
		Return Me._value.GetHashCode()
	End Function


	Public Overrides Function Equals(obj As Object) As Boolean
		If obj Is Nothing Then
			Return False

		ElseIf obj Is Me Then
			Return True

		ElseIf TypeOf obj Is JsonBool Then
			Dim other = DirectCast(obj, JsonBool)
			Return Me._value = other._value

		End If

		Return False
	End Function


	Public Overloads Function Equals(other As JsonBool) As Boolean
		If other Is Nothing Then
			Return False
		ElseIf other Is Me Then
			Return True
		End If

		Return Me._value = other._value
	End Function

#End Region

#Region "Operators"

	Public Shared Operator =(former As JsonBool, latter As JsonBool) As Boolean
		If former Is Nothing AndAlso latter Is Nothing Then
			Return True
		ElseIf former IsNot Nothing Then
			Return former.Equals(latter)
		Else
			Return False
		End If
	End Operator


	Public Shared Operator <>(former As JsonBool, latter As JsonBool) As Boolean
		Return Not former = latter
	End Operator


	Public Overloads Shared Widening Operator CType(value As JsonBool) As Boolean
		If value Is Nothing Then
			Return False
		End If

		Return value.Value
	End Operator


	Public Overloads Shared Widening Operator CType(value As Boolean) As JsonBool
		If value Then
			Return JsonBool.True
		Else
			Return JsonBool.False
		End If
	End Operator

#End Region

End Class

