---
title: "Server configuration: backup compression default"
description: "Find out about the backup compression default option. See how it determines whether SQL Server creates compressed backups by default, and learn how to set it."
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "SQL Server Management Studio [SQL Server], backup compression default option"
  - "backup compression [SQL Server], backup compression default Option"
---
# Server configuration: backup compression default

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to view or configure the `backup compression default` server configuration option in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. The `backup compression default` option determines whether the server instance creates compressed backups by default. When [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is installed, the `backup compression default` option is off.

## Limitations

Backup compression isn't available in all editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).

By default, compression significantly increases CPU usage, and the additional CPU consumed by the compression process might adversely affect concurrent operations. Therefore, you might want to create low-priority compressed backups in a session whose CPU usage is limited by [Resource Governor](../../relational-databases/resource-governor/resource-governor.md). For more information, see [Use Resource Governor to Limit CPU Usage by Backup Compression](../../relational-databases/backup-restore/use-resource-governor-to-limit-cpu-usage-by-backup-compression-transact-sql.md).

## Recommendations

When you're creating an individual backup, configuring a log shipping configuration, or creating a maintenance plan, you can override the server-level default.

Backup compression is supported for both disk backup devices and tape backup devices.

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, a user must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

#### View or configure the backup compression default option

1. In Object Explorer, right-click a server and select **Properties**.

1. Select the **Database settings** node.

1. Under **Backup and restore**, **Compress backup** shows the current setting of the `backup compression default` option. This setting determines the server-level default for compressing backups, as follows:

   - If the **Compress backup** box is blank, new backups are uncompressed by default.

   - If the **Compress backup** box is checked, new backups are compressed by default.

   If you're a member of the **sysadmin** or **serveradmin** fixed server role, you can also change the default setting by selecting the **Compress backup** box.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

#### View the backup compression default option

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example queries the [sys.configurations](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md) catalog view to determine the value for `backup compression default`. A value of `0` means that backup compression is off, and a value of `1` means that backup compression is enabled.

   ```sql
   SELECT value
   FROM sys.configurations
   WHERE name = 'backup compression default';
   GO
   ```

#### Configure the backup compression default option

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to configure the server instance to create compressed backups by default.

   ```sql
   EXECUTE sp_configure 'backup compression default', 1;

   RECONFIGURE;
   GO
   ```

For more information, see [Server configuration options](server-configuration-options-sql-server.md).

<a id="FollowUp"></a>

## Follow up: After you configure the backup compression default option

The setting takes effect immediately without restarting the server.

## Related content

- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [Backup overview (SQL Server)](../../relational-databases/backup-restore/backup-overview-sql-server.md)
