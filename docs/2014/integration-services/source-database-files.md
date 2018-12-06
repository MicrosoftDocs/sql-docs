---
title: "Source database files | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.transferdatabasetask.sourcedbfiles.f1"
ms.assetid: 7dc6bfeb-37c1-45e8-a705-a87564922265
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Source database files
  Use the **Source Database Files** dialog box to view the database file names and locations on the source server, or to specify a network file share location for the Transfer Database task. For more information about this task, see [Transfer Database Task](control-flow/transfer-database-task.md).  
  
 To populate this dialog box with the database file names and locations on the source server, specify the **SourceConnection** and **SourceDatabaseName** first in the **Databases** page of the **Transfer Database Task Editor** dialog box.  
  
## Options  
 **Source File**  
 Database file names on the source server that will be transferred. **Source File** is read only.  
  
 **Source Folder**  
 Folder on the source server where the database files to be transferred reside. **Source Folder** is read only.  
  
 **Network File Share**  
 Network shared folder on the source server from where the database files will be transferred. Use **Network File Share** when you transfer a database in offline mode by specifying **DatabaseOffline** for **Method** in the **Databases** page of the **Transfer Database Task Editor** dialog box.  
  
 Enter the network file share location, or click the browse button **(...)** to locate the network file share location.  
  
 When you transfer a database in offline mode, the database files are copied to the **Network file share** location on the source server before they are transferred to the destination server.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Transfer Database Task Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Transfer Database Task Editor &#40;Databases Page&#41;](../../2014/integration-services/transfer-database-task-editor-databases-page.md)  
  
  
