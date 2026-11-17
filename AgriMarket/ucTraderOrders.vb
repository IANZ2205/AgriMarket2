Imports System.Data
Imports System.ComponentModel

Partial Public Class ucTraderOrders
    Private _traderId As Integer

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property TraderId As Integer
        Get
            Return _traderId
        End Get
        Set(value As Integer)
            _traderId = value
            LoadOrders()
        End Set
    End Property

    Private Async Sub ucTraderOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyGridStyle()
        If _traderId > 0 Then Await LoadOrders()
    End Sub

    Private Sub ApplyGridStyle()
        dgvOrders.BackgroundColor = Theme.Card
        dgvOrders.BorderStyle = BorderStyle.None
        dgvOrders.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvOrders.GridColor = Theme.Border
        dgvOrders.EnableHeadersVisualStyles = False
        dgvOrders.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = Theme.Background
        dgvOrders.ColumnHeadersDefaultCellStyle.ForeColor = Theme.TextMuted
        dgvOrders.ColumnHeadersDefaultCellStyle.Font = Theme.FontBold
        dgvOrders.ColumnHeadersDefaultCellStyle.SelectionBackColor = Theme.Background
        dgvOrders.ColumnHeadersHeight = 40
        dgvOrders.DefaultCellStyle.BackColor = Theme.Card
        dgvOrders.DefaultCellStyle.ForeColor = Theme.TextDark
        dgvOrders.DefaultCellStyle.SelectionBackColor = Theme.Tint
        dgvOrders.DefaultCellStyle.SelectionForeColor = Theme.Ocean
        dgvOrders.DefaultCellStyle.Font = Theme.FontMain
        dgvOrders.RowTemplate.Height = 45
    End Sub

    Private Async Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Await LoadOrders()
    End Sub

    Private Async Function LoadOrders() As Task
        If _traderId = 0 Then Return
        Try
            Dim response As String = Await NetworkClient.SendRequestAsync($"GET_TRADER_ORDERS|{_traderId}")
            Dim parts As String() = response.Split("|"c)

            If parts(0) = "OK" Then
                Dim dt As New DataTable()
                dt.Columns.Add("OrderID", GetType(Integer))
                dt.Columns.Add("Produce")
                dt.Columns.Add("Buyer")
                dt.Columns.Add("Qty", GetType(Double))
                dt.Columns.Add("Total (UGX)")
                dt.Columns.Add("Status")

                If parts.Length > 2 Then
                    For Each r As String In parts(2).Split("^"c)
                        If r.Trim() <> "" Then
                            Dim f As String() = r.Split("~"c)
                            dt.Rows.Add(f(0), f(1), f(2), f(3), f(4), f(5))
                        End If
                    Next
                End If
                dgvOrders.DataSource = dt
                dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading orders: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Async Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If dgvOrders.SelectedRows.Count = 0 Then Exit Sub
        Dim orderId As Integer = Convert.ToInt32(dgvOrders.SelectedRows(0).Cells("OrderID").Value)
        Dim currentStatus As String = dgvOrders.SelectedRows(0).Cells("Status").Value.ToString()

        If currentStatus <> "Pending" Then
            MessageBox.Show("Only Pending orders can be confirmed.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim response As String = Await NetworkClient.SendRequestAsync($"UPDATE_ORDER_STATUS|{orderId}|Confirmed|{_traderId}")
        Dim parts As String() = response.Split("|"c)
        If parts(0) = "OK" Then
            MessageBox.Show("Order Confirmed! Stock has been reserved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Await LoadOrders()
        Else
            MessageBox.Show("Error: " & parts(3), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Async Sub btnComplete_Click(sender As Object, e As EventArgs) Handles btnComplete.Click
        If dgvOrders.SelectedRows.Count = 0 Then Exit Sub
        Dim orderId As Integer = Convert.ToInt32(dgvOrders.SelectedRows(0).Cells("OrderID").Value)
        Dim currentStatus As String = dgvOrders.SelectedRows(0).Cells("Status").Value.ToString()

        If currentStatus <> "Confirmed" Then
            MessageBox.Show("Only Confirmed orders can be completed.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim response As String = Await NetworkClient.SendRequestAsync($"UPDATE_ORDER_STATUS|{orderId}|Completed|{_traderId}|")
        Dim parts As String() = response.Split("|"c)
        If parts(0) = "OK" Then
            MessageBox.Show("Order Completed! Stock has been deducted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Await LoadOrders()
        Else
            MessageBox.Show("Error: " & parts(3), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Async Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        If dgvOrders.SelectedRows.Count = 0 Then Exit Sub
        Dim orderId As Integer = Convert.ToInt32(dgvOrders.SelectedRows(0).Cells("OrderID").Value)
        Dim currentStatus As String = dgvOrders.SelectedRows(0).Cells("Status").Value.ToString()

        If currentStatus <> "Pending" Then
            MessageBox.Show("Only Pending orders can be rejected.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Ask for a reason
        Dim reason As String = InputBox("Please enter the reason for rejecting this order:", "Reject Order")
        If String.IsNullOrWhiteSpace(reason) Then
            MessageBox.Show("You must provide a reason to reject the order.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim response As String = Await NetworkClient.SendRequestAsync($"UPDATE_ORDER_STATUS|{orderId}|Rejected|{_traderId}|{reason}")
        Dim parts As String() = response.Split("|"c)

        If parts(0) = "OK" Then
            MessageBox.Show("Order Rejected. The buyer has been notified.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Await LoadOrders()
        Else
            MessageBox.Show("Error: " & parts(3), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class