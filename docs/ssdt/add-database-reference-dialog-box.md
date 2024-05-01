---
title: Add Database Reference dialog box
description: Learn about database references and how to use them. View the procedures you can perform in the Add Database Reference dialog box.
author: markingmyname
ms.author: maghan
ms.date: 03/25/2024
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
f1_keywords:
  - "sql.data.tools.adddatabasereference.dialog"
  - "sql.data.tools.newdatabase.dialog"
  - "sql.data.tools.criticalerror.dialog"
---

# Add Database Reference Dialog Box

This article describes the procedures you can perform in the **Add Database Reference** dialog box.

Database references let you:

Access objects in other databases.  
A project can reference another database on any server by using a three-part or four-part name resolution. You can use SQLCMD variables to allow references to work on multiple servers and databases when using a three-part or four-part name for a reference.

Create a composite solution for multiple database projects.  
In a composite project, database references partition an extensive database into several separate projects. You create a reference that doesn't contain variables or values for the database or server (only using one and two-part names).

Database references can be made to a database project in the current solution or to a DACPAC. Adding a database reference to a project changes project dependencies and building order.

## Select the database to reference

You can reference another database project using the same solution, a system database, or a DACPAC.

If your solution contains more than one database project, **Database projects in the current solution** is enabled. You can reference another database in the solution.

Select **System database** if you intend to select one of the system databases as a database reference.

Select **Data-tier Application (.dacpac)** to reference a database in a DACPAC, and browse to the directory with the DACPAC file.

## Select the database relative location

After you select the database you want to reference, you can specify the expected location of a database object relative to the referencing project.

References can be resolved for objects in one of the following locations:

- In the referencing database.

- In a database other than the referencing database but on, the same server.

- In a database other than the referencing database, on a different server.

Specify a database name. If you chose **System database**, you shouldn't modify the system database literally. If you chose **Database projects in the current solution**, the default name of the database is based on the name of the database in the project.

If you selected **Different database, different server**, specify a server name.

You can use a (SQLCMD) database variable. If you want to refer to the database with a variable (instead of the literal name), accept or modify the default database variable. A database variable is helpful if you publish the database project from multiple servers and databases. In that situation, a developer can go to **SQLCMD Variables** in the project's property pages and specify the local name of the database. If you leave **Database variable** blank, you can only refer to the database by its literal name. If you specify a database variable name, you can't refer to the database by its literal name.

If you selected **Different database, different server**, a (SQLCMD) server variable is required. A server variable helps publish the database project from multiple servers and databases. In that situation, a developer can go to **SQLCMD Variables** in the project's property pages and specify the local name of the server. You can only refer to the server with the variable, not the server name.

> [!IMPORTANT]  
> In some situations, you can create a database reference with the same name as an existing one. Two database references with the same name can result in unexpected behavior. In this situation, delete both database references.

## Common procedures

The following are common procedures:

### To create a reference to a database on the same server

1. In Solution Explorer, right-click **References** and select **Add Database Reference**.

1. In the **Add Database Reference** dialog box, specify the **Database Location** as **Different database, same server**.

1. Specify the name of the database.

1. Decide whether you want to use a database variable.

1. Copy the example usage and paste it into your .sql file. In the example usage, change `Schema1` to the name of your schema (for example, [dbo]). Also, modify the name of the database object as appropriate for your project.

1. Build the solution.

### To create a reference to a database on another server

1. In Solution Explorer, right-click **References** and select **Add Database Reference**.

1. In the **Add Database Reference** dialog box, specify the **Database Location** as **Different database, different server**.

1. Make sure that the database name is correct.

1. Decide whether you want to use a database variable.

1. Specify the name of the server and the server variable.

1. Copy the example usage and paste it into your .sql file. In the example usage, change `Schema1` to the name of your schema (for example, [dbo]). Also, modify the name of the database object as appropriate for your project.

1. Build the solution.

### To create a composite project

1. In Solution Explorer, right-click **References** and select **Add Database Reference**.

1. Select the source of the database you're referencing (a project in the solution or a DACPAC).

1. In the **Add Database Reference** dialog box, specify the **Database Location** as **Same database**.

1. Copy the example usage and paste it into your .sql file. In the example usage, change `Schema1` to the name of your schema. Also, modify the name of the database object as appropriate for your project.

1. Build the solution.

When you publish this project, you can deploy composite projects in the same solution into a single target:

1. Right-click the project name in **Solution Explorer** and select **Publish** to display the **Publish Database** dialog box.

1. In the **Publish Database** dialog box, select **Advanced**.

1. In the **Advanced Publish Settings** dialog box, make sure that **Include composite objects** is checked in the **Advanced Deployment Options** list.

## Related content

- [Project-Oriented Offline Database Development](project-oriented-offline-database-development.md)
