Public Class frmLogin
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not NetworkClient.Connect() Then
            btnLogin.Enabled = False
            MessageBox.Show("Please start the AgriMarket Server first, then restart this client.", "Server Offline", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Async Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim email As String = txtEmail.Text.Trim()
        Dim password As String = txtPassword.Text

        If email = "" Or password = "" Then
            MessageBox.Show("Please enter both email and password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        btnLogin.Enabled = False
        btnLogin.Text = "Connecting..."

        Dim response As String = Await NetworkClient.SendRequestAsync($"LOGIN|{email}|{password}")

        btnLogin.Enabled = True
        btnLogin.Text = "Log In"

        Dim parts As String() = response.Split("|"c)

        If parts(0) = "OK" Then
            Dim userId As Integer = Convert.ToInt32(parts(2))
            Dim role As String = parts(3)
            If role = "Admin" Then
                MessageBox.Show("Administrators must log in directly on the Server application.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            Dim name As String = parts(4)
            Dim status As String = parts(5)

            ' Check if a trader is pending approval
            If status = "Pending" Then
                MessageBox.Show("Your trader account is awaiting administrator approval.", "Account Pending", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim dashboard As New frmDashboard(userId, name, role)
            dashboard.Show()
            Me.Hide()
        Else
            MessageBox.Show(parts(3), "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPassword.Clear()
            txtPassword.Focus()
        End If
    End Sub
    Private Sub lnkSignUp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkSignUp.LinkClicked
        Dim registerForm As New frmRegister()
        registerForm.Show()
        Me.Hide()
    End Sub
End Class