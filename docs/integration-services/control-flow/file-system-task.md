---
title: "File System Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.filesystemtask.f1"
  - "sql13.dts.designer.filesystemtask.general.f1"
helpviewer_keywords: 
  - "File System task [Integration Services]"
ms.assetid: 7dd79a6a-e066-4028-a385-1d40f31056f8
author: janinezhang
ms.author: janinez
manager: craigg
---
# File System Task

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The File System task performs operations on files and directories in the file system. For example, by using the File System task, a package can create, move, or delete directories and files. You can also use the File System task to set attributes on files and directories. For example, the File System task can make files hidden or read-only.  
  
 All File System task operations use a source, which can be a file or a directory. For example, the file that the task copies or the directory it deletes is a source. The source can be specified by using a File connection manager that points to the directory or file or by providing the name of a variable that contains the source path. For more information, see [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md) and [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md).  
  
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
 The following table describes the custom log entry for the File System task. For more information, see [Integration Services &#40;SSIS&#41; Logging](../../integration-services/performance/integration-services-ssis-logging.md).  
  
|Log entry|Description|  
|---------------|-----------------|  
|**FileSystemOperation**|Reports the operation that the task performs. The log entry is written when the file system operation starts and includes information about the source and destination.|  
  
## Configuring the File System Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see the following topics:  
  
-   [File System Task Editor &#40;General Page&#41;](../../integration-services/control-flow/file-system-task-editor-general-page.md)  
  
-   [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see the following topic:  
  
-   [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b)  
  
 For more information about how to set these properties programmatically, see the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.FileSystemTask.FileSystemTask>  
  
## Related Tasks  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes a task that downloads and uploads data files and manages directories on servers. For more information, see [FTP Task](../../integration-services/control-flow/ftp-task.md).  
  
## File System Task Editor (General Page)
  Use the **General** page of the **File System Task Editor** dialog to configure the file system operation that the task performs.  
  
 You must specify a source and destination connection manager by setting the SourceConnection and DestinationConnection properties. You can either provide the names of File connection managers that point to the files that the task uses as a source or destination, or if the paths of the files are stored in variables, you can provide the names of the variables. To use variables to store the file paths, you must set first set the IsSourcePathVariable option for the source connection and the IsDestinationPatheVariable option for the destination connection to **True**. You can then choose the existing system or user-defined variables to use, or you can create new variables. In the **Add Variable** dialog box, you can configure and specify the scope of the variables. The scope must be the File System task or a parent container. For more information see, [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md) and [Use Variables in Packages](https://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787).  
  
> [!NOTE]  
>  To override the variables you selected for the **SourceConnection** and **DestinationConnection** properties, enter an expression for the **Source** and **Destination** properties. You enter expressions on the **Expressions** page of the **File System Task Editor**. For example, to set the path of the files that the task uses as a destination, you may want to use variable A under certain conditions and use variable B under other conditions.  
  
> [!NOTE]  
>  The File System task operates on a single file or directory. Therefore, this task does not support the use of wildcard characters to perform the same operation on multiple files or directories. To have the File System task repeat an operation on multiple files or directories, put the File System task in a Foreach Loop container. For more information, see [File System Task](../../integration-services/control-flow/file-system-task.md).  
  
 You can use expressions to use different variables for the  
  
### Options  
 **IsDestinationPathVariable**  
 Indicate whether the destination path is stored in a variable. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**True**|The destination path is stored in a variable. Selecting this value displays the dynamic option, **DestinationVariable**.|  
|**False**|The destination path is specified in a File connection manager. Selecting this value displays the dynamic option, **DestinationConnection**.|  
  
 **OverwriteDestination**  
 Specify whether the operation can overwrite files in the destination directory.  
  
 **Name**  
 Provide a unique name for the File System task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the File System task.  
  
 **Operation**  
 Select the file-system operation to perform. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Copy directory**|Copy a directory. Selecting this value displays the dynamic options for a source and destination.|  
|**Copy file**|Copy a file. Selecting this value displays the dynamic options for a source and destination.|  
|**Create directory**|Create a directory. Selecting this value displays the dynamic options for a source and a destination directory.|  
|**Delete directory**|Delete a directory. Selecting this value displays the dynamic options for a source.|  
|**Delete directory content**|Delete the content of a directory. Selecting this value displays the dynamic options for a source.|  
|**Delete file**|Delete a file. Selecting this value displays the dynamic options for a source.|  
|**Move directory**|Move a directory. Selecting this value displays the dynamic options for a source and destination.|  
|**Move file**|Move a file. Selecting this value displays the dynamic options for a source and destination. When moving a file, do not include a file name in the directory path that you provide as the destination.|  
|**Rename file**|Rename a file. Selecting this value displays the dynamic options for a source and destination. When renaming a file, include the new file name in the directory path that you provide for the destination.|  
|**Set attributes**|Set the attributes of a file or directory. Selecting this value displays the dynamic options for a source and operation.|  
  
 **IsSourcePathVariable**  
 Indicate whether the destination path is stored in a variable. This property has the options listed in the following table.  
  
|Value||  
|-----------|-|  
|**True**|The destination path is stored in a variable. Selecting this value displays the dynamic option, **SourceVariable**.|  
|**False**|The destination path is specified in a File connection manager. Selecting this value displays the dynamic option, **DestinationVariable**.|  
  
### IsDestinationPathVariable Dynamic Options  
  
#### IsDestinationPathVariable = True  
 **DestinationVariable**  
 Select the variable name in the list, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), [Add Variable](https://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5)  
  
#### IsDestinationPathVariable = False  
 **DestinationConnection**  
 Select a File connection manager in the list, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../integration-services/connection-manager/file-connection-manager-editor.md)  
  
### IsSourcePathVariable Dynamic Options  
  
#### IsSourcePathVariable = True  
 **SourceVariable**  
 Select the variable name in the list, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), [Add Variable](https://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5)  
  
#### IsSourcePathVariable = False  
 **SourceConnection**  
 Select a File connection manager in the list, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md)  
  
### Operation Dynamic Options  
  
#### Operation = Set Attributes  
 **Hidden**  
 Indicate whether the file or directory is visible.  
  
 **ReadOnly**  
 Indicate whether the file is read-only.  
  
 **Archive**  
 Indicate whether the file or directory is ready for archiving.  
  
 **System**  
 Indicate whether the file is an operating system file.  
  
#### Operation = Create directory  
 **UseDirectoryIfExists**  
 Indicates whether the **Create directory** operation uses an existing directory with the specified name instead of creating a new directory.  
  
## See Also  
 [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md)   
 [Control Flow](../../integration-services/control-flow/control-flow.md)  
  
  
