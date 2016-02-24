''' <summary>
''' Dynamic wrapper around any <see cref="JsonValue" />.
''' </summary>
<DebuggerDisplay("JsonDynamic: Value = {Value}")>
Public Structure JsonDynamic

	''' <summary>
	''' Tests if this is JSON null.
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
	''' Tests if this is JSON string or number or boolean or null.
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
	''' <exception cref="InvalidCastException">Underlying JSON value is not object or array.</exception>
	Public ReadOnly Property Count As Integer
		Get
			Me.ThrowIfValueIsNull()

			If Me._value.IsObject Then
				Return Me._value.AsObject().Count
			ElseIf Me._value.IsArray Then
				Return Me._value.AsArray().Count
			End If

			Throw New InvalidCastException("Json value is not object nor array.")
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


	''' <summary>
	''' Gets the memeber indexed by given <paramref name="key" />.
	''' </summary>
	''' <param name="key">Key of the member to return.</param>
	''' <value>Member stored under given <paramref name="key" />.</value>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is Nothing.</exception>
	''' <exception cref="KeyNotFoundException">The property is retrieved and <paramref name="key" /> is not found.</exception>
	''' <exception cref="InvalidCastException">If underlying JSON value is not object.</exception>
	Default Public Property Item(key As String) As JsonDynamic
		Get
			Me.ThrowIfValueIsNull()
			Return Me._value.AsObject()(key).ToDynamic()
		End Get
		Set(value As JsonDynamic)
			Me.ThrowIfValueIsNull()
			Me._value.AsObject()(key) = value.Value
		End Set
	End Property


	''' <summary>
	''' Gets the item stored on given <paramref name="index" />.
	''' </summary>
	''' <param name="index">Index of the item to return.</param>
	''' <value>Item stored under given <paramref name="key" />.</value>
	''' <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is less than 0 or is equal or more than <see cref="Count" />.</exception>
	''' <exception cref="InvalidCastException">If underlying JSON value is not array.</exception>
	Default Public Property Item(index As Integer) As JsonDynamic
		Get
			Me.ThrowIfValueIsNull()
			Return Me._value.AsArray()(index).ToDynamic()
		End Get
		Set(value As JsonDynamic)
			Me.ThrowIfValueIsNull()
			Me._value.AsArray()(index) = value.Value
		End Set
	End Property


	''' <summary>
	''' Creates new dynamic wrapper around specified <see cref="JsonValue" />.
	''' </summary>
	''' <param name="value">The value to wrap as dynamic type. Value can be null.</param>
	Public Sub New(value As JsonValue)
		Me._value = value
	End Sub

#Region "Object Methods"

	''' <summary>
	''' Adds an member with the specified key to this object.
	''' </summary>
	''' <param name="key">The key of the element to add.</param>
	''' <param name="value">The value of the element to add. The value can be null.</param>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="ArgumentException">An property with the same <paramref name="key" /> already exists.</exception>
	''' <exception cref="InvalidCastException">If this object does not wrap <see cref="JsonObject" />.</exception>
	Public Sub Add(key As String, value As JsonValue)
		Me.ThrowIfValueIsNull()
		Me._value.AsObject().Add(key, value)
	End Sub


	''' <summary>
	''' Adds an empty array property and returns it's instance.
	''' </summary>
	''' <param name="key">The key of the array to add.</param>
	''' <returns>The added array as dynamic.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="ArgumentException">An property with the same <paramref name="key" /> already exists.</exception>
	''' <exception cref="InvalidCastException">If this object does not wrap <see cref="JsonObject" />.</exception>
	Public Function AddArray(key As String) As JsonDynamic
		Me.ThrowIfValueIsNull()
		Return Me._value.AsObject().AddArray(key).ToDynamic()
	End Function


	''' <summary>
	''' Adds an empty object property and returns its instance.
	''' </summary>
	''' <param name="key">The key of the object to add.</param>
	''' <returns>The added objec tas dynamic.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="ArgumentException">An property with the same <paramref name="key" /> already exists.</exception>
	''' <exception cref="InvalidCastException">If this object does not wrap <see cref="JsonObject" />.</exception>
	Public Function AddObject(key As String) As JsonDynamic
		Return Me._value.AsObject().AddObject(key).ToDynamic()
	End Function


	''' <summary>
	''' Gets value stored under given key and returns it as dynamic type. If not such member exists than defaultValue 
	''' is returned.
	''' </summary>
	''' <param name="key">The key under which is member stored.</param>
	''' <param name="defaultValue">Default value returned in case no such member exists.</param>
	''' <returns>Value stored under given key as dynamic.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="InvalidCastException">If this object does not wrap <see cref="JsonObject" />.</exception>
	Public Function GetOrDefault(key As String, Optional defaultValue As JsonValue = Nothing) As JsonDynamic
		Me.ThrowIfValueIsNull()
		Return Me._value.AsObject().GetOrDefault(key, defaultValue).ToDynamic()
	End Function


	''' <summary>
	''' Gets value stored under given key and casts it to dynamic type. If not such member exists than defaultValue 
	''' is returned.
	''' </summary>
	''' <param name="key">The key under which is member stored.</param>
	''' <param name="defaultValue">Default value returned in case no such member exists.</param>
	''' <returns>Value stored under given key as dynamic.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is Nothing.</exception>
	''' <exception cref="InvalidCastException">If this object does not wrap <see cref="JsonObject" />.</exception>
	Public Function GetOrDefault(key As String, defaultValue As JsonDynamic) As JsonDynamic
		Me.ThrowIfValueIsNull()
		Return Me.GetOrDefault(key, defaultValue.Value)
	End Function


	''' <summary>
	''' Gets the member stored under given key. If key does not exist provided value is stored as new member.
	''' </summary>
	''' <param name="key">The key under which is array stored.</param>
	''' <param name="addValue">The value to add if specified member does not exist.</param>
	''' <returns>Value stored under given key casted to dynamic.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is Nothing.</exception>
	''' <exception cref="InvalidCastException">If this object does not wrap <see cref="JsonObject" />.</exception>
	Public Function GetOrAdd(key As String, addValue As JsonValue) As JsonDynamic
		Me.ThrowIfValueIsNull()
		Return Me._value.AsObject().GetOrAdd(key, addValue).ToDynamic()
	End Function


	''' <summary>
	''' Gets the member stored under given key. If key does not exist provided value is stored as new member.
	''' </summary>
	''' <param name="key">The key under which is array stored.</param>
	''' <param name="addValue">The value to add if specified member does not exist.</param>
	''' <returns>Value stored under given key casted to dynamic.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is Nothing.</exception>
	''' <exception cref="InvalidCastException">If this object does not wrap <see cref="JsonObject" />.</exception>
	Public Function GetOrAdd(key As String, addValue As JsonDynamic) As JsonDynamic
		Me.ThrowIfValueIsNull()
		Return Me.GetOrAdd(key, addValue.Value)
	End Function


	''' <summary>
	''' Tests whether the object contains a member with the specified key. It does not check the value of the member 
	''' which might be null.
	''' </summary>
	''' <param name="key">The key of the member to find.</param>
	''' <returns>True if member with specified key member is defined in the object.</returns>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="InvalidCastException">If this object does not wrap <see cref="JsonObject" />.</exception>
	Public Function ContainsKey(key As String) As Boolean
		Me.ThrowIfValueIsNull()
		Return Me._value.AsObject().ContainsKey(key)
	End Function


	''' <summary>
	''' Tests whether the object contains a member with the specified key and which is not null.
	''' </summary>
	''' <param name="key">The key of the member to find.</param>
	''' <returns>true if member with specified key exists and is not null.</returns>
	''' <remarks>Equivalent to Me.ContainsKey(key) AndAlso Me(key) IsNot Nothing.</remarks>
	''' <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
	''' <exception cref="InvalidCastException">If this object does not wrap <see cref="JsonObject" />.</exception>
	Public Function ContainsKeyNotNull(key As String) As Boolean
		Me.ThrowIfValueIsNull()
		Return Me._value.AsObject().ContainsKeyNotNull(key)
	End Function

#End Region

#Region "Array Methods"

	''' <summary>
	''' Adds an items to the end of the array.
	''' </summary>
	''' <param name="item">Value to add.</param>
	''' <exception cref="InvalidCastException">If this object does not wrap <see cref="JsonArray" />.</exception>
	Public Sub Add(item As JsonValue)
		Me.ThrowIfValueIsNull()
		Me._value.AsArray().Add(item)
	End Sub


	''' <summary>
	''' Adds an empty array to the end of the array and returns its instance.
	''' </summary>
	''' <returns>The added array as dynamic.</returns>
	''' <exception cref="InvalidCastException">If this object does not wrap <see cref="JsonArray" />.</exception>
	Public Function AddArray() As JsonDynamic
		Me.ThrowIfValueIsNull()
		Return Me._value.AsArray().AddArray().ToDynamic()
	End Function


	''' <summary>
	''' Adds an empty object and returns its instance.
	''' </summary>
	''' <returns>The added object as dynamic.</returns>
	''' <exception cref="InvalidCastException">If this object does not wrap <see cref="JsonArray" />.</exception>
	Public Function AddObject() As JsonDynamic
		Me.ThrowIfValueIsNull()
		Return Me._value.AsArray().AddObject().ToDynamic()
	End Function


	''' <summary>
	''' Adds the values of the specified collection to the end of the array.
	''' </summary>
	''' <param name="items">Collection of items to add.</param>
	''' <exception cref="InvalidCastException">If this object does not wrap <see cref="JsonArray" />.</exception>
	Public Sub AddRange(items As IEnumerable(Of JsonValue))
		Me.ThrowIfValueIsNull()
		Me._value.AsArray().AddRange(items)
	End Sub


	''' <summary>
	''' Inserts an value into the array at the specified index.
	''' </summary>
	''' <param name="index">The zero-based index at which item should be inserted.</param>
	''' <param name="item">The object to insert. The value can be null.</param>
	''' <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is less than 0 or is equal or more than <see cref="Count" />.</exception>
	''' <exception cref="InvalidCastException">If this object does not wrap <see cref="JsonArray" />.</exception>
	Public Sub Insert(index As Integer, item As JsonValue)
		Me.ThrowIfValueIsNull()
		Me._value.AsArray().Insert(index, item)
	End Sub


	''' <summary>
	''' Inserts an empty array at the specified index and returns its instance.
	''' </summary>
	''' <param name="index">The zero-based index at which item should be inserted.</param>
	''' <returns>The inserted array as dynamic.</returns>
	''' <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is less than 0 or is equal or more than <see cref="Count" />.</exception>
	''' <exception cref="InvalidCastException">If this object does not wrap <see cref="JsonArray" />.</exception>
	Public Function InsertArray(index As Integer) As JsonDynamic
		Me.ThrowIfValueIsNull()
		Return Me._value.AsArray().InsertArray(index).ToDynamic()
	End Function


	''' <summary>
	''' Adds an empty object at the specified index and returns its instance.
	''' </summary>
	''' <param name="index">The zero-based index at which item should be inserted.</param>
	''' <returns>The inserted object as dynamic.</returns>
	''' <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is less than 0 or is equal or more than <see cref="Count" />.</exception>
	''' <exception cref="InvalidCastException">If this object does not wrap <see cref="JsonArray" />.</exception>
	Public Function InsertObject(index As Integer) As JsonDynamic
		Me.ThrowIfValueIsNull()
		Return Me._value.AsArray().InsertObject(index).ToDynamic()
	End Function


	''' <summary>
	''' Inserts the values of a collection into the array at the specified index.
	''' </summary>
	''' <param name="index">The zero-based index at which items should be inserted.</param>
	''' <param name="items">The collection of items which will be inserted to the specified position in the array. The collection can contains items whose value is null.</param>
	''' <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is less than 0 or is equal or more than <see cref="Count" />.</exception>
	''' <exception cref="ArgumentNullException"><paramref name="items" /> is null.</exception>
	''' <exception cref="InvalidCastException">If this object does not wrap <see cref="JsonArray" />.</exception>
	Public Sub InsertRange(index As Integer, items As IEnumerable(Of JsonValue))
		Me.ThrowIfValueIsNull()
		Me._value.AsArray().InsertRange(index, items)
	End Sub

#End Region

#Region "GetHashCode(), Equals()"

	''' <summary>
	''' Determines whether current object is equal to another object.
	''' </summary>
	''' <param name="obj">The object to compare with the current object.</param>
	''' <returns>True if the current object is equal to this, false otherwise.</returns>
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


	''' <summary>
	''' Determines whether current object is equal to another object.
	''' </summary>
	''' <param name="other">The object to compare with the current object.</param>
	''' <returns>True if the current object is equal to this, false otherwise.</returns>
	Public Overloads Function Equals(other As JsonDynamic) As Boolean
		If Me._value IsNot Nothing Then
			Return Me._value.Equals(other._value)
		ElseIf other._value IsNot Nothing Then
			Return False
		Else
			Return True
		End If
	End Function


	''' <summary>
	''' Determines whether current object is equal to another object.
	''' </summary>
	''' <param name="other">The object to compare with the current object.</param>
	''' <returns>True if the current object is equal to this, false otherwise.</returns>
	Public Overloads Function Equals(other As JsonValue) As Boolean
		If Me._value IsNot Nothing Then
			Return Me._value.Equals(other)
		ElseIf other IsNot Nothing Then
			Return False
		Else
			Return True
		End If
	End Function


	''' <summary>
	''' Serves as a hash function for a particular type.
	''' </summary>
	''' <returns>A hash code for the current <see cref="T:System.Object"/>.</returns>
	Public Overrides Function GetHashCode() As Integer
		If Me._value Is Nothing Then
			Return 0
		Else
			Return Me._value.GetHashCode()
		End If
	End Function

#End Region

#Region "Operators"

	''' <summary>
	''' Test whether values of its operands are equal.
	''' </summary>
	''' <param name="former">First value to compare.</param>
	''' <param name="latter">Second value to compare.</param>
	''' <returns>True if <paramref name="former" /> is equal to <paramref name="latter" />.</returns>
	Public Shared Operator =(former As JsonDynamic, latter As JsonDynamic) As Boolean
		Return former.Equals(latter)
	End Operator


	''' <summary>
	''' Test whether values of its operands are inequal.
	''' </summary>
	''' <param name="former">First value to compare.</param>
	''' <param name="latter">Second value to compare.</param>
	''' <returns>True if <paramref name="former" /> is inequal to <paramref name="latter" />.</returns>
	Public Shared Operator <>(former As JsonDynamic, latter As JsonDynamic) As Boolean
		Return Not former = latter
	End Operator


	''' <summary>
	''' Performs an explicit conversion from <see cref="JsonValue" /> to <see cref="JsonDynamic" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonDynamic" /> casted from the specified value.</returns>
	Public Shared Narrowing Operator CType(value As JsonValue) As JsonDynamic
		Return New JsonDynamic(value)
	End Operator


	''' <summary>
	''' Performs an explicit conversion from <see cref="JsonDynamic" /> to <see cref="JsonValue" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonValue" /> casted from the specified value.</returns>
	Public Shared Narrowing Operator CType(value As JsonDynamic) As JsonValue
		Return value._value
	End Operator


	''' <summary>
	''' Performs an implicit conversion from <see cref="String" /> to <see cref="JsonDynamic" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonDynamic" /> casted from the specified value.</returns>
	Public Shared Widening Operator CType(value As String) As JsonDynamic
		Return New JsonDynamic(value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from <see cref="Boolean" /> to <see cref="JsonDynamic" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonDynamic" /> casted from the specified value.</returns>
	Public Shared Widening Operator CType(value As Boolean) As JsonDynamic
		Return New JsonDynamic(value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from nullable <see cref="Boolean" /> to <see cref="JsonDynamic" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonDynamic" /> casted from the specified value.</returns>
	Public Shared Widening Operator CType(value As Boolean?) As JsonDynamic
		Return New JsonDynamic(value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from <see cref="Integer" /> to <see cref="JsonDynamic" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonDynamic" /> casted from the specified value.</returns>
	Public Shared Widening Operator CType(value As Integer) As JsonDynamic
		Return New JsonDynamic(value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from <see cref="Integer" /> to <see cref="JsonDynamic" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonDynamic" /> casted from the specified value.</returns>
	Public Shared Widening Operator CType(value As Integer?) As JsonDynamic
		Return New JsonDynamic(value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from <see cref="Long" /> to <see cref="JsonDynamic" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonDynamic" /> casted from the specified value.</returns>
	Public Shared Widening Operator CType(value As Long) As JsonDynamic
		Return New JsonDynamic(value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from nullable <see cref="Long" /> to <see cref="JsonDynamic" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonDynamic" /> casted from the specified value.</returns>
	Public Shared Widening Operator CType(value As Long?) As JsonDynamic
		Return New JsonDynamic(value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from <see cref="Single" /> to <see cref="JsonDynamic" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonDynamic" /> casted from the specified value.</returns>
	Public Shared Widening Operator CType(value As Single) As JsonDynamic
		Return New JsonDynamic(value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from nullable <see cref="Single" /> to <see cref="JsonDynamic" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonDynamic" /> casted from the specified value.</returns>
	Public Shared Widening Operator CType(value As Single?) As JsonDynamic
		Return New JsonDynamic(value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from <see cref="Double" /> to <see cref="JsonDynamic" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonDynamic" /> casted from the specified value.</returns>
	Public Shared Widening Operator CType(value As Double) As JsonDynamic
		Return New JsonDynamic(value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from nullable <see cref="Double" /> to <see cref="JsonDynamic" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonDynamic" /> casted from the specified value.</returns>
	Public Shared Widening Operator CType(value As Double?) As JsonDynamic
		Return New JsonDynamic(value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from <see cref="Decimal" /> to <see cref="JsonDynamic" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonDynamic" /> casted from the specified value.</returns>
	Public Shared Widening Operator CType(value As Decimal) As JsonDynamic
		Return New JsonDynamic(value)
	End Operator


	''' <summary>
	''' Performs an implicit conversion from nullable <see cref="Decimal" /> to <see cref="JsonDynamic" />.
	''' </summary>
	''' <param name="value">The value to convert.</param>
	''' <returns>The <see cref="JsonDynamic" /> casted from the specified value.</returns>
	Public Shared Widening Operator CType(value As Decimal?) As JsonDynamic
		Return New JsonDynamic(value)
	End Operator

#End Region

	''' <summary>
	''' Gets to JSON encoded string representing this object.
	''' </summary>
	''' <returns>JSON string</returns>
	Public Function ToJson() As String
		Return JsonParser.Encode(Me._value, JsonEncoderOptions.ToStringDefault)
	End Function


	Private Sub ThrowIfValueIsNull()
		If Me._value Is Nothing Then
			Throw New InvalidCastException("Cannot cast dynamic null to requested type.")
		End If
	End Sub

End Structure
