---
title: Configurable retry logic core APIs in SqlClient
description: Learn how to use the configurable retry logic core APIs to implement custom retry logic in your application with Microsoft.Data.SqlClient.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-deshtehari
ms.date: 03/22/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Configurable retry logic core APIs in SqlClient

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

If the built-in retry logic providers don't cover your needs, you can create your own custom providers. You can then assign those providers to a `SqlConnection` or `SqlCommand` object to apply your custom logic.

The built-in providers are designed around three interfaces that can be used to implement custom providers. Custom retry providers can then be used in the same way as internal retry providers on a <xref:Microsoft.Data.SqlClient.SqlConnection> or <xref:Microsoft.Data.SqlClient.SqlCommand>:

1. <xref:Microsoft.Data.SqlClient.SqlRetryIntervalBaseEnumerator>: Generates a sequence of time intervals.
2. <xref:Microsoft.Data.SqlClient.SqlRetryLogicBase>: Retrieves the next time interval for a given enumerator, if the number of retries has not been exceeded and a transient condition is met.
3. <xref:Microsoft.Data.SqlClient.SqlRetryLogicBaseProvider>: Applies retry logic to connection and command operations.

> [!CAUTION]
> By implementing a custom retry logic provider, you're in charge of all aspects, including concurrency, performance, and exception management.

## Example

The implementation in this sample is as simple as possible to demonstrate step-by-step customization. It doesn't include advanced practices like thread safety, async, and concurrency. For a deep-dive into a real implementation, you can study the pre-defined retry logic in the [Microsoft.Data.SqlClient GitHub repository](https://github.com/dotnet/SqlClient/).

1. Define custom configurable retry logic classes:

    - **Enumerator**: Define a fixed sequence of time intervals and extend the acceptable range of times from two minutes to four minutes.

    [!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#6](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#6)]

    - **Retry logic**: Implement retry logic on any command that isn't part of an active transaction. Lower the number of retries from 60 to 20.

    [!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#7](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#7)]

    - **Provider**: Implements a retry provider that retries on synchronous operations without a `Retrying` event. Adds <xref:System.TimeoutException> to the existing <xref:Microsoft.Data.SqlClient.SqlException> transient exception error numbers.

    [!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#8](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#8)]

2. Create a retry provider instance consisting of the defined custom types:

    [!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#4](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#4)]

    - The following function will evaluate an exception by using the given list of retryable exceptions and the special <xref:System.TimeoutException> exception to determine if it's retryable:

    [!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#5](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#5)]

3. Use the customized retry logic:

    - Define the retry logic parameters:

    [!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#1](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#1)]

    - Create a custom retry provider:

    [!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#2](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#2)]

    - Assign the retry provider to the <xref:Microsoft.Data.SqlClient.SqlConnection.RetryLogicProvider%2A?displayProperty=nameWithType> or <xref:Microsoft.Data.SqlClient.SqlCommand.RetryLogicProvider%2A?displayProperty=nameWithType>:

    [!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#3](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#3)]

> [!NOTE]
> Don't forget to enable the configurable retry logic switch before using it. For more information, see [Enable configurable retry logic](appcontext-switches.md#enable-configurable-retry-logic).

## See also

- [Microsoft.Data.SqlClient GitHub repository](https://github.com/dotnet/SqlClient/)
- [Configurable retry logic in SqlClient](configurable-retry-logic.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
