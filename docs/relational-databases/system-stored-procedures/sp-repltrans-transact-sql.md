---
title: "sp_repltrans (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_repltrans_TSQL"
  - "sp_repltrans"
helpviewer_keywords: 
  - "sp_repltrans"
ms.assetid: 738e2322-335b-44fa-820e-f31c02743978
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_repltrans (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a result set of all the transactions in the publication database transaction log that are marked for replication but have not been marked as distributed. This stored procedure is executed at the Publisher on a publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_repltrans  
```  
  
## Result Sets  
 **sp_repltrans** returns information about the publication database from which it is executed, allowing you to view transactions currently not distributed (those transactions remaining in the transaction log that have not been sent to the Distributor). The result set displays the log sequence numbers of the first and last records for each transaction. **sp_repltrans** is similar to [sp_replcmds &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replcmds-transact-sql.md) but does not return the commands for the transactions.  
  
## Remarks  
 **sp_repltrans** is used in transactional replication.  
  
 **sp_repltrans** is not supported for non- [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_repltrans**.  
  
## See Also  
 [sp_repldone &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-repldone-transact-sql.md)   
 [sp_replflush &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replflush-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
