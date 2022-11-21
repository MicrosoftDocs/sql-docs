---
title: "Configure the max degree of parallelism Server Configuration Option"
description: Learn about the max degree of parallelism (MAXDOP) option. See how to use it to limit the number of processors that SQL Server uses in parallel plan execution.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: katsmith
ms.date: 08/23/2022
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
ms.custom:
  - contperf-fy20q4
  - event-tier1-build-2022
helpviewer_keywords:
  - "parallel queries [SQL Server]"
  - "processors [SQL Server], parallel queries"
  - "number of processors for parallel queries"
  - "max degree of parallelism option"
  - "MaxDop"
---

# Configure the max degree of parallelism Server Configuration Option

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This topic describes how to configure the `max degree of parallelism` (MAXDOP) server configuration option in SQL Server by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[Azure Data Studio](../../includes/name-sos-short.md)], or [!INCLUDE[tsql](../../includes/tsql-md.md)]. When an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] runs on a computer that has more than one microprocessor or CPU, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] detects whether parallelism can be used. The degree of parallelism sets the number of processors employed to run a single statement, for each parallel plan execution. You can use the `max degree of parallelism` option to limit the number of processors to use in parallel plan execution. For more detail on the limit set by `max degree of parallelism` (MAXDOP), see the [Considerations](#considerations) section in this page. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] considers parallel execution plans for queries, index data definition language (DDL) operations, parallel inserts, online alter column, parallel stats collection, and static and keyset-driven cursor population.

> [!NOTE]
> [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] introduced automatic recommendations for setting the MAXDOP server configuration option based on the number of processors available during the installation process. The setup user interface allows you to either accept the recommended settings or enter your own value. For more information, see [Database Engine Configuration - MaxDOP page](../../sql-server/install/instance-configuration.md#maxdop).
>
> In Azure SQL Database and Azure SQL Managed Instance, the default MAXDOP setting for each **new** single database, elastic pool database and managed instance is 8. In Azure SQL Database, the **MAXDOP** database-scoped configuration is set to 8. In Azure SQL Managed Instance, the `max degree of parallelism` (MAXDOP) server configuration option is set to 8.
>
> For more on MAXDOP in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], see [Configure the max degree of parallelism (MAXDOP) in Azure SQL Database](/azure/azure-sql/database/configure-max-degree-of-parallelism).

##  <a name="BeforeYouBegin"></a> Before You Begin  

### <a name="considerations"></a> Considerations  

- This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] professional.

- If the affinity mask option isn't set to the default, it may restrict the number of processors available to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on symmetric multiprocessing (SMP) systems.
  
- Setting max degree of parallelism (MAXDOP) to 0 allows [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use all the available processors up to 64 processors. However, this isn't the recommended value for most cases. For more information on the recommended values for max degree of parallelism, see the [Recommendations](#recommendations) section in this page.

- To suppress parallel plan generation, set `max degree of parallelism` to `1`. Set the value to a number from 1 to 32,767 to specify the maximum number of processor cores that can be used during a single query execution. If a value greater than the number of available processors is specified, the actual number of available processors is used. If the computer has only one processor, the `max degree of parallelism` value is ignored.

- The max degree of parallelism limit is set per [task](../../relational-databases/system-dynamic-management-views/sys-dm-os-tasks-transact-sql.md). It isn't a per [request](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md) or per query limit. This means that during a parallel query execution, a single request can spawn multiple tasks up to the MAXDOP limit, and each task will use one worker and one scheduler. For more information, see the *Scheduling parallel tasks* section in the [Thread and Task Architecture Guide](../../relational-databases/thread-and-task-architecture-guide.md).
  
- You can override the max degree of parallelism server configuration value:
    - At the query level, using the **MAXDOP** [query hint](../../t-sql/queries/hints-transact-sql-query.md) or [Query Store hint](../../relational-databases/performance/query-store-hints.md).
    - At the database level, using the **MAXDOP** [database scoped configuration](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).
    - At the workload level, using the **MAX_DOP** [Resource Governor workload group configuration option](../../t-sql/statements/create-workload-group-transact-sql.md).

- Index operations that create or rebuild an index, or that drop a clustered index, can be resource intensive. You can override the max degree of parallelism value for index operations by specifying the MAXDOP index option in the index statement. The MAXDOP value is applied to the statement at execution time and isn't stored in the index metadata. For more information, see [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md).
  
- In addition to queries and index operations, this option also controls the parallelism of DBCC CHECKTABLE, DBCC CHECKDB, and DBCC CHECKFILEGROUP. You can disable parallel execution plans for these statements by using trace flag 2528. For more information, see [Trace Flags &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).

<a name="recommendations"></a>

<a name="Guidelines"></a>

### Recommendations

Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], during service startup if the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] detects more than eight physical cores per NUMA node or socket at startup, soft-NUMA nodes are created automatically by default. The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] places logical processors from the same physical core into different soft-NUMA nodes. The recommendations in the table below are aimed at keeping all the worker threads of a parallel query within the same soft-NUMA node. This will improve the performance of the queries and distribution of worker threads across the NUMA nodes for the workload. For more information, see [Soft-NUMA](../../database-engine/configure-windows/soft-numa-sql-server.md).

Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], use the following guidelines when you configure the `max degree of parallelism` server configuration value:

|Server configuration|Number of processors|Guidance|
|----------------|-----------------|-----------------|
|Server with single NUMA node|Less than or equal to eight logical processors|Keep MAXDOP at or below # of logical processors|
|Server with single NUMA node|Greater than eight logical processors|Keep MAXDOP at 8|
|Server with multiple NUMA nodes|Less than or equal to 16 logical processors per NUMA node|Keep MAXDOP at or below # of logical processors per NUMA node|
|Server with multiple NUMA nodes|Greater than 16 logical processors per NUMA node|Keep MAXDOP at half the number of logical processors per NUMA node with a MAX value of 16|
  
> [!NOTE]
> NUMA node in the above table refers to soft-NUMA nodes automatically created by [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and higher versions, or hardware-based NUMA nodes if soft-NUMA has been disabled.
> Use these same guidelines when you set the max degree of parallelism option for Resource Governor workload groups. For more information, see [CREATE WORKLOAD GROUP (Transact-SQL)](../../t-sql/statements/create-workload-group-transact-sql.md).
  
From [!INCLUDE[ssKatmai](../../includes/ssKatmai-md.md)] through [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], use the following guidelines when you configure the `max degree of parallelism` server configuration value:

|Server configuration|Number of processors|Guidance|
|----------------|-----------------|-----------------|
|Server with single NUMA node|Less than or equal to eight logical processors|Keep MAXDOP at or below # of logical processors|
|Server with single NUMA node|Greater than eight logical processors|Keep MAXDOP at 8|
|Server with multiple NUMA nodes|Less than or equal to eight logical processors per NUMA node|Keep MAXDOP at or below # of logical processors per NUMA node|
|Server with multiple NUMA nodes|Greater than eight logical processors per NUMA node|Keep MAXDOP at 8|
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the RECONFIGURE statement, a user must be granted the ALTER SETTINGS server-level permission. The ALTER SETTINGS permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.
  
<a id="use-sql-server-management-studio"></a>

## <a name="SSMSProcedure"></a> Use SQL Server Management Studio or Azure Data Studio
  
In [!INCLUDE[Azure Data Studio](../../includes/name-sos-short.md)], install the `Database Admin Tool Extensions for Windows` extension, or use the below T-SQL method.

#### To configure the max degree of parallelism option

These options change the MAXDOP for the instance.

1. In **Object Explorer**, right-click the desired instance and select **Properties**.
  
2. Select the **Advanced** node.
  
3. In the **Max Degree of Parallelism** box, select the maximum number of processors to use in parallel plan execution.
  
## <a name="TsqlProcedure"></a> Use Transact-SQL  
  
#### To configure the max degree of parallelism option with T-SQL
  
1. Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] with [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[Azure Data Studio](../../includes/name-sos-short.md)].
  
2. From the Standard bar, select **New Query**.
  
3. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to configure the `max degree of parallelism` option to `16`.
  
```sql  
USE AdventureWorks2012 ;  
GO   
EXEC sp_configure 'show advanced options', 1;  
GO  
RECONFIGURE WITH OVERRIDE;  
GO  
EXEC sp_configure 'max degree of parallelism', 16;  
GO  
RECONFIGURE WITH OVERRIDE;  
GO  
```  
  
 For more information, see [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md).

##  <a name="FollowUp"></a> Follow Up: After you configure the max degree of parallelism option  

The setting takes effect immediately without restarting the server.



## See also  

- [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing.md)  
- [Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md)
- [Trace Flags](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)  
- [Query Store hints](../../relational-databases/performance/query-store-hints.md)
- [Query Hints (Transact-SQL)](../../t-sql/queries/hints-transact-sql-query.md)
- [Hints (Transact-SQL) - Query](../../t-sql/queries/hints-transact-sql-query.md)
- [USE HINT query hint](../../t-sql/queries/hints-transact-sql-query.md#use_hint)
- [ALTER DATABASE SCOPED CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)
- [affinity mask Server Configuration Option](../../database-engine/configure-windows/affinity-mask-server-configuration-option.md)
- [Server Configuration Options (SQL Server)](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
- [Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md#DOP)
- [Thread and Task Architecture Guide](../../relational-databases/thread-and-task-architecture-guide.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [Set Index Options](../../relational-databases/indexes/set-index-options.md)

## Next steps

- [Degree of Parallelism Feedback](../../relational-databases/performance/intelligent-query-processing-feedback.md#degree-of-parallelism-dop-feedback)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Monitor and Tune for Performance](../../relational-databases/performance/monitor-and-tune-for-performance.md)
- [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md)