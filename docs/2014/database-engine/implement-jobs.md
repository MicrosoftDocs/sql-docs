---
title: "Implement Jobs | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-cross-instance"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "jobs [SQL Server Agent]"
  - "SQL Server Agent jobs"
  - "SQL Server Agent jobs, about jobs"
  - "jobs [SQL Server Agent], about jobs"
ms.assetid: 69e06724-25c7-4fb3-8a5b-3d4596f21756
caps.latest.revision: 27
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Implement Jobs
  You can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs to automate routine administrative tasks and run them on a recurring basis, making administration more efficient.  
  
 A job is a specified series of operations performed sequentially by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. A job can perform a wide range of activities, including running [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts, command-line applications, Microsoft ActiveX scripts, Integration Services packages, Analysis Services commands and queries, or Replication tasks. Jobs can run repetitive tasks or those that can be scheduled, and they can automatically notify users of job status by generating alerts, thereby greatly simplifying [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administration.  
  
 You can run a job manually, or you can configure it to run according to a schedule or in response to alerts.  
  
## Related Tasks  
  
|||  
|-|-|  
|**Description**|**Topic**|  
|Contains information about creating jobs and assigning ownership.|[Create Jobs](../../2014/database-engine/create-jobs.md)|  
|Contains information about organizing jobs into categories.|[Organize Jobs](../../2014/database-engine/organize-jobs.md)|  
|Contains information about the different kinds of job steps you can create and how to manage them.|[Manage Job Steps](../../2014/database-engine/manage-job-steps.md)|  
|Contains information about how to define when jobs start running and how often they should run.|[Create and Attach Schedules to Jobs](../../2014/database-engine/create-and-attach-schedules-to-jobs.md)|  
|Contains information about manually running jobs (without a schedule).|[Run Jobs](../../2014/database-engine/run-jobs.md)|  
|Contains information about how you can configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to respond to jobs. For example, you can configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to notify administrators when jobs are finished.|[Specify Job Responses](../../2014/database-engine/specify-job-responses.md)|  
|Contains information about how to view existing jobs, their history once executes, and how to modify them.|[View or Modify Jobs](../../2014/database-engine/view-or-modify-jobs.md)|  
|Contains information about how to delete jobs.|[Delete Jobs](../../2014/database-engine/delete-jobs.md)|  
  
## See Also  
 [Implement SQL Server Agent Security](../../2014/database-engine/implement-sql-server-agent-security.md)  
  
  