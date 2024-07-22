---
title: "Create a database"
description: Create a database in SQL Server by using SQL Server Management Studio or Transact-SQL. View recommendations for the procedure.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 07/19/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "databases [SQL Server], creating"
  - "database creation [SQL Server], SQL Server Management Studio"
  - "creating databases"
---
# Create a database

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to create a database in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

To create a database in Azure SQL Database using T-SQL, see [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md).

## Limitations

A maximum of 32,767 databases can be specified on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## Prerequisites

The `CREATE DATABASE` statement must run in autocommit mode (the default transaction management mode) and isn't allowed in an explicit or implicit transaction.

## Recommendations

The [master](master-database.md) database should be backed up whenever a user database is created, modified, or dropped.

When you create a database, make the data files as large as possible based on the maximum amount of data you expect in the database.

## Permissions

Requires `CREATE DATABASE` permission in the `master` database, or requires `CREATE ANY DATABASE`, or `ALTER ANY DATABASE` permission.

To maintain control over disk use on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], permission to create databases is typically limited to a few [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] logins.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

1. In **Object Explorer**, connect to an instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand that instance.

1. Right-click **Databases**, and then select **New Database**.

1. In **New Database**, enter a database name.

1. To create the database by accepting all default values, select **OK**; otherwise, continue with the following optional steps.

1. To change the owner name, select (**...**) to select another owner.

   > [!NOTE]  
   > The **Use full-text indexing** option is always checked and dimmed because all user databases are full-text enabled.

1. To change the default values of the primary data and transaction log files, in the **Database files** grid, select the appropriate cell and enter the new value. For more information, see [Add Data or Log Files to a Database](add-data-or-log-files-to-a-database.md).

1. To change the collation of the database, select the **Options** page, and then select a collation from the list.

1. To change the recovery model, select the **Options** page and select a recovery model from the list.

1. To change database options, select the **Options** page, and then modify the database options. For a description of each option, see [ALTER DATABASE SET options](../../t-sql/statements/alter-database-transact-sql-set-options.md).

1. To add a new filegroup, select the **Filegroups** page. Select **Add** and then enter the values for the filegroup.

1. To add an extended property to the database, select the **Extended Properties** page.

   1. In the **Name** column, enter a name for the extended property.

   1. In the **Value** column, enter the extended property text. For example, enter one or more statements that describe the database.

1. To create the database, select **OK**.

## <a id="TsqlProcedure"></a> Use Transact-SQL

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example creates the database `Sales`. Because the keyword `PRIMARY` isn't used, the first file (`Sales_dat`) becomes the primary file. Because `MB` or `KB` aren't specified in the `SIZE` parameter for the `Sales_dat` file, it uses `MB` and is allocated in megabytes. The `Sales_log` file is allocated in megabytes because the `MB` suffix is explicitly stated in the `SIZE` parameter.

```sql
USE master;
GO

CREATE DATABASE Sales ON
(NAME = Sales_dat,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\saledat.mdf',
    SIZE = 10,
    MAXSIZE = 50,
    FILEGROWTH = 5)
LOG ON
(NAME = Sales_log,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\salelog.ldf',
    SIZE = 5 MB,
    MAXSIZE = 25 MB,
    FILEGROWTH = 5 MB);
GO
```

For more examples, see [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md).

## Related content

- [Database Files and Filegroups](database-files-and-filegroups.md)
- [Database detach and attach (SQL Server)](database-detach-and-attach-sql-server.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [Add Data or Log Files to a Database](add-data-or-log-files-to-a-database.md)
