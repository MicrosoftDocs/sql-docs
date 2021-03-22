---
title: "Configurable retry logic introduction"
description: "Introduces different sections of configurable retry logic."
ms.date: "03/22/2021"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: David-Engel
ms.author: v-daenge
ms.reviewer: v-deshtehari
---
# Configurable retry logic introduction

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Configurable retry logic lets developers and application administrators to manage an application behavior against database with transient failures to establish a connection or execute a command through code or application configuration file, by defining the list of transient error numbers and specifying the retry properties. In addition to the transient error numbers, they can use regular expression to include or exclude a command execution from automatic retries for a specific group of SQL statement. 

## Feature components

This feature is composed by three main components:

1. **Core APIs**: Developers specifically can use these interfaces to implement their own retry logic to use by <xref:Microsoft.Data.SqlClient.SqlConnection> and <xref:Microsoft.Data.SqlClient.SqlCommand>. For more information, see [Configurable retry logic core APIs](configurable-retry-logic-core-apis.md). 
2. **Pre-defined configurable retry logic**: The internal implementation of the common retry logic methods using `core APIs`, which is accessible by <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory> class. For more information, see [Internal retry logic providers](internal-retry-logic-providers.md). 
3. **Configuration file schema**: To specify the default retry logic per <xref:Microsoft.Data.SqlClient.SqlConnection> and <xref:Microsoft.Data.SqlClient.SqlCommand> in an application. For more information, see [Configurable retry logic and configuration file](configurable-retry-logic-config-file.md). 

## Quick start

To use this feature, follow the 4 below steps:

1. Enable the safety switch in the preview version. To see how to enable the safety switch using the AppContext see [Enable configurable retry logic](appcontext-switches.md#Enable-configurable-retry-logic).

2. Define the retry logic specifications using <xref:Microsoft.Data.SqlClient.SqlRetryLogicOption> type.  
In this sample, some of the retry parameters are set and the rest of them will use the default values.

[!code-csharp[SqlConfigurableRetryLogic_StepByStep_OpenConnection#1](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_OpenConnection.cs#1)]

3. Create a retry logic provider using the defined <xref:Microsoft.Data.SqlClient.SqlRetryLogicOption> object.

[!code-csharp[SqlConfigurableRetryLogic_StepByStep_OpenConnection#2](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_OpenConnection.cs#2)]

4. Assign the defined <xref:Microsoft.Data.SqlClient.SqlRetryLogicBaseProvider> instance to the <xref:Microsoft.Data.SqlClient.SqlConnection.RetryLogicProvider> or <xref:Microsoft.Data.SqlClient.SqlCommand.RetryLogicProvider>.   
In this sample, a connection opening will retry to establish a connection if it hit one of the transient errors of the <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory> internal list for maximum five times.

[!code-csharp[SqlConfigurableRetryLogic_StepByStep_OpenConnection#3](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_OpenConnection.cs#3)]

> [!NOTE]  
> These steps are the same for a command execution except assign the retry provider object to <xref:Microsoft.Data.SqlClient.SqlCommand.RetryLogicProvider> propertry before executing the command.

## See also

- [Configurable retry logic core APIs](configurable-retry-logic-core-apis.md)
- [Internal retry logic providers](internal-retry-logic-providers.md)
- [Configurable retry logic and configuration file](configurable-retry-logic-config-file.md)
- [Enable configurable retry logic](appcontext-switches.md#Enable-configurable-retry-logic)
- [Configurable retry logic with SqlClient](configurable-retry-logic.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
