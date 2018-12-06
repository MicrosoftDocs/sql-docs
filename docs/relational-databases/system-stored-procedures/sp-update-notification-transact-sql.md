---
title: "sp_update_notification (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_update_notification_TSQL"
  - "sp_update_notification"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_updatenotification"
ms.assetid: 3e1c3d40-8c24-46ce-a68e-ce6c6a237fda
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_update_notification (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Updates the notification method of an alert notification.  

  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_update_notification  
          [@alert_name =] 'alert' ,  
     [@operator_name =] 'operator' ,  
     [@notification_method =] notification  
```  
  
## Arguments  
 [ **@alert_name =**] **'***alert***'**  
 The name of the alert associated with this notification. *alert* is **sysname**, with no default.  
  
 [ **@operator_name =**]  **'***operator***'**  
 The operator who will be notified when the alert occurs. *operator* is **sysname**, with no default.  
  
 [ **@notification_method =**] *notification*  
 The method by which the operator is notified. *notification*is **tinyint**, with no default, and can be one or more of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|E-mail|  
|**2**|Pager|  
|**4**|**net send**|  
|**7**|All methods|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_update_notification** must be run from the **msdb** database.  
  
 You can update a notification for an operator who does not have the necessary address information using the specified *notification_method*. If a failure occurs when sending an e-mail message or pager notification, the failure is reported in the Microsoft SQL Server Agent error log.  
  
## Permissions  
 To run this stored procedure, users must be granted the **sysadmin** fixed server role.  
  
## Examples  
 The following example modifies the notification method for notifications sent to `François Ajenstat`for the alert `Test Alert`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_update_notification  
   @alert_name = N'Test Alert',  
   @operator_name = N'François Ajenstat',  
   @notification_method = 7;  
GO  
```  
  
## See Also  
 [sp_add_notification &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-notification-transact-sql.md)   
 [sp_delete_notification &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-notification-transact-sql.md)   
 [sp_help_notification &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-notification-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
