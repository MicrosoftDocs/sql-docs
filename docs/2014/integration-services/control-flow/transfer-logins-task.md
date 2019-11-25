---
title: "Transfer Logins Task | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.transferloginstask.f1"
helpviewer_keywords: 
  - "Transfer Logins task [Integration Services]"
ms.assetid: 1df60fd6-c019-405d-8155-c330dbac2cc1
author: janinezhang
ms.author: janinez
manager: craigg
---
# Transfer Logins Task
  The Transfer Logins task transfers one or more logins between instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Transfer Logins Between Instances of SQL Server  
 The Transfer Logins task supports a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] source and destination.  
  
## Events  
 The task raises an information event that reports the number of logins transferred and a warning event when a login is overwritten.  
  
 The Transfer Logins task does not report incremental progress of the login transfer; it reports only 0% and 100 % completion.  
  
## Execution Value  
 The execution value, defined in the `ExecutionValue` property of the task, returns the number of logins transferred. By assigning a user-defined variable to the `ExecValueVariable` property of the Transfer Logins task, information about the login transfer can be made available to other objects in the package. For more information, see [Integration Services &#40;SSIS&#41; Variables](../integration-services-ssis-variables.md) and [Use Variables in Packages](../use-variables-in-packages.md).  
  
## Log Entries  
 The Transfer Logins task includes the following custom log entries:  
  
-   TransferLoginsTaskStarTransferringObjects    This log entry reports the transfer has started. The log entry includes the start time.  
  
-   TransferLoginsTaskFinishedTransferringObjects   This log entry reports the transfer has completed. The log entry includes the end time.  
  
 In addition, a log entry for the `OnInformation` event reports the number of logins that were transferred, and a log entry for the `OnWarning` event is written for each login on the destination that is overwritten.  
  
## Security and Permissions  
 To browse logins on the source server and to create logins on the destination server, the user must be a member of the sysadmin server role on both servers.  
  
## Configuration of the Transfer Logins Task  
 The Transfer Logins task can be configured to transfer all logins, only specified logins, or all logins that have access to specified databases only. The sa login cannot be transferred. The sa login may be renamed; however, the renamed sa login cannot be transferred either.  
  
 You can also indicate whether the task copies the security identifiers (SIDs) associated with the logins. If the Transfer Logins task is used in conjunction with the Transfer Database task the SIDs must be copied to the destination; otherwise, the transferred logins are not recognized by the destination database.  
  
 At the destination, the transferred logins are disabled and assigned random passwords. A member of the sysadmin role on the destination server must change the passwords and enable the logins before the logins can be used.  
  
 The logins to be transferred may already exist on the destination. The Transfer Logins task can be configured to handle existing logins in the following ways:  
  
-   Overwrite existing logins.  
  
-   Fail the task when duplicate logins exist.  
  
-   Skip duplicate logins.  
  
 At run time, the Transfer Logins task connects to the source and destination servers by using two SMO connection managers. The SMO connection managers are configured separately from the Transfer Logins task, and then referenced in the Transfer Logins task. The SMO connection managers specify the server and the authentication mode to use when accessing the server. For more information, see [SMO Connection Manager](../connection-manager/smo-connection-manager.md).  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [Transfer Logins Task Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)  
  
-   [Transfer Logins Task Editor &#40;Logins Page&#41;](../transfer-logins-task-editor-logins-page.md)  
  
-   [Expressions Page](../expressions/expressions-page.md)  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md)  
  
## Programmatic Configuration of the Transfer Logins Task  
 For more information about programmatically setting these properties, click the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.TransferLoginsTask.TransferLoginsTask>  
  
  
