<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucBrowse
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
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnSearch = New ModernButton()
        Me.btnRefresh = New ModernButton()
        Me.flowProducts = New System.Windows.Forms.FlowLayoutPanel()
        Me.panelOrder = New System.Windows.Forms.Panel()
        Me.lblOrderTitle = New System.Windows.Forms.Label()
        Me.lblSelectedItem = New System.Windows.Forms.Label()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.btnPlaceOrder = New ModernButton()
        Me.btnCancel = New ModernButton()
        Me.panelOrder.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.lblTitle.Location = New System.Drawing.Point(30, 30)
        Me.lblTitle.Text = "Fresh produce from traders near you"
        '
        'txtSearch
        '
        Me.txtSearch.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtSearch.Location = New System.Drawing.Point(30, 90)
        Me.txtSearch.Size = New System.Drawing.Size(400, 29)
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(440, 85)
        Me.btnSearch.Size = New System.Drawing.Size(100, 40)
        Me.btnSearch.Text = "Search"
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(1050, 30)
        Me.btnRefresh.Size = New System.Drawing.Size(100, 40)
        Me.btnRefresh.Text = "Refresh"
        '
        'flowProducts
        '
        Me.flowProducts.AutoScroll = True
        Me.flowProducts.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.flowProducts.Location = New System.Drawing.Point(30, 140)
        Me.flowProducts.Size = New System.Drawing.Size(1120, 450)
        '
        'panelOrder
        '
        Me.panelOrder.BackColor = System.Drawing.Color.White
        Me.panelOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelOrder.Controls.Add(Me.lblOrderTitle)
        Me.panelOrder.Controls.Add(Me.lblSelectedItem)
        Me.panelOrder.Controls.Add(Me.lblQty)
        Me.panelOrder.Controls.Add(Me.txtQty)
        Me.panelOrder.Controls.Add(Me.lblTotal)
        Me.panelOrder.Controls.Add(Me.btnPlaceOrder)
        Me.panelOrder.Controls.Add(Me.btnCancel)
        Me.panelOrder.Location = New System.Drawing.Point(700, 140)
        Me.panelOrder.Size = New System.Drawing.Size(450, 350)
        Me.panelOrder.Visible = False
        '
        'lblOrderTitle
        '
        Me.lblOrderTitle.AutoSize = True
        Me.lblOrderTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblOrderTitle.Location = New System.Drawing.Point(20, 20)
        Me.lblOrderTitle.Text = "Place an Order"
        '
        'lblSelectedItem
        '
        Me.lblSelectedItem.AutoSize = True
        Me.lblSelectedItem.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblSelectedItem.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.lblSelectedItem.Location = New System.Drawing.Point(20, 70)
        Me.lblSelectedItem.Text = "Item Name"
        '
        'lblQty
        '
        Me.lblQty.AutoSize = True
        Me.lblQty.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblQty.Location = New System.Drawing.Point(20, 110)
        Me.lblQty.Text = "Quantity (kg)"
        '
        'txtQty
        '
        Me.txtQty.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtQty.Location = New System.Drawing.Point(20, 135)
        Me.txtQty.Size = New System.Drawing.Size(400, 29)
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotal.ForeColor = System.Drawing.Color.FromArgb(138, 82, 0)
        Me.lblTotal.Location = New System.Drawing.Point(20, 190)
        Me.lblTotal.Text = "Estimated Total: 0"
        '
        'btnPlaceOrder
        '
        Me.btnPlaceOrder.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnPlaceOrder.ForeColor = System.Drawing.Color.White
        Me.btnPlaceOrder.Location = New System.Drawing.Point(20, 250)
        Me.btnPlaceOrder.Size = New System.Drawing.Size(190, 45)
        Me.btnPlaceOrder.Text = "Confirm Order"
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.btnCancel.Location = New System.Drawing.Point(230, 250)
        Me.btnCancel.Size = New System.Drawing.Size(190, 45)
        Me.btnCancel.Text = "Cancel"
        '
        'ucBrowse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.Controls.Add(Me.panelOrder)
        Me.Controls.Add(Me.flowProducts)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.lblTitle)
        Me.Name = "ucBrowse"
        Me.Size = New System.Drawing.Size(1180, 610)
        Me.panelOrder.ResumeLayout(False)
        Me.panelOrder.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As ModernButton
    Friend WithEvents btnRefresh As ModernButton
    Friend WithEvents flowProducts As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents panelOrder As System.Windows.Forms.Panel
    Friend WithEvents lblOrderTitle As System.Windows.Forms.Label
    Friend WithEvents lblSelectedItem As System.Windows.Forms.Label
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents btnPlaceOrder As ModernButton
    Friend WithEvents btnCancel As ModernButton
End Class