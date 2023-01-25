---
description: "sp_update_jobschedule (Transact-SQL)"
title: "sp_update_jobschedule (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_update_jobschedule_TSQL"
  - "sp_update_jobschedule"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_update_jobschedule"
ms.assetid: 4df02594-4cd1-49a9-8d97-37c44e4d5423
author: markingmyname
ms.author: maghan
---
# sp_update_jobschedule (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Changes the schedule settings for the specified job.  
  
 **sp_update_jobschedule** is provided for backward compatibility only.  
  
> [!IMPORTANT]
>  For more information about syntax used in earlier versions of Microsoft SQL Server, see the Transact-SQL Referencefor Microsoft SQL Server 2000*.*  
  
## Remarks  
 Job schedules can now be managed independently of jobs. To update a schedule, use **sp_update_schedule**.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 Only members of **sysadmin** can use this stored procedure to update job schedules that are owned by other users.  
  
## See Also  
 [SQL Server Agent Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sql-server-agent-stored-procedures-transact-sql.md)   
 [sp_update_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-schedule-transact-sql.md)  
  
  
