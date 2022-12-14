---
title: Create a Snapshot of a Project
description: Become familiar with data-tier application files, or snapshots, and see how to use them. Find out how to create or import snapshots and how to compare them.
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.SqlProjectImportSnapshotSummaryDialog.dialog"
  - "sql.data.tools.SqlProjectImportSnapshotDialog.dialog"
ms.assetid: bed670a3-13bd-4d88-91a1-58d5b9524a97
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 02/09/2017
---

# How to: Create a Snapshot of a Project

A **Data-tier Application** file provides you with a read-only representation of the database schema at the time it is created. It is essentially being treated as a database schema from which you can import the schema objects back to a project. You can also compare it with the schema of a database or a project, and update the database or project to reflect the schema defined in the snapshot.  
  
In the event of a user error on a source database project, you can revert the source project to the state it was in when the snapshot was created. You can also establish snapshots at various stages of your development for baseline purpose.  
  
> [!WARNING]  
> The following procedures utilize entities created in previous procedures in the [Connected Database Development](../ssdt/connected-database-development.md) and [Project-Oriented Offline Database Development](../ssdt/project-oriented-offline-database-development.md) sections.  
  
### To create a snapshot  
  
1.  Right-click the **TradeDev** project in **Solution Explorer**, and select **Data-tier Application (\*.dacpac)...**.  
  
2.  SSDT will attempt to build the project first. If there is no build error, a **Snapshot** folder is created in **Solution Explorer**. Inside this folder, SSDT creates a .dacpac file using the name format of "\<Project Name\>_YYYYMMDD_HH-MM-SS.dacpac".  
  
3.  Right-click the .dacpac file and select **Rename**. Change the default file name to "TradeDev1.dacpac".  
  
4.  Right-click the **GetProductsBySupplier** function in **Solution Explorer** and select **Delete** to remove it from the project.  
  
5.  Follow the previous steps to create a new snapshot called **TradeDev2.dacpac**.  
  
### To import a snapshot  
  
1.  Right-click the **TradeDev** project in **Solution Explorer**, select **Import**, then **Data-tier Application (\*.dacpac)...** from the contextual menus.  
  
2.  In the **Import Data-tier Application** dialog box, click **Browse** to select **TradeDev1.dacpac** to be used as the source of the import.  
  
    Notice that the **Target project** section has been disabled, since the current project is the default target. Click **Start** to start the import.  
  
3.  Click **Finish** in the **Summary** page. In **Solution Explorer**, notice that the deleted table has been restored to the project.  
  
    > [!WARNING]  
    > The import snapshot will import all database entities in the snapshot schema to the project. This might create duplicate entities as a result. For example, each of the tables and view now contains an additional copy of itself named <ObjectName_1>. Right-click each of these duplicate objects in **Solution Explorer** and select **Delete** to remove it from the project.  
  
### To compare snapshots  
  
1.  Right-click **TradeDev1.dacpac** in Solution Explorer and select **Schema Compare**. The **Schema Compare** window opens.  
  
2.  Use the **Data-tier Application File** options to set the source and target schemas. Make sure that the **Source Schema** is set to **TradeDev1.dacpac** in **Data-tier Application File**, and the **Target Schema** is set to **TradeDev2.dacpac**.  
  
3.  Click **OK** to start the compare. Notice that the deleted function is being highlighted as a difference between the old and new snapshot.  
  
    You can easily find the delta of different snapshots by using Schema Compare. In this case, you can find out how your project evolves during the development process.  
  
## See Also  
[How to: Use Schema Compare to Compare Different Database Definitions](../ssdt/how-to-use-schema-compare-to-compare-different-database-definitions.md)  
  
