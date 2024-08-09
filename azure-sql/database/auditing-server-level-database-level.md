---
title: Auditing policy at the server and database level
titleSuffix: Azure SQL Database & Azure Synapse Analytics
description: This article explains the differences for Auditing policies of Azure SQL Database and Azure Synapse Analytics at the server and database level.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: mathoma
ms.date: 04/26/2023
ms.service: azure-sql-database
ms.subservice: security
ms.topic: conceptual
---
# Auditing policy at the server and database level

[!INCLUDE[appliesto-sqldb-asa](../includes/appliesto-sqldb-asa.md)]

This article highlights Auditing policies for [Azure SQL Database](sql-database-paas-overview.md) and [Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is) at the server level and the database level.

## Define server-level vs. database-level auditing policy

An auditing policy can be defined for a specific database or as a default [server](logical-servers.md) policy in Azure (which hosts SQL Database or Azure Synapse):

- A server policy applies to all existing and newly created databases on the server.

- If *server auditing is enabled*, it *always applies to the database*. The database is audited regardless of the database auditing settings.

- When an auditing policy is defined at the database-level to a Log Analytics workspace or an Event Hubs destination, the following operations don't keep the source database-level auditing policy:

  - [Database copy](database-copy.md)
  - [Point-in-time restore](recovery-using-backups.md)
  - [Geo-replication](active-geo-replication-overview.md) (secondary database doesn't have database-level auditing)

- Enabling auditing on the database in addition to enabling auditing on the server *doesn't* override or change any of the settings of the server auditing. Both audits exist side by side. In other words, the database is audited twice in parallel; once by the server policy and once by the database policy.

  > [!NOTE]  
  > You should avoid enabling both server auditing and database blob auditing together, unless:
  >
  > - You want to use a different *storage account*, *retention period* or *Log Analytics Workspace* for a specific database.
  > - You want to audit event types or categories for a specific database that differ from the rest of the databases on the server. For example, you might have table inserts that need to be audited only for a specific database.
  >
  > Otherwise, we recommended that you enable only server-level auditing and leave the database-level auditing disabled for all databases.

## See also

- [Auditing overview](auditing-overview.md)
- Data Exposed episode [What's New in Azure SQL Auditing](/Shows/Data-Exposed/Whats-New-in-Azure-SQL-Auditing)
- [Auditing for SQL Managed Instance](../managed-instance/auditing-configure.md)
- [Auditing for SQL Server](/sql/relational-databases/security/auditing/sql-server-audit-database-engine)
