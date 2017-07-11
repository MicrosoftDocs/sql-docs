---
title: "Transfer Error Messages Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.transfererrormessagestask.f1"
helpviewer_keywords: 
  - "Transfer Error Messages task [Integration Services]"
ms.assetid: da702289-035a-4d14-bd74-04461fbfee1b
caps.latest.revision: 31
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Transfer Error Messages Task
  The Transfer Error Messages task transfers one or more [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user-defined error messages between instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. User-defined messages are messages with an identifier that is equal to or greater than 50000. Messages with an identifier less than 50000 are system error messages, and cannot be transferred by using the Transfer Error Messages task.  
  
 The Transfer Error Messages task can be configured to transfer all error messages, or only the specified error messages. User-defined error messages may be available in a number of different languages and the task can be configured to transfer only messages in selected languages. A us_english version of the message that uses code page 1033 must exist on the destination server before you can transfer other language versions of the message to that server.  
  
 The sysmessages table in the master database contains all the error messages—both system and user-defined—that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses.  
  
 The user-defined messages to be transferred may already exist on the destination. An error message is defined as a duplicate error message if the identifier and the language are the same. The Transfer Error Messages task can be configured to handle existing error messages in the following ways:  
  
-   Overwrite existing error messages.  
  
-   Fail the task when duplicate messages exist.  
  
-   Skip duplicate error messages.  
  
 At run time, the Transfer Error Messages task connects to the source and destination servers by using one or two SMO connection managers. The SMO connection manager is configured separately from the Transfer Error Messages task, and then is referenced in the Transfer Error Messages task. The SMO connection manager specifies the server and the authentication mode to use when accessing the server. For more information, see [SMO Connection Manager](../../integration-services/connection-manager/smo-connection-manager.md).  
  
 The Transfer Error Messages task supports a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] source and destination. There are no restrictions on which version to use as a source or destination.  
  
## Events  
 The task raises an information event that reports the number of error messages that have been transferred.  
  
 The Transfer Error Messages task does not report incremental progress of the error message transfer; it reports only 0% and 100 % completion.  
  
## Execution Value  
 The execution value, defined in the **ExecutionValue** property of the task, returns the number of error messages that have been transferred. By assigning a user-defined variable to the **ExecValueVariable** property of the Transfer Error Message task, information about the error message transfer can be made available to other objects in the package. For more information, see [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md) and [Use Variables in Packages](http://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787).  
  
## Log Entries  
 The Transfer Error Messages task includes the following custom log entries:  
  
-   TransferErrorMessagesTaskStartTransferringObjects    This log entry reports that the transfer has started. The log entry includes the start time.  
  
-   TransferErrorMessagesTaskFinishedTransferringObjects   This log entry reports that the transfer has finished. The log entry includes the end time.  
  
 In addition, a log entry for the **OnInformation** event reports the number of error messages that were transferred, and a log entry for the **OnWarning event** is written for each error message on the destination that is overwritten.  
  
## Security and Permissions  
 To create new error messages, the user that runs the package must be a member of the sysadmin or serveradmin server role on the destination server.  
  
## Configuration of the Transfer Error Messages Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [Transfer Error Messages Task Editor &#40;General Page&#41;](../../integration-services/control-flow/transfer-error-messages-task-editor-general-page.md)  
  
-   [Transfer Error Messages Task Editor &#40;Messages Page&#41;](../../integration-services/control-flow/transfer-error-messages-task-editor-messages-page.md)  
  
-   [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
 For information about programmatically setting these properties, click the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.TransferErrorMessagesTask.TransferErrorMessagesTask>  
  
## Related Tasks  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](http://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b)  
  
## See Also  
 [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md)   
 [Control Flow](../../integration-services/control-flow/control-flow.md)  
  
  