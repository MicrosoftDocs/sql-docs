---
title: Build and deploy to a local database
description: Learn about the local server instance that SQL Server provides. See how to use this instance for building, testing, and debugging development projects.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/30/2023
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
---

# How to: Build and deploy to a local database

[!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] provides a local on-demand server instance, called [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Express Local Database Runtime (LocalDB), which is activated when you debug a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Database project. This local server instance can be used as a sandbox for building, testing, and debugging your project.

It is independent of any of your installed [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instances, and isn't accessible outside [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Data Tools (SSDT). Such an arrangement is ideal for developers with limited or no access to production databases, but would like to test their projects locally before authorized personnel deploy them to production. In addition, when you're developing a database solution for Azure SQL, you can utilize the convenience provided by this local server to develop and test your database project locally, before deploying it to the cloud.

## Limitations

A database under the local database node in [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Object Explorer is a reflection of its corresponding database project, and isn't related to the same-named database in a connected server instance.

> [!WARNING]  
> The following procedures utilize entities created in previous procedures in the [Connected Database Development](connected-database-development.md) and [Project-Oriented Offline Database Development](project-oriented-offline-database-development.md) sections.

## Use the local database

1. In **SQL Server Object Explorer**, under the **SQL Server** node, a new node named **Local** appears. This is the local database instance.

1. Expand the **Local** and **Databases** nodes. Notice the appearance of a database with the same name as the TradeDev project. Expand the nodes under this database. The **Data Tools Operations**  window shows the status of expansion/import operations in progress on any database in the **Local** node. They don't contain any of the tables and entities we created in earlier procedures.

1. Press F5 to debug the TradeDev database project.

   By default, SSDT uses the local database server instance for debugging database projects. In this case, SSDT first attempts to build the project, and if there's no error, the project (and its entities) are deployed to the local database. If you debug the same project later, SSDT detects any changes since your last debugging session, and deploy only those changes to the local database.

1. Expand the nodes under `TradeDev` in the **Local** database server again. This time, notice that the tables, views, and functions were deployed to the local database server.

1. Right-click the `TradeDev` node and select **New Query**.

1. In the script pane, paste this code and select the **Execute Query** button to run the query.

   ```sql
   SELECT * FROM dbo.GetProductsBySupplier(1);
   ```

1. The **Message** pane shows `(0 row(s) affected)`, and the **Results** pane returns no rows. This is because we query against the local database, instead of the connected database that actually contains real data.

   You can confirm this by right-clicking the `Products` table under this local `TradeDev` database, and select **View Data**. The table is empty.

## Replicate real data to the local database

1. In **SQL Server Object Explorer**, expand your connected SQL Server instance and locate the `TradeDev` database.

    Right-click `Suppliers` table and select **View Data**.

1. Select the **Script** button (the second button from the right) on top of the Data Editor. Copy the `INSERT` statements from the script.

1. Expand the **Local** server instance and right-click the `TradeDev` node, select **New Query**.

1. Paste the `INSERT` statements into this query window and execute the query.

1. Repeat the above steps to replicate data from the `Products` and `Fruits` tables in the connected `TradeDev` database to the local `TradeDev` database.

1. Right-click the **Local** server instance and select **Refresh**. Examine the tables using **View Data** to verify that the local database was populated.

1. Right-click the `TradeDev` node of the Local server instance, and select **New Query**.

1. In the script pane, paste this code and select the **Execute Query** button to run the query.

   ```sql
   SELECT * FROM dbo.GetProductsBySupplier(1);
   ```

1. In the **Results** pane below the Transact-SQL Editor pane, you see that the `Apples` and `Potato Chips` rows of the `Products` table are returned.
