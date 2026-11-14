<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucHome
    Inherits System.Windows.Forms.UserControl

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblWelcome = New System.Windows.Forms.Label()
        Me.lblSubText = New System.Windows.Forms.Label()

        ' Stat Cards
        Me.cardStock = New ucStatCard()
        Me.cardPending = New ucStatCard()
        Me.cardCompleted = New ucStatCard()
        Me.cardSales = New ucStatCard()

        ' Charts
        Me.pnlLineChart = New System.Windows.Forms.Panel()
        Me.lblLineChartTitle = New System.Windows.Forms.Label()
        Me.ucLineChart1 = New ucLineChart()

        Me.pnlDonutChart = New System.Windows.Forms.Panel()
        Me.lblDonutChartTitle = New System.Windows.Forms.Label()
        Me.ucDonutChart1 = New ucDonutChart()

        ' Tables
        Me.lblSectionBatches = New System.Windows.Forms.Label()
        Me.dgvBatchesToWatch = New System.Windows.Forms.DataGridView()
        Me.lblSectionNotifs = New System.Windows.Forms.Label()
        Me.dgvLatestNotifs = New System.Windows.Forms.DataGridView()

        ' Setup Form
        Me.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.Size = New System.Drawing.Size(1180, 700) ' Made taller for the charts

        ' Welcome Text
        Me.lblWelcome.AutoSize = True
        Me.lblWelcome.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.lblWelcome.Location = New System.Drawing.Point(30, 30)
        Me.lblWelcome.Text = "Welcome back!"

        Me.lblSubText.AutoSize = True
        Me.lblSubText.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblSubText.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblSubText.Location = New System.Drawing.Point(35, 75)
        Me.lblSubText.Text = "Here is what is happening with your business today."

        ' Stat Cards Setup
        Me.cardStock.Location = New System.Drawing.Point(30, 120)
        Me.cardStock.Size = New System.Drawing.Size(260, 110)
        Me.cardStock.Title = "Items in stock"
        Me.cardStock.IconColor = Color.FromArgb(1, 96, 100)

        Me.cardPending.Location = New System.Drawing.Point(310, 120)
        Me.cardPending.Size = New System.Drawing.Size(260, 110)
        Me.cardPending.Title = "Pending orders"
        Me.cardPending.IconColor = Color.FromArgb(138, 82, 0)

        Me.cardCompleted.Location = New System.Drawing.Point(590, 120)
        Me.cardCompleted.Size = New System.Drawing.Size(260, 110)
        Me.cardCompleted.Title = "Completed orders"
        Me.cardCompleted.IconColor = Color.FromArgb(27, 122, 80)

        Me.cardSales.Location = New System.Drawing.Point(870, 120)
        Me.cardSales.Size = New System.Drawing.Size(260, 110)
        Me.cardSales.Title = "Sales this month"
        Me.cardSales.IconColor = Color.FromArgb(1, 96, 100)

        ' Line Chart Panel
        Me.pnlLineChart.BackColor = Color.White
        Me.pnlLineChart.Location = New System.Drawing.Point(30, 250)
        Me.pnlLineChart.Size = New System.Drawing.Size(700, 200)
        Me.lblLineChartTitle.AutoSize = True
        Me.lblLineChartTitle.Font = New Font("Segoe UI", 12.0!, FontStyle.Bold)
        Me.lblLineChartTitle.Location = New Point(15, 15)
        Me.lblLineChartTitle.Text = "Order performance this week"
        Me.ucLineChart1.Location = New Point(15, 50)
        Me.ucLineChart1.Size = New Size(670, 130)
        Me.pnlLineChart.Controls.Add(Me.lblLineChartTitle)
        Me.pnlLineChart.Controls.Add(Me.ucLineChart1)

        ' Donut Chart Panel
        Me.pnlDonutChart.BackColor = Color.White
        Me.pnlDonutChart.Location = New System.Drawing.Point(750, 250)
        Me.pnlDonutChart.Size = New System.Drawing.Size(380, 200)
        Me.lblDonutChartTitle.AutoSize = True
        Me.lblDonutChartTitle.Font = New Font("Segoe UI", 12.0!, FontStyle.Bold)
        Me.lblDonutChartTitle.Location = New Point(15, 15)
        Me.lblDonutChartTitle.Text = "Orders by status"
        Me.ucDonutChart1.Location = New Point(70, 40)
        Me.ucDonutChart1.Size = New Size(240, 150)
        Me.pnlDonutChart.Controls.Add(Me.lblDonutChartTitle)
        Me.pnlDonutChart.Controls.Add(Me.ucDonutChart1)

        ' Batches to Watch
        Me.lblSectionBatches.AutoSize = True
        Me.lblSectionBatches.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblSectionBatches.Location = New System.Drawing.Point(30, 470)
        Me.lblSectionBatches.Text = "Batches to watch"

        Me.dgvBatchesToWatch.Location = New System.Drawing.Point(30, 510)
        Me.dgvBatchesToWatch.Size = New System.Drawing.Size(540, 160)
        Me.dgvBatchesToWatch.AllowUserToAddRows = False
        Me.dgvBatchesToWatch.ReadOnly = True

        ' Latest Notifications
        Me.lblSectionNotifs.AutoSize = True
        Me.lblSectionNotifs.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblSectionNotifs.Location = New System.Drawing.Point(590, 470)
        Me.lblSectionNotifs.Text = "Latest notifications"

        Me.dgvLatestNotifs.Location = New System.Drawing.Point(590, 510)
        Me.dgvLatestNotifs.Size = New System.Drawing.Size(540, 160)
        Me.dgvLatestNotifs.AllowUserToAddRows = False
        Me.dgvLatestNotifs.ReadOnly = True

        ' Add to Form
        Me.Controls.Add(Me.lblWelcome)
        Me.Controls.Add(Me.lblSubText)
        Me.Controls.Add(Me.cardStock)
        Me.Controls.Add(Me.cardPending)
        Me.Controls.Add(Me.cardCompleted)
        Me.Controls.Add(Me.cardSales)
        Me.Controls.Add(Me.pnlLineChart)
        Me.Controls.Add(Me.pnlDonutChart)
        Me.Controls.Add(Me.lblSectionBatches)
        Me.Controls.Add(Me.dgvBatchesToWatch)
        Me.Controls.Add(Me.lblSectionNotifs)
        Me.Controls.Add(Me.dgvLatestNotifs)
    End Sub

    Friend WithEvents lblWelcome As System.Windows.Forms.Label
    Friend WithEvents lblSubText As System.Windows.Forms.Label
    Friend WithEvents cardStock As ucStatCard
    Friend WithEvents cardPending As ucStatCard
    Friend WithEvents cardCompleted As ucStatCard
    Friend WithEvents cardSales As ucStatCard
    Friend WithEvents pnlLineChart As System.Windows.Forms.Panel
    Friend WithEvents lblLineChartTitle As System.Windows.Forms.Label
    Friend WithEvents ucLineChart1 As ucLineChart
    Friend WithEvents pnlDonutChart As System.Windows.Forms.Panel
    Friend WithEvents lblDonutChartTitle As System.Windows.Forms.Label
    Friend WithEvents ucDonutChart1 As ucDonutChart
    Friend WithEvents lblSectionBatches As System.Windows.Forms.Label
    Friend WithEvents dgvBatchesToWatch As System.Windows.Forms.DataGridView
    Friend WithEvents lblSectionNotifs As System.Windows.Forms.Label
    Friend WithEvents dgvLatestNotifs As System.Windows.Forms.DataGridView
End Class