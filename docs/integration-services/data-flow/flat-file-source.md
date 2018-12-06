---
title: "Flat File Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.flatfilesource.f1"
  - "sql13.dts.designer.flatfilesourceadapter.connection.f1"
  - "sql13.dts.designer.flatfilesourceadapter.columns.f1"
  - "sql13.dts.designer.flatfilesourceadapter.errorhandling.f1"
helpviewer_keywords: 
  - "sources [Integration Services], Flat File"
  - "text file reading [Integration Services]"
  - "flat files"
  - "Flat File source"
ms.assetid: 4a64f7f3-f25d-4db0-93b3-a29496030e58
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Flat File Source
  The Flat File source reads data from a text file. The text file can be in delimited, fixed width, or mixed format.  
  
-   Delimited format uses column and row delimiters to define columns and rows.  
  
-   Fixed width format uses width to define columns and rows. This format also includes a character for padding fields to their maximum width.  
  
-   Ragged right format uses width to define all columns, except for the last column, which is delimited by the row delimiter.  
  
 You can configure the Flat File source in the following ways:  
  
-   Add a column to the transformation output that contains the name of the text file from which the Flat File source extracts data.  
  
-   Specify whether the Flat File source interprets zero-length strings in columns as null values.  
  
    > [!NOTE]  
    >  The Flat File connection manager that the Flat File source uses must be configured to use a delimited format to interpret zero-length strings as nulls. If the connection manager uses the fixed width or ragged right formats, data that consists of spaces cannot be interpreted as null values.  
  
 The output columns in the output of the Flat File source include the FastParse property. FastParse indicates whether the column uses the quicker, but locale-insensitive, fast parsing routines that [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides or the locale-sensitive standard parsing routines. For more information, see [Fast Parse](https://msdn.microsoft.com/library/6688707d-3c5b-404e-aa2f-e13092ac8d95) and [Standard Parse](https://msdn.microsoft.com/library/dfe835b1-ea52-4e18-a23a-5188c5b6f013).  
  
 Output columns also include the UseBinaryFormat property. You use this property to implement support for binary data, such as data with the packed decimal format, in files. By default UseBinaryFormat is set to **false**. If you want to use a binary format, set UseBinaryFormat to **true** and the data type on the output column to **DT_BYTES**. When you do this, the Flat File source skips the data conversion and passes the data to the output column as is. You can then use a transformation such as the Derived Column or Data Conversion to cast the **DT_BYTES** data to a different data type, or you can write custom script in a Script transformation to interpret the data. You can also write a custom data flow component to interpret the data. For more information about which data types you can cast **DT_BYTES** to, see [Cast &#40;SSIS Expression&#41;](../../integration-services/expressions/cast-ssis-expression.md).  
  
 This source uses a Flat File connection manager to access the text file. By setting properties on the Flat File connection manager, you can provide information about the file and each column in it, and specify how the Flat File source should handle the data in the text file. For example, you can specify the characters that delimit columns and rows in the file, and the data type and the length of each column. For more information, see [Flat File Connection Manager](../../integration-services/connection-manager/flat-file-connection-manager.md).  
  
 This source has one output and one error output.  
  
## Configuration of the Flat File Source  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [Flat File Custom Properties](../../integration-services/data-flow/flat-file-custom-properties.md)  
  
## Related Tasks  
 For details about how to set properties of a data flow component, see [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## Flat File Source Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **Flat File Source Editor** dialog box to select the connection manager that the Flat File source will use. The Flat File source reads data from a text file, which can be in a delimited, fixed width, or mixed format.  
  
 A Flat File source can use one of the following types of connection managers:  
  
-   A Flat File connection manager if the source is a single flat file. For more information, see [Flat File Connection Manager](../../integration-services/connection-manager/flat-file-connection-manager.md).  
  
-   A Multiple Flat Files connection manager if the source is multiple flat files and the Data Flow task is inside a loop container, such as the For Loop container. On each loop of the container, the Flat File source loads data from the next file name that the Multiple Flat Files connection manager provides. For more information, see [Multiple Flat Files Connection Manager](../../integration-services/connection-manager/multiple-flat-files-connection-manager.md).  
  
### Options  
 **Flat file connection manager**  
 Select an existing connection manager from the list, or create a new connection manager by clicking **New**.  
  
 **New**  
 Create a new connection manager by using the **Flat File Connection Manager Editor** dialog box.  
  
 **Retain null values from the source as null values in the data flow**  
 Specify whether to keep null values when data is extracted. The default value of this property is **false**. When this value is f**alse**, the Flat File source replaces null values from the source data with appropriate default values for each column, such as empty strings for string columns and zero for numeric columns.  
  
 **Preview**  
 Preview results by using the **Data View** dialog box. Preview can display up to 200 rows.  
  
## Flat File Source Editor (Columns Page)
  Use the **Columns** node of the **Flat File Source Editor** dialog box to map an output column to each external (source) column.  
  
> [!NOTE]  
>  The **FileNameColumnName** property of the Flat File source and the **FastParse** property of its output columns are not available in the **Flat File Source Editor**, but can be set by using the **Advanced Editor**. For more information on these properties, see the Flat File Source section of [Flat File Custom Properties](../../integration-services/data-flow/flat-file-custom-properties.md).  
  
### Options  
 **Available External Columns**  
 View the list of available external columns in the data source. You cannot use this table to add or delete columns.  
  
 **External Column**  
 View external (source) columns in the order in which the task will read them. You can change this order by first clearing the selected columns in the table, and then selecting external columns from the list in a different order.  
  
 **Output Column**  
 Provide a unique name for each output column. The default is the name of the selected external (source) column; however, you can choose any unique, descriptive name. The name provided will be displayed within [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
## Flat File Source Editor (Error Output Page)
  Use the **Error Output** page of the **Flat File Source Editor** dialog box to select error-handling options and to set properties on error output columns.\  
  
### Options  
 **Input/Output**  
 View the name of the data source.  
  
 **Column**  
 View the external (source) columns that you selected on the **Connection Manager** page of the **Flat File Source Editor**dialog box.  
  
 **Error**  
 Specify what should happen when an error occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Related Topics:** [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md)  
  
 **Truncation**  
 Specify what should happen when a truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Description**  
 View the description of the error.  
  
 **Set this value to selected cells**  
 Specify what should happen to all the selected cells when an error or truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Apply**  
 Apply the error handling option to the selected cells.  
  
## See Also  
 [Flat File Destination](../../integration-services/data-flow/flat-file-destination.md)   
 [Data Flow](../../integration-services/data-flow/data-flow.md)  
  
  
