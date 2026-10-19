---
title: Handle errors and messages
description: Catch SqlException, inspect SqlError details, identify failed batch commands, and receive database engine informational messages with Microsoft.Data.SqlClient.
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

# Handle errors and messages

Microsoft.Data.SqlClient reports server and provider failures through <xref:Microsoft.Data.SqlClient.SqlException>. A database operation can return multiple errors, so inspect the exception's <xref:Microsoft.Data.SqlClient.SqlException.Errors%2A> collection instead of relying only on the combined `Message`.

Warnings and informational messages from the database engine are delivered through <xref:Microsoft.Data.SqlClient.SqlConnection.InfoMessage>. Subscribe before opening the connection or executing the command that can produce the message.

Handle a database failure in this order:

1. Catch `SqlException` at a boundary that can add operation context or choose a recovery action.
1. Inspect every `SqlError` in the `Errors` collection.
1. Record structured diagnostic fields without recording credentials or sensitive values.
1. Check the connection state after a connection-related failure.
1. Retry only when the error is transient and the operation is safe to repeat.

The examples in this article assume that an open <xref:Microsoft.Data.SqlClient.SqlConnection> named `connection` targets the [AdventureWorks sample database](../../samples/adventureworks-install-configure.md). The C# examples require `using Microsoft.Data.SqlClient;`.

## Receive informational messages

The `InfoMessage` event receives low-severity server messages, including output from `PRINT`. One event can contain multiple `SqlError` objects. Subscribe before executing the command that can produce a message:

```csharp
connection.InfoMessage += (_, eventArgs) =>
{
    foreach (SqlError message in eventArgs.Errors)
    {
        Console.WriteLine(
            $"Info {message.Number}, class {message.Class}: {message.Message}");
    }
};

using var command = new SqlCommand(
    "PRINT N'Connected and ready.';",
    connection);
command.ExecuteNonQuery();
```

The excerpt prints:

```output
Info 0, class 0: Connected and ready.
```

## Catch SqlException

Catch `SqlException` around the database operation that you can handle or enrich. Don't use a broad catch to turn a failed database operation into a successful result.

A database operation can return more than one error. Each <xref:Microsoft.Data.SqlClient.SqlError> in `Errors` includes structured fields:

| Property | Meaning |
| --- | --- |
| `Number` | Database engine or provider error number. |
| `Class` | Severity. |
| `State` | Server-defined state that can distinguish locations for the same error number. |
| `LineNumber` | Reported line number. |
| `Procedure` | Procedure or trigger name when supplied. |
| `Server` | Server name reported for the error. |
| `Message` | Human-readable error text. |

The convenience properties on `SqlException`, including `Number`, `Class`, `State`, `LineNumber`, `Procedure`, and `Server`, reflect only the first error. The following excerpt prints a message and then raises an error in the same batch. It reads `exception.Number` and then iterates `Errors` to report every entry:

```csharp
using (var failing = new SqlCommand("""
    PRINT N'Starting controlled operation.';
    RAISERROR (N'The controlled operation failed.', 16, 1);
    """, connection))
{
    try
    {
        await failing.ExecuteNonQueryAsync();
    }
    catch (SqlException exception)
    {
        Console.WriteLine($"First error number: {exception.Number}");

        foreach (SqlError error in exception.Errors)
        {
            Console.WriteLine(
                $"Number={error.Number}; Class={error.Class}; " +
                $"State={error.State}; Message={error.Message}");
        }
    }
}
```

The excerpt prints:

```output
First error number: 50000
Number=50000; Class=16; State=1; Message=The controlled operation failed.
Number=0; Class=0; State=1; Message=Starting controlled operation.
```

The `PRINT` output doesn't raise `InfoMessage`. When a batch returns an error that raises `SqlException`, the provider appends that batch's informational messages to the end of `SqlException.Errors`.

This excerpt catches the controlled error only to show its fields. In application code, rethrow with `throw;` unless the catch block chooses a real recovery action. Make decisions from documented error numbers and operation context. Don't parse localized message text.

## Check the connection state

The database engine assigns a severity class to each message. Lower classes are informational. Classes treated as errors produce `SqlException`. High-severity errors can leave the connection closed or broken.

After an error, check <xref:System.Data.Common.DbConnection.State%2A> before you reuse the connection:

```csharp
using var command = new SqlCommand(
    "RAISERROR (N'Controlled failure.', 16, 1);",
    connection);

try
{
    command.ExecuteNonQuery();
}
catch (SqlException)
{
    Console.WriteLine($"Connection state: {connection.State}");
}
```

Don't assume a connection remains usable because the exception was caught. A severity 16 error ordinarily leaves it open, but connection-breaking failures can close it. Dispose a closed or broken connection and get another connection from the pool. Don't encode retry policy from severity alone. The operation must also be safe to repeat.

## Identify a failed batch command

When a <xref:Microsoft.Data.SqlClient.SqlBatch> fails, `SqlException.BatchCommand` can reference the associated <xref:Microsoft.Data.SqlClient.SqlBatchCommand>. The property is `null` when the provider can't attribute the failure to one batch command. The following excerpt runs two batch commands. The second one fails:

```csharp
using (var batch = new SqlBatch(connection))
{
    batch.BatchCommands.Add(new SqlBatchCommand("SELECT 1;"));
    batch.BatchCommands.Add(new SqlBatchCommand("SELECT 1 / 0;"));

    try
    {
        await batch.ExecuteNonQueryAsync();
    }
    catch (SqlException exception)
    {
        Console.WriteLine($"Error {exception.Number}: {exception.Message}");

        if (exception.BatchCommand is SqlBatchCommand failedCommand)
        {
            Console.WriteLine($"Failed command: {failedCommand.CommandText}");
        }
    }
}
```

The excerpt prints:

```output
Error 8134: Divide by zero error encountered.
Failed command: SELECT 1 / 0;
```

Command text can contain sensitive literals in poorly parameterized applications. Prefer stable application operation names in production logs, and don't log parameter values by default.

## Route user errors to InfoMessage

<xref:Microsoft.Data.SqlClient.SqlConnection.FireInfoMessageEventOnUserErrors%2A> defaults to `false`. When you set it to `true`, errors with severity 16 or lower go to the `InfoMessage` handler instead of raising `SqlException`:

```csharp
connection.InfoMessage += (_, eventArgs) =>
{
    foreach (SqlError message in eventArgs.Errors)
    {
        Console.WriteLine(
            $"Info {message.Number}, class {message.Class}: {message.Message}");
    }
};
connection.FireInfoMessageEventOnUserErrors = true;

using var command = new SqlCommand(
    "RAISERROR (N'Routed to InfoMessage.', 16, 1);",
    connection);
command.ExecuteNonQuery();
```

No exception is thrown. The excerpt prints:

```output
Info 50000, class 16: Routed to InfoMessage.
```

Use this option only when the application has a deliberate event-based error model, because the calling code no longer sees an exception. Errors with severity 17 or higher still raise `SqlException`. Set the property once as part of connection configuration rather than toggling it around individual commands.

## Log diagnostic context

Capture enough structured context to correlate the failure without exposing data. The following excerpt logs an application operation name, the `ClientConnectionId`, and the fields of every `SqlError`. `ClientConnectionId` helps correlate a client failure with server and network diagnostics. It isn't a credential.

```csharp
const string operation = "LoadOrders";

using (var load = new SqlCommand("SELECT 1 / 0;", connection))
{
    try
    {
        await load.ExecuteScalarAsync();
    }
    catch (SqlException exception)
    {
        Console.WriteLine(
            $"Operation={operation}; " +
            $"ClientConnectionId={exception.ClientConnectionId}");

        foreach (SqlError error in exception.Errors)
        {
            Console.WriteLine(
                $"Number={error.Number}; Class={error.Class}; " +
                $"State={error.State}; Procedure={error.Procedure}; " +
                $"Line={error.LineNumber}");
        }
    }
}
```

The `ClientConnectionId` value differs on every connection. The excerpt prints output similar to:

```output
Operation=LoadOrders; ClientConnectionId=<connection ID>
Number=8134; Class=16; State=1; Procedure=; Line=1
```

Also record whether a transaction was active and, if `BatchCommand` is available, which batch command failed. Include sanitized server and database identifiers only if organizational policy permits them. Don't log connection strings, access tokens, passwords, or sensitive parameter values. Use [diagnostic counters](diagnostic-counters.md), [event source tracing](enable-eventsource-tracing.md), and the [SqlClient troubleshooting guide](sqlclient-troubleshooting-guide.md) for deeper investigation.

## Related content

- Return to the [command execution workflow](execute-command.md).
- To identify a failed command in a group, [execute multiple commands with SqlBatch](execute-multiple-commands-sql-batch.md).
- To add bounded retries, use [configurable retry logic](configurable-retry-logic.md).
- When exception fields aren't enough, use the [SqlClient troubleshooting guide](sqlclient-troubleshooting-guide.md).
