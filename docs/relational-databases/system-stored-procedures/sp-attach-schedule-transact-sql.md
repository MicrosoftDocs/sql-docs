---
title: "sp_attach_schedule (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_attach_schedule_TSQL"
  - "sp_attach_schedule"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_attach_schedule"
ms.assetid: 80c80eaf-cf23-4ed8-b8dd-65fe59830dd1
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_attach_schedule (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Sets a schedule for a job.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_attach_schedule  
     { [ @job_id = ] job_id | [ @job_name = ] 'job_name' } ,   
     { [ @schedule_id = ] schedule_id   
     | [ @schedule_name = ] 'schedule_name' }  
```  
  
## Arguments  
`[ @job_id = ] job_id`
 The job identification number of the job to which the schedule is added. *job_id*is **uniqueidentifier**, with a default of NULL.  
  
`[ @job_name = ] 'job_name'`
 The name of the job to which the schedule is added. *job_name*is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  Either *job_id* or *job_name* must be specified, but both cannot be specified.  
  
`[ @schedule_id = ] schedule_id`
 The schedule identification number of the schedule to set for the job. *schedule_id*is **int**, with a default of NULL.  
  
`[ @schedule_name = ] 'schedule_name'`
 The name of the schedule to set for the job. *schedule_name*is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  Either *schedule_id* or *schedule_name* must be specified, but both cannot be specified.  
  
## Remarks  
 The schedule and the job must have the same owner.  
  
 A schedule can be set for more than one job. A job can be run on more than one schedule.  
  
 This stored procedure must be run from the **msdb** database.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 Note that the job owner can attach a job to a schedule and detach a job from a schedule without also having to be the schedule owner. However, a schedule cannot be deleted if the detach would leave it with no jobs, unless the caller is the schedule owner.  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] checks if the user owns both the job and the schedule.  
  
## Examples  
 The following example creates a schedule named `NightlyJobs`. Jobs that use this schedule execute every day when the time on the server is `01:00`. The example attaches the schedule to the job `BackupDatabase` and the job `RunReports`.  
  
> [!NOTE]  
>  This example assumes that the job `BackupDatabase` and the job `RunReports` already exist.  
  
```  
USE msdb ;  
GO  
  
EXEC sp_add_schedule  
    @schedule_name = N'NightlyJobs' ,  
    @freq_type = 4,  
    @freq_interval = 1,  
    @active_start_time = 010000 ;  
GO  
  
EXEC sp_attach_schedule  
   @job_name = N'BackupDatabase',  
   @schedule_name = N'NightlyJobs' ;  
GO  
  
EXEC sp_attach_schedule  
   @job_name = N'RunReports',  
   @schedule_name = N'NightlyJobs' ;  
GO  
```  
  
## See Also  
 [sp_add_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md)   
 [sp_detach_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-detach-schedule-transact-sql.md)   
 [sp_delete_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-schedule-transact-sql.md)  
  
  
