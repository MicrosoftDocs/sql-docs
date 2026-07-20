---
title: "sys.dm_db_stats_properties (Transact-SQL)"
description: The sys.dm_db_stats_properties dynamic management function returns properties of statistics for the specified database object.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/13/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.dm_db_stats_properties_TSQL"
  - "sys.dm_db_stats_properties"
  - "dm_db_stats_properties_TSQL"
  - "dm_db_stats_properties"
helpviewer_keywords:
  - "sys.dm_db_stats_properties"
dev_langs:
  - TSQL
ai-usage: ai-assisted
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.dm_db_stats_properties (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The `sys.dm_db_stats_properties` dynamic management function returns statistics for the specified database object in the current database. 

For partitioned tables, also see the similar [sys.dm_db_incremental_stats_properties](sys-dm-db-incremental-stats-properties-transact-sql.md).

## Syntax

```syntaxsql
sys.dm_db_stats_properties (object_id, stats_id)  
```  

## Arguments

#### object_id

  The ID of the object in the current database. `object_id` is **int**.    

#### stats_id

 The ID of statistics for the specified `object_id`. You can get the statistics ID from the [sys.stats](../system-catalog-views/sys-stats-transact-sql.md) dynamic management view. `stats_id` is **int**.  

## Table returned

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
| `object_id` |**int**|ID of the object (table or indexed view).|    
| `stats_id` |**int**|ID of the statistics object. Is unique within the table or indexed view. For more information, see [sys.stats (Transact-SQL)](../system-catalog-views/sys-stats-transact-sql.md).|  
| `last_updated` |**datetime2**|Date and time the statistics object was last updated. For more information, see the [Remarks](#Remarks) section in this article.|  
| `rows` |**bigint**|Total number of rows in the table or indexed view when statistics were last updated. If the statistics are filtered or correspond to a filtered index, the number of rows might be less than the number of rows in the table.|  
| `rows_sampled` |**bigint**|Total number of rows sampled for statistics calculations.|  
| `steps` |**int**|Number of steps in the histogram. For more information, see [DBCC SHOW_STATISTICS (Transact-SQL)](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md).|  
| `unfiltered_rows` |**bigint**|Total number of rows in the table before applying the filter expression (for filtered statistics). If statistics aren't filtered, `unfiltered_rows` is equal to the value returned in the `rows` column.|  
| `modification_counter` |**bigint**|Total number of modifications for the leading statistics column (the column on which the histogram is built) since the last time statistics were updated.<br /><br /> Memory-optimized tables: starting [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] this column contains: total number of modifications for the table since the last time statistics were updated or the database was restarted.|  
| `persisted_sample_percent` |**float**|Persisted sample percentage used for statistic updates that don't explicitly specify a sampling percentage. If value is zero, then no persisted sample percentage is set for this statistic.<br /><br /> **Applies to:** [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP1 CU4 and later versions|  

<a id="Remarks"></a>

## Remarks

The `sys.dm_db_stats_properties` function returns an empty rowset under any of the following conditions:  

- `object_id` or `stats_id` is `NULL`.
- The specified object isn't found or doesn't correspond to a table or indexed view.    
- The specified statistics ID doesn't correspond to existing statistics for the specified object ID.    
- The current user doesn't have permissions to view the statistics object.  

This behavior allows for the safe usage of `sys.dm_db_stats_properties` when cross applied to rows in views such as `sys.objects` and `sys.stats`.

Statistics update date is stored in the [statistics blob object](../statistics/statistics.md#DefinitionQOStatistics) together with the [histogram](../statistics/statistics.md#histogram) and [density vector](../statistics/statistics.md#density), not in the metadata. When no data is read to generate statistics data, the statistics blob isn't created, the date isn't available, and the `last_updated` column is `NULL`. This condition applies to filtered statistics for which the predicate doesn't return any rows, or to new empty tables.

## Permissions

You need `SELECT` permissions on statistics columns, or you need to own the table, or be a member of the `sysadmin` fixed server role, the `db_owner` fixed database role, or the `db_ddladmin` fixed database role.    

## Examples

### A. Simple example

The following example returns information about the statistics for the `Person.Person` table in the `AdventureWorks` database.

```sql
SELECT * FROM sys.dm_db_stats_properties (object_id('Person.Person'), 1);
``` 

<a id="b-returning-all-statistics-properties-for-a-table"></a>

### B. Return all statistics properties for a table

The following example returns properties of all statistics that exist for the table `Sales.SalesOrderDetail`.  

```sql  
SELECT 
    OBJECT_SCHEMA_NAME(stat.object_id) AS schema_name,
    OBJECT_NAME(stat.object_id)        AS table_name,
    sp.stats_id, 
    stat.[name], 
    stat.filter_definition, 
    sp.last_updated, 
    sp.[rows], 
    sp.rows_sampled, 
    sp.steps,
    sp.unfiltered_rows, 
    sp.modification_counter   
FROM sys.stats AS stat   
OUTER APPLY sys.dm_db_stats_properties(stat.object_id, stat.stats_id) AS sp  
WHERE stat.object_id = OBJECT_ID(N'Sales.SalesOrderDetail')
```  

<a id="c-returning-statistics-properties-for-frequently-modified-objects"></a>

### C. Return statistics properties for frequently modified objects

The following example returns all tables, indexed views, and statistics in the current database for which the leading column was modified more than 1,000 times since the last statistics update.  

```sql  
SELECT 
    OBJECT_SCHEMA_NAME(obj.[object_id]) AS schema_name,
    obj.[name],
    stat.[name],
    stat.stats_id, 
    sp.last_updated, 
    sp.modification_counter  
FROM sys.objects AS obj   
INNER JOIN sys.stats AS stat ON stat.object_id = obj.object_id  
OUTER APPLY sys.dm_db_stats_properties(stat.object_id, stat.stats_id) AS sp  
WHERE sp.modification_counter > 1000;
```

## Related content

- [DBCC SHOW_STATISTICS (Transact-SQL)](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md)
- [sys.stats (Transact-SQL)](../system-catalog-views/sys-stats-transact-sql.md)
- [Object Related Dynamic Management Views and Functions (Transact-SQL)](object-related-dynamic-management-views-and-functions-transact-sql.md)
- [Dynamic Management Views and Functions (Transact-SQL)](system-dynamic-management-objects.md)
- [sys.dm_db_incremental_stats_properties (Transact-SQL)](sys-dm-db-incremental-stats-properties-transact-sql.md)
- [sys.dm_db_stats_histogram (Transact-SQL)](sys-dm-db-stats-histogram-transact-sql.md)
