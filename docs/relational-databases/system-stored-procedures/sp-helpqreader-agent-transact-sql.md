---
description: "sp_helpqreader_agent (Transact-SQL)"
title: "sp_helpqreader_agent (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_helpqreader_agent_TSQL"
  - "sp_helpqreader_agent"
helpviewer_keywords: 
  - "sp_helpqreader_agent"
ms.assetid: 8e74e1aa-e95b-4183-8017-bf123439b08d
author: markingmyname
ms.author: maghan
---
# sp_helpqreader_agent (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns properties of the Queue Reader agent. This stored procedure is executed at the Distributor on the distribution database or at the Publisher on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpqreader_agent [ [ @frompublisher = ] frompublisher ]  
```  
  
## Arguments  
`[ @frompublisher = ] frompublisher`
 Specifies whether the stored procedure is called at the Publisher or at the Distributor. *frompublisher* is bit, with a default value of 0. **1** means that the stored procedure is called from the Publisher, and **0** means that the stored procedure is called from the Distributor.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|ID of the agent.|  
|**name**|**nvarchar(100)**|Name of the agent.|  
|**job_id**|**uniqueidentifier**|Unique ID of the agent job.|  
|**job_login**|**nvarchar(512)**|Is the Windows account under which the Distribution agent runs, which is returned in the format *DOMAIN*\\*username*.|  
|**job_password**|**sysname**|For security reasons, a value of **\*\*\*\*\*\*\*\*\*\*** is always returned.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helpqreader_agent** is used in transactional replication.  
  
## Permissions  
 When the value of *frompublisher* is **1**, only members of the **sysadmin** fixed server role at the Publisher or members of the **db_owner** fixed database role on the publication database can execute **sp_helpqreader_agent**. Otherwise, only members of the **sysadmin** fixed server role at the Distributor or members of the **db_owner** fixed database role on the distribution database can execute **sp_helpqreader_agent**.  
  
## See Also  
 [Enable Updating Subscriptions for Transactional Publications](../../relational-databases/replication/publish/enable-updating-subscriptions-for-transactional-publications.md)  
  
  
