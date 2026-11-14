<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucNotifications
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
        Me.btnMarkRead = New ModernButton()
        Me.dgvNotifs = New System.Windows.Forms.DataGridView()
        CType(Me.dgvNotifs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.lblTitle.Location = New System.Drawing.Point(30, 30)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(200, 37)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Notifications"
        '
        'btnMarkRead
        '
        Me.btnMarkRead.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnMarkRead.ForeColor = System.Drawing.Color.White
        Me.btnMarkRead.Location = New System.Drawing.Point(1000, 30)
        Me.btnMarkRead.Name = "btnMarkRead"
        Me.btnMarkRead.Size = New System.Drawing.Size(150, 40)
        Me.btnMarkRead.TabIndex = 1
        Me.btnMarkRead.Text = "Mark All Read"
        '
        'dgvNotifs
        '
        Me.dgvNotifs.AllowUserToAddRows = False
        Me.dgvNotifs.AllowUserToDeleteRows = False
        Me.dgvNotifs.Location = New System.Drawing.Point(30, 90)
        Me.dgvNotifs.Name = "dgvNotifs"
        Me.dgvNotifs.ReadOnly = True
        Me.dgvNotifs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvNotifs.Size = New System.Drawing.Size(1120, 500)
        Me.dgvNotifs.TabIndex = 2
        '
        'ucNotifications
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.Controls.Add(Me.dgvNotifs)
        Me.Controls.Add(Me.btnMarkRead)
        Me.Controls.Add(Me.lblTitle)
        Me.Name = "ucNotifications"
        Me.Size = New System.Drawing.Size(1180, 610)
        CType(Me.dgvNotifs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnMarkRead As ModernButton
    Friend WithEvents dgvNotifs As System.Windows.Forms.DataGridView
End Class