---
title: "Send Mail Task Editor (Mail Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.sendmailtask.mail.f1"
helpviewer_keywords: 
  - "Send Mail Task Editor"
ms.assetid: adb385d5-ef24-4d18-b9ea-b39e00a7075e
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Send Mail Task Editor (Mail Page)
  Use the **Mail** page of the **Send Mail Task Editor** dialog box to specify recipients, message type, and priority for a message. You can also attach files to the message. The message text can be a string you provide, a file connection to a file that contains the text, or the name of a variable that contains the text.  
  
 To learn about this task, see [Send Mail Task](control-flow/send-mail-task.md).  
  
## Options  
 **SMTPConnection**  
 Select an SMTP connection manager in the list, or click **\<New connection...>** to create a new connection manager.  
  
> [!IMPORTANT]  
>  The SMTP connection manager supports only anonymous authentication and Windows Authentication. It does not support basic authentication.  
  
 **Related Topics:** [SMTP Connection Manager](connection-manager/smtp-connection-manager.md)  
  
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
  
## MessageSourceType Dynamic Options  
  
### MessageSourceType = Direct Input  
 **MessageSource**  
 Type the message text or click the browse button (...) and then type the message in the **Message source** dialog box.  
  
### MessageSourceType = File connection  
 **MessageSource**  
 Select a File connection manager in the list or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)  
  
### MessageSourceType = Variable  
 **MessageSource**  
 Select a variable in the list or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md), [Add Variable](../../2014/integration-services/add-variable.md)  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Send Mail Task Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Expressions Page](expressions/expressions-page.md)  
  
  
