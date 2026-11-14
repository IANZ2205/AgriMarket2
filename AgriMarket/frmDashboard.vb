Public Class frmDashboard
    Private _userId As Integer
    Private _userName As String
    Private _userRole As String

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
        NavigateTo("Home")
    End Sub

    Private Sub HandleNotification(message As String)
        If Me.InvokeRequired Then
            Me.Invoke(New Action(Sub() HandleNotification(message)))
        Else
            Dim toast As New frmToast("AgriMarket Alert", message)
            toast.Show()
        End If
    End Sub

    Private Sub NavigateTo(screen As String)
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

                Dim homeView As New ucHome()
                homeView.UserId = _userId
                homeView.UserRole = _userRole
                homeView.UserName = _userName
                homeView.Dock = DockStyle.Fill
                panelContent.Controls.Add(homeView)

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
    End Sub

    ' --- Event Handlers for Product Detail ---
    Private Sub HandleViewProduct(itemId As Integer)
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
        NavigateTo("Browse")
    End Sub

    Private Async Sub HandleChatFromDetail(traderId As Integer)
        Await NavigateToChat(traderId)
    End Sub

    Private Async Sub HandlePlaceOrderFromDetail(id As Integer, traderId As Integer, price As Decimal, qty As Integer)
        Dim inputQty As String = InputBox("How many units do you want to order? (Available: " & qty & ")", "Place Order")
        Dim parsedQty As Integer

        If Integer.TryParse(inputQty, parsedQty) AndAlso parsedQty > 0 AndAlso parsedQty <= qty Then
            Dim res As String = Await NetworkClient.SendRequestAsync($"PLACE_ORDER|{id}|{traderId}|{_userId}|{parsedQty}|{price}")
            If res.StartsWith("OK") Then
                MessageBox.Show("Order placed successfully! The trader has been notified.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                NavigateTo("Browse")
            Else
                MessageBox.Show("Error placing order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    ' --- Existing Helper Methods ---
    Public Async Function NavigateToChat(traderId As Integer) As Task
        NavigateTo("Chats")
        For Each ctrl As Control In panelContent.Controls
            If TypeOf ctrl Is ucChats Then
                Await CType(ctrl, ucChats).OpenChatWith(traderId)
                Exit For
            End If
        Next
    End Function

    Private Async Sub HandleStartChatRequested(traderName As String)
        Dim res As String = Await NetworkClient.SendRequestAsync($"GET_USER_ID|{traderName}")
        Dim parts As String() = res.Split("|"c)
        If parts(0) = "OK" Then
            Dim traderId As Integer = Convert.ToInt32(parts(2))
            Await NavigateToChat(traderId)
        End If
    End Sub

    ' --- Top Bar Click Events ---
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        RemoveHandler NetworkClient.NotificationReceived, AddressOf HandleNotification
        Dim loginForm As New frmLogin()
        loginForm.Show()
        Me.Close()
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

    Private Sub HandleViewTrader(traderId As Integer)
        panelContent.Controls.Clear()

        Dim traderView As New ucTraderDetails()
        traderView.LoadTrader(traderId)
        traderView.Dock = DockStyle.Fill

        AddHandler traderView.BackClicked, Sub() NavigateTo("Browse")
        AddHandler traderView.ChatClicked, Async Sub(id) Await NavigateToChat(id)
        AddHandler traderView.ViewProductClicked, AddressOf HandleViewProduct

        panelContent.Controls.Add(traderView)
    End Sub
End Class