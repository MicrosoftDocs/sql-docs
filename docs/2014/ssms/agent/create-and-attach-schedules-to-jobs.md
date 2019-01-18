---
title: "Create and Attach Schedules to Jobs | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "jobs [SQL Server]"
  - "scheduling jobs [SQL Server]"
  - "jobs [SQL Server], scheduling"
  - "CPU [SQL Server], idle conditions"
  - "automatic job processing"
  - "SQL Server Agent jobs, scheduling"
  - "idle time [SQL Server]"
ms.assetid: 079c2984-0052-4a37-a2b8-4ece56e6b6b5
author: stevestein
ms.author: sstein
manager: craigg
---
# Create and Attach Schedules to Jobs
  Scheduling [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs means defining the condition or conditions that cause the job to begin running without user interaction. You can schedule a job to run automatically by creating a new schedule for the job, or by attaching an existing schedule to the job.  
  
 There are two ways to create a schedule:  
  
-   Create the schedule while you are creating a job.  
  
-   Create the schedule in Object Explorer.  
  
 After a schedule has been created, you can attach that schedule to multiple jobs, even if the schedule was created for a specific job. You can also detach schedules from jobs.  
  
 A schedule can be based upon time or an event. For example, you can schedule a job to run at the following times:  
  
-   Whenever [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent starts.  
  
-   Whenever CPU utilization of the computer is at a level you have defined as idle.  
  
-   One time, at a specific date and time.  
  
-   On a recurring schedule.  
  
 As an alternative to job schedules, you can also create an alert that responds to an event by running a job.  
  
> [!NOTE]  
>  Only one instance of the job can be run at a time. If you try to run a job manually while it is running as scheduled, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent refuses the request.  
  
 To prevent a scheduled job from running, you must do one of the following:  
  
-   Disable the schedule.  
  
-   Disable the job.  
  
-   Detach the schedule from the job.  
  
-   Stop the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service.  
  
-   Delete the schedule.  
  
 If the schedule is not enabled, the job can still run in response to an alert or when a user runs the job manually. When a job schedule is not enabled, the schedule is not enabled for any job that uses the schedule.  
  
 You must explicitly re-enable a schedule that has been disabled. Editing the schedule does not automatically re-enable the schedule.  
  
## Scheduling Start Dates  
 The start date of a schedule must be greater than or equal to 19900101.  
  
 When you are attaching a schedule to a job, you should review the start date that the schedule uses to run the job for the first time. The start date depends upon the day and time when you attach the schedule to the job. For example, you create a schedule that runs every other Monday at 8:00 A.M. If you create a job at 10:00 A.M. on Monday, March 3, 2008, the schedule start date is Monday, March 17, 2008. If you create another job on Tuesday, March 4, 2008, the schedule start date is Monday, March 10, 2008.  
  
 You can change the schedule start date after you attach the schedule to a job.  
  
## CPU Idle Schedules  
 To maximize CPU resources, you can define a CPU idle condition for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent uses the CPU idle condition setting to determine the best time to run jobs. For example, you can schedule a job to rebuild indexes during CPU idle time and slow production periods.  
  
 Before you define jobs to run during CPU idle time, determine the load on the CPU during normal processing. To do this, use [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] or Performance Monitor to monitor server traffic and collect statistics. You can then use the information you gather to set the CPU idle time percentage and duration.  
  
 Define the CPU idle condition as a percentage below which CPU usage must remain for a specified time. Next, set the amount of time. When the CPU usage is below the specified percentage for the specified amount of time, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent starts all jobs that have a CPU idle time schedule. For more information on using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] or Performance Monitor to monitor CPU usage, see [Monitor CPU Usage](../../relational-databases/performance-monitor/monitor-cpu-usage.md).  
  
## Related Tasks  
  
|||  
|-|-|  
|**Description**|**Topic**|  
|Describes how to create a schedule for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job.|[Create a Schedule](create-a-schedule.md)|  
|Describes how to schedule a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job.|[Schedule a Job](schedule-a-job.md)|  
|Explains how to define the CPU idle condition for your server.|[Set CPU Idle Time and Duration &#40;SQL Server Management Studio&#41;](set-cpu-idle-time-and-duration-sql-server-management-studio.md)|  
  
## See Also  
 [sp_help_jobschedule &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-help-jobschedule-transact-sql)   
 [dbo.sysjobschedules &#40;Transact-SQL&#41;](/sql/relational-databases/system-tables/dbo-sysjobschedules-transact-sql)  
  
  
