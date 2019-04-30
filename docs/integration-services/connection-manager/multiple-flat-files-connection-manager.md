---
title: "Multiple Flat Files Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.multifile.advanced.f1"
  - "sql13.dts.designer.multifile.columns.f1"
  - "sql13.dts.designer.multifile.general.f1"
  - "sql13.dts.designer.multifile.preview.f1"
helpviewer_keywords: 
  - "Multiple Flat Files connection manager"
  - "connections [Integration Services], flat files"
  - "flat files"
  - "flat file connections [Integration Services]"
  - "connection managers [Integration Services], Multiple Flat Files"
  - "multiple flat file connections"
ms.assetid: 31fc3f7a-d323-44f5-a907-1fa3de66631a
author: janinezhang
ms.author: janinez
manager: craigg
---
# Multiple Flat Files Connection Manager

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  A Multiple Flat Files connection manager enables a package to access data in multiple flat files. For example, a Flat File source can use a Multiple Flat Files connection manager when the Data Flow task is inside a loop container, such as the For Loop container. On each loop of the container, the Flat File source loads data from the next file name that the Multiple Flat Files connection manager provides.  
  
 When you add a Multiple Flat Files connection manager to a package, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that will resolve to a Multiple Flat Files connection at run time, sets the properties on the Multiple Flat Files connection manager, and adds the Multiple Flat Files connection manager to the **Connections** collection of the package.  
  
 The **ConnectionManagerType** property of the connection manager is set to **MULTIFLATFILE**.  
  
 You can configure the Multiple Flat Files connection manager in the following ways:  
  
-   Specify the files, locale, and code page to use. The locale is used to interpret locale-sensitive data such as dates, and the code page is used to convert string data to Unicode.  
  
-   Specify the file format. You can use a delimited, fixed width, or ragged right format.  
  
-   Specify a header row, data row, and column delimiters. Column delimiters can be set at the file level and overwritten at the column level.  
  
-   Indicate whether the first row in the files contains column names.  
  
-   Specify a text qualifier character. Each column can be configured to recognize a text qualifier.  
  
-   Set properties such as the name, data type, and maximum width on individual columns.  
  
 When the Multiple Flat Files connection manager references multiple files, the paths of the files are separated by the pipe (|) character. The **ConnectionString** property of the connection manager has the following format:  
  
 \<*path*>|\<*path*>  
  
 You can also specify multiple files by using wildcard characters. For example, to reference all the text files on the C drive, the value of the **ConnectionString** property can be set to C:\\*.txt.  
  
 If a Multiple Flat Files connection manager references multiple files, all the files must have the same format.  
  
 By default, the Multiple Flat Files connection manager sets the length of string columns to 50 characters. In the **Multiple Flat Files Connection Manager Editor** dialog box, you can evaluate sample data and automatically resize the length of these columns to prevent truncation of data or excess column width. Unless you resize the column length in a Flat File source or a transformation, the column length remains the same throughout the data flow. If these columns map to destination columns that are narrower, warnings appear in the user interface, and at run time, errors may occur due to data truncation. You can resize the columns to be compatible with the destination columns in the Flat File connection manager, the Flat File source, or a transformation. To modify the length of output columns, you set the **Length** property of the output column on the **Input and Output Properties** tab in the **Advanced Editor** dialog box.  
  
 If you update column lengths in the Multiple Flat Files connection manager after you have added and configured the Flat File source that uses the connection manager, you do not have to manually resize the output columns in the Flat File source. When you open the **Flat File Source** dialog box, the Flat File source provides an option to synchronize the column metadata.  
  
## Configuration of the Multiple Flat Files Connection Manager  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../../integration-services/building-packages-programmatically/adding-connections-programmatically.md).  
  
## Multiple Flat Files Connection Manager Editor (General Page)
  Use the **General** page of the **Multiple Flat Files Connection Manager Editor** dialog box to select a group of files that have the same data format, and to specify their data format. A multiple flat files connection enables a package to connect to a group of text files that have the same format.  
  
 To learn more about the Multiple Flat Files connection manager, see [Multiple Flat Files Connection Manager](../../integration-services/connection-manager/multiple-flat-files-connection-manager.md).  
  
### Options  
 **Connection manager name**  
 Provide a unique name for the Multiple Flat Files connection in the workflow. The name provided will be displayed within [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 **Description**  
 Describe the connection. As a best practice, describe the connection in terms of its purpose, to make packages self-documenting and easier to maintain.  
  
 **File names**  
 Type the path and file names to use in the Multiple Flat Files connection. You can specify multiple files by using wildcard characters, as in the example "C:\\*.txt", or by using the vertical pipe character (|) to separate multiple file names. All files must have the same data format.  
  
 **Browse**  
 Browse the file names to use in the Multiple Flat Files connection. You can select multiple files. All files must have the same data format.  
  
 **Locale**  
 Specify the location to provide information for ordering and for date and time conversion.  
  
 **Unicode**  
 Indicate whether to use Unicode. Using Unicode precludes specifying a code page.  
  
 **Code page**  
 Specify the code page for non-Unicode text.  
  
 **Format**  
 Indicate whether to use delimited, fixed width, or ragged right formatting. All files must have the same data format.  
  
|Value|Description|  
|-----------|-----------------|  
|Delimited|Columns are separated by delimiters, specified on the **Columns** page.|  
|Fixed width|Columns have a fixed width, specified by dragging marker lines on the **Columns** page.|  
|Ragged right|Ragged right files are files in which every column has a fixed width, except for the last column, which is delimited by the row delimiter, specified on the **Columns** page.|  
  
 **Text qualifier**  
 Specify the text qualifier to use. For example, you can specify to enclose text with quotation marks.  
  
 **Header row delimiter**  
 Select from the list of delimiters for header rows, or enter the delimiter text.  
  
|Value|Description|  
|-----------|-----------------|  
|**{CR}{LF}**|The header row is delimited by a carriage return-line feed combination.|  
|**{CR}**|The header row is delimited by a carriage return.|  
|**{LF}**|The header row is delimited by a line feed.|  
|**Semicolon {;}**|The header row is delimited by a semicolon.|  
|**Colon {:}**|The header row is delimited by a colon.|  
|**Comma {,}**|The header row is delimited by a comma.|  
|**Tab {t}**|The header row is delimited by a tab.|  
|**Vertical bar {&#124;}**|The header row is delimited by a vertical bar.|  
  
 **Header rows to skip**  
 Specify the number of header rows to skip, if any.  
  
 **Column names in the first data row**  
 Indicate whether to expect or provide column names in the first data row.  
  
## Multiple Flat Files Connection Manager Editor (Columns Page)
  Use the **Columns** node of the **Multiple Flat Files Connection Manager Editor** dialog box to specify the row and column information, and to preview the first selected file.  
  
 To learn more about the Multiple Flat Files connection manager, see [Multiple Flat Files Connection Manager](../../integration-services/connection-manager/multiple-flat-files-connection-manager.md).  
  
### Static Options  
 **Connection manager name**  
 Provide a unique name for the Multiple Flat Files connection in the workflow. The name provided will be displayed within [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 **Description**  
 Describe the connection. As a best practice, describe the connection in terms of its purpose, to make packages self-documenting and easier to maintain.  
  
### Flat File Format Dynamic Options  
  
#### Format = Delimited  
 **Row delimiter**  
 Select from the list of available row delimiters, or enter the delimiter text.  
  
|Value|Description|  
|-----------|-----------------|  
|**{CR}{LF}**|Rows are delimited by a carriage return-line feed combination.|  
|**{CR}**|Rows are delimited by a carriage return.|  
|**{LF}**|Rows are delimited by a line feed.|  
|**Semicolon {;}**|Rows are delimited by a semicolon.|  
|**Colon {:}**|Rows are delimited by a colon.|  
|**Comma {,}**|Rows are delimited by a comma.|  
|**Tab {t}**|Rows are delimited by a tab.|  
|**Vertical bar {&#124;}**|Rows are delimited by a vertical bar.|  
  
 **Column delimiter**  
 Select from the list of available column delimiters, or enter the delimiter text.  
  
|Value|Description|  
|-----------|-----------------|  
|**{CR}{LF}**|Columns are delimited by a carriage return-line feed combination.|  
|**{CR}**|Columns are delimited by a carriage return.|  
|**{LF}**|Columns are delimited by a line feed.|  
|**Semicolon {;}**|Columns are delimited by a semicolon.|  
|**Colon {:}**|Columns are delimited by a colon.|  
|**Comma {,}**|Columns are delimited by a comma.|  
|**Tab {t}**|Columns are delimited by a tab.|  
|**Vertical bar {&#124;}**|Columns are delimited by a vertical bar.|  
  
 **Reset Columns**  
 Remove all but the original columns by clicking **Reset Columns**.  
  
#### Format = Fixed Width  
 **Font**  
 Select the font in which to display the preview data.  
  
 **Source data columns**  
 Adjust the width of the row by sliding the vertical row marker, and adjust the width of the columns by clicking the ruler at the top of the preview window  
  
 **Row width**  
 Specify the length of the row before adding delimiters for individual columns. Or, drag the vertical line in the preview window to mark the end of the row. The row width value is automatically updated.  
  
 **Reset Columns**  
 Remove all but the original columns by clicking **Reset Columns**.  
  
#### Format = Ragged Right  
  
> [!NOTE]  
>  Ragged right files are those in which every column has a fixed width, except for the last column. It is delimited by the row delimiter.  
  
 **Font**  
 Select the font in which to display the preview data.  
  
 **Source data columns**  
 Adjust the width of the row by sliding the vertical row marker, and adjust the width of the columns by clicking the ruler at the top of the preview window  
  
 **Row delimiter**  
 Select from the list of available row delimiters, or enter the delimiter text.  
  
|Value|Description|  
|-----------|-----------------|  
|**{CR}{LF}**|Rows are delimited by a carriage return-line feed combination.|  
|**{CR}**|Rows are delimited by a carriage return.|  
|**{LF}**|Rows are delimited by a line feed.|  
|**Semicolon {;}**|Rows are delimited by a semicolon.|  
|**Colon {:}**|Rows are delimited by a colon.|  
|**Comma {,}**|Rows are delimited by a comma.|  
|**Tab {t}**|Rows are delimited by a tab.|  
|**Vertical bar {&#124;}**|Rows are delimited by a vertical bar.|  
  
 **Reset Columns**  
 Remove all but the original columns by clicking **Reset Columns**.  
  
## Multiple Flat Files Connection Manager Editor (Advanced Page)
  Use the **Advanced** page of the **Multiple Flat Files Connection ManagerEditor** dialog box to set properties such as data type and delimiters of each column in the text files to which the flat file connection manager connects.  
  
 By default, the length of string columns is 50 characters. You can evaluate sample data and automatically resize the length of these columns to prevent truncation of data or excess column width. You can also update other metadata to enable compatibility with destination columns. For example, you might change the data type of a column that contains only integer data to a numeric data type, such as DT_I2.  
  
 To learn more about the Multiple Flat Files connection manager, see [Multiple Flat Files Connection Manager](../../integration-services/connection-manager/multiple-flat-files-connection-manager.md).  
  
### Options  
 **Connection manager name**  
 Provide a unique name for the Multiple Flat Files connection manager in the workflow. The name provided will be displayed within the **Connection Managers** area of [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 **Description**  
 Describe the connection manager. As a best practice, describe the connection manager in terms of its purpose, to make packages self-documenting and easier to maintain.  
  
 **Configure the properties of each column**  
 Select a column in the left pane to view its properties in the right pane. See the following table for a description of data type properties. Some of the properties listed are configurable only for some flat file formats.  
  
|Property|Description|  
|--------------|-----------------|  
|**ColumnType**|Denotes whether the column is delimited, fixed width, or ragged right. This property is read-only. Ragged right files are files in which every column has a fixed width, except for the last column, which is terminated by the row delimiter.|  
|**OutputColumnWidth**|Specify a value to be stored as a count of bytes; for Unicode files, this will display as a count of characters. In the Data Flow task, this value is used to set the output column width for the Flat File source.<br /><br /> Note: In the object model, the name of this property is MaximumWidth.|  
|**DataType**|Select from the list of available data types. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).|  
|**TextQualified**|Indicate whether text data is qualified using a text qualifier character:<br /><br /> **True**: Text data in the flat file is qualified.<br /><br /> **False**: Text data in the flat file is not qualified.|  
|**Name**|Provide a column name. The default is a numbered list of columns; however, you can choose any unique, descriptive name.|  
|**DataScale**|Specify the scale of numeric data. Scale refers to the number of decimal places. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).|  
|**ColumnDelimiter**|Select from the list of available column delimiters. Choose delimiters that are not likely to occur in the text. This value is ignored for fixed-width columns.<br /><br /> **{CR}{LF}** - columns are delimited by a carriage return-line feed combination<br /><br /> **{CR}** - columns are delimited by a carriage return<br /><br /> **{LF}** - columns are delimited by a line feed<br /><br /> **Semicolon {;}** - columns are delimited by a semicolon<br /><br /> **Colon {:}** - columns are delimited by a colon<br /><br /> **Comma {,}** - columns are delimited by a comma<br /><br /> **Tab {t}** - columns are delimited by a tab<br /><br /> **Vertical bar {&#124;}** - columns are delimited by a vertical bar|  
|**DataPrecision**|Specify the precision of numeric data. Precision refers to the number of digits. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).|  
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
 Use the **Suggest Column Types** dialog box to evaluate sample data in the first selected file and to obtain suggestions for the data type and length of each column. For more information, see [Suggest Column Types Dialog Box UI Reference](../../integration-services/connection-manager/suggest-column-types-dialog-box-ui-reference.md).  
  
## Multiple Flat Files Connection Manager Editor (Preview Page)
  Use the **Preview** page of the **Multiple Flat Files ConnectionManager Editor** dialog box to view the contents of the first selected source file divided into columns as you have defined them.  
  
 To learn more about the Multiple Flat Files connection manager, see [Multiple Flat Files Connection Manager](../../integration-services/connection-manager/multiple-flat-files-connection-manager.md).  
  
### Options  
 **Connection manager name**  
 Provide a unique name for the Multiple Flat Files connection in the workflow. The name provided will be displayed within the **Connection Managers** area of [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 **Description**  
 Describe the connection. As a best practice, describe the connection in terms of its purpose, to make packages self-documenting and easier to maintain.  
  
 **Data rows to skip**  
 Specify how many rows to skip at the beginning of the flat file.  
  
 **Preview rows**  
 View sample data in the first selected flat file, divided into columns and rows by using the options selected.  
  
## See Also  
 [Flat File Source](../../integration-services/data-flow/flat-file-source.md)   
 [Flat File Destination](../../integration-services/data-flow/flat-file-destination.md)   
 [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md)  
  
  
