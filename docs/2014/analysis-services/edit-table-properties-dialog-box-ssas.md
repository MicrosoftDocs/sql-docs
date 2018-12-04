---
title: "Edit Table Properties Dialog Box (SSAS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.edittablepropdb.f1"
ms.assetid: 8d913e83-7246-44cc-8fc7-31729023c0d8
author: minewiskan
ms.author: owend
manager: craigg
---
# Edit Table Properties Dialog Box (SSAS)
  The **Edit Table Properties** dialog box enables you to view and modify the properties of tables that are imported into the model designer by using the Table Import Wizard. To access this dialog box, in the model designer, select a table, then click the **Table** menu, and then click **Table Properties**.  
  
## UIElement List  
 The options for this dialog box are different depending on whether you originally imported data by selecting tables from a list or by using a SQL query.  
  
## Table Preview Mode  
 **Table name**  
 Displays the name of the data table in the model.  
  
> [!NOTE]  
>  You cannot edit the name here. However, you can change the name of the table by right-clicking the table tab at the bottom of the model designer.  
  
 **Connection name**  
 Displays the name of the connection that is currently in use.  
  
 **Source name**  
 Display or change the table from which the data is obtained.  
  
 If you change the source to a table that has different columns than the current table, a message is displayed warning that the columns are different. You must then select the columns that you want to put in the current table and click **Save**. You can replace the entire table by selecting the check box at the left of the table.  
  
> [!NOTE]  
>  When you change the data source of a table, you essentially replace the contents of the current table with the contents of the new source table.  
  
 **Column names from**  
 |||  
|-|-|  
|**Source**|Select this option to replace the current column names with the column names from the selected source table.|  
|**Model**|Select this option to use the current column names as they exist in the model.|  
  
 **Refresh preview**  
 Click to view the columns of data in the currently selected source table.  
  
 **Switch to**  
 |||  
|-|-|  
|**Table preview**|Select this option to preview the selected table and a limited number of rows of data.|  
|**Query editor**|Select this option to view the query against the selected data source. This option is not available for all data sources.|  
  
 **Checkbox in the column header**  
 Select the checkbox to include the column in the data import. Clear the checkbox to remove the column from the data import.  
  
 **Down-arrow button in the column header**  
 Filter data in the column.  
  
 **Clear row filters**  
 Click to remove any filters that have been applied.  
  
 **OK**  
 Click to apply all changes that you made, including replacing columns.  
  
## Query Design Mode  
 **Table name**  
 Displays the name of the data table in the model.  
  
> [!NOTE]  
>  You cannot edit the name here; however, you can change the name of the table by right-clicking the table tab at the bottom of the designer.  
  
 **Connection name**  
 Displays the name of the connection that is currently in use.  
  
 **Switch to**  
 |||  
|-|-|  
|**Table preview**|Select this option to preview the selected table and a few rows of data.|  
|**Query editor**|Select this option to view the query that will be issued against the selected data source.|  
  
 **Sql statement**  
 Displays the SQL statement that is issued against the current data source to retrieve rows. By default, all rows are retrieved, but you can retrieve a subset of rows, either by designing a filter, or by manually editing the SQL statement.  
  
 **Validate**  
 Click to verify that the statement is syntactically correct for the selected data source and provider.  
  
 **Design**  
 Click to open a visual query designer and build a query statement. For information about how to use the designer, press F1 from the designer.  
  
 **OK**  
 Click to apply all changes that you made, including replacing columns.  
  
## See Also  
 [Tables and Columns &#40;SSAS Tabular&#41;](tabular-models/tables-and-columns-ssas-tabular.md)  
  
  
