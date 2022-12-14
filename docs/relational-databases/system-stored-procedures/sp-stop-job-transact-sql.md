---
description: "sp_stop_job (Transact-SQL)"
title: "sp_stop_job (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/01/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_stop_job_TSQL"
  - "sp_stop_job"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_stop_job"
ms.assetid: 64b4cc75-99a0-421e-b418-94e37595bbb0
author: markingmyname
ms.author: maghan
---
# sp_stop_job (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Instructs [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to stop the execution of a job.  

  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_stop_job   
      [@job_name =] 'job_name'  
    | [@job_id =] job_id   
    | [@originating_server =] 'master_server'  
    | [@server_name =] 'target_server'  
```  
  
## Arguments  
`[ @job_name = ] 'job_name'`
 The name of the job to stop. *job_name* is **sysname**, with a default of NULL.  
  
`[ @job_id = ] job_id`
 The identification number of the job to stop. *job_id* is **uniqueidentifier**, with a default of NULL.  
  
`[ @originating_server = ] 'master_server'`
 The name of the master server. If specified, all multiserver jobs are stopped. *master_server* is **nvarchar(128)**, with a default of NULL. Specify this parameter only when calling **sp_stop_job** at a target server.  
  
> [!NOTE]  
>  Only one of the first three parameters can be specified.  
  
`[ @server_name = ] 'target_server'`
 The name of the specific target server on which to stop a multiserver job. *target_server* is **nvarchar(128)**, with a default of NULL. Specify this parameter only when calling **sp_stop_job** at a master server for a multiserver job.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 **sp_stop_job** sends a stop signal to the database. Some processes can be stopped immediately and some must reach a stable point (or an entry point to the code path) before they can stop. Some long-running [!INCLUDE[tsql](../../includes/tsql-md.md)] statements such as BACKUP, RESTORE, and some DBCC commands can take a long time to finish. When these are running, it may take a while before the job is canceled. Stopping a job causes a "Job Canceled" entry to be recorded in the job history.  
  
 If a job is currently executing a step of type **CmdExec** or **PowerShell**, the process being run (for example, MyProgram.exe) is forced to end prematurely. Premature ending can result in unpredictable behavior such as files in use by the process being held open. Consequently, **sp_stop_job** should be used only in extreme circumstances if the job contains steps of type **CmdExec** or **PowerShell**.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 Members of **SQLAgentUserRole** and **SQLAgentReaderRole** can only stop jobs that they own. Members of **SQLAgentOperatorRole** can stop all local jobs including those that are owned by other users. Members of **sysadmin** can stop all local and multiserver jobs.  
  
## Examples  
 The following example stops a job named `Weekly Sales Data Backup`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_stop_job  
    N'Weekly Sales Data Backup' ;  
GO  
```  
  
## See Also  
 [sp_delete_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-job-transact-sql.md)   
 [sp_help_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-job-transact-sql.md)   
 [sp_start_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-start-job-transact-sql.md)   
 [sp_update_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-job-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
