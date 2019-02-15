---
title: "Message Queue Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.messagequeuetask.f1"
  - "sql13.dts.designer.msgqueuetask.general.f1"
  - "sql13.dts.designer.msgqueuetask.send.f1"
  - "sql13.dts.designer.msgqueuetask.receive.f1"
  - "sql13.dts.designer.selectvariables.f1"
helpviewer_keywords: 
  - "Message Queue task [Integration Services]"
  - "receiving messages"
  - "messages [Integration Services]"
  - "sending messages"
ms.assetid: ae1d8fad-6649-4e93-b589-14a32d07da33
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Message Queue Task
  The Message Queue task allows you to use Message Queuing (also known as MSMQ) to send and receive messages between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages, or to send messages to an application queue that is processed by a custom application. These messages can take the form of simple text, files, or variables and their values.  
  
 By using the Message Queue task, you can coordinate operations throughout your enterprise. Messages can be queued and delivered later if the destination is unavailable or busy; for example, the task can queue messages for the offline laptop computer of sales representatives, who receive their messages when they connect to the network. You can use the Message Queue task for the following purposes:  
  
-   Delaying task execution until other packages check in. For example, after nightly maintenance at each of your retail sites, a Message Queue task sends a message to your corporate computer. A package running on the corporate computer contains Message Queue tasks, each waiting for a message from a particular retail site. When a message from a site arrives, a task uploads data from that site. After all the sites have checked in, the package computes summary totals.  
  
-   Sending data files to the computer that processes them. For example, the output from a restaurant cash register can be sent in a data file message to the corporate payroll system, where data about each waiter's tips is extracted.  
  
-   Distributing files throughout your enterprise. For example, a package can use a Message Queue task to send a package file to another computer. A package running on the destination computer then uses a Message Queue task to retrieve and save the package locally.  
  
 When sending or receiving messages, the Message Queue task uses one of four message types: data file, string, string message to variable, or variable. The string message to variable message type can be used only when receiving messages.  
  
 The task uses an MSMQ connection manager to connect to a message queue. For more information, see [MSMQ Connection Manager](../../integration-services/connection-manager/msmq-connection-manager.md). For more information about Message Queuing, see the [MSDN Library](https://go.microsoft.com/fwlink/?LinkId=7022).  
  
 The Message Queue task requires that the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service be installed. Some [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components that you may select for installation on the **Components to Install** page or the **Feature Selection** page of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard install a partial subset of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] components. These components are useful for specific tasks, but the functionality of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] will be limited. For example, the [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] option installs the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] components required to design a package, but the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is not installed, and therefore the Message Queue task is not functional. To ensure a complete installation of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], you must select [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] on the **Components to Install** page. For more information about installing and running the Message Queue task, see [Install Integration Services](../../integration-services/install-windows/install-integration-services.md).  
  
> [!NOTE]  
>  The Message Queue task fails to comply with Federal Information Processing Standard (FIPS) 140-2 when the computer's operating system is configured in FIPS mode and the task uses encryption. If the Message Queue task does not use encryption, the task can run successfully.  
  
## Message Types  
 You can configure the message types that the Message Queue task provides in the following ways:  
  
-   **Data file** message specifies that a file contains the message. When receiving messages, you can configure the task to save the file, overwrite an existing file, and specify the package from which the task can receive messages.  
  
-   **String** message specifies the message as a string. When receiving messages, you can configure the task to compare the received string with a user-defined string and take action depending on the comparison. String comparison can be exact, case-sensitive or case-insensitive, or use a substring.  
  
-   **String message to variable** specifies the source message as a string that is sent to a destination variable. You can configure the task to compare the received string with a user-defined string using an exact, case-insensitive, or substring comparison. This message type is available only when the task is receiving messages.  
  
-   **Variable** specifies that the message contains one or more variables. You can configure the task to specify the names of the variables included in the message. When receiving messages, you can configure the task to specify both the package from which it can receive messages and the variable that is the destination of the message.  
  
## Sending Messages  
 When configuring the Message Queue task to send messages, you can use one of the encryption algorithms that are currently supported by the Message Queuing technology, RC2 and RC4, to encrypt the message. Both of these encryption algorithms are now considered cryptographically weak compared to newer algorithms, which Message Queuing technology do not yet support. Therefore, you should consider your cryptography needs carefully when sending messages using the Message Queue task.  
  
## Receiving Messages  
 When receiving messages, the Message Queue task can be configured in the following ways:  
  
-   Bypassing the message, or removing the message from the queue.  
  
-   Specifying a time-out.  
  
-   Failing if a time-out occurs.  
  
-   Overwriting an existing file, if the message is stored in a **Data file**.  
  
-   Saving the message file to a different file name, if the message uses the **Data file message** type.  
  
## Custom Logging Messages Available on the Message Queue Task  
 The following table lists the custom log entries for the Message Queue task. For more information, see [Integration Services &#40;SSIS&#41; Logging](../../integration-services/performance/integration-services-ssis-logging.md).  
  
|Log entry|Description|  
|---------------|-----------------|  
|**MSMQAfterOpen**|Indicates that the task finished opening the message queue.|  
|**MSMQBeforeOpen**|Indicates that the task began to open the message queue.|  
|**MSMQBeginReceive**|Indicates that the task began to receive a message.|  
|**MSMQBeginSend**|Indicates that the task began to send a message.|  
|**MSMQEndReceive**|Indicates that the task finished receiving a message.|  
|**MSMQEndSend**|Indicates that the task finished sending a message.|  
|**MSMQTaskInfo**|Provides descriptive information about the task.|  
|**MSMQTaskTimeOut**|Indicates that the task timed out.|  
  
## Configuration of the Message Queue Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically. For information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
 For information about programmatically setting these properties, see the documentation for the **Microsoft.SqlServer.Dts.Tasks.MessageQueueTask.MessageQueueTask** class in the Developer Guide.  
  
## Related Tasks  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b).  
  
## Message Queue Task Editor (General Page)
  Use the **General page** of the **Message Queue Task Editor** dialog box to name and describe the Message Queue task, to specify the message format, and to indicate whether the task sends or receives messages.  
  
### Options  
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
  
## Message Queue Task Editor (Send Page)
  Use the **Send** page of the **Message Queue Task Editor** dialog box to configure a Message Queue task to send messages from a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package.  
  
### Options  
 **UseEncryption**  
 Indicate whether to encrypt the message. The default is **False**.  
  
 **EncryptionAlgorithm**  
 If you choose to use encryption, specify the name of the encryption algorithm to use. The Message Queue task can use the RC2 and RC4 algorithms. The default is **RC2**.  
  
> [!NOTE]  
>  The RC4 algorithm is only supported for backward compatibility. New material can only be encrypted using RC4 or RC4_128 when the database is in compatibility level 90 or 100. (Not recommended.) Use a newer algorithm such as one of the AES algorithms instead. In the current release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], material encrypted using RC4 or RC4_128 can be decrypted in any compatibility level.  
  
> [!IMPORTANT]  
>  These are the encryption algorithms that the Message Queuing (also known as MSMQ) technology supports. Both of these encryption algorithms are now considered cryptographically weak compared to newer algorithms, which Message Queuing does not yet support. Therefore, you should consider your cryptography needs carefully when sending messages using the Message Queue task.  
  
 **MessageType**  
 Select the message type. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Data file message**|The message is stored in a file. Selecting the value displays the dynamic option, **DataFileMessage**.|  
|**Variable message**|The message is stored in a variable. Selecting the value displays the dynamic option, **VariableMessage**.|  
|**String message**|The message is stored in the Message Queue task. Selecting the value displays the dynamic option, **StringMessage**.|  
  
### MessageType Dynamic Options  
  
#### MessageType = Data file message  
 **DataFileMessage**  
 Type the path of the data file, or click the ellipsis **(...)** and then locate the file.  
  
#### MessageType = Variable message  
 **VariableMessage**  
 Type the variable names, or click the ellipsis **(...)** and then select the variables. Variables are separated by commas.  
  
 **Related Topics:** Select Variables  
  
#### MessageType = String message  
 **StringMessage**  
 Type the string message, or click the ellipsis **(...)** and then type the message in the **Enter String Message** dialog box.  
  
## Message Queue Task Editor (Receive Page)
  Use the **Receive** page of the **Message Queue Task Editor** dialog box to configure a Message Queue task to receive [!INCLUDE[msCoName](../../includes/msconame-md.md)] Message Queuing (MSMQ) messages.  
  
### Options  
 **RemoveFromMessageQueue**  
 Indicate whether to remove the message from the queue after it is received. By default, this value is set to **False**.  
  
 **ErrorIfMessageTimeOut**  
 Indicate whether the task fails when the message times out, displaying an error message. The default is **False**.  
  
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
  
### MessageType Dynamic Options  
  
#### MessageType = Data file message  
 **SaveFileAs**  
 Type the path of the file to use, or click the ellipsis button **(...)** and then locate the file.  
  
 **Overwrite**  
 Indicate whether to overwrite the data in an existing file when saving the contents of a data file message. The default is **False**.  
  
 **Filter**  
 Specify whether to apply a filter to the message. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**No filter**|The task does not filter messages. Selecting the value displays the dynamic option, **IdentifierReadOnly**.|  
|**From package**|The message receives only messages from the specified package. Selecting the value displays the dynamic option, **Identifier**.|  
  
#### Filter Dynamic Options  
  
##### Filter = No filter  
 **IdentifierReadOnly**  
 This option is read-only. It may be blank or contain the GUID of a package when the Filter property was previously set.  
  
##### Filter = From package  
 **Identifier**  
 If you choose to apply a filter, type the unique identifier of the package from which messages can be received, or click the ellipsis button **(...)** and then specify the package.  
  
 **Related Topics:** [Select a Package](../../integration-services/control-flow/select-a-package.md)  
  
#### MessageType = Variable message  
 **Filter**  
 Specify whether to apply a filter to messages. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**No filter**|The task does not filter messages. Selecting the value displays the dynamic option, **IdentifierReadOnly**.|  
|**From package**|The message receives only messages from the specified package. Selecting the value displays the dynamic option, **Identifier**.|  
  
 **Variable**  
 Type the variable name, or click \<**New variable...**> and then configure a new variable.  
  
 **Related Topics:** [Add Variable](https://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5)  
  
#### Filter Dynamic Options  
  
##### Filter = No filter  
 **IdentifierReadOnly**  
 This option is blank.  
  
##### Filter = From package  
 **Identifier**  
 If you choose to apply a filter, type the unique identifier of the package from which messages can be received, or click the ellipsis button **(...)** and then specify the package.  
  
 **Related Topics:** [Select a Package](../../integration-services/control-flow/select-a-package.md)  
  
#### MessageType = String message  
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
  
#### MessageType = String message to variable  
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
  
 **Related Topics:** [Add Variable](https://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5)  
  
## Select Variables
  Use the **Select Variables** dialog box to specify the variables to use in a send message operation in the Message Queue task. The **Available Variables** list includes system and user-defined variables that are in the scope of the Message Queue task or its parent container. The task uses the variables in the **Selected Variables** list.  
  
### Options  
 **Available Variables**  
 Select one or more variables.  
  
 **Selected Variables**  
 Select one or more variables.  
  
 **Right Arrows**  
 Move selected variables to the **Selected Variables** list.  
  
 **Left Arrows**  
 Move selected variables back to the **Available Variables** list.  
  
 **New Variable**  
 Create a new variable.  
  
 **Related Topics:** [Add Variable](https://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5)  
## See Also  
 [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md)   
 [Control Flow](../../integration-services/control-flow/control-flow.md)  
  
  
