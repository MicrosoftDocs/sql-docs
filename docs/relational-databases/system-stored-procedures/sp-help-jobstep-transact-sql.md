---
title: "sp_help_jobstep (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_help_jobstep_TSQL"
  - "sp_help_jobstep"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_jobstep"
ms.assetid: 4a13b804-45f2-4f82-987f-42d9a57dd6db
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_help_jobstep (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information for the steps in a job used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service to perform automated activities.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_jobstep { [ @job_id = ] 'job_id' | [ @job_name = ] 'job_name' }  
     [ , [ @step_id = ] step_id ]   
     [ , [ @step_name = ] 'step_name' ]   
     [ , [ @suffix = ] suffix ]   
```  
  
## Arguments  
`[ @job_id = ] 'job_id'`
 The job identification number for which to return job information. *job_id* is **uniqueidentifier**, with a default of NULL.  
  
`[ @job_name = ] 'job_name'`
 The name of the job. *job_name* is **sysname**, with a default NULL.  
  
> [!NOTE]  
>  Either *job_id* or *job_name* must be specified, but both cannot be specified.  
  
`[ @step_id = ] step_id`
 The identification number of the step in the job. If not included, all steps in the job are included. *step_id* is **int**, with a default of NULL.  
  
`[ @step_name = ] 'step_name'`
 The name of the step in the job. *step_name* is **sysname**, with a default of NULL.  
  
`[ @suffix = ] suffix`
 A flag indicating whether a text description is appended to the **flags** column in the output. *suffix*is **bit**, with the default of **0**. If *suffix* is **1**, a description is appended.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**step_id**|**int**|Unique identifier for the step.|  
|**step_name**|**sysname**|Name of the step in the job.|  
|**subsystem**|**nvarchar(40)**|Subsystem in which to execute the step command.|  
|**command**|**nvarchar(max)**|Command executed in the step.|  
|**flags**|**int**|A bitmask of values that control step behavior.|  
|**cmdexec_success_code**|**int**|For a **CmdExec** step, this is the process exit code of a successful command.|  
|**on_success_action**|**tinyint**|Action to take if the step succeeds:<br /><br /> **1** = Quit the job reporting success.<br /><br /> **2** = Quit the job reporting failure.<br /><br /> **3** = Go to the next step.<br /><br /> **4** = Go to step.|  
|**on_success_step_id**|**int**|If **on_success_action** is 4, this indicates the next step to execute.|  
|**on_fail_action**|**tinyint**|What to do if the step fails. Values are same as **on_success_action**.|  
|**on_fail_step_id**|**int**|If **on_fail_action** is 4, this indicates the next step to execute.|  
|**server**|**sysname**|Reserved.|  
|**database_name**|**sysname**|For a [!INCLUDE[tsql](../../includes/tsql-md.md)] step, this is the database in which the command executes.|  
|**database_user_name**|**sysname**|For a [!INCLUDE[tsql](../../includes/tsql-md.md)] step, this is the database user context in which the command executes.|  
|**retry_attempts**|**int**|Maximum number of times the command should be retried (if it is unsuccessful).|  
|**retry_interval**|**int**|Interval (in minutes) for any retry attempts.|  
|**os_run_priority**|**int**|Reserved.|  
|**output_file_name**|**nvarchar(200)**|File to which command output should be written ([!INCLUDE[tsql](../../includes/tsql-md.md)], **CmdExec**, and **PowerShell** steps only).|  
|**last_run_outcome**|**int**|Outcome of the step the last time it ran:<br /><br /> **0** = Failed<br /><br /> **1** = Succeeded<br /><br /> **2** = Retry<br /><br /> **3** = Canceled<br /><br /> **5** = Unknown|  
|**last_run_duration**|**int**|Duration (in seconds) of the step the last time it ran.|  
|**last_run_retries**|**int**|Number of times the command was retried the last time the step ran.|  
|**last_run_date**|**int**|Date the step last started execution.|  
|**last_run_time**|**int**|Time the step last started execution.|  
|**proxy_id**|**int**|Proxy for the job step.|  
  
## Remarks  
 **sp_help_jobstep** is in the **msdb** database.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 Members of **SQLAgentUserRole** can only view job steps for jobs that they own.  
  
## Examples  
  
### A. Return information for all steps in a specific job  
 The following example returns all the job steps for the job named `Weekly Sales Data Backup`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_jobstep  
    @job_name = N'Weekly Sales Data Backup' ;  
GO  
```  
  
### B. Return information about a specific job step  
 The following example returns information about the first job step for the job named `Weekly Sales Data Backup`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_jobstep  
    @job_name = N'Weekly Sales Data Backup',  
    @step_id = 1 ;  
GO  
```  
  
## See Also  
 [sp_add_jobstep &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-jobstep-transact-sql.md)   
 [sp_delete_jobstep &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-jobstep-transact-sql.md)   
 [sp_help_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-job-transact-sql.md)   
 [sp_update_jobstep &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-jobstep-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
