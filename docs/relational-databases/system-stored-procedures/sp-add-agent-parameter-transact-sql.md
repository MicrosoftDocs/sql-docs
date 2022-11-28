---
description: "sp_add_agent_parameter (Transact-SQL)"
title: "sp_add_agent_parameter (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_add_agent_parameter_TSQL"
  - "sp_add_agent_parameter"
helpviewer_keywords: 
  - "sp_add_agent_parameter"
ms.assetid: 055f4765-0574-47c3-bf7d-6ef6e9bd8b34
author: mashamsft
ms.author: mathoma
---
# sp_add_agent_parameter (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Adds a new parameter and its value to an agent profile. This stored procedure is executed at the Distributor on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_add_agent_parameter [ @profile_id = ] profile_id  
        , [ @parameter_name = ] 'parameter_name'   
        , [ @parameter_value = ] 'parameter_value'   
```  
  
## Arguments  
`[ @profile_id = ] profile_id`
 Is the ID of the profile from the **MSagent_profiles** table in the **msdb** database. *profile_id* is **int**, with no default.  
  
 To find out what agent type this *profile_id* represents, find the *profile_id* in the [MSagent_profiles &#40;Transact-SQL&#41;](../../relational-databases/system-tables/msagent-profiles-transact-sql.md) table, and note the *agent_type* field value. The values are as follows:  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Snapshot Agent|  
|**2**|Log Reader Agent|  
|**3**|Distribution Agent|  
|**4**|Merge Agent|  
|**9**|Queue Reader Agent|  
  
`[ @parameter_name = ] 'parameter_name'`
 Is the name of the parameter. *parameter_name* is **sysname**, with no default. For a list of parameters already defined in system profiles, see [Replication Agent Profiles](../../relational-databases/replication/agents/replication-agent-profiles.md). For a complete list of valid parameters for each agent, see the following topics:  
  
-   [Replication Snapshot Agent](../../relational-databases/replication/agents/replication-snapshot-agent.md)  
  
-   [Replication Log Reader Agent](../../relational-databases/replication/agents/replication-log-reader-agent.md)  
  
-   [Replication Distribution Agent](../../relational-databases/replication/agents/replication-distribution-agent.md)  
  
-   [Replication Merge Agent](../../relational-databases/replication/agents/replication-merge-agent.md)  
  
-   [Replication Queue Reader Agent](../../relational-databases/replication/agents/replication-queue-reader-agent.md)  
  
`[ @parameter_value = ] 'parameter_value'`
 Is the value to be assigned to the parameter. *parameter_value* is **nvarchar(255)**, with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_add_agent_parameter** is used in snapshot replication, transactional replication, and merge replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_add_agent_parameter**.  
  
## See Also  
 [Work with Replication Agent Profiles](../../relational-databases/replication/agents/work-with-replication-agent-profiles.md)   
 [Replication Agent Profiles](../../relational-databases/replication/agents/replication-agent-profiles.md)   
 [sp_add_agent_profile &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-agent-profile-transact-sql.md)   
 [sp_change_agent_profile &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-change-agent-profile-transact-sql.md)   
 [sp_change_agent_parameter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-change-agent-parameter-transact-sql.md)   
 [sp_drop_agent_parameter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-drop-agent-parameter-transact-sql.md)   
 [sp_help_agent_parameter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-agent-parameter-transact-sql.md)  
  
  
