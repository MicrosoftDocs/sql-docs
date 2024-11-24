---
title: "Converting MySQL Databases (MySQLToSQL)"
description: Learn how to convert MySQL database objects to SQL Server or Azure SQL Database objects with SSMA, after you connect and set project and data mapping options.
author: nilabjaball
ms.author: niball
ms.reviewer: randolphwest
ms.date: 11/19/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.mysql.convert.f1"
---
# Convert MySQL Databases (MySQLToSQL)

After you connect to MySQL, connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, and set project and data mapping options, you can convert MySQL database objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database objects, using SQL Server Migration Assistant (SSMA) for MySQL.

## The conversion process

Converting database objects takes the object definitions from MySQL, converts them to similar [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL objects, and then loads this information into the SSMA metadata. It doesn't load the information into the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can then view the objects and their properties by using the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure Metadata Explorer.

During the conversion, SSMA prints output messages to the Output pane and error messages to the Error List pane. Use the output and error information to determine whether you have to modify your MySQL databases or your conversion process to obtain the desired conversion results.

## Set conversion options

Before converting objects, review the project conversion options in the **Project Settings** dialog box. By using this dialog box, you can set how SSMA converts tables and indexes. For more information, see [Project Settings (Conversion)](project-settings-conversion-mysqltosql.md).

## Conversion results

The following table shows which MySQL objects are converted, and the resulting [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] objects:

| MySQL objects | Resulting SQL Server objects |
| --- | --- |
| Tables with dependent objects such as indexes | SSMA creates tables with dependent objects. Table is converted with all indexes and constraints. Indexes are converted into separate [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] objects.<br /><br />**Spatial data type mapping** can be performed only at table node level.<br /><br />For more information on the Table Conversion settings, see [Conversion Settings](conversion-settings-mysqltosql.md) |
| Functions | If the function can be directly converted to Transact-SQL, SSMA creates a function. In some cases, the function must be converted to a stored procedure. This can be done by using **Function Conversion** in Project Settings. In this case, SSMA creates a stored procedure and a function that calls the stored procedure.<br /><br />Choices given:<br /><br />- Convert according to project settings<br />- Convert to function<br /><br />- Convert to stored procedure<br /><br />For more information on Function Conversion settings, see [Conversion Settings](conversion-settings-mysqltosql.md) |
| Procedures | If the procedure can be directly converted to Transact-SQL, SSMA creates a stored procedure. In some cases, a stored procedure must be called in an autonomous transaction. In this case, SSMA creates two stored procedures: one that implements the procedure, and another that is used for calling the implementing stored procedure. |
| Database Conversion | Databases as MySQL objects aren't directly converted by SSMA for MySQL. MySQL databases are treated more like a schema name and all the physical parameters are lost during conversion. SSMA for MySQL uses [Mapping MySQL Databases to SQL Server Schemas](mapping-mysql-databases-to-sql-server-schemas-mysqltosql.md) to map objects from MySQL database to appropriate SQL Server database/schema pair. |
| Trigger Conversion | SSMA creates triggers based on the following rules:<br /><br />- BEFORE triggers are converted into INSTEAD OF T-SQL triggers<br /><br />- AFTER triggers are converted into AFTER T-SQL triggers with or without iterations per rows. |
| View Conversion | SSMA creates views with dependent objects |
| Statement Conversion | - Each SQL statement object might contain a single MySQL statement (like DDL, DML, and other types of statements) or BEGIN ... END block.<br /><br />- In a MultiStatement Conversion:BEGIN ... END block conversion, the SQL statement can also contain a BEGIN ... END block like one in procedure, function, or trigger definition. Those blocks should be converted the same way they're being converted for the single MySQL statement objects. |

## Convert MySQL database objects

To convert MySQL database objects, you first select the objects that you want to convert, and then have SSMA perform the conversion. To view output messages during the conversion, on the **View** menu, select **Output**.

### Convert MySQL objects to SQL Server or Azure SQL syntax

1. In MySQL Metadata Explorer, expand the MySQL server, and then expand **Databases**.

1. Select objects to convert:

   - To convert all schemas, select the check box next to **Databases**.

   - To convert or omit a database, select the check box next to the Database name.

   - To convert or omit a category of objects, expand a schema, and then select or clear the check box next to the category.

   - To convert or omit individual objects, expand the category folder, and then select or clear the check box next to the object.

1. To convert all selected objects, right-click **Databases** and select **Convert Schema**.

    You can also convert individual objects or categories of objects by right-clicking the object or its parent folder, and then selecting **Convert Schema**.

## View conversion problems

Some MySQL objects might not be converted. You can determine the conversion success rates by viewing the summary conversion report.

### View a summary report

1. In MySQL Metadata Explorer, select **Databases**.

1. In the right pane, select the **Report** tab.

   This report shows the summary assessment report for all database objects that were assessed or converted. You can also view a summary report for individual objects:

   - To view the report for an individual schema, select the database in MySQL Metadata Explorer.

   - To view the report for an individual object, select the object in MySQL Metadata Explorer. Objects that have conversion problems have a red error icon.

For objects that failed conversion, you can view the syntax that resulted in the conversion failure.

### View individual conversion problems

1. In MySQL Metadata Explorer, expand **Databases**.

1. Expand the database that shows a red error icon.

1. Under the database, expand a folder that has a red error icon.

1. Select the object that has a red error icon.

1. In the right pane, select the **Report** tab.

1. At the top of the **Report** tab, is a dropdown list. If the list shows **Statistics**, change the selection to **Source**.

   SSMA displays the source code and several buttons immediately above the code.

1. Select the **Next Problem** button. This is a red error icon with a right-pointing arrow.

   SSMA highlights the first problematic source code it finds in the current object.

For each item that couldn't be converted, you have to determine what you want to do with that object:

- You can modify the object in the MySQL database to remove or revise problematic code. To load the updated code into SSMA, you have to update the metadata. For more information, see [Connecting to MySQL](connecting-to-mysql-mysqltosql.md)

- You can exclude the object from migration. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure Metadata Explorer and MySQL Metadata Explorer, clear the check box next to the item before loading the objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL and migrating data from MySQL.

## Related content

- [Migrating MySQL Databases to SQL Server - Azure SQL Database (MySQLToSQL)](migrating-mysql-databases-to-sql-server-azure-sql-db-mysqltosql.md)
- [Loading Converted Database Objects into SQL Server (MySQLToSQL)](loading-converted-database-objects-into-sql-server-mysqltosql.md)
