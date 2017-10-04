---
title: "Excel Destination | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.exceldest.f1"
  - "sql13.dts.designer.exceldestadapter.connection.f1"
  - "sql13.dts.designer.exceldestadapter.mappings.f1"
  - "sql13.dts.designer.exceldestadapter.erroroutput.f1"
helpviewer_keywords: 
  - "destinations [Integration Services], Excel"
  - "Excel [Integration Services]"
ms.assetid: 37c07446-1264-4814-b4f5-9c66d333bb24
caps.latest.revision: 49
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Excel Destination
  The Excel destination loads data into worksheets or ranges in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Excel workbooks.  
  
## Access Modes  
 The Excel destination provides three different data access modes for loading data:  
  
-   A table or view.  
  
-   A table or view specified in a variable.  
  
-   The results of an SQL statement. The query can be a parameterized query.  
  
> [!IMPORTANT]  
>  In Excel, a worksheet or range is the equivalent of a table or view. The lists of available tables in the Excel Source and Destination editors display only existing worksheets (identified by the $ sign appended to the worksheet name, such as Sheet1$) and named ranges (identified by the absence of the $ sign, such as MyRange).  
  
## Usage Considerations  
 The Excel Connection Manager uses the [!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB Provider for Jet 4.0 and its supporting Excel ISAM (Indexed Sequential Access Method) driver to connect and read and write data to Excel data sources.  
  
 Many existing [!INCLUDE[msCoName](../../includes/msconame-md.md)] Knowledge Base articles document the behavior of this provider and driver, and although these articles are not specific to [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] or its predecessor Data Transformation Services, you may want to know about certain behaviors that can lead to unexpected results. For general information on the use and behavior of the Excel driver, see [HOWTO: Use ADO with Excel Data from Visual Basic or VBA](http://support.microsoft.com/kb/257819).  
  
 The following behaviors of the Jet provider that is included with the Excel driver can lead to unexpected results when saving data to an Excel destination.  
  
-   **Saving text data**. When the Excel driver saves text data values to an Excel destination, the driver precedes the text in each cell with the single quote character (') to ensure that the saved values will be interpreted as text values. If you have or develop other applications that read or process the saved data, you may need to include special handling for the single quote character that precedes each text value.  
  
     For information on how to avoid including the single quote, see this blog post, [Single quote is appended to all strings when data is transformed to excel when using Excel destination data flow component in SSIS package](http://go.microsoft.com/fwlink/?LinkId=400876), on msdn.com.  
  
-   **Saving memo (ntext) da**ta. Before you can successfully save strings longer than 255 characters to an Excel column, the driver must recognize the data type of the destination column as **memo** and not **string**. If the destination table already contains rows of data, then the first few rows that are sampled by the driver must contain at least one instance of a value longer than 255 characters in the memo column. If the destination table is created during package design or at run time, then the CREATE TABLE statement must use LONGTEXT (or one of its synonyms) as the data type of the the memo column.  
  
-   **Data types**. The Excel driver recognizes only a limited set of data types. For example, all numeric columns are interpreted as doubles (DT_R8), and all string columns (other than memo columns) are interpreted as 255-character Unicode strings (DT_WSTR). [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] maps the Excel data types as follows:  
  
    -   Numeric    double-precision float (DT_R8)  
  
    -   Currency     currency (DT_CY)  
  
    -   Boolean     Boolean (DT_BOOL)  
  
    -   Date/time     **datetime** (DT_DATE)  
  
    -   String     Unicode string, length 255 (DT_WSTR)  
  
    -   Memo     Unicode text stream (DT_NTEXT)  
  
-   **Data type and length conversions**. [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] does not implicitly convert data types. As a result, you may need to use the Derived Column or Data Conversion transformations to convert Excel data explicitly before loading it into a non-Excel destination, or to convert non-Excel data before loading it into an Excel destination. In this case, it may be useful to create the initial package by using the Import and Export Wizard, which configures the necessary conversions for you. Some examples of the conversions that may be required include the following:  
  
    -   Conversion between Unicode Excel string columns and non-Unicode string columns with specific codepages.  
  
    -   Conversion between 255-character Excel string columns and string columns of different lengths.  
  
    -   Conversion between double-precision Excel numeric columns and numeric columns of other types.  
  
## Configuration of the Excel Destination  
 The Excel destination uses an Excel connection manager to connect to a data source, and the connection manager specifies the workbook file to use. For more information, see [Excel Connection Manager](../../integration-services/connection-manager/excel-connection-manager.md).  
  
 The Excel destination has one regular input and one error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects all the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](http://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [Excel Custom Properties](../../integration-services/data-flow/excel-custom-properties.md)  
  
 For more information about how to set properties, see [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## Related Tasks  
  
-   [Connect to an Excel Workbook](../../integration-services/connection-manager/connect-to-an-excel-workbook.md)  
  
-   [Loop through Excel Files and Tables by Using a Foreach Loop Container](../../integration-services/control-flow/loop-through-excel-files-and-tables-by-using-a-foreach-loop-container.md)  
  
-   [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md)  
  
## Related Content  
  
-   Blog entry, [Excel in Integration Services, Part 1 of 3: Connections and Components](http://go.microsoft.com/fwlink/?LinkId=217674), on dougbert.com  
  
-   Blog entry, [Excel in Integration Services, Part 2 of 3: Tables and Data Types](http://go.microsoft.com/fwlink/?LinkId=217675), on dougbert.com.  
  
-   Blog entry, [Excel in Integration Services, Part 3 of 3: Issues and Alternatives](http://go.microsoft.com/fwlink/?LinkId=217676), on dougbert.com.  
  
## Excel Destination Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **Excel Destination Editor** dialog box to specify data source information, and to preview the results. The Excel destination loads data into a worksheet or a named range in a [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] workbook.  
  
> [!NOTE]  
>  The **CommandTimeout** property of the Excel destination is not available in the **Excel Destination Editor**, but can be set by using the **Advanced Editor**. In addition, certain Fast Load options are available only in the **Advanced Editor**. For more information on these properties, see the Excel Destination section of [Excel Custom Properties](../../integration-services/data-flow/excel-custom-properties.md).  
  
### Static Options  
 **Excel connection manager**  
 Select an existing Excel connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection manager by using the **Excel Connection Manager** dialog box.  
  
 **Data access mode**  
 Specify the method for selecting data from the source.  
  
|Option|Description|  
|------------|-----------------|  
|Table or view|Loads data into a worksheet or named range in the Excel data source.|  
|Table name or view name variable|Specify the worksheet or range name in a variable.<br /><br /> **Related information**: [Use Variables in Packages](http://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787)|  
|SQL command|Load data into the Excel destination by using an SQL query.|  
  
 **Name of the Excel sheet**  
 Select the excel destination from the drop-down list. If the list is empty, click **New**.  
  
 **New**  
 Click **New** to launch the **Create Table** dialog box. When you click **OK**, the dialog box creates the excel file that the **Excel Connection Manager** points to.  
  
 **View Existing Data**  
 Preview results by using the **Preview Query Results** dialog box. Preview can display up to 200 rows.  
  
> [!WARNING]  
>  If the **Excel connection manager** you selected points to an excel file that does not exist, you will see an error message when you click this button.  
  
### Data Access Mode Dynamic Options  
  
#### Data access mode = Table or view  
 **Name of the Excel sheet**  
 Select the name of the worksheet or named range from a list of those available in the data source.  
  
#### Data access mode = Table name or view name variable  
 **Variable name**  
 Select the variable that contains the name of the worksheet or named range.  
  
#### Data access mode = SQL command  
 **SQL command text**  
 Enter the text of an SQL query, build the query by clicking **Build Query**, or locate the file that contains the query text by clicking **Browse**.  
  
 **Build Query**  
 Use the **Query Builder** dialog box to construct the SQL query visually.  
  
 **Browse**  
 Use the **Open** dialog box to locate the file that contains the text of the SQL query.  
  
 **Parse Query**  
 Verify the syntax of the query text.  
  
## Excel Destination Editor (Mappings Page)
  Use the **Mappings** page of the **Excel Destination Editor** dialog box to map input columns to destination columns.  
  
### Options  
 **Available Input Columns**  
 View the list of available input columns. Use a drag-and-drop operation to map available input columns in the table to destination columns.  
  
 **Available Destination Columns**  
 View the list of available destination columns. Use a drag-and-drop operation to map available destination columns in the table to input columns.  
  
 **Input Column**  
 View input columns selected from the table above. You can change the mappings by using the list of **Available Input Columns**.  
  
 **Destination Column**  
 View each available destination column, whether it is mapped or not.  
  
## Excel Destination Editor (Error Output Page)
  Use the **Advanced** page of the **Excel Destination Editor** dialog box to specify options for error handling.  
  
### Options  
 **Input or Output**  
 View the name of the data source.  
  
 **Column**  
 View the external (source) columns that you selected in the **Connection Manager** node of the **Excel Source Editor**dialog box.  
  
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
 [Excel Source](../../integration-services/data-flow/excel-source.md)   
 [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md)   
 [Data Flow](../../integration-services/data-flow/data-flow.md)   
 [Working with Excel Files with the Script Task](../../integration-services/extending-packages-scripting-task-examples/working-with-excel-files-with-the-script-task.md)  
  
  