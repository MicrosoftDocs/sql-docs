---
description: "sp_syspolicy_rename_condition (Transact-SQL)"
title: "sp_syspolicy_rename_condition (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_syspolicy_rename_condition"
  - "sp_syspolicy_rename_condition_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_syspolicy_rename_condition"
ms.assetid: d9f3f9b1-701b-4fce-9b42-c282656caf84
author: VanMSFT
ms.author: vanto
---
# sp_syspolicy_rename_condition (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Renames an existing condition in Policy-Based Management.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_syspolicy_rename_condition { [ @name = ] 'name' | [ @condition_id = ] condition_id }  
    , [ @new_name = ] 'new_name'  
```  
  
## Arguments  
`[ @name = ] 'name'`
 Is the name of the condition that you want to rename. *name* is **sysname**, and must be specified if *condition_id* is NULL.  
  
`[ @condition_id = ] condition_id`
 Is the identifier for the condition that you want to rename. *condition_id* is **int**, and must be specified if *name* is NULL.  
  
`[ @new_name = ] 'new_name'`
 Is the new name of the condition. *new_name* is **sysname**, and is required. Cannot be NULL or an empty string.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 You must run sp_syspolicy_rename_condition in the context of the msdb system database.  
  
 You must specify a value for either *name* or *condition_id*. Both cannot be NULL. To obtain these values, query the msdb.dbo.syspolicy_conditions system view.  
  
## Permissions  
 Requires membership in the PolicyAdministratorRole fixed database role.  
  
> [!IMPORTANT]  
>  Possible elevation of credentials: Users in the PolicyAdministratorRole role can create server triggers and schedule policy executions that can affect the operation of the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. For example, users in the PolicyAdministratorRole role can create a policy that can prevent most objects from being created in the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Because of this possible elevation of credentials, the PolicyAdministratorRole role should be granted only to users who are trusted with controlling the configuration of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
## Examples  
 The following example renames a condition that is named 'Change Tracking Enabled'.  
  
```  
EXEC msdb.dbo.sp_syspolicy_rename_condition @name = N'Change Tracking Enabled'  
, @new_name = N'Verify Change Tracking Enabled';  
  
GO  
```  
  
## See Also  
 [Policy-Based Management Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/policy-based-management-stored-procedures-transact-sql.md)  
  
  
