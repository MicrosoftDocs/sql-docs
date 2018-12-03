---
title: "Transfer Master Stored Procedures Task | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.transfermasterspstask.f1"
helpviewer_keywords: 
  - "Transfer Master Stored Procedures task [Integration Services]"
ms.assetid: 81702560-48a3-46d1-a469-e41304c7af8e
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Transfer Master Stored Procedures Task
  The Transfer Master Stored Procedures task transfers one or more user-defined stored procedures between **master** databases on instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To transfer a stored procedure from the **master** database, the owner of the procedure must be dbo.  
  
 The Transfer Master Stored Procedures task can be configured to transfer all stored procedures or only specified stored procedures. This task does not copy system stored procedures.  
  
 The master stored procedures to be transferred may already exist on the destination. The Transfer Master Stored Procedures task can be configured to handle existing stored procedures in the following ways:  
  
-   Overwrite existing stored procedures.  
  
-   Fail the task when duplicate stored procedures exist.  
  
-   Skip duplicate stored procedures.  
  
 At run time, the Transfer Master Stored Procedures task connects to the source and destination servers by using two SMO connection managers. The SMO connection managers are configured separately from the Transfer Master Stored Procedures task, and then referenced in the Transfer Master Stored Procedures task. The SMO connection managers specify the server and the authentication mode to use when accessing the server. For more information, see [SMO Connection Manager](../connection-manager/smo-connection-manager.md).  
  
## Transferring Stored Procedures Between Instances of SQL Server  
 The Transfer Master Stored Procedures task supports a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] source and destination.  
  
## Events  
 The task raises an information event that reports the number of stored procedures transferred and a warning event when a stored procedure is overwritten.  
  
 The Transfer Master Stored Procedures task does not report incremental progress of the login transfer; it reports only 0% and 100 % completion.  
  
## Execution Value  
 The execution value, defined in the `ExecutionValue` property of the task, returns the number of stored procedures transferred. By assigning a user-defined variable to the `ExecValueVariable` property of the Transfer Master Stored Procedures task, information about the stored procedure transfer can be made available to other objects in the package. For more information, see [Integration Services &#40;SSIS&#41; Variables](../integration-services-ssis-variables.md) and [Use Variables in Packages](../use-variables-in-packages.md).  
  
## Log Entries  
 The Transfer Master Stored Procedures task includes the following custom log entries:  
  
-   TransferStoredProceduresTaskStartTransferringObjects  This log entry reports that the transfer has started. The log entry includes the start time.  
  
-   TransferSStoredProceduresTaskFinishedTransferringObjects  This log entry reports that the transfer has finished. The log entry includes the end time.  
  
 In addition, a log entry for the `OnInformation` event reports the number of stored procedures that were transferred, and a log entry for the `OnWarning` event is written for each stored procedure on the destination that is overwritten.  
  
## Security and Permissions  
 The user must have permission to view the list of stored procedure in the **master** database on the source, and must be a member of the sysadmin server role or have permission to created stored procedures in the **master** database on the destination server.  
  
## Configuration of the Transfer Master Stored Procedures Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [Transfer Master Stored Procedures Task Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)  
  
-   [Transfer Master Stored Procedures Task Editor &#40;Stored Procedures Page&#41;](../transfer-master-stored-procedures-task-editor-stored-procedures-page.md)  
  
-   [Expressions Page](../expressions/expressions-page.md)  
  
 For information about programmatically setting these properties, click the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.TransferStoredProceduresTask.TransferStoredProceduresTask>  
  
### Configuring the Transfer Master Stored Procedures Task Programmatically  
  
## Related Tasks  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md)  
  
## See Also  
 [Transfer SQL Server Objects Task](transfer-sql-server-objects-task.md)   
 [Integration Services Tasks](integration-services-tasks.md)   
 [Control Flow](control-flow.md)  
  
  
