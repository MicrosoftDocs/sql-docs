---
title: Perform index operations online
description: Create, rebuild, or drop indexes online in the SQL Server Database Engine.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 03/04/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "index online operations [SQL Server]"
  - "online index operations"
  - "ONLINE option"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Perform index operations online

[!INCLUDE [SQL Server Azure SQL Database Azure SQL MI](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to create, rebuild, or drop indexes online in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. The `ONLINE` option allows concurrent user access to the underlying table or clustered index data and any associated nonclustered indexes during these index operations. For example, while a clustered index is being rebuilt by one user, that user and others can continue to update and query the underlying data.

When you perform data definition language (DDL) operations offline, such as building or rebuilding a clustered index, these operations hold exclusive (X) locks on the underlying data and associated indexes. This prevents modifications and queries to the underlying data until the index operation is complete.

> [!NOTE]  
> Index rebuild commands might hold exclusive locks on clustered indexes after a large object column is dropped from a table, even when performed online.

## Supported platforms

Online index operations aren't available in every edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).

Online index operations are available in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)].

## Limitations

We recommend performing online index operations for business environments that operate 24 hours a day, seven days a week, in which the need for concurrent user activity during index operations is vital.

The `ONLINE` option is available in the following [!INCLUDE [tsql](../../includes/tsql-md.md)] statements.

- [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md)
- [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md)
- [DROP INDEX](../../t-sql/statements/drop-index-transact-sql.md)
- [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) (To add or drop `UNIQUE` or `PRIMARY KEY` constraints with `CLUSTERED` index option)

For more limitations and restrictions concerning creating, rebuilding, or dropping indexes online, see [Guidelines for online index operations](guidelines-for-online-index-operations.md).

## Permissions

Requires `ALTER` permission on the table or view.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

1. In Object Explorer, select the plus sign to expand the database that contains the table on which you want to rebuild an index online.

1. Expand the **Tables** folder.

1. Select the plus sign to expand the table on which you want to rebuild an index online.

1. Expand the **Indexes** folder.

1. Right-click the index that you want to rebuild online and select **Properties**.

1. Under **Select a page**, select **Options**.

1. Select **Allow online DML processing**, and then select **True** from the list.

1. Select **OK**.

1. Right-click the index that you want to rebuild online and select **Rebuild**.

1. In the **Rebuild Indexes** dialog box, verify that the correct index is in the **Indexes to rebuild** grid and select **OK**.

## <a id="TsqlProcedure"></a> Use Transact-SQL

The following example rebuilds an existing online index in the AdventureWorks database.

```sql
ALTER INDEX AK_Employee_NationalIDNumber
    ON HumanResources.Employee
    REBUILD WITH (ONLINE = ON);
```

The following example deletes a clustered index online and moves the resulting table (heap) to the filegroup `NewGroup` by using the `MOVE TO` clause. The `sys.indexes`, `sys.tables`, and `sys.filegroups` catalog views are queried to verify the index and table placement in the filegroups before and after the move.

:::code language="sql" source="codesnippet/tsql/perform-index-operations_1.sql":::

For more information, see [ALTER INDEX (Transact-SQL)](../../t-sql/statements/alter-index-transact-sql.md).

## Related content

- [Resumable index considerations](guidelines-for-online-index-operations.md#resumable-index-considerations)
