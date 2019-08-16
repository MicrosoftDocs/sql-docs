---
title: "Load Data by Using the ODBC Destination | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 339ec0a8-922e-48c0-97b3-fc5ee34f95e3
author: janinezhang
ms.author: janinez
manager: craigg
---
# Load Data by Using the ODBC Destination
  This procedure shows how to load data by using the ODBC destination. To add and configure an ODBC destination, the package must already include at least one Data Flow task and source.  
  
### To load data using an ODBC destination  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package you want.  
  
2.  Click the **Data Flow** tab, and then, from the **Toolbox**, drag the ODBC destination to the design surface.  
  
3.  Drag an available output of a data flow component to the input of the ODBC destination.  
  
4.  Double-click the ODBC destination.  
  
5.  In the **ODBC Destination Editor** dialog box, on the **Connection Manager** page, select an existing ODBC connection manager or click **New** to create a new connection manager.  
  
6.  Select the data access method.  
  
    -   **Table Name - Batch**: Select this option to configure the ODBC destination to work in batch mode. When you select this option, you can set the **Batch size**.  
  
    -   **Table Name - Row by Row**: Select this option to configure the ODBC destination to insert each of the rows into the destination table one at a time. When you select this option, the data is loaded into the table one row at a time.  
  
7.  In the **Name of the table or the view** field, select an available table or view from the database from the list or type in a regular expression to identify the table.This list contains the first 1000 tables only. If your database contains more than 1000 tables, you can type the beginning of a table name or use the (*) wild card to enter any part of the name to display the table or tables you want to use.  
  
8.  You can click **Preview** to view up to 200 rows of the data from the table selected in the ODBC destination.  
  
9. Click **Mappings** and then map columns from the **Available Input Columns** list to columns in the **Available Destination Columns** list by dragging columns from one list to another.  
  
10. To configure the error output, click **Error Output**.  
  
11. Click **OK**.  
  
12. To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [ODBC Destination Editor &#40;Connection Manager Page&#41;](../odbc-destination-editor-connection-manager-page.md)   
 [ODBC Destination Editor &#40;Mappings Page&#41;](../odbc-destination-editor-mappings-page.md)   
 [ODBC Source Editor &#40;Error Output Page&#41;](../odbc-source-editor-error-output-page.md)  
  
  
