Imports System.ComponentModel
Imports System.IO

Public Class ucProfile
    Private _userId As Integer
    Private _currentPicPath As String = ""
    Private _newPicBase64 As String = ""
    Private _loadAttempted As Boolean = False

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property UserId As Integer
        Get
            Return _userId
        End Get
        Set(value As Integer)
            _userId = value
        End Set
    End Property

    Private Async Sub ucProfile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Hide the debug label immediately
        If lblDebug IsNot Nothing Then
            lblDebug.Visible = False
        End If

        If _loadAttempted Then Return
        _loadAttempted = True

        If _userId = 0 Then Return

        Await LoadProfileAsync()
    End Sub

    Private Async Function LoadProfileAsync() As Task
        Try
            Dim response As String = Await NetworkClient.SendRequestAsync($"GET_PROFILE|{_userId}")
            If String.IsNullOrEmpty(response) Then Return

            Dim parts As String() = response.Split("|"c)
            If parts.Length < 3 Then Return
            If parts(0) <> "OK" Then Return

            Dim f As String() = parts(2).Split("~"c)
            txtName.Text = If(f.Length > 0, f(0), "")
            txtEmail.Text = If(f.Length > 1, f(1), "")
            txtPhone.Text = If(f.Length > 2, f(2), "")
            txtDistrict.Text = If(f.Length > 3, f(3), "")
            _currentPicPath = If(f.Length > 4, f(4), "")

            If Not String.IsNullOrEmpty(_currentPicPath) Then
                Await LoadProfilePicture(_currentPicPath)
            End If
        Catch
        End Try
    End Function

    Private Async Function LoadProfilePicture(storedFileName As String) As Task
        Try
            Dim fileResp As String = Await NetworkClient.SendRequestAsync($"GET_FILE|profiles|{storedFileName}")
            Dim parts As String() = fileResp.Split("|"c)
            If parts(0) = "OK" AndAlso parts.Length > 2 Then
                Dim base64 As String = parts(2)
                Dim bytes As Byte() = Convert.FromBase64String(base64)
                Using ms As New MemoryStream(bytes)
                    picProfile.Image = Image.FromStream(ms)
                End Using
                lblPhotoStatus.Text = "Current photo loaded"
                lblPhotoStatus.ForeColor = System.Drawing.Color.FromArgb(27, 122, 80)
            End If
        Catch
        End Try
    End Function

    Private Sub btnChangePhoto_Click(sender As Object, e As EventArgs) Handles btnChangePhoto.Click
        Using ofd As New OpenFileDialog()
            ofd.Title = "Choose a profile picture"
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png"
            If ofd.ShowDialog() = DialogResult.OK Then
                Try
                    Dim imageBytes As Byte() = File.ReadAllBytes(ofd.FileName)
                    If imageBytes.Length > 5000000 Then
                        MessageBox.Show("Image is too large. Max size is 5MB.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    picProfile.Image = Image.FromFile(ofd.FileName)
                    _newPicBase64 = Convert.ToBase64String(imageBytes)
                    lblPhotoStatus.Text = "✓ " & Path.GetFileName(ofd.FileName) & " (not yet saved)"
                    lblPhotoStatus.ForeColor = System.Drawing.Color.FromArgb(217, 164, 65)
                Catch ex As Exception
                    MessageBox.Show("Error loading image: " & ex.Message)
                End Try
            End If
        End Using
    End Sub

    Private Async Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        btnSave.Enabled = False
        btnSave.Text = "Saving..."

        Dim response As String = Await NetworkClient.SendRequestAsync($"UPDATE_PROFILE|{_userId}|{txtName.Text}|{txtPhone.Text}|{txtDistrict.Text}|{_newPicBase64}")
        Dim parts As String() = response.Split("|"c)

        btnSave.Enabled = True
        btnSave.Text = "Save Profile"

        If parts(0) = "OK" Then
            MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            _newPicBase64 = ""
            lblPhotoStatus.Text = "Photo saved"
            lblPhotoStatus.ForeColor = System.Drawing.Color.FromArgb(27, 122, 80)
            Await LoadProfileAsync()
        Else
            MessageBox.Show("Error: " & parts(3), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Async Sub btnChangePass_Click(sender As Object, e As EventArgs) Handles btnChangePass.Click
        If String.IsNullOrWhiteSpace(txtNewPass.Text) Then
            MessageBox.Show("Please enter a new password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If txtNewPass.Text.Length < 6 Then
            MessageBox.Show("Password must be at least 6 characters.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim response As String = Await NetworkClient.SendRequestAsync($"CHANGE_PASSWORD|{_userId}|{txtNewPass.Text}")
        Dim parts As String() = response.Split("|"c)

        If parts(0) = "OK" Then
            MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtNewPass.Clear()
        Else
            MessageBox.Show("Error: " & parts(3), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class