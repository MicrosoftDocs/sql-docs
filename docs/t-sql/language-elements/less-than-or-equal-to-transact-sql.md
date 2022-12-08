---
title: "<= (Less Than or Equal To) (Transact-SQL)"
description: "&lt;= (Less Than or Equal To) (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "03/13/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "<=_TSQL"
helpviewer_keywords:
  - "<= (less than or equal to operator)"
  - "less than or equal to operator (<=)"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---

# &lt;= (Less Than or Equal To) (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Compares two expressions (a comparison operator). When you compare nonnull expressions, the result is TRUE if the left operand has a value lower than or equal to the right operand; otherwise, the result is FALSE.  
  
Unlike the = (equality) comparison operator, the result of the >= comparison of two NULL values does not depend on the ANSI_NULLS setting.  
 
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
expression <= expression  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

*expression*  

Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md). Both expressions must have implicitly convertible data types. The conversion depends on the rules of [data type precedence](../../t-sql/data-types/data-type-precedence-transact-sql.md).  
  
## Result Types  

**Boolean**  
  
## Examples  
  
### A. Using <= in a simple query  

The following example returns all rows in the `HumanResources.Department` table that have a value in `DepartmentID` that is less than or equal to the value 3.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT DepartmentID, Name  
FROM HumanResources.Department  
WHERE DepartmentID <= 3  
ORDER BY DepartmentID;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
DepartmentID Name  
------------ --------------------------------------------------  
1            Engineering  
2            Tool Design  
3            Sales  
  
(3 row(s) affected)  
  
```  
  
## See Also  
- [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
- [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)
