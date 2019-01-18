---
title: "MSrepl_errors (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSrepl_errors"
  - "MSrepl_errors_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSrepl_errors system table"
ms.assetid: c6e023c1-2c32-4269-8d76-e442ea309e4b
author: stevestein
ms.author: sstein
manager: craigg
---
# MSrepl_errors (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
  
  
