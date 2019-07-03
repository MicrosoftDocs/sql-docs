---
title: "How to: Delete Objects and Resolve Dependencies | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "Microsoft.VisualStudio.Data.Tools.Project.HelpKeywords.SqlProjectDropDatabaseConfirmationDialog"
  - "sql.data.tools.dropdatabaseconfirmation.dialog"
  - "sql.data.tools.dropmultipledatabasesconfirmation.dialog"
ms.assetid: fb31c2b1-ca4f-4e11-a0b6-5c26430f1c8c
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# How to: Delete Objects and Resolve Dependencies
When you rename or delete an object in **SQL Server Object Explorer**, SQL Server Data Tools automatically detects all its dependency objects, and will prepare an ALTER script to rename or drop the dependency as needed.  
  
> [!WARNING]  
> The following procedures uses entities created in previous procedures in the [Connected Database Development](../ssdt/connected-database-development.md) section.  
  
### To delete a database  
  
1.  Right-click a database in **SQL Server Object Explorer**, and select **Delete**.  
  
2.  Accept all the default settings in the **Delete Database** dialog, and click **OK**.  
  
### To rename a table  
  
1.  Make sure that the `Customer` table is not opened in either the Table Designer or the Transact\-SQL Editor.  
  
2.  Expand the **Tables** node in **SQL Server Object Explorer**. Right-click the **Customer** table and select **Rename**.  
  
3.  Change the table name to **Customers** and press ENTER.  
  
4.  Notice that a **Database Update** operation is immediately invoked on your behalf. SSDT will call the sp_rename stored procedure on your behalf to rename the table. If there are any dependent objects such as foreign key constraints, they will also be updated.  
  
    > [!WARNING]  
    > Script-based dependencies such as references to a table from a view, or stored procedures are not automatically updated by SSDT. After the rename, you can use the **Error List** pane to locate all other dependencies and manually fix them.  
  
5.  Apply the change following the steps in the previous [How to: Update a Connected Database with Power Buffer](../ssdt/how-to-update-a-connected-database-with-power-buffer.md) procedure.  
  
6.  Right-click the **Customers** table in **SQL Server Object Explorer** again, and select **View Data**. Notice that table data is intact after the rename operation.  
  
7.  Right-click the **Products** table and select **View Code**. Notice that the foreign key reference has been automatically updated to `REFERENCES [dbo].[Customers] ([Id])` to reflect the renaming.  
  
### To delete a table  
  
1.  Right-click the **Customers** table in **SQL Server Object Explorer**, and select **Delete**.  
  
2.  In the **Preview Database Updates** dialog, under **User Action**, notice that SSDT has identified all the dependent objects, in this case, a foreign key reference that will be dropped.  
  
3.  Click **Update Database**.  
  
4.  Right-click the **Products** table in **SQL Server Object Explorer**, and select **View Code**. Notice that the foreign key reference to the `Customers` table is gone.  
  
    > [!WARNING]  
    > If you already have the **Products** table opened in Table Designer or Transact\-SQL Editor when the delete operation occurs, it will not automatically refresh to show the deletion of the foreign key reference. In addition, errors about unresolved references may show up in the **Error List**. To resolve this issue, close the Table Designer or Transact\-SQL Editor, and reopen the Products table.  
  
