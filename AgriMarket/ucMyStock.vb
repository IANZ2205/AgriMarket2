Imports System.ComponentModel
Imports System.Data
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Text

Public Class ucMyStock
    Private _traderId As Integer
    Private _img1Base64 As String = ""
    Private _img2Base64 As String = ""
    Private _img3Base64 As String = ""
    Private _bannerLabel As System.Windows.Forms.Label
    Private _emptyLabel As System.Windows.Forms.Label
    Private _allUnits As New List(Of String)()
    Private _unitMap As New Dictionary(Of String, List(Of String))()
    Private _languageIds As New List(Of Integer)()

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
        BuildPolish()
        ApplyGridStyle()
        Await LoadUnits()
        Await LoadCategoryUnitMap()
        Await LoadCategories()
        Await LoadLanguages()
        If _traderId > 0 Then Await LoadStock()
    End Sub

    Private Sub BuildPolish()
        _bannerLabel = New System.Windows.Forms.Label()
        _bannerLabel.AutoSize = True
        _bannerLabel.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular)
        _bannerLabel.ForeColor = System.Drawing.Color.FromArgb(120, 130, 135)
        _bannerLabel.Location = New System.Drawing.Point(180, 42)
        _bannerLabel.Text = ""
        Me.Controls.Add(_bannerLabel)

        _emptyLabel = New System.Windows.Forms.Label()
        _emptyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        _emptyLabel.Font = New System.Drawing.Font("Segoe UI", 13, System.Drawing.FontStyle.Italic)
        _emptyLabel.ForeColor = System.Drawing.Color.FromArgb(150, 155, 160)
        _emptyLabel.BackColor = System.Drawing.Color.White
        _emptyLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        _emptyLabel.Location = New System.Drawing.Point(30, 90)
        _emptyLabel.Size = New System.Drawing.Size(1120, 500)
        _emptyLabel.Text = "No stock yet. Click '+ Add Produce' to get started."
        _emptyLabel.Visible = False
        Me.Controls.Add(_emptyLabel)
    End Sub

    Private Sub ApplyGridStyle()
        dgvStock.BackgroundColor = Theme.Card
        dgvStock.BorderStyle = BorderStyle.None
        dgvStock.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvStock.GridColor = Color.FromArgb(235, 235, 235)
        dgvStock.EnableHeadersVisualStyles = False
        dgvStock.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvStock.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(244, 249, 249)
        dgvStock.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(120, 130, 135)
        dgvStock.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
        dgvStock.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(244, 249, 249)
        dgvStock.ColumnHeadersDefaultCellStyle.Padding = New Padding(8, 0, 8, 0)
        dgvStock.ColumnHeadersHeight = 42
        dgvStock.DefaultCellStyle.BackColor = Color.White
        dgvStock.DefaultCellStyle.ForeColor = Color.FromArgb(15, 43, 45)
        dgvStock.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 243, 243)
        dgvStock.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 43, 45)
        dgvStock.DefaultCellStyle.Padding = New Padding(8, 0, 8, 0)
        dgvStock.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9)
        dgvStock.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 252, 252)
        dgvStock.RowTemplate.Height = 46
    End Sub

    Private Async Function LoadUnits() As Task
        Try
            Dim response As String = Await NetworkClient.SendRequestAsync("GET_UNITS|")
            Dim parts As String() = response.Split("|"c)
            If parts(0) = "OK" AndAlso parts.Length > 2 Then
                _allUnits.Clear()
                For Each u In parts(2).Split("^"c)
                    If u.Trim() <> "" Then _allUnits.Add(u)
                Next
            End If
        Catch
        End Try
    End Function

    Private Async Function LoadCategoryUnitMap() As Task
        Try
            Dim response As String = Await NetworkClient.SendRequestAsync("GET_CATEGORY_UNITS|")
            Dim parts As String() = response.Split("|"c)
            If parts(0) = "OK" AndAlso parts.Length > 2 Then
                _unitMap.Clear()
                For Each pair In parts(2).Split("^"c)
                    If pair.Trim() <> "" Then
                        Dim f As String() = pair.Split("~"c)
                        If f.Length >= 2 Then
                            Dim cat As String = f(0)
                            Dim unit As String = f(1)
                            If Not _unitMap.ContainsKey(cat) Then
                                _unitMap(cat) = New List(Of String)()
                            End If
                            _unitMap(cat).Add(unit)
                        End If
                    End If
                Next
            End If
        Catch
        End Try
    End Function

    Private Async Function LoadCategories() As Task
        Try
            Dim response As String = Await NetworkClient.SendRequestAsync("GET_CATEGORIES|")
            Dim parts As String() = response.Split("|"c)
            If parts(0) = "OK" AndAlso parts.Length > 2 Then
                cmbCategory.Items.Clear()
                For Each c In parts(2).Split("^"c)
                    If c.Trim() <> "" Then cmbCategory.Items.Add(c)
                Next
                If cmbCategory.Items.Count > 0 Then cmbCategory.SelectedIndex = 0
            End If
        Catch
        End Try
    End Function

    Private Async Function LoadLanguages() As Task
        Try
            Dim response As String = Await NetworkClient.SendRequestAsync("GET_LANGUAGES|")
            Dim parts As String() = response.Split("|"c)
            If parts(0) = "OK" AndAlso parts.Length > 2 Then
                _languageIds.Clear()
                Dim labels() As System.Windows.Forms.Label = {lblLang1, lblLang2, lblLang3, lblLang4}
                Dim i As Integer = 0
                For Each r In parts(2).Split("^"c)
                    If r.Trim() <> "" AndAlso i < 4 Then
                        Dim f As String() = r.Split("~"c)
                        If f.Length >= 2 Then
                            _languageIds.Add(Convert.ToInt32(f(0)))
                            labels(i).Text = f(1) & " Name (optional)"
                        End If
                        i += 1
                    End If
                Next
            End If
        Catch
        End Try
    End Function

    Private Sub cmbCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCategory.SelectedIndexChanged
        If cmbCategory.SelectedIndex < 0 Then Return
        If _allUnits.Count = 0 Then Return

        Dim catName As String = cmbCategory.SelectedItem.ToString()
        cmbUnit.Items.Clear()

        If _unitMap.ContainsKey(catName) AndAlso _unitMap(catName).Count > 0 Then
            For Each u In _unitMap(catName)
                cmbUnit.Items.Add(u)
            Next
        Else
            For Each u In _allUnits
                cmbUnit.Items.Add(u)
            Next
        End If

        If cmbUnit.Items.Count > 0 Then cmbUnit.SelectedIndex = 0
    End Sub

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
                dt.Columns.Add("Unit")
                dt.Columns.Add("Expiry Date")
                dt.Columns.Add("Batch Status")
                dt.Columns.Add("ImagePath")

                Dim expiringCount As Integer = 0
                Dim expiredCount As Integer = 0

                If parts.Length > 2 Then
                    For Each r As String In parts(2).Split("^"c)
                        If r.Trim() <> "" Then
                            Dim f As String() = r.Split("~"c)
                            Dim displayStatus As String = f(8)
                            Try
                                Dim expDate As DateTime = Convert.ToDateTime(f(7))
                                Dim daysLeft As Integer = (expDate.Date - DateTime.Now.Date).Days
                                If daysLeft <= 0 Then
                                    displayStatus = "Expired"
                                ElseIf daysLeft <= 2 Then
                                    displayStatus = "Expiring"
                                ElseIf displayStatus = "Active" OrElse displayStatus = "Fresh" Then
                                    displayStatus = "Fresh"
                                End If
                            Catch
                            End Try

                            Select Case displayStatus
                                Case "Expiring"
                                    expiringCount += 1
                                Case "Expired"
                                    expiredCount += 1
                            End Select

                            Dim unit As String = If(f.Length > 10, f(10), "kg")
                            dt.Rows.Add(f(0), f(1), f(2), f(3), f(4), f(5), f(6), unit, f(7), displayStatus, f(9))
                        End If
                    Next
                End If

                dgvStock.DataSource = dt
                If dgvStock.Columns.Contains("BatchID") Then dgvStock.Columns("BatchID").Visible = False
                If dgvStock.Columns.Contains("ItemID") Then dgvStock.Columns("ItemID").Visible = False
                If dgvStock.Columns.Contains("ImagePath") Then dgvStock.Columns("ImagePath").Visible = False
                dgvStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                If dgvStock.Columns.Contains("Produce Name") Then
                    dgvStock.Columns("Produce Name").DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
                End If
                If dgvStock.Columns.Contains("Price (UGX)") Then
                    dgvStock.Columns("Price (UGX)").DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
                    dgvStock.Columns("Price (UGX)").DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
                End If
                If dgvStock.Columns.Contains("Unit") Then
                    dgvStock.Columns("Unit").Width = 70
                End If

                Dim bannerParts As New List(Of String)
                bannerParts.Add(dt.Rows.Count.ToString() & " batch" & If(dt.Rows.Count = 1, "", "es"))
                If expiringCount > 0 Then bannerParts.Add(expiringCount.ToString() & " expiring soon")
                If expiredCount > 0 Then bannerParts.Add(expiredCount.ToString() & " expired")
                _bannerLabel.Text = String.Join("   •   ", bannerParts)

                If expiredCount > 0 Then
                    _bannerLabel.ForeColor = System.Drawing.Color.FromArgb(179, 38, 30)
                ElseIf expiringCount > 0 Then
                    _bannerLabel.ForeColor = System.Drawing.Color.FromArgb(138, 82, 0)
                Else
                    _bannerLabel.ForeColor = System.Drawing.Color.FromArgb(27, 122, 80)
                End If

                If dt.Rows.Count = 0 Then
                    dgvStock.Visible = False
                    _emptyLabel.Visible = True
                    _emptyLabel.BringToFront()
                Else
                    dgvStock.Visible = True
                    _emptyLabel.Visible = False
                End If
            Else
                MessageBox.Show("Error: " & parts(3), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Connection error: " & ex.Message)
        End Try
    End Function

    Private Sub dgvStock_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvStock.CellPainting
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        If dgvStock.Columns(e.ColumnIndex).Name <> "Batch Status" Then Return
        If e.Value Is Nothing Then Return

        e.PaintBackground(e.CellBounds, True)

        Dim status As String = e.Value.ToString()
        Dim bgColor As Color
        Dim fgColor As Color

        Select Case status
            Case "Fresh", "Active"
                bgColor = Color.FromArgb(221, 242, 232)
                fgColor = Color.FromArgb(27, 122, 80)
            Case "Expiring"
                bgColor = Color.FromArgb(255, 241, 214)
                fgColor = Color.FromArgb(138, 82, 0)
            Case "Expired"
                bgColor = Color.FromArgb(251, 227, 224)
                fgColor = Color.FromArgb(179, 38, 30)
            Case Else
                bgColor = Color.FromArgb(240, 240, 240)
                fgColor = Color.FromArgb(15, 43, 45)
        End Select

        Dim chipWidth As Integer = Math.Min(95, e.CellBounds.Width - 10)
        Dim chipHeight As Integer = 24
        Dim chipX As Integer = e.CellBounds.X + (e.CellBounds.Width - chipWidth) \ 2
        Dim chipY As Integer = e.CellBounds.Y + (e.CellBounds.Height - chipHeight) \ 2
        Dim chipRect As New Rectangle(chipX, chipY, chipWidth, chipHeight)

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        Using path As GraphicsPath = RoundedRect(chipRect, 12)
            Using brush As New SolidBrush(bgColor)
                e.Graphics.FillPath(brush, path)
            End Using
        End Using

        Using f As New System.Drawing.Font("Segoe UI", 8, System.Drawing.FontStyle.Bold)
            TextRenderer.DrawText(e.Graphics, status, f, chipRect, fgColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
        End Using

        e.Handled = True
    End Sub

    Private Function RoundedRect(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim d As Integer = radius * 2
        path.AddArc(rect.X, rect.Y, d, d, 180, 90)
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90)
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90)
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90)
        path.CloseFigure()
        Return path
    End Function

    Private Function SafeText(s As String) As String
        If String.IsNullOrEmpty(s) Then Return ""
        Return s.Replace("|"c, " ").Replace("~"c, " ").Replace("^"c, " ").Replace(ControlChars.CrLf, " ").Replace(ControlChars.Lf, " ").Replace(ControlChars.Cr, " ")
    End Function

    Private Sub btnShowAdd_Click(sender As Object, e As EventArgs) Handles btnShowAdd.Click
        panelAdd.Visible = Not panelAdd.Visible
        panelAdd.BringToFront()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        panelAdd.Visible = False
        ClearForm()
    End Sub

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
                    If imageBytes.Length > 5000000 Then
                        MessageBox.Show("Image is too large. Max size is 5MB.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
        If txtName.Text.Trim() = "" Or cmbCategory.SelectedIndex = -1 Or cmbUnit.SelectedIndex = -1 Or txtPrice.Text = "" Or txtQty.Text = "" Or txtShelf.Text = "" Then
            MessageBox.Show("Please fill in all required fields (Name, Category, Unit, Price, Quantity, Shelf Life).", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
            Dim unit As String = SafeText(cmbUnit.SelectedItem.ToString())
            Dim lowStock As Integer = 10
            Try
                lowStock = Convert.ToInt32(txtLowStock.Text)
            Catch
            End Try
            If lowStock <= 0 Then lowStock = 10
            Dim description As String = SafeText(txtDescription.Text.Trim())

            Dim langBoxes() As System.Windows.Forms.TextBox = {txtLang1, txtLang2, txtLang3, txtLang4}
            Dim blob As New StringBuilder()
            For i As Integer = 0 To 3
                Dim langName As String = langBoxes(i).Text.Trim()
                If Not String.IsNullOrEmpty(langName) AndAlso i < _languageIds.Count Then
                    If blob.Length > 0 Then blob.Append("^")
                    blob.Append(_languageIds(i)).Append("~").Append(SafeText(langName))
                End If
            Next

            Dim cmd As String = $"ADD_ITEM|{_traderId}|{SafeText(txtName.Text.Trim())}|{SafeText(cmbCategory.SelectedItem.ToString())}|{price}|{unit}|{lowStock}|{description}|{qty}|{shelf}|{_img1Base64}|{_img2Base64}|{_img3Base64}|{blob.ToString()}"

            Dim response As String = Await NetworkClient.SendRequestAsync(cmd)
            Dim parts As String() = response.Split("|"c)

            If parts(0) = "OK" Then
                MessageBox.Show("Produce added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Async Sub btnTopUp_Click(sender As Object, e As EventArgs) Handles btnTopUp.Click
        If dgvStock.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an item from the grid first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim itemId As Integer = Convert.ToInt32(dgvStock.SelectedRows(0).Cells("ItemID").Value)
        Dim itemName As String = dgvStock.SelectedRows(0).Cells("Produce Name").Value.ToString()
        Dim itemUnit As String = dgvStock.SelectedRows(0).Cells("Unit").Value.ToString()

        Dim qtyInput As String = InputBox($"How many {itemUnit} of {itemName} are you adding?", "Top Up Stock")
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
        If cmbCategory.Items.Count > 0 Then cmbCategory.SelectedIndex = 0 Else cmbCategory.SelectedIndex = -1
        If cmbUnit.Items.Count > 0 Then cmbUnit.SelectedIndex = 0 Else cmbUnit.SelectedIndex = -1
        txtPrice.Clear()
        txtLowStock.Text = "10"
        txtQty.Clear()
        txtShelf.Clear()
        txtDescription.Clear()
        txtLang1.Clear()
        txtLang2.Clear()
        txtLang3.Clear()
        txtLang4.Clear()
        _img1Base64 = ""
        _img2Base64 = ""
        _img3Base64 = ""
        picPreview.Image = Nothing
        picPreview2.Image = Nothing
        picPreview3.Image = Nothing
    End Sub
End Class