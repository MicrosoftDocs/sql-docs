---
title: "sys.dm_hadr_physical_seeding_stats (Transact-SQL)"
description: sys.dm_hadr_physical_seeding_stats (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/17/2021"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_hadr_physical_seeding_stats_TSQL"
  - "sys.dm_hadr_physical_seeding_stats"
  - "sys.dm_hadr_physical_seeding_stats_TSQL"
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "physical seeding"
  - "sys.dm_hadr_physical_seeding_stats dynamic management view"
dev_langs:
  - "TSQL"
---
# sys.dm_hadr_physical_seeding_stats (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

On the primary replica, query sys.dm_hadr_physical_seeding_stats DMV to see the physical statistics for each seeding process that is currently running.

The following table defines the meaning of the various columns:  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**local_physical_seeding_id**|**uniqueidentifier**|Unique identifier of this seeding operation on the local replica.|  
|**remote_physical_seeding_id**|**uniqueidentifier**|Unique identifier of this seeding operation on the remote replica.|  
|**local_database_id**|**int**|Database ID on the local replica.|  
|**local_database_name**|**nvarchar**|Database Name on the local replica. |
|**remote_machine_name**|**nvarchar**|Remote replica machine name.|  
|**role_desc**|**nvarchar**|Seeding role description. (available values: Source, Destination, Forwarder, ForwarderDestination)|
|**internal_state_desc**|**nvarchar**|Description of the local replica state.|
|**transfer_rate_bytes_per_second**|**bigint**|Current seeding transfer rate in bytes per second.|
|**transfered_size_bytes**|**bigint**|Total bytes already transferred.|
|**database_size_bytes**|**bigint**|Total size in bytes of the database being seeded.|
|**start_time_utc**|**datetime**|Start time of the seeding operation in UTC.|
|**end_time_utc**|**datetime**|End time of the seeding operation in UTC.|
|**estimate_time_complete_utc**|**datetime**|Estimation of the completion time for an in-process seeding operation, in UTC.|
|**total_disk_io_wait_time_ms**|**bigint**|Sum of the disk IO wait time encountered, in ms.|
|**total_network_wait_time_ms**|**bigint**|Sum of the network IO wait time encountered, in ms.|
|**failure_code**|**int**|Failure code for the seeding operation.|
|**failure_message**|**nvarchar**|Message corresponding to the failure code.|
|**failure_time_utc**|**datetime**|Time of failure, in UTC.|
|**is_compression_enabled**|**bit**|Indicates whether compression in enabled for the seeding operation.|
  
## Security  
  
### Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Automatic Page Repair &#40;Availability Groups: Database Mirroring&#41;](../../sql-server/failover-clusters/automatic-page-repair-availability-groups-database-mirroring.md)   
 [suspect_pages &#40;Transact-SQL&#41;](../../relational-databases/system-tables/suspect-pages-transact-sql.md)   
 [Manage the suspect_pages Table &#40;SQL Server&#41;](../../relational-databases/backup-restore/manage-the-suspect-pages-table-sql-server.md)  
  
