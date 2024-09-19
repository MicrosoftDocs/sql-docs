---
title: "View or change the compatibility level of a database"
description: Learn how to view or change the compatibility level of a database in SQL Server or Azure SQL by using SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 07/25/2024
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords:
  - "compatibility levels [SQL Server], viewing"
  - "compatibility [SQL Server], databases"
  - "compatibility levels [SQL Server], changing"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# View or change the compatibility level of a database

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to view or change the compatibility level of a database in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)], Azure SQL Database, or Azure SQL Managed Instance by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

Before you change the compatibility level of a database, you should understand the effect of the change on your applications. For more information, see [ALTER DATABASE compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

## Permissions

Requires `ALTER` permission on the database.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

To view or change the compatibility level of a database using [SQL Server Management Studio (SSMS)](../../ssms/sql-server-management-studio-ssms.md)

1. Connect to the appropriate server or instance hosting your database.

1. Select the server name in **Object Explorer**.

1. Expand **Databases**, and, depending on the database, either select a user database or expand **System Databases** and select a system database.

   > [!NOTE]  
   > You can't modify the compatibility level of system databases in Azure SQL Database.

1. Right-click the database, and then select **Properties**.

   The **Database Properties** dialog box opens.

1. In the **Select a page** pane, select **Options**.

1. The current compatibility level is displayed in the **Compatibility level** list box.

   To change the compatibility level, select a different option from the list. The available options for different [!INCLUDE [ssDE-md](../../includes/ssde-md.md)] versions are listed in the [ALTER DATABASE Compatibility Level (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#supported-dbcompats) page.

## <a id="TsqlProcedure"></a> Use Transact-SQL

You can use Transact-SQL to view or change the compatibility level of a database using SSMS or [Azure Data Studio](/azure-data-studio/what-is-azure-data-studio).

### View the compatibility level of a database

1. Connect to the appropriate server or instance hosting your database.

1. Open a **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example returns the compatibility level of the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] [sample database](../../samples/adventureworks-install-configure.md).

   ```sql
   USE AdventureWorks2022;
   GO
   SELECT compatibility_level
   FROM sys.databases WHERE name = 'AdventureWorks2022';
   GO
   ```

### Change the compatibility level of a database

1. Connect to the appropriate server or instance hosting your database.

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example changes the compatibility level of the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database to `160`, which is the compatibility level for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

   ```sql
   ALTER DATABASE AdventureWorks2022
   SET COMPATIBILITY_LEVEL = 160;
   GO
   ```

## Related content

- [ALTER DATABASE (Transact-SQL) Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
