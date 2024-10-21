---
title: "Server configuration: min memory per query"
description: Learn how to use the min memory per query option to specify the minimum number of kilobytes that SQL Server allocates for a query.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "memory [SQL Server], queries"
  - "minimum query memory"
  - "queries [SQL Server], memory"
  - "min memory per query option"
  - "min memory grant"
---
# Server configuration: min memory per query

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to configure the `min memory per query` server configuration option in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. The `min memory per query` option specifies the minimum amount of memory (in kilobytes) to allocate for the execution of a query. This is also known as the minimum memory grant. For example, if `min memory per query` is set to 2,048 KB, the query is guaranteed to get at least that much total memory. The default value is 1,024 KB. The minimum value 512 KB, and the maximum is 2,147,483,647 KB (2 GB).

## Limitations

The amount of min memory per query has precedence over the [index create memory](configure-the-index-create-memory-server-configuration-option.md) option. If you modify both options and the index create memory is less than min memory per query, you receive a warning message, but the value is set. During query execution, you receive another similar warning.

<a id="Recommendations"></a>

## Recommendations

This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] professional.

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] query processor tries to determine the optimal amount of memory to allocate to a query. The min memory per query option lets the administrator specify the minimum amount of memory any single query receives. Queries generally receive more memory than this, if they have hash and sort operations on a large volume of data. Increasing the value of min memory per query might improve performance for some small to medium-sized queries, but doing so could lead to increased competition for memory resources. The min memory per query option includes memory allocated for sort operations.

Don't set the min memory per query server configuration option too high, especially on very busy systems, because the query has to wait<sup>1</sup> until it can secure the minimum memory requested, or until the value specified in the query wait server configuration option is exceeded. If more memory is available than the specified minimum value required to execute the query, the query is allowed to make use of the extra memory, if the memory can be used effectively by the query.

<sup>1</sup> In this scenario, the wait type is typically `RESOURCE_SEMAPHORE`. For more information, see [sys.dm_os_wait_stats](../../relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md).

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, a user must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. In Object Explorer, right-click a server and select **Properties**.

1. Select the **Memory** node.

1. In the **Minimum memory per query** box, enter the minimum amount of memory (in kilobytes) to allocate for the execution of a query.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to set the value of the `min memory per query` option to `3500` KB.

   ```sql
   USE master;
   GO

   EXECUTE sp_configure 'show advanced options', 1;
   GO

   RECONFIGURE;
   GO

   EXECUTE sp_configure 'min memory per query', 3500;
   GO

   RECONFIGURE;
   GO

   EXECUTE sp_configure 'show advanced options', 0;
   GO

   RECONFIGURE;
   GO
   ```

<a id="FollowUp"></a>

## Follow up: After you configure the min memory per query option

The setting takes effect immediately without restarting the server.

## Related content

- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [Server configuration: index create memory](configure-the-index-create-memory-server-configuration-option.md)
- [sys.dm_os_wait_stats (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md)
- [sys.dm_exec_query_memory_grants (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-memory-grants-transact-sql.md)
