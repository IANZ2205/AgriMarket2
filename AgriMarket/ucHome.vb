Imports System.Data
Imports System.ComponentModel

Public Class ucHome
    Private _userId As Integer
    Private _userRole As String
    Private _userName As String

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property UserId As Integer
        Get
            Return _userId
        End Get
        Set(value As Integer)
            _userId = value
        End Set
    End Property

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property UserRole As String
        Get
            Return _userRole
        End Get
        Set(value As String)
            _userRole = value
        End Set
    End Property

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property UserName As String
        Get
            Return _userName
        End Get
        Set(value As String)
            _userName = value
            lblWelcome.Text = "Welcome back, " & value & "!"
        End Set
    End Property

    Private Async Sub ucHome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyGridStyles()
        If _userId > 0 Then Await LoadDashboard()
    End Sub

    Private Sub ApplyGridStyles()
        Dim grids() As DataGridView = {dgvBatchesToWatch, dgvLatestNotifs}
        For Each dgv In grids
            dgv.BackgroundColor = Color.White
            dgv.BorderStyle = BorderStyle.None
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            dgv.GridColor = Theme.Border
            dgv.EnableHeadersVisualStyles = False
            dgv.ColumnHeadersVisible = False
            dgv.DefaultCellStyle.BackColor = Color.White
            dgv.DefaultCellStyle.ForeColor = Theme.TextDark
            dgv.DefaultCellStyle.SelectionBackColor = Theme.Tint
            dgv.DefaultCellStyle.SelectionForeColor = Theme.Ocean
            dgv.DefaultCellStyle.Font = New Font("Segoe UI", 10.0!)
            dgv.RowTemplate.Height = 35
        Next
    End Sub

    Private Async Function LoadDashboard() As Task
        If _userRole = "Trader" Then
            ' 1. Stats & Charts
            Dim response As String = Await NetworkClient.SendRequestAsync($"GET_DASHBOARD_STATS|{_userId}")
            Dim parts As String() = response.Split("|"c)

            Dim pendingCount As Integer = 0
            Dim completedCount As Integer = 0

            If parts(0) = "OK" Then
                Dim stats As String() = parts(2).Split("~"c)
                cardStock.Value = stats(0)
                cardPending.Value = stats(1)
                cardCompleted.Value = stats(2)
                cardSales.Value = "UGX " & Convert.ToDecimal(stats(3)).ToString("N0")

                pendingCount = Convert.ToInt32(stats(1))
                completedCount = Convert.ToInt32(stats(2))

                ' Feed the Donut Chart
                ucDonutChart1.SetData(completedCount, pendingCount, 0)

                ' Feed the Line Chart (Sample data for the week based on completed/pending)
                Dim weeklyData As New List(Of Integer) From {0, completedCount, 0, pendingCount, 0, 0, completedCount}
                ucLineChart1.SetData(weeklyData)
            End If

            ' 2. Batches to watch (Filtered)
            Dim stockRes As String = Await NetworkClient.SendRequestAsync($"GET_BATCHES_TO_WATCH|{_userId}")
            Dim sParts As String() = stockRes.Split("|"c)
            If sParts(0) = "OK" AndAlso sParts.Length > 2 Then
                Dim dt As New DataTable()
                dt.Columns.Add("Batch")
                dt.Columns.Add("Produce")
                dt.Columns.Add("Expiry")
                For Each r As String In sParts(2).Split("^"c)
                    If r.Trim() <> "" Then
                        Dim f As String() = r.Split("~"c)
                        dt.Rows.Add(f(0), f(1), f(2))
                    End If
                Next
                dgvBatchesToWatch.DataSource = dt
                dgvBatchesToWatch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End If

            ' 3. Notifications
            Dim notifRes As String = Await NetworkClient.SendRequestAsync($"GET_NOTIFICATIONS|{_userId}")
            Dim nParts As String() = notifRes.Split("|"c)
            If nParts(0) = "OK" AndAlso nParts.Length > 2 Then
                Dim dt As New DataTable()
                dt.Columns.Add("Message")
                For Each r As String In nParts(2).Split("^"c)
                    If r.Trim() <> "" Then
                        Dim f As String() = r.Split("~"c)
                        dt.Rows.Add(f(3))
                    End If
                Next
                dgvLatestNotifs.DataSource = dt
                dgvLatestNotifs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End If

        ElseIf _userRole = "Admin" Then
            ' Admin logic goes here
            lblSubText.Text = "System-wide overview."
            cardStock.Title = "Total Users"
            cardPending.Title = "Active Traders"
            cardCompleted.Title = "Active Buyers"
            cardSales.Title = "Total Orders"
            pnlLineChart.Visible = False
            pnlDonutChart.Visible = False
            dgvBatchesToWatch.Visible = False
            lblSectionBatches.Visible = False
            dgvLatestNotifs.Visible = False
            lblSectionNotifs.Visible = False
        Else
            ' Buyer Dashboard
            lblSubText.Text = "Browse the market and find fresh produce."
            cardStock.Title = "Active Listings"
            cardPending.Title = "My Pending Orders"
            cardCompleted.Title = "My Completed Orders"
            cardSales.Title = "Total Spent"
            pnlLineChart.Visible = False
            pnlDonutChart.Visible = False
            dgvBatchesToWatch.Visible = False
            lblSectionBatches.Visible = False
            dgvLatestNotifs.Visible = False
            lblSectionNotifs.Visible = False
        End If
    End Function
End Class