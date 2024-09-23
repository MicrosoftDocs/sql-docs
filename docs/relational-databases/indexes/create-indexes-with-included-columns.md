---
title: "Create indexes with included columns"
description: Create indexes with included columns
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: vanto
ms.date: 09/17/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "index size [SQL Server]"
  - "index keys [SQL Server]"
  - "index columns [SQL Server]"
  - "size [SQL Server], indexes"
  - "key columns [SQL Server]"
  - "included columns"
  - "nonclustered indexes [SQL Server], included columns"
  - "designing indexes [SQL Server], included columns"
  - "nonkey columns"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Create indexes with included columns

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to add included (or nonkey) columns to extend the functionality of nonclustered indexes in [!INCLUDE [ssNoVersion_md](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. By including nonkey columns, you can create nonclustered indexes that cover more queries. This is because the nonkey columns have the following benefits:

- They can be data types not allowed as index key columns.
- They aren't considered by the [!INCLUDE [ssDE](../../includes/ssde-md.md)] when calculating the number of index key columns or index key size.

An index with nonkey columns can significantly improve query performance when all columns in the query are included in the index either as key or nonkey columns. Performance gains are achieved because the query optimizer can locate all the column values within the index; table or clustered index data isn't accessed resulting in fewer disk I/O operations.

> [!NOTE]  
> When an index contains all the columns referenced by a query it is typically referred to as *covering the query*.

## <a name="DesignRecs"></a> Design Recommendations

- Redesign nonclustered indexes that have a large index key size so that only columns used for searching and lookups are key columns. Make all other columns that cover the query into nonkey columns. In this way, you'll have all columns needed to cover the query, but the index key itself is small and efficient.

- Include nonkey columns in a nonclustered index to avoid exceeding the current index size limitations of a maximum of 32 key columns and a maximum index key size of 1,700 bytes (16 key columns and 900 bytes prior to [!INCLUDE [ssSQL15_md](../../includes/sssql16-md.md)]). The [!INCLUDE [ssDE](../../includes/ssde-md.md)] doesn't consider nonkey columns when calculating the number of index key columns or index key size.

- The order of nonkey columns in the index definition doesn't affect the performance of queries that use the index.

- Avoid very wide nonclustered indexes where the included columns don't represent a narrow enough subset of the underlying table columns. If adding wide indexes, always verify if the cost of updating one extra wide index offsets the cost of reading directly from the table.

## <a name="Restrictions"></a> Limitations and restrictions

- Nonkey columns can only be defined on nonclustered indexes.

- All data types except **text**, **ntext**, and **image** can be used as nonkey columns.

- Computed columns that are deterministic and either precise or imprecise can be nonkey columns. For more information, see [Indexes on computed columns](indexes-on-computed-columns.md).

- Computed columns derived from **image**, **ntext**, and **text** data types can be nonkey columns as long as the computed column data type is allowed as a nonkey index column.

- Nonkey columns can't be dropped from a table unless that table's index is dropped first.

- Nonkey columns can't be changed, except to do the following:

    - Change the nullability of the column from NOT NULL to NULL.

    - Increase the length of **varchar**, **nvarchar**, or **varbinary** columns.

## <a name="Security"></a> Security

### <a name="Permissions"></a> Permissions

Requires ALTER permission on the table or view. User must be a member of the **sysadmin** fixed server role or the **db_ddladmin** and **db_owner** fixed database roles.

## <a name="SSMSProcedure"></a> Using SQL Server Management Studio to create an index with nonkey columns

1. In Object Explorer, select the plus sign to expand the database that contains the table on which you want to create an index with nonkey columns.

1. Select the plus sign to expand the **Tables** folder.

1. Select the plus sign to expand the table on which you want to create an index with nonkey columns.

1. Right-click the **Indexes** folder, point to **New Index**, and select **Non-Clustered Index...**.

1. In the **New Index** dialog box, on the **General** page, enter the name of the new index in the **Index name** box.

1. Under the **Index key columns** tab, select **Add...**.

1. In the **Select Columns from** _table\_name_ dialog box, select the check box or check boxes of the table column or columns to be added to the index.

1. Select **OK**.

1. Under the **Included columns** tab, select **Add...**.

1. In the **Select Columns from**_table\_name_ dialog box, select the check box or check boxes of the table column or columns to be added to the index as nonkey columns.

1. Select **OK**.

1. In the **New Index** dialog box, select **OK**.

## <a name="TsqlProcedure"></a> Using Transact-SQL to create an index with nonkey columns

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

    ```sql
    USE AdventureWorks2022;
    GO
    -- Creates a nonclustered index on the Person.Address table with four included (nonkey) columns.
    -- index key column is PostalCode and the nonkey columns are
    -- AddressLine1, AddressLine2, City, and StateProvinceID.
    CREATE NONCLUSTERED INDEX IX_Address_PostalCode
    ON Person.Address (PostalCode)
    INCLUDE (AddressLine1, AddressLine2, City, StateProvinceID);
    GO
    ```

## Related content

- [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md)
- [SQL Server and Azure SQL index architecture and design guide](../sql-server-index-design-guide.md)
