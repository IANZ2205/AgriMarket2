Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.IO

Public Class ucProductCard
    Public Event OrderClicked(itemId As Integer)
    Private _itemId As Integer
    Private _traderId As Integer
    Private _price As Decimal
    Private _qty As Integer
    Private _name As String
    Private _imgPath As String = ""
    Private _imgLoaded As Boolean = False

    Public Sub Setup(id As Integer, traderId As Integer, name As String, trader As String, price As Decimal, qty As Integer, expiry As String, imgPath As String)
        _itemId = id
        _traderId = traderId
        _name = name
        _price = price
        _qty = qty
        _imgPath = imgPath

        lblName.Text = name
        lblTrader.Text = trader
        lblPrice.Text = "UGX " & price.ToString("N0")
        lblQty.Text = qty.ToString() & " available"
        btnView.Text = "View Details"

        Try
            Dim expDate As DateTime = Convert.ToDateTime(expiry)
            Dim daysLeft As Integer = (expDate - DateTime.Now.Date).Days
            If daysLeft <= 3 Then
                lblBadge.Text = daysLeft.ToString() & " days left"
                lblBadge.BackColor = Color.FromArgb(251, 227, 224)
                lblBadge.ForeColor = Color.FromArgb(179, 38, 30)
            Else
                lblBadge.Text = daysLeft.ToString() & " days left"
                lblBadge.BackColor = Color.FromArgb(221, 242, 232)
                lblBadge.ForeColor = Color.FromArgb(27, 122, 80)
            End If
        Catch
            lblBadge.Text = "Unknown"
        End Try

        If Not String.IsNullOrEmpty(_imgPath) Then
            LoadImageAsync()
        End If
    End Sub

    Private Async Sub LoadImageAsync()
        If _imgLoaded Then Return
        Try
            Dim resp As String = Await NetworkClient.SendRequestAsync($"GET_FILE|products|{_imgPath}")
            Dim parts As String() = resp.Split("|"c)
            If parts(0) = "OK" AndAlso parts.Length > 2 Then
                Dim bytes As Byte() = Convert.FromBase64String(parts(2))
                Using ms As New MemoryStream(bytes)
                    Using tmpImg As Image = Image.FromStream(ms)
                        If pnlImage.IsDisposed Then Return
                        pnlImage.BackgroundImage = New Bitmap(tmpImg)
                        pnlImage.BackgroundImageLayout = ImageLayout.Zoom
                        _imgLoaded = True
                    End Using
                End Using
            End If
        Catch
        End Try
    End Sub

    Private Sub ucProductCard_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        Using path As GraphicsPath = GetRoundedPath(ClientRectangle, 15)
            Using pen As New Pen(Theme.Border, 1)
                g.DrawPath(pen, path)
            End Using
        End Using
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        RaiseEvent OrderClicked(_itemId)
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