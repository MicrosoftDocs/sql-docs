---
title: "Transfer Jobs Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.transferjobstask.f1"
  - "sql13.dts.designer.transferjobstask.general.f1"
  - "sql13.dts.designer.transferjobstask.jobs.f1"
helpviewer_keywords: 
  - "Transfer Jobs task [Integration Services]"
ms.assetid: 1bf33885-9c5b-47e4-a549-f5920b66a1de
author: janinezhang
ms.author: janinez
manager: craigg
---
# Transfer Jobs Task
  The Transfer Jobs task transfers one or more [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs between instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 The Transfer Jobs task can be configured to transfer all jobs, or only specified jobs. You can also indicate whether the transferred jobs are enabled at the destination.  
  
 The jobs to be transferred may already exist on the destination. The Transfer Jobs task can be configured to handle existing jobs in the following ways:  
  
-   Overwrite existing jobs.  
  
-   Fail the task when duplicate jobs exist.  
  
-   Skip duplicate jobs.  
  
 At run time, the Transfer Jobs task connects to the source and destination servers by using one or two SMO connection managers. The SMO connection manager is configured separately from the Transfer Jobs task, and then is referenced in the Transfer Jobs task. The SMO connection manager specifies the server and the authentication mode to use when accessing the server. For more information, see [SMO Connection Manager](../../integration-services/connection-manager/smo-connection-manager.md).  
  
## Transferring Jobs Between Instances of SQL Server  
 The Transfer Jobs task supports a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] source and destination. There are no restrictions on which version to use as a source or destination.  
  
## Events  
 The Transfer Jobs task raises an information event that reports the number of jobs transferred and a warning event when a job is overwritten. The task does not report incremental progress of the job transfer; it reports only 0% and 100% completion.  
  
## Execution Value  
 The execution value, defined in the **ExecutionValue** property of the task, returns the number of jobs that are transferred. By assigning a user-defined variable to the **ExecValueVariable** property of the Transfer Jobs task, information about the job transfer can be made available to other objects in the package. For more information, see [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md) and [Use Variables in Packages](https://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787).  
  
## Log Entries  
 The Transfer Jobs task includes the following custom log entries:  
  
-   TransferJobsTaskStarTransferringObjects   This log entry reports that the transfer has started. The log entry includes the start time.  
  
-   TransferJobsTaskFinishedTransferringObjects    This log entry reports that the transfer has finished. The log entry includes the end time.  
  
 In addition, a log entry for the **OnInformation** event reports the number of jobs that were transferred and a log entry for the **OnWarning** event is written for each job on the destination that is overwritten.  
  
## Security and Permissions  
 To transfer jobs, the user must be a member of the sysadmin fixed server role or one of the fixed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles on the msdb database on the both the source and destination instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Configuration of the Transfer Jobs Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
 For information about programmatically setting these properties, click the of the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.TransferJobsTask.TransferJobsTask>  
  
## Related Tasks  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b)  
  
## Transfer Jobs Task Editor (General Page)
  Use the **General** page of the **Transfer Jobs Task Editor** dialog box to name and describe the Transfer Jobs task.  
  
> [!NOTE]  
>  Only members of the **sysadmin** fixed server role or one of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles on the destination server can successfully create jobs there. To access jobs on the source server, users must be a member of at least the **SQLAgentUserRole** fixed database role there. For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles and their permissions, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
### Options  
 **Name**  
 Type a unique name for the Transfer Jobs task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the Transfer Jobs task.  
  
## Transfer Jobs Task Editor (Jobs Page)
  Use the **Jobs** page of the **Transfer Jobs Task Editor** dialog box to specify properties for copying one or more [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs from one instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to another.  
  
> [!NOTE]  
>  To access jobs on the source server, users must be a member of at least the **SQLAgentUserRole** fixed database role on the server. To successfully create jobs on the destination server, the user must be a member of the **sysadmin** fixed server role or one of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles. For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles and their permissions, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
### Options  
 **SourceConnection**  
 Select a SMO connection manager in the list, or click **\<New connection...>** to create a new connection to the source server.  
  
 **DestinationConnection**  
 Select a SMO connection manager in the list, or click **\<New connection...>** to create a new connection to the destination server.  
  
 **TransferAllJobs**  
 Select whether the task should copy all or only the specified [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs from the source to the destination server.  
  
 This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**True**|Copy all jobs.|  
|**False**|Copy only the specified jobs.|  
  
 **JobsList**  
 Click the browse button **(...)** to select the jobs to copy. At least one job must be selected.  
  
> [!NOTE]  
>  Specify the **SourceConnection** before selecting jobs to copy.  
  
 The **JobsList** option is unavailable when **TransferAllJobs** is set to **True**.  
  
 **IfObjectExists**  
 Select how the task should handle jobs of the same name that already exist on the destination server.  
  
 This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**FailTask**|Task fails if jobs of the same name already exist on the destination server.|  
|**Overwrite**|Task overwrites jobs of the same name on the destination server.|  
|**Skip**|Task skips jobs of the same name that exist on the destination server.|  
  
 **EnableJobsAtDestination**  
 Select whether the jobs copied to the destination server should be enabled.  
  
 This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**True**|Enable jobs on destination server.|  
|**False**|Disable jobs on destination server.|  
  
## See Also  
 [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md)   
 [Control Flow](../../integration-services/control-flow/control-flow.md)  
  
  
