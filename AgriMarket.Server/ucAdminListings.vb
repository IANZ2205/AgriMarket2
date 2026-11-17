Imports System.Data
Imports System.Drawing

Public Class ucAdminListings
    Private lblTitle As System.Windows.Forms.Label
    Private lblSubtitle As System.Windows.Forms.Label
    Private txtSearch As System.Windows.Forms.TextBox
    Private cmbFilter As System.Windows.Forms.ComboBox
    Private dgvListings As System.Windows.Forms.DataGridView
    Private btnRefresh As System.Windows.Forms.Button
    Private lblCounter As System.Windows.Forms.Label

    Private Sub ucAdminListings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildUI()
        LoadListings()
    End Sub

    Private Sub BuildUI()
        lblTitle = New System.Windows.Forms.Label()
        lblTitle.AutoSize = True
        lblTitle.Font = New System.Drawing.Font("Segoe UI", 24, System.Drawing.FontStyle.Bold)
        lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        lblTitle.Location = New System.Drawing.Point(30, 25)
        lblTitle.Text = "All Listings"
        Me.Controls.Add(lblTitle)

        lblSubtitle = New System.Windows.Forms.Label()
        lblSubtitle.AutoSize = True
        lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 11)
        lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblSubtitle.Location = New System.Drawing.Point(35, 72)
        lblSubtitle.Text = "Every product listed across all traders"
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
        cmbFilter.Items.AddRange(New Object() {"All", "Active", "Hidden", "Removed"})
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
        lblCounter.Text = "0 listings"
        Me.Controls.Add(lblCounter)

        dgvListings = New System.Windows.Forms.DataGridView()
        dgvListings.BackgroundColor = System.Drawing.Color.White
        dgvListings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        dgvListings.Location = New System.Drawing.Point(30, 160)
        dgvListings.Size = New System.Drawing.Size(1120, 480)
        dgvListings.AllowUserToAddRows = False
        dgvListings.ReadOnly = True
        dgvListings.RowHeadersVisible = False
        dgvListings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        dgvListings.MultiSelect = False
        dgvListings.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        dgvListings.GridColor = System.Drawing.Color.FromArgb(230, 230, 230)
        dgvListings.EnableHeadersVisualStyles = False
        dgvListings.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        dgvListings.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        dgvListings.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        dgvListings.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
        dgvListings.ColumnHeadersHeight = 36
        dgvListings.RowTemplate.Height = 38
        dgvListings.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10)
        dgvListings.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        dgvListings.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        dgvListings.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.Controls.Add(dgvListings)
    End Sub

    Private Sub LoadListings()
        Try
            Dim allListings As System.Data.DataTable = DatabaseHelper.GetAllListingsAdmin()
            Dim filtered As System.Data.DataTable = allListings.Clone()

            Dim filterStatus As String = "All"
            If cmbFilter.SelectedItem IsNot Nothing Then filterStatus = cmbFilter.SelectedItem.ToString()
            Dim searchText As String = txtSearch.Text.Trim().ToLower()

            For Each row As System.Data.DataRow In allListings.Rows
                Dim statusMatches As Boolean = (filterStatus = "All" OrElse row("Status").ToString() = filterStatus)
                Dim searchMatches As Boolean = String.IsNullOrEmpty(searchText) OrElse
                                                row("Produce").ToString().ToLower().Contains(searchText) OrElse
                                                row("Trader").ToString().ToLower().Contains(searchText) OrElse
                                                row("Category").ToString().ToLower().Contains(searchText)
                If statusMatches AndAlso searchMatches Then
                    filtered.ImportRow(row)
                End If
            Next

            dgvListings.DataSource = filtered
            If dgvListings.Columns.Contains("ItemID") Then dgvListings.Columns("ItemID").Visible = False
            If dgvListings.Columns.Contains("Produce") Then dgvListings.Columns("Produce").AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            If dgvListings.Columns.Contains("Trader") Then dgvListings.Columns("Trader").AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            If dgvListings.Columns.Contains("Category") Then dgvListings.Columns("Category").Width = 140
            If dgvListings.Columns.Contains("Price") Then dgvListings.Columns("Price").Width = 120
            If dgvListings.Columns.Contains("Status") Then dgvListings.Columns("Status").Width = 120

            lblCounter.Text = filtered.Rows.Count.ToString() & " listing(s) shown"
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Error loading listings: " & ex.Message)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs)
        LoadListings()
    End Sub

    Private Sub cmbFilter_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadListings()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        LoadListings()
    End Sub
End Class