''' <summary>
''' Represents JSON string. Internal value is never null.
''' </summary>
''' <remarks>Immutable</remarks>
Public Class JsonString
	Inherits JsonValue


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

#Region "ToString(), GetHashCode(), Equals()"

	Public Overrides Function ToString() As String
		Return JsonParser.Encode(Me)
	End Function


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

#End Region

End Class
