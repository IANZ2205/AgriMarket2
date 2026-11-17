Imports System.Data
Imports System.Drawing

Public Class ucAdminMasterLists
    Private lblTitle As System.Windows.Forms.Label
    Private lblSubtitle As System.Windows.Forms.Label
    Private cmbListType As System.Windows.Forms.ComboBox
    Private lblListTypeCaption As System.Windows.Forms.Label
    Private txtNewItem As System.Windows.Forms.TextBox
    Private btnAdd As System.Windows.Forms.Button
    Private btnDelete As System.Windows.Forms.Button
    Private btnRefresh As System.Windows.Forms.Button
    Private dgvItems As System.Windows.Forms.DataGridView
    Private lblCounter As System.Windows.Forms.Label

    Private Sub ucAdminMasterLists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildUI()
        LoadItems()
    End Sub

    Private Sub BuildUI()
        lblTitle = New System.Windows.Forms.Label()
        lblTitle.AutoSize = True
        lblTitle.Font = New System.Drawing.Font("Segoe UI", 24, System.Drawing.FontStyle.Bold)
        lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        lblTitle.Location = New System.Drawing.Point(30, 25)
        lblTitle.Text = "Master Lists"
        Me.Controls.Add(lblTitle)

        lblSubtitle = New System.Windows.Forms.Label()
        lblSubtitle.AutoSize = True
        lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 11)
        lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblSubtitle.Location = New System.Drawing.Point(35, 72)
        lblSubtitle.Text = "Manage the dropdown data used across the application"
        Me.Controls.Add(lblSubtitle)

        lblListTypeCaption = New System.Windows.Forms.Label()
        lblListTypeCaption.AutoSize = True
        lblListTypeCaption.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        lblListTypeCaption.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblListTypeCaption.Location = New System.Drawing.Point(30, 115)
        lblListTypeCaption.Text = "List Type:"
        Me.Controls.Add(lblListTypeCaption)

        cmbListType = New System.Windows.Forms.ComboBox()
        cmbListType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        cmbListType.Font = New System.Drawing.Font("Segoe UI", 11)
        cmbListType.Location = New System.Drawing.Point(110, 110)
        cmbListType.Size = New System.Drawing.Size(220, 27)
        cmbListType.Items.AddRange(New Object() {"Categories", "Districts"})
        cmbListType.SelectedIndex = 0
        AddHandler cmbListType.SelectedIndexChanged, AddressOf cmbListType_SelectedIndexChanged
        Me.Controls.Add(cmbListType)

        txtNewItem = New System.Windows.Forms.TextBox()
        txtNewItem.Font = New System.Drawing.Font("Segoe UI", 11)
        txtNewItem.Location = New System.Drawing.Point(30, 165)
        txtNewItem.Size = New System.Drawing.Size(400, 27)
        txtNewItem.Text = ""
        Me.Controls.Add(txtNewItem)

        btnAdd = New System.Windows.Forms.Button()
        btnAdd.BackColor = System.Drawing.Color.FromArgb(27, 122, 80)
        btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnAdd.FlatAppearance.BorderSize = 0
        btnAdd.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnAdd.ForeColor = System.Drawing.Color.White
        btnAdd.Location = New System.Drawing.Point(450, 161)
        btnAdd.Size = New System.Drawing.Size(120, 32)
        btnAdd.Text = "+ Add Item"
        btnAdd.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnAdd.Click, AddressOf btnAdd_Click
        Me.Controls.Add(btnAdd)

        btnDelete = New System.Windows.Forms.Button()
        btnDelete.BackColor = System.Drawing.Color.FromArgb(179, 38, 30)
        btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnDelete.FlatAppearance.BorderSize = 0
        btnDelete.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnDelete.ForeColor = System.Drawing.Color.White
        btnDelete.Location = New System.Drawing.Point(580, 161)
        btnDelete.Size = New System.Drawing.Size(160, 32)
        btnDelete.Text = "Delete Selected"
        btnDelete.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnDelete.Click, AddressOf btnDelete_Click
        Me.Controls.Add(btnDelete)

        btnRefresh = New System.Windows.Forms.Button()
        btnRefresh.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnRefresh.FlatAppearance.BorderSize = 0
        btnRefresh.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        btnRefresh.ForeColor = System.Drawing.Color.White
        btnRefresh.Location = New System.Drawing.Point(750, 161)
        btnRefresh.Size = New System.Drawing.Size(110, 32)
        btnRefresh.Text = "Refresh"
        btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnRefresh.Click, AddressOf btnRefresh_Click
        Me.Controls.Add(btnRefresh)

        lblCounter = New System.Windows.Forms.Label()
        lblCounter.AutoSize = True
        lblCounter.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Italic)
        lblCounter.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblCounter.Location = New System.Drawing.Point(880, 168)
        lblCounter.Text = "0 items"
        Me.Controls.Add(lblCounter)

        dgvItems = New System.Windows.Forms.DataGridView()
        dgvItems.BackgroundColor = System.Drawing.Color.White
        dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        dgvItems.Location = New System.Drawing.Point(30, 215)
        dgvItems.Size = New System.Drawing.Size(1120, 420)
        dgvItems.AllowUserToAddRows = False
        dgvItems.ReadOnly = True
        dgvItems.RowHeadersVisible = False
        dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        dgvItems.MultiSelect = False
        dgvItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        dgvItems.GridColor = System.Drawing.Color.FromArgb(230, 230, 230)
        dgvItems.EnableHeadersVisualStyles = False
        dgvItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        dgvItems.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        dgvItems.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        dgvItems.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
        dgvItems.ColumnHeadersHeight = 36
        dgvItems.RowTemplate.Height = 38
        dgvItems.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10)
        dgvItems.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        dgvItems.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        dgvItems.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.Controls.Add(dgvItems)
    End Sub

    Private Sub LoadItems()
        Try
            Dim listType As String = "Categories"
            If cmbListType.SelectedItem IsNot Nothing Then listType = cmbListType.SelectedItem.ToString()

            Dim dt As System.Data.DataTable = DatabaseHelper.GetMasterList(listType)
            dgvItems.DataSource = dt

            If dgvItems.Columns.Contains("ID") Then dgvItems.Columns("ID").Visible = False
            If dgvItems.Columns.Contains("Name") Then dgvItems.Columns("Name").AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill

            lblCounter.Text = dt.Rows.Count.ToString() & " item(s) in " & listType
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Error loading list: " & ex.Message)
        End Try
    End Sub

    Private Sub cmbListType_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadItems()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs)
        Dim name As String = txtNewItem.Text.Trim()
        If String.IsNullOrEmpty(name) Then
            System.Windows.Forms.MessageBox.Show("Please enter a name.", "Validation", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim listType As String = "Categories"
        If cmbListType.SelectedItem IsNot Nothing Then listType = cmbListType.SelectedItem.ToString()

        If DatabaseHelper.AddMasterListItem(listType, name) Then
            System.Windows.Forms.MessageBox.Show("Item added successfully!", "Success", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
            txtNewItem.Clear()
            LoadItems()
        Else
            System.Windows.Forms.MessageBox.Show("Failed to add. The item may already exist.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs)
        If dgvItems.SelectedRows.Count = 0 Then
            System.Windows.Forms.MessageBox.Show("Please select an item first.", "Validation", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim id As Integer = Convert.ToInt32(dgvItems.SelectedRows(0).Cells("ID").Value)
        Dim name As String = dgvItems.SelectedRows(0).Cells("Name").Value.ToString()

        Dim confirm As System.Windows.Forms.DialogResult = System.Windows.Forms.MessageBox.Show(
            "Are you sure you want to delete '" & name & "'?",
            "Confirm Delete",
            System.Windows.Forms.MessageBoxButtons.YesNo,
            System.Windows.Forms.MessageBoxIcon.Warning)

        If confirm = System.Windows.Forms.DialogResult.Yes Then
            Dim listType As String = "Categories"
            If cmbListType.SelectedItem IsNot Nothing Then listType = cmbListType.SelectedItem.ToString()

            If DatabaseHelper.DeleteMasterListItem(listType, id) Then
                LoadItems()
            Else
                System.Windows.Forms.MessageBox.Show("Could not delete. This item may be in use.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        LoadItems()
    End Sub
End Class