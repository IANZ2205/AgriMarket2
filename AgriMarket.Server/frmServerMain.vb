Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.Data
Imports System.Collections.Concurrent
Imports System.Diagnostics

' ========== SERVER VERSION: SECURITY QUESTIONS ==========
Public Class frmServerMain
    Private listener As TcpListener
    Private listenerThread As Thread
    Private isRunning As Boolean = False
    Public Shared ReadOnly ConnectedClients As New ConcurrentDictionary(Of Integer, TcpClient)()
    Public Shared ReadOnly ClientSessions As New ConcurrentDictionary(Of Integer, ClientSession)()
    Private perfTimer As System.Windows.Forms.Timer
    Private _perfTickCount As Integer = 0

    Private Sub frmServerMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DatabaseHelper.InitializeDatabase()
        DatabaseHelper.SeedAdminUser()
        DatabaseHelper.RunDailyMaintenance()
        PerformanceSampler.Initialize()
        StartServer()
        StartPerfTimer()
    End Sub

    Private Sub StartPerfTimer()
        perfTimer = New System.Windows.Forms.Timer()
        perfTimer.Interval = 2000
        AddHandler perfTimer.Tick, AddressOf PerfTimer_Tick
        perfTimer.Start()
    End Sub

    Private Sub PerfTimer_Tick(sender As Object, e As EventArgs)
        Try
            PerformanceSampler.TakeSample()
            _perfTickCount += 1
            If _perfTickCount >= 30 Then
                _perfTickCount = 0
                Try
                    DatabaseHelper.SavePerfSample(
                        ConnectedClients.Count,
                        PerformanceSampler.CurrentReqPerSec,
                        0, 0,
                        PerformanceSampler.CurrentAvgRespMs,
                        PerformanceSampler.CurrentCpu,
                        PerformanceSampler.CurrentMemoryMB)
                Catch
                End Try
            End If
        Catch
        End Try
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
                    Dim sess As ClientSession = Nothing
                    If ClientSessions.TryRemove(kvp.Key, sess) Then
                        If sess IsNot Nothing AndAlso sess.SessionID > 0 Then
                            Try
                                DatabaseHelper.EndSession(sess.SessionID, "Disconnected")
                            Catch
                            End Try
                        End If
                    End If
                    Exit For
                End If
            Next
        End Try
    End Sub

    Private Sub ProcessMessage(msg As String, client As TcpClient)
        LogMessage("Received: " & TruncateForLog(msg))
        PerformanceSampler.RecordRequest()
        Dim sw As Stopwatch = Stopwatch.StartNew()

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
                        Dim mustChange As String = "0"
                        Try
                            If userRow("MustChangePassword") IsNot DBNull.Value AndAlso Convert.ToInt32(userRow("MustChangePassword")) = 1 Then
                                mustChange = "1"
                            End If
                        Catch
                        End Try
                        SendResponse(client, $"OK|{reqId}|{userRow("UserID")}|{userRow("Role")}|{userRow("DisplayName")}|{userRow("Status")}|{mustChange}" & ControlChars.CrLf)
                        Dim userId As Integer = Convert.ToInt32(userRow("UserID"))
                        ConnectedClients(userId) = client
                        Dim ip As String = ""
                        Try
                            ip = client.Client.RemoteEndPoint.ToString()
                        Catch
                        End Try
                        Dim newSess As New ClientSession(ip, DateTime.Now)
                        Try
                            newSess.SessionID = DatabaseHelper.CreateSession(userId, "", ip)
                        Catch
                        End Try
                        ClientSessions(userId) = newSess
                    Else
                        DatabaseHelper.WriteLog("Warning", "Login", $"Failed login attempt for {parts(2)}.")
                        SendResponse(client, $"ERR|{reqId}|BAD_CREDENTIALS|Invalid email or password." & ControlChars.CrLf)
                    End If


                Case "COMPLETE_PASSWORD_RESET"
                    Dim userId As Integer = Convert.ToInt32(parts(2))
                    Dim newPass As String = parts(3)
                    If DatabaseHelper.SetNewPasswordAndClearFlag(userId, newPass) Then
                        DatabaseHelper.WriteLog("Info", "Password", $"UserID {userId} completed password reset.")
                        SendResponse(client, $"OK|{reqId}|Updated" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|FAILED|Could not set new password" & ControlChars.CrLf)
                    End If

                Case "REGISTER"
                    ' Old: REGISTER|ReqID|Email|Password|Role|Name|Base64Pic (6 params)
                    ' New: REGISTER|ReqID|Email|Password|Role|Name|Base64Pic|QuestionID|Answer (8 params)
                    Dim base64Pic As String = If(parts.Length > 6, parts(6), "")
                    Dim qid As Integer = 0
                    Dim answer As String = ""
                    If parts.Length > 8 Then
                        Try
                            qid = Convert.ToInt32(parts(7))
                        Catch
                        End Try
                        answer = parts(8)
                    End If
                    If DatabaseHelper.RegisterUser(parts(2), parts(3), parts(4), parts(5), base64Pic, qid, answer) Then
                        DatabaseHelper.WriteLog("Info", "Register", $"New {parts(4)} registered: {parts(2)}.")
                        SendResponse(client, $"OK|{reqId}|Registered" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|DUPLICATE|Email already in use." & ControlChars.CrLf)
                    End If

                Case "GET_SECURITY_QUESTIONS"
                    Dim dt As DataTable = DatabaseHelper.GetSecurityQuestionList()
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        sb.Append($"{row("ID")}~{SanitizeField(row("Text").ToString())}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "GET_QUESTION"
                    Dim email As String = parts(2)
                    Dim dt As DataTable = DatabaseHelper.GetSecurityQuestionByEmail(email)
                    If dt.Rows.Count = 0 Then
                        SendResponse(client, $"ERR|{reqId}|NOT_FOUND|No account with that email." & ControlChars.CrLf)
                    Else
                        Dim qText As String = dt.Rows(0)("QuestionText").ToString()
                        If String.IsNullOrEmpty(qText) Then
                            SendResponse(client, $"ERR|{reqId}|NO_QUESTION|This account has no security question set." & ControlChars.CrLf)
                        Else
                            SendResponse(client, $"OK|{reqId}|{dt.Rows(0)("QuestionID")}~{SanitizeField(qText)}" & ControlChars.CrLf)
                        End If
                    End If

                Case "RESET_PASSWORD_WITH_ANSWER"
                    Dim email As String = parts(2)
                    Dim answer As String = parts(3)
                    Dim newPass As String = parts(4)
                    Dim result As String = DatabaseHelper.ResetPasswordWithAnswer(email, answer, newPass)
                    Select Case result
                        Case "OK"
                            DatabaseHelper.WriteLog("Info", "Password", $"User {email} reset password via security question.")
                            SendResponse(client, $"OK|{reqId}|Password reset successfully." & ControlChars.CrLf)
                        Case "NOT_FOUND"
                            SendResponse(client, $"ERR|{reqId}|NOT_FOUND|No account with that email." & ControlChars.CrLf)
                        Case "NO_QUESTION"
                            SendResponse(client, $"ERR|{reqId}|NO_QUESTION|This account has no security question set." & ControlChars.CrLf)
                        Case "BAD_ANSWER"
                            SendResponse(client, $"ERR|{reqId}|BAD_ANSWER|The answer is incorrect." & ControlChars.CrLf)
                        Case Else
                            SendResponse(client, $"ERR|{reqId}|SERVER_ERROR|An internal error occurred." & ControlChars.CrLf)
                    End Select

                Case "GET_STOCK"
                    Dim dt As DataTable = DatabaseHelper.GetTraderStock(Convert.ToInt32(parts(2)))
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        sb.Append($"{row("BatchID")}~{row("ItemID")}~{row("Produce Name")}~{row("Category")}~{row("Price (UGX)")}~{row("Batch Number")}~{row("Available Qty")}~{row("Expiry Date")}~{row("Batch Status")}~{row("ImagePath")}~{row("Unit")}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "ADD_ITEM"
                    ' Three supported formats:
                    '   NEW (15 parts): ADD_ITEM|ReqID|TraderId|Name|Category|Price|Unit|LowStock|Description|Qty|Shelf|Img1|Img2|Img3|LocalNamesBlob
                    '   MID (12 parts): ADD_ITEM|ReqID|TraderId|Name|Category|Price|Unit|Qty|Shelf|Img1|Img2|Img3
                    '   OLD (11 parts): ADD_ITEM|ReqID|TraderId|Name|Category|Price|Qty|Shelf|Img1|Img2|Img3
                    If parts.Length >= 15 Then
                        ' NEW format — full fields
                        Dim lowStock As Integer = 10
                        Try
                            lowStock = Convert.ToInt32(parts(7))
                        Catch
                        End Try
                        Dim description As String = parts(8)
                        Dim qty As Integer = Convert.ToInt32(parts(9))
                        Dim shelf As Integer = Convert.ToInt32(parts(10))
                        Dim img1 As String = If(parts.Length > 11, parts(11), "")
                        Dim img2 As String = If(parts.Length > 12, parts(12), "")
                        Dim img3 As String = If(parts.Length > 13, parts(13), "")
                        Dim blob As String = If(parts.Length > 14, parts(14), "")

                        Dim localNames As New Dictionary(Of Integer, String)()
                        If Not String.IsNullOrEmpty(blob) Then
                            For Each pair As String In blob.Split("^"c)
                                If pair.Trim() <> "" Then
                                    Dim kv As String() = pair.Split("~"c)
                                    If kv.Length >= 2 Then
                                        Dim langId As Integer
                                        If Integer.TryParse(kv(0), langId) Then
                                            localNames(langId) = kv(1)
                                        End If
                                    End If
                                End If
                            Next
                        End If

                        If DatabaseHelper.AddItemWithBatchWithUnit(Convert.ToInt32(parts(2)), parts(3), parts(4), Convert.ToDecimal(parts(5)), parts(6), qty, shelf, img1, img2, img3, lowStock, description, localNames) Then
                            DatabaseHelper.WriteLog("Info", "Stock", $"New item '{parts(3)}' added by TraderID {parts(2)}.")
                            SendResponse(client, $"OK|{reqId}|Added" & ControlChars.CrLf)
                        Else
                            SendResponse(client, $"ERR|{reqId}|FAILED|Could not add item" & ControlChars.CrLf)
                        End If
                    ElseIf parts.Length >= 12 Then
                        ' MID format — with unit, no description/local names
                        Dim img1 As String = If(parts.Length > 9, parts(9), "")
                        Dim img2 As String = If(parts.Length > 10, parts(10), "")
                        Dim img3 As String = If(parts.Length > 11, parts(11), "")
                        If DatabaseHelper.AddItemWithBatchWithUnit(Convert.ToInt32(parts(2)), parts(3), parts(4), Convert.ToDecimal(parts(5)), parts(6), Convert.ToInt32(parts(7)), Convert.ToInt32(parts(8)), img1, img2, img3) Then
                            DatabaseHelper.WriteLog("Info", "Stock", $"New item '{parts(3)}' added by TraderID {parts(2)}.")
                            SendResponse(client, $"OK|{reqId}|Added" & ControlChars.CrLf)
                        Else
                            SendResponse(client, $"ERR|{reqId}|FAILED|Could not add item" & ControlChars.CrLf)
                        End If
                    Else
                        ' OLD format — defaults to kg
                        Dim img1 As String = If(parts.Length > 8, parts(8), "")
                        Dim img2 As String = If(parts.Length > 9, parts(9), "")
                        Dim img3 As String = If(parts.Length > 10, parts(10), "")
                        If DatabaseHelper.AddItemWithBatch(Convert.ToInt32(parts(2)), parts(3), parts(4), Convert.ToDecimal(parts(5)), Convert.ToInt32(parts(6)), Convert.ToInt32(parts(7)), img1, img2, img3) Then
                            DatabaseHelper.WriteLog("Info", "Stock", $"New item '{parts(3)}' added by TraderID {parts(2)}.")
                            SendResponse(client, $"OK|{reqId}|Added" & ControlChars.CrLf)
                        Else
                            SendResponse(client, $"ERR|{reqId}|FAILED|Could not add item" & ControlChars.CrLf)
                        End If
                    End If

                Case "GET_ALL_STOCK"
                    Dim dt As DataTable = DatabaseHelper.GetAllAvailableStock()
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        sb.Append($"{row("ItemID")}~{row("Produce")}~{row("Category")}~{row("Price (UGX)")}~{row("Available Qty")}~{row("Expiry Date")}~{row("Trader Name")}~{row("ImagePath")}~{row("Unit")}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "SEARCH_STOCK"
                    Dim searchTerm As String = parts(2)
                    Dim dt As DataTable = DatabaseHelper.SearchStock(searchTerm)
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        sb.Append($"{row("ItemID")}~{row("Produce")}~{row("Category")}~{row("Price (UGX)")}~{row("Available Qty")}~{row("Expiry Date")}~{row("Trader Name")}~{row("ImagePath")}~{row("Unit")}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "GET_LANGUAGES"
                    Dim dt As DataTable = DatabaseHelper.GetLanguages()
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        sb.Append($"{row("ID")}~{SanitizeField(row("Name").ToString())}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "GET_CATEGORY_UNITS"
                    Dim dt As DataTable = DatabaseHelper.GetAllCategoryUnitLinks()
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        sb.Append($"{SanitizeField(row("CategoryName").ToString())}~{SanitizeField(row("UnitName").ToString())}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "GET_CATEGORIES"
                    Dim dt As DataTable = DatabaseHelper.GetMasterList("Categories")
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        sb.Append($"{row("Name")}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "GET_UNITS"
                    Dim dt As DataTable = DatabaseHelper.GetMasterList("Units")
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        sb.Append($"{row("Name")}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "GET_DISTRICTS"
                    Dim dt As DataTable = DatabaseHelper.GetMasterList("Districts")
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        sb.Append($"{row("Name")}^")
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
                        SendResponse(client, $"OK|{reqId}|{row("ItemID")}~{row("Produce")}~{row("Category")}~{row("Price")}~{row("Qty")}~{row("Expiry")}~{row("Description")}~{row("TraderID")}~{row("TraderName")}~{row("TraderPhone")}~{row("TraderDistrict")}~{row("ImagePath")}~{row("Unit")}" & ControlChars.CrLf)
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
                    ' Old format: PLACE_ORDER|ReqID|ItemId|TraderId|BuyerId|Qty|Price
                    ' New format: PLACE_ORDER|ReqID|ItemId|TraderId|BuyerId|Qty|Price|NeededBy|DeliveryMode|DeliveryNote|BuyerNote
                    Dim neededBy As String = If(parts.Length > 7, parts(7), "")
                    Dim deliveryMode As String = If(parts.Length > 8, parts(8), "Pickup")
                    Dim deliveryNote As String = If(parts.Length > 9, SanitizeField(parts(9)), "")
                    Dim buyerNote As String = If(parts.Length > 10, SanitizeField(parts(10)), "")

                    If DatabaseHelper.PlaceOrder(Convert.ToInt32(parts(2)), Convert.ToInt32(parts(3)), Convert.ToInt32(parts(4)), Convert.ToInt32(parts(5)), Convert.ToDecimal(parts(6)), neededBy, deliveryMode, deliveryNote, buyerNote) Then
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
                    Dim dt As DataTable = DatabaseHelper.GetChatHistoryFull(convId)
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        Dim msgType As String = row("MsgType").ToString()
                        Dim body As String = SanitizeField(row("Body").ToString())
                        Dim fileName As String = row("FileName").ToString()
                        Dim origName As String = SanitizeField(row("OriginalName").ToString())
                        Dim fileSize As String = row("FileSize").ToString()
                        Dim locText As String = SanitizeField(row("LocationText").ToString())
                        Dim sentAt As String = row("SentAt").ToString()
                        sb.Append($"{row("MessageID")}~{row("SenderID")}~{row("SenderName")}~{msgType}~{body}~{fileName}~{origName}~{fileSize}~{locText}~{sentAt}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "SEND_MESSAGE"
                    Dim convId As Integer = Convert.ToInt32(parts(2))
                    Dim senderId As Integer = Convert.ToInt32(parts(3))
                    Dim receiverId As Integer = Convert.ToInt32(parts(4))

                    Dim success As Boolean = False
                    Dim msgType As String = "Text"
                    Dim bodyPreview As String = ""
                    If DatabaseHelper.IsBlockedByEither(senderId, receiverId) Then
                        SendResponse(client, $"ERR|{reqId}|BLOCKED|You cannot message this user. One of you has blocked the other." & ControlChars.CrLf)
                        Exit Try
                    End If

                    If parts.Length >= 11 Then
                        msgType = parts(5)
                        Dim body As String = parts(6)
                        Dim origName As String = parts(7)
                        Dim fileSize As Integer = 0
                        Try
                            fileSize = Convert.ToInt32(parts(8))
                        Catch
                        End Try
                        Dim locText As String = parts(9)
                        Dim base64 As String = parts(10)

                        success = DatabaseHelper.SendMessageFull(convId, senderId, msgType, body, origName, fileSize, locText, base64)
                        bodyPreview = body
                        If String.IsNullOrEmpty(bodyPreview) Then
                            Select Case msgType
                                Case "Image"
                                    bodyPreview = "[Photo]"
                                Case "File"
                                    bodyPreview = "[File] " & origName
                                Case "Location"
                                    bodyPreview = "[Location] " & locText
                            End Select
                        End If
                    Else
                        Dim body As String = parts(5)
                        success = DatabaseHelper.SendMessage(convId, senderId, body)
                        bodyPreview = body
                    End If

                    If success Then
                        SendResponse(client, $"OK|{reqId}|Sent" & ControlChars.CrLf)

                        If ConnectedClients.ContainsKey(receiverId) Then
                            Dim targetClient As TcpClient = ConnectedClients(receiverId)
                            Dim senderName As String = DatabaseHelper.GetUserNameById(senderId)
                            Dim safePreview As String = SanitizeField(bodyPreview)
                            Dim pushMsg As String = $"PUSH_CHAT|0|{convId}|{SanitizeField(senderName)}|{safePreview}" & ControlChars.CrLf
                            SendResponse(targetClient, pushMsg)
                        End If
                    Else
                        SendResponse(client, $"ERR|{reqId}|FAILED|Could not send message" & ControlChars.CrLf)
                    End If

                Case "GET_FILE"
                    Dim purpose As String = If(parts.Length > 2, parts(2), "chat")
                    Dim storedName As String = If(parts.Length > 3, parts(3), "")
                    Dim base64 As String = DatabaseHelper.ReadFileAsBase64(purpose, storedName)
                    If Not String.IsNullOrEmpty(base64) Then
                        SendResponse(client, $"OK|{reqId}|{base64}" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|NOT_FOUND|File not found" & ControlChars.CrLf)
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
                        SendResponse(client, $"OK|{reqId}|{row("DisplayName")}~{row("Email")}~{row("Phone")}~{row("District")}~{row("ProfilePic")}" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|NOT_FOUND|User not found" & ControlChars.CrLf)
                    End If

                Case "UPDATE_PROFILE"
                    Dim userId As Integer = Convert.ToInt32(parts(2))
                    Dim name As String = parts(3)
                    Dim phone As String = parts(4)
                    Dim district As String = parts(5)
                    Dim newPicBase64 As String = If(parts.Length > 6, parts(6), "")

                    Dim success As Boolean = DatabaseHelper.UpdateUserProfile(userId, name, phone, district)

                    If success AndAlso Not String.IsNullOrEmpty(newPicBase64) Then
                        Try
                            Dim newPicPath As String = DatabaseHelper.SaveFileToFolder("profile.jpg", newPicBase64, "profiles", ".jpg")
                            If Not String.IsNullOrEmpty(newPicPath) Then
                                DatabaseHelper.UpdateUserProfilePic(userId, newPicPath)
                            End If
                        Catch
                        End Try
                    End If

                    If success Then
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

                Case "CHAT_BLOCK"
                    Dim blockerId As Integer = Convert.ToInt32(parts(2))
                    Dim blockedId As Integer = Convert.ToInt32(parts(3))
                    If DatabaseHelper.BlockUser(blockerId, blockedId) Then
                        SendResponse(client, $"OK|{reqId}|Blocked" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|FAILED|Could not block user" & ControlChars.CrLf)
                    End If

                Case "IS_BLOCKED"
                    Dim userA As Integer = Convert.ToInt32(parts(2))
                    Dim userB As Integer = Convert.ToInt32(parts(3))
                    Dim state As String = "NONE"
                    If DatabaseHelper.HasUserBlocked(userA, userB) Then
                        state = "I_BLOCKED"
                    ElseIf DatabaseHelper.HasUserBlocked(userB, userA) Then
                        state = "BLOCKED"
                    End If
                    SendResponse(client, $"OK|{reqId}|{state}" & ControlChars.CrLf)


                Case "CHAT_UNBLOCK"
                    Dim blockerId As Integer = Convert.ToInt32(parts(2))
                    Dim blockedId As Integer = Convert.ToInt32(parts(3))
                    If DatabaseHelper.UnblockUser(blockerId, blockedId) Then
                        SendResponse(client, $"OK|{reqId}|Unblocked" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|FAILED|Could not unblock user" & ControlChars.CrLf)
                    End If

                Case "CHAT_REPORT"
                    Dim convId As Integer = Convert.ToInt32(parts(2))
                    Dim reportedBy As Integer = Convert.ToInt32(parts(3))
                    Dim reportedUserId As Integer = Convert.ToInt32(parts(4))
                    Dim reason As String = parts(5)
                    Dim reportId As Integer = DatabaseHelper.ReportChat(convId, reportedBy, reportedUserId, reason)
                    If reportId > 0 Then
                        DatabaseHelper.WriteLog("Warning", "ChatReport", $"UserID {reportedBy} reported UserID {reportedUserId} in ConvID {convId}. Reason: {reason}")
                        SendResponse(client, $"OK|{reqId}|{reportId}" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|FAILED|Could not submit report" & ControlChars.CrLf)
                    End If

                Case "GET_CHAT_REPORTS"
                    Dim dt As DataTable = DatabaseHelper.GetChatReports()
                    Dim sb As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        Dim reason As String = SanitizeField(row("Reason").ToString())
                        Dim rname As String = SanitizeField(row("ReporterName").ToString())
                        Dim tname As String = SanitizeField(row("ReportedName").ToString())
                        sb.Append($"{row("ReportID")}~{row("ConvID")}~{rname}~{tname}~{row("ReportedUserID")}~{reason}~{row("Status")}~{row("CreatedAt")}^")
                    Next
                    SendResponse(client, $"OK|{reqId}|{sb.ToString()}" & ControlChars.CrLf)

                Case "UPDATE_REPORT_STATUS"
                    Dim reportId As Integer = Convert.ToInt32(parts(2))
                    Dim newStatus As String = parts(3)
                    Dim reviewedBy As Integer = Convert.ToInt32(parts(4))
                    Dim note As String = If(parts.Length > 5, parts(5), "")
                    If DatabaseHelper.UpdateReportStatus(reportId, newStatus, reviewedBy, note) Then
                        SendResponse(client, $"OK|{reqId}|Updated" & ControlChars.CrLf)
                    Else
                        SendResponse(client, $"ERR|{reqId}|FAILED|Could not update report" & ControlChars.CrLf)
                    End If


                Case Else
                    SendResponse(client, $"ERR|{reqId}|UNKNOWN_COMMAND|Command not recognized" & ControlChars.CrLf)
            End Select
        Catch ex As Exception
            PerformanceSampler.RecordError()
            LogMessage("Error processing message: " & ex.Message)
            SendResponse(client, $"ERR|{reqId}|SERVER_ERROR|{ex.Message}" & ControlChars.CrLf)
        Finally
            sw.Stop()
            PerformanceSampler.RecordResponse(sw.ElapsedMilliseconds)
        End Try
    End Sub

    Private Function TruncateForLog(msg As String, Optional maxLen As Integer = 200) As String
        If String.IsNullOrEmpty(msg) Then Return msg
        If msg.Length <= maxLen Then Return msg
        Return msg.Substring(0, maxLen) & "...[truncated, total " & msg.Length & " chars]"
    End Function

    Private Function SanitizeField(s As String) As String
        If String.IsNullOrEmpty(s) Then Return ""
        Return s.Replace("|"c, " ").Replace("~"c, " ").Replace("^"c, " ").Replace(ControlChars.CrLf, " ")
    End Function

    Private Sub SendResponse(client As TcpClient, message As String)
        Try
            Dim stream As NetworkStream = client.GetStream()
            Dim data As Byte() = Encoding.UTF8.GetBytes(message)
            stream.Write(data, 0, data.Length)
            LogMessage("Sent: " & TruncateForLog(message))
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

    Public Shared Sub ForceDisconnect(userId As Integer)
        If ConnectedClients.ContainsKey(userId) Then
            Dim c As TcpClient = Nothing
            If ConnectedClients.TryRemove(userId, c) Then
                Try
                    Dim stream As NetworkStream = c.GetStream()
                    Dim msg As String = "PUSH_FORCE_LOGOUT|0|Disconnected by administrator." & ControlChars.CrLf
                    Dim data As Byte() = Encoding.UTF8.GetBytes(msg)
                    stream.Write(data, 0, data.Length)
                Catch
                End Try
                Try
                    c.Close()
                Catch
                End Try
            End If
        End If
        Dim s As ClientSession = Nothing
        If ClientSessions.TryRemove(userId, s) Then
            If s IsNot Nothing AndAlso s.SessionID > 0 Then
                Try
                    DatabaseHelper.EndSession(s.SessionID, "ForcedByAdmin")
                Catch
                End Try
            End If
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
        If perfTimer IsNot Nothing Then perfTimer.Stop()
        If listener IsNot Nothing Then listener.Stop()
    End Sub

    Private Sub btnAdminLogin_Click(sender As Object, e As EventArgs) Handles btnAdminLogin.Click
        Dim loginForm As New frmAdminLogin()
        loginForm.Show()
    End Sub
End Class