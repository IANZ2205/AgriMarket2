<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucMyOrders
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
        Me.btnRefresh = New ModernButton()
        Me.btnCancelOrder = New ModernButton()
        Me.dgvMyOrders = New System.Windows.Forms.DataGridView()
        CType(Me.dgvMyOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.lblTitle.Location = New System.Drawing.Point(30, 30)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(167, 37)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "My Orders"
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(1050, 30)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(100, 40)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "Refresh"
        '
        'btnCancelOrder
        '
        Me.btnCancelOrder.BackColor = System.Drawing.Color.FromArgb(179, 38, 30)
        Me.btnCancelOrder.ForeColor = System.Drawing.Color.White
        Me.btnCancelOrder.Location = New System.Drawing.Point(930, 30)
        Me.btnCancelOrder.Name = "btnCancelOrder"
        Me.btnCancelOrder.Size = New System.Drawing.Size(110, 40)
        Me.btnCancelOrder.TabIndex = 2
        Me.btnCancelOrder.Text = "Cancel Order"
        '
        'dgvMyOrders
        '
        Me.dgvMyOrders.AllowUserToAddRows = False
        Me.dgvMyOrders.AllowUserToDeleteRows = False
        Me.dgvMyOrders.Location = New System.Drawing.Point(30, 90)
        Me.dgvMyOrders.Name = "dgvMyOrders"
        Me.dgvMyOrders.ReadOnly = True
        Me.dgvMyOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMyOrders.Size = New System.Drawing.Size(1120, 500)
        Me.dgvMyOrders.TabIndex = 3
        '
        'ucMyOrders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.Controls.Add(Me.dgvMyOrders)
        Me.Controls.Add(Me.btnCancelOrder)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.lblTitle)
        Me.Name = "ucMyOrders"
        Me.Size = New System.Drawing.Size(1180, 610)
        CType(Me.dgvMyOrders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As ModernButton
    Friend WithEvents btnCancelOrder As ModernButton
    Friend WithEvents dgvMyOrders As System.Windows.Forms.DataGridView
End Class