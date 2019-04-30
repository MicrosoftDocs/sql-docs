---
title: "Extract Data by Using the ODBC Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 10f25703-49a2-4d45-abab-6b4da2a57ba5
author: janinezhang
ms.author: janinez
manager: craigg
---
# Extract Data by Using the ODBC Source

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  This procedure describes how to extract data by using an ODBC source. To add and configure an ODBC source, the package must already include at least one Data Flow task.  
  
### To extract data using an ODBC source  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] package you want.  
  
2.  Click the **Data Flow** tab, and then, from the **Toolbox**, drag the ODBC source to the design surface.  
  
3.  Double-click the ODBC source.  
  
4.  In the **ODBC Source Editor** dialog box, on the **Connection Manager** page, select an existing ODBC connection manager or click **New** to create a new connection manager.  
  
5.  Select the data access method.  
  
    -   **Table Name**: Select a table or view in the database or type a regular expression to identify the table to which the ODBC connection manager connects.  
  
         This list contains the first 1000 tables only. If your database contains more than 1000 tables, you can type the beginning of a table name or use the (*) wild card to enter any part of the name to display the table or tables you want to use.  
  
    -   **SQL Command**: Type an SQL Command or click **Browse** to load the SQL query from a text file.  
  
6.  You can click **Preview** to view up to 200 rows of the data extracted by the ODBC source.  
  
7.  To update the mapping between external and output columns, click **Columns** and select different columns in the **External Column** list.  
  
8.  If necessary, update the names of output columns by editing values in the **Output Column** list.  
  
9. To configure the error output, click **Error Output**.  
  
10. Click **OK**.  
  
11. To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [ODBC Source Editor &#40;Connection Manager Page&#41;](../../integration-services/data-flow/odbc-source-editor-connection-manager-page.md)   
 [ODBC Source Editor &#40;Columns Page&#41;](../../integration-services/data-flow/odbc-source-editor-columns-page.md)   
 [ODBC Source Editor &#40;Error Output Page&#41;](../../integration-services/data-flow/odbc-source-editor-error-output-page.md)  
  
  
