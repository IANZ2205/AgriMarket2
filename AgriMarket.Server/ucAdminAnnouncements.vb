Imports System.Data
Imports System.Drawing

Public Class ucAdminAnnouncements
    Private lblTitle As System.Windows.Forms.Label
    Private lblSubtitle As System.Windows.Forms.Label

    Private lblComposeTitle As System.Windows.Forms.Label
    Private txtTitle As System.Windows.Forms.TextBox
    Private lblBody As System.Windows.Forms.Label
    Private txtBody As System.Windows.Forms.TextBox
    Private lblAudience As System.Windows.Forms.Label
    Private cmbAudience As System.Windows.Forms.ComboBox
    Private btnSend As System.Windows.Forms.Button
    Private btnRefresh As System.Windows.Forms.Button

    Private lblHistory As System.Windows.Forms.Label
    Private dgvAnnouncements As System.Windows.Forms.DataGridView

    Private Sub ucAdminAnnouncements_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildUI()
        LoadHistory()
    End Sub

    Private Sub BuildUI()
        lblTitle = New System.Windows.Forms.Label()
        lblTitle.AutoSize = True
        lblTitle.Font = New System.Drawing.Font("Segoe UI", 24, System.Drawing.FontStyle.Bold)
        lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        lblTitle.Location = New System.Drawing.Point(30, 25)
        lblTitle.Text = "Announcements"
        Me.Controls.Add(lblTitle)

        lblSubtitle = New System.Windows.Forms.Label()
        lblSubtitle.AutoSize = True
        lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 11)
        lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblSubtitle.Location = New System.Drawing.Point(35, 72)
        lblSubtitle.Text = "Broadcast a message to all users, traders, or buyers"
        Me.Controls.Add(lblSubtitle)

        ' Compose panel
        Dim composePanel As New System.Windows.Forms.Panel()
        composePanel.BackColor = System.Drawing.Color.White
        composePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        composePanel.Location = New System.Drawing.Point(30, 110)
        composePanel.Size = New System.Drawing.Size(1120, 230)
        Me.Controls.Add(composePanel)

        lblComposeTitle = New System.Windows.Forms.Label()
        lblComposeTitle.AutoSize = True
        lblComposeTitle.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        lblComposeTitle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblComposeTitle.Location = New System.Drawing.Point(20, 15)
        lblComposeTitle.Text = "Title"
        composePanel.Controls.Add(lblComposeTitle)

        txtTitle = New System.Windows.Forms.TextBox()
        txtTitle.Font = New System.Drawing.Font("Segoe UI", 11)
        txtTitle.Location = New System.Drawing.Point(20, 38)
        txtTitle.Size = New System.Drawing.Size(600, 27)
        composePanel.Controls.Add(txtTitle)

        lblAudience = New System.Windows.Forms.Label()
        lblAudience.AutoSize = True
        lblAudience.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        lblAudience.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblAudience.Location = New System.Drawing.Point(640, 15)
        lblAudience.Text = "Audience"
        composePanel.Controls.Add(lblAudience)

        cmbAudience = New System.Windows.Forms.ComboBox()
        cmbAudience.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        cmbAudience.Font = New System.Drawing.Font("Segoe UI", 11)
        cmbAudience.Location = New System.Drawing.Point(640, 38)
        cmbAudience.Size = New System.Drawing.Size(200, 27)
        cmbAudience.Items.AddRange(New Object() {"All", "Traders", "Buyers"})
        cmbAudience.SelectedIndex = 0
        composePanel.Controls.Add(cmbAudience)

        lblBody = New System.Windows.Forms.Label()
        lblBody.AutoSize = True
        lblBody.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        lblBody.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblBody.Location = New System.Drawing.Point(20, 75)
        lblBody.Text = "Message Body"
        composePanel.Controls.Add(lblBody)

        txtBody = New System.Windows.Forms.TextBox()
        txtBody.Font = New System.Drawing.Font("Segoe UI", 11)
        txtBody.Location = New System.Drawing.Point(20, 98)
        txtBody.Size = New System.Drawing.Size(820, 60)
        txtBody.Multiline = True
        composePanel.Controls.Add(txtBody)

        btnSend = New System.Windows.Forms.Button()
        btnSend.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnSend.FlatAppearance.BorderSize = 0
        btnSend.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnSend.ForeColor = System.Drawing.Color.White
        btnSend.Location = New System.Drawing.Point(20, 175)
        btnSend.Size = New System.Drawing.Size(180, 38)
        btnSend.Text = "Broadcast Now"
        btnSend.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnSend.Click, AddressOf btnSend_Click
        composePanel.Controls.Add(btnSend)

        btnRefresh = New System.Windows.Forms.Button()
        btnRefresh.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnRefresh.FlatAppearance.BorderSize = 0
        btnRefresh.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnRefresh.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        btnRefresh.Location = New System.Drawing.Point(215, 175)
        btnRefresh.Size = New System.Drawing.Size(120, 38)
        btnRefresh.Text = "Refresh"
        btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnRefresh.Click, AddressOf btnRefresh_Click
        composePanel.Controls.Add(btnRefresh)

        ' History panel
        lblHistory = New System.Windows.Forms.Label()
        lblHistory.AutoSize = True
        lblHistory.Font = New System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold)
        lblHistory.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        lblHistory.Location = New System.Drawing.Point(30, 360)
        lblHistory.Text = "Past Announcements"
        Me.Controls.Add(lblHistory)

        dgvAnnouncements = New System.Windows.Forms.DataGridView()
        dgvAnnouncements.BackgroundColor = System.Drawing.Color.White
        dgvAnnouncements.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        dgvAnnouncements.Location = New System.Drawing.Point(30, 400)
        dgvAnnouncements.Size = New System.Drawing.Size(1120, 235)
        dgvAnnouncements.AllowUserToAddRows = False
        dgvAnnouncements.ReadOnly = True
        dgvAnnouncements.RowHeadersVisible = False
        dgvAnnouncements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        dgvAnnouncements.MultiSelect = False
        dgvAnnouncements.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        dgvAnnouncements.GridColor = System.Drawing.Color.FromArgb(230, 230, 230)
        dgvAnnouncements.EnableHeadersVisualStyles = False
        dgvAnnouncements.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        dgvAnnouncements.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        dgvAnnouncements.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        dgvAnnouncements.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
        dgvAnnouncements.ColumnHeadersHeight = 36
        dgvAnnouncements.RowTemplate.Height = 38
        dgvAnnouncements.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10)
        dgvAnnouncements.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        dgvAnnouncements.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        dgvAnnouncements.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.Controls.Add(dgvAnnouncements)
    End Sub

    Private Sub LoadHistory()
        Try
            Dim dt As System.Data.DataTable = DatabaseHelper.GetAnnouncements()
            dgvAnnouncements.DataSource = dt

            If dgvAnnouncements.Columns.Contains("ID") Then dgvAnnouncements.Columns("ID").Visible = False
            If dgvAnnouncements.Columns.Contains("Title") Then dgvAnnouncements.Columns("Title").Width = 220
            If dgvAnnouncements.Columns.Contains("Body") Then dgvAnnouncements.Columns("Body").AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            If dgvAnnouncements.Columns.Contains("Audience") Then dgvAnnouncements.Columns("Audience").Width = 100
            If dgvAnnouncements.Columns.Contains("Sent At") Then dgvAnnouncements.Columns("Sent At").Width = 160
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Error loading announcements: " & ex.Message)
        End Try
    End Sub

    Private Sub btnSend_Click(sender As Object, e As EventArgs)
        Dim title As String = txtTitle.Text.Trim()
        Dim body As String = txtBody.Text.Trim()
        Dim audience As String = "All"
        If cmbAudience.SelectedItem IsNot Nothing Then audience = cmbAudience.SelectedItem.ToString()

        If String.IsNullOrEmpty(title) OrElse String.IsNullOrEmpty(body) Then
            System.Windows.Forms.MessageBox.Show("Please fill in both the Title and the Message Body.", "Validation", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If

        If DatabaseHelper.SendAnnouncement(title, body, audience) Then
            System.Windows.Forms.MessageBox.Show("Announcement broadcasted to " & audience & " successfully!", "Success", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
            txtTitle.Clear()
            txtBody.Clear()
            LoadHistory()
        Else
            System.Windows.Forms.MessageBox.Show("Failed to send announcement.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        LoadHistory()
    End Sub
End Class