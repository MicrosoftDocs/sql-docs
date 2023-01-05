---
title: "sys.dm_db_mirroring_auto_page_repair (Transact-SQL)"
description: Database Mirroring - sys.dm_db_mirroring_auto_page_repair
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_db_mirroring_auto_page_repair_TSQL"
  - "sys.dm_db_mirroring_auto_page_repair_TSQL"
  - "sys.dm_db_mirroring_auto_page_repair"
  - "dm_db_mirroring_auto_page_repair"
helpviewer_keywords:
  - "automatic page repair"
  - "database mirroring [SQL Server], automatic page repair"
  - "sys.dm_db_mirroring_auto_page_repair dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 49f0fc2a-e25e-47e1-a135-563adb509af1
---
# Database Mirroring - sys.dm_db_mirroring_auto_page_repair
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a row for every automatic page-repair attempt on any mirrored database on the server instance. This view contains rows for the latest automatic page-repair attempts on a given mirrored database, with a maximum of 100 rows per database. As soon as a database reaches the maximum, the row for its next automatic page-repair attempt replaces one of the existing entries. The following table defines the meaning of the various columns.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_id**|**int**|ID of the database to which this row corresponds.|  
|**file_id**|**int**|ID of the file in which the page is located.|  
|**page_id**|**bigint**|ID of the page in the file.|  
|**error_type**|**int**|Type of the error. The values can be:<br /><br /> **-**1 = All hardware 823 errors<br /><br /> 1 = 824 errors other than a bad checksum or a torn page (such as a bad page ID)<br /><br /> 2 = Bad checksum<br /><br /> 3 = Torn page|  
|**page_status**|**int**|The status of the page-repair attempt:<br /><br /> 2 = Queued for request from partner.<br /><br /> 3 = Request sent to partner.<br /><br /> 4 = Queued for automatic page repair (response received from partner).<br /><br /> 5 = Automatic page repair succeeded and the page should be usable.<br /><br /> 6 = Irreparable. This indicates that an error occurred during page-repair attempt, for example, because the page is also corrupted on the partner, the partner is disconnected, or a network problem occurred. This state is not terminal; if corruption is encountered again on the page, the page will be requested again from the partner.|  
|**modification_time**|**datetime**|Time of last change to the page status.|  
  
## Security  
  
### Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Automatic Page Repair &#40;Availability Groups: Database Mirroring&#41;](../../sql-server/failover-clusters/automatic-page-repair-availability-groups-database-mirroring.md)   
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [suspect_pages &#40;Transact-SQL&#41;](../../relational-databases/system-tables/suspect-pages-transact-sql.md)   
 [Manage the suspect_pages Table &#40;SQL Server&#41;](../../relational-databases/backup-restore/manage-the-suspect-pages-table-sql-server.md)  
  
  


