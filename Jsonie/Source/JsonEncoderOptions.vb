''' <summary>
''' Options for JSON encoder.
''' </summary>
Public Class JsonEncoderOptions

	''' <summary>
	''' Gets default encoder options.
	''' </summary>
	Public Shared ReadOnly [Default] As New JsonEncoderOptions()


	''' <summary>
	''' Controls whether pretty JSON formatting will be used.
	''' </summary>
	Public UsePrettyFormat As Boolean

End Class
