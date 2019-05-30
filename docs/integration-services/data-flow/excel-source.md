---
title: "Excel Source | Microsoft Docs"
ms.custom: ""
ms.date: "05/15/2018"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.excelsource.f1"
  - "sql13.dts.designer.excelsourceadapter.connection.f1"
  - "sql13.dts.designer.excelsourceadapter.columns.f1"
  - "sql13.dts.designer.excelsourceadapter.erroroutput.f1"
helpviewer_keywords: 
  - "Excel [Integration Services]"
  - "sources [Integration Services], Excel"
ms.assetid: e66349f3-b1b8-4763-89b7-7803541a4d62
author: janinezhang
ms.author: janinez
manager: craigg
---
# Excel Source

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The Excel source extracts data from worksheets or ranges in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Excel workbooks.  

> [!IMPORTANT]
> For detailed info about connecting to Excel files, and about limitations and known issues for loading data from or to Excel files, see [Load data from or to Excel with SQL Server Integration Services (SSIS)](../load-data-to-from-excel-with-ssis.md).

## Access Modes
 The Excel source provides four different data access modes for extracting data:  
  
-   A table or view.  
  
-   A table or view specified in a variable.  
  
-   The results of an SQL statement. The query can be a parameterized query.  
  
-   The results of an SQL statement stored in a variable.  
  
 The Excel source uses an Excel connection manager to connect to a data source, and the connection manager specifies the workbook file to use. For more information, see [Excel Connection Manager](../../integration-services/connection-manager/excel-connection-manager.md).  
  
 The Excel source has one regular output and one error output.  
  
## Excel Source Configuration  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects all the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [Excel Custom Properties](../../integration-services/data-flow/excel-custom-properties.md)  
  
 For information about looping through a group of Excel files, see [Loop through Excel Files and Tables by Using a Foreach Loop Container](../../integration-services/control-flow/loop-through-excel-files-and-tables-by-using-a-foreach-loop-container.md).  
  
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
|Table name or view name variable|Specify the worksheet or range name in a variable.<br /><br /> **Related information:** [Use Variables in Packages](https://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787)|  
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
[Load data from or to Excel with SQL Server Integration Services (SSIS)](../load-data-to-from-excel-with-ssis.md)  
[Excel Destination](excel-destination.md)  
[Excel Connection Manager](../connection-manager/excel-connection-manager.md)
