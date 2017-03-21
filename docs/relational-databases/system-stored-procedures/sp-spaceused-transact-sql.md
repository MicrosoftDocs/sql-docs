---
title: "sp_spaceused (Transact-SQL) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "10/18/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sp_spaceused_TSQL"
  - "sp_spaceused"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_spaceused"
ms.assetid: c6253b48-29f5-4371-bfcd-3ef404060621
caps.latest.revision: 62
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sp_spaceused (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Displays the number of rows, disk space reserved, and disk space used by a table, indexed view, or [!INCLUDE[ssSB](../../includes/sssb-md.md)] queue in the current database, or displays the disk space reserved and used by the whole database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Applies to SQL Server and Azure SQL Database 
sp_spaceused [[ @objname = ] 'objname' ]   
[, [ @updateusage = ] 'updateusage' ]  
[, [ @mode = ] 'mode' ]  
[, [ @oneresultset = ] oneresultset ]  
```  
```  
-- Applies to Azure SQL Data Warehouse and Parallel Data Warehouse 
sp_spaceused   -- works on databases, not tables 
[, [ @updateusage = ] 'updateusage' ]  
[, [ @mode = ] 'mode' ]  
[, [ @oneresultset = ] oneresultset ]  
```
 
 
  
## Arguments  
 [ **@objname=**] **'***objname***'** 
 Applies to: SQL Server, Azure SQL Database
  
 Is the qualified or nonqualified name of the table, indexed view, or queue for which space usage information is requested. Quotation marks are required only if a qualified object name is specified. If a fully qualified object name (including a database name) is provided, the database name must be the name of the current database.  
  
 If *objname* is not specified, results are returned for the whole database.  
  
 *objname* is **nvarchar(776)**, with a default of NULL.  
  
 [ **@updateusage=**] **'***updateusage***'**  
 Indicates DBCC UPDATEUSAGE should be run to update space usage information. When *objname* is not specified, the statement is run on the whole database; otherwise, the statement is run on *objname*. Values can be **true** or **false**. *updateusage* is **varchar(5)**, with a default of **false**.  
  
 [ **@mode=**] **'***mode***'**  
 Indicates the scope of the results. For a stretched table or database, the *mode* parameter lets you include or exclude the remote portion of the object. For more info, see [Stretch Database](../../sql-server/stretch-database/stretch-database.md).  
  
 The *mode* argument can have the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|ALL|Returns the storage statistics of the object or database including both the local portion and the remote portion.|  
|LOCAL_ONLY|Returns the storage statistics of only the local portion of the object or database. If the object or database is not Stretch-enabled, returns the same statistics as when @mode = ALL.|  
|REMOTE_ONLY|Returns the storage statistics of only the remote portion of the object or database. This option raises an error when one of the following conditions is true:<br /><br /> The table is not enabled for Stretch.<br /><br /> The table is enabled for Stretch, but you have never enabled data migration. In this case, the remote table does not yet have a schema.<br /><br /> The user has manually dropped the remote table.<br /><br /> The provisioning of the remote data archive returned a status of Success, but in fact it failed.|  
  
 *mode* is **varchar(11)**, with a default of **N'ALL'**.  
  
 [ **@oneresultset=**] *oneresultset*  
 Indicates whether to return a single result set. The *oneresultset* argument can have the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|0|When *@objname* is null or is not specified, two result sets are returned. This is the current and default behavior.|  
|1|When *@objname* = null or is not specified, a single result set is returned.|  
  
 *oneresultset* is **bit**, with a default of **0**.  
  
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
  
## Remarks  
 **database_size** will always be larger than the sum of **reserved** + **unallocated space** because it includes the size of log files, but **reserved** and **unallocated_space** consider only data pages.  
  
 Pages that are used by XML indexes and full-text indexes are included in **index_size** for both result sets. When *objname* is specified, the pages for the XML indexes and full-text indexes for the object are also counted in the total **reserved** and **index_size** results.  
  
 If space usage is calculated for a database or an object that has a spatial index, the space-size columns, such as **database_size**, **reserved**, and **index_size**, include the size of the spatial index.  
  
 When *updateusage* is specified, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] scans the data pages in the database and makes any required corrections to the **sys.allocation_units** and **sys.partitions** catalog views regarding the storage space used by each table. There are some situations, for example, after an index is dropped, when the space information for the table may not be current. *updateusage* can take some time to run on large tables or databases. Use *updateusage* only when you suspect incorrect values are being returned and when the process will not have an adverse effect on other users or processes in the database. If preferred, DBCC UPDATEUSAGE can be run separately.  
  
> [!NOTE]  
>  When you drop or rebuild large indexes, or drop or truncate large tables, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] defers the actual page deallocations, and their associated locks, until after the transaction commits. Deferred drop operations do not release allocated space immediately. Therefore, the values returned by **sp_spaceused** immediately after dropping or truncating a large object may not reflect the actual disk space available.  
  
## Permissions  
 Permission to execute **sp_spaceused** is granted to the **public** role. Only members of the **db_owner** fixed database role can specify the **@updateusage** parameter.  
  
## Examples  
  
### A. Displaying disk space information about a table  
 The following example reports disk space information for the `Vendor` table and its indexes.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_spaceused N'Purchasing.Vendor';  
GO  
```  
  
### B. Displaying updated space information about a database  
 The following example summarizes space used in the current database and uses the optional parameter `@updateusage` to ensure current values are returned.  
  
```  
USE AdventureWorks008R2;  
GO  
EXEC sp_spaceused @updateusage = N'TRUE';  
GO  
```  
  
### C. Displaying space usage information about the remote table associated with a Stretch-enabled table  
 The following example summarizes the space used by the remote table associated with a Stretch-enabled table by using the **@mode** argument to specify the remote target. For more info, see [Stretch Database](../../sql-server/stretch-database/stretch-database.md).  
  
```tsql  
USE StretchedAdventureWorks2016  
GO  
EXEC sp_spaceused N'Purchasing.Vendor', @mode = 'REMOTE_ONLY'  
```  
  
### D. Displaying space usage information for a database in a single result set  
 The following example summarizes space usage for the current database in a single result set.  
  
```tsql  
USE AdventureWorks2016  
GO  
EXEC sp_spaceused @oneresultset = 1  
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
  
  
