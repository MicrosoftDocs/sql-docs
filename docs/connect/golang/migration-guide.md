---
title: "Migrate to go-mssqldb from Other Drivers"
description: "Migrate Go applications from lib/pq, pgx, go-sql-driver/mysql, and other database drivers to go-mssqldb for SQL Server."
author: dlevy-msft
ms.author: dlevy
ms.date: 06/23/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Migrate to go-mssqldb from other drivers

This guide helps Go developers migrate from PostgreSQL, MySQL, and other database drivers to `go-mssqldb` for SQL Server. It covers SQL syntax differences, driver-specific changes, and equivalent patterns for common operations.

> [!NOTE]
> Examples in this article run against the [AdventureWorks2025](../../samples/adventureworks-install-configure.md) sample database.

## Why migrate to SQL Server with go-mssqldb

| Feature | SQL Server with go-mssqldb |
| --- | --- |
| Enterprise security | Always Encrypted, row-level security, dynamic data masking, transparent data encryption (TDE). |
| Authentication | Microsoft Entra ID (formerly Azure AD), managed identity, Windows authentication, Kerberos. |
| Azure integration | Native Azure SQL Database support with automatic encryption, passwordless connections, and failover handling. |
| Performance | Columnstore indexes, in-memory OLTP, query store for automatic plan correction. |
| Data formats | Built-in JSON and XML support with server-side indexing and querying. |
| Tooling | SQL Server Management Studio (SSMS), Azure Data Studio, Visual Studio Code SQL extensions. |

## Migration considerations checklist

Review these items before you start porting queries line by line:

| Consideration | What to change |
| --- | --- |
| Placeholder syntax differs from many Go drivers. | Replace `?`, `$1`, and similar placeholders with `@name` or `@p1`, then pass `sql.Named()` arguments where appropriate. |
| `LastInsertId()` isn't supported. | Use `OUTPUT INSERTED.<column>` or `SELECT SCOPE_IDENTITY()` instead. |
| Go `string` values map to Unicode types by default. | Expect `nvarchar` semantics unless you choose a driver-specific type such as `mssql.VarChar` for non-Unicode data. |
| Temporary tables are scoped to the physical connection. | Keep create-and-use temp table logic on the same connection or inside the same transaction if later statements depend on that table. |
| Microsoft Entra authentication uses a different driver registration. | Import `github.com/microsoft/go-mssqldb/azuread`, use `sql.Open("azuresql", ...)`, and set Azure SQL TLS options explicitly with `encrypt=true&TrustServerCertificate=false`. |

For deeper guidance on these areas, see [Queries and statements](queries-statements.md), [Limitations](known-limitations.md), [Data type mappings](data-type-mappings.md), [Microsoft Entra ID authentication](entra-authentication.md), and [Troubleshooting](troubleshooting.md).

## Migrate from lib/pq (PostgreSQL)

### Change the import and driver name

Replace the `lib/pq` import and driver name with `go-mssqldb`:

```go
import _ "github.com/lib/pq"
db, err := sql.Open("postgres", connString)

// After (SQL Server with go-mssqldb):
import _ "github.com/microsoft/go-mssqldb"
db, err := sql.Open("sqlserver", connString)
```

### Change the connection string format

Convert the PostgreSQL key-value format to a SQL Server URL:

```go
// lib/pq connection string:
"host=<server> port=5432 user=<user> password=<password> dbname=AdventureWorks2025 sslmode=require"

// go-mssqldb connection string:
"sqlserver://<user>:<password>@<server>:1433?database=AdventureWorks2025&encrypt=true"
```

### Change parameter placeholders

PostgreSQL uses `$1`, `$2` positional placeholders. SQL Server uses named parameters with `@`:

```go
// PostgreSQL (lib/pq):
db.QueryContext(ctx, "SELECT * FROM users WHERE id = $1 AND status = $2", id, status)

// SQL Server (go-mssqldb):
db.QueryContext(ctx, "SELECT ProductID, Name FROM Production.Product WHERE ProductID = @id AND Color = @color",
    sql.Named("id", id),
    sql.Named("color", color))
```

### SQL syntax changes

| Operation | PostgreSQL | SQL Server |
| --- | --- | --- |
| Auto-increment | `SERIAL` or `GENERATED ALWAYS AS IDENTITY` | `IDENTITY(1,1)` |
| Get inserted ID | `RETURNING id` | `OUTPUT INSERTED.id` or `SELECT SCOPE_IDENTITY()` |
| Boolean type | `BOOLEAN` | `BIT` |
| String concatenation | `\|\|` | `+` or `CONCAT()` |
| Current timestamp | `NOW()` or `CURRENT_TIMESTAMP` | `GETUTCDATE()` or `SYSDATETIMEOFFSET()` |
| Limit rows | `LIMIT 10 OFFSET 20` | `OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY` |
| String type | `TEXT` or `VARCHAR` | `NVARCHAR(MAX)` or `NVARCHAR(n)` |
| JSON extraction | `column->>'key'` | `JSON_VALUE(column, '$.key')` |
| Upsert | `INSERT ... ON CONFLICT DO UPDATE` | `MERGE` statement |
| Array type | `INTEGER[]` | No native arrays. Use table-valued parameters. |
| Case sensitivity | Case-sensitive by default | Case-insensitive by default (depends on collation). |

### Get the inserted ID

Replace PostgreSQL's `RETURNING` clause with SQL Server's `OUTPUT` clause:

```go
var id int
err := db.QueryRowContext(ctx,
    "INSERT INTO users (name, email) VALUES ($1, $2) RETURNING id",
    name, email).Scan(&id)

// SQL Server (go-mssqldb) using OUTPUT:
var id int
err := db.QueryRowContext(ctx,
    "INSERT INTO HumanResources.Department (Name, GroupName) OUTPUT INSERTED.DepartmentID VALUES (@name, @groupName)",
    sql.Named("name", name),
    sql.Named("groupName", groupName)).Scan(&id)
```

### Pagination

Replace `LIMIT`/`OFFSET` syntax with `OFFSET`/`FETCH NEXT` syntax.

```go
// PostgreSQL:
"SELECT * FROM employees ORDER BY id LIMIT $1 OFFSET $2"

// SQL Server (requires ORDER BY):
"SELECT ProductID, Name, ListPrice FROM Production.Product ORDER BY ProductID OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY"
```

## Migrate from pgx (PostgreSQL)

The `pgx` driver uses its own connection pool and API that differ from `database/sql`. To migrate, switch to the standard `database/sql` interface.

### Change from pgx pool to database/sql

Replace the `pgxpool` API with the standard `database/sql` interface.

```go
// Before (pgx):
import "github.com/jackc/pgx/v5/pgxpool"
pool, err := pgxpool.New(ctx, "postgres://user:pass@<server>:5432/AdventureWorks2025")
defer pool.Close()
rows, err := pool.Query(ctx, "SELECT * FROM users WHERE id = $1", id)

// After (go-mssqldb with database/sql):
import (
    "database/sql"
    _ "github.com/microsoft/go-mssqldb"
)
db, err := sql.Open("sqlserver", "sqlserver://user:pass@<server>:1433?database=AdventureWorks2025")
defer db.Close()
rows, err := db.QueryContext(ctx,
    "SELECT ProductID, Name FROM Production.Product WHERE ProductID = @id", sql.Named("id", id))
```

### Replace pgx-specific features

| pgx feature | go-mssqldb equivalent |
| --- | --- |
| `pgx.CollectRows` | Manual `rows.Next()` and `rows.Scan()` loop. |
| `pgx.RowToStructByName` | Manual `rows.Scan` into struct fields. |
| Batch queries (`pgx.Batch`) | Multiple `ExecContext` calls or stored procedures. |
| `COPY FROM` for bulk insert | `mssql.CopyIn` for bulk insert. |
| `pgx.ConnConfig` | `msdsn.Config` or URL-based connection strings. |
| `pgxpool.Pool` | `sql.DB` with `SetMaxOpenConns` and `SetMaxIdleConns`. |

## Migrate from go-sql-driver/mysql (MySQL)

### Swap the MySQL import and driver name

Replace the MySQL import and driver name with `go-mssqldb`.

```go
// Before (MySQL):
import _ "github.com/go-sql-driver/mysql"
db, err := sql.Open("mysql", connString)

// After (SQL Server):
import _ "github.com/microsoft/go-mssqldb"
db, err := sql.Open("sqlserver", connString)
```

### Convert the MySQL DSN format

Convert the MySQL DSN format to a SQL Server URL.

```go
// MySQL DSN:
"user:password@tcp(<server>:3306)/AdventureWorks2025?tls=true"

// go-mssqldb URL:
"sqlserver://user:password@<server>:1433?database=AdventureWorks2025&encrypt=true"
```

### Replace question mark placeholders

MySQL uses `?` positional placeholders.

```go
// MySQL:
db.QueryContext(ctx, "SELECT * FROM users WHERE id = ? AND status = ?", id, status)

// SQL Server:
db.QueryContext(ctx, "SELECT ProductID, Name FROM Production.Product WHERE ProductID = @id AND Color = @color",
    sql.Named("id", id),
    sql.Named("color", color))
```

### Compare MySQL and SQL Server syntax

| Operation | MySQL | SQL Server |
| --- | --- | --- |
| Auto-increment | `AUTO_INCREMENT` | `IDENTITY(1,1)` |
| Get inserted ID | `LAST_INSERT_ID()` | `SCOPE_IDENTITY()` or `OUTPUT INSERTED.id` |
| Limit rows | `LIMIT 10 OFFSET 20` | `OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY` |
| Current timestamp | `NOW()` | `GETUTCDATE()` |
| If null | `IFNULL(expr, default)` | `ISNULL(expr, default)` or `COALESCE(expr, default)` |
| String length | `LENGTH(str)` | `LEN(str)` |
| Substring | `SUBSTRING(str, start, len)` | `SUBSTRING(str, start, len)` (same) |
| Date format | `DATE_FORMAT(d, '%Y-%m-%d')` | `FORMAT(d, 'yyyy-MM-dd')` or `CONVERT(VARCHAR, d, 23)` |
| Upsert | `INSERT ... ON DUPLICATE KEY UPDATE` | `MERGE` statement |
| Backtick quoting | `` `column` `` | `[column]` |

### Replace MySQL-specific syntax

| MySQL syntax | SQL Server equivalent |
| --- | --- |
| `AUTO_INCREMENT` | `IDENTITY(1,1)` |
| `LIMIT n OFFSET m` | `OFFSET m ROWS FETCH NEXT n ROWS ONLY` (requires `ORDER BY`) |

**AUTO_INCREMENT to IDENTITY:**

```sql
-- MySQL:
CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100)
);

-- SQL Server:
CREATE TABLE HumanResources.NewDepartment (
    DepartmentID SMALLINT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);
```

**LIMIT to OFFSET/FETCH:**

```sql
-- MySQL:
SELECT * FROM users LIMIT 10 OFFSET 20;

-- SQL Server:
SELECT ProductID, Name, ListPrice
FROM Production.Product
ORDER BY ProductID
OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY;
```

## Common migration patterns

### Get the last inserted identity

Use the `OUTPUT` clause or `SCOPE_IDENTITY()` to retrieve the inserted row's identity value:

```go
// Pattern 1: OUTPUT clause (recommended, works with batch inserts).
var id int64
err := db.QueryRowContext(ctx,
    "INSERT INTO HumanResources.Department (Name, GroupName) OUTPUT INSERTED.DepartmentID VALUES (@name, @groupName)",
    sql.Named("name", "Engineering"),
    sql.Named("groupName", "Research and Development")).Scan(&id)

// Pattern 2: SCOPE_IDENTITY (works with single-row inserts).
var id int64
err := db.QueryRowContext(ctx,
    "INSERT INTO HumanResources.Department (Name, GroupName) VALUES (@name, @groupName); SELECT SCOPE_IDENTITY()",
    sql.Named("name", "Engineering"),
    sql.Named("groupName", "Research and Development")).Scan(&id)
```

> [!TIP]
> Prefer the `OUTPUT` clause over `SCOPE_IDENTITY()`. The `OUTPUT` clause works with batch inserts and doesn't depend on statement ordering.

### Upsert (insert or update)

PostgreSQL's `ON CONFLICT` and MySQL's `ON DUPLICATE KEY UPDATE` map to SQL Server's `MERGE`:

```go
_, err := db.ExecContext(ctx, `
    MERGE HumanResources.Department AS target
    USING (SELECT @id AS DepartmentID, @name AS Name, @groupName AS GroupName) AS source
    ON target.DepartmentID = source.DepartmentID
    WHEN MATCHED THEN
        UPDATE SET Name = source.Name, GroupName = source.GroupName, ModifiedDate = GETDATE()
    WHEN NOT MATCHED THEN
        INSERT (Name, GroupName, ModifiedDate) VALUES (source.Name, source.GroupName, GETDATE());`,
    sql.Named("id", dept.ID),
    sql.Named("name", dept.Name),
    sql.Named("groupName", dept.GroupName))
```

### Bulk insert

Replace PostgreSQL's `COPY` or MySQL's `LOAD DATA` with `mssql.CopyIn`.

```go
import mssql "github.com/microsoft/go-mssqldb"

stmt, err := db.Prepare(mssql.CopyIn("HumanResources.Department", mssql.BulkOptions{}, "Name", "GroupName", "ModifiedDate"))
if err != nil {
    return err
}

for _, dept := range departments {
    _, err = stmt.Exec(dept.Name, dept.GroupName, time.Now())
    if err != nil {
        return err
    }
}

// Flush the buffer.
_, err = stmt.Exec()
if err != nil {
    return err
}
stmt.Close()
```

### Handle NULL values

All Go database drivers handle NULL values the same way because they use the `database/sql` types.

```go
var color sql.NullString
err := db.QueryRowContext(ctx,
    "SELECT Color FROM Production.Product WHERE ProductID = @id",
    sql.Named("id", 1)).Scan(&color)

if color.Valid {
    fmt.Println(color.String)
} else {
    fmt.Println("NULL")
}
```

### Transactions

All Go database drivers use the same `database/sql` transaction API.

```go
tx, err := db.BeginTx(ctx, nil)
if err != nil {
    return err
}
defer tx.Rollback()

_, err = tx.ExecContext(ctx,
    "UPDATE Production.ProductInventory SET Quantity = Quantity - @qty WHERE ProductID = @pid AND LocationID = @fromLoc",
    sql.Named("qty", qty),
    sql.Named("pid", productID),
    sql.Named("fromLoc", fromLocationID))
if err != nil {
    return err
}

_, err = tx.ExecContext(ctx,
    "UPDATE Production.ProductInventory SET Quantity = Quantity + @qty WHERE ProductID = @pid AND LocationID = @toLoc",
    sql.Named("qty", qty),
    sql.Named("pid", productID),
    sql.Named("toLoc", toLocationID))
if err != nil {
    return err
}

return tx.Commit()
```

## Migration checklist

| Step | Action |
| --- | --- |
| Install the driver | `go get github.com/microsoft/go-mssqldb` |
| Change imports | Replace the old driver import with `_ "github.com/microsoft/go-mssqldb"`. |
| Update connection strings | Change to SQL Server URL format: `sqlserver://user:pass@host?database=db`. |
| Replace parameter placeholders | Change `$1`/`$2` or `?` to `@name` with `sql.Named`. |
| Update SQL syntax | Change `LIMIT`/`OFFSET`, `RETURNING`, `NOW()`, `SERIAL`, and other database-specific SQL. |
| Replace bulk operations | Change `COPY` or `LOAD DATA` to `mssql.CopyIn`. |
| Update schema DDL | Change `SERIAL`/`AUTO_INCREMENT` to `IDENTITY`, `TEXT` to `NVARCHAR`, `BOOLEAN` to `BIT`. |
| Test all queries | Run your test suite against a SQL Server instance to catch syntax differences. |
| Configure encryption | Add `encrypt=true` (or rely on Azure auto-detection) for production. |
| Set up authentication | Configure Microsoft Entra ID for Azure, or SQL Server authentication for on-premises. |

## Related content

- [Quickstart](quickstart.md)
- [Installation](installation.md)
- [Connection strings](connection-strings.md)
- [Data type mappings](data-type-mappings.md)
- [Bulk operations](bulk-operations.md)
- [Table-valued parameters](table-valued-parameters.md)
