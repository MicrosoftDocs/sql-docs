---
title: "Create filtered indexes"
description: A filtered index is an optimized disk-based rowstore nonclustered index especially suited to cover queries that select from a well-defined subset of data.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 10/14/2022
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "filtered indexes [SQL Server], about filtered indexes"
  - "designing indexes [SQL Server], filtered"
  - "filtered indexes [SQL Server]"
  - "nonclustered indexes [SQL Server], filtered"
  - "indexes [SQL Server], filtered"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create filtered indexes

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This article describes how to create a filtered index using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS) or [!INCLUDE[tsql](../../includes/tsql-md.md)]. A filtered index is an optimized disk-based rowstore nonclustered index especially suited to cover queries that select from a well-defined subset of data. It uses a filter predicate to index a portion of rows in the table. A well-designed filtered index can improve query performance and reduce index maintenance and storage costs compared with full-table indexes.

Filtered indexes can provide the following advantages over full-table indexes:

1. Improved query performance and plan quality.

   A well-designed filtered index improves query performance and execution plan quality because it is smaller than a full-table nonclustered index and has filtered statistics. The filtered statistics are more accurate than full-table statistics because they cover only the rows in the filtered index.

1. Reduced index maintenance costs.

   An index is maintained only when data manipulation language (DML) statements affect the data in the index. A filtered index reduces index maintenance costs compared with a full-table nonclustered index because it is smaller and is only maintained when the data in the index is changed. It is possible to have a large number of filtered indexes, especially when they contain data that is changed infrequently. Similarly, if a filtered index contains only the frequently modified data, the smaller size of the index reduces the cost of updating the statistics.

1. Reduced index storage costs.

   Creating a filtered index can reduce disk storage for nonclustered indexes when a full-table index isn't necessary. You can replace a full-table nonclustered index with multiple filtered indexes without significantly increasing the storage requirements.

## <a id="Design"></a> Design considerations

When a column only has a few relevant values for queries, you can create a filtered index on the subset of values.  The resulting index will be smaller and cost less to maintain than a full-table nonclustered index defined on the same key columns.

For example, consider a filtered index in the following data scenarios. In each case, the `WHERE` clause of the filtered index should be a subset of the `WHERE` clause of a query to benefit from the filtered index.

- When the values in a column are mostly NULL and the query selects only from the non-NULL values. You can create a filtered index for the non-NULL data rows.
- When rows in a table are marked as processed by a recurring workflow or queue process. Over time, most rows in the table will be marked as processed. A filtered index on rows that aren't yet processed would benefit the recurring query that looks for rows that aren't yet processed.
- When a table has heterogeneous data rows. You can create a filtered index for one or more categories of data. This can improve the performance of queries on these data rows by narrowing the focus of a query to a specific area of the table. Again, the resulting index will be smaller and cost less to maintain than a full-table nonclustered index.

## <a id="Restrictions"></a> Limitations and restrictions

- You can't create a filtered index on a view. However, the query optimizer can benefit from a filtered index defined on a table that is referenced in a view. The query optimizer considers a filtered index for a query that selects from a view if the query results will be correct.

- You can't create a filtered index on a table when the column accessed in the filter expression is of a CLR data type.

- Filtered indexes have the following advantages over indexed views:

  - Reduced index maintenance costs. For example, the query processor uses fewer CPU resources to update a filtered index than an indexed view.

  - Improved plan quality. For example, during query compilation, the query optimizer considers using a filtered index in more situations than the equivalent indexed view.

  - Online index rebuilds. You can rebuild filtered indexes while they are available for queries. Online index rebuilds aren't supported for indexed views. For more information, see the `REBUILD` option for [ALTER INDEX (Transact-SQL)](../../t-sql/statements/alter-index-transact-sql.md).

  - Non-unique indexes. Filtered indexes can be non-unique, whereas indexed views must be unique.

- Filtered indexes are defined on one table and only support simple [comparison operators](../../t-sql/language-elements/comparison-operators-transact-sql.md). If you need a filter expression that references multiple tables or has complex logic, you should create a view. Filtered indexes don't support `LIKE` operators.

- A column in the filtered index expression doesn't need to be a key or included column in the filtered index definition if the filtered index expression is equivalent to the query predicate and the query doesn't return the column in the filtered index expression with the query results.

- A column in the filtered index expression should be a key or included column in the filtered index definition if the query predicate uses the column in a comparison that isn't equivalent to the filtered index expression.

- A column in the filtered index expression should be a key or included column in the filtered index definition if the column is in the query result set.

- The clustered index key of the table doesn't need to be a key or included column in the filtered index definition. The clustered index key is automatically included in all nonclustered indexes, including filtered indexes. Learn more in the [index architecture and design guide](../sql-server-index-design-guide.md#nonclustered-index-architecture).

- If the comparison operator specified in the filtered index expression of the filtered index results in an implicit or explicit data conversion, an error will occur if the conversion occurs on the left side of a comparison operator. A solution is to write the filtered index expression with the data conversion operator (`CAST` or `CONVERT`) on the right side of the comparison operator.

- Review the required `SET` options for filtered index creation in [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md) syntax

- Filters can't be applied to primary key or unique constraints, but can be applied to indexes with the `UNIQUE` property.

- You can't create a filtered index on a computed column.

## Permissions

Requires ALTER permission on the table or view. The user must be a member of the **sysadmin** fixed server role or the **db_ddladmin** and **db_owner** fixed database roles. To modify the filtered index expression, use `CREATE INDEX WITH DROP_EXISTING`.

## <a id="SSMSProcedure"></a> Create a filtered index with SSMS

1. In Object Explorer, select the plus sign to expand the database that contains the table on which you want to create a filtered index.

1. Select the plus sign to expand the **Tables** folder.

1. Select the plus sign to expand the table on which you want to create a filtered index.

1. Right-click the **Indexes** folder, point to **New Index**, and select **Non-Clustered Index...**.

1. In the **New Index** dialog box, on the **General** page, enter the name of the new index in the **Index name** box.

1. Under **Index key columns**, select **Add...**.

1. In the **Select Columns from**_table\_name_ dialog box, select the check box or check boxes of the table column or columns to be added to the index.

1. Select **OK**.

1. On the **Filter** page, under **Filter Expression**, enter SQL expression that you'll use to create the filtered index.

1. Select **OK**.

## <a id="TsqlProcedure"></a> Create a filtered index with Transact-SQL

This example uses the `AdventureWorks2019` database, available for download at [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md).

1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

```sql
USE AdventureWorks2019;
GO

DROP INDEX IF EXISTS FIBillOfMaterialsWithEndDate
    ON Production.BillOfMaterials
GO

CREATE NONCLUSTERED INDEX FIBillOfMaterialsWithEndDate
    ON Production.BillOfMaterials (ComponentID, StartDate)
    WHERE EndDate IS NOT NULL ;
GO
```

The filtered index `FIBillOfMaterialsWithEndDate` is valid for the following query. [You can display the query execution plan](../performance/display-an-actual-execution-plan.md) to determine if the query optimizer used the filtered index.

```sql
USE AdventureWorks2019;
GO

SELECT ProductAssemblyID, ComponentID, StartDate
FROM Production.BillOfMaterials
WHERE EndDate IS NOT NULL
    AND ComponentID = 5
    AND StartDate > '01/01/2008' ;
GO
```

## Next steps

To learn more about creating indexes and related concepts, see the following articles:

- [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md)
- [SQL Server and Azure SQL index architecture and design guide](../sql-server-index-design-guide.md)
- [Display an actual execution plan](../performance/display-an-actual-execution-plan.md)
- [Index types](indexes.md)
