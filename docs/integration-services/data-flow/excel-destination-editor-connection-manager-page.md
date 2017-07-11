---
title: "Excel Destination Editor (Connection Manager Page) | Microsoft Docs"
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
  - "sql13.dts.designer.exceldestadapter.connection.f1"
helpviewer_keywords: 
  - "Excel Destination Editor"
ms.assetid: fc13f725-963c-488e-91e2-20627133e842
caps.latest.revision: 43
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Excel Destination Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **Excel Destination Editor** dialog box to specify data source information, and to preview the results. The Excel destination loads data into a worksheet or a named range in a [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] workbook.  
  
> [!NOTE]  
>  The **CommandTimeout** property of the Excel destination is not available in the **Excel Destination Editor**, but can be set by using the **Advanced Editor**. In addition, certain Fast Load options are available only in the **Advanced Editor**. For more information on these properties, see the Excel Destination section of [Excel Custom Properties](../../integration-services/data-flow/excel-custom-properties.md).  
  
 To learn more about the Excel destination, see [Excel Destination](../../integration-services/data-flow/excel-destination.md).  
  
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
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)   
 [Excel Destination Editor &#40;Mappings Page&#41;](../../integration-services/data-flow/excel-destination-editor-mappings-page.md)   
 [Excel Destination Editor &#40;Error Output Page&#41;](../../integration-services/data-flow/excel-destination-editor-error-output-page.md)   
 [Loop through Excel Files and Tables by Using a Foreach Loop Container](../../integration-services/control-flow/loop-through-excel-files-and-tables-by-using-a-foreach-loop-container.md)  
  
  