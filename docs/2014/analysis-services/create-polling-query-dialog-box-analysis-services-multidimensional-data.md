---
title: "Create Polling Query Dialog Box (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.createpollingquerydialog.f1"
ms.assetid: 0f2902b5-796a-4eb0-be03-01514dc01b9a
author: minewiskan
ms.author: owend
manager: craigg
---
# Create Polling Query Dialog Box (Analysis Services - Multidimensional Data)
  Use the **Create Polling Query** dialog box in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] to create a polling query in the **Notifications** tab of the **Storage Options** dialog box. A polling query is typically a singleton query that returns a value [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] can use to determine if changes have been made to a table or other relational object. You can display the **Create Polling Query** dialog box by clicking the ellipsis button (**...**) on the **Polling Query** column of the grid for the **Scheduled polling** option on the **Notifications** tab of the **Storage Options** dialog box. For more information about the **Notifications** tab of the **Storage Options** dialog box, see [Notifications &#40;Storage Options Dialog Box&#41; &#40;Analysis Services - Multidimensional Data&#41;](notifications-storage-options-dialog-analysis-services-multidimensional-data.md).  
  
 The type of value that should be returned by the polling query depends on the type of updates planned for the multidimensional OLAP (MOLAP) cache of the object based on the table being queried:  
  
-   If **Enable incremental update** is not selected on the **Notifications** tab of the **Storage Options** dialog box, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] fully updates the MOLAP cache for the object if a change is detected during scheduled polling. The polling query used should determine if records have been added to the table since the last polling period.  
  
-   If **Enable incremental update** is selected on the **Notifications** tab of the **Storage Options** dialog box, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] incrementally updates the MOLAP cache for the object if a change is detected during scheduled polling. The polling query used should determine the last record in the table.  
  
 For example, you can use the following polling queries to supply either full or incremental updates for the Customer dimension in the [!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)] sample [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database:  
  
|Update type|Polling query|  
|-----------------|-------------------|  
|Full update|`SELECT`<br /><br /> `COUNT(*) AS TotalCount`<br /><br /> `FROM`<br /><br /> `[dbo].[DimCustomer]`|  
|Incremental update|`SELECT`<br /><br /> `MAX([CustomerKey]) AS LastCustomerKey`<br /><br /> `FROM`<br /><br /> `[dbo].[DimCustomer]`|  
  
 For more information about full and incremental updates for scheduled polling notifications, see [Proactive Caching &#40;Partitions&#41;](multidimensional-models-olap-logical-cube-objects/partitions-proactive-caching.md).  
  
 The query entered must be a valid query command for the underlying provider. The query is prepared with the underlying provider for validation, and to identify the columns returned. The dialog box can present two views:  
  
-   Visual Database Tools (VDT) Query Builder  
  
     For all users, the VDT Query Builder view provides a set of user interface tools for visually constructing and testing a SQL query.  
  
-   Generic Query Builder  
  
     For advanced users, the Generic Query Builder view provides a simpler, more direct user interface for constructing and testing a SQL query.  
  
## Options  
 **Data Source**  
 Specifies the data source for the query.  
  
 **Query definition**  
 The query definition provides a toolbar and panes in which to define and test the query, depending on the selected view.  
  
 **Toolbar**  
 Use the toolbar to manage datasets, select panes to display, and control various query functions.  
  
|Value|Description|  
|-----------|-----------------|  
|**Switch to Generic Query Builder**|Select to display only the options available to the Generic Query Builder view. Only the following options are displayed:<br /><br /> **SQL pane**<br /><br /> **Result pane**<br /><br /> **Toolbar**, containing only **Switch to VDT Query Builder** and **Run**<br /><br /> <br /><br /> Note: This option is displayed only if **Switch to VDT Query Builder** is selected.|  
|**Switch to VDT Query Builder**|Select to display all of the options available to the Visual Database Tools (VDT) Query Builder view.<br /><br /> Note: This option is displayed only if **Switch to Generic Query Builder** is selected.|  
|**Show/Hide Diagram Pane**|Shows or hides the **Diagram pane**.<br /><br /> Note: This option is displayed only if **Switch to VDT Query Builder** is selected.|  
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
  
  
