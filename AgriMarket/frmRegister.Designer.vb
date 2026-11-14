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
        Me.lblProfilePic = New System.Windows.Forms.Label()
        Me.btnUploadPic = New ModernButton()
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
        Me.panelLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.panelLeft.Controls.Add(Me.lblLogo)
        Me.panelLeft.Controls.Add(Me.lblTagline)
        Me.panelLeft.Controls.Add(Me.lblDesc)
        Me.panelLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.panelLeft.Location = New System.Drawing.Point(0, 0)
        Me.panelLeft.Name = "panelLeft"
        Me.panelLeft.Size = New System.Drawing.Size(450, 680)
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
        Me.lblDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
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
        Me.panelRegister.Controls.Add(Me.lblProfilePic)
        Me.panelRegister.Controls.Add(Me.btnUploadPic)
        Me.panelRegister.Controls.Add(Me.picProfile)
        Me.panelRegister.Controls.Add(Me.btnRegister)
        Me.panelRegister.Controls.Add(Me.lnkBackToLogin)
        Me.panelRegister.Location = New System.Drawing.Point(580, 30)
        Me.panelRegister.Name = "panelRegister"
        Me.panelRegister.Size = New System.Drawing.Size(480, 620)
        Me.panelRegister.TabIndex = 1
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(40, 20)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(263, 37)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Create your account"
        '
        'lblRole
        '
        Me.lblRole.AutoSize = True
        Me.lblRole.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblRole.ForeColor = System.Drawing.Color.FromArgb(CType(CType(79, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblRole.Location = New System.Drawing.Point(40, 70)
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
        Me.cmbRole.Location = New System.Drawing.Point(40, 92)
        Me.cmbRole.Name = "cmbRole"
        Me.cmbRole.Size = New System.Drawing.Size(400, 29)
        Me.cmbRole.TabIndex = 2
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(79, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblName.Location = New System.Drawing.Point(40, 135)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(122, 19)
        Me.lblName.TabIndex = 3
        Me.lblName.Text = "Full Name / Display"
        '
        'txtName
        '
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtName.Location = New System.Drawing.Point(40, 157)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(400, 29)
        Me.txtName.TabIndex = 4
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(79, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblEmail.Location = New System.Drawing.Point(40, 200)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(46, 19)
        Me.lblEmail.TabIndex = 5
        Me.lblEmail.Text = "Email"
        '
        'txtEmail
        '
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtEmail.Location = New System.Drawing.Point(40, 222)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(400, 29)
        Me.txtEmail.TabIndex = 6
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(79, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblPassword.Location = New System.Drawing.Point(40, 265)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(74, 19)
        Me.lblPassword.TabIndex = 7
        Me.lblPassword.Text = "Password"
        '
        'txtPassword
        '
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtPassword.Location = New System.Drawing.Point(40, 287)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(400, 29)
        Me.txtPassword.TabIndex = 8
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'lblConfirm
        '
        Me.lblConfirm.AutoSize = True
        Me.lblConfirm.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblConfirm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(79, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblConfirm.Location = New System.Drawing.Point(40, 330)
        Me.lblConfirm.Name = "lblConfirm"
        Me.lblConfirm.Size = New System.Drawing.Size(129, 19)
        Me.lblConfirm.TabIndex = 9
        Me.lblConfirm.Text = "Confirm Password"
        '
        'txtConfirm
        '
        Me.txtConfirm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtConfirm.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtConfirm.Location = New System.Drawing.Point(40, 352)
        Me.txtConfirm.Name = "txtConfirm"
        Me.txtConfirm.Size = New System.Drawing.Size(400, 29)
        Me.txtConfirm.TabIndex = 10
        Me.txtConfirm.UseSystemPasswordChar = True
        '
        'lblProfilePic
        '
        Me.lblProfilePic.AutoSize = True
        Me.lblProfilePic.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblProfilePic.ForeColor = System.Drawing.Color.FromArgb(CType(CType(79, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.lblProfilePic.Location = New System.Drawing.Point(40, 395)
        Me.lblProfilePic.Name = "lblProfilePic"
        Me.lblProfilePic.Size = New System.Drawing.Size(130, 19)
        Me.lblProfilePic.TabIndex = 11
        Me.lblProfilePic.Text = "Profile Picture (Optional)"
        '
        'btnUploadPic
        '
        Me.btnUploadPic.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.btnUploadPic.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.btnUploadPic.Location = New System.Drawing.Point(40, 420)
        Me.btnUploadPic.Name = "btnUploadPic"
        Me.btnUploadPic.Size = New System.Drawing.Size(140, 35)
        Me.btnUploadPic.TabIndex = 12
        Me.btnUploadPic.Text = "Choose Image"
        '
        'picProfile
        '
        Me.picProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picProfile.Location = New System.Drawing.Point(190, 415)
        Me.picProfile.Name = "picProfile"
        Me.picProfile.Size = New System.Drawing.Size(45, 45)
        Me.picProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picProfile.TabIndex = 13
        Me.picProfile.TabStop = False
        '
        'btnRegister
        '
        Me.btnRegister.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.btnRegister.ForeColor = System.Drawing.Color.White
        Me.btnRegister.Location = New System.Drawing.Point(40, 480)
        Me.btnRegister.Name = "btnRegister"
        Me.btnRegister.Size = New System.Drawing.Size(400, 45)
        Me.btnRegister.TabIndex = 14
        Me.btnRegister.Text = "Create Account"
        '
        'lnkBackToLogin
        '
        Me.lnkBackToLogin.AutoSize = True
        Me.lnkBackToLogin.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lnkBackToLogin.LinkColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.lnkBackToLogin.Location = New System.Drawing.Point(150, 540)
        Me.lnkBackToLogin.Name = "lnkBackToLogin"
        Me.lnkBackToLogin.Size = New System.Drawing.Size(166, 19)
        Me.lnkBackToLogin.TabIndex = 15
        Me.lnkBackToLogin.TabStop = True
        Me.lnkBackToLogin.Text = "Already have an account? Log in"
        '
        'frmRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1180, 680)
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
    Friend WithEvents lblProfilePic As System.Windows.Forms.Label
    Friend WithEvents btnUploadPic As ModernButton
    Friend WithEvents picProfile As System.Windows.Forms.PictureBox
    Friend WithEvents btnRegister As ModernButton
    Friend WithEvents lnkBackToLogin As System.Windows.Forms.LinkLabel
End Class