---
title: Project-Oriented Offline Database Development
description: View available resources on project-oriented offline database development tasks, such as importing objects into a database and using sequence objects.
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.dbprojectwizard.general"
  - "sql.data.tools.dbprojectwizard.summary"
ms.assetid: e61e830d-9fcd-45e7-b7b4-93a42155dd56
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 02/09/2017
---

# Project-Oriented Offline Database Development

This section describes features provided by SQL Server Data Tools (SSDT) for authoring, building, debugging and publishing a database project.  
  
Using SSDT, you can create an offline database project and implement schema changes by adding, modifying or deleting the definitions of objects (represented by scripts) in the project, without a connection to a server instance. These can all be accomplished by using the table designer, or the Transact\-SQL Editor. You can also write and debug Transact\-SQL and CLR objects in the same project. You can use Schema Compare to ensure that your project stays in sync with the production database, and create snapshots for your project in each stage of the development cycle for comparison purposes. While you are working on your database projects in a team-based environment, you can employ version control for all the files. After your database project has been developed, tested and debugged, you can hand off your project to authorized personnel to be published to a production environment.  
  
> [!NOTE]  
> How To topics in this section contain a series of tasks that can be completed in a sequence.  
  
## In This Section  
  
|Topic|Description|  
|---------|---------------|  
|[Import into a Database Project](../ssdt/import-into-a-database-project.md)|Describes importing objects from a live database, .dacpac, or script.|  
|[Add Database Reference Dialog Box](../ssdt/add-database-reference-dialog-box.md)|Describes various ways to add a database reference.|  
|[Check for Updates Dialog Box](../ssdt/check-for-updates-dialog-box.md)|Describes how SQL Server Data Tools can check for product updates.|  
|[Database Project Settings](../ssdt/database-project-settings.md)|Describes various project settings to control aspects of your database and build configurations.|  
|[How to: Browse Objects in a SQL Server Database Project](../ssdt/how-to-browse-objects-in-a-sql-server-database-project.md)|The SQL Server Object Explorer in Visual Studio now contains a dedicated Projects node, under which all SQL Server database projects in your solution are grouped in an SQL Server Management Studio-like hierarchy.|  
|[Data Tools Operations Window](../ssdt/data-tools-operations-window.md)|Describes the **Data Tools Operations** window, which shows the progress of some operations and notifies you of any errors.|  
|[Transact-SQL Editor Options](../ssdt/transact-sql-editor-options.md)|Describes Transact\-SQL options.|  
|[How to: Create a New Database Project](../ssdt/how-to-create-a-new-database-project.md)|Create a database project and import existing database schema.|  
|[How to: Use Schema Compare to Compare Different Database Definitions](../ssdt/how-to-use-schema-compare-to-compare-different-database-definitions.md)|Compare the schemas of a database and a project and sync up.|  
|[How to: Build and Deploy to a Local Database](../ssdt/how-to-build-and-deploy-to-a-local-database.md)|Use the local on-demand SQL Server instance, which is activated when you debug a database project.|  
|[How to: Change Target Platform and Publish a Database Project](../ssdt/how-to-change-target-platform-and-publish-a-database-project.md)|Change the target SQL Server platform for your project to any supported instance of SQL Server and validate syntax.|  
|[How to: Create a Snapshot of a Project](../ssdt/how-to-create-a-snapshot-of-a-project.md)|Create a read-only proxy of the database schema, and revert the source project when unwanted changes are applied to the project.|  
|[How to: Use Microsoft SQL Server 2012 Objects in Your Project](../ssdt/how-to-use-microsoft-sql-server-2012-objects-in-your-project.md)|Add a new Sequence object to your project.|  
|[How to: Work with CLR Database Objects](../ssdt/how-to-work-with-clr-database-objects.md)|Create and publish CLR objects in the SQL Server Data Tools Database project.|  
|[How to: Convert a Visual Studio 2010 Database Projects to SQL Server Database Projects and Retarget to a Different Platform](../ssdt/how-to-convert-visual-studio-2010-database-projects-to-ssql-server-projects.md)|Convert existing SQL Server Database, CLR objects, and Data-Tier Application projects created in Visual Studio 2010 to the SQL Server Data Tools Database project.|  
|[How to: Specify Predeployment or Postdeployment Scripts](../ssdt/how-to-specify-predeployment-or-postdeployment-scripts.md)|Discusses how to use scripts that you want to run before or after the deployment of your database.|  
  
## Related Sections

[Manage Tables, Relationships, and Fix Errors](../ssdt/manage-tables-relationships-and-fix-errors.md)
