---
title: "syspolicy_conditions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "syspolicy_conditions"
  - "syspolicy_conditions_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "syspolicy_conditions view"
ms.assetid: af97d26c-4bd5-4b08-be51-8419e3b2832c
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# syspolicy_conditions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Displays one row for each Policy-Based Management condition in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. syspolicy_conditions belongs to the dbo schema in the msdb database. The following table describes the columns in the syspolicy_conditions view.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|condition_id|**int**|Identifier of this condition. Each condition represents a collection of one or more condition expressions.|  
|name|**sysname**|Name of the condition.|  
|date_created|**datetime**|Date and time the condition was created.|  
|description|**nvarchar(max)**|Description of the condition. The description column is optional and can be NULL.|  
|created_by|**sysname**|Login that created the condition.|  
|modified_by|**sysname**|Login that most recently modified the condition. Is NULL if never modified.|  
|date_modified|**datetime**|Date and time the condition was created. Is NULL if never modified.|  
|is_name_condition|**smallint**|Specifies whether the condition is a naming condition.<br /><br /> 0 = The condition expression does not contain the @Name variable.<br /><br /> 1 = The condition expression contains the @Name variable.|  
|facet|**nvarchar(max)**|Name of the facet that the condition is based on.|  
|Expression|**nvarchar(max)**|Expression of the facet states.|  
|obj_name|**sysname**|The object name assigned to @Name if the condition expression contains this variable.|  
  
## Remarks  
 When you are troubleshooting Policy-Based Management, query the syspolicy_conditions view to determine who created or last changed the condition.  
  
## Permissions  
 Requires membership in the PolicyAdministratorRole role in the msdb database.  
  
## See Also  
 [Administer Servers by Using Policy-Based Management](../../relational-databases/policy-based-management/administer-servers-by-using-policy-based-management.md)   
 [Policy-Based Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/policy-based-management-views-transact-sql.md)  
  
  
