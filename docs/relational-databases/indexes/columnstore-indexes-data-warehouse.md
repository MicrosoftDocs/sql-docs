---
title: "Columnstore indexes - Data Warehouse | Microsoft Docs"
ms.custom: ""
ms.date: "12/01/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
ms.assetid: 21fd153b-116d-47fc-a926-f1528299a391
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Columnstore indexes - Data Warehouse
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  Columnstore indexes, in conjunction with partitioning, are essential for building a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data warehouse.  
  
## What's new  
 [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] introduces these features for columnstore performance enhancements:  
  
-   Always On supports querying a columnstore index on a readable secondary replica.  
-   Multiple Active Result Sets (MARS) supports columnstore indexes.  
-   A new dynamic management view [sys.dm_db_column_store_row_group_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-column-store-row-group-physical-stats-transact-sql.md) provides performance troubleshooting information at the row group level.  
-   Single-threaded queries on columnstore indexes can run in batch mode. Previously, only multi-threaded queries could run in batch mode.  
-   The `SORT` operator runs in batch mode.  
-   Multiple `DISTINCT` operation runs in batch mode.  
-   Window Aggregates now runs in batch mode for database compatibility level 130 and higher.  
-   Aggregate Pushdown for efficient processing of aggregates. This is supported on all database compatibility levels.  
-   String predicate pushdown for efficient processing of string predicates. This is supported on all database compatibility levels.  
-   Snapshot isolation for database compatibility level 130 and higher.  
  
## Improve performance by combining nonclustered and columnstore indexes  
 Beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], you can define nonclustered indexes on a clustered columnstore index.   
  
### Example: Improve efficiency of table seeks with a nonclustered index  
 To improve efficiency of table seeks in a data warehouse, you can create a nonclustered index designed to run queries that perform best with table seeks. For example, queries that look for matching values or return a small range of values will perform better against a B-tree index rather than a columnstore index. They don't require a full table scan through the columnstore index and will return the correct result faster by doing a binary search through a B-tree index.  
  
```sql  
--BASIC EXAMPLE: Create a nonclustered index on a columnstore table.  
  
--Create the table  
CREATE TABLE t_account (  
    AccountKey int NOT NULL,  
    AccountDescription nvarchar (50),  
    AccountType nvarchar(50),  
    UnitSold int  
);  
GO  
  
--Store the table as a columnstore.  
CREATE CLUSTERED COLUMNSTORE INDEX taccount_cci ON t_account;  
GO  
  
--Add a nonclustered index.  
CREATE UNIQUE INDEX taccount_nc1 ON t_account (AccountKey);  
```  
  
### Example: Use a nonclustered index to enforce a primary key constraint on a columnstore table  
 By design, a columnstore table does not allow a primary key constraint. Now you can use a nonclustered index on a columnstore table to enforce a primary key constraint. A primary key is equivalent to a UNIQUE constraint on a non-NULL column, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] implements a UNIQUE constraint as a nonclustered index. Combining these facts, the following example defines a UNIQUE constraint on the non-NULL column accountkey. The result is a nonclustered index that enforces a primary key constraint as a UNIQUE constraint on a non-NULL column.  
  
 Next, the table is converted to a clustered columnstore index. During the conversion, the nonclustered index persists. The result is a clustered columnstore index with a nonclustered index that enforces a primary key constraint. Since any update or insert on the columnstore table will also affect the nonclustered index, all operations that violate the unique constraint and the non-NULL will cause the entire operation to fail.  
  
 The result is a columnstore index with a nonclustered index that enforces a primary key constraint on both indexes.  
  
```sql 
--EXAMPLE: Enforce a primary key constraint on a columnstore table.   
  
--Create a rowstore table with a unique constraint.  
--The unique constraint is implemented as a nonclustered index.  
CREATE TABLE t_account (  
    AccountKey int NOT NULL,  
    AccountDescription nvarchar (50),  
    AccountType nvarchar(50),  
    UnitSold int,  
  
    CONSTRAINT uniq_account UNIQUE (AccountKey)  
);  
  
--Store the table as a columnstore.   
--The unique constraint is preserved as a nonclustered index on the columnstore table.  
CREATE CLUSTERED COLUMNSTORE INDEX t_account_cci ON t_account  
  
--By using the previous two steps, every row in the table meets the UNIQUE constraint  
--on a non-NULL column.  
--This has the same end-result as having a primary key constraint  
--All updates and inserts must meet the unique constraint on the nonclustered index or they will fail.  
  
--If desired, add a foreign key constraint on AccountKey.  
  
ALTER TABLE [dbo].[t_account]  
WITH CHECK ADD FOREIGN KEY([AccountKey]) REFERENCES my_dimension(Accountkey); 
```  
  
### Improve performance by enabling row-level and row-group-level locking  
 To complement the nonclustered index on a columnstore index feature, [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] offers granular locking capability for select, update, and delete operations. Queries can run with row-level locking on index seeks against a nonclustered index and rowgroup-level locking on full table scans against the columnstore index. Use this to achieve higher read/write concurrency by using row-level and rowgroup-level locking appropriately.  
  
```sql  
--Granular locking example  
--Store table t_account as a columnstore table.  
CREATE CLUSTERED COLUMNSTORE INDEX taccount_cci ON t_account  
GO  
  
--Add a nonclustered index for use with this example  
CREATE UNIQUE INDEX taccount_nc1 ON t_account (AccountKey);  
GO  
  
--Look at locking with access through the nonclustered index  
SET TRANSACTION ISOLATION LEVEL repeatable read;  
GO  
  
BEGIN TRAN  
    -- The query plan chooses a seek operation on the nonclustered index  
    -- and takes the row lock  
    SELECT * FROM t_account WHERE AccountKey = 100;  
END TRAN  
```  
  
### Snapshot isolation and read-committed snapshot isolations  
 Use snapshot isolation (SI) to guarantee transactional consistency, and read-committed snapshot isolations (RCSI) to guarantee statement level consistency for queries on columnstore indexes. This allows the queries to run without blocking data writers. This non-blocking behavior also significantly reduces the likelihood of deadlocks for complex transactions. For more information, see [Snapshot Isolation in SQL Server](https://msdn.microsoft.com/library/tcbchxcb\(v=vs.110\).aspx) on MSDN.  
  
## See Also  
 [Columnstore Indexes Design Guidance](../../relational-databases/indexes/columnstore-indexes-design-guidance.md)   
 [Columnstore Indexes Data Loading Guidance](../../relational-databases/indexes/columnstore-indexes-data-loading-guidance.md)   
 [Columnstore Indexes Query Performance](../../relational-databases/indexes/columnstore-indexes-query-performance.md)   
 [Get started with Columnstore for real-time operational analytics](../../relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics.md)   
 [Columnstore Indexes Defragmentation](../../relational-databases/indexes/columnstore-indexes-defragmentation.md)  
 [Columnstore Index Architecture](../../relational-databases/sql-server-index-design-guide.md#columnstore_index) 
  
