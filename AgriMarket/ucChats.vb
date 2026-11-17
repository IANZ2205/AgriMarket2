Imports System.Data
Imports System.ComponentModel
Imports System.Drawing
Imports System.IO

Public Class ucChats
    Private _currentUserId As Integer
    Private _currentConvId As Integer = 0
    Private _otherUserId As Integer = 0
    Private _currentHistory As New List(Of ChatMessage)()
    Private _isRendering As Boolean = False
    Private _chatLoadVersion As Integer = 0

    Private Class ChatMessage
        Public SenderId As Integer
        Public SenderName As String
        Public MsgType As String
        Public Body As String
        Public FileName As String
        Public OriginalName As String
        Public FileSize As Integer
        Public LocationText As String
        Public SentAt As String
    End Class

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property CurrentUserId As Integer
        Get
            Return _currentUserId
        End Get
        Set(value As Integer)
            _currentUserId = value
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

    Private Async Sub ucChats_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyGridStyle()
        SetupAttachMenu()
        SetupChatMenu()
        AddHandler NetworkClient.ChatMessageReceived, AddressOf HandleIncomingChat
        If _currentUserId > 0 Then Await LoadConversations()
    End Sub

    Private Sub ucChats_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Try
            RemoveHandler NetworkClient.ChatMessageReceived, AddressOf HandleIncomingChat
        Catch
        End Try
    End Sub

    Private Sub SetupAttachMenu()
        If ctxAttach Is Nothing Then Return
        ctxAttach.Items.Clear()

        Dim miPhoto As New ToolStripMenuItem("Send Photo")
        AddHandler miPhoto.Click, AddressOf AttachPhoto_Click
        ctxAttach.Items.Add(miPhoto)

        Dim miDocument As New ToolStripMenuItem("Send Document")
        AddHandler miDocument.Click, AddressOf AttachDocument_Click
        ctxAttach.Items.Add(miDocument)

        Dim miLocation As New ToolStripMenuItem("Share Location")
        AddHandler miLocation.Click, AddressOf AttachLocation_Click
        ctxAttach.Items.Add(miLocation)

        AddHandler btnAttach.Click, Sub(s, ev) ctxAttach.Show(btnAttach, New Point(0, btnAttach.Height))
    End Sub

    Private Sub SetupChatMenu()
        If ctxChatMenu Is Nothing Then Return
        ctxChatMenu.Items.Clear()

        Dim miBlock As New ToolStripMenuItem("Block this user")
        AddHandler miBlock.Click, AddressOf BlockUser_Click
        ctxChatMenu.Items.Add(miBlock)

        Dim miReport As New ToolStripMenuItem("Report this user")
        AddHandler miReport.Click, AddressOf ReportUser_Click
        ctxChatMenu.Items.Add(miReport)

        ' Using your existing btnMore control to show the menu
        AddHandler btnMore.Click, Sub(s, ev) ctxChatMenu.Show(btnMore, New Point(0, btnMore.Height))
    End Sub

    Private Async Sub CheckBlockedState()
        Try
            If _currentConvId = 0 OrElse _otherUserId = 0 Then
                lblBlockedNotice.Visible = False
                panelInput.Enabled = True
                Return
            End If

            Dim resp As String = Await NetworkClient.SendRequestAsync($"IS_BLOCKED|{_currentUserId}|{_otherUserId}")
            Dim parts As String() = resp.Split("|"c)
            If parts(0) = "OK" AndAlso parts.Length > 2 Then
                Dim state As String = parts(2)
                If state = "NONE" Then
                    lblBlockedNotice.Visible = False
                    panelInput.Enabled = True
                ElseIf state = "I_BLOCKED" Then
                    lblBlockedNotice.Text = "You have blocked this user. Unblock via the ... menu to send messages."
                    lblBlockedNotice.Visible = True
                    panelInput.Enabled = False
                Else
                    lblBlockedNotice.Text = "This user has blocked you. You cannot send messages."
                    lblBlockedNotice.Visible = True
                    panelInput.Enabled = False
                End If
            End If
        Catch
        End Try
    End Sub

    Private Async Sub BlockUser_Click(sender As Object, e As EventArgs)
        If _currentConvId = 0 OrElse _otherUserId = 0 Then Exit Sub
        Dim confirm As DialogResult = MessageBox.Show(
            "Block this user? You will not be able to exchange new messages until you unblock them.",
            "Block User",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)
        If confirm = DialogResult.Yes Then
            Dim resp As String = Await NetworkClient.SendRequestAsync($"CHAT_BLOCK|{_currentUserId}|{_otherUserId}")
            If resp.StartsWith("OK") Then
                MessageBox.Show("User blocked.", "Blocked", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CheckBlockedState()
            Else
                MessageBox.Show("Could not block user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Async Sub ReportUser_Click(sender As Object, e As EventArgs)
        If _currentConvId = 0 OrElse _otherUserId = 0 Then Exit Sub

        Dim reason As String = InputBox("Please describe the issue with this user's messages:", "Report User")
        If String.IsNullOrWhiteSpace(reason) Then Exit Sub

        Dim safeReason As String = reason.Trim().Replace("|"c, " ").Replace("~"c, " ").Replace("^"c, " ")
        Dim resp As String = Await NetworkClient.SendRequestAsync($"CHAT_REPORT|{_currentConvId}|{_currentUserId}|{_otherUserId}|{safeReason}")
        If resp.StartsWith("OK") Then
            MessageBox.Show("Your report has been submitted to the administrator.", "Report Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Could not submit report.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
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
        lblBlockedNotice.Visible = False
        panelInput.Enabled = True
        Await LoadChatHistory()
    End Sub

    Private Async Function LoadChatHistory() As Task
        _chatLoadVersion += 1
        Dim myVersion As Integer = _chatLoadVersion

        Dim convAtCall As Integer = _currentConvId
        Dim response As String = Await NetworkClient.SendRequestAsync($"GET_CHAT_HISTORY|{convAtCall}")

        If myVersion <> _chatLoadVersion Then Return
        If convAtCall <> _currentConvId Then Return

        Dim newHistory As New List(Of ChatMessage)()

        If response.StartsWith("OK") Then
            Dim parts As String() = response.Split("|"c)
            If parts.Length > 2 Then
                For Each r As String In parts(2).Split("^"c)
                    If r.Trim() <> "" Then
                        Dim f As String() = r.Split("~"c)
                        Dim msg As New ChatMessage()
                        msg.SenderId = Convert.ToInt32(f(1))
                        msg.SenderName = f(2)
                        msg.MsgType = If(f.Length > 3, f(3), "Text")
                        msg.Body = If(f.Length > 4, f(4), "")
                        msg.FileName = If(f.Length > 5, f(5), "")
                        msg.OriginalName = If(f.Length > 6, f(6), "")
                        Try
                            msg.FileSize = If(f.Length > 7, Convert.ToInt32(f(7)), 0)
                        Catch
                        End Try
                        msg.LocationText = If(f.Length > 8, f(8), "")
                        msg.SentAt = If(f.Length > 9, f(9), "")
                        newHistory.Add(msg)
                    End If
                Next
            End If
        End If

        _currentHistory = newHistory
        RenderBubbles()
        CheckBlockedState()
    End Function

    Private Function GetUsableWidth() As Integer
        Dim w As Integer = panelMessages.ClientSize.Width
        If w < 100 Then w = panelMessages.Width
        If w < 100 AndAlso Me.Parent IsNot Nothing Then w = Me.Parent.Width - 350
        If w < 100 Then w = 800
        Return w - 30
    End Function

    Private Sub RenderBubbles()
        If _isRendering Then Return
        _isRendering = True

        Try
            panelMessages.SuspendLayout()
            panelMessages.Controls.Clear()

            Dim usableWidth As Integer = GetUsableWidth()
            If usableWidth < 200 Then usableWidth = 200
            Dim maxBubbleWidth As Integer = Math.Min(450, CInt(usableWidth * 0.75))

            Dim currentY As Integer = 10

            For Each msg As ChatMessage In _currentHistory
                Dim isMine As Boolean = (msg.SenderId = _currentUserId)

                Select Case msg.MsgType
                    Case "Image"
                        Dim bubble As New Panel()
                        bubble.Size = New Size(220, 180)
                        bubble.BackColor = Theme.Tint
                        bubble.BorderStyle = BorderStyle.FixedSingle

                        Dim pic As New PictureBox()
                        pic.Location = New Point(10, 10)
                        pic.Size = New Size(200, 140)
                        pic.SizeMode = PictureBoxSizeMode.Zoom
                        pic.BackColor = Color.White
                        Dim storedName As String = msg.FileName
                        AddHandler pic.Click, Sub(s, ev)
                                                  If Not String.IsNullOrEmpty(storedName) Then
                                                      ShowImagePreview(storedName)
                                                  End If
                                              End Sub
                        bubble.Controls.Add(pic)

                        Dim caption As New Label()
                        caption.Text = If(String.IsNullOrEmpty(msg.Body), "[Photo] - click to open", msg.Body & " - click to open")
                        caption.AutoSize = False
                        caption.Size = New Size(200, 20)
                        caption.Location = New Point(10, 155)
                        caption.Font = New Font("Segoe UI", 8, FontStyle.Italic)
                        caption.ForeColor = Color.Gray
                        bubble.Controls.Add(caption)

                        If isMine Then
                            bubble.Location = New Point(usableWidth - bubble.Width, currentY)
                        Else
                            bubble.Location = New Point(10, currentY)
                        End If

                        panelMessages.Controls.Add(bubble)
                        LoadImageForBubble(pic, msg.FileName)
                        currentY += bubble.Height + 8

                    Case "File"
                        Dim bubble As New Panel()
                        bubble.Size = New Size(280, 70)
                        bubble.BackColor = If(isMine, Theme.Ocean, Theme.Tint)
                        bubble.BorderStyle = BorderStyle.FixedSingle

                        Dim lblName As New Label()
                        lblName.Text = msg.OriginalName
                        lblName.AutoSize = False
                        lblName.Size = New Size(260, 20)
                        lblName.Location = New Point(10, 8)
                        lblName.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                        lblName.ForeColor = If(isMine, Color.White, Theme.TextDark)
                        bubble.Controls.Add(lblName)

                        Dim sizeKB As Double = msg.FileSize / 1024.0
                        Dim lblSize As New Label()
                        lblSize.Text = String.Format("{0:N1} KB - click to download", sizeKB)
                        lblSize.AutoSize = False
                        lblSize.Size = New Size(260, 18)
                        lblSize.Location = New Point(10, 30)
                        lblSize.Font = New Font("Segoe UI", 8)
                        lblSize.ForeColor = If(isMine, Color.White, Color.Gray)
                        bubble.Controls.Add(lblSize)

                        Dim storedName As String = msg.FileName
                        Dim origName As String = msg.OriginalName
                        AddHandler bubble.Click, Sub(s, ev) DownloadFile(storedName, origName)
                        AddHandler lblName.Click, Sub(s, ev) DownloadFile(storedName, origName)
                        AddHandler lblSize.Click, Sub(s, ev) DownloadFile(storedName, origName)

                        If isMine Then
                            bubble.Location = New Point(usableWidth - bubble.Width, currentY)
                        Else
                            bubble.Location = New Point(10, currentY)
                        End If

                        panelMessages.Controls.Add(bubble)
                        currentY += bubble.Height + 8

                    Case "Location"
                        Dim bubble As New Panel()
                        bubble.Size = New Size(280, 70)
                        bubble.BackColor = If(isMine, Theme.Ocean, Theme.Tint)
                        bubble.BorderStyle = BorderStyle.FixedSingle

                        Dim lblLoc As New Label()
                        lblLoc.Text = "[Location] " & msg.LocationText
                        lblLoc.AutoSize = False
                        lblLoc.Size = New Size(260, 50)
                        lblLoc.Location = New Point(10, 10)
                        lblLoc.Font = New Font("Segoe UI", 10)
                        lblLoc.ForeColor = If(isMine, Color.White, Theme.TextDark)
                        bubble.Controls.Add(lblLoc)

                        If isMine Then
                            bubble.Location = New Point(usableWidth - bubble.Width, currentY)
                        Else
                            bubble.Location = New Point(10, currentY)
                        End If

                        panelMessages.Controls.Add(bubble)
                        currentY += bubble.Height + 8

                    Case Else
                        Dim bubble As New Label()
                        bubble.Text = msg.Body
                        bubble.AutoSize = False
                        bubble.Font = New Font("Segoe UI", 11.0!)
                        bubble.Padding = New Padding(12, 8, 12, 8)

                        Dim maxTextWidth As Integer = maxBubbleWidth - 24
                        Dim textSize As Size = TextRenderer.MeasureText(msg.Body, bubble.Font, New Size(maxTextWidth, Integer.MaxValue), TextFormatFlags.WordBreak)
                        Dim bubbleWidth As Integer = Math.Min(textSize.Width + 24, maxBubbleWidth)
                        Dim bubbleHeight As Integer = textSize.Height + 18

                        bubble.Size = New Size(bubbleWidth, bubbleHeight)

                        If isMine Then
                            bubble.BackColor = Theme.Ocean
                            bubble.ForeColor = Color.White
                            bubble.Location = New Point(usableWidth - bubbleWidth, currentY)
                        Else
                            bubble.BackColor = Theme.Tint
                            bubble.ForeColor = Theme.TextDark
                            bubble.Location = New Point(10, currentY)
                        End If

                        panelMessages.Controls.Add(bubble)
                        currentY += bubbleHeight + 8
                End Select
            Next

            If currentY > panelMessages.ClientSize.Height Then
                panelMessages.AutoScrollMinSize = New Size(0, currentY + 10)
            Else
                panelMessages.AutoScrollMinSize = New Size(0, 0)
            End If

            panelMessages.ResumeLayout(True)
        Finally
            _isRendering = False
        End Try

        If panelMessages.Controls.Count > 0 Then
            Try
                panelMessages.ScrollControlIntoView(panelMessages.Controls(panelMessages.Controls.Count - 1))
            Catch
            End Try
        End If
    End Sub

    Private Async Sub LoadImageForBubble(pic As PictureBox, storedName As String)
        Try
            Dim fileResp As String = Await NetworkClient.SendRequestAsync($"GET_FILE|chat|{storedName}")
            Dim parts As String() = fileResp.Split("|"c)
            If parts(0) = "OK" AndAlso parts.Length > 2 Then
                Dim bytes As Byte() = Convert.FromBase64String(parts(2))
                Using ms As New MemoryStream(bytes)
                    Using tempImg As Image = Image.FromStream(ms)
                        pic.Image = New Bitmap(tempImg)
                    End Using
                End Using
            End If
        Catch
        End Try
    End Sub

    Private Async Sub ShowImagePreview(storedName As String)
        Try
            Dim fileResp As String = Await NetworkClient.SendRequestAsync($"GET_FILE|chat|{storedName}")
            Dim parts As String() = fileResp.Split("|"c)
            If parts(0) = "OK" AndAlso parts.Length > 2 Then
                Dim bytes As Byte() = Convert.FromBase64String(parts(2))
                Dim bmp As Bitmap
                Using ms As New MemoryStream(bytes)
                    Using tempImg As Image = Image.FromStream(ms)
                        bmp = New Bitmap(tempImg)
                    End Using
                End Using

                Dim preview As New Form()
                preview.Text = "Image Preview"
                preview.Size = New Size(700, 600)
                preview.StartPosition = FormStartPosition.CenterParent
                preview.BackColor = Color.Black

                Dim pic As New PictureBox()
                pic.Image = bmp
                pic.SizeMode = PictureBoxSizeMode.Zoom
                pic.Dock = DockStyle.Fill
                preview.Controls.Add(pic)
                preview.ShowDialog()
                preview.Dispose()
                bmp.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show("Could not load image: " & ex.Message)
        End Try
    End Sub

    Private Async Sub DownloadFile(storedName As String, originalName As String)
        If String.IsNullOrEmpty(storedName) Then Return
        Try
            Dim saveDlg As New SaveFileDialog()
            saveDlg.FileName = originalName
            If saveDlg.ShowDialog() = DialogResult.OK Then
                Dim fileResp As String = Await NetworkClient.SendRequestAsync($"GET_FILE|chat|{storedName}")
                Dim parts As String() = fileResp.Split("|"c)
                If parts(0) = "OK" AndAlso parts.Length > 2 Then
                    Dim bytes As Byte() = Convert.FromBase64String(parts(2))
                    File.WriteAllBytes(saveDlg.FileName, bytes)
                    MessageBox.Show("File saved to: " & saveDlg.FileName, "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Could not fetch file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Download failed: " & ex.Message)
        End Try
    End Sub

    Private Sub panelMessages_Resize(sender As Object, e As EventArgs) Handles panelMessages.Resize
        If _currentHistory.Count > 0 AndAlso Not _isRendering Then
            RenderBubbles()
        End If
    End Sub

    Private Async Sub AttachPhoto_Click(sender As Object, e As EventArgs)
        If _currentConvId = 0 Then
            MessageBox.Show("Please open a conversation first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Using ofd As New OpenFileDialog()
            ofd.Title = "Choose a photo to send"
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png"
            If ofd.ShowDialog() = DialogResult.OK Then
                Try
                    Dim bytes As Byte() = File.ReadAllBytes(ofd.FileName)
                    If bytes.Length > 5000000 Then
                        MessageBox.Show("Image is too large. Max size is 5MB.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    Dim base64 As String = Convert.ToBase64String(bytes)
                    Dim fileName As String = Path.GetFileName(ofd.FileName)
                    Dim response As String = Await NetworkClient.SendRequestAsync($"SEND_MESSAGE|{_currentConvId}|{_currentUserId}|{_otherUserId}|Image||{SafeName(fileName)}|{bytes.Length}||{base64}")
                    If response.StartsWith("OK") Then
                        Await LoadChatHistory()
                    Else
                        ShowSendError("photo", response)
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End If
        End Using
    End Sub

    Private Async Sub AttachDocument_Click(sender As Object, e As EventArgs)
        If _currentConvId = 0 Then
            MessageBox.Show("Please open a conversation first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Using ofd As New OpenFileDialog()
            ofd.Title = "Choose a document to send"
            ofd.Filter = "Documents|*.pdf;*.doc;*.docx;*.xls;*.xlsx"
            If ofd.ShowDialog() = DialogResult.OK Then
                Try
                    Dim bytes As Byte() = File.ReadAllBytes(ofd.FileName)
                    If bytes.Length > 5000000 Then
                        MessageBox.Show("File is too large. Max size is 5MB.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    Dim base64 As String = Convert.ToBase64String(bytes)
                    Dim fileName As String = Path.GetFileName(ofd.FileName)
                    Dim response As String = Await NetworkClient.SendRequestAsync($"SEND_MESSAGE|{_currentConvId}|{_currentUserId}|{_otherUserId}|File||{SafeName(fileName)}|{bytes.Length}||{base64}")
                    If response.StartsWith("OK") Then
                        Await LoadChatHistory()
                    Else
                        ShowSendError("document", response)
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End If
        End Using
    End Sub

    Private Async Sub AttachLocation_Click(sender As Object, e As EventArgs)
        If _currentConvId = 0 Then
            MessageBox.Show("Please open a conversation first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Dim locForm As New frmShareLocation()
        If locForm.ShowDialog() = DialogResult.OK Then
            Dim locText As String = locForm.LocationText
            Dim response As String = Await NetworkClient.SendRequestAsync($"SEND_MESSAGE|{_currentConvId}|{_currentUserId}|{_otherUserId}|Location||{SafeName(locText)}|0|{SafeName(locText)}|")
            If response.StartsWith("OK") Then
                Await LoadChatHistory()
            Else
                ShowSendError("location", response)
            End If
        End If
    End Sub

    Private Sub ShowSendError(kind As String, response As String)
        Dim parts As String() = response.Split("|"c)
        Dim msg As String = "Could not send " & kind & "."
        If parts.Length > 3 Then msg &= Environment.NewLine & Environment.NewLine & "Server said: " & parts(3)
        MessageBox.Show(msg, "Send Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Function SafeName(s As String) As String
        If String.IsNullOrEmpty(s) Then Return ""
        Return s.Replace("|"c, " ").Replace("~"c, " ").Replace("^"c, " ")
    End Function

    Private Async Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        If String.IsNullOrWhiteSpace(txtMessage.Text) Or _currentConvId = 0 Then Exit Sub
        If Not panelInput.Enabled Then
            MessageBox.Show(lblBlockedNotice.Text, "Cannot Send", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Dim body As String = txtMessage.Text.Trim()
        txtMessage.Clear()

        Dim response As String = Await NetworkClient.SendRequestAsync($"SEND_MESSAGE|{_currentConvId}|{_currentUserId}|{_otherUserId}|{body}")
        Dim parts As String() = response.Split("|"c)

        If parts(0) = "OK" Then
            Await LoadChatHistory()
        Else
            Dim msg As String = "Error sending message."
            If parts.Length > 3 Then msg = parts(3)
            MessageBox.Show(msg)
        End If
    End Sub

    Private Sub txtMessage_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMessage.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnSend.PerformClick()
        End If
    End Sub

    Private Sub HandleIncomingChat(convId As Integer, senderName As String, message As String)
        If Me.IsDisposed OrElse Me.Disposing Then Return
        If Me.InvokeRequired Then
            Try
                Me.Invoke(New Action(Sub() HandleIncomingChat(convId, senderName, message)))
            Catch
            End Try
            Return
        End If
        If convId = _currentConvId Then
            Dim t As Task = LoadChatHistory()
        Else
            Dim t2 As Task = LoadConversations()
        End If
    End Sub
End Class