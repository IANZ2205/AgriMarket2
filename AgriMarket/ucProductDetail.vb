Imports System.IO

Public Class ucProductDetail
    Private _itemId As Integer
    Private _traderId As Integer
    Private _buyerId As Integer
    Private _price As Decimal
    Private _availableQty As Integer

    Public Event BackClicked()
    Public Event PlaceOrderClicked(itemId As Integer, traderId As Integer, price As Decimal, qty As Integer)
    Public Event ChatClicked(traderId As Integer)

    Public Sub LoadItem(id As Integer, buyerId As Integer)
        _itemId = id
        _buyerId = buyerId
        _LoadData()
    End Sub

    Private Async Sub _LoadData()
        Dim response As String = Await NetworkClient.SendRequestAsync($"GET_ITEM_DETAIL|{_itemId}")
        Dim parts As String() = response.Split("|"c)

        If parts(0) = "OK" Then
            Dim f As String() = parts(2).Split("~"c)
            ' Format: ItemID~Produce~Category~Price~Qty~Expiry~Description~TraderID~TraderName~TraderPhone~TraderDistrict~ImagePath

            _traderId = Convert.ToInt32(f(7))
            _price = Convert.ToDecimal(f(3))
            _availableQty = Convert.ToInt32(f(4))

            lblName.Text = f(1)
            lblCategory.Text = f(2)
            lblPrice.Text = "UGX " & _price.ToString("N0")
            lblQty.Text = _availableQty.ToString() & " available"
            lblDesc.Text = If(String.IsNullOrEmpty(f(6)), "No description provided.", f(6))

            lblTraderName.Text = "Name: " & f(8)
            lblTraderPhone.Text = "Phone: " & f(9)
            lblTraderDistrict.Text = "District: " & f(10)

            ' Load Image (Simplified: Assumes image is in Server's folder, not client)
            ' In a fully networked app, we'd download this via FILE_GET
            Try
                Dim imgPath As String = f(11)
                If Not String.IsNullOrEmpty(imgPath) Then
                    Dim fullPath As String = Path.Combine(Application.StartupPath, "Files", "products", imgPath)
                    ' Note: The client won't have the server's images unless they are copied. 
                    ' For this demo, if the file isn't found, we just leave it blank.
                    If File.Exists(fullPath) Then
                        picImage.Image = Image.FromFile(fullPath)
                    End If
                End If
            Catch
            End Try
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        RaiseEvent BackClicked()
    End Sub

    Private Sub btnPlaceOrder_Click(sender As Object, e As EventArgs) Handles btnPlaceOrder.Click
        RaiseEvent PlaceOrderClicked(_itemId, _traderId, _price, _availableQty)
    End Sub

    Private Sub btnChat_Click(sender As Object, e As EventArgs) Handles btnChat.Click
        RaiseEvent ChatClicked(_traderId)
    End Sub

    Public Event ViewTraderClicked(traderId As Integer)

    Private Sub btnViewTrader_Click(sender As Object, e As EventArgs) Handles btnViewTrader.Click
        RaiseEvent ViewTraderClicked(_traderId)
    End Sub
End Class