---
title: "DATABASEPROPERTYEX (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/29/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DATABASEPROPERTYEX"
  - "DATABASEPROPERTYEX_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DATABASEPROPERTYEX function"
  - "displaying database properties"
  - "database properties [SQL Server]"
ms.assetid: 8a9e0ffb-28b5-4640-95b2-a54e3e5ad941
caps.latest.revision: 84
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DATABASEPROPERTYEX (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Returns the current setting of the specified database option or property for the specified database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
DATABASEPROPERTYEX ( database , property )  
```  
  
## Arguments  
*database*  
Is an expression that represents the name of the database for which to return the named property information. *database* is **nvarchar(128)**.  
For [!INCLUDE[ssSDS](../../includes/sssds-md.md)], must be the name of the current database. Returns NULL for all properties if a different database name is provided.
  
*property*  
Is an expression that represents the name of the database property to return. *property* is **varchar(128)**, and can be one of the following values. The return type is **sql_variant**. The following table shows the base data type for each property value.
  
> [!NOTE]  
>  If the database is not started, properties that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] retrieves by accessing the database directly instead of retrieving the value from metadata will return NULL. That is, if the database has AUTO_CLOSE set to ON, or the database is otherwise offline.  
  
|Property|Description|Value returned|  
|---|---|---|
|Collation|Default collation name for the database.|Collation name<br /><br /> NULL = Database is not started.<br /><br /> Base data type: **nvarchar(128)**|  
|ComparisonStyle|The Windows comparison style of the collation. ComparisonStyle is a bitmap that is calculated by using the following values for the possible styles.<br /><br /> Ignore case : 1<br /><br /> Ignore accent : 2<br /><br /> Ignore Kana : 65536<br /><br /> Ignore width : 131072<br /><br /> <br /><br /> For example, the default of 196609 is the result of combining the Ignore case, Ignore Kana, and Ignore width options.|Returns the comparison style.<br /><br /> Returns 0 for all binary collations.<br /><br /> Base data type: **int**|  
|Edition|The database edition or service tier.|**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], [!INCLUDE[ssSDW](../../includes/sssdw-md.md)].<br /><br /> <br /><br /> Web = Web Edition Database<br /><br /> Business = Business Edition Database<br /><br /> Basic<br /><br /> Standard<br /><br /> Premium<br /><br /> System (for master database)<br /><br /> NULL = Database is not started.<br /><br /> Base data type: **nvarchar**(64)|  
|IsAnsiNullDefault|Database follows ISO rules for allowing null values.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsAnsiNullsEnabled|All comparisons to a null evaluate to unknown.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsAnsiPaddingEnabled|Strings are padded to the same length before comparison or insert.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsAnsiWarningsEnabled|Error or warning messages are issued when standard error conditions occur.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsArithmeticAbortEnabled|Queries are ended when an overflow or divide-by-zero error occurs during query execution.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsAutoClose|Database shuts down cleanly and frees resources after the last user exits.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsAutoCreateStatistics|Query optimizer creates single-column statistics, as required, to improve query performance.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsAutoCreateStatisticsIncremental|Auto created single-column statistics are incremental when possible.|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> <br /><br /> 1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsAutoShrink|Database files are candidates for automatic periodic shrinking.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsAutoUpdateStatistics|Query optimizer updates existing statistics when they are used by a query and might be out-of-date.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|
|IsClone|Database is a schema and statistics only copy of a user database.|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Service Pack 2.<br /><br /> <br /><br /> 1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**| 
|IsCloseCursorsOnCommitEnabled|Cursors that are open when a transaction is committed are closed.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsFulltextEnabled|Database is enabled for full-text and semantic indexing.|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> <br /><br /> 1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**<br /><br /> **Note:** The value of this property has no effect. User databases are always enabled for full-text search. This column will be removed in a future release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Do not use this column in new development work, and modify applications that currently use any of these columns as soon as possible.|  
|IsInStandBy|Database is online as read-only, with restore log allowed.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsLocalCursorsDefault|Cursor declarations default to LOCAL.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsMemoryOptimizedElevateToSnapshotEnabled|Memory-optimized tables are accessed using SNAPSHOT isolation when the session setting TRANSACTION ISOLATION LEVEL is set to a lower isolation level, READ COMMITTED or READ UNCOMMITTED.|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> <br /><br /> 1 = TRUE<br /><br /> 0 = FALSE<br /><br /> Base data type: **int**|  
|IsMergePublished|The tables of a database can be published for merge replication, if replication is installed.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsNullConcat|Null concatenation operand yields NULL.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsNumericRoundAbortEnabled|Errors are generated when loss of precision occurs in expressions.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsParameterizationForced|PARAMETERIZATION database SET option is FORCED.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid|  
|IsQuotedIdentifiersEnabled|Double quotation marks can be used on identifiers.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsPublished|The tables of the database can be published for snapshot or transactional replication, if replication is installed.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsRecursiveTriggersEnabled|Recursive firing of triggers is enabled.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsSubscribed|Database is subscribed to a publication.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsSyncWithBackup|The database is either a published database or a distribution database, and can be restored without disrupting transactional replication.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsTornPageDetectionEnabled|The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] detects incomplete I/O operations caused by power failures or other system outages.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid<br /><br /> Base data type: **int**|  
|IsXTPSupported|Indicates whether the database supports In-Memory OLTP, i.e., creating and using memory-optimized tables and natively compiled modules.<br /><br /> Specific to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:<br /><br /> IsXTPSupported is independent of the existence of any MEMORY_OPTIMIZED_DATA filegroup, which is required for creating In-Memory OLTP objects.|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]), [!INCLUDE[ssSDS](../../includes/sssds-md.md)].<br /><br /> **Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] starting [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)].<br /><br /> <br /><br /> 1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input not valid, an error, or not applicable<br /><br /> Base data type: **int**|  
|LCID|The Windows locale identifier (LCID) of the collation.|LCID value (in decimal format).<br /><br /> Base data type: **int**|  
|MaxSizeInBytes|Maximum database size in bytes.|**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], [!INCLUDE[ssSDW](../../includes/sssdw-md.md)].<br /><br /> <br /><br /> 1073741824<br /><br /> 5368709120<br /><br /> 10737418240<br /><br /> 21474836480<br /><br /> 32212254720<br /><br /> 42949672960<br /><br /> 53687091200<br /><br /> NULL = Database is not started<br /><br /> Base data type: **bigint**|  
|Recovery|Recovery model for the database.|FULL = Full recovery model<br /><br /> BULK_LOGGED = Bulk logged model<br /><br /> SIMPLE = Simple recovery model<br /><br /> Base data type: **nvarchar(128)**|  
|ServiceObjective|Describes the performance level of the database in [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)] or [!INCLUDE[ssSDW](../../includes/sssdw-md.md)].|Can be one of the following:<br /><br /> Null: database not started<br /><br /> Shared (for Web/Business editions)<br /><br /> Basic<br /><br /> S0<br /><br /> S1<br /><br /> S2<br /><br /> S3<br /><br /> P1<br /><br /> P2<br /><br /> P3<br /><br /> ElasticPool<br /><br /> System (for master DB)<br /><br /> Base data type: **nvarchar(32)**|  
|ServiceObjectiveId|The id of the service objective in [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].|**uniqueidentifier** that identifies the service objective.|  
|SQLSortOrder|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sort order ID supported in earlier versions of SQL Server.|0 = Database is using Windows collation<br /><br /> >0 = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sort order ID<br /><br /> NULL = Input not valid or database is not started<br /><br /> Base data type: **tinyint**|  
|Status|Database status.|ONLINE = Database is available for query.<br /><br /> **Note:** The ONLINE status may be returned while the database is being opened and is not yet recovered. To identify when a database can accept connections, query the Collation property of **DATABASEPROPERTYEX**. The database can accept connections when the database collation returns a non-null value. For Always On databases, query the database_state or database_state_desc columns of sys.dm_hadr_database_replica_states.<br /><br /> OFFLINE = Database was explicitly taken offline.<br /><br /> RESTORING = Database is being restored.<br /><br /> RECOVERING = Database is recovering and not yet ready for queries.<br /><br /> SUSPECT = Database did not recover.<br /><br /> EMERGENCY = Database is in an emergency, read-only state. Access is restricted to sysadmin members<br /><br /> Base data type: **nvarchar(128)**|  
|Updateability|Indicates whether data can be modified.|READ_ONLY = Data can be read but not modified.<br /><br /> READ_WRITE = Data can be read and modified.<br /><br /> Base data type: **nvarchar(128)**|  
|UserAccess|Indicates which users can access the database.|SINGLE_USER = Only one db_owner, dbcreator, or sysadmin user at a time<br /><br /> RESTRICTED_USER = Only members of db_owner, dbcreator, and sysadmin roles<br /><br /> MULTI_USER = All users<br /><br /> Base data type: **nvarchar(128)**|  
|Version|Internal version number of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] code with which the database was created. [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|Version number = Database is open.<br /><br /> NULL = Database is not started.<br /><br /> Base data type: **int**|  
  
## Return types
**sql_variant**
  
## Exceptions  
Returns NULL on error or if a caller does not have permission to view the object.
  
In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as OBJECT_ID may return NULL if the user does not have any permission on the object. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).
  
## Remarks  
DATABASEPROPERTYEX returns only one property setting at a time. To display multiple property settings, use the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.
  
## Examples  
  
### A. Retrieving the status of the AUTO_SHRINK database option  
The following example returns the status of the AUTO_SHRINK database option for the `AdventureWorks` database.
  
```sql
SELECT DATABASEPROPERTYEX('AdventureWorks2014', 'IsAutoShrink');  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)] This indicates that AUTO_SHRINK is off.
  
```sql
------------------  
0  
```  
  
### B. Retrieving the default collation for a database  
The following example returns several attributes of the `AdventureWorks` database.
  
```sql
SELECT   
    DATABASEPROPERTYEX('AdventureWorks2014', 'Collation') AS Collation,  
    DATABASEPROPERTYEX('AdventureWorks2014', 'Edition') AS Edition,  
    DATABASEPROPERTYEX('AdventureWorks2014', 'ServiceObjective') AS ServiceObjective,  
    DATABASEPROPERTYEX('AdventureWorks2014', 'MaxSizeInBytes') AS MaxSizeInBytes  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
Collation                     Edition        ServiceObjective  MaxSizeInBytes  
----------------------------  -------------  ----------------  --------------  
SQL_Latin1_General_CP1_CI_AS  DataWarehouse  DW1000            5368709120  
```  
  
## See also
[ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)  
[Database States](../../relational-databases/databases/database-states.md)  
[sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)  
[sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)  
[SERVERPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/serverproperty-transact-sql.md)
  
  
