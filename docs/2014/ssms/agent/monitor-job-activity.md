---
title: "Monitor Job Activity | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent, monitoring"
  - "jobs [SQL Server Agent], monitoring"
  - "monitoring [SQL Server], jobs"
  - "activity monitoring [SQL Server Agent]"
  - "monitoring [SQL Server], SQL Server Agent"
  - "monitoring [SQL Server Agent]"
  - "SQL Server Agent Job Activity Monitor"
  - "SQL Server Agent jobs, monitoring"
  - "performance [SQL Server], jobs"
  - "current activity"
ms.assetid: 71cb432b-631d-4b8b-9965-e731b3d8266d
author: stevestein
ms.author: sstein
manager: craigg
---
# Monitor Job Activity
  You can monitor the current activity of all defined jobs on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent Job Activity Monitor.  
  
## SQL Server Agent Sessions  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent creates a new session each time the service starts. When a new session is created, the **sysjobactivity** table in the **msdb** database is populated with all the existing defined jobs. This table preserves the last activity for jobs when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is restarted. Each session records [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent normal job activity from the start of the job to its finish. Information about these sessions is stored in the **syssessions** table of the **msdb** database.  
  
## Job Activity Monitor  
 The Job Activity Monitor allows you to view the **sysjobactivity** table by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. You can view all jobs on the server, or you can define filters to limit the number of jobs displayed. You can also sort the job information by clicking on a column heading in the **Agent Job Activity** grid. For example, when you select the **Last Run** column heading, you can view the jobs in the order that they were last run. Clicking the column heading again toggles the jobs in ascending and descending order based on their last run date.  
  
 Using the Job Activity Monitor you can perform the following tasks:  
  
-   Start and stop jobs.  
  
-   View job properties.  
  
-   View the history for a specific job.  
  
-   Refresh the information in the **Agent Job Activity** grid manually or set an automatic refresh interval by clicking **View refresh settings**.  
  
 Use the Job Activity Monitor when you want to find out what jobs are scheduled to run, the last outcome of jobs that have run during the current session, and to find out which jobs are currently running or idle. If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service fails unexpectedly, you can determine which jobs were in the middle of being executed by looking at the previous session in the Job Activity Monitor.  
  
 To open the Job Activity Monitor, expand **SQL Server Agent** in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] Object Explorer, right-click **Job Activity Monitor**, and click **View Job Activity**.  
  
 You can also view job activity for the current session by using the stored procedure **sp_help_jobactivity**.  
  
## Related Tasks  
  
|||  
|-|-|  
|**Description**|**Topic**|  
|Describes how to view the runtime state of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs.|[View Job Activity](view-job-activity.md)|  
  
## See Also  
 [View Job Activity](view-job-activity.md)   
 [dbo.sysjobactivity &#40;Transact-SQL&#41;](/sql/relational-databases/system-tables/dbo-sysjobactivity-transact-sql)   
 [dbo.syssessions &#40;Transact-SQL&#41;](/sql/relational-databases/system-tables/dbo-syssessions-transact-sql)   
 [sp_help_jobactivity &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-help-jobactivity-transact-sql)  
  
  
