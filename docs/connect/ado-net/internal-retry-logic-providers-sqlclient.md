---
title: Internal retry logic providers in SqlClient
description: Learn how to use the built-in configurable retry logic providers in your application to handle transient errors against your database.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-deshtehari
ms.date: 03/22/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Internal retry logic providers in SqlClient

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Built-in, internal retry providers have been implemented for the most common retry patterns. You can use the retry providers by using the following <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory?displayProperty=fullName> static methods:

- <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateFixedRetryProvider%2A?displayProperty=nameWithType>
- <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateIncrementalRetryProvider%2A?displayProperty=nameWithType>
- <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateExponentialRetryProvider%2A?displayProperty=nameWithType>
- <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateNoneRetryProvider%2A?displayProperty=nameWithType>

> [!NOTE]
> All of the internal retry providers slightly randomize interval gap times before each retry. This randomization avoids hitting the database at the same time when multiple clients are trying to connect or execute a command with the same configuration.

> [!WARNING]
> Internal retry providers don't support retrying on a command that executes in an open transaction. That operation will execute without retry logic. You can override this behavior by using custom retry logic. For more information, see [Configurable retry logic core APIs in SqlClient](configurable-retry-logic-core-apis-sqlclient.md).

## Example

You can find samples for `connection` and `command` retry logic at the following links:

- <xref:Microsoft.Data.SqlClient.SqlConnection.RetryLogicProvider%2A?displayProperty=nameWithType>
- <xref:Microsoft.Data.SqlClient.SqlCommand.RetryLogicProvider%2A?displayProperty=nameWithType>

## See also

- [Configurable retry logic core APIs in SqlClient](configurable-retry-logic-core-apis-sqlclient.md)
- [Configurable retry logic in SqlClient](configurable-retry-logic.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
