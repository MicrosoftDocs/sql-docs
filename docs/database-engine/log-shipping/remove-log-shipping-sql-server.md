---
title: "Remove Log Shipping (SQL Server)"
description: Learn how to remove log shipping by using SQL Server Management Studio or Transact-SQL in SQL Server.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 02/23/2026
ms.service: sql
ms.subservice: log-shipping
ms.topic: how-to
helpviewer_keywords:
  - "log shipping [SQL Server], removing"
  - "removing log shipping"
  - "deleting log shipping"
---
# Remove Log Shipping (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to remove log shipping in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. Connect to the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that is currently the log shipping primary server, and expand that instance.

1. Expand **Databases**, right-click the log shipping primary database, and then select **Properties**.

1. Under **Select a page**, select **Transaction Log Shipping**.

1. Clear the **Enable this as a primary database in a log shipping configuration** check box.

1. Select **OK** to remove log shipping from this primary database.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

1. On the log shipping primary server, execute [sp_delete_log_shipping_primary_secondary](../../relational-databases/system-stored-procedures/sp-delete-log-shipping-primary-secondary-transact-sql.md) to delete the information about the secondary database from the primary server.

1. On the log shipping secondary server, execute [sp_delete_log_shipping_secondary_database](../../relational-databases/system-stored-procedures/sp-delete-log-shipping-secondary-database-transact-sql.md) to delete the secondary database.

   > [!NOTE]  
   > If there are no other secondary databases with the same secondary ID, `sp_delete_log_shipping_secondary_primary` is invoked from `sp_delete_log_shipping_secondary_database` and deletes the entry for the secondary ID and the copy and restore jobs.

1. On the log shipping primary server, execute `sp_delete_log_shipping_primary_database` to delete information about the log shipping configuration from the primary server. This also deletes the backup job.

1. On the log shipping primary server, disable the backup job. For more information, see [Disable or Enable a Job](/ssms/agent/disable-or-enable-a-job).

1. On the log shipping secondary server, disable the copy and restore jobs.

1. Optionally, if you're no longer using the log shipping secondary database, you can delete it from the secondary server.

## Permissions

The log-shipping stored procedures require membership in the **sysadmin** fixed server role.

## Related tasks

- [Upgrade SQL Server with log shipping (Transact-SQL)](upgrade-sql-server-log-shipping-transact-sql.md)
- [Configure Log Shipping (SQL Server)](../../database-engine/log-shipping/configure-log-shipping-sql-server.md)
- [Add a Secondary Database to a Log Shipping Configuration (SQL Server)](../../database-engine/log-shipping/add-a-secondary-database-to-a-log-shipping-configuration-sql-server.md)
- [Remove a Secondary Database from a Log Shipping Configuration (SQL Server)](../../database-engine/log-shipping/remove-a-secondary-database-from-a-log-shipping-configuration-sql-server.md)
- [Monitor Log Shipping (Transact-SQL)](../../database-engine/log-shipping/monitor-log-shipping-transact-sql.md)
- [Fail Over to a Log Shipping Secondary (SQL Server)](../../database-engine/log-shipping/fail-over-to-a-log-shipping-secondary-sql-server.md)
- [Disable or Enable a Job](/ssms/agent/disable-or-enable-a-job)

## Related content

- [About log shipping (SQL Server)](about-log-shipping-sql-server.md)
- [Log Shipping Tables and Stored Procedures](log-shipping-tables-and-stored-procedures.md)
