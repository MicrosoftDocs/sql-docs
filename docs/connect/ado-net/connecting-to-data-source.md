---
title: Connect to a data source with Microsoft.Data.SqlClient
description: Create, open, use, monitor, cancel, and dispose Microsoft.Data.SqlClient connections safely in .NET applications.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, paulmedynski, cmalhotra
ms.date: 09/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
dev_langs:
  - csharp
ai-usage: ai-assisted
---

# Connect to a data source with Microsoft.Data.SqlClient

<xref:Microsoft.Data.SqlClient.SqlConnection> represents one logical connection to SQL Server, Azure SQL, or another supported SQL Server-compatible endpoint. Opening the object gets a physical connection from the connection pool when one is available. Closing or disposing it returns that physical connection to the pool.

Use short-lived `SqlConnection` objects for units of work. Don't keep one global connection open for the application.

## Build the connection configuration

Load a connection string from the application's configuration system. Use <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder> when code needs to validate or add settings:

```csharp
string configuredConnectionString =
    configuration.GetConnectionString("Orders")
    ?? throw new InvalidOperationException(
        "Connection string 'Orders' wasn't configured.");

var builder = new SqlConnectionStringBuilder(configuredConnectionString)
{
    ApplicationName = "Orders.Api",
};
```

Create the `SqlConnection` from `builder.ConnectionString`. Don't concatenate user input into the string. For authentication patterns, secure storage, and syntax, see [Connection strings](connection-strings.md).

## Open and dispose connections

Call <xref:Microsoft.Data.SqlClient.SqlConnection.Open%2A> in synchronous code or <xref:Microsoft.Data.SqlClient.SqlConnection.OpenAsync%2A> in asynchronous code. Open a new logical connection for each independent operation:

```csharp
public static async Task<string?> LoadOrderStatusAsync(
    string connectionString,
    int orderId,
    CancellationToken cancellationToken)
{
    await using var connection = new SqlConnection(connectionString);
    await connection.OpenAsync(cancellationToken);

    const string sql = """
        SELECT Status
        FROM Sales.Orders
        WHERE OrderId = @orderId;
        """;

    using var command =
        new SqlCommand(sql, connection) { CommandTimeout = 30 };
    command.Parameters.Add(
        new SqlParameter("@orderId", SqlDbType.Int) { Value = orderId });

    object? value =
        await command.ExecuteScalarAsync(cancellationToken);
    return value is null or DBNull ? null : (string)value;
}
```

The `await using` statement disposes the connection on success, error, or cancellation. With pooling enabled, disposal normally resets and returns the physical connection instead of closing its network socket.

Dispose readers and commands before the connection that owns them. Don't rely on garbage collection or a finalizer to return connections to the pool.

## Use asynchronous APIs

Use asynchronous calls for network-bound database work in web servers, services, user interfaces, and workers:

- `OpenAsync(cancellationToken)`
- `ExecuteNonQueryAsync(cancellationToken)`
- `ExecuteReaderAsync(cancellationToken)`
- `ExecuteScalarAsync(cancellationToken)`
- `ReadAsync(cancellationToken)`

You don't need `Asynchronous Processing=true`. Microsoft.Data.SqlClient 4.0 and later versions don't support that connection string keyword.

Don't start another operation on a connection, command, or reader before the current asynchronous operation finishes.

## Apply cancellation and timeouts

Pass the caller's <xref:System.Threading.CancellationToken> through every asynchronous database call. Cancellation asks the provider to stop the pending work, but completion isn't guaranteed to be immediate. Continue to use bounded connection and command timeouts.

These controls have separate scopes:

| Control | Scope |
| --- | --- |
| `Connect Timeout` | Connection establishment or waiting for a pooled connection |
| `SqlCommand.CommandTimeout` | One command execution |
| `CancellationToken` | Caller-requested cancellation of an asynchronous operation |

A timeout or cancellation doesn't prove that the server rolled back an operation. Use a transaction when multiple changes must commit or roll back as one unit, and make retry decisions from the operation's idempotency and transaction outcome.

## Understand connection state

The <xref:Microsoft.Data.SqlClient.SqlConnection.State%2A> property returns a snapshot from the <xref:System.Data.ConnectionState> enumeration.

| State | Meaning |
| --- | --- |
| `Closed` | The logical connection isn't open. |
| `Connecting` | An open operation is in progress. |
| `Open` | The logical connection is open. |

Don't use `State` as a health check before every command. The network can fail after any check. Execute the operation and handle the resulting exception.

The driver normally reports closed-to-open and open-to-closed transitions. Don't depend on observing `Executing`, `Fetching`, or `Broken` as application lifecycle phases.

The <xref:System.Data.Common.DbConnection.StateChange> event reports state transitions. The <xref:Microsoft.Data.SqlClient.SqlConnection.InfoMessage> event reports informational messages and server warnings that don't become exceptions. Use these events for diagnostics, not for coordinating concurrent work.

## Don't share a connection concurrently

`SqlConnection`, `SqlCommand`, `SqlDataReader`, and `SqlTransaction` don't support concurrent use by multiple threads. Give each concurrent operation its own connection and let connection pooling reuse the physical connections.

Multiple Active Result Sets (MARS) permits multiple active batches on one connection in supported scenarios. It doesn't make SqlClient objects thread-safe, and it adds session and transaction rules. Leave it disabled unless one operation specifically needs it.

Don't register an open `SqlConnection` as a singleton in dependency injection. Register the connection string, an immutable options object, or a factory that creates a new connection.

## Use transactions deliberately

A local transaction belongs to its connection. Every command in the transaction must use that connection and set its `Transaction` property.

```csharp
await using var connection = new SqlConnection(connectionString);
await connection.OpenAsync(cancellationToken);

await using SqlTransaction transaction =
    (SqlTransaction)await connection.BeginTransactionAsync(cancellationToken);

using var command = new SqlCommand(sql, connection, transaction);
command.Parameters.Add(
    new SqlParameter("@value", SqlDbType.Int) { Value = value });
await command.ExecuteNonQueryAsync(cancellationToken);

await transaction.CommitAsync(cancellationToken);
```

If the operation fails before `CommitAsync`, disposing the transaction rolls it back. Keep transactions short. Don't do network calls, user interaction, or unrelated computation while a database transaction holds locks.

When `System.Transactions.Transaction.Current` is active, `Open` and `OpenAsync` automatically enlist by default. Set `Enlist=false` only when the operation must stay outside the ambient transaction.

## Measure one logical connection

Set <xref:Microsoft.Data.SqlClient.SqlConnection.StatisticsEnabled%2A> to `true` to collect provider statistics for one `SqlConnection` object:

```csharp
await using var connection = new SqlConnection(connectionString)
{
    StatisticsEnabled = true,
};

await connection.OpenAsync(cancellationToken);
connection.ResetStatistics();

using var command = new SqlCommand(sql, connection);
await command.ExecuteNonQueryAsync(cancellationToken);

System.Collections.IDictionary statistics =
    connection.RetrieveStatistics();
long roundTrips =
    Convert.ToInt64(statistics["ServerRoundtrips"]);
```

`RetrieveStatistics` returns a snapshot. `ResetStatistics` starts a new measurement boundary. Set `StatisticsEnabled=false` to stop collecting; values collected so far remain available. Statistics are per connection object and add overhead, so enable them for targeted diagnosis instead of every production request.

For process-wide pool and connection measurements, use [SqlClient diagnostic counters](diagnostic-counters.md).

## Handle connection failures

Catch <xref:Microsoft.Data.SqlClient.SqlException> at a boundary that can log, translate, or retry the failure. Record:

- `Number`
- `State`
- `Class`
- `ClientConnectionId`
- The operation name and configured server and database identifiers

Don't log the connection string, password, client secret, or access token.

Dispose a broken connection. The pool removes invalid physical connections when it detects them. If a credential, token, certificate, DNS target, or server changed, correct the configuration before retrying.

Use bounded retry logic only for transient failures. Initial-open retry, idle connection recovery, and command retry are different mechanisms. See [Configurable retry logic](configurable-retry-logic.md).

## Related content

- [Use Microsoft.Data.SqlClient in a .NET app](get-started-sqlclient-driver.md)
- [Connection strings](connection-strings.md)
- [Connection options](connection-options.md).
- [SQL Server connection pooling](sql-server-connection-pooling.md)
- [Asynchronous programming](asynchronous-programming.md)
- [Transactions and concurrency](transactions-and-concurrency.md)
