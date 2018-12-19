---
title: "sp_help_jobschedule (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_help_jobschedule"
  - "sp_help_jobschedule_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_jobschedule"
ms.assetid: 2cded902-9272-4667-ac4b-a4f95a9f008e
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_help_jobschedule (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information about the scheduling of jobs used by [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to perform automated activities.  
 
 
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_jobschedule { [ @job_id = ] job_id | [ @job_name = ] 'job_name' }  
     [ , [ @schedule_name = ] 'schedule_name' ]  
     [ , [ @schedule_id = ] schedule_id ]  
     [ , [ @include_description = ] include_description ]  
```  
  
## Arguments  
 [ **@job_id=** ] *job_id*  
 The job identification number. *job_id*is **uniqueidentifier**, with a default of NULL.  
  
 [ **@job_name=** ] **'**_job_name_**'**  
 The name of the job. *job_name*is **sysname**, with a default of NULL.  
  
> **NOTE:** Either *job_id* or *job_name* must be specified, but both cannot be specified.  
  
 [ **@schedule_name=** ] **'**_schedule_name_**'**  
 The name of the schedule item for the job. *schedule_name*is **sysname**, with a default of NULL.  
  
 [ **@schedule_id=** ] *schedule_id*  
 The identification number of the schedule item for the job. *schedule_id*is **int**, with a default of NULL.  
  
 [ **@include_description=** ] *include_description*  
 Specifies whether to include the description of the schedule in the result set. *include_description* is **bit**, with a default of **0**. When *include_description* is **0**, the description of the schedule is not included in the result set. When *include_description* is **1**, the description of the schedule is included in the result set.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**schedule_id**|**int**|Schedule identifier number.|  
|**schedule_name**|**sysname**|Name of the schedule.|  
|**enabled**|**int**|Whether the schedule enabled (**1**) or not enabled (**0**).|  
|**freq_type**|**int**|Value indicating when the job is to be executed.<br /><br /> **1** = Once<br /><br /> **4** = Daily<br /><br /> **8** = Weekly<br /><br /> **16** = Monthly<br /><br /> **32** = Monthly, relative to the **freq_interval**<br /><br /> **64** = Run when **SQLServerAgent** service starts.|  
|**freq_interval**|**int**|Days when the job is executed. The value depends on the value of **freq_type**. For more information, see [sp_add_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md).|  
|**freq_subday_type**|**int**|Units for **freq_subday_interval**. For more information, see [sp_add_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md).|  
|**freq_subday_interval**|**int**|Number of **freq_subday_type** periods to occur between each execution of the job. For more information, see [sp_add_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md).|  
|**freq_relative_interval**|**int**|Scheduled job's occurrence of the **freq_interval** in each month. For more information, see [sp_add_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md).|  
|**freq_recurrence_factor**|**int**|Number of months between the scheduled execution of the job.|  
|**active_start_date**|**int**|Date the schedule is activated.|  
|**active_end_date**|**int**|End date of the schedule.|  
|**active_start_time**|**int**|Time of the day the schedule starts.|  
|**active_end_time**|**int**|Time of the day schedule ends.|  
|**date_created**|**datetime**|Date the schedule is created.|  
|**schedule_description**|**nvarchar(4000)**|An English description of the schedule that is derived from values in **msdb.dbo.sysschedules**. When *include_description* is **0**, this column contains text stating that the description was not requested.|  
|**next_run_date**|**int**|Date the schedule will next cause the job to run.|  
|**next_run_time**|**int**|Time the schedule will next cause the job to run.|  
|**schedule_uid**|**uniqueidentifier**|Identifier for the schedule.|  
|**job_count**|**int**|Count of jobs returned.|  
  
> **NOTE:  sp_help_jobschedule** returns values from the **dbo.sysjobschedules** and **dbo.sysschedules** system tables in **msdb**. **sysjobschedules** updates every 20 minutes. This might affect the values that are returned by this stored procedure.  
  
## Remarks  
 The parameters of **sp_help_jobschedule** can be used only in certain combinations. If *schedule_id* is specified, neither *job_id* nor *job_name* can be specified. Otherwise, the *job_id* or *job_name* parameters can be used with *schedule_name*.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 Members of **SQLAgentUserRole** can only view properties of job schedules that they own.  
  
## Examples  
  
### A. Returning the job schedule for a specific job  
 The following example returns the scheduling information for a job named `BackupDatabase`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_jobschedule  
    @job_name = N'BackupDatabase' ;  
GO  
```  
  
### B. Returning the job schedule for a specific schedule  
 The following example returns the information for the schedule named `NightlyJobs` and the job named `RunReports`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_jobschedule   
    @job_name = N'RunReports',  
    @schedule_name = N'NightlyJobs' ;  
GO  
```  
  
### C. Returning the job schedule and schedule description for a specific schedule  
 The following example returns the information for the schedule named `NightlyJobs` and the job named `RunReports`. The result set returned includes a description of the schedule.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_jobschedule  
    @job_name = N'RunReports',  
    @schedule_name = N'NightlyJobs',  
    @include_description = 1 ;  
GO  
```  
  
## See Also  
 [sp_add_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md)   
 [sp_delete_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-schedule-transact-sql.md)   
 [sp_update_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-schedule-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
