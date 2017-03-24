---
title: "FTP Task Editor (General Page) | Microsoft Docs"
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
  - "sql13.dts.designer.ftptask.general.f1"
helpviewer_keywords: 
  - "File Transfer Protocol Task Editor"
ms.assetid: 28b46fdd-b04a-4f97-a99f-883f5735a6d9
caps.latest.revision: 29
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# FTP Task Editor (General Page)
  Use the **General** page of the **FTP Task Editor** dialog box to specify the FTP connection manager that connects to the FTP server that the task communicates with. You can also name and describe the FTP task.  
  
 To learn about this task, see [FTP Task](../../integration-services/control-flow/ftp-task.md).  
  
## Options  
 **FtpConnection**  
 Select an existing FTP connection manager, or click \<**New connection...**> to create a connection manager.  
  
> [!IMPORTANT]  
>  The FTP connection manager supports only anonymous authentication and basic authentication. It does not support Windows Authentication.  
  
 **Related Topics**: [FTP Connection Manager](../../integration-services/connection-manager/ftp-connection-manager.md), [FTP Connection Manager Editor](../../integration-services/connection-manager/ftp-connection-manager-editor.md)  
  
 **StopOnFailure**  
 Indicate whether the FTP task terminates if an FTP operation fails.  
  
 **Name**  
 Provide a unique name for the FTP task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the FTP task.  
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)   
 [FTP Task Editor &#40;File Transfer Page&#41;](../../integration-services/control-flow/ftp-task-editor-file-transfer-page.md)   
 [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
  