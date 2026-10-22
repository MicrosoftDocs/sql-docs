---
title: Auditing Policy at the Server and Database Level
titleSuffix: Azure SQL Database
description: Learn how server-level and database-level auditing policies differ in Azure SQL Database and how enabling both affects audit output.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: vanto
ms.date: 10/05/2026
ms.service: azure-sql-database
ms.subservice: security
ms.topic: concept-article
monikerRange: "=azuresql || =azuresql-db"
---
# Auditing policy at the server and database level

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article highlights auditing policies for [Azure SQL Database](sql-database-paas-overview.md) at the server level and the database level.

## Define server-level vs. database-level auditing policy

You can define an auditing policy for a specific database or as a default [server](logical-servers.md) policy in Azure SQL Database:

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

## Auditing in Azure Synapse Analytics

For information about server-level and database-level auditing in Azure Synapse Analytics, see [Server-level and database-level auditing](/azure/synapse-analytics/sql/auditing-server-level-database-level).

## Related content

- [Auditing for Azure SQL Database](auditing-overview.md)
- [What's New in Azure SQL Auditing](/Shows/Data-Exposed/Whats-New-in-Azure-SQL-Auditing)
- [Get started with Azure SQL Managed Instance auditing](../managed-instance/auditing-configure.md)
- [Auditing for SQL Server](/sql/relational-databases/security/auditing/sql-server-audit-database-engine)
