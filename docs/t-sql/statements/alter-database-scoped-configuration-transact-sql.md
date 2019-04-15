---
title: "ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: 03/27/2019
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: conceptual
f1_keywords: 
  - "ALTER_DATABASE_SCOPED_CONFIGURATION"
  - "ALTER_DATABASE_SCOPED_CONFIGURATION_TSQL"
  - "DATABASE_SCOPED_CONFIGURATION_TSQL"
  - "SCOPED_CONFIGURATION_TSQL"
  - "SCOPED_TSQL"
  - "ALTER_DATABASE_SCOPED_TSQL"
  - "DATABASE_SCOPED_TSQL"
helpviewer_keywords: 
  - "ALTER DATABASE SCOPED CONFIGURATION statement"
  - "configuration [SQL Server], ALTER DATABASE SCOPED CONFIGURATION statement"
ms.assetid: 63373c2f-9a0b-431b-b9d2-6fa35641571a
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)

[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

This statement enables several database configuration settings at the **individual database** level. This statement is available in [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)] and in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. Those settings are:

- Clear procedure cache.
- Set the MAXDOP parameter to an arbitrary value (1,2, ...) for the primary database based on what works best for that particular database and set a different value (such as 0) for all secondary database used (such as for reporting queries).
- Set the query optimizer cardinality estimation model independent of the database to compatibility level.
- Enable or disable parameter sniffing at the database level.
- Enable or disable query optimization hotfixes at the database level.
- Enable or disable the identity cache at the database level.
- Enable or disable a compiled plan stub to be stored in cache when a batch is compiled for the first time.
- Enable or disable collection of execution statistics for natively compiled T-SQL modules.
- Enable or disable online by default options for DDL statements that support the `ONLINE =` syntax.
- Enable or disable resumable by default options for DDL statements that support the `RESUMABLE =` syntax.
- Enable or disable the auto-drop functionality of global temporary tables.
- Enable or disable [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing.md) features.
- Enable or disable the [lightweight query profiling infrastructure](../../relational-databases/performance/query-profiling-infrastructure.md).
- Enable or disable the new `String or binary data would be truncated` error message.
- Enable or disable collection of last actual execution plan in [sys.dm_exec_query_plan_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-stats-transact-sql.md).

![link icon](../../database-engine/configure-windows/media/topic-link.gif "link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```
ALTER DATABASE SCOPED CONFIGURATION
{
     {  [ FOR SECONDARY] SET <set_options>}
}
| CLEAR PROCEDURE_CACHE  [plan_handle]
| SET < set_options >
[;]

< set_options > ::=
{
    MAXDOP = { <value> | PRIMARY}
    | LEGACY_CARDINALITY_ESTIMATION = { ON | OFF | PRIMARY}
    | PARAMETER_SNIFFING = { ON | OFF | PRIMARY}
    | QUERY_OPTIMIZER_HOTFIXES = { ON | OFF | PRIMARY}
    | IDENTITY_CACHE = { ON | OFF }
    | INTERLEAVED_EXECUTION_TVF = {  ON | OFF }
    | BATCH_MODE_MEMORY_GRANT_FEEDBACK = { ON | OFF  }
    | BATCH_MODE_ADAPTIVE_JOINS = { ON | OFF }
    | TSQL_SCALAR_UDF_INLINING = { ON | OFF }
    | ELEVATE_ONLINE = { OFF | WHEN_SUPPORTED | FAIL_UNSUPPORTED }
    | ELEVATE_RESUMABLE = { OFF | WHEN_SUPPORTED | FAIL_UNSUPPORTED }
    | OPTIMIZE_FOR_AD_HOC_WORKLOADS = { ON | OFF }
    | XTP_PROCEDURE_EXECUTION_STATISTICS = { ON | OFF }
    | XTP_QUERY_EXECUTION_STATISTICS = { ON | OFF }
    | ROW_MODE_MEMORY_GRANT_FEEDBACK = { ON | OFF }
    | BATCH_MODE_ON_ROWSTORE = { ON | OFF }
    | DEFERRED_COMPILATION_TV = { ON | OFF }
    | GLOBAL_TEMPORARY_TABLE_AUTODROP = { ON | OFF }
    | LIGHTWEIGHT_QUERY_PROFILING = { ON | OFF }
    | VERBOSE_TRUNCATION_WARNINGS = { ON | OFF }
    | LAST_QUERY_PLAN_STATS = { ON | OFF }
}
```

## Arguments

FOR SECONDARY

Specifies the settings for secondary databases (all secondary databases must have the identical values).

CLEAR PROCEDURE_CACHE [plan_handle]

Clears the procedure (plan) cache for the database, and can be executed both on the primary and the secondaries.  

Specify a query plan handle to clear a single query plan from the plan cache.

> [!NOTE]
> Specifying a query plan handle is available in Azure SQL Database and SQL Server 2019 or higher.

MAXDOP **=** {\<value> | PRIMARY }
**\<value>**

Specifies the default MAXDOP setting that should be used for statements. 0 is the default value and indicates that the server configuration will be used instead. The MAXDOP at the database scope overrides (unless it is set to 0) the **max degree of parallelism** set at the server level by sp_configure. Query hints can still override the DB scoped MAXDOP in order to tune specific queries that need different setting. All these settings are limited by the MAXDOP set for the Workload Group.

You can use the max degree of parallelism option to limit the number of processors to use in parallel plan execution. SQL Server considers parallel execution plans for queries, index data definition language (DDL) operations, parallel insert, online alter column, parallel stats collection, and static and keyset-driven cursor population.

To set this option at the instance level, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).

> [!TIP]
> To accomplish this at the query level, add the **MAXDOP** [query hint](../../t-sql/queries/hints-transact-sql-query.md).

PRIMARY

Can only be set for the secondaries, while the database in on the primary, and indicates that the configuration will be the one set for the primary. If the configuration for the primary changes, the value on the secondaries will change accordingly without the need to set the secondaries value explicitly. **PRIMARY** is the default setting for the secondaries.

LEGACY_CARDINALITY_ESTIMATION **=** { ON | **OFF** | PRIMARY }

Enables you to set the query optimizer cardinality estimation model to the SQL Server 2012 and earlier version independent of the compatibility level of the database. The default is **OFF**, which sets the query optimizer cardinality estimation model based on the compatibility level of the database. Setting LEGACY_CARDINALITY_ESTIMATION to **ON** is equivalent to enabling [Trace Flag 9481](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).

> [!TIP]
> To accomplish this at the query level, add the **QUERYTRACEON** [query hint](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).
> Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1, to accomplish this at the query level, add the **USE HINT** [query hint](../../t-sql/queries/hints-transact-sql-query.md#use_hint) instead of using the trace flag.

PRIMARY

This value is only valid on secondaries while the database in on the primary, and specifies that the query optimizer cardinality estimation model setting on all secondaries will be the value set for the primary. If the configuration on the primary for the query optimizer cardinality estimation model changes, the value on the secondaries will change accordingly. **PRIMARY** is the default setting for the secondaries.

PARAMETER_SNIFFING **=** { **ON** | OFF | PRIMARY}

Enables or disables [parameter sniffing](../../relational-databases/query-processing-architecture-guide.md#ParamSniffing). The default is ON. Setting PARAMETER_SNIFFING to OFF is equivalent to enabling [Trace Flag 4136](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).

> [!TIP]
> To accomplish this at the query level, see the **OPTIMIZE FOR UNKNOWN** [query hint](../../t-sql/queries/hints-transact-sql-query.md).
> Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1, to accomplish this at the query level, the **USE HINT** [query hint](../../t-sql/queries/hints-transact-sql-query.md#use_hint) is also available.

PRIMARY

This value is only valid on secondaries while the database in on the primary, and specifies that the value for this setting on all secondaries will be the value set for the primary. If the configuration on the primary for using [parameter sniffing](../../relational-databases/query-processing-architecture-guide.md#ParamSniffing) changes, the value on the secondaries will change accordingly without the need to set the secondaries value explicitly. PRIMARY is the default setting for the secondaries.

<a name="qo_hotfixes"></a> QUERY_OPTIMIZER_HOTFIXES **=** { ON | **OFF** | PRIMARY }

Enables or disables query optimization hotfixes regardless of the compatibility level of the database. The default is **OFF**, which disables query optimization hotfixes that were released after the highest available compatibility level was introduced for a specific version (post-RTM). Setting this to **ON** is equivalent to enabling [Trace Flag 4199](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).

> [!TIP]
> To accomplish this at the query level, add the **QUERYTRACEON** [query hint](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).
> Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1, to accomplish this at the query level, add the USE HINT [query hint](../../t-sql/queries/hints-transact-sql-query.md#use_hint) instead of using the trace flag.

PRIMARY

This value is only valid on secondaries while the database in on the primary, and specifies that the value for this setting on all secondaries is the value set for the primary. If the configuration for the primary changes, the value on the secondaries changes accordingly without the need to set the secondaries value explicitly. PRIMARY is the default setting for the secondaries.

IDENTITY_CACHE **=** { **ON** | OFF }

**Applies to**: [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

Enables or disables identity cache at the database level. The default is **ON**. Identity caching is used to improve INSERT performance on tables with identity columns. To avoid gaps in the values of an identity column in cases where the server restarts unexpectedly or fails over to a secondary server, disable the IDENTITY_CACHE option. This option is similar to the existing [Trace Flag 272](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md), except that it can be set at the database level rather than only at the server level.

> [!NOTE]
> This option can only be set for the PRIMARY. For more information, see [identity columns](create-table-transact-sql-identity-property.md).

INTERLEAVED_EXECUTION_TVF **=** { **ON** | OFF }

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

Allows you to enable or disable Interleaved execution for multi-statement table valued functions at the database or statement scope while still maintaining database compatibility level 140 and higher. Interleaved execution is a feature that is part of Adaptive query processing in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. For more information, please refer to [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing.md).

> [!NOTE]
> For database compatibility level 130 or lower, this database scoped configuration has no effect.

BATCH_MODE_MEMORY_GRANT_FEEDBACK **=** { **ON** | OFF}

**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssNoVersion](../../includes/sssqlv15-md.md)] 

Allows you to enable or disable batch mode memory grant feedback at the database scope while still maintaining database compatibility level 140 and higher. Batch mode memory grant feedback a feature that is part of [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing.md) introduced in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)].

> [!NOTE]
> For database compatibility level 130 or lower, this database scoped configuration has no effect.

BATCH_MODE_ADAPTIVE_JOINS **=** { **ON** | OFF}

**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssNoVersion](../../includes/sssqlv15-md.md)] 

Allows you to enable or disable batch mode adaptive joins at the database scope while still maintaining database compatibility level 140 and higher. Batch mode adaptive joins is a feature that is part of [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing.md) introduced in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)].

> [!NOTE]
> For database compatibility level 130 or lower, this database scoped configuration has no effect.

TSQL_SCALAR_UDF_INLINING **=** { **ON** | OFF }

**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] (feature is in public preview)

Allows you to enable or disable T-SQL Scalar UDF inlining at the database scope while still maintaining database compatibility level 150 and higher. T-SQL Scalar UDF inlining is part of the [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing.md) feature family.

> [!NOTE]
> For database compatibility level 140 or lower, this database scoped configuration has no effect.

ELEVATE_ONLINE = { OFF | WHEN_SUPPORTED | FAIL_UNSUPPORTED }

**Applies to**: [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)] (feature is in public preview)

Allows you to select options to cause the engine to automatically elevate supported operations to online. The default is OFF, which means operations will not be elevated to online unless specified in the statement. [sys.database_scoped_configurations](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md) reflects the current value of ELEVATE_ONLINE. These options will only apply to operations that are supported for online.

FAIL_UNSUPPORTED

This value elevates all supported DDL operations to ONLINE. Operations that do not support online execution will fail and throw a warning.

WHEN_SUPPORTED

This value elevates operations that support ONLINE. Operations that do not support online will be run offline.

> [!NOTE]
> You can override the default setting by submitting a statement with the ONLINE option specified.

ELEVATE_RESUMABLE= { OFF | WHEN_SUPPORTED | FAIL_UNSUPPORTED }

**Applies to**: [!INCLUDE[ssSDS](../../includes/sssds-md.md)] and [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] (feature is in public preview)

Allows you to select options to cause the engine to automatically elevate supported operations to resumable. The default is OFF, which means operations are not be elevated to resumable unless specified in the statement. [sys.database_scoped_configurations](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md) reflects the current value of ELEVATE_RESUMABLE. These options only apply to operations that are supported for resumable.

FAIL_UNSUPPORTED

This value elevates all supported DDL operations to RESUMABLE. Operations that do not support resumable execution fail and throw a warning.

WHEN_SUPPORTED

This value elevates operations that support RESUMABLE. Operations that do not support resumable are run non-resumably.

> [!NOTE]
> You can override the default setting by submitting a statement with the RESUMABLE option specified.

OPTIMIZE_FOR_AD_HOC_WORKLOADS **=** { ON | **OFF** }

**Applies to**: [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)]

Enables or disables a compiled plan stub to be stored in cache when a batch is compiled for the first time. The default is OFF. Once the database scoped configuration OPTIMIZE_FOR_AD_HOC_WORKLOADS is enabled for a database, a compiled plan stub will be stored in cache when a batch is compiled for the first time. Plan stubs have a smaller memory footprint compared to the size of the full compiled plan. If a batch is compiled or executed again, the compiled plan stub will be removed and replaced with a full compiled plan.

XTP_PROCEDURE_EXECUTION_STATISTICS **=** { ON | **OFF** }

**Applies to**: [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)]

Enables or disables collection of execution statistics at the module-level for natively compiled T-SQL modules in the current database. The default is OFF. The execution statistics are reflected in [sys.dm_exec_procedure_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-procedure-stats-transact-sql.md).

Module-level execution statistics for natively compiled T-SQL modules are collected if either this option is ON, or if statistics collection is enabled through [sp_xtp_control_proc_exec_stats](../../relational-databases/system-stored-procedures/sys-sp-xtp-control-proc-exec-stats-transact-sql.md).

XTP_QUERY_EXECUTION_STATISTICS **=** { ON | **OFF** }

**Applies to**: [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)]

Enables or disables collection of execution statistics at the statement-level for natively compiled T-SQL modules in the current database. The default is OFF. The execution statistics are reflected in [sys.dm_exec_query_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md) and in [Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md).

Statement-level execution statistics for natively compiled T-SQL modules are collected if either this option is ON, or if statistics collection is enabled through [sp_xtp_control_query_exec_stats](../../relational-databases/system-stored-procedures/sys-sp-xtp-control-query-exec-stats-transact-sql.md).

For more information about performance monitoring of natively compiled [!INCLUDE[tsql](../../includes/tsql-md.md)] modules see [Monitoring Performance of Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/monitoring-performance-of-natively-compiled-stored-procedures.md).

ROW_MODE_MEMORY_GRANT_FEEDBACK **=** { **ON** | OFF}

**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] (feature is in public preview)

Allows you to enable or disable row mode memory grant feedback at the database scope while still maintaining database compatibility level 150 and higher. Row mode memory grant feedback a feature that is part of [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing.md) introduced in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] (row mode is supported in [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]).

> [!NOTE]
> For database compatibility level 140 or lower, this database scoped configuration has no effect.

BATCH_MODE_ON_ROWSTORE **=** { **ON** | OFF}

**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] (feature is in public preview)

Allows you to enable or disable batch mode on rowstore at the database scope while still maintaining database compatibility level 150 and higher. Batch mode on rowstore is a feature that is part of [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing.md) feature family.

> [!NOTE]
> For database compatibility level 140 or lower, this database scoped configuration has no effect.

DEFERRED_COMPILATION_TV **=** { **ON** | OFF}

**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] (feature is in public preview)

Allows you to enable or disable table variable deferred compilation at the database scope while still maintaining database compatibility level 150 and higher. Table variable deferred compilation is a feature that is part of [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing.md) feature family.

> [!NOTE]
> For database compatibility level 140 or lower, this database scoped configuration has no effect.

GLOBAL_TEMPORARY_TABLE_AUTODROP **=** { **ON** | OFF }

**Applies to**: [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)] (feature is in public preview)

Allows setting the auto drop functionality for [global temporary tables](create-table-transact-sql.md). The default is ON, which means that the global temporary tables are automatically dropped when not in use by any session. When set to OFF, global temporary tables need to be explicitly dropped using a DROP TABLE statement or will be automatically dropped on server restart.

- With [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] single databases and elastic pools, this option can be set in the individual user databases of the SQL Database server.
- In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] managed instance, this option is set in `TempDB` and the setting of the individual user databases has no effect.

LIGHTWEIGHT_QUERY_PROFILING **=** { **ON** | OFF}

**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] 

Allows you to enable or disable the [lightweight query profiling infrastructure](../../relational-databases/performance/query-profiling-infrastructure.md). The lightweight query profiling infrastructure (LWP) provides query performance data more efficiently than standard profiling mechanisms and is enabled by default.

<a name="verbose-truncation"></a>

VERBOSE_TRUNCATION_WARNINGS **=** { **ON** | OFF}

**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] 

Allows you to enable or disable the new `String or binary data would be truncated` error message. [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] introduces a new, more specific error message (2628) for this scenario:  

`String or binary data would be truncated in table '%.*ls', column '%.*ls'. Truncated value: '%.*ls'.`

When set to ON under database compatibility level 150, truncation errors raise the new error message 2628 to provide more context and simplify the troubleshooting process.

When set to OFF under database compatibility level 150, truncation errors raise the previous error message 8152.

For database compatibility level 140 or lower, error message 2628 remains an opt-in error message that requires [trace flag](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) 460 to be enabled, and this database scoped configuration has no effect.

LAST_QUERY_PLAN_STATS **=** { ON | **OFF**}

**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] 

Allows you to enable or disable colection of the last query plan statistics (equivalent to an actual execution plan) in [sys.dm_exec_query_plan_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-stats-transact-sql.md).

## <a name="Permissions"></a> Permissions

Requires `ALTER ANY DATABASE SCOPE CONFIGURATION` on the database. This permission can be granted by a user with CONTROL permission on a database.

## General Remarks
While you can configure secondary databases to have different scoped configuration settings from their primary, all secondary databases use the same configuration. Different settings cannot be configured for individual secondaries.

Executing this statement clears the procedure cache in the current database, which means that all queries have to recompile.

For 3-part name queries, the settings for the current database connection for the query are honored, other than for SQL modules (such as procedures, functions, and triggers) that are compiled in the current database context and therefore uses the options of the database in which they reside.

The `ALTER_DATABASE_SCOPED_CONFIGURATION` event is added as a DDL event that can be used to fire a DDL trigger, and is a child of the `ALTER_DATABASE_EVENTS` trigger group.

Database scoped configuration settings will be carried over with the database, which means that when a given database is restored or attached, the existing configuration settings remain.

## Limitations and Restrictions

### MAXDOP
The granular settings can override the global ones and that resource governor can cap all other MAXDOP settings. The logic for MAXDOP setting is the following:

- Query hint overrides both the `sp_configure` and the database scoped configuration. If the resource group MAXDOP is set for the workload group:

  - If the query hint is set to zero (0), it is overridden by the resource governor setting.

  - If the query hint is not zero (0), it is capped by the resource governor setting.

- The database scoped configuration (unless it's zero) overrides the `sp_configure` setting unless there is a query hint and is capped by the resource governor setting.

- The `sp_configure` setting is overridden by the resource governor setting.

### QUERY_OPTIMIZER_HOTFIXES

When `QUERYTRACEON` hint is used to enable the default query optimizer of SQL Server 7.0 through [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] versions or query optimizer hotfixes, it would be an OR condition between the query hint and the database scoped configuration setting, meaning if either is enabled, the database scoped configurations apply.

### Geo DR

Readable secondary databases (Always On Availability Groups and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] geo-replicated databases), use the secondary value by checking the state of the database. Even though recompile does not occur on failover and technically the new primary has queries that are using the secondary settings, the idea is that the setting between primary and secondary only vary when the workload is different and therefore the cached queries are using the optimal settings, whereas new queries pick the new settings that are appropriate for them.

### DacFx

Since `ALTER DATABASE SCOPED CONFIGURATION` is a new feature in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]) that affects the database schema, exports of the schema (with or without data) are not able to be imported into an older version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], such as [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]. For example, an export to a [DACPAC](https://msdn.microsoft.com/library/ee210546.aspx#Anchor_3) or a [BACPAC](https://msdn.microsoft.com/library/ee210546.aspx#Anchor_4) from an [!INCLUDE[ssSDS](../../includes/sssds-md.md)] or [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] database that used this new feature would not be able to be imported into a down-level server.

### ELEVATE_ONLINE

This option only applies to DDL statements that support the `WITH (ONLINE = <syntax>)`. XML indexes are not affected.

### ELEVATE_RESUMABLE

This option only applies to DDL statements that support the `WITH (RESUMABLE = <syntax>)`. XML indexes are not affected.

## Metadata

The [sys.database_scoped_configurations &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md) system view provides information about scoped configurations within a database. Database-scoped configuration options only show up in sys.database_scoped_configurations as they are overrides to server-wide default settings. The [sys.configurations &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md) system view only shows server-wide settings.

## Examples
These examples demonstrate the use of ALTER DATABASE SCOPED CONFIGURATION

### A. Grant Permission
This example grant permission required to execute ALTER DATABASE SCOPED CONFIGURATION to user Joe.

```sql
GRANT ALTER ANY DATABASE SCOPED CONFIGURATION to [Joe] ;
```

### B. Set MAXDOP
This example sets MAXDOP = 1 for a primary database and MAXDOP = 4 for a secondary database in a geo-replication scenario.

```sql
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 1 ;
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = 4 ;
```

This example sets MAXDOP for a secondary database to be the same as it is set for its primary database in a geo-replication scenario.

```sql
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY ;
```

### C. Set LEGACY_CARDINALITY_ESTIMATION
This example sets LEGACY_CARDINALITY_ESTIMATION to ON for a secondary database in a geo-replication scenario.

```sql
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = ON ;
```

This example sets LEGACY_CARDINALITY_ESTIMATION for a secondary database as it is for its primary database in a geo-replication scenario.

```sql
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY ;
```

### D. Set PARAMETER_SNIFFING
This example sets PARAMETER_SNIFFING to OFF for a primary database in a geo-replication scenario.

```sql
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = OFF ;
```

This example sets PARAMETER_SNIFFING to OFF for a secondary database in a geo-replication scenario.

```sql
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = OFF ;
```

This example sets PARAMETER_SNIFFING for secondary database as it is on primary database in a geo-replication scenario.

```sql
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY ;
```

### E. Set QUERY_OPTIMIZER_HOTFIXES
Set QUERY_OPTIMIZER_HOTFIXES to ON for a primary database in a geo-replication scenario.

```sql
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = ON ;
```

### F. Clear Procedure Cache
This example clears the procedure cache (possible only for a primary database).

```sql
ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE;
```

### G. Set IDENTITY_CACHE
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] (feature is in public preview)

This example disables the identity cache.

```sql
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = OFF ;
```

### H. Set OPTIMIZE_FOR_AD_HOC_WORKLOADS
**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] 

This example enables a compiled plan stub to be stored in cache when a batch is compiled for the first time.

```sql
ALTER DATABASE SCOPED CONFIGURATION SET OPTIMIZE_FOR_AD_HOC_WORKLOADS = ON;
```

### I. Set ELEVATE_ONLINE
**Applies to**: [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)] (feature is in public preview)

This example sets ELEVATE_ONLINE to FAIL_UNSUPPORTED.

```sql
ALTER DATABASE SCOPED CONFIGURATION SET ELEVATE_ONLINE = FAIL_UNSUPPORTED ;
```

### J. Set ELEVATE_RESUMABLE
**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssNoVersion](../../includes/sssqlv15-md.md)] (feature is in public preview)

This example sets ELEVATE_RESUMABLE to WHEN_SUPPORTED.

```sql
ALTER DATABASE SCOPED CONFIGURATION SET ELEVATE_RESUMABLE = WHEN_SUPPORTED ;
```

### K. Clear a query plan from the plan cache
This example clears a specific plan from the procedure cache 

```sql
ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE 0x06000500F443610F003B7CD12C02000001000000000000000000000000000000000000000000000000000000;
```

## Additional Resources

### MAXDOP Resources

- [Degree of Parallelism](../../relational-databases/query-processing-architecture-guide.md#DOP)
- [Recommendations and guidelines for the "max degree of parallelism" configuration option in SQL Server](https://support.microsoft.com/kb/2806535)

### LEGACY_CARDINALITY_ESTIMATION Resources

- [Cardinality Estimation (SQL Server)](../../relational-databases/performance/cardinality-estimation-sql-server.md)
- [Optimizing Your Query Plans with the SQL Server 2014 Cardinality Estimator](https://msdn.microsoft.com/library/dn673537.aspx)

### PARAMETER_SNIFFING Resources

- [Parameter Sniffing](../../relational-databases/query-processing-architecture-guide.md#ParamSniffing)
- ["I smell a parameter!"](https://blogs.msdn.microsoft.com/queryoptteam/2006/03/31/i-smell-a-parameter/)

### QUERY_OPTIMIZER_HOTFIXES Resources

- [Trace Flags](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)
- [SQL Server query optimizer hotfix trace flag 4199 servicing model](https://support.microsoft.com/kb/974006)

### ELEVATE_ONLINE Resources

[Guidelines for Online Index Operations](../../relational-databases/indexes/guidelines-for-online-index-operations.md)

### ELEVATE_RESUMABLE Resources

[Guidelines for Online Index Operations](../../relational-databases/indexes/guidelines-for-online-index-operations.md)

## More information

- [sys.database_scoped_configurations](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md)
- [sys.configurations](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md)
- [Databases and Files Catalog Views](../../relational-databases/system-catalog-views/databases-and-files-catalog-views-transact-sql.md)
- [Server Configuration Options](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
- [How Online Index Operations Work](../../relational-databases/indexes/how-online-index-operations-work.md)
- [Perform Index Operations Online](../../relational-databases/indexes/perform-index-operations-online.md)
- [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md)
- [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)
