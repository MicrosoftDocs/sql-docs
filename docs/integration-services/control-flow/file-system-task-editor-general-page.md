---
title: "File System Task Editor (General Page) | Microsoft Docs"
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
  - "sql13.dts.designer.filesystemtask.general.f1"
helpviewer_keywords: 
  - "File System Task Editor"
ms.assetid: 51fe6614-3418-4eff-a28d-02ea31cc9aa9
caps.latest.revision: 43
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# File System Task Editor (General Page)
  Use the **General** page of the **File System Task Editor** dialog to configure the file system operation that the task performs.  
  
 To learn about this task, see [File System Task](../../integration-services/control-flow/file-system-task.md).  
  
 You must specify a source and destination connection manager by setting the SourceConnection and DestinationConnection properties. You can either provide the names of File connection managers that point to the files that the task uses as a source or destination, or if the paths of the files are stored in variables, you can provide the names of the variables. To use variables to store the file paths, you must set first set the IsSourcePathVariable option for the source connection and the IsDestinationPatheVariable option for the destination connection to **True**. You can then choose the existing system or user-defined variables to use, or you can create new variables. In the **Add Variable** dialog box, you can configure and specify the scope of the variables. The scope must be the File System task or a parent container. For more information see, [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md) and [Use Variables in Packages](http://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787).  
  
> [!NOTE]  
>  To override the variables you selected for the **SourceConnection** and **DestinationConnection** properties, enter an expression for the **Source** and **Destination** properties. You enter expressions on the **Expressions** page of the **File System Task Editor**. For example, to set the path of the files that the task uses as a destination, you may want to use variable A under certain conditions and use variable B under other conditions.  
  
> [!NOTE]  
>  The File System task operates on a single file or directory. Therefore, this task does not support the use of wildcard characters to perform the same operation on multiple files or directories. To have the File System task repeat an operation on multiple files or directories, put the File System task in a Foreach Loop container. For more information, see [File System Task](../../integration-services/control-flow/file-system-task.md).  
  
 You can use expressions to use different variables for the  
  
## Options  
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
  
## IsDestinationPathVariable Dynamic Options  
  
### IsDestinationPathVariable = True  
 **DestinationVariable**  
 Select the variable name in the list, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), [Add Variable](http://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5)  
  
### IsDestinationPathVariable = False  
 **DestinationConnection**  
 Select a File connection manager in the list, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../integration-services/connection-manager/file-connection-manager-editor.md)  
  
## IsSourcePathVariable Dynamic Options  
  
### IsSourcePathVariable = True  
 **SourceVariable**  
 Select the variable name in the list, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), [Add Variable](http://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5)  
  
### IsSourcePathVariable = False  
 **SourceConnection**  
 Select a File connection manager in the list, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../integration-services/connection-manager/file-connection-manager-editor.md)  
  
## Operation Dynamic Options  
  
### Operation = Set Attributes  
 **Hidden**  
 Indicate whether the file or directory is visible.  
  
 **ReadOnly**  
 Indicate whether the file is read-only.  
  
 **Archive**  
 Indicate whether the file or directory is ready for archiving.  
  
 **System**  
 Indicate whether the file is an operating system file.  
  
### Operation = Create directory  
 **UseDirectoryIfExists**  
 Indicates whether the **Create directory** operation uses an existing directory with the specified name instead of creating a new directory.  
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)   
 [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
  