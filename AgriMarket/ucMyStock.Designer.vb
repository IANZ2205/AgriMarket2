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
        Me.panelAddScroll = New System.Windows.Forms.Panel()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.lblUnit = New System.Windows.Forms.Label()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.lblPrice = New System.Windows.Forms.Label()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.lblLowStock = New System.Windows.Forms.Label()
        Me.txtLowStock = New System.Windows.Forms.TextBox()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.lblShelf = New System.Windows.Forms.Label()
        Me.txtShelf = New System.Windows.Forms.TextBox()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.btnUploadPhoto = New ModernButton()
        Me.picPreview = New System.Windows.Forms.PictureBox()
        Me.btnUploadPhoto2 = New ModernButton()
        Me.picPreview2 = New System.Windows.Forms.PictureBox()
        Me.btnUploadPhoto3 = New ModernButton()
        Me.picPreview3 = New System.Windows.Forms.PictureBox()
        Me.lblLocalNames = New System.Windows.Forms.Label()
        Me.lblLang1 = New System.Windows.Forms.Label()
        Me.txtLang1 = New System.Windows.Forms.TextBox()
        Me.lblLang2 = New System.Windows.Forms.Label()
        Me.txtLang2 = New System.Windows.Forms.TextBox()
        Me.lblLang3 = New System.Windows.Forms.Label()
        Me.txtLang3 = New System.Windows.Forms.TextBox()
        Me.lblLang4 = New System.Windows.Forms.Label()
        Me.txtLang4 = New System.Windows.Forms.TextBox()
        Me.btnSave = New ModernButton()
        Me.btnCancel = New ModernButton()
        CType(Me.dgvStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelAdd.SuspendLayout()
        Me.panelAddScroll.SuspendLayout()
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
        Me.btnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.btnTopUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.btnShowAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.dgvStock.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvStock.Location = New System.Drawing.Point(30, 90)
        Me.dgvStock.Name = "dgvStock"
        Me.dgvStock.ReadOnly = True
        Me.dgvStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvStock.Size = New System.Drawing.Size(1120, 500)
        Me.dgvStock.TabIndex = 3
        '
        'panelAdd
        '
        Me.panelAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panelAdd.BackColor = System.Drawing.Color.White
        Me.panelAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelAdd.Controls.Add(Me.btnCancel)
        Me.panelAdd.Controls.Add(Me.btnSave)
        Me.panelAdd.Controls.Add(Me.panelAddScroll)
        Me.panelAdd.Controls.Add(Me.lblAddTitle)
        Me.panelAdd.Location = New System.Drawing.Point(660, 50)
        Me.panelAdd.Name = "panelAdd"
        Me.panelAdd.Size = New System.Drawing.Size(500, 560)
        Me.panelAdd.TabIndex = 4
        Me.panelAdd.Visible = False
        '
        'lblAddTitle
        '
        Me.lblAddTitle.AutoSize = True
        Me.lblAddTitle.Font = New System.Drawing.Font("Segoe UI", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lblAddTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.lblAddTitle.Location = New System.Drawing.Point(20, 15)
        Me.lblAddTitle.Name = "lblAddTitle"
        Me.lblAddTitle.Size = New System.Drawing.Size(174, 28)
        Me.lblAddTitle.TabIndex = 0
        Me.lblAddTitle.Text = "Add New Produce"
        '
        'panelAddScroll
        '
        Me.panelAddScroll.AutoScroll = True
        Me.panelAddScroll.BackColor = System.Drawing.Color.White
        Me.panelAddScroll.Controls.Add(Me.lblName)
        Me.panelAddScroll.Controls.Add(Me.txtName)
        Me.panelAddScroll.Controls.Add(Me.lblCategory)
        Me.panelAddScroll.Controls.Add(Me.cmbCategory)
        Me.panelAddScroll.Controls.Add(Me.lblUnit)
        Me.panelAddScroll.Controls.Add(Me.cmbUnit)
        Me.panelAddScroll.Controls.Add(Me.lblPrice)
        Me.panelAddScroll.Controls.Add(Me.txtPrice)
        Me.panelAddScroll.Controls.Add(Me.lblLowStock)
        Me.panelAddScroll.Controls.Add(Me.txtLowStock)
        Me.panelAddScroll.Controls.Add(Me.lblQty)
        Me.panelAddScroll.Controls.Add(Me.txtQty)
        Me.panelAddScroll.Controls.Add(Me.lblShelf)
        Me.panelAddScroll.Controls.Add(Me.txtShelf)
        Me.panelAddScroll.Controls.Add(Me.lblDescription)
        Me.panelAddScroll.Controls.Add(Me.txtDescription)
        Me.panelAddScroll.Controls.Add(Me.btnUploadPhoto)
        Me.panelAddScroll.Controls.Add(Me.picPreview)
        Me.panelAddScroll.Controls.Add(Me.btnUploadPhoto2)
        Me.panelAddScroll.Controls.Add(Me.picPreview2)
        Me.panelAddScroll.Controls.Add(Me.btnUploadPhoto3)
        Me.panelAddScroll.Controls.Add(Me.picPreview3)
        Me.panelAddScroll.Controls.Add(Me.lblLocalNames)
        Me.panelAddScroll.Controls.Add(Me.lblLang1)
        Me.panelAddScroll.Controls.Add(Me.txtLang1)
        Me.panelAddScroll.Controls.Add(Me.lblLang2)
        Me.panelAddScroll.Controls.Add(Me.txtLang2)
        Me.panelAddScroll.Controls.Add(Me.lblLang3)
        Me.panelAddScroll.Controls.Add(Me.txtLang3)
        Me.panelAddScroll.Controls.Add(Me.lblLang4)
        Me.panelAddScroll.Controls.Add(Me.txtLang4)
        Me.panelAddScroll.Location = New System.Drawing.Point(10, 55)
        Me.panelAddScroll.Name = "panelAddScroll"
        Me.panelAddScroll.Size = New System.Drawing.Size(478, 435)
        Me.panelAddScroll.TabIndex = 1
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblName.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblName.Location = New System.Drawing.Point(5, 10)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(96, 15)
        Me.lblName.TabIndex = 0
        Me.lblName.Text = "Produce Name *"
        '
        'txtName
        '
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txtName.Location = New System.Drawing.Point(5, 32)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(440, 27)
        Me.txtName.TabIndex = 1
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblCategory.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblCategory.Location = New System.Drawing.Point(5, 70)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(63, 15)
        Me.lblCategory.TabIndex = 2
        Me.lblCategory.Text = "Category *"
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.cmbCategory.Location = New System.Drawing.Point(5, 92)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(210, 28)
        Me.cmbCategory.TabIndex = 3
        '
        'lblUnit
        '
        Me.lblUnit.AutoSize = True
        Me.lblUnit.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblUnit.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblUnit.Location = New System.Drawing.Point(225, 70)
        Me.lblUnit.Name = "lblUnit"
        Me.lblUnit.Size = New System.Drawing.Size(83, 15)
        Me.lblUnit.TabIndex = 4
        Me.lblUnit.Text = "Unit of Measure *"
        '
        'cmbUnit
        '
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.cmbUnit.Location = New System.Drawing.Point(225, 92)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.Size = New System.Drawing.Size(220, 28)
        Me.cmbUnit.TabIndex = 5
        '
        'lblPrice
        '
        Me.lblPrice.AutoSize = True
        Me.lblPrice.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblPrice.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblPrice.Location = New System.Drawing.Point(5, 130)
        Me.lblPrice.Name = "lblPrice"
        Me.lblPrice.Size = New System.Drawing.Size(90, 15)
        Me.lblPrice.TabIndex = 6
        Me.lblPrice.Text = "Price per Unit *"
        '
        'txtPrice
        '
        Me.txtPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrice.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txtPrice.Location = New System.Drawing.Point(5, 152)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(210, 27)
        Me.txtPrice.TabIndex = 7
        '
        'lblLowStock
        '
        Me.lblLowStock.AutoSize = True
        Me.lblLowStock.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblLowStock.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblLowStock.Location = New System.Drawing.Point(225, 130)
        Me.lblLowStock.Name = "lblLowStock"
        Me.lblLowStock.Size = New System.Drawing.Size(140, 15)
        Me.lblLowStock.TabIndex = 8
        Me.lblLowStock.Text = "Low Stock Level (default 10)"
        '
        'txtLowStock
        '
        Me.txtLowStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLowStock.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txtLowStock.Location = New System.Drawing.Point(225, 152)
        Me.txtLowStock.Name = "txtLowStock"
        Me.txtLowStock.Size = New System.Drawing.Size(220, 27)
        Me.txtLowStock.TabIndex = 9
        Me.txtLowStock.Text = "10"
        '
        'lblQty
        '
        Me.lblQty.AutoSize = True
        Me.lblQty.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblQty.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblQty.Location = New System.Drawing.Point(5, 190)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(60, 15)
        Me.lblQty.TabIndex = 10
        Me.lblQty.Text = "Quantity *"
        '
        'txtQty
        '
        Me.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQty.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txtQty.Location = New System.Drawing.Point(5, 212)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(210, 27)
        Me.txtQty.TabIndex = 11
        '
        'lblShelf
        '
        Me.lblShelf.AutoSize = True
        Me.lblShelf.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblShelf.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblShelf.Location = New System.Drawing.Point(225, 190)
        Me.lblShelf.Name = "lblShelf"
        Me.lblShelf.Size = New System.Drawing.Size(105, 15)
        Me.lblShelf.TabIndex = 12
        Me.lblShelf.Text = "Shelf Life (Days) *"
        '
        'txtShelf
        '
        Me.txtShelf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShelf.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txtShelf.Location = New System.Drawing.Point(225, 212)
        Me.txtShelf.Name = "txtShelf"
        Me.txtShelf.Size = New System.Drawing.Size(220, 27)
        Me.txtShelf.TabIndex = 13
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblDescription.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblDescription.Location = New System.Drawing.Point(5, 250)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(74, 15)
        Me.lblDescription.TabIndex = 14
        Me.lblDescription.Text = "Description"
        '
        'txtDescription
        '
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtDescription.Location = New System.Drawing.Point(5, 272)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescription.Size = New System.Drawing.Size(440, 55)
        Me.txtDescription.TabIndex = 15
        '
        'btnUploadPhoto
        '
        Me.btnUploadPhoto.BackColor = System.Drawing.Color.FromArgb(95, 179, 179)
        Me.btnUploadPhoto.FlatAppearance.BorderSize = 0
        Me.btnUploadPhoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUploadPhoto.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnUploadPhoto.ForeColor = System.Drawing.Color.White
        Me.btnUploadPhoto.Location = New System.Drawing.Point(5, 340)
        Me.btnUploadPhoto.Name = "btnUploadPhoto"
        Me.btnUploadPhoto.Size = New System.Drawing.Size(155, 32)
        Me.btnUploadPhoto.TabIndex = 16
        Me.btnUploadPhoto.Text = "Main Photo *"
        Me.btnUploadPhoto.UseVisualStyleBackColor = False
        '
        'picPreview
        '
        Me.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPreview.Location = New System.Drawing.Point(165, 340)
        Me.picPreview.Name = "picPreview"
        Me.picPreview.Size = New System.Drawing.Size(40, 32)
        Me.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picPreview.TabStop = False
        '
        'btnUploadPhoto2
        '
        Me.btnUploadPhoto2.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.btnUploadPhoto2.FlatAppearance.BorderSize = 0
        Me.btnUploadPhoto2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUploadPhoto2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnUploadPhoto2.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnUploadPhoto2.Location = New System.Drawing.Point(5, 380)
        Me.btnUploadPhoto2.Name = "btnUploadPhoto2"
        Me.btnUploadPhoto2.Size = New System.Drawing.Size(155, 32)
        Me.btnUploadPhoto2.TabIndex = 17
        Me.btnUploadPhoto2.Text = "Photo 2 (optional)"
        Me.btnUploadPhoto2.UseVisualStyleBackColor = False
        '
        'picPreview2
        '
        Me.picPreview2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPreview2.Location = New System.Drawing.Point(165, 380)
        Me.picPreview2.Name = "picPreview2"
        Me.picPreview2.Size = New System.Drawing.Size(40, 32)
        Me.picPreview2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picPreview2.TabStop = False
        '
        'btnUploadPhoto3
        '
        Me.btnUploadPhoto3.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.btnUploadPhoto3.FlatAppearance.BorderSize = 0
        Me.btnUploadPhoto3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUploadPhoto3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnUploadPhoto3.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnUploadPhoto3.Location = New System.Drawing.Point(225, 380)
        Me.btnUploadPhoto3.Name = "btnUploadPhoto3"
        Me.btnUploadPhoto3.Size = New System.Drawing.Size(155, 32)
        Me.btnUploadPhoto3.TabIndex = 18
        Me.btnUploadPhoto3.Text = "Photo 3 (optional)"
        Me.btnUploadPhoto3.UseVisualStyleBackColor = False
        '
        'picPreview3
        '
        Me.picPreview3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPreview3.Location = New System.Drawing.Point(385, 380)
        Me.picPreview3.Name = "picPreview3"
        Me.picPreview3.Size = New System.Drawing.Size(40, 32)
        Me.picPreview3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picPreview3.TabStop = False
        '
        'lblLocalNames
        '
        Me.lblLocalNames.AutoSize = True
        Me.lblLocalNames.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblLocalNames.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.lblLocalNames.Location = New System.Drawing.Point(5, 425)
        Me.lblLocalNames.Name = "lblLocalNames"
        Me.lblLocalNames.Size = New System.Drawing.Size(200, 19)
        Me.lblLocalNames.TabIndex = 19
        Me.lblLocalNames.Text = "Local Names (optional)"
        '
        'lblLang1
        '
        Me.lblLang1.AutoSize = True
        Me.lblLang1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblLang1.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblLang1.Location = New System.Drawing.Point(5, 450)
        Me.lblLang1.Name = "lblLang1"
        Me.lblLang1.Size = New System.Drawing.Size(65, 15)
        Me.lblLang1.TabIndex = 20
        Me.lblLang1.Text = "Language 1"
        '
        'txtLang1
        '
        Me.txtLang1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLang1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtLang1.Location = New System.Drawing.Point(5, 470)
        Me.txtLang1.Name = "txtLang1"
        Me.txtLang1.Size = New System.Drawing.Size(440, 25)
        Me.txtLang1.TabIndex = 21
        '
        'lblLang2
        '
        Me.lblLang2.AutoSize = True
        Me.lblLang2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblLang2.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblLang2.Location = New System.Drawing.Point(5, 500)
        Me.lblLang2.Name = "lblLang2"
        Me.lblLang2.Size = New System.Drawing.Size(65, 15)
        Me.lblLang2.TabIndex = 22
        Me.lblLang2.Text = "Language 2"
        '
        'txtLang2
        '
        Me.txtLang2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLang2.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtLang2.Location = New System.Drawing.Point(5, 520)
        Me.txtLang2.Name = "txtLang2"
        Me.txtLang2.Size = New System.Drawing.Size(440, 25)
        Me.txtLang2.TabIndex = 23
        '
        'lblLang3
        '
        Me.lblLang3.AutoSize = True
        Me.lblLang3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblLang3.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblLang3.Location = New System.Drawing.Point(5, 550)
        Me.lblLang3.Name = "lblLang3"
        Me.lblLang3.Size = New System.Drawing.Size(65, 15)
        Me.lblLang3.TabIndex = 24
        Me.lblLang3.Text = "Language 3"
        '
        'txtLang3
        '
        Me.txtLang3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLang3.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtLang3.Location = New System.Drawing.Point(5, 570)
        Me.txtLang3.Name = "txtLang3"
        Me.txtLang3.Size = New System.Drawing.Size(440, 25)
        Me.txtLang3.TabIndex = 25
        '
        'lblLang4
        '
        Me.lblLang4.AutoSize = True
        Me.lblLang4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblLang4.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        Me.lblLang4.Location = New System.Drawing.Point(5, 600)
        Me.lblLang4.Name = "lblLang4"
        Me.lblLang4.Size = New System.Drawing.Size(65, 15)
        Me.lblLang4.TabIndex = 26
        Me.lblLang4.Text = "Language 4"
        '
        'txtLang4
        '
        Me.txtLang4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLang4.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtLang4.Location = New System.Drawing.Point(5, 620)
        Me.txtLang4.Name = "txtLang4"
        Me.txtLang4.Size = New System.Drawing.Size(440, 25)
        Me.txtLang4.TabIndex = 27
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(20, 500)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(220, 42)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save Produce"
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.btnCancel.Location = New System.Drawing.Point(250, 500)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(220, 42)
        Me.btnCancel.TabIndex = 3
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
        Me.panelAddScroll.ResumeLayout(False)
        Me.panelAddScroll.PerformLayout()
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
    Friend WithEvents panelAddScroll As System.Windows.Forms.Panel
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents lblUnit As System.Windows.Forms.Label
    Friend WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Friend WithEvents lblPrice As System.Windows.Forms.Label
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents lblLowStock As System.Windows.Forms.Label
    Friend WithEvents txtLowStock As System.Windows.Forms.TextBox
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents lblShelf As System.Windows.Forms.Label
    Friend WithEvents txtShelf As System.Windows.Forms.TextBox
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents btnUploadPhoto As ModernButton
    Friend WithEvents picPreview As System.Windows.Forms.PictureBox
    Friend WithEvents btnUploadPhoto2 As ModernButton
    Friend WithEvents picPreview2 As System.Windows.Forms.PictureBox
    Friend WithEvents btnUploadPhoto3 As ModernButton
    Friend WithEvents picPreview3 As System.Windows.Forms.PictureBox
    Friend WithEvents lblLocalNames As System.Windows.Forms.Label
    Friend WithEvents lblLang1 As System.Windows.Forms.Label
    Friend WithEvents txtLang1 As System.Windows.Forms.TextBox
    Friend WithEvents lblLang2 As System.Windows.Forms.Label
    Friend WithEvents txtLang2 As System.Windows.Forms.TextBox
    Friend WithEvents lblLang3 As System.Windows.Forms.Label
    Friend WithEvents txtLang3 As System.Windows.Forms.TextBox
    Friend WithEvents lblLang4 As System.Windows.Forms.Label
    Friend WithEvents txtLang4 As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As ModernButton
    Friend WithEvents btnCancel As ModernButton
End Class