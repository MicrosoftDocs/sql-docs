---
title: "$PARTITION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "$partition_TSQL"
  - "$partition"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "$PARTITION function"
  - "partitions [SQL Server], numbers"
ms.assetid: abc865d0-57a8-49da-8821-29457c808d2a
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# $PARTITION (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the partition number into which a set of partitioning column values would be mapped for any specified partition function in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
[ database_name. ] $PARTITION.partition_function_name(expression)  
```  
  
## Arguments  
 *database_name*  
 Is the name of the database that contains the partition function.  
  
 *partition_function_name*  
 Is the name of any existing partition function against which a set of partitioning column values are being applied.  
  
 *expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) whose data type must either match or be implicitly convertible to the data type of its corresponding partitioning column. *expression* can also be the name of a partitioning column that currently participates in *partition_function_name*.  
  
## Return Types  
 **int**  
  
## Remarks  
 $PARTITION returns an **int** value between 1 and the number of partitions of the partition function.  
  
 $PARTITION returns the partition number for any valid value, regardless of whether the value currently exists in a partitioned table or index that uses the partition function.  
  
## Examples  
  
### A. Getting the partition number for a set of partitioning column values  
 The following example creates a partition function `RangePF1` that will partition a table or index into four partitions. $PARTITION is used to determine that the value `10`, representing the partitioning column of `RangePF1`, would be put in partition 1 of the table.  
  
```  
USE AdventureWorks2012;  
GO  
CREATE PARTITION FUNCTION RangePF1 ( int )  
AS RANGE FOR VALUES (10, 100, 1000) ;  
GO  
SELECT $PARTITION.RangePF1 (10) ;  
GO  
```  
  
### B. Getting the number of rows in each nonempty partition of a partitioned table or index  
 The following example returns the number of rows in each partition of table `TransactionHistory` that contains data. The `TransactionHistory` table uses partition function `TransactionRangePF1` and is partitioned on the `TransactionDate` column.  
  
 To execute this example, you must first run the PartitionAW.sql script against the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database. For more information, see [PartitioningScript](https://go.microsoft.com/fwlink/?LinkId=201015).  
  
```  
USE AdventureWorks2012;  
GO  
SELECT $PARTITION.TransactionRangePF1(TransactionDate) AS Partition,   
COUNT(*) AS [COUNT] FROM Production.TransactionHistory   
GROUP BY $PARTITION.TransactionRangePF1(TransactionDate)  
ORDER BY Partition ;  
GO  
```  
  
### C. Returning all rows from one partition of a partitioned table or index  
 The following example returns all rows that are in partition `5` of the table `TransactionHistory`.  
  
> [!NOTE]  
>  To execute this example, you must first run the PartitionAW.sql script against the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database. For more information, see [PartitioningScript](https://go.microsoft.com/fwlink/?LinkId=201015).  
  
```  
SELECT * FROM Production.TransactionHistory  
WHERE $PARTITION.TransactionRangePF1(TransactionDate) = 5 ;  
```  
  
## See Also  
 [CREATE PARTITION FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-partition-function-transact-sql.md)  
  
  
