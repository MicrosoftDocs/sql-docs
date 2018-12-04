---
title: "Demonstration: Performance Improvement of In-Memory OLTP | Microsoft Docs"
ms.custom: ""
ms.date: "08/19/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: c6def45d-d2d4-4d24-8068-fab4cd94d8cc
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Demonstration: Performance Improvement of In-Memory OLTP
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  The code sample in this topic demonstrates the fast performance of memory-optimized tables. The performance improvement is evident when data in a memory-optimized table is accessed from traditional, interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)]. This performance improvement is even greater when data in a memory-optimized table is accessed from a natively compiled stored procedure (NCSProc).  
 
To see a more comprehensive demonstration of the potential performance improvements of In-Memory OLTP see [In-Memory OLTP Performance Demo v1.0](https://github.com/Microsoft/sql-server-samples/releases/tag/in-memory-oltp-demo-v1.0). 
  
 The code example in the present article is single-threaded, and it does not take advantage of the concurrency benefits of In-Memory OLTP. A workload that uses concurrency will see a greater performance gain. The code example shows only one aspect of performance improvement, namely data access efficiency for INSERT.  
  
 The performance improvement offered by memory-optimized tables is fully realized when data in a memory-optimized table is accessed from a NCSProc.  
  
## Code Example  
 The following subsections describe each step.  
  
### Step 1a: Prerequisite If Using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 The steps in this first subsection applies only if you are running in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and does not apply if you are running in [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)]. Do the following:  
  
1.  Use SQL Server Management Studio (SSMS.exe) to connect to your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Or any tool similar to SSMS.exe is fine.  
  
2.  Manually create a directory named **C:\data\\**. The sample Transact-SQL code expects the directory to pre-exist.  
  
3.  Run the short T-SQL to create the database and its memory-optimized filegroup.  
  
```sql  
go  
CREATE DATABASE imoltp;    --  Transact-SQL  
go  
  
ALTER DATABASE imoltp ADD FILEGROUP [imoltp_mod]  
    CONTAINS MEMORY_OPTIMIZED_DATA;  
  
ALTER DATABASE imoltp ADD FILE  
    (name = [imoltp_dir], filename= 'c:\data\imoltp_dir')  
    TO FILEGROUP imoltp_mod;  
go  
  
USE imoltp;  
go  
```  
  
### Step 1b: Prerequisite If Using [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)]  
 This subsection applies only if you are using [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)]. Do the following:  
  
1.  Decide which existing test database you will use for the code example.  
  
2.  If you decide to create a new test database, use the [Azure portal](https://portal.azure.com) to create a database named **imoltp**.  
  
 If you would like instructions for using the Azure portal for this, see [Get Started with Azure SQL Database](https://azure.microsoft.com/documentation/articles/sql-database-get-started).  
  
### Step 2: Create Memory-Optimized Tables, and NCSProc  
 This step creates memory-optimized tables, and a natively compiled stored procedure (NCSProc). Do the following:  
  
1.  Use SSMS.exe to connect to your new database.  
  
2.  Run the following T-SQL in your database.  
  
```sql  
go  
DROP PROCEDURE IF EXISTS ncsp;  
DROP TABLE IF EXISTS sql;  
DROP TABLE IF EXISTS hash_i;  
DROP TABLE IF EXISTS hash_c;  
go  
  
CREATE TABLE [dbo].[sql] (  
  c1 INT NOT NULL PRIMARY KEY,  
  c2 NCHAR(48) NOT NULL  
);  
go  
  
CREATE TABLE [dbo].[hash_i] (  
  c1 INT NOT NULL PRIMARY KEY NONCLUSTERED HASH WITH (BUCKET_COUNT=1000000),  
  c2 NCHAR(48) NOT NULL  
) WITH (MEMORY_OPTIMIZED=ON, DURABILITY = SCHEMA_AND_DATA);  
go  
  
CREATE TABLE [dbo].[hash_c] (  
  c1 INT NOT NULL PRIMARY KEY NONCLUSTERED HASH WITH (BUCKET_COUNT=1000000),  
  c2 NCHAR(48) NOT NULL  
) WITH (MEMORY_OPTIMIZED=ON, DURABILITY = SCHEMA_AND_DATA);  
go  
  
CREATE PROCEDURE ncsp  
    @rowcount INT,  
    @c NCHAR(48)  
  WITH NATIVE_COMPILATION, SCHEMABINDING, EXECUTE AS OWNER  
  AS   
  BEGIN ATOMIC   
  WITH (TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE = N'us_english')  
  DECLARE @i INT = 1;  
  WHILE @i <= @rowcount  
  BEGIN;  
    INSERT INTO [dbo].[hash_c] VALUES (@i, @c);  
    SET @i += 1;  
  END;  
END;  
go  
```  
  
### Step 3: Run the Code  
 Now you can execute the queries that will demonstrate the performance of memory-optimized tables. Do the following:  
  
1.  Use SSMS.exe to run the following T-SQL in your database.  
  
     Ignore any speed or other performance data this first run generates. The first run ensure several one-time only operations are performed, such as initial allocations of memory.  
  
2.  Again, use SSMS.exe to rerun the following T-SQL in your database.  
  
```sql  
go  
SET STATISTICS TIME OFF;  
SET NOCOUNT ON;  
  
-- Inserts, one at a time.  
  
DECLARE @starttime DATETIME2 = sysdatetime();  
DECLARE @timems INT;  
DECLARE @i INT = 1;  
DECLARE @rowcount INT = 100000;  
DECLARE @c NCHAR(48) = N'12345678901234567890123456789012345678';  
  
-- Harddrive-based table and interpreted Transact-SQL.  
  
BEGIN TRAN;  
  WHILE @i <= @rowcount  
  BEGIN;  
    INSERT INTO [dbo].[sql] VALUES (@i, @c);  
    SET @i += 1;  
  END;  
COMMIT;  
  
SET @timems = datediff(ms, @starttime, sysdatetime());  
SELECT 'A: Disk-based table and interpreted Transact-SQL: '  
    + cast(@timems AS VARCHAR(10)) + ' ms';  
  
-- Interop Hash.  
  
SET @i = 1;  
SET @starttime = sysdatetime();  
  
BEGIN TRAN;  
  WHILE @i <= @rowcount  
    BEGIN;  
      INSERT INTO [dbo].[hash_i] VALUES (@i, @c);  
      SET @i += 1;  
    END;  
COMMIT;  
  
SET @timems = datediff(ms, @starttime, sysdatetime());  
SELECT 'B: memory-optimized table with hash index and interpreted Transact-SQL: '  
    + cast(@timems as VARCHAR(10)) + ' ms';  
  
-- Compiled Hash.  
  
SET @starttime = sysdatetime();  
  
EXECUTE ncsp @rowcount, @c;  
  
SET @timems = datediff(ms, @starttime, sysdatetime());  
SELECT 'C: memory-optimized table with hash index and native SP:'  
    + cast(@timems as varchar(10)) + ' ms';  
go  
  
DELETE sql;  
DELETE hash_i;  
DELETE hash_c;  
go  
```  
  
 Next are the output time statistics generated by our second test run.  
  
```sql  
10453 ms , A: Disk-based table and interpreted Transact-SQL.  
5626 ms , B: memory-optimized table with hash index and interpreted Transact-SQL.  
3937 ms , C: memory-optimized table with hash index and native SP.  
```  
  
## See Also  
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../../relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
  
