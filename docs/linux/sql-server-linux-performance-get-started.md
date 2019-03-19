---
title: Get started with performance features of SQL Server on Linux | Microsoft Docs
description: This article provides an introduction of SQL Server performance features for Linux users who are new to SQL Server. Many of these examples work on all platforms, but the context of this article is Linux. 
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 03/17/2017
ms.topic: conceptual
ms.prod: sql
ms.technology: linux
ms.assetid: 60036d26-4797-4872-9a9e-3552841c61be
ms.custom: "sql-linux"
---
# Walkthrough for the performance features of SQL Server on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

If you are a Linux user who is new to SQL Server, the following tasks walk you through some of the performance features. These are not unique or specific to Linux, but it helps to give you an idea of areas to investigate further. In each example, a link is provided to the depth documentation for that area.

> [!NOTE]
> The following examples use the AdventureWorks sample database. For instructions on how to obtain and install this sample database, see [Restore a SQL Server database from Windows to Linux](sql-server-linux-migrate-restore-database.md).

## Create a Columnstore Index
A columnstore index is a technology for storing and querying large stores of data in a columnar data format, called a columnstore.  

1. Add a Columnstore index to the SalesOrderDetail table by executing the following Transact-SQL commands:

   ```sql
   CREATE NONCLUSTERED COLUMNSTORE INDEX [IX_SalesOrderDetail_ColumnStore]
      ON Sales.SalesOrderDetail
      (UnitPrice, OrderQty, ProductID)
   GO
   ```

2. Execute the following query that uses the Columnstore Index to scan the table:

   ```sql
   SELECT ProductID, SUM(UnitPrice) SumUnitPrice, AVG(UnitPrice) AvgUnitPrice,
      SUM(OrderQty) SumOrderQty, AVG(OrderQty) AvgOrderQty
   FROM Sales.SalesOrderDetail
      GROUP BY ProductID
      ORDER BY ProductID
   ```

3. Verify that the Columnstore Index was used by looking up the object_id for the Columnstore index and confirming that it appears in the usage stats for the SalesOrderDetail table:

   ```sql
   SELECT * FROM sys.indexes WHERE name = 'IX_SalesOrderDetail_ColumnStore'
   GO

   SELECT * 
   FROM sys.dm_db_index_usage_stats
      WHERE database_id = DB_ID('AdventureWorks')
      AND object_id = OBJECT_ID('AdventureWorks.Sales.SalesOrderDetail');
   ```
   
## Use In-Memory OLTP
SQL Server provides In-Memory OLTP features that can greatly improve the performance of application systems.  This section of the Evaluation Guide will walk you through the steps to create a memory-optimized table stored in memory and a natively compiled stored procedure that can access the table without needing to be compiled or interpreted.

### Configure Database for In-Memory OLTP
1. It's recommended to set the database to a compatibility level of at least 130 to use In-Memory OLTP.  Use the following query to check the current compatibility level of AdventureWorks:  

   ```sql
   USE AdventureWorks
   GO
   SELECT d.compatibility_level
   FROM sys.databases as d
     WHERE d.name = Db_Name();
   GO
   ```
   
   If necessary, update the level to 130:

   ```sql
   ALTER DATABASE CURRENT
   SET COMPATIBILITY_LEVEL = 130;
   GO
   ```

2. When a transaction involves both a disk-based table and a memory-optimized table, it's essential that the memory-optimized portion of the transaction operate at the transaction isolation level named SNAPSHOT.  To reliably enforce this level for memory-optimized tables in a cross-container transaction, execute the following:

   ```sql
   ALTER DATABASE CURRENT SET MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT=ON
   GO
   ```

3. Before you can create a memory-optimized table you must first create a Memory Optimized FILEGROUP and a container for data files:

   ```sql
   ALTER DATABASE AdventureWorks ADD FILEGROUP AdventureWorks_mod CONTAINS memory_optimized_data
   GO  
   ALTER DATABASE AdventureWorks ADD FILE (NAME='AdventureWorks_mod', FILENAME='/var/opt/mssql/data/AdventureWorks_mod') TO FILEGROUP AdventureWorks_mod
   GO
   ```

### Create a Memory-Optimized Table
The primary store for memory-optimized tables is main memory and so unlike disk-based tables, data does not need to be read in from disk into memory buffers.  To create a memory-optimized table, use the MEMORY_OPTIMIZED = ON clause.

1. Execute the following query to create the memory-optimized table dbo.ShoppingCart.  As a default, the data will be persisted on disk for durability purposes (Note that DURABILITY can also be set to persist the schema only). 

   ```sql
   CREATE TABLE dbo.ShoppingCart ( 
   ShoppingCartId INT IDENTITY(1,1) PRIMARY KEY NONCLUSTERED,
   UserId INT NOT NULL INDEX ix_UserId NONCLUSTERED HASH WITH (BUCKET_COUNT=1000000), 
   CreatedDate DATETIME2 NOT NULL, 
   TotalPrice MONEY
   ) WITH (MEMORY_OPTIMIZED=ON) 
   GO
   ```

2. Insert some records into the table:

   ```sql
   INSERT dbo.ShoppingCart VALUES (8798, SYSDATETIME(), NULL) 
   INSERT dbo.ShoppingCart VALUES (23, SYSDATETIME(), 45.4) 
   INSERT dbo.ShoppingCart VALUES (80, SYSDATETIME(), NULL) 
   INSERT dbo.ShoppingCart VALUES (342, SYSDATETIME(), 65.4) 
   ```

### Natively compiled Stored Procedure
SQL Server supports natively compiled stored procedures that access memory-optimized tables. The T-SQL statements are compiled to machine code and stored as native DLLs, enabling faster data access and more efficient query execution than traditional T-SQL.   Stored procedures that are marked with NATIVE_COMPILATION are natively compiled. 

1. Execute the following script to create a natively compiled stored procedure that inserts a large number of records into the ShoppingCart table:


   ```sql
   CREATE PROCEDURE dbo.usp_InsertSampleCarts @InsertCount int 
    WITH NATIVE_COMPILATION, SCHEMABINDING AS 
   BEGIN ATOMIC 
   WITH (TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE = N'us_english')

   DECLARE @i int = 0

   WHILE @i < @InsertCount 
    BEGIN 
     INSERT INTO dbo.ShoppingCart VALUES (1, SYSDATETIME() , NULL) 
     SET @i += 1 
    END
   END 
   ```
2. Insert 1,000,000 rows:

   ```sql
   EXEC usp_InsertSampleCarts 1000000 
   ```

3. Verify the rows have been inserted:

   ```sql
   SELECT COUNT(*) FROM dbo.ShoppingCart 
   ```

### Learn More About In-Memory OLTP
For more information about In-Memory OLTP, see the following topics:

- [Quick Start 1: In-Memory OLTP Technologies for Faster Transact-SQL Performance](../relational-databases/in-memory-oltp/survey-of-initial-areas-in-in-memory-oltp.md)
- [Migrating to In-Memory OLTP](../relational-databases/in-memory-oltp/migrating-to-in-memory-oltp.md)
- [Faster temp table and table variable by using memory optimization](../relational-databases/in-memory-oltp/faster-temp-table-and-table-variable-by-using-memory-optimization.md)
- [Monitor and Troubleshoot Memory Usage](../relational-databases/in-memory-oltp/monitor-and-troubleshoot-memory-usage.md)
- [In-Memory OLTP (In-Memory Optimization)](../relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)

## Use Query Store
Query Store collects detailed performance information about queries, execution plans, and runtime statistics.

Query Store is not active by default and can be enabled with ALTER DATABASE:

   ```sql
   ALTER DATABASE AdventureWorks SET QUERY_STORE = ON;
   ```

Run the following query to return information about queries and plans in the query store: 

   ```sql
   SELECT Txt.query_text_id, Txt.query_sql_text, Pl.plan_id, Qry.*
   FROM sys.query_store_plan AS Pl
      JOIN sys.query_store_query AS Qry
         ON Pl.query_id = Qry.query_id
      JOIN sys.query_store_query_text AS Txt
         ON Qry.query_text_id = Txt.query_text_id ;
   ```

## Query Dynamic Management Views
Dynamic management views return server state information that can be used to monitor the health of a server instance, diagnose problems, and tune performance.

To query the dm_os_wait stats dynamic management view:

   ```sql
   SELECT wait_type, wait_time_ms
   FROM sys.dm_os_wait_stats;
   ```
