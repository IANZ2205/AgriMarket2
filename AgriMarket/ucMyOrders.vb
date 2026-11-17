Imports System.ComponentModel
Imports System.Data
Imports System.IO

Public Class ucMyOrders
    Private _buyerId As Integer
    Private _currentTab As String = "All"
    Private _allOrders As DataTable

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property BuyerId As Integer
        Get
            Return _buyerId
        End Get
        Set(value As Integer)
            ' We just save the ID. We DO NOT load data here. 
            ' Loading here causes the freeze because the UI isn't ready yet.
            _buyerId = value
        End Set
    End Property

    Private Sub ucMyOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyGridStyle()
        StyleTab(btnTabAll, True)
        StyleTab(btnTabPending, False)
        StyleTab(btnTabOngoing, False)
        StyleTab(btnTabCompleted, False)
        StyleTab(btnTabClosed, False)

        ' NOW we load the data. The screen is fully built.
        If _buyerId > 0 Then
            Dim t As Task = LoadOrders()
        End If
    End Sub

    Private Sub ApplyGridStyle()
        dgvMyOrders.BackgroundColor = Theme.Card
        dgvMyOrders.BorderStyle = BorderStyle.None
        dgvMyOrders.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvMyOrders.GridColor = Color.FromArgb(235, 235, 235)
        dgvMyOrders.EnableHeadersVisualStyles = False
        dgvMyOrders.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvMyOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(244, 249, 249)
        dgvMyOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(120, 130, 135)
        dgvMyOrders.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        dgvMyOrders.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(244, 249, 249)
        dgvMyOrders.ColumnHeadersHeight = 40
        dgvMyOrders.DefaultCellStyle.BackColor = Color.White
        dgvMyOrders.DefaultCellStyle.ForeColor = Color.FromArgb(15, 43, 45)
        dgvMyOrders.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 243, 243)
        dgvMyOrders.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 43, 45)
        dgvMyOrders.DefaultCellStyle.Font = New Font("Segoe UI", 9)
        dgvMyOrders.RowTemplate.Height = 42
    End Sub

    Private Sub StyleTab(btn As ModernButton, active As Boolean)
        If active Then
            btn.BackColor = Color.FromArgb(1, 96, 100)
            btn.ForeColor = Color.White
        Else
            btn.BackColor = Color.White
            btn.ForeColor = Color.FromArgb(1, 96, 100)
        End If
    End Sub

    Private Sub SetActiveTab(tab As String)
        _currentTab = tab
        StyleTab(btnTabAll, tab = "All")
        StyleTab(btnTabPending, tab = "Pending")
        StyleTab(btnTabOngoing, tab = "Ongoing")
        StyleTab(btnTabCompleted, tab = "Completed")
        StyleTab(btnTabClosed, tab = "Closed")
        ApplyFilter()
    End Sub

    Private Function MatchesTab(status As String) As Boolean
        Dim s As String = status.Trim()
        Select Case _currentTab
            Case "All"
                Return True
            Case "Pending"
                Return s.Equals("Pending", StringComparison.OrdinalIgnoreCase)
            Case "Ongoing"
                Return s.Equals("Confirmed", StringComparison.OrdinalIgnoreCase)
            Case "Completed"
                Return s.Equals("Completed", StringComparison.OrdinalIgnoreCase)
            Case "Closed"
                Return s.Equals("Rejected", StringComparison.OrdinalIgnoreCase) OrElse s.Equals("Cancelled", StringComparison.OrdinalIgnoreCase)
        End Select
        Return False
    End Function

    Private Sub ApplyFilter()
        If _allOrders Is Nothing Then Return

        Dim filtered As DataTable = _allOrders.Clone()
        For Each row As DataRow In _allOrders.Rows
            If MatchesTab(row("Status").ToString()) Then
                filtered.ImportRow(row)
            End If
        Next

        If filtered.Rows.Count = 0 Then
            dgvMyOrders.Visible = False
            lblEmpty.Visible = True
            lblEmpty.BringToFront()
        Else
            dgvMyOrders.Visible = True
            lblEmpty.Visible = False
        End If

        dgvMyOrders.DataSource = filtered

        If dgvMyOrders.Columns.Contains("OrderID") Then dgvMyOrders.Columns("OrderID").Width = 80
        If dgvMyOrders.Columns.Contains("ItemID") Then dgvMyOrders.Columns("ItemID").Visible = False
        If dgvMyOrders.Columns.Contains("Produce") Then dgvMyOrders.Columns("Produce").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        If dgvMyOrders.Columns.Contains("Trader") Then dgvMyOrders.Columns("Trader").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        If dgvMyOrders.Columns.Contains("Qty") Then dgvMyOrders.Columns("Qty").Width = 70
        If dgvMyOrders.Columns.Contains("Total (UGX)") Then dgvMyOrders.Columns("Total (UGX)").Width = 120
        If dgvMyOrders.Columns.Contains("Status") Then dgvMyOrders.Columns("Status").Width = 110
        If dgvMyOrders.Columns.Contains("Date") Then dgvMyOrders.Columns("Date").Width = 140

        UpdateActionButtons()
    End Sub

    Private Sub UpdateActionButtons()
        Dim status As String = ""
        If dgvMyOrders.Visible AndAlso dgvMyOrders.SelectedRows.Count > 0 Then
            Try
                status = dgvMyOrders.SelectedRows(0).Cells("Status").Value.ToString()
            Catch
            End Try
        End If

        btnReorder.Enabled = (status = "Rejected" OrElse status = "Cancelled")
        btnCancelOrder.Enabled = (status = "Pending" OrElse status = "Confirmed")
    End Sub

    Private Async Function LoadOrders() As Task
        If _buyerId = 0 Then Return
        Try
            Dim response As String = Await NetworkClient.SendRequestAsync($"GET_BUYER_ORDERS|{_buyerId}")
            Dim parts As String() = response.Split("|"c)

            If parts(0) = "OK" Then
                Dim dt As New DataTable()
                dt.Columns.Add("OrderID", GetType(Integer))
                dt.Columns.Add("ItemID", GetType(Integer))
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
                            If f.Length >= 8 Then
                                dt.Rows.Add(f(0), f(1), f(2), f(3), f(4), f(5), f(6), f(7))
                            End If
                        End If
                    Next
                End If

                _allOrders = dt
                ApplyFilter()
            Else
                MessageBox.Show("Server returned an error: " & response, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading orders: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Async Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Await LoadOrders()
    End Sub

    Private Sub btnTabAll_Click(sender As Object, e As EventArgs) Handles btnTabAll.Click
        SetActiveTab("All")
    End Sub

    Private Sub btnTabPending_Click(sender As Object, e As EventArgs) Handles btnTabPending.Click
        SetActiveTab("Pending")
    End Sub

    Private Sub btnTabOngoing_Click(sender As Object, e As EventArgs) Handles btnTabOngoing.Click
        SetActiveTab("Ongoing")
    End Sub

    Private Sub btnTabCompleted_Click(sender As Object, e As EventArgs) Handles btnTabCompleted.Click
        SetActiveTab("Completed")
    End Sub

    Private Sub btnTabClosed_Click(sender As Object, e As EventArgs) Handles btnTabClosed.Click
        SetActiveTab("Closed")
    End Sub

    Private Sub dgvMyOrders_SelectionChanged(sender As Object, e As EventArgs) Handles dgvMyOrders.SelectionChanged
        UpdateActionButtons()
    End Sub

    Private Async Sub btnCancelOrder_Click(sender As Object, e As EventArgs) Handles btnCancelOrder.Click
        If dgvMyOrders.SelectedRows.Count = 0 Then Exit Sub
        Dim orderId As Integer = Convert.ToInt32(dgvMyOrders.SelectedRows(0).Cells("OrderID").Value)
        Dim currentStatus As String = dgvMyOrders.SelectedRows(0).Cells("Status").Value.ToString()

        If currentStatus <> "Pending" AndAlso currentStatus <> "Confirmed" Then
            MessageBox.Show("Only Pending or Confirmed orders can be cancelled.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim reason As String = InputBox("Please enter a reason for cancelling this order:", "Cancel Order")
        If String.IsNullOrWhiteSpace(reason) Then Exit Sub

        Dim safeReason As String = reason.Trim().Replace("|"c, " ").Replace("~"c, " ").Replace("^"c, " ")
        Dim response As String = Await NetworkClient.SendRequestAsync($"UPDATE_ORDER_STATUS|{orderId}|Cancelled|{_buyerId}|{safeReason}")
        Dim parts As String() = response.Split("|"c)

        If parts(0) = "OK" Then
            MessageBox.Show("Order cancelled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Await LoadOrders()
        Else
            Dim msg As String = "Error"
            If parts.Length > 3 Then msg = parts(3)
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Async Sub btnReorder_Click(sender As Object, e As EventArgs) Handles btnReorder.Click
        If dgvMyOrders.SelectedRows.Count = 0 Then Exit Sub
        Dim row As DataGridViewRow = dgvMyOrders.SelectedRows(0)
        Dim status As String = row.Cells("Status").Value.ToString()

        If status <> "Rejected" AndAlso status <> "Cancelled" Then
            MessageBox.Show("Only rejected or cancelled orders can be reordered.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim itemId As Integer = Convert.ToInt32(row.Cells("ItemID").Value)

        btnReorder.Enabled = False
        Dim resp As String = Await NetworkClient.SendRequestAsync($"GET_ITEM_DETAIL|{itemId}")
        btnReorder.Enabled = True

        Dim parts As String() = resp.Split("|"c)
        If parts(0) = "OK" AndAlso parts.Length > 2 Then
            Dim f As String() = parts(2).Split("~"c)
            Dim traderId As Integer = Convert.ToInt32(f(7))
            Dim currentPrice As Decimal = Convert.ToDecimal(f(3))
            Dim availableQty As Integer = Convert.ToInt32(f(4))

            If availableQty <= 0 Then
                MessageBox.Show("This item is no longer in stock. You cannot reorder it.", "Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim orderForm As New frmPlaceOrder(itemId, traderId, _buyerId, currentPrice, availableQty)
            If orderForm.ShowDialog() = DialogResult.OK Then
                MessageBox.Show("Order placed successfully! The trader has been notified.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Await LoadOrders()
            End If
        Else
            MessageBox.Show("Could not fetch item details. The item may no longer exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class