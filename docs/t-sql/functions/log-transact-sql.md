---
title: "LOG (Transact-SQL)"
description: "LOG (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/29/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "LOG"
  - "LOG_TSQL"
helpviewer_keywords:
  - "float expressions"
  - "logarithm of expression"
  - "LOG function"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# LOG (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the natural logarithm of the specified **float** expression in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
-- Syntax for SQL Server, Azure SQL Database  
  
LOG ( float_expression [, base ] )  
```  
  
```syntaxsql
-- Syntax for Azure Synapse SQL 
  
LOG ( float_expression )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *float_expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of type **float** or of a type that can be implicitly converted to **float**.  
  
 *base*  
 Optional integer argument that sets the base for the logarithm.  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later
  
## Return Types  
 **float**  
  
## Remarks  
 By default, **LOG()** returns the natural logarithm. Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], you can change the base of the logarithm to another value by using the optional *base* parameter.  
  
 The natural logarithm is the logarithm to the base **e**, where **e** is an irrational constant approximately equal to 2.718281828.  
  
 The natural logarithm of the exponential of a number is the number itself: LOG( EXP( *n* ) ) = *n*. And the exponential of the natural logarithm of a number is the number itself: EXP( LOG( *n* ) ) = *n*.  
  
## Examples  
  
### A. Calculating the logarithm for a number.  
 The following example calculates the `LOG` for the specified **float** expression.  
  
```sql  
DECLARE @var FLOAT = 10;  
SELECT 'The LOG of the variable is: ' + CONVERT(VARCHAR, LOG(@var));  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
-------------------------------------  
The LOG of the variable is: 2.30259  
  
(1 row(s) affected)  
```  
  
### B. Calculating the logarithm of the exponent of a number.  
 The following example calculates the `LOG` for the exponent of a number.  
  
```sql  
SELECT LOG (EXP (10));  
  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
----------------------------------  
10  
(1 row(s) affected)  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. Calculating the logarithm for a number  
 The following example calculates the `LOG` for the specified **float** expression.  
  
```sql  
SELECT LOG(10);  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 ----------------`  
  
 2.30
 ```  
  
## See Also  
 [Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)   
 [EXP &#40;Transact-SQL&#41;](../../t-sql/functions/exp-transact-sql.md)   
 [LOG10 &#40;Transact-SQL&#41;](../../t-sql/functions/log10-transact-sql.md)  
  
  

