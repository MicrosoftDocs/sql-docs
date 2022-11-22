---
description: "Raw File Source"
title: "Raw File Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.rawfilesource.f1"
  - "sql13.dts.designer.rawfilesourceconnectionmanager.f1"
  - "sql13.dts.designer.rawfilesourcecolumns.f1"
helpviewer_keywords: 
  - "sources [Integration Services], Raw File"
  - "raw data [Integration Services]"
  - "Raw File source"
ms.assetid: 5b4daea5-7f76-4674-aa77-0a79f9f97f7d
author: chugugrace
ms.author: chugu
---
# Raw File Source

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  The Raw File source reads raw data from a file. Because the representation of the data is native to the source, the data requires no translation and almost no parsing. This means that the Raw File source can read data more quickly than other sources such as the Flat File and the OLE DB sources.  
  
 The Raw File source is used to retrieve raw data that was previously written by the Raw File destination. You can also point the Raw File source to an empty raw file that contains only the columns (metadata-only file). You use the Raw File destination to generate the metadata-only file without having to run the package. For more information, see [Raw File Destination](../../integration-services/data-flow/raw-file-destination.md).  
  
 The raw file format contains sort information. The Raw File Destination saves all the sort information including the comparison flags for string columns. The Raw File source reads and honors the sort information. You do have the option of configuring the Raw File Source to ignore the sort flags in the file, using the Advanced Editor. For more information about comparison flags, see [Comparing String Data](../../integration-services/data-flow/comparing-string-data.md).  
  
 You configure the Raw File by specifying the name of the name of the file that the Raw File source reads.  
  
> [!NOTE]  
>  This source does not use a connection manager.  
  
 This source has one output. It does not support an error output.  
  
## Configuration of the Raw File Source  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](./set-the-properties-of-a-data-flow-component.md)  
  
-   [Raw File Custom Properties](../../integration-services/data-flow/raw-file-custom-properties.md)  
  
## Related Tasks  
 For information about how to set the properties of the component, see [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## Related Content  
  
-   Blog entry, [Raw Files Are Awesome](https://www.sqlservercentral.com/blogs/31-days-of-ssis-%e2%80%93-raw-files-are-awesome-131), on sqlservercentral.com  
  
## Raw File Source Editor (Connection Manager Page)
  The Raw File source reads raw data from a file. Because the representation of the data is native to the source, the data requires no translation and almost no parsing.   
## Raw File Source Editor (Columns Page)
  The Raw File source reads raw data from a file. Because the representation of the data is native to the source, the data requires no translation and almost no parsing.   
## See Also  
 [Raw File Destination](../../integration-services/data-flow/raw-file-destination.md)   
 [Data Flow](../../integration-services/data-flow/data-flow.md)  
  
