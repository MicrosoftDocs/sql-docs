---
title: "View the Dependencies of a Table"
description: "View the dependencies of a table with SQL Server Management Studio or Transact-SQL."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 05/02/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "table dependencies [SQL Server]"
  - "dependencies [SQL Server], tables"
  - "displaying dependences"
  - "viewing dependencies"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# View the dependencies of a table

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

You can view a table's dependencies in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

## Permissions

Requires `VIEW DEFINITION` permission on the database and `SELECT` permission on `sys.sql_expression_dependencies` for the database. By default, `SELECT` permission is granted only to members of the **db_owner** fixed database role. When `SELECT` and `VIEW DEFINITION` permissions are granted to another user, the grantee can view all dependencies in the database.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

#### View the objects on which a table depends

1. In **Object Explorer**, expand **Databases**, expand a database, and then expand **Tables**.

1. Right-click a table, and then select **View Dependencies**.

1. In the **Object Dependencies**_\<object name\>_ dialog box, select either **Objects that depend on** _\<object name\>_, or **Objects on which**_\<object name\>_**depends**.

1. Select an object in the **Dependencies** grid. The type of object (such as "Trigger" or "Stored Procedure"), appears in the **Type** box.

> [!NOTE]  
> Viewing dependencies using **Object Explorer** > **View Dependencies** isn't supported in Azure Synapse Analytics, instead use [sys.sql_expression_dependencies](../system-catalog-views/sys-sql-expression-dependencies-transact-sql.md). Azure Synapse Analytics SQL pools support tables, views, filtered statistics, and Transact-SQL stored procedures entity types from this list. Dependency information is created and maintained for tables, views, and filtered statistics only.

## <a id="TsqlProcedure"></a> Use Transact-SQL

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

#### View the objects that depend on a table

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

   ```sql
   USE AdventureWorks2022;
   GO
   SELECT * FROM sys.sql_expression_dependencies
   WHERE referencing_id = OBJECT_ID(N'Production.vProductAndDescription');
   GO
   ```

#### View the dependencies of a table

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. The following example returns the objects that depend on the table `Production.Product`. Copy and paste the following example into the query window and select **Execute**.

   ```sql
   USE AdventureWorks2022;
   GO
   SELECT * FROM sys.sql_expression_dependencies
   WHERE referenced_id = OBJECT_ID(N'Production.Product');
   GO
   ```

## Related content

- [sys.sql_expression_dependencies (Transact-SQL)](../system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)
