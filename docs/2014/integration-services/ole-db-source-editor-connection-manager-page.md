---
title: "OLE DB Source Editor (Connection Manager Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.oledbsourceadapter.connection.f1"
helpviewer_keywords: 
  - "OLE DB Source Editor"
ms.assetid: 53699902-8699-4547-b56b-a5b2079e98b6
author: douglaslms
ms.author: douglasl
manager: craigg
---
# OLE DB Source Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **OLE DB Source Editor** dialog box to select the OLE DB connection manager for the source. This page also lets you select a table or view from the database.  
  
> [!NOTE]  
>  To load data from a data source that uses [!INCLUDE[msCoName](../includes/msconame-md.md)] Office Excel 2007, use an OLE DB source. You cannot use an Excel source to load data from an Excel 2007 data source. For more information, see [Configure OLE DB Connection Manager](configure-ole-db-connection-manager.md).  
>   
>  To load data from a data source that uses [!INCLUDE[msCoName](../includes/msconame-md.md)] Office Excel 2003 or earlier, use an Excel source. For more information, see [Excel Source Editor &#40;Connection Manager Page&#41;](../../2014/integration-services/excel-source-editor-connection-manager-page.md).  
  
> [!NOTE]  
>  The `CommandTimeout` property of the OLE DB source is not available in the **OLE DB Source Editor**, but can be set by using the **Advanced Editor**. For more information on this property, see the Excel Source section of [OLE DB Custom Properties](data-flow/ole-db-custom-properties.md).  
  
 To learn more about the OLE DB source, see [OLE DB Source](data-flow/ole-db-source.md).  
  
## Open the OLE DB Source Editor (Connection Manager Page  
  
1.  Add the OLE DB source to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package, in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
2.  Right-click the source component and when click **Edit**.  
  
3.  Click **Connection Manager**.  
  
## Static Options  
 **OLE DB connection manager**  
 Select an existing connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection manager by using the **Configure OLE DB Connection Manager** dialog box.  
  
 **Data access mode**  
 Specify the method for selecting data from the source.  
  
|Option|Description|  
|------------|-----------------|  
|Table or view|Retrieve data from a table or view in the OLE DB data source.|  
|Table name or view name variable|Specify the table or view name in a variable.<br /><br /> **Related information:** [Use Variables in Packages](../../2014/integration-services/use-variables-in-packages.md)|  
|SQL command|Retrieve data from the OLE DB data source by using a SQL query.|  
|SQL command from variable|Specify the SQL query text in a variable.|  
  
 **Preview**  
 Preview results by using the **Data View** dialog box. **Preview** can display up to 200 rows.  
  
> [!NOTE]  
>  When you preview data, columns with a CLR user-defined type do not contain data. Instead the values \<value too big to display> or System.Byte[] display. The former displays when the data source is accessed using the SQL OLE DB provider, the latter when using the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Native Client provider.  
  
## Data Access Mode Dynamic Options  
  
### Data access mode = Table or view  
 **Name of the table or the view**  
 Select the name of the table or view from a list of those available in the data source.  
  
### Data access mode = Table name or view name variable  
 **Variable name**  
 Select the variable that contains the name of the table or view.  
  
### Data access mode = SQL command  
 **SQL command text**  
 Enter the text of a SQL query, build the query by clicking **Build Query**, or locate the file that contains the query text by clicking **Browse**.  
  
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
 [OLE DB Source Editor &#40;Columns Page&#41;](../../2014/integration-services/ole-db-source-editor-columns-page.md)   
 [OLE DB Source Editor &#40;Error Output Page&#41;](../../2014/integration-services/ole-db-source-editor-error-output-page.md)   
 [Extract Data by Using the OLE DB Source](data-flow/extract-data-by-using-the-ole-db-source.md)   
 [OLE DB Connection Manager](connection-manager/ole-db-connection-manager.md)  
  
  
