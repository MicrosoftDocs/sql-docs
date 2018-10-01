---
title: "How to: Build and Deploy to a Local Database | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: ebca8ff8-9a09-4207-8979-9d577af7c1d5
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# How to: Build and Deploy to a Local Database
Microsoft SQL Server 2012 provides a local on-demand server instance, called SQL Server Express Local Database Runtime, which is activated when you debug a SQL Server Database project. This local server instance can be used as a sandbox for building, testing and debugging your project. It is independent of any of your installed SQL Server instances, and is not accessible outside SQL Server Data Tools (SSDT). Such an arrangement is ideal for developers who have limited or no access to production databases, but would like to test their projects locally before authorized personnel will deploy them to production. In addition, when you are developing a database solution for SQL Azure, you can utilize the convenience provided by this local server to develop and test your database project locally, before deploying it to the cloud.  
  
> [!WARNING]  
> A database under the local database node in SQL Server Object Explorer is a reflection of its corresponding database project, and is not related to the same-named database in a connected server instance.  
  
> [!WARNING]  
> The following procedures utilize entities created in previous procedures in the [Connected Database Development](../ssdt/connected-database-development.md) and [Project-Oriented Offline Database Development](../ssdt/project-oriented-offline-database-development.md) sections.  
  
### To use the local database  
  
1.  Notice that in **SQL Server Object Explorer**, under the **SQL Server** node, a new node named **Local** appears. This is the local database instance.  
  
2.  Expand the **Local** and **Databases** nodes. Notice the appearance of a database with the same name as the TradeDev project. Expand the nodes under this database. The **Data Tools Operations**  window shows the status of expansion/import operations in progress on any database in the **Local** node. Notice that they do not contain any of the tables and entities we created in earlier procedures.  
  
3.  Press F5 to debug the TradeDev database project.  
  
    By default, SSDT will use the local database server instance for debugging database projects. In this case, SSDT will first attempt to build the project, and if there is no error, the project (and its entities) will be deployed to the local database. If you debug the same project later, SSDT will detect any changes since your last debugging session, and deploy only those to the local database.  
  
4.  Expand the nodes under **TradeDev** in the **Local** database server again. This time, notice that the tables, views and functions have been deployed to the local database server.  
  
5.  Right-click the **TradeDev** node and select **New Query**.  
  
6.  In the script pane, paste this code and click the **Execute Query** button to run the query.  
  
    ```  
    select * from dbo.GetProductsBySupplier(1)  
    ```  
  
7.  The **Message** pane shows (0 row(s) affected), and the **Results** pane returns no row. This is because we are querying against the local database instead of the connected database that actually contains real data.  
  
    You can confirm this by right-clicking the **Products** table under this local **TradeDev** database, and select **View Data**. Notice that the table is empty.  
  
### To replicate real data to the local database  
  
1.  In **SQL Server Object Explorer**, expand your connected SQL Server instance and locate the **TradeDev** database.  
  
    Right-click **Suppliers** table and select **View Data**.  
  
2.  Click the **Script** button (the second button from the right) on top of the Data Editor. Copy the `INSERT` statements from the script.  
  
3.  Expand the **Local** server instance and right-click the **TradeDev** node, select **New Query**.  
  
4.  Paste the `INSERT` statements into this query window and execute the query.  
  
5.  Repeat the above steps to replicate data from the **Products** and **Fruits** tables in the connected **TradeDev** database to the local **TradeDev** database.  
  
6.  Right-click the **Local** server instance and select **Refresh**. Examine the tables using **View Data** to verify that the local database has been populated.  
  
7.  Right-click the **TradeDev** node of the Local server instance, and select **New Query**.  
  
8.  In the script pane, paste this code and click the **Execute Query** button to run the query.  
  
    ```  
    select * from dbo.GetProductsBySupplier(1)  
    ```  
  
9. In the **Results** pane below the Transact\-SQL Editor pane, you will see that the Apples and Potato Chips rows of the `Products` table are returned.  
  
