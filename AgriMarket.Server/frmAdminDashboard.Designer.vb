<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAdminDashboard
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
        Me.panelTop = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.tabControl = New System.Windows.Forms.TabControl()
        Me.tabDashboard = New System.Windows.Forms.TabPage()
        Me.tabUsers = New System.Windows.Forms.TabPage()
        Me.tabReports = New System.Windows.Forms.TabPage()

        ' Dashboard Stats
        Me.lblUsers = New System.Windows.Forms.Label()
        Me.lblTraders = New System.Windows.Forms.Label()
        Me.lblBuyers = New System.Windows.Forms.Label()
        Me.lblOrders = New System.Windows.Forms.Label()

        ' Users Grid
        Me.dgvUsers = New System.Windows.Forms.DataGridView()
        Me.btnRefreshUsers = New System.Windows.Forms.Button()
        Me.btnApprove = New System.Windows.Forms.Button()

        ' Reports Tab
        Me.cmbReportType = New System.Windows.Forms.ComboBox()
        Me.dgvReports = New System.Windows.Forms.DataGridView()
        Me.btnPreviewReport = New System.Windows.Forms.Button()
        Me.lblReportTitle = New System.Windows.Forms.Label()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()

        ' Setup Form
        Me.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.ClientSize = New System.Drawing.Size(1180, 680)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AgriMarket - Admin Dashboard"

        ' Top Bar
        Me.panelTop.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.panelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelTop.Size = New System.Drawing.Size(1180, 70)
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(20, 18)
        Me.lblTitle.Text = "AgriMarket Server Admin"
        Me.btnLogout.BackColor = System.Drawing.Color.Transparent
        Me.btnLogout.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogout.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnLogout.ForeColor = System.Drawing.Color.White
        Me.btnLogout.Location = New System.Drawing.Point(1050, 18)
        Me.btnLogout.Size = New System.Drawing.Size(100, 35)
        Me.btnLogout.Text = "Log Out"
        Me.btnLogout.UseVisualStyleBackColor = False
        Me.panelTop.Controls.Add(Me.lblTitle)
        Me.panelTop.Controls.Add(Me.btnLogout)

        ' Tab Control
        Me.tabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabControl.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.tabControl.Location = New System.Drawing.Point(0, 70)
        Me.tabControl.Size = New System.Drawing.Size(1180, 610)

        ' Dashboard Tab
        Me.tabDashboard.Text = "Dashboard Overview"
        Me.tabDashboard.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.lblUsers.AutoSize = True
        Me.lblUsers.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.lblUsers.Location = New System.Drawing.Point(50, 50)
        Me.lblUsers.Text = "Total Users: 0"
        Me.lblTraders.AutoSize = True
        Me.lblTraders.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.lblTraders.Location = New System.Drawing.Point(50, 100)
        Me.lblTraders.Text = "Active Traders: 0"
        Me.lblBuyers.AutoSize = True
        Me.lblBuyers.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.lblBuyers.Location = New System.Drawing.Point(50, 150)
        Me.lblBuyers.Text = "Active Buyers: 0"
        Me.lblOrders.AutoSize = True
        Me.lblOrders.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.lblOrders.Location = New System.Drawing.Point(50, 200)
        Me.lblOrders.Text = "Total Orders: 0"
        Me.tabDashboard.Controls.Add(Me.lblUsers)
        Me.tabDashboard.Controls.Add(Me.lblTraders)
        Me.tabDashboard.Controls.Add(Me.lblBuyers)
        Me.tabDashboard.Controls.Add(Me.lblOrders)

        ' Users Tab
        Me.tabUsers.Text = "Manage Users"
        Me.tabUsers.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.dgvUsers.Location = New System.Drawing.Point(20, 20)
        Me.dgvUsers.Size = New System.Drawing.Size(1120, 480)
        Me.btnRefreshUsers.Location = New System.Drawing.Point(900, 520)
        Me.btnRefreshUsers.Size = New System.Drawing.Size(110, 40)
        Me.btnRefreshUsers.Text = "Refresh"
        Me.btnApprove.Location = New System.Drawing.Point(1020, 520)
        Me.btnApprove.Size = New System.Drawing.Size(120, 40)
        Me.btnApprove.Text = "Approve Trader"
        Me.btnApprove.BackColor = System.Drawing.Color.FromArgb(27, 122, 80)
        Me.btnApprove.ForeColor = System.Drawing.Color.White
        Me.btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnApprove.FlatAppearance.BorderSize = 0
        Me.tabUsers.Controls.Add(Me.dgvUsers)
        Me.tabUsers.Controls.Add(Me.btnRefreshUsers)
        Me.tabUsers.Controls.Add(Me.btnApprove)

        ' Reports Tab
        Me.tabReports.Text = "Printable Reports"
        Me.tabReports.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)

        Me.lblReportTitle.AutoSize = True
        Me.lblReportTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblReportTitle.Location = New System.Drawing.Point(20, 20)
        Me.lblReportTitle.Text = "Select Report:"

        Me.cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbReportType.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.cmbReportType.Items.AddRange(New Object() {"Stock Report (RP-1)", "Sales Report (RP-3)"})
        Me.cmbReportType.Location = New System.Drawing.Point(180, 18)
        Me.cmbReportType.Size = New System.Drawing.Size(250, 29)

        Me.btnPreviewReport.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnPreviewReport.ForeColor = System.Drawing.Color.White
        Me.btnPreviewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreviewReport.FlatAppearance.BorderSize = 0
        Me.btnPreviewReport.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnPreviewReport.Location = New System.Drawing.Point(450, 15)
        Me.btnPreviewReport.Size = New System.Drawing.Size(120, 35)
        Me.btnPreviewReport.Text = "Print / Preview"

        Me.dgvReports.Location = New System.Drawing.Point(20, 70)
        Me.dgvReports.Size = New System.Drawing.Size(1120, 480)
        Me.dgvReports.AllowUserToAddRows = False
        Me.dgvReports.ReadOnly = True

        Me.tabReports.Controls.Add(Me.lblReportTitle)
        Me.tabReports.Controls.Add(Me.cmbReportType)
        Me.tabReports.Controls.Add(Me.btnPreviewReport)
        Me.tabReports.Controls.Add(Me.dgvReports)

        Me.tabControl.TabPages.Add(Me.tabDashboard)
        Me.tabControl.TabPages.Add(Me.tabUsers)
        Me.tabControl.TabPages.Add(Me.tabReports)

        Me.Controls.Add(Me.tabControl)
        Me.Controls.Add(Me.panelTop)
        Me.tabMasterLists = New System.Windows.Forms.TabPage()
        Me.cmbListType = New System.Windows.Forms.ComboBox()
        Me.dgvMaster = New System.Windows.Forms.DataGridView()
        Me.txtNewItem = New System.Windows.Forms.TextBox()
        Me.btnAddMaster = New System.Windows.Forms.Button()
        Me.btnDeleteMaster = New System.Windows.Forms.Button()

        Me.tabMasterLists.Text = "Master Lists"
        Me.tabMasterLists.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)

        Me.cmbListType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbListType.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.cmbListType.Items.AddRange(New Object() {"Categories", "Districts"})
        Me.cmbListType.Location = New System.Drawing.Point(20, 20)
        Me.cmbListType.Size = New System.Drawing.Size(200, 28)
        Me.cmbListType.SelectedIndex = 0

        Me.txtNewItem.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txtNewItem.Location = New System.Drawing.Point(240, 20)
        Me.txtNewItem.Size = New System.Drawing.Size(300, 28)

        Me.btnAddMaster.BackColor = System.Drawing.Color.FromArgb(27, 122, 80)
        Me.btnAddMaster.FlatAppearance.BorderSize = 0
        Me.btnAddMaster.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddMaster.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnAddMaster.ForeColor = System.Drawing.Color.White
        Me.btnAddMaster.Location = New System.Drawing.Point(560, 18)
        Me.btnAddMaster.Size = New System.Drawing.Size(100, 32)
        Me.btnAddMaster.Text = "Add Item"

        Me.btnDeleteMaster.BackColor = System.Drawing.Color.FromArgb(179, 38, 30)
        Me.btnDeleteMaster.FlatAppearance.BorderSize = 0
        Me.btnDeleteMaster.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeleteMaster.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDeleteMaster.ForeColor = System.Drawing.Color.White
        Me.btnDeleteMaster.Location = New System.Drawing.Point(680, 18)
        Me.btnDeleteMaster.Size = New System.Drawing.Size(120, 32)
        Me.btnDeleteMaster.Text = "Delete Selected"

        Me.dgvMaster.Location = New System.Drawing.Point(20, 70)
        Me.dgvMaster.Size = New System.Drawing.Size(1120, 480)
        Me.dgvMaster.AllowUserToAddRows = False
        Me.dgvMaster.ReadOnly = True
        Me.dgvMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect

        Me.tabMasterLists.Controls.Add(Me.cmbListType)
        Me.tabMasterLists.Controls.Add(Me.txtNewItem)
        Me.tabMasterLists.Controls.Add(Me.btnAddMaster)
        Me.tabMasterLists.Controls.Add(Me.btnDeleteMaster)
        Me.tabMasterLists.Controls.Add(Me.dgvMaster)


        ' Announcements Tab
        Me.tabAnnouncements = New System.Windows.Forms.TabPage()
        Me.lblAnnounceTitle = New System.Windows.Forms.Label()
        Me.txtAnnounceTitle = New System.Windows.Forms.TextBox()
        Me.lblAnnounceBody = New System.Windows.Forms.Label()
        Me.txtAnnounceBody = New System.Windows.Forms.TextBox()
        Me.btnSendAnnouncement = New System.Windows.Forms.Button()

        ' Connected Users Tab
        Me.tabConnected = New System.Windows.Forms.TabPage()
        Me.dgvConnected = New System.Windows.Forms.DataGridView()
        Me.btnRefreshConnected = New System.Windows.Forms.Button()


        Me.tabAnnouncements.Text = "Announcements"
        Me.tabAnnouncements.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)

        Me.lblAnnounceTitle.AutoSize = True
        Me.lblAnnounceTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblAnnounceTitle.Location = New System.Drawing.Point(20, 20)
        Me.lblAnnounceTitle.Text = "Title"

        Me.txtAnnounceTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtAnnounceTitle.Location = New System.Drawing.Point(20, 50)
        Me.txtAnnounceTitle.Size = New System.Drawing.Size(500, 29)

        Me.lblAnnounceBody.AutoSize = True
        Me.lblAnnounceBody.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblAnnounceBody.Location = New System.Drawing.Point(20, 100)
        Me.lblAnnounceBody.Text = "Message Body"

        Me.txtAnnounceBody.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtAnnounceBody.Location = New System.Drawing.Point(20, 130)
        Me.txtAnnounceBody.Size = New System.Drawing.Size(500, 200)
        Me.txtAnnounceBody.Multiline = True

        Me.btnSendAnnouncement.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnSendAnnouncement.FlatAppearance.BorderSize = 0
        Me.btnSendAnnouncement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSendAnnouncement.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnSendAnnouncement.ForeColor = System.Drawing.Color.White
        Me.btnSendAnnouncement.Location = New System.Drawing.Point(20, 350)
        Me.btnSendAnnouncement.Size = New System.Drawing.Size(500, 50)
        Me.btnSendAnnouncement.Text = "Broadcast Announcement"

        Me.tabAnnouncements.Controls.Add(Me.lblAnnounceTitle)
        Me.tabAnnouncements.Controls.Add(Me.txtAnnounceTitle)
        Me.tabAnnouncements.Controls.Add(Me.lblAnnounceBody)
        Me.tabAnnouncements.Controls.Add(Me.txtAnnounceBody)
        Me.tabAnnouncements.Controls.Add(Me.btnSendAnnouncement)

        Me.tabConnected.Text = "Connected Users"
        Me.tabConnected.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)

        Me.dgvConnected.Location = New System.Drawing.Point(20, 20)
        Me.dgvConnected.Size = New System.Drawing.Size(1120, 480)
        Me.dgvConnected.AllowUserToAddRows = False
        Me.dgvConnected.ReadOnly = True
        Me.dgvConnected.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect

        Me.btnRefreshConnected.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnRefreshConnected.FlatAppearance.BorderSize = 0
        Me.btnRefreshConnected.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefreshConnected.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefreshConnected.ForeColor = System.Drawing.Color.White
        Me.btnRefreshConnected.Location = New System.Drawing.Point(1000, 520)
        Me.btnRefreshConnected.Size = New System.Drawing.Size(140, 40)
        Me.btnRefreshConnected.Text = "Refresh List"

        Me.tabConnected.Controls.Add(Me.dgvConnected)
        Me.tabConnected.Controls.Add(Me.btnRefreshConnected)

        Me.tabControl.TabPages.Add(Me.tabAnnouncements)
        Me.tabControl.TabPages.Add(Me.tabConnected)

        Me.tabControl.TabPages.Add(Me.tabMasterLists)

        ' Listings Tab
        Me.tabListingsAdmin = New System.Windows.Forms.TabPage()
        Me.dgvAllListings = New System.Windows.Forms.DataGridView()

        ' Orders Tab
        Me.tabOrdersAdmin = New System.Windows.Forms.TabPage()
        Me.dgvAllOrders = New System.Windows.Forms.DataGridView()

        Me.tabListingsAdmin.Text = "All Listings"
        Me.tabListingsAdmin.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.dgvAllListings.Location = New System.Drawing.Point(20, 20)
        Me.dgvAllListings.Size = New System.Drawing.Size(1120, 530)
        Me.dgvAllListings.AllowUserToAddRows = False
        Me.dgvAllListings.ReadOnly = True
        Me.dgvAllListings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.tabListingsAdmin.Controls.Add(Me.dgvAllListings)

        Me.tabOrdersAdmin.Text = "All Orders"
        Me.tabOrdersAdmin.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.dgvAllOrders.Location = New System.Drawing.Point(20, 20)
        Me.dgvAllOrders.Size = New System.Drawing.Size(1120, 530)
        Me.dgvAllOrders.AllowUserToAddRows = False
        Me.dgvAllOrders.ReadOnly = True
        Me.dgvAllOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.tabOrdersAdmin.Controls.Add(Me.dgvAllOrders)

        Me.tabControl.TabPages.Add(Me.tabListingsAdmin)
        Me.tabControl.TabPages.Add(Me.tabOrdersAdmin)


        Me.tabLogs = New System.Windows.Forms.TabPage()
        Me.dgvLogs = New System.Windows.Forms.DataGridView()
        Me.btnRefreshLogs = New System.Windows.Forms.Button()


        Me.tabLogs.Text = "Server Logs"
        Me.tabLogs.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)

        Me.dgvLogs.Location = New System.Drawing.Point(20, 20)
        Me.dgvLogs.Size = New System.Drawing.Size(1120, 480)
        Me.dgvLogs.AllowUserToAddRows = False
        Me.dgvLogs.ReadOnly = True
        Me.dgvLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect

        Me.btnRefreshLogs.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnRefreshLogs.FlatAppearance.BorderSize = 0
        Me.btnRefreshLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefreshLogs.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefreshLogs.ForeColor = System.Drawing.Color.White
        Me.btnRefreshLogs.Location = New System.Drawing.Point(1000, 520)
        Me.btnRefreshLogs.Size = New System.Drawing.Size(140, 40)
        Me.btnRefreshLogs.Text = "Refresh Logs"

        Me.tabLogs.Controls.Add(Me.dgvLogs)
        Me.tabLogs.Controls.Add(Me.btnRefreshLogs)
        Me.tabControl.TabPages.Add(Me.tabLogs)

    End Sub

    Friend WithEvents panelTop As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnLogout As System.Windows.Forms.Button
    Friend WithEvents tabControl As System.Windows.Forms.TabControl
    Friend WithEvents tabDashboard As System.Windows.Forms.TabPage
    Friend WithEvents tabUsers As System.Windows.Forms.TabPage
    Friend WithEvents tabReports As System.Windows.Forms.TabPage
    Friend WithEvents lblUsers As System.Windows.Forms.Label
    Friend WithEvents lblTraders As System.Windows.Forms.Label
    Friend WithEvents lblBuyers As System.Windows.Forms.Label
    Friend WithEvents lblOrders As System.Windows.Forms.Label
    Friend WithEvents dgvUsers As System.Windows.Forms.DataGridView
    Friend WithEvents btnRefreshUsers As System.Windows.Forms.Button
    Friend WithEvents btnApprove As System.Windows.Forms.Button
    Friend WithEvents cmbReportType As System.Windows.Forms.ComboBox
    Friend WithEvents dgvReports As System.Windows.Forms.DataGridView
    Friend WithEvents btnPreviewReport As System.Windows.Forms.Button
    Friend WithEvents lblReportTitle As System.Windows.Forms.Label
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents tabMasterLists As System.Windows.Forms.TabPage
    Friend WithEvents cmbListType As System.Windows.Forms.ComboBox
    Friend WithEvents txtNewItem As System.Windows.Forms.TextBox
    Friend WithEvents btnAddMaster As System.Windows.Forms.Button
    Friend WithEvents btnDeleteMaster As System.Windows.Forms.Button
    Friend WithEvents dgvMaster As System.Windows.Forms.DataGridView
    Friend WithEvents tabAnnouncements As System.Windows.Forms.TabPage
    Friend WithEvents lblAnnounceTitle As System.Windows.Forms.Label
    Friend WithEvents txtAnnounceTitle As System.Windows.Forms.TextBox
    Friend WithEvents lblAnnounceBody As System.Windows.Forms.Label
    Friend WithEvents txtAnnounceBody As System.Windows.Forms.TextBox
    Friend WithEvents btnSendAnnouncement As System.Windows.Forms.Button
    Friend WithEvents tabConnected As System.Windows.Forms.TabPage
    Friend WithEvents dgvConnected As System.Windows.Forms.DataGridView
    Friend WithEvents btnRefreshConnected As System.Windows.Forms.Button
    Friend WithEvents tabListingsAdmin As System.Windows.Forms.TabPage
    Friend WithEvents dgvAllListings As System.Windows.Forms.DataGridView
    Friend WithEvents tabOrdersAdmin As System.Windows.Forms.TabPage
    Friend WithEvents dgvAllOrders As System.Windows.Forms.DataGridView
    Friend WithEvents tabLogs As System.Windows.Forms.TabPage
    Friend WithEvents dgvLogs As System.Windows.Forms.DataGridView
    Friend WithEvents btnRefreshLogs As System.Windows.Forms.Button
End Class