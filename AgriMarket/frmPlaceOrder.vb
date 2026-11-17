Public Class frmPlaceOrder
    Private _itemId As Integer
    Private _traderId As Integer
    Private _buyerId As Integer
    Private _price As Decimal
    Private _availableQty As Integer
    Private _itemName As String = ""

    Private lblTitle As System.Windows.Forms.Label
    Private lblItemInfo As System.Windows.Forms.Label
    Private lblAvailable As System.Windows.Forms.Label
    Private lblUnitPrice As System.Windows.Forms.Label
    Private lblQtyLabel As System.Windows.Forms.Label
    Private txtQty As System.Windows.Forms.TextBox
    Private lblNeededBy As System.Windows.Forms.Label
    Private dtpNeededBy As System.Windows.Forms.DateTimePicker
    Private lblDeliveryMode As System.Windows.Forms.Label
    Private rdoPickup As System.Windows.Forms.RadioButton
    Private rdoDelivery As System.Windows.Forms.RadioButton
    Private lblDeliveryNote As System.Windows.Forms.Label
    Private txtDeliveryNote As System.Windows.Forms.TextBox
    Private lblBuyerNote As System.Windows.Forms.Label
    Private txtBuyerNote As System.Windows.Forms.TextBox
    Private lblTotal As System.Windows.Forms.Label
    Private btnPlace As ModernButton
    Private btnCancel As ModernButton
    Private lblReminder As System.Windows.Forms.Label

    Public Sub New(itemId As Integer, traderId As Integer, buyerId As Integer, price As Decimal, availableQty As Integer)
        InitializeComponent()
        _itemId = itemId
        _traderId = traderId
        _buyerId = buyerId
        _price = price
        _availableQty = availableQty
    End Sub

    Private Async Sub frmPlaceOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildUI()
        Await LoadItemName()
        UpdateTotal()
    End Sub

    Private Async Function LoadItemName() As Task
        Try
            Dim resp As String = Await NetworkClient.SendRequestAsync($"GET_ITEM_DETAIL|{_itemId}")
            Dim parts As String() = resp.Split("|"c)
            If parts(0) = "OK" AndAlso parts.Length > 2 Then
                Dim f As String() = parts(2).Split("~"c)
                _itemName = f(1)
                lblItemInfo.Text = _itemName
                Dim unit As String = If(f.Length > 12, f(12), "units")
                lblAvailable.Text = "Available: " & _availableQty.ToString() & " " & unit
                lblUnitPrice.Text = "Unit price: UGX " & _price.ToString("N0")
            End If
        Catch
        End Try
    End Function

    Private Sub BuildUI()
        lblTitle = New System.Windows.Forms.Label()
        lblTitle.AutoSize = True
        lblTitle.Font = New System.Drawing.Font("Segoe UI", 20, System.Drawing.FontStyle.Bold)
        lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        lblTitle.Location = New System.Drawing.Point(30, 20)
        lblTitle.Text = "Place Order"
        Me.Controls.Add(lblTitle)

        lblItemInfo = New System.Windows.Forms.Label()
        lblItemInfo.AutoSize = True
        lblItemInfo.Font = New System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold)
        lblItemInfo.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        lblItemInfo.Location = New System.Drawing.Point(30, 65)
        lblItemInfo.Text = "Loading..."
        Me.Controls.Add(lblItemInfo)

        lblAvailable = New System.Windows.Forms.Label()
        lblAvailable.AutoSize = True
        lblAvailable.Font = New System.Drawing.Font("Segoe UI", 10)
        lblAvailable.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblAvailable.Location = New System.Drawing.Point(30, 95)
        lblAvailable.Text = "Available: ..."
        Me.Controls.Add(lblAvailable)

        lblUnitPrice = New System.Windows.Forms.Label()
        lblUnitPrice.AutoSize = True
        lblUnitPrice.Font = New System.Drawing.Font("Segoe UI", 10)
        lblUnitPrice.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblUnitPrice.Location = New System.Drawing.Point(30, 115)
        lblUnitPrice.Text = "Unit price: ..."
        Me.Controls.Add(lblUnitPrice)

        lblQtyLabel = New System.Windows.Forms.Label()
        lblQtyLabel.AutoSize = True
        lblQtyLabel.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        lblQtyLabel.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblQtyLabel.Location = New System.Drawing.Point(30, 155)
        lblQtyLabel.Text = "Quantity *"
        Me.Controls.Add(lblQtyLabel)

        txtQty = New System.Windows.Forms.TextBox()
        txtQty.Font = New System.Drawing.Font("Segoe UI", 12)
        txtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtQty.Location = New System.Drawing.Point(30, 178)
        txtQty.Size = New System.Drawing.Size(455, 27)
        AddHandler txtQty.TextChanged, AddressOf TxtQty_TextChanged
        Me.Controls.Add(txtQty)

        lblNeededBy = New System.Windows.Forms.Label()
        lblNeededBy.AutoSize = True
        lblNeededBy.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        lblNeededBy.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblNeededBy.Location = New System.Drawing.Point(30, 215)
        lblNeededBy.Text = "Needed by"
        Me.Controls.Add(lblNeededBy)

        dtpNeededBy = New System.Windows.Forms.DateTimePicker()
        dtpNeededBy.Font = New System.Drawing.Font("Segoe UI", 11)
        dtpNeededBy.Format = System.Windows.Forms.DateTimePickerFormat.Short
        dtpNeededBy.Location = New System.Drawing.Point(30, 238)
        dtpNeededBy.Size = New System.Drawing.Size(455, 27)
        Me.Controls.Add(dtpNeededBy)

        lblDeliveryMode = New System.Windows.Forms.Label()
        lblDeliveryMode.AutoSize = True
        lblDeliveryMode.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        lblDeliveryMode.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblDeliveryMode.Location = New System.Drawing.Point(30, 275)
        lblDeliveryMode.Text = "Delivery Mode"
        Me.Controls.Add(lblDeliveryMode)

        rdoPickup = New System.Windows.Forms.RadioButton()
        rdoPickup.Text = "Pickup"
        rdoPickup.Font = New System.Drawing.Font("Segoe UI", 11)
        rdoPickup.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        rdoPickup.Checked = True
        rdoPickup.Location = New System.Drawing.Point(30, 298)
        rdoPickup.Size = New System.Drawing.Size(120, 25)
        Me.Controls.Add(rdoPickup)

        rdoDelivery = New System.Windows.Forms.RadioButton()
        rdoDelivery.Text = "Delivery"
        rdoDelivery.Font = New System.Drawing.Font("Segoe UI", 11)
        rdoDelivery.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        rdoDelivery.Location = New System.Drawing.Point(160, 298)
        rdoDelivery.Size = New System.Drawing.Size(120, 25)
        Me.Controls.Add(rdoDelivery)

        lblDeliveryNote = New System.Windows.Forms.Label()
        lblDeliveryNote.AutoSize = True
        lblDeliveryNote.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        lblDeliveryNote.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblDeliveryNote.Location = New System.Drawing.Point(30, 335)
        lblDeliveryNote.Text = "Delivery Note (optional)"
        Me.Controls.Add(lblDeliveryNote)

        txtDeliveryNote = New System.Windows.Forms.TextBox()
        txtDeliveryNote.Font = New System.Drawing.Font("Segoe UI", 11)
        txtDeliveryNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtDeliveryNote.Location = New System.Drawing.Point(30, 358)
        txtDeliveryNote.Size = New System.Drawing.Size(455, 25)
        Me.Controls.Add(txtDeliveryNote)

        lblBuyerNote = New System.Windows.Forms.Label()
        lblBuyerNote.AutoSize = True
        lblBuyerNote.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        lblBuyerNote.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblBuyerNote.Location = New System.Drawing.Point(30, 395)
        lblBuyerNote.Text = "Note to Trader (optional)"
        Me.Controls.Add(lblBuyerNote)

        txtBuyerNote = New System.Windows.Forms.TextBox()
        txtBuyerNote.Font = New System.Drawing.Font("Segoe UI", 11)
        txtBuyerNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtBuyerNote.Location = New System.Drawing.Point(30, 418)
        txtBuyerNote.Size = New System.Drawing.Size(455, 55)
        txtBuyerNote.Multiline = True
        Me.Controls.Add(txtBuyerNote)

        lblTotal = New System.Windows.Forms.Label()
        lblTotal.AutoSize = True
        lblTotal.Font = New System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold)
        lblTotal.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        lblTotal.Location = New System.Drawing.Point(30, 490)
        lblTotal.Text = "Estimated Total: UGX 0"
        Me.Controls.Add(lblTotal)

        btnPlace = New ModernButton()
        btnPlace.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        btnPlace.ForeColor = System.Drawing.Color.White
        btnPlace.Location = New System.Drawing.Point(30, 535)
        btnPlace.Size = New System.Drawing.Size(220, 45)
        btnPlace.Text = "Place Order"
        AddHandler btnPlace.Click, AddressOf btnPlace_Click
        Me.Controls.Add(btnPlace)

        btnCancel = New ModernButton()
        btnCancel.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        btnCancel.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        btnCancel.Location = New System.Drawing.Point(265, 535)
        btnCancel.Size = New System.Drawing.Size(220, 45)
        btnCancel.Text = "Cancel"
        AddHandler btnCancel.Click, AddressOf btnCancel_Click
        Me.Controls.Add(btnCancel)

        lblReminder = New System.Windows.Forms.Label()
        lblReminder.AutoSize = False
        lblReminder.Font = New System.Drawing.Font("Segoe UI", 8, System.Drawing.FontStyle.Italic)
        lblReminder.ForeColor = System.Drawing.Color.FromArgb(120, 130, 135)
        lblReminder.Location = New System.Drawing.Point(30, 590)
        lblReminder.Size = New System.Drawing.Size(455, 50)
        lblReminder.Text = "The trader will confirm or reject your order. If they confirm, the stock is reserved for you."
        Me.Controls.Add(lblReminder)
    End Sub

    Private Sub TxtQty_TextChanged(sender As Object, e As EventArgs)
        UpdateTotal()
    End Sub

    Private Sub UpdateTotal()
        Dim q As Integer = 0
        If Integer.TryParse(txtQty.Text, q) Then
            Dim total As Decimal = q * _price
            lblTotal.Text = "Estimated Total: UGX " & total.ToString("N0")
        Else
            lblTotal.Text = "Estimated Total: UGX 0"
        End If
    End Sub

    Private Async Sub btnPlace_Click(sender As Object, e As EventArgs)
        Dim qty As Integer
        If Not Integer.TryParse(txtQty.Text, qty) Then
            MessageBox.Show("Please enter a valid quantity.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If qty <= 0 Then
            MessageBox.Show("Quantity must be greater than zero.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If qty > _availableQty Then
            MessageBox.Show("Quantity exceeds available stock (" & _availableQty & ").", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim neededBy As String = dtpNeededBy.Value.ToString("yyyy-MM-dd")
        Dim deliveryMode As String = If(rdoDelivery.Checked, "Delivery", "Pickup")
        Dim deliveryNote As String = SafeField(txtDeliveryNote.Text.Trim())
        Dim buyerNote As String = SafeField(txtBuyerNote.Text.Trim())

        btnPlace.Enabled = False
        btnPlace.Text = "Placing..."

        Dim cmd As String = $"PLACE_ORDER|{_itemId}|{_traderId}|{_buyerId}|{qty}|{_price}|{neededBy}|{deliveryMode}|{deliveryNote}|{buyerNote}"
        Dim resp As String = Await NetworkClient.SendRequestAsync(cmd)
        Dim parts As String() = resp.Split("|"c)

        btnPlace.Enabled = True
        btnPlace.Text = "Place Order"

        If parts(0) = "OK" Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            Dim msg As String = "Could not place order."
            If parts.Length > 3 Then msg = parts(3)
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Function SafeField(s As String) As String
        If String.IsNullOrEmpty(s) Then Return ""
        Return s.Replace("|"c, " ").Replace("~"c, " ").Replace("^"c, " ").Replace(ControlChars.CrLf, " ").Replace(ControlChars.Lf, " ").Replace(ControlChars.Cr, " ")
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class