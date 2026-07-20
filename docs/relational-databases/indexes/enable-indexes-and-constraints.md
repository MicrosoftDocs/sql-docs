---
title: "Enable Indexes and Constraints"
description: Learn how to enable indexes and constraints in the SQL Server Database Engine.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 07/16/2025
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "indexes [SQL Server], enabling"
  - "nonclustered indexes [SQL Server], enabling a disabled index"
  - "index enabling [SQL Server]"
  - "disabled indexes [SQL Server], how to enable"
  - "constraints [SQL Server], enabling"
  - "clustered indexes, enabling disabled indexes"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Enable indexes and constraints

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

This article describes how to enable a disabled index in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. After an index is disabled, it remains in a disabled state until it rebuilds or is dropped.

## Limitations

After the index rebuilds, any constraints that were disabled because of disabling the index must be manually enabled. `PRIMARY KEY` and `UNIQUE` constraints are enabled by rebuilding the associated index. This index must be rebuilt (enabled) before you can enable `FOREIGN KEY` constraints that reference the `PRIMARY KEY` or `UNIQUE` constraint. `FOREIGN KEY` constraints are enabled by using the `ALTER TABLE CHECK CONSTRAINT` statement.

Rebuilding a disabled clustered index can't be performed when the `ONLINE` option is set to `ON`.

When the clustered index is disabled or enabled and the nonclustered index is disabled, the clustered index action has the following results on the disabled nonclustered index.

| Clustered index action | Disabled nonclustered index status |
| --- | --- |
| `ALTER INDEX REBUILD` | Remains disabled |
| `ALTER INDEX ALL REBUILD` | Rebuilt and enabled |
| `DROP INDEX` | Rebuilt and enabled |
| `CREATE INDEX WITH DROP_EXISTING` | Remains disabled |

Creating a new clustered index behaves the same as `ALTER INDEX ALL REBUILD`.

Allowed actions on nonclustered indexes associated with a clustered index depend on the state, whether disabled or enabled, of both index types. The following table summarizes the allowed actions on nonclustered indexes.

| Nonclustered index action | When both the clustered and nonclustered indexes are disabled | When the clustered index is enabled and the nonclustered index is in either state |
| --- | --- | --- |
| `ALTER INDEX REBUILD` | The action fails | The action succeeds |
| `DROP INDEX` | The action succeeds | The action succeeds |
| `CREATE INDEX WITH DROP_EXISTING` | The action fails | The action succeeds |

When rebuilding disabled compressed nonclustered indexes, `data_compression` defaults to `none`, meaning that indexes are uncompressed. This is due to compression settings metadata is lost when nonclustered indexes are disabled. To work around this issue, you must specify explicit data compression in rebuild statement.

## Permissions

Requires `ALTER` permission on the table or view. If using `DBCC DBREINDEX`, you must either own the table or be a member of the **sysadmin** fixed server role, or a member of the **db_ddladmin** or **db_owner** fixed database roles.

## Use SQL Server Management Studio

### Enable a disabled index

1. In Object Explorer, select the plus sign to expand the database that contains the table on which you want to enable an index.

1. Select the plus sign to expand the **Tables** folder.

1. Select the plus sign to expand the table on which you want to enable an index.

1. Select the plus sign to expand the **Indexes** folder.

1. Right-click the index you want to enable and select **Rebuild**.

1. In the **Rebuild Indexes** dialog box, verify that the correct index is in the **Indexes to rebuild** grid and select **OK**.

### Enable all indexes on a table

1. In Object Explorer, select the plus sign to expand the database that contains the table on which you want to enable the indexes.

1. Select the plus sign to expand the **Tables** folder.

1. Select the plus sign to expand the table on which you want to enable the indexes.

1. Right-click the **Indexes** folder and select **Rebuild All**.

1. In the **Rebuild Indexes** dialog box, verify that the correct indexes are in the **Indexes to rebuild** grid and select **OK**. To remove an index from the **Indexes to rebuild** grid, select the index and then press the Delete key.

The following information is available in the **Rebuild Indexes** dialog box:

## Use Transact-SQL

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### Enable a disabled index using ALTER INDEX

Execute the following Transact-SQL script. This example enables the `IX_Employee_OrganizationLevel_OrganizationNode` index on the `HumanResources.Employee` table.

```sql
USE AdventureWorks2022;
GO

ALTER INDEX IX_Employee_OrganizationLevel_OrganizationNode
ON HumanResources.Employee REBUILD;
GO
```

### Enable a disabled index using CREATE INDEX

Execute the following Transact-SQL script. This example recreates the `IX_Employee_OrganizationLevel_OrganizationNode` index on the `HumanResources.Employee` table, using the `OrganizationLevel` and `OrganizationNode` columns, and then deletes the existing `IX_Employee_OrganizationLevel_OrganizationNode` index.

```sql
USE AdventureWorks2022;
GO

CREATE INDEX IX_Employee_OrganizationLevel_OrganizationNode
ON HumanResources.Employee(OrganizationLevel, OrganizationNode) WITH (DROP_EXISTING = ON);
GO
```

### Enable a disabled index using DBCC DBREINDEX

> [!NOTE]  
> [!INCLUDE [ssnotedepfutureavoid-md](../../includes/ssnotedepfutureavoid-md.md)]

Execute the following Transact-SQL script. This example enables the `IX_Employee_OrganizationLevel_OrganizationNode` index on the `HumanResources.Employee` table.

```sql
USE AdventureWorks2022;
GO

DBCC DBREINDEX ("HumanResources.Employee", IX_Employee_OrganizationLevel_OrganizationNode);
GO
```

### Enable all indexes on a table using ALTER INDEX

Execute the following Transact-SQL script. This example enables all indexes on the `HumanResources.Employee` table.

```sql
USE AdventureWorks2022;
GO

ALTER INDEX ALL
ON HumanResources.Employee REBUILD;
GO
```

### Enable all indexes on a table using DBCC DBREINDEX

> [!NOTE]  
> [!INCLUDE [ssnotedepfutureavoid-md](../../includes/ssnotedepfutureavoid-md.md)]

Execute the following Transact-SQL script. This example enables all indexes on the `HumanResources.Employee` table.

```sql
USE AdventureWorks2022;
GO

DBCC DBREINDEX ("HumanResources.Employee", " ");
GO
```

## Related content

- [ALTER INDEX (Transact-SQL)](../../t-sql/statements/alter-index-transact-sql.md)
- [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md)
- [DBCC DBREINDEX (Transact-SQL)](../../t-sql/database-console-commands/dbcc-dbreindex-transact-sql.md)
