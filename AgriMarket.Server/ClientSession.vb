Public Class ClientSession
    Public Property SessionID As Integer
    Public Property IPAddress As String
    Public Property LoginTime As DateTime
    Public Property LastActivity As DateTime

    Public Sub New(ip As String, loginTime As DateTime)
        Me.IPAddress = ip
        Me.LoginTime = loginTime
        Me.LastActivity = loginTime
    End Sub
End Class