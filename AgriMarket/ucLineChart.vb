Imports System.Drawing.Drawing2D

Public Class ucLineChart
    Inherits Control

    Private _values As New List(Of Integer)()

    Public Sub SetData(values As List(Of Integer))
        _values = values
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        ' Draw grid lines (FIXED: Corrected Using/Next structure)
        Using p As New Pen(Theme.Border, 1)
            For i As Integer = 1 To 4
                Dim y As Integer = (Me.Height / 5) * i
                g.DrawLine(p, 30, y, Me.Width - 10, y)
            Next
        End Using

        If _values.Count < 2 Then Return

        ' Find max value for scaling
        Dim maxVal As Integer = 1
        For Each v As Integer In _values
            If v > maxVal Then maxVal = v
        Next

        ' Calculate points
        Dim points As New List(Of Point)()
        Dim stepX As Single = CSng((Me.Width - 40) / (_values.Count - 1))
        Dim scaleY As Single = CSng((Me.Height - 30) / maxVal)

        For i As Integer = 0 To _values.Count - 1
            Dim x As Integer = CInt(30 + (i * stepX))
            Dim y As Integer = CInt(Me.Height - 10 - (_values(i) * scaleY))
            points.Add(New Point(x, y))
        Next

        ' Draw the line
        Using p As New Pen(Theme.Ocean, 3)
            g.DrawLines(p, points.ToArray())
        End Using

        ' Draw points
        For Each pt In points
            g.FillEllipse(Brushes.White, pt.X - 4, pt.Y - 4, 8, 8)
            g.DrawEllipse(New Pen(Theme.Ocean, 2), pt.X - 4, pt.Y - 4, 8, 8)
        Next
    End Sub
End Class