---
title: "$PARTITION (Transact-SQL)"
description: "$PARTITION returns the partition number into which a set of partitioning column values can be mapped for any specified partition function."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 01/07/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "$partition_TSQL"
  - "$partition"
helpviewer_keywords:
  - "$PARTITION function"
  - "partitions [SQL Server], numbers"
dev_langs:
  - "TSQL"
---
# $PARTITION (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Returns the partition number into which a set of partitioning column values can be mapped for any specified partition function.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
[ database_name. ] $PARTITION.partition_function_name(expression)
```

## Arguments

#### *database_name*

The name of the database that contains the partition function.

#### *partition_function_name*

The name of any existing partition function against which a set of partitioning column values are being applied.

#### *expression*

An [expression](../../t-sql/language-elements/expressions-transact-sql.md) whose data type must either match or be implicitly convertible to the data type of its corresponding partitioning column. This parameter can also be the name of a partitioning column that currently participates in *partition_function_name*.

## Return types

**int**

## Remarks

`$PARTITION` returns an **int** value between `1` and the number of partitions of the partition function.

`$PARTITION` returns the partition number for any valid value, regardless of whether the value currently exists in a partitioned table or index that uses the partition function.

## Examples

### A. Get the partition number for a set of partitioning column values

This example creates a partition function `RangePF1` using [RANGE LEFT](../../relational-databases/partitions/partitioned-tables-and-indexes.md#partition-function) that will partition a table or index into four partitions. `$PARTITION` is used to determine that the value `10`, representing the partitioning column of `RangePF1`, would be put in partition `1` of the table.

```sql
CREATE PARTITION FUNCTION RangePF1(INT)
    AS RANGE LEFT
    FOR VALUES (10, 100, 1000);
GO

SELECT $PARTITION.RangePF1 (10);
GO
```

### B. Get the number of rows in each nonempty partition of a partitioned table or index

This example shows how to use `$PARTITION` to return the number of rows in each partition of table that contains data.

> [!NOTE]  
> To execute this example, you must first create the partition function `RangePF1` using the code in the previous example.

1. Create a partition scheme, `RangePS1`, for the partition function `RangePF1`.

   ```sql
   CREATE PARTITION SCHEME RangePS1
       AS PARTITION RangePF1
       ALL TO ('PRIMARY');
   GO
   ```

1. Create a table, `dbo.PartitionTable`, on the `RangePS1` partition scheme with `col1` as the partitioning column.

   ```sql
   CREATE TABLE dbo.PartitionTable
   (
       col1 INT PRIMARY KEY,
       col2 CHAR (20)
   ) ON RangePS1 (col1);
   GO
   ```

1. Insert four rows into the `dbo.PartitionTable` table. These rows are inserted into partitions based on the partition function `RangePF1` definition: `1` and `10` go to partition `1`, while `500` and `1000` go to `3`.

   ```sql
   INSERT dbo.PartitionTable (col1, col2)
   VALUES (1, 'a row'),
       (10, 'another row'),
       (500, 'another row'),
       (1000, 'another row');
   GO
   ```

1. Query the `dbo.PartitionTable` and uses `$PARTITION.RangePF1(col1)` in the `GROUP BY` clause to query the number of rows in each partition that contains data.

   ```sql
   SELECT $PARTITION.RangePF1 (col1) AS Partition,
          COUNT(*) AS [COUNT]
   FROM dbo.PartitionTable
   GROUP BY $PARTITION.RangePF1 (col1)
   ORDER BY Partition;
   GO
   ```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

| Partition | COUNT |
| --- | --- |
| 1 | 2 |
| 3 | 2 |

Rows aren't returned for partition number `2`, which exists but doesn't contain data.

### C. Return all rows from one partition of a partitioned table or index

The following example returns all rows that are in partition 3 of the table `PartitionTable`.

```sql
SELECT col1, col2
FROM dbo.PartitionTable
WHERE $PARTITION.RangePF1 (col1) = 3;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

| col1 | col2 |
| --- | --- |
| `500` | another row |
| `1000` | another row |

## Related content

- [Partitioned tables and indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md)
- [CREATE PARTITION FUNCTION (Transact-SQL)](../statements/create-partition-function-transact-sql.md)
- [Modify a partition function](../../relational-databases/partitions/modify-a-partition-function.md)
- [Modify a partition scheme](../../relational-databases/partitions/modify-a-partition-scheme.md)
- [sys.partition_functions (Transact-SQL)](../../relational-databases/system-catalog-views/sys-partition-functions-transact-sql.md)
- [sys.partition_schemes (Transact-SQL)](../../relational-databases/system-catalog-views/sys-partition-schemes-transact-sql.md)
