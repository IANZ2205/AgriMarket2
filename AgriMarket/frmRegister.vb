Public Class frmRegister
    Private Sub lnkBackToLogin_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkBackToLogin.LinkClicked
        Dim loginForm As New frmLogin()
        loginForm.Show()
        Me.Hide()
    End Sub

    Private Async Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        If cmbRole.SelectedIndex = -1 Or txtName.Text.Trim() = "" Or txtEmail.Text.Trim() = "" Or txtPassword.Text = "" Then
            MessageBox.Show("Please fill in all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If txtPassword.Text <> txtConfirm.Text Then
            MessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        btnRegister.Enabled = False
        btnRegister.Text = "Creating..."

        Dim response As String = Await NetworkClient.SendRequestAsync($"REGISTER|{txtEmail.Text.Trim()}|{txtPassword.Text}|{cmbRole.SelectedItem.ToString()}|{txtName.Text.Trim()}")

        btnRegister.Enabled = True
        btnRegister.Text = "Create Account"

        Dim parts As String() = response.Split("|"c)
        If parts(0) = "OK" Then
            MessageBox.Show("Account created successfully! You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Dim loginForm As New frmLogin()
            loginForm.Show()
            Me.Hide()
        Else
            MessageBox.Show(parts(3), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class