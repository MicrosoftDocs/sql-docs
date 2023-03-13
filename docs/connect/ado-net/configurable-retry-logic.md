---
title: Configurable retry logic in SqlClient
description: Learn how to use the configurable retry logic feature of Microsoft.Data.SqlClient when establishing a connection or executing a command.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-deshtehari
ms.date: 03/22/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Configurable retry logic in SqlClient

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

An application that communicates with elements running in the cloud has to be sensitive to the transient faults that can occur in this environment. These faults are typically self-correcting. If the action that triggered a fault is repeated after a suitable delay, it's likely to be successful.

> [!NOTE]
> This feature is available starting with Microsoft.Data.SqlClient version 3.0.0 preview 1.

## Retry pattern

Attempting to complete an operation despite transient errors, instead of throwing an exception and letting a user decide the next action, is an intelligent decision called a retry pattern. For more information, see [Retry pattern](/azure/architecture/patterns/retry).

## Transient faults

You can have a robust infrastructure and use well-known applications implemented with the latest technologies to reduce service downtime. However, it's impossible to reduce failures to zero. Transient errors are those faults that sometimes happen for known reasons and will disappear after a short time. For example, when a load-balancing change is in progress on the server-side, it may briefly cause requested services to fail or time out. For more information, see [Transient faults](/azure/azure-sql/database/troubleshoot-common-connectivity-issues#transient-errors-transient-faults).

## Do and don't

Even though using a retry pattern greatly improves an application's resiliency, it could negatively impact an application if used in the wrong circumstances. Before adding an exception to the list of transient faults, pause for a moment and ask yourself, "Will it resolve itself soon?". Don't rush. Study the reasons if you don't have a good answer for the question. For more information, see [Troubleshooting connectivity issues and other errors with Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/troubleshoot-common-errors-issues).

## In this section

[Configurable retry logic in SqlClient introduction](configurable-retry-logic-sqlclient-introduction.md)  
Introduces different section of configurable retry logic.

[Internal retry logic providers in SqlClient](internal-retry-logic-providers-sqlclient.md)  
Demonstrates how to use pre-defined retry providers to apply the retry logic against database.

[Configurable retry logic core APIs in SqlClient](configurable-retry-logic-core-apis-sqlclient.md)  
Demonstrates how to use core APIs to implement custom retry logic.

[Configurable retry logic configuration file with SqlClient](configurable-retry-logic-config-file-sqlclient.md)  
Demonstrates how to specify default retry logic providers through a configuration file.

## See also

- [Retry Pattern](/azure/architecture/patterns/retry)
- [Transient faults](/azure/azure-sql/database/troubleshoot-common-connectivity-issues#transient-errors-transient-faults)
- [Troubleshooting connectivity issues and other errors with Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/troubleshoot-common-errors-issues)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
