
Public Enum MessageType
    Information = 1
    Warning = 2
    LightError = 3
    MajorError = 4
End Enum

Module Log

#Region "Events"

#End Region

#Region "Properties"
    Dim MessageBuffer As List(Of LogMessage) = New List(Of LogMessage)
#End Region

#Region "Property Functions"
    Public ReadOnly Property BufferSize()
        Get
            BufferSize = MessageBuffer.Count
        End Get
    End Property
#End Region

#Region "Subs"
    Public Sub AddMessage(ByVal _type As MessageType, ByVal _message As String)
        Dim newMessage As LogMessage = New LogMessage("[" & Now.ToShortDateString & " " & Now.ToLongTimeString & "." & Now.Millisecond.ToString("D3") & "] " & _message, _type)
        MessageBuffer.Add(newMessage)
    End Sub

    Public Function hasNextMessage(ByVal _readIndex As Integer) As Boolean
        Return (_readIndex < BufferSize() - 1)
    End Function

    Public Function getMessage(ByVal _readIndex As Integer) As LogMessage
        If hasNextMessage(_readIndex - 1) Then
            Return MessageBuffer(_readIndex)
        End If
        Return Nothing
    End Function

    Public Function getLastMessage() As LogMessage
        Return getMessage(BufferSize() - 1)
    End Function
#End Region

End Module

Public Class LogMessage
    Public ReadOnly Property Message As String
    Public ReadOnly Property Type As MessageType

    Sub New()

    End Sub

    Sub New(ByVal _message As String, ByVal _type As MessageType)
        Me.Message = _message
        Me.Type = _type
    End Sub
End Class
