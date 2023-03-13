---
title: "sp_syspolicy_subscribe_to_policy_category (Transact-SQL)"
description: "sp_syspolicy_subscribe_to_policy_category (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syspolicy_subscribe_to_policy_category"
  - "sp_syspolicy_subscribe_to_policy_category_TSQL"
helpviewer_keywords:
  - "sp_syspolicy_subscribe_to_policy_category"
dev_langs:
  - "TSQL"
---
# sp_syspolicy_subscribe_to_policy_category (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Adds a policy category subscription for the specified database.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_syspolicy_subscribe_to_policy_category [ @policy_category = ] 'policy_category'  
```  
  
## Arguments  
`[ @policy_category = ] 'policy_category'`
 Is the name of the policy category that you want the database to subscribe to. *policy_category* is **sysname**, and is required.  
  
 To obtain values for *policy_category*, query the msdb.dbo.syspolicy_policy_categories system view.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 You must run sp_syspolicy_subscribe_to_policy_category in the context of the database where you want to add a policy category subscription.  
  
## Permissions  
 Requires membership in the db_owner fixed database role.  
  
## Examples  
 The following example adds a subscription to the 'Finance' policy category for the specified database.  
  
```  
USE <database_name>;  
  
EXEC sys.sp_syspolicy_subscribe_to_policy_category @policy_category = N'Finance';  
  
GO  
```  
  
## See Also  
 [Policy-Based Management Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/policy-based-management-stored-procedures-transact-sql.md)   
 [sp_syspolicy_unsubscribe_from_policy_category &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syspolicy-unsubscribe-from-policy-category-transact-sql.md)  
  
  
