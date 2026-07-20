---
title: "ALTER DATABASE SCOPED CONFIGURATION"
titleSuffix: SQL Server (Transact-SQL)
description: Enable several database configuration settings at the individual database level.
author: markingmyname
ms.author: maghan
ms.reviewer: derekw, bobward, jovanpop, wiassaf, mariyaali, randolphwest
ms.date: 06/29/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
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
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azure-sqldw-latest || =fabric-sqldb"
ai-usage: ai-assisted
---

# ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricsqldb.md)]

Use this command to enable several database configuration settings at the **individual database** level.

[!INCLUDE [alter-db](../../includes/alter-db.md)]

> [!IMPORTANT]
> Different `DATABASE SCOPED CONFIGURATION` options are supported in different versions and platforms of the SQL Database Engine. This article describes **all** `DATABASE SCOPED CONFIGURATION` options. Versions where applicable are noted. Make sure that you use the syntax that's available in the version of service that you're using.

The following settings are supported in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)], and in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] as indicated by the **Applies to** line for each setting in the [Arguments](#arguments) section:

- Clear procedure cache.
- Set the MAXDOP parameter to a recommended value (1, 2, ...) for the primary database based on what works best for that particular workload, and set a different value for secondary replica databases used by reporting queries. For guidance on choosing a MAXDOP, review [Server configuration: max degree of parallelism](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).
- Set the query optimizer cardinality estimation model independent of the database to compatibility level.
- Enable or disable parameter sniffing at the database level.
- Enable or disable query optimization hotfixes at the database level.
- Enable or disable the identity cache at the database level.
- Enable or disable a compiled plan stub to be stored in cache when a batch is compiled for the first time.
- Enable or disable collection of execution statistics for natively compiled [!INCLUDE [tsql](../../includes/tsql-md.md)] modules.
- Enable or disable online by default options for DDL statements that support the `ONLINE =` syntax.
- Enable or disable resumable by default options for DDL statements that support the `RESUMABLE =` syntax.
- Enable or disable [Intelligent query processing in SQL databases](../../relational-databases/performance/intelligent-query-processing.md) features.
- Enable or disable accelerated plan forcing.
- Enable or disable the autodrop functionality of global temporary tables.
- Enable or disable the [lightweight query profiling infrastructure](../../relational-databases/performance/query-profiling-infrastructure.md).
- Enable or disable the new `String or binary data would be truncated` error message.
- Enable or disable collection of last actual execution plan in [sys.dm_exec_query_plan_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-stats-transact-sql.md).
- Specify the number of minutes a paused resumable index operation is paused before it's automatically aborted by the [!INCLUDE [ssDE-md](../../includes/ssde-md.md)].
- Enable or disable waiting for locks at low priority for asynchronous statistics update.
- Enable or disable uploading ledger digests to Azure Blob Storage.
- Set the default [full-text index](../../relational-databases/search/full-text-search.md) version (`1` or `2`).
- In [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], sets the compatibility level of a user database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]:

```syntaxsql
ALTER DATABASE SCOPED CONFIGURATION
{
    { [ FOR SECONDARY ] SET <set_options> }
}
| CLEAR PROCEDURE_CACHE [plan_handle]
| SET < set_options >
[;]

< set_options > ::=
{
      ACCELERATED_PLAN_FORCING = { ON | OFF }
    | ALLOW_BUILTIN_TVF_IN_ALL_COMPAT_LEVELS = { ON | OFF }
    | ALLOW_STALE_VECTOR_INDEX = { ON | OFF }
    | ASYNC_STATS_UPDATE_WAIT_AT_LOW_PRIORITY = { ON | OFF }
    | BATCH_MODE_ADAPTIVE_JOINS = { ON | OFF }
    | BATCH_MODE_MEMORY_GRANT_FEEDBACK = { ON | OFF }
    | BATCH_MODE_ON_ROWSTORE = { ON | OFF }
    | CE_FEEDBACK = { ON | OFF }
    | DEFERRED_COMPILATION_TV = { ON | OFF }
    | DOP_FEEDBACK = { ON | OFF }
    | ELEVATE_ONLINE = { OFF | WHEN_SUPPORTED | FAIL_UNSUPPORTED }
    | ELEVATE_RESUMABLE = { OFF | WHEN_SUPPORTED | FAIL_UNSUPPORTED }
    | EXEC_QUERY_STATS_FOR_SCALAR_FUNCTIONS = { ON | OFF }
    | FORCE_SHOWPLAN_RUNTIME_PARAMETER_COLLECTION = { ON | OFF }   
    | FULLTEXT_INDEX_VERSION = <version>
    | IDENTITY_CACHE = { ON | OFF }
    | INTERLEAVED_EXECUTION_TVF = { ON | OFF }
    | ISOLATE_SECURITY_POLICY_CARDINALITY  = { ON | OFF }
    | GLOBAL_TEMPORARY_TABLE_AUTO_DROP = { ON | OFF }
    | LAST_QUERY_PLAN_STATS = { ON | OFF }
    | LEDGER_DIGEST_STORAGE_ENDPOINT = { <endpoint URL string> | OFF }
    | LEGACY_CARDINALITY_ESTIMATION = { ON | OFF | PRIMARY }
    | LIGHTWEIGHT_QUERY_PROFILING = { ON | OFF }
    | MAXDOP = { <value> | PRIMARY }
    | MEMORY_GRANT_FEEDBACK_PERCENTILE_GRANT = { ON | OFF }
    | MEMORY_GRANT_FEEDBACK_PERSISTENCE = { ON | OFF }
    | OPTIMIZE_FOR_AD_HOC_WORKLOADS = { ON | OFF }
    | OPTIMIZED_PLAN_FORCING = { ON | OFF }
    | OPTIMIZED_SP_EXECUTESQL = { ON | OFF }
    | OPTIONAL_PARAMETER_OPTIMIZATION = { ON | OFF }
    | PARAMETER_SENSITIVE_PLAN_OPTIMIZATION = { ON | OFF }
    | PARAMETER_SNIFFING = { ON | OFF | PRIMARY }
    | PAUSED_RESUMABLE_INDEX_ABORT_DURATION_MINUTES = <time>
    | PREVIEW_FEATURES = { ON | OFF }
    | QUERY_OPTIMIZER_HOTFIXES = { ON | OFF | PRIMARY }
    | READABLE_SECONDARY_TEMPORARY_STATS_AUTO_CREATE = { ON | OFF | PRIMARY }
    | READABLE_SECONDARY_TEMPORARY_STATS_AUTO_UPDATE = { ON | OFF | PRIMARY }
    | ROW_MODE_MEMORY_GRANT_FEEDBACK = { ON | OFF }
    | TSQL_SCALAR_UDF_INLINING = { ON | OFF }
    | VERBOSE_TRUNCATION_WARNINGS = { ON | OFF }
    | XTP_PROCEDURE_EXECUTION_STATISTICS = { ON | OFF }
    | XTP_QUERY_EXECUTION_STATISTICS = { ON | OFF }
}
```

Syntax for Azure Synapse Analytics:

```syntaxsql
ALTER DATABASE SCOPED CONFIGURATION
{
    SET <set_options>
}
[;]

< set_options > ::=
{
    DW_COMPATIBILITY_LEVEL = { AUTO | 10 | 20 | 30 | 40 | 50 | 9000 }
}
```

## Arguments

#### FOR SECONDARY

Specifies the settings for secondary databases. All secondary databases must have the identical values.

#### CLEAR PROCEDURE_CACHE [ plan_handle ]

Clears the procedure (plan) cache for the database. You can run this command on both the primary and the secondaries.

To clear a single query plan from the plan cache, specify a query plan handle.

**Applies to**: Specifying a query plan handle is available in [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)].

### SET options

#### ACCELERATED_PLAN_FORCING = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Enables an optimized mechanism for query plan forcing, applicable to all forms of plan forcing, such as [Query Store Force Plan](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md#Regressed), [Automatic Tuning](../../relational-databases/automatic-tuning/automatic-tuning.md#automatic-plan-correction), or the [USE PLAN](../queries/hints-transact-sql-query.md#use-plan) query hint. The default is `ON`.

> [!NOTE]  
> It isn't recommended to disable accelerated plan forcing.

<a id="allow-builtin-tvf-in-all-compat-levels"></a>

#### ALLOW_BUILTIN_TVF_IN_ALL_COMPAT_LEVELS = { ON | OFF }

**Applies to**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]

The `ALLOW_BUILTIN_TVF_IN_ALL_COMPAT_LEVELS` database-scoped configuration is **in preview**.

When enabled, it allows the following built-in table-valued functions (TVFs) to be used regardless of the database compatibility level:

- [GENERATE_SERIES](../functions/generate-series-transact-sql.md)
- [OPENJSON](../functions/openjson-transact-sql.md)
- [REGEXP_MATCHES](../functions/regexp-matches-transact-sql.md)
- [REGEXP_SPLIT_TO_TABLE](../functions/regexp-split-to-table-transact-sql.md)
- [STRING_SPLIT](../functions/string-split-transact-sql.md)

When disabled, built-in TVFs are only recognized starting with a specific compatibility level, as described in the documentation article for each function.

#### ALLOW_STALE_VECTOR_INDEX = { ON | OFF }

**Applies to**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]

Currently in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], vector indexes make tables read-only. To allow the table to be writable, use the `ALLOW_STALE_VECTOR_INDEX` database scoped configuration.

```sql
ALTER DATABASE SCOPED CONFIGURATION
SET ALLOW_STALE_VECTOR_INDEX = ON;
GO

SELECT *
FROM sys.database_scoped_configurations
WHERE [name] = 'ALLOW_STALE_VECTOR_INDEX';
```

When `ALLOW_STALE_VECTOR_INDEX = ON`, the vector index isn't updated when you insert or update new data in the table. To refresh the vector index, you must drop and recreate it.

> [!NOTE]  
> The `ALLOW_STALE_VECTOR_INDEX` database scoped configuration option isn't currently available in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

#### ASYNC_STATS_UPDATE_WAIT_AT_LOW_PRIORITY = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

If you enable asynchronous statistics updates, enabling this configuration causes the background request updating statistics to wait for a `Sch-M` lock on a low priority queue. This wait avoids blocking other sessions in high concurrency scenarios. For more information, see [AUTO_UPDATE_STATISTICS_ASYNC](../../relational-databases/statistics/statistics.md#auto_update_statistics_async). The default is `OFF`.

#### BATCH_MODE_ADAPTIVE_JOINS = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Enables or disables batch mode adaptive joins at the database scope while still maintaining database compatibility level 140 and higher. The default is `ON`. Batch mode adaptive joins is a feature that is part of [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing-details.md#batch-mode-adaptive-joins) introduced in [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)].

For database compatibility level 130 or lower versions, this database scoped configuration has no effect.

#### BATCH_MODE_MEMORY_GRANT_FEEDBACK = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Enables or disables batch mode memory grant feedback at the database scope while still maintaining database compatibility level 140 and higher. The default is `ON`. Batch mode memory grant feedback, introduced in [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)], is part of intelligent query processing suite of features. For more information, see [Memory grant feedback](../../relational-databases/performance/intelligent-query-processing-memory-grant-feedback.md).

For database compatibility level 130 or lower versions, this database scoped configuration has no effect.

#### BATCH_MODE_ON_ROWSTORE = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Enables or disables batch mode on rowstore at the database scope while still maintaining database compatibility level 150 and higher. The default is `ON`. Batch mode on rowstore is a feature that is part of [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing-details.md#batch-mode-on-rowstore) feature family.

For database compatibility level 140 or lower versions, this database scoped configuration has no effect.

#### CE_FEEDBACK = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

CE feedback addresses perceived regression problems that result from incorrect CE model assumptions when using the default CE (CE120 or higher). CE feedback can selectively use different model assumptions. Requires Query Store enabled and in `READ_WRITE` mode. For more information, see [Cardinality estimation (CE) feedback](../../relational-databases/performance/intelligent-query-processing-cardinality-estimation-feedback.md). The default is `ON` in database compatibility level 160 and higher.

#### DEFERRED_COMPILATION_TV = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Enables or disables table variable deferred compilation at the database scope while maintaining database compatibility level 150 or higher. The default is `ON`. Table variable deferred compilation is a feature that's part of the [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing-details.md#table-variable-deferred-compilation) feature family.

For database compatibility level 140 or lower versions, this database scoped configuration has no effect.

#### DOP_FEEDBACK = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)] with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy)

Identifies parallelism inefficiencies for repeating queries, based on elapsed time and waits. If parallelism usage is inefficient, DOP feedback lowers the DOP for the next execution of the query, from whatever is the configured DOP, and verifies if it helps. Requires Query Store enabled and in `READ_WRITE` mode. For more information, see [Degree of parallelism (DOP) feedback](../../relational-databases/performance/intelligent-query-processing-degree-parallelism-feedback.md). The default is `OFF`.

#### ELEVATE_ONLINE = { OFF | WHEN_SUPPORTED | FAIL_UNSUPPORTED }

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Allows you to select options to cause the engine to automatically elevate supported operations to online. 

This option only applies to DDL statements that support the `WITH (ONLINE = <syntax>)`. XML indexes aren't affected.

The default is `OFF`, which means operations aren't elevated to online unless specified in the statement. [sys.database_scoped_configurations](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md) reflects the current value of `ELEVATE_ONLINE`. These options only apply to operations that are supported for online. You can override the default setting by submitting a statement with the ONLINE option specified.

`FAIL_UNSUPPORTED`

This value elevates all supported DDL operations to ONLINE. Operations that don't support online execution fail and throw an error.

Adding a column to a table is an online operation in the general case. In some scenarios, for example when [adding a non-nullable column](alter-table-transact-sql.md#adding-not-null-columns-as-an-online-operation), a column can't be added online. In those cases, if `FAIL_UNSUPPORTED` is set, the operation fails.

`WHEN_SUPPORTED`

This value elevates operations that support ONLINE. Operations that don't support online are run offline.

For more information, see [Guidelines for online index operations](../../relational-databases/indexes/guidelines-for-online-index-operations.md).

#### ELEVATE_RESUMABLE = { OFF | WHEN_SUPPORTED | FAIL_UNSUPPORTED }

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Allows you to select options to cause the engine to automatically elevate supported operations to resumable.

This option only applies to DDL statements that support the `WITH (RESUMABLE = <syntax>)`. XML indexes aren't affected.

The default is `OFF`, which means operations aren't elevated to resumable unless specified in the statement. [sys.database_scoped_configurations](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md) reflects the current value of `ELEVATE_RESUMABLE`. These options only apply to operations that are supported for resumable. You can override the default setting by submitting a statement with the RESUMABLE option specified.

`FAIL_UNSUPPORTED`

This value elevates all supported DDL operations to `RESUMABLE`. Operations that don't support resumable execution fail and throw an error.

`WHEN_SUPPORTED`

This value elevates operations that support `RESUMABLE`. Operations that don't support resumable are run nonresumable.

For more information, see [Guidelines for online index operations](../../relational-databases/indexes/guidelines-for-online-index-operations.md).

#### EXEC_QUERY_STATS_FOR_SCALAR_FUNCTIONS = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Controls whether execution statistics for scalar user-defined functions (UDF) appear in the [sys.dm_exec_function_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-function-stats-transact-sql.md) system view. For some intensive workloads that are scalar UDF-heavy, collecting function execution statistics might cause a noticeable performance overhead. You can avoid this overhead by setting the `EXEC_QUERY_STATS_FOR_SCALAR_FUNCTIONS` database-scoped configuration to `OFF`. The default is `ON`.

#### FORCE_SHOWPLAN_RUNTIME_PARAMETER_COLLECTION = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

When you troubleshoot long running queries with lightweight query execution statistics profiling or the [sys.dm_exec_query_statistics_xml](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-statistics-xml-transact-sql.md) DMV, `FORCE_SHOWPLAN_RUNTIME_PARAMETER_COLLECTION` causes SQL Server generates a Showplan XML fragment that includes the `ParameterRuntimeValue`.

> [!IMPORTANT]  
> Don't enable the `FORCE_SHOWPLAN_RUNTIME_PARAMETER_COLLECTION` database scoped configuration option continuously in a production environment. Enable it only for time-limited troubleshooting purposes. This database scoped configuration option adds extra and possibly significant CPU and memory overhead as SQL Server creates a Showplan XML fragment with runtime parameter information, whether the `sys.dm_exec_query_statistics_xml` DMV or lightweight query execution statistics profile infrastructure is enabled or not.

#### FULLTEXT_INDEX_VERSION

**Applies to**: [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Sets the [full-text index version](../../relational-databases/search/full-text-index-version-upgrade.md) to use when creating or rebuilding indexes. This configuration only takes effect when you issue either a `CREATE FULLTEXT INDEX` statement for new indexes or an `ALTER FULLTEXT CATALOG ... REBUILD` statement to rebuild all indexes in a catalog.

As of [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)], the available versions are:

| Version | Comments |
| --- | --- |
| `1` | Specifies new and rebuilt indexes that use the legacy full-text filter and wordbreaker components from [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and earlier versions, for future populations and queries. As these components are no longer included in [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)] and later versions, they must be manually copied from an older instance. |
| `2` (default) | Specifies new and rebuilt indexes that use the full-text filter and wordbreaker components included in [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)], for future populations and queries. |

The `FULLTEXT_INDEX_VERSION` configuration also controls which full-text components the following system stored procedures, views, and functions report and use:

- [sp_help_fulltext_system_components](../../relational-databases/system-stored-procedures/sp-help-fulltext-system-components-transact-sql.md)
- [sys.fulltext_languages](../../relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql.md)
- [sys.fulltext_document_types](../../relational-databases/system-catalog-views/sys-fulltext-document-types-transact-sql.md)
- [sys.dm_fts_parser](../../relational-databases/system-dynamic-management-views/sys-dm-fts-parser-transact-sql.md)

#### IDENTITY_CACHE = { ON | OFF }

**Applies to**: [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Enables or disables identity cache at the database level. The default is `ON`. Identity caching improves `INSERT` performance on tables with identity columns. To avoid gaps in the values of an identity column when the server restarts unexpectedly or fails over to a secondary server, disable the `IDENTITY_CACHE` option. This option is similar to the existing [trace flag 272](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf272), but is set at the database level.

You can set this option only for the primary replica. For more information, see [identity columns](create-table-transact-sql-identity-property.md).

#### INTERLEAVED_EXECUTION_TVF = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Enables or disables interleaved execution for multi-statement table-valued functions at the database or statement scope while still maintaining database compatibility level 140 or higher. The default is `ON`. Interleaved execution is a feature that's part of Adaptive query processing in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. For more information, see [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing-details.md#interleaved-execution-for-mstvfs).

For database compatibility level 130 or lower versions, this database scoped configuration has no effect.

In SQL Server 2017 (14.x) only, the option `INTERLEAVED_EXECUTION_TVF` had the older name of `DISABLE_INTERLEAVED_EXECUTION_TVF`.

#### ISOLATE_SECURITY_POLICY_CARDINALITY = { ON | OFF}

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Allows you to control whether a [Row-level security](../../relational-databases/security/row-level-security.md) (RLS) predicate affects the cardinality of the execution plan of the overall user query. The default is `OFF`. When `ISOLATE_SECURITY_POLICY_CARDINALITY` is ON, an RLS predicate doesn't affect the cardinality of an execution plan. For example, consider a table containing 1 million rows and an RLS predicate that restricts the result to 10 rows for a specific user issuing the query. With this database scoped configuration set to OFF, the cardinality estimate of this predicate is 10. When this database scoped configuration is ON, query optimization estimates 1 million rows. It's recommended to use the default value for most workloads.

#### GLOBAL_TEMPORARY_TABLE_AUTO_DROP = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Sets the autodrop functionality for [global temporary tables](create-table-transact-sql.md#temporary-tables). The default is `ON`, which means that the global temporary tables are automatically dropped when not in use by any session or task. When set to `OFF`, you can only explicitly drop global temporary tables by using a `DROP TABLE` statement or they are automatically dropped on service restart.

- In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] single databases and elastic pools, set this option in the individual user databases.
- In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and Azure SQL Managed Instance, set this option in `tempdb`. The setting in individual user databases has no effect.

#### LAST_QUERY_PLAN_STATS = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Allows you to enable or disable collection of the last query plan statistics (equivalent to an actual execution plan) in [sys.dm_exec_query_plan_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-stats-transact-sql.md). The default is `OFF`.

#### LEDGER_DIGEST_STORAGE_ENDPOINT = { &lt;endpoint URL string&gt; | OFF }

**Applies to**: [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]

Enables or disables uploading ledger digests to Azure Blob Storage. To enable uploading ledger digests, specify the endpoint of an Azure Blob storage account. To disable uploading ledger digests, set the option value to `OFF`. The default is `OFF`.

#### LEGACY_CARDINALITY_ESTIMATION = { ON | OFF | PRIMARY }

Enables you to set the query optimizer cardinality estimation model to the SQL Server 2012 and earlier version independent of the compatibility level of the database. The default is `OFF`, which sets the query optimizer cardinality estimation model based on the compatibility level of the database. Setting `LEGACY_CARDINALITY_ESTIMATION` to `ON` is equivalent to enabling [trace flag 9481](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf9481).

- To set this option at the query level, add the `QUERYTRACEON` [query hint](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).
- To set this option at the query level in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] with Service Pack 1 and later versions, add the **USE HINT** [query hint](../queries/hints-transact-sql-query.md#use_hint) instead of using the trace flag.

PRIMARY

This value is only valid on secondaries while the database in on the primary, and specifies that the query optimizer cardinality estimation model setting on all secondaries is the value set for the primary. If the configuration on the primary for the query optimizer cardinality estimation model changes, the value on the secondaries changes accordingly. **PRIMARY** is the default setting for the secondaries.

For more information, see [Cardinality Estimation (SQL Server)](../../relational-databases/performance/cardinality-estimation-sql-server.md).

<a id="lqp"></a>

#### LIGHTWEIGHT_QUERY_PROFILING = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Allows you to enable or disable the [lightweight query profiling infrastructure](../../relational-databases/performance/query-profiling-infrastructure.md). The lightweight query profiling infrastructure (LWP) provides query performance data more efficiently than standard profiling mechanisms and is enabled by default. The default is `ON`.

#### MAXDOP = {\<value> | PRIMARY }

**\<value>**

Specifies the default **max degree of parallelism (MAXDOP)** setting that should be used for statements. 0 is the default value and indicates that the server configuration is used instead. The MAXDOP at the database scope overrides (unless it's set to 0) the `max degree of parallelism` set at the server level by `sp_configure`. Query hints can still override the database scoped MAXDOP in order to tune specific queries that need different setting. All these settings are limited by the MAXDOP set for the [workload group](create-workload-group-transact-sql.md).

Use the MAXDOP option to limit the number of processors to use in parallel plan execution. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] considers parallel execution plans for queries, index data definition language (DDL) operations, parallel insert, online alter column, parallel stats collection, and static and keyset-driven cursor population.

The **max degree of parallelism (MAXDOP)** limit is set per [task](../../relational-databases/system-dynamic-management-views/sys-dm-os-tasks-transact-sql.md). It isn't a per [request](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md) or per query limit. This means that during a parallel query execution, a single request can spawn multiple tasks, which are assigned to a [scheduler](../../relational-databases/system-dynamic-management-views/sys-dm-os-tasks-transact-sql.md). For more information, see the [Thread and task architecture guide](../../relational-databases/thread-and-task-architecture-guide.md).

To set this option at the instance level, see [Server configuration: max degree of parallelism](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).

In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the MAXDOP database-scoped configuration for new single and elastic pool databases is set to 8 by default. For more information and recommendations on configuring MAXDOP optimally in Azure SQL Database, see [Configure MAXDOP on Azure SQL Database](/azure/azure-sql/database/configure-max-degree-of-parallelism?view=azuresql-db&preserve-view=true).

- To set this option at the query level, use the `MAXDOP` [query hint](../queries/hints-transact-sql-query.md).
- To set this option at the server level, use the **max degree of parallelism (MAXDOP)** [server configuration option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).
- To set this option at the workload level, use the `MAX_DOP` [Resource Governor workload group configuration option](create-workload-group-transact-sql.md).

PRIMARY

Can only be set for the secondaries, while the database in on the primary, and indicates that the configuration is the one set for the primary. If the configuration for the primary changes, the value on the secondaries changes accordingly without the need to set the secondaries value explicitly. **PRIMARY** is the default setting for the secondaries.

For more information, see [Degree of Parallelism](../../relational-databases/query-processing-architecture-guide.md#degree-of-parallelism-dop).

#### MEMORY_GRANT_FEEDBACK_PERCENTILE_GRANT = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]

Enables or disables the memory grant feedback percentile feature for all query executions that start in the database. The default is `ON`. For more information, see [Percentile and persistence mode memory grant feedback](../../relational-databases/performance/intelligent-query-processing-memory-grant-feedback.md#percentile-and-persistence-mode-memory-grant-feedback).

For database compatibility level 140 or lower versions, this database scoped configuration has no effect.

#### MEMORY_GRANT_FEEDBACK_PERSISTENCE = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Enables or disables memory grant feedback persistence for all query executions that start in the database. The default is `ON`. For more information, see [Percentile and persistence mode memory grant feedback](../../relational-databases/performance/intelligent-query-processing-memory-grant-feedback.md#percentile-and-persistence-mode-memory-grant-feedback).

For database compatibility level 140 or lower versions, this database scoped configuration has no effect.

#### OPTIMIZE_FOR_AD_HOC_WORKLOADS = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Enables or disables storing a compiled plan stub in cache when a batch is compiled for the first time. The default is `OFF`. After you enable the database scoped configuration `OPTIMIZE_FOR_AD_HOC_WORKLOADS` for a database, the database stores a compiled plan stub in cache when a batch is compiled for the first time. Plan stubs use less memory than the full compiled plan. If a batch is compiled or executed again, the Database Engine removes the compiled plan stub and replaces it with a full compiled plan.

#### OPTIMIZED_PLAN_FORCING = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]

Optimized plan forcing reduces compilation overhead for repeating forced queries. The default is `ON`. After the query execution plan is generated, specific compilation steps are stored for reuse as an optimization replay script. An optimization replay script is stored as part of the compressed showplan XML in [Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md), in a hidden `OptimizationReplay` attribute. For more information, see [Optimized plan forcing with Query Store](../../relational-databases/performance/optimized-plan-forcing-query-store.md).

#### OPTIMIZED_SP_EXECUTESQL = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]

Enables or disables the compilation serialization behavior of `sp_executesql` when a batch is compiled. The default is `OFF`. Allowing batches that use `sp_executesql` to serialize the compilation process reduces the effect of compilation storms. A compilation storm is a situation where a large number of queries are being compiled simultaneously, leading to performance problems and resource contention.

When `OPTIMIZED_SP_EXECUTESQL` is `ON`, the first execution of `sp_executesql` compiles and inserts its compiled plan into the plan cache. Other sessions abort waiting on the compile lock and reuse the plan once it becomes available. This behavior makes `sp_executesql` act like objects such as stored procedures and triggers from a compilation perspective.

#### OPTIONAL_PARAMETER_OPTIMIZATION = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]

Enables or disables the [Optional parameter plan optimization (OPPO)](../../relational-databases/performance/optional-parameter-optimization.md) feature. The default is `ON` starting in database compatibility level 170.

When enabled, the adaptive plan optimization generates multiple execution plans for queries that include optional parameters. These plans typically use predicates in the form of:

- `@p IS NULL AND @p1 IS NOT NULL`
- `@p IS NULL OR @p1 IS NOT NULL`

The feature can choose a more optimal plan at runtime based on whether the parameter is `NULL`, which improves performance for queries that could otherwise default to suboptimal performance for such query patterns.

#### PARAMETER_SENSITIVE_PLAN_OPTIMIZATION = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Parameter sensitivity plan (PSP) optimization addresses the scenario where a single cached plan for a parameterized query isn't optimal for all possible incoming parameter values. This situation occurs with nonuniform data distributions. The default is `ON` starting in database compatibility level 160. For more information, see [Parameter Sensitive Plan optimization](../../relational-databases/performance/parameter-sensitive-plan-optimization.md).

#### PARAMETER_SNIFFING = { ON | OFF | PRIMARY }

Enables or disables [parameter sniffing](../../relational-databases/query-processing-architecture-guide.md#parameter-sensitivity). The default is `ON`. Setting `PARAMETER_SNIFFING` to `OFF` is equivalent to enabling [trace flag 4136](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf4136).

- To accomplish this at the query level, see the `OPTIMIZE FOR UNKNOWN` [query hint](../queries/hints-transact-sql-query.md).
- In [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP1 and later versions, to accomplish this at the query level, the `USE HINT` [query hint](../queries/hints-transact-sql-query.md#use_hint) is also available.

PRIMARY

This value is valid only on secondaries while the database is on the primary. It specifies that the value for this setting on all secondaries is the value set for the primary. If the configuration on the primary for using [parameter sniffing](../../relational-databases/query-processing-architecture-guide.md#parameter-sensitivity) changes, the value on the secondaries changes accordingly without the need to set the secondaries value explicitly. PRIMARY is the default setting for the secondaries.

For more information on `PARAMETER_SNIFFING`, see ["I smell a parameter!"](/archive/blogs/queryoptteam/i-smell-a-parameter).

#### PAUSED_RESUMABLE_INDEX_ABORT_DURATION_MINUTES

**Applies to**: [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

The `PAUSED_RESUMABLE_INDEX_ABORT_DURATION_MINUTES` option determines how long (in minutes) the resumable index is paused before the Database Engine automatically aborts it.

- The default value is set to one day (1,440 minutes).
- The minimum duration is set to 1 minute.
- The maximum duration is 71,582 minutes.
- When set to `0`, a paused operation never automatically aborts.

The current value for this option is displayed in [sys.database_scoped_configurations](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md).

<a id="preview-features"></a>

#### PREVIEW_FEATURES = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]

> [!CAUTION]  
> Preview features aren't recommended for production environments.

Allows usage of preview features. To learn more, review [Preview features in SQL Server](../../sql-server/sql-server-2025-release-notes.md#preview-features).

The default is `OFF`.

For an example of how to use this option, see [Using preview features in SQL Server](#n-enable-preview-features).

<a id="qo_hotfixes"></a>

#### QUERY_OPTIMIZER_HOTFIXES = { ON | OFF | PRIMARY }

**Applies to**: [!INCLUDE [ssSQL16](../../includes/sssql16-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Enables or disables query optimization hotfixes regardless of the compatibility level of the database. The default is `OFF`, which disables query optimization hotfixes that were released after the highest available compatibility level for a specific version (post-RTM). Setting `QUERY_OPTIMIZER_HOTFIXES` to `ON` is equivalent to enabling [trace flag 4199](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf4199).

- To set this option at the query level, add the `QUERYTRACEON` [query hint](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).
- To enable this feature at the query level in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] with Service Pack 1 and later versions, add the USE HINT [query hint](../queries/hints-transact-sql-query.md#use_hint) instead of using the trace flag.

When you use the `QUERYTRACEON` hint to enable the default Query Optimizer of SQL Server 7.0 through [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] versions or Query Optimizer hotfixes, it creates an OR condition between the query hint and the database scoped configuration setting. If either option is enabled, the database scoped configurations apply.

PRIMARY

This value is valid only on secondaries while the database is on the primary. It specifies that the value for this setting on all secondaries is the value set for the primary. If the configuration for the primary changes, the value on the secondaries changes accordingly without the need to set the secondaries value explicitly. PRIMARY is the default setting for the secondaries.

For more information on `QUERY_OPTIMIZER_HOTFIXES`, see [SQL Server query optimizer hotfix trace flag 4199 servicing model](https://support.microsoft.com/help/974006).

#### READABLE_SECONDARY_TEMPORARY_STATS_AUTO_CREATE = { ON | OFF | PRIMARY }

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazure-sqlmi-autd](../../includes/applies-to-version/ssazure-sqlmi-autd.md)], and [!INCLUDE [ssazure-sqlmi-2025](../../includes/applies-to-version/ssazure-sqlmi-2025.md)]

Enables or disables the automatic creation of [temporary statistics](../../relational-databases/statistics/statistics.md#temporary-statistics) for readable secondary replicas of a database and for database snapshots.

The default is `ON`.

#### READABLE_SECONDARY_TEMPORARY_STATS_AUTO_UPDATE = { ON | OFF | PRIMARY }

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazure-sqlmi-autd](../../includes/applies-to-version/ssazure-sqlmi-autd.md)], and [!INCLUDE [ssazure-sqlmi-2025](../../includes/applies-to-version/ssazure-sqlmi-2025.md)]

Enables or disables the automatic update of [temporary statistics](../../relational-databases/statistics/statistics.md#temporary-statistics) for readable secondary replicas of a database and for database snapshots.

The default is `ON`.

#### ROW_MODE_MEMORY_GRANT_FEEDBACK = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Enable or disable row mode memory grant feedback at the database scope while still maintaining database compatibility level 150 or higher. The default is `ON`. Row mode memory grant feedback is a feature that's part of [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing-memory-grant-feedback.md#row-mode-memory-grant-feedback) introduced in [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)]. Row mode is supported in [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. For more information on memory grant feedback, see [Memory grant feedback](../../relational-databases/performance/intelligent-query-processing-memory-grant-feedback.md).

For database compatibility level 140 or lower versions, this database scoped configuration has no effect.

#### TSQL_SCALAR_UDF_INLINING = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] (feature is in preview)

Enable or disable T-SQL Scalar UDF inlining at the database scope while still maintaining database compatibility level 150 or higher. The default is `ON`. T-SQL Scalar UDF inlining is part of the [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing-details.md#scalar-udf-inlining) feature family.

> [!NOTE]  
> For database compatibility level 140 or lower versions, this database scoped configuration has no effect.

<a id="verbose-truncation"></a>

#### VERBOSE_TRUNCATION_WARNINGS = { ON | OFF }

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Enable or disable the new `String or binary data would be truncated` error message. The default is `ON`. [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] introduced a more specific error message (2628) for this scenario:

`String or binary data would be truncated in table '%.*ls', column '%.*ls'. Truncated value: '%.*ls'.`

When set to `ON` under database compatibility level 150, truncation errors raise the new error message 2628 to provide more context and simplify the troubleshooting process.

When set to `OFF` under database compatibility level 150, truncation errors raise the previous error message 8152.

For database compatibility level 140 or lower versions, error message 2628 remains an opt-in error message that requires [trace flag 460](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf460) to be enabled, and this database scoped configuration has no effect.

#### XTP_PROCEDURE_EXECUTION_STATISTICS = { ON | OFF }

**Applies to**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Enables or disables collection of execution statistics at the module-level for natively compiled T-SQL modules in the current database. The default is `OFF`. The execution statistics are reflected in [sys.dm_exec_procedure_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-procedure-stats-transact-sql.md).

Module-level execution statistics for natively compiled T-SQL modules are collected if either this option is ON, or if statistics collection is enabled through [sp_xtp_control_proc_exec_stats](../../relational-databases/system-stored-procedures/sys-sp-xtp-control-proc-exec-stats-transact-sql.md).

#### XTP_QUERY_EXECUTION_STATISTICS = { ON | OFF }

**Applies to**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

Enables or disables collection of execution statistics at the statement-level for natively compiled T-SQL modules in the current database. The default is `OFF`. The execution statistics are reflected in [sys.dm_exec_query_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md) and in [Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md).

Statement-level execution statistics for natively compiled T-SQL modules are collected if either this option is `ON`, or if statistics collection is enabled through [sp_xtp_control_query_exec_stats](../../relational-databases/system-stored-procedures/sys-sp-xtp-control-query-exec-stats-transact-sql.md).

For more information about performance monitoring of natively compiled [!INCLUDE [tsql](../../includes/tsql-md.md)] modules, see [Monitoring Performance of Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/monitoring-performance-of-natively-compiled-stored-procedures.md).

#### DW_COMPATIBILITY_LEVEL = { AUTO | 10 | 20 | 30 | 40 | 50 | 9000 }

**Applies to**: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] only

Sets [!INCLUDE [tsql](../../includes/tsql-md.md)] and query processing behaviors to be compatible with the specified version of the database engine. Once you set it, when a query runs on that database, it uses only the compatible features. At each compatibility level, various query processing enhancements are supported. Each level absorbs the functionality of the preceding level. A database's compatibility level is set to AUTO by default when it's first created and this is the recommended setting. The compatibility level is preserved even after database pause/resume, backup/restore operations. The default is `AUTO`.

| Compatibility Level | Comments |
| --- | --- |
| `AUTO` | Default. The Synapse Analytics engine automatically updates its value. It's represented by `0` in [sys.database_scoped_configurations](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md). `AUTO` currently maps to compatibility level `30` functionality. |
| `10` | Exercises the Transact-SQL and query engine behaviors before the introduction of compatibility level support. |
| `20` | First compatibility level that includes gated Transact-SQL and query engine behaviors. The system stored procedure [sp_describe_undeclared_parameters](../../relational-databases/system-stored-procedures/sp-describe-undeclared-parameters-transact-sql.md) is supported under this level. |
| `30` | Includes new query engine behaviors. |
| `40` | Includes new query engine behaviors. |
| `50` | Multi-column distribution is supported under this level. To learn more, see [CREATE TABLE](create-table-azure-sql-data-warehouse.md), [CREATE TABLE AS SELECT](create-table-as-select-azure-sql-data-warehouse.md), and [CREATE MATERIALIZED VIEW AS SELECT](create-materialized-view-as-select-transact-sql.md). |
| `9000` | Preview compatibility level. Feature-specific documentation calls out preview features gated under this level. This level also includes abilities of highest non-`9000` level. |

## Permissions

Requires `ALTER ANY DATABASE SCOPED CONFIGURATION` on the database. A user with `CONTROL` permission on a database can grant this permission.

## Remarks

While you can configure secondary databases to have different scoped configuration settings from their primary, all secondary databases use the same configuration. You can't configure different settings for individual secondaries.

Executing this statement clears the procedure cache in the current database, which means that all queries have to recompile.

For three-part name queries, the settings for the current database connection for the query are honored, except for SQL modules (such as procedures, functions, and triggers) that are compiled in another database context and therefore use the options of the database in which they reside. Similarly, when updating statistics asynchronously, the setting of `ASYNC_STATS_UPDATE_WAIT_AT_LOW_PRIORITY` for the database where statistics reside is honored.

The `ALTER_DATABASE_SCOPED_CONFIGURATION` event is added as a DDL event that can be used to fire a DDL trigger. It's a child of the `ALTER_DATABASE_EVENTS` trigger group.

When you restore or attach a database, database scoped configuration settings are carried over and remain with the database.

Starting with [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)], in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)], some option names changed:

- `DISABLE_INTERLEAVED_EXECUTION_TVF` changed to `INTERLEAVED_EXECUTION_TVF`
- `DISABLE_BATCH_MODE_MEMORY_GRANT_FEEDBACK` changed to `BATCH_MODE_MEMORY_GRANT_FEEDBACK`
- `DISABLE_BATCH_MODE_ADAPTIVE_JOINS` changed to `BATCH_MODE_ADAPTIVE_JOINS`

### Check the status of a database scoped configuration option

To check if a configuration is enabled (1) or disabled (0) in a database, query [sys.database_scoped_configurations](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md). For example, to check the value for `LEGACY_CARDINALITY_ESTIMATION`, use a query like this:

```sql
USE <user_database>;
SELECT
    name,
    value,
    value_for_secondary
FROM sys.database_scoped_configurations
WHERE name = 'LEGACY_CARDINALITY_ESTIMATION';
```

## Limitations

### MAXDOP

Granular settings can override the global settings, and the resource governor can cap all other MAXDOP settings. The following logic applies to the `MAXDOP` setting:

- Query hint overrides both the `sp_configure` and the database scoped configuration. If the resource group MAXDOP is set for the workload group:

  - If the query hint is set to zero (0), it's overridden by the resource governor setting.

  - If the query hint isn't zero (0), it's capped by the resource governor setting.

- The database scoped configuration (unless it's zero) overrides the `sp_configure` setting unless there's a query hint and is capped by the resource governor setting.

- The resource governor setting overrides the `sp_configure` setting.

### Geo-replicated disaster recovery (DR)

Readable secondary databases (Always On Availability Groups, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)] geo-replicated databases) use the secondary value by checking the state of the database. Even though recompile doesn't occur on failover, and technically the new primary has queries that are using the secondary settings, the settings between primary and secondary only vary when the workload is different. Therefore, the cached queries use the optimal settings, whereas new queries pick the new settings that are appropriate for them.

### DacFx

The `ALTER DATABASE SCOPED CONFIGURATION` feature is available in [!INCLUDE [ssSQL16](../../includes/sssql16-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]. Because it affects the database schema, exports of the schema (with or without data) can't be imported into [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] and earlier versions. For example, an export to a [DACPAC](../../relational-databases/data-tier-applications/data-tier-applications.md) or a [BACPAC](../../relational-databases/data-tier-applications/data-tier-applications.md#bacpac) from an [!INCLUDE [ssSDS](../../includes/sssds-md.md)] or [!INCLUDE [ssSQL16](../../includes/sssql16-md.md)] database that uses this feature can't be imported into a down-level server.

## Metadata

The [sys.database_scoped_configurations](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md) system view provides information about scoped configurations within a database. Database-scoped configuration options only show up in `sys.database_scoped_configurations` as they're overrides to server-wide default settings. The [sys.configurations](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md) system view only shows server-wide settings.

## Examples

These examples demonstrate the use of `ALTER DATABASE SCOPED CONFIGURATION`.

### A. Grant permission

This example grants the permission required to execute `ALTER DATABASE SCOPED CONFIGURATION` to user `Joe`.

```sql
GRANT ALTER ANY DATABASE SCOPED CONFIGURATION TO [Joe];
```

### B. Set MAXDOP

This example sets MAXDOP = 1 for a primary database and MAXDOP = 4 for a secondary database in a geo-replication scenario.

```sql
ALTER DATABASE SCOPED CONFIGURATION
SET MAXDOP = 1;

ALTER DATABASE SCOPED CONFIGURATION
FOR SECONDARY
SET MAXDOP = 4;
```

This example sets MAXDOP for a secondary database to be the same as it's set for its primary database in a geo-replication scenario.

```sql
ALTER DATABASE SCOPED CONFIGURATION
FOR SECONDARY
SET MAXDOP = PRIMARY;
```

### C. Set LEGACY_CARDINALITY_ESTIMATION

This example sets `LEGACY_CARDINALITY_ESTIMATION` to `ON` for a secondary database in a geo-replication scenario.

```sql
ALTER DATABASE SCOPED CONFIGURATION
FOR SECONDARY
SET LEGACY_CARDINALITY_ESTIMATION = ON;
```

This example sets `LEGACY_CARDINALITY_ESTIMATION` for a secondary database as it is on the primary database in a geo-replication scenario.

```sql
ALTER DATABASE SCOPED CONFIGURATION
FOR SECONDARY
SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
```

### D. Set PARAMETER_SNIFFING

The following example sets `PARAMETER_SNIFFING` to `OFF` for a primary database in a geo-replication scenario.

```sql
ALTER DATABASE SCOPED CONFIGURATION
SET PARAMETER_SNIFFING = OFF;
```

The following example sets `PARAMETER_SNIFFING` to `OFF` for a secondary database in a geo-replication scenario.

```sql
ALTER DATABASE SCOPED CONFIGURATION
FOR SECONDARY
SET PARAMETER_SNIFFING = OFF;
```

The following example sets `PARAMETER_SNIFFING` for a secondary database to match the primary database in a geo-replication scenario.

```sql
ALTER DATABASE SCOPED CONFIGURATION
FOR SECONDARY
SET PARAMETER_SNIFFING = PRIMARY;
```

### E. Set QUERY_OPTIMIZER_HOTFIXES

Set `QUERY_OPTIMIZER_HOTFIXES` to `ON` for a primary database in a geo-replication scenario.

```sql
ALTER DATABASE SCOPED CONFIGURATION
SET QUERY_OPTIMIZER_HOTFIXES = ON;
```

### F. Clear procedure cache

The following example clears the procedure cache. You can clear the procedure cache only for a primary database.

```sql
ALTER DATABASE SCOPED CONFIGURATION
CLEAR PROCEDURE_CACHE;
```

### G. Set IDENTITY_CACHE

**Applies to**: [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

The following example disables the identity cache.

```sql
ALTER DATABASE SCOPED CONFIGURATION
SET IDENTITY_CACHE = OFF;
```

### H. Set OPTIMIZE_FOR_AD_HOC_WORKLOADS

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

This example enables storing a compiled plan stub in the cache when a batch is compiled for the first time.

```sql
ALTER DATABASE SCOPED CONFIGURATION
SET OPTIMIZE_FOR_AD_HOC_WORKLOADS = ON;
```

### I. Set ELEVATE_ONLINE

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

This example sets `ELEVATE_ONLINE` to `FAIL_UNSUPPORTED`.

```sql
ALTER DATABASE SCOPED CONFIGURATION
SET ELEVATE_ONLINE = FAIL_UNSUPPORTED;
```

### J. Set ELEVATE_RESUMABLE

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

This example sets `ELEVATE_RESUMABLE` to `WHEN_SUPPORTED`.

```sql
ALTER DATABASE SCOPED CONFIGURATION
SET ELEVATE_RESUMABLE = WHEN_SUPPORTED;
```

### K. Clear a query plan from the plan cache

**Applies to**: [!INCLUDE [ssSQL19](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

This example clears a specific plan from the procedure cache:

```sql
ALTER DATABASE SCOPED CONFIGURATION
CLEAR PROCEDURE_CACHE 0x06000500F443610F003B7CD12C02000001000000000000000000000000000000000000000000000000000000;
```

### L. Set paused duration

**Applies to**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]

This example sets the resumable index paused duration to 60 minutes.

```sql
ALTER DATABASE SCOPED CONFIGURATION
SET PAUSED_RESUMABLE_INDEX_ABORT_DURATION_MINUTES = 60;
```

### M. Enable and disable uploading ledger digests

**Applies to**: [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and later versions

This example enables uploading ledger digests to an Azure storage account.

```sql
ALTER DATABASE SCOPED CONFIGURATION
SET LEDGER_DIGEST_STORAGE_ENDPOINT = 'https://mystorage.blob.core.windows.net';
```

This example disables uploading ledger digests.

```sql
ALTER DATABASE SCOPED CONFIGURATION
SET LEDGER_DIGEST_STORAGE_ENDPOINT = OFF;
```

### N. Enable preview features

Enable the ability to use features in [preview](#preview-features).

```sql
ALTER DATABASE SCOPED CONFIGURATION
SET PREVIEW_FEATURES = ON;

SELECT *
FROM sys.database_scoped_configurations
WHERE [name] = 'PREVIEW_FEATURES';
```

### O. Allow vector index to go stale

In the current preview state of Azure SQL Database and Fabric SQL database, vector indexes make tables read-only. To make the table writable, enable the following database scoped configuration:

```sql
ALTER DATABASE SCOPED CONFIGURATION
SET ALLOW_STALE_VECTOR_INDEX = ON;

SELECT *
FROM sys.database_scoped_configurations
WHERE [name] = 'ALLOW_STALE_VECTOR_INDEX';
```

When `ALLOW_STALE_VECTOR_INDEX = ON`, the vector index isn't updated when you insert or update new data in the table. To refresh the vector index, you must drop and recreate it.

This configuration option isn't currently available in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

## Related content

- [sys.database_scoped_configurations](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md)
- [sys.configurations](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md)
- [Databases and Files Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/databases-and-files-catalog-views-transact-sql.md)
- [Server configuration options](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
- [ALTER INDEX (Transact-SQL)](alter-index-transact-sql.md)
- [CREATE INDEX (Transact-SQL)](create-index-transact-sql.md)
- [Recommendations and guidelines for the "max degree of parallelism" configuration option in SQL Server](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md#Guidelines)
- [How online index operations work](../../relational-databases/indexes/how-online-index-operations-work.md)
- [Perform index operations online](../../relational-databases/indexes/perform-index-operations-online.md)
- [Intelligent query processing in SQL databases](../../relational-databases/performance/intelligent-query-processing.md)
- [Memory grant feedback](../../relational-databases/performance/intelligent-query-processing-memory-grant-feedback.md)
- [Cardinality estimation (CE) feedback](../../relational-databases/performance/intelligent-query-processing-cardinality-estimation-feedback.md)
- [Degree of parallelism (DOP) feedback](../../relational-databases/performance/intelligent-query-processing-degree-parallelism-feedback.md)
