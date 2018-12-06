---
title: "sp_delete_jobsteplog (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_delete_jobsteplog"
  - "sp_delete_jobsteplog_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_delete_jobsteplog"
ms.assetid: e9ef4c99-abde-4038-b6a3-a25dcbaf0958
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_delete_jobsteplog (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job step logs that are specified with the arguments. Use this stored procedure to maintain the **sysjobstepslogs** table in the **msdb** database.  
  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_delete_jobsteplog { [ @job_id = ] 'job_id' | [ @job_name = ] 'job_name' }  
       [ , [ @step_id = ] step_id | [ @step_name = ] 'step_name' ]  
       [ , [ @older_than = ] 'date' ]  
       [ , [ @larger_than = ] 'size_in_bytes' ]  
```  
  
## Arguments  
 [ **@job_id =**] **'**_job_id_**'**  
 The job identification number for the job that contains the job step log to be removed. *job_id* is **int**, with a default of NULL.  
  
 [ **@job_name =**] **'**_job_name_**'**  
 The name of the job. *job_name* is **sysname**, with a default of NULL.  
  
> **NOTE:** Either *job_id* or *job_name* must be specified, but both cannot be specified.  
  
 [ **@step_id =**] *step_id*  
 The identification number of the step in the job for which the job step log is to be deleted. If not included, all job step logs in the job are deleted unless **@older_than** or **@larger_than** are specified. *step_id* is **int**, with a default of NULL.  
  
 [ **@step_name =**] **'**_step_name_**'**  
 The name of the step in the job for which the job step log is to be deleted. *step_name* is **sysname**, with a default of NULL.  
  
> **NOTE:** Either *step_id* or *step_name* can be specified, but both cannot be specified.  
  
 [ **@older_than =**] **'**_date_**'**  
 The date and time of the oldest job step log you want to keep. All job step logs that are older than this date and time are removed. *date* is **datetime**, with a default of NULL. Both **@older_than** and **@larger_than** can be specified.  
  
 [ **@larger_than =**] **'**_size_in_bytes_**'**  
 The size in bytes of the largest job step log you want to keep. All job step logs that are larger that this size are removed. Both **@larger_than** and **@older_than** can be specified.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 **sp_delete_jobsteplog** is in the **msdb** database.  
  
 If no arguments except **@job_id** or **@job_name** are specified, all job step logs for the specified job are deleted.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 Only members of **sysadmin** can delete a job step log that is owned by another user.  
  
## Examples  
  
### A. Removing all job step logs from a job  
 The following example removes all job step logs for the job `Weekly Sales Data Backup`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_delete_jobsteplog  
    @job_name = N'Weekly Sales Data Backup';  
GO  
```  
  
### B. Removing the job step log for a particular job step  
 The following example removes the job step log for step 2 in the job `Weekly Sales Data Backup`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_delete_jobsteplog  
    @job_name = N'Weekly Sales Data Backup',  
    @step_id = 2;  
GO  
```  
  
### C. Removing all job step logs based on age and size  
 The following example removes all job steps logs that are older than noon October 25, 2005 and larger than 100 megabytes (MB) from the job `Weekly Sales Data Backup`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_delete_jobsteplog  
    @job_name = N'Weekly Sales Data Backup',  
    @older_than = '10/25/2005 12:00:00',  
    @larger_than = 104857600;  
GO  
```  
  
## See Also  
 [sp_help_jobsteplog &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-jobsteplog-transact-sql.md)   
 [SQL Server Agent Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sql-server-agent-stored-procedures-transact-sql.md)  
  
  
