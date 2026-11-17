Public Class frmDashboard
    Private _userId As Integer
    Private _userName As String
    Private _userRole As String
    Private _handlingDisconnect As Boolean = False
    Private _navigating As Boolean = False

    Public Sub New(userId As Integer, userName As String, userRole As String)
        InitializeComponent()
        _userId = userId
        _userName = userName
        _userRole = userRole
    End Sub

    Private Sub frmDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblWelcome.Text = "Welcome, " & _userName
        lblRole.Text = _userRole

        If _userRole = "Trader" Then
            btnNavStock.Visible = True
            btnNavOrders.Visible = True
            btnNavBrowse.Visible = False
            btnNavMyOrders.Visible = False
            btnNavAdmin.Visible = False
        ElseIf _userRole = "Admin" Then
            btnNavStock.Visible = False
            btnNavOrders.Visible = False
            btnNavBrowse.Visible = False
            btnNavMyOrders.Visible = False
            btnNavAdmin.Visible = True
        Else
            btnNavStock.Visible = False
            btnNavOrders.Visible = False
            btnNavBrowse.Visible = True
            btnNavMyOrders.Visible = True
            btnNavAdmin.Visible = False
        End If

        AddHandler NetworkClient.NotificationReceived, AddressOf HandleNotification
        AddHandler NetworkClient.ForceLogoutReceived, AddressOf HandleForceLogout
        AddHandler NetworkClient.ConnectionLost, AddressOf HandleConnectionLost

        NavigateTo("Home")
    End Sub

    Private Sub frmDashboard_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            RemoveHandler NetworkClient.NotificationReceived, AddressOf HandleNotification
            RemoveHandler NetworkClient.ForceLogoutReceived, AddressOf HandleForceLogout
            RemoveHandler NetworkClient.ConnectionLost, AddressOf HandleConnectionLost
        Catch
        End Try
    End Sub

    ' Defer navigation so we don't dispose a control from inside its own event handler.
    Private Sub DeferredNavigateTo(screen As String)
        If IsDisposed OrElse Disposing Then Return
        If Not Me.IsHandleCreated Then
            NavigateTo(screen)
            Return
        End If
        Try
            Me.BeginInvoke(New Action(Sub()
                                          If IsDisposed OrElse Disposing Then Return
                                          NavigateTo(screen)
                                      End Sub))
        Catch
            Try
                NavigateTo(screen)
            Catch
            End Try
        End Try
    End Sub

    Private Sub DeferredNavigateToChat(traderId As Integer)
        If IsDisposed OrElse Disposing Then Return
        If Not Me.IsHandleCreated Then
            Dim t As Task = NavigateToChat(traderId)
            Return
        End If
        Try
            Me.BeginInvoke(New Action(Sub()
                                          If IsDisposed OrElse Disposing Then Return
                                          Dim t As Task = NavigateToChat(traderId)
                                      End Sub))
        Catch
        End Try
    End Sub

    Private Sub HandleNotification(message As String)
        If Me.IsDisposed OrElse Me.Disposing Then Return
        If Me.InvokeRequired Then
            Try
                Me.Invoke(New Action(Sub() HandleNotification(message)))
            Catch
            End Try
            Return
        End If
        If _handlingDisconnect Then Return
        Try
            Dim toast As New frmToast("AgriMarket Alert", message)
            toast.Show()
        Catch
        End Try
    End Sub

    Private Sub HandleForceLogout(reason As String)
        If Me.IsDisposed OrElse Me.Disposing Then Return
        If Me.InvokeRequired Then
            Try
                Me.Invoke(New Action(Sub() HandleForceLogout(reason)))
            Catch
            End Try
            Return
        End If
        If _handlingDisconnect Then Return
        GoToLogin("You have been disconnected." & vbCrLf & vbCrLf & reason & vbCrLf & vbCrLf & "Please log in again.", "Session Ended")
    End Sub

    Private Sub HandleConnectionLost()
        If Me.IsDisposed OrElse Me.Disposing Then Return
        If Me.InvokeRequired Then
            Try
                Me.Invoke(New Action(Sub() HandleConnectionLost()))
            Catch
            End Try
            Return
        End If
        If _handlingDisconnect Then Return
        GoToLogin("The connection to the server was lost. Please log in again.", "Connection Lost")
    End Sub

    Private Sub GoToLogin(message As String, title As String)
        If _handlingDisconnect Then Return
        _handlingDisconnect = True

        If Not String.IsNullOrEmpty(message) Then
            Try
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch
            End Try
        End If

        Try
            Dim loginForm As New frmLogin()
            loginForm.Show()
        Catch
        End Try
        Me.Close()
    End Sub

    Private Sub NavigateTo(screen As String)
        If _navigating Then Return
        _navigating = True
        Try
            panelContent.Controls.Clear()

            For Each ctrl As Control In panelTop.Controls
                If TypeOf ctrl Is Button AndAlso ctrl.Name.StartsWith("btnNav") Then
                    ctrl.BackColor = Color.Transparent
                    ctrl.ForeColor = Color.White
                    ctrl.Font = New Font("Segoe UI", 10.0!, FontStyle.Regular)
                End If
            Next

            Select Case screen
                Case "Home"
                    btnNavHome.BackColor = Color.White
                    btnNavHome.ForeColor = Color.FromArgb(1, 96, 100)
                    btnNavHome.Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)

                    If _userRole = "Trader" Then
                        Dim homeView As New ucHome()
                        homeView.UserId = _userId
                        homeView.UserRole = _userRole
                        homeView.UserName = _userName
                        homeView.Dock = DockStyle.Fill
                        panelContent.Controls.Add(homeView)
                    Else
                        Dim browseView As New ucBrowse()
                        browseView.BuyerId = _userId
                        browseView.Dock = DockStyle.Fill
                        AddHandler browseView.StartChatRequested, AddressOf HandleStartChatRequested
                        AddHandler browseView.ViewProductClicked, AddressOf HandleViewProduct
                        panelContent.Controls.Add(browseView)
                    End If

                Case "Stock"
                    btnNavStock.BackColor = Color.White
                    btnNavStock.ForeColor = Color.FromArgb(1, 96, 100)
                    btnNavStock.Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)

                    Dim stockView As New ucMyStock()
                    stockView.TraderId = _userId
                    stockView.Dock = DockStyle.Fill
                    panelContent.Controls.Add(stockView)

                Case "Orders"
                    btnNavOrders.BackColor = Color.White
                    btnNavOrders.ForeColor = Color.FromArgb(1, 96, 100)
                    btnNavOrders.Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)

                    Dim ordersView As New ucTraderOrders()
                    ordersView.TraderId = _userId
                    ordersView.Dock = DockStyle.Fill
                    panelContent.Controls.Add(ordersView)

                Case "Browse"
                    btnNavBrowse.BackColor = Color.White
                    btnNavBrowse.ForeColor = Color.FromArgb(1, 96, 100)
                    btnNavBrowse.Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)

                    Dim browseView As New ucBrowse()
                    browseView.BuyerId = _userId
                    browseView.Dock = DockStyle.Fill
                    AddHandler browseView.StartChatRequested, AddressOf HandleStartChatRequested
                    AddHandler browseView.ViewProductClicked, AddressOf HandleViewProduct
                    panelContent.Controls.Add(browseView)

                Case "MyOrders"
                    btnNavMyOrders.BackColor = Color.White
                    btnNavMyOrders.ForeColor = Color.FromArgb(1, 96, 100)
                    btnNavMyOrders.Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)

                    Dim myOrdersView As New ucMyOrders()
                    myOrdersView.BuyerId = _userId
                    myOrdersView.Dock = DockStyle.Fill
                    panelContent.Controls.Add(myOrdersView)

                Case "Admin"
                    btnNavAdmin.BackColor = Color.White
                    btnNavAdmin.ForeColor = Color.FromArgb(1, 96, 100)
                    btnNavAdmin.Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)

                    Dim adminView As New ucAdminUsers()
                    adminView.Dock = DockStyle.Fill
                    panelContent.Controls.Add(adminView)

                Case "Chats"
                    btnNavChats.BackColor = Color.White
                    btnNavChats.ForeColor = Color.FromArgb(1, 96, 100)
                    btnNavChats.Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)

                    Dim chatView As New ucChats()
                    chatView.CurrentUserId = _userId
                    chatView.Dock = DockStyle.Fill
                    panelContent.Controls.Add(chatView)

                Case "Notifications"
                    btnNavNotifications.BackColor = Color.White
                    btnNavNotifications.ForeColor = Color.FromArgb(1, 96, 100)
                    btnNavNotifications.Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)

                    Dim notifView As New ucNotifications()
                    notifView.UserId = _userId
                    notifView.Dock = DockStyle.Fill
                    panelContent.Controls.Add(notifView)

                Case "Profile"
                    btnNavProfile.BackColor = Color.White
                    btnNavProfile.ForeColor = Color.FromArgb(1, 96, 100)
                    btnNavProfile.Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)

                    Dim profileView As New ucProfile()
                    profileView.UserId = _userId
                    profileView.Dock = DockStyle.Fill
                    panelContent.Controls.Add(profileView)
            End Select
        Finally
            _navigating = False
        End Try
    End Sub

    ' ============ All event handlers that clear the parent container ============

    Private Sub HandleViewProduct(itemId As Integer)
        If IsDisposed OrElse Disposing Then Return
        If Not Me.IsHandleCreated Then
            DoShowProduct(itemId)
            Return
        End If
        Try
            Me.BeginInvoke(New Action(Sub()
                                          If IsDisposed OrElse Disposing Then Return
                                          DoShowProduct(itemId)
                                      End Sub))
        Catch
        End Try
    End Sub

    Private Sub DoShowProduct(itemId As Integer)
        panelContent.Controls.Clear()

        Dim detailView As New ucProductDetail()
        detailView.LoadItem(itemId, _userId)
        detailView.Dock = DockStyle.Fill

        AddHandler detailView.BackClicked, AddressOf HandleBackToBrowse
        AddHandler detailView.ChatClicked, AddressOf HandleChatFromDetail
        AddHandler detailView.PlaceOrderClicked, AddressOf HandlePlaceOrderFromDetail
        AddHandler detailView.ViewTraderClicked, AddressOf HandleViewTrader

        panelContent.Controls.Add(detailView)
    End Sub

    Private Sub HandleBackToBrowse()
        DeferredNavigateTo("Browse")
    End Sub

    Private Sub HandleChatFromDetail(traderId As Integer)
        DeferredNavigateToChat(traderId)
    End Sub

    Private Sub HandlePlaceOrderFromDetail(id As Integer, traderId As Integer, price As Decimal, qty As Integer)
        Dim orderForm As New frmPlaceOrder(id, traderId, _userId, price, qty)
        If orderForm.ShowDialog() = DialogResult.OK Then
            MessageBox.Show("Order placed successfully! The trader has been notified.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            DeferredNavigateTo("Browse")
        End If
    End Sub

    Private Sub HandleViewTrader(traderId As Integer)
        If IsDisposed OrElse Disposing Then Return
        If Not Me.IsHandleCreated Then
            DoShowTrader(traderId)
            Return
        End If
        Try
            Me.BeginInvoke(New Action(Sub()
                                          If IsDisposed OrElse Disposing Then Return
                                          DoShowTrader(traderId)
                                      End Sub))
        Catch
        End Try
    End Sub

    Private Sub DoShowTrader(traderId As Integer)
        panelContent.Controls.Clear()

        Dim traderView As New ucTraderDetails()
        traderView.LoadTrader(traderId)
        traderView.Dock = DockStyle.Fill

        AddHandler traderView.BackClicked, Sub() DeferredNavigateTo("Browse")
        AddHandler traderView.ChatClicked, Async Sub(id) Await NavigateToChat(id)
        AddHandler traderView.ViewProductClicked, AddressOf HandleViewProduct

        panelContent.Controls.Add(traderView)
    End Sub

    Public Async Function NavigateToChat(traderId As Integer) As Task
        NavigateTo("Chats")
        For Each ctrl As Control In panelContent.Controls
            If TypeOf ctrl Is ucChats Then
                Await CType(ctrl, ucChats).OpenChatWith(traderId)
                Exit For
            End If
        Next
    End Function

    Private Sub HandleStartChatRequested(traderName As String)
        If IsDisposed OrElse Disposing Then Return
        If Not Me.IsHandleCreated Then
            Dim t As Task = DoStartChat(traderName)
            Return
        End If
        Try
            Me.BeginInvoke(New Action(Async Sub()
                                          If IsDisposed OrElse Disposing Then Return
                                          Await DoStartChat(traderName)
                                      End Sub))
        Catch
        End Try
    End Sub

    Private Async Function DoStartChat(traderName As String) As Task
        Dim res As String = Await NetworkClient.SendRequestAsync($"GET_USER_ID|{traderName}")
        Dim parts As String() = res.Split("|"c)
        If parts(0) = "OK" Then
            Dim traderId As Integer = Convert.ToInt32(parts(2))
            Await NavigateToChat(traderId)
        End If
    End Function

    ' ============ Top bar click handlers ============

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        GoToLogin("", "")
    End Sub

    Private Sub btnNavHome_Click(sender As Object, e As EventArgs) Handles btnNavHome.Click
        NavigateTo("Home")
    End Sub

    Private Sub btnNavStock_Click(sender As Object, e As EventArgs) Handles btnNavStock.Click
        NavigateTo("Stock")
    End Sub

    Private Sub btnNavOrders_Click(sender As Object, e As EventArgs) Handles btnNavOrders.Click
        NavigateTo("Orders")
    End Sub

    Private Sub btnNavBrowse_Click(sender As Object, e As EventArgs) Handles btnNavBrowse.Click
        NavigateTo("Browse")
    End Sub

    Private Sub btnNavMyOrders_Click(sender As Object, e As EventArgs) Handles btnNavMyOrders.Click
        NavigateTo("MyOrders")
    End Sub

    Private Sub btnNavAdmin_Click(sender As Object, e As EventArgs) Handles btnNavAdmin.Click
        NavigateTo("Admin")
    End Sub

    Private Sub btnNavChats_Click(sender As Object, e As EventArgs) Handles btnNavChats.Click
        NavigateTo("Chats")
    End Sub

    Private Sub btnNavNotifications_Click(sender As Object, e As EventArgs) Handles btnNavNotifications.Click
        NavigateTo("Notifications")
    End Sub

    Private Sub btnNavProfile_Click(sender As Object, e As EventArgs) Handles btnNavProfile.Click
        NavigateTo("Profile")
    End Sub
End Class