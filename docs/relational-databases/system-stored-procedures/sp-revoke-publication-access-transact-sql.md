---
title: "sp_revoke_publication_access (Transact-SQL)"
description: "sp_revoke_publication_access (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_revoke_publication_access_TSQL"
  - "sp_revoke_publication_access"
helpviewer_keywords:
  - "sp_revoke_publication_access"
dev_langs:
  - "TSQL"
---
# sp_revoke_publication_access (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Removes the login from a publications access list. This stored procedure is executed at the Publisher on the publication database.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_revoke_publication_access [ @publication = ] 'publication' , [ @login = ] 'login'  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication to access. *publication* is **sysname**, with no default.  
  
`[ @login = ] 'login'`
 Is the login ID. *login* is **sysname**, with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_revoke_publication_access** is used in snapshot, transactional, and merge replication.  
  
 **sp_revoke_publication_access** can be called repeatedly.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_revoke_publication_access**.  
  
## See Also  
 [sp_grant_publication_access &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grant-publication-access-transact-sql.md)   
 [sp_help_publication_access &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-publication-access-transact-sql.md)   
 [Secure the Publisher](../../relational-databases/replication/security/secure-the-publisher.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
