---
title: "Configure remote access (server configuration option)"
description: Learn about alternatives to the deprecated remote access option. View other sources for troubleshooting issues with SQL Server connections.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/02/2023
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "remote servers [SQL Server], stored procedure execution"
---
# Configure remote access (server configuration option)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article is about the **remote access** configuration option, which is a deprecated [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] communication feature.

This option affects servers that are added by using [sp_addserver](../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md) and [sp_addlinkedserver](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md). You should leave **remote access** enabled (the default) if you use [linked servers](../../relational-databases/linked-servers/linked-servers-database-engine.md).

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

If you reached this page because you're having trouble connecting to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see one of the following articles instead:

- [Tutorial: Getting Started with the Database Engine](../../relational-databases/tutorial-getting-started-with-the-database-engine.md)
- [Logging In to SQL Server](../../database-engine/configure-windows/logging-in-to-sql-server.md)
- [Connect to SQL Server When System Administrators Are Locked Out](../../database-engine/configure-windows/connect-to-sql-server-when-system-administrators-are-locked-out.md)
- [Connect to a Registered Server (SQL Server Management Studio)](../../ssms/register-servers/connect-to-a-registered-server-sql-server-management-studio.md)
- [Connect to Any SQL Server Component from SQL Server Management Studio](../../ssms/f1-help/connect-to-any-sql-server-component-from-sql-server-management-studio.md)
- [Connect to the Database Engine With sqlcmd](../../tools/sqlcmd/sqlcmd-connect-database-engine.md)
- [How to Troubleshoot Connecting to the SQL Server Database Engine](/troubleshoot/sql/connect/network-related-or-instance-specific-error-occurred-while-establishing-connection)

Programmers may be interested in the following articles:

- [Quickstart: Use .NET Core (C#) to query a database](/azure/azure-sql/database/connect-query-dotnet-core)
- [Connecting to an Instance of SQL Server](../../relational-databases/server-management-objects-smo/create-program/connecting-to-an-instance-of-sql-server.md)
- [Add new connections in Visual Studio](/visualstudio/data-tools/add-new-connections)

## Manage remote access

The **remote access** configuration option controls the execution of stored procedures from local or remote servers on which instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] are running.

The default value for the **remote access** option is `1` (enabled). This grants permission to run local stored procedures from remote servers or remote stored procedures from the local server. To prevent local stored procedures from being run from a remote server or remote stored procedures from being run on the local server, set the option to `0` (disabled).

This setting doesn't take effect until you restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

Remote access is required for the log shipping status report in SQL Server Management Studio (SSMS) to work and the LSAlert Job to complete appropriately.

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default.

To execute `sp_configure` with both parameters to change a configuration option, or to run the RECONFIGURE statement, a user must be granted the ALTER SETTINGS server-level permission. The ALTER SETTINGS permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

1. In Object Explorer, right-click a server and select **Properties**.

1. Select the **Connections** node.

1. Under **Remote server connections**, select or clear the **Allow remote connections to this server** check box.

## <a id="TsqlProcedure"></a> Use Transact-SQL

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to set the value of the `remote access` option to `0`.

```sql
EXEC sp_configure 'remote access', 0;
GO
RECONFIGURE;
GO
```

For more information, see [Server configuration options (SQL Server)](../../database-engine/configure-windows/server-configuration-options-sql-server.md).

## Next steps

- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server Configuration Options (SQL Server)](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
