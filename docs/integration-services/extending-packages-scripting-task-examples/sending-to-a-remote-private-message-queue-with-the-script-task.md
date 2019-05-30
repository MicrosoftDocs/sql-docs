---
title: "Sending to a Remote Private Message Queue with the Script Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "Script task [Integration Services], remote private message queues"
  - "Message Queue task [Integration Services]"
  - "Script task [Integration Services], examples"
ms.assetid: 636314fd-d099-45cd-8bb4-f730d0a06739
author: janinezhang
ms.author: janinez
manager: craigg
---
# Sending to a Remote Private Message Queue with the Script Task

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Message Queuing (also known as MSMQ) makes it easy for developers to communicate with application programs quickly and reliably by sending and receiving messages. A message queue may be located on the local computer or a remote computer, and may be public or private. In [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], the MSMQ connection manager and Message Queue task do not support sending to a private queue on a remote computer. However, by using the Script task, it is easy to send a message to a remote private queue.  
  
> [!NOTE]  
>  If you want to create a task that you can more easily reuse across multiple packages, consider using the code in this Script task sample as the starting point for a custom task. For more information, see [Developing a Custom Task](../../integration-services/extending-packages-custom-objects/task/developing-a-custom-task.md).  
  
## Description  
 The following example uses an existing MSMQ connection manager, together with objects and methods from the System.Messaging namespace, to send the text contained in a package variable to a remote private message queue. The call to the M:Microsoft.SqlServer.Dts.ManagedConnections.MSMQConn.AcquireConnection(System.Object) method of the MSMQ connection manager returns a **MessageQueue** object whose **Send** method accomplishes this task.  
  
#### To configure this Script Task example  
  
1.  Create an MSMQ connection manager with the default name. Set the path of a valid remote private queue, in the following format:  
  
    ```  
    FORMATNAME:DIRECT=OS:<computername>\private$\<queuename>  
    ```  
  
2.  Create an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] variable named **MessageText** of type **String** to pass the message text into the script. Enter a default message as the value of the variable.  
  
3.  Add a Script Task to the design surface and edit it. On the **Script** tab of the **Script Task Editor**, add the `MessageText` variable to the **ReadOnlyVariables** property to make the variable available inside the script.  
  
4.  Click **Edit Script** to open the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] Tools for Applications (VSTA) script editor.  
  
5.  Add a reference in the script project to the **System.Messaging** namespace.  
  
6.  Replace the contents of the script window with the code in the following section.  
  
## Code  
  
```vb  
Imports System  
Imports Microsoft.SqlServer.Dts.Runtime  
Imports System.Messaging  
  
Public Class ScriptMain  
  
    Public Sub Main()  
  
        Dim remotePrivateQueue As MessageQueue  
        Dim messageText As String  
  
        remotePrivateQueue = _  
            DirectCast(Dts.Connections("Message Queue Connection Manager").AcquireConnection(Dts.Transaction), _  
            MessageQueue)  
        messageText = DirectCast(Dts.Variables("MessageText").Value, String)  
        remotePrivateQueue.Send(messageText)  
  
        Dts.TaskResult = ScriptResults.Success  
  
    End Sub  
  
End Class  
```  
  
```csharp  
using System;  
using Microsoft.SqlServer.Dts.Runtime;  
using System.Messaging;  
  
public class ScriptMain  
{  
  
    public void Main()  
        {  
  
            MessageQueue remotePrivateQueue = new MessageQueue();  
            string messageText;  
  
            remotePrivateQueue = (MessageQueue)(Dts.Connections["Message Queue Connection Manager"].AcquireConnection(Dts.Transaction) as MessageQueue);  
            messageText = (string)(Dts.Variables["MessageText"].Value);  
            remotePrivateQueue.Send(messageText);  
  
            Dts.TaskResult = (int)ScriptResults.Success;  
  
        }  
  
}  
```  
  
## See Also  
 [Message Queue Task](../../integration-services/control-flow/message-queue-task.md)  
  
  
