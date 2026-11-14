Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.ComponentModel

Public Class ModernButton
    Inherits Button

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property BorderRadius As Integer = 8
    Private _isHovering As Boolean = False

    Public Sub New()
        Me.FlatStyle = FlatStyle.Flat
        Me.FlatAppearance.BorderSize = 0
        Me.DoubleBuffered = True ' Prevents flickering
        Me.Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)
        Me.Cursor = Cursors.Hand
        Me.Size = New Size(120, 40)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        ' Hover effect (lighten the color slightly)
        Dim currentColor As Color = If(_isHovering, ControlPaint.Light(Me.BackColor, 0.2F), Me.BackColor)

        ' Draw rounded rectangle
        Using path As GraphicsPath = GetRoundedPath(ClientRectangle, BorderRadius)
            Using brush As New SolidBrush(currentColor)
                g.FillPath(brush, path)
            End Using
        End Using

        ' Draw Text perfectly centered
        TextRenderer.DrawText(g, Me.Text, Me.Font, ClientRectangle, Me.ForeColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
    End Sub

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        _isHovering = True
        Me.Invalidate()
        MyBase.OnMouseEnter(e)
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        _isHovering = False
        Me.Invalidate()
        MyBase.OnMouseLeave(e)
    End Sub

    Private Function GetRoundedPath(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim d As Integer = radius * 2
        path.AddArc(rect.X, rect.Y, d, d, 180, 90)
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90)
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90)
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90)
        path.CloseFigure()
        Return path
    End Function
End Class