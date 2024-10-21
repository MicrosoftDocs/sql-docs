---
title: "Convert Db2 schemas (Db2ToSQL)"
description: Learn how to convert Db2 database objects to SQL Server database objects in SSMA for Db2.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.db2.convert.f1"
---
# Convert Db2 schemas (Db2ToSQL)

After you connect to both Db2 and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and set project and data mapping options, you can convert Db2 database objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database objects.

## The conversion process

Converting database objects takes the object definitions from Db2, converts them to similar [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] objects, and then loads this information into the SQL Server Migration Assistant (SSMA) metadata. It doesn't load the information into the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can then view the objects and their properties by using the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer.

During the conversion, SSMA prints output messages to the Output pane and error messages to the Error List pane. Use the output and error information to determine whether you have to modify your Db2 databases or your conversion process to obtain the desired conversion results.

## Set conversion options

Before converting objects, review the project conversion options in the **Project Settings** dialog box. Use this dialog box to set how SSMA converts functions and global variables. For more information, see [Project Settings (Conversion)](project-settings-conversion-db2tosql.md).

## Conversion results

The following table shows which Db2 objects are converted, and the resulting [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] objects:

| Db2 objects | Resulting SQL Server objects |
| --- | --- |
| Data types | SSMA maps every type except the following types:<br /><br />`CLOB`: Some native functions that work with this type aren't supported (for example, `CLOB_EMPTY()`)<br /><br />`BLOB`: Some native functions for work with this type aren't supported (for example, `BLOB_EMPTY()`)<br /><br />`DBLOB`: Some native functions for work with this type aren't supported (for example, `DBLOB_EMPTY()`) |
| User-defined types | SSMA maps the following user-defined objects:<br /><br />- Distinct type<br />- Structured type<br />- SQL PL data types<br /><br />**Note:** Weak cursor types aren't supported. |
| Special registers | SSMA only maps the following registers:<br /><br />`CURRENT TIMESTAMP`<br />`CURRENT DATE`<br />`CURRENT TIME`<br />`CURRENT TIMEZONE`<br />`CURRENT USER`<br />`SESSION_USER` and `USER`<br />`SYSTEM_USER`<br />`CURRENT CLIENT_APPLNAME`<br />`CURRENT CLIENT_WRKSTNNAME`<br />`CURRENT LOCK TIMEOUT`<br />`CURRENT SCHEMA`<br />`CURRENT SERVER`<br />`CURRENT ISOLATION`<br /><br />Other special registers aren't mapped to SQL Server semantics. |
| `CREATE TABLE` | SSMA maps `CREATE TABLE` with the following exceptions:<br /><br />Multidimensional clustering (MDC) tables<br />Range-clustered tables (RCT)<br />Partitioned tables<br />Detached table<br />`DATA CAPTURE` clause<br />`IMPLICITLY HIDDEN` option<br />`VOLATILE` option |
| `CREATE VIEW` | SSMA maps `CREATE VIEW` with `WITH LOCAL CHECK OPTION` but other options aren't mapped to SQL Server semantics |
| `CREATE INDEX` | SSMA maps `CREATE INDEX` with the following exceptions:<br /><br />XML index<br />`BUSINESS_TIME WITHOUT OVERLAPS` option<br />`PARTITIONED` clause<br />`SPECIFICATION ONLY` option<br />`EXTEND USING` option<br />`MINPCTUSED` option<br />`PAGE SPLIT` option |
| Triggers | SSMA maps the following trigger semantics:<br /><br />`AFTER` / `FOR EACH ROW` triggers<br /><br />`AFTER` / `FOR EACH STATEMENT` triggers<br /><br />`BEFORE` / `FOR EACH ROW` and `INSTEAD OF` / `FOR EACH ROW` triggers |
| Sequences | Mapped. |
| `SELECT` statement | SSMA maps `SELECT` with the following exceptions:<br /><br />data-change-table-reference clause - Partially mapped, but `FINAL` tables aren't supported<br /><br />table-reference clause - Partially mapped, but only-table-reference, outer-table-reference, analyze-table-expression, collection-derived-table, xmltable-expression aren't mapped to SQL Server semantics<br /><br />`period-specification` clause - Not mapped.<br /><br />Continue-handler clause - Not mapped.<br /><br />Typed-correlation clause - Not mapped.<br /><br />Concurrent-access-resolution clause - Not mapped. |
| `VALUES` statement | Mapped. |
| `INSERT` statement | Mapped. |
| `UPDATE` statement | SSMA maps `UPDATE` with the following exceptions:<br /><br />Table-reference clause - only-table-reference isn't mapped to SQL Server semantics<br /><br />Period clause - Isn't mapped. |
| `MERGE` statement | SSMA maps `MERGE` with the following exceptions:<br /><br />Single vs Multiple Occurrences of Each clause - Mapped to SQL Server semantics for limited occurrences of each clause<br /><br />`SIGNAL` clause - doesn't map to SQL Server semantics<br /><br />Mixed `UPDATE` and `DELETE` clauses - doesn't map to SQL Server semantics<br /><br />Period-clause - doesn't map to SQL Server semantics |
| `DELETE` statement | SSMA maps `DELETE` with the following exceptions:<br /><br />Table-reference clause - only-table-reference isn't mapped to SQL Server semantics<br /><br />Period clause - doesn't map to SQL Server semantics |
| Isolation level and lock type | Mapped. |
| Procedures (SQL) | Mapped. |
| Procedures (external) | Require manual update. |
| Procedures (sourced) | Don't map to SQL Server semantics. |
| Assignment statement | Mapped. |
| `CALL` statement for a procedure | Mapped. |
| `CASE` statement | Mapped. |
| `FOR` statement | Mapped. |
| `GOTO` statement | Mapped. |
| `IF` statement | Mapped. |
| `ITERATE` statement | Mapped. |
| `LEAVE` statement | Mapped. |
| `LOOP` statement | Mapped. |
| `REPEAT` statement | Mapped. |
| `RESIGNAL` statement | Conditions aren't supported. Messages can be optional. |
| `RETURN` statement | Mapped. |
| `SIGNAL` statement | Conditions aren't supported. Messages can be optional. |
| `WHILE` statement | Mapped. |
| `GET DIAGNOSTICS` statement | SSMA maps `GET DIAGNOSTICS` with the following exceptions:<br /><br />`ROW_COUNT` - Mapped.<br /><br />`Db2_RETURN_STATUS` - Mapped.<br /><br />`MESSAGE_TEXT` - Mapped.<br /><br />`Db2_SQL_NESTING_LEVEL` - doesn't map to SQL Server semantics<br /><br />`Db2_TOKEN_STRING` - doesn't map to SQL Server semantics |
| Cursors | SSMA maps cursors with the following exceptions:<br /><br />`ALLOCATE CURSOR` statement - doesn't map to SQL Server semantics<br /><br />`ASSOCIATE LOCATORS` statement - doesn't map to SQL Server semantics<br /><br />`DECLARE CURSOR` statement - Returnability clause isn't mapped to SQL Server semantics<br /><br />`FETCH` statement - Partial mapping. Variables as target are supported only. `SQLDA DESCRIPTOR` isn't mapped to SQL Server semantics |
| Variables | Mapped. |
| Exceptions, handlers, and conditions | SSMA maps *exception handling* with the following exceptions:<br /><br />`EXIT` handlers - Mapped.<br /><br />`UNDO` handlers - Mapped.<br /><br />`CONTINUE` handlers - Not mapped.<br /><br />Conditions - It doesn't map to SQL Server semantics. |
| Dynamic SQL | Not mapped. |
| Aliases | Mapped. |
| Nicknames | Partial mapping. Manual processing is required for underlying object |
| Synonyms | Mapped. |
| Standard Functions in Db2 | SSMA maps Db2 standard functions when an equivalent function is available in SQL Server: |
| Authorization | Not mapped. |
| Predicates | Mapped. |
| `SELECT INTO` statement | Not mapped. |
| `VALUES INTO` statement | Not mapped. |
| Transaction control | Not mapped. |

## Convert Db2 database objects

To convert Db2 database objects, you first select the objects that you want to convert, and then have SSMA perform the conversion. To view output messages during the conversion, navigate to **View** > **Output**.

**To convert Db2 objects to SQL Server syntax**

1. In Db2 Metadata Explorer, expand the Db2 server, and then expand **Schemas**.

1. Select objects to convert:

    - To convert all schemas, select the check box next to **Schemas**.

    - To convert or omit a database, select the check box next to the schema name.

    - To convert or omit a category of objects, expand a schema, and then select or clear the check box next to the category.

    - To convert or omit individual objects, expand the category folder, and then select or clear the check box next to the object.

1. To convert all selected objects, right-click **Schemas** and select **Convert Schema**.

    You can also convert individual objects or categories of objects by right-clicking the object or its parent folder, and then selecting **Convert Schema**.

## View conversion problems

Some Db2 objects might not be converted. You can determine the conversion success rates by viewing the summary conversion report.

### View a summary report

1. In Db2 Metadata Explorer, select **Schemas**.

1. In the right pane, select the **Report** tab.

   This report shows the summary assessment report for all database objects that were assessed or converted. You can also view a summary report for individual objects:

   - To view the report for an individual schema, select the schema in Db2 Metadata Explorer.

   - To view the report for an individual object, select the object in Db2 Metadata Explorer. Objects that have conversion problems have a red error icon.

For objects that failed conversion, you can view the syntax that resulted in the conversion failure.

### View individual conversion problems

1. In Db2 Metadata Explorer, expand **Schemas**.

1. Expand the schema that shows a red error icon.

1. Under the schema, expand a folder that has a red error icon.

1. Select the object that has a red error icon.

1. In the right pane, select the **Report** tab.

1. At the top of the **Report** tab, is a dropdown list. If the list shows **Statistics**, change the selection to **Source**.

   SSMA displays the source code and several buttons immediately above the code.

1. Select the **Next Problem** button (a red error icon with a right-pointing arrow).

   SSMA highlights the first problematic source code it finds in the current object.

For each item that couldn't be converted, you've to determine what you want to do with that object:

- You can modify the source code for procedures on the **SQL** tab.

- You can modify the object in the Db2 database to remove or revise problematic code. To load the updated code into SSMA, you must update the metadata. For more information, see [Connect to Db2 database](connecting-to-db2-database-db2tosql.md).

- You can exclude the object from migration. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer and Db2 Metadata Explorer, clear the check box next to the item before loading the objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and migrating data from Db2.

## Related content

- [Migrate Db2 Data into SQL Server (Db2ToSQL)](migrating-db2-data-into-sql-server-db2tosql.md)
- [Load converted database objects into SQL Server (Db2ToSQL)](loading-converted-database-objects-into-sql-server-db2tosql.md)
