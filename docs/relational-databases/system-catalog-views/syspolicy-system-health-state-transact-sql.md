---
title: "syspolicy_system_health_state (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "syspolicy_system_health_state_TSQL"
  - "syspolicy_system_health_state"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "syspolicy_system_health_state view"
ms.assetid: 00815106-9fe4-481d-a9e1-a256101887f4
author: VanMSFT
ms.author: vanto
manager: craigg
---
# syspolicy_system_health_state (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Displays one row for each Policy-Based Management policy and target query expression combination. Use the syspolicy_system_health_state view to programmatically check the policy health of the server. The following table describes the columns in the syspolicy_system_health_state view.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|health_state_id|**bigint**|Identifier of the policy health state record.|  
|policy_id|**int**|Identifier of the policy.|  
|last_run_date|**datetime**|Date and time the policy was last run.|  
|target_query_expression_with_id|**nvarchar(400)**|The target expression, with values assigned to identity variables, that defines the target against which the policy is evaluated.|  
|target_query_expression|**nvarchar(max)**|The epxression that defines the target against which the policy is evaluated.|  
|result|**bit**|Health state of this target with regard to the policy:<br /><br /> 0 = Failure<br /><br /> 1 = Success|  
  
## Remarks  
 The syspolicy_system_health_state view displays the most recent health state of target query expression for each active (enabled) policy. The [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Object Explorer and Object Explorer Details page aggregates policy health from this view to show the critical health state.  
  
## Permissions  
 Requires membership in the PolicyAdministratorRole role in the msdb database.  
  
## See Also  
 [Administer Servers by Using Policy-Based Management](../../relational-databases/policy-based-management/administer-servers-by-using-policy-based-management.md)   
 [Policy-Based Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/policy-based-management-views-transact-sql.md)  
  
  
