---
title: "Raw File Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.rawfilesource.f1"
helpviewer_keywords: 
  - "sources [Integration Services], Raw File"
  - "raw data [Integration Services]"
  - "Raw File source"
ms.assetid: 5b4daea5-7f76-4674-aa77-0a79f9f97f7d
author: janinezhang
ms.author: janinez
manager: craigg
---
# Raw File Source
  The Raw File source reads raw data from a file. Because the representation of the data is native to the source, the data requires no translation and almost no parsing. This means that the Raw File source can read data more quickly than other sources such as the Flat File and the OLE DB sources.  
  
 The Raw File source is used to retrieve raw data that was previously written by the Raw File destination. You can also point the Raw File source to an empty raw file that contains only the columns (metadata-only file). You use the Raw File destination to generate the metadata-only file without having to run the package. For more information, see [Raw File Destination](raw-file-destination.md).  
  
 The raw file format contains sort information. The Raw File Destination saves all the sort information including the comparison flags for string columns. The Raw File source reads and honors the sort information. You do have the option of configuring the Raw File Source to ignore the sort flags in the file, using the Advanced Editor. For more information about comparison flags, see [Comparing String Data](comparing-string-data.md).  
  
 You configure the Raw File by specifying the name of the name of the file that the Raw File source reads.  
  
> [!NOTE]  
>  This source does not use a connection manager.  
  
 This source has one output. It does not support an error output.  
  
## Configuration of the Raw File Source  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../common-properties.md)  
  
-   [Raw File Custom Properties](raw-file-custom-properties.md)  
  
## Related Tasks  
 For information about how to set the properties of the component, see [Set the Properties of a Data Flow Component](set-the-properties-of-a-data-flow-component.md).  
  
## Related Content  
  
-   Blog entry, [Raw Files Are Awesome](http://www.sqlservercentral.com/blogs/stratesql/archive/2011/1/1/31-days-of-ssis-_1320_-raw-files-are-awesome-_2800_1_2F00_31_2900_.aspx), on sqlservercentral.com  
  
## See Also  
 [Raw File Destination](raw-file-destination.md)   
 [Data Flow](data-flow.md)  
  
  
