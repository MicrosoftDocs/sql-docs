---
title: "sp_helpreplicationdboption (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpreplicationdboption_TSQL"
  - "sp_helpreplicationdboption"
helpviewer_keywords: 
  - "sp_helpreplicationdboption"
ms.assetid: 143ce689-108b-49d7-9892-fd3a86897f38
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpreplicationdboption (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Shows whether databases at the Publisher are enabled for replication. This stored procedure is executed at the Publisher on any database. *Not supported for Oracle Publishers.*  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpreplicationdboption [ [ @dbname =] 'dbname' ]  
    [ , [ @type = ] 'type' ]  
    [ , [ @reserved = ] reserved ]  
```  
  
## Arguments  
`[ @dbname = ] 'dbname'`
 Is the name of the database. *dbname* is **sysname**, with a default of **%**. If **%**, then the result set contains all databases at the Publisher, otherwise only information on the specified database is returned. Information is not returned for any databases on which the user does not have the appropriate permissions, as described below.  
  
`[ @type = ] 'type'`
 Restricts the result set to contain only databases on which the specified replication option *type* value has been enabled. *type* is **sysname**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**publish**|Transactional replication allowed.|  
|**merge publish**|Merge replication allowed.|  
|**replication allowed** (default)|Either transactional or merge replication allowed.|  
  
`[ @reserved = ] reserved`
 Specifies whether information on existing publications and subscriptions is returned. *reserved* is **bit**, with a default value of 0. If **1**, the result set includes information on whether the database specified has any existing publications or subscriptions.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of the database.|  
|**id**|**int**|Database identifier.|  
|**transpublish**|**bit**|If the database has been enabled for snapshot or transactional publishing; where a value of **1** means that snapshot or transactional publishing is enabled.|  
|**mergepublish**|**bit**|If the database has been enabled for merge publishing; where a value of **1** means that merge publishing is enabled.|  
|**dbowner**|**bit**|If the user is a member of the **db_owner** fixed database role; where a value of **1** indicates that the user is a member of this role.|  
|**dbreadonly**|**bit**|Is if the database is marked as read-only; where a value of **1** means that the database is read-only.|  
|**haspublications**|**bit**|Is if the database has any existing publications; where a value of **1** means that there are existing publications.|  
|**haspullsubscriptions**|**bit**|Is if the database has any existing pull subscriptions; where a value of **1** means that there are existing pull subscriptions.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helpreplicationdboption** is used in snapshot, transactional, and merge replication.  
  
## Permissions  
 Members of the **sysadmin** fixed server role can execute **sp_helpreplicationdboption** for any database. Members of the **db_owner** fixed database role can execute **sp_helpreplicationdboption** for that database.  
  
## See Also  
 [sp_replicationdboption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
