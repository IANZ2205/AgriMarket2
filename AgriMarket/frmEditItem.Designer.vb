<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEditItem
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

        ' Item Details Section
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblPrice = New System.Windows.Forms.Label()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.lblDesc = New System.Windows.Forms.Label()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.btnSaveItem = New ModernButton()

        ' Batch Adjustment Section
        Me.lblBatchTitle = New System.Windows.Forms.Label()
        Me.lblBatchNo = New System.Windows.Forms.Label()
        Me.lblNewQty = New System.Windows.Forms.Label()
        Me.txtNewQty = New System.Windows.Forms.TextBox()
        Me.lblReason = New System.Windows.Forms.Label()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.btnAdjustBatch = New ModernButton()

        Me.SuspendLayout()
        '
        'Form Setup
        '
        Me.ClientSize = New System.Drawing.Size(800, 550)
        Me.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Edit Item"
        '
        'Title
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.lblTitle.Location = New System.Drawing.Point(30, 30)
        Me.lblTitle.Text = "Edit Item Details"
        '
        'Name
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblName.Location = New System.Drawing.Point(30, 90)
        Me.lblName.Text = "Produce Name"
        Me.txtName.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtName.Location = New System.Drawing.Point(30, 115)
        Me.txtName.Size = New System.Drawing.Size(350, 29)
        '
        'Price
        '
        Me.lblPrice.AutoSize = True
        Me.lblPrice.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblPrice.Location = New System.Drawing.Point(30, 160)
        Me.lblPrice.Text = "Price per Unit (UGX)"
        Me.txtPrice.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtPrice.Location = New System.Drawing.Point(30, 185)
        Me.txtPrice.Size = New System.Drawing.Size(350, 29)
        '
        'Description
        '
        Me.lblDesc.AutoSize = True
        Me.lblDesc.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblDesc.Location = New System.Drawing.Point(30, 230)
        Me.lblDesc.Text = "Description"
        Me.txtDesc.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtDesc.Location = New System.Drawing.Point(30, 255)
        Me.txtDesc.Size = New System.Drawing.Size(350, 60)
        Me.txtDesc.Multiline = True
        '
        'Save Item Button
        '
        Me.btnSaveItem.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnSaveItem.ForeColor = System.Drawing.Color.White
        Me.btnSaveItem.Location = New System.Drawing.Point(30, 340)
        Me.btnSaveItem.Size = New System.Drawing.Size(350, 45)
        Me.btnSaveItem.Text = "Save Item Details"
        '
        'Batch Adjustment Section
        '
        Me.lblBatchTitle.AutoSize = True
        Me.lblBatchTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblBatchTitle.Location = New System.Drawing.Point(430, 90)
        Me.lblBatchTitle.Text = "Adjust Batch Quantity"
        '
        'Batch Info
        '
        Me.lblBatchNo.AutoSize = True
        Me.lblBatchNo.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblBatchNo.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.lblBatchNo.Location = New System.Drawing.Point(430, 130)
        Me.lblBatchNo.Text = "Batch: "
        '
        'New Qty
        '
        Me.lblNewQty.AutoSize = True
        Me.lblNewQty.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblNewQty.Location = New System.Drawing.Point(430, 170)
        Me.lblNewQty.Text = "New Quantity"
        Me.txtNewQty.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtNewQty.Location = New System.Drawing.Point(430, 195)
        Me.txtNewQty.Size = New System.Drawing.Size(320, 29)
        '
        'Reason
        '
        Me.lblReason.AutoSize = True
        Me.lblReason.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblReason.Location = New System.Drawing.Point(430, 240)
        Me.lblReason.Text = "Reason for Adjustment (Required)"
        Me.txtReason.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtReason.Location = New System.Drawing.Point(430, 265)
        Me.txtReason.Size = New System.Drawing.Size(320, 60)
        Me.txtReason.Multiline = True
        '
        'Adjust Batch Button
        '
        Me.btnAdjustBatch.BackColor = System.Drawing.Color.FromArgb(95, 179, 179)
        Me.btnAdjustBatch.ForeColor = System.Drawing.Color.White
        Me.btnAdjustBatch.Location = New System.Drawing.Point(430, 340)
        Me.btnAdjustBatch.Size = New System.Drawing.Size(320, 45)
        Me.btnAdjustBatch.Text = "Save Batch Adjustment"
        '
        'Add Controls
        '
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.lblPrice)
        Me.Controls.Add(Me.txtPrice)
        Me.Controls.Add(Me.lblDesc)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.btnSaveItem)

        Me.Controls.Add(Me.lblBatchTitle)
        Me.Controls.Add(Me.lblBatchNo)
        Me.Controls.Add(Me.lblNewQty)
        Me.Controls.Add(Me.txtNewQty)
        Me.Controls.Add(Me.lblReason)
        Me.Controls.Add(Me.txtReason)
        Me.Controls.Add(Me.btnAdjustBatch)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblPrice As System.Windows.Forms.Label
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents lblDesc As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveItem As ModernButton
    Friend WithEvents lblBatchTitle As System.Windows.Forms.Label
    Friend WithEvents lblBatchNo As System.Windows.Forms.Label
    Friend WithEvents lblNewQty As System.Windows.Forms.Label
    Friend WithEvents txtNewQty As System.Windows.Forms.TextBox
    Friend WithEvents lblReason As System.Windows.Forms.Label
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents btnAdjustBatch As ModernButton
End Class