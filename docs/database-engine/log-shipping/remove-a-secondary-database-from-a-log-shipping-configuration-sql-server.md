---
title: Remove Log Shipping Secondary
description: Learn how to remove a log shipping secondary partner by using SQL Server Management Studio or Transact-SQL in SQL Server.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 02/23/2026
ms.service: sql
ms.subservice: log-shipping
ms.topic: how-to
helpviewer_keywords:
  - "deleting secondary databases"
  - "secondary databases [SQL Server], in log shipping"
  - "removing secondary databases"
  - "secondary data files [SQL Server], removing"
  - "log shipping [SQL Server], secondary databases"
---
# Remove a Secondary Database from a Log Shipping Configuration (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to remove a log shipping secondary database in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. Connect to the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that is currently the log shipping primary server and expand that instance.

1. Expand **Databases**, right-click the log shipping primary database, and then select **Properties**.

1. Under **Select a page**, select **Transaction Log Shipping**.

1. Under **Secondary server instances and databases**, select the database you want to remove.

1. Select **Remove**.

1. Select **OK** to update the configuration.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

1. On the primary server, execute [sp_delete_log_shipping_primary_secondary](../../relational-databases/system-stored-procedures/sp-delete-log-shipping-primary-secondary-transact-sql.md) to delete the information about the secondary database from the primary server.

1. On the secondary server, execute [sp_delete_log_shipping_secondary_database](../../relational-databases/system-stored-procedures/sp-delete-log-shipping-secondary-database-transact-sql.md) to delete the secondary database.

   > [!NOTE]  
   > If there are no other secondary databases with the same secondary ID, `sp_delete_log_shipping_secondary_primary` is invoked from `sp_delete_log_shipping_secondary_database` and deletes the entry for the secondary ID and the copy and restore jobs.

1. On the secondary server, disable the copy and restore jobs. For more information, see [Disable or Enable a Job](/ssms/agent/disable-or-enable-a-job).

## Permissions

The log-shipping stored procedures require membership in the **sysadmin** fixed server role.

## Related tasks

- [Upgrade SQL Server with log shipping (Transact-SQL)](upgrade-sql-server-log-shipping-transact-sql.md)
- [Configure Log Shipping (SQL Server)](../../database-engine/log-shipping/configure-log-shipping-sql-server.md)
- [Add a Secondary Database to a Log Shipping Configuration (SQL Server)](../../database-engine/log-shipping/add-a-secondary-database-to-a-log-shipping-configuration-sql-server.md)
- [Remove Log Shipping (SQL Server)](../../database-engine/log-shipping/remove-log-shipping-sql-server.md)
- [View the Log Shipping Report (SQL Server Management Studio)](../../database-engine/log-shipping/view-the-log-shipping-report-sql-server-management-studio.md)
- [Monitor Log Shipping (Transact-SQL)](../../database-engine/log-shipping/monitor-log-shipping-transact-sql.md)
- [Fail Over to a Log Shipping Secondary (SQL Server)](../../database-engine/log-shipping/fail-over-to-a-log-shipping-secondary-sql-server.md)

## Related content

- [About log shipping (SQL Server)](about-log-shipping-sql-server.md)
- [Log Shipping Tables and Stored Procedures](log-shipping-tables-and-stored-procedures.md)
