---
title: "go-mssqldb Data Type Mappings"
description: "Data type conversion between Go types and SQL Server types in the go-mssqldb driver."
author: dlevy-msft
ms.author: dlevy
ms.date: 03/28/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ai-usage: ai-assisted
---
# go-mssqldb data type mappings

The `go-mssqldb` driver converts Go types to SQL Server types automatically when you pass parameters, and converts SQL Server types back to Go types when scanning results. This article documents the default conversions and the driver-specific types available for explicit control.

Examples in this article run against the [AdventureWorks2025](../../samples/adventureworks-install-configure.md) sample database. Read-oriented examples query built-in objects such as `Production.Product` and `Sales.vSalesPerson`. Write-oriented examples target `HumanResources.Department` and `Production.ProductCategory`.

## Go to SQL Server parameter mappings

When you pass values as query parameters, the driver converts Go types to SQL Server types:

| Go type | SQL Server type | Notes |
| --- | --- | --- |
| `string` | **nvarchar** | Unicode string. Use `mssql.VarChar` for non-Unicode **varchar**. |
| `[]byte` | **varbinary** | Binary data. |
| `int64` | **bigint** | |
| `float64` | **float** | 64-bit float. |
| `bool` | **bit** | |
| `time.Time` | **datetimeoffset** | Preserves time zone offset. Use `mssql.DateTime1` for **datetime**. Use `mssql.DateTimeOffset` for explicit offset control. |
| `int32` | **int** | |
| `int16` | **smallint** | |
| `int8` | **tinyint** | |
| `float32` | **real** | 32-bit float. |
| `mssql.TVP` | Table-valued parameter | See [Table-valued parameters](table-valued-parameters.md). |

## Driver-specific types

Use these types from the `mssql` package when you need explicit control over the SQL Server type:

| Driver type | SQL Server type | Description |
| --- | --- | --- |
| `mssql.VarChar` | **varchar** | Non-Unicode string. Wrap a `string` value. |
| `mssql.NVarCharMax` | **nvarchar(max)** | Unicode large string. |
| `mssql.VarCharMax` | **varchar(max)** | Non-Unicode large string. |
| `mssql.NChar` | **nchar** | Fixed-length Unicode string. Wrap a `string` value. |
| `mssql.DateTime1` | **datetime** | Legacy datetime without offset. |
| `mssql.DateTimeOffset` | **datetimeoffset** | Explicit offset control. |
| `mssql.UniqueIdentifier` | **uniqueidentifier** | GUID value. |
| `mssql.TVP` | User-defined table type | Table-valued parameter struct. |

### Example: varchar vs. nvarchar

By default, `string` parameters are sent as `nvarchar`. To use `varchar` instead:

```go
import "github.com/microsoft/go-mssqldb"

// Sends as nvarchar (default)
db.QueryContext(ctx, "SELECT * FROM Production.Product WHERE Name = @p1", "Adjustable Race")

// Sends as varchar
db.QueryContext(ctx, "SELECT * FROM Production.Product WHERE Name = @p1", mssql.VarChar("Adjustable Race"))
```

### Example: uniqueidentifier

Use the `mssql.UniqueIdentifier` type to scan `uniqueidentifier` columns:

```go
import "github.com/microsoft/go-mssqldb"

var id mssql.UniqueIdentifier
err := db.QueryRowContext(ctx, "SELECT rowguid FROM Production.Product WHERE Name = @p1",
    sql.Named("p1", "Adjustable Race")).Scan(&id)
```

## SQL Server to Go result mappings

When you scan query results, the driver converts SQL Server types to Go types:

| SQL Server type | Go type | Notes |
| --- | --- | --- |
| **int**, **bigint**, **smallint**, **tinyint** | `int64` | All integer types scan to `int64`. Cast as needed. |
| **float**, **real** | `float64` | |
| **decimal**, **numeric**, **money**, **smallmoney** | `[]byte` or `string` | No native Go decimal type. Scan to `string` or use a third-party decimal library. |
| **bit** | `bool` | |
| **char**, **varchar**, **text** | `string` | |
| **nchar**, **nvarchar**, **ntext** | `string` | |
| **binary**, **varbinary**, **image** | `[]byte` | |
| **date**, **datetime**, **datetime2**, **smalldatetime** | `time.Time` | |
| **datetimeoffset** | `time.Time` | Time zone offset is preserved. |
| **time** | `time.Time` | Date component is `0001-01-01`. |
| **uniqueidentifier** | `[]byte` or `mssql.UniqueIdentifier` | Scans to raw bytes by default. Use `mssql.UniqueIdentifier` for formatted GUID output. |
| **xml** | `string` | |

## Named parameters

Use `sql.Named` to create named parameters:

```go
rows, err := db.QueryContext(ctx,
    "SELECT * FROM Sales.vSalesPerson WHERE CountryRegionName = @location AND FirstName = @name",
    sql.Named("location", "Australia"),
    sql.Named("name", "Jared"))
```

> [!NOTE]
> Positional parameters (`@p1`, `@p2`) also work. The driver assigns ordinal names based on the order of the arguments.

## Handle NULL values

SQL Server columns that allow NULL require special handling in Go. The standard `database/sql` package provides nullable types for this purpose.

### sql.Null types

Use `sql.NullString`, `sql.NullInt64`, `sql.NullFloat64`, `sql.NullBool`, and `sql.NullTime` to handle columns that can be NULL:

```go
var name sql.NullString
var color sql.NullString
var weight sql.NullFloat64
var sellStartDate sql.NullTime
var sellEndDate sql.NullTime

err := db.QueryRowContext(ctx,
    "SELECT Name, Color, Weight, SellStartDate, SellEndDate FROM Production.Product WHERE ProductID = @id",
    sql.Named("id", 1)).Scan(&name, &color, &weight, &sellStartDate, &sellEndDate)
if err != nil {
    log.Fatal(err)
}

if name.Valid {
    fmt.Println("Name:", name.String)
} else {
    fmt.Println("Name: NULL")
}

if weight.Valid {
    fmt.Printf("Weight: %.2f\n", weight.Float64)
}
```

### Pointer-based NULL handling

As an alternative to `sql.Null` types, use pointers. A `nil` pointer represents NULL:

```go
var name *string
var color *string

err := db.QueryRowContext(ctx,
    "SELECT Name, Color FROM Production.Product WHERE ProductID = @id",
    sql.Named("id", 1)).Scan(&name, &color)

if name != nil {
    fmt.Println("Name:", *name)
} else {
    fmt.Println("Name: NULL")
}
```

> [!TIP]
> Pointer-based NULL handling is more concise, but `sql.Null` types make the NULL intent more explicit in struct definitions. Choose whichever pattern your team prefers and use it consistently.

### Send NULL values as parameters

Pass `nil` to send a NULL value as a parameter:

```go
// Insert a row with a NULL Color.
_, err := db.ExecContext(ctx,
    "INSERT INTO Production.ProductCategory (Name) VALUES (@name)",
    sql.Named("name", "Custom Parts"))
```

## Decimal and numeric precision

SQL Server's `decimal` and `numeric` types support up to 38 digits of precision. Go's `float64` provides only about 15-16 significant digits. Scanning high-precision values into `float64` causes silent precision loss.

### Scan to string

The safest and simplest approach is to scan `decimal`/`numeric`/`money`/`smallmoney` columns into `string`, then convert with a precision-safe library:

```go
var priceStr string
err := db.QueryRowContext(ctx,
    "SELECT ListPrice FROM Production.Product WHERE ProductID = @id",
    sql.Named("id", 1)).Scan(&priceStr)
if err != nil {
    log.Fatal(err)
}
fmt.Println("Price:", priceStr) // "12345.678901234567890"
```

For performance-sensitive paths, you can scan into `[]byte` and parse directly with your decimal library to reduce string allocations.

### Use shopspring/decimal for arithmetic

The `shopspring/decimal` library provides arbitrary-precision decimal arithmetic:

```go
import "github.com/shopspring/decimal"

var priceStr string
err := db.QueryRowContext(ctx,
    "SELECT ListPrice FROM Production.Product WHERE ProductID = @id",
    sql.Named("id", 1)).Scan(&priceStr)
if err != nil {
    log.Fatal(err)
}

price, err := decimal.NewFromString(priceStr)
if err != nil {
    log.Fatal(err)
}

tax := price.Mul(decimal.NewFromFloat(0.08))
total := price.Add(tax)
fmt.Println("Total:", total.StringFixed(2))
```

> [!CAUTION]
> Never scan `money` or `decimal` columns into `float64` when exact precision matters (financial calculations, currency, tax). Use exact-value scanning (`string` or `[]byte`) with `shopspring/decimal` or `cockroachdb/apd` instead.

### Send decimal values as parameters

When sending decimal values as parameters, use `string` to avoid float precision loss:

```go
price := "12345.678901234567890"
_, err := db.ExecContext(ctx,
    "UPDATE Production.Product SET ListPrice = @price WHERE ProductID = @id",
    sql.Named("price", price),
    sql.Named("id", 1)) // String is converted to decimal by SQL Server.
```

## Date and time types

### time.Time mapping

The driver maps Go's `time.Time` to `datetimeoffset` by default, which preserves the time zone offset. Use driver-specific types for other datetime formats:

| Go value | SQL Server type | When to use |
| --- | --- | --- |
| `time.Time` | `datetimeoffset` | Default. Use when the time zone matters. |
| `mssql.DateTime1(t)` | `datetime` | Legacy columns that don't support offset. |
| `mssql.DateTimeOffset(t)` | `datetimeoffset` | Explicit offset control. |

### UTC best practice

Store times in UTC and convert to local time in the application layer:

```go
now := time.Now().UTC()
_, err := db.ExecContext(ctx,
    "INSERT INTO Production.ScrapReason (Name, ModifiedDate) VALUES (@name, @modified)",
    sql.Named("name", "Operator error"),
    sql.Named("modified", now))
```

## uniqueidentifier (GUID) patterns

SQL Server stores `uniqueidentifier` values with the first three groups in little-endian byte order (also known as mixed-endian or GUID byte order). If you scan a `uniqueidentifier` column into `[]byte`, the raw bytes don't match the standard string representation. Use `mssql.UniqueIdentifier` instead, which handles the byte-order conversion automatically.

### Scan with mssql.UniqueIdentifier

By default, `uniqueidentifier` columns scan to `[]byte`. Use `mssql.UniqueIdentifier` for formatted GUID strings:

```go
var id mssql.UniqueIdentifier
err := db.QueryRowContext(ctx,
    "SELECT rowguid FROM Production.Product WHERE Name = @name",
    sql.Named("name", "Adjustable Race")).Scan(&id)
if err != nil {
    log.Fatal(err)
}
fmt.Println("ID:", id) // "6F9619FF-8B86-D011-B42D-00C04FC964FF"
```

### Generate and send GUIDs

Use `NEWID()` on the server or generate in Go:

```go
// Server-side generation
var newID mssql.UniqueIdentifier
err := db.QueryRowContext(ctx,
    "INSERT INTO Production.ProductCategory (Name) OUTPUT INSERTED.rowguid VALUES (@name)",
    sql.Named("name", "Custom Parts")).Scan(&newID)
```

Client-side generation with the `google/uuid` package:

```go
import "github.com/google/uuid"

id := mssql.UniqueIdentifier(uuid.New())
_, err := db.ExecContext(ctx,
    "UPDATE Production.ProductCategory SET rowguid = @id WHERE Name = @name",
    sql.Named("id", id),
    sql.Named("name", "Custom Parts"))
```

## Type mapping checklist

| Scenario | Recommended approach |
| --- | --- |
| Nullable columns | Use `sql.NullString`, `sql.NullInt64`, `sql.NullFloat64`, `sql.NullBool`, `sql.NullTime`, or pointer types. |
| Financial/decimal values | Scan to `string`, use `shopspring/decimal` for arithmetic. |
| Dates with time zones | Use `time.Time` (maps to `datetimeoffset` by default). |
| Legacy datetime columns | Use `mssql.DateTime1` when sending parameters. |
| GUIDs | Scan with `mssql.UniqueIdentifier` for formatted output. |
| Non-Unicode strings | Use `mssql.VarChar` to avoid implicit `nvarchar` conversion. |
| JSON columns | Scan to `string`, unmarshal with `encoding/json`. See [JSON and XML data](json-xml-data.md). |
| XML columns | Scan to `string`, unmarshal with `encoding/xml`. See [JSON and XML data](json-xml-data.md). |

## Related content

- [Queries and statements](queries-statements.md)
- [JSON and XML data](json-xml-data.md)
- [Table-valued parameters](table-valued-parameters.md)
- [Limitations](known-limitations.md)
- [Stored procedures](stored-procedures.md)
