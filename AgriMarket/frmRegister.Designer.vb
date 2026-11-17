<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRegister
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.panelLeft = New System.Windows.Forms.Panel()
        Me.lblLogo = New System.Windows.Forms.Label()
        Me.lblTagline = New System.Windows.Forms.Label()
        Me.lblDesc = New System.Windows.Forms.Label()
        Me.panelRegister = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblRole = New System.Windows.Forms.Label()
        Me.cmbRole = New System.Windows.Forms.ComboBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.lblConfirm = New System.Windows.Forms.Label()
        Me.txtConfirm = New System.Windows.Forms.TextBox()
        Me.lblSecurityQ = New System.Windows.Forms.Label()
        Me.cmbSecurityQ = New System.Windows.Forms.ComboBox()
        Me.lblAnswer = New System.Windows.Forms.Label()
        Me.txtAnswer = New System.Windows.Forms.TextBox()
        Me.lblProfilePic = New System.Windows.Forms.Label()
        Me.btnUploadPic = New ModernButton()
        Me.lblPicStatus = New System.Windows.Forms.Label()
        Me.picProfile = New System.Windows.Forms.PictureBox()
        Me.btnRegister = New ModernButton()
        Me.lnkBackToLogin = New System.Windows.Forms.LinkLabel()
        Me.panelLeft.SuspendLayout()
        Me.panelRegister.SuspendLayout()
        CType(Me.picProfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panelLeft
        '
        Me.panelLeft.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.panelLeft.Controls.Add(Me.lblLogo)
        Me.panelLeft.Controls.Add(Me.lblTagline)
        Me.panelLeft.Controls.Add(Me.lblDesc)
        Me.panelLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.panelLeft.Location = New System.Drawing.Point(0, 0)
        Me.panelLeft.Name = "panelLeft"
        Me.panelLeft.Size = New System.Drawing.Size(450, 760)
        Me.panelLeft.TabIndex = 0
        '
        'lblLogo
        '
        Me.lblLogo.AutoSize = True
        Me.lblLogo.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lblLogo.ForeColor = System.Drawing.Color.White
        Me.lblLogo.Location = New System.Drawing.Point(60, 60)
        Me.lblLogo.Name = "lblLogo"
        Me.lblLogo.Size = New System.Drawing.Size(175, 37)
        Me.lblLogo.TabIndex = 0
        Me.lblLogo.Text = "AgriMarket"
        '
        'lblTagline
        '
        Me.lblTagline.AutoSize = True
        Me.lblTagline.Font = New System.Drawing.Font("Segoe UI", 28.0!, System.Drawing.FontStyle.Bold)
        Me.lblTagline.ForeColor = System.Drawing.Color.White
        Me.lblTagline.Location = New System.Drawing.Point(50, 180)
        Me.lblTagline.Name = "lblTagline"
        Me.lblTagline.Size = New System.Drawing.Size(280, 51)
        Me.lblTagline.TabIndex = 1
        Me.lblTagline.Text = "Join the market."
        '
        'lblDesc
        '
        Me.lblDesc.AutoSize = True
        Me.lblDesc.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblDesc.ForeColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.lblDesc.Location = New System.Drawing.Point(55, 300)
        Me.lblDesc.MaximumSize = New System.Drawing.Size(350, 0)
        Me.lblDesc.Name = "lblDesc"
        Me.lblDesc.Size = New System.Drawing.Size(354, 63)
        Me.lblDesc.TabIndex = 2
        Me.lblDesc.Text = "Create your account in a minute. Traders are approved by an admin before they can post stock."
        '
        'panelRegister
        '
        Me.panelRegister.BackColor = System.Drawing.Color.White
        Me.panelRegister.Controls.Add(Me.lblTitle)
        Me.panelRegister.Controls.Add(Me.lblRole)
        Me.panelRegister.Controls.Add(Me.cmbRole)
        Me.panelRegister.Controls.Add(Me.lblName)
        Me.panelRegister.Controls.Add(Me.txtName)
        Me.panelRegister.Controls.Add(Me.lblEmail)
        Me.panelRegister.Controls.Add(Me.txtEmail)
        Me.panelRegister.Controls.Add(Me.lblPassword)
        Me.panelRegister.Controls.Add(Me.txtPassword)
        Me.panelRegister.Controls.Add(Me.lblConfirm)
        Me.panelRegister.Controls.Add(Me.txtConfirm)
        Me.panelRegister.Controls.Add(Me.lblSecurityQ)
        Me.panelRegister.Controls.Add(Me.cmbSecurityQ)
        Me.panelRegister.Controls.Add(Me.lblAnswer)
        Me.panelRegister.Controls.Add(Me.txtAnswer)
        Me.panelRegister.Controls.Add(Me.lblProfilePic)
        Me.panelRegister.Controls.Add(Me.btnUploadPic)
        Me.panelRegister.Controls.Add(Me.lblPicStatus)
        Me.panelRegister.Controls.Add(Me.picProfile)
        Me.panelRegister.Controls.Add(Me.btnRegister)
        Me.panelRegister.Controls.Add(Me.lnkBackToLogin)
        Me.panelRegister.Location = New System.Drawing.Point(580, 15)
        Me.panelRegister.Name = "panelRegister"
        Me.panelRegister.Size = New System.Drawing.Size(480, 730)
        Me.panelRegister.TabIndex = 1
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.lblTitle.Location = New System.Drawing.Point(40, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(263, 37)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Create your account"
        '
        'lblRole
        '
        Me.lblRole.AutoSize = True
        Me.lblRole.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblRole.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblRole.Location = New System.Drawing.Point(40, 55)
        Me.lblRole.Name = "lblRole"
        Me.lblRole.Size = New System.Drawing.Size(39, 19)
        Me.lblRole.TabIndex = 1
        Me.lblRole.Text = "I am a"
        '
        'cmbRole
        '
        Me.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRole.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.cmbRole.FormattingEnabled = True
        Me.cmbRole.Items.AddRange(New Object() {"Trader", "Retailer", "Wholesaler"})
        Me.cmbRole.Location = New System.Drawing.Point(40, 77)
        Me.cmbRole.Name = "cmbRole"
        Me.cmbRole.Size = New System.Drawing.Size(400, 29)
        Me.cmbRole.TabIndex = 2
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblName.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblName.Location = New System.Drawing.Point(40, 115)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(122, 19)
        Me.lblName.TabIndex = 3
        Me.lblName.Text = "Full Name / Display"
        '
        'txtName
        '
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtName.Location = New System.Drawing.Point(40, 137)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(400, 29)
        Me.txtName.TabIndex = 4
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblEmail.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblEmail.Location = New System.Drawing.Point(40, 175)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(46, 19)
        Me.lblEmail.TabIndex = 5
        Me.lblEmail.Text = "Email"
        '
        'txtEmail
        '
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtEmail.Location = New System.Drawing.Point(40, 197)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(400, 29)
        Me.txtEmail.TabIndex = 6
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblPassword.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblPassword.Location = New System.Drawing.Point(40, 235)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(74, 19)
        Me.lblPassword.TabIndex = 7
        Me.lblPassword.Text = "Password"
        '
        'txtPassword
        '
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtPassword.Location = New System.Drawing.Point(40, 257)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(400, 29)
        Me.txtPassword.TabIndex = 8
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'lblConfirm
        '
        Me.lblConfirm.AutoSize = True
        Me.lblConfirm.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblConfirm.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblConfirm.Location = New System.Drawing.Point(40, 295)
        Me.lblConfirm.Name = "lblConfirm"
        Me.lblConfirm.Size = New System.Drawing.Size(129, 19)
        Me.lblConfirm.TabIndex = 9
        Me.lblConfirm.Text = "Confirm Password"
        '
        'txtConfirm
        '
        Me.txtConfirm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtConfirm.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtConfirm.Location = New System.Drawing.Point(40, 317)
        Me.txtConfirm.Name = "txtConfirm"
        Me.txtConfirm.Size = New System.Drawing.Size(400, 29)
        Me.txtConfirm.TabIndex = 10
        Me.txtConfirm.UseSystemPasswordChar = True
        '
        'lblSecurityQ
        '
        Me.lblSecurityQ.AutoSize = True
        Me.lblSecurityQ.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblSecurityQ.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblSecurityQ.Location = New System.Drawing.Point(40, 355)
        Me.lblSecurityQ.Name = "lblSecurityQ"
        Me.lblSecurityQ.Size = New System.Drawing.Size(180, 19)
        Me.lblSecurityQ.TabIndex = 11
        Me.lblSecurityQ.Text = "Security Question"
        '
        'cmbSecurityQ
        '
        Me.cmbSecurityQ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSecurityQ.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.cmbSecurityQ.FormattingEnabled = True
        Me.cmbSecurityQ.Location = New System.Drawing.Point(40, 377)
        Me.cmbSecurityQ.Name = "cmbSecurityQ"
        Me.cmbSecurityQ.Size = New System.Drawing.Size(400, 29)
        Me.cmbSecurityQ.TabIndex = 12
        '
        'lblAnswer
        '
        Me.lblAnswer.AutoSize = True
        Me.lblAnswer.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblAnswer.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblAnswer.Location = New System.Drawing.Point(40, 415)
        Me.lblAnswer.Name = "lblAnswer"
        Me.lblAnswer.Size = New System.Drawing.Size(200, 19)
        Me.lblAnswer.TabIndex = 13
        Me.lblAnswer.Text = "Your Answer (case-insensitive)"
        '
        'txtAnswer
        '
        Me.txtAnswer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAnswer.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtAnswer.Location = New System.Drawing.Point(40, 437)
        Me.txtAnswer.Name = "txtAnswer"
        Me.txtAnswer.Size = New System.Drawing.Size(400, 29)
        Me.txtAnswer.TabIndex = 14
        '
        'lblProfilePic
        '
        Me.lblProfilePic.AutoSize = True
        Me.lblProfilePic.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblProfilePic.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblProfilePic.Location = New System.Drawing.Point(40, 480)
        Me.lblProfilePic.Name = "lblProfilePic"
        Me.lblProfilePic.Size = New System.Drawing.Size(180, 19)
        Me.lblProfilePic.TabIndex = 15
        Me.lblProfilePic.Text = "Profile Picture (Optional)"
        '
        'btnUploadPic
        '
        Me.btnUploadPic.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.btnUploadPic.FlatAppearance.BorderSize = 0
        Me.btnUploadPic.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUploadPic.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnUploadPic.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnUploadPic.Location = New System.Drawing.Point(40, 503)
        Me.btnUploadPic.Name = "btnUploadPic"
        Me.btnUploadPic.Size = New System.Drawing.Size(140, 35)
        Me.btnUploadPic.TabIndex = 16
        Me.btnUploadPic.Text = "Choose Image"
        Me.btnUploadPic.UseVisualStyleBackColor = False
        '
        'lblPicStatus
        '
        Me.lblPicStatus.AutoSize = True
        Me.lblPicStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic)
        Me.lblPicStatus.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblPicStatus.Location = New System.Drawing.Point(190, 513)
        Me.lblPicStatus.Name = "lblPicStatus"
        Me.lblPicStatus.Size = New System.Drawing.Size(80, 15)
        Me.lblPicStatus.TabIndex = 17
        Me.lblPicStatus.Text = "No file chosen"
        '
        'picProfile
        '
        Me.picProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picProfile.Location = New System.Drawing.Point(390, 498)
        Me.picProfile.Name = "picProfile"
        Me.picProfile.Size = New System.Drawing.Size(50, 50)
        Me.picProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picProfile.TabIndex = 18
        Me.picProfile.TabStop = False
        '
        'btnRegister
        '
        Me.btnRegister.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnRegister.FlatAppearance.BorderSize = 0
        Me.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRegister.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnRegister.ForeColor = System.Drawing.Color.White
        Me.btnRegister.Location = New System.Drawing.Point(40, 565)
        Me.btnRegister.Name = "btnRegister"
        Me.btnRegister.Size = New System.Drawing.Size(400, 45)
        Me.btnRegister.TabIndex = 19
        Me.btnRegister.Text = "Create Account"
        Me.btnRegister.UseVisualStyleBackColor = False
        '
        'lnkBackToLogin
        '
        Me.lnkBackToLogin.AutoSize = True
        Me.lnkBackToLogin.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lnkBackToLogin.LinkColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.lnkBackToLogin.Location = New System.Drawing.Point(150, 630)
        Me.lnkBackToLogin.Name = "lnkBackToLogin"
        Me.lnkBackToLogin.Size = New System.Drawing.Size(196, 19)
        Me.lnkBackToLogin.TabIndex = 20
        Me.lnkBackToLogin.TabStop = True
        Me.lnkBackToLogin.Text = "Already have an account? Log in"
        '
        'frmRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.ClientSize = New System.Drawing.Size(1180, 760)
        Me.MinimumSize = New System.Drawing.Size(1180, 760)
        Me.Controls.Add(Me.panelRegister)
        Me.Controls.Add(Me.panelLeft)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "frmRegister"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AgriMarket - Sign Up"
        Me.panelLeft.ResumeLayout(False)
        Me.panelLeft.PerformLayout()
        Me.panelRegister.ResumeLayout(False)
        Me.panelRegister.PerformLayout()
        CType(Me.picProfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelLeft As System.Windows.Forms.Panel
    Friend WithEvents lblLogo As System.Windows.Forms.Label
    Friend WithEvents lblTagline As System.Windows.Forms.Label
    Friend WithEvents lblDesc As System.Windows.Forms.Label
    Friend WithEvents panelRegister As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblRole As System.Windows.Forms.Label
    Friend WithEvents cmbRole As System.Windows.Forms.ComboBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblConfirm As System.Windows.Forms.Label
    Friend WithEvents txtConfirm As System.Windows.Forms.TextBox
    Friend WithEvents lblSecurityQ As System.Windows.Forms.Label
    Friend WithEvents cmbSecurityQ As System.Windows.Forms.ComboBox
    Friend WithEvents lblAnswer As System.Windows.Forms.Label
    Friend WithEvents txtAnswer As System.Windows.Forms.TextBox
    Friend WithEvents lblProfilePic As System.Windows.Forms.Label
    Friend WithEvents btnUploadPic As ModernButton
    Friend WithEvents lblPicStatus As System.Windows.Forms.Label
    Friend WithEvents picProfile As System.Windows.Forms.PictureBox
    Friend WithEvents btnRegister As ModernButton
    Friend WithEvents lnkBackToLogin As System.Windows.Forms.LinkLabel
End Class