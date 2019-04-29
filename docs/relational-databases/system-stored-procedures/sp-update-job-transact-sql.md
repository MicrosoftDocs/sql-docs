---
title: "sp_update_job (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_update_job"
  - "sp_update_job_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_update_job"
ms.assetid: cbdfea38-9e42-47f3-8fc8-5978b82e2623
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_update_job (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the attributes of a job.  
  

  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_update_job [ @job_id =] job_id | [@job_name =] 'job_name'  
     [, [@new_name =] 'new_name' ]   
     [, [@enabled =] enabled ]  
     [, [@description =] 'description' ]   
     [, [@start_step_id =] step_id ]  
     [, [@category_name =] 'category' ]   
     [, [@owner_login_name =] 'login' ]  
     [, [@notify_level_eventlog =] eventlog_level ]  
     [, [@notify_level_email =] email_level ]  
     [, [@notify_level_netsend =] netsend_level ]  
     [, [@notify_level_page =] page_level ]  
     [, [@notify_email_operator_name =] 'operator_name' ]  
     [, [@notify_netsend_operator_name =] 'netsend_operator' ]  
     [, [@notify_page_operator_name =] 'page_operator' ]  
     [, [@delete_level =] delete_level ]   
     [, [@automatic_post =] automatic_post ]  
```  
  
## Arguments  
`[ @job_id = ] job_id`
 The identification number of the job to be updated. *job_id*is **uniqueidentifier**.  
  
`[ @job_name = ] 'job_name'`
 The name of the job. *job_name* is **nvarchar(128)**.  
  
> **NOTE:** Either *job_id* or *job_name* must be specified but both cannot be specified.  
  
`[ @new_name = ] 'new_name'`
 The new name for the job. *new_name* is **nvarchar(128)**.  
  
`[ @enabled = ] enabled`
 Specifies whether the job is enabled (**1**) or not enabled (**0**). *enabled* is **tinyint**.  
  
`[ @description = ] 'description'`
 The description of the job. *description* is **nvarchar(512)**.  
  
`[ @start_step_id = ] step_id`
 The identification number of the first step to execute for the job. *step_id* is **int**.  
  
`[ @category_name = ] 'category'`
 The category of the job. *category* is **nvarchar(128)**.  
  
`[ @owner_login_name = ] 'login'`
 The name of the login that owns the job. *login* is **nvarchar(128)** Only members of the **sysadmin** fixed server role can change job ownership.  
  
`[ @notify_level_eventlog = ] eventlog_level`
 Specifies when to place an entry in the Microsoft Windows application log for this job. *eventlog_level*is **int**, and can be one of these values.  
  
|Value|Description (action)|  
|-----------|----------------------------|  
|**0**|Never|  
|**1**|On success|  
|**2**|On failure|  
|**3**|Always|  
  
`[ @notify_level_email = ] email_level`
 Specifies when to send an e-mail upon the completion of this job. *email_level*is **int**. *email_level*uses the same values as *eventlog_level*.  
  
`[ @notify_level_netsend = ] netsend_level`
 Specifies when to send a network message upon the completion of this job. *netsend_level*is **int**. *netsend_level*uses the same values as *eventlog_level*.  
  
`[ @notify_level_page = ] page_level`
 Specifies when to send a page upon the completion of this job. *page_level* is **int**. *page_level*uses the same values as *eventlog_level*.  
  
`[ @notify_email_operator_name = ] 'operator_name'`
 The name of the operator to whom the e-mail is sent when *email_level* is reached. *email_name* is **nvarchar(128)**.  
  
`[ @notify_netsend_operator_name = ] 'netsend_operator'`
 The name of the operator to whom the network message is sent. *netsend_operator* is **nvarchar(128)**.  
  
`[ @notify_page_operator_name = ] 'page_operator'`
 The name of the operator to whom a page is sent. *page_operator* is **nvarchar(128)**.  
  
`[ @delete_level = ] delete_level`
 Specifies when to delete the job. *delete_value*is **int**. *delete_level*uses the same values as *eventlog_level*.  
  
`[ @automatic_post = ] automatic_post`
 Reserved.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_update_job** must be run from the **msdb** database.  
  
 **sp_update_job** changes only those settings for which parameter values are supplied. If a parameter is omitted, the current setting is retained.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 Only members of **sysadmin** can use this stored procedure to edit the attributes of jobs that are owned by other users.  
  
## Examples  
 The following example changes the name, description, and enabled status of the job `NightlyBackups`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_update_job  
    @job_name = N'NightlyBackups',  
    @new_name = N'NightlyBackups -- Disabled',  
    @description = N'Nightly backups disabled during server migration.',  
    @enabled = 0 ;  
GO  
```  
  
## See Also  
 [sp_add_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-job-transact-sql.md)   
 [sp_delete_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-job-transact-sql.md)   
 [sp_help_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-job-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
