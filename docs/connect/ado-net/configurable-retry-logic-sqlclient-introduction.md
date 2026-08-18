---
title: Configure Retry Logic in SqlClient
description: Create and assign a built-in Microsoft.Data.SqlClient retry provider for transient connection or command failures.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra, randolphwest
ms.date: 08/14/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Configure retry logic in SqlClient

[!INCLUDE [dotnet-all](../../includes/products/applies-full/dotnet-all.md)]

[!INCLUDE [Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

This article shows how to create an exponential retry provider and assign it to a <xref:Microsoft.Data.SqlClient.SqlConnection>. The provider retries the driver's built-in list of transient connection errors and attempts to open the connection up to five times.

## Prerequisites

- Microsoft.Data.SqlClient 3.0 or a later version.
- A connection string for SQL Server, Azure SQL Database, Azure SQL Managed Instance, or SQL database in Microsoft Fabric.

## Configure a connection retry provider

1. Define the retry options. Leave `TransientErrors` as `null` so the provider uses the [built-in transient error list](internal-retry-logic-providers-sqlclient.md#built-in-transient-error-list).

   [!code-csharp[SqlConfigurableRetryLogic_StepByStep_OpenConnection#1](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_OpenConnection.cs#1)]

1. Create a provider from the options.

   [!code-csharp[SqlConfigurableRetryLogic_StepByStep_OpenConnection#2](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_OpenConnection.cs#2)]

1. Assign the provider before opening the connection.

   [!code-csharp[SqlConfigurableRetryLogic_StepByStep_OpenConnection#3](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_OpenConnection.cs#3)]

`NumberOfTries = 5` allows one initial attempt and up to four retries. The exponential provider increases the interval between attempts and adds random jitter. `MaxTimeInterval` caps each individual delay, not the total elapsed time for all attempts.

## Configure command retries

Create a separate provider and assign it to <xref:Microsoft.Data.SqlClient.SqlCommand.RetryLogicProvider%2A?displayProperty=nameWithType>. Set <xref:Microsoft.Data.SqlClient.SqlRetryLogicOption.AuthorizedSqlCondition%2A?displayProperty=nameWithType> to a predicate that returns `true` only for commands your application can safely repeat.

> [!CAUTION]  
> A built-in provider doesn't retry a command when the connection has an active transaction. If a transient failure invalidates a transaction, roll back and retry the entire transaction. Don't retry only the failing statement.

Setting <xref:Microsoft.Data.SqlClient.SqlRetryLogicOption.TransientErrors%2A?displayProperty=nameWithType> replaces the driver's built-in error list. To extend the baseline without losing the defaults, see [Extend the built-in transient error list](internal-retry-logic-providers-sqlclient.md#built-in-transient-error-list).

## Related content

- [Create a custom retry provider for SqlClient](configurable-retry-logic-core-apis-sqlclient.md)
- [Built-in retry logic providers in SqlClient](internal-retry-logic-providers-sqlclient.md)
- [Configure SqlClient retry logic with a configuration file](configurable-retry-logic-config-file-sqlclient.md)
- [Configurable retry logic in SqlClient](configurable-retry-logic.md)
- [Microsoft.Data.SqlClient for SQL Server](microsoft-ado-net-sql-server.md)
