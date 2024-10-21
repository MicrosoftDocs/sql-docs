---
title: "Server configuration: query wait"
description: Learn about the query wait option. See how to use it to specify the number of seconds a SQL Server query waits for resources before it times out.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "queries [SQL Server], timing out"
  - "time [SQL Server], query wait time"
  - "query wait option [SQL Server]"
---
# Server configuration: query wait

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to configure the **query wait** server configuration option in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. Memory-intensive queries (such as those involving sorting and hashing) are queued when there isn't enough memory available to run the query. The **query wait** option specifies the time, in seconds (from 0 through 2,147,483,647), that a query waits for resources before it times out. The default value for this option is -1. This means the time-out is calculated as 25 times the estimated query cost.

> [!IMPORTANT]  
> A transaction that contains the waiting query might hold locks while the query waits for memory. In rare situations, it's possible for an undetectable deadlock to occur. Decreasing the query wait time lowers the probability of such deadlocks. Eventually, a waiting query will be terminated and the transaction locks released. However, increasing the maximum wait time might increase the amount of time for the query to be terminated. Changes to this option aren't recommended.

## Recommendations

This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] professional.

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, a user must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. In Object Explorer, right-click a server and select **Properties**.

1. Select the **Advanced** node.

1. Under **Parallelism**, type the desired value for the **query wait** option.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to set the value of the `query wait` option to `7500` seconds.

   ```sql
   USE master;
   GO

   EXECUTE sp_configure 'show advanced options', 1;
   GO

   RECONFIGURE;
   GO

   EXECUTE sp_configure 'query wait', 7500;
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

## Follow up: After you configure the query wait option

The setting takes effect immediately without restarting the server.

## Related content

- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
