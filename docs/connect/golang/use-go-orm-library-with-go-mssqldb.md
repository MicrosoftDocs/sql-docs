---
title: "Use GORM with go-mssqldb"
description: "Use the GORM ORM with the go-mssqldb driver to model, query, and write data in SQL Server from Go."
author: dlevy-msft
ms.author: dlevy
ms.date: 05/01/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Use GORM with go-mssqldb

[GORM](https://gorm.io/) is a popular Go ORM that supports SQL Server through the `gorm.io/driver/sqlserver` dialect, which uses `go-mssqldb` internally. GORM handles schema differences, query generation, and type mapping so you can work with Go structs instead of writing raw SQL.

This article covers GORM patterns that are specific to SQL Server. For general GORM usage, see the [GORM documentation](https://gorm.io/docs/).

Examples in this article run against the [AdventureWorks2025](../../samples/adventureworks-install-configure.md) sample database.

## Install GORM and the SQL Server dialect

Create or open a Go module, then add GORM and the SQL Server dialect:

```bash
mkdir go-gorm-sqlserver
cd go-gorm-sqlserver
go mod init example.com/go-gorm-sqlserver
go get gorm.io/gorm
go get gorm.io/driver/sqlserver
```

## Connect to SQL Server

Open a GORM session by passing a `sqlserver://` DSN to the dialect:

```go
package main

import (
    "fmt"
    "log"

    "gorm.io/driver/sqlserver"
    "gorm.io/gorm"
)

func main() {
    // Replace <server>, <user>, and <password> with your own values.
    // In production, load credentials from environment variables or a secret store.
    dsn := "sqlserver://<user>:<password>@<server>.database.windows.net:1433?database=AdventureWorks2025&encrypt=true&TrustServerCertificate=false"
    db, err := gorm.Open(sqlserver.Open(dsn), &gorm.Config{})
    if err != nil {
        log.Fatal(err)
    }

    sqlDB, err := db.DB()
    if err != nil {
        log.Fatal(err)
    }
    defer sqlDB.Close()

    fmt.Println("Connected to SQL Server with GORM")
}
```

The dialect uses the `go-mssqldb` driver under the hood, so all connection options from [Connection strings](connection-strings.md) and [Connection options](connection-options.md) apply to the DSN.

## Map models to existing tables

GORM's default naming convention uses lowercase plural table names (for example, `products`), which doesn't match SQL Server conventions. Use the `TableName()` method to bind a model to an existing schema-qualified table:

```go
type Product struct {
    ProductID   int32  `gorm:"column:ProductID;primaryKey"`
    Name        string `gorm:"column:Name"`
    ProductLine *string `gorm:"column:ProductLine"`
    ListPrice   float64 `gorm:"column:ListPrice"`
}

func (Product) TableName() string {
    return "Production.Product"
}
```

> [!TIP]
> Always define `TableName()` when you work with existing SQL Server databases. Relying on GORM's auto-pluralization leads to "invalid object name" errors against tables that follow SQL Server naming conventions.

## Query data

After you define a model, use GORM's query methods to read data:

### Find rows with a WHERE clause

Filter, sort, and limit results by using `Where`, `Order`, and `Limit`:

```go
var products []Product
result := db.Where("ListPrice > ?", 1000).
    Order("ListPrice DESC").
    Limit(5).
    Find(&products)
if result.Error != nil {
    log.Fatal(result.Error)
}

for _, p := range products {
    fmt.Printf("%-35s $%.2f\n", p.Name, p.ListPrice)
}
```

### Retrieve a single row

Use `First` with a condition to load one record:

```go
var product Product
if err := db.First(&product, "ProductID = ?", 680).Error; err != nil {
    log.Fatal(err)
}
fmt.Printf("%s: $%.2f\n", product.Name, product.ListPrice)
```

### Count rows

Use `Count` on a model scope to get a row count:

```go
var count int64
db.Model(&Product{}).Where("ListPrice > ?", 0).Count(&count)
fmt.Printf("Products with a list price: %d\n", count)
```

## Insert, update, and delete rows

Write operations use the same model structs. The examples in this section target `Production.ScrapReason` so they don't modify critical tables.

```go
type ScrapReason struct {
    ScrapReasonID int16     `gorm:"column:ScrapReasonID;primaryKey;autoIncrement"`
    Name          string    `gorm:"column:Name"`
    ModifiedDate  time.Time `gorm:"column:ModifiedDate"`
}

func (ScrapReason) TableName() string {
    return "Production.ScrapReason"
}
```

### Insert a row

Pass a populated struct to `Create` to insert a row and populate the auto-generated primary key:

```go
reason := ScrapReason{Name: "GORM test scrap", ModifiedDate: time.Now()}
if err := db.Create(&reason).Error; err != nil {
    log.Fatal(err)
}
fmt.Printf("Inserted ScrapReasonID: %d\n", reason.ScrapReasonID)

// Clean up
db.Delete(&reason)
```

### Update a row

Use `Model` with `Update` to change a column value:

```go
reason := ScrapReason{Name: "GORM update test", ModifiedDate: time.Now()}
db.Create(&reason)

db.Model(&reason).Update("Name", "GORM updated scrap")

var updated ScrapReason
db.First(&updated, reason.ScrapReasonID)
fmt.Printf("Updated name: %s\n", updated.Name)

// Clean up
db.Delete(&reason)
```

### Delete a row

Pass a struct with a populated primary key to `Delete`:

```go
reason := ScrapReason{Name: "GORM delete test", ModifiedDate: time.Now()}
db.Create(&reason)
id := reason.ScrapReasonID

db.Delete(&reason)

var count int64
db.Model(&ScrapReason{}).Where("ScrapReasonID = ?", id).Count(&count)
fmt.Printf("Rows remaining with that ID: %d\n", count)
```

## Batch insert and the 2,100-parameter limit

SQL Server limits `sp_executesql` to 2,100 parameters per statement. GORM's batch insert generates a multi-row `VALUES` clause where each column consumes one parameter, so a table with 10 columns hits the limit at roughly 210 rows per batch.

Set `CreateBatchSize` on the session to stay within the limit:

```go
reasons := make([]ScrapReason, 50)
for i := range reasons {
    reasons[i] = ScrapReason{
        Name:         fmt.Sprintf("Batch scrap %d", i+1),
        ModifiedDate: time.Now(),
    }
}

// 2 inserted columns per row (Name, ModifiedDate; the autoIncrement ID is excluded),
// so batches of 500 are well within the 2,100-parameter limit.
result := db.CreateInBatches(&reasons, 500)
if result.Error != nil {
    log.Fatal(result.Error)
}
fmt.Printf("Inserted %d rows in batches\n", result.RowsAffected)

// Clean up
for _, r := range reasons {
    db.Delete(&r)
}
```

For tables with many columns, reduce the batch size proportionally. A safe formula: `batch size = floor(2100 / number of inserted columns)`.

## Transactions

Wrap multiple operations in a GORM transaction:

```go
err := db.Transaction(func(tx *gorm.DB) error {
    r1 := ScrapReason{Name: "TX scrap 1", ModifiedDate: time.Now()}
    if err := tx.Create(&r1).Error; err != nil {
        return err
    }

    r2 := ScrapReason{Name: "TX scrap 2", ModifiedDate: time.Now()}
    if err := tx.Create(&r2).Error; err != nil {
        return err
    }

    // Both inserts commit together.
    fmt.Printf("Inserted IDs: %d, %d\n", r1.ScrapReasonID, r2.ScrapReasonID)

    // Clean up inside the same transaction.
    tx.Delete(&r1)
    tx.Delete(&r2)

    return nil
})
if err != nil {
    log.Fatal(err)
}
fmt.Println("Transaction committed")
```

If the callback returns an error, GORM rolls back automatically.

## Raw SQL fallback

When GORM's query builder doesn't generate the SQL Server syntax you need, drop to raw SQL while still using the GORM session:

```go
var results []struct {
    Name      string
    ListPrice float64
}
result := db.Raw("SELECT TOP 5 Name, ListPrice FROM Production.Product WHERE ListPrice > @price ORDER BY ListPrice DESC",
    sql.Named("price", 1000)).Scan(&results)
if result.Error != nil {
    log.Fatal(result.Error)
}

for _, r := range results {
    fmt.Printf("%-35s $%.2f\n", r.Name, r.ListPrice)
}
```

Raw queries use the same connection pool managed by GORM, so pool settings from `db.DB()` still apply.

## Configure the connection pool

Access the underlying `*sql.DB` to set pool parameters:

```go
sqlDB, err := db.DB()
if err != nil {
    log.Fatal(err)
}
sqlDB.SetMaxOpenConns(25)
sqlDB.SetMaxIdleConns(10)
sqlDB.SetConnMaxLifetime(5 * time.Minute)
```

For pool sizing guidance, see [Connection pooling](connection-pooling.md).

## AutoMigrate considerations

`AutoMigrate` creates or alters tables to match your model definitions. It works with SQL Server, but be aware of these behaviors:

- `AutoMigrate` creates tables in the default schema (`dbo`) unless you override `TableName()` with a schema prefix.
- It adds missing columns and creates indexes but doesn't drop columns or change existing column types.
- SQL Server has a 128-character limit on index names. GORM's generated index names can exceed this limit on tables with long names or composite indexes. Use explicit `gorm:"index:idx_short_name"` tags if you hit this limit.

For production databases with managed schemas, prefer [migration tools](migration-guide.md) over `AutoMigrate`.

## Related content

- [Connection strings](connection-strings.md)
- [Bulk operations](bulk-operations.md) for high-throughput inserts outside GORM
- [Error handling and retry patterns](error-handling.md)
- [Performance tuning](performance-tuning.md)
