Public Class LogListView
    Inherits System.Windows.Forms.ListBox

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
            _messageReadIndex = _messageReadIndex + 1
            message = Log.getMessage(_messageReadIndex)
            Me.Items.Add(message.Message)
            Me.TopIndex = Me.Items.Count - 1
        End While
    End Sub
#End Region
End Class
