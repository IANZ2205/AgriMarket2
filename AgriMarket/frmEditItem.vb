Public Class frmEditItem
    Private _itemId As Integer
    Private _batchId As Integer
    Private _batchNo As String

    ' Pass in the data from the My Stock screen
    Public Sub New(itemId As Integer, name As String, price As Decimal, batchId As Integer, batchNo As String, currentQty As Integer)
        InitializeComponent()
        _itemId = itemId
        _batchId = batchId
        _batchNo = batchNo

        ' Populate the Item Details
        txtName.Text = name
        txtPrice.Text = price.ToString()

        ' Populate the Batch Details
        lblBatchNo.Text = "Batch: " & batchNo
        txtNewQty.Text = currentQty.ToString()
    End Sub

    Private Async Sub btnSaveItem_Click(sender As Object, e As EventArgs) Handles btnSaveItem.Click
        Dim price As Decimal
        If Not Decimal.TryParse(txtPrice.Text, price) Then
            MessageBox.Show("Please enter a valid price.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim response As String = Await NetworkClient.SendRequestAsync($"UPDATE_ITEM|{_itemId}|{txtName.Text.Trim()}|{price}|{txtDesc.Text.Trim()}")
        Dim parts As String() = response.Split("|"c)

        If parts(0) = "OK" Then
            MessageBox.Show("Item details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Error: " & parts(3), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Async Sub btnAdjustBatch_Click(sender As Object, e As EventArgs) Handles btnAdjustBatch.Click
        If String.IsNullOrWhiteSpace(txtReason.Text) Then
            MessageBox.Show("A reason is required for adjusting batch quantities.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim qty As Integer
        If Not Integer.TryParse(txtNewQty.Text, qty) OrElse qty < 0 Then
            MessageBox.Show("Please enter a valid positive quantity.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim response As String = Await NetworkClient.SendRequestAsync($"ADJUST_BATCH|{_batchId}|{qty}|{txtReason.Text.Trim()}")
        Dim parts As String() = response.Split("|"c)

        If parts(0) = "OK" Then
            MessageBox.Show("Batch quantity adjusted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Else
            MessageBox.Show("Error: " & parts(3), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class