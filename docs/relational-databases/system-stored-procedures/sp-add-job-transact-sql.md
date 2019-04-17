---
title: "sp_add_job (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_add_job_TSQL"
  - "sp_add_job"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_add_job"
ms.assetid: 6ca8fe2c-7b1c-4b59-b4c7-e3b7485df274
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_add_job (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds a new job executed by the SQLServerAgent service.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_add_job [ @job_name = ] 'job_name'  
     [ , [ @enabled = ] enabled ]   
     [ , [ @description = ] 'description' ]   
     [ , [ @start_step_id = ] step_id ]   
     [ , [ @category_name = ] 'category' ]   
     [ , [ @category_id = ] category_id ]   
     [ , [ @owner_login_name = ] 'login' ]   
     [ , [ @notify_level_eventlog = ] eventlog_level ]   
     [ , [ @notify_level_email = ] email_level ]   
     [ , [ @notify_level_netsend = ] netsend_level ]   
     [ , [ @notify_level_page = ] page_level ]   
     [ , [ @notify_email_operator_name = ] 'email_name' ]   
          [ , [ @notify_netsend_operator_name = ] 'netsend_name' ]   
     [ , [ @notify_page_operator_name = ] 'page_name' ]   
     [ , [ @delete_level = ] delete_level ]   
     [ , [ @job_id = ] job_id OUTPUT ]   
```  
  
## Arguments  
`[ @job_name = ] 'job_name'`
 The name of the job. The name must be unique and cannot contain the percent (**%**) character. *job_name*is **nvarchar(128)**, with no default.  
  
`[ @enabled = ] enabled`
 Indicates the status of the added job. *enabled*is **tinyint**, with a default of 1 (enabled). If **0**, the job is not enabled and does not run according to its schedule; however, it can be run manually.  
  
`[ @description = ] 'description'`
 The description of the job. *description* is **nvarchar(512)**, with a default of NULL. If *description* is omitted, "No description available" is used.  
  
`[ @start_step_id = ] step_id`
 The identification number of the first step to execute for the job. *step_id*is **int**, with a default of 1.  
  
`[ @category_name = ] 'category'`
 The category for the job. *category*is **sysname**, with a default of NULL.  
  
`[ @category_id = ] category_id`
 A language-independent mechanism for specifying a job category. *category_id*is **int**, with a default of NULL.  
  
`[ @owner_login_name = ] 'login'`
 The name of the login that owns the job. *login*is **sysname**, with a default of NULL, which is interpreted as the current login name. Only members of the **sysadmin** fixed server role can set or change the value for **@owner_login_name**. If users who are not members of the **sysadmin** role set or change the value of **@owner_login_name**, execution of this stored procedure fails and an error is returned.  
  
`[ @notify_level_eventlog = ] eventlog_level`
 A value indicating when to place an entry in the Microsoft Windows application log for this job. *eventlog_level*is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|Never|  
|**1**|On success|  
|**2** (default)|On failure|  
|**3**|Always|  
  
`[ @notify_level_email = ] email_level`
 A value that indicates when to send an e-mail upon the completion of this job. *email_level*is **int**, with a default of **0**, which indicates never. *email_level*uses the same values as *eventlog_level*.  
  
`[ @notify_level_netsend = ] netsend_level`
 A value that indicates when to send a network message upon the completion of this job. *netsend_level*is **int**, with a default of **0**, which indicates never. *netsend_level* uses the same values as *eventlog_level*.  
  
`[ @notify_level_page = ] page_level`
 A value that indicates when to send a page upon the completion of this job. *page_level*is **int**, with a default of **0**, which indicates never. *page_level*uses the same values as *eventlog_level*.  
  
`[ @notify_email_operator_name = ] 'email_name'`
 The e-mail name of the person to send e-mail to when *email_level* is reached. *email_name* is **sysname**, with a default of NULL.  
  
`[ @notify_netsend_operator_name = ] 'netsend_name'`
 The name of the operator to whom the network message is sent upon completion of this job. *netsend_name*is **sysname**, with a default of NULL.  
  
`[ @notify_page_operator_name = ] 'page_name'`
 The name of the person to page upon completion of this job. *page_name*is **sysname**, with a default of NULL.  
  
`[ @delete_level = ] delete_level`
 A value that indicates when to delete the job. *delete_value*is **int**, with a default of 0, which means never. *delete_level*uses the same values as *eventlog_level*.  
  
> [!NOTE]  
>  When *delete_level* is **3**, the job is executed only once, regardless of any schedules defined for the job. Furthermore, if a job deletes itself, all history for the job is also deleted.  
  
`[ @job_id = ] _job_idOUTPUT`
 The job identification number assigned to the job if created successfully. *job_id*is an output variable of type **uniqueidentifier**, with a default of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 **@originating_server** exists in **sp_add_job,** but is not listed under Arguments. **@originating_server** is reserved for internal use.  
  
 After **sp_add_job** has been executed to add a job, **sp_add_jobstep** can be used to add steps that perform the activities for the job. **sp_add_jobschedule** can be used to create the schedule that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service uses to execute the job. Use **sp_add_jobserver** to set the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance where the job executes, and **sp_delete_jobserver** to remove the job from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
 If the job will execute on one or more target servers in a multiserver environment, use **sp_apply_job_to_targets** to set the target servers or target server groups for the job. To remove jobs from target servers or target server groups, use **sp_remove_job_from_targets**.  
  
 [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides an easy, graphical way to manage jobs, and is the recommended way to create and manage the job infrastructure.  
  
## Permissions  
 To run this stored procedure, users must be a member of the **sysadmin** fixed server role, or be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles, which reside in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For information about the specific permissions that are associated with each of these fixed database roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 Only members of the **sysadmin** fixed server role can set or change the value for **@owner_login_name**. If users who are not members of the **sysadmin** role set or change the value of **@owner_login_name**, execution of this stored procedure fails and an error is returned.  
  
## Examples  
  
### A. Adding a job  
 This example adds a new job named `NightlyBackups`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_add_job  
    @job_name = N'NightlyBackups' ;  
GO  
```  
  
### B. Adding a job with pager, e-mail, and net send information  
 This example creates a job named `Ad hoc Sales Data Backup` that notifies `François Ajenstat` (by pager, e-mail, or network pop-up message) if the job fails, and deletes the job upon successful completion.  
  
> [!NOTE]  
>  This example assumes that an operator named `François Ajenstat` and a login named `françoisa` already exist.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_add_job  
    @job_name = N'Ad hoc Sales Data Backup',   
    @enabled = 1,  
    @description = N'Ad hoc backup of sales data',  
    @owner_login_name = N'françoisa',  
    @notify_level_eventlog = 2,  
    @notify_level_email = 2,  
    @notify_level_netsend = 2,  
    @notify_level_page = 2,  
    @notify_email_operator_name = N'François Ajenstat',  
    @notify_netsend_operator_name = N'François Ajenstat',   
    @notify_page_operator_name = N'François Ajenstat',  
    @delete_level = 1 ;  
GO  
```  
  
## See Also  
 [sp_add_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md)   
 [sp_add_jobstep &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-jobstep-transact-sql.md)   
 [sp_add_jobserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-jobserver-transact-sql.md)   
 [sp_apply_job_to_targets &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-apply-job-to-targets-transact-sql.md)   
 [sp_delete_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-job-transact-sql.md)   
 [sp_delete_jobserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-jobserver-transact-sql.md)   
 [sp_remove_job_from_targets &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-remove-job-from-targets-transact-sql.md)   
 [sp_help_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-job-transact-sql.md)   
 [sp_help_jobstep &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-jobstep-transact-sql.md)   
 [sp_update_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-job-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
