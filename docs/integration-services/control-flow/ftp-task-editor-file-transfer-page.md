---
title: "FTP Task Editor (File Transfer Page) | Microsoft Docs"
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
  - "sql13.dts.designer.ftptask.filetransfer.f1"
helpviewer_keywords: 
  - "File Transfer Protocol Task Editor"
ms.assetid: 37e52220-feb2-474c-ad88-fa1b1059acd4
caps.latest.revision: 26
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# FTP Task Editor (File Transfer Page)
  Use the **File Transfer** page of the **FTP Task Editor** dialog box to configure the FTP operation that the task performs.  
  
 To learn about this task, see [FTP Task](../../integration-services/control-flow/ftp-task.md).  
  
## Options  
 **IsRemotePathVariable**  
 Indicate whether the remote path is stored in a variable. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**True**|The destination path is stored in a variable. Selecting the value displays the dynamic option, **RemoteVariable**.|  
|**False**|The destination path is specified in a File connection manager. Selecting the value displays the dynamic option, **RemotePath**.|  
  
 **OverwriteFileAtDestination**  
 Specify whether a file at the destination can be overwritten.  
  
 **IsLocalPathVariable**  
 Indicate whether the local path is stored in a variable. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**True**|The destination path is stored in a variable. Selecting the value displays the dynamic option, **LocalVariable**.|  
|**False**|The destination path is specified in a File connection manager. Selecting the value displays the dynamic option, **LocalPath**.|  
  
 **Operation**  
 Select the FTP operation to perform. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Send files**|Send files. Selecting this value displays the dynamic options, **LocalVariable**, **LocalPathRemoteVariable** and **RemotePath**.|  
|**Receive files**|Receive files. Selecting this value displays the dynamic options, **LocalVariable**, **LocalPathRemoteVariable** and **RemotePath**.|  
|**Create local directory**|Create a local directory. Selecting this value displays the dynamic options, **LocalVariable** and **LocalPath**.|  
|**Create remote directory**|Create a remote directory. Selecting this value displays the dynamic options, **RemoteVariable** and **RemotelPath**.|  
|**Remove local directory**|Removes a local directory. Selecting this value displays the dynamic options, **LocalVariable** and **LocalPath**.|  
|**Remove remote directory**|Remove a remote directory. Selecting this value displays the dynamic options, **RemoteVariable** and **RemotePath**.|  
|**Delete local files**|Delete local files. Selecting this value displays the dynamic options, **LocalVariable** and **LocalPath**.|  
|**Delete remote files**|Delete remote files. Selecting this value displays the dynamic options, **RemoteVariable** and **RemotePath**.|  
  
 **IsTransferASCII**  
 Indicate whether files transferred to and from the remote FTP server should be transferred in ASCII mode.  
  
## IsRemotePathVariable Dynamic Options  
  
### IsRemotePathVariable = True  
 **RemoteVariable**  
 Select an existing user-defined variable, or click \<**New variable...**> to create a user-defined variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), Add Variable  
  
### IsRemotePathVariable = False  
 **RemotePath**  
 Select an existing FTP connection manager, or click \<**New connection…**> to create a connection manager.  
  
 **Related Topics:** [FTP Connection Manager](../../integration-services/connection-manager/ftp-connection-manager.md), [FTP Connection Manager Editor](../../integration-services/connection-manager/ftp-connection-manager-editor.md)  
  
## IsLocalPathVariable Dynamic Options  
  
### IsLocalPathVariable = True  
 **LocalVariable**  
 Select an existing user-defined variable, or click \<**New variable...**> to create a variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), Add Variable  
  
### IsLocalPathVariable = False  
 **LocalPath**  
 Select an existing File connection manager, or click \<**New connection…**> to create a connection manager.  
  
 **Related Topics**: [Flat File Connection Manager](../../integration-services/connection-manager/flat-file-connection-manager.md), [File Connection Manager Editor](../../integration-services/connection-manager/file-connection-manager-editor.md)  
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)   
 [FTP Task Editor &#40;General Page&#41;](../../integration-services/control-flow/ftp-task-editor-general-page.md)   
 [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
  