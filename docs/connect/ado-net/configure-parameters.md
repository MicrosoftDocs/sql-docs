---
title: Configure parameters with Microsoft.Data.SqlClient
description: Learn how to configure input, output, table-valued, and streaming parameters for Microsoft.Data.SqlClient commands.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, paulmedynski, cmalhotra
ms.date: 10/13/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
dev_langs:
  - csharp
ai-usage: ai-assisted
---

# Configure parameters with Microsoft.Data.SqlClient

<a id="configuring-parameters"></a>

Parameters send values to a command separately from its Transact-SQL (T-SQL) text. Use <xref:Microsoft.Data.SqlClient.SqlParameter> objects instead of concatenating or interpolating values into SQL statements. Parameters help protect against SQL injection and preserve the value's database type.

The examples in this article assume that an open <xref:Microsoft.Data.SqlClient.SqlConnection> named `connection` targets the [AdventureWorks sample database](../../samples/adventureworks-install-configure.md).

The C# examples require `using Microsoft.Data.SqlClient;`. Examples that use `SqlDbType` also require `using System.Data;`.

<a id="example"></a>

## Add input parameters

Add a parameter for each value placeholder in the command text. Specify the parameter's SQL Server type and assign its value:

```csharp
const string sql = """
    SELECT ProductID, Name, ListPrice
    FROM Production.Product
    WHERE ProductSubcategoryID = @subcategoryId
      AND ListPrice >= @minimumPrice
    ORDER BY ListPrice;
    """;

using var command = new SqlCommand(sql, connection);
command.Parameters.Add("@subcategoryId", SqlDbType.Int).Value = 2;
command.Parameters.Add("@minimumPrice", SqlDbType.Money).Value = 1_000m;

using SqlDataReader reader = command.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine(
        $"{reader.GetInt32(0)}: {reader.GetString(1)} ({reader.GetDecimal(2):C})");
}
```

Use the same casing for a parameter name in the command text and in the `SqlParameter`. SQL Server matches variable names by using the [server collation](../../relational-databases/collations/set-or-change-the-server-collation.md), so a mismatch fails with a "Must declare the scalar variable" error on a server with a case-sensitive collation.

## Work with parameter placeholders

Microsoft.Data.SqlClient uses named parameters in the form `@parameterName`. Positional question mark (`?`) placeholders aren't supported.

Parameter placeholders represent values, not SQL identifiers or syntax. You can't use a parameter for a table name, column name, sort direction, or an entire SQL fragment. If an application selects an identifier dynamically, validate it against an allow list and add it to the command text only after validation. Continue to pass all data values as parameters.

For text commands, Microsoft.Data.SqlClient sends parameterized statements to SQL Server through `sp_executesql`. SQL Server can reuse an execution plan when the statement text and parameter definitions remain the same.

## Specify parameter data types

Set <xref:Microsoft.Data.SqlClient.SqlParameter.SqlDbType%2A> explicitly when the application knows the target SQL Server type. Also set type-specific metadata such as <xref:Microsoft.Data.SqlClient.SqlParameter.Size%2A>, <xref:Microsoft.Data.SqlClient.SqlParameter.Precision%2A>, and <xref:Microsoft.Data.SqlClient.SqlParameter.Scale%2A> when applicable.

This example matches the **nvarchar(50)** and **datetime** columns in `Person.Person`:

```csharp
const string sql = """
    SELECT BusinessEntityID, FirstName, LastName
    FROM Person.Person
    WHERE LastName = @lastName
      AND ModifiedDate >= @modifiedSince
    ORDER BY BusinessEntityID;
    """;

using var command = new SqlCommand(sql, connection);

SqlParameter lastName = command.Parameters.Add(
    "@lastName",
    SqlDbType.NVarChar,
    50);
lastName.Value = "O'Brien";

SqlParameter modifiedSince = command.Parameters.Add(
    "@modifiedSince",
    SqlDbType.DateTime);
modifiedSince.Value = new DateTime(2013, 1, 1);
```

Matching the parameter metadata to the target column avoids implicit conversions and prevents changes in string length or decimal scale from changing the parameter definition.

For decimal values, set precision and scale explicitly. The `Weight` column in `Production.Product` uses **decimal(8, 2)**:

```csharp
const string sql = """
    SELECT ProductID, Name, Weight
    FROM Production.Product
    WHERE Weight >= @minimumWeight
    ORDER BY Weight;
    """;

using var command = new SqlCommand(sql, connection);
SqlParameter minimumWeight = command.Parameters.Add(
    "@minimumWeight",
    SqlDbType.Decimal);
minimumWeight.Precision = 8;
minimumWeight.Scale = 2;
minimumWeight.Value = 10.25m;
```

### Pass database NULL values

Assign <xref:System.DBNull.Value> to send a database `NULL`. C# `null` doesn't represent a SQL Server `NULL` for ordinary scalar parameters:

```csharp
const string sql = """
    SELECT SalesOrderID, OrderDate, SalesPersonID
    FROM Sales.SalesOrderHeader
    WHERE (@salesPersonId IS NULL OR SalesPersonID = @salesPersonId)
    ORDER BY SalesOrderID;
    """;

using var command = new SqlCommand(sql, connection);
SqlParameter salesPersonId = command.Parameters.Add(
    "@salesPersonId",
    SqlDbType.Int);

int? selectedSalesPersonId = null;
salesPersonId.Value = selectedSalesPersonId.HasValue
    ? selectedSalesPersonId.Value
    : DBNull.Value;
```

### Use inferred metadata selectively

The <xref:Microsoft.Data.SqlClient.SqlParameterCollection.AddWithValue%2A> method infers parameter metadata from the runtime value. Inference is convenient when the inferred metadata is stable and matches the database type. It can produce different parameter definitions when string lengths, decimal precision, or other value characteristics change.

Prefer `Add` with an explicit `SqlDbType` for production commands whose target schema is known. Don't pass C# `null` to `AddWithValue`; specify the type and assign `DBNull.Value` instead.

## Reuse command parameters

Create a parameter once and change only its <xref:Microsoft.Data.SqlClient.SqlParameter.Value%2A> when you run the same command with different values:

```csharp
const string sql = """
    SELECT TOP (1) Name, ListPrice
    FROM Production.Product
    WHERE ProductSubcategoryID = @subcategoryId
    ORDER BY ListPrice DESC;
    """;

using var command = new SqlCommand(sql, connection);
SqlParameter subcategoryId = command.Parameters.Add(
    "@subcategoryId",
    SqlDbType.Int);

foreach (int id in new[] { 1, 2, 3 })
{
    subcategoryId.Value = id;

    using SqlDataReader reader = command.ExecuteReader();
    if (reader.Read())
    {
        Console.WriteLine($"{id}: {reader.GetString(0)}");
    }
}
```

Don't add another parameter with the same name on each execution. Reuse the existing parameter or clear the collection before rebuilding it.

<a id="supply-the-parameterdirection-property"></a>

## Set parameter direction

The <xref:Microsoft.Data.SqlClient.SqlParameter.Direction%2A> property identifies how a command uses a parameter. Set it to a <xref:System.Data.ParameterDirection> value:

| Direction | Use |
| --- | --- |
| <xref:System.Data.ParameterDirection.Input> | Sends a value to a statement or stored procedure. This value is the default. |
| <xref:System.Data.ParameterDirection.Output> | Receives a value from a stored procedure output parameter. |
| <xref:System.Data.ParameterDirection.InputOutput> | Sends an initial value and receives the updated value. |
| <xref:System.Data.ParameterDirection.ReturnValue> | Receives the integer value from a T-SQL `RETURN` statement. |

For output, input/output, and return-value parameters, read <xref:Microsoft.Data.SqlClient.SqlParameter.Value%2A> only after the command finishes. If a command returns rows, close or dispose the <xref:Microsoft.Data.SqlClient.SqlDataReader> before reading those values.

For complete stored procedure examples, see [Modify data with stored procedures](modify-data-with-stored-procedures.md).

<a id="using-parameters-with-a-sqlcommand-and-a-stored-procedure"></a>

## Use parameters with a stored procedure

Set <xref:System.Data.CommandType.StoredProcedure> and use the procedure's parameter names. This example calls the `dbo.uspGetEmployeeManagers` procedure included in AdventureWorks:

```csharp
using var command = new SqlCommand(
    "dbo.uspGetEmployeeManagers",
    connection);
command.CommandType = CommandType.StoredProcedure;
command.Parameters.Add(
    "@BusinessEntityID",
    SqlDbType.Int).Value = 6;

using SqlDataReader reader = command.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine(
        $"{reader.GetString(2)} {reader.GetString(3)}");
}
```

The parameter name must match the name declared by the stored procedure.

## Configure table-valued parameters

A table-valued parameter sends multiple rows to a stored procedure or parameterized statement. To configure one:

1. Set `SqlDbType.Structured`.
1. Set <xref:Microsoft.Data.SqlClient.SqlParameter.TypeName%2A> to the database user-defined table type.
1. Assign a <xref:System.Data.DataTable>, <xref:System.Data.Common.DbDataReader>, or `IEnumerable<SqlDataRecord>` row source.

A `DataTable` with defined columns and no rows sends an empty table. An empty `IEnumerable<SqlDataRecord>` throws <xref:System.ArgumentException>; assign C# `null` to send zero rows for that input form. Don't assign `DBNull.Value` to a table-valued parameter.

For type creation, row mapping, and complete examples, see [Table-valued parameters](sql/table-valued-parameters.md).

## Configure streaming values

Assign a <xref:System.IO.Stream>, <xref:System.IO.TextReader>, or <xref:System.Xml.XmlReader> to <xref:Microsoft.Data.SqlClient.SqlParameter.Value%2A> to provide a large value incrementally. For bounded-memory transfer, use a `(max)`, **xml**, or **json** parameter type. Microsoft.Data.SqlClient buffers the source before execution for non-`(max)` character and binary types and the legacy large object types.

The parameter type must match the value:

| Value | Compatible `SqlDbType` values |
| --- | --- |
| <xref:System.IO.Stream> | `Binary`, `VarBinary`, or the legacy `Image` type |
| <xref:System.IO.TextReader> | `Char`, `NChar`, `NVarChar`, `VarChar`, the legacy `Text` and `NText` types, `Xml`, or `Json` |
| <xref:System.Xml.XmlReader> | `Char`, `NChar`, `NVarChar`, `VarChar`, the legacy `Text` and `NText` types, `Xml`, or `Json` |

Microsoft.Data.SqlClient throws <xref:System.ArgumentException>, <xref:System.InvalidCastException>, or <xref:System.Data.SqlTypes.SqlTypeException> when the object type, `SqlDbType`, or declared size is incompatible. For complete upload and download examples, see [SqlClient streaming support](sqlclient-streaming-support.md).

## Derive parameter information

<xref:Microsoft.Data.SqlClient.SqlCommandBuilder.DeriveParameters%2A> queries SQL Server for a stored procedure's parameter metadata and replaces the command's existing parameter collection:

```csharp
using var command = new SqlCommand(
    "dbo.uspGetEmployeeManagers",
    connection);
command.CommandType = CommandType.StoredProcedure;

SqlCommandBuilder.DeriveParameters(command);
command.Parameters["@BusinessEntityID"].Value = 6;
```

`DeriveParameters` requires an extra database round trip each time it's called. Derive metadata during development when necessary, then define parameters explicitly in performance-sensitive paths.

## Related content

- [Execute a command](execute-command.md)
- [Modify data with stored procedures](modify-data-with-stored-procedures.md)
- [Table-valued parameters](sql/table-valued-parameters.md)
- [SqlClient streaming support](sqlclient-streaming-support.md)
