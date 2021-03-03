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

  Returns a row for every automatic page-repair attempt on any availability database on an availability replica that is hosted for any availability group by the server instance. This view contains rows for the latest automatic page-repair attempts on a given primary or secondary database, with a maximum of 100 rows per database. As soon as a database reaches the maximum, the row for its next automatic page-repair attempt replaces one of the existing entries.
  
  The following table defines the meaning of the various columns:  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  

|**start_time**|**datetime**|
|**completion_time**|**datetime**|empty|  
|**ag_id**|**uniqueidentifier**|ID .|  
|**ag_db-id**|**uniqueidentifier**|empty |  
|**ag_remote-replica_id**|**uniqueidentifier**|empty
|**operation_id**|**uniqueidentifier**|empty |  
|**is_source**|**bit**|empty|
|**current_state**|**bit**|empty|
|**performed_seeding**|**bit**|empty|
|**failured_state**|**int**|empty|
|**failured_state_desc**|**ncharvar**|empty|
|**error_code**|**int**|empty|
|**number_of_attempts**|**int**| empty|

 

  
## Security  
  
### Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Automatic Page Repair &#40;Availability Groups: Database Mirroring&#41;](../../sql-server/failover-clusters/automatic-page-repair-availability-groups-database-mirroring.md)   
 [suspect_pages &#40;Transact-SQL&#41;](../../relational-databases/system-tables/suspect-pages-transact-sql.md)   
 [Manage the suspect_pages Table &#40;SQL Server&#41;](../../relational-databases/backup-restore/manage-the-suspect-pages-table-sql-server.md)  
  
  

