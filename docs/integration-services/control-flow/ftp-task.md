---
title: "FTP Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.ftptask.f1"
  - "sql13.dts.designer.ftptask.general.f1"
  - "sql13.dts.designer.ftptask.filetransfer.f1"
helpviewer_keywords: 
  - "FTP task [Integration Services]"
ms.assetid: 41c3f2c4-ee04-460a-9822-bb9ae4036c2e
author: janinezhang
ms.author: janinez
manager: craigg
---
# FTP Task
  The FTP task downloads and uploads data files and manages directories on servers. For example, a package can download data files from a remote server or an Internet location as part of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package workflow. You can use the FTP task for the following purposes:  
  
-   Copying directories and data files from one directory to another, before or after moving data, and applying transformations to the data.  
  
-   Logging in to a source FTP location and copying files or packages to a destination directory.  
  
-   Downloading files from an FTP location and applying transformations to column data before loading the data into a database.  
  
 At run time, the FTP task connects to a server by using an FTP connection manager. The FTP connection manager is configured separately from the FTP task, and then is referenced in the FTP task. The FTP connection manager includes the server settings, the credentials for accessing the FTP server, and options such as the time-out and the number of retries for connecting to the server. For more information, see [FTP Connection Manager](../../integration-services/connection-manager/ftp-connection-manager.md).  
  
> [!IMPORTANT]  
>  The FTP connection manager supports only anonymous authentication and basic authentication. It does not support Windows Authentication.  
  
 When accessing a local file or a local directory, the FTP task uses a File connection manager or path information stored in a variable. In contrast, when accessing a remote file or a remote directory, the FTP task uses a directly specified path on the remote server, as specified in the FTP connection manager, or path information stored in a variable. For more information, see [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md) and [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md).  
  
 This means that the FTP task can receive multiple files and delete multiple remote files; but the task can send only one file and delete only one local file if it uses a connection manager, because a File connection manager can access only one file. To access multiple local files, the FTP task must use a variable to provide the path information. For example, a variable that contains "C:\Test\&#42;.txt" provides a path that supports deleting or sending all the files that have a .txt extension in the Test directory.  
  
 To send multiple files and access multiple local files and directories, you can also execute the FTP task multiple times by including the task in a Foreach Loop. The Foreach Loop can enumerate across files in a directory using the For Each File enumerator. For more information, see [Foreach Loop Container](../../integration-services/control-flow/foreach-loop-container.md).  
  
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
 The following table lists the custom log entries for the FTP task. For more information, see [Integration Services &#40;SSIS&#41; Logging](../../integration-services/performance/integration-services-ssis-logging.md).  
  
|Log entry|Description|  
|---------------|-----------------|  
|**FTPConnectingToServer**|Indicates that the task initiated a connection to the FTP server.|  
|**FTPOperation**|Reports the beginning of and the type of FTP operation that the task performs.|  
  
## Related Tasks  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b).  
  
 For more information about programmatically setting these properties, see <xref:Microsoft.SqlServer.Dts.Tasks.FtpTask.FtpTask>.  
  
## FTP Task Editor (General Page)
  Use the **General** page of the **FTP Task Editor** dialog box to specify the FTP connection manager that connects to the FTP server that the task communicates with. You can also name and describe the FTP task.  
  
### Options  
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
  
## FTP Task Editor (File Transfer Page)
  Use the **File Transfer** page of the **FTP Task Editor** dialog box to configure the FTP operation that the task performs.  
  
### Options  
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
  
### IsRemotePathVariable Dynamic Options  
  
#### IsRemotePathVariable = True  
 **RemoteVariable**  
 Select an existing user-defined variable, or click \<**New variable...**> to create a user-defined variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), Add Variable  
  
#### IsRemotePathVariable = False  
 **RemotePath**  
 Select an existing FTP connection manager, or click \<**New connection...**> to create a connection manager.  
  
 **Related Topics:** [FTP Connection Manager](../../integration-services/connection-manager/ftp-connection-manager.md), [FTP Connection Manager Editor](../../integration-services/connection-manager/ftp-connection-manager-editor.md)  
  
### IsLocalPathVariable Dynamic Options  
  
#### IsLocalPathVariable = True  
 **LocalVariable**  
 Select an existing user-defined variable, or click \<**New variable...**> to create a variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), Add Variable  
  
#### IsLocalPathVariable = False  
 **LocalPath**  
 Select an existing File connection manager, or click \<**New connection...**> to create a connection manager.  
  
 **Related Topics**: [Flat File Connection Manager](../../integration-services/connection-manager/flat-file-connection-manager.md)  
  
## See Also  
 [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md)   
 [Control Flow](../../integration-services/control-flow/control-flow.md)  
  
  
