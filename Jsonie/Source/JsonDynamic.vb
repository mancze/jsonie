Imports Dextronet.Jsonie.Extensions


''' <summary>
''' Dynamic wrapper around any JsonValue.
''' </summary>
Public Structure JsonDynamic

	Public Shared ReadOnly [Null] As JsonDynamic = Nothing


	''' <summary>
	''' Tests if this is null JSON object.
	''' </summary>
	Public ReadOnly Property IsNull As Boolean
		Get
			Return Me._value Is Nothing
		End Get
	End Property


	''' <summary>
	''' Tests if this is JSON object.
	''' </summary>
	Public ReadOnly Property IsObject As Boolean
		Get
			Return TypeOf Me._value Is JsonObject
		End Get
	End Property


	''' <summary>
	''' Tests if this is JSON array.
	''' </summary>
	Public ReadOnly Property IsArray As Boolean
		Get
			Return TypeOf Me._value Is JsonArray
		End Get
	End Property


	''' <summary>
	''' Tests if this is not JSON object nor array.
	''' </summary>
	Public ReadOnly Property IsScalar As Boolean
		Get
			If Me._value Is Nothing Then
				Return True
			End If

			Return Not Me._value.IsObject AndAlso Not Me._value.IsArray
		End Get
	End Property


	''' <summary>
	''' Tests if this is JSON string.
	''' </summary>
	Public ReadOnly Property IsString As Boolean
		Get
			Return TypeOf Me._value Is JsonString
		End Get
	End Property


	''' <summary>
	''' Test if this is JSON number.
	''' </summary>
	Public ReadOnly Property IsNumber As Boolean
		Get
			Return TypeOf Me._value Is JsonNumber
		End Get
	End Property


	''' <summary>
	''' Tests if this is JSON boolean.
	''' </summary>
	Public ReadOnly Property IsBoolean As Boolean
		Get
			Return TypeOf Me._value Is JsonBool
		End Get
	End Property


	''' <summary>
	''' Gets the number of items in array or members in object of the dynamic type.
	''' </summary>
	Public ReadOnly Property Count As Integer
		Get
			If Me._value Is Nothing Then
				Return 0
			ElseIf Me._value.IsObject Then
				Return Me._value.AsObject().Count
			ElseIf Me._value.IsArray Then
				Return Me._value.AsArray().Count
			Else
				Return 0
			End If
		End Get
	End Property


	''' <summary>
	''' Gets the wrapped value.
	''' </summary>
	Public ReadOnly Property Value As JsonValue
		Get
			Return _value
		End Get
	End Property
	Friend ReadOnly _value As JsonValue


	Default Public Property Item(key As String) As JsonDynamic
		Get
			Return Me._value.AsObject()(key).ToDynamic()
		End Get
		Set(value As JsonDynamic)
			Me._value.AsObject()(key) = value.Value
		End Set
	End Property


	Default Public Property Item(index As Integer) As JsonDynamic
		Get
			Return Me._value.AsArray()(index).ToDynamic()
		End Get
		Set(value As JsonDynamic)
			Me._value.AsArray()(index).ToDynamic()
		End Set
	End Property


	''' <summary>
	''' Creates new dynamic wrapper around any JsonValue (null accepted).
	''' </summary>
	''' <param name="value"></param>
	''' <remarks></remarks>
	Public Sub New(value As JsonValue)
		Me._value = value
	End Sub

#Region "Object Methods"

	''' <summary>
	''' Adds an member with the provided key to the object.
	''' </summary>
	''' <exception cref="ArgumentNullException">key is null.</exception>
	''' <exception cref="ArgumentException">An property with the same key already exists.</exception>
	''' <exception cref="InvalidCastException">If this dynamic object does not wrap JsonObject.</exception>
	Public Sub Add(key As String, value As JsonValue)
		Me._value.AsObject().Add(key, value)
	End Sub


	''' <summary>
	''' Adds an blank array property and returns it's instance.
	''' </summary>
	''' <exception cref="ArgumentNullException">key is null.</exception>
	''' <exception cref="ArgumentException">An property with the same key already exists.</exception>
	''' <exception cref="InvalidCastException">If this dynamic object does not wrap JsonObject.</exception>
	Public Function AddArray(key As String) As JsonDynamic
		Return Me._value.AsObject().AddArray(key).ToDynamic()
	End Function


	''' <summary>
	''' Adds an blank object property and returns it's instance.
	''' </summary>
	''' <exception cref="ArgumentNullException">key is null.</exception>
	''' <exception cref="ArgumentException">An property with the same key already exists.</exception>
	''' <exception cref="InvalidCastException">If this dynamic object does not wrap JsonObject.</exception>
	Public Function AddObject(key As String) As JsonDynamic
		Return Me._value.AsObject().AddObject(key).ToDynamic()
	End Function


	''' <summary>
	''' Gets value stored under given key and returns it as dynamic type. If not such member exists than defaultValue 
	''' is returned.
	''' </summary>
	''' <param name="key">Key under which is member stored.</param>
	''' <param name="defaultValue">Default value returned in case no such member exists.</param>
	''' <returns>Value stored under given key as dynamic.</returns>
	''' <exception cref="InvalidCastException">If this dynamic object does not wrap JsonObject.</exception>
	Public Function GetOrDefault(key As String, Optional defaultValue As JsonValue = Nothing) As JsonDynamic
		Dim result As JsonValue = Nothing

		If Me._value Is Nothing Then
			Throw New InvalidCastException("Dynamic type represents null.")
		ElseIf Me._value.AsObject().TryGetValue(key, result) Then
			Return result.ToDynamic()
		Else
			Return defaultValue.ToDynamic()
		End If
	End Function


	''' <summary>
	''' Gets value stored under given key and casts it to dynamic type. If not such member exists than defaultValue 
	''' is returned.
	''' </summary>
	''' <param name="key">Key under which is member stored.</param>
	''' <param name="defaultValue">Default value returned in case no such member exists.</param>
	''' <returns>Value stored under given key as dynamic.</returns>
	''' <exception cref="InvalidCastException">If this dynamic object does not wrap JsonObject.</exception>
	Public Function GetOrDefault(key As String, defaultValue As JsonDynamic) As JsonDynamic
		Dim result As JsonValue = Nothing

		If Me._value Is Nothing Then
			Throw New InvalidCastException("Dynamic type represents null.")
		ElseIf Me._value.AsObject().TryGetValue(key, result) Then
			' cast result to desired type
			Return result.ToDynamic()
		Else
			Return defaultValue
		End If
	End Function


	''' <summary>
	''' Gets the member stored under given key. If key does not exist provided value is stored as new member.
	''' </summary>
	''' <param name="key">Key under which is array stored.</param>
	''' <returns>Value stored under given key casted to dynamic.</returns>
	''' <exception cref="InvalidCastException">If this dynamic object does not wrap JsonObject.</exception>
	Public Function GetOrAdd(key As String, addValue As JsonDynamic) As JsonDynamic
		Dim value As JsonValue = Nothing

		If Me._value Is Nothing Then
			Throw New InvalidCastException("Dynamic type represents null.")
		ElseIf Me._value.AsObject().TryGetValue(key, value) Then
			Return value.ToDynamic()
		Else
			Me(key) = addValue
			Return addValue
		End If
	End Function


	''' <summary>
	''' Gets the member stored under given key. If key does not exist provided value is stored as new member.
	''' </summary>
	''' <param name="key">Key under which is array stored.</param>
	''' <returns>Value stored under given key casted to dynamic.</returns>
	''' <exception cref="InvalidCastException">If this dynamic object does not wrap JsonObject.</exception>
	Public Function GetOrAdd(key As String, addValue As JsonValue) As JsonDynamic
		Dim value As JsonValue = Nothing

		If Me._value Is Nothing Then
			Throw New InvalidCastException("Dynamic type represents null.")
		ElseIf Me._value.AsObject().TryGetValue(key, value) Then
			Return value.ToDynamic()
		Else
			Me._value.AsObject()(key) = addValue
			Return addValue.ToDynamic()
		End If
	End Function


	''' <summary>
	''' Determines whether the JSON object contains an property with the specified key. It does not check the value of
	''' the property which might be null.
	''' </summary>
	''' <returns>True if given property is defined in the object.</returns>
	''' <exception cref="InvalidCastException">If this dynamic object does not wrap JsonObject.</exception>
	Public Function ContainsKey(key As String) As Boolean
		If Me._value Is Nothing Then
			Throw New InvalidCastException("Dynamic type represents null.")
		Else
			Return Me._value.AsObject().ContainsKey(key)
		End If
	End Function


	''' <summary>
	''' Determines whether the JSON object contains an property with the specified key and which is not null.
	''' </summary>
	''' <returns>True if Me(key) exists and is not null.</returns>
	''' <remarks>Equivalent to Me.ContainsKey(key) AndAlso Me(key) IsNot Nothing.</remarks>
	''' <exception cref="InvalidCastException">If this dynamic object does not wrap JsonObject.</exception>
	Public Function ContainsKeyNotNull(key As String) As Boolean
		If Me._value Is Nothing Then
			Throw New InvalidCastException("Dynamic type represents null.")
		Else
			Return Me._value.AsObject().ContainsKeyNotNull(key)
		End If
	End Function

#End Region

#Region "Array Methods"

	''' <summary>
	''' Adds an items to the end of the array.
	''' </summary>
	''' <param name="item">Value to add.</param>
	Public Sub Add(item As JsonValue)
		If Me._value Is Nothing Then
			Throw New InvalidCastException("Dynamic type represents null.")
		End If

		Me._value.AsArray().Add(item)
	End Sub


	''' <summary>
	''' Adds the values of the specified collection to the end of the array.
	''' </summary>
	''' <param name="items">Collection of items to add.</param>
	Public Sub AddRange(items As IEnumerable(Of JsonValue))
		If Me._value Is Nothing Then
			Throw New InvalidCastException("Dynamic type represents null.")
		End If

		Me._value.AsArray().AddRange(items)
	End Sub


	''' <summary>
	''' Inserts an value into the array at the specified index.
	''' </summary>
	''' <exception cref="ArgumentOutOfRangeException">
	''' index is less than 0 or index is greater than Count.
	''' </exception> 
	Public Sub Insert(index As Integer, item As JsonValue)
		If Me._value Is Nothing Then
			Throw New InvalidCastException("Dynamic type represents null.")
		End If

		Me._value.AsArray().Insert(index, item)
	End Sub


	''' <summary>
	''' Inserts the values of a collection into the array at the specified index.
	''' </summary>
	''' <exception cref="ArgumentOutOfRangeException">
	''' index is less than 0 or index is greater than Count.
	''' </exception> 
	Public Sub InsertRange(index As Integer, items As IEnumerable(Of JsonValue))
		If Me._value Is Nothing Then
			Throw New InvalidCastException("Dynamic type represents null.")
		End If

		Me._value.AsArray().InsertRange(index, items)
	End Sub


	''' <summary>
	''' Gets value stored under given key index. If not such offset exists than defaultValue is returned.
	''' </summary>
	''' <param name="index">Index under which is value stored.</param>
	''' <param name="defaultValue">Default value returned in case no such offset exists.</param>
	''' <returns>Value stored under given offset as dynamic.</returns>
	''' <exception cref="InvalidCastException">If this dynamic object does not wrap JsonArray.</exception>
	Public Function GetOrDefault(index As Integer, Optional defaultValue As JsonValue = Nothing) As JsonDynamic
		Dim result As JsonValue = Nothing

		If Me._value Is Nothing Then
			Throw New InvalidCastException("Dynamic type represents null.")
		End If

		Dim array = Me._value.AsArray()
		If index >= array.Count Then
			Return defaultValue.ToDynamic()
		Else
			Return array(index).ToDynamic()
		End If
	End Function


	''' <summary>
	''' Gets value stored under given key index. If not such offset exists than defaultValue is returned.
	''' </summary>
	''' <param name="index">Index under which is value stored.</param>
	''' <param name="defaultValue">Default value returned in case no such offset exists.</param>
	''' <returns>Value stored under given offset as dynamic.</returns>
	''' <exception cref="InvalidCastException">If this dynamic object does not wrap JsonArray.</exception>
	Public Function GetOrDefault(index As Integer, defaultValue As JsonDynamic) As JsonDynamic
		Dim result As JsonValue = Nothing

		If Me._value Is Nothing Then
			Throw New InvalidCastException("Dynamic type represents null.")
		End If

		Dim array = Me._value.AsArray()
		If index >= array.Count Then
			Return defaultValue
		Else
			Return array(index).ToDynamic()
		End If
	End Function

#End Region

#Region "ToString(), GetHashCode(), Equals()"

	Public Overrides Function Equals(obj As Object) As Boolean
		If TypeOf obj Is JsonDynamic Then
			Dim other = CType(obj, JsonDynamic)
			Return Me.Equals(other)

		ElseIf TypeOf obj Is JsonValue Then
			Dim other = CType(obj, JsonValue)
			Return Me.Equals(other)

		ElseIf Me._value Is Nothing Then
			' me represents null 
			Return Object.Equals(obj, Nothing)

		Else
			Return False

		End If
	End Function


	Public Overloads Function Equals(other As JsonDynamic) As Boolean
		If Me._value IsNot Nothing Then
			Return Me._value.Equals(other._value)
		ElseIf other._value IsNot Nothing Then
			Return False
		Else
			Return True
		End If
	End Function


	Public Overloads Function Equals(other As JsonValue) As Boolean
		If Me._value IsNot Nothing Then
			Return Me._value.Equals(other)
		ElseIf other IsNot Nothing Then
			Return False
		Else
			Return True
		End If
	End Function


	Public Overrides Function GetHashCode() As Integer
		If Me._value Is Nothing Then
			Return 0
		Else
			Return Me._value.GetHashCode()
		End If
	End Function


	Public Overrides Function ToString() As String
		Return JsonParser.Encode(Me._value, JsonEncoderOptions.ToStringDefault)
	End Function

#End Region

#Region "Operators"

	Public Shared Operator =(former As JsonDynamic, latter As JsonDynamic) As Boolean
		Return former.Equals(latter)
	End Operator


	Public Shared Operator <>(former As JsonDynamic, latter As JsonDynamic) As Boolean
		Return Not former = latter
	End Operator


	Public Shared Narrowing Operator CType(value As JsonValue) As JsonDynamic
		If value Is Nothing Then
			Return Nothing
		End If

		Return New JsonDynamic(value)
	End Operator


	Public Shared Widening Operator CType(value As JsonDynamic) As JsonValue
		If value = Nothing Then
			Return Nothing
		End If

		Return value._value
	End Operator


	Public Shared Widening Operator CType(text As String) As JsonDynamic
		Return New JsonDynamic(text)
	End Operator


	Public Shared Widening Operator CType(value As Boolean) As JsonDynamic
		Return New JsonDynamic(value)
	End Operator


	Public Shared Widening Operator CType(value As Boolean?) As JsonDynamic
		Return New JsonDynamic(value)
	End Operator


	Public Shared Widening Operator CType(number As Integer) As JsonDynamic
		Return New JsonDynamic(number)
	End Operator


	Public Shared Widening Operator CType(number As Integer?) As JsonDynamic
		Return New JsonDynamic(number)
	End Operator


	Public Shared Widening Operator CType(number As Long) As JsonDynamic
		Return New JsonDynamic(number)
	End Operator


	Public Shared Widening Operator CType(number As Long?) As JsonDynamic
		Return New JsonDynamic(number)
	End Operator


	Public Shared Widening Operator CType(number As Single) As JsonDynamic
		Return New JsonDynamic(number)
	End Operator


	Public Shared Widening Operator CType(number As Single?) As JsonDynamic
		Return New JsonDynamic(number)
	End Operator


	Public Shared Widening Operator CType(number As Double) As JsonDynamic
		Return New JsonDynamic(number)
	End Operator


	Public Shared Widening Operator CType(number As Double?) As JsonDynamic
		Return New JsonDynamic(number)
	End Operator


	Public Shared Widening Operator CType(number As Decimal) As JsonDynamic
		Return New JsonDynamic(number)
	End Operator


	Public Shared Widening Operator CType(number As Decimal?) As JsonDynamic
		Return New JsonDynamic(number)
	End Operator

#End Region

End Structure
