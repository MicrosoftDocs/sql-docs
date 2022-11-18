---
title: "ISNUMERIC (Transact-SQL)"
description: "ISNUMERIC (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/13/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ISNUMERIC"
  - "ISNUMERIC_TSQL"
helpviewer_keywords:
  - "expressions [SQL Server], valid numeric type"
  - "numeric data"
  - "ISNUMERIC function"
  - "verifying valid numeric type"
  - "valid numeric type [SQL Server]"
  - "checking valid numeric type"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# ISNUMERIC (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Determines whether an expression is a valid numeric type.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql 
ISNUMERIC ( expression )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *expression*  
 Is the [expression](../../t-sql/language-elements/expressions-transact-sql.md) to be evaluated.  
  
## Return Types  
 **int**  
  
## Remarks  
 ISNUMERIC returns 1 when the input expression evaluates to a valid numeric data type; otherwise it returns 0. Valid [numeric data types](../../t-sql/data-types/numeric-types.md) include the following:  

| Area | Numeric data types |
|-|-|
| [Exact Numerics](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md) | **bigint**, **int**, **smallint**, **tinyint**, **bit** |
| [Fixed Precision](../../t-sql/data-types/decimal-and-numeric-transact-sql.md) | **decimal**, **numeric** |
| [Approximate](../../t-sql/data-types/float-and-real-transact-sql.md) | **float**, **real** |
| [Monetary Values](../../t-sql/data-types/money-and-smallmoney-transact-sql.md) | **money**, **smallmoney** |

> [!NOTE]  
> ISNUMERIC returns 1 for some characters that are not numbers, such as plus (+), minus (-), and valid currency symbols such as the dollar sign ($). For a complete list of currency symbols, see [money and smallmoney &#40;Transact-SQL&#41;](../../t-sql/data-types/money-and-smallmoney-transact-sql.md).  
  
## Examples  
 The following example uses `ISNUMERIC` to return all the postal codes that are not numeric values.  
  
```sql
USE AdventureWorks2012;  
GO  
SELECT City, PostalCode  
FROM Person.Address   
WHERE ISNUMERIC(PostalCode) <> 1;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example uses `ISNUMERIC` to return all the postal codes that are not numeric values.  
  
```sql
USE master;  
GO  
SELECT name, ISNUMERIC(name) AS IsNameANumber, database_id, ISNUMERIC(database_id) AS IsIdANumber   
FROM sys.databases;  
GO  
```  
  
## See also

- [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)
- [System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-category-transact-sql.md)
- [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)
