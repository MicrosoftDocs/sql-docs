---
title: "go-mssqldb Stored Procedures"
description: "Call stored procedures with the go-mssqldb driver, including output parameters and return status."
author: dlevy-msft
ms.author: dlevy
ms.date: 04/30/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Stored procedures with go-mssqldb

The `go-mssqldb` driver supports calling stored procedures with input parameters, output parameters, and return status values. This article covers the common patterns.

Most snippets in this article assume the usual `database/sql` setup is already in place, with `database/sql` imported as `sql`, a `ctx` value available, and `db` initialized. Snippets include import blocks only when they introduce additional packages such as `context`, `fmt`, `log`, `os`, or `github.com/microsoft/go-mssqldb`. When a code block intentionally continues the same example, use `=` for values that were already declared earlier in that example; use `:=` for fresh declarations in standalone snippets.

## Call a stored procedure

Use `ExecContext` or `QueryContext` with the procedure name directly:

```go
_, err := db.ExecContext(ctx, "dbo.uspGetEmployeeManagers",
    sql.Named("BusinessEntityID", 6))
```

Prefer the procedure name plus parameters for ordinary procedure calls. Use an explicit `EXECUTE ...` string only when you need to embed the call in a larger Transact-SQL (T-SQL) batch or use syntax that isn't represented by `sql.Named` parameters.

## Output parameters

Use `sql.Named` with `sql.Out` to receive output parameter values:

```go
import (
    "context"
    "database/sql"
    "fmt"
    "log"
)

ctx := context.Background()
var employeeCount int64
_, err := db.ExecContext(ctx, "dbo.GetEmployeeCount",
    sql.Named("count", sql.Out{Dest: &employeeCount}))
if err != nil {
    log.Fatal(err)
}
fmt.Println("Employee count:", employeeCount)
```

The corresponding T-SQL procedure:

```sql
CREATE PROCEDURE dbo.GetEmployeeCount
    @count INT OUTPUT
AS
BEGIN
    SELECT @count = COUNT(*) FROM HumanResources.Employee;
END;
```

## Input/output parameters

For parameters that are both input and output, set `In: true` on the `sql.Out` struct:

```go
var result int64 = 10
_, err := db.ExecContext(ctx, "dbo.DoubleValue",
    sql.Named("value", sql.Out{Dest: &result, In: true}))
if err != nil {
    log.Fatal(err)
}
fmt.Println("Doubled:", result)
```

## Return status

Use `mssql.ReturnStatus` to capture the integer return value of a stored procedure. This example uses the AdventureWorks `dbo.uspGetEmployeeManagers` procedure:

```go
import (
    "database/sql"
    "fmt"
    "log"

    mssql "github.com/microsoft/go-mssqldb"
)

var returnStatus mssql.ReturnStatus
rows, err := db.QueryContext(ctx, "dbo.uspGetEmployeeManagers",
    sql.Named("BusinessEntityID", 6),
    &returnStatus)
if err != nil {
    log.Fatal(err)
}
defer rows.Close()

for rows.Next() {
}
if err := rows.Err(); err != nil {
    log.Fatal(err)
}

fmt.Println("Return status:", returnStatus)
```

`ReturnStatus` works with `ExecContext` and `QueryContext`, but not with `QueryRowContext`.

You can combine `ReturnStatus` with procedure parameters. This example continues the previous example, so it reuses the `mssql` import shown earlier and uses the AdventureWorks `dbo.uspGetEmployeeManagers` procedure:

```go
var returnStatus mssql.ReturnStatus
rows, err := db.QueryContext(ctx, "dbo.uspGetEmployeeManagers",
    sql.Named("BusinessEntityID", 6),
    &returnStatus)
if err != nil {
    log.Fatal(err)
}
defer rows.Close()
```

## Result sets from stored procedures

If the stored procedure returns result sets, use `QueryContext`:

```go
rows, err := db.QueryContext(ctx, "dbo.uspGetEmployeeManagers", sql.Named("BusinessEntityID", 6))
if err != nil {
    log.Fatal(err)
}
defer rows.Close()

for rows.Next() {
    var reportingLevel int
    var employeeID int
    var employeeFirstName string
    var employeeLastName string
    var organizationNode string
    var managerFirstName string
    var managerLastName string
    if err := rows.Scan(&reportingLevel, &employeeID, &employeeFirstName, &employeeLastName, &organizationNode, &managerFirstName, &managerLastName); err != nil {
        log.Fatal(err)
    }
    fmt.Printf("Level %d: %d %s %s (manager: %s %s)\n",
        reportingLevel, employeeID, employeeFirstName, employeeLastName, managerFirstName, managerLastName)
    _ = organizationNode
}
```

For procedures that return multiple result sets, use `rows.NextResultSet()`. For more information, see [Queries and statements](queries-statements.md).

### Read all rows before using output parameters or return status

SQL Server sends output parameters and return status values after the result sets finish. If you read an output variable before consuming all rows, the value can still be incomplete.

This example continues the previous one and reuses the `mssql` import shown earlier.

It doesn't print the row data. The loop only consumes the result set so `returnStatus` is available after the query finishes.

```go
var returnStatus mssql.ReturnStatus
rows, err := db.QueryContext(ctx, "dbo.uspGetEmployeeManagers",
    sql.Named("BusinessEntityID", 6),
    &returnStatus)
if err != nil {
    log.Fatal(err)
}
defer rows.Close()

for rows.Next() {
    var reportingLevel int
    var employeeID int
    var employeeFirstName string
    var employeeLastName string
    var organizationNode string
    var managerFirstName string
    var managerLastName string
    if err := rows.Scan(&reportingLevel, &employeeID, &employeeFirstName, &employeeLastName, &organizationNode, &managerFirstName, &managerLastName); err != nil {
        log.Fatal(err)
    }
    _ = organizationNode
    _ = managerFirstName
    _ = managerLastName
}
if err := rows.Err(); err != nil {
    log.Fatal(err)
}

fmt.Println("Return status:", returnStatus)
```

Use `ExecContext` when the procedure doesn't return rows. If the procedure does return rows, wait until all rows and result sets are consumed before you read output parameters or return status.

## Temporary tables and stored procedures

When you create a temporary table and then query it in separate calls, the operations might run on different connections from the pool. Since temporary tables are scoped to a single connection, the second call might not see the table.

To work with temporary tables, use a single-connection approach:

```go
conn, err := db.Conn(ctx)
if err != nil {
    log.Fatal(err)
}
defer conn.Close()

_, err = conn.ExecContext(ctx, "CREATE TABLE #TempItems (Id INT, Name NVARCHAR(50))")
if err != nil {
    log.Fatal(err)
}

_, err = conn.ExecContext(ctx, "INSERT INTO #TempItems VALUES (1, N'Item A')")
if err != nil {
    log.Fatal(err)
}

rows, err := conn.QueryContext(ctx, "SELECT * FROM #TempItems")
if err != nil {
    log.Fatal(err)
}
defer rows.Close()
```

Alternatively, wrap the operations in a transaction, which pins them to the same connection automatically.

## Capture PRINT and RAISERROR messages

SQL Server `PRINT` statements and `RAISERROR` with severity 0-10 produce informational messages that aren't returned as Go errors. To capture these messages, enable the `log` connection parameter with flag `2` (messages):

```text
sqlserver://<user>:<password>@<server>?database=AdventureWorks2025&log=2
```

With logging enabled, the driver writes `PRINT` output and low-severity `RAISERROR` messages to Go's standard `log` package. To capture them programmatically, set a custom logger before opening the connection:

```go
import (
    "database/sql"
    "log"
    "os"

    mssql "github.com/microsoft/go-mssqldb"
)

// Direct driver messages to a custom logger.
mssql.SetLogger(log.New(os.Stdout, "mssql: ", log.LstdFlags))

db, err := sql.Open("sqlserver",
    "sqlserver://<user>:<password>@<server>?database=AdventureWorks2025&log=2")
```

Then any stored procedure that uses `PRINT` or `RAISERROR(..., 0, 1)` sends its messages to your logger.

> [!NOTE]
> `RAISERROR` with severity 11 or higher produces a Go error that you can handle with normal error checking. Only severity 0-10 messages require the `log` parameter to capture.

For more information about logging flags and programmatic log capture, see [Logging and diagnostics](logging-diagnostics.md).

## Related content

- [Queries and statements with go-mssqldb](queries-statements.md)
- [go-mssqldb data type mappings](data-type-mappings.md)
- [Table-valued parameters with go-mssqldb](table-valued-parameters.md)
