---
title: Execute multiple commands with SqlBatch
description: Group and execute multiple SQL statements or stored procedures with SqlBatch, including parameters, multiple results, transactions, cancellation, and error attribution.
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

# Execute multiple commands with SqlBatch

Use <xref:Microsoft.Data.SqlClient.SqlBatch> to send an ordered collection of <xref:Microsoft.Data.SqlClient.SqlBatchCommand> objects through one open <xref:Microsoft.Data.SqlClient.SqlConnection>. Each batch command has its own command text, command type, parameters, command behavior, and rows-affected result.

Use `SqlBatch` when an application has distinct commands that it wants to send together. Use one parameterized `SqlCommand` when the work is one statement or stored procedure. Use <xref:Microsoft.Data.SqlClient.SqlBulkCopy> or a table-valued parameter for large row sets.

The examples in this article assume that an open `SqlConnection` named `connection` targets the [AdventureWorks sample database](../../samples/adventureworks-install-configure.md).

The C# examples require `using Microsoft.Data.SqlClient;`. Examples that use `SqlDbType` or `CommandBehavior` also require `using System.Data;`. Examples that use `DbDataReader` require `using System.Data.Common;`.

## Version and platform support

The examples target Microsoft.Data.SqlClient 7.1.0. Select a package version and target framework that the [Microsoft.Data.SqlClient support lifecycle](sqlclient-driver-support-lifecycle.md) supports for your application.

`SqlBatch` derives from `DbBatch` on modern .NET. Microsoft.Data.SqlClient also exposes `SqlBatch` in its .NET Framework and .NET Standard 2.0 package assets. The public surface can differ by target framework, so compile the application for every target you ship.

<a id="example"></a>
<a id="execute-a-batch"></a>

## Create a batch

Create a batch for an open connection, and then add commands in execution order:

```csharp
using var batch = new SqlBatch(connection);
batch.BatchCommands.Add(new SqlBatchCommand("""
    SELECT TOP (3) ProductID, Name
    FROM Production.Product
    ORDER BY ProductID;
    """));
batch.BatchCommands.Add(new SqlBatchCommand("""
    SELECT TOP (3) SalesOrderID, OrderDate
    FROM Sales.SalesOrderHeader
    ORDER BY SalesOrderID;
    """));

using DbDataReader reader = batch.ExecuteReader();
do
{
    while (reader.Read())
    {
        Console.WriteLine($"{reader.GetValue(0)} | {reader.GetValue(1)}");
    }
}
while (reader.NextResult());
```

`CommandType.Text` is the default. Set a batch command to `CommandType.StoredProcedure` for a stored procedure. Setting `CommandType.TableDirect` throws <xref:System.ArgumentOutOfRangeException>.

## Add parameters to batch commands

Parameters belong to an individual `SqlBatchCommand`. The same placeholder name in two batch commands doesn't share a value:

```csharp
using var batch = new SqlBatch(connection);

var products = new SqlBatchCommand("""
    SELECT ProductID, Name
    FROM Production.Product
    WHERE ProductSubcategoryID = @id
    ORDER BY ProductID;
    """);
products.Parameters.Add("@id", SqlDbType.Int).Value = 2;
batch.BatchCommands.Add(products);

var orders = new SqlBatchCommand("""
    SELECT SalesOrderID, OrderDate
    FROM Sales.SalesOrderHeader
    WHERE SalesPersonID = @id
    ORDER BY SalesOrderID;
    """);
orders.Parameters.Add("@id", SqlDbType.Int).Value = 279;
batch.BatchCommands.Add(orders);

using DbDataReader reader = batch.ExecuteReader();
```

Set each parameter's SQL type and applicable size, precision, and scale from the database contract. For more information, see [Configure parameters](configure-parameters.md).

## Execute data-changing commands

Use `ExecuteNonQuery` or `ExecuteNonQueryAsync` when the batch changes data and you don't need result rows. The returned value is the total rows affected when row counts are available. After execution, each command's `RecordsAffected` reports its own count.

This example rolls back its changes:

```csharp
using SqlTransaction transaction = connection.BeginTransaction();
using var batch = new SqlBatch(connection, transaction);

var firstUpdate = new SqlBatchCommand("""
    UPDATE Sales.SalesOrderHeader
    SET Status = @status
    WHERE SalesOrderID = @salesOrderId;
    """);
firstUpdate.Parameters.Add("@status", SqlDbType.TinyInt).Value = 2;
firstUpdate.Parameters.Add("@salesOrderId", SqlDbType.Int).Value = 43659;
batch.BatchCommands.Add(firstUpdate);

var secondUpdate = new SqlBatchCommand("""
    UPDATE Sales.SalesOrderHeader
    SET Status = @status
    WHERE SalesOrderID = @salesOrderId;
    """);
secondUpdate.Parameters.Add("@status", SqlDbType.TinyInt).Value = 2;
secondUpdate.Parameters.Add("@salesOrderId", SqlDbType.Int).Value = 43660;
batch.BatchCommands.Add(secondUpdate);

int totalRows = batch.ExecuteNonQuery();
Console.WriteLine($"Total rows: {totalRows}");

foreach (SqlBatchCommand command in batch.BatchCommands)
{
    Console.WriteLine(command.RecordsAffected);
}

transaction.Rollback();
```

## Read multiple results

Use `ExecuteReader` or `ExecuteReaderAsync` when any command returns rows. The reader exposes results in batch-command order. Call `NextResult` or `NextResultAsync` until it returns `false`.

A command can produce no row set, one row set, or multiple row sets. Don't assume one `NextResult` call maps to one `SqlBatchCommand`.

## Use command behavior

Pass a batch-level <xref:System.Data.CommandBehavior> to `ExecuteReader` or `ExecuteReaderAsync` when the reader needs sequential access or must close the connection:

```csharp
using var batch = new SqlBatch(connection);
batch.BatchCommands.Add(new SqlBatchCommand("""
    SELECT DocumentSummary
    FROM Production.Document
    WHERE DocumentSummary IS NOT NULL;
    """));

using DbDataReader reader =
    batch.ExecuteReader(CommandBehavior.SequentialAccess);

char[] buffer = new char[4096];
while (reader.Read())
{
    using TextReader summary = reader.GetTextReader(0);
    int count;
    while ((count = summary.Read(buffer, 0, buffer.Length)) > 0)
    {
        Console.Out.Write(buffer, 0, count);
    }
    Console.WriteLine();
}
```

The batch-level overloads accept only `CommandBehavior.Default`, `CommandBehavior.SequentialAccess`, `CommandBehavior.CloseConnection`, and combinations of those values. Other valid values, such as `SingleRow`, `SchemaOnly`, and `KeyInfo`, throw <xref:System.ArgumentOutOfRangeException> when passed to a batch-level overload. The batch-level behavior overloads are available starting in Microsoft.Data.SqlClient 7.1.

Set <xref:Microsoft.Data.SqlClient.SqlBatchCommand.CommandBehavior%2A> on an individual command for options such as `SingleRow`, `SchemaOnly`, and `KeyInfo`. Use the batch-level overload for `SequentialAccess` and `CloseConnection`.

## Use a transaction

`SqlBatch` doesn't make its commands atomic. Use an explicit transaction when all commands must commit or roll back together. This example rolls back so AdventureWorks remains unchanged:

```csharp
using SqlTransaction transaction = connection.BeginTransaction();
using var batch = new SqlBatch(connection, transaction);

batch.BatchCommands.Add(new SqlBatchCommand("""
    UPDATE Sales.SalesOrderHeader
    SET Status = 2
    WHERE SalesOrderID = 43659;
    """));
batch.BatchCommands.Add(new SqlBatchCommand("""
    UPDATE Sales.SalesOrderHeader
    SET Status = 2
    WHERE SalesOrderID = 43660;
    """));

batch.ExecuteNonQuery();
transaction.Rollback();
```

Call `Commit` instead of `Rollback` only when the application accepts the complete batch. Assign the transaction before execution. The transaction and batch must use the same connection. SqlClient doesn't override the asynchronous transaction methods, so `BeginTransactionAsync`, `CommitAsync`, and `RollbackAsync` complete synchronously. For more information, see [Use task-based SQL transaction methods](asynchronous-programming.md#use-sql-transactions-and-the-new-asynchronous-feature).

## Handle timeouts, cancellation, and errors

Set <xref:Microsoft.Data.SqlClient.SqlBatch.Timeout%2A> in seconds for the batch. The batch uses the connection's `Command Timeout` setting when you configure one. Otherwise, the default is 30 seconds. A value of `0` has no time limit, and negative values throw <xref:System.ArgumentException>. Pass a cancellation token to an asynchronous execution method when the caller needs to stop waiting, or call <xref:Microsoft.Data.SqlClient.SqlBatch.Cancel%2A>. Cancellation is best effort. The operation might finish before cancellation takes effect.

When execution fails, catch <xref:Microsoft.Data.SqlClient.SqlException> and inspect every item in its `Errors` collection. <xref:Microsoft.Data.SqlClient.SqlException.BatchCommand%2A> identifies the associated `SqlBatchCommand` when that information is available:

```csharp
using var batch = new SqlBatch(connection);
batch.BatchCommands.Add(new SqlBatchCommand(
    "SELECT TOP (1) ProductID FROM Production.Product;"));
batch.BatchCommands.Add(new SqlBatchCommand("SELECT 1 / 0;"));

try
{
    using DbDataReader reader = batch.ExecuteReader();
    do
    {
        while (reader.Read())
        {
            Console.WriteLine(reader.GetValue(0));
        }
    }
    while (reader.NextResult());
}
catch (SqlException exception)
{
    Console.WriteLine(
        exception.BatchCommand is null
            ? "The batch failed."
            : $"Failed command: {exception.BatchCommand.CommandText}");
}
```

Reader execution is incremental. An error from a later batch command might surface when `Read` or `NextResult` advances to that command, so process the reader inside the `try` block.

Don't log parameter values that contain credentials, tokens, or sensitive data. For error fields and informational messages, see [Handle errors and messages](handle-errors-messages.md).

## Related content

- [Execute a command](execute-command.md)
- [Configure parameters](configure-parameters.md)
- [Retrieve data with SqlDataReader](retrieve-data-by-datareader.md)
- [Asynchronous programming](asynchronous-programming.md)
