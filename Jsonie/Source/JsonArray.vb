''' <summary>
''' Represents Json array.
''' </summary>
Public Class JsonArray
	Inherits JsonValue
	Implements IList(Of JsonValue)


	''' <summary>
	''' Gets the number of elements contained in array.
	''' </summary>
	Public ReadOnly Property Count As Integer Implements ICollection(Of JsonValue).Count
		Get
			Return Me.data.Count
		End Get
	End Property


	''' <summary>
	''' Gets or sets the element at the specified index.
	''' </summary>
	Default Public Property Item(index As Integer) As JsonValue Implements IList(Of JsonValue).Item
		Get
			Return Me.data(index)
		End Get
		Set(value As JsonValue)
			Me.data(index) = value
		End Set
	End Property


	''' <summary>
	''' Casted data to ICollection.
	''' </summary>
	Private ReadOnly Property DataAsICollection As ICollection(Of JsonValue)
		Get
			Return Me.data
		End Get
	End Property


	''' <summary>
	''' Data container.
	''' </summary>
	Private data As List(Of JsonValue)


	Public Sub New()
		Me.data = New List(Of JsonValue)()
	End Sub


	Public Sub New(capacity As Integer)
		Me.data = New List(Of JsonValue)(capacity)
	End Sub


	Public Sub New(collection As IEnumerable(Of JsonValue))
		Me.data = New List(Of JsonValue)(collection)
	End Sub


	Public Sub Add(item As JsonValue) Implements ICollection(Of JsonValue).Add
		Me.data.Add(item)
	End Sub


	Public Sub Insert(index As Integer, item As JsonValue) Implements IList(Of JsonValue).Insert
		Me.data.Insert(index, item)
	End Sub


	Public Function Contains(item As JsonValue) As Boolean Implements ICollection(Of JsonValue).Contains
		Return Me.data.Contains(item)
	End Function


	Public Function IndexOf(item As JsonValue) As Integer Implements IList(Of JsonValue).IndexOf
		Return Me.data.IndexOf(item)
	End Function


	Public Sub RemoveAt(index As Integer) Implements IList(Of JsonValue).RemoveAt
		Me.data.RemoveAt(index)
	End Sub


	Public Function Remove(item As JsonValue) As Boolean Implements ICollection(Of JsonValue).Remove
		Return Me.data.Remove(item)
	End Function


	Public Sub Clear() Implements ICollection(Of JsonValue).Clear
		Me.data.Clear()
	End Sub

#Region "ICollection"

	Protected ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of JsonValue).IsReadOnly
		Get
			Return Me.DataAsICollection.IsReadOnly
		End Get
	End Property


	Public Sub CopyTo(array() As JsonValue, arrayIndex As Integer) Implements ICollection(Of JsonValue).CopyTo
		Me.data.CopyTo(array, arrayIndex)
	End Sub

#End Region

#Region "IEnumerable"

	Public Function GetEnumerator() As IEnumerator(Of JsonValue) Implements IEnumerable(Of JsonValue).GetEnumerator
		Return Me.data.GetEnumerator()
	End Function


	Protected Function GetEnumeratorObject() As IEnumerator Implements IEnumerable.GetEnumerator
		Return Me.GetEnumerator()
	End Function

#End Region

#Region "ToString(), GetHashCode(), Equals()"

	Public Overrides Function ToString() As String
		Return JsonParser.Encode(Me)
	End Function


	Public Overrides Function GetHashCode() As Integer
		Return Me.data.GetHashCode()
	End Function


	Public Overrides Function Equals(obj As Object) As Boolean
		If obj Is Nothing Then
			Return False

		ElseIf obj Is Me Then
			Return True

		ElseIf TypeOf obj Is JsonArray Then
			Dim other = DirectCast(obj, JsonArray)
			Return Me.Equals(other)
		End If

		Return False
	End Function


	Public Overloads Function Equals(other As JsonArray) As Boolean
		If other Is Nothing Then
			Return False

		ElseIf other Is Me Then
			Return True

		End If

		If Me.Count <> other.Count Then
			Return False
		End If

		For index = 0 To Me.Count - 1
			If Not Me.Item(index).Equals(other.Item(index)) Then
				Return False
			End If
		Next

		Return True
	End Function

#End Region

End Class
