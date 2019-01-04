---
title: "Message Queue Task | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.messagequeuetask.f1"
helpviewer_keywords: 
  - "Message Queue task [Integration Services]"
  - "receiving messages"
  - "messages [Integration Services]"
  - "sending messages"
ms.assetid: ae1d8fad-6649-4e93-b589-14a32d07da33
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Message Queue Task
  The Message Queue task allows you to use Message Queuing (also known as MSMQ) to send and receive messages between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages, or to send messages to an application queue that is processed by a custom application. These messages can take the form of simple text, files, or variables and their values.  
  
 By using the Message Queue task, you can coordinate operations throughout your enterprise. Messages can be queued and delivered later if the destination is unavailable or busy; for example, the task can queue messages for the offline laptop computer of sales representatives, who receive their messages when they connect to the network. You can use the Message Queue task for the following purposes:  
  
-   Delaying task execution until other packages check in. For example, after nightly maintenance at each of your retail sites, a Message Queue task sends a message to your corporate computer. A package running on the corporate computer contains Message Queue tasks, each waiting for a message from a particular retail site. When a message from a site arrives, a task uploads data from that site. After all the sites have checked in, the package computes summary totals.  
  
-   Sending data files to the computer that processes them. For example, the output from a restaurant cash register can be sent in a data file message to the corporate payroll system, where data about each waiter's tips is extracted.  
  
-   Distributing files throughout your enterprise. For example, a package can use a Message Queue task to send a package file to another computer. A package running on the destination computer then uses a Message Queue task to retrieve and save the package locally.  
  
 When sending or receiving messages, the Message Queue task uses one of four message types: data file, string, string message to variable, or variable. The string message to variable message type can be used only when receiving messages.  
  
 The task uses an MSMQ connection manager to connect to a message queue. For more information, see [MSMQ Connection Manager](../connection-manager/msmq-connection-manager.md). For more information about Message Queuing, see the [MSDN Library](https://go.microsoft.com/fwlink/?LinkId=7022).  
  
 The Message Queue task requires that the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service be installed. Some [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components that you may select for installation on the **Components to Install** page or the **Feature Selection** page of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard install a partial subset of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] components. These components are useful for specific tasks, but the functionality of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] will be limited. For example, the [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] option installs the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] components required to design a package, but the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is not installed, and therefore the Message Queue task is not functional. To ensure a complete installation of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], you must select [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] on the **Components to Install** page. For more information about installing and running the Message Queue task, see [Install Integration Services](../install-windows/install-integration-services.md).  
  
> [!NOTE]  
>  The Message Queue task fails to comply with Federal Information Processing Standard (FIPS) 140-2 when the computer's operating system is configured in FIPS mode and the task uses encryption. If the Message Queue task does not use encryption, the task can run successfully.  
  
## Message Types  
 You can configure the message types that the Message Queue task provides in the following ways:  
  
-   `Data file` message specifies that a file contains the message. When receiving messages, you can configure the task to save the file, overwrite an existing file, and specify the package from which the task can receive messages.  
  
-   `String` message specifies the message as a string. When receiving messages, you can configure the task to compare the received string with a user-defined string and take action depending on the comparison. String comparison can be exact, case-sensitive or case-insensitive, or use a substring.  
  
-   `String message to variable` specifies the source message as a string that is sent to a destination variable. You can configure the task to compare the received string with a user-defined string using an exact, case-insensitive, or substring comparison. This message type is available only when the task is receiving messages.  
  
-   `Variable` specifies that the message contains one or more variables. You can configure the task to specify the names of the variables included in the message. When receiving messages, you can configure the task to specify both the package from which it can receive messages and the variable that is the destination of the message.  
  
## Sending Messages  
 When configuring the Message Queue task to send messages, you can use one of the encryption algorithms that are currently supported by the Message Queuing technology, RC2 and RC4, to encrypt the message. Both of these encryption algorithms are now considered cryptographically weak compared to newer algorithms, which Message Queuing technology do not yet support. Therefore, you should consider your cryptography needs carefully when sending messages using the Message Queue task.  
  
## Receiving Messages  
 When receiving messages, the Message Queue task can be configured in the following ways:  
  
-   Bypassing the message, or removing the message from the queue.  
  
-   Specifying a time-out.  
  
-   Failing if a time-out occurs.  
  
-   Overwriting an existing file, if the message is stored in a `Data file`.  
  
-   Saving the message file to a different file name, if the message uses the `Data file message` type.  
  
## Custom Logging Messages Available on the Message Queue Task  
 The following table lists the custom log entries for the Message Queue task. For more information, see [Integration Services &#40;SSIS&#41; Logging](../performance/integration-services-ssis-logging.md) and [Custom Messages for Logging](../custom-messages-for-logging.md).  
  
|Log entry|Description|  
|---------------|-----------------|  
|`MSMQAfterOpen`|Indicates that the task finished opening the message queue.|  
|`MSMQBeforeOpen`|Indicates that the task began to open the message queue.|  
|`MSMQBeginReceive`|Indicates that the task began to receive a message.|  
|`MSMQBeginSend`|Indicates that the task began to send a message.|  
|`MSMQEndReceive`|Indicates that the task finished receiving a message.|  
|`MSMQEndSend`|Indicates that the task finished sending a message.|  
|`MSMQTaskInfo`|Provides descriptive information about the task.|  
|`MSMQTaskTimeOut`|Indicates that the task timed out.|  
  
## Configuration of the Message Queue Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically. For information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [Message Queue Task Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)  
  
-   [Message Queue Task Editor &#40;Receive Page&#41;](../message-queue-task-editor-receive-page.md)  
  
-   [Message Queue Task Editor &#40;Send Page&#41;](../message-queue-task-editor-send-page.md)  
  
-   [Expressions Page](../expressions/expressions-page.md)  
  
 For information about programmatically setting these properties, see the documentation for the **Microsoft.SqlServer.Dts.Tasks.MessageQueueTask.MessageQueueTask** class in the Developer Guide.  
  
## Related Tasks  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md).  
  
## See Also  
 [Integration Services Tasks](integration-services-tasks.md)   
 [Control Flow](control-flow.md)  
  
  
