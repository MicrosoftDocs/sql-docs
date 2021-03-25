---
title: Internal retry logic providers in SqlClient
description: Learn how to use the built-in configurable retry logic providers in your application to handle transient errors against your database.
ms.date: 03/22/2021
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: David-Engel
ms.author: v-daenge
ms.reviewer: v-deshtehari
---
# Internal retry logic providers in SqlClient

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Built-in, internal retry providers have been implemented for the most common retry patterns. You can use the retry providers by using the following `Microsoft.Data.SqlClient.SqlConfigurableRetryFactory` static methods:

- `Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateFixedRetryProvider`
- `Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateIncrementalRetryProvider`
- `Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateExponentialRetryProvider`
- `Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateNoneRetryProvider`

> [!NOTE]
> All of the internal retry providers slightly randomize interval gap times before each retry. This randomization avoids hitting the database at the same time when multiple clients are trying to connect or execute a command with the same configuration.

> [!CAUTION]
> Internal retry providers don't support retrying on a command that executes in an open transaction. That operation will execute without retry logic. You can override this behavior by using custom retry logic. For more information, see [Configurable retry logic core APIs in SqlClient](configurable-retry-logic-core-apis-sqlclient.md).

<!-- These links won't be live until after the feature is released in a GA version.
## Example

You can find samples for `connection` and `command` retry logic at the following links:

- [Microsoft.Data.SqlClient.SqlConnection.RetryLogicProvider#example](/dotnet/api/microsoft.data.sqlclient.sqlconnection.RetryLogicProvider?view=sqlclient-dotnet-core-2.1&preserve-view=true#examples)
- [Microsoft.Data.SqlClient.SqlCommand.RetryLogicProvider#example](/dotnet/api/microsoft.data.sqlclient.sqlcommand.RetryLogicProvider?view=sqlclient-dotnet-core-2.1&preserve-view=true#examples)
-->

## See also

- [Configurable retry logic core APIs in SqlClient](configurable-retry-logic-core-apis-sqlclient.md)
- [Configurable retry logic in SqlClient](configurable-retry-logic.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
