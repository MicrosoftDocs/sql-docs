---
description: "Send Mail Task"
title: "Send Mail Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.sendmailtask.f1"
  - "sql13.dts.designer.sendmailtask.general.f1"
  - "sql13.dts.designer.sendmailtask.mail.f1"
helpviewer_keywords: 
  - "mail [Integration Services]"
  - "Send Mail task"
  - "e-mail [Integration Services]"
  - "messages [Integration Services]"
  - "sending messages"
ms.assetid: fe0b7cbc-fe8e-4fe2-95b4-2953efff5869
author: chugugrace
ms.author: chugu
---
# Send Mail Task

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  The Send Mail task sends an e-mail message. By using the Send Mail task, a package can send messages if tasks in the package workflow succeed or fail, or send messages in response to an event that the package raises at run time. For example, the task can notify a database administrator about the success or failure of the Backup Database task.  
  
 You can configure the Send Mail task in the following ways:  
  
-   Provide the message text for the e-mail message.  
  
-   Provide a subject line for the e-mail message.  
  
-   Set the priority level of the message. The task supports three priority levels: normal, low, and high.  
  
-   Specify the recipients on the To, Cc, and Bcc lines. If the task specifies multiple recipients, they are separated by semicolons.  
  
    > [!NOTE]  
    >  The To, Cc, and Bcc lines are limited to 256 characters each in accordance with Internet standards.  
  
-   Include attachments. If the task specifies multiple attachments, they are separated by the pipe (|) character.  
  
    > [!NOTE]  
    >  If an attachment file does not exist when the package runs, an error occurs.  
  
-   Specify the SMTP connection manager to use.  
  
    > [!IMPORTANT]  
    >  The SMTP connection manager supports only anonymous authentication and Windows Authentication. It does not support basic authentication.  
  
 The message text can be a string that you provide, a connection to a file that contains the text, or the name of a variable that contains the text. The task uses a File connection manager to connect to a file. For more information, see [Flat File Connection Manager](../../integration-services/connection-manager/flat-file-connection-manager.md).  
  
 The task uses an SMTP connection manager to connect to a mail server. For more information, see [SMTP Connection Manager](../../integration-services/connection-manager/smtp-connection-manager.md).  
  
## Custom Logging Messages Available on the Send Mail Task  
 The following table lists the custom log entries for the Send Mail task. For more information, see [Integration Services &#40;SSIS&#41; Logging](../../integration-services/performance/integration-services-ssis-logging.md).  
  
|Log entry|Description|  
|---------------|-----------------|  
|**SendMailTaskBegin**|Indicates that the task began to send an e-mail message.|  
|**SendMailTaskEnd**|Indicates that the task finished sending an e-mail message.|  
|**SendMailTaskInfo**|Provides descriptive information about the task.|  
  
## Configuring the Send Mail Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
 For information about programmatically setting these properties, click the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.SendMailTask.SendMailTask>  
  
## Related Tasks  
 For information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click [Set the Properties of a Task or Container](./add-or-delete-a-task-or-a-container-in-a-control-flow.md).  
  
## Related Content  
  
-   Technical article, [How to send email with delivery notification in C#](https://go.microsoft.com/fwlink/?LinkId=237625), on shareourideas.com  
  
## Send Mail Task Editor (General Page)
  Use the **General page** of the **Send Mail Task Editor** dialog box to name and describe the Send Mail task.  
  
### Options  
 **Name**  
 Provide a unique name for the Send Mail task. This name is used as the label in the task icon.  
  
 **Note** Task names must be unique within a package.  
  
 **Description**  
 Type a description of the Send Mail task.  
  
## Send Mail Task Editor (Mail Page)
  Use the **Mail** page of the **Send Mail Task Editor** dialog box to specify recipients, message type, and priority for a message. You can also attach files to the message. The message text can be a string you provide, a file connection to a file that contains the text, or the name of a variable that contains the text.  
  
### Options  
 **SMTPConnection**  
 Select an SMTP connection manager in the list, or click **\<New connection...>** to create a new connection manager.  
  
> [!IMPORTANT]  
>  The SMTP connection manager supports only anonymous authentication and Windows Authentication. It does not support basic authentication.  
  
 **Related Topics:** [SMTP Connection Manager](../../integration-services/connection-manager/smtp-connection-manager.md)  
  
 **From**  
 Specify the e-mail address of the sender.  
  
 **To**  
 Provide the e-mail addresses of the recipients, delimited by semicolons.  
  
 **Cc**  
 Specify the e-mail addresses, delimited by semicolons, of individuals who also receive copies of the message.  
  
 **Bcc**  
 Specify the e-mail addresses, delimited by semicolons, of individuals who receive blind carbon copies (Bcc) copies of the message.  
  
 **Subject**  
 Provide a subject for the e-mail message.  
  
 **MessageSourceType**  
 Select the source type of the message. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Direct input**|Set the source to the message text. Selecting this value displays the dynamic option, **MessageSource**.|  
|**File connection**|Set the source to the file that contains the message text. Selecting this value displays the dynamic option, **MessageSource**.|  
|**Variable**|Set the source to a variable that contains the message text. Selecting this value displays the dynamic option, **MessageSource**.|  
  
 **Priority**  
 Set the priority of the message.  
  
 **Attachments**  
 Provide the file names of attachments to the e-mail message, delimited by the pipe (|) character.  
  
> [!NOTE]  
>  The To, Cc, and Bcc lines are limited to 256 characters in accordance with Internet standards.  
  
### MessageSourceType Dynamic Options  
  
#### MessageSourceType = Direct Input  
 **MessageSource**  
 Type the message text or click the browse button (...) and then type the message in the **Message source** dialog box.  
  
#### MessageSourceType = File connection  
 **MessageSource**  
 Select a File connection manager in the list or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md), [File Connection Manager Editor](../connection-manager/file-connection-manager.md)  
  
#### MessageSourceType = Variable  
 **MessageSource**  
 Select a variable in the list or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), [Add Variable](../integration-services-ssis-variables.md)  
  
## See Also  
 [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md)   
 [Control Flow](../../integration-services/control-flow/control-flow.md)  
  
