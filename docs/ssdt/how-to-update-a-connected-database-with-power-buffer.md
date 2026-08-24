---
title: Update a Connected Database with Power Buffer
description: Learn how to use Power Buffer to update a database. See how to verify changes before you apply them and how to save changes in a script for later deployment.
author: dzsquared
ms.author: drskwier
ms.reviewer: randolphwest
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
f1_keywords:
  - "sql.data.tools.commitpreview.dialog"
---

# How to: Update a connected database with Power Buffer

SQL Server Data Tools (SSDT) Power Buffer technology makes it easy for you to apply changes to your connected database by storing all your edits in the current session. Any errors caused by editing in Power Buffer window (in either the Transact-SQL Editor or Table Designer) immediately show up in the **Error List** pane, which enables you to follow the errors identified for further troubleshooting. You can verify your pending changes until you're ready to apply them to your database. During the update process, SSDT automatically creates an `ALTER` script based on your edits, and alerts you of any potential issues. You can then apply all the changes that accumulated across all open Power Buffer windows to the same database, or save the `ALTER` script to be deployed later.

SSDT is also aware of any changes made to your database schema outside Visual Studio. For example, if you add a new table to an existing database in SQL Server Management Studio, such change immediately shows up in the SQL Server Object Explorer in Visual Studio without manually refreshing it. The drift detection feature ensures that you're always viewing the latest schema definition of a database in SQL Server Object Explorer. Any database objects opened in Table Designer or Transact-SQL Editor for editing aren't refreshed to show changes outside Visual Studio.

The following procedures utilize entities created in previous procedures in the [Manage tables, relationships, and fix errors](manage-tables-relationships-and-fix-errors.md) section.

## Apply the changes made in the previous procedures

1. Select the green **Update** button on the toolbar ("Update Database" tooltip is displayed if you hover over the button). The toolbar is above the Columns Grid of the Table Designer.

1. The **Preview Database Updates** dialog box appears. A deployment script based on your changes is generated in the background. The dialog box then shows a summary of the actions SSDT is going to take (for example, creating or dropping database entities), together with potential issues it identifies (this isn't applicable to our procedure, but comes in handy when your database definition contains errors that prevent an update action until resolved).

1. If you don't want to update the database at this moment, select the **Cancel** button to exit the **Preview Database Updates** dialog box.

1. If you're comfortable with the changes so far, select the **Update Database** button in the **Preview Database Updates** dialog box. The deployment script is executed on your behalf, and your accumulated changes are now applied to the database.

1. If you want to view the deployment script to verify or make some changes before updating, select the **Generate Script** button in the **Preview Database Updates** dialog box. The generated script opens in a new Transact-SQL editor window. You can press the **Execute Query** button in the Transact-SQL editor toolbar to run this query. This is similar to what the **Update Database** button has done for you in Step 4.

   > [!WARNING]  
   > If you make any changes to the deployment script and execute it, such changes don't show up in any opened database entities. For example, if you rename a column of the `Customers` table in the deployment script and execute it to update the database, and if the `Customers` table is opened in the Table Designer, the column name is still the old one when you selected the **Update Database** button. You have to manually close the Table Designer without saving it locally as a script. When you reopen the table from **SQL Server Object Explorer**, you notice that the database has been updated with the changes you made in the deployment script.

1. In the **Output** pane of the Transact-SQL editor (or the **Message** pane if you're executing the deployment script yourself), notice the following output, which indicates that the update is successful.

   ```output
   Creating [dbo].[Customers]...Creating [dbo].[Products]...Creating [dbo].[Suppliers]...Creating FK_Products_SupplierId...Creating FK_Products_CustomerId...Creating CK_Products_ShelfLife The transacted portion of the database update succeeded.
   Checking existing data against newly created constraintsUpdate complete.
   ```

1. In **SQL Server Object Explorer**, notice that the new tables have shown up under the **Tables** node of the `Trade` database.

### View changes made to a database outside Visual Studio

1. Open SQL Server Management Studio. In the **Connect to Server** dialog box, enter the same database server that you have been connected to in Visual Studio and select **Connect**.

1. In **SQL Server Object Explorer**, expand **Databases** and navigate to the `Trade` database.

1. Right-click **Tables** under `Trade` and select **New Table**. In the Table Designer, enter **id** as the Column Name and **int** as the Data Type.

1. Select the **Save** icon in the toolbar to save the table. Accept the default name and select **OK**.

   Go back to Visual Studio. Examine the **Tables** node under the `Trade` database in **SQL Server Object Explorer**. Notice the appearance of the newly created `Table_1` table.

1. Right-click `Table_1` and select **Delete**. Select **Update Database** in the **Preview Database Updates** dialog box.

## Related content

- [How to: Fix errors](how-to-fix-errors.md)
