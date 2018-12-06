---
title: "Faster temp table and table variable by using memory optimization | Microsoft Docs"
ms.custom: ""
ms.date: "06/01/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 38512a22-7e63-436f-9c13-dde7cf5c2202
author: Jodebrui
ms.author: jodebrui
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Faster temp table and table variable by using memory optimization
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  
If you use temporary tables, table variables, or table-valued parameters, consider conversions of them to leverage memory-optimized tables and table variables to improve performance. The code changes are usually minimal.  
  
This article describes:  
  
- Scenarios which argue in favor of conversion to In-Memory.  
- Technical steps for implementing the conversions to In-Memory.  
- Prerequisites before conversion to In-Memory.  
- A code sample that highlights the performance benefits of memory-optimization
  
  
## A. Basics of memory-optimized table variables  
  
A memory-optimized table variable provides great efficiency by using the same memory-optimized algorithm and data structures that are used by memory-optimized tables. The efficiency is maximized when the table variable is accessed from within a natively compiled module.  
  
  
A memory-optimized table variable:  
  
- Is stored only in memory, and has no component on disk.  
- Involves no IO activity.  
- Involves no tempdb utilization or contention.  
- Can be passed into a stored proc as a table-valued parameter (TVP).  
- Must have at least one index, either hash or nonclustered.  
  - For a hash index, the bucket count should ideally be 1-2 times the number of expected unique index keys, but overestimating bucket count is usually fine (up to 10X). For details see [Indexes for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/indexes-for-memory-optimized-tables.md).  

  
  
#### Object types  
  
In-Memory OLTP provides the following objects that can be used for memory-optimizing temp tables and table variables:  
  
- Memory-optimized tables  
  - Durability = SCHEMA_ONLY  
- Memory-optimized table variables  
  - Must be declared in two steps (rather than inline):  
    - `CREATE TYPE my_type AS TABLE ...;` , then  
    - `DECLARE @mytablevariable my_type;`.  
  
  
## B. Scenario: Replace global tempdb &#x23;&#x23;table  
  
Replacing a global temporary table with a memory-optimized SCHEMA_ONLY table is fairly straightforward. The biggest change is to create the table at deployment time, not at runtime. Creation of memory-optimized tables takes longer than creation of traditional tables, due to the compile-time optimizations. Creating and dropping memory-optimized tables as part of the online workload would impact the performance of the workload, as well as the performance of redo on AlwaysOn secondaries and database recovery.

Suppose you have the following global temporary table.  
  
  
```sql
CREATE TABLE ##tempGlobalB  
    (  
        Column1   INT   NOT NULL ,  
        Column2   NVARCHAR(4000)  
    );  
```
  
  
  
Consider replacing the global temporary table with the following memory-optimized table that has DURABILITY = SCHEMA_ONLY.  
  
  
  
```sql
CREATE TABLE dbo.soGlobalB  
(  
    Column1   INT   NOT NULL   INDEX ix1 NONCLUSTERED,  
    Column2   NVARCHAR(4000)  
)  
    WITH  
        (MEMORY_OPTIMIZED = ON,  
        DURABILITY        = SCHEMA_ONLY);  
```  
  
  
  
#### B.1 Steps  
  
The conversion from global temporary to SCHEMA_ONLY is the following steps:  
  
  
1. Create the **dbo.soGlobalB** table, one time, just as you would any traditional on-disk table.  
2. From your Transact-SQL, remove the create of the **&#x23;&#x23;tempGlobalB** table.  It is important to create the memory-optimized table at deployment time, not at runtime, to avoid the compilation overhead that comes with table creation.
3. In your T-SQL, replace all mentions of **&#x23;&#x23;tempGlobalB** with **dbo.soGlobalB**.  
  
  
## C. Scenario: Replace session tempdb &#x23;table  
  
The preparations for replacing a session temporary table involve more T-SQL than for the earlier global temporary table scenario. Happily the extra T-SQL does not mean any more effort is needed to accomplish the conversion.  

As with the global temp table scenario, the biggest change is to create the table at deployment time, not runtime, to avoid the compilation overhead.
  
Suppose you have the following session temporary table.  
  
  
  
```sql  
CREATE TABLE #tempSessionC  
(  
    Column1   INT   NOT NULL ,  
    Column2   NVARCHAR(4000)  
);  
```
  
  
  
First, create the following table-value function to filter on **@@spid**. The function will be usable by all SCHEMA_ONLY tables that you convert from session temporary tables.  
  
  
  
```sql  
CREATE FUNCTION dbo.fn_SpidFilter(@SpidFilter smallint)  
    RETURNS TABLE  
    WITH SCHEMABINDING , NATIVE_COMPILATION  
AS  
    RETURN  
        SELECT 1 AS fn_SpidFilter  
            WHERE @SpidFilter = @@spid;  
```
  
  
  
Second, create the SCHEMA_ONLY table, plus a security policy on the table.  
  
  
Note that each memory-optimized table must have at least one index.  
  
- For table dbo.soSessionC a HASH index might be better, if we calculate the appropriate BUCKET_COUNT. But for this sample we simplify to a NONCLUSTERED index.  
  
  
  
```sql
CREATE TABLE dbo.soSessionC  
(  
    Column1     INT         NOT NULL,  
    Column2     NVARCHAR(4000)  NULL,  

    SpidFilter  SMALLINT    NOT NULL   DEFAULT (@@spid),  

    INDEX ix_SpidFiler NONCLUSTERED (SpidFilter),  
    --INDEX ix_SpidFilter HASH  
    --    (SpidFilter) WITH (BUCKET_COUNT = 64),  
        
    CONSTRAINT CHK_soSessionC_SpidFilter  
        CHECK ( SpidFilter = @@spid ),  
)  
    WITH  
        (MEMORY_OPTIMIZED = ON,  
            DURABILITY = SCHEMA_ONLY);  
go  
  
  
CREATE SECURITY POLICY dbo.soSessionC_SpidFilter_Policy  
    ADD FILTER PREDICATE dbo.fn_SpidFilter(SpidFilter)  
    ON dbo.soSessionC  
    WITH (STATE = ON);  
go  
```  
  
  
  
Third, in your general T-SQL code:  
  
1. Change all references to the temp table in your Transact-SQL statements to the new memory-optimized table:
    - _Old:_ &#x23;tempSessionC  
    - _New:_ dbo.soSessionC  
2. Replace the `CREATE TABLE #tempSessionC` statements in your code with `DELETE FROM dbo.soSessionC`, to ensure a session is not exposed to table contents inserted by a previous session with the same session_id. It is important to create the memory-optimized table at deployment time, not at runtime, to avoid the compilation overhead that comes with table creation.
3. Remove the `DROP TABLE #tempSessionC` statements from your code - optionally you can insert a `DELETE FROM dbo.soSessionC` statement, in case memory size is a potential concern
  
  
  
  
## D. Scenario: Table variable can be MEMORY_OPTIMIZED=ON  
  
  
A traditional table variable represents a table in the tempdb database. For much faster performance you can memory-optimize your table variable.  
  
Here is the T-SQL for a traditional table variable. Its scope ends when either the batch or the session ends.  
  
  
  
```sql
DECLARE @tvTableD TABLE  
    ( Column1   INT   NOT NULL ,  
      Column2   CHAR(10) );  
```
  
  
  
#### D.1 Convert inline to explicit  
  
The preceding syntax is said to create the table variable *inline*. The inline syntax does not support memory-optimization. So let us convert the inline syntax to the explicit syntax for the TYPE.  
  
*Scope:* The TYPE definition created by the first go-delimited batch persists even after the server is shutdown and restarted. But after the first go delimiter, the declared table @tvTableC persists only until the next go is reached and the batch ends.  
  
  
  
```sql  
CREATE TYPE dbo.typeTableD  
    AS TABLE  
    (  
        Column1  INT   NOT NULL ,  
        Column2  CHAR(10)  
    );  
go  
        
SET NoCount ON;  
DECLARE @tvTableD dbo.typeTableD  
;  
INSERT INTO @tvTableD (Column1) values (1), (2)  
;  
SELECT * from @tvTableD;  
go  
```
  
  
  
#### D.2 Convert explicit on-disk to memory-optimized  
  
A memory-optimized table variable does not reside in tempdb. Memory-optimization results in speed increases that are often 10 times faster or more.  
  
The conversion to memory-optimized is achieved in only one step. Enhance the explicit TYPE creation to be the following, which adds:  
  
- An index. Again, each memory-optimized table must have at least one index.  
- MEMORY_OPTIMIZED = ON.  
  
  
  
```sql
CREATE TYPE dbo.typeTableD  
    AS TABLE  
    (  
        Column1  INT   NOT NULL   INDEX ix1,  
        Column2  CHAR(10)  
    )  
    WITH  
        (MEMORY_OPTIMIZED = ON);  
```  
  
  
  
Done.  
  
  
## E. Prerequisite FILEGROUP for SQL Server  
  
On Microsoft SQL Server, to use memory-optimized features, your database must have a FILEGROUP that is declared with **MEMORY_OPTIMIZED_DATA**.  
  
- Azure SQL Database does not require creating this FILEGROUP.  
  
  
*Prerequisite:* The following Transact-SQL code for a FILEGROUP is a prerequisite for the long T-SQL code samples in later sections of this article.  
  
1. You must use SSMS.exe or another tool that can submit T-SQL.  
2. Paste the sample FILEGROUP T-SQL code into SSMS.  
3. Edit the T-SQL to change its specific names and directory paths to your liking.  
  - All directories in the FILENAME value must preexist, except the final directory must not preexist.  
4. Run your edited T-SQL.  
  - There is no need to run the FILEGROUP T-SQL more than one time, even if you repeatedly adjust and rerun the speed comparison T-SQL in the next subsection.  
  
  
  
 
```sql
ALTER DATABASE InMemTest2  
    ADD FILEGROUP FgMemOptim3  
        CONTAINS MEMORY_OPTIMIZED_DATA;  
go  
ALTER DATABASE InMemTest2  
    ADD FILE  
    (  
        NAME = N'FileMemOptim3a',  
        FILENAME = N'C:\DATA\FileMemOptim3a'  
                    --  C:\DATA\    preexisted.  
    )  
    TO FILEGROUP FgMemOptim3;  
go  
```  


The following script creates the filegroup for you and configures recommended database settings: [enable-in-memory-oltp.sql](https://raw.githubusercontent.com/Microsoft/sql-server-samples/master/samples/features/in-memory/t-sql-scripts/enable-in-memory-oltp.sql)
  
For more information about `ALTER DATABASE ... ADD` for FILE and FILEGROUP, see:  
  
- [ALTER DATABASE File and Filegroup Options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md)  
- [The Memory Optimized Filegroup](../../relational-databases/in-memory-oltp/the-memory-optimized-filegroup.md)    
  
  
## F. Quick test to prove speed improvement  
  
  
This section provides Transact-SQL code that you can run to test and compare the speed gain for INSERT-DELETE from using a memory-optimized table variable. The code is composed of two halves that are nearly the same, except in the first half the table type is memory-optimized.  
  
The comparison test lasts about 7 seconds. To run the sample:  
  
1. *Prerequisite:* You must already have run the FILEGROUP T-SQL from the previous section.  
2. Run the following T-SQL INSERT-DELETE script.  
  - Notice the 'GO 5001' statement, which resubmits the T-SQL 5001 times. You can adjust the number and rerun.  
  
When running the script in an Azure SQL Database, make sure to run from a VM in the same region.

  
```sql
PRINT ' ';  
PRINT '---- Next, memory-optimized, faster. ----';  

DROP TYPE IF EXISTS dbo.typeTableC_mem;  
go  
CREATE TYPE dbo.typeTableC_mem  -- !!  Memory-optimized.  
        AS TABLE  
        (  
            Column1  INT NOT NULL INDEX ix1,  
            Column2  CHAR(10)  
        )  
        WITH  
            (MEMORY_OPTIMIZED = ON);  
go  
DECLARE @dateString_Begin nvarchar(64) =  
    Convert(nvarchar(64), GetUtcDate(), 121);  
PRINT Concat(@dateString_Begin, '  = Begin time, _mem.');  
go  
SET NoCount ON;  
DECLARE @tvTableC dbo.typeTableC_mem;  -- !!  

INSERT INTO @tvTableC (Column1) values (1), (2);  
INSERT INTO @tvTableC (Column1) values (3), (4);  
DELETE @tvTableC;  

GO 5001  

DECLARE @dateString_End nvarchar(64) =  
    Convert(nvarchar(64), GetUtcDate(), 121);  
PRINT Concat(@dateString_End, '  = End time, _mem.');  
go  
DROP TYPE IF EXISTS dbo.typeTableC_mem;  
go  

---- End memory-optimized.  
-------------------------------------------------  
---- Start traditional on-disk.  

PRINT ' ';  
PRINT '---- Next, tempdb based, slower. ----';  

DROP TYPE IF EXISTS dbo.typeTableC_tempdb;  
go  
CREATE TYPE dbo.typeTableC_tempdb  -- !!  Traditional tempdb.  
    AS TABLE  
    (  
        Column1  INT NOT NULL ,  
        Column2  CHAR(10)  
    );  
go  
DECLARE @dateString_Begin nvarchar(64) =  
    Convert(nvarchar(64), GetUtcDate(), 121);  
PRINT Concat(@dateString_Begin, '  = Begin time, _tempdb.');  
go  
SET NoCount ON;  
DECLARE @tvTableC dbo.typeTableC_tempdb;  -- !!  

INSERT INTO @tvTableC (Column1) values (1), (2);  
INSERT INTO @tvTableC (Column1) values (3), (4);  
DELETE @tvTableC;  

GO 5001  

DECLARE @dateString_End nvarchar(64) =  
    Convert(nvarchar(64), GetUtcDate(), 121);  
PRINT Concat(@dateString_End, '  = End time, _tempdb.');  
go  
DROP TYPE IF EXISTS dbo.typeTableC_tempdb;  
go  
----  

PRINT '---- Tests done. ----';  

go  

/*** Actual output, SQL Server 2016:  

---- Next, memory-optimized, faster. ----  
2016-04-20 00:26:58.033  = Begin time, _mem.  
Beginning execution loop  
Batch execution completed 5001 times.  
2016-04-20 00:26:58.733  = End time, _mem.  

---- Next, tempdb based, slower. ----  
2016-04-20 00:26:58.750  = Begin time, _tempdb.  
Beginning execution loop  
Batch execution completed 5001 times.  
2016-04-20 00:27:05.440  = End time, _tempdb.  
---- Tests done. ----  
***/
```
  
  
  
## G. Predict active memory consumption  
  
You can learn to predict the active memory needs of your memory-optimized tables with the following resources:  
  
- [Estimate Memory Requirements for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables.md)  
- [Table and Row Size in Memory-Optimized Tables: Example Calculation](../../relational-databases/in-memory-oltp/table-and-row-size-in-memory-optimized-tables.md)  
  
For larger table variables, nonclustered indexes use more memory than they do for memory-optimized *tables*. The larger the row count and the index key, the more the difference increases.  
  
If the memory-optimized table variable is accessed only with one exact key value per access, a hash index might be a better choice than a nonclustered index. However, if you cannot estimate the appropriate BUCKET_COUNT, a NONCLUSTERED index is a good second choice.  
  
## H. See also  
  
- [Memory-Optimized Tables.](../../relational-databases/in-memory-oltp/memory-optimized-tables.md)

- [Defining Durability for Memory-Optimized Objects.](../../relational-databases/in-memory-oltp/defining-durability-for-memory-optimized-objects.md)

- [Cumulative Update to eliminate chance of improper Out Of Memory errors, announced in blog September 2017.](https://support.microsoft.com/help/4025208/fix-memory-leak-occurs-when-you-use-memory-optimized-tables-in-microso)
    - [SQL Server 2016 build versions](https://support.microsoft.com/help/3177312/sql-server-2016-build-versions) provides full details of releases, service packs, and cumulative updates.
    - These occasional improper errors did not occur in the Enterprise edition of SQL Server.

