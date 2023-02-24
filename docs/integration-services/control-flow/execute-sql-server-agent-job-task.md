---
description: "Execute SQL Server Agent Job Task"
title: "Execute SQL Server Agent Job Task"
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.executesqlserveragentjobtask.f1"
helpviewer_keywords: 
  - "Execute SQL Server Agent Job task [Integration Services]"
  - "jobs [Integration Services]"
  - "SQL Server Agent [Integration Services]"
ms.assetid: 3aa3bc0e-1a1c-452e-81b8-b4e3422ea053
author: chugugrace
ms.author: chugu
ms.custom: "seo-lt-2019"
---
# Execute SQL Server Agent Job Task

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  The Execute [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent Job task runs [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows service that runs jobs that have been defined in an instance of SQL Server. You can create jobs that execute Transact-SQL statements and ActiveX scripts, perform [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and Replication maintenance tasks, or run packages. You can also configure a job to monitor [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and fire alerts. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs are typically used to automate tasks that you perform repeatedly. For more information, see [Implement Jobs](../../ssms/agent/implement-jobs.md).  
  
 By using the Execute [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent Job task, a package can perform administrative tasks related to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components. For example, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job can run a system stored procedure such as **sp_enum_dtspackages** to obtain a list of packages in a folder.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent must be running before local or multiserver administrative jobs can run automatically.  
  
 This task encapsulates the **sp_start_job** system procedure and passes the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job to the procedure as an argument. For more information, see [sp_start_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-start-job-transact-sql.md).  
  
## Configuring the Execute SQL Server Agent Job Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer. This task is in the **Maintenance Plan Tasks** section of the **Toolbox** in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Execute SQL Server Agent Job Task &#40;Maintenance Plan&#41;](../../relational-databases/maintenance-plans/execute-sql-server-agent-job-task-maintenance-plan.md)  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](./add-or-delete-a-task-or-a-container-in-a-control-flow.md)  
  
