---
title: "MSMQ Connection Manager Editor | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.msmqconnectionmanager.f1"
helpviewer_keywords: 
  - "MSMQ Connection Manager Editor"
ms.assetid: ef842cb4-82da-4550-85fe-9bedbc1e77c7
caps.latest.revision: 29
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# MSMQ Connection Manager Editor
  Use the **MSMQ Connection Manager** dialog box to specify the path to a Message Queuing (also known as MSMQ) message queue.  
  
 To learn more about the MSMQ connection manager, see [MSMQ Connection Manager](../../integration-services/connection-manager/msmq-connection-manager.md).  
  
> [!NOTE]  
>  The MSMQ connection manager supports local public and private queues and remote public queues. It does not support remote private queues. For a workaround that uses the Script Task, see [Sending to a Remote Private Message Queue with the Script Task](../../integration-services/extending-packages-scripting-task-examples/sending-to-a-remote-private-message-queue-with-the-script-task.md).  
  
## Options  
 **Name**  
 Provide a unique name for the MSMQ connection manager in the workflow. The name provided will be displayed within [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 **Description**  
 Describe the connection manager. As a best practice, describe the connection manager in terms of its purpose, to make packages self-documenting and easier to maintain.  
  
 **Path**  
 Type the complete path of the message queue. The format of the path depends on the type of queue.  
  
|Queue type|Sample path|  
|----------------|-----------------|  
|Public|\<computer name>\\<queue name\>|  
|Private|\<computer name>\Private$\\<queue name\>|  
  
 You can use "." to represent the local computer.  
  
 **Test**  
 After configuring the MSMQ connection manager, confirm that the connection is viable by clicking **Test**.  
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)  
  
  