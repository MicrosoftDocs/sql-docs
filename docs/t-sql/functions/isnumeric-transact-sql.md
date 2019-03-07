---
title: "ISNUMERIC (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ISNUMERIC"
  - "ISNUMERIC_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "expressions [SQL Server], valid numeric type"
  - "numeric data"
  - "ISNUMERIC function"
  - "verifying valid numeric type"
  - "valid numeric type [SQL Server]"
  - "checking valid numeric type"
ms.assetid: 7aa816de-529a-4f6c-a99f-4d5a9ef599eb
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ISNUMERIC (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Determines whether an expression is a valid numeric type.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
ISNUMERIC ( expression )  
```  
  
## Arguments  
 *expression*  
 Is the [expression](../../t-sql/language-elements/expressions-transact-sql.md) to be evaluated.  
  
## Return Types  
 **int**  
  
## Remarks  
 ISNUMERIC returns 1 when the input expression evaluates to a valid numeric data type; otherwise it returns 0. Valid [numeric data types](../../t-sql/data-types/numeric-types.md) include the following:  

|||
|-|-|
| [Exact Numerics](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md) | **bigint**, **int**, **smallint**, **tinyint**, **bit** |
| [Fixed Precision](../../t-sql/data-types/decimal-and-numeric-transact-sql.md) | **decimal**, **numeric** |
| [Approximate](../../t-sql/data-types/float-and-real-transact-sql.md) | **float**, **real** |
| [Monetary Values](../../t-sql/data-types/money-and-smallmoney-transact-sql.md) | **money**, **smallmoney** |

  
> [!NOTE]  
>  ISNUMERIC returns 1 for some characters that are not numbers, such as plus (+), minus (-), and valid currency symbols such as the dollar sign ($). For a complete list of currency symbols, see [money and smallmoney &#40;Transact-SQL&#41;](../../t-sql/data-types/money-and-smallmoney-transact-sql.md).  
  
## Examples  
 The following example uses `ISNUMERIC` to return all the postal codes that are not numeric values.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT City, PostalCode  
FROM Person.Address   
WHERE ISNUMERIC(PostalCode)<> 1;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example uses `ISNUMERIC` to return all the postal codes that are not numeric values.  
  
```  
USE master;  
GO  
SELECT name, isnumeric(name) AS IsNameANumber, database_id, isnumeric(database_id) AS IsIdANumber   
FROM sys.databases;  
GO  
```  
  
## See Also  
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-for-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
  
  

