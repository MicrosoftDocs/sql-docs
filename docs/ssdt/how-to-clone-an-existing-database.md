---
title: "How to: Clone an Existing Database | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: aad3594a-11cf-4e68-a622-071a93d43875
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# How to: Clone an Existing Database
This task uses some of the steps you learned in previous procedures to create a new database and port existing data over. In addition, it uses the steps discussed in [How to: Use Schema Compare to Compare Different Database Definitions](../ssdt/how-to-use-schema-compare-to-compare-different-database-definitions.md) to synchronize the schema of a source and project database.  
  
By using these steps, you can easily create a development or test database from a production database with identical schema and data. You can then continue to develop the test database in a connected mode, or create a database project for offline development and testing, all without disrupting the operation of the production database.  
  
> [!WARNING]  
> The following procedures uses entities created in previous procedures in the [Connected Database Development](../ssdt/connected-database-development.md) section.  
  
### To create a development database  
  
1.  In **SQL Server Object Explorer**, under the **SQL Server** node, expand your connected server instance.  
  
2.  Right-click the **Databases** node and select **Add New Database**.  
  
3.  Rename the new database to **TradeDev**.  
  
4.  Right-click the **Trade** database in **SQL Server Object Explorer**, and select **Schema Compare**. Follow the steps in the [How to: Use Schema Compare to Compare Different Database Definitions](../ssdt/how-to-use-schema-compare-to-compare-different-database-definitions.md) topic, choosing the original **Trade** database as the source and the new **TradeDev** database as the target. This will update **TradeDev** with the schema from **Trade**.  
  
### To replicate data  
  
1.  The previous step has duplicated only the schema of the production database to the development database. In this procedure, you will duplicate production data to the development database.  
  
    Right-click the **Suppliers** table in the **Trade** database and select **View Data**. The Data Editor opens.  
  
2.  Click the **Script** button next to **Max Rows** in the toolbar.  
  
3.  When the script window opens, make sure Connected is shown in the status bar below the Transact\-SQL script pane. If Disconnected is shown, click the **Connect** button (the leftmost one in the toolbar) and enter your server information and credentials.  
  
4.  In the **Database** dropdown menu next to the **Connect**/**Disconnect** buttons, select **TradeDev**. This is similar to the Transact\-SQL`USE` statement, and will ensure that the script in the code editor will be executed against the **TradeDev** database.  
  
5.  Click the **Execute Query** button to execute the `INSERT` statements. This will insert all the rows from the `Suppliers` table of the `Trade` database to the `Suppliers` table in the `TradeDev` database.  
  
6.  Repeat the above steps for all the tables in the `Trade`database, so that they are replicated to the `TradeDev`database.  
  
7.  Use the Data Editor to verify that all the tables in the new `TradeDev`database have been populated.  
  
## See Also  
[How to: Use Schema Compare to Compare Different Database Definitions](../ssdt/how-to-use-schema-compare-to-compare-different-database-definitions.md)  
  
