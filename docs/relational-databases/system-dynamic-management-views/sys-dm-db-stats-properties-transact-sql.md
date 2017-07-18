---
title: "sys.dm_db_stats_properties (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/05/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_db_stats_properties_TSQL"
  - "sys.dm_db_stats_properties"
  - "dm_db_stats_properties_TSQL"
  - "dm_db_stats_properties"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_db_stats_properties"
ms.assetid: 8a54889d-e263-4881-9fcb-b1db410a9453
caps.latest.revision: 13
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.dm_db_stats_properties (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns properties of statistics for the specified database object (table or indexed view) in the current [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. For partitioned tables, see the similar [sys.dm_db_incremental_stats_properties](../../relational-databases/system-dynamic-management-views/sys-dm-db-incremental-stats-properties-transact-sql.md). 
 
## Syntax  
  
```  
sys.dm_db_stats_properties (object_id, stats_id)  
```  
  
## Arguments  
 *object_id*  
 Is the ID of the object in the current database for which properties of one of its statistics is requested. *object_id* is **int**.  
  
 *stats_id*  
 Is the ID of statistics for the specified *object_id*. The statistics ID can be obtained from the [sys.stats](../../relational-databases/system-catalog-views/sys-stats-transact-sql.md) dynamic management view. *stats_id* is **int**.  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|object_id|**int**|ID of the object (table or indexed view) for which to return the properties of the statistics object.|  
|stats_id|**int**|ID of the statistics object. Is unique within the table or indexed view. For more information, see [sys.stats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-stats-transact-sql.md).|  
|last_updated|**datetime2**|Date and time the statistics object was last updated.|  
|rows|**bigint**|Total number of rows in the table or indexed view when statistics were last updated. If the statistics are filtered or correspond to a filtered index, the number of rows might be less than the number of rows in the table.|  
|rows_sampled|**bigint**|Total number of rows sampled for statistics calculations.|  
|steps|**int**|Number of steps in the histogram. For more information, see [DBCC SHOW_STATISTICS &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md).|  
|unfiltered_rows|**bigint**|Total number of rows in the table before applying the filter expression (for filtered statistics). If statistics are not filtered, unfiltered_rows is equal to the value returns in the rows column.|  
|modification_counter|**bigint**|Total number of modifications for the leading statistics column (the column on which the histogram is built) since the last time statistics were updated.<br /><br /> Memory-optimized tables: starting [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] and in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] this column contains: total number of modifications for the table since the last time statistics were updated or the database was restarted.|  
  
## Remarks  
 **sys.dm_db_stats_properties** returns an empty rowset under any of the following conditions:  
  
-   **object_id** or **stats_id** is NULL.  
  
-   The specified object is not found or does not correspond to a table or indexed view.  
  
-   The specified statistics ID does not correspond to existing statistics for the specified object ID.  
  
-   The current user does not have permissions to view the statistics object.  
  
 This behavior allows for the safe usage of **sys.dm_db_stats_properties** when cross applied to rows in views such as **sys.objects** and **sys.stats**.  
  
## Permissions  
 Requires that the user has select permissions on statistics columns or the user owns the table or the user is a member of the `sysadmin` fixed server role, the `db_owner` fixed database role, or the `db_ddladmin` fixed database role.  
  
## Examples  

### A. Simple example
The following example returns the statistics for the `Person.Person` table in the AdventureWorks database.

```
SELECT * FROM sys.dm_db_stats_properties (object_id('Person.Person'), 1);
``` 
  
### B. Returning all statistics properties for a table  
 The following example returns properties of all statistics that exist for the table TEST.  
  
```  
SELECT sp.stats_id, name, filter_definition, last_updated, rows, rows_sampled, steps, unfiltered_rows, modification_counter   
FROM sys.stats AS stat   
CROSS APPLY sys.dm_db_stats_properties(stat.object_id, stat.stats_id) AS sp  
WHERE stat.object_id = object_id('TEST');  
```  
  
### C. Returning statistics properties for frequently modified objects  
 The following example returns all tables, indexed views, and statistics in the current database for which the leading column was modified more than 1000 times since the last statistics update.  
  
```  
SELECT obj.name, obj.object_id, stat.name, stat.stats_id, last_updated, modification_counter  
FROM sys.objects AS obj   
INNER JOIN sys.stats AS stat ON stat.object_id = obj.object_id  
CROSS APPLY sys.dm_db_stats_properties(stat.object_id, stat.stats_id) AS sp  
WHERE modification_counter > 1000;  
```  
  
## See Also  
 [DBCC SHOW_STATISTICS &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md)   
 [sys.stats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-stats-transact-sql.md)   
 [Object Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/object-related-dynamic-management-views-and-functions-transact-sql.md)   
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)  
 [sys.dm_db_incremental_stats_properties (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-incremental-stats-properties-transact-sql.md)  
 [sys.dm_db_stats_histogram (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-stats-histogram-transact-sql.md) 
  

