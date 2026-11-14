<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucTraderOrders
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
        Me.btnConfirm = New ModernButton()
        Me.btnComplete = New ModernButton()
        Me.dgvOrders = New System.Windows.Forms.DataGridView()
        CType(Me.dgvOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.lblTitle.Location = New System.Drawing.Point(30, 30)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(232, 37)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Incoming Orders"
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(880, 30)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(100, 40)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "Refresh"
        '
        'btnConfirm
        '
        Me.btnConfirm.BackColor = System.Drawing.Color.FromArgb(27, 122, 80)
        Me.btnConfirm.ForeColor = System.Drawing.Color.White
        Me.btnConfirm.Location = New System.Drawing.Point(1000, 30)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(150, 40)
        Me.btnConfirm.TabIndex = 2
        Me.btnConfirm.Text = "Confirm Order"
        '
        'btnComplete
        '
        Me.btnComplete.BackColor = System.Drawing.Color.FromArgb(95, 179, 179)
        Me.btnComplete.ForeColor = System.Drawing.Color.White
        Me.btnComplete.Location = New System.Drawing.Point(1000, 80)
        Me.btnComplete.Name = "btnComplete"
        Me.btnComplete.Size = New System.Drawing.Size(150, 40)
        Me.btnComplete.TabIndex = 3
        Me.btnComplete.Text = "Mark Complete"

        Me.btnReject = New ModernButton()
        Me.btnReject.BackColor = System.Drawing.Color.FromArgb(179, 38, 30)
        Me.btnReject.ForeColor = System.Drawing.Color.White
        Me.btnReject.Location = New System.Drawing.Point(1000, 130)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(150, 40)
        Me.btnReject.TabIndex = 5
        Me.btnReject.Text = "Reject Order"
        Me.Controls.Add(Me.btnReject)
        '
        'dgvOrders
        '
        Me.dgvOrders.AllowUserToAddRows = False
        Me.dgvOrders.AllowUserToDeleteRows = False
        Me.dgvOrders.Location = New System.Drawing.Point(30, 90)
        Me.dgvOrders.Name = "dgvOrders"
        Me.dgvOrders.ReadOnly = True
        Me.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvOrders.Size = New System.Drawing.Size(920, 500)
        Me.dgvOrders.TabIndex = 4
        '
        'ucTraderOrders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.Controls.Add(Me.dgvOrders)
        Me.Controls.Add(Me.btnComplete)
        Me.Controls.Add(Me.btnConfirm)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.lblTitle)
        Me.Name = "ucTraderOrders"
        Me.Size = New System.Drawing.Size(1180, 610)
        CType(Me.dgvOrders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As ModernButton
    Friend WithEvents btnConfirm As ModernButton
    Friend WithEvents btnComplete As ModernButton
    Friend WithEvents dgvOrders As System.Windows.Forms.DataGridView
    Friend WithEvents btnReject As ModernButton
End Class