---
title: "sp_delete_jobschedule (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_delete_jobschedule"
  - "sp_delete_jobschedule_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_delete_jobschedule"
ms.assetid: 82fbb48b-603a-4016-a7fb-1ce17fb76919
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_delete_jobschedule (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Deletes a schedule for a job.  
  
 **sp_delete_jobschedule** is provided for backward compatibility only.  
  
  
## Remarks  
 Job schedules can now be managed independently of jobs. To remove a schedule from a job, use **sp_detach_schedule**. To delete a schedule, use **sp_delete_schedule**.  
  
> **NOTE:  sp_delete_jobschedule** does not support schedules that are attached to multiple jobs. If an existing script calls **sp_delete_jobschedule** to remove a schedule that is attached to more than one job, the procedure returns an error.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 Members of the **sysadmin** role can delete any job schedule. Users who are not members of the **sysadmin** role can only delete job schedules that they own.  
  
## See Also  
 [sp_delete_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-schedule-transact-sql.md)   
 [sp_detach_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-detach-schedule-transact-sql.md)   
 [View or Modify Jobs](../../ssms/agent/view-or-modify-jobs.md)   
 [sp_add_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md)   
 [sp_help_jobschedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-jobschedule-transact-sql.md)   
 [sp_update_jobschedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-jobschedule-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
