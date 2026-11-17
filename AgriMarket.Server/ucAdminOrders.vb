Imports System.Data
Imports System.Drawing

Public Class ucAdminOrders
    Private lblTitle As System.Windows.Forms.Label
    Private lblSubtitle As System.Windows.Forms.Label
    Private txtSearch As System.Windows.Forms.TextBox
    Private cmbFilter As System.Windows.Forms.ComboBox
    Private dgvOrders As System.Windows.Forms.DataGridView
    Private btnRefresh As System.Windows.Forms.Button
    Private lblCounter As System.Windows.Forms.Label
    Private lblTotalValue As System.Windows.Forms.Label
    Private lblTotalCaption As System.Windows.Forms.Label

    Private Sub ucAdminOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildUI()
        LoadOrders()
    End Sub

    Private Sub BuildUI()
        lblTitle = New System.Windows.Forms.Label()
        lblTitle.AutoSize = True
        lblTitle.Font = New System.Drawing.Font("Segoe UI", 24, System.Drawing.FontStyle.Bold)
        lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        lblTitle.Location = New System.Drawing.Point(30, 25)
        lblTitle.Text = "All Orders"
        Me.Controls.Add(lblTitle)

        lblSubtitle = New System.Windows.Forms.Label()
        lblSubtitle.AutoSize = True
        lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 11)
        lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblSubtitle.Location = New System.Drawing.Point(35, 72)
        lblSubtitle.Text = "Every order placed on AgriMarket, across all traders and buyers"
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
        cmbFilter.Items.AddRange(New Object() {"All", "Pending", "Confirmed", "Completed", "Rejected", "Cancelled"})
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
        lblCounter.Text = "0 orders"
        Me.Controls.Add(lblCounter)

        lblTotalCaption = New System.Windows.Forms.Label()
        lblTotalCaption.AutoSize = True
        lblTotalCaption.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        lblTotalCaption.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblTotalCaption.Location = New System.Drawing.Point(920, 114)
        lblTotalCaption.Text = "Total value:"
        Me.Controls.Add(lblTotalCaption)

        lblTotalValue = New System.Windows.Forms.Label()
        lblTotalValue.AutoSize = True
        lblTotalValue.Font = New System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold)
        lblTotalValue.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        lblTotalValue.Location = New System.Drawing.Point(1020, 110)
        lblTotalValue.Text = "UGX 0"
        Me.Controls.Add(lblTotalValue)

        dgvOrders = New System.Windows.Forms.DataGridView()
        dgvOrders.BackgroundColor = System.Drawing.Color.White
        dgvOrders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        dgvOrders.Location = New System.Drawing.Point(30, 160)
        dgvOrders.Size = New System.Drawing.Size(1120, 480)
        dgvOrders.AllowUserToAddRows = False
        dgvOrders.ReadOnly = True
        dgvOrders.RowHeadersVisible = False
        dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        dgvOrders.MultiSelect = False
        dgvOrders.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        dgvOrders.GridColor = System.Drawing.Color.FromArgb(230, 230, 230)
        dgvOrders.EnableHeadersVisualStyles = False
        dgvOrders.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        dgvOrders.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        dgvOrders.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
        dgvOrders.ColumnHeadersHeight = 36
        dgvOrders.RowTemplate.Height = 38
        dgvOrders.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10)
        dgvOrders.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        dgvOrders.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        dgvOrders.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.Controls.Add(dgvOrders)
    End Sub

    Private Sub LoadOrders()
        Try
            Dim allOrders As System.Data.DataTable = DatabaseHelper.GetAllOrdersAdmin()
            Dim filtered As System.Data.DataTable = allOrders.Clone()

            Dim filterStatus As String = "All"
            If cmbFilter.SelectedItem IsNot Nothing Then filterStatus = cmbFilter.SelectedItem.ToString()
            Dim searchText As String = txtSearch.Text.Trim().ToLower()

            Dim totalValue As Decimal = 0

            For Each row As System.Data.DataRow In allOrders.Rows
                Dim statusMatches As Boolean = (filterStatus = "All" OrElse row("Status").ToString() = filterStatus)
                Dim searchMatches As Boolean = String.IsNullOrEmpty(searchText) OrElse
                                                row("Produce").ToString().ToLower().Contains(searchText) OrElse
                                                row("Buyer").ToString().ToLower().Contains(searchText) OrElse
                                                row("Trader").ToString().ToLower().Contains(searchText)
                If statusMatches AndAlso searchMatches Then
                    filtered.ImportRow(row)
                    Try
                        totalValue += Convert.ToDecimal(row("Total"))
                    Catch
                    End Try
                End If
            Next

            dgvOrders.DataSource = filtered

            If dgvOrders.Columns.Contains("Order No") Then dgvOrders.Columns("Order No").Width = 90
            If dgvOrders.Columns.Contains("Produce") Then dgvOrders.Columns("Produce").AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            If dgvOrders.Columns.Contains("Buyer") Then dgvOrders.Columns("Buyer").AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            If dgvOrders.Columns.Contains("Trader") Then dgvOrders.Columns("Trader").AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            If dgvOrders.Columns.Contains("Qty") Then dgvOrders.Columns("Qty").Width = 70
            If dgvOrders.Columns.Contains("Total") Then dgvOrders.Columns("Total").Width = 120
            If dgvOrders.Columns.Contains("Status") Then dgvOrders.Columns("Status").Width = 110
            If dgvOrders.Columns.Contains("Date") Then dgvOrders.Columns("Date").Width = 150

            lblCounter.Text = filtered.Rows.Count.ToString() & " order(s) shown"
            lblTotalValue.Text = "UGX " & totalValue.ToString("N0")
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Error loading orders: " & ex.Message)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs)
        LoadOrders()
    End Sub

    Private Sub cmbFilter_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadOrders()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        LoadOrders()
    End Sub
End Class