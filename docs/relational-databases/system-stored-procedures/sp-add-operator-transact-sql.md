---
title: "sp_add_operator (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_add_operator"
  - "sp_add_operator_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_add_operator"
ms.assetid: 817cd98a-4dff-4ed8-a546-f336c144d1e0
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_add_operator (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates an operator (notification recipient) for use with alerts and jobs.  
  
 
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_add_operator [ @name = ] 'name'   
     [ , [ @enabled = ] enabled ]   
     [ , [ @email_address = ] 'email_address' ]   
     [ , [ @pager_address = ] 'pager_address' ]   
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
 [ **@name=** ] **'***name***'**  
 The name of an operator (notification recipient). This name must be unique and cannot contain the percent (**%**) character. *name* is **sysname**, with no default.  
  
 [ **@enabled=** ] *enabled*  
 Indicates the current status of the operator. *enabled* is **tinyint**, with a default of **1** (enabled). If **0**, the operator is not enabled and does not receive notifications.  
  
 [ **@email_address=** ] **'***email_address***'**  
 The e-mail address of the operator. This string is passed directly to the e-mail system. *email_address* is **nvarchar(100)**, with a default of NULL.  
  
 You can specify either a physical e-mail address or an alias for *email_address*. For example:  
  
 '**jdoe**' or '**jdoe@xyz.com**'  
  
> [!NOTE]  
>  You must use the e-mail address for Database Mail.  
  
 [ **@pager_address=** ] **'***pager_address***'**  
 The pager address of the operator. This string is passed directly to the e-mail system. *pager_address* is **narchar(100)**, with a default of NULL.  
  
 [ **@weekday_pager_start_time=** ] *weekday_pager_start_time*  
 The time after which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent sends pager notification to the specified operator on the weekdays, from Monday through Friday. *weekday_pager_start_time*is **int**, with a default of **090000**, which indicates 9:00 A.M. on a 24-hour clock, and must be entered using the form HHMMSS.  
  
 [ **@weekday_pager_end_time=** ] *weekday_pager_end_time*  
 The time after which **SQLServerAgent** service no longer sends pager notification to the specified operator on the weekdays, from Monday through Friday. *weekday_pager_end_time*is **int**, with a default of 180000, which indicates 6:00 P.M. on a 24-hour clock, and must be entered using the form HHMMSS.  
  
 [ **@saturday_pager_start_time =**] *saturday_pager_start_time*  
 The time after which **SQLServerAgent** service sends pager notification to the specified operator on Saturdays. *saturday_pager_start_time* is **int**, with a default of 090000, which indicates 9:00 A.M. on a 24-hour clock, and must be entered using the form HHMMSS.  
  
 [ **@saturday_pager_end_time=** ] *saturday_pager_end_time*  
 The time after which **SQLServerAgent** service no longer sends pager notification to the specified operator on Saturdays. *saturday_pager_end_time*is **int**, with a default of **180000**, which indicates 6:00 P.M. on a 24-hour clock, and must be entered using the form HHMMSS.  
  
 [ **@sunday_pager_start_time=** ] *sunday_pager_start_time*  
 The time after which **SQLServerAgent** service sends pager notification to the specified operator on Sundays. *sunday_pager_start_time*is **int**, with a default of **090000**, which indicates 9:00 A.M. on a 24-hour clock, and must be entered using the form HHMMSS.  
  
 [ **@sunday_pager_end_time =**] *sunday_pager_end_time*  
 The time after which **SQLServerAgent** service no longer sends pager notification to the specified operator on Sundays. *sunday_pager_end_time*is **int**, with a default of **180000**, which indicates 6:00 P.M. on a 24-hour clock, and must be entered using the form HHMMSS.  
  
 [ **@pager_days=** ] *pager_days*  
 Is a number that indicates the days that the operator is available for pages (subject to the specified start/end times). *pager_days*is **tinyint**, with a default of **0** indicating the operator is never available to receive a page. Valid values are from **0** through **127**. *pager_days*is calculated by adding the individual values for the required days. For example, from Monday through Friday is **2**+**4**+**8**+**16**+**32** = **62**. The following table lists the value for each day of the week.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Sunday|  
|**2**|Monday|  
|**4**|Tuesday|  
|**8**|Wednesday|  
|**16**|Thursday|  
|**32**|Friday|  
|**64**|Saturday|  
  
 [ **@netsend_address=** ] **'***netsend_address***'**  
 The network address of the operator to whom the network message is sent. *netsend_address*is **nvarchar(100)**, with a default of NULL.  
  
 [ **@category_name=** ] **'***category***'**  
 The name of the category for this operator. *category* is **sysname**, with a default of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 **sp_add_operator** must be run from the **msdb** database.  
  
 Paging is supported by the e-mail system, which must have an e-mail-to-pager capability if you want to use paging.  
  
 [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides an easy, graphical way to manage jobs, and is the recommended way to create and manage the job infrastructure.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_add_operator**.  
  
## Examples  
 The following example sets up the operator information for `danwi`. The operator is enabled. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent sends notifications by pager from Monday through Friday from 8 A.M. to 5 P.M.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_add_operator  
    @name = N'Dan Wilson',  
    @enabled = 1,  
    @email_address = N'danwi',  
    @pager_address = N'5551290AW@pager.Adventure-Works.com',  
    @weekday_pager_start_time = 080000,  
    @weekday_pager_end_time = 170000,  
    @pager_days = 62 ;  
GO  
```  
  
## See Also  
 [sp_delete_operator &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-operator-transact-sql.md)   
 [sp_help_operator &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-operator-transact-sql.md)   
 [sp_update_operator &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-operator-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
