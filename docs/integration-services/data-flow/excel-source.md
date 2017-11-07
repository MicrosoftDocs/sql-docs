---
title: "Excel Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.excelsource.f1"
  - "sql13.dts.designer.excelsourceadapter.connection.f1"
  - "sql13.dts.designer.excelsourceadapter.columns.f1"
  - "sql13.dts.designer.excelsourceadapter.erroroutput.f1"
helpviewer_keywords: 
  - "Excel [Integration Services]"
  - "sources [Integration Services], Excel"
ms.assetid: e66349f3-b1b8-4763-89b7-7803541a4d62
caps.latest.revision: 60
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Excel Source
  The Excel source extracts data from worksheets or ranges in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Excel workbooks.  
  
 The Excel source provides four different data access modes for extracting data:  
  
-   A table or view.  
  
-   A table or view specified in a variable.  
  
-   The results of an SQL statement. The query can be a parameterized query.  
  
-   The results of an SQL statement stored in a variable.  
  
> [!IMPORTANT]  
>  In Excel, a worksheet or range is the equivalent of a table or view. The list of available tables in the Excel Source and Destination editors displays existing worksheets (identified by the $ sign appended to the worksheet name, such as Sheet1$) and named ranges (identified by the absence of the $ sign, such as MyRange). For more information, see the Usage Considerations section.  
  
 The Excel source uses an Excel connection manager to connect to a data source, and the connection manager specifies the workbook file to use. For more information, see [Excel Connection Manager](../../integration-services/connection-manager/excel-connection-manager.md).  
  
 The Excel source has one regular output and one error output.  
  
## Usage Considerations  
 The Excel Connection Manager uses the [!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB Provider for Jet 4.0 and its supporting Excel ISAM (Indexed Sequential Access Method) driver to connect and read and write data to Excel data sources.  
  
 Many existing [!INCLUDE[msCoName](../../includes/msconame-md.md)] Knowledge Base articles document the behavior of this provider and driver, and although these articles are not specific to [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] or its predecessor Data Transformation Services, you may want to know about certain behaviors that can lead to unexpected results. For general information on the use and behavior of the Excel driver, see [HOWTO: Use ADO with Excel Data from Visual Basic or VBA](http://support.microsoft.com/kb/257819).  
  
 The following behaviors of the Jet provider with the Excel driver can lead to unexpected results when reading data from an Excel data source.  
  
-   **Data sources**. The source of data in an Excel workbook can be a worksheet, to which the $ sign must be appended (for example, Sheet1$), or a named range (for example, MyRange). In a SQL statement, the name of a worksheet must be delimited (for example, [Sheet1$]) to avoid a syntax error caused by the $ sign. The Query Builder automatically adds these delimiters. When you specify a worksheet or range, the driver reads the contiguous block of cells starting with the first non-empty cell in the upper-left corner of the worksheet or range. Therefore you cannot have empty rows in the source data, or an empty row between title or header rows and the data rows.  
  
-   **Missing values**. The Excel driver reads a certain number of rows (by default, 8 rows) in the specified source to guess at the data type of each column. When a column appears to contain mixed data types, especially numeric data mixed with text data, the driver decides in favor of the majority data type, and returns null values for cells that contain data of the other type. (In a tie, the numeric type wins.) Most cell formatting options in the Excel worksheet do not seem to affect this data type determination. You can modify this behavior of the Excel driver by specifying Import Mode. To specify Import Mode, add **IMEX=1** to the value of Extended Properties in the connection string of the Excel connection manager in the **Properties** window. For more information, see [PRB: Excel Values Returned as NULL Using DAO OpenRecordset](http://support.microsoft.com/kb/194124).  
  
-   **Truncated text**. When the driver determines that an Excel column contains text data, the driver selects the data type (string or memo) based on the longest value that it samples. If the driver does not discover any values longer than 255 characters in the rows that it samples, it treats the column as a 255-character string column instead of a memo column. Therefore, values longer than 255 characters may be truncated. To import data from a memo column without truncation, you must make sure that the memo column in at least one of the sampled rows contains a value longer than 255 characters, or you must increase the number of rows sampled by the driver to include such a row. You can increase the number of rows sampled by increasing the value of **TypeGuessRows** under the **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Jet\4.0\Engines\Excel** registry key. For more information, see [PRB: Transfer of Data from Jet 4.0 OLEDB Source Fails w/ Error](http://support.microsoft.com/kb/281517).  
  
-   **Data types**. The Excel driver recognizes only a limited set of data types. For example, all numeric columns are interpreted as doubles (DT_R8), and all string columns (other than memo columns) are interpreted as 255-character Unicode strings (DT_WSTR). [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] maps the Excel data types as follows:  
  
    -   Numeric – double-precision float (DT_R8)  
  
    -   Currency – currency (DT_CY)  
  
    -   Boolean – Boolean (DT_BOOL)  
  
    -   Date/time – **datetime** (DT_DATE)  
  
    -   String – Unicode string, length 255 (DT_WSTR)  
  
    -   Memo – Unicode text stream (DT_NTEXT)  
  
-   **Data type and length conversions**. [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] does not implicitly convert data types. As a result, you may need to use Derived Column or Data Conversion transformations to convert Excel data explicitly before loading it into a non-Excel destination, or to convert non-Excel data before loading it into an Excel destination. In this case, it may be useful to create the initial package by using the Import and Export Wizard, which configures the necessary conversions for you. Some examples of the conversions that may be required include the following:  
  
    -   Conversion between Unicode Excel string columns and non-Unicode string columns with specific codepages  
  
    -   Conversion between 255-character Excel string columns and string columns of different lengths  
  
    -   Conversion between double-precision Excel numeric columns and numeric columns of other types  
  
## Excel Source Configuration  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects all the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](http://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [Excel Custom Properties](../../integration-services/data-flow/excel-custom-properties.md)  
  
 For information about looping through a group of Excel files, see [Loop through Excel Files and Tables by Using a Foreach Loop Container](../../integration-services/control-flow/loop-through-excel-files-and-tables-by-using-a-foreach-loop-container.md).  
  
## Related Tasks  
  
-   [Map Query Parameters to Variables in a Data Flow Component](../../integration-services/data-flow/map-query-parameters-to-variables-in-a-data-flow-component.md)  
  
-   [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md)  
  
-   [Sort Data for the Merge and Merge Join Transformations](../../integration-services/data-flow/transformations/sort-data-for-the-merge-and-merge-join-transformations.md)  
  
-   [Loop through Excel Files and Tables by Using a Foreach Loop Container](../../integration-services/control-flow/loop-through-excel-files-and-tables-by-using-a-foreach-loop-container.md)  
  
## Excel Source Editor (Connection Manager Page)
  Use the **Connection Manager** node of the **Excel Source Editor** dialog box to select the [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] workbook for the source to use. The Excel source reads data from a worksheet or named range in an existing workbook.  
  
> [!NOTE]  
>  The **CommandTimeout** property of the Excel source is not available in the **Excel Source Editor**, but can be set by using the **Advanced Editor**. For more information on this property, see the Excel Source section of [Excel Custom Properties](../../integration-services/data-flow/excel-custom-properties.md).  
  
### Static Options  
 **OLE DB connection manager**  
 Select an existing Excel connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection manager by using the **Excel Connection Manager** dialog box.  
  
 **Data access mode**  
 Specify the method for selecting data from the source.  
  
|Value|Description|  
|-----------|-----------------|  
|Table or view|Retrieve data from a worksheet or named range in the Excel file.|  
|Table name or view name variable|Specify the worksheet or range name in a variable.<br /><br /> **Related information:** [Use Variables in Packages](http://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787)|  
|SQL command|Retrieve data from the Excel file by using a SQL query. |  
|SQL command from variable|Specify the SQL query text in a variable.|  
  
 **Preview**  
 Preview results by using the **Data View** dialog box. Preview can display up to 200 rows.  
  
### Data Access Mode Dynamic Options  
  
#### Data access mode = Table or view  
 **Name of the Excel sheet**  
 Select the name of the worksheet or named range from a list of those available in the Excel workbook.  
  
#### Data access mode = Table name or view name variable  
 **Variable name**  
 Select the variable that contains the name of the worksheet or named range.  
  
#### Data access mode = SQL command  
 **SQL command text**  
 Enter the text of a SQL query, build the query by clicking **Build Query**, or browse to the file that contains the query text by clicking **Browse**.  
  
 **Parameters**  
 If you have entered a parameterized query by using ? as a parameter placeholder in the query text, use the **Set Query Parameters** dialog box to map query input parameters to package variables.  
  
 **Build query**  
 Use the **Query Builder** dialog box to construct the SQL query visually.  
  
 **Browse**  
 Use the **Open** dialog box to locate the file that contains the text of the SQL query.  
  
 **Parse query**  
 Verify the syntax of the query text.  
  
#### Data access mode = SQL command from variable  
 **Variable name**  
 Select the variable that contains the text of the SQL query.  
  
## Excel Source Editor (Columns Page)
  Use the **Columns** page of the **Excel Source Editor** dialog box to map an output column to each external (source) column.  
  
### Options  
 **Available External Columns**  
 View the list of available external columns in the data source. You cannot use this table to add or delete columns.  
  
 **External Column**  
 View external (source) columns in the order in which the task will read them. You can change this order by first clearing the selected columns in the table discussed above, and then selecting external columns from the list in a different order.  
  
 **Output Column**  
 Provide a unique name for each output column. The default is the name of the selected external (source) column; however, you can choose any unique, descriptive name. The name provided will be displayed within [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
## Excel Source Editor (Error Output Page)
  Use the **Error Output** page of the **Excel Source Editor** dialog box to select error handling options and to set properties on error output columns.  
  
### Options  
 **Input or Output**  
 View the name of the data source.  
  
 **Column**  
 View the external (source) columns that you selected on the **Connection Manager** page of the **Excel Source Editor**dialog box.  
  
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
  
## Related Content  
  
-   Blog entry, [Importing data from 64-bit Excel in SSIS](http://go.microsoft.com/fwlink/?LinkId=217673), on hrvoje.piasevoli.com  
  
-   Blog entry, [Excel in Integration Services, Part 1 of 3: Connections and Components](http://go.microsoft.com/fwlink/?LinkId=217674), on dougbert.com  
  
-   Blog entry, [Excel in Integration Services, Part 2 of 3: Tables and Data Types](http://go.microsoft.com/fwlink/?LinkId=217675), on dougbert.com.  
  
-   Blog entry, [Excel in Integration Services, Part 3 of 3: Issues and Alternatives](http://go.microsoft.com/fwlink/?LinkId=217676), on dougbert.com.  
  
  