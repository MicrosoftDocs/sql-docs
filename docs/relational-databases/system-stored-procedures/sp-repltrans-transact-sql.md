---
title: "sp_repltrans (Transact-SQL)"
description: "sp_repltrans (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_repltrans_TSQL"
  - "sp_repltrans"
helpviewer_keywords:
  - "sp_repltrans"
dev_langs:
  - "TSQL"
---
# sp_repltrans (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns a result set of all the transactions in the publication database transaction log that are marked for replication but have not been marked as distributed. This stored procedure is executed at the Publisher on a publication database.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
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
  
  
