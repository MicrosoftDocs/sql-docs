---
title: "ALTER DATABASE SCOPED CONFIGURATION"
titleSuffix: SQL Server (Transact-SQL)
description: Enable several database configuration settings at the individual database level.
author: markingmyname
ms.author: maghan
ms.reviewer: katsmith, jovanpop, wiassaf
ms.date: 11/01/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - "seo-lt-2019"
  - "event-tier1-build-2022"
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
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017||=azure-sqldw-latest"
---

# ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)

[!INCLUDE[sqlserver2016-asdb-asdbmi-asa.md](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

This command enables several database configuration settings at the **individual database** level.

> [!IMPORTANT]  
> Different `DATABASE SCOPED CONFIGURATION` options are supported in different versions of SQL Server or Azure services. This page describes **all** `DATABASE SCOPED CONFIGURATION` options. Versions where applicable are described in the text below. Make sure that you use the syntax that is available in the version of service that you are using.

The following settings are supported in [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)], [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)] and in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as indicated by the **Applies to** line for each setting in the [Arguments](#arguments) section:

- Clear procedure cache.
- Set the MAXDOP parameter to a recommended value (1,2, ...) for the primary database based on what works best for that particular workload, and set a different value for secondary replica databases used by reporting queries. For guidance on choosing a MAXDOP, review [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).
- Set the query optimizer cardinality estimation model independent of the database to compatibility level.
- Enable or disable parameter sniffing at the database level.
- Enable or disable query optimization hotfixes at the database level.
- Enable or disable the identity cache at the database level.
- Enable or disable a compiled plan stub to be stored in cache when a batch is compiled for the first time.
- Enable or disable collection of execution statistics for natively compiled [!INCLUDE[tsql](../../includes/tsql-md.md)] modules.
- Enable or disable online by default options for DDL statements that support the `ONLINE =` syntax.
- Enable or disable resumable by default options for DDL statements that support the `RESUMABLE =` syntax.
- Enable or disable [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing.md) features.
- Enable or disable accelerated plan forcing.
- Enable or disable the auto-drop functionality of global temporary tables.
- Enable or disable the [lightweight query profiling infrastructure](../../relational-databases/performance/query-profiling-infrastructure.md).
- Enable or disable the new `String or binary data would be truncated` error message.
- Enable or disable collection of last actual execution plan in [sys.dm_exec_query_plan_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-stats-transact-sql.md).
- Specify the number of minutes that a paused resumable index operation is paused before it is automatically aborted by the [!INCLUDE[ssde_md](../../includes/ssde_md.md)].
- Enable or disable waiting for locks at low priority for asynchronous statistics update.
- Enable or disable uploading ledger digests to Azure Blob Storage.

This setting is only available in [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)].

- Set the compatibility level of a user database

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
-- Syntax for SQL Server, Azure SQL Database and Azure SQL Managed Instance

ALTER DATABASE SCOPED CONFIGURATION
{
    { [ FOR SECONDARY] SET <set_options>}
}
| CLEAR PROCEDURE_CACHE [plan_handle]
| SET < set_options >
[;]

< set_options > ::=
{
    MAXDOP = { <value> | PRIMARY}
    | LEGACY_CARDINALITY_ESTIMATION = { ON | OFF | PRIMARY}
    | PARAMETER_SNIFFING = { ON | OFF | PRIMARY}
    | QUERY_OPTIMIZER_HOTFIXES = { ON | OFF | PRIMARY}
    | IDENTITY_CACHE = { ON | OFF }
    | INTERLEAVED_EXECUTION_TVF = { ON | OFF }
    | BATCH_MODE_MEMORY_GRANT_FEEDBACK = { ON | OFF }
    | BATCH_MODE_ADAPTIVE_JOINS = { ON | OFF }
    | TSQL_SCALAR_UDF_INLINING = { ON | OFF }
    | ELEVATE_ONLINE = { OFF | WHEN_SUPPORTED | FAIL_UNSUPPORTED }
    | ELEVATE_RESUMABLE = { OFF | WHEN_SUPPORTED | FAIL_UNSUPPORTED }
    | OPTIMIZE_FOR_AD_HOC_WORKLOADS = { ON | OFF }
    | XTP_PROCEDURE_EXECUTION_STATISTICS = { ON | OFF }
    | XTP_QUERY_EXECUTION_STATISTICS = { ON | OFF }
    | ROW_MODE_MEMORY_GRANT_FEEDBACK = { ON | OFF }
    | MEMORY_GRANT_FEEDBACK_PERCENTILE = { ON | OFF }
    | MEMORY_GRANT_FEEDBACK_PERSISTENCE = { ON | OFF }
    | BATCH_MODE_ON_ROWSTORE = { ON | OFF }
    | DEFERRED_COMPILATION_TV = { ON | OFF }
    | ACCELERATED_PLAN_FORCING = { ON | OFF }
    | GLOBAL_TEMPORARY_TABLE_AUTO_DROP = { ON | OFF }
    | LIGHTWEIGHT_QUERY_PROFILING = { ON | OFF }
    | VERBOSE_TRUNCATION_WARNINGS = { ON | OFF }
    | LAST_QUERY_PLAN_STATS = { ON | OFF }
    | PAUSED_RESUMABLE_INDEX_ABORT_DURATION_MINUTES = <time>
    | ISOLATE_SECURITY_POLICY_CARDINALITY  = { ON | OFF }
    | EXEC_QUERY_STATS_FOR_SCALAR_FUNCTIONS = { ON | OFF }
    | ASYNC_STATS_UPDATE_WAIT_AT_LOW_PRIORITY = { ON | OFF }
    | OPTIMIZED_PLAN_FORCING = { ON | OFF }
    | DOP_FEEDBACK = { ON | OFF }
    | CE_FEEDBACK = { ON | OFF }
    | PARAMETER_SENSITIVE_PLAN_OPTIMIZATION = { ON | OFF }
    | LEDGER_DIGEST_STORAGE_ENDPOINT = { <endpoint URL string> | OFF }
}
```

> [!IMPORTANT]  
> Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)], some option names have changed:
>
> - `DISABLE_INTERLEAVED_EXECUTION_TVF` changed to `INTERLEAVED_EXECUTION_TVF`
> - `DISABLE_BATCH_MODE_MEMORY_GRANT_FEEDBACK` changed to `BATCH_MODE_MEMORY_GRANT_FEEDBACK`
> - `DISABLE_BATCH_MODE_ADAPTIVE_JOINS` changed to `BATCH_MODE_ADAPTIVE_JOINS`

```syntaxsql
-- Syntax for Azure Synapse Analytics

ALTER DATABASE SCOPED CONFIGURATION
{
    SET <set_options>
}
[;]

< set_options > ::=
{
    DW_COMPATIBILITY_LEVEL = { AUTO | 10 | 20 | 9000 }
}
```

## Arguments

#### FOR SECONDARY

Specifies the settings for secondary databases (all secondary databases must have the identical values).

#### CLEAR PROCEDURE_CACHE [plan_handle]

Clears the procedure (plan) cache for the database, and can be executed both on the primary and the secondaries.

Specify a query plan handle to clear a single query plan from the plan cache.

**Applies to:** Specifying a query plan handle is available in starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)].

#### MAXDOP = {\<value> | PRIMARY }

**\<value>**

Specifies the default **max degree of parallelism (MAXDOP)** setting that should be used for statements. 0 is the default value and indicates that the server configuration will be used instead. The MAXDOP at the database scope overrides (unless it is set to 0) the **max degree of parallelism** set at the server level by sp_configure. Query hints can still override the database scoped MAXDOP in order to tune specific queries that need different setting. All these settings are limited by the MAXDOP set for the [Workload Group](create-workload-group-transact-sql.md).

You can use the MAXDOP option to limit the number of processors to use in parallel plan execution. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] considers parallel execution plans for queries, index data definition language (DDL) operations, parallel insert, online alter column, parallel stats collection, and static and keyset-driven cursor population.

> [!NOTE]  
> The **max degree of parallelism (MAXDOP)** limit is set per [task](../../relational-databases/system-dynamic-management-views/sys-dm-os-tasks-transact-sql.md). It is not a per [request](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md) or per query limit. This means that during a parallel query execution, a single request can spawn multiple tasks which are assigned to a [scheduler](../../relational-databases/system-dynamic-management-views/sys-dm-os-tasks-transact-sql.md). For more information, see the [Thread and Task Architecture Guide](../../relational-databases/thread-and-task-architecture-guide.md).

To set this option at the instance level, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).

> [!NOTE]  
> In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], the MAXDOP database-scoped configuration for new single and elastic pool databases is set to 8 by default. MAXDOP can be configured for each database as described in the current article. For recommendations on configuring MAXDOP optimally, see [Additional Resources](#additional-resources) section.

> [!TIP]  
> To accomplish this at the query level, use the **MAXDOP** [query hint](../../t-sql/queries/hints-transact-sql-query.md).  
> To accomplish this at the server level, use the **max degree of parallelism (MAXDOP)** [server configuration option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).  
> To accomplish this at the workload level, use the **MAX_DOP** [Resource Governor workload group configuration option](../../t-sql/statements/create-workload-group-transact-sql.md).

PRIMARY

Can only be set for the secondaries, while the database in on the primary, and indicates that the configuration will be the one set for the primary. If the configuration for the primary changes, the value on the secondaries will change accordingly without the need to set the secondaries value explicitly. **PRIMARY** is the default setting for the secondaries.

#### LEGACY_CARDINALITY_ESTIMATION = { ON | OFF | PRIMARY }

Enables you to set the query optimizer cardinality estimation model to the SQL Server 2012 and earlier version independent of the compatibility level of the database. The default is **OFF**, which sets the query optimizer cardinality estimation model based on the compatibility level of the database. Setting LEGACY_CARDINALITY_ESTIMATION to **ON** is equivalent to enabling [Trace Flag 9481](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).

> [!TIP]  
> To accomplish this at the query level, add the **QUERYTRACEON** [query hint](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).
> Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP1, to accomplish this at the query level, add the **USE HINT** [query hint](../../t-sql/queries/hints-transact-sql-query.md#use_hint) instead of using the trace flag.

PRIMARY

This value is only valid on secondaries while the database in on the primary, and specifies that the query optimizer cardinality estimation model setting on all secondaries will be the value set for the primary. If the configuration on the primary for the query optimizer cardinality estimation model changes, the value on the secondaries will change accordingly. **PRIMARY** is the default setting for the secondaries.

#### PARAMETER_SNIFFING = { ON | OFF | PRIMARY }

Enables or disables [parameter sniffing](../../relational-databases/query-processing-architecture-guide.md#parameter-sensitivity). The default is ON. Setting PARAMETER_SNIFFING to OFF is equivalent to enabling [Trace Flag 4136](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).

> [!TIP]  
> To accomplish this at the query level, see the **OPTIMIZE FOR UNKNOWN** [query hint](../../t-sql/queries/hints-transact-sql-query.md).
> Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP1, to accomplish this at the query level, the **USE HINT** [query hint](../../t-sql/queries/hints-transact-sql-query.md#use_hint) is also available.

PRIMARY

This value is only valid on secondaries while the database in on the primary, and specifies that the value for this setting on all secondaries will be the value set for the primary. If the configuration on the primary for using [parameter sniffing](../../relational-databases/query-processing-architecture-guide.md#parameter-sensitivity) changes, the value on the secondaries will change accordingly without the need to set the secondaries value explicitly. PRIMARY is the default setting for the secondaries.

#### <a id="qo_hotfixes"></a> QUERY_OPTIMIZER_HOTFIXES = { ON | OFF | PRIMARY }

Enables or disables query optimization hotfixes regardless of the compatibility level of the database. The default is **OFF**, which disables query optimization hotfixes that were released after the highest available compatibility level was introduced for a specific version (post-RTM). Setting this to **ON** is equivalent to enabling [Trace Flag 4199](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL16](../../includes/sssql16-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

> [!TIP]  
> To accomplish this at the query level, add the **QUERYTRACEON** [query hint](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).
> Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP1, to accomplish this at the query level, add the USE HINT [query hint](../../t-sql/queries/hints-transact-sql-query.md#use_hint) instead of using the trace flag.

PRIMARY

This value is only valid on secondaries while the database in on the primary, and specifies that the value for this setting on all secondaries is the value set for the primary. If the configuration for the primary changes, the value on the secondaries changes accordingly without the need to set the secondaries value explicitly. PRIMARY is the default setting for the secondaries.

#### IDENTITY_CACHE = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Enables or disables identity cache at the database level. The default is **ON**. Identity caching is used to improve INSERT performance on tables with identity columns. To avoid gaps in the values of an identity column in cases where the server restarts unexpectedly or fails over to a secondary server, disable the IDENTITY_CACHE option. This option is similar to the existing [Trace Flag 272](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md), except that it can be set at the database level rather than only at the server level.

> [!NOTE]  
> This option can only be set for the PRIMARY. For more information, see [identity columns](create-table-transact-sql-identity-property.md).

#### INTERLEAVED_EXECUTION_TVF = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Allows you to enable or disable Interleaved execution for multi-statement table-valued functions at the database or statement scope while still maintaining database compatibility level 140 and higher. The default is **ON**. Interleaved execution is a feature that is part of Adaptive query processing in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. For more information, please refer to [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing-details.md#interleaved-execution-for-mstvfs).

> [!NOTE]  
> For database compatibility level 130 or lower, this database scoped configuration has no effect.
>
> In SQL Server 2017 (14.x) only, the option INTERLEAVED_EXECUTION_TVF had the older name of **DISABLE**_INTERLEAVED_EXECUTION_TVF.

#### BATCH_MODE_MEMORY_GRANT_FEEDBACK = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Allows you to enable or disable batch mode memory grant feedback at the database scope while still maintaining database compatibility level 140 and higher. The default is **ON**. Batch mode memory grant feedback, introduced in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)], is part of intelligent query processing suite of features. For more information, see [Memory grant feedback](../../relational-databases/performance/intelligent-query-processing-feedback.md#memory-grant-feedback).

> [!NOTE]  
> For database compatibility level 130 or lower, this database scoped configuration has no effect.

#### BATCH_MODE_ADAPTIVE_JOINS = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Allows you to enable or disable batch mode adaptive joins at the database scope while still maintaining database compatibility level 140 and higher. The default is **ON**. Batch mode adaptive joins is a feature that is part of [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing-details.md#batch-mode-adaptive-joins) introduced in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)].

> [!NOTE]  
> For database compatibility level 130 or lower, this database scoped configuration has no effect.

#### TSQL_SCALAR_UDF_INLINING = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] (feature is in public preview)

Allows you to enable or disable T-SQL Scalar UDF inlining at the database scope while still maintaining database compatibility level 150 and higher. The default is **ON**. T-SQL Scalar UDF inlining is part of the [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing-details.md#scalar-udf-inlining) feature family.

> [!NOTE]  
> For database compatibility level 140 or lower, this database scoped configuration has no effect.

#### ELEVATE_ONLINE = { OFF | WHEN_SUPPORTED | FAIL_UNSUPPORTED }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Allows you to select options to cause the engine to automatically elevate supported operations to online. The default is OFF, which means operations will not be elevated to online unless specified in the statement. [sys.database_scoped_configurations](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md) reflects the current value of ELEVATE_ONLINE. These options will only apply to operations that are supported for online.

FAIL_UNSUPPORTED

This value elevates all supported DDL operations to ONLINE. Operations that do not support online execution will fail and throw an error.

> [!NOTE]  
> Adding a column to a table is an online operation in the general case. In some scenarios, for example when [adding a non nullable column](alter-table-transact-sql.md#adding-not-null-columns-as-an-online-operation), a column cannot be added online. In those cases, if FAIL_UNSUPPORTED is set, the operation will fail.

WHEN_SUPPORTED

This value elevates operations that support ONLINE. Operations that do not support online will be run offline.

> [!NOTE]  
> You can override the default setting by submitting a statement with the ONLINE option specified.

#### ELEVATE_RESUMABLE= { OFF | WHEN_SUPPORTED | FAIL_UNSUPPORTED }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Allows you to select options to cause the engine to automatically elevate supported operations to resumable. The default is OFF, which means operations are not be elevated to resumable unless specified in the statement. [sys.database_scoped_configurations](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md) reflects the current value of ELEVATE_RESUMABLE. These options only apply to operations that are supported for resumable.

FAIL_UNSUPPORTED

This value elevates all supported DDL operations to RESUMABLE. Operations that do not support resumable execution fail and throw an error.

WHEN_SUPPORTED

This value elevates operations that support RESUMABLE. Operations that do not support resumable are run non-resumably.

> [!NOTE]  
> You can override the default setting by submitting a statement with the RESUMABLE option specified.

#### OPTIMIZE_FOR_AD_HOC_WORKLOADS = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Enables or disables a compiled plan stub to be stored in cache when a batch is compiled for the first time. The default is **OFF**. Once the database scoped configuration OPTIMIZE_FOR_AD_HOC_WORKLOADS is enabled for a database, a compiled plan stub will be stored in cache when a batch is compiled for the first time. Plan stubs have a smaller memory footprint compared to the size of the full compiled plan. If a batch is compiled or executed again, the compiled plan stub will be removed and replaced with a full compiled plan.

#### XTP_PROCEDURE_EXECUTION_STATISTICS = { ON | OFF }

**Applies to:** [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Enables or disables collection of execution statistics at the module-level for natively compiled T-SQL modules in the current database. The default is **OFF**. The execution statistics are reflected in [sys.dm_exec_procedure_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-procedure-stats-transact-sql.md).

Module-level execution statistics for natively compiled T-SQL modules are collected if either this option is ON, or if statistics collection is enabled through [sp_xtp_control_proc_exec_stats](../../relational-databases/system-stored-procedures/sys-sp-xtp-control-proc-exec-stats-transact-sql.md).

#### XTP_QUERY_EXECUTION_STATISTICS = { ON | OFF }

**Applies to:** [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Enables or disables collection of execution statistics at the statement-level for natively compiled T-SQL modules in the current database. The default is **OFF**. The execution statistics are reflected in [sys.dm_exec_query_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md) and in [Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md).

Statement-level execution statistics for natively compiled T-SQL modules are collected if either this option is ON, or if statistics collection is enabled through [sp_xtp_control_query_exec_stats](../../relational-databases/system-stored-procedures/sys-sp-xtp-control-query-exec-stats-transact-sql.md).

For more information about performance monitoring of natively compiled [!INCLUDE[tsql](../../includes/tsql-md.md)] modules see [Monitoring Performance of Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/monitoring-performance-of-natively-compiled-stored-procedures.md).

#### ROW_MODE_MEMORY_GRANT_FEEDBACK = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Allows you to enable or disable row mode memory grant feedback at the database scope while still maintaining database compatibility level 150 and higher. The default is **ON**. Row mode memory grant feedback a feature that is part of [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing-feedback.md#row-mode-memory-grant-feedback) introduced in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]. Row mode is supported in [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. For more information on memory grant feedback, see [Memory grant feedback](../../relational-databases/performance/intelligent-query-processing-feedback.md#memory-grant-feedback).

> [!NOTE]  
> For database compatibility level 140 or lower, this database scoped configuration has no effect.

#### MEMORY_GRANT_FEEDBACK_PERCENTILE = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)])

Allows you to disable memory grant feedback percentile for all query executions originating from the database. Default is **ON**. For complete information, see [Percentile and persistence mode memory grant feedback](../../relational-databases/performance/intelligent-query-processing-feedback.md#percentile-and-persistence-mode-memory-grant-feedback).

> [!NOTE]  
> For database compatibility level 140 or lower, this database scoped configuration has no effect.

#### MEMORY_GRANT_FEEDBACK_PERSISTENCE = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)])

Allows you to disable memory grant feedback persistence for all query executions originating from the database. Default is **ON**. For complete information, see [Percentile and persistence mode memory grant feedback](../../relational-databases/performance/intelligent-query-processing-feedback.md#percentile-and-persistence-mode-memory-grant-feedback).

> [!NOTE]  
> For database compatibility level 140 or lower, this database scoped configuration has no effect.

#### BATCH_MODE_ON_ROWSTORE = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Allows you to enable or disable batch mode on rowstore at the database scope while still maintaining database compatibility level 150 and higher. The default is **ON**. Batch mode on rowstore is a feature that is part of [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing-details.md#batch-mode-on-rowstore) feature family.

> [!NOTE]  
> For database compatibility level 140 or lower, this database scoped configuration has no effect.

#### DEFERRED_COMPILATION_TV = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Allows you to enable or disable table variable deferred compilation at the database scope while still maintaining database compatibility level 150 and higher. The default is **ON**.  Table variable deferred compilation is a feature that is part of [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing-details.md#table-variable-deferred-compilation) feature family.

> [!NOTE]  
> For database compatibility level 140 or lower, this database scoped configuration has no effect.

#### ACCELERATED_PLAN_FORCING = { ON | OFF }

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Enables an optimized mechanism for query plan forcing, applicable to all forms of plan forcing, such as [Query Store Force Plan](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md#Regressed), [Automatic Tuning](../../relational-databases/automatic-tuning/automatic-tuning.md#automatic-plan-correction), or the [USE PLAN](../../t-sql/queries/hints-transact-sql-query.md#use-plan) query hint. The default is **ON**.

> [!NOTE]  
> It is not recommended to disable accelerated plan forcing.

#### GLOBAL_TEMPORARY_TABLE_AUTO_DROP = { ON | OFF }

**Applies to:** [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Allows setting the auto-drop functionality for [global temporary tables](../../t-sql/statements/create-table-transact-sql.md#temporary-tables). The default is **ON**, which means that the global temporary tables are automatically dropped when not in use by any session. When set to OFF, global temporary tables need to be explicitly dropped using a DROP TABLE statement or will be automatically dropped on server restart.

- With [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] single databases and elastic pools, this option can be set in the individual user databases of the SQL Database server.
- In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Azure SQL Managed Instance, this option is set in `TempDB` and the setting of the individual user databases has no effect.

<a name="lqp"></a>

#### LIGHTWEIGHT_QUERY_PROFILING = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Allows you to enable or disable the [lightweight query profiling infrastructure](../../relational-databases/performance/query-profiling-infrastructure.md). The lightweight query profiling infrastructure (LWP) provides query performance data more efficiently than standard profiling mechanisms and is enabled by default. The default is **ON**. 

<a name="verbose-truncation"></a>

#### VERBOSE_TRUNCATION_WARNINGS = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Allows you to enable or disable the new `String or binary data would be truncated` error message. The default is **ON**. [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] introduces a new, more specific error message (2628) for this scenario:

`String or binary data would be truncated in table '%.*ls', column '%.*ls'. Truncated value: '%.*ls'.`

When set to ON under database compatibility level 150, truncation errors raise the new error message 2628 to provide more context and simplify the troubleshooting process.

When set to OFF under database compatibility level 150, truncation errors raise the previous error message 8152.

For database compatibility level 140 or lower, error message 2628 remains an opt-in error message that requires [trace flag](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) 460 to be enabled, and this database scoped configuration has no effect.

#### LAST_QUERY_PLAN_STATS = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Allows you to enable or disable collection of the last query plan statistics (equivalent to an actual execution plan) in [sys.dm_exec_query_plan_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-stats-transact-sql.md). The default is **OFF**. 

#### PAUSED_RESUMABLE_INDEX_ABORT_DURATION_MINUTES

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

The `PAUSED_RESUMABLE_INDEX_ABORT_DURATION_MINUTES` option determines how long (in minutes) the resumable index is being paused before being automatically aborted by the engine.

- The default value is set to one day (1440 minutes)
- The minimum duration is set to 1 minute
- The maximum duration is 71,582 minutes
- When set to 0, a paused operation will never automatically abort

The current value for this option is displayed in [sys.database_scoped_configurations](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md).

#### ISOLATE_SECURITY_POLICY_CARDINALITY = { ON | OFF}

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Allows you to control whether a [Row-Level Security](../../relational-databases/security/row-level-security.md) (RLS) predicate affects the cardinality of the execution plan of the overall user query. The default is **OFF**.  When ISOLATE_SECURITY_POLICY_CARDINALITY is ON, an RLS predicate does not affect the cardinality of an execution plan. For example, consider a table containing 1 million rows and an RLS predicate that restricts the result to 10 rows for a specific user issuing the query. With this database scoped configuration set to OFF, the cardinality estimate of this predicate will be 10. When this database scoped configuration is ON, query optimization will estimate 1 million rows. It is recommended to use the default value for most workloads.

#### DW_COMPATIBILITY_LEVEL = { AUTO | 10 | 20 | 9000 }

**Applies to:** [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)] only

Sets [!INCLUDE[tsql](../../includes/tsql-md.md)] and query processing behaviors to be compatible with the specified version of the database engine. Once it's set, when a query is executed on that database, only the compatible features will be exercised. At each compatibility level, various query processing enhancements are supported. Each level absorbs the functionality of the preceding level. A database's compatibility level is set to AUTO by default when it's first created and this is the recommended setting. The compatibility level is preserved even after database pause/resume, backup/restore operations.  The default is **AUTO**. 

| Compatibility Level |   Comments|
|-----------------------|--------------|
|**AUTO**| Default.  Its value is automatically updated by the Synapse Analytics engine and is represented by `0` in [sys.database_scoped_configurations](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md).  AUTO currently maps to compatibility level **20** functionality. |
|**10**| Exercises the Transact-SQL and query processing behaviors before the introduction of compatibility level support.|
|**20**| First compatibility level that includes gated Transact-SQL and query processing behaviors. The system stored procedure [sp_describe_undeclared_parameters](../../relational-databases/system-stored-procedures/sp-describe-undeclared-parameters-transact-sql.md) is supported under this level.|
|**9000**| Preview compatibility level. Preview features gated under this level are called out in feature-specific documentation. This level also includes abilities of highest non-9000 level.|

#### EXEC_QUERY_STATS_FOR_SCALAR_FUNCTIONS = { ON | OFF }

**Applies to:** [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Allows you to control whether execution statistics for scalar user-defined functions (UDF) appear in the [sys.dm_exec_function_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-function-stats-transact-sql.md) system view. For some intensive workloads that are scalar UDF-heavy, collecting function execution statistics may cause a noticeable performance overhead. This can be avoided by setting the `EXEC_QUERY_STATS_FOR_SCALAR_FUNCTIONS` database-scoped configuration to `OFF`.  The default is **ON**. 

#### ASYNC_STATS_UPDATE_WAIT_AT_LOW_PRIORITY = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

If asynchronous statistics update is enabled, enabling this configuration will cause the background request updating statistics to wait for a `Sch-M` lock on a low priority queue, to avoid blocking other sessions in high concurrency scenarios. For more information, see [AUTO_UPDATE_STATISTICS_ASYNC](../../relational-databases/statistics/statistics.md#auto_update_statistics_async).  The default is **OFF**. 

#### OPTIMIZED_PLAN_FORCING = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)]) <!--, [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] (Preview) and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)] (Preview)-->

Optimized plan forcing reduces compilation overhead for repeating forced queries. The default is **ON**. Once the query execution plan is generated, specific compilation steps are stored for reuse as an optimization replay script. An optimization replay script is stored as part of the compressed showplan XML in [Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md), in a hidden `OptimizationReplay` attribute. Learn more in [Optimized plan forcing with Query Store](../../relational-databases/performance/optimized-plan-forcing-query-store.md).

#### DOP_FEEDBACK = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)])

Identifies parallelism inefficiencies for repeating queries, based on elapsed time and waits. If parallelism usage is deemed inefficient, DOP feedback lowers the DOP for the next execution of the query, from whatever is the configured DOP, and verifies if it helps. Requires Query Store enabled and in READ_WRITE mode. For more information, see [Degrees of Parallelism (DOP) feedback](../../relational-databases/performance/intelligent-query-processing-feedback.md#degree-of-parallelism-dop-feedback). The default is **OFF**. 

#### CE_FEEDBACK = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)])

CE feedback addresses perceived regression issues resulting from incorrect CE model assumptions when using the default CE (CE120 or higher) and can selectively use different model assumptions. Requires Query Store enabled and in READ_WRITE mode. For more information, see [Cardinality estimation (CE) feedback](../../relational-databases/performance/intelligent-query-processing-feedback.md#cardinality-estimation-ce-feedback). The default is **ON** in database compatibility level 160 and higher. 

#### PARAMETER_SENSITIVE_PLAN_OPTIMIZATION = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)])

Parameter sensitivity plan (PSP) optimization addresses the scenario where a single cached plan for a parameterized query is not optimal for all possible incoming parameter values. This is the case with non-uniform data distributions. The default is **ON** starting in database compatibility level 160. For more information, see [Parameter Sensitive Plan optimization](../../relational-databases/performance/parameter-sensitivity-plan-optimization.md).

#### LEDGER_DIGEST_STORAGE_ENDPOINT = { &lt;endpoint URL string&gt; | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)])

Enables or disables uploading ledger digests to Azure Blob Storage. To enable uploading ledger digests, specify the endpoint of an Azure Blob storage account. To disable uploading ledger digests, set the option value to OFF. The default is OFF.

#### FORCE_SHOWPLAN_RUNTIME_PARAMETER_COLLECTION = { ON | OFF }

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

Causes SQL Server to generate a Showplan XML fragment with the ParameterRuntimeValue when using the lightweight query execution statistics profiling infrastructure or executing the `sys.dm_exec_query_statistics` DMV while troubleshooting long running queries.

> [!IMPORTANT]  
> The FORCE_SHOWPLAN_RUNTIME_PARAMETER_COLLECTION database scoped configuration option isn't meant to be enabled continuously in a production environment, but only for time-limited troubleshooting purposes. Using this database scoped configuration option will introduce additional and possibly significant CPU and memory overhead as we will create a Showplan XML fragment with runtime parameter information, whether the `sys.dm_exec_query_statistics_xml` DMV or lightweight query execution statistics profile infrastructure is enabed or not.

## <a id="Permissions"></a> Permissions

Requires `ALTER ANY DATABASE SCOPED CONFIGURATION` on the database. This permission can be granted by a user with `CONTROL` permission on a database.

## Remarks

While you can configure secondary databases to have different scoped configuration settings from their primary, all secondary databases use the same configuration. Different settings cannot be configured for individual secondaries.

Executing this statement clears the procedure cache in the current database, which means that all queries have to recompile.

For 3-part name queries, the settings for the current database connection for the query are honored, other than for SQL modules (such as procedures, functions, and triggers) that are compiled in another database context and therefore use the options of the database in which they reside. Similarly, when updating statistics asynchronously, the setting of ASYNC_STATS_UPDATE_WAIT_AT_LOW_PRIORITY for the database where statistics reside is honored.

The `ALTER_DATABASE_SCOPED_CONFIGURATION` event is added as a DDL event that can be used to fire a DDL trigger, and is a child of the `ALTER_DATABASE_EVENTS` trigger group.

Database scoped configuration settings will be carried over with the database, which means that when a given database is restored or attached, the existing configuration settings remain.

Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)], some option names have changed:

- `DISABLE_INTERLEAVED_EXECUTION_TVF` changed to `INTERLEAVED_EXECUTION_TVF`
- `DISABLE_BATCH_MODE_MEMORY_GRANT_FEEDBACK` changed to `BATCH_MODE_MEMORY_GRANT_FEEDBACK`
- `DISABLE_BATCH_MODE_ADAPTIVE_JOINS` changed to `BATCH_MODE_ADAPTIVE_JOINS`

## Limitations and Restrictions

### MAXDOP

The granular settings can override the global ones and that resource governor can cap all other MAXDOP settings. The logic for MAXDOP setting is the following:

- Query hint overrides both the `sp_configure` and the database scoped configuration. If the resource group MAXDOP is set for the workload group:

  - If the query hint is set to zero (0), it is overridden by the resource governor setting.

  - If the query hint is not zero (0), it is capped by the resource governor setting.

- The database scoped configuration (unless it's zero) overrides the `sp_configure` setting unless there is a query hint and is capped by the resource governor setting.

- The `sp_configure` setting is overridden by the resource governor setting.

### QUERY_OPTIMIZER_HOTFIXES

When `QUERYTRACEON` hint is used to enable the default Query Optimizer of SQL Server 7.0 through [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] versions or Query Optimizer hotfixes, it would be an OR condition between the query hint and the database scoped configuration setting, meaning if either is enabled, the database scoped configurations apply.

### Geo DR

Readable secondary databases (Always On Availability Groups, [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)] geo-replicated databases), use the secondary value by checking the state of the database. Even though recompile does not occur on failover and technically the new primary has queries that are using the secondary settings, the idea is that the setting between primary and secondary only vary when the workload is different and therefore the cached queries are using the optimal settings, whereas new queries pick the new settings that are appropriate for them.

### DacFx

Since `ALTER DATABASE SCOPED CONFIGURATION` is a new feature in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL16](../../includes/sssql16-md.md)]) that affects the database schema, exports of the schema (with or without data) are not able to be imported into an older version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], such as [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]. For example, an export to a [DACPAC](../../relational-databases/data-tier-applications/data-tier-applications.md) or a [BACPAC](../../relational-databases/data-tier-applications/data-tier-applications.md#bacpac) from an [!INCLUDE[ssSDS](../../includes/sssds-md.md)] or [!INCLUDE[ssSQL16](../../includes/sssql16-md.md)] database that used this new feature would not be able to be imported into a down-level server.

### ELEVATE_ONLINE

This option only applies to DDL statements that support the `WITH (ONLINE = <syntax>)`. XML indexes are not affected.

### ELEVATE_RESUMABLE

This option only applies to DDL statements that support the `WITH (RESUMABLE = <syntax>)`. XML indexes are not affected.

## Metadata

The [sys.database_scoped_configurations (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md) system view provides information about scoped configurations within a database. Database-scoped configuration options only show up in `sys.database_scoped_configurations` as they are overrides to server-wide default settings. The [sys.configurations (Transact-SQL)](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md) system view only shows server-wide settings.

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

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

This example disables the identity cache.

```sql
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = OFF ;
```

### H. Set OPTIMIZE_FOR_AD_HOC_WORKLOADS

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

This example enables a compiled plan stub to be stored in cache when a batch is compiled for the first time.

```sql
ALTER DATABASE SCOPED CONFIGURATION SET OPTIMIZE_FOR_AD_HOC_WORKLOADS = ON;
```

### I. Set ELEVATE_ONLINE

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

This example sets ELEVATE_ONLINE to FAIL_UNSUPPORTED.

```sql
ALTER DATABASE SCOPED CONFIGURATION SET ELEVATE_ONLINE = FAIL_UNSUPPORTED ;
```

### J. Set ELEVATE_RESUMABLE

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

This example sets ELEVATE_RESUMABLE to WHEN_SUPPORTED.

```sql
ALTER DATABASE SCOPED CONFIGURATION SET ELEVATE_RESUMABLE = WHEN_SUPPORTED ;
```

### K. Clear a query plan from the plan cache

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

This example clears a specific plan from the procedure cache

```sql
ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE 0x06000500F443610F003B7CD12C02000001000000000000000000000000000000000000000000000000000000;
```

### L. Set paused duration

**Applies to:** [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]

This example sets the resumable index paused duration to 60 minutes.

```sql
ALTER DATABASE SCOPED CONFIGURATION
SET PAUSED_RESUMABLE_INDEX_ABORT_DURATION_MINUTES = 60
```

### M. Enable and disable uploading ledger digests

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)])

This example enables uploading ledger digests to an Azure storage account.

```sql
ALTER DATABASE SCOPED CONFIGURATION
SET LEDGER_DIGEST_STORAGE_ENDPOINT = 'https://mystorage.blob.core.windows.net'
```

This example disables uploading ledger digests.

```sql
ALTER DATABASE SCOPED CONFIGURATION
SET LEDGER_DIGEST_STORAGE_ENDPOINT = OFF
```

## Additional Resources

### MAXDOP Resources

- [Degree of Parallelism](../../relational-databases/query-processing-architecture-guide.md#degree-of-parallelism-dop)
- [Recommendations and guidelines for the "max degree of parallelism" configuration option in SQL Server](https://support.microsoft.com/kb/2806535)

### LEGACY_CARDINALITY_ESTIMATION Resources

- [Cardinality Estimation (SQL Server)](../../relational-databases/performance/cardinality-estimation-sql-server.md)
- [Optimizing Your Query Plans with the SQL Server 2014 Cardinality Estimator](/previous-versions/dn673537(v=msdn.10))

### PARAMETER_SNIFFING Resources

- [Parameter Sniffing](../../relational-databases/query-processing-architecture-guide.md#parameter-sensitivity)
- ["I smell a parameter!"](/archive/blogs/queryoptteam/i-smell-a-parameter)

### QUERY_OPTIMIZER_HOTFIXES Resources

- [Trace Flags](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)
- [SQL Server query optimizer hotfix trace flag 4199 servicing model](https://support.microsoft.com/kb/974006)

### ELEVATE_ONLINE Resources

[Guidelines for Online Index Operations](../../relational-databases/indexes/guidelines-for-online-index-operations.md)

### ELEVATE_RESUMABLE Resources

[Guidelines for Online Index Operations](../../relational-databases/indexes/guidelines-for-online-index-operations.md)

## See also

- [sys.database_scoped_configurations](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md)
- [sys.configurations](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md)
- [Databases and Files Catalog Views](../../relational-databases/system-catalog-views/databases-and-files-catalog-views-transact-sql.md)
- [Server Configuration Options](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
- [ALTER INDEX (Transact-SQL)](../../t-sql/statements/alter-index-transact-sql.md)
- [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md)

## Next steps

- [Recommendations and guidelines for the "max degree of parallelism" configuration option in SQL Server](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md#Guidelines)
- [How Online Index Operations Work](../../relational-databases/indexes/how-online-index-operations-work.md)
- [Perform Index Operations Online](../../relational-databases/indexes/perform-index-operations-online.md)
- [Intelligent query processing features](../../relational-databases/performance/intelligent-query-processing.md)
- [Query processing feedback features](../../relational-databases/performance/intelligent-query-processing-feedback.md)
