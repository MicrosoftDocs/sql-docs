---
title: "Work with Data in the Results Pane (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "View Designer, Results pane"
  - "queries [Visual Database Tools]"
  - "result sets [SQL Server], queries"
  - "Query Designer [SQL Server], Results pane"
  - "results [SQL Server], query"
  - "Visual Database Tools [SQL Server], queries"
  - "queries [SQL Server], results"
  - "Results pane"
ms.assetid: 4f8a0080-91ef-4442-83ae-53be2f478c54
author: "markingmyname"
ms.author: "maghan"
manager: craigg

---
# Work with Data in the Results Pane (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
After you run a query or view, the results are shown in the Results pane. You can then work with those results. For example, you can add and delete rows, input or change data, and easily navigate through large results sets.  
  
The following information can help you avoid problems and work effectively with your results sets.  
  
## Returning the results set  
You can return results from either a query or a view and can choose whether to open just the results pane or all panes. In either case the query or view will open in Query and View Designer. The difference is that one opens with only the Results pane showing and the other opens with all windows that have been selected in the Options dialog box. The default is all four panes (Results, SQL, Diagram, and Criteria).  
  
For more information see [Open Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/open-queries-visual-database-tools.md).  
  
To change the design of the query or view so it returns a different set of results or returns records in a different order see the topics listed in [Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md).  
  
You can also determine whether to return all or part of the results set in two ways--stop the query as it runs or choose how much of results to return before the query is run.  
  
## Navigating in the results pane  
You can quickly navigate through the records using the navigation bar at the bottom of the Results pane.  
  
There are buttons for going to the first and last records, the next and previous records, and for going to a particular record.  
  
To go to a particular record, type the number of the row in the text box in the navigation bar and then press ENTER.  
  
For information about using keyboard shortcuts in the Query and View Designer see [Navigate in the Query and View Designer &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/navigate-in-the-query-and-view-designer-visual-database-tools.md).  
  
## Committing changes to the database  
The Results pane uses optimistic concurrency control so the grid shows a copy of the data in the database rather than an entirely live view. This way changes are only committed to the database after you move off of a row. This allows more than one user to work with the database at the same time. If there are conflicts (for example if another user changed the same row you changed and committed it to the database before you did) you will receive a message telling you of the conflict and offering resolutions.  
  
## Undo changes using ESC  
You can only undo a change if it hasn't yet been committed to the database. The data is not committed if you haven't moved off of the record or if once you do move off the record you get an error message indicating the change won't be committed. If it hasn't been committed you can undo the change by using the ESC key.  
  
To undo all changes in a row, move to a cell in that row that you have not edited and press the ESC key.  
  
To undo changes to a particular cell that you have edited, move to that cell press the ESC key.  
  
## Adding or deleting data in the database  
To see how your database design is working you may need to add sample data to the database. You can enter it into the results pane directly or you can copy it from another program, such as notepad or Excel, and paste it into the results pane.  
  
In addition to copying rows into the Results pane you can add new records or modify or delete existing ones. For more information see [Add New Rows in the Results Pane &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/add-new-rows-in-the-results-pane-visual-database-tools.md), [Delete Rows in the Results Pane &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/delete-rows-in-the-results-pane-visual-database-tools.md), and [Edit Rows in the Results Pane &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/edit-rows-in-the-results-pane-visual-database-tools.md).  
  
## Tips for working with NULL values and empty cells  
When you click on an empty row to add a new record, the initial value for all columns is *NULL*. If a column allows null values you can leave it as is.  
  
If you want to replace a non-null value with null, type NULL in capital letters. The Results pane will give the word italic formatting to indicate that it is to be recognized as a null value rather than as a string.  
  
To type in the string "null" type the letters without quotes. As long as at least one of the letters is in lower case, the value will be treated as a string rather than a null value.  
  
Values for columns with a binary data type will have NULL values by default. These values can't be changed in the Results pane.  
  
To input an empty space instead of using null, delete the existing text and move off of the cell.  
  
## Validating data  
The Query and View Designer can validate some kinds of data against the columns properties. For example, if you enter "abc" into a column with a float data type, you will receive an error and the change will not be committed to the database.  
  
The quickest way to see the data type of a column when you're in the Results pane is to open the Diagram pane and hover over the name of the column in the table or table-valued object.  
  
> [!NOTE]  
> The maximum length the Results pane can show for a text data type is 2,147,483,647.  
  
## Keeping the results set synchronized with the query definition  
While you're working on the results of a query or view it is possible for the records in the results pane to get out of synchronization with the queries definition. For example, if you ran a query for four out of five columns in a table, then used the Diagram pane to add the fifth column to the definition of the query, that fifth column's data will not automatically be added to the results pane. To make the results pane reflect the new query definition, run the query again.  
  
You can tell if this happens--an alert icon and the text "Query Changed" appears in the lower-right corner of the results pane and the icon is repeated in the upper-left corner of the pane.  
  
## Reconciling changes made by multiple users  
While you're working on the results of a query or view it is possible fore the records to be changed by a different user who is also working with the database.  
  
If this happens you will receive a notice as soon as you move off of the cell with the conflict. You will then be able to override the other user's change, update your results pane with the other user's change, or keep editing your results pane without reconciling the differences. If you choose not to reconcile the differences your changes will not be committed to the database.  
  
## Limitations in the Results pane  
  
### What can not be updated  
These tips may help you work successfully with data in the Results pane.  
  
-   Queries that include columns from more than one table or view can't be updated.  
  
-   Views can only be updated if the database constraints allow it.  
  
-   Results returned by a stored procedure can't be updated.  
  
-   Queries or views using the GROUP BY, DISTINCT, or TO XML clauses are not updatable.  
  
-   Results returned by table-valued functions can only be updated in some cases.  
  
-   Data in columns that result from an expression in the query.  
  
-   Data that was not successfully translated by the provider.  
  
### What can not be represented fully  
What is returned to the Results pane from the database is greatly controlled by the provider for the data source you are using. The Results pane can't always translate the data from all database management systems. Here are come cases where this is so.  
  
-   Binary data types are often not useful for people working in the Results pane and they can take a very long time to download. So they are represented by *<Binary data>* or *Null*.  
  
-   Precision and scale can not always be preserved. For example, the Results pane supports a precision of 27. If data is of a data type with a greater precision, the data may be truncated or may be represented by *<Unable to read data>*.  
  
## See Also  
[Perform Basic Operations with Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/perform-basic-operations-with-queries-visual-database-tools.md)  
[Specify Search Criteria &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/specify-search-criteria-visual-database-tools.md)  
  
