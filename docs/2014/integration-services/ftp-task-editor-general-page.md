---
title: "FTP Task Editor (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.ftptask.general.f1"
helpviewer_keywords: 
  - "File Transfer Protocol Task Editor"
ms.assetid: 28b46fdd-b04a-4f97-a99f-883f5735a6d9
author: janinezhang
ms.author: janinez
manager: craigg
---
# FTP Task Editor (General Page)
  Use the **General** page of the **FTP Task Editor** dialog box to specify the FTP connection manager that connects to the FTP server that the task communicates with. You can also name and describe the FTP task.  
  
 To learn about this task, see [FTP Task](control-flow/ftp-task.md).  
  
## Options  
 **FtpConnection**  
 Select an existing FTP connection manager, or click \<**New connection...**> to create a connection manager.  
  
> [!IMPORTANT]  
>  The FTP connection manager supports only anonymous authentication and basic authentication. It does not support Windows Authentication.  
  
 **Related Topics**: [FTP Connection Manager](connection-manager/ftp-connection-manager.md), [FTP Connection Manager Editor](../../2014/integration-services/ftp-connection-manager-editor.md)  
  
 **StopOnFailure**  
 Indicate whether the FTP task terminates if an FTP operation fails.  
  
 **Name**  
 Provide a unique name for the FTP task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the FTP task.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [FTP Task Editor &#40;File Transfer Page&#41;](../../2014/integration-services/ftp-task-editor-file-transfer-page.md)   
 [Expressions Page](expressions/expressions-page.md)  
  
  
