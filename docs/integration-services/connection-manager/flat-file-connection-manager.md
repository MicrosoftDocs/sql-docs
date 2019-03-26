---
title: "Flat File Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.ffileconnection.general.f1"
  - "sql13.dts.designer.ffileconnection.columns.f1"
  - "sql13.dts.designer.ffileconnection.columnproperties.f1"
  - "sql13.dts.designer.ffileconnection.preview.f1"
helpviewer_keywords: 
  - "connection managers [Integration Services], Flat File"
  - "connections [Integration Services], flat files"
  - "files [Integration Services], connections"
  - "Flat File connection manager"
  - "flat files"
  - "flat file connections [Integration Services]"
ms.assetid: 7830f80d-af32-4e8f-a6fc-f03af6bc1946
author: janinezhang
ms.author: janinez
manager: craigg
---
# Flat File Connection Manager
  A Flat File connection manager enables a package to access data in a flat file. For example, the Flat File source and destination can use Flat File connection managers to extract and load data.  
  
 The Flat File connection manager can access only one file. To reference multiple files, use a Multiple Flat Files connection manager instead of a Flat File connection manager. For more information, see [Multiple Flat Files Connection Manager](../../integration-services/connection-manager/multiple-flat-files-connection-manager.md).  
  
## Column Length  
 By default, the Flat File connection manager sets the length of string columns to 50 characters. In the **Flat File Connection Manager Editor** dialog box, you can evaluate sample data and automatically resize the length of these columns to prevent truncation of data or excess column width. Also, unless you subsequently resize the column length in a Flat File source or a transformation, the column length of string column remains the same throughout the data flow. If these string columns map to destination columns that are narrower, warnings appear in the user interface. Moreover, at run time, errors may occur due to data truncation. To avoid errors or truncation, you can resize the columns to be compatible with the destination columns in the Flat File connection manager, the Flat File source, or a transformation. To modify the length of output columns, you set the **Length** property of the output column on the **Input and Output Properties** tab in the **Advanced Editor** dialog box.  
  
 If you update column lengths in the Flat File connection manager after you have added and configured the Flat File source that uses the connection manager, you do not have to manually resize the output columns in the Flat File source. When you open the **Flat File Source** dialog box, the Flat File source provides an option to synchronize the column metadata.  
  
## Configuration of the Flat File Connection Manager  
 When you add a Flat File connection manager to a package, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that will resolve to a Flat File connection at run time, sets the Flat File connection properties, and adds the Flat File connection manager to the **Connections** collection of the package.  
  
 The **ConnectionManagerType** property of the connection manager is set to **FLATFILE**.  
  
 By default, the Flat File connection manager always checks for a row delimiter in unquoted data, and starts a new row when a row delimiter is found. This enables the connection manager to correctly parse files with rows that are missing column fields.  
  
 In some cases, disabling this feature may improve package performance. You can disable this feature by setting the Flat File connection manager property, **AlwaysCheckForRowDelimiters**, to **False**.  
  
 You can configure the Flat File connection manager in the following ways:  
  
-   Specify the file, locale, and code page to use. The locale is used to interpret locale-sensitive data such as dates, and the code page is used to convert string data to Unicode.  
  
-   Specify the file format. You can use a delimited, fixed width, or ragged right format.  
  
-   Specify a header row, data row, and column delimiters. Column delimiters can be set at the file level and overwritten at the column level.  
  
-   Indicate whether the first row in the file contains column names.  
  
-   Specify a text qualifier character. Each column can be configured to recognize a text qualifier.  
  
     The use of a qualifier character to embed a qualifier character into a qualified string is supported by the Flat File Connection Manager. The double instance of a text qualifier is interpreted as a literal, single instance of that string. For example, if the text qualifier is a single quote and the input data is 'abc', 'def', 'g'hi', the output data is abc, def, g'hi. However, an instance of a qualifier embedded in a qualified string causes the Flat File Source to fail with the error DTS_E_PRIMEOUTPUTFAILED.
  
-   Set properties such as the name, data type, and maximum width on individual columns.  
  
 You can set the ConnectionString property for the Flat File connection manager by specifying an expression in the Properties window of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. To avoid a validation error, do the following.  
  
-   When you use an expression to specify the file, add a file path in the **File name** box in the **Flat File Connection Manager Editor**.  
  
-   Set the **DelayValidation** property on the Flat File connection manager to **True**.  
  
 You can use an expression to create a file name at runtime by using the Flat File connection manager with the Flat File destination.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../../integration-services/building-packages-programmatically/adding-connections-programmatically.md).  
  
## Flat File Connection Manager Editor (General Page)
  Use the **General** page of the **Flat File Connection Manager Editor** dialog box to select a file and data format. A flat file connection enables a package to connect to a text file.  
  
 To learn more about the Flat File connection manager, see [Flat File Connection Manager](../../integration-services/connection-manager/flat-file-connection-manager.md).  
  
### Options  
 **Connection manager name**  
 Provide a unique name for the flat file connection in the workflow. The name provided will be displayed within [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 **Description**  
 Describe the connection. As a best practice, describe the connection in terms of its purpose, to make packages self-documenting and easier to maintain.  
  
 **File name**  
 Type the path and file name to use in the flat file connection.  
  
 **Browse**  
 Locate the file name to use in the flat file connection.  
  
 **Locale**  
 Specify the locale to provide language-specific information for ordering and for date and time formats.  
  
 **Unicode**  
 Indicate whether to use Unicode. If you use Unicode, you cannot specify a code page.  
  
 **Code page**  
 Specify the code page for non-Unicode text.  
  
 **Format**  
 Indicate whether the file uses delimited, fixed width, or ragged right formatting.  
  
|Value|Description|  
|-----------|-----------------|  
|Delimited|Columns are separated by delimiters, specified on the **Columns** page.|  
|Fixed width|Columns have a fixed width.|  
|Ragged right|Ragged right files are files in which every column has a fixed width, except for the last column. It is delimited by the row delimiter.|  
  
 **Text qualifier**  
 Specify the text qualifier to use. For example, you can specify that text fields are enclosed in quotation marks.  
  
> [!NOTE]  
>  After you select a text qualifier, you cannot re-select the **None** option. Type **None** to de-select the text qualifier.  
  
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
 Specify the number of header rows or initial data rows to skip, if any.  
  
 **Column names in the first data row**  
 Indicate whether to expect or provide column names in the first data row.  
## Flat File Connection Manager Editor (Columns Page)
  Use the **Columns** page of the **Flat File Connection Manager Editor** dialog box to specify the row and column information, and to preview the file.  
  
 To learn more about the Flat File connection manager, see [Flat File Connection Manager](../../integration-services/connection-manager/flat-file-connection-manager.md).  
  
### Static Options  
 **Connection manager name**  
 Provide a unique name for the Flat File connection in the workflow. The name provided will be displayed within [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
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
  
 **Refresh**  
 View the effect of changing the delimiters to skip by clicking **Refresh**. This button only becomes visible after you have changed other connection options.  
  
 **Preview rows**  
 View sample data in the flat file, divided into columns and rows by using the options selected.  
  
 **Reset Columns**  
 Remove all but the original columns by clicking **Reset Columns**.  
  
#### Format = Fixed Width  
 **Font**  
 Select the font in which to display the preview data.  
  
 **Source data columns**  
 Adjust the width of the row by sliding the vertical red row marker, and adjust the width of the columns by clicking the ruler at the top of the preview window  
  
 **Row width**  
 Specify the length of the row before adding delimiters for individual columns. Or, drag the vertical red line in the preview window to mark the end of the row. The row width value is automatically updated.  
  
 **Reset Columns**  
 Remove all but the original columns by clicking **Reset Columns**.  
  
#### Format = Ragged Right  
  
> [!NOTE]  
>  Ragged right files are files in which every column has a fixed width, except for the last column. It is delimited by the row delimiter.  
  
 **Font**  
 Select the font in which to display the preview data.  
  
 **Source data columns**  
 Adjust the width of the row by sliding the vertical red row marker, and adjust the width of the columns by clicking the ruler at the top of the preview window  
  
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
## Flat File Connection Manager Editor (Advanced Page)
  Use the **Advanced** page of the **Flat File Connection Manager Editor** dialog box to set properties that specify how Integration Services reads and writes data in flat files. You can change the names of columns in the flat file, and set properties that include data type and delimiters for each column in the file.  
  
 By default, the length of string columns is 50 characters. You can resize the length of these columns to prevent truncation of data or excess column width. You can also update other metadata to enable compatibility with destination columns. For example, you might change the data type of a column that contains only integer data to a numeric data type, such as DT_I2. You can make these modifications manually, or you can click the **Select Types** button to use the **Suggest Column Types** dialog box to evaluate sample data and make some of these changes for you automatically.  
  
 To learn more about the Flat File connection manager, see [Flat File Connection Manager](../../integration-services/connection-manager/flat-file-connection-manager.md).  
  
### Options  
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
## Flat File Connection Manager Editor (Preview Page)
  Use the **Preview** node of the **Flat File Connection Manager Editor** dialog box to view the contents of the source file in a tabular format.  
  
 To learn more about the Flat File connection manager, see [Flat File Connection Manager](../../integration-services/connection-manager/flat-file-connection-manager.md).  
  
### Options  
 **Connection manager name**  
 Provide a unique name for the Flat File connection in the workflow. The name provided will be displayed within [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 **Description**  
 Describe the connection. As a best practice, describe the connection in terms of its purpose, to make packages self-documenting and easier to maintain.  
  
 **Data rows to skip**  
 Specify how many rows to skip at the beginning of the flat file.  
  
 **Refresh**  
 View the effect of changing the number of rows to skip by clicking **Refresh**. This button only becomes visible after you have changed other connection options.  
  
 **Preview rows**  
 View sample data in the flat file, divided into columns and rows according to the options you have selected.  
 
