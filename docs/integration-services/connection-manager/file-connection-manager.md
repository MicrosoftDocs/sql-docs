---
title: "File Connection Manager"
description: "File Connection Manager"
author: chugugrace
ms.author: chugu
ms.date: "03/29/2024"
ms.service: sql
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords:
  - "sql13.dts.designer.fileconnectionmanager.f1"
  - "sql13.dts.designer.suggestdatatypes.f1"
  - "sql13.dts.designer.fileconnection.f1"
helpviewer_keywords:
  - "folders [Integration Services], connections"
  - "files [Integration Services], connections"
  - "files [Integration Services]"
  - "connection managers [Integration Services], File"
  - "connections [Integration Services], files"
  - "File connection manager"
---
# File Connection Manager

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  A File connection manager enables a package to reference an existing file or folder, or to create a file or folder at run time. For example, you can reference an Excel file. Certain components in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] use information in files to perform their work. For example, an Execute SQL task can reference a file that contains the SQL statements that the task executes. Other components perform operations on files. For example, the File System task can reference a file to copy it to a new location.  
  
## Usage Types of the File Connection Manager  
 The **FileUsageType** property of the File connection manager specifies how the file connection is used. The File connection manager can create a file, create a folder, use an existing file, or use an existing folder.  
  
 The following table lists the values of **FileUsageType**.  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|File connection manager uses an existing file.|  
|**1**|File connection manager creates a file.|  
|**2**|File connection manager uses an existing folder.|  
|**3**|File connection manager creates a folder.|  
  
## Multiple File or Folder Connections  
 The File connection manager can reference only one file or folder. To reference multiple files or folders, use a Multiple Files connection manager instead of a File connection manager. For more information, see [Multiple Files Connection Manager](../../integration-services/connection-manager/multiple-files-connection-manager.md).  
  
## Configuration of the File Connection Manager  
 When you add a File connection manager to a package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that will resolve to a File connection at run time, sets the File connection properties, and adds the File connection to the **Connections** collection of the package.  
  
 The **ConnectionManagerType** property of the connection manager is set to **FILE**.  
  
 You can configure a File connection manager in the following ways:  
  
-   Specify the usage type.  
  
-   Specify a file or folder.  
  
 You can set the ConnectionString property for the File connection manager by specifying an expression in the Properties window of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. However, to avoid a validation error when you use an expression to specify the file or folder, in the **File Connection Manager Editor**, for **File/Folder**, add a file or folder path.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [File Connection Manager Editor]().  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../../integration-services/building-packages-programmatically/adding-connections-programmatically.md).  
  
## File Connection Manager Editor
  Use the **File Connection Manager Editor** dialog box to specify the properties used to connect to a file or a folder.  
  
> [!NOTE]  
>  You can set the ConnectionString property for the File connection manager by specifying an expression in the Properties window of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. However, to avoid a validation error when you use an expression to specify the file or folder, in the **File Connection Manager Editor**, for **File/Folder**, add a file or folder path.  
  
 To learn more about the File connection manager, see [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md).  
  
### Options  
 **Usage Type**  
 Specify whether the **File Connection Manager** connects to an existing file or folder or creates a new file or folder.  
  
|Value|Description|  
|-----------|-----------------|  
|Create file|Create a new file at run time.|  
|Existing file|Use an existing file.|  
|Create folder|Create a new folder at run time.|  
|Existing folder|Use an existing folder.|  
  
 **File / Folder**  
 If **File**, specify the file to use.  
  
 If **Folder**, specify the folder to use.  
  
 **Browse**  
 Select the file or folder by using the **Select File** or **Browse for Folder** dialog box.  
  
## Add File Connection Manager Dialog Box UI Reference

  Use the **Add File Connection Manager** dialog box to define a connection to a group of files or folders.  
  
 To learn more about the Multiple Files connection manager, see [Multiple Files Connection Manager](../../integration-services/connection-manager/multiple-files-connection-manager.md).  
  
> [!NOTE]  
>  The built-in tasks and data flow components in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] do not use the Multiple Files connection manager. However, you can use this connection manager in the Script task or Script component.

### Dialog box options for Add File Connection Manager

 **Usage type**  
 Specify the type of files to use for the multiple files connection manager.  
  
|Value|Description|  
|-----------|-----------------|  
|**Create files**|The connection manager will create the files.|  
|**Existing files**|The connection manager will use existing files.|  
|**Create folders**|The connection manager will create the folders.|  
|**Existing folders**|The connection manager will use existing folders.|  
  
 **Files / Folders**  
 View the files or folders that you have added by using the buttons described as follows.  
  
 **Add**  
 Add a file by using the **Select Files** dialog box, or add a folder by using the **Browse for Folder** dialog box.  
  
 **Edit**  
 Select a file or folder, and then replace it with a different file or folder by using the **Select Files** or **Browse for Folder** dialog box.  
  
 **Remove**  
 Select a file or folder, and then remove it from the list by using the **Remove** button.  
  
 **Arrow buttons**  
 Select a file or folder, and then use the arrow buttons to move it up or down to specify the sequence of access.

## Suggest Column Types Dialog Box UI Reference

  Use the **Suggest Column Types** dialog box to identify the data type and length of columns in a Flat File Connection Manager based on a sampling of the file content.  
  
 To learn more about the data types used by [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).

### Dialog box options for Suggest Column Types

 **Number of rows**  
 Type or select the number of rows in the sample that the algorithm uses.  
  
 **Suggest the smallest integer data type**  
 Clear this check box to skip the assessment. If selected, determines the smallest possible integer data type for columns that contain integral numeric data.  
  
 **Suggest the smallest real data type**  
 Clear this check box to skip the assessment. If selected, determines whether columns that contain real numeric data can use the smaller real data type, DT_R4.  
  
 **Identify Boolean columns using the following values**  
 Type the two values that you want to use as the Boolean values true and false. The values must be separated by a comma, and the first value represents True.  
  
 **Pad string columns**  
 Select this check box to enable string padding.  
  
 **Percent padding**  
 Type or select the percentage of the column lengths by which to increase the length of columns for character data types. The percentage must be an integer.

## Related content

 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)
