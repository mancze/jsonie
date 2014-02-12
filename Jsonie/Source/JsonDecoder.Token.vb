Partial Class JsonDecoder

	Private Enum Token
		Invalid
		[Null]
		[False]
		[True]
		Number
		[String]
		Comma
		Colon
		ObjectBegin
		ObjectEnd
		ArrayBegin
		ArrayEnd
	End Enum

End Class
