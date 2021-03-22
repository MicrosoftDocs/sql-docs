---
title: "Configurable retry logic with SqlClient"
description: "Describes how to use the **configurable retry logic** feature of Microsoft SqlClient Data Provider for SQL Server to establish a connection or execute a command."
ms.date: "03/22/2021"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: David-Engel
ms.author: v-daenge
ms.reviewer: v-deshtehari
---
# Configurable retry logic with SqlClient

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

An application that communicates with elements running in a service, especially in the cloud services must be sensitive to the transient faults that can occur in this environment. These faults are typically self-corrective, and if the action that triggered a fault is repeated after a suitable delay it's likely to be successful.

> [!NOTE]  
> This feature is available since version 3.0.0 preview 1.

## Retry pattern

Attempting to make an operation complete against transient barriers instead of throwing an exception and letting a user decide for the next required action is an intelligent decision called retry pattern. For more information, see [Retry Pattern](/azure/architecture/patterns/retry).

## Transient faults

You can have a robust infrastructure and use well-known applications implemented with the latest technologies to decrease service downtime. However, it's still impossible to make these failures to zero. Technically, transient errors are whatever faults that sometimes happen for known reasons and will disappear after a known estimated time. For instance, when a load balancing is in progress on the server-side causes, the requested services likely failed or timed out in between. For more information, see [Transient faults](/azure/azure-sql/database/troubleshoot-common-connectivity-issues#transient-errors-transient-faults).

## Do and don't

Even though using a retry pattern profoundly affects an application's resiliency, it could negatively impact an application if used in the wrong circumstances. Before adding an exception to the list of transient faults, hesitate a moment and ask yourself, "Will it resolve itself soon?". Don't rush, and put more time to study its reasons if you didn't have a firm answer for the question. For more information, see [Troubleshooting connectivity issues and other errors with Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/troubleshoot-common-errors-issues).

## In this section

[Configurable retry logic introduction](configurable-retry-logic-introduction.md)  
Introduces different section of configurable retry logic.  

[Internal retry logic providers](internal-retry-logic-providers.md)  
Demonstrates how to use pre-defined retry providers to apply the retry logic against database.  

[Configurable retry logic core APIs](configurable-retry-logic-core-apis.md)  
Demonstrates how to use core APIs to implement custom retry logic.  

[Configurable retry logic and configuration file](configurable-retry-logic-config-file.md)  
Demonstrates how to specify default retry logic providers through a configuration file.  

## See also

- [Retry Pattern](/azure/architecture/patterns/retry)
- [Transient faults](/azure/azure-sql/database/troubleshoot-common-connectivity-issues#transient-errors-transient-faults)
- [Troubleshooting connectivity issues and other errors with Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/troubleshoot-common-errors-issues)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
