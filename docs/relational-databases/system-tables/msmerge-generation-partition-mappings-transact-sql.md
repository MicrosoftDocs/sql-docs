---
title: "MSmerge_generation_partition_mappings (T-SQL)"
description: Describes the MSmerge_generation_partition_mappings stored procedure used to track changes to partitions in a merge publication.
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
ms.custom: seo-lt-2019
f1_keywords:
  - "MSmerge_generation_partition_mappings_TSQL"
  - "MSmerge_generation_partition_mappings"
helpviewer_keywords:
  - "MSmerge_generation_partition_mappings system table"
dev_langs:
  - "TSQL"
ms.assetid: 443a4024-ce48-4772-9ee5-95bd6fb6476b
---
# MSmerge_generation_partition_mappings (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSmerge_generation_partition_mappings** table is used to track changes to partitions in a merge publication. This table is stored in the publication and scubscription databases.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publication_number**|**smallint**|Identifies the merge publication|  
|**generation**|**bigint**|The generation value.|  
|**partition_id**|**int**|Identifies the partition.|  
|**changecount**|**int**|The number of times that the partition has changed.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
