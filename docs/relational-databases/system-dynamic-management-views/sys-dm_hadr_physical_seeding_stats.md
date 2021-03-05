---
description: "sys.dm_hadr_physical_seeding_stats (Transact-SQL)"
title: "sys.dm_hadr_physical_seeding_stats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2021"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "reference"
ms.assetid: d7840adf-4a1b-41ac-bc94-102c07ad1c79
author: cawrites
ms.author: chadam
f1_keywords: 
  - "dm_hadr_physical_seeding_stats"
  - "sys.dm_hadr_physical_seeding_stats
  - "sys.dm_hadr_physical_seeding_stats_TSQL"
  - "dm_hadr_physical_seeding"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "sys.dm_hadr_physical_seeding_stats
  - "sys.dm_hadr_physical_seeding_stats dynamic management view"



---
# sys.dm_hadr_physical_seeding_stats (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

On the primary replica, query sys.dm_hadr_physical_seeding_stats DMV to see the physical statistics for each seeding process that is currently running.

The following table defines the meaning of the various columns:  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**local_physical_seeding_id**|**uniqueidentifier**|empty|  
|**remote_physical_seeding_id**|**uniqueidentifier**|empty|  
|**local_database_id**|**int**|empty|  
|**local_database_name**|**nvarchar**|empty |
|**remote_machine_name**|**nvarchar**|empty |  
|**role_desc**|**nvarchar**|empty |
|**internal_state_desc**|**nvarchar**|empty |
|**transfer_rate_bytes_per_second**|**bigint**|empty |
|**transfered_size_bytes**|**bigint**|empty |
|**database_size_bytes**|**bigint**|empty |
|**start_time_utc**|**datetime**|empty |
|**end_time_utc**|**datetime**|empty |
|**estimate_time_complete_utc**|**datetime**|empty |
|**total_disk_io_wait_time_ms**|**bigint**|empty |
|**total_network_wait_time_ms**|**bigint**|empty |
|**failure_code**|**int**|empty |
|**failure_message**|**nvarchar**|empty |
|**failure_time_utc**|**datetime**|empty |
|**is_compression_enabled**|**bit**|empty|
|**datetime**|empty|  
  
## Security  
  
### Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Automatic Page Repair &#40;Availability Groups: Database Mirroring&#41;](../../sql-server/failover-clusters/automatic-page-repair-availability-groups-database-mirroring.md)   
 [suspect_pages &#40;Transact-SQL&#41;](../../relational-databases/system-tables/suspect-pages-transact-sql.md)   
 [Manage the suspect_pages Table &#40;SQL Server&#41;](../../relational-databases/backup-restore/manage-the-suspect-pages-table-sql-server.md)  
  
  

