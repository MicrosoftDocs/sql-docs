---
title: "sp_change_agent_profile (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_change_agent_profile"
  - "sp_change_agent_profile_TSQL"
helpviewer_keywords: 
  - "sp_change_agent_profile"
ms.assetid: e73acf8d-0be8-4197-ba11-fe798d0e2820
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_change_agent_profile (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes a parameter of a replication agent profile stored in the [MSagent_profiles &#40;Transact-SQL&#41;](../../relational-databases/system-tables/msagent-profiles-transact-sql.md) table. This stored procedure is executed at the Distributor on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_change_agent_profile [ @profile_id = ] profile_id   
        , [ @property = ] 'property'   
        , [ @value = ] 'value'   
```  
  
## Arguments  
 [ **@profile_id=** ] *profile_id*  
 Is the ID of the profile. *profile_id* is **int**, with no default.  
  
 [ **@property=** ] **'***property***'**  
 Is the name of the property. *property* is **sysname**, with no default.  
  
 [ **@value=** ] **'***value***'**  
 Is the new value of the property. *value* is **nvarchar(3000)**, with no default.  
  
 This table describes the profile properties that can be changed.  
  
|Property|Description|  
|--------------|-----------------|  
|**description**|Description for the profile.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_change_agent_profile** is used in all types of replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_change_agent_profile**.  
  
## See Also  
 [sp_add_agent_profile &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-agent-profile-transact-sql.md)   
 [sp_drop_agent_profile &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-drop-agent-profile-transact-sql.md)   
 [sp_help_agent_profile &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-agent-profile-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
