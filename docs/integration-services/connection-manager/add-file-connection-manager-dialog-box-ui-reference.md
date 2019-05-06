---
title: "Add File Connection Manager Dialog Box UI Reference | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.fileconnection.f1"
helpviewer_keywords: 
  - "Add File Connection Manager"
ms.assetid: 9370bfb5-5993-4ad8-a9cd-2de53f320f34
author: janinezhang
ms.author: janinez
manager: craigg
---
# Add File Connection Manager Dialog Box UI Reference
  Use the **Add File Connection Manager** dialog box to define a connection to a group of files or folders.  
  
 To learn more about the Multiple Files connection manager, see [Multiple Files Connection Manager](../../integration-services/connection-manager/multiple-files-connection-manager.md).  
  
> [!NOTE]  
>  The built-in tasks and data flow components in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] do not use the Multiple Files connection manager. However, you can use this connection manager in the Script task or Script component.  
  
## Options  
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
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)  
  
  
