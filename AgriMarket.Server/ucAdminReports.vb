Imports System.Data
Imports System.Drawing
Imports System.Drawing.Printing

Public Class ucAdminReports
    Private lblTitle As System.Windows.Forms.Label
    Private lblSubtitle As System.Windows.Forms.Label
    Private lblReportType As System.Windows.Forms.Label
    Private cmbReportType As System.Windows.Forms.ComboBox
    Private btnPreview As System.Windows.Forms.Button
    Private btnRefresh As System.Windows.Forms.Button
    Private dgvReports As System.Windows.Forms.DataGridView
    Private lblCounter As System.Windows.Forms.Label
    Private PrintDocument1 As System.Drawing.Printing.PrintDocument
    Private PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private _printData As System.Data.DataTable

    Private Sub ucAdminReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildUI()
        LoadReport()
    End Sub

    Private Sub BuildUI()
        lblTitle = New System.Windows.Forms.Label()
        lblTitle.AutoSize = True
        lblTitle.Font = New System.Drawing.Font("Segoe UI", 24, System.Drawing.FontStyle.Bold)
        lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        lblTitle.Location = New System.Drawing.Point(30, 25)
        lblTitle.Text = "Reports"
        Me.Controls.Add(lblTitle)

        lblSubtitle = New System.Windows.Forms.Label()
        lblSubtitle.AutoSize = True
        lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 11)
        lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblSubtitle.Location = New System.Drawing.Point(35, 72)
        lblSubtitle.Text = "Generate printable stock and sales reports"
        Me.Controls.Add(lblSubtitle)

        lblReportType = New System.Windows.Forms.Label()
        lblReportType.AutoSize = True
        lblReportType.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        lblReportType.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblReportType.Location = New System.Drawing.Point(30, 115)
        lblReportType.Text = "Report Type:"
        Me.Controls.Add(lblReportType)

        cmbReportType = New System.Windows.Forms.ComboBox()
        cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        cmbReportType.Font = New System.Drawing.Font("Segoe UI", 11)
        cmbReportType.Location = New System.Drawing.Point(140, 110)
        cmbReportType.Size = New System.Drawing.Size(280, 27)
        cmbReportType.Items.AddRange(New Object() {"Stock Report (RP-1)", "Sales Report (RP-3)"})
        cmbReportType.SelectedIndex = 0
        AddHandler cmbReportType.SelectedIndexChanged, AddressOf cmbReportType_SelectedIndexChanged
        Me.Controls.Add(cmbReportType)

        btnRefresh = New System.Windows.Forms.Button()
        btnRefresh.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnRefresh.FlatAppearance.BorderSize = 0
        btnRefresh.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnRefresh.ForeColor = System.Drawing.Color.White
        btnRefresh.Location = New System.Drawing.Point(440, 106)
        btnRefresh.Size = New System.Drawing.Size(110, 32)
        btnRefresh.Text = "Refresh"
        btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnRefresh.Click, AddressOf btnRefresh_Click
        Me.Controls.Add(btnRefresh)

        btnPreview = New System.Windows.Forms.Button()
        btnPreview.BackColor = System.Drawing.Color.FromArgb(27, 122, 80)
        btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnPreview.FlatAppearance.BorderSize = 0
        btnPreview.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnPreview.ForeColor = System.Drawing.Color.White
        btnPreview.Location = New System.Drawing.Point(560, 106)
        btnPreview.Size = New System.Drawing.Size(160, 32)
        btnPreview.Text = "Print / Preview"
        btnPreview.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnPreview.Click, AddressOf btnPreview_Click
        Me.Controls.Add(btnPreview)

        lblCounter = New System.Windows.Forms.Label()
        lblCounter.AutoSize = True
        lblCounter.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Italic)
        lblCounter.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblCounter.Location = New System.Drawing.Point(740, 114)
        lblCounter.Text = "0 rows"
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
        dgvReports.RowTemplate.Height = 34
        dgvReports.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9)
        dgvReports.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        dgvReports.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        dgvReports.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.Controls.Add(dgvReports)

        ' Print objects
        PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        AddHandler PrintDocument1.PrintPage, AddressOf PrintDocument_PrintPage
        PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
    End Sub

    Private Sub LoadReport()
        Try
            Dim reportType As String = "Stock Report (RP-1)"
            If cmbReportType.SelectedItem IsNot Nothing Then reportType = cmbReportType.SelectedItem.ToString()

            If reportType = "Stock Report (RP-1)" Then
                _printData = DatabaseHelper.GetStockReport()
            Else
                _printData = DatabaseHelper.GetSalesReport()
            End If

            dgvReports.DataSource = _printData
            dgvReports.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill

            lblCounter.Text = _printData.Rows.Count.ToString() & " row(s)"
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Error loading report: " & ex.Message)
        End Try
    End Sub

    Private Sub cmbReportType_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadReport()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        LoadReport()
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs)
        If _printData Is Nothing OrElse _printData.Rows.Count = 0 Then
            System.Windows.Forms.MessageBox.Show("No data to print.", "Empty Report", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If

        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private Sub PrintDocument_PrintPage(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs)
        Dim fontHeader As New System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold)
        Dim fontSub As New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular)
        Dim fontBody As New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Regular)
        Dim yPos As Integer = e.MarginBounds.Top
        Dim leftMargin As Integer = e.MarginBounds.Left

        Dim title As String = "Stock Report (RP-1)"
        If cmbReportType.SelectedItem IsNot Nothing Then title = cmbReportType.SelectedItem.ToString()

        e.Graphics.DrawString("AgriMarket - " & title, fontHeader, System.Drawing.Brushes.Black, leftMargin, yPos)
        yPos += 30
        e.Graphics.DrawString("Printed on: " & DateTime.Now.ToString("dd MMM yyyy HH:mm"), fontSub, System.Drawing.Brushes.Gray, leftMargin, yPos)
        yPos += 40

        Dim xPos As Integer = leftMargin
        For Each col As System.Data.DataColumn In _printData.Columns
            e.Graphics.DrawString(col.ColumnName, fontSub, System.Drawing.Brushes.Black, xPos, yPos)
            xPos += 150
        Next
        yPos += 25
        e.Graphics.DrawLine(System.Drawing.Pens.Black, leftMargin, yPos - 5, e.MarginBounds.Right, yPos - 5)

        For Each row As System.Data.DataRow In _printData.Rows
            xPos = leftMargin
            For Each col As System.Data.DataColumn In _printData.Columns
                Dim cellValue As String = row(col).ToString()
                If cellValue.Length > 18 Then cellValue = cellValue.Substring(0, 15) & "..."
                e.Graphics.DrawString(cellValue, fontBody, System.Drawing.Brushes.Black, xPos, yPos)
                xPos += 150
            Next
            yPos += 25
            If yPos > e.MarginBounds.Bottom - 50 Then
                e.HasMorePages = True
                Exit Sub
            End If
        Next
    End Sub
End Class