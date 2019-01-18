---
title: "Add Tables to a CDC Instance | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "addTabs"
ms.assetid: ad260e19-c021-4035-9311-c02fc96ceaea
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Add Tables to a CDC Instance
  Use the Table Selection dialog box to add additional tables from the Oracle source to the CDC instance. The tables selected are added to the list in the **Tables** tab of the Properties editor.  
  
 By default, no tables are included in the list of tables in this dialog box. You can select the **(Select All)** check box or search for specific tables.  
  
 **To search for specific tables**  
 Enter search criteria as follows, and then click **Search**:  
  
-   **Schema**: Select a database schema from the list. Only tables that have that schema will be included in the list.  
  
-   **Table Name Pattern**: Enter any string of characters. Only tables that include the character string entered are displayed.  
  
> [!NOTE]  
>  You can enter criteria in one or both of these fields.  
  
-   **Display first 1000 matching tables**: By default this check box is selected. It limits the display to the first 1000 matching tables. If you clear the check box, all tables that match the criteria are displayed. If there are a large number of tables, it may take a long time to display the list.  
  
 **To select tables to include in the CDC instance**  
 Click the check box next to any of the tables you want to include, and then click **Add**. The tables are added to the list in the New Instance Wizard **Select Tables and Columns** page.  
  
 Click **Close** to close the dialog box without adding any tables.  
  
> [!NOTE]  
>  If you select a table that includes a non-supported data type, you will see an error message and the table will not be included.  
  
> [!NOTE]  
>  You can view the list of tables in the viewer. When using the viewer the information is read only. The viewer also includes a list of the captured columns in the table. For information on how to access the viewer, see [How to Manage a CDC Instance](manage-a-cdc-instance.md).  
  
## See Also  
 [How to Edit the CDC Instance Properties](how-to-edit-the-cdc-instance-properties.md)   
 [How to Manage a CDC Instance](manage-a-cdc-instance.md)   
 [Select Oracle Tables for Capturing Changes](select-oracle-tables-for-capturing-changes.md)  
  
  
