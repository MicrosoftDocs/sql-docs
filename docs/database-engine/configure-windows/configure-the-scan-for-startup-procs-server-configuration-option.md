---
title: "Server configuration: scan for startup procs"
description: "Learn about the scan for startup procs option. See how it specifies whether SQL Server scans for and runs all automatically run stored procedures at startup."
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "scan for startup procs option"
---
# Server configuration: scan for startup procs

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to configure the `scan for startup procs` server configuration option in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. Use the `scan for startup procs` option to scan for automatic execution of stored procedures at [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] startup time. If this option is set to 1, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] scans for and runs all automatically run stored procedures that are defined on the server. The default value for `scan for startup procs` is `0` (don't scan).

## Recommendations

This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] professional.

The value for this option can be set by using `sp_configure`; however, the option is set automatically if you use `sp_procoption`, which is used to mark or unmark automatically run stored procedures. When `sp_procoption` is used to mark the first stored procedure as an autoproc, this option is set automatically to a value of `1`. When `sp_procoption` is used to unmark the last stored procedure as an autoproc, this option is automatically set to a value of `0`. If you use `sp_procoption` to mark and unmark autoprocs, and if you always unmark autoprocs before dropping them, there's no need to set this option manually.

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, a user must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. In Object Explorer, right-click a server and select **Properties**.

1. Select the **Advanced** node.

1. Under **Miscellaneous**, change the **Scan for Startup Procs** option to True or False by selecting the value you want from the dropdown list box.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to set the value of the `scan for startup procs` option to `1`.

   ```sql
   USE master;
   GO

   EXECUTE sp_configure 'show advanced options', 1;
   GO

   RECONFIGURE;
   GO

   EXECUTE sp_configure 'scan for startup procs', 1;
   GO

   RECONFIGURE;
   GO

   EXECUTE sp_configure 'show advanced options', 0;
   GO

   RECONFIGURE;
   GO
   ```

<a id="FollowUp"></a>

## Follow up: After you configure the scan for startup procs option

The server must be restarted before the setting can take effect.

## Related content

- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [sp_procoption (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-procoption-transact-sql.md)
