---
title: "MSsnapshotdeliveryprogress (Transact-SQL)"
description: MSsnapshotdeliveryprogress (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSsnapshotdeliveryprogress_TSQL"
  - "MSsnapshotdeliveryprogress"
helpviewer_keywords:
  - "MSsnapshotdeliveryprogress system table"
dev_langs:
  - "TSQL"
ms.assetid: 9164bfe2-6fc4-4b52-946a-09ea3cf67041
---
# MSsnapshotdeliveryprogress (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSsnapshotdeliveryprogress** table is used to track files that have been successfully delivered to the Subscriber when a snapshot is being applied. This data is used to resume the delivery of files in case the Merge Agent fails to deliver all of the files during the session so that the same files are not delivered again the next time that the Merge Agent is run. This table is stored at the Subscriber in the subscription database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**session_token**|**nvarchar(260)**|Identifies the path to snapshot folder from which the file was successfully delivered. For publications that use parameterized filters, the string **dynsnap** will be appended to the value.|  
|**progress_token_hash**|**int**|A hash value generated based on the value of *progress_token* that is used improve lookup efficiency for a given *progress_token* value.|  
|**progress_token**|**nvarchar(500)**|Identifies a file that has been successfully delivered, where the value is a combination of the file name and path.|  
|**progress_timestamp**|**datetime**|The **datetime** value that indicates when a snapshot file was successfully delivered.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
