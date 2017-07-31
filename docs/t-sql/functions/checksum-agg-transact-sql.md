---
title: "CHECKSUM_AGG (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 38
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# CHECKSUM_AGG (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns the checksum of the values in a group. Null values are ignored. Can be followed by the [OVER clause](../../t-sql/queries/select-over-clause-transact-sql.md).
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
CHECKSUM_AGG ( [ ALL | DISTINCT ] expression )  
```  
  
## Arguments  
**ALL**  
Applies the aggregate function to all values. ALL is the default.
  
DISTINCT  
Specifies that CHECKSUM_AGG returns the checksum of unique values.
  
*expression*  
Is an integer [expression](../../t-sql/language-elements/expressions-transact-sql.md). Aggregate functions and subqueries are not allowed.
  
## Return types
Returns the checksum of all *expression* values as **int**.
  
## Remarks  
CHECKSUM_AGG can be used to detect changes in a table.
  
The order of the rows in the table does not affect the result of CHECKSUM_AGG. Also, CHECKSUM_AGG functions may be used with the DISTINCT keyword and the GROUP BY clause.
  
If one of the values in the expression list changes, the checksum of the list also generally changes. However, there is a small chance that the checksum will not change.
  
CHECKSUM_AGG has similar functionality with other aggregate functions. For more information, see [Aggregate Functions &#40;Transact-SQL&#41;](../../t-sql/functions/aggregate-functions-transact-sql.md).
  
## Examples  
The following example uses `CHECKSUM_AGG` to detect changes in the `Quantity` column of the `ProductInventory` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.
  
```sql
--Get the checksum value before the column value is changed.  
SELECT CHECKSUM_AGG(CAST(Quantity AS int))  
FROM Production.ProductInventory;  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
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
  
```sql
------------------------  
287  
```  
  
## See also
[CHECKSUM &#40;Transact-SQL&#41;](../../t-sql/functions/checksum-transact-sql.md)  
[OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md)
  
  
