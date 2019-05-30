---
title: "sp_grant_publication_access (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_grant_publication_access_TSQL"
  - "sp_grant_publication_access"
helpviewer_keywords: 
  - "sp_grant_publication_access"
ms.assetid: 17993952-def6-4a16-b1c1-323ec42967f8
ms.author: vanto
manager: craigg
manager: craigg
---
# sp_grant_publication_access (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds a login to the access list of the publication. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_grant_publication_access [ @publication = ] 'publication', [ @login = ] 'login'   
    [ , [ @reserved = ] 'reserved' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication to access. **'***publication***'** is **sysname**, with no default.  
  
`[ @login = ] 'login'`
 Is the login ID. **'***login***'** is **sysname**, with no default.  
  
`[ @reserved = ] 'reserved'`
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_grant_publication_access** is used in snapshot, transactional, and merge replication.  
  
 This stored procedure can be called repeatedly.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_grant_publication_access**.  
  
## See Also  
 [sp_help_publication_access &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-publication-access-transact-sql.md)   
 [sp_revoke_publication_access &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-revoke-publication-access-transact-sql.md)   
 [Secure the Publisher](../../relational-databases/replication/security/secure-the-publisher.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
