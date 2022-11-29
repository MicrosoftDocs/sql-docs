---
title: Create a New Database Project
description: Find out how to create a new database project. See how to import the schema from an existing database into the new project.
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.dbprojectwizard.importschema"
  - "sql.data.tools.SqlProjectImportDatabaseDialog.dialog"
  - "sql.data.tools.importscriptwizard.welcome"
  - "sql.data.tools.importscriptwizard.fileselection"
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 02/09/2017
---

# How to: Create a New Database Project

You can create a new database project and import database schema from an existing database, a .sql script file or a Data-tier application (.dacpac). You can then invoke the same visual designer tools (Transact\-SQL Editor, Table Designer) available for connected database development to make changes to the offline database project, and publish the changes back to the production database. The changes can also be saved as a script to be published later. Using the **Project Properties** pane, you can change the target platform to different versions of SQL Server (including SQL Azure).  
  
The following two procedures essentially achieve the same goal by creating a new database project and importing schema from an existing database. Each database object will be represented as a SQL script file (.sql) in **Solution Explorer**. For more information on importing database schema from a snapshot, see [How to: Create a Snapshot of a Project](../ssdt/how-to-create-a-snapshot-of-a-project.md).  
  
> [!WARNING]  
> The following procedures utilize entities created in previous procedures in the [Connected Database Development](../ssdt/connected-database-development.md) section.  
  
### To create a new database project off a connected database  
  
1.  Right-click the **TradeDev** node in **SQL Server Object Explorer** and select **Create New Project**.  
  
2.  In the **Import Database** dialog box, notice that the **Source database connection** settings have been predefined by the database you have selected in **SQL Server Object Explorer**. In the **Target project** setting, change the name of the project to **TradeDev**.  
  
3.  In the **Import Settings** section, notice the options for importing specific objects and settings, and creating folders for each schema and/or object type. For an organized hierarchy of all your database objects, accept all default settings and click **Start**.  
  
4.  The **Import Database** dialog shows a progress bar and displays a list of objects SSDT is importing. When the import operation has completed, click **Finish** to exit the final screen.  
  
5.  Examine the hierarchy in the **Solution Explorer**. Expand the **dbo** folder and you will find separate **Functions**, **Tables** and **Views** folders. Notice that the tables and function are grouped under their schema folders.  
  
6.  Double-click **Products.sql** under **Tables**. The **Table Designer** opens, showing the visual interpretation of the table in the Columns Grid, and the script definition of the table in the Script Pane. This is identical to what we see in the [Connected Database Development](../ssdt/connected-database-development.md) section.  
  
7.  Uncheck the **Allow Nulls** box for the **CustomerId** column. Press CTRL + S to save the file.  
  
8.  Right-click the **TradeDev** project in **Solution Explorer** and select **Build** to build the database project.  
  
    The results of Build operation can be seen in the Output Window  
  
### To create a new project and import existing database schema  
  
1.  Click **File**, **New**, then **Project**. In the **New Project** dialog box, select **SQL Server** in the left pane. Notice that there is only one type of database project: the **SQL Server Database Project**. There is no platform-specific project as in previous versions of Visual Studio. You will be able to set your target platform in the **Project Settings** dialog box after the project has been created. Such task will be covered in the [How to: Change Target Platform and Publish a Database Project](../ssdt/how-to-change-target-platform-and-publish-a-database-project.md) topic.  
  
2.  Change the name of the project to **TradeDev** and click **OK** to create the new project.  
  
3.  Right-click the newly created **TradeDev** project in **Solution Explorer**, select **Import**, then **Database**.  
  
    The **Import Database** dialog box opens. In the **Source database connection** section, click **Choose a database** and select **TradeDev**. If **TradeDev** is absent from the dropdown list, use the **New Connection** button to edit the Connection Properties.  
  
4.  In the **Import Settings** section, notice the options for importing specific objects and settings, and creating folders for each schema and/or object type. For an organized hierarchy of all your database objects, accept all default settings and click **Start**.  
  
5.  The **Import Database** dialog shows a progress bar and displays a list of objects SSDT is importing. When the import operation has completed, click **Finish** to exit the final screen.  
  
6.  Examine the hierarchy in the **Solution Explorer**. Expand the **dbo** folder and you will find separate **Functions**, **Tables** and **Views** folders. Notice that the tables and function are grouped under their schema folders.  
  
7.  Double-click **Products.sql** under **Tables**. The **Table Designer** opens, showing the visual interpretation of the table in the Columns Grid, and the script definition of the table in the Script Pane. This is identical to what we see in the [Connected Database Development](../ssdt/connected-database-development.md) section.  
  
8.  Uncheck the **Allow Nulls** box for the **CustomerId** column. Press CTRL + S to save the file.  
  
9. Right-click the **TradeDev** project in **Solution Explorer** and select **Build** to build the database project.  
  
## See Also  
[How to: Change Target Platform and Publish a Database Project](../ssdt/how-to-change-target-platform-and-publish-a-database-project.md)  
  
