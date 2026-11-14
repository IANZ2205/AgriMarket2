<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDashboard
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        panelTop = New Panel()
        lblLogo = New Label()
        btnNavHome = New Button()
        btnNavStock = New Button()
        btnNavOrders = New Button()
        btnNavBrowse = New Button()
        btnNavMyOrders = New Button()
        btnNavAdmin = New Button()
        btnNavChats = New Button()
        btnNavNotifications = New Button()
        lblWelcome = New Label()
        lblRole = New Label()
        btnNavProfile = New Button()
        btnLogout = New Button()
        panelContent = New Panel()
        panelTop.SuspendLayout()
        SuspendLayout()
        ' 
        ' panelTop
        ' 
        panelTop.BackColor = Color.FromArgb(CByte(1), CByte(96), CByte(100))
        panelTop.Controls.Add(lblLogo)
        panelTop.Controls.Add(btnNavHome)
        panelTop.Controls.Add(btnNavStock)
        panelTop.Controls.Add(btnNavOrders)
        panelTop.Controls.Add(btnNavBrowse)
        panelTop.Controls.Add(btnNavMyOrders)
        panelTop.Controls.Add(btnNavAdmin)
        panelTop.Controls.Add(btnNavChats)
        panelTop.Controls.Add(btnNavNotifications)
        panelTop.Controls.Add(lblWelcome)
        panelTop.Controls.Add(lblRole)
        panelTop.Controls.Add(btnNavProfile)
        panelTop.Controls.Add(btnLogout)
        panelTop.Dock = DockStyle.Top
        panelTop.Location = New Point(0, 0)
        panelTop.Name = "panelTop"
        panelTop.Size = New Size(1180, 70)
        panelTop.TabIndex = 0
        ' 
        ' lblLogo
        ' 
        lblLogo.AutoSize = True
        lblLogo.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        lblLogo.ForeColor = Color.White
        lblLogo.Location = New Point(20, 18)
        lblLogo.Name = "lblLogo"
        lblLogo.Size = New Size(132, 30)
        lblLogo.TabIndex = 0
        lblLogo.Text = "AgriMarket"
        ' 
        ' btnNavHome
        ' 
        btnNavHome.BackColor = Color.White
        btnNavHome.Cursor = Cursors.Hand
        btnNavHome.FlatAppearance.BorderSize = 0
        btnNavHome.FlatStyle = FlatStyle.Flat
        btnNavHome.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnNavHome.ForeColor = Color.FromArgb(CByte(1), CByte(96), CByte(100))
        btnNavHome.Location = New Point(180, 18)
        btnNavHome.Name = "btnNavHome"
        btnNavHome.Size = New Size(90, 35)
        btnNavHome.TabIndex = 1
        btnNavHome.Text = "Home"
        btnNavHome.UseVisualStyleBackColor = False
        ' 
        ' btnNavStock
        ' 
        btnNavStock.BackColor = Color.Transparent
        btnNavStock.Cursor = Cursors.Hand
        btnNavStock.FlatAppearance.BorderSize = 0
        btnNavStock.FlatStyle = FlatStyle.Flat
        btnNavStock.Font = New Font("Segoe UI", 10.0F)
        btnNavStock.ForeColor = Color.White
        btnNavStock.Location = New Point(280, 18)
        btnNavStock.Name = "btnNavStock"
        btnNavStock.Size = New Size(100, 35)
        btnNavStock.TabIndex = 2
        btnNavStock.Text = "My Stock"
        btnNavStock.UseVisualStyleBackColor = False
        ' 
        ' btnNavOrders
        ' 
        btnNavOrders.BackColor = Color.Transparent
        btnNavOrders.Cursor = Cursors.Hand
        btnNavOrders.FlatAppearance.BorderSize = 0
        btnNavOrders.FlatStyle = FlatStyle.Flat
        btnNavOrders.Font = New Font("Segoe UI", 10.0F)
        btnNavOrders.ForeColor = Color.White
        btnNavOrders.Location = New Point(390, 18)
        btnNavOrders.Name = "btnNavOrders"
        btnNavOrders.Size = New Size(100, 35)
        btnNavOrders.TabIndex = 3
        btnNavOrders.Text = "Orders"
        btnNavOrders.UseVisualStyleBackColor = False
        ' 
        ' btnNavBrowse
        ' 
        btnNavBrowse.BackColor = Color.Transparent
        btnNavBrowse.Cursor = Cursors.Hand
        btnNavBrowse.FlatAppearance.BorderSize = 0
        btnNavBrowse.FlatStyle = FlatStyle.Flat
        btnNavBrowse.Font = New Font("Segoe UI", 10.0F)
        btnNavBrowse.ForeColor = Color.White
        btnNavBrowse.Location = New Point(280, 18)
        btnNavBrowse.Name = "btnNavBrowse"
        btnNavBrowse.Size = New Size(100, 35)
        btnNavBrowse.TabIndex = 4
        btnNavBrowse.Text = "Browse"
        btnNavBrowse.UseVisualStyleBackColor = False
        ' 
        ' btnNavMyOrders
        ' 
        btnNavMyOrders.BackColor = Color.Transparent
        btnNavMyOrders.Cursor = Cursors.Hand
        btnNavMyOrders.FlatAppearance.BorderSize = 0
        btnNavMyOrders.FlatStyle = FlatStyle.Flat
        btnNavMyOrders.Font = New Font("Segoe UI", 10.0F)
        btnNavMyOrders.ForeColor = Color.White
        btnNavMyOrders.Location = New Point(390, 18)
        btnNavMyOrders.Name = "btnNavMyOrders"
        btnNavMyOrders.Size = New Size(100, 35)
        btnNavMyOrders.TabIndex = 5
        btnNavMyOrders.Text = "My Orders"
        btnNavMyOrders.UseVisualStyleBackColor = False
        ' 
        ' btnNavAdmin
        ' 
        btnNavAdmin.BackColor = Color.Transparent
        btnNavAdmin.Cursor = Cursors.Hand
        btnNavAdmin.FlatAppearance.BorderSize = 0
        btnNavAdmin.FlatStyle = FlatStyle.Flat
        btnNavAdmin.Font = New Font("Segoe UI", 10.0F)
        btnNavAdmin.ForeColor = Color.White
        btnNavAdmin.Location = New Point(280, 18)
        btnNavAdmin.Name = "btnNavAdmin"
        btnNavAdmin.Size = New Size(110, 35)
        btnNavAdmin.TabIndex = 6
        btnNavAdmin.Text = "Manage Users"
        btnNavAdmin.UseVisualStyleBackColor = False
        ' 
        ' btnNavChats
        ' 
        btnNavChats.BackColor = Color.Transparent
        btnNavChats.Cursor = Cursors.Hand
        btnNavChats.FlatAppearance.BorderSize = 0
        btnNavChats.FlatStyle = FlatStyle.Flat
        btnNavChats.Font = New Font("Segoe UI", 10.0F)
        btnNavChats.ForeColor = Color.White
        btnNavChats.Location = New Point(500, 18)
        btnNavChats.Name = "btnNavChats"
        btnNavChats.Size = New Size(100, 35)
        btnNavChats.TabIndex = 7
        btnNavChats.Text = "Chats"
        btnNavChats.UseVisualStyleBackColor = False
        ' 
        ' btnNavNotifications
        ' 
        btnNavNotifications.BackColor = Color.Transparent
        btnNavNotifications.Cursor = Cursors.Hand
        btnNavNotifications.FlatAppearance.BorderSize = 0
        btnNavNotifications.FlatStyle = FlatStyle.Flat
        btnNavNotifications.Font = New Font("Segoe UI", 10.0F)
        btnNavNotifications.ForeColor = Color.White
        btnNavNotifications.Location = New Point(600, 18)
        btnNavNotifications.Name = "btnNavNotifications"
        btnNavNotifications.Size = New Size(110, 35)
        btnNavNotifications.TabIndex = 8
        btnNavNotifications.Text = "Notifications"
        btnNavNotifications.UseVisualStyleBackColor = False
        ' 
        ' lblWelcome
        ' 
        lblWelcome.AutoSize = True
        lblWelcome.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        lblWelcome.ForeColor = Color.White
        lblWelcome.Location = New Point(750, 20)
        lblWelcome.Name = "lblWelcome"
        lblWelcome.Size = New Size(76, 19)
        lblWelcome.TabIndex = 9
        lblWelcome.Text = "Welcome,"
        ' 
        ' lblRole
        ' 
        lblRole.AutoSize = True
        lblRole.Font = New Font("Segoe UI", 9.0F)
        lblRole.ForeColor = Color.FromArgb(CByte(232), CByte(243), CByte(243))
        lblRole.Location = New Point(750, 40)
        lblRole.Name = "lblRole"
        lblRole.Size = New Size(30, 15)
        lblRole.TabIndex = 10
        lblRole.Text = "Role"
        ' 
        ' btnNavProfile
        ' 
        btnNavProfile.BackColor = Color.Transparent
        btnNavProfile.Cursor = Cursors.Hand
        btnNavProfile.FlatAppearance.BorderColor = Color.White
        btnNavProfile.FlatStyle = FlatStyle.Flat
        btnNavProfile.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        btnNavProfile.ForeColor = Color.White
        btnNavProfile.Location = New Point(969, 18)
        btnNavProfile.Name = "btnNavProfile"
        btnNavProfile.Size = New Size(70, 35)
        btnNavProfile.TabIndex = 11
        btnNavProfile.Text = "Profile"
        btnNavProfile.UseVisualStyleBackColor = False
        ' 
        ' btnLogout
        ' 
        btnLogout.BackColor = Color.Transparent
        btnLogout.Cursor = Cursors.Hand
        btnLogout.FlatAppearance.BorderColor = Color.White
        btnLogout.FlatStyle = FlatStyle.Flat
        btnLogout.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        btnLogout.ForeColor = Color.White
        btnLogout.Location = New Point(1068, 18)
        btnLogout.Name = "btnLogout"
        btnLogout.Size = New Size(100, 35)
        btnLogout.TabIndex = 12
        btnLogout.Text = "Log Out"
        btnLogout.UseVisualStyleBackColor = False
        ' 
        ' panelContent
        ' 
        panelContent.BackColor = Color.FromArgb(CByte(244), CByte(249), CByte(249))
        panelContent.Dock = DockStyle.Fill
        panelContent.Location = New Point(0, 70)
        panelContent.Name = "panelContent"
        panelContent.Size = New Size(1180, 610)
        panelContent.TabIndex = 1
        ' 
        ' frmDashboard
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1180, 680)
        Controls.Add(panelContent)
        Controls.Add(panelTop)
        Name = "frmDashboard"
        StartPosition = FormStartPosition.CenterScreen
        Text = "AgriMarket - Dashboard"
        panelTop.ResumeLayout(False)
        panelTop.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents panelTop As System.Windows.Forms.Panel
    Friend WithEvents lblLogo As System.Windows.Forms.Label
    Friend WithEvents btnNavHome As System.Windows.Forms.Button
    Friend WithEvents btnNavStock As System.Windows.Forms.Button
    Friend WithEvents btnNavOrders As System.Windows.Forms.Button
    Friend WithEvents btnNavBrowse As System.Windows.Forms.Button
    Friend WithEvents btnNavMyOrders As System.Windows.Forms.Button
    Friend WithEvents btnNavAdmin As System.Windows.Forms.Button
    Friend WithEvents btnNavChats As System.Windows.Forms.Button
    Friend WithEvents btnNavNotifications As System.Windows.Forms.Button
    Friend WithEvents lblWelcome As System.Windows.Forms.Label
    Friend WithEvents lblRole As System.Windows.Forms.Label
    Friend WithEvents btnNavProfile As System.Windows.Forms.Button
    Friend WithEvents btnLogout As System.Windows.Forms.Button
    Friend WithEvents panelContent As System.Windows.Forms.Panel
End Class