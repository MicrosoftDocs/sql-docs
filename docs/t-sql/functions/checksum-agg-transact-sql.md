---
title: "CHECKSUM_AGG (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CHECKSUM_AGG"
  - "CHECKSUM_AGG_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "checksum values"
  - "CHECKSUM_AGG function"
  - "groups [SQL Server], checksum values"
ms.assetid: cdede70c-4eb5-4c92-98ab-b07787ab7222
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# CHECKSUM_AGG (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

This function returns the checksum of the values in a group. `CHECKSUM_AGG` ignores null values. The [OVER clause](../../t-sql/queries/select-over-clause-transact-sql.md) can follow `CHECKSUM_AGG`.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
CHECKSUM_AGG ( [ ALL | DISTINCT ] expression )  
```  
  
## Arguments  
**ALL**  
Applies the aggregate function to all values. ALL is the default argument.
  
DISTINCT  
Specifies that `CHECKSUM_AGG` returns the checksum of unique values.
  
*expression*  
An integer [expression](../../t-sql/language-elements/expressions-transact-sql.md). `CHECKSUM_AGG` does not allow use of aggregate functions or subqueries.
  
## Return types
Returns the checksum of all *expression* values as **int**.
  
## Remarks  
`CHECKSUM_AGG` can detect changes in a table.
  
The `CHECKSUM_AGG` result does not depend on the order of the rows in the table. Also, `CHECKSUM_AGG` functions allow the use of the `DISTINCT` keyword and the `GROUP BY` clause.
  
If an expression list value changes, the list checksum value list will also probably change. However, a small possibility exists that the calculated checksum will not change.
  
`CHECKSUM_AGG` has functionality similar to that of other aggregate functions. For more information, see [Aggregate Functions &#40;Transact-SQL&#41;](../../t-sql/functions/aggregate-functions-transact-sql.md).
  
## Examples  
These examples use `CHECKSUM_AGG` to detect changes in the `Quantity` column of the `ProductInventory` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.
  
```sql
--Get the checksum value before the column value is changed.  

SELECT CHECKSUM_AGG(CAST(Quantity AS int))  
FROM Production.ProductInventory;  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
------------------------  
262  
```  
  
```sql
UPDATE Production.ProductInventory   
SET Quantity=125  
WHERE Quantity=100;  
GO  

--Get the checksum of the modified column.  
SELECT CHECKSUM_AGG(CAST(Quantity AS int))  
FROM Production.ProductInventory;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
------------------------  
287  
```  
  
## See also
[CHECKSUM &#40;Transact-SQL&#41;](../../t-sql/functions/checksum-transact-sql.md)  
[HASHBYTES &#40;Transact-SQL&#41;](../../t-sql/functions/hashbytes-transact-sql.md)  
[BINARY_CHECKSUM  &#40;Transact-SQL&#41;](../../t-sql/functions/binary-checksum-transact-sql.md)
[OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md)
  
