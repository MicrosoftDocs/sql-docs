---
title: Configurable retry logic in SqlClient introduction
description: Learn about the different aspects of configurable retry logic in Microsoft.Data.SqlClient and how to make your application resilient to transient errors.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-deshtehari
ms.date: 03/22/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Configurable retry logic in SqlClient introduction

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Configurable retry logic lets developers and administrators manage application behavior when transient faults happen. The feature adds controls during connection or execution of a command. The controls can be defined through code or an application configuration file. Transient error numbers and retry properties can be defined to control retry behavior. Also, regular expressions can be used to filter specific SQL statements.

## Feature components

This feature consists of three main components:

1. **Core APIs**: Developers can use these interfaces to implement their own retry logic on <xref:Microsoft.Data.SqlClient.SqlConnection> and <xref:Microsoft.Data.SqlClient.SqlCommand> objects. For more information, see [Configurable retry logic core APIs in SqlClient](configurable-retry-logic-core-apis-sqlclient.md).
2. **Pre-defined configurable retry logic**: Built-in retry logic methods using the core APIs are accessible from the <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory> class. For more information, see [Internal retry logic providers in SqlClient](internal-retry-logic-providers-sqlclient.md).
3. **Configuration file schema**: To specify the default retry logic for <xref:Microsoft.Data.SqlClient.SqlConnection> and <xref:Microsoft.Data.SqlClient.SqlCommand> in an application. For more information, see [Configurable retry logic configuration file with SqlClient](configurable-retry-logic-config-file-sqlclient.md).

## Quick start

To use this feature, follow these four steps:

1. Enable the safety switch in the preview version. For information on how to enable the AppContext safety switch, see [Enable configurable retry logic](appcontext-switches.md#enable-configurable-retry-logic).

2. Define the retry logic options using <xref:Microsoft.Data.SqlClient.SqlRetryLogicOption>.  
In this sample, some of the retry parameters are set and the rest of them will use the default values.

    [!code-csharp[SqlConfigurableRetryLogic_StepByStep_OpenConnection#1](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_OpenConnection.cs#1)]

3. Create a retry logic provider using your <xref:Microsoft.Data.SqlClient.SqlRetryLogicOption> object.

    [!code-csharp[SqlConfigurableRetryLogic_StepByStep_OpenConnection#2](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_OpenConnection.cs#2)]

4. Assign the <xref:Microsoft.Data.SqlClient.SqlRetryLogicBaseProvider> instance to the <xref:Microsoft.Data.SqlClient.SqlConnection.RetryLogicProvider%2A?displayProperty=nameWithType> or <xref:Microsoft.Data.SqlClient.SqlCommand.RetryLogicProvider%2A?displayProperty=nameWithType>.  
In this sample, the connection open command will retry if it hits one of the transient errors in the <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory> internal list for a maximum of five times.

    [!code-csharp[SqlConfigurableRetryLogic_StepByStep_OpenConnection#3](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_StepByStep_OpenConnection.cs#3)]

> [!NOTE]
> These steps are the same for a command execution, except you would instead assign the retry provider to the <xref:Microsoft.Data.SqlClient.SqlCommand.RetryLogicProvider%2A?displayProperty=nameWithType> property before executing the command.

## See also

- [Configurable retry logic core APIs in SqlClient](configurable-retry-logic-core-apis-sqlclient.md)
- [Internal retry logic providers in SqlClient](internal-retry-logic-providers-sqlclient.md)
- [Configurable retry logic configuration file with SqlClient](configurable-retry-logic-config-file-sqlclient.md)
- [Enable configurable retry logic](appcontext-switches.md#enable-configurable-retry-logic)
- [Configurable retry logic in SqlClient](configurable-retry-logic.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
