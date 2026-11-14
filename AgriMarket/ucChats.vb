Imports System.Data
Imports System.ComponentModel
Imports System.Drawing

Public Class ucChats
    Private _currentUserId As Integer
    Private _currentConvId As Integer = 0
    Private _otherUserId As Integer = 0

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property CurrentUserId As Integer
        Get
            Return _currentUserId
        End Get
        Set(value As Integer)
            _currentUserId = value
            LoadConversations()
        End Set
    End Property

    Public Async Function OpenChatWith(targetUserId As Integer) As Task
        _otherUserId = targetUserId
        Dim response As String = Await NetworkClient.SendRequestAsync($"START_CHAT|{_currentUserId}|{_otherUserId}")
        Dim parts As String() = response.Split("|"c)
        If parts(0) = "OK" Then
            _currentConvId = Convert.ToInt32(parts(2))
            Await LoadChatHistory()
            Await LoadConversations()
        End If
    End Function

    Private Sub ucChats_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyGridStyle()
        AddHandler NetworkClient.ChatMessageReceived, AddressOf HandleIncomingChat
        If _currentUserId > 0 Then LoadConversations()
    End Sub

    Private Sub ApplyGridStyle()
        dgvConversations.BackgroundColor = Theme.Card
        dgvConversations.BorderStyle = BorderStyle.None
        dgvConversations.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvConversations.GridColor = Theme.Border
        dgvConversations.EnableHeadersVisualStyles = False
        dgvConversations.ColumnHeadersVisible = False
        dgvConversations.DefaultCellStyle.BackColor = Theme.Card
        dgvConversations.DefaultCellStyle.ForeColor = Theme.TextDark
        dgvConversations.DefaultCellStyle.SelectionBackColor = Theme.Tint
        dgvConversations.DefaultCellStyle.SelectionForeColor = Theme.Ocean
        dgvConversations.DefaultCellStyle.Font = New Font("Segoe UI", 11.0!, FontStyle.Bold)
        dgvConversations.RowTemplate.Height = 60
    End Sub

    Private Async Function LoadConversations() As Task
        Dim response As String = Await NetworkClient.SendRequestAsync($"GET_CONVERSATIONS|{_currentUserId}")
        Dim parts As String() = response.Split("|"c)

        If parts(0) = "OK" Then
            Dim dt As New DataTable()
            dt.Columns.Add("ConvID", GetType(Integer))
            dt.Columns.Add("OtherUserID", GetType(Integer))
            dt.Columns.Add("Name")
            dt.Columns.Add("Role")

            If parts.Length > 2 Then
                For Each r As String In parts(2).Split("^"c)
                    If r.Trim() <> "" Then
                        Dim f As String() = r.Split("~"c)
                        dt.Rows.Add(f(0), f(1), f(2), f(3))
                    End If
                Next
            End If
            dgvConversations.DataSource = dt
            dgvConversations.Columns("ConvID").Visible = False
            dgvConversations.Columns("OtherUserID").Visible = False
            dgvConversations.Columns("Name").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            dgvConversations.Columns("Role").Width = 80
        End If
    End Function

    Private Async Sub dgvConversations_SelectionChanged(sender As Object, e As EventArgs) Handles dgvConversations.SelectionChanged
        If dgvConversations.SelectedRows.Count = 0 Then Return
        Dim row = dgvConversations.SelectedRows(0)
        _currentConvId = Convert.ToInt32(row.Cells("ConvID").Value)
        _otherUserId = Convert.ToInt32(row.Cells("OtherUserID").Value)

        lblChatHeader.Text = row.Cells("Name").Value.ToString()
        Await LoadChatHistory()
    End Sub

    Private Async Function LoadChatHistory() As Task
        panelMessages.Controls.Clear()

        Dim response As String = Await NetworkClient.SendRequestAsync($"GET_CHAT_HISTORY|{_currentConvId}")
        Dim parts As String() = response.Split("|"c)

        If parts(0) = "OK" AndAlso parts.Length > 2 Then
            Dim currentY As Integer = 10
            ' Use ClientSize to avoid the scrollbar width issue
            Dim containerWidth As Integer = panelMessages.ClientSize.Width - 30

            For Each r As String In parts(2).Split("^"c)
                If r.Trim() <> "" Then
                    Dim f As String() = r.Split("~"c)
                    Dim senderId As Integer = Convert.ToInt32(f(1))
                    Dim msgBody As String = f(3)

                    Dim isMine As Boolean = (senderId = _currentUserId)

                    ' Create the chat bubble
                    Dim bubble As New Label()
                    bubble.Text = msgBody
                    bubble.AutoSize = False
                    bubble.Font = New Font("Segoe UI", 11.0!)
                    bubble.Padding = New Padding(10, 8, 10, 8)

                    ' Measure the text to size the bubble correctly
                    Dim textSize As Size = TextRenderer.MeasureText(msgBody, bubble.Font, New Size(400, Integer.MaxValue), TextFormatFlags.WordBreak)
                    bubble.Size = New Size(textSize.Width + 20, textSize.Height + 16)

                    ' Position Left or Right
                    If isMine Then
                        bubble.BackColor = Theme.Ocean
                        bubble.ForeColor = Color.White
                        bubble.Location = New Point(containerWidth - bubble.Width, currentY)
                    Else
                        bubble.BackColor = Theme.Tint
                        bubble.ForeColor = Theme.TextDark
                        bubble.Location = New Point(10, currentY)
                    End If

                    panelMessages.Controls.Add(bubble)
                    currentY += bubble.Height + 10
                End If
            Next

            ' Scroll to the bottom
            If panelMessages.Controls.Count > 0 Then
                panelMessages.ScrollControlIntoView(panelMessages.Controls(panelMessages.Controls.Count - 1))
            End If
        End If
    End Function

    Private Async Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        If String.IsNullOrWhiteSpace(txtMessage.Text) Or _currentConvId = 0 Then Exit Sub

        Dim body As String = txtMessage.Text.Trim()
        txtMessage.Clear()

        Dim response As String = Await NetworkClient.SendRequestAsync($"SEND_MESSAGE|{_currentConvId}|{_currentUserId}|{_otherUserId}|{body}")
        Dim parts As String() = response.Split("|"c)

        If parts(0) = "OK" Then
            Await LoadChatHistory()
        Else
            MessageBox.Show("Error sending message: " & parts(3))
        End If
    End Sub

    Private Sub txtMessage_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMessage.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnSend.PerformClick()
        End If
    End Sub

    Private Sub HandleIncomingChat(convId As Integer, senderName As String, message As String)
        If Me.InvokeRequired Then
            Me.Invoke(New Action(Sub() HandleIncomingChat(convId, senderName, message)))
        Else
            If convId = _currentConvId Then
                LoadChatHistory()
            Else
                LoadConversations()
            End If
        End If
    End Sub
End Class