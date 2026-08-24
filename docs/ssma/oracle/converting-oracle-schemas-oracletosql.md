---
title: "Converting Oracle Schemas (OracleToSQL)"
description: Learn how to convert Oracle database objects to SQL Server database objects by using SSMA for Oracle.
author: nilabjaball
ms.author: niball
ms.reviewer: randolphwest
ms.date: 11/12/2025
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.oracle.convert.f1"
helpviewer_keywords:
  - "Conversion Results"
---

# Convert Oracle schemas (OracleToSQL)

After you connect to Oracle, connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and set project and data mapping options, you can convert Oracle database objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database objects.

## The conversion process

Converting database objects takes the object definitions from Oracle, converts them to similar [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] objects, and then loads this information into the metadata for Microsoft SQL Server Migration Assistant (SSMA) for Oracle. It doesn't load the information into the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can then view the objects and their properties by using the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer.

During the conversion, SSMA prints output messages to the **Output** pane and error messages to the **Error List** pane. Use the output and error information to determine whether you have to modify your Oracle databases or your conversion process to obtain the desired conversion results.

## Set conversion options

Before converting objects, review the project conversion options in the **Project Settings** dialog box. By using this dialog box, you can determine how SSMA converts functions and global variables. For more information, see [Project Settings (Conversion)](project-settings-conversion-oracletosql.md).

## Conversion results

The following table shows which Oracle objects are converted, and the resulting [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] objects:

| Oracle objects | Resulting SQL Server objects |
| --- | --- |
| Functions | If the function can be directly converted to [!INCLUDE [tsql](../../includes/tsql-md.md)], SSMA creates a function.<br /><br />In some cases, the function must be converted to a stored procedure. In this case, SSMA creates a stored procedure and a function that calls the stored procedure. |
| Procedures | If the procedure can be directly converted to [!INCLUDE [tsql](../../includes/tsql-md.md)], SSMA creates a stored procedure.<br /><br />In some cases, a stored procedure must be called in an autonomous transaction. In this case, SSMA creates two stored procedures: one that implements the procedure, and another that calls the procedure that implements. |
| Packages | SSMA creates a set of stored procedures and functions that are unified by similar object names. |
| Sequences | SSMA creates sequence objects ([!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] and later versions) or emulates Oracle sequences. |
| Tables with dependent objects such as indexes and triggers | SSMA creates tables with dependent objects. |
| Views with dependent objects, such as triggers | SSMA creates views with dependent objects. |
| Materialized views | SSMA creates indexed views on SQL Server with some exceptions. Conversion fails if the materialized view includes one or more of the following constructs:<br /><br />User-defined function.<br /><br />Nondeterministic field, function, or expression in `SELECT`, `WHERE`, or `GROUP BY` clauses.<br /><br />Usage of Float column in `SELECT*`, `WHERE`, or `GROUP BY` clauses (special case of previous issue).<br /><br />Custom data type (including nested tables).<br /><br />`COUNT` (distinct &lt;field&gt;).<br /><br />`FETCH`.<br /><br />`OUTER` joins (`LEFT`, `RIGHT`, or `FULL`).<br /><br />Subquery, other view.<br /><br />`OVER`, `RANK`, `LEAD`, or `LOG`.<br /><br />`MIN`, `MAX`.<br /><br />`UNION`, `MINUS`, `INTERSECT`.<br /><br />`HAVING`. |
| Trigger | SSMA creates triggers based on the following rules:<br /><br />`BEFORE` triggers are converted to `INSTEAD OF` triggers.<br /><br />`AFTER` triggers are converted to `AFTER` triggers.<br /><br />`INSTEAD OF` triggers are converted to `INSTEAD OF` triggers. Multiple `INSTEAD OF` triggers defined on the same operation are combined into one trigger.<br /><br />Row-level triggers are emulated by using cursors.<br /><br />Compound triggers are converted to `INSTEAD OF` triggers. Multiple compound triggers are combined into a single trigger.<br /><br />Cascading triggers are converted into multiple individual triggers. |
| Synonyms | Synonyms are created for the following object types:<br /><br />Tables and object tables.<br />Views and object views.<br />Stored procedures.<br />Functions.<br /><br />Synonyms for the following objects are resolved and replaced by direct object references:<br /><br />Sequences.<br />Packages.<br />Java class schema objects.<br />User-defined object types.<br /><br />Synonyms for another synonym can't be migrated and are marked as errors.<br /><br />Synonyms aren't created for materialized views. |
| User-defined types | SSMA doesn't provide support for conversion of user-defined types. User-defined types, including its usage in PL/SQL programs, are marked with special conversion errors guided by the following rules:<br /><br />Table column of a user-defined type is converted to `VARCHAR(8000)`.<br /><br />Argument of a user-defined type to a stored procedure or function is converted to `VARCHAR(8000)`.<br /><br />Variable of a user-defined type in PL/SQL block is converted to `VARCHAR(8000)`.<br /><br />Object table is converted to a standard table.<br /><br />Object view is converted to a standard view. |

## Convert Oracle database objects

To convert Oracle database objects, select the objects that you want to convert, and then have SSMA perform the conversion. To view output messages during the conversion, on the **View** menu, select **Output**.

**Convert Oracle objects to SQL Server syntax**

1. In Oracle Metadata Explorer, expand the Oracle server, and then expand **Schemas**.

1. Select objects to convert:

   - To convert all schemas, select the check box next to **Schemas**.
   - To convert or omit a database, select the check box next to the schema name.
   - To convert or omit a category of objects, expand a schema, and then select or clear the check box next to the category.
   - To convert or omit individual objects, expand the category folder, and then select or clear the check box next to the object.

1. To convert all selected objects, right-click **Schemas** and select **Convert Schema**.

    You can also convert individual objects or categories of objects by right-clicking the object or its parent folder, and then selecting **Convert Schema**.

## View conversion problems

Some Oracle objects might not be converted. You can determine the conversion success rates by viewing the summary conversion report.

### View a summary report

1. In Oracle Metadata Explorer, select **Schemas**.

1. In the right pane, select the **Report** tab.

   This report shows the summary assessment report for all database objects that were assessed or converted. You can also view a summary report for individual objects.

   - To view the report for an individual schema, select the schema in Oracle Metadata Explorer.
   - To view the report for an individual object, select the object in Oracle Metadata Explorer. Objects that have conversion problems have a red error icon.

For objects that failed conversion, you can view the syntax that resulted in the conversion failure.

### View individual conversion problems

1. In Oracle Metadata Explorer, expand **Schemas**.

1. Expand the schema with a red error icon.

1. Under the schema, expand a folder with a red error icon.

1. Select the object with a red error icon.

1. In the right pane, select the **Report** tab.

1. There's a dropdown list at the top of the **Report** tab. If the list shows **Statistics**, change the selection to **Source**. SSMA displays the source code and several buttons immediately above the code.

1. Select the **Next Problem** button, which displays a red error icon with an arrow that points to the right. SSMA highlights the first problematic source code it finds in the current object.

For each item that couldn't be converted, choose what you want to do with that object:

- You can modify the source code for procedures on the **SQL** tab.
- You can modify the object in the Oracle database to remove or revise problematic code. To load the updated code into SSMA, you have to update the metadata. For more information, see [Connecting to Oracle Database](connecting-to-oracle-database-oracletosql.md).
- You can exclude the object from migration. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer and Oracle Metadata Explorer, clear the check box next to the item. Then load the objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and migrate data from Oracle.

> [!div class="nextstepaction"]
> [Load converted database objects into SQL Server](loading-converted-database-objects-into-sql-server-oracletosql.md)
