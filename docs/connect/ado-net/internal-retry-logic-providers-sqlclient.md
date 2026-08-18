---
title: Built-In Retry Logic Providers in SqlClient
description: Learn how the built-in Microsoft.Data.SqlClient retry providers schedule retries, select transient errors, and handle commands and transactions.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra, randolphwest
ms.date: 08/14/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---
# Built-in retry logic providers in SqlClient

[!INCLUDE [dotnet-all](../../includes/products/applies-full/dotnet-all.md)]

[!INCLUDE [Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

<xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory?displayProperty=fullName> creates providers for common retry schedules. Configurable retry logic is off by default. Assign a provider to <xref:Microsoft.Data.SqlClient.SqlConnection.RetryLogicProvider%2A?displayProperty=nameWithType> or <xref:Microsoft.Data.SqlClient.SqlCommand.RetryLogicProvider%2A?displayProperty=nameWithType> to enable it for that object.

## Choose a retry provider

| Factory method | Delay pattern |
| --- | --- |
| <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateFixedRetryProvider%2A?displayProperty=nameWithType> | Approximately the same delay before each retry. |
| <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateIncrementalRetryProvider%2A?displayProperty=nameWithType> | Adds `DeltaTime` to the delay after each retry. |
| <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateExponentialRetryProvider%2A?displayProperty=nameWithType> | Increases the delay exponentially after each retry. |
| <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateNoneRetryProvider%2A?displayProperty=nameWithType> | Doesn't retry. This provider is the default. |

The fixed, incremental, and exponential providers add random jitter to each interval. Jitter reduces synchronized retry bursts when many clients encounter the same outage.

`NumberOfTries` is the total number of attempts, including the initial operation. For example, `NumberOfTries = 3` allows the initial attempt and up to two retries. Its valid range is 1 through 60.

## Built-in transient error list

When <xref:Microsoft.Data.SqlClient.SqlRetryLogicOption.TransientErrors%2A?displayProperty=nameWithType> is `null`, the built-in providers retry the 20 error numbers in `SqlConfigurableRetryFactory.BaselineTransientErrors`, grouped by where the failure originates:

| Failure area | Error numbers |
| --- | --- |
| Login-process transport | `233`, `997`, `10060` |
| Database availability during login | `4060`, `4221` |
| Statement-level | `1204`, `1205`, `1222` |
| Resource limit or throttling | `10928`, `10929`, `40501`, `49918`, `49919`, `49920` |
| Azure SQL service failover | `40143`, `40197`, `40540`, `40613` |
| Dedicated SQL pool state | `42108`, `42109` |

Each error is described in the sections that follow.

> [!IMPORTANT]  
> Setting `TransientErrors` replaces the built-in list. It doesn't append to the list. Include every error that the provider should retry.

In Microsoft.Data.SqlClient 7.0, <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.BaselineTransientErrors%2A?displayProperty=nameWithType> exposes the built-in list as a read-only collection. Use it to extend the baseline without copying error numbers from the driver source:

```csharp
var transientErrors = SqlConfigurableRetryFactory.BaselineTransientErrors
    .Append(12345)
    .ToArray();

var options = new SqlRetryLogicOption
{
    NumberOfTries = 5,
    DeltaTime = TimeSpan.FromSeconds(2),
    MaxTimeInterval = TimeSpan.FromSeconds(30),
    TransientErrors = transientErrors,
};
```

For earlier driver versions, create an application-owned collection that contains the baseline errors you need and your additional errors. Before copying a baseline, select the [SqlClient source tag](https://github.com/dotnet/SqlClient/tags) that matches your installed package version and inspect `SqlConfigurableRetryFactory.cs`. The list on the `main` branch can change after your package is released.

## Errors during connection establishment

The following errors are retryable in the built-in list or worth adding to `TransientErrors` on top of the built-in list.

[!INCLUDE [transient-connection-errors](../includes/transient-connection-errors.md)]

## Errors during command execution

[!INCLUDE [transient-command-errors](../includes/transient-command-errors.md)]

The driver, not the engine, surfaces client-side representations of statement timeout and cancellation errors (for example, the Microsoft.Data.SqlClient `-2` timeout), so these errors aren't in the built-in list. If your application catches these errors separately, handle them at the same transaction boundary as the engine errors described earlier.

## Command and transaction behavior

The built-in providers skip retry when a command runs inside an ambient <xref:System.Transactions.TransactionScope> or has a <xref:Microsoft.Data.SqlClient.SqlTransaction> attached. The command runs once without retry logic. Retrying a single statement inside a transaction can duplicate earlier work or violate the transaction's intended ordering.

> [!CAUTION]  
> For deadlocks and other retryable failures inside a transaction, roll back and retry the entire transaction as one unit. Don't retry only the failing command.

Use <xref:Microsoft.Data.SqlClient.SqlRetryLogicOption.AuthorizedSqlCondition%2A?displayProperty=nameWithType> to limit command retries to operations your application can safely repeat. The predicate receives the command text. If the predicate returns `false`, the command runs once without retry logic.

## Example

For complete connection and command examples, see:

- <xref:Microsoft.Data.SqlClient.SqlConnection.RetryLogicProvider%2A?displayProperty=nameWithType>
- <xref:Microsoft.Data.SqlClient.SqlCommand.RetryLogicProvider%2A?displayProperty=nameWithType>

## Related content

- [Create a custom retry provider for SqlClient](configurable-retry-logic-core-apis-sqlclient.md)
- [Configurable retry logic in SqlClient](configurable-retry-logic.md)
- [Microsoft.Data.SqlClient for SQL Server](microsoft-ado-net-sql-server.md)
