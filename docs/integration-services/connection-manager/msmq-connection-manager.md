---
title: "MSMQ Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.msmqconnectionmanager.f1"
helpviewer_keywords: 
  - "connections [Integration Services], message queues"
  - "connection managers [Integration Services], MSMQ"
  - "MSMQ connection manager"
  - "message queue connections [Integration Services]"
ms.assetid: a86900e2-450e-479f-b207-e1b02361d395
author: janinezhang
ms.author: janinez
manager: craigg
---
# MSMQ Connection Manager
  An MSMQ connection manager enables a package to connect to a message queue that uses Message Queuing (also known as MSMQ). The Message Queue task that [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes uses an MSMQ connection manager.  
  
 When you add an MSMQ connection manager to a package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that will resolve to an MSMQ connection at run time, sets the connection manager properties, and adds the connection manager to the **Connections** collection on the package. The **ConnectionManagerType** property of the connection manager is set to **MSMQ**.  
  
 You can configure an MSMQ connection manager in the following ways:  
  
-   Provide a connection string.  
  
-   Provide the path of the message queue to connect to.  
  
 The format of the path depends on the type of queue, as shown in the following table.  
  
|Queue type|Sample path|  
|----------------|-----------------|  
|Public|\<computer name>\\<queue name\>|  
|Private|\<computer name>\Private$\\<queue name\>|  
  
 You can use a period (.) to represent the local computer.  
  
## Configuration of the MSMQ Connection Manager  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [MSMQ Connection Manager Editor](../../integration-services/connection-manager/msmq-connection-manager-editor.md).  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../../integration-services/building-packages-programmatically/adding-connections-programmatically.md).  
  
## MSMQ Connection Manager Editor
  Use the **MSMQ Connection Manager** dialog box to specify the path to a Message Queuing (also known as MSMQ) message queue.  
  
 To learn more about the MSMQ connection manager, see [MSMQ Connection Manager](../../integration-services/connection-manager/msmq-connection-manager.md).  
  
> [!NOTE]  
>  The MSMQ connection manager supports local public and private queues and remote public queues. It does not support remote private queues. For a workaround that uses the Script Task, see [Sending to a Remote Private Message Queue with the Script Task](../../integration-services/extending-packages-scripting-task-examples/sending-to-a-remote-private-message-queue-with-the-script-task.md).  
  
### Options  
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
 [Message Queue Task](../../integration-services/control-flow/message-queue-task.md)   
 [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md)  
  
  
