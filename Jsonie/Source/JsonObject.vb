''' <summary>
''' Represents a JSON object.
''' </summary>
<DebuggerDisplay("JsonObject: Count = {Count}")>
Public Class JsonObject
	Inherits JsonValue
	Implements IDictionary(Of String, JsonValue)


	''' <summary>
	''' Tests if json object is empty (has no property defined).
	''' </summary>
	Public ReadOnly Property IsEmpty As Boolean
		Get
			Return Me.Count = 0
		End Get
	End Property


	''' <summary>
	''' Gets the number of propeties contained in the object.
	''' </summary>
	Public ReadOnly Property Count As Integer Implements ICollection(Of KeyValuePair(Of String, JsonValue)).Count
		Get
			Return Me._data.Count
		End Get
	End Property


	''' <summary>
	''' Gets or sets the element with the specified <paramref name="key" />.
	''' </summary>
	''' <param name="key">The key of the element to get or set.</param>
	''' <value>The element with the specified key.</value>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is Nothing.</exception>
	''' <exception cref="KeyNotFoundException"><paramref name="key" /> is not found within dictionary.</exception>
	Default Public Property Item(key As String) As JsonValue Implements IDictionary(Of String, JsonValue).Item
		Get
			Return Me._data.Item(key)
		End Get
		Set(value As JsonValue)
			Me._data.Item(key) = value
		End Set
	End Property


	''' <summary>
	''' Store typed to collection.
	''' </summary>
	Private ReadOnly Property DataAsICollection As ICollection(Of KeyValuePair(Of String, JsonValue))
		Get
			Return _data
		End Get
	End Property


	Private ReadOnly _data As Dictionary(Of String, JsonValue)


	''' <summary>
	''' Creates new empty JSON object.
	''' </summary>
	Public Sub New()
		Me._data = New Dictionary(Of String, JsonValue)()
	End Sub


	''' <summary>
	''' Adds an member with the specified key and value to this object.
	''' </summary>
	''' <param name="key">The key of the element to add.</param>
	''' <param name="value">The value of the element to add. The value can be null.</param>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="ArgumentException">An property with the same <paramref name="key" /> already exists.</exception>
	Public Sub Add(key As String, value As JsonValue) Implements IDictionary(Of String, JsonValue).Add
		Me._data.Add(key, value)
	End Sub


	''' <summary>
	''' Adds an empty array member and returns its instance.
	''' </summary>
	''' <param name="key">The key of the array to add.</param>
	''' <returns>The added array.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="ArgumentException">An property with the same <paramref name="key" /> already exists.</exception>
	Public Function AddArray(key As String) As JsonArray
		Return Me.AddAndGet(key, New JsonArray())
	End Function


	''' <summary>
	''' Adds an empty object property and returns its instance.
	''' </summary>
	''' <param name="key">The key of the object to add.</param>
	''' <returns>The added object.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="ArgumentException">An property with the same <paramref name="key" /> already exists.</exception>
	Public Function AddObject(key As String) As JsonObject
		Return Me.AddAndGet(key, New JsonObject())
	End Function


	''' <summary>
	''' Gets the value associated with the specified key.
	''' </summary>
	''' <param name="key">The key of the member to get.</param>
	''' <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter.</param>
	''' <returns>true if object contains member with specified key, false otherwise.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	Public Function TryGetValue(key As String, ByRef value As JsonValue) As Boolean Implements IDictionary(Of String, JsonValue).TryGetValue
		Return Me._data.TryGetValue(key, value)
	End Function


	''' <summary>
	''' Gets value stored under given key and casts it to desired type. If not such member exists
	''' than defaultValue is returned.
	''' </summary>
	''' <typeparam name="TJsonValue">Desired type of value.</typeparam>
	''' <param name="key">The key under which is object stored.</param>
	''' <param name="defaultValue">Default value returned in case no such member exists.</param>
	''' <returns>Value stored under given key casted to TJsonValue.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="InvalidCastException">If property with given key exist and is not of given type TJsonValue.</exception>
	Public Function GetOrDefault(Of TJsonValue As {JsonValue})(key As String, Optional defaultValue As TJsonValue = Nothing) As TJsonValue
		Dim result As JsonValue = Nothing

		If Me.TryGetValue(key, result) Then
			' cast result to desired type
			Return CType(result, TJsonValue)
		Else
			Return defaultValue
		End If
	End Function


	''' <summary>
	''' Gets value stored under given key and casts it to boolean. If not such member exists
	''' than defaultValue is returned.
	''' </summary>
	''' <param name="key">The key under which is value stored.</param>
	''' <param name="defaultValue">Default value returned in case no such member exists.</param>
	''' <returns>Value stored under given key casted to boolean.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="InvalidCastException">If property with given key exist and is not of Boolean type.</exception>
	Public Function GetOrDefault(key As String, defaultValue As Boolean) As JsonBool
		Dim result As JsonValue = Nothing

		If Me.TryGetValue(key, result) Then
			' cast result to desired type
			Return CType(result, JsonBool)
		Else
			Return defaultValue
		End If
	End Function


	''' <summary>
	''' Gets value stored under given key and casts it to String. If not such member exists
	''' than defaultValue is returned.
	''' </summary>
	''' <param name="key">The key under which is value stored.</param>
	''' <param name="defaultValue">Default value returned in case no such member exists.</param>
	''' <returns>Value stored under given key casted to String.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="InvalidCastException">If property with given key exist and is not of String type.</exception>
	Public Function GetOrDefault(key As String, defaultValue As String) As JsonString
		Dim result As JsonValue = Nothing

		If Me.TryGetValue(key, result) Then
			' cast result to desired type
			Return CType(result, JsonString)
		Else
			Return defaultValue
		End If
	End Function


	''' <summary>
	''' Gets value stored under given key and casts it to decimal. If not such member exists
	''' than defaultValue is returned.
	''' </summary>
	''' <param name="key">The key under which is value stored.</param>
	''' <param name="defaultValue">Default value returned in case no such member exists.</param>
	''' <returns>Value stored under given key casted to String.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="InvalidCastException">If property with given key exist and is not of Decimal type.</exception>
	Public Function GetOrDefault(key As String, defaultValue As Decimal) As JsonNumber
		Dim result As JsonValue = Nothing

		If Me.TryGetValue(key, result) Then
			' cast result to desired type
			Return CType(result, JsonNumber)
		Else
			Return defaultValue
		End If
	End Function


	''' <summary>
	''' Gets value stored under given key and casts it to decimal. If not such member exists
	''' than defaultValue is returned.
	''' </summary>
	''' <param name="key">The key under which is value stored.</param>
	''' <param name="defaultValue">Default value returned in case no such member exists.</param>
	''' <returns>Value stored under given key casted to String.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="InvalidCastException">If property with given key exist and is not of Integer type.</exception>
	Public Function GetOrDefault(key As String, defaultValue As Integer) As JsonNumber
		Dim result As JsonValue = Nothing

		If Me.TryGetValue(key, result) Then
			' cast result to desired type
			Return CType(result, JsonNumber)
		Else
			Return defaultValue
		End If
	End Function


	''' <summary>
	''' Gets value stored under given key and casts it to decimal. If not such member exists
	''' than defaultValue is returned.
	''' </summary>
	''' <param name="key">The key under which is value stored.</param>
	''' <param name="defaultValue">Default value returned in case no such member exists.</param>
	''' <returns>Value stored under given key casted to String.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="InvalidCastException">If property with given key exist and is not of Long type.</exception>
	Public Function GetOrDefault(key As String, defaultValue As Long) As JsonNumber
		Dim result As JsonValue = Nothing

		If Me.TryGetValue(key, result) Then
			' cast result to desired type
			Return CType(result, JsonNumber)
		Else
			Return defaultValue
		End If
	End Function


	''' <summary>
	''' Gets value stored under given key and casts it to decimal. If not such member exists
	''' than defaultValue is returned.
	''' </summary>
	''' <param name="key">The key under which is value stored.</param>
	''' <param name="defaultValue">Default value returned in case no such member exists.</param>
	''' <returns>Value stored under given key casted to String.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="InvalidCastException">If property with given key exist and is not of Double type.</exception>
	Public Function GetOrDefault(key As String, defaultValue As Double) As JsonNumber
		Dim result As JsonValue = Nothing

		If Me.TryGetValue(key, result) Then
			' cast result to desired type
			Return CType(result, JsonNumber)
		Else
			Return defaultValue
		End If
	End Function


	''' <summary>
	''' Gets value stored under given key and casts it to decimal. If not such member exists
	''' than defaultValue is returned.
	''' </summary>
	''' <param name="key">The key under which is value stored.</param>
	''' <param name="defaultValue">Default value returned in case no such member exists.</param>
	''' <returns>Value stored under given key casted to String.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="InvalidCastException">If property with given key exist and is not of Single type.</exception>
	Public Function GetOrDefault(key As String, defaultValue As Single) As JsonNumber
		Dim result As JsonValue = Nothing

		If Me.TryGetValue(key, result) Then
			' cast result to desired type
			Return CType(result, JsonNumber)
		Else
			Return defaultValue
		End If
	End Function


	''' <summary>
	''' Gets the member stored under given key and casts it to desired type. If key does not exist specified value is 
	''' stored as a new member.
	''' </summary>
	''' <typeparam name="TJsonValue">Type of the value to get.</typeparam>
	''' <param name="key">The key of the member to get.</param>
	''' <param name="addValue">The value to add if specified member does not exist.</param>
	''' <returns>The value stored under given key casted to the TJsonValue.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is Nothing.</exception>
	''' <exception cref="InvalidCastException">If property with given key exist and is not of TJsonValue type.</exception>
	Public Function GetOrAdd(Of TJsonValue As {JsonValue})(key As String, addValue As TJsonValue) As TJsonValue
		Dim result As JsonValue = Nothing

		If Me.TryGetValue(key, result) Then
			' cast result to desired type
			Return CType(result, TJsonValue)
		Else
			Return Me.AddAndGet(key, addValue)
		End If
	End Function


	''' <summary>
	''' Gets value stored under given key and casts it to object. If key does not exist new empty object is added to
	''' the specified key.
	''' </summary>
	''' <example>
	''' Dim myInnerObject = myObject.GetOrAddObject("inner")
	''' myInnerObject("some") = value
	''' </example>
	''' <param name="key">The key of the member to get.</param>
	''' <returns>The value stored under given key casted to <see cref="JsonObject" />.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="InvalidCastException">If property with given key exist and is not <see cref="JsonObject" />.</exception>
	Public Function GetOrAddObject(key As String) As JsonObject
		Dim value As JsonValue = Nothing

		If Me.TryGetValue(key, value) Then
			' cast value to object
			Return CType(value, JsonObject)
		Else
			Return Me.AddAndGet(key, New JsonObject())
		End If
	End Function


	''' <summary>
	''' Gets value stored under given key and casts it to array. If key does not exist new empty array is added to the
	''' specified key.
	''' </summary>
	''' <example>
	''' Dim myInnerArray = myObject.GetOrAddArray("inner")
	''' myInnerArray.Add(True)
	''' </example>
	''' <param name="key">The key of the member to get.</param>
	''' <returns>The value stored under given key casted to <see cref="JsonArray" />.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="InvalidCastException">If property with given key exist and is not of <see cref="JsonArray" />.</exception>
	Public Function GetOrAddArray(key As String) As JsonArray
		Dim value As JsonValue = Nothing

		If Me.TryGetValue(key, value) Then
			' cast value to object
			Return CType(value, JsonArray)
		Else
			Return Me.AddAndGet(key, New JsonArray())
		End If
	End Function


	''' <summary>
	''' Adds new member into object and returns added value (actually the passed parameter). This is handy for one-line 
	''' adding and variable initalization:
	''' <example>
	''' Dim myArray = myObject.AddAndGet("array", new JsonArray())
	''' </example>
	''' </summary>
	''' <typeparam name="TJsonValue">Type of the value being added.</typeparam>
	''' <param name="key">The key of the value.</param>
	''' <param name="value">The value to add.</param>
	''' <returns>The value of the added member.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="ArgumentException">An property with the same <paramref name="key" /> already exists.</exception>
	Protected Function AddAndGet(Of TJsonValue As {JsonValue})(key As String, value As TJsonValue) As TJsonValue
		Me._data.Add(key, value)
		Return value
	End Function


	''' <summary>
	''' Removes the member with the specified key from the object.
	''' </summary>
	''' <param name="key">The key of the memeber to remove.</param>
	''' <returns>true if member is successfully found and removed, false otherwise.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	Public Function Remove(key As String) As Boolean Implements IDictionary(Of String, JsonValue).Remove
		Return Me._data.Remove(key)
	End Function


	''' <summary>
	''' Tests whether the object contains a member with the specified key. It does not check the value of the member 
	''' which might be null.
	''' </summary>
	''' <param name="key">The key of the member to find.</param>
	''' <returns>True if member with specified key member is defined in the object.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	Public Function ContainsKey(key As String) As Boolean Implements IDictionary(Of String, JsonValue).ContainsKey
		Return Me._data.ContainsKey(key)
	End Function


	''' <summary>
	''' Tests whether the object contains a member with the specified key and which is not null.
	''' </summary>
	''' <param name="key">The key of the member to find.</param>
	''' <returns>true if member with specified key exists and is not null.</returns>
	''' <remarks>Equivalent to Me.ContainsKey(key) AndAlso Me(key) IsNot Nothing.</remarks>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	Public Function ContainsKeyNotNull(key As String) As Boolean
		Dim value As JsonValue = Nothing
		If Me.TryGetValue(key, value) Then
			Return value IsNot Nothing
		Else
			Return False
		End If
	End Function


	''' <summary>
	''' Removes all members from the object.
	''' </summary>
	Public Sub Clear() Implements ICollection(Of KeyValuePair(Of String, JsonValue)).Clear
		Me._data.Clear()
	End Sub

#Region "IDictionary"

	''' <summary>
	''' Gets an ICollection(Of T) containing the keys of the object.
	''' </summary>
	Public ReadOnly Property Keys As ICollection(Of String) Implements IDictionary(Of String, JsonValue).Keys
		Get
			Return Me._data.Keys
		End Get
	End Property


	''' <summary>
	''' Gets an ICollection(Of T) containing the values in the object.
	''' </summary>
	Public ReadOnly Property Values As ICollection(Of JsonValue) Implements IDictionary(Of String, JsonValue).Values
		Get
			Return Me._data.Values
		End Get
	End Property

#End Region

#Region "ICollection"

	''' <summary>
	''' Returns False.
	''' </summary>
	Protected ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of KeyValuePair(Of String, JsonValue)).IsReadOnly
		Get
			Return Me.DataAsICollection.IsReadOnly
		End Get
	End Property


	''' <summary>
	''' Adds an item to the collection.
	''' </summary>
	''' <param name="item">Item to add.</param>
	Protected Sub Add(item As KeyValuePair(Of String, JsonValue)) Implements ICollection(Of KeyValuePair(Of String, JsonValue)).Add
		Me.DataAsICollection.Add(item)
	End Sub


	''' <summary>
	''' Determines whether the collection contains a specific value.
	''' </summary>
	''' <param name="item">The item to locate in the object.</param>
	''' <returns>true if item is found in the object, false otherwise</returns>
	Protected Function Contains(item As KeyValuePair(Of String, JsonValue)) As Boolean Implements ICollection(Of KeyValuePair(Of String, JsonValue)).Contains
		Return Me._data.Contains(item)
	End Function


	''' <summary>
	''' Copies the elements of the collection to an Array, starting at a particular Array index.
	''' </summary>
	''' <param name="array">The destination array.</param>
	''' <param name="arrayIndex">The offset index where the elements will be copied to.</param>
	Public Sub CopyTo(array As KeyValuePair(Of String, JsonValue)(), arrayIndex As Integer) Implements ICollection(Of KeyValuePair(Of String, JsonValue)).CopyTo
		Me.DataAsICollection.CopyTo(array, arrayIndex)
	End Sub


	''' <summary>
	''' Removes the first occurrence of a specific object from the collection.
	''' </summary>
	''' <param name="item">The item to remove from the object.</param>
	''' <returns>true if item was successfully removed from the collection, false otherwise.</returns>
	Protected Function Remove(item As KeyValuePair(Of String, JsonValue)) As Boolean Implements ICollection(Of KeyValuePair(Of String, JsonValue)).Remove
		Return Me.DataAsICollection.Remove(item)
	End Function

#End Region

#Region "IEnumerable"

	''' <summary>
	''' Returns an enumerator that iterates through the object.
	''' </summary>
	''' <returns>An enumerator for the object.</returns>
	Public Function GetEnumerator() As IEnumerator(Of KeyValuePair(Of String, JsonValue)) Implements IEnumerable(Of KeyValuePair(Of String, JsonValue)).GetEnumerator
		Return Me._data.GetEnumerator()
	End Function


	''' <summary>
	''' Returns an enumerator that iterates through the object.
	''' </summary>
	''' <returns>An enumerator for the object.</returns>
	Private Function GetEnumeratorObject() As IEnumerator Implements IEnumerable.GetEnumerator
		Return Me._data.GetEnumerator()
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

		ElseIf TypeOf obj Is JsonObject Then
			Dim other = DirectCast(obj, JsonObject)
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
	Public Overloads Function Equals(other As JsonObject) As Boolean
		If other Is Nothing Then
			Return False

		ElseIf other Is Me Then
			Return True

		ElseIf Me.Count <> other.Count Then
			Return False

		End If

		For Each pair In Me
			Dim otherValue As JsonValue = Nothing

			If other.TryGetValue(pair.Key, otherValue) Then
				If pair.Value IsNot Nothing Then
					If Not pair.Value.Equals(otherValue) Then
						Return False
					End If
				ElseIf otherValue IsNot Nothing Then
					If Not otherValue.Equals(pair.Value) Then
						Return False
					End If
				End If
			Else
				Return False
			End If
		Next

		For Each pair In other
			' equality was tested in previous for
			If Not Me.ContainsKey(pair.Key) Then
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
