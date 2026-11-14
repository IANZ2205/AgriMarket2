Imports System.Data
Imports System.ComponentModel

Public Class ucNotifications
    Private _userId As Integer

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property UserId As Integer
        Get
            Return _userId
        End Get
        Set(value As Integer)
            _userId = value
            LoadNotifications()
        End Set
    End Property

    Private Sub ucNotifications_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyGridStyle()
        If _userId > 0 Then LoadNotifications()
    End Sub

    Private Sub ApplyGridStyle()
        dgvNotifs.BackgroundColor = Theme.Card
        dgvNotifs.BorderStyle = BorderStyle.None
        dgvNotifs.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvNotifs.GridColor = Theme.Border
        dgvNotifs.EnableHeadersVisualStyles = False
        dgvNotifs.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvNotifs.ColumnHeadersDefaultCellStyle.BackColor = Theme.Background
        dgvNotifs.ColumnHeadersDefaultCellStyle.ForeColor = Theme.TextMuted
        dgvNotifs.ColumnHeadersDefaultCellStyle.Font = Theme.FontBold
        dgvNotifs.ColumnHeadersDefaultCellStyle.SelectionBackColor = Theme.Background
        dgvNotifs.ColumnHeadersHeight = 40
        dgvNotifs.DefaultCellStyle.BackColor = Theme.Card
        dgvNotifs.DefaultCellStyle.ForeColor = Theme.TextDark
        dgvNotifs.DefaultCellStyle.SelectionBackColor = Theme.Tint
        dgvNotifs.DefaultCellStyle.SelectionForeColor = Theme.Ocean
        dgvNotifs.DefaultCellStyle.Font = Theme.FontMain
        dgvNotifs.RowTemplate.Height = 45
    End Sub

    Private Async Function LoadNotifications() As Task
        Dim response As String = Await NetworkClient.SendRequestAsync($"GET_NOTIFICATIONS|{_userId}")
        Dim parts As String() = response.Split("|"c)

        If parts(0) = "OK" Then
            Dim dt As New DataTable()
            dt.Columns.Add("ID", GetType(Integer))
            dt.Columns.Add("Type")
            dt.Columns.Add("Title")
            dt.Columns.Add("Message")
            dt.Columns.Add("Read")
            dt.Columns.Add("Date")

            If parts.Length > 2 Then
                For Each r As String In parts(2).Split("^"c)
                    If r.Trim() <> "" Then
                        Dim f As String() = r.Split("~"c)
                        dt.Rows.Add(f(0), f(1), f(2), f(3), If(f(4) = "1", "Yes", "No"), f(5))
                    End If
                Next
            End If
            dgvNotifs.DataSource = dt
            dgvNotifs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End If
    End Function

    Private Async Sub btnMarkRead_Click(sender As Object, e As EventArgs) Handles btnMarkRead.Click
        Await NetworkClient.SendRequestAsync($"MARK_NOTIFS_READ|{_userId}")
        Await LoadNotifications()
    End Sub
End Class