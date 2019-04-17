---
title: "sp_help_notification (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_help_notification"
  - "sp_help_notification_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_notification"
ms.assetid: 0273457f-9d2a-4a6f-9a16-6a6bf281cba0
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_help_notification (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Reports a list of alerts for a given operator or a list of operators for a given alert.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_notification  
     [ @object_type = ] 'object_type' ,  
     [ @name = ] 'name' ,  
     [ @enum_type = ] 'enum_type' ,   
     [ @notification_method = ] notification_method   
     [ , [ @target_name = ] 'target_name' ]   
```  
  
## Arguments  
`[ @object_type = ] 'object_type'`
 The type of information to be returned. *object_type*is **char(9)**, with no default. *object_type* can be ALERTS, which lists the alerts assigned to the supplied operator name*,* or OPERATORS, which lists the operators responsible for the supplied alert name*.*  
  
`[ @name = ] 'name'`
 An operator name (if *object_type* is OPERATORS) or an alert name (if *object_type* is ALERTS). *name* is **sysname**, with no default.  
  
`[ @enum_type = ] 'enum_type'`
 The *object_type*information that is returned. *enum_type* is ACTUAL in most cases. *enum_type*is **char(10)**, with no default, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|ACTUAL|Lists only the *object_types* associated with *name*.|  
|ALL|Lists all the*object_types* including those that are not associated with *name*.|  
|TARGET|Lists only the *object_types* matching the supplied *target_name*, regardless of association with*name*.|  
  
`[ @notification_method = ] notification_method`
 A numeric value that determines the notification method columns to return. *notification_method* is **tinyint**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|E-mail: returns only the **use_email** column.|  
|**2**|Pager: returns only the **use_pager** column.|  
|**4**|NetSend: returns only the **use_netsend** column.|  
|**7**|All: returns all columns.|  
  
`[ @target_name = ] 'target_name'`
 An alert name to search for (if *object_type* is ALERTS) or an operator name to search for (if *object_type* is OPERATORS). *target_name* is needed only if *enum_type* is TARGET. *target_name* is **sysname**, with a default of NULL.  
  
## Return Code Valves  
 0 (success) or 1 (failure)  
  
## Result Sets  
 If *object_type* is **ALERTS**, the result set lists all the alerts for a given operator.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**alert_id**|**int**|Alert identifier number.|  
|**alert_name**|**sysname**|Alert name.|  
|**use_email**|**int**|E-mail is used to notify the operator:<br /><br /> **1** = Yes<br /><br /> **0** = No|  
|**use_pager**|**int**|Pager is used to notify operator:<br /><br /> **1** = Yes<br /><br /> **0** = No|  
|**use_netsend**|**int**|Network pop-up is used to notify the operator:<br /><br /> **1** = Yes<br /><br /> **0** = No|  
|**has_email**|**int**|Number of e-mail notifications sent for this alert.|  
|**has_pager**|**int**|Number of pager notifications sent for this alert.|  
|**has_netsend**|**int**|Number of **net send** notifications sent for this alert.|  
  
 If **object_type** is **OPERATORS**, the result set lists all the operators for a given alert.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**operator_id**|**int**|Operator identification number.|  
|**operator_name**|**sysname**|Operator name.|  
|**use_email**|**int**|E-mail is used to send notification of the operator:<br /><br /> **1** = Yes<br /><br /> **0** = No|  
|**use_pager**|**int**|Pager is used to send notification of the operator:<br /><br /> **1** = Yes<br /><br /> **0** = No|  
|**use_netsend**|**int**|Is a network pop-up used to notify the operator:<br /><br /> **1** = Yes<br /><br /> **0** = No|  
|**has_email**|**int**|Operator has an e-mail address:<br /><br /> **1** = Yes<br /><br /> **0** = No|  
|**has_pager**|**int**|Operator has a pager address:<br /><br /> **1** = Yes<br /><br /> **0** = No|  
|**has_netsend**|**int**|Operator has net send notification configured.<br /><br /> **1** = Yes<br /><br /> **0** = No|  
  
## Remarks  
 This stored procedure must be run from the **msdb** database.  
  
## Permissions  
 To execute this stored procedure, a user must be a member of the **sysadmin** fixed server role.  
  
## Examples  
  
### A. Listing alerts for a specific operator  
 The following example returns all alerts for which the operator `François Ajenstat` receives any kind of notification.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_notification   
    @object_type = N'ALERTS',  
    @name = N'François Ajenstat',  
    @enum_type = N'ACTUAL',  
    @notification_method = 7 ;  
GO  
```  
  
### B. Listing operators for a specific alert  
 The following example returns all operators who receive any kind of notification for the `Test Alert` alert.  
  
```  
USE msdb ;  
GO  
  
EXEC sp_help_notification  
    @object_type = N'OPERATORS',  
    @name = N'Test Alert',  
    @enum_type = N'ACTUAL',  
    @notification_method = 7 ;  
GO  
```  
  
## See Also  
 [sp_add_notification &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-notification-transact-sql.md)   
 [sp_delete_notification &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-notification-transact-sql.md)   
 [sp_update_notification &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-notification-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
