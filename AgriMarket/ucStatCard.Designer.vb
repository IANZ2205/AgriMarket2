<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucStatCard
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
        Me.lblValue = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlIcon = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'lblValue
        '
        Me.lblValue.AutoSize = True
        Me.lblValue.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lblValue.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.lblValue.Location = New System.Drawing.Point(85, 30)
        Me.lblValue.Name = "lblValue"
        Me.lblValue.Size = New System.Drawing.Size(32, 37)
        Me.lblValue.TabIndex = 0
        Me.lblValue.Text = "0"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblTitle.Location = New System.Drawing.Point(88, 70)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(44, 19)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "Label"
        '
        'pnlIcon
        '
        Me.pnlIcon.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.pnlIcon.Location = New System.Drawing.Point(20, 30)
        Me.pnlIcon.Name = "pnlIcon"
        Me.pnlIcon.Size = New System.Drawing.Size(50, 50)
        Me.pnlIcon.TabIndex = 2
        '
        'ucStatCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.pnlIcon)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.lblValue)
        Me.Name = "ucStatCard"
        Me.Size = New System.Drawing.Size(250, 120)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblValue As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlIcon As System.Windows.Forms.Panel
End Class