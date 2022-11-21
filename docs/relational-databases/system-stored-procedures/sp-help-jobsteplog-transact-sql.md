---
description: "sp_help_jobsteplog (Transact-SQL)"
title: "sp_help_jobsteplog (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_help_jobsteplog_TSQL"
  - "sp_help_jobsteplog"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_jobsteplog"
ms.assetid: 1a0be7b1-8f31-4b4c-aadb-586c0e00ed04
author: markingmyname
ms.author: maghan
---
# sp_help_jobsteplog (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns metadata about a specific [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job step log. **sp_help_jobsteplog** does not return the actual log.  

  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_jobsteplog { [ @job_id = ] 'job_id' | [ @job_name = ] 'job_name' }  
     [ , [ @step_id = ] step_id ]  
     [ , [ @step_name = ] 'step_name' ]  
```  
  
## Arguments  
`[ @job_id = ] 'job_id'`
 The job identification number for which to return job step log information. *job_id* is **int**, with a default of NULL.  
  
`[ @job_name = ] 'job_name'`
 The name of the job. *job_name* is **sysname**, with a default NULL.  
  
> [!NOTE]  
>  Either *job_id* or *job_name* must be specified, but both cannot be specified.  
  
`[ @step_id = ] step_id`
 The identification number of the step in the job. If not included, all steps in the job are included. *step_id* is **int**, with a default of NULL.  
  
`[ @step_name = ] 'step_name'`
 The name of the step in the job. *step_name* is **sysname**, with a default of NULL.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**job_id**|**uniqueidentifier**|Unique identifier of the job.|  
|**job_name**|**sysname**|Name of the job.|  
|**step_id**|**int**|Identifier for the step within the job. For example, if the step is the first step in the job, its *step_id* is 1.|  
|**step_name**|**sysname**|Name of the step in the job.|  
|**step_uid**|**uniqueidentifier**|Unique identifier of the step (system generated) in the job.|  
|**date_created**|**datetime**|Date that the step was created.|  
|**date_modified**|**datetime**|Date that the step was last modified.|  
|**log_size**|**float**|Size of the job step log in megabytes (MB).|  
|**log**|**nvarchar(max)**|Job step log output.|  
  
## Remarks  
 **sp_help_jobsteplog** is in the **msdb** database.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 Members of **SQLAgentUserRole** can only view job step log metadata for job steps that they own.  
  
## Examples  
  
### A. Returns job step log information for all steps in a specific job  
 The following example returns all the job step log information for the job named `Weekly Sales Data Backup`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_jobsteplog  
    @job_name = N'Weekly Sales Data Backup' ;  
GO  
```  
  
### B. Return job step log information about a specific job step  
 The following example returns job step log information about the first job step for the job named `Weekly Sales Data Backup`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_jobsteplog  
    @job_name = N'Weekly Sales Data Backup',  
    @step_id = 1 ;  
GO  
```  
  
## See Also  
 [sp_add_jobstep &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-jobstep-transact-sql.md)   
 [sp_delete_jobstep &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-jobstep-transact-sql.md)   
 [sp_help_jobstep &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-jobstep-transact-sql.md)   
 [sp_delete_jobstep &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-jobstep-transact-sql.md)   
 [sp_delete_jobsteplog &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-jobsteplog-transact-sql.md)   
 [SQL Server Agent Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sql-server-agent-stored-procedures-transact-sql.md)  
  
  
