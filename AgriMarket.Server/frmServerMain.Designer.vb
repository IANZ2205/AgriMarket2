<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmServerMain
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
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtLog = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.lblTitle.Location = New System.Drawing.Point(30, 30)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(400, 45)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "AgriMarket Server Console"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(179, 38, 30)
        Me.lblStatus.Location = New System.Drawing.Point(35, 90)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(120, 21)
        Me.lblStatus.TabIndex = 1
        Me.lblStatus.Text = "Status: OFFLINE"

        Me.btnAdminLogin = New System.Windows.Forms.Button()
        Me.btnAdminLogin.BackColor = System.Drawing.Color.FromArgb(95, 179, 179)
        Me.btnAdminLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdminLogin.FlatAppearance.BorderSize = 0
        Me.btnAdminLogin.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnAdminLogin.ForeColor = System.Drawing.Color.White
        Me.btnAdminLogin.Location = New System.Drawing.Point(800, 85)
        Me.btnAdminLogin.Name = "btnAdminLogin"
        Me.btnAdminLogin.Size = New System.Drawing.Size(150, 35)
        Me.btnAdminLogin.Text = "Open Admin Login"
        Me.btnAdminLogin.UseVisualStyleBackColor = False
        Me.Controls.Add(Me.btnAdminLogin)
        '
        'txtLog
        '
        Me.txtLog.BackColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLog.Font = New System.Drawing.Font("Consolas", 11.0!)
        Me.txtLog.ForeColor = System.Drawing.Color.FromArgb(27, 122, 80)
        Me.txtLog.Location = New System.Drawing.Point(30, 130)
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ReadOnly = True
        Me.txtLog.Size = New System.Drawing.Size(920, 500)
        Me.txtLog.TabIndex = 2
        Me.txtLog.Text = ""
        '
        'frmServerMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.ClientSize = New System.Drawing.Size(984, 661)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.lblTitle)
        Me.Name = "frmServerMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AgriMarket Server"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents txtLog As System.Windows.Forms.RichTextBox
    Friend WithEvents btnAdminLogin As System.Windows.Forms.Button
End Class