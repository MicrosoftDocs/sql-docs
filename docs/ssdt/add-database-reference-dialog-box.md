---
title: "Add Database Reference Dialog Box | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.adddatabasereference.dialog"
ms.assetid: 838caa2a-4117-48bc-8c6c-9e7ceab38893
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# Add Database Reference Dialog Box
This topic describes the procedures you can perform in the **Add Database Reference** dialog box.  
  
Database references let you:  
  
Access objects in other databases.  
A project can reference another database on any server by using three-part or four-part name resolution. When using a three-part or four-part name for a reference, you can use SQLCMD variables to allow references to work on multiple servers and databases.  
  
Create a composite solution of multiple database projects.  
In a composite project, database references partition a large database into several separate projects. You create a reference that does not contain variables or values for the database or server (only using one and two part names).  
  
Database references can be made to a database project in the current solution or to a DACPAC. Adding a database reference to a project changes project dependencies and build order.  
  
## Selecting the Database to Reference  
You can reference another database project in the same solution, a system database, or a DACPAC.  
  
If there is more than one database project in your solution, **Database projects in the current solution** is enabled. You can reference another database in the solution.  
  
Select **System database** if you intend to select one of the system databases as a database reference.  
  
Select **Data-tier Application (.dacpac)** to reference a database in a DACPAC, and browse to the directory with the DACPAC file.  
  
## Selecting the Database's Relative Location  
After you select the database you want to reference, you can specify the expected location of a database object, relative to the referencing project.  
  
References can be resolved for objects in one of the following locations:  
  
- In the referencing database.  
  
- In a database other than the referencing database, but on the same server.  
  
- In a database other than the referencing database, on a different server.  
  
Specify a database name. If you chose **System database**, you shouldn't modify the system database literal. If you chose **Database projects in the current solution**, the default name of the database is based on the name of the database in the project.  
  
If you selected **Different database, different server**, specify a server name.  
  
You can use a (SQLCMD) database variable. If you want to refer to the database with a variable (instead of the literal name), accept or modify the default database variable. A database variable is useful if you publish the database project from multiple servers and databases. In that situation, a developer can go to **SQLCMD Variables** in the project's property pages and specify the local name of the database. If you leave **Database variable** blank, you can only refer to the database by its literal name. If you specify a database variable name, you cannot refer to the database by its literal name.  
  
If you selected **Different database, different server**, a (SQLCMD) server variable is required. A server variable is useful to publish the database project from multiple servers and databases. In that situation, a developer can go to **SQLCMD Variables** in the project's property pages and specify the local name of the server. You can only refer to the server with the variable, and not with the server name.  
  
> [!IMPORTANT]  
> In some situations, you can create a database reference that has the same name as an existing database reference. Two database references with the same name can result in unexpected behavior. In this situation, delete both database references.  
  
## Common Procedures  
The following are common procedures:  
  
### To create a reference to a database on the same server  
  
1.  In Solution Explorer, right click **References** and select **Add Database Reference**.  
  
2.  In the **Add Database Reference** dialog box, specify the **Database Location** as **Different database, same server**.  
  
3.  Specify the name of the database.  
  
4.  Decide whether you want to use a database variable.  
  
5.  Copy the example usage and paste it into your .sql file. In the example usage, change [Schema1] to the name of your schema (for example, [dbo]). Also, modify the name of the database object as appropriate for your project.  
  
6.  Build the solution.  
  
### To create a reference to a database on another server  
  
1.  In Solution Explorer, right click **References** and select **Add Database Reference**.  
  
2.  In the **Add Database Reference** dialog box, specify the **Database Location** as **Different database, different server**.  
  
3.  Make sure that the database name is correct.  
  
4.  Decide whether you want to use a database variable.  
  
5.  Specify the name of the server and the server variable.  
  
6.  Copy the example usage and paste it into your .sql file. In the example usage, change [Schema1] to the name of your schema (for example, [dbo]). Also, modify the name of the database object as appropriate for your project.  
  
7.  Build the solution.  
  
### To create a composite project  
  
1.  In Solution Explorer, right click **References** and select **Add Database Reference**.  
  
2.  Select the source of the database you are referencing (a project in the solution or a DACPAC).  
  
3.  In the **Add Database Reference** dialog box, specify the **Database Location** as **Same database**.  
  
4.  Copy the example usage and paste it into your .sql file. In the example usage, change [Schema1] to the name of your schema. Also, modify the name of the database object as appropriate for your project.  
  
5.  Build the solution.  
  
When you publish this project, you can deploy composite projects in the same solution into a single target:  
  
1.  Right click the project name in **Solution Explorer** and select **Publish** to display the **Publish Database** dialog box.  
  
2.  In the **Publish Database** dialog box, click **Advanced**.  
  
3.  In the **Advanced Publish Settings** dialog box, make sure that **Include composite objects** is checked in the **Advanced Deployment Options** list.  
  
## See Also  
[Project-Oriented Offline Database Development](../ssdt/project-oriented-offline-database-development.md)  
  
