---
title: "sp_delete_job (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_delete_job"
  - "sp_delete_job_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_delete_job"
ms.assetid: b85db6e4-623c-41f1-9643-07e5ea38db09
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_delete_job (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Deletes a job.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_delete_job { [ @job_id = ] job_id | [ @job_name = ] 'job_name' } ,  
     [ , [ @originating_server = ] 'server' ]   
     [ , [ @delete_history = ] delete_history ]  
     [ , [ @delete_unused_schedule = ] delete_unused_schedule ]  
```  
  
## Arguments  
`[ @job_id = ] job_id`
 Is the identification number of the job to be deleted. *job_id* is **uniqueidentifier**, with a default of NULL.  
  
`[ @job_name = ] 'job_name'`
 Is the name of the job to be deleted. *job_name* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  Either *job_id* or *job_name*must be specified; both cannot be specified.  
  
`[ @originating_server = ] 'server'`
 For internal use.  
  
`[ @delete_history = ] delete_history`
 Specifies whether to delete the history for the job. *delete_history* is **bit**, with a default of **1**. When *delete_history* is **1**, the job history for the job is deleted. When *delete_history* is **0**, the job history is not deleted.  
  
 Note that when a job is deleted and the history is not deleted, historical information for the job will not display in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent graphical user interface job history, but the information will still reside in the **sysjobhistory** table in the **msdb** database.  
  
`[ @delete_unused_schedule = ] delete_unused_schedule`
 Specifies whether to delete the schedules attached to this job if they are not attached to any other job. *delete_unused_schedule* is **bit**, with a default of **1**. When *delete_unused_schedule* is **1**, schedules attached to this job are deleted if no other jobs reference the schedule. When *delete_unused_schedule* is **0**, the schedules are not deleted.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 The **@originating_server** argument is reserved for internal use.  
  
 The **@delete_unused_schedule** argument provides backward compatibility with previous versions of SQL Server by automatically removing schedules that are not attached to any job. Notice that this parameter defaults to the backward-compatible behavior. To retain schedules that are not attached to a job, you must provide the value **0** as the **@delete_unused_schedule** argument.  
  
 [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides an easy, graphical way to manage jobs, and is the recommended way to create and manage the job infrastructure.  
  
 This stored procedure cannot delete maintenance plans, and cannot delete jobs that are part of maintenance plans. Instead, use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to delete maintenance plans.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 Members of the **sysadmin** fixed server role can execute **sp_delete_job** to delete any job. A user that is not a member of the **sysadmin** fixed server role can only delete jobs owned by that user.  
  
## Examples  
 The following example deletes the job `NightlyBackups`.  
  
```  
USE msdb ;  
GO  
  
EXEC sp_delete_job  
    @job_name = N'NightlyBackups' ;  
GO  
```  
  
## See Also  
 [sp_add_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-job-transact-sql.md)   
 [sp_help_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-job-transact-sql.md)   
 [sp_update_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-job-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
