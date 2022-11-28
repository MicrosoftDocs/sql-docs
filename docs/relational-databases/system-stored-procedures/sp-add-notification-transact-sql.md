---
description: "sp_add_notification (Transact-SQL)"
title: "sp_add_notification (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_add_notification_TSQL"
  - "sp_add_notification"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_add_notification"
ms.assetid: 0525e0a2-ed0b-4e69-8a4c-a9e3e3622fbd
author: MashaMSFT
ms.author: mathoma
---
# sp_add_notification (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Sets up a notification for an alert.  
  
  
## Syntax  
  
```  
  
sp_add_notification [ @alert_name = ] 'alert' ,   
    [ @operator_name = ] 'operator' ,   
    [ @notification_method = ] notification_method  
```  
  
## Arguments  
`[ @alert_name = ] 'alert'`
 The alert for this notification. *alert* is **sysname**, with no default.  
  
`[ @operator_name = ] 'operator'`
 The operator to be notified when the alert occurs. *operator* is **sysname**, with no default.  
  
`[ @notification_method = ] notification_method`
 The method by which the operator is notified. *notification_method* is **tinyint**, with no default. *notification_method* can be one or more of these values combined with an **OR** logical operator.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|E-mail|  
|**2**|Pager|  
|**4**|**net send**|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 **sp_add_notification** must be run from the **msdb** database.  
  
 [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides an easy, graphical way to manage the entire alerting system. Using [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] is the recommended way to configure your alert infrastructure.  
  
 To send a notification in response to an alert, you must first configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to send mail.  
  
 If a failure occurs when sending an e-mail message or pager notification, the failure is reported in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service error log.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_add_notification**.  
  
## Examples  
 The following example adds an e-mail notification for the specified alert (`Test Alert`).  
  
> [!NOTE]  
> This example assumes that `Test Alert` already exists and that `François Ajenstat` is a valid operator name.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_add_notification  
 @alert_name = N'Test Alert',  
 @operator_name = N'François Ajenstat',  
 @notification_method = 1 ;  
GO  
```  
  
## See also  
 [sp_delete_notification &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-notification-transact-sql.md)   
 [sp_help_notification &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-notification-transact-sql.md)   
 [sp_update_notification &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-notification-transact-sql.md)   
 [sp_add_operator &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-operator-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
