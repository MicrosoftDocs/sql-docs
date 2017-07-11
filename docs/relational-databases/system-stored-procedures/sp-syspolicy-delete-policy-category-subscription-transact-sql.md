---
title: "sp_syspolicy_delete_policy_category_subscription (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sp_syspolicy_delete_policy_category_subscription_TSQL"
  - "sp_syspolicy_delete_policy_category_subscription"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_syspolicy_delete_policy_category_subscription"
ms.assetid: eeab0120-c869-4c95-a79d-6dc418d0b23a
caps.latest.revision: 7
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# sp_syspolicy_delete_policy_category_subscription (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Deletes a policy category subscription for a specific database.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)).|  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_syspolicy_delete_policy_category_subscription [ @policy_category_subscription_id = ] policy_category_subscription_id  
```  
  
## Arguments  
 [ **@policy_category_subscription_id=** ] *policy_category_subscription_id*  
 Is the identifier for the policy category subscription. *policy_category_subscription_id* is **int**.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 You must run sp_syspolicy_delete_policy_category_subscription in the context of the msdb system database.  
  
 You cannot delete a policy category subscription when the subscription is mandated.  
  
## Permissions  
 This stored procedure runs in the context of the current owner of the stored procedure.  
  
 To obtain values for *policy_category_subscription_id*, you can use the following query:  
  
```  
SELECT a.policy_category_subscription_id, a.target_object, b.name AS category_name  
FROM msdb.dbo.syspolicy_policy_category_subscriptions AS a  
INNER JOIN msdb.dbo.syspolicy_policy_categories AS b  
ON a.policy_category_id = b.policy_category_id;  
```  
  
## Examples  
 The following example deletes a policy category subscription with an ID of 1.  
  
```  
EXEC msdb.dbo.sp_syspolicy_delete_policy_category_subscription @policy_category_subscription_id = 1;  
  
GO  
```  
  
## See Also  
 [Policy-Based Management Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/policy-based-management-stored-procedures-transact-sql.md)   
 [sp_syspolicy_update_policy_category_subscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syspolicy-update-policy-category-subscription-transact-sql.md)  
  
  