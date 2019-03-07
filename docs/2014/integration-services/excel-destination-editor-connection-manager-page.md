---
title: "Excel Destination Editor (Connection Manager Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.exceldestadapter.connection.f1"
helpviewer_keywords: 
  - "Excel Destination Editor"
ms.assetid: fc13f725-963c-488e-91e2-20627133e842
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Excel Destination Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **Excel Destination Editor** dialog box to specify data source information, and to preview the results. The Excel destination loads data into a worksheet or a named range in a [!INCLUDE[ofprexcel](../includes/ofprexcel-md.md)] workbook.  
  
> [!NOTE]  
>  The `CommandTimeout` property of the Excel destination is not available in the **Excel Destination Editor**, but can be set by using the **Advanced Editor**. In addition, certain Fast Load options are available only in the **Advanced Editor**. For more information on these properties, see the Excel Destination section of [Excel Custom Properties](data-flow/excel-custom-properties.md).  
  
 To learn more about the Excel destination, see [Excel Destination](data-flow/excel-destination.md).  
  
## Static Options  
 **Excel connection manager**  
 Select an existing Excel connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection manager by using the **Excel Connection Manager** dialog box.  
  
 **Data access mode**  
 Specify the method for selecting data from the source.  
  
|Option|Description|  
|------------|-----------------|  
|Table or view|Loads data into a worksheet or named range in the Excel data source.|  
|Table name or view name variable|Specify the worksheet or range name in a variable.<br /><br /> **Related information**: [Use Variables in Packages](../../2014/integration-services/use-variables-in-packages.md)|  
|SQL command|Load data into the Excel destination by using an SQL query.|  
  
 **Name of the Excel sheet**  
 Select the excel destination from the drop-down list. If the list is empty, click **New**.  
  
 **New**  
 Click **New** to launch the **Create Table** dialog box. When you click **OK**, the dialog box creates the excel file that the **Excel Connection Manager** points to.  
  
 **View Existing Data**  
 Preview results by using the **Preview Query Results** dialog box. Preview can display up to 200 rows.  
  
> [!WARNING]  
>  If the **Excel connection manager** you selected points to an excel file that does not exist, you will see an error message when you click this button.  
  
## Data Access Mode Dynamic Options  
  
### Data access mode = Table or view  
 **Name of the Excel sheet**  
 Select the name of the worksheet or named range from a list of those available in the data source.  
  
### Data access mode = Table name or view name variable  
 **Variable name**  
 Select the variable that contains the name of the worksheet or named range.  
  
### Data access mode = SQL command  
 **SQL command text**  
 Enter the text of an SQL query, build the query by clicking **Build Query**, or locate the file that contains the query text by clicking **Browse**.  
  
 **Build Query**  
 Use the **Query Builder** dialog box to construct the SQL query visually.  
  
 **Browse**  
 Use the **Open** dialog box to locate the file that contains the text of the SQL query.  
  
 **Parse Query**  
 Verify the syntax of the query text.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Excel Destination Editor &#40;Mappings Page&#41;](../../2014/integration-services/excel-destination-editor-mappings-page.md)   
 [Excel Destination Editor &#40;Error Output Page&#41;](../../2014/integration-services/excel-destination-editor-error-output-page.md)   
 [Loop through Excel Files and Tables by Using a Foreach Loop Container](control-flow/foreach-loop-container.md)  
  
  
