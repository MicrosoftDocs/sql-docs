---
title: "Converting Access Database Objects (AccessToSQL)"
description: Learn how to select Access database objects after you connect to SQL Server/Azure SQL Database, and then convert the schemas to SQL Server/SQL Database schemas.
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
helpviewer_keywords:
  - "Access databases"
  - "Access databases, converting schemas"
  - "conversion"
  - "conversion, converting schemas"
  - "indexes, altering"
  - "metadata"
  - "metadata, altering"
  - "metadata, converting"
  - "migrating databases, one-click"
  - "migrating databases, single-step"
  - "one-click migration"
  - "single-step migration"
  - "schemas"
  - "schemas, converting"
  - "SQL"
  - "SQL, converting"
  - "syntax"
  - "syntax, converting"
  - "tables, altering"
  - "translating Access to Azure SQL"
  - "translating Access to SQL Server"
---
# Convert Access database objects (AccessToSQL)

After you add Access databases and connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, SQL Server Migration Assistant (SSMA) displays metadata for these objects. You can now select Access database objects, and then convert the schemas into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL schemas.

## The conversion process

Converting database objects takes the object definitions from the Access metadata, converts them into equivalent [!INCLUDE [tsql](../../includes/tsql-md.md)] syntax, and then loads this information into the project. You can then view the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL objects and their properties by using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Metadata Explorer.

> [!IMPORTANT]  
> Converting objects doesn't create the objects in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL. It only converts the object definitions and stores the information in the SSMA project.

During the conversion, SSMA prints status messages to the Output pane, and error, warning, and informational messages to the Error List pane. Use this information to determine whether you need to modify your Access databases or your conversion process to obtain the desired conversion results. You can also use the information in the [Prepare Access databases for migration](preparing-access-databases-for-migration-accesstosql.md) article to determine what is and isn't converted.

## Set conversion options

Before converting objects, review the project conversion options in the **Project Settings** dialog box. By using this dialog box, you can set how SSMA converts indexed memo columns, primary keys, foreign key constraints, timestamps, and tables without indexes. For more information, see [Project Settings (Conversion)](project-settings-conversion-accesstosql.md).

## Conversion results

The following table shows the converted Access objects, and the resulting [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL objects:

| Access object | Resulting SQL Server object |
| --- | --- |
| table | table |
| column | column |
| index | index |
| foreign key | foreign key |
| query | view<br /><br />Most `SELECT` queries are converted to views. Other queries, such as `UPDATE` queries, aren't migrated.<br />`SELECT` queries that take parameters aren't converted, nor are cross-tab queries. |
| report | not converted |
| form | not converted |
| macro | not converted |
| module | not converted |
| default value | default value |
| allow zero length column property | check constraint |
| column validation rule | check constraint |
| table validation rule | check constraint |
| primary key | primary key |

## Convert Access objects

To convert Access database objects, first select the objects you want to convert, and then have SSMA do the conversion. To view output messages during the conversion, on the **View** menu, select **Output**.

1. In Access Metadata Explorer, expand **Access-metabase**, and then expand **Databases**.

1. Do one or more of the following steps:

   - To convert all databases, select the check box next to **Databases**.

   - To convert or omit individual databases, select or clear the check box next to the database name.

   - To convert or omit queries, expand the database, and then select or clear the **Queries** check box.

   - To convert or omit individual tables, expand the database, expand **Tables**, and then select or clear the check box next to the table.

1. Do one of the following steps:

   - To convert schemas, right-click **Databases** and select **Convert Schema**.

     You can also convert individual objects. To convert an object, regardless of which objects are selected, right-click the object, and select **Convert Schema**.

     When you convert an object, it appears in bold in Access Metadata Explorer.

   - To convert, load, and migrate schemas and data in one step, right-click **Databases** and select **Convert, Load, and Migrate**.

1. Review messages in the **Output** pane and any errors and warnings in the **Error List** pane.

## Alter tables and indexes

After you convert Access metadata to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL metadata, and before you load the objects into your target, you can alter the tables and indexes.

1. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Metadata Explorer, select the table or index you want to alter.

1. On the **Table** tab, select the property you want to alter and then enter or select the new setting. For example, you can change **nvarchar(15)** to **nvarchar(20)**, or select a check box to make a table column nullable.

   Move the cursor out of the changed property cell by selecting another row or pressing the <kbd>Tab</kbd> key.

1. Select **Apply**.

You can now view the changes in the code on the **SQL** tab.

## Related content

- [Migrate Access databases to SQL Server and Azure SQL](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)
- [Load converted database objects into SQL Server](loading-converted-database-objects-into-sql-server-accesstosql.md)
