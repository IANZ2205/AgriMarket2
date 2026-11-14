Imports System.Drawing.Drawing2D

Public Class ucDonutChart
    Inherits Control

    Private _completed As Integer = 0
    Private _pending As Integer = 0
    Private _rejected As Integer = 0

    Public Sub SetData(completed As Integer, pending As Integer, rejected As Integer)
        _completed = completed
        _pending = pending
        _rejected = rejected
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        Dim total As Integer = _completed + _pending + _rejected
        Dim rect As New Rectangle(20, 20, Me.Width - 40, Me.Height - 40)

        If total = 0 Then
            ' Draw a grey placeholder ring if there are no orders
            g.DrawEllipse(New Pen(Color.LightGray, 20), rect)
        Else
            Dim startAngle As Single = -90

            ' Completed (Ocean)
            Dim sweep1 As Single = CSng((_completed / total) * 360)
            g.FillPie(New SolidBrush(Theme.Ocean), rect, startAngle, sweep1)
            startAngle += sweep1

            ' Pending (Amber)
            Dim sweep2 As Single = CSng((_pending / total) * 360)
            g.FillPie(New SolidBrush(Color.FromArgb(138, 82, 0)), rect, startAngle, sweep2)
            startAngle += sweep2

            ' Rejected (Red)
            Dim sweep3 As Single = CSng((_rejected / total) * 360)
            g.FillPie(New SolidBrush(Color.FromArgb(179, 38, 30)), rect, startAngle, sweep3)
        End If

        ' Draw the center hole to make it a Donut
        Dim holeSize As Integer = 80
        Dim holeRect As New Rectangle(Me.Width / 2 - holeSize / 2, Me.Height / 2 - holeSize / 2, holeSize, holeSize)
        g.FillEllipse(New SolidBrush(Color.White), holeRect)

        ' Draw Total in the center
        Using f As New Font("Segoe UI", 14, FontStyle.Bold)
            Dim textSize As Size = TextRenderer.MeasureText(total.ToString(), f)
            g.DrawString(total.ToString(), f, Brushes.Black, CSng(Me.Width / 2 - textSize.Width / 2), CSng(Me.Height / 2 - textSize.Height / 2 - 5))
        End Using
        Using f As New Font("Segoe UI", 8, FontStyle.Regular)
            Dim textSize As Size = TextRenderer.MeasureText("orders", f)
            g.DrawString("orders", f, Brushes.Gray, CSng(Me.Width / 2 - textSize.Width / 2), CSng(Me.Height / 2 + textSize.Height / 2 - 5))
        End Using
    End Sub
End Class