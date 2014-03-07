Friend NotInheritable Class ThrowHelper

	Private Sub New()
	End Sub


	Friend Shared Sub ThrowNullDynamicCasted()
		Throw New InvalidCastException("Dynamic type represents null.")
	End Sub

End Class
