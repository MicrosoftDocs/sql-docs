---
title: "Server configuration: remote login timeout"
description: "Learn about the remote login timeout option. See how it limits the number of seconds that SQL Server allots for connecting to a remote server."
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "remote login timeout option"
---
# Server configuration: remote login timeout

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to configure the `remote login timeout` server configuration option in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. The `remote login timeout` option specifies the number of seconds to wait before returning from a failed attempt to log in to a remote server. For example, if you're trying to log in to a remote server and that server is down, `remote login timeout` helps make sure that you don't have to wait indefinitely before your computer stops trying to log in. The default value for this option is 10 seconds. A value of `0` allows for an infinite wait.

> [!NOTE]  
> The default value for this option is 20 seconds in [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)].

## Limitations

The `remote login timeout` option affects connections to OLE DB providers made for heterogeneous queries.

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, a user must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. In Object Explorer, right-click a server and select **Properties**.

1. Select the **Advanced** node.

1. Under **Network**, select a value for the **Remote Login Timeout** box.

     Use the **remote login timeout** option to specify the number of seconds to wait before returning from a failed remote login attempt.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to set the value of the `remote login timeout` option to `35` seconds.

   ```sql
   USE master;
   GO

   EXECUTE sp_configure 'remote login timeout', 35;
   GO

   RECONFIGURE;
   GO
   ```

For more information, see [Server configuration options](server-configuration-options-sql-server.md).

<a id="FollowUp"></a>

## Follow up: After you configure the remote login timeout option

The setting takes effect immediately without restarting the server.

## Related content

- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
