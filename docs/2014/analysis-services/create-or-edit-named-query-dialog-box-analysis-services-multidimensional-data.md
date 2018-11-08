---
title: "Create or Edit Named Query Dialog Box (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.dsvdesigner.createnamedquery.f1"
helpviewer_keywords: 
  - "Create Named Query dialog box"
ms.assetid: 8e192ad6-a0b1-4e21-bb3f-087c93e62941
author: minewiskan
ms.author: owend
manager: craigg
---
# Create or Edit Named Query Dialog Box (Analysis Services - Multidimensional Data)
  Use the **Create/Edit Named Query** dialog box in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] to create or edit a named query in **Data Source View Designer**. A named query can be treated as a table, on which you can base other [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] objects. You can display the **Create/Edit Named Query** dialog box by:  
  
-   Clicking **New Named Query** on the **Toolbar** pane of **Data Source View Designer**.  
  
-   Right-clicking the **Diagram** pane of **Data Source View Designer** and selecting **New Named Query**.  
  
-   Right-clicking the name of a named query in the **Diagram** or **Tables** pane of **Data Source View Designer** and selecting **Edit Named Query**.  
  
 The query entered must be a valid query command for the underlying provider. The query is prepared with the underlying provider for validation, and to identify the columns returned. The dialog box can present two views:  
  
-   Visual Database Tools (VDT) Query Builder  
  
     For all users, the VDT Query Builder view provides a set of user interface tools for visually constructing and testing a SQL query.  
  
-   Generic Query Builder  
  
     For advanced users, the Generic Query Builder view provides a simpler, more direct user interface for constructing and testing a SQL query.  
  
## Options  
 **Name**  
 Type the name of the query.  
  
 **Description**  
 Type the optional description of the query.  
  
 **Data Source**  
 Specifies the data source for the query.  
  
 **Query definition**  
 The query definition provides a toolbar and panes in which to define and test the query, depending on the selected view.  
  
 **Toolbar**  
 Use the toolbar to manage datasets, select panes to display, and control various query functions.  
  
|Value|Description|  
|-----------|-----------------|  
|**Switch to Generic Query Builder**|Select to display only the options available to the Generic Query Builder view. Only the following options are displayed:<br />**SQL pane**<br />**Result pane**<br />**Toolbar**, containing only **Switch to VDT Query Builder** and **Run**<br /><br /> <br /><br /> Note: This option is displayed only if **Switch to VDT Query Builder** is selected.|  
|**Switch to VDT Query Builder**|Select to display all of the options available to the Visual Database Tools (VDT) Query Builder view.<br /><br /> Note: This option is displayed only if **Switch to Generic Query Builder** is selected.|  
|**Show/Hide Diagram Pane**|Shows or hides the **Diagram pane**.<br /><br /> **Note** This option is displayed only if **Switch to VDT Query Builder** is selected.|  
|**Show/Hide Grid Pane**|Shows or hides the **Grid pane**.<br /><br /> Note: This option is displayed only if **Switch to VDT Query Builder** is selected.|  
|**Show/Hide SQL Pane**|Shows or hides the **SQL pane**.<br /><br /> Note: This option is displayed only if **Switch to VDT Query Builder** is selected.|  
|**Show/Hide Result Pane**|Shows or hides the **Result pane**.<br /><br /> Note: This option is displayed only if **Switch to VDT Query Builder** is selected.|  
|**Run**|Runs the query. Results are displayed in the **Result pane**.|  
|**Verify SQL**|Verifies the SQL statement in the query.<br /><br /> Note: This option is displayed only if **Switch to VDT Query Builder** is selected.|  
|**Sort Ascending**|Sorts output rows on the selected column in the **Grid pane**, in ascending order.<br /><br /> Note: This option is displayed only if **Switch to VDT Query Builder** is selected.|  
|**Sort Descending**|Sorts output rows on the selected column in the **Grid pane**, in descending order.<br /><br /> Note: This option is displayed only if **Switch to VDT Query Builder** is selected.|  
|**Remove Filter**|Removes sort criteria, if applicable, for the selected row in the **Grid pane**.<br /><br /> Note: This option is displayed only if **Switch to VDT Query Builder** is selected.|  
|**Use Group By**|Adds grouping functionality to the query.<br /><br /> Note: This option is displayed only if **Switch to VDT Query Builder** is selected.|  
|**Add Table**|Displays the **Add Table** dialog box to add a new table or view to the query. For more information about the **Add Table** dialog box, see [Add Table Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](add-table-dialog-box-analysis-services-multidimensional-data.md).<br /><br /> Note: This option is displayed only if **Switch to VDT Query Builder** is selected.|  
  
 **Diagram pane**  
 Displays the objects referenced by the query as a diagram. The diagram shows the tables included in the query, and how they are joined. Select or clear the check box next to a column in a table to add or remove it from the query output.  
  
 When you add tables to the query, the dialog box creates joins between tables based on the keys in the table. To add a join, drag a field from one table onto a field in another table. To manage a join, right-click the join.  
  
 Right-click the **Diagram pane** to add or remove tables, select all the tables, and show or hide panes.  
  
> [!NOTE]  
>  The contents of the **Diagram pane**, **Grid pane**, and **SQL pane** are synchronized, so that changes in one pane are reflected in the other two panes.  
  
> [!IMPORTANT]  
>  Changing query types is not supported by the dialog box.  
  
 **Grid pane**  
 Displays the objects referenced by the query in a grid. You can use this pane to add and remove columns to the query and change the settings for each column.  
  
> [!NOTE]  
>  The contents of the **Diagram pane**, **Grid pane**, and **SQL pane** are synchronized, so that changes in one pane are reflected in the other two panes.  
  
 **SQL pane**  
 Displays the query as a SQL statement. Type to change the SQL statement for the query.  
  
> [!NOTE]  
>  The contents of the **Diagram pane**, **Grid pane**, and **SQL pane** are synchronized, so that changes in one pane are reflected in the other two panes.  
  
 **Result pane**  
 Displays the results of the query when you click **Run** on the **Toolbar** pane.  
  
## See Also  
 [Analysis Services Designers and Dialog Boxes &#40;Multidimensional Data&#41;](analysis-services-designers-and-dialog-boxes-multidimensional-data.md)   
 [Define Named Queries in a Data Source View &#40;Analysis Services&#41;](multidimensional-models/define-named-queries-in-a-data-source-view-analysis-services.md)  
  
  
