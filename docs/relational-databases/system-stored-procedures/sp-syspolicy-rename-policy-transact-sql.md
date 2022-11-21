---
description: "sp_syspolicy_rename_policy (Transact-SQL)"
title: "sp_syspolicy_rename_policy (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_syspolicy_rename_policy_TSQL"
  - "sp_syspolicy_rename_policy"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_syspolicy_rename_policy"
ms.assetid: ce2b07f5-23b1-4f49-8e7b-c18cf3f3d45b
author: VanMSFT
ms.author: vanto
---
# sp_syspolicy_rename_policy (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Renames an existing policy in Policy-Based Management.  
  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_syspolicy_rename_policy { [ @name = ] 'name' | [ @policy_id = ] policy_id }  
    , [ @new_name = ] 'new_name'  
```  
  
## Arguments  
`[ @name = ] 'name'`
 Is the name of the policy that you want to rename. *name* is **sysname**, and must be specified if *policy_id* is NULL.  
  
`[ @policy_id = ] policy_id`
 Is the identifier for the policy that you want to rename. *policy_id* is **int**, and must be specified if *name* is NULL.  
  
`[ @new_name = ] 'new_name'`
 Is the new name for the policy. *new_name* is **sysname**, and is required. Cannot be NULL or an empty string.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 You must run sp_syspolicy_rename_policy in the context of the msdb system database.  
  
 You must specify a value for either *name* or *policy_id*. Both cannot be NULL. To obtain these values, query the msdb.dbo.syspolicy_policies system view.  
  
## Permissions  
 Requires membership in the PolicyAdministratorRole fixed database role.  
  
> [!IMPORTANT]  
>  Possible elevation of credentials: Users in the PolicyAdministratorRole role can create server triggers and schedule policy executions that can affect the operation of the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. For example, users in the PolicyAdministratorRole role can create a policy that can prevent most objects from being created in the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Because of this possible elevation of credentials, the PolicyAdministratorRole role should be granted only to users who are trusted with controlling the configuration of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
## Examples  
 The following example renames a policy that is named 'Test Policy 1' to 'Test Policy 2'.  
  
```  
EXEC msdb.dbo.sp_syspolicy_rename_policy @name = N'Test Policy 1'  
, @new_name = N'Test Policy 2';  
  
GO  
```  
  
## See Also  
 [Policy-Based Management Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/policy-based-management-stored-procedures-transact-sql.md)  
  
  
