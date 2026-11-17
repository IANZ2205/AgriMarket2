Imports System.Drawing

' ========== MENU VERSION 4 - SELF-CONTAINED ==========
Public Class frmAdminDashboard
    Private panelTop As System.Windows.Forms.Panel
    Private panelContent As System.Windows.Forms.Panel
    Private lblLogo As System.Windows.Forms.Label
    Private lblWelcome As System.Windows.Forms.Label
    Private lblRole As System.Windows.Forms.Label
    Private btnLogout As System.Windows.Forms.Button
    Private menuButtons As New System.Collections.Generic.Dictionary(Of String, System.Windows.Forms.Button)()

    Private Sub frmAdminDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildShell()
        NavigateTo("Dashboard")
    End Sub

    Private Sub BuildShell()
        panelTop = New System.Windows.Forms.Panel()
        panelTop.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        panelTop.Dock = System.Windows.Forms.DockStyle.Top
        panelTop.Height = 70
        Me.Controls.Add(panelTop)

        ' Right side: welcome + logout
        Dim panelWelcome As New System.Windows.Forms.Panel()
        panelWelcome.Dock = System.Windows.Forms.DockStyle.Right
        panelWelcome.Width = 230
        panelWelcome.BackColor = System.Drawing.Color.Transparent
        panelTop.Controls.Add(panelWelcome)

        btnLogout = New System.Windows.Forms.Button()
        btnLogout.Text = "Log Out"
        btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnLogout.FlatAppearance.BorderColor = System.Drawing.Color.White
        btnLogout.Font = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
        btnLogout.ForeColor = System.Drawing.Color.White
        btnLogout.BackColor = System.Drawing.Color.Transparent
        btnLogout.Cursor = System.Windows.Forms.Cursors.Hand
        btnLogout.Size = New System.Drawing.Size(90, 35)
        btnLogout.Location = New System.Drawing.Point(130, 18)
        AddHandler btnLogout.Click, AddressOf btnLogout_Click
        panelWelcome.Controls.Add(btnLogout)

        lblWelcome = New System.Windows.Forms.Label()
        lblWelcome.AutoSize = True
        lblWelcome.Font = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
        lblWelcome.ForeColor = System.Drawing.Color.White
        lblWelcome.Location = New System.Drawing.Point(10, 18)
        lblWelcome.Text = "Admin"
        panelWelcome.Controls.Add(lblWelcome)

        lblRole = New System.Windows.Forms.Label()
        lblRole.AutoSize = True
        lblRole.Font = New System.Drawing.Font("Segoe UI", 8)
        lblRole.ForeColor = System.Drawing.Color.FromArgb(232, 243, 243)
        lblRole.Location = New System.Drawing.Point(10, 38)
        lblRole.Text = "Administrator"
        panelWelcome.Controls.Add(lblRole)

        ' Left side: logo
        Dim panelLogo As New System.Windows.Forms.Panel()
        panelLogo.Dock = System.Windows.Forms.DockStyle.Left
        panelLogo.Width = 160
        panelLogo.BackColor = System.Drawing.Color.Transparent
        panelTop.Controls.Add(panelLogo)

        lblLogo = New System.Windows.Forms.Label()
        lblLogo.AutoSize = True
        lblLogo.Font = New System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold)
        lblLogo.ForeColor = System.Drawing.Color.White
        lblLogo.Location = New System.Drawing.Point(20, 20)
        lblLogo.Text = "AgriMarket"
        panelLogo.Controls.Add(lblLogo)

        ' Middle: menu
        Dim panelMenu As New System.Windows.Forms.Panel()
        panelMenu.Dock = System.Windows.Forms.DockStyle.Fill
        panelMenu.BackColor = System.Drawing.Color.Transparent
        panelTop.Controls.Add(panelMenu)
        panelMenu.BringToFront()

        ' 10 menu items: display label, internal page key
        Dim menuItems As New List(Of Tuple(Of String, String))()
        menuItems.Add(New Tuple(Of String, String)("Dashboard", "Dashboard"))
        menuItems.Add(New Tuple(Of String, String)("Users", "Users"))
        menuItems.Add(New Tuple(Of String, String)("Listings", "Listings"))
        menuItems.Add(New Tuple(Of String, String)("Orders", "Orders"))
        menuItems.Add(New Tuple(Of String, String)("Masters", "Master Lists"))
        menuItems.Add(New Tuple(Of String, String)("Notices", "Announcements"))
        menuItems.Add(New Tuple(Of String, String)("Reports", "Reports"))
        menuItems.Add(New Tuple(Of String, String)("Logs", "Logs"))
        menuItems.Add(New Tuple(Of String, String)("Online", "Connected"))
        menuItems.Add(New Tuple(Of String, String)("Live", "Performance"))
        menuItems.Add(New Tuple(Of String, String)("Reported", "Reported"))

        Dim xPos As Integer = 8
        For Each item In menuItems
            Dim btn As New System.Windows.Forms.Button()
            btn.Text = item.Item1
            btn.Tag = item.Item2
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 0
            btn.Font = New System.Drawing.Font("Segoe UI", 8.5, System.Drawing.FontStyle.Regular)
            btn.ForeColor = System.Drawing.Color.White
            btn.BackColor = System.Drawing.Color.Transparent
            btn.Cursor = System.Windows.Forms.Cursors.Hand
            Dim w As Integer = Math.Max(60, item.Item1.Length * 6 + 16)
            btn.Size = New System.Drawing.Size(w, 35)
            btn.Location = New System.Drawing.Point(xPos, 18)
            AddHandler btn.Click, AddressOf MenuButton_Click
            panelMenu.Controls.Add(btn)
            menuButtons(item.Item2) = btn
            xPos += w + 2
        Next

        ' Content area
        panelContent = New System.Windows.Forms.Panel()
        panelContent.Dock = System.Windows.Forms.DockStyle.Fill
        panelContent.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.Controls.Add(panelContent)
        panelContent.BringToFront()
    End Sub

    Private Sub MenuButton_Click(sender As Object, e As EventArgs)
        Dim btn = CType(sender, System.Windows.Forms.Button)
        NavigateTo(btn.Tag.ToString())
    End Sub

    Private Sub NavigateTo(page As String)
        For Each kvp In menuButtons
            kvp.Value.BackColor = System.Drawing.Color.Transparent
            kvp.Value.ForeColor = System.Drawing.Color.White
            kvp.Value.Font = New System.Drawing.Font("Segoe UI", 8.5, System.Drawing.FontStyle.Regular)
        Next

        If menuButtons.ContainsKey(page) Then
            menuButtons(page).BackColor = System.Drawing.Color.White
            menuButtons(page).ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
            menuButtons(page).Font = New System.Drawing.Font("Segoe UI", 8.5, System.Drawing.FontStyle.Bold)
        End If

        panelContent.Controls.Clear()

        Dim uc As System.Windows.Forms.UserControl = Nothing
        Select Case page
            Case "Dashboard"
                uc = New ucAdminDashboard()
            Case "Users"
                uc = New ucAdminUsers()
            Case "Listings"
                uc = New ucAdminListings()
            Case "Orders"
                uc = New ucAdminOrders()
            Case "Master Lists"
                uc = New ucAdminMasterLists()
            Case "Announcements"
                uc = New ucAdminAnnouncements()
            Case "Reports"
                uc = New ucAdminReports()
            Case "Logs"
                uc = New ucAdminLogs()
            Case "Connected"
                uc = New ucAdminConnectedUsers()
            Case "Performance"
                uc = New ucAdminLivePerformance()
            Case "Reported"
                uc = New ucAdminReportedChats()
        End Select


        If uc IsNot Nothing Then
            uc.Dock = System.Windows.Forms.DockStyle.Fill
            panelContent.Controls.Add(uc)
        End If

    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
End Class