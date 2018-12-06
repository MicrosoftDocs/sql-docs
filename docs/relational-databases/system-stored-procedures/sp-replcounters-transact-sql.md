---
title: "sp_replcounters (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_replcounters"
  - "sp_replcounters_TSQL"
helpviewer_keywords: 
  - "sp_replcounters"
ms.assetid: fe585b1f-edda-421f-81d6-8a03a3a535d2
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_replcounters (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns replication statistics about latency, throughput, and transaction count for each published database. This stored procedure is executed at the Publisher on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_replcounters  
  
```  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**Database**|**sysname**|Name of the database.|  
|**Replicated transactions**|**int**|Number of transactions in the log awaiting delivery to the distribution database.|  
|**Replication rate trans/sec**|**float**|Average number of transactions per second delivered to the distribution database.|  
|**Replication latency**|**float**|Average time, in seconds, that transactions were in the log before being distributed.|  
|**Replbeginlsn**|**binary(10)**|Log sequence number (LSN) of the current truncation point in the log.|  
|**Replnextlsn**|**binary(10)**|LSN of the next commit record awaiting delivery to the distribution database.|  
  
## Remarks  
 **sp_replcounters** is used in transactional replication.  
  
## Permissions  
 Requires membership in the **db_owner** fixed database role or **sysadmin** fixed server role.  
  
## See Also  
 [sp_replcmds &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replcmds-transact-sql.md)   
 [sp_repldone &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-repldone-transact-sql.md)   
 [sp_replflush &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replflush-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
