<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucTraderDetails
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
        Me.btnBack = New ModernButton()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblStats = New System.Windows.Forms.Label()
        Me.lblSectionTitle = New System.Windows.Forms.Label()
        Me.flowProducts = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnChat = New ModernButton()
        Me.SuspendLayout()
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.btnBack.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnBack.Location = New System.Drawing.Point(30, 30)
        Me.btnBack.Size = New System.Drawing.Size(100, 35)
        Me.btnBack.Text = "< Back"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.lblName.Location = New System.Drawing.Point(30, 90)
        Me.lblName.Text = "Trader Name"
        '
        'lblStats
        '
        Me.lblStats.AutoSize = True
        Me.lblStats.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblStats.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblStats.Location = New System.Drawing.Point(35, 135)
        Me.lblStats.Text = "District | Phone | Completed Orders"
        '
        'btnChat
        '
        Me.btnChat.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnChat.ForeColor = System.Drawing.Color.White
        Me.btnChat.Location = New System.Drawing.Point(900, 90)
        Me.btnChat.Size = New System.Drawing.Size(150, 45)
        Me.btnChat.Text = "Chat with Trader"
        '
        'lblSectionTitle
        '
        Me.lblSectionTitle.AutoSize = True
        Me.lblSectionTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblSectionTitle.Location = New System.Drawing.Point(30, 190)
        Me.lblSectionTitle.Text = "Other Produce from this Trader"
        '
        'flowProducts
        '
        Me.flowProducts.AutoScroll = True
        Me.flowProducts.Location = New System.Drawing.Point(30, 230)
        Me.flowProducts.Size = New System.Drawing.Size(1100, 380)
        '
        'ucTraderDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.lblStats)
        Me.Controls.Add(Me.btnChat)
        Me.Controls.Add(Me.lblSectionTitle)
        Me.Controls.Add(Me.flowProducts)
        Me.Name = "ucTraderDetails"
        Me.Size = New System.Drawing.Size(1180, 650)
        Me.ResumeLayout(False)
        Me.PerformLayout()


        Me.btnViewTrader = New ModernButton()
        Me.btnViewTrader.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.btnViewTrader.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnViewTrader.Location = New System.Drawing.Point(470, 540)
        Me.btnViewTrader.Size = New System.Drawing.Size(200, 45)
        Me.btnViewTrader.Text = "View Trader Details"
        Me.Controls.Add(Me.btnViewTrader)

    End Sub

    Friend WithEvents btnBack As ModernButton
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblStats As System.Windows.Forms.Label
    Friend WithEvents lblSectionTitle As System.Windows.Forms.Label
    Friend WithEvents flowProducts As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnChat As ModernButton
    Friend WithEvents btnViewTrader As ModernButton
End Class