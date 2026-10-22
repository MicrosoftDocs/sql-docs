---
title: Asynchronous programming
description: Use task-based Microsoft.Data.SqlClient methods with cancellation, command timeouts, transactions, streaming, bulk copy, and Multiple Active Result Sets (MARS).
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

# Asynchronous programming

Use task-based asynchronous methods to avoid blocking a thread while Microsoft.Data.SqlClient waits for connection and network I/O. Asynchronous execution doesn't make a command or reader safe for concurrent use, and it doesn't reduce query execution time on the server.

The examples that query application tables use the [AdventureWorks sample database](../../samples/adventureworks-install-configure.md). The C# examples require `using Microsoft.Data.SqlClient;`.

## Convert a call path to asynchronous code

Convert one complete call path at a time:

1. Make the containing method asynchronous.
1. Replace connection, command, reader, and transaction calls with their task-based counterparts.
1. `await` each operation before using the same object again.
1. Pass the caller's cancellation token through every operation that the caller can cancel.
1. Keep a finite command timeout as the database-operation limit.

Use `await` from connection open through result processing. The following excerpt assumes that `connectionString` and `sql` are already defined. It creates a `CancellationTokenSource` with a finite timeout for the operation:

```csharp
using var cancellation = new CancellationTokenSource(TimeSpan.FromSeconds(30));

await using var connection = new SqlConnection(connectionString);
await connection.OpenAsync(cancellation.Token);

using var command = new SqlCommand(sql, connection);
await using SqlDataReader reader =
    await command.ExecuteReaderAsync(cancellation.Token);

while (await reader.ReadAsync(cancellation.Token))
{
    Console.WriteLine(reader.GetValue(0));
}
```

`ExecuteReaderAsync` returns when the first result is available, not after all rows arrive. Each `ReadAsync` call advances one row, so the loop processes rows as they arrive and releases the thread only while it waits for more data from the server. The 30-second token limits the whole operation, including the read loop.

Only one asynchronous execution can be pending on a `SqlCommand`. Only one asynchronous read, result transition, or field operation can be pending on a `SqlDataReader`. Await each operation before starting the next operation on the same object.

## Asynchronous programming features

The following synchronous methods have task-based counterparts that accept a cancellation token:

| Synchronous method | Asynchronous method | Example |
| --- | --- | --- |
| `SqlConnection.Open` | `SqlConnection.OpenAsync` | [Convert a synchronous method](#synchronous-to-asynchronous-connection-open) |
| `SqlCommand.ExecuteNonQuery` | `SqlCommand.ExecuteNonQueryAsync` | [Run commands asynchronously](#run-commands-asynchronously) |
| `SqlCommand.ExecuteScalar` | `SqlCommand.ExecuteScalarAsync` | [Run commands asynchronously](#run-commands-asynchronously) |
| `SqlCommand.ExecuteXmlReader` | `SqlCommand.ExecuteXmlReaderAsync` | [Run commands asynchronously](#run-commands-asynchronously) |
| `SqlCommand.ExecuteReader` | `SqlCommand.ExecuteReaderAsync` | [Convert a call path to asynchronous code](#convert-a-call-path-to-asynchronous-code) |
| `SqlDataReader.Read` | `SqlDataReader.ReadAsync` | [Convert a call path to asynchronous code](#convert-a-call-path-to-asynchronous-code) |
| `SqlDataReader.NextResult` | `SqlDataReader.NextResultAsync` | [Read results asynchronously](#read-results-asynchronously) |
| `SqlDataReader.IsDBNull` | `SqlDataReader.IsDBNullAsync` | [Read results asynchronously](#read-results-asynchronously) |
| `SqlDataReader.GetFieldValue<T>` | `SqlDataReader.GetFieldValueAsync<T>` | [Read results asynchronously](#read-results-asynchronously) |
| `SqlBatch.ExecuteNonQuery`, `ExecuteScalar`, and `ExecuteReader` | `SqlBatch.ExecuteNonQueryAsync`, `ExecuteScalarAsync`, and `ExecuteReaderAsync` | [Execute a batch](execute-multiple-commands-sql-batch.md#execute-a-batch) |
| `SqlBulkCopy.WriteToServer` | `SqlBulkCopy.WriteToServerAsync` | [Use asynchronous operations with SqlBulkCopy](#use-asynchronous-operations-with-sqlbulkcopy) |

Use synchronous disposal when an object only implements `IDisposable`. Use `await using` for objects that implement `IAsyncDisposable`. Dispose the reader before reusing its connection.

### Run commands asynchronously

The following examples assume that `connection` is open. Use <xref:Microsoft.Data.SqlClient.SqlCommand.ExecuteScalarAsync%2A> for a single value, and pass a caller-owned cancellation token:

```csharp
using var cancellation = new CancellationTokenSource(
    TimeSpan.FromSeconds(30));
CancellationToken token = cancellation.Token;

using var count = new SqlCommand(
    "SELECT COUNT(*) FROM Production.Product;",
    connection);
int total = (int)(await count.ExecuteScalarAsync(token))!;
Console.WriteLine($"Row count: {total}");
```

Use <xref:Microsoft.Data.SqlClient.SqlCommand.ExecuteNonQueryAsync%2A> for data-changing statements. Use <xref:Microsoft.Data.SqlClient.SqlCommand.ExecuteXmlReaderAsync%2A> for a `FOR XML` query. Dispose an XML reader before you execute another command on the same connection. Without Multiple Active Result Sets (MARS), a connection allows only one open reader.

### Read results asynchronously

The following example passes the same cancellation token to each of these asynchronous calls:

- `ExecuteReaderAsync` runs the query.
- `ReadAsync` reads each row.
- `GetFieldValueAsync` reads each column value.
- `NextResultAsync` moves to the next result set.
- `IsDBNullAsync` checks for `NULL`.

```csharp
using var cancellation = new CancellationTokenSource(
    TimeSpan.FromSeconds(30));
CancellationToken token = cancellation.Token;

using var query = new SqlCommand("""
    SELECT TOP (3) ProductID, Name
    FROM Production.Product
    ORDER BY ProductID;

    SELECT TOP (1) ProductID, Color
    FROM Production.Product
    WHERE Color IS NULL;
    """, connection);
await using SqlDataReader reader = await query.ExecuteReaderAsync(token);

while (await reader.ReadAsync(token))
{
    int id = await reader.GetFieldValueAsync<int>(0, token);
    string name = await reader.GetFieldValueAsync<string>(1, token);
    Console.WriteLine($"{id}: {name}");
}

if (await reader.NextResultAsync(token) && await reader.ReadAsync(token))
{
    int productId = await reader.GetFieldValueAsync<int>(0, token);
    bool colorIsNull = await reader.IsDBNullAsync(1, token);
    Console.WriteLine($"{productId}: Color is NULL: {colorIsNull}");
}
```

<xref:Microsoft.Data.SqlClient.SqlDataReader.NextResultAsync%2A> moves to the next result set. <xref:Microsoft.Data.SqlClient.SqlDataReader.GetFieldValueAsync%2A> and <xref:Microsoft.Data.SqlClient.SqlDataReader.IsDBNullAsync%2A> read one column of the current row. When you use the default `CommandBehavior`, the row is already buffered, so these calls usually complete without waiting. When you use `CommandBehavior.SequentialAccess`, they can wait for column data from the network. For more information about sequential access, see [SqlClient streaming support](sqlclient-streaming-support.md).

<a id="synchronous-to-asynchronous-connection-open"></a>

### Convert a synchronous method

This synchronous method opens a connection, runs a command, and returns the number of rows in `sys.objects`:

```csharp
static int CountObjects(string connectionString)
{
    using var connection = new SqlConnection(connectionString);
    connection.Open();

    using var command = new SqlCommand("SELECT COUNT(*) FROM sys.objects;", connection);
    return (int)command.ExecuteScalar()!;
}
```

To convert the method to asynchronous code, make these changes:

- Change the return type to `Task<int>`.
- Add a `CancellationToken` parameter.
- Replace `Open` with `OpenAsync`, and replace `ExecuteScalar` with `ExecuteScalarAsync`.
- Dispose the connection with `await using`.

The converted method looks like this:

```csharp
static async Task<int> CountObjectsAsync(
    string connectionString,
    CancellationToken cancellationToken)
{
    await using var connection = new SqlConnection(connectionString);
    await connection.OpenAsync(cancellationToken);

    using var command = new SqlCommand("SELECT COUNT(*) FROM sys.objects;", connection);
    return (int)(await command.ExecuteScalarAsync(cancellationToken))!;
}
```

The caller creates the token and awaits the result:

```csharp
using var cancellation = new CancellationTokenSource(TimeSpan.FromSeconds(30));
int objectCount = await CountObjectsAsync(connectionString, cancellation.Token);
Console.WriteLine($"Objects: {objectCount}");
```

Both methods return the same count. The output depends on the database you connect to.

`Connect Timeout` still limits connection establishment and pool waits. Canceling the token can return control to the caller sooner.

<a id="add-the-asynchronous-feature-in-an-existing-application-mixing-old-and-new-patterns"></a>

### Add asynchronous calls to an existing application

Convert one complete call path at a time. Avoid calling `.Result`, `.Wait()`, or `GetAwaiter().GetResult()` on database tasks because those calls block and can deadlock in environments with a synchronization context.

Don't start an asynchronous command and then use the same command or reader synchronously before the task finishes. Without MARS, don't use the same connection synchronously before the task finishes.

## Add cancellation and time limits

### Cancel an asynchronous operation

Pass the same cancellation token from connection open through command execution:

```csharp
using var cancellation = new CancellationTokenSource(TimeSpan.FromSeconds(10));

await using var connection = new SqlConnection(connectionString);
await connection.OpenAsync(cancellation.Token);

using var command = new SqlCommand(
    "SELECT COUNT(*) FROM Production.Product;",
    connection);
await command.ExecuteScalarAsync(cancellation.Token);
```

Cancellation is best effort. The operation might finish before cancellation takes effect, and the observed exception depends on where cancellation occurs. Don't assume that receiving a cancellation request means the server rolled back completed work. When a set of changes must succeed or fail as a unit, use an explicit transaction. For more information, see [Local transactions](local-transactions.md).

<xref:Microsoft.Data.SqlClient.SqlCommand.Cancel%2A> requests cancellation of the current command. It doesn't replace a caller-owned cancellation token. The operation might finish before the cancellation request takes effect.

After cancellation or timeout, await the operation and dispose the reader and command. Inspect the connection state before reuse. A connection-breaking failure requires a new connection.

### Configure timeouts

Connection and command timeouts cover different phases:

| Setting | Covers |
| --- | --- |
| `Connect Timeout` | Connection establishment and waits for a pooled connection. |
| `CommandTimeout` | Command execution and cumulative network-read waits. |
| Cancellation token | A caller-controlled request to stop waiting. |

`CommandTimeout` is measured in seconds. `0` means no command time limit. The default is the connection's `Command Timeout` value when configured, otherwise 30 seconds.

For a reader, the command timeout applies to each provider read operation's cumulative network wait. It doesn't include time spent processing a row in application code. A timeout can occur before the first row or while fetching a later row.

### Combine cancellation and command timeout

Use a finite command timeout and caller cancellation together. This example assumes that `connection` is open:

```csharp
using var cancellation = new CancellationTokenSource(TimeSpan.FromSeconds(30));

using var command = new SqlCommand("""
    SELECT TOP (10) SalesOrderID, OrderDate
    FROM Sales.SalesOrderHeader
    ORDER BY SalesOrderID;
    """, connection)
{
    CommandTimeout = 15
};

await using SqlDataReader reader =
    await command.ExecuteReaderAsync(cancellation.Token);

while (await reader.ReadAsync(cancellation.Token))
{
    Console.WriteLine($"{reader.GetInt32(0)} | {reader.GetDateTime(1):O}");
}
```

If the caller cancels first, the awaited call throws an exception. The exception type depends on where cancellation occurs. If the command timeout expires first, the provider reports a timeout. In either case, the command might finish successfully before the cancellation or timeout takes effect.

## Advanced asynchronous patterns

<a id="use-the-base-provider-model-and-the-asynchronous-feature"></a>

### Use asynchronous methods through base provider types

Provider-independent code can use <xref:System.Data.Common.DbConnection>, <xref:System.Data.Common.DbCommand>, and <xref:System.Data.Common.DbDataReader> asynchronous methods. Create parameters through the provider command and avoid downcasts unless the application needs a Microsoft.Data.SqlClient-specific feature.

<a id="use-sql-transactions-and-the-new-asynchronous-feature"></a>

### Use task-based SQL transaction methods

Use the task-based local transaction methods when the target framework exposes them. The following example assumes that `connection` is open, `sql` holds the command text, and the caller passes in `cancellationToken`:

```csharp
await using SqlTransaction transaction =
    (SqlTransaction)await connection.BeginTransactionAsync(cancellationToken);

using var command = new SqlCommand(sql, connection, transaction);

try
{
    await command.ExecuteNonQueryAsync(cancellationToken);
    await transaction.CommitAsync(cancellationToken);
}
catch (Exception operationException)
{
    try
    {
        await transaction.RollbackAsync(CancellationToken.None);
    }
    catch (Exception rollbackException)
    {
        throw new AggregateException(
            "The operation and transaction rollback both failed.",
            operationException,
            rollbackException);
    }

    throw;
}
```

Use a separate cleanup token for rollback. The operation token might already be canceled. Handle a rollback failure without discarding the original operation exception. This example preserves both exceptions in an <xref:System.AggregateException>. An application can instead log the rollback exception and rethrow the original exception.

These Microsoft.Data.SqlClient methods use the inherited <xref:System.Data.Common.DbConnection> and <xref:System.Data.Common.DbTransaction> implementations, which call the synchronous transaction methods and return a completed task. Awaiting them doesn't release the calling thread while the transaction operation runs. Cancellation is checked before the operation starts and doesn't interrupt an operation in progress. Command execution inside the transaction is still asynchronous.

<a id="use-distributed-transactions-and-the-new-asynchronous-feature"></a>

### Use asynchronous distributed transactions

Asynchronous commands can execute inside a <xref:System.Transactions.TransactionScope>. The scope sets <xref:System.Transactions.Transaction.Current%2A?displayProperty=nameWithType>, which .NET calls the *ambient transaction*. A connection that opens inside the scope enlists in that transaction automatically, because the `Enlist` connection keyword defaults to `true`. Configure `TransactionScopeAsyncFlowOption.Enabled` when the transaction must flow across `await`. Distributed transactions have platform, server, and coordinator requirements. For more information, see [System.Transactions integration](system-transactions-integration-with-sql-server.md).

<a id="asynchronous-operations-with-sqlbulkcopy"></a>

### Use asynchronous operations with SqlBulkCopy

Use <xref:Microsoft.Data.SqlClient.SqlBulkCopy.WriteToServerAsync%2A> for asynchronous bulk copy. Keep the source reader or row collection alive until the returned task completes. Configure batch size, transaction behavior, and timeout for the operation. For more information, see [Bulk copy operations](sql/bulk-copy-operations-sql-server.md).

### Asynchronously use multiple commands with MARS

MARS allows more than one pending command on one physical connection. Enable it with `MultipleActiveResultSets=True` only when the application needs that connection-sharing model.

MARS isn't required for asynchronous programming. Without MARS, finish or dispose the active reader before executing another command on the same connection. A second connection is often clearer for truly independent work.

For session behavior and transaction restrictions, see [Enable Multiple Active Result Sets](sql/enable-multiple-active-result-sets.md).

### Asynchronously read and update data with MARS

When you enable MARS, one command can read rows while another command executes on the same connection. Await each command and reader operation separately. MARS operations aren't thread safe, and server execution can still serialize at yield points.

When read and update operations must be atomic, assign the same local transaction to every command and test the transaction's MARS behavior. Don't commit while another operation in that transaction is still pending.

## Legacy asynchronous programming

Microsoft.Data.SqlClient retains the Asynchronous Programming Model (APM) methods `BeginExecuteNonQuery`, `BeginExecuteReader`, and `BeginExecuteXmlReader` for compatibility. Don't use them in new code. `CommandTimeout` is ignored by these old-style asynchronous calls and is honored by task-based methods such as `ExecuteReaderAsync`.

The legacy methods don't require `Asynchronous Processing=true`. Microsoft.Data.SqlClient 4.0 and later versions don't support the `Asynchronous Processing` connection property.

## Related content

- Start with the synchronous [command execution workflow](execute-command.md).
- To process rows, [retrieve data with SqlDataReader](retrieve-data-by-datareader.md).
- To transfer large values, use [SqlClient streaming support](sqlclient-streaming-support.md).
- For MARS session and transaction behavior, see [Multiple Active Result Sets](sql/multiple-active-result-sets-mars.md).
