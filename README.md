# AgriMarket

AgriMarket is a client-server marketplace for fresh farm produce that addresses a common problem: small traders lose stock because batches are not tracked, buyers cannot reliably see available produce, orders are not recorded consistently, and communication is scattered. The solution is a networked desktop system with a server that holds the SQLite database, enforces business rules, and provides an administrator dashboard, plus a client application for traders and buyers.

## Features by Role

### Trader
- Register and manage a trader profile.
- Add produce items and stock in expiry-dated batches.
- Upload product and profile photos.
- View and manage stock using batch-level quantities.
- Review, confirm, reject, and complete incoming orders.
- Chat with buyers and receive notifications.

### Retailer/Wholesaler
- Browse available fresh produce and view item details.
- Place orders for available stock.
- Track order status from Pending through completion or cancellation.
- Chat with traders and receive push notifications.
- Manage a buyer profile and upload a profile photo.

### Administrator
- Approve or reject trader registrations.
- Manage users, items, categories, and other master lists.
- View dashboards, reports, orders, stock, and system activity.
- Monitor the server and enforce marketplace administration rules.

## System Architecture

The Server owns the SQLite database and business rules. Clients connect to the Server over TCP port 5000 for authentication, marketplace operations, chat, notifications, and file-related requests.

```text
+----------+       TCP : 5000       +----------+       SQLite       +----------+
| Client   | <--------------------> | Server   | <--------------> | Database |
| Trader/  |                         | Rules,   |                  |          |
| Buyer    |                         | Admin    |                  |          |
+----------+                         +----------+                  +----------+
```

## Technology Stack

| Technology | Use |
|---|---|
| VB.NET (.NET 10) | Application language and runtime |
| Windows Forms | Desktop user interface |
| SQLite via Microsoft.Data.Sqlite | Server-side data storage |
| System.Net.Sockets (TCP) | Client-server networking on port 5000 |
| SHA-256 hashing | Password hashing |
| Visual Studio 2026 | Development environment |

## Project Structure

- `AgriMarket/` — Client application for traders and buyers. Key files include `NetworkClient.vb`, `frmLogin.vb`, `frmRegister.vb`, `frmDashboard.vb`, and role-specific controls such as `ucBrowse.vb`, `ucMyStock.vb`, `ucMyOrders.vb`, and `ucChats.vb`.
- `AgriMarket.Server/` — Server application, SQLite data layer, business rules, and administration. Key files include `DatabaseHelper.vb`, `frmServerMain.vb`, `frmAdminLogin.vb`, and `frmAdminDashboard.vb`.
- `AgriMarket.slnx` — Visual Studio solution file.

## Setup & Installation

### Requirements
- .NET 10 SDK
- Visual Studio 2026 with the .NET desktop development workload
- Windows operating system for Windows Forms

### Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/IANZ2205/AgriMarket2.git
   cd AgriMarket2
   ```
2. Open `AgriMarket.slnx` in Visual Studio 2026.
3. Restore NuGet packages from Visual Studio or run `dotnet restore`.
4. Configure the solution to use multiple startup projects: start both `AgriMarket.Server` and `AgriMarket`.

## Running the Application

1. Start `AgriMarket.Server` first. It listens on TCP port `5000` and provides the default administrator account `admin@agrimarket.com` with password `password123`.
2. Start one or more `AgriMarket` client instances.
3. To run a second client instance in Visual Studio, use **Debug → Start New Instance** while the client project is selected.

## Business Rules

| Rule | Requirement |
|---|---|
| Batch number | Format: `<ItemCode>-<YYYYMMDD>-<NN>` |
| Expiry date | Calculated from the item or batch shelf-life rules and batch date |
| Stock allocation | First Expiry, First Out (FEFO) allocation |
| Reservation | Stock is reserved when an order is confirmed |
| Overselling | Orders cannot exceed available, unreserved stock |
| Trader approval | Trader accounts require administrator approval before trading |
| Order prices | The item price is snapshotted into each order |
| Images | JPG or PNG only, with a maximum size of 1 MB |

## Author

Makumbi Ian Mukisa (IANZ2205)  
Ndejje University  
Faculty of Science and Computing  
Course: BIT/BCS 3101 / BSE 3101 / BBC 2105
