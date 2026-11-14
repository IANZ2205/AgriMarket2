Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Collections.Concurrent

Public Class NetworkClient
    Public Shared Client As TcpClient
    Private Shared ReadOnly ServerIP As String = "127.0.0.1"
    Private Shared ReadOnly ServerPort As Integer = 5000
    Private Shared _reqIdCounter As Integer = 0
    Private Shared ReadOnly _pendingRequests As New ConcurrentDictionary(Of Integer, TaskCompletionSource(Of String))

    ' Events for pushes
    Public Shared Event NotificationReceived(message As String)
    Public Shared Event ChatMessageReceived(convId As Integer, senderName As String, message As String)

    Public Shared Function Connect() As Boolean
        Try
            Client = New TcpClient()
            Client.Connect(ServerIP, ServerPort)

            Dim receiveThread As New Thread(AddressOf ListenForServer)
            receiveThread.IsBackground = True
            receiveThread.Start()

            Return True
        Catch ex As Exception
            MessageBox.Show("Could not connect to server. Make sure the Server app is running." & Environment.NewLine & ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Shared Async Function SendRequestAsync(command As String) As Task(Of String)
        If Client Is Nothing OrElse Not Client.Connected Then
            Return "ERR|0|NOT_CONNECTED|Client is not connected."
        End If

        Dim reqId As Integer = Interlocked.Increment(_reqIdCounter)
        Dim tcs As New TaskCompletionSource(Of String)()
        _pendingRequests.TryAdd(reqId, tcs)

        Dim fullMessage As String = $"{command.Split("|"c)(0)}|{reqId}|{command.Substring(command.IndexOf("|"c) + 1)}"

        Dim stream As NetworkStream = Client.GetStream()
        Dim data As Byte() = Encoding.UTF8.GetBytes(fullMessage & ControlChars.CrLf)
        Await stream.WriteAsync(data, 0, data.Length)

        Dim timeoutTask = Task.Delay(30000)
        Dim completedTask = Await Task.WhenAny(tcs.Task, timeoutTask)

        If completedTask Is timeoutTask Then
            _pendingRequests.TryRemove(reqId, Nothing)
            Return "ERR|0|TIMEOUT|Server did not respond in time."
        Else
            Return Await tcs.Task
        End If
    End Function

    Private Shared Sub ListenForServer()
        Dim stream As NetworkStream = Client.GetStream()
        Dim buffer(1024) As Byte
        Dim sb As New StringBuilder()

        Try
            While Client.Connected
                Dim bytesRead As Integer = stream.Read(buffer, 0, buffer.Length)
                If bytesRead = 0 Then Exit While

                sb.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead))

                If sb.ToString().Contains(ControlChars.CrLf) Then
                    Dim fullData As String = sb.ToString()
                    Dim messages As String() = fullData.Split(New String() {ControlChars.CrLf}, StringSplitOptions.RemoveEmptyEntries)

                    For Each msg As String In messages
                        Dim parts As String() = msg.Split("|"c)

                        If parts.Length >= 2 Then
                            Dim reqId As Integer
                            If Integer.TryParse(parts(1), reqId) Then
                                If reqId = 0 Then
                                    ' This is a PUSH from the server
                                    If parts(0) = "PUSH_NOTIF" Then
                                        Dim nMsg As String = parts(3)
                                        RaiseEvent NotificationReceived(nMsg)
                                    ElseIf parts(0) = "PUSH_CHAT" Then
                                        ' Format: PUSH_CHAT|0|ConvID|SenderName|Body
                                        Dim cId As Integer = Convert.ToInt32(parts(2))
                                        Dim sName As String = parts(3)
                                        Dim body As String = parts(4)
                                        RaiseEvent ChatMessageReceived(cId, sName, body)
                                    End If
                                Else
                                    ' It's a normal response
                                    Dim tcs As TaskCompletionSource(Of String) = Nothing
                                    If _pendingRequests.TryRemove(reqId, tcs) Then
                                        tcs.SetResult(msg)
                                    End If
                                End If
                            End If
                        End If
                    Next

                    sb.Clear()
                End If
            End While
        Catch ex As Exception
            ' Connection lost
        End Try
    End Sub
End Class