---
title: "Message Queue Task Editor (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.msgqueuetask.general.f1"
helpviewer_keywords: 
  - "Message Queue Task Editor"
ms.assetid: 09368b18-37a5-4321-a173-7cfe5d42d2a2
author: janinezhang
ms.author: janinez
manager: craigg
---
# Message Queue Task Editor (General Page)
  Use the **General page** of the **Message Queue Task Editor** dialog box to name and describe the Message Queue task, to specify the message format, and to indicate whether the task sends or receives messages.  
  
 To learn about this task, see [Message Queue Task](control-flow/message-queue-task.md).  
  
## Options  
 **Name**  
 Provide a unique name for the Message Queue task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the Message Queue task.  
  
 **Use2000Format**  
 Indicate whether to use the 2000 format of Message Queuing (also known as MSMQ). The default is `False`.  
  
 **MSMQConnection**  
 Select an existing MSMQ connection manager or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics**: [MSMQ Connection Manager](connection-manager/msmq-connection-manager.md), [MSMQ Connection Manager Editor](../../2014/integration-services/msmq-connection-manager-editor.md)  
  
 **Message**  
 Specify whether the Message Queue task sends or receive messages. If you select **Send message**, the Send page is listed in the left pane of the dialog box; if you select **Receive message**, the Receive page is listed. By default, this value is set to **Send message**.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Message Queue Task Editor &#40;Receive Page&#41;](../../2014/integration-services/message-queue-task-editor-receive-page.md)   
 [Message Queue Task Editor &#40;Send Page&#41;](../../2014/integration-services/message-queue-task-editor-send-page.md)   
 [Expressions Page](expressions/expressions-page.md)  
  
  
