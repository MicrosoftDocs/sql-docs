---
title: "Configure the remote query timeout (server configuration option)"
description: "Learn about the remote query timeout option. See how it determines the number of seconds that a remote operation can take before SQL Server times out."
author: rwestMSFT
ms.author: randolphwest
ms.date: 03/08/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "time limit for remote queries [SQL Server]"
  - "remote query timeout option"
---
# Configure the remote query timeout (server configuration option)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to configure the **remote query timeout** server configuration option in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

The **remote query timeout** option specifies how long, in seconds, a remote operation can take before [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] times out. The default value for this option is `600`, which is a 10-minute wait. Setting this value to `0` disables the time-out. This value applies to an outgoing connection initiated by the [!INCLUDE [ssDE](../../includes/ssde-md.md)] as a remote query. This value has no effect on queries received by the [!INCLUDE [ssDE](../../includes/ssde-md.md)]. A query waits until it completes.

For heterogeneous queries, **remote query timeout** specifies the number of seconds (initialized in the command object using the `DBPROP_COMMANDTIMEOUT` rowset property) that a remote provider should wait for result sets before the query times out. This value is also used to set `DBPROP_GENERALTIMEOUT` if supported by the remote provider. This will cause any other operations to time out after the specified number of seconds.

For remote stored procedures, **remote query timeout** specifies the number of seconds that must elapse after sending a remote `EXEC` statement before the remote stored procedure times out.

> [!NOTE]  
> The **remote query timeout** server configuration setting is unrelated to connection and query time-out errors. For more information, see [Troubleshoot query time-out errors](/troubleshoot/sql/database-engine/performance/troubleshoot-query-timeouts).

## Prerequisites

Remote server connections must be allowed before this value can be set.

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, a user must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

1. In Object Explorer, right-click a server and select **Properties**.

1. Select the **Connections** node.

1. Under **Remote server connections**, in the **Remote query timeout** box, type or select a value from 0 through 2,147,483,647 to set the maximum number seconds for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to wait before timing out.

## <a id="TsqlProcedure"></a> Use Transact-SQL

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to set the value of the `remote query timeout` option to `0` to disable the time-out.

```sql
USE AdventureWorks2022;
GO
EXEC sp_configure 'remote query timeout', 0;
GO
RECONFIGURE;
GO
```

For more information, see [Server configuration options (SQL Server)](server-configuration-options-sql-server.md).

## Follow up: After you configure the remote query timeout option

The setting takes effect immediately without restarting the server.

## Related content

- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Rowset Properties and Behaviors (Native Client OLE DB provider)](../../relational-databases/native-client-ole-db-rowsets/rowset-properties-and-behaviors.md)
- [Server configuration options (SQL Server)](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
