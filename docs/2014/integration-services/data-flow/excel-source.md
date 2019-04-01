---
title: "Excel Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.excelsource.f1"
helpviewer_keywords: 
  - "Excel [Integration Services]"
  - "sources [Integration Services], Excel"
ms.assetid: e66349f3-b1b8-4763-89b7-7803541a4d62
author: janinezhang
ms.author: janinez
manager: craigg
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
  
 The Excel source uses an Excel connection manager to connect to a data source, and the connection manager specifies the workbook file to use. For more information, see [Excel Connection Manager](../connection-manager/excel-connection-manager.md).  
  
 The Excel source has one regular output and one error output.  
  
## Usage Considerations  
 The Excel Connection Manager uses the [!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB Provider for Jet 4.0 and its supporting Excel ISAM (Indexed Sequential Access Method) driver to connect and read and write data to Excel data sources.  
  
 Many existing [!INCLUDE[msCoName](../../includes/msconame-md.md)] Knowledge Base articles document the behavior of this provider and driver, and although these articles are not specific to [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] or its predecessor Data Transformation Services, you may want to know about certain behaviors that can lead to unexpected results. For general information on the use and behavior of the Excel driver, see [HOWTO: Use ADO with Excel Data from Visual Basic or VBA](https://support.microsoft.com/kb/257819).  
  
 The following behaviors of the Jet provider with the Excel driver can lead to unexpected results when reading data from an Excel data source.  
  
-   **Data sources**. The source of data in an Excel workbook can be a worksheet, to which the $ sign must be appended (for example, Sheet1$), or a named range (for example, MyRange). In a SQL statement, the name of a worksheet must be delimited (for example, [Sheet1$]) to avoid a syntax error caused by the $ sign. The Query Builder automatically adds these delimiters. When you specify a worksheet or range, the driver reads the contiguous block of cells starting with the first non-empty cell in the upper-left corner of the worksheet or range. Therefore you cannot have empty rows in the source data, or an empty row between title or header rows and the data rows.  
  
-   **Missing values**. The Excel driver reads a certain number of rows (by default, 8 rows) in the specified source to guess at the data type of each column. When a column appears to contain mixed data types, especially numeric data mixed with text data, the driver decides in favor of the majority data type, and returns null values for cells that contain data of the other type. (In a tie, the numeric type wins.) Most cell formatting options in the Excel worksheet do not seem to affect this data type determination. You can modify this behavior of the Excel driver by specifying Import Mode. To specify Import Mode, add `IMEX=1` to the value of Extended Properties in the connection string of the Excel connection manager in the **Properties** window. For more information, see [PRB: Excel Values Returned as NULL Using DAO OpenRecordset](https://support.microsoft.com/kb/194124).  
  
-   **Truncated text**. When the driver determines that an Excel column contains text data, the driver selects the data type (string or memo) based on the longest value that it samples. If the driver does not discover any values longer than 255 characters in the rows that it samples, it treats the column as a 255-character string column instead of a memo column. Therefore, values longer than 255 characters may be truncated. To import data from a memo column without truncation, you must make sure that the memo column in at least one of the sampled rows contains a value longer than 255 characters, or you must increase the number of rows sampled by the driver to include such a row. You can increase the number of rows sampled by increasing the value of **TypeGuessRows** under the **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Jet\4.0\Engines\Excel** registry key. For more information, see [PRB: Transfer of Data from Jet 4.0 OLEDB Source Fails w/ Error](https://support.microsoft.com/kb/281517).  
  
-   **Data types**. The Excel driver recognizes only a limited set of data types. For example, all numeric columns are interpreted as doubles (DT_R8), and all string columns (other than memo columns) are interpreted as 255-character Unicode strings (DT_WSTR). [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] maps the Excel data types as follows:  
  
    -   Numeric - double-precision float (DT_R8)  
  
    -   Currency - currency (DT_CY)  
  
    -   Boolean - Boolean (DT_BOOL)  
  
    -   Date/time - `datetime` (DT_DATE)  
  
    -   String - Unicode string, length 255 (DT_WSTR)  
  
    -   Memo - Unicode text stream (DT_NTEXT)  
  
-   **Data type and length conversions**. [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] does not implicitly convert data types. As a result, you may need to use Derived Column or Data Conversion transformations to convert Excel data explicitly before loading it into a non-Excel destination, or to convert non-Excel data before loading it into an Excel destination. In this case, it may be useful to create the initial package by using the Import and Export Wizard, which configures the necessary conversions for you. Some examples of the conversions that may be required include the following:  
  
    -   Conversion between Unicode Excel string columns and non-Unicode string columns with specific codepages  
  
    -   Conversion between 255-character Excel string columns and string columns of different lengths  
  
    -   Conversion between double-precision Excel numeric columns and numeric columns of other types  
  
## Excel Source Configuration  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **Excel Source Editor** dialog box, click one of the following topics:  
  
-   [Excel Source Editor &#40;Connection Manager Page&#41;](../excel-source-editor-connection-manager-page.md)  
  
-   [Excel Source Editor &#40;Columns Page&#41;](../excel-source-editor-columns-page.md)  
  
-   [Excel Source Editor &#40;Error Output Page&#41;](../excel-source-editor-error-output-page.md)  
  
 The **Advanced Editor** dialog box reflects all the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../common-properties.md)  
  
-   [Excel Custom Properties](excel-custom-properties.md)  
  
 For information about looping through a group of Excel files, see [Loop through Excel Files and Tables by Using a Foreach Loop Container](../control-flow/foreach-loop-container.md).  
  
## Related Tasks  

-   [Import data from Excel or export data to Excel with SQL Server Integration Services (SSIS)](../load-data-to-from-excel-with-ssis.md)

-   [Map Query Parameters to Variables in a Data Flow Component](map-query-parameters-to-variables-in-a-data-flow-component.md)  
  
-   [Set the Properties of a Data Flow Component](set-the-properties-of-a-data-flow-component.md)  
  
-   [Sort Data for the Merge and Merge Join Transformations](transformations/sort-data-for-the-merge-and-merge-join-transformations.md)  
  
-   [Loop through Excel Files and Tables by Using a Foreach Loop Container](../control-flow/foreach-loop-container.md)  
  
## Related Content  
  
-   Blog entry, [Importing data from 64-bit Excel in SSIS](https://go.microsoft.com/fwlink/?LinkId=217673), on hrvoje.piasevoli.com  
  
-   Blog entry, [Excel in Integration Services, Part 1 of 3: Connections and Components](https://go.microsoft.com/fwlink/?LinkId=217674), on dougbert.com  
  
-   Blog entry, [Excel in Integration Services, Part 2 of 3: Tables and Data Types](https://go.microsoft.com/fwlink/?LinkId=217675), on dougbert.com.  
  
-   Blog entry, [Excel in Integration Services, Part 3 of 3: Issues and Alternatives](https://go.microsoft.com/fwlink/?LinkId=217676), on dougbert.com.  
  
-   Blog entry, [Connecting to Excel (XLSX) in SSIS ](http://microsoft-ssis.blogspot.com/2014/02/connecting-to-excel-xlsx-in-ssis.html).  
  
  
