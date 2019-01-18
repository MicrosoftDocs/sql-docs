---
title: "FTP Task | Microsoft Docs"
ms.custom: ""
ms.date: "08/22/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.ftptask.f1"
helpviewer_keywords: 
  - "FTP task [Integration Services]"
ms.assetid: 41c3f2c4-ee04-460a-9822-bb9ae4036c2e
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# FTP Task
  The FTP task downloads and uploads data files and manages directories on servers. For example, a package can download data files from a remote server or an Internet location as part of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package workflow. You can use the FTP task for the following purposes:  
  
-   Copying directories and data files from one directory to another, before or after moving data, and applying transformations to the data.  
  
-   Logging in to a source FTP location and copying files or packages to a destination directory.  
  
-   Downloading files from an FTP location and applying transformations to column data before loading the data into a database.  
  
 At run time, the FTP task connects to a server by using an FTP connection manager. The FTP connection manager is configured separately from the FTP task, and then is referenced in the FTP task. The FTP connection manager includes the server settings, the credentials for accessing the FTP server, and options such as the time-out and the number of retries for connecting to the server. For more information, see [FTP Connection Manager](../connection-manager/ftp-connection-manager.md).  
  
> [!IMPORTANT]  
>  The FTP connection manager supports only anonymous authentication and basic authentication. It does not support Windows Authentication.  
  
 When accessing a local file or a local directory, the FTP task uses a File connection manager or path information stored in a variable. In contrast, when accessing a remote file or a remote directory, the FTP task uses a directly specified path on the remote server, as specified in the FTP connection manager, or path information stored in a variable. For more information, see [File Connection Manager](../connection-manager/file-connection-manager.md) and [Integration Services &#40;SSIS&#41; Variables](../integration-services-ssis-variables.md).  
  
 This means that the FTP task can receive multiple files and delete multiple remote files; but the task can send only one file and delete only one local file if it uses a connection manager, because a File connection manager can access only one file. To access multiple local files, the FTP task must use a variable to provide the path information. For example, a variable that contains "C:\Test\\*.txt" provides a path that supports deleting or sending all the files that have a .txt extension in the Test directory.  
  
 To send multiple files and access multiple local files and directories, you can also execute the FTP task multiple times by including the task in a Foreach Loop. The Foreach Loop can enumerate across files in a directory using the For Each File enumerator. For more information, see [Foreach Loop Container](foreach-loop-container.md).  
  
 The FTP task supports the *?* and *\** wildcard characters in paths. This lets the task access multiple files. However, you can use wildcard characters only in the part of the path that specifies the file name. For example, C:\MyDirectory\\*.txt is a valid path, but C:\\\*\MyText.txt is not.  
  
 The FTP operations can be configured to stop the File System task when the operation fails, or to transfer files in ASCII mode. The operations that send and receive files copy can be configured to overwrite destination files and directories.  
  
## Predefined FTP Operations  
 The FTP task includes a predefined set of operations. The following table describes these operations.  
  
|Operation|Description|  
|---------------|-----------------|  
|Send files|Sends a file from the local computer to the FTP server.|  
|Receive files|Saves a file from the FTP server to the local computer.|  
|Create local directory|Creates a folder on the local computer.|  
|Create remote directory|Creates a folder on the FTP server.|  
|Remove local directory|Deletes a folder on the local computer.|  
|Remove remote directory|Deletes a folder on the FTP server.|  
|Delete local files|Deletes a file on the local computer.|  
|Delete remote files|Deletes a file on the FTP server.|  
  
## Custom Log Entries Available on the FTP Task  
 The following table lists the custom log entries for the FTP task. For more information, see [Integration Services &#40;SSIS&#41; Logging](../performance/integration-services-ssis-logging.md) and [Custom Messages for Logging](../custom-messages-for-logging.md).  
  
|Log entry|Description|  
|---------------|-----------------|  
|`FTPConnectingToServer`|Indicates that the task initiated a connection to the FTP server.|  
|`FTPOperation`|Reports the beginning of and the type of FTP operation that the task performs.|  
  
## Related Tasks  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md).  
  
 For more information about programmatically setting these properties, see <xref:Microsoft.SqlServer.Dts.Tasks.FtpTask.FtpTask>.  
  
## See Also  
 [FTP Task Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)   
 [FTP Task Editor &#40;File Transfer Page&#41;](../ftp-task-editor-file-transfer-page.md)   
 [Integration Services Tasks](integration-services-tasks.md)   
 [Control Flow](control-flow.md)  
  
  
