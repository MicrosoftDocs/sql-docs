---
description: "sys.dm_hadr_ag_threads (Transact-SQL)"
title: "sys.dm_hadr_ag_threads (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "02/08/2021"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_hadr_ag_threads_TSQL"
  - "sys.dm_hadr_ag_threads"
  - "dm_hadr_ag_threads_TSQL"
  - "dm_hadr_ag_threads"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "Availability Groups [SQL Server], WSFC clusters"
  - "sys.dm_hadr_ag_threads catalog view"
ms.assetid: 
author: kfarlee
ms.author:
monikerRange:
---
# sys.dm_hadr_ag_threads



The Hadr thread telemetry DMVs ( **sys.dm_hadr_ag_threads** and [sys.dm_hadr_db_threads](../../relational-databases/system-dynamic-management-views/sys-dm-hadr-db-threads.md) allow users to quickly gain insight into thread usage by Availability Group and by high availability database. Understanding this thread usage is an important benchmark for tuning Availability Groups.

This DMV reports on thread usage at the Availability Group level.


|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**group_id**|**uniqueidentifier**|Unique identifier of the availability group.|
|**name**|**nvarchar(128)**|Name of the availability group|
|**num_databases**|**int**|Number of databases in the AG|
|**num_capture_threads**|**int**|Number of threads used for capturing changes for this Availability Group|
|**num_redo_threads**|**int**|Number of threads used for redo for this Availability Group|
|**num_parallel_redo_threads**|**int**|Number of threads used for parallel redo for this Availability Group|
|**num_hadr_threads**|**int**|Total number of hadr threads for this Availability Group|


## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## Examples  
  
## See Also  
 [Always On Availability Groups Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/always-on-availability-groups-dynamic-management-views-functions.md)   
 [Always On Availability Groups Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md)   
 [Monitor Availability Groups &#40;Transact-SQL&#41;](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)   
 [AlwaysOn Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)  
  