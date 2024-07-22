---
title: Automation in Azure SQL overview
titleSuffix: Azure SQL Database & Azure SQL Managed Instance & Azure Synapse Analytics
description: "Features for job automation to run Transact-SQL (T-SQL) scripts include elastic jobs on Azure SQL Database and SQL Agent jobs on Azure SQL Managed instance."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: srinia, mathoma
ms.date: 04/09/2024
ms.service: sql-db-mi
ms.topic: conceptual
ms.custom: sqldbrb=1
dev_langs:
  - "TSQL"
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---
# Automate management tasks in Azure SQL

[!INCLUDE [appliesto-sqldb-sqlmi-asa-ss](../includes/appliesto-sqldb-sqlmi-asa-ss.md)]

This article summarizes job automation options in Azure SQL platforms, including [Azure SQL Database](sql-database-paas-overview.md), [Azure SQL Database elastic pools](elastic-pool-overview.md), [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md), and [Azure Synapse Analytics](/azure/synapse-analytics/overview-what-is).

Consider the following job scheduling and task automation technologies on different Azure SQL platforms:

- **Elastic jobs** are job scheduling services that execute custom jobs on one or many databases in [Azure SQL Database](sql-database-paas-overview.md) or [Azure SQL Database elastic pools](elastic-pool-overview.md). For more information, see the [elastic jobs overview](elastic-jobs-overview.md).
- **SQL Agent Jobs** are executed by the [SQL Agent service](/sql/ssms/agent/sql-server-agent) that continues to be used for task automation in SQL Server and is also included with Azure SQL Managed Instances. For T-SQL script job automation in Azure SQL Managed Instance, consider [SQL Agent for Azure SQL Managed Instance](../managed-instance/job-automation-managed-instance.md). The SQL Agent on SQL managed instances is very similar to SQL Server. SQL Agent is not available in Azure SQL Database.
- **Pipelines with recurring triggers** can be used for T-SQL script automation in Azure Synapse Analytics. [Pipelines with recurring triggers](/azure/synapse-analytics/data-integration/concepts-data-factory-differences) are [based on Azure Data Factory](/azure/synapse-analytics/data-integration/concepts-data-factory-differences).

## Differences between SQL Agent and elastic jobs

The following table summarizes key differences between elastic jobs and SQL Agent:

| |**Elastic jobs** |**SQL Agent** |
|---------|---------|---------|
|**Platform**| [Azure SQL Database](elastic-jobs-overview.md) | [SQL Server](/sql/ssms/agent/sql-server-agent), [Azure SQL Managed Instance](../managed-instance/job-automation-managed-instance.md) |
|**Scope** | Any number of databases in Azure SQL Database only. Targets can be in different logical servers, subscriptions, and/or regions (dynamically enumerated at job runtime). | Any individual database in the same instance as the SQL Agent.<br /><br />The [Multi Server Administration (MSX/TSX) feature](/sql/ssms/agent/create-a-multiserver-environment) of SQL Agent allows for master/target instances to coordinate job execution, though this feature is not available in SQL Managed Instance. |
|**Supported APIs and tools** | T-SQL, PowerShell, REST APIs, Azure portal, Azure Resource Manager | T-SQL, PowerShell, SQL Server Management Studio (SSMS) |

## Next step

> [!div class="nextstepaction"]
> [Elastic jobs in Azure SQL Database](elastic-jobs-overview.md)
