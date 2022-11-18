---
title: "IHpublisherindexes (Transact-SQL)"
description: IHpublisherindexes (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "IHpublisherindexes"
  - "IHpublisherindexes_TSQL"
helpviewer_keywords:
  - "IHpublisherindexes system table"
dev_langs:
  - "TSQL"
ms.assetid: 6008ef89-eeb9-46dc-93a2-f7623298cf0f
---
# IHpublisherindexes (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **IHpublisherindexes** system table contains one row for each index replicated from non-SQL Server Publishers using the current Distributor. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisherindex_id**|**int**|Identifies a published index.|  
|**table_id**|**int**|Identifies the table from [IHpublishertables](../../relational-databases/system-tables/ihpublishertables-transact-sql.md) to which the index belongs.|  
|**publisher_id**|**smallint**|Identifies the non-SQL Server Publisher from which the index is being published.|  
|**name**|**sysname**|The name of the published index.|  
|**type**|**nvarchar(255)**|A supported index type from the [IHindextypes](../../relational-databases/system-tables/ihindextypes-transact-sql.md) system table.|  
  
## See Also  
 [Heterogeneous Database Replication](../../relational-databases/replication/non-sql/heterogeneous-database-replication.md)   
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
