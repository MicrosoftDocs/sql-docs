---
title: Limitations and known issues for SSIS on Linux
description: This article describes limitations and known issues for SQL Server Integration Services (SSIS) on Linux computers
author: lrtoyou1223 
ms.author: lle 
ms.reviewer: maghan
ms.date: 06/06/2018
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
---
# Limitations and known issues for SSIS on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article describes limitations and known issues for SQL Server Integration Services (SSIS) on Linux.

## General limitations and known issues

The following features are not supported in this release of SSIS on Linux:
  - SSIS Catalog database
  - Scheduled package execution by SQL Agent
  - Windows Authentication
  - Third-party components
  - Change Data Capture (CDC)
  - SSIS Scale Out
  - Azure Feature Pack for SSIS
  - Hadoop and HDFS support
  - Microsoft Connector for SAP BW

For other limitations and known issues with SSIS on Linux, see the [Release Notes](sql-server-linux-release-notes-2017.md#ssis).

## <a name="components"></a> Supported and unsupported components

The following built-in Integration Services components are supported on Linux. Some of them have limitations on the Linux platform. Built-in components that are not listed here are not supported on Linux.

## Supported control flow tasks
- Bulk Insert Task
- Data Flow Task
- Data Profiling Task
- Execute SQL Task
- Execute T-SQL Statement Task
- Expression Task
- FTP Task
- Web Service Task
- XML Task

## Control flow tasks supported with limitations

| Task | Limitations |
|------------|---|
| Execute Process task | Only supports in-process mode. |
| File System task | The *Move directory* and *Set file attributes* actions are not supported. |
| Script task | Only supports standard .NET Framework APIs. |
| Send Mail task | Only supports anonymous user mode. |
| Transfer Database task | UNC paths are not supported. |

## Supported and unsupported maintenance plan tasks

In a SQL Server maintenance plan, you can typically use a variety of SSIS tasks.

The following maintenance plan tasks are not supported on Linux:
- Notify Operator
- Execute SQL Server Agent Job

The following maintenance plan tasks are supported on Linux:
- Check Database Integrity
- Shrink Database
- Reorganize Index
- Rebuild Index
- Update Statistics
- Clean Up History
- Back Up Database
- T-SQL Statement

## Supported control flow containers
- Sequence Container
- For Loop Container
- Foreach Loop Container

## Supported data flow sources and destinations
- Raw File source and destination
- XML Source

## Data flow sources and destinations supported with limitations

| Component | Limitations |
|------------|---|
| ADO.NET source and destination | Only support the SQLClient data provider. |
| Flat File source and destination | 1. Only support Windows-style file paths, to which the default path mapping rule is applied. For example `D:\home\ssis\travel.csv` becomes `/home/ssis/travel.csv`. 2. Azure File share that mounted on Red Hat 7 is not supported. |
| OData source | Only supports Basic authentication. |
| ODBC source and destination | Supports 64-bit Unicode ODBC drivers on Linux. Depends on the UnixODBC driver manager on Linux. |
| OLE DB source and destination | Only support SQL Server Native Client 11.0 and Microsoft OLE DB Provider for SQL Server. |

## Supported data flow transformations
- Aggregate
- Audit
- Balanced Data Distributor
- Character Map
- Conditional Split
- Copy Column
- Data Conversion
- Derived Column
- Export Column
- Fuzzy Grouping
- Fuzzy Lookup
- Import Column
- Lookup
- Merge
- Merge Join
- Multicast
- Pivot
- Row Count
- Slowly Changing Dimension
- Sort
- Term Lookup
- Union All
- Unpivot

## Data flow transformations supported with limitations

| Component | Limitations |
|------------|---|
| OLE DB Command transformation | Same limitations as the OLE DB source and destination. |
| Script component | Only supports standard .NET Framework APIs. |

## Supported and unsupported log providers
All the built-in SSIS log providers are supported on Linux except the Windows Event Log provider.

The SQL Server log provider supports only SQL Authentication; it does not support Windows Authentication.

The SSIS log providers for Text files, for XML files, and for SQL Server Profiler write their output to a file that you specify. The following considerations apply to the file path:
-   If you don't provide a path, the log provider writes to the current directory of the host. If the current user doesn't have permission to write to the current directory of the host, the log provider raises an error.
-   You can't use an environment variable in a file path. If you specify an environment variable, the literal text that you specify appears in the file path. For example, if you specify `%TMP%/log.txt`, the log  provider appends the literal text `/%TMP%/log.txt` to the current host directory.

## Related content about SSIS on Linux
-   [Extract, transform, and load data on Linux with SSIS](sql-server-linux-migrate-ssis.md)
-   [Install SQL Server Integration Services (SSIS) on Linux](sql-server-linux-setup-ssis.md)
-   [Configure SQL Server Integration Services on Linux with ssis-conf](sql-server-linux-configure-ssis.md)
-   [Schedule SQL Server Integration Services package execution on Linux with cron](sql-server-linux-schedule-ssis-packages.md)
