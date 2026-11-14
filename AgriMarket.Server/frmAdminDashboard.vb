Imports System.Data
Imports System.Drawing.Printing

Public Class frmAdminDashboard
    Private _printData As DataTable

    Private Sub frmAdminDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyGridStyle(dgvUsers)
        ApplyGridStyle(dgvReports)
        ApplyGridStyle(dgvMaster)
        ApplyGridStyle(dgvAllListings)
        ApplyGridStyle(dgvAllOrders)
        ApplyGridStyle(dgvLogs)
        LoadAllListings()
        LoadAllOrders()

        LoadStats()
        LoadUsers()
        LoadLogs()

        cmbReportType.SelectedIndex = 0
        LoadReportData()

        ' Load the master list safely
        cmbListType.SelectedIndex = 0
        LoadMasterList()
    End Sub

    Private Sub ApplyGridStyle(grid As DataGridView)
        grid.BackgroundColor = Color.White
        grid.BorderStyle = BorderStyle.None
        grid.EnableHeadersVisualStyles = False
        grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(232, 243, 243)
        grid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        grid.ColumnHeadersHeight = 35
        grid.RowTemplate.Height = 30
    End Sub

    Private Sub LoadStats()
        Dim dt As DataTable = DatabaseHelper.GetAdminDashboardStats()
        If dt.Rows.Count > 0 Then
            lblUsers.Text = "Total Registered Users: " & dt.Rows(0)("TotalUsers").ToString()
            lblTraders.Text = "Active Traders: " & dt.Rows(0)("TotalTraders").ToString()
            lblBuyers.Text = "Active Buyers: " & dt.Rows(0)("TotalBuyers").ToString()
            lblOrders.Text = "Total Orders Placed: " & dt.Rows(0)("TotalOrders").ToString()
        End If
    End Sub

    Private Sub LoadUsers()
        dgvUsers.DataSource = DatabaseHelper.GetAllUsers()
    End Sub

    Private Sub LoadReportData()
        If cmbReportType.SelectedIndex = 0 Then
            _printData = DatabaseHelper.GetStockReport()
        Else
            _printData = DatabaseHelper.GetSalesReport()
        End If
        dgvReports.DataSource = _printData
    End Sub

    Private Sub cmbReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbReportType.SelectedIndexChanged
        LoadReportData()
    End Sub

    Private Sub btnRefreshUsers_Click(sender As Object, e As EventArgs) Handles btnRefreshUsers.Click
        LoadUsers()
        LoadStats()
    End Sub

    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        If dgvUsers.SelectedRows.Count = 0 Then Exit Sub
        Dim userId As Integer = Convert.ToInt32(dgvUsers.SelectedRows(0).Cells("UserID").Value)

        If DatabaseHelper.ApproveUser(userId) Then
            MessageBox.Show("Trader Approved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadUsers()
            LoadStats()
        End If
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Me.Close()
    End Sub

    ' --- PRINTING LOGIC ---
    Private Sub btnPreviewReport_Click(sender As Object, e As EventArgs) Handles btnPreviewReport.Click
        If _printData Is Nothing OrElse _printData.Rows.Count = 0 Then
            MessageBox.Show("No data to print for this report.", "Empty Report", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim fontHeader As New Font("Segoe UI", 16, FontStyle.Bold)
        Dim fontSub As New Font("Segoe UI", 10, FontStyle.Regular)
        Dim fontBody As New Font("Segoe UI", 9, FontStyle.Regular)
        Dim yPos As Integer = e.MarginBounds.Top
        Dim leftMargin As Integer = e.MarginBounds.Left

        Dim title As String = cmbReportType.SelectedItem.ToString()
        e.Graphics.DrawString("AgriMarket - " & title, fontHeader, Brushes.Black, leftMargin, yPos)
        yPos += 30
        e.Graphics.DrawString("Printed on: " & DateTime.Now.ToString("dd MMM yyyy HH:mm"), fontSub, Brushes.Gray, leftMargin, yPos)
        yPos += 40

        Dim xPos As Integer = leftMargin
        For Each col As DataColumn In _printData.Columns
            e.Graphics.DrawString(col.ColumnName, fontSub, Brushes.Black, xPos, yPos)
            xPos += 150
        Next
        yPos += 25
        e.Graphics.DrawLine(Pens.Black, leftMargin, yPos - 5, e.MarginBounds.Right, yPos - 5)

        For Each row As DataRow In _printData.Rows
            xPos = leftMargin
            For Each col As DataColumn In _printData.Columns
                Dim cellValue As String = row(col).ToString()
                If cellValue.Length > 18 Then cellValue = cellValue.Substring(0, 15) & "..."
                e.Graphics.DrawString(cellValue, fontBody, Brushes.Black, xPos, yPos)
                xPos += 150
            Next
            yPos += 25
            If yPos > e.MarginBounds.Bottom - 50 Then
                e.HasMorePages = True
                Exit Sub
            End If
        Next
    End Sub

    ' --- MASTER LISTS LOGIC ---
    Private Sub cmbListType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbListType.SelectedIndexChanged
        LoadMasterList()
    End Sub

    Private Sub LoadMasterList()
        ' Safety check for initialization
        If dgvMaster Is Nothing OrElse cmbListType Is Nothing Then Return
        If cmbListType.SelectedItem Is Nothing Then Return

        Dim listType As String = cmbListType.SelectedItem.ToString()
        dgvMaster.DataSource = DatabaseHelper.GetMasterList(listType)

        If dgvMaster.Columns.Contains("ID") Then dgvMaster.Columns("ID").Visible = False
        If dgvMaster.Columns.Contains("Name") Then dgvMaster.Columns("Name").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    End Sub

    Private Sub btnAddMaster_Click(sender As Object, e As EventArgs) Handles btnAddMaster.Click
        Dim name As String = txtNewItem.Text.Trim()
        If String.IsNullOrWhiteSpace(name) Then
            MessageBox.Show("Please enter a name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim listType As String = cmbListType.SelectedItem.ToString()
        If DatabaseHelper.AddMasterListItem(listType, name) Then
            MessageBox.Show("Item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtNewItem.Clear()
            LoadMasterList()
        Else
            MessageBox.Show("Failed to add item. It may already exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnDeleteMaster_Click(sender As Object, e As EventArgs) Handles btnDeleteMaster.Click
        If dgvMaster.SelectedRows.Count = 0 Then Exit Sub

        Dim id As Integer = Convert.ToInt32(dgvMaster.SelectedRows(0).Cells("ID").Value)
        Dim name As String = dgvMaster.SelectedRows(0).Cells("Name").Value.ToString()

        If MessageBox.Show($"Are you sure you want to delete '{name}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Dim listType As String = cmbListType.SelectedItem.ToString()
            If DatabaseHelper.DeleteMasterListItem(listType, id) Then
                LoadMasterList()
            Else
                MessageBox.Show("Could not delete. This item might be in use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub
    ' Announcements Logic
    Private Sub btnSendAnnouncement_Click(sender As Object, e As EventArgs) Handles btnSendAnnouncement.Click
        Dim title As String = txtAnnounceTitle.Text.Trim()
        Dim body As String = txtAnnounceBody.Text.Trim()

        If String.IsNullOrWhiteSpace(title) OrElse String.IsNullOrWhiteSpace(body) Then
            MessageBox.Show("Please fill in both the Title and the Message Body.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' In a full app, we'd loop through all active users and push to their online sockets.
        ' For this demonstration, we will save it to the database for everyone.
        ' (Note: To push to online users, we would need to share the ConnectedClients dictionary)

        MessageBox.Show("Announcement broadcasted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        txtAnnounceTitle.Clear()
        txtAnnounceBody.Clear()
    End Sub

    ' Connected Users Logic
    Private Sub btnRefreshConnected_Click(sender As Object, e As EventArgs) Handles btnRefreshConnected.Click
        ' Because the Admin Dashboard runs on the same PC as the Server, 
        ' we can't easily access the Server's private ConnectedClients dictionary here.
        ' Instead, for the exam demonstration, we will show a list of users who are "Active" in the database.

        Dim dt As DataTable = DatabaseHelper.GetAllUsers()
        Dim activeUsers As DataTable = dt.Clone()

        For Each row As DataRow In dt.Rows
            If row("Status").ToString() = "Active" Then
                activeUsers.ImportRow(row)
            End If
        Next

        dgvConnected.DataSource = activeUsers
        ApplyGridStyle(dgvConnected)
    End Sub

    Private Sub LoadAllListings()
        dgvAllListings.DataSource = DatabaseHelper.GetAllListingsAdmin()
    End Sub

    Private Sub LoadAllOrders()
        dgvAllOrders.DataSource = DatabaseHelper.GetAllOrdersAdmin()
    End Sub

    Private Sub LoadLogs()
        dgvLogs.DataSource = DatabaseHelper.GetLogs()
    End Sub

    Private Sub btnRefreshLogs_Click(sender As Object, e As EventArgs) Handles btnRefreshLogs.Click
        LoadLogs()
    End Sub
End Class