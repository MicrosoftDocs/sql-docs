---
title: "ROWCOUNT_BIG (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ROWCOUNT_BIG"
  - "ROWCOUNT_BIG_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ROWCOUNT_BIG function"
  - "number of rows affected by statement"
  - "row affected by statements [SQL Server]"
  - "statements [SQL Server], last statement"
  - "counting rows"
ms.assetid: 6e18a0eb-bb36-4348-90d9-8b1ecf095064
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# ROWCOUNT_BIG (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the number of rows affected by the last statement executed. This function operates like [@@ROWCOUNT](../../t-sql/functions/rowcount-transact-sql.md), except the return type of ROWCOUNT_BIG is **bigint**.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ROWCOUNT_BIG ( )  
```  
  
## Return Types  
 **bigint**  
  
## Remarks  
 Following a SELECT statement, this function returns the number of rows returned by the SELECT statement.  
  
 Following an INSERT, UPDATE, or DELETE statement, this function returns the number of rows affected by the data modification statement.  
  
 Following statements that do not return rows, such as an IF statement, this function returns 0.  
  
## See Also  
 [COUNT_BIG &#40;Transact-SQL&#41;](../../t-sql/functions/count-big-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
  
  
