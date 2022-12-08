---
description: "sp_update_operator (Transact-SQL)"
title: "sp_update_operator (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_update_operator_TSQL"
  - "sp_update_operator"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_update_operator"
ms.assetid: 231750a6-4828-4d03-afe6-b91d38c42ed3
author: markingmyname
ms.author: maghan
---
# sp_update_operator (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Updates information about an operator (notification recipient) for use with alerts and jobs.  
  
   ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_update_operator   
     [ @name =] 'name'   
     [ , [ @new_name = ] 'new_name' ]   
     [ , [ @enabled = ] enabled]   
     [ , [ @email_address = ] 'email_address' ]  
     [ , [ @pager_address = ] 'pager_number']   
     [ , [ @weekday_pager_start_time = ] weekday_pager_start_time ]  
     [ , [ @weekday_pager_end_time = ] weekday_pager_end_time ]   
     [ , [ @saturday_pager_start_time = ] saturday_pager_start_time ]  
     [ , [ @saturday_pager_end_time = ] saturday_pager_end_time ]   
     [ , [ @sunday_pager_start_time = ] sunday_pager_start_time ]  
     [ , [ @sunday_pager_end_time = ] sunday_pager_end_time ]   
     [ , [ @pager_days = ] pager_days ]   
     [ , [ @netsend_address = ] 'netsend_address' ]   
     [ , [ @category_name = ] 'category' ]  
```  
  
## Arguments  
 [ @name=] '*name*'  
 The name of the operator to modify. *name* is **sysname**, with no default.  
  
 [ @new_name=] '*new_name*'  
 The new name for the operator. This name must be unique. *new_name* is **sysname**, with a default of NULL.  
  
 [ @enabled=] *enabled*  
 A number indicating the operator's current status (**1** if currently enabled, **0** if not). *enabled* is **tinyint**, with a default of NULL. If not enabled, an operator will not receive alert notifications.  
  
 [ @email_address=] '*email_address*'  
 The e-mail address of the operator. This string is passed directly to the e-mail system. *email_address* is **nvarchar(100)**, with a default of NULL.  
  
 [ @pager_address=] '*pager_number*'  
 The pager address of the operator. This string is passed directly to the e-mail system. *pager_number* is **nvarchar(100)**, with a default of NULL.  
  
 [ @weekday_pager_start_time=] *weekday_pager_start_time*  
 Specifies the time after which a pager notification can be sent to this operator, from Monday through Friday. *weekday_pager_start_time*is **int**, with a default of NULL, and must be entered in the form HHMMSS for use with a 24-hour clock.  
  
 [ @weekday_pager_end_time=] *weekday_pager_end_time*  
 Specifies the time after which a pager notification cannot be sent to the specified operator, from Monday through Friday. *weekday_pager_end_time*is **int**, with a default of NULL, and must be entered in the form HHMMSS for use with a 24-hour clock.  
  
 [ @saturday_pager_start_time=] *saturday_pager_start_time*  
 Specifies the time after which a pager notification can be sent to the specified operator on Saturdays. *saturday_pager_start_time*is **int**, with a default of NULL, and must be entered in the form HHMMSS for use with a 24-hour clock.  
  
 [ @saturday_pager_end_time=] *saturday_pager_end_time*  
 Specifies the time after which a pager notification cannot be sent to the specified operator on Saturdays. *saturday_pager_end_time*is **int**, with a default of NULL, and must be entered in the form HHMMSS for use with a 24-hour clock.  
  
 [ @sunday_pager_start_time=] *sunday_pager_start_time*  
 Specifies the time after which a pager notification can be sent to the specified operator on Sundays. *sunday_pager_start_time*is **int**, with a default of NULL, and must be entered in the form HHMMSS for use with a 24-hour clock.  
  
 [ @sunday_pager_end_time=] *sunday_pager_end_time*  
 Specifies the time after which a pager notification cannot be sent to the specified operator on Sundays. *sunday_pager_end_time*is **int**, with a default of NULL, and must be entered in the form HHMMSS for use with a 24-hour clock.  
  
 [ @pager_days=] *pager_days*  
 Specifies the days that the operator is available to receive pages (subject to the specified start/end times). *pager_days*is **tinyint**, with a default of NULL, and must be a value from **0** through **127**. *pager_days* is calculated by adding the individual values for the required days. For example, from Monday through Friday is **2**+**4**+**8**+**16**+**32** = **64**.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Sunday|  
|**2**|Monday|  
|**4**|Tuesday|  
|**8**|Wednesday|  
|**16**|Thursday|  
|**32**|Friday|  
|**64**|Saturday|  
  
 [ @netsend_address=] '*netsend_address*'  
 The network address of the operator to whom the network message is sent. *netsend_address*is **nvarchar(100)**, with a default of NULL.  
  
 [ @category_name=] '*category*'  
 The name of the category for this alert. *category* is **sysname**, with a default of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 sp_update_operator must be run from the msdb database.  
  
## Permissions  
 Permissions to execute this procedure default to members of the sysadmin fixed server role.  
  
## Examples  
 The following example updates the operator status to enabled, and sets the days (from Monday through Friday, from 8 A.M. through 5 P.M.) when the operator can be paged.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_update_operator   
    @name = N'François Ajenstat',  
    @enabled = 1,  
    @email_address = N'françoisa',  
    @pager_address = N'5551290AW@pager.Adventure-Works.com',  
    @weekday_pager_start_time = 080000,  
    @weekday_pager_end_time = 170000,  
    @pager_days = 64 ;  
GO  
```  
  
## See Also  
 [sp_add_operator &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-operator-transact-sql.md)   
 [sp_delete_operator &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-operator-transact-sql.md)   
 [sp_help_operator &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-operator-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
