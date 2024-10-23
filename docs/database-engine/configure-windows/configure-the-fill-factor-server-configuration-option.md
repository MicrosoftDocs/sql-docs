---
title: "Server configuration: fill factor"
description: Learn about the fill factor option. See how to configure it to specify the percentage of space on each leaf-level page that SQL Server fills with data.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "fill factor option [SQL Server]"
---
# Server configuration: fill factor

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to configure the `fill factor` server configuration option in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. Fill factor is provided for fine-tuning index data storage and performance. When an index is created or rebuilt, the fill-factor value determines the percentage of space on each leaf-level page to be filled with data, reserving the rest as free space for future growth. For more information, see [Specify Fill Factor for an Index](../../relational-databases/indexes/specify-fill-factor-for-an-index.md).

## Recommendations

This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] professional.

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, a user must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. In Object Explorer, right-click a server and select **Properties**.

1. Select the **Database Settings** node.

1. In the **Default index fill factor** box, type or select the index fill factor that you want.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to set the value of the `fill factor` option to `100`.

   ```sql
   USE master;
   GO

   EXECUTE sp_configure 'show advanced options', 1;
   GO

   RECONFIGURE;
   GO

   EXECUTE sp_configure 'fill factor', 100;
   GO

   RECONFIGURE;
   GO

   EXECUTE sp_configure 'show advanced options', 0;
   GO

   RECONFIGURE;
   GO
   ```

For more information, see [Server configuration options](server-configuration-options-sql-server.md).

<a id="FollowUp"></a>

## Follow up: After you configure the fill factor option

The server must be restarted before the setting can take effect.

## Related content

- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [ALTER INDEX (Transact-SQL)](../../t-sql/statements/alter-index-transact-sql.md)
- [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md)
- [Specify Fill Factor for an Index](../../relational-databases/indexes/specify-fill-factor-for-an-index.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [sys.indexes (Transact-SQL)](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)
