---
title: "Delete a Database"
description: Learn how to delete a user-defined database in SQL Server Management Studio in SQL Server by using SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 01/26/2026
ms.service: sql
ms.subservice: supportability
ms.topic: how-to
helpviewer_keywords:
  - "database removal [SQL Server], SQL Server Management Studio"
  - "removing databases"
  - "deleting databases"
  - "dropping databases"
  - "databases [SQL Server], dropping"
  - "database removal [SQL Server]"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Delete a database

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to delete a user-defined database in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

<a id="Prerequisites"></a>

## Prerequisites

- Delete any database snapshots that exist on the database. For more information, see [Drop a Database Snapshot](drop-a-database-snapshot-transact-sql.md).

- If the database is involved in log shipping, remove log shipping.

- If the database is published for transactional replication, or published or subscribed to merge replication, remove replication from the database.

> [!WARNING]  
> Consider taking a full backup of the database before dropping it. You can recreate a deleted database only by restoring a full backup. For more information, see [Quickstart: Backup and restore a SQL Server database with SSMS](../backup-restore/quickstart-backup-restore-database.md).

## Permissions

To run `DROP DATABASE`, you need `CONTROL` permission on the database.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. In **Object Explorer**, connect to an instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.

1. Expand **Databases**, right-click the database to delete, and then select **Delete**.

1. Confirm the correct database is selected, and then select **OK**.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

For more information, see [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md).

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example removes the `Sales` and `NewSales` databases.

  ```sql
  USE master;
  GO
  DROP DATABASE Sales, NewSales;
  ```

<a id="FollowUp"></a>

## Follow up: After deleting a database

Back up the `master` database. If you need to restore `master`, any database that you deleted since the last backup of `master` still has references in the system catalog views and might cause error messages.

<a id="Restrictions"></a>

## Limitations

You can't delete system databases. For more information, see [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md).

## Related content

- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [DROP DATABASE (Transact-SQL)](../../t-sql/statements/drop-database-transact-sql.md)
- [Restore and recovery overview (SQL Server)](../backup-restore/restore-and-recovery-overview-sql-server.md)
