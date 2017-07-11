---
title: "Create Jobs | Microsoft Docs"
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
  - "jobs [SQL Server Agent], creating"
  - "SQL Server Agent jobs, creating"
ms.assetid: 465fb7fc-7622-4252-a178-ea51691c935b
caps.latest.revision: 3
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Create Jobs
A job is a specified series of operations performed sequentially by [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent. A job can perform a wide range of activities, including running [!INCLUDE[tsql](../../includes/tsql_md.md)] scripts, command prompt applications, Microsoft ActiveX scripts, Integration Services packages, Analysis Services commands and queries, or Replication tasks. Jobs can run repetitive or schedulable tasks, and they can automatically notify users of job status by generating alerts, thereby greatly simplifying [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] administration.  
  
To create a job, a user must be a member of one of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent fixed database roles or the **sysadmin** fixed server role. A job can be edited only by its owner or members of the **sysadmin** role. Members of the **sysadmin** role can assign job ownership to other users, and they can run any job, regardless of the job owner. For more information about the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent fixed database roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
Jobs can be written to run on the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or on multiple instances across an enterprise. To run jobs on multiple servers, you must set up at least one master server and one or more target servers. For more information about master and target servers, see [Automated Administration Across an Enterprise](../../ssms/agent/automated-administration-across-an-enterprise.md)  
  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent records job and job step information in the job history.  
  
## Related Tasks  
  
|||  
|-|-|  
|**Description**|**Topic**|  
|Describes how to create a [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent job.|[Create a Job](../../ssms/agent/create-a-job.md)|  
|Describes how to reassign ownership of [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent jobs to another user.|[Give Others Ownership of a Job](../../ssms/agent/give-others-ownership-of-a-job.md)|  
|Describes how to set up the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent job history log.|[Set Up the Job History Log](../../ssms/agent/set-up-the-job-history-log.md)|  
  
## See Also  
[Manage Job Steps](../../ssms/agent/manage-job-steps.md)  
[Automated Administration Across an Enterprise](../../ssms/agent/automated-administration-across-an-enterprise.md)  
[Create and Attach Schedules to Jobs](../../ssms/agent/create-and-attach-schedules-to-jobs.md)  
[Run Jobs](../../ssms/agent/run-jobs.md)  
[View or Modify Jobs](../../ssms/agent/view-or-modify-jobs.md)  
  
