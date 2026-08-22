---
title: "sys.dm_exec_external_operations (Transact-SQL)"
description: sys.dm_exec_external_operations (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "DM_EXEC_EXTERNAL_OPERATIONS_TSQL"
  - "DM_EXEC_EXTERNAL_OPERATIONS"
  - "SYS.DM_EXEC_EXTERNAL_OPERATIONS_TSQL"
helpviewer_keywords:
  - "PolyBase,views"
  - "PolyBase"
  - "sys.dm_exec_external_operations management view"
  - "dm_exec_external_operations management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sys.dm_exec_external_operations (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Captures information about external PolyBase operations.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|execution_id|**nvarchar(32)**|Unique query identifier associated with PolyBase query|See ID in [sys.dm_exec_requests &#40;Transact-SQL&#41;](sys-dm-exec-requests-transact-sql.md)|  
|step_index|**int**|Index of the query step|See step_index in [sys.dm_exec_distributed_request_steps &#40;Transact-SQL&#41;](sys-dm-exec-distributed-request-steps-transact-sql.md)|  
|operation_ type|**nvarchar(128)**|Describes a Hadoop operation or other external operation|'External Hadoop Operation'|  
|operation_ name|**nvarchar(4000)**|Indicates how the status of job in percentage (how much is the input consumed)|0-1 - multiplied by factor 100 (completed)|  
|map_  progress|**float**|Indicates how the status of a reduce job in percentage, if any|0-1 - multiplied by factor 100 (completed)|  
  
## Related content

- [PolyBase troubleshooting with dynamic management views](/previous-versions/sql/sql-server-2016/mt146389(v=sql.130))
- [System dynamic management views and functions](system-dynamic-management-objects.md)
- [Database related dynamic management views (Transact-SQL)](database-related-dynamic-management-views-transact-sql.md)
