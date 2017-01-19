---
title: "Delete Jobs | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "delete jobs"
ms.assetid: bffb915e-bc84-4417-aa35-183243ca3312
caps.latest.revision: 5
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Delete Jobs
A job is a specified series of operations performed sequentially by SQL Server Agent. By default, jobs are not deleted when execution finishes. You can delete one or more [!INCLUDE[msCoName](../../includes/msconame_md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent jobs regardless of success or failure of the job. You can also configure [!INCLUDE[msCoName](../../includes/msconame_md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent to automatically delete jobs when they succeed, fail, or complete.  
  
By default, members of the **sysadmin** fixed server role can execute the [sp_delete_job (Transact-SQL)](http://msdn.microsoft.com/en-us/b85db6e4-623c-41f1-9643-07e5ea38db09) system stored procedure to delete a job. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
Members of the **sysadmin** fixed server role can execute **sp_delete_job** to delete any job. A user that is not a member of the **sysadmin** fixed server role can only delete jobs owned by that user.  
  
## Related Tasks  
  
|||  
|-|-|  
|**Description**|**Topic**|  
|Describes how to delete one or more [!INCLUDE[msCoName](../../includes/msconame_md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent jobs.|[Delete One or More Jobs](../../ssms/agent/delete-one-or-more-jobs.md)|  
|Describes how to configure [!INCLUDE[msCoName](../../includes/msconame_md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent to automatically delete jobs when they succeed, fail, or complete.|[Automatically Delete a Job](../../ssms/agent/automatically-delete-a-job.md)|  
  
