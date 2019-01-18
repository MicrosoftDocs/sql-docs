---
title: "Transfer Jobs Task | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.transferjobstask.f1"
helpviewer_keywords: 
  - "Transfer Jobs task [Integration Services]"
ms.assetid: 1bf33885-9c5b-47e4-a549-f5920b66a1de
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Transfer Jobs Task
  The Transfer Jobs task transfers one or more [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs between instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 The Transfer Jobs task can be configured to transfer all jobs, or only specified jobs. You can also indicate whether the transferred jobs are enabled at the destination.  
  
 The jobs to be transferred may already exist on the destination. The Transfer Jobs task can be configured to handle existing jobs in the following ways:  
  
-   Overwrite existing jobs.  
  
-   Fail the task when duplicate jobs exist.  
  
-   Skip duplicate jobs.  
  
 At run time, the Transfer Jobs task connects to the source and destination servers by using one or two SMO connection managers. The SMO connection manager is configured separately from the Transfer Jobs task, and then is referenced in the Transfer Jobs task. The SMO connection manager specifies the server and the authentication mode to use when accessing the server. For more information, see [SMO Connection Manager](../connection-manager/smo-connection-manager.md).  
  
## Transferring Jobs Between Instances of SQL Server  
 The Transfer Jobs task supports a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] source and destination. There are no restrictions on which version to use as a source or destination.  
  
## Events  
 The Transfer Jobs task raises an information event that reports the number of jobs transferred and a warning event when a job is overwritten. The task does not report incremental progress of the job transfer; it reports only 0% and 100% completion.  
  
## Execution Value  
 The execution value, defined in the `ExecutionValue` property of the task, returns the number of jobs that are transferred. By assigning a user-defined variable to the `ExecValueVariable` property of the Transfer Jobs task, information about the job transfer can be made available to other objects in the package. For more information, see [Integration Services &#40;SSIS&#41; Variables](../integration-services-ssis-variables.md) and [Use Variables in Packages](../use-variables-in-packages.md).  
  
## Log Entries  
 The Transfer Jobs task includes the following custom log entries:  
  
-   TransferJobsTaskStarTransferringObjects   This log entry reports that the transfer has started. The log entry includes the start time.  
  
-   TransferJobsTaskFinishedTransferringObjects    This log entry reports that the transfer has finished. The log entry includes the end time.  
  
 In addition, a log entry for the `OnInformation` event reports the number of jobs that were transferred and a log entry for the `OnWarning` event is written for each job on the destination that is overwritten.  
  
## Security and Permissions  
 To transfer jobs, the user must be a member of the sysadmin fixed server role or one of the fixed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles on the msdb database on the both the source and destination instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Configuration of the Transfer Jobs Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [Transfer Jobs Task Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)  
  
-   [Transfer Jobs Task Editor &#40;Jobs Page&#41;](../transfer-jobs-task-editor-jobs-page.md)  
  
-   [Expressions Page](../expressions/expressions-page.md)  
  
 For information about programmatically setting these properties, click the of the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.TransferJobsTask.TransferJobsTask>  
  
## Related Tasks  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md)  
  
## See Also  
 [Integration Services Tasks](integration-services-tasks.md)   
 [Control Flow](control-flow.md)  
  
  
