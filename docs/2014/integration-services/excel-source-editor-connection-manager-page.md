---
title: "Excel Source Editor (Connection Manager Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.excelsourceadapter.connection.f1"
helpviewer_keywords: 
  - "Excel Source Editor"
ms.assetid: 428e04e0-ad98-45d0-8345-12ec1b67b2eb
author: janinezhang
ms.author: janinez
manager: craigg
---
# Excel Source Editor (Connection Manager Page)
  Use the **Connection Manager** node of the **Excel Source Editor** dialog box to select the [!INCLUDE[ofprexcel](../includes/ofprexcel-md.md)] workbook for the source to use. The Excel source reads data from a worksheet or named range in an existing workbook.  
  
> [!NOTE]  
>  The `CommandTimeout` property of the Excel source is not available in the **Excel Source Editor**, but can be set by using the **Advanced Editor**. For more information on this property, see the Excel Source section of [Excel Custom Properties](data-flow/excel-custom-properties.md).  
  
 To learn more about the Excel source, see [Excel Source](data-flow/excel-source.md).  
  
## Static Options  
 **OLE DB connection manager**  
 Select an existing Excel connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection manager by using the **Excel Connection Manager** dialog box.  
  
 **Data access mode**  
 Specify the method for selecting data from the source.  
  
|Value|Description|  
|-----------|-----------------|  
|Table or view|Retrieve data from a worksheet or named range in the Excel file.|  
|Table name or view name variable|Specify the worksheet or range name in a variable.<br /><br /> **Related information:** [Use Variables in Packages](../../2014/integration-services/use-variables-in-packages.md)|  
|SQL command|Retrieve data from the Excel file by using a SQL query. For information about query syntax, see [Excel Source](data-flow/excel-source.md).|  
|SQL command from variable|Specify the SQL query text in a variable.|  
  
 **Preview**  
 Preview results by using the **Data View** dialog box. Preview can display up to 200 rows.  
  
## Data Access Mode Dynamic Options  
  
### Data access mode = Table or view  
 **Name of the Excel sheet**  
 Select the name of the worksheet or named range from a list of those available in the Excel workbook.  
  
### Data access mode = Table name or view name variable  
 **Variable name**  
 Select the variable that contains the name of the worksheet or named range.  
  
### Data access mode = SQL command  
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
  
### Data access mode = SQL command from variable  
 **Variable name**  
 Select the variable that contains the text of the SQL query.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Excel Source Editor &#40;Columns Page&#41;](../../2014/integration-services/excel-source-editor-columns-page.md)   
 [Excel Source Editor &#40;Error Output Page&#41;](../../2014/integration-services/excel-source-editor-error-output-page.md)   
 [Excel Connection Manager](connection-manager/excel-connection-manager.md)   
 [Loop through Excel Files and Tables by Using a Foreach Loop Container](control-flow/foreach-loop-container.md)  
  
  
