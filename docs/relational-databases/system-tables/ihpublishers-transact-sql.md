---
title: "IHpublishers (Transact-SQL)"
description: IHpublishers (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "IHpublishers"
  - "IHpublishers_TSQL"
helpviewer_keywords:
  - "IHpublishers system table"
dev_langs:
  - "TSQL"
ms.assetid: 77007246-f10b-4b87-8edf-7afc3c2096af
---
# IHpublishers (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **IHpublishers** system table contains one row for each non-SQL Server Publisher using the current Distributor. This table is stored in the distribution database.  
  
## Definition  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher_id**|**smallint**|Identifies a non-SQL Server Publisher.|  
|**vendor**|**sysname**|The name of the vendor for the non-SQL Server database.|  
|**publisher_guid**|**uniqueidentifier**|A GUID that identifies the non-SQL Server Publisher.|  
|**flush_request_time**|**datetime**|Indicates the date and time when the last change occurred to article metadata that required the Log Reader Agent to update its metadata cache.|  
|**version**|**sysname**|A text string that characterizes the version of the non-SQL Server Publisher.|  
  
## See Also  
 [Heterogeneous Database Replication](../../relational-databases/replication/non-sql/heterogeneous-database-replication.md)   
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)   
 [sp_adddistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistpublisher-transact-sql.md)   
 [sp_changedistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changedistpublisher-transact-sql.md)   
 [sp_helpdistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdistpublisher-transact-sql.md)  
  
  
