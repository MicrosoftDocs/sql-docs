---
title: "File Connection Manager Editor | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.fileconnectionmanager.f1"
helpviewer_keywords: 
  - "File Connection Manager Editor"
ms.assetid: 051c48e5-3d86-49af-b554-ff62e3de3622
author: douglaslms
ms.author: douglasl
manager: craigg
---
# File Connection Manager Editor
  Use the **File Connection Manager Editor** dialog box to specify the properties used to connect to a file or a folder.  
  
> [!NOTE]  
>  You can set the ConnectionString property for the File connection manager by specifying an expression in the Properties window of [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. However, to avoid a validation error when you use an expression to specify the file or folder, in the **File Connection Manager Editor**, for **File/Folder**, add a file or folder path.  
  
 To learn more about the File connection manager, see [File Connection Manager](connection-manager/file-connection-manager.md).  
  
## Options  
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
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)  
  
  
