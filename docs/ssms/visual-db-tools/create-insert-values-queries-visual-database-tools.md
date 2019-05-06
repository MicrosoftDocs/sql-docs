---
title: "Create Insert Values Queries (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "inserting values"
  - "queries [SQL Server], types"
  - "adding values"
  - "row additions [SQL Server], Insert Values query"
  - "inserting rows"
  - "Insert Values query"
  - "adding rows"
  - "table values [SQL Server]"
ms.assetid: 2d4b2f6d-cc09-434b-8a0e-ccce40628064
author: "markingmyname"
ms.author: "maghan"
manager: craigg

---
# Create Insert Values Queries (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
You can create a new row in the current table using an Insert Values query. When you create an Insert Values query, you specify:  
  
-   The database table to add the row to.  
  
-   The columns whose contents you want to add.  
  
-   The value or expression to insert into the individual columns.  
  
For example, the following query adds a row to the `titles` table, specifying values for the title, type, publisher, and price:  
  
```  
INSERT INTO titles  
         (title_id, title, type, pub_id, price)  
VALUES   ('BU9876', 'Creating Web Pages', 'business', '1389', '29.99')  
```  
  
When you create an Insert Values query, the Criteria pane changes to reflect the only options available for inserting a new row: the column name and the value to insert.  
  
> [!CAUTION]  
> You cannot undo the action of executing an Insert Values query. As a precaution, back up your data before executing the query.  
  
### To create an Insert Values query  
  
1.  Add the table you want to update to the Diagram pane.  
  
2.  From the **Query Designer** menu point to **Change Type**, and then click **Insert Values**.  
  
    > [!NOTE]  
    > If more than one table is displayed in the Diagram pane when you start the Insert Values query, the Query and View Designer displays the [Choose Target Table for Insert Values Dialog Box](../../ssms/visual-db-tools/choose-target-table-for-insert-values-dialog-box-visual-database-tools.md) to prompt you for the name of the table to update.  
  
3.  In the Diagram pane, click the check box for each column for which you want to supply new values. Those columns will show in the Criteria pane. Columns will be updated only if you add them to the query.  
  
4.  In the **New Value** column of the Criteria pane, enter the new value for the column. You can enter literal values, column names, or expressions. The value must match (or be compatible with) the data type of the column you are updating.  
  
    > [!CAUTION]  
    > The Query and View Designer cannot check that a value fits within the length of the column you are inserting. If you provide a value that is too long, it might be truncated without warning. For example, if a `name` column is 20 characters long but you specify an insert value of 25 characters, the last 5 characters might be truncated.  
  
When you execute an Insert Values query, no results are reported in the [Results Pane](../../ssms/visual-db-tools/results-pane-visual-database-tools.md). Instead, a message appears indicating how many rows were changed.  
  
## See Also  
[Supported Query Types &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/supported-query-types-visual-database-tools.md)  
[Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
[Perform Basic Operations with Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/perform-basic-operations-with-queries-visual-database-tools.md)  
  
