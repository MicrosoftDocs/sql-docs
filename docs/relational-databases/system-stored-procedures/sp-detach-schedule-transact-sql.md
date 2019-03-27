---
title: "sp_detach_schedule (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_detach_schedule"
  - "sp_detach_schedule_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_detach_schedule"
ms.assetid: 9a1fc335-1bef-4638-a33a-771c54a5dd19
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_detach_schedule (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes an association between a schedule and a job.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_detach_schedule   
     { [ @job_id = ] job_id | [ @job_name = ] 'job_name' } ,  
       { [ @schedule_id = ] schedule_id | [ @schedule_name = ] 'schedule_name' } ,  
     [ @delete_unused_schedule = ] delete_unused_schedule   
```  
  
## Arguments  
`[ @job_id = ] job_id`
 The job identification number of the job to remove the schedule from. *job_id* is **uniqueidentifier**, with a default of NULL.  
  
`[ @job_name = ] 'job_name'`
 The name of the job to remove the schedule from. *job_name* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  Either *job_id* or *job_name* must be specified, but both cannot be specified.  
  
`[ @schedule_id = ] schedule_id`
 The schedule identification number of the schedule to remove from the job. *schedule_id* is **int**, with a default of NULL.  
  
`[ @schedule_name = ] 'schedule_name'`
 The name of the schedule to remove from the job. *schedule_name* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  Either *schedule_id* or *schedule_name* must be specified, but both cannot be specified.  
  
`[ @delete_unused_schedule = ] delete_unused_schedule`
 Specifies whether to delete unused job schedules. *delete_unused_schedule* is **bit**, with a default of **0**, which means that all schedules will be kept, even if no jobs reference them. If set to **1**, unused job schedules are deleted if no jobs reference them.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 Note that the job owner can attach a job to a schedule and detach a job from a schedule without also having to be the schedule owner. However, a schedule cannot be deleted if the detach would leave it with no jobs unless the caller is the schedule owner.  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] checks to determine whether the user owns the schedule. Only members of the **sysadmin** fixed server role can detach schedules from jobs owned by another user.  
  
## Examples  
 The following example removes an association between a `'NightlyJobs'` schedule and a `'BackupDatabase'` job.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_detach_schedule  
    @job_name = 'BackupDatabase',  
    @schedule_name = 'NightlyJobs' ;  
GO  
```  
  
## See Also  
 [sp_add_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md)   
 [sp_attach_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-attach-schedule-transact-sql.md)   
 [sp_delete_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-schedule-transact-sql.md)  
  
  
