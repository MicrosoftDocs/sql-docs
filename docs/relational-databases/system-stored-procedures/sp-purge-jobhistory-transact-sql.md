---
description: "sp_purge_jobhistory (Transact-SQL)"
title: "sp_purge_jobhistory (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_purge_jobhistory_TSQL"
  - "sp_purge_jobhistory"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_purge_jobhistory"
ms.assetid: 237f9bad-636d-4262-9bfb-66c034a43e88
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_purge_jobhistory (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Removes the history records for a job.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_purge_jobhistory   
   {   [ @job_name = ] 'job_name' |   
     | [ @job_id = ] job_id }  
   [ , [ @oldest_date = ] oldest_date ]  
```  
  
## Arguments  
`[ @job_name = ] 'job_name'`
 The name of the job for which to delete the history records. *job_name*is **sysname**, with a default of NULL. Either *job_id* or *job_name* must be specified, but both cannot be specified.  
  
> [!NOTE]  
>  Members of the **sysadmin** fixed server role or members of the **SQLAgentOperatorRole** fixed database role can execute **sp_purge_jobhistory** without specifying a *job_name* or *job_id*. When **sysadmin** users do not specify these arguments, the job history for all local and multiserver jobs is deleted within the time specified by *oldest_date*. When **SQLAgentOperatorRole** users do not specify these arguments, the job history for all local jobs is deleted within the time specified by *oldest_date*.  
  
`[ @job_id = ] job_id`
 The job identification number of the job for the records to be deleted. *job_id* is **uniqueidentifier**, with a default of NULL. Either *job_id* or *job_name* must be specified, but both cannot be specified. See the note in the description of **\@job_name** for information about how **sysadmin** or **SQLAgentOperatorRole** users can use this argument.  
  
`[ @oldest_date = ] oldest_date`
 The oldest record to retain in the history. *oldest_date* is **datetime**, with a default of NULL. When *oldest_date* is specified, **sp_purge_jobhistory** only removes records that are older than the value specified.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 When **sp_purge_jobhistory** completes successfully, a message is returned.  
  
## Permissions  
 By default, only members of the **sysadmin** fixed server role or the **SQLAgentOperatorRole** fixed database role can execute this stored procedure. Members of **sysadmin** can purge the job history for all local and multiserver jobs. Members of **SQLAgentOperatorRole** can purge the job history for all local jobs only.  
  
 Other users, including members of **SQLAgentUserRole** and members of **SQLAgentReaderRole**, must explicitly be granted the EXECUTE permission on **sp_purge_jobhistory**. After being granted EXECUTE permission on this stored procedure, these users can only purge the history for jobs that they own.  
  
 The **SQLAgentUserRole**, **SQLAgentReaderRole**, and **SQLAgentOperatorRole** fixed database roles are in the **msdb** database. For details about their permissions, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
## Examples  
  
### A. Remove history for a specific job  
 The following example removes the history for a job named `NightlyBackups`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_purge_jobhistory  
    @job_name = N'NightlyBackups' ;  
GO  
```  
  
### B. Remove history for all jobs  
  
> [!NOTE]  
>  Only members of the **sysadmin** fixed server role and members of the **SQLAgentOperatorRole** can remove history for all jobs. When **sysadmin** users execute this stored procedure with no parameters, the job history for all local and multiserver jobs is purged. When **SQLAgentOperatorRole** users execute this stored procedure with no parameters, only the job history for all local jobs is purged.  
  
 The following example executes the procedure with no parameters to remove all history records.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_purge_jobhistory ;  
GO  
```  
  
## See Also  
 [sp_help_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-job-transact-sql.md)   
 [sp_help_jobhistory &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-jobhistory-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [GRANT Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-object-permissions-transact-sql.md)  
  
  
