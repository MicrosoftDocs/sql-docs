---
title: "Reconcile Changes Made by Multiple Users (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "table modifications [SQL Server], multiple users"
  - "reconciling changes made by multiple users"
  - "modifications made by multiple users"
ms.assetid: fc7ed4f2-ad3d-47fc-a3ef-51e5bb069ef0
author: "markingmyname"
ms.author: "maghan"
manager: craigg

---
# Reconcile Changes Made by Multiple Users (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
In a multiuser environment, changes can be made on the same object by multiple users at once. This can happen when you're working on the structure of the object in the Table or Database Diagram designers or it can happen to values in the results returned in the Query and View designer's Results pane. This can cause conflicts that you'll want to resolve.  
  
## Conflicts in the Table or Database Diagram Designers  
For example, another user might delete or rename a table while you are working with the same or a related table in Table Designer. When you attempt to save your table, the [Database Changes Detected Dialog Box &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/database-changes-detected-dialog-box-visual-database-tools.md) notifies you that the database has been updated since you opened the table.  
  
This dialog box also displays a list of database objects that will be affected as a result of saving your table. At this point, you can take one of these actions:  
  
-   Choose **Yes** to save your table and update the database with all the changes in the list.  
  
    This action could affect tables that share the same database objects. For example, suppose you edit the `au_id` column in the `titleauthors` table while another user is working on the `authors` table which is related to the `titleauthors` table by the `au_id` column. Saving your table will affect the other user's table. Similarly, suppose that another user defined a check constraint for the `qty` column in the `sales` table. If you delete the `qty` column and save the `sales` table, the other user's check constraint will be affected.  
  
-   Choose **No** to cancel the save action.  
  
    You can then close the table without saving it. When you reopen the table it will match what is in the database.  
  
-   Choose **Save Text File** to save a list of the changes.  
  
    You can save the list of database changes shown in the **Database Changes Detected** dialog box to a text file so that you can investigate the cause of other users' changes. For example, if another user edited a table that you marked for deletion, you may want to research whether the table should be deleted before updating the database.  
  
## Conflicts in the Query and View Designer  
If you run a query or return the results of a view, the data is shown in the [Results Pane](../../ssms/visual-db-tools/results-pane-visual-database-tools.md). Multiple users can work on the same set of data at the same time, which can cause conflicts.  
  
For example, lets say you and a colleague each run a query to show all the data in the `titleauthors` table. Your colleague changes the first name in the first record returned from Barb to Barbara. At this point the database has Barbara in that field, while your result set still shows Barb. Now you type in Barbara and click off of the row. You will receive a message asking you how you want to resolve the conflict.  
  
-   Click **Yes** to update the database with your changes.  
  
    This will override your colleague's changes.  
  
-   Click **No** to have your result set updated to what's currently in the database.  
  
    This will override your changes with those of your colleague's.  
  
-   Click **Cancel** to continue to edit without resolving the conflict.  
  
    In this case you will not be able to commit your changes to the database.  
  
## See Also  
[Database Changes Detected Dialog Box &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/database-changes-detected-dialog-box-visual-database-tools.md)  
  
