Imports System.Drawing.Drawing2D
Imports System.ComponentModel

Public Class ucStatCard
    Private _value As String = "0"
    Private _title As String = "Label"
    Private _iconColor As Color = Theme.Ocean

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property Value As String
        Get
            Return _value
        End Get
        Set(v As String)
            _value = v
            lblValue.Text = v
        End Set
    End Property

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property Title As String
        Get
            Return _title
        End Get
        Set(v As String)
            _title = v
            lblTitle.Text = v
        End Set
    End Property
    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property IconColor As Color
        Get
            Return _iconColor
        End Get
        Set(v As Color)
            _iconColor = v
            pnlIcon.BackColor = Color.White ' <--- CHANGED TO WHITE
            pnlIcon.Invalidate()
        End Set
    End Property

    Private Sub ucStatCard_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        Using path As GraphicsPath = GetRoundedPath(ClientRectangle, 15)
            Using pen As New Pen(Theme.Border, 1)
                g.DrawPath(pen, path)
            End Using
        End Using
    End Sub

    Private Sub pnlIcon_Paint(sender As Object, e As PaintEventArgs) Handles pnlIcon.Paint
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        Using brush As New SolidBrush(Color.FromArgb(40, _iconColor))
            g.FillEllipse(brush, 0, 0, pnlIcon.Width - 1, pnlIcon.Height - 1)
        End Using
        Using brush As New SolidBrush(_iconColor)
            g.FillEllipse(brush, 10, 10, 30, 30)
        End Using
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