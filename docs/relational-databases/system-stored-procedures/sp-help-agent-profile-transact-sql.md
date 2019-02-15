---
title: "sp_help_agent_profile (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_help_agent_profile"
  - "sp_help_agent_profile_TSQL"
helpviewer_keywords: 
  - "sp_help_agent_profile"
ms.assetid: 5637b671-4aa3-497e-9a1c-c99798a1afb4
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_help_agent_profile (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Displays the profile of a specified agent. This stored procedure is executed at the Distributor on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_agent_profile [ [ @agent_type = ] agent_type ]   
    [ , [ @profile_id = ] profile_id ]  
```  
  
## Arguments  
 [ **@agent_type=**] *agent_type*  
 Is the type of agent. *agent_type* is **int**, with a default of **0**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Snapshot Agent|  
|**2**|Log Reader Agent|  
|**3**|Distribution Agent|  
|**4**|Merge Agent|  
|**9**|Queue Reader Agent|  
  
 [ **@profile_id=**] *profile_id*  
 Is the ID of the profile to be displayed. *profile_id* is **int**, with a default of **-1**, which returns all the profiles in the **MSagent_profiles** table.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**profile_id**|**int**|ID of the profile.|  
|**profile_name**|**sysname**|Unique for agent type.|  
|**agent_type**|**int**|**1** = Snapshot Agent<br /><br /> **2** = Log Reader Agent<br /><br /> **3** = Distribution Agent<br /><br /> **4** = Merge Agent<br /><br /> **9** = Queue Reader Agent|  
|**Type**|**int**|**0** = System<br /><br /> **1** = Custom|  
|**description**|**varchar(3000)**|Description of the profile.|  
|**def_profile**|**bit**|Specifies whether this profile is the default for this agent type.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_help_agent_profile** is used in all types of replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **replmonitor** fixed database role can execute **sp_help_agent_profile**.  
  
## See Also  
 [Work with Replication Agent Profiles](../../relational-databases/replication/agents/work-with-replication-agent-profiles.md)   
 [sp_add_agent_profile &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-agent-profile-transact-sql.md)   
 [sp_drop_agent_profile &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-drop-agent-profile-transact-sql.md)   
 [sp_help_agent_parameter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-agent-parameter-transact-sql.md)  
  
  
