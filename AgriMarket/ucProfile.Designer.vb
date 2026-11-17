<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucProfile
    Inherits System.Windows.Forms.UserControl

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
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.picProfile = New System.Windows.Forms.PictureBox()
        Me.btnChangePhoto = New ModernButton()
        Me.lblPhotoStatus = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.lblPhone = New System.Windows.Forms.Label()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.lblDistrict = New System.Windows.Forms.Label()
        Me.txtDistrict = New System.Windows.Forms.TextBox()
        Me.btnSave = New ModernButton()
        Me.lblChangePass = New System.Windows.Forms.Label()
        Me.lblNewPass = New System.Windows.Forms.Label()
        Me.txtNewPass = New System.Windows.Forms.TextBox()
        Me.btnChangePass = New ModernButton()
        Me.lblDebug = New System.Windows.Forms.Label()
        CType(Me.picProfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.lblTitle.Location = New System.Drawing.Point(30, 25)
        Me.lblTitle.Text = "My Profile & Settings"
        '
        'picProfile
        '
        Me.picProfile.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.picProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picProfile.Location = New System.Drawing.Point(30, 80)
        Me.picProfile.Name = "picProfile"
        Me.picProfile.Size = New System.Drawing.Size(100, 100)
        Me.picProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picProfile.TabStop = False
        '
        'btnChangePhoto
        '
        Me.btnChangePhoto.BackColor = System.Drawing.Color.FromArgb(95, 179, 179)
        Me.btnChangePhoto.ForeColor = System.Drawing.Color.White
        Me.btnChangePhoto.Location = New System.Drawing.Point(145, 80)
        Me.btnChangePhoto.Size = New System.Drawing.Size(140, 35)
        Me.btnChangePhoto.Text = "Change Photo"
        '
        'lblPhotoStatus
        '
        Me.lblPhotoStatus.AutoSize = True
        Me.lblPhotoStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic)
        Me.lblPhotoStatus.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblPhotoStatus.Location = New System.Drawing.Point(145, 125)
        Me.lblPhotoStatus.Text = "No photo selected"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblName.Location = New System.Drawing.Point(30, 200)
        Me.lblName.Text = "Display Name"
        '
        'txtName
        '
        Me.txtName.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Location = New System.Drawing.Point(30, 225)
        Me.txtName.Size = New System.Drawing.Size(350, 27)
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblEmail.Location = New System.Drawing.Point(30, 265)
        Me.lblEmail.Text = "Email (Read-Only)"
        '
        'txtEmail
        '
        Me.txtEmail.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtEmail.Location = New System.Drawing.Point(30, 290)
        Me.txtEmail.Size = New System.Drawing.Size(350, 27)
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.ReadOnly = True
        Me.txtEmail.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        '
        'lblPhone
        '
        Me.lblPhone.AutoSize = True
        Me.lblPhone.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblPhone.Location = New System.Drawing.Point(30, 330)
        Me.lblPhone.Text = "Phone Number"
        '
        'txtPhone
        '
        Me.txtPhone.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhone.Location = New System.Drawing.Point(30, 355)
        Me.txtPhone.Size = New System.Drawing.Size(350, 27)
        '
        'lblDistrict
        '
        Me.lblDistrict.AutoSize = True
        Me.lblDistrict.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblDistrict.Location = New System.Drawing.Point(30, 395)
        Me.lblDistrict.Text = "District"
        '
        'txtDistrict
        '
        Me.txtDistrict.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtDistrict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDistrict.Location = New System.Drawing.Point(30, 420)
        Me.txtDistrict.Size = New System.Drawing.Size(350, 27)
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(30, 470)
        Me.btnSave.Size = New System.Drawing.Size(350, 45)
        Me.btnSave.Text = "Save Profile"
        '
        'lblChangePass
        '
        Me.lblChangePass.AutoSize = True
        Me.lblChangePass.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblChangePass.Location = New System.Drawing.Point(500, 80)
        Me.lblChangePass.Text = "Change Password"
        '
        'lblNewPass
        '
        Me.lblNewPass.AutoSize = True
        Me.lblNewPass.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblNewPass.Location = New System.Drawing.Point(500, 120)
        Me.lblNewPass.Text = "New Password"
        '
        'txtNewPass
        '
        Me.txtNewPass.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtNewPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewPass.Location = New System.Drawing.Point(500, 145)
        Me.txtNewPass.Size = New System.Drawing.Size(350, 27)
        Me.txtNewPass.UseSystemPasswordChar = True
        '
        'btnChangePass
        '
        Me.btnChangePass.BackColor = System.Drawing.Color.FromArgb(95, 179, 179)
        Me.btnChangePass.ForeColor = System.Drawing.Color.White
        Me.btnChangePass.Location = New System.Drawing.Point(500, 195)
        Me.btnChangePass.Size = New System.Drawing.Size(350, 45)
        Me.btnChangePass.Text = "Update Password"
        '
        'lblDebug
        '
        Me.lblDebug.AutoSize = False
        Me.lblDebug.Font = New System.Drawing.Font("Consolas", 9.0!)
        Me.lblDebug.ForeColor = System.Drawing.Color.Gray
        Me.lblDebug.Location = New System.Drawing.Point(30, 560)
        Me.lblDebug.Size = New System.Drawing.Size(1100, 45)
        Me.lblDebug.Text = "Debug: (not loaded yet)"
        '
        'ucProfile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.picProfile)
        Me.Controls.Add(Me.btnChangePhoto)
        Me.Controls.Add(Me.lblPhotoStatus)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.lblEmail)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.lblPhone)
        Me.Controls.Add(Me.txtPhone)
        Me.Controls.Add(Me.lblDistrict)
        Me.Controls.Add(Me.txtDistrict)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.lblChangePass)
        Me.Controls.Add(Me.lblNewPass)
        Me.Controls.Add(Me.txtNewPass)
        Me.Controls.Add(Me.btnChangePass)
        Me.Controls.Add(Me.lblDebug)
        Me.Name = "ucProfile"
        Me.Size = New System.Drawing.Size(1180, 610)
        CType(Me.picProfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents picProfile As System.Windows.Forms.PictureBox
    Friend WithEvents btnChangePhoto As ModernButton
    Friend WithEvents lblPhotoStatus As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents lblPhone As System.Windows.Forms.Label
    Friend WithEvents txtPhone As System.Windows.Forms.TextBox
    Friend WithEvents lblDistrict As System.Windows.Forms.Label
    Friend WithEvents txtDistrict As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As ModernButton
    Friend WithEvents lblChangePass As System.Windows.Forms.Label
    Friend WithEvents lblNewPass As System.Windows.Forms.Label
    Friend WithEvents txtNewPass As System.Windows.Forms.TextBox
    Friend WithEvents btnChangePass As ModernButton
    Friend WithEvents lblDebug As System.Windows.Forms.Label
End Class