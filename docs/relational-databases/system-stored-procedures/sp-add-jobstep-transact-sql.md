---
title: "sp_add_jobstep (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_add_jobstep_TSQL"
  - "sp_add_jobstep"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_add_jobstep"
ms.assetid: 97900032-523d-49d6-9865-2734fba1c755
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_add_jobstep (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds a step (operation) to a job.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_add_jobstep [ @job_id = ] job_id | [ @job_name = ] 'job_name'   
     [ , [ @step_id = ] step_id ]   
     { , [ @step_name = ] 'step_name' }   
     [ , [ @subsystem = ] 'subsystem' ]   
     [ , [ @command = ] 'command' ]   
     [ , [ @additional_parameters = ] 'parameters' ]   
          [ , [ @cmdexec_success_code = ] code ]   
     [ , [ @on_success_action = ] success_action ]   
          [ , [ @on_success_step_id = ] success_step_id ]   
          [ , [ @on_fail_action = ] fail_action ]   
          [ , [ @on_fail_step_id = ] fail_step_id ]   
     [ , [ @server = ] 'server' ]   
     [ , [ @database_name = ] 'database' ]   
     [ , [ @database_user_name = ] 'user' ]   
     [ , [ @retry_attempts = ] retry_attempts ]   
     [ , [ @retry_interval = ] retry_interval ]   
     [ , [ @os_run_priority = ] run_priority ]   
     [ , [ @output_file_name = ] 'file_name' ]   
     [ , [ @flags = ] flags ]   
     [ , { [ @proxy_id = ] proxy_id   
         | [ @proxy_name = ] 'proxy_name' } ]  
```  
  
## Arguments  
`[ @job_id = ] job_id`
 The identification number of the job to which to add the step. *job_id* is **uniqueidentifier**, with a default of NULL.  
  
`[ @job_name = ] 'job_name'`
 The name of the job to which to add the step. *job_name* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  Either *job_id* or *job_name* must be specified, but both cannot be specified.  
  
`[ @step_id = ] step_id`
 The sequence identification number for the job step. Step identification numbers start at **1** and increment without gaps. If a step is inserted in the existing sequence, the sequence numbers are adjusted automatically. A value is provided if *step_id* is not specified. *step_id*is **int**, with a default of NULL.  
  
`[ @step_name = ] 'step_name'`
 The name of the step. *step_name*is **sysname**, with no default.  
  
`[ @subsystem = ] 'subsystem'`
 The subsystem used by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service to execute *command*. *subsystem* is **nvarchar(40)**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|'**ACTIVESCRIPTING**'|Active Script<br /><br /> **\*\* Important \*\*** [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]|  
|'**CMDEXEC**'|Operating-system command or executable program|  
|'**DISTRIBUTION**'|Replication Distribution Agent job|  
|'**SNAPSHOT**'|Replication Snapshot Agent job|  
|'**LOGREADER**'|Replication Log Reader Agent job|  
|'**MERGE**'|Replication Merge Agent job|  
|'**QueueReader**'|Replication Queue Reader Agent job|  
|'**ANALYSISQUERY**'|Analysis Services query (MDX, DMX).|  
|'**ANALYSISCOMMAND**'|Analysis Services command (XMLA).|  
|'**Dts**'|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package execution|  
|'**PowerShell**'|PowerShell Script|  
|'**TSQL**' (default)|[!INCLUDE[tsql](../../includes/tsql-md.md)] statement|  
  
`[ @command = ] 'command'`
 The commands to be executed by **SQLServerAgent** service through *subsystem*. *command* is **nvarchar(max)**, with a default of NULL. SQL Server Agent provides token substitution that gives you the same flexibility that variables provide when you write software programs.  
  
> [!IMPORTANT]  
>  An escape macro must now accompany all tokens used in job steps, or else those job steps will fail. In addition, you must now enclose token names in parentheses and place a dollar sign (`$`) at the beginning of the token syntax. For example:  
>   
>  `$(ESCAPE_` *macro name* `(DATE))`  
  
 For more information about these tokens and updating your job steps to use the new token syntax, see [Use Tokens in Job Steps](../../ssms/agent/use-tokens-in-job-steps.md).  
  
> [!IMPORTANT]  
>  Any Windows user with write permissions on the Windows Event Log can access job steps that are activated by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alerts or WMI alerts. To avoid this security risk, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent tokens that can be used in jobs activated by alerts are disabled by default. These tokens are: **A-DBN**, **A-SVR**, **A-ERR**, **A-SEV**, **A-MSG**., and **WMI(**_property_**)**. Note that in this release, use of tokens is extended to all alerting.  
>   
>  If you need to use these tokens, first ensure that only members of trusted Windows security groups, such as the Administrators group, have write permissions on the Event Log of the computer where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resides. Then, right-click **SQL Server Agent** in Object Explorer, select **Properties**, and on the **Alert System** page, select **Replace tokens for all job responses to alerts** to enable these tokens.  
  
`[ @additional_parameters = ] 'parameters'`
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)] *parameters* is **ntext**, with a default of NULL.  
  
`[ @cmdexec_success_code = ] code`
 The value returned by a **CmdExec** subsystem command to indicate that *command* executed successfully. *code*is **int**, with a default of **0**.  
  
`[ @on_success_action = ] success_action`
 The action to perform if the step succeeds. *success_action*is **tinyint**, and can be one of these values.  
  
|Value|Description (action)|  
|-----------|----------------------------|  
|**1** (default)|Quit with success|  
|**2**|Quit with failure|  
|**3**|Go to next step|  
|**4**|Go to step *on_success_step_id*|  
  
`[ @on_success_step_id = ] success_step_id`
 The ID of the step in this job to execute if the step succeeds and *success_action*is **4**. *success_step_id*is **int**, with a default of **0**.  
  
`[ @on_fail_action = ] fail_action`
 The action to perform if the step fails. *fail_action*is **tinyint**, and can be one of these values.  
  
|Value|Description (action)|  
|-----------|----------------------------|  
|**1**|Quit with success|  
|**2** (default)|Quit with failure|  
|**3**|Go to next step|  
|**4**|Go to step *on_fail_step_id*|  
  
`[ @on_fail_step_id = ] fail_step_id`
 The ID of the step in this job to execute if the step fails and *fail_action*is **4**. *fail_step_id*is **int**, with a default of **0**.  
  
`[ @server = ] 'server'`
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)] *server*is **nvarchar(30)**, with a default of NULL.  
  
`[ @database_name = ] 'database'`
 The name of the database in which to execute a [!INCLUDE[tsql](../../includes/tsql-md.md)] step. *database* is **sysname**, with a default of NULL, in which case the **master** database is used. Names that are enclosed in brackets ([ ]) are not allowed. For an ActiveX job step, the *database* is the name of the scripting language that the step uses.  
  
`[ @database_user_name = ] 'user'`
 The name of the user account to use when executing a [!INCLUDE[tsql](../../includes/tsql-md.md)] step. *user* is **sysname**, with a default of NULL. When *user* is NULL, the step runs in the job owner's user context on *database*.  SQL Server Agent will include this parameter only if the job owner is a SQL Server sysadmin. If so, the given Transact-SQL step will be executed in the context of the given SQL Server user name. If the job owner is not a SQL Server sysadmin, then the Transact-SQL step will always be executed in the context of the login that owns this job, and the @database_user_name parameter will be ignored.  
  
`[ @retry_attempts = ] retry_attempts`
 The number of retry attempts to use if this step fails. *retry_attempts*is **int**, with a default of **0**, which indicates no retry attempts.  
  
`[ @retry_interval = ] retry_interval`
 The amount of time in minutes between retry attempts. *retry_interval*is **int**, with a default of **0**, which indicates a **0**-minute interval.  
  
`[ @os_run_priority = ] run_priority`
 Reserved.  
  
`[ @output_file_name = ] 'file_name'`
 The name of the file in which the output of this step is saved. *file_name*is **nvarchar(200)**, with a default of NULL. *file_name*can include one or more of the tokens listed under *command*. This parameter is valid only with commands running on the [!INCLUDE[tsql](../../includes/tsql-md.md)], **CmdExec**, **PowerShell**, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], or [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] subsystems.  
  
`[ @flags = ] flags`
 Is an option that controls behavior. *flags* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**0** (default)|Overwrite output file|  
|**2**|Append to output file|  
|**4**|Write [!INCLUDE[tsql](../../includes/tsql-md.md)] job step output to step history|  
|**8**|Write log to table (overwrite existing history)|  
|**16**|Write log to table (append to existing history)|  
|**32**|Write all output to job history|  
|**64**|Create a Windows event to use as a signal for the Cmd jobstep to abort|  
  
`[ @proxy_id = ] proxy_id`
 The id number of the proxy that the job step runs as. *proxy_id* is type **int**, with a default of NULL. If no *proxy_id* is specified, no *proxy_name* is specified, and no *user_name* is specified, the job step runs as the service account for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent.  
  
`[ @proxy_name = ] 'proxy_name'`
 The name of the proxy that the job step runs as. *proxy_name* is type **sysname**, with a default of NULL. If no *proxy_id* is specified, no *proxy_name* is specified, and no *user_name* is specified, the job step runs as the service account for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 **sp_add_jobstep** must be run from the **msdb** database.  
  
 SQL Server Management Studio provides an easy, graphical way to manage jobs, and is the recommended way to create and manage the job infrastructure.  
  
 A job step must specify a proxy unless the creator of the job step is a member of the **sysadmin** fixed security role.  
  
 A proxy may be identified by *proxy_name* or *proxy_id*.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 The creator of the job step must have access to the proxy for the job step. Members of the **sysadmin** fixed server role have access to all proxies. Other users must be explicitly granted access to a proxy.  
  
## Examples  
 The following example creates a job step that changes database access to read-only for the Sales database. In addition, this example specifies 5 retry attempts, with each retry to occur after a 5 minute wait.  
  
> [!NOTE]  
>  This example assumes that the `Weekly Sales Data Backup` job already exists.  
  
```  
USE msdb;  
GO  
EXEC sp_add_jobstep  
    @job_name = N'Weekly Sales Data Backup',  
    @step_name = N'Set database to read only',  
    @subsystem = N'TSQL',  
    @command = N'ALTER DATABASE SALES SET READ_ONLY',   
    @retry_attempts = 5,  
    @retry_interval = 5 ;  
GO  
```  
  
## See Also  
 [View or Modify Jobs](../../ssms/agent/view-or-modify-jobs.md)   
 [sp_add_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-job-transact-sql.md)   
 [sp_add_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md)   
 [sp_delete_jobstep &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-jobstep-transact-sql.md)   
 [sp_help_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-job-transact-sql.md)   
 [sp_help_jobstep &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-jobstep-transact-sql.md)   
 [sp_update_jobstep &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-jobstep-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
