Imports System.Data

Public Class ucAdminUsers
    Private Sub ucAdminUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyGridStyle()
        LoadUsers()
    End Sub

    Private Sub ApplyGridStyle()
        dgvUsers.BackgroundColor = Theme.Card
        dgvUsers.BorderStyle = BorderStyle.None
        dgvUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvUsers.GridColor = Theme.Border
        dgvUsers.EnableHeadersVisualStyles = False
        dgvUsers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Theme.Background
        dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Theme.TextMuted
        dgvUsers.ColumnHeadersDefaultCellStyle.Font = Theme.FontBold
        dgvUsers.ColumnHeadersDefaultCellStyle.SelectionBackColor = Theme.Background
        dgvUsers.ColumnHeadersHeight = 40
        dgvUsers.DefaultCellStyle.BackColor = Theme.Card
        dgvUsers.DefaultCellStyle.ForeColor = Theme.TextDark
        dgvUsers.DefaultCellStyle.SelectionBackColor = Theme.Tint
        dgvUsers.DefaultCellStyle.SelectionForeColor = Theme.Ocean
        dgvUsers.DefaultCellStyle.Font = Theme.FontMain
        dgvUsers.RowTemplate.Height = 45
    End Sub

    Private Async Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Await LoadUsers()
    End Sub

    Private Async Function LoadUsers() As Task
        Try
            Dim response As String = Await NetworkClient.SendRequestAsync("GET_USERS|")
            Dim parts As String() = response.Split("|"c)

            If parts(0) = "OK" Then
                ' The server will send the data back as a specially formatted string
                ' Format: OK|ReqID|UserID~Name~Email~Role~Status^UserID~Name...
                Dim dt As New DataTable()
                dt.Columns.Add("UserID", GetType(Integer))
                dt.Columns.Add("Name")
                dt.Columns.Add("Email")
                dt.Columns.Add("Role")
                dt.Columns.Add("Status")

                Dim usersData As String = parts(2)
                Dim userRows As String() = usersData.Split("^"c)

                For Each uRow As String In userRows
                    If uRow.Trim() <> "" Then
                        Dim fields As String() = uRow.Split("~"c)
                        dt.Rows.Add(fields(0), fields(1), fields(2), fields(3), fields(4))
                    End If
                Next

                dgvUsers.DataSource = dt
                dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Else
                MessageBox.Show("Error loading users: " & parts(3), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Connection error: " & ex.Message)
        End Try
    End Function

    Private Async Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        If dgvUsers.SelectedRows.Count = 0 Then Exit Sub

        Dim userId As Integer = Convert.ToInt32(dgvUsers.SelectedRows(0).Cells("UserID").Value)
        Dim currentStatus As String = dgvUsers.SelectedRows(0).Cells("Status").Value.ToString()
        Dim role As String = dgvUsers.SelectedRows(0).Cells("Role").Value.ToString()

        If role <> "Trader" Then
            MessageBox.Show("Only Trader accounts need approval.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If currentStatus = "Active" Then
            MessageBox.Show("This trader is already approved.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim response As String = Await NetworkClient.SendRequestAsync($"APPROVE_USER|{userId}")
        Dim parts As String() = response.Split("|"c)

        If parts(0) = "OK" Then
            MessageBox.Show("Trader Approved Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Await LoadUsers()
        Else
            MessageBox.Show("Failed to approve: " & parts(3), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class