---
title: "Server configuration: cost threshold for parallelism"
description: Learn about the cost threshold for parallelism option. See how its value affects whether SQL Server runs parallel plans for queries, and find out how to set it.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/25/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "cost threshold for parallelism option"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Server configuration: cost threshold for parallelism

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to configure the `cost threshold for parallelism` server configuration option in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

## About cost threshold for parallelism

The `cost threshold for parallelism` option specifies the threshold at which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] considers parallel plans on computers with more than one [logical processor](../../sql-server/compute-capacity-limits-by-edition-of-sql-server.md#remarks). The `cost threshold for parallelism` option can be set to any value from 0 through 32,767.

Cost is the sum of *estimated* operator costs in a query plan (for example, CPU and I/O). It's a relative measure used only for plan selection; it doesn't measure actual runtime.

Certain Transact-SQL components can inhibit a parallel plan. For example, noninlineable scalar user-defined functions (UDFs), table variable modifications, and certain system calls. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] evaluates parallel alternatives only when the best serial plan's estimated cost exceeds the `cost threshold for parallelism` value, and might then choose a cheaper parallel plan.

## Limitations

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ignores the `cost threshold for parallelism` value under the following conditions:

- Your computer has only one logical processor.

- Only a single logical processor is available to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] because of the `affinity mask` configuration option.

- The `max degree of parallelism` server configuration option is set to `1`.

## Recommendations

This option is an advanced option, and should be changed only by an experienced database professional.

The default value of `5` is a starting point, not a recommendation. On modern [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] systems, raising it can help to keep smaller OLTP queries executing with serial plans. Use small increments and observe a full business cycle before further changes. Perform application testing with higher and lower values if needed to optimize application performance.

> [!NOTE]  
> In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], you can't set `cost threshold for parallelism`. Use `MAXDOP` to control parallelism instead. For more information, see [Changing default MAXDOP in Azure SQL Database and Azure SQL Managed Instance](https://techcommunity.microsoft.com/blog/azuresqlblog/changing-default-maxdop-in-azure-sql-database-and-azure-sql-managed-instance/1538528).

In certain cases, a parallel plan might be chosen even though the query's plan cost is less than the current `cost threshold for parallelism` value. The decision to use a parallel or serial plan is based on a cost estimate provided earlier in the optimization process. For more information, see the [Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md#parallel-query-processing).

## Remarks

To see if the `cost threshold for parallelism` server configuration option is set too high or too low for your workload, look for the following indicators.

| Cost&nbsp;threshold&nbsp;for parallelism&nbsp;setting | Description |
| --- | --- |
| **Too low** | - Too many CPU-light queries go parallel.<br /><br /> - In [Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md), many plans have `is_parallel_plan` set to `1`.<br /><br /> - `CXPACKET` and `CXCONSUMER` dominate wait type statistics. You might also see `THREADPOOL` and `SOS_SCHEDULER_YIELD` waits. |
| **Too high** | - Not enough of the workload's CPU-heavy queries go parallel, and CPU utilization is higher than optimal as a result.<br /><br /> - `SOS_SCHEDULER_YIELD` dominates wait type statistics. |

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, a user must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. In Object Explorer, right-click a server and select **Properties**.

1. Select the **Advanced** node.

1. Under **Parallelism**, change the `cost threshold for parallelism` option to the value you want. Type or select a value from 0 to 32,767.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to set the value of `cost threshold for parallelism` to `20`.

```sql
USE master;
GO

EXECUTE sp_configure 'show advanced options', 1;
GO

RECONFIGURE;
GO

EXECUTE sp_configure 'cost threshold for parallelism', 20;
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

## Follow up: After you configure the cost threshold for parallelism option

The setting takes effect immediately without restarting the server.

## Related content

- [Configure parallel index operations](../../relational-databases/indexes/configure-parallel-index-operations.md)
- [Query hints (Transact-SQL)](../../t-sql/queries/hints-transact-sql-query.md)
- [ALTER WORKLOAD GROUP (Transact-SQL)](../../t-sql/statements/alter-workload-group-transact-sql.md)
- [Server configuration: affinity mask](affinity-mask-server-configuration-option.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sys.sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
