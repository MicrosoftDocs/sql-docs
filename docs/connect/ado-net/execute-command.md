---
title: Execute a command
description: Execute SQL statements and stored procedures with SqlCommand, choose the correct execution method, and control transactions, timeouts, and cancellation.
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

# Execute a command

<a id="executing-a-command"></a>

Use <xref:Microsoft.Data.SqlClient.SqlCommand> to execute Transact-SQL (T-SQL) statements or stored procedures. A command needs an open <xref:Microsoft.Data.SqlClient.SqlConnection>. Dispose the command after use, and dispose any reader before you reuse the connection.

The examples in this article assume that an open `SqlConnection` named `connection` targets the [AdventureWorks sample database](../../samples/adventureworks-install-configure.md).

The C# examples require `using Microsoft.Data.SqlClient;`. Examples that use `SqlDbType`, `CommandType`, or `CommandBehavior` also require `using System.Data;`.

For connection setup, authentication, and encryption, see [Connection strings](connection-strings.md) and [Encryption and certificate validation](encryption-and-certificate-validation.md).

## Choose an execution method

Choose the method that matches the expected result:

| Method | Use when | Result |
| --- | --- | --- |
| <xref:Microsoft.Data.SqlClient.SqlCommand.ExecuteNonQuery%2A> | The command changes data or schema. | The number of rows affected. `-1` can occur when no count is available, including when `SET NOCOUNT ON` suppresses counts. |
| <xref:Microsoft.Data.SqlClient.SqlCommand.ExecuteScalar%2A> | You need one value. | The first column of the first row, or `null` when the result has no rows. A database `NULL` is returned as <xref:System.DBNull.Value>. |
| <xref:Microsoft.Data.SqlClient.SqlCommand.ExecuteReader%2A> | You need rows, multiple result sets, or schema. | A <xref:Microsoft.Data.SqlClient.SqlDataReader>. |
| <xref:Microsoft.Data.SqlClient.SqlCommand.ExecuteXmlReader%2A> | The command returns XML in the shape required by `ExecuteXmlReader`. | An <xref:System.Xml.XmlReader>. |

Each method has a task-based asynchronous counterpart. Pass a <xref:System.Threading.CancellationToken> when the caller needs to stop waiting. For cancellation and timeout behavior, see [Asynchronous programming](asynchronous-programming.md).

<a id="example"></a>

## Execute a text command

`CommandType.Text` is the default. Place the statement in <xref:Microsoft.Data.SqlClient.SqlCommand.CommandText%2A> and pass values through parameters:

```csharp
const string sql = """
    SELECT ProductID, Name, ListPrice
    FROM Production.Product
    WHERE ProductSubcategoryID = @subcategoryId
    ORDER BY ListPrice DESC;
    """;

using var command = new SqlCommand(sql, connection);
command.Parameters.Add("@subcategoryId", SqlDbType.Int).Value = 2;

using SqlDataReader reader = command.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine(
        $"{reader.GetInt32(0)} | {reader.GetString(1)} | {reader.GetDecimal(2):C}");
}
```

For parameter types, sizes, null values, and directions, see [Configure parameters](configure-parameters.md). Don't concatenate untrusted values into `CommandText`.

## Execute a stored procedure

Set `CommandType.StoredProcedure` and use the schema-qualified procedure name:

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

Microsoft.Data.SqlClient supports `CommandType.Text` and `CommandType.StoredProcedure`. Setting `CommandType.TableDirect` throws <xref:System.ArgumentOutOfRangeException>.

## Check what each method returns

Use `ExecuteScalar` when you need one value:

```csharp
using var command = new SqlCommand(
    "SELECT COUNT(*) FROM Production.Product WHERE SellEndDate IS NULL;",
    connection);

int currentProductCount = (int)command.ExecuteScalar()!;
Console.WriteLine($"Current products: {currentProductCount}");
```

`ExecuteScalar` returns `null` when the result has no rows and `DBNull.Value` when the first value is a database `NULL`.

Use `ExecuteNonQuery` for data-changing statements. This example rolls back its change so AdventureWorks remains unchanged:

```csharp
using SqlTransaction transaction = connection.BeginTransaction();

using var command = new SqlCommand("""
    UPDATE Sales.SalesOrderHeader
    SET Status = @status
    WHERE SalesOrderID = @salesOrderId;
    """, connection, transaction);
command.Parameters.Add("@status", SqlDbType.TinyInt).Value = 2;
command.Parameters.Add("@salesOrderId", SqlDbType.Int).Value = 43659;

int rowsAffected = command.ExecuteNonQuery();
Console.WriteLine($"Rows affected: {rowsAffected}");

transaction.Rollback();
```

`SET NOCOUNT ON` suppresses affected-row counts, so `ExecuteNonQuery` can return `-1` even when a statement changes data.

Use `ExecuteXmlReader` for a `FOR XML` result:

```csharp
using var command = new SqlCommand("""
    SELECT TOP (3) ProductID, Name
    FROM Production.Product
    ORDER BY ProductID
    FOR XML PATH('Product'), ROOT('Products');
    """, connection);

using System.Xml.XmlReader reader = command.ExecuteXmlReader();
reader.MoveToContent();
Console.WriteLine(reader.ReadOuterXml());
```

## Use a local transaction

When a connection has an active local <xref:Microsoft.Data.SqlClient.SqlTransaction>, assign that transaction to every command executed on the connection. This example rolls back its change so AdventureWorks remains unchanged:

```csharp
using SqlTransaction transaction = connection.BeginTransaction();

using var command = new SqlCommand("""
    UPDATE Sales.SalesOrderHeader
    SET Status = @status
    WHERE SalesOrderID = @salesOrderId;
    """, connection, transaction);
command.Parameters.Add("@status", SqlDbType.TinyInt).Value = 2;
command.Parameters.Add("@salesOrderId", SqlDbType.Int).Value = 43659;

command.ExecuteNonQuery();
transaction.Rollback();
```

Call `Commit` instead of `Rollback` only when the application accepts the complete transaction. A command that doesn't reference the active transaction throws <xref:System.InvalidOperationException>. Execution also fails if the transaction belongs to another connection. For transaction boundaries and rollback, see [Local transactions](local-transactions.md).

## Set a command timeout

<xref:Microsoft.Data.SqlClient.SqlCommand.CommandTimeout%2A> limits command execution and cumulative network-read waits in seconds. A command uses the connection's `Command Timeout` setting when one is configured. Otherwise, the default is 30 seconds. A value of `0` has no time limit. Negative values throw <xref:System.ArgumentException>.

When the timeout expires, the command throws <xref:Microsoft.Data.SqlClient.SqlException> with `Number` set to `-2`:

```csharp
using var command = new SqlCommand(
    "WAITFOR DELAY '00:00:05';",
    connection)
{
    CommandTimeout = 1
};

try
{
    command.ExecuteNonQuery();
}
catch (SqlException exception) when (exception.Number == -2)
{
    Console.WriteLine("The command timed out.");
}
```

`Connect Timeout` limits connection establishment and pool waits. `CommandTimeout` starts after command execution begins. For more information, see [Connection options](connection-options.md#timeout-options).

Use a finite timeout and a cancellation token together. The timeout sets a limit on the database operation. Canceling the token can return control to the caller before the timeout expires. Don't assume that cancellation and timeout produce the same exception.

## Reuse and prepare commands

Add a parameter once, and then change its value after each execution finishes:

```csharp
using var command = new SqlCommand("""
    SELECT TOP (1) Name, ListPrice
    FROM Production.Product
    WHERE ProductSubcategoryID = @subcategoryId
    ORDER BY ListPrice DESC;
    """, connection);
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

Call <xref:Microsoft.Data.SqlClient.SqlCommand.Prepare%2A> only when measurements justify it. `Prepare` doesn't prepare stored procedures or text commands without parameters. For parameterized text, set complete metadata such as `Size`, `Precision`, and `Scale` on each parameter before you call `Prepare`.

<a id="troubleshooting-commands"></a>

## Troubleshoot commands

Catch <xref:Microsoft.Data.SqlClient.SqlException> around the database operation and inspect its `Errors` collection when the server returns more than one error. For command errors, warnings, and informational messages, see [Handle errors and messages](handle-errors-messages.md).

Use [diagnostic counters](diagnostic-counters.md), [event source tracing](enable-eventsource-tracing.md), and the [SqlClient troubleshooting guide](sqlclient-troubleshooting-guide.md) when an error requires more context. Apply [configurable retry logic](configurable-retry-logic.md) only when repeating the command is safe.

## Related content

- [Configure parameters](configure-parameters.md)
- [Retrieve data with SqlDataReader](retrieve-data-by-datareader.md)
- [Modify data with stored procedures](modify-data-with-stored-procedures.md)
- [Handle errors and messages](handle-errors-messages.md)
