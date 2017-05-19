---
title: "Send Mail Task | Microsoft Docs"
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
  - "sql13.dts.designer.sendmailtask.f1"
helpviewer_keywords: 
  - "mail [Integration Services]"
  - "Send Mail task"
  - "e-mail [Integration Services]"
  - "messages [Integration Services]"
  - "sending messages"
ms.assetid: fe0b7cbc-fe8e-4fe2-95b4-2953efff5869
caps.latest.revision: 51
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Send Mail Task
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
  
 For information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [Send Mail Task Editor &#40;General Page&#41;](../../integration-services/control-flow/send-mail-task-editor-general-page.md)  
  
-   [Send Mail Task Editor &#40;Mail Page&#41;](../../integration-services/control-flow/send-mail-task-editor-mail-page.md)  
  
-   [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
 For information about programmatically setting these properties, click the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.SendMailTask.SendMailTask>  
  
## Related Tasks  
 For information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click [Set the Properties of a Task or Container](http://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b).  
  
## Related Content  
  
-   Technical article, [How to send email with delivery notification in C#](http://go.microsoft.com/fwlink/?LinkId=237625), on shareourideas.com  
  
## See Also  
 [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md)   
 [Control Flow](../../integration-services/control-flow/control-flow.md)  
  
  