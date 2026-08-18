---
title: Create a Custom Retry Provider for SqlClient
description: Implement custom Microsoft.Data.SqlClient retry intervals, conditions, and execution behavior with the configurable retry logic APIs.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra, randolphwest
ms.date: 08/14/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Create a custom retry provider for SqlClient

[!INCLUDE [dotnet-all](../../includes/products/applies-full/dotnet-all.md)]

[!INCLUDE [Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

If the built-in retry logic providers don't cover your needs, you can create your own custom providers. You can then assign those providers to a `SqlConnection` or `SqlCommand` object to apply your custom logic.

The built-in providers are designed around three base classes that you can extend to implement a custom provider. Assign a custom provider to a <xref:Microsoft.Data.SqlClient.SqlConnection> or <xref:Microsoft.Data.SqlClient.SqlCommand> in the same way as a built-in provider:

1. <xref:Microsoft.Data.SqlClient.SqlRetryIntervalBaseEnumerator>: Generates a sequence of time intervals.
1. <xref:Microsoft.Data.SqlClient.SqlRetryLogicBase>: Retrieves the next time interval for a given enumerator, if the number of retries hasn't been exceeded and a transient condition is met.
1. <xref:Microsoft.Data.SqlClient.SqlRetryLogicBaseProvider>: Applies retry logic to connection and command operations.

> [!CAUTION]  
> A custom retry provider controls concurrency, cancellation, performance, delay scheduling, exception handling, and event behavior. Prefer a [built-in provider](internal-retry-logic-providers-sqlclient.md) unless you need execution behavior that its options can't express.

## Example

This sample demonstrates the extension points with a minimal synchronous implementation. It isn't production-ready: the asynchronous methods throw <xref:System.NotImplementedException>, and the provider doesn't implement thread safety, cancellation, concurrent use, or the `Retrying` event. For a production implementation, study the built-in retry logic in the [Microsoft.Data.SqlClient GitHub repository](https://github.com/dotnet/SqlClient/).

1. Define custom configurable retry logic classes:

   - **Enumerator**: Define a fixed sequence of time intervals and extend the acceptable range of times from two minutes to four minutes.

   [!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#6](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#6)]

   - **Retry logic**: Implement retry logic on any command that isn't part of an active transaction. Lower the number of retries from 60 to 20.

   [!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#7](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#7)]

   - **Provider**: Implement a provider for synchronous operations without a `Retrying` event. Treat <xref:System.TimeoutException> and configured <xref:Microsoft.Data.SqlClient.SqlException> error numbers as retryable.

   [!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#8](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#8)]

1. Create a retry provider instance consisting of the defined custom types:

   [!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#4](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#4)]

   - Evaluate an exception against the configured error numbers and <xref:System.TimeoutException> to determine whether to retry it:

   [!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#5](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#5)]

1. Use the customized retry logic:

   - Define the retry logic parameters:

   [!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#1](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#1)]

   - Create a custom retry provider:

   [!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#2](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#2)]

   - Assign the retry provider to the <xref:Microsoft.Data.SqlClient.SqlConnection.RetryLogicProvider%2A?displayProperty=nameWithType> or <xref:Microsoft.Data.SqlClient.SqlCommand.RetryLogicProvider%2A?displayProperty=nameWithType>:

   [!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#3](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#3)]

## Related content

- [Microsoft.Data.SqlClient GitHub repository](https://github.com/dotnet/SqlClient/)
- [Configurable retry logic in SqlClient](configurable-retry-logic.md)
- [Microsoft.Data.SqlClient for SQL Server](microsoft-ado-net-sql-server.md)
