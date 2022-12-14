---
title: "ROWCOUNT_BIG (Transact-SQL)"
description: "ROWCOUNT_BIG (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "03/13/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "ROWCOUNT_BIG"
  - "ROWCOUNT_BIG_TSQL"
helpviewer_keywords:
  - "ROWCOUNT_BIG function"
  - "number of rows affected by statement"
  - "row affected by statements [SQL Server]"
  - "statements [SQL Server], last statement"
  - "counting rows"
dev_langs:
  - "TSQL"
---
# ROWCOUNT_BIG (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the number of rows affected by the last statement executed. This function operates like [@@ROWCOUNT](../../t-sql/functions/rowcount-transact-sql.md), except the return type of ROWCOUNT_BIG is **bigint**.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
ROWCOUNT_BIG ( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 **bigint**  
  
## Remarks  
 Following a SELECT statement, this function returns the number of rows returned by the SELECT statement.  
  
 Following an INSERT, UPDATE, or DELETE statement, this function returns the number of rows affected by the data modification statement.  
  
 Following statements that do not return rows, such as an IF statement, this function returns 0.  
  
## See Also  
 [COUNT_BIG &#40;Transact-SQL&#41;](../../t-sql/functions/count-big-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
  
  
