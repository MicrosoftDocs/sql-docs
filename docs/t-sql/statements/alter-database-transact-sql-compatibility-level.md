---
title: "ALTER DATABASE compatibility level (Transact-SQL)"
description: Sets Transact-SQL and query processing behaviors to be compatible with the specified version of the Database Engine.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/21/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "COMPATIBILITY_LEVEL_TSQL"
  - "COMPATIBILITY_LEVEL"
helpviewer_keywords:
  - "80 compatibility level"
  - "ALTER DATABASE statement, compatibility levels"
  - "90 compatibility level"
  - "compatibility levels [SQL Server]"
  - "100 compatibility level"
  - "db compatibility level"
  - "db compat level"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# ALTER DATABASE (Transact-SQL) compatibility level

[!INCLUDE [sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Sets [!INCLUDE[tsql](../../includes/tsql-md.md)] and query processing behaviors to be compatible with the specified version of the SQL engine. For other ALTER DATABASE options, see [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md).

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

## Syntax

```syntaxsql
ALTER DATABASE database_name
SET COMPATIBILITY_LEVEL = { 160 | 150 | 140 | 130 | 120 | 110 | 100 | 90 }
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *database_name*

The name of the database to be modified.

#### COMPATIBILITY_LEVEL { 160 | 150 | 140 | 130 | 120 | 110 | 100 | 90 | 80 }

The version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with which the database is to be made compatible. The following compatibility level values can be configured (not all versions supports all of the above listed compatibility level):

<a name="supported-dbcompats"></a>

| Product | Database Engine version | Default compatibility level designation | Supported compatibility level values |
| --- | --- | --- | --- |
| [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] | 16 | 160 | 160, 150, 140, 130, 120, 110, 100 |
| [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] | 15 | 150 | 150, 140, 130, 120, 110, 100 |
| [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] | 14 | 140 | 140, 130, 120, 110, 100 |
| [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] | 12 | 150 | 160, 150, 140, 130, 120, 110, 100 |
| [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)] | 12 | 150 | 160, 150, 140, 130, 120, 110, 100 |
| [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] | 13 | 130 | 130, 120, 110, 100 |
| [!INCLUDE[sssql14-md](../../includes/sssql14-md.md)] | 12 | 120 | 120, 110, 100 |
| [!INCLUDE[sssql14-md](../../includes/sssql14-md.md)] | 11 | 110 | 110, 100, 90 |
| [!INCLUDE[sql2008r2-md](../../includes/sql2008r2-md.md)] | 10.5 | 100 | 100, 90, 80 |
| [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] | 10 | 100 | 100, 90, 80 |
| [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] | 9 | 90 | 90, 80 |
| [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] | 8 | 80 | 80 |

> [!IMPORTANT]  
> The database engine version numbers for SQL Server and Azure SQL Database are not comparable with each other, and rather are internal build numbers for these separate products. The database engine for Azure SQL Database is based on the same code base as the SQL Server database engine. Most importantly, the database engine in Azure SQL Database always has the newest SQL database engine bits. Version 12 of Azure SQL Database is newer than version 15 of SQL Server.

## Best practices for upgrading database compatibility level

For the recommended workflow for upgrading the compatibility level, see [Keep performance stability during the upgrade to newer SQL Server](../../relational-databases/performance/query-store-usage-scenarios.md#CEUpgrade). Additionally, for an assisted experience with upgrading the database compatibility level, see [Upgrading Databases by using the Query Tuning Assistant](../../relational-databases/performance/upgrade-dbcompat-using-qta.md).

## Remarks

For all installations of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the default compatibility level is associated with the version of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. New databases are set to this level unless the `model` database has a lower compatibility level. For databases attached or restored from any earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the database keeps its existing compatibility level, if it is at least minimum allowed for that instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Moving a database with a compatibility level lower than the allowed level by the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] automatically sets the database to the lowest compatibility level allowed. This applies to both system and user databases.

The below behaviors are expected for [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] when a database is attached or restored, and after an in-place upgrade:

- If the compatibility level of a user database was 100 or higher before the upgrade, it remains the same after upgrade.
- If the compatibility level of a user database was 90 before upgrade, in the upgraded database, the compatibility level is set to 100, which is the lowest supported compatibility level in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)].
- The compatibility levels of the `tempdb`, `model`, `msdb`, and Resource databases are set to the default compatibility level for a given [!INCLUDE[ssDE](../../includes/ssde-md.md)] version.
- The `master` system database retains the compatibility level it had before upgrade. This won't affect user database behavior.

For pre-existing databases running at lower compatibility levels, as long as the application doesn't need to use enhancements that are only available in a higher database compatibility level, it is a valid approach to maintain the previous database compatibility level. For new development work, or when an existing application requires use of new features such as [Intelligent Query Processing](../../relational-databases/performance/intelligent-query-processing.md) and some new [!INCLUDE[tsql](../../includes/tsql-md.md)], plan to upgrade the database compatibility level to the latest available. For more information, see [Compatibility levels and Database Engine upgrades](../../database-engine/install-windows/compatibility-certification.md#compatibility-levels-and-database-engine-upgrades).

> [!NOTE]  
> If there are no user objects and dependencies, it is generally safe to upgrade to the default compatibility level. For more information, see [Recommendations - master database](../../relational-databases/databases/master-database.md#recommendations).

Use `ALTER DATABASE` to change the compatibility level of the database. The new compatibility level setting for a database takes effect when a `USE <database>` command is issued, or a new login is processed with that database as the default database context.
To view the current compatibility level of a database, query the `compatibility_level` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

A [distribution database](../../relational-databases/replication/distribution-database.md) that was created in an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and is upgraded to [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] RTM or Service Pack 1 has a compatibility level of 90, which isn't supported for other databases. This doesn't have an effect on the functionality of replication. Upgrading to later service packs and versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will result in the compatibility level of the distribution database to be increased to match that of the `master` database.

To use database compatibility level 120 or higher for a database overall, but opt-in to the [**cardinality estimation**](../../relational-databases/performance/cardinality-estimation-sql-server.md) model of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], which maps to database compatibility level 110, see [ALTER DATABASE SCOPED CONFIGURATION](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md), and in particular its keyword `LEGACY_CARDINALITY_ESTIMATION = ON`.

To determine the current compatibility level, query the `compatibility_level` column of [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md).

```sql
SELECT name, compatibility_level FROM sys.databases;
```

### Remarks for Azure SQL Database

As of **November 2019**, in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], the default compatibility level is 150 for newly created databases. [!INCLUDE[msCoName](../../includes/msconame-md.md)] doesn't update database compatibility level for existing databases. It is up to customers to do at their own discretion. [!INCLUDE[msCoName](../../includes/msconame-md.md)] highly recommends that customers plan to upgrade to the latest compatibility level in order to use the latest query optimization improvements.

For details about how to assess the performance differences of your most important queries between two different compatibility levels on [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], see [Improved Query Performance with Compatibility Level 130 in Azure SQL Database](https://techcommunity.microsoft.com/t5/azure-sql-blog/improved-query-performance-with-compatibility-level-130-in-azure/ba-p/386100). This article refers to compatibility level 130 and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but the same methodology applies for upgrades to 140 or higher levels in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

To determine the version of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] that you're connected to, execute the following query.

```sql
SELECT SERVERPROPERTY('ProductVersion');
```

Not all features that vary by compatibility level are supported on [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

## Compatibility levels and database engine upgrades

Database compatibility level is a valuable tool to help with database modernization by allowing the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] to be upgraded while keeping the same functional status for connecting applications by maintaining the same pre-upgrade database compatibility level. This means that it's possible to upgrade from an older version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (such as [!INCLUDE[ssKatmai](../../includes/ssKatmai-md.md)]) to [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] (including Azure SQL Managed Instance) with no application changes (except for database connectivity). For more information, see [Compatibility Certification](../../database-engine/install-windows/compatibility-certification.md).

As long as the application doesn't need to use enhancements that are only available in a higher database compatibility level, it is a valid approach to upgrade the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and maintain the previous database compatibility level. For more information on using compatibility level for backward compatibility, see [Compatibility Certification](../../database-engine/install-windows/compatibility-certification.md).

## Compatibility levels and stored procedures

When a stored procedure executes, it uses the current compatibility level of the database in which it's defined. When the compatibility setting of a database is changed, all of its stored procedures are automatically recompiled accordingly.

## <a id="backwardCompat"></a> Use compatibility level for backward compatibility

The [database compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md) setting provides backward compatibility with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in what relates to [!INCLUDE[tsql](../../includes/tsql-md.md)] and query optimization behaviors only for the specified database, not for the entire server.

Starting with compatibility mode 130, any new query plan affecting fixes and features have been intentionally added only to the new compatibility level. This has been done in order to minimize the risk during upgrades that arise from performance degradation due to query plan changes potentially introduced by new query optimization behaviors.

From an application perspective, use the lower compatibility level as a safer migration path to work around version differences, in the behaviors that are controlled by the relevant compatibility level setting. The goal should still be to upgrade to the latest compatibility level at some point in time, in order to inherit some of the new features such as [Intelligent Query Processing](../../relational-databases/performance/intelligent-query-processing.md), but to do so in a controlled way.

For more information, including the recommended workflow for upgrading database compatibility level, see [Best Practices for upgrading database compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#best-practices-for-upgrading-database-compatibility-level).

 - **Discontinued** functionality introduced in a given [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version **is not** protected by compatibility level. This refers to functionality that was removed from the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. For example, the `FASTFIRSTROW` hint was discontinued in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and replaced with the `OPTION (FAST n )` hint. Setting the database compatibility level to 110 won't restore the discontinued hint. For more information on discontinued functionality, see [Discontinued database engine functionality in SQL Server](../../database-engine/discontinued-database-engine-functionality-in-sql-server.md).

 - **Breaking changes** introduced in a given [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version **may not** be protected by compatibility level. This refers to behavior changes between versions of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. [!INCLUDE[tsql](../../includes/tsql-md.md)] behavior is usually protected by compatibility level. However, changed or removed system objects are **not** protected by compatibility level.

     An example of a breaking change **protected** by compatibility level is an implicit conversion from datetime to datetime2 data types. Under database compatibility level 130, these show improved accuracy by accounting for the fractional milliseconds, resulting in different converted values. To restore previous conversion behavior, set the database compatibility level to 120 or lower.

     Examples of breaking changes **not protected** by compatibility level are:

     - Changed column names in system objects. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] the column `single_pages_kb` in `sys.dm_os_sys_info` was renamed to `pages_kb`. Regardless of the compatibility level, the query `SELECT single_pages_kb FROM sys.dm_os_sys_info` will produce error 207 (Invalid column name).
     - Removed system objects. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] the `sp_dboption` was removed. Regardless of the compatibility level, the statement `EXEC sp_dboption 'AdventureWorks2016', 'autoshrink', 'FALSE';` will produce error 2812 (Couldn't find stored procedure 'sp_dboption').

     For more information on breaking changes, see [Breaking Changes to Database Engine Features in SQL Server 2019](../../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2019.md), [Breaking Changes to Database Engine Features in SQL Server 2017](../../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2017.md), [Breaking Changes to Database Engine Features in SQL Server 2016](../../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016.md), and [Breaking Changes to Database Engine Features in SQL Server 2014](/previous-versions/sql/2014/database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016?preserve-view=true&view=sql-server-2014).

## Differences between compatibility levels

For all installations of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the default compatibility level is associated with the version of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], as seen in [this table](#supported-dbcompats). For new development work, always plan to certify applications on the latest database compatibility level.

New [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax isn't gated by database compatibility level, except when they can break existing applications by creating a conflict with user [!INCLUDE[tsql](../../includes/tsql-md.md)] code. These exceptions are documented in the next sections of this article that outline the differences between specific compatibility levels.

Database compatibility level also provides backward compatibility with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], because databases attached or restored from any earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] retain their existing compatibility level (if same or higher than the minimum allowed compatibility level). This was discussed in the [Using compatibility level for backward compatibility](#backwardCompat) section of this article.

Starting with database compatibility level 130, any new fixes and features affecting query plans have been added only to the latest compatibility level available, also called the default compatibility level. This has been done in order to minimize the risk during upgrades that arise from performance degradation due to query plan changes, potentially introduced by new query optimization behaviors.

The fundamental plan-affecting changes added only to the default compatibility level of a new version of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] are:

1. **Query Optimizer fixes released for previous [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions under trace flag 4199 become automatically enabled in the default compatibility level of a newer [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version**. **Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

    For example, when [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] was released, all the Query Optimizer fixes released for previous [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions (and respective compatibility levels 100 through 120) became automatically enabled for databases that use the [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] default compatibility level (130). Only post-RTM Query Optimizer fixes need to be explicitly enabled.

    To enable Query Optimizer fixes, you can use the following methods:

    - At the server level, with [trace flag 4199](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf4199).
    - At the database level, with the `QUERY_OPTIMIZER_HOTFIXES` option in [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).
    - At the query level, with the `USE HINT 'ENABLE_QUERY_OPTIMIZER_HOTFIXES'` [query hint](../../t-sql/queries/hints-transact-sql-query.md#use_hint) by modifying the query.
    - At the query level, with the `USE HINT 'ENABLE_QUERY_OPTIMIZER_HOTFIXES'` without code changes, using the [Query Store hint (Preview)](../../relational-databases/performance/query-store-hints.md) feature.

    Later, when [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] was released, all the Query Optimizer fixes released after [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] RTM became automatically enabled for databases using the [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] default compatibility level (140). This is a cumulative behavior that includes all previous versions fixes as well. Again, only post-RTM Query Optimizer fixes need to be explicitly enabled.

    The following table summarizes this behavior:

    | Database Engine (DE) version | Database Compatibility Level | TF 4199 | QO changes from all previous Database Compatibility Levels | QO changes for DE version post-RTM |
    | --- | --- | --- | --- | --- |
    | 13 ([!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]) | 100 to 120<br /><br /><br />130 | Off<br />On<br /><br />Off<br />On | **Disabled**<br />Enabled<br /><br />**Enabled**<br />Enabled | Disabled<br />Enabled<br /><br />Disabled<br />Enabled |
    | 14 ([!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]) | 100 to 120<br /><br /><br />130<br /><br /><br />140 | Off<br />On<br /><br />Off<br />On<br /><br />Off<br />On | **Disabled**<br />Enabled<br /><br />**Enabled**<br />Enabled<br /><br />**Enabled**<br />Enabled | Disabled<br />Enabled<br /><br />Disabled<br />Enabled<br /><br />Disabled<br />Enabled |
    | 15 ([!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]) and 12 ([!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]) | 100 to 120<br /><br /><br />130 to 140<br /><br /><br />150 | Off<br />On<br /><br />Off<br />On<br /><br />Off<br />On | **Disabled**<br />Enabled<br /><br />**Enabled**<br />Enabled<br /><br />**Enabled**<br />Enabled | Disabled<br />Enabled<br /><br />Disabled<br />Enabled<br /><br />Disabled<br />Enabled |
    | 16 ([!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)]) and 12 ([!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]) | 100 to 120<br /><br /><br />130 to 150<br /><br /><br />160 | Off<br />On<br /><br />Off<br />On<br /><br />Off<br />On | **Disabled**<br />Enabled<br /><br />**Enabled**<br />Enabled<br /><br />**Enabled**<br />Enabled | Disabled<br />Enabled<br /><br />Disabled<br />Enabled<br /><br />Disabled<br />Enabled |

    Query Optimizer fixes that address wrong results or access violation errors aren't protected by trace flag 4199. Those fixes aren't considered optional.

1. **Changes to the [Cardinality Estimator](../../relational-databases/performance/cardinality-estimation-sql-server.md) released on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] are enabled only in the default compatibility level of a new [!INCLUDE[ssDE](../../includes/ssde-md.md)] version**, but not on previous compatibility levels.

    For example, when [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] was released, changes to the cardinality estimation process were available only for databases using [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] default compatibility level (130). Previous compatibility levels retained the cardinality estimation behavior that was available before [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)].

    Later, when [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] was released, newer changes to the cardinality estimation process were available only for databases using [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] default compatibility level (140). Database compatibility level 130 retained the [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] cardinality estimation behavior.

    The following table summarizes this behavior:

    | Database Engine version | Database Compatibility Level | New version CE changes |
    | --- | --- | --- |
    | 13 ([!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]) | < 130<br />130 | Disabled<br />Enabled |
    | 14 ([!INCLUDE[ssSQL17](../../includes/sssql17-md.md)])<sup>1</sup> | < 140<br />140 | Disabled<br />Enabled |
    | 15 ([!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)])<sup>1</sup> | < 150<br />150 | Disabled<br />Enabled |
    | 16 ([!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)])<sup>1</sup> | < 160<br />160 | Disabled<br />Enabled |

    <sup>1</sup> Also applicable to [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

Other differences between specific compatibility levels are available in the next sections of this article.

## Differences between compatibility level 150 and level 160

This section describes new behaviors introduced with compatibility level 160.

| Compatibility level setting of 150 or lower | Compatibility level setting of 160 |
| :--- | :--- |
| Parameterized queries have a single query plan based on the parameters used for the first execution. Only one query plan is cached and used for all parameter values. This can cause a query plan to be inefficient for some values of the parameter, also known as a parameter sensitive plan. | Parameterized queries can have multiple cached query plans for different selectivity categories of a parameter. Parameter sensitive plan optimization is enabled by default in compatibility level 160. For more information, see [PSP Optimization](../../relational-databases/performance/intelligent-query-processing-details.md#parameter-sensitivity-plan-optimization). |
| Cardinality estimation uses only one default set of model assumptions about the underlying data distribution and usage patterns for all databases and queries. The only way to change or adjust any one of those assumptions is when the user undertakes a manual process to explicitly indicate which model assumptions should be used, by using query hints. No internal adjustment can be made to this default model after a query plan is generated. | Cardinality estimation starts with the default set of model assumptions about the underlying data distribution and usage patterns, but after some executions for a given query, the Database Engine learns which different sets of model assumptions might yield more accurate estimates, and therefore adjusts the assumptions in use to better match the data set being queried. CE Feedback is enabled by default in compatibility level 160. For more information, see [CE Feedback](../../relational-databases/performance/intelligent-query-processing-feedback.md#cardinality-estimation-ce-feedback). |
| No automatic determination of the optimal degree of parallelism is attempted by the Database Engine. For information on manually controlling the maximum degree of parallelism (MAXDOP) at the instance, database, query, or workload levels, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md#configure-the-max-degree-of-parallelism-server-configuration-option) | Degree of parallelism (DOP) Feedback improves query performance by identifying parallelism inefficiencies for repeating queries, based on elapsed time and waits. If parallelism usage is deemed inefficient, DOP Feedback will lower the DOP for the next execution of the query, from whatever is the configured DOP, and verify if it helps. DOP Feedback isn't enabled by default. To enable DOP Feedback, enable the `DOP_FEEDBACK` database scoped configuration in a database. For more information, see [DOP Feedback](../../relational-databases/performance/intelligent-query-processing-feedback.md#degree-of-parallelism-dop-feedback). |

## Differences between compatibility level 140 and level 150

This section describes new behaviors introduced with compatibility level 150.

| Compatibility level setting of 140 or lower | Compatibility level setting of 150 |
| --- | --- |
| Relational data warehouse and analytic workloads may not be able to use columnstore indexes due to OLTP-overhead, lack of vendor support or other limitations. Without columnstore indexes, these workloads can't benefit from batch execution mode. | Batch execution mode is now available for analytic workloads without requiring columnstore indexes. For more information, see [batch mode on rowstore.](../../relational-databases/performance/intelligent-query-processing-details.md#batch-mode-on-rowstore) |
| Row-mode queries that request insufficient memory grant sizes that result in spills to disk may continue to have issues on consecutive executions. | Row-mode queries that request insufficient memory grant sizes that result in spills to disk may have improved performance on consecutive executions. For more information, see [row mode memory grant feedback](../../relational-databases/performance/intelligent-query-processing-feedback.md#row-mode-memory-grant-feedback). |
| Row-mode queries that request an excessive memory grant size that results in concurrency issues may continue to have issues on consecutive executions. | Row-mode queries that request an excessive memory grant size that results in concurrency issues may have improved concurrency on consecutive executions. For more information, see [row mode memory grant feedback](../../relational-databases/performance/intelligent-query-processing-feedback.md#row-mode-memory-grant-feedback). |
| Queries referencing T-SQL scalar UDFs will use iterative invocation, lack costing and force serial execution. | T-SQL scalar UDFs are transformed into equivalent relational expressions that are "inlined" into the calling query, often resulting in significant performance gains. For more information, see [T-SQL scalar UDF inlining.](../../relational-databases/performance/intelligent-query-processing-details.md#scalar-udf-inlining) |
| Table variables use a fixed guess for the cardinality estimate. If the actual number of rows is much higher than the guessed value, performance of downstream operations can suffer. | New plans will use the actual cardinality of the table variable encountered on first compilation instead of a fixed guess. For more information, see [table variable deferred compilation.](../../relational-databases/performance/intelligent-query-processing-details.md#table-variable-deferred-compilation) |

For more information on query processing features enabled in database compatibility level 150, refer to [What's new in SQL Server 2019](../../sql-server/what-s-new-in-sql-server-2019.md) and [Intelligent query processing in SQL databases](../../relational-databases/performance/intelligent-query-processing.md).

## Differences between compatibility level 130 and level 140

This section describes new behaviors introduced with compatibility level 140.

| Compatibility level setting of 130 or lower | Compatibility level setting of 140 |
| --- | --- |
| Cardinality estimates for statements referencing multi-statement table-valued functions use a fixed row guess. | Cardinality estimates for eligible statements referencing multi-statement table-valued functions will use the actual cardinality of the function output. This is enabled via **interleaved execution** for multi-statement table-valued functions. |
| Batch-mode queries that request insufficient memory grant sizes that result in spills to disk may continue to have issues on consecutive executions. | Batch-mode queries that request insufficient memory grant sizes that result in spills to disk may have improved performance on consecutive executions. This is enabled via **batch mode memory grant feedback** which will update the memory grant size of a cached plan if spills have occurred for batch mode operators. |
| Batch-mode queries that request an excessive memory grant size that results in concurrency issues may continue to have issues on consecutive executions. | Batch-mode queries that request an excessive memory grant size that results in concurrency issues may have improved concurrency on consecutive executions. This is enabled via **batch mode memory grant feedback** which will update the memory grant size of a cached plan if an excessive amount was originally requested. |
| Batch-mode queries that contain join operators are eligible for three physical join algorithms, including nested loop, hash join and merge join. If cardinality estimates are incorrect for join inputs, an inappropriate join algorithm may be selected. If this occurs, performance will suffer, and the inappropriate join algorithm will remain in use until the cached plan is recompiled. | There is an additional join operator called **adaptive join**. If cardinality estimates are incorrect for the outer build join input, an inappropriate join algorithm may be selected. If this occurs and the statement is eligible for an adaptive join, a nested loop will be used for smaller join inputs, and a hash join will be used for larger join inputs dynamically without requiring recompilation. |
| Trivial plans referencing Columnstore indexes aren't eligible for batch mode execution. | A trivial plan referencing Columnstore indexes will be discarded in favor of a plan that is eligible for batch mode execution. |
| The `sp_execute_external_script` UDX operator can only run in row mode. | The `sp_execute_external_script` UDX operator is eligible for batch mode execution. |
| Multi-statement table-valued functions (TVFs) don't have interleaved execution | Interleaved execution for multi-statement TVFs to improve plan quality. |

Fixes that were under trace flag 4199 in earlier versions of SQL Server prior to SQL Server 2017 are now enabled by default. With compatibility mode 140. Trace flag 4199 will still be applicable for new query optimizer fixes that are released after SQL Server 2017. For information about Trace Flag 4199, see [Trace Flag 4199](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf4199).

## Differences between compatibility level 120 and level 130

This section describes new behaviors introduced with compatibility level 130.

| Compatibility level setting of 120 or lower | Compatibility level setting of 130 |
| --- | --- |
| The INSERT in an INSERT-SELECT statement is single-threaded. | The INSERT in an INSERT-SELECT statement is multi-threaded or can have a parallel plan. |
| Queries on a memory-optimized table execute single-threaded. | Queries on a memory-optimized table can now have parallel plans. |
| Introduced the SQL 2014 Cardinality estimator **CardinalityEstimationModelVersion="120"** | Further cardinality estimation (CE) Improvements with the Cardinality Estimation Model 130, which is visible from a Query plan. **CardinalityEstimationModelVersion="130"** |
| Batch mode versus Row Mode changes with Columnstore indexes:<br /><ul><li>Sorts on a table with Columnstore index are in Row mode <li>Windowing function aggregates operate in row mode such as `LAG` or `LEAD` <li>Queries on Columnstore tables with Multiple distinct clauses operated in Row mode <li>Queries running under MAXDOP 1 or with a serial plan executed in Row mode</li></ul> | Batch mode versus Row Mode changes with Columnstore indexes:<br /><ul><li>Sorts on a table with a Columnstore index are now in batch mode <li>Windowing aggregates now operate in batch mode such as `LAG` or `LEAD` <li>Queries on Columnstore tables with Multiple distinct clauses operate in Batch mode <li>Queries running under MAXDOP 1 or with a serial plan execute in Batch Mode</li></ul> |
| Statistics can be automatically updated. | The logic that automatically updates statistics is more aggressive on large tables. In practice, this should reduce cases where customers have seen performance issues on queries where newly inserted rows are queried frequently but where the statistics hadn't been updated to include those values. |
| Trace 2371 is OFF by default in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]. | [Trace 2371](/archive/blogs/psssql/default-auto-statistics-update-threshold-change-for-sql-server-2016) is ON by default in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]. Trace flag 2371 tells the auto statistics updater to sample a smaller yet wiser subset of rows, in a table that has a great many rows.<br /><br />One improvement is to include in the sample more rows that were inserted recently.<br /><br />Another improvement is to let queries run while the update statistics process is running, rather than blocking the query. |
| For level 120, statistics are sampled by a single-threaded process. | For level 130, statistics are sampled by a multi-threaded process (parallel process). |
| 253 incoming foreign keys is the limit. | A given table can be referenced by up to 10,000 incoming foreign keys or similar references. For restrictions, see [Create Foreign Key Relationships](../../relational-databases/tables/create-foreign-key-relationships.md). |
| The deprecated MD2, MD4, MD5, SHA, and SHA1 hash algorithms are permitted. | Only SHA2_256 and SHA2_512 hash algorithms are permitted. |
| | [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] includes improvements in some data types conversions and some (mostly uncommon) operations. For details see [SQL Server 2016 improvements in handling some data types and uncommon operations](https://support.microsoft.com/help/4010261/sql-server-2016-improvements-in-handling-some-data-types-and-uncommon). |
| The `STRING_SPLIT` function isn't available. | The `STRING_SPLIT` function is available under compatibility level 130 or above. If your database compatibility level is lower than 130, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] won't be able to find and execute `STRING_SPLIT` function. |

Fixes that were under trace flag 4199 in earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] prior to [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] are now enabled by default. With compatibility mode 130. Trace flag 4199 will still be applicable for new query optimizer fixes that are released after [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]. To use the older query optimizer in [!INCLUDE[ssSDS](../../includes/sssds-md.md)] you must select compatibility level 110. For information about Trace Flag 4199, see [Trace Flag 4199](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf4199).

## Differences between lower compatibility levels and level 120

This section describes new behaviors introduced with compatibility level 120.

| Compatibility level setting of 110 or lower | Compatibility level setting of 120 |
| --- | --- |
| The older query optimizer is used. | [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] includes substantial improvements to the component that creates and optimizes query plans. This new query optimizer feature is dependent upon use of the database compatibility level 120. New database applications should be developed using database compatibility level 120 to take advantage of these improvements. Applications that are migrated from earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should be carefully tested to confirm that good performance is maintained or improved. If performance degrades, you can set the database compatibility level to 110 or earlier to use the older query optimizer methodology.<br /><br />Database compatibility level 120 uses a new cardinality estimator that is tuned for modern data warehousing and OLTP workloads. Before setting database compatibility level to 110 because of performance issues, see the recommendations in the *Query Plans* section of the [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] [What's New in Database Engine](../../sql-server/what-s-new-in-sql-server-2016.md) article. |
| In compatibility levels lower than 120, the language setting is ignored when converting a **date** value to a string value. This behavior is specific only to the **date** type. See example B in the Examples section below. | The language setting isn't ignored when converting a **date** value to a string value. |
| Recursive references on the right-hand side of an `EXCEPT` clause create an infinite loop. Example C in the Examples section below demonstrates this behavior. | Recursive references in an `EXCEPT` clause generates an error in compliance with the ANSI SQL standard. |
| Recursive common table expression (CTE) allows duplicate column names. | Recursive CTE doesn't allow duplicate column names. |
| Disabled triggers are enabled if the triggers are altered. | Altering a trigger doesn't change the state (enabled or disabled) of the trigger. |
| The OUTPUT INTO table clause ignores the `IDENTITY_INSERT SETTING = OFF` and allows explicit values to be inserted. | You can't insert explicit values for an identity column in a table when `IDENTITY_INSERT` is set to OFF. |
| When the database containment is set to partial, validating the `$action` field in the `OUTPUT` clause of a `MERGE` statement can return a collation error. | The collation of the values returned by the `$action` clause of a `MERGE` statement is the database collation instead of the server collation and a collation conflict error isn't returned. |
| A `SELECT INTO` statement always creates a single-threaded insert operation. | A `SELECT INTO` statement can create a parallel insert operation. When inserting a large number of rows, the parallel operation can improve performance. |

## Differences between lower compatibility levels and levels 100 and 110

This section describes new behaviors introduced with compatibility level 110. This section also applies to compatibility levels above 110.

| Compatibility level setting of 100 or lower | Compatibility level setting of at least 110 |
| --- | --- |
| Common language runtime (CLR) database objects are executed with version 4 of the CLR. However, some behavior changes introduced in version 4 of the CLR are avoided. For more information, see [What's New in CLR Integration](../../relational-databases/clr-integration/clr-integration-what-s-new.md). | CLR database objects are executed with version 4 of the CLR. |
| The XQuery functions **string-length** and **substring** count each surrogate as two characters. | The XQuery functions **string-length** and **substring** count each surrogate as one character. |
| `PIVOT` is allowed in a recursive common table expression (CTE) query. However, the query returns incorrect results when there are multiple rows per grouping. | `PIVOT` isn't allowed in a recursive common table expression (CTE) query. An error is returned. |
| The RC4 algorithm is only supported for backward compatibility. New material can only be encrypted using RC4 or RC4_128 when the database is in compatibility level 90 or 100. (Not recommended.) In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], material encrypted using RC4 or RC4_128 can be decrypted in any compatibility level. | New material can't be encrypted using RC4 or RC4_128. Use a newer algorithm such as one of the AES algorithms instead. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], material encrypted using RC4 or RC4_128 can be decrypted in any compatibility level. |
| The default style for `CAST` and `CONVERT` operations on **time** and **datetime2** data types is 121 except when either type is used in a computed column expression. For computed columns, the default style is 0. This behavior impacts computed columns when they are created, used in queries involving auto-parameterization, or used in constraint definitions.<br /><br />Example D in the Examples section below shows the difference between styles 0 and 121. It doesn't demonstrate the behavior described above. For more information about date and time styles, see [CAST and CONVERT](../../t-sql/functions/cast-and-convert-transact-sql.md). | Under compatibility level 110, the default style for `CAST` and `CONVERT` operations on **time** and **datetime2** data types is always 121. If your query relies on the old behavior, use a compatibility level less than 110, or explicitly specify the 0 style in the affected query.<br /><br />Upgrading the database to compatibility level 110 won't change user data that has been stored to disk. You must manually correct this data as appropriate. For example, if you used `SELECT INTO` to create a table from a source that contained a computed column expression described above, the data (using style 0) would be stored rather than the computed column definition itself. You would need to manually update this data to match style 121. |
| The [+ (Addition)](../../t-sql/language-elements/add-transact-sql.md) operator may be applied to an operand of type **date**, **time**, **datetime2**, or **datetimeoffset** if the other operand has type **datetime** or **smalldatetime**. | Attempting to apply the addition operator to an operand of type **date**, **time**, **datetime2**, or **datetimeoffset** and an operand of type **datetime** or **smalldatetime** will cause error 402. |
| Any columns in remote tables of type **smalldatetime** that are referenced in a partitioned view are mapped as **datetime**. Corresponding columns in local tables (in the same ordinal position in the select list) must be of type **datetime**. | Any columns in remote tables of type **smalldatetime** that are referenced in a partitioned view are mapped as **smalldatetime**. Corresponding columns in local tables (in the same ordinal position in the select list) must be of type **smalldatetime**.<br /><br />After upgrading to 110, the distributed partitioned view will fail because of the data type mismatch. You can resolve this by changing the data type on the remote table to **datetime** or setting the compatibility level of the local database to 100 or lower. |
| `SOUNDEX` function implements the following rules:<br /><br />1) Upper-case H or upper-case W is ignored when separating two consonants that have the same number in the `SOUNDEX` code.<br /><br />2) If the first two characters of *character_expression* have the same number in the `SOUNDEX` code, both characters are included. Else, if a set of side-by-side consonants have the same number in the `SOUNDEX` code, all of them are excluded except the first. | `SOUNDEX` function implements the following rules:<br /><br />1) If upper-case H or upper-case W separate two consonants that have the same number in the `SOUNDEX` code, the consonant to the right is ignored<br /><br />2) If a set of side-by-side consonants have the same number in the `SOUNDEX` code, all of them are excluded except the first.<br /><br />The additional rules may cause the values computed by the `SOUNDEX` function to be different than the values computed under earlier compatibility levels. After upgrading to compatibility level 110, you may need to rebuild the indexes, heaps, or CHECK constraints that use the `SOUNDEX` function. For more information, see [SOUNDEX](../../t-sql/functions/soundex-transact-sql.md). |
| `STRING_AGG` is available without an `<order_clause>`. | `STRING_AGG` is available with an optional `<order_clause>`. For more information, see [STRING_AGG](../functions/string-agg-transact-sql.md) |

## Differences between compatibility level 90 and level 100

This section describes new behaviors introduced with compatibility level 100.

| Compatibility level setting of 90 | Compatibility level setting of 100 | Possibility of impact |
| --- | --- | --- |
| The QUOTED_IDENTIFER setting is always set to ON for multistatement table-valued functions when they are created regardless of the session level setting. | The QUOTED IDENTIFIER session setting is honored when multistatement table-valued functions are created. | Medium |
| When you create or alter a partition function, **datetime** and **smalldatetime** literals in the function are evaluated assuming US_English as the language setting. | The current language setting is used to evaluate **datetime** and **smalldatetime** literals in the partition function. | Medium |
| The `FOR BROWSE` clause is allowed (and ignored) in `INSERT` and `SELECT INTO` statements. | The `FOR BROWSE` clause isn't allowed in `INSERT` and `SELECT INTO` statements. | Medium |
| Full-text predicates are allowed in the `OUTPUT` clause. | Full-text predicates aren't allowed in the `OUTPUT` clause. | Low |
| `CREATE FULLTEXT STOPLIST`, `ALTER FULLTEXT STOPLIST`, and `DROP FULLTEXT STOPLIST` aren't supported. The system stoplist is automatically associated with new full-text indexes. | `CREATE FULLTEXT STOPLIST`, `ALTER FULLTEXT STOPLIST`, and `DROP FULLTEXT STOPLIST` are supported. | Low |
| `MERGE` isn't enforced as a reserved keyword. | MERGE is a fully reserved keyword. The `MERGE` statement is supported under both 100 and 90 compatibility levels. | Low |
| Using the \<dml_table_source> argument of the INSERT statement raises a syntax error. | You can capture the results of an OUTPUT clause in a nested INSERT, UPDATE, DELETE, or MERGE statement, and insert those results into a target table or view. This is done using the \<dml_table_source> argument of the INSERT statement. | Low |
| Unless `NOINDEX` is specified, `DBCC CHECKDB` or `DBCC CHECKTABLE` performs both physical and logical consistency checks on a single table or indexed view and on all its nonclustered and XML indexes. Spatial indexes aren't supported. | Unless `NOINDEX` is specified, `DBCC CHECKDB` or `DBCC CHECKTABLE` performs both physical and logical consistency checks on a single table and on all its nonclustered indexes. However, on XML indexes, spatial indexes, and indexed views, only physical consistency checks are performed by default.<br /><br />If `WITH EXTENDED_LOGICAL_CHECKS` is specified, logical checks are performed on indexed views, XML indexes, and spatial indexes, where present. By default, physical consistency checks are performed before the logical consistency checks. If `NOINDEX` is also specified, only the logical checks are performed. | Low |
| When an OUTPUT clause is used with a data manipulation language (DML) statement and a run-time error occurs during statement execution, the entire transaction is terminated and rolled back. | When an `OUTPUT` clause is used with a data manipulation language (DML) statement and a run-time error occurs during statement execution, the behavior depends on the `SET XACT_ABORT` setting. If `SET XACT_ABORT` is OFF, a statement abort error generated by the DML statement using the `OUTPUT` clause will terminate the statement, but the execution of the batch continues and the transaction isn't rolled back. If `SET XACT_ABORT` is ON, all run-time errors generated by the DML statement using the OUTPUT clause will terminate the batch, and the transaction is rolled back. | Low |
| CUBE and ROLLUP aren't enforced as reserved keywords. | `CUBE` and `ROLLUP` are reserved keywords within the GROUP BY clause. | Low |
| Strict validation is applied to elements of the XML **anyType** type. | Lax validation is applied to elements of the **anyType** type. For more information, see [Wildcard Components and Content Validation](../../relational-databases/xml/wildcard-components-and-content-validation.md). | Low |
| The special attributes **xsi:nil** and **xsi:type** can't be queried or modified by data manipulation language statements.<br /><br />This means that `/e/@xsi:nil` fails while `/e/@*` ignores the **xsi:nil** and **xsi:type** attributes. However, `/e` returns the **xsi:nil** and **xsi:type** attributes for consistency with `SELECT xmlCol`, even if `xsi:nil = "false"`. | The special attributes **xsi:nil** and **xsi:type** are stored as regular attributes and can be queried and modified.<br /><br />For example, executing the query `SELECT x.query('a/b/@*')` returns all attributes including **xsi:nil** and **xsi:type**. To exclude these types in the query, replace `@*` with `@*[namespace-uri(.) != "`*insert xsi namespace uri*`"` and not `(local-name(.) = "type"` or `local-name(.) ="nil".` | Low |
| A user-defined function that converts an XML constant string value to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] datetime type is marked as deterministic. | A user-defined function that converts an XML constant string value to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] datetime type is marked as non-deterministic. | Low |
| The XML union and list types aren't fully supported. | The union and list types are fully supported including the following functionality:<br /><br />Union of list<br /><br />Union of union<br /><br />List of atomic types<br /><br />List of union | Low |
| The SET options required for an xQuery method aren't validated when the method is contained in a view or inline table-valued function. | The SET options required for an xQuery method are validated when the method is contained in a view or inline table-valued function. An error is raised if the SET options of the method are set incorrectly. | Low |
| XML attribute values that contain end-of-line characters (carriage return and line feed) aren't normalized according to the XML standard. That is, both characters are returned instead of a single line-feed character. | XML attribute values that contain end-of-line characters (carriage return and line feed) are normalized according to the XML standard. That is, all line breaks in external parsed entities (including the document entity) are normalized on input by translating both the two-character sequence #xD #xA and any #xD that isn't followed by #xA to a single #xA character.<br /><br />Applications that use attributes to transport string values that contain end-of-line characters won't receive these characters back as they are submitted. To avoid the normalization process, use the XML numeric character entities to encode all end-of-line characters. | Low |
| The column properties `ROWGUIDCOL` and `IDENTITY` can be incorrectly named as a constraint. For example the statement `CREATE TABLE T (C1 int CONSTRAINT MyConstraint IDENTITY)` executes, but the constraint name isn't preserved and isn't accessible to the user. | The column properties `ROWGUIDCOL` and `IDENTITY` can't be named as a constraint. Error 156 is returned. | Low |
| Updating columns by using a two-way assignment such as `UPDATE T1 SET @v = column_name = <expression>` can produce unexpected results because the live value of the variable can be used in other clauses such as the `WHERE` and `ON` clause during statement execution instead of the statement starting value. This can cause the meanings of the predicates to change unpredictably on a per-row basis.<br /><br />This behavior is applicable only when the compatibility level is set to 90. | Updating columns by using a two-way assignment produces expected results because only the statement starting value of the column is accessed during statement execution. | Low |
| Variable assignment is allowed in a statement containing a top-level UNION operator, but returns unexpected results. Learn more in [example E](#e-variable-assignment---top-level-union-operator). | Variable assignment isn't allowed in a statement containing a top-level UNION operator. Error 10734 is returned. Find a suggested rewrite in [example E](#e-variable-assignment---top-level-union-operator). | Low |
| The ODBC function {fn CONVERT()} uses the default date format of the language. For some languages, the default format is YDM, which can result in conversion errors when CONVERT() is combined with other functions, such as `{fn CURDATE()}`, that expect a YMD format. | The ODBC function `{fn CONVERT()}` uses style 121 (a language-independent YMD format) when converting to the ODBC data types SQL_TIMESTAMP, SQL_DATE, SQL_TIME, SQLDATE, SQL_TYPE_TIME, and SQL_TYPE_TIMESTAMP. | Low |
| Datetime intrinsics such as DATEPART don't require string input values to be valid datetime literals. For example, `SELECT DATEPART (year, '2007/05-30')` compiles successfully. | Datetime intrinsics such as `DATEPART` require string input values to be valid datetime literals. Error 241 is returned when an invalid datetime literal is used. | Low |
| Trailing spaces specified in the first input parameter to the REPLACE function are trimmed when the parameter is of type char. For example, in the statement SELECT '<' + REPLACE(CONVERT(char(6), 'ABC '), ' ', 'L') + '>', the value 'ABC ' is incorrectly evaluated as 'ABC'. | Trailing spaces are always preserved. For applications that rely on the previous behavior of the function, use the RTRIM function when specifying the first input parameter for the function. For example, the following syntax will reproduce the SQL Server 2005 behavior SELECT '<' + REPLACE(RTRIM(CONVERT(char(6), 'ABC ')), ' ', 'L') + '>'. | Low |

## Reserved keywords

The compatibility setting also determines the keywords that are reserved by the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. The following table shows the reserved keywords that are introduced by each of the compatibility levels.

| Compatibility level setting | Reserved keywords |
| --- | --- |
| 130 | To be determined. |
| 120 | None. |
| 110 | `WITHIN GROUP`, `TRY_CONVERT`, `SEMANTICKEYPHRASETABLE`, `SEMANTICSIMILARITYDETAILSTABLE`, `SEMANTICSIMILARITYTABLE` |
| 100 | `CUBE`, `MERGE`, `ROLLUP` |
| 90 | `EXTERNAL`, `PIVOT`, `UNPIVOT`, `REVERT`, `TABLESAMPLE` |

At a given compatibility level, the reserved keywords include all of the keywords introduced at or below that level. Thus, for instance, for applications at level 110, all of the keywords listed in the preceding table are reserved. At the lower compatibility levels, level-100 keywords remain valid object names, but the level-110 language features corresponding to those keywords are unavailable.

Once introduced, a keyword remains reserved. For example, the reserved keyword PIVOT, which was introduced in compatibility level 90, is also reserved in levels 100, 110, and 120.

If an application uses an identifier that is reserved as a keyword for its compatibility level, the application will fail. To work around this, enclose the identifier between either brackets (**[]**) or quotation marks (**""**); for example, to upgrade an application that uses the identifier `EXTERNAL` to compatibility level 90, you could change the identifier to either `[EXTERNAL]` or `"EXTERNAL"`.

For more information, see [Reserved Keywords](../../t-sql/language-elements/reserved-keywords-transact-sql.md).

## Permissions

Requires `ALTER` permission on the database.

## Examples

### A. Change the compatibility level

The following example changes the compatibility level of the **AdventureWorks2019** [sample database](../../samples/adventureworks-install-configure.md) database to 150, the default for [!INCLUDE[ssSQL19](../../includes/sssql19-md.md)].

```sql
ALTER DATABASE AdventureWorks2019
SET COMPATIBILITY_LEVEL = 150;
GO
```

The following example returns the compatibility level of the current database.

```sql
SELECT name, compatibility_level
FROM sys.databases
WHERE name = db_name();
GO
```

### B. Ignore the SET LANGUAGE statement except under compatibility level 120 or higher

The following query ignores the `SET LANGUAGE` statement except under compatibility level 120 or higher.

```sql
SET DATEFORMAT dmy;
DECLARE @t2 date = '12/5/2011' ;
SET LANGUAGE dutch;
SELECT CONVERT(varchar(11), @t2, 106);
GO
```

Results when the compatibility level is less than 120: `12 May 2011`

Results when the compatibility level is set to 120 or higher: `12 mei 2011`

### C. For compatibility-level setting of 110 or lower, recursive references on the right-hand side of an EXCEPT clause create an infinite loop

```sql
WITH cte AS
    (SELECT * FROM (VALUES (1),(2),(3)) v (a)),
r AS
    (SELECT a FROM cte
    UNION ALL
    (SELECT a FROM cte EXCEPT SELECT a FROM r)
)
SELECT a
FROM r;
GO
```

### D. The difference between styles 0 and 121

When the compatibility level is lower than 110, the default style for `CAST` and `CONVERT` operations on **time** and **datetime2** data types is 121 except when either type is used in a computed column expression. For computed columns, the default style is 0.

When the compatibility level is 110 or higher, the default style for `CAST` and `CONVERT` operations on **time** and **datetime2** data types is always 121. See [Differences between lower compatibility levels and levels 100 and 110](#differences-between-lower-compatibility-levels-and-levels-100-and-110) for more information.

For more information about date and time styles, see [CAST and CONVERT](../../t-sql/functions/cast-and-convert-transact-sql.md).

```sql
DROP TABLE IF EXISTS t1;
GO

CREATE TABLE t1 (c1 time(7), c2 datetime2);
GO

INSERT t1 (c1,c2) VALUES (GETDATE(), GETDATE());
GO

SELECT CONVERT(nvarchar(16),c1,0) AS TimeStyle0
       ,CONVERT(nvarchar(16),c1,121)AS TimeStyle121
       ,CONVERT(nvarchar(32),c2,0) AS Datetime2Style0
       ,CONVERT(nvarchar(32),c2,121)AS Datetime2Style121
FROM t1;
GO
```

This returns results such as the following:

| TimeStyle0 | TimeStyle121 | Datetime2Style0 | Datetime2Style121 |
| --- | --- | --- | --- |
| 3:15PM | 15:15:35.8100000 | Jun 7 2011 3:15PM | 2011-06-07 15:15:35.8130000 |

### E. Variable assignment - top-level UNION operator

Under the database compatibility level setting of 90, variable assignment is allowed in a statement containing a top-level UNION operator, but returns unexpected results. For example, in the following statements, local variable `@v` is assigned the value of the column `BusinessEntityID` from the union of two tables. By definition, when the SELECT statement returns more than one value, the variable is assigned the last value that is returned. In this case, the variable is correctly assigned the last value, however, the result set of the SELECT UNION statement is also returned.

```sql
ALTER DATABASE AdventureWorks2012
SET compatibility_level = 110;
GO
USE AdventureWorks2012;
GO
DECLARE @v int;
SELECT @v = BusinessEntityID FROM HumanResources.Employee
UNION ALL
SELECT @v = BusinessEntityID FROM HumanResources.EmployeeAddress;
SELECT @v;
```

Under the database compatibility level setting of 100 and higher, variable assignment isn't allowed in a statement containing a top-level UNION operator. Error 10734 is returned.

To resolve the error, rewrite the query as shown in the following example.

```sql
DECLARE @v int;
SELECT @v = BusinessEntityID FROM
    (SELECT BusinessEntityID FROM HumanResources.Employee
     UNION ALL
     SELECT BusinessEntityID FROM HumanResources.EmployeeAddress) AS Test;
SELECT @v;
```

## Next steps

For more information on database compatibility levels and related concepts, see the following articles:

- [Keep performance stability during the upgrade to newer SQL Server](../../relational-databases/performance/query-store-usage-scenarios.md#CEUpgrade)
- [Change the Database Compatibility Mode and use the Query Store](../../database-engine/install-windows/change-the-database-compatibility-mode-and-use-the-query-store.md)
- [Compatibility Certification](../../database-engine/install-windows/compatibility-certification.md)
- [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md)
- [Upgrading Databases by using the Query Tuning Assistant](../../relational-databases/performance/upgrade-dbcompat-using-qta.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [View or Change the Compatibility Level of a Database](../../relational-databases/databases/view-or-change-the-compatibility-level-of-a-database.md)
- [Query Store hints](../../relational-databases/performance/query-store-hints.md)
