---
title: "Statistics for Memory-Optimized Tables"
description: Learn how the query optimizer uses statistics about columns in memory-optimized tables to create query plans that improve query performance for In-Memory OLTP.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "10/23/2016"
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.assetid: e644766d-1d1c-43d7-83ff-8ccfe4f3af9f
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Statistics for Memory-Optimized Tables
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  The query optimizer uses statistics about columns to create query plans that improve query performance. Statistics are collected from the tables in the database and stored in the database metadata.  
  
 Statistics are created automatically, but can also be created manually. For example, statistics are created automatically for index key columns when the index is created. For more information about creating statistics see [Statistics](../../relational-databases/statistics/statistics.md).  
  
 Table data typically changes over time as rows are inserted, updated, and deleted. This means statistics need to be updated periodically. By default, statistics on tables are updated automatically when the query optimizer determines they might be out of date.  
  
 Considerations for statistics on memory-optimized tables:  
  
-   Starting in SQL Server 2016 and in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], automatic update of statistics is supported for memory-optimized tables, when using database compatibility level of at least 130. See [ALTER DATABASE Compatibility Level (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md). If a database has tables that were previously created using a lower compatibility level, the statistics need to be updated manually once, to enable automatic update of statistics going forward.
  
-   For natively compiled stored procedures, execution plans for queries in the procedure are optimized when the procedure is compiled, which happens at create time. They are not automatically recompiled when statistics are updated. Therefore, the tables should contain a representative set of data before the procedures are created.  
  
-   Natively compiled stored procedures can be manually recompiled using [sp_recompile (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-recompile-transact-sql.md), and they are automatically recompiled if the database is taken offline and brought back online, or if there is a database failover or server restart.  
  
## Enabling Automatic Update of Statistics in Existing Tables

When tables are created in a database that has compatibility level of at least 130, automatic update of statistics is enabled for all statistics on that table, and no further action is needed.

If a database has memory-optimized tables that were created in an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or under a lower compatibility level than 130, the statistics need to be updated manually once to enable automatic update going forward.

To enable automatic update of statistics for memory-optimized tables that were created under an older compatibility level, follow these steps:

1. Update the database compatibility level: `ALTER DATABASE CURRENT SET COMPATIBILITY_LEVEL=130`

2. Manually update the statistics of the memory-optimized tables. Below is a sample script that performs the same.

3. Manually recompile the natively compiled stored procedures to benefit from the updated statistics.

*One-time script for statistics:* For memory-optimized tables that were created under a lower compatibility level, you can run the following Transact-SQL script one time to update the statistics of all memory-optimized tables, and enable automatic update of statistics from then onward (assuming AUTO_UPDATE_STATISTICS is enabled for the database):

```
-- Assuming AUTO_UPDATE_STATISTICS is already ON for your database:
-- ALTER DATABASE CURRENT SET AUTO_UPDATE_STATISTICS ON;

ALTER DATABASE CURRENT SET COMPATIBILITY_LEVEL = 130;
GO
DECLARE @sql NVARCHAR(MAX) = N'';
SELECT
      @sql += N'UPDATE STATISTICS '
         + quotename(schema_name(t.schema_id))
         + N'.'
         + quotename(t.name)
         + ';' + CHAR(13) + CHAR(10)
   FROM sys.tables AS t
   WHERE t.is_memory_optimized = 1 AND 
		t.object_id IN (SELECT object_id FROM sys.stats WHERE no_recompute=1)
;
EXECUTE sp_executesql @sql;
GO
-- Each row appended to @sql looks roughly like:
-- UPDATE STATISTICS [dbo].[MyMemoryOptimizedTable];
```

*Verify auto-update is enabled:* The following script verifies whether automatic update is enabled for statistics on memory-optimized tables. After running the previous script it will return `1` in the column `auto-update enabled` for all statistic objects.

```
SELECT 
	quotename(schema_name(o.schema_id)) + N'.' + quotename(o.name) AS [table],
	s.name AS [statistics object],
	1-s.no_recompute AS [auto-update enabled]
FROM sys.stats s JOIN sys.tables o ON s.object_id=o.object_id
WHERE o.is_memory_optimized=1
```

## Guidelines for Deploying Tables and Procedures  
 To ensure that the query optimizer has up-to-date statistics when creating query plans, deploy memory-optimized tables and natively compiled stored procedures that access these tables using these four steps:  
  
1.  Ensure the database has compatibility level of at least 130. See [ALTER DATABASE Compatibility Level (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

2.  Create tables and indexes. Indexes should be specified inline in the **CREATE TABLE** statements.  
  
3.  Load data into the tables.  
  
4.  Create stored procedures that access the tables.  
  
 Creating natively compiled stored procedures after you load the data ensures that the optimizer has statistics available for the memory-optimized tables. This will ensure efficient query plans when the procedure is compiled.  

## See Also  
 [Memory-Optimized Tables](./sample-database-for-in-memory-oltp.md)  
  
