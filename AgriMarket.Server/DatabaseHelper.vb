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

                ' 1. Users
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
                        MustChangePassword INTEGER DEFAULT 0,
                        CreatedAt TEXT NOT NULL
                    );"
                Using command As New SqliteCommand(createUsersTable, connection)
                    command.ExecuteNonQuery()
                End Using

                ' === MIGRATIONS: Add columns to old DBs ===
                Try
                    Using m As New SqliteCommand("ALTER TABLE Users ADD COLUMN Phone TEXT;", connection)
                        m.ExecuteNonQuery()
                    End Using
                Catch
                End Try
                Try
                    Using m As New SqliteCommand("ALTER TABLE Users ADD COLUMN District TEXT;", connection)
                        m.ExecuteNonQuery()
                    End Using
                Catch
                End Try
                Try
                    Using m As New SqliteCommand("ALTER TABLE Users ADD COLUMN ProfilePic TEXT;", connection)
                        m.ExecuteNonQuery()
                    End Using
                Catch
                End Try
                Try
                    Using m As New SqliteCommand("ALTER TABLE Users ADD COLUMN MustChangePassword INTEGER DEFAULT 0;", connection)
                        m.ExecuteNonQuery()
                    End Using
                Catch
                End Try
                Try
                    Using m As New SqliteCommand("ALTER TABLE Users ADD COLUMN SecurityQuestionID INTEGER;", connection)
                        m.ExecuteNonQuery()
                    End Using
                Catch
                End Try
                Try
                    Using m As New SqliteCommand("ALTER TABLE Users ADD COLUMN SecurityAnswerHash TEXT;", connection)
                        m.ExecuteNonQuery()
                    End Using
                Catch
                End Try                ' === Orders migrations (Phase C) ===
                Try
                    Using m As New SqliteCommand("ALTER TABLE Orders ADD COLUMN OrderNo TEXT;", connection)
                        m.ExecuteNonQuery()
                    End Using
                Catch
                End Try
                Try
                    Using m As New SqliteCommand("ALTER TABLE Orders ADD COLUMN NeededBy TEXT;", connection)
                        m.ExecuteNonQuery()
                    End Using
                Catch
                End Try
                Try
                    Using m As New SqliteCommand("ALTER TABLE Orders ADD COLUMN DeliveryMode TEXT DEFAULT 'Pickup';", connection)
                        m.ExecuteNonQuery()
                    End Using
                Catch
                End Try
                Try
                    Using m As New SqliteCommand("ALTER TABLE Orders ADD COLUMN DeliveryNote TEXT;", connection)
                        m.ExecuteNonQuery()
                    End Using
                Catch
                End Try
                Try
                    Using m As New SqliteCommand("ALTER TABLE Orders ADD COLUMN BuyerNote TEXT;", connection)
                        m.ExecuteNonQuery()
                    End Using
                Catch
                End Try
                Try
                    Using m As New SqliteCommand("ALTER TABLE Orders ADD COLUMN ReasonID INTEGER;", connection)
                        m.ExecuteNonQuery()
                    End Using
                Catch
                End Try
                Try
                    Using m As New SqliteCommand("ALTER TABLE Orders ADD COLUMN ReasonComment TEXT;", connection)
                        m.ExecuteNonQuery()
                    End Using
                Catch
                End Try
                Try
                    Using m As New SqliteCommand("ALTER TABLE Orders ADD COLUMN ConfirmedAt TEXT;", connection)
                        m.ExecuteNonQuery()
                    End Using
                Catch
                End Try
                Try
                    Using m As New SqliteCommand("ALTER TABLE Orders ADD COLUMN ClosedAt TEXT;", connection)
                        m.ExecuteNonQuery()
                    End Using
                Catch
                End Try
                ' === END MIGRATIONS ===

                ' 2. Items (added Unit column)
                Dim createItemsTable As String = "
                    CREATE TABLE IF NOT EXISTS Items (
                        ItemID INTEGER PRIMARY KEY AUTOINCREMENT,
                        TraderID INTEGER NOT NULL,
                        EnglishName TEXT NOT NULL,
                        Category TEXT NOT NULL,
                        Description TEXT,
                        UnitPrice REAL NOT NULL,
                        Unit TEXT DEFAULT 'kg',
                        LowStockLevel INTEGER DEFAULT 10,
                        Status TEXT DEFAULT 'Active',
                        CreatedAt TEXT NOT NULL
                    );"
                Using command As New SqliteCommand(createItemsTable, connection)
                    command.ExecuteNonQuery()
                End Using

                ' Migrations for older DBs
                Try
                    Using command As New SqliteCommand("ALTER TABLE Items ADD COLUMN Description TEXT;", connection)
                        command.ExecuteNonQuery()
                    End Using
                Catch
                End Try
                Try
                    Using command As New SqliteCommand("ALTER TABLE Items ADD COLUMN Unit TEXT DEFAULT 'kg';", connection)
                        command.ExecuteNonQuery()
                    End Using
                Catch
                End Try

                ' 3. ItemImages
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

                ' 4. Batches
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

                ' 5. Orders
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

                ' 6. OrderEvents
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

                ' 7. Notifications
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

                ' 8. Conversations
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

                ' 9. Messages
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

                ' 10. Categories, Districts
                Dim createCategoriesTable As String = "CREATE TABLE IF NOT EXISTS Categories (CategoryID INTEGER PRIMARY KEY AUTOINCREMENT, CategoryName TEXT NOT NULL UNIQUE);"
                Using command As New SqliteCommand(createCategoriesTable, connection)
                    command.ExecuteNonQuery()
                End Using
                Dim createDistrictsTable As String = "CREATE TABLE IF NOT EXISTS Districts (DistrictID INTEGER PRIMARY KEY AUTOINCREMENT, DistrictName TEXT NOT NULL UNIQUE);"
                Using command As New SqliteCommand(createDistrictsTable, connection)
                    command.ExecuteNonQuery()
                End Using

                ' 11. ServerLogs
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

                ' 12. Announcements
                Dim createAnnouncementsTable As String = "
                    CREATE TABLE IF NOT EXISTS Announcements (
                        AnnID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT NOT NULL,
                        Body TEXT NOT NULL,
                        Audience TEXT NOT NULL,
                        CreatedBy INTEGER NOT NULL,
                        CreatedAt TEXT NOT NULL
                    );"
                Using command As New SqliteCommand(createAnnouncementsTable, connection)
                    command.ExecuteNonQuery()
                End Using

                ' 13. Units master list
                Dim createUnitsTable As String = "
                    CREATE TABLE IF NOT EXISTS Units (
                        UnitID INTEGER PRIMARY KEY AUTOINCREMENT,
                        UnitName TEXT NOT NULL UNIQUE
                    );"
                Using command As New SqliteCommand(createUnitsTable, connection)
                    command.ExecuteNonQuery()
                End Using

                ' Seed Units if empty (from write-up section 8.4)
                Using cmd As New SqliteCommand("SELECT COUNT(*) FROM Units", connection)
                    If Convert.ToInt32(cmd.ExecuteScalar()) = 0 Then
                        Dim seedUnits As String = "
                            INSERT INTO Units (UnitName) VALUES ('kg');
                            INSERT INTO Units (UnitName) VALUES ('g');
                            INSERT INTO Units (UnitName) VALUES ('tonne');
                            INSERT INTO Units (UnitName) VALUES ('bag');
                            INSERT INTO Units (UnitName) VALUES ('sack');
                            INSERT INTO Units (UnitName) VALUES ('crate');
                            INSERT INTO Units (UnitName) VALUES ('basket');
                            INSERT INTO Units (UnitName) VALUES ('bunch');
                            INSERT INTO Units (UnitName) VALUES ('tin');
                            INSERT INTO Units (UnitName) VALUES ('litre');
                            INSERT INTO Units (UnitName) VALUES ('dozen');
                            INSERT INTO Units (UnitName) VALUES ('tray');
                            INSERT INTO Units (UnitName) VALUES ('head');
                            INSERT INTO Units (UnitName) VALUES ('bird');
                            INSERT INTO Units (UnitName) VALUES ('piece');"
                        Using cmdInsert As New SqliteCommand(seedUnits, connection)
                            cmdInsert.ExecuteNonQuery()
                        End Using
                    End If
                End Using

                ' ================================================================
                ' PHASE A: Tables for advanced features
                ' ================================================================

                ' 14. Languages master list
                Using cmd As New SqliteCommand("
                    CREATE TABLE IF NOT EXISTS Languages (
                        LanguageID INTEGER PRIMARY KEY AUTOINCREMENT,
                        LanguageName TEXT NOT NULL UNIQUE,
                        SortOrder INTEGER DEFAULT 0,
                        Active INTEGER DEFAULT 1
                    );", connection)
                    cmd.ExecuteNonQuery()
                End Using

                ' 15. ItemLocalNames - up to 4 local names per item
                Using cmd As New SqliteCommand("
                    CREATE TABLE IF NOT EXISTS ItemLocalNames (
                        ItemID INTEGER NOT NULL,
                        LanguageID INTEGER NOT NULL,
                        LocalName TEXT NOT NULL,
                        PRIMARY KEY (ItemID, LanguageID)
                    );", connection)
                    cmd.ExecuteNonQuery()
                End Using

                ' 16. RejectionReasons
                Using cmd As New SqliteCommand("
                    CREATE TABLE IF NOT EXISTS RejectionReasons (
                        ReasonID INTEGER PRIMARY KEY AUTOINCREMENT,
                        ReasonText TEXT NOT NULL UNIQUE,
                        AppliesTo TEXT DEFAULT 'Reject',
                        RequiresComment INTEGER DEFAULT 0,
                        Active INTEGER DEFAULT 1
                    );", connection)
                    cmd.ExecuteNonQuery()
                End Using

                ' 17. CategoryUnits - which units are valid for which category
                Using cmd As New SqliteCommand("
                    CREATE TABLE IF NOT EXISTS CategoryUnits (
                        CategoryID INTEGER NOT NULL,
                        UnitID INTEGER NOT NULL,
                        PRIMARY KEY (CategoryID, UnitID)
                    );", connection)
                    cmd.ExecuteNonQuery()
                End Using

                ' 18. Settings - key/value store
                Using cmd As New SqliteCommand("
                    CREATE TABLE IF NOT EXISTS Settings (
                        SettingKey TEXT PRIMARY KEY,
                        SettingValue TEXT NOT NULL,
                        Description TEXT
                    );", connection)
                    cmd.ExecuteNonQuery()
                End Using

                ' 19. Sessions - login history
                Using cmd As New SqliteCommand("
                    CREATE TABLE IF NOT EXISTS Sessions (
                        SessionID INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserID INTEGER NOT NULL,
                        Token TEXT,
                        IPAddress TEXT,
                        LoginAt TEXT NOT NULL,
                        LastActivityAt TEXT NOT NULL,
                        LogoutAt TEXT,
                        EndReason TEXT
                    );", connection)
                    cmd.ExecuteNonQuery()
                End Using

                ' 20. PerfSamples - one row per minute
                Using cmd As New SqliteCommand("
                    CREATE TABLE IF NOT EXISTS PerfSamples (
                        SampleID INTEGER PRIMARY KEY AUTOINCREMENT,
                        SampledAt TEXT NOT NULL,
                        UsersOnline INTEGER,
                        ReqPerSec REAL,
                        KBInPerSec REAL,
                        KBOutPerSec REAL,
                        AvgRespMs REAL,
                        CpuPct REAL,
                        MemMB REAL
                    );", connection)
                    cmd.ExecuteNonQuery()
                End Using

                ' 21. OrderAllocations - FEFO tracking
                Using cmd As New SqliteCommand("
                    CREATE TABLE IF NOT EXISTS OrderAllocations (
                        AllocID INTEGER PRIMARY KEY AUTOINCREMENT,
                        OrderID INTEGER NOT NULL,
                        BatchID INTEGER NOT NULL,
                        Qty REAL NOT NULL
                    );", connection)
                    cmd.ExecuteNonQuery()
                End Using

                ' === Add 'Other' category if missing (per write-up 8.4) ===
                Using cmd As New SqliteCommand("SELECT COUNT(*) FROM Categories WHERE CategoryName = 'Other'", connection)
                    If Convert.ToInt32(cmd.ExecuteScalar()) = 0 Then
                        Using cmdInsert As New SqliteCommand("INSERT INTO Categories (CategoryName) VALUES ('Other')", connection)
                            cmdInsert.ExecuteNonQuery()
                        End Using
                    End If
                End Using

                ' === SEED LANGUAGES ===
                Using cmd As New SqliteCommand("SELECT COUNT(*) FROM Languages", connection)
                    If Convert.ToInt32(cmd.ExecuteScalar()) = 0 Then
                        Dim seedLang As String = "
                            INSERT INTO Languages (LanguageName, SortOrder) VALUES ('Luganda', 1);
                            INSERT INTO Languages (LanguageName, SortOrder) VALUES ('Runyankole/Rukiga', 2);
                            INSERT INTO Languages (LanguageName, SortOrder) VALUES ('Lusoga', 3);
                            INSERT INTO Languages (LanguageName, SortOrder) VALUES ('Acholi', 4);"
                        Using cmdInsert As New SqliteCommand(seedLang, connection)
                            cmdInsert.ExecuteNonQuery()
                        End Using
                    End If
                End Using

                ' === SEED REJECTION REASONS ===
                Using cmd As New SqliteCommand("SELECT COUNT(*) FROM RejectionReasons", connection)
                    If Convert.ToInt32(cmd.ExecuteScalar()) = 0 Then
                        Dim seedReasons As String = "
                            INSERT INTO RejectionReasons (ReasonText, AppliesTo, RequiresComment) VALUES ('Price not agreed', 'Reject', 0);
                            INSERT INTO RejectionReasons (ReasonText, AppliesTo, RequiresComment) VALUES ('Not enough stock', 'Reject', 0);
                            INSERT INTO RejectionReasons (ReasonText, AppliesTo, RequiresComment) VALUES ('Item no longer available', 'Reject', 0);
                            INSERT INTO RejectionReasons (ReasonText, AppliesTo, RequiresComment) VALUES ('Buyer asked to cancel', 'Cancel', 0);
                            INSERT INTO RejectionReasons (ReasonText, AppliesTo, RequiresComment) VALUES ('Delivery not possible', 'Cancel', 0);
                            INSERT INTO RejectionReasons (ReasonText, AppliesTo, RequiresComment) VALUES ('Other', 'Both', 1);"
                        Using cmdInsert As New SqliteCommand(seedReasons, connection)
                            cmdInsert.ExecuteNonQuery()
                        End Using
                    End If
                End Using

                ' === SEED SETTINGS ===
                Using cmd As New SqliteCommand("SELECT COUNT(*) FROM Settings", connection)
                    If Convert.ToInt32(cmd.ExecuteScalar()) = 0 Then
                        Dim seedSettings As String = "
                            INSERT INTO Settings (SettingKey, SettingValue, Description) VALUES ('ExpirySoonDays', '2', 'Days before expiry to mark as expiring soon');
                            INSERT INTO Settings (SettingKey, SettingValue, Description) VALUES ('ExpirySoonPercent', '25', 'Percentage of shelf life for expiring soon threshold');
                            INSERT INTO Settings (SettingKey, SettingValue, Description) VALUES ('DefaultLowStock', '10', 'Default low stock level for new items');
                            INSERT INTO Settings (SettingKey, SettingValue, Description) VALUES ('SessionTimeoutSeconds', '120', 'Seconds of inactivity before session ends');
                            INSERT INTO Settings (SettingKey, SettingValue, Description) VALUES ('MaxFailedLogins', '5', 'Failed login attempts before lockout');
                            INSERT INTO Settings (SettingKey, SettingValue, Description) VALUES ('LockMinutes', '5', 'Minutes to lock account after failed logins');
                            INSERT INTO Settings (SettingKey, SettingValue, Description) VALUES ('LogRetentionDays', '30', 'Days to keep server logs');
                            INSERT INTO Settings (SettingKey, SettingValue, Description) VALUES ('MaxFileMB', '5', 'Maximum file size in megabytes');
                            INSERT INTO Settings (SettingKey, SettingValue, Description) VALUES ('ServerPort', '5000', 'TCP port the server listens on');"
                        Using cmdInsert As New SqliteCommand(seedSettings, connection)
                            cmdInsert.ExecuteNonQuery()
                        End Using
                    End If
                End Using

                ' === SEED CATEGORY-UNIT LINKS ===
                Using cmd As New SqliteCommand("SELECT COUNT(*) FROM CategoryUnits", connection)
                    If Convert.ToInt32(cmd.ExecuteScalar()) = 0 Then
                        Try
                            Dim linkSql As String = ""
                            linkSql &= "INSERT INTO CategoryUnits (CategoryID, UnitID) SELECT c.CategoryID, u.UnitID FROM Categories c, Units u WHERE c.CategoryName = 'Food crop' AND u.UnitName IN ('kg','g','tonne','bag','sack','basket','bunch','tin');"
                            linkSql &= "INSERT INTO CategoryUnits (CategoryID, UnitID) SELECT c.CategoryID, u.UnitID FROM Categories c, Units u WHERE c.CategoryName = 'Cash crop' AND u.UnitName IN ('kg','g','tonne','bag','sack');"
                            linkSql &= "INSERT INTO CategoryUnits (CategoryID, UnitID) SELECT c.CategoryID, u.UnitID FROM Categories c, Units u WHERE c.CategoryName = 'Livestock' AND u.UnitName IN ('head','kg');"
                            linkSql &= "INSERT INTO CategoryUnits (CategoryID, UnitID) SELECT c.CategoryID, u.UnitID FROM Categories c, Units u WHERE c.CategoryName = 'Poultry' AND u.UnitName IN ('bird','kg');"
                            linkSql &= "INSERT INTO CategoryUnits (CategoryID, UnitID) SELECT c.CategoryID, u.UnitID FROM Categories c, Units u WHERE c.CategoryName = 'Fish' AND u.UnitName IN ('kg','piece');"
                            linkSql &= "INSERT INTO CategoryUnits (CategoryID, UnitID) SELECT c.CategoryID, u.UnitID FROM Categories c, Units u WHERE c.CategoryName = 'Dairy and eggs' AND u.UnitName IN ('litre','tray','dozen','piece');"
                            linkSql &= "INSERT INTO CategoryUnits (CategoryID, UnitID) SELECT c.CategoryID, u.UnitID FROM Categories c, Units u WHERE c.CategoryName = 'Other' AND u.UnitName IN ('kg','piece');"
                            Using cmdLink As New SqliteCommand(linkSql, connection)
                                cmdLink.ExecuteNonQuery()
                            End Using
                        Catch
                        End Try
                    End If
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
        Return RegisterUser(email, password, role, name, base64ProfilePic, 0, "")
    End Function

    Public Shared Function RegisterUser(email As String, password As String, role As String, name As String, base64ProfilePic As String, securityQuestionId As Integer, securityAnswer As String) As Boolean
        Try
            Dim initialStatus As String = If(role = "Trader", "Pending", "Active")
            Dim profilePicPath As String = SaveImageToFolder(base64ProfilePic, "profiles")
            Dim answerHash As String = If(String.IsNullOrEmpty(securityAnswer), "", HashSecurityAnswer(securityAnswer))

            Using connection As New SqliteConnection(ConnectionString)
                connection.Open()
                Dim query As String = "INSERT INTO Users (Email, PasswordHash, Salt, Role, DisplayName, Status, ProfilePic, SecurityQuestionID, SecurityAnswerHash, CreatedAt) VALUES (@email, @hash, 'defaultsalt', @role, @name, @status, @pic, @qid, @ahash, @date)"
                Using cmd As New SqliteCommand(query, connection)
                    cmd.Parameters.AddWithValue("@email", email)
                    cmd.Parameters.AddWithValue("@hash", HashPassword(password))
                    cmd.Parameters.AddWithValue("@role", role)
                    cmd.Parameters.AddWithValue("@name", name)
                    cmd.Parameters.AddWithValue("@status", initialStatus)
                    cmd.Parameters.AddWithValue("@pic", profilePicPath)
                    If securityQuestionId > 0 Then
                        cmd.Parameters.AddWithValue("@qid", securityQuestionId)
                    Else
                        cmd.Parameters.AddWithValue("@qid", DBNull.Value)
                    End If
                    If String.IsNullOrEmpty(answerHash) Then
                        cmd.Parameters.AddWithValue("@ahash", DBNull.Value)
                    Else
                        cmd.Parameters.AddWithValue("@ahash", answerHash)
                    End If
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As SqliteException
            Return False
        End Try
    End Function

    Public Shared Function HashSecurityAnswer(answer As String) As String
        If String.IsNullOrEmpty(answer) Then Return ""
        Return HashPassword(answer.Trim().ToLower())
    End Function
    Public Shared Function AddItemWithBatch(traderId As Integer, name As String, category As String, price As Decimal, qty As Integer, shelfLife As Integer, img1 As String, img2 As String, img3 As String) As Boolean
        Dim itemCode As String = name.Substring(0, Math.Min(3, name.Length)).ToUpper()
        Dim dateStr As String = DateTime.Now.ToString("yyyyMMdd")
        Dim batchNo As String = $"{itemCode}-{dateStr}-01"

        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Using transaction As SqliteTransaction = connection.BeginTransaction()
                Try
                    Dim insertItemQuery As String = "INSERT INTO Items (TraderID, EnglishName, Category, Description, UnitPrice, Unit, Status, CreatedAt) VALUES (@traderId, @name, @category, '', @price, 'kg', 'Active', @date); SELECT last_insert_rowid();"
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
                       (SELECT FileName FROM ItemImages WHERE ItemID = i.ItemID AND IsPrimary = 1 LIMIT 1) AS ImagePath,
                       IFNULL(i.Unit, 'kg') AS Unit
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
                       (SELECT FileName FROM ItemImages WHERE ItemID = i.ItemID AND IsPrimary = 1 LIMIT 1) AS ImagePath,
                       IFNULL(i.Unit, 'kg') AS Unit
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
                       (SELECT FileName FROM ItemImages WHERE ItemID = i.ItemID AND IsPrimary = 1 LIMIT 1) AS ImagePath,
                       IFNULL(i.Unit, 'kg') AS Unit
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
                       (SELECT FileName FROM ItemImages WHERE ItemID = i.ItemID AND IsPrimary = 1 LIMIT 1) AS ImagePath,
                       IFNULL(i.Unit, 'kg') AS Unit
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

    Public Shared Function PlaceOrder(itemId As Integer, traderId As Integer, buyerId As Integer, qty As Integer, price As Decimal, Optional neededBy As String = "", Optional deliveryMode As String = "Pickup", Optional deliveryNote As String = "", Optional buyerNote As String = "") As Boolean
        Dim total As Decimal = qty * price
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Using transaction As SqliteTransaction = connection.BeginTransaction()
                Try
                    Dim insertOrderQuery As String = "INSERT INTO Orders (ItemID, TraderID, BuyerID, Qty, UnitPriceAtOrder, TotalAmount, NeededBy, DeliveryMode, DeliveryNote, BuyerNote, Status, CreatedAt) VALUES (@itemId, @traderId, @buyerId, @qty, @price, @total, @neededBy, @deliveryMode, @deliveryNote, @buyerNote, 'Pending', @date); SELECT last_insert_rowid();"
                    Dim orderId As Long = 0
                    Using cmd As New SqliteCommand(insertOrderQuery, connection)
                        cmd.Transaction = transaction
                        cmd.Parameters.AddWithValue("@itemId", itemId)
                        cmd.Parameters.AddWithValue("@traderId", traderId)
                        cmd.Parameters.AddWithValue("@buyerId", buyerId)
                        cmd.Parameters.AddWithValue("@qty", qty)
                        cmd.Parameters.AddWithValue("@price", price)
                        cmd.Parameters.AddWithValue("@total", total)
                        cmd.Parameters.AddWithValue("@neededBy", If(String.IsNullOrEmpty(neededBy), "", neededBy))
                        cmd.Parameters.AddWithValue("@deliveryMode", If(String.IsNullOrEmpty(deliveryMode), "Pickup", deliveryMode))
                        cmd.Parameters.AddWithValue("@deliveryNote", If(String.IsNullOrEmpty(deliveryNote), "", deliveryNote))
                        cmd.Parameters.AddWithValue("@buyerNote", If(String.IsNullOrEmpty(buyerNote), "", buyerNote))
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
            Dim query As String = "SELECT DisplayName, Email, IFNULL(Phone, '') AS Phone, IFNULL(District, '') AS District, IFNULL(ProfilePic, '') AS ProfilePic FROM Users WHERE UserID = @id"
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
            ElseIf listType = "Units" Then
                query = "SELECT UnitID AS ID, UnitName AS Name FROM Units ORDER BY UnitID"
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
            ElseIf listType = "Units" Then
                query = "INSERT INTO Units (UnitName) VALUES (@name)"
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
            ElseIf listType = "Units" Then
                query = "DELETE FROM Units WHERE UnitID = @id"
            End If
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@id", id)
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    Public Shared Function GetAnnouncements() As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT AnnID AS [ID], Title, Body, Audience, CreatedAt AS [Sent At] FROM Announcements ORDER BY AnnID DESC"
            Using cmd As New SqliteCommand(query, connection)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function SendAnnouncement(title As String, body As String, audience As String, Optional createdBy As Integer = 0) As Boolean
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Using transaction As SqliteTransaction = connection.BeginTransaction()
                Try
                    Dim insertQuery As String = "INSERT INTO Announcements (Title, Body, Audience, CreatedBy, CreatedAt) VALUES (@title, @body, @aud, @by, @date)"
                    Using cmd As New SqliteCommand(insertQuery, connection)
                        cmd.Transaction = transaction
                        cmd.Parameters.AddWithValue("@title", title)
                        cmd.Parameters.AddWithValue("@body", body)
                        cmd.Parameters.AddWithValue("@aud", audience)
                        cmd.Parameters.AddWithValue("@by", createdBy)
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd.ExecuteNonQuery()
                    End Using

                    Dim userQuery As String
                    Select Case audience
                        Case "Traders"
                            userQuery = "SELECT UserID FROM Users WHERE Role = 'Trader' AND Status = 'Active'"
                        Case "Buyers"
                            userQuery = "SELECT UserID FROM Users WHERE (Role = 'Retailer' OR Role = 'Wholesaler') AND Status = 'Active'"
                        Case Else
                            userQuery = "SELECT UserID FROM Users WHERE Status = 'Active'"
                    End Select

                    Dim userIds As New List(Of Integer)
                    Using cmd As New SqliteCommand(userQuery, connection)
                        cmd.Transaction = transaction
                        Using reader As SqliteDataReader = cmd.ExecuteReader()
                            While reader.Read()
                                userIds.Add(reader.GetInt32(0))
                            End While
                        End Using
                    End Using

                    For Each uid As Integer In userIds
                        Dim notifQuery As String = "INSERT INTO Notifications (UserID, NotifType, Title, Body, IsRead, CreatedAt) VALUES (@uid, 'ANNOUNCEMENT', @title, @body, 0, @date)"
                        Using cmd As New SqliteCommand(notifQuery, connection)
                            cmd.Transaction = transaction
                            cmd.Parameters.AddWithValue("@uid", uid)
                            cmd.Parameters.AddWithValue("@title", title)
                            cmd.Parameters.AddWithValue("@body", body)
                            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                            cmd.ExecuteNonQuery()
                        End Using
                    Next

                    transaction.Commit()
                    Return True
                Catch ex As Exception
                    transaction.Rollback()
                    Return False
                End Try
            End Using
        End Using
    End Function

    Public Shared Function GetAllListingsAdmin() As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT i.ItemID, i.EnglishName AS [Produce], u.DisplayName AS [Trader], i.Category, i.UnitPrice AS [Price], i.Status FROM Items i JOIN Users u ON i.TraderID = u.UserID ORDER BY i.CreatedAt DESC"
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
            Dim query As String = "SELECT o.OrderID AS [Order No], i.EnglishName AS [Produce], u1.DisplayName AS [Buyer], u2.DisplayName AS [Trader], o.Qty, o.TotalAmount AS [Total], o.Status, o.CreatedAt AS [Date] FROM Orders o JOIN Items i ON o.ItemID = i.ItemID JOIN Users u1 ON o.BuyerID = u1.UserID JOIN Users u2 ON o.TraderID = u2.UserID ORDER BY o.CreatedAt DESC"
            Using cmd As New SqliteCommand(query, connection)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
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

    Public Shared Function AddItemWithBatchWithUnit(traderId As Integer, name As String, category As String, price As Decimal, unit As String, qty As Integer, shelfLife As Integer, img1 As String, img2 As String, img3 As String, Optional lowStockLevel As Integer = 10, Optional description As String = "", Optional localNames As Dictionary(Of Integer, String) = Nothing) As Boolean
        Dim itemCode As String = name.Substring(0, Math.Min(3, name.Length)).ToUpper()
        Dim dateStr As String = DateTime.Now.ToString("yyyyMMdd")
        Dim batchNo As String = $"{itemCode}-{dateStr}-01"

        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Using transaction As SqliteTransaction = connection.BeginTransaction()
                Try
                    Dim insertItemQuery As String = "INSERT INTO Items (TraderID, EnglishName, Category, Description, UnitPrice, Unit, LowStockLevel, Status, CreatedAt) VALUES (@traderId, @name, @category, @desc, @price, @unit, @lowStock, 'Active', @date); SELECT last_insert_rowid();"
                    Dim itemId As Long = 0
                    Using cmd As New SqliteCommand(insertItemQuery, connection)
                        cmd.Transaction = transaction
                        cmd.Parameters.AddWithValue("@traderId", traderId)
                        cmd.Parameters.AddWithValue("@name", name)
                        cmd.Parameters.AddWithValue("@category", category)
                        cmd.Parameters.AddWithValue("@desc", If(String.IsNullOrEmpty(description), "", description))
                        cmd.Parameters.AddWithValue("@price", price)
                        cmd.Parameters.AddWithValue("@unit", If(String.IsNullOrEmpty(unit), "kg", unit))
                        cmd.Parameters.AddWithValue("@lowStock", If(lowStockLevel <= 0, 10, lowStockLevel))
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        itemId = Convert.ToInt64(cmd.ExecuteScalar())
                    End Using

                    ' Save local names if provided
                    If localNames IsNot Nothing Then
                        For Each kvp In localNames
                            If Not String.IsNullOrWhiteSpace(kvp.Value) Then
                                Using cmd As New SqliteCommand("INSERT OR REPLACE INTO ItemLocalNames (ItemID, LanguageID, LocalName) VALUES (@id, @lang, @name)", connection)
                                    cmd.Transaction = transaction
                                    cmd.Parameters.AddWithValue("@id", itemId)
                                    cmd.Parameters.AddWithValue("@lang", kvp.Key)
                                    cmd.Parameters.AddWithValue("@name", kvp.Value.Trim())
                                    cmd.ExecuteNonQuery()
                                End Using
                            End If
                        Next
                    End If

                    ' Save images
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

                    ' Create the batch
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

    Public Shared Function SuspendUser(userId As Integer) As Boolean
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "UPDATE Users SET Status = 'Suspended' WHERE UserID = @userId AND Role != 'Admin'"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@userId", userId)
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    Public Shared Function ReactivateUser(userId As Integer) As Boolean
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "UPDATE Users SET Status = 'Active' WHERE UserID = @userId"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@userId", userId)
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    Public Shared Function ResetUserPassword(userId As Integer, newPass As String) As Boolean
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "UPDATE Users SET PasswordHash = @hash WHERE UserID = @userId"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@hash", HashPassword(newPass))
                cmd.Parameters.AddWithValue("@userId", userId)
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function
    Public Shared Function GenerateTempPassword() As String
        ' Excludes I, O, 0, 1 to avoid confusion with L, 0, etc.
        Dim chars As String = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789"
        Dim rng As New Random()
        Dim sb As New StringBuilder("TMP-")
        For i As Integer = 0 To 5
            sb.Append(chars(rng.Next(chars.Length)))
        Next
        Return sb.ToString()
    End Function

    Public Shared Function ResetPasswordToTemporary(userId As Integer) As String
        Dim tempPassword As String = GenerateTempPassword()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            ' Ensure the column exists (migration for old DBs)
            Try
                Using cmd As New SqliteCommand("ALTER TABLE Users ADD COLUMN MustChangePassword INTEGER DEFAULT 0;", connection)
                    cmd.ExecuteNonQuery()
                End Using
            Catch
            End Try

            Dim query As String = "UPDATE Users SET PasswordHash = @hash, MustChangePassword = 1 WHERE UserID = @userId"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@hash", HashPassword(tempPassword))
                cmd.Parameters.AddWithValue("@userId", userId)
                If cmd.ExecuteNonQuery() > 0 Then Return tempPassword
            End Using
        End Using
        Return ""
    End Function

    Public Shared Function SetNewPasswordAndClearFlag(userId As Integer, newPass As String) As Boolean
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "UPDATE Users SET PasswordHash = @hash, MustChangePassword = 0 WHERE UserID = @userId"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@hash", HashPassword(newPass))
                cmd.Parameters.AddWithValue("@userId", userId)
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function


    Public Shared Function SaveFileToFolder(originalName As String, base64Content As String, folderName As String, extension As String) As String
        Try
            Dim bytes As Byte() = Convert.FromBase64String(base64Content)
            Dim fileName As String = Guid.NewGuid().ToString() & extension
            Dim folderPath As String = Path.Combine(Application.StartupPath, "Files", folderName)
            If Not Directory.Exists(folderPath) Then Directory.CreateDirectory(folderPath)
            File.WriteAllBytes(Path.Combine(folderPath, fileName), bytes)
            Return fileName
        Catch
            Return ""
        End Try
    End Function

    Public Shared Function ReadFileAsBase64(folderName As String, storedFileName As String) As String
        Try
            Dim fullPath As String = Path.Combine(Application.StartupPath, "Files", folderName, storedFileName)
            If Not File.Exists(fullPath) Then Return ""
            Dim bytes As Byte() = File.ReadAllBytes(fullPath)
            Return Convert.ToBase64String(bytes)
        Catch
            Return ""
        End Try
    End Function

    Public Shared Function UpdateUserProfilePic(userId As Integer, picPath As String) As Boolean
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "UPDATE Users SET ProfilePic = @pic WHERE UserID = @id"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@pic", picPath)
                cmd.Parameters.AddWithValue("@id", userId)
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    Public Shared Function GetSecurityQuestionList() As DataTable
        ' Self-healing: create table and seed if missing/empty
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()

            ' Ensure table exists
            Try
                Using cmd As New SqliteCommand("
                    CREATE TABLE IF NOT EXISTS SecurityQuestions (
                        QuestionID INTEGER PRIMARY KEY AUTOINCREMENT,
                        QuestionText TEXT NOT NULL UNIQUE
                    );", connection)
                    cmd.ExecuteNonQuery()
                End Using
            Catch
            End Try

            ' Seed if empty
            Try
                Using cmd As New SqliteCommand("SELECT COUNT(*) FROM SecurityQuestions", connection)
                    If Convert.ToInt32(cmd.ExecuteScalar()) = 0 Then
                        Dim seedSql As String = "
                            INSERT INTO SecurityQuestions (QuestionText) VALUES ('What is the name of your first school?');
                            INSERT INTO SecurityQuestions (QuestionText) VALUES ('What is the name of your first pet?');
                            INSERT INTO SecurityQuestions (QuestionText) VALUES ('In which town were you born?');"
                        Using seedCmd As New SqliteCommand(seedSql, connection)
                            seedCmd.ExecuteNonQuery()
                        End Using
                    End If
                End Using
            Catch
            End Try
        End Using

        ' Now read them back
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT QuestionID AS ID, QuestionText AS Text FROM SecurityQuestions ORDER BY QuestionID"
            Using cmd As New SqliteCommand(query, connection)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function GetSecurityQuestionByEmail(email As String) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "
                SELECT u.SecurityQuestionID AS QuestionID, IFNULL(sq.QuestionText, '') AS QuestionText
                FROM Users u
                LEFT JOIN SecurityQuestions sq ON u.SecurityQuestionID = sq.QuestionID
                WHERE u.Email = @email AND u.Status != 'Suspended'"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@email", email)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function ResetPasswordWithAnswer(email As String, answer As String, newPassword As String) As String
        ' Returns: "OK" | "NOT_FOUND" | "NO_QUESTION" | "BAD_ANSWER" | "ERROR"
        Try
            Using connection As New SqliteConnection(ConnectionString)
                connection.Open()

                Dim userId As Integer = 0
                Dim storedHash As String = ""
                Dim query As String = "SELECT UserID, IFNULL(SecurityAnswerHash, '') AS Hash FROM Users WHERE Email = @email AND Status != 'Suspended'"
                Using cmd As New SqliteCommand(query, connection)
                    cmd.Parameters.AddWithValue("@email", email)
                    Using reader As SqliteDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            userId = reader.GetInt32(0)
                            storedHash = reader.GetString(1)
                        Else
                            Return "NOT_FOUND"
                        End If
                    End Using
                End Using

                If String.IsNullOrEmpty(storedHash) Then Return "NO_QUESTION"
                If HashSecurityAnswer(answer) <> storedHash Then Return "BAD_ANSWER"

                Dim updateQuery As String = "UPDATE Users SET PasswordHash = @hash, MustChangePassword = 0 WHERE UserID = @id"
                Using cmd As New SqliteCommand(updateQuery, connection)
                    cmd.Parameters.AddWithValue("@hash", HashPassword(newPassword))
                    cmd.Parameters.AddWithValue("@id", userId)
                    cmd.ExecuteNonQuery()
                End Using

                Return "OK"
            End Using
        Catch
            Return "ERROR"
        End Try
    End Function

    ' === Chat Attachments: migrations + extended functions ===

    Public Shared Sub MigrateMessagesForAttachments()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Try
                Using m As New SqliteCommand("ALTER TABLE Messages ADD COLUMN MsgType TEXT DEFAULT 'Text';", connection)
                    m.ExecuteNonQuery()
                End Using
            Catch
            End Try
            Try
                Using m As New SqliteCommand("ALTER TABLE Messages ADD COLUMN FileName TEXT;", connection)
                    m.ExecuteNonQuery()
                End Using
            Catch
            End Try
            Try
                Using m As New SqliteCommand("ALTER TABLE Messages ADD COLUMN OriginalName TEXT;", connection)
                    m.ExecuteNonQuery()
                End Using
            Catch
            End Try
            Try
                Using m As New SqliteCommand("ALTER TABLE Messages ADD COLUMN FileSize INTEGER DEFAULT 0;", connection)
                    m.ExecuteNonQuery()
                End Using
            Catch
            End Try
            Try
                Using m As New SqliteCommand("ALTER TABLE Messages ADD COLUMN LocationText TEXT;", connection)
                    m.ExecuteNonQuery()
                End Using
            Catch
            End Try
        End Using
    End Sub

    Private Shared Function GetSafeExtension(originalName As String) As String
        If String.IsNullOrEmpty(originalName) Then Return ""
        Dim idx As Integer = originalName.LastIndexOf("."c)
        If idx < 0 Then Return ""
        Dim ext As String = originalName.Substring(idx).ToLower()
        Select Case ext
            Case ".jpg", ".jpeg", ".png", ".pdf", ".doc", ".docx", ".xls", ".xlsx"
                Return ext
            Case Else
                Return ""
        End Select
    End Function

    Public Shared Function SendMessageFull(convId As Integer, senderId As Integer, msgType As String, body As String, originalName As String, fileSize As Integer, locationText As String, base64Content As String) As Boolean
        Dim storedFileName As String = ""

        ' Ensure the extended columns exist before inserting
        MigrateMessagesForAttachments()

        If Not String.IsNullOrEmpty(base64Content) AndAlso (msgType = "Image" OrElse msgType = "File") Then
            Try
                Dim ext As String = GetSafeExtension(originalName)
                If String.IsNullOrEmpty(ext) Then
                    If msgType = "Image" Then
                        ext = ".jpg"
                    Else
                        Return False
                    End If
                End If
                storedFileName = SaveFileToFolder(originalName, base64Content, "chat", ext)
                If String.IsNullOrEmpty(storedFileName) Then Return False
            Catch
                Return False
            End Try
        End If

        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "INSERT INTO Messages (ConvID, SenderID, MsgType, Body, FileName, OriginalName, FileSize, LocationText, SentAt) VALUES (@convId, @senderId, @msgType, @body, @fileName, @origName, @fileSize, @locText, @date)"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@convId", convId)
                cmd.Parameters.AddWithValue("@senderId", senderId)
                cmd.Parameters.AddWithValue("@msgType", msgType)
                cmd.Parameters.AddWithValue("@body", If(String.IsNullOrEmpty(body), "", body))
                cmd.Parameters.AddWithValue("@fileName", If(String.IsNullOrEmpty(storedFileName), "", storedFileName))
                cmd.Parameters.AddWithValue("@origName", If(String.IsNullOrEmpty(originalName), "", originalName))
                cmd.Parameters.AddWithValue("@fileSize", fileSize)
                cmd.Parameters.AddWithValue("@locText", If(String.IsNullOrEmpty(locationText), "", locationText))
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    Public Shared Function GetChatHistoryFull(convId As Integer) As DataTable
        Dim dt As New DataTable()

        ' Ensure columns exist
        MigrateMessagesForAttachments()

        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "
                SELECT m.MessageID, m.SenderID, u.DisplayName AS SenderName, 
                       IFNULL(m.MsgType, 'Text') AS MsgType, IFNULL(m.Body, '') AS Body,
                       IFNULL(m.FileName, '') AS FileName, IFNULL(m.OriginalName, '') AS OriginalName,
                       IFNULL(m.FileSize, 0) AS FileSize, IFNULL(m.LocationText, '') AS LocationText,
                       m.SentAt
                FROM Messages m
                JOIN Users u ON m.SenderID = u.UserID
                WHERE m.ConvID = @convId 
                ORDER BY m.MessageID ASC"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@convId", convId)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ' === Chat Blocks & Reports ===

    Public Shared Sub MigrateChatBlocksAndReports()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Try
                Using m As New SqliteCommand("
                    CREATE TABLE IF NOT EXISTS ChatBlocks (
                        BlockerID INTEGER NOT NULL,
                        BlockedID INTEGER NOT NULL,
                        CreatedAt TEXT NOT NULL,
                        PRIMARY KEY (BlockerID, BlockedID)
                    );", connection)
                    m.ExecuteNonQuery()
                End Using
            Catch
            End Try
            Try
                Using m As New SqliteCommand("
                    CREATE TABLE IF NOT EXISTS ChatReports (
                        ReportID INTEGER PRIMARY KEY AUTOINCREMENT,
                        ConvID INTEGER NOT NULL,
                        ReportedBy INTEGER NOT NULL,
                        ReportedUserID INTEGER NOT NULL,
                        Reason TEXT NOT NULL,
                        Status TEXT DEFAULT 'Open',
                        CreatedAt TEXT NOT NULL,
                        ReviewedBy INTEGER,
                        ReviewedAt TEXT,
                        AdminNote TEXT
                    );", connection)
                    m.ExecuteNonQuery()
                End Using
            Catch
            End Try
        End Using
    End Sub

    Public Shared Function BlockUser(blockerId As Integer, blockedId As Integer) As Boolean
        MigrateChatBlocksAndReports()
        Try
            Using connection As New SqliteConnection(ConnectionString)
                connection.Open()
                Dim query As String = "INSERT OR IGNORE INTO ChatBlocks (BlockerID, BlockedID, CreatedAt) VALUES (@blocker, @blocked, @date)"
                Using cmd As New SqliteCommand(query, connection)
                    cmd.Parameters.AddWithValue("@blocker", blockerId)
                    cmd.Parameters.AddWithValue("@blocked", blockedId)
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    Return cmd.ExecuteNonQuery() >= 0
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function

    Public Shared Function UnblockUser(blockerId As Integer, blockedId As Integer) As Boolean
        MigrateChatBlocksAndReports()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "DELETE FROM ChatBlocks WHERE BlockerID = @blocker AND BlockedID = @blocked"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@blocker", blockerId)
                cmd.Parameters.AddWithValue("@blocked", blockedId)
                cmd.ExecuteNonQuery()
                Return True
            End Using
        End Using
    End Function

    Public Shared Function IsBlockedByEither(userA As Integer, userB As Integer) As Boolean
        MigrateChatBlocksAndReports()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT COUNT(*) FROM ChatBlocks WHERE (BlockerID = @a AND BlockedID = @b) OR (BlockerID = @b AND BlockedID = @a)"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@a", userA)
                cmd.Parameters.AddWithValue("@b", userB)
                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    Public Shared Function HasUserBlocked(blockerId As Integer, blockedId As Integer) As Boolean
        MigrateChatBlocksAndReports()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT COUNT(*) FROM ChatBlocks WHERE BlockerID = @blocker AND BlockedID = @blocked"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@blocker", blockerId)
                cmd.Parameters.AddWithValue("@blocked", blockedId)
                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    Public Shared Function ReportChat(convId As Integer, reportedBy As Integer, reportedUserId As Integer, reason As String) As Integer
        MigrateChatBlocksAndReports()
        Try
            Using connection As New SqliteConnection(ConnectionString)
                connection.Open()
                Dim query As String = "INSERT INTO ChatReports (ConvID, ReportedBy, ReportedUserID, Reason, Status, CreatedAt) VALUES (@conv, @by, @target, @reason, 'Open', @date); SELECT last_insert_rowid();"
                Using cmd As New SqliteCommand(query, connection)
                    cmd.Parameters.AddWithValue("@conv", convId)
                    cmd.Parameters.AddWithValue("@by", reportedBy)
                    cmd.Parameters.AddWithValue("@target", reportedUserId)
                    cmd.Parameters.AddWithValue("@reason", reason)
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        Catch
            Return 0
        End Try
    End Function

    Public Shared Function GetChatReports() As DataTable
        MigrateChatBlocksAndReports()
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "
                SELECT r.ReportID, r.ConvID,
                       ur.DisplayName AS ReporterName,
                       ut.DisplayName AS ReportedName,
                       ut.UserID AS ReportedUserID,
                       r.Reason, r.Status, r.CreatedAt
                FROM ChatReports r
                JOIN Users ur ON r.ReportedBy = ur.UserID
                JOIN Users ut ON r.ReportedUserID = ut.UserID
                ORDER BY r.ReportID DESC"
            Using cmd As New SqliteCommand(query, connection)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function UpdateReportStatus(reportId As Integer, newStatus As String, reviewedBy As Integer, adminNote As String) As Boolean
        MigrateChatBlocksAndReports()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "UPDATE ChatReports SET Status = @status, ReviewedBy = @by, ReviewedAt = @date, AdminNote = @note WHERE ReportID = @id"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@status", newStatus)
                cmd.Parameters.AddWithValue("@by", reviewedBy)
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.Parameters.AddWithValue("@note", If(String.IsNullOrEmpty(adminNote), "", adminNote))
                cmd.Parameters.AddWithValue("@id", reportId)
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function
    ' ============ PHASE A-2: Helper functions for new tables ============

    ' --- Languages ---
    Public Shared Function GetLanguages() As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Using cmd As New SqliteCommand("SELECT LanguageID AS ID, LanguageName AS Name FROM Languages WHERE Active = 1 ORDER BY SortOrder", connection)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ' --- ItemLocalNames ---
    Public Shared Function GetItemLocalNames(itemId As Integer) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "
                SELECT l.LanguageID, l.LanguageName, IFNULL(n.LocalName, '') AS LocalName
                FROM Languages l
                LEFT JOIN ItemLocalNames n ON l.LanguageID = n.LanguageID AND n.ItemID = @itemId
                WHERE l.Active = 1
                ORDER BY l.SortOrder"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@itemId", itemId)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Sub SaveItemLocalNames(itemId As Integer, names As Dictionary(Of Integer, String))
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Using transaction As SqliteTransaction = connection.BeginTransaction()
                Try
                    Using cmd As New SqliteCommand("DELETE FROM ItemLocalNames WHERE ItemID = @id", connection)
                        cmd.Transaction = transaction
                        cmd.Parameters.AddWithValue("@id", itemId)
                        cmd.ExecuteNonQuery()
                    End Using
                    For Each kvp In names
                        If Not String.IsNullOrWhiteSpace(kvp.Value) Then
                            Using cmd As New SqliteCommand("INSERT INTO ItemLocalNames (ItemID, LanguageID, LocalName) VALUES (@id, @lang, @name)", connection)
                                cmd.Transaction = transaction
                                cmd.Parameters.AddWithValue("@id", itemId)
                                cmd.Parameters.AddWithValue("@lang", kvp.Key)
                                cmd.Parameters.AddWithValue("@name", kvp.Value.Trim())
                                cmd.ExecuteNonQuery()
                            End Using
                        End If
                    Next
                    transaction.Commit()
                Catch
                    transaction.Rollback()
                End Try
            End Using
        End Using
    End Sub

    ' --- RejectionReasons ---
    Public Shared Function GetRejectionReasons(appliesTo As String) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT ReasonID, ReasonText, RequiresComment FROM RejectionReasons WHERE Active = 1 AND (AppliesTo = @a OR AppliesTo = 'Both') ORDER BY ReasonID"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@a", appliesTo)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ' --- CategoryUnits ---
    Public Shared Function GetCategoryIdByName(categoryName As String) As Integer
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Using cmd As New SqliteCommand("SELECT CategoryID FROM Categories WHERE CategoryName = @n", connection)
                cmd.Parameters.AddWithValue("@n", categoryName)
                Dim r As Object = cmd.ExecuteScalar()
                If r IsNot Nothing Then Return Convert.ToInt32(r)
            End Using
        End Using
        Return 0
    End Function

    Public Shared Function GetUnitsForCategory(categoryId As Integer) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "
                SELECT u.UnitID AS ID, u.UnitName AS Name
                FROM Units u
                JOIN CategoryUnits cu ON u.UnitID = cu.UnitID
                WHERE cu.CategoryID = @cat AND u.Active = 1
                ORDER BY u.UnitID"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@cat", categoryId)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ' --- Settings ---
    Public Shared Function GetSetting(key As String, defaultValue As String) As String
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Using cmd As New SqliteCommand("SELECT SettingValue FROM Settings WHERE SettingKey = @k", connection)
                cmd.Parameters.AddWithValue("@k", key)
                Dim r As Object = cmd.ExecuteScalar()
                If r IsNot Nothing AndAlso r IsNot DBNull.Value Then Return r.ToString()
            End Using
        End Using
        Return defaultValue
    End Function

    Public Shared Function GetSettingInt(key As String, defaultValue As Integer) As Integer
        Dim s As String = GetSetting(key, defaultValue.ToString())
        Dim result As Integer
        If Integer.TryParse(s, result) Then Return result
        Return defaultValue
    End Function

    Public Shared Function GetSettingDouble(key As String, defaultValue As Double) As Double
        Dim s As String = GetSetting(key, defaultValue.ToString())
        Dim result As Double
        If Double.TryParse(s, result) Then Return result
        Return defaultValue
    End Function

    Public Shared Sub SetSetting(key As String, value As String)
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Using cmd As New SqliteCommand("INSERT OR REPLACE INTO Settings (SettingKey, SettingValue) VALUES (@k, @v)", connection)
                cmd.Parameters.AddWithValue("@k", key)
                cmd.Parameters.AddWithValue("@v", value)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Shared Function GetAllSettings() As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Using cmd As New SqliteCommand("SELECT SettingKey, SettingValue, IFNULL(Description, '') AS Description FROM Settings ORDER BY SettingKey", connection)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ' --- Sessions ---
    Public Shared Function CreateSession(userId As Integer, token As String, ip As String) As Integer
        Try
            Using connection As New SqliteConnection(ConnectionString)
                connection.Open()
                Dim q As String = "INSERT INTO Sessions (UserID, Token, IPAddress, LoginAt, LastActivityAt) VALUES (@u, @t, @ip, @login, @login); SELECT last_insert_rowid();"
                Using cmd As New SqliteCommand(q, connection)
                    cmd.Parameters.AddWithValue("@u", userId)
                    cmd.Parameters.AddWithValue("@t", If(String.IsNullOrEmpty(token), "", token))
                    cmd.Parameters.AddWithValue("@ip", If(String.IsNullOrEmpty(ip), "", ip))
                    cmd.Parameters.AddWithValue("@login", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        Catch
            Return 0
        End Try
    End Function

    Public Shared Sub UpdateSessionActivity(sessionId As Integer)
        If sessionId <= 0 Then Return
        Try
            Using connection As New SqliteConnection(ConnectionString)
                connection.Open()
                Using cmd As New SqliteCommand("UPDATE Sessions SET LastActivityAt = @d WHERE SessionID = @id", connection)
                    cmd.Parameters.AddWithValue("@d", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    cmd.Parameters.AddWithValue("@id", sessionId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch
        End Try
    End Sub

    Public Shared Sub EndSession(sessionId As Integer, reason As String)
        If sessionId <= 0 Then Return
        Try
            Using connection As New SqliteConnection(ConnectionString)
                connection.Open()
                Using cmd As New SqliteCommand("UPDATE Sessions SET LogoutAt = @d, EndReason = @r WHERE SessionID = @id AND LogoutAt IS NULL", connection)
                    cmd.Parameters.AddWithValue("@d", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    cmd.Parameters.AddWithValue("@r", If(String.IsNullOrEmpty(reason), "Logout", reason))
                    cmd.Parameters.AddWithValue("@id", sessionId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch
        End Try
    End Sub

    Public Shared Function GetSessionHistory(userId As Integer, limit As Integer) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "
                SELECT SessionID, LoginAt, IFNULL(LogoutAt, '') AS LogoutAt, IFNULL(EndReason, '') AS EndReason, IFNULL(IPAddress, '') AS IPAddress
                FROM Sessions
                WHERE UserID = @u
                ORDER BY SessionID DESC
                LIMIT @lim"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@u", userId)
                cmd.Parameters.AddWithValue("@lim", limit)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function GetTotalTimeOnline(userId As Integer) As Double
        Try
            Using connection As New SqliteConnection(ConnectionString)
                connection.Open()
                Dim query As String = "SELECT LoginAt, LogoutAt FROM Sessions WHERE UserID = @u"
                Using cmd As New SqliteCommand(query, connection)
                    cmd.Parameters.AddWithValue("@u", userId)
                    Dim totalSeconds As Double = 0
                    Using reader As SqliteDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim login As DateTime = Convert.ToDateTime(reader("LoginAt"))
                            Dim logout As DateTime = If(reader.IsDBNull(1), DateTime.Now, Convert.ToDateTime(reader("LogoutAt")))
                            totalSeconds += (logout - login).TotalSeconds
                        End While
                    End Using
                    Return totalSeconds
                End Using
            End Using
        Catch
            Return 0
        End Try
    End Function

    ' --- PerfSamples ---
    Public Shared Sub SavePerfSample(usersOnline As Integer, reqPerSec As Double, kbIn As Double, kbOut As Double, avgRespMs As Double, cpuPct As Double, memMB As Double)
        Try
            Using connection As New SqliteConnection(ConnectionString)
                connection.Open()
                Dim q As String = "INSERT INTO PerfSamples (SampledAt, UsersOnline, ReqPerSec, KBInPerSec, KBOutPerSec, AvgRespMs, CpuPct, MemMB) VALUES (@d, @uo, @rps, @kbi, @kbo, @resp, @cpu, @mem)"
                Using cmd As New SqliteCommand(q, connection)
                    cmd.Parameters.AddWithValue("@d", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    cmd.Parameters.AddWithValue("@uo", usersOnline)
                    cmd.Parameters.AddWithValue("@rps", reqPerSec)
                    cmd.Parameters.AddWithValue("@kbi", kbIn)
                    cmd.Parameters.AddWithValue("@kbo", kbOut)
                    cmd.Parameters.AddWithValue("@resp", avgRespMs)
                    cmd.Parameters.AddWithValue("@cpu", cpuPct)
                    cmd.Parameters.AddWithValue("@mem", memMB)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch
        End Try
    End Sub

    Public Shared Function GetPerfSamples(limit As Integer) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "SELECT SampledAt, UsersOnline, ReqPerSec, CpuPct, MemMB, AvgRespMs FROM PerfSamples ORDER BY SampleID DESC LIMIT @lim"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@lim", limit)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ' --- OrderAllocations ---
    Public Shared Function SaveOrderAllocation(orderId As Integer, batchId As Integer, qty As Double) As Boolean
        Try
            Using connection As New SqliteConnection(ConnectionString)
                connection.Open()
                Using cmd As New SqliteCommand("INSERT INTO OrderAllocations (OrderID, BatchID, Qty) VALUES (@o, @b, @q)", connection)
                    cmd.Parameters.AddWithValue("@o", orderId)
                    cmd.Parameters.AddWithValue("@b", batchId)
                    cmd.Parameters.AddWithValue("@q", qty)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function

    Public Shared Function GetOrderAllocations(orderId As Integer) As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "
                SELECT a.AllocID, a.BatchID, b.BatchNo, a.Qty, b.ExpiryDate
                FROM OrderAllocations a
                JOIN Batches b ON a.BatchID = b.BatchID
                WHERE a.OrderID = @o
                ORDER BY a.AllocID"
            Using cmd As New SqliteCommand(query, connection)
                cmd.Parameters.AddWithValue("@o", orderId)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Sub DeleteOrderAllocations(orderId As Integer)
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Using cmd As New SqliteCommand("DELETE FROM OrderAllocations WHERE OrderID = @o", connection)
                cmd.Parameters.AddWithValue("@o", orderId)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Shared Function GetAllCategoryUnitLinks() As DataTable
        Dim dt As New DataTable()
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()
            Dim query As String = "
                SELECT c.CategoryName, u.UnitName
                FROM CategoryUnits cu
                JOIN Categories c ON cu.CategoryID = c.CategoryID
                JOIN Units u ON cu.UnitID = u.UnitID
                ORDER BY c.CategoryName, u.UnitID"
            Using cmd As New SqliteCommand(query, connection)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

End Class