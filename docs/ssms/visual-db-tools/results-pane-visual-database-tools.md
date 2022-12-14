---
description: "Results Pane (Visual Database Tools)"
title: Results Pane
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "View Designer, Results pane"
  - "result sets [SQL Server], queries"
  - "synchronization [SQL Server], query results with definition"
  - "displaying query results in grid"
  - "grid showing query results [SQL Server]"
  - "showing query results in grid"
  - "Query Designer [SQL Server], Results pane"
  - "results [SQL Server], query"
  - "viewing query results"
  - "queries [SQL Server], results"
  - "Results pane"
ms.assetid: 6309a1bc-a628-4141-8bb5-b35924bd19f9
author: markingmyname
ms.author: maghan
ms.reviewer: 

---
# Results Pane (Visual Database Tools)
[!INCLUDE[SQL Server](../../includes/applies-to-version/sqlserver.md)]
The Results pane shows the results of the most recently executed SELECT query. (The results of other query types are displayed in message boxes.) To open the results pane, open or create a query or view or return a table's data. If the results pane doesn't show by default, from the **Query Designer** menu, point to **Pane**, and then click **Results**.  
  
## What You Can Do in the Results Pane  
  
-   View the result set for the most recently executed SELECT query in a spreadsheet-like grid.  
  
-   For queries or views that show data from a single table or view, you can edit the values in individual columns in the result set, add new rows, and delete existing rows.  
  
## Limitations in the Results Pane  
  
-   Results returned by table-valued functions can only be updated in some cases.  
  
-   Queries or views that include columns from more than one table or view cannot be updated.  
  
-   Results returned by a stored procedure cannot be updated.  
  
-   Queries or views using the GROUP BY or DISTINCT clauses are not updatable.  
  
## Navigating in the Results Pane  
You can quickly navigate through the records using the navigation bar at the bottom of the Results pane.  
  
There are buttons for going to the first and last records, the next and previous records, and for going to a particular record.  
  
To go to a particular record, type the number of the row in the text box in the navigation bar and then press ENTER.  
  
For information about using keyboard shortcuts in the Query and View Designer see [Navigate in the Query and View Designer &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/navigate-in-the-query-and-view-designer-visual-database-tools.md).  
  
## Keeping the Results Set Synchronized with the Query Definition  
While you are working on the results of a query or view it is possible for the records in the results pane to get out of synchronization with the query's definition. For example, if you run a query for four out of five columns in a table, and then use the Diagram pane to add the fifth column to the definition of the query, that fifth column's data will not automatically be added to the Results pane. To make the results pane reflect the new query definition, run the query again.  
  
If a query changes, an alert icon and the text "Query Changed" appears in the lower-right corner of the results pane. The icon is repeated in the upper-left corner of the pane.  
  
## See Also  
[Create Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/create-queries-visual-database-tools.md)  
[Run Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/run-queries-visual-database-tools.md)  
[Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
[Diagram Pane &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/diagram-pane-visual-database-tools.md)  
[Criteria Pane &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/criteria-pane-visual-database-tools.md)  
[Work with Data in the Results Pane &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/work-with-data-in-the-results-pane-visual-database-tools.md)  
  
