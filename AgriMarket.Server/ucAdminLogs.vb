Imports System.Data
Imports System.Drawing

Public Class ucAdminLogs
    Private lblTitle As System.Windows.Forms.Label
    Private lblSubtitle As System.Windows.Forms.Label
    Private txtSearch As System.Windows.Forms.TextBox
    Private cmbLevel As System.Windows.Forms.ComboBox
    Private btnRefresh As System.Windows.Forms.Button
    Private btnClear As System.Windows.Forms.Button
    Private dgvLogs As System.Windows.Forms.DataGridView
    Private lblCounter As System.Windows.Forms.Label

    Private Sub ucAdminLogs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildUI()
        LoadLogs()
    End Sub

    Private Sub BuildUI()
        lblTitle = New System.Windows.Forms.Label()
        lblTitle.AutoSize = True
        lblTitle.Font = New System.Drawing.Font("Segoe UI", 24, System.Drawing.FontStyle.Bold)
        lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        lblTitle.Location = New System.Drawing.Point(30, 25)
        lblTitle.Text = "Server Logs"
        Me.Controls.Add(lblTitle)

        lblSubtitle = New System.Windows.Forms.Label()
        lblSubtitle.AutoSize = True
        lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 11)
        lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblSubtitle.Location = New System.Drawing.Point(35, 72)
        lblSubtitle.Text = "Everything that has happened on this server"
        Me.Controls.Add(lblSubtitle)

        txtSearch = New System.Windows.Forms.TextBox()
        txtSearch.Font = New System.Drawing.Font("Segoe UI", 11)
        txtSearch.Location = New System.Drawing.Point(30, 110)
        txtSearch.Size = New System.Drawing.Size(400, 27)
        txtSearch.Text = ""
        AddHandler txtSearch.TextChanged, AddressOf txtSearch_TextChanged
        Me.Controls.Add(txtSearch)

        cmbLevel = New System.Windows.Forms.ComboBox()
        cmbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        cmbLevel.Font = New System.Drawing.Font("Segoe UI", 11)
        cmbLevel.Location = New System.Drawing.Point(450, 110)
        cmbLevel.Size = New System.Drawing.Size(160, 27)
        cmbLevel.Items.AddRange(New Object() {"All Levels", "Info", "Warning", "Error"})
        cmbLevel.SelectedIndex = 0
        AddHandler cmbLevel.SelectedIndexChanged, AddressOf cmbLevel_SelectedIndexChanged
        Me.Controls.Add(cmbLevel)

        btnRefresh = New System.Windows.Forms.Button()
        btnRefresh.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnRefresh.FlatAppearance.BorderSize = 0
        btnRefresh.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnRefresh.ForeColor = System.Drawing.Color.White
        btnRefresh.Location = New System.Drawing.Point(630, 106)
        btnRefresh.Size = New System.Drawing.Size(110, 32)
        btnRefresh.Text = "Refresh"
        btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnRefresh.Click, AddressOf btnRefresh_Click
        Me.Controls.Add(btnRefresh)

        btnClear = New System.Windows.Forms.Button()
        btnClear.BackColor = System.Drawing.Color.FromArgb(179, 38, 30)
        btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnClear.FlatAppearance.BorderSize = 0
        btnClear.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnClear.ForeColor = System.Drawing.Color.White
        btnClear.Location = New System.Drawing.Point(750, 106)
        btnClear.Size = New System.Drawing.Size(140, 32)
        btnClear.Text = "Clear Old Logs"
        btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnClear.Click, AddressOf btnClear_Click
        Me.Controls.Add(btnClear)

        lblCounter = New System.Windows.Forms.Label()
        lblCounter.AutoSize = True
        lblCounter.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Italic)
        lblCounter.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblCounter.Location = New System.Drawing.Point(910, 114)
        lblCounter.Text = "0 log entries"
        Me.Controls.Add(lblCounter)

        dgvLogs = New System.Windows.Forms.DataGridView()
        dgvLogs.BackgroundColor = System.Drawing.Color.White
        dgvLogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        dgvLogs.Location = New System.Drawing.Point(30, 160)
        dgvLogs.Size = New System.Drawing.Size(1120, 480)
        dgvLogs.AllowUserToAddRows = False
        dgvLogs.ReadOnly = True
        dgvLogs.RowHeadersVisible = False
        dgvLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        dgvLogs.MultiSelect = False
        dgvLogs.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        dgvLogs.GridColor = System.Drawing.Color.FromArgb(230, 230, 230)
        dgvLogs.EnableHeadersVisualStyles = False
        dgvLogs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        dgvLogs.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        dgvLogs.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        dgvLogs.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
        dgvLogs.ColumnHeadersHeight = 36
        dgvLogs.RowTemplate.Height = 34
        dgvLogs.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9)
        dgvLogs.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        dgvLogs.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        dgvLogs.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        AddHandler dgvLogs.CellFormatting, AddressOf dgvLogs_CellFormatting
        Me.Controls.Add(dgvLogs)
    End Sub

    Private Sub LoadLogs()
        Try
            Dim allLogs As System.Data.DataTable = DatabaseHelper.GetLogs()
            Dim filtered As System.Data.DataTable = allLogs.Clone()

            Dim filterLevel As String = "All Levels"
            If cmbLevel.SelectedItem IsNot Nothing Then filterLevel = cmbLevel.SelectedItem.ToString()
            Dim searchText As String = txtSearch.Text.Trim().ToLower()

            For Each row As System.Data.DataRow In allLogs.Rows
                Dim levelMatches As Boolean = (filterLevel = "All Levels" OrElse row("Level").ToString() = filterLevel)
                Dim searchMatches As Boolean = String.IsNullOrEmpty(searchText) OrElse
                                                row("Type").ToString().ToLower().Contains(searchText) OrElse
                                                row("Details").ToString().ToLower().Contains(searchText)
                If levelMatches AndAlso searchMatches Then
                    filtered.ImportRow(row)
                End If
            Next

            dgvLogs.DataSource = filtered

            If dgvLogs.Columns.Contains("ID") Then dgvLogs.Columns("ID").Visible = False
            If dgvLogs.Columns.Contains("Time") Then dgvLogs.Columns("Time").Width = 160
            If dgvLogs.Columns.Contains("Level") Then dgvLogs.Columns("Level").Width = 100
            If dgvLogs.Columns.Contains("Type") Then dgvLogs.Columns("Type").Width = 140
            If dgvLogs.Columns.Contains("Details") Then dgvLogs.Columns("Details").AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill

            lblCounter.Text = filtered.Rows.Count.ToString() & " log entry(ies)"
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Error loading logs: " & ex.Message)
        End Try
    End Sub

    Private Sub dgvLogs_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs)
        If e.ColumnIndex < 0 OrElse e.Value Is Nothing Then Return
        Try
            Dim colName As String = dgvLogs.Columns(e.ColumnIndex).Name
            If colName = "Level" Then
                Select Case e.Value.ToString()
                    Case "Error"
                        e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(179, 38, 30)
                        e.CellStyle.Font = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
                    Case "Warning"
                        e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(217, 164, 65)
                        e.CellStyle.Font = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
                    Case "Info"
                        e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(27, 122, 80)
                        e.CellStyle.Font = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
                End Select
            End If
        Catch
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs)
        LoadLogs()
    End Sub

    Private Sub cmbLevel_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadLogs()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        LoadLogs()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs)
        Dim confirm As System.Windows.Forms.DialogResult = System.Windows.Forms.MessageBox.Show(
            "Are you sure you want to clear all old logs? This cannot be undone.",
            "Confirm Clear Logs",
            System.Windows.Forms.MessageBoxButtons.YesNo,
            System.Windows.Forms.MessageBoxIcon.Warning)

        If confirm = System.Windows.Forms.DialogResult.Yes Then
            System.Windows.Forms.MessageBox.Show("Log clearing will be implemented in a future update. For now, all logs are preserved for audit purposes.", "Not Implemented", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
        End If
    End Sub
End Class