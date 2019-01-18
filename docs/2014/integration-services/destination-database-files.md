---
title: "Destination Database Files | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.transferdatabasetask.destdbfiles.f1"
ms.assetid: f6f90417-86fb-4b8c-a790-0b215c344ef6
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Destination Database Files
  Use the **Destination Database Files** dialog box to view or change the database file names and locations on the destination server, or to specify a network file location for the Transfer Database task. For more information about this task, see [Transfer Database Task](control-flow/transfer-database-task.md).  
  
 To automatically populate this dialog box with the database file names and locations on the source server, specify the **SourceConnection**, **SourceDatabaseName**, and **SourceDatabaseFiles** first in the **Databases** page of the **Transfer Database Task Editor** dialog box.  
  
## Options  
 **Destination File**  
 Names of the transferred database files on the destination server.  
  
 Enter the file name, or click the file name to edit it.  
  
 **Destination Folder**  
 Folder on the destination server where the database files will be transferred to.  
  
 Enter the folder path, click the folder path to edit it, or click browse to locate the folder where you want to transfer the database files on the destination server.  
  
 **Network File Share**  
 Network shared folder on the destination server where the database files will be transferred to. Use **Network file share** when you transfer a database in offline mode by specifying **DatabaseOffline** for **Method** in the **Databases** page of the **Transfer Database Task Editor** dialog box.  
  
 Enter the network file share location, or click browse to locate the network file share location.  
  
 When you transfer a database in offline mode, the database files are copied to the **Network file share** location before they are transferred to the **Destination folder** location.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Transfer Database Task Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Transfer Database Task Editor &#40;Databases Page&#41;](../../2014/integration-services/transfer-database-task-editor-databases-page.md)  
  
  
