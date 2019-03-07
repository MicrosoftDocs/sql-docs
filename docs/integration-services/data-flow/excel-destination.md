---
title: "Excel Destination | Microsoft Docs"
ms.custom: ""
ms.date: "04/02/2018"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.exceldest.f1"
  - "sql13.dts.designer.exceldestadapter.connection.f1"
  - "sql13.dts.designer.exceldestadapter.mappings.f1"
  - "sql13.dts.designer.exceldestadapter.erroroutput.f1"
helpviewer_keywords: 
  - "destinations [Integration Services], Excel"
  - "Excel [Integration Services]"
ms.assetid: 37c07446-1264-4814-b4f5-9c66d333bb24
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Excel Destination
  The Excel destination loads data into worksheets or ranges in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Excel workbooks.  

> [!IMPORTANT]
> For detailed info about connecting to Excel files, and about limitations and known issues for loading data from or to Excel files, see [Load data from or to Excel with SQL Server Integration Services (SSIS)](../load-data-to-from-excel-with-ssis.md).
  
## Access Modes  
 The Excel destination provides three different data access modes for loading data:  
  
-   A table or view.  
  
-   A table or view specified in a variable.  
  
-   The results of an SQL statement. The query can be a parameterized query.  
  
## Configure the Excel Destination  
 The Excel destination uses an Excel connection manager to connect to a data source, and the connection manager specifies the workbook file to use. For more information, see [Excel Connection Manager](../../integration-services/connection-manager/excel-connection-manager.md).  
  
 The Excel destination has one regular input and one error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects all the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [Excel Custom Properties](../../integration-services/data-flow/excel-custom-properties.md)  
  
 For more information about how to set properties, see [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
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
|Table name or view name variable|Specify the worksheet or range name in a variable.<br /><br /> **Related information**: [Use Variables in Packages](https://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787)|  
|SQL command|Load data into the Excel destination by using an SQL query.|  
  
 **Name of the Excel sheet**  
 Select the excel destination from the drop-down list. If the list is empty, click **New**.  
  
 **New**  
 Click **New** to launch the **Create Table** dialog box. When you click **OK**, the dialog box creates the excel file that the **Excel Connection Manager** points to.  
  
 **View Existing Data**  
 Preview results by using the **Preview Query Results** dialog box. Preview can display up to 200 rows.  
  
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
 [Load data from or to Excel with SQL Server Integration Services (SSIS)](../load-data-to-from-excel-with-ssis.md)  
 [Excel Source](../../integration-services/data-flow/excel-source.md)   
[Excel Connection Manager](../connection-manager/excel-connection-manager.md)
