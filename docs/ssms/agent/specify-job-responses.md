---
description: "Specify Job Responses"
title: "Specify Job Responses"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "jobs [SQL Server Agent], responses"
  - "SQL Server Agent jobs, responses"
  - "actions [SQL Server Agent jobs]"
  - "responding to jobs"
ms.assetid: 050242e1-9b79-4ade-91a9-580707b9d2d9
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Specify Job Responses
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Job responses specify actions that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service will take after a job completes. Job responses ensure that database administrators know when jobs complete and how frequently they run. Typical job responses include:  
  
-   Notifying the operator by using e-mail, electronic paging, or a **net send** message.  
  
    Use one of these job responses if the operator must perform a follow-up action. For example, if a backup job completes successfully, the operator must be notified to remove the backup tape and store it in a safe location.  
  
-   Writing an event message to the Windows application log.  
  
    You can use this response only for failed jobs.  
  
-   Automatically deleting the job.  
  
    Use this job response if you are certain that you do not need to rerun this job.  
  
## Related Tasks  
  
|Description|Topic|  
|-|-|  
|Describes how to notify an operator of job status.|[Notify an Operator of Job Status](../../ssms/agent/notify-an-operator-of-job-status.md)|  
|Describes how to write job status to the Windows application log.|[Write the Job Status to the Windows Application Log](../../ssms/agent/write-the-job-status-to-the-windows-application-log.md)|  
  
## See Also  
[Monitor and Respond to Events](../../ssms/agent/monitor-and-respond-to-events.md)  
