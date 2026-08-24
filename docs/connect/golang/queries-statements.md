---
title: "go-mssqldb Queries and Statements"
description: "Run queries, execute statements, handle multiple result sets, and use transactions with the go-mssqldb driver."
author: dlevy-msft
ms.author: dlevy
ms.date: 07/13/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Queries and statements with go-mssqldb

The `go-mssqldb` driver uses the standard `database/sql` interface for running queries and executing statements. This article covers common patterns for data access with the driver.

## Run a SELECT query

Use `QueryContext` to execute a query that returns rows:

```go
rows, err := db.QueryContext(ctx,
    "SELECT BusinessEntityID, FirstName + ' ' + LastName AS Name, CountryRegionName FROM Sales.vSalesPerson WHERE CountryRegionName = @p1",
    sql.Named("p1", "Australia"))
if err != nil {
    log.Fatal(err)
}
defer rows.Close()

for rows.Next() {
    var id int
    var name, location string
    if err := rows.Scan(&id, &name, &location); err != nil {
        log.Fatal(err)
    }
    fmt.Printf("%d: %s (%s)\n", id, name, location)
}
if err = rows.Err(); err != nil {
    log.Fatal(err)
}
```

> [!IMPORTANT]
> Always call `rows.Close()` (typically with `defer`) and check `rows.Err()` after the loop. Failing to close rows can leak connections from the pool. `rows.Close()` can also return a server-side error while the driver drains remaining tokens, so don't ignore it when the result set isn't fully consumed.

If you stop reading early, close the rows explicitly and handle the close error:

```go
rows, err := db.QueryContext(ctx,
    "SELECT TOP (100) ProductID, Name FROM Production.Product ORDER BY ProductID")
if err != nil {
    log.Fatal(err)
}

for rows.Next() {
    var id int
    var name string
    if err := rows.Scan(&id, &name); err != nil {
        _ = rows.Close()
        log.Fatal(err)
    }

    fmt.Printf("%d %s\n", id, name)
    break // Stop early for demonstration.
}

if err := rows.Close(); err != nil {
    log.Fatal(err)
}
if err := rows.Err(); err != nil {
    log.Fatal(err)
}
```

Examples in this article run against the [AdventureWorks2025](../../samples/adventureworks-install-configure.md) sample database. Read-oriented examples query built-in objects such as `Sales.vSalesPerson`, `Production.Product`, and `Sales.SalesOrderHeader`. Write-oriented examples target `HumanResources.Department` and `Production.ProductInventory`.

## Query a single row

Use `QueryRowContext` when you expect exactly one row:

```go
var id int
var name string
err := db.QueryRowContext(ctx,
    "SELECT BusinessEntityID, FirstName + ' ' + LastName AS Name FROM Sales.vSalesPerson WHERE BusinessEntityID = @p1",
    sql.Named("p1", 280)).Scan(&id, &name)
if err == sql.ErrNoRows {
    fmt.Println("No employee found.")
} else if err != nil {
    log.Fatal(err)
} else {
    fmt.Printf("Employee %d: %s\n", id, name)
}
```

## Execute a statement

Use `ExecContext` for `INSERT`, `UPDATE`, `DELETE`, and DDL statements:

```go
result, err := db.ExecContext(ctx,
    "INSERT INTO HumanResources.Department (Name, GroupName) VALUES (@p1, @p2)",
    sql.Named("p1", "Data Science"),
    sql.Named("p2", "Research and Development"))
if err != nil {
    log.Fatal(err)
}

rowsAffected, _ := result.RowsAffected()
fmt.Printf("Rows affected: %d\n", rowsAffected)
```

> [!IMPORTANT]
> The `go-mssqldb` driver doesn't support `LastInsertId()`. Calling it returns an error. Use an `OUTPUT` clause or a separate `SELECT SCOPE_IDENTITY()` query to retrieve an inserted identity value.

If you use `SELECT SCOPE_IDENTITY()`, run it in the same batch or transaction as the `INSERT` so the identity scope stays on the same connection.

- **Stored procedures:** When every statement runs inside a procedure that uses `SET NOCOUNT ON`, `RowsAffected()` returns 0 because no row count is sent. To get a count, remove `SET NOCOUNT ON`, or return the count through an `OUTPUT` parameter or `SELECT` statement.
- **Triggers:** `RowsAffected()` includes rows affected by `AFTER` triggers that don't use `SET NOCOUNT ON`. A single-row `UPDATE` on a table whose trigger writes two audit rows returns 3, not 1. Adding `SET NOCOUNT ON` to the trigger body excludes the trigger's rows and returns 1. The outer statement's count is still reported. Don't remove `SET NOCOUNT ON` from a trigger to correct a row count; removing it causes the inflated number.
- **Multi-statement batches:** `ExecContext(ctx, "UPDATE ...; UPDATE ...")` returns the sum of all counted statements, not the last statement's count. Use separate `ExecContext` calls to get a per-statement count.

This behavior matches [`SqlCommand.ExecuteNonQuery`](/dotnet/api/microsoft.data.sqlclient.sqlcommand.executenonquery), whose return value includes rows affected by the insert or update operation and by any triggers.

## Parameterized queries

Always use parameterized queries to avoid SQL injection. The driver supports both positional and named parameters.

> [!IMPORTANT]
> The `go-mssqldb` driver uses `@p1`, `@p2`, and so on for positional parameters and `sql.Named()` for named parameters. The `?` placeholder syntax that some other drivers use (such as MySQL's `go-sql-driver`) doesn't work with the `sqlserver` driver name. If you're migrating from another database, replace all `?` or `$1`-style placeholders with `@p1`-style or named parameters.

### Positional parameters

Use `@p1`, `@p2` placeholders and pass values in order:

```go
rows, err := db.QueryContext(ctx,
    "SELECT BusinessEntityID, FirstName, CountryRegionName FROM Sales.vSalesPerson WHERE FirstName = @p1 AND CountryRegionName = @p2",
    "Jared", "Australia")
```

### Named parameters

Use `sql.Named()` to bind values to named placeholders:

```go
rows, err := db.QueryContext(ctx,
    "SELECT BusinessEntityID, FirstName, CountryRegionName FROM Sales.vSalesPerson WHERE FirstName = @name AND CountryRegionName = @location",
    sql.Named("name", "Jared"),
    sql.Named("location", "Australia"))
```

## Multiple result sets

Use `rows.NextResultSet()` to iterate through multiple result sets that a single batch or stored procedure returns.

> [!IMPORTANT]
> You must fully exhaust `rows.Next()` for each result set before calling `rows.NextResultSet()`. Calling `NextResultSet()` before `Next()` returns `false` and skips the remaining rows silently.

Use this loop pattern to process all result sets reliably:

```go
rows, err := db.QueryContext(ctx,
    `SELECT TOP (3) ProductID, Name
     FROM Production.Product
     ORDER BY ProductID;

    SELECT TOP (3) SalesOrderID, CONVERT(NVARCHAR(10), OrderDate, 23) AS OrderDate
     FROM Sales.SalesOrderHeader
     ORDER BY SalesOrderID DESC;`)
if err != nil {
    log.Fatal(err)
}
defer rows.Close()

setIndex := 0
for {
    switch setIndex {
    case 0:
        for rows.Next() {
            var productID int
            var productName string
            if err := rows.Scan(&productID, &productName); err != nil {
                log.Fatal(err)
            }
            fmt.Printf("Product %d: %s\n", productID, productName)
        }
    case 1:
        for rows.Next() {
            var salesOrderID int
            var orderDate string
            if err := rows.Scan(&salesOrderID, &orderDate); err != nil {
                log.Fatal(err)
            }
            fmt.Printf("Order %d: %s\n", salesOrderID, orderDate)
        }
    }

    if err := rows.Err(); err != nil {
        log.Fatal(err)
    }
    if !rows.NextResultSet() {
        break
    }
    setIndex++
}
```

## Transactions

Use `BeginTx` to start a transaction with a specific isolation level. For comprehensive transaction guidance, including isolation levels, savepoints, deadlock handling, and retry patterns, see [Transactions](transactions.md).

```go
tx, err := db.BeginTx(ctx, &sql.TxOptions{
    Isolation: sql.LevelSerializable,
})
if err != nil {
    log.Fatal(err)
}
defer tx.Rollback()

// Subtract from source location.
_, err = tx.ExecContext(ctx,
    "UPDATE Production.ProductInventory SET Quantity = Quantity - @p1 WHERE ProductID = @p2 AND LocationID = 1",
    sql.Named("p1", 5),
    sql.Named("p2", 1))
if err != nil {
    log.Fatal(err)
}

// Add to destination location.
_, err = tx.ExecContext(ctx,
    "UPDATE Production.ProductInventory SET Quantity = Quantity + @p1 WHERE ProductID = @p2 AND LocationID = 6",
    sql.Named("p1", 5),
    sql.Named("p2", 1))
if err != nil {
    log.Fatal(err)
}

if err = tx.Commit(); err != nil {
    log.Fatal(err)
}
```

## Get inserted identity values

The `go-mssqldb` driver doesn't support `LastInsertId()`. Use the `OUTPUT` clause to retrieve the identity value in the same statement:

```go
var newID int64
err := db.QueryRowContext(ctx,
    "INSERT INTO HumanResources.Department (Name, GroupName) OUTPUT INSERTED.DepartmentID VALUES (@name, @grp)",
    sql.Named("name", "Data Science"),
    sql.Named("grp", "Research and Development")).Scan(&newID)
if err != nil {
    log.Fatal(err)
}
fmt.Printf("Inserted department with ID: %d\n", newID)
```

For multiple rows:

```go
rows, err := db.QueryContext(ctx, `
    INSERT INTO HumanResources.Department (Name, GroupName)
    OUTPUT INSERTED.DepartmentID, INSERTED.Name
    VALUES (@n1, @g1), (@n2, @g2)`,
    sql.Named("n1", "Data Science"), sql.Named("g1", "Research and Development"),
    sql.Named("n2", "Cloud Ops"), sql.Named("g2", "Information Technology"))
if err != nil {
    log.Fatal(err)
}
defer rows.Close()

for rows.Next() {
    var id int64
    var name string
    if err := rows.Scan(&id, &name); err != nil {
        log.Fatal(err)
    }
    fmt.Printf("Inserted: %d - %s\n", id, name)
}
```

## Pagination

Use `OFFSET` and `FETCH NEXT` for server-side pagination. An `ORDER BY` clause is required:

### Offset-based pagination

Pass the offset and page size as parameters:

```go
func getEmployeesPage(ctx context.Context, db *sql.DB, page, pageSize int) ([]Employee, error) {
    offset := (page - 1) * pageSize
    rows, err := db.QueryContext(ctx, `
        SELECT BusinessEntityID, FirstName + ' ' + LastName AS Name, CountryRegionName AS Location
        FROM Sales.vSalesPerson
        ORDER BY BusinessEntityID
        OFFSET @offset ROWS
        FETCH NEXT @pageSize ROWS ONLY`,
        sql.Named("offset", offset),
        sql.Named("pageSize", pageSize))
    if err != nil {
        return nil, err
    }
    defer rows.Close()

    var employees []Employee
    for rows.Next() {
        var e Employee
        if err := rows.Scan(&e.Id, &e.Name, &e.Location); err != nil {
            return nil, err
        }
        employees = append(employees, e)
    }
    return employees, rows.Err()
}
```

### Keyset pagination for large tables

Offset pagination becomes slow on large tables because the server must skip rows. Keyset pagination uses the last seen key to fetch the next page efficiently:

```go
func getNextPage(ctx context.Context, db *sql.DB, lastID int, pageSize int) ([]Employee, error) {
    rows, err := db.QueryContext(ctx, `
        SELECT TOP(@pageSize) BusinessEntityID, FirstName + ' ' + LastName AS Name, CountryRegionName AS Location
        FROM Sales.vSalesPerson
        WHERE BusinessEntityID > @lastID
        ORDER BY BusinessEntityID`,
        sql.Named("pageSize", pageSize),
        sql.Named("lastID", lastID))
    if err != nil {
        return nil, err
    }
    defer rows.Close()

    var employees []Employee
    for rows.Next() {
        var e Employee
        if err := rows.Scan(&e.Id, &e.Name, &e.Location); err != nil {
            return nil, err
        }
        employees = append(employees, e)
    }
    return employees, rows.Err()
}
```

> [!TIP]
> Keyset pagination is significantly faster than `OFFSET`/`FETCH` for deep pages (page 1000+) because it uses an index seek instead of scanning and skipping rows.

## Batch multiple statements

Send multiple SQL statements in a single call to reduce network round trips.

```go
rows, err := db.QueryContext(ctx, `
    SELECT COUNT(*) FROM HumanResources.Employee;
    SELECT COUNT(*) FROM Sales.SalesOrderHeader;
    SELECT COUNT(*) FROM Production.Product;`)
if err != nil {
    log.Fatal(err)
}
defer rows.Close()

var empCount, orderCount, productCount int

if rows.Next() {
    if err := rows.Scan(&empCount); err != nil {
        log.Fatal(err)
    }
}

if rows.NextResultSet() && rows.Next() {
    if err := rows.Scan(&orderCount); err != nil {
        log.Fatal(err)
    }
}

if rows.NextResultSet() && rows.Next() {
    if err := rows.Scan(&productCount); err != nil {
        log.Fatal(err)
    }
}

if err := rows.Err(); err != nil {
    log.Fatal(err)
}
fmt.Printf("Employees: %d, Orders: %d, Products: %d\n",
    empCount, orderCount, productCount)
```

## Process large result sets efficiently

For queries that return millions of rows, process results in a streaming fashion. Don't accumulate all rows in memory.

```go
func processLargeTable(ctx context.Context, db *sql.DB) error {
    rows, err := db.QueryContext(ctx, "SELECT TransactionID, CONVERT(NVARCHAR(30), TransactionDate, 126) FROM Production.TransactionHistory")
    if err != nil {
        return err
    }
    defer rows.Close()

    var processed int
    for rows.Next() {
        var id int
        var data string
        if err := rows.Scan(&id, &data); err != nil {
            return err
        }

        // Process each row without accumulating.
        if err := handleRow(id, data); err != nil {
            return err
        }

        processed++
        if processed%10000 == 0 {
            log.Printf("Processed %d rows", processed)
        }
    }
    return rows.Err()
}
```

> [!CAUTION]
> An open `*sql.Rows` pins a connection from the pool until `rows.Close()` is called. For very long-running result set processing, consider breaking the work into ranges using keyset pagination to avoid holding a connection for minutes.

## Upsert with MERGE

SQL Server uses the `MERGE` statement for insert-or-update (upsert) operations.

```go
_, err := db.ExecContext(ctx, `
    MERGE HumanResources.Department AS target
    USING (SELECT @id AS DepartmentID, @name AS Name, @grp AS GroupName) AS source
    ON target.DepartmentID = source.DepartmentID
    WHEN MATCHED THEN
        UPDATE SET Name = source.Name, GroupName = source.GroupName
    WHEN NOT MATCHED THEN
        INSERT (Name, GroupName)
        VALUES (source.Name, source.GroupName);`,
    sql.Named("id", dept.Id),
    sql.Named("name", dept.Name),
    sql.Named("grp", dept.GroupName))
```

## Prepared statements

Use `PrepareContext` to create a reusable prepared statement. Prepared statements can improve performance when the same query runs many times with different parameters.

```go
stmt, err := db.PrepareContext(ctx,
    "SELECT TOP (1) FirstName + ' ' + LastName AS Name FROM Sales.vSalesPerson WHERE CountryRegionName = @p1")
if err != nil {
    log.Fatal(err)
}
defer stmt.Close()

for _, location := range []string{"Australia", "India", "Germany"} {
    var name string
    err := stmt.QueryRowContext(ctx, location).Scan(&name)
    if err != nil {
        log.Println(location, err)
        continue
    }
    fmt.Printf("%s: %s\n", location, name)
}
```

## Context cancellation

All `database/sql` methods accept a `context.Context`. Use it for timeouts and cancellation.

```go
ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
defer cancel()

rows, err := db.QueryContext(ctx, "SELECT * FROM Production.TransactionHistory")
```

If the context deadline expires, the driver cancels the query on the server and returns an error to the caller.

## Related content

- [Transactions with go-mssqldb](transactions.md)
- [Error handling and retry patterns with go-mssqldb](error-handling.md)
- [go-mssqldb data type mappings](data-type-mappings.md)
- [Stored procedures with go-mssqldb](stored-procedures.md)
- [JSON and XML data with go-mssqldb](json-xml-data.md)
- [Connection pooling with go-mssqldb](connection-pooling.md)
