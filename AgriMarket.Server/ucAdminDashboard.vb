Imports System.Data
Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class ucAdminDashboard
    Private _ordersData As System.Data.DataTable

    Private lblTitle As System.Windows.Forms.Label
    Private lblSubtitle As System.Windows.Forms.Label

    Private pnlStat1 As RoundedPanel
    Private pnlStat2 As RoundedPanel
    Private pnlStat3 As RoundedPanel
    Private pnlStat4 As RoundedPanel

    Private lblStat1Title As System.Windows.Forms.Label
    Private lblStat2Title As System.Windows.Forms.Label
    Private lblStat3Title As System.Windows.Forms.Label
    Private lblStat4Title As System.Windows.Forms.Label

    Private lblStat1Value As System.Windows.Forms.Label
    Private lblStat2Value As System.Windows.Forms.Label
    Private lblStat3Value As System.Windows.Forms.Label
    Private lblStat4Value As System.Windows.Forms.Label

    Private pnlBarChart As RoundedPanel
    Private pnlDonutChart As RoundedPanel
    Private picBarChart As System.Windows.Forms.PictureBox
    Private picDonutChart As System.Windows.Forms.PictureBox

    Private lblApprovalTitle As System.Windows.Forms.Label
    Private dgvApprovals As System.Windows.Forms.DataGridView
    Private btnApprove As System.Windows.Forms.Button
    Private btnReject As System.Windows.Forms.Button

    Private lblAttentionTitle As System.Windows.Forms.Label
    Private lstAttention As System.Windows.Forms.ListBox

    Private Sub ucAdminDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildTitle()
        BuildStat1()
        BuildStat2()
        BuildStat3()
        BuildStat4()
        BuildCharts()
        BuildApprovals()
        BuildAttention()
        ApplyGridStyle()
        LoadStats()
        LoadOrdersCharts()
        LoadPendingApprovals()
        LoadAttentionItems()
    End Sub

    Private Sub BuildTitle()
        lblTitle = New System.Windows.Forms.Label()
        lblTitle.AutoSize = True
        lblTitle.Font = New System.Drawing.Font("Segoe UI", 26, System.Drawing.FontStyle.Bold)
        lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        lblTitle.Location = New System.Drawing.Point(40, 30)
        lblTitle.Text = "System overview"
        Me.Controls.Add(lblTitle)

        lblSubtitle = New System.Windows.Forms.Label()
        lblSubtitle.AutoSize = True
        lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 11)
        lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblSubtitle.Location = New System.Drawing.Point(45, 82)
        lblSubtitle.Text = "Everything happening on AgriMarket today"
        Me.Controls.Add(lblSubtitle)
    End Sub

    Private Function MakeCard(x As Integer, title As String, iconColor As Color, ByRef outTitle As System.Windows.Forms.Label, ByRef outValue As System.Windows.Forms.Label) As RoundedPanel
        Dim card As New RoundedPanel()
        card.CornerRadius = 14
        card.Location = New System.Drawing.Point(x, 130)
        card.Size = New System.Drawing.Size(280, 125)

        ' Icon circle (Round via CornerRadius = half of size)
        Dim icon As New RoundedPanel()
        icon.CornerRadius = 24
        icon.BackColor = iconColor
        icon.ShowBorder = False
        icon.Location = New System.Drawing.Point(22, 34)
        icon.Size = New System.Drawing.Size(48, 48)
        card.Controls.Add(icon)

        ' Title
        outTitle = New System.Windows.Forms.Label()
        outTitle.AutoSize = True
        outTitle.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular)
        outTitle.ForeColor = System.Drawing.Color.FromArgb(120, 130, 135)
        outTitle.Location = New System.Drawing.Point(90, 30)
        outTitle.Text = title
        card.Controls.Add(outTitle)

        ' Value
        outValue = New System.Windows.Forms.Label()
        outValue.AutoSize = True
        outValue.Font = New System.Drawing.Font("Segoe UI", 24, System.Drawing.FontStyle.Bold)
        outValue.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        outValue.Location = New System.Drawing.Point(88, 55)
        outValue.Text = "0"
        card.Controls.Add(outValue)

        Return card
    End Function

    Private Sub BuildStat1()
        pnlStat1 = MakeCard(40, "Traders", System.Drawing.Color.FromArgb(1, 96, 100), lblStat1Title, lblStat1Value)
        Me.Controls.Add(pnlStat1)
    End Sub

    Private Sub BuildStat2()
        pnlStat2 = MakeCard(335, "Buyers", System.Drawing.Color.FromArgb(95, 179, 179), lblStat2Title, lblStat2Value)
        Me.Controls.Add(pnlStat2)
    End Sub

    Private Sub BuildStat3()
        pnlStat3 = MakeCard(630, "Active listings", System.Drawing.Color.FromArgb(27, 122, 80), lblStat3Title, lblStat3Value)
        Me.Controls.Add(pnlStat3)
    End Sub

    Private Sub BuildStat4()
        pnlStat4 = MakeCard(925, "Orders today", System.Drawing.Color.FromArgb(217, 164, 65), lblStat4Title, lblStat4Value)
        Me.Controls.Add(pnlStat4)
    End Sub

    Private Sub BuildCharts()
        pnlBarChart = New RoundedPanel()
        pnlBarChart.CornerRadius = 14
        pnlBarChart.Location = New System.Drawing.Point(40, 280)
        pnlBarChart.Size = New System.Drawing.Size(730, 220)

        Dim barTitle As New System.Windows.Forms.Label()
        barTitle.AutoSize = True
        barTitle.Font = New System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold)
        barTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        barTitle.Location = New System.Drawing.Point(20, 15)
        barTitle.Text = "Orders this week"
        pnlBarChart.Controls.Add(barTitle)

        picBarChart = New System.Windows.Forms.PictureBox()
        picBarChart.BackColor = System.Drawing.Color.White
        picBarChart.Location = New System.Drawing.Point(20, 45)
        picBarChart.Size = New System.Drawing.Size(690, 160)
        pnlBarChart.Controls.Add(picBarChart)

        Me.Controls.Add(pnlBarChart)

        pnlDonutChart = New RoundedPanel()
        pnlDonutChart.CornerRadius = 14
        pnlDonutChart.Location = New System.Drawing.Point(790, 280)
        pnlDonutChart.Size = New System.Drawing.Size(415, 220)

        Dim donutTitle As New System.Windows.Forms.Label()
        donutTitle.AutoSize = True
        donutTitle.Font = New System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold)
        donutTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        donutTitle.Location = New System.Drawing.Point(20, 15)
        donutTitle.Text = "Orders by status"
        pnlDonutChart.Controls.Add(donutTitle)

        picDonutChart = New System.Windows.Forms.PictureBox()
        picDonutChart.BackColor = System.Drawing.Color.White
        picDonutChart.Location = New System.Drawing.Point(20, 45)
        picDonutChart.Size = New System.Drawing.Size(380, 160)
        pnlDonutChart.Controls.Add(picDonutChart)

        Me.Controls.Add(pnlDonutChart)
    End Sub

    Private Sub BuildApprovals()
        lblApprovalTitle = New System.Windows.Forms.Label()
        lblApprovalTitle.AutoSize = True
        lblApprovalTitle.Font = New System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold)
        lblApprovalTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        lblApprovalTitle.Location = New System.Drawing.Point(40, 520)
        lblApprovalTitle.Text = "Traders waiting for approval"
        Me.Controls.Add(lblApprovalTitle)

        btnApprove = New System.Windows.Forms.Button()
        btnApprove.BackColor = System.Drawing.Color.FromArgb(27, 122, 80)
        btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnApprove.FlatAppearance.BorderSize = 0
        btnApprove.Font = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
        btnApprove.ForeColor = System.Drawing.Color.White
        btnApprove.Location = New System.Drawing.Point(540, 518)
        btnApprove.Size = New System.Drawing.Size(110, 30)
        btnApprove.Text = "Approve"
        btnApprove.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnApprove.Click, AddressOf btnApprove_Click
        Me.Controls.Add(btnApprove)

        btnReject = New System.Windows.Forms.Button()
        btnReject.BackColor = System.Drawing.Color.FromArgb(179, 38, 30)
        btnReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnReject.FlatAppearance.BorderSize = 0
        btnReject.Font = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
        btnReject.ForeColor = System.Drawing.Color.White
        btnReject.Location = New System.Drawing.Point(660, 518)
        btnReject.Size = New System.Drawing.Size(110, 30)
        btnReject.Text = "Reject"
        btnReject.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnReject.Click, AddressOf btnReject_Click
        Me.Controls.Add(btnReject)

        dgvApprovals = New System.Windows.Forms.DataGridView()
        dgvApprovals.BackgroundColor = System.Drawing.Color.White
        dgvApprovals.BorderStyle = System.Windows.Forms.BorderStyle.None
        dgvApprovals.Location = New System.Drawing.Point(40, 558)
        dgvApprovals.Size = New System.Drawing.Size(730, 140)
        dgvApprovals.AllowUserToAddRows = False
        dgvApprovals.ReadOnly = True
        dgvApprovals.RowHeadersVisible = False
        dgvApprovals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Controls.Add(dgvApprovals)
    End Sub

    Private Sub BuildAttention()
        lblAttentionTitle = New System.Windows.Forms.Label()
        lblAttentionTitle.AutoSize = True
        lblAttentionTitle.Font = New System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold)
        lblAttentionTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        lblAttentionTitle.Location = New System.Drawing.Point(790, 520)
        lblAttentionTitle.Text = "Needs attention"
        Me.Controls.Add(lblAttentionTitle)

        lstAttention = New System.Windows.Forms.ListBox()
        lstAttention.BackColor = System.Drawing.Color.White
        lstAttention.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lstAttention.Font = New System.Drawing.Font("Segoe UI", 10)
        lstAttention.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        lstAttention.ItemHeight = 24
        lstAttention.Location = New System.Drawing.Point(790, 558)
        lstAttention.Size = New System.Drawing.Size(415, 140)
        Me.Controls.Add(lstAttention)
    End Sub

    Private Sub ApplyGridStyle()
        dgvApprovals.BackgroundColor = System.Drawing.Color.White
        dgvApprovals.BorderStyle = System.Windows.Forms.BorderStyle.None
        dgvApprovals.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        dgvApprovals.GridColor = System.Drawing.Color.FromArgb(235, 235, 235)
        dgvApprovals.EnableHeadersVisualStyles = False
        dgvApprovals.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        dgvApprovals.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.White
        dgvApprovals.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(120, 130, 135)
        dgvApprovals.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
        dgvApprovals.ColumnHeadersHeight = 30
        dgvApprovals.RowTemplate.Height = 32
        dgvApprovals.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9)
        dgvApprovals.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        dgvApprovals.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
    End Sub

    Private Sub LoadStats()
        Try
            Dim dt As System.Data.DataTable = DatabaseHelper.GetAdminDashboardStats()
            If dt.Rows.Count > 0 Then
                Dim row As System.Data.DataRow = dt.Rows(0)
                lblStat1Value.Text = row("TotalTraders").ToString()
                lblStat2Value.Text = row("TotalBuyers").ToString()
            End If

            Try
                Dim listings As System.Data.DataTable = DatabaseHelper.GetAllListingsAdmin()
                Dim activeCount As Integer = 0
                For Each r As System.Data.DataRow In listings.Rows
                    If r("Status").ToString() = "Active" Then activeCount += 1
                Next
                lblStat3Value.Text = activeCount.ToString()
            Catch
                lblStat3Value.Text = "0"
            End Try

            Try
                Dim orders As System.Data.DataTable = DatabaseHelper.GetAllOrdersAdmin()
                Dim todayCount As Integer = 0
                For Each r As System.Data.DataRow In orders.Rows
                    Try
                        Dim d As DateTime = Convert.ToDateTime(r("Date")).Date
                        If d = DateTime.Now.Date Then todayCount += 1
                    Catch
                    End Try
                Next
                lblStat4Value.Text = todayCount.ToString()
            Catch
                lblStat4Value.Text = "0"
            End Try
        Catch
        End Try
    End Sub

    Private Sub LoadOrdersCharts()
        Try
            _ordersData = DatabaseHelper.GetAllOrdersAdmin()

            Dim completed As Integer = 0
            Dim confirmed As Integer = 0
            Dim pending As Integer = 0
            Dim rejected As Integer = 0
            Dim cancelled As Integer = 0

            For Each r As System.Data.DataRow In _ordersData.Rows
                Select Case r("Status").ToString()
                    Case "Completed"
                        completed += 1
                    Case "Confirmed"
                        confirmed += 1
                    Case "Pending"
                        pending += 1
                    Case "Rejected"
                        rejected += 1
                    Case "Cancelled"
                        cancelled += 1
                End Select
            Next

            DrawDonut(completed, confirmed, pending, rejected, cancelled)
            DrawBarChart()
        Catch
        End Try
    End Sub

    Private Sub DrawDonut(completed As Integer, confirmed As Integer, pending As Integer, rejected As Integer, cancelled As Integer)
        If picDonutChart.Width <= 0 OrElse picDonutChart.Height <= 0 Then Return
        Dim bmp As New System.Drawing.Bitmap(picDonutChart.Width, picDonutChart.Height)
        Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmp)
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
            g.Clear(System.Drawing.Color.White)

            Dim total As Integer = completed + confirmed + pending + rejected + cancelled
            Dim rect As New System.Drawing.Rectangle(10, 10, 140, 140)

            If total = 0 Then
                Using p As New System.Drawing.Pen(System.Drawing.Color.FromArgb(240, 240, 240), 22)
                    g.DrawEllipse(p, rect)
                End Using
            Else
                Dim startAngle As Single = -90

                Dim sweep1 As Single = CSng((completed / total) * 360)
                If sweep1 > 0 Then g.FillPie(New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(1, 96, 100)), rect, startAngle, sweep1)
                startAngle += sweep1

                Dim sweep2 As Single = CSng((confirmed / total) * 360)
                If sweep2 > 0 Then g.FillPie(New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(95, 179, 179)), rect, startAngle, sweep2)
                startAngle += sweep2

                Dim sweep3 As Single = CSng((pending / total) * 360)
                If sweep3 > 0 Then g.FillPie(New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(217, 164, 65)), rect, startAngle, sweep3)
                startAngle += sweep3

                Dim sweep4 As Single = CSng((rejected / total) * 360)
                If sweep4 > 0 Then g.FillPie(New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(179, 38, 30)), rect, startAngle, sweep4)
                startAngle += sweep4

                Dim sweep5 As Single = CSng((cancelled / total) * 360)
                If sweep5 > 0 Then g.FillPie(New System.Drawing.SolidBrush(System.Drawing.Color.LightGray), rect, startAngle, sweep5)
            End If

            g.FillEllipse(System.Drawing.Brushes.White, 42, 42, 76, 76)

            Using f As New System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold)
                Dim s As String = total.ToString()
                Dim sz As System.Drawing.SizeF = g.MeasureString(s, f)
                g.DrawString(s, f, System.Drawing.Brushes.Black, 80 - sz.Width / 2, 68)
            End Using
            Using f As New System.Drawing.Font("Segoe UI", 8)
                Dim s As String = "orders"
                Dim sz As System.Drawing.SizeF = g.MeasureString(s, f)
                g.DrawString(s, f, System.Drawing.Brushes.Gray, 80 - sz.Width / 2, 94)
            End Using

            Dim lx As Integer = 170
            Dim ly As Integer = 10
            Dim stp As Integer = 26

            g.FillRectangle(New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(1, 96, 100)), lx, ly + 4, 12, 12)
            g.DrawString("Completed (" & completed & ")", New System.Drawing.Font("Segoe UI", 9), System.Drawing.Brushes.Black, lx + 18, ly)
            ly += stp

            g.FillRectangle(New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(95, 179, 179)), lx, ly + 4, 12, 12)
            g.DrawString("Confirmed (" & confirmed & ")", New System.Drawing.Font("Segoe UI", 9), System.Drawing.Brushes.Black, lx + 18, ly)
            ly += stp

            g.FillRectangle(New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(217, 164, 65)), lx, ly + 4, 12, 12)
            g.DrawString("Pending (" & pending & ")", New System.Drawing.Font("Segoe UI", 9), System.Drawing.Brushes.Black, lx + 18, ly)
            ly += stp

            g.FillRectangle(New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(179, 38, 30)), lx, ly + 4, 12, 12)
            g.DrawString("Rejected (" & rejected & ")", New System.Drawing.Font("Segoe UI", 9), System.Drawing.Brushes.Black, lx + 18, ly)
            ly += stp

            g.FillRectangle(New System.Drawing.SolidBrush(System.Drawing.Color.LightGray), lx, ly + 4, 12, 12)
            g.DrawString("Cancelled (" & cancelled & ")", New System.Drawing.Font("Segoe UI", 9), System.Drawing.Brushes.Black, lx + 18, ly)
        End Using

        picDonutChart.Image = bmp
    End Sub

    Private Sub DrawBarChart()
        If picBarChart.Width <= 0 OrElse picBarChart.Height <= 0 Then Return
        Dim bmp As New System.Drawing.Bitmap(picBarChart.Width, picBarChart.Height)
        Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmp)
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
            g.Clear(System.Drawing.Color.White)

            Dim days(6) As Integer
            Dim today As DateTime = DateTime.Now.Date
            Dim daysSinceMonday As Integer = (CInt(today.DayOfWeek) + 6) Mod 7
            Dim monday As DateTime = today.AddDays(-daysSinceMonday)

            For Each r As System.Data.DataRow In _ordersData.Rows
                Try
                    Dim d As DateTime = Convert.ToDateTime(r("Date")).Date
                    Dim diff As Integer = CInt((d - monday).TotalDays)
                    If diff >= 0 AndAlso diff <= 6 Then
                        days(diff) += 1
                    End If
                Catch
                End Try
            Next

            Dim maxVal As Integer = 1
            For Each v As Integer In days
                If v > maxVal Then maxVal = v
            Next

            Dim chartLeft As Integer = 40
            Dim chartRight As Integer = picBarChart.Width - 20
            Dim chartTop As Integer = 20
            Dim chartBottom As Integer = picBarChart.Height - 30
            Dim chartHeight As Integer = chartBottom - chartTop

            Using p As New System.Drawing.Pen(System.Drawing.Color.FromArgb(235, 235, 235), 1)
                For i As Integer = 0 To 4
                    Dim y As Integer = chartTop + (chartHeight \ 4) * i
                    g.DrawLine(p, chartLeft, y, chartRight, y)
                Next
            End Using

            Dim dayLabels() As String = {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"}
            Dim slotWidth As Integer = (chartRight - chartLeft) \ 7
            Dim barWidth As Integer = slotWidth \ 2

            For i As Integer = 0 To 6
                Dim barHeight As Integer = 0
                If maxVal > 0 Then
                    barHeight = CInt((days(i) / maxVal) * chartHeight)
                End If

                Dim x As Integer = chartLeft + (slotWidth * i) + (slotWidth - barWidth) \ 2
                Dim y As Integer = chartBottom - barHeight

                If barHeight > 0 Then
                    ' Rounded top bar
                    Dim radius As Integer = Math.Min(barWidth \ 2, 8)
                    Using path As New System.Drawing.Drawing2D.GraphicsPath()
                        path.AddArc(x, y, radius * 2, radius * 2, 180, 90)
                        path.AddArc(x + barWidth - radius * 2, y, radius * 2, radius * 2, 270, 90)
                        path.AddLine(x + barWidth, y + radius, x + barWidth, chartBottom)
                        path.AddLine(x + barWidth, chartBottom, x, chartBottom)
                        path.AddLine(x, chartBottom, x, y + radius)
                        path.CloseFigure()
                        Using brush As New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(1, 96, 100))
                            g.FillPath(brush, path)
                        End Using
                    End Using
                End If

                Using f As New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
                    Dim s As String = days(i).ToString()
                    Dim sz As System.Drawing.SizeF = g.MeasureString(s, f)
                    g.DrawString(s, f, System.Drawing.Brushes.Black, x + (barWidth - sz.Width) / 2, y - 20)
                End Using

                Using f As New System.Drawing.Font("Segoe UI", 8)
                    Dim sz As System.Drawing.SizeF = g.MeasureString(dayLabels(i), f)
                    g.DrawString(dayLabels(i), f, System.Drawing.Brushes.Gray, x + (barWidth - sz.Width) / 2, chartBottom + 6)
                End Using
            Next
        End Using

        picBarChart.Image = bmp
    End Sub

    Private Sub LoadPendingApprovals()
        Try
            Dim allUsers As System.Data.DataTable = DatabaseHelper.GetAllUsers()
            Dim display As New System.Data.DataTable()
            display.Columns.Add("UserID", GetType(Integer))
            display.Columns.Add("Name")
            display.Columns.Add("Email")

            For Each row As System.Data.DataRow In allUsers.Rows
                If row("Status").ToString() = "Pending" AndAlso row("Role").ToString() = "Trader" Then
                    display.Rows.Add(row("UserID"), row("Name"), row("Email"))
                End If
            Next

            dgvApprovals.DataSource = display
            If dgvApprovals.Columns.Contains("UserID") Then dgvApprovals.Columns("UserID").Visible = False
            dgvApprovals.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill

            If display.Rows.Count = 0 Then
                lblApprovalTitle.Text = "Traders waiting for approval (none)"
            Else
                lblApprovalTitle.Text = "Traders waiting for approval (" & display.Rows.Count & ")"
            End If
        Catch
        End Try
    End Sub

    Private Sub LoadAttentionItems()
        lstAttention.Items.Clear()
        Try
            Dim allUsers As System.Data.DataTable = DatabaseHelper.GetAllUsers()
            Dim pendingCount As Integer = 0
            Dim suspendedCount As Integer = 0
            For Each row As System.Data.DataRow In allUsers.Rows
                If row("Status").ToString() = "Pending" Then pendingCount += 1
                If row("Status").ToString() = "Suspended" Then suspendedCount += 1
            Next

            If pendingCount > 0 Then lstAttention.Items.Add("- " & pendingCount & " trader(s) pending approval")
            If suspendedCount > 0 Then lstAttention.Items.Add("- " & suspendedCount & " suspended account(s)")

            Try
                Dim listings As System.Data.DataTable = DatabaseHelper.GetAllListingsAdmin()
                Dim activeListings As Integer = 0
                For Each r As System.Data.DataRow In listings.Rows
                    If r("Status").ToString() = "Active" Then activeListings += 1
                Next
                If activeListings > 0 Then lstAttention.Items.Add("- " & activeListings & " active listing(s) in the market")
            Catch
            End Try

            If lstAttention.Items.Count = 0 Then
                lstAttention.Items.Add("All good - no items need attention")
            End If
        Catch
        End Try
    End Sub

    Private Sub btnApprove_Click(sender As Object, e As EventArgs)
        If dgvApprovals.SelectedRows.Count = 0 Then
            System.Windows.Forms.MessageBox.Show("Please select a trader first.", "Validation", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim userId As Integer = Convert.ToInt32(dgvApprovals.SelectedRows(0).Cells("UserID").Value)
        If DatabaseHelper.ApproveUser(userId) Then
            System.Windows.Forms.MessageBox.Show("Trader approved successfully!", "Success", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
            LoadPendingApprovals()
            LoadStats()
            LoadAttentionItems()
        End If
    End Sub

    Private Sub btnReject_Click(sender As Object, e As EventArgs)
        System.Windows.Forms.MessageBox.Show("Rejection is not yet implemented. Use Manage Users to suspend the account.", "Not Implemented", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
    End Sub
End Class