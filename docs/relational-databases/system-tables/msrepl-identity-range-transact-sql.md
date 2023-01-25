---
title: "MSrepl_identity_range (Transact-SQL)"
description: MSrepl_identity_range (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSrepl_identity_range_TSQL"
  - "MSrepl_identity_range"
helpviewer_keywords:
  - "MSrepl_identity_range system table"
dev_langs:
  - "TSQL"
ms.assetid: 6e92a8e8-7667-4c98-b1c4-46735bac50d8
---
# MSrepl_identity_range (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSrepl_identity_range** table provides identity range management support. This table is stored in the publication, distribution and subscription databases  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher**|**sysname**|The name of the Publisher.|  
|**publisher_db**|**sysname**|The name of the publication database.|  
|**tablename**|**sysname**|The name of the table.|  
|**identity_support**|**int**|Specifies if automatic identity range handling is enabled. 0 specifies that automatic identity range handling is not enabled.|  
|**next_seed**|**bigint**|If automatic identity range is enabled, indicates the starting point of the next range.|  
|**pub_range**|**bigint**|The publisher identity range size.|  
|**range**|**bigint**|The size of the consecutive identity values that would be assigned to subscribers in an adjustment.|  
|**max_identity**|**bigint**|The maximum boundary of the identity range.|  
|**threshold**|**int**|The identity range threshold percentage.|  
|**current_max**|**bigint**|The current max that can be assigned but not necessarily be assigned.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
