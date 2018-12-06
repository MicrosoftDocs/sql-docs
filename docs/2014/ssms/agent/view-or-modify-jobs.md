---
title: "View or Modify Jobs | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "jobs [SQL Server Agent], modifying"
  - "jobs [SQL Server Agent], viewing"
  - "modifying jobs"
  - "viewing jobs"
  - "SQL Server Agent jobs, viewing"
  - "SQL Server Agent jobs, modifying"
  - "displaying jobs"
ms.assetid: 57f649b8-190c-4304-abd7-7ca5297deab7
author: stevestein
ms.author: sstein
manager: craigg
---
# View or Modify Jobs
  You can view any job you have created. After you have run a job, you can also view its history. Viewing a job's history allows you to see when the job ran, the status of the job as a whole, and the status of each job step in the job. You can see whether the job ever failed in the past, when the job last completed successfully, and what output the job created each time the job ran. Members of the **sysadmin** fixed server role can view or modify any job, regardless of the owner.  
  
> [!NOTE]  
>  A job must have been executed at least one time for there to be a job history. You can limit the total size of the job history log and the size per job.  
  
 Finally, you can modify a job to meet new requirements. You can modify:  
  
-   Response options  
  
-   Schedules  
  
-   Job steps  
  
-   Ownership  
  
-   Job category  
  
-   Target servers (multiserver jobs only)  
  
 To make sure that changes to multiserver jobs take effect, you must post the changes to the download list so that target servers can download the updated job. To ensure that target servers have the most current job definitions, post an INSERT instruction after you update the multiserver job as follows:  
  
```  
EXECUTE sp_post_msx_operation 'INSERT', 'JOB', '<job id>'  
```  
  
 For more information, see [sp_purge_jobhistory &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-purge-jobhistory-transact-sql).  
  
 Members of the **sysadmin** fixed server role can view the definition or history of any job, and can modify any job.  
  
## Related Tasks  
  
|||  
|-|-|  
|**Description**|**Topic**|  
|Describes how to view [!INCLUDE[msCoName](../../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent jobs.|[View a Job](view-a-job.md)|  
|Describes how to view the [!INCLUDE[msCoName](../../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent job history log.|[View the Job History](view-the-job-history.md)|  
|Describes how to delete the contents of the [!INCLUDE[msCoName](../../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent job history log.|[Clear the Job History Log](clear-the-job-history-log.md)|  
|Describes how to set size limits for [!INCLUDE[msCoName](../../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent job history logs.|[Resize the Job History Log](resize-the-job-history-log.md)|  
|Describes how to change the properties of [!INCLUDE[msCoName](../../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent jobs.|[Modify a Job](modify-a-job.md)|  
  
## See Also  
 [dbo.sysjobhistory &#40;Transact-SQL&#41;](/sql/relational-databases/system-tables/dbo-sysjobhistory-transact-sql)  
  
  
