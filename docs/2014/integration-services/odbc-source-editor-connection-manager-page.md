---
title: "ODBC Source Editor (Connection Manager Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssis.designer.odbcsource.connection.f1"
ms.assetid: e2c8dc83-6394-4d6c-9232-97e94be72431
author: janinezhang
ms.author: janinez
manager: craigg
---
# ODBC Source Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **ODBC Source Editor** dialog box to select the ODBC connection manager for the source. This page also lets you select a table or view from the database.  
  
 For more information about the ODBC source, see [ODBC Source](data-flow/odbc-source.md).  
  
## Task List  
 **To open the ODBC Source Editor Connection Manager Page**  
  
-   In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../includes/ssiscurrent-md.md)] package that has the ODBC source.  
  
-   On the **Data Flow** tab, double-click the ODBC source.  
  
## Options  
  
### Connection manager  
 Select an existing ODBC connection manager from the list, or click **New** to create a new connection. The connection can be to any ODBC-supported database.  
  
### New  
 Click **New**. The **Configure ODBC Connection Manager Editor** dialog box opens where you can create a new ODBC connection manager.  
  
### Data Access Mode  
 Select the method for selecting data from the source. The options are shown in the following table:  
  
|Option|Description|  
|------------|-----------------|  
|Table Name|Retrieve data from a table or view in the ODBC data source. When you select this option, select a value from the list for the following:|  
||**Name of the table or the view**: Select an available table or view from the list or type a regular expression to identify the table.|  
||This list contains the first 1000 tables only. If your database contains more than 1000 tables, you can type the beginning of a table name or use the (*) wild card to enter any part of the name to display the table or tables you want to use.|  
|SQL command|Retrieve data from the ODBC data source by using an SQL query. You should write the query in the syntax of the source database you are working with. When you select this option, enter a query in one of the following ways:|  
||Enter the text of the SQL query in the **SQL command text** field.|  
||Click **Browse** to load the SQL query from a text file.|  
||Click **Parse query** to verify the syntax of the query text.|  
  
### Preview  
 Click **Preview** to view up to the first 200 rows of the data extracted from the table or view you selected.  
  
## See Also  
 [ODBC Source Custom Properties](data-flow/odbc-source-custom-properties.md)   
 [ODBC Source Editor &#40;Columns Page&#41;](../../2014/integration-services/odbc-source-editor-columns-page.md)   
 [ODBC Source Editor &#40;Error Output Page&#41;](../../2014/integration-services/odbc-source-editor-error-output-page.md)  
  
  
