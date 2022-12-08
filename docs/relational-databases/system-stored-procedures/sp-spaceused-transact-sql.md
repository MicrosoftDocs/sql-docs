---
description: "sp_spaceused displays the number of rows, disk space reserved, and disk space used by objects in the database."
title: "sp_spaceused (Transact-SQL)"
ms.custom: ""
ms.date: 07/25/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_spaceused_TSQL"
  - "sp_spaceused"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_spaceused"
ms.assetid: c6253b48-29f5-4371-bfcd-3ef404060621
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_spaceused (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Displays the number of rows, disk space reserved, and disk space used by a table, indexed view, or [!INCLUDE[ssSB](../../includes/sssb-md.md)] queue in the current database, or displays the disk space reserved and used by the whole database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
sp_spaceused [[ @objname = ] 'objname' ]   
[, [ @updateusage = ] 'updateusage' ]  
[, [ @mode = ] 'mode' ]  
[, [ @oneresultset = ] oneresultset ]  
[, [ @include_total_xtp_storage = ] include_total_xtp_storage ]
```
  
> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]
  
## Arguments  

For [!INCLUDE[sssdw-md](../../includes/sssdw-md.md)] and [!INCLUDE[sspdw-md](../../includes/sspdw-md.md)], `sp_spaceused` must specify named parameters (for example `sp_spaceused (@objname= N'Table1');` rather than relying upon the ordinal position of parameters. 

`[ @objname = ] 'objname'`
   
 Is the qualified or nonqualified name of the table, indexed view, or queue for which space usage information is requested. Quotation marks are required only if a qualified object name is specified. If a fully qualified object name (including a database name) is provided, the database name must be the name of the current database.  
If *objname* is not specified, results are returned for the whole database.  
*objname* is **nvarchar(776)**, with a default of NULL.  
> [!NOTE]  
> [!INCLUDE[sssdw-md](../../includes/sssdw-md.md)] and [!INCLUDE[sspdw-md](../../includes/sspdw-md.md)] only support database and table objects.
  
`[ @updateusage = ] 'updateusage'`
 Indicates DBCC UPDATEUSAGE should be run to update space usage information. When *objname* is not specified, the statement is run on the whole database; otherwise, the statement is run on *objname*. Values can be **true** or **false**. *updateusage* is **varchar(5)**, with a default of **false**.  
  
`[ @mode = ] 'mode'`
 Indicates the scope of the results. For a stretched table or database, the *mode* parameter lets you include or exclude the remote portion of the object. For more info, see [Stretch Database](../../sql-server/stretch-database/stretch-database.md).  

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

 The *mode* argument can have the following values:  
  
|Value|Description|  
|-----------|-----------------|  
|ALL|Returns the storage statistics of the object or database including both the local portion and the remote portion.|  
|LOCAL_ONLY|Returns the storage statistics of only the local portion of the object or database. If the object or database is not Stretch-enabled, returns the same statistics as when @mode = ALL.|  
|REMOTE_ONLY|Returns the storage statistics of only the remote portion of the object or database. This option raises an error when one of the following conditions is true:<br /><br /> The table is not enabled for Stretch.<br /><br /> The table is enabled for Stretch, but you have never enabled data migration. In this case, the remote table does not yet have a schema.<br /><br /> The user has manually dropped the remote table.<br /><br /> The provisioning of the remote data archive returned a status of Success, but in fact it failed.|  
  
 *mode* is **varchar(11)**, with a default of **N'ALL'**.  
  
`[ @oneresultset = ] oneresultset`
 Indicates whether to return a single result set. The *oneresultset* argument can have the following values:  
  
|Value|Description|  
|-----------|-----------------|  
|0|When *\@objname* is null or is not specified, two result sets are returned. Two result sets is the default behavior.|  
|1|When *\@objname* = null or is not specified, a single result set is returned.|  
  
 *oneresultset* is **bit**, with a default of **0**.  

`[ @include_total_xtp_storage] 'include_total_xtp_storage'`
**Applies to:** [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)], [!INCLUDE[sssds-md](../../includes/sssds-md.md)].  
  
 When @oneresultset=1, the parameter @include_total_xtp_storage determines whether the single resultset includes columns for MEMORY_OPTIMIZED_DATA storage. The default value is 0, that is, by default (if the parameter is omitted) the XTP columns are not included in the resultset.  

## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 If *objname* is omitted and the value of *oneresultset* is 0, the following result sets are returned to provide current database size information.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_name**|**nvarchar(128)**|Name of the current database.|  
|**database_size**|**varchar(18)**|Size of the current database in megabytes. **database_size** includes both data and log files.|  
|**unallocated space**|**varchar(18)**|Space in the database that has not been reserved for database objects.|  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**reserved**|**varchar(18)**|Total amount of space allocated by objects in the database.|  
|**data**|**varchar(18)**|Total amount of space used by data.|  
|**index_size**|**varchar(18)**|Total amount of space used by indexes.|  
|**unused**|**varchar(18)**|Total amount of space reserved for objects in the database, but not yet used.|  
  
 If *objname* is omitted and the value of *oneresultset* is 1, the following single result set is returned to provide current database size information.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_name**|**nvarchar(128)**|Name of the current database.|  
|**database_size**|**varchar(18)**|Size of the current database in megabytes. **database_size** includes both data and log files.|  
|**unallocated space**|**varchar(18)**|Space in the database that has not been reserved for database objects.|  
|**reserved**|**varchar(18)**|Total amount of space allocated by objects in the database.|  
|**data**|**varchar(18)**|Total amount of space used by data.|  
|**index_size**|**varchar(18)**|Total amount of space used by indexes.|  
|**unused**|**varchar(18)**|Total amount of space reserved for objects in the database, but not yet used.|  
  
 If *objname* is specified, the following result set is returned for the specified object.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**nvarchar(128)**|Name of the object for which space usage information was requested.<br /><br /> The schema name of the object is not returned. If the schema name is required, use the [sys.dm_db_partition_stats](../../relational-databases/system-dynamic-management-views/sys-dm-db-partition-stats-transact-sql.md) or [sys.dm_db_index_physical_stats](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md) dynamic management views to obtain equivalent size information.|  
|**rows**|**char(20)**|Number of rows existing in the table. If the object specified is a [!INCLUDE[ssSB](../../includes/sssb-md.md)] queue, this column indicates the number of messages in the queue.|  
|**reserved**|**varchar(18)**|Total amount of reserved space for *objname*.|  
|**data**|**varchar(18)**|Total amount of space used by data in *objname*.|  
|**index_size**|**varchar(18)**|Total amount of space used by indexes in *objname*.|  
|**unused**|**varchar(18)**|Total amount of space reserved for *objname* but not yet used.|  
 
This is the default mode, when no parameters are specified. The following result sets are returned detailing on-disk database size information. 

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_name**|**nvarchar(128)**|Name of the current database.|  
|**database_size**|**varchar(18)**|Size of the current database in megabytes. **database_size** includes both data and log files. If the database has a MEMORY_OPTIMIZED_DATA filegroup, this includes the total on-disk size of all checkpoint files in the filegroup.|  
|**unallocated space**|**varchar(18)**|Space in the database that has not been reserved for database objects. If the database has a MEMORY_OPTIMIZED_DATA filegroup, this includes the total on-disk size of the checkpoint files with state PRECREATED in the filegroup.|  

Space used by tables in the database: (this resultset does not reflect memory-optimized tables, as there is no per-table accounting of disk usage) 

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**reserved**|**varchar(18)**|Total amount of space allocated by objects in the database.|  
|**data**|**varchar(18)**|Total amount of space used by data.|  
|**index_size**|**varchar(18)**|Total amount of space used by indexes.|  
|**unused**|**varchar(18)**|Total amount of space reserved for objects in the database, but not yet used.|

The following result set is returned **ONLY IF** the database has a MEMORY_OPTIMIZED_DATA filegroup with at least one container: 

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**xtp_precreated**|**varchar(18)**|Total size of checkpoint files with state PRECREATED, in KB. Counts towards the unallocated space in the database as a whole. [For example, if there is 600,000 KB of precreated checkpoint files, this column contains '600000 KB']|  
|**xtp_used**|**varchar(18)**|Total size of checkpoint files with states UNDER CONSTRUCTION, ACTIVE, and MERGE TARGET, in KB. This is the disk space actively used for data in memory-optimized tables.|  
|**xtp_pending_truncation**|**varchar(18)**|Total size of checkpoint files with state WAITING_FOR_LOG_TRUNCATION, in KB. This is the disk space used for checkpoint files that are awaiting cleanup, once log truncation happens.|

If *objname* is omitted, the value of oneresultset is 1, and *include_total_xtp_storage* is 1, the following single result set is returned to provide current database size information. If `include_total_xtp_storage` is 0 (the default), the last three columns are omitted. 

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_name**|**nvarchar(128)**|Name of the current database.|  
|**database_size**|**varchar(18)**|Size of the current database in megabytes. **database_size** includes both data and log files. If the database has a MEMORY_OPTIMIZED_DATA filegroup, this includes the total on-disk size of all checkpoint files in the filegroup.|
|**unallocated space**|**varchar(18)**|Space in the database that has not been reserved for database objects. If the database has a MEMORY_OPTIMIZED_DATA filegroup, this includes the total on-disk size of the checkpoint files with state PRECREATED in the filegroup.|  
|**reserved**|**varchar(18)**|Total amount of space allocated by objects in the database.|  
|**data**|**varchar(18)**|Total amount of space used by data.|  
|**index_size**|**varchar(18)**|Total amount of space used by indexes.|  
|**unused**|**varchar(18)**|Total amount of space reserved for objects in the database, but not yet used.|
|**xtp_precreated**|**varchar(18)**|Total size of checkpoint files with state PRECREATED, in KB. This counts towards the unallocated space in the database as a whole. Returns NULL if the database does not have a memory_optimized_data filegroup with at least one container. *This column is only included if @include_total_xtp_storage=1*.| 
|**xtp_used**|**varchar(18)**|Total size of checkpoint files with states UNDER CONSTRUCTION, ACTIVE, and MERGE TARGET, in KB. This is the disk space actively used for data in memory-optimized tables. Returns NULL if the database does not have a memory_optimized_data filegroup with at least one container. *This column is only included if @include_total_xtp_storage=1*.| 
|**xtp_pending_truncation**|**varchar(18)**|Total size of checkpoint files with state WAITING_FOR_LOG_TRUNCATION, in KB. This is the disk space used for checkpoint files that are awaiting cleanup, once log truncation happens. Returns NULL if the database does not have a memory_optimized_data filegroup with at least one container. This column is only included if `@include_total_xtp_storage=1`.|

## Remarks  
 **database_size** is generally larger than the sum of **reserved** + **unallocated space** because it includes the size of log files, but **reserved** and **unallocated_space** consider only data pages. In some cases with Azure Synapse Analytics, this statement may not be true. 
  
 Pages that are used by XML indexes and full-text indexes are included in **index_size** for both result sets. When *objname* is specified, the pages for the XML indexes and full-text indexes for the object are also counted in the total **reserved** and **index_size** results.  
  
 If space usage is calculated for a database or an object that has a spatial index, the space-size columns, such as **database_size**, **reserved**, and **index_size**, include the size of the spatial index.  
  
 When *updateusage* is specified, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] scans the data pages in the database and makes any required corrections to the **sys.allocation_units** and **sys.partitions** catalog views regarding the storage space used by each table. There are some situations, for example, after an index is dropped, when the space information for the table may not be current. *updateusage* can take some time to run on large tables or databases. Use *updateusage* only when you suspect incorrect values are being returned and when the process will not have an adverse effect on other users or processes in the database. If preferred, DBCC UPDATEUSAGE can be run separately.  
  
> [!NOTE]  
>  When you drop or rebuild large indexes, or drop or truncate large tables, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] defers the actual page deallocations, and their associated locks, until after the transaction commits. Deferred drop operations do not release allocated space immediately. Therefore, the values returned by **sp_spaceused** immediately after dropping or truncating a large object may not reflect the actual disk space available.  
  
## Permissions  
 Permission to execute **sp_spaceused** is granted to the **public** role. Only members of the **db_owner** fixed database role can specify the **\@updateusage** parameter.  
  
## Examples  
  
### A. Displaying disk space information about a table  
 The following example reports disk space information for the `Vendor` table and its indexes.  
  
```sql  
USE AdventureWorks2016;  
GO  
EXEC sp_spaceused N'Purchasing.Vendor';  
GO  
```  
  
### B. Displaying updated space information about a database  
 The following example summarizes space used in the current database and uses the optional parameter `@updateusage` to ensure current values are returned.  
  
```sql  
USE AdventureWorks2016;  
GO  
EXEC sp_spaceused @updateusage = N'TRUE';  
GO  
```  
  
### C. Displaying space usage information about the remote table associated with a Stretch-enabled table  
 The following example summarizes the space used by the remote table associated with a Stretch-enabled table by using the **\@mode** argument to specify the remote target. For more info, see [Stretch Database](../../sql-server/stretch-database/stretch-database.md).  
  
```sql  
USE StretchedAdventureWorks2016  
GO  
EXEC sp_spaceused N'Purchasing.Vendor', @mode = 'REMOTE_ONLY'  
```  
  
### D. Displaying space usage information for a database in a single result set  
 The following example summarizes space usage for the current database in a single result set.  
  
```sql  
USE AdventureWorks2016  
GO  
EXEC sp_spaceused @oneresultset = 1  
```  

### E. Displaying space usage information for a database with at least one MEMORY_OPTIMIZED file group in a single result set 
 The following example summarizes space usage for the current database with at least one MEMORY_OPTIMIZED file group in a single result set.
 
```sql
USE WideWorldImporters
GO
EXEC sp_spaceused @updateusage = 'FALSE', @mode = 'ALL', @oneresultset = '1', @include_total_xtp_storage = '1';
GO
``` 

### F. Displaying space usage information for a MEMORY_OPTIMIZED table object in a database.
 The following example summarizes space usage for a MEMORY_OPTIMIZED table object in the current database with at least one MEMORY_OPTIMIZED file group.
 
```sql
USE WideWorldImporters
GO
EXEC sp_spaceused
@objname = N'VehicleTemparatures',
@updateusage = 'FALSE',
@mode = 'ALL',
@oneresultset = '0',
@include_total_xtp_storage = '1';
GO
```  

## See Also  
 [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [DBCC UPDATEUSAGE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-updateusage-transact-sql.md)   
 [SQL Server Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)   
 [sys.allocation_units &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-allocation-units-transact-sql.md)   
 [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)   
 [sys.index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [sys.partitions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partitions-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
