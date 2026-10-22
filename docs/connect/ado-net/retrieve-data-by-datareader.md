---
title: Retrieve data with SqlDataReader
description: Read rows, nullable values, multiple result sets, schema, and affected-row counts with SqlDataReader.
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

# Retrieve data by using a DataReader

<a id="retrieve-data-by-a-datareader"></a>

The <xref:Microsoft.Data.SqlClient.SqlDataReader> reads a forward-only sequence of results from a SQL database. It doesn't load the complete result into a `DataSet`, so it's suitable for processing rows as they arrive.

The examples in this article assume that an open <xref:Microsoft.Data.SqlClient.SqlConnection> named `connection` targets the [AdventureWorks sample database](../../samples/adventureworks-install-configure.md).

The C# examples require `using Microsoft.Data.SqlClient;`. Examples that use `SqlDbType` or `CommandBehavior` also require `using System.Data;`.

## Read rows

The basic reader workflow is:

1. Execute a command with `ExecuteReader` or `ExecuteReaderAsync`.
1. Resolve column ordinals for the current result.
1. Call `Read` once for each row.
1. Check nullable columns with `IsDBNull` before calling a typed getter.
1. Dispose the reader before reusing the connection or reading output parameters.

This example caches column ordinals, uses typed getters, and checks the nullable `Color` column:

```csharp
using var command = new SqlCommand("""
    SELECT ProductID, Name, Color
    FROM Production.Product
    WHERE ProductSubcategoryID = @subcategoryId
    ORDER BY ProductID;
    """, connection);
command.Parameters.Add("@subcategoryId", SqlDbType.Int).Value = 2;

using SqlDataReader reader = command.ExecuteReader();
int idOrdinal = reader.GetOrdinal("ProductID");
int nameOrdinal = reader.GetOrdinal("Name");
int colorOrdinal = reader.GetOrdinal("Color");

while (reader.Read())
{
    int id = reader.GetInt32(idOrdinal);
    string name = reader.GetString(nameOrdinal);
    string? color = reader.IsDBNull(colorOrdinal)
        ? null
        : reader.GetString(colorOrdinal);

    Console.WriteLine($"{id} | {name} | {color ?? "(no color)"}");
}
```

Calling `GetString` on a `NULL` value throws <xref:System.Data.SqlTypes.SqlNullValueException>, so check `IsDBNull` first.

`HasRows` reports whether the current result contains rows, but you can usually call `Read` directly. For an empty result, the first `Read` call returns `false`:

```csharp
using var command = new SqlCommand("""
    SELECT ProductID
    FROM Production.Product
    WHERE ProductID = @productId;
    """, connection);
command.Parameters.Add("@productId", SqlDbType.Int).Value = -1;

using SqlDataReader reader = command.ExecuteReader();
Console.WriteLine($"HasRows: {reader.HasRows}");
Console.WriteLine($"First Read: {reader.Read()}");
```

For large values, execute with `CommandBehavior.SequentialAccess` and use `GetStream`, `GetTextReader`, or `GetXmlReader`. For more information, see [SqlClient streaming support](sqlclient-streaming-support.md).

## Close the DataReader

Dispose the reader as soon as processing finishes. An `await using` or `using` statement closes it even when row processing throws.

Output parameters, input/output parameters, and stored procedure return values aren't final until the reader closes:

```csharp
using var command = new SqlCommand("""
    SELECT TOP (5) ProductID, Name
    FROM Production.Product
    ORDER BY ProductID;
    SET @count = @@ROWCOUNT;
    """, connection);

SqlParameter count = command.Parameters.Add("@count", SqlDbType.Int);
count.Direction = ParameterDirection.Output;

using (SqlDataReader reader = command.ExecuteReader())
{
    while (reader.Read())
    {
        Console.WriteLine(reader.GetString(1));
    }
}

Console.WriteLine($"Rows returned: {count.Value}");
```

Without Multiple Active Result Sets (MARS), an open reader keeps its connection busy and another command can't execute on that connection. With MARS enabled, separate commands can have pending results on one connection, but their operations still aren't thread safe. For more information, see [Multiple Active Result Sets](sql/multiple-active-result-sets-mars.md).

If the command passed `CommandBehavior.CloseConnection`, closing the reader also closes the connection. Otherwise, the code that opened the connection remains responsible for disposing it.

## Read affected-row counts

<xref:Microsoft.Data.SqlClient.SqlDataReader.RecordsAffected%2A> reports the rows changed by `INSERT`, `UPDATE`, and `DELETE` statements in the command. Read it after the reader closes. This example rolls back its update:

```csharp
using SqlTransaction transaction = connection.BeginTransaction();
using var command = new SqlCommand("""
    UPDATE Production.Product
    SET ModifiedDate = ModifiedDate
    WHERE ProductID IN (706, 707);

    SELECT ProductID, Name
    FROM Production.Product
    WHERE ProductID IN (706, 707)
    ORDER BY ProductID;
    """, connection, transaction);

SqlDataReader reader = command.ExecuteReader();
using (reader)
{
    while (reader.Read())
    {
        Console.WriteLine($"{reader.GetInt32(0)} | {reader.GetString(1)}");
    }
}

Console.WriteLine($"RecordsAffected: {reader.RecordsAffected}");
transaction.Rollback();
```

For a `SELECT`-only command, `RecordsAffected` is `-1`. `SET NOCOUNT ON` suppresses the counts, which also leaves the value at `-1`.

<a id="retrieve-multiple-result-sets-using-nextresult"></a>

## Retrieve multiple result sets by using NextResult

One command or stored procedure can return multiple results. Process the current result completely, and then call `NextResult` or `NextResultAsync`:

```csharp
using var command = new SqlCommand("""
    SELECT TOP (3) ProductID, Name
    FROM Production.Product
    ORDER BY ProductID;

    SELECT COUNT(*) AS ProductCount
    FROM Production.Product;
    """, connection);

using SqlDataReader reader = command.ExecuteReader();
int result = 1;

do
{
    Console.WriteLine($"Result {result++}: {reader.FieldCount} column(s)");
    while (reader.Read())
    {
        var values = new object[reader.FieldCount];
        reader.GetValues(values);
        Console.WriteLine(string.Join(" | ", values));
    }
}
while (reader.NextResult());
```

Column count, names, ordinals, and types can change for each result. Recompute cached ordinals after `NextResult` returns `true`.

Only one asynchronous operation can be pending on a reader at a time. Await `ReadAsync`, `NextResultAsync`, or an asynchronous field operation before starting another operation on the same reader.

## Get schema information from the DataReader

Call <xref:Microsoft.Data.SqlClient.SqlDataReader.GetColumnSchema%2A> for a strongly typed description of the current result's columns. Call <xref:Microsoft.Data.SqlClient.SqlDataReader.GetSchemaTable%2A> when existing code needs the schema in a <xref:System.Data.DataTable>:

```csharp
using var command = new SqlCommand("""
    SELECT ProductID, Name, Color
    FROM Production.Product;
    """, connection);

using SqlDataReader reader = command.ExecuteReader(
    CommandBehavior.SchemaOnly);

foreach (System.Data.Common.DbColumn column in reader.GetColumnSchema())
{
    Console.WriteLine(
        $"{column.ColumnOrdinal}: {column.ColumnName} " +
        $"({column.DataTypeName}), nullable={column.AllowDBNull}");
}
```

`CommandBehavior.SchemaOnly` requests column metadata without rows. Schema information describes the current result set. Request it again after moving to the next result. Depending on the query and server permissions, some base-table metadata might be unavailable.

## Related content

- [Execute a command](execute-command.md)
- [SqlClient streaming support](sqlclient-streaming-support.md)
- [Asynchronous programming](asynchronous-programming.md)
- [DataAdapters and DataReaders](dataadapters-datareaders.md)
