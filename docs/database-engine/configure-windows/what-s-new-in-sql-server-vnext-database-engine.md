---
title: "What&#39;s New in SQL Server vNext (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: "sql-vnext"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 42f45b23-6509-45e8-8ee7-76a78f99a920
caps.latest.revision: 15
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# What&#39;s New in SQL Server vNext (Database Engine)
[!INCLUDE[tsql-appliesto-ssvNxt-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]

**Note:** SQL Server vNext also includes the features added in SQL Server 2016 service packs. For those items, see [What's New in SQL Server 2016 (Database Engine)](../../database-engine/configure-windows/what-s-new-in-sql-server-2016-database-engine.md).

## SQL Server Database Engine (CTP 1.4)
- There are no new Database Engine features in this CTP.
- This CTP contains bug fixes for the Database Engine.

## SQL Server Database Engine (CTP 1.3)
- Indirect checkpoint performance improvements.
- Cluster-less Availability Groups support added.
- Minimum Replica Commit Availability Groups setting added.
- Availability Groups can now work across Windows-Linux to enable cross-OS migrations and testing.
- Temporal Tables Retention Policy support added,
- New DMV SYS.DM_DB_STATS_HISTOGRAM
- Online non-clustered columnstore index buill and rebuild support added
- 5 new dynamic management views to return information about Linux process. For more information, see [Linux Process Dynamic Management Views](../../relational-databases/system-dynamic-management-views/linux-process-dynamic-management-views-transact-sql.md).   
- [sys.dm_db_stats_histogram (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-stats-histogram-transact-sql.md) is added for examining statistics.

## SQL Server Database Engine (CTP 1.2)

- The Database Tuning Advisor (DTA) released with SQL Server Management Studio version 16.4, when analyzing SQL Server 2016 and later, has additional options.    
   - Improved performance. For more information, see [Performance Improvements using Database Engine Tuning Advisor (DTA) recommendations](../../relational-databases/performance/performance-improvements-using-dta-recommendations.md).
   - The `-fc` option for allowing recommendations of columnstore indexes. For more information, see [DTA Utility](../../tools/dta/dta-utility.md) and [Columnstore index recommendations in Database Engine Tuning Advisor (DTA)](../../relational-databases/performance/columnstore-index-recommendations-in-database-engine-tuning-advisor-dta.md).  
   - The `-iq` option for allowing the DTA to review a workload from the Query Store. For more information, see [Tuning Database Using Workload from Query Store](../../relational-databases/performance/tuning-database-using-workload-from-query-store.md).
   

## SQL Server Database Engine (CTP 1.1)

<a name="InMemoryBulletsCtp11"/>
- For In-Memory functionality, additional enhancements to memory-optimized tables and natively compiled functions are listed next, and code samples are available in [subsequent text](#InMemory_CodeSamples):
    - Support for computed columns in memory-optimized tables, including indexes on computed columns.
    - Full support for JSON functions in natively compiled modules, and in check constraints.
    - `CROSS APPLY` operator in natively compiled modules.   
- New string functions [CONCAT_WS](../../t-sql/functions/concat-ws-transact-sql.md), [TRANSLATE](../../t-sql/functions/translate-transact-sql.md), and [TRIM](../../t-sql/functions/trim-transact-sql.md) are added.   
- The `WITHIN GROUP` clause is now supported for the [STRING_AGG](../../t-sql/functions/string-agg-transact-sql.md) function.
- Two new Japanese collation families (Japanese_Bushu_Kakusu_140 and Japanese_XJIS_140) were added, and the collation option Variation-selector-sensitive (_VSS) was added for use in Japanese collations. For more detail see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md)   
- New bulk access options ([BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) and [OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) ) enable access data directly from a file specified as CSV format, and from files stored in Azure Blob storage through the new `BLOB_STORAGE` option of [EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md).


## SQL Server Database Engine (CTP 1.0)

- Database **COMPATIBILITY_LEVEL** 140 has been added.   Customers running in this level will get the latest language features and query optimizer behaviors. This includes changes in each pre-release version Microsoft releases.
- Improvements to the way incremental statistics update thresholds are computed (140 compat mode required).
- [sys.dm_exec_query_statistics_xml](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-statistics-xml-transact-sql.md) is added.
- We have added many performance and language enhancements to In-Memory Tables:
    - `sp_spaceused` is now supported for in-memory tables.
    - `sp_rename` is now supported for native modules.
    - `CASE` statements are now supported for native modules.
    - The limitation of 8 indexes on in-memory tables has been removed.
    - `TOP (N) WITH TIES` is now supported in native modules.
    - `ALTER TABLE` against in-memory tables is now substantially faster in some cases.
    - Transaction redo In-memory tables is now done in parallel. This substantially reduces the time to do failovers or in some cases restarts.
    - In-memory checkpoint files can now be stored on Azure Storage. This provides equivalent capabilities to MDF compared to LDF files,  which already have this capability.
- Clustered Columnstore Indexes now support LOB columns (nvarchar(max), varchar(max), varbinary(max)).
- The [STRING_AGG](../../t-sql/functions/string-agg-transact-sql.md) aggregate function has been added.  
- New Permissions: `DATABASE SCOPED CREDENTIAL` is now a class of securable, supporting `CONTROL`, `ALTER`, `REFERENCES`, `TAKE OWNERSHIP`, and `VIEW DEFINITION` permissions. `ADMINISTER DATABASE BULK OPERATIONS` which is restricted to SQL Database is now visible in `sys.fn_builtin_permissions`.   
- The [sys.dm_os_host_info](../../relational-databases/system-dynamic-management-views/sys-dm-os-host-info-transact-sql.md) DMV is added to provide operating system information for both Windows and Linux.   
- The database roles are created with R Services for managing permissions associated with packages. For more information, see [R Package management for SQL Server](../../advanced-analytics/r-services/r-package-management-for-sql-server-r-services.md).
 
<a name="InMemory_CodeSamples"/> 
## Code Samples for new In-Memory Enhancements

The following subsections provide Transact-SQL code samples which illustrate new In-Memory features which bullet listed in preceding text in this article.

The CTP 1.1 bullet list for In-Memory is [here](#InMemoryBulletsCtp11).

#### Computed column in a memory-optimized table

This CREATE TABLE statement illustrates the following features which were mentioned in preceding text about CTP 1.1:

- JSON check constraint on a column.
- New computed columns.
- An index on a computed column.

```tsql
CREATE TABLE Product(
    ProductID  int            PRIMARY KEY NONCLUSTERED,
    Name       nvarchar(400)  NOT NULL,
    Price      float,

    Data      nvarchar(4000)  CONSTRAINT [Data contains JSON]  CHECK (ISJSON(Data)=1),

    MadeIn  AS CAST(JSON_VALUE(Data, '$.MadeIn')            as NVARCHAR(50)) PERSISTED,
    Cost    AS CAST(JSON_VALUE(Data, '$.ManufacturingCost') as float       ),

    INDEX [idx_Product_MadeIn] NONCLUSTERED (MadeIn)
)
        WITH (MEMORY_OPTIMIZED=ON);
```

#### CROSS APPLY, and JSON functions

This CREATE PROCEDURE statement, for a natively compiled stored procedure, illustrates the following features which were mentioned in preceding text about CTP 1.1:

- CROSS APPLY operator.
- JSON functions.

```tsql
CREATE OR ALTER PROCEDURE ProductList()
    WITH SCHEMABINDING, NATIVE_COMPILATION
as begin
    atomic with (transaction isolation level = snapshot,  language = N'English')

    SELECT
            ProductID, Name, Price, Tags,
            Data,
            JSON_VALUE(Data,'$.MadeIn') AS MadeIn,
            value
        FROM Product
        CROSS APPLY OPENJSON(Data, '$.SalesReasons')
        FOR JSON PATH
end;
```

