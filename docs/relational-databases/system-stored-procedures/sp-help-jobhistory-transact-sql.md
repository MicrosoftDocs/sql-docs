---
description: "sp_help_jobhistory (Transact-SQL)"
title: "sp_help_jobhistory (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_help_jobhistory_TSQL"
  - "sp_help_jobhistory"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_jobhistory"
ms.assetid: a944d44e-411b-4735-8ce4-73888d4262d7
author: markingmyname
ms.author: maghan
---
# sp_help_jobhistory (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Provides information about the jobs for servers in the multiserver administration domain.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_jobhistory [ [ @job_id = ] job_id ]   
     [ , [ @job_name = ] 'job_name' ]   
     [ , [ @step_id = ] step_id ]   
     [ , [ @sql_message_id = ] sql_message_id ]   
     [ , [ @sql_severity = ] sql_severity ]   
     [ , [ @start_run_date = ] start_run_date ]   
     [ , [ @end_run_date = ] end_run_date ]   
     [ , [ @start_run_time = ] start_run_time ]   
     [ , [ @end_run_time = ] end_run_time ]   
     [ , [ @minimum_run_duration = ] minimum_run_duration ]   
     [ , [ @run_status = ] run_status ]   
     [ , [ @minimum_retries = ] minimum_retries ]   
     [ , [ @oldest_first = ] oldest_first ]   
     [ , [ @server = ] 'server' ]   
     [ , [ @mode = ] 'mode' ]  
```  
  
## Arguments  
`[ @job_id = ] job_id`
 The job identification number. *job_id* is **uniqueidentifier**, with a default of NULL.  
  
`[ @job_name = ] 'job_name'`
 The name of the job. *job_name* is **sysname**, with a default of NULL.  
  
`[ @step_id = ] step_id`
 The step identification number. *step_id* is **int**, with a default of NULL.  
  
`[ @sql_message_id = ] sql_message_id`
 The identification number of the error message returned by Microsoft SQL Server when executing the job. *sql_message_id* is **int**, with a default of NULL.  
  
`[ @sql_severity = ] sql_severity`
 The severity level of the error message returned by SQL Server when executing the job. *sql_severity* is **int**, with a default of NULL.  
  
`[ @start_run_date = ] start_run_date`
 The date the job was started. *start_run_date*is **int**, with a default of NULL. *start_run_date* must be entered in the form YYYYMMDD, where YYYY is a four-character year, MM is a two-character month name, and DD is a two-character day name.  
  
`[ @end_run_date = ] end_run_date`
 The date the job was completed. *end_run_date* is **int**, with a default of NULL. *end_run_date*must be entered in the form YYYYMMDD, where YYYY is a four-digit year, MM is a two-character month name, and DD is a two-character day name.  
  
`[ @start_run_time = ] start_run_time`
 The time the job was started. *start_run_time* is **int**, with a default of NULL. *start_run_time*must be entered in the form HHMMSS, where HH is a two-character hour of the day, MM is a two-character minute of the day, and SS is a two-character second of the day.  
  
`[ @end_run_time = ] end_run_time`
 The time the job completed its execution. *end_run_time* is **int**, with a default of NULL. *end_run_time*must be entered in the form HHMMSS, where HH is a two-character hour of the day, MM is a two-character minute of the day, and SS is a two-character second of the day.  
  
`[ @minimum_run_duration = ] minimum_run_duration`
 The minimum length of time for the completion of the job. *minimum_run_duration* is **int**, with a default of NULL. *minimum_run_duration*must be entered in the form HHMMSS, where HH is a two-character hour of the day, MM is a two-character minute of the day, and SS is a two-character second of the day.  
  
`[ @run_status = ] run_status`
 The execution status of the job. *run_status* is **int**, with a default of NULL, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|Failed|  
|**1**|Succeeded|  
|**2**|Retry (step only)|  
|**3**|Canceled|  
|**4**|In-progress message|  
|**5**|Unknown|  
  
`[ @minimum_retries = ] minimum_retries`
 The minimum number of times a job should retry running. *minimum_retries* is **int**, with a default of NULL.  
  
`[ @oldest_first = ] oldest_first`
 Is whether to present the output with the oldest jobs first. *oldest_first* is **int**, with a default of **0**, which presents the newest jobs first. **1** presents the oldest jobs first.  
  
`[ @server = ] 'server'`
 The name of the server on which the job was performed. *server* is **nvarchar(30)**, with a default of NULL.  
  
`[ @mode = ] 'mode'`
 Is whether SQL Server prints all columns in the result set (**FULL**) or a summary of the columns. *mode* is **varchar(7)**, with a default of **SUMMARY**.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 The actual column list depends on the value of *mode*. The most comprehensive set of columns is shown below and is returned when *mode* is FULL.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**instance_id**|**int**|History entry identification number.|  
|**job_id**|**uniqueidentifier**|Job identification number.|  
|**job_name**|**sysname**|Job name.|  
|**step_id**|**int**|Step identification number (will be **0** for a job history).|  
|**step_name**|**sysname**|Step name (will be NULL for a job history).|  
|**sql_message_id**|**int**|For a [!INCLUDE[tsql](../../includes/tsql-md.md)] step, the most recent [!INCLUDE[tsql](../../includes/tsql-md.md)] error number encountered while running the command.|  
|**sql_severity**|**int**|For a [!INCLUDE[tsql](../../includes/tsql-md.md)] step, the highest [!INCLUDE[tsql](../../includes/tsql-md.md)] error severity encountered while running the command.|  
|**message**|**nvarchar(1024)**|Job or step history message.|  
|**run_status**|**int**|Outcome of the job or step.|  
|**run_date**|**int**|Date the job or step began executing.|  
|**run_time**|**int**|Time the job or step began executing.|  
|**run_duration**|**int**|Elapsed time in the execution of the job or step in HHMMSS format.|  
|**operator_emailed**|**nvarchar(20)**|Operator who was e-mailed regarding this job (is NULL for step history).|  
|**operator_netsent**|**nvarchar(20)**|Operator who was sent a network message regarding this job (is NULL for step history).|  
|**operator_paged**|**nvarchar(20)**|Operator who was paged regarding this job (is NULL for step history).|  
|**retries_attempted**|**int**|Number of times the step was retried (always 0 for a job history).|  
|**server**|**nvarchar(30)**|Server the step or job executes on. Is always (**local**).|  
  
## Remarks  
 **sp_help_jobhistory** returns a report with the history of the specified scheduled jobs. If no parameters are specified, the report contains the history for all scheduled jobs.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 Members of the **SQLAgentUserRole** database role can only view the history for jobs that they own.  
  
## Examples  
  
### A. Listing all job information for a job  
 The following example lists all job information for the `NightlyBackups` job.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_jobhistory   
    @job_name = N'NightlyBackups' ;  
GO  
```  
  
### B. Listing information for jobs that match certain conditions  
 The following example prints all columns and all job information for any failed jobs and failed job steps with an error message of `50100` (a user-defined error message) and a severity of `20`.  
  
```  
USE msdb  
GO  
  
EXEC dbo.sp_help_jobhistory  
    @sql_message_id = 50100,  
    @sql_severity = 20,  
    @run_status = 0,  
    @mode = N'FULL' ;  
GO  
```  
  
## See Also  
 [sp_purge_jobhistory &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-purge-jobhistory-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
