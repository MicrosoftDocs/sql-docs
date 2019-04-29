---
title: "sp_updatestats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/25/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_updatestats_TSQL"
  - "sp_updatestats"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_updatestats"
ms.assetid: 01184651-6e61-45d9-a502-366fecca0ee4
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_updatestats (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Runs `UPDATE STATISTICS` against all user-defined and internal tables in the current database.  
  
For more information about `UPDATE STATISTICS`, see [UPDATE STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/update-statistics-transact-sql.md). For more information about statistics, see [Statistics](../../relational-databases/statistics/statistics.md).  
    
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sp_updatestats [ [ @resample = ] 'resample']  
```  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Arguments  
`[ @resample = ] 'resample'`
 Specifies that **sp_updatestats** will use the RESAMPLE option of the [UPDATE STATISTICS](../../t-sql/statements/update-statistics-transact-sql.md) statement. If **'resample'** is not specified, **sp_updatestats** updates statistics by using the default sampling. **resample** is **varchar(8)** with a default value of NO.  
  
## Remarks  
 **sp_updatestats** executes `UPDATE STATISTICS`, by specifying the `ALL` keyword, on all user-defined and internal tables in the database. sp_updatestats displays messages that indicate its progress. When the update is completed, it reports that statistics have been updated for all tables.  
  
sp_updatestats updates statistics on disabled nonclustered indexes and does not update statistics on disabled clustered indexes.  
  
For disk-based tables, **sp_updatestats** updates statistics based on the **modification_counter** information in the **sys.dm_db_stats_properties** catalog view, updating statistics where at least one row has been modified. Statistics on memory-optimized tables are always updated when executing **sp_updatestats**. Therefore do not execute **sp_updatestats** more than necessary.  
  
**sp_updatestats** can trigger a recompile of stored procedures or other compiled code. However, **sp_updatestats** might not cause a recompile, if only one query plan is possible for the tables referenced and the indexes on them. A recompilation would be unnecessary in these cases even if statistics are updated.  
  
For databases with a compatibility level below 90, executing **sp_updatestats** does not preserve the latest NORECOMPUTE setting for specific statistics. For databases with a compatibility level of 90 or higher, sp_updatestats does preserve the latest NORECOMPUTE option for specific statistics. For more information about disabling and re-enabling statistics updates, see [Statistics](../../relational-databases/statistics/statistics.md).  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role, or ownership of the database (**dbo**).  

## Examples  
The following example updates the statistics for tables in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```sql  
USE AdventureWorks2012;  
GO  
EXEC sp_updatestats;   
```  

## Automatic index and statistics management
Leverage solutions such as [Adaptive Index Defrag](https://github.com/Microsoft/tigertoolbox/tree/master/AdaptiveIndexDefrag) to automatically manage index defragmentation and statistics updates for one or more databases. This procedure automatically chooses whether to rebuild or reorganize an index according to its fragmentation level, amongst other parameters, and update statistics with a linear threshold.

## See Also  
 [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)   
 [CREATE STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/create-statistics-transact-sql.md)   
 [DBCC SHOW_STATISTICS &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md)   
 [DROP STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/drop-statistics-transact-sql.md)   
 [sp_autostats &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-autostats-transact-sql.md)   
 [sp_createstats &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-createstats-transact-sql.md)   
 [UPDATE STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/update-statistics-transact-sql.md)   
 [System Stored Procedures](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
 
