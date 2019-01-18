---
title: "Export Column Transformation Editor (Columns Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.fileextractortransformation.columns.f1"
helpviewer_keywords: 
  - "Export Column Transformation Editor"
ms.assetid: b659a5c2-5509-4b5b-9c82-136c971d5c7f
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Export Column Transformation Editor (Columns Page)
  Use the **Columns** page of the **Export Column Transformation Editor** dialog box to specify columns in the data flow to extract to files. You can specify whether the Export Column transformation appends data to a file or overwrites an existing file.  
  
 To learn more about the Export Column transformation, see [Export Column Transformation](data-flow/transformations/export-column-transformation.md).  
  
## Options  
 **Extract Column**  
 Select from the list of input columns that contain text or image data. All rows should have definitions for **Extract Column** and **File Path Column**.  
  
 **File Path Column**  
 Select from the list of input columns that contain file paths and file names. All rows should have definitions for **Extract Column** and **File Path Column**.  
  
 **Allow Append**  
 Specify whether the transformation appends data to existing files. The default is `false`.  
  
 **Force Truncate**  
 Specify whether the transformation deletes the contents of existing files before writing data. The default is `false`.  
  
 **Write BOM**  
 Specify whether to write a byte-order mark (BOM) to the file. A BOM is only written if the data has the `DT_NTEXT` or DT_WSTR data type and is not appended to an existing data file.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Export Column Transformation Editor &#40;Error Output Page&#41;](../../2014/integration-services/export-column-transformation-editor-error-output-page.md)  
  
  
