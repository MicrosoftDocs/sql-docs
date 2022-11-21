---
title: "ALTER DATABASE SET Options (Transact-SQL)"
description: Learn how to set database options such as Automatic tuning, encryption, Query Store in SQL Server, and Azure SQL Database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/04/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: event-tier1-build-2022
helpviewer_keywords:
  - "online database state [SQL Server]"
  - "database options [SQL Server]"
  - "emergency database state [SQL Server]"
  - "databases [SQL Server], options"
  - "read-only databases"
  - "recovery models [SQL Server], switching"
  - "ALTER DATABASE statement, SET options"
  - "offline database state [SQL Server]"
  - "snapshot isolation framework option"
  - "checksums [SQL Server]"
  - "Automatic tuning"
  - " Data Retention Policy"
  - "query plan regression correction"
  - "auto_create_statistics"
  - "auto_update_statistics"
  - "Query Store options"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azure-sqldw-latest || =azuresqldb-mi-current"
---
# ALTER DATABASE SET options (Transact-SQL)

Sets database options in Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], and [!INCLUDE[ssSDW](../../includes/sssdw-md.md)]. For other ALTER DATABASE options, see [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md).

> [!NOTE]  
> Setting some options with ALTER DATABASE may require exclusive database access. If the ALTER DATABASE statement does not complete in a timely manner, check to see if other sessions within the database are blocking the ALTER DATABASE session.

For more information about the syntax conventions, see [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

## Select a product

In the following row, select whichever product name you're interested in. Doing that displays different content here on this webpage, appropriate for whichever product you select.

::: moniker range=">=sql-server-2016 || >=sql-server-linux-2017"

:::row:::
    :::column:::
        ***\* SQL Server \**** &nbsp;
    :::column-end:::
    :::column:::
        [SQL Database](alter-database-transact-sql-set-options.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed Instance](alter-database-transact-sql-set-options.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-database-transact-sql-set-options.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## SQL Server

Database mirroring, [!INCLUDE[ssHADR](../../includes/sshadr-md.md)], and compatibility levels are `SET` options but are described in separate articles because of their length. For more information, see [ALTER DATABASE Database Mirroring](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md), [ALTER DATABASE SET HADR](../../t-sql/statements/alter-database-transact-sql-set-hadr.md), and [ALTER DATABASE compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

Database scoped configurations are used to set several database configurations at the individual database level. For more information, see [ALTER DATABASE SCOPED CONFIGURATION](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).

> [!NOTE]  
> Many database set options can be configured for the current session by using [SET statements](../../t-sql/statements/set-statements-transact-sql.md) and are often configured by applications when they connect. Session-level set options override the `ALTER DATABASE SET` values. The database options described in the following sections are values that you can set for sessions that don't explicitly provide other set option values.

## Syntax

```syntaxsql
ALTER DATABASE { database_name | CURRENT }
SET
{
    <option_spec> [ ,...n ] [ WITH <termination> ]
}

<option_spec> ::=
{
    <accelerated_database_recovery>
  | <auto_option>
  | <automatic_tuning_option>
  | <change_tracking_option>
  | <containment_option>
  | <cursor_option>
  | <database_mirroring_option>
  | <date_correlation_optimization_option>
  | <db_encryption_option>
  | <db_state_option>
  | <db_update_option>
  | <db_user_access_option>
  | <delayed_durability_option>
  | <external_access_option>
  | FILESTREAM ( <FILESTREAM_option> )
  | <HADR_options>
  | <mixed_page_allocation_option>
  | <parameterization_option>
  | <query_store_options>
  | <recovery_option>
  | <remote_data_archive_option>
  | <service_broker_option>
  | <snapshot_option>
  | <sql_option>
  | <suspend_for_snapshot_backup>
  | <target_recovery_time_option>
  | <termination>
  | <temporal_history_retention>
  | <data_retention_policy>
}
;

<accelerated_database_recovery> ::=
{
    ACCELERATED_DATABASE_RECOVERY = { ON | OFF }
     [ ( PERSISTENT_VERSION_STORE_FILEGROUP = { filegroup name } ) ];
}

<auto_option> ::=
{
    AUTO_CLOSE { ON | OFF }
  | AUTO_CREATE_STATISTICS { OFF | ON [ ( INCREMENTAL = { ON | OFF } ) ] }
  | AUTO_SHRINK { ON | OFF }
  | AUTO_UPDATE_STATISTICS { ON | OFF }
  | AUTO_UPDATE_STATISTICS_ASYNC { ON | OFF }
}

<automatic_tuning_option> ::=
{
    AUTOMATIC_TUNING ( FORCE_LAST_GOOD_PLAN = { DEFAULT | ON | OFF } )
}

<change_tracking_option> ::=
{
    CHANGE_TRACKING
   {
       = OFF
     | = ON [ ( <change_tracking_option_list > [,...n] ) ]
     | ( <change_tracking_option_list> [,...n] )
   }
}

<change_tracking_option_list> ::=
{
   AUTO_CLEANUP = { ON | OFF }
 | CHANGE_RETENTION = retention_period { DAYS | HOURS | MINUTES }
}

<containment_option> ::=
   CONTAINMENT = { NONE | PARTIAL }

<cursor_option> ::=
{
    CURSOR_CLOSE_ON_COMMIT { ON | OFF }
  | CURSOR_DEFAULT { LOCAL | GLOBAL }
}

<database_mirroring_option>
  ALTER DATABASE Database Mirroring

<date_correlation_optimization_option> ::=
    DATE_CORRELATION_OPTIMIZATION { ON | OFF }

<db_encryption_option> ::=
    ENCRYPTION { ON | OFF | SUSPEND | RESUME }

<db_state_option> ::=
    { ONLINE | OFFLINE | EMERGENCY }

<db_update_option> ::=
    { READ_ONLY | READ_WRITE }

<db_user_access_option> ::=
    { SINGLE_USER | RESTRICTED_USER | MULTI_USER }

<delayed_durability_option> ::=
    DELAYED_DURABILITY = { DISABLED | ALLOWED | FORCED }

<external_access_option> ::=
{
    DB_CHAINING { ON | OFF }
  | TRUSTWORTHY { ON | OFF }
  | DEFAULT_FULLTEXT_LANGUAGE = { <lcid> | <language name> | <language alias> }
  | DEFAULT_LANGUAGE = { <lcid> | <language name> | <language alias> }
  | NESTED_TRIGGERS = { OFF | ON }
  | TRANSFORM_NOISE_WORDS = { OFF | ON }
  | TWO_DIGIT_YEAR_CUTOFF = { 1753, ..., 2049, ..., 9999 }
}

<FILESTREAM_option> ::=
{
    NON_TRANSACTED_ACCESS = { OFF | READ_ONLY | FULL
  | DIRECTORY_NAME = <directory_name>
}

<HADR_options> ::=
    ALTER DATABASE SET HADR

<mixed_page_allocation_option> ::=
    MIXED_PAGE_ALLOCATION { OFF | ON }

<parameterization_option> ::=
    PARAMETERIZATION { SIMPLE | FORCED }

<query_store_options> ::=
{
    QUERY_STORE
    {
          = OFF [ ( FORCED ) ]
        | = ON [ ( <query_store_option_list> [,...n] ) ]
        | ( < query_store_option_list> [,...n] )
        | CLEAR [ ALL ]
    }
}

<query_store_option_list> ::=
{
      OPERATION_MODE = { READ_WRITE | READ_ONLY }
    | CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = number )
    | DATA_FLUSH_INTERVAL_SECONDS = number
    | MAX_STORAGE_SIZE_MB = number
    | INTERVAL_LENGTH_MINUTES = number
    | SIZE_BASED_CLEANUP_MODE = { AUTO | OFF }
    | QUERY_CAPTURE_MODE = { ALL | AUTO | CUSTOM | NONE }
    | MAX_PLANS_PER_QUERY = number
    | WAIT_STATS_CAPTURE_MODE = { ON | OFF }
    | QUERY_CAPTURE_POLICY = ( <query_capture_policy_option_list> [,...n] )
}

<query_capture_policy_option_list> :: =
{
      STALE_CAPTURE_POLICY_THRESHOLD = number { DAYS | HOURS }
    | EXECUTION_COUNT = number
    | TOTAL_COMPILE_CPU_TIME_MS = number
    | TOTAL_EXECUTION_CPU_TIME_MS = number
}

<recovery_option> ::=
{
    RECOVERY { FULL | BULK_LOGGED | SIMPLE }
  | TORN_PAGE_DETECTION { ON | OFF }
  | PAGE_VERIFY { CHECKSUM | TORN_PAGE_DETECTION | NONE }
}

<remote_data_archive_option> ::=
{
    REMOTE_DATA_ARCHIVE =
    {
        ON ( SERVER = <server_name>,
             {
                  CREDENTIAL = <db_scoped_credential_name>
                  | FEDERATED_SERVICE_ACCOUNT = ON | OFF
             }
        )
        | OFF
    }
}

<service_broker_option> ::=
{
    ENABLE_BROKER
  | DISABLE_BROKER
  | NEW_BROKER
  | ERROR_BROKER_CONVERSATIONS
  | HONOR_BROKER_PRIORITY { ON | OFF }
}

<snapshot_option> ::=
{
    ALLOW_SNAPSHOT_ISOLATION { ON | OFF }
  | READ_COMMITTED_SNAPSHOT { ON | OFF }
  | MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = { ON | OFF }
}
<sql_option> ::=
{
    ANSI_NULL_DEFAULT { ON | OFF }
  | ANSI_NULLS { ON | OFF }
  | ANSI_PADDING { ON | OFF }
  | ANSI_WARNINGS { ON | OFF }
  | ARITHABORT { ON | OFF }
  | COMPATIBILITY_LEVEL = { 160 | 150 | 140 | 130 | 120 | 110 | 100 }
  | CONCAT_NULL_YIELDS_NULL { ON | OFF }
  | NUMERIC_ROUNDABORT { ON | OFF }
  | QUOTED_IDENTIFIER { ON | OFF }
  | RECURSIVE_TRIGGERS { ON | OFF }
}

<suspend_for_snapshot_backup> ::=
    SET SUSPEND_FOR_SNAPSHOT_BACKUP = { ON | OFF } [ ( MODE = COPY_ONLY ) ]

<target_recovery_time_option> ::=
    TARGET_RECOVERY_TIME = target_recovery_time { SECONDS | MINUTES }

<termination>::=
{
    ROLLBACK AFTER number [ SECONDS ]
  | ROLLBACK IMMEDIATE
  | NO_WAIT
}

<temporal_history_retention> ::=
    TEMPORAL_HISTORY_RETENTION { ON | OFF }

<data_retention_policy> ::=
    DATA_RETENTION { ON | OFF }
```

## Arguments

#### *database_name*

The name of the database to be modified.

#### CURRENT

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)])

Runs the action in the current database. `CURRENT` isn't supported for all options in all contexts. If `CURRENT` fails, provide the database name.

#### \<accelerated_database_recovery> ::=

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)])

Enables [accelerated database recovery](../../relational-databases/accelerated-database-recovery-management.md) (ADR) per-database. ADR is set to OFF by default in [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]. By using this syntax, you can designate a specific file group for the Persistent Version Store (PVS) data. If no file group is specified, the PVS will be stored in the PRIMARY file group. For examples and more information, see [Accelerated database recovery](../../relational-databases/accelerated-database-recovery-management.md).

#### \<auto_option> ::=

Controls automatic options.

#### <a id="auto_close"></a> AUTO_CLOSE { ON | OFF }

- ON

  The database is shut down cleanly and its resources are freed after the last user exits.

  The database automatically reopens when a user tries to use the database again. For example, this re-open behavior occurs when a user issues a `USE database_name` statement. The database may shut down cleanly with AUTO_CLOSE set to ON. If so, the database doesn't re-open until a user tries to use the database the next time the [!INCLUDE[ssDE](../../includes/ssde-md.md)] restarts.

  After a database is shut down, the next time an application attempts to use the database, the database must first be opened, and then the status changed to online. This might take some time and can result in application timeouts.

- OFF

  The database remains open after the last user exits.

  The AUTO_CLOSE option is useful for desktop databases because it allows for database files to be managed as regular files. They can be moved, copied to make backups, or even emailed to other users. The AUTO_CLOSE process is asynchronous; repeatedly opening and closing the database doesn't reduce performance.

> [!NOTE]  
> The AUTO_CLOSE option isn't available in a contained database or on [!INCLUDE[ssSDS](../../includes/sssds-md.md)].
> You can determine this option's status by examining the `is_auto_close_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or the `IsAutoClose` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.
>  
> When AUTO_CLOSE is set to ON, some columns in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view and the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function will return NULL because the database is unavailable to retrieve the data. To resolve this issue, run a USE statement to open the database.
>  
> Database mirroring requires AUTO_CLOSE set to OFF.

When the database is set to `AUTOCLOSE = ON`, an operation that initiates an automatic database shutdown clears the plan cache for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Clearing the plan cache causes a recompilation of all subsequent execution plans and can cause a sudden, temporary decrease in query performance. Starting with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 2, for each cleared cache store in the plan cache, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log contains the following informational message: `SQL Server has encountered %d occurrence(s) of cachestore flush for the '%s' cachestore (part of plan cache) due to some database maintenance or reconfigure operations`. This message is logged every five minutes as long as the cache is flushed within that time interval.

The AUTO_CLOSE setting can be a useful feature in some rare situations, for example, in an SQL Server instance without enough memory to operate stably with a large number of databases, or for a legacy 32-bit SQL Server instance with a large number of databases. In such scenarios, it may be useful to enable AUTO_CLOSE and conserve the memory resources required to keep a database open when there is no application using the database. When the database is open, some default memory allocations are required (for example, internal structures to represent various database metadata objects and transaction log buffers).

#### <a id="auto_create_statistics"></a> AUTO_CREATE_STATISTICS { ON | OFF }

- ON

  The Query Optimizer creates statistics on single columns in query predicates, as necessary, to improve query plans and query performance. These single-column statistics are created when Query Optimizer compiles queries. The single-column statistics are created only on columns that aren't already the first column of an existing statistics object.

  The default setting is ON. We recommend that you use the default setting for most databases.

- OFF

  The Query Optimizer doesn't create statistics on single columns in query predicates when it's compiling queries. Setting this option to OFF can cause suboptimal query plans and degraded query performance.

You can determine this option's status by examining the `is_auto_create_stats_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAutoCreateStatistics` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

For more information, see the section "Using the Database-wide statistics options" in [Statistics](../../relational-databases/statistics/statistics.md).

#### INCREMENTAL = ON | OFF

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

Set AUTO_CREATE_STATISTICS to ON, and set INCREMENTAL to ON. This sets automatically created stats as incremental whenever incremental stats are supported. The default value is OFF. For more information, see [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md).

#### <a id="auto_shrink"></a> AUTO_SHRINK { ON | OFF }

- ON

  The database files are candidates for periodic shrinking. Unless you have a specific requirement, do not set the AUTO_SHRINK database option to ON. For more information, see [Shrink a database](../../relational-databases/databases/shrink-a-database.md).

  Both data files and log files can be automatically shrunk. AUTO_SHRINK reduces the size of the transaction log only if you set the database to SIMPLE recovery model or if you back up the log. When you set AUTO_SHRINK to OFF, the database files aren't automatically shrunk during periodic checks for unused space.

  The AUTO_SHRINK option shrinks files when more than 25 percent of the file contains unused space. It shrinks the file to one of two sizes (whichever is larger):

  - The size at which 25 percent of the file is unused space
  - The size of the file when it was created

  You can't shrink a read-only database.

- OFF

  The database files are not automatically shrunk during periodic checks for unused space.

You can determine this option's status by examining the `is_auto_shrink_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAutoShrink` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

> [!NOTE]  
> The AUTO_SHRINK option isn't available in a Contained Database.

#### <a id="auto_update_statistics"></a> AUTO_UPDATE_STATISTICS { ON | OFF }

- ON

  Specifies that Query Optimizer updates statistics when they're used by a query and when they might be out-of-date. Statistics become out-of-date after insert, update, delete, or merge operations change the data distribution in the table or indexed view. Query Optimizer determines when statistics might be out-of-date by counting the number of data modifications since the last statistics update and comparing the number of modifications to a threshold. The threshold is based on the number of rows in the table or indexed view.

  Query Optimizer checks for out-of-date statistics before it compiles a query and runs a cached query plan. Query Optimizer uses the columns, tables, and indexed views in the query predicate to determine which statistics might be out-of-date. Query Optimizer determines this information before it compiles a query. Before running a cached query plan, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] verifies that the query plan references up-to-date statistics.

  The AUTO_UPDATE_STATISTICS option applies to statistics created for indexes, single-columns in query predicates, and statistics that are created by using the CREATE STATISTICS statement. This option also applies to filtered statistics.

  The default is ON. We recommend that you use the default setting for most databases.

  Use the AUTO_UPDATE_STATISTICS_ASYNC option to specify whether the statistics are updated synchronously or asynchronously.

- OFF

  Specifies that Query Optimizer doesn't update statistics when they're used by a query. Query Optimizer also doesn't update statistics when they might be out-of-date. Setting this option to OFF can cause suboptimal query plans and degraded query performance.

You can determine this option's status by examining the `is_auto_update_stats_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAutoUpdateStatistics` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

For more information, see the section "Using the Database-wide statistics options" in [Statistics](../../relational-databases/statistics/statistics.md).

#### <a id="auto_update_statistics_async"></a> AUTO_UPDATE_STATISTICS_ASYNC { ON | OFF }

- ON

  Specifies that statistics updates for the AUTO_UPDATE_STATISTICS option are asynchronous. Query Optimizer doesn't wait for statistics updates to complete before it compiles queries.

  Setting this option to ON has no effect unless AUTO_UPDATE_STATISTICS is set to ON.

  By default, the AUTO_UPDATE_STATISTICS_ASYNC option is OFF, and Query Optimizer updates statistics synchronously.

- OFF

  Specifies that statistics updates for the AUTO_UPDATE_STATISTICS option are synchronous. Query Optimizer waits for statistics updates to complete before it compiles queries.

  > [!NOTE]  
  > Setting this option to OFF has no effect unless AUTO_UPDATE_STATISTICS is set to ON.

You can determine this option's status by examining the `is_auto_update_stats_async_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

For more information that describes when to use synchronous or asynchronous statistics updates, see the "Statistics options" section in [Statistics](../../relational-databases/statistics/statistics.md#statistics-options).

#### <a name="auto_tuning"></a> \<automatic_tuning_option> ::=

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)])

Enables or disables `FORCE_LAST_GOOD_PLAN` [Automatic tuning](../../relational-databases/automatic-tuning/automatic-tuning.md) option. You can view the status of this option in the view `sys.database_automatic_tuning_options`.

#### FORCE_LAST_GOOD_PLAN = { DEFAULT | ON | OFF }

- DEFAULT

  The default value for SQL Server is OFF.

- ON

  The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] automatically forces the last known good plan on the [!INCLUDE[tsql-md](../../includes/tsql-md.md)] queries where new query plan causes performance regressions. The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] continuously monitors query performance of the [!INCLUDE[tsql-md](../../includes/tsql-md.md)] query with the forced plan.

  If there are performance gains, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] will keep using last known good plan. If performance gains are not detected, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] will produce a new query plan. The statement will fail if the [Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md) isn't enabled or if the Query Store isn't in *Read-Write* mode.

- OFF

  The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] reports potential query performance regressions caused by query plan changes in [sys.dm_db_tuning_recommendations](../../relational-databases/system-dynamic-management-views/sys-dm-db-tuning-recommendations-transact-sql.md) view. However, these recommendations are not automatically applied. Users can monitor active recommendations and fix identified problems by applying [!INCLUDE[tsql-md](../../includes/tsql-md.md)] scripts that are shown in the view. The default value is OFF.

#### \<change_tracking_option> ::=

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDSFull](../../includes/sssds-md.md)]

Controls change tracking options. You can enable change tracking, set options, change options, and disable change tracking. For examples, see the [Examples](#examples) section later in this article.

- ON

  Enables change tracking for the database. When you enable change tracking, you can also set the AUTO CLEANUP and CHANGE RETENTION options.

- AUTO_CLEANUP = { ON | OFF }

  - ON

    Change tracking information is automatically removed after the specified retention period.

  - OFF

    Change tracking data isn't automatically removed from the database.

- CHANGE_RETENTION = *retention_period* { DAYS | HOURS | MINUTES }

  Specifies the minimum period for keeping change tracking information in the database. Data is removed only when the AUTO_CLEANUP value is ON.

  *retention_period* is an integer that specifies the numerical component of the retention period.

  The default retention period is **2 days**. The minimum retention period is 1 minute. The default retention type is **DAYS**.

- OFF
Disables change tracking for the database. Disable change tracking on all tables before you disable change tracking off the database.

#### \<containment_option> ::=

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)])

Controls database containment options.

#### CONTAINMENT = { NONE | PARTIAL}

- NONE

  The database isn't a contained database.

- PARTIAL

  The database is a contained database. Setting database containment to partial will fail if the database has replication, change data capture, or change tracking enabled. Error checking stops after one failure. For more information about contained databases, see [Contained Databases](../../relational-databases/databases/contained-databases.md).

#### \<cursor_option> ::=

Controls cursor options.

#### CURSOR_CLOSE_ON_COMMIT { ON | OFF }

- ON

  Any cursors open when you commit or roll back a transaction are closed.

- OFF

  Cursors remain open when a transaction is committed; rolling back a transaction closes any cursors except those defined as INSENSITIVE or STATIC.

Connection-level settings that are set by using the SET statement override the default database setting for CURSOR_CLOSE_ON_COMMIT. ODBC and OLE DB clients issue a connection-level SET statement setting CURSOR_CLOSE_ON_COMMIT to OFF for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET CURSOR_CLOSE_ON_COMMIT](../../t-sql/statements/set-cursor-close-on-commit-transact-sql.md).

You can determine this option's status by examining the `is_cursor_close_on_commit_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or the `IsCloseCursorsOnCommitEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### CURSOR_DEFAULT { LOCAL | GLOBAL }

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

Controls whether cursor scope uses LOCAL or GLOBAL.

- LOCAL

  When you specify LOCAL and don't define a cursor as GLOBAL when you create the cursor, the cursor's scope is local. Specifically, the scope is local to the batch, stored procedure, or trigger in which you created the cursor. The cursor name is valid only within this scope.

  The cursor can be referenced by local cursor variables in the batch, stored procedure, or trigger, or a stored procedure OUTPUT parameter. The cursor is implicitly deallocated when the batch, stored procedure, or trigger ends. The cursor is deallocated unless it was passed back in an OUTPUT parameter. The cursor might be passed back in an OUTPUT parameter. If the cursor passes back this way, the cursor is deallocated when the last variable that references the cursor is deallocated or goes out of scope.

- GLOBAL

  When GLOBAL is specified, and a cursor isn't defined as LOCAL when created, the scope of the cursor is global to the connection. The cursor name can be referenced in any stored procedure or batch executed by the connection.

  The cursor is implicitly deallocated only at disconnect. For more information, see [DECLARE CURSOR](../../t-sql/language-elements/declare-cursor-transact-sql.md).

You can determine this option's status by examining the `is_local_cursor_default` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsLocalCursorsDefault` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### \<temporal_history_retention> ::=

#### TEMPORAL_HISTORY_RETENTION { ON | OFF }

ON by default but also automatically set to OFF after point in time restore operation. For more information including how to enable this setting, see [How to configure retention policy](../../relational-databases/tables/manage-retention-of-historical-data-in-system-versioned-temporal-tables.md#how-to-configure-retention-policy).

- ON

  Default. Enables temporal table retention policy. For more information, see [Manage retention of historical data in system-versioned temporal tables](../../relational-databases/tables/manage-retention-of-historical-data-in-system-versioned-temporal-tables.md).

- OFF

  Do not perform temporal historical retention policy.

#### \<data_retention_policy> ::=

**Applies to**: Azure SQL Edge *Only*

#### DATA_RETENTION { ON | OFF }

- ON

  Enables Data Retention policy-based cleanup on a database.

- OFF

  Disables Data Retention policy-based cleanup on a database.

#### \<database_mirroring>

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

For the argument descriptions, see [ALTER DATABASE Database Mirroring](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md).

#### \<date_correlation_optimization_option> ::=

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

Controls the date_correlation_optimization option.

#### DATE_CORRELATION_OPTIMIZATION { ON | OFF }

- ON

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] maintains correlation statistics where a FOREIGN KEY constraint links any two tables in the database and the tables have **datetime** columns.

- OFF

  Correlation statistics are not maintained.

To set DATE_CORRELATION_OPTIMIZATION to ON, there must be no active connections to the database except for the connection that's executing the ALTER DATABASE statement. Afterwards, multiple connections are supported.

The current setting of this option can be determined by examining the `is_date_correlation_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

#### \<db_encryption_option> ::=

Controls the database encryption state.

#### ENCRYPTION { ON | OFF | SUSPEND | RESUME }

- ON

  Sets the database to be encrypted.

- OFF

  Sets the database to not be encrypted.

- SUSPEND

  **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)])

  Can be used to pause the encryption scan after transparent data encryption has been enabled or disabled, or after the encryption key has been changed.

- RESUME

  **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)])

  Can be used to resume a previously paused encryption scan.

For more information about database encryption, see [Transparent Data Encryption](../../relational-databases/security/encryption/transparent-data-encryption.md), and [Transparent Data Encryption with Azure SQL Database](/azure/azure-sql/database/transparent-data-encryption-tde-overview).

When encryption is enabled at the database level, all file groups will be encrypted. Any new file groups will inherit the encrypted property. If any file groups in the database are set to READ ONLY, the database encryption operation will fail.

You can see the encryption state of the database and the state of the encryption scan by using the [sys.dm_database_encryption_keys](../../relational-databases/system-dynamic-management-views/sys-dm-database-encryption-keys-transact-sql.md) dynamic management view.

#### \<db_state_option> ::=

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

Controls the state of the database.

- OFFLINE

  The database is closed, shut down cleanly, and marked offline. The database can't be modified while it's offline.

- ONLINE

  The database is open and available for use.

- EMERGENCY

  The database is marked READ_ONLY, logging is disabled, and access is limited to members of the sysadmin fixed server role. EMERGENCY is primarily used for troubleshooting purposes. For example, a database marked as suspect because of a corrupted log file can be set to the EMERGENCY state. This setting could enable the system administrator read-only access to the database. Only members of the sysadmin fixed server role can set a database to the EMERGENCY state.

Requires the `ALTER DATABASE` permission for the subject database, to change a database to the offline or emergency state, and the server level `ALTER ANY DATABASE` permission to move a database from offline to online.

You can determine this option's status by examining the `state` and `state_desc` columns in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `Status` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function. For more information, see [Database States](../../relational-databases/databases/database-states.md).

A database marked as RESTORING can't be set to OFFLINE, ONLINE, or EMERGENCY. A database may be in the RESTORING state during an active restore operation or when a restore operation of a database or log file fails because of a corrupted backup file.

#### \<db_update_option> ::=

Controls whether updates are allowed on the database.

- READ_ONLY

  Users can read data from the database but not modify it.

  > [!NOTE]  
  > To improve query performance, update statistics before setting a database to READ_ONLY. If additional statistics are needed after a database is set to READ_ONLY, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] will create statistics in the `tempdb` system database. For more information about statistics for a read-only database, see [Statistics](../../relational-databases/statistics/statistics.md).

- READ_WRITE

  The database is available for read and write operations.

To change this state, you must have exclusive access to the database. For more information, see the SINGLE_USER clause.

> [!NOTE]  
> On [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] federated databases, `SET { READ_ONLY | READ_WRITE }` is disabled.

#### \<db_user_access_option> ::=

Controls user access to the database.

#### SINGLE_USER

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

Specifies that only one user at a time can access the database. If you specify SINGLE_USER and another user connects to the database, the ALTER DATABASE statement is blocked until all users disconnect from the specified database. To override this behavior, see the WITH \<termination> clause.

The database remains in SINGLE_USER mode even if the user that set the option signs out. At that point, a different user, but only one, can connect to the database.

Before you set the database to SINGLE_USER, verify the AUTO_UPDATE_STATISTICS_ASYNC option is set to OFF. When set to ON, the background thread used to update statistics takes a connection against the database, and you'll be unable to access the database in single-user mode. To view the status of this option, query the `is_auto_update_stats_async_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. If the option is set to ON, perform the following tasks:

1. Set AUTO_UPDATE_STATISTICS_ASYNC to OFF.

1. Check for active asynchronous statistics jobs by querying the [sys.dm_exec_background_job_queue](../../relational-databases/system-dynamic-management-views/sys-dm-exec-background-job-queue-transact-sql.md) dynamic management view.

If there are active jobs, either allow the jobs to complete or manually terminate them by using [KILL STATS JOB](../../t-sql/language-elements/kill-stats-job-transact-sql.md).

#### RESTRICTED_USER

Allows for only members of the `db_owner` fixed database role and `dbcreator` and `sysadmin` fixed server roles to connect to the database. RESTRICTED_USER doesn't limit their number. Disconnect all connections to the database using the timeframe specified by the ALTER DATABASE statement's termination clause. After the database has transitioned to the RESTRICTED_USER state, connection attempts by unqualified users are refused.

#### MULTI_USER

All users that have the appropriate permissions to connect to the database are allowed. You can determine this option's status by examining the `user_access` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `UserAccess` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### \<delayed_durability_option> ::=

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with  [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)])

Controls whether transactions commit fully durable or delayed durable.

- DISABLED

  All transactions following `SET DISABLED` are fully durable. Any durability options set in an atomic block or commit statement are ignored.

- ALLOWED

  All transactions following `SET ALLOWED` are either fully durable or delayed durable, depending upon the durability option set in the atomic block or commit statement.

- FORCED

  All transactions following `SET FORCED` are delayed durable. Any durability options set in an atomic block or commit statement are ignored.

#### \<external_access_option> ::=

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

Controls whether the database can be accessed by external resources, such as objects from another database.

#### DB_CHAINING { ON | OFF }

- ON

  Database can be the source or target of a cross-database ownership chain.

- OFF

  Database can't participate in cross-database ownership chaining.

> [!IMPORTANT]  
> The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will recognize this setting when the cross db ownership chaining server option is 0 (OFF). When cross db ownership chaining is 1 (ON), all user databases can participate in cross-database ownership chains, regardless of the value of this option. This option is set by using [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md).

To set this option, requires `CONTROL SERVER` permission on the database.

The DB_CHAINING option can't be set on the `master`, `model`, and `tempdb` system databases.

You can determine this option's status by examining the `is_db_chaining_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

#### TRUSTWORTHY { ON | OFF }

- ON

  Database modules (for example, user-defined functions or stored procedures) that use an impersonation context can access resources outside the database.

- OFF

  Database modules in an impersonation context can't access resources outside the database.

  TRUSTWORTHY is set to OFF whenever the database is attached.

By default, all system databases except the `msdb` database have TRUSTWORTHY set to OFF. The value can't be changed for the `model` and `tempdb` databases. We recommend that you never set the TRUSTWORTHY option to ON for the `master` database.

To set this option, requires `CONTROL SERVER` permission on the database.

You can determine this option's status by examining the `is_trustworthy_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

#### DEFAULT_FULLTEXT_LANGUAGE

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)])

Specifies the default language value for full-text indexed columns.

> [!IMPORTANT]  
> This option is allowable only when CONTAINMENT has been set to PARTIAL. If CONTAINMENT is set to NONE, errors will occur.

#### DEFAULT_LANGUAGE

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)])

Specifies the default language for all newly created logins. Language can be specified by providing the local ID (lcid), the language name, or the language alias. For a list of acceptable language names and aliases, see [sys.syslanguages](../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md). This option is allowable only when CONTAINMENT has been set to PARTIAL. If CONTAINMENT is set to NONE, errors will occur.

#### NESTED_TRIGGERS

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)])

Specifies whether an AFTER trigger can cascade; that is, perform an action that initiates another trigger, which initiates another trigger, and so on. This option is allowable only when CONTAINMENT has been set to PARTIAL. If CONTAINMENT is set to NONE, errors will occur.

#### TRANSFORM_NOISE_WORDS

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)])

Used to suppress an error message if noise words, or stopwords, cause a Boolean operation on a full-text query to fail. This option is allowable only when CONTAINMENT has been set to PARTIAL. If CONTAINMENT is set to NONE, errors will occur.

#### TWO_DIGIT_YEAR_CUTOFF

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)])

Specifies an integer from 1753 to 9999 that represents the cutoff year for interpreting two-digit years as four-digit years. This option is allowable only when CONTAINMENT has been set to PARTIAL. If CONTAINMENT is set to NONE, errors will occur.

#### \<FILESTREAM_option> ::=

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)])

Controls the settings for FileTables.

#### NON_TRANSACTED_ACCESS = { OFF | READ_ONLY | FULL }

- OFF

  Non-transactional access to FileTable data is disabled.

- READ_ONLY

  FILESTREAM data in FileTables in this database can be read by non-transactional processes.

- FULL

  Enables full, non-transactional access to FILESTREAM data in FileTables is enabled.

#### DIRECTORY_NAME = *\<directory_name>*

A Windows-compatible directory name. This name should be unique among all the database-level directory names in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. Uniqueness comparison is case-insensitive, regardless of collation settings. This option must be set before creating a FileTable in this database.

#### \<HADR_options> ::=

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

See [ALTER DATABASE SET HADR](../../t-sql/statements/alter-database-transact-sql-set-hadr.md).

#### \<mixed_page_allocation_option> ::=

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)])

Controls whether the database can create initial pages using a mixed extent for the first eight pages of a table or index.

#### MIXED_PAGE_ALLOCATION { OFF | ON }

- OFF

  The database always creates initial pages using uniform extents. OFF is the default value.

- ON

  The database can create initial pages using mixed extents.

This setting is ON for all system databases. The `tempdb` system database is the only system database that supports OFF.

#### \<PARAMETERIZATION_option> ::=

Controls the parameterization option. For more information on parameterization, see the [Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md#simple-parameterization).

#### PARAMETERIZATION { SIMPLE | FORCED }

- SIMPLE

  Queries are parameterized based on the default behavior of the database.

- FORCED

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] parameterizes all queries in the database.

The current setting of this option can be determined by examining the `is_parameterization_forced` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

#### <a name="query-store"></a> \<query_store_options> ::=

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)])

#### ON | OFF [ ( FORCED )  ] | CLEAR [ ALL ]

Controls whether the Query Store is enabled in this database, and also controls removing the contents of the Query Store. For more information, see [Query Store Usage Scenarios](../../relational-databases/performance/query-store-usage-scenarios.md).

- ON

  Enables the Query Store.

  Many new performance features of [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] such as Query Store hints, CE Feedback, Degree of Parallelism (DOP) feedback, and Memory Grant feedback (MGF) persistence required Query Store to be enabled. For databases that have been restored from other SQL Server instances and for those databases that are upgraded from an in-place upgrade to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], these databases will retain the previous Query Store settings. If there is concern about the overhead Query Store may introduce, administrators can leverage [custom capture policies](#query_capture_policy_option_list--) with `QUERY_CAPTURE_MODE = CUSTOM`. For examples of how to enable the Query Store with custom capture policy options, see the [Examples](#examples) section later in this article.

- OFF [ ( FORCED ) ]

  Disables the Query Store. <!--OFF is the default value for [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)], and [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)].--> FORCED is optional. FORCED aborts all running Query Store background tasks, and skips the synchronous flush when Query Store is turned off. Causes the Query Store to shut down as fast as possible. FORCED applies to [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP2 CU14, [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU21, [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] CU6, and later builds.

  > [!NOTE]  
  > Query Store cannot be disabled in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] single database and Elastic Pool. Executing `ALTER DATABASE [database] SET QUERY_STORE = OFF` will return the warning `'QUERY_STORE=OFF' is not supported in this version of SQL Server.`.

- CLEAR [ ALL ]

  Removes query-related data from the Query Store. ALL is optional. ALL removes query-related data and metadata from the Query Store.

#### OPERATION_MODE { READ_ONLY | READ_WRITE }

Describes the operation mode of the Query Store.

#### READ_WRITE

The Query Store collects and persists query plan and runtime execution statistics information.

#### READ_ONLY

Information can be read from the Query Store, but new information isn't added. If the maximum issued space of the Query Store has been exhausted, the Query Store will change is operation mode to READ_ONLY.

#### CLEANUP_POLICY

Describes the data retention policy of the Query Store. STALE_QUERY_THRESHOLD_DAYS determines the number of days for which the information for a query is kept in the Query Store. STALE_QUERY_THRESHOLD_DAYS is type **bigint**. The default value is 30.

#### DATA_FLUSH_INTERVAL_SECONDS

Determines the frequency at which data written to the Query Store is persisted to disk. To optimize for performance, data collected by the Query Store is asynchronously written to the disk. The frequency at which this asynchronous transfer occurs is configured by using the DATA_FLUSH_INTERVAL_SECONDS argument. DATA_FLUSH_INTERVAL_SECONDS is type **bigint**. The default value is **900** (15 min).

#### MAX_STORAGE_SIZE_MB

Determines the space issued to the Query Store. MAX_STORAGE_SIZE_MB is type **bigint**. The default value is **100 MB** for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] through [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]). Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], the default value is **1000 MB**.

`MAX_STORAGE_SIZE_MB` limit isn't strictly enforced. Storage size is checked only when Query Store writes data to disk. This interval is set by the `DATA_FLUSH_INTERVAL_SECONDS` option or the [!INCLUDE[ssManStudio](../../includes/ssManStudio-md.md)] Query Store dialog option **Data Flush Interval**. The interval default value is 900 seconds (or 15 minutes).

If the Query Store has breached the `MAX_STORAGE_SIZE_MB` limit between storage size checks, it will transition to read-only mode. If `SIZE_BASED_CLEANUP_MODE` is enabled, the cleanup mechanism to enforce the `MAX_STORAGE_SIZE_MB` limit is also triggered.

Once enough space has been cleared, the Query Store mode will automatically switch back to read-write.

> [!IMPORTANT]  
> If you think that your workload capture will need more than 10 GB of disk space, you should probably rethink and optimize your workload to reuse query plans (for example using [forced parameterization](../../relational-databases/query-processing-architecture-guide.md#forced-parameterization), or adjust the Query Store configurations.
> Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], you can set `QUERY_CAPTURE_MODE` to CUSTOM for additional control over the query capture policy.

#### INTERVAL_LENGTH_MINUTES

Determines the time interval at which runtime execution statistics data is aggregated into the Query Store. To optimize for space usage, the runtime execution statistics in the runtime stats store are aggregated over a fixed time window. This fixed time window is configured by using the INTERVAL_LENGTH_MINUTES argument. INTERVAL_LENGTH_MINUTES is type **bigint**. The default value is **60**.

#### SIZE_BASED_CLEANUP_MODE { AUTO | OFF }

Controls whether cleanup automatically activates when the total amount of data gets close to maximum size.

- AUTO

  Size-based cleanup will be automatically activated when size on disk reaches 90% of **MAX_STORAGE_SIZE_MB**. Size-based cleanup removes the least expensive and oldest queries first. It stops at approximately 80% of **MAX_STORAGE_SIZE_MB**. This value is the default configuration value.

- OFF

  Size-based cleanup won't be automatically activated.

SIZE_BASED_CLEANUP_MODE is type **nvarchar**.

#### QUERY_CAPTURE_MODE { ALL | AUTO | CUSTOM | NONE }

Designates the currently active query capture mode. Each mode defines specific query capture policies. QUERY_CAPTURE_MODE is type **nvarchar**.

> [!NOTE]  
> Cursors, queries inside Stored Procedures, and Natively compiled queries are always captured when the query capture mode is set to ALL, AUTO, or CUSTOM.

- ALL

  Captures all queries. **ALL** is the default configuration value for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] through [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]).

- AUTO

  Capture relevant queries based on execution count and resource consumption. This is the default configuration value for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

- NONE

  Stop capturing new queries. The Query Store will continue to collect compile and runtime statistics for queries that were captured already. Use this configuration with caution since you may miss capturing important queries.

- CUSTOM

  **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)])

  Allows control over the [QUERY_CAPTURE_POLICY options](#query_capture_policy_option_list--). Custom capture policies can help Query Store capture the most important queries in your workload. See the <query_capture_policy_option_list> for customizable options.

#### MAX_PLANS_PER_QUERY

Defines the maximum number of plans maintained for each query. MAX_PLANS_PER_QUERY is type **int**. The default value is **200**.

#### WAIT_STATS_CAPTURE_MODE { ON | OFF }

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]))

Controls whether wait statistics will be captured per query.

- ON

  Wait statistics information per query is captured. This value is the default configuration value.

- OFF

  Wait statistics information per query won't be captured.

#### \<query_capture_policy_option_list> :: =

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)])

Controls the [Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md) capture policy options. Except for STALE_CAPTURE_POLICY_THRESHOLD, these options define the OR conditions that need to happen for queries to be captured in the defined Stale Capture Policy Threshold value.

Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], the `QUERY_CAPTURE_MODE = AUTO` setting captures query store details when any of the following thresholds are hit:

- EXECUTION_COUNT = 30 executions = execution count
- TOTAL_COMPILE_CPU_TIME_MS = 1 second = compile time in milliseconds
- TOTAL_EXECUTION_CPU_TIME_MS = 100 ms = execution CPU time in milliseconds

For example:

```sql
EXECUTION_COUNT = 30,
TOTAL_COMPILE_CPU_TIME_MS = 1000,
TOTAL_EXECUTION_CPU_TIME_MS = 100
```

You can customize these options with `QUERY_CAPTURE_MODE = CUSTOM`:

- STALE_CAPTURE_POLICY_THRESHOLD = *integer* { DAYS | HOURS }

  Defines the evaluation interval period to determine if a query should be captured. The default is 1 day, and it can be set from 1 hour to seven days.

- EXECUTION_COUNT = *integer*

  Defines the number of times a query is executed over the evaluation period. The default is 30, which means that for the default Stale Capture Policy Threshold, a query must execute at least 30 times in one day to be persisted in the Query Store. EXECUTION_COUNT is type **int**.

- TOTAL_COMPILE_CPU_TIME_MS = *integer*

  Defines total elapsed compile CPU time used by a query over the evaluation period. The default is 1000, which means that for the default Stale Capture Policy Threshold, a query must have a total of at least one second of CPU time spent during query compilation in one day to be persisted in the Query Store. TOTAL_COMPILE_CPU_TIME_MS is type **int**.

- TOTAL_EXECUTION_CPU_TIME_MS = *integer*

  Defines total elapsed execution CPU time used by a query over the evaluation period. The default is 100 which means that for the default Stale Capture Policy Threshold, a query must have a total of at least 100 ms of CPU time spent during execution in one day to be persisted in the Query Store. TOTAL_EXECUTION_CPU_TIME_MS is type **int**.

#### \<recovery_option> ::=

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

Controls database recovery options and disk I/O error checking.

- FULL

  Provides full recovery after media failure by using transaction log backups. If a data file is damaged, media recovery can restore all committed transactions. For more information, see [Recovery Models](../../relational-databases/backup-restore/recovery-models-sql-server.md).

- BULK_LOGGED

  Provides recovery after media failure. Combines the best performance and least amount of log-space use for certain large-scale or bulk operations. For information about what operations can be minimally logged, see [The Transaction Log](../../relational-databases/logs/the-transaction-log-sql-server.md). Under the BULK_LOGGED recovery model, logging for these operations is minimal. For more information, see [Recovery Models](../../relational-databases/backup-restore/recovery-models-sql-server.md).

- SIMPLE

  A simple backup strategy that uses minimal log space is provided. Log space can be automatically reused when it's no longer required for server failure recovery. For more information, see [Recovery Models](../../relational-databases/backup-restore/recovery-models-sql-server.md).

  > [!IMPORTANT]  
  > The simple recovery model is easier to manage than the other two models but at the expense of greater data loss exposure if a data file is damaged. All changes since the most recent database or differential database backup are lost and must be manually reentered.

The default recovery model is determined by the recovery model of the `model` system database. For more information about selecting the appropriate recovery model, see [Recovery Models](../../relational-databases/backup-restore/recovery-models-sql-server.md).

You can determine this option's status by examining the `recovery_model` and `recovery_model_desc` columns in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `Recovery` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### TORN_PAGE_DETECTION { ON | OFF }

- ON

  Incomplete pages can be detected by the [!INCLUDE[ssDE](../../includes/ssde-md.md)].

- OFF

  Incomplete pages can't be detected by the [!INCLUDE[ssDE](../../includes/ssde-md.md)].

> [!IMPORTANT]  
> The syntax structure TORN_PAGE_DETECTION ON | OFF will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using this syntax structure in new development work, and plan to modify applications that currently use the syntax structure. Use the PAGE_VERIFY option instead.

#### <a id="page_verify"></a> PAGE_VERIFY { CHECKSUM | TORN_PAGE_DETECTION | NONE }

Discovers damaged database pages caused by disk I/O path errors. Disk I/O path errors can be the cause of database corruption problems. These errors are most often caused by power failures or disk hardware failures that occur at the time the page is written to disk.

- CHECKSUM

  Calculates a checksum over the contents of the whole page and stores the value in the page header when a page is written to disk. When the page is read from disk, the checksum is recomputed and compared to the checksum value stored in the page header. If the values don't match, error message 824 (indicating a checksum failure) is reported to both the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log and the Windows event log. A checksum failure indicates an I/O path problem. To determine the root cause requires investigation of the hardware, firmware drivers, BIOS, filter drivers (such as virus software), and other I/O path components.

- TORN_PAGE_DETECTION

  Saves a specific 2-bit pattern for each 512-byte sector in the 8-kilobyte (KB) database page and stored in the database page header when the page is written to disk. When the page is read from disk, the torn bits stored in the page header are compared to the actual page sector information.

  Unmatched values indicate that only part of the page was written to disk. In this situation, error message 824 (indicating a torn page error) is reported to both the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log and the Windows event log. Torn pages are typically detected by database recovery if it's truly an incomplete write of a page. However, other I/O path failures can cause a torn page at any time.

- NONE

  Database page writes won't generate a CHECKSUM or TORN_PAGE_DETECTION value. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will not verify a checksum or torn page during a read even if a CHECKSUM or TORN_PAGE_DETECTION value is present in the page header.

Consider the following important points when you use the PAGE_VERIFY option:

- The default is **CHECKSUM**.
- When a user or system database is upgraded to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or a later version, the PAGE_VERIFY value (NONE or TORN_PAGE_DETECTION) isn't changed. We recommend that you change to CHECKSUM.

  > [!NOTE]  
  > In earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the PAGE_VERIFY database option is set to NONE for the `tempdb` database and can't be modified. Starting with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], the default value for the `tempdb` database is CHECKSUM for new installations of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When upgrading an installation [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the default value remains NONE. The option can be modified. We recommend that you use CHECKSUM for the `tempdb` database.

- TORN_PAGE_DETECTION may use fewer resources but provides a minimal subset of the CHECKSUM protection.
- PAGE_VERIFY can be set without taking the database offline, locking the database, or otherwise impeding concurrency on that database.
- CHECKSUM is mutually exclusive to TORN_PAGE_DETECTION. Both options can't be enabled at the same time.

When a torn page or checksum failure is detected, you can recover by restoring the data or potentially rebuilding the index if the failure is limited only to index pages. If you encounter a checksum failure, to determine the type of database page or pages affected, run DBCC CHECKDB. For more information about restore options, see [RESTORE Arguments](../../t-sql/statements/restore-statements-arguments-transact-sql.md). Although restoring the data will resolve the data corruption problem, the root cause, for example, disk hardware failure, should be diagnosed and corrected as soon as possible to prevent continuing errors.

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will retry any read that fails with a checksum, torn page, or other I/O error four times. If the read is successful in any one of the retry attempts, a message is written to the error log. The command that triggered the read will continue. The command will fail with error message 824 if the retry attempts fail.

For more information about error messages 823, 824 and 825, see:

- [How to troubleshoot a Msg 823 error in SQL Server](https://support.microsoft.com/help/2015755)
- [How to troubleshoot Msg 824 in SQL Server](https://support.microsoft.com/help/2015756)
- [How to troubleshoot Msg - read retry](https://support.microsoft.com/help/2015757).

The current setting of this option can be determined by examining the `page_verify_option` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or the `IsTornPageDetectionEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### \<remote_data_archive_option> ::=

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)])

Enables or disables Stretch Database for the database. For more info, see [Stretch Database](../../sql-server/stretch-database/stretch-database.md).

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

#### REMOTE_DATA_ARCHIVE = { ON ( SERVER = \<server_name>, { CREDENTIAL = \<db_scoped_credential_name> | FEDERATED_SERVICE_ACCOUNT = ON | OFF } ) | OFF

- ON

  Enables Stretch Database for the database. For more info, including additional prerequisites, see [Enable Stretch Database for a database](../../sql-server/stretch-database/enable-stretch-database-for-a-database.md).

  Requires `db_owner` permission to enable Stretch Database for a table. Requires `db_owner` and `CONTROL DATABASE` permissions to enable Stretch Database for a database.

  - SERVER = \<server_name>

    Specifies the address of the Azure server. Include the `.database.windows.net` portion of the name. For example, `MyStretchDatabaseServer.database.windows.net`.

  - CREDENTIAL = \<db_scoped_credential_name>

    Specifies the database scoped credential that the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses to connect to the Azure server. Make sure the credential exists before you run this command. For more info, see [CREATE DATABASE SCOPED CREDENTIAL](../../t-sql/statements/create-database-scoped-credential-transact-sql.md).

  - FEDERATED_SERVICE_ACCOUNT = { ON | OFF }

    You can use a federated service account for the on-premises SQL Server to communicate with the remote Azure server when the following conditions are all true.

    - The service account under which the instance of SQL Server is running is a domain account.
    - The domain account belongs to a domain whose Active Directory is federated with Azure Active Directory.
    - The remote Azure server is configured to support Azure Active Directory authentication.
    - The service account under which the instance of SQL Server is running must be configured as a `dbmanager` or `sysadmin` account on the remote Azure server.

    If you specify that the federated service account is ON, you can't also specify the CREDENTIAL argument. Provide the CREDENTIAL argument if you specify OFF.

- OFF

  Disables Stretch Database for the database. For more info, see [Disable Stretch Database and bring back remote data](../../sql-server/stretch-database/disable-stretch-database-and-bring-back-remote-data.md).

  You can only disable Stretch Database for a database after the database no longer contains any tables that are enabled for Stretch Database. After you disable Stretch Database, data migration stops. Also, query results no longer include results from remote tables.

  Disabling Stretch Database doesn't remove the remote database. To delete the remote database, drop it by using the Azure portal.

#### \<service_broker_option> ::=

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

Controls the following [!INCLUDE[ssSB](../../includes/sssb-md.md)] options: enables or disables message delivery, sets a new [!INCLUDE[ssSB](../../includes/sssb-md.md)] identifier, or sets conversation priorities to ON or OFF.

#### ENABLE_BROKER

Specifies that [!INCLUDE[ssSB](../../includes/sssb-md.md)] is enabled for the specified database. Message delivery is started, and the `is_broker_enabled` flag is set to true in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. The database keeps the existing [!INCLUDE[ssSB](../../includes/sssb-md.md)] identifier. Service broker can't be enabled while the database is the principal in a database mirroring configuration.

> [!NOTE]  
> ENABLE_BROKER requires an exclusive database lock. If other sessions have locked resources in the database, ENABLE_BROKER will wait until the other sessions release their locks. To enable [!INCLUDE[ssSB](../../includes/sssb-md.md)] in a user database, ensure that no other sessions are using the database before you run the `ALTER DATABASE SET ENABLE_BROKER` statement, such as by putting the database in single user mode. To enable [!INCLUDE[ssSB](../../includes/sssb-md.md)] in the `msdb` database, first stop [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent so that [!INCLUDE[ssSB](../../includes/sssb-md.md)] can obtain the necessary lock.

#### DISABLE_BROKER

Specifies that [!INCLUDE[ssSB](../../includes/sssb-md.md)] is disabled for the specified database. Message delivery is stopped, and the `is_broker_enabled` flag is set to false in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. The database keeps the existing [!INCLUDE[ssSB](../../includes/sssb-md.md)] identifier.

#### NEW_BROKER

Specifies that the database should receive a new broker identifier. The database acts as a new service broker. As such, all existing conversations in the database are immediately removed without producing end dialog messages. Any route that references the old [!INCLUDE[ssSB](../../includes/sssb-md.md)] identifier must be re-created with the new identifier.

#### ERROR_BROKER_CONVERSATIONS

Specifies that [!INCLUDE[ssSB](../../includes/sssb-md.md)] message delivery is enabled. This setting preserves the existing [!INCLUDE[ssSB](../../includes/sssb-md.md)] identifier for the database. [!INCLUDE[ssSB](../../includes/sssb-md.md)] ends all conversations in the database with an error. This setting enables applications to run regular cleanup for existing conversations.

#### HONOR_BROKER_PRIORITY { ON | OFF }

- ON

  Send operations take into consideration the priority levels that are assigned to conversations. Messages from conversations that have high priority levels are sent before messages from conversations that are assigned low-priority levels.

- OFF

  Send operations run as if all conversations have the default priority level.

Changes to the HONOR_BROKER_PRIORITY option take effect immediately for new dialogs or dialogs that have no messages waiting to be sent. Dialogs with messages to be sent when ALTER DATABASE is run won't pick up the new setting until some of the messages for the dialog are sent. The amount of time before all dialogs start using the new setting can vary considerably.

The current setting of this property is reported in the `is_broker_priority_honored` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

#### \<snapshot_option> ::=

Calculates the transaction isolation level.

#### ALLOW_SNAPSHOT_ISOLATION { ON | OFF }

- ON

  Enables Snapshot option at the database level. When it's enabled, DML statements start generating row versions even when no transaction uses Snapshot Isolation. Once this option is enabled, transactions can specify the SNAPSHOT transaction isolation level. When a transaction runs at the SNAPSHOT isolation level, all statements see a snapshot of data as it exists at the start of the transaction. If a transaction running at the SNAPSHOT isolation level accesses data in multiple databases, either ALLOW_SNAPSHOT_ISOLATION must be set to ON in all the databases, or each statement in the transaction must use locking hints on any reference in a FROM clause to a table in a database where ALLOW_SNAPSHOT_ISOLATION is OFF.

- OFF

  Turns off the Snapshot option at the database level. Transactions can't specify the SNAPSHOT transaction isolation level.

When you set ALLOW_SNAPSHOT_ISOLATION to a new state (from ON to OFF, or from OFF to ON), ALTER DATABASE doesn't return control to the caller until all existing transactions in the database are committed. If the database is already in the state specified in the ALTER DATABASE statement, control is returned to the caller immediately. If the ALTER DATABASE statement doesn't return quickly, use [sys.dm_tran_active_snapshot_database_transactions](../../relational-databases/system-dynamic-management-views/sys-dm-tran-active-snapshot-database-transactions-transact-sql.md) to determine whether there are long-running transactions. If the ALTER DATABASE statement is canceled, the database remains in the state it was in when ALTER DATABASE was started. The [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view indicates the state of snapshot-isolation transactions in the database. If `snapshot_isolation_state_desc` = IN_TRANSITION_TO_ON, the command `ALTER DATABASE ... ALLOW_SNAPSHOT_ISOLATION OFF` will pause six seconds and retry the operation.

You can't change the state of ALLOW_SNAPSHOT_ISOLATION if the database is OFFLINE.

If you set ALLOW_SNAPSHOT_ISOLATION in a READ_ONLY database, the setting will be kept if the database is later set to READ_WRITE.

You can change the ALLOW_SNAPSHOT_ISOLATION settings for the `master`, `model`, `msdb`, and `tempdb` databases. The setting is kept every time the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is stopped and restarted if you change the setting for `tempdb`. If you change the setting for `model`, that setting becomes the default for any new databases that are created, except for `tempdb`.

The option is ON by default for the `master` and `msdb` databases.

The current setting of this option can be determined by examining the `snapshot_isolation_state` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

#### READ_COMMITTED_SNAPSHOT { ON | OFF }

- ON

  Enables Read-Committed Snapshot option at the database level. When it's enabled, DML statements start generating row versions even when no transaction uses Snapshot Isolation. Once this option is enabled, the transactions specifying the read committed isolation level use row versioning instead of locking. All statements see a snapshot of data as it exists at the start of the statement when a transaction runs at the READ COMMITTED isolation level.

- OFF

  Turns off Read-Committed Snapshot option at the database level. Transactions specifying the READ COMMITTED isolation level use locking.

To set READ_COMMITTED_SNAPSHOT ON or OFF, there must be no active connections to the database except for the connection running the ALTER DATABASE command. However, the database doesn't have to be in single-user mode. You can't change the state of this option when the database is OFFLINE.

If you set READ_COMMITTED_SNAPSHOT in a READ_ONLY database, the setting will be kept when the database is later set to READ_WRITE.

READ_COMMITTED_SNAPSHOT can't be turned ON for the `master`, `tempdb`, or `msdb` system databases. If you change the setting for `model`, that setting becomes the default for any new databases created, except for `tempdb`.

The current setting of this option can be determined by examining the `is_read_committed_snapshot_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

> [!WARNING]  
> When a table is created with **DURABILITY = SCHEMA_ONLY**, and **READ_COMMITTED_SNAPSHOT** is subsequently changed using **ALTER DATABASE**, data in the table will be lost.

#### MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT { ON | OFF }

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)])

- ON

  When the transaction isolation level is set to any isolation level lower than SNAPSHOT, all interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] operations on memory-optimized tables are run under SNAPSHOT isolation. Examples of isolation levels lower than snapshot are READ COMMITTED or READ UNCOMMITTED. These operations run whether the transaction isolation level is set explicitly at the session level, or the default is used implicitly.

- OFF

  Doesn't elevate the transaction isolation level for interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] operations on memory-optimized tables.

You can't change the state of MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT if the database is OFFLINE.

The default option is OFF.

The current setting of this option can be determined by examining the `is_memory_optimized_elevate_to_snapshot_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

#### \<sql_option> ::=

Controls the ANSI compliance options at the database level.

#### ANSI_NULL_DEFAULT { ON | OFF }

Determines the default value, NULL or NOT NULL, of a column or [CLR user-defined type](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md) for which the nullability isn't explicitly defined in CREATE TABLE or ALTER TABLE statements. Columns that are defined with constraints follow constraint rules whatever this setting may be.

- ON

  The default value for an undefined column is NULL.

- OFF

  The default value for an undefined column is NOT NULL.

Connection-level settings that are set by using the SET statement override the default database-level setting for ANSI_NULL_DEFAULT. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_NULL_DEFAULT to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_NULL_DFLT_ON](../../t-sql/statements/set-ansi-null-dflt-on-transact-sql.md).

For ANSI compatibility, setting the database option ANSI_NULL_DEFAULT to ON changes the database default to NULL.

You can determine this option's status by examining the `is_ansi_null_default_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAnsiNullDefault` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### ANSI_NULLS { ON | OFF }

- ON

  All comparisons to a null value evaluate to UNKNOWN.

- OFF

  Comparisons of non-Unicode values to a null value evaluate to TRUE if both values are NULL.

> [!IMPORTANT]  
> In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], ANSI_NULLS will always be ON and any applications that explicitly set the option to OFF will produce an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.

Connection-level settings that are set by using the SET statement override the default database setting for ANSI_NULLS. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_NULLS to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_NULLS](../../t-sql/statements/set-ansi-nulls-transact-sql.md).

> [!IMPORTANT]  
> SET ANSI_NULLS also must be set to ON when you create or make changes to indexes on computed columns or indexed views.

You can determine this option's status by examining the `is_ansi_nulls_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAnsiNullsEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### ANSI_PADDING { ON | OFF }

- ON

  Strings are padded to the same length before conversion. Also padded to the same length before inserting to a **varchar** or **nvarchar** data type.

- OFF

  Inserts trailing blanks in character values into **varchar** or **nvarchar** columns. Also leaves trailing zeros in binary values that are inserted into **varbinary** columns. Values aren't padded to the length of the column.

  When OFF is specified, this setting affects only the definition of new columns.

> [!IMPORTANT]  
> In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], ANSI_PADDING will always be ON and any applications that explicitly set the option to OFF will produce an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. We recommend that you always set ANSI_PADDING to ON. ANSI_PADDING must be ON when you create or manipulate indexes on computed columns or indexed views.

**char(*n*)** and **binary(*n*)** columns that allow for nulls are padded to the column length when ANSI_PADDING is set to ON. Trailing blanks and zeros are trimmed when ANSI_PADDING is OFF. **char(_n_)** and **binary(_n_)** columns that don't allow nulls are always padded to the length of the column.

Connection-level settings that are set by using the SET statement override the default database-level setting for ANSI_PADDING. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_PADDING to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_PADDING](../../t-sql/statements/set-ansi-padding-transact-sql.md).

You can determine this option's status by examining the `is_ansi_padding_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAnsiPaddingEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### ANSI_WARNINGS { ON | OFF }

- ON

  Errors or warnings are issued when conditions such as divide-by-zero occur. Errors and warnings are also issued when null values appear in aggregate functions.

- OFF

  No warnings are raised and null values are returned when conditions such as divide-by-zero occur.

> [!IMPORTANT]  
> SET ANSI_WARNINGS must be set to ON when you create or make changes to indexes on computed columns or indexed views.

Connection-level settings that are set by using the SET statement override the default database setting for ANSI_WARNINGS. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_WARNINGS to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_WARNINGS](../../t-sql/statements/set-ansi-warnings-transact-sql.md).

You can determine this option's status by examining the `is_ansi_warnings_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAnsiWarningsEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### ARITHABORT { ON | OFF }

- ON

  A query is ended when an overflow or divide-by-zero error occurs during query execution.

- OFF

  A warning message is displayed when one of these errors occurs. The query, batch, or transaction continues to process as if no error occurred even if a warning is displayed.

> [!IMPORTANT]  
> SET ARITHABORT must be set to ON when you create or make changes to indexes on computed columns or indexed views.

You can determine this option's status by examining the `is_arithabort_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsArithmeticAbortEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### COMPATIBILITY_LEVEL = { 160 | 150 | 140 | 130 | 120 | 110 | 100 }

For more information, see [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

#### CONCAT_NULL_YIELDS_NULL { ON | OFF }

- ON

  The result of a concatenation operation is NULL when either operand is NULL. For example, concatenating the character string "This is" and NULL returns the NULL value instead of the "This is" value.

- OFF

  The null value is treated as an empty character string.

> [!IMPORTANT]
> CONCAT_NULL_YIELDS_NULL must be set to ON when you create or make changes to indexes on computed columns or indexed views.
>  
> In upcoming versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], CONCAT_NULL_YIELDS_NULL will always be ON, and any applications that explicitly set the option to OFF will trigger an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.

Connection-level settings that are set by using the SET statement override the default database setting for CONCAT_NULL_YIELDS_NULL. By default, ODBC and OLE DB clients issue a connection-level SET statement setting CONCAT_NULL_YIELDS_NULL to ON for the session when connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET CONCAT_NULL_YIELDS_NULL](../../t-sql/statements/set-concat-null-yields-null-transact-sql.md).

You can determine this option's status by examining the `is_concat_null_yields_null_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsNullConcat` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### NUMERIC_ROUNDABORT { ON | OFF }

- ON

  An error is generated when loss of precision occurs in an expression.

- OFF

  Loss of precision doesn't generate an error message, and the result is rounded to the precision of the column or variable storing the result.

  > [!IMPORTANT]  
  > NUMERIC_ROUNDABORT must be set to OFF when you create or make changes to indexes on computed columns or indexed views.

You can determine the status of this option in the `is_numeric_roundabort_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsNumericRoundAbortEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### QUOTED_IDENTIFIER { ON | OFF }

- ON

  Double quotation marks can be used to enclose delimited identifiers.

  All strings delimited by double quotation marks are interpreted as object identifiers. Quoted identifiers don't have to follow the [!INCLUDE[tsql](../../includes/tsql-md.md)] rules for identifiers. They can be keywords and can include characters that aren't allowed in [!INCLUDE[tsql](../../includes/tsql-md.md)] identifiers. If a single quotation mark (') is part of the literal string, it can be represented by double quotation marks (").

- OFF

  Identifiers can't be in quotation marks and must follow all [!INCLUDE[tsql](../../includes/tsql-md.md)] rules for identifiers. Literals can be delimited by either single or double quotation marks.

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also allows for identifiers to be delimited by square brackets (`[` and `]`). Bracketed identifiers can always be used, whatever the QUOTED_IDENTIFIER setting is. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).

When a table is created, the QUOTED IDENTIFIER option is always stored as ON in the metadata of the table. The option is stored even if the option is set to OFF when the table is created.

Connection-level settings that are set by using the SET statement override the default database setting for QUOTED_IDENTIFIER. ODBC and OLE DB clients issue a connection-level SET statement setting QUOTED_IDENTIFIER to ON, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET QUOTED_IDENTIFIER](../../t-sql/statements/set-quoted-identifier-transact-sql.md).

You can determine this option's status by examining the `is_quoted_identifier_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsQuotedIdentifiersEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### RECURSIVE_TRIGGERS { ON | OFF }

- ON

  Recursive firing of AFTER triggers is allowed.

- OFF

  You can determine this option's status by examining the `is_recursive_triggers_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsRecursiveTriggersEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

> [!NOTE]  
> Only direct recursion is prevented when RECURSIVE_TRIGGERS is set to OFF. To disable indirect recursion, you must also set the nested triggers server option to 0.

You can determine this option's status by examining the `is_recursive_triggers_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or the `IsRecursiveTriggersEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### \<suspend_for_snapshot_backup> ::=

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL22](../../includes/sssql22-md.md)])

Suspends databases for snapshot backup. May define a group of one or more databases. May designate copy only mode.

#### SET SUSPEND_FOR_SNAPSHOT_BACKUP = { ON | **OFF** }

Suspends, or un-suspends databases. Default OFF.

####  MODE = COPY_ONLY

Optional. Uses COPY_ONLY mode.

#### \<target_recovery_time_option> ::=

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)])

Specifies the frequency of indirect checkpoints on a per-database basis. Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] the default value for new databases is **1 minute**, which indicates database will use indirect checkpoints. For older versions the default is 0, which indicates that the database will use automatic checkpoints, whose frequency depends on the recovery interval setting of the server instance. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends 1 minute for most systems.

#### TARGET_RECOVERY_TIME = *target_recovery_time* { SECONDS | MINUTES }

- *target_recovery_time*

  Specifies the maximum bound on the time to recover the specified database in the event of a crash. *target_recovery_time* is type **int**.

- SECONDS

  Indicates that *target_recovery_time* is expressed as the number of seconds.

- MINUTES

  Indicates that *target_recovery_time* is expressed as the number of minutes.

For more information about indirect checkpoints, see [Database Checkpoints](../../relational-databases/logs/database-checkpoints-sql-server.md).

#### WITH \<termination> ::=

Specifies when to roll back incomplete transactions when the database is transitioned from one state to another. If the termination clause is omitted, the ALTER DATABASE statement waits indefinitely if there's any lock on the database. Only one termination clause can be specified, and it follows the SET clauses.

> [!NOTE]  
> Not all database options use the WITH \<termination> clause. For more information, see the table under [Setting options](#SettingOptions) of the "Remarks" section of this article.

- ROLLBACK AFTER *integer* [SECONDS] | ROLLBACK IMMEDIATE

  Specifies whether to roll back after the specified number of seconds or immediately.

- NO_WAIT

  Specifies that the request will fail if the requested database state or option change can't complete immediately. Completing immediately means not waiting for transactions to commit or roll back on their own.

## <a id="SettingOptions"></a> Set options

To retrieve current settings for database options, use the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)

After you set a database option, the new setting takes effect immediately.

You can change the default values for any one of the database options for all newly created databases. To do so, change the appropriate database option in the `model` database.

Not all database options use the WITH \<termination> clause or can be specified in combination with other options. The following table lists these options and their option and termination status.

 | Options category | Can be specified with other options | Can use the WITH \<termination> clause |
 | --- | --- | --- |
 | \<db_state_option> | Yes | Yes |
 | \<db_user_access_option> | Yes | Yes |
 | \<db_update_option> | Yes | Yes |
 | \<delayed_durability_option> | Yes | Yes |
 | \<external_access_option> | Yes | No |
 | \<cursor_option> | Yes | No |
 | \<auto_option> | Yes | No |
 | \<sql_option> | Yes | No |
 | \<recovery_option> | Yes | No |
 | \<target_recovery_time_option> | No | Yes |
 | \<database_mirroring_option> | No | No |
 | ALLOW_SNAPSHOT_ISOLATION | No | No |
 | READ_COMMITTED_SNAPSHOT | No | Yes |
 | MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT | Yes | Yes |
 | \<service_broker_option> | Yes | No |
 | DATE_CORRELATION_OPTIMIZATION | Yes | Yes |
 | \<parameterization_option> | Yes | Yes |
 | \<change_tracking_option> | Yes | Yes |
 | \<db_encryption_option> | Yes | No |
 | \<accelerated_database_recovery> | Yes | Yes |

The plan cache for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is cleared by setting one of the following options:

:::row:::
    :::column:::
        OFFLINE
    :::column-end:::
    :::column:::
        ONLINE
    :::column-end:::
    :::column:::
        MODIFY_NAME
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        COLLATE
    :::column-end:::
    :::column:::
        READ_ONLY
    :::column-end:::
    :::column:::
        READ_WRITE
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        MODIFY FILEGROUP DEFAULT
    :::column-end:::
    :::column:::
        MODIFY FILEGROUP READ_WRITE
    :::column-end:::
    :::column:::
        MODIFY FILEGROUP READ_ONLY
    :::column-end:::
:::row-end:::

The plan cache is also flushed in the following scenarios.

- A database has the AUTO_CLOSE database option set to ON. When no user connection references or uses the database, the background task tries to close and shut down the database automatically.
- You run several queries against a database that has default options. Then, the database is dropped.
- A database snapshot for a source database is dropped.
- You successfully rebuild the transaction log for a database.
- You restore a database backup.
- You detach a database.

Clearing the plan cache causes a recompilation of all subsequent execution plans and can cause a sudden, temporary decrease in query performance. For each cleared cache store in the plan cache, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log contains the following informational message: `SQL Server has encountered %d occurrence(s) of cachestore flush for the '%s' cachestore (part of plan cache) due to some database maintenance or reconfigure operations`. This message is logged every five minutes as long as the cache is flushed within that time interval.

## Examples

### A. Set options on a database

The following example sets the recovery model and data page verification options for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database.

```sql
USE master;
GO
ALTER DATABASE [database_name]
SET RECOVERY FULL PAGE_VERIFY CHECKSUM;
GO
```

### B. Set the database to READ_ONLY

Changing the state of a database or file group to READ_ONLY or READ_WRITE requires exclusive access to the database. The following example sets the database to `SINGLE_USER` mode to obtain exclusive access. The example then sets the state of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to `READ_ONLY` and returns access to the database to all users.

> [!NOTE]  
> This example uses the termination option `WITH ROLLBACK IMMEDIATE` in the first `ALTER DATABASE` statement. All incomplete transactions will be rolled back and any other connections to the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database will be immediately disconnected.

```sql
USE master;
GO
ALTER DATABASE [database_name]
SET SINGLE_USER
WITH ROLLBACK IMMEDIATE;
GO
ALTER DATABASE [database_name]
SET READ_ONLY
GO
ALTER DATABASE [database_name]
SET MULTI_USER;
GO
```

### C. Enable snapshot isolation on a database

The following example enables the snapshot isolation framework option for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
USE [database_name];
USE master;
GO
ALTER DATABASE [database_name]
SET ALLOW_SNAPSHOT_ISOLATION ON;
GO
-- Check the state of the snapshot_isolation_framework
-- in the database.
SELECT name, snapshot_isolation_state,
    snapshot_isolation_state_desc AS description
FROM sys.databases
WHERE name = N'[database_name]';
GO
```

The result set shows that the snapshot isolation framework is enabled.

| name | snapshot_isolation_state | description |
| --- | --- | --- |
| [database_name] | 1 | ON |

### D. Enable, modify, or disable change tracking

The following example enables change tracking for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database and sets the retention period to `2` days.

```sql
ALTER DATABASE [database_name]
SET CHANGE_TRACKING = ON
(AUTO_CLEANUP = ON, CHANGE_RETENTION = 2 DAYS);
```

The following example shows how to change the retention period to `3` days.

```sql
ALTER DATABASE [database_name]
SET CHANGE_TRACKING (CHANGE_RETENTION = 3 DAYS);
```

The following example shows how to disable change tracking for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
ALTER DATABASE [database_name]
SET CHANGE_TRACKING = OFF;
```

### E. Enable the Query Store

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)])

The following example enables the Query Store and configures its parameters.

```sql
ALTER DATABASE [database_name]
SET QUERY_STORE = ON
    (
      OPERATION_MODE = READ_WRITE,
      CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = 90 ),
      DATA_FLUSH_INTERVAL_SECONDS = 900,
      QUERY_CAPTURE_MODE = AUTO,
      MAX_STORAGE_SIZE_MB = 1024,
      INTERVAL_LENGTH_MINUTES = 60
    );
```

### F. Enable the Query Store with wait statistics

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)])

The following example enables the Query Store and configures its parameters.

```sql
ALTER DATABASE [database_name]
SET QUERY_STORE = ON
    (
      OPERATION_MODE = READ_WRITE,
      CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = 90 ),
      DATA_FLUSH_INTERVAL_SECONDS = 900,
      MAX_STORAGE_SIZE_MB = 1024,
      INTERVAL_LENGTH_MINUTES = 60,
      SIZE_BASED_CLEANUP_MODE = AUTO,
      MAX_PLANS_PER_QUERY = 200,
      WAIT_STATS_CAPTURE_MODE = ON,
    );
```

### G. Enable the Query Store with custom capture policy options

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)])

The following example enables the Query Store and configures its parameters.

```sql
ALTER DATABASE [database_name]
SET QUERY_STORE = ON
    (
      OPERATION_MODE = READ_WRITE,
      CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = 90 ),
      DATA_FLUSH_INTERVAL_SECONDS = 900,
      MAX_STORAGE_SIZE_MB = 1024,
      INTERVAL_LENGTH_MINUTES = 60,
      SIZE_BASED_CLEANUP_MODE = AUTO,
      MAX_PLANS_PER_QUERY = 200,
      WAIT_STATS_CAPTURE_MODE = ON,
      QUERY_CAPTURE_MODE = CUSTOM,
      QUERY_CAPTURE_POLICY = (
        STALE_CAPTURE_POLICY_THRESHOLD = 24 HOURS,
        EXECUTION_COUNT = 30,
        TOTAL_COMPILE_CPU_TIME_MS = 1000,
        TOTAL_EXECUTION_CPU_TIME_MS = 100
      )
    );
```

## See also

- [Statistics](../../relational-databases/statistics/statistics.md)
- [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)
- [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [sys.data_spaces](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md)
- [sys.database_automatic_tuning_options](../../relational-databases/system-catalog-views/sys-database-automatic-tuning-options-transact-sql.md)
- [sys.database_automatic_tuning_mode](../../relational-databases/system-catalog-views/sys-database-automatic-tuning-mode-transact-sql.md)

## Next steps

- [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
- [ALTER DATABASE Database Mirroring](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md)
- [ALTER DATABASE SET HADR](../../t-sql/statements/alter-database-transact-sql-set-hadr.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [Enable and Disable Change Tracking](../../relational-databases/track-changes/enable-and-disable-change-tracking-sql-server.md)
- [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md)
- [SET TRANSACTION ISOLATION LEVEL](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md)
- [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [Best Practice with the Query Store](../../relational-databases/performance/best-practice-with-the-query-store.md)

::: moniker-end
::: moniker range="=azuresqldb-current"

:::row:::
    :::column:::
        [SQL Server](alter-database-transact-sql-set-options.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        ***\* SQL Database \**** &nbsp;
    :::column-end:::
    :::column:::
        [SQL Managed Instance](alter-database-transact-sql-set-options.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-database-transact-sql-set-options.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## SQL Database

Compatibility levels are `SET` options but are described in [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

> [!NOTE]  
> Many database set options can be configured for the current session by using [SET Statements](../../t-sql/statements/set-statements-transact-sql.md) and are often configured by applications when they connect. Session-level set options override the `ALTER DATABASE SET` values. The database options described in the following sections are values that can be set for sessions that don't explicitly provide other set option values.

## Syntax

```syntaxsql
ALTER DATABASE { database_name | Current }
SET
{
    <option_spec> [ ,...n ] [ WITH <termination> ]
}
;

<option_spec> ::=
{
    <auto_option>
  | <automatic_tuning_option>
  | <change_tracking_option>
  | <cursor_option>
  | <db_encryption_option>
  | <db_update_option>
  | <db_user_access_option>
  | <delayed_durability_option>
  | <parameterization_option>
  | <query_store_options>
  | <snapshot_option>
  | <sql_option>
  | <target_recovery_time_option>
  | <termination>
  | <temporal_history_retention>
}
;

<auto_option> ::=
{
    AUTO_CREATE_STATISTICS { OFF | ON [ ( INCREMENTAL = { ON | OFF } ) ] }
  | AUTO_SHRINK { ON | OFF }
  | AUTO_UPDATE_STATISTICS { ON | OFF }
  | AUTO_UPDATE_STATISTICS_ASYNC { ON | OFF }
}

<automatic_tuning_option> ::=
{
    AUTOMATIC_TUNING = { AUTO | INHERIT | CUSTOM }
  | AUTOMATIC_TUNING ( CREATE_INDEX = { DEFAULT | ON | OFF } )
  | AUTOMATIC_TUNING ( DROP_INDEX = { DEFAULT | ON | OFF } )
  | AUTOMATIC_TUNING ( FORCE_LAST_GOOD_PLAN = { DEFAULT | ON | OFF } )
}

<change_tracking_option> ::=
{
    CHANGE_TRACKING
    {
        = OFF
      | = ON [ ( <change_tracking_option_list > [,...n] ) ]
      | ( <change_tracking_option_list> [,...n] )
    }
}

<change_tracking_option_list> ::=
   {
       AUTO_CLEANUP = { ON | OFF }
     | CHANGE_RETENTION = retention_period { DAYS | HOURS | MINUTES }
   }

<cursor_option> ::=
{
    CURSOR_CLOSE_ON_COMMIT { ON | OFF }
}

<db_encryption_option> ::=
  ENCRYPTION { ON | OFF }

<db_update_option> ::=
  { READ_ONLY | READ_WRITE }

<db_user_access_option> ::=
  { RESTRICTED_USER | MULTI_USER }

<delayed_durability_option> ::= DELAYED_DURABILITY = { DISABLED | ALLOWED | FORCED }

<parameterization_option> ::=
  PARAMETERIZATION { SIMPLE | FORCED }

<query_store_options> ::=
{
  QUERY_STORE
  {
      = OFF
    | = ON [ ( <query_store_option_list> [,... n] ) ]
    | ( < query_store_option_list> [,... n] )
    | CLEAR [ ALL ]
  }
}

<query_store_option_list> ::=
{
  OPERATION_MODE = { READ_WRITE | READ_ONLY }
  | CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = number )
  | DATA_FLUSH_INTERVAL_SECONDS = number
  | MAX_STORAGE_SIZE_MB = number
  | INTERVAL_LENGTH_MINUTES = number
  | SIZE_BASED_CLEANUP_MODE = { AUTO | OFF }
  | QUERY_CAPTURE_MODE = { ALL | AUTO | CUSTOM | NONE }
  | MAX_PLANS_PER_QUERY = number
  | WAIT_STATS_CAPTURE_MODE = { ON | OFF }
  | QUERY_CAPTURE_POLICY = ( <query_capture_policy_option_list> [,...n] )
}

<query_capture_policy_option_list> :: =
{
    STALE_CAPTURE_POLICY_THRESHOLD = number { DAYS | HOURS }
    | EXECUTION_COUNT = number
    | TOTAL_COMPILE_CPU_TIME_MS = number
    | TOTAL_EXECUTION_CPU_TIME_MS = number
}

<snapshot_option> ::=
{
    ALLOW_SNAPSHOT_ISOLATION { ON | OFF }
  | READ_COMMITTED_SNAPSHOT { ON | OFF }
  | MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT { ON | OFF }
}
<sql_option> ::=
{
    ANSI_NULL_DEFAULT { ON | OFF }
  | ANSI_NULLS { ON | OFF }
  | ANSI_PADDING { ON | OFF }
  | ANSI_WARNINGS { ON | OFF }
  | ARITHABORT { ON | OFF }
  | COMPATIBILITY_LEVEL = { 160 | 150 | 140 | 130 | 120 | 110 | 100 }
  | CONCAT_NULL_YIELDS_NULL { ON | OFF }
  | NUMERIC_ROUNDABORT { ON | OFF }
  | QUOTED_IDENTIFIER { ON | OFF }
  | RECURSIVE_TRIGGERS { ON | OFF }
}

<termination>::=
{
    ROLLBACK AFTER integer [ SECONDS ]
  | ROLLBACK IMMEDIATE
  | NO_WAIT
}

<temporal_history_retention>::=TEMPORAL_HISTORY_RETENTION { ON | OFF }
```

## Arguments

#### *database_name*

Is the name of the database to be modified.

- CURRENT

  `CURRENT` runs the action in the current database. `CURRENT` isn't supported for all options in all contexts. If `CURRENT` fails, provide the database name.

#### \<auto_option> ::=

Controls automatic options.

#### <a id="auto_create_statistics"></a> AUTO_CREATE_STATISTICS { ON | OFF }

- ON

  Query Optimizer creates statistics on single columns in query predicates, as necessary, to improve query plans and query performance. These single-column statistics are created when Query Optimizer compiles queries. The single-column statistics are created only on columns that are not already the first column of an existing statistics object.

  The default is ON. We recommend that you use the default setting for most databases.

- OFF

  Query Optimizer doesn't create statistics on single columns in query predicates when it's compiling queries. Setting this option to OFF can cause suboptimal query plans and degraded query performance.

You can determine this option's status by examining the `is_auto_create_stats_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAutoCreateStatistics` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

For more information, see the "Statistics options" section in [Statistics](../../relational-databases/statistics/statistics.md#statistics-options).

#### INCREMENTAL = ON | OFF

Set AUTO_CREATE_STATISTICS to ON, and set INCREMENTAL to ON. This setting creates automatically created stats as incremental whenever incremental stats are supported. The default value is OFF. For more information, see [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md).

#### <a id="auto_shrink"></a> AUTO_SHRINK { ON | OFF }

- ON

  The database files are candidates for periodic shrinking. Unless you have a specific requirement, do not set the AUTO_SHRINK database option to ON. For more information, see [Shrink a database](../../relational-databases/databases/shrink-a-database.md).

Both data file and log files can be automatically shrunk. AUTO_SHRINK reduces the size of the transaction log only if you set the database to SIMPLE recovery model or if you back up the log. When set to OFF, the database files aren't automatically shrunk during periodic checks for unused space.

The AUTO_SHRINK option causes files to be shrunk when more than 25 percent of the file contains unused space. The option causes the file to shrink to one of two sizes. It shrinks to whichever is larger:

- The size where 25 percent of the file is unused space
- The size of the file when it was created

You can't shrink a read-only database.

- OFF

  The database files are not automatically shrunk during periodic checks for unused space.

You can determine this option's status by examining the `is_auto_shrink_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAutoShrink` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

> [!NOTE]  
> The AUTO_SHRINK option isn't available in a contained database.

#### <a id="auto_update_statistics"></a> AUTO_UPDATE_STATISTICS { ON | OFF }

- ON

  Specifies that Query Optimizer updates statistics when they're used by a query and when they might be out-of-date. Statistics become out-of-date after insert, update, delete, or merge operations change the data distribution in the table or indexed view. Query Optimizer determines when statistics might be out-of-date by counting the number of data modifications since the last statistics update and comparing the number of modifications to a threshold. The threshold is based on the number of rows in the table or indexed view.

  Query Optimizer checks for out-of-date statistics before it compiles a query and runs a cached query plan. Query Optimizer uses the columns, tables, and indexed views in the query predicate to determine which statistics might be out-of-date. Query Optimizer determines this information before it compiles a query. Before running a cached query plan, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] verifies that the query plan references up-to-date statistics.

  The AUTO_UPDATE_STATISTICS option applies to statistics created for indexes, single-columns in query predicates, and statistics that are created by using the CREATE STATISTICS statement. This option also applies to filtered statistics.

  The default is ON. We recommend that you use the default setting for most databases.

  Use the AUTO_UPDATE_STATISTICS_ASYNC option to specify whether the statistics are updated synchronously or asynchronously.

- OFF

  Specifies that Query Optimizer doesn't update statistics when they're used by a query. Query Optimizer also doesn't update statistics when they might be out-of-date. Setting this option to OFF can cause suboptimal query plans and degraded query performance.

  You can determine this option's status by examining the `is_auto_update_stats_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAutoUpdateStatistics` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

  For more information, see the "Statistics options" section in [Statistics](../../relational-databases/statistics/statistics.md#statistics-options).

#### <a id="auto_update_statistics_async"></a> AUTO_UPDATE_STATISTICS_ASYNC { ON | OFF }

- ON

  Specifies that statistics updates for the AUTO_UPDATE_STATISTICS option are asynchronous. Query Optimizer doesn't wait for statistics updates to complete before it compiles queries.

  Setting this option to ON has no effect unless AUTO_UPDATE_STATISTICS is set to ON.

  By default, the AUTO_UPDATE_STATISTICS_ASYNC option is set to OFF, and Query Optimizer updates statistics synchronously.

- OFF

  Specifies that statistics updates for the AUTO_UPDATE_STATISTICS option are synchronous. Query Optimizer waits for statistics updates to complete before it compiles queries.

  Setting this option to OFF has no effect unless AUTO_UPDATE_STATISTICS is set to ON.

You can determine this option's status by examining the `is_auto_update_stats_async_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

For more information that describes when to use synchronous or asynchronous statistics updates, see the "Statistics options" section in [Statistics](../../relational-databases/statistics/statistics.md#statistics-options).

#### <a name="auto_tuning"></a> \<automatic_tuning_option> ::=

Controls automatic options for [Automatic tuning](../../relational-databases/automatic-tuning/automatic-tuning.md). You can view the options for the following settings in the Azure portal or via T-SQL in the view `sys.database_automatic_tuning_options`.

#### AUTOMATIC_TUNING = { AUTO | INHERIT | CUSTOM }

- AUTO

  Setting the Automatic tuning value to AUTO will apply Azure configuration defaults for Automatic tuning.  In the Azure portal, this reflects the option to "Inherit from: Azure defaults".

- INHERIT

  Using the INHERIT value inherits the default configuration from the parent server. In the Azure portal, this reflects the option to "Inherit from: Server". This is especially useful if you would like to customize Automatic tuning configuration on a parent server, and have all the databases on such server INHERIT these custom settings. Please note that in order for the inheritance to work, the three individual tuning options FORCE_LAST_GOOD_PLAN, CREATE_INDEX, and DROP_INDEX need to be set to DEFAULT on databases.

- CUSTOM

  Using the CUSTOM value, you'll need to custom-configure each of the Automatic Tuning options available on databases. In the Azure portal, this reflects the option to "Inherit from: Don't inherit".

#### CREATE_INDEX = { DEFAULT | ON | OFF }

Enables or disables automatic index management `CREATE_INDEX` option of [Automatic tuning](../../relational-databases/automatic-tuning/automatic-tuning.md). You can view the status for this option in the Azure portal or via T-SQL in the view `sys.database_automatic_tuning_options`.

- DEFAULT

  Inherits default settings from the server. In this case, options of enabling or disabling individual Automatic tuning features are defined at the server level.

- ON

  When enabled, missing indexes are automatically generated on a database. Following the index creation, gains to the performance of the workload are verified. When such created index no longer provides benefits to the workload performance, it's automatically reverted. Indexes automatically created are flagged as a system generated indexed.

- OFF

  Doesn't automatically generate missing indexes on the database.

#### DROP_INDEX = { DEFAULT | ON | OFF }

Enables or disables automatic index management `DROP_INDEX` option of [Automatic Tuning](../../relational-databases/automatic-tuning/automatic-tuning.md). You can view the status for this option in the Azure portal or via T-SQL in the view `sys.database_automatic_tuning_options`.

- DEFAULT

  Inherits default settings from the server. In this case, options of enabling or disabling individual Automatic tuning features are defined at the server level.

- ON

  Automatically drops duplicate or no longer useful indexes to the performance workload.

- OFF

  Doesn't automatically drop missing indexes on the database.

#### FORCE_LAST_GOOD_PLAN = { DEFAULT | ON | OFF }

Enables or disables automatic plan correction `FORCE_LAST_GOOD_PLAN` option of [Automatic tuning](../../relational-databases/automatic-tuning/automatic-tuning.md). You can view the status for this option in the Azure portal or via T-SQL in the view `sys.database_automatic_tuning_options`.

- DEFAULT

  Inherits default settings from the server. In this case, options of enabling or disabling individual Automatic tuning features are defined at the server level. This is the default value. The default value for new Azure SQL servers is ON, meaning that by default, new databases will inherit the setting of ON.

- ON

  The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] automatically forces the last known good plan on the [!INCLUDE[tsql-md](../../includes/tsql-md.md)] queries where new query plan causes performance regressions. The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] continuously monitors query performance of the [!INCLUDE[tsql-md](../../includes/tsql-md.md)] query with the forced plan. If there are performance gains, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] will keep using last known good plan. If performance gains are not detected, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] will produce a new query plan. The statement will fail if the Query Store isn't enabled or if it's not in *Read-Write* mode.

- OFF

  The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] reports potential query performance regressions caused by query plan changes in [sys.dm_db_tuning_recommendations](../../relational-databases/system-dynamic-management-views/sys-dm-db-tuning-recommendations-transact-sql.md) view. However, these recommendations are not automatically applied. Users can monitor active recommendations and fix identified problems by applying [!INCLUDE[tsql-md](../../includes/tsql-md.md)] scripts that are shown in the view.

#### \<change_tracking_option> ::=

Controls change tracking options. You can enable change tracking, set options, change options, and disable change tracking. For examples, see the [Examples](#examples) section later in this article.

- ON

  Enables change tracking for the database. When you enable change tracking, you can also set the AUTO CLEANUP and CHANGE RETENTION options.

  - AUTO_CLEANUP = { ON | OFF }

    - ON

      Change tracking information is automatically removed after the specified retention period.

    - OFF

      Change tracking data isn't removed from the database.

  - CHANGE_RETENTION = *retention_period* { DAYS | HOURS | MINUTES }

    Specifies the minimum period for keeping change tracking information in the database. Data is removed only when the AUTO_CLEANUP value is ON.

    *retention_period* is an integer that specifies the numerical component of the retention period.

    The default retention period is **2 days**. The minimum retention period is 1 minute. The default retention type is **DAYS**.

- OFF

  Disables change tracking for the database. Disable change tracking on all tables before you disable change tracking off the database.

#### \<cursor_option> ::=

Controls cursor options.

#### CURSOR_CLOSE_ON_COMMIT { ON | OFF }

- ON

  Any cursors open when you commit or roll back a transaction are closed.

- OFF

  Cursors remain open when a transaction is committed; rolling back a transaction closes any cursors except those defined as INSENSITIVE or STATIC.

Connection-level settings that are set by using the SET statement override the default database setting for CURSOR_CLOSE_ON_COMMIT. ODBC and OLE DB clients issue a connection-level SET statement setting CURSOR_CLOSE_ON_COMMIT to OFF for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET CURSOR_CLOSE_ON_COMMIT](../../t-sql/statements/set-cursor-close-on-commit-transact-sql.md).

You can determine this option's status by examining the `is_cursor_close_on_commit_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or the `IsCloseCursorsOnCommitEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function. The cursor is implicitly deallocated only at disconnect. For more information, see [DECLARE CURSOR](../../t-sql/language-elements/declare-cursor-transact-sql.md).

#### \<db_encryption_option> ::=

Controls the database encryption state.

#### ENCRYPTION { ON | OFF }

Sets the database to be encrypted (ON) or not encrypted (OFF). For more information about database encryption, see [Transparent Data Encryption](../../relational-databases/security/encryption/transparent-data-encryption.md), and [Transparent Data Encryption with Azure SQL Database](/azure/azure-sql/database/transparent-data-encryption-tde-overview).

When encryption is enabled at the database level, all file groups will be encrypted. Any new file groups will inherit the encrypted property. If any file groups in the database are set to READ ONLY, the database encryption operation will fail.

You can see the encryption state of the database by using the [sys.dm_database_encryption_keys](../../relational-databases/system-dynamic-management-views/sys-dm-database-encryption-keys-transact-sql.md) dynamic management view.

#### \<db_update_option> ::=

Controls whether updates are allowed on the database.

- READ_ONLY

  Users can read data from the database but not modify it.

  > [!NOTE]  
  > To improve query performance, update statistics before setting a database to READ_ONLY. If additional statistics are needed after a database is set to READ_ONLY, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] will create statistics in `tempdb`. For more information about statistics for a read-only database, see [Statistics](../../relational-databases/statistics/statistics.md).

- READ_WRITE

  The database is available for read and write operations.

To change this state, you must have exclusive access to the database. For more information, see the SINGLE_USER clause.

> [!NOTE]  
> On [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] federated databases, `SET { READ_ONLY | READ_WRITE }` is disabled.

#### \<db_user_access_option> ::=

Controls user access to the database.

- RESTRICTED_USER

  Allows for only members of the `db_owner` fixed database role and `dbcreator` and `sysadmin` fixed server roles to connect to the database, but doesn't limit their number. All connections to the database are disconnected in the timeframe specified by the termination clause of the ALTER DATABASE statement. After the database has transitioned to the RESTRICTED_USER state, connection attempts by unqualified users are refused.  In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], should be executed from within the user database. From the `master` database, you may encounter an error message `Msg 42008, Level 16, State 3, Line 1 ODBC error: State: 28000: Error: 18456 Message:'[Microsoft][ODBC Driver 17 for SQL Server][SQL Server]Login failed for user '##MS_InstanceCertificate##'.'.`

- MULTI_USER

  All users that have the appropriate permissions to connect to the database are allowed. You can determine this option's status by examining the `user_access` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or the `UserAccess` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.  In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], should be executed from within the user database. From the `master` database, you may encounter an error message `Msg 42008, Level 16, State 3, Line 1 ODBC error: State: 28000: Error: 18456 Message:'[Microsoft][ODBC Driver 17 for SQL Server][SQL Server]Login failed for user '##MS_InstanceCertificate##'.'.`

#### \<delayed_durability_option> ::=

Controls whether transactions commit fully durable or delayed durable.

- DISABLED

  All transactions following `SET DISABLED` are fully durable. Any durability options set in an atomic block or commit statement are ignored.

- ALLOWED

  All transactions following `SET ALLOWED` are either fully durable or delayed durable, depending upon the durability option set in the atomic block or commit statement.

- FORCED

  All transactions following `SET FORCED` are delayed durable. Any durability options set in an atomic block or commit statement are ignored.

#### \<PARAMETERIZATION_option> ::=

Controls the parameterization option.

#### PARAMETERIZATION { SIMPLE | FORCED }

- SIMPLE

  Queries are parameterized based on the default behavior of the database.

- FORCED

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] parameterizes all queries in the database.

The current setting of this option can be determined by examining the `is_parameterization_forced` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

#### <a name="query-store"></a> \<query_store_options> ::=

- ON | OFF | CLEAR [ ALL ]

  Controls whether the Query Store is enabled in this database, and also controls removing the contents of the Query Store.

  - ON

    Enables the Query Store. ON is the default value.

  - OFF

    Disables the Query Store.

    > [!NOTE]  
    > Query Store cannot be disabled in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] single database and Elastic Pool. Executing `ALTER DATABASE [database] SET QUERY_STORE = OFF` will return the warning `'QUERY_STORE=OFF' is not supported in this version of SQL Server.`.

  - CLEAR

    Remove the contents of the Query Store.

#### OPERATION_MODE

Describes the operation mode of the Query Store. Valid values are READ_ONLY and READ_WRITE. In READ_WRITE mode, the Query Store collects and persists query plan and runtime execution statistics information. In READ_ONLY mode, information can be read from the Query Store, but new information isn't added. If the maximum allocated space of the Query Store has been exhausted, the Query Store will change is operation mode to READ_ONLY.

#### CLEANUP_POLICY

Describes the data retention policy of the Query Store. STALE_QUERY_THRESHOLD_DAYS determines the number of days for which the information for a query is kept in the Query Store. STALE_QUERY_THRESHOLD_DAYS is type **bigint**. The default value is 30. For [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)] Basic edition, default is **7** days.

#### DATA_FLUSH_INTERVAL_SECONDS

Determines the frequency at which data written to the Query Store is persisted to disk. To optimize for performance, data collected by the Query Store is asynchronously written to the disk. The frequency at which this asynchronous transfer occurs is configured by using the DATA_FLUSH_INTERVAL_SECONDS argument. DATA_FLUSH_INTERVAL_SECONDS is type **bigint**. The default value is **900** (15 min).

#### MAX_STORAGE_SIZE_MB

Determines the space allocated to the Query Store. MAX_STORAGE_SIZE_MB is type **bigint**. For [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)] Premium edition, default is **1000 MB** and for [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)] Basic edition, default is **10 MB**.

> [!NOTE]  
> `MAX_STORAGE_SIZE_MB` setting limit is 10,240 MB on [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

> [!NOTE]  
> `MAX_STORAGE_SIZE_MB` limit isn't strictly enforced. Storage size is checked only when Query Store writes data to disk. This interval is set by the `DATA_FLUSH_INTERVAL_SECONDS` option or the [!INCLUDE[ssManStudio](../../includes/ssManStudio-md.md)] Query Store dialog option **Data Flush Interval**. The interval default value is 900 seconds (or 15 minutes).
> If the Query Store has breached the `MAX_STORAGE_SIZE_MB` limit between storage size checks, it will transition to read-only mode. If `SIZE_BASED_CLEANUP_MODE` is enabled, the cleanup mechanism to enforce the `MAX_STORAGE_SIZE_MB` limit is also triggered.
> Once enough space has been cleared, the Query Store mode will automatically switch back to read-write.

> [!IMPORTANT]  
> If you think that your workload capture will need more than 10 GB of disk space, you should probably rethink and optimize your workload to reuse query plans (for example using [forced parameterization](../../relational-databases/query-processing-architecture-guide.md#forced-parameterization), or adjust the Query Store configurations.
> Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], you can set `QUERY_CAPTURE_MODE` to CUSTOM for additional control over the query capture policy.

#### INTERVAL_LENGTH_MINUTES

Determines the time interval at which runtime execution statistics data is aggregated into the Query Store. To optimize for space usage, the runtime execution statistics in the runtime stats store are aggregated over a fixed time window. This fixed time window is configured by using the INTERVAL_LENGTH_MINUTES argument. INTERVAL_LENGTH_MINUTES is type **bigint**. The default value is **60**.

#### SIZE_BASED_CLEANUP_MODE = { AUTO | OFF }

Controls whether cleanup will be automatically activated when the total amount of data gets close to maximum size.

- OFF

  Size-based cleanup won't be automatically activated.

- AUTO

  Size-based cleanup will be automatically activated when size on disk reaches 90% of **max_storage_size_mb**. Size-based cleanup removes the least expensive and oldest queries first. It stops at approximately 80% of **max_storage_size_mb**. This is the default configuration value.

SIZE_BASED_CLEANUP_MODE is type **nvarchar**.

#### QUERY_CAPTURE_MODE { ALL | AUTO | CUSTOM | NONE }

Designates the currently active query capture mode. Each mode defines specific query capture policies.

> [!NOTE]  
> Cursors, queries inside Stored Procedures, and Natively compiled queries are always captured when the query capture mode is set to ALL, AUTO, or CUSTOM.

- ALL

  Captures all queries.

- AUTO

  Capture relevant queries based on execution count and resource consumption. This is the default configuration value for [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

- NONE

  Stop capturing new queries. The Query Store will continue to collect compile and runtime statistics for queries that were captured already. Use this configuration with caution since you may miss capturing important queries.

- CUSTOM

  Allows control over the QUERY_CAPTURE_POLICY options.

QUERY_CAPTURE_MODE is type **nvarchar**.

#### MAX_PLANS_PER_QUERY

Defines the maximum number of plans maintained for each query. MAX_PLANS_PER_QUERY is type **int**. The default value is **200**.

#### WAIT_STATS_CAPTURE_MODE { ON | OFF }

Controls whether wait statistics will be captured per query.

- ON

  Wait statistics information per query is captured. This value is the default configuration value.

- OFF

  Wait statistics information per query won't be captured.

#### \<query_capture_policy_option_list> :: =

Controls the Query Store capture policy options. Except for STALE_CAPTURE_POLICY_THRESHOLD, these options define the OR conditions that need to happen for queries to be captured in the defined Stale Capture Policy Threshold value.

#### STALE_CAPTURE_POLICY_THRESHOLD = *integer* { DAYS | HOURS }

Defines the evaluation interval period to determine if a query should be captured. The default is 1 day, and it can be set from 1 hour to seven days. *number* is type **int**.

#### EXECUTION_COUNT = *integer*

Defines the number of times a query is executed over the evaluation period. The default is 30, which means that for the default Stale Capture Policy Threshold, a query must execute at least 30 times in one day to be persisted in the Query Store. EXECUTION_COUNT is type **int**.

#### TOTAL_COMPILE_CPU_TIME_MS = *integer*

Defines total elapsed compile CPU time used by a query over the evaluation period. The default is 1000, which means that for the default Stale Capture Policy Threshold, a query must have a total of at least one second of CPU time spent during query compilation in one day to be persisted in the Query Store. TOTAL_COMPILE_CPU_TIME_MS is type **int**.

#### TOTAL_EXECUTION_CPU_TIME_MS = *integer*

Defines total elapsed execution CPU time used by a query over the evaluation period. The default is 100 which means that for the default Stale Capture Policy Threshold, a query must have a total of at least 100 ms of CPU time spent during execution in one day to be persisted in the Query Store. TOTAL_EXECUTION_CPU_TIME_MS is type **int**.

#### \<snapshot_option> ::=

Determines the transaction isolation level.

#### ALLOW_SNAPSHOT_ISOLATION { ON | OFF }

- ON

  Enables Snapshot option at the database level. When it's enabled, DML statements start generating row versions even when no transaction uses Snapshot Isolation. Once this option is enabled, transactions can specify the SNAPSHOT transaction isolation level. When a transaction runs at the SNAPSHOT isolation level, all statements see a snapshot of data as it exists at the start of the transaction. If a transaction running at the SNAPSHOT isolation level accesses data in multiple databases, either ALLOW_SNAPSHOT_ISOLATION must be set to ON in all the databases, or each statement in the transaction must use locking hints on any reference in a FROM clause to a table in a database where ALLOW_SNAPSHOT_ISOLATION is OFF.

- OFF

  Turns off the Snapshot option at the database level. Transactions can't specify the SNAPSHOT transaction isolation level.

When you set ALLOW_SNAPSHOT_ISOLATION to a new state (from ON to OFF, or from OFF to ON), ALTER DATABASE doesn't return control to the caller until all existing transactions in the database are committed. If the database is already in the state specified in the ALTER DATABASE statement, control is returned to the caller immediately. If the ALTER DATABASE statement doesn't return quickly, use [sys.dm_tran_active_snapshot_database_transactions](../../relational-databases/system-dynamic-management-views/sys-dm-tran-active-snapshot-database-transactions-transact-sql.md) to determine whether there are long-running transactions. If the ALTER DATABASE statement is canceled, the database remains in the state it was in when ALTER DATABASE was started. The [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view indicates the state of snapshot-isolation transactions in the database. If `snapshot_isolation_state_desc` = IN_TRANSITION_TO_ON, the statement `ALTER DATABASE .... ALLOW_SNAPSHOT_ISOLATION OFF` will pause six seconds and retry the operation.

You can't change the state of ALLOW_SNAPSHOT_ISOLATION if the database is OFFLINE.

If you set ALLOW_SNAPSHOT_ISOLATION in a READ_ONLY database, the setting will be kept if the database is later set to READ_WRITE.

The current setting of this option can be determined by examining the `snapshot_isolation_state` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

#### READ_COMMITTED_SNAPSHOT { ON | OFF }

- ON

  Enables Read-Committed Snapshot option at the database level. When it's enabled, DML statements start generating row versions even when no transaction uses Snapshot Isolation. Once this option is enabled, the transactions specifying the READ COMMITTED isolation level use row versioning instead of locking. All statements see a snapshot of data as it exists at the start of the statement when a transaction runs at the READ COMMITTED isolation level.

- OFF

  Turns off Read-Committed Snapshot option at the database level. Transactions specifying the READ COMMITTED isolation level use locking.

To set READ_COMMITTED_SNAPSHOT ON or OFF, there must be no active connections to the database except for the connection running the ALTER DATABASE command. However, the database doesn't have to be in single-user mode. You can't change the state of this option when the database is OFFLINE.

If you set READ_COMMITTED_SNAPSHOT in a READ_ONLY database, the setting will be kept when the database is later set to READ_WRITE.

READ_COMMITTED_SNAPSHOT can't be turned ON for the `master`, `tempdb`, or `msdb` system databases. If you change the setting for `model`, that setting becomes the default for any new databases created, except for `tempdb`.

The current setting of this option can be determined by examining the `is_read_committed_snapshot_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

> [!WARNING]  
> When a table is created with `DURABILITY = SCHEMA_ONLY`, and **READ_COMMITTED_SNAPSHOT** is subsequently changed using `ALTER DATABASE`, data in the table will be lost.

> [!TIP]  
> In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], the `ALTER DATABASE` command to set READ_COMMITTED_SNAPSHOT ON or OFF for a database must be executed in the `master` database.

#### MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT { ON | OFF }

- ON

  When the transaction isolation level is set to any isolation level lower than SNAPSHOT, all interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] operations on memory-optimized tables are run under SNAPSHOT isolation. Examples of isolation levels lower than snapshot are READ COMMITTED or READ UNCOMMITTED. These operations run whether the transaction isolation level is set explicitly at the session level, or the default is used implicitly.

- OFF

  Doesn't elevate the transaction isolation level for interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] operations on memory-optimized tables.

You can't change the state of MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT if the database is OFFLINE.

The default value is OFF.

The current setting of this option can be determined by examining the `is_memory_optimized_elevate_to_snapshot_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

#### \<sql_option> ::=

Controls the ANSI compliance options at the database level.

#### ANSI_NULL_DEFAULT { ON | OFF }

Determines the default value, NULL or NOT NULL, of a column or [CLR user-defined type](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md) for which the nullability isn't explicitly defined in CREATE TABLE or ALTER TABLE statements. Columns that are defined with constraints follow constraint rules whatever this setting may be.

- ON

  The default value is NULL.

- OFF

  The default value is NOT NULL.

Connection-level settings that are set by using the SET statement override the default database-level setting for ANSI_NULL_DEFAULT. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_NULL_DEFAULT to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_NULL_DFLT_ON](../../t-sql/statements/set-ansi-null-dflt-on-transact-sql.md).

For ANSI compatibility, setting the database option ANSI_NULL_DEFAULT to ON changes the database default to NULL.

You can determine this option's status by examining the `is_ansi_null_default_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAnsiNullDefault` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### ANSI_NULLS { ON | OFF }

- ON

  All comparisons to a null value evaluate to UNKNOWN.

- OFF

  Comparisons of non-Unicode values to a null value evaluate to TRUE if both values are NULL.

> [!IMPORTANT]  
> In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], ANSI_NULLS will always be ON and any applications that explicitly set the option to OFF will produce an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.

Connection-level settings that are set by using the SET statement override the default database setting for ANSI_NULLS. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_NULLS to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_NULLS](../../t-sql/statements/set-ansi-nulls-transact-sql.md).

> [!NOTE]  
> SET ANSI_NULLS also must be set to ON when you create or make changes to indexes on computed columns or indexed views.

You can determine this option's status by examining the `is_ansi_nulls_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAnsiNullsEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### ANSI_PADDING { ON | OFF }

- ON

  Strings are padded to the same length before conversion. Also padded to the same length before inserting to a **varchar** or **nvarchar** data type.

- OFF

  Inserts trailing blanks in character values into **varchar** or **nvarchar** columns. Also leaves trailing zeros in binary values that are inserted into **varbinary** columns. Values aren't padded to the length of the column.

  When OFF is specified, this setting affects only the definition of new columns.

> [!IMPORTANT]  
> In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], ANSI_PADDING will always be ON and any applications that explicitly set the option to OFF will produce an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. We recommend that you always set ANSI_PADDING to ON. ANSI_PADDING must be ON when you create or manipulate indexes on computed columns or indexed views.

**char(*n*)** and **binary(*n*)** columns that allow for nulls are padded to the column length when ANSI_PADDING is set to ON. Trailing blanks and zeros are trimmed when ANSI_PADDING is OFF. **char(_n_)** and **binary(_n_)** columns that don't allow nulls are always padded to the length of the column.

Connection-level settings that are set by using the SET statement override the default database-level setting for ANSI_PADDING. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_PADDING to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_PADDING](../../t-sql/statements/set-ansi-padding-transact-sql.md).

You can determine this option's status by examining the `is_ansi_padding_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAnsiPaddingEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### ANSI_WARNINGS { ON | OFF }

- ON

  Errors or warnings are issued when conditions such as divide-by-zero occur. Errors and warnings are also issued when null values appear in aggregate functions.

- OFF

  No warnings are raised and null values are returned when conditions such as divide-by-zero occur.

> [!NOTE]  
> SET ANSI_WARNINGS must be set to ON when you create or make changes to indexes on computed columns or indexed views.

Connection-level settings that are set by using the SET statement override the default database setting for ANSI_WARNINGS. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_WARNINGS to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_WARNINGS](../../t-sql/statements/set-ansi-warnings-transact-sql.md).

You can determine this option's status by examining the `is_ansi_warnings_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAnsiWarningsEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### ARITHABORT { ON | OFF }

- ON

  A query is ended when an overflow or divide-by-zero error occurs during query execution.

- OFF

  A warning message is displayed when one of these errors occurs. The query, batch, or transaction continues to process as if no error occurred even if a warning is displayed.

> [!NOTE]  
> SET ARITHABORT must be set to ON when you create or make changes to indexes on computed columns or indexed views.

You can determine this option's status by examining the `is_arithabort_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsArithmeticAbortEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### COMPATIBILITY_LEVEL = { 160 | 150 | 140 | 130 | 120 | 110 | 100 }

For more information, see [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

#### CONCAT_NULL_YIELDS_NULL { ON | OFF }

- ON

  The result of a concatenation operation is NULL when either operand is NULL. For example, concatenating the character string "This is" and NULL causes the value NULL, instead of the value "This is".

- OFF

  The null value is treated as an empty character string.

> [!NOTE]  
> CONCAT_NULL_YIELDS_NULL must be set to ON when you create or make changes to indexes on computed columns or indexed views.
>  
> In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], CONCAT_NULL_YIELDS_NULL will always be ON and any applications that explicitly set the option to OFF will produce an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.

Connection-level settings that are set by using the SET statement override the default database setting for CONCAT_NULL_YIELDS_NULL. By default, ODBC and OLE DB clients issue a connection-level SET statement setting CONCAT_NULL_YIELDS_NULL to ON for the session when connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET CONCAT_NULL_YIELDS_NULL](../../t-sql/statements/set-concat-null-yields-null-transact-sql.md).

You can determine this option's status by examining the `is_concat_null_yields_null_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsNullConcat` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### NUMERIC_ROUNDABORT { ON | OFF }

- ON

  An error is generated when loss of precision occurs in an expression.

- OFF

  Loss of precision doesn't generate an error message, and the result is rounded to the precision of the column or variable storing the result.

> [!IMPORTANT]  
> NUMERIC_ROUNDABORT must be set to OFF when you create or make changes to indexes on computed columns or indexed views.

You can determine the status for this option in the `is_numeric_roundabort_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsNumericRoundAbortEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### QUOTED_IDENTIFIER { ON | OFF }

- ON

  Double quotation marks can be used to enclose delimited identifiers.

All strings delimited by double quotation marks are interpreted as object identifiers. Quoted identifiers don't have to follow the [!INCLUDE[tsql](../../includes/tsql-md.md)] rules for identifiers. They can be keywords and can include characters not allowed in [!INCLUDE[tsql](../../includes/tsql-md.md)] identifiers. If a single quotation mark (') is part of the literal string, it can be represented by double quotation marks (").

- OFF

  Identifiers can't be in quotation marks and must follow all [!INCLUDE[tsql](../../includes/tsql-md.md)] rules for identifiers. Literals can be delimited by either single or double quotation marks.

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also allows for identifiers to be delimited by square brackets ([ ]). Bracketed identifiers can always be used, whatever the QUOTED_IDENTIFIER setting is. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).

When a table is created, the QUOTED IDENTIFIER option is always stored as ON in the metadata of the table. The option is stored even if the option is set to OFF when the table is created.

Connection-level settings that are set by using the SET statement override the default database setting for QUOTED_IDENTIFIER. ODBC and OLE DB clients issue a connection-level SET statement setting QUOTED_IDENTIFIER to ON, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET QUOTED_IDENTIFIER](../../t-sql/statements/set-quoted-identifier-transact-sql.md).

You can determine this option's status by examining the `is_quoted_identifier_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsQuotedIdentifiersEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### RECURSIVE_TRIGGERS { ON | OFF }

- ON

  Recursive firing of AFTER triggers is allowed.

- OFF

  You can determine this option's status by examining the `is_recursive_triggers_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsRecursiveTriggersEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

> [!NOTE]  
> Only direct recursion is prevented when RECURSIVE_TRIGGERS is set to OFF. To disable indirect recursion, you must also set the nested triggers server option to 0.

You can determine this option's status by examining the `is_recursive_triggers_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or the `IsRecursiveTriggersEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### \<target_recovery_time_option> ::=

Specifies the frequency of indirect checkpoints on a per-database basis. Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] the default value for new databases is **1 minute**, which indicates database will use indirect checkpoints. For older versions the default is 0, which indicates that the database will use automatic checkpoints, whose frequency depends on the recovery interval setting of the server instance. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends 1 minute for most systems.

#### TARGET_RECOVERY_TIME = *target_recovery_time* { SECONDS | MINUTES }

- *target_recovery_time*

  Specifies the maximum bound on the time to recover the specified database in the event of a crash. *target_recovery_time* is type **int**.

- SECONDS

  Indicates that *target_recovery_time* is expressed as the number of seconds.

- MINUTES

  Indicates that *target_recovery_time* is expressed as the number of minutes.

For more information about indirect checkpoints, see [Database Checkpoints](../../relational-databases/logs/database-checkpoints-sql-server.md).

#### WITH \<termination> ::=

Specifies when to roll back incomplete transactions when the database is transitioned from one state to another. If the termination clause is omitted, the ALTER DATABASE statement waits indefinitely if there's any lock on the database. Only one termination clause can be specified, and it follows the SET clauses.

> [!NOTE]  
> Not all database options use the WITH \<termination> clause. For more information, see the table under [Setting options](#SettingOptions) of the "Remarks" section of this article.

- ROLLBACK AFTER *integer* [SECONDS] | ROLLBACK IMMEDIATE

  Specifies whether to roll back after the specified number of seconds or immediately.

- NO_WAIT

  Specifies that the request will fail if the requested database state or option change can't complete immediately. Completing immediately means not waiting for transactions to commit or roll back on their own.

#### \<temporal_history_retention> ::=

- TEMPORAL_HISTORY_RETENTION { ON | OFF }

  ON by default but also automatically set to OFF after point in time restore operation. For more information including how to enable this setting, see [How to configure retention policy](../../relational-databases/tables/manage-retention-of-historical-data-in-system-versioned-temporal-tables.md#how-to-configure-retention-policy).

  - ON

    Default. Enables temporal table retention policy. For more information, see [Manage retention of historical data in system-versioned temporal tables](../../relational-databases/tables/manage-retention-of-historical-data-in-system-versioned-temporal-tables.md).

  - OFF

    Do not perform temporal historical retention policy.

## <a id="SettingOptions"></a> Set options

To retrieve current settings for database options, use the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)

After you set a database option, the new setting takes effect immediately.

You can change the default values for any one of the database options for all newly created databases. To do so, change the appropriate database option in the `model` database.

Not all database options use the WITH \<termination> clause or can be specified in combination with other options. The following table lists these options and their option and termination status.

| Options category | Can be specified with other options | Can use the WITH \<termination> clause |
| --- | --- | --- |
| \<auto_option> | Yes | No |
| \<change_tracking_option> | Yes | Yes |
| \<cursor_option> | Yes | No |
| \<db_encryption_option> | Yes | No |
| \<db_update_option> | Yes | Yes |
| \<db_user_access_option> | Yes | Yes |
| \<delayed_durability_option> | Yes | Yes |
| \<parameterization_option> | Yes | Yes |
| ALLOW_SNAPSHOT_ISOLATION | No | No |
| READ_COMMITTED_SNAPSHOT | No | Yes |
| MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT | Yes | Yes |
| DATE_CORRELATION_OPTIMIZATION | Yes | Yes |
| \<sql_option> | Yes | No |
| \<target_recovery_time_option> | No | Yes |

## Examples

### A. Set the database to READ_ONLY

Changing the state of a database or file group to READ_ONLY or READ_WRITE requires exclusive access to the database and may take a few seconds to complete. The following example sets the database to `RESTRICTED_USER` mode to limit access. The example then sets the state of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to `READ_ONLY` and returns access to the database to all users.

```sql
--Connect to [database_name];
GO
ALTER DATABASE [database_name]
SET RESTRICTED_USER;
GO
ALTER DATABASE [database_name]
SET READ_ONLY
--`SET READ_ONLY` command may take a few seconds to complete.
GO
ALTER DATABASE [database_name]
SET MULTI_USER;
GO
```

To set the database back to read-write mode:

```sql
--Connect to [database_name];
GO
ALTER DATABASE [database_name]
SET READ_WRITE
GO
```

To verify:

```sql
SELECT [name], user_access_desc, is_read_only FROM sys.databases
WHERE [name] = 'database_name'
GO
```

### B. Enable snapshot isolation on a database

The following example enables the snapshot isolation framework option for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
--Connect to [database_name]
GO
ALTER DATABASE [database_name]
SET ALLOW_SNAPSHOT_ISOLATION ON;
GO
```

Verify the state of the `snapshot_isolation_framework` in the database.

```sql
--Connect to [database_name]
SELECT name, snapshot_isolation_state,
    snapshot_isolation_state_desc AS description
FROM sys.databases
WHERE name = N'database_name';
GO
```

The result set shows that the snapshot isolation framework is enabled.

| name | snapshot_isolation_state | description |
| --- | --- | --- |
| [database_name] | 1 | ON |

### C. Enable, modify, or disable change tracking

The following example enables change tracking for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database and sets the retention period to `2` days.

```sql
--Connect to [database_name]
ALTER DATABASE [database_name]
SET CHANGE_TRACKING = ON
(AUTO_CLEANUP = ON, CHANGE_RETENTION = 2 DAYS);
```

The following example shows how to change the retention period to 3 days.

```sql
--Connect to [database_name]
ALTER DATABASE [database_name]
SET CHANGE_TRACKING (CHANGE_RETENTION = 3 DAYS);
```

The following example shows how to disable change tracking for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
--Connect to [database_name]
ALTER DATABASE [database_name]
SET CHANGE_TRACKING = OFF;
```

### D. Enable the Query Store

The following example enables the Query Store and configures Query Store parameters.

```sql
--Connect to [database_name]
ALTER DATABASE [database_name]
SET QUERY_STORE = ON
    (
      OPERATION_MODE = READ_WRITE,
      CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = 90 ),
      DATA_FLUSH_INTERVAL_SECONDS = 900,
      QUERY_CAPTURE_MODE = AUTO,
      MAX_STORAGE_SIZE_MB = 1024,
      INTERVAL_LENGTH_MINUTES = 60
    );
```

### E. Enable the Query Store with wait statistics

The following example enables the Query Store and configures its parameters.

```sql
--Connect to [database_name]
ALTER DATABASE [database_name]
SET QUERY_STORE = ON
    (
      OPERATION_MODE = READ_WRITE,
      CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = 90 ),
      DATA_FLUSH_INTERVAL_SECONDS = 900,
      MAX_STORAGE_SIZE_MB = 1024,
      INTERVAL_LENGTH_MINUTES = 60,
      SIZE_BASED_CLEANUP_MODE = AUTO,
      MAX_PLANS_PER_QUERY = 200,
      WAIT_STATS_CAPTURE_MODE = ON
    );
```

### F. Enable the Query Store with custom capture policy options

The following example enables the Query Store and configures its parameters.

```sql
--Connect to [database_name]
ALTER DATABASE [database_name]
SET QUERY_STORE = ON
    (
      OPERATION_MODE = READ_WRITE,
      CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = 90 ),
      DATA_FLUSH_INTERVAL_SECONDS = 900,
      MAX_STORAGE_SIZE_MB = 1024,
      INTERVAL_LENGTH_MINUTES = 60,
      SIZE_BASED_CLEANUP_MODE = AUTO,
      MAX_PLANS_PER_QUERY = 200,
      WAIT_STATS_CAPTURE_MODE = ON,
      QUERY_CAPTURE_MODE = CUSTOM,
      QUERY_CAPTURE_POLICY = (
        STALE_CAPTURE_POLICY_THRESHOLD = 24 HOURS,
        EXECUTION_COUNT = 30,
        TOTAL_COMPILE_CPU_TIME_MS = 1000,
        TOTAL_EXECUTION_CPU_TIME_MS = 100
      )
    );
```

## See also

- [Statistics](../../relational-databases/statistics/statistics.md)
- [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)
- [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [sys.data_spaces](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md)
- [sys.database_automatic_tuning_options](../../relational-databases/system-catalog-views/sys-database-automatic-tuning-options-transact-sql.md)
- [sys.database_automatic_tuning_mode](../../relational-databases/system-catalog-views/sys-database-automatic-tuning-mode-transact-sql.md)

## Next steps

- [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
- [ALTER DATABASE Database Mirroring](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [Enable and Disable Change Tracking](../../relational-databases/track-changes/enable-and-disable-change-tracking-sql-server.md)
- [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md)
- [SET TRANSACTION ISOLATION LEVEL](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md)
- [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [Best Practice with the Query Store](../../relational-databases/performance/best-practice-with-the-query-store.md)
- [Query Store hints](../../relational-databases/performance/query-store-hints.md)

::: moniker-end
::: moniker range="=azuresqldb-mi-current"

:::row:::
    :::column:::
        [SQL Server](alter-database-transact-sql-set-options.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Database](alter-database-transact-sql-set-options.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        ***\* SQL Managed Instance \**** &nbsp;
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-database-transact-sql-set-options.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## Azure SQL Managed Instance

Compatibility levels are `SET` options but are described in [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

> [!NOTE]  
> Many database set options can be configured for the current session by using [SET Statements](../../t-sql/statements/set-statements-transact-sql.md) and are often configured by applications when they connect. Session-level set options override the `ALTER DATABASE SET` values. The database options described in the following sections are values that can be set for sessions that don't explicitly provide other set option values.

## Syntax

```syntaxsql
ALTER DATABASE { database_name | Current }
SET
{
    <optionspec> [ ,...n ]
}
;

<optionspec> ::=
{
    <auto_option>
  | <change_tracking_option>
  | <cursor_option>
  | <db_encryption_option>
  | <delayed_durability_option>
  | <parameterization_option>
  | <query_store_options>
  | <snapshot_option>
  | <sql_option>
  | <target_recovery_time_option>
  | <termination>
  | <temporal_history_retention>
}
;
<auto_option> ::=
{
    AUTO_CREATE_STATISTICS { OFF | ON [ ( INCREMENTAL = { ON | OFF } ) ] }
  | AUTO_SHRINK { ON | OFF }
  | AUTO_UPDATE_STATISTICS { ON | OFF }
  | AUTO_UPDATE_STATISTICS_ASYNC { ON | OFF }
}

<automatic_tuning_option> ::=
{
    AUTOMATIC_TUNING ( FORCE_LAST_GOOD_PLAN = { DEFAULT | ON | OFF } )
}

<change_tracking_option> ::=
{
    CHANGE_TRACKING
    {
       = OFF
     | = ON [ ( <change_tracking_option_list > [,...n] ) ]
     | ( <change_tracking_option_list> [,...n] )
    }
}

<change_tracking_option_list> ::=
   {
       AUTO_CLEANUP = { ON | OFF }
     | CHANGE_RETENTION = retention_period { DAYS | HOURS | MINUTES }
   }

<cursor_option> ::=
{
    CURSOR_CLOSE_ON_COMMIT { ON | OFF }
}

<db_encryption_option> ::=
  ENCRYPTION { ON | OFF }

<delayed_durability_option> ::=DELAYED_DURABILITY = { DISABLED | ALLOWED | FORCED }

<parameterization_option> ::=
  PARAMETERIZATION { SIMPLE | FORCED }

<query_store_options> ::=
{
  QUERY_STORE
  {
    = OFF
    | = ON [ ( <query_store_option_list> [,... n] ) ]
    | ( < query_store_option_list> [,... n] )
    | CLEAR [ ALL ]
  }
}

<query_store_option_list> ::=
{
  OPERATION_MODE = { READ_WRITE | READ_ONLY }
  | CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = number )
  | DATA_FLUSH_INTERVAL_SECONDS = number
  | MAX_STORAGE_SIZE_MB = number
  | INTERVAL_LENGTH_MINUTES = number
  | SIZE_BASED_CLEANUP_MODE = { AUTO | OFF }
  | QUERY_CAPTURE_MODE = { ALL | AUTO | CUSTOM | NONE }
  | MAX_PLANS_PER_QUERY = number
  | WAIT_STATS_CAPTURE_MODE = { ON | OFF }
  | QUERY_CAPTURE_POLICY = ( <query_capture_policy_option_list> [,...n] )
}

<query_capture_policy_option_list> :: =
{
    STALE_CAPTURE_POLICY_THRESHOLD = number { DAYS | HOURS }
    | EXECUTION_COUNT = number
    | TOTAL_COMPILE_CPU_TIME_MS = number
    | TOTAL_EXECUTION_CPU_TIME_MS = number
}

<snapshot_option> ::=
{
    ALLOW_SNAPSHOT_ISOLATION { ON | OFF }
  | READ_COMMITTED_SNAPSHOT { ON | OFF }
  | MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT { ON | OFF }
}
<sql_option> ::=
{
    ANSI_NULL_DEFAULT { ON | OFF }
  | ANSI_NULLS { ON | OFF }
  | ANSI_PADDING { ON | OFF }
  | ANSI_WARNINGS { ON | OFF }
  | ARITHABORT { ON | OFF }
  | COMPATIBILITY_LEVEL = { 160 | 150 | 140 | 130 | 120 | 110 | 100 }
  | CONCAT_NULL_YIELDS_NULL { ON | OFF }
  | NUMERIC_ROUNDABORT { ON | OFF }
  | QUOTED_IDENTIFIER { ON | OFF }
  | RECURSIVE_TRIGGERS { ON | OFF }
}

<temporal_history_retention>::= TEMPORAL_HISTORY_RETENTION { ON | OFF }
```

## Arguments

#### *database_name*

Is the name of the database to be modified.

#### CURRENT

`CURRENT` runs the action in the current database. `CURRENT` isn't supported for all options in all contexts. If `CURRENT` fails, provide the database name.

#### \<auto_option> ::=

Controls automatic options.

#### <a id="auto_create_statistics"></a> AUTO_CREATE_STATISTICS { ON | OFF }

- ON

  Query Optimizer creates statistics on single columns in query predicates, as necessary, to improve query plans and query performance. These single-column statistics are created when Query Optimizer compiles queries. The single-column statistics are created only on columns that are not already the first column of an existing statistics object.

  The default is ON. We recommend that you use the default setting for most databases.

- OFF

  Query Optimizer doesn't create statistics on single columns in query predicates when it's compiling queries. Setting this option to OFF can cause suboptimal query plans and degraded query performance.

  You can determine this option's status by examining the `is_auto_create_stats_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAutoCreateStatistics` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

  For more information, see the "Statistics options" section in [Statistics](../../relational-databases/statistics/statistics.md#statistics-options).

#### INCREMENTAL = ON | OFF

Set AUTO_CREATE_STATISTICS to ON, and set INCREMENTAL to ON. This setting creates automatically created stats as incremental whenever incremental stats are supported. The default value is OFF. For more information, see [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md).

#### <a id="auto_shrink"></a> AUTO_SHRINK { ON | OFF }

- ON

  The database files are candidates for periodic shrinking. Unless you have a specific requirement, do not set the AUTO_SHRINK database option to ON. For more information, see [Shrink a database](../../relational-databases/databases/shrink-a-database.md).

  Both data file and log files can be automatically shrunk. AUTO_SHRINK reduces the size of the transaction log only if you set the database to SIMPLE recovery model or if you back up the log. When set to OFF, the database files aren't automatically shrunk during periodic checks for unused space.

  The AUTO_SHRINK option causes files to be shrunk when more than 25 percent of the file contains unused space. The option causes the file to shrink to one of two sizes. It shrinks to whichever is larger:

  - The size where 25 percent of the file is unused space
  - The size of the file when it was created

  You can't shrink a read-only database.

- OFF

  The database files are not automatically shrunk during periodic checks for unused space.

You can determine this option's status by examining the `is_auto_shrink_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAutoShrink` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

> [!NOTE]  
> The AUTO_SHRINK option isn't available in a Contained Database.

#### <a id="auto_update_statistics"></a> AUTO_UPDATE_STATISTICS { ON | OFF }

- ON

  Specifies that Query Optimizer updates statistics when they're used by a query and when they might be out-of-date. Statistics become out-of-date after insert, update, delete, or merge operations change the data distribution in the table or indexed view. Query Optimizer determines when statistics might be out-of-date by counting the number of data modifications since the last statistics update and comparing the number of modifications to a threshold. The threshold is based on the number of rows in the table or indexed view.

  Query Optimizer checks for out-of-date statistics before it compiles a query and runs a cached query plan. Query Optimizer uses the columns, tables, and indexed views in the query predicate to determine which statistics might be out-of-date. Query Optimizer determines this information before it compiles a query. Before running a cached query plan, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] verifies that the query plan references up-to-date statistics.

  The AUTO_UPDATE_STATISTICS option applies to statistics created for indexes, single-columns in query predicates, and statistics that are created by using the CREATE STATISTICS statement. This option also applies to filtered statistics.

  The default is ON. We recommend that you use the default setting for most databases.

  Use the AUTO_UPDATE_STATISTICS_ASYNC option to specify whether the statistics are updated synchronously or asynchronously.

- OFF

  Specifies that Query Optimizer doesn't update statistics when they're used by a query. Query Optimizer also doesn't update statistics when they might be out-of-date. Setting this option to OFF can cause suboptimal query plans and degraded query performance.

You can determine this option's status by examining the `is_auto_update_stats_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAutoUpdateStatistics` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

For more information, see the section "Using the database-wide statistics options" in [Statistics](../../relational-databases/statistics/statistics.md).

#### <a id="auto_update_statistics_async"></a> AUTO_UPDATE_STATISTICS_ASYNC { ON | OFF }

- ON

  Specifies that statistics updates for the AUTO_UPDATE_STATISTICS option are asynchronous. Query Optimizer doesn't wait for statistics updates to complete before it compiles queries.

  Setting this option to ON has no effect unless AUTO_UPDATE_STATISTICS is set to ON.

  By default, the AUTO_UPDATE_STATISTICS_ASYNC option is set to OFF, and Query Optimizer updates statistics synchronously.

- OFF

  Specifies that statistics updates for the AUTO_UPDATE_STATISTICS option are synchronous. Query Optimizer waits for statistics updates to complete before it compiles queries.

  Setting this option to OFF has no effect unless AUTO_UPDATE_STATISTICS is set to ON.

You can determine this option's status by examining the `is_auto_update_stats_async_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

For more information that describes when to use synchronous or asynchronous statistics updates, see the section "Using the database-wide statistics options" in [Statistics](../../relational-databases/statistics/statistics.md).

#### <a name="auto_tuning"></a> \<automatic_tuning_option> ::=

Controls automatic options for [Automatic tuning](../../relational-databases/automatic-tuning/automatic-tuning.md).

#### FORCE_LAST_GOOD_PLAN = { DEFAULT | ON | OFF }

Enables or disables `FORCE_LAST_GOOD_PLAN` [Automatic tuning](../../relational-databases/automatic-tuning/automatic-tuning.md) option.

- DEFAULT

  The default value for Azure SQL Managed Instance is ON.

- ON

  The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] automatically forces the last known good plan on the [!INCLUDE[tsql-md](../../includes/tsql-md.md)] queries where new query plan causes performance regressions. The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] continuously monitors query performance of the [!INCLUDE[tsql-md](../../includes/tsql-md.md)] query with the forced plan. If there are performance gains, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] will keep using last known good plan. If performance gains are not detected, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] will produce a new query plan. The statement will fail if the Query Store isn't enabled or if it's not in *Read-Write* mode.  This is the default value.

- OFF

  The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] reports potential query performance regressions caused by query plan changes in [sys.dm_db_tuning_recommendations](../../relational-databases/system-dynamic-management-views/sys-dm-db-tuning-recommendations-transact-sql.md) view. However, these recommendations are not automatically applied. Users can monitor active recommendations and fix identified problems by applying [!INCLUDE[tsql-md](../../includes/tsql-md.md)] scripts that are shown in the view.

#### \<change_tracking_option> ::=

Controls change tracking options. You can enable change tracking, set options, change options, and disable change tracking. For examples, see the [Examples](#examples) section later in this article.

- ON

  Enables change tracking for the database. When you enable change tracking, you can also set the AUTO CLEANUP and CHANGE RETENTION options.

#### AUTO_CLEANUP = { ON | OFF }

- ON

  Change tracking information is automatically removed after the specified retention period.

- OFF

  Change tracking data isn't removed from the database.

#### CHANGE_RETENTION = *retention_period* { DAYS | HOURS | MINUTES }

Specifies the minimum period for keeping change tracking information in the database. Data is removed only when the AUTO_CLEANUP value is ON.

*retention_period* is an integer that specifies the numerical component of the retention period.

The default retention period is **2 days**. The minimum retention period is 1 minute. The default retention type is **DAYS**.

- OFF

  Disables change tracking for the database. Disable change tracking on all tables before you disable change tracking off the database.

#### \<cursor_option> ::=

Controls cursor options.

#### CURSOR_CLOSE_ON_COMMIT { ON | OFF }

- ON

  Any cursors open when you commit or roll back a transaction are closed.

- OFF

  Cursors remain open when a transaction is committed; rolling back a transaction closes any cursors except those defined as INSENSITIVE or STATIC.

Connection-level settings that are set by using the SET statement override the default database setting for CURSOR_CLOSE_ON_COMMIT. ODBC and OLE DB clients issue a connection-level SET statement setting CURSOR_CLOSE_ON_COMMIT to OFF for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET CURSOR_CLOSE_ON_COMMIT](../../t-sql/statements/set-cursor-close-on-commit-transact-sql.md).

You can determine this option's status by examining the `is_cursor_close_on_commit_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or the IsCloseCursorsOnCommitEnabled property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function. The cursor is implicitly deallocated only at disconnect. For more information, see [DECLARE CURSOR](../../t-sql/language-elements/declare-cursor-transact-sql.md).

#### \<db_encryption_option> ::=

Controls the database encryption state.

#### ENCRYPTION { ON | OFF }

Sets the database to be encrypted (ON) or not encrypted (OFF). For more information about database encryption, see [Transparent Data Encryption](../../relational-databases/security/encryption/transparent-data-encryption.md), and [Transparent Data Encryption with Azure SQL Database](/azure/azure-sql/database/transparent-data-encryption-tde-overview).

When encryption is enabled at the database level, all file groups will be encrypted. Any new file groups will inherit the encrypted property. If any file groups in the database are set to READ ONLY, the database encryption operation will fail.

You can see the encryption state of the database by using the [sys.dm_database_encryption_keys](../../relational-databases/system-dynamic-management-views/sys-dm-database-encryption-keys-transact-sql.md) dynamic management view.

#### \<delayed_durability_option> ::=

Controls whether transactions commit fully durable or delayed durable.

- DISABLED

  All transactions following `SET DISABLED` are fully durable. Any durability options set in an atomic block or commit statement are ignored.

- ALLOWED

  All transactions following `SET ALLOWED` are either fully durable or delayed durable, depending upon the durability option set in the atomic block or commit statement.

- FORCED

  All transactions following `SET FORCED` are delayed durable. Any durability options set in an atomic block or commit statement are ignored.

#### \<PARAMETERIZATION_option> ::=

Controls the parameterization option.

#### PARAMETERIZATION { SIMPLE | FORCED }

- SIMPLE

  Queries are parameterized based on the default behavior of the database.

- FORCED

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] parameterizes all queries in the database.

The current setting of this option can be determined by examining the `is_parameterization_forced` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

#### <a name="query-store"></a> \<query_store_options> ::=

- ON | OFF | CLEAR [ ALL ]

  Controls whether the Query Store is enabled in this database, and also controls removing the contents of the Query Store.

  - ON

    Enables the Query Store.

  - OFF

    Disables the Query Store. This is the default value.

  - CLEAR

    Remove the contents of the Query Store.

#### OPERATION_MODE

Describes the operation mode of the Query Store. Valid values are READ_ONLY and READ_WRITE. In READ_WRITE mode, the Query Store collects and persists query plan and runtime execution statistics information. In READ_ONLY mode, information can be read from the Query Store, but new information isn't added. If the maximum allocated space of the Query Store has been exhausted, the Query Store will change is operation mode to READ_ONLY.

#### CLEANUP_POLICY

Describes the data retention policy of the Query Store. STALE_QUERY_THRESHOLD_DAYS determines the number of days for which the information for a query is kept in the Query Store. STALE_QUERY_THRESHOLD_DAYS is type **bigint**. The default value is 30. For [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)] Basic edition, default is **7** days.

#### DATA_FLUSH_INTERVAL_SECONDS

Determines the frequency at which data written to the Query Store is persisted to disk. To optimize for performance, data collected by the Query Store is asynchronously written to the disk. The frequency at which this asynchronous transfer occurs is configured by using the DATA_FLUSH_INTERVAL_SECONDS argument. DATA_FLUSH_INTERVAL_SECONDS is type **bigint**. The default value is **900** (15 min).

#### MAX_STORAGE_SIZE_MB

Determines the space allocated to the Query Store. MAX_STORAGE_SIZE_MB is type **bigint**. The default value is **100 MB**.

`MAX_STORAGE_SIZE_MB` limit isn't strictly enforced. Storage size is checked only when Query Store writes data to disk. This interval is set by the `DATA_FLUSH_INTERVAL_SECONDS` option or the [!INCLUDE[ssManStudio](../../includes/ssManStudio-md.md)] Query Store dialog option **Data Flush Interval**. The interval default value is 900 seconds (or 15 minutes).

If the Query Store has breached the `MAX_STORAGE_SIZE_MB` limit between storage size checks, it will transition to read-only mode. If `SIZE_BASED_CLEANUP_MODE` is enabled, the cleanup mechanism to enforce the `MAX_STORAGE_SIZE_MB` limit is also triggered.

Once enough space has been cleared, the Query Store mode will automatically switch back to read-write.

> [!IMPORTANT]  
> If you think that your workload capture will need more than 10 GB of disk space, you should probably rethink and optimize your workload to reuse query plans (for example using [forced parameterization](../../relational-databases/query-processing-architecture-guide.md#forced-parameterization), or adjust the Query Store configurations.
> Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], you can set `QUERY_CAPTURE_MODE` to CUSTOM for additional control over the query capture policy.

#### INTERVAL_LENGTH_MINUTES

Determines the time interval at which runtime execution statistics data is aggregated into the Query Store. To optimize for space usage, the runtime execution statistics in the runtime stats store are aggregated over a fixed time window. This fixed time window is configured by using the INTERVAL_LENGTH_MINUTES argument. INTERVAL_LENGTH_MINUTES is type **bigint**. The default value is **60**.

#### SIZE_BASED_CLEANUP_MODE = { AUTO | OFF }

Controls whether cleanup will be automatically activated when the total amount of data gets close to maximum size.

- OFF

  Size-based cleanup won't be automatically activated.

- AUTO

  Size-based cleanup will be automatically activated when size on disk reaches 90% of **max_storage_size_mb**. Size-based cleanup removes the least expensive and oldest queries first. It stops at approximately 80% of **max_storage_size_mb**. This is the default configuration value.

SIZE_BASED_CLEANUP_MODE is type **nvarchar**.

#### QUERY_CAPTURE_MODE { ALL | AUTO | CUSTOM | NONE }

Designates the currently active query capture mode.

- ALL

  All queries are captured.

- AUTO

  Capture relevant queries based on execution count and resource consumption. This is the default configuration value for [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

- NONE

  Stop capturing new queries. The Query Store will continue to collect compile and runtime statistics for queries that were captured already. Use this configuration with caution since you may miss capturing important queries.

QUERY_CAPTURE_MODE is type **nvarchar**.

#### MAX_PLANS_PER_QUERY

An integer representing the maximum number of plans maintained for each query. MAX_PLANS_PER_QUERY is type **int**. The default value is **200**.

#### WAIT_STATS_CAPTURE_MODE { ON | OFF }

Controls whether wait statistics will be captured per query.

- ON

  Wait statistics information per query is captured. This value is the default configuration value.

- OFF

  Wait statistics information per query won't be captured.

#### \<query_capture_policy_option_list> :: =

Controls the Query Store capture policy options. Except for STALE_CAPTURE_POLICY_THRESHOLD, these options define the OR conditions that need to happen for queries to be captured in the defined Stale Capture Policy Threshold value.

#### STALE_CAPTURE_POLICY_THRESHOLD = *integer* { DAYS | HOURS }

Defines the evaluation interval period to determine if a query should be captured. The default is 1 day, and it can be set from 1 hour to seven days.

#### EXECUTION_COUNT = *integer*

Defines the number of times a query is executed over the evaluation period. The default is 30, which means that for the default Stale Capture Policy Threshold, a query must execute at least 30 times in one day to be persisted in the Query Store. EXECUTION_COUNT is type **int**.

#### TOTAL_COMPILE_CPU_TIME_MS = *integer*

Defines total elapsed compile CPU time used by a query over the evaluation period. The default is 1000, which means that for the default Stale Capture Policy Threshold, a query must have a total of at least one second of CPU time spent during query compilation in one day to be persisted in the Query Store. TOTAL_COMPILE_CPU_TIME_MS is type **int**.

#### TOTAL_EXECUTION_CPU_TIME_MS = *integer*

Defines total elapsed execution CPU time used by a query over the evaluation period. The default is 100, which means that for the default Stale Capture Policy Threshold, a query must have a total of at least 100 ms of CPU time spent during execution in one day to be persisted in the Query Store. TOTAL_EXECUTION_CPU_TIME_MS is type **int**.

#### \<snapshot_option> ::=

Determines the transaction isolation level.

#### ALLOW_SNAPSHOT_ISOLATION { ON | OFF }

- ON

  Enables the Snapshot option at the database level. When it's enabled, DML statements start generating row versions even when no transaction uses Snapshot Isolation. After this option is enabled, transactions can specify the SNAPSHOT transaction isolation level. When a transaction runs at the SNAPSHOT isolation level, all statements see a snapshot of data as it exists at the start of the transaction. If a transaction running at the SNAPSHOT isolation level accesses data in multiple databases, either ALLOW_SNAPSHOT_ISOLATION must be set to ON in all the databases, or each statement in the transaction must use locking hints on any reference in a FROM clause to a table in a database where ALLOW_SNAPSHOT_ISOLATION is OFF.

- OFF

  Turns off the Snapshot option at the database level. Transactions can't specify the SNAPSHOT transaction isolation level.

When you set ALLOW_SNAPSHOT_ISOLATION to a new state (from ON to OFF, or from OFF to ON), ALTER DATABASE doesn't return control to the caller until all existing transactions in the database are committed. If the database is already in the state specified in the ALTER DATABASE statement, control is returned to the caller immediately. If the ALTER DATABASE statement doesn't return quickly, use [sys.dm_tran_active_snapshot_database_transactions](../../relational-databases/system-dynamic-management-views/sys-dm-tran-active-snapshot-database-transactions-transact-sql.md) to determine whether there are long-running transactions. If the ALTER DATABASE statement is canceled, the database remains in the state it was in when ALTER DATABASE was started. The [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view indicates the state of snapshot-isolation transactions in the database. If **snapshot_isolation_state_desc** = IN_TRANSITION_TO_ON, the statement `ALTER DATABASE ... ALLOW_SNAPSHOT_ISOLATION OFF` will pause six seconds and retry the operation.

You can't change the state of ALLOW_SNAPSHOT_ISOLATION if the database is OFFLINE.

You can change the ALLOW_SNAPSHOT_ISOLATION settings for the `master`, `model`, `msdb`, and `tempdb` databases. The setting is kept every time the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is stopped and restarted if you change the setting for `tempdb`. If you change the setting for the `model` system database, that setting becomes the default for any new databases that are created, except for `tempdb`.

The option is ON, by default, for the `master` and `msdb` databases.

The current setting of this option can be determined by examining the `snapshot_isolation_state` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

#### READ_COMMITTED_SNAPSHOT { ON | OFF }

- ON

  Enables the Read-Committed Snapshot option at the database level. When it's enabled, DML statements start generating row versions even when no transaction uses Snapshot Isolation. After this option is enabled, the transactions specifying the READ COMMITTED isolation level use row versioning instead of locking. All statements see a snapshot of data as it exists at the start of the statement when a transaction runs at the READ COMMITTED isolation level.

- OFF

  Turns off the Read-Committed Snapshot option at the database level. Transactions specifying the READ COMMITTED isolation level use locking.

To set READ_COMMITTED_SNAPSHOT to ON or OFF, there must be no active connections to the database except for the connection running the ALTER DATABASE command. However, the database doesn't have to be in single-user mode. You can't change the state of this option when the database is OFFLINE.

READ_COMMITTED_SNAPSHOT can't be turned ON for the `master`, `tempdb`, or `msdb` system databases. If you change the setting for the `model` system database, that setting becomes the default for any new databases created, except for `tempdb`.

The current setting of this option can be determined by examining the `is_read_committed_snapshot_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

> [!WARNING]  
> When a table is created with **DURABILITY = SCHEMA_ONLY**, and **READ_COMMITTED_SNAPSHOT** is subsequently changed using **ALTER DATABASE**, data in the table will be lost.

#### MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT { ON | OFF }

- ON

  When the transaction isolation level is set to any isolation level lower than SNAPSHOT, all interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] operations on memory-optimized tables are run under SNAPSHOT isolation. Examples of isolation levels lower than snapshot are READ COMMITTED or READ UNCOMMITTED. These operations run whether the transaction isolation level is set explicitly at the session level, or the default is used implicitly.

- OFF

  Doesn't elevate the transaction isolation level for interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] operations on memory-optimized tables.

You can't change the state of MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT if the database is OFFLINE.

The default value is OFF.

The current setting of this option can be determined by examining the `is_memory_optimized_elevate_to_snapshot_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

#### \<sql_option> ::=

Controls the ANSI compliance options at the database level.

#### ANSI_NULL_DEFAULT { ON | OFF }

Determines the default value, NULL or NOT NULL, of a column or [CLR user-defined type](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md) for which the nullability isn't explicitly defined in CREATE TABLE or ALTER TABLE statements. Columns that are defined with constraints follow constraint rules whatever this setting may be.

- ON

  The default value is NULL.

- OFF

  The default value is NOT NULL.

Connection-level settings that are set by using the SET statement override the default database-level setting for ANSI_NULL_DEFAULT. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_NULL_DEFAULT to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_NULL_DFLT_ON](../../t-sql/statements/set-ansi-null-dflt-on-transact-sql.md).

For ANSI compatibility, setting the database option ANSI_NULL_DEFAULT to ON changes the database default to NULL.

You can determine this option's status by examining the `is_ansi_null_default_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAnsiNullDefault` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### ANSI_NULLS { ON | OFF }

- ON

  All comparisons to a null value evaluate to UNKNOWN.

- OFF

  Comparisons of non-Unicode values to a null value evaluate to TRUE if both values are NULL.

> [!IMPORTANT]  
> In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], ANSI_NULLS will always be ON and any applications that explicitly set the option to OFF will produce an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.

Connection-level settings that are set by using the SET statement override the default database setting for ANSI_NULLS. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_NULLS to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_NULLS](../../t-sql/statements/set-ansi-nulls-transact-sql.md).

> [!IMPORTANT]  
> SET ANSI_NULLS also must be set to ON when you create or make changes to indexes on computed columns or indexed views.

You can determine this option's status by examining the `is_ansi_nulls_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAnsiNullsEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### ANSI_PADDING { ON | OFF }

- ON

  Strings are padded to the same length before conversion. Also padded to the same length before inserting to a **varchar** or **nvarchar** data type.

- OFF

  Inserts trailing blanks in character values into **varchar** or **nvarchar** columns. Also leaves trailing zeros in binary values that are inserted into **varbinary** columns. Values aren't padded to the length of the column.

  When OFF is specified, this setting affects only the definition of new columns.

> [!IMPORTANT]  
> In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], ANSI_PADDING will always be ON and any applications that explicitly set the option to OFF will produce an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. We recommend that you always set ANSI_PADDING to ON. ANSI_PADDING must be ON when you create or manipulate indexes on computed columns or indexed views.

**char(*n*)** and **binary(*n*)** columns that allow for nulls are padded to the column length when ANSI_PADDING is set to ON. Trailing blanks and zeros are trimmed when ANSI_PADDING is OFF. **char(_n_)** and **binary(_n_)** columns that don't allow nulls are always padded to the length of the column.

  Connection-level settings that are set by using the SET statement override the default database-level setting for ANSI_PADDING. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_PADDING to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_PADDING](../../t-sql/statements/set-ansi-padding-transact-sql.md).

You can determine this option's status by examining the `is_ansi_padding_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAnsiPaddingEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### ANSI_WARNINGS { ON | OFF }

- ON

  Errors or warnings are issued when conditions such as divide-by-zero occur. Errors and warnings are also issued when null values appear in aggregate functions.

- OFF

  No warnings are raised and null values are returned when conditions such as divide-by-zero occur.

> [!IMPORTANT]  
> SET ANSI_WARNINGS must be set to ON when you create or make changes to indexes on computed columns or indexed views.

Connection-level settings that are set by using the SET statement override the default database setting for ANSI_WARNINGS. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_WARNINGS to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_WARNINGS](../../t-sql/statements/set-ansi-warnings-transact-sql.md).

You can determine this option's status by examining the `is_ansi_warnings_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAnsiWarningsEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### ARITHABORT { ON | OFF }

- ON

  A query is ended when an overflow or divide-by-zero error occurs during query execution.

- OFF

  A warning message is displayed when one of these errors occurs. The query, batch, or transaction continues to process as if no error occurred even if a warning is displayed.

> [!IMPORTANT]  
> SET ARITHABORT must be set to ON when you create or make changes to indexes on computed columns or indexed views.

You can determine this option's status by examining the `is_arithabort_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsArithmeticAbortEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### COMPATIBILITY_LEVEL = { 160 | 150 | 140 | 130 | 120 | 110 | 100 }

For more information, see [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

#### CONCAT_NULL_YIELDS_NULL { ON | OFF }

- ON

  The result of a concatenation operation is NULL when either operand is NULL. For example, concatenating the character string "This is" and NULL causes the value NULL, instead of the value "This is".

- OFF

  The null value is treated as an empty character string.

> [!IMPORTANT]  
> CONCAT_NULL_YIELDS_NULL must be set to ON when you create or make changes to indexes on computed columns or indexed views.
>  
> In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], CONCAT_NULL_YIELDS_NULL will always be ON and any applications that explicitly set the option to OFF will produce an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.

Connection-level settings that are set by using the SET statement override the default database setting for CONCAT_NULL_YIELDS_NULL. By default, ODBC and OLE DB clients issue a connection-level SET statement setting CONCAT_NULL_YIELDS_NULL to ON for the session when connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET CONCAT_NULL_YIELDS_NULL](../../t-sql/statements/set-concat-null-yields-null-transact-sql.md).

You can determine this option's status by examining the `is_concat_null_yields_null_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsNullConcat` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### NUMERIC_ROUNDABORT { ON | OFF }

- ON

  An error is generated when loss of precision occurs in an expression.

- OFF

  Loss of precision doesn't generate an error message, and the result is rounded to the precision of the column or variable storing the result.

> [!IMPORTANT]  
> NUMERIC_ROUNDABORT must be set to OFF when you create or make changes to indexes on computed columns or indexed views.

You can determine the status of this option in the `is_numeric_roundabort_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsNumericRoundAbortEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### QUOTED_IDENTIFIER { ON | OFF }

- ON

  Double quotation marks can be used to enclose delimited identifiers.

  All strings delimited by double quotation marks are interpreted as object identifiers. Quoted identifiers don't have to follow the [!INCLUDE[tsql](../../includes/tsql-md.md)] rules for identifiers. They can be keywords and can include characters not allowed in [!INCLUDE[tsql](../../includes/tsql-md.md)] identifiers. If a single quotation mark (') is part of the literal string, it can be represented by double quotation marks (").

- OFF

  Identifiers can't be in quotation marks and must follow all [!INCLUDE[tsql](../../includes/tsql-md.md)] rules for identifiers. Literals can be delimited by either single or double quotation marks.

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also allows for identifiers to be delimited by square brackets ([ ]). Bracketed identifiers can always be used, whatever the QUOTED_IDENTIFIER setting is. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).

  When a table is created, the QUOTED IDENTIFIER option is always stored as ON in the metadata of the table. The option is stored even if the option is set to OFF when the table is created.

Connection-level settings that are set by using the SET statement override the default database setting for QUOTED_IDENTIFIER. ODBC and OLE DB clients issue a connection-level SET statement setting QUOTED_IDENTIFIER to ON, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET QUOTED_IDENTIFIER](../../t-sql/statements/set-quoted-identifier-transact-sql.md).

You can determine this option's status by examining the `is_quoted_identifier_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsQuotedIdentifiersEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### RECURSIVE_TRIGGERS { ON | OFF }

- ON

  Recursive firing of AFTER triggers is allowed.

- OFF

  You can determine this option's status by examining the `is_recursive_triggers_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsRecursiveTriggersEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

  > [!NOTE]  
  > Only direct recursion is prevented when RECURSIVE_TRIGGERS is set to OFF. To disable indirect recursion, you must also set the nested triggers server option to 0.

You can determine this option's status by examining the `is_recursive_triggers_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or the `IsRecursiveTriggersEnabled` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

#### \<target_recovery_time_option> ::=

**target_recovery_time_option** isn't supported on Azure SQL Managed Instance.

Specifies the frequency of indirect checkpoints on a per-database basis. Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] the default value for new databases is **1 minute**, which indicates database will use indirect checkpoints. For older versions the default is 0, which indicates that the database will use automatic checkpoints, whose frequency depends on the recovery interval setting of the server instance. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends 1 minute for most systems.

#### WITH \<termination> ::=

Specifies when to roll back incomplete transactions when the database is transitioned from one state to another. If the termination clause is omitted, the ALTER DATABASE statement waits indefinitely if there's any lock on the database. Only one termination clause can be specified, and it follows the SET clauses.

> [!NOTE]  
> Not all database options use the WITH \<termination> clause. For more information, see the table under [Setting options](#SettingOptions) of the "Remarks" section of this article.

- ROLLBACK AFTER *integer* [SECONDS] | ROLLBACK IMMEDIATE

  Specifies whether to roll back after the specified number of seconds or immediately.

- NO_WAIT

  Specifies that the request will fail if the requested database state or option change can't complete immediately. Completing immediately means not waiting for transactions to commit or roll back on their own.

#### \<temporal_history_retention> ::=

- TEMPORAL_HISTORY_RETENTION { ON | OFF }

  ON by default but also automatically set to OFF after point in time restore operation. For more information including how to enable this setting, see [How to configure retention policy](../../relational-databases/tables/manage-retention-of-historical-data-in-system-versioned-temporal-tables.md#how-to-configure-retention-policy).

  - ON

    Default. Enables temporal table retention policy. For more information, see [Manage retention of historical data in system-versioned temporal tables](../../relational-databases/tables/manage-retention-of-historical-data-in-system-versioned-temporal-tables.md).

  - OFF

    Do not perform temporal historical retention policy.

## <a id="SettingOptions"></a> Set options

To retrieve current settings for database options, use the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)

After you set a database option, the new setting takes effect immediately.

You can change the default values for any one of the database options for all newly created databases. To do so, change the appropriate database option in the `model` system database.

## Examples

### A. Enable snapshot isolation on a database

The following example enables the snapshot isolation framework option for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
USE master;
GO
ALTER DATABASE [database_name]
SET ALLOW_SNAPSHOT_ISOLATION ON;
GO
-- Check the state of the snapshot_isolation_framework
-- in the database.
SELECT name, snapshot_isolation_state,
    snapshot_isolation_state_desc AS description
FROM sys.databases
WHERE name = N'[database_name]';
GO
```

The result set shows that the snapshot isolation framework is enabled.

| name | snapshot_isolation_state | description |
| --- | --- | --- |
| [database_name] | 1 | ON |

### B. Enable, modify, or disable change tracking

The following example enables change tracking for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database and sets the retention period to `2` days.

```sql
ALTER DATABASE [database_name]
SET CHANGE_TRACKING = ON
(AUTO_CLEANUP = ON, CHANGE_RETENTION = 2 DAYS);
```

The following example shows how to change the retention period to `3` days.

```sql
ALTER DATABASE [database_name]
SET CHANGE_TRACKING (CHANGE_RETENTION = 3 DAYS);
```

The following example shows how to disable change tracking for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
ALTER DATABASE [database_name]
SET CHANGE_TRACKING = OFF;
```

### C. Enable the Query Store

The following example enables the Query Store and configures Query Store parameters.

```sql
ALTER DATABASE [database_name]
SET QUERY_STORE = ON
    (
      OPERATION_MODE = READ_WRITE,
      CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = 90 ),
      DATA_FLUSH_INTERVAL_SECONDS = 900,
      QUERY_CAPTURE_MODE = AUTO,
      MAX_STORAGE_SIZE_MB = 1024,
      INTERVAL_LENGTH_MINUTES = 60
    );
```

### D. Enable the Query Store with wait statistics

The following example enables the Query Store and configures its parameters.

```sql
ALTER DATABASE [database_name]
SET QUERY_STORE = ON
    (
      OPERATION_MODE = READ_WRITE,
      CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = 90 ),
      DATA_FLUSH_INTERVAL_SECONDS = 900,
      MAX_STORAGE_SIZE_MB = 1024,
      INTERVAL_LENGTH_MINUTES = 60,
      SIZE_BASED_CLEANUP_MODE = AUTO,
      MAX_PLANS_PER_QUERY = 200,
      WAIT_STATS_CAPTURE_MODE = ON
    );
```

### E. Enable the Query Store with custom capture policy options

The following example enables the Query Store and configures its parameters.

```sql
ALTER DATABASE [database_name]
SET QUERY_STORE = ON
    (
      OPERATION_MODE = READ_WRITE,
      CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = 90 ),
      DATA_FLUSH_INTERVAL_SECONDS = 900,
      MAX_STORAGE_SIZE_MB = 1024,
      INTERVAL_LENGTH_MINUTES = 60,
      SIZE_BASED_CLEANUP_MODE = AUTO,
      MAX_PLANS_PER_QUERY = 200,
      WAIT_STATS_CAPTURE_MODE = ON,
      QUERY_CAPTURE_MODE = CUSTOM,
      QUERY_CAPTURE_POLICY = (
        STALE_CAPTURE_POLICY_THRESHOLD = 24 HOURS,
        EXECUTION_COUNT = 30,
        TOTAL_COMPILE_CPU_TIME_MS = 1000,
        TOTAL_EXECUTION_CPU_TIME_MS = 100
      )
    );
```



## See also

- [Statistics](../../relational-databases/statistics/statistics.md)
- [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)
- [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [sys.data_spaces](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md)
- [sys.database_automatic_tuning_options](../../relational-databases/system-catalog-views/sys-database-automatic-tuning-options-transact-sql.md)
- [sys.database_automatic_tuning_mode](../../relational-databases/system-catalog-views/sys-database-automatic-tuning-mode-transact-sql.md)

## Next steps

- [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
- [ALTER DATABASE Database Mirroring](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
- [Enable and Disable Change Tracking](../../relational-databases/track-changes/enable-and-disable-change-tracking-sql-server.md)
- [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md)
- [SET TRANSACTION ISOLATION LEVEL](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md)
- [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [Best Practice with the Query Store](../../relational-databases/performance/best-practice-with-the-query-store.md)

::: moniker-end
::: moniker range="=azure-sqldw-latest"

:::row:::
    :::column:::
        [SQL Server](alter-database-transact-sql-set-options.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Database](alter-database-transact-sql-set-options.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed Instance](alter-database-transact-sql-set-options.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        ***\* Azure Synapse<br />Analytics \**** &nbsp;
    :::column-end:::
:::row-end:::

&nbsp;

## Azure Synapse Analytics

## Syntax

```syntaxsql
ALTER DATABASE { database_name }
SET
{
    <optionspec> [ ,...n ]
}
;

<option_spec>::=
{
    <auto_option>
  | <db_encryption_option>
  | <query_store_options>
  | <result_set_caching>
  | <snapshot_option>
}
;

<auto_option> ::=
{
    AUTO_CREATE_STATISTICS { OFF | ON }
}

<db_encryption_option> ::=
{
    ENCRYPTION { ON | OFF }
}

<query_store_option> ::=
{
    QUERY_STORE { OFF | ON }
}

<result_set_caching_option> ::=
{
    RESULT_SET_CACHING { ON | OFF }
}

<snapshot_option> ::=
{
    READ_COMMITTED_SNAPSHOT { ON | OFF }
}
```

## Arguments

#### *database_name*

Is the name of the database to be modified.

#### <auto_option> ::=

Controls automatic options.

#### AUTO_CREATE_STATISTICS { ON | OFF }

- ON

  Query Optimizer creates statistics on single columns in query predicates, as necessary, to improve query plans and query performance. These single-column statistics are created when Query Optimizer compiles queries. The single-column statistics are created only on columns that are not already the first column of an existing statistics object.

  The default is ON. We recommend that you use the default setting for most databases.

- OFF

  Query Optimizer doesn't create statistics on single columns in query predicates when it's compiling queries. Setting this option to OFF can cause suboptimal query plans and degraded query performance.

This command must be run while connected to the user database.

You can determine this option's status by examining the `is_auto_create_stats_on` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the `IsAutoCreateStatistics` property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

For more information, see the section "Using the database-wide statistics options" in [Statistics](../../relational-databases/statistics/statistics.md).

#### <db_encryption_option> ::=

Controls the database encryption state.

#### ENCRYPTION { ON | OFF }

- ON

  Sets the database to be encrypted.

- OFF

  Sets the database to not be encrypted.

For more information about database encryption, see [Transparent data encryption (TDE)](../../relational-databases/security/encryption/transparent-data-encryption.md), and [Transparent data encryption for SQL Database, SQL Managed Instance, and Azure Synapse Analytics](/azure/azure-sql/database/transparent-data-encryption-tde-overview).

When encryption is enabled at the database level, all file groups will be encrypted. Any new file groups will inherit the encrypted property. If any file groups in the database are set to READ ONLY, the database encryption operation will fail.

You can see the encryption state of the database and the state of the encryption scan by using the `sys.dm_database_encryption_keys` dynamic management view.

#### <query_store_option> ::=

Controls whether the Query Store is enabled in this data warehouse.

#### QUERY_STORE { ON | OFF }

- ON

  Enables the Query Store.

- OFF

  Disables the Query Store. OFF is the default value.

> [!NOTE]  
> For [!INCLUDE[ssSDW](../../includes/sssdw-md.md)], you must execute `ALTER DATABASE SET QUERY_STORE` from the user database. Executing the statement from another data warehouse instance isn't supported.

> [!NOTE]  
> For Azure Synapse Analytics, the Query Store can be enabled as on other platforms but additional configuration options are not supported.

#### <result_set_caching_option> ::=

**Applies to**: Azure Synapse Analytics

Controls whether query result is cached in the database.

#### RESULT_SET_CACHING { ON | OFF }

- ON

  Specifies that query result sets returned from this database will be cached in the database.

- OFF

  Specifies that query result sets returned from this database will not be cached in the database.

This command must be run while connected to the `master` database.  Change to this database setting takes effect immediately.  Storage costs are incurred by caching query result sets. After disabling result caching for a database, previously persisted result cache will immediately be deleted from Azure Synapse storage.

Run this command to check a database's result set caching configuration.  If result set caching is turned ON, `is_result_set_caching_on` will return 1.

```sql
SELECT name, is_result_set_caching_on FROM sys.databases
WHERE name = <'Your_Database_Name'>
```

Run this command to check if a query was executed using cached result.  The `result_cache_hit` column returns 1 for cache hit, 0 for cache miss, and negative values for reasons why result set caching was not used.  Check [sys.dm_pdw_exec_requests](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-requests-transact-sql.md?view=aps-pdw-2016-au7&preserve-view=true) for details.

```sql
SELECT request_id, command, result_cache_hit FROM sys.dm_pdw_exec_requests
WHERE request_id = <'Your_Query_Request_ID'>
```

> [!NOTE]  
> Result set caching should not be used in conjunction with [DECRYPTBYKEY](../functions/decryptbykey-transact-sql.md). If this cryptographic function must be used, ensure you have result set caching disabled (either at [session-level](./set-result-set-caching-transact-sql.md) or [database-level](alter-database-transact-sql-set-options.md)) at the time of execution.

> [!IMPORTANT]  
> The operations to create result set cache and retrieve data from the cache happen on the control node of a data warehouse instance. When result set caching is turned ON, running queries that return large result set (for example, >1 million rows) can cause high CPU usage on the control node and slow down the overall query response on the instance. Those queries are commonly used during data exploration or ETL operations. To avoid stressing the control node and cause performance issue, users should turn OFF result set caching on the database before running those types of queries.

For details on performance tuning with result set caching, check [Performance tuning guidance](/azure/sql-data-warehouse/performance-tuning-result-set-caching).

##### Permissions

To set the RESULT_SET_CACHING option, a user needs server-level principal login (the one created by the provisioning process) or be a member of the `dbmanager` database role.

#### <snapshot_option> ::=

**Applies to**: Azure Synapse Analytics

Controls the transaction isolation level of a database.

#### READ_COMMITTED_SNAPSHOT { ON | OFF }

- ON

  Enables the READ_COMMITTED_SNAPSHOT option at the database level.

- OFF

  Turn off the READ_COMMITTED_SNAPSHOT option at the database level.

This command must be run while connected to the `master` database. Turning READ_COMMITTED_SNAPSHOT ON or OFF for a user database will kill all open connections to this database. You may want to make this change during database maintenance window or wait until there's no active connection to the database except for the connection running the ALTER DATABASE command.  The database doesn't have to be in single-user mode. Changing READ_COMMITTED_SNAPSHOT setting at session level isn't supported.  To verify this setting for a database, check the `is_read_committed_snapshot_on` column in `sys.databases`.

In a database with READ_COMMITTED_SNAPSHOT enabled, queries may experience slower performance due to the scan of versions if multiple data versions are present. Long-open transactions can also cause an increase in the size of the database. This issue occurs if there are data changes by these transactions that block version cleanup.

##### Permissions

To set the READ_COMMITTED_SNAPSHOT option, a user needs ALTER permission on the database.

## Examples

### Check statistics setting for a database

```sql
SELECT name, is_auto_create_stats_on FROM sys.databases
```

### Enable query store for a database

```sql
ALTER DATABASE [database_name]
SET QUERY_STORE = ON;
```

### Enable result set caching for a database

```sql
-- Run this command when connecting to the MASTER database

ALTER DATABASE [database_name]
SET RESULT_SET_CACHING ON;
```

### Check result set caching setting for a database

```sql
SELECT name, is_result_set_caching_on
FROM sys.databases;
```

### Enable the Read_Committed_Snapshot option for a database

Run this command when connecting to the `master` database.

```sql
ALTER DATABASE MyDatabase
SET READ_COMMITTED_SNAPSHOT ON;
```

## See also

- [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)
- [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [Azure Synapse Analytics language elements](/azure/sql-data-warehouse/sql-data-warehouse-reference-tsql-language-elements)

## Next steps

- [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md)
- [Best practices for Azure Synapse Analytics](/azure/sql-data-warehouse/sql-data-warehouse-best-practices#maintain-statistics)
- [Designing tables in Azure Synapse Analytics](/azure/sql-data-warehouse/sql-data-warehouse-tables-overview#statistics)

::: moniker-end
