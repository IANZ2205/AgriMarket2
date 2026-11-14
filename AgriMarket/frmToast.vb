Imports System.Runtime.InteropServices

Public Class frmToast
    ' Make the form have rounded corners
    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")>
    Private Shared Function CreateRoundRectRgn(ByVal nLeftRect As Integer, ByVal nTopRect As Integer, ByVal nRightRect As Integer, ByVal nBottomRect As Integer, ByVal nWidthEllipse As Integer, ByVal nHeightEllipse As Integer) As IntPtr
    End Function

    Private WithEvents timer As New Timer()

    Public Sub New(title As String, message As String)
        InitializeComponent()
        lblTitle.Text = title
        lblMessage.Text = message

        ' Apply rounded corners
        Me.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15))
    End Sub

    Private Sub frmToast_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Position in bottom right corner of screen
        Dim workingArea = Screen.PrimaryScreen.WorkingArea
        Me.Location = New Point(workingArea.Right - Me.Width - 20, workingArea.Bottom - Me.Height - 20)

        ' Auto close after 4 seconds
        timer.Interval = 4000
        timer.Start()
    End Sub

    Private Sub timer_Tick(sender As Object, e As EventArgs) Handles timer.Tick
        timer.Stop()
        Me.Close()
    End Sub
End Class