---
title: "How to: Use Rename and Refactoring to Make Changes to your Database Objects | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.dbrefactoring.previewdialog"
  - "sql.data.tools.editor.howto.refactoring"
  - "sql.data.tools.dbrefactoring.renamedialog"
  - "sql.data.tools.dbrefactoring.moveschemadialog"
  - "sql.data.tools.dbrefactoring.renameserverdatabasedialog"
ms.assetid: f35520e6-8e6e-47b1-87a3-22c0cf2cabdb
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# How to: Use Rename and Refactoring to Make Changes to your Database Objects
The Refactor contextual menu in the Transact\-SQL Editor allows you to rename or move an object to a different schema, and do a preview of all affected areas before committing the change. You can also use the Refactor menu to fully qualify all references to database objects, or expand any wildcard characters in `SELECT` statements in your database project.  
  
> [!NOTE]  
> The following procedures utilize entities created in previous procedures in the [Connected Database Development](../ssdt/connected-database-development.md) and [Project-Oriented Offline Database Development](../ssdt/project-oriented-offline-database-development.md) sections.  
  
### To rename a type  
  
1.  Right-click the **Products** table (Products.sql) in **Solution Explorer** and select **View Code** to open the script in Transact\-SQL editor.  
  
2.  Right click `[Products]` in the script, select **Refactor**, and **Rename**.  
  
3.  In the **New Name** field, change it to **Product**. Leave the **Preview Changes** option checked and click **OK**.  
  
4.  In the next screen, you will be able to preview a list of scripts that are going to be affected by this rename operation. Specifically, all the places that refer to `Products` will be highlighted. This is very similar to the Find All References task in the previous procedure. Click anything on the top pane and view the actual change in the scripts (highlighted in green) in the bottom pane.  
  
5.  Click **Apply**.  
  
6.  For script files that are already opened in Table Designer or Transact\-SQL Editor, notice that the Transact\-SQL Editor has highlighted locations where changes have taken place with a green bar on the left.  
  
7.  Notice the addition of **TradeDev.refactorlog** in **Solution Explorer**. Double-click to open it. It contains an XML representation of all the changes in this session.  
  
8.  Press F5 to build and deploy the project to the local database.  
  
9. Right-click the **TradeDev** database under **Local** in **SQL Server Object Explorer**, and select **Refresh**.  
  
10. Expand **Tables**, and notice that the **Products** table has been renamed.  
  
11. Right-click **Product** and select **View Data**. Notice that existing data is kept intact regardless of the rename operation.  
  
### To expand wildcards  
  
1.  Expand the **Functions** node in **Solution Explorer**, and double-click **GetProductsBySupplier.sql**.  
  
2.  Place the cursor on the asterisk in this line and right-click. Select **Refactor**, and **Expand Wildcards**.  
  
    ```  
    SELECT * from Product p  
    ```  
  
3.  In the **Preview Changes** dialog box, click `SELECT * from Product p` on the top pane to highlight it.  
  
4.  In the **Preview Changes** pane below, notice that `*` has been expanded to the following in the script.  
  
    ```  
    [Id], [Name], [ShelfLife], [SupplierId], [CustomerId]  
    ```  
  
5.  Click the **Apply** button.  Notice the line that contains changes brought forth by the expand operation is again highlighted with a green bar on the left.  
  
### To fully qualify database object names  
  
1.  Make sure **GetProductsBySupplier.sql** is still open in Transact\-SQL Editor.  
  
2.  Place the cursor on `Product` in this line and right-click. Select **Refactor**, and **Fully-Qualify Names**.  
  
    ```  
    SELECT [Id], [Name], [ShelfLife], [SupplierId], [CustomerId] from Product p  
    ```  
  
3.  Click the **Apply** button in the **Preview Changes** dialog box.  Notice the all the object references have been updated to include the name of the object's schema and, if the object has a parent, the name of the parent.  
  
    ```  
    SELECT [p].[Id], [p].[Name], [p].[ShelfLife], [p].[SupplierId], [p].[CustomerId] from [dbo].[Product] p  
    ```  
  
### To move schema  
  
1.  Right-click the object that you want move. Select **Refactor**, and **Move Schema**.  
  
2.  In the **New Schema** list, click the name of the schema into which you want to move the object. Click OK.  
  
    If you selected the **Preview changes** check box, **the Preview Changes** dialog box appears. Otherwise, the object name is updated, and the object is moved to the new schema.  
  
