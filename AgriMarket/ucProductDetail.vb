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
    Public Event ViewTraderClicked(traderId As Integer)

    Public Sub LoadItem(id As Integer, buyerId As Integer)
        _itemId = id
        _buyerId = buyerId
        _LoadData()
    End Sub

    Private Async Sub _LoadData()
        Try
            Dim response As String = Await NetworkClient.SendRequestAsync($"GET_ITEM_DETAIL|{_itemId}")

            If Me.IsDisposed OrElse Me.Disposing Then Return

            Dim parts As String() = response.Split("|"c)

            If parts(0) = "OK" AndAlso parts.Length > 2 Then
                Dim f As String() = parts(2).Split("~"c)

                ' Safely parse the TraderID
                If f.Length > 7 AndAlso Integer.TryParse(f(7), _traderId) Then
                    ' _traderId is now correctly set
                Else
                    _traderId = 0
                End If

                _price = Convert.ToDecimal(f(3))
                _availableQty = Convert.ToInt32(f(4))

                lblName.Text = f(1)
                lblCategory.Text = f(2)
                lblPrice.Text = "UGX " & _price.ToString("N0")
                lblQty.Text = _availableQty.ToString() & " available"
                lblDesc.Text = If(String.IsNullOrEmpty(f(6)), "No description provided.", f(6))

                lblTraderName.Text = "Name: " & f(8)
                lblTraderPhone.Text = "Phone: " & If(String.IsNullOrEmpty(f(9)), "(not provided)", f(9))
                lblTraderDistrict.Text = "District: " & If(String.IsNullOrEmpty(f(10)), "(not provided)", f(10))

                Dim imgPath As String = If(f.Length > 11, f(11), "")
                If Not String.IsNullOrEmpty(imgPath) Then
                    Await LoadImageAsync(imgPath)
                End If
            End If
        Catch ex As Exception
            If Not Me.IsDisposed AndAlso Not Me.Disposing Then
                MessageBox.Show("Could not load product: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub
    Private Async Function LoadImageAsync(imgPath As String) As Task
        Try
            Dim resp As String = Await NetworkClient.SendRequestAsync($"GET_FILE|products|{imgPath}")

            If Me.IsDisposed OrElse Me.Disposing Then Return

            Dim parts As String() = resp.Split("|"c)
            If parts(0) = "OK" AndAlso parts.Length > 2 Then
                Dim bytes As Byte() = Convert.FromBase64String(parts(2))
                Using ms As New MemoryStream(bytes)
                    Using tmpImg As Image = Image.FromStream(ms)
                        If Me.IsDisposed OrElse Me.Disposing Then Return
                        picImage.Image = New Bitmap(tmpImg)
                    End Using
                End Using
            End If
        Catch
        End Try
    End Function

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        RaiseEvent BackClicked()
    End Sub

    Private Sub btnPlaceOrder_Click(sender As Object, e As EventArgs) Handles btnPlaceOrder.Click
        RaiseEvent PlaceOrderClicked(_itemId, _traderId, _price, _availableQty)
    End Sub

    Private Sub btnChat_Click(sender As Object, e As EventArgs) Handles btnChat.Click
        RaiseEvent ChatClicked(_traderId)
    End Sub

    Private Sub btnViewTrader_Click(sender As Object, e As EventArgs) Handles btnViewTrader.Click
        RaiseEvent ViewTraderClicked(_traderId)
    End Sub
End Class