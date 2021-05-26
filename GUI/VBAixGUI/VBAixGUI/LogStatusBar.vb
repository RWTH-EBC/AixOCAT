Public Class LogStatusBar
    Inherits System.Windows.Forms.ToolStripStatusLabel


#Region "Events"

#End Region

#Region "Properties"
    Dim _messageReadIndex As Integer = -1
#End Region

#Region "Property Functions"

#End Region

#Region "Subs"
    Public Sub StartLogging()
        TimerPoll.Stop()
        TimerPoll.Interval = 100
        TimerPoll.Start()
    End Sub

    Public Sub StopLogging()
        TimerPoll.Stop()
    End Sub
#End Region

#Region "Form Events"
    Private Sub TimerPoll_Tick(sender As Object, e As EventArgs) Handles TimerPoll.Tick
        Dim message As LogMessage
        While Log.hasNextMessage(_messageReadIndex)
            message = Log.getLastMessage()
            _messageReadIndex = Log.BufferSize - 1
            Me.Text = message.Message
            Select Case message.Type
                Case MessageType.Warning
                    Me.BackColor = System.Drawing.Color.FromArgb(255, 237, 0)
                Case MessageType.LightError
                    Me.BackColor = System.Drawing.Color.FromArgb(246, 168, 0)
                Case MessageType.MajorError
                    Me.BackColor = System.Drawing.Color.FromArgb(204, 7, 30)
                Case Else
                    Me.BackColor = Color.Transparent
            End Select
        End While
    End Sub
#End Region
End Class
