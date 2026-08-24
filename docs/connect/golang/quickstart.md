---
title: "Quickstart: Connect and Query with go-mssqldb"
description: "Connect to SQL Server from a Go application and run a query using the go-mssqldb driver."
author: dlevy-msft
ms.author: dlevy
ms.date: 07/13/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: quickstart
ai-usage: ai-assisted
---
# Quickstart: Connect and query with go-mssqldb

In this quickstart, you connect to a SQL Server database from Go and run a query. You use the `go-mssqldb` driver with the standard `database/sql` package.

## Prerequisites

For the main sample in this article, you need:

- [Go 1.25 or later](https://go.dev/dl/)
- Access to a SQL Server-compatible endpoint. The main sample assumes a local SQL Server instance on Windows.
- The `AdventureWorks2025` sample database. For download and restore instructions, see [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md).

If you're connecting to Azure SQL instead of a local SQL Server instance, use [Use go-mssqldb with Azure SQL Database](azure-sql.md) for Azure-specific prerequisites and setup.

## Choose your starting point

The main sample in this article uses a local SQL Server instance on Windows with integrated authentication. Use the SQL authentication or Microsoft Entra ID variations later in this article if you're on Linux or macOS, or if you're connecting to Azure SQL.

- Continue in this article if you want the smallest end-to-end sample against SQL Server and you can create a local test database.
- If you're connecting with a SQL Server login instead of Windows authentication, continue in this article and use the SQL authentication variation later.
- If you're connecting to Azure SQL Database, Azure SQL Managed Instance, or SQL database in Fabric, start with [Use go-mssqldb with Azure SQL Database](azure-sql.md). That article covers Azure TLS requirements, connection limits, and passwordless authentication.
- If you're using Microsoft Entra ID, import `github.com/microsoft/go-mssqldb/azuread` and use the `azuresql` driver name. For credential flow details, see [Microsoft Entra ID authentication](entra-authentication.md).

## Create a Go project

1. Create and initialize a new directory:

   ```bash
   mkdir go-sql-quickstart
   cd go-sql-quickstart
   go mod init go-sql-quickstart
   ```

1. Install the driver:

   ```bash
   go get github.com/microsoft/go-mssqldb
   ```

## Connect and query

Create a file named `main.go` with the following Go code:

```go
package main

import (
    "context"
    "database/sql"
    "fmt"
    "log"

    _ "github.com/microsoft/go-mssqldb"
)

func main() {
    // Build the connection string.
    connString := "sqlserver://<server>?database=AdventureWorks2025&trusted_connection=yes"

    // Open and verify the connection.
    db, err := sql.Open("sqlserver", connString)
    if err != nil {
        log.Fatal("Error opening connection: ", err.Error())
    }
    defer db.Close()

    if err = db.PingContext(context.Background()); err != nil {
        log.Fatal("Error pinging database: ", err.Error())
    }
    fmt.Println("Connected to SQL Server.")

    // Run a SELECT query.
    rows, err := db.QueryContext(context.Background(),
        "SELECT DepartmentID, Name, GroupName FROM HumanResources.Department")
    if err != nil {
        log.Fatal("Error running query: ", err.Error())
    }
    defer rows.Close()

    // Iterate through the results.
    fmt.Println("\nDepartments:")
    for rows.Next() {
        var id int
        var name, groupName string
        if err := rows.Scan(&id, &name, &groupName); err != nil {
            log.Fatal("Error scanning row: ", err.Error())
        }
        fmt.Printf("  %d: %s (%s)\n", id, name, groupName)
    }
    if err = rows.Err(); err != nil {
        log.Fatal("Error after row iteration: ", err.Error())
    }
}
```

Run the application:

```bash
go run main.go
```

Expected output:

```output
Connected to SQL Server.

Departments:
  1: Engineering (Research and Development)
  2: Tool Design (Research and Development)
  3: Sales (Sales and Marketing)
  4: Marketing (Sales and Marketing)
  ...
```

## Insert a row

Add the following function to `main.go` and call it from `main()`:

```go
func insertDepartment(db *sql.DB, name, groupName string) (int64, error) {
    ctx := context.Background()
    result, err := db.ExecContext(ctx,
        "INSERT INTO HumanResources.Department (Name, GroupName) VALUES (@p1, @p2)",
        sql.Named("p1", name),
        sql.Named("p2", groupName))
    if err != nil {
        return 0, err
    }
    return result.RowsAffected()
}
```

> [!IMPORTANT]
> The `go-mssqldb` driver doesn't support `LastInsertId()`. Calling it returns an error. Use an `OUTPUT` clause or a separate `SELECT SCOPE_IDENTITY()` query to retrieve an inserted identity value.

## Use SQL authentication

To connect by using a SQL Server authentication instead of Windows authentication, change the connection string. For all connection string formats and options, see [Connection strings](connection-strings.md).

```go
connString := "sqlserver://<user>:<password>@<server>:1433?database=AdventureWorks2025"
```


## Use Microsoft Entra ID

If you're connecting to Azure SQL Database, Azure SQL Managed Instance, or SQL database in Fabric, start with [Use go-mssqldb with Azure SQL Database](azure-sql.md). The example in this section shows only the minimum code changes required in `main.go`.

To authenticate by using Microsoft Entra ID, import the `azuread` package:

```go
import (
    _ "github.com/microsoft/go-mssqldb/azuread"
)
```

Then use the `azuresql` driver name and add `fedauth` to the connection string. For all Microsoft Entra options, see [Microsoft Entra ID authentication](entra-authentication.md).

```go
db, err := sql.Open("azuresql", "sqlserver://<server>.database.windows.net?database=AdventureWorks2025&fedauth=ActiveDirectoryDefault&encrypt=true&TrustServerCertificate=false")
```


## Related content

- [Install the go-mssqldb driver](installation.md)
- [go-mssqldb connection strings](connection-strings.md)
- [Use go-mssqldb with Azure SQL Database](azure-sql.md)
- [Queries and statements with go-mssqldb](queries-statements.md)
