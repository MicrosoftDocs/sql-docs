---
title: "Multiple Files Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "folders [Integration Services], connections"
  - "files [Integration Services], connections"
  - "Multiple Files connection manager"
  - "connection managers [Integration Services], Multiple Files"
  - "connections [Integration Services], files"
  - "multiple file connections"
ms.assetid: 10bdc56e-c5cd-4ddb-b2f7-375fe57fe8b2
author: janinezhang
ms.author: janinez
manager: craigg
---
# Multiple Files Connection Manager

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  A Multiple Files connection manager enables a package to reference existing files and folders, or to create files and folders at run time.  
  
> [!NOTE]  
>  The built-in tasks and data flow components in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] do not use the Multiple Files connection manager. However, you can use this connection manager in the Script task or Script component. For information about how to use connection managers in the Script task, see [Connecting to Data Sources in the Script Task](../../integration-services/extending-packages-scripting/task/connecting-to-data-sources-in-the-script-task.md). For information about how to use connection managers in the Script component, see [Connecting to Data Sources in the Script Component](../../integration-services/extending-packages-scripting/data-flow-script-component/connecting-to-data-sources-in-the-script-component.md).  
  
## Usage Types of the Multiple Files Connection Manager  
 The **FileUsageType** property of the Multiple Files connection manager specifies how the connection is used. The Multiple Files connection manager can create files, create folders, use existing files, and use existing folders.  
  
 The following table lists the values of **FileUsageType**.  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|Multiple Files connection manager uses an existing file.|  
|**1**|Multiple Files connection manager creates a file.|  
|**2**|Multiple Files connection manager uses an existing folder.|  
|**3**|Multiple Files connection manager creates a folder.|  
  
## Configuration of the Multiple Files Connection Manager  
 When you add a Multiple Files connection manager to a package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that will resolve to a Multiple Files connection at run time, sets the Multiple Files connection properties, and adds the Multiple Files connection to the **Connections** collection of the package.  
  
 The **ConnectionManagerType** property of the connection manager is set to **MULTIFILE**.  
  
 You can configure a Multiple Files connection manager in the following ways:  
  
-   Specify the usage type of files and folders.  
  
-   Specify files and folders.  
  
-   If using multiple files or folders, specify the order in which the files and folders are accessed.  
  
 If the Multiple Files connection manager references multiple files and folders, the paths of the files and folders are separated by the pipe (|) character. The **ConnectionString** property of the connection manager has the following format:  
  
 \<*path*>|\<*path*>  
  
 You can also specify multiple files or folders using wildcard characters. For example, to reference all the text files on the C drive, the value of the **ConnectionString** property can be set to C:\\*.txt.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [Add File Connection Manager Dialog Box UI Reference](../../integration-services/connection-manager/add-file-connection-manager-dialog-box-ui-reference.md).  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../../integration-services/building-packages-programmatically/adding-connections-programmatically.md).  
  
  
