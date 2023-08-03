---
title: "sp_drop_agent_parameter (Transact-SQL)"
description: "sp_drop_agent_parameter (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_drop_agent_parameter_TSQL"
  - "sp_drop_agent_parameter"
helpviewer_keywords:
  - "sp_drop_agent_parameter"
dev_langs:
  - "TSQL"
---
# sp_drop_agent_parameter (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Drops one or all parameters from a profile in the **MSagent_parameters** table. This stored procedure is executed at the Distributor where the agent is running, on any database.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_drop_agent_parameter [ @profile_id = ] profile_id  
    [ , [ @parameter_name = ] 'parameter_name' ]  
```  
  
## Arguments  
`[ @profile_id = ] profile_id`
 Is the ID of the profile for which a parameter is to be dropped. *profile_id* is **int**, with no default.  
  
`[ @parameter_name = ] 'parameter_name'`
 Is the name of the parameter to be dropped. *parameter_name* is **sysname**, with a default of **%**. If **%**, all parameters for the specified profile are dropped.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_drop_agent_parameter** is used in all types of replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_drop_agent_parameter**.  
  
## See Also  
 [sp_add_agent_parameter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-agent-parameter-transact-sql.md)   
 [sp_help_agent_parameter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-agent-parameter-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
