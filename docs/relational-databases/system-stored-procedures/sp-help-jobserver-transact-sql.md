---
title: "sp_help_jobserver (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_help_jobserver"
  - "sp_help_jobserver_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_jobserver"
ms.assetid: 57971787-f9f5-4199-9f64-c2b61a308906
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_help_jobserver (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information about the server for a given job.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_jobserver  
     { [ @job_id = ] job_id   
     | [ @job_name = ] 'job_name' }  
     [ , [ @show_last_run_details = ] show_last_run_details ]  
```  
  
## Arguments  
`[ @job_id = ] job_id`
 The job identification number for which to return information. *job_id* is **uniqueidentifier**, with a default of NULL.  
  
`[ @job_name = ] 'job_name'`
 The job name for which to return information. *job_name* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  Either *job_id* or *job_name* must be specified, but both cannot be specified.  
  
`[ @show_last_run_details = ] show_last_run_details`
 Is whether the last-run execution information is part of the result set. *show_last_run_details* is **tinyint**, with a default of **0**. **0** does not include last-run information, and **1** does.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**server_id**|**int**|Identification number of the target server.|  
|**server_name**|**nvarchar(30)**|Computer name of the target server.|  
|**enlist_date**|**datetime**|Date the target server enlisted into the master server.|  
|**last_poll_date**|**datetime**|Date the target server last polled the master server.|  
  
 If **sp_help_jobserver** is executed with *show_last_run_details* set to **1**, the result set has these additional columns.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**last_run_date**|**int**|Date the job last started execution on this target server.|  
|**last_run_time**|**int**|Time the job last started execution on this server.|  
|**last_run_duration**|**int**|Duration of the job the last time it ran on this target server (in seconds).|  
|**last_outcome_message**|**nvarchar(1024)**|Describes the last outcome of the job.|  
|**last_run_outcome**|**int**|Outcome of the job the last time it ran on this server:<br /><br /> **0** = Failed<br /><br /> **1** = Succeeded<br /><br /> **3** = Canceled<br /><br /> **5** = Unknown|  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 Members of **SQLAgentUserRole** can only view information for jobs that they own.  
  
## Examples  
 The following example returns information, including last-run information, about the `NightlyBackups` job.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_jobserver  
    @job_name = N'NightlyBackups',  
    @show_last_run_details = 1 ;  
GO  
```  
  
## See Also  
 [sp_add_jobserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-jobserver-transact-sql.md)   
 [sp_delete_jobserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-jobserver-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
