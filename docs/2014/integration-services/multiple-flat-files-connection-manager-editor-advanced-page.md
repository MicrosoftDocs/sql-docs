---
title: "Multiple Flat Files Connection Manager Editor (Advanced Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.multifile.advanced.f1"
helpviewer_keywords: 
  - "Multiple Flat Files Connection Manager Editor"
ms.assetid: fc883131-c03d-4ab3-8220-b51cbe243a82
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Multiple Flat Files Connection Manager Editor (Advanced Page)
  Use the **Advanced** page of the **Multiple Flat Files Connection ManagerEditor** dialog box to set properties such as data type and delimiters of each column in the text files to which the flat file connection manager connects.  
  
 By default, the length of string columns is 50 characters. You can evaluate sample data and automatically resize the length of these columns to prevent truncation of data or excess column width. You can also update other metadata to enable compatibility with destination columns. For example, you might change the data type of a column that contains only integer data to a numeric data type, such as DT_I2.  
  
 To learn more about the Multiple Flat Files connection manager, see [Multiple Flat Files Connection Manager](connection-manager/multiple-flat-files-connection-manager.md).  
  
## Options  
 **Connection manager name**  
 Provide a unique name for the Multiple Flat Files connection manager in the workflow. The name provided will be displayed within the **Connection Managers** area of [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer.  
  
 **Description**  
 Describe the connection manager. As a best practice, describe the connection manager in terms of its purpose, to make packages self-documenting and easier to maintain.  
  
 **Configure the properties of each column**  
 Select a column in the left pane to view its properties in the right pane. See the following table for a description of data type properties. Some of the properties listed are configurable only for some flat file formats.  
  
|Property|Description|  
|--------------|-----------------|  
|**ColumnType**|Denotes whether the column is delimited, fixed width, or ragged right. This property is read-only. Ragged right files are files in which every column has a fixed width, except for the last column, which is terminated by the row delimiter.|  
|**OutputColumnWidth**|Specify a value to be stored as a count of bytes; for Unicode files, this will display as a count of characters. In the Data Flow task, this value is used to set the output column width for the Flat File source.<br /><br /> Note: In the object model, the name of this property is MaximumWidth.|  
|**DataType**|Select from the list of available data types. For more information, see [Integration Services Data Types](data-flow/integration-services-data-types.md).|  
|**TextQualified**|Indicate whether text data is qualified using a text qualifier character. Valid values are:<br /><br /> **True**: Text data in the flat file is qualified.<br /><br /> **False**: Text data in the flat file is not qualified.|  
|**Name**|Provide a column name. The default is a numbered list of columns; however, you can choose any unique, descriptive name.|  
|**DataScale**|Specify the scale of numeric data. Scale refers to the number of decimal places. For more information, see [Integration Services Data Types](data-flow/integration-services-data-types.md).|  
|**ColumnDelimiter**|Select from the list of available column delimiters. Choose delimiters that are not likely to occur in the text. This value is ignored for fixed-width columns.<br /><br /> **{CR}{LF}** - columns are delimited by a carriage return-line feed combination<br /><br /> **{CR}** - columns are delimited by a carriage return<br /><br /> **{LF}** - columns are delimited by a line feed<br /><br /> **Semicolon {;}** - columns are delimited by a semicolon<br /><br /> **Colon {:}** - columns are delimited by a colon<br /><br /> **Comma {,}** - columns are delimited by a comma<br /><br /> **Tab {t}** - columns are delimited by a tab<br /><br /> **Vertical bar {&#124;}** - columns are delimited by a vertical bar|  
|**DataPrecision**|Specify the precision of numeric data. Precision refers to the number of digits. For more information, see [Integration Services Data Types](data-flow/integration-services-data-types.md).|  
|**InputColumnWidth**|Specify a value to be stored as a count of bytes; for Unicode files, this will display as a count of characters. This value is ignored for delimited columns.<br /><br /> **Note** In the object model, the name of this property is ColumnWidth.|  
  
 **New**  
 Add a new column by clicking **New**. By default, the **New** button adds a new column at the end of the list. The button also has the following options, available in the dropdown list.  
  
|Value|Description|  
|-----------|-----------------|  
|**Add Column**|Add a new column at the end of the list.|  
|**Insert Before**|Insert a new column before the selected column.|  
|**Insert After**|Insert a new column after the selected column.|  
  
 **Delete**  
 Select a column, and then remove it by clicking **Delete**.  
  
 **Suggest Types**  
 Use the **Suggest Column Types** dialog box to evaluate sample data in the first selected file and to obtain suggestions for the data type and length of each column. For more information, see [Suggest Column Types Dialog Box UI Reference](connection-manager/suggest-column-types-dialog-box-ui-reference.md).  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Multiple Flat Files Connection Manager Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Multiple Flat Files Connection Manager Editor &#40;Columns Page&#41;](../../2014/integration-services/multiple-flat-files-connection-manager-editor-columns-page.md)   
 [Multiple Flat Files Connection Manager Editor &#40;Preview Page&#41;](../../2014/integration-services/multiple-flat-files-connection-manager-editor-preview-page.md)  
  
  
