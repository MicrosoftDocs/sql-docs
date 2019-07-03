---
title: "sp_update_jobstep (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_update_jobstep"
  - "sp_update_jobstep_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_update_jobstep"
ms.assetid: e158802c-c347-4a5d-bf75-c03e5ae56e6b
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_update_jobstep (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the setting for a step in a job that is used to perform automated activities.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_update_jobstep   
     {   [@job_id =] job_id   
       | [@job_name =] 'job_name' } ,  
     [@step_id =] step_id  
     [ , [@step_name =] 'step_name' ]  
     [ , [@subsystem =] 'subsystem' ]   
     [ , [@command =] 'command' ]  
     [ , [@additional_parameters =] 'parameters' ]  
     [ , [@cmdexec_success_code =] success_code ]  
     [ , [@on_success_action =] success_action ]   
     [ , [@on_success_step_id =] success_step_id ]  
     [ , [@on_fail_action =] fail_action ]   
     [ , [@on_fail_step_id =] fail_step_id ]  
     [ , [@server =] 'server' ]   
     [ , [@database_name =] 'database' ]  
     [ , [@database_user_name =] 'user' ]   
     [ , [@retry_attempts =] retry_attempts ]  
     [ , [@retry_interval =] retry_interval ]   
     [ , [@os_run_priority =] run_priority ]  
     [ , [@output_file_name =] 'file_name' ]   
     [ , [@flags =] flags ]  
     [ ,  {   [ @proxy_id = ] proxy_id   
            | [ @proxy_name = ] 'proxy_name' }   
```  
  
## Arguments  
`[ @job_id = ] job_id`
 The identification number of the job to which the step belongs. *job_id*is **uniqueidentifier**, with a default of NULL. Either *job_id* or *job_name* must be specified but both cannot be specified.  
  
`[ @job_name = ] 'job_name'`
 The name of the job to which the step belongs. *job_name*is **sysname**, with a default of NULL. Either *job_id* or *job_name* must be specified but both cannot be specified.  
  
`[ @step_id = ] step_id`
 The identification number for the job step to be modified. This number cannot be changed. *step_id*is **int**, with no default.  
  
`[ @step_name = ] 'step_name'`
 Is a new name for the step. *step_name*is **sysname**, with a default of NULL.  
  
`[ @subsystem = ] 'subsystem'`
 The subsystem used by Microsoft SQL Server Agent to execute *command*. *subsystem* is **nvarchar(40)**, with a default of NULL.  
  
`[ @command = ] 'command'`
 The command(s) to be executed through *subsystem*. *command* is **nvarchar(max)**, with a default of NULL.  
  
`[ @additional_parameters = ] 'parameters'`
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
`[ @cmdexec_success_code = ] success_code`
 The value returned by a **CmdExec** subsystem command to indicate that *command* executed successfully. *success_code* is **int**, with a default of NULL.  
  
`[ @on_success_action = ] success_action`
 The action to perform if the step succeeds.*success_action* is **tinyint**, with a default of NULL, and can be one of these values.  
  
|Value|Description (action)|  
|-----------|----------------------------|  
|**1**|Quit with success.|  
|**2**|Quit with failure.|  
|**3**|Go to next step.|  
|**4**|Go to step *success_step_id.*|  
  
`[ @on_success_step_id = ] success_step_id`
 The identification number of the step in this job to execute if step succeeds and *success_action* is **4**. *success_step_id* is **int**, with a default of NULL.  
  
`[ @on_fail_action = ] fail_action`
 The action to perform if the step fails. *fail_action* is **tinyint**, with a default of NULL and can have one of these values.  
  
|Value|Description (action)|  
|-----------|----------------------------|  
|**1**|Quit with success.|  
|**2**|Quit with failure.|  
|**3**|Go to next step.|  
|**4**|Go to step *fail_step_id**.*|  
  
`[ @on_fail_step_id = ] fail_step_id`
 The identification number of the step in this job to execute if the step fails and *fail_action* is **4**. *fail_step_id* is **int**, with a default of NULL.  
  
`[ @server = ] 'server'`
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)] *server* is **nvarchar(128)**, with a default of NULL.  
  
`[ @database_name = ] 'database'`
 The name of the database in which to execute a [!INCLUDE[tsql](../../includes/tsql-md.md)] step. *database*is **sysname**. Names that are enclosed in brackets ([ ]) are not allowed. The default value is NULL.  
  
`[ @database_user_name = ] 'user'`
 The name of the user account to use when executing a [!INCLUDE[tsql](../../includes/tsql-md.md)] step. *user*is **sysname**, with a default of NULL.  
  
`[ @retry_attempts = ] retry_attempts`
 The number of retry attempts to use if this step fails. *retry_attempts*is **int**, with a default of NULL.  
  
`[ @retry_interval = ] retry_interval`
 The amount of time in minutes between retry attempts. *retry_interval* is **int**, with a default of NULL.  
  
`[ @os_run_priority = ] run_priority`
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
`[ @output_file_name = ] 'file_name'`
 The name of the file in which the output of this step is saved. *file_name* is **nvarchar(200)**, with a default of NULL. This parameter is only valid with commands running in [!INCLUDE[tsql](../../includes/tsql-md.md)] or CmdExec subsystems.  
  
 To set output_file_name back to NULL, you must set *output_file_name* to an empty string (' ') or to a string of blank characters, but you cannot use the **CHAR(32)** function. For example, set this argument to an empty string as follows:  
  
 **@output_file_name = ' '**  
  
`[ @flags = ] flags`
 An option that controls behavior. *flags* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**0** (default)|Overwrite output file.|  
|**2**|Append to output file|  
|**4**|Write Transact-SQL job step output to step history|  
|**8**|Write log to table (overwrite existing history)|  
|**16**|Write log to table (append to existing history)|  
  
`[ @proxy_id = ] proxy_id`
 The ID number of the proxy that the job step runs as. *proxy_id* is type **int**, with a default of NULL. If no *proxy_id* is specified, no *proxy_name* is specified, and no *user_name* is specified, the job step runs as the service account for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent.  
  
`[ @proxy_name = ] 'proxy_name'`
 The name of the proxy that the job step runs as. *proxy_name* is type **sysname**, with a default of NULL. If no *proxy_id* is specified, no *proxy_name* is specified, and no *user_name* is specified, the job step runs as the service account for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_update_jobstep** must be run from the **msdb** database.  
  
 Updating a job step increments the job version number.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 Only members of **sysadmin** can update a job step owned by another user.  
  
 If the job step requires access to a proxy, the creator of the job step must have access to the proxy for the job step. All subsystems, except Transact-SQL, require a proxy account. Members of **sysadmin** have access to all proxies, and can use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account for the proxy.  
  
## Examples  
 The following example changes the number of retry attempts for the first step of the `Weekly Sales Data Backup` job. After running this example, the number of retry attempts is `10`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_update_jobstep  
    @job_name = N'Weekly Sales Data Backup',  
    @step_id = 1,  
    @retry_attempts = 10 ;  
GO  
```  
  
## See Also  
 [View or Modify Jobs](../../ssms/agent/view-or-modify-jobs.md)   
 [sp_delete_jobstep &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-jobstep-transact-sql.md)   
 [sp_help_jobstep &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-jobstep-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
