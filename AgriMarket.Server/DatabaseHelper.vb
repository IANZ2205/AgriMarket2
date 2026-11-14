Imports Microsoft.Data.Sqlite
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Imports System.Data

Public Class DatabaseHelper
    Private Shared ReadOnly ConnectionString As String = $"Data Source={Application.StartupPath}\AgriMarket.db"

    Public Shared Sub InitializeDatabase()
        Try
            Using connection As New SqliteConnection(ConnectionString)
                connection.Open()

                ' 1. Users Table (Added ProfilePic)
                Dim createUsersTable As String = "
                    CREATE TABLE IF NOT EXISTS Users (
                        UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Email TEXT NOT NULL UNIQUE,
                        PasswordHash TEXT NOT NULL,
                        Salt TEXT NOT NULL,
                        Role TEXT NOT NULL,
                        DisplayName TEXT,
                        Status TEXT NOT NULL,
                        Phone TEXT,
                        District TEXT,
                        ProfilePic TEXT,
                        CreatedAt TEXT NOT NULL
                    );"
                Using command As New SqliteCommand(createUsersTable, connection)
                    command.ExecuteNonQuery()
                End Using

                ' 2. Items Table
                Dim createItemsTable As String = "
                    CREATE TABLE IF NOT EXISTS Items (
                        ItemID INTEGER PRIMARY KEY AUTOINCREMENT,
                        TraderID INTEGER NOT NULL,
                        EnglishName TEXT NOT NULL,
                        Category TEXT NOT NULL,
                        Description TEXT,
                        UnitPrice REAL NOT NULL,
                        LowStockLevel INTEGER DEFAULT 10,
                        Status TEXT DEFAULT 'Active',
                        CreatedAt TEXT NOT NULL
                    );"
                Using command As New SqliteCommand(createItemsTable, connection)
                    command.ExecuteNonQuery()
                End Using

                ' 3. ItemImages Table
                Dim createItemImagesTable As String = "
                    CREATE TABLE IF NOT EXISTS ItemImages (
                        ImageID INTEGER PRIMARY KEY AUTOINCREMENT,
                        ItemID INTEGER NOT NULL,
                        FileName TEXT NOT NULL,
                        IsPrimary INTEGER DEFAULT 0
                    );"
                Using command As New SqliteCommand(createItemImagesTable, connection)
                    command.ExecuteNonQuery()
                End Using

                ' 4. Batches Table
                Dim createBatchesTable As String = "
                    CREATE TABLE IF NOT EXISTS Batches (
                        BatchID INTEGER PRIMARY KEY AUTOINCREMENT,
                        ItemID INTEGER NOT NULL,
                        BatchNo TEXT NOT NULL,
                        QtyAdded REAL NOT NULL,
                        QtyRemaining REAL NOT NULL,
                        QtyReserved REAL DEFAULT 0,
                        ShelfLifeDays INTEGER NOT NULL,
                        DateAdded TEXT NOT NULL,
                        ExpiryDate TEXT NOT NULL,
                        Status TEXT DEFAULT 'Active'
                    );"
                Using command As New SqliteCommand(createBatchesTable, connection)
                    command.ExecuteNonQuery()
                End Using

                ' 5. Orders Table
                Dim createOrdersTable As String = "
                    CREATE TABLE IF NOT EXISTS Orders (
                        OrderID INTEGER PRIMARY KEY AUTOINCREMENT,
                        ItemID INTEGER NOT NULL,
                        TraderID INTEGER NOT NULL,
                        BuyerID INTEGER NOT NULL,
                        Qty REAL NOT NULL,
                        UnitPriceAtOrder REAL NOT NULL,
                        TotalAmount REAL NOT NULL,
                        Status TEXT DEFAULT 'Pending',
                        CreatedAt TEXT NOT NULL
                    );"
                Using command As New SqliteCommand(createOrdersTable, connection)
                    command.ExecuteNonQuery()
                End Using

                ' 6. OrderEvents Table
                Dim createOrderEventsTable As String = "
                    CREATE TABLE IF NOT EXISTS OrderEvents (
                        EventID INTEGER PRIMARY KEY AUTOINCREMENT,
                        OrderID INTEGER NOT NULL,
                        Status TEXT NOT NULL,
                        ByUserID INTEGER NOT NULL,
                        EventAt TEXT NOT NULL,
                        Comment TEXT
                    );"
                Using command As New SqliteCommand(createOrderEventsTable, connection)
                    command.ExecuteNonQuery()
                End Using

                ' 7. Notifications Table
                Dim createNotificationsTable As String = "
                    CREATE TABLE IF NOT EXISTS Notifications (
                        NotifID INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserID INTEGER NOT NULL,
                        NotifType TEXT NOT NULL,
                        Title TEXT NOT NULL,
                        Body TEXT NOT NULL,
                        IsRead INTEGER DEFAULT 0,
                        CreatedAt TEXT NOT NULL
                    );"
                Using command As New SqliteCommand(createNotificationsTable, connection)
                    command.ExecuteNonQuery()
                End Using

                ' 8. Conversations Table
                Dim createConversationsTable As String = "
                    CREATE TABLE IF NOT EXISTS Conversations (
                        ConvID INTEGER PRIMARY KEY AUTOINCREMENT,
                        User1ID INTEGER NOT NULL,
                        User2ID INTEGER NOT NULL,
                        CreatedAt TEXT NOT NULL,
                        UNIQUE(User1ID, User2ID)
                    );"
                Using command As New SqliteCommand(createConversationsTable, connection)
                    command.ExecuteNonQuery()
                End Using

                ' 9. Messages Table
                Dim createMessagesTable As String = "
                    CREATE TABLE IF NOT EXISTS Messages (
                        MessageID INTEGER PRIMARY KEY AUTOINCREMENT,
                        ConvID INTEGER NOT NULL,
                        SenderID INTEGER NOT NULL,
                        Body TEXT NOT NULL,
                        SentAt TEXT NOT NULL,
                        IsRead INTEGER DEFAULT 0
                    );"
                Using command As New SqliteCommand(createMessagesTable, connection)
                    command.ExecuteNonQuery()
                End Using

                ' 10. Master Lists
                Dim createCategoriesTable As String = "CREATE TABLE IF NOT EXISTS Categories (CategoryID INTEGER PRIMARY KEY AUTOINCREMENT, CategoryName TEXT NOT NULL UNIQUE);"
                Using command As New SqliteCommand(createCategoriesTable, connection)
                    command.ExecuteNonQuery()
                End Using
                Dim createDistrictsTable As String = "CREATE TABLE IF NOT EXISTS Districts (DistrictID INTEGER PRIMARY KEY AUTOINCREMENT, DistrictName TEXT NOT NULL UNIQUE);"
                Using command As New SqliteCommand(createDistrictsTable, connection)
                    command.ExecuteNonQuery()
                End Using

                ' 11. Server Logs Table
                Dim createLogsTable As String = "
                    CREATE TABLE IF NOT EXISTS ServerLogs (
                        LogID INTEGER PRIMARY KEY AUTOINCREMENT,
                        LoggedAt TEXT NOT NULL,
                        Level TEXT NOT NULL,
                        EventType TEXT NOT NULL,
                        Details TEXT NOT NULL
                    );"
                Using command As New SqliteCommand(createLogsTable, connection)
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Database initialization failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Shared Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Return Convert.ToBase64String(hash)
        End Using
    End Function

    Public Shared Sub SeedAdminUser()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim checkQuery As String = "SELECT COUNT(*) FROM Users"
            Using checkCmd As New SqliteCommand(checkQuery, connection)
                Dim count As Long = Convert.ToInt64(checkCmd.ExecuteScalar())
                If count = 0 Then
                    Dim insertQuery As String = "INSERT INTO Users (Email, PasswordHash, Salt, Role, DisplayName, Status, CreatedAt) VALUES ('admin@agrimarket.com', @hash, 'defaultsalt', 'Admin', 'System Admin', 'Active', @date)"
                    Using insertCmd As New SqliteCommand(insertQuery, connection)
                        insertCmd.Parameters.AddWithValue("@hash", HashPassword("password123"))
                        insertCmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        insertCmd.ExecuteNonQuery()
                    End Using
                End If
            End Using
        End Using
    End Sub

    Public Shared Function AuthenticateUser(email As String, password As String) As DataRow
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT * FROM Users WHERE Email = @email AND PasswordHash = @hash AND Status = 'Active'"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@email", email)
                cmd.Parameters.AddWithValue("@hash", HashPassword(password))
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    Dim dt As New DataTable()
                    dt.Load(reader)
                    If dt.Rows.Count > 0 Then Return dt.Rows(0) Else Return Nothing
                End Using
            End Using
        End Using
    End Function

    Public Shared Function AuthenticateUserRaw(email As String, password As String) As DataRow
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT * FROM Users WHERE Email = @email AND PasswordHash = @hash AND Status != 'Suspended'"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@email", email)
                cmd.Parameters.AddWithValue("@hash", HashPassword(password))
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    Dim dt As New DataTable()
                    dt.Load(reader)
                    If dt.Rows.Count > 0 Then Return dt.Rows(0) Else Return Nothing
                End Using
            End Using
        End Using
    End Function

    Public Shared Function RegisterUser(email As String, password As String, role As String, name As String, base64ProfilePic As String) As Boolean
        Try
            Dim initialStatus As String = If(role = "Trader", "Pending", "Active")
            Dim profilePicPath As String = SaveImageToFolder(base64ProfilePic, "profiles")

            Using connection As New SqliteConnection(ConnectionString)
                connection.Open()
                Dim query As String = "INSERT INTO Users (Email, PasswordHash, Salt, Role, DisplayName, Status, ProfilePic, CreatedAt) VALUES (@email, @hash, 'defaultsalt', @role, @name, @status, @pic, @date)"
                Using cmd As New SqliteCommand(query, connection)
                    cmd.Parameters.AddWithValue("@email", email)
                    cmd.Parameters.AddWithValue("@hash", HashPassword(password))
                    cmd.Parameters.AddWithValue("@role", role)
                    cmd.Parameters.AddWithValue("@name", name)
                    cmd.Parameters.AddWithValue("@status", initialStatus)
                    cmd.Parameters.AddWithValue("@pic", profilePicPath)
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As SqliteException
            Return False
        End Try
    End Function

    Public Shared Function AddItemWithBatch(traderId As Integer, name As String, category As String, price As Decimal, qty As Integer, shelfLife As Integer, img1 As String, img2 As String, img3 As String) As Boolean
        Dim itemCode As String = name.Substring(0, Math.Min(3, name.Length)).ToUpper()
        Dim dateStr As String = DateTime.Now.ToString("yyyyMMdd")
        Dim batchNo As String = $"{itemCode}-{dateStr}-01"

        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Using transaction As SqliteTransaction = connection.BeginTransaction()
                Try
                    Dim insertItemQuery As String = "INSERT INTO Items (TraderID, EnglishName, Category, Description, UnitPrice, Status, CreatedAt) VALUES (@traderId, @name, @category, '', @price, 'Active', @date); SELECT last_insert_rowid();"
                    Dim itemId As Long = 0
                    Using cmd As New SqliteCommand(insertItemQuery, connection)
                        cmd.Transaction = transaction
                        cmd.Parameters.AddWithValue("@traderId", traderId)
                        cmd.Parameters.AddWithValue("@name", name)
                        cmd.Parameters.AddWithValue("@category", category)
                        cmd.Parameters.AddWithValue("@price", price)
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        itemId = Convert.ToInt64(cmd.ExecuteScalar())
                    End Using

                    If Not String.IsNullOrEmpty(img1) Then
                        Dim fName As String = SaveImageToFolder(img1, "products")
                        Using cmd As New SqliteCommand("INSERT INTO ItemImages (ItemID, FileName, IsPrimary) VALUES (@id, @file, 1)", connection)
                            cmd.Transaction = transaction
                            cmd.Parameters.AddWithValue("@id", itemId)
                            cmd.Parameters.AddWithValue("@file", fName)
                            cmd.ExecuteNonQuery()
                        End Using
                    End If
                    If Not String.IsNullOrEmpty(img2) Then
                        Dim fName As String = SaveImageToFolder(img2, "products")
                        Using cmd As New SqliteCommand("INSERT INTO ItemImages (ItemID, FileName, IsPrimary) VALUES (@id, @file, 0)", connection)
                            cmd.Transaction = transaction
                            cmd.Parameters.AddWithValue("@id", itemId)
                            cmd.Parameters.AddWithValue("@file", fName)
                            cmd.ExecuteNonQuery()
                        End Using
                    End If
                    If Not String.IsNullOrEmpty(img3) Then
                        Dim fName As String = SaveImageToFolder(img3, "products")
                        Using cmd As New SqliteCommand("INSERT INTO ItemImages (ItemID, FileName, IsPrimary) VALUES (@id, @file, 0)", connection)
                            cmd.Transaction = transaction
                            cmd.Parameters.AddWithValue("@id", itemId)
                            cmd.Parameters.AddWithValue("@file", fName)
                            cmd.ExecuteNonQuery()
                        End Using
                    End If

                    Dim insertBatchQuery As String = "INSERT INTO Batches (ItemID, BatchNo, QtyAdded, QtyRemaining, ShelfLifeDays, DateAdded, ExpiryDate, Status) VALUES (@itemId, @batchNo, @qty, @qty, @shelf, @dateAdded, @expiry, 'Active')"
                    Using cmd As New SqliteCommand(insertBatchQuery, connection)
                        cmd.Transaction = transaction
                        cmd.Parameters.AddWithValue("@itemId", itemId)
                        cmd.Parameters.AddWithValue("@batchNo", batchNo)
                        cmd.Parameters.AddWithValue("@qty", qty)
                        cmd.Parameters.AddWithValue("@shelf", shelfLife)
                        cmd.Parameters.AddWithValue("@dateAdded", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd.Parameters.AddWithValue("@expiry", DateTime.Now.AddDays(shelfLife).ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd.ExecuteNonQuery()
                    End Using

                    transaction.Commit()
                    Return True
                Catch ex As Exception
                    transaction.Rollback()
                    MessageBox.Show("Error adding item: " & ex.Message)
                    Return False
                End Try
            End Using
        End Using
    End Function

    Private Shared Function SaveImageToFolder(base64Image As String, folderName As String) As String
        If String.IsNullOrEmpty(base64Image) Then Return ""
        Try
            Dim imageBytes As Byte() = Convert.FromBase64String(base64Image)
            Dim fileName As String = Guid.NewGuid().ToString() & ".jpg"
            Dim folderPath As String = Path.Combine(Application.StartupPath, "Files", folderName)
            If Not Directory.Exists(folderPath) Then Directory.CreateDirectory(folderPath)
            File.WriteAllBytes(Path.Combine(folderPath, fileName), imageBytes)
            Return fileName
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function GetTraderStock(traderId As Integer) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "
                SELECT b.BatchID, i.ItemID, i.EnglishName AS [Produce Name], i.Category, i.UnitPrice AS [Price (UGX)],
                       b.BatchNo AS [Batch Number], b.QtyRemaining AS [Available Qty], 
                       b.ExpiryDate AS [Expiry Date], b.Status AS [Batch Status],
                       (SELECT FileName FROM ItemImages WHERE ItemID = i.ItemID AND IsPrimary = 1 LIMIT 1) AS ImagePath
                FROM Items i
                JOIN Batches b ON i.ItemID = b.ItemID
                WHERE i.TraderID = @traderId
                ORDER BY i.CreatedAt DESC"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@traderId", traderId)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function GetAllAvailableStock() As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "
                SELECT i.ItemID, i.EnglishName AS [Produce], i.Category, i.UnitPrice AS [Price (UGX)], 
                       b.QtyRemaining AS [Available Qty], b.ExpiryDate AS [Expiry Date],
                       u.DisplayName AS [Trader Name],
                       (SELECT FileName FROM ItemImages WHERE ItemID = i.ItemID AND IsPrimary = 1 LIMIT 1) AS ImagePath
                FROM Items i
                JOIN Batches b ON i.ItemID = b.ItemID
                JOIN Users u ON i.TraderID = u.UserID
                WHERE i.Status = 'Active' AND b.Status = 'Active' AND b.QtyRemaining > 0 AND u.Status = 'Active'
                ORDER BY i.CreatedAt DESC"
            Using cmd As New SqliteCommand(query, connection)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function SearchStock(searchTerm As String) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "
                SELECT i.ItemID, i.EnglishName AS [Produce], i.Category, i.UnitPrice AS [Price (UGX)], 
                       b.QtyRemaining AS [Available Qty], b.ExpiryDate AS [Expiry Date],
                       u.DisplayName AS [Trader Name],
                       (SELECT FileName FROM ItemImages WHERE ItemID = i.ItemID AND IsPrimary = 1 LIMIT 1) AS ImagePath
                FROM Items i
                JOIN Batches b ON i.ItemID = b.ItemID
                JOIN Users u ON i.TraderID = u.UserID
                WHERE i.Status = 'Active' AND b.Status = 'Active' AND b.QtyRemaining > 0 AND u.Status = 'Active'
                  AND (i.EnglishName LIKE @search OR u.DisplayName LIKE @search)
                ORDER BY i.CreatedAt DESC"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function GetItemDetailById(itemId As Integer) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "
                SELECT i.ItemID, i.EnglishName AS [Produce], i.Category, i.UnitPrice AS [Price], 
                       b.QtyRemaining AS [Qty], b.ExpiryDate AS [Expiry], i.Description,
                       u.UserID AS TraderID, u.DisplayName AS TraderName, u.Phone AS TraderPhone, u.District AS TraderDistrict,
                       (SELECT FileName FROM ItemImages WHERE ItemID = i.ItemID AND IsPrimary = 1 LIMIT 1) AS ImagePath
                FROM Items i
                JOIN Batches b ON i.ItemID = b.ItemID
                JOIN Users u ON i.TraderID = u.UserID
                WHERE i.ItemID = @itemId"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@itemId", itemId)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function GetTraderDetails(traderId As Integer) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT DisplayName, Phone, District, (SELECT COUNT(OrderID) FROM Orders WHERE TraderID = @id AND Status = 'Completed') AS CompletedOrders FROM Users WHERE UserID = @id"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@id", traderId)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function GetUserIdByName(name As String) As Integer
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT UserID FROM Users WHERE DisplayName = @name"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@name", name)
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot Nothing Then Return Convert.ToInt32(result)
            End Using
        End Using
        Return 0
    End Function

    Public Shared Function PlaceOrder(itemId As Integer, traderId As Integer, buyerId As Integer, qty As Integer, price As Decimal) As Boolean
        Dim total As Decimal = qty * price
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Using transaction As SqliteTransaction = connection.BeginTransaction()
                Try
                    Dim insertOrderQuery As String = "INSERT INTO Orders (ItemID, TraderID, BuyerID, Qty, UnitPriceAtOrder, TotalAmount, Status, CreatedAt) VALUES (@itemId, @traderId, @buyerId, @qty, @price, @total, 'Pending', @date); SELECT last_insert_rowid();"
                    Dim orderId As Long = 0
                    Using cmd As New SqliteCommand(insertOrderQuery, connection)
                        cmd.Transaction = transaction
                        cmd.Parameters.AddWithValue("@itemId", itemId)
                        cmd.Parameters.AddWithValue("@traderId", traderId)
                        cmd.Parameters.AddWithValue("@buyerId", buyerId)
                        cmd.Parameters.AddWithValue("@qty", qty)
                        cmd.Parameters.AddWithValue("@price", price)
                        cmd.Parameters.AddWithValue("@total", total)
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        orderId = Convert.ToInt64(cmd.ExecuteScalar())
                    End Using
                    Dim insertEventQuery As String = "INSERT INTO OrderEvents (OrderID, Status, ByUserID, EventAt, Comment) VALUES (@orderId, 'Pending', @buyerId, @date, 'Order placed by buyer.')"
                    Using cmd As New SqliteCommand(insertEventQuery, connection)
                        cmd.Transaction = transaction
                        cmd.Parameters.AddWithValue("@orderId", orderId)
                        cmd.Parameters.AddWithValue("@buyerId", buyerId)
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd.ExecuteNonQuery()
                    End Using
                    transaction.Commit()
                    Return True
                Catch ex As Exception
                    transaction.Rollback()
                    Return False
                End Try
            End Using
        End Using
    End Function

    Public Shared Function GetTraderOrders(traderId As Integer) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT o.OrderID, i.EnglishName AS [Produce], u.DisplayName AS [Buyer], o.Qty, o.TotalAmount AS [Total (UGX)], o.Status FROM Orders o JOIN Items i ON o.ItemID = i.ItemID JOIN Users u ON o.BuyerID = u.UserID WHERE o.TraderID = @traderId ORDER BY o.CreatedAt DESC"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@traderId", traderId)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function UpdateOrderStatus(orderId As Integer, newStatus As String, userId As Integer, comment As String) As Boolean
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Using transaction As SqliteTransaction = connection.BeginTransaction()
                Try
                    Dim currentStatus As String = ""
                    Using cmd As New SqliteCommand("SELECT Status FROM Orders WHERE OrderID = @orderId", connection)
                        cmd.Transaction = transaction
                        cmd.Parameters.AddWithValue("@orderId", orderId)
                        currentStatus = cmd.ExecuteScalar().ToString()
                    End Using

                    Dim updateQuery As String = "UPDATE Orders SET Status = @status WHERE OrderID = @orderId"
                    Using cmd As New SqliteCommand(updateQuery, connection)
                        cmd.Transaction = transaction
                        cmd.Parameters.AddWithValue("@status", newStatus)
                        cmd.Parameters.AddWithValue("@orderId", orderId)
                        cmd.ExecuteNonQuery()
                    End Using

                    Dim itemId As Integer = 0
                    Dim qty As Integer = 0
                    Using cmd As New SqliteCommand("SELECT ItemID, Qty FROM Orders WHERE OrderID = @orderId", connection)
                        cmd.Transaction = transaction
                        cmd.Parameters.AddWithValue("@orderId", orderId)
                        Using reader As SqliteDataReader = cmd.ExecuteReader()
                            If reader.Read() Then
                                itemId = reader.GetInt32(0)
                                qty = reader.GetInt32(1)
                            End If
                        End Using
                    End Using

                    If itemId > 0 Then
                        If newStatus = "Confirmed" Then
                            Dim reserveQuery As String = "UPDATE Batches SET QtyReserved = QtyReserved + @qty WHERE BatchID = (SELECT BatchID FROM Batches WHERE ItemID = @itemId AND QtyRemaining > QtyReserved ORDER BY ExpiryDate ASC LIMIT 1)"
                            Using cmd As New SqliteCommand(reserveQuery, connection)
                                cmd.Transaction = transaction
                                cmd.Parameters.AddWithValue("@qty", qty)
                                cmd.Parameters.AddWithValue("@itemId", itemId)
                                cmd.ExecuteNonQuery()
                            End Using
                        ElseIf newStatus = "Completed" Then
                            Dim deductQuery As String = "UPDATE Batches SET QtyRemaining = QtyRemaining - @qty, QtyReserved = QtyReserved - @qty WHERE BatchID = (SELECT BatchID FROM Batches WHERE ItemID = @itemId AND QtyReserved >= @qty ORDER BY ExpiryDate ASC LIMIT 1)"
                            Using cmd As New SqliteCommand(deductQuery, connection)
                                cmd.Transaction = transaction
                                cmd.Parameters.AddWithValue("@qty", qty)
                                cmd.Parameters.AddWithValue("@itemId", itemId)
                                cmd.ExecuteNonQuery()
                            End Using
                        ElseIf newStatus = "Cancelled" Or newStatus = "Rejected" Then
                            If currentStatus = "Confirmed" Then
                                Dim releaseQuery As String = "UPDATE Batches SET QtyReserved = QtyReserved - @qty WHERE BatchID = (SELECT BatchID FROM Batches WHERE ItemID = @itemId AND QtyReserved >= @qty ORDER BY ExpiryDate ASC LIMIT 1)"
                                Using cmd As New SqliteCommand(releaseQuery, connection)
                                    cmd.Transaction = transaction
                                    cmd.Parameters.AddWithValue("@qty", qty)
                                    cmd.Parameters.AddWithValue("@itemId", itemId)
                                    cmd.ExecuteNonQuery()
                                End Using
                            End If
                        End If
                    End If

                    Dim eventQuery As String = "INSERT INTO OrderEvents (OrderID, Status, ByUserID, EventAt, Comment) VALUES (@orderId, @status, @userId, @date, @comment)"
                    Using cmd As New SqliteCommand(eventQuery, connection)
                        cmd.Transaction = transaction
                        cmd.Parameters.AddWithValue("@orderId", orderId)
                        cmd.Parameters.AddWithValue("@status", newStatus)
                        cmd.Parameters.AddWithValue("@userId", userId)
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd.Parameters.AddWithValue("@comment", If(String.IsNullOrEmpty(comment), "", comment))
                        cmd.ExecuteNonQuery()
                    End Using

                    transaction.Commit()
                    Return True
                Catch ex As Exception
                    transaction.Rollback()
                    Return False
                End Try
            End Using
        End Using
    End Function

    Public Shared Function GetBuyerIdForOrder(orderId As Integer) As Integer
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT BuyerID FROM Orders WHERE OrderID = @orderId"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@orderId", orderId)
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot Nothing Then Return Convert.ToInt32(result)
            End Using
        End Using
        Return 0
    End Function

    Public Shared Function GetTraderIdForOrder(orderId As Integer) As Integer
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT TraderID FROM Orders WHERE OrderID = @orderId"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@orderId", orderId)
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot Nothing Then Return Convert.ToInt32(result)
            End Using
        End Using
        Return 0
    End Function

    Public Shared Function GetBuyerOrders(buyerId As Integer) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT o.OrderID, i.EnglishName AS [Produce], u.DisplayName AS [Trader], o.Qty, o.TotalAmount AS [Total (UGX)], o.Status, o.CreatedAt AS [Date] FROM Orders o JOIN Items i ON o.ItemID = i.ItemID JOIN Users u ON o.TraderID = u.UserID WHERE o.BuyerID = @buyerId ORDER BY o.CreatedAt DESC"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@buyerId", buyerId)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function GetTraderDashboardStats(traderId As Integer) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT (SELECT COUNT(ItemID) FROM Items WHERE TraderID = @traderId AND Status = 'Active') AS ItemsInStock, (SELECT COUNT(OrderID) FROM Orders WHERE TraderID = @traderId AND Status = 'Pending') AS PendingOrders, (SELECT COUNT(OrderID) FROM Orders WHERE TraderID = @traderId AND Status = 'Completed') AS CompletedOrders, (SELECT IFNULL(SUM(TotalAmount), 0) FROM Orders WHERE TraderID = @traderId AND Status = 'Completed') AS TotalSales"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@traderId", traderId)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function GetAdminDashboardStats() As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT (SELECT COUNT(UserID) FROM Users) AS TotalUsers, (SELECT COUNT(UserID) FROM Users WHERE Role = 'Trader' AND Status = 'Active') AS TotalTraders, (SELECT COUNT(UserID) FROM Users WHERE (Role = 'Retailer' OR Role = 'Wholesaler') AND Status = 'Active') AS TotalBuyers, (SELECT COUNT(OrderID) FROM Orders) AS TotalOrders"
            Using cmd As New SqliteCommand(query, connection)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function GetAllUsers() As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT UserID, DisplayName AS [Name], Email, Role, Status FROM Users ORDER BY CreatedAt DESC"
            Using cmd As New SqliteCommand(query, connection)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function ApproveUser(userId As Integer) As Boolean
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "UPDATE Users SET Status = 'Active' WHERE UserID = @userId"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@userId", userId)
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    Public Shared Function GetOrCreateConversation(user1 As Integer, user2 As Integer) As Integer
        Dim lowUser As Integer = Math.Min(user1, user2)
        Dim highUser As Integer = Math.Max(user1, user2)
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim checkQuery As String = "SELECT ConvID FROM Conversations WHERE User1ID = @u1 AND User2ID = @u2"
            Using cmd As New SqliteCommand(checkQuery, connection)
                cmd.Parameters.AddWithValue("@u1", lowUser)
                cmd.Parameters.AddWithValue("@u2", highUser)
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot Nothing Then Return Convert.ToInt32(result)
            End Using
            Dim insertQuery As String = "INSERT INTO Conversations (User1ID, User2ID, CreatedAt) VALUES (@u1, @u2, @date); SELECT last_insert_rowid();"
            Using cmd As New SqliteCommand(insertQuery, connection)
                cmd.Parameters.AddWithValue("@u1", lowUser)
                cmd.Parameters.AddWithValue("@u2", highUser)
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    Public Shared Function SendMessage(convId As Integer, senderId As Integer, body As String) As Boolean
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "INSERT INTO Messages (ConvID, SenderID, Body, SentAt) VALUES (@convId, @senderId, @body, @date)"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@convId", convId)
                cmd.Parameters.AddWithValue("@senderId", senderId)
                cmd.Parameters.AddWithValue("@body", body)
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    Public Shared Function GetChatHistory(convId As Integer) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT m.MessageID, m.SenderID, u.DisplayName AS SenderName, m.Body, m.SentAt FROM Messages m JOIN Users u ON m.SenderID = u.UserID WHERE m.ConvID = @convId ORDER BY m.MessageID ASC"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@convId", convId)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function GetUserConversations(userId As Integer) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT c.ConvID, CASE WHEN c.User1ID = @userId THEN c.User2ID ELSE c.User1ID END AS OtherUserID, u.DisplayName AS OtherUserName, u.Role AS OtherUserRole FROM Conversations c JOIN Users u ON (c.User1ID = u.UserID OR c.User2ID = u.UserID) WHERE (c.User1ID = @userId OR c.User2ID = @userId) AND u.UserID <> @userId"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@userId", userId)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function GetUserNameById(userId As Integer) As String
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT DisplayName FROM Users WHERE UserID = @userId"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@userId", userId)
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot Nothing Then Return result.ToString()
            End Using
        End Using
        Return "Unknown"
    End Function

    Public Shared Sub AddNotification(userId As Integer, notifType As String, title As String, body As String)
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "INSERT INTO Notifications (UserID, NotifType, Title, Body, IsRead, CreatedAt) VALUES (@userId, @type, @title, @body, 0, @date)"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@userId", userId)
                cmd.Parameters.AddWithValue("@type", notifType)
                cmd.Parameters.AddWithValue("@title", title)
                cmd.Parameters.AddWithValue("@body", body)
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Shared Function GetNotifications(userId As Integer) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT NotifID, NotifType, Title, Body, IsRead, CreatedAt FROM Notifications WHERE UserID = @userId ORDER BY CreatedAt DESC"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@userId", userId)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Sub MarkNotificationsAsRead(userId As Integer)
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "UPDATE Notifications SET IsRead = 1 WHERE UserID = @userId"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@userId", userId)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Shared Function GetTraderBatchesToWatch(traderId As Integer) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT b.BatchNo AS [Batch], i.EnglishName AS [Produce], b.ExpiryDate AS [Expiry], b.QtyRemaining AS [Qty], b.Status FROM Batches b JOIN Items i ON b.ItemID = i.ItemID WHERE i.TraderID = @traderId AND b.Status = 'Active' AND (date(b.ExpiryDate) <= date('now', '+7 days') OR b.QtyRemaining <= 10) ORDER BY b.ExpiryDate ASC"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@traderId", traderId)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Sub RunDailyMaintenance()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT b.BatchID, b.BatchNo, b.ExpiryDate, b.Status, i.TraderID, i.EnglishName FROM Batches b JOIN Items i ON b.ItemID = i.ItemID WHERE b.Status = 'Active' AND b.QtyRemaining > 0"
            Using cmd As New SqliteCommand(query, connection)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    Dim batchesToUpdate As New List(Of Tuple(Of Integer, String, Integer, String, String))
                    While reader.Read()
                        Dim batchId As Integer = reader.GetInt32(0)
                        Dim batchNo As String = reader.GetString(1)
                        Dim expiryDate As DateTime = Convert.ToDateTime(reader.GetString(2))
                        Dim currentStatus As String = reader.GetString(3)
                        Dim traderId As Integer = reader.GetInt32(4)
                        Dim itemName As String = reader.GetString(5)
                        Dim daysLeft As Integer = (expiryDate - DateTime.Now.Date).Days
                        Dim newStatus As String = currentStatus
                        If daysLeft <= 0 Then
                            newStatus = "Expired"
                        ElseIf daysLeft <= 2 Then
                            newStatus = "Expiring"
                        End If
                        If newStatus <> currentStatus Then
                            batchesToUpdate.Add(New Tuple(Of Integer, String, Integer, String, String)(batchId, newStatus, traderId, batchNo, itemName))
                        End If
                    End While
                    reader.Close()
                    For Each b In batchesToUpdate
                        Using updateCmd As New SqliteCommand("UPDATE Batches SET Status = @status WHERE BatchID = @id", connection)
                            updateCmd.Parameters.AddWithValue("@status", b.Item2)
                            updateCmd.Parameters.AddWithValue("@id", b.Item1)
                            updateCmd.ExecuteNonQuery()
                        End Using
                        Dim msg As String = $"Batch {b.Item4} of {b.Item5} is now {b.Item2}."
                        AddNotification(b.Item3, "BATCH_" & b.Item2.ToUpper(), "Stock Alert", msg)
                    Next
                End Using
            End Using
        End Using
    End Sub

    Public Shared Function TopUpItemBatch(itemId As Integer, qty As Integer, shelfLife As Integer) As String
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim itemName As String = ""
            Using cmd As New SqliteCommand("SELECT EnglishName FROM Items WHERE ItemID = @id", connection)
                cmd.Parameters.AddWithValue("@id", itemId)
                itemName = cmd.ExecuteScalar().ToString()
            End Using
            Dim itemCode As String = itemName.Substring(0, Math.Min(3, itemName.Length)).ToUpper()
            Dim dateStr As String = DateTime.Now.ToString("yyyyMMdd")
            Dim countQuery As String = "SELECT COUNT(*) FROM Batches WHERE ItemID = @id AND DateAdded LIKE @today"
            Dim count As Integer = 0
            Using cmd As New SqliteCommand(countQuery, connection)
                cmd.Parameters.AddWithValue("@id", itemId)
                cmd.Parameters.AddWithValue("@today", dateStr & "%")
                count = Convert.ToInt32(cmd.ExecuteScalar())
            End Using
            Dim batchNo As String = $"{itemCode}-{dateStr}-{(count + 1).ToString("D2")}"
            Dim query As String = "INSERT INTO Batches (ItemID, BatchNo, QtyAdded, QtyRemaining, ShelfLifeDays, DateAdded, ExpiryDate, Status) VALUES (@id, @batchNo, @qty, @qty, @shelf, @dateAdded, @expiry, 'Active')"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@id", itemId)
                cmd.Parameters.AddWithValue("@batchNo", batchNo)
                cmd.Parameters.AddWithValue("@qty", qty)
                cmd.Parameters.AddWithValue("@shelf", shelfLife)
                cmd.Parameters.AddWithValue("@dateAdded", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.Parameters.AddWithValue("@expiry", DateTime.Now.AddDays(shelfLife).ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.ExecuteNonQuery()
            End Using
            Return batchNo
        End Using
    End Function

    Public Shared Function UpdateItemDetails(itemId As Integer, newName As String, newPrice As Decimal, newDesc As String) As Boolean
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "UPDATE Items SET EnglishName = @name, UnitPrice = @price, Description = @desc WHERE ItemID = @id"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@name", newName)
                cmd.Parameters.AddWithValue("@price", newPrice)
                cmd.Parameters.AddWithValue("@desc", newDesc)
                cmd.Parameters.AddWithValue("@id", itemId)
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    Public Shared Function AdjustBatchQuantity(batchId As Integer, newQty As Integer, reason As String) As Boolean
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "UPDATE Batches SET QtyRemaining = @qty, Status = CASE WHEN @qty <= 0 THEN 'SoldOut' ELSE 'Active' END WHERE BatchID = @id"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@qty", newQty)
                cmd.Parameters.AddWithValue("@id", batchId)
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    Public Shared Function GetUserProfile(userId As Integer) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT DisplayName, Email, Phone, District FROM Users WHERE UserID = @id"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@id", userId)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function UpdateUserProfile(userId As Integer, name As String, phone As String, district As String) As Boolean
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "UPDATE Users SET DisplayName = @name, Phone = @phone, District = @district WHERE UserID = @id"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@name", name)
                cmd.Parameters.AddWithValue("@phone", phone)
                cmd.Parameters.AddWithValue("@district", district)
                cmd.Parameters.AddWithValue("@id", userId)
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    Public Shared Function ChangeUserPassword(userId As Integer, newPass As String) As Boolean
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "UPDATE Users SET PasswordHash = @hash WHERE UserID = @id"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@hash", HashPassword(newPass))
                cmd.Parameters.AddWithValue("@id", userId)
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    Public Shared Function GetStockReport() As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT u.DisplayName AS [Trader], i.EnglishName AS [Produce], b.BatchNo AS [Batch Number], b.DateAdded, b.ExpiryDate, b.QtyRemaining AS [Remaining], b.QtyReserved AS [Reserved], b.Status FROM Batches b JOIN Items i ON b.ItemID = i.ItemID JOIN Users u ON i.TraderID = u.UserID ORDER BY u.DisplayName, i.EnglishName"
            Using cmd As New SqliteCommand(query, connection)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function GetSalesReport() As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT o.OrderID AS [Order No], o.CreatedAt AS [Date], u.DisplayName AS [Buyer], i.EnglishName AS [Produce], o.Qty, o.UnitPriceAtOrder AS [Unit Price], o.TotalAmount AS [Total (UGX)] FROM Orders o JOIN Items i ON o.ItemID = i.ItemID JOIN Users u ON o.BuyerID = u.UserID WHERE o.Status = 'Completed' ORDER BY o.CreatedAt DESC"
            Using cmd As New SqliteCommand(query, connection)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function GetMasterList(listType As String) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = ""
            If listType = "Categories" Then
                query = "SELECT CategoryID AS ID, CategoryName AS Name FROM Categories ORDER BY CategoryName"
            ElseIf listType = "Districts" Then
                query = "SELECT DistrictID AS ID, DistrictName AS Name FROM Districts ORDER BY DistrictName"
            End If
            If query <> "" Then
                Using cmd As New SqliteCommand(query, connection)
                    Using reader As SqliteDataReader = cmd.ExecuteReader()
                        dt.Load(reader)
                    End Using
                End Using
            End If
        End Using
        Return dt
    End Function

    Public Shared Function AddMasterListItem(listType As String, name As String) As Boolean
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = ""
            If listType = "Categories" Then
                query = "INSERT INTO Categories (CategoryName) VALUES (@name)"
            ElseIf listType = "Districts" Then
                query = "INSERT INTO Districts (DistrictName) VALUES (@name)"
            End If
            Try
                Using cmd As New SqliteCommand(query, connection)
                    cmd.Parameters.AddWithValue("@name", name)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            Catch ex As SqliteException
                Return False
            End Try
        End Using
    End Function

    Public Shared Function DeleteMasterListItem(listType As String, id As Integer) As Boolean
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = ""
            If listType = "Categories" Then
                query = "DELETE FROM Categories WHERE CategoryID = @id"
            ElseIf listType = "Districts" Then
                query = "DELETE FROM Districts WHERE DistrictID = @id"
            End If
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@id", id)
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    Public Shared Sub WriteLog(level As String, eventType As String, details As String)
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "INSERT INTO ServerLogs (LoggedAt, Level, EventType, Details) VALUES (@date, @level, @type, @details)"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.Parameters.AddWithValue("@level", level)
                cmd.Parameters.AddWithValue("@type", eventType)
                cmd.Parameters.AddWithValue("@details", details)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Shared Function GetLogs() As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT LogID AS [ID], LoggedAt AS [Time], Level, EventType AS [Type], Details FROM ServerLogs ORDER BY LogID DESC"
            Using cmd As New SqliteCommand(query, connection)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function GetAllListingsAdmin() As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "
                SELECT i.ItemID, i.EnglishName AS [Produce], u.DisplayName AS [Trader], 
                       i.Category, i.UnitPrice AS [Price], i.Status
                FROM Items i
                JOIN Users u ON i.TraderID = u.UserID
                ORDER BY i.CreatedAt DESC"
            Using cmd As New SqliteCommand(query, connection)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function GetAllOrdersAdmin() As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "
                SELECT o.OrderID AS [Order No], i.EnglishName AS [Produce], 
                       u1.DisplayName AS [Buyer], u2.DisplayName AS [Trader], 
                       o.Qty, o.TotalAmount AS [Total], o.Status, o.CreatedAt AS [Date]
                FROM Orders o
                JOIN Items i ON o.ItemID = i.ItemID
                JOIN Users u1 ON o.BuyerID = u1.UserID
                JOIN Users u2 ON o.TraderID = u2.UserID
                ORDER BY o.CreatedAt DESC"
            Using cmd As New SqliteCommand(query, connection)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function
End Class