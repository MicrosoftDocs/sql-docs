---
title: "File System Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.filesystemtask.f1"
helpviewer_keywords: 
  - "File System task [Integration Services]"
ms.assetid: 7dd79a6a-e066-4028-a385-1d40f31056f8
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# File System Task
  The File System task performs operations on files and directories in the file system. For example, by using the File System task, a package can create, move, or delete directories and files. You can also use the File System task to set attributes on files and directories. For example, the File System task can make files hidden or read-only.  
  
 All File System task operations use a source, which can be a file or a directory. For example, the file that the task copies or the directory it deletes is a source. The source can be specified by using a File connection manager that points to the directory or file or by providing the name of a variable that contains the source path. For more information, see [File Connection Manager](../connection-manager/file-connection-manager.md) and [Integration Services &#40;SSIS&#41; Variables](../integration-services-ssis-variables.md).  
  
 The operations that copy and move file and directories and rename files use a destination and a source. The destination is specified by using a File connection manager or a variable. File system task operations can be configured to permit overwriting of destination files and directories. The operation that creates a new directory can be configured to use an existing directory that has the specified name instead of failing when the directory already exists.  
  
## Predefined File System Operations  
 The File System task includes a predefined set of operations. The following table describes these operations.  
  
|Operation|Description|  
|---------------|-----------------|  
|Copy directory|Copies a folder from one location to another.|  
|Copy file|Copies a file from one location to another.|  
|Create directory|Creates a folder in a specified location.|  
|Delete directory|Deletes a folder in a specified location.|  
|Delete directory content|Deletes all files and folders in a folder.|  
|Delete file|Deletes a file in a specified location.|  
|Move directory|Moves a folder from one location to another.|  
|Move file|Moves a file from one location to another.|  
|Rename file|Renames a file in a specified location.|  
|Set attributes|Sets attributes on files and folders. Attributes include Archive, Hidden, Normal, ReadOnly, and System. Normal is the lack of attributes, and it cannot be combined with other attributes. All other attributes can be used in combination.|  
  
 The File System task operates on a single file or directory. Therefore, this task does not support the use of wildcard characters to perform the same operation on multiple files. To have the File System task repeat an operation on multiple files or directories, put the File System task in a Foreach Loop container, as described in the following steps:  
  
-   **Configure the Foreach Loop container** On the **Collection** page of the Foreach Loop Editor, set the enumerator to **Foreach File Enumerator** and enter the wildcard expression as the enumerator configuration for **Files**. On the **Variable Mappings** page of the Foreach Loop Editor, map a variable that you want to use to pass the file names one at a time to the File System task.  
  
-   **Add and configure a File System task** Add a File System task to the Foreach Loop container. On the **General** page of the File System Task Editor, set the **SourceVariable** or **DestinationVariable** property to the variable that you defined in the Foreach Loop container.  
  
## Custom Log Entries Available on the File System Task  
 The following table describes the custom log entry for the File System task. For more information, see [Integration Services &#40;SSIS&#41; Logging](../performance/integration-services-ssis-logging.md) and [Custom Messages for Logging](../custom-messages-for-logging.md).  
  
|Log entry|Description|  
|---------------|-----------------|  
|`FileSystemOperation`|Reports the operation that the task performs. The log entry is written when the file system operation starts and includes information about the source and destination.|  
  
## Configuring the File System Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see the following topics:  
  
-   [File System Task Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)  
  
-   [Expressions Page](../expressions/expressions-page.md)  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see the following topic:  
  
-   [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md)  
  
 For more information about how to set these properties programmatically, see the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.FileSystemTask.FileSystemTask>  
  
## Related Tasks  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes a task that downloads and uploads data files and manages directories on servers. For more information, see [FTP Task](ftp-task.md).  
  
## See Also  
 [Integration Services Tasks](integration-services-tasks.md)   
 [Control Flow](control-flow.md)  
  
  
