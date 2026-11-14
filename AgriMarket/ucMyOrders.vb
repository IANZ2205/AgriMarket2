Imports System.Data
Imports System.ComponentModel

Public Class ucMyOrders
    Private _buyerId As Integer

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property BuyerId As Integer
        Get
            Return _buyerId
        End Get
        Set(value As Integer)
            _buyerId = value
            LoadOrders()
        End Set
    End Property

    Private Sub ucMyOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyGridStyle()
        If _buyerId > 0 Then LoadOrders()
    End Sub

    Private Sub ApplyGridStyle()
        dgvMyOrders.BackgroundColor = Theme.Card
        dgvMyOrders.BorderStyle = BorderStyle.None
        dgvMyOrders.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvMyOrders.GridColor = Theme.Border
        dgvMyOrders.EnableHeadersVisualStyles = False
        dgvMyOrders.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvMyOrders.ColumnHeadersDefaultCellStyle.BackColor = Theme.Background
        dgvMyOrders.ColumnHeadersDefaultCellStyle.ForeColor = Theme.TextMuted
        dgvMyOrders.ColumnHeadersDefaultCellStyle.Font = Theme.FontBold
        dgvMyOrders.ColumnHeadersDefaultCellStyle.SelectionBackColor = Theme.Background
        dgvMyOrders.ColumnHeadersHeight = 40
        dgvMyOrders.DefaultCellStyle.BackColor = Theme.Card
        dgvMyOrders.DefaultCellStyle.ForeColor = Theme.TextDark
        dgvMyOrders.DefaultCellStyle.SelectionBackColor = Theme.Tint
        dgvMyOrders.DefaultCellStyle.SelectionForeColor = Theme.Ocean
        dgvMyOrders.DefaultCellStyle.Font = Theme.FontMain
        dgvMyOrders.RowTemplate.Height = 45
    End Sub

    Private Async Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Await LoadOrders()
    End Sub

    Private Async Function LoadOrders() As Task
        If _buyerId = 0 Then Return
        Try
            Dim response As String = Await NetworkClient.SendRequestAsync($"GET_BUYER_ORDERS|{_buyerId}")
            Dim parts As String() = response.Split("|"c)

            If parts(0) = "OK" Then
                Dim dt As New DataTable()
                dt.Columns.Add("OrderID", GetType(Integer))
                dt.Columns.Add("Produce")
                dt.Columns.Add("Trader")
                dt.Columns.Add("Qty", GetType(Double))
                dt.Columns.Add("Total (UGX)")
                dt.Columns.Add("Status")
                dt.Columns.Add("Date")

                If parts.Length > 2 Then
                    For Each r As String In parts(2).Split("^"c)
                        If r.Trim() <> "" Then
                            Dim f As String() = r.Split("~"c)
                            dt.Rows.Add(f(0), f(1), f(2), f(3), f(4), f(5), f(6))
                        End If
                    Next
                End If
                dgvMyOrders.DataSource = dt
                dgvMyOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading orders: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Async Sub btnCancelOrder_Click(sender As Object, e As EventArgs) Handles btnCancelOrder.Click
        If dgvMyOrders.SelectedRows.Count = 0 Then Exit Sub

        Dim orderId As Integer = Convert.ToInt32(dgvMyOrders.SelectedRows(0).Cells("OrderID").Value)
        Dim currentStatus As String = dgvMyOrders.SelectedRows(0).Cells("Status").Value.ToString()

        If currentStatus <> "Pending" AndAlso currentStatus <> "Confirmed" Then
            MessageBox.Show("Only Pending or Confirmed orders can be cancelled.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim reason As String = InputBox("Please enter a reason for cancelling this order:", "Cancel Order")
        If String.IsNullOrWhiteSpace(reason) Then
            MessageBox.Show("A reason is required to cancel the order.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim response As String = Await NetworkClient.SendRequestAsync($"UPDATE_ORDER_STATUS|{orderId}|Cancelled|{_buyerId}|{reason}")
        Dim parts As String() = response.Split("|"c)

        If parts(0) = "OK" Then
            MessageBox.Show("Order cancelled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Await LoadOrders()
        Else
            MessageBox.Show("Error: " & parts(3), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class