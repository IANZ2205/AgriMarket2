<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucProductDetail
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
        Me.picImage = New System.Windows.Forms.PictureBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.lblPrice = New System.Windows.Forms.Label()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.lblDesc = New System.Windows.Forms.Label()

        Me.pnlTrader = New System.Windows.Forms.Panel()
        Me.lblTraderTitle = New System.Windows.Forms.Label()
        Me.lblTraderName = New System.Windows.Forms.Label()
        Me.lblTraderPhone = New System.Windows.Forms.Label()
        Me.lblTraderDistrict = New System.Windows.Forms.Label()

        Me.btnPlaceOrder = New ModernButton()
        Me.btnChat = New ModernButton()
        Me.btnViewTrader = New ModernButton()

        CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTrader.SuspendLayout()
        Me.SuspendLayout()
        '
        'Back Button
        '
        Me.btnBack.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.btnBack.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnBack.Location = New System.Drawing.Point(30, 30)
        Me.btnBack.Size = New System.Drawing.Size(100, 35)
        Me.btnBack.Text = "< Back"
        '
        'Image
        '
        Me.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picImage.Location = New System.Drawing.Point(30, 90)
        Me.picImage.Size = New System.Drawing.Size(400, 300)
        Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        '
        'Name & Details
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.lblName.Location = New System.Drawing.Point(460, 90)
        Me.lblName.Text = "Produce Name"

        Me.lblCategory.AutoSize = True
        Me.lblCategory.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblCategory.ForeColor = System.Drawing.Color.Gray
        Me.lblCategory.Location = New System.Drawing.Point(465, 135)
        Me.lblCategory.Text = "Category"

        Me.lblPrice.AutoSize = True
        Me.lblPrice.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lblPrice.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.lblPrice.Location = New System.Drawing.Point(460, 170)
        Me.lblPrice.Text = "UGX 0"

        Me.lblQty.AutoSize = True
        Me.lblQty.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblQty.Location = New System.Drawing.Point(465, 210)
        Me.lblQty.Text = "0 available"

        Me.lblDesc.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.lblDesc.Location = New System.Drawing.Point(465, 250)
        Me.lblDesc.Size = New System.Drawing.Size(600, 100)
        Me.lblDesc.Text = "Description goes here..."

        'Trader Panel
        '
        Me.pnlTrader.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.pnlTrader.Controls.Add(Me.lblTraderTitle)
        Me.pnlTrader.Controls.Add(Me.lblTraderName)
        Me.pnlTrader.Controls.Add(Me.lblTraderPhone)
        Me.pnlTrader.Controls.Add(Me.lblTraderDistrict)
        Me.pnlTrader.Location = New System.Drawing.Point(30, 420)
        Me.pnlTrader.Size = New System.Drawing.Size(1035, 100)

        Me.lblTraderTitle.AutoSize = True
        Me.lblTraderTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTraderTitle.Location = New System.Drawing.Point(15, 15)
        Me.lblTraderTitle.Text = "Trader Information"

        Me.lblTraderName.AutoSize = True
        Me.lblTraderName.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblTraderName.Location = New System.Drawing.Point(15, 50)
        Me.lblTraderName.Text = "Name"

        Me.lblTraderPhone.AutoSize = True
        Me.lblTraderPhone.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblTraderPhone.Location = New System.Drawing.Point(300, 50)
        Me.lblTraderPhone.Text = "Phone"

        Me.lblTraderDistrict.AutoSize = True
        Me.lblTraderDistrict.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblTraderDistrict.Location = New System.Drawing.Point(550, 50)
        Me.lblTraderDistrict.Text = "District"

        'Action Buttons
        '
        Me.btnPlaceOrder.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnPlaceOrder.ForeColor = System.Drawing.Color.White
        Me.btnPlaceOrder.Location = New System.Drawing.Point(30, 540)
        Me.btnPlaceOrder.Size = New System.Drawing.Size(200, 45)
        Me.btnPlaceOrder.Text = "Place Order"

        Me.btnChat.BackColor = System.Drawing.Color.FromArgb(95, 179, 179)
        Me.btnChat.ForeColor = System.Drawing.Color.White
        Me.btnChat.Location = New System.Drawing.Point(250, 540)
        Me.btnChat.Size = New System.Drawing.Size(200, 45)
        Me.btnChat.Text = "Chat with Trader"

        Me.btnViewTrader.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.btnViewTrader.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnViewTrader.Location = New System.Drawing.Point(470, 540)
        Me.btnViewTrader.Size = New System.Drawing.Size(200, 45)
        Me.btnViewTrader.Text = "View Trader Details"

        'Add Controls
        '
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.picImage)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.lblCategory)
        Me.Controls.Add(Me.lblPrice)
        Me.Controls.Add(Me.lblQty)
        Me.Controls.Add(Me.lblDesc)
        Me.Controls.Add(Me.pnlTrader)
        Me.Controls.Add(Me.btnPlaceOrder)
        Me.Controls.Add(Me.btnChat)
        Me.Controls.Add(Me.btnViewTrader)

        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Name = "ucProductDetail"
        Me.Size = New System.Drawing.Size(1100, 620)

        CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTrader.ResumeLayout(False)
        Me.pnlTrader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents btnBack As ModernButton
    Friend WithEvents picImage As System.Windows.Forms.PictureBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Friend WithEvents lblPrice As System.Windows.Forms.Label
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents lblDesc As System.Windows.Forms.Label
    Friend WithEvents pnlTrader As System.Windows.Forms.Panel
    Friend WithEvents lblTraderTitle As System.Windows.Forms.Label
    Friend WithEvents lblTraderName As System.Windows.Forms.Label
    Friend WithEvents lblTraderPhone As System.Windows.Forms.Label
    Friend WithEvents lblTraderDistrict As System.Windows.Forms.Label
    Friend WithEvents btnPlaceOrder As ModernButton
    Friend WithEvents btnChat As ModernButton
    Friend WithEvents btnViewTrader As ModernButton
End Class