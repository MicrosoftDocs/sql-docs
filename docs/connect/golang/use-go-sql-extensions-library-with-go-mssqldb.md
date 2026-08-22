---
title: "Use sqlx with go-mssqldb"
description: "Use the sqlx library with the go-mssqldb driver for struct scanning, named parameters, and query helpers in Go."
author: dlevy-msft
ms.author: dlevy
ms.date: 05/01/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Use sqlx with go-mssqldb

[sqlx](https://github.com/jmoiron/sqlx) is a lightweight library that extends Go's `database/sql` with struct scanning, named parameter binding, and query helpers. It works with any `database/sql` driver, including `go-mssqldb`.

Unlike an ORM, sqlx doesn't generate SQL or manage schemas. You write the queries and sqlx handles the mapping between result sets and Go structs. This balance makes it a good fit when you want less boilerplate than raw `database/sql` without the abstraction of a full ORM.

Examples in this article run against the [AdventureWorks2025](../../samples/adventureworks-install-configure.md) sample database.

## Install sqlx

Create or open a Go module, and then add `sqlx` and `go-mssqldb`:

```bash
mkdir go-sqlx-sqlserver
cd go-sqlx-sqlserver
go mod init example.com/go-sqlx-sqlserver
go get github.com/jmoiron/sqlx
go get github.com/microsoft/go-mssqldb
```

## Connect to SQL Server

Open a connection by using `sqlx.Open` with the `sqlserver` driver name:

```go
package main

import (
    "fmt"
    "log"

    "github.com/jmoiron/sqlx"
    _ "github.com/microsoft/go-mssqldb"
)

func main() {
    // Replace <server>, <user>, and <password> with your own values.
    // In production, load credentials from environment variables or a secret store.
    dsn := "sqlserver://<user>:<password>@<server>.database.windows.net:1433?database=AdventureWorks2025&encrypt=true&TrustServerCertificate=false"
    db, err := sqlx.Open("sqlserver", dsn)
    if err != nil {
        log.Fatal(err)
    }
    defer db.Close()

    if err := db.Ping(); err != nil {
        log.Fatal(err)
    }
    fmt.Println("Connected to SQL Server with sqlx")
}
```

`sqlx.Open` returns an `*sqlx.DB` that wraps the standard `*sql.DB`. All `database/sql` methods still work, plus the sqlx extensions described in this article.

## Struct scanning with Select and Get

The main advantage of sqlx over raw `database/sql` is automatic struct scanning. Map columns to struct fields with `db` tags:

```go
type Product struct {
    ProductID int32   `db:"ProductID"`
    Name      string  `db:"Name"`
    ListPrice float64 `db:"ListPrice"`
}
```

### Select multiple rows

`Select` scans all result rows into a slice of structs:

```go
var products []Product
err := db.Select(&products,
    "SELECT TOP 5 ProductID, Name, ListPrice FROM Production.Product WHERE ListPrice > @p1 ORDER BY ListPrice DESC",
    sql.Named("p1", 1000))
if err != nil {
    log.Fatal(err)
}
for _, p := range products {
    fmt.Printf("%-35s $%.2f\n", p.Name, p.ListPrice)
}
```

`Select` executes the query, scans every row into a struct, and closes the rows iterator. It replaces the `Query` / `for rows.Next()` / `rows.Scan()` pattern.

### Get a single row

`Get` scans the first row into a single struct:

```go
var product Product
err := db.Get(&product,
    "SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID = @p1",
    sql.Named("p1", 680))
if err != nil {
    log.Fatal(err)
}
fmt.Printf("%s: $%.2f\n", product.Name, product.ListPrice)
```

`Get` returns `sql.ErrNoRows` if the query returns no rows. If multiple rows match, only the first is scanned.

## Named parameters

sqlx supports `:name` style named parameters using maps or structs as the parameter source. However, `go-mssqldb` uses `@name` parameters natively, so for SQL Server, use `sql.Named` with the standard `@p1` placeholders as shown in the previous examples.

If you prefer sqlx's `:name` syntax, use `sqlx.Named` to rebind:

```go
query, args, err := sqlx.Named(
    "SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID = :id",
    map[string]interface{}{"id": 680})
if err != nil {
    log.Fatal(err)
}
// Rebind to positional placeholders for go-mssqldb
query = db.Rebind(query)

var product Product
err = db.Get(&product, query, args...)
if err != nil {
    log.Fatal(err)
}
fmt.Printf("%s: $%.2f\n", product.Name, product.ListPrice)
```

> [!NOTE]
> `sqlx.Named` converts `:name` placeholders to positional `?` parameters. `Rebind` then converts `?` to the target driver's format. Use this two-step approach when you want portable named parameters across drivers.

## IN clause expansion

Building `WHERE column IN (...)` clauses with a variable number of values is tedious with raw SQL. Use `sqlx.In` to expand a slice into individual placeholders:

```go
ids := []int32{680, 706, 707}
query, args, err := sqlx.In(
    "SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID IN (?)", ids)
if err != nil {
    log.Fatal(err)
}
query = db.Rebind(query)

var products []Product
err = db.Select(&products, query, args...)
if err != nil {
    log.Fatal(err)
}
for _, p := range products {
    fmt.Printf("%d: %s ($%.2f)\n", p.ProductID, p.Name, p.ListPrice)
}
```

`sqlx.In` expands the single `?` into `?, ?, ?` and flattens the slice into the args list. `Rebind` then converts to the driver's placeholder format.

## Execute writes

Use `Exec` for INSERT, UPDATE, and DELETE statements. These examples target `Production.ScrapReason` so they don't modify critical tables.

> [!CAUTION]
> sqlx also offers `MustExec`, which panics instead of returning an error. Avoid `MustExec` in production services because an unrecovered panic terminates the process. The examples in this section use `Exec` with explicit error checks.

### Insert a row

Use `Exec` with an `INSERT` statement and named parameters:

```go
result, err := db.Exec(
    "INSERT INTO Production.ScrapReason (Name, ModifiedDate) VALUES (@p1, @p2)",
    sql.Named("p1", "sqlx test scrap"), sql.Named("p2", time.Now()))
if err != nil {
    log.Fatal(err)
}
affected, _ := result.RowsAffected()
fmt.Printf("Inserted %d row(s)\n", affected)

// Clean up
db.Exec("DELETE FROM Production.ScrapReason WHERE Name = @p1",
    sql.Named("p1", "sqlx test scrap"))
```

### Transaction with sqlx

Wrap operations in a transaction using `Beginx`:

```go
tx, err := db.Beginx()
if err != nil {
    log.Fatal(err)
}
defer tx.Rollback() // no-op after Commit, but essential if an error occurs

if _, err := tx.Exec(
    "INSERT INTO Production.ScrapReason (Name, ModifiedDate) VALUES (@p1, @p2)",
    sql.Named("p1", "sqlx tx scrap 1"), sql.Named("p2", time.Now())); err != nil {
    log.Fatal(err)
}
if _, err := tx.Exec(
    "INSERT INTO Production.ScrapReason (Name, ModifiedDate) VALUES (@p1, @p2)",
    sql.Named("p1", "sqlx tx scrap 2"), sql.Named("p2", time.Now())); err != nil {
    log.Fatal(err)
}

if err := tx.Commit(); err != nil {
    log.Fatal(err)
}
fmt.Println("Transaction committed")

// Clean up
db.Exec("DELETE FROM Production.ScrapReason WHERE Name IN (@p1, @p2)",
    sql.Named("p1", "sqlx tx scrap 1"), sql.Named("p2", "sqlx tx scrap 2"))
```

The `defer tx.Rollback()` call ensures the transaction is cleaned up if any operation fails. After a successful `Commit`, `Rollback` is a safe no-op. The `tx` object provides all the same sqlx helpers (`Select`, `Get`, `Exec`) scoped to the transaction.

## Struct scanning with joins

sqlx can scan columns from joined queries into a flat struct or an embedded struct.

```go
type ProductWithCategory struct {
    ProductID    int32   `db:"ProductID"`
    ProductName  string  `db:"ProductName"`
    CategoryName string  `db:"CategoryName"`
    ListPrice    float64 `db:"ListPrice"`
}

var results []ProductWithCategory
err := db.Select(&results, `
    SELECT TOP 5
        p.ProductID,
        p.Name AS ProductName,
        pc.Name AS CategoryName,
        p.ListPrice
    FROM Production.Product p
    JOIN Production.ProductSubcategory ps ON p.ProductSubcategoryID = ps.ProductSubcategoryID
    JOIN Production.ProductCategory pc ON ps.ProductCategoryID = pc.ProductCategoryID
    WHERE p.ListPrice > @p1
    ORDER BY p.ListPrice DESC`,
    sql.Named("p1", 1000))
if err != nil {
    log.Fatal(err)
}
for _, r := range results {
    fmt.Printf("%-35s %-15s $%.2f\n", r.ProductName, r.CategoryName, r.ListPrice)
}
```

## When to choose sqlx vs GORM vs database/sql

| Choose... | When it fits best |
| --- | --- |
| [`database/sql`](queries-statements.md) | You want zero dependencies beyond the driver, or you need full control over every query. |
| [sqlx](use-go-sql-extensions-library-with-go-mssqldb.md) | You want struct scanning and query helpers without an ORM's abstraction layer. |
| [GORM](use-go-orm-library-with-go-mssqldb.md) | You want schema management, associations, hooks, and a full ORM feature set. |

## Related content

- [Queries and statements with go-mssqldb](queries-statements.md)
- [Use GORM with go-mssqldb](use-go-orm-library-with-go-mssqldb.md)
- [Connection pooling with go-mssqldb](connection-pooling.md)
- [Error handling and retry patterns with go-mssqldb](error-handling.md)
