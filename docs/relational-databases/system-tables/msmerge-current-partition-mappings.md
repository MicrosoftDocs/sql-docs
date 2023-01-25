---
title: "MSmerge_current_partition_mappings"
description: MSmerge_current_partition_mappings
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSmerge_current_partition_mappings"
  - "MSmerge_current_partition_mappings_TSQL"
helpviewer_keywords:
  - "MSmerge_current_partition_mappings system table"
dev_langs:
  - "TSQL"
ms.assetid: a3088840-5a30-40f5-8e8a-aa03afc4905f
---
# MSmerge_current_partition_mappings
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSmerge_current_partition_mappings** table stores one row for each partition id a given changed row belongs to. This table is stored in the publication database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publication_number**|**smallint**|The publication number, which is stored in **sysmergepublications**.|  
|**tablenick**|**int**|The nickname of the published table.|  
|**rowguid**|**uniqueidentifier**|The row identifier for the given row.|  
|**partition_id**|**int**|The ID of the partition to which the row belongs. The value is -1 if the row change is relevant to all Subscribers.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
