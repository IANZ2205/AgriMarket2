Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.Data
Imports System.Collections.Concurrent

Public Class frmServerMain
    Private listener As TcpListener
    Private listenerThread As Thread
    Private isRunning As Boolean = False
    Private Shared ReadOnly ConnectedClients As New ConcurrentDictionary(Of Integer, TcpClient)()

    Private Sub frmServerMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DatabaseHelper.InitializeDatabase()
        DatabaseHelper.SeedAdminUser()
        DatabaseHelper.RunDailyMaintenance()
        StartServer()
    End Sub

    Private Sub StartServer()
        Try
            listener = New TcpListener(IPAddress.Any, 5000)
            listener.Start()

            isRunning = True
            lblStatus.Text = "Status: ONLINE - Listening on port 5000"
            lblStatus.ForeColor = Color.FromArgb(27, 122, 80)
            LogMessage("Server started. Listening on port 5000...")
            DatabaseHelper.WriteLog("Info", "Server", "Server started listening on port 5000.")

            listenerThread = New Thread(AddressOf ListenForClients)
            listenerThread.IsBackground = True
            listenerThread.Start()
        Catch ex As Exception
            LogMessage("Error starting server: " & ex.Message)
        End Try
    End Sub

    Private Sub ListenForClients()
        Try
            While isRunning
                Dim client As TcpClient = listener.AcceptTcpClient()
                Dim clientEndPoint As IPEndPoint = CType(client.Client.RemoteEndPoint, IPEndPoint)
                LogMessage("New client connected from: " & clientEndPoint.Address.ToString())

                Dim clientThread As New Thread(Sub() HandleClient(client, clientEndPoint.Address.ToString()))
                clientThread.IsBackground = True
                clientThread.Start()
            End While
        Catch ex As Exception
            If isRunning Then LogMessage("Listener Error: " & ex.Message)
        End Try
    End Sub

    Private Sub HandleClient(client As TcpClient, ipAddress As String)
        Dim stream As NetworkStream = client.GetStream()
        Dim buffer(8192) As Byte
        Dim sb As New StringBuilder()

        Try
            While client.Connected
                Dim bytesRead As Integer = stream.Read(buffer, 0, buffer.Length)
                If bytesRead = 0 Then Exit While

                sb.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead))

                If sb.ToString().Contains(ControlChars.CrLf) Then
                    Dim fullData As String = sb.ToString()
                    Dim messages As String() = fullData.Split(New String() {ControlChars.CrLf}, StringSplitOptions.RemoveEmptyEntries)

                    For Each msg As String In messages
                        ProcessMessage(msg, client)
                    Next

                    sb.Clear()
                End If
            End While
        Catch ex As Exception
            LogMessage("Client " & ipAddress & " disconnected unexpectedly.")
        Finally
            client.Close()
            LogMessage("Client " & ipAddress & " disconnected.")
            For Each kvp In ConnectedClients
                If kvp.Value Is client Then
                    Dim removed As TcpClient = Nothing
                    ConnectedClients.TryRemove(kvp.Key, removed)
                    Exit For
                End If
            Next
        End Try
    End Sub

    Private Sub ProcessMessage(msg As String, client As TcpClient)
        LogMessage("Received: " & msg)

        Dim parts As String() = msg.Split("|"c)
        Dim command As String = parts(0)
        Dim reqId As String = parts(1)

        Try
            Select Case command
                Case "HELLO"
                    SendResponse(client, $"OK|{reqId}|{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}|1.0" & ControlChars.CrLf)

                Case "LOGIN"
                    Dim userRow As DataRow = DatabaseHelper.AuthenticateUserRaw(parts(2), parts(3))
                    If userRow IsNot Nothing Then
                        DatabaseHelper.WriteLog("Info", "Login", $"User {userRow("Email")} logged in.")
                        SendResponse(client, $"OK|{reqId}|{userRow("UserID")}|{userRow("Role")}|{userRow("DisplayName")}|{userRow("Status")}" & ControlChars.CrLf)
                        Dim userId As Integer = Convert.ToInt32(userRow("UserID"))
                        ConnectedClients(userId) = client
                    Else
                        DatabaseHelper.WriteLog("Warning", "Login", $"Failed login attempt for {parts(2)}.")
                        SendResponse(client, $"ERR|{reqId}|BAD_CREDENTIALS|Invalid email or password." & ControlChars.CrLf)
                    End If

                Case "REGISTER"
                    ' REGISTER|ReqID|Email|Password|Role|Name|Base64ProfilePic
                    Dim base64Pic As String = If(parts.Length > 6, parts(6), "")
                    If DatabaseHelper.RegisterUser(parts(2), parts(3), parts(4), parts(5), base64Pic) Then
                        DatabaseHelper.WriteLog("Info", "Register", $"New {parts(4)} registered: {parts(2)}.")
                        SendResponse(client, $"OK|{reqId}|Registered" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|DUPLICATE|Email already in use." & ControlChars.CrLf)
                    End If

                Case "GET_STOCK"
                    Dim dt As DataTable = DatabaseHelper.GetTraderStock(Convert.ToInt32(parts(2)))
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        sb.Append($"{row("BatchID")}~{row("ItemID")}~{row("Produce Name")}~{row("Category")}~{row("Price (UGX)")}~{row("Batch Number")}~{row("Available Qty")}~{row("Expiry Date")}~{row("Batch Status")}~{row("ImagePath")}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "ADD_ITEM"
                    ' ADD_ITEM|ReqID|TraderId|Name|Category|Price|Qty|Shelf|Img1|Img2|Img3
                    Dim img1 As String = If(parts.Length > 8, parts(8), "")
                    Dim img2 As String = If(parts.Length > 9, parts(9), "")
                    Dim img3 As String = If(parts.Length > 10, parts(10), "")
                    If DatabaseHelper.AddItemWithBatch(Convert.ToInt32(parts(2)), parts(3), parts(4), Convert.ToDecimal(parts(5)), Convert.ToInt32(parts(6)), Convert.ToInt32(parts(7)), img1, img2, img3) Then
                        DatabaseHelper.WriteLog("Info", "Stock", $"New item '{parts(3)}' added by TraderID {parts(2)}.")
                        SendResponse(client, $"OK|{reqId}|Added" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|FAILED|Could not add item" & ControlChars.CrLf)
                    End If

                Case "GET_ALL_STOCK"
                    Dim dt As DataTable = DatabaseHelper.GetAllAvailableStock()
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        sb.Append($"{row("ItemID")}~{row("Produce")}~{row("Category")}~{row("Price (UGX)")}~{row("Available Qty")}~{row("Expiry Date")}~{row("Trader Name")}~{row("ImagePath")}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "SEARCH_STOCK"
                    Dim searchTerm As String = parts(2)
                    Dim dt As DataTable = DatabaseHelper.SearchStock(searchTerm)
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        sb.Append($"{row("ItemID")}~{row("Produce")}~{row("Category")}~{row("Price (UGX)")}~{row("Available Qty")}~{row("Expiry Date")}~{row("Trader Name")}~{row("ImagePath")}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "GET_USER_ID"
                    Dim uid As Integer = DatabaseHelper.GetUserIdByName(parts(2))
                    SendResponse(client, $"OK|{reqId}|{uid}" & ControlChars.CrLf)

                Case "GET_ITEM_DETAIL"
                    Dim itemId As Integer = Convert.ToInt32(parts(2))
                    Dim dt As DataTable = DatabaseHelper.GetItemDetailById(itemId)
                    If dt.Rows.Count > 0 Then
                        Dim row As DataRow = dt.Rows(0)
                        SendResponse(client, $"OK|{reqId}|{row("ItemID")}~{row("Produce")}~{row("Category")}~{row("Price")}~{row("Qty")}~{row("Expiry")}~{row("Description")}~{row("TraderID")}~{row("TraderName")}~{row("TraderPhone")}~{row("TraderDistrict")}~{row("ImagePath")}" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|NOT_FOUND|Item not found" & ControlChars.CrLf)
                    End If

                Case "GET_TRADER_DETAILS"
                    Dim dt As DataTable = DatabaseHelper.GetTraderDetails(Convert.ToInt32(parts(2)))
                    If dt.Rows.Count > 0 Then
                        Dim row As DataRow = dt.Rows(0)
                        SendResponse(client, $"OK|{reqId}|{row("DisplayName")}~{row("Phone")}~{row("District")}~{row("CompletedOrders")}" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|NOT_FOUND|Trader not found" & ControlChars.CrLf)
                    End If

                Case "PLACE_ORDER"
                    If DatabaseHelper.PlaceOrder(Convert.ToInt32(parts(2)), Convert.ToInt32(parts(3)), Convert.ToInt32(parts(4)), Convert.ToInt32(parts(5)), Convert.ToDecimal(parts(6))) Then
                        DatabaseHelper.WriteLog("Info", "Order", $"Order placed for ItemID {parts(2)} by BuyerID {parts(4)}.")
                        SendResponse(client, $"OK|{reqId}|Placed" & ControlChars.CrLf)
                        Dim traderId As Integer = Convert.ToInt32(parts(3))
                        PushNotification(traderId, "NEW_ORDER", "You have received a new order!")
                    Else
                        DatabaseHelper.WriteLog("Error", "Order", $"Failed to place order for ItemID {parts(2)}.")
                        SendResponse(client, $"ERR|{reqId}|FAILED|Could not place order" & ControlChars.CrLf)
                    End If

                Case "GET_TRADER_ORDERS"
                    Dim dt As DataTable = DatabaseHelper.GetTraderOrders(Convert.ToInt32(parts(2)))
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        sb.Append($"{row("OrderID")}~{row("Produce")}~{row("Buyer")}~{row("Qty")}~{row("Total (UGX)")}~{row("Status")}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "UPDATE_ORDER_STATUS"
                    Dim orderId As Integer = Convert.ToInt32(parts(2))
                    Dim newStatus As String = parts(3)
                    Dim userId As Integer = Convert.ToInt32(parts(4))
                    Dim comment As String = If(parts.Length > 5, parts(5), "")

                    If DatabaseHelper.UpdateOrderStatus(orderId, newStatus, userId, comment) Then
                        SendResponse(client, $"OK|{reqId}|Updated" & ControlChars.CrLf)

                        Dim buyerId As Integer = DatabaseHelper.GetBuyerIdForOrder(orderId)
                        Dim traderId As Integer = DatabaseHelper.GetTraderIdForOrder(orderId)

                        If newStatus = "Confirmed" Then
                            If buyerId > 0 Then PushNotification(buyerId, "ORDER_CONFIRMED", $"Your order #{orderId} has been confirmed.")
                        ElseIf newStatus = "Rejected" Then
                            If buyerId > 0 Then PushNotification(buyerId, "ORDER_REJECTED", $"Your order #{orderId} was rejected. Reason: {comment}")
                        ElseIf newStatus = "Completed" Then
                            If buyerId > 0 Then PushNotification(buyerId, "ORDER_COMPLETED", $"Your order #{orderId} has been completed.")
                        ElseIf newStatus = "Cancelled" Then
                            If userId = buyerId Then
                                If traderId > 0 Then PushNotification(traderId, "ORDER_CANCELLED", $"Order #{orderId} was cancelled by the buyer. Reason: {comment}")
                            Else
                                If buyerId > 0 Then PushNotification(buyerId, "ORDER_CANCELLED", $"Order #{orderId} was cancelled by the trader. Reason: {comment}")
                            End If
                        End If
                    Else
                        SendResponse(client, $"ERR|{reqId}|FAILED|Could not update status" & ControlChars.CrLf)
                    End If

                Case "GET_BUYER_ORDERS"
                    Dim dt As DataTable = DatabaseHelper.GetBuyerOrders(Convert.ToInt32(parts(2)))
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        sb.Append($"{row("OrderID")}~{row("Produce")}~{row("Trader")}~{row("Qty")}~{row("Total (UGX)")}~{row("Status")}~{row("Date")}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "GET_DASHBOARD_STATS"
                    Dim dt As DataTable = DatabaseHelper.GetTraderDashboardStats(Convert.ToInt32(parts(2)))
                    If dt.Rows.Count > 0 Then
                        Dim row As DataRow = dt.Rows(0)
                        SendResponse(client, $"OK|{reqId}|{row("ItemsInStock")}~{row("PendingOrders")}~{row("CompletedOrders")}~{row("TotalSales")}" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"OK|{reqId}|0~0~0~0" & ControlChars.CrLf)
                    End If

                Case "GET_ADMIN_STATS"
                    Dim dt As DataTable = DatabaseHelper.GetAdminDashboardStats()
                    If dt.Rows.Count > 0 Then
                        Dim row As DataRow = dt.Rows(0)
                        SendResponse(client, $"OK|{reqId}|{row("TotalUsers")}~{row("TotalTraders")}~{row("TotalBuyers")}~{row("TotalOrders")}" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"OK|{reqId}|0~0~0~0" & ControlChars.CrLf)
                    End If

                Case "GET_USERS"
                    Dim dt As DataTable = DatabaseHelper.GetAllUsers()
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        sb.Append($"{row("UserID")}~{row("Name")}~{row("Email")}~{row("Role")}~{row("Status")}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "APPROVE_USER"
                    If DatabaseHelper.ApproveUser(Convert.ToInt32(parts(2))) Then
                        DatabaseHelper.WriteLog("Info", "Approval", $"Admin approved UserID {parts(2)}.")
                        SendResponse(client, $"OK|{reqId}|Approved" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|FAILED|Could not approve user" & ControlChars.CrLf)
                    End If

                Case "START_CHAT"
                    Dim u1 As Integer = Convert.ToInt32(parts(2))
                    Dim u2 As Integer = Convert.ToInt32(parts(3))
                    Dim convId As Integer = DatabaseHelper.GetOrCreateConversation(u1, u2)
                    SendResponse(client, $"OK|{reqId}|{convId}" & ControlChars.CrLf)

                Case "GET_CHAT_HISTORY"
                    Dim convId As Integer = Convert.ToInt32(parts(2))
                    Dim dt As DataTable = DatabaseHelper.GetChatHistory(convId)
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        Dim safeBody As String = row("Body").ToString().Replace(ControlChars.CrLf, " ")
                        sb.Append($"{row("MessageID")}~{row("SenderID")}~{row("SenderName")}~{safeBody}~{row("SentAt")}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "SEND_MESSAGE"
                    Dim convId As Integer = Convert.ToInt32(parts(2))
                    Dim senderId As Integer = Convert.ToInt32(parts(3))
                    Dim receiverId As Integer = Convert.ToInt32(parts(4))
                    Dim body As String = parts(5)

                    If DatabaseHelper.SendMessage(convId, senderId, body) Then
                        SendResponse(client, $"OK|{reqId}|Sent" & ControlChars.CrLf)

                        If ConnectedClients.ContainsKey(receiverId) Then
                            Dim targetClient As TcpClient = ConnectedClients(receiverId)
                            Dim senderName As String = DatabaseHelper.GetUserNameById(senderId)
                            Dim pushMsg As String = $"PUSH_CHAT|0|{convId}|{senderName}|{body}" & ControlChars.CrLf
                            SendResponse(targetClient, pushMsg)
                        End If
                    Else
                        SendResponse(client, $"ERR|{reqId}|FAILED|Could not send message" & ControlChars.CrLf)
                    End If

                Case "GET_CONVERSATIONS"
                    Dim dt As DataTable = DatabaseHelper.GetUserConversations(Convert.ToInt32(parts(2)))
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        sb.Append($"{row("ConvID")}~{row("OtherUserID")}~{row("OtherUserName")}~{row("OtherUserRole")}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "GET_NOTIFICATIONS"
                    Dim dt As DataTable = DatabaseHelper.GetNotifications(Convert.ToInt32(parts(2)))
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        sb.Append($"{row("NotifID")}~{row("NotifType")}~{row("Title")}~{row("Body")}~{row("IsRead")}~{row("CreatedAt")}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "MARK_NOTIFS_READ"
                    DatabaseHelper.MarkNotificationsAsRead(Convert.ToInt32(parts(2)))
                    SendResponse(client, $"OK|{reqId}|Marked" & ControlChars.CrLf)

                Case "GET_BATCHES_TO_WATCH"
                    Dim dt As DataTable = DatabaseHelper.GetTraderBatchesToWatch(Convert.ToInt32(parts(2)))
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        sb.Append($"{row("Batch")}~{row("Produce")}~{row("Expiry")}~{row("Qty")}~{row("Status")}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "GET_PROFILE"
                    Dim dt As DataTable = DatabaseHelper.GetUserProfile(Convert.ToInt32(parts(2)))
                    If dt.Rows.Count > 0 Then
                        Dim row As DataRow = dt.Rows(0)
                        SendResponse(client, $"OK|{reqId}|{row("DisplayName")}~{row("Email")}~{row("Phone")}~{row("District")}" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|NOT_FOUND|User not found" & ControlChars.CrLf)
                    End If

                Case "UPDATE_PROFILE"
                    If DatabaseHelper.UpdateUserProfile(Convert.ToInt32(parts(2)), parts(3), parts(4), parts(5)) Then
                        SendResponse(client, $"OK|{reqId}|Updated" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|FAILED|Could not update profile" & ControlChars.CrLf)
                    End If

                Case "CHANGE_PASSWORD"
                    If DatabaseHelper.ChangeUserPassword(Convert.ToInt32(parts(2)), parts(3)) Then
                        SendResponse(client, $"OK|{reqId}|Updated" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|FAILED|Could not change password" & ControlChars.CrLf)
                    End If

                Case "TOPUP_ITEM"
                    Dim batchNo As String = DatabaseHelper.TopUpItemBatch(Convert.ToInt32(parts(2)), Convert.ToInt32(parts(3)), Convert.ToInt32(parts(4)))
                    If batchNo IsNot Nothing Then
                        DatabaseHelper.WriteLog("Info", "Stock", $"Item {parts(2)} topped up. New batch: {batchNo}.")
                        SendResponse(client, $"OK|{reqId}|{batchNo}" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|FAILED|Could not top up item" & ControlChars.CrLf)
                    End If

                Case "UPDATE_ITEM"
                    If DatabaseHelper.UpdateItemDetails(Convert.ToInt32(parts(2)), parts(3), Convert.ToDecimal(parts(4)), parts(5)) Then
                        SendResponse(client, $"OK|{reqId}|Updated" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|FAILED|Could not update item" & ControlChars.CrLf)
                    End If

                Case "ADJUST_BATCH"
                    If DatabaseHelper.AdjustBatchQuantity(Convert.ToInt32(parts(2)), Convert.ToInt32(parts(3)), parts(4)) Then
                        DatabaseHelper.WriteLog("Warning", "Stock", $"Batch {parts(2)} adjusted to {parts(3)}. Reason: {parts(4)}.")
                        SendResponse(client, $"OK|{reqId}|Adjusted" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|FAILED|Could not adjust batch" & ControlChars.CrLf)
                    End If

                Case Else
                    SendResponse(client, $"ERR|{reqId}|UNKNOWN_COMMAND|Command not recognized" & ControlChars.CrLf)
            End Select
        Catch ex As Exception
            LogMessage("Error processing message: " & ex.Message)
            SendResponse(client, $"ERR|{reqId}|SERVER_ERROR|Internal server error" & ControlChars.CrLf)
        End Try
    End Sub

    Private Sub SendResponse(client As TcpClient, message As String)
        Try
            Dim stream As NetworkStream = client.GetStream()
            Dim data As Byte() = Encoding.UTF8.GetBytes(message)
            stream.Write(data, 0, data.Length)
            LogMessage("Sent: " & message.Trim())
        Catch ex As Exception
            LogMessage("Error sending response: " & ex.Message)
        End Try
    End Sub

    Private Sub PushNotification(userId As Integer, notifType As String, message As String)
        DatabaseHelper.AddNotification(userId, notifType, "AgriMarket Alert", message)

        If ConnectedClients.ContainsKey(userId) Then
            Dim targetClient As TcpClient = ConnectedClients(userId)
            Dim pushMsg As String = $"PUSH_NOTIF|0|{notifType}|{message}" & ControlChars.CrLf
            SendResponse(targetClient, pushMsg)
        End If
    End Sub

    Private Sub LogMessage(msg As String)
        If txtLog.InvokeRequired Then
            txtLog.Invoke(New Action(Sub() LogMessage(msg)))
        Else
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}" & Environment.NewLine)
            txtLog.ScrollToCaret()
        End If
    End Sub

    Private Sub frmServerMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        isRunning = False
        If listener IsNot Nothing Then listener.Stop()
    End Sub

    Private Sub btnAdminLogin_Click(sender As Object, e As EventArgs) Handles btnAdminLogin.Click
        Dim loginForm As New frmAdminLogin()
        loginForm.Show()
    End Sub
End Class