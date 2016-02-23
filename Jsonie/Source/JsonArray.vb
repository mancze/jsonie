''' <summary>
''' Represents a JSON array.
''' </summary>
''' <remarks>
''' Despite the naming "array" (which was given to match the JSON specification) the class internally works with 
''' <see cref="List(Of JsonValue)" /> and behaves like ordinary <see cref="IList(Of JsonValue)" />.
''' </remarks>
<DebuggerDisplay("JsonArray: Count = {Count}")>
Public Class JsonArray
	Inherits JsonValue
	Implements IList(Of JsonValue)


	''' <summary>
	''' Gets the number of elements contained in array.
	''' </summary>
	Public ReadOnly Property Count As Integer Implements ICollection(Of JsonValue).Count
		Get
			Return Me._data.Count
		End Get
	End Property


	''' <summary>
	''' Gets or sets the element at the specified index.
	''' </summary>
	''' <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is less than 0 or is equal or more than <see cref="Count" />.</exception>
	Default Public Property Item(index As Integer) As JsonValue Implements IList(Of JsonValue).Item
		Get
			Return Me._data(index)
		End Get
		Set(value As JsonValue)
			Me._data(index) = value
		End Set
	End Property


	''' <summary>
	''' Casted data to ICollection.
	''' </summary>
	Private ReadOnly Property DataAsICollection As ICollection(Of JsonValue)
		Get
			Return Me._data
		End Get
	End Property


	''' <summary>
	''' Data container.
	''' </summary>
	Private ReadOnly _data As List(Of JsonValue)


	''' <summary>
	''' Creates new empty JSON array.
	''' </summary>
	Public Sub New()
		Me._data = New List(Of JsonValue)()
	End Sub


	''' <summary>
	''' Creates new empty JSON array which and has the specified initial capacity.
	''' </summary>
	''' <param name="capacity">The number of elements that the new array can initially store.</param>
	Public Sub New(capacity As Integer)
		Me._data = New List(Of JsonValue)(capacity)
	End Sub


	''' <summary>
	''' Creates new JSON array that contains elements copied from the given collection.
	''' </summary>
	''' <param name="collection">The collection whose elements to copy to the new array.</param>
	Public Sub New(collection As IEnumerable(Of JsonValue))
		Me._data = New List(Of JsonValue)(collection)
	End Sub


	''' <summary>
	''' Adds an items to the end of the array.
	''' </summary>
	''' <param name="item">Value to add.</param>
	Public Sub Add(item As JsonValue) Implements ICollection(Of JsonValue).Add
		Me._data.Add(item)
	End Sub


	''' <summary>
	''' Adds the values of the specified collection to the end of the array.
	''' </summary>
	''' <param name="items">Collection of items to add.</param>
	Public Sub AddRange(items As IEnumerable(Of JsonValue))
		Me._data.AddRange(items)
	End Sub


	''' <summary>
	''' Adds an empty array and returns its instance.
	''' </summary>
	''' <returns>The added array.</returns>
	Public Function AddArray() As JsonArray
		Dim array = New JsonArray()
		Me.Add(array)
		Return array
	End Function


	''' <summary>
	''' Adds an empty object and returns its instance.
	''' </summary>
	''' <returns>The added object.</returns>
	Public Function AddObject() As JsonObject
		Dim obj = New JsonObject()
		Me.Add(obj)
		Return obj
	End Function


	''' <summary>
	''' Inserts an value into the array at the specified index.
	''' </summary>
	''' <param name="index">The zero-based index at which item should be inserted.</param>
	''' <param name="item">The object to insert. The value can be null.</param>
	''' <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is less than 0 or is equal or more than <see cref="Count" />.</exception>
	Public Sub Insert(index As Integer, item As JsonValue) Implements IList(Of JsonValue).Insert
		Me._data.Insert(index, item)
	End Sub


	''' <summary>
	''' Inserts the values of a collection into the array at the specified index.
	''' </summary>
	''' <param name="index">The zero-based index at which items should be inserted.</param>
	''' <param name="items">The collection of items which will be inserted to the specified position in the array. The collection can contains items whose value is null.</param>
	''' <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is less than 0 or is equal or more than <see cref="Count" />.</exception>
	''' <exception cref="ArgumentNullException"><paramref name="items" /> is null.</exception>
	Public Sub InsertRange(index As Integer, items As IEnumerable(Of JsonValue))
		Me._data.InsertRange(index, items)
	End Sub


	''' <summary>
	''' Inserts an empty array at the specified index and returns its instance.
	''' </summary>
	''' <param name="index">The zero-based index at which item should be inserted.</param>
	''' <returns>The inserted array.</returns>
	''' <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is less than 0 or is equal or more than <see cref="Count" />.</exception>
	Public Function InsertArray(index As Integer) As JsonArray
		Dim array = New JsonArray()
		Me.Insert(index, array)
		Return array
	End Function


	''' <summary>
	''' Adds an empty object at the specified index  and returns its instance.
	''' </summary>
	''' <param name="index">The zero-based index at which item should be inserted.</param>
	''' <returns>The inserted object.</returns>
	''' <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is less than 0 or is equal or more than <see cref="Count" />.</exception>
	Public Function InsertObject(index As Integer) As JsonObject
		Dim obj = New JsonObject()
		Me.Insert(index, obj)
		Return obj
	End Function


	''' <summary>
	''' Determines whether an element is in the array.
	''' </summary>
	''' <param name="item">The object to locate in the array. The value can be null.</param>
	''' <returns>true if item is found in the array; otherwise, false.</returns>
	Public Function Contains(item As JsonValue) As Boolean Implements ICollection(Of JsonValue).Contains
		Return Me._data.Contains(item)
	End Function


	''' <summary>
	''' Searches for the specified object and returns the zero-based index of the first occurrence within the entire 
	''' array.
	''' </summary>
	''' <param name="item">The object to locate in the array. The value can be null.</param>
	''' <returns>
	''' The zero-based index of the first occurrence of item within the entire array, if found; otherwise, –1.
	''' </returns>
	Public Function IndexOf(item As JsonValue) As Integer Implements IList(Of JsonValue).IndexOf
		Return Me._data.IndexOf(item)
	End Function


	''' <summary>
	''' Removes the element at the specified index of the array.
	''' </summary>
	''' <param name="index">The zero-based index of the element to remove.</param>
	''' <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is less than 0 or is equal or more than <see cref="Count" />.</exception>
	Public Sub RemoveAt(index As Integer) Implements IList(Of JsonValue).RemoveAt
		Me._data.RemoveAt(index)
	End Sub


	''' <summary>
	''' Removes the first occurrence of a specific object from the JsonArray.
	''' </summary>
	''' <param name="item">The object to remove from the JsonArray. The value can be null.</param>
	''' <returns>
	''' true if item is successfully removed; otherwise, false. This method also returns false if item was not found 
	''' in the array.
	''' </returns>
	Public Function Remove(item As JsonValue) As Boolean Implements ICollection(Of JsonValue).Remove
		Return Me._data.Remove(item)
	End Function


	''' <summary>
	''' Removes all elements from the array.
	''' </summary>
	Public Sub Clear() Implements ICollection(Of JsonValue).Clear
		Me._data.Clear()
	End Sub

#Region "ICollection"

	''' <summary>
	''' Gets a value indicating whether the System.Collections.Generic.ICollection(Of T) is read-only.
	''' </summary>
	Protected ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of JsonValue).IsReadOnly
		Get
			Return Me.DataAsICollection.IsReadOnly
		End Get
	End Property


	''' <summary>
	''' Copies the entire JsonArray to a compatible one-dimensional array, starting at the specified index of the 
	''' target array.
	''' </summary>
	''' <param name="array">
	''' The one-dimensional System.Array that is the destination of the elements copied from JsonArray. 
	''' The System.Array must have zero-based indexing.
	''' </param>
	''' <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
	''' <exception cref="ArgumentNullException">array is null.</exception>
	''' <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
	''' <exception cref="ArgumentException">
	''' The number of elements in the JsonArray is greater than the available space from arrayIndex to the end of
	''' the destination array.
	''' </exception>
	Public Sub CopyTo(array() As JsonValue, arrayIndex As Integer) Implements ICollection(Of JsonValue).CopyTo
		Me._data.CopyTo(array, arrayIndex)
	End Sub

#End Region

#Region "IEnumerable"

	''' <summary>
	''' Returns an enumerator that iterates through the array.
	''' </summary>
	''' <returns>An enumerator for the array.</returns>
	Public Function GetEnumerator() As IEnumerator(Of JsonValue) Implements IEnumerable(Of JsonValue).GetEnumerator
		Return Me._data.GetEnumerator()
	End Function


	''' <summary>
	''' Returns an enumerator that iterates through the array.
	''' </summary>
	''' <returns>An enumerator for the array.</returns>
	Private Function GetEnumeratorObject() As IEnumerator Implements IEnumerable.GetEnumerator
		Return Me.GetEnumerator()
	End Function

#End Region

#Region "GetHashCode(), Equals()"

	''' <summary>
	''' Serves as a hash function for a particular type.
	''' </summary>
	''' <returns>A hash code for the current <see cref="T:System.Object"/>.</returns>
	Public Overrides Function GetHashCode() As Integer
		Return Me._data.GetHashCode()
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

		ElseIf TypeOf obj Is JsonArray Then
			Dim other = DirectCast(obj, JsonArray)
			Return Me.Equals(other)

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


	''' <summary>
	''' Determines whether current object is equal to another object.
	''' </summary>
	''' <param name="other">The object to compare with the current object.</param>
	''' <returns>True if the current object is equal to this, false otherwise.</returns>
	Public Overloads Function Equals(other As JsonDynamic) As Boolean
		Return Me.Equals(other.Value)
	End Function

#End Region

End Class
