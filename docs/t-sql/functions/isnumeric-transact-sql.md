---
title: "ISNUMERIC (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 44
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# ISNUMERIC (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Determines whether an expression is a valid numeric type.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
ISNUMERIC ( expression )  
```  
  
## Arguments  
 *expression*  
 Is the [expression](../../t-sql/language-elements/expressions-transact-sql.md) to be evaluated.  
  
## Return Types  
 **int**  
  
## Remarks  
 ISNUMERIC returns 1 when the input expression evaluates to a valid numeric data type; otherwise it returns 0. Valid numeric data types include the following:  
  
|||  
|-|-|  
|**int**|**numeric**|  
|**bigint**|**money**|  
|**smallint**|**smallmoney**|  
|**tinyint**|**float**|  
|**decimal**|**real**|  
  
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
  
  

