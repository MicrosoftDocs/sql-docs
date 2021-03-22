---
title: "Internal retry logic providers"
description: "Demonstrates how to use pre-defined retry providers to apply the retry logic against database."
ms.date: "03/22/2021"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: David-Engel
ms.author: v-daenge
ms.reviewer: v-deshtehari
---
# Internal retry logic providers

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Pre-defined internal retry providers have been implemented the most common retry patterns. You can create the latest retry providers by <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory> class static methods as follows:

- <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateFixedRetryProvider>  
- <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateIncrementalRetryProvider>  
- <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateExponentialRetryProvider>  
- <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateNoneRetryProvider>  

> [!NOTE]  
> All of internal retry providers support slightly randomization in interval gap time before each retry to avoid hitting the backend database at the same time in a case multiple clients are trying to connect or execute a command aginst a database with the same configuration.

> [!CAUTION]  
> Internal retry providers doesn't allow retrying on a command that execute in an open transaction, and that operation will execute without retry logic. This behaviour can be override in a custom retry logic. For more information, see [Configurable retry logic core APIs](configurable-retry-logic-core-apis.md).

## Example
You can find samples for `connection` and `command` from the following links:
- <xref:Microsoft.Data.SqlClient.SqlConnection.RetryLogicProvider#Example>
- <xref:Microsoft.Data.SqlClient.SqlCommand.RetryLogicProvider#Example>

## See also

- [Configurable retry logic core APIs](configurable-retry-logic-core-apis.md)
- [Configurable retry logic with SqlClient](configurable-retry-logic.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
