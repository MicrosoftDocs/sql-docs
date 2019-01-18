---
title: "syspolicy_policy_category_subscriptions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "syspolicy_policy_category_subscriptions_TSQL"
  - "syspolicy_policy_category_subscriptions"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "syspolicy_policy_group_subscriptions view"
ms.assetid: b3b3a7d7-0b78-46c0-9755-045f7a5692b9
author: VanMSFT
ms.author: vanto
manager: craigg
---
# syspolicy_policy_category_subscriptions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Displays one row for each Policy-Based Management subscription in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Each row describes a target and policy category pair. The following table describes the columns in the syspolicy_policy_group_subscriptions view.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|policy_category_subscription_id|**int**|Identifier of this record.|  
|target_type|**sysname**|Type of database object that is the target of this subscription.|  
|target_object|**sysname**|Name of the target object.|  
|policy_category_id|**int**|ID of the policy category that is applied to the target.|  
  
## Remarks  
 This view shows the targets that are subscribed to policy categories.  
  
## Permissions  
 Requires membership in the PolicyAdministratorRole role in the msdb database.  
  
## See Also  
 [Administer Servers by Using Policy-Based Management](../../relational-databases/policy-based-management/administer-servers-by-using-policy-based-management.md)   
 [Policy-Based Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/policy-based-management-views-transact-sql.md)  
  
  
