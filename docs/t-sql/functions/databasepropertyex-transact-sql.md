---
title: "DATABASEPROPERTYEX (Transact-SQL)"
description: "DATABASEPROPERTYEX (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "04/23/2018"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DATABASEPROPERTYEX"
  - "DATABASEPROPERTYEX_TSQL"
helpviewer_keywords:
  - "DATABASEPROPERTYEX function"
  - "displaying database properties"
  - "database properties [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# DATABASEPROPERTYEX (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

For a specified database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this function returns the current setting of the specified database option or property.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DATABASEPROPERTYEX ( database , property )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*database*  
An expression specifying the name of the database for which `DATABASEPROPERTYEX` will return the named property information. *database* has an **nvarchar(128)** data type.  

For [!INCLUDE[ssSDS](../../includes/sssds-md.md)], `DATABASEPROPERTYEX` requires the name of the current database. It returns NULL for all properties if given a different database name.
  
*property*  
An expression specifying the name of the database property to return. *property* has a **varchar(128)** data type, and supports one of the values in this table:
  
> [!NOTE]  
>  If the database hasn't yet started, calls to `DATABASEPROPERTYEX` will return NULL if `DATABASEPROPERTYEX` retrieves those values by direct database access, instead of retrieval from metadata. A database with AUTO_CLOSE set to ON, or otherwise offline, is defined as 'not started.'  
  
|Property|Description|Value returned|  
|---|---|---|
|Collation|Default collation name for the database.|Collation name<br /><br /> NULL: Database isn't started.<br /><br /> Base data type: **nvarchar(128)**|  
|ComparisonStyle|The Windows comparison style of the collation. Use the following style values to build a bitmap for the finished  ComparisonStyle value:<br /><br /> Ignore case: 1<br /> Ignore accent: 2<br />Ignore kana: 65536<br />Ignore width: 131072<br /><br /> For example, the default of 196609 is the result of combining the ignore case, ignore kana, and ignore width options.|Returns the comparison style.<br /><br /> Returns 0 for all binary collations.<br /><br /> Base data type: **int**|  
|Edition|The database edition or service tier.|**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], [!INCLUDE[ssSDW](../../includes/sssdw-md.md)].<br /><br /> <br /><br /> General Purpose<br /><br /> Business Critical<br /><br /> Basic<br /><br /> Standard<br /><br /> Premium<br /><br /> System (for master database)<br /><br /> NULL: Database isn't started.<br /><br /> Base data type: **nvarchar**(64)|  
|IsAnsiNullDefault|Database follows ISO rules for allowing null values.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|  
|IsAnsiNullsEnabled|All comparisons to a null evaluate to unknown.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|  
|IsAnsiPaddingEnabled|Strings are padded to the same length before comparison or insert.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|  
|IsAnsiWarningsEnabled|SQL Server issues error or warning messages when standard error conditions occur.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|  
|IsArithmeticAbortEnabled|Queries end when an overflow or divide-by-zero error occurs during query execution.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|  
|IsAutoClose|Database shuts down cleanly and frees resources after the last user exits.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|  
|IsAutoCreateStatistics|Query optimizer creates single-column statistics, as required, to improve query performance.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|  
|IsAutoCreateStatisticsIncremental|Auto-created single column statistics are incremental when possible.|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.<br /><br /> 1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|  
|IsAutoShrink|Database files are candidates for automatic periodic shrinking.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid  input<br /><br /> Base data type: **int**|  
|IsAutoUpdateStatistics|When a query uses potentially out-of-date existing statistics, the query optimizer updates those statistics.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Input not valid<br /><br /> Base data type: **int**|
|IsClone|Database is a schema- and statistics-only copy of a user database created with DBCC CLONEDATABASE. See [Microsoft Support Article](https://support.microsoft.com/help/3177838) for more information.|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2 and later.<br /><br /> 1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**| 
|IsCloseCursorsOnCommitEnabled|When a transaction commits, all open cursors will close.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**| 
|IsDatabaseSuspendedForSnapshotBackup|Database is suspended.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**<br /> |   
|IsFulltextEnabled|Database is enabled for full-text and semantic indexing.|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later.<br /><br /> <br /><br /> 1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Input not valid<br /><br /> Base data type: **int**<br /><br /> **Note:** The value of this property now has no effect. User databases are always enabled for full-text search. A future release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will remove this property. Do not use this property in new development work, and modify applications that currently use this property as soon as possible.|  
|IsInStandBy|Database is online as read-only, with restore log allowed.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|  
|IsLocalCursorsDefault|Cursor declarations default to LOCAL.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|  
|IsMemoryOptimizedElevateToSnapshotEnabled|Memory-optimized tables are accessed using SNAPSHOT isolation, when the session setting TRANSACTION ISOLATION LEVEL is set to READ COMMITTED, READ UNCOMMITTED, or a lower isolation level.|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.<br /><br /> <br /><br /> 1: TRUE<br /><br /> 0: FALSE<br /><br /> Base data type: **int**|  
|IsMergePublished|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports database table publication for merge replication, if replication is installed.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|  
|IsNullConcat|Null concatenation operand yields NULL.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|  
|IsNumericRoundAbortEnabled|Errors are generated when a loss of precision occurs in expressions.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|  
|IsParameterizationForced|PARAMETERIZATION database SET option is FORCED.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input|  
|IsQuotedIdentifiersEnabled|Double quotation marks on identifiers are allowed.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|  
|IsPublished|If replication is installed, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports database table publication for snapshot or transactional replication.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|  
|IsRecursiveTriggersEnabled|Recursive firing of triggers is enabled.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|  
|IsSubscribed|Database is subscribed to a publication.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|  
|IsSyncWithBackup|The database is either a published database or a distribution database, and it supports a restore that will not disrupt transactional replication.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|  
|IsTornPageDetectionEnabled|The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] detects incomplete I/O operations caused by power failures or other system outages.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**| 
|IsVerifiedClone|Database is a schema- and statistics- only copy of a user database, created using the WITH VERIFY_CLONEDB option of DBCC CLONEDATABASE. See this [Microsoft Support Article](https://support.microsoft.com/help/3177838) for more information.|**Applies to**: Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP2.<br /><br /> <br /><br /> 1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input<br /><br /> Base data type: **int**|
|IsXTPSupported|Indicates whether the database supports In-Memory OLTP. For example, creation and use of memory-optimized tables and natively compiled modules.<br /><br /> Specific to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:<br /><br /> IsXTPSupported is independent of the existence of any MEMORY_OPTIMIZED_DATA filegroup, which is required for creating In-Memory OLTP objects.|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later), and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].<br /><br /> 1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: Invalid input, an error, or not applicable<br /><br /> Base data type: **int**|  
|LastGoodCheckDbTime|The date and time of the last successful DBCC CHECKDB that ran on the specified database.<sup>1</sup> If DBCC CHECKDB hasn't been run on a database, 1900-01-01 00:00:00.000 is returned.|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] beginning with SP2.</br>[!INCLUDE[sssql17](../../includes/sssql17-md.md)] beginning with CU9.</br>[!INCLUDE[sssql19](../../includes/sssql19-md.md)] or later. </br>Azure SQL Database.<br/><br/>A datetime value<br /><br /> NULL: Invalid input<br /><br /> Base data type: **datetime**| 
|LCID|The collation Windows locale identifier (LCID).|LCID value (in decimal format).<br /><br /> Base data type: **int**|  
|MaxSizeInBytes|Maximum database size, in bytes.|**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], [!INCLUDE[ssSDW](../../includes/sssdw-md.md)].<br /><br />[Azure SQL Database and Azure Synapse Analytics](/azure/sql-database/sql-database-single-database-scale#dtu-based-purchasing-model) – Value is based on SLO unless additional storage has been purchased.<br /><br />[vCore](/azure/sql-database/sql-database-single-database-scale#vcore-based-purchasing-model) – Value is in 1GB increments up to max size.<br /><br />NULL: Database isn't started<br /><br /> Base data type: **bigint**|  
|Recovery|Database recovery model|FULL: Full recovery model<br /><br /> BULK_LOGGED: Bulk logged model<br /><br /> SIMPLE: Simple recovery model<br /><br /> Base data type: **nvarchar(128)**|  
|ServiceObjective|Describes the performance level of the database in [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)] or [!INCLUDE[ssSDW](../../includes/sssdw-md.md)].|One of the following values:<br /><br /> Null: database not started<br /><br /> Shared (for Web/Business editions)<br /><br /> Basic<br /><br /> S0<br /><br /> S1<br /><br /> S2<br /><br /> S3<br /><br /> P1<br /><br /> P2<br /><br /> P3<br /><br /> ElasticPool<br /><br /> System (for master DB)<br /><br /> Base data type: **nvarchar(32)**|  
|ServiceObjectiveId|The ID of the service objective in [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].|**uniqueidentifier** that identifies the service objective.|  
|SQLSortOrder|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sort order ID supported in earlier versions of SQL Server.|0: Database uses Windows collation<br /><br /> >0: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sort order ID<br /><br /> NULL: Invalid input, or database hasn't started<br /><br /> Base data type: **tinyint**|  
|Status|Database status.|ONLINE: Database is available for query.<br /><br /> **Note:** The function may return a status of ONLINE while the database opens and hasn't yet recovered. To identify if an ONLINE database can accept connections, query the Collation property of **DATABASEPROPERTYEX**. The ONLINE database can accept connections when the database collation returns a non-null value. For Always On databases, query the database_state or database_state_desc columns of `sys.dm_hadr_database_replica_states`.<br /><br /> OFFLINE: Database was explicitly taken offline.<br /><br /> RESTORING: Database restore has started.<br /><br /> RECOVERING: Database recovery has started and the database isn't yet ready for queries.<br /><br /> SUSPECT: Database didn't recover.<br /><br /> EMERGENCY: Database is in an emergency, read-only state. Access is restricted to sysadmin members<br /><br /> Base data type: **nvarchar(128)**|  
|Updateability|Indicates whether data can be modified.|READ_ONLY: Database supports data reads but not data modifications.<br /><br /> READ_WRITE: Database supports data reads and modifications.<br /><br /> Base data type: **nvarchar(128)**|  
|UserAccess|Indicates which users can access the database.|SINGLE_USER: Only one db_owner, dbcreator, or sysadmin user at a time<br /><br /> RESTRICTED_USER: Only members of db_owner, dbcreator, or sysadmin roles<br /><br /> MULTI_USER: All users<br /><br /> Base data type: **nvarchar(128)**|  
|Version|Internal version number of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] code with which the database was created. [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|Version number: Database is open.<br /><br /> NULL: Database hasn't started.<br /><br /> Base data type: **int**| 

<br/>   

> [!NOTE]  
> <sup>1</sup> For databases that are part of an Availability Group, `LastGoodCheckDbTime` will return the date and time of the last successful DBCC CHECKDB that ran on the primary replica, regardless of which replica you run the command from. 

## Return types
**sql_variant**
  
## Exceptions  
Returns NULL on error, or if a caller does not have permission to view the object.
  
In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a user can only view the metadata of securables that the user owns or on which the user has been granted permission. This rule means that metadata-emitting, built-in functions such as `OBJECT_ID` may return NULL if the user has no permissions on the object. See [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md) for more information.
  
## Remarks  
`DATABASEPROPERTYEX` returns only one property setting at a time. To display multiple property settings, use the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.
  
## Examples  
  
### A. Retrieving the status of the AUTO_SHRINK database option  
This example returns the status of the AUTO_SHRINK database option for the `AdventureWorks` database.
  
```sql
SELECT DATABASEPROPERTYEX('AdventureWorks2014', 'IsAutoShrink');  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)] This indicates that AUTO_SHRINK is off.
  
```
------------------  
0  
```  
  
### B. Retrieving the default collation for a database  
This example returns several attributes of the `AdventureWorks` database.
  
```sql
SELECT   
    DATABASEPROPERTYEX('AdventureWorks2014', 'Collation') AS Collation,  
    DATABASEPROPERTYEX('AdventureWorks2014', 'Edition') AS Edition,  
    DATABASEPROPERTYEX('AdventureWorks2014', 'ServiceObjective') AS ServiceObjective,  
    DATABASEPROPERTYEX('AdventureWorks2014', 'MaxSizeInBytes') AS MaxSizeInBytes  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
Collation                     Edition        ServiceObjective  MaxSizeInBytes  
----------------------------  -------------  ----------------  --------------  
SQL_Latin1_General_CP1_CI_AS  DataWarehouse  DW1000            5368709120  
```  

### C. Use DATABASEPROPERTYEX to verify connection to replica  
When using Azure SQL Database read the scale-out feature, you can verify whether you're connected to a read-only replica or not by running the following query in the context of your database. It will return READ_ONLY when you're connected to a read-only replica. This way, you can also identify when a query is running on a read-only replica.
  
```sql
SELECT DATABASEPROPERTYEX(DB_NAME(), 'Updateability'); 
```  

## See also
[ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)  
[Database States](../../relational-databases/databases/database-states.md)  
[sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)  
[sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)  
[SERVERPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/serverproperty-transact-sql.md)
  
  
