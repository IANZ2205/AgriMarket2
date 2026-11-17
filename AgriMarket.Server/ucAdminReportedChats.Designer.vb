Imports System.Data
Imports System.Drawing

Public Class ucAdminReportedChats
    Private lblTitle As System.Windows.Forms.Label
    Private lblSubtitle As System.Windows.Forms.Label
    Private dgvReports As System.Windows.Forms.DataGridView
    Private btnRefresh As System.Windows.Forms.Button
    Private btnDismiss As System.Windows.Forms.Button
    Private btnWarn As System.Windows.Forms.Button
    Private btnSuspend As System.Windows.Forms.Button
    Private lblCounter As System.Windows.Forms.Label

    Private Sub ucAdminReportedChats_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildUI()
        LoadReports()
    End Sub

    Private Sub BuildUI()
        lblTitle = New System.Windows.Forms.Label()
        lblTitle.AutoSize = True
        lblTitle.Font = New System.Drawing.Font("Segoe UI", 24, System.Drawing.FontStyle.Bold)
        lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        lblTitle.Location = New System.Drawing.Point(30, 25)
        lblTitle.Text = "Reported Chats"
        Me.Controls.Add(lblTitle)

        lblSubtitle = New System.Windows.Forms.Label()
        lblSubtitle.AutoSize = True
        lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 11)
        lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblSubtitle.Location = New System.Drawing.Point(35, 72)
        lblSubtitle.Text = "User-submitted reports about inappropriate chat behavior"
        Me.Controls.Add(lblSubtitle)

        btnRefresh = New System.Windows.Forms.Button()
        btnRefresh.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnRefresh.FlatAppearance.BorderSize = 0
        btnRefresh.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnRefresh.ForeColor = System.Drawing.Color.White
        btnRefresh.Location = New System.Drawing.Point(30, 110)
        btnRefresh.Size = New System.Drawing.Size(110, 32)
        btnRefresh.Text = "Refresh"
        btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnRefresh.Click, AddressOf btnRefresh_Click
        Me.Controls.Add(btnRefresh)

        btnDismiss = New System.Windows.Forms.Button()
        btnDismiss.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        btnDismiss.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnDismiss.FlatAppearance.BorderSize = 0
        btnDismiss.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnDismiss.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        btnDismiss.Location = New System.Drawing.Point(150, 110)
        btnDismiss.Size = New System.Drawing.Size(130, 32)
        btnDismiss.Text = "Dismiss Report"
        btnDismiss.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnDismiss.Click, AddressOf btnDismiss_Click
        Me.Controls.Add(btnDismiss)

        btnWarn = New System.Windows.Forms.Button()
        btnWarn.BackColor = System.Drawing.Color.FromArgb(217, 164, 65)
        btnWarn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnWarn.FlatAppearance.BorderSize = 0
        btnWarn.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnWarn.ForeColor = System.Drawing.Color.White
        btnWarn.Location = New System.Drawing.Point(290, 110)
        btnWarn.Size = New System.Drawing.Size(130, 32)
        btnWarn.Text = "Warn User"
        btnWarn.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnWarn.Click, AddressOf btnWarn_Click
        Me.Controls.Add(btnWarn)

        btnSuspend = New System.Windows.Forms.Button()
        btnSuspend.BackColor = System.Drawing.Color.FromArgb(179, 38, 30)
        btnSuspend.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnSuspend.FlatAppearance.BorderSize = 0
        btnSuspend.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnSuspend.ForeColor = System.Drawing.Color.White
        btnSuspend.Location = New System.Drawing.Point(430, 110)
        btnSuspend.Size = New System.Drawing.Size(160, 32)
        btnSuspend.Text = "Suspend User"
        btnSuspend.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnSuspend.Click, AddressOf btnSuspend_Click
        Me.Controls.Add(btnSuspend)

        lblCounter = New System.Windows.Forms.Label()
        lblCounter.AutoSize = True
        lblCounter.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Italic)
        lblCounter.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblCounter.Location = New System.Drawing.Point(620, 118)
        lblCounter.Text = "0 report(s)"
        Me.Controls.Add(lblCounter)

        dgvReports = New System.Windows.Forms.DataGridView()
        dgvReports.BackgroundColor = System.Drawing.Color.White
        dgvReports.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        dgvReports.Location = New System.Drawing.Point(30, 160)
        dgvReports.Size = New System.Drawing.Size(1120, 480)
        dgvReports.AllowUserToAddRows = False
        dgvReports.ReadOnly = True
        dgvReports.RowHeadersVisible = False
        dgvReports.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        dgvReports.MultiSelect = False
        dgvReports.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        dgvReports.GridColor = System.Drawing.Color.FromArgb(230, 230, 230)
        dgvReports.EnableHeadersVisualStyles = False
        dgvReports.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        dgvReports.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        dgvReports.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        dgvReports.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
        dgvReports.ColumnHeadersHeight = 36
        dgvReports.RowTemplate.Height = 38
        dgvReports.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10)
        dgvReports.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        dgvReports.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        dgvReports.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.Controls.Add(dgvReports)
    End Sub

    Private Sub LoadReports()
        Try
            Dim dt As DataTable = DatabaseHelper.GetChatReports()
            dgvReports.DataSource = dt

            If dgvReports.Columns.Contains("ReportID") Then dgvReports.Columns("ReportID").Width = 80
            If dgvReports.Columns.Contains("ConvID") Then dgvReports.Columns("ConvID").Visible = False
            If dgvReports.Columns.Contains("ReporterName") Then dgvReports.Columns("ReporterName").Width = 160
            If dgvReports.Columns.Contains("ReportedName") Then dgvReports.Columns("ReportedName").Width = 160
            If dgvReports.Columns.Contains("ReportedUserID") Then dgvReports.Columns("ReportedUserID").Visible = False
            If dgvReports.Columns.Contains("Reason") Then dgvReports.Columns("Reason").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If dgvReports.Columns.Contains("Status") Then dgvReports.Columns("Status").Width = 100
            If dgvReports.Columns.Contains("CreatedAt") Then dgvReports.Columns("CreatedAt").Width = 160

            lblCounter.Text = dt.Rows.Count.ToString() & " report(s)"
        Catch ex As Exception
            MessageBox.Show("Error loading reports: " & ex.Message)
        End Try
    End Sub

    Private Function GetSelectedReport() As Tuple(Of Integer, Integer, String, String)
        If dgvReports.SelectedRows.Count = 0 Then Return Nothing
        Try
            Dim row As DataGridViewRow = dgvReports.SelectedRows(0)
            Dim reportId As Integer = Convert.ToInt32(row.Cells("ReportID").Value)
            Dim reportedUserId As Integer = Convert.ToInt32(row.Cells("ReportedUserID").Value)
            Dim reportedName As String = row.Cells("ReportedName").Value.ToString()
            Dim status As String = row.Cells("Status").Value.ToString()
            Return New Tuple(Of Integer, Integer, String, String)(reportId, reportedUserId, reportedName, status)
        Catch
            Return Nothing
        End Try
    End Function

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        LoadReports()
    End Sub

    Private Sub btnDismiss_Click(sender As Object, e As EventArgs)
        Dim report = GetSelectedReport()
        If report Is Nothing Then
            MessageBox.Show("Please select a report first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If DatabaseHelper.UpdateReportStatus(report.Item1, "Dismissed", 1, "No action taken.") Then
            DatabaseHelper.WriteLog("Info", "ReportDismissed", $"Admin dismissed ReportID {report.Item1}.")
            MessageBox.Show("Report dismissed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadReports()
        End If
    End Sub

    Private Sub btnWarn_Click(sender As Object, e As EventArgs)
        Dim report = GetSelectedReport()
        If report Is Nothing Then
            MessageBox.Show("Please select a report first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim note As String = InputBox("Warning message to send to the reported user:", "Warn User", "Please review our community guidelines. Continued violations may result in account suspension.")
        If String.IsNullOrWhiteSpace(note) Then Exit Sub

        DatabaseHelper.AddNotification(report.Item2, "SYSTEM", "Warning from Administrator", note)
        DatabaseHelper.UpdateReportStatus(report.Item1, "Reviewed", 1, "Warning issued: " & note)
        DatabaseHelper.WriteLog("Warning", "ReportWarning", $"Admin warned UserID {report.Item2} ({report.Item3}). ReportID {report.Item1}.")

        MessageBox.Show("Warning sent to " & report.Item3 & ".", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        LoadReports()
    End Sub

    Private Sub btnSuspend_Click(sender As Object, e As EventArgs)
        Dim report = GetSelectedReport()
        If report Is Nothing Then
            MessageBox.Show("Please select a report first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim confirm As DialogResult = MessageBox.Show(
            "Suspend '" & report.Item3 & "'? They will be disconnected immediately and unable to log in.",
            "Confirm Suspend",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)
        If confirm <> DialogResult.Yes Then Exit Sub

        If DatabaseHelper.SuspendUser(report.Item2) Then
            DatabaseHelper.UpdateReportStatus(report.Item1, "Reviewed", 1, "Account suspended.")
            DatabaseHelper.WriteLog("Warning", "ReportSuspend", $"Admin suspended UserID {report.Item2} ({report.Item3}). ReportID {report.Item1}.")
            Try
                frmServerMain.ForceDisconnect(report.Item2)
            Catch
            End Try
            MessageBox.Show(report.Item3 & " has been suspended.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadReports()
        Else
            MessageBox.Show("Could not suspend user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class