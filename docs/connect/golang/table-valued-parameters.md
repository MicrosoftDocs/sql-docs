---
title: "go-mssqldb Table-Valued Parameters"
description: "Pass structured data to stored procedures using table-valued parameters (TVP) with the go-mssqldb driver."
author: dlevy-msft
ms.author: dlevy
ms.date: 04/30/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Table-valued parameters with go-mssqldb

Table-valued parameters (TVP) enable you to pass multiple rows of structured data to a stored procedure or parameterized query. The `go-mssqldb` driver supports TVP through the `mssql.TVP` type.

Use TVPs when you need to pass a strongly typed rowset into a stored procedure or command and preserve column-level structure. For the highest-throughput path into a destination table, use [Bulk operations](bulk-operations.md). If your payload is nested or already JSON-shaped in the application layer, use [JSON and XML data](json-xml-data.md).

## Prerequisites

Create a user-defined table type and a stored procedure that accepts it:

```sql
CREATE TYPE dbo.DepartmentType AS TABLE (
    Name NVARCHAR(50),
    GroupName NVARCHAR(50)
);
GO

CREATE PROCEDURE dbo.InsertDepartments
    @departments dbo.DepartmentType READONLY
AS
BEGIN
    INSERT INTO HumanResources.Department (Name, GroupName)
    SELECT Name, GroupName FROM @departments;
END;
GO
```

## Define a Go struct for the TVP rows

Create a Go struct type that matches the columns in the user-defined table type:

```go
type DepartmentRow struct {
    Name      string
    GroupName string
}
```

## Pass a TVP to a stored procedure

Create an `mssql.TVP` value with the type name and a slice of structs, then pass it as a parameter:

```go
import (
    "context"
    "database/sql"

    "github.com/microsoft/go-mssqldb"
)

func insertDepartments(ctx context.Context, db *sql.DB, departments []DepartmentRow) error {
    tvp := mssql.TVP{
        TypeName: "dbo.DepartmentType",
        Value:    departments,
    }

    _, err := db.ExecContext(ctx, "dbo.InsertDepartments",
        sql.Named("departments", tvp))
    return err
}
```

Call the function:

```go
departments := []DepartmentRow{
    {Name: "Data Science", GroupName: "Research and Development"},
    {Name: "Cloud Ops", GroupName: "Information Technology"},
    {Name: "Developer Relations", GroupName: "Sales and Marketing"},
}
err := insertDepartments(ctx, db, departments)
```

## Column-to-field mapping

The driver maps TVP columns to struct fields by position (not by name). The first struct field maps to the first column in the table type, the second field maps to the second column, the third field maps to the third column, and so on for all columns in the table type.

To skip a column, you can't use struct tags. Reorder or restructure your Go type to match the column order in the user-defined table type.

## Supported field types

TVP struct fields support the same Go types as regular parameters:

| Go field type | SQL Server column type |
| --- | --- |
| `string` | `nvarchar` |
| `mssql.VarChar` | `varchar` |
| `int64`, `int32`, `int16`, `int8` | `bigint`, `int`, `smallint`, `tinyint` |
| `float64`, `float32` | `float`, `real` |
| `bool` | `bit` |
| `time.Time` | `datetimeoffset` |
| `[]byte` | `varbinary` |
| `mssql.UniqueIdentifier` | `uniqueidentifier` |

## Empty TVP

You can pass an empty slice. The stored procedure receives a table with zero rows:

```go
tvp := mssql.TVP{
    TypeName: "dbo.DepartmentType",
    Value:    []DepartmentRow{},
}
```

## Schema-qualified type name

`TypeName` must be the user-defined table type name (not the destination table name or stored procedure name). If the table type is in a non-default schema, include the schema in `TypeName`:

```go
tvp := mssql.TVP{
    TypeName: "HumanResources.DepartmentType",
    Value:    departments,
}
```

## Related content

- [Stored procedures with go-mssqldb](stored-procedures.md)
- [go-mssqldb data type mappings](data-type-mappings.md)
- [Queries and statements with go-mssqldb](queries-statements.md)
