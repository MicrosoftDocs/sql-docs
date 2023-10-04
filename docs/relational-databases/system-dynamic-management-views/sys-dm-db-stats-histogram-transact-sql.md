---
title: "sys.dm_db_stats_histogram (Transact-SQL)"
description: sys.dm_db_stats_histogram returns the statistics histogram for the specified database object (table or indexed view) in the current database.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/06/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: conceptual
f1_keywords:
  - "sys.dm_db_stats_histogram"
  - "sys.dm_db_stats_histogram_TSQL"
  - "dm_db_stats_histogram"
  - "dm_db_stats_histogram_TSQL"
helpviewer_keywords:
  - "sys.dm_db_stats_histogram dynamic management function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# sys.dm_db_stats_histogram (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Returns the statistics histogram for the specified database object (table or indexed view) in the current [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. Similar to `DBCC SHOW_STATISTICS WITH HISTOGRAM`.

> [!NOTE] 
> This DMF is available starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP1 CU2

## Syntax  
  
```syntaxsql  
sys.dm_db_stats_histogram (object_id, stats_id)  
```  
  
## Arguments  

 *object_id*  
 Is the ID of the object in the current database for which properties of one of its statistics is requested. *object_id* is **int**.  
  
 *stats_id*  
 Is the ID of statistics for the specified *object_id*. The statistics ID can be obtained from the [sys.stats](../../relational-databases/system-catalog-views/sys-stats-transact-sql.md) dynamic management view. *stats_id* is **int**.  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|object_id |**int**|ID of the object (table or indexed view) for which to return the properties of the statistics object.|  
|stats_id |**int**|ID of the statistics object. Is unique within the table or indexed view. For more information, see [sys.stats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-stats-transact-sql.md).|  
|step_number |**int** |The number of step in the histogram. |
|range_high_key |**sql_variant** |Upper bound column value for a histogram step. The column value is also called a key value.|
|range_rows |**real** |Estimated number of rows whose column value falls within a histogram step, excluding the upper bound. |
|equal_rows |**real** |Estimated number of rows whose column value equals the upper bound of the histogram step. |
|distinct_range_rows |**bigint** |Estimated number of rows with a distinct column value within a histogram step, excluding the upper bound. |
|average_range_rows |**real** |Average number of rows with duplicate column values within a histogram step, excluding the upper bound (`RANGE_ROWS / DISTINCT_RANGE_ROWS` for `DISTINCT_RANGE_ROWS > 0`). |
  
 ## Remarks  
 
 The resultset for `sys.dm_db_stats_histogram` returns information similar to `DBCC SHOW_STATISTICS WITH HISTOGRAM` and also includes `object_id`, `stats_id`, and `step_number`.

 Because the column `range_high_key` is a sql_variant data type, you may need to use `CAST` or `CONVERT` if a predicate does comparison with a non-string constant.

### Histogram
  
 A histogram measures the frequency of occurrence for each distinct value in a data set. The query optimizer computes a histogram on the column values in the first key column of the statistics object, selecting the column values by statistically sampling the rows or by performing a full scan of all rows in the table or view. If the histogram is created from a sampled set of rows, the stored totals for number of rows and number of distinct values are estimates and do not need to be whole integers.  
  
 To create the histogram, the query optimizer sorts the column values, computes the number of values that match each distinct column value and then aggregates the column values into a maximum of 200 contiguous histogram steps. Each step includes a range of column values followed by an upper bound column value. The range includes all possible column values between boundary values, excluding the boundary values themselves. The lowest of the sorted column values is the upper boundary value for the first histogram step.  
  
 The following diagram shows a histogram with six steps. The area to the left of the first upper boundary value is the first step.  
  
 :::image type="content" source="../../relational-databases/system-dynamic-management-views/media/histogram-2.svg" alt-text="Image of how a histogram is calculated from sampled column values.":::  

 For each histogram step:  
  
-   Bold line represents the upper boundary value (*range_high_key*) and the number of times it occurs (*equal_rows*)  
  
-   Solid area left of *range_high_key* represents the range of column values and the average number of times each column value occurs (*average_range_rows*). The *average_range_rows* for the first histogram step is always 0.  
  
-   Dotted lines represent the sampled values used to estimate total number of distinct values in the range (*distinct_range_rows*) and total number of values in the range (*range_rows*). The query optimizer uses *range_rows* and *distinct_range_rows* to compute *average_range_rows* and does not store the sampled values.  
  
 The query optimizer defines the histogram steps according to their statistical significance. It uses a maximum difference algorithm to minimize the number of steps in the histogram while maximizing the difference between the boundary values. The maximum number of steps is 200. The number of histogram steps can be fewer than the number of distinct values, even for columns with fewer than 200 boundary points. For example, a column with 100 distinct values can have a histogram with fewer than 100 boundary points.  
  
## Permissions  

Requires that the user has select permissions on statistics columns or the user owns the table or the user is a member of the `sysadmin` fixed server role, the `db_owner` fixed database role, or the `db_ddladmin` fixed database role.

## Examples  

### A. Simple example    
The following example creates and populates a simple table. Then creates statistics on the `Country_Name` column.

```sql
CREATE TABLE Country
(Country_ID int IDENTITY PRIMARY KEY,
Country_Name varchar(120) NOT NULL);
INSERT Country (Country_Name) VALUES ('Canada'), ('Denmark'), ('Iceland'), ('Peru');

CREATE STATISTICS Country_Stats  
    ON Country (Country_Name) ;  
```   
The primary key occupies `stat_id` number 1, so call `sys.dm_db_stats_histogram` for `stat_id` number 2, to return the statistics histogram for the `Country` table.

```sql
SELECT * FROM sys.dm_db_stats_histogram(OBJECT_ID('Country'), 2);
```

### B. Useful query:   

```sql  
SELECT hist.step_number, hist.range_high_key, hist.range_rows, 
    hist.equal_rows, hist.distinct_range_rows, hist.average_range_rows
FROM sys.stats AS s
CROSS APPLY sys.dm_db_stats_histogram(s.[object_id], s.stats_id) AS hist
WHERE s.[name] = N'<statistic_name>';
```

### C. Useful query:

The following example selects from table `Country` with a predicate on column `Country_Name`.

```sql  
SELECT * FROM Country 
WHERE Country_Name = 'Canada';
```

The following example looks at the previously created statistic on table `Country` and column `Country_Name` for the histogram step matching the predicate in the query above.

```sql  
SELECT ss.name, ss.stats_id, shr.steps, shr.rows, shr.rows_sampled, 
    shr.modification_counter, shr.last_updated, sh.range_rows, sh.equal_rows
FROM sys.stats ss
INNER JOIN sys.stats_columns sc 
    ON ss.stats_id = sc.stats_id AND ss.object_id = sc.object_id
INNER JOIN sys.all_columns ac 
    ON ac.column_id = sc.column_id AND ac.object_id = sc.object_id
CROSS APPLY sys.dm_db_stats_properties(ss.object_id, ss.stats_id) shr
CROSS APPLY sys.dm_db_stats_histogram(ss.object_id, ss.stats_id) sh
WHERE ss.[object_id] = OBJECT_ID('Country') 
    AND ac.name = 'Country_Name'
    AND sh.range_high_key = CAST('Canada' AS CHAR(8));
```
  
## Next steps
[DBCC SHOW_STATISTICS (Transact-SQL)](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md)   
[Object Related Dynamic Management Views and Functions (Transact-SQL)](../../relational-databases/system-dynamic-management-views/object-related-dynamic-management-views-and-functions-transact-sql.md)  
[sys.dm_db_stats_properties (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-stats-properties-transact-sql.md)  
