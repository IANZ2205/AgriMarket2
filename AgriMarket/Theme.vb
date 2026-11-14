Imports System.Drawing

Public Module Theme
    ' Colors from the AgriLink/AgriMarket design doc
    Public ReadOnly Ocean As Color = Color.FromArgb(1, 96, 100)
    Public ReadOnly Teal As Color = Color.FromArgb(95, 179, 179)
    Public ReadOnly Background As Color = Color.FromArgb(244, 249, 249)
    Public ReadOnly Card As Color = Color.White
    Public ReadOnly Tint As Color = Color.FromArgb(232, 243, 243)
    Public ReadOnly TextDark As Color = Color.FromArgb(15, 43, 45)
    Public ReadOnly TextMuted As Color = Color.FromArgb(79, 107, 108)
    Public ReadOnly Border As Color = Color.FromArgb(211, 228, 228)
    Public ReadOnly Danger As Color = Color.FromArgb(179, 38, 30)

    ' Fonts
    Public ReadOnly FontMain As Font = New Font("Segoe UI", 10.0!, FontStyle.Regular)
    Public ReadOnly FontBold As Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)
    Public ReadOnly FontHeader As Font = New Font("Segoe UI", 16.0!, FontStyle.Bold)
    Public ReadOnly FontTitle As Font = New Font("Segoe UI", 20.0!, FontStyle.Bold)
End Module