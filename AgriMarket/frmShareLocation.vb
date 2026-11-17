Imports System.ComponentModel

Public Class frmShareLocation
    Private lblTitle As System.Windows.Forms.Label
    Private lblDistrict As System.Windows.Forms.Label
    Private cmbDistrict As System.Windows.Forms.ComboBox
    Private lblLandmark As System.Windows.Forms.Label
    Private txtLandmark As System.Windows.Forms.TextBox
    Private lblMapLink As System.Windows.Forms.Label
    Private txtMapLink As System.Windows.Forms.TextBox
    Private btnSend As System.Windows.Forms.Button
    Private btnCancel As System.Windows.Forms.Button

    Private _locationText As String = ""
    Private _mapLink As String = ""

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property LocationText As String
        Get
            Return _locationText
        End Get
        Set(value As String)
            _locationText = value
        End Set
    End Property

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property MapLink As String
        Get
            Return _mapLink
        End Get
        Set(value As String)
            _mapLink = value
        End Set
    End Property

    Private Sub frmShareLocation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildUI()
    End Sub

    Private Sub BuildUI()
        lblTitle = New System.Windows.Forms.Label()
        lblTitle.AutoSize = True
        lblTitle.Font = New System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold)
        lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        lblTitle.Location = New System.Drawing.Point(20, 20)
        lblTitle.Text = "Share Your Location"
        Me.Controls.Add(lblTitle)

        lblDistrict = New System.Windows.Forms.Label()
        lblDistrict.AutoSize = True
        lblDistrict.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        lblDistrict.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblDistrict.Location = New System.Drawing.Point(20, 65)
        lblDistrict.Text = "District *"
        Me.Controls.Add(lblDistrict)

        cmbDistrict = New System.Windows.Forms.ComboBox()
        cmbDistrict.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
        cmbDistrict.Font = New System.Drawing.Font("Segoe UI", 11)
        cmbDistrict.Location = New System.Drawing.Point(20, 88)
        cmbDistrict.Size = New System.Drawing.Size(440, 28)
        Me.Controls.Add(cmbDistrict)

        lblLandmark = New System.Windows.Forms.Label()
        lblLandmark.AutoSize = True
        lblLandmark.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        lblLandmark.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblLandmark.Location = New System.Drawing.Point(20, 125)
        lblLandmark.Text = "Landmark / Address *"
        Me.Controls.Add(lblLandmark)

        txtLandmark = New System.Windows.Forms.TextBox()
        txtLandmark.Font = New System.Drawing.Font("Segoe UI", 11)
        txtLandmark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtLandmark.Location = New System.Drawing.Point(20, 148)
        txtLandmark.Size = New System.Drawing.Size(440, 27)
        Me.Controls.Add(txtLandmark)

        lblMapLink = New System.Windows.Forms.Label()
        lblMapLink.AutoSize = True
        lblMapLink.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        lblMapLink.ForeColor = System.Drawing.Color.FromArgb(79, 107, 108)
        lblMapLink.Location = New System.Drawing.Point(20, 185)
        lblMapLink.Text = "Map Link (optional)"
        Me.Controls.Add(lblMapLink)

        txtMapLink = New System.Windows.Forms.TextBox()
        txtMapLink.Font = New System.Drawing.Font("Segoe UI", 11)
        txtMapLink.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtMapLink.Location = New System.Drawing.Point(20, 208)
        txtMapLink.Size = New System.Drawing.Size(440, 27)
        Me.Controls.Add(txtMapLink)

        btnSend = New System.Windows.Forms.Button()
        btnSend.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnSend.FlatAppearance.BorderSize = 0
        btnSend.Font = New System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold)
        btnSend.ForeColor = System.Drawing.Color.White
        btnSend.Location = New System.Drawing.Point(20, 260)
        btnSend.Size = New System.Drawing.Size(215, 42)
        btnSend.Text = "Send Location"
        btnSend.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnSend.Click, AddressOf btnSend_Click
        Me.Controls.Add(btnSend)

        btnCancel = New System.Windows.Forms.Button()
        btnCancel.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.Font = New System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold)
        btnCancel.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        btnCancel.Location = New System.Drawing.Point(245, 260)
        btnCancel.Size = New System.Drawing.Size(215, 42)
        btnCancel.Text = "Cancel"
        btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        AddHandler btnCancel.Click, AddressOf btnCancel_Click
        Me.Controls.Add(btnCancel)
    End Sub

    Private Sub btnSend_Click(sender As Object, e As EventArgs)
        If cmbDistrict.Text.Trim() = "" Or txtLandmark.Text.Trim() = "" Then
            MessageBox.Show("Please fill in the district and landmark.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        _locationText = cmbDistrict.Text.Trim() & " | " & txtLandmark.Text.Trim()
        _mapLink = txtMapLink.Text.Trim()

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class