---
title: "File Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "folders [Integration Services], connections"
  - "files [Integration Services], connections"
  - "files [Integration Services]"
  - "connection managers [Integration Services], File"
  - "connections [Integration Services], files"
  - "File connection manager"
ms.assetid: 019078bc-44ee-4975-9169-0f9a89e3f3be
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# File Connection Manager
  A File connection manager enables a package to reference an existing file or folder, or to create a file or folder at run time. For example, you can reference an Excel file. Certain components in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] use information in files to perform their work. For example, an Execute SQL task can reference a file that contains the SQL statements that the task executes. Other components perform operations on files. For example, the File System task can reference a file to copy it to a new location.  
  
## Usage Types of the File Connection Manager  
 The `FileUsageType` property of the File connection manager specifies how the file connection is used. The File connection manager can create a file, create a folder, use an existing file, or use an existing folder.  
  
 The following table lists the values of `FileUsageType`.  
  
|Value|Description|  
|-----------|-----------------|  
|`0`|File connection manager uses an existing file.|  
|`1`|File connection manager creates a file.|  
|`2`|File connection manager uses an existing folder.|  
|`3`|File connection manager creates a folder.|  
  
## Multiple File or Folder Connections  
 The File connection manager can reference only one file or folder. To reference multiple files or folders, use a Multiple Files connection manager instead of a File connection manager. For more information, see [Multiple Files Connection Manager](multiple-files-connection-manager.md).  
  
## Configuration of the File Connection Manager  
 When you add a File connection manager to a package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that will resolve to a File connection at run time, sets the File connection properties, and adds the File connection to the `Connections` collection of the package.  
  
 The `ConnectionManagerType` property of the connection manager is set to `FILE`.  
  
 You can configure a File connection manager in the following ways:  
  
-   Specify the usage type.  
  
-   Specify a file or folder.  
  
 You can set the ConnectionString property for the File connection manager by specifying an expression in the Properties window of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. However, to avoid a validation error when you use an expression to specify the file or folder, in the **File Connection Manager Editor**, for **File/Folder**, add a file or folder path.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [File Connection Manager Editor](../file-connection-manager-editor.md).  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../building-packages-programmatically/adding-connections-programmatically.md).  
  
  
