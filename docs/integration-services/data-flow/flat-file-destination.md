---
title: "Flat File Destination | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.flatfiledest.f1"
  - "sql13.dts.designer.flatfiledestadapter.connection.f1"
  - "sql13.dts.designer.flatfiledestadapter.mappings.f1"
helpviewer_keywords: 
  - "flat files"
  - "Flat File destination"
  - "text file writing [Integration Services]"
  - "destinations [Integration Services], Flat File"
ms.assetid: e0d6e356-8db4-48aa-ba66-029397f98f61
author: janinezhang
ms.author: janinez
manager: craigg
---
# Flat File Destination
  The Flat File destination writes data to a text file. The text file can be in delimited, fixed width, fixed width with row delimiter, or ragged right format.  
  
 You can configure the Flat File destination in the following ways:  
  
-   Provide a block of text that is inserted in the file before any data is written. The text can provide information such as column headings.  
  
-   Specify whether to overwrite a data in a destination file that has the same name.  
  
 This destination uses a Flat File connection manager to access the text file. By setting properties on the Flat File connection manager that the Flat File destination uses, you can specify how the Flat File destination formats and writes the text file. When you configure the Flat File connection manager, you specify information about the file and about each column in the file. For example, you specify the characters that delimit columns and rows in the file, and you specify the data type and the length of each column. For more information, see [Flat File Connection Manager](../../integration-services/connection-manager/flat-file-connection-manager.md).  
  
 The Flat File destination includes the Header custom property. This property can be updated by a property expression when the package is loaded. For more information, see [Integration Services &#40;SSIS&#41; Expressions](../../integration-services/expressions/integration-services-ssis-expressions.md), [Use Property Expressions in Packages](../../integration-services/expressions/use-property-expressions-in-packages.md), and [Flat File Custom Properties](../../integration-services/data-flow/flat-file-custom-properties.md).  
  
 This destination has one output. It does not support an error output.  
  
## Configuration of the Flat File Destination  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [Flat File Custom Properties](../../integration-services/data-flow/flat-file-custom-properties.md)  
  
## Related Tasks  
 For information about how to set the properties of a data flow component, see [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## Flat File Destination Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **Flat File Destination Editor** dialog box to select the flat file connection for the destination, and to specify whether to overwrite or append to the existing destination file. The flat file destination writes data to a text file. This text file can be in delimited, fixed width, fixed width with row delimiter, or ragged right format.  
  
### Options  
 **Flat File connection manager**  
 Select an existing connection manager by using the list box, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection by using the **Flat File Format** and **Flat File Connection Manager Editor** dialog boxes.  
  
 In addition to the standard flat file formats of delimited, fixed width, and ragged right, the **Flat File Format** dialog box has a fourth option, **Fixed width with row delimiters**. This option represents a special case of the ragged-right format in which [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] adds a dummy column as the final column of data. This dummy column ensures that the final column has a fixed width.  
  
 The **Fixed width with row delimiters** option is not available in the **Flat File Connection Manager Editor**. If necessary, you can emulate this option in the editor. To emulate this option, on the **General** page of the **Flat File Connection Manager Editor**, for **Format**, select **Ragged right**. Then on the **Advanced** page of the editor, add a new dummy column as the final column of data.  
  
 **Overwrite data in the file**  
 Indicate whether to overwrite an existing file, or to append data to it.  
  
 **Header**  
 Type a block of text to insert into the file before any data is written. Use this option to include additional information, such as column headings.  
  
 **Preview**  
 Preview results by using the **Data View** dialog box. Preview can display up to 200 rows.  
  
## Flat File Destination Editor (Mappings Page)
  Use the **Mappings** page of the **Flat File Destination Editor** dialog box to map input columns to destination columns.  
  
### Options  
 **Available Input Columns**  
 View the list of available input columns. Use a drag-and-drop operation to map available input columns to destination columns.  
  
 **Available Destination Columns**  
 View the list of available destination columns. Use a drag-and-drop operation to map available destination columns to input columns.  
  
 **Input Column**  
 View input columns selected earlier in this topic. You can change the mappings by using the list of **Available Input Columns**. Select **\<ignore>** to exclude the column from the output.  
  
 **Destination Column**  
 View each available destination column, whether it is mapped or not.  
  
## See Also  
 [Flat File Source](../../integration-services/data-flow/flat-file-source.md)   
 [Data Flow](../../integration-services/data-flow/data-flow.md)  
  
  
