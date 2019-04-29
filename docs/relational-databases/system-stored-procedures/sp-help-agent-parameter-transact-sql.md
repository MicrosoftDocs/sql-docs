---
title: "sp_help_agent_parameter (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_help_agent_parameter"
  - "sp_help_agent_parameter_TSQL"
helpviewer_keywords: 
  - "sp_help_agent_parameter"
ms.assetid: 8fb4a9c3-19af-4a34-8004-572729ba3d15
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_help_agent_parameter (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns all the parameters of a profile from the [MSagent_parameters &#40;Transact-SQL&#41;](../../relational-databases/system-tables/msagent-parameters-transact-sql.md) system table. This stored procedure is executed at the Distributor where the agent is running, on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_agent_parameter [ [ @profile_id = ] profile_id ]  
```  
  
## Arguments  
`[ @profile_id = ] profile_id`
 Is the ID of the profile from the [MSagent_parameters &#40;Transact-SQL&#41;](../../relational-databases/system-tables/msagent-parameters-transact-sql.md) table. *profile_id* is **int**, with a default of **-1**, which returns all parameters.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**profile_id**|**int**|ID of the agent profile.|  
|**parameter_name**|**sysname**|Name of the parameter.|  
|**value**|**nvarchar(255)**|Value of the parameter.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_help_agent_parameter** is used in all types of replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **replmonitor** fixed database role can execute **sp_help_agent_parameter**.  
  
## See Also  
 [Work with Replication Agent Profiles](../../relational-databases/replication/agents/work-with-replication-agent-profiles.md)   
 [sp_add_agent_parameter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-agent-parameter-transact-sql.md)   
 [sp_drop_agent_parameter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-drop-agent-parameter-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
