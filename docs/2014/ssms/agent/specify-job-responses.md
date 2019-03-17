---
title: "Specify Job Responses | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "jobs [SQL Server Agent], responses"
  - "SQL Server Agent jobs, responses"
  - "actions [SQL Server Agent jobs]"
  - "responding to jobs"
ms.assetid: 050242e1-9b79-4ade-91a9-580707b9d2d9
author: stevestein
ms.author: sstein
manager: craigg
---
# Specify Job Responses
  Job responses specify actions that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service will take after a job completes. Job responses ensure that database administrators know when jobs complete and how frequently they run. Typical job responses include:  
  
-   Notifying the operator by using e-mail, electronic paging, or a **net send** message.  
  
     Use one of these job responses if the operator must perform a follow-up action. For example, if a backup job completes successfully, the operator must be notified to remove the backup tape and store it in a safe location.  
  
-   Writing an event message to the Windows application log.  
  
     You can use this response only for failed jobs.  
  
-   Automatically deleting the job.  
  
     Use this job response if you are certain that you do not need to rerun this job.  
  
## Related Tasks  
  
|||  
|-|-|  
|**Description**|**Topic**|  
|Describes how to notify an operator of job status.|[Notify an Operator of Job Status](notify-an-operator-of-job-status.md)|  
|Describes how to write job status to the Windows application log.|[Write the Job Status to the Windows Application Log](../../reporting-services/report-server/windows-application-log.md)|  
  
## See Also  
 [Monitor and Respond to Events](monitor-and-respond-to-events.md)  
  
  
