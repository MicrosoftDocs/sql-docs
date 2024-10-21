---
title: "Server configuration: recovery interval (min)"
description: Learn about the recovery interval option. See how its value affects how often SQL Server issues automatic checkpoints on a database.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "restoring recovery interval [SQL Server]"
  - "checkpoints [SQL Server]"
  - "recovery interval option [SQL Server]"
  - "default recovery interval option"
  - "time [SQL Server], recovery interval"
  - "interval for recovery [SQL Server]"
  - "maximum number of minutes per database recovery"
  - "recovery [SQL Server], recovery interval option"
---
# Server configuration: recovery interval (min)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to configure the `recovery interval (min)` server configuration option in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. The `recovery interval (min)` option defines an upper limit on the time recovering a database should take. The [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] uses the value specified for this option to determine approximately how often to issue [automatic checkpoints](../../relational-databases/logs/database-checkpoints-sql-server.md) on a given database.

The default recovery-interval value is 0, which allows the [!INCLUDE [ssDE](../../includes/ssde-md.md)] to automatically configure the recovery interval. Typically, the default recovery interval results in automatic checkpoints occurring approximately once a minute for active databases and a recovery time of less than one minute. Higher values indicate the approximate maximum recovery time, in minutes. For example, setting the recovery interval to 3 indicates a maximum recovery time of approximately three minutes.

The setting takes effect immediately without restarting the server.

## Limitations

The recovery interval affects only databases that use the default target recovery time (`0`). To override the server recovery interval on a database, configure a non-default target recovery time on the database. For more information, see [Change the target recovery time of a database (SQL Server)](../../relational-databases/logs/change-the-target-recovery-time-of-a-database-sql-server.md).

## Recommendations

This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] professional.

Typically, we recommend that you keep the recovery interval at `0`, unless you experience performance problems. If you decide to increase the recovery-interval setting, we recommend increasing it gradually by small increments and evaluating the effect of each incremental increase on recovery performance.

If you use `sp_configure` to change the value of the `recovery interval (min)` option to more than 60 (minutes), specify `RECONFIGURE WITH OVERRIDE`. `WITH OVERRIDE` disables configuration value checking (for values that aren't valid or are nonrecommended values).

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, you must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

## Use SQL Server Management Studio

1. In Object Explorer, right-click server instance and select **Properties**.

1. Select the **Database settings** node.

1. Under **Recovery**, in the **Recovery interval (minutes)** box, type or select a value from 0 through 32767 to set the maximum amount of time, in minutes, that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] should spend recovering each database at startup. The default is 0, indicating automatic configuration by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. In practice, this means a recovery time of less than one minute and a checkpoint approximately every one minute for active databases.

## Use Transact-SQL

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to set the value of the `recovery interval (min)` option to `3` minutes.

   ```sql
   USE master;
   GO

   EXECUTE sp_configure 'show advanced options', 1;
   GO

   RECONFIGURE;
   GO

   EXECUTE sp_configure 'recovery interval (min)', 3;
   GO

   RECONFIGURE;
   GO

   EXECUTE sp_configure 'show advanced options', 0;
   GO

   RECONFIGURE;
   GO
   ```

For more information, see [Server configuration options](server-configuration-options-sql-server.md).

## Related content

- [Change the target recovery time of a database (SQL Server)](../../relational-databases/logs/change-the-target-recovery-time-of-a-database-sql-server.md)
- [Database checkpoints (SQL Server)](../../relational-databases/logs/database-checkpoints-sql-server.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [show advanced options Server Configuration Option](show-advanced-options-server-configuration-option.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
