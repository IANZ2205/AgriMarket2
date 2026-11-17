<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucChats
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
        Me.panelSidebar = New System.Windows.Forms.Panel()
        Me.lblChatsTitle = New System.Windows.Forms.Label()
        Me.dgvConversations = New System.Windows.Forms.DataGridView()
        Me.panelChatArea = New System.Windows.Forms.Panel()
        Me.panelMessages = New System.Windows.Forms.Panel()
        Me.panelInput = New System.Windows.Forms.Panel()
        Me.btnAttach = New ModernButton()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.btnSend = New ModernButton()
        Me.lblChatHeader = New System.Windows.Forms.Label()
        Me.btnMore = New ModernButton()
        Me.ctxAttach = New System.Windows.Forms.ContextMenuStrip()
        Me.ctxChatMenu = New System.Windows.Forms.ContextMenuStrip()
        Me.lblBlockedNotice = New System.Windows.Forms.Label()
        CType(Me.dgvConversations, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelSidebar.SuspendLayout()
        Me.panelChatArea.SuspendLayout()
        Me.panelInput.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelSidebar
        '
        Me.panelSidebar.BackColor = System.Drawing.Color.White
        Me.panelSidebar.Controls.Add(Me.dgvConversations)
        Me.panelSidebar.Controls.Add(Me.lblChatsTitle)
        Me.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left
        Me.panelSidebar.Location = New System.Drawing.Point(0, 0)
        Me.panelSidebar.Name = "panelSidebar"
        Me.panelSidebar.Size = New System.Drawing.Size(300, 610)
        Me.panelSidebar.TabIndex = 0
        '
        'lblChatsTitle
        '
        Me.lblChatsTitle.AutoSize = True
        Me.lblChatsTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblChatsTitle.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.lblChatsTitle.Location = New System.Drawing.Point(20, 20)
        Me.lblChatsTitle.Name = "lblChatsTitle"
        Me.lblChatsTitle.Size = New System.Drawing.Size(150, 30)
        Me.lblChatsTitle.TabIndex = 0
        Me.lblChatsTitle.Text = "Conversations"
        '
        'dgvConversations
        '
        Me.dgvConversations.AllowUserToAddRows = False
        Me.dgvConversations.AllowUserToDeleteRows = False
        Me.dgvConversations.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgvConversations.Location = New System.Drawing.Point(20, 70)
        Me.dgvConversations.Name = "dgvConversations"
        Me.dgvConversations.ReadOnly = True
        Me.dgvConversations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvConversations.Size = New System.Drawing.Size(260, 520)
        Me.dgvConversations.TabIndex = 1
        '
        'panelChatArea
        '
        Me.panelChatArea.BackColor = System.Drawing.Color.FromArgb(244, 249, 249)
        Me.panelChatArea.Controls.Add(Me.lblBlockedNotice)
        Me.panelChatArea.Controls.Add(Me.panelMessages)
        Me.panelChatArea.Controls.Add(Me.panelInput)
        Me.panelChatArea.Controls.Add(Me.btnMore)
        Me.panelChatArea.Controls.Add(Me.lblChatHeader)
        Me.panelChatArea.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelChatArea.Location = New System.Drawing.Point(300, 0)
        Me.panelChatArea.Name = "panelChatArea"
        Me.panelChatArea.Size = New System.Drawing.Size(880, 610)
        Me.panelChatArea.TabIndex = 1
        '
        'panelMessages
        '
        Me.panelMessages.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panelMessages.AutoScroll = True
        Me.panelMessages.BackColor = System.Drawing.Color.White
        Me.panelMessages.Location = New System.Drawing.Point(20, 70)
        Me.panelMessages.Name = "panelMessages"
        Me.panelMessages.Size = New System.Drawing.Size(840, 420)
        Me.panelMessages.TabIndex = 2
        '
        'panelInput
        '
        Me.panelInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panelInput.Controls.Add(Me.btnAttach)
        Me.panelInput.Controls.Add(Me.btnSend)
        Me.panelInput.Controls.Add(Me.txtMessage)
        Me.panelInput.Location = New System.Drawing.Point(20, 530)
        Me.panelInput.Name = "panelInput"
        Me.panelInput.Size = New System.Drawing.Size(840, 60)
        Me.panelInput.TabIndex = 3
        '
        'btnAttach
        '
        Me.btnAttach.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.btnAttach.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnAttach.Location = New System.Drawing.Point(0, 10)
        Me.btnAttach.Name = "btnAttach"
        Me.btnAttach.Size = New System.Drawing.Size(80, 40)
        Me.btnAttach.TabIndex = 2
        Me.btnAttach.Text = "+ Attach"
        '
        'txtMessage
        '
        Me.txtMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtMessage.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtMessage.Location = New System.Drawing.Point(90, 10)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(630, 29)
        Me.txtMessage.TabIndex = 0
        '
        'btnSend
        '
        Me.btnSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSend.BackColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnSend.ForeColor = System.Drawing.Color.White
        Me.btnSend.Location = New System.Drawing.Point(740, 5)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(100, 40)
        Me.btnSend.TabIndex = 1
        Me.btnSend.Text = "Send"
        '
        'lblChatHeader
        '
        Me.lblChatHeader.AutoSize = True
        Me.lblChatHeader.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblChatHeader.ForeColor = System.Drawing.Color.FromArgb(15, 43, 45)
        Me.lblChatHeader.Location = New System.Drawing.Point(20, 20)
        Me.lblChatHeader.Name = "lblChatHeader"
        Me.lblChatHeader.Size = New System.Drawing.Size(180, 30)
        Me.lblChatHeader.TabIndex = 4
        Me.lblChatHeader.Text = "Select a conversation"
        '
        'btnMore
        '
        Me.btnMore.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMore.BackColor = System.Drawing.Color.FromArgb(232, 243, 243)
        Me.btnMore.ForeColor = System.Drawing.Color.FromArgb(1, 96, 100)
        Me.btnMore.Location = New System.Drawing.Point(830, 15)
        Me.btnMore.Name = "btnMore"
        Me.btnMore.Size = New System.Drawing.Size(40, 40)
        Me.btnMore.TabIndex = 5
        Me.btnMore.Text = "..."
        '
        'lblBlockedNotice
        '
        Me.lblBlockedNotice.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBlockedNotice.BackColor = System.Drawing.Color.FromArgb(251, 227, 224)
        Me.lblBlockedNotice.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblBlockedNotice.ForeColor = System.Drawing.Color.FromArgb(179, 38, 30)
        Me.lblBlockedNotice.Location = New System.Drawing.Point(20, 500)
        Me.lblBlockedNotice.Name = "lblBlockedNotice"
        Me.lblBlockedNotice.Size = New System.Drawing.Size(840, 25)
        Me.lblBlockedNotice.TabIndex = 6
        Me.lblBlockedNotice.Text = "You cannot message this user."
        Me.lblBlockedNotice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblBlockedNotice.Visible = False
        '
        'ctxAttach
        '
        Me.ctxAttach.Name = "ctxAttach"
        Me.ctxAttach.Size = New System.Drawing.Size(180, 70)
        '
        'ctxChatMenu
        '
        Me.ctxChatMenu.Name = "ctxChatMenu"
        Me.ctxChatMenu.Size = New System.Drawing.Size(180, 70)
        '
        'ucChats
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.panelChatArea)
        Me.Controls.Add(Me.panelSidebar)
        Me.Name = "ucChats"
        Me.Size = New System.Drawing.Size(1180, 610)
        CType(Me.dgvConversations, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelSidebar.ResumeLayout(False)
        Me.panelSidebar.PerformLayout()
        Me.panelChatArea.ResumeLayout(False)
        Me.panelChatArea.PerformLayout()
        Me.panelInput.ResumeLayout(False)
        Me.panelInput.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelSidebar As System.Windows.Forms.Panel
    Friend WithEvents lblChatsTitle As System.Windows.Forms.Label
    Friend WithEvents dgvConversations As System.Windows.Forms.DataGridView
    Friend WithEvents panelChatArea As System.Windows.Forms.Panel
    Friend WithEvents panelMessages As System.Windows.Forms.Panel
    Friend WithEvents panelInput As System.Windows.Forms.Panel
    Friend WithEvents btnAttach As ModernButton
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents btnSend As ModernButton
    Friend WithEvents lblChatHeader As System.Windows.Forms.Label
    Friend WithEvents btnMore As ModernButton
    Friend WithEvents ctxAttach As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ctxChatMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents lblBlockedNotice As System.Windows.Forms.Label
End Class