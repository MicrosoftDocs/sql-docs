---
title: "Configurable retry logic core APIs"
description: "Demonstrates how to use core APIs to implement custom retry logic."
ms.date: "03/22/2021"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: David-Engel
ms.author: v-daenge
ms.reviewer: v-deshtehari
---
# Configurable retry logic core APIs

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

You need to read this article if the pre-defined retry providers don't cover your special retry logic that you wish to apply on a connection establishing or a command executing operations.  

Internal configurable retry logic is designed around these three interfaces that can be used to implement a retry logic from scratch, and customized retry providers will be used as same as internal retry providers by a <xref:Microsoft.Data.SqlClient.SqlConnection> or <xref:Microsoft.Data.SqlClient.SqlCommand>:  

1. <xref:Microsoft.Data.SqlClient.SqlRetryIntervalBaseEnumerator>: Generates a sequence of time intervals.  
2. <xref:Microsoft.Data.SqlClient.SqlRetryLogicBase>: Retrieves the next time interval by a given enumerator, considering number of retries if a transient condition occurs.  
3. <xref:Microsoft.Data.SqlClient.SqlRetryLogicBaseProvider>: Applies a given retry logic to connection and command operations.  

> [!CAUTION]  
> By implementing a custom retry logic, you're in charge of all the aspects of it, including concurrency, performance and exception management.

## Example
In this sample has been tried to keep the implementation as simple as possible just to demonstrate a step-by-step customization, and it doesn't include advanced programing topics like thread safety, async, and concurrency. To deep dive into a real implementation, you can study the pre-defined retry logic in [Microsoft.Data.SqlClient](https://github.com/dotnet/SqlClient/) repository at GitHub.  

1. Define customized configurable retry logic classes:  

- **Enumerator**: Defining a simple fixed sequence of time interval and extend the acceptance range of times from 2 minutes to 4 minutes.  

[!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#6](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#6)]

- **Retry logic**: Implementing a retry logic with accepting an active transaction without skipping any type of commands to apply a retry and decrease the number of retries from 60 to 20 times.  

[!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#7](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#7)]

- **Provider**: Implementing a retry provider just to apply retry on synchronous operations without `Retrying` event and includes <xref:System.TimeoutException> in transient exceptions besides the given <xref:Microsoft.Data.SqlClient.SqlException> error numbers.  

[!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#8](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#8)]

2. Create a retry provider instance of the defined customized types:

[!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#4](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#4)]

3. The following function will assess an exception by the given list of retriable exceptions and the special <xref:System.TimeoutException> exception to find if it's retriable:

[!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#5](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#5)]

4. How to use it?  
- Define the retry logic parameters:  

[!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#1](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#1)]

- Create a custom retry provider:  

[!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#2](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#2)]

- Assign the retry provider to the <xref:Microsoft.Data.SqlClient.SqlConnection.RetryLogicProvider> or <xref:Microsoft.Data.SqlClient.SqlCommand.RetryLogicProvider>:  

[!code-csharp[SqlConfigurableRetryLogic_StepByStep_CustomProvider#3](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_CustomProvider.cs#3)]

> [!NOTE]  
> Don't forget to enable the configurable retry logic switch before using it. For more information, see [Enable configurable retry logic](appcontext-switches.md#Enable-configurable-retry-logic).

## See also

- [Microsoft.Data.SqlClient GitHub repository](https://github.com/dotnet/SqlClient/)
- [Configurable retry logic with SqlClient](configurable-retry-logic.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
