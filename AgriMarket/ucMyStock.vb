Imports System.Data
Imports System.ComponentModel
Imports System.IO

Public Class ucMyStock
    Private _traderId As Integer
    Private _img1Base64 As String = ""
    Private _img2Base64 As String = ""
    Private _img3Base64 As String = ""

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property TraderId As Integer
        Get
            Return _traderId
        End Get
        Set(value As Integer)
            _traderId = value
            LoadStock()
        End Set
    End Property

    Private Async Sub ucMyStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyGridStyle()
        Await LoadCategories()
        If _traderId > 0 Then Await LoadStock()
    End Sub

    Private Sub ApplyGridStyle()
        dgvStock.BackgroundColor = Theme.Card
        dgvStock.BorderStyle = BorderStyle.None
        dgvStock.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvStock.GridColor = Theme.Border
        dgvStock.EnableHeadersVisualStyles = False
        dgvStock.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvStock.ColumnHeadersDefaultCellStyle.BackColor = Theme.Background
        dgvStock.ColumnHeadersDefaultCellStyle.ForeColor = Theme.TextMuted
        dgvStock.ColumnHeadersDefaultCellStyle.Font = Theme.FontBold
        dgvStock.ColumnHeadersDefaultCellStyle.SelectionBackColor = Theme.Background
        dgvStock.ColumnHeadersHeight = 40
        dgvStock.DefaultCellStyle.BackColor = Theme.Card
        dgvStock.DefaultCellStyle.ForeColor = Theme.TextDark
        dgvStock.DefaultCellStyle.SelectionBackColor = Theme.Tint
        dgvStock.DefaultCellStyle.SelectionForeColor = Theme.Ocean
        dgvStock.DefaultCellStyle.Font = Theme.FontMain
        dgvStock.RowTemplate.Height = 45
    End Sub

    Private Async Function LoadCategories() As Task
        Dim response As String = Await NetworkClient.SendRequestAsync("GET_CATEGORIES|")
        Dim parts As String() = response.Split("|"c)
        If parts(0) = "OK" AndAlso parts.Length > 2 Then
            cmbCategory.Items.Clear()
            For Each c In parts(2).Split("^"c)
                If c.Trim() <> "" Then cmbCategory.Items.Add(c)
            Next
        End If
    End Function

    Private Async Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Await LoadStock()
    End Sub

    Private Async Function LoadStock() As Task
        If _traderId = 0 Then Return
        Try
            Dim response As String = Await NetworkClient.SendRequestAsync($"GET_STOCK|{_traderId}")
            Dim parts As String() = response.Split("|"c)

            If parts(0) = "OK" Then
                Dim dt As New DataTable()
                dt.Columns.Add("BatchID", GetType(Integer))
                dt.Columns.Add("ItemID", GetType(Integer))
                dt.Columns.Add("Produce Name")
                dt.Columns.Add("Category")
                dt.Columns.Add("Price (UGX)")
                dt.Columns.Add("Batch Number")
                dt.Columns.Add("Available Qty")
                dt.Columns.Add("Expiry Date")
                dt.Columns.Add("Batch Status")
                dt.Columns.Add("ImagePath")

                If parts.Length > 2 Then
                    For Each r As String In parts(2).Split("^"c)
                        If r.Trim() <> "" Then
                            Dim f As String() = r.Split("~"c)
                            dt.Rows.Add(f(0), f(1), f(2), f(3), f(4), f(5), f(6), f(7), f(8), f(9))
                        End If
                    Next
                End If
                dgvStock.DataSource = dt
                dgvStock.Columns("BatchID").Visible = False
                dgvStock.Columns("ImagePath").Visible = False
                dgvStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Else
                MessageBox.Show("Error: " & parts(3), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Connection error: " & ex.Message)
        End Try
    End Function

    Private Sub btnShowAdd_Click(sender As Object, e As EventArgs) Handles btnShowAdd.Click
        panelAdd.Visible = Not panelAdd.Visible
        panelAdd.BringToFront()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        panelAdd.Visible = False
        ClearForm()
    End Sub

    ' --- Photo Upload Helpers ---
    Private Sub btnUploadPhoto_Click(sender As Object, e As EventArgs) Handles btnUploadPhoto.Click
        _img1Base64 = GetBase64FromFile(picPreview)
    End Sub

    Private Sub btnUploadPhoto2_Click(sender As Object, e As EventArgs) Handles btnUploadPhoto2.Click
        _img2Base64 = GetBase64FromFile(picPreview2)
    End Sub

    Private Sub btnUploadPhoto3_Click(sender As Object, e As EventArgs) Handles btnUploadPhoto3.Click
        _img3Base64 = GetBase64FromFile(picPreview3)
    End Sub

    Private Function GetBase64FromFile(picBox As PictureBox) As String
        Using ofd As New OpenFileDialog()
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png"
            If ofd.ShowDialog() = DialogResult.OK Then
                Try
                    Dim imageBytes As Byte() = File.ReadAllBytes(ofd.FileName)
                    ' Limit to 1MB to prevent network timeouts
                    If imageBytes.Length > 1000000 Then
                        MessageBox.Show("Image is too large. Max size is 1MB.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return ""
                    End If
                    picBox.Image = Image.FromFile(ofd.FileName)
                    Return Convert.ToBase64String(imageBytes)
                Catch ex As Exception
                    MessageBox.Show("Error loading image: " & ex.Message)
                End Try
            End If
        End Using
        Return ""
    End Function

    Private Async Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtName.Text.Trim() = "" Or cmbCategory.SelectedIndex = -1 Or txtPrice.Text = "" Or txtQty.Text = "" Or txtShelf.Text = "" Then
            MessageBox.Show("Please fill in all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If String.IsNullOrEmpty(_img1Base64) Then
            MessageBox.Show("Please upload the main photo.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            Dim price As Decimal = Convert.ToDecimal(txtPrice.Text)
            Dim qty As Integer = Convert.ToInt32(txtQty.Text)
            Dim shelf As Integer = Convert.ToInt32(txtShelf.Text)

            Dim response As String = Await NetworkClient.SendRequestAsync($"ADD_ITEM|{_traderId}|{txtName.Text.Trim()}|{cmbCategory.SelectedItem.ToString()}|{price}|{qty}|{shelf}|{_img1Base64}|{_img2Base64}|{_img3Base64}")
            Dim parts As String() = response.Split("|"c)

            If parts(0) = "OK" Then
                MessageBox.Show("Produce added successfully and a new batch was created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                panelAdd.Visible = False
                ClearForm()
                Await LoadStock()
            Else
                MessageBox.Show("Error: " & parts(3), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Please enter valid numbers for Price, Quantity, and Shelf Life.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' --- Existing functionality ---
    Private Async Sub btnTopUp_Click(sender As Object, e As EventArgs) Handles btnTopUp.Click
        If dgvStock.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an item from the grid first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim itemId As Integer = Convert.ToInt32(dgvStock.SelectedRows(0).Cells("ItemID").Value)
        Dim itemName As String = dgvStock.SelectedRows(0).Cells("Produce Name").Value.ToString()

        Dim qtyInput As String = InputBox($"How many units of {itemName} are you adding?", "Top Up Stock")
        If String.IsNullOrWhiteSpace(qtyInput) Then Exit Sub
        Dim qty As Integer
        If Not Integer.TryParse(qtyInput, qty) OrElse qty <= 0 Then
            MessageBox.Show("Please enter a valid positive number for quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim shelfInput As String = InputBox($"Enter the shelf life (in days) for this new batch of {itemName}:", "Top Up Stock")
        If String.IsNullOrWhiteSpace(shelfInput) Then Exit Sub
        Dim shelf As Integer
        If Not Integer.TryParse(shelfInput, shelf) OrElse shelf <= 0 Then
            MessageBox.Show("Please enter a valid positive number for shelf life.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim response As String = Await NetworkClient.SendRequestAsync($"TOPUP_ITEM|{itemId}|{qty}|{shelf}")
        Dim parts As String() = response.Split("|"c)

        If parts(0) = "OK" Then
            MessageBox.Show($"Stock added successfully! New Batch: {parts(2)}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Await LoadStock()
        Else
            MessageBox.Show("Error: " & parts(3), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Async Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If dgvStock.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an item from the grid first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim row As DataGridViewRow = dgvStock.SelectedRows(0)
        Dim batchId As Integer = Convert.ToInt32(row.Cells("BatchID").Value)
        Dim itemId As Integer = Convert.ToInt32(row.Cells("ItemID").Value)
        Dim name As String = row.Cells("Produce Name").Value.ToString()
        Dim price As Decimal = Convert.ToDecimal(row.Cells("Price (UGX)").Value)
        Dim batchNo As String = row.Cells("Batch Number").Value.ToString()
        Dim qty As Integer = Convert.ToInt32(row.Cells("Available Qty").Value)

        Dim editForm As New frmEditItem(itemId, name, price, batchId, batchNo, qty)
        editForm.ShowDialog()

        Await LoadStock()
    End Sub

    Private Sub ClearForm()
        txtName.Clear()
        cmbCategory.SelectedIndex = -1
        txtPrice.Clear()
        txtQty.Clear()
        txtShelf.Clear()
        _img1Base64 = ""
        _img2Base64 = ""
        _img3Base64 = ""
        picPreview.Image = Nothing
        picPreview2.Image = Nothing
        picPreview3.Image = Nothing
    End Sub
End Class