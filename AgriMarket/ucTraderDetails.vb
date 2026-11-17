Public Class ucTraderDetails
    Private _traderId As Integer

    Public Event BackClicked()
    Public Event ChatClicked(traderId As Integer)
    Public Event ViewProductClicked(itemId As Integer)


    Public Async Sub LoadTrader(id As Integer)
        _traderId = id
        flowProducts.Controls.Clear() ' Clear old cards

        ' 1. Load Trader Profile
        Dim response As String = Await NetworkClient.SendRequestAsync($"GET_TRADER_DETAILS|{id}")
        Dim parts As String() = response.Split("|"c)

        If parts(0) = "OK" Then
            Dim f As String() = parts(2).Split("~"c)
            lblName.Text = f(0)
            lblStats.Text = $"{f(2)} | {f(1)} | {f(3)} completed orders"
        Else
            lblName.Text = "Trader Not Found"
            lblStats.Text = "Could not load details."
            Return ' Stop if we can't find the trader
        End If

        ' 2. Load Their Products
        Dim prodResponse As String = Await NetworkClient.SendRequestAsync($"GET_ALL_STOCK|")
        Dim pParts As String() = prodResponse.Split("|"c)

        If pParts(0) = "OK" AndAlso pParts.Length > 2 Then
            Dim traderName As String = lblName.Text.Trim().ToLower()

            For Each r As String In pParts(2).Split("^"c)
                If r.Trim() <> "" Then
                    Dim f As String() = r.Split("~"c)
                    Dim itemTraderName As String = f(6).Trim().ToLower()

                    ' Use .ToLower() for safe comparison
                    If itemTraderName = traderName Then
                        Dim card As New ucProductCard()
                        Dim itemId As Integer = Convert.ToInt32(f(0))
                        card.Setup(itemId, 0, f(1), f(6), Convert.ToDecimal(f(3)), Convert.ToInt32(f(4)), f(5), f(7))
                        AddHandler card.OrderClicked, AddressOf HandleProductClick
                        flowProducts.Controls.Add(card)
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        RaiseEvent BackClicked()
    End Sub

    Private Sub btnChat_Click(sender As Object, e As EventArgs) Handles btnChat.Click
        RaiseEvent ChatClicked(_traderId)
    End Sub

    Private Sub HandleProductClick(itemId As Integer)
        RaiseEvent ViewProductClicked(itemId)
    End Sub
End Class