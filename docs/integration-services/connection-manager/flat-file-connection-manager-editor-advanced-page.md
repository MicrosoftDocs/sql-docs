---
title: "Flat File Connection Manager Editor (Advanced Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.ffileconnection.columnproperties.f1"
helpviewer_keywords: 
  - "Flat File Connection Manager Editor"
ms.assetid: 58aa3dee-4774-4e0b-a956-96d199be4c3a
caps.latest.revision: 36
author: "v-thepet"
ms.author: "douglasl"
manager: "jhubbard"
---
# Flat File Connection Manager Editor (Advanced Page)
  Use the **Advanced** page of the **Flat File Connection Manager Editor** dialog box to set properties that specify how Integration Services reads and writes data in flat files. You can change the names of columns in the flat file, and set properties that include data type and delimiters for each column in the file.  
  
 By default, the length of string columns is 50 characters. You can resize the length of these columns to prevent truncation of data or excess column width. You can also update other metadata to enable compatibility with destination columns. For example, you might change the data type of a column that contains only integer data to a numeric data type, such as DT_I2. You can make these modifications manually, or you can click the **Select Types** button to use the **Suggest Column Types** dialog box to evaluate sample data and make some of these changes for you automatically.  
  
 To learn more about the Flat File connection manager, see [Flat File Connection Manager](../../integration-services/connection-manager/flat-file-connection-manager.md).  
  
## Options  
 **Connection manager name**  
 Provide a unique name for the flat file connection manager in the workflow. The name provided will be displayed within [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 **Description**  
 Describe the connection manager. As a best practice, describe the connection manager in terms of its purpose, to make packages self-documenting and easier to maintain.  
  
 **Configure the properties of each column**  
 Select a column in the left pane to view its properties in the right pane. See the following table for a description of data type properties. Some of the properties listed are configurable only for some flat file formats.  
  
|Property|Description|  
|--------------|-----------------|  
|**ColumnType**|Denotes whether the column is delimited, fixed width, or ragged right. This property is read-only. Ragged right files are files in which every column has a fixed width, except for the last column. It is delimited by the row delimiter.|  
|**OutputColumnWidth**|Specify a value to be stored as a count of bytes; for Unicode files, this value corresponds to a count of characters. In the Data Flow task, this value is used to set the output column width for the Flat File source. In the object model, the name of this property is MaximumWidth.|  
|**DataType**|Select from the list of available data types. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).|  
|**TextQualified**|Indicate whether text data is surrounded by text qualifier characters such as quote characters.<br /><br /> True: Text data in the flat file is qualified. False: Text data in the flat file is NOT qualified.|  
|**Name**|Provide a descriptive column name. If you do not enter a name, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] automatically creates a name in the format Column 0, Column 1 and so forth.|  
|**DataScale**|Specify the scale of numeric data. Scale refers to the number of decimal places. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).|  
|**ColumnDelimiter**|Select from the list of available column delimiters. Choose delimiters that are not likely to occur in the text. This value is ignored for fixed-width columns.<br /><br /> **{CR}{LF}**. Columns are delimited by a carriage return-line feed combination.<br /><br /> **{CR}**. Columns are delimited by a carriage return.<br /><br /> **{LF}**. Columns are delimited by a line feed.<br /><br /> **Semicolon {;}**. Columns are delimited by a semicolon.<br /><br /> **Colon {:}**. Columns are delimited by a colon.<br /><br /> **Comma {,}**. Columns are delimited by a comma.<br /><br /> **Tab {t}**. Columns are delimited by a tab.<br /><br /> **Vertical bar {&#124;}**. Columns are delimited by a vertical bar.|  
|**DataPrecision**|Specify the precision of numeric data. Precision refers to the number of digits. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).|  
|**InputColumnWidth**|Specify a value to be stored as a count of bytes; for Unicode files, this will display as a count of characters. This value is ignored for delimited columns.<br /><br /> **Note** In the object model, the name of this property is ColumnWidth.|  
  
 **New**  
 Add a new column by clicking **New**. By default, the **New** button adds a new column at the end of the list. The button also has the following options, available in the drop-down list.  
  
|Value|Description|  
|-----------|-----------------|  
|**Add Column**|Add a new column at the end of the list.|  
|**Insert Before**|Insert a new column before the selected column.|  
|**Insert After**|Insert a new column after the selected column.|  
  
 **Delete**  
 Select a column, and then remove it by clicking **Delete**.  
  
 **Suggest Types**  
 Use the **Suggest Column Types** dialog box to evaluate sample data in the file and to obtain suggestions for the data type and length of each column. For more information, see [Suggest Column Types Dialog Box UI Reference](../../integration-services/connection-manager/suggest-column-types-dialog-box-ui-reference.md).  
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)   
 [Flat File Connection Manager Editor &#40;General Page&#41;](../../integration-services/connection-manager/flat-file-connection-manager-editor-general-page.md)   
 [Flat File Connection Manager Editor &#40;Columns Page&#41;](../../integration-services/connection-manager/flat-file-connection-manager-editor-columns-page.md)   
 [Flat File Connection Manager Editor &#40;Preview Page&#41;](../../integration-services/connection-manager/flat-file-connection-manager-editor-preview-page.md)  
  
  