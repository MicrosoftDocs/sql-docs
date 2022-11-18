---
description: "sp_add_jobserver (Transact-SQL)"
title: "sp_add_jobserver (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_add_jobserver"
  - "sp_add_jobserver_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_add_jobserver"
ms.assetid: 485252cc-0081-490a-9bd1-cbbd68eea286
author: markingmyname
ms.author: maghan
---
# sp_add_jobserver (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Targets the specified job at the specified server.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_add_jobserver [ @job_id = ] job_id | [ @job_name = ] 'job_name'  
     [ , [ @server_name = ] 'server' ]   
```  
  
## Arguments  
`[ @job_id = ] job_id`
 The identification number of the job. *job_id* is **uniqueidentifier**, with a default of NULL.  
  
`[ @job_name = ] 'job_name'`
 The name of the job. *job_name* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  Either *job_id* or *job_name* must be specified, but both cannot be specified.  
  
`[ @server_name = ] 'server'`
 The name of the server at which to target the job. *server* is **nvarchar(30)**, with a default of N'(LOCAL)'. *server* can be either **(LOCAL)** for a local server, or the name of an existing target server.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 **\@automatic_post** exists in **sp_add_jobserver**, but is not listed under Arguments. **\@automatic_post** is reserved for internal use.  
  
 [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides an easy, graphical way to manage jobs, and is the recommended way to create and manage the job infrastructure.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 Only members of the **sysadmin** fixed server role can execute **sp_add_jobserver** for jobs that involve multiple servers.  
  
## Examples  
  
### A. Assigning a job to the local server  
 The following example assigns the job `NightlyBackups` to run on the local server.  
  
> [!NOTE]  
>  This example assumes that the `NightlyBackups` job already exists.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_add_jobserver  
    @job_name = N'NightlyBackups' ;  
GO  
```  
  
### B. Assigning a job to run on a different server  
 The following example assigns the multiserver job `Weekly Sales Backups` to the server `SEATTLE2`.  
  
> [!NOTE]  
>  This example assumes that the `Weekly Sales Backups` job already exists and that `SEATTLE2` is registered as a target server for the current instance.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_add_jobserver  
    @job_name = N'Weekly Sales Backups',  
    @server_name = N'SEATTLE2' ;  
GO  
```  
  
## See Also  
 [sp_apply_job_to_targets &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-apply-job-to-targets-transact-sql.md)   
 [sp_delete_jobserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-jobserver-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
