---
title: "Create Update Queries (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "tables [SQL Server], updating"
  - "queries [SQL Server], types"
  - "Update query"
  - "updating tables"
ms.assetid: 178b7b75-8078-4e61-b2a8-4719b9d8033d
author: "markingmyname"
ms.author: "maghan"
manager: craigg

---
# Create Update Queries (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
You can change the contents of multiple rows in one operation by using an Update query. For example, in a `titles` table you can use an Update query to add 10% to the price of all books for a particular publisher.  
  
When you create an Update query, you specify:  
  
-   The table to update.  
  
-   The columns whose contents you want to update.  
  
-   The value or expression to use to update the individual columns.  
  
-   Search conditions to define the rows you want to update.  
  
For example, the following query updates the `titles` table by adding 10% to the price of all titles for one publisher:  
  
```  
UPDATE titles  
SET price = price * 1.1  
WHERE (pub_id = '0766')  
```  
  
> [!CAUTION]  
> You cannot undo the action of executing an Update query. As a precaution, back up your data before executing the query.  
  
### To create an Update query  
  
1.  Add the table you want to update to the Diagram pane.  
  
2.  From the **Query Designer** menu point to **Change Type**, and then click **Update**.  
  
    > [!NOTE]  
    > If more than one table is displayed in the Diagram pane when you start the Update query, the Query and View Designer displays the [Choose Target Table for Insert Values Dialog Box](../../ssms/visual-db-tools/choose-target-table-for-insert-values-dialog-box-visual-database-tools.md) to prompt you for the name of the table to update.  
  
3.  In the Diagram pane, click the check box for each column for which you want to supply new values. Those columns will show in the Criteria pane. Columns will be updated only if you add them to the query.  
  
4.  In the **New Value** column of the Criteria pane, enter the update value for the column. You can enter literal values, column names, or expressions. The value must match (or be compatible with) the data type of the column you are updating.  
  
    > [!CAUTION]  
    > The Query and View Designer cannot check that a value fits within the length of the column you are updating. If you provide a value that is too long, it might be truncated without warning. For example, if a `name` column is 20 characters long but you specify an update value of 25 characters, the last 5 characters might be truncated.  
  
5.  Define the rows to update by entering search conditions in the **Filter** column. For details, see [Specify Search Criteria &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/specify-search-criteria-visual-database-tools.md).  
  
    If you do not specify a search condition, all rows in the specified table will be updated.  
  
    > [!NOTE]  
    > When you add a column to the Criteria pane for use in a search condition, the Query and View Designer also adds it to the list of columns to be updated. If you want to use a column for a search condition but not update it, clear the check box next to the column name in the rectangle representing the table or table-valued object.  
  
When you execute an Update query, no results are reported in the [Results Pane](../../ssms/visual-db-tools/results-pane-visual-database-tools.md). Instead, a message appears indicating how many rows were changed.  
  
## See Also  
[Supported Query Types &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/supported-query-types-visual-database-tools.md)  
[Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
[Perform Basic Operations with Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/perform-basic-operations-with-queries-visual-database-tools.md)  
  
