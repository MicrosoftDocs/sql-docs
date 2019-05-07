---
title: "Issues of Database Evolution (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "compatibility [SQL Server], multuser database changes"
  - "database evolution [SQL Server]"
ms.assetid: 1ed6ae10-d212-4ec2-8569-1b94ab1cba6d
author: "markingmyname"
ms.author: "maghan"
manager: craigg

---
# Issues of Database Evolution (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
If you change the structure of a deployed database, you must take special care that your alteration is compatible with the existing data and database structure. You might need to take special steps when you make the following modifications:  
  
-   **Adding a Constraint** If you add a constraint, the database might already contain data that does not satisfy it. When you try to save the new constraint, the [Post-Save Notifications Dialog Box &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/post-save-notifications-dialog-box-visual-database-tools.md) informs you that the database server could not create the constraint. To force the database to accept the new constraint, you can clear the **Check existing data on creation** check box.  
  
-   **Adding a Relationship** If you add a relationship, the database might already contain rows of the foreign-key table that do not have corresponding rows in the primary-key table. That is, the existing data might not satisfy referential integrity. When you try to save the new relationship, the[Post-Save Notifications Dialog Box &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/post-save-notifications-dialog-box-visual-database-tools.md) informs you that the database server could not save the revised foreign-key table. To force the database to accept the modification, you can clear the **Check existing data on creation** check box.  
  
-   **Modifying a Table Contributing to an Indexed View** If you modify a table that contributes to a Microsoft SQL Server indexed view, the indexes on the view will be lost. See the SQL Server Books Online for information on recreating indexes.  
  
-   **Deleting an Object** If you delete an object, such as a column, table, or view, check first to be sure that the object is not referenced by another object in the database.  
  
No matter how you alter the database design, you should retain a history of the alterations. One approach is to retain SQL scripts for all modifications that you ever make to your production database.  
  
## See Also  
[Working with Constraints (Visual Database Tools)](https://msdn.microsoft.com/637098af-2567-48f8-90f4-b41df059833e)  
[Multiuser Environments &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/multiuser-environments-visual-database-tools.md)  
  
