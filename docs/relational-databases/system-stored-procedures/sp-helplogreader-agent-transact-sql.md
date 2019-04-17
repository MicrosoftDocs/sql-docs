---
title: "sp_helplogreader_agent (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helplogreader_agent"
  - "sp_helplogreader_agent_TSQL"
helpviewer_keywords: 
  - "sp_helplogreader_agent"
ms.assetid: ff837209-e2b3-481a-a48f-8530bfe53d97
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helplogreader_agent (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns properties of the Log Reader Agent job for the publication database. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helplogreader_agent [ [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher. *publisher* is **sysname**, with a default of NULL.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|ID of the agent.|  
|**name**|**nvarchar(100)**|Name of the agent.|  
|**publisher_security_mode**|**smallint**|Security mode used by the agent when connecting to the Publisher, which can be one of the following:<br /><br /> **0** = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication<br /><br /> **1** = Windows Authentication.|  
|**publisher_login**|**sysname**|Login used when connecting to the Publisher.|  
|**publisher_password**|**nvarchar(524)**|For security reasons, a value of **\*\*\*\*\*\*\*\*\*\*** is always returned.|  
|**job_id**|**uniqueidentifier**|Unique ID of the agent job.|  
|**job_login**|**nvarchar(512)**|Is the Windows account under which the Log Reader Agent runs, which is returned in the format *domain*\\*username*.|  
|**job_password**|**sysname**|For security reasons, a value of **\*\*\*\*\*\*\*\*\*\*** is always returned.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helplogreader_agent** is used in transactional replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role at the Publisher or members of the **db_owner** fixed database role on the publication database can execute **sp_helplogreader_agent**.  
  
## See Also  
 [View and Modify Replication Security Settings](../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)   
 [sp_addlogreader_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlogreader-agent-transact-sql.md)   
 [sp_changelogreader_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changelogreader-agent-transact-sql.md)  
  
  
