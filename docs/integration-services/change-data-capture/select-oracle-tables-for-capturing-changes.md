---
title: "Select Oracle Tables for Capturing Changes | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "selOraTabDia"
ms.assetid: 2e295dc8-999d-4c4d-96cc-1519674b47a4
author: janinezhang
ms.author: janinez
manager: craigg
---
# Select Oracle Tables for Capturing Changes
  Use this dialog box to select the tables that are included in the CDC instance. The tables selected are added to the list in the **Select Tables and Columns** page of the New Instance wizard. You can do the following in this dialog box.  
  
 By default, no tables are included in the list of tables in this dialog box. You can select the check box at the top of the check box column to select all of the tables or search for specific tables.  
  
 **To search for specific tables**  
 Enter search criteria as follows, and then click **Search**:  
  
-   **Schema**: Select a database schema from the list. Only tables that have that schema will be included in the list.  
  
-   **Table Name Pattern**: Enter any string of characters. Only tables that include the character string entered are displayed.  
  
> [!NOTE]  
>  You can enter criteria in one or both of these fields.  
  
-   **Display first 1000 matching tables**: By default this check box is selected. It limits the display to the first 1000 matching tables. If you clear the check box, all tables that match the criteria are displayed. If there are a large number of tables, it may take a long time to display the list.  
  
 **To select tables to include in the CDC instance**  
 Click the check box next to any of the tables you want to include, and then click **Add**. The tables are added to the list in the New Instance Wizard **Select Tables and Columns** page.  
  
 Click **Close** to close the dialog box without adding any additional tables.  
  
> [!NOTE]  
>  If you select a table that includes a non-supported data type, you will see an error message and the table will not be included.  
  
## See Also  
 [How to Create the SQL Server Change Database Instance](../../integration-services/change-data-capture/how-to-create-the-sql-server-change-database-instance.md)   
 [Select Oracle Tables and Columns](../../integration-services/change-data-capture/select-oracle-tables-and-columns.md)  
  
  
