﻿<DebuggerDisplay("JsonObject: Count = {Count}")>
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
			Return Me.data.Count
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
			Return Me.data.Item(key)
		End Get
		Set(value As JsonValue)
			Me.data.Item(key) = value
		End Set
	End Property


	''' <summary>
	''' Store typed to collection.
	''' </summary>
	Private ReadOnly Property DataAsICollection As ICollection(Of KeyValuePair(Of String, JsonValue))
		Get
			Return data
		End Get
	End Property


	''' <summary>
	''' Underlying store.
	''' </summary>
	Private data As Dictionary(Of String, JsonValue)


	Public Sub New()
		Me.data = New Dictionary(Of String, JsonValue)
	End Sub


	''' <summary>
	''' Adds an element with the provided key and value to the object.
	''' </summary>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="ArgumentException">An property with the same <paramref name="key" /> already exists.</exception>
	Public Sub Add(key As String, value As JsonValue) Implements IDictionary(Of String, JsonValue).Add
		Me.data.Add(key, value)
	End Sub


	''' <summary>
	''' Adds an blank array property and returns it's instance.
	''' </summary>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="ArgumentException">An property with the same <paramref name="key" /> already exists.</exception>
	Public Function AddArray(key As String) As JsonArray
		Return Me.AddAndGet(key, New JsonArray())
	End Function


	''' <summary>
	''' Adds an blank object property and returns it's instance.
	''' </summary>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="ArgumentException">An property with the same <paramref name="key" /> already exists.</exception>
	Public Function AddObject(key As String) As JsonObject
		Return Me.AddAndGet(key, New JsonObject())
	End Function


	''' <summary>
	''' Gets the value associated with the specified key.
	''' </summary>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	Public Function TryGetValue(key As String, ByRef value As JsonValue) As Boolean Implements IDictionary(Of String, JsonValue).TryGetValue
		Return Me.data.TryGetValue(key, value)
	End Function


	''' <summary>
	''' Gets value stored under given key and casts it to desired type. If not such member exists
	''' than defaultValue is returned.
	''' </summary>
	''' <typeparam name="TJsonValue">Desired type of value.</typeparam>
	''' <param name="key">Key under which is object stored.</param>
	''' <param name="defaultValue">Default value returned in case no such member exists.</param>
	''' <returns>Value stored under given key casted to TJsonValue</returns>
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
	''' <param name="key">Key under which is object stored.</param>
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
	''' <param name="key">Key under which is object stored.</param>
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
	''' <param name="key">Key under which is object stored.</param>
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
	''' <param name="key">Key under which is object stored.</param>
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
	''' <param name="key">Key under which is object stored.</param>
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
	''' <param name="key">Key under which is object stored.</param>
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
	''' <param name="key">Key under which is object stored.</param>
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
	''' Safetly gets object stored under given key. If key does not exist, new JsonObject is stored
	''' under given key and returned. If null is stored under given key, new JsonObject takes
	''' places instead and also is returned.
	''' 
	''' Therefore this method des not return null, only exception might be thrown if another value
	''' is already stored under given key and it's not JsonObject.
	''' </summary>
	''' <example>
	''' Dim myInnerObject = myObject.GetOrAddObject("inner")
	''' myInnerObject("some") = value
	''' </example>
	''' <param name="key">Key under which is object stored.</param>
	''' <returns>Value stored under given key casted as object.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="InvalidCastException">If property with given key exist and is not of given type TJsonValue.</exception>
	Public Function GetOrAddObject(key As String) As JsonObject
		Dim value As JsonValue = Nothing
		If Me.TryGetValue(key, value) Then
			If value Is Nothing Then
				' overwrite null to object
				Dim newObject = New JsonObject()
				Me(key) = newObject
				Return newObject

			Else
				' cast value to object
				Return CType(value, JsonObject)

			End If
		Else
			Return Me.AddAndGet(key, New JsonObject())

		End If
	End Function


	''' <summary>
	''' Safetly gets array stored under given key. If key does not exist, new JsonArray is stored
	''' under given key and returned. If null is stored under given key, new JsonArray takes
	''' places instead and also is returned.
	''' 
	''' Therefore this method des not return null, only exception might be thrown if another value
	''' is already stored under given key and it's not JsonArray.
	''' </summary>
	''' <example>
	''' Dim myInnerArray = myObject.GetOrAddArray("inner")
	''' myInnerObject.Add(True)
	''' </example>
	''' <param name="key">Key under which is array stored.</param>
	''' <returns>Value stored under given key casted as array.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="InvalidCastException">If property with given key exist and is not of given type TJsonValue.</exception>
	Public Function GetOrAddArray(key As String) As JsonArray
		Dim value As JsonValue = Nothing
		If Me.TryGetValue(key, value) Then
			If value Is Nothing Then
				' overwrite null to object
				Dim newObject = New JsonArray()
				Me(key) = newObject
				Return newObject

			Else
				' cast value to object
				Return CType(value, JsonArray)

			End If
		Else
			Return Me.AddAndGet(key, New JsonArray())

		End If
	End Function


	''' <summary>
	''' Adds item into object and returns added value (actually the passed parameter).
	''' This is handy for one-line adding and variable initalization:
	''' <example>
	''' Dim myArray = myObject.AddAndGet("array", new JsonArray())
	''' </example>
	''' </summary>
	''' <typeparam name="TJsonValue">Type of the value being added.</typeparam>
	''' <param name="key">Key of the value.</param>
	''' <param name="value">Value to add.</param>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="ArgumentException">An property with the same <paramref name="key" /> already exists.</exception>
	Protected Function AddAndGet(Of TJsonValue As {JsonValue})(key As String, value As TJsonValue) As TJsonValue
		Me.data.Add(key, value)
		Return value
	End Function


	''' <summary>
	''' Removes the element with the specified key from the object.
	''' </summary>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	Public Function Remove(key As String) As Boolean Implements IDictionary(Of String, JsonValue).Remove
		Return Me.data.Remove(key)
	End Function


	''' <summary>
	''' Determines whether the JSON object contains an property with the specified key. It does not check the value of
	''' the property which might be null.
	''' </summary>
	''' <returns>True if given property is defined in the object.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	Public Function ContainsKey(key As String) As Boolean Implements IDictionary(Of String, JsonValue).ContainsKey
		Return Me.data.ContainsKey(key)
	End Function


	''' <summary>
	''' Determines whether the JSON object contains an property with the specified key and which is not null.
	''' </summary>
	''' <returns>True if Me(key) exists and is not null.</returns>
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
	''' Removes all properties from the object.
	''' </summary>
	Public Sub Clear() Implements ICollection(Of KeyValuePair(Of String, JsonValue)).Clear
		Me.data.Clear()
	End Sub

#Region "IDictionary"

	''' <summary>
	''' Gets an ICollection(Of T) containing the keys of the object.
	''' </summary>
	Public ReadOnly Property Keys As ICollection(Of String) Implements IDictionary(Of String, JsonValue).Keys
		Get
			Return Me.data.Keys
		End Get
	End Property


	''' <summary>
	''' Gets an ICollection(Of T) containing the values in the object.
	''' </summary>
	Public ReadOnly Property Values As ICollection(Of JsonValue) Implements IDictionary(Of String, JsonValue).Values
		Get
			Return Me.data.Values
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
	Protected Sub Add(item As KeyValuePair(Of String, JsonValue)) Implements ICollection(Of KeyValuePair(Of String, JsonValue)).Add
		Me.DataAsICollection.Add(item)
	End Sub


	''' <summary>
	''' Determines whether the collection contains a specific value.
	''' </summary>
	Protected Function Contains(item As KeyValuePair(Of String, JsonValue)) As Boolean Implements ICollection(Of KeyValuePair(Of String, JsonValue)).Contains
		Return Me.data.Contains(item)
	End Function


	''' <summary>
	''' Copies the elements of the collection to an Array, starting at a particular Array index.
	''' </summary>
	Public Sub CopyTo(array As KeyValuePair(Of String, JsonValue)(), arrayIndex As Integer) Implements ICollection(Of KeyValuePair(Of String, JsonValue)).CopyTo
		Me.DataAsICollection.CopyTo(array, arrayIndex)
	End Sub


	''' <summary>
	''' Removes the first occurrence of a specific object from the collection.
	''' </summary>
	Protected Function Remove(item As KeyValuePair(Of String, JsonValue)) As Boolean Implements ICollection(Of KeyValuePair(Of String, JsonValue)).Remove
		Return Me.DataAsICollection.Remove(item)
	End Function

#End Region

#Region "IEnumerable"

	Public Function GetEnumerator() As IEnumerator(Of KeyValuePair(Of String, JsonValue)) Implements IEnumerable(Of KeyValuePair(Of String, JsonValue)).GetEnumerator
		Return Me.data.GetEnumerator()
	End Function


	Protected Function GetEnumeratorObject() As IEnumerator Implements IEnumerable.GetEnumerator
		Return Me.data.GetEnumerator()
	End Function

#End Region

#Region "GetHashCode(), Equals()"

	Public Overrides Function GetHashCode() As Integer
		Return Me.data.GetHashCode()
	End Function


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


	Public Overloads Function Equals(other As JsonDynamic) As Boolean
		Return Me.Equals(other.Value)
	End Function

#End Region

End Class
