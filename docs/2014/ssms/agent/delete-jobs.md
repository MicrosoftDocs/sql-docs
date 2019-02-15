---
title: "Delete Jobs | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "delete jobs"
ms.assetid: bffb915e-bc84-4417-aa35-183243ca3312
author: stevestein
ms.author: sstein
manager: craigg
---
# Delete Jobs
  A job is a specified series of operations performed sequentially by SQL Server Agent. By default, jobs are not deleted when execution finishes. You can delete one or more [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs regardless of success or failure of the job. You can also configure [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to automatically delete jobs when they succeed, fail, or complete.  
  
 By default, members of the **sysadmin** fixed server role can execute the [sp_delete_job &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-delete-job-transact-sql) system stored procedure to delete a job. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](sql-server-agent-fixed-database-roles.md).  
  
 Members of the **sysadmin** fixed server role can execute **sp_delete_job** to delete any job. A user that is not a member of the **sysadmin** fixed server role can only delete jobs owned by that user.  
  
## Related Tasks  
  
|||  
|-|-|  
|**Description**|**Topic**|  
|Describes how to delete one or more [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs.|[Delete One or More Jobs](delete-one-or-more-jobs.md)|  
|Describes how to configure [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to automatically delete jobs when they succeed, fail, or complete.|[Automatically Delete a Job](automatically-delete-a-job.md)|  
  
  
