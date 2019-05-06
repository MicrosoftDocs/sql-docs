---
title: "How to: Update a Connected Database with Power Buffer | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.commitpreview.dialog"
ms.assetid: 4048b7f8-71a9-47ad-b812-3fc1e8066240
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# How to: Update a Connected Database with Power Buffer
SQL Server Data Tools Power Buffer technology makes it easy for you to apply changes to your connected database by storing all your edits in the current session. Any errors caused by editing in Power Buffer window (in either the Transact\-SQL Editor or Table Designer) immediately show up in the **Error List** pane, which enables you to follow the errors identified for further troubleshooting. You can verify your pending changes until you are ready to apply them to your database. During the update process, SSDT automatically creates an ALTER script based on your edits, and alerts you of any potential issues. You can then apply all the changes that have accumulated across all open Power Buffer windows to the same database, or save the ALTER script to be deployed later.  
  
SSDT is also aware of any changes made to your database schema outside Visual Studio. For example, if you add a new table to an existing database in SQL Server Management Studio, such change will immediately show up in the SQL Server Object Explorer in Visual Studio without manually refreshing it. The drift detection feature ensures that you are always viewing the latest schema definition of a database in SQL Server Object Explorer. Notice that any database objects opened in Table Designer or Transact\-SQL Editor for editing will not be refreshed to show changes outside Visual Studio.  
  
The following procedures utilize entities created in previous procedures in the [Connected Database Development](../ssdt/connected-database-development.md) section.  
  
### To apply the changes made in the previous procedures  
  
1.  Click the green **Update** button on the toolbar ("Update Database" tooltip is displayed if you hover over the button). The toolbar is above the Columns Grid of the Table Designer.  
  
2.  The **Preview Database Updates** dialog box appears. A deployment script based on your changes is generated in the background. The dialog box then shows a summary of the actions SSDT is going to take (e.g., creating or dropping database entities), together with potential issues it has identified (this is not applicable to our procedure, but will come in handy when your database definition contains errors that prevent an update action until resolved).  
  
3.  If you do not want to update the database at this moment, click the **Cancel** button to exit the **Preview Database Updates** dialog box.  
  
4.  If you are comfortable with the changes so far, click the **Update Database** button in the **Preview Database Updates** dialog box. The deployment script is executed on your behalf, and your accumulated changes are now applied to the database.  
  
5.  If you want to view the deployment script to verify or make some changes before updating, click the **Generate Script** button in the **Preview Database Updates** dialog box. The generated script will open in a new Transact\-SQL editor window. You can press the **Execute Query** button in the Transact\-SQL editor toolbar to run this query. This is similar to what the **Update Database** button has done for you in Step 4.  
  
    > [!WARNING]  
    > If you make any changes to the deployment script and execute it, such changes will not show up in any opened database entities. For example, if you rename a column of the `Customers` table in the deployment script and execute it to update the database, and if the `Customers` table is opened in the Table Designer, the column name will still be the old one when you clicked the **Update Database** button. You have to manually close the Table Designer without saving it locally as a script. When you reopen the table from **SQL Server Object Explorer**, you will notice that the database has actually been updated with the changes you made in the deployment script.  
  
6.  In the **Output** pane of the Transact\-SQL editor (or the **Message** pane if you are executing the deployment script yourself), notice the followings which indicate that the update is successful.  
  
**Creating [dbo].[Customers]...Creating [dbo].[Products]...Creating [dbo].[Suppliers]...Creating FK_Products_SupplierId...Creating FK_Products_CustomerId...Creating CK_Products_ShelfLife The transacted portion of the database update succeeded.Checking existing data against newly created constraintsUpdate complete.**  
  
7.  In **SQL Server Object Explorer**, notice that the new tables have shown up under the **Tables** node of the **Trade** database.  
  
### To view changes made to a database outside Visual Studio  
  
1.  Open SQL Server Management Studio. In the **Connect to Server** dialog box, enter the same database server that you have been connected to in Visual Studio and click **Connect**.  
  
2.  In **SQL Server Object Explorer**, expand **Databases** and navigate to the **Trade** database.  
  
3.  Right-click **Tables** under **Trade** and select **New Table**. In the Table Designer, enter **id** as the Column Name and **int** as the Data Type.  
  
4.  Click the **Save** icon in the toolbar to save the table. Accept the default name and click **OK**.  
  
    Go back to Visual Studio. Examine the **Tables** node under the **Trade** database in **SQL Server Object Explorer**. Notice the appearance of the newly created **Table_1** table.  
  
5.  Right-click **Table_1** and select **Delete**. Click **Update Database** in the **Preview Database Updates** dialog box.  
  
## See Also  
[How to: Fix Errors](../ssdt/how-to-fix-errors.md)  
  
