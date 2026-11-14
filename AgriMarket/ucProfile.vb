Imports System.ComponentModel

Public Class ucProfile
    Private _userId As Integer

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property UserId As Integer
        Get
            Return _userId
        End Get
        Set(value As Integer)
            _userId = value
            LoadProfile()
        End Set
    End Property

    Private Async Sub LoadProfile()
        If _userId = 0 Then Return

        Dim response As String = Await NetworkClient.SendRequestAsync($"GET_PROFILE|{_userId}")
        Dim parts As String() = response.Split("|"c)

        If parts(0) = "OK" Then
            ' Format: Name~Email~Phone~District
            Dim f As String() = parts(2).Split("~"c)
            txtName.Text = f(0)
            txtEmail.Text = f(1)
            txtPhone.Text = f(2)
            txtDistrict.Text = f(3)
        End If
    End Sub

    Private Async Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim response As String = Await NetworkClient.SendRequestAsync($"UPDATE_PROFILE|{_userId}|{txtName.Text}|{txtPhone.Text}|{txtDistrict.Text}")
        Dim parts As String() = response.Split("|"c)

        If parts(0) = "OK" Then
            MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Error: " & parts(3), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Async Sub btnChangePass_Click(sender As Object, e As EventArgs) Handles btnChangePass.Click
        If String.IsNullOrWhiteSpace(txtNewPass.Text) Then
            MessageBox.Show("Please enter a new password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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