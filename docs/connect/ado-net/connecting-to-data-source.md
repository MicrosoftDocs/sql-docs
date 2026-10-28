---
title: Connect to a Data Source with Microsoft.Data.SqlClient
description: Open, close, monitor, and identify Microsoft.Data.SqlClient connections, and handle StateChange and InfoMessage events in .NET applications.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, paulmedynski, cmalhotra
ms.date: 10/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
dev_langs:
  - csharp
ai-usage: ai-assisted
---

# Connect to a data source with Microsoft.Data.SqlClient

<xref:Microsoft.Data.SqlClient.SqlConnection> represents one logical connection to SQL Server, Azure SQL Database, Azure SQL Managed Instance, or SQL database in Microsoft Fabric. Opening the object gets a physical connection from the connection pool when one is available. Closing or disposing it returns that physical connection to the pool.

Use short-lived `SqlConnection` objects for units of work. Don't keep one global connection open for the application.

## Prerequisites

- The [.NET 10 SDK or a later supported .NET SDK](https://dotnet.microsoft.com/download). The SDK runs on Windows, Linux, and macOS. It also builds the **.NET Framework** version of each sample, which runs only on Windows and needs .NET Framework 4.8.1.
- A database that you can connect to in SQL Server, Azure SQL Database, Azure SQL Managed Instance, or SQL database in Microsoft Fabric.

## Create the project

Microsoft.Data.SqlClient 7.0 and later versions need the version-matched `Microsoft.Data.SqlClient.Extensions.Azure` package for driver-provided Microsoft Entra ID authentication modes, such as `Active Directory Default`. If your app uses only SQL authentication or Windows integrated authentication, you can omit it.

### [.NET](#tab/dotnet)

In an empty folder, run these commands:

```console
dotnet new console --framework net10.0 --name SqlClientConnect
cd SqlClientConnect
dotnet add package Microsoft.Data.SqlClient --version 7.1.1
dotnet add package Microsoft.Data.SqlClient.Extensions.Azure --version 7.1.1
```

### [.NET Framework](#tab/netframework)

The console template doesn't offer .NET Framework targets. In an empty folder, create the project:

```console
dotnet new console --framework net10.0 --name SqlClientConnect
cd SqlClientConnect
```

Replace the contents of `SqlClientConnect.csproj` with this project file, which targets .NET Framework 4.8.1 and the default C# 7.3 language version:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net481</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Data.SqlClient" Version="7.1.1" />
    <PackageReference Include="Microsoft.Data.SqlClient.Extensions.Azure" Version="7.1.1" />
  </ItemGroup>

</Project>
```

For other .NET Framework project types, see [.NET Framework projects](download-microsoft-sqlclient-data-provider.md#net-framework-projects).

---

The samples in this article read the connection string from the `SQL_CONNECTION_STRING` environment variable. Set it before you run a sample. For connection string formats, see [Connection strings](connection-strings.md).

Every sample except the transaction sample is a complete program. To run one, replace the contents of `Program.cs` with it, and then run `dotnet run`. The transaction sample is a fragment for your own method. The **.NET** tab uses top-level statements, implicit `using` directives, and `await using`. The **.NET Framework** tab uses C# 7.3, which doesn't have `await using`, so it uses `using` statements instead.

<a name="connecting-to-sql-server"></a>

## Open a connection

Call <xref:Microsoft.Data.SqlClient.SqlConnection.OpenAsync%2A> in asynchronous code or <xref:Microsoft.Data.SqlClient.SqlConnection.Open%2A> in synchronous code. Declare the connection with `await using` or `using` so it's disposed when the code leaves the scope, even when an exception is thrown.

<a name="integrated-security-and-aspnet"></a>
With Windows integrated authentication, the connection uses the identity of the process. For a web application or a Windows service, that's the application pool or service account, not the signed-in user. For more information, see [Choose an authentication pattern](connection-strings.md#choose-an-authentication-pattern).

This complete program opens a connection, reports each state change, and reads two server properties:

### [.NET](#tab/dotnet)

```csharp
using Microsoft.Data.SqlClient;

string connectionString =
    Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
    ?? throw new InvalidOperationException(
        "Set the SQL_CONNECTION_STRING environment variable.");

await using var connection = new SqlConnection(connectionString);
connection.StateChange += (_, e) =>
    Console.WriteLine($"State: {e.OriginalState} to {e.CurrentState}");

await connection.OpenAsync();

await using var command = new SqlCommand(
    """
    SELECT CAST(SERVERPROPERTY('Edition') AS nvarchar(128)),
           CAST(SERVERPROPERTY('EngineEdition') AS int);
    """,
    connection);

await using (SqlDataReader reader = await command.ExecuteReaderAsync())
{
    await reader.ReadAsync();
    Console.WriteLine($"Edition: {reader.GetString(0)}");
    Console.WriteLine($"EngineEdition: {reader.GetInt32(1)}");
}
```

### [.NET Framework](#tab/netframework)

```csharp
using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

class Program
{
    static async Task Main()
    {
        string connectionString =
            Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
            ?? throw new InvalidOperationException(
                "Set the SQL_CONNECTION_STRING environment variable.");

        using (var connection = new SqlConnection(connectionString))
        {
            connection.StateChange += (_, e) =>
                Console.WriteLine($"State: {e.OriginalState} to {e.CurrentState}");

            await connection.OpenAsync();

            using (var command = new SqlCommand(
                @"SELECT CAST(SERVERPROPERTY('Edition') AS nvarchar(128)),
                         CAST(SERVERPROPERTY('EngineEdition') AS int);",
                connection))
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                await reader.ReadAsync();
                Console.WriteLine($"Edition: {reader.GetString(0)}");
                Console.WriteLine($"EngineEdition: {reader.GetInt32(1)}");
            }
        }
    }
}
```

---

Against SQL Server 2025 Enterprise edition, the output is:

```output
State: Closed to Open
Edition: Enterprise Edition: Core-based Licensing (64-bit)
EngineEdition: 3
State: Open to Closed
```

The last line appears when the connection is disposed at the end of the program.

<a name="closing-connections"></a>

## Close and dispose connections

Always close a connection when you finish with it so the physical connection returns to the pool. The `using` and `await using` statements dispose the connection when the code exits the block, including when an unhandled exception is thrown. You can also call <xref:Microsoft.Data.SqlClient.SqlConnection.Close%2A> or <xref:System.ComponentModel.Component.Dispose%2A> directly. `Close` and `Dispose` have the same effect on the physical connection.

Follow these rules:

- Dispose readers and commands before the connection that owns them.
- Don't rely on garbage collection to return a connection to the pool. A connection that goes out of scope without being closed isn't returned promptly, and the pool can run out of connections.
- Don't call `Close` or `Dispose` on a connection, a reader, or any other managed object in a class finalizer. A finalizer should release only unmanaged resources that its class owns directly. For more information, see [Garbage collection](/dotnet/standard/garbage-collection/).
- After `Close`, you can call `Open` again on the same object. After `Dispose`, create a new object.

With pooling enabled, closing a connection returns it to the pool without closing the network connection. The pool resets the session state when the connection is reused. For more information, see [SQL Server connection pooling](sql-server-connection-pooling.md).

## Use asynchronous APIs

Use asynchronous calls for network-bound database work in web servers, services, user interfaces, and workers:

- `OpenAsync(cancellationToken)`
- `ExecuteNonQueryAsync(cancellationToken)`
- `ExecuteReaderAsync(cancellationToken)`
- `ExecuteScalarAsync(cancellationToken)`
- `ReadAsync(cancellationToken)`

Don't start another operation on a connection, command, or reader before the current asynchronous operation finishes.

## Apply cancellation and timeouts

Pass the caller's <xref:System.Threading.CancellationToken> through every asynchronous database call. Cancellation asks the driver to stop the pending work, but completion isn't guaranteed to be immediate. Continue to use bounded connection and command timeouts.

These controls have separate scopes:

| Control | Scope |
| --- | --- |
| `Connect Timeout` | Connection establishment, or the wait for a pooled connection |
| `SqlCommand.CommandTimeout` | One command execution |
| `CancellationToken` | Caller-requested cancellation of an asynchronous operation |

A timeout or cancellation doesn't prove that the server rolled back an operation. Use a transaction when multiple changes must commit or roll back as one unit, and make retry decisions from the operation's idempotency and transaction outcome.

<a name="work-with-the-statechange-event"></a>

## Track connection state

The <xref:Microsoft.Data.SqlClient.SqlConnection.State%2A> property returns a snapshot from the <xref:System.Data.ConnectionState> enumeration.

| State | Meaning |
| --- | --- |
| `Closed` | The logical connection isn't open. |
| `Connecting` | An open operation is in progress. |
| `Open` | The logical connection is open. |

The <xref:System.Data.Common.DbConnection.StateChange> event fires when the state changes. Its <xref:System.Data.StateChangeEventArgs> argument has two properties:

- `OriginalState` is the state before the change.
- `CurrentState` is the state after the change.

The driver reports the `Closed` to `Open` and `Open` to `Closed` transitions, as the sample in [Open a connection](#open-a-connection) shows. Don't depend on observing `Executing`, `Fetching`, or `Broken` as application lifecycle phases.

Don't use `State` as a health check before every command. The network can fail after any check. Run the operation and handle the resulting exception. Use `StateChange` for diagnostics, not for coordinating concurrent work.

<a name="work-with-the-infomessage-event"></a>

## Receive informational messages

The <xref:Microsoft.Data.SqlClient.SqlConnection.InfoMessage> event delivers messages that the server sends without raising an error. These messages include `PRINT` output, `RAISERROR` messages with a severity of 10 or lower, and server warnings. Errors with a severity of 11 or higher raise a <xref:Microsoft.Data.SqlClient.SqlException> instead. For severity levels, see [Database Engine error severities](../../relational-databases/errors-events/database-engine-error-severities.md).

The event handler receives a <xref:Microsoft.Data.SqlClient.SqlInfoMessageEventArgs> object. Its `Errors` property holds a collection of <xref:Microsoft.Data.SqlClient.SqlError> objects. Each one includes the message number, text, severity class, procedure, and line number.

### [.NET](#tab/dotnet)

```csharp
using Microsoft.Data.SqlClient;

string connectionString =
    Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
    ?? throw new InvalidOperationException(
        "Set the SQL_CONNECTION_STRING environment variable.");

await using var connection = new SqlConnection(connectionString);
connection.InfoMessage += (_, e) =>
{
    foreach (SqlError error in e.Errors)
    {
        Console.WriteLine(
            $"Message {error.Number}, severity {error.Class}: {error.Message}");
    }
};

await connection.OpenAsync();

await using var command = new SqlCommand(
    """
    PRINT 'Starting the batch.';
    RAISERROR('Processed %d rows.', 10, 1, 42);
    """,
    connection);

await command.ExecuteNonQueryAsync();
```

### [.NET Framework](#tab/netframework)

```csharp
using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

class Program
{
    static async Task Main()
    {
        string connectionString =
            Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
            ?? throw new InvalidOperationException(
                "Set the SQL_CONNECTION_STRING environment variable.");

        using (var connection = new SqlConnection(connectionString))
        {
            connection.InfoMessage += (_, e) =>
            {
                foreach (SqlError error in e.Errors)
                {
                    Console.WriteLine(
                        $"Message {error.Number}, severity {error.Class}: {error.Message}");
                }
            };

            await connection.OpenAsync();

            using (var command = new SqlCommand(
                @"PRINT 'Starting the batch.';
                  RAISERROR('Processed %d rows.', 10, 1, 42);",
                connection))
            {
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
```

---

The output is:

```output
Message 0, severity 0: Starting the batch.
Message 50000, severity 0: Processed 42 rows.
```

The server reports a `RAISERROR` severity of 10 as class 0, so `SqlError.Class` shows `0` for both messages.

<a name="handle-errors-as-infomessages"></a>

### Handle user errors as informational messages

By default, the driver reports an error with a severity of 11 or higher by throwing a `SqlException`. That exception doesn't mean the server stopped the batch. Many errors, including `RAISERROR` with a severity of 16, let the server run the remaining statements, so later statements can change data even when your code catches the exception. To receive these errors as messages instead of exceptions, set <xref:Microsoft.Data.SqlClient.SqlConnection.FireInfoMessageEventOnUserErrors%2A> to `true`. The driver then sends errors with a severity of 16 or lower to the `InfoMessage` event instead of throwing. This property changes how the driver reports errors. It doesn't change which statements the server runs.

### [.NET](#tab/dotnet)

```csharp
using Microsoft.Data.SqlClient;

string connectionString =
    Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
    ?? throw new InvalidOperationException(
        "Set the SQL_CONNECTION_STRING environment variable.");

await using var connection = new SqlConnection(connectionString)
{
    FireInfoMessageEventOnUserErrors = true,
};
connection.InfoMessage += (_, e) =>
{
    foreach (SqlError error in e.Errors)
    {
        Console.WriteLine(
            $"Message {error.Number}, severity {error.Class}: {error.Message}");
    }
};

await connection.OpenAsync();

await using var command = new SqlCommand(
    """
    RAISERROR('Row 7 failed validation.', 16, 1);
    SELECT 'The batch continued.';
    """,
    connection);

await using (SqlDataReader reader = await command.ExecuteReaderAsync())
{
    do
    {
        while (await reader.ReadAsync())
        {
            Console.WriteLine(reader.GetString(0));
        }
    }
    while (await reader.NextResultAsync());
}
```

### [.NET Framework](#tab/netframework)

```csharp
using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

class Program
{
    static async Task Main()
    {
        string connectionString =
            Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
            ?? throw new InvalidOperationException(
                "Set the SQL_CONNECTION_STRING environment variable.");

        using (var connection = new SqlConnection(connectionString)
        {
            FireInfoMessageEventOnUserErrors = true,
        })
        {
            connection.InfoMessage += (_, e) =>
            {
                foreach (SqlError error in e.Errors)
                {
                    Console.WriteLine(
                        $"Message {error.Number}, severity {error.Class}: {error.Message}");
                }
            };

            await connection.OpenAsync();

            using (var command = new SqlCommand(
                @"RAISERROR('Row 7 failed validation.', 16, 1);
                  SELECT 'The batch continued.';",
                connection))
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                do
                {
                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine(reader.GetString(0));
                    }
                }
                while (await reader.NextResultAsync());
            }
        }
    }
}
```

---

The output is:

```output
Message 50000, severity 16: Row 7 failed validation.
The batch continued.
```

Errors with a severity of 17 or higher still throw a `SqlException`, regardless of this setting. When you enable this property, your handler is responsible for detecting failures. Code that expects an exception for every error doesn't see one.

## Report the application identity

Starting with Microsoft.Data.SqlClient 7.1.0, a framework, tool, or other middleware can report which application opened a connection. Set <xref:Microsoft.Data.SqlClient.SqlConnection.RegisteredApplication%2A> to a <xref:Microsoft.Data.SqlClient.RegisteredApplication> value before you open the connection. The driver sends the value to the server once, during login.

Follow these rules:

- **Set the value through the property.** No connection string keyword exists for it.
- **Set the value before you open the connection.** Setting it while the connection is opening or open throws an <xref:System.InvalidOperationException>.
- **Treat the value as telemetry.** The client supplies the value and can report any identifier, so the server doesn't authenticate it.
- **Expect pooled connections to report the original identity.** The value isn't part of the pool key. A connection served from the pool reports the identity of the logical connection that caused the physical connection to be created.

> [!CAUTION]
> Never use `RegisteredApplication` for authorization or any other security decision. Any client can report any identifier.

### Choose an identifier

The default value is `Unknown`, which is `0`. The enumeration has an underlying type of **ushort**, so identifiers range from `0` through `0xFFFF`.

| Range | Use |
| --- | --- |
| `0x0001` through `0x7FFF` | Microsoft-defined applications. Named enumeration members use this range. |
| `0x8000` through `0xBFFF` | Reserved for small-scale use. |
| `0xC000` through `0xFFFF` | Public and developer use. |

The named members are `EntityFrameworkCore`, `SemanticKernel`, `ManagementStudio`, `SqlManagementObjects`, `DataTierApplicationFramework`, `SqlToolsService`, `AspNetCoreDistributedSqlServerCache`, `EntityFramework`, `AzureFunctionsSqlExtension`, `OrleansAdoNet`, `DurableTaskSqlServer`, `SqlPackage`, and `DataApiBuilder`. To report an identifier that has no named member, cast the number to `RegisteredApplication`.

The `RegisteredApplication` type and property aren't CLS-compliant because the enumeration uses **ushort**. C# code can use them directly.

### Set the identifier

This program reports an identifier from the developer range and then shows what happens when code changes the value after the connection opens:

### [.NET](#tab/dotnet)

```csharp
using Microsoft.Data.SqlClient;

string connectionString =
    Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
    ?? throw new InvalidOperationException(
        "Set the SQL_CONNECTION_STRING environment variable.");

await using var connection = new SqlConnection(connectionString)
{
    RegisteredApplication = (RegisteredApplication)0xC001,
};

await connection.OpenAsync();
Console.WriteLine(
    $"Reported identifier: 0x{(ushort)connection.RegisteredApplication:X4}");

try
{
    connection.RegisteredApplication = RegisteredApplication.Unknown;
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
}
```

### [.NET Framework](#tab/netframework)

```csharp
using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

class Program
{
    static async Task Main()
    {
        string connectionString =
            Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
            ?? throw new InvalidOperationException(
                "Set the SQL_CONNECTION_STRING environment variable.");

        using (var connection = new SqlConnection(connectionString)
        {
            RegisteredApplication = (RegisteredApplication)0xC001,
        })
        {
            await connection.OpenAsync();
            Console.WriteLine(
                $"Reported identifier: 0x{(ushort)connection.RegisteredApplication:X4}");

            try
            {
                connection.RegisteredApplication = RegisteredApplication.Unknown;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
            }
        }
    }
}
```

---

The output is:

```output
Reported identifier: 0xC001
InvalidOperationException: Not allowed to change the 'RegisteredApplication' property. The connection's current state is open.
```

### Understand pooling effects

The driver reports the identifier only when it creates a new physical connection. With pooling enabled:

- A connection that reuses a pooled physical connection reports the identity of the connection that created the physical connection, not its own.
- Physical connections that the pool opens in the background to satisfy `Min Pool Size` report `Unknown`.

If one connection string serves several identities, treat the reported value as approximate. Disable pooling for that connection string when you need exact attribution, and measure the extra connection cost first.

`SqlConnection.Clone` copies the `RegisteredApplication` value to the new connection.

## Don't share a connection concurrently

`SqlConnection`, `SqlCommand`, `SqlDataReader`, and `SqlTransaction` don't support concurrent use by multiple threads. Give each concurrent operation its own connection and let connection pooling reuse the physical connections.

Multiple Active Result Sets (MARS) permits multiple active batches on one connection in supported scenarios. It doesn't make SqlClient objects thread-safe, and it adds session and transaction rules. Leave it disabled unless one operation specifically needs it.

Don't register an open `SqlConnection` as a singleton in dependency injection. Register the connection string, an immutable options object, or a factory that creates a new connection.

## Use transactions deliberately

A local transaction belongs to its connection. Every command in the transaction must use that connection and set its `Transaction` property.

### [.NET](#tab/dotnet)

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

### [.NET Framework](#tab/netframework)

`BeginTransactionAsync` and `CommitAsync` don't exist on .NET Framework, so this version uses `BeginTransaction` and `Commit`:

```csharp
using (var connection = new SqlConnection(connectionString))
{
    await connection.OpenAsync(cancellationToken);

    using (SqlTransaction transaction = connection.BeginTransaction())
    using (var command = new SqlCommand(sql, connection, transaction))
    {
        command.Parameters.Add(
            new SqlParameter("@value", SqlDbType.Int) { Value = value });
        await command.ExecuteNonQueryAsync(cancellationToken);

        transaction.Commit();
    }
}
```

---

If the operation fails before the commit, disposing the transaction rolls it back. Keep transactions short. Don't do network calls, user interaction, or unrelated computation while a database transaction holds locks.

When `System.Transactions.Transaction.Current` is active, `Open` and `OpenAsync` automatically enlist by default. Set `Enlist=false` only when the operation must stay outside the ambient transaction.

## Measure one logical connection

Set <xref:Microsoft.Data.SqlClient.SqlConnection.StatisticsEnabled%2A> to `true` to collect driver statistics for one `SqlConnection` object:

### [.NET](#tab/dotnet)

```csharp
using System.Collections;
using Microsoft.Data.SqlClient;

string connectionString =
    Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
    ?? throw new InvalidOperationException(
        "Set the SQL_CONNECTION_STRING environment variable.");

await using var connection = new SqlConnection(connectionString)
{
    StatisticsEnabled = true,
};

await connection.OpenAsync();
connection.ResetStatistics();

await using var command = new SqlCommand("SELECT name FROM sys.databases;", connection);
await using (SqlDataReader reader = await command.ExecuteReaderAsync())
{
    while (await reader.ReadAsync())
    {
    }
}

IDictionary statistics = connection.RetrieveStatistics();
Console.WriteLine($"ServerRoundtrips: {statistics["ServerRoundtrips"]}");
Console.WriteLine($"SelectRows: {statistics["SelectRows"]}");
Console.WriteLine($"BytesReceived > 0: {(long)statistics["BytesReceived"]! > 0}");
```

### [.NET Framework](#tab/netframework)

```csharp
using System;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

class Program
{
    static async Task Main()
    {
        string connectionString =
            Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
            ?? throw new InvalidOperationException(
                "Set the SQL_CONNECTION_STRING environment variable.");

        using (var connection = new SqlConnection(connectionString)
        {
            StatisticsEnabled = true,
        })
        {
            await connection.OpenAsync();
            connection.ResetStatistics();

            using (var command = new SqlCommand("SELECT name FROM sys.databases;", connection))
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                }
            }

            IDictionary statistics = connection.RetrieveStatistics();
            Console.WriteLine($"ServerRoundtrips: {statistics["ServerRoundtrips"]}");
            Console.WriteLine($"SelectRows: {statistics["SelectRows"]}");
            Console.WriteLine($"BytesReceived > 0: {(long)statistics["BytesReceived"] > 0}");
        }
    }
}
```

---

The output shows one round trip. The row count depends on the number of databases on the server:

```output
ServerRoundtrips: 1
SelectRows: 14
BytesReceived > 0: True
```

`RetrieveStatistics` returns a snapshot. `ResetStatistics` starts a new measurement boundary. Set `StatisticsEnabled` to `false` to stop collecting. Values collected so far remain available. Statistics are per connection object and add overhead, so enable them for targeted diagnosis instead of every production request.

## Handle connection failures

Catch <xref:Microsoft.Data.SqlClient.SqlException> at a boundary that can log, translate, or retry the failure. Record:

- `Number`
- `State`
- `Class`
- `ClientConnectionId`
- The operation name and configured server and database identifiers

Don't log the connection string, password, client secret, or access token.

Dispose a broken connection. The pool removes invalid physical connections when it detects them. If a credential, token, certificate, DNS target, or server changed, correct the configuration before retrying.

Use bounded retry logic only for transient failures. Initial-open retry, idle connection recovery, and command retry are different mechanisms. For more information, see [Configurable retry logic](configurable-retry-logic.md) and the connection recovery options in [Connection options](connection-options.md#connection-recovery-options).

## Related content

- [Use Microsoft.Data.SqlClient in a .NET app](get-started-sqlclient-driver.md)
- [Connection strings](connection-strings.md)
- [Connection options](connection-options.md)
- [SQL Server connection pooling](sql-server-connection-pooling.md)
- [Asynchronous programming](asynchronous-programming.md)
- [Transactions and concurrency](transactions-and-concurrency.md)
