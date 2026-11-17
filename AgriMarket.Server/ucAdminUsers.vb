Imports System.Data
Imports System.Drawing

Public Class ucAdminUsers
    Private lblTitle As System.Windows.Forms.Label
    Private lblSubtitle As System.Windows.Forms.Label
    Private txtSearch As System.Windows.Forms.TextBox
    Private cmbFilter As System.Windows.Forms.ComboBox
    Private dgvUsers As System.Windows.Forms.DataGridView
    Private btnApprove As System.Windows.Forms.Button
    Private btnSuspend As System.Windows.Forms.Button
    Private btnReactivate As System.Windows.Forms.Button
    Private btnResetPassword As System.Windows.Forms.Button
    Private btnRefresh As System.Windows.Forms.Button
    Private lblCounter As System.Windows.Forms.Label

    Private Sub ucAdminUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildUI()
        LoadUsers()
    End Sub

    Private Sub BuildUI()
        lblTitle = New System.Windows.Forms.Label()
        lblTitle.AutoSize = True
        lblTitle.Font = New System.Drawing.Font("Segoe UI", 24, System.Drawing.FontStyle.Bold)
        lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        lblTitle.Location = New System.Drawing.Point(30, 25)
        lblTitle.Text = "Manage Users"
        Me.Controls.Add(lblTitle)

        lblSubtitle = New System.Windows.Forms.Label()
        lblSubtitle.AutoSize = True
        lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 11)
        lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblSubtitle.Location = New System.Drawing.Point(35, 72)
        lblSubtitle.Text = "All registered accounts on AgriMarket"
        Me.Controls.Add(lblSubtitle)

        txtSearch = New System.Windows.Forms.TextBox()
        txtSearch.Font = New System.Drawing.Font("Segoe UI", 11)
        txtSearch.Location = New System.Drawing.Point(30, 110)
        txtSearch.Size = New System.Drawing.Size(400, 27)
        txtSearch.Text = ""
        AddHandler txtSearch.TextChanged, AddressOf txtSearch_TextChanged
        Me.Controls.Add(txtSearch)

        cmbFilter = New System.Windows.Forms.ComboBox()
        cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        cmbFilter.Font = New System.Drawing.Font("Segoe UI", 11)
        cmbFilter.Location = New System.Drawing.Point(450, 110)
        cmbFilter.Size = New System.Drawing.Size(180, 27)
        cmbFilter.Items.AddRange(New Object() {"All", "Pending", "Active", "Suspended"})
        cmbFilter.SelectedIndex = 0
        AddHandler cmbFilter.SelectedIndexChanged, AddressOf cmbFilter_SelectedIndexChanged
        Me.Controls.Add(cmbFilter)

        btnRefresh = New System.Windows.Forms.Button()
        btnRefresh.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnRefresh.FlatAppearance.BorderSize = 0
        btnRefresh.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnRefresh.ForeColor = System.Drawing.Color.White
        btnRefresh.Location = New System.Drawing.Point(650, 106)
        btnRefresh.Size = New System.Drawing.Size(110, 32)
        btnRefresh.Text = "Refresh"
        btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnRefresh.Click, AddressOf btnRefresh_Click
        Me.Controls.Add(btnRefresh)

        lblCounter = New System.Windows.Forms.Label()
        lblCounter.AutoSize = True
        lblCounter.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Italic)
        lblCounter.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblCounter.Location = New System.Drawing.Point(780, 114)
        lblCounter.Text = "0 users"
        Me.Controls.Add(lblCounter)

        dgvUsers = New System.Windows.Forms.DataGridView()
        dgvUsers.BackgroundColor = System.Drawing.Color.White
        dgvUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        dgvUsers.Location = New System.Drawing.Point(30, 160)
        dgvUsers.Size = New System.Drawing.Size(1120, 435)
        dgvUsers.AllowUserToAddRows = False
        dgvUsers.ReadOnly = True
        dgvUsers.RowHeadersVisible = False
        dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        dgvUsers.MultiSelect = False
        dgvUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        dgvUsers.GridColor = System.Drawing.Color.FromArgb(230, 230, 230)
        dgvUsers.EnableHeadersVisualStyles = False
        dgvUsers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        dgvUsers.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
        dgvUsers.ColumnHeadersHeight = 36
        dgvUsers.RowTemplate.Height = 38
        dgvUsers.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10)
        dgvUsers.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        dgvUsers.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        dgvUsers.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.Controls.Add(dgvUsers)

        btnApprove = New System.Windows.Forms.Button()
        btnApprove.BackColor = System.Drawing.Color.FromArgb(27, 122, 80)
        btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnApprove.FlatAppearance.BorderSize = 0
        btnApprove.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnApprove.ForeColor = System.Drawing.Color.White
        btnApprove.Location = New System.Drawing.Point(30, 610)
        btnApprove.Size = New System.Drawing.Size(140, 36)
        btnApprove.Text = "Approve Trader"
        btnApprove.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnApprove.Click, AddressOf btnApprove_Click
        Me.Controls.Add(btnApprove)

        btnSuspend = New System.Windows.Forms.Button()
        btnSuspend.BackColor = System.Drawing.Color.FromArgb(179, 38, 30)
        btnSuspend.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnSuspend.FlatAppearance.BorderSize = 0
        btnSuspend.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnSuspend.ForeColor = System.Drawing.Color.White
        btnSuspend.Location = New System.Drawing.Point(180, 610)
        btnSuspend.Size = New System.Drawing.Size(140, 36)
        btnSuspend.Text = "Suspend User"
        btnSuspend.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnSuspend.Click, AddressOf btnSuspend_Click
        Me.Controls.Add(btnSuspend)

        btnReactivate = New System.Windows.Forms.Button()
        btnReactivate.BackColor = System.Drawing.Color.FromArgb(95, 179, 179)
        btnReactivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnReactivate.FlatAppearance.BorderSize = 0
        btnReactivate.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnReactivate.ForeColor = System.Drawing.Color.White
        btnReactivate.Location = New System.Drawing.Point(330, 610)
        btnReactivate.Size = New System.Drawing.Size(160, 36)
        btnReactivate.Text = "Reactivate User"
        btnReactivate.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnReactivate.Click, AddressOf btnReactivate_Click
        Me.Controls.Add(btnReactivate)

        btnResetPassword = New System.Windows.Forms.Button()
        btnResetPassword.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        btnResetPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnResetPassword.FlatAppearance.BorderSize = 0
        btnResetPassword.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnResetPassword.ForeColor = System.Drawing.Color.White
        btnResetPassword.Location = New System.Drawing.Point(500, 610)
        btnResetPassword.Size = New System.Drawing.Size(180, 36)
        btnResetPassword.Text = "Reset Password"
        btnResetPassword.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnResetPassword.Click, AddressOf btnResetPassword_Click
        Me.Controls.Add(btnResetPassword)
    End Sub

    Private Sub LoadUsers()
        Try
            Dim allUsers As System.Data.DataTable = DatabaseHelper.GetAllUsers()
            Dim filtered As System.Data.DataTable = allUsers.Clone()

            Dim filterStatus As String = "All"
            If cmbFilter.SelectedItem IsNot Nothing Then filterStatus = cmbFilter.SelectedItem.ToString()
            Dim searchText As String = txtSearch.Text.Trim().ToLower()

            For Each row As System.Data.DataRow In allUsers.Rows
                Dim statusMatches As Boolean = (filterStatus = "All" OrElse row("Status").ToString() = filterStatus)
                Dim searchMatches As Boolean = String.IsNullOrEmpty(searchText) OrElse
                                                row("Name").ToString().ToLower().Contains(searchText) OrElse
                                                row("Email").ToString().ToLower().Contains(searchText)
                If statusMatches AndAlso searchMatches Then
                    filtered.ImportRow(row)
                End If
            Next

            dgvUsers.DataSource = filtered
            If dgvUsers.Columns.Contains("UserID") Then dgvUsers.Columns("UserID").Visible = False
            If dgvUsers.Columns.Contains("Name") Then dgvUsers.Columns("Name").AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            If dgvUsers.Columns.Contains("Email") Then dgvUsers.Columns("Email").AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            If dgvUsers.Columns.Contains("Role") Then dgvUsers.Columns("Role").Width = 130
            If dgvUsers.Columns.Contains("Status") Then dgvUsers.Columns("Status").Width = 130

            lblCounter.Text = filtered.Rows.Count.ToString() & " user(s) shown"
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Error loading users: " & ex.Message)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs)
        LoadUsers()
    End Sub

    Private Sub cmbFilter_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadUsers()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        LoadUsers()
    End Sub

    Private Function GetSelectedUser() As Tuple(Of Integer, String, String, String)
        If dgvUsers.SelectedRows.Count = 0 Then Return Nothing
        Try
            Dim row As DataGridViewRow = dgvUsers.SelectedRows(0)
            Dim userId As Integer = Convert.ToInt32(row.Cells("UserID").Value)
            Dim name As String = row.Cells("Name").Value.ToString()
            Dim role As String = row.Cells("Role").Value.ToString()
            Dim status As String = row.Cells("Status").Value.ToString()
            Return New Tuple(Of Integer, String, String, String)(userId, name, role, status)
        Catch
            Return Nothing
        End Try
    End Function

    Private Sub btnApprove_Click(sender As Object, e As EventArgs)
        Dim user = GetSelectedUser()
        If user Is Nothing Then
            System.Windows.Forms.MessageBox.Show("Please select a user first.", "Validation", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If
        If user.Item3 <> "Trader" Then
            System.Windows.Forms.MessageBox.Show("Only Trader accounts need approval.", "Info", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        End If
        If user.Item4 = "Active" Then
            System.Windows.Forms.MessageBox.Show("This trader is already approved.", "Info", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        End If
        If DatabaseHelper.ApproveUser(user.Item1) Then
            DatabaseHelper.WriteLog("Info", "Approval", $"Admin approved UserID {user.Item1} ({user.Item2}).")
            System.Windows.Forms.MessageBox.Show("Trader approved successfully!", "Success", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
            LoadUsers()
        End If
    End Sub

    Private Sub btnSuspend_Click(sender As Object, e As EventArgs)
        Dim user = GetSelectedUser()
        If user Is Nothing Then
            System.Windows.Forms.MessageBox.Show("Please select a user first.", "Validation", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If
        If user.Item3 = "Admin" Then
            System.Windows.Forms.MessageBox.Show("Admin accounts cannot be suspended from here.", "Restricted", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If
        If user.Item4 = "Suspended" Then
            System.Windows.Forms.MessageBox.Show("This user is already suspended.", "Info", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        End If
        Dim confirm As System.Windows.Forms.DialogResult = System.Windows.Forms.MessageBox.Show(
            "Suspend '" & user.Item2 & "'? They will be disconnected immediately and unable to log in.",
            "Confirm Suspend",
            System.Windows.Forms.MessageBoxButtons.YesNo,
            System.Windows.Forms.MessageBoxIcon.Warning)
        If confirm = System.Windows.Forms.DialogResult.Yes Then
            If DatabaseHelper.SuspendUser(user.Item1) Then
                DatabaseHelper.WriteLog("Warning", "Suspend", $"Admin suspended UserID {user.Item1} ({user.Item2}).")
                Try
                    frmServerMain.ForceDisconnect(user.Item1)
                Catch
                End Try
                System.Windows.Forms.MessageBox.Show("User suspended and disconnected.", "Success", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                LoadUsers()
            Else
                System.Windows.Forms.MessageBox.Show("Could not suspend user.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub btnReactivate_Click(sender As Object, e As EventArgs)
        Dim user = GetSelectedUser()
        If user Is Nothing Then
            System.Windows.Forms.MessageBox.Show("Please select a user first.", "Validation", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If
        If user.Item4 = "Active" Then
            System.Windows.Forms.MessageBox.Show("This user is already active.", "Info", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        End If
        If DatabaseHelper.ReactivateUser(user.Item1) Then
            DatabaseHelper.WriteLog("Info", "Reactivate", $"Admin reactivated UserID {user.Item1} ({user.Item2}).")
            System.Windows.Forms.MessageBox.Show("User reactivated successfully!", "Success", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
            LoadUsers()
        Else
            System.Windows.Forms.MessageBox.Show("Could not reactivate user.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnResetPassword_Click(sender As Object, e As EventArgs)
        Dim user = GetSelectedUser()
        If user Is Nothing Then
            System.Windows.Forms.MessageBox.Show("Please select a user first.", "Validation", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If
        If user.Item3 = "Admin" Then
            System.Windows.Forms.MessageBox.Show("Admin passwords must be changed from the Admin settings screen.", "Restricted", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim confirm As System.Windows.Forms.DialogResult = System.Windows.Forms.MessageBox.Show(
            "Reset password for '" & user.Item2 & "'?" & vbCrLf & vbCrLf &
            "A temporary password will be generated. The user will be disconnected immediately and will be required to set their own new password on first login.",
            "Confirm Password Reset",
            System.Windows.Forms.MessageBoxButtons.YesNo,
            System.Windows.Forms.MessageBoxIcon.Warning)

        If confirm = System.Windows.Forms.DialogResult.Yes Then
            Dim tempPass As String = DatabaseHelper.ResetPasswordToTemporary(user.Item1)
            If Not String.IsNullOrEmpty(tempPass) Then
                DatabaseHelper.WriteLog("Warning", "ResetPassword", $"Admin reset password for UserID {user.Item1} ({user.Item2}).")
                Try
                    frmServerMain.ForceDisconnect(user.Item1)
                Catch
                End Try

                Dim msg As String = "Password reset for: " & user.Item2 & vbCrLf & vbCrLf &
                                    "TEMPORARY PASSWORD:" & vbCrLf &
                                    tempPass & vbCrLf & vbCrLf &
                                    "Share this with the user out-of-band (verbally or via trusted channel)." & vbCrLf &
                                    "They will be required to set their own password on first login."
                System.Windows.Forms.MessageBox.Show(msg, "Temporary Password Generated", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)

                LoadUsers()
            Else
                System.Windows.Forms.MessageBox.Show("Could not reset password.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
            End If
        End If
    End Sub
End Class