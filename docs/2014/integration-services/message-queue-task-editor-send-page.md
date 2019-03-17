---
title: "Message Queue Task Editor (Send Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.msgqueuetask.send.f1"
helpviewer_keywords: 
  - "Message Queue Task Editor"
ms.assetid: 565aa079-ac44-4407-8efc-cddab839de30
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Message Queue Task Editor (Send Page)
  Use the **Send** page of the **Message Queue Task Editor** dialog box to configure a Message Queue task to send messages from a [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package.  
  
 To learn about this task, see [Message Queue Task](control-flow/message-queue-task.md).  
  
## Options  
 **UseEncryption**  
 Indicate whether to encrypt the message. The default is `False`.  
  
 **EncryptionAlgorithm**  
 If you choose to use encryption, specify the name of the encryption algorithm to use. The Message Queue task can use the RC2 and RC4 algorithms. The default is **RC2**.  
  
> [!NOTE]  
>  The RC4 algorithm is only supported for backward compatibility. New material can only be encrypted using RC4 or RC4_128 when the database is in compatibility level 90 or 100. (Not recommended.) Use a newer algorithm such as one of the AES algorithms instead. In the current release of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], material encrypted using RC4 or RC4_128 can be decrypted in any compatibility level.  
  
> [!IMPORTANT]  
>  These are the encryption algorithms that the Message Queuing (also known as MSMQ) technology supports. Both of these encryption algorithms are now considered cryptographically weak compared to newer algorithms, which Message Queuing does not yet support. Therefore, you should consider your cryptography needs carefully when sending messages using the Message Queue task.  
  
 **MessageType**  
 Select the message type. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Data file message**|The message is stored in a file. Selecting the value displays the dynamic option, **DataFileMessage**.|  
|**Variable message**|The message is stored in a variable. Selecting the value displays the dynamic option, **VariableMessage**.|  
|**String message**|The message is stored in the Message Queue task. Selecting the value displays the dynamic option, **StringMessage**.|  
  
## MessageType Dynamic Options  
  
### MessageType = Data file message  
 **DataFileMessage**  
 Type the path of the data file, or click the ellipsis **(...)** and then locate the file.  
  
### MessageType = Variable message  
 **VariableMessage**  
 Type the variable names, or click the ellipsis **(...)** and then select the variables. Variables are separated by commas.  
  
 **Related Topics:** Select Variables  
  
### MessageType = String message  
 **StringMessage**  
 Type the string message, or click the ellipsis **(...)** and then type the message in the **Enter String Message** dialog box.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Message Queue Task Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Message Queue Task Editor &#40;Receive Page&#41;](../../2014/integration-services/message-queue-task-editor-receive-page.md)   
 [Expressions Page](expressions/expressions-page.md)  
  
  
