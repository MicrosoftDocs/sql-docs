---
title: "MSmerge_identity_range (Transact-SQL)"
description: MSmerge_identity_range (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSmerge_identity_range_TSQL"
  - "MSmerge_identity_range"
helpviewer_keywords:
  - "MSmerge_identity_range system table"
dev_langs:
  - "TSQL"
ms.assetid: 493a2028-88a0-4e83-ad89-ae5661d9f477
---
# MSmerge_identity_range (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSmerge_identity_range** table is used to track the numeric ranges assigned to identity columns for subscription to publications on which replication is automatically managing these range assignments. This table is stored in the publication and subscription databases.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**subid**|**uniqueidentifier**|The unique identification number for a given subscription.|  
|**artid**|**uniqueidentifier**|The unique identification number for the given article.|  
|**range_begin**|**numeric(38)**|The identity value at the start of the current range.|  
|**range_end**|**numeric(38)**|The identity value at the end of the current range.|  
|**next_range_begin**|**numeric(38)**|The identity value at the start of the next range to be assigned.|  
|**next_range_end**|**numeric(38)**|The identity value at the end of the next range to be assigned.|  
|**is_pub_range**|**bit**|A value of **1** if the identity range is assigned to the publication.|  
|**max_used**|**numeric(38)**|The maximum identity value that can be assigned.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
