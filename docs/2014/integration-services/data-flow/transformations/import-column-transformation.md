---
title: "Import Column Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.importcolumntrans.f1"
helpviewer_keywords: 
  - "Import Column transformation [Integration Services]"
  - "columns [Integration Services], importing"
  - "importing data, SSIS packages"
  - "inserting data"
ms.assetid: ac3b74c1-ef54-4297-8062-1734324fffcc
author: janinezhang
ms.author: janinez
manager: craigg
---
# Import Column Transformation
  The Import Column transformation reads data from files and adds the data to columns in a data flow. Using this transformation, a package can add text and images stored in separate files to a data flow. For example, a data flow that loads data into a table that stores product information can include the Import Column transformation to import customer reviews of each product from files and add the reviews to the data flow.  
  
 You can configure the Import Column transformation in the following ways:  
  
-   Specify the columns to which the transformation adds data.  
  
-   Specify whether the transformation expects a byte-order mark (BOM).  
  
    > [!NOTE]  
    >  A BOM is only expected if the data has the DT_NTEXT data type.  
  
 A column in the transformation input contains the names of files that hold the data. Each row in the dataset can specify a different file. When the Import Column transformation processes a row, it reads the file name, opens the corresponding file in the file system, and loads the file content into an output column. The data type of the output column must be DT_TEXT, DT_NTEXT, or DT_IMAGE. For more information, see [Integration Services Data Types](../integration-services-data-types.md).  
  
 This transformation has one input, one output, and one error output.  
  
## Configuration of the Import Column Transformation  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../../common-properties.md)  
  
-   [Transformation Custom Properties](transformation-custom-properties.md)  
  
## Related Tasks  
 For information about how to set properties of this component, see [Set the Properties of a Data Flow Component](../set-the-properties-of-a-data-flow-component.md).  
  
## See Also  
 [Export Column Transformation](export-column-transformation.md)   
 [Data Flow](../data-flow.md)   
 [Integration Services Transformations](integration-services-transformations.md)  
  
  
