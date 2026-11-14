<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucMyStock
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
        Me.btnTopUp = New ModernButton()
        Me.btnEdit = New ModernButton()
        Me.btnShowAdd = New ModernButton()
        Me.dgvStock = New System.Windows.Forms.DataGridView()
        Me.panelAdd = New System.Windows.Forms.Panel()
        Me.lblAddTitle = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.lblPrice = New System.Windows.Forms.Label()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.lblShelf = New System.Windows.Forms.Label()
        Me.txtShelf = New System.Windows.Forms.TextBox()

        ' Photo Controls
        Me.btnUploadPhoto = New ModernButton()
        Me.picPreview = New System.Windows.Forms.PictureBox()
        Me.btnUploadPhoto2 = New ModernButton()
        Me.picPreview2 = New System.Windows.Forms.PictureBox()
        Me.btnUploadPhoto3 = New ModernButton()
        Me.picPreview3 = New System.Windows.Forms.PictureBox()

        Me.btnSave = New ModernButton()
        Me.btnCancel = New ModernButton()

        CType(Me.dgvStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelAdd.SuspendLayout()
        CType(Me.picPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPreview2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPreview3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.lblTitle.Location = New System.Drawing.Point(30, 30)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(135, 37)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "My Stock"
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.btnEdit.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnEdit.Location = New System.Drawing.Point(640, 30)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(110, 40)
        Me.btnEdit.TabIndex = 14
        Me.btnEdit.Text = "Edit / Adjust"
        '
        'btnTopUp
        '
        Me.btnTopUp.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnTopUp.FlatAppearance.BorderSize = 0
        Me.btnTopUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTopUp.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnTopUp.ForeColor = System.Drawing.Color.White
        Me.btnTopUp.Location = New System.Drawing.Point(760, 30)
        Me.btnTopUp.Name = "btnTopUp"
        Me.btnTopUp.Size = New System.Drawing.Size(110, 40)
        Me.btnTopUp.TabIndex = 13
        Me.btnTopUp.Text = "Top Up Stock"
        Me.btnTopUp.UseVisualStyleBackColor = False
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
        'btnShowAdd
        '
        Me.btnShowAdd.BackColor = System.Drawing.Color.FromArgb(95, 179, 179)
        Me.btnShowAdd.ForeColor = System.Drawing.Color.White
        Me.btnShowAdd.Location = New System.Drawing.Point(1000, 30)
        Me.btnShowAdd.Name = "btnShowAdd"
        Me.btnShowAdd.Size = New System.Drawing.Size(150, 40)
        Me.btnShowAdd.TabIndex = 2
        Me.btnShowAdd.Text = "+ Add Produce"
        '
        'dgvStock
        '
        Me.dgvStock.AllowUserToAddRows = False
        Me.dgvStock.AllowUserToDeleteRows = False
        Me.dgvStock.Location = New System.Drawing.Point(30, 90)
        Me.dgvStock.Name = "dgvStock"
        Me.dgvStock.ReadOnly = True
        Me.dgvStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvStock.Size = New System.Drawing.Size(1120, 500)
        Me.dgvStock.TabIndex = 3
        '
        'panelAdd
        '
        Me.panelAdd.BackColor = System.Drawing.Color.White
        Me.panelAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelAdd.Controls.Add(Me.lblAddTitle)
        Me.panelAdd.Controls.Add(Me.lblName)
        Me.panelAdd.Controls.Add(Me.txtName)
        Me.panelAdd.Controls.Add(Me.lblCategory)
        Me.panelAdd.Controls.Add(Me.cmbCategory)
        Me.panelAdd.Controls.Add(Me.lblPrice)
        Me.panelAdd.Controls.Add(Me.txtPrice)
        Me.panelAdd.Controls.Add(Me.lblQty)
        Me.panelAdd.Controls.Add(Me.txtQty)
        Me.panelAdd.Controls.Add(Me.lblShelf)
        Me.panelAdd.Controls.Add(Me.txtShelf)
        Me.panelAdd.Controls.Add(Me.btnUploadPhoto)
        Me.panelAdd.Controls.Add(Me.picPreview)
        Me.panelAdd.Controls.Add(Me.btnUploadPhoto2)
        Me.panelAdd.Controls.Add(Me.picPreview2)
        Me.panelAdd.Controls.Add(Me.btnUploadPhoto3)
        Me.panelAdd.Controls.Add(Me.picPreview3)
        Me.panelAdd.Controls.Add(Me.btnSave)
        Me.panelAdd.Controls.Add(Me.btnCancel)
        Me.panelAdd.Location = New System.Drawing.Point(700, 90)
        Me.panelAdd.Name = "panelAdd"
        Me.panelAdd.Size = New System.Drawing.Size(450, 650) ' Made taller for 3 photos
        Me.panelAdd.TabIndex = 4
        Me.panelAdd.Visible = False
        '
        'lblAddTitle
        '
        Me.lblAddTitle.AutoSize = True
        Me.lblAddTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblAddTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.lblAddTitle.Location = New System.Drawing.Point(20, 20)
        Me.lblAddTitle.Name = "lblAddTitle"
        Me.lblAddTitle.Size = New System.Drawing.Size(187, 30)
        Me.lblAddTitle.TabIndex = 0
        Me.lblAddTitle.Text = "Add New Produce"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblName.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblName.Location = New System.Drawing.Point(20, 70)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(105, 19)
        Me.lblName.TabIndex = 1
        Me.lblName.Text = "Produce Name"
        '
        'txtName
        '
        Me.txtName.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtName.Location = New System.Drawing.Point(20, 95)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(400, 29)
        Me.txtName.TabIndex = 2
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblCategory.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblCategory.Location = New System.Drawing.Point(20, 140)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(66, 19)
        Me.lblCategory.TabIndex = 3
        Me.lblCategory.Text = "Category"
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.cmbCategory.Location = New System.Drawing.Point(20, 165)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(400, 29)
        Me.cmbCategory.TabIndex = 4
        '
        'lblPrice
        '
        Me.lblPrice.AutoSize = True
        Me.lblPrice.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblPrice.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblPrice.Location = New System.Drawing.Point(20, 210)
        Me.lblPrice.Name = "lblPrice"
        Me.lblPrice.Size = New System.Drawing.Size(88, 19)
        Me.lblPrice.TabIndex = 5
        Me.lblPrice.Text = "Price per kg"
        '
        'txtPrice
        '
        Me.txtPrice.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtPrice.Location = New System.Drawing.Point(20, 235)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(400, 29)
        Me.txtPrice.TabIndex = 6
        '
        'lblQty
        '
        Me.lblQty.AutoSize = True
        Me.lblQty.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblQty.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblQty.Location = New System.Drawing.Point(20, 280)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(82, 19)
        Me.lblQty.TabIndex = 7
        Me.lblQty.Text = "Quantity (kg)"
        '
        'txtQty
        '
        Me.txtQty.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtQty.Location = New System.Drawing.Point(20, 305)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(400, 29)
        Me.txtQty.TabIndex = 8
        '
        'lblShelf
        '
        Me.lblShelf.AutoSize = True
        Me.lblShelf.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblShelf.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblShelf.Location = New System.Drawing.Point(20, 350)
        Me.lblShelf.Name = "lblShelf"
        Me.lblShelf.Size = New System.Drawing.Size(110, 19)
        Me.lblShelf.TabIndex = 9
        Me.lblShelf.Text = "Shelf Life (Days)"
        '
        'txtShelf
        '
        Me.txtShelf.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtShelf.Location = New System.Drawing.Point(20, 375)
        Me.txtShelf.Name = "txtShelf"
        Me.txtShelf.Size = New System.Drawing.Size(400, 29)
        Me.txtShelf.TabIndex = 10
        '
        'Photo 1
        '
        Me.btnUploadPhoto.BackColor = System.Drawing.Color.FromArgb(95, 179, 179)
        Me.btnUploadPhoto.ForeColor = System.Drawing.Color.White
        Me.btnUploadPhoto.Location = New System.Drawing.Point(20, 425)
        Me.btnUploadPhoto.Name = "btnUploadPhoto"
        Me.btnUploadPhoto.Size = New System.Drawing.Size(130, 30)
        Me.btnUploadPhoto.TabIndex = 11
        Me.btnUploadPhoto.Text = "Main Photo *"

        Me.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPreview.Location = New System.Drawing.Point(160, 425)
        Me.picPreview.Name = "picPreview"
        Me.picPreview.Size = New System.Drawing.Size(40, 30)
        Me.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picPreview.TabIndex = 12
        Me.picPreview.TabStop = False
        '
        'Photo 2
        '
        Me.btnUploadPhoto2.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.btnUploadPhoto2.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnUploadPhoto2.Location = New System.Drawing.Point(210, 425)
        Me.btnUploadPhoto2.Name = "btnUploadPhoto2"
        Me.btnUploadPhoto2.Size = New System.Drawing.Size(130, 30)
        Me.btnUploadPhoto2.TabIndex = 13
        Me.btnUploadPhoto2.Text = "Photo 2"

        Me.picPreview2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPreview2.Location = New System.Drawing.Point(350, 425)
        Me.picPreview2.Name = "picPreview2"
        Me.picPreview2.Size = New System.Drawing.Size(40, 30)
        Me.picPreview2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picPreview2.TabIndex = 14
        Me.picPreview2.TabStop = False
        '
        'Photo 3
        '
        Me.btnUploadPhoto3.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.btnUploadPhoto3.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnUploadPhoto3.Location = New System.Drawing.Point(20, 465)
        Me.btnUploadPhoto3.Name = "btnUploadPhoto3"
        Me.btnUploadPhoto3.Size = New System.Drawing.Size(130, 30)
        Me.btnUploadPhoto3.TabIndex = 15
        Me.btnUploadPhoto3.Text = "Photo 3"

        Me.picPreview3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPreview3.Location = New System.Drawing.Point(160, 465)
        Me.picPreview3.Name = "picPreview3"
        Me.picPreview3.Size = New System.Drawing.Size(40, 30)
        Me.picPreview3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picPreview3.TabIndex = 16
        Me.picPreview3.TabStop = False
        '
        'Save / Cancel
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(20, 520)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(190, 45)
        Me.btnSave.TabIndex = 17
        Me.btnSave.Text = "Save Produce"
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.btnCancel.Location = New System.Drawing.Point(230, 520)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(190, 45)
        Me.btnCancel.TabIndex = 18
        Me.btnCancel.Text = "Cancel"
        '
        'ucMyStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.Controls.Add(Me.panelAdd)
        Me.Controls.Add(Me.dgvStock)
        Me.Controls.Add(Me.btnShowAdd)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnTopUp)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.lblTitle)
        Me.Name = "ucMyStock"
        Me.Size = New System.Drawing.Size(1180, 610)
        CType(Me.dgvStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelAdd.ResumeLayout(False)
        Me.panelAdd.PerformLayout()
        CType(Me.picPreview, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPreview2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPreview3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As ModernButton
    Friend WithEvents btnTopUp As ModernButton
    Friend WithEvents btnEdit As ModernButton
    Friend WithEvents btnShowAdd As ModernButton
    Friend WithEvents dgvStock As System.Windows.Forms.DataGridView
    Friend WithEvents panelAdd As System.Windows.Forms.Panel
    Friend WithEvents lblAddTitle As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents lblPrice As System.Windows.Forms.Label
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents lblShelf As System.Windows.Forms.Label
    Friend WithEvents txtShelf As System.Windows.Forms.TextBox
    Friend WithEvents btnUploadPhoto As ModernButton
    Friend WithEvents picPreview As System.Windows.Forms.PictureBox
    Friend WithEvents btnUploadPhoto2 As ModernButton
    Friend WithEvents picPreview2 As System.Windows.Forms.PictureBox
    Friend WithEvents btnUploadPhoto3 As ModernButton
    Friend WithEvents picPreview3 As System.Windows.Forms.PictureBox
    Friend WithEvents btnSave As ModernButton
    Friend WithEvents btnCancel As ModernButton
End Class