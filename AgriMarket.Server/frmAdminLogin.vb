Public Class frmAdminLogin
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim email As String = txtEmail.Text.Trim()
        Dim password As String = txtPassword.Text

        If email = "" Or password = "" Then
            MessageBox.Show("Please enter both email and password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Direct database call - no network!
        Dim userRow As DataRow = DatabaseHelper.AuthenticateUserRaw(email, password)

        If userRow IsNot Nothing Then
            If userRow("Role").ToString() <> "Admin" Then
                MessageBox.Show("This login is for Administrators only.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If

            ' Open the Admin Dashboard
            Dim adminDash As New frmAdminDashboard()
            adminDash.Show()
            Me.Hide()
        Else
            MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class