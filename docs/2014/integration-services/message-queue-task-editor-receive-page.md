---
title: "Message Queue Task Editor (Receive Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.msgqueuetask.receive.f1"
helpviewer_keywords: 
  - "Message Queue Task Editor"
ms.assetid: 7028756d-1dcc-480c-bbcd-e9654f0772a0
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Message Queue Task Editor (Receive Page)
  Use the **Receive** page of the **Message Queue Task Editor** dialog box to configure a Message Queue task to receive [!INCLUDE[msCoName](../includes/msconame-md.md)] Message Queuing (MSMQ) messages.  
  
 To learn about this task, see [Message Queue Task](control-flow/message-queue-task.md).  
  
## Options  
 **RemoveFromMessageQueue**  
 Indicate whether to remove the message from the queue after it is received. By default, this value is set to `False`.  
  
 **ErrorIfMessageTimeOut**  
 Indicate whether the task fails when the message times out, displaying an error message. The default is `False`.  
  
 **TimeoutAfter**  
 If you choose to display an error message on task failure, specify the number of seconds to wait before displaying the time-out message.  
  
 **MessageType**  
 Select the message type. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Data file message**|The message is stored in a file. Selecting the value displays the dynamic option, **DataFileMessage**.|  
|**Variable message**|The message is stored in a variable. Selecting the value displays the dynamic option, **VariableMessage**.|  
|**String message**|The message is stored in the Message Queue task. Selecting the value displays the dynamic option, **StringMessage**.|  
|**String message to variable**|The message<br /><br /> Selecting the value displays the dynamic option, **StringMessage**.|  
  
## MessageType Dynamic Options  
  
### MessageType = Data file message  
 **SaveFileAs**  
 Type the path of the file to use, or click the ellipsis button **(...)** and then locate the file.  
  
 **Overwrite**  
 Indicate whether to overwrite the data in an existing file when saving the contents of a data file message. The default is `False`.  
  
 **Filter**  
 Specify whether to apply a filter to the message. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**No filter**|The task does not filter messages. Selecting the value displays the dynamic option, **IdentifierReadOnly**.|  
|**From package**|The message receives only messages from the specified package. Selecting the value displays the dynamic option, **Identifier**.|  
  
### Filter Dynamic Options  
  
#### Filter = No filter  
 **IdentifierReadOnly**  
 This option is read-only. It may be blank or contain the GUID of a package when the Filter property was previously set.  
  
#### Filter = From package  
 **Identifier**  
 If you choose to apply a filter, type the unique identifier of the package from which messages can be received, or click the ellipsis button **(...)** and then specify the package.  
  
 **Related Topics:** [Select a Package](control-flow/select-a-package.md)  
  
### MessageType = Variable message  
 **Filter**  
 Specify whether to apply a filter to messages. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**No filter**|The task does not filter messages. Selecting the value displays the dynamic option, **IdentifierReadOnly**.|  
|**From package**|The message receives only messages from the specified package. Selecting the value displays the dynamic option, **Identifier**.|  
  
 **Variable**  
 Type the variable name, or click \<**New variable...**> and then configure a new variable.  
  
 **Related Topics:** [Add Variable](../../2014/integration-services/add-variable.md)  
  
### Filter Dynamic Options  
  
#### Filter = No filter  
 **IdentifierReadOnly**  
 This option is blank.  
  
#### Filter = From package  
 **Identifier**  
 If you choose to apply a filter, type the unique identifier of the package from which messages can be received, or click the ellipsis button **(...)** and then specify the package.  
  
 **Related Topics:** [Select a Package](control-flow/select-a-package.md)  
  
### MessageType = String message  
 **Compare**  
 Specify whether to apply a filter to messages. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**None**|Messages are not compared.|  
|**Exact match**|Messages must match exactly the string in the **CompareString** option.|  
|**Ignore case**|Message must match the string in the **CompareString** option, but the comparison is not case sensitive.|  
|**Containing**|Message must contain the string in the **CompareString** option.|  
  
 **CompareString**  
 Unless the **Compare** option is set to **None**, provide the string to which the message is compared.  
  
### MessageType = String message to variable  
 **Compare**  
 Specify whether to apply a filter to messages. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**None**|Messages are not compared.|  
|**Exact match**|The message must match exactly the string in the **CompareString** option.|  
|**Ignore case**|The message must match the string in the **CompareString** option but the comparison is not case sensitive.|  
|**Containing**|The message must contain the string in the **CompareString** option.|  
  
 **CompareString**  
 Unless the **Compare** option is set to **None**, provide the string to which the message is compared.  
  
 **Variable**  
 Type the name of the variable to hold the received message, or click \<**New variable...**> and then configure a new variable.  
  
 **Related Topics:** [Add Variable](../../2014/integration-services/add-variable.md)  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Message Queue Task Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Message Queue Task Editor &#40;Send Page&#41;](../../2014/integration-services/message-queue-task-editor-send-page.md)   
 [Expressions Page](expressions/expressions-page.md)   
 [Message Queue Task](control-flow/message-queue-task.md)  
  
  
