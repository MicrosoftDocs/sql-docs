---
title: "Manage Job Steps | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "job steps [SQL Server replication]"
  - "job steps [SQL Server Agent]"
  - "jobs [SQL Server Agent], Integration Services package step"
  - "executable programs as job steps"
  - "operating systems [SQL Server], job steps"
  - "Transact-SQL job step"
  - "job steps [Transact-SQL]"
  - "Integration Services packages, job steps"
  - "replication job steps [SQL Server]"
  - "logs [SQL Server], jobs"
  - "SQL Server Agent jobs, job steps"
  - "ActiveX scripting jobs [SQL Server]"
  - "job steps [Analysis Services]"
ms.assetid: 51352afc-a0a4-428b-8985-f9e58bb57c31
author: stevestein
ms.author: sstein
manager: craigg
---
# Manage Job Steps
  A job step is an action that the job takes on a database or a server. Every job must have at least one job step. Job steps can be:  
  
-   Executable programs and operating system commands.  
  
-   [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, including stored procedures and extended stored procedures.  
  
-   PowerShell scripts.  
  
-   [!INCLUDE[msCoName](../../includes/msconame-md.md)] ActiveX scripts.  
  
-   Replication tasks.  
  
-   [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] tasks.  
  
-   [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages.  
  
 Every job step runs in a specific security context. If the job step specifies a proxy, the job step runs in the security context of the credential for the proxy. If a job step does not specify a proxy, the job step runs in the context of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account. Only members of the sysadmin fixed server role can create jobs that do not explicitly specify a proxy.  
  
 Because job steps run in the context of a specific [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows user, that user must have the permissions and configuration necessary for the job step to execute. For example, if you create a job that requires a drive letter or a Universal Naming Convention (UNC) path, the job steps may run under your Windows user account while testing the tasks. However, the Windows user for the job step must also have the necessary permissions, drive letter configurations, or access to the required drive. Otherwise, the job step fails. To prevent this problem, ensure that the proxy for each job step has the necessary permissions for the task that the job step performs. For more information, see [Security Center for SQL Server Database Engine and Azure SQL Database](../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md).  
  
## Job Step Logs  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent can write output from some job steps either to an operating system file or to the sysjobstepslogs table in the msdb database. The following job step types can write output to both destinations:  
  
-   Executable programs and operating system commands.  
  
-   [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
-   [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] tasks.  
  
 Only job steps that are executed by users who are members of the sysadmin fixed server role can write job step output to operating system files. If job steps are executed by users who are members of the SQLAgentUserRole, SQLAgentReaderRole, or the SQLAgentOperatorRole fixed database roles in the msdb database, then the output from these job steps can be written only to the sysjobstepslogs table.  
  
 Job step logs are automatically deleted when jobs or job steps are deleted.  
  
> [!NOTE]  
>  Replication task and [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package job step logging is handled by their respective subsystem. You cannot use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to configure jog step logging for these types of job steps.  
  
## Executable Programs and Operating-System Commands As Job Steps  
 Executable programs and operating-system commands can be used as job steps. These files may have .bat, .cmd, .com, or .exe file extensions.  
  
 When you use an executable program or an operating-system command as a job step, you must specify:  
  
-   The process exit code returned if the command was successful.  
  
-   The command to execute. To execute an operating system command, this is simply the command itself. For an external program, this is the name of the program and the arguments to the program, for example: **C:\Program Files\Microsoft SQL Server\100\Tools\Binn\sqlcmd.exe -e -q "sp_who"**  
  
    > [!NOTE]  
    >  You must provide the full path to the executable if the executable is not located in a directory specified in the system path or the path for the user that the job step runs as.  
  
## Transact-SQL Job Steps  
 When you create a [!INCLUDE[tsql](../../includes/tsql-md.md)] job step, you must:  
  
-   Identify the database in which to run the job.  
  
-   Type the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement to execute. The statement may call a stored procedure or an extended stored procedure.  
  
 Optionally, you can open an existing [!INCLUDE[tsql](../../includes/tsql-md.md)] file as the command for the job step.  
  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] job steps do not use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxies. Instead, the job step runs as the owner of the job step, or as the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account if the owner of the job step is a member of the sysadmin fixed server role. Members of the sysadmin fixed server role can also specify that [!INCLUDE[tsql](../../includes/tsql-md.md)] job steps run under the context of another user by using the *database_user_name* parameter of the sp_add_jobstep stored procedure. For more information, see [sp_add_jobstep &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-add-jobstep-transact-sql).  
  
> [!NOTE]  
>  A single [!INCLUDE[tsql](../../includes/tsql-md.md)] job step can contain multiple batches. [!INCLUDE[tsql](../../includes/tsql-md.md)] job steps can contain embedded GO commands.  
  
## PowerShell Scripting Job Steps  
 When you create a PowerShell script job step, you must specify one of two things as the command for the step:  
  
-   The text of a PowerShell script.  
  
-   An existing PowerShell script file to be opened.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent PowerShell subsystem opens a PowerShell session and loads the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell snap-ins. The PowerShell script used as the job step command can reference the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell provider and cmdlets. For more information about writing PowerShell scripts using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell snap-ins, see the [SQL Server PowerShell](../../powershell/sql-server-powershell.md).  
  
## ActiveX Scripting Job Steps  
  
> [!IMPORTANT]  
>  The ActiveX scripting job step will be removed from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent in a future version of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.  
  
 When you create an ActiveX scripting job step, you must:  
  
-   Identify the scripting language in which the job step is written.  
  
-   Write the ActiveX script.  
  
 You can also open an existing ActiveX script file as the command for the job step. Alternatively, ActiveX script commands can be externally compiled (for example, using Microsoft Visual Basic) and then run as executable programs.  
  
 When a job step command is an ActiveX script, you can use the SQLActiveScriptHost object to print output to the job step history log or create COM objects. SQLActiveScriptHost is a global object that is introduced by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent hosting system into the script name space. The object has two methods (Print and CreateObject). The following example shows how ActiveX scripting works in Visual Basic Scripting Edition (VBScript).  
  
```  
' VBScript example for ActiveX Scripting job step  
' Create a Dmo.Server object. The object connects to the  
' server on which the script is running.  
  
Set oServer = CreateObject("SQLDmo.SqlServer")  
oServer.LoginSecure = True  
oServer.Connect "(local)"  
'Disconnect and destroy the server object  
oServer.DisConnect  
Set oServer = nothing  
  
```  
  
## Replication Job Steps  
 When you create publications and subscriptions using replication, replication jobs are created by default. The type of job created is determined by the type of replication (snapshot, transactional, or merge) and the options used.  
  
 Replication job steps activate one of these replication agents:  
  
-   Snapshot Agent (Snapshot job)  
  
-   Log Reader Agent (LogReader job)  
  
-   Distribution Agent (Distribution job)  
  
-   Merge Agent (Merge job)  
  
-   Queue Reader Agent (QueueReader job)  
  
 When replication is set up, you can specify to run the replication agents in one of three ways: continuously after [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is started, on demand, or according to a schedule. For more information about replication agents, see [Replication Agents Overview](../../relational-databases/replication/agents/replication-agents-overview.md).  
  
## Analysis Services Job Steps  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent supports two distinct types of Analysis Services job steps, command job steps, and query job steps.  
  
### Analysis Services Command Job Steps  
 When you create an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] command job step, you must:  
  
-   Identify the database OLAP server in which to run the job step.  
  
-   Type the statement to execute. The statement must be an XML for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] **Execute** method. The statement may not contain a complete SOAP envelope or an XML for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] **Discover** method. Notice that, while [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] supports complete SOAP envelopes and the **Discover** method, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job steps do not.  
  
### Analysis Services Query Job Steps  
 When you create an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] query job step, you must:  
  
-   Identify the database OLAP server in which to run the job step.  
  
-   Type the statement to execute. The statement must be a multidimensional expressions (MDX) query.  
  
 For more information on MDX, see [MDX Query Fundamentals &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/mdx/mdx-query-fundamentals-analysis-services.md).  
  
## Integration Services Packages  
 When you create an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package job step, you must do the following:  
  
-   Identify the source of the package.  
  
-   Identify the location of the package.  
  
-   If configuration files are required for the package, identify the configuration files.  
  
-   If command files are required for the package, identify the command files.  
  
-   Identify the verification to use for the package. For example, you can specify that the package must be signed, or that the package must have a specific package ID.  
  
-   Identify the data sources for the package.  
  
-   Identify the log providers for the package.  
  
-   Specify variables and values to set before running the package.  
  
-   Identify execution options.  
  
-   Add or modify command-line options.  
  
 Note that if you deployed the package to the SSIS Catalog and you specify **SSIS Catalog** as the package source, much of this configuration information is obtained automatically from the package. Under the **Configuration** tab you can specify the environment, parameter values, connection manager values, property overrides, and whether the package runs in a 32-bit runtime environment.  
  
 For more information about creating job steps that run [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages, see [SQL Server Agent Jobs for Packages](../../integration-services/packages/sql-server-agent-jobs-for-packages.md).  
  
## Related Tasks  
  
|||  
|-|-|  
|**Description**|**Topic**|  
|Describes how to create a job step with an executable program.|[Create a CmdExec Job Step](create-a-cmdexec-job-step.md)|  
|Describes how to reset [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent permissions.|[Configure a User to Create and Manage SQL Server Agent Jobs](configure-a-user-to-create-and-manage-sql-server-agent-jobs.md)|  
|Describes how to create a [!INCLUDE[tsql](../../includes/tsql-md.md)] job step.|[Create a Transact-SQL Job Step](create-a-transact-sql-job-step.md)|  
|Describes how to define options for Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent Transact-SQL job steps.|[Define Transact-SQL Job Step Options](define-transact-sql-job-step-options.md)|  
|Describes how to create an ActiveX script job step.|[Create an ActiveX Script Job Step](create-an-activex-script-job-step.md)|  
|Describes how to create and define [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job steps that execute [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Analysis Services commands and queries.|[Create an Analysis Services Job Step](create-an-analysis-services-job-step.md)|  
|Describes what action [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should take if a failure occurs during job execution.|[Set Job Step Success or Failure Flow](set-job-step-success-or-failure-flow.md)|  
|Describes how to view job step details in the Job Step Properties dialog.|[View Job Step Information](view-job-step-information.md)|  
|Describes how to delete a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job step log.|[Delete a Job Step Log](delete-a-job-step-log.md)|  
  
## See Also  
 [dbo.sysjobstepslogs &#40;Transact-SQL&#41;](/sql/relational-databases/system-tables/dbo-sysjobstepslogs-transact-sql)   
 [Create Jobs](create-jobs.md)   
 [sp_add_job &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-add-job-transact-sql)  
  
  
