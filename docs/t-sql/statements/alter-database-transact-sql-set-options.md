---
title: "ALTER DATABASE SET Options (Transact-SQL) | Microsoft Docs"
description: Learn about how to set database options such as automatic tuning, encryption, query store in a SQL Server and Azure SQL Database
ms.custom: ""
ms.date: 03/27/2019
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
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
  - "automatic tuning"
  - "SQL plan regression correction"
  - "auto_create_statistics"
  - "auto_update_statistics"
ms.assetid: f76fbd84-df59-4404-806b-8ecb4497c9cc
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: "=azuresqldb-current||=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ALTER DATABASE SET Options (Transact-SQL)

Sets database options in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Azure SQL Database. For other ALTER DATABASE options, see [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md).

Click one of the following tabs for the syntax, arguments, remarks, permissions, and examples for a particular SQL version with which you're working.

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

## Click a product!

In the following row, click whichever product name you're interested in. The click displays different content here on this webpage, appropriate for whichever product you click.

::: moniker range=">=sql-server-2016||>=sql-server-linux-2017||=sqlallproducts-allversions"

> |||
> |---|---|
> |**_\* SQL Server \*_** &nbsp;|[SQL Database<br />single database/elastic pool](alter-database-transact-sql-set-options.md?view=azuresqldb-current)|[SQL Database<br />managed instance](alter-database-transact-sql-set-options.md?view=azuresqldb-mi-current)|||

&nbsp;

## SQL Server

Database mirroring, [!INCLUDE[ssHADR](../../includes/sshadr-md.md)], and compatibility levels are `SET` options but are described in separate articles because of their length. For more information, see [ALTER DATABASE Database Mirroring](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md), [ALTER DATABASE SET HADR](../../t-sql/statements/alter-database-transact-sql-set-hadr.md), and [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

> [!NOTE]
> Many database set options can be configured for the current session by using [SET Statements](../../t-sql/statements/set-statements-transact-sql.md) and are often configured by applications when they connect. Session level set options override the **ALTER DATABASE SET** values. The database options described below are values that can be set for sessions that don't explicitly provide other set option values.

## Syntax

```
ALTER DATABASE { database_name | CURRENT }
SET
{
    <option_spec> [ ,...n ] [ WITH <termination> ]
}

<option_spec> ::=
{
    <auto_option>
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
  | <target_recovery_time_option>
  | <termination>
}
;

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
  AUTOMATIC_TUNING ( FORCE_LAST_GOOD_PLAN = { ON | OFF } )
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
          = OFF
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
    | SIZE_BASED_CLEANUP_MODE = [ AUTO | OFF ]
    | QUERY_CAPTURE_MODE = [ ALL | AUTO | NONE ]
    | MAX_PLANS_PER_QUERY = number
    | WAIT_STATS_CAPTURE_MODE = [ ON | OFF ]
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
        ON ( SERVER = <server_name> ,
                  {CREDENTIAL = <db_scoped_credential_name>
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
  | HONOR_BROKER_PRIORITY { ON | OFF}
}

<snapshot_option> ::=
{
    ALLOW_SNAPSHOT_ISOLATION { ON | OFF }
  | READ_COMMITTED_SNAPSHOT {ON | OFF }
  | MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = {ON | OFF }
}
<sql_option> ::=
{
    ANSI_NULL_DEFAULT { ON | OFF }
  | ANSI_NULLS { ON | OFF }
  | ANSI_PADDING { ON | OFF }
  | ANSI_WARNINGS { ON | OFF }
  | ARITHABORT { ON | OFF }
  | COMPATIBILITY_LEVEL = { 140 | 130 | 120 | 110 | 100 | 90 }
  | CONCAT_NULL_YIELDS_NULL { ON | OFF }
  | NUMERIC_ROUNDABORT { ON | OFF }
  | QUOTED_IDENTIFIER { ON | OFF }
  | RECURSIVE_TRIGGERS { ON | OFF }
}

<target_recovery_time_option> ::=
    TARGET_RECOVERY_TIME = target_recovery_time { SECONDS | MINUTES }

<termination>::=
{
    ROLLBACK AFTER integer [ SECONDS ]
  | ROLLBACK IMMEDIATE
  | NO_WAIT
}
```

## Arguments

*database_name*
Is the name of the database to be modified.

CURRENT
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].

`CURRENT` runs the action in the current database. `CURRENT` isn't supported for all options in all contexts. If `CURRENT` fails, provide the database name.

**\<auto_option> ::=**

Controls automatic options.
<a name="auto_close"></a> AUTO_CLOSE { ON | OFF }
ON
The database is shut down cleanly and its resources are freed after the last user exits.

The database automatically reopens when a user tries to use the database again. For example, by issuing a `USE database_name` statement. The database may shut down cleanly with AUTO_CLOSE set to ON. If so, the database doesn't reopen until a user tries to use the database the next time the [!INCLUDE[ssDE](../../includes/ssde-md.md)] restarts.

OFF
The database remains open after the last user exits.

The AUTO_CLOSE option is useful for desktop databases because it allows for database files to be managed as regular files. They can be moved, copied to make backups, or even e-mailed to other users. The AUTO_CLOSE process is asynchronous; repeatedly opening and closing the database doesn't reduce performance.

> [!NOTE]
> The AUTO_CLOSE option isn't available in a Contained Database or on [!INCLUDE[ssSDS](../../includes/sssds-md.md)].
> You can determine this option's status by examining the is_auto_close_on column in the sys.databases catalog view or the IsAutoClose property of the DATABASEPROPERTYEX function.
>
> When AUTO_CLOSE is ON, some columns in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view and DATABASEPROPERTYEX function will return NULL because the database is unavailable to retrieve the data. To resolve this, execute a USE statement to open the database.
>
> Database mirroring requires AUTO_CLOSE OFF.

When the database is set to AUTOCLOSE = ON, an operation that initiates an automatic database shutdown clears the plan cache for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Clearing the plan cache causes a recompilation of all subsequent execution plans and can cause a sudden, temporary decrease in query performance. In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 2 and higher, for each cleared cachestore in the plan cache, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log contains the following informational message: " [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has encountered %d occurrence(s) of cachestore flush for the '%s' cachestore (part of plan cache) due to some database maintenance or reconfigure operations". This message is logged every five minutes as long as the cache is flushed within that time interval.

<a name="auto_create_statistics"></a> AUTO_CREATE_STATISTICS { ON | OFF }
ON
The query optimizer creates statistics on single columns in query predicates, as necessary, to improve query plans and query performance. These single-column statistics are created when the query optimizer compiles queries. The single-column statistics are created only on columns that are not already the first column of an existing statistics object.

The default is ON. We recommend that you use the default setting for most databases.

OFF
The query optimizer doesn't create statistics on single columns in query predicates when it is compiling queries. Setting this option to OFF can cause suboptimal query plans and degraded query performance.

You can determine this option's status by examining the is_auto_create_stats_on column in the sys.databases catalog view. You can also determine the status by examining the IsAutoCreateStatistics property of the DATABASEPROPERTYEX function.

For more information, see the section "Using the Database-Wide Statistics Options" in [Statistics](../../relational-databases/statistics/statistics.md).

INCREMENTAL = ON | OFF
Set AUTO_CREATE_STATISTICS to ON, and set INCREMENTAL to ON. This setting creates automatically created stats as incremental whenever incremental stats are supported. The default value is OFF. For more information, see [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md).

**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDS](../../includes/sssds-md.md)].

<a name="auto_shrink"></a> AUTO_SHRINK { ON | OFF }
ON
The database files are candidates for periodic shrinking.

Both data file and log files can be automatically shrunk. AUTO_SHRINK reduces the size of the transaction log only if you set the database to SIMPLE recovery model or if you back up the log. When set to OFF, the database files aren't automatically shrunk during periodic checks for unused space.

The AUTO_SHRINK option causes files to be shrunk when more than 25 percent of the file contains unused space. The option causes the file to shrink to one of two sizes. It shrinks to whichever is larger:

- the size where 25 percent of the file is unused space
- the size of the file when it was created

You can't shrink a read-only database.

OFF
The database files are not automatically shrunk during periodic checks for unused space.

You can determine this option's status by examining the is_auto_shrink_on column in the sys.databases catalog view. You can also determine the status by examining the IsAutoShrink property of the DATABASEPROPERTYEX function.

> [!NOTE]
> The AUTO_SHRINK option isn't available in a Contained Database.

<a name="auto_update_statistics"></a> AUTO_UPDATE_STATISTICS { ON | OFF }
ON
Specifies that the query optimizer updates statistics when they're used by a query and when they might be out-of-date. Statistics become out-of-date after insert, update, delete, or merge operations change the data distribution in the table or indexed view. The query optimizer determines when statistics might be out-of-date by counting the number of data modifications since the last statistics update and comparing the number of modifications to a threshold. The threshold is based on the number of rows in the table or indexed view.

The query optimizer checks for out-of-date statistics before it compiles a query and it runs a cached query plan. The query optimizer uses the columns, tables, and indexed views in the query predicate to determine which statistics might be out-of-date. The query optimizer determines this information before it compiles a query. Before executing a cached query plan, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] verifies that the query plan references up-to-date statistics.

The AUTO_UPDATE_STATISTICS option applies to statistics created for indexes, single-columns in query predicates, and statistics that are created by using the CREATE STATISTICS statement. This option also applies to filtered statistics.

The default is ON. We recommend that you use the default setting for most databases.

Use the AUTO_UPDATE_STATISTICS_ASYNC option to specify whether the statistics are updated synchronously or asynchronously.

OFF
Specifies that the query optimizer doesn't update statistics when they're used by a query. The query optimizer also doesn't update statistics when they might be out-of-date. Setting this option to OFF can cause suboptimal query plans and degraded query performance.

You can determine this option's status by examining the is_auto_update_stats_on column in the sys.databases catalog view. You can also determine the status by examining the IsAutoUpdateStatistics property of the DATABASEPROPERTYEX function.

For more information, see the section "Using the Database-Wide Statistics Options" in [Statistics](../../relational-databases/statistics/statistics.md).

<a name="auto_update_statistics_async"></a> AUTO_UPDATE_STATISTICS_ASYNC { ON | OFF }
ON
Specifies that statistics updates for the AUTO_UPDATE_STATISTICS option are asynchronous. The query optimizer doesn't wait for statistics updates to complete before it compiles queries.

Setting this option to ON has no effect unless AUTO_UPDATE_STATISTICS is set to ON.

By default, the AUTO_UPDATE_STATISTICS_ASYNC option is set to OFF, and the query optimizer updates statistics synchronously.

OFF
Specifies that statistics updates for the AUTO_UPDATE_STATISTICS option are synchronous. The query optimizer waits for statistics updates to complete before it compiles queries.

Setting this option to OFF has no effect unless AUTO_UPDATE_STATISTICS is set to ON.

You can determine this option's status by examining the is_auto_update_stats_async_on column in the sys.databases catalog view.

For more information that describes when to use synchronous or asynchronous statistics updates, see the section "Using the Database-Wide Statistics Options" in [Statistics](../../relational-databases/statistics/statistics.md).

<a name="auto_tuning"></a> **\<automatic_tuning_option> ::=**
**Applies to**: [!INCLUDE[sssqlv14-md](../../includes/sssqlv14-md.md)].

Enables or disables `FORCE_LAST_GOOD_PLAN` [automatic tuning](../../relational-databases/automatic-tuning/automatic-tuning.md) option.

FORCE_LAST_GOOD_PLAN = { ON | OFF }
ON
The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] automatically forces the last known good plan on the [!INCLUDE[tsql-md](../../includes/tsql-md.md)] queries where new SQL plan causes performance regressions. The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] continuously monitors query performance of the [!INCLUDE[tsql-md](../../includes/tsql-md.md)] query with the forced plan.

If there are performance gains, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] will keep using last known good plan. If performance gains are not detected, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] will produce a new SQL plan. The statement will fail if Query Store isn't enabled or if it isn't in *Read-Write* mode.

OFF

The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] reports potential query performance regressions caused by SQL plan changes in [sys.dm_db_tuning_recommendations](../../relational-databases/system-dynamic-management-views/sys-dm-db-tuning-recommendations-transact-sql.md) view. However, these recommendations are not automatically applied. User can monitor active recommendations and fix identified problems by applying [!INCLUDE[tsql-md](../../includes/tsql-md.md)] scripts that are shown in the view. This is the default value.

**\<change_tracking_option> ::=**

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDSFull](../../includes/sssds-md.md)].

Controls change tracking options. You can enable change tracking, set options, change options, and disable change tracking. For examples, see the Examples section later in this article.

ON
Enables change tracking for the database. When you enable change tracking, you can also set the AUTO CLEANUP and CHANGE RETENTION options.

AUTO_CLEANUP = { ON | OFF }
ON
Change tracking information is automatically removed after the specified retention period.

OFF
Change tracking data isn't removed from the database.

CHANGE_RETENTION =*retention_period* { DAYS | HOURS | MINUTES }
Specifies the minimum period for keeping change tracking information in the database. Data is removed only when the AUTO_CLEANUP value is ON.

*retention_period* is an integer that specifies the numerical component of the retention period.

The default retention period is 2 days. The minimum retention period is 1 minute. The default retention type is DAYS.

OFF
Disables change tracking for the database. Disable change tracking on all tables before you disable change tracking off the database.

**\<containment_option> ::=**

**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].

Controls database containment options.

CONTAINMENT = { NONE | PARTIAL}
NONE
The database isn't a contained database.

PARTIAL
The database is a contained database. Setting database containment to partial will fail if the database has replication, change data capture, or change tracking enabled. Error checking stops after one failure. For more information about contained databases, see [Contained Databases](../../relational-databases/databases/contained-databases.md).

**\<cursor_option> ::=**

Controls cursor options.

CURSOR_CLOSE_ON_COMMIT { ON | OFF }
ON
Any cursors open when you commit or roll back a transaction are closed.

OFF
Cursors remain open when a transaction is committed; rolling back a transaction closes any cursors except those defined as INSENSITIVE or STATIC.

Connection-level settings that are set by using the SET statement override the default database setting for CURSOR_CLOSE_ON_COMMIT. ODBC and OLE DB clients issue a connection-level SET statement setting CURSOR_CLOSE_ON_COMMIT to OFF for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET CURSOR_CLOSE_ON_COMMIT](../../t-sql/statements/set-cursor-close-on-commit-transact-sql.md).

You can determine this option's status by examining the is_cursor_close_on_commit_on column in the sys.databases catalog view or the IsCloseCursorsOnCommitEnabled property of the DATABASEPROPERTYEX function.

CURSOR_DEFAULT { LOCAL | GLOBAL }
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

Controls whether cursor scope uses LOCAL or GLOBAL.

LOCAL
When you specify LOCAL and don't define a cursor as GLOBAL when you create the cursor, the cursor's scope is local. Specifically, the scope is local to the batch, stored procedure, or trigger in which you created the cursor. The cursor name is valid only within this scope.

The cursor can be referenced by local cursor variables in the batch, stored procedure, or trigger, or a stored procedure OUTPUT parameter. The cursor is implicitly deallocated when the batch, stored procedure, or trigger ends. The cursor is deallocated unless it was passed back in an OUTPUT parameter. The cursor might be passed back in an OUTPUT parameter. If the cursor passes back this way, the cursor is deallocated when the last variable that references the cursor is deallocated or goes out of scope.

GLOBAL
When GLOBAL is specified, and a cursor isn't defined as LOCAL when created, the scope of the cursor is global to the connection. The cursor name can be referenced in any stored procedure or batch executed by the connection.

The cursor is implicitly deallocated only at disconnect. For more information, see [DECLARE CURSOR](../../t-sql/language-elements/declare-cursor-transact-sql.md).

You can determine this option's status by examining the is_local_cursor_default column in the sys.databases catalog view. You can also determine the status by examining the IsLocalCursorsDefault property of the DATABASEPROPERTYEX function.

**\<database_mirroring>**

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

For the argument descriptions, see [ALTER DATABASE Database Mirroring](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md).

**\<date_correlation_optimization_option> ::=**

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

Controls the date_correlation_optimization option.

DATE_CORRELATION_OPTIMIZATION { ON | OFF }
ON
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] maintains correlation statistics where a FOREIGN KEY constraint links any two tables in the database and the tables have **datetime** columns.

OFF
Correlation statistics are not maintained.

To set DATE_CORRELATION_OPTIMIZATION to ON, there must be no active connections to the database except for the connection that is executing the ALTER DATABASE statement. Afterwards, multiple connections are supported.

The current setting of this option can be determined by examining the is_date_correlation_on column in the sys.databases catalog view.

**\<db_encryption_option> ::=**

Controls the database encryption state.

ENCRYPTION {ON | OFF | SUSPEND | RESUME}
Sets the database to be encrypted (ON) or not encrypted (OFF). For more information about database encryption, see [Transparent Data Encryption](../../relational-databases/security/encryption/transparent-data-encryption.md), and [Transparent Data Encryption with Azure SQL Database](../../relational-databases/security/encryption/transparent-data-encryption-azure-sql.md).

In [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] and later, SUSPEND and RESUME options can be used to pause and resume the encryption scan after TDE has been enabled or disabled, or after the encryption key has been changed.

When encryption is enabled at the database level, all filegroups will be encrypted. Any new filegroups will inherit the encrypted property. If any filegroups in the database are set to **READ ONLY**, the database encryption operation will fail.

You can see the encryption state of the database as well as the state of the encryption scan by using the [sys.dm_database_encryption_keys](../../relational-databases/system-dynamic-management-views/sys-dm-database-encryption-keys-transact-sql.md) dynamic management view.

**\<db_state_option> ::=**

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

Controls the state of the database.

OFFLINE
The database is closed, shut down cleanly, and marked offline. The database can't be modified while it's offline.

ONLINE
The database is open and available for use.

EMERGENCY
The database is marked READ_ONLY, logging is disabled, and access is limited to members of the sysadmin fixed server role. EMERGENCY is primarily used for troubleshooting purposes. For example, a database marked as suspect because of a corrupted log file can be set to the EMERGENCY state. This setting could enable the system administrator read-only access to the database. Only members of the sysadmin fixed server role can set a database to the EMERGENCY state.

> [!NOTE]
> **Permissions:** ALTER DATABASE permission for the subject database is required to change a database to the offline or emergency state. The server level ALTER ANY DATABASE permission is required to move a database from offline to online.

You can determine this option's status by examining the state and state_desc columns in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. You can also determine the status by examining the Status property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function. For more information, see [Database States](../../relational-databases/databases/database-states.md).

A database marked as RESTORING can't be set to OFFLINE, ONLINE, or EMERGENCY. A database may be in the RESTORING state during an active restore operation or when a restore operation of a database or log file fails because of a corrupted backup file.

**\<db_update_option> ::=**

Controls whether updates are allowed on the database.

READ_ONLY
Users can read data from the database but not modify it.

> [!NOTE]
> To improve query performance, update statistics before setting a database to READ_ONLY. If additional statistics are needed after a database is set to READ_ONLY, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] will create statistics in tempdb. For more information about statistics for a read-only database, see [Statistics](../../relational-databases/statistics/statistics.md).

READ_WRITE
The database is available for read and write operations.

To change this state, you must have exclusive access to the database. For more information, see the SINGLE_USER clause.

> [!NOTE]
> On [!INCLUDE[ssSDS](../../includes/sssds-md.md)] federated databases, SET { READ_ONLY | READ_WRITE } is disabled.

**\<db_user_access_option> ::=**

Controls user access to the database.

SINGLE_USER
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

Specifies that only one user at a time can access the database. If you specify SINGLE_USER and other users connect to the database, the ALTER DATABASE statement is blocked until all users disconnect from the specified database. To override this behavior, see the WITH \<termination> clause.

The database remains in SINGLE_USER mode even if the user that set the option signs out. At that point, a different user, but only one, can connect to the database.

Before you set the database to SINGLE_USER, verify the AUTO_UPDATE_STATISTICS_ASYNC option is set to OFF. When set to ON, the background thread used to update statistics takes a connection against the database, and you will be unable to access the database in single-user mode. To view the status of this option, query the is_auto_update_stats_async_on column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. If the option is set to ON, perform the following tasks:

1. Set AUTO_UPDATE_STATISTICS_ASYNC to OFF.

2. Check for active asynchronous statistics jobs by querying the [sys.dm_exec_background_job_queue](../../relational-databases/system-dynamic-management-views/sys-dm-exec-background-job-queue-transact-sql.md) dynamic management view.

If there are active jobs, either allow the jobs to complete or manually terminate them by using [KILL STATS JOB](../../t-sql/language-elements/kill-stats-job-transact-sql.md).

RESTRICTED_USER
RESTRICTED_USER allows for only members of the db_owner fixed database role and dbcreator and sysadmin fixed server roles to connect to the database. RESTRICTED_USER doesn't limit their number. Disconnect all connections to the database using the timeframe specified by the ALTER DATABASE statement's termination clause. After the database has transitioned to the RESTRICTED_USER state, connection attempts by unqualified users are refused.

MULTI_USER
All users that have the appropriate permissions to connect to the database are allowed.

You can determine this option's status by examining the user_access column in the sys.databases catalog view. You can also determine the status by examining the UserAccess property of the DATABASEPROPERTYEX function.

**\<delayed_durability_option> ::=**

**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].

Controls whether transactions commit fully durable or delayed durable.

DISABLED
All transactions following SET DISABLED are fully durable. Any durability options set in an atomic block or commit statement are ignored.

ALLOWED
All transactions following SET ALLOWED are either fully durable or delayed durable, depending upon the durability option set in the atomic block or commit statement.

FORCED
All transactions following SET FORCED are delayed durable. Any durability options set in an atomic block or commit statement are ignored.

**\<external_access_option> ::=**

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

Controls whether the database can be accessed by external resources, such as objects from another database.

DB_CHAINING { ON | OFF }
ON
Database can be the source or target of a cross-database ownership chain.

OFF
Database can't participate in cross-database ownership chaining.

> [!IMPORTANT]
> The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will recognize this setting when the cross db ownership chaining server option is 0 (OFF). When cross db ownership chaining is 1 (ON), all user databases can participate in cross-database ownership chains, regardless of the value of this option. This option is set by using [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md).

To set this option, requires CONTROL SERVER permission on the database.

The DB_CHAINING option can't be set on these system databases: master, model, and tempdb.

You can determine this option's status by examining the is_db_chaining_on column in the sys.databases catalog view.

TRUSTWORTHY { ON | OFF }
ON
Database modules (for example, user-defined functions or stored procedures) that use an impersonation context can access resources outside the database.

OFF
Database modules in an impersonation context can't access resources outside the database.

TRUSTWORTHY is set to OFF whenever the database is attached.

By default, all system databases except the msdb database have TRUSTWORTHY set to OFF. The value can't be changed for the model and tempdb databases. We recommend that you never set the TRUSTWORTHY option to ON for the master database.

To set this option, requires CONTROL SERVER permission on the database.

You can determine this option's status by examining the is_trustworthy_on column in the sys.databases catalog view.

DEFAULT_FULLTEXT_LANGUAGE
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].

Specifies the default language value for full-text indexed columns.

> [!IMPORTANT]
> This option is allowable only when CONTAINMENT has been set to PARTIAL. If CONTAINMENT is set to NONE, errors will occur.

DEFAULT_LANGUAGE
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].

Specifies the default language for all newly created logins. Language can be specified by providing the local ID (lcid), the language name, or the language alias. For a list of acceptable language names and aliases, see [sys.syslanguages](../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md). This option is allowable only when CONTAINMENT has been set to PARTIAL. If CONTAINMENT is set to NONE, errors will occur.

NESTED_TRIGGERS
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].

Specifies whether an AFTER trigger can cascade; that is, perform an action that initiates another trigger, which initiates another trigger, and so on. This option is allowable only when CONTAINMENT has been set to PARTIAL. If CONTAINMENT is set to NONE, errors will occur.

TRANSFORM_NOISE_WORDS
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].

Used to suppress an error message if noise words, or stopwords, cause a Boolean operation on a full-text query to fail. This option is allowable only when CONTAINMENT has been set to PARTIAL. If CONTAINMENT is set to NONE, errors will occur.

TWO_DIGIT_YEAR_CUTOFF
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].

Specifies an integer from 1753 to 9999 that represents the cutoff year for interpreting two-digit years as four-digit years. This option is allowable only when CONTAINMENT has been set to PARTIAL. If CONTAINMENT is set to NONE, errors will occur.

**\<FILESTREAM_option> ::=**

**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].

Controls the settings for FileTables.

NON_TRANSACTED_ACCESS = { OFF | READ_ONLY | FULL }
OFF
Non-transactional access to FileTable data is disabled.

READ_ONLY
FILESTREAM data in FileTables in this database can be read by non-transactional processes.

FULL
Enables full, non-transactional access to FILESTREAM data in FileTables is enabled.

DIRECTORY_NAME = *\<directory_name>*
A windows-compatible directory name. This name should be unique among all the database-level directory names in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. Uniqueness comparison is case-insensitive, regardless of collation settings. This option must be set before creating a FileTable in this database.

**\<HADR_options> ::=**

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

See [ALTER DATABASE SET HADR](../../t-sql/statements/alter-database-transact-sql-set-hadr.md).

**\<mixed_page_allocation_option> ::=**

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [current version](https://go.microsoft.com/fwlink/p/?LinkId=299658)).

MIXED_PAGE_ALLOCATION { OFF | ON } controls whether the database can create initial pages using a mixed extent for the first eight pages of a table or index.

OFF
The database always creates initial pages using uniform extents. OFF is the default value.

ON
The database can create initial pages using mixed extents.

This setting is ON for all system databases. **tempdb** is the only system database that supports OFF.

**\<PARAMETERIZATION_option> ::=**

Controls the parameterization option. For more information on parameterization, see the [Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md#SimpleParam).

PARAMETERIZATION { SIMPLE | FORCED }
SIMPLE
Queries are parameterized based on the default behavior of the database.

FORCED
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] parameterizes all queries in the database.

The current setting of this option can be determined by examining the is_parameterization_forced column in the sys.databases catalog view.

**\<query_store_options> ::=**

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]).

ON | OFF | CLEAR [ ALL ]
Controls if the query store is enabled in this database, and also controls removing the contents of the query store. For more information, see [Query Store Usage Scenarios](../../relational-databases/performance/query-store-usage-scenarios.md).

ON
Enables the query store.

OFF
Disables the query store. OFF is the default value.

CLEAR
Remove the contents of the query store.

> [!NOTE]
> For Azure SQL Data Warehouse, you must execute `ALTER DATABASE SET QUERY_STORE` from the user database. Executing the statement from another data warehouse instance isn't supported.

OPERATION_MODE
Describes the operation mode of the query store. Valid values are READ_ONLY and READ_WRITE. In READ_WRITE mode, the query store collects and persists query plan and runtime execution statistics information. In READ_ONLY mode, information can be read from the query store, but new information isn't added. If the maximum issued space of the query store has been exhausted, the query store will change is operation mode to READ_ONLY.

CLEANUP_POLICY
Describes the data retention policy of the query store. STALE_QUERY_THRESHOLD_DAYS determines the number of days for which the information for a query is kept in the query store. STALE_QUERY_THRESHOLD_DAYS is type **bigint**.

DATA_FLUSH_INTERVAL_SECONDS
Determines the frequency at which data written to the query store is persisted to disk. To optimize for performance, data collected by the query store is asynchronously written to the disk. The frequency at which this asynchronous transfer occurs is configured by using the DATA_FLUSH_INTERVAL_SECONDS argument. DATA_FLUSH_INTERVAL_SECONDS is type **bigint**.

MAX_STORAGE_SIZE_MB
Determines the space issued to the query store. MAX_STORAGE_SIZE_MB is type **bigint**.

INTERVAL_LENGTH_MINUTES
Determines the time interval at which runtime execution statistics data is aggregated into the query store. To optimize for space usage, the runtime execution statistics in the runtime stats store are aggregated over a fixed time window. This fixed time window is configured by using the INTERVAL_LENGTH_MINUTES argument. INTERVAL_LENGTH_MINUTES is type **bigint**.

SIZE_BASED_CLEANUP_MODE
Controls whether cleanup automatically activates when total amount of data gets close to maximum size:
OFF
Size-based cleanup won't be automatically activated.

AUTO
Size-based cleanup will be automatically activated when size on disk reaches 90% of **max_storage_size_mb**. Size-based cleanup removes the least expensive and oldest queries first. It stops at approximately 80% of **max_storage_size_mb**.This value is the default configuration value.

SIZE_BASED_CLEANUP_MODE is type **nvarchar**.

QUERY_CAPTURE_MODE
Designates the currently active query capture mode:

ALL
Captures all queries. ALL is the default configuration value. This is the default configuration value for [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]

AUTO
Capture relevant queries based on execution count and resource consumption.

NONE
Stop capturing new queries. Query Store will continue to collect compile and runtime statistics for queries that were captured already. Use this configuration with caution since you may miss capturing important queries.

QUERY_CAPTURE_MODE is type **nvarchar**.

MAX_PLANS_PER_QUERY
An integer representing the maximum number of plans maintained for each query. Default is 200.

**\<recovery_option> ::=**

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

Controls database recovery options and disk I/O error checking.

FULL
Provides full recovery after media failure by using transaction log backups. If a data file is damaged, media recovery can restore all committed transactions. For more information, see [Recovery Models](../../relational-databases/backup-restore/recovery-models-sql-server.md).

BULK_LOGGED
Provides recovery after media failure. Combines the best performance and least amount of log-space use for certain large-scale or bulk operations. For information about what operations can be minimally logged, see [The Transaction Log](../../relational-databases/logs/the-transaction-log-sql-server.md). Under the BULK_LOGGED recovery model, logging for these operations is minimal. For more information, see [Recovery Models](../../relational-databases/backup-restore/recovery-models-sql-server.md).

SIMPLE
A simple backup strategy that uses minimal log space is provided. Log space can be automatically reused when it's no longer required for server failure recovery. For more information, see [Recovery Models](../../relational-databases/backup-restore/recovery-models-sql-server.md).

> [!IMPORTANT]
> The simple recovery model is easier to manage than the other two models but at the expense of greater data loss exposure if a data file is damaged. All changes since the most recent database or differential database backup are lost and must be manually reentered.

The default recovery model is determined by the recovery model of the **model** database. For more information about selecting the appropriate recovery model, see [Recovery Models](../../relational-databases/backup-restore/recovery-models-sql-server.md).

You can determine this option's status by examining the **recovery_model** and **recovery_model_desc** columns in the sys.databases catalog view. You can also determine the status by examining the Recovery property of the DATABASEPROPERTYEX function.

TORN_PAGE_DETECTION { ON | OFF }
ON
Incomplete pages can be detected by the [!INCLUDE[ssDE](../../includes/ssde-md.md)].

OFF
Incomplete pages can't be detected by the [!INCLUDE[ssDE](../../includes/ssde-md.md)].

> [!IMPORTANT]
> The syntax structure TORN_PAGE_DETECTION ON | OFF will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using this syntax structure in new development work, and plan to modify applications that currently use the syntax structure. Use the PAGE_VERIFY option instead.

<a name="page_verify"></a> PAGE_VERIFY { CHECKSUM | TORN_PAGE_DETECTION | NONE }
Discovers damaged database pages caused by disk I/O path errors. Disk I/O path errors can be the cause of database corruption problems. These errors are most often caused by power failures or disk hardware failures that occur at the time the page is written to disk.

CHECKSUM
Calculates a checksum over the contents of the whole page and stores the value in the page header when a page is written to disk. When the page is read from disk, the checksum is recomputed and compared to the checksum value stored in the page header. If the values don't match, error message 824 (indicating a checksum failure) is reported to both the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log and the Windows event log. A checksum failure indicates an I/O path problem. To determine the root cause requires investigation of the hardware, firmware drivers, BIOS, filter drivers (such as virus software), and other I/O path components.

TORN_PAGE_DETECTION
Saves a specific 2-bit pattern for each 512-byte sector in the 8-kilobyte (KB) database page and stored in the database page header when the page is written to disk. When the page is read from disk, the torn bits stored in the page header are compared to the actual page sector information.

Unmatched values indicate that only part of the page was written to disk. In this situation, error message 824 (indicating a torn page error) is reported to both the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log and the Windows event log. Torn pages are typically detected by database recovery if it is truly an incomplete write of a page. However, other I/O path failures can cause a torn page at any time.

NONE
Database page writes won't generate a CHECKSUM or TORN_PAGE_DETECTION value. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will not verify a checksum or torn page during a read even if a CHECKSUM or TORN_PAGE_DETECTION value is present in the page header.

Consider the following important points when you use the PAGE_VERIFY option:

- The default is CHECKSUM.
- When a user or system database is upgraded to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or a later version, the PAGE_VERIFY value (NONE or TORN_PAGE_DETECTION) is kept. We recommend that you use CHECKSUM.

    > [!NOTE]
    > In earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the PAGE_VERIFY database option is set to NONE for the tempdb database and can't be modified. In [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions, the default value for the tempdb database is CHECKSUM for new installations of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When upgrading an installation [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the default value remains NONE. The option can be modified. We recommend that you use CHECKSUM for the tempdb database.

- TORN_PAGE_DETECTION may use fewer resources but provides a minimal subset of the CHECKSUM protection.
- PAGE_VERIFY can be set without taking the database offline, locking the database, or otherwise impeding concurrency on that database.
- CHECKSUM is mutually exclusive to TORN_PAGE_DETECTION. Both options can't be enabled at the same time.

When a torn page or checksum failure is detected, you can recover by restoring the data or potentially rebuilding the index if the failure is limited only to index pages. If you encounter a checksum failure, to determine the type of database page or pages affected, run DBCC CHECKDB. For more information about restore options, see [RESTORE Arguments](../../t-sql/statements/restore-statements-arguments-transact-sql.md). Although restoring the data will resolve the data corruption problem, the root cause, for example, disk hardware failure, should be diagnosed and corrected as soon as possible to prevent continuing errors.

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will retry any read that fails with a checksum, torn page, or other I/O error four times. If the read is successful in any one of the retry attempts, a message is written to the error log. The command that triggered the read will continue. The command will fail with error message 824 if the retry attempts fail.

For more information about error messages 823, 824 and 825, see:

- [How to troubleshoot a Msg 823 error in SQL Server](https://support.microsoft.com/help/2015755)
- [How to troubleshoot Msg 824 in SQL Server](https://support.microsoft.com/help/2015756)
- [How to troubleshoot Msg - read retry](https://support.microsoft.com/help/2015757).

The current setting of this option can be determined by examining the *page_verify_option* column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or the *IsTornPageDetectionEnabled* property of the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.

**\<remote_data_archive_option> ::=**

**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].

Enables or disables Stretch Database for the database. For more info, see [Stretch Database](../../sql-server/stretch-database/stretch-database.md).

REMOTE_DATA_ARCHIVE = { ON ( SERVER = \<server_name> , { CREDENTIAL = \<db_scoped_credential_name> | FEDERATED_SERVICE_ACCOUNT = ON | OFF } )| OFF
ON
Enables Stretch Database for the database. For more info, including additional prerequisites, see [Enable Stretch Database for a database](../../sql-server/stretch-database/enable-stretch-database-for-a-database.md).

**Permissions**. Enabling Stretch Database for a database or a table requires db_owner permissions. Enabling Stretch Database for a database also requires CONTROL DATABASE permissions.

SERVER = \<server_name>
Specifies the address of the Azure server. Include the `.database.windows.net` portion of the name. For example, `MyStretchDatabaseServer.database.windows.net`.

CREDENTIAL = \<db_scoped_credential_name>
Specifies the database scoped credential that the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses to connect to the Azure server. Make sure the credential exists before you run this command. For more info, see [CREATE DATABASE SCOPED CREDENTIAL](../../t-sql/statements/create-database-scoped-credential-transact-sql.md).

FEDERATED_SERVICE_ACCOUNT = ON | OFF
You can use a federated service account for the on premises SQL Server to communicate with the remote Azure server when the following conditions are all true.

- The service account under which the instance of SQL Server is running is a domain account.
- The domain account belongs to a domain whose Active Directory is federated with Azure Active Directory.
- The remote Azure server is configured to support Azure Active Directory authentication.
- The service account under which the instance of SQL Server is running must be configured as a dbmanager or sysadmin account on the remote Azure server.

If you specify ON, you can't also specify the CREDENTIAL argument. Provide the CREDENTIAL argument if you specify OFF.

OFF
Disables Stretch Database for the database. For more info, see [Disable Stretch Database and bring back remote data](../../sql-server/stretch-database/disable-stretch-database-and-bring-back-remote-data.md).

You can only disable Stretch Database for a database after the database no longer contains any tables that are enabled for Stretch Database. After you disable Stretch Database, data migration stops. Also, query results no longer include results from remote tables.

Disabling Stretch doesn't remove the remote database. If you want to delete the remote database, you have to drop it by using the Azure portal.

**\<service_broker_option> ::=**

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

Controls the following [!INCLUDE[ssSB](../../includes/sssb-md.md)] options: enables or disables message delivery, sets a new [!INCLUDE[ssSB](../../includes/sssb-md.md)] identifier, or sets conversation priorities to ON or OFF.

ENABLE_BROKER
Specifies that [!INCLUDE[ssSB](../../includes/sssb-md.md)] is enabled for the specified database. Message delivery is started, and the is_broker_enabled flag is set to true in the sys.databases catalog view. The database keeps the existing [!INCLUDE[ssSB](../../includes/sssb-md.md)] identifier. Service broker can't be enabled while the database is the principal in a database mirroring configuration.

> [!NOTE]
> ENABLE_BROKER requires an exclusive database lock. If other sessions have locked resources in the database, ENABLE_BROKER will wait until the other sessions release their locks. To enable [!INCLUDE[ssSB](../../includes/sssb-md.md)] in a user database, ensure that no other sessions are using the database before you run the ALTER DATABASE SET ENABLE_BROKER statement, such as by putting the database in single user mode. To enable [!INCLUDE[ssSB](../../includes/sssb-md.md)] in the msdb database, first stop [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent so that [!INCLUDE[ssSB](../../includes/sssb-md.md)] can obtain the necessary lock.

DISABLE_BROKER
Specifies that [!INCLUDE[ssSB](../../includes/sssb-md.md)] is disabled for the specified database. Message delivery is stopped, and the is_broker_enabled flag is set to false in the sys.databases catalog view. The database keeps the existing [!INCLUDE[ssSB](../../includes/sssb-md.md)] identifier.

NEW_BROKER
Specifies that the database should receive a new broker identifier. The database acts as a new service broker. As such, all existing conversations in the database are immediately removed without producing end dialog messages. Any route that references the old [!INCLUDE[ssSB](../../includes/sssb-md.md)] identifier must be re-created with the new identifier.

ERROR_BROKER_CONVERSATIONS
Specifies that [!INCLUDE[ssSB](../../includes/sssb-md.md)] message delivery is enabled. This setting preserves the existing [!INCLUDE[ssSB](../../includes/sssb-md.md)] identifier for the database. [!INCLUDE[ssSB](../../includes/sssb-md.md)] ends all conversations in the database with an error. This setting enables applications to run regular cleanup for existing conversations.

HONOR_BROKER_PRIORITY {ON | OFF}
ON
Send operations take into consideration the priority levels that are assigned to conversations. Messages from conversations that have high priority levels are sent before messages from conversations that are assigned low-priority levels.

OFF
Send operations run as if all conversations have the default priority level.

Changes to the HONOR_BROKER_PRIORITY option take effect immediately for new dialogs or dialogs that have no messages waiting to be sent. Dialogs with messages to be sent when ALTER DATABASE is run won't pick up the new setting until some of the messages for the dialog are sent. The amount of time before all dialogs start using the new setting can vary considerably.

The current setting of this property is reported in the is_broker_priority_honored column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

**\<snapshot_option> ::=**

Calculates the transaction isolation level.

ALLOW_SNAPSHOT_ISOLATION { ON | OFF }
ON
Enables Snapshot option at the database level. When it is enabled, DML statements start generating row versions even when no transaction uses Snapshot Isolation. Once this option is enabled, transactions can specify the SNAPSHOT transaction isolation level. When a transaction runs at the SNAPSHOT isolation level, all statements see a snapshot of data as it exists at the start of the transaction. If a transaction running at the SNAPSHOT isolation level accesses data in multiple databases, either ALLOW_SNAPSHOT_ISOLATION must be set to ON in all the databases, or each statement in the transaction must use locking hints on any reference in a FROM clause to a table in a database where ALLOW_SNAPSHOT_ISOLATION is OFF.

OFF
Turns off the Snapshot option at the database level. Transactions can't specify the SNAPSHOT transaction isolation level.

When you set ALLOW_SNAPSHOT_ISOLATION to a new state (from ON to OFF, or from OFF to ON), ALTER DATABASE doesn't return control to the caller until all existing transactions in the database are committed. If the database is already in the state specified in the ALTER DATABASE statement, control is returned to the caller immediately. If the ALTER DATABASE statement doesn't return quickly, use [sys.dm_tran_active_snapshot_database_transactions](../../relational-databases/system-dynamic-management-views/sys-dm-tran-active-snapshot-database-transactions-transact-sql.md) to determine whether there are long-running transactions. If the ALTER DATABASE statement is canceled, the database remains in the state it was in when ALTER DATABASE was started. The [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view indicates the state of snapshot-isolation transactions in the database. If **snapshot_isolation_state_desc** = IN_TRANSITION_TO_ON, ALTER DATABASE ALLOW_SNAPSHOT_ISOLATION OFF will pause six seconds and retry the operation.

You can't change the state of ALLOW_SNAPSHOT_ISOLATION if the database is OFFLINE.

If you set ALLOW_SNAPSHOT_ISOLATION in a READ_ONLY database, the setting will be kept if the database is later set to READ_WRITE.

You can change the ALLOW_SNAPSHOT_ISOLATION settings for the master, model, msdb, and tempdb databases. The setting is kept every time the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is stopped and restarted if you change the setting for tempdb. If you change the setting for model, that setting becomes the default for any new databases that are created, except for tempdb.

The option is ON, by default, for the master and msdb databases.

The current setting of this option can be determined by examining the snapshot_isolation_state column in the sys.databases catalog view.

READ_COMMITTED_SNAPSHOT { ON | OFF }
ON
Enables Read-Committed Snapshot option at the database level. When it's enabled, DML statements start generating row versions even when no transaction uses Snapshot Isolation. Once this option is enabled, the transactions specifying the read committed isolation level use row versioning instead of locking. All statements see a snapshot of data as it exists at the start of the statement when a transaction runs at the read committed isolation level.

OFF
Turns off Read-Committed Snapshot option at the database level. Transactions specifying the READ COMMITTED isolation level use locking.

To set READ_COMMITTED_SNAPSHOT ON or OFF, there must be no active connections to the database except for the connection executing the ALTER DATABASE command. However, the database doesn't have to be in single-user mode. You can't change the state of this option when the database is OFFLINE.

If you set READ_COMMITTED_SNAPSHOT in a READ_ONLY database, the setting will be kept when the database is later set to READ_WRITE.

READ_COMMITTED_SNAPSHOT can't be turned ON for the master, tempdb, or msdb system databases. If you change the setting for model, that setting becomes the default for any new databases created, except for tempdb.

The current setting of this option can be determined by examining the is_read_committed_snapshot_on column in the sys.databases catalog view.

> [!WARNING]
>When a table is created with **DURABILITY = SCHEMA_ONLY**, and **READ_COMMITTED_SNAPSHOT** is subsequently changed using **ALTER DATABASE**, data in the table will be lost.

MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT { ON | OFF }
**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].

ON
  When the transaction isolation level is set to any isolation level lower than SNAPSHOT, all interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] operations on memory-optimized tables are run under SNAPSHOT isolation. Examples of isolation levels lower than snapshot are READ COMMITTED or READ UNCOMMITTED. These operations run whether the transaction isolation level is set explicitly at the session level, or the default is used implicitly.

OFF
doesn't elevate the transaction isolation level for interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] operations on memory-optimized tables.

You can't change the state of MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT if the database is OFFLINE.

The option is OFF, by default.

The current setting of this option can be determined by examining the **is_memory_optimized_elevate_to_snapshot_on** column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

**\<sql_option> ::=**

Controls the ANSI compliance options at the database level.

ANSI_NULL_DEFAULT { ON | OFF }
Determines the default value, NULL or NOT NULL, of a column or [CLR user-defined type](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md) for which the nullability isn't explicitly defined in CREATE TABLE or ALTER TABLE statements. Columns that are defined with constraints follow constraint rules whatever this setting may be.

ON
The default value is NULL.

OFF
The default value isn't NULL.

Connection-level settings that are set by using the SET statement override the default database-level setting for ANSI_NULL_DEFAULT. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_NULL_DEFAULT to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_NULL_DFLT_ON](../../t-sql/statements/set-ansi-null-dflt-on-transact-sql.md).

For ANSI compatibility, setting the database option ANSI_NULL_DEFAULT to ON changes the database default to NULL.

You can determine this option's status by examining the is_ansi_null_default_on column in the sys.databases catalog view. You can also determine the status by examining the IsAnsiNullDefault property of the DATABASEPROPERTYEX function.

ANSI_NULLS { ON | OFF }
ON
All comparisons to a null value evaluate to UNKNOWN.

OFF
Comparisons of non-UNICODE values to a null value evaluate to TRUE if both values are NULL.

> [!IMPORTANT]
> In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], ANSI_NULLS will always be ON and any applications that explicitly set the option to OFF will produce an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.

  Connection-level settings that are set by using the SET statement override the default database setting for ANSI_NULLS. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_NULLS to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_NULLS](../../t-sql/statements/set-ansi-nulls-transact-sql.md).

SET ANSI_NULLS also must be set to ON when you create or make changes to indexes on computed columns or indexed views.

You can determine this option's status by examining the is_ansi_nulls_on column in the sys.databases catalog view. You can also determine the status by examining the IsAnsiNullsEnabled property of the DATABASEPROPERTYEX function.

ANSI_PADDING { ON | OFF }
ON
Strings are padded to the same length before conversion. Also padded to the same length before inserting to a **varchar** or **nvarchar** data type.

OFF
Inserts trailing blanks in character values into **varchar** or **nvarchar** columns. Also leaves trailing zeros in binary values that are inserted into **varbinary** columns. Values aren't padded to the length of the column.

When OFF is specified, this setting affects only the definition of new columns.

> [!IMPORTANT]
> In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], ANSI_PADDING will always be ON and any applications that explicitly set the option to OFF will produce an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. We recommend that you always set ANSI_PADDING to ON. ANSI_PADDING must be ON when you create or manipulate indexes on computed columns or indexed views.

**char(_n_)** and **binary(_n_)** columns that allow for nulls are padded to the column length when ANSI_PADDING is set to ON. Trailing blanks and zeros are trimmed when ANSI_PADDING is OFF. **char(_n_)** and **binary(_n_)** columns that don't allow nulls are always padded to the length of the column.

  Connection-level settings that are set by using the SET statement override the default database-level setting for ANSI_PADDING. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_PADDING to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_PADDING](../../t-sql/statements/set-ansi-padding-transact-sql.md).

You can determine this option's status by examining the is_ansi_padding_on column in the sys.databases catalog view. You can also determine the status by examining the IsAnsiPaddingEnabled property of the DATABASEPROPERTYEX function.

ANSI_WARNINGS { ON | OFF }
ON
Errors or warnings are issued when conditions such as divide-by-zero occur. Errors and warnings are also issued when null values appear in aggregate functions.

OFF
No warnings are raised and null values are returned when conditions such as divide-by-zero occur.

SET ANSI_WARNINGS must be set to ON when you create or make changes to indexes on computed columns or indexed views.

  Connection-level settings that are set by using the SET statement override the default database setting for ANSI_WARNINGS. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_WARNINGS to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_WARNINGS](../../t-sql/statements/set-ansi-warnings-transact-sql.md).

You can determine this option's status by examining the is_ansi_warnings_on column in the sys.databases catalog view. You can also determine the status by examining the IsAnsiWarningsEnabled property of the DATABASEPROPERTYEX function.

ARITHABORT { ON | OFF }
ON
A query is ended when an overflow or divide-by-zero error occurs during query execution.

OFF
A warning message is displayed when one of these errors occurs. The query, batch, or transaction continues to process as if no error occurred even if a warning is displayed.

SET ARITHABORT must be set to ON when you create or make changes to indexes on computed columns or indexed views.

  You can determine this option's status by examining the is_arithabort_on column in the sys.databases catalog view. You can also determine the status by examining the IsArithmeticAbortEnabled property of the DATABASEPROPERTYEX function.

COMPATIBILITY_LEVEL = { 140 | 130 | 120 | 110 | 100 | 90 }
For more information, see [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

CONCAT_NULL_YIELDS_NULL { ON | OFF }
ON
The result of a concatenation operation is NULL when either operand is NULL. For example, concatenating the character string "This is" and NULL causes the value NULL, instead of the value "This is".

OFF
The null value is treated as an empty character string.

CONCAT_NULL_YIELDS_NULL must be set to ON when you create or make changes to indexes on computed columns or indexed views.

> [!IMPORTANT]
> In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], CONCAT_NULL_YIELDS_NULL will always be ON and any applications that explicitly set the option to OFF will produce an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.

Connection-level settings that are set by using the SET statement override the default database setting for CONCAT_NULL_YIELDS_NULL. By default, ODBC and OLE DB clients issue a connection-level SET statement setting CONCAT_NULL_YIELDS_NULL to ON for the session when connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET CONCAT_NULL_YIELDS_NULL](../../t-sql/statements/set-concat-null-yields-null-transact-sql.md).

You can determine this option's status by examining the is_concat_null_yields_null_on column in the sys.databases catalog view. You can also determine the status by examining the IsNullConcat property of the DATABASEPROPERTYEX function.

QUOTED_IDENTIFIER { ON | OFF }
ON
Double quotation marks can be used to enclose delimited identifiers.

All strings delimited by double quotation marks are interpreted as object identifiers. Quoted identifiers don't have to follow the [!INCLUDE[tsql](../../includes/tsql-md.md)] rules for identifiers. They can be keywords and can include characters not allowed in [!INCLUDE[tsql](../../includes/tsql-md.md)] identifiers. If a single quotation mark (') is part of the literal string, it can be represented by double quotation marks (").

OFF
Identifiers can't be in quotation marks and must follow all [!INCLUDE[tsql](../../includes/tsql-md.md)] rules for identifiers. Literals can be delimited by either single or double quotation marks.

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also allows for identifiers to be delimited by square brackets ([ ]). Bracketed identifiers can always be used, whatever the QUOTED_IDENTIFIER setting is. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).

  When a table is created, the QUOTED IDENTIFIER option is always stored as ON in the metadata of the table. The option is stored even if the option is set to OFF when the table is created.

Connection-level settings that are set by using the SET statement override the default database setting for QUOTED_IDENTIFIER. ODBC and OLE DB clients issue a connection-level SET statement setting QUOTED_IDENTIFIER to ON, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET QUOTED_IDENTIFIER](../../t-sql/statements/set-quoted-identifier-transact-sql.md).

  You can determine this option's status by examining the is_quoted_identifier_on column in the sys.databases catalog view. You can also determine the status by examining the IsQuotedIdentifiersEnabled property of the DATABASEPROPERTYEX function.

NUMERIC_ROUNDABORT { ON | OFF }
ON
An error is generated when loss of precision occurs in an expression.

OFF
Losses of precision don't generate error messages and the result is rounded to the precision of the column or variable storing the result.

NUMERIC_ROUNDABORT must be set to OFF when you create or make changes to indexes on computed columns or indexed views.

You can determine this option's status by examining the is_numeric_roundabort_on column in the sys.databases catalog view. You can also determine the status by examining the IsNumericRoundAbortEnabled property of the DATABASEPROPERTYEX function.

RECURSIVE_TRIGGERS { ON | OFF }
ON
Recursive firing of AFTER triggers is allowed.

OFF
You can determine this option's status by examining the is_recursive_triggers_on column in the sys.databases catalog view. You can also determine the status by examining the IsRecursiveTriggersEnabled property of the DATABASEPROPERTYEX function.

> [!NOTE]
> Only direct recursion is prevented when RECURSIVE_TRIGGERS is set to OFF. To disable indirect recursion, you must also set the nested triggers server option to 0.

You can determine this option's status by examining the is_recursive_triggers_on column in the sys.databases catalog view or the IsRecursiveTriggersEnabled property of the DATABASEPROPERTYEX function.

**\<target_recovery_time_option> ::=**

**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].

Specifies the frequency of indirect checkpoints on a per-database basis. Beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] the default value for new databases is 1 minute, which indicates database will use indirect checkpoints. For older versions the default is 0, which indicates that the database will use automatic checkpoints, whose frequency depends on the recovery interval setting of the server instance. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends 1 minute for most systems.

TARGET_RECOVERY_TIME **=**_target_recovery_time_ { SECONDS | MINUTES }
*target_recovery_time*
Specifies the maximum bound on the time to recover the specified database in the event of a crash.

SECONDS
Indicates that *target_recovery_time* is expressed as the number of seconds.

MINUTES
Indicates that *target_recovery_time* is expressed as the number of minutes.

For more information about indirect checkpoints, see [Database Checkpoints](../../relational-databases/logs/database-checkpoints-sql-server.md).

**WITH \<termination> ::=**

Specifies when to roll back incomplete transactions when the database is transitioned from one state to another. If the termination clause is omitted, the ALTER DATABASE statement waits indefinitely if there's any lock on the database. Only one termination clause can be specified, and it follows the SET clauses.

> [!NOTE]
> Not all database options use the WITH \<termination> clause. For more information, see the table under "[Setting Options](#SettingOptions) of the "Remarks" section of this article.

ROLLBACK AFTER *integer* [SECONDS] | ROLLBACK IMMEDIATE
Specifies whether to roll back after the specified number of seconds or immediately.

NO_WAIT
Specifies that the request will fail if the requested database state or option change can't complete immediately. Completing immediately means not waiting for transactions to commit or roll back on their own.

## <a name="SettingOptions"></a> Setting Options

To retrieve current settings for database options, use the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)

After you set a database option, the modification takes effect immediately.

You can change the default values for any one of the database options for all newly created databases. To do so, change the appropriate database option in the model database.

Not all database options use the WITH \<termination> clause or can be specified in combination with other options. The following table lists these options and their option and termination status.

|Options category|Can be specified with other options|Can use the WITH \<termination> clause|
|----------------------|-----------------------------------------|---------------------------------------------|
|\<db_state_option>|Yes|Yes|
|\<db_user_access_option>|Yes|Yes|
|\<db_update_option>|Yes|Yes|
|\<delayed_durability_option>|Yes|Yes|
|\<external_access_option>|Yes|No|
|\<cursor_option>|Yes|No|
|\<auto_option>|Yes|No|
|\<sql_option>|Yes|No|
|\<recovery_option>|Yes|No|
|\<target_recovery_time_option>|No|Yes|
|\<database_mirroring_option>|No|No|
|ALLOW_SNAPSHOT_ISOLATION|No|No|
|READ_COMMITTED_SNAPSHOT|No|Yes|
|MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT|Yes|Yes|
|\<service_broker_option>|Yes|No|
|DATE_CORRELATION_OPTIMIZATION|Yes|Yes|
|\<parameterization_option>|Yes|Yes|
|\<change_tracking_option>|Yes|Yes|
|\<db_encryption_option>|Yes|No|

The plan cache for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is cleared by setting one of the following options:

|||
|-|-|
|OFFLINE|READ_WRITE|
|ONLINE|MODIFY FILEGROUP DEFAULT|
|MODIFY_NAME|MODIFY FILEGROUP READ_WRITE|
|COLLATE|MODIFY FILEGROUP READ_ONLY|
|READ_ONLY||

The procedure cache is also flushed in the following scenarios.

- A database has the AUTO_CLOSE database option set to ON. When no user connection references or uses the database, the background task tries to close and shut down the database automatically.
- You run several queries against a database that has default options. Then, the database is dropped.
- A database snapshot for a source database is dropped.
- You successfully rebuild the transaction log for a database.
- You restore a database backup.
- You detach a database.

Clearing the plan cache causes a recompilation of all subsequent execution plans and can cause a sudden, temporary decrease in query performance. For each cleared cachestore in the plan cache, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log contains the following informational message: " [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has encountered %d occurrence(s) of cachestore flush for the '%s' cachestore (part of plan cache) due to some database maintenance or reconfigure operations". This message is logged every five minutes as long as the cache is flushed within that time interval.

## Examples

### A. Setting options on a database

The following example sets the recovery model and data page verification options for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database.

```sql
USE master;
GO
ALTER DATABASE AdventureWorks2012
SET RECOVERY FULL PAGE_VERIFY CHECKSUM;
GO

```

### B. Setting the database to READ_ONLY

Changing the state of a database or filegroup to READ_ONLY or READ_WRITE requires exclusive access to the database. The following example sets the database to `SINGLE_USER` mode to obtain exclusive access. The example then sets the state of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to `READ_ONLY` and returns access to the database to all users.

> [!NOTE]
>This example uses the termination option `WITH ROLLBACK IMMEDIATE` in the first `ALTER DATABASE` statement. All incomplete transactions will be rolled back and any other connections to the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database will be immediately disconnected.

```sql
USE master;
GO
ALTER DATABASE AdventureWorks2012
SET SINGLE_USER
WITH ROLLBACK IMMEDIATE;
GO
ALTER DATABASE AdventureWorks2012
SET READ_ONLY
GO
ALTER DATABASE AdventureWorks2012
SET MULTI_USER;
GO

```

### C. Enabling snapshot isolation on a database

The following example enables the snapshot isolation framework option for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2012;
USE master;
GO
ALTER DATABASE AdventureWorks2012
SET ALLOW_SNAPSHOT_ISOLATION ON;
GO
-- Check the state of the snapshot_isolation_framework
-- in the database.
SELECT name, snapshot_isolation_state,
    snapshot_isolation_state_desc AS description
FROM sys.databases
WHERE name = N'AdventureWorks2012';
GO

```

The result set shows that the snapshot isolation framework is enabled.

|name |snapshot_isolation_state |description|
|-------------------- |------------------------|----------|
|AdventureWorks2012 |1| ON |

### D. Enabling, modifying, and disabling change tracking

The following example enables change tracking for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database and sets the retention period to `2` days.

```sql
ALTER DATABASE AdventureWorks2012
SET CHANGE_TRACKING = ON
(AUTO_CLEANUP = ON, CHANGE_RETENTION = 2 DAYS);
```

The following example shows how to change the retention period to `3` days.

```sql
ALTER DATABASE AdventureWorks2012
SET CHANGE_TRACKING (CHANGE_RETENTION = 3 DAYS);
```

The following example shows how to disable change tracking for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
ALTER DATABASE AdventureWorks2012
SET CHANGE_TRACKING = OFF;
```

### E. Enabling the query store

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]).

The following example enables the query store and configures query store parameters.

```sql
ALTER DATABASE AdventureWorks2012
SET QUERY_STORE = ON
    (
      OPERATION_MODE = READ_WRITE
    , CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = 90 )
    , DATA_FLUSH_INTERVAL_SECONDS = 900
    , MAX_STORAGE_SIZE_MB = 1024
    , INTERVAL_LENGTH_MINUTES = 60
    );
```

## See Also

- [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
- [ALTER DATABASE Database Mirroring](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md)
- [ALTER DATABASE SET HADR](../../t-sql/statements/alter-database-transact-sql-set-hadr.md)
- [Statistics](../../relational-databases/statistics/statistics.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md?view=sql-server-2017)
- [Enable and Disable Change Tracking](../../relational-databases/track-changes/enable-and-disable-change-tracking-sql-server.md)
- [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)
- [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md)
- [SET TRANSACTION ISOLATION LEVEL](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md)
- [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [sys.data_spaces](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md)
- [Best Practice with the Query Store](../../relational-databases/performance/best-practice-with-the-query-store.md)

::: moniker-end
::: moniker range="=azuresqldb-current||=sqlallproducts-allversions"

> |||
> |---|---|
> |[SQL Server](alter-database-transact-sql-set-options.md?view=sql-server-2017)|**_\* SQL Database<br />single database/elastic pool \*_** &nbsp;|[SQL Database<br />managed instance](alter-database-transact-sql-set-options.md?view=azuresqldb-mi-current)|

&nbsp;

## Azure SQL Database single database/elastic pool

Compatibility levels are `SET` options but are described in [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

> [!NOTE]
> Many database set options can be configured for the current session by using [SET Statements](../../t-sql/statements/set-statements-transact-sql.md) and are often configured by applications when they connect. Session level set options override the **ALTER DATABASE SET** values. The database options described below are values that can be set for sessions that don't explicitly provide other set option values.

## Syntax

```
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
{AUTOMATIC_TUNING = { AUTO | INHERIT | CUSTOM }
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
  | SIZE_BASED_CLEANUP_MODE = [ AUTO | OFF ]
  | QUERY_CAPTURE_MODE = [ ALL | AUTO | NONE ]
  | MAX_PLANS_PER_QUERY = number
}

<snapshot_option> ::=
{
    ALLOW_SNAPSHOT_ISOLATION { ON | OFF }
  | READ_COMMITTED_SNAPSHOT {ON | OFF }
  | MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT {ON | OFF }
}
<sql_option> ::=
{
    ANSI_NULL_DEFAULT { ON | OFF }
  | ANSI_NULLS { ON | OFF }
  | ANSI_PADDING { ON | OFF }
  | ANSI_WARNINGS { ON | OFF }
  | ARITHABORT { ON | OFF }
  | COMPATIBILITY_LEVEL = { 140 | 130 | 120 | 110 | 100 }
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

*database_name*
Is the name of the database to be modified.

CURRENT
`CURRENT` runs the action in the current database. `CURRENT` isn't supported for all options in all contexts. If `CURRENT` fails, provide the database name.

**\<auto_option> ::=**

Controls automatic options.
<a name="auto_create_statistics"></a> AUTO_CREATE_STATISTICS { ON | OFF }
ON
The query optimizer creates statistics on single columns in query predicates, as necessary, to improve query plans and query performance. These single-column statistics are created when the query optimizer compiles queries. The single-column statistics are created only on columns that are not already the first column of an existing statistics object.

The default is ON. We recommend that you use the default setting for most databases.

OFF
The query optimizer doesn't create statistics on single columns in query predicates when it is compiling queries. Setting this option to OFF can cause suboptimal query plans and degraded query performance.

You can determine this option's status by examining the is_auto_create_stats_on column in the sys.databases catalog view. You can also determine the status by examining the IsAutoCreateStatistics property of the DATABASEPROPERTYEX function.

For more information, see the section "Using the Database-Wide Statistics Options" in [Statistics](../../relational-databases/statistics/statistics.md).

INCREMENTAL = ON | OFF
Set AUTO_CREATE_STATISTICS to ON, and set INCREMENTAL to ON. This setting creates automatically created stats as incremental whenever incremental stats are supported. The default value is OFF. For more information, see [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md).

<a name="auto_shrink"></a> AUTO_SHRINK { ON | OFF }
ON
The database files are candidates for periodic shrinking.

Both data file and log files can be automatically shrunk. AUTO_SHRINK reduces the size of the transaction log only if you set the database to SIMPLE recovery model or if you back up the log. When set to OFF, the database files aren't automatically shrunk during periodic checks for unused space.

The AUTO_SHRINK option causes files to be shrunk when more than 25 percent of the file contains unused space. The option causes the file to shrink to one of two sizes. It shrinks to whichever is larger:

- the size where 25 percent of the file is unused space
- the size of the file when it was created

You can't shrink a read-only database.

OFF
The database files are not automatically shrunk during periodic checks for unused space.

You can determine this option's status by examining the is_auto_shrink_on column in the sys.databases catalog view. You can also determine the status by examining the IsAutoShrink property of the DATABASEPROPERTYEX function.

> [!NOTE]
> The AUTO_SHRINK option isn't available in a Contained Database.

<a name="auto_update_statistics"></a> AUTO_UPDATE_STATISTICS { ON | OFF }
ON
Specifies that the query optimizer updates statistics when they're used by a query and when they might be out-of-date. Statistics become out-of-date after insert, update, delete, or merge operations change the data distribution in the table or indexed view. The query optimizer determines when statistics might be out-of-date by counting the number of data modifications since the last statistics update and comparing the number of modifications to a threshold. The threshold is based on the number of rows in the table or indexed view.

The query optimizer checks for out-of-date statistics before it compiles a query and it runs a cached query plan. The query optimizer uses the columns, tables, and indexed views in the query predicate to determine which statistics might be out-of-date. The query optimizer determines this information before it compiles a query. Before executing a cached query plan, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] verifies that the query plan references up-to-date statistics.

The AUTO_UPDATE_STATISTICS option applies to statistics created for indexes, single-columns in query predicates, and statistics that are created by using the CREATE STATISTICS statement. This option also applies to filtered statistics.

The default is ON. We recommend that you use the default setting for most databases.

Use the AUTO_UPDATE_STATISTICS_ASYNC option to specify whether the statistics are updated synchronously or asynchronously.

OFF
Specifies that the query optimizer doesn't update statistics when they're used by a query. The query optimizer also doesn't update statistics when they might be out-of-date. Setting this option to OFF can cause suboptimal query plans and degraded query performance.

You can determine this option's status by examining the is_auto_update_stats_on column in the sys.databases catalog view. You can also determine the status by examining the IsAutoUpdateStatistics property of the DATABASEPROPERTYEX function.

For more information, see the section "Using the Database-Wide Statistics Options" in [Statistics](../../relational-databases/statistics/statistics.md).

<a name="auto_update_statistics_async"></a> AUTO_UPDATE_STATISTICS_ASYNC { ON | OFF }
ON
Specifies that statistics updates for the AUTO_UPDATE_STATISTICS option are asynchronous. The query optimizer doesn't wait for statistics updates to complete before it compiles queries.

Setting this option to ON has no effect unless AUTO_UPDATE_STATISTICS is set to ON.

By default, the AUTO_UPDATE_STATISTICS_ASYNC option is set to OFF, and the query optimizer updates statistics synchronously.

OFF
Specifies that statistics updates for the AUTO_UPDATE_STATISTICS option are synchronous. The query optimizer waits for statistics updates to complete before it compiles queries.

Setting this option to OFF has no effect unless AUTO_UPDATE_STATISTICS is set to ON.

You can determine this option's status by examining the is_auto_update_stats_async_on column in the sys.databases catalog view.

For more information that describes when to use synchronous or asynchronous statistics updates, see the section "Using the Database-Wide Statistics Options" in [Statistics](../../relational-databases/statistics/statistics.md).

<a name="auto_tuning"></a> **\<automatic_tuning_option> ::=**
**Applies to**: [!INCLUDE[sssqlv14-md](../../includes/sssqlv14-md.md)].

Controls automatic options for [automatic tuning](../../relational-databases/automatic-tuning/automatic-tuning.md).

AUTOMATIC_TUNING = { AUTO | INHERIT | CUSTOM }
AUTO
Setting the automatic tuning value to AUTO will apply Azure configuration defaults for automatic tuning.

INHERIT
Using the value INHERIT will inherit the default configuration from the parent server. This is especially useful if you would like to customize automatic tuning configuration on a parent server, and have all the databases on such server INHERIT these custom settings. Please note that in order for the inheritance to work, the three individual tuning options FORCE_LAST_GOOD_PLAN, CREATE_INDEX and DROP_INDEX need to be set to DEFAULT on databases.

CUSTOM
Using the value CUSTOM, you will need to manually custom configure each of the automatic tuning options available on databases.

Enables or disables automatic index management `CREATE_INDEX` option of [automatic tuning](../../relational-databases/automatic-tuning/automatic-tuning.md).

CREATE_INDEX = { DEFAULT | ON | OFF }
DEFALT
Inherits default settings from the server. In this case, options of enabling or disabling individual Automatic tuning features are defined at the server level.

ON
When enabled, missing indexes are automatically generated on a database. Following the index creation, gains to the performance of the workload are verified. When such created index no longer provides benefits to the workload performance, it is automatically reverted. Indexes automatically created are flagged as a system generated indexed.

OFF
doesn't automatically generate missing indexes on the database.

Enables or disables automatic index management `DROP_INDEX` option of [automatic tuning](../../relational-databases/automatic-tuning/automatic-tuning.md).

DROP_INDEX = { DEFAULT | ON | OFF }
DEFAULT
Inherits default settings from the server. In this case, options of enabling or disabling individual Automatic tuning features are defined at the server level.

ON
Automatically drops duplicate or no longer useful indexes to the performance workload.

OFF
doesn't automatically drop missing indexes on the database.

Enables or disables automatic plan correction `FORCE_LAST_GOOD_PLAN` option of [automatic tuning](../../relational-databases/automatic-tuning/automatic-tuning.md).

FORCE_LAST_GOOD_PLAN = { DEFAULT | ON | OFF }
DEFAULT
Inherits default settings from the server. In this case, options of enabling or disabling individual Automatic tuning features are defined at the server level.

ON
The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] automatically forces the last known good plan on the [!INCLUDE[tsql-md](../../includes/tsql-md.md)] queries where new SQL plan causes performance regressions. The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] continuously monitors query performance of the [!INCLUDE[tsql-md](../../includes/tsql-md.md)] query with the forced plan. If there are performance gains, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] will keep using last known good plan. If performance gains are not detected, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] will produce a new SQL plan. The statement will fail if Query Store isn't enabled or if it isn't in *Read-Write* mode.

OFF
The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] reports potential query performance regressions caused by SQL plan changes in [sys.dm_db_tuning_recommendations](../../relational-databases/system-dynamic-management-views/sys-dm-db-tuning-recommendations-transact-sql.md) view. However, these recommendations are not automatically applied. User can monitor active recommendations and fix identified problems by applying [!INCLUDE[tsql-md](../../includes/tsql-md.md)] scripts that are shown in the view. This is the default value.

**\<change_tracking_option> ::=**

Controls change tracking options. You can enable change tracking, set options, change options, and disable change tracking. For examples, see the Examples section later in this article.

ON
Enables change tracking for the database. When you enable change tracking, you can also set the AUTO CLEANUP and CHANGE RETENTION options.

AUTO_CLEANUP = { ON | OFF }
ON
Change tracking information is automatically removed after the specified retention period.

OFF
Change tracking data isn't removed from the database.

CHANGE_RETENTION =*retention_period* { DAYS | HOURS | MINUTES }
Specifies the minimum period for keeping change tracking information in the database. Data is removed only when the AUTO_CLEANUP value is ON.

*retention_period* is an integer that specifies the numerical component of the retention period.

The default retention period is 2 days. The minimum retention period is 1 minute. The default retention type is DAYS.

OFF
Disables change tracking for the database. Disable change tracking on all tables before you disable change tracking off the database.

**\<cursor_option> ::=**

Controls cursor options.

CURSOR_CLOSE_ON_COMMIT { ON | OFF }
ON
Any cursors open when you commit or roll back a transaction are closed.

OFF
Cursors remain open when a transaction is committed; rolling back a transaction closes any cursors except those defined as INSENSITIVE or STATIC.

Connection-level settings that are set by using the SET statement override the default database setting for CURSOR_CLOSE_ON_COMMIT. ODBC and OLE DB clients issue a connection-level SET statement setting CURSOR_CLOSE_ON_COMMIT to OFF for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET CURSOR_CLOSE_ON_COMMIT](../../t-sql/statements/set-cursor-close-on-commit-transact-sql.md).

You can determine this option's status by examining the is_cursor_close_on_commit_on column in the sys.databases catalog view or the IsCloseCursorsOnCommitEnabled property of the DATABASEPROPERTYEX function. The cursor is implicitly deallocated only at disconnect. For more information, see [DECLARE CURSOR](../../t-sql/language-elements/declare-cursor-transact-sql.md).

**\<db_encryption_option> ::=**

Controls the database encryption state.

ENCRYPTION {ON | OFF}
Sets the database to be encrypted (ON) or not encrypted (OFF). For more information about database encryption, see [Transparent Data Encryption](../../relational-databases/security/encryption/transparent-data-encryption.md), and [Transparent Data Encryption with Azure SQL Database](../../relational-databases/security/encryption/transparent-data-encryption-azure-sql.md).

When encryption is enabled at the database level, all filegroups will be encrypted. Any new filegroups will inherit the encrypted property. If any filegroups in the database are set to **READ ONLY**, the database encryption operation will fail.

You can see the encryption state of the database by using the [sys.dm_database_encryption_keys](../../relational-databases/system-dynamic-management-views/sys-dm-database-encryption-keys-transact-sql.md) dynamic management view.

**\<db_update_option> ::=**

Controls whether updates are allowed on the database.

READ_ONLY
Users can read data from the database but not modify it.

> [!NOTE]
>To improve query performance, update statistics before setting a database to READ_ONLY. If additional statistics are needed after a database is set to READ_ONLY, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] will create statistics in tempdb. For more information about statistics for a read-only database, see [Statistics](../../relational-databases/statistics/statistics.md).

READ_WRITE
The database is available for read and write operations.

To change this state, you must have exclusive access to the database. For more information, see the SINGLE_USER clause.

> [!NOTE]
> On [!INCLUDE[ssSDS](../../includes/sssds-md.md)] federated databases, SET { READ_ONLY | READ_WRITE } is disabled.

**\<db_user_access_option> ::=**

Controls user access to the database.

RESTRICTED_USER
RESTRICTED_USER allows for only members of the db_owner fixed database role and dbcreator and sysadmin fixed server roles to connect to the database, but doesn't limit their number. All connections to the database are disconnected in the timeframe specified by the termination clause of the ALTER DATABASE statement. After the database has transitioned to the RESTRICTED_USER state, connection attempts by unqualified users are refused. **RESTRICTED_USER** can't be modified with SQL Database managed instance.

MULTI_USER
All users that have the appropriate permissions to connect to the database are allowed.

You can determine this option's status by examining the user_access column in the sys.databases catalog view or the UserAccess property of the DATABASEPROPERTYEX function.

**\<delayed_durability_option> ::=**

Controls whether transactions commit fully durable or delayed durable.

DISABLED
All transactions following SET DISABLED are fully durable. Any durability options set in an atomic block or commit statement are ignored.

ALLOWED
All transactions following SET ALLOWED are either fully durable or delayed durable, depending upon the durability option set in the atomic block or commit statement.

FORCED
All transactions following SET FORCED are delayed durable. Any durability options set in an atomic block or commit statement are ignored.

**\<PARAMETERIZATION_option> ::=**

Controls the parameterization option.

PARAMETERIZATION { SIMPLE | FORCED }
SIMPLE
Queries are parameterized based on the default behavior of the database.

FORCED
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] parameterizes all queries in the database.

The current setting of this option can be determined by examining the is_parameterization_forced column in the sys.databases catalog view.

**\<query_store_options> ::=**

ON | OFF | CLEAR [ ALL ]
Controls if the query store is enabled in this database, and also controls removing the contents of the query store.

ON
Enables the query store.

OFF
Disables the query store. This is the default value.

CLEAR
Remove the contents of the query store.

OPERATION_MODE
Describes the operation mode of the query store. Valid values are READ_ONLY and READ_WRITE. In READ_WRITE mode, the query store collects and persists query plan and runtime execution statistics information. In READ_ONLY mode, information can be read from the query store, but new information isn't added. If the maximum allocated space of the query store has been exhausted, the query store will change is operation mode to READ_ONLY.

CLEANUP_POLICY
Describes the data retention policy of the query store. STALE_QUERY_THRESHOLD_DAYS determines the number of days for which the information for a query is kept in the query store. STALE_QUERY_THRESHOLD_DAYS is type **bigint**.

DATA_FLUSH_INTERVAL_SECONDS
Determines the frequency at which data written to the query store is persisted to disk. To optimize for performance, data collected by the query store is asynchronously written to the disk. The frequency at which this asynchronous transfer occurs is configured by using the DATA_FLUSH_INTERVAL_SECONDS argument. DATA_FLUSH_INTERVAL_SECONDS is type **bigint**.

MAX_STORAGE_SIZE_MB
Determines the space allocated to the query store. MAX_STORAGE_SIZE_MB is type **bigint**.

INTERVAL_LENGTH_MINUTES
Determines the time interval at which runtime execution statistics data is aggregated into the query store. To optimize for space usage, the runtime execution statistics in the runtime stats store are aggregated over a fixed time window. This fixed time window is configured by using the INTERVAL_LENGTH_MINUTES argument. INTERVAL_LENGTH_MINUTES is type **bigint**.

SIZE_BASED_CLEANUP_MODE
Controls whether cleanup will be automatically activated when total amount of data gets close to maximum size:

OFF
Size based cleanup won't be automatically activated.

AUTO
Size based cleanup will be automatically activated when size on disk reaches 90% of **max_storage_size_mb**. Size based cleanup removes the least expensive and oldest queries first. It stops at approximately 80% of **max_storage_size_mb**. This is the default configuration value.

SIZE_BASED_CLEANUP_MODE is type **nvarchar**.

QUERY_CAPTURE_MODE
Designates the currently active query capture mode:

ALL
All queries are captured. This is the default configuration value.

AUTO
Capture relevant queries based on execution count and resource consumption.This is the default configuration value for [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)]

NONE
Stop capturing new queries. Query Store will continue to collect compile and runtime statistics for queries that were captured already. Use this configuration with caution since you may miss to capture important queries.

QUERY_CAPTURE_MODE is type **nvarchar**.

MAX_PLANS_PER_QUERY
An integer representing the maximum number of plans maintained for each query. Default is 200.

**\<snapshot_option> ::=**

Determines the transaction isolation level.

ALLOW_SNAPSHOT_ISOLATION { ON | OFF }
ON
Enables Snapshot option at the database level. When it is enabled, DML statements start generating row versions even when no transaction uses Snapshot Isolation. Once this option is enabled, transactions can specify the SNAPSHOT transaction isolation level. When a transaction runs at the SNAPSHOT isolation level, all statements see a snapshot of data as it exists at the start of the transaction. If a transaction running at the SNAPSHOT isolation level accesses data in multiple databases, either ALLOW_SNAPSHOT_ISOLATION must be set to ON in all the databases, or each statement in the transaction must use locking hints on any reference in a FROM clause to a table in a database where ALLOW_SNAPSHOT_ISOLATION is OFF.

OFF
Turns off the Snapshot option at the database level. Transactions can't specify the SNAPSHOT transaction isolation level.

When you set ALLOW_SNAPSHOT_ISOLATION to a new state (from ON to OFF, or from OFF to ON), ALTER DATABASE doesn't return control to the caller until all existing transactions in the database are committed. If the database is already in the state specified in the ALTER DATABASE statement, control is returned to the caller immediately. If the ALTER DATABASE statement doesn't return quickly, use [sys.dm_tran_active_snapshot_database_transactions](../../relational-databases/system-dynamic-management-views/sys-dm-tran-active-snapshot-database-transactions-transact-sql.md) to determine whether there are long-running transactions. If the ALTER DATABASE statement is canceled, the database remains in the state it was in when ALTER DATABASE was started. The [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view indicates the state of snapshot-isolation transactions in the database. If **snapshot_isolation_state_desc** = IN_TRANSITION_TO_ON, ALTER DATABASE ALLOW_SNAPSHOT_ISOLATION OFF will pause six seconds and retry the operation.

You can't change the state of ALLOW_SNAPSHOT_ISOLATION if the database is OFFLINE.

If you set ALLOW_SNAPSHOT_ISOLATION in a READ_ONLY database, the setting will be kept if the database is later set to READ_WRITE.

You can change the ALLOW_SNAPSHOT_ISOLATION settings for the master, model, msdb, and tempdb databases. The setting is kept every time the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is stopped and restarted if you change the setting for tempdb. If you change the setting for model, that setting becomes the default for any new databases that are created, except for tempdb.

The option is ON, by default, for the master and msdb databases.

The current setting of this option can be determined by examining the snapshot_isolation_state column in the sys.databases catalog view.

READ_COMMITTED_SNAPSHOT { ON | OFF }
ON
Enables Read-Committed Snapshot option at the database level. When it's enabled, DML statements start generating row versions even when no transaction uses Snapshot Isolation. Once this option is enabled, the transactions specifying the read committed isolation level use row versioning instead of locking. All statements see a snapshot of data as it exists at the start of the statement when a transaction runs at the read committed isolation level.

OFF
Turns off Read-Committed Snapshot option at the database level. Transactions specifying the READ COMMITTED isolation level use locking.

To set READ_COMMITTED_SNAPSHOT ON or OFF, there must be no active connections to the database except for the connection executing the ALTER DATABASE command. However, the database doesn't have to be in single-user mode. You can't change the state of this option when the database is OFFLINE.

If you set READ_COMMITTED_SNAPSHOT in a READ_ONLY database, the setting will be kept when the database is later set to READ_WRITE.

READ_COMMITTED_SNAPSHOT can't be turned ON for the master, tempdb, or msdb system databases. If you change the setting for model, that setting becomes the default for any new databases created, except for tempdb.

The current setting of this option can be determined by examining the is_read_committed_snapshot_on column in the sys.databases catalog view.

> [!WARNING]
>When a table is created with **DURABILITY = SCHEMA_ONLY**, and **READ_COMMITTED_SNAPSHOT** is subsequently changed using **ALTER DATABASE**, data in the table will be lost.

MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT { ON | OFF }

ON
  When the transaction isolation level is set to any isolation level lower than SNAPSHOT, all interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] operations on memory-optimized tables are run under SNAPSHOT isolation. Examples of isolation levels lower than snapshot are READ COMMITTED or READ UNCOMMITTED. These operations run whether the transaction isolation level is set explicitly at the session level, or the default is used implicitly.

OFF
doesn't elevate the transaction isolation level for interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] operations on memory-optimized tables.

You can't change the state of MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT if the database is OFFLINE.

The option is OFF, by default.

The current setting of this option can be determined by examining the **is_memory_optimized_elevate_to_snapshot_on** column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

**\<sql_option> ::=**

Controls the ANSI compliance options at the database level.

ANSI_NULL_DEFAULT { ON | OFF }
Determines the default value, NULL or NOT NULL, of a column or [CLR user-defined type](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md) for which the nullability isn't explicitly defined in CREATE TABLE or ALTER TABLE statements. Columns that are defined with constraints follow constraint rules whatever this setting may be.

ON
The default value is NULL.

OFF
The default value isn't NULL.

Connection-level settings that are set by using the SET statement override the default database-level setting for ANSI_NULL_DEFAULT. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_NULL_DEFAULT to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_NULL_DFLT_ON](../../t-sql/statements/set-ansi-null-dflt-on-transact-sql.md).

For ANSI compatibility, setting the database option ANSI_NULL_DEFAULT to ON changes the database default to NULL.

You can determine this option's status by examining the is_ansi_null_default_on column in the sys.databases catalog view. You can also determine the status by examining the IsAnsiNullDefault property of the DATABASEPROPERTYEX function.

ANSI_NULLS { ON | OFF }
ON
All comparisons to a null value evaluate to UNKNOWN.

OFF
Comparisons of non-UNICODE values to a null value evaluate to TRUE if both values are NULL.

> [!IMPORTANT]
> In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], ANSI_NULLS will always be ON and any applications that explicitly set the option to OFF will produce an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.

  Connection-level settings that are set by using the SET statement override the default database setting for ANSI_NULLS. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_NULLS to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_NULLS](../../t-sql/statements/set-ansi-nulls-transact-sql.md).

SET ANSI_NULLS also must be set to ON when you create or make changes to indexes on computed columns or indexed views.

You can determine this option's status by examining the is_ansi_nulls_on column in the sys.databases catalog view. You can also determine the status by examining the IsAnsiNullsEnabled property of the DATABASEPROPERTYEX function.

ANSI_PADDING { ON | OFF }
ON
Strings are padded to the same length before conversion. Also padded to the same length before inserting to a **varchar** or **nvarchar** data type.

OFF
Inserts trailing blanks in character values into **varchar** or **nvarchar** columns. Also leaves trailing zeros in binary values that are inserted into **varbinary** columns. Values aren't padded to the length of the column.

When OFF is specified, this setting affects only the definition of new columns.

> [!IMPORTANT]
> In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], ANSI_PADDING will always be ON and any applications that explicitly set the option to OFF will produce an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. We recommend that you always set ANSI_PADDING to ON. ANSI_PADDING must be ON when you create or manipulate indexes on computed columns or indexed views.

**char(_n_)** and **binary(_n_)** columns that allow for nulls are padded to the column length when ANSI_PADDING is set to ON. Trailing blanks and zeros are trimmed when ANSI_PADDING is OFF. **char(_n_)** and **binary(_n_)** columns that don't allow nulls are always padded to the length of the column.

  Connection-level settings that are set by using the SET statement override the default database-level setting for ANSI_PADDING. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_PADDING to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_PADDING](../../t-sql/statements/set-ansi-padding-transact-sql.md).

You can determine this option's status by examining the is_ansi_padding_on column in the sys.databases catalog view. You can also determine the status by examining the IsAnsiPaddingEnabled property of the DATABASEPROPERTYEX function.

ANSI_WARNINGS { ON | OFF }
ON
Errors or warnings are issued when conditions such as divide-by-zero occur. Errors and warnings are also issued when null values appear in aggregate functions.

OFF
No warnings are raised and null values are returned when conditions such as divide-by-zero occur.

SET ANSI_WARNINGS must be set to ON when you create or make changes to indexes on computed columns or indexed views.

  Connection-level settings that are set by using the SET statement override the default database setting for ANSI_WARNINGS. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_WARNINGS to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_WARNINGS](../../t-sql/statements/set-ansi-warnings-transact-sql.md).

You can determine this option's status by examining the is_ansi_warnings_on column in the sys.databases catalog view. You can also determine the status by examining the IsAnsiWarningsEnabled property of the DATABASEPROPERTYEX function.

ARITHABORT { ON | OFF }
ON
A query is ended when an overflow or divide-by-zero error occurs during query execution.

OFF
A warning message is displayed when one of these errors occurs. The query, batch, or transaction continues to process as if no error occurred even if a warning is displayed.

SET ARITHABORT must be set to ON when you create or make changes to indexes on computed columns or indexed views.

  You can determine this option's status by examining the is_arithabort_on column in the sys.databases catalog view. You can also determine the status by examining the IsArithmeticAbortEnabled property of the DATABASEPROPERTYEX function.

COMPATIBILITY_LEVEL = { 140 | 130 | 120 | 110 | 100 }
For more information, see [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

CONCAT_NULL_YIELDS_NULL { ON | OFF }
ON
The result of a concatenation operation is NULL when either operand is NULL. For example, concatenating the character string "This is" and NULL causes the value NULL, instead of the value "This is".

OFF
The null value is treated as an empty character string.

CONCAT_NULL_YIELDS_NULL must be set to ON when you create or make changes to indexes on computed columns or indexed views.

> [!IMPORTANT]
> In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], CONCAT_NULL_YIELDS_NULL will always be ON and any applications that explicitly set the option to OFF will produce an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.

Connection-level settings that are set by using the SET statement override the default database setting for CONCAT_NULL_YIELDS_NULL. By default, ODBC and OLE DB clients issue a connection-level SET statement setting CONCAT_NULL_YIELDS_NULL to ON for the session when connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET CONCAT_NULL_YIELDS_NULL](../../t-sql/statements/set-concat-null-yields-null-transact-sql.md).

You can determine this option's status by examining the is_concat_null_yields_null_on column in the sys.databases catalog view. You can also determine the status by examining the IsNullConcat property of the DATABASEPROPERTYEX function.

QUOTED_IDENTIFIER { ON | OFF }
ON
Double quotation marks can be used to enclose delimited identifiers.

All strings delimited by double quotation marks are interpreted as object identifiers. Quoted identifiers don't have to follow the [!INCLUDE[tsql](../../includes/tsql-md.md)] rules for identifiers. They can be keywords and can include characters not allowed in [!INCLUDE[tsql](../../includes/tsql-md.md)] identifiers. If a single quotation mark (') is part of the literal string, it can be represented by double quotation marks (").

OFF
Identifiers can't be in quotation marks and must follow all [!INCLUDE[tsql](../../includes/tsql-md.md)] rules for identifiers. Literals can be delimited by either single or double quotation marks.

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also allows for identifiers to be delimited by square brackets ([ ]). Bracketed identifiers can always be used, whatever the QUOTED_IDENTIFIER setting is. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).

  When a table is created, the QUOTED IDENTIFIER option is always stored as ON in the metadata of the table. The option is stored even if the option is set to OFF when the table is created.

Connection-level settings that are set by using the SET statement override the default database setting for QUOTED_IDENTIFIER. ODBC and OLE DB clients issue a connection-level SET statement setting QUOTED_IDENTIFIER to ON, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET QUOTED_IDENTIFIER](../../t-sql/statements/set-quoted-identifier-transact-sql.md).

  You can determine this option's status by examining the is_quoted_identifier_on column in the sys.databases catalog view. You can also determine the status by examining the IsQuotedIdentifiersEnabled property of the DATABASEPROPERTYEX function.

NUMERIC_ROUNDABORT { ON | OFF }
ON
An error is generated when loss of precision occurs in an expression.

OFF
Losses of precision don't generate error messages and the result is rounded to the precision of the column or variable storing the result.

NUMERIC_ROUNDABORT must be set to OFF when you create or make changes to indexes on computed columns or indexed views.

You can determine this option's status by examining the is_numeric_roundabort_on column in the sys.databases catalog view. You can also determine the status by examining the IsNumericRoundAbortEnabled property of the DATABASEPROPERTYEX function.

RECURSIVE_TRIGGERS { ON | OFF }
ON
Recursive firing of AFTER triggers is allowed.

OFF
You can determine this option's status by examining the is_recursive_triggers_on column in the sys.databases catalog view. You can also determine the status by examining the IsRecursiveTriggersEnabled property of the DATABASEPROPERTYEX function.

> [!NOTE]
>Only direct recursion is prevented when RECURSIVE_TRIGGERS is set to OFF. To disable indirect recursion, you must also set the nested triggers server option to 0.

You can determine this option's status by examining the is_recursive_triggers_on column in the sys.databases catalog view or the IsRecursiveTriggersEnabled property of the DATABASEPROPERTYEX function.

**\<target_recovery_time_option> ::=**

Specifies the frequency of indirect checkpoints on a per-database basis. Beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] the default value for new databases is 1 minute, which indicates database will use indirect checkpoints. For older versions the default is 0, which indicates that the database will use automatic checkpoints, whose frequency depends on the recovery interval setting of the server instance. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends 1 minute for most systems.

TARGET_RECOVERY_TIME **=**_target_recovery_time_ { SECONDS | MINUTES }
*target_recovery_time*
Specifies the maximum bound on the time to recover the specified database in the event of a crash.

SECONDS
Indicates that *target_recovery_time* is expressed as the number of seconds.

MINUTES
Indicates that *target_recovery_time* is expressed as the number of minutes.

For more information about indirect checkpoints, see [Database Checkpoints](../../relational-databases/logs/database-checkpoints-sql-server.md).

**WITH \<termination> ::=**

Specifies when to roll back incomplete transactions when the database is transitioned from one state to another. If the termination clause is omitted, the ALTER DATABASE statement waits indefinitely if there's any lock on the database. Only one termination clause can be specified, and it follows the SET clauses.

> [!NOTE]
> Not all database options use the WITH \<termination> clause. For more information, see the table under "[Setting Options](#SettingOptions) of the "Remarks" section of this article.

ROLLBACK AFTER *integer* [SECONDS] | ROLLBACK IMMEDIATE
Specifies whether to roll back after the specified number of seconds or immediately.

NO_WAIT
Specifies that the request will fail if the requested database state or option change can't complete immediately. Completing immediately means not waiting for transactions to commit or roll back on their own.

## <a name="SettingOptions"></a> Setting Options

To retrieve current settings for database options, use the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)

After you set a database option, the modification takes effect immediately.

You can change the default values for any one of the database options for all newly created databases. To do so, change the appropriate database option in the model database.

Not all database options use the WITH \<termination> clause or can be specified in combination with other options. The following table lists these options and their option and termination status.

|Options category|Can be specified with other options|Can use the WITH \<termination> clause|
|----------------------|-----------------------------------------|---------------------------------------------|
|\<auto_option>|Yes|No|
|\<change_tracking_option>|Yes|Yes|
|\<cursor_option>|Yes|No|
|\<db_encryption_option>|Yes|No|
|\<db_update_option>|Yes|Yes|
|\<db_user_access_option>|Yes|Yes|
|\<delayed_durability_option>|Yes|Yes|
|\<parameterization_option>|Yes|Yes|
|ALLOW_SNAPSHOT_ISOLATION|No|No|
|READ_COMMITTED_SNAPSHOT|No|Yes|
|MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT|Yes|Yes|
|DATE_CORRELATION_OPTIMIZATION|Yes|Yes|
|\<sql_option>|Yes|No|
|\<target_recovery_time_option>|No|Yes|

## Examples

### A. Setting the database to READ_ONLY

Changing the state of a database or filegroup to READ_ONLY or READ_WRITE requires exclusive access to the database. The following example sets the database to `RESTRICTED_USER` mode to limit access. The example then sets the state of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to `READ_ONLY` and returns access to the database to all users.

```sql
USE master;
GO
ALTER DATABASE AdventureWorks2012
SET RESTRICTED_USER;
GO
ALTER DATABASE AdventureWorks2012
SET READ_ONLY
GO
ALTER DATABASE AdventureWorks2012
SET MULTI_USER;
GO

```

### B. Enabling snapshot isolation on a database

The following example enables the snapshot isolation framework option for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2012;
USE master;
GO
ALTER DATABASE AdventureWorks2012
SET ALLOW_SNAPSHOT_ISOLATION ON;
GO
-- Check the state of the snapshot_isolation_framework
-- in the database.
SELECT name, snapshot_isolation_state,
    snapshot_isolation_state_desc AS description
FROM sys.databases
WHERE name = N'AdventureWorks2012';
GO

```

The result set shows that the snapshot isolation framework is enabled.

|name |snapshot_isolation_state |description|
|-------------------- |------------------------|----------|
|AdventureWorks2012 |1| ON |

### C. Enabling, modifying, and disabling change tracking

The following example enables change tracking for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database and sets the retention period to `2` days.

```sql
ALTER DATABASE AdventureWorks2012
SET CHANGE_TRACKING = ON
(AUTO_CLEANUP = ON, CHANGE_RETENTION = 2 DAYS);
```

The following example shows how to change the retention period to `3` days.

```sql
ALTER DATABASE AdventureWorks2012
SET CHANGE_TRACKING (CHANGE_RETENTION = 3 DAYS);
```

The following example shows how to disable change tracking for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
ALTER DATABASE AdventureWorks2012
SET CHANGE_TRACKING = OFF;
```

### D. Enabling the query store

The following example enables the query store and configures query store parameters.

```sql
ALTER DATABASE AdventureWorks2012
SET QUERY_STORE = ON
(
      OPERATION_MODE = READ_WRITE
    , CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = 90 )
    , DATA_FLUSH_INTERVAL_SECONDS = 900
    , MAX_STORAGE_SIZE_MB = 1024
    , INTERVAL_LENGTH_MINUTES = 60
    );
```

## See Also

- [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
- [ALTER DATABASE Database Mirroring](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md)
- [Statistics](../../relational-databases/statistics/statistics.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md?view=azuresqldb-currentls)
- [Enable and Disable Change Tracking](../../relational-databases/track-changes/enable-and-disable-change-tracking-sql-server.md)
- [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)
- [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md)
- [SET TRANSACTION ISOLATION LEVEL](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md)
- [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [sys.data_spaces](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md)
- [Best Practice with the Query Store](../../relational-databases/performance/best-practice-with-the-query-store.md)

::: moniker-end
::: moniker range="=azuresqldb-mi-current||=sqlallproducts-allversions"

> |||
> |---|---|
> |[SQL Server](alter-database-transact-sql-set-options.md?view=sql-server-2017)|[SQL Database<br />single database/elastic pool](alter-database-transact-sql-set-options.md?view=azuresqldb-current) |**_\* SQL Database<br />managed instance \*_** &nbsp;|

&nbsp;

## Azure SQL Database managed instance

Compatibility levels are `SET` options but are described in [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

> [!NOTE]
> Many database set options can be configured for the current session by using [SET Statements](../../t-sql/statements/set-statements-transact-sql.md) and are often configured by applications when they connect. Session level set options override the **ALTER DATABASE SET** values. The database options described below are values that can be set for sessions that don't explicitly provide other set option values.

## Syntax

```
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
  AUTOMATIC_TUNING ( FORCE_LAST_GOOD_PLAN = { ON | OFF } )
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
  | SIZE_BASED_CLEANUP_MODE = [ AUTO | OFF ]
  | QUERY_CAPTURE_MODE = [ ALL | AUTO | NONE ]
  | MAX_PLANS_PER_QUERY = number
}

<snapshot_option> ::=
{
    ALLOW_SNAPSHOT_ISOLATION { ON | OFF }
  | READ_COMMITTED_SNAPSHOT {ON | OFF }
  | MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT {ON | OFF }
}
<sql_option> ::=
{
    ANSI_NULL_DEFAULT { ON | OFF }
  | ANSI_NULLS { ON | OFF }
  | ANSI_PADDING { ON | OFF }
  | ANSI_WARNINGS { ON | OFF }
  | ARITHABORT { ON | OFF }
  | COMPATIBILITY_LEVEL = { 140 | 130 | 120 | 110 | 100 }
  | CONCAT_NULL_YIELDS_NULL { ON | OFF }
  | NUMERIC_ROUNDABORT { ON | OFF }
  | QUOTED_IDENTIFIER { ON | OFF }
  | RECURSIVE_TRIGGERS { ON | OFF }
}

<temporal_history_retention>::=TEMPORAL_HISTORY_RETENTION { ON | OFF }
```

## Arguments

*database_name*
Is the name of the database to be modified.

CURRENT
`CURRENT` runs the action in the current database. `CURRENT` isn't supported for all options in all contexts. If `CURRENT` fails, provide the database name.

**\<auto_option> ::=**

Controls automatic options.
<a name="auto_create_statistics"></a> AUTO_CREATE_STATISTICS { ON | OFF }
ON
The query optimizer creates statistics on single columns in query predicates, as necessary, to improve query plans and query performance. These single-column statistics are created when the query optimizer compiles queries. The single-column statistics are created only on columns that are not already the first column of an existing statistics object.

The default is ON. We recommend that you use the default setting for most databases.

OFF
The query optimizer doesn't create statistics on single columns in query predicates when it is compiling queries. Setting this option to OFF can cause suboptimal query plans and degraded query performance.

You can determine this option's status by examining the is_auto_create_stats_on column in the sys.databases catalog view. You can also determine the status by examining the IsAutoCreateStatistics property of the DATABASEPROPERTYEX function.

For more information, see the section "Using the Database-Wide Statistics Options" in [Statistics](../../relational-databases/statistics/statistics.md).

INCREMENTAL = ON | OFF
Set AUTO_CREATE_STATISTICS to ON, and set INCREMENTAL to ON. This setting creates automatically created stats as incremental whenever incremental stats are supported. The default value is OFF. For more information, see [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md).

<a name="auto_shrink"></a> AUTO_SHRINK { ON | OFF }
ON
The database files are candidates for periodic shrinking.

Both data file and log files can be automatically shrunk. AUTO_SHRINK reduces the size of the transaction log only if you set the database to SIMPLE recovery model or if you back up the log. When set to OFF, the database files aren't automatically shrunk during periodic checks for unused space.

The AUTO_SHRINK option causes files to be shrunk when more than 25 percent of the file contains unused space. The option causes the file to shrink to one of two sizes. It shrinks to whichever is larger:

- the size where 25 percent of the file is unused space
- the size of the file when it was created

You can't shrink a read-only database.

OFF
The database files are not automatically shrunk during periodic checks for unused space.

You can determine this option's status by examining the is_auto_shrink_on column in the sys.databases catalog view. You can also determine the status by examining the IsAutoShrink property of the DATABASEPROPERTYEX function.

> [!NOTE]
> The AUTO_SHRINK option isn't available in a Contained Database.

<a name="auto_update_statistics"></a> AUTO_UPDATE_STATISTICS { ON | OFF }
ON
Specifies that the query optimizer updates statistics when they're used by a query and when they might be out-of-date. Statistics become out-of-date after insert, update, delete, or merge operations change the data distribution in the table or indexed view. The query optimizer determines when statistics might be out-of-date by counting the number of data modifications since the last statistics update and comparing the number of modifications to a threshold. The threshold is based on the number of rows in the table or indexed view.

The query optimizer checks for out-of-date statistics before it compiles a query and it runs a cached query plan. The query optimizer uses the columns, tables, and indexed views in the query predicate to determine which statistics might be out-of-date. The query optimizer determines this information before it compiles a query. Before executing a cached query plan, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] verifies that the query plan references up-to-date statistics.

The AUTO_UPDATE_STATISTICS option applies to statistics created for indexes, single-columns in query predicates, and statistics that are created by using the CREATE STATISTICS statement. This option also applies to filtered statistics.

The default is ON. We recommend that you use the default setting for most databases.

Use the AUTO_UPDATE_STATISTICS_ASYNC option to specify whether the statistics are updated synchronously or asynchronously.

OFF
Specifies that the query optimizer doesn't update statistics when they're used by a query. The query optimizer also doesn't update statistics when they might be out-of-date. Setting this option to OFF can cause suboptimal query plans and degraded query performance.

You can determine this option's status by examining the is_auto_update_stats_on column in the sys.databases catalog view. You can also determine the status by examining the IsAutoUpdateStatistics property of the DATABASEPROPERTYEX function.

For more information, see the section "Using the Database-Wide Statistics Options" in [Statistics](../../relational-databases/statistics/statistics.md).

<a name="auto_update_statistics_async"></a> AUTO_UPDATE_STATISTICS_ASYNC { ON | OFF }
ON
Specifies that statistics updates for the AUTO_UPDATE_STATISTICS option are asynchronous. The query optimizer doesn't wait for statistics updates to complete before it compiles queries.

Setting this option to ON has no effect unless AUTO_UPDATE_STATISTICS is set to ON.

By default, the AUTO_UPDATE_STATISTICS_ASYNC option is set to OFF, and the query optimizer updates statistics synchronously.

OFF
Specifies that statistics updates for the AUTO_UPDATE_STATISTICS option are synchronous. The query optimizer waits for statistics updates to complete before it compiles queries.

Setting this option to OFF has no effect unless AUTO_UPDATE_STATISTICS is set to ON.

You can determine this option's status by examining the is_auto_update_stats_async_on column in the sys.databases catalog view.

For more information that describes when to use synchronous or asynchronous statistics updates, see the section "Using the Database-Wide Statistics Options" in [Statistics](../../relational-databases/statistics/statistics.md).

<a name="auto_tuning"></a> **\<automatic_tuning_option> ::=**
**Applies to**: [!INCLUDE[sssqlv14-md](../../includes/sssqlv14-md.md)].

Enables or disables `FORCE_LAST_GOOD_PLAN` [automatic tuning](../../relational-databases/automatic-tuning/automatic-tuning.md) option.

FORCE_LAST_GOOD_PLAN = { ON | OFF }
ON
The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] automatically forces the last known good plan on the [!INCLUDE[tsql-md](../../includes/tsql-md.md)] queries where new SQL plan causes performance regressions. The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] continuously monitors query performance of the [!INCLUDE[tsql-md](../../includes/tsql-md.md)] query with the forced plan. If there are performance gains, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] will keep using last known good plan. If performance gains are not detected, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] will produce a new SQL plan. The statement will fail if Query Store isn't enabled or if it isn't in *Read-Write* mode.
OFF
The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] reports potential query performance regressions caused by SQL plan changes in [sys.dm_db_tuning_recommendations](../../relational-databases/system-dynamic-management-views/sys-dm-db-tuning-recommendations-transact-sql.md) view. However, these recommendations are not automatically applied. User can monitor active recommendations and fix identified problems by applying [!INCLUDE[tsql-md](../../includes/tsql-md.md)] scripts that are shown in the view. This is the default value.

**\<change_tracking_option> ::=**

Controls change tracking options. You can enable change tracking, set options, change options, and disable change tracking. For examples, see the Examples section later in this article.

ON
Enables change tracking for the database. When you enable change tracking, you can also set the AUTO CLEANUP and CHANGE RETENTION options.

AUTO_CLEANUP = { ON | OFF }
ON
Change tracking information is automatically removed after the specified retention period.

OFF
Change tracking data isn't removed from the database.

CHANGE_RETENTION =*retention_period* { DAYS | HOURS | MINUTES }
Specifies the minimum period for keeping change tracking information in the database. Data is removed only when the AUTO_CLEANUP value is ON.

*retention_period* is an integer that specifies the numerical component of the retention period.

The default retention period is 2 days. The minimum retention period is 1 minute. The default retention type is DAYS.

OFF
Disables change tracking for the database. Disable change tracking on all tables before you disable change tracking off the database.

**\<cursor_option> ::=**

Controls cursor options.

CURSOR_CLOSE_ON_COMMIT { ON | OFF }
ON
Any cursors open when you commit or roll back a transaction are closed.

OFF
Cursors remain open when a transaction is committed; rolling back a transaction closes any cursors except those defined as INSENSITIVE or STATIC.

Connection-level settings that are set by using the SET statement override the default database setting for CURSOR_CLOSE_ON_COMMIT. ODBC and OLE DB clients issue a connection-level SET statement setting CURSOR_CLOSE_ON_COMMIT to OFF for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET CURSOR_CLOSE_ON_COMMIT](../../t-sql/statements/set-cursor-close-on-commit-transact-sql.md).

You can determine this option's status by examining the is_cursor_close_on_commit_on column in the sys.databases catalog view or the IsCloseCursorsOnCommitEnabled property of the DATABASEPROPERTYEX function. The cursor is implicitly deallocated only at disconnect. For more information, see [DECLARE CURSOR](../../t-sql/language-elements/declare-cursor-transact-sql.md).

**\<db_encryption_option> ::=**

Controls the database encryption state.

ENCRYPTION {ON | OFF}
Sets the database to be encrypted (ON) or not encrypted (OFF). For more information about database encryption, see [Transparent Data Encryption](../../relational-databases/security/encryption/transparent-data-encryption.md), and [Transparent Data Encryption with Azure SQL Database](../../relational-databases/security/encryption/transparent-data-encryption-azure-sql.md).

When encryption is enabled at the database level, all filegroups will be encrypted. Any new filegroups will inherit the encrypted property. If any filegroups in the database are set to **READ ONLY**, the database encryption operation will fail.

You can see the encryption state of the database by using the [sys.dm_database_encryption_keys](../../relational-databases/system-dynamic-management-views/sys-dm-database-encryption-keys-transact-sql.md) dynamic management view.

**\<db_update_option> ::=**

Controls whether updates are allowed on the database.

READ_ONLY
Users can read data from the database but not modify it.

> [!NOTE]
>To improve query performance, update statistics before setting a database to READ_ONLY. If additional statistics are needed after a database is set to READ_ONLY, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] will create statistics in tempdb. For more information about statistics for a read-only database, see [Statistics](../../relational-databases/statistics/statistics.md).

READ_WRITE
The database is available for read and write operations.

To change this state, you must have exclusive access to the database.

**\<db_user_access_option> ::=**

Controls user access to the database.

RESTRICTED_USER
RESTRICTED_USER allows for only members of the db_owner fixed database role and dbcreator and sysadmin fixed server roles to connect to the database, but doesn't limit their number. All connections to the database are disconnected in the timeframe specified by the termination clause of the ALTER DATABASE statement. After the database has transitioned to the RESTRICTED_USER state, connection attempts by unqualified users are refused. **RESTRICTED_USER** can't be modified with SQL Database managed instance.

MULTI_USER
All users that have the appropriate permissions to connect to the database are allowed.

You can determine this option's status by examining the user_access column in the sys.databases catalog view or the UserAccess property of the DATABASEPROPERTYEX function.

**\<delayed_durability_option> ::=**

Controls whether transactions commit fully durable or delayed durable.

DISABLED
All transactions following SET DISABLED are fully durable. Any durability options set in an atomic block or commit statement are ignored.

ALLOWED
All transactions following SET ALLOWED are either fully durable or delayed durable, depending upon the durability option set in the atomic block or commit statement.

FORCED
All transactions following SET FORCED are delayed durable. Any durability options set in an atomic block or commit statement are ignored.

**\<PARAMETERIZATION_option> ::=**

Controls the parameterization option.

PARAMETERIZATION { SIMPLE | FORCED }
SIMPLE
Queries are parameterized based on the default behavior of the database.

FORCED
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] parameterizes all queries in the database.

The current setting of this option can be determined by examining the is_parameterization_forced column in the sys.databases catalog view.

**\<query_store_options> ::=**

ON | OFF | CLEAR [ ALL ]
Controls if the query store is enabled in this database, and also controls removing the contents of the query store.

ON
Enables the query store.

OFF
Disables the query store. This is the default value.

CLEAR
Remove the contents of the query store.

OPERATION_MODE
Describes the operation mode of the query store. Valid values are READ_ONLY and READ_WRITE. In READ_WRITE mode, the query store collects and persists query plan and runtime execution statistics information. In READ_ONLY mode, information can be read from the query store, but new information isn't added. If the maximum allocated space of the query store has been exhausted, the query store will change is operation mode to READ_ONLY.

CLEANUP_POLICY
Describes the data retention policy of the query store. STALE_QUERY_THRESHOLD_DAYS determines the number of days for which the information for a query is kept in the query store. STALE_QUERY_THRESHOLD_DAYS is type **bigint**.

DATA_FLUSH_INTERVAL_SECONDS
Determines the frequency at which data written to the query store is persisted to disk. To optimize for performance, data collected by the query store is asynchronously written to the disk. The frequency at which this asynchronous transfer occurs is configured by using the DATA_FLUSH_INTERVAL_SECONDS argument. DATA_FLUSH_INTERVAL_SECONDS is type **bigint**.

MAX_STORAGE_SIZE_MB
Determines the space allocated to the query store. MAX_STORAGE_SIZE_MB is type **bigint**.

INTERVAL_LENGTH_MINUTES
Determines the time interval at which runtime execution statistics data is aggregated into the query store. To optimize for space usage, the runtime execution statistics in the runtime stats store are aggregated over a fixed time window. This fixed time window is configured by using the INTERVAL_LENGTH_MINUTES argument. INTERVAL_LENGTH_MINUTES is type **bigint**.

SIZE_BASED_CLEANUP_MODE
Controls whether cleanup will be automatically activated when total amount of data gets close to maximum size:

OFF
Size based cleanup won't be automatically activated.

AUTO
Size based cleanup will be automatically activated when size on disk reaches 90% of **max_storage_size_mb**. Size based cleanup removes the least expensive and oldest queries first. It stops at approximately 80% of **max_storage_size_mb**. This is the default configuration value.

SIZE_BASED_CLEANUP_MODE is type **nvarchar**.

QUERY_CAPTURE_MODE
Designates the currently active query capture mode.

ALL
All queries are captured. This is the default configuration value.

AUTO
Capture relevant queries based on execution count and resource consumption.This is the default configuration value for [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)]

NONE
Stop capturing new queries. Query Store will continue to collect compile and runtime statistics for queries that were captured already. Use this configuration with caution since you may miss to capture important queries.

QUERY_CAPTURE_MODE is type **nvarchar**.

MAX_PLANS_PER_QUERY
An integer representing the maximum number of plans maintained for each query. Default is 200.

**\<snapshot_option> ::=**

Determines the transaction isolation level.

ALLOW_SNAPSHOT_ISOLATION { ON | OFF }
ON
Enables Snapshot option at the database level. When it is enabled, DML statements start generating row versions even when no transaction uses Snapshot Isolation. Once this option is enabled, transactions can specify the SNAPSHOT transaction isolation level. When a transaction runs at the SNAPSHOT isolation level, all statements see a snapshot of data as it exists at the start of the transaction. If a transaction running at the SNAPSHOT isolation level accesses data in multiple databases, either ALLOW_SNAPSHOT_ISOLATION must be set to ON in all the databases, or each statement in the transaction must use locking hints on any reference in a FROM clause to a table in a database where ALLOW_SNAPSHOT_ISOLATION is OFF.

OFF
Turns off the Snapshot option at the database level. Transactions can't specify the SNAPSHOT transaction isolation level.

When you set ALLOW_SNAPSHOT_ISOLATION to a new state (from ON to OFF, or from OFF to ON), ALTER DATABASE doesn't return control to the caller until all existing transactions in the database are committed. If the database is already in the state specified in the ALTER DATABASE statement, control is returned to the caller immediately. If the ALTER DATABASE statement doesn't return quickly, use [sys.dm_tran_active_snapshot_database_transactions](../../relational-databases/system-dynamic-management-views/sys-dm-tran-active-snapshot-database-transactions-transact-sql.md) to determine whether there are long-running transactions. If the ALTER DATABASE statement is canceled, the database remains in the state it was in when ALTER DATABASE was started. The [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view indicates the state of snapshot-isolation transactions in the database. If **snapshot_isolation_state_desc** = IN_TRANSITION_TO_ON, ALTER DATABASE ALLOW_SNAPSHOT_ISOLATION OFF will pause six seconds and retry the operation.

You can't change the state of ALLOW_SNAPSHOT_ISOLATION if the database is OFFLINE.

If you set ALLOW_SNAPSHOT_ISOLATION in a READ_ONLY database, the setting will be kept if the database is later set to READ_WRITE.

You can change the ALLOW_SNAPSHOT_ISOLATION settings for the master, model, msdb, and tempdb databases. The setting is kept every time the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is stopped and restarted if you change the setting for tempdb. If you change the setting for model, that setting becomes the default for any new databases that are created, except for tempdb.

The option is ON, by default, for the master and msdb databases.

The current setting of this option can be determined by examining the snapshot_isolation_state column in the sys.databases catalog view.

READ_COMMITTED_SNAPSHOT { ON | OFF }
ON
Enables Read-Committed Snapshot option at the database level. When it's enabled, DML statements start generating row versions even when no transaction uses Snapshot Isolation. Once this option is enabled, the transactions specifying the read committed isolation level use row versioning instead of locking. All statements see a snapshot of data as it exists at the start of the statement when a transaction runs at the read committed isolation level.

OFF
Turns off Read-Committed Snapshot option at the database level. Transactions specifying the READ COMMITTED isolation level use locking.

To set READ_COMMITTED_SNAPSHOT ON or OFF, there must be no active connections to the database except for the connection executing the ALTER DATABASE command. However, the database doesn't have to be in single-user mode. You can't change the state of this option when the database is OFFLINE.

If you set READ_COMMITTED_SNAPSHOT in a READ_ONLY database, the setting will be kept when the database is later set to READ_WRITE.

READ_COMMITTED_SNAPSHOT can't be turned ON for the master, tempdb, or msdb system databases. If you change the setting for model, that setting becomes the default for any new databases created, except for tempdb.

The current setting of this option can be determined by examining the is_read_committed_snapshot_on column in the sys.databases catalog view.

> [!WARNING]
>When a table is created with **DURABILITY = SCHEMA_ONLY**, and **READ_COMMITTED_SNAPSHOT** is subsequently changed using **ALTER DATABASE**, data in the table will be lost.

MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT { ON | OFF }

ON
  When the transaction isolation level is set to any isolation level lower than SNAPSHOT, all interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] operations on memory-optimized tables are run under SNAPSHOT isolation. Examples of isolation levels lower than snapshot are READ COMMITTED or READ UNCOMMITTED. These operations run whether the transaction isolation level is set explicitly at the session level, or the default is used implicitly.

OFF
doesn't elevate the transaction isolation level for interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] operations on memory-optimized tables.

You can't change the state of MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT if the database is OFFLINE.

The option is OFF, by default.

The current setting of this option can be determined by examining the **is_memory_optimized_elevate_to_snapshot_on** column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

**\<sql_option> ::=**

Controls the ANSI compliance options at the database level.

ANSI_NULL_DEFAULT { ON | OFF }
Determines the default value, NULL or NOT NULL, of a column or [CLR user-defined type](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md) for which the nullability isn't explicitly defined in CREATE TABLE or ALTER TABLE statements. Columns that are defined with constraints follow constraint rules whatever this setting may be.

ON
The default value is NULL.

OFF
The default value isn't NULL.

Connection-level settings that are set by using the SET statement override the default database-level setting for ANSI_NULL_DEFAULT. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_NULL_DEFAULT to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_NULL_DFLT_ON](../../t-sql/statements/set-ansi-null-dflt-on-transact-sql.md).

For ANSI compatibility, setting the database option ANSI_NULL_DEFAULT to ON changes the database default to NULL.

You can determine this option's status by examining the is_ansi_null_default_on column in the sys.databases catalog view. You can also determine the status by examining the IsAnsiNullDefault property of the DATABASEPROPERTYEX function.

ANSI_NULLS { ON | OFF }
ON
All comparisons to a null value evaluate to UNKNOWN.

OFF
Comparisons of non-UNICODE values to a null value evaluate to TRUE if both values are NULL.

> [!IMPORTANT]
> In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], ANSI_NULLS will always be ON and any applications that explicitly set the option to OFF will produce an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.

  Connection-level settings that are set by using the SET statement override the default database setting for ANSI_NULLS. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_NULLS to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_NULLS](../../t-sql/statements/set-ansi-nulls-transact-sql.md).

SET ANSI_NULLS also must be set to ON when you create or make changes to indexes on computed columns or indexed views.

You can determine this option's status by examining the is_ansi_nulls_on column in the sys.databases catalog view. You can also determine the status by examining the IsAnsiNullsEnabled property of the DATABASEPROPERTYEX function.

ANSI_PADDING { ON | OFF }
ON
Strings are padded to the same length before conversion. Also padded to the same length before inserting to a **varchar** or **nvarchar** data type.

OFF
Inserts trailing blanks in character values into **varchar** or **nvarchar** columns. Also leaves trailing zeros in binary values that are inserted into **varbinary** columns. Values aren't padded to the length of the column.

When OFF is specified, this setting affects only the definition of new columns.

> [!IMPORTANT]
> In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], ANSI_PADDING will always be ON and any applications that explicitly set the option to OFF will produce an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. We recommend that you always set ANSI_PADDING to ON. ANSI_PADDING must be ON when you create or manipulate indexes on computed columns or indexed views.

**char(_n_)** and **binary(_n_)** columns that allow for nulls are padded to the column length when ANSI_PADDING is set to ON. Trailing blanks and zeros are trimmed when ANSI_PADDING is OFF. **char(_n_)** and **binary(_n_)** columns that don't allow nulls are always padded to the length of the column.

  Connection-level settings that are set by using the SET statement override the default database-level setting for ANSI_PADDING. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_PADDING to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_PADDING](../../t-sql/statements/set-ansi-padding-transact-sql.md).

You can determine this option's status by examining the is_ansi_padding_on column in the sys.databases catalog view. You can also determine the status by examining the IsAnsiPaddingEnabled property of the DATABASEPROPERTYEX function.

ANSI_WARNINGS { ON | OFF }
ON
Errors or warnings are issued when conditions such as divide-by-zero occur. Errors and warnings are also issued when null values appear in aggregate functions.

OFF
No warnings are raised and null values are returned when conditions such as divide-by-zero occur.

SET ANSI_WARNINGS must be set to ON when you create or make changes to indexes on computed columns or indexed views.

  Connection-level settings that are set by using the SET statement override the default database setting for ANSI_WARNINGS. ODBC and OLE DB clients issue a connection-level SET statement setting ANSI_WARNINGS to ON for the session, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET ANSI_WARNINGS](../../t-sql/statements/set-ansi-warnings-transact-sql.md).

You can determine this option's status by examining the is_ansi_warnings_on column in the sys.databases catalog view. You can also determine the status by examining the IsAnsiWarningsEnabled property of the DATABASEPROPERTYEX function.

ARITHABORT { ON | OFF }
ON
A query is ended when an overflow or divide-by-zero error occurs during query execution.

OFF
A warning message is displayed when one of these errors occurs. The query, batch, or transaction continues to process as if no error occurred even if a warning is displayed.

SET ARITHABORT must be set to ON when you create or make changes to indexes on computed columns or indexed views.

  You can determine this option's status by examining the is_arithabort_on column in the sys.databases catalog view. You can also determine the status by examining the IsArithmeticAbortEnabled property of the DATABASEPROPERTYEX function.

COMPATIBILITY_LEVEL = { 140 | 130 | 120 | 110 | 100 }
For more information, see [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

CONCAT_NULL_YIELDS_NULL { ON | OFF }
ON
The result of a concatenation operation is NULL when either operand is NULL. For example, concatenating the character string "This is" and NULL causes the value NULL, instead of the value "This is".

OFF
The null value is treated as an empty character string.

CONCAT_NULL_YIELDS_NULL must be set to ON when you create or make changes to indexes on computed columns or indexed views.

> [!IMPORTANT]
> In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], CONCAT_NULL_YIELDS_NULL will always be ON and any applications that explicitly set the option to OFF will produce an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.

Connection-level settings that are set by using the SET statement override the default database setting for CONCAT_NULL_YIELDS_NULL. By default, ODBC and OLE DB clients issue a connection-level SET statement setting CONCAT_NULL_YIELDS_NULL to ON for the session when connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET CONCAT_NULL_YIELDS_NULL](../../t-sql/statements/set-concat-null-yields-null-transact-sql.md).

You can determine this option's status by examining the is_concat_null_yields_null_on column in the sys.databases catalog view. You can also determine the status by examining the IsNullConcat property of the DATABASEPROPERTYEX function.

QUOTED_IDENTIFIER { ON | OFF }
ON
Double quotation marks can be used to enclose delimited identifiers.

All strings delimited by double quotation marks are interpreted as object identifiers. Quoted identifiers don't have to follow the [!INCLUDE[tsql](../../includes/tsql-md.md)] rules for identifiers. They can be keywords and can include characters not allowed in [!INCLUDE[tsql](../../includes/tsql-md.md)] identifiers. If a single quotation mark (') is part of the literal string, it can be represented by double quotation marks (").

OFF
Identifiers can't be in quotation marks and must follow all [!INCLUDE[tsql](../../includes/tsql-md.md)] rules for identifiers. Literals can be delimited by either single or double quotation marks.

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also allows for identifiers to be delimited by square brackets ([ ]). Bracketed identifiers can always be used, whatever the QUOTED_IDENTIFIER setting is. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).

  When a table is created, the QUOTED IDENTIFIER option is always stored as ON in the metadata of the table. The option is stored even if the option is set to OFF when the table is created.

Connection-level settings that are set by using the SET statement override the default database setting for QUOTED_IDENTIFIER. ODBC and OLE DB clients issue a connection-level SET statement setting QUOTED_IDENTIFIER to ON, by default. The clients run the statement when you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SET QUOTED_IDENTIFIER](../../t-sql/statements/set-quoted-identifier-transact-sql.md).

  You can determine this option's status by examining the is_quoted_identifier_on column in the sys.databases catalog view. You can also determine the status by examining the IsQuotedIdentifiersEnabled property of the DATABASEPROPERTYEX function.

NUMERIC_ROUNDABORT { ON | OFF }
ON
An error is generated when loss of precision occurs in an expression.

OFF
Losses of precision don't generate error messages and the result is rounded to the precision of the column or variable storing the result.

NUMERIC_ROUNDABORT must be set to OFF when you create or make changes to indexes on computed columns or indexed views.

You can determine this option's status by examining the is_numeric_roundabort_on column in the sys.databases catalog view. You can also determine the status by examining the IsNumericRoundAbortEnabled property of the DATABASEPROPERTYEX function.

RECURSIVE_TRIGGERS { ON | OFF }
ON
Recursive firing of AFTER triggers is allowed.

OFF
You can determine this option's status by examining the is_recursive_triggers_on column in the sys.databases catalog view. You can also determine the status by examining the IsRecursiveTriggersEnabled property of the DATABASEPROPERTYEX function.

> [!NOTE]
> Only direct recursion is prevented when RECURSIVE_TRIGGERS is set to OFF. To disable indirect recursion, you must also set the nested triggers server option to 0.

You can determine this option's status by examining the is_recursive_triggers_on column in the sys.databases catalog view or the IsRecursiveTriggersEnabled property of the DATABASEPROPERTYEX function.

**\<target_recovery_time_option> ::=**

Specifies the frequency of indirect checkpoints on a per-database basis. Beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] the default value for new databases is 1 minute, which indicates database will use indirect checkpoints. For older versions the default is 0, which indicates that the database will use automatic checkpoints, whose frequency depends on the recovery interval setting of the server instance. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends 1 minute for most systems.

TARGET_RECOVERY_TIME **=**_target_recovery_time_ { SECONDS | MINUTES }
*target_recovery_time*
Specifies the maximum bound on the time to recover the specified database in the event of a crash.

SECONDS
Indicates that *target_recovery_time* is expressed as the number of seconds.

MINUTES
Indicates that *target_recovery_time* is expressed as the number of minutes.

For more information about indirect checkpoints, see [Database Checkpoints](../../relational-databases/logs/database-checkpoints-sql-server.md).

ROLLBACK AFTER *integer* [SECONDS] | ROLLBACK IMMEDIATE
Specifies whether to roll back after the specified number of seconds or immediately.

NO_WAIT
Specifies that the request will fail if the requested database state or option change can't complete immediately. Completing immediately means not waiting for transactions to commit or roll back on their own.

## <a name="SettingOptions"></a> Setting Options

To retrieve current settings for database options, use the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)

After you set a database option, the modification takes effect immediately.

You can change the default values for any one of the database options for all newly created databases. To do so, change the appropriate database option in the model database.

## Examples

### A. Setting the database to READ_ONLY

Changing the state of a database or filegroup to READ_ONLY or READ_WRITE requires exclusive access to the database. The following example sets the database to `RESTRICTED_USER` mode to restricted access. The example then sets the state of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to `READ_ONLY` and returns access to the database to all users.

```sql
USE master;
GO
ALTER DATABASE AdventureWorks2012
SET RESTRICTED_USER;
GO
ALTER DATABASE AdventureWorks2012
SET READ_ONLY
GO
ALTER DATABASE AdventureWorks2012
SET MULTI_USER;
GO

```

### B. Enabling snapshot isolation on a database

The following example enables the snapshot isolation framework option for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2012;
USE master;
GO
ALTER DATABASE AdventureWorks2012
SET ALLOW_SNAPSHOT_ISOLATION ON;
GO
-- Check the state of the snapshot_isolation_framework
-- in the database.
SELECT name, snapshot_isolation_state,
    snapshot_isolation_state_desc AS description
FROM sys.databases
WHERE name = N'AdventureWorks2012';
GO

```

The result set shows that the snapshot isolation framework is enabled.

|name |snapshot_isolation_state |description|
|-------------------- |------------------------|----------|
|AdventureWorks2012 |1| ON |

### C. Enabling, modifying, and disabling change tracking

The following example enables change tracking for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database and sets the retention period to `2` days.

```sql
ALTER DATABASE AdventureWorks2012
SET CHANGE_TRACKING = ON
(AUTO_CLEANUP = ON, CHANGE_RETENTION = 2 DAYS);
```

The following example shows how to change the retention period to `3` days.

```sql
ALTER DATABASE AdventureWorks2012
SET CHANGE_TRACKING (CHANGE_RETENTION = 3 DAYS);
```

The following example shows how to disable change tracking for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
ALTER DATABASE AdventureWorks2012
SET CHANGE_TRACKING = OFF;
```

### D. Enabling the query store

The following example enables the query store and configures query store parameters.

```sql
ALTER DATABASE AdventureWorks2012
SET QUERY_STORE = ON 
  (  
      OPERATION_MODE = READ_WRITE
    , CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = 90 )
    , DATA_FLUSH_INTERVAL_SECONDS = 900
    , MAX_STORAGE_SIZE_MB = 1024
    , INTERVAL_LENGTH_MINUTES = 60
    );
```

## See Also

- [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
- [ALTER DATABASE Database Mirroring](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md)
- [Statistics](../../relational-databases/statistics/statistics.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md?view=azuresqldb-mi-current)
- [Enable and Disable Change Tracking](../../relational-databases/track-changes/enable-and-disable-change-tracking-sql-server.md)
- [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)
- [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md)
- [SET TRANSACTION ISOLATION LEVEL](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md)
- [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [sys.data_spaces](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md)
- [Best Practice with the Query Store](../../relational-databases/performance/best-practice-with-the-query-store.md)

::: moniker-end
