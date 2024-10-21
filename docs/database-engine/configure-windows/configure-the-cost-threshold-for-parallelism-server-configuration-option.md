---
title: "Server configuration: cost threshold for parallelism"
description: Learn about the cost threshold for parallelism option. See how its value affects whether SQL Server runs parallel plans for queries, and find out how to set it.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "cost threshold for parallelism option"
---
# Server configuration: cost threshold for parallelism

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to configure the `cost threshold for parallelism` server configuration option in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. The `cost threshold for parallelism` option specifies the threshold at which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] creates and runs parallel plans for queries. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] creates and runs a parallel plan for a query only when the estimated cost to run a serial plan for the same query is higher than the value set in `cost threshold for parallelism`. The cost refers to an estimated cost required to run the serial plan on a specific hardware configuration, and isn't a unit of time. The `cost threshold for parallelism` option can be set to any value from 0 through 32767.

## Limitations

The cost refers to an abstracted unit of cost and not a unit of estimated time. Only set `cost threshold for parallelism` on symmetric multiprocessors.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ignores the `cost threshold for parallelism` value under the following conditions:

- Your computer has only one logical processor.

- Only a single logical processor is available to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] because of the `affinity mask` configuration option.

- The `max degree of parallelism` option is set to `1`.

A logical processor is the basic unit of processor hardware that allows the operating system to dispatch a task or execute a thread context. Each logical processor can execute only one thread context at a time. The processor core is the circuitry that decodes and executes instructions. A processor core might contain one or more logical processors. The following [!INCLUDE [tsql](../../includes/tsql-md.md)] query can be used for obtaining CPU information for the system.

```sql
SELECT (cpu_count / hyperthread_ratio) AS PhysicalCPUs,
       cpu_count AS logicalCPUs
FROM sys.dm_os_sys_info;
```

## Recommendations

This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] professional.

In certain cases, a parallel plan might be chosen even though the query's cost plan is less than the current `cost threshold for parallelism` value. This can happen because the decision to use a parallel or serial plan is based on a cost estimate provided earlier in the optimization process. For more information, see the [Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md#parallel-query-processing).

While the default value of `5` is adequate for most systems, a different value might be appropriate. Perform application testing with higher and lower values if needed to optimize application performance.

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, a user must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. In Object Explorer, right-click a server and select **Properties**.

1. Select the **Advanced** node.

1. Under **Parallelism**, change the `cost threshold for parallelism` option to the value you want. Type or select a value from 0 to 32767.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to set the value of the `cost threshold for parallelism` option to `10`.

```sql
USE master;
GO

EXECUTE sp_configure 'show advanced options', 1;
GO

RECONFIGURE;
GO

EXECUTE sp_configure 'cost threshold for parallelism', 10;
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

- [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md)
- [Query hints (Transact-SQL)](../../t-sql/queries/hints-transact-sql-query.md)
- [ALTER WORKLOAD GROUP (Transact-SQL)](../../t-sql/statements/alter-workload-group-transact-sql.md)
- [affinity mask (server configuration option)](affinity-mask-server-configuration-option.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
