---
title: "Message Queue Task Editor (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.msgqueuetask.general.f1"
helpviewer_keywords: 
  - "Message Queue Task Editor"
ms.assetid: 09368b18-37a5-4321-a173-7cfe5d42d2a2
caps.latest.revision: 25
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Message Queue Task Editor (General Page)
  Use the **General page** of the **Message Queue Task Editor** dialog box to name and describe the Message Queue task, to specify the message format, and to indicate whether the task sends or receives messages.  
  
 To learn about this task, see [Message Queue Task](../../integration-services/control-flow/message-queue-task.md).  
  
## Options  
 **Name**  
 Provide a unique name for the Message Queue task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the Message Queue task.  
  
 **Use2000Format**  
 Indicate whether to use the 2000 format of Message Queuing (also known as MSMQ). The default is **False**.  
  
 **MSMQConnection**  
 Select an existing MSMQ connection manager or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics**: [MSMQ Connection Manager](../../integration-services/connection-manager/msmq-connection-manager.md), [MSMQ Connection Manager Editor](../../integration-services/connection-manager/msmq-connection-manager-editor.md)  
  
 **Message**  
 Specify whether the Message Queue task sends or receive messages. If you select **Send message**, the Send page is listed in the left pane of the dialog box; if you select **Receive message**, the Receive page is listed. By default, this value is set to **Send message**.  
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)   
 [Message Queue Task Editor &#40;Receive Page&#41;](../../integration-services/control-flow/message-queue-task-editor-receive-page.md)   
 [Message Queue Task Editor &#40;Send Page&#41;](../../integration-services/control-flow/message-queue-task-editor-send-page.md)   
 [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
  