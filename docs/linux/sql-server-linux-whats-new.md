
---
# required metadata

title: What's New for SQL Server 2017 RC0 on Linux | Microsoft Docs
description: This topic highlights what's new for the current release of SQL Server 2017 on Linux.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 06/28/2017
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
# What's new for SQL Server 2017 on Linux

This topic describes what's new for SQL Server 2017 running on Linux.

## RC0

The RC0 release contains the following improvements and fixes:

- TBD.

## CTP 2.1

The CTP 2.1 release contains the following improvements and fixes:

- Added [environment variables to configure the SQL Server service](sql-server-linux-configure-environment-variables.md).
- [mssql-conf](sql-server-linux-configure-mssql-conf.md) now requires two-part naming convention for settings.
- The [mssql-scripter](https://github.com/Microsoft/sql-xplat-cli) tool. This utility enables developers, DBAs, and sysadmins to generate `CREATE` and `INSERT` Transact-SQL scripts from database objects in SQL Server, Azure SQL DB, and Azure SQL DW databases from the command line.
- The [DBFS tool](https://github.com/Microsoft/dbfs). This is an open-source tool that enables DBAs and sysadmins to monitor SQL Server more easily by exposing live data from SQL Server Dynamic Management Views (DMVs) as virtual files in a virtual directory on Linux operating systems.
- SQL Server Integration Services (SSIS) now runs on Linux. In addition, there's a new package that lets you run SSIS packages on Linux from the commandline. For more info, see the [blog post announcing SSIS support for Linux](https://blogs.msdn.microsoft.com/ssis/2017/05/17/ssis-helsinki-is-available-in-sql-server-vnext-ctp2-1/). With SSIS on Linux CTP 2.1 Refresh, your SSIS packages can use ODBC connections on Linux. For more info, see the [blog post announcing ODBC support on Linux](https://blogs.msdn.microsoft.com/ssis/2017/06/16/odbc-is-supported-in-ssis-on-linux-ssis-helsinki-ctp2-1-refresh/).

## CTP 2.0

The CTP 2.0 release contains the following improvements and fixes:

- Added **Log Shipping** functionality for SQL Server Agent.
- Localized messages of mssql-conf.
- Linux path formatting are now compatible throughout the SQL Server Engine. But support for "C:\\" prefixed paths will continue.
- Enabled DMV **sys.dm_os_file_exists**.
- Enabled DMV **sys.fn_trace_gettable**.
- Added [CLR Strict Security mode](/sql/database-engine/configure-windows/clr-strict-security).
- SQL Graph.
- Resumable Online Index Rebuilds.
- Adaptive Query Processing.
- Added UTF-8 encoding for system files, including log files.
- Fixed In-memory databases location limitation. 
- Add new cluster type `CLUSTER_TYPE = EXTERNAL` for configuring an availability group for high availability.
- Fix Availability Group Listener for read-only routing.
- Production support for Early Adoption Program (EAP) customers. Sign up [here](http://aka.ms/eapsignup).

## CTP 1.4

The CTP 1.4 release contains the following improvements and fixes:

- Enabled the [SQL Server Agent](sql-server-linux-setup-sql-agent.md).
  - Enabled T-SQL Jobs functionality.
- Fixed timezone bugs:
  - Timezone support for Asia/Kolkata.
  - Fixed GETDATE() function.
- Network Async I/0 Improvements:
  - Significant improvements to In-Memory OLTP workload performance.
- Docker image now includes SQL Server command-line utilities. (sqlcmd/bcp).
- Enabled Virtual Device Interface (VDI) support for backups.
- Location of TempDB can now be modified after installation using `ALTER DATABASE`.

## CTP 1.3

The CTP 1.3 release contains the following improvements and fixes:

- Enabled [Full-text Search](sql-server-linux-setup-full-text-search.md) feature.
- Enabled Always On [Availability Groups functionality](sql-server-linux-availability-group-overview.md) for High Availability.
- Additional functionality in **mssql-conf**:
  - First time set-up using **mssql-conf**. See the use of mssql-conf in the [installation guides](sql-server-linux-setup.md#platforms).
  - Enabling Availability Groups. See the [Availability Groups topic](sql-server-linux-availability-group-overview.md).
- Fixed native Linux path support for In-memory OLTP filegroups.
- Enabled dm_os_host_info DMV functionality.

## CTP 1.2

The CTP 1.2 release contains the following improvements and fixes:

- Support for [SUSE Linux Enterprise Server v12 SP2](sql-server-linux-setup-suse-linux-enterprise-server.md).
- Bug fixes for core engine and stability improvements.
- Docker image: 
  - Fixed [issue #1](https://github.com/Microsoft/mssql-docker/issues/1) by adding Python to the image.
  - Removed `/opt/mssql/data` as the default volume.
- Updated to .NET 4.6.2.

## CTP 1.1

The CTP 1.1 release contains the following improvements and fixes:

- Support for Red Hat Enterprise Linux version 7.3.
- Support for Ubuntu 16.10.
- Upgraded Docker OS layer to Ubuntu 16.04.
- Fixed telemetry issues in Docker image.
- Fixed SQL Server Setup script related bugs.
- Enhanced performance for natively compiled T-SQL modules, including:
  - **OPENJSON**, **FOR JSON**, **JSON** built-ins.
  - Computed Columns (Only indexes are allowed on persisted computed columns, but not on non-persisted computed columns for in-memory tables).
  - **CROSS APPLY** operations.
- New language features:
  - String functions: **TRIM**, **CONCAT_WS**, **TRANSLATE** and **STRING_AGG** with support for **WITHIN GROUP (ORDER BY)**.
  - **BULK IMPORT** now supports CSV format and Azure Blob Storage as File Source.

Under compatibility mode 140:

- Improved the performance of updates to non-clustered columnstore indexes in the case when the row is in the delta store. Changed from delete and insert operations to update. Also changed the plan shape used from wide to narrow.
- Batch mode queries now support "memory grant feedback loops". This will improve concurrency and throughput on systems running repeated queries that use batch mode. This can allow more queries to run on systems that are otherwise blocking on memory before starting queries.
- Improved performance in batch mode parallelism by ignoring trivial plan for batch mode plans to allow for parallel plans to be picked instead against columnstores. 

[Improvements from Service Pack 1](https://blogs.msdn.microsoft.com/sqlreleaseservices/sql-server-2016-service-pack-1-sp1-released/) in this CTP1.1 release:
- Database cloning for CLR, Filestream/Filetable, In-memory and Query Store objects.
- **CREATE** or **ALTER** operators for programmability objects.
- New **USE HINT** query option to provide hints for the query processor. Learn more here: [Query Hints](https://msdn.microsoft.com/en-us/library/ms181714.aspx).
- SQL service account can now programmatically identify Enable Lock Pages in Memory and Instant File Initialization permissions.
- Support for TempDB file count, file size and file growth settings.
- Extended diagnostics in showplan XML.
- Lightweight per-operator query execution profiling.
- New Dynamic Management Function **sys.dm_exec_query_statistics_xml**.
- New Dynamic Management Function for incremental statistics.
- Removed noisy In-memory related logging messages from errorlog.
- Improved AlwaysOn Latency Diagnostics.
- Cleaned up Manual Change Tracking.
- **DROP TABLE** support for replication.
- **BULK INSERT** into heaps with **AUTO TABLOCK** under TF 715.
- Parallel **INSERT..SELECT** changes for local temp tables.

Learn more about these fixes in the [Service Pack 1 Release description](https://blogs.msdn.microsoft.com/sqlreleaseservices/sql-server-2016-service-pack-1-sp1-released/).

Many database engine improvements apply to both Windows and Linux. The only exception would be for database engine features that are currently not supported on Linux. For more information, see [What's New in SQL Server 2017 (Database Engine)](https://msdn.microsoft.com/library/mt775028).

## See also

For installation requirements, unsupported feature areas, and known issues, see [Release notes for SQL Server 2017 on Linux](sql-server-linux-release-notes.md).
