---
title: "sp_help_publication_access (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_help_publication_access"
  - "sp_help_publication_access_TSQL"
helpviewer_keywords: 
  - "sp_help_publication_access"
ms.assetid: 9408fa13-54a0-4cb1-8fb0-845e5536ef50
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_help_publication_access (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a list of all granted logins for a publication. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_publication_access [ @publication = ] 'publication'  
    [ , [ @return_granted = ] 'return_granted' ]   
    [ , [ @login = ] 'login' ]  
    [ , [ @initial_list = ] initial_list ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication to access. *publication* is **sysname**, with no default.  
  
`[ @return_granted = ] 'return_granted'`
 Is the login ID. *return_granted* is **bit**, with a default of 1. If **0** is specified and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication is used, the available logins that appear at the Publisher but not at the Distributor are returned. If **0** is specified and Windows Authentication is used, the logins not specifically denied access at either the Publisher or Distributor are returned.  
  
`[ @login = ] 'login'`
 Is the standard security login ID. *login* is **sysname**, with a default of **%**.  
  
`[ @initial_list = ] initial_list`
 Specifies whether to return all members with publication access or just those who had access before new members were added to the list. *initial_list* is bit, with a default of **0**.  
  
 **1** returns information for all members of the **sysadmin** fixed server role with valid logins at the Distributor that existed when the publication was created, as well as the current login.  
  
 **0** returns information for all members of the **sysadmin** fixed server role with valid logins at the Distributor that existed when the publication was created as well as all users in the publication access list who do not belong to the **sysadmin** fixed server role.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**Loginname**|**nvarchar(256)**|Actual login name.|  
|**Isntname**|**int**|**0** = Login is not a Windows user.<br /><br /> **1** = Login is a Windows user.|  
|**Isntgroup**|**int**|**0** = Login is not a Windows group.<br /><br /> **1** = Login is a Windows group.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_help_publication_access** is used in all types of replication.  
  
 When both **Isntname** and **Isntgroup** in the result set are **0**, it is assumed that the login is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_help_publication_access**.  
  
## See Also  
 [sp_grant_publication_access &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grant-publication-access-transact-sql.md)   
 [sp_revoke_publication_access &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-revoke-publication-access-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
