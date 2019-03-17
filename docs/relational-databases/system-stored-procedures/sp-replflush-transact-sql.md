---
title: "sp_replflush (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_replflush"
  - "sp_replflush_TSQL"
helpviewer_keywords: 
  - "sp_replflush"
ms.assetid: 20809f5f-941d-427f-8f0c-de7a6c487584
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_replflush (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Flushes the article cache. This stored procedure is executed at the Publisher on the publication database.  
  
> [!IMPORTANT]  
>  You should not have to execute this procedure manually. **sp_replflush** should only be used for troubleshooting replication as directed by an experienced replication support professional.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_replflush  
```  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_replflush** is used in transactional replication.  
  
 Article definitions are stored in the cache for efficiency. **sp_replflush** is used by other replication stored procedures whenever an article definition is modified or dropped.  
  
 Only one client connection can have log reader access to a given database. If a client has log reader access to a database, executing **sp_replflush** causes the client to release its access. Other clients can then scan the transaction log using **sp_replcmds** or **sp_replshowcmds**.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_replflush**.  
  
## See Also  
 [sp_replcmds &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replcmds-transact-sql.md)   
 [sp_repldone &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-repldone-transact-sql.md)   
 [sp_repltrans &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-repltrans-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
