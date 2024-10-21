---
title: View or change server properties (SQL Server)
description: Learn how to use SQL Server Management Studio, Transact-SQL, or SQL Server Configuration Manager to view or change the properties of an instance of SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.connectionproperties.f1"
helpviewer_keywords:
  - "viewing server properties"
  - "server properties [SQL Server]"
  - "displaying server properties"
  - "servers [SQL Server], viewing"
  - "Connection Properties dialog box"
---
# View or change server properties (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to view or change the properties of an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE [tsql](../../includes/tsql-md.md)], or SQL Server Configuration Manager.

Steps depend on the tool:

- [SQL Server Management Studio](#SSMSProcedure)
- [Transact-SQL](#TsqlProcedure)
- [SQL Server Configuration Manager](#sql-server-configuration-manager)

## Limitations

When using `sp_configure`, you must run either `RECONFIGURE` or `RECONFIGURE WITH OVERRIDE` after setting a configuration option. The `RECONFIGURE WITH OVERRIDE` statement is usually reserved for configuration options that should be used with extreme caution. However, `RECONFIGURE WITH OVERRIDE` works for all configuration options, and you can use it in place of `RECONFIGURE`.

> [!NOTE]  
> `RECONFIGURE` executes within a transaction. If any of the reconfigure operations fail, none of the reconfigure operations will take effect.

Some property pages present information obtained via Windows Management Instrumentation (WMI). To display those pages, WMI must be installed on the computer running [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)].

## Server-level roles

For more information, see [Server-level roles](../../relational-databases/security/authentication-access/server-level-roles.md).

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, a user must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

<a id="SSMSProcedure"></a>

## SQL Server Management Studio

### View or change server properties

1. In Object Explorer, right-click a server, and then select **Properties**.

1. In the **Server Properties** dialog box, select a page to view or change server information about that page. Some properties are read-only.

<a id="TsqlProcedure"></a>

## Transact-SQL

### View server properties by using the SERVERPROPERTY built-in function

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example uses the [SERVERPROPERTY](../../t-sql/functions/serverproperty-transact-sql.md) built-in function in a `SELECT` statement to return information about the current server. This scenario is useful when there are multiple instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installed on a Windows-based server, and the client must open another connection to the same instance that is used by the current connection.

   ```sql
   SELECT CONVERT (sysname, SERVERPROPERTY('servername'));
   GO
   ```

### View server properties by using the sys.servers catalog view

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example queries the [sys.servers](../../relational-databases/system-catalog-views/sys-servers-transact-sql.md) catalog view to return the name (`name`) and ID (`server_id`) of the current server, and the name of the OLE DB provider (`provider`) for connecting to a linked server.

   ```sql
   USE master;
   GO

   SELECT name,
          server_id,
          provider
   FROM sys.servers;
   GO
   ```

### View server properties by using the sys.configurations catalog view

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example queries the [sys.configurations](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md) catalog view to return information about each server configuration option on the current server. The example returns the name (`name`) and description (`description`) of the option, its value (`value`), and whether the option is an advanced option (`is_advanced`).

    ```sql
    SELECT name,
           description,
           value,
           is_advanced
    FROM sys.configurations;
    GO
    ```

### Change a server property by using sp_configure

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to change a server property. The example changes the value of the `fill factor` option to `100`. The server must be restarted before the change can take effect.

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

## SQL Server Configuration Manager

Some server properties can be viewed or changed by using SQL Server Configuration Manager. For example, you can view the version and edition of the instance of SQL Server, or change the location where error log files are stored. These properties can also be viewed by querying the [Server dynamic management views and functions](../../relational-databases/system-dynamic-management-views/server-related-dynamic-management-views-and-functions-transact-sql.md).

### View or change server properties

1. On the **Start** menu, point to **All Programs**, point to [!INCLUDE [ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then select **SQL Server Configuration Manager**.

1. In **SQL Server Configuration Manager**, select **SQL Server Services**.

1. In the details pane, right-click **SQL Server (\<**_instancename_**>)**, and then select **Properties**.

1. In the **SQL Server (\<**_instancename_**>) Properties** dialog box, change the server properties on the **Service** tab or the **Advanced** tab, and then select **OK**.

## Restart after changes

For some properties, you might need to restart the server before the change can take effect.

## Related content

- [Server configuration options](server-configuration-options-sql-server.md)
- [Connect to the Database Engine](../../sql-server/connect-to-database-engine.md)
- [SET Statements (Transact-SQL)](../../t-sql/statements/set-statements-transact-sql.md)
- [SERVERPROPERTY (Transact-SQL)](../../t-sql/functions/serverproperty-transact-sql.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [SELECT (Transact-SQL)](../../t-sql/queries/select-transact-sql.md)
- [Configure WMI to Show Server Status in SQL Server Tools](../../ssms/configure-wmi-to-show-server-status-in-sql-server-tools.md)
- [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md)
- [Configuration Functions (Transact-SQL)](../../t-sql/functions/configuration-functions-transact-sql.md)
- [Server dynamic management views and functions (Transact-SQL)](../../relational-databases/system-dynamic-management-views/server-related-dynamic-management-views-and-functions-transact-sql.md)
