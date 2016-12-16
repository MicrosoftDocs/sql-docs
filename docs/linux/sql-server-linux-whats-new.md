---
# required metadata

title: What's New for SQL Server vNext on Linux - SQL Server vNext CTP 1.1 | Microsoft Docs
description: This topic highlights what's new for this release of SQL Server vNext on Linux.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 12/12/2016
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 456b6f31-6b97-4e31-80ab-b40151ec4868

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---
# What's new for SQL Server vNext on Linux

This topic describes what's new for SQL Server vNext running on Linux.

## CTP 1.1

The CTP 1.1 release contains the following improvements and fixes:
- Support for Red Hat Enterprise Linux version 7.3.
- Support for Ubuntu 16.10
- Upgraded Docker OS layer to Ubuntu 16.04.
- Fixed telemetry issues in Docker image.
- Fixed SQL Server Setup script related bugs
- Enhanced performance for natively compiled T-SQL modules, including:
    - `OPENJSON`, `FOR JSON`, `JSON` built-ins.
    - Computed Columns (Only indexes are allowed on persisted computed columns, but not on non-persisted computed columns for in-memory tables.)
    - `CROSS APPLY` operations.
- New language features:
    - String functions: `TRIM`, `CONCAT_WS`, `TRANSLATE` and `STRING_AGG` with support for `WITHIN GROUP (ORDER BY)`.
    - `BULK IMPORT` now supports CSV format and Azure Blob Storage as File Source.

Under compatibility mode 140:

- Improved the performance of updates to non-clustered columnstore indexes in the case when the row is in the delta store. Changed from delete and insert operations to update. Also changed the plan shape used from wide to narrow.
- Batch mode queries now support "memory grant feedback loops". This will improve concurrency and throughput on systems running repeated queries that use batch mode. This can allow more queries to run on systems that are otherwise blocking on memory before starting queries.
- Improved performance in batch mode parallelism by ignoring trivial plan for batch mode plans to allow for parallel plans to be picked instead against columnstores. 

[Improvements from Service Pack 1](https://blogs.msdn.microsoft.com/sqlreleaseservices/sql-server-2016-service-pack-1-sp1-released/) in this CTP1.1 release:
- Database cloning for CLR, Filestream/Filetable, In-memory and Query Store objects.
- `CREATE` or `ALTER` operators for programmability objects. 
- New `USE HINT` query option to provide hints for the query processor. Learn more here: [Query Hints](https://msdn.microsoft.com/en-us/library/ms181714.aspx).
- SQL service account can now programmatically identify Enable Lock Pages in Memory and Instant File Initialization permissions.
- Support for TempDB file count, file size and file growth settings. 
- Extended diagnostics in showplan XML.
- Lightweight per-operator query execution profiling.
- New Dynamic Management Function `sys.dm_exec_query_statistics_xml`.
- New Dynamic Management Function for incremental statistics. 
- Removed noisy In-memory related logging messages from errorlog.
- Improved AlwaysOn Latency Diagnostics.
- Cleaned up Manual Change Tracking.
- `DROP TABLE` support for replication.
- `BULK INSERT` into heaps with `AUTO TABLOCK` under TF 715.
- Parallel `INSERT..SELECT` changes for local temp tables.

Learn more about these fixes in the [Service Pack 1 Release description](https://blogs.msdn.microsoft.com/sqlreleaseservices/sql-server-2016-service-pack-1-sp1-released/).

Many database engine improvements apply to both Windows and Linux. The only exception would be for database engine features that are currently not supported on Linux. For more information, see [What's New in SQL Server vNext (Database Engine)](https://msdn.microsoft.com/library/mt775028).

## See also

For installation requirements, unsupported feature areas, and known issues, see [Release notes for SQL Server vNext on Linux](sql-server-linux-release-notes.md).
