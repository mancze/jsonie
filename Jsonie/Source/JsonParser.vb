Imports System.IO


''' <summary>
''' Static helper class for easy JSON encoding/decoding.
''' </summary>
Public Class JsonParser

	Private Sub New()
		Throw New InvalidOperationException("Static-only class.")
	End Sub

#Region "Decode"

	''' <summary>
	''' Deserializes JSON encoded string into object representation.
	''' </summary>
	''' <param name="jsonString">String which should be parsed.</param>
	''' <param name="options">Decoder options.</param>
	''' <returns>Decoded JSON value.</returns>
	''' <exception cref="JsonFormatException">When json string is invalid.</exception>
	Public Shared Function Decode(jsonString As String, Optional options As JsonDecoderOptions = Nothing) As JsonValue
		Using reader = New StringReader(jsonString)
			Return Decode(reader, options)
		End Using
	End Function


	''' <summary>
	''' Deserializes JSON encoded string into object representation.
	''' </summary>
	''' <param name="stream">Input stream containing json string.</param>
	''' <param name="options">Decoder options.</param>
	''' <returns>Decoded JSON value.</returns>
	''' <exception cref="JsonFormatException">When json string is invalid.</exception>
	Public Shared Function Decode(stream As Stream, Optional options As JsonDecoderOptions = Nothing) As JsonValue
		Using reader = New StreamReader(stream)
			Return Decode(reader, options)
		End Using
	End Function


	''' <summary>
	''' Deserializes JSON encoded string into object representation.
	''' </summary>
	''' <param name="reader">Reader of the json string.</param>
	''' <param name="options">Decoder options.</param>
	''' <returns>Decoded JSON value.</returns>
	''' <exception cref="JsonFormatException">When json string is invalid.</exception>
	Public Shared Function Decode(reader As TextReader, Optional options As JsonDecoderOptions = Nothing) As JsonValue
		Dim decoder = New JsonDecoder(options)
		Return decoder.Decode(reader)
	End Function

#End Region

#Region "Encode"

	''' <summary>
	''' Serializes JSON value into JSON string.
	''' </summary>
	''' <param name="value">Value which should be serialized.</param>
	''' <param name="options">Encoder options.</param>
	''' <returns>JSON encoded value.</returns>
	Public Shared Function Encode(value As JsonValue, Optional options As JsonEncoderOptions = Nothing) As String
		Using stream = New MemoryStream()
			Dim utf8WithoutBom = New System.Text.UTF8Encoding(False)
			Using writer = New StreamWriter(stream, utf8WithoutBom)
				Encode(value, writer, options)

				' seek to the end
				writer.Flush()
				stream.Seek(0, SeekOrigin.Begin)

				Using reader = New StreamReader(stream)
					Return reader.ReadToEnd()
				End Using
			End Using
		End Using
	End Function


	''' <summary>
	''' Serializes JSON value into JSON string.
	''' </summary>
	''' <param name="value">Value which should be serialized.</param>
	''' <param name="stream">Output stream where JSON text will be outputted.</param>
	''' <param name="options">Encoder options.</param>
	Public Shared Sub Encode(value As JsonValue, stream As Stream, Optional options As JsonEncoderOptions = Nothing)
		Dim utf8WithoutBom = New System.Text.UTF8Encoding(False)
		Using writer = New StreamWriter(stream, utf8WithoutBom)
			Encode(value, writer, options)
		End Using
	End Sub


	''' <summary>
	''' Serializes JSON value into JSON string.
	''' </summary>
	''' <param name="value">Value which should be serialized.</param>
	''' <param name="writer">Text writer to which JSON will be written.</param>
	''' <param name="options">Encoder options.</param>
	Public Shared Sub Encode(value As JsonValue, writer As TextWriter, Optional options As JsonEncoderOptions = Nothing)
		Dim encoder = New JsonEncoder(options)
		encoder.Encode(value, writer)
	End Sub

#End Region

End Class
