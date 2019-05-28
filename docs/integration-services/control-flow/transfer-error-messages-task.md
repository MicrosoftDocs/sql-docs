---
title: "Transfer Error Messages Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.transfererrormessagestask.f1"
  - "sql13.dts.designer.transfererrormessagestask.general.f1"
  - "sql13.dts.designer.transfererrormessagestask.errormessages.F1"
helpviewer_keywords: 
  - "Transfer Error Messages task [Integration Services]"
ms.assetid: da702289-035a-4d14-bd74-04461fbfee1b
author: janinezhang
ms.author: janinez
manager: craigg
---
# Transfer Error Messages Task

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The Transfer Error Messages task transfers one or more [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user-defined error messages between instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. User-defined messages are messages with an identifier that is equal to or greater than 50000. Messages with an identifier less than 50000 are system error messages, and cannot be transferred by using the Transfer Error Messages task.  
  
 The Transfer Error Messages task can be configured to transfer all error messages, or only the specified error messages. User-defined error messages may be available in a number of different languages and the task can be configured to transfer only messages in selected languages. A us_english version of the message that uses code page 1033 must exist on the destination server before you can transfer other language versions of the message to that server.  
  
 The sysmessages table in the master database contains all the error messages-both system and user-defined-that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses.  
  
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
 The execution value, defined in the **ExecutionValue** property of the task, returns the number of error messages that have been transferred. By assigning a user-defined variable to the **ExecValueVariable** property of the Transfer Error Message task, information about the error message transfer can be made available to other objects in the package. For more information, see [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md) and [Use Variables in Packages](https://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787).  
  
## Log Entries  
 The Transfer Error Messages task includes the following custom log entries:  
  
-   TransferErrorMessagesTaskStartTransferringObjects    This log entry reports that the transfer has started. The log entry includes the start time.  
  
-   TransferErrorMessagesTaskFinishedTransferringObjects   This log entry reports that the transfer has finished. The log entry includes the end time.  
  
 In addition, a log entry for the **OnInformation** event reports the number of error messages that were transferred, and a log entry for the **OnWarning event** is written for each error message on the destination that is overwritten.  
  
## Security and Permissions  
 To create new error messages, the user that runs the package must be a member of the sysadmin or serveradmin server role on the destination server.  
  
## Configuration of the Transfer Error Messages Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
 For information about programmatically setting these properties, click the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.TransferErrorMessagesTask.TransferErrorMessagesTask>  
  
## Related Tasks  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b)  
  
## Transfer Error Messages Task Editor (General Page)
  Use the **General** page of the **Transfer Error Messages Task Editor** dialog box to name and describe the Transfer Error Messages task. The Transfer Error Messages task transfers one or more [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user-defined error messages between instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].   
  
### Options  
 **Name**  
 Type a unique name for the Transfer Error Messages task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the Transfer Error Messages task.  
  
## Transfer Error Messages Task Editor (Messages Page)
  Use the **Messages** page of the **Transfer Error Messages Task Editor** dialog box to specify properties for copying one or more [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user-defined error messages from one instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to another. 
  
### Options  
 **SourceConnection**  
 Select a SMO connection manager in the list, or click **\<New connection...>** to create a new connection to the source server.  
  
 **DestinationConnection**  
 Select a SMO connection manager in the list, or click **\<New connection...>** to create a new connection to the destination server.  
  
 **IfObjectExists**  
 Select whether the task should overwrite existing user-defined error messages, skip existing messages, or fail if error messages of the same name already exist on the destination server.  
  
 **TransferAllErrorMessages**  
 Select whether the task should copy all or only the specified user-defined messages from the source server to the destination server.  
  
 This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**True**|Copy all user-defined messages.|  
|**False**|Copy only the specified user-defined messages.|  
  
 **ErrorMessagesList**  
 Click the browse button **(...)** to select the error messages to copy.  
  
> [!NOTE]  
>  You must specify the **SourceConnection** before you can select error messages to copy.  
  
 **ErrorMessageLanguagesList**  
 Click the browse button **(...)** to select the languages for which to copy user-defined error messages to the destination server. A us_english (code page 1033) version of the message must exist on the destination server before you can transfer other language versions of the message to that server.  
  
> [!NOTE]  
>  You must specify the **SourceConnection** before you can select error messages to copy.  
  
## See Also  
 [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md)   
 [Control Flow](../../integration-services/control-flow/control-flow.md)  
  
  
