---
title: "File Connection Manager Editor | Microsoft Docs"
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
  - "sql13.dts.designer.fileconnectionmanager.f1"
helpviewer_keywords: 
  - "File Connection Manager Editor"
ms.assetid: 051c48e5-3d86-49af-b554-ff62e3de3622
caps.latest.revision: 22
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# File Connection Manager Editor
  Use the **File Connection Manager Editor** dialog box to specify the properties used to connect to a file or a folder.  
  
> [!NOTE]  
>  You can set the ConnectionString property for the File connection manager by specifying an expression in the Properties window of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. However, to avoid a validation error when you use an expression to specify the file or folder, in the **File Connection Manager Editor**, for **File/Folder**, add a file or folder path.  
  
 To learn more about the File connection manager, see [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md).  
  
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
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)  
  
  