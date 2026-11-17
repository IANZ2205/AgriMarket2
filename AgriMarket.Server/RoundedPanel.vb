Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

Public Class RoundedPanel
    Inherits Panel

    Private _cornerRadius As Integer = 12
    Private _borderColor As Color = Color.FromArgb(230, 230, 230)
    Private _showBorder As Boolean = True

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property CornerRadius As Integer
        Get
            Return _cornerRadius
        End Get
        Set(value As Integer)
            _cornerRadius = value
            UpdateRegion()
            Invalidate()
        End Set
    End Property

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property BorderColor As Color
        Get
            Return _borderColor
        End Get
        Set(value As Color)
            _borderColor = value
            Invalidate()
        End Set
    End Property

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property ShowBorder As Boolean
        Get
            Return _showBorder
        End Get
        Set(value As Boolean)
            _showBorder = value
            Invalidate()
        End Set
    End Property

    Public Sub New()
        Me.DoubleBuffered = True
        Me.BackColor = Color.White
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        UpdateRegion()
    End Sub

    Private Sub UpdateRegion()
        If Me.Width <= 0 OrElse Me.Height <= 0 Then Return
        Try
            Using path As GraphicsPath = GetRoundedPath(New Rectangle(0, 0, Me.Width, Me.Height), _cornerRadius)
                Me.Region = New Region(path)
            End Using
        Catch
        End Try
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        If Not _showBorder Then Return
        If Me.Width <= 0 OrElse Me.Height <= 0 Then Return
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        Using path As GraphicsPath = GetRoundedPath(New Rectangle(0, 0, Me.Width - 1, Me.Height - 1), _cornerRadius)
            Using pen As New Pen(_borderColor, 1)
                g.DrawPath(pen, path)
            End Using
        End Using
    End Sub

    Private Function GetRoundedPath(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim r As Integer = radius * 2
        If r > rect.Width Then r = rect.Width
        If r > rect.Height Then r = rect.Height
        path.AddArc(rect.X, rect.Y, r, r, 180, 90)
        path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90)
        path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90)
        path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90)
        path.CloseFigure()
        Return path
    End Function
End Class