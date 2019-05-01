---
title: "How to: Change Target Platform and Publish a Database Project | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.publish.dialog"
  - "sql.data.tools.publishdacproject"
ms.assetid: 6012e120-5f72-4f4f-ae6e-f9a57ae1dea7
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# How to: Change Target Platform and Publish a Database Project
You can change the target SQL Server version for your SQL Server Data Tools (SSDT) database project to any supported instance of SQL Server (SQL Server 2005, 2008, 2008 R2, Microsoft SQL Server 2012, or SQL Azure). By doing so, you can centralize your database development in one project, but publish it to multiple SQL Server instances as the need arises.  
  
SSDT also makes this task simple by being aware of your target platform and automatically detecting any error in your code (for example., when you are using unsupported features for a project that is going to be published to SQL Azure).  
  
> [!WARNING]  
> The following procedures utilize entities created in previous procedures in the [Connected Database Development](../ssdt/connected-database-development.md) and [Project-Oriented Offline Database Development](../ssdt/project-oriented-offline-database-development.md) sections.  
  
### To change a project's target platform  
  
1.  Right-click your project in **Solution Explorer** and select **Properties**. Click the **Project Settings** tab on the left to access the **Project Settings** property page.  
  
2.  The **Target platform** dropdown list in this page contains all the supported SQL Server platforms that a database project can be published to. For this procedure, select **SQL Azure**.  
  
### To use platform validation when editing scripts  
  
1.  Right-click the **Products** table in Solution Explorer, and select **View Code** to open it in Transact\-SQL Editor.  
  
2.  Append `ON [PRIMARY]` to the end of the `CREATE TABLE` statement.  
  
3.  Notice that the following error shows up in the **Error List** pane:  SQL70015: 'Filegroup reference and partitioning scheme' is not supported in SQL Azure..  
  
    SSDT automatically validates your script based on the target platform. In this case, since filegroup is not supported in SQL Azure, SSDT returns an error. For a list of non-supported Transact\-SQL statements in SQL Azure, see [Partially Supported Transact-SQL Statements (Microsoft Azure SQL Database)](https://msdn.microsoft.com/library/ee336267.aspx).  
  
4.  Remove the `ON` clause. Notice that the error immediately disappears from the **Error List**.  
  
### To publish a database project  
  
1.  If you have access to a SQL Azure instance, you can skip to the next step. Otherwise, right-click the **TradeDev** project in **Solution Explorer** and select **Properties** to access the **Project Settings** property page. Use the **Target platform** dropdown list to select the SQL Server platform that you want to publish the project to.  
  
2.  Right-click the **TradeDev** project in **Solution Explorer** and select **Publish**. SSDT will start building your project. If there is no build error, the **Publish Database** dialog box appears.  
  
3.  In the **Publish Database** dialog box, click **Edit** to edit the Target database connection.  
  
4.  In the **Connection Properties** dialog box, enter your SQL Server instance name and your credentials for authentication. In **Connect to a database**, enter **NewTrade**. This will attempt to publish your database project to a new database. You can also choose an existing database to publish to. For example, if you choose the existing **TradeDev** database, then all the changes you have been making to the objects (as scripts) in the offline **TradeDev** project will be propagated to the live **TradeDev** database.  
  
    If you have permission to make any changes to the database you want to publish to, press the **Publish** button. If, however, you do not have write access to a production database, you can click the **Generate Script** button to produce a Transact\-SQL publish script, which can then be handed off to a DBA. The DBA can then run the script to update the production server so that its schema is in sync with the database project.  
  
5.  The **Data Tools Operations**  window will show the progress of your publish operations, and notify you of any errors. In this new window, you can also choose to view the deployment preview, the generated script, or the full publish results if desired.  
  
6.  You can also save the publish settings in a profile, so that you can reuse the same settings for future publish operations. To do so, click the **Save Profile As** button in the **Publish Database** dialog box. In the future, you can click the **Load Profile** button when you want to reload existing settings.  
  
7.  Notice the messages in the **Data Tools Operations** window. Click on "View Preview" link to the right of **Creating publish preview...** This will open the deployment preview report. If your project's target platform is not identical to the database server where the project is published to, SSDT will issue a warning in this report.  For example, if your project's target platform is Microsoft SQL Server 2012 and you are attempting to publish the project to a SQL Server 2008 R2 server instance, you will see the following warning in the **Output** window:  
  
**A project which specifies Microsoft SQL Server 2012 as the target platform may experience compatibility issues with SQL Server 2008**    If such project contains entities (for example, a Sequence object) that are introduced in Microsoft SQL Server 2012, the publishing operation will fail.  
  
    The deployment will fail if object predicates use **CONTAINS** or **FREETEXT** over a newly created full-text index and transactional scripts are used. If the option to include transactional scripts is enabled during deployment, then procedures and views are defined inside a transaction while a full-text index is defined outside of a transaction at the end of the deploy script. Because of this ordering in the script, procedures or views using CONTAINS or FREETEXT will not be resolved against the full-text index, resulting in a deployment error.  
  
