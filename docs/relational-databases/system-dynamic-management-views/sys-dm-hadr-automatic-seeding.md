---
description: "sys.dm_hadr_automatic_seeding (Transact-SQL)"
title: "sys.dm_hadr_automatic_seeding (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2021"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "reference"
f1_keywords: 
  - "dm_hadr_automatic_seeding_TSQL"
  - "sys.dm_hadr_automatic_seeding"
  - "sys.dm_hadr_automatic_seeding_TSQL"
  - "dm_hadr_automatic_seeding"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "automatic seeding"
  - "sys.dm_hadr_automatic_seeding dynamic management view"
ms.assetid: d7840adf-4a1b-41ac-bc94-102c07ad1c79
author: cawrites
ms.author: chadam
---
# sys.dm_hadr_automatic_seeding (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

On the primary replica, query sys.dm_hadr_automatic_seeding to check the status of the automatic seeding process. The view returns one row for each seeding process.  
  
The following table defines the meaning of the various columns:  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**start_time**|**datetime**|The time the operation was initiated.|
|**completion_time**|**datetime**|The time the operation completed (null if ongoing).|  
|**ag_id**|**uniqueidentifier**|Unique ID for each Availability Group.|  
|**ag_db_id**|**uniqueidentifier**|Unique ID for each database in the Available Group.|  
|**ag_remote_replica_id**|**uniqueidentifier**|Unique ID for the other replica this seeding operation involves.|
|**operation_id**|**uniqueidentifier**|Unique identifier for this seeding operation.|  
|**is_source**|**bit**|Gets a value indicating whether this replica is the source (primary) of the seeding operation.|
|**current_state**|**bit**|Gets the current seeding state the operation is in.|
|**performed_seeding**|**bit**|Database streaming for seeding is initialized.|
|**failure_state**|**int**|Gets the reason the operation failed.|
|**failure_state_desc**|**ncharvar**|Gets the description the operation failed.|
|**error_code**|**int**|Gets any SQL error code encountered during seeding.|
|**number_of_attempts**|**int**|Gets the number of times this seeding operation has been attempted.|


## Security  
  
### Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Automatic Page Repair &#40;Availability Groups: Database Mirroring&#41;](../../sql-server/failover-clusters/automatic-page-repair-availability-groups-database-mirroring.md)   
 [suspect_pages &#40;Transact-SQL&#41;](../../relational-databases/system-tables/suspect-pages-transact-sql.md)   
 [Manage the suspect_pages Table &#40;SQL Server&#41;](../../relational-databases/backup-restore/manage-the-suspect-pages-table-sql-server.md)  
  
