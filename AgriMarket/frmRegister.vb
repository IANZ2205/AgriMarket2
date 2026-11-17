Imports System.Data
Imports System.IO

Public Class frmRegister
    Private _profilePicBase64 As String = ""
    Private _questionIds As New List(Of Integer)()
    Private _lblDebug As Label

    Private Async Sub frmRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' BuildDebugLabel() ' Hidden for production
        Await LoadSecurityQuestions()
    End Sub
    Private Sub BuildDebugLabel()
        If panelRegister Is Nothing Then Return

        _lblDebug = New Label()
        _lblDebug.AutoSize = False
        _lblDebug.Font = New Font("Consolas", 8)
        _lblDebug.ForeColor = Color.Gray
        _lblDebug.BackColor = Color.Transparent
        _lblDebug.Location = New Point(40, 665)
        _lblDebug.Size = New Size(400, 40)
        _lblDebug.Text = "Debug: loading questions..."
        panelRegister.Controls.Add(_lblDebug)
        _lblDebug.BringToFront()
    End Sub

    Private Async Function LoadSecurityQuestions() As Task
        Try
            If _lblDebug IsNot Nothing Then
                _lblDebug.Text = "Debug: sending GET_SECURITY_QUESTIONS..."
                _lblDebug.ForeColor = Color.Gray
            End If

            Dim response As String = Await NetworkClient.SendRequestAsync("GET_SECURITY_QUESTIONS|")

            If String.IsNullOrEmpty(response) Then
                PopulateFallbackQuestions()
                If _lblDebug IsNot Nothing Then
                    _lblDebug.Text = "Debug: empty response. Using fallback questions."
                    _lblDebug.ForeColor = Color.OrangeRed
                End If
                Return
            End If

            Dim parts As String() = response.Split("|"c)

            If parts(0) = "OK" AndAlso parts.Length > 2 Then
                cmbSecurityQ.Items.Clear()
                _questionIds.Clear()
                For Each r In parts(2).Split("^"c)
                    If r.Trim() <> "" Then
                        Dim f As String() = r.Split("~"c)
                        If f.Length >= 2 Then
                            _questionIds.Add(Convert.ToInt32(f(0)))
                            cmbSecurityQ.Items.Add(f(1))
                        End If
                    End If
                Next

                If cmbSecurityQ.Items.Count = 0 Then
                    PopulateFallbackQuestions()
                    If _lblDebug IsNot Nothing Then
                        _lblDebug.Text = "Debug: server returned 0 questions. Using fallback."
                        _lblDebug.ForeColor = Color.OrangeRed
                    End If
                Else
                    cmbSecurityQ.SelectedIndex = 0
                    If _lblDebug IsNot Nothing Then
                        _lblDebug.Text = "Debug: loaded " & cmbSecurityQ.Items.Count & " questions from server."
                        _lblDebug.ForeColor = Color.Green
                    End If
                End If
            Else
                PopulateFallbackQuestions()
                Dim shortResp As String = response
                If shortResp.Length > 80 Then shortResp = shortResp.Substring(0, 80)
                If _lblDebug IsNot Nothing Then
                    _lblDebug.Text = "Debug: server returned: " & shortResp & " -- using fallback."
                    _lblDebug.ForeColor = Color.OrangeRed
                End If
            End If
        Catch ex As Exception
            PopulateFallbackQuestions()
            If _lblDebug IsNot Nothing Then
                _lblDebug.Text = "Debug: exception (" & ex.Message & "). Using fallback."
                _lblDebug.ForeColor = Color.Red
            End If
        End Try
    End Function

    Private Sub PopulateFallbackQuestions()
        cmbSecurityQ.Items.Clear()
        _questionIds.Clear()
        _questionIds.Add(1)
        cmbSecurityQ.Items.Add("What is the name of your first school?")
        _questionIds.Add(2)
        cmbSecurityQ.Items.Add("What is the name of your first pet?")
        _questionIds.Add(3)
        cmbSecurityQ.Items.Add("In which town were you born?")
        cmbSecurityQ.SelectedIndex = 0
    End Sub

    Private Sub lnkBackToLogin_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkBackToLogin.LinkClicked
        Dim loginForm As New frmLogin()
        loginForm.Show()
        Me.Hide()
    End Sub

    Private Sub btnUploadPic_Click(sender As Object, e As EventArgs) Handles btnUploadPic.Click
        Try
            Using ofd As New OpenFileDialog()
                ofd.Title = "Choose a profile picture"
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png"
                If ofd.ShowDialog() = DialogResult.OK Then
                    Dim imageBytes As Byte() = File.ReadAllBytes(ofd.FileName)
                    If imageBytes.Length > 5000000 Then
                        MessageBox.Show("Image is too large. Max size is 5MB.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    picProfile.Image = Image.FromFile(ofd.FileName)
                    _profilePicBase64 = Convert.ToBase64String(imageBytes)
                    lblPicStatus.Text = "✓ " & Path.GetFileName(ofd.FileName)
                    lblPicStatus.ForeColor = System.Drawing.Color.FromArgb(27, 122, 80)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading image: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        If cmbRole.SelectedIndex = -1 Or txtName.Text.Trim() = "" Or txtEmail.Text.Trim() = "" Or txtPassword.Text = "" Then
            MessageBox.Show("Please fill in all required fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If txtPassword.Text <> txtConfirm.Text Then
            MessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If cmbSecurityQ.SelectedIndex = -1 Then
            MessageBox.Show("Please choose a security question.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtAnswer.Text) Then
            MessageBox.Show("Please enter an answer to your security question.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        btnRegister.Enabled = False
        btnRegister.Text = "Creating..."

        Dim qid As Integer = _questionIds(cmbSecurityQ.SelectedIndex)
        Dim safeAnswer As String = txtAnswer.Text.Trim().Replace("|"c, " ").Replace("~"c, " ").Replace("^"c, " ")

        Dim response As String = Await NetworkClient.SendRequestAsync($"REGISTER|{txtEmail.Text.Trim()}|{txtPassword.Text}|{cmbRole.SelectedItem.ToString()}|{txtName.Text.Trim()}|{_profilePicBase64}|{qid}|{safeAnswer}")

        btnRegister.Enabled = True
        btnRegister.Text = "Create Account"

        Dim parts As String() = response.Split("|"c)
        If parts(0) = "OK" Then
            MessageBox.Show("Account created successfully! You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Dim loginForm As New frmLogin()
            loginForm.Show()
            Me.Hide()
        Else
            Dim msg As String = "Registration failed."
            If parts.Length > 3 Then msg = parts(3)
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class