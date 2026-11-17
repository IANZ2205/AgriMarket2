Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class ucAdminLivePerformance
    Private lblTitle As System.Windows.Forms.Label
    Private lblSubtitle As System.Windows.Forms.Label
    Private statCards(3) As RoundedPanel
    Private statValues(3) As System.Windows.Forms.Label
    Private chartBoxes(3) As System.Windows.Forms.PictureBox
    Private perfTimer As System.Windows.Forms.Timer

    Private _titleFont As Font
    Private _subtitleFont As Font
    Private _axisFont As Font
    Private _chartTitleFont As Font
    Private _chartValueFont As Font
    Private _chartMsgFont As Font
    Private _titleBrush As SolidBrush
    Private _subtitleBrush As SolidBrush
    Private _axisBrush As SolidBrush
    Private _chartTitleBrush As SolidBrush
    Private _chartValueBrush As SolidBrush
    Private _chartMsgBrush As SolidBrush
    Private _gridPen As Pen
    Private _linePen As Pen
    Private _hasResources As Boolean = False

    Private Sub ucAdminLivePerformance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Me.DesignMode Then Return
        BuildUI()
        CreateGdiResources()

        Try
            PerformanceSampler.Initialize()
            PerformanceSampler.TakeSample()
        Catch
        End Try

        ' Seed AFTER TakeSample — fills to 10 so charts draw immediately
        Try
            PerformanceSampler.SeedIfNeeded(10)
        Catch
        End Try

        perfTimer = New System.Windows.Forms.Timer()
        perfTimer.Interval = 3000
        AddHandler perfTimer.Tick, AddressOf UpdateCharts
        perfTimer.Start()

        UpdateCharts(Nothing, EventArgs.Empty)
    End Sub

    Private Sub CreateGdiResources()
        Try
            _titleFont = New Font("Segoe UI", 24, FontStyle.Bold)
            _subtitleFont = New Font("Segoe UI", 11)
            _axisFont = New Font("Segoe UI", 8)
            _chartTitleFont = New Font("Segoe UI", 14, FontStyle.Bold)
            _chartValueFont = New Font("Segoe UI", 14, FontStyle.Bold)
            _chartMsgFont = New Font("Segoe UI", 10, FontStyle.Italic)
            _titleBrush = New SolidBrush(Color.FromArgb(15, 43, 45))
            _subtitleBrush = New SolidBrush(Color.FromArgb(79, 107, 108))
            _axisBrush = New SolidBrush(Color.FromArgb(120, 130, 140))
            _chartTitleBrush = New SolidBrush(Color.FromArgb(230, 230, 230))
            _chartValueBrush = New SolidBrush(Color.FromArgb(70, 200, 210))
            _chartMsgBrush = New SolidBrush(Color.FromArgb(120, 130, 140))
            _gridPen = New Pen(Color.FromArgb(50, 58, 65), 1)
            _linePen = New Pen(Color.FromArgb(70, 200, 210), 2)
            _hasResources = True
        Catch
            _hasResources = False
        End Try
    End Sub

    Private Sub ucAdminLivePerformance_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        If perfTimer IsNot Nothing Then
            perfTimer.Stop()
            perfTimer.Dispose()
        End If
        Try
            If _titleFont IsNot Nothing Then _titleFont.Dispose()
            If _subtitleFont IsNot Nothing Then _subtitleFont.Dispose()
            If _axisFont IsNot Nothing Then _axisFont.Dispose()
            If _chartTitleFont IsNot Nothing Then _chartTitleFont.Dispose()
            If _chartValueFont IsNot Nothing Then _chartValueFont.Dispose()
            If _chartMsgFont IsNot Nothing Then _chartMsgFont.Dispose()
            If _titleBrush IsNot Nothing Then _titleBrush.Dispose()
            If _subtitleBrush IsNot Nothing Then _subtitleBrush.Dispose()
            If _axisBrush IsNot Nothing Then _axisBrush.Dispose()
            If _chartTitleBrush IsNot Nothing Then _chartTitleBrush.Dispose()
            If _chartValueBrush IsNot Nothing Then _chartValueBrush.Dispose()
            If _chartMsgBrush IsNot Nothing Then _chartMsgBrush.Dispose()
            If _gridPen IsNot Nothing Then _gridPen.Dispose()
            If _linePen IsNot Nothing Then _linePen.Dispose()
        Catch
        End Try
    End Sub

    Private Sub UpdateCharts(sender As Object, e As EventArgs)
        If Me.IsDisposed OrElse Me.Disposing Then Return
        If Me.Parent Is Nothing Then
            If perfTimer IsNot Nothing Then perfTimer.Stop()
            Return
        End If

        Try
            PerformanceSampler.TakeSample()
        Catch
        End Try

        Try
            Dim cpuData As List(Of Double) = PerformanceSampler.GetRecentValues("cpu", 30)
            Dim memData As List(Of Double) = PerformanceSampler.GetRecentValues("memory", 30)
            Dim reqData As List(Of Double) = PerformanceSampler.GetRecentValues("req", 30)
            Dim respData As List(Of Double) = PerformanceSampler.GetRecentValues("resp", 30)

            Dim memMax As Double = 100
            If memData.Count > 0 Then
                Dim m As Double = memData.Max + 50
                If m > memMax Then memMax = m
            End If

            Dim reqMax As Double = 5
            If reqData.Count > 0 Then
                Dim m As Double = reqData.Max + 2
                If m > reqMax Then reqMax = m
            End If

            Dim respMax As Double = 100
            If respData.Count > 0 Then
                Dim m As Double = respData.Max + 10
                If m > respMax Then respMax = m
            End If

            DrawChart(chartBoxes(0), "CPU", "% Utilization", cpuData, 0, 100, PerformanceSampler.CurrentCpu.ToString("F0") & "%")
            DrawChart(chartBoxes(1), "Memory", "MB used by server", memData, 0, memMax, PerformanceSampler.CurrentMemoryMB.ToString("F0") & " MB")
            DrawChart(chartBoxes(2), "Requests/sec", "Throughput", reqData, 0, reqMax, PerformanceSampler.CurrentReqPerSec.ToString("F1") & "/s")
            DrawChart(chartBoxes(3), "Response time", "Milliseconds", respData, 0, respMax, PerformanceSampler.CurrentAvgRespMs.ToString("F0") & " ms")

            statValues(0).Text = PerformanceSampler.CurrentCpu.ToString("F0") & "%"
            statValues(1).Text = PerformanceSampler.CurrentMemoryMB.ToString("F0") & " MB"
            statValues(2).Text = PerformanceSampler.CurrentReqPerSec.ToString("F1") & "/s"
            statValues(3).Text = PerformanceSampler.CurrentErrorsLastHour.ToString()
        Catch
        End Try
    End Sub

    Private Sub BuildUI()
        lblTitle = New System.Windows.Forms.Label()
        lblTitle.AutoSize = True
        lblTitle.Font = New System.Drawing.Font("Segoe UI", 24, System.Drawing.FontStyle.Bold)
        lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        lblTitle.Location = New System.Drawing.Point(30, 25)
        lblTitle.Text = "Live Performance"
        Me.Controls.Add(lblTitle)

        lblSubtitle = New System.Windows.Forms.Label()
        lblSubtitle.AutoSize = True
        lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 11)
        lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblSubtitle.Location = New System.Drawing.Point(35, 72)
        lblSubtitle.Text = "Real-time server health - refreshes every 3 seconds " & PerformanceSampler.Version
        Me.Controls.Add(lblSubtitle)

        Dim cardTitles() As String = {"CPU Usage", "Memory", "Requests/sec", "Errors (last hour)"}
        Dim cardColors() As Color = {
            System.Drawing.Color.FromArgb(1, 96, 100),
            System.Drawing.Color.FromArgb(95, 179, 179),
            System.Drawing.Color.FromArgb(27, 122, 80),
            System.Drawing.Color.FromArgb(179, 38, 30)
        }

        For i As Integer = 0 To 3
            Dim card As New RoundedPanel()
            card.CornerRadius = 14
            card.BackColor = System.Drawing.Color.White
            card.Location = New System.Drawing.Point(30 + i * 285, 110)
            card.Size = New System.Drawing.Size(270, 95)

            Dim icon As New System.Windows.Forms.Panel()
            icon.BackColor = cardColors(i)
            icon.Location = New System.Drawing.Point(20, 28)
            icon.Size = New System.Drawing.Size(40, 40)
            card.Controls.Add(icon)

            Dim lblT As New System.Windows.Forms.Label()
            lblT.AutoSize = True
            lblT.Font = New System.Drawing.Font("Segoe UI", 9)
            lblT.ForeColor = System.Drawing.Color.FromArgb(120, 130, 135)
            lblT.Location = New System.Drawing.Point(75, 22)
            lblT.Text = cardTitles(i)
            card.Controls.Add(lblT)

            Dim lblV As New System.Windows.Forms.Label()
            lblV.AutoSize = True
            lblV.Font = New System.Drawing.Font("Segoe UI", 20, System.Drawing.FontStyle.Bold)
            lblV.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
            lblV.Location = New System.Drawing.Point(73, 45)
            lblV.Text = "0"
            card.Controls.Add(lblV)

            statCards(i) = card
            statValues(i) = lblV
            Me.Controls.Add(card)
        Next

        chartBoxes(0) = CreateChartBox(New System.Drawing.Rectangle(30, 225, 555, 195))
        chartBoxes(1) = CreateChartBox(New System.Drawing.Rectangle(600, 225, 550, 195))
        chartBoxes(2) = CreateChartBox(New System.Drawing.Rectangle(30, 435, 555, 195))
        chartBoxes(3) = CreateChartBox(New System.Drawing.Rectangle(600, 435, 550, 195))
    End Sub

    Private Function CreateChartBox(rect As System.Drawing.Rectangle) As System.Windows.Forms.PictureBox
        Dim pic As New System.Windows.Forms.PictureBox()
        pic.BackColor = System.Drawing.Color.FromArgb(20, 26, 32)
        pic.Location = rect.Location
        pic.Size = rect.Size
        Me.Controls.Add(pic)
        Return pic
    End Function

    Private Sub DrawChart(pic As System.Windows.Forms.PictureBox, title As String, subtitle As String, data As List(Of Double), minVal As Double, maxVal As Double, currentText As String)
        If pic Is Nothing OrElse pic.Width <= 0 OrElse pic.Height <= 0 Then Return
        If Not _hasResources Then Return

        Dim bmp As Bitmap = Nothing
        Try
            bmp = New Bitmap(pic.Width, pic.Height)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.SmoothingMode = SmoothingMode.AntiAlias
                g.Clear(Color.FromArgb(20, 26, 32))

                Dim leftPad As Integer = 15
                Dim rightPad As Integer = 15
                Dim topPad As Integer = 55
                Dim bottomPad As Integer = 28

                Dim chartLeft As Integer = leftPad
                Dim chartRight As Integer = pic.Width - rightPad
                Dim chartTop As Integer = topPad
                Dim chartBottom As Integer = pic.Height - bottomPad
                Dim chartW As Integer = chartRight - chartLeft
                Dim chartH As Integer = chartBottom - chartTop

                g.DrawString(title, _chartTitleFont, _chartTitleBrush, leftPad, 8)
                g.DrawString(subtitle, _axisFont, _axisBrush, leftPad + 1, 32)

                Dim sz As SizeF = g.MeasureString(currentText, _chartValueFont)
                g.DrawString(currentText, _chartValueFont, _chartValueBrush, pic.Width - rightPad - sz.Width, 8)

                For i As Integer = 0 To 4
                    Dim y As Integer = chartTop + (chartH \ 4) * i
                    g.DrawLine(_gridPen, chartLeft, y, chartRight, y)
                Next

                If data Is Nothing OrElse data.Count < 2 Then
                    Dim msg As String = "v3 - no samples"
                    Dim msz As SizeF = g.MeasureString(msg, _chartMsgFont)
                    g.DrawString(msg, _chartMsgFont, _chartMsgBrush, chartLeft + (chartW - msz.Width) / 2, chartTop + (chartH - msz.Height) / 2)
                Else
                    Dim points As New List(Of PointF)()
                    Dim range As Double = maxVal - minVal
                    If range <= 0 Then range = 1

                    For i As Integer = 0 To data.Count - 1
                        Dim x As Single = chartLeft + CSng(chartW * i / (data.Count - 1))
                        Dim val As Double = data(i)
                        If val < minVal Then val = minVal
                        If val > maxVal Then val = maxVal
                        Dim y As Single = chartBottom - CSng((val - minVal) / range * chartH)
                        points.Add(New PointF(x, y))
                    Next

                    Dim areaPoints As New List(Of PointF)(points)
                    areaPoints.Add(New PointF(chartRight, chartBottom))
                    areaPoints.Add(New PointF(chartLeft, chartBottom))

                    Using gradBrush As New LinearGradientBrush(
                        New Rectangle(chartLeft, chartTop, Math.Max(1, chartW), Math.Max(1, chartH)),
                        Color.FromArgb(110, 70, 200, 210),
                        Color.FromArgb(10, 70, 200, 210),
                        LinearGradientMode.Vertical)
                        g.FillPolygon(gradBrush, areaPoints.ToArray())
                    End Using

                    g.DrawLines(_linePen, points.ToArray())
                End If

                g.DrawString("60s ago", _axisFont, _axisBrush, chartLeft, pic.Height - 20)
                Dim nowStr As String = "now"
                Dim nowSz As SizeF = g.MeasureString(nowStr, _axisFont)
                g.DrawString(nowStr, _axisFont, _axisBrush, pic.Width - rightPad - nowSz.Width, pic.Height - 20)
            End Using

            If pic.Image IsNot Nothing Then
                Dim old As Image = pic.Image
                pic.Image = Nothing
                Try
                    old.Dispose()
                Catch
                End Try
            End If

            pic.Image = bmp
            bmp = Nothing
        Catch
            If bmp IsNot Nothing Then
                Try
                    bmp.Dispose()
                Catch
                End Try
            End If
        End Try
    End Sub
End Class