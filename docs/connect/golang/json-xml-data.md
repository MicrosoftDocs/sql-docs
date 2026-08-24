---
title: "go-mssqldb JSON and XML Data"
description: "Work with JSON and XML data in SQL Server using the go-mssqldb driver. Parse, query, and store structured data formats."
author: dlevy-msft
ms.author: dlevy
ms.date: 04/30/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# JSON and XML data with go-mssqldb

SQL Server has built-in support for JSON and XML data. This article shows how to use the `go-mssqldb` driver to query, insert, and transform JSON and XML data between Go structures and SQL Server.

Examples in this article run against the [AdventureWorks2025](../../samples/adventureworks-install-configure.md) sample database. Read-oriented examples query built-in objects such as `Sales.vSalesPerson` and `Sales.SalesOrderHeader`. Write-oriented examples use `OPENJSON` and XML `.nodes()` to insert rows into `HumanResources.Department`.

Most snippets assume `ctx` and `db` are already initialized. You can use this setup pattern:

```go
import (
    "context"
    "database/sql"
    "log"
    "time"

    _ "github.com/microsoft/go-mssqldb"
)

db, err := sql.Open("sqlserver", "sqlserver://<user>:<password>@<server>?database=AdventureWorks2025&encrypt=true&TrustServerCertificate=true")
if err != nil {
    log.Fatal(err)
}
defer db.Close()

ctx, cancel := context.WithTimeout(context.Background(), 15*time.Second)
defer cancel()
```

## JSON data

### Query results as JSON with FOR JSON

Use `FOR JSON PATH` to return query results as a JSON string:

```go
var jsonResult string
err := db.QueryRowContext(ctx,
    "SELECT BusinessEntityID AS Id, FirstName + ' ' + LastName AS Name, CountryRegionName AS Department FROM Sales.vSalesPerson WHERE CountryRegionName = @dept FOR JSON PATH",
    sql.Named("dept", "United States")).Scan(&jsonResult)
if err != nil {
    log.Fatal(err)
}
fmt.Println(jsonResult)
```

> [!NOTE]
> SQL Server can return `FOR JSON` output across multiple rows for larger payloads. If you need to support larger results reliably, read all returned rows and concatenate them. See [Handle large JSON results](#handle-large-json-results).

### Handle large JSON results

When SQL Server returns JSON output in multiple rows, concatenate the chunks before unmarshaling or printing the result:

```go
import "strings"

func queryJSON(ctx context.Context, db *sql.DB, query string, args ...any) (string, error) {
    rows, err := db.QueryContext(ctx, query, args...)
    if err != nil {
        return "", err
    }
    defer rows.Close()

    var sb strings.Builder
    for rows.Next() {
        var chunk string
        if err := rows.Scan(&chunk); err != nil {
            return "", err
        }
        sb.WriteString(chunk)
    }
    if err := rows.Err(); err != nil {
        return "", err
    }
    return sb.String(), nil
}
```

Usage:

```go
jsonStr, err := queryJSON(ctx, db,
    "SELECT BusinessEntityID AS Id, FirstName + ' ' + LastName AS Name, CountryRegionName AS Department FROM Sales.vSalesPerson FOR JSON PATH")
if err != nil {
    log.Fatal(err)
}
```

### Unmarshal JSON results into Go structs

Combine `FOR JSON` with `encoding/json` to deserialize SQL Server results directly into Go types:

```go
import "encoding/json"

type Employee struct {
    ID         int    `json:"Id"`
    Name       string `json:"Name"`
    Department string `json:"Department"`
}

func getEmployees(ctx context.Context, db *sql.DB, dept string) ([]Employee, error) {
    var jsonStr string
    err := db.QueryRowContext(ctx,
        "SELECT BusinessEntityID AS Id, FirstName + ' ' + LastName AS Name, CountryRegionName AS Department FROM Sales.vSalesPerson WHERE CountryRegionName = @dept FOR JSON PATH",
        sql.Named("dept", dept)).Scan(&jsonStr)
    if err != nil {
        return nil, err
    }

    var employees []Employee
    if err := json.Unmarshal([]byte(jsonStr), &employees); err != nil {
        return nil, err
    }
    return employees, nil
}
```

### Nested JSON with FOR JSON PATH

Create nested JSON structures by using dot notation in column aliases:

```go
var jsonResult string
err := db.QueryRowContext(ctx, `
    SELECT
        soh.SalesOrderID AS [Id],
        soh.OrderDate AS [OrderDate],
        CONCAT(pp.FirstName, ' ', pp.LastName) AS [Customer.Name],
        ea.EmailAddress AS [Customer.Email],
        soh.TotalDue AS [Total]
    FROM Sales.SalesOrderHeader AS soh
    JOIN Sales.Customer AS c ON soh.CustomerID = c.CustomerID
    LEFT JOIN Person.Person AS pp ON c.PersonID = pp.BusinessEntityID
    LEFT JOIN Person.EmailAddress AS ea ON pp.BusinessEntityID = ea.BusinessEntityID
    WHERE soh.SalesOrderID = @id
    FOR JSON PATH, WITHOUT_ARRAY_WRAPPER`,
    sql.Named("id", 43659)).Scan(&jsonResult)
```

The result:

```json
{"Id":43659,"OrderDate":"2022-05-30T00:00:00","Customer":{"Name":"James Hendergart","Email":"james9@adventure-works.com"},"Total":23153.2339}
```

### Parse JSON in SQL Server with OPENJSON

Use `OPENJSON` to shred a JSON array into rows server-side:

```go
jsonData := `[
    {"Name": "Alice", "Department": "Engineering"},
    {"Name": "Bob", "Department": "Marketing"}
]`

rows, err := db.QueryContext(ctx, `
    SELECT Name, Department
    FROM OPENJSON(@json)
    WITH (
        Name NVARCHAR(100) '$.Name',
        Department NVARCHAR(100) '$.Department'
    )`, sql.Named("json", jsonData))
if err != nil {
    log.Fatal(err)
}
defer rows.Close()

for rows.Next() {
    var name, dept string
    if err := rows.Scan(&name, &dept); err != nil {
        log.Fatal(err)
    }
    fmt.Printf("%s - %s\n", name, dept)
}
if err := rows.Err(); err != nil {
    log.Fatal(err)
}
```

### Insert JSON data as a parameter

Send a Go struct as a JSON string to a stored procedure or `OPENJSON`:

```go
type OrderItem struct {
    ProductID int     `json:"productId"`
    Quantity  int     `json:"quantity"`
    Price     float64 `json:"price"`
}

func sendJSONPayload(ctx context.Context, db *sql.DB, metadata map[string]any) error {
    jsonBytes, err := json.Marshal(metadata)
    if err != nil {
        return err
    }

    // Pass JSON to a stored procedure for server-side processing.
    _, err = db.ExecContext(ctx,
        "EXECUTE dbo.ProcessDepartmentMetadata @payload",
        sql.Named("payload", string(jsonBytes)))
    return err
}
```

Usage:

```go
metadata := map[string]any{
    "source":  "docs-sample",
    "batchId": "b-20260709-01",
    "departments": []map[string]any{
        {"name": "Data Science", "groupName": "Research and Development"},
        {"name": "Cloud Ops", "groupName": "Information Technology"},
    },
}

if err := sendJSONPayload(ctx, db, metadata); err != nil {
    log.Fatal(err)
}
```

### Batch insert from JSON with OPENJSON

Insert multiple rows from a JSON array in a single statement:

```go
func bulkInsertFromJSON(ctx context.Context, db *sql.DB, jsonData string) (int64, error) {
    result, err := db.ExecContext(ctx, `
        INSERT INTO HumanResources.Department (Name, GroupName)
        SELECT Name, GroupName
        FROM OPENJSON(@json)
        WITH (
            Name NVARCHAR(50) '$.name',
            GroupName NVARCHAR(50) '$.groupName'
        )`, sql.Named("json", jsonData))
    if err != nil {
        return 0, err
    }
    return result.RowsAffected()
}
```

Usage:

```go
jsonData := `[
    {"name": "Data Science", "groupName": "Research and Development"},
    {"name": "Cloud Ops", "groupName": "Information Technology"},
    {"name": "Developer Relations", "groupName": "Sales and Marketing"}
]`

rowsInserted, err := bulkInsertFromJSON(ctx, db, jsonData)
if err != nil {
    log.Fatal(err)
}
fmt.Printf("Inserted %d rows\n", rowsInserted)
```

### JSON_VALUE and JSON_QUERY

Extract values from JSON columns without transferring the entire JSON document:

```go
// Extract a scalar value.
var productName string
err := db.QueryRowContext(ctx, `
    SELECT Name
    FROM Production.Product
    WHERE ProductID = @id`,
    sql.Named("id", 1)).Scan(&productName)
if err != nil {
    log.Fatal(err)
}
fmt.Println(productName)

// Extract a JSON array from a FOR JSON subquery.
var itemsJSON string
err = db.QueryRowContext(ctx, `
    SELECT (
        SELECT ProductID, Name
        FROM Production.Product
        WHERE ProductSubcategoryID = @subId
        FOR JSON PATH
    ) AS Items`,
    sql.Named("subId", 1)).Scan(&itemsJSON)
if err != nil {
    log.Fatal(err)
}
fmt.Println(itemsJSON)
```

> [!TIP]
> `JSON_VALUE` returns a scalar value (string, number). `JSON_QUERY` returns an object or array. Use the correct function for the data type you need.

### JSON computed columns and indexes

For frequently queried JSON properties, create computed columns with indexes for better performance:

```sql
-- Demo table with JSON-only rows.
IF OBJECT_ID('dbo.ProductJsonDemo', 'U') IS NOT NULL
    DROP TABLE dbo.ProductJsonDemo;

CREATE TABLE dbo.ProductJsonDemo (
    ProductDescriptionID INT IDENTITY(1,1) PRIMARY KEY,
    Metadata NVARCHAR(MAX) NOT NULL,
    CONSTRAINT CK_ProductJsonDemo_Metadata_IsJson CHECK (ISJSON(Metadata) = 1),
    -- Cast to a bounded length so the index key stays under SQL Server limits.
    LocaleName AS CAST(JSON_VALUE(Metadata, '$.locale') AS NVARCHAR(10)) PERSISTED
);

INSERT INTO dbo.ProductJsonDemo (Metadata)
VALUES
    (N'{"locale":"en","title":"Road helmet"}'),
    (N'{"locale":"fr","title":"Casque de route"}');

CREATE INDEX IX_ProductJsonDemo_LocaleName ON dbo.ProductJsonDemo(LocaleName);
```

Then query the computed column directly from Go:

```go
rows, err := db.QueryContext(ctx,
    "SELECT ProductDescriptionID, LocaleName FROM dbo.ProductJsonDemo WHERE LocaleName = @name",
    sql.Named("name", "en"))
if err != nil {
    log.Fatal(err)
}
defer rows.Close()

for rows.Next() {
    var id int
    var localeName string
    if err := rows.Scan(&id, &localeName); err != nil {
        log.Fatal(err)
    }
    fmt.Printf("%d %s\n", id, localeName)
}
if err := rows.Err(); err != nil {
    log.Fatal(err)
}
```

## XML data

### Query results as XML with FOR XML

Use `FOR XML PATH` to return query results as XML:

```go
var xmlResult string
err := db.QueryRowContext(ctx, `
    SELECT BusinessEntityID AS [@id], FirstName + ' ' + LastName AS Name, CountryRegionName AS Department
    FROM Sales.vSalesPerson
    WHERE CountryRegionName = @dept
    FOR XML PATH('Employee'), ROOT('Employees')`,
    sql.Named("dept", "United States")).Scan(&xmlResult)
if err != nil {
    log.Fatal(err)
}
fmt.Println(xmlResult)
```

The result:

```xml
<Employees>
    <Employee id="274"><Name>Stephen Jiang</Name><Department>United States</Department></Employee>
    <Employee id="275"><Name>Michael Blythe</Name><Department>United States</Department></Employee>
    <Employee id="276"><Name>Linda Mitchell</Name><Department>United States</Department></Employee>
    <Employee id="277"><Name>Jillian Carson</Name><Department>United States</Department></Employee>
    <Employee id="279"><Name>Tsvi Reiter</Name><Department>United States</Department></Employee>
    <Employee id="280"><Name>Pamela Ansman-Wolfe</Name><Department>United States</Department></Employee>
    <Employee id="281"><Name>Shu Ito</Name><Department>United States</Department></Employee>
    <Employee id="283"><Name>David Campbell</Name><Department>United States</Department></Employee>
    <Employee id="284"><Name>Tete Mensa-Annan</Name><Department>United States</Department></Employee>
    <Employee id="285"><Name>Syed Abbas</Name><Department>United States</Department></Employee>
    <Employee id="287"><Name>Amy Alberts</Name><Department>United States</Department></Employee>
</Employees>
```

### Handle large XML results

Like JSON, large XML results are split across rows.

```go
func queryXML(ctx context.Context, db *sql.DB, query string, args ...any) (string, error) {
    rows, err := db.QueryContext(ctx, query, args...)
    if err != nil {
        return "", err
    }
    defer rows.Close()

    var sb strings.Builder
    for rows.Next() {
        var chunk string
        if err := rows.Scan(&chunk); err != nil {
            return "", err
        }
        sb.WriteString(chunk)
    }
    if err := rows.Err(); err != nil {
        return "", err
    }
    return sb.String(), nil
}
```

### Parse XML results in Go

Use the `encoding/xml` package to unmarshal XML results.

```go
import "encoding/xml"

type EmployeeList struct {
    XMLName   xml.Name   `xml:"Employees"`
    Employees []Employee `xml:"Employee"`
}

type Employee struct {
    ID         int    `xml:"id,attr"`
    Name       string `xml:"Name"`
    Department string `xml:"Department"`
}

func getEmployeesXML(ctx context.Context, db *sql.DB, dept string) (*EmployeeList, error) {
    xmlStr, err := queryXML(ctx, db, `
        SELECT BusinessEntityID AS [@id], FirstName + ' ' + LastName AS Name, CountryRegionName AS Department
        FROM Sales.vSalesPerson
        WHERE CountryRegionName = @dept
        FOR XML PATH('Employee'), ROOT('Employees')`,
        sql.Named("dept", dept))
    if err != nil {
        return nil, err
    }

    var result EmployeeList
    if err := xml.Unmarshal([]byte(xmlStr), &result); err != nil {
        return nil, err
    }
    return &result, nil
}
```

### Pass XML parameters to SQL Server

Send an XML document to a stored procedure or query:

```go
xmlData := `<Employees>
    <Employee><Name>Alice</Name><Department>Engineering</Department></Employee>
    <Employee><Name>Bob</Name><Department>Marketing</Department></Employee>
</Employees>`

rows, err := db.QueryContext(ctx, `
    DECLARE @xmlDoc XML = CAST(@xml AS XML);
    SELECT
        e.value('(Name)[1]', 'NVARCHAR(100)') AS Name,
        e.value('(Department)[1]', 'NVARCHAR(100)') AS Department
    FROM @xmlDoc.nodes('/Employees/Employee') AS t(e)`,
    sql.Named("xml", xmlData))
if err != nil {
    log.Fatal(err)
}
defer rows.Close()

for rows.Next() {
    var name, dept string
    if err := rows.Scan(&name, &dept); err != nil {
        log.Fatal(err)
    }
    fmt.Printf("%s - %s\n", name, dept)
}
if err := rows.Err(); err != nil {
    log.Fatal(err)
}
```

> [!NOTE]
> The `@xml` parameter is sent as **nvarchar(max)** by default. The `.nodes()` method requires the **xml** data type, so cast the parameter explicitly with `CAST(@xml AS XML)`.

### Insert rows from XML

Use the `.nodes()` method with an `INSERT...SELECT` statement to shred XML into table rows.

```go
func bulkInsertFromXML(ctx context.Context, db *sql.DB, xmlData string) (int64, error) {
    result, err := db.ExecContext(ctx, `
        DECLARE @xmlDoc XML = CAST(@xml AS XML);
        INSERT INTO HumanResources.Department (Name, GroupName)
        SELECT
            e.value('(Name)[1]', 'NVARCHAR(50)'),
            e.value('(GroupName)[1]', 'NVARCHAR(50)')
        FROM @xmlDoc.nodes('/Departments/Department') AS t(e)`,
        sql.Named("xml", xmlData))
    if err != nil {
        return 0, err
    }
    return result.RowsAffected()
}
```

## Choose between JSON and XML

| Consideration | JSON | XML |
| --- | --- | --- |
| Go ecosystem support | Standard `encoding/json`. Struct tags for mapping. | Standard `encoding/xml`. More verbose struct tags. |
| SQL Server support | `OPENJSON`, `JSON_VALUE`, `JSON_QUERY`, `FOR JSON` (SQL Server 2016 and later versions) | `nodes()`, `value()`, `query()`, `FOR XML` (all versions) |
| Performance | Generally faster parsing. Less verbose wire format. | Supports schemas and validation. More verbose. |
| Schema validation | No built-in schema validation in SQL Server. | Supports XML Schema Collections for server-side validation. |
| Indexing | Computed columns with JSON_VALUE + index. | XML indexes (primary and secondary). |
| Data modeling | Arrays and nested objects. Natural fit for Go slices and maps. | Hierarchical documents with attributes and namespaces. |

> [!TIP]
> For new development, JSON is usually the better choice. It requires less parsing overhead, produces smaller payloads, and maps naturally to Go structs. Use XML when you need schema validation or when integrating with systems that require XML.

## Related content

- [Queries and statements with go-mssqldb](queries-statements.md)
- [go-mssqldb data type mappings](data-type-mappings.md)
- [Stored procedures with go-mssqldb](stored-procedures.md)
- [Performance tuning with go-mssqldb](performance-tuning.md)
