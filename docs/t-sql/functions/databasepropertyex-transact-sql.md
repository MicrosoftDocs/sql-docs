---
title: DATABASEPROPERTYEX (Transact-SQL)
description: DATABASEPROPERTYEX returns the current setting of the specified database option or property.
author: markingmyname
ms.author: maghan
ms.reviewer: wiassaf, randolphwest
ms.date: 03/11/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "DATABASEPROPERTYEX"
  - "DATABASEPROPERTYEX_TSQL"
helpviewer_keywords:
  - "DATABASEPROPERTYEX function"
  - "displaying database properties"
  - "database properties [SQL Server]"
dev_langs:
  - TSQL
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# DATABASEPROPERTYEX (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

For a specified database in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the `DATABASEPROPERTYEX` function returns the current setting of the specified database option or property.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DATABASEPROPERTYEX ( database , property )
```

## Arguments

#### *database*

An expression specifying the name of the database for which `DATABASEPROPERTYEX` returns the named property information. *database* has an **nvarchar(128)** data type.

For [!INCLUDE [ssSDS](../../includes/sssds-md.md)], `DATABASEPROPERTYEX` requires the name of the current database. It returns `NULL` for all properties if given a different database name.

#### *property*

An expression specifying the name of the database property to return. *property* has a **varchar(128)** data type, and supports one of the values in this table:

> [!NOTE]  
> If the database hasn't yet started, calls to `DATABASEPROPERTYEX` return `NULL` if `DATABASEPROPERTYEX` retrieves those values by direct database access, instead of retrieval from metadata. A database with `AUTO_CLOSE` set to `ON`, or otherwise offline, is defined as 'not started.'

| Property | Description | Value returned |
| --- | --- | --- |
| `Collation`<br /><br />Data type: **nvarchar(128)** | Default collation name for the database. | Collation name. If `NULL`, the database isn't started. |
| `ComparisonStyle`<br /><br />Data type: **int** | The Windows comparison style of the collation. Use the following style values to build a bitmap for the finished `ComparisonStyle` value:<br /><br />- `1`: Ignore case<br />- `2`: Ignore accent<br />- `65536`: Ignore kana<br />- `131072`: Ignore width<br /><br />For example, the default of `196609` is the result of combining the *ignore case*, *ignore kana*, and *ignore width* options. | Returns the comparison style.<br /><br />Returns `0` for all binary collations. |
| `Edition`<br /><br />Data type: **nvarchar(64)** | The database edition or service tier. | - `General Purpose`<br />- `Business Critical`<br />- `Basic`<br />- `Standard`<br />- `Premium`<br />- `System` (for `master` database)<br />- `FabricSQLDB`: [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]<br />- `NULL`: Database isn't started.<br /><br />**Applies to**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]. |
| `IsAnsiNullDefault`<br /><br />Data type: **int** | Database follows ISO rules for allowing `NULL` values. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsAnsiNullsEnabled`<br /><br />Data type: **int** | All comparisons to a `NULL` evaluate to unknown. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsAnsiPaddingEnabled`<br /><br />Data type: **int** | Strings are padded to the same length before comparison or insert. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsAnsiWarningsEnabled`<br /><br />Data type: **int** | SQL Server issues error or warning messages when standard error conditions occur. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsArithmeticAbortEnabled`<br /><br />Data type: **int** | Queries end when an overflow or divide-by-zero error occurs during query execution. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsAutoClose`<br /><br />Data type: **int** | Database shuts down cleanly and frees resources after the last user exits. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsAutoCreateStatistics`<br /><br />Data type: **int** | Query optimizer creates single-column statistics, as required, to improve query performance. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsAutoCreateStatisticsIncremental`<br /><br />Data type: **int** | Auto-created single column statistics are incremental when possible. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions. |
| `IsAutomaticIndexCompactionOn`<br /><br />Data type: **int** | Automatic index compaction is enabled for the database. | - `1`: True<br />- `0`: False<br />- `NULL`: Not available<br /><br />**Applies to**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]<sup>[AUTD](/azure/azure-sql/managed-instance/update-policy#always-up-to-date-update-policy)</sup>, and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]. |
| `IsAutoShrink`<br /><br />Data type: **int** | Database files are candidates for automatic periodic shrinking. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsAutoUpdateStatistics`<br /><br />Data type: **int** | When a query uses potentially out-of-date existing statistics, the query optimizer updates those statistics. | - `1`: True<br />- `0`: False<br />- `NULL`: Input not valid |
| `IsClone`<br /><br />Data type: **int** | Database is a schema- and statistics-only copy of a user database created with `DBCC CLONEDATABASE`. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] SP2 and later versions. |
| `IsCloseCursorsOnCommitEnabled`<br /><br />Data type: **int** | When a transaction commits, all open cursors are closed. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsDatabaseSuspendedForSnapshotBackup`<br /><br />Data type: **int** | Database is suspended. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsFulltextEnabled`<br /><br />Data type: **int** | Database is enabled for full-text and semantic indexing. | - `1`: True<br />- `0`: False<br />- `NULL`: Input not valid<br /><br />**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later versions.<br /><br />**Note:** The value of this property now has no effect. User databases are always enabled for full-text search. A future release of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] will remove this property. Don't use this property in new development work, and modify applications that currently use this property as soon as possible. |
| `IsInStandBy`<br /><br />Data type: **int** | Database is online as read-only, with restore log allowed. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsLocalCursorsDefault`<br /><br />Data type: **int** | Cursor declarations default to `LOCAL`. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsMemoryOptimizedElevateToSnapshotEnabled`<br /><br />Data type: **int** | Memory-optimized tables are accessed using `SNAPSHOT` isolation, when the session setting `TRANSACTION ISOLATION LEVEL` is set to `READ COMMITTED`, `READ UNCOMMITTED`, or a lower isolation level. | - `1`: True<br />- `0`: False<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions. |
| `IsMergePublished`<br /><br />Data type: **int** | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports database table publication for merge replication, if replication is installed. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsNullConcat`<br /><br />Data type: **int** | Null concatenation operand yields `NULL`. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsNumericRoundAbortEnabled`<br /><br />Data type: **int** | Errors are generated when a loss of precision occurs in expressions. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsOptimizedLockingOn`<br /><br />Data type: **int** | Optimized locking is enabled for the database. | - `1`: True<br />- `0`: False<br />- `NULL`: Not available<br /><br />**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]<sup>[AUTD](/azure/azure-sql/managed-instance/update-policy#always-up-to-date-update-policy)</sup>, and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]. |
| `IsParameterizationForced`<br /><br />Data type: **int** | `PARAMETERIZATION` database `SET` option is `FORCED`. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsQuotedIdentifiersEnabled`<br /><br />Data type: **int** | Double quotation marks on identifiers are allowed. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsPublished`<br /><br />Data type: **int** | If replication is installed, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports database table publication for snapshot or transactional replication. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsRecursiveTriggersEnabled`<br /><br />Data type: **int** | Recursive firing of triggers is enabled. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsSubscribed`<br /><br />Data type: **int** | Database is subscribed to a publication. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsSyncWithBackup`<br /><br />Data type: **int** | The database is either a published database or a distribution database, and it supports a restore that doesn't disrupt transactional replication. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsTornPageDetectionEnabled`<br /><br />Data type: **int** | The [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] detects incomplete I/O operations caused by power failures or other system outages. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input |
| `IsVerifiedClone`<br /><br />Data type: **int** | Database is a schema- and statistics- only copy of a user database, created using the `WITH VERIFY_CLONEDB` option of `DBCC CLONEDATABASE`. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP2 and later versions. |
| `IsXTPSupported`<br /><br />Data type: **int** | Indicates whether the database supports In-Memory OLTP. For example, creation and use of memory-optimized tables and natively compiled modules.<br /><br />Specific to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]:<br /><br />IsXTPSupported is independent of the existence of any `MEMORY_OPTIMIZED_DATA` filegroup, which is required for creating In-Memory OLTP objects. | - `1`: True<br />- `0`: False<br />- `NULL`: Invalid input, an error, or not applicable<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. |
| `LastGoodCheckDbTime`<br /><br />Data type: **datetime** | The date and time of the last successful `DBCC CHECKDB` that ran on the specified database. If `DBCC CHECKDB` hasn't been run on a database, `1900-01-01 00:00:00.000` is returned. For databases that are part of an availability group, `LastGoodCheckDbTime` returns the date and time of the last successful `DBCC CHECKDB` that ran on the primary replica, regardless of which replica you run the command from. | `NULL`: Invalid input<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP2, [!INCLUDE [sssql17](../../includes/sssql17-md.md)] CU9, [!INCLUDE [sssql19](../../includes/sssql19-md.md)] and later versions, Azure SQL Database, and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]. |
| `LCID`<br /><br />Data type: **int** | The collation Windows locale identifier (LCID). | LCID value (in decimal format). |
| `MaxSizeInBytes`<br /><br />Data type: **bigint** | Maximum database size, in bytes. | - [Azure SQL Database and Azure Synapse Analytics](/azure/sql-database/sql-database-single-database-scale#dtu-based-purchasing-model): Value is based on SLO unless extra storage has been purchased.<br /><br />- [vCore](/azure/sql-database/sql-database-single-database-scale#vcore-based-purchasing-model): Value is in 1GB increments up to max size.<br /><br />- `NULL`: Database isn't started<br /><br />**Applies to**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], and [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]. |
| `Recovery`<br /><br />Data type: **nvarchar(128)** | Database recovery model. | - `FULL`: Full recovery model<br />- `BULK_LOGGED`: Bulk logged model<br />- `SIMPLE`: Simple recovery model |
| `ServiceObjective`<br /><br />Data type: **nvarchar(32)** | Describes the performance level of the database in [!INCLUDE [sssds](../../includes/sssds-md.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], or [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]. | One of the following values:<br /><br />- `NULL`: database not started<br />- `Shared` (for Web/Business editions)<br />- `Basic`<br />- `S0`<br />- `S1`<br />- `S2`<br />- `S3`<br />- `P1`<br />- `P2`<br />- `P3`<br />- `ElasticPool`<br />- `System` (for `master` database)<br />- `FabricSQLDB`: [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)] |
| `ServiceObjectiveId`<br /><br />Data type: **uniqueidentifier** | The ID of the service objective in [!INCLUDE [sssds](../../includes/sssds-md.md)]. | ID of the service objective. |
| `SQLSortOrder`<br /><br />Data type: **tinyint** | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] sort order ID supported in earlier versions of SQL Server. | - `0`: Database uses Windows collation<br /><br />- `>0`: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] sort order ID<br /><br />- `NULL`: Invalid input, or database hasn't started |
| `Status`<br /><br />Data type: **nvarchar(128)** | Database status. | `ONLINE`: Database is available for query.<br /><br />**Note:** The function might return a status of `ONLINE` while the database opens and hasn't yet recovered. To identify if an `ONLINE` database can accept connections, query the `Collation` property of `DATABASEPROPERTYEX`. The `ONLINE` database can accept connections when the database collation returns a non-null value. For Always On databases, query the `database_state` or `database_state_desc` columns of `sys.dm_hadr_database_replica_states`.<br /><br />- `OFFLINE`: Database was explicitly taken offline.<br /><br />- `RESTORING`: Database restore has started.<br /><br />- `RECOVERING`: Database recovery has started and the database isn't yet ready for queries.<br /><br />- `SUSPECT`: Database didn't recover.<br /><br />- `EMERGENCY`: Database is in an emergency, read-only state. Access is restricted to **sysadmin** members |
| `Updateability`<br /><br />Data type: **nvarchar(128)** | Indicates whether data can be modified. | `READ_ONLY`: Database supports data reads but not data modifications.<br /><br />- `READ_WRITE`: Database supports data reads and modifications. |
| `UserAccess`<br /><br />Data type: **nvarchar(128)** | Indicates which users can access the database. | `SINGLE_USER`: Only one **db_owner**, **dbcreator**, or **sysadmin** user at a time<br /><br />- `RESTRICTED_USER`: Only members of **db_owner**, **dbcreator**, or **sysadmin** fixed server roles<br /><br />- `MULTI_USER`: All users |
| `Version`<br /><br />Data type: **int** | Internal version number of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] code with which the database was created. [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)] | - Version number: Database is open.<br /><br />- `NULL`: Database hasn't started. |
| `ReplicaID`<br /><br />Data type: **nvarchar(128)** | The replica ID of a connected hyperscale database/replica. | Only returns the replica ID of a connected Hyperscale database/replica. To learn more about replica types, see [Hyperscale secondary replicas](/azure/azure-sql/database/service-tier-hyperscale-replicas).<br /><br />- `NULL`: Not a hyperscale database, or the database isn't started.<br /><br />**Applies to**: Azure SQL Database Hyperscale. |

## Return types

**sql_variant**

## Exceptions

Returns `NULL` on error, or if a caller doesn't have permission to view the object.

In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], a user can only view the metadata of securables that the user owns or on which the user has been granted permission. This rule means that metadata-emitting, built-in functions such as `OBJECT_ID` can return `NULL` if the user has no permissions on the object. For more information, see [Metadata visibility configuration](../../relational-databases/security/metadata-visibility-configuration.md).

## Remarks

`DATABASEPROPERTYEX` returns only one property setting at a time. To display multiple property settings, use the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Retrieve the status of the AUTO_SHRINK database option

This example returns the status of the `AUTO_SHRINK` database option for the `AdventureWorks` database.

```sql
SELECT DATABASEPROPERTYEX('AdventureWorks2022', 'IsAutoShrink');
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)] This indicates that `AUTO_SHRINK` is off.

```output
0
```

### B. Retrieve the default collation for a database

This example returns several attributes of the `AdventureWorks` database.

```sql
SELECT DATABASEPROPERTYEX('AdventureWorks2022', 'Collation') AS Collation,
       DATABASEPROPERTYEX('AdventureWorks2022', 'Edition') AS Edition,
       DATABASEPROPERTYEX('AdventureWorks2022', 'ServiceObjective') AS ServiceObjective,
       DATABASEPROPERTYEX('AdventureWorks2022', 'MaxSizeInBytes') AS MaxSizeInBytes;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Collation                     Edition        ServiceObjective  MaxSizeInBytes
----------------------------  -------------  ----------------  --------------
SQL_Latin1_General_CP1_CI_AS  DataWarehouse  DW1000            5368709120
```

### C. Use DATABASEPROPERTYEX to verify connection to replica

When you use the Azure SQL Database scale-out feature, you can verify whether you're connected to a read-only replica or not by running the following query in the context of your database. It returns `READ_ONLY` when you're connected to a read-only replica. This way, you can also identify when a query is running on a read-only replica.

```sql
SELECT DATABASEPROPERTYEX(DB_NAME(), 'Updateability');
```

## Related content

- [ALTER DATABASE (Transact-SQL)](../statements/alter-database-transact-sql.md)
- [Database states](../../relational-databases/databases/database-states.md)
- [sys.databases (Transact-SQL)](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [sys.database_files (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)
- [SERVERPROPERTY (Transact-SQL)](serverproperty-transact-sql.md)
