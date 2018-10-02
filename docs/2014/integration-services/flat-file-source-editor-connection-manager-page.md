---
title: "Flat File Source Editor (Connection Manager Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.flatfilesourceadapter.connection.f1"
helpviewer_keywords: 
  - "Flat File Source Editor"
ms.assetid: 2efd6baa-ed75-4f3f-b667-514024cebdb8
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Flat File Source Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **Flat File Source Editor** dialog box to select the connection manager that the Flat File source will use. The Flat File source reads data from a text file, which can be in a delimited, fixed width, or mixed format.  
  
 A Flat File source can use one of the following types of connection managers:  
  
-   A Flat File connection manager if the source is a single flat file. For more information, see [Flat File Connection Manager](connection-manager/file-connection-manager.md).  
  
-   A Multiple Flat Files connection manager if the source is multiple flat files and the Data Flow task is inside a loop container, such as the For Loop container. On each loop of the container, the Flat File source loads data from the next file name that the Multiple Flat Files connection manager provides. For more information, see [Multiple Flat Files Connection Manager](connection-manager/multiple-flat-files-connection-manager.md).  
  
 To learn more about the Flat File source, see [Flat File Source](data-flow/flat-file-source.md).  
  
## Options  
 **Flat file connection manager**  
 Select an existing connection manager from the list, or create a new connection manager by clicking **New**.  
  
 **New**  
 Create a new connection manager by using the **Flat File Connection Manager Editor** dialog box.  
  
 **Retain null values from the source as null values in the data flow**  
 Specify whether to keep null values when data is extracted. The default value of this property is **false**. When this value is f`alse`, the Flat File source replaces null values from the source data with appropriate default values for each column, such as empty strings for string columns and zero for numeric columns.  
  
 **Preview**  
 Preview results by using the **Data View** dialog box. Preview can display up to 200 rows.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Flat File Source Editor &#40;Columns Page&#41;](../../2014/integration-services/flat-file-source-editor-columns-page.md)   
 [Flat File Source Editor &#40;Error Output Page&#41;](../../2014/integration-services/flat-file-source-editor-error-output-page.md)   
 [Flat File Connection Manager](connection-manager/file-connection-manager.md)  
  
  
