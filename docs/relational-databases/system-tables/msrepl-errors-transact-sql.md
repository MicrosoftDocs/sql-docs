---
title: "MSrepl_errors (Transact-SQL)"
description: MSrepl_errors (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSrepl_errors"
  - "MSrepl_errors_TSQL"
helpviewer_keywords:
  - "MSrepl_errors system table"
dev_langs:
  - "TSQL"
ms.assetid: c6e023c1-2c32-4269-8d76-e442ea309e4b
---
# MSrepl_errors (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSrepl_errors** table contains rows with extended Distribution Agent and Merge Agent failure information. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|The ID of the error.|  
|**time**|**datetime**|The time the error occurred.|  
|**error_type_id**|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**source_type_id**|**int**|The error source type ID.|  
|**source_name**|**nvarchar(100)**|The name of the error source.|  
|**error_code**|**sysname**|The error code.|  
|**error_text**|**ntext**|The error message.|  
|**xact_seqno**|**varbinary(16)**|The starting transaction log sequence number of the failed execution batch. Used only by the Distribution Agents, this is the transaction log sequence number of the first transaction in the failed execution batch.|  
|**command_id**|**int**|The command ID of the failed execution batch. Used only by the Distribution Agents, this is the command ID of the first command in the failed execution batch.|  
|**session_id**|**int**|The ID of the agent session in which the error occurred.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
