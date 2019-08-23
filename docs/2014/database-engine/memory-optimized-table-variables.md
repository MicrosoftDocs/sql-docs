---
title: "Memory-Optimized Table Variables | Microsoft Docs"
ms.custom: ""
ms.date: "07/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: bd102e95-53e2-4da6-9b8b-0e4f02d286d3
author: stevestein
ms.author: sstein
manager: craigg
---
# Memory-Optimized Table Variables
  In addition to memory-optimized tables (for efficient data access) and natively compiled stored procedures (for efficient query processing and business logic execution) [!INCLUDE[hek_2](../includes/hek-2-md.md)] introduces a third kind of object: the memory-optimized table type. A table variable created using a memory-optimized table type is a memory-optimized table variable.  
  
 Memory-optimized table variables offer the following advantages when compared to disk-based table variables:  
  
-   The variables are only stored in memory. Data access is more efficient because memory-optimized table type use the same memory-optimized algorithm and data structures used for memory-optimized tables, especially when the variables are used in natively compiled stored procedures.  
  
-   With memory-optimized table variables, there is no tempdb utilization. Table variables are not stored in tempdb and do not use any resources in tempdb.  
  
 The typical usage scenarios for memory-optimized table variables are:  
  
-   Storing intermediate results and creating single result sets based on multiple queries in natively compiled stored procedures.  
  
-   Passing table-valued parameters into natively compiled stored procedures and interpreted stored procedures.  
  
-   Replacing disk-based table variables, and in some cases #temp tables that are local to a stored procedure. This is particularly useful if there is a lot of tempdb contention in the system.  
  
-   Table variables can be used to simulate cursors in natively compiled stored procedures, which can help you work around surface area limitations in natively compiled stored procedures.  
  
 Like memory-optimized tables, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] generates a DLL for each memory-optimized table type. (Compilation is invoked when the memory-optimized table type is created and not when used to create memory-optimized table variables.) This DLL includes the functions for accessing indexes and retrieving data from the table variables. When a memory-optimized table variable is declared based on the table type, an instance of the table and index structures corresponding to the table type is created in the user session. The table variable can then be used in the same way as disk-based table variables. You can insert, update, and delete rows in the table variable, and you can use the variables in [!INCLUDE[tsql](../includes/tsql-md.md)] queries. You can also pass the variables into natively compiled and interpreted stored procedures, as table-valued parameters (TVP).  
  
 The following sample shows a memory-optimized table type from the AdventureWorks-based In-Memory OLTP sample ([SQL Server 2014 In-Memory OLTP Sample](https://msftdbprodsamples.codeplex.com/releases/view/114491)).  
  
```sql
CREATE TYPE Sales.SalesOrderDetailType_inmem
   AS TABLE
(
   OrderQty         smallint   NOT NULL,
   ProductID        int        NOT NULL,

   SpecialOfferID   int        NOT NULL
      INDEX  IX_SpecialOfferID  NONCLUSTERED,

   LocalID          int        NOT NULL,

   INDEX IX_ProductID HASH (ProductID)
      WITH ( BUCKET_COUNT = 8 )
)
WITH ( MEMORY_OPTIMIZED = ON );
```  
  
 The sample shows that the syntax of memory-optimized table types is similar to disk-based table types, with the following exceptions:  
  
-   `MEMORY_OPTIMIZED=ON` indicates that the table type is memory-optimized.  
  
-   The type must have at least one index. As with memory-optimized tables, you can use hash and nonclustered indexes.  
  
     For a hash index, the bucket count should be about one to two times the number of expected unique index keys. For more information, see [Determining the Correct Bucket Count for Hash Indexes](../relational-databases/indexes/indexes.md).  
  
-   The data type and constraint restrictions on memory-optimized tables also apply to memory-optimized table types. For example, in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] default constraints are supported, but check constraints are not.  
  
 Like memory-optimized tables, memory-optimized table variables,  
  
-   Do not support parallel plans.  
  
-   Must fit in memory and do not use disk resources.  
  
 Disk-based table variables exist in tempdb. Memory-optimized table variables exist in the user database (but they do not consume storage and are not recovered).  
  
 You cannot create a memory-optimized table variable using in-line syntax. Unlike disk-based table variables, you must create a type first.  
  
## Table-Valued Parameters  
 The following sample script shows the declaration of a table variable as the memory-optimized table type `Sales.SalesOrderDetailType_inmem`, the insert of three rows into the variable, and passing the variable as a TVP into `Sales.usp_InsertSalesOrder_inmem`.  
  
```sql  
DECLARE @od Sales.SalesOrderDetailType_inmem,  
  @SalesOrderID uniqueidentifier,  
  @DueDate datetime2 = SYSDATETIME()  
  
INSERT @od (LocalID, ProductID, OrderQty, SpecialOfferID) VALUES  
  (1, 888, 2, 1),  
  (2, 450, 13, 1),  
  (3, 841, 1, 1)  
  
EXEC Sales.usp_InsertSalesOrder_inmem  
  @SalesOrderID = @SalesOrderID,  
  @DueDate = @DueDate,  
 @OnlineOrderFlag = 1,  
  @SalesOrderDetails = @od  
```  
  
 Memory-optimized table types can be used as the type for stored procedure table-valued parameters (TVPs) and can be referenced by clients exactly the same as disk-based table types and TVPs. Therefore, the invocation of stored procedures with memory-optimized TVPs, and natively compiled stored procedures works exactly the same as the invocation of interpreted stored procedures with disk-based TVPs.  
  
## #temp Table Replacement  
 The following sample shows memory-optimized table types and table variables as a replacement for #temp tables that are local to a stored procedure.  
  
```sql  
-- Using SQL procedure and temp table  
CREATE TABLE #tempTable (c INT NOT NULL PRIMARY KEY NONCLUSTERED)  
  
CREATE PROCEDURE sqlProc  
AS  
BEGIN  
  TRUNCATE TABLE #tempTable  
  
  INSERT #tempTable VALUES (1)  
  INSERT #tempTable VALUES (2)  
  INSERT #tempTable VALUES (3)  
  SELECT * FROM #tempTable  
END  
GO  
  
-- Using natively compiled stored procedure and table variable  
CREATE TYPE TT AS TABLE (c INT NOT NULL PRIMARY KEY NONCLUSTERED)  
GO  
  
CREATE PROCEDURE NCSPProc  
WITH NATIVE_COMPILATION, SCHEMABINDING, EXECUTE AS OWNER  
AS  
BEGIN ATOMIC WITH (TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE = N'us_english')  
  DECLARE @tableVariable TT  
  INSERT @tableVariable VALUES (1)  
  INSERT @tableVariable VALUES (2)  
  INSERT @tableVariable VALUES (3)  
  SELECT c FROM @tableVariable  
END  
GO  
```  
  
## Creating a Single Result Set  
 The following sample shows how to store intermediate results and create single result sets based on multiple queries in natively compiled stored procedures. The sample is computing the union `SELECT c1 FROM dbo.t1 UNION SELECT c1 FROM dbo.t2`.  
  
```sql  
CREATE DATABASE hk  
GO  
ALTER DATABASE hk ADD FILEGROUP hk_mod CONTAINS MEMORY_OPTIMIZED_DATA  
ALTER DATABASE hk ADD FILE( NAME = 'hk_mod' , FILENAME = 'c:\data\hk_mod') TO FILEGROUP hk_mod;  
  
USE hk  
GO  
  
CREATE TYPE tab1 AS TABLE (c1 INT NOT NULL, INDEX idx NONCLUSTERED(c1)) WITH (MEMORY_OPTIMIZED = ON)  
  
CREATE TABLE dbo.t1 (c1 INT NOT NULL, INDEX idx NONCLUSTERED(c1)) WITH (MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_ONLY)  
CREATE TABLE dbo.t2 (c1 INT NOT NULL, INDEX idx NONCLUSTERED(c1)) WITH (MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_ONLY)  
  
INSERT INTO dbo.t1 VALUES (1), (2)  
INSERT INTO dbo.t2 VALUES (3), (4)  
GO  
  
CREATE PROCEDURE dbo.p1  
  WITH NATIVE_COMPILATION, SCHEMABINDING, EXECUTE AS OWNER  
  AS  
  BEGIN ATOMIC WITH ( TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE = N'us_english' )  
  
    DECLARE @t dbo.tab1  
    INSERT @t (c1)  
    SELECT c1 FROM dbo.t1;  
  
    INSERT @t (c1)  
    SELECT c1 FROM dbo.t2;  
  
    SELECT c1 FROM @t;  
  END  
GO  
  
EXEC dbo.p1  
GO  
```  
  
## Memory Consumption for Table Variables  
 Memory consumption for table variables is similar to memory-optimized tables, with the exception of nonclustered indexes. If you insert a lot of rows into memory-optimized table variables with nonclustered indexes and if the index keys are large, these table variables will use a disproportionate amount of memory. Nonclustered indexes on large table variables require proportionately more memory than a nonclustered index would require for the same number of rows inserted into a table (more memory in the index pages).  
  
 Memory for table variables comes from the database's Resource Governor resource pool.  
  
 Unlike memory-optimized tables, the memory consumed (including deleted rows) by table variables is freed when the table variable goes out of scope.  
  
 Memory is accounted for as part of the single PGPOOL memory consumer of the database.  
  
## See Also  
 [Transact-SQL Support for In-Memory OLTP](../relational-databases/in-memory-oltp/transact-sql-support-for-in-memory-oltp.md)  
  
  
