---
title: "sp_update_agent_profile (Transact-SQL)"
description: "sp_update_agent_profile (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_update_agent_profile_TSQL"
  - "sp_update_agent_profile"
helpviewer_keywords:
  - "sp_update_agent_profile"
dev_langs:
  - "TSQL"
---
# sp_update_agent_profile (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Updates the profile used by a replication agent. This stored procedure is executed at the Distributor on the distribution database.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_update_agent_profile [@agent_type=] agent_type, [ @agent_id= ] agent_id, [ @profile_id= ] profile_id  
```  
  
## Arguments  
`[ @agent_type = ] 'agent_type'`
 Is the type of agent. *agent_type* is **int**, with no default, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Snapshot Agent.|  
|**2**|Log Reader Agent.|  
|**3**|Distribution Agent.|  
|**4**|Merge Agent.|  
|**9**|Queue Reader Agent.|  
  
`[ @agent_id = ] 'agent_id'` 
 Is the ID of the agent. *agent_id* is **int**, with no default.  
  
`[ @profile_id = ] 'profile_id'` 
 Is the ID of the profile that the agent should use. *profile_id* is **int**, with no default. To view a list of profiles defined for each agent, use [sp_help_agent_profile &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-agent-profile-transact-sql.md). For more information about system profiles, see [Replication Agent Profiles](../../relational-databases/replication/agents/replication-agent-profiles.md).  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_update_agent_profile** is used in all types of replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_update_agent_profile**.  
  
## See Also  
 [Replication Agent Profiles](../../relational-databases/replication/agents/replication-agent-profiles.md)   
 [sp_add_agent_profile &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-agent-profile-transact-sql.md)   
 [sp_change_agent_profile &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-change-agent-profile-transact-sql.md)   
 [sp_drop_agent_profile &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-drop-agent-profile-transact-sql.md)   
 [sp_help_agent_profile &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-agent-profile-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
