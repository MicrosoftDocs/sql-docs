---
title: Update statistics
description: Learn how to update query optimization statistics on a table or indexed view in SQL Server by using SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: derekw, randolphwest
ms.date: 04/01/2024
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords:
  - "updating statistics"
  - "statistics [SQL Server], updating"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Update statistics

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

You can update query optimization statistics on a table or indexed view in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. By default, the query optimizer already updates statistics as necessary to improve the query plan; in some cases you can improve query performance by using `UPDATE STATISTICS` or the stored procedure `sp_updatestats` to update statistics more frequently than the default updates.

Updating statistics ensures that queries compile with up-to-date statistics. However, updating statistics causes queries to recompile. We recommend not updating statistics too frequently, because there's a performance tradeoff between improving query plans and the time it takes to recompile queries. The specific tradeoffs depend on your application. `UPDATE STATISTICS` can use `tempdb` to sort the sample of rows for building statistics.

## Permissions

If using `UPDATE STATISTICS` or making changes through [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], requires ALTER permission on the table or view. If using `sp_updatestats`, requires membership in the **sysadmin** fixed server role, or ownership of the database (**dbo**).

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

#### Update a statistics object

1. In **Object Explorer**, select the plus sign to expand the database in which you want to update the statistic.

1. Select the plus sign to expand the **Tables** folder.

1. Select the plus sign to expand the table in which you want to update the statistic.

1. Select the plus sign to expand the **Statistics** folder.

1. Right-click the statistics object you wish to update and select **Properties**.

1. In the **Statistics Properties -**_statistics\_name_ dialog box, select the **Update statistics for these columns** check box and then select **OK**.

## <a id="TsqlProcedure"></a> Use Transact-SQL

### Update a specific statistics object

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

    ```sql
    USE AdventureWorks2022;
    GO
    -- The following example updates the statistics for the AK_SalesOrderDetail_rowguid index of the SalesOrderDetail table.
    UPDATE STATISTICS Sales.SalesOrderDetail AK_SalesOrderDetail_rowguid;
    GO
    ```

### Update all statistics in a table

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

    ```sql
    USE AdventureWorks2022;
    GO
    -- The following example updates the statistics for all indexes on the SalesOrderDetail table.
    UPDATE STATISTICS Sales.SalesOrderDetail;
    GO
    ```

For more information, see [UPDATE STATISTICS](../../t-sql/statements/update-statistics-transact-sql.md).

### Update all statistics in a database

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

    ```sql
    USE AdventureWorks2022;
    GO
    -- The following example updates the statistics for all tables in the database.
    EXEC sp_updatestats;
    ```

### Automatic index and statistics management

Use solutions such as [Adaptive Index Defrag](https://github.com/Microsoft/tigertoolbox/tree/master/AdaptiveIndexDefrag) to automatically manage index defragmentation and statistics updates for one or more databases. This procedure automatically chooses whether to rebuild or reorganize an index according to its fragmentation level, among other parameters, and update statistics with a linear threshold.

## Related content

- [UPDATE STATISTICS (Transact-SQL)](../../t-sql/statements/update-statistics-transact-sql.md)
