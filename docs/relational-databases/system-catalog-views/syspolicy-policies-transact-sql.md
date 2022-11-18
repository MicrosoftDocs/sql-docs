---
title: "syspolicy_policies (Transact-SQL)"
description: syspolicy_policies (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "syspolicy_policies_TSQL"
  - "syspolicy_policies"
helpviewer_keywords:
  - "syspolicy_policies view"
dev_langs:
  - "TSQL"
ms.assetid: aecf35bb-187e-4f80-870f-48081b88974e
---
# syspolicy_policies (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Displays one row for each Policy-Based Management policy in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. syspolicy_policies belongs to the dbo schema in the msdb database. The following table describes the columns in the syspolicy_policies view.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|policy_id|**int**|Identifier of the policy.|  
|name|**sysname**|Name of the policy.|  
|condition_id|**int**|ID of the condition enforced or tested by this policy.|  
|root_condition_id|**int**|For internal use only.|  
|date_created|**datetime**|Date and time the policy was created.|  
|execution_mode|**int**|Evaluation mode for the policy. Possible values are as follows:<br /><br /> 0 = On demand<br /><br /> This mode evaluates the policy when directly specified by the user.<br /><br /> 1 = On change: prevent<br /><br /> This automated mode uses DDL triggers to prevent policy violations.<br /><br /> 2 = On change: log only<br /><br /> This automated mode uses event notification to evaluate a policy when a relevant change occurs and logs policy violations.<br /><br /> 4 = On schedule<br /><br /> This automated mode uses a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job to periodically evaluate a policy. The mode logs policy violations.<br /><br /> Note: The value 3 is not a possible value.|  
|policy_category|**int**|ID of the Policy-Based Management policy category that this policy belongs to. Is NULL if it is the default policy group.|  
|schedule_uid|**uniqueidentifier**|When the execution_mode is On schedule, contains the ID of the schedule; otherwise, is NULL.|  
|description|**nvarchar(max)**|Description of the policy. The description column is optional and can be NULL.|  
|help_text|**nvarchar(4000)**|The hyperlink text that belongs to help_link.|  
|help_link|**nvarchar(2083)**|The additional help hyperlink that is assigned to the policy by the policy creator.|  
|object_set_id|**int**|ID of the object set that the policy evaluates.|  
|is_enabled|**bit**|Indicates whether the policy is currently enabled (1) or disabled (0).|  
|job_id|**uniqueidentifier**|When the execution_mode is On schedule, contains the ID of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job that runs the policy.|  
|created_by|**sysname**|Login that created the policy.|  
|modified_by|**sysname**|Login that most recently modified the policy. Is NULL if never modified.|  
|date_modified|**datetime**|Date and time the policy was created. Is NULL if never modified.|  
  
## Remarks  
 When you are troubleshooting Policy-Based Management, query the [syspolicy_conditions](../../relational-databases/system-catalog-views/syspolicy-conditions-transact-sql.md) view to determine whether the policy is enabled. This view also displays who created or last changed the policy.  
  
## Permissions  
 Requires membership in the PolicyAdministratorRole role in the msdb database.  
  
## See Also  
 [Administer Servers by Using Policy-Based Management](../../relational-databases/policy-based-management/administer-servers-by-using-policy-based-management.md)   
 [Policy-Based Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/policy-based-management-views-transact-sql.md)  
  
  
