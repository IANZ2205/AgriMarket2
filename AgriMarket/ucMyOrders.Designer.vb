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
        Me.btnReorder = New ModernButton()
        Me.btnCancelOrder = New ModernButton()
        Me.btnTabAll = New ModernButton()
        Me.btnTabPending = New ModernButton()
        Me.btnTabOngoing = New ModernButton()
        Me.btnTabCompleted = New ModernButton()
        Me.btnTabClosed = New ModernButton()
        Me.dgvMyOrders = New System.Windows.Forms.DataGridView()
        Me.lblEmpty = New System.Windows.Forms.Label()
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
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(1050, 30)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(100, 40)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "Refresh"
        '
        'btnReorder
        '
        Me.btnReorder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReorder.BackColor = System.Drawing.Color.FromArgb(95, 179, 179)
        Me.btnReorder.ForeColor = System.Drawing.Color.White
        Me.btnReorder.Location = New System.Drawing.Point(920, 30)
        Me.btnReorder.Name = "btnReorder"
        Me.btnReorder.Size = New System.Drawing.Size(120, 40)
        Me.btnReorder.TabIndex = 2
        Me.btnReorder.Text = "Reorder"
        '
        'btnCancelOrder
        '
        Me.btnCancelOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancelOrder.BackColor = System.Drawing.Color.FromArgb(179, 38, 30)
        Me.btnCancelOrder.ForeColor = System.Drawing.Color.White
        Me.btnCancelOrder.Location = New System.Drawing.Point(780, 30)
        Me.btnCancelOrder.Name = "btnCancelOrder"
        Me.btnCancelOrder.Size = New System.Drawing.Size(130, 40)
        Me.btnCancelOrder.TabIndex = 3
        Me.btnCancelOrder.Text = "Cancel Order"
        '
        'Tab Buttons
        '
        Me.btnTabAll.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnTabAll.ForeColor = System.Drawing.Color.White
        Me.btnTabAll.Location = New System.Drawing.Point(30, 90)
        Me.btnTabAll.Name = "btnTabAll"
        Me.btnTabAll.Size = New System.Drawing.Size(100, 35)
        Me.btnTabAll.TabIndex = 4
        Me.btnTabAll.Text = "All"
        '
        Me.btnTabPending.BackColor = System.Drawing.Color.White
        Me.btnTabPending.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnTabPending.Location = New System.Drawing.Point(140, 90)
        Me.btnTabPending.Name = "btnTabPending"
        Me.btnTabPending.Size = New System.Drawing.Size(100, 35)
        Me.btnTabPending.TabIndex = 5
        Me.btnTabPending.Text = "Pending"
        '
        Me.btnTabOngoing.BackColor = System.Drawing.Color.White
        Me.btnTabOngoing.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnTabOngoing.Location = New System.Drawing.Point(250, 90)
        Me.btnTabOngoing.Name = "btnTabOngoing"
        Me.btnTabOngoing.Size = New System.Drawing.Size(100, 35)
        Me.btnTabOngoing.TabIndex = 6
        Me.btnTabOngoing.Text = "Ongoing"
        '
        Me.btnTabCompleted.BackColor = System.Drawing.Color.White
        Me.btnTabCompleted.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnTabCompleted.Location = New System.Drawing.Point(360, 90)
        Me.btnTabCompleted.Name = "btnTabCompleted"
        Me.btnTabCompleted.Size = New System.Drawing.Size(110, 35)
        Me.btnTabCompleted.TabIndex = 7
        Me.btnTabCompleted.Text = "Completed"
        '
        Me.btnTabClosed.BackColor = System.Drawing.Color.White
        Me.btnTabClosed.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnTabClosed.Location = New System.Drawing.Point(480, 90)
        Me.btnTabClosed.Name = "btnTabClosed"
        Me.btnTabClosed.Size = New System.Drawing.Size(100, 35)
        Me.btnTabClosed.TabIndex = 8
        Me.btnTabClosed.Text = "Closed"
        '
        'dgvMyOrders
        '
        Me.dgvMyOrders.AllowUserToAddRows = False
        Me.dgvMyOrders.AllowUserToDeleteRows = False
        Me.dgvMyOrders.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMyOrders.Location = New System.Drawing.Point(30, 145)
        Me.dgvMyOrders.Name = "dgvMyOrders"
        Me.dgvMyOrders.ReadOnly = True
        Me.dgvMyOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMyOrders.Size = New System.Drawing.Size(1120, 445)
        Me.dgvMyOrders.TabIndex = 9
        '
        'lblEmpty
        '
        Me.lblEmpty.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblEmpty.Font = New System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Italic)
        Me.lblEmpty.ForeColor = System.Drawing.Color.FromArgb(150, 155, 160)
        Me.lblEmpty.BackColor = System.Drawing.Color.White
        Me.lblEmpty.Location = New System.Drawing.Point(30, 145)
        Me.lblEmpty.Name = "lblEmpty"
        Me.lblEmpty.Size = New System.Drawing.Size(1120, 445)
        Me.lblEmpty.TabIndex = 10
        Me.lblEmpty.Text = "No orders in this category yet."
        Me.lblEmpty.Visible = False
        '
        'ucMyOrders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.Controls.Add(Me.lblEmpty)
        Me.Controls.Add(Me.dgvMyOrders)
        Me.Controls.Add(Me.btnTabClosed)
        Me.Controls.Add(Me.btnTabCompleted)
        Me.Controls.Add(Me.btnTabOngoing)
        Me.Controls.Add(Me.btnTabPending)
        Me.Controls.Add(Me.btnTabAll)
        Me.Controls.Add(Me.btnCancelOrder)
        Me.Controls.Add(Me.btnReorder)
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
    Friend WithEvents btnReorder As ModernButton
    Friend WithEvents btnCancelOrder As ModernButton
    Friend WithEvents btnTabAll As ModernButton
    Friend WithEvents btnTabPending As ModernButton
    Friend WithEvents btnTabOngoing As ModernButton
    Friend WithEvents btnTabCompleted As ModernButton
    Friend WithEvents btnTabClosed As ModernButton
    Friend WithEvents dgvMyOrders As System.Windows.Forms.DataGridView
    Friend WithEvents lblEmpty As System.Windows.Forms.Label
End Class