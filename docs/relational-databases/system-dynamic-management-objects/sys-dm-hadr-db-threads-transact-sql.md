---
title: "sys.dm_hadr_db_threads (Transact-SQL)"
description: sys.dm_hadr_db_threads (Transact-SQL)
author: MashaMSFT
ms.author: mathoma
ms.reviewer: wiassaf, ryanston
ms.date: "02/27/2023"
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.dm_hadr_db_threads_TSQL"
  - "sys.dm_hadr_db_threads"
  - "dm_hadr_db_threads_TSQL"
  - "dm_hadr_db_threads"
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "Availability Groups [SQL Server], WSFC clusters"
  - "sys.dm_hadr_db_threads catalog view"
dev_langs:
  - "TSQL"
---
# sys.dm_hadr_db_threads (Transact-SQL)

The HADR thread telemetry DMVs (**sys.dm_hadr_db_threads** and [sys.dm_hadr_ag_threads](sys-dm-hadr-ag-threads-transact-sql.md)) allow users to quickly gain insight into thread usage by Availability Group and by high availability database. Understanding this thread usage is an important benchmark for tuning availability groups.

This DMV reports on thread usage at the availability group database level.

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**group_id**|**uniqueidentifier**|Identifier of the availability group to which the database belongs.|
|**ag_db_id**|**uniqueidentifier**|Identifier of the database within the availability group. This identifier is identical on every replica to which this database is joined.|
|**name**|**nvarchar(128)**|Name of the database.|
|**num_capture_threads**|**int**|Number of log capture threads for this database.|
|**num_redo_threads**|**int**|Number of redo threads for this database.|
|**num_parallel_redo_threads**|**int**|Number of parallel redo threads for this database.|

## Permissions  

 Requires VIEW SERVER STATE permission on the server.  
  
### Permissions for SQL Server 2022 and later

Requires VIEW SERVER PERFORMANCE STATE permission on the server.

## Related content

- [Always On availability groups dynamic management views and functions](always-on-availability-groups-dynamic-management-views-functions.md)
- [Always On Availability Groups Catalog Views (Transact-SQL)](../system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md)
- [Monitor Availability Groups (Transact-SQL)](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)
- [What is an Always On availability group?](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
