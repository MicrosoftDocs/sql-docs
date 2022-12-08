---
title: "$PARTITION (Transact-SQL)"
description: "$PARTITION (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "4/22/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
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
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns the partition number into which a set of partitioning column values would be mapped for any specified partition function.
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
[ database_name. ] $PARTITION.partition_function_name(expression)  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

*database_name*  
Is the name of the database that contains the partition function.  
  
*partition_function_name*  
Is the name of any existing partition function against which a set of partitioning column values are being applied.  
  
*expression*  
Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) whose data type must either match or be implicitly convertible to the data type of its corresponding partitioning column. *expression* can also be the name of a partitioning column that currently participates in *partition_function_name*.  
  
## Return types

**int**  
  
## Remarks  

$PARTITION returns an **int** value between 1 and the number of partitions of the partition function.  
  
$PARTITION returns the partition number for any valid value, regardless of whether the value currently exists in a partitioned table or index that uses the partition function.  
  
## Examples  
  
### A. Get the partition number for a set of partitioning column values  

This example creates a partition function `RangePF1` using [RANGE LEFT](../../relational-databases/partitions/partitioned-tables-and-indexes.md#partition-function) that will partition a table or index into four partitions. $PARTITION is used to determine that the value `10`, representing the partitioning column of `RangePF1`, would be put in partition 1 of the table.  
  
```sql  
CREATE PARTITION FUNCTION RangePF1 ( INT )  
AS RANGE LEFT FOR VALUES (10, 100, 1000) ;  
GO

SELECT $PARTITION.RangePF1 (10) ;  
GO  
```  
  
### B. Get the number of rows in each nonempty partition of a partitioned table or index  

This example shows how to use `$PARTITION` to return the number of rows in each partition of table that contains data.

The example:
- Creates a partition scheme, `RangePS1`, for the partition function `RangePF1`. 
- Creates a table, `dbo.PartitionTable`, on the `RangePS1` partition scheme with `col1` as the partitioning column.
- Inserts four rows into the `dbo.PartitionTable` table. Based on the partition function definition, these rows will be inserted into partitions 2 and 3. Partitions 1 and 4 will remain empty.
- Queries the `dbo.PartitionTable` and uses `$PARTITION.RangePF1(col1)` in the GROUP BY clause to query the number of rows in each partition that contains data.
  
> [!NOTE]
> To execute this example, you must first create the partition function `RangePF1` using the code in the previous example.
  
```sql
CREATE PARTITION SCHEME RangePS1  
    AS PARTITION RangePF1  
    ALL TO ('PRIMARY') ;  
GO  

CREATE TABLE dbo.PartitionTable (col1 int PRIMARY KEY, col2 char(10))  
    ON RangePS1 (col1) ;  
GO

INSERT dbo.PartitionTable (col1, col2)
VALUES ((1,'a row'),(100,'another row'),(500,'another row'),(1000,'another row'))


SELECT 
	$PARTITION.RangePF1(col1) AS Partition,   
	COUNT(*) AS [COUNT] 
FROM dbo.PartitionTable
GROUP BY $PARTITION.RangePF1(col1)  
ORDER BY Partition ;  
GO  
``` 

The `SELECT` query should return the following results:

| Partition | COUNT |
|-----------|-------|
| 2         | 1     |
| 3         | 3     |

Rows are not returned for partitions number 1 and 4, which exist but do not contain data.

### C. Return all rows from one partition of a partitioned table or index  

The following example returns all rows that are in partition 3 of the table `PartitionTable`.  
  
```sql  
SELECT col1, col2
FROM dbo.PartitionTable
WHERE $PARTITION.RangePF1(col1) = 3 ;  
```

The query should return the following results:

| col1 | col2         |
|------|--------------|
| 101  | another row  |
| 500  | a third row  |
| 501  | a fourth row |
  
## Next steps

Learn more about table partitioning in these articles:

- [Partitioned tables and indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md)
- [CREATE PARTITION FUNCTION (Transact-SQL)](../statements/create-partition-function-transact-sql.md)
- [Modify a Partition Function](../../relational-databases/partitions/modify-a-partition-function.md)
- [Modify a Partition Scheme](../../relational-databases/partitions/modify-a-partition-scheme.md)
- [sys.partition_functions (Transact-SQL)](../../relational-databases/system-catalog-views/sys-partition-functions-transact-sql.md)
- [sys.partition_schemes (Transact-SQL)](../../relational-databases/system-catalog-views/sys-partition-schemes-transact-sql.md)
