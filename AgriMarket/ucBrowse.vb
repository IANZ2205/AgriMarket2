Imports System.Data
Imports System.ComponentModel

Public Class ucBrowse
    Private _buyerId As Integer

    Public Event StartChatRequested(traderName As String)
    Public Event ViewProductClicked(itemId As Integer)

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property BuyerId As Integer
        Get
            Return _buyerId
        End Get
        Set(value As Integer)
            _buyerId = value
            LoadBrowse()
        End Set
    End Property

    Private Sub ucBrowse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _buyerId > 0 Then LoadBrowse()
    End Sub

    Private Async Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearch.Clear()
        Await LoadBrowse()
    End Sub

    Private Async Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Await LoadBrowse(txtSearch.Text.Trim())
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnSearch.PerformClick()
        End If
    End Sub

    Private Async Function LoadBrowse(Optional searchTerm As String = "") As Task
        Try
            flowProducts.Controls.Clear()

            Dim command As String = "GET_ALL_STOCK|"
            If searchTerm <> "" Then command = $"SEARCH_STOCK|{searchTerm}"

            Dim response As String = Await NetworkClient.SendRequestAsync(command)
            Dim parts As String() = response.Split("|"c)

            If parts(0) = "OK" AndAlso parts.Length > 2 Then
                For Each r As String In parts(2).Split("^"c)
                    If r.Trim() <> "" Then
                        Dim f As String() = r.Split("~"c)

                        Dim card As New ucProductCard()
                        Dim itemId As Integer = Convert.ToInt32(f(0))

                        card.Setup(itemId, 0, f(1), f(6), Convert.ToDecimal(f(3)), Convert.ToInt32(f(4)), f(5), f(7))
                        AddHandler card.OrderClicked, AddressOf HandleCardOrderClick

                        flowProducts.Controls.Add(card)
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading produce: " & ex.Message)
        End Try
    End Function

    Private Sub HandleCardOrderClick(itemId As Integer)
        RaiseEvent ViewProductClicked(itemId)
    End Sub
End Class