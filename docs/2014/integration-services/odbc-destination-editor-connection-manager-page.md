---
title: "ODBC Destination Editor (Connection Manager Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssis.designer.odbcdest.connection.f1"
ms.assetid: f6d9c6c2-e4c4-468b-9e0d-af7b9609614d
author: douglaslms
ms.author: douglasl
manager: craigg
---
# ODBC Destination Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **ODBC Destination Editor** dialog box to select the ODBC connection manager for the destination. This page also lets you select a table or view from the database  
  
 For more information about the ODBC destination, see [ODBC Destination](data-flow/odbc-destination.md).  
  
 **To open the ODBC Destination Editor Connection Manager Page**  
  
## Task List  
  
-   In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../includes/ssiscurrent-md.md)] package that has the ODBC destination.  
  
-   On the **Data Flow** tab, double-click the ODBC destination.  
  
-   In the **ODBC Destination Editor**, click **Connection Manager**.  
  
## Options  
  
### Connection manager  
 Select an existing ODBC connection manager from the list, or click New to create a new connection. The connection can be to any ODBC-supported database.  
  
### New  
 Click **New**. The **Configure ODBC Connection Manager Editor** dialog box opens where you can create a new connection manager.  
  
### Data Access Mode  
 Select the method for loading data to the destination. The options are shown in the following table:  
  
|Option|Description|  
|------------|-----------------|  
|Table Name - Batch|Select this option to configure the ODBC destination to work in batch mode. When you select this option the following options are available:|  
||**Name of the table or the view**: Select an available table or view from the list.<br /><br /> This list contains the first 1000 tables only. If your database contains more than 1000 tables, you can type the beginning of a table name or use the (\*) wild card to enter any part of the name to display the table or tables you want to use.<br /><br /> **Batch size**: Type the size of the batch for bulk loading. This is the number of rows loaded as a batch|  
|Table Name - Row by Row|Select this option to configure the ODBC destination to insert each of the rows into the destination table one at a time. When you select this option the following option is available:|  
||**Name of the table or the view**: Select an available table or view from the database from the list.<br /><br /> This list contains the first 1000 tables only. If your database contains more than 1000 tables, you can type the beginning of a table name or use the (*) wild card to enter any part of the name to display the table or tables you want to use.|  
  
### Preview  
 Click **Preview** to view up to 200 rows of data for the table that you selected.  
  
## See Also  
 [ODBC Destination Custom Properties](data-flow/odbc-destination-custom-properties.md)   
 [ODBC Destination Editor &#40;Mappings Page&#41;](../../2014/integration-services/odbc-destination-editor-mappings-page.md)   
 [ODBC Destination Editor &#40;Error Output Page&#41;](../../2014/integration-services/odbc-destination-editor-error-output-page.md)  
  
  
