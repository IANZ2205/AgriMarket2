Imports System.Data
Imports System.Drawing

Public Class ucAdminConnectedUsers
    Private lblTitle As System.Windows.Forms.Label
    Private lblSubtitle As System.Windows.Forms.Label
    Private lblCounter As System.Windows.Forms.Label
    Private dgvUsers As System.Windows.Forms.DataGridView
    Private btnRefresh As System.Windows.Forms.Button
    Private btnDisconnect As System.Windows.Forms.Button
    Private timer As System.Windows.Forms.Timer

    Private Sub ucAdminConnectedUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildUI()
        LoadConnected()

        timer = New System.Windows.Forms.Timer()
        timer.Interval = 3000
        AddHandler timer.Tick, AddressOf Timer_Tick
        timer.Start()
    End Sub

    Private Sub ucAdminConnectedUsers_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        If timer IsNot Nothing Then timer.Stop()
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs)
        LoadConnected()
    End Sub

    Private Sub BuildUI()
        lblTitle = New System.Windows.Forms.Label()
        lblTitle.AutoSize = True
        lblTitle.Font = New System.Drawing.Font("Segoe UI", 24, System.Drawing.FontStyle.Bold)
        lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        lblTitle.Location = New System.Drawing.Point(30, 25)
        lblTitle.Text = "Connected Users"
        Me.Controls.Add(lblTitle)

        lblSubtitle = New System.Windows.Forms.Label()
        lblSubtitle.AutoSize = True
        lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 11)
        lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblSubtitle.Location = New System.Drawing.Point(35, 72)
        lblSubtitle.Text = "Who is online right now (auto-refreshes every 3 seconds)"
        Me.Controls.Add(lblSubtitle)

        btnRefresh = New System.Windows.Forms.Button()
        btnRefresh.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnRefresh.FlatAppearance.BorderSize = 0
        btnRefresh.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnRefresh.ForeColor = System.Drawing.Color.White
        btnRefresh.Location = New System.Drawing.Point(30, 110)
        btnRefresh.Size = New System.Drawing.Size(120, 32)
        btnRefresh.Text = "Refresh Now"
        btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnRefresh.Click, AddressOf btnRefresh_Click
        Me.Controls.Add(btnRefresh)

        btnDisconnect = New System.Windows.Forms.Button()
        btnDisconnect.BackColor = System.Drawing.Color.FromArgb(179, 38, 30)
        btnDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnDisconnect.FlatAppearance.BorderSize = 0
        btnDisconnect.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnDisconnect.ForeColor = System.Drawing.Color.White
        btnDisconnect.Location = New System.Drawing.Point(160, 110)
        btnDisconnect.Size = New System.Drawing.Size(180, 32)
        btnDisconnect.Text = "Disconnect Selected"
        btnDisconnect.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnDisconnect.Click, AddressOf btnDisconnect_Click
        Me.Controls.Add(btnDisconnect)

        lblCounter = New System.Windows.Forms.Label()
        lblCounter.AutoSize = True
        lblCounter.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Italic)
        lblCounter.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblCounter.Location = New System.Drawing.Point(360, 118)
        lblCounter.Text = "0 users online"
        Me.Controls.Add(lblCounter)

        dgvUsers = New System.Windows.Forms.DataGridView()
        dgvUsers.BackgroundColor = System.Drawing.Color.White
        dgvUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        dgvUsers.Location = New System.Drawing.Point(30, 160)
        dgvUsers.Size = New System.Drawing.Size(1120, 470)
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
        dgvUsers.ColumnHeadersHeight = 38
        dgvUsers.RowTemplate.Height = 40
        dgvUsers.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10)
        dgvUsers.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        dgvUsers.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        dgvUsers.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.Controls.Add(dgvUsers)
    End Sub

    Private Sub LoadConnected()
        Try
            Dim lookup As New Dictionary(Of Integer, DataRow)
            Try
                Dim allUsers As DataTable = DatabaseHelper.GetAllUsers()
                For Each row As DataRow In allUsers.Rows
                    lookup(Convert.ToInt32(row("UserID"))) = row
                Next
            Catch
            End Try

            Dim dt As New DataTable()
            dt.Columns.Add("UserID", GetType(Integer))
            dt.Columns.Add("Name")
            dt.Columns.Add("Role")
            dt.Columns.Add("IP Address")
            dt.Columns.Add("Logged In")
            dt.Columns.Add("Time Online")

            Dim now As DateTime = DateTime.Now

            For Each kvp In frmServerMain.ClientSessions
                Dim userId As Integer = kvp.Key
                Dim session As ClientSession = kvp.Value

                Dim name As String = "(unknown)"
                Dim role As String = ""
                If lookup.ContainsKey(userId) Then
                    name = lookup(userId)("Name").ToString()
                    role = lookup(userId)("Role").ToString()
                End If

                Dim online As TimeSpan = now - session.LoginTime
                Dim onlineStr As String
                If online.TotalHours >= 1 Then
                    onlineStr = CInt(online.TotalHours).ToString() & "h " & online.Minutes.ToString() & "m"
                ElseIf online.TotalMinutes >= 1 Then
                    onlineStr = online.Minutes.ToString() & "m " & online.Seconds.ToString() & "s"
                Else
                    onlineStr = online.Seconds.ToString() & "s"
                End If

                dt.Rows.Add(userId, name, role, session.IPAddress, session.LoginTime.ToString("HH:mm:ss"), onlineStr)
            Next

            Dim previouslySelected As Integer = -1
            If dgvUsers.SelectedRows.Count > 0 Then
                Try
                    previouslySelected = Convert.ToInt32(dgvUsers.SelectedRows(0).Cells("UserID").Value)
                Catch
                End Try
            End If

            dgvUsers.DataSource = dt

            If dgvUsers.Columns.Contains("UserID") Then dgvUsers.Columns("UserID").Visible = False
            If dgvUsers.Columns.Contains("Name") Then dgvUsers.Columns("Name").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If dgvUsers.Columns.Contains("Role") Then dgvUsers.Columns("Role").Width = 120
            If dgvUsers.Columns.Contains("IP Address") Then dgvUsers.Columns("IP Address").Width = 180
            If dgvUsers.Columns.Contains("Logged In") Then dgvUsers.Columns("Logged In").Width = 120
            If dgvUsers.Columns.Contains("Time Online") Then dgvUsers.Columns("Time Online").Width = 130

            lblCounter.Text = dt.Rows.Count.ToString() & " user(s) online"

            If previouslySelected >= 0 Then
                For Each row As DataGridViewRow In dgvUsers.Rows
                    Try
                        If Convert.ToInt32(row.Cells("UserID").Value) = previouslySelected Then
                            row.Selected = True
                            Exit For
                        End If
                    Catch
                    End Try
                Next
            End If
        Catch
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        LoadConnected()
    End Sub

    Private Sub btnDisconnect_Click(sender As Object, e As EventArgs)
        If dgvUsers.SelectedRows.Count = 0 Then
            System.Windows.Forms.MessageBox.Show("Please select a user first.", "Validation", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim userId As Integer = Convert.ToInt32(dgvUsers.SelectedRows(0).Cells("UserID").Value)
        Dim userName As String = dgvUsers.SelectedRows(0).Cells("Name").Value.ToString()

        Dim confirm As System.Windows.Forms.DialogResult = System.Windows.Forms.MessageBox.Show(
            "Disconnect '" & userName & "' immediately?",
            "Confirm Disconnect",
            System.Windows.Forms.MessageBoxButtons.YesNo,
            System.Windows.Forms.MessageBoxIcon.Warning)

        If confirm = System.Windows.Forms.DialogResult.Yes Then
            frmServerMain.ForceDisconnect(userId)
            DatabaseHelper.WriteLog("Warning", "Disconnect", $"Admin disconnected UserID {userId} ({userName}).")
            LoadConnected()
        End If
    End Sub
End Class