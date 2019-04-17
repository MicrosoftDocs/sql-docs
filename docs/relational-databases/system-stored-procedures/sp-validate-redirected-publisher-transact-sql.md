---
title: "sp_validate_redirected_publisher (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_validate_redirected_publisher"
  - "sp_validate_redirected_publisher_TSQL"
helpviewer_keywords: 
  - "sp_validate_redirected_publisher"
ms.assetid: 2b7fdbad-17e4-4442-b0b2-9b5e8f84b91d
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_validate_redirected_publisher (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Verifies that the current host for the publishing database is capable of supporting replication. Must be run from a distribution database. This procedure is called by **sp_get_redirected_publisher**.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
      sp_validate_redirected_publisher   
    [ @original_publisher = ] 'original_publisher',  
    [ @publisher_db = ] 'database_name',   
    [ @redirected_publisher = ] 'new_publisher' output  
```  
  
## Arguments  
`[ @original_publisher = ] 'original_publisher'`
 The name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that originally published the database. *original_publisher* is **sysname**, with no default.  
  
`[ @publisher_db = ] 'publisher_db'`
 The name of the database being published. *publisher_db* is **sysname**, with no default.  
  
`[ @redirected_publisher = ] 'redirected_publisher'`
 The target of redirection specified when **sp_redirect_publisher** was called for the publisher/database pair. *redirected_publisher* is **sysname**, with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None.  
  
## Remarks  
 If no entry exists for the publisher and the publishing database, **sp_validate_redirected_publisher** returns null in the output parameter *@redirected_publisher*. If an entry exists, it is returned in the output parameter in both success and failure cases.  
  
 If the validation succeeds, **sp_validate_redirected_publisher** returns a success indication.  
  
 If the validation fails, errors are raised describing the failure.  
  
## Permissions  
 Caller must either be a member of the **sysadmin** fixed server role, the **db_owner** fixed database role for the distribution database, or a member of a publication access list for a defined publication associated with the publisher database.  
  
## See Also  
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)   
 [sp_get_redirected_publisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-get-redirected-publisher-transact-sql.md)   
 [sp_redirect_publisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-redirect-publisher-transact-sql.md)   
 [sp_validate_replica_hosts_as_publishers &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-validate-replica-hosts-as-publishers-transact-sql.md)  
  
  
