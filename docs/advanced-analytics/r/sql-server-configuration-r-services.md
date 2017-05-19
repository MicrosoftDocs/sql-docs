---
title: "SQL Server Configuration (R Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/10/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 4b08969f-b90b-46b3-98e7-0bf7734833fc
caps.latest.revision: 13
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# SQL Server Configuration (R Services)
The information in this section provides general guidance on the hardware and network configuration of the computer that is used to host [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)]. It should be considered in addition to the general [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] performance tuning information provided in the [Performance Center for SQL Server Database Engine and Azure SQL Database](../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md).

## Processor

[!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] can perform tasks in parallel by using the available cores on the machine; the more cores that are available, the better the performance. Since [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] is normally used by multiple users simultaneously, the database administrator should determine the ideal number of cores that are needed to support peak workload computations. While the number of cores may not help for IO bound operations, CPU bound algorithms will benefit from faster CPUs with many cores.

## Memory

The amount of memory available on the computer can have a large impact on the performance of advanced analytic algorithms. Insufficient memory may affect the degree of parallelism when using the SQL compute context. It can also affect the chunk size (rows per read operation) that can be processed, and the number of simultaneous sessions that can be supported.

A minimum of 32GB is highly recommended. If you have more than 32GB available, you can configure the SQL data source to use more rows in every read operation to improve performance.

## Power Options

On the Windows operating system, the __High Performance__ power option should be used. Using a different power setting will result in decreased or inconsistent performance when using [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)].

## Disk IO

Training and prediction jobs using [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] are inherently IO bound, and depend on the speed of the disk(s) that the database is stored on. Faster drives, such as solid state drives (SSD) may help. 

Disk IO is also affected by other applications accessing the disk: for example, read operations against a database by other clients. Disk IO performance can also be affected by settings on the file system in use, such as the block size used by the file system. If multiple drives are available, store the databases on a different drive than [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] so that requests for [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] are not hitting the same disk as requests for data stored in the database.

Disk IO can also greatly impact performance when running RevoScaleR analytic functions that use multiple iterations during training. For example, `rxLogit`, `rxDTree`, `rxDForest` and `rxBTrees` all use multiple iterations. When the data source is [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], these algorithms use temporary files that are optimized to capture the data. These files are automatically cleaned up after the session completes. Having a high performance disk for read/write operations can significantly improve the overall elapsed time for these algorithms.

> [!NOTE]
> [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] requires 8.3 filename support on Windows operating systems. You can use fsutil.exe to determine whether a drive supports 8.3 filenames, or to enable support if it does not. For more information on using fsutil.exe with 8.3 filenames, see [Fsutil 8dot3name](https://technet.microsoft.com/library/ff621566(v=ws.11).aspx).

### Table Compression

IO performance can often be improved by using either compression or columstore indexes. Generally, data is often repeated in several columns within a table, so using a columnstore index takes advantage of these repetitions when compressing the data.

A columnstore index might not be as efficient if there are a lot of insertions into the table, but is a good choice if the data is static or only changes infrequently. If a columnar store is not appropriate, enabling compression on a row major table can be used to improve IO.

For more information, see the following documents:

* [Data Compression](../../relational-databases/data-compression/data-compression.md)

* [Enable Compression on a Table or Index](../../relational-databases/data-compression/enable-compression-on-a-table-or-index.md)

* [Columnstore Indexes Guide](https://msdn.microsoft.com/library/gg492088.aspx)

## Paging File

The Windows operating system uses a paging file to manage crash dumps and for storing virtual memory pages. If you notice excessive paging, consider increasing the physical memory on the machine. Although having more physical memory does not eliminate paging, it does reduce the need for paging.

The speed of the disk that the page file is stored on can also affect performance. Storing the page file on an SSD, or using multiple page files across multiple SSDs, can improve performance.

See [How to determine the appropriate page file size for 64-bit versions of Windows](https://support.microsoft.com/en-us/kb/2860880) for information on sizing the page file.

## Resource Governance

[!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] supports resource governance for controlling the various resources used by [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)]. For example, the default value for memory consumption by R is limited to 20% of the total memory available for [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)]. This is done to ensure that [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] workflows are not severely affected by long running R jobs. However, these limits can be changed by the database administrator. 

The resources limited are __MAX_CPU_PERCENT__, __MAX_MEMORY_PERCENT__, and __MAX_PROCESSES__. To view the current settings, use this [!INCLUDE[tsql_md](../../includes/tsql-md.md)] statement:

```T-SQL
SELECT * FROM sys.resource_governor_external_resource_pools
``` 

If [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] is primarily used for R Services, it might be helpful to increase MAX_CPU_PERCENT to 40% or 60%. If there many R sessions using the same [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] at the same time, all three will be increased. To change the allocated resource values, use [!INCLUDE[tsql_md](../../includes/tsql-md.md)] statements. 

This example sets the memory usage to 40%:

```T-SQL
ALTER EXTERNAL RESOURCE POOL [default] WITH (MAX_MEMORY_PERCENT = 40)
```
The following example sets all three configurable values:
```T-SQL
ALTER EXTERNAL RESOURCE POOL [default] WITH (MAX_CPU_PERCENT = 40, MAX_MEMORY_PERCENT = 50, MAX_PROCESSES = 20)`
``` 

> [!NOTE]
> To make changes to these settings take effect immediately, run the statement `ALTER RESOURCE GOVERNOR RECONFIGURE` after changing a memory, CPU, or max process setting. 

## See Also
[Resource Governor](../../relational-databases/resource-governor/resource-governor.md)

[CREATE EXTERNAL RESOURCE POOL](../../t-sql/statements/create-external-resource-pool-transact-sql.md)

 [SQL Server R Services Performance Tuning Guide](../../advanced-analytics/r-services/sql-server-r-services-performance-tuning.md)
 
 
 [Performance Case Study](../../advanced-analytics/r-services/performance-case-study-r-services.md)
 
 [R and Data Optimization](../../advanced-analytics/r-services/r-and-data-optimization-r-services.md)
