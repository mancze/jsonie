''' <summary>
''' Options for JSON encoder.
''' </summary>
Public Class JsonEncoderOptions

	''' <summary>
	''' Gets default encoder options.
	''' </summary>
	Public Shared ReadOnly [Default] As New JsonEncoderOptions()


	''' <summary>
	''' Gets default internal encoder options used in ToString() conversions.
	''' </summary>
	Friend Shared ReadOnly Property ToStringDefault As JsonEncoderOptions
		Get
			If _toStringDefault Is Nothing Then
				_toStringDefault = New JsonEncoderOptions()
				_toStringDefault.UsePrettyFormat = True
			End If

			Return _toStringDefault
		End Get
	End Property
	Private Shared _toStringDefault As JsonEncoderOptions


	''' <summary>
	''' Controls whether pretty JSON formatting will be used.
	''' </summary>
	Public UsePrettyFormat As Boolean

End Class
