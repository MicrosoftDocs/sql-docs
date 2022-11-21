---
title: "MSmerge_metadataaction_request (Transact-SQL)"
description: MSmerge_metadataaction_request (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSmerge_metadataaction_request"
  - "MSmerge_metadataaction_request_TSQL"
helpviewer_keywords:
  - "MSmerge_metadataaction_request system table"
dev_langs:
  - "TSQL"
ms.assetid: cd31a114-900a-4218-ab58-d959e547c647
---
# MSmerge_metadataaction_request (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSmerge_metadataaction_request** table stores one row for each compensating action that is required. Using Web synchronization, if an error occurs and the synchronization must be retried, an entry is made into **MSmerge_metadataaction_request**. During the upload phase of the subsequent merge, requests for all articles belonging to the publication being synchronized are retrieved from this table and uploaded. When the synchronization is successfully completed, the corresponding row in the **MSmerge_metadataaction_request** table is deleted. This table is stored at the Publisher in the publication database and at the Subscriber in the subscription database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**tablenick**|**int**|The nickname of the published table.|  
|**rowguid**|**uniqueidentifier**|The row identifier for the given row.|  
|**action**|**tinyint**|Identifies the compensating action required.|  
|**generation**|**bigint**|The value of the generation for which the compensating action is needed.|  
|**changed**|**int**|Internal-use only.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)   
 [Web Synchronization for Merge Replication](../../relational-databases/replication/web-synchronization-for-merge-replication.md)  
  
  
