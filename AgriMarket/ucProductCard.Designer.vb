<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucProductCard
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
        Me.pnlImage = New System.Windows.Forms.Panel()
        Me.lblBadge = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblTrader = New System.Windows.Forms.Label()
        Me.lblPrice = New System.Windows.Forms.Label()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.btnView = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'pnlImage
        '
        Me.pnlImage.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.pnlImage.Location = New System.Drawing.Point(15, 15)
        Me.pnlImage.Name = "pnlImage"
        Me.pnlImage.Size = New System.Drawing.Size(240, 120)
        Me.pnlImage.TabIndex = 0
        '
        'lblBadge
        '
        Me.lblBadge.AutoSize = True
        Me.lblBadge.BackColor = System.Drawing.Color.FromArgb(221, 242, 232)
        Me.lblBadge.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblBadge.ForeColor = System.Drawing.Color.FromArgb(27, 122, 80)
        Me.lblBadge.Location = New System.Drawing.Point(170, 145)
        Me.lblBadge.Name = "lblBadge"
        Me.lblBadge.Padding = New System.Windows.Forms.Padding(5, 2, 5, 2)
        Me.lblBadge.Size = New System.Drawing.Size(80, 19)
        Me.lblBadge.TabIndex = 1
        Me.lblBadge.Text = "9 days left"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblName.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.lblName.Location = New System.Drawing.Point(15, 145)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(80, 21)
        Me.lblName.TabIndex = 2
        Me.lblName.Text = "Produce"
        '
        'lblTrader
        '
        Me.lblTrader.AutoSize = True
        Me.lblTrader.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTrader.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblTrader.Location = New System.Drawing.Point(15, 175)
        Me.lblTrader.Name = "lblTrader"
        Me.lblTrader.Size = New System.Drawing.Size(100, 15)
        Me.lblTrader.TabIndex = 3
        Me.lblTrader.Text = "Trader Name"
        '
        'lblPrice
        '
        Me.lblPrice.AutoSize = True
        Me.lblPrice.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblPrice.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.lblPrice.Location = New System.Drawing.Point(15, 200)
        Me.lblPrice.Name = "lblPrice"
        Me.lblPrice.Size = New System.Drawing.Size(100, 21)
        Me.lblPrice.TabIndex = 4
        Me.lblPrice.Text = "UGX 1,000"
        '
        'lblQty
        '
        Me.lblQty.AutoSize = True
        Me.lblQty.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblQty.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblQty.Location = New System.Drawing.Point(15, 225)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(50, 15)
        Me.lblQty.TabIndex = 5
        Me.lblQty.Text = "100 kg"
        '
        'btnView
        '
        Me.btnView.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnView.FlatAppearance.BorderSize = 0
        Me.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnView.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnView.ForeColor = System.Drawing.Color.White
        Me.btnView.Location = New System.Drawing.Point(180, 210)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(75, 30)
        Me.btnView.TabIndex = 6
        Me.btnView.Text = "View Details"
        Me.btnView.UseVisualStyleBackColor = False
        '
        'ucProductCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.btnView)
        Me.Controls.Add(Me.lblQty)
        Me.Controls.Add(Me.lblPrice)
        Me.Controls.Add(Me.lblTrader)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.lblBadge)
        Me.Controls.Add(Me.pnlImage)
        Me.Name = "ucProductCard"
        Me.Size = New System.Drawing.Size(270, 260)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlImage As System.Windows.Forms.Panel
    Friend WithEvents lblBadge As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblTrader As System.Windows.Forms.Label
    Friend WithEvents lblPrice As System.Windows.Forms.Label
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents btnView As System.Windows.Forms.Button
End Class