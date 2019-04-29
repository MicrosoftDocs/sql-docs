---
title: "sp_help_jobactivity (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_help_jobactivity_TSQL"
  - "sp_help_jobactivity"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_jobactivity"
ms.assetid: d344864f-b4d3-46b1-8933-b81dec71f511
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_help_jobactivity (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Lists information about the runtime state of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_jobactivity { [ @job_id = ] job_id | [ @job_name = ] 'job_name' }  
     [ , [ @session_id = ] session_id ]  
```  
  
## Arguments  
`[ @job_id = ] job_id`
 The job identification number. *job_id*is **uniqueidentifier**, with a default of NULL.  
  
`[ @job_name = ] 'job_name'`
 The name of the job. *job_name*is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  Either *job_id* or *job_name* must be specified, but both cannot be specified.  
  
`[ @session_id = ] session_id`
 The session id to report information about. *session_id* is **int**, with a default of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 Returns the following result set:  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**session_id**|**int**|Agent session identification number.|  
|**job_id**|**uniqueidentifier**|Identifier for the job.|  
|**job_name**|**sysname**|Name of the job.|  
|**run_requested_date**|**datetime**|When that the job was requested to run.|  
|**run_requested_source**|**sysname**|The source of the request to run the job. One of:<br /><br /> **1** = Run on a schedule<br /><br /> **2** = Run in response to an alert<br /><br /> **3** = Run on startup<br /><br /> **4** = Run by user<br /><br /> **6** = Run on CPU idle schedule|  
|**queued_date**|**datetime**|When the request was queued. NULL if the job was run directly.|  
|**start_execution_date**|**datetime**|When the job was assigned to a runnable thread.|  
|**last_executed_step_id**|**int**|The step ID of the most recently run job step.|  
|**last_exectued_step_date**|**datetime**|The time that the most recently run job step started to run.|  
|**stop_execution_date**|**datetime**|The time that the job stopped running.|  
|**next_scheduled_run_date**|**datetime**|When the job is next scheduled to run.|  
|**job_history_id**|**int**|Identifier for the job history in the job history table.|  
|**message**|**nvarchar(1024)**|Message produced during the last run of the job.|  
|**run_status**|**int**|Status returned from the last run of the job:<br /><br /> **0** = Error failed<br /><br /> **1** = Succeeded<br /><br /> **3** = Canceled<br /><br /> **5** = Status unknown|  
|**operator_id_emailed**|**int**|ID number of the operator notified via email at completion of the job.|  
|**operator_id_netsent**|**int**|ID number of the operator notified via **net send** at completion of the job.|  
|**operator_id_paged**|**int**|ID number of the operator notified via pager at completion of the job.|  
  
## Remarks  
 This procedure provides a snapshot of the current state of the jobs. The results returned represent information at the time that the request is processed.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent creates a session id each time that the Agent service starts. The session id is stored in the table **msdb.dbo.syssessions**.  
  
 When no *session_id* is provided, lists information about the most recent session.  
  
 When no *job_name* or *job_id* is provided, lists information for all jobs.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can run this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 Only members of **sysadmin** can view the activity for jobs owned by other users.  
  
## Examples  
 The following example lists activity for all jobs that the current user has permission to view.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_jobactivity ;  
GO  
```  
  
## See Also  
 [SQL Server Agent Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sql-server-agent-stored-procedures-transact-sql.md)  
  
  
