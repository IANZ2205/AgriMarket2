Public Class frmForcePasswordChange
    Private _userId As Integer
    Private lblTitle As System.Windows.Forms.Label
    Private lblMessage As System.Windows.Forms.Label
    Private lblNewPass As System.Windows.Forms.Label
    Private txtNewPass As System.Windows.Forms.TextBox
    Private lblConfirm As System.Windows.Forms.Label
    Private txtConfirm As System.Windows.Forms.TextBox
    Private btnSave As System.Windows.Forms.Button
    Private btnCancel As System.Windows.Forms.Button

    Public Sub New(userId As Integer)
        InitializeComponent()
        _userId = userId
    End Sub

    Private Sub frmForcePasswordChange_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildUI()
    End Sub

    Private Sub BuildUI()
        lblTitle = New System.Windows.Forms.Label()
        lblTitle.AutoSize = True
        lblTitle.Font = New System.Drawing.Font("Segoe UI", 20, System.Drawing.FontStyle.Bold)
        lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        lblTitle.Location = New System.Drawing.Point(30, 25)
        lblTitle.Text = "Set a new password"
        Me.Controls.Add(lblTitle)

        lblMessage = New System.Windows.Forms.Label()
        lblMessage.AutoSize = False
        lblMessage.Font = New System.Drawing.Font("Segoe UI", 10)
        lblMessage.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblMessage.Location = New System.Drawing.Point(32, 70)
        lblMessage.Size = New System.Drawing.Size(455, 45)
        lblMessage.Text = "Your password was reset by an administrator. Please set a new password to continue."
        Me.Controls.Add(lblMessage)

        lblNewPass = New System.Windows.Forms.Label()
        lblNewPass.AutoSize = True
        lblNewPass.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        lblNewPass.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblNewPass.Location = New System.Drawing.Point(32, 135)
        lblNewPass.Text = "New Password"
        Me.Controls.Add(lblNewPass)

        txtNewPass = New System.Windows.Forms.TextBox()
        txtNewPass.Font = New System.Drawing.Font("Segoe UI", 12)
        txtNewPass.Location = New System.Drawing.Point(32, 158)
        txtNewPass.Size = New System.Drawing.Size(455, 29)
        txtNewPass.UseSystemPasswordChar = True
        Me.Controls.Add(txtNewPass)

        lblConfirm = New System.Windows.Forms.Label()
        lblConfirm.AutoSize = True
        lblConfirm.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        lblConfirm.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblConfirm.Location = New System.Drawing.Point(32, 200)
        lblConfirm.Text = "Confirm Password"
        Me.Controls.Add(lblConfirm)

        txtConfirm = New System.Windows.Forms.TextBox()
        txtConfirm.Font = New System.Drawing.Font("Segoe UI", 12)
        txtConfirm.Location = New System.Drawing.Point(32, 223)
        txtConfirm.Size = New System.Drawing.Size(455, 29)
        txtConfirm.UseSystemPasswordChar = True
        Me.Controls.Add(txtConfirm)

        btnSave = New System.Windows.Forms.Button()
        btnSave.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.Font = New System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold)
        btnSave.ForeColor = System.Drawing.Color.White
        btnSave.Location = New System.Drawing.Point(32, 290)
        btnSave.Size = New System.Drawing.Size(220, 45)
        btnSave.Text = "Set Password"
        btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnSave.Click, AddressOf btnSave_Click
        Me.Controls.Add(btnSave)

        btnCancel = New System.Windows.Forms.Button()
        btnCancel.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.Font = New System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold)
        btnCancel.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        btnCancel.Location = New System.Drawing.Point(267, 290)
        btnCancel.Size = New System.Drawing.Size(220, 45)
        btnCancel.Text = "Cancel"
        btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnCancel.Click, AddressOf btnCancel_Click
        Me.Controls.Add(btnCancel)

        Me.AcceptButton = btnSave
        Me.CancelButton = btnCancel
    End Sub

    Private Async Sub btnSave_Click(sender As Object, e As EventArgs)
        Dim p1 As String = txtNewPass.Text
        Dim p2 As String = txtConfirm.Text

        If String.IsNullOrWhiteSpace(p1) Then
            System.Windows.Forms.MessageBox.Show("Please enter a new password.", "Validation", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If
        If p1.Length < 6 Then
            System.Windows.Forms.MessageBox.Show("Password must be at least 6 characters.", "Validation", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If
        If p1 <> p2 Then
            System.Windows.Forms.MessageBox.Show("Passwords do not match.", "Validation", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If

        btnSave.Enabled = False
        btnSave.Text = "Saving..."

        Dim response As String = Await NetworkClient.SendRequestAsync($"COMPLETE_PASSWORD_RESET|{_userId}|{p1}")
        btnSave.Enabled = True
        btnSave.Text = "Set Password"

        Dim parts As String() = response.Split("|"c)
        If parts(0) = "OK" Then
            System.Windows.Forms.MessageBox.Show("Password updated successfully. Welcome to AgriMarket!", "Success", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            Dim msg As String = "Could not save password."
            If parts.Length > 3 Then msg = parts(3)
            System.Windows.Forms.MessageBox.Show(msg, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class